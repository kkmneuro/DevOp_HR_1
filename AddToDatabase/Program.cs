using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AddToDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = @"C:\tmp\neurotrader\data.mdb";

            OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName);
            conn.Open();
            Thread.Sleep(3000);

            int testCase = 0;

            // emulate program stage change
            if (testCase == 0)
            {
                for (int i = 0; i < 60; i++)
                {
                    Thread.Sleep(2000);
                    Console.WriteLine("Send data, inner counter " + i.ToString());
                    var commandString = string.Format(
                        @"
insert into TestBehavioralModelsAccY(
psychophysiological_Session_Data_ID, [Time], Temperature, HartRate, AccY, Session_Component_ID, Sub_Component_ID, Sub_Component_Protocol_ID, Participant_ID, [Data])
 values({0},'10/05/2017 06:30:{1}', 33.6967766284943, 120.805366516113, {2}, 2, 4, 71, 58, 'aaa')",
i, i, (i % 2 == 0 ? "-75" : "-5"));
                    OleDbCommand cmd = new OleDbCommand(commandString, conn);
                    cmd.ExecuteNonQuery();
                }
            }

            // Logic Query 1 (Direction)
            if (testCase == 1)
            {
                for (int sub_Protocol_ID = 68; sub_Protocol_ID <= 74; sub_Protocol_ID++)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Send data, sub_Protocol_ID " + sub_Protocol_ID.ToString());
                    var commandString = string.Format(
                    @"
insert into Sub_Component_Protocol_Psychophysiological_Session_Data_TPS_1(
psychophysiological_Session_Data_ID, [Time], Temperature, HartRate, AccY, Session_Component_ID, Sub_Component_ID, Sub_Component_Protocol_ID, Sub_Protocol_ID, Participant_ID, [Data])
 values({0},'10/05/2017 06:30:57', 33.6967766284943, {1}, 30, 2, 4, 71, {2}, 58, 'aaa')",
sub_Protocol_ID, 60 - (sub_Protocol_ID-68) + 3 , sub_Protocol_ID);
                    OleDbCommand cmd = new OleDbCommand(commandString, conn);
                    cmd.ExecuteNonQuery();
                }
            }

            // Logic Query 2 (entry trigger)
            if (testCase == 2)
            {
                for (int ind = 0; ind < 12; ind++)
                {
                    Thread.Sleep(1000);
                    int heartRate = ind < 6 ? 110 : 50;
                    DateTime dt = new DateTime(2017, 10, 5, 11, 0 + (ind) / 6, (ind % 6) * 10);
                    var dtStr = dt.ToString("dd/MM/yyyy HH:mm:ss");
                    Console.WriteLine(string.Format("Send data, heart rate: {0}, time: {1}, data ind: {2}", heartRate, dtStr, ind));
                    var commandString = string.Format(
                    @"
insert into Sub_Component_Protocol_Psychophysiological_Session_Data_TPS_1(
psychophysiological_Session_Data_ID, [Time], Temperature, HartRate, AccY, Session_Component_ID, Sub_Component_ID, Sub_Component_Protocol_ID, Sub_Protocol_ID, Participant_ID, [Data])
 values({0},'{1}', 33.6967766284943, {2}, 30, 2, 4, 71, 74, 58, 'aaa')",
ind, dtStr, heartRate);
                    OleDbCommand cmd = new OleDbCommand(commandString, conn);
                    cmd.ExecuteNonQuery();
                }
            }

            if (testCase == 3)
            {
                for (int ind = 0; ind < 100; ind++)
                {
                    Thread.Sleep(150);
                    var dt = DateTime.Now;
                    var dtStr = dt.ToString("dd/MM/yyyy HH:mm:ss");
                    Console.WriteLine("Send data, inner counter " + ind.ToString());
                    var commandStr = string.Format(@"
INSERT INTO Sub_Component_Protocol_Psychophysiological_Session_Data_TPS_1(
[Time], Temperature, HartRate, AccY, Session_Component_ID, Sub_Component_ID, Sub_Component_Protocol_ID, Participant_ID, [Data])
 VALUES('{0}', 33.6967766284943, 120.805366516113, -75, 2, 4, 71, 58, 'aaa')", dtStr);
                    OleDbCommand cmd = new OleDbCommand(commandStr, conn);
                    cmd.ExecuteNonQuery();
                }
            }

            conn.Close();
        }
    }
}
