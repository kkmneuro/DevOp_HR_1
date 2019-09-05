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

using System.Windows.Forms.DataVisualization.Charting;

using TPSForNeuroTrader;

using System.Data.SqlClient;

namespace TEST
{
    public partial class BIODataWindow : Form
    {
        /// <summary>
        /// Are we saving bio data to DB ir not 
        /// </summary>
        public bool SavingData { get; private set; } // timer is always runing, but bio data is writin into db only when this var is true


        public int UserSessionID { get; set; }

        private SqlConnection sqlConenstion = new SqlConnection("Server=185.144.156.161;Database=nt;User Id=neurouser;Password = AlggoMalgo123;");

        /// <summary>
        /// Last measure of TPS data
        /// </summary>
        public TPSData LastBioData { get; private set; }


        /// <summary>
        /// 1st level
        /// </summary>
        public int Session_Component_ID { get; set; }       // 1st level. ex: 2- Trader training
        /// <summary>
        /// 2nd level
        /// </summary>
        public int Sub_Component_ID { get; set; }           // 2nd level. ex: 4- Rutines
        /// <summary>
        /// 3rd level
        /// </summary>
        public int Sub_Component_Protocol_ID { get; set; }  // 3rd level. ex: 70-Night before rutine
        /// <summary>
        /// 4th level
        /// </summary>
        public int Sub_Protocol_ID { get; set; }            // 4th level. ex: 58-Center youself    

        public int Participant_ID { get; set; }            // UserId    

        public string AdditionalData { get; set; }  // saving additional data like start or end



        private TPSForNeuroTrader.TPSForNeuroTrader tpsr; // access to device

        public TPSForNeuroTrader.TPSForNeuroTrader TPSDevice
        { get { return tpsr;  } private set { tpsr = value; } }

        private OleDbConnection myconn; // connestion to MS access file 
        private OleDbCommand cmd;       // measurement data and who is writing 

     
        string strdb = "Neuro-Xchange_Psychophysiology1.mdb"; //ConfigurationManager.AppSettings["DBLocation"];



        bool useTPSDevice = false;

        double minX, maxX;  // scale size
        int countX;

        System.Globalization.NumberFormatInfo nfi;



        public BIODataWindow(TPSForNeuroTrader.TPSForNeuroTrader tpsrX)
        {

            if (tpsrX == null) useTPSDevice = false;
            else useTPSDevice = true;

            if (useTPSDevice)
            {
                AdditionalData = "";
                Sub_Protocol_ID = 0;

            /*    myconn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source='" + strdb + "'");

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
                */
                InitializeComponent();

                chart1.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;
                chart1.ChartAreas[0].AxisY.IsStartedFromZero = false;
                chart1.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;

                chart2.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;
                chart2.ChartAreas[0].AxisY.IsStartedFromZero = false;
                chart2.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;

                chart3.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;
                chart3.ChartAreas[0].AxisY.IsStartedFromZero = false;
                chart3.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;

                chart4.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;
                chart4.ChartAreas[0].AxisY.IsStartedFromZero = false;
                chart4.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;


                chart1.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss";
                chart1.ChartAreas[0].AxisX.LabelStyle.IsEndLabelVisible = false;
                chart2.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss";
                chart2.ChartAreas[0].AxisX.LabelStyle.IsEndLabelVisible = false;
                chart3.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss";
                chart3.ChartAreas[0].AxisX.LabelStyle.IsEndLabelVisible = false;
                chart4.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss";
                chart4.ChartAreas[0].AxisX.LabelStyle.IsEndLabelVisible = false;


               // static_Scale();

                tpsr = tpsrX;

                


                if (tpsr.getState() != e_cs.STARTED) // Start reading data from device !!!
                {
                    if (!tpsr.startDevice())
                    {
                        MessageBox.Show("Was not able to start read BIO data from device.");
                        return;
                    }
                }

                
                nfi = new System.Globalization.NumberFormatInfo();
                nfi.NumberDecimalSeparator = ".";

                sqlConenstion.Open();
                this.timer1.Start();
                LastBioData = new TPSData();

            }
        }

