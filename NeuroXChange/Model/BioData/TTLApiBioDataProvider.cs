using AxTTLLiveCtrlLib;
using NeuroXChange.Common;
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

        private string databaseLocation;
        private string tableName;
        private bool saveBioData;
        private OleDbConnection myconn;
        private OleDbCommand cmd;

        private AxTTLLive axTTLLive;
        private TPSForNeuroTrader.TPSForNeuroTrader tpsr;

        public Timer timer1;

        public int Session_Component_ID { get; set;}

        public int Sub_Component_ID { get; set; }

        public int Sub_Component_Protocol_ID { get; set; }

        public int Sub_Protocol_ID { get; set; }

        public int Participant_ID { get; set; }

        public string AdditionalData { get; set; }

        public TTLApiBioDataProvider(IniFileReader iniFileReader)
        {
            usbConnectionStr = iniFileReader.Read("TPSUSBPort", "BioData");
            databaseLocation = iniFileReader.Read("Location", "Database");
            tableName = iniFileReader.Read("Table", "Database");
            saveBioData = bool.Parse(iniFileReader.Read("SaveBioData", "Database"));

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
            Session_Component_ID = 2;
            Sub_Component_ID = 4;
            Sub_Component_Protocol_ID = 0;
            //Sub_Component_Protocol_ID = 71;   // manually set "comp day" training
            Sub_Protocol_ID = 0;
            Participant_ID = 1;
            AdditionalData = "";

            myconn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + databaseLocation);
            try
            {
                myconn.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show("Was not able to establish a connection with local database. \r\n" + e.Message);
            }
            cmd = new OleDbCommand();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = myconn;

            timer1 = new Timer();
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

            // TODO: temporary, remove later
            // tpsData.dt = DateTime.Now;

            int id = -1;
            if (saveBioData
                && Sub_Component_Protocol_ID != 0   // Sub_Component_Protocol_ID (as TrainingType) should exists
                )
            {
                id = writeData(tpsData);
            }

            // send signal that new data come
            var bioData = new BioData();
            bioData.psychophysiological_Session_Data_ID = id;
            bioData.time = tpsData.dt;
            bioData.temperature = tpsData.Temp;
            bioData.hartRate = tpsData.HR;
            bioData.skinConductance = tpsData.SC;
            bioData.accX = tpsData.AccX;
            bioData.accY = tpsData.AccY;
            bioData.accZ = tpsData.AccZ;
            bioData.session_Component_ID = Session_Component_ID;
            bioData.sub_Component_ID = Sub_Component_ID;
            bioData.sub_Component_Protocol_ID = Sub_Component_Protocol_ID;
            bioData.sub_Protocol_ID = Sub_Protocol_ID;
            bioData.participant_ID = Participant_ID;
            bioData.data = AdditionalData;
            NotifyObservers(BioDataEvent.NewBioDataTick, bioData);
        }

        // writes biodata and returns id in database table
        private int writeData(TPSData data)
        {
            if (data.Gesture == double.NaN)
            {
                data.Gesture = 0.0;
            }
            cmd.CommandText = string.Concat(new object[]
            {
                "insert into " + tableName + "\r\n                          ([Time], Temperature, HartRate, SkinConductance, AccX, AccY, AccZ, Session_Component_ID, Sub_Component_ID, Sub_Component_Protocol_ID, Sub_Protocol_ID, Participant_ID, Data) \r\n                   values ('",
                data.dt,
                "','",
                data.Temp,
                "','",
                data.HR,
                "','",
                data.SC,
                "', '",
                data.AccX,
                "','",
                data.AccY,
                "','",
                data.AccZ,
                "',",
                Session_Component_ID,
                ", ",
                Sub_Component_ID,
                ", ",
                Sub_Component_Protocol_ID,
                ", ",
                Sub_Protocol_ID,
                ", ",
                Participant_ID,
                ", '",
                AdditionalData,
                "')"
            });

            cmd.ExecuteNonQuery();

            if (AdditionalData != "")
            {
                AdditionalData = "";
            }

            cmd.CommandText = "Select @@Identity";
            int id = (int)cmd.ExecuteScalar();

            return id;
        }
    }

}
