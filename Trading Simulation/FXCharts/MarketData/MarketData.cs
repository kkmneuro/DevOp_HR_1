using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;



namespace MarketData
{


    /// <summary>
    /// Generates Fake Forex data
    /// </summary>
    public class MarketData : IMarketData
    {

        private TimeFrame.TF _TimeFrame;
        private Future _future;
        private DateTime _from;

        public event PriceTickEventHandler OnPriceTick;
        private Timer ticker = null;


        private decimal _lastPrice;

        private PriceGeneratorHelper pgh;

        /// <summary>
        /// Price candles in specific time frame
        /// </summary>
        PriceList price = null;

        public MarketData()
        {
            pgh = new PriceGeneratorHelper();
            ticker = new Timer(500);
            ticker.Elapsed += Tick;
            ticker.Start();


        }



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

        public PriceList getFutureData(Future future, TimeFrame.TF frame, DateTime from)
        {
            if ((
                (price == null)) ||
                (future != _future)||
                (frame != _TimeFrame)||
                (from != _from))
                price = getRandomFutureData(future, frame, from);
            _future = future;
            _TimeFrame = frame;
            _from = from;
            return price;
        }

        /// <summary>
        /// return initil price
        /// </summary>
        /// <param name="future"></param>
        /// <returns></returns>
        private decimal starPrice(Future future)
        {
            switch (future.Name)
            {
                case "EUR/USD": return 1.12508M;
                case "AUD/USD": return 0.77M;
                case "GBP/USD": return 1.34145M;
                case "USD/CHF": return 0.969130293M;
                case "USD/JPY": return 101.461039M;
                case "XAU/USD": return 1351.76M;
            }
            return 0M;
        }


        /// <summary>
        /// Generate candle price data for chart
        /// </summary>
        /// <param name="future"></param>
        /// <param name="frame"></param>
        /// <param name="from"></param>
        /// <returns></returns>
        private PriceList getRandomFutureData(Future future, TimeFrame.TF frame, DateTime from)
        {



            _TimeFrame = frame;

            decimal startP = starPrice(future); // initial price
            decimal closeP = startP;           
            TimeSpan timeStep = TimeFrame.getTimeFrameSpan(frame); // time for one candle stick
            DateTime goToNow = from;

            PriceList priceList = new PriceList(_TimeFrame); // generated price data

            while (goToNow < DateTime.Now) //
            {
                
                PriceCandle p = pgh.getNextPriceCandle(closeP, frame);

                closeP = p.Close;

                goToNow = goToNow.Add(timeStep); // lets go into next candle 

                if (goToNow < DateTime.Now)
                {
                    p.Date = goToNow;
                    priceList.p.Add(p);
                }
            }
            if (priceList.p.Count != 0) // If we were not generating history data just from currnet momment
                _lastPrice = priceList.p[priceList.p.Count -1].Close; // 
            else if (priceList.p.Count == 0) priceList.p.Add(new PriceCandle  // lets add at least one candle for begining  ir not we will have out of range exception
            {
                Date = DateTime.Now,
                Close = startP,
                High = startP,
                Low = startP,
                Open = startP,
            });
            return priceList;
        }


        /// <summary>
        ///  Informing about a a new price in a form of event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tick(object sender, ElapsedEventArgs e)
        {
            if (OnPriceTick != null)
            {

                _lastPrice = pgh.GetNextRandomPrice(_lastPrice);

                // we are raising event
                var eventArg = new PriceTickEventArgs() { Price = _lastPrice, PriceTime = System.DateTime.Now, TimeFrame = _TimeFrame };
                OnPriceTick(this, eventArg);
            }
        }


    }
}
