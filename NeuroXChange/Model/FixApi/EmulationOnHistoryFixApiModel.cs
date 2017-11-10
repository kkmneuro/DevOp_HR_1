using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuroXChange.Model.BioData;
using NeuroXChange.Common;
using System.Data.OleDb;

namespace NeuroXChange.Model.FixApi
{
    public class EmulationOnHistoryFixApiModel : AbstractFixApiModel
    {
        public override void OnNext(BioDataEvent bioDataEvent, object data)
        {
            if (bioDataEvent != BioDataEvent.NewBioDataTick)
            {
                return;
            }

            var bioData = (BioData.BioData)data;

            // treat payload as price information
            if ((bioData.payload != null) && (bioData.payload is string[]))
            {
                var payload = (string[])bioData.payload;
                var tickPrice = new TickPrice();
                tickPrice.buy = payload[0];
                tickPrice.sell = payload[1];
                tickPrice.time = bioData.time;
                NotifyObservers(FixApiModelEvent.PriceChanged, tickPrice);
            }
        }

        public override void StartProcessing()
        {
        }

        public override void StopProcessing()
        {
        }
    }
}
