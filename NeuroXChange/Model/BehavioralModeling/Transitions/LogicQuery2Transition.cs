using NeuroXChange.Model.BehavioralModeling.BehavioralModelCondition;
using NeuroXChange.Model.BehavioralModeling.BehavioralModels;
using System;

namespace NeuroXChange.Model.BehavioralModeling.Transitions
{
    public class LogicQuery2Transition : AbstractTransition
    {
        private LogicQuery2Condition logicQuery2Condition;

        public LogicQuery2Transition(LogicQuery2Condition logicQuery2Condition)
            : base(BehavioralModelState.DirectionConfirmed, BehavioralModelState.ExecuteOrder)
        {
            this.logicQuery2Condition = logicQuery2Condition;
        }

        public override BehavioralModelState Execute(SimpleBehavioralModel model, DateTime tickTime)
        {
            if ((model.PreviousTickState & fromStates) > 0 && logicQuery2Condition.isConditionMet)
            {
                model.lq2OrderDirection = (int)logicQuery2Condition.detailsData;
                if (model.lq1OrderDirection == model.lq2OrderDirection)
                {
                    model.OrderDirection = model.lq2OrderDirection;
                    return toState;
                }
            }
            return model.PreviousTickState;
        }
    }
}
