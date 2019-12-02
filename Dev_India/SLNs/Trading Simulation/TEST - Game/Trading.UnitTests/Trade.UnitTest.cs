using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TradingAccount;

namespace Tradings.UnitTests
{
    [TestClass]
    public class Trade
    {

    //    Sell sell = new Sell(1, "USD", "EUR/USD", 1000, 1.4M, 1.3M);
      //  Buy  buy  = new Buy(2, "USD", "EUR/USD", 1000, 1.4M, 1.3M);


        [TestMethod]
        public void InitSellEURUSD()
        {
            Sell sell2 = new Sell(1, "USD", 1000, new Price("EUR/USD", 1.4M, 1.3M));


            Assert.AreEqual(1000, sell2.Amount);
            Assert.AreEqual(1.4M, sell2.Ask);
            Assert.AreEqual(1.3M, sell2.Bid);
            Assert.AreEqual(-100M, sell2.GainLoss);
            Assert.AreEqual(0.1M, sell2.Spread);
            Assert.AreEqual(100M, sell2.SpreadValue);
            Assert.AreEqual("EUR/USD", sell2.Symbol);
            Assert.AreEqual(1, sell2.TradeID);
            Assert.AreEqual(TradingAccount.Status.Open, sell2.TradeStatus);
            Assert.AreEqual(1300M, sell2.TradeValue);
        }

        [TestMethod]
        public void InitBuyEURUSD()
        {
            Buy buy2 = new Buy(1, "USD",  1000, new Price("EUR/USD", 1.4M, 1.3M));


            Assert.AreEqual(1000, buy2.Amount);
            Assert.AreEqual(1.4M, buy2.Ask);
            Assert.AreEqual(1.3M, buy2.Bid);
            Assert.AreEqual(-100M, buy2.GainLoss);
            Assert.AreEqual(0.1M, buy2.Spread);
            Assert.AreEqual(100M, buy2.SpreadValue);
            Assert.AreEqual("EUR/USD", buy2.Symbol);
            Assert.AreEqual(1, buy2.TradeID);
            Assert.AreEqual(TradingAccount.Status.Open, buy2.TradeStatus);
            Assert.AreEqual(1300M, buy2.TradeValue);
        }

        [TestMethod]
        public void BuyPriceUpEURUSD()
        {
            Buy buy2 = new Buy(1, "USD", 1000, new Price("EUR/USD", 1.4M, 1.3M));

            buy2.UpdateTradeValue(new Price("EUR/USD", 1.5M, 1.5M));

            Assert.AreEqual(1000, buy2.Amount);
            Assert.AreEqual(1.5M, buy2.Ask);
            Assert.AreEqual(1.3M, buy2.Bid);
            Assert.AreEqual(0M, buy2.GainLoss);
            Assert.AreEqual(0.1M, buy2.Spread);
            Assert.AreEqual(100M, buy2.SpreadValue);
            Assert.AreEqual("EUR/USD", buy2.Symbol);
            Assert.AreEqual(1, buy2.TradeID);
            Assert.AreEqual(TradingAccount.Status.Open, buy2.TradeStatus);
            Assert.AreEqual(1400M, buy2.TradeValue);

            buy2.UpdateTradeValue(new Price("EUR/USD", 1.6M, 1.6M));

            Assert.AreEqual(1000, buy2.Amount);
            Assert.AreEqual(1.6M, buy2.Ask);
            Assert.AreEqual(1.3M, buy2.Bid);
            Assert.AreEqual(100M, buy2.GainLoss);
            Assert.AreEqual(0.1M, buy2.Spread);
            Assert.AreEqual(100M, buy2.SpreadValue);
            Assert.AreEqual("EUR/USD", buy2.Symbol);
            Assert.AreEqual(1, buy2.TradeID);
            Assert.AreEqual(TradingAccount.Status.Open, buy2.TradeStatus);
            Assert.AreEqual(1500M, buy2.TradeValue);

        }

