namespace NeuroXChange.Model.BioData
{
    public enum BioDataEvent
    {
        NewBioDataTick,

        EmulationModePaused,
        EmulationModeContinued,

        // event only to show progress (total ticks and how many ticks remain)
        EmulationModeProgress,

        // there is no more bio data
        EmulationModeBioDataFinished
    }
}
