using NeuroTraderProtocols;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace NeuroXChange.Model.ServerConnection
{
    public class ServerConnector
    {
        private MainNeuroXModel model;

        public bool SaveCredentials { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
        public string Server { get; private set; }
        public ushort Port { get; private set; }

        // connection objects
        public TcpClient tcpClient { get; private set; }
        public SslStream sslStream { get; private set; }

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
            }

            Server = model.iniFileReader.Read("Server", "Authorisation", "127.0.0.1");
            Port = ushort.Parse(model.iniFileReader.Read("Port", "Authorisation", "7657"));
        }

        public void UpdateINICredentials()
        {
            model.iniFileReader.Write("SaveCredentials", SaveCredentials.ToString(), "Authorisation");
            model.iniFileReader.Write("Login", SaveCredentials ? UserLogin : string.Empty, "Authorisation");
            model.iniFileReader.Write("Password", SaveCredentials ? UserPassword : string.Empty, "Authorisation");
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

        public bool Connect(out string errorMessage)
        {
            bool result = false;

            errorMessage = null;

            // Connect to a remote device.  
            try
            {
                // Establish the remote endpoint for the socket.  
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Server);
                IPAddress ipAddress = ipHostInfo.AddressList.First(address => address.AddressFamily == AddressFamily.InterNetwork);
                string ipAddressString = ipAddress.ToString();

                tcpClient = new TcpClient(ipAddressString, Port);
                sslStream = new SslStream(tcpClient.GetStream(), false,
                    new RemoteCertificateValidationCallback(ValidateServerCertificate), null);
                sslStream.AuthenticateAsClient("WS2016DC-1");

                BinaryWriter writer = new BinaryWriter(sslStream);
                writer.Write((byte)NTProtocolHeader.General);
                writer.Write((byte)NTProtocolHeader.Authorisation);
                writer.Flush();

                BinaryFormatter formatter = new BinaryFormatter();
                var credentials = new AuthorisationPacket {Login = UserLogin, Password = UserPassword };
                formatter.Serialize(sslStream, credentials);
                sslStream.Flush();

                // Receive the response from the remote device.
                byte[] bytes = new byte[4];
                int bytesRec = sslStream.Read(bytes, 0, 4);

                if (bytesRec != 1)
                {
                    errorMessage = "Error on server side";
                    Disconnect();
                }

                AuthorisationResult authResult = (AuthorisationResult)bytes[0];

                switch (authResult)
                {
                    case AuthorisationResult.UnknownError:
                        errorMessage = "Unknown error";
                        break;
                    case AuthorisationResult.Ok:
                        result = true;
                        break;
                    case AuthorisationResult.ProtocolError:
                        errorMessage = "Error in the protocol";
                        break;
                    case AuthorisationResult.NoSuchUser:
                        errorMessage = "Wrong credentials!\nCan't find such user";
                        break;
                    case AuthorisationResult.WrongPassword:
                        errorMessage = "Wrong credentials!\nWrong password";
                        break;
                }

                if (authResult != AuthorisationResult.Ok)
                {
                    Disconnect();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                errorMessage = e.ToString();
                Disconnect();
                return false;
            }

            return result;
        }

        public void Synchronize()
        {
            if (sslStream == null || !sslStream.CanRead || !tcpClient.Connected)
            {
                string errorMessage;
                if (!Connect(out errorMessage))
                {
                    return;
                }
            }

            // get last statistics from the server
            BinaryWriter writer = new BinaryWriter(sslStream);
            writer.Write((byte)NTProtocolHeader.RequestStatistics);
            writer.Flush();

            BinaryFormatter formatter = new BinaryFormatter();
            var stats = (StatisticsPacket)formatter.Deserialize(sslStream);
            model.PublishSynchonizationEvent($"Received stats: {stats}");
        }

        public void Disconnect()
        {
            sslStream.Close();
            tcpClient.Close();
        }
    }
}
