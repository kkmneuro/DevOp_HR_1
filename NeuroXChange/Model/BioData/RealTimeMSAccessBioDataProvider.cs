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
        private volatile bool NeedStop = false;
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
        }

        private void GenerateNewData()
        {
            try
            {
                conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + this.databaseLocation);
                conn.Open();

                int lastID = -1;

                // calculate lastInd from existing top row
                var commandStr =
                    string.Format(@"SELECT TOP 1 * 
                                FROM {0}
                                ORDER BY Psychophysiological_Session_Data_ID DESC",
                    this.tableName);
                var cmd = new OleDbCommand(commandStr, conn);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    var data = BioData.FromOleDbDataReader(reader);
                    lastID = data.psychophysiological_Session_Data_ID;
                    NotifyObservers(data);
                }
                reader.Close();
                cmd.Dispose();

                // main loop
                while (!NeedStop)
                {
                    Thread.Sleep(200);
                    if (NeedStop)
                    {
                        break;
                    }

                    commandStr =
                        string.Format(@"SELECT TOP 20 * 
                                FROM {0}
                                WHERE Psychophysiological_Session_Data_ID > {1}
                                ORDER BY Psychophysiological_Session_Data_ID ASC",
                        this.tableName, lastID);

                    cmd = new OleDbCommand(commandStr, conn);
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int currID = Int32.Parse(reader["Psychophysiological_Session_Data_ID"].ToString());

                        if (lastID >= currID)
                        {
                            continue;
                        }
                        lastID = currID;

                        var data = BioData.FromOleDbDataReader(reader);
                        NotifyObservers(data);
                    }
                    reader.Close();
                    cmd.Dispose();
                }

                conn.Close();

            } catch (Exception e)
            {
                Console.Out.WriteLine(e);
            }
        }

        public override void StartProcessing()
        {
            thread.Start();
        }

        public override void StopProcessing()
        {
            NeedStop = true;
        }
    }
}
