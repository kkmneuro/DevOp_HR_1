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
        private string databaseLocation;
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

            if (!saveBioData && !saveTickPrice && !savePriceAtBioDataTick && !saveUserActions)
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


            // creating tables from enumerations
            CreateTableFromEnum(cmd, typeof(UserAction));
            CreateTableFromEnum(cmd, typeof(OrderDirection));
            CreateTableFromEnum(cmd, typeof(OpenReason));
            CreateTableFromEnum(cmd, typeof(Portfolio.CloseReason));


            // create tables if they are not exists yet

            if (saveBioData)
            {
                try
                {
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
                            [ApplicationStates] INTEGER
                        )";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"CREATE INDEX Time_IDX ON BioData ([Time])";
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) {
                    var str = ex.Message;
                }
            }

            // can't save PriceAtBioDataTick without BioData
            savePriceAtBioDataTick &= saveBioData;

            if (saveTickPrice || savePriceAtBioDataTick)
            {
                try
                {
                    cmd.CommandText =
                        @"CREATE TABLE InstrumentTable (
                            [ID] AUTOINCREMENT NOT NULL PRIMARY KEY,
                            [Title] VARCHAR(40) NOT NULL);";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText =
                        @"INSERT INTO InstrumentTable
                            ([ID], [Title])
                            VALUES (1, 'EURUSD');";
                    cmd.ExecuteNonQuery();
                }
                catch { }
            }

            if (saveTickPrice)
            {
                try
                {
                    cmd.CommandText = 
                        @"CREATE TABLE TickPrice (
                            [ID] AUTOINCREMENT NOT NULL PRIMARY KEY,
                            [InstrumentID] NUMBER NOT NULL,
                            [Time] DATETIME NOT NULL,
                            [SellPrice] DOUBLE NOT NULL,
                            [BuyPrice] DOUBLE NOT NULL
                        );";
                    cmd.ExecuteNonQuery();
                }
                catch { }
            }

            if (savePriceAtBioDataTick)
            {
                try
                {
                    cmd.CommandText =
                        @"CREATE TABLE PriceAtBioDataTick (
                            [ID] NUMBER NOT NULL PRIMARY KEY,
                            [InstrumentID] NUMBER NOT NULL,
                            [SellPrice] DOUBLE NOT NULL,
                            [BuyPrice] DOUBLE NOT NULL);";
                    cmd.ExecuteNonQuery();
                }
                catch { }
            }

            if (saveUserActions)
            {
                try
                {
                    cmd.CommandText =
                        @"CREATE TABLE UserActions (
                            [ID] AUTOINCREMENT NOT NULL PRIMARY KEY,
                            [ActionID] NUMBER NOT NULL,
                            [Time] DATETIME NOT NULL,
                            [Data] TEXT,
                            CONSTRAINT FK_Action FOREIGN KEY (ActionID)
                            REFERENCES UserAction(ID));";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText =
                        @"CREATE INDEX Time_IDX
                        ON UserActions ([Time])";
                    cmd.ExecuteNonQuery();
                }
                catch { }
            }


            // saving trades history
            try
            {
                var commandStr = string.Format(
                    @"CREATE TABLE {0} (
                        [ID] AUTOINCREMENT NOT NULL PRIMARY KEY,
                        [OrderGroup] NUMBER NOT NULL,
                        [BMModelID] NUMBER NOT NULL,
                        [PlaceTime] DATETIME NOT NULL,
                        [OpenTime] DATETIME NOT NULL,
                        [OpenPrice] NUMBER NOT NULL,
                        [Direction] NUMBER NOT NULL,
                        [Value] NUMBER NOT NULL,
                        [LotSize] NUMBER NOT NULL,
                        [OpenReason] NUMBER NOT NULL,
                        [CloseTime] DATETIME NOT NULL,
                        [ClosePrice] NUMBER NOT NULL,
                        [CloseReason] NUMBER NOT NULL,
                        [Profitability] NUMBER NOT NULL,
                        [HardStopLossPips] NUMBER,
                        [TakeProfitPips] NUMBER,
                        [TrailingStopLossPips] NUMBER,
                        CONSTRAINT FK_Direction FOREIGN KEY (Direction) REFERENCES OrderDirection(ID),
                        CONSTRAINT FK_OpenReason FOREIGN KEY (OpenReason) REFERENCES OpenReason(ID),
                        CONSTRAINT FK_CloseReason FOREIGN KEY (CloseReason) REFERENCES CloseReason(ID)
                    );",
                    "OrdersHistory");
                cmd.CommandText = commandStr;
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"CREATE INDEX PlaceTime_IDX ON OrdersHistory ([PlaceTime])";
                cmd.ExecuteNonQuery();
                cmd.CommandText = @"CREATE INDEX OpenTime_IDX ON OrdersHistory ([OpenTime])";
                cmd.ExecuteNonQuery();
                cmd.CommandText = @"CREATE INDEX CloseTime_IDX ON OrdersHistory ([CloseTime])";
                cmd.ExecuteNonQuery();
            }
            catch {}

            DatabaseConnected = true;
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
                        ([OrderGroup],
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
                        [TakeProfitPips],
                        [TrailingStopLossPips])
                VALUES ({1}, {2}, '{3}', '{4}', {5}, {6}, {7}, {8}, {9}, '{10}', {11}, {12}, {13}, {14}, {15}, {16});",
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
                StringHelpers.NullableToString(order.TrailingStopLossPips)
                );

            var cmd = new OleDbCommand(commandText, connection);
            cmd.ExecuteNonQuery();
        }

        private void CreateTableFromEnum(OleDbCommand cmd, Type type)
        {
            try
            {
                var commandStr = string.Format(
                   "CREATE TABLE {0} ([ID] NUMBER NOT NULL PRIMARY KEY, [Description] TEXT NOT NULL);",
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
    }
}
