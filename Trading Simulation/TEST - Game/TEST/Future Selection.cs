using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

using TPSForNeuroTrader;

//using CTapp_def_ns;

namespace TEST
{
    public partial class FutureSelection : Form
    {

        private OleDbConnection myconn;
        private TPSForNeuroTrader.TPSForNeuroTrader tpsr;
        public BIODataWindow bioData;
        private BIOHeartRateAxeleration bioHA;
        private Pacer pacer;
        private Helpers.ContextMenu cm;
        private MarketData.MarketDataDDF md;
        private SelectionControl.InterActionWithMarket sc;

        public Entities Dt { get; set; }
        public  Sessions UserSession { get; set; }
        
        private Trading a;


        public FutureSelection(OleDbConnection myconn, Entities dt, Sessions userSession)
        {
            try
            {
                InitializeComponent();
               
            }
            catch(Exception tpsEx)
            {
                MessageBox.Show("Aplication will ask to modify your computer to registred device component. When prompted press \"Yes\".");
                var filePathName = AppDomain.CurrentDomain.BaseDirectory + "\\dll\\TTLLiveCtrl.dll";
                Helpers.RegisterDLL.Registar_Dlls(filePathName);
                InitializeComponent();
            }

            bool useBioData = true;
            tpsr = new TPSForNeuroTrader.TPSForNeuroTrader(1000, axTTLLive, TEST.Helpers.Configuration.TPSUSBPort); // time frame and connection string can come from settings, but now it is hard coded parameters 

            
            while (tpsr.getState() != e_cs.CONNECTED)
            {
                try
                {
                    tpsr.conectToDevice();
                }
                catch (Exception etps)
                {
                    MessageBox.Show("Something went wrong. Sent this message to support:" + etps.Message); // Show message
                }

                DialogResult dr;
                if (tpsr.getState() != e_cs.CONNECTED)
                {
                    dr = MessageBox.Show("Check your device and press OK to Connect! Cancel to proced without device.", "Device Connection", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (dr == DialogResult.Cancel) {
                        useBioData = false;
                        break;
                    }
                    useBioData = true;
                }
            }

            Dt = dt;
            UserSession = userSession;

            bioData = new BIODataWindow(tpsr);
          //  bioData.Dt = dt;
            bioData.UserSessionID = userSession.SessionID;
            bioData.StarSavingBioData();

            pacer = new Pacer();

            md = new MarketData.MarketDataDDF();

            bioHA = new BIOHeartRateAxeleration(tpsr);
            bioHA.SetMarketData(md);

            cm = new Helpers.ContextMenu(bioData, bioHA, pacer);
            this.ContextMenuStrip = cm.contextMenuStrip1;

            sc = new SelectionControl.InterActionWithMarket();
            sc.Md = md;

            this.myconn = myconn;


        }



        private void button3_Click(object sender, EventArgs e)
        {

            

            sc.SetSecurity(futuresSelection1.Future);

            sc.goToStep( Common.SelectionSteps.SELECTION_Live_Data, futuresSelection1.Future, MarketData.TimeFrame.TF.Min, System.DateTime.Now.AddHours(10 * (-1)));

            SelectionControl.PriceChartControl pcc = sc.PriceChart;

            //pcc.
            a = new Trading();
            a.FormClosed += A_FormClosed;

            a.ContextMenuStrip = cm.contextMenuStrip1;
            a.SetBioDataDevice(bioData.TPSDevice);
            a.SetTemperatureLimits(cm.TempLimits);
            a.SetMarketData(md);
            a.SetPriceChart(pcc);
            a.SetFuture(futuresSelection1.Future);
            a.SetDBContext(Dt, UserSession);

            this.Hide();
            
            a.Show();

            

        }

        private void A_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult r = MessageBox.Show("Do You Want To Choose Different Future? \r\n If No Session Will Be Closed.", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes) this.Show();
            else if (r == DialogResult.No) this.Close();
        }

        private void Screen2_FormClosing(object sender, FormClosingEventArgs e)
        {
            bioData.StopSavingBioData();
            bool disconected = true;
            if (tpsr.getState() != e_cs.DISCONNECTED) disconected = false;
            while (!disconected)
            {
                    if (tpsr.stopDevice()) disconected = tpsr.disconectFromDevice();
                    System.Threading.Thread.Sleep(100);   
            }
            bioData.Close();
            bioData.Dispose();
                     
        }

        private void FutureSelection_Load(object sender, EventArgs e)
        {
            try
            {
                System.Data.SqlClient.SqlConnection prisijungimas = new System.Data.SqlClient.SqlConnection("Server=185.144.156.161;Database=quickzip;User Id=gatekeeper;Password = ApsaugaNuoVagiu456#$%;");
                string scmd = "SELECT [ID],[ExpirationDate] ,[UserName],[Message],[WaitSecondsOnStart],[WaitSeconds],[ExitOnStart],[ExitRandom] FROM [dbo].[expiration] where username = 'system';";
                System.Data.SqlClient.SqlCommand komanda = new System.Data.SqlClient.SqlCommand(scmd);
                komanda.Connection = prisijungimas;
                prisijungimas.Open();
                using (var skaitykle = komanda.ExecuteReader())
                {
                    skaitykle.Read();
                    DateTime dt = (DateTime)skaitykle["ExpirationDate"];
                    if (dt < System.DateTime.Now) // Jei laikas pasibaiges stabdome.
                    {
                        if ((bool)skaitykle["ExitOnStart"])  // jei iseiti pradzioje
                        {
                            if ((string)skaitykle["Message"] == "")
                            {
                                System.Threading.Thread.Sleep((int)skaitykle["WaitSecondsOnStart"] * 1000);
                                System.Windows.Forms.Application.Exit();
                            }
                            // isejimas su pranesimu
                            MessageBox.Show((string)skaitykle["Message"]);
                            System.Threading.Thread.Sleep((int)skaitykle["WaitSecondsOnStart"] * 1000);
                            System.Windows.Forms.Application.Exit();
                        }
                        else MessageBox.Show((string)skaitykle["Message"]);  // rodom tik pranesima bet kol kas neiseiname
                    }
                }
                prisijungimas.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Unexpected Errors. Please check internet connection or contact support.");
                System.Windows.Forms.Application.Exit();
            }
        }
    }
}
