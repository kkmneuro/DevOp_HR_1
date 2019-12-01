using NeuroTraderProtocols;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NeuroXChange.Model.ServerConnection
{
    public class ServerConnector
    {
        private MainNeuroXModel model;

        public bool SaveCredentials { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
        //public string Server { get; private set; }
        public string UsbPort { get; set; }
        public string ErrorMessage { get; set; }
        public string OrderSocket { get; set; }
        public string QuotesSocket { get; set; }

        // connection objects
        //public TcpClient tcpClient { get; private set; }
        //public SslStream sslStream { get; private set; }

        public ServerConnector(MainNeuroXModel model)
        {
            this.model = model;

            UserLogin = string.Empty;
            UserPassword = string.Empty;

            SaveCredentials = bool.Parse(model.iniFileReader.Read("SaveCredentials", "Authorisation", "false"));
            if (SaveCredentials)
            {
                UserLogin = model.iniFileReader.Read("Login", "Authorisation", string.Empty);
                UserPassword = model.iniFileReader.Read("Password", "Authorisation", string.Empty);
                OrderSocket = model.iniFileReader.Read("OrderSocket", "Authorisation", string.Empty); ;
                QuotesSocket = model.iniFileReader.Read("QuoteSocket", "Authorisation", string.Empty); ;
            }

            //Server = model.iniFileReader.Read("Server", "Authorisation", "127.0.0.1");
            //Port = ushort.Parse(model.iniFileReader.Read("Port", "Authorisation", "7657"));
        }

        public void UpdateINICredentials()
        {
            //model.iniFileReader.Write("SaveCredentials", SaveCredentials.ToString(), "Authorisation");
            model.iniFileReader.Write("Login", SaveCredentials ? UserLogin : string.Empty, "Authorisation");
            model.iniFileReader.Write("Password", SaveCredentials ? UserPassword : string.Empty, "Authorisation");

            if (UsbPort!=null)
            model.iniFileReader.Write("TPSUSBPort", @"\\.\" + UsbPort.ToString(), "BioData");
            //model.iniFileReader.Read("TPSUSBPort", "BioData", "\\\\.\\COM5");
        }

        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.RemoteCertificateNotAvailable ||
                sslPolicyErrors == SslPolicyErrors.RemoteCertificateNameMismatch)
            {
                return false;
            }

            return true;
        }


        public string isQuotesLogonSuccessful = "";
        public string isOrdersLogonSuccessful = "";

        private void INSTANCE_OnLogonResponceQuotes(string reason, string brokerName)
        {
            if (reason == "VALID")
            {
                isQuotesLogonSuccessful = "TRUE";
            }
            else
            {
                isQuotesLogonSuccessful = "FALSE";
                ErrorMessage = reason;
            }

        }

        private void INSTANCE_OnLogonResponceOrders(string reason, string brokerName)
        {
           
                if (reason == "VALID")
                {
                isOrdersLogonSuccessful = "TRUE";
                //this.model.WsHelperOrders.participantRequest();
            }
                else
                {
                isOrdersLogonSuccessful = "FALSE";
                ErrorMessage = reason;
                }           

        }



        public bool Connect()
        {


            this.model.WsHelperQuotes.OnLogonResponse += new Action<string, string>(INSTANCE_OnLogonResponceQuotes);
            this.model.WsHelperQuotes.Init(UserLogin, UserPassword, QuotesSocket);
            ////await Task.Delay(2000);

            this.model.WsHelperOrders.OnLogonResponse += new Action<string, string>(INSTANCE_OnLogonResponceOrders);
            this.model.WsHelperOrders.Init(UserLogin, UserPassword, OrderSocket);
            //await Task.Delay(2000);           

           

            while(isQuotesLogonSuccessful == "" || isOrdersLogonSuccessful == "")
            {
                //System.Threading.Thread.Sleep(1000);
            }

            if (isQuotesLogonSuccessful == "TRUE" && isOrdersLogonSuccessful == "TRUE")
            {
                Globals.CurrentLoginStatus = LoginStatus.Success;
                return true;
            }

            Globals.CurrentLoginStatus = LoginStatus.Failure;
            return false;
      
            }

        public void Synchronize()
        {
            
        }

        public void Disconnect()
        {
         }
    }
}
