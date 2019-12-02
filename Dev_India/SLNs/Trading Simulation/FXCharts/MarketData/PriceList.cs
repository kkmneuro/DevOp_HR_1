using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketData
{
    public class PriceList
    {
        public TimeFrame.TF tf { get; set; } = TimeFrame.TF.Min; // default one minute
        public Future f { get; set; }
        public List<PriceCandle> p { get; set; }


        public PriceList(TimeFrame.TF Tf)
        {
            tf = Tf;
            p = new List<PriceCandle>();
        }


        public TimeSpan LengthOfCandle()
        {
            TimeSpan cduration;

            switch (tf)
            {
                case TimeFrame.TF.None: cduration = new TimeSpan(0); break;
                case TimeFrame.TF.Tick: cduration = new TimeSpan(0); break;
                case TimeFrame.TF.Min: cduration = new TimeSpan(0, 1, 0); break;
                case TimeFrame.TF.Min5: cduration = new TimeSpan(0, 5, 0); break;
                case TimeFrame.TF.Min20: cduration = new TimeSpan(0, 20, 0); break;
                case TimeFrame.TF.Hour: cduration = new TimeSpan(1, 0, 0); break;
                case TimeFrame.TF.Day: cduration = new TimeSpan(24, 0, 0); break;
                default: cduration = new TimeSpan(0, 1, 0); break;
            }

            return cduration;
        }

    }
}
