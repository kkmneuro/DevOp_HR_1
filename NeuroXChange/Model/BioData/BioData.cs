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
        public int sub_Component_Protocol_ID;
        public int sub_Protocol_ID;

        // implementation dependent payload
        public object payload;

        public static BioData FromOleDbDataReader(OleDbDataReader reader, bool hasPrice = false)
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
            data.sub_Component_Protocol_ID = Int32.Parse(reader["Sub_Component_Protocol_ID"].ToString());
            data.sub_Protocol_ID = Int32.Parse(reader["Sub_Protocol_ID"].ToString());
            if (hasPrice)
            {
                data.payload = new string[] {reader["SellPrice"].ToString(), reader["BuyPrice"].ToString() };
            }
            else
            {
                data.payload = null;
            }
            return data;
        }
    }
}
