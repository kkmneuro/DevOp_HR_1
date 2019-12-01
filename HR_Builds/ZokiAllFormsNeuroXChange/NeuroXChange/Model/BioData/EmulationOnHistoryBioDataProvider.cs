using System;
using System.Data.OleDb;
using System.IO;
using System.Threading;
using NeuroXChange.Common;
using NeuroXChange.Model.Database;
using System.Windows.Forms;

namespace NeuroXChange.Model.BioData
{
    public class EmulationOnHistoryBioDataProvider : AbstractBioDataProvider
    {
        private volatile bool NeedStop = false;
        private OleDbConnection conn;
        private OleDbDataReader reader;
        private Thread thread;
        private string databaseLocation;
        private long startDataRowId;
        private long endDataRowId;
        private volatile int tickInterval;

        private int totalTicksCount;
        private int ticksPassed;
        private volatile bool paused;
        private volatile bool nextTickEmulation;

        public EmulationOnHistoryBioDataProvider(MainNeuroXModel model, 
            LocalDatabaseConnector localDatabaseConnector,
            IniFileReader iniFileReader)
            : base(model, localDatabaseConnector)
        {
            databaseLocation = iniFileReader.Read("Location", "Database", "Data\\PsychophysiologyDatabase.mdb");

            if (!File.Exists(this.databaseLocation))
            {
                throw new Exception("Can't find database \"" + this.databaseLocation + "\"");
            }

            startDataRowId = Int64.Parse(iniFileReader.Read("StartDataRowId", "EmulationOnHistory", "250000"));
            endDataRowId = Int64.Parse(iniFileReader.Read("EndDataRowId", "EmulationOnHistory", "550000"));
            tickInterval = Int32.Parse(iniFileReader.Read("HistoryTickInterval", "EmulationOnHistory", "100"));

            if (startDataRowId > endDataRowId)
            {
                throw new Exception("startDataRowId can't be greater than endDataRowId!\n\rCorrect these fields in ini-file");
            }

            totalTicksCount = 0;
            ticksPassed = 0;
            paused = false;
            nextTickEmulation = false;
            thread = new Thread(new ThreadStart(GenerateNewData));
        }

        private void GenerateNewData()
        {
            try
            {
                conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + this.databaseLocation);
                conn.Open();

                var TotalRowsCountCommandStr = string.Format(
                    @"SELECT COUNT(*) AS totalTicks FROM BioData
                WHERE ID >= {0} AND ID <= {1}",
                    startDataRowId, endDataRowId);

                var cmd = new OleDbCommand(TotalRowsCountCommandStr, conn);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    totalTicksCount = Int32.Parse(reader["totalTicks"].ToString());
                }
                reader.Close();
                cmd.Dispose();

                var commandStr = string.Format(
                    @"SELECT BioData.*, sellPrice, buyPrice FROM BioData
                LEFT OUTER JOIN PriceAtBioDataTick ON BioData.ID = PriceAtBioDataTick.ID
                WHERE BioData.ID >= {0} AND BioData.ID <= {1}
                ORDER BY BioData.ID",
                    startDataRowId, endDataRowId);

                cmd = new OleDbCommand(commandStr, conn);

                reader = cmd.ExecuteReader();
                while (!NeedStop)
                {
                    if (!paused || nextTickEmulation)
                    {
                        GenerateNewTick();
                        if (nextTickEmulation)
                        {
                            nextTickEmulation = false;
                        }
                        else
                        {
                            Thread.Sleep(tickInterval);
                        }
                    } else
                    {
                        Thread.Sleep(25);
                    }
                }
                NeedStop = true;
                reader.Close();
                cmd.Dispose();
                conn.Close();
                NotifyObservers(BioDataEvent.EmulationModeBioDataFinished, null);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error in connection to database:\r\n" + e.Message);
            }
        }

        private void GenerateNewTick()
        {
            if (NeedStop)
            {
                return;
            }
            NeedStop = !reader.Read();
            if (NeedStop)
            {
                return;
            }

            ticksPassed++;
            var data = BioData.FromOleDbDataReader(reader, true);
            NotifyObservers(BioDataEvent.NewBioDataTick, data);
            NotifyObservers(BioDataEvent.EmulationModeProgress, new int[] { ticksPassed, totalTicksCount });
        }

        public override void StartProcessing()
        {
            thread.Start();
        }

        public override void StopProcessing()
        {
            NeedStop = true;
        }

        // controlling functions
        public void StartEmulation()
        {
            if (paused)
            {
                NotifyObservers(BioDataEvent.EmulationModeContinued, null);
            }
            paused = false;
        }

        public void PauseEmulation()
        {
            if (!paused)
            {
                NotifyObservers(BioDataEvent.EmulationModePaused, null);
            }
            paused = true;
        }

        public void NextTickEmulation()
        {
            nextTickEmulation = true;
        }

        public void ChangeEmulationModeTickInterval(int tickInterval)
        {
            this.tickInterval = tickInterval;
        }
    }
}
