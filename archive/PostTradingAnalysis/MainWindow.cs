using OxyPlot.Series;
using System;
using System.IO;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Linq;

namespace PostTradingAnalysis
{
    public partial class MainWindow : Form
    {
        private PostTradingAnalysisApplication application;
        private DeserializeDockContent m_deserializeDockContent;
        private const string dockPanelConfigFile = "PTADockPanel.config";

        public MainWindow(PostTradingAnalysisApplication application)
        {
            InitializeComponent();
            this.application = application;
            m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
            cbStddevInterval.SelectedIndex = 2;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            vS2015BlueTheme1.Measures.DockPadding = 0;

            if (File.Exists(dockPanelConfigFile))
            {
                dockPanel.LoadFromXml(dockPanelConfigFile, m_deserializeDockContent);
            }
            else
            {
                foreach(var kv in application.chartWindows)
                {
                    kv.Value.DockPanel = application.mainWindow.dockPanel;
                }
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbDates.SelectedIndex > -1)
                {
                    var userId = application.cbUserInd2UserID[cbUsers.SelectedIndex];
                    var timeRange = application.activeDates[userId][cbDates.SelectedIndex];
                    var timeFrom = timeRange.Item1;
                    var timeTo = timeRange.Item2;
                    application.LoadData(timeFrom, timeTo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            return application.chartWindows.First(x => x.Value.WindowName == persistString).Value;
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            dockPanel.SaveAsXml(dockPanelConfigFile);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void loadDataPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelLoadData.Visible = loadDataPanelToolStripMenuItem.Checked;
        }

        private void cbStddevInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            // values from combobox:
            //15 seconds
            //30 seconds
            //1 minute
            //3 minutes
            //5 minutes

            int seconds = 0;
            switch (cbStddevInterval.SelectedIndex)
            {
                case 0:
                    seconds = 15;
                    break;
                case 1:
                    seconds = 30;
                    break;
                case 2:
                    seconds = 60;
                    break;
                case 3:
                    seconds = 60*3;
                    break;
                case 4:
                    seconds = 60*5;
                    break;
            }

            application.SetChartPeriod(seconds*2);
        }

        public static string dblstr(double val)
        {
            if (double.IsNaN(val))
                return "";
            else
                return val.ToString();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (cbDates.SelectedIndex == -1)
                return;
            saveFileDialog.FileName = "PostAnalysisData - " + dtFrom.Value.ToString("dd_MM_yyyy HH_mm_ss") + "  -  " + dtTo.Value.ToString("dd_MM_yyyy HH_mm_ss") + "  -  " + cbStddevInterval.Text + ".tsv";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter outputFile = new StreamWriter(saveFileDialog.FileName))
                {
                    var stdDevPeriod = application.GetStdDevPeriod();
                    var temperatureStddevAway = ((LineSeries)application.chartWindows["Temperature stddev away"].plotView.Model.Series[0]).Points;
                    var heartRateStddevAway = ((LineSeries)application.chartWindows["Heart rate stddev away"].plotView.Model.Series[0]).Points;
                    var skinConductanceStddevAway = ((LineSeries)application.chartWindows["Skin conductance stddev away"].plotView.Model.Series[0]).Points;
                    var priceStddevAway = ((LineSeries)application.chartWindows["Price stddev away"].plotView.Model.Series[0]).Points;
                    var temperatureStddevVelocityStdaway = ((LineSeries)application.chartWindows["Temperature velocity stdaway"].plotView.Model.Series[0]).Points;
                    var heartRateStddevVelocityStdaway = ((LineSeries)application.chartWindows["Heart rate velocity stdaway"].plotView.Model.Series[0]).Points;
                    var skinConductanceStddevVelocityStdaway = ((LineSeries)application.chartWindows["Skin conductance velocity stdaway"].plotView.Model.Series[0]).Points;
                    var priceStddevVelocityStdaway = ((LineSeries)application.chartWindows["Price velocity stdaway"].plotView.Model.Series[0]).Points;
                    var scPriceStdDevMult = ((LineSeries)application.chartWindows["SC stddev * Price stddev"].plotView.Model.Series[0]).Points;
                    var scPriceStdDevSubstrUp = ((LineSeries)application.chartWindows["SC stdev away - Price stddev away"].plotView.Model.Series[0]).Points;
                    var scPriceStdDevSubstrDown = ((LineSeries)application.chartWindows["SC stdev away - Price stddev away"].plotView.Model.Series[1]).Points;
                    outputFile.WriteLine("Id\tTime\tTemperature\tHeartRate\tSkinConductance\tBuyPrice\tTemperatureStddevAway\tHeartRateStddevAway\tSkinConductanceStddevAway\tPriceStddevAway\tTemperatureVelocityStdaway\tHeartRateVelocityStdaway\tSkinConductanceVelocityStdaway\tPriceVelocityStdaway\tscPriceStdDevMult\tscPriceStdDevSubstr");
                    for (int ind = 0; ind < application.bioData.Count; ind++)
                    {
                        var dataPoint = application.bioData[ind];
                        if (dataPoint.time < dtFrom.Value || dataPoint.time > dtTo.Value)
                            continue;
                        outputFile.Write(dataPoint.id);
                        outputFile.Write("\t");
                        outputFile.Write(dataPoint.time.ToString("dd/MM/yyyy HH:mm:ss"));
                        outputFile.Write("\t");
                        outputFile.Write(dataPoint.temperature);
                        outputFile.Write("\t");
                        outputFile.Write(dataPoint.heartRate);
                        outputFile.Write("\t");
                        outputFile.Write(dataPoint.skinConductance);
                        outputFile.Write("\t");
                        outputFile.Write(dataPoint.buyPrice);
                        outputFile.Write("\t");
                        outputFile.Write(dblstr(temperatureStddevAway[ind].Y));
                        outputFile.Write("\t");
                        outputFile.Write(dblstr(heartRateStddevAway[ind].Y));
                        outputFile.Write("\t");
                        outputFile.Write(dblstr(skinConductanceStddevAway[ind].Y));
                        outputFile.Write("\t");
                        outputFile.Write(dblstr(priceStddevAway[ind].Y));
                        outputFile.Write("\t");
                        outputFile.Write(dblstr(temperatureStddevVelocityStdaway[ind].Y));
                        outputFile.Write("\t");
                        outputFile.Write(dblstr(heartRateStddevVelocityStdaway[ind].Y));
                        outputFile.Write("\t");
                        outputFile.Write(dblstr(skinConductanceStddevVelocityStdaway[ind].Y));
                        outputFile.Write("\t");
                        outputFile.Write(dblstr(priceStddevVelocityStdaway[ind].Y));
                        outputFile.Write("\t");
                        outputFile.Write(dblstr(scPriceStdDevMult[ind].Y));
                        outputFile.Write("\t");
                        var y = skinConductanceStddevAway[ind].Y - priceStddevAway[ind].Y;
                        outputFile.Write(dblstr(y));
                        outputFile.WriteLine();
                    }
                }
            }
        }

        private void cbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbDates.Items.Clear();
            var userId = application.cbUserInd2UserID[cbUsers.SelectedIndex];
            foreach (var dates in application.activeDates[userId])
            {
                cbDates.Items.Add(dates.Item1.ToString("yyyy/MM/dd   HH:mm:ss") + "  -  " + dates.Item2.ToString("HH:mm:ss"));
            }

            if(cbDates.Items.Count > 0)
            {
                cbDates.SelectedIndex = 0;
            }
        }

        private void cbDates_SelectedIndexChanged(object sender, EventArgs e)
        {
            var userId = application.cbUserInd2UserID[cbUsers.SelectedIndex];
            var interval = application.activeDates[userId][cbDates.SelectedIndex];
            dtFrom.Value = interval.Item1;
            dtTo.Value = interval.Item2;
        }
    }
}
