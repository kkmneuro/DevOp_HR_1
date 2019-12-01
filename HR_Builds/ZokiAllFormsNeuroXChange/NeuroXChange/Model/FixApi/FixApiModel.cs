using NeuroXChange.Common;
using NeuroXChange.Model.BioData;
using NeuroXChange.Model.Database;
using NeuroXChange.Model;
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
using System.Web.Script.Serialization;
using System.Windows.Forms;
using NeuroXChange.Data.DS;
using System.Data;

namespace NeuroXChange.Model.FixApi
{
   

    public class FixApiModel: AbstractFixApiModel
    {
        public FixApiModelState State { get; private set; }
        public override event Action<string> RefreshClient = delegate { };
        public override event Action<string[]> RefreshSymbols = delegate { };
        public override event Action<DataTable> RefreshQuotes = delegate { };

        /*private int pricePort;
        private int tradePort;
        private string host;
        private string username;
        private string password;
        private string senderCompID;
        private string senderSubID;
        private string targetCompID;*/
        private MessageConstructor messageConstructor;
        private JavaScriptSerializer serializer;
        List<Symbol> lst;
        private readonly DS4MarketWatch marketDS = new DS4MarketWatch();

        int messageSequenceNumber = 1;

        private TcpClient priceClient;
        private static SslStream priceStreamSSL;

        private volatile bool NeedStop = false;
        //private Thread threadRequests;
        private Thread threadReader;
        private Thread threadWriter;
        private Thread threadConnectionChecker;
        private WsHelperOrders _wsHelperOrders;
        private WsHelperQuotes _wsHelperQuotes;

        private TickPrice priceDataBottom = null;

        private const int _MAM_MAX_REQUEST_QTY = 10;
        const int SUBSCRIBE = 28;
        StringBuilder logmsgsnapshot = new StringBuilder();
        StringBuilder logmsgquote = new StringBuilder();




        public FixApiModel(LocalDatabaseConnector localDatabaseConnector,
            IniFileReader iniFileReader, WsHelperOrders wshelperOrders, WsHelperQuotes wshelperQuotes)
            :base(localDatabaseConnector)
        {
            serializer = new JavaScriptSerializer();
            this._wsHelperOrders = wshelperOrders;
            this._wsHelperQuotes = wshelperQuotes;
            //this.threadRequests = new Thread(SendRequests);
            lst = new List<Symbol>();

            State = FixApiModelState.Disconnected;

            //pricePort = Int32.Parse(iniFileReader.Read("pricePort", "FixApi"));
            //tradePort = Int32.Parse(iniFileReader.Read("tradePort", "FixApi"));
            //host = iniFileReader.Read("Host", "FixApi");
            //username = iniFileReader.Read("Username", "FixApi");
            //password = iniFileReader.Read("Password", "FixApi");
            //senderCompID = iniFileReader.Read("SenderCompID", "FixApi");
            //senderSubID = iniFileReader.Read("SenderSubID", "FixApi");
            //targetCompID = iniFileReader.Read("TargetCompID", "FixApi");
            logmsgsnapshot = new StringBuilder();
            //messageConstructor = new MessageConstructor(
            //    host, username, password, senderCompID, senderSubID, targetCompID);

            threadReader = new Thread(GenerateNewData);
            threadWriter = new Thread(SendRequests);
            threadConnectionChecker = new Thread(CheckConnection);

            TryConnect();

        }

   

        private void CheckConnection()
        {
            while (!NeedStop)
            {
                Thread.Sleep(5000);

                if (State == FixApiModelState.Disconnected)
                {
                    TryConnect();
                }
            }
        }

