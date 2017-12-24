using System;

namespace NeuroXChange.Model
{
    [Flags]
    public enum ApplicationState
    {
        UsualState = 0,
        LiveModePaused = 1
    }
}
