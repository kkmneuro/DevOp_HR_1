using System;
using NeuroXChange.Model.BioData;

namespace NeuroXChange.Model.BehavioralModeling.BehavioralModelCondition
{
    public class LogicQuery2Condition : AbstractBehavioralModelCondition
    {
        private TimeSpan minute2Span = TimeSpan.FromMinutes(2);

        private DateTime? lq2LastHeartRateBigger100 = null;
        private DateTime? lq2LastHeartRateSmaller60 = null;

        private TimeSpan sec5Span = TimeSpan.FromSeconds(5);
        private DateTime? lq2LastHeartRateBigger100_5sec = null;
        private DateTime? lq2LastHeartRateSmaller60_5sec = null;

        public override void OnNext(BioData.BioData data)
        {
            isConditionMet = false;

            if (lq2LastHeartRateBigger100.HasValue && (data.time - lq2LastHeartRateBigger100.Value) > minute2Span)
            {
                lq2LastHeartRateBigger100 = null;
            }
            if (lq2LastHeartRateSmaller60.HasValue && (data.time - lq2LastHeartRateSmaller60.Value) > minute2Span)
            {
                lq2LastHeartRateSmaller60 = null;
            }
            if (lq2LastHeartRateBigger100_5sec.HasValue && (data.time - lq2LastHeartRateBigger100_5sec.Value) > sec5Span)
            {
                lq2LastHeartRateBigger100_5sec = null;
            }
            if (lq2LastHeartRateSmaller60_5sec.HasValue && (data.time - lq2LastHeartRateSmaller60_5sec.Value) > sec5Span)
            {
                lq2LastHeartRateSmaller60_5sec = null;
            }


            if (lq2LastHeartRateBigger100.HasValue && data.heartRate < 60)
            {
                lq2LastHeartRateBigger100 = null;
                isConditionMet = true;
                detailsData = 0;
            } else
            if (lq2LastHeartRateSmaller60.HasValue && data.heartRate > 100)
            {
                lq2LastHeartRateSmaller60 = null;
                isConditionMet = true;
                detailsData = 1;
            } else
            if (lq2LastHeartRateBigger100_5sec.HasValue &&
                lq2LastHeartRateSmaller60_5sec.HasValue &&
                data.heartRate > 100 &&
                lq2LastHeartRateBigger100_5sec.Value < lq2LastHeartRateSmaller60_5sec.Value)
            {
                lq2LastHeartRateBigger100_5sec = null;
                lq2LastHeartRateSmaller60_5sec = null;
                isConditionMet = false;
                detailsData = -1;
            }

            if (data.heartRate < 60)
            {
                lq2LastHeartRateSmaller60 = data.time;
                lq2LastHeartRateSmaller60_5sec = data.time;
            }
            if (data.heartRate > 100)
            {
                lq2LastHeartRateBigger100 = data.time;
                lq2LastHeartRateBigger100_5sec = data.time;
            }
        }
    }
}
