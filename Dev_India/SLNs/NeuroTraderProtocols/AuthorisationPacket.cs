using System;

namespace NeuroTraderProtocols
{
    [Serializable]
    public class AuthorisationPacket
    {
        public string Login;
        public string Password;
    }
}
