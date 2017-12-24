using AxTTLLiveCtrlLib;
using NeuroXChange.Common;
using NeuroXChange.Model.Database;
using System;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using TPSForNeuroTrader;

namespace NeuroXChange.Model.BioData
{
    public class TTLApiBioDataProvider : AbstractBioDataProvider
    {
        private string usbConnectionStr;
        private int bioDataTickInterval;

        private AxTTLLive axTTLLive;
        private TPSForNeuroTrader.TPSForNeuroTrader tpsr;

        public Timer timer1;

        public TTLApiBioDataProvider(MainNeuroXModel model, 
            LocalDatabaseConnector localDatabaseConnector,
            IniFileReader iniFileReader)
            :base(model, localDatabaseConnector)
        {
            usbConnectionStr = iniFileReader.Read("TPSUSBPort", "BioData", "\\\\.\\COM5");
            bioDataTickInterval = Int32.Parse(iniFileReader.Read("BioDataTickInterval", "BioData", "500"));

            try
            {
                InitializeaxTTLLive();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                string filePathName = AppDomain.CurrentDomain.BaseDirectory + "\\3dparty\\TTLLiveCtrl.dll";
                Register_Dlls(filePathName);
                InitializeaxTTLLive();
            }

            tpsr = new TPSForNeuroTrader.TPSForNeuroTrader(1000, this.axTTLLive, usbConnectionStr);
            while (this.tpsr.getState() != e_cs.CONNECTED)
            {
                try
                {
                    this.tpsr.conectToDevice();
                }
                catch (Exception etps)
                {
                    MessageBox.Show(etps.Message);
                }

                if (tpsr.getState() != e_cs.CONNECTED)
                {
                    DialogResult dr = MessageBox.Show("Check your device and press OK to Connect! Cancel to proced without device.", "Device Connection", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                    if (dr == DialogResult.Cancel)
                    {
                        break;
                    }
                }
            }

            // -- initialize logic
            timer1 = new Timer();
            timer1.Interval = bioDataTickInterval;
            timer1.Tick += new EventHandler(timer1_Tick);

            if (tpsr.getState() != e_cs.STARTED)
            {
                if (!tpsr.startDevice())
                {
                    MessageBox.Show("Was not able to start read BIO data from device.");
                }
            }
        }

        public override void StartProcessing()
        {
            timer1.Start();
        }

        public override void StopProcessing()
        {
            timer1.Stop();
        }

        private void Register_Dlls(string filePath)
        {
            try
            {
                string arg_fileinfo = " \"" + filePath + "\"";
                Process reg = new Process();
                reg.StartInfo.FileName = "regsvr32.exe";
                reg.StartInfo.Arguments = arg_fileinfo;
                reg.StartInfo.Verb = "runas";
                reg.Start();
                reg.WaitForExit();
                reg.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InitializeaxTTLLive()
        {
            axTTLLive = new AxTTLLive();
            axTTLLive.BeginInit();
            byte[] bytes = Convert.FromBase64String("AAEAAAD/////AQAAAAAAAAAMAgAAAFdTeXN0ZW0uV2luZG93cy5Gb3JtcywgVmVyc2lvbj00LjAuMC4wLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPWI3N2E1YzU2MTkzNGUwODkFAQAAACFTeXN0ZW0uV2luZG93cy5Gb3Jtcy5BeEhvc3QrU3RhdGUBAAAABERhdGEHAgIAAAAJAwAAAA8DAAAAGQAAAAIBAAAAAQAAAAAAAAAAAAAAAAQAAAAACQAACw==");
            using (MemoryStream ms = new MemoryStream(bytes, 0, bytes.Length))
            {
                ms.Write(bytes, 0, bytes.Length);
                ms.Position = 0;
                object deserealized = new BinaryFormatter().Deserialize(ms);
                axTTLLive.OcxState = (AxHost.State)deserealized;
            }
            axTTLLive.EndInit();

            axTTLLive.CreateControl();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TPSData tpsData = tpsr.GetTPSData();

            if (tpsr.getState() != e_cs.STARTED)
            {
                return;
            }

            // send signal that new data come
            var bioData = new BioData();
            bioData.time = tpsData.dt;
            bioData.temperature = tpsData.Temp;
            bioData.heartRate = tpsData.HR;
            bioData.skinConductance = tpsData.SC;
            bioData.accX = tpsData.AccX;
            bioData.accY = tpsData.AccY;
            bioData.accZ = tpsData.AccZ;
            FillApplicaitonStates(bioData);

            bioData.id = localDatabaseConnector.WriteBioData(bioData);

            NotifyObservers(BioDataEvent.NewBioDataTick, bioData);
        }
    }

}
