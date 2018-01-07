﻿using NeuroXChange.Model.BehavioralModeling.BehavioralModels;
using NeuroXChange.Model.BehavioralModeling.Transitions;
using NeuroXChange.Model.Database;
using NeuroXChange.Model.FixApi;
using NeuroXChange.Model.Portfolio;
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

        public Portfolio.Portfolio portfolio;

        // some variables
        public int lq1OrderDirection { get; set; }
        public int lq2OrderDirection { get; set; }
        public DateTime previousTransitionToDirectionConfirmed { get; private set; }
        public int lastNot74SubProtocolID { get; set; }

        public LinkedList<TransitionHistoryItem> TransitionHistory { get; private set; }
        public LocalDatabaseConnector localDatabaseConnector { get; set;}

        // private variables
        private TickPrice LastPrice;

        public SimpleBehavioralModel()
        {
            PreviousTickState = BehavioralModelState.InitialState;
            CurrentTickState = BehavioralModelState.InitialState;
            transitions = new List<AbstractTransition>();
            TransitionHistory = new LinkedList<TransitionHistoryItem>();

            portfolio = new Portfolio.Portfolio();
        }

        public virtual void OnNext(BioData.BioData data)
        {
            PreviousTickState = CurrentTickState;
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
                    Order order;
                    portfolio.OpenOrder(OrderDirection, LastPrice, out order);
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
            portfolio.OnNext(price);
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
            //DataRowInBehavioralModelsWindow["All trades"] = TradesTotal;
            //DataRowInBehavioralModelsWindow["Trades today"] = TradesToday;
            //DataRowInBehavioralModelsWindow["Profitability"] = Profitability.ToString("0.##");
        }

        public bool ManualTrade(
            int direction, TickPrice price, double takeProfitValue, double stopLossValue)
        {
            Order order;
            var result = portfolio.OpenOrder(direction, price, out order);
            if (order != null)
            {
                order.TakeProfitPips = portfolio.TakeProfitValueToPips(direction, price, takeProfitValue);
                order.StopLossPips = portfolio.StopLossValueToPips(direction, price, stopLossValue);
            }
            return result;
        }
    }
}
