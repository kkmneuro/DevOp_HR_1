using NeuroXChange.Model.BehavioralModeling.BehavioralModelCondition;
using NeuroXChange.Model.BehavioralModeling.BehavioralModels;
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
        public BehavioralModelState PreviousTickState { get; private set; }
        public BehavioralModelState CurrentTickState { get; private set; }

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

            PreviousTickState = BehavioralModelState.InitialState;
            CurrentTickState = BehavioralModelState.InitialState;
            OrderDirection = 0;
            TradesToday = 0;
            TradesTotal = 0;
            Profitability = 0.0;
        }

        public virtual void OnNext(BioData.BioData data)
        {
            PreviousTickState = CurrentTickState;

            // ----- AccY event ------
            if (accYCondition != null && accYCondition.isConditionMet)
            {
                switch (PreviousTickState)
                {
                    case BehavioralModelState.InitialState:
                        {
                            CurrentTickState = BehavioralModelState.ReadyToTrade;
                            break;
                        }
                    case BehavioralModelState.ReadyToTrade:
                        {
                            CurrentTickState = BehavioralModelState.Preactivation;
                            break;
                        }
                    case BehavioralModelState.Preactivation:
                        {
                            CurrentTickState = BehavioralModelState.DirectionConfirmed;
                            break;
                        }
                    case BehavioralModelState.DirectionConfirmed:
                        {
                            CurrentTickState = BehavioralModelState.ExecuteOrder;
                            break;
                        }
                    case BehavioralModelState.ExecuteOrder:
                        {
                            CurrentTickState = BehavioralModelState.ConfirmationFilled;
                            break;
                        }
                    case BehavioralModelState.ConfirmationFilled:
                        {
                            CurrentTickState = BehavioralModelState.InitialState;
                            break;
                        }
                }
            }


            // ----- HR Oscillations conditions -----
            if (hrReadyToTradeCondition.isConditionMet)
            {
                if (PreviousTickState == BehavioralModelState.InitialState)
                {
                    CurrentTickState = BehavioralModelState.ReadyToTrade;
                }
            }
            if (hrPreactivationCondition.isConditionMet)
            {
                if (PreviousTickState == BehavioralModelState.ReadyToTrade)
                {
                    CurrentTickState = BehavioralModelState.Preactivation;
                }
            }


            if (PreviousTickState != CurrentTickState)
            {
                UpdateStatistics();
            }
        }

        public virtual void UpdateStatistics()
        {
            dataRow["State"] = BehavioralModelStateHelper.StateToString(CurrentTickState);
            if (CurrentTickState == BehavioralModelState.DirectionConfirmed ||
                CurrentTickState == BehavioralModelState.ExecuteOrder)
            {
                dataRow["In position"] = OrderDirection == 0 ? "LONG" : "SHORT";
            }
            else
            {
                dataRow["In position"] = "-";
            }
            dataRow["All trades"] = TradesTotal;
            dataRow["Trades today"] = TradesToday;
            dataRow["Profitability"] = Profitability;
        }
    }
}
