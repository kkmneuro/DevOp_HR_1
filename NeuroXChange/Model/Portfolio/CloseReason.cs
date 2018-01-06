namespace NeuroXChange.Model.Portfolio
{
    public enum CloseReason
    {
        ShouldntBeClosed,
        ManuallyClosed,
        StopLossExecuted,
        TakeProfitExecuted,
        ReverseOrderRequested,
        NotEnoughFunds
    }
}
