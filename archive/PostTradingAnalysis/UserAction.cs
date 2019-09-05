using System;
using System.Data.SqlClient;

namespace PostTradingAnalysis
{
    public class UserAction
    {
        public long id;
        public int actionId;
        public DateTime time;
        public long detail;

        public static UserAction FromSqlDataReader(SqlDataReader reader)
        {
            var data = new UserAction();
            data.id = Int64.Parse(reader["ID"].ToString());
            data.actionId = Int32.Parse(reader["ActionID"].ToString());
            data.time = DateTime.Parse(reader["Time"].ToString());
            data.detail = Int64.Parse(reader["Detail"].ToString());
            return data;
        }
    }
}
