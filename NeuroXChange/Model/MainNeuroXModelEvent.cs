namespace NeuroXChange.Model
{
    public enum MainNeuroXModelEvent
    {
        // application steps
        StepInitialState,
        StepReadyToTrade,
        StepPreactivation,
        StepDirectionConfirmed,
        StepExecuteOrder,
        StepConfirmationFilled,

        // logic queries
        LogicQueryDirection
    }
}
