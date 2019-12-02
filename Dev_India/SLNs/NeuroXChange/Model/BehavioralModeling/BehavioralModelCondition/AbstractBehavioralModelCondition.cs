namespace NeuroXChange.Model.BehavioralModeling.BehavioralModelCondition
{
    public abstract class AbstractBehavioralModelCondition
    {
        // is this condition met or not
        public bool isConditionMet { get; protected set; }
        
        // additional data, like is it buy or sell signal, etc.
        // depends on exact condition implementation
        public object detailsData { get; protected set; }

        public AbstractBehavioralModelCondition()
        {
            isConditionMet = false;
            detailsData = null;
        }

        public abstract void OnNext(BioData.BioData data);
    }
}
