using NeuroXChange.Model.BehavioralModeling.BioDataProcessors;

namespace NeuroXChange.Model.BehavioralModeling.BehavioralModelCondition
{
    public class HRPreactivationCondition : AbstractBehavioralModelCondition
    {
        private HeartRateProcessor heartRateProcessor;
        private double minOscillationsCount;
        private double maxOscillationsCount;

        public HRPreactivationCondition(
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
            if (minOscillationsCount < info.oscillations5minAverage
                && info.oscillations5minAverage < maxOscillationsCount)
            {
                isConditionMet = true;
            }
        }
    }
}
