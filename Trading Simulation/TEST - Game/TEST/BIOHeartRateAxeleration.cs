using System;
using System.Windows.Forms;

using System.Windows.Forms.DataVisualization.Charting;

using TPSForNeuroTrader;


namespace TEST
{
    public partial class BIOHeartRateAxeleration : Form
    { 
        public BIOHeartRateAxeleration(TPSForNeuroTrader.TPSForNeuroTrader tpsBioData)
        {
            InitializeComponent();
            bioHeartRateAccelerationControl1.SetBioDataDevice(tpsBioData);   
                     
        }

        public void SetMarketData(MarketData.MarketDataDDF md)
        {
            md.OnPriceTick += bioHeartRateAccelerationControl1._OnPriceTick;
        }


        



        private void BIOHeartRateAxeleration_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

    }
}
