using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroXChange.Model.BioData
{
    public struct Sub_Component_Protocol_Psychophysiological_Session_Data_TPS
    {
        public int psychophysiological_Session_Data_ID;
        public DateTime time;
        public double temperature;
        public double hartRate;
        public double skinConductance;
        public double accX;
        public double accY;
        public double accZ;
        public int session_Component_ID;
        public int sub_Component_ID;
        public int sub_Component_Protocol_ID;
        public int sub_Protocol_ID;
        public int participant_ID;
        public string data;
    }
}
