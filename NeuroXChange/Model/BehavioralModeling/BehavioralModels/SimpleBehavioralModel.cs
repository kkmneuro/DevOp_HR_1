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

        public DataTable ProfitabilityHistory;

        public SimpleBehavioralModel()
        {
            PreviousTickState = BehavioralModelState.InitialState;
            CurrentTickState = BehavioralModelState.InitialState;
            transitions = new List<AbstractTransition>();
            TransitionHistory = new LinkedList<TransitionHistoryItem>();

            ProfitabilityHistory = new DataTable("Profitability History");
            ProfitabilityHistory.Columns.Add("Time", typeof(string));
            ProfitabilityHistory.Columns.Add("Order", typeof(string));
            ProfitabilityHistory.Columns.Add("Price", typeof(string));
            ProfitabilityHistory.Columns.Add("Value", typeof(string));
            ProfitabilityHistory.Columns.Add("Lot size", typeof(string));
            ProfitabilityHistory.Columns.Add("Balance", typeof(string));
            ProfitabilityHistory.Columns.Add("Profitability", typeof(string));
        }

        // profitability calculation variables
        private int LotSize = 100000;
        private double LastPrice = 0.0;
        private int LastValue = 0;
        private int TotalValue = 0;
        private double CurrentBalance = 0;

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

                    var row = ProfitabilityHistory.NewRow();
                    row["Time"] = data.time.ToString();
                    row["Order"] = OrderDirection == 0 ? "Buy" : "Sell";
                    row["Price"] = OrderDirection == 0 ? lastPrice.buyString : lastPrice.sellString;
                    var CurrentPrice = OrderDirection == 0 ? lastPrice.buy : lastPrice.sell;
                    if (LastValue == 0)
                    {
                        CurrentBalance = 0;
                        LastValue = OrderDirection == 0 ? 1 : -1;
                        TotalValue = LastValue;
                    } else
                    {
                        if (LastValue > 0 && OrderDirection == 0)
                        {
                            CurrentBalance += (CurrentPrice - LastPrice) * TotalValue * LotSize;
                            LastValue = 1;
                            TotalValue++;
                        }
                        else if (LastValue < 0 && OrderDirection == 1)
                        {
                            CurrentBalance += (LastPrice - CurrentPrice) * TotalValue * LotSize;
                            LastValue = -1;
                            TotalValue--;
                        }
                        else
                        {
                            if (LastValue > 0)
                            {
                                CurrentBalance += (CurrentPrice - LastPrice) * TotalValue * LotSize;
                            }
                            else if (LastValue < 0)
                            {
                                CurrentBalance += (LastPrice - CurrentPrice) * TotalValue * LotSize;
                            }
                            LastValue = -TotalValue;
                            TotalValue = 0;
                        }
                    }
                    LastPrice = CurrentPrice;
                    row["Value"] = LastValue.ToString();
                    row["Lot size"] = LotSize.ToString();
                    row["Balance"] = CurrentBalance.ToString("0.##");
                    Profitability += CurrentBalance;
                    row["Profitability"] = Profitability.ToString("0.##");
                    ProfitabilityHistory.Rows.Add(row);
                    ProfitabilityHistory.AcceptChanges();

                    if (TotalValue == 0)
                    {
                        CurrentBalance = 0;
                    }

                    DataRowInBehavioralModelsWindow["Profitability"] = Profitability.ToString("0.##");
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
            DataRowInBehavioralModelsWindow["Profitability"] = Profitability.ToString("0.##");
        }
    }
}
