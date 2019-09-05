using NeuroXChange.Model.BehavioralModeling.BehavioralModelCondition;
using NeuroXChange.Model.BehavioralModeling.BehavioralModels;
using System;

namespace NeuroXChange.Model.BehavioralModeling.Transitions
{
    public class LogicQuery2Transition : AbstractTransition
    {
        private LogicQuery2Condition logicQuery2Condition;
        private Func<bool> basicCondition;
        private bool checkLQ1Direction;

        public LogicQuery2Transition(
            string name,
            LogicQuery2Condition logicQuery2Condition,
            Func<bool> basicCondition,
            bool checkLQ1Direction = true)
            : base(name, BehavioralModelState.DirectionConfirmed, BehavioralModelState.ExecuteOrder)
        {
            this.logicQuery2Condition = logicQuery2Condition;
            this.basicCondition = basicCondition;
            this.checkLQ1Direction = checkLQ1Direction;
        }

        public override BehavioralModelState Execute(SimpleBehavioralModel model, DateTime tickTime)
        {
            if ((model.PreviousTickState & fromStates) > 0 && logicQuery2Condition.isConditionMet)
            {
                model.lq2OrderDirection = (int)logicQuery2Condition.detailsData;
                if (basicCondition!= null && !basicCondition())
                {
                    return model.PreviousTickState;
                }

                if (!checkLQ1Direction || model.lq1OrderDirection == model.lq2OrderDirection)
                {
                    model.Direction = model.lq2OrderDirection;
                    return toState;
                }
            }
            return model.PreviousTickState;
        }
    }
}
