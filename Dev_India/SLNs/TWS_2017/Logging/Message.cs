using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logging
{
    public class Message
    {
        internal LogType type;
        internal string txtmsg;
        DateTime RequestTime;
        public Message(LogType logtype, DateTime dt, string msg)
        {
            type = logtype;
            txtmsg = msg;
            RequestTime = dt;
        }
        public override string ToString()
        {
            return RequestTime.ToString("hh:mm:ss tt dd/MM/yyyy") + " => " + txtmsg;
        }
    }
    public enum LogType
    {
        INFO,
        ERROR,
        IN,
        OUT,
        IN_OUT,
        LOG
    }
}
