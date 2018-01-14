using NeuroXChange.Common;
using NeuroXChange.Model.FixApi;
using NeuroXChange.Model.Portfolio;
using System;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace NeuroXChange.Model.Database
{
    public class LocalDatabaseConnector
    {
        public const int CurrentDBVersion = 2;

        private string databaseLocation;
        private string connectionString;
        private bool emulationOnHistoryMode;

        public bool DatabaseConnected { get; private set; }

        public bool saveBioData { get; private set; }
        public bool saveTickPrice { get; private set; }
        public bool savePriceAtBioDataTick { get; private set; }
        public bool saveUserActions { get; private set; }

        private OleDbConnection connection = null;

        public LocalDatabaseConnector(IniFileReader iniFileReader)
        {
            DatabaseConnected = false;

            emulationOnHistoryMode = Boolean.Parse(iniFileReader.Read("UseEmulationOnHistory", "EmulationOnHistory", "false"));
            databaseLocation = iniFileReader.Read("Location", "Database", "Data\\PsychophysiologyDatabase.mdb");

            saveBioData = bool.Parse(iniFileReader.Read("SaveBioData", "Database", "true"));
            saveTickPrice = bool.Parse(iniFileReader.Read("SaveTickPrice", "Database", "false"));
            savePriceAtBioDataTick = bool.Parse(iniFileReader.Read("SavePriceAtBioDataTick", "Database", "true"));
            saveUserActions = bool.Parse(iniFileReader.Read("SaveUserActions", "Database", "true"));

            connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"{0}\"", databaseLocation);

            bool needToCreateDBStructure = false;

            if (!File.Exists(databaseLocation))
            {
                needToCreateDBStructure = true;

                // create empty MS Access database file if there are no such file 
                try
                {
                    MessageBox.Show("File \"" + databaseLocation + "\" doesn't exist. It will be created");
                    var cat = new ADOX.Catalog();
                    cat.Create(connectionString);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error in creating of \"" + databaseLocation + "\" file:\r\n" + e.Message);
                    return;
                }
            }

            OleDbCommand cmd;

            // try to open database
            try
            {
                connection = new OleDbConnection(connectionString);
                connection.Open();
                cmd = new OleDbCommand();
                cmd.Connection = connection;
            }
            catch (Exception e)
            {
                MessageBox.Show("Was not able to establish a connection with local database. \r\n" + e.Message);
                return;
            }

            if (needToCreateDBStructure)
            {
                try
                {
                    CreateDatabaseStructure(cmd);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error in creating database structure:\r\n" + e.Message);
                    return;
                }
            }

            int dbVersion = GetDatabaseVersion(cmd);

            // updating database structure if needed
            if (dbVersion < CurrentDBVersion)
            {
                try
                {
                    MessageBox.Show(
                        string.Format("Database structure need to be updated\r\nCurrent version: {0}\r\nLatest version: {1}",
                        dbVersion, CurrentDBVersion));
                    UpdateDatabaseStructure(cmd, dbVersion);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error in updating structure:\r\n" + e.Message);
                    return;
                }
            }

            // can't save PriceAtBioDataTick without BioData
            savePriceAtBioDataTick &= saveBioData;

            DownloadOrdersVariables(cmd);

            DatabaseConnected = true;
        }


        // persistent orders variables
        public int LastOrderID { get; private set; }
        public int InitiateNewOrderID()
        {
            LastOrderID++;

            var commandText = string.Format(
                @"UPDATE DBSettings SET [Value] = '{0}' WHERE [Key] = 'LastOrderID'",
                LastOrderID);
            var cmd = new OleDbCommand(commandText, connection);
            cmd.ExecuteNonQuery();

            return LastOrderID;
        }
        public int LastGroupID { get; private set; }
        public int InitiateNewGroupID()
        {
            LastGroupID++;

            var commandText = string.Format(
                @"UPDATE DBSettings SET [Value] = '{0}' WHERE [Key] = 'LastGroupID'",
                LastGroupID);
            var cmd = new OleDbCommand(commandText, connection);
            cmd.ExecuteNonQuery();

            return LastGroupID;
        }
        private void DownloadOrdersVariables(OleDbCommand cmd)
        {
            cmd.CommandText = @"SELECT [Value] FROM DBSettings WHERE [Key] = 'LastOrderID'";
            var reader = cmd.ExecuteReader();
            reader.Read();
            LastOrderID = Int32.Parse(reader["Value"].ToString());
            reader.Close();
            
            cmd.CommandText = @"SELECT [Value] FROM DBSettings WHERE [Key] = 'LastGroupID'";
            reader = cmd.ExecuteReader();
            reader.Read();
            LastGroupID = Int32.Parse(reader["Value"].ToString());
            reader.Close();
        }


        // returns id of added row in the database table
        public int WriteBioData(BioData.BioData data)
        {
            if (!DatabaseConnected || !saveBioData || emulationOnHistoryMode)
            {
                return -1;
            }

            var commandText = string.Format(@"
                INSERT INTO BioData ([Time], Temperature, HeartRate, SkinConductance, AccX, AccY, AccZ, TrainingType, TrainingStep, ApplicationStates)
                    VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, {8}, {9})",
                data.time,
                data.temperature,
                data.heartRate,
                data.skinConductance,
                data.accX,
                data.accY,
                data.accZ,
                data.trainingType,
                data.trainingStep,
                data.applicationStates
                );

            var cmd = new OleDbCommand(commandText, connection);
            cmd.ExecuteNonQuery();

            cmd.CommandText = "Select @@Identity";
            int id = (int)cmd.ExecuteScalar();

            return id;
        }

        public void WriteTickPrice(TickPrice tickPrice)
        {
            if (!DatabaseConnected || !saveTickPrice || emulationOnHistoryMode)
            {
                return;
            }

            var commandText = string.Format(
                @"INSERT INTO TickPrice 
                ([InstrumentID], [Time], [SellPrice], [BuyPrice])
                VALUES ({0}, '{1}', {2}, {3});",
                1,
                DateTime.Now,
                tickPrice.sellString,
                tickPrice.buyString);

            var cmd = new OleDbCommand(commandText, connection);
            cmd.ExecuteNonQueryAsync();
        }

        public void WritePriceAtBioDataTick(TickPrice tickPrice, long biodataId)
        {
            if (!DatabaseConnected || !savePriceAtBioDataTick || emulationOnHistoryMode)
            {
                return;
            }

            var commandText = string.Format(
                @"INSERT INTO PriceAtBioDataTick
                    ([ID], [InstrumentID], [SellPrice], [BuyPrice])
                    VALUES ({0}, {1}, {2}, {3} );",
                biodataId,
                1,
                tickPrice.sellString,
                tickPrice.buyString);

            var cmd = new OleDbCommand(commandText, connection);
            cmd.ExecuteNonQueryAsync();
        }

        public void WriteUserAction(UserAction action, string data = null)
        {
            WriteUserAction(action, DateTime.Now, data);
        }

        public void WriteUserAction(UserAction action, DateTime time, string data = null)
        {
            if (!DatabaseConnected || !saveUserActions || emulationOnHistoryMode)
            {
                return;
            }

            var commandText = string.Format(
                @"INSERT INTO UserActions
                    ([ActionID], [Time], [Data])
                    VALUES ({0}, '{1}', '{2}');",
                (int)action,
                time,
                data);

            var cmd = new OleDbCommand(commandText, connection);
            cmd.ExecuteNonQueryAsync();
        }

        public void WriteClosedOrder(Order order)
        {
            if (!DatabaseConnected || emulationOnHistoryMode)
            {
                return;
            }

            var commandText = string.Format(
                @"INSERT INTO {0} 
                        ([ID],
                        [OrderGroup],
                        [BMModelID],
                        [PlaceTime],
                        [OpenTime],
                        [OpenPrice],
                        [Direction],
                        [Value],
                        [LotSize],
                        [OpenReason],
                        [CloseTime],
                        [ClosePrice],
                        [CloseReason],
                        [Profitability],
                        [HardStopLossPips],
                        [TakeProfitPips])
                VALUES ({16}, {1}, {2}, '{3}', '{4}', {5}, {6}, {7}, {8}, {9}, '{10}', {11}, {12}, {13}, {14}, {15});",
                "OrdersHistory",
                order.OrderGroup,
                order.BMModelID,
                order.PlaceTime,
                order.OpenTime,
                order.OpenPrice,
                (int)order.Direction,
                order.Value,
                order.LotSize,
                (int)order.openReason,
                order.CloseTime,
                order.ClosePrice,
                (int?)order.closeReason,
                order.Profitability,
                order.HardStopLossPips,
                order.TakeProfitPips,
                order.OrderID
                );

            var cmd = new OleDbCommand(commandText, connection);
            cmd.ExecuteNonQuery();
        }


        private void CreateDatabaseStructure(OleDbCommand cmd)
        {
            // DBSettings table
            cmd.CommandText =
                @"CREATE TABLE DBSettings (
                            [Key] TEXT NOT NULL PRIMARY KEY,
                            [Value] TEXT);";
            cmd.ExecuteNonQuery();

            cmd.CommandText = string.Format(
                @"INSERT INTO DBSettings ([Key], [Value]) VALUES ('Version', {0});",
                CurrentDBVersion);
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"INSERT INTO DBSettings ([Key], [Value]) VALUES ('LastOrderID', '0');";
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"INSERT INTO DBSettings ([Key], [Value]) VALUES ('LastGroupID', '0');";
            cmd.ExecuteNonQuery();

            // creating tables from enumerations
            CreateTableFromEnum(cmd, typeof(UserAction));
            CreateTableFromEnum(cmd, typeof(OrderDirection));
            CreateTableFromEnum(cmd, typeof(OpenReason));
            CreateTableFromEnum(cmd, typeof(Portfolio.CloseReason));

            // BioData table
            cmd.CommandText =
                @"CREATE TABLE BioData (
                            [ID] AUTOINCREMENT NOT NULL PRIMARY KEY,
                            [Time] DATETIME NOT NULL,
                            [Temperature] DOUBLE,
                            [HeartRate] DOUBLE,
                            [SkinConductance] DOUBLE,
                            [AccX] DOUBLE,
                            [AccY] DOUBLE,
                            [AccZ] DOUBLE,
                            [TrainingType] INTEGER,
                            [TrainingStep] INTEGER,
                            [ApplicationStates] LONG
                        )";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"CREATE INDEX Time_IDX ON BioData ([Time])";
            cmd.ExecuteNonQuery();

            // Instrument table
            cmd.CommandText =
                @"CREATE TABLE Instrument (
                    [ID] AUTOINCREMENT NOT NULL PRIMARY KEY,
                    [Title] VARCHAR(40) NOT NULL);";
            cmd.ExecuteNonQuery();

            cmd.CommandText =
                @"INSERT INTO Instrument
                    ([ID], [Title])
                    VALUES (1, 'EURUSD');";
            cmd.ExecuteNonQuery();

            // TickPrice table
            cmd.CommandText =
                @"CREATE TABLE TickPrice (
                            [ID] AUTOINCREMENT NOT NULL PRIMARY KEY,
                            [InstrumentID] LONG NOT NULL,
                            [Time] DATETIME NOT NULL,
                            [SellPrice] DOUBLE NOT NULL,
                            [BuyPrice] DOUBLE NOT NULL
                        );";
            cmd.ExecuteNonQuery();

            // PriceAtBioDataTick table
            cmd.CommandText =
                @"CREATE TABLE PriceAtBioDataTick (
                            [ID] LONG NOT NULL PRIMARY KEY,
                            [InstrumentID] LONG NOT NULL,
                            [SellPrice] DOUBLE NOT NULL,
                            [BuyPrice] DOUBLE NOT NULL);";
            cmd.ExecuteNonQuery();

            // UserActions table
            cmd.CommandText =
                @"CREATE TABLE UserActions (
                            [ID] AUTOINCREMENT NOT NULL PRIMARY KEY,
                            [ActionID] INTEGER NOT NULL,
                            [Time] DATETIME NOT NULL,
                            [Data] TEXT,
                            CONSTRAINT FK_Action FOREIGN KEY (ActionID)
                            REFERENCES UserAction(ID));";
            cmd.ExecuteNonQuery();

            cmd.CommandText =
                @"CREATE INDEX Time_IDX
                        ON UserActions ([Time])";
            cmd.ExecuteNonQuery();

            // OrdersHistory table
            cmd.CommandText =
                @"CREATE TABLE OrdersHistory (
                        [ID] LONG NOT NULL PRIMARY KEY,
                        [OrderGroup] LONG NOT NULL,
                        [BMModelID] LONG NOT NULL,
                        [PlaceTime] DATETIME NOT NULL,
                        [OpenTime] DATETIME NOT NULL,
                        [OpenPrice] DOUBLE NOT NULL,
                        [Direction] INTEGER NOT NULL,
                        [Value] INTEGER NOT NULL,
                        [LotSize] INTEGER NOT NULL,
                        [OpenReason] INTEGER NOT NULL,
                        [CloseTime] DATETIME NOT NULL,
                        [ClosePrice] DOUBLE NOT NULL,
                        [CloseReason] INTEGER NOT NULL,
                        [Profitability] LONG NOT NULL,
                        [HardStopLossPips] INTEGER,
                        [TakeProfitPips] INTEGER,
                        CONSTRAINT FK_Direction FOREIGN KEY (Direction) REFERENCES OrderDirection(ID),
                        CONSTRAINT FK_OpenReason FOREIGN KEY (OpenReason) REFERENCES OpenReason(ID),
                        CONSTRAINT FK_CloseReason FOREIGN KEY (CloseReason) REFERENCES CloseReason(ID)
                    );";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"CREATE INDEX PlaceTime_IDX ON OrdersHistory ([PlaceTime])";
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"CREATE INDEX OpenTime_IDX ON OrdersHistory ([OpenTime])";
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"CREATE INDEX CloseTime_IDX ON OrdersHistory ([CloseTime])";
            cmd.ExecuteNonQuery();
        }

        private int GetDatabaseVersion(OleDbCommand cmd)
        {
            int version = 0;

            try
            {
                cmd.CommandText = @"SELECT [Value] FROM DBSettings WHERE [Key] = 'Version'";

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    version = Int32.Parse(reader["Value"].ToString());
                }
                else
                {
                    return 0;
                }
                reader.Close();
            } catch
            {
                return 0;
            }

            return version;
        }

        private void CreateTableFromEnum(OleDbCommand cmd, Type type)
        {
            try
            {
                var commandStr = string.Format(
                   "CREATE TABLE {0} ([ID] INTEGER NOT NULL PRIMARY KEY, [Description] TEXT NOT NULL);",
                   type.Name);
                cmd.CommandText = commandStr;
                cmd.ExecuteNonQuery();

                foreach (var value in type.GetEnumValues())
                {
                    commandStr = string.Format(
                        @"INSERT INTO {0}
                            ([ID], [Description])
                            VALUES ({1}, '{2}')",
                        type.Name, (int)value, value);
                    cmd.CommandText = commandStr;
                    cmd.ExecuteNonQuery();
                }
            }
            catch {}
        }

        private void UpdateDatabaseStructure(OleDbCommand cmd, int fromVersion)
        {
            if (fromVersion != CurrentDBVersion)
            {
                throw new Exception(string.Format(
                    @"Can't update database from version {0} to version {1}!\r\nYou need to start data colleciton from the scratch",
                    fromVersion, CurrentDBVersion));
            }
        }
    }
}
