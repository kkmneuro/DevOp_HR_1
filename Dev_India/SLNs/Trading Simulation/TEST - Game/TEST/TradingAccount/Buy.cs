using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingAccount
{
    public class Buy : AbstractTrade
    {
        
        public Buy(int tradeId, string accoutCurrency, decimal amount, Price price)
            :base(tradeId, accoutCurrency, amount, price)
        {
            TotalPayment = amount * price.Ask;
            TradeValue = TotalPayment - SpreadValue;
            GainLoss = TradeValue - TotalPayment;

            TradeGainLossToBase();

            TradeStatus = Status.Open;
        }

        /// <summary>
        /// Recalculate trade value at new price 
        /// </summary>
        /// <param name="symbol">if symbol mach</param>
        /// <param name="ask"></param>
        /// <returns></returns>
        public override decimal UpdateTradeValue( Price price)
        {
            Ask = price.Ask;
            TradeValue = Amount * price.Ask - SpreadValue;
            GainLoss = TradeValue - TotalPayment;
            TradeStatus = Status.Open;

            TradeGainLossToBase();

            return TradeValue;
        }


    }
}
