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

using SelectionControl;
using MarketData;
using Common;

namespace TEST
{
    public partial class Centre_Yourself : Form
    {
        private OleDbConnection myconn;
        private BIOData bioData;

        private InterActionWithMarket _sc;
        public InterActionWithMarket Sc
        {
            get { return _sc; }
            set
            {
                int marginW = this.tableLayoutPanel1.Size.Width / 20;
                int marginH = this.tableLayoutPanel1.Size.Height / 20;

                _sc = value;
                _sc.Hide();
                _sc.InfoTextColor = System.Drawing.Color.White;
                _sc.Dock = System.Windows.Forms.DockStyle.Fill;
                _sc.Anchor = System.Windows.Forms.AnchorStyles.None;
                _sc.Location = new System.Drawing.Point(marginW, marginH);
                _sc.Name = "MarketSelection";
                _sc.Size = new System.Drawing.Size(
                    this.tableLayoutPanel1.Size.Width  - 2 /*marginW*/, 
                    this.tableLayoutPanel1.Size.Height - 2 /*marginH*/);
                //_md.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                _sc.TabIndex = 3;
                _sc.TabStop = false;
                this.tableLayoutPanel1.Controls.Add(this._sc, 0, 2);
            }
        }

        private string name;

        OleDbDataReader reader;

        public Centre_Yourself(OleDbConnection myconn, BIOData bio, int Session_Component_ID, int Sub_Component_ID, int Sub_Component_Protocol_ID, int UserID, string name, InterActionWithMarket scc)
        {
            this.myconn = myconn;

            this.name = name;

            bioData = bio;

            InitializeComponent();

            Sc = scc;

            bioHeartRateAccelerationControl1.SetBioDataDevice(bio.TPSDevice);


            //connection.Open();
            OleDbCommand select = new OleDbCommand();
            select.Connection = myconn;
            select.CommandText = "Select Sub_Protocol_ID, Type, Text, DataToInit, BreathsPerMinute, CyclesToFinish, Ordering From Sub_Protocol Where Session_Component_ID = " + Session_Component_ID + " and  Sub_Component_ID = "+ Sub_Component_ID + " and Sub_Component_Protocol_ID  = " + Sub_Component_Protocol_ID + " order by Ordering";  // and Type = 'Selection'
            reader = select.ExecuteReader();
            if (reader.Read())
            {
                
                bioData.Session_Component_ID = Session_Component_ID; // ??? it is a mess need to refactor DB
                bioData.Sub_Component_ID = Sub_Component_ID; // ??? it is a mess need to refactor DB
                bioData.Sub_Component_Protocol_ID = Sub_Component_Protocol_ID;
                bioData.Sub_Protocol_ID = 0;  // this is know only in the process fo protocol and it changes in live manner
                bioData.Participant_ID = UserID;
                bioData.StarSavingBioData("start - " + name);
                getNextStep(reader);

            }
        }



        private void Centre_Yourself_Load(object sender, EventArgs e)
        {
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            breathPacerControl1.Start(new Performance_Objective(), 3);
        }

        // breathPacerControl1.Start(new Performance_Objective(), 3);


        private void breathPacerControl1_RoutineFinished(object sender, EventArgs e)
        {
            _sc.Hide();
            if (reader.Read())
            {
                getNextStep(reader);
            }
            else
            {
                bioData.StopSavingBioData("stop - " + name); // stop recording data from BIO diviece !!!!!!!!
            }
        }