        [TestMethod]
        public void BuyPriceDownEURUSD()
        {
            Buy buy2 = new Buy(1, "USD",  1000, new Price("EUR/USD", 1.4M, 1.4M));

            buy2.UpdateTradeValue(new Price("EUR/USD", 1.3M, 1.3M));

            Assert.AreEqual(1000, buy2.Amount);
            Assert.AreEqual(1.3M, buy2.Ask);
            Assert.AreEqual(1.4M, buy2.Bid);
            Assert.AreEqual(-100M, buy2.GainLoss);
            Assert.AreEqual(0M, buy2.Spread);
            Assert.AreEqual(0M, buy2.SpreadValue);
            Assert.AreEqual("EUR/USD", buy2.Symbol);
            Assert.AreEqual(1, buy2.TradeID);
            Assert.AreEqual(TradingAccount.Status.Open, buy2.TradeStatus);
            Assert.AreEqual(1300M, buy2.TradeValue);
            Assert.AreEqual(-100M, buy2.BaseGainLoss);



            buy2.UpdateTradeValue(new Price("EUR/USD", 1.2M, 1.2M));

            Assert.AreEqual(1000, buy2.Amount);
            Assert.AreEqual(1.2M, buy2.Ask);
            Assert.AreEqual(1.4M, buy2.Bid);
            Assert.AreEqual(-200M, buy2.GainLoss);
            Assert.AreEqual(0M, buy2.Spread);
            Assert.AreEqual(0M, buy2.SpreadValue);
            Assert.AreEqual("EUR/USD", buy2.Symbol);
            Assert.AreEqual(1, buy2.TradeID);
            Assert.AreEqual(TradingAccount.Status.Open, buy2.TradeStatus);
            Assert.AreEqual(1200M, buy2.TradeValue);

            Assert.AreEqual(1200M, buy2.TradeValue);
            Assert.AreEqual(-200M, buy2.BaseGainLoss);

        }

        [TestMethod]
        public void SellPriceUpEURUSD()
        {
            Sell sell2 = new Sell(1, "USD", 10000, new Price("EUR/USD", 0.9517M, 0.9517M));

            sell2.UpdateTradeValue(new Price("EUR/USD", 0.9550M, 0.9550M));

            Assert.AreEqual(10000, sell2.Amount);
            Assert.AreEqual(0.9517M, sell2.Ask);
            Assert.AreEqual(0.9550M, sell2.Bid);
            Assert.AreEqual(-33M, sell2.GainLoss);
            Assert.AreEqual(0.0M, sell2.Spread);
            Assert.AreEqual(0M, sell2.SpreadValue);
            Assert.AreEqual("EUR/USD", sell2.Symbol);
            Assert.AreEqual(1, sell2.TradeID);
            Assert.AreEqual(TradingAccount.Status.Open, sell2.TradeStatus);
            Assert.AreEqual(9550M, sell2.TradeValue);
            Assert.AreEqual(-33M, sell2.BaseGainLoss);
            Assert.AreEqual(9550M, sell2.BaseTradeValue);



            /*            sell2.CalculateTradeValue(1.5M);

                        Assert.AreEqual(1000, sell2.Amount);
                        Assert.AreEqual(1.4M, sell2.Ask);
                        Assert.AreEqual(1.5M, sell2.Bid);
                        Assert.AreEqual(-300M, sell2.GainLoss);
                        Assert.AreEqual(0.1M, sell2.Spread);
                        Assert.AreEqual(100M, sell2.SpreadValue);
                        Assert.AreEqual("EUR/USD", sell2.Symbol);
                        Assert.AreEqual(1, sell2.TradeID);
                        Assert.AreEqual(TradingAccount.Status.Open, sell2.TradeStatus);
                        Assert.AreEqual(1100M, sell2.TradeValue);
                        */
        }

