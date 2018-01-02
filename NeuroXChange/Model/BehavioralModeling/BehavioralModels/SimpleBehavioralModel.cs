using NeuroXChange.Model.BehavioralModeling.BehavioralModels;
using NeuroXChange.Model.BehavioralModeling.Transitions;
using NeuroXChange.Model.Database;
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

        // order variables
        public int LotSize { get; set; }
        public int StopLossPips { get; set; }
        public int TakeProfitPips { get; set; }
        public double PipSize { get; set; }

        // profitability calculation
        public int TradesToday { get; private set; }
        public int TradesTotal { get; private set; }
        public double Profitability { get; private set; }
        public double OpenedOrderPrice { get; private set; }
        public int LastValue { get; private set; }
        public int TotalValue { get; private set; }
        public double CurrentBalance { get; private set; }

        // some variables
        public int lq1OrderDirection { get; set; }
        public int lq2OrderDirection { get; set; }
        public DateTime previousTransitionToDirectionConfirmed { get; private set; }
        public int lastNot74SubProtocolID { get; set; }

        public LinkedList<TransitionHistoryItem> TransitionHistory { get; private set; }
        public LocalDatabaseConnector localDatabaseConnector { get; set;}

        public DataTable ProfitabilityHistory;

        // private variables
        private TickPrice LastPrice;

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

            OpenedOrderPrice = 0.0;
            LastValue = 0;
            TotalValue = 0;
            CurrentBalance = 0.0;
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
                if (PreviousTickState == BehavioralModelState.ConfirmationFilled

                    // TODO: update code to have several opened orders
                    // this is a temporal limitation to have only one opened order
                    && (TotalValue == 0 ||
                        (TotalValue < 0 && OrderDirection == 0) ||
                        (TotalValue > 0 && OrderDirection == 1)))
                {
                    TradesTotal++;
                    TradesToday++;

                    // profitability calculation
                    var CurrentPrice = OrderDirection == 0 ? LastPrice.buy : LastPrice.sell;
                    var CurrentDiff = 0.0;
                    if (TotalValue == 0)
                    {
                        CurrentBalance = 0;
                        TotalValue = 0;
                        LastValue = OrderDirection == 0 ? 1 : -1;
                    } else
                    {
                        if (TotalValue > 0 && OrderDirection == 0)
                        {
                            CurrentDiff = (CurrentPrice - OpenedOrderPrice) * TotalValue * LotSize;
                            LastValue = 1;
                        }
                        else if (TotalValue < 0 && OrderDirection == 1)
                        {
                            CurrentDiff = (OpenedOrderPrice - CurrentPrice) * TotalValue * LotSize;
                            LastValue = -1;
                        }
                        else
                        {
                            if (TotalValue > 0)
                            {
                                CurrentDiff = (CurrentPrice - OpenedOrderPrice) * TotalValue * LotSize;
                            }
                            else if (TotalValue < 0)
                            {
                                CurrentDiff = (OpenedOrderPrice - CurrentPrice) * TotalValue * LotSize;
                            }
                            LastValue = -TotalValue;
                        }
                    }
                    TotalValue += LastValue;
                    OpenedOrderPrice = CurrentPrice;
                    CurrentBalance += CurrentDiff;
                    Profitability += CurrentDiff;

                    // add trade to trades history
                    var row = ProfitabilityHistory.NewRow();
                    row["Time"] = data.time.ToString();
                    row["Order"] = OrderDirection == 0 ? "Buy" : "Sell";
                    row["Price"] = OrderDirection == 0 ? LastPrice.buyString : LastPrice.sellString;
                    row["Value"] = LastValue.ToString();
                    row["Lot size"] = LotSize.ToString();
                    row["Balance"] = CurrentBalance.ToString("0.##");
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

        public virtual void OnNext(TickPrice price)
        {
            LastPrice = price;

            if (TotalValue == 0)
            {
                return;
            }

            int closeDirection;
            double CurrentPrice;
            double priceDiff;
            if (TotalValue > 0)
            {
                closeDirection = 1;
                CurrentPrice = LastPrice.sell;
                priceDiff = CurrentPrice - OpenedOrderPrice;
            }
            else
            {
                closeDirection = 0;
                CurrentPrice = LastPrice.buy;
                priceDiff = CurrentPrice - OpenedOrderPrice;
            }

            int pipDifference = (int)(priceDiff / PipSize);

            if (pipDifference > -StopLossPips && pipDifference < TakeProfitPips)
            {
                return;
            }

            var CurrentDiff = priceDiff * TotalValue * LotSize;

            if (pipDifference <= -StopLossPips)
            {
                localDatabaseConnector.WriteUserAction(
                    UserAction.StopLossTriggered,
                    string.Format("opened_direction:{0}, balance:{1}", TotalValue > 0 ? "buy" : "sell", CurrentDiff));
            }
            else
            {
                localDatabaseConnector.WriteUserAction(
                    UserAction.TakeProfitTriggered,
                    string.Format("opened_direction:{0}, balance:{1}", TotalValue > 0 ? "buy" : "sell", CurrentDiff));
            }


            // closing open orders
            LastValue = -TotalValue;
            TotalValue += LastValue;
            OpenedOrderPrice = CurrentPrice;
            CurrentBalance += CurrentDiff;
            Profitability += CurrentDiff;

            // add trade to trades history
            var row = ProfitabilityHistory.NewRow();
            row["Time"] = price.time.ToString();
            row["Order"] = closeDirection == 0 ? "Buy" : "Sell";
            row["Price"] = closeDirection == 0 ? LastPrice.buyString : LastPrice.sellString;
            row["Value"] = LastValue.ToString();
            row["Lot size"] = LotSize.ToString();
            row["Balance"] = CurrentBalance.ToString("0.##");
            row["Profitability"] = Profitability.ToString("0.##");
            ProfitabilityHistory.Rows.Add(row);
            ProfitabilityHistory.AcceptChanges();

            if (TotalValue == 0)
            {
                CurrentBalance = 0;
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

        public bool ManualTrade(
            int direction, TickPrice price, double takeProfit, double stopLoss)
        {
            // TODO: this is copy-paste, need to move to separate method

            // TODO: update code to have several opened orders
            // this is a temporal limitation to have only one opened order
            if (!(TotalValue == 0 ||
                (TotalValue < 0 && direction == 0) ||
                (TotalValue > 0 && direction == 1)))
            {
                return false;
            }

            // setup takeProfit and stopLoss
            if (TotalValue == 0)
            {
                if (direction == 0)
                {
                    TakeProfitPips = (int) ((takeProfit - price.sell) / PipSize);
                    StopLossPips = (int)((price.sell - stopLoss) / PipSize);
                }
                else
                {
                    TakeProfitPips = (int)((price.buy - takeProfit) / PipSize);
                    StopLossPips = (int)((stopLoss - price.buy) / PipSize);
                }
                localDatabaseConnector.WriteUserAction(
                    UserAction.PositionManuallyOpened,
                    string.Format("direction:{0}, price:{1}, tp:{2}, sl:{3}",
                                direction == 0 ? "buy" : "sell",
                                direction == 0 ? price.buy : price.sell,
                                TakeProfitPips,
                                StopLossPips));
            }

            TradesTotal++;
            TradesToday++;

            // profitability calculation
            var CurrentPrice = direction == 0 ? price.buy : price.sell;
            var CurrentDiff = 0.0;
            if (TotalValue == 0)
            {
                CurrentBalance = 0;
                TotalValue = 0;
                LastValue = direction == 0 ? 1 : -1;
            }
            else
            {
                CurrentDiff = (CurrentPrice - OpenedOrderPrice) * TotalValue * LotSize;
                LastValue = -TotalValue;
                localDatabaseConnector.WriteUserAction(
                    UserAction.PositionManuallyClosed,
                    string.Format("opened_direction:{0}, balance:{1:0.#####}", direction == 0 ? "sell" : "buy", CurrentDiff));
            }
            TotalValue += LastValue;
            OpenedOrderPrice = CurrentPrice;
            CurrentBalance += CurrentDiff;
            Profitability += CurrentDiff;

            // add trade to trades history
            var row = ProfitabilityHistory.NewRow();
            row["Time"] = DateTime.Now.ToString();
            row["Order"] = direction == 0 ? "Buy" : "Sell";
            row["Price"] = direction == 0 ? price.buyString : price.sellString;
            row["Value"] = LastValue.ToString();
            row["Lot size"] = LotSize.ToString();
            row["Balance"] = CurrentBalance.ToString("0.##");
            row["Profitability"] = Profitability.ToString("0.##");
            ProfitabilityHistory.Rows.Add(row);
            ProfitabilityHistory.AcceptChanges();

            if (TotalValue == 0)
            {
                CurrentBalance = 0;
            }

            DataRowInBehavioralModelsWindow["Profitability"] = Profitability.ToString("0.##");
            return true;
        }
    }
}