        private void TryConnect()
        {
            //if (State != FixApiModelState.Disconnected)
            //{
            //    return;
            //}

            //State = FixApiModelState.Connecting;

            //try
            //{
            //    priceClient = new TcpClient(host, pricePort);
            //    priceStreamSSL = new SslStream(priceClient.GetStream(), false,
            //                new RemoteCertificateValidationCallback(ValidateServerCertificate), null);
            //    priceStreamSSL.ReadTimeout = 1000;
            //}
            //catch
            //{
            //    State = FixApiModelState.Disconnected;
            //    return;
            //}

            //// if it can't authenticate, it will throw an exception wich will end the application
            //priceStreamSSL.AuthenticateAsClient(host);
            //messageSequenceNumber = 1;

            //State = FixApiModelState.Connected;
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
            while (!NeedStop)
            {
                if (priceStreamSSL == null)
                {
                    Thread.Sleep(1000);
                    continue;
                }

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
                    }
                    catch
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
            foreach (var kv in list)
            {
                if (kv.StartsWith("270="))
                {
                    prices.Add(kv.Substring(4));
                }
            }
            if (prices.Count == 2)
            {
                var tickPrice = new TickPrice(prices[0], prices[1], DateTime.Now);
                localDatabaseConnector.WriteTickPrice(tickPrice);
                NotifyObservers(FixApiModelEvent.PriceChanged, tickPrice);
                priceDataBottom = tickPrice;
            }
        }

        private void SendRequests()
        {
            ////while (!NeedStop)
            ////{   
            //    QuotesDataRequestMessage(SubscribeRequestType.SUBSCRIBE, lst);
            //    //System.Threading.Thread.Sleep(100);

            ////}

            QuotesDataRequestMessage(SubscribeRequestType.SUBSCRIBE, lst);

            while (!NeedStop)
            {
                try
                {
                    Thread.Sleep(200);
                    var message = messageConstructor.LogonMessage(MessageConstructor.SessionQualifier.QUOTE, messageSequenceNumber, 30, false);
                    SendMessage(message);

                    //Thread.Sleep(200);
                    //message = messageConstructor.MarketDataRequestMessage(MessageConstructor.SessionQualifier.QUOTE, messageSequenceNumber, "EURUSD:WDqsoT", 1, 1, 0, 1, 1);
                    //SendMessage(message);

                    while (!NeedStop)
                    {
                        Thread.Sleep(100);
                        message = messageConstructor.HeartbeatMessage(MessageConstructor.SessionQualifier.QUOTE, messageSequenceNumber);
                        SendMessage(message);
                    }
                }
                catch
                {
                    State = FixApiModelState.Disconnected;
                    Thread.Sleep(1000);
                }
            }

           

        }

        private string SendMessage(string message)
        {
            var byteArray = Encoding.ASCII.GetBytes(message);
            priceStreamSSL.Write(byteArray, 0, byteArray.Length);
            messageSequenceNumber++;
            return "";
        }



        public void QuotesDataRequestMessage(Enum ReqType, List<Symbol> lst)
        {
            var json = string.Empty;

            try
            {
                if (lst.Count > 0)
                {
                    List<List<Symbol>> _lstSymbols = new List<List<Symbol>>();
                    Globals.SymbolTable.Rows.Clear();
                    //  _lstSymbols.Add(lst);
                    //while (lst.Any())
                    //{
                    //    _lstSymbols.Add(lst.Take(_MAM_MAX_REQUEST_QTY).ToList());
                    //    lst = lst.Skip(_MAM_MAX_REQUEST_QTY).ToList();
                        _lstSymbols.Add(lst);
                    //}
                    foreach (var itemTemp in _lstSymbols)
                    {

                        foreach (Symbol item in itemTemp)
                        {
                            DataRow dr = Globals.SymbolTable.NewRow();
                            dr["CONTRACT"] = item.Contract;
                            dr["GATEWAY"] = item.Gateway;
                            dr["PRODUCT"] = item.Product;
                            dr["PRODUCTTYPE"] = '1';
                            Globals.SymbolTable.Rows.Add(dr);

                        }
                        //SymbolSubscribeRequest SubscribeRequest = new SymbolSubscribeRequest();
                        //SubscribeRequest.Symbol = new List<SymbolInfo>();

                        //foreach (Symbol item in itemTemp)
                        //{
                        //    SymbolInfo objSymbol = new SymbolInfo();
                        //    objSymbol.Contract = item.Contract;
                        //    objSymbol.Gateway = item.Gateway;
                        //    objSymbol.Product = item.Product;
                        //    objSymbol.ProductType = '1';//item.ProductType;
                        //    SubscribeRequest.Symbol.Add(objSymbol);
                        //}
                        //SubscribeRequest.NoOfSymbols = SubscribeRequest.Symbol.Count;
                        //SubscribeRequest.isForSubscribe = (SubscribeRequestType)ReqType;
                        //SubscribeRequest.msgtype = SUBSCRIBE;
                        //json = new JavaScriptSerializer().Serialize(SubscribeRequest);

                        //this._wsHelperQuotes.send(json);

                    }

                    string[] symbolArray = (from myRow in Globals.SymbolTable.AsEnumerable() select myRow.Field<string>("CONTRACT")).ToArray();
                    this.RefreshSymbols(symbolArray);      
                  }
            }
            catch (Exception ex)
            {
                throw ex;
            }

           // return json;

        }


