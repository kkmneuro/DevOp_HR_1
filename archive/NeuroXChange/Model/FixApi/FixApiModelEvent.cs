using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroXChange.Model.FixApi
{
    public enum FixApiModelEvent
    {
        RawMessageReceived,
        LoginMessage,
        PriceChanged
    }
}
