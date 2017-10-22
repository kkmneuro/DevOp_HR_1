using NeuroXChange.Model.BehavioralModeling.BehavioralModelCondition;
using System.Data;

namespace NeuroXChange.Model
{
    public abstract class AbstractBehavioralModel
    {
        // used for saving model statistic information
        public DataRow dataRow { get; set; }

        // behavioral conditions
        private AccYCondition accYCondition;
        private HRReadyToTradeCondition hrReadyToTradeCondition;
        private HRPreactivationCondition hrPreactivationCondition;
        private AbstractBehavioralModelCondition logicQuery1Condition;
        private AbstractBehavioralModelCondition logicQuery2Condition;

        // behavioral model states
        public MainNeuroXModelEvent previousTickState { get; private set; }
        public MainNeuroXModelEvent currentTickState { get; private set; }


        // are we buying or selling
        public int orderDirection { get; private set; }  // 0 - buy, 1 - sell

        public AbstractBehavioralModel(
            AccYCondition accYCondition,
            HRReadyToTradeCondition hrReadyToTradeCondition,
            HRPreactivationCondition hrPreactivationCondition,
            AbstractBehavioralModelCondition logicQuery1Condition,
            AbstractBehavioralModelCondition logicQuery2Condition)
        {
            this.accYCondition = accYCondition;
            this.hrReadyToTradeCondition = hrReadyToTradeCondition;
            this.hrPreactivationCondition = hrPreactivationCondition;
            this.logicQuery1Condition = logicQuery1Condition;
            this.logicQuery2Condition = logicQuery2Condition;

            previousTickState = MainNeuroXModelEvent.StepInitialState;
            currentTickState = MainNeuroXModelEvent.StepInitialState;
            orderDirection = 0;
        }

        public virtual void OnNext(BioData.BioData data)
        {
            previousTickState = currentTickState;

            // change state according to accYCondition
            if (accYCondition.isConditionMet)
            {
                switch (previousTickState)
                {
                    case MainNeuroXModelEvent.StepInitialState:
                        {
                            currentTickState = MainNeuroXModelEvent.StepReadyToTrade;
                            break;
                        }
                    case MainNeuroXModelEvent.StepReadyToTrade:
                        {
                            currentTickState = MainNeuroXModelEvent.StepPreactivation;
                            break;
                        }
                    case MainNeuroXModelEvent.StepPreactivation:
                        {
                            currentTickState = MainNeuroXModelEvent.StepDirectionConfirmed;
                            break;
                        }
                    case MainNeuroXModelEvent.StepDirectionConfirmed:
                        {
                            currentTickState = MainNeuroXModelEvent.StepExecuteOrder;
                            break;
                        }
                    case MainNeuroXModelEvent.StepExecuteOrder:
                        {
                            currentTickState = MainNeuroXModelEvent.StepConfirmationFilled;
                            break;
                        }
                    case MainNeuroXModelEvent.StepConfirmationFilled:
                        {
                            currentTickState = MainNeuroXModelEvent.StepInitialState;
                            break;
                        }
                }
            }

            if (previousTickState != currentTickState)
            {
                dataRow["State"] = currentTickState.ToString();
            }
        }
    }
}
