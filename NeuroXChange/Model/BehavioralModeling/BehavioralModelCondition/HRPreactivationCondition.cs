using NeuroXChange.Model.BehavioralModeling.BioDataProcessors;

namespace NeuroXChange.Model.BehavioralModeling.BehavioralModelCondition
{
    public class HRPreactivationCondition : AbstractBehavioralModelCondition
    {
        private HeartRateProcessor heartRateProcessor;

        public HRPreactivationCondition(HeartRateProcessor heartRateProcessor)
        {
            this.heartRateProcessor = heartRateProcessor;
        }

        // this condition uses OnNext only as signal for processing
        // and uses heartRateProcessor.heartRateInfo instead of data
        public override void OnNext(BioData.BioData data)
        {
            isConditionMet = false;
            HeartRateInfo info = heartRateProcessor.heartRateInfo;
            if (5 < info.oscillations5minAverage && info.oscillations5minAverage < 6.5)
            {
                isConditionMet = true;
            }
        }
    }
}
