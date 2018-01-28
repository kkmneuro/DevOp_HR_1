using NeuroXChange.Model.BioData;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.IO;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

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

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tbFile.Text = openFileDialog.FileName;
            }
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

            openFileDialog.InitialDirectory = "Data";

            var now = DateTime.Now;
            var timeFrom = new DateTime(now.Year, now.Month, now.Day);
            var timeTo = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);

            dtpFrom.Value = timeFrom;
            dtpTo.Value = timeTo;
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

        private IDockContent GetContentFromPersistString(string persistString)
        {
            return application.chartWindows[persistString];
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

            application.SetStdDevPeriod(seconds*2);
        }
    }
}
