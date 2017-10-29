using NeuroXChange.Model.BehavioralModeling.BehavioralModels;
using System;

namespace NeuroXChange.Model.BehavioralModeling.Transitions
{
    public abstract class AbstractTransition
    {
        protected BehavioralModelState fromStates;
        protected BehavioralModelState toState;

        public AbstractTransition(
            BehavioralModelState fromStates,
            BehavioralModelState toState)
        {
            this.fromStates = fromStates;
            this.toState = toState;
        }

        public abstract BehavioralModelState Execute(SimpleBehavioralModel model, DateTime tickTime);
    }
}
