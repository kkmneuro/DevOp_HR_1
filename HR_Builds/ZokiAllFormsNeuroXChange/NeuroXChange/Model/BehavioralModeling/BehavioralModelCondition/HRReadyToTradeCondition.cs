using NeuroXChange.Model.BehavioralModeling.BioDataProcessors;

namespace NeuroXChange.Model.BehavioralModeling.BehavioralModelCondition
{
    public class HRReadyToTradeCondition : AbstractBehavioralModelCondition
    {
        private HeartRateProcessor heartRateProcessor;
        private double minOscillationsCount;
        private double maxOscillationsCount;

        public HRReadyToTradeCondition(
            HeartRateProcessor heartRateProcessor,
            double minOscillationsCount,
            double maxOscillationsCount)
        {
            this.heartRateProcessor = heartRateProcessor;
            this.minOscillationsCount = minOscillationsCount;
            this.maxOscillationsCount = maxOscillationsCount;
        }

        // this condition uses OnNext only as signal for processing
        // and uses heartRateProcessor.heartRateInfo instead of data
        public override void OnNext(BioData.BioData data)
        {
            isConditionMet = false;
            HeartRateInfo info = heartRateProcessor.heartRateInfo;
            if (minOscillationsCount < info.oscillations3minAverage
                && info.oscillations3minAverage < maxOscillationsCount)
            {
                isConditionMet = true;
            }
        }
    }
}
