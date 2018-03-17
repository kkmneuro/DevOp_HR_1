using System;

namespace NeuroTraderProtocols
{
    [Serializable]
    public class StatisticsPacket
    {
        public double LastUserActionTime;
        public double LastBioDataTime;
        public double LastOrderTime;

        public override string ToString()
        {
            return $"UserAction: {DateTime.FromOADate(LastUserActionTime)}  BioData: {DateTime.FromOADate(LastBioDataTime)}  Order: {DateTime.FromOADate(LastOrderTime)}";
        }
    }
}
