using NeuroXChange.Model.BehavioralModeling.BehavioralModelCondition;
using NeuroXChange.Model.BehavioralModeling.BioDataProcessors;
using NeuroXChange.Model.BioData;
using System.Collections.Generic;

namespace NeuroXChange.Model.BehavioralModeling
{
    public class BehavioralModelsContainer : IBioDataObserver
    {
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
        public int behavioralModelsCount { get; private set; }
        public AbstractBehavioralModel[] behavioralModels { get; private set; }
        public AbstractBehavioralModel activeBehavioralModel { get; private set; }

        public BehavioralModelsContainer()
        {
            // initialize raw biological processors
            heartRateProcessor = new HeartRateProcessor();

            // initialize conditions
            accYCondition = new AccYCondition();
            hrReadyToTradeCondition = new HRReadyToTradeCondition(heartRateProcessor);
            hrPreactivationCondition = new HRPreactivationCondition(heartRateProcessor);
            logicQuery1Condition = new LogicQuery1Condition();
            logicQuery2Condition = new LogicQuery2Condition();

            // add initialized conditions to List for more easy processing
            conditions = new List<AbstractBehavioralModelCondition>();
            conditions.Add(accYCondition);
            conditions.Add(hrReadyToTradeCondition);
            conditions.Add(hrPreactivationCondition);
            conditions.Add(logicQuery1Condition);
            conditions.Add(logicQuery2Condition);

            // initialize behavioral models
            behavioralModelsCount = 15;
            behavioralModels = new AbstractBehavioralModel[behavioralModelsCount];
            for (int i = 0; i < behavioralModelsCount; i++)
            {
                behavioralModels[i] = 
                    new BehavioralModel1(
                        accYCondition,
                        hrReadyToTradeCondition,
                        hrPreactivationCondition,
                        logicQuery1Condition,
                        logicQuery2Condition);
            }
            activeBehavioralModel = behavioralModels[0];
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
    }
}