        [TestMethod]
        public void SellPriceDownEURUSD()
        {
            Sell sell2 = new Sell(1, "USD",  10000, new Price("EUR/USD", 0.9517M, 0.9517M));

            sell2.UpdateTradeValue(new Price("EUR/USD", 0.9505M, 0.9505M));

            Assert.AreEqual(10000, sell2.Amount);
            Assert.AreEqual(0.9517M, sell2.Ask);
            Assert.AreEqual(0.9505M, sell2.Bid);
            Assert.AreEqual(12.00M, sell2.GainLoss);
            Assert.AreEqual(0.0M, sell2.Spread);
            Assert.AreEqual(00M, sell2.SpreadValue);
            Assert.AreEqual("EUR/USD", sell2.Symbol);
            Assert.AreEqual(1, sell2.TradeID);
            Assert.AreEqual(TradingAccount.Status.Open, sell2.TradeStatus);
            Assert.AreEqual(9505M, sell2.TradeValue);
            Assert.AreEqual(12M, sell2.BaseGainLoss);
            Assert.AreEqual(9505M, sell2.BaseTradeValue);




            /*
            sell2 = new Sell(1, "USD", "EUR/USD", 1000, 1.1M, 1.1M);

            sell2.CalculateTradeValue(0.9M);

            Assert.AreEqual(1000, sell2.Amount);
            Assert.AreEqual(1.1M, sell2.Ask);
            Assert.AreEqual(0.9M, sell2.Bid);
            Assert.AreEqual(200M, sell2.GainLoss);
            Assert.AreEqual(0.0M, sell2.Spread);
            Assert.AreEqual(00M, sell2.SpreadValue);
            Assert.AreEqual("EUR/USD", sell2.Symbol);
            Assert.AreEqual(1, sell2.TradeID);
            Assert.AreEqual(TradingAccount.Status.Open, sell2.TradeStatus);
            Assert.AreEqual(900M, sell2.TradeValue);
    */   
    }


        [TestMethod]
        public void InitBuyUSDJPY()
        {
            Buy buy2 = new Buy(1, "USD",  10000, new Price("USD/JPY", 115.05M, 115.05M));

          //  buy2.CalculateTradeValue(114.45M);

            Assert.AreEqual(10000, buy2.Amount);
            Assert.AreEqual(115.05M, buy2.Ask);
            Assert.AreEqual(115.05M, buy2.Bid);
            Assert.AreEqual(0M, buy2.GainLoss);
            Assert.AreEqual(0M, buy2.Spread);
            Assert.AreEqual(0M, buy2.SpreadValue);
            Assert.AreEqual("USD/JPY", buy2.Symbol);
            Assert.AreEqual(1, buy2.TradeID);
            Assert.AreEqual(TradingAccount.Status.Open, buy2.TradeStatus);
            Assert.AreEqual(1150500M, buy2.TradeValue);

            Assert.AreEqual(10000M, buy2.BaseTradeValue);
            Assert.AreEqual(0M, buy2.BaseGainLoss);

        }


        [TestMethod]
        public void InitBuyUSDJPYPriceDown()
        {
            Buy buy2 = new Buy(1, "USD",  10000, new Price("USD/JPY", 115.05M, 115.05M));

              buy2.UpdateTradeValue(new Price("USD/JPY", 114.45M, 114.45M));

            Assert.AreEqual(10000, buy2.Amount);
            Assert.AreEqual(114.45M, buy2.Ask);
            Assert.AreEqual(115.05M, buy2.Bid);
            Assert.AreEqual(-6000M, buy2.GainLoss);
            Assert.AreEqual(0M, buy2.Spread);
            Assert.AreEqual(0M, buy2.SpreadValue);
            Assert.AreEqual("USD/JPY", buy2.Symbol);
            Assert.AreEqual(1, buy2.TradeID);
            Assert.AreEqual(TradingAccount.Status.Open, buy2.TradeStatus);
            Assert.AreEqual(1144500M, buy2.TradeValue);

            Assert.AreEqual(9947.58M, System.Math.Round(buy2.BaseTradeValue,2));
            Assert.AreEqual(-52.42M, System.Math.Round(buy2.BaseGainLoss,2));

        }