        public override void SubscribeForQuotes(Enum ReqType, List<string> lst)
        {
            var json = string.Empty;          

            try
            {
                if (lst.Count > 0)
                {
                    SymbolSubscribeRequest SubscribeRequest = new SymbolSubscribeRequest();
                    SubscribeRequest.Symbol = new List<SymbolInfo>();
                    
                    foreach(string item in lst)
                    {
                        DataRow dr = Globals.SymbolTable.Rows.Find(item);
                        SymbolInfo objSymbol = new SymbolInfo();
                        objSymbol.Contract = dr["CONTRACT"].ToString();
                        objSymbol.Gateway = dr["GATEWAY"].ToString();
                        objSymbol.Product = dr["PRODUCT"].ToString();
                        objSymbol.ProductType = '1';//item.ProductType;
                        SubscribeRequest.Symbol.Add(objSymbol);
                    }
                    SubscribeRequest.NoOfSymbols = SubscribeRequest.Symbol.Count;
                    SubscribeRequest.isForSubscribe = (SubscribeRequestType)ReqType;
                    SubscribeRequest.msgtype = SUBSCRIBE;
                    json = new JavaScriptSerializer().Serialize(SubscribeRequest);

                    this._wsHelperQuotes.send(json);
                }
                                  
               }
            catch (Exception ex)
            {
                throw ex;
            }
            // return json;
        }

        public override void SubscribeForQuotes(Enum ReqType, string contract)
        {

            DataRow dr = Globals.SymbolTable.Rows.Find(contract);
            SymbolSubscribeRequest SubscribeRequest = new SymbolSubscribeRequest();
            SubscribeRequest.Symbol = new List<SymbolInfo>();

           
                SymbolInfo objSymbol = new SymbolInfo();
                objSymbol.Contract = dr["CONTRACT"].ToString();
                objSymbol.Gateway = dr["GATEWAY"].ToString();
                objSymbol.Product = dr["PRODUCT"].ToString();
                objSymbol.ProductType = '1';//item.ProductType;
                SubscribeRequest.Symbol.Add(objSymbol);
           
            SubscribeRequest.NoOfSymbols = SubscribeRequest.Symbol.Count;
            SubscribeRequest.isForSubscribe = (SubscribeRequestType)ReqType;
            SubscribeRequest.msgtype = SUBSCRIBE;
            var json = new JavaScriptSerializer().Serialize(SubscribeRequest);
            this._wsHelperQuotes.send(json);

        }



        public override void StartProcessing()
        {
           
            this._wsHelperOrders.OnParticipantResponse += _wsHelperOrders_OnParticipantResponse;
            this._wsHelperQuotes.OnQuoteResponse += _wsHelperQuotes_OnQuoteResponse;
            this._wsHelperQuotes.OnQuoteSnapshotResponse += _wsHelperQuotes_OnQuoteSnapshotResponse;
            this._wsHelperOrders.participantRequest();
            threadReader.Start();
            threadConnectionChecker.Start();
            
        }

       

