﻿namespace NeuroXChange.Model.Portfolio
{
    // DO NOT FORGET TO ADD VALUE TO DATABASE ALSO WHEN NEW ADDED!
    public enum CloseReason
    {
        ShouldntBeClosed,
        ManuallyClosed,
        HardStopLossExecuted,
        TrailingStopLossExecuted,
        TakeProfitExecuted,
        ReverseOrderRequested,
        NotEnoughFunds
    }
}
