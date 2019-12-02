using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TradingAccount;

namespace Tradings.UnitTests
{
    [TestClass]
    public class AccountTest
    {

        Account account;
        int tradeID = 0;
        public AccountTest()
        {
            //account = new Account(); 
            
        }

        [TestMethod]
        public void AddTradesSell()
        {


        }

        [TestMethod]
        public void AddTradesBuy()
        {
            account = new Account("USD", 1000, 100, 0);
            

            decimal balance = account.Balance; // 100 000
            decimal openPositionValue = account.OpenPositionsValue; // 0

            decimal tradeVals = 0;

            Buy s1 = new Buy(++tradeID, account.BaseCurrency, 1000, new Price("EUR/USD", 1.5M, 1.5M));
            tradeVals += s1.TotalPayment;

            Buy s2 = new Buy(++tradeID, account.BaseCurrency, 2000, new Price("EUR/USD", 1.5M, 1.5M));
            tradeVals += s2.TotalPayment;

            Buy s3 = new Buy(++tradeID, account.BaseCurrency, 3000, new Price("EUR/USD", 1.5M, 1.5M));
            tradeVals += s3.TotalPayment;


            account.AddTradeUpdateAccount(s1);
            account.AddTradeUpdateAccount(s2);
            account.AddTradeUpdateAccount(s3);


            Assert.AreEqual(100000, account.Balance);
            Assert.AreEqual(tradeVals, account.OpenPositionsValue);
            Assert.AreEqual(100000, account.AccountValue);
            Assert.AreEqual(91000, account.AvailableForTrade);
            Assert.AreEqual("USD", account.BaseCurrency);
            Assert.AreEqual(1000, account.Deposit);
            Assert.AreEqual(100, account.Leverage);
            Assert.AreEqual(0, account.MarginLevel);
            Assert.AreEqual(0, account.MarginValue);
            Assert.AreEqual(9000, account.OpenPositionsValue);
            Assert.AreEqual(0, account.UnrealisedProfitLoss);
            

        }


     

        [TestMethod]
        public void NewAccountAddTradePriceUp()
        {
            account = new Account("USD", 1000, 100, 50);


            decimal balance = account.Balance; // 100 000
            decimal openPositionValue = account.OpenPositionsValue; // 0

            decimal tradeVals = 0;

            Buy s1 = new Buy(++tradeID, account.BaseCurrency, 1000, new Price("EUR/USD", 1.5M, 1.5M));
            tradeVals += s1.TotalPayment;

            Buy s2 = new Buy(++tradeID, account.BaseCurrency, 1000, new Price("EUR/USD", 1.5M, 1.5M));
            tradeVals += s2.TotalPayment;

            Buy s3 = new Buy(++tradeID, account.BaseCurrency, 1000, new Price("EUR/USD", 1.5M, 1.5M));
            tradeVals += s3.TotalPayment;


            account.AddTradeUpdateAccount(s1);
            account.AddTradeUpdateAccount(s2);
            account.AddTradeUpdateAccount(s3);

            account.priceChange(new Price("EUR/USD", 1.6M, 1.6M));

            Assert.AreEqual(100000, account.Balance);
            Assert.AreEqual(100300, account.AccountValue);
            Assert.AreEqual(95500, account.AvailableForTrade);
            Assert.AreEqual("USD", account.BaseCurrency);
            Assert.AreEqual(1000, account.Deposit);
            Assert.AreEqual(100, account.Leverage);
            Assert.AreEqual(50, account.MarginLevel);
            Assert.AreEqual(500, account.MarginValue);
            Assert.AreEqual(4500, account.OpenPositionsValue);
            Assert.AreEqual(300, account.UnrealisedProfitLoss);
        }

        [TestMethod]
        public void NewAccountAddTradePriceDown()
        {
            account = new Account("USD", 1000, 100, 50);


            decimal balance = account.Balance; // 100 000
            decimal openPositionValue = account.OpenPositionsValue; // 0

            decimal tradeVals = 0;

            Buy s1 = new Buy(++tradeID, account.BaseCurrency, 1000, new Price("EUR/USD", 1.5M, 1.5M));
            tradeVals += s1.TotalPayment;

            Buy s2 = new Buy(++tradeID, account.BaseCurrency, 1000, new Price("EUR/USD", 1.5M, 1.5M));
            tradeVals += s2.TotalPayment;

            Buy s3 = new Buy(++tradeID, account.BaseCurrency, 1000, new Price("EUR/USD", 1.5M, 1.5M));
            tradeVals += s3.TotalPayment;


            account.AddTradeUpdateAccount(s1);
            account.AddTradeUpdateAccount(s2);
            account.AddTradeUpdateAccount(s3);

            account.priceChange(new Price("EUR/USD", 1.4M, 1.4M));

            Assert.AreEqual(100000, account.Balance);
            Assert.AreEqual(99700, account.AccountValue);
            Assert.AreEqual(95500, account.AvailableForTrade);
            Assert.AreEqual("USD", account.BaseCurrency);
            Assert.AreEqual(1000, account.Deposit);
            Assert.AreEqual(100, account.Leverage);
            Assert.AreEqual(50, account.MarginLevel);
            Assert.AreEqual(500, account.MarginValue);
            Assert.AreEqual(4500, account.OpenPositionsValue);
            Assert.AreEqual(-300, account.UnrealisedProfitLoss);
        }

