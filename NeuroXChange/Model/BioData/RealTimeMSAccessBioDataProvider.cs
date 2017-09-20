using NeuroXChange.Common;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NeuroXChange.Model.BioData
{
    public class RealTimeMSAccessBioDataProvider : AbstractBioDataProvider
    {
        private OleDbConnection conn;
        private Thread thread;
        private string databaseLocation;
        private string tableName;

        public RealTimeMSAccessBioDataProvider(IniFileReader iniFileReader)
        {
            this.databaseLocation = iniFileReader.Read("Location", "Database");
            this.tableName = iniFileReader.Read("Table", "Database");

            if (!File.Exists(this.databaseLocation))
            {
                throw new Exception("Can't find database \"" + this.databaseLocation + "\"");
            }

            thread = new Thread(new ThreadStart(GenerateNewData));
            conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + this.databaseLocation);
            conn.Open();
            thread.Start();
        }

        private void GenerateNewData()
        {
            int lastInd = 0;

            var cmd = new OleDbCommand(
                @"SELECT TOP 1 * FROM " + this.tableName + @" 
                ORDER BY Psychophysiological_Session_Data_ID DESC", conn);

            while (true)
            {
                Thread.Sleep(100);

                var reader = cmd.ExecuteReader();
                reader.Read();

                int indNow = Int32.Parse(reader["psychophysiological_Session_Data_ID"].ToString());
                if (lastInd == indNow)
                {
                    reader.Close();
                    continue;
                }
                lastInd = indNow;

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
                reader.Close();
            }
        }

        public override void StopProcessing()
        {
            thread.Abort();
            conn.Close();
        }
    }
}
