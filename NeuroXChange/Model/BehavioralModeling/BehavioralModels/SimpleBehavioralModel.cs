using NeuroXChange.Model.BehavioralModeling.BehavioralModelCondition;

namespace NeuroXChange.Model.BehavioralModeling
{
    public class SimpleBehavioralModel : AbstractBehavioralModel
    {
        public SimpleBehavioralModel(
            AccYCondition accYCondition,
            HRReadyToTradeCondition hrReadyToTradeCondition,
            HRPreactivationCondition hrPreactivationCondition,
            AbstractBehavioralModelCondition logicQuery1Condition,
            AbstractBehavioralModelCondition logicQuery2Condition)
                :base(accYCondition, hrReadyToTradeCondition, hrPreactivationCondition,
                    logicQuery1Condition, logicQuery2Condition)
        {

        }

        public override void OnNext(BioData.BioData data)
        {
            base.OnNext(data);
        }
    }
}
