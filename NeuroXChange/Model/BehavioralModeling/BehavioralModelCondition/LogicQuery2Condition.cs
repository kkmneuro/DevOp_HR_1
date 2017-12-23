using System;
using NeuroXChange.Model.BioData;

namespace NeuroXChange.Model.BehavioralModeling.BehavioralModelCondition
{
    public class LogicQuery2Condition : AbstractBehavioralModelCondition
    {
        private TimeSpan minuteSpan = TimeSpan.FromMinutes(1);

        private DateTime? lq2LastHeartRateBigger100 = null;
        private DateTime? lq2LastHeartRateSmaller60 = null;

        public override void OnNext(BioData.BioData data)
        {
            isConditionMet = false;

            if (lq2LastHeartRateBigger100.HasValue && (data.time - lq2LastHeartRateBigger100.Value) > minuteSpan)
            {
                lq2LastHeartRateBigger100 = null;
            }
            if (lq2LastHeartRateSmaller60.HasValue && (data.time - lq2LastHeartRateSmaller60.Value) > minuteSpan)
            {
                lq2LastHeartRateSmaller60 = null;
            }

            if (lq2LastHeartRateBigger100.HasValue && data.heartRate < 60)
            {
                lq2LastHeartRateBigger100 = null;
                isConditionMet = true;
                detailsData = 0;
            }
            if (lq2LastHeartRateSmaller60.HasValue && data.heartRate > 100)
            {
                lq2LastHeartRateSmaller60 = null;
                isConditionMet = true;
                detailsData = 1;
            }

            if (data.heartRate < 60)
            {
                lq2LastHeartRateSmaller60 = data.time;
            }
            if (data.heartRate > 100)
            {
                lq2LastHeartRateBigger100 = data.time;
            }
        }
    }
}
