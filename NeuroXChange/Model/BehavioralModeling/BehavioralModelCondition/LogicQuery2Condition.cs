using System;
using NeuroXChange.Model.BioData;

namespace NeuroXChange.Model.BehavioralModeling.BehavioralModelCondition
{
    public class LogicQuery2Condition : AbstractBehavioralModelCondition
    {
        private TimeSpan minuteSpan = TimeSpan.FromMinutes(1);

        private DateTime? lq2LastHartRateBigger100 = null;
        private DateTime? lq2LastHartRateSmaller60 = null;

        private AbstractBehavioralModelCondition subCondition;
        private bool subConditionShouldMet;

        public LogicQuery2Condition(
            AbstractBehavioralModelCondition subCondition,
            bool subConditionShouldMet)
        {
            this.subCondition = subCondition;
            this.subConditionShouldMet = subConditionShouldMet;
        }

        public override void OnNext(BioData.BioData data)
        {
            isConditionMet = false;

            if (lq2LastHartRateBigger100.HasValue && (data.time - lq2LastHartRateBigger100.Value) > minuteSpan)
            {
                lq2LastHartRateBigger100 = null;
            }
            if (lq2LastHartRateSmaller60.HasValue && (data.time - lq2LastHartRateSmaller60.Value) > minuteSpan)
            {
                lq2LastHartRateSmaller60 = null;
            }

            if (subCondition == null || (subCondition.isConditionMet == subConditionShouldMet))
            {
                if (lq2LastHartRateBigger100.HasValue && data.hartRate < 60)
                {
                    lq2LastHartRateBigger100 = null;
                    isConditionMet = true;
                    detailsData = 0;
                }
                if (lq2LastHartRateSmaller60.HasValue && data.hartRate > 100)
                {
                    lq2LastHartRateSmaller60 = null;
                    isConditionMet = true;
                    detailsData = 1;
                }
            }

            if (data.hartRate < 60)
            {
                lq2LastHartRateSmaller60 = data.time;
            }
            if (data.hartRate > 100)
            {
                lq2LastHartRateBigger100 = data.time;
            }
        }
    }
}
