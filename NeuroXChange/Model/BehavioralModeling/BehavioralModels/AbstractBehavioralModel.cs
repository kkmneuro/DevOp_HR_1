using NeuroXChange.Model.BehavioralModeling.BehavioralModelCondition;
using NeuroXChange.Model.BehavioralModeling.BehavioralModels;
using System;
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

        // some variables
        private DateTime previousTickTime;
        private int lq1OrderDirection;
        private int lq2OrderDirection;

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

            previousTickTime = DateTime.FromOADate(0);
        }

        public virtual void OnNext(BioData.BioData data)
        {
            PreviousTickState = CurrentTickState;

            // reset count of trades today if date is new
            if (previousTickTime.Date != data.time.Date)
            {
                TradesToday = 0;
                dataRow["Trades today"] = TradesToday;
            }
            previousTickTime = data.time;

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


            // ------ Logic Query 1 ------
            if (logicQuery1Condition.isConditionMet)
            {
                if (PreviousTickState == BehavioralModelState.Preactivation)
                {
                    CurrentTickState = BehavioralModelState.DirectionConfirmed;
                    lq1OrderDirection = (int)logicQuery1Condition.detailsData;
                    OrderDirection = lq1OrderDirection;
                }
            }

            //  ------ Logic Query 2 ------
            if (logicQuery2Condition.isConditionMet)
            {
                int localLq2OrderDirection = (int)logicQuery2Condition.detailsData;
                if (PreviousTickState == BehavioralModelState.DirectionConfirmed)
                {
                    if (lq1OrderDirection == localLq2OrderDirection)
                    {
                        CurrentTickState = BehavioralModelState.ExecuteOrder;
                        lq2OrderDirection = localLq2OrderDirection;
                        OrderDirection = lq2OrderDirection;
                    }
                }
            }


            // ------ Simple transition between last 2 states ------
            if (PreviousTickState == BehavioralModelState.ExecuteOrder)
            {
                CurrentTickState = BehavioralModelState.ConfirmationFilled;
            }
            else if (PreviousTickState == BehavioralModelState.ConfirmationFilled)
            {
                TradesTotal++;
                TradesToday++;
                CurrentTickState = BehavioralModelState.InitialState;
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
