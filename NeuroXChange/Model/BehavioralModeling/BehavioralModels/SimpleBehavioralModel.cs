using NeuroXChange.Model.BehavioralModeling.BehavioralModelCondition;
using NeuroXChange.Model.BehavioralModeling.BehavioralModels;
using System;
using System.Data;

namespace NeuroXChange.Model
{
    public class SimpleBehavioralModel
    {
        // used for saving model statistic information
        public DataRow dataRow { get; set; }

        // behavioral conditions
        private AccYCondition accYCondition;
        private HRReadyToTradeCondition hrReadyToTradeCondition;
        private HRPreactivationCondition hrPreactivationCondition;
        private AbstractBehavioralModelCondition logicQuery1Condition;
        private AbstractBehavioralModelCondition logicQuery2Condition;

        // specific condition variants
        public Func<bool> LQ1SubCondition { get; set; }
        public Func<bool> LQ2SubCondition { get; set; }
        // if HR Oscillation not met, move to "Initial state" / "Ready to trade"
        public bool MoveBackIfHROscNotMet { get; set; }
        // after this period of time, if there no LQ2 condition, move back to "Preactivation" state
        public TimeSpan DirectionConfirmedExpirationTime { get; set; }

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
        private DateTime previousTransitionToDirectionConfirmed;

        public SimpleBehavioralModel(
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

            LQ1SubCondition = null;
            LQ2SubCondition = null;
            MoveBackIfHROscNotMet = false;
            DirectionConfirmedExpirationTime = TimeSpan.FromMinutes(15);

            previousTickTime = DateTime.FromOADate(0);
            previousTransitionToDirectionConfirmed = DateTime.FromOADate(0);
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


            // ----- Move back if HR Oscillations conditions not met -----
            if (MoveBackIfHROscNotMet)
            {
                if (!hrPreactivationCondition.isConditionMet
                    && (PreviousTickState == BehavioralModelState.Preactivation
                        || PreviousTickState == BehavioralModelState.DirectionConfirmed))
                {
                    CurrentTickState = BehavioralModelState.ReadyToTrade;
                }
                if (!hrReadyToTradeCondition.isConditionMet
                    && (PreviousTickState == BehavioralModelState.ReadyToTrade
                        || PreviousTickState == BehavioralModelState.Preactivation
                        || PreviousTickState == BehavioralModelState.DirectionConfirmed))
                {
                    CurrentTickState = BehavioralModelState.InitialState;
                }
            }


            // ----- HR Oscillations conditions -----
            if (DirectionConfirmedExpirationTime != null)
            {
                if (PreviousTickState == BehavioralModelState.DirectionConfirmed)
                {
                    if (DirectionConfirmedExpirationTime < (data.time - previousTransitionToDirectionConfirmed))
                    {
                        CurrentTickState = BehavioralModelState.Preactivation;
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
                    if (LQ1SubCondition == null || LQ1SubCondition())
                    {
                        CurrentTickState = BehavioralModelState.DirectionConfirmed;
                        lq1OrderDirection = (int)logicQuery1Condition.detailsData;
                        OrderDirection = lq1OrderDirection;
                        previousTransitionToDirectionConfirmed = data.time;
                    }
                }
            }

            //  ------ Logic Query 2 ------
            if (logicQuery2Condition.isConditionMet)
            {
                if (PreviousTickState == BehavioralModelState.DirectionConfirmed)
                {
                    int localLq2OrderDirection = (int)logicQuery2Condition.detailsData;
                    if (lq1OrderDirection == localLq2OrderDirection &&
                        (LQ2SubCondition == null || LQ2SubCondition()))
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