        private void getNextStep(OleDbDataReader reader)
        {
            // listBox1.Items.Add(reader[1].ToString() + "," + reader[2].ToString());
            int sub_Protocol_ID = (int)reader.GetValue(0);
            string type = reader.GetValue(1).ToString();
            string text = reader.GetValue(2).ToString();
            string dataToInit = reader.GetValue(3).ToString();
            double breathsPerMinute = System.Convert.ToDouble(reader.GetValue(4));
            int    cyclesToFinish = System.Convert.ToInt16(reader.GetValue(5));

            bioData.Sub_Protocol_ID = sub_Protocol_ID;

            
            List<BreathPacer.BreathSettings> breathSettings = new List<BreathPacer.BreathSettings>()
                    { new BreathPacer.BreathSettings() { BreathsPerMinute = breathsPerMinute,    CyclesToFinish = cyclesToFinish } };            
            breathPacerControl1.Start(breathSettings);
            showContent(type, text, dataToInit);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="text"></param>
        /// <param name="imgLocation"></param>
        private void showContent(string type, string text, string dataToInit) {

            if (type == "Text")
            {
                pictureBox1.Hide();
                label2.Text = text;
            }
            if (type == "Text_Image")
            {
                label2.Text = text;
                pictureBox1.ImageLocation = dataToInit;
                pictureBox1.Load();
                pictureBox1.Show();
            }

            if (type == "Selection")
            {
                label2.Text = text;
                pictureBox1.Hide();
                string[] parameters = dataToInit.Split('|'); // configuration data from DB

                /*
                 *  parameters[0] - SelectionStep
                 *  parameters[1] - Future (EUR/USD, GBP/USD ... etc...) 
                 *  parameters[2] - TimeFrame ("Day", "Hour", "Min","Min20", "Min5", "None", "Tick")
                 *  parameters[2] - Amount of history data to display in hours. (0.0123, 0.1, 1, 2 5 ...)
                 */





                Future future = null;
                TimeFrame.TF tf = TimeFrame.TF.Min; // detault
                double amountOfhistory = 1;  //default

                SelectionSteps step = SelectionStep.stringToSelectionStep(parameters[0]);
                if (parameters.Count() > 2) // must be 1 but with a comment it works when it is 2, commment is also mandatory 
                {
                    if (parameters[1] != "Selected") // Is Future/Security is not selected lets assign it
                    if (_sc.Security == null) future = new Future(parameters[1]); // if future is not set lets set it from DB if it is already set lets use previous one (methoed for setting future is not set yet).
                    if ((_sc.Security == null)&&(future == null)) future  = new Future("EUR/USD"); // in case if pararam is not set and emty les use default :? :/ :\
                    tf = TimeFrame.getTFFromString(parameters[2]);
                    amountOfhistory = System.Convert.ToDouble(parameters[3]);  // into hours
                }
                _sc.goToStep(step, future, tf, System.DateTime.Now.AddHours(amountOfhistory * (-1))); // we are not adding we are substracting
             //   if(!this.tableLayoutPanel1.Controls.Contains(this._sc)) this.tableLayoutPanel1.Controls.Add(this._sc, 0, 2); // does control is in from and visible
              //  Sc = _sc;
              _sc.Focus();
                _sc.Show();
            }

            if (type == "Trading")
            {
                label2.Text = text;
                pictureBox1.Hide();
                string[] parameters = dataToInit.Split('|'); // configuration data from DB

                /*
                 *  parameters[0] - SelectionStep
                 *  parameters[1] - Future (EUR/USD, GBP/USD ... etc...) 
                 *  parameters[2] - TimeFrame ("Day", "Hour", "Min","Min20", "Min5", "None", "Tick")
                 *  parameters[2] - Amount of history data to display in hours. (0.0123, 0.1, 1, 2 5 ...)
                 */

                Future future = null;
                TimeFrame.TF tf = TimeFrame.TF.Min; // detault
                double amountOfhistory = 1;  //default

                SelectionSteps step = SelectionStep.stringToSelectionStep(parameters[0]);
                if (parameters.Count() > 2) // must be 1 but with a comment it works when it is 2, commment is also mandatory 
                {
                    if (parameters[1] != "Selected") // Is Future/Security is not selected lets assign it
                        if (_sc.Security == null) future = new Future(parameters[1]); // if future is not set lets set it from DB if it is already set lets use previous one (methoed for setting future is not set yet).
                    if ((_sc.Security == null) && (future == null)) future = new Future("EUR/USD"); // in case if pararam is not set and emty les use default :? :/ :\
                    tf = TimeFrame.getTFFromString(parameters[2]);
                    amountOfhistory = System.Convert.ToDouble(parameters[3]);  // into hours
                }

                Live_Trading_Open_Session tradingWindow = new Live_Trading_Open_Session();

                tradingWindow.SetBioDataDevice(bioData.TPSDevice);

                tradingWindow.FormClosed += TradingWindow_FormClosed;
                Hide(); // hide current last window
                _sc.goToStep(SelectionSteps.SELECTION_Live_Data, future, tf, System.DateTime.Now.AddHours(amountOfhistory * (-1)));
                tradingWindow.SetPriceChart(_sc.PriceChart);
                tradingWindow.SetBreather(breathPacerControl1.BreathsPerMinute, (int)breathPacerControl1.CyclesToFinish);
                tradingWindow.ContextMenuStrip = this.ContextMenuStrip;
                tradingWindow.Show();

            }
        }

        private void TradingWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            bioData.StopSavingBioData("stop - (in trading window) - " + name);
            this.Close();            
        }

        private void Centre_Yourself_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.tableLayoutPanel1.Controls.Remove(_sc);
            if (e.CloseReason == CloseReason.UserClosing)
            {
                bioData.StopSavingBioData("stop - user closed window - " + name);
            }
        }

    }
}