        private void _wsHelperQuotes_OnQuoteSnapshotResponse(SnapShot _QuoteSnapshot)
        {
            //throw new NotImplementedException();
            logmsgsnapshot = new StringBuilder();
            logmsgsnapshot.AppendLine("------------ Quotes snapshot Response ------------------");
            
            foreach (var item in _QuoteSnapshot.OHLC)
            {
                logmsgsnapshot.AppendLine("Contact::" + item.Contract + "||Product:: " + item.Product + "||Product Type:: " + item.ProductType);

                if (wsHelperContract.INSTANCE.ddContractDetails.ContainsKey(item.Contract))
                {
                    DataRow dr = Globals.TradeTable.Rows.Find(item.Contract);
                    InstrumentSpec spec = wsHelperContract.INSTANCE.ddContractDetails[item.Contract];

                    int vol = (int)item.Volume;
                    if (spec != null && spec.ContractSize > 0)
                    {
                        vol = (int)(item.Volume / spec.ContractSize);
                    }
                    if (dr != null)
                    {
                        dr["VOL"] = vol.ToString();
                        dr.AcceptChanges();
                        Globals.TradeTable.AcceptChanges();
                    }

                    logmsgsnapshot.AppendLine("Vol::" +vol.ToString());
                }
            }
            System.Diagnostics.Trace.WriteLine(logmsgsnapshot.ToString());         
            this.RefreshClient(logmsgsnapshot.ToString());
           
        }
       
        private void _wsHelperQuotes_OnQuoteResponse(QuotesStream _QuotesStream)
        {
            //throw new NotImplementedException();
            logmsgquote = new StringBuilder();
            logmsgquote.AppendLine("--------------------Quote Response-------------------");           

            foreach (var quotes in _QuotesStream.QuotesItem)
            {
                try
                {
                    logmsgquote.Append("Contact::" + quotes.Contract + "||Product:: " + quotes.Product+ "||Product Type:: " + quotes.ProductType);
                    DataRow dr = Globals.TradeTable.Rows.Find(quotes.Contract);

                    InstrumentSpec spec = wsHelperContract.INSTANCE.ddContractDetails[quotes.Contract];
                    long _size = quotes.Size;
                    if (spec != null && spec.ContractSize > 0)
                    {
                        _size = (long)(quotes.Size / spec.ContractSize);
                    }
                    double _Price = quotes.Price / Math.Pow(10, spec.Digits);
                    switch (quotes.QuotesStreamType)
                    {
                        case "C":
                            {
                                //marketDS.dtMarketWatch.Rows[index]["ClosePrice"] = Convert.ToDecimal(_Price).ToString("0.00");
                                //marketDS.dtMarketWatch.Rows[index]["ClosePrice"] = Convert.ToDecimal(_Price).ToString();
                                logmsgquote.Append("/ClosedPrice::" + Convert.ToDecimal(_Price).ToString());
                                if (dr != null)
                                {
                                    dr["CLOSE"] = Convert.ToDecimal(_Price).ToString();
                                    dr.AcceptChanges();
                                }
                            }
                            break;
                        case "O":
                            {
                                //marketDS.dtMarketWatch.Rows[index]["OpenPrice"] = Convert.ToDecimal(_Price).ToString();
                                logmsgquote.Append("/OpenPrice::" + Convert.ToDecimal(_Price).ToString());
                                if (dr != null)
                                {
                                    dr["OPEN"] = Convert.ToDecimal(_Price).ToString();
                                    dr.AcceptChanges();
                                }
                            }
                            break;
                        case "A":
                            {
                                logmsgquote.Append("/SellPrice::" + Convert.ToDecimal(_Price).ToString());
                                if (dr != null)
                                {
                                    dr["SELL"] = Convert.ToDecimal(_Price).ToString();
                                    dr.AcceptChanges();
                                }
                            }
                            break;
                        case "B":
                            {
                                logmsgquote.Append("/BuyPrice::" + Convert.ToDecimal(_Price).ToString());
                                if (dr != null)
                                {
                                    dr["BUY"] = Convert.ToDecimal(_Price).ToString();
                                    dr.AcceptChanges();
                                }
                            }
                            break;
                        case "H":
                            {
                                //marketDS.dtMarketWatch.Rows[index]["High"] = Convert.ToDecimal(_Price).ToString();
                                logmsgquote.Append("/High::" + Convert.ToDecimal(_Price).ToString());
                                if (dr != null)
                                {
                                    dr["HIGH"] = Convert.ToDecimal(_Price).ToString();
                                    dr.AcceptChanges();
                                }
                            }
                            break;
                        case "L":
                            {
                                logmsgquote.Append("/LTP::" + Convert.ToDecimal(_Price).ToString());
                                if (dr != null)
                                {
                                    dr["LTP"] = Convert.ToDecimal(_Price).ToString();
                                    dr.AcceptChanges();
                                }
                            }
                            break;
                        case "M":
                            {
                                logmsgquote.Append("/Low::" + Convert.ToDecimal(_Price).ToString());
                                if (dr != null)
                                {
                                    dr["LOW"] = Convert.ToDecimal(_Price).ToString();
                                    dr.AcceptChanges();
                                }
                            }
                            break;
                        case "V":
                            {
                                logmsgquote.Append("/SIZE::" + Convert.ToDecimal(_size).ToString());
                                if (dr != null)
                                {
                                    dr["SIZE"] = Convert.ToDecimal(_Price).ToString();
                                    dr.AcceptChanges();
                                }
                            }
                            break;
                        case "P":
                            {
                            }
                            break;
                        default:

                            break;
                    }

                    var tickPrice = new TickPrice(dr["BUY"].ToString(), dr["SELL"].ToString(), DateTime.Now);
                    localDatabaseConnector.WriteTickPrice(tickPrice);
                    NotifyObservers(FixApiModelEvent.PriceChanged, tickPrice);
                    priceDataBottom = tickPrice;

                }
                catch (Exception)
                {

                }

                Globals.TradeTable.AcceptChanges();
                System.Diagnostics.Trace.WriteLine(logmsgquote.ToString());
                this.RefreshClient(logmsgquote.ToString());
                this.RefreshQuotes(Globals.TradeTable);

            }
        }

