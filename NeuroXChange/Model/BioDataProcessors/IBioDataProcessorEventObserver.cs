namespace NeuroXChange.Model.BioDataProcessors
{
    public interface IBioDataProcessorEventObserver
    {
        void OnNext(BioDataProcessorEvent bioDataProcessorEvent, object data);
    }
}
