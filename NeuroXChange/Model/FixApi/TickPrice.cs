using NeuroXChange.Common;
using System;

namespace NeuroXChange.Model.FixApi
{
    public class TickPrice
    {
        public string buyString;
        public string sellString;
        public double buy;
        public double sell;
        public DateTime time;

        public TickPrice() : this("", "", DateTime.Now)
        {
        }

        public TickPrice(string buyString, string sellString, DateTime time)
        {
            this.buyString = buyString;
            this.sellString = sellString;
            this.time = time;

            if (!string.IsNullOrEmpty(buyString))
            {
                this.buy = StringHelpers.ParseDoubleCultureIndependent(buyString);
            }
            else
            {
                this.buy = double.NaN;
            }

            if (!string.IsNullOrEmpty(sellString))
            {
                this.sell = StringHelpers.ParseDoubleCultureIndependent(sellString);
            }
            else
            {
                this.sell = double.NaN;
            }
        }

        public void UpdateFrom(TickPrice other)
        {
            buyString = other.buyString;
            sellString = other.sellString;
            buy = other.buy;
            sell = other.sell;
            time = other.time;
        }
    }
}
