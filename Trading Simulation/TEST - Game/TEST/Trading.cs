using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;

using SelectionControl;

namespace TEST
{
    public partial class Trading : Form
    {


        private PriceChartControl _pcc;
        private MarketData.MarketDataDDF _md;
        private string _tradingSymbol;
        Entities Dt;
        Sessions userSession;
        System.Globalization.NumberFormatInfo nfi;

        private SqlConnection sqlConenstion = new SqlConnection("Server=185.144.156.161;Database=nt;User Id=neurouser;Password = AlggoMalgo123;");


        public Helpers.TemperatureLimits TemperatureLimits { get; set; }

        public void SetBioDataDevice(TPSForNeuroTrader.TPSForNeuroTrader tpsBioData)
        {
            bioHeartRateAccelerationControl1.SetBioDataDevice(tpsBioData);
            tradingManager1.LastBioData = tpsBioData.GetTPSData();
             
        }

        public void SetTemperatureLimits(Helpers.TemperatureLimits tl)
        {
            this.TemperatureLimits = tl;
            tradingManager1.TemperatureLimits = tl;
        }

        public Trading()
        {
            InitializeComponent();
            nfi = new System.Globalization.NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
        }




        public void SetDBContext(Entities ent, Sessions userSession)
        {
            Dt = ent;
            this.userSession = userSession;

          //  tradingManager1.Dt = Dt;
            tradingManager1.UserSession = userSession;
        }

        public void SetFuture(MarketData.Future f)
        {
            _tradingSymbol = f.Name;
            tradingManager1.TradingSymbol = _tradingSymbol;
        }

        public void SetMarketData(MarketData.MarketDataDDF md)
        {
            _md = md;
            _md.OnPriceTick += _md_OnPriceTick;
            _md.OnPriceTick += bioHeartRateAccelerationControl1._OnPriceTick;
            _md.OnPriceTick += _md_OnPriceTickSaveToDB;
        }


        string cmdStr = "";
        int i = 0;

        async private void _md_OnPriceTickSaveToDB(object source, MarketData.PriceTickEventArgs e)
        {


            i++;
            cmdStr = cmdStr +

            "INSERT INTO[dbo].[PriceData] ([SessionID] ,[Symbol] ,[Price] ,[PriceAsk] ,[PriceBid] , [Volume], [TimeFrame], [Time])" +
            " VALUES("+ userSession.SessionID +" ,'"+ e.Symbol +"' , "+  e.Price.ToString(nfi) +" ,"+ e.PriceAsk.ToString(nfi) + ", " + e.PriceBid.ToString(nfi) + ", "+ e.Volume.ToString(nfi) + ", 'Tick' ,'"+ e.PriceTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'); \n\r";



            if (i > 30) 
            {
                try
                {
                    SqlCommand querySaveStaff = new SqlCommand(cmdStr);
                    querySaveStaff.Connection = sqlConenstion;
                    if (sqlConenstion.State != ConnectionState.Open) sqlConenstion.Open();
                    int ii = await querySaveStaff.ExecuteNonQueryAsync();

                    cmdStr = "";
                    i = 0;

                }
                catch (Exception exx)
                {
                 //   MessageBox.Show(" Please contact support.  " + exx.Message);
                }
                
            }

        }
        

        private void _md_OnPriceTick(object source, MarketData.PriceTickEventArgs e)
        {
            tradingManager1.receiveNewPrice(new TradingAccount.Price(e.Symbol, e.Price, e.Price));
        }

        private void button3_Click(object sender, EventArgs e)
        {
          //  breathPacerControl1.Stop();
            Close();
        }

        private void Live_Trading_Open_Session_Load(object sender, EventArgs e)
        {
            try
            {
                System.Data.SqlClient.SqlConnection prisijungimas = new System.Data.SqlClient.SqlConnection("Server=185.144.156.161;Database=quickzip;User Id=gatekeeper;Password = ApsaugaNuoVagiu456#$%;");
                string scmd = "SELECT [ID],[ExpirationDate] ,[UserName],[Message],[WaitSecondsOnStart],[WaitSeconds],[ExitOnStart],[ExitRandom] FROM [dbo].[expiration] where username = '" + userSession.Users.Email + "';";
                System.Data.SqlClient.SqlCommand komanda = new System.Data.SqlClient.SqlCommand(scmd);
                komanda.Connection = prisijungimas;
                prisijungimas.Open();
                using (var skaitykle = komanda.ExecuteReader())
                {
                    skaitykle.Read();
                    DateTime dt = (DateTime)skaitykle["ExpirationDate"];
                    if (dt < System.DateTime.Now) // Jei laikas pasibaiges stabdome.
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
                }
                prisijungimas.Close();
            }
            catch (Exception ee)
            {
                System.Windows.Forms.Application.Exit();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
         //   breathPacerControl1.Stop();
            this.Close();
        }

        public void SetBreather(double breathsPerMinute, int cyclesToFinish) {

            List<BreathPacer.BreathSettings> breathSettings = new List<BreathPacer.BreathSettings>()
                    { new BreathPacer.BreathSettings() { BreathsPerMinute = breathsPerMinute,    CyclesToFinish = cyclesToFinish } };
         //   breathPacerControl1.Start(breathSettings);
        }

        

        public void SetPriceChart(PriceChartControl pcc)
        {
            _pcc = pcc;
            //pcc.BackColor = System.Drawing.Color.Black;
            this.chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            pcc.Dock = System.Windows.Forms.DockStyle.Fill;
            //pcc.Location = new System.Drawing.Point(205, 103);
            //this.chart1.Size = new System.Drawing.Size(429, 267);

            this.tableLayoutPanel1.Controls.Remove(chart1);

            this.tableLayoutPanel1.Controls.Add(pcc, 1, 1);

            if (_pcc.P.p.Count > 0)
            {
                MarketData.PriceCandle x = _pcc.P.p[_pcc.P.p.Count - 1];
                TradingAccount.Price y = new TradingAccount.Price(_pcc.P.f.Name, x.Close, x.Close);
                tradingManager1.receiveNewPrice(y);
            }

        }

        private void Live_Trading_Open_Session_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.tableLayoutPanel1.Controls.Remove(_pcc);
            _md.OnPriceTick -= _md_OnPriceTick;
            // Close session

        }
    }
}
