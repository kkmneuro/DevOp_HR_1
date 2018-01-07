namespace NeuroXChange.Model.Portfolio
{
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
