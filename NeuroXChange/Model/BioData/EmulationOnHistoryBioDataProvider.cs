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
        private string tableName;
        private long startDataRowId;
        private long endDataRowId;
        private volatile int tickInterval;

        private string priceAtBioDataTickTable;

        private int totalTicksCount;
        private int ticksPassed;
        private volatile bool paused;
        private volatile bool nextTickEmulation;

        public EmulationOnHistoryBioDataProvider(LocalDatabaseConnector localDatabaseConnector,
            IniFileReader iniFileReader)
            : base(localDatabaseConnector)
        {
            databaseLocation = iniFileReader.Read("Location", "Database", "Data\\PsychophysiologyDatabase.mdb");
            tableName = iniFileReader.Read("BioDataTable", "Database", "BioData");
            priceAtBioDataTickTable = iniFileReader.Read("PriceAtBioDataTickTable", "Database", "PriceAtBioDataTick");

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
                    @"SELECT COUNT(*) AS totalTicks FROM {0}
                WHERE ID >= {2} AND ID <= {3}",
                    tableName, null, startDataRowId, endDataRowId);

                var cmd = new OleDbCommand(TotalRowsCountCommandStr, conn);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    totalTicksCount = Int32.Parse(reader["totalTicks"].ToString());
                }
                reader.Close();
                cmd.Dispose();

                var commandStr = string.Format(
                    @"SELECT {0}.*, sellPrice, buyPrice FROM {0}
                LEFT OUTER JOIN {1} ON {0}.ID = {1}.ID
                WHERE ID >= {2} AND ID <= {3}
                ORDER BY ID",
                    tableName, priceAtBioDataTickTable, startDataRowId, endDataRowId);

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