        public void static_Scale()
        {
            // Temperature                 
            chart1.ChartAreas[0].AxisY.Minimum = 20;
            chart1.ChartAreas[0].AxisY.Maximum = 40;

            // heart rate
            chart2.ChartAreas[0].AxisY.Minimum = 45;
            chart2.ChartAreas[0].AxisY.Maximum = 120;

            // Skin Cunductance
            chart3.ChartAreas[0].AxisY.Minimum = 0;
            chart3.ChartAreas[0].AxisY.Maximum = 5;

            // X Y Z
            chart4.ChartAreas[0].AxisY.Minimum = -100;
            chart4.ChartAreas[0].AxisY.Maximum = 100;
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            if (useTPSDevice)
            {
                TPSData data = tpsr.GetTPSData();
                
                
                LastBioData.AccX = data.AccX;
                LastBioData.AccY = data.AccY;
                LastBioData.AccZ = data.AccZ;
                LastBioData.dt = data.dt;
                LastBioData.HR = data.HR;
                LastBioData.SC = data.SC;
                LastBioData.Temp = data.Temp;

                if (chart1.Series["Temperature"].Points.Count > 200) // in charts show only 600 points.
                {
                    chart1.Series["Temperature"].Points.RemoveAt(0);

                    minX   = (chart1.Series["Temperature"].Points[0]).XValue;
                    countX = chart1.Series["Temperature"].Points.Count -1;
                    maxX = (chart1.Series["Temperature"].Points[countX]).XValue;  

                    chart1.ChartAreas[0].AxisX.Minimum = minX;
                    chart1.ChartAreas[0].AxisX.Maximum = DateTime.FromOADate(maxX).AddSeconds(5).ToOADate();


                    chart2.Series["Heart Rate"].Points.RemoveAt(0);
                    chart2.ChartAreas[0].AxisX.Minimum = minX;
                    chart2.ChartAreas[0].AxisX.Maximum = DateTime.FromOADate(maxX).AddSeconds(5).ToOADate();

                    chart3.Series["Skin Conductance"].Points.RemoveAt(0);
                    chart3.ChartAreas[0].AxisX.Minimum = minX;
                    chart3.ChartAreas[0].AxisX.Maximum = DateTime.FromOADate(maxX).AddSeconds(5).ToOADate();

                    chart4.Series["AccX"].Points.RemoveAt(0);
                    chart4.Series["AccY"].Points.RemoveAt(0);
                    chart4.Series["AccZ"].Points.RemoveAt(0);
                    chart4.ChartAreas[0].AxisX.Minimum = minX;
                    chart4.ChartAreas[0].AxisX.Maximum = DateTime.FromOADate(maxX).AddSeconds(5).ToOADate();
                }


                chart1.Series["Temperature"].Points.AddXY(data.dt, data.Temp);

                chart2.Series["Heart Rate"].Points.AddXY(data.dt, data.HR);
                chart3.Series["Skin Conductance"].Points.AddXY(data.dt, data.SC);

                
                chart4.Series["AccX"].Points.AddXY(data.dt, data.AccX);
                chart4.Series["AccY"].Points.AddXY(data.dt, data.AccY);
                chart4.Series["AccZ"].Points.AddXY(data.dt, data.AccZ);

                update_scales();


                if (SavingData || (AdditionalData != ""))
                {
                 //   writeDataLocal(data);
                    writeDataServer(data);
                }
            }

        }

        private void update_scales()
        {
            chart1.ChartAreas[0].AxisY.Maximum = Double.NaN; // sets the Maximum to NaN
            chart1.ChartAreas[0].AxisY.Minimum = Double.NaN; // sets the Minimum to NaN
            chart1.ChartAreas[0].RecalculateAxesScale();

            chart2.ChartAreas[0].AxisY.Maximum = Double.NaN; // sets the Maximum to NaN
            chart2.ChartAreas[0].AxisY.Minimum = Double.NaN; // sets the Minimum to NaN
            chart2.ChartAreas[0].RecalculateAxesScale();

            chart3.ChartAreas[0].AxisY.Maximum = Double.NaN; // sets the Maximum to NaN
            chart3.ChartAreas[0].AxisY.Minimum = Double.NaN; // sets the Minimum to NaN
            chart3.ChartAreas[0].RecalculateAxesScale();

            chart4.ChartAreas[0].AxisY.Maximum = Double.NaN; // sets the Maximum to NaN
            chart4.ChartAreas[0].AxisY.Minimum = Double.NaN; // sets the Minimum to NaN
            chart4.ChartAreas[0].RecalculateAxesScale();
        }

