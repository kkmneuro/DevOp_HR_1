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
            else if (persistString == typeof(BehavioralModelTransitionsWindow).ToString())
                return mainNeuroXView.behavioralModelTransitionsWindow;
            else if (persistString == typeof(BMColorCodedWithPriceWindow).ToString())
                return mainNeuroXView.bMColorCodedWithPriceWindow;
            else if (persistString == typeof(EmulationModeControlWindow).ToString())
                return mainNeuroXView.emulationModeControlWindow;
            else if (persistString == typeof(ProfitabilityWindow).ToString())
                return mainNeuroXView.profitabilityWindow;
            return null;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void windowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DockContent dockContentWindow = null;
            if (sender == rawInformationToolStripMenuItem)
                dockContentWindow = mainNeuroXView.rawInformationWindow;
            else if (sender == newOrderToolStripMenuItem)
                dockContentWindow = mainNeuroXView.newOrderWindow;
            else if (sender == chartsToolStripMenuItem)
                dockContentWindow = mainNeuroXView.chartsWindow;
            else if (sender == breathPacerToolStripMenuItem)
                dockContentWindow = mainNeuroXView.breathPacerWindow;
            else if (sender == indicatorsToolStripMenuItem)
                dockContentWindow = mainNeuroXView.indicatorsWindow;
            else if (sender == behavioralModelsToolStripMenuItem)
                dockContentWindow = mainNeuroXView.behavioralModelWindow;
            else if (sender == behavioralModelTransitonsToolStripMenuItem)
                dockContentWindow = mainNeuroXView.behavioralModelTransitionsWindow;
            else if (sender == bMColorCodedWithPriceToolStripMenuItem)
                dockContentWindow = mainNeuroXView.bMColorCodedWithPriceWindow;
            else if (sender == emulationModeControlToolStripMenuItem)
                dockContentWindow = mainNeuroXView.emulationModeControlWindow;
            else if (sender == profitabilityToolStripMenuItem)
                dockContentWindow = mainNeuroXView.profitabilityWindow;

            dockContentWindow.Show();
            if (mainNeuroXView.allWindowsOnTop && dockContentWindow.Pane.IsFloat)
            {
                dockContentWindow.Pane.FloatWindow.TopMost = true;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainNeuroXView.logoWindow.ShowDialog();
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            dockPanel.SaveAsXml(dockPanelConfigFile);
            iniFileReader.Write("MainWindowX", Location.X.ToString(), "Interface");
            iniFileReader.Write("MainWindowY", Location.Y.ToString(), "Interface");
            iniFileReader.Write("MainWindowWidth", Size.Width.ToString(), "Interface");
            iniFileReader.Write("MainWindowHeight", Size.Height.ToString(), "Interface");
            iniFileReader.Write("TickInterval", mainNeuroXView.emulationModeControlWindow.tickSizeUpDown.Value.ToString(), "EmulationOnHistory");
        }

        void dockStateChangedAction(object sender, EventArgs e)
        {
            var dockContentWindow = (DockContent)sender;
            if (mainNeuroXView.allWindowsOnTop && dockContentWindow.Pane!= null && dockContentWindow.Pane.IsFloat)
            {
                dockContentWindow.Pane.FloatWindow.TopMost = true;
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            Location = new Point(
                Int32.Parse(iniFileReader.Read("MainWindowX", "Interface")),
                Int32.Parse(iniFileReader.Read("MainWindowY", "Interface")));
            Size = new Size(
                Int32.Parse(iniFileReader.Read("MainWindowWidth", "Interface")),
                Int32.Parse(iniFileReader.Read("MainWindowHeight", "Interface")));

            mainNeuroXView.rawInformationWindow.DockStateChanged += dockStateChangedAction;
            mainNeuroXView.chartsWindow.DockStateChanged += dockStateChangedAction;
            mainNeuroXView.newOrderWindow.DockStateChanged += dockStateChangedAction;
            mainNeuroXView.breathPacerWindow.DockStateChanged += dockStateChangedAction;
            mainNeuroXView.indicatorsWindow.DockStateChanged += dockStateChangedAction;
            mainNeuroXView.behavioralModelWindow.DockStateChanged += dockStateChangedAction;
            mainNeuroXView.behavioralModelTransitionsWindow.DockStateChanged += dockStateChangedAction;
            mainNeuroXView.bMColorCodedWithPriceWindow.DockStateChanged += dockStateChangedAction;
            mainNeuroXView.emulationModeControlWindow.DockStateChanged += dockStateChangedAction;
            mainNeuroXView.profitabilityWindow.DockStateChanged += dockStateChangedAction;

            if (File.Exists(dockPanelConfigFile))
                dockPanel.LoadFromXml(dockPanelConfigFile, m_deserializeDockContent);

            mainNeuroXView.rawInformationWindow.DockPanel = dockPanel;
            mainNeuroXView.chartsWindow.DockPanel = dockPanel;
            mainNeuroXView.newOrderWindow.DockPanel = dockPanel;
            mainNeuroXView.breathPacerWindow.DockPanel = dockPanel;
            mainNeuroXView.indicatorsWindow.DockPanel = dockPanel;
            mainNeuroXView.behavioralModelWindow.DockPanel = dockPanel;
            mainNeuroXView.behavioralModelTransitionsWindow.DockPanel = dockPanel;
            mainNeuroXView.bMColorCodedWithPriceWindow.DockPanel = dockPanel;
            mainNeuroXView.emulationModeControlWindow.DockPanel = dockPanel;
            mainNeuroXView.profitabilityWindow.DockPanel = dockPanel;
        }
    }
}