        private void _wsHelperOrders_OnParticipantResponse(string message)
        {
            ParticipantCollection collection = serializer.Deserialize<ParticipantCollection>(message);
            if (collection.accountInfo != null)
            {
                Globals.AccountId = collection.accountInfo[0].AccountID;
                System.Diagnostics.Trace.Write("OnParticipantResponse ::: " + collection.accountInfo[0].AccountID);

                //write to log
                NeuroXChange.Model.Globals.LoggerClient.WriteLog(Globals.AccountId.ToString(), "Login Successful", DateTime.Now);

                wsHelperContract.INSTANCE.LoadIntialData(collection.accountInfo[0].Group);
               
               
                foreach (InstrumentSpec item in wsHelperContract.INSTANCE.ddContractDetails.Values)
                {
                   //System.Diagnostics.Trace.Write("OnParticipantResponse WS Service ::: " + item.InstruementID);
                    var sym = Symbol.GetSymbol(Symbol.getKey(item)[0]);
                    lst.Add(sym);
                }              

            }
            
            if (lst.Count>0)
                this.threadWriter.Start();
        }

        public override void StopProcessing()
        {
            NeedStop = true;
            threadWriter.Abort();
            threadConnectionChecker.Abort();
            threadReader.Abort();
        }

        public override void OnNext(BioDataEvent bioDataEvent, object data)
        {
            if (bioDataEvent != BioDataEvent.NewBioDataTick ||
                priceDataBottom == null)
            {
                return;
            }

            var biodata = (BioData.BioData)data;

            // current biodata tick was not saved
            if (biodata.id < 1)
            {
                return;
            }

            localDatabaseConnector.WritePriceAtBioDataTick(priceDataBottom, biodata.id);
        }

       
       
    }
}
