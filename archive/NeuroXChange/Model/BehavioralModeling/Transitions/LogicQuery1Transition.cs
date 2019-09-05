using NeuroXChange.Model.BehavioralModeling.BehavioralModelCondition;
using NeuroXChange.Model.BehavioralModeling.BehavioralModels;
using System;

namespace NeuroXChange.Model.BehavioralModeling.Transitions
{
    public class LogicQuery1Transition : AbstractTransition
    {
        private LogicQuery1Condition logicQuery1Condition;

        public LogicQuery1Transition(string name,
            LogicQuery1Condition logicQuery1Condition)
            : base(name, BehavioralModelState.Preactivation, BehavioralModelState.DirectionConfirmed)
        {
            this.logicQuery1Condition = logicQuery1Condition;
        }

        public override BehavioralModelState Execute(SimpleBehavioralModel model, DateTime tickTime)
        {
            if ((model.PreviousTickState & fromStates) > 0 && logicQuery1Condition.isConditionMet)
            {
                model.lq1OrderDirection = (int)logicQuery1Condition.detailsData;
                model.Direction = model.lq1OrderDirection;
                model.lastNot74SubProtocolID = logicQuery1Condition.lastNot74SubProtocolID;
                return toState;
            }
            else
            {
                return model.PreviousTickState;
            }
        }
    }
}
