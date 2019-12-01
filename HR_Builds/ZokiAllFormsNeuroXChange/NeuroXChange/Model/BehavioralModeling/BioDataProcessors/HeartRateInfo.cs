using System;

namespace NeuroXChange.Model.BehavioralModeling.BioDataProcessors
{
    public struct HeartRateInfo
    {
        public DateTime time;
        public double heartRate2minAverage;
        public string heartRateInnerState;
        public double oscillations1minAverage;
        public double oscillations3minAverage;
        public double oscillations5minAverage;
    }
}
