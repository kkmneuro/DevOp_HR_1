using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingAccount
{
    public class Price
    {
        public decimal Ask { get; set; }
        public decimal Bid { get; set; }

        public string Symbol { get; set; }

        public Price()
        {
        }

        public Price(string symbol, decimal ask, decimal bid)
        {
            Symbol = symbol;
            Ask = ask;
            Bid = bid;
        }
    }
}
