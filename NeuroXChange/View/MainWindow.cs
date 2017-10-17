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
        private string dockPanelConfigFile = "DockPanel.config";
        private DeserializeDockContent m_deserializeDockContent;

        public MainWindow(MainNeuroXView mainNeuroXView)
        {
            this.mainNeuroXView = mainNeuroXView;
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
            dockPanel.SaveAsXml(dockPanelConfigFile);
            Application.Exit();
        }

        private void rawInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainNeuroXView.rawInformationWindow.Show(dockPanel, DockState.Float);
        }

        private void chartsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainNeuroXView.chartsWindow.Show(dockPanel, DockState.Float);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainNeuroXView.logoWindow.Show();
        }

        private void newOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainNeuroXView.newOrderWindow.Show(dockPanel, DockState.Float);
        }

        private void breathPacerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainNeuroXView.breathPacerWindow.Show(dockPanel, DockState.Float);
        }

        private void indicatorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainNeuroXView.indicatorsWindow.Show(dockPanel, DockState.Float);
        }

        private void behavioralModelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainNeuroXView.behavioralModelWindow.Show(dockPanel, DockState.Float);
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            dockPanel.SaveAsXml(dockPanelConfigFile);
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            if (File.Exists(dockPanelConfigFile))
                dockPanel.LoadFromXml(dockPanelConfigFile, m_deserializeDockContent);
        }
    }
}
