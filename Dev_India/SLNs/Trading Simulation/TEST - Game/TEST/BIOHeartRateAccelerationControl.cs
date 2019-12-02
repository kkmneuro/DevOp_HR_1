using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using TPSForNeuroTrader;

namespace TEST
{
    public partial class BIOHeartRateAccelerationControl : UserControl
    {

        private TPSForNeuroTrader.TPSForNeuroTrader tpsr; // access to device
        private bool useTPSDevice = false;

        private double minX, maxX;  // scale size
        private int countX;
        private double previousHR = 0;
        private double deltaHR = 0;
        private DateTime previousDT;
        private TimeSpan deltaTime;
        private double hra; //heart rate acceleration on last time span

        private double price = 0;
        private double previousPrice = 0;
        private double deltaPRice = 0;
        private DateTime currentPriceDT;
        private DateTime previousPriceDT;
        private TimeSpan deltaPriceTime;
        private double priceRA = 0;



        public void SetBioDataDevice(TPSForNeuroTrader.TPSForNeuroTrader tpsBioData)
        {
            if (tpsBioData == null) useTPSDevice = false;
            else useTPSDevice = true;

            if (useTPSDevice)
            {
                tpsr = tpsBioData;

                if (tpsr.getState() != e_cs.STARTED) // Start reading data from device !!!
                {
                    if (!tpsr.startDevice())
                    {
                        MessageBox.Show("Was not able to start read BIO data from device.");
                    }
                }
                this.timer1.Start();
            }
        }

        public BIOHeartRateAccelerationControl()
        {
            
                InitializeComponent();



                chart1.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;
                chart1.ChartAreas[0].AxisY.IsStartedFromZero = false;
                chart1.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;

                chart1.Series["hra"].XValueType = ChartValueType.Time;
                chart1.Series["pra"].XValueType = ChartValueType.Time;
                chart1.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss";
                chart1.ChartAreas[0].AxisX.LabelStyle.IsEndLabelVisible = false;

            //chart1.Series["hra"].ToolTip = "NeurAlgoΩ - Heart Rate \nAcceleration  \n(Beats/Second)";


        }


        private void BIOHeartRateAxeleration_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }


        public void _OnPriceTick(object source, MarketData.PriceTickEventArgs e)
        {
            price = System.Convert.ToDouble(e.Price);
            if (previousPrice == 0) previousPrice = price;
           currentPriceDT = e.PriceTime; 
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            if (useTPSDevice)
            {
                TPSData data = tpsr.GetTPSData();

                // remove first point if it is on the limit
                if (chart1.Series["hra"].Points.Count > 100) // in charts show only 200 points.
                {
                    chart1.Series["hra"].Points.RemoveAt(0);
                    chart1.Series["pra"].Points.RemoveAt(0);

                    //      minX = (chart1.Series["hra"].Points[0]).XValue;
                    //      countX = chart1.Series["hra"].Points.Count-1;
                    //      maxX = (chart1.Series["hra"].Points[countX]).XValue;

                    //      chart1.ChartAreas[0].AxisX.Minimum = minX;
                    //      chart1.ChartAreas[0].AxisX.Maximum = DateTime.FromOADate(maxX).AddSeconds(5).ToOADate();

                }

                if (previousHR == 0) previousHR = data.HR; // to avoid fluctuation in beging

                if (previousHR != data.HR)
                {
                    // * Calculation of HRA BEGIN * /
                    deltaHR = data.HR - previousHR;
                    previousHR = data.HR;

                    deltaTime = data.dt - previousDT;
                    previousDT = data.dt;

                    hra = deltaHR / (deltaTime.TotalMilliseconds / 1000);
                    /* Calculation of HRA END */


                    deltaPriceTime = currentPriceDT.Subtract(previousPriceDT);
                    if (deltaPriceTime.TotalMilliseconds > 0)
                        if (previousPrice != price)
                        {
                            /* Calculation of PriceRA BEGIN */
                            deltaPRice = price - previousPrice;
                            previousPrice = price;


                            deltaPriceTime = currentPriceDT.Subtract(previousPriceDT);
                            previousPriceDT = new DateTime(currentPriceDT.Ticks);



                            priceRA = (deltaPRice * 30000); /// (deltaPriceTime.TotalMilliseconds/100) ;

                            /* Calculation of PriceRA END */


                        }



                    chart1.Series["hra"].Points.AddXY( data.dt , hra);
                    chart1.Series["pra"].Points.AddXY( data.dt , priceRA);

                    update_scales();
                }

            }

        }

        private void update_scales()
        {
            chart1.ChartAreas[0].AxisY.Maximum = Double.NaN; // sets the Maximum to NaN
            chart1.ChartAreas[0].AxisY.Minimum = Double.NaN; // sets the Minimum to NaN
            chart1.ChartAreas[0].RecalculateAxesScale();
        }

    }
}
