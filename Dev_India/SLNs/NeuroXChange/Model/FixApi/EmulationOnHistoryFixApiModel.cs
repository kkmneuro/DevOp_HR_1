using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuroXChange.Model.BioData;
using NeuroXChange.Common;
using System.Data.OleDb;
using NeuroXChange.Model.Database;

namespace NeuroXChange.Model.FixApi
{
    public class EmulationOnHistoryFixApiModel : AbstractFixApiModel
    {
        public EmulationOnHistoryFixApiModel(LocalDatabaseConnector localDatabaseConnector)
            :base(localDatabaseConnector)
        {
        }

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
                var tickPrice = new TickPrice(payload[0], payload[1], bioData.time);
                NotifyObservers(FixApiModelEvent.PriceChanged, tickPrice);
            }
        }

        public override void StartProcessing()
        {
        }

        public override void StopProcessing()
        {
        }

        public override void SubscribeForQuotes(Enum ReqType, List<string> lst)
        {
            throw new NotImplementedException();
        }

        public override void SubscribeForQuotes(Enum ReqType, string contract)
        {
            throw new NotImplementedException();
        }
    }
}
