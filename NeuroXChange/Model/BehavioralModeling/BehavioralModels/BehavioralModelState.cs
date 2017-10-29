//using System;

namespace NeuroXChange.Model.BehavioralModeling.BehavioralModels
{
    //[Flags]
    public enum BehavioralModelState
    {
        InitialState = 1,
        ReadyToTrade = 2,
        Preactivation = 4,
        DirectionConfirmed = 8,
        ExecuteOrder = 16,
        ConfirmationFilled = 32
    }
}
