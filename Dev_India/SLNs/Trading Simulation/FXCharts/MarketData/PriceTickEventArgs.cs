using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketData
{
    public delegate void PriceTickEventHandler(object source, PriceTickEventArgs e);

    public class PriceTickEventArgs : EventArgs
    {
        public string  Symbol   { get; set; }
        public decimal Price    { get; set; }
        public decimal PriceBid { get; set; }
        public decimal PriceAsk { get; set; }
        public decimal Volume   { get; set; }

        public DateTime PriceTime { get; set; }
        public TimeFrame.TF TimeFrame { get; set; }  // this one is not needed in tikc price
    }
}
