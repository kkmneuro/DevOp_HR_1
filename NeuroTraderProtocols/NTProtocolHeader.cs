﻿namespace NeuroTraderProtocols
{
    public enum NTProtocolHeader : byte
    {
        // first header of communidation
        General = 7,

        // main block headers
        Authorisation = 15,         // client sends login/password, server sends answer
        RequestStatistics = 16,     // client sends stats request, server sends answer
        NewData = 17                // client sends data, server, doesn't send anything
    }
}
