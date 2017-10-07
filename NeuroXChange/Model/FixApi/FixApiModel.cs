﻿using NeuroXChange.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NeuroXChange.Model.FixApi
{
    public class FixApiModel
    {
        private List<IFixApiObserver> observers = new List<IFixApiObserver>();

        private int pricePort;
        private int tradePort;
        private string host;
        private string username;
        private string password;
        private string senderCompID;
        private string senderSubID;
        private string targetCompID;
        private MessageConstructor messageConstructor;

        int messageSequenceNumber = 1;

        private TcpClient priceClient;
        private static SslStream priceStreamSSL;

        private volatile bool NeedStop = false;
        private Thread threadReader;
        private Thread threadWriter;

        public FixApiModel(IniFileReader iniFileReader)
        {
            pricePort = Int32.Parse(iniFileReader.Read("pricePort", "FixApi"));
            tradePort = Int32.Parse(iniFileReader.Read("tradePort", "FixApi"));
            host = iniFileReader.Read("Host", "FixApi");
            username = iniFileReader.Read("Username", "FixApi");
            password = iniFileReader.Read("Password", "FixApi");
            senderCompID = iniFileReader.Read("SenderCompID", "FixApi");
            senderSubID = iniFileReader.Read("SenderSubID", "FixApi");
            targetCompID = iniFileReader.Read("TargetCompID", "FixApi");

            messageConstructor = new MessageConstructor(
                host, username, password, senderCompID, senderSubID, targetCompID);

            priceClient = new TcpClient(host, pricePort);
            priceStreamSSL = new SslStream(priceClient.GetStream(), false,
                        new RemoteCertificateValidationCallback(ValidateServerCertificate), null);
            priceStreamSSL.AuthenticateAsClient(host);

            threadReader = new Thread(GenerateNewData);
            threadReader.Start();

            threadWriter = new Thread(SendRequests);
            threadWriter.Start();
        }

        private static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;
            throw new Exception("Certificate error: {0}" + sslPolicyErrors.ToString());
        }

        private void GenerateNewData()
        {
            var buffer = new byte[2048];
            priceStreamSSL.ReadTimeout = 1000;
            while (!NeedStop)
            {
                //var res = priceStreamSSL.Read(buffer, 0, 2048);
                if (priceStreamSSL.CanRead)
                {
                    try
                    {
                        var x = priceStreamSSL.Read(buffer, 0, 2048);
                        if (x > 10)
                        {
                            var returnedMessage = Encoding.ASCII.GetString(buffer, 0, x);
                            MessageReceived(returnedMessage);
                        }
                    } catch
                    {
                        Thread.Sleep(200);
                    }
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }

        void MessageReceived(string message)
        {
            NotifyObservers(FixApiModelEvent.RawMessageReceived, message);
            var list = message.Split('\u0001');
            var prices = new List<string>();
            foreach(var kv in list)
            {
                if (kv.StartsWith("270="))
                {
                    prices.Add(kv.Substring(4));
                }
            }
            if (prices.Count == 2)
            {
                NotifyObservers(FixApiModelEvent.PriceChanged, prices);
            }
        }

        private void SendRequests()
        {
            Thread.Sleep(200);
            var message = messageConstructor.LogonMessage(MessageConstructor.SessionQualifier.QUOTE, messageSequenceNumber, 30, false);
            SendMessage(message);

            Thread.Sleep(200);
            //message = messageConstructor.MarketDataRequestMessage(MessageConstructor.SessionQualifier.QUOTE, messageSequenceNumber, "EURUSD:WDqsoT", 1, 0, 0, 1, 1);
            message = messageConstructor.MarketDataRequestMessage(MessageConstructor.SessionQualifier.QUOTE, messageSequenceNumber, "EURUSD:WDqsoT", 1, 1, 0, 1, 1);
            SendMessage(message);

            while (!NeedStop)
            {
                Thread.Sleep(100);
                message = messageConstructor.HeartbeatMessage(MessageConstructor.SessionQualifier.QUOTE, messageSequenceNumber);
                SendMessage(message);
            }
        }

        private string SendMessage(string message)
        {
            var byteArray = Encoding.ASCII.GetBytes(message);
            priceStreamSSL.Write(byteArray, 0, byteArray.Length);
            messageSequenceNumber++;
            return "";
        }

        public void StopProcessing()
        {
            NeedStop = true;
        }

        // ---- Observable pattern implementation
        public void RegisterObserver(IFixApiObserver observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
        }

        public void RemoveObserver(IFixApiObserver observer)
        {
            if (observers.Contains(observer))
                observers.Remove(observer);
        }

        private void NotifyObservers(FixApiModelEvent modelEvent, object data)
        {
            foreach (var observer in observers)
                observer.OnNext(modelEvent, data);
        }
    }
}