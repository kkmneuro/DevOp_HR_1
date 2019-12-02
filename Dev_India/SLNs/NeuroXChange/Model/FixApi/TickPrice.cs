using NeuroXChange.Common;
using System;

namespace NeuroXChange.Model.FixApi
{
    public class TickPrice
    {
        public string sellString;
        public string buyString;
        public double buy;
        public double sell;
        public DateTime time;

        public TickPrice() : this("", "", DateTime.Now)
        {
        }

        public TickPrice(string sellString, string buyString, DateTime time)
        {
            this.sellString = sellString;
            this.buyString = buyString;
            this.time = time;

            if (!string.IsNullOrEmpty(sellString))
            {
                this.sell = StringHelpers.ParseDoubleCultureIndependent(sellString);
            }
            else
            {
                this.sell = double.NaN;
            }

            if (!string.IsNullOrEmpty(buyString))
            {
                this.buy = StringHelpers.ParseDoubleCultureIndependent(buyString);
            }
            else
            {
                this.buy = double.NaN;
            }
        }
    }
}
