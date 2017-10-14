using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroXChange.Model.BioData
{
    public struct BioData
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

        public static BioData FromOleDbDataReader(OleDbDataReader reader)
        {
            var data = new BioData();
            data.psychophysiological_Session_Data_ID = Int32.Parse(reader["Psychophysiological_Session_Data_ID"].ToString());
            data.time = DateTime.Parse(reader["Time"].ToString());
            data.temperature = Double.Parse(reader["Temperature"].ToString());
            data.hartRate = Double.Parse(reader["HartRate"].ToString());
            data.skinConductance = Double.Parse(reader["SkinConductance"].ToString());
            data.accX = Double.Parse(reader["AccX"].ToString());
            data.accY = Double.Parse(reader["AccY"].ToString());
            data.accZ = Double.Parse(reader["AccZ"].ToString());
            data.session_Component_ID = Int32.Parse(reader["Session_Component_ID"].ToString());
            data.sub_Component_ID = Int32.Parse(reader["Sub_Component_ID"].ToString());
            data.sub_Component_Protocol_ID = Int32.Parse(reader["Sub_Component_Protocol_ID"].ToString());
            data.sub_Protocol_ID = Int32.Parse(reader["Sub_Protocol_ID"].ToString());
            data.participant_ID = Int32.Parse(reader["Participant_ID"].ToString());
            data.data = reader["Data"].ToString();
            return data;
        }
    }
}
