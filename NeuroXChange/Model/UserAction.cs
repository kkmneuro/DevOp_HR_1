namespace NeuroXChange.Model
{
    public enum UserAction
    {
        // application events
        ApplicationStarted = 1,
        ApplicationClosed,

        // training
        TrainingStarted = 10,
        TrainingManuallyStopped,
        TrainingPaused,
        TrainingResumed,
        TrainingFinished
    }
}
