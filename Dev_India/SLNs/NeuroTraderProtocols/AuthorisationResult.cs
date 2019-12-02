namespace NeuroTraderProtocols
{
    public enum AuthorisationResult : byte 
    {
        UnknownError = 199,
        Ok = 200,
        ProtocolError = 201,
        NoSuchUser = 202,
        WrongPassword = 203
    }
}
