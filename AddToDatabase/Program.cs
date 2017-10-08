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
            string fileName = @"C:\tmp\neurotrader\Neuro-Xchange_Psychophysiology1.mdb";

            OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName);
            conn.Open();
            Thread.Sleep(3000);

            int testCase = 1;

            // emulate program stage change
            if (testCase == 0)
            {
                for (int i = 1000000000; i < 1000000100; i++)
                {
                    Thread.Sleep(2000);
                    Console.WriteLine("Send data, inner counter " + i.ToString());
                    OleDbCommand cmd = new OleDbCommand(@"
insert into Sub_Component_Protocol_Psychophysiological_Session_Data_TPS_1(
psychophysiological_Session_Data_ID, [Time], Temperature, HartRate, AccY, Session_Component_ID, Sub_Component_ID, Sub_Component_Protocol_ID, Participant_ID, [Data])
 values(" + i.ToString() + @",'10/05/2017 06:30:57', 33.6967766284943, 120.805366516113, " + (i % 2 == 0 ? "-75" : "-5") + ", 2, 4, 71, 58, 'aaa')", conn);
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


            conn.Close();
        }
    }
}
