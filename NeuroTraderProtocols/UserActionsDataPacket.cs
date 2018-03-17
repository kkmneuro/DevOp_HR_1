using System;

namespace NeuroTraderProtocols
{
    [Serializable]
    public struct UserActionsData
    {
        public ulong ActionID;
        public double Time;
        public int Data;
    }

    [Serializable]
    public struct UserActionsDataPacket
    {
        public UserActionsData[] Actions;
    }
}
