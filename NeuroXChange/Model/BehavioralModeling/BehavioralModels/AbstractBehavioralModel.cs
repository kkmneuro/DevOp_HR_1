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
        public MainNeuroXModelEvent PreviousTickState { get; private set; }
        public MainNeuroXModelEvent CurrentTickState { get; private set; }


        // are we buying or selling
        public int OrderDirection { get; private set; }  // 0 - buy, 1 - sell

        // other statistics
        public int TradesToday { get; private set; }
        public int TradesTotal { get; private set; }
        public double Profitability { get; private set; }

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

            PreviousTickState = MainNeuroXModelEvent.StepInitialState;
            CurrentTickState = MainNeuroXModelEvent.StepInitialState;
            OrderDirection = 0;
            TradesToday = 0;
            TradesTotal = 0;
            Profitability = 0.0;
        }

        public virtual void OnNext(BioData.BioData data)
        {
            PreviousTickState = CurrentTickState;

            // change state according to accYCondition
            if (accYCondition.isConditionMet)
            {
                switch (PreviousTickState)
                {
                    case MainNeuroXModelEvent.StepInitialState:
                        {
                            CurrentTickState = MainNeuroXModelEvent.StepReadyToTrade;
                            break;
                        }
                    case MainNeuroXModelEvent.StepReadyToTrade:
                        {
                            CurrentTickState = MainNeuroXModelEvent.StepPreactivation;
                            break;
                        }
                    case MainNeuroXModelEvent.StepPreactivation:
                        {
                            CurrentTickState = MainNeuroXModelEvent.StepDirectionConfirmed;
                            break;
                        }
                    case MainNeuroXModelEvent.StepDirectionConfirmed:
                        {
                            CurrentTickState = MainNeuroXModelEvent.StepExecuteOrder;
                            break;
                        }
                    case MainNeuroXModelEvent.StepExecuteOrder:
                        {
                            CurrentTickState = MainNeuroXModelEvent.StepConfirmationFilled;
                            break;
                        }
                    case MainNeuroXModelEvent.StepConfirmationFilled:
                        {
                            CurrentTickState = MainNeuroXModelEvent.StepInitialState;
                            break;
                        }
                }
            }

            if (PreviousTickState != CurrentTickState)
            {
                UpdateStatistics();
            }
        }

        public virtual void UpdateStatistics()
        {
            dataRow["State"] = CurrentTickState.ToString();
            dataRow["In position"] = OrderDirection == 0 ? "LONG" : "SHORT";
            dataRow["All trades"] = TradesTotal;
            dataRow["Trades today"] = TradesToday;
            dataRow["Profitability"] = Profitability;
        }
    }
}
