using NeuroXChange.Common;
using NeuroXChange.Model.FixApi;
using System;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace NeuroXChange.Model.Database
{
    public class LocalDatabaseConnector
    {
        public bool DatabaseConnected { get; private set; }

        private string databaseLocation;

        public bool saveBioData { get; private set; }
        private string bioDataTable;

        public bool saveTickPrice { get; private set; }
        private string tickPriceTable;

        public bool savePriceAtBioDataTick { get; private set; }
        private string priceAtBioDataTickTable;

        private string instrumentTable;

        private OleDbConnection connection = null;
        private OleDbCommand generalCmd = null;
        private OleDbCommand biodataCmd = null;
        private OleDbCommand priceCmd = null;

        public LocalDatabaseConnector(IniFileReader iniFileReader)
        {
            DatabaseConnected = false;

            databaseLocation = iniFileReader.Read("Location", "Database");

            saveBioData = bool.Parse(iniFileReader.Read("SaveBioData", "Database"));
            bioDataTable = iniFileReader.Read("BioDataTable", "Database");

            saveTickPrice = bool.Parse(iniFileReader.Read("SaveTickPrice", "Database"));
            tickPriceTable = iniFileReader.Read("TickPriceTable", "Database");

            savePriceAtBioDataTick = bool.Parse(iniFileReader.Read("SavePriceAtBioDataTick", "Database"));
            priceAtBioDataTickTable = iniFileReader.Read("PriceAtBioDataTickTable", "Database");

            instrumentTable = iniFileReader.Read("InstrumentTable", "Database");

            if (!saveBioData && !saveTickPrice && !savePriceAtBioDataTick)
            {
                return;
            }

            var connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + databaseLocation;

            // create empty MS Access database file if there are no such file 
            try
            {
                if (!File.Exists(databaseLocation))
                {
                    MessageBox.Show("File \"" + databaseLocation + "\" doesn't exist. It will be created");
                    var cat = new ADOX.Catalog();
                    cat.Create(connectionString);
                }
            } 
            catch (Exception e)
            {
                MessageBox.Show("Error in creation of \"" + databaseLocation + "\" file: " + e.Message);
                return;
            }

            // try to open database
            try
            {
                connection = new OleDbConnection(connectionString);
                connection.Open();
                generalCmd = new OleDbCommand();
                generalCmd.Connection = connection;
            }
            catch (Exception e)
            {
                MessageBox.Show("Was not able to establish a connection with local database. \r\n" + e.Message);
                return;
            }

            // create tables if they are not exists yet

            if (saveBioData)
            {
                try
                {
                    var commandText = string.Format(
                        @"CREATE TABLE {0} ([Psychophysiological_Session_Data_ID] AUTOINCREMENT NOT NULL PRIMARY KEY,
                            [Time] DATETIME NOT NULL,
                            [Temperature] DOUBLE,
                            [HartRate] DOUBLE,
                            [SkinConductance] DOUBLE,
                            [AccX] DOUBLE,
                            [AccY] DOUBLE,
                            [AccZ] DOUBLE,
                            [Session_Component_ID] INTEGER,
                            [Sub_Component_ID] INTEGER,
                            [Sub_Component_Protocol_ID] INTEGER,
                            [Sub_Protocol_ID] INTEGER,
                            [Participant_ID] INTEGER,
                            [Data] TEXT(255)
                        )",
                        bioDataTable);
                    generalCmd = new OleDbCommand();
                    generalCmd.Connection = connection;
                    generalCmd.CommandText = commandText;
                    generalCmd.ExecuteNonQuery();
                }
                catch { }

                biodataCmd = new OleDbCommand();
                biodataCmd.Connection = connection;
            }

            // can't save PriceAtBioDataTick without BioData
            savePriceAtBioDataTick &= saveBioData;

            if (saveTickPrice || savePriceAtBioDataTick)
            {
                try
                {
                    var commandText = string.Format(
                        @"CREATE TABLE {0}
                        ([ID] AUTOINCREMENT NOT NULL PRIMARY KEY, [Title] VARCHAR(40) NOT NULL);",
                        instrumentTable);
                    generalCmd.CommandText = commandText;
                    generalCmd.ExecuteNonQuery();

                    commandText = string.Format(
                        @"INSERT INTO {0}
                        ([ID], [Title])
                        VALUES (1, 'EURUSD');",
                        instrumentTable);
                    generalCmd.CommandText = commandText;
                    generalCmd.ExecuteNonQuery();
                }
                catch { }
            }

            if (saveTickPrice)
            {
                try
                {
                    var commandStr = string.Format(
                        "CREATE TABLE {0} ([ID] AUTOINCREMENT NOT NULL PRIMARY KEY, [Instrument_ID] NUMBER NOT NULL, [Time] DATETIME NOT NULL, [SellPrice] DOUBLE NOT NULL, [BuyPrice] DOUBLE NOT NULL);",
                        tickPriceTable);
                    generalCmd.CommandText = commandStr;
                    generalCmd.ExecuteNonQuery();
                }
                catch { }

                priceCmd = new OleDbCommand();
                priceCmd.Connection = connection;
            }

            if (savePriceAtBioDataTick)
            {
                try
                {
                    var commandStr = string.Format(
                        "CREATE TABLE {0} ([ID] NUMBER NOT NULL PRIMARY KEY, [Instrument_ID] NUMBER NOT NULL, [SellPrice] DOUBLE NOT NULL, [BuyPrice] DOUBLE NOT NULL);",
                        priceAtBioDataTickTable);
                    generalCmd.CommandText = commandStr;
                    generalCmd.ExecuteNonQuery();
                }
                catch { }
            }

            DatabaseConnected = true;
        }

        // returns id of added row in the database table
        public int WriteBioData(BioData.BioData data)
        {
            if (!DatabaseConnected || !saveBioData)
            {
                return -1;
            }

            var commandText = string.Format(@"
                INSERT INTO {0} ([Time], Temperature, HartRate, SkinConductance, AccX, AccY, AccZ, Session_Component_ID, Sub_Component_ID, Sub_Component_Protocol_ID, Sub_Protocol_ID, Participant_ID, Data)
                    VALUES ('{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', {8}, {9}, {10}, {11}, {12}, '{13}')",
                bioDataTable,
                data.time,
                data.temperature,
                data.hartRate,
                data.skinConductance,
                data.accX,
                data.accY,
                data.accZ,
                data.session_Component_ID,
                data.sub_Component_ID,
                data.sub_Component_Protocol_ID,
                data.sub_Protocol_ID,
                data.participant_ID,
                data.data
                );

            biodataCmd.CommandText = commandText;
            biodataCmd.ExecuteNonQuery();

            biodataCmd.CommandText = "Select @@Identity";
            int id = (int)biodataCmd.ExecuteScalar();

            return id;
        }

        public void WriteTickPrice(TickPrice tickPrice)
        {
            if (!DatabaseConnected || !saveTickPrice)
            {
                return;
            }

            var commandText = string.Format(
                @"INSERT INTO {0} 
                ([Instrument_ID], [Time], [SellPrice], [BuyPrice])
                VALUES ({1}, '{2}', {3}, {4});",
                tickPriceTable,
                1,
                DateTime.Now,
                tickPrice.sellString,
                tickPrice.buyString);

            priceCmd.CommandText = commandText;
            priceCmd.ExecuteNonQueryAsync();
        }

        public void WritePriceAtBioDataTick(TickPrice tickPrice, int biodataId)
        {
            if (!DatabaseConnected || !savePriceAtBioDataTick)
            {
                return;
            }

            var commandText = string.Format(
                @"INSERT INTO {0}
                ([ID], [Instrument_ID], [SellPrice], [BuyPrice])
                VALUES ({1}, {2}, {3}, {4});",
                priceAtBioDataTickTable,
                biodataId,
                1,
                tickPrice.sellString,
                tickPrice.buyString);

            // use biodataCmd because it is in the same thread
            biodataCmd.CommandText = commandText;
            biodataCmd.ExecuteNonQueryAsync();
        }
    }
}
