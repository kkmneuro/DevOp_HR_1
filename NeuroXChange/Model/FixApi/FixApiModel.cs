using NeuroXChange.Common;
using NeuroXChange.Model.BioData;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NeuroXChange.Model.FixApi
{
    public class FixApiModel: AbstractFixApiModel
    {
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

        // database
        private bool saveTickPrice = false;
        private bool savePriceAtBioDataTick = false;
        private string databaseLocation;
        private string tickPriceTableName;
        private string instrumentTableName;
        private string priceAtBioDataTickTableName;
        private OleDbConnection conn = null;

        struct PriceEntry
        {
            public DateTime timestamp;
            public string sellPrice;
            public string buyPrice;
        }
        private PriceEntry? priceDataBottom = null;
        private Queue<PriceEntry> priceData = null;

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

            saveTickPrice = Boolean.Parse(iniFileReader.Read("SaveTickPrice", "Database"));
            savePriceAtBioDataTick = Boolean.Parse(iniFileReader.Read("SavePriceAtBioDataTick", "Database"));
            if (saveTickPrice || savePriceAtBioDataTick)
            {
                databaseLocation = iniFileReader.Read("Location", "Database");
                tickPriceTableName = iniFileReader.Read("TickPriceTable", "Database");
                instrumentTableName = iniFileReader.Read("InstrumentTable", "Database");
                priceAtBioDataTickTableName = iniFileReader.Read("PriceAtBioDataTickTable", "Database");

                if (File.Exists(databaseLocation))
                {
                    // try to create tables, skip if tables exists
                    conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + this.databaseLocation);
                    conn.Open();
                    try
                    {
                        var commandStr = string.Format(
                        "CREATE TABLE {0} ([ID] AUTOINCREMENT NOT NULL PRIMARY KEY, [Title] VARCHAR(40) NOT NULL);",
                        instrumentTableName);
                        var cmd = new OleDbCommand(commandStr, conn);
                        cmd.ExecuteNonQuery();

                        commandStr = string.Format(
                            "INSERT INTO {0} ([ID], [Title]) values(1, 'EURUSD');",
                            instrumentTableName);
                        cmd = new OleDbCommand(commandStr, conn);
                        cmd.ExecuteNonQuery();
                    } catch { }
                }
                else
                {
                    saveTickPrice = false;
                    savePriceAtBioDataTick = false;
                }
            }
            if (saveTickPrice)
            {
                try
                {
                    var commandStr = string.Format(
                    "CREATE TABLE {0} ([ID] AUTOINCREMENT NOT NULL PRIMARY KEY, [Instrument_ID] NUMBER NOT NULL, [Time] DATETIME NOT NULL, [SellPrice] DOUBLE NOT NULL, [BuyPrice] DOUBLE NOT NULL);",
                    tickPriceTableName);
                    var cmd = new OleDbCommand(commandStr, conn);
                    cmd.ExecuteNonQuery();
                }
                catch { }
            }
            if (savePriceAtBioDataTick)
            {
                try
                {
                    var commandStr = string.Format(
                    "CREATE TABLE {0} ([ID] NUMBER NOT NULL PRIMARY KEY, [Instrument_ID] NUMBER NOT NULL, [SellPrice] DOUBLE NOT NULL, [BuyPrice] DOUBLE NOT NULL);",
                    priceAtBioDataTickTableName);
                    var cmd = new OleDbCommand(commandStr, conn);
                    cmd.ExecuteNonQuery();
                }
                catch { }
                priceData = new Queue<PriceEntry>();
            }


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
                NotifyObservers(FixApiModelEvent.PriceChanged, new string[2] {prices[0], prices[1] });
                if (saveTickPrice)
                {
                    var commandStr = string.Format(
"INSERT INTO {0} ([Instrument_ID], [Time], [SellPrice], [BuyPrice]) values({1}, '{2}', {3}, {4});",
tickPriceTableName, 1, DateTime.Now, prices[0], prices[1]);
                    var cmd = new OleDbCommand(commandStr, conn);
                    cmd.ExecuteNonQueryAsync();
                }
                if (savePriceAtBioDataTick)
                {
                    priceData.Enqueue(new PriceEntry { timestamp = DateTime.Now, sellPrice = prices[0], buyPrice = prices[1] });
                    if (priceData.Count > 1000)
                    {
                        priceDataBottom = priceData.Dequeue();
                    }
                }
            }
        }

        private void SendRequests()
        {
            Thread.Sleep(200);
            var message = messageConstructor.LogonMessage(MessageConstructor.SessionQualifier.QUOTE, messageSequenceNumber, 30, false);
            SendMessage(message);

            Thread.Sleep(200);
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

        public override void StopProcessing()
        {
            NeedStop = true;
            if (conn != null)
            {
                conn.Close();
            }
        }

        public override void OnNext(BioData.BioData data)
        {
            if (!savePriceAtBioDataTick)
            {
                return;
            }

            if (!priceDataBottom.HasValue && priceData.Count == 0)
            {
                return;
            }
            if (!priceDataBottom.HasValue)
            {
                priceDataBottom = priceData.Dequeue();
            }

            while (priceData.Count > 0 && priceData.Peek().timestamp < data.time)
            {
                priceDataBottom = priceData.Dequeue();
            }

            DateTime selectedDateTime = priceDataBottom.Value.timestamp;
            if (priceData.Count == 0 && data.time - selectedDateTime > TimeSpan.FromMinutes(1))
            {
                priceDataBottom = null;
                return;
            }

            if (priceDataBottom.HasValue && priceDataBottom.Value.timestamp < data.time)
            {
                var commandStr = string.Format(
                    "INSERT INTO {0} ([ID], [Instrument_ID], [SellPrice], [BuyPrice]) values({1}, {2}, {3}, {4});",
                    priceAtBioDataTickTableName, data.psychophysiological_Session_Data_ID, 1, priceDataBottom.Value.sellPrice, priceDataBottom.Value.buyPrice);
                var cmd = new OleDbCommand(commandStr, conn);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
