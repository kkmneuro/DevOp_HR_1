using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

using ddfplus;
using ddfplus.Historical.Client;
using ddfplus.Net;

namespace MarketData
{


    /// <summary>
    /// Generates Fake Forex data
    /// </summary>
    public class MarketDataDDF : IMarketData
    {

        private TimeFrame.TF _TimeFrame;
        private Future _future;
        private DateTime _from;

        public event PriceTickEventHandler OnPriceTick;

        private decimal _lastPrice;

        //   private PriceGeneratorHelper pgh;

        private Request request;
        Client _client = null;
        Status _status = Status.Disconnected;


        /// <summary>
        /// Price candles in specific time frame
        /// </summary>
        PriceList price = null;

        public MarketDataDDF()
        {

            request =  new MinuteRequest();
            request.Username = "kkmedanic";
            request.Password = "barchart";
            request.Server = "ds01.ddfplus.com/historical/";  // historical server
     /**/  // request.Symbol = "^EURUSD";
     /**/   ((MinuteRequest)request).Interval = 1; // aka time frame in minutes
            ((MinuteRequest)request).FormT = false; // ??
            request.Timeout = 30;
            request.DownloadFormat = DownloadFormat.Csv;
            request.MaxRecords = 0;
            request.SortOrder = ddfplus.Historical.Client.SortOrder.None;
            request.Properties["normalizetimestamps"] = true;




            Connection.Username = "kkmedanic";
            Connection.Password = "barchart";
            Connection.Mode = ConnectionMode.TCPClient;
            Connection.Server = "qs-us-e-01.aws.barchart.com";  // streaming server
            Connection.Port = 0;

            //Connection.Properties["traceerrors"] = ??;
            //Connection.Properties["tracewarnings"] = ??;
            //Connection.Properties["traceinfo"] = ??;
            //Connection.Properties["messagetracefilter"] = ??;

            // The streaming version must be set prior to creating any clients expected to work for that version
            Connection.Properties["streamingversion"] = "3";

            Connection.StatusChange += new Connection.StatusChangeEventHandler(Connection_StatusChange);

            _client = new Client();

            

            _client.NewQuote += new Client.NewQuoteEventHandler(_client_NewQuote);
            _client.NewBookQuote += new Client.NewBookQuoteEventHandler(_client_NewBookQuote);
            _client.NewTimestamp += new Client.NewTimestampEventHandler(_client_NewTimestamp);
            _client.NewOHLCQuote += new Client.NewOHLCQuoteEventHandler(_client_NewOHLCQuote);
            _client.NewMessage += new Client.NewMessageEventHandler(_client_NewMessage);

        //    _client.Symbols = "^EURUSD, ^AUDUSD, ^XAUUSD";


        }

        private void Connection_StatusChange(object sender, ddfplus.Net.StatusChangeEventArgs e)
        {
             _status = e.NewStatus;// throw new NotImplementedException();
        }

        public void Close_Connection()
        {
            _client.Symbols = "";
            Connection.Close();
        } 


        #region ddf Client Event Handlers

        void _client_NewMessage(object sender, Client.NewMessageEventArgs e)
        {
            string msg = Encoding.UTF8.GetString(e.Message);
        }

        void _client_NewOHLCQuote(object sender, Client.NewOHLCQuoteEventArgs e)
        {
            OHLCQuote ohlcq = e.OHLCQuote;
        }

        void _client_NewTimestamp(object sender, Client.NewTimestampEventArgs e)
        {
            // 
        }

        void _client_NewBookQuote(object sender, Client.NewBookQuoteEventArgs e)
        {
            BookQuote bq = e.BookQuote;

          
        }

        void _client_NewQuote(object sender, Client.NewQuoteEventArgs e)
        {
            Quote q = e.Quote;
            string symbol = getStringForFutureOfDDF(q.Symbol);
            if (_future != null)
            if ((OnPriceTick != null)&&(_future.Name == symbol))  
            {

                   _lastPrice = (decimal)q.Ask;

                // we are raising event
                 var eventArg = new PriceTickEventArgs()
                 {
                     Price = _lastPrice,
                     PriceAsk = (decimal)q.Ask,
                     PriceBid = (decimal)q.Bid,
                     PriceTime = q.Timestamp,
                     Symbol = symbol,
                     TimeFrame = _TimeFrame
                 };
                 OnPriceTick(this, eventArg);
            }
        }

        #endregion ddf Client Event Handlers



        public List<Future> getAvailableFutures()
        {
            List<Future> ftr = new List<Future> {
                new Future("EUR/USD"),
                new Future("AUD/USD"),
                new Future("GBP/USD"),
                new Future("USD/CHF"),
                new Future("USD/JPY"),
                new Future("XAU/USD")
            };


            return ftr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="future"></param>
        /// <param name="frame"></param>
        /// <param name="from"></param>
        /// <returns></returns>
        public PriceList getFutureData(Future future, TimeFrame.TF frame, DateTime from)
        {
            request.Symbol = getStringOfFutureForDDF(future);
            _future = future;
            _client.Symbols = request.Symbol;
            ((MinuteRequest)request).Interval = (short)TimeFrame.getMinutesInFrame(frame);
            request.Start = from;

            List<IRecord> records = request.GetResponse();



            price = new PriceList(frame);
            price.f = future;

            foreach (IRecord rec in records)
            {

                IMinute minute = (IMinute)rec;
                //   string sym = minute is IIntradayRecord ? ((IIntradayRecord)minute).Symbol : request.Symbol;
                price.p.Add(
                    new PriceCandle()
                    {
                        Date = minute.Timestamp,
                        Open = (decimal)minute.Open,
                        High = (decimal)minute.High,
                        Low = (decimal)minute.Low,
                        Close = (decimal)minute.Close,
                        Volume = minute.Volume
                    });
            }
            return price;
        }


  /*      private void OnNewMinute(object sender, NewRecordEventArgs e)
        {
          //  _totalRecords += e.Records != null ? e.Records.Count : 0;
            string symbol = sender is Request ? ((Request)sender).Symbol : "";
           // AddMinutesToGrid(e.Records, symbol);
        }  */


        private string getStringOfFutureForDDF(Future future)
        {
            switch (future.Name)
            {
                case "EUR/USD":  return "^EURUSD";
                case "AUD/USD" : return "^AUDUSD";
                case "GBP/USD" : return "^GBPUSD";
                case "USD/CHF" : return "^USDCHF";
                case "USD/JPY" : return "^USDJPY";
                case "XAU/USD" : return "^XAUUSD";
                default: return "^EURUSD";
            }
        }

        private string getStringForFutureOfDDF(string future)
        {
            switch (future)
            {
                case "^EURUSD": return "EUR/USD";
                case "^AUDUSD": return "AUD/USD";
                case "^GBPUSD": return "GBP/USD";
                case "^USDCHF": return "USD/CHF";
                case "^USDJPY": return "USD/JPY";
                case "^XAUUSD": return "XAU/USD";
                default: return "EURUSD";
            }
        }

    }
}
