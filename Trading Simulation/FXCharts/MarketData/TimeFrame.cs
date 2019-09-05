using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketData
{
    public class TimeFrame
    {

        public enum TF { None = -1, Tick = 0, Min = 10, Min5 = 20, Min20 = 30, Hour = 40, Day = 50 }

        // public TimeSpan Span { get; private set; }

        public static TimeSpan getTimeFrameSpan(TF s)
        {
            switch (s)
            {
                //Tick = 0, Min = 10, Min5 = 20, Min20 = 30, Hour = 40, Day = 50  
                case TF.Tick: return new TimeSpan(0, 0, 1);
                case TF.Min: return new TimeSpan(0, 1, 0);
                case TF.Min5: return new TimeSpan(0, 5, 0);
                case TF.Min20: return new TimeSpan(0, 20, 0);
                case TF.Hour: return new TimeSpan(1, 0, 0);
                case TF.Day: return new TimeSpan(1, 0, 0, 0);
                default: return new TimeSpan(0, 0, 1);
            }

        }


        public static TF getTFFromString(string tf)
        {
           switch (tf) {
                case "Day"   : return TF.Day; 
                case "Hour"  : return TF.Hour;
                case "Min"   : return TF.Min;
                case "Min20" : return TF.Min20;
                case "Min5"  : return TF.Min5;
                case "None"  : return TF.None;
                case "Tick"  : return TF.Tick;
                default:  return TF.Min;
            }
        }

        public static int getMinutesInFrame(TF tf)
        {
            switch (tf)
            {
                //Tick = 0, Min = 10, Min5 = 20, Min20 = 30, Hour = 40, Day = 50  
                case TF.Tick: return 0;
                case TF.Min: return 1;
                case TF.Min5: return 5;
                case TF.Min20: return 20;
                case TF.Hour: return 60;
                case TF.Day: return 1440;
                default: return 1;
            }
        }

    }
}
