using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace NeuroTraderLogger
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Logger : ILogger
    {
       

        public void WriteLog(string source, string log, DateTime datetime)
        {

            String connectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(connectionString);
            myConnection.Open();
            //System.Diagnostics.Trace.WriteLine("Source: " + source + " log: " + log + " datetime: " + datetime);
            string query = "INSERT INTO ActionLog (AccountId, UserAction, createdon)";
            query += " VALUES (@AccountId, @UserAction, @CreatedOn)";

            SqlCommand myCommand = new SqlCommand(query, myConnection);
            myCommand.Parameters.AddWithValue("@AccountId", source);
            myCommand.Parameters.AddWithValue("@UserAction", log);
            myCommand.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
            // ... other parameters
            myCommand.ExecuteNonQuery();

        }
    }
}
