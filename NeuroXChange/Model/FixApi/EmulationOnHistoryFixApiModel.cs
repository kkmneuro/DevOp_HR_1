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
        public override void OnNext(BioData.BioData data)
        {
            // treat payload as price information
            if ((data.payload != null) && (data.payload is string[]))
            {
                var payload = (string[])data.payload;
                var tickPrice = new TickPrice();
                tickPrice.buy = payload[0];
                tickPrice.sell = payload[1];
                tickPrice.time = data.time;
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
