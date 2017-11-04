using NeuroXChange.Model.BehavioralModeling.BehavioralModels;
using NeuroXChange.Model.BehavioralModeling.Transitions;
using System;
using System.Collections.Generic;
using System.Data;

namespace NeuroXChange.Model
{
    public class SimpleBehavioralModel
    {
        // transitions
        public List<AbstractTransition> transitions { get; set; }

        // used for saving model statistic information
        public DataRow dataRow { get; set; }

        // behavioral model states
        public BehavioralModelState PreviousTickState { get; private set; }
        public BehavioralModelState CurrentTickState { get; private set; }
        private DateTime previousTickTime;

        // are we buying or selling
        public int OrderDirection { get; set; }  // 0 - buy, 1 - sell

        // other statistics
        public int TradesToday { get; private set; }
        public int TradesTotal { get; private set; }
        public double Profitability { get; private set; }

        // some variables
        public int lq1OrderDirection { get; set; }
        public int lq2OrderDirection { get; set; }
        public DateTime previousTransitionToDirectionConfirmed { get; private set; }
        public int lastNot74SubProtocolID { get; set; }

        // transition history
        public DataSet TransitionHistorySet { get; private set; }
        public DataTable TransitionHistoryTable { get; private set; }

        public SimpleBehavioralModel()
        {
            PreviousTickState = BehavioralModelState.InitialState;
            CurrentTickState = BehavioralModelState.InitialState;
            transitions = new List<AbstractTransition>();
            TransitionHistorySet = new DataSet("Transition history");
            TransitionHistoryTable = TransitionHistorySet.Tables.Add("TransitionHistory");
            TransitionHistoryTable.Columns.Add("Time", typeof(string));
            TransitionHistoryTable.Columns.Add("To state", typeof(string));
            TransitionHistoryTable.Columns.Add("From state", typeof(string));
            TransitionHistoryTable.Columns.Add("Transition", typeof(string));
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

            // execute transitions code
            AbstractTransition executedTransition = null;
            foreach (var transition in transitions)
            {
                // transitions could not only transit to another state,
                // but also update behavioral model inner variables
                CurrentTickState = transition.Execute(this, data.time);

                // if state was changed, skip other transitions
                if (PreviousTickState != CurrentTickState)
                {
                    executedTransition = transition;
                    break;
                }
            }

            if (PreviousTickState != CurrentTickState)
            {
                if (CurrentTickState == BehavioralModelState.DirectionConfirmed)
                {
                    previousTransitionToDirectionConfirmed = data.time;
                }
                if (PreviousTickState == BehavioralModelState.ConfirmationFilled)
                {
                    TradesTotal++;
                    TradesToday++;
                }
                UpdateStatistics();

                var row = TransitionHistoryTable.NewRow();
                row["Time"] = data.time.ToString("MM/dd  HH:mm:ss");
                row["To state"] = BehavioralModelStateHelper.StateToString(CurrentTickState);
                row["From state"] = BehavioralModelStateHelper.StateToString(PreviousTickState);
                row["Transition"] = executedTransition.ToString();
                TransitionHistoryTable.Rows.Add(row);
            }
        }

        public virtual void UpdateStatistics()
        {
            dataRow["State"] = BehavioralModelStateHelper.StateToString(CurrentTickState);
            if (CurrentTickState == BehavioralModelState.DirectionConfirmed ||
                CurrentTickState == BehavioralModelState.ExecuteOrder)
            {
                dataRow["In position"] = OrderDirection == 0 ? "LONG" : OrderDirection == 1 ? "SHORT" : "NO DIRECTION";
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
