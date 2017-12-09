using NeuroXChange.Common;
using NeuroXChange.Model.BehavioralModeling.BehavioralModelCondition;
using NeuroXChange.Model.BehavioralModeling.BehavioralModels;
using NeuroXChange.Model.BehavioralModeling.BioDataProcessors;
using NeuroXChange.Model.BehavioralModeling.Transitions;
using NeuroXChange.Model.BioData;
using NeuroXChange.Model.FixApi;
using System;
using System.Collections.Generic;
using System.Data;

namespace NeuroXChange.Model.BehavioralModeling
{
    public class BehavioralModelsContainer
    {
        // statistics that could be used for representation in UI
        public DataSet behavioralModelsDataSet { get; private set; }
        public string behavioralModelsDataTableName { get; private set; }
        private DataTable behavioralModelsDataTable;

        // processors of raw biological data
        public HeartRateProcessor heartRateProcessor { get; private set; }

        // behavioral model conditions properties
        public List<AbstractBehavioralModelCondition> conditions { get; private set; }

        // behavioral models properties
        public int BehavioralModelsCount { get; private set; }
        public SimpleBehavioralModel[] behavioralModels { get; private set; }

        private int activeBehavioralModelIndex;
        public int ActiveBehavioralModelIndex
        {
            get
            {
                return activeBehavioralModelIndex;
            }

            set
            {
                if (activeBehavioralModelIndex == value)
                {
                    return;
                }
                int oldActiveIndex = activeBehavioralModelIndex;
                activeBehavioralModelIndex = value;
                UpdateActiveTag(oldActiveIndex);
                UpdateActiveTag(ActiveBehavioralModelIndex);
            }
        }

