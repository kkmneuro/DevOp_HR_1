using NeuroXChange.Common;
using NeuroXChange.Model;
using NeuroXChange.View;
using NeuroXChange.View.Training;
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
        private string dockPanelConfigFile;
        private DeserializeDockContent m_deserializeDockContent;
        public SelectionEnum currentSelection;


        // maro stavio, ja digo private BreathPacerWindow breathpacerwindow = null;


        public MainWindow(MainNeuroXView mainNeuroXView, IniFileReader iniFileReader)
        {
            this.mainNeuroXView = mainNeuroXView;
            this.iniFileReader = iniFileReader;
            this.currentSelection = mainNeuroXView.status;
            InitializeComponent();
            m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);

            if (mainNeuroXView.SimplestMode)
            {
                dockPanelConfigFile = "DockPanelSimplestMode.config";
                fileToolStripMenuItem.Visible = false;
                compDayToolStripMenuItem.Visible = false;
                windowsToolStripMenuItem.Visible = false;
            }
            else
            {
                dockPanelConfigFile = "DockPanel.config";
                fileToolStripMenuItemSimplestMode.Visible = false;
                windowsToolStripMenuItemSimplestMode.Visible = false;
            }
        }

        private void LoadWindow()
        {
            mainNeuroXView.tradeControlWindow.Hide();
            mainNeuroXView.applicationControlWindow.Hide();
            mainNeuroXView.chartsWindow.Hide();
            mainNeuroXView.trainingControlWindow.Hide();
            mainNeuroXView.symbolSelectionWindow.Hide();
            if (this.currentSelection == SelectionEnum.Training)
            {
                //MessageBox.Show("Training");
                windowToolStripMenuItem_Click(trainingToolStripMenuItem, null);
                windowToolStripMenuItem_Click(chartsToolStripMenuItem, null);
                windowToolStripMenuItem_Click(applicationControlToolStripMenuItem, null);
                //gogo dodo
                windowToolStripMenuItem_Click(breathPacerToolStripMenuItem, null);
                windowToolStripMenuItem_Click(tradeToolStripMenuItem, null);
                //windowToolStripMenuItem_Click(trainingToolStripMenuItem, null);
                windowToolStripMenuItem_Click(indicatorsToolStripMenuItem, null);
                windowToolStripMenuItem_Click(rawInformationToolStripMenuItem, null);
                windowToolStripMenuItem_Click(bMColorCodedWithPriceToolStripMenuItem, null);


            }
            else if (this.currentSelection == SelectionEnum.Trade)
            {
                //MessageBox.Show("Trade");
                windowToolStripMenuItem_Click(symbolsToolStripMenuItem, null);
                windowToolStripMenuItem_Click(tradeToolStripMenuItem, null);
                windowToolStripMenuItem_Click(applicationControlToolStripMenuItem, null);
            }
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
            else if (persistString == typeof(ApplicationControlWindow).ToString())
                return mainNeuroXView.applicationControlWindow;
            else if (persistString == typeof(EmulationModeControlWindow).ToString())
                return mainNeuroXView.emulationModeControlWindow;
            else if (persistString == typeof(OrdersWindow).ToString())
                return mainNeuroXView.ordersWindow;
            else if (persistString == typeof(CompDayWindow).ToString())
                return mainNeuroXView.compDayWindow;
            else if (persistString == typeof(MarketSentimentSurveyWindow).ToString())
                return mainNeuroXView.marketSentimentSurveyWindow;
            else if (persistString == typeof(TradeControlWindow).ToString())
                return mainNeuroXView.tradeControlWindow;
            else if (persistString == typeof(TraningControlWindow).ToString())
                return mainNeuroXView.trainingControlWindow;
           // else if (persistString == typeof(SymbolSelectionWindow).ToString())
             //   return mainNeuroXView.symbolSelectionWindow;
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
            else if (sender == newOrderToolStripMenuItem || sender == newOrderToolStripMenuItemSimplestMode)
                dockContentWindow = mainNeuroXView.newOrderWindow;
            else if (sender == chartsToolStripMenuItem || sender == chartsToolStripMenuItemSimplestMode)
                dockContentWindow = mainNeuroXView.chartsWindow;
            else if (sender == breathPacerToolStripMenuItem || sender == breathPacerToolStripMenuItemSimplestMode)
                dockContentWindow = mainNeuroXView.breathPacerWindow;
            else if (sender == indicatorsToolStripMenuItem || sender == indicatorsToolStripMenuItemSimplestMode)
                dockContentWindow = mainNeuroXView.indicatorsWindow;
            else if (sender == behavioralModelsToolStripMenuItem)
                dockContentWindow = mainNeuroXView.behavioralModelWindow;
            else if (sender == behavioralModelTransitonsToolStripMenuItem)
                dockContentWindow = mainNeuroXView.behavioralModelTransitionsWindow;
            else if (sender == bMColorCodedWithPriceToolStripMenuItem)
                dockContentWindow = mainNeuroXView.bMColorCodedWithPriceWindow;
            else if (sender == applicationControlToolStripMenuItem || sender == applicationControlToolStripMenuItemSimplestMode)
                dockContentWindow = mainNeuroXView.applicationControlWindow;
            else if (sender == emulationModeControlToolStripMenuItem)
                dockContentWindow = mainNeuroXView.emulationModeControlWindow;
            else if (sender == ordersToolStripMenuItem)
                dockContentWindow = mainNeuroXView.ordersWindow;
            else if (sender == compDayToolStripMenuItem)
                dockContentWindow = mainNeuroXView.compDayWindow;
            else if (sender == marketSentimentSurveyToolStripMenuItem)
                dockContentWindow = mainNeuroXView.marketSentimentSurveyWindow;
            else if (sender == tradeToolStripMenuItem)
            {
                //
                this.mainNeuroXView.symbolSelectionWindow.Show();
                dockContentWindow = mainNeuroXView.tradeControlWindow;
            }
            else if (sender == trainingToolStripMenuItem)
            {
                    dockContentWindow = mainNeuroXView.trainingControlWindow;
            }
            //else if (sender == symbolsToolStripMenuItem)
              //  dockContentWindow = mainNeuroXView.symbolSelectionWindow;

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
            //write to log
            NeuroXChange.Model.Globals.LoggerClient.WriteLog(Globals.AccountId.ToString(), "Main Form closing.", DateTime.Now);

            if (!mainNeuroXView.isStateGood)
                return;

            // manually close comp day training if it was opened
            mainNeuroXView.compDayWindow.Hide();

            dockPanel.SaveAsXml(dockPanelConfigFile);
            iniFileReader.Write("MainWindowX", Location.X.ToString(), "Interface");
            iniFileReader.Write("MainWindowY", Location.Y.ToString(), "Interface");
            iniFileReader.Write("MainWindowWidth", Size.Width.ToString(), "Interface");
            iniFileReader.Write("MainWindowHeight", Size.Height.ToString(), "Interface");
#if !SIMPLEST
            iniFileReader.Write("HistoryTickInterval", mainNeuroXView.emulationModeControlWindow.tickSizeUpDown.Value.ToString(), "EmulationOnHistory");
#endif            

        }

        void dockStateChangedAction(object sender, EventArgs e)
        {
            var dockContentWindow = (DockContent)sender;
            if (mainNeuroXView.allWindowsOnTop && dockContentWindow.Pane != null && dockContentWindow.Pane.IsFloat)
            {
                dockContentWindow.Pane.FloatWindow.TopMost = true;
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            Model.Globals.ApplicationStop = true;
            timerStartRecording.Interval = Convert.ToInt32(this.iniFileReader.Read("FirstInterval", "Timer", "5000"));
            timerStartRecording.Enabled = true;


            Location = new Point(
                Int32.Parse(iniFileReader.Read("MainWindowX", "Interface", "258")),
                Int32.Parse(iniFileReader.Read("MainWindowY", "Interface", "80")));
            Size = new Size(
                Int32.Parse(iniFileReader.Read("MainWindowWidth", "Interface", "1100")),
                Int32.Parse(iniFileReader.Read("MainWindowHeight", "Interface", "800")));

            mainNeuroXView.rawInformationWindow.DockStateChanged += dockStateChangedAction;
            mainNeuroXView.chartsWindow.DockStateChanged += dockStateChangedAction;
            mainNeuroXView.newOrderWindow.DockStateChanged += dockStateChangedAction;
            mainNeuroXView.breathPacerWindow.DockStateChanged += dockStateChangedAction;
            mainNeuroXView.indicatorsWindow.DockStateChanged += dockStateChangedAction;
            mainNeuroXView.behavioralModelWindow.DockStateChanged += dockStateChangedAction;
            mainNeuroXView.behavioralModelTransitionsWindow.DockStateChanged += dockStateChangedAction;
            mainNeuroXView.bMColorCodedWithPriceWindow.DockStateChanged += dockStateChangedAction;
            mainNeuroXView.applicationControlWindow.DockStateChanged += dockStateChangedAction;
            mainNeuroXView.emulationModeControlWindow.DockStateChanged += dockStateChangedAction;
            mainNeuroXView.ordersWindow.DockStateChanged += dockStateChangedAction;
            mainNeuroXView.compDayWindow.DockStateChanged += dockStateChangedAction;
            mainNeuroXView.marketSentimentSurveyWindow.DockStateChanged += dockStateChangedAction;
            mainNeuroXView.tradeControlWindow.DockStateChanged += dockStateChangedAction;
            mainNeuroXView.trainingControlWindow.DockStateChanged += dockStateChangedAction;
            //mainNeuroXView.symbolSelectionWindow.DockStateChanged += dockStateChangedAction;

            if (File.Exists(dockPanelConfigFile) && dockPanel.Contents.Count==0)
                dockPanel.LoadFromXml(dockPanelConfigFile, m_deserializeDockContent);

            mainNeuroXView.rawInformationWindow.DockPanel = dockPanel;
            mainNeuroXView.chartsWindow.DockPanel = dockPanel;
            mainNeuroXView.newOrderWindow.DockPanel = dockPanel;
            mainNeuroXView.breathPacerWindow.DockPanel = dockPanel;
            mainNeuroXView.indicatorsWindow.DockPanel = dockPanel;
            mainNeuroXView.behavioralModelWindow.DockPanel = dockPanel;
            mainNeuroXView.behavioralModelTransitionsWindow.DockPanel = dockPanel;
            mainNeuroXView.bMColorCodedWithPriceWindow.DockPanel = dockPanel;
            mainNeuroXView.applicationControlWindow.DockPanel = dockPanel;
            mainNeuroXView.emulationModeControlWindow.DockPanel = dockPanel;
            mainNeuroXView.ordersWindow.DockPanel = dockPanel;
            mainNeuroXView.compDayWindow.DockPanel = dockPanel;
            mainNeuroXView.marketSentimentSurveyWindow.DockPanel = dockPanel;
            mainNeuroXView.tradeControlWindow.DockPanel = dockPanel;
            mainNeuroXView.trainingControlWindow.DockPanel = dockPanel;
            //mainNeuroXView.symbolSelectionWindow.DockPanel = dockPanel;

            LoadWindow();
        }

        private void behavioralModelSL_Click(object sender, EventArgs e)
        {

        }
        public void DisableTimer()
        {
            timerStartRecording.Enabled = false;
            timerStartRecording.Stop();
        }

        public void EnableTimer()
        {
            timerStartRecording.Enabled = true;
            timerStartRecording.Start();
        }

        private void timerStartRecording_Tick(object sender, EventArgs e)
        {
           
            if (!Model.Globals.ApplicationStop)
            {
                //timerfunction();

                Model.Globals.StartRecording = false;
                mainNeuroXView.applicationControlWindow.FindForm().Controls.Find("btnStart", true).First().Enabled = true;
                mainNeuroXView.applicationControlWindow.FindForm().Controls.Find("btnStop", true).First().Enabled = false;
                Model.Globals.ApplicationStop = true;
                DisableTimer();
            }

            if (Model.Globals.FirstTick)
            {
                Model.Globals.FirstTick = false;
                timerfunction();
            }

            timerStartRecording.Interval = Convert.ToInt32(this.iniFileReader.Read("SecondInterval", "Timer", "900000"));

        }

        private void timerfunction()
         {
            //timerStartRecording.Enabled = false;
            if (MessageBox.Show(mainNeuroXView.applicationControlWindow, "Are you ready to start recording your biodata?", "Recording Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //write to log
                NeuroXChange.Model.Globals.LoggerClient.WriteLog(Globals.AccountId.ToString(), "Timer ticked and user wants to do recording.", DateTime.Now);
                Model.Globals.StartRecording = true;
                mainNeuroXView.applicationControlWindow.FindForm().Controls.Find("btnStart", true).First().Enabled = false;
                mainNeuroXView.applicationControlWindow.FindForm().Controls.Find("btnStop", true).First().Enabled = true;
                Model.Globals.ApplicationStop = false;
                EnableTimer();
            }
            else
            {
                //write to log
                NeuroXChange.Model.Globals.LoggerClient.WriteLog(Globals.AccountId.ToString(), "Timer ticked and user don't want to continue recording.", DateTime.Now);

                Model.Globals.StartRecording = false;
                mainNeuroXView.applicationControlWindow.FindForm().Controls.Find("btnStart", true).First().Enabled = true;
                mainNeuroXView.applicationControlWindow.FindForm().Controls.Find("btnStop", true).First().Enabled = false;
                Model.Globals.ApplicationStop = true;
                DisableTimer();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
