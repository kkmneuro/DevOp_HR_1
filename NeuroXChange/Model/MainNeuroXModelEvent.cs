using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroXChange.Model
{
    public enum MainNeuroXModelEvent
    {
        StepInitialState,
        StepReadyToTrade,
        StepPreactivation,
        StepDirectionConfirmed,
        StepExecuteOrder,
        StepConfirmationFilled
    }
}
