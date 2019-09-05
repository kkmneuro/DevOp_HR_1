using System;

namespace NeuroTraderProtocols
{
    [Serializable]
    public struct BioDataPricePacket
    {
        public double[] Time;
        public float[] Temperature;
        public float[] HeartRate;
        public float[] SkinConductance;
        public short[] TrainingType;
        public short[] TrainingStep;
        public int[] ApplicationStates;

        public float[] BuyPrice;
        public float[] SellPrice;
    }
}
