using System;
using System.Data.SqlClient;

namespace PostTradingAnalysis
{
    public class User
    {
        public long id;
        public string fullName;
        public string mail;
        public string login;

        public static User FromSqlDataReader(SqlDataReader reader)
        {
            var user = new User();
            user.id = Int64.Parse(reader["ID"].ToString());
            user.fullName = reader["FullName"].ToString();
            user.mail = reader["Mail"].ToString();
            user.login = reader["Login"].ToString();
            return user;
        }
    }
}
