using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


using TEST.DataBase;

namespace DataBase.UnitTests
{
    [TestClass]
    public class DataBaseTesting
    {

        bool wait = true;
        [TestMethod]
        public void DBConnect()
        {
            int count =0;
            
            DBConection dbCon = new DBConection("Server=185.144.156.161;Database=nt;User Id=neurouser;Password = AlggoMalgo123; ");
            DBConection.connection.StateChange += Connection_StateChange;

            Assert.AreEqual(DBConection.connection.State, System.Data.ConnectionState.Connecting);
            while (wait)
            {
                System.Threading.Thread.Sleep(100);
                count++;
            }
            Assert.AreEqual(DBConection.connection.State, System.Data.ConnectionState.Open);
        }

        private void Connection_StateChange(object sender, System.Data.StateChangeEventArgs e)
        {
           if (e.CurrentState == System.Data.ConnectionState.Connecting) wait = true;
           else wait = false;
            //if (e.CurrentState == System.Data.ConnectionState.) wait = true;
        }
    }
}
