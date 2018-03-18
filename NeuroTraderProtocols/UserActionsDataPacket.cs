using System;

namespace NeuroTraderProtocols
{
    [Serializable]
    public struct UserActionsData
    {
        public int ActionID;
        public double Time;
        public long Data;
    }

    [Serializable]
    public struct UserActionsDataPacket
    {
        public UserActionsData[] Actions;
    }
}
