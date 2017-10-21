namespace NeuroXChange.Model.BehavioralModeling.BehavioralModelCondition
{
    public class AccYCondition : AbstractBehavioralModelCondition
    {
        // step constants
        private int stepChangeStart = -60;
        private int stepChangeEnd = -20;

        private bool returnedBack = false;

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
