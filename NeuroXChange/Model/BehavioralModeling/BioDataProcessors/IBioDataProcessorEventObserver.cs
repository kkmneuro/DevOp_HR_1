namespace NeuroXChange.Model.BehavioralModeling.BioDataProcessors

{
    public interface IBioDataProcessorEventObserver
    {
        void OnNext(BioDataProcessorEvent bioDataProcessorEvent, object data);
    }
}
