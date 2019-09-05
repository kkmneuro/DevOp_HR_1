using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketData
{

    public class PriceGeneratorHelper
    {



        Random r;

        public PriceGeneratorHelper()
        {
            r = new Random();
        }


        /// <summary>
        /// how many steps to go up or down in price
        /// </summary>
        /// <param name="fluctuationStep"></param>
        /// <param name="tf"></param>
        /// <returns></returns>
        private int stepsHighLow(TimeFrame.TF tf)
        {
            switch (tf)
            {
                case TimeFrame.TF.Tick: return 4;
                case TimeFrame.TF.Min: return 6;
                case TimeFrame.TF.Min5: return 12;
                case TimeFrame.TF.Min20: return 24;
                case TimeFrame.TF.Hour: return 50;
                case TimeFrame.TF.Day: return 100;
            }

            return 3;

        }


        public PriceCandle getNextPriceCandle(decimal openPrice, TimeFrame.TF tf)
        {
            decimal open = openPrice;
            int highSteps, lowSteps, closeSteps;

            decimal fluctuationStep = openPrice / 1000; // random step used to create price swing

            int x = stepsHighLow(tf);
            highSteps = r.Next(x);
            lowSteps = r.Next(x);

            int xx = r.Next(2);

            if (xx == 1)         // positive(2) or negative(1) change in closing price
                closeSteps = r.Next(lowSteps) * (-1);
            else closeSteps = r.Next(highSteps);

            PriceCandle p = new PriceCandle();

            p.Open = open;
            p.High = open + highSteps * fluctuationStep;
            p.Low = open - lowSteps * fluctuationStep;
            p.Close = open + closeSteps * fluctuationStep;
            //p.Date 

            return p;
        }

        public decimal GetNextRandomPrice(decimal _lastPrice)
        {

            decimal fluctuationStep = _lastPrice / 1000; // random step used to create price swing

            int x = stepsHighLow(TimeFrame.TF.Tick);
            int steps = r.Next(x);



            int xx = r.Next(2);

            if (xx == 1)         // positive(2) or negative(1) change in closing price
                steps = steps * (-1);

            return _lastPrice + (fluctuationStep * steps);
        }
    }
    
}
