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
    }
}
