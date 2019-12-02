using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace TEST.DataBase
{
    class CommandExecutor
    {

        private DBConection dbConnection;

        private List<Commands.BaseCommandWriteUpdateRecord> _writeCmd;
        /// <summary>
        /// executes comands from list merged in to one command
        /// </summary>
        private SqlCommand sqlCmd;



        public CommandExecutor()
        {
            dbConnection = new DBConection("");
            sqlCmd = new SqlCommand();
            sqlCmd.Connection = DBConection.connection;
        }


        /// <summary>
        /// Write Or Update single record and return an ID
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns> returnes record id</returns>
        public string WriteOrUpdate(Commands.BaseCommandWriteUpdateRecord cmd)
        {

            // execute scalar to get ID
            return "";

        } 

        /// <summary>
        /// Add command to command list for mass execution.
        /// only inserts with out returnin an id (good for price and bio data)
        /// </summary>
        /// <param name="cmd"></param>
        public void AddToExecutionList(Commands.BaseCommandWriteUpdateRecord cmd)
        {
            _writeCmd.Add(cmd);
        }

        /// <summary>
        /// Read single object from DB
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public object GetObjest(Commands.BaseCommandReadSingleRecord cmd)
        {
            return new object();
        }


        private void write() { }

        /// <summary>
        /// inserts values from insert commands in list _writeCmd
        /// </summary>
        private void massExecutor()
        {
            while (true)
            {

            }
        }
    }
}
