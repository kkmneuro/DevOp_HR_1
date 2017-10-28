using NeuroXChange.Common;
using NeuroXChange.Model.BehavioralModeling.BehavioralModelCondition;
using NeuroXChange.Model.BehavioralModeling.BioDataProcessors;
using NeuroXChange.Model.BioData;
using System;
using System.Collections.Generic;
using System.Data;

namespace NeuroXChange.Model.BehavioralModeling
{
    public class BehavioralModelsContainer : IBioDataObserver
    {
        // statistics that could be used for representation in UI
        public DataSet behavioralModelsDataSet { get; private set; }
        public string behavioralModelsDataTableName { get; private set; }
        private DataTable behavioralModelsDataTable;

        // processors of raw biological data
        public HeartRateProcessor heartRateProcessor { get; private set; }

        // behavioral model conditions properties
        public List<AbstractBehavioralModelCondition> conditions { get; private set; }
        public AccYCondition accYCondition { get; private set; }
        public HRReadyToTradeCondition hrReadyToTradeCondition { get; private set; }
        public HRPreactivationCondition hrPreactivationCondition { get; private set; }
        public LogicQuery1Condition logicQuery1Condition { get; private set; }
        public LogicQuery2Condition logicQuery2Condition { get; private set; }

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
            accYCondition = new AccYCondition(stepChangeStart, stepChangeEnd);

            double MinOscillationsCount = Double.Parse(iniFileReader.Read("MinOscillationsCount", "LogicConditions"));
            double MaxOscillationsCount = Double.Parse(iniFileReader.Read("MaxOscillationsCount", "LogicConditions"));

            hrReadyToTradeCondition = new HRReadyToTradeCondition(heartRateProcessor, MinOscillationsCount, MaxOscillationsCount);

            hrPreactivationCondition = new HRPreactivationCondition(heartRateProcessor, MinOscillationsCount, MaxOscillationsCount);

            logicQuery1Condition = new LogicQuery1Condition(100, 60);

            logicQuery2Condition = new LogicQuery2Condition();

            // initialize specific models/conditions variants
            Func<bool> hrPreactivationMet = () => { return hrPreactivationCondition.isConditionMet; };
            Func<bool> hrPreactivationNotMet = () => { return !hrPreactivationCondition.isConditionMet; };

            // add initialized conditions to List for more easy processing
            conditions = new List<AbstractBehavioralModelCondition>();
            conditions.Add(accYCondition);
            conditions.Add(hrReadyToTradeCondition);
            conditions.Add(hrPreactivationCondition);
            conditions.Add(logicQuery1Condition);
            conditions.Add(logicQuery2Condition);

            // initialize behavioral models
            BehavioralModelsCount = 16;
            behavioralModels = new SimpleBehavioralModel[BehavioralModelsCount];

            // initialize all models with same conditions
            for (int i = 0; i < BehavioralModelsCount; i++)
            {
                var model = new SimpleBehavioralModel(
                    null, hrReadyToTradeCondition, hrPreactivationCondition,
                    logicQuery1Condition, logicQuery2Condition);
                behavioralModels[i] = model;

                model.LQ1SubCondition = hrPreactivationMet;
                model.LQ2SubCondition = i % 2 == 0 ? hrPreactivationMet : hrPreactivationNotMet;
                model.DirectionConfirmedExpirationTime = TimeSpan.FromMinutes(15);
                model.MoveBackIfHROscNotMet = true;
            }

            // add datarow for each model
            for (int i = 0; i < BehavioralModelsCount; i++)
            {
                var row = behavioralModelsDataTable.NewRow();
                row["Model"] = i + 1;
                behavioralModels[i].dataRow = row;
                behavioralModelsDataTable.Rows.Add(row);
                behavioralModels[i].UpdateStatistics();
            }

            ActiveBehavioralModelIndex = 0;
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

        // update statistics for specific model
        private void UpdateActiveTag(int modelInd)
        {
            var modelIndStr = (modelInd + 1).ToString();
            if (modelInd == ActiveBehavioralModelIndex)
            {
                behavioralModels[modelInd].dataRow["Model"] = modelIndStr + " (active)";
            } else
            {
                behavioralModels[modelInd].dataRow["Model"] = modelIndStr;
            }
        }
    }
}
