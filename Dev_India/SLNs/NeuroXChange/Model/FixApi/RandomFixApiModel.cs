using System;
using NeuroXChange.Model.BioData;
using NeuroXChange.Model.Database;
using NeuroXChange.Common;
using System.Threading;
using System.Collections.Generic;

namespace NeuroXChange.Model.FixApi
{
    public class RandomFixApiModel:AbstractFixApiModel
    {
        private volatile bool NeedStop = false;
        private Thread threadReader;
        private Random random;

        private TickPrice priceDataBottom = null;

        public RandomFixApiModel(LocalDatabaseConnector localDatabaseConnector,
            IniFileReader iniFileReader)
            : base(localDatabaseConnector)
        {
            threadReader = new Thread(GenerateNewData);
            random = new Random();
        }

        private void GenerateNewData()
        {
            var sell = 1.1 + random.NextDouble();
            var buy = sell + 0.0005;
            priceDataBottom = new TickPrice(sell.ToString("0.#####"), buy.ToString("0.#####"), DateTime.Now);

            while (!NeedStop)
            {
                sell = priceDataBottom.sell + (random.NextDouble() - 0.5) / 1000.0;
                buy = sell + 0.00025;

                var tickPrice = new TickPrice(sell.ToString("0.#####"), buy.ToString("0.#####"), DateTime.Now);
                NotifyObservers(FixApiModelEvent.PriceChanged, tickPrice);
                priceDataBottom = tickPrice;
                Thread.Sleep(500);
            }
        }

        public override void OnNext(BioDataEvent bioDataEvent, object data)
        {
        }

        public override void StartProcessing()
        {
            threadReader.Start();
        }

        public override void StopProcessing()
        {
            NeedStop = true;
        }

        public override void SubscribeForQuotes(Enum ReqType, string contract)
        {
            throw new NotImplementedException();
        }

        public override void SubscribeForQuotes(Enum ReqType, List<string> lst)
        {
            throw new NotImplementedException();
        }
    }
}
