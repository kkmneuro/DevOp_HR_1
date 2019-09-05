using NeuroXChange.Common;
using NeuroXChange.Model.BehavioralModeling.BehavioralModels;
using NeuroXChange.Model.BehavioralModeling.BioDataProcessors;
using NeuroXChange.Model.BioData;
using NeuroXChange.Model.FixApi;
using System;
using System.Collections.Generic;
using System.Data;
#if !SIMPLEST
using NeuroXChange.Model.BehavioralModeling.BehavioralModelCondition;
using NeuroXChange.Model.BehavioralModeling.Transitions;
#endif

namespace NeuroXChange.Model.BehavioralModeling
{
    public class BehavioralModelsContainer
    {
        private MainNeuroXModel mainNeuroXModel;

        // statistics that could be used for representation in UI
        public DataSet behavioralModelsDataSet { get; private set; }
        public string behavioralModelsDataTableName { get; private set; }
        private DataTable behavioralModelsDataTable;

        // processors of raw biological data
        public HeartRateProcessor heartRateProcessor { get; private set; }

#if !SIMPLEST
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
                // TODO: refactor working with manual trading model
                if (value == 16)
                {
                    return;
                }
                int oldActiveIndex = activeBehavioralModelIndex;
                activeBehavioralModelIndex = value;
                UpdateActiveTag(oldActiveIndex);
                UpdateActiveTag(ActiveBehavioralModelIndex);
            }
        }
#endif


        public BehavioralModelsContainer(MainNeuroXModel mainNeuroXModel, IniFileReader iniFileReader)
        {
            this.mainNeuroXModel = mainNeuroXModel;

            // initialize order parameters
            int lotSize = Int32.Parse(iniFileReader.Read("LotSize", "MarketOrders", "100000"));
            int? stopLossPips = Int32.Parse(iniFileReader.Read("StopLossPips", "MarketOrders", "60"));
            int? takeProfitPips = Int32.Parse(iniFileReader.Read("TakeProfitPips", "MarketOrders", "100"));
            double pipSize = StringHelpers.ParseDoubleCultureIndependent(iniFileReader.Read("PipSize", "MarketOrders", "0.00001"));

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

#if !SIMPLEST

            // initialize conditions
            var stepChangeStart = StringHelpers.ParseDoubleCultureIndependent(iniFileReader.Read("StepChangeStart", "LogicConditions", "-60"), true);
            var stepChangeEnd = StringHelpers.ParseDoubleCultureIndependent(iniFileReader.Read("StepChangeEnd", "LogicConditions", "-20"), true);
            //var accYCondition = new AccYCondition(stepChangeStart, stepChangeEnd);
            double MinOscillationsCount = StringHelpers.ParseDoubleCultureIndependent(iniFileReader.Read("MinOscillationsCount", "LogicConditions", "5"));
            double MaxOscillationsCount = StringHelpers.ParseDoubleCultureIndependent(iniFileReader.Read("MaxOscillationsCount", "LogicConditions", "6.5"));
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
            Func<bool> hrPreactivationMet = () => { return hrPreactivationCondition.isConditionMet; };
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
            var logicQuery2TransitionPreactMet = new LogicQuery2Transition("Logic query 2 HR met", logicQuery2Condition, hrPreactivationMet);
            var logicQuery2TransitionPreactNotMet = new LogicQuery2Transition("Logic query 2 HR not met", logicQuery2Condition, hrPreactivationNotMet);
            var logicQuery2TransitionV2PreactMet = new LogicQuery2Transition("Logic query 2 v2 HR met", logicQuery2Condition, hrPreactivationMet, false);
            var logicQuery2TransitionV2PreactNotMet = new LogicQuery2Transition("Logic query 2 v2 HR not met", logicQuery2Condition, hrPreactivationNotMet, false);
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
            BehavioralModelsCount = 17;
            behavioralModels = new SimpleBehavioralModel[BehavioralModelsCount];

   
            // initialize all models with same conditions
            for (int i = 0; i < BehavioralModelsCount; i++)
            {
                var model = new SimpleBehavioralModel(i+1, mainNeuroXModel.localDatabaseConnector);
                behavioralModels[i] = model;

                // set constants
                model.portfolio.DefaultLotSize = lotSize;
                model.portfolio.DefaultStopLossPips = stopLossPips;
                model.portfolio.DefaultTakeProfitPips = takeProfitPips;
                model.portfolio.DefaultPipSize = pipSize;

                // model 16 (17th if numerated from 1) is manual trading model
                // TODO: refactor
                if (i == 16)
                {
                    break;
                }

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
                }
                else if (i == 2 || i == 3 || i == 8 || i == 9)
                {
                    model.transitions.Add(logicQuery1TransitionV2);
                }
                else if (i == 4 || i == 5 || i == 10 || i == 11)
                {
                    model.transitions.Add(logicQuery1TransitionV3);
                }
                else if (12 <= i && i <= 15)
                {
                    model.transitions.Add(logicQuery1TransitionV4);
                }

                if (i != 4 && i != 5 && i != 10 && i != 11)
                {
                    if (i % 2 ==0)
                        model.transitions.Add(logicQuery2TransitionPreactMet);
                    else
                        model.transitions.Add(logicQuery2TransitionPreactNotMet);
                }
                else
                {
                    if (i % 2 == 0)
                        model.transitions.Add(logicQuery2TransitionV2PreactMet);
                    else
                        model.transitions.Add(logicQuery2TransitionV2PreactNotMet);
                }

                model.transitions.Add(executeOrderToConfirmationFilledTransition);
                model.transitions.Add(confirmationFilledToInitialStateTransition);
            }


            // add datarow for each model
            for (int i = 0; i < BehavioralModelsCount; i++)
            {
                var row = behavioralModelsDataTable.NewRow();
                row["Model"] = i < 16 ? (i + 1).ToString() : "Manual";
                behavioralModels[i].DataRowInBehavioralModelsWindow = row;
                behavioralModelsDataTable.Rows.Add(row);
                behavioralModels[i].UpdateStatistics();
            }

            ActiveBehavioralModelIndex = Int32.Parse(iniFileReader.Read("ActiveModel", "BehavioralModels", "13")) - 1;
            UpdateActiveTag(ActiveBehavioralModelIndex);

            LoadTradesHistory();
#endif
        }

        public void OnNext(BioData.BioData data)
        {
            // notify processors
            heartRateProcessor.OnNext(data);

#if !SIMPLEST
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
#endif
        }

        private TickPrice lastPrice = new TickPrice();
        public void OnNext(TickPrice price)
        {
#if !SIMPLEST
            foreach (var model in behavioralModels)
            {
                model.OnNext(price);
            }
#endif
        }

#if !SIMPLEST
        // update statistics for specific model
        private void UpdateActiveTag(int modelInd)
        {
            // TODO: refactor working with manual trade model
            if (modelInd >= 16)
            {
                return;
            }

            var modelIndStr = (modelInd + 1).ToString();
            if (modelInd == ActiveBehavioralModelIndex)
            {
                behavioralModels[modelInd].DataRowInBehavioralModelsWindow["Model"] = modelIndStr + " (active)";
            } else
            {
                behavioralModels[modelInd].DataRowInBehavioralModelsWindow["Model"] = modelIndStr;
            }
        }

        private void LoadTradesHistory()
        {
            var history = mainNeuroXModel.localDatabaseConnector.LoadTradesHistory();
            foreach (var order in history)
            {
                behavioralModels[order.BMModelID - 1].portfolio.AddHistoryOrder(order);
            }
        }
#endif
    }
}
