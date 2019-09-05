using NeuroXChange.Model.BehavioralModeling.BehavioralModelCondition;
using NeuroXChange.Model.BehavioralModeling.BehavioralModels;
using System;

namespace NeuroXChange.Model.BehavioralModeling.Transitions
{
    public class FunctionalTransition : AbstractTransition
{
        private Func<bool> func;

        public FunctionalTransition(string name,
            Func<bool> func,
            BehavioralModelState fromStates,
            BehavioralModelState toState) : base(name, fromStates, toState)
        {
            this.func = func;
        }

        public override BehavioralModelState Execute(SimpleBehavioralModel model, DateTime tickTime)
        {
            if ((model.PreviousTickState & fromStates) > 0 && func())
            {
                return toState;
            }
            else
            {
                return model.PreviousTickState;
            }
        }
    }
}
