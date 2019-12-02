using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingAccount
{
    public class Sell : Buy // AbstractTrade
    {
        /// <summary>
        /// Create a sell action for a given parameters
        /// </summary>
        /// <param name="tradeID"></param>
        /// <param name="symbol"></param>
        /// <param name="amount"></param>
        /// <param name="ask"></param>
        /// <param name="bid"></param>
        public Sell(int tradeId, string accoutCurrency, decimal amount,Price price)
                        : base(tradeId, accoutCurrency , amount, price)
        {
            TotalPayment = amount * price.Ask;
            TradeValue = TotalPayment - SpreadValue;
            GainLoss = TradeValue - TotalPayment;

            TradeGainLossToBase();

            TradeStatus = Status.Open;
        }

        /// <summary>
        ///  Recalculate trade value at new price 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        public override decimal UpdateTradeValue(Price price)
        {
            if (price.Symbol == this.Symbol)
            {
                Bid = price.Bid;
                TradeValue = Amount * price.Bid - SpreadValue;
                GainLoss = TotalPayment - TradeValue;
                //            if (GainLoss < 0) TradeValue = TradeValue + GainLoss;
                TradeStatus = Status.Open;


                TradeGainLossToBase();
            }

            return TradeValue;
        }

    }
}
