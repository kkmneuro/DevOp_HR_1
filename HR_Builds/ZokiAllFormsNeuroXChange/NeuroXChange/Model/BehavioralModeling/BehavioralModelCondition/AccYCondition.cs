namespace NeuroXChange.Model.BehavioralModeling.BehavioralModelCondition
{
    public class AccYCondition : AbstractBehavioralModelCondition
    {
        // step constants
        private double stepChangeStart = -60;
        private double stepChangeEnd = -20;

        private bool returnedBack = false;

        public AccYCondition(
            double stepChangeStart,
            double stepChangeEnd)
        {
            this.stepChangeStart = stepChangeStart;
            this.stepChangeEnd = stepChangeEnd;
        }

        public override void OnNext(BioData.BioData data)
        {
            isConditionMet = false;
            if (data.accY > stepChangeEnd)
            {
                returnedBack = true;
            }
            if (data.accY < stepChangeStart && returnedBack)
            {
                returnedBack = false;
                isConditionMet = true;
            }
        }
    }
}
