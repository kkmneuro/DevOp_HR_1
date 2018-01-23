using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuroXChange.Model.BehavioralModeling.BehavioralModelCondition
{
    public class LogicQuery1Condition : AbstractBehavioralModelCondition
    {
        // we will look at trainingStep in the range [66,73]
        private const int subProtocolIdLeftBorder = 66;
        private const int subProtocolIdRigtBorder = 73;
        private const int subProtocolIdRangeCount = subProtocolIdRigtBorder - subProtocolIdLeftBorder + 1;
        private const int subProtocolFire = 74;

        private TimeSpan minuteSpan = TimeSpan.FromMinutes(1);

        // arrays that helps determine is it buy or sell condition
        private int[] buyIDs = { 66, 68, 71, 72 };
        private int[] sellIDs = { 67, 69, 70, 73 };

        // heart rate should be less than heartRateDown
        // or doesn't matter if heartRateDown == -1
        private int heartRateDown = 60;

        // heart rate should be less than heartRateDown
        // or doesn't matter if heartRateUp == -1
        private int heartRateUp = 100;

        // previous protocol ID that is not 74
        public int lastNot74SubProtocolID { get; private set; }

        // treat basic condition conversely
        private bool inverseCondition;

        private LinkedList<DateTime>[] higherHRElements;
        private LinkedList<DateTime>[] lowerHRElements;

        private int previousSubProtocol;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="heartRateUp"></param>
        /// <param name="heartRateDown"></param>
        /// <param name="doesNotCondition">where Sub-protocol_ID does not fulfill the condition</param>
        public LogicQuery1Condition(int heartRateUp, int heartRateDown, bool inverseCondition = false)
        {
            this.heartRateUp = heartRateUp;
            this.heartRateDown = heartRateDown;
            this.inverseCondition = inverseCondition;
            higherHRElements = new LinkedList<DateTime>[subProtocolIdRangeCount];
            lowerHRElements = new LinkedList<DateTime>[subProtocolIdRangeCount];
            for (int i = 0; i < subProtocolIdRangeCount; i++)
            {
                higherHRElements[i] = new LinkedList<DateTime>();
                lowerHRElements[i] = new LinkedList<DateTime>();
            }
        }

        public override void OnNext(BioData.BioData data)
        {
            // always true if inverse condition
            if (inverseCondition)
            {
                lastNot74SubProtocolID = -1;
                detailsData = 2;
                isConditionMet = true;
                return;
            }

            if (isConditionMet)
            {
                // reset global variables
                lastNot74SubProtocolID = -1;
            }

            isConditionMet = false;

            // prevent firing conditions several times in a row
            if (previousSubProtocol == data.trainingStep &&
                data.trainingStep == subProtocolFire)
            {
                return;
            }
            previousSubProtocol = data.trainingStep;

            if (subProtocolIdLeftBorder <= data.trainingStep
                && data.trainingStep <= subProtocolIdRigtBorder)
            {
                int id = data.trainingStep - subProtocolIdLeftBorder;
                if (heartRateUp == -1 || data.heartRate > heartRateUp)
                {
                    higherHRElements[id].AddLast(data.time);
                    UpdateExpired(higherHRElements[id], data.time);
                }
                if (heartRateDown == -1 || data.heartRate < heartRateDown)
                {
                    lowerHRElements[id].AddLast(data.time);
                    UpdateExpired(lowerHRElements[id], data.time);
                }
            }
            else if (data.trainingStep == subProtocolFire)
            {
                int bestId = -1;
                DateTime bestTime = DateTime.FromOADate(0);
                for (int id = 0; id < subProtocolIdRangeCount; id++)
                {
                    UpdateExpired(higherHRElements[id], data.time);
                    UpdateExpired(lowerHRElements[id], data.time);

                    DateTime compateTo = bestTime;

                    // need to check conditions bigger than heartRateUp
                    if (heartRateUp != -1)
                    {
                        if (higherHRElements[id].Count == 0)
                        {
                            continue;
                        }
                        compateTo = higherHRElements[id].Last.Value;
                    }

                    // need to check conditions less than heartRateDown
                    if (heartRateDown != -1)
                    {
                        if (lowerHRElements[id].Count == 0)
                        {
                            continue;
                        }
                        if (heartRateUp != -1)
                        {
                            if (higherHRElements[id].First.Value > lowerHRElements[id].Last.Value)
                            {
                                return;
                            }
                        }
                        compateTo = lowerHRElements[id].Last.Value;
                    }

                    // compare with best variant so far
                    if (bestId == -1 || bestTime < compateTo)
                    {
                        bestId = id;
                        bestTime = compateTo;
                    }
                }

                // found subprotocol_id from the range with HR met conditions
                if (!inverseCondition && bestId != -1)
                {
                    lastNot74SubProtocolID = bestId + subProtocolIdLeftBorder;

                    if (buyIDs.Contains(lastNot74SubProtocolID))
                    {
                        detailsData = 0;
                    }
                    else if (sellIDs.Contains(lastNot74SubProtocolID))
                    {
                        detailsData = 1;
                    }

                    isConditionMet = true;
                }
            }
        }

        private void UpdateExpired(LinkedList<DateTime> list, DateTime time)
        {
            while (list.Count > 0 && (time - list.First.Value) > minuteSpan)
            {
                list.RemoveFirst();
            }
        }
    }
}
