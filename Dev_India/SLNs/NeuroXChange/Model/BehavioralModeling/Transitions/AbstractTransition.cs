using NeuroXChange.Model.BehavioralModeling.BehavioralModels;
using System;

namespace NeuroXChange.Model.BehavioralModeling.Transitions
{
    public abstract class AbstractTransition
    {
        protected BehavioralModelState fromStates;
        protected BehavioralModelState toState;

        public string Name { get; set; }

        public AbstractTransition(
            string name,
            BehavioralModelState fromStates,
            BehavioralModelState toState)
        {
            this.Name = name;
            this.fromStates = fromStates;
            this.toState = toState;
        }

        public abstract BehavioralModelState Execute(SimpleBehavioralModel model, DateTime tickTime);

        public override string ToString()
        {
            return Name;
        }
    }
}
