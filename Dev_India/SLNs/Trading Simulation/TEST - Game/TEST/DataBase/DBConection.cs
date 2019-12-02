using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace TEST.DataBase
{
    public class DBConection
    {

        private string sqlConnectionString;
        public static SqlConnection connection;

        public DBConection(string cs)
        {
            sqlConnectionString = cs;
            connection = new SqlConnection(cs);
            var s = connection.OpenAsync(); 
        }


    }
}
