using System.Linq;

namespace NeuroXChange.Model.BehavioralModeling.BehavioralModelCondition
{
    public class LogicQuery1Condition : AbstractBehavioralModelCondition
    {
        // hart rate const
        private int maxHeartRate = 60;

        // arrays that helps determine is it buy or sell condition
        private int[] buyIDs = { 66, 68, 71, 72 };
        private int[] sellIDs = { 67, 69, 70, 73 };

        // previous protocol ID that is not 74
        private int lastNot74SubProtocolID = -1;

        public LogicQuery1Condition(int maxHeartRate)
        {
            this.maxHeartRate = maxHeartRate;
        }

        public override void OnNext(BioData.BioData data)
        {
            isConditionMet = false;

            if (data.sub_Protocol_ID != 74 && data.hartRate < maxHeartRate)
            {
                lastNot74SubProtocolID = data.sub_Protocol_ID;
            }

            if (data.sub_Protocol_ID == 74
                && 65 <= lastNot74SubProtocolID
                && lastNot74SubProtocolID < 74)
            {
                if (buyIDs.Contains(lastNot74SubProtocolID))
                {
                    detailsData = 0;
                }
                else if (sellIDs.Contains(lastNot74SubProtocolID))
                {
                    detailsData = 1;
                }

                isConditionMet = true;

                // reset global variables
                lastNot74SubProtocolID = -1;
            }
        }
    }
}
