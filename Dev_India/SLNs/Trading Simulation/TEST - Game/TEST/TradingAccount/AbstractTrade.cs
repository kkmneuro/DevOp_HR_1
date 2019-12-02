using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingAccount
{

    
    public enum Status { Open = 1, Closed = 0 }


    /// <summary>
    /// Base Class For Trade Action (trade only with major pairs with USD)
    /// </summary>
    public abstract class AbstractTrade
    {
        /// <summary>
        /// Trade ID
        /// </summary>
        public long TradeID { get;  set; }

        /// <summary>
        /// Currency of account.
        /// After trade is closed its value is translated to profit/loss into account currency
        /// </summary>
        public string AccoutCurrency { get; set; }

        /// <summary>
        /// Amount of base currency in a trade possition
        /// </summary>
        public decimal Amount{ get; protected set; }
    
        /// <summary>
        /// price of future
        /// </summary>
        public decimal Ask { get; protected set; }

        /// <summary>
        /// price of future (at time of purchase)
        /// </summary>
        public decimal AskInitial { get; protected set; }


        /// <summary>
        /// direrence between ask and bid prices at trade action moment (pips).
        /// </summary>
        public decimal Spread { get; protected set; }


        /// <summary>
        /// Value paid for trade (trde price)
        /// </summary>
        public decimal SpreadValue { get; protected set; }

        /// <summary>
        /// Total valuea of trade (unrealised loss or profit)
        /// </summary>
        public decimal TradeValue { get; set; }

        /// <summary>
        /// Total valuea of trade (unrealised loss or profit) in account currency
        /// </summary>
        public decimal BaseTradeValue { get; set; }

        /// <summary>
        /// Bid price (at time of purchase)
        /// </summary>
        public decimal Bid { get; protected set; }


        /// <summary>
        /// Bid price (at time of purchase)
        /// </summary>
        public decimal BidInitial { get; protected set; }
        /// <summary>
        /// what is traded, ussually a currency pair
        /// </summary>
        public string Symbol { get; protected set; }

        /// <summary>
        /// Total Loss or profit of the trade
        /// </summary>
        public decimal GainLoss { get; protected set; }

        /// <summary>
        /// Total Loss or profit of the trade in accounts currency
        /// </summary>
        public decimal BaseGainLoss { get; protected set; }

        /// <summary>
        /// Open or close
        /// </summary>
        public Status TradeStatus { get; protected set; }

        /// <summary>
        /// Amount of account's currency in a trade.
        /// </summary>
        public decimal TotalPayment { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tradeId"></param>
        /// <param name="accoutCurrency"></param>
        /// <param name="amount"></param>
        /// <param name="price">price of future</param>
        public AbstractTrade(int tradeId, string accoutCurrency, decimal amount, Price price)
        {
            TradeID = tradeId;
            AccoutCurrency = accoutCurrency;
            Ask = price.Ask; // ask price
            Bid = price.Bid;

            AskInitial = price.Ask; // ask price
            BidInitial = price.Bid;

            Amount = amount;
            Symbol = price.Symbol;            
            Spread = price.Ask - price.Bid;    // spread is calculate only when initiating an account 
            SpreadValue = Spread * amount; // trade price calculated initiating a trade
        }

        /// <summary>
        /// Reclculates current value of trade according to rate and return change
        /// </summary>
        /// <param name="symbol">Future of the trade</param>
        /// <param name="rate"> current rate of future</param>
        abstract public decimal UpdateTradeValue(Price price);

        /// <summary>
        /// Close current trade, if symbol maches at given symbol and updated ratate.
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="rate"></param>
        /// <returns>total trade (amount of trade plus gain/loss minus spead)</returns>
        public decimal Close(Price rate)
        {
            if (rate.Symbol == Symbol)
            TradeStatus = Status.Closed;
            return UpdateTradeValue(rate);
        }

        /// <summary>
        /// Close current trade and retunr a gain. 
        /// Ok if price uptadet automatically.
        /// </summary>
        /// <returns></returns>
        public decimal Close()
        {
            TradeStatus = Status.Closed;
            return TradeValue;
        }

        /// <summary>
        /// Calculate Trade total trade value and gain/loss in a base currency of account
        /// </summary>
        protected void TradeGainLossToBase()
        {
            if (Symbol.IndexOf(AccoutCurrency) == 0)
            {
                decimal x = 0;
                if (this.GetType() == typeof(Buy)) x = Ask;
                else if (this.GetType() == typeof(Sell)) x = Bid;
                BaseGainLoss = GainLoss *1/x;
                BaseTradeValue = TradeValue * 1/x + BaseGainLoss;
            }
            else
            { 
                    BaseGainLoss = GainLoss;
                    BaseTradeValue = TradeValue;             
            }
        }
    }
}