        private void writeDataLocal(TPSData data)
        {
            //write data from TPS devive into database
            if (data.Gesture == Double.NaN) data.Gesture = 0; // ??? why it is not working
            cmd.CommandText =
                    @"insert into Sub_Component_Protocol_Psychophysiological_Session_Data_TPS
                          ([Time], Temperature, HartRate, SkinConductance, AccX, AccY, AccZ, Session_Component_ID, Sub_Component_ID, Sub_Component_Protocol_ID, Sub_Protocol_ID, Participant_ID, Data) 
                   values ('" + data.dt + "','" + data.Temp + "','" + data.HR + "','" + data.SC + "', '" // TODO: gesture is 0 as it producing Nan
                                  + data.AccX + "','" + data.AccY + "','" + data.AccZ + "'," 
                                   + Session_Component_ID + ", "
                                   + Sub_Component_ID + ", "
                                   + Sub_Component_Protocol_ID + ", " 
                                   + Sub_Protocol_ID + ", " 
                                   + Participant_ID
                                   + ", '" + AdditionalData + "')";
            cmd.ExecuteNonQuery();

            if (AdditionalData != "") AdditionalData = "";  // aditional data is writen only once
        }


        string cmdStr = "";
        int i = 0;
        async private  void writeDataServer(TPSData data)
        {



            i++;
            cmdStr = cmdStr + @"INSERT INTO [dbo].[BioData]([SessionID],[DateTime],[Temperature],[HR],[SC],[AccX],[AccY],[AccZ]) "
+"VALUES( "+ UserSessionID + ", '" + data.dt + "', " + data.Temp.ToString(nfi) + ", "+ data.HR.ToString(nfi) + ",  " + data.SC.ToString(nfi) + ", " + data.AccX.ToString(nfi) + ",  " + data.AccY.ToString(nfi) + ", " + data.AccZ.ToString(nfi) + "); \n\r ";



            if (AdditionalData != "") AdditionalData = "";  // aditional data is writen only once (it was only for local DB and for routines)

            if (i > 30) 
            {
                try
                {
                    SqlCommand querySaveStaff = new SqlCommand(cmdStr);
                    querySaveStaff.Connection = sqlConenstion;
                    if (sqlConenstion.State != ConnectionState.Open) sqlConenstion.Open();
                    int ii = await querySaveStaff.ExecuteNonQueryAsync();

                    cmdStr = "";

                 //   int i = await Dt.SaveChangesAsync(); //.SaveChangesAsync();
                 //   Dt.Database.ExecuteSqlCommand("")


                    //   bioDataToSave.Clear();

                }
                catch (Exception e)
                {
                   // MessageBox.Show(e.Message);
                }
                
            }

        }

        public void StarSavingBioData(string startMSG = "start")
        {
            if (useTPSDevice)
            {
                AdditionalData = startMSG;
                SavingData = true;
            }
        }


        public void StopSavingBioData(string stopMSG = "stop")
        {
            if (useTPSDevice)
            {
                AdditionalData = stopMSG;
                SavingData = false;
             //   useTPSDevice = false;
            }
        }



        private void BIOData_Shown(object sender, EventArgs e)
        {
            if (useTPSDevice)
            {
                if (tpsr.getState() != e_cs.STARTED)
                {
                    if (!tpsr.startDevice())
                    {
                        MessageBox.Show("Was not able to start read BIO data from device.");
                        return;
                    }
                }
                this.timer1.Start();
            }
        }

        private void BIOData_FormClosing(object sender, FormClosingEventArgs e)
        {
            

            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }



        BIODataWindow()
        {
        //    tpsr.stopDevice();
        }
    }
}
