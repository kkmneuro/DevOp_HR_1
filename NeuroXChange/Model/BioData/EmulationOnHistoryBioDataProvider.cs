using System;
using System.Data.OleDb;
using System.IO;
using System.Threading;
using NeuroXChange.Common;

namespace NeuroXChange.Model.BioData
{
    public class EmulationOnHistoryBioDataProvider : AbstractBioDataProvider
    {
        private volatile bool NeedStop = false;
        private OleDbConnection conn;
        private Thread thread;
        private string databaseLocation;
        private string tableName;
        private long startDataRowId;
        private long endDataRowId;
        private int tickInterval;

        public EmulationOnHistoryBioDataProvider(IniFileReader iniFileReader)
        {
            databaseLocation = iniFileReader.Read("Location", "Database");
            tableName = iniFileReader.Read("Table", "Database");

            if (!File.Exists(this.databaseLocation))
            {
                throw new Exception("Can't find database \"" + this.databaseLocation + "\"");
            }

            startDataRowId = Int64.Parse(iniFileReader.Read("StartDataRowId", "EmulationOnHistory"));
            endDataRowId = Int64.Parse(iniFileReader.Read("EndDataRowId", "EmulationOnHistory"));
            tickInterval = Int32.Parse(iniFileReader.Read("TickInterval", "EmulationOnHistory"));

            if (startDataRowId > endDataRowId)
            {
                throw new Exception("startDataRowId can't be greater than endDataRowId!\n\rCorrect these fields in ini-file");
            }

            thread = new Thread(new ThreadStart(GenerateNewData));
            thread.Start();
        }

        private void GenerateNewData()
        {
            try
            {
                conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + this.databaseLocation);
                conn.Open();

                var commandStr = string.Format(
                    @"SELECT * FROM {0}
                WHERE psychophysiological_Session_Data_ID >= {1} AND psychophysiological_Session_Data_ID <= {2}
                ORDER BY Psychophysiological_Session_Data_ID",
                    tableName, startDataRowId, endDataRowId);

                var cmd = new OleDbCommand(commandStr, conn);

                var reader = cmd.ExecuteReader();
                while (!NeedStop && reader.Read())
                {
                    var data = BioData.FromOleDbDataReader(reader);
                    NotifyObservers(data);
                    Thread.Sleep(tickInterval);
                }
                reader.Close();
                cmd.Dispose();
                conn.Close();
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e);
            }
        }

        public override void StopProcessing()
        {
            NeedStop = true;
        }
    }
}
