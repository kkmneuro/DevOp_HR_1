namespace NeuroXChange.Model
{
    // DO NOT FORGET TO ADD VALUE TO DATABASE ALSO WHEN NEW ADDED!
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
        TrainingFinished,
        ManualPositionChosen,
        MarketSentimentSurveyPressed = 50,

        // manual traiding
        PositionManuallyOpened = 100,
        PositionManuallyClosed
    }
}
