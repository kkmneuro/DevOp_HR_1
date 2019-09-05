using System;
using System.Collections.Generic;

namespace NeuroXChange.Model.BehavioralModeling.BioDataProcessors
{
    public class HeartRateProcessor : AbstractBioDataProcessor
    {
        // states transition:
        // HR is greater than HRHigh: InitialState -> InitialHigher
        // HR is less than HRLow: InitialHigher -> Lower
        // HR is greater than HRLow: Lower -> Higher
        private enum HRState
        {
            InitialState,
            InitialHigher,
            LowerThanAverage,
            HigherThanAverage
        }

        public HeartRateInfo heartRateInfo { get; private set; }

        // for average heart rate calculation (over 2 minutes)
        private TimeSpan heartRateAverageTime = TimeSpan.FromMinutes(2);
        private Queue<BioData.BioData> lastBioData = new Queue<BioData.BioData>();
        private double heartRateSum = 0.0;
        private bool lastBioData2minReached = false;

        // HR states transition
        private HRState lastHRState = HRState.InitialState;

        // last 5 minutes timestamps of HRstates when it changes from HigherThanAverage to LowerThanAverage
        private List<DateTime> lastStatesChanges = new List<DateTime>();
        private TimeSpan lastStatesChangesHistoryTime = TimeSpan.FromMinutes(5);

        public HeartRateProcessor()
        {
            this.heartRateInfo = new HeartRateInfo();
            HeartRateInfo heartRateInfo = this.heartRateInfo;
            heartRateInfo.heartRate2minAverage = -1.0;
            heartRateInfo.heartRateInnerState = HRState.InitialState.ToString();
            heartRateInfo.oscillations1minAverage = -1.0;
            heartRateInfo.oscillations3minAverage = -1.0;
            heartRateInfo.oscillations5minAverage = -1.0;
            this.heartRateInfo = heartRateInfo;
        }

        public override void OnNext(BioData.BioData data)
        {
            HeartRateInfo heartRateInfo = this.heartRateInfo;

            double heartRateNow = data.heartRate;

            // update HR average for the last 2 minutes
            while (lastBioData.Count > 0 && (data.time - lastBioData.Peek().time) > heartRateAverageTime)
            {
                lastBioData2minReached = true;
                BioData.BioData oldData = lastBioData.Dequeue();
                heartRateSum -= oldData.heartRate;
            }

            if (lastBioData.Count < 200)
            {
                lastBioData2minReached = false;
            }
            heartRateSum += data.heartRate;
            lastBioData.Enqueue(data);

            // remove obsolete elements of lastStatesChanges
            while (lastStatesChanges.Count > 0 && (data.time - lastStatesChanges[0]) > lastStatesChangesHistoryTime)
            {
                lastStatesChanges.RemoveAt(0);
            }

            double HRAverage = -1;
            HRState newHRState = HRState.InitialState;
            if (lastBioData2minReached)
            {
                HRAverage = heartRateSum / lastBioData.Count;

                // HRState
                newHRState = lastHRState;
                double HRAverageHigh = HRAverage * 1.0025;
                double HRAverageLow = HRAverage * 0.9975;
                if (heartRateNow > HRAverageHigh)
                {
                    if (lastHRState == HRState.InitialState)
                    {
                        newHRState = HRState.InitialHigher;
                    }
                    else
                    {
                        newHRState = HRState.HigherThanAverage;
                    }
                } else if (heartRateNow < HRAverageLow)
                {
                    if (lastHRState == HRState.HigherThanAverage)
                    {
                        newHRState = HRState.LowerThanAverage;
                    }
                }

                // HRState changes from higher to lower
                if (lastHRState == HRState.HigherThanAverage && newHRState == HRState.LowerThanAverage)
                {
                    lastStatesChanges.Add(data.time);

                    // get precise interval in seconds from oldest state change point to current state change point
                    var interval5min = (data.time - lastStatesChanges[0]).TotalSeconds;
                    if (interval5min > 30 && lastStatesChanges.Count > 1)
                    {
                        heartRateInfo.oscillations5minAverage = 60 / (interval5min / (lastStatesChanges.Count - 1));
                    }

                    int firstIn3min = 0;
                    while (firstIn3min < lastStatesChanges.Count && (data.time - lastStatesChanges[firstIn3min]) > TimeSpan.FromMinutes(3))
                    {
                        firstIn3min++;
                    }
                    if (firstIn3min + 1 < lastStatesChanges.Count)
                    {
                        var interval3min = (data.time - lastStatesChanges[firstIn3min]).TotalSeconds;
                        if (interval3min > 30)
                        {
                            heartRateInfo.oscillations3minAverage = 60 / (interval3min / (lastStatesChanges.Count - firstIn3min - 1));
                        }
                    }

                    int firstIn1min = firstIn3min;
                    while (firstIn1min < lastStatesChanges.Count && (data.time - lastStatesChanges[firstIn1min]) > TimeSpan.FromMinutes(1))
                    {
                        firstIn1min++;
                    }
                    if (firstIn1min + 1 < lastStatesChanges.Count)
                    {
                        var interval1min = (data.time - lastStatesChanges[firstIn1min]).TotalSeconds;
                        if (interval1min > 30)
                        {
                            heartRateInfo.oscillations1minAverage = 60 / (interval1min / (lastStatesChanges.Count - firstIn1min - 1));
                        }
                    }
                }
            }
            else
            {
                heartRateInfo.oscillations1minAverage = -1.0;
                heartRateInfo.oscillations3minAverage = -1.0;
                heartRateInfo.oscillations5minAverage = -1.0;
            }
            lastHRState = newHRState;

            // send HeartRateRawStatistics event
            heartRateInfo.time = data.time;
            heartRateInfo.heartRate2minAverage = HRAverage;
            heartRateInfo.heartRateInnerState = lastHRState.ToString();

            this.heartRateInfo = heartRateInfo;
        }
    }
}
