using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace TEST.DataBase
{
    public abstract class CommandSQL
    {
        public string  SQLStatement { get; private set; }

        /// <summary>
        /// Sql execution command
        /// </summary>
        /// 
        private SqlCommand sqlCmd;

        /// <summary>
        /// 
        /// </summary>
        private string result;

        /// <summary>
        /// 
        /// </summary>
        abstract public void Execute();

        /// <summary>
        /// Set sql statement
        /// </summary>
        /// <param name="sql"></param>
        public void SetSQLStatement(string sql)
        {
            SQLStatement = sql;
        }


    }
}