        [TestMethod]
        public void NewAccountAddTradePriceUpCloseTrade()
        {
            account = new Account("USD", 1000, 100, 50);


            decimal balance = account.Balance; // 100 000
            decimal openPositionValue = account.OpenPositionsValue; // 0

            decimal tradeVals = 0;

            Buy s1 = new Buy(++tradeID, account.BaseCurrency, 1000, new Price("EUR/USD", 1.5M, 1.5M));
            tradeVals += s1.TotalPayment;

            Buy s2 = new Buy(++tradeID, account.BaseCurrency, 1000, new Price("EUR/USD", 1.5M, 1.5M));
            tradeVals += s2.TotalPayment;

            Buy s3 = new Buy(++tradeID, account.BaseCurrency, 1000, new Price("EUR/USD", 1.5M, 1.5M));
            tradeVals += s3.TotalPayment;


            account.AddTradeUpdateAccount(s1);
            account.AddTradeUpdateAccount(s2);
            account.AddTradeUpdateAccount(s3);

            account.priceChange(new Price("EUR/USD", 1.6M, 1.6M));

            account.CloseTradeUpdateAccount(s2);

            Assert.AreEqual(100100, account.Balance);
            Assert.AreEqual(100300, account.AccountValue);
            Assert.AreEqual(95500+1600, account.AvailableForTrade);
            Assert.AreEqual("USD", account.BaseCurrency);
            Assert.AreEqual(1100, account.Deposit);
            Assert.AreEqual(100, account.Leverage);
            Assert.AreEqual(50, account.MarginLevel);
            Assert.AreEqual(500, account.MarginValue);
            Assert.AreEqual(3000, account.OpenPositionsValue);
            Assert.AreEqual(200, account.UnrealisedProfitLoss);
        }


        [TestMethod]
        public void NewAccountAddTradePriceUpCloseTradeID()
        {
            account = new Account("USD", 1000, 100, 50);

            int tradeID = 0;

            decimal balance = account.Balance; // 100 000
            decimal openPositionValue = account.OpenPositionsValue; // 0

            decimal tradeVals = 0;

            Buy s1 = new Buy(++tradeID, account.BaseCurrency, 1000, new Price("EUR/USD", 1.5M, 1.5M));
            tradeVals += s1.TotalPayment;

            Buy s2 = new Buy(++tradeID, account.BaseCurrency, 1000, new Price("EUR/USD", 1.5M, 1.5M));
            tradeVals += s2.TotalPayment;

            Buy s3 = new Buy(++tradeID, account.BaseCurrency, 1000, new Price("EUR/USD", 1.5M, 1.5M));
            tradeVals += s3.TotalPayment;


            account.AddTradeUpdateAccount(s1);
            account.AddTradeUpdateAccount(s2);
            account.AddTradeUpdateAccount(s3);

            account.priceChange(new Price("EUR/USD", 1.6M, 1.6M));

            account.CloseTradeUpdateAccount(2);

            Assert.AreEqual(100100, account.Balance);
            Assert.AreEqual(100300, account.AccountValue);
            Assert.AreEqual(95500 + 1600, account.AvailableForTrade);
            Assert.AreEqual("USD", account.BaseCurrency);
            Assert.AreEqual(1100, account.Deposit);
            Assert.AreEqual(100, account.Leverage);
            Assert.AreEqual(50, account.MarginLevel);
            Assert.AreEqual(500, account.MarginValue);
            Assert.AreEqual(3000, account.OpenPositionsValue);
            Assert.AreEqual(200, account.UnrealisedProfitLoss);
        }


        [TestMethod]
        public void NewAccountAddTradesPriceDownMarginCall()
        {

            account = new Account("USD", 1000, 100, 50);


            decimal balance = account.Balance; // 100 000
            decimal openPositionValue = account.OpenPositionsValue; // 0

            decimal tradeVals = 0;

            Buy s1 = new Buy(++tradeID, account.BaseCurrency, 10000, new Price("EUR/USD", 1.5M, 1.5M));
            tradeVals += s1.TotalPayment;

            Buy s2 = new Buy(++tradeID, account.BaseCurrency, 10000, new Price("EUR/USD", 1.5M, 1.5M));
            tradeVals += s2.TotalPayment;

            Buy s3 = new Buy(++tradeID, account.BaseCurrency, 10000, new Price("EUR/USD", 1.5M, 1.5M));
            tradeVals += s3.TotalPayment;

//            Buy s4 = new Buy(++tradeID, account.BaseCurrency, 10000, new Price("EUR/USD", 1.5M, 1.5M));
//            tradeVals += s4.TotalPayment;


            account.AddTradeUpdateAccount(s1);
            account.AddTradeUpdateAccount(s2);
            account.AddTradeUpdateAccount(s3);
  //          account.AddTradeUpdateAccount(s4);

            account.priceChange(new Price("EUR/USD", 1.48M, 1.48M));

            Assert.AreEqual(99400, account.Balance);
            Assert.AreEqual(99400, account.AccountValue);
            Assert.AreEqual(99400, account.AvailableForTrade);
            Assert.AreEqual("USD", account.BaseCurrency);
            Assert.AreEqual(400, account.Deposit);
            Assert.AreEqual(100, account.Leverage);
            Assert.AreEqual(50, account.MarginLevel);
            Assert.AreEqual(500, account.MarginValue);
            Assert.AreEqual(0, account.OpenPositionsValue);
            Assert.AreEqual(0, account.UnrealisedProfitLoss);
            Assert.AreEqual(AccountStatus.ClosedOnMarginCall, account.AccountStatus);
        }
    }
}
