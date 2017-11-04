using NeuroXChange.Model.BehavioralModeling.BehavioralModelCondition;
using NeuroXChange.Model.BehavioralModeling.BehavioralModels;
using System;

namespace NeuroXChange.Model.BehavioralModeling.Transitions
{
    public class ConditionalTransition : AbstractTransition
    {
        private AbstractBehavioralModelCondition condition;

        public ConditionalTransition(string name,
            AbstractBehavioralModelCondition condition,
            BehavioralModelState fromStates,
            BehavioralModelState toState) : base(name, fromStates, toState)
        {
            this.condition = condition;
        }

        public override BehavioralModelState Execute(SimpleBehavioralModel model, DateTime tickTime)
        {
            if ((model.PreviousTickState & fromStates) > 0
                && condition.isConditionMet)
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