        public BehavioralModelsContainer(IniFileReader iniFileReader)
        {
            // initialize order parameters
            int lotSize = Int32.Parse(iniFileReader.Read("LotSize", "Orders"));

            // initialize dataset to save statistics
            behavioralModelsDataSet = new DataSet("BehavioralModelsDataSet");
            behavioralModelsDataTableName = "BehavioralModels";
            behavioralModelsDataTable = behavioralModelsDataSet.Tables.Add(behavioralModelsDataTableName);
            DataColumn pkModelID =
                behavioralModelsDataTable.Columns.Add("Model", typeof(string));
            behavioralModelsDataTable.Columns.Add("State", typeof(string));
            behavioralModelsDataTable.Columns.Add("In position", typeof(string));
            behavioralModelsDataTable.Columns.Add("All trades", typeof(Int32));
            behavioralModelsDataTable.Columns.Add("Trades today", typeof(Int32));
            behavioralModelsDataTable.Columns.Add("Profitability", typeof(Double));
            behavioralModelsDataTable.PrimaryKey = new DataColumn[] { pkModelID };

            // initialize raw biological processors
            heartRateProcessor = new HeartRateProcessor();

            // initialize conditions
            var stepChangeStart = Double.Parse(iniFileReader.Read("StepChangeStart", "LogicConditions"));
            var stepChangeEnd = Double.Parse(iniFileReader.Read("StepChangeEnd", "LogicConditions"));
            //var accYCondition = new AccYCondition(stepChangeStart, stepChangeEnd);
            double MinOscillationsCount = Double.Parse(iniFileReader.Read("MinOscillationsCount", "LogicConditions"));
            double MaxOscillationsCount = Double.Parse(iniFileReader.Read("MaxOscillationsCount", "LogicConditions"));
            var hrReadyToTradeCondition = new HRReadyToTradeCondition(heartRateProcessor, MinOscillationsCount, MaxOscillationsCount);
            var hrPreactivationCondition = new HRPreactivationCondition(heartRateProcessor, MinOscillationsCount, MaxOscillationsCount);
            var logicQuery1Condition = new LogicQuery1Condition(100, 60);
            var logicQuery1ConditionV2 = new LogicQuery1Condition(100, -1);
            var logicQuery1ConditionV3 = new LogicQuery1Condition(100, 60, true);
            var logicQuery1ConditionV4 = new LogicQuery1Condition(-1, 60);
            var logicQuery2Condition = new LogicQuery2Condition();

            // add initialized conditions to List for more easy processing
            conditions = new List<AbstractBehavioralModelCondition>();
            //conditions.Add(accYCondition);
            conditions.Add(hrReadyToTradeCondition);
            conditions.Add(hrPreactivationCondition);
            conditions.Add(logicQuery1Condition);
            conditions.Add(logicQuery1ConditionV2);
            conditions.Add(logicQuery1ConditionV3);
            conditions.Add(logicQuery1ConditionV4);
            conditions.Add(logicQuery2Condition);

            // create transitions
            var hrReadyToTradeTransition = new ConditionalTransition(
                "Ready to trade",
                hrReadyToTradeCondition,
                BehavioralModelState.InitialState,
                BehavioralModelState.ReadyToTrade);
            var hrPreactivationTransition = new ConditionalTransition(
                "Preactivation",
                hrPreactivationCondition,
                BehavioralModelState.ReadyToTrade,
                BehavioralModelState.Preactivation);
            Func<bool> hrPreactivationNotMet = () => { return !hrPreactivationCondition.isConditionMet; };
            Func<bool> hrReadyToTradeNotMet = () => { return !hrReadyToTradeCondition.isConditionMet; };
            var hrPreactivationNotMetTransition = new FunctionalTransition(
                "Preactivation not met",
                hrPreactivationNotMet,
                BehavioralModelState.Preactivation | BehavioralModelState.DirectionConfirmed,
                BehavioralModelState.ReadyToTrade);
            var hrReadyToTradeNotMetTransition = new FunctionalTransition(
                "Ready to trade not met",
                hrReadyToTradeNotMet,
                BehavioralModelState.ReadyToTrade | BehavioralModelState.Preactivation | BehavioralModelState.DirectionConfirmed,
                BehavioralModelState.InitialState);
            var hrPreactivationNotMetTransitionV2 = new FunctionalTransition(
                "Preactivation not met v2",
                hrPreactivationNotMet,
                BehavioralModelState.Preactivation,
                BehavioralModelState.ReadyToTrade);
            var hrReadyToTradeNotMetTransitionV2 = new FunctionalTransition(
                "Ready to trade not met v2",
                hrReadyToTradeNotMet,
                BehavioralModelState.ReadyToTrade | BehavioralModelState.Preactivation,
                BehavioralModelState.InitialState);
            var directionConfirmedExpirationTransition = new DirectionConfirmedExpirationTransition("Time expired", TimeSpan.FromMinutes(15));
            var logicQuery1Transition = new LogicQuery1Transition("Logic query 1", logicQuery1Condition);
            var logicQuery1TransitionV2 = new LogicQuery1Transition("Logic query 1 v2", logicQuery1ConditionV2);
            var logicQuery1TransitionV3 = new LogicQuery1Transition("Logic query 1 v3", logicQuery1ConditionV3);
            var logicQuery1TransitionV4 = new LogicQuery1Transition("Logic query 1 v4", logicQuery1ConditionV4);
            var logicQuery2Transition = new LogicQuery2Transition("Logic query 2", logicQuery2Condition);
            var logicQuery2TransitionV2 = new LogicQuery2Transition("Logic query 2 v2", logicQuery2Condition, false);
            Func<bool> alwaysTrueFunction = () => { return true; };
            var executeOrderToConfirmationFilledTransition = new FunctionalTransition(
                "Execute order to confirmation filled",
                alwaysTrueFunction,
                BehavioralModelState.ExecuteOrder,
                BehavioralModelState.ConfirmationFilled);
            var confirmationFilledToInitialStateTransition = new FunctionalTransition(
                "Confirmation filled to initial state",
                alwaysTrueFunction,
                BehavioralModelState.ConfirmationFilled,
                BehavioralModelState.InitialState);

            // models 7+
            var hrReadyToTradeTransitionV2 = new FunctionalTransition(
                "Ready to trade v2",
                hrReadyToTradeNotMet,
                BehavioralModelState.InitialState,
                BehavioralModelState.ReadyToTrade);
            var hrReadyToTradeNotMetTransitionV3 = new ConditionalTransition(
                "Ready to trade not met v3",
                hrReadyToTradeCondition,
                BehavioralModelState.ReadyToTrade | BehavioralModelState.Preactivation | BehavioralModelState.DirectionConfirmed,
                BehavioralModelState.InitialState);
            var hrReadyToTradeNotMetTransitionV4 = new ConditionalTransition(
                "Ready to trade not met v4",
                hrReadyToTradeCondition,
                BehavioralModelState.ReadyToTrade | BehavioralModelState.Preactivation,
                BehavioralModelState.InitialState);


            // initialize behavioral models
            BehavioralModelsCount = 16;
            behavioralModels = new SimpleBehavioralModel[BehavioralModelsCount];

   
            // initialize all models with same conditions
            for (int i = 0; i < BehavioralModelsCount; i++)
            {
                var model = new SimpleBehavioralModel();
                behavioralModels[i] = model;

                if (i <= 5 || i == 12 || i == 13)
                {
                    model.transitions.Add(hrReadyToTradeTransition);
                }
                else
                {
                    model.transitions.Add(hrReadyToTradeTransitionV2);
                }

                model.transitions.Add(hrPreactivationTransition);

                if (i <= 5 || i == 12 || i == 13)
                {
                    if (i % 2 == 0)
                    {
                        model.transitions.Add(hrPreactivationNotMetTransition);
                        model.transitions.Add(hrReadyToTradeNotMetTransition);
                    }
                    else
                    {
                        model.transitions.Add(hrPreactivationNotMetTransitionV2);
                        model.transitions.Add(hrReadyToTradeNotMetTransitionV2);
                    }
                } else
                {
                    if (i % 2 == 0)
                    {
                        model.transitions.Add(hrPreactivationNotMetTransition);
                        model.transitions.Add(hrReadyToTradeNotMetTransitionV3);
                    }
                    else
                    {
                        model.transitions.Add(hrPreactivationNotMetTransitionV2);
                        model.transitions.Add(hrReadyToTradeNotMetTransitionV4);
                    }
                }

                model.transitions.Add(directionConfirmedExpirationTransition);

                if (i == 0 || i == 1 || i == 6 || i == 7)
                {
                    model.transitions.Add(logicQuery1Transition);
                    model.transitions.Add(logicQuery2Transition);
                }
                else if (i == 2 || i == 3 || i == 8 || i == 9)
                {
                    model.transitions.Add(logicQuery1TransitionV2);
                    model.transitions.Add(logicQuery2Transition);
                }
                else if (i == 4 || i == 5 || i == 10 || i == 11)
                {
                    model.transitions.Add(logicQuery1TransitionV3);
                    model.transitions.Add(logicQuery2TransitionV2);
                }
                else if (12 <= i && i <= 15)
                {
                    model.transitions.Add(logicQuery1TransitionV4);
                    model.transitions.Add(logicQuery2Transition);
                }

                model.transitions.Add(executeOrderToConfirmationFilledTransition);
                model.transitions.Add(confirmationFilledToInitialStateTransition);

                model.LotSize = lotSize;
            }


            // add datarow for each model
            for (int i = 0; i < BehavioralModelsCount; i++)
            {
                var row = behavioralModelsDataTable.NewRow();
                row["Model"] = i + 1;
                behavioralModels[i].DataRowInBehavioralModelsWindow = row;
                behavioralModelsDataTable.Rows.Add(row);
                behavioralModels[i].UpdateStatistics();
            }

            ActiveBehavioralModelIndex = Int32.Parse(iniFileReader.Read("ActiveModel", "BehavioralModels")) - 1;
            UpdateActiveTag(ActiveBehavioralModelIndex);
        }

        public void OnNext(BioData.BioData data)
        {
            // notify processors
            heartRateProcessor.OnNext(data);

            // notify conditions
            foreach (var condition in conditions)
            {
                condition.OnNext(data);
            }

            // notify models
            foreach (var model in behavioralModels)
            {
                model.OnNext(data);
            }
        }

        private TickPrice lastPrice = new TickPrice();
        public void OnNext(TickPrice price)
        {
            foreach (var model in behavioralModels)
            {
                model.OnNext(price);
            }
        }

        // update statistics for specific model
        private void UpdateActiveTag(int modelInd)
        {
            var modelIndStr = (modelInd + 1).ToString();
            if (modelInd == ActiveBehavioralModelIndex)
            {
                behavioralModels[modelInd].DataRowInBehavioralModelsWindow["Model"] = modelIndStr + " (active)";
            } else
            {
                behavioralModels[modelInd].DataRowInBehavioralModelsWindow["Model"] = modelIndStr;
            }
        }
    }
}
