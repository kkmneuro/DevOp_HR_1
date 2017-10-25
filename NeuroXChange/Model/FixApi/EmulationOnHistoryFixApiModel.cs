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
                NotifyObservers(FixApiModelEvent.PriceChanged, (string[]) data.payload);
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