        [TestMethod]
        public void InitBuyUSDJPYPriceUp()
        {
            Buy buy2 = new Buy(1, "USD",  10000, new Price("USD/JPY", 115.05M, 115.05M));

            buy2.UpdateTradeValue(new Price("USD/JPY", 120.45M, 120.45M));

            Assert.AreEqual(10000, buy2.Amount);
            Assert.AreEqual(120.45M, buy2.Ask);
            Assert.AreEqual(115.05M, buy2.Bid);
            //Assert.AreEqual(-6000M, buy2.GainLoss);
            Assert.AreEqual(0M, buy2.Spread);
            Assert.AreEqual(0M, buy2.SpreadValue);
            Assert.AreEqual("USD/JPY", buy2.Symbol);
            Assert.AreEqual(1, buy2.TradeID);
            //Assert.AreEqual(TradingAccount.Status.Open, buy2.TradeStatus);
            //Assert.AreEqual(1144500M, buy2.TradeValue);

            Assert.AreEqual(10448.32M, System.Math.Round(buy2.BaseTradeValue, 2));
            Assert.AreEqual(448.32M, System.Math.Round(buy2.BaseGainLoss, 2));

        }

        //--------------------

        [TestMethod]
        public void InitSellUSDJPY()
        {
            Sell sell = new Sell(1, "USD", 10000, new Price("USD/JPY", 115.05M, 115.05M));

            //  buy2.CalculateTradeValue(114.45M);

            Assert.AreEqual(10000, sell.Amount);
            Assert.AreEqual(115.05M, sell.Ask);
            Assert.AreEqual(115.05M, sell.Bid);
            Assert.AreEqual(0M, sell.GainLoss);
            Assert.AreEqual(0M, sell.Spread);
            Assert.AreEqual(0M, sell.SpreadValue);
            Assert.AreEqual("USD/JPY", sell.Symbol);
            Assert.AreEqual(1, sell.TradeID);
            Assert.AreEqual(TradingAccount.Status.Open, sell.TradeStatus);
            Assert.AreEqual(1150500M, sell.TradeValue);

            Assert.AreEqual(10000M, sell.BaseTradeValue);
            Assert.AreEqual(0M, sell.BaseGainLoss);

        }


        [TestMethod]
        public void InitSellUSDJPYPriceDown()
        {
            Sell sell = new Sell(1, "USD", 10000, new Price("USD/JPY", 115.05M, 115.05M));

            sell.UpdateTradeValue(new Price("USD/JPY", 114.45M, 114.45M));

            Assert.AreEqual(10000, sell.Amount);
            Assert.AreEqual(115.05M, sell.Ask);
            Assert.AreEqual(114.45M, sell.Bid);
            Assert.AreEqual(6000M, sell.GainLoss);
            Assert.AreEqual(0M, sell.Spread);
            Assert.AreEqual(0M, sell.SpreadValue);
            Assert.AreEqual("USD/JPY", sell.Symbol);
            Assert.AreEqual(1, sell.TradeID);
            Assert.AreEqual(TradingAccount.Status.Open, sell.TradeStatus);
            Assert.AreEqual(1144500M, sell.TradeValue);

            Assert.AreEqual(10052.42M, System.Math.Round(sell.BaseTradeValue, 2));
            Assert.AreEqual(52.42M, System.Math.Round(sell.BaseGainLoss, 2));

        }

        [TestMethod]
        public void InitSellUSDJPYPriceUp()
        {
            Sell sell = new Sell(1, "USD", 10000, new Price("USD/JPY", 115.05M, 115.05M));

            sell.UpdateTradeValue(new Price("USD/JPY", 120.45M, 120.45M));

            Assert.AreEqual(10000, sell.Amount);
            Assert.AreEqual(115.05M, sell.Ask);
            Assert.AreEqual(120.45M, sell.Bid);
            Assert.AreEqual(-54000M, sell.GainLoss);
            Assert.AreEqual(0M, sell.Spread);
            Assert.AreEqual(0M, sell.SpreadValue);
            Assert.AreEqual("USD/JPY", sell.Symbol);
            Assert.AreEqual(1, sell.TradeID);
            //Assert.AreEqual(TradingAccount.Status.Open, buy2.TradeStatus);
            //Assert.AreEqual(1144500M, buy2.TradeValue);

            Assert.AreEqual(9551.68M, System.Math.Round(sell.BaseTradeValue, 2));
            Assert.AreEqual(-448.32M, System.Math.Round(sell.BaseGainLoss, 2));

        }

    }
}
