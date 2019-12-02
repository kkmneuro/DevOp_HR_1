namespace NeuroXChange.Model
{
    public enum MainNeuroXModelEvent
    {
        ActiveModelChanged,
        ActiveModelStateChanged,

        // inner model event
        LogicQueryDirection,

        // synchronization
        SyncrhonizationStarted,
        SynchronizationEvent,       // for debug purpose
        SynchronizationFinished
    }
}
