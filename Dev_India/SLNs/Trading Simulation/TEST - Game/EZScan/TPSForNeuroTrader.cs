using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CTapp_def_ns;

namespace TPSForNeuroTrader
{


    /// <summary>
    /// 
    /// </summary>
    public class TPSData
    {
        public DateTime dt;
        public double SC;
        public double HR;
        public double Temp;
        public double Gesture;
        public double AccX;
        public double AccY;
        public double AccZ;

    }

    public enum e_cs { DISCONNECTED, CONNECTED, STARTED };


    /// <summary>
    /// 
    /// </summary>
    public class TPSForNeuroTrader
    {


        private TPSReader tpsr1;
        private int timeFrame;
        private string connectionString;

        /// <summary>
        /// TPS device connection management class
        /// </summary>
        /// <param name="timef"> How often data is read</param>
        /// <param name="TTLLive">Componnent whic connects to device</param>
        /// <param name="conenctionStr">connection string to COM port (ex.: \\.\COM3)</param>
        public TPSForNeuroTrader(int timef, AxTTLLiveCtrlLib.AxTTLLive TTLLive, string conenctionStr)
        {       
            timeFrame = timef;
            tpsr1 = new TPSReader(TTLLive);
            connectionString = conenctionStr;                
        }

        /// <summary>
        /// This one is not implemented yet it supposes to connect to device automatically (Simulation of button "Connect" click). 
        /// </summary>
        public bool conectToDevice()
        {
            try
            {
                tpsr1.tryConnection(connectionString);
            }
            catch (Exception eee)
            {
                throw new Exception("Connection to device was not succsessful. Check device and try again", eee); 
            }

            if (tpsr1.cxnState == TPSReader.e_cs.DISCONNECTED) return false;           
            if (tpsr1.cxnState == TPSReader.e_cs.CONNECTED) return true;
            return false;
        }

        public bool disconectFromDevice()
        {
            tpsr1.Disconnect();
            if (tpsr1.cxnState == TPSReader.e_cs.DISCONNECTED) return true;
            else return false;
        }

        public bool startDevice()
        {
            if (tpsr1.cxnState == TPSReader.e_cs.CONNECTED)
            {
                tpsr1.Start();
                if (tpsr1.cxnState == TPSReader.e_cs.STARTED) return true;
                else return false;                
            }
            return false;
        }


        /// <summary>
        /// stop connected device
        /// </summary>
        /// <returns></returns>
        public bool stopDevice()
        {
            if (tpsr1.cxnState == TPSReader.e_cs.STARTED)
            {
                tpsr1.Stop();
                if (tpsr1.cxnState == TPSReader.e_cs.CONNECTED) return true;
                else return false;
            }
            else throw new Exception("Trying to stop device which is not started or connected."); 
            //return true;
        }

        /// <summary>
        /// Get state of TPS reader
        /// </summary>
        /// <returns> state of reader one of DISCONNECTED, CONNECTED, STARTED</returns>
        public e_cs getState()
        {
            return (e_cs)tpsr1.cxnState;
        }

        /// <summary>
        /// Returns current measurement data from device 
        /// </summary>
        /// <returns>Returns data type TPSData </returns>
        public TPSData GetTPSData()
        {
            TPSData data = new TPSData();

            if (tpsr1.cxnState == TPSReader.e_cs.STARTED)
            {
                string eerr = "";
                try
                {
                    tpsr1.process();
                }
                catch (Exception exept)
                {
                    eerr = exept.Message;
                }
                if (eerr == "") // if not error read data 
                {
                    data.dt = System.DateTime.Now;
                    data.SC = tpsr1.SC;
                    data.HR = tpsr1.HR;
                    data.Temp = tpsr1.Temp * 100;
                    data.Gesture = tpsr1.gestureAngle;
                    data.AccX = tpsr1.AccX;
                    data.AccY = tpsr1.AccY;
                    data.AccZ = tpsr1.AccZ;
                }
            }
            else if (tpsr1.cxnState == TPSReader.e_cs.CONNECTED) throw new Exception("Device is connected, but not started.");
            else;// throw new Exception("Device is not connected and not started.");

            return data;
        }



        
        /// <summary>
        /// Destructor 
        /// </summary>
        ~TPSForNeuroTrader() {
        }


    }
}
