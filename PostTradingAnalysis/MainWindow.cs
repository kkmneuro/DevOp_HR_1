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
            cbStddevInterval.SelectedIndex = 8;
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
            //1 second
            //2 seconds
            //3 seconds
            //5 seconds
            //10 seconds
            //15 seconds
            //20 seconds
            //30 seconds
            //1 minute
            //2 minutes
            //3 minutes
            //5 minutes
            //10 minutes

            int seconds = 0;
            switch (cbStddevInterval.SelectedIndex)
            {
                case 0:
                    seconds = 1;
                    break;
                case 1:
                    seconds = 2;
                    break;
                case 2:
                    seconds = 2;
                    break;
                case 3:
                    seconds = 5;
                    break;
                case 4:
                    seconds = 10;
                    break;
                case 5:
                    seconds = 15;
                    break;
                case 6:
                    seconds = 20;
                    break;
                case 7:
                    seconds = 30;
                    break;
                case 8:
                    seconds = 60;
                    break;
                case 9:
                    seconds = 60*2;
                    break;
                case 10:
                    seconds = 60*3;
                    break;
                case 11:
                    seconds = 60*5;
                    break;
                case 12:
                    seconds = 60*10;
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
            saveFileDialog.FileName = "PostAnalysisData - " + cbDates.Text.Replace('/', '_').Replace(':', '_') + " - " + cbStddevInterval.Text + ".txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter outputFile = new StreamWriter(saveFileDialog.FileName))
                {
                    var stdDevPeriod = application.GetStdDevPeriod();
                    var temperatureStddev = ((LineSeries)application.chartWindows["Temperature stddev"].plotView.Model.Series[0]).Points;
                    var heartRateStddev = ((LineSeries)application.chartWindows["Heart rate stddev"].plotView.Model.Series[0]).Points;
                    var skinConductanceStddev = ((LineSeries)application.chartWindows["Skin conductance stddev"].plotView.Model.Series[0]).Points;
                    var priceStddev = ((LineSeries)application.chartWindows["Price stddev"].plotView.Model.Series[0]).Points;
                    var temperatureStddevAway = ((LineSeries)application.chartWindows["Temperature stddev away"].plotView.Model.Series[0]).Points;
                    var heartRateStddevAway = ((LineSeries)application.chartWindows["Heart rate stddev away"].plotView.Model.Series[0]).Points;
                    var skinConductanceStddevAway = ((LineSeries)application.chartWindows["Skin conductance stddev away"].plotView.Model.Series[0]).Points;
                    var priceStddevAway = ((LineSeries)application.chartWindows["Price stddev away"].plotView.Model.Series[0]).Points;
                    var temperatureStddevVelocity = ((LineSeries)application.chartWindows["Temperature velocity"].plotView.Model.Series[0]).Points;
                    var heartRateStddevVelocity = ((LineSeries)application.chartWindows["Heart rate velocity"].plotView.Model.Series[0]).Points;
                    var skinConductanceStddevVelocity = ((LineSeries)application.chartWindows["Skin conductance velocity"].plotView.Model.Series[0]).Points;
                    var priceStddevVelocity = ((LineSeries)application.chartWindows["Price velocity"].plotView.Model.Series[0]).Points;
                    var scPriceStdDevMult = ((LineSeries)application.chartWindows["SC stddev * Price stddev"].plotView.Model.Series[0]).Points;
                    var scPriceStdDevSubstrUp = ((LineSeries)application.chartWindows["SC stdev away - Price stddev away"].plotView.Model.Series[0]).Points;
                    var scPriceStdDevSubstrDown = ((LineSeries)application.chartWindows["SC stdev away - Price stddev away"].plotView.Model.Series[1]).Points;
                    outputFile.WriteLine("Id\tTime\tTemperature\tHeartRate\tSkinConductance\tTrainingStep\tBuyPrice\tTemperatureStddev\tHeartRateStddev\tSkinConductanceStddev\tPriceStddev\tTemperatureStddevAway\tHeartRateStddevAway\tSkinConductanceStddevAway\tPriceStddevAway\tTemperatureVelocity\tHeartRateVelocity\tSkinConductanceVelocity\tPriceVelocity\tscPriceStdDevMult\tscPriceStdDevSubstr");
                    for (int ind = 0; ind < application.bioData.Count; ind++)
                    {
                        var dataPoint = application.bioData[ind];
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
                        outputFile.Write(dataPoint.trainingStep);
                        outputFile.Write("\t");
                        outputFile.Write(dataPoint.buyPrice);
                        outputFile.Write("\t");
                        outputFile.Write(dblstr(temperatureStddev[ind].Y));
                        outputFile.Write("\t");
                        outputFile.Write(dblstr(heartRateStddev[ind].Y));
                        outputFile.Write("\t");
                        outputFile.Write(dblstr(skinConductanceStddev[ind].Y));
                        outputFile.Write("\t");
                        outputFile.Write(dblstr(priceStddev[ind].Y));
                        outputFile.Write("\t");
                        outputFile.Write(dblstr(temperatureStddevAway[ind].Y));
                        outputFile.Write("\t");
                        outputFile.Write(dblstr(heartRateStddevAway[ind].Y));
                        outputFile.Write("\t");
                        outputFile.Write(dblstr(skinConductanceStddevAway[ind].Y));
                        outputFile.Write("\t");
                        outputFile.Write(dblstr(priceStddevAway[ind].Y));
                        outputFile.Write("\t");
                        outputFile.Write(dblstr(temperatureStddevVelocity[ind].Y));
                        outputFile.Write("\t");
                        outputFile.Write(dblstr(heartRateStddevVelocity[ind].Y));
                        outputFile.Write("\t");
                        outputFile.Write(dblstr(skinConductanceStddevVelocity[ind].Y));
                        outputFile.Write("\t");
                        outputFile.Write(dblstr(priceStddevVelocity[ind].Y));
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
    }
}
