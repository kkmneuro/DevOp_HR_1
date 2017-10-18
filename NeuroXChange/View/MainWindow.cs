using NeuroXChange.Common;
using NeuroXChange.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using WeifenLuo.WinFormsUI.Docking;

namespace NeuroXChange
{
    public partial class MainWindow : Form
    {
        private MainNeuroXView mainNeuroXView = null;
        private IniFileReader iniFileReader = null;
        private string dockPanelConfigFile = "DockPanel.config";
        private DeserializeDockContent m_deserializeDockContent;

        public MainWindow(MainNeuroXView mainNeuroXView, IniFileReader iniFileReader)
        {
            this.mainNeuroXView = mainNeuroXView;
            this.iniFileReader = iniFileReader;
            InitializeComponent();
            m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(BehavioralModelsWindow).ToString())
                return mainNeuroXView.behavioralModelWindow;
            else if (persistString == typeof(BreathPacerWindow).ToString())
                return mainNeuroXView.breathPacerWindow;
            else if (persistString == typeof(ChartsWindow).ToString())
                return mainNeuroXView.chartsWindow;
            else if (persistString == typeof(IndicatorsWindow).ToString())
                return mainNeuroXView.indicatorsWindow;
            else if (persistString == typeof(NewOrderWindow).ToString())
                return mainNeuroXView.newOrderWindow;
            else if (persistString == typeof(RawInformationWindow).ToString())
                return mainNeuroXView.rawInformationWindow;
            return null;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void rawInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainNeuroXView.rawInformationWindow.Show();
        }

        private void chartsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainNeuroXView.chartsWindow.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainNeuroXView.logoWindow.Show();
        }

        private void newOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainNeuroXView.newOrderWindow.Show();
        }

        private void breathPacerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainNeuroXView.breathPacerWindow.Show();
        }

        private void indicatorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainNeuroXView.indicatorsWindow.Show();
        }

        private void behavioralModelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainNeuroXView.behavioralModelWindow.Show();
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            dockPanel.SaveAsXml(dockPanelConfigFile);
            iniFileReader.Write("MainWindowX", Location.X.ToString(), "Interface");
            iniFileReader.Write("MainWindowY", Location.Y.ToString(), "Interface");
            iniFileReader.Write("MainWindowWidth", Size.Width.ToString(), "Interface");
            iniFileReader.Write("MainWindowHeight", Size.Height.ToString(), "Interface");
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            Location = new Point(
                Int32.Parse(iniFileReader.Read("MainWindowX", "Interface")),
                Int32.Parse(iniFileReader.Read("MainWindowY", "Interface")));
            Size = new Size(
                Int32.Parse(iniFileReader.Read("MainWindowWidth", "Interface")),
                Int32.Parse(iniFileReader.Read("MainWindowHeight", "Interface")));

            if (File.Exists(dockPanelConfigFile))
                dockPanel.LoadFromXml(dockPanelConfigFile, m_deserializeDockContent);

            mainNeuroXView.rawInformationWindow.DockPanel = dockPanel;
            mainNeuroXView.chartsWindow.DockPanel = dockPanel;
            mainNeuroXView.newOrderWindow.DockPanel = dockPanel;
            mainNeuroXView.breathPacerWindow.DockPanel = dockPanel;
            mainNeuroXView.indicatorsWindow.DockPanel = dockPanel;
            mainNeuroXView.behavioralModelWindow.DockPanel = dockPanel;
        }
    }
}
