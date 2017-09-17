using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NeuroXChange.Model.BioData
{
    public class MSAccessBioDataProvider : AbstractBioDataProvider
    {
        private OleDbConnection conn;
        Thread thread;

        public MSAccessBioDataProvider(string fileName)
        {
            thread = new Thread(new ThreadStart(SendNewData));
            conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName);
            conn.Open();
            thread.Start();
        }

        private void SendNewData()
        {
            var cmd = new OleDbCommand(
                @"select * from Sub_Component_Protocol_Psychophysiological_Session_Data_TPS
                WHERE psychophysiological_Session_Data_ID > 391000 AND psychophysiological_Session_Data_ID < 391700
                ORDER BY Psychophysiological_Session_Data_ID"
, conn);

            var reader = cmd.ExecuteReader();
            Thread.Sleep(20);
            while (reader.Read())
            {
                var data = new Sub_Component_Protocol_Psychophysiological_Session_Data_TPS();
                data.psychophysiological_Session_Data_ID = Int32.Parse(reader["psychophysiological_Session_Data_ID"].ToString());
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
                NotifyObservers(data);
                Thread.Sleep(20);
            }
            conn.Close();
        }

        public override void StopProcessing()
        {
            thread.Abort();
            conn.Close();
        }
    }
}
