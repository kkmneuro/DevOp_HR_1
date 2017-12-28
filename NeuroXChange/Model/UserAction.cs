﻿namespace NeuroXChange.Model
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
        TrainingFinished,
        ManualPositionChosen,

        // manual traiding
        PositionManuallyOpened = 100,
        PositionManuallyClosed,
        TakeProfitTriggered,
        StopLossTriggered
    }
}
