using NeuroXChange.Model.BehavioralModeling.BehavioralModels;
using NeuroXChange.Model.BehavioralModeling.Transitions;
using NeuroXChange.Model.FixApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace NeuroXChange.Model
{
    public class SimpleBehavioralModel
    {
        // transitions
        public List<AbstractTransition> transitions { get; set; }

        // used for saving model statistic information
        public DataRow DataRowInBehavioralModelsWindow { get; set; }

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

        public LinkedList<TransitionHistoryItem> TransitionHistory { get; private set; }

        public TickPrice lastPrice { get; set; }

        public SimpleBehavioralModel()
        {
            PreviousTickState = BehavioralModelState.InitialState;
            CurrentTickState = BehavioralModelState.InitialState;
            transitions = new List<AbstractTransition>();
            TransitionHistory = new LinkedList<TransitionHistoryItem>();
        }

        public virtual void OnNext(BioData.BioData data)
        {
            PreviousTickState = CurrentTickState;

            // reset count of trades today if date is new
            if (previousTickTime.Date != data.time.Date)
            {
                TradesToday = 0;
                DataRowInBehavioralModelsWindow["Trades today"] = TradesToday;
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

                var inst = new TransitionHistoryItem();
                inst.ID = TransitionHistory.Count;
                inst.Time = data.time.ToString("MM/dd  HH:mm:ss");
                inst.ToState = BehavioralModelStateHelper.StateToString(CurrentTickState);
                inst.FromState = BehavioralModelStateHelper.StateToString(PreviousTickState);
                inst.Transition = executedTransition.ToString();
                TransitionHistory.AddLast(inst);
            }
        }

        public virtual void UpdateStatistics()
        {
            DataRowInBehavioralModelsWindow["State"] = BehavioralModelStateHelper.StateToString(CurrentTickState);
            if (CurrentTickState == BehavioralModelState.DirectionConfirmed ||
                CurrentTickState == BehavioralModelState.ExecuteOrder)
            {
                DataRowInBehavioralModelsWindow["In position"] = OrderDirection == 0 ? "LONG" : OrderDirection == 1 ? "SHORT" : "NO DIRECTION";
            }
            else
            {
                DataRowInBehavioralModelsWindow["In position"] = "-";
            }
            DataRowInBehavioralModelsWindow["All trades"] = TradesTotal;
            DataRowInBehavioralModelsWindow["Trades today"] = TradesToday;
            DataRowInBehavioralModelsWindow["Profitability"] = Profitability;
        }
    }
}
