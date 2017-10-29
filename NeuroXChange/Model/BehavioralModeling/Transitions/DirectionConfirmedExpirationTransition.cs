using NeuroXChange.Model.BehavioralModeling.BehavioralModels;
using System;

namespace NeuroXChange.Model.BehavioralModeling.Transitions
{
    public class DirectionConfirmedExpirationTransition : AbstractTransition
    {
        private TimeSpan expirationTime;

        public DirectionConfirmedExpirationTransition(TimeSpan expirationTime)
            : base(BehavioralModelState.DirectionConfirmed, BehavioralModelState.Preactivation)
        {
            this.expirationTime = expirationTime;
        }

        public override BehavioralModelState Execute(SimpleBehavioralModel model, DateTime tickTime)
        {
            if ((model.PreviousTickState & fromStates) > 0
                && (tickTime - model.previousTransitionToDirectionConfirmed) > expirationTime)
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
