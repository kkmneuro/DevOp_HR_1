using NeuroXChange.Model.BioData;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Windows.Forms;

namespace PostTradingAnalysis
{
    public partial class MainWindow : Form
    {
        private PostTradingAnalysisApplication application;

        public MainWindow(PostTradingAnalysisApplication application)
        {
            InitializeComponent();

            this.application = application;
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tbFile.Text = openFileDialog.FileName;
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            vS2015BlueTheme1.Measures.DockPadding = 0;

            openFileDialog.InitialDirectory = "Data";

            var now = DateTime.Now;
            var timeFrom = new DateTime(now.Year, now.Month, now.Day);
            var timeTo = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);

            //dtpFrom.Value = timeFrom;
            dtpTo.Value = timeTo;

            btnLoad_Click(null, null);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                application.LoadData(
                    tbFile.Text, dtpFrom.Value, dtpTo.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
