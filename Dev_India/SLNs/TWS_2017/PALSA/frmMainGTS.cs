using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nevron.UI.WinForm.Controls;
using PALSA.Cls;
using Nevron.UI.WinForm.Docking;
using System.Reflection;
using System.Resources;
using System.Collections;
using Nevron.UI;
using System.Threading;
using System.Diagnostics;
using Nevron.Serialization;
using System.IO;
using System.Drawing.Printing;
using System.Drawing.Imaging;


namespace GTS
{
    public partial class frmMainGTS : NForm
    {
        #region Class Object Declaration
        //public clsUtilityFunctions objFunction;
        //public clsChartFunctions objChartFunction;
        //public clsSaveAndLoadFunctions objSaveLoadWorkSpace;
        //public clsToolbarFunctions objToolFunction;
        //public clsOrderFunctions objOrderFunction;
        //public clsConstructorFunctions objConstructorFunc;
        #endregion

        #region Variables

        private static frmMainGTS FMInstanceGTS_ = new frmMainGTS();

        private bool m_Closed;
        public static bool EcoCalendarDis = true;
        public static bool isClone = false;        
        public static bool bmnuFLoadWrksp = false;
        private Dictionary<string, ReversePosition> ddReverseOrder = null;
        private Dictionary<string, string> _listHotKeysDefaultSetting = new Dictionary<string, string>();

        private NUIDocument m_ActiveDocument;
        public NDocumentManager m_DocMan = new NDocumentManager();
        //public ctlChart m_ActiveChart;
        //public ChartProperty cp = new ChartProperty();
        //public uctlHistoryDataManager m_ctlData = uctlHistoryDataManager.GetHistoryMgrInstance();
        //private HotKeyHandler hotkeyhandler_ = null;
        public static NPalette NevronPalette;
        public ColorScheme curColorScheme;
        //public DialogOrderType ObjDialogOrderType = null;
        private BackgroundWorker backgroundWorker1;
        private NCommandBarsState m_state;
        public ArrayList myAL = new ArrayList();
        //public IndicatorSelection m_selection = new IndicatorSelection();
        public List<string> symb_forexPair = new List<string>();

        public static string m_Style; //Nevron UI skin style
        public readonly string m_CmdArg; //The command line string
        private string m_ActiveDocumentName; //The active document name (ctlChart, ctlOrder, etc.)
        public static string PreviousClOrdIdTradeGrid = "";

        public static string Account_Bal = null;
        public static string strExePath_ = Application.StartupPath;
        public static System.Resources.ResourceManager rm = ResourceManager.CreateFileBasedResourceManager("MyApp", strExePath_ + "\\LanguageResources", null);
        private string[] loginData;
        public static string lang_string = "null";        


        #endregion

      
        private frmMainGTS()
        {            
            InitializeComponent();
        }
        private frmMainGTS(string LoadChart)
        {
            InitializeComponent();
            m_CmdArg = LoadChart;
            DockManagerSetting();
        }
     

        private void frmMainGTS_Load(object sender, EventArgs e)
        {
            //SpalashDisplay();
            InitClasses();
            objFunction.SetToolbarPosition();
            hotkeyhandler_ = new HotKeyHandler();
            DocManagerEvent();
            DockManagerSetting();
            resetToolbar();
            HotKeyEvent();
            ExecuitionPanel.Select();
            LoginLoad();

            Initializer objInit = Initializer.GetReference();

            switch (objInit.objSettingForm_.strStartUpWorkSapce_)
            {
                case "Previous":
                    LoadPreviousWorkspace();
                    StartApplication();
                    break;
                case "None":
                    StartApplication();
                    LoadInitialCharts();
                    break;
                case "Specific":
                    LoadSpecificWorkspace();
                    StartApplication();
                    break;
                default:
                    StartApplication();
                    break;
            }

            //clsGlobal.IsSaveWorkSpace = Properties.Settings.Default.IsSaveWorkSpace;
            //if (Properties.Settings.Default.IsStartupWorkspacePrevious == true)
            //{
            //    LoadPreviousWorkspace();
            //    StartApplication();
            //}
            //else if (Properties.Settings.Default.IsStartupWorkspaceNone == true)
            //{
            //    StartApplication();
            //    LoadInitialCharts();
            //}
            //else if (Properties.Settings.Default.IsStartupWorkspaceSpecific == true)
            //{
            //    LoadSpecificWorkspace();
            //    StartApplication();
            //}
            //else
            //{
            //    StartApplication();
            //}
            VerticallyAction();
            objFunction.DisableMenuOptions();
            navigatorPanel.Select();
            uctlForex_.LoadLastSetting();
            uctlMarketWatchInstance.LoadLastSetting();
        }

        private void StartApplication()
        {
            //OrderManager.getOrderManager().InitAccount();
            //Properties.Settings.Default.Style = "Office2007Silver";
            //GTS.Cls.Theme theme = new Theme();
            //theme.mdiNevronObject = this;
            //theme.UpdateStyle();

            //Initializer.GetReference().LoadSettingFormData(SettingFile);
            //objFunction.ApplySetting();
            //InitializeApplication();
            //uctlTerminal1.InitOrderManager();
            //uctlExecutionDetail1.InitMyOrderManager();

            ////this.Text = Properties.Settings.Default.AppName;
            //LoadChartFromCommandLine();
            //LoadSavedSettings4Application();
        }



        private void DocManagerEvent()
        {
            //try
            //{
            //    hotkeyhandler_ = new HotKeyHandler();
                
            //    m_DockManager.DocumentManager.DocumentClosed += new DocumentEventHandler(DocumentManager_DocumentClosed);
            //    m_DockManager.DocumentManager.DocumentClosing += new DocumentCancelEventHandler(DocumentManager_DocumentClosing);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error in initializing\nStacktrace:\n" + ex.StackTrace, "Galaxy Tradestation"
            //                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    Logger.LogEx(ex, "frmMainGTS", "DocManagerEvent()");
            //}
        }
        void DocumentManager_DocumentClosed(object sender, DocumentEventArgs e)
        {
        }
        private void HotKeyEvent()
        {
            //this.HotKeyPressed += new HotKeyPressedEventHandler(frmMainGTS_HotKeyPressed);
            //this.HotKeys.Add(new HotKey("Save", Keys.S, HotKey.HotKeyModifiers.MOD_ALT | HotKey.HotKeyModifiers.MOD_SHIFT));
            //this.TraycontextMenuStrip.ItemClicked += new ToolStripItemClickedEventHandler(TraycontextMenuStrip_ItemClicked);
        }
        //void frmMainGTS_HotKeyPressed(object sender, HotKeyPressedEventArgs e)
        //{
        //    try
        //    {
        //        switch (e.HotKey.Name)
        //        {
        //            //General hot keys
        //            case "Connection":
        //                break;
        //            case "NewWorkSpace":
        //                break;
        //            case "SaveAsWorkSpace":// SaveWorkspace();
        //                break;
        //            case "ShowSettings":
        //                objToolFunction.ShowSetting();
        //                break;
        //            case "ShowHotKeys":
        //                break;
        //            case "CloseApplication": //CloseApplication();
        //                break;
        //            case "Showhelp":
        //                objToolFunction.HelpUserManual();
        //                break;
        //            case "CheckForUpdates":
        //                break;
        //            case "RunPTLBuilder":
        //                break;
        //            //PanelHotKeys
        //            case "HideHeader":
        //                break;
        //            case "DetechAttchWindow":
        //                break;
        //            case "ViewMode":
        //                break;
        //            case "Search":
        //                break;
        //            case "ColumnsManager":
        //                break;
        //            case "TabelAlert":
        //                break;
        //            case "Filter":
        //                break;
        //            case "Settings":
        //                objToolFunction.ShowSetting();
        //                break;
        //            case "ColorSettings":
        //                break;
        //            case "Increasefontsize":
        //                break;
        //            case "Decreasefontsize":
        //                break;
        //            case "Print": objChartFunction.SnapPrint();
        //                break;
        //            //case "CloseApplication": 
        //            //    break;
        //            case "EmailSnapshot":
        //                break;
        //            case "SaveSapshot": objChartFunction.Snap_Save();
        //                break;
        //            case "Clipboard":
        //                break;

        //            // Trading hot keys
        //            case "CloseAllPositions": objOrderFunction.CloseAllFilledOrders(false);
        //                break;
        //            case "CancelAllOrders": objOrderFunction.CancelAllPendingOrders(false);// CancelAllOrdersRequestMesg();
        //                break;
        //            case "OpenOrderEntry": objOrderFunction.ShowOrderEntry();
        //                break;
        //            case "OpenReports":
        //                break;
        //            case "StartStopAutotrading":
        //                break;

        //            //Position Panel Hot Keys
        //            case "ClosePositions":
        //                break;
        //            case "ModifyPositions":
        //                break;

        //            // Order Panel
        //            case "ShowSLTPOrders":
        //                break;
        //            case "ModifyOrder":
        //                break;
        //            case "CancleOrder":
        //                break;

        //            // Informer Panel
        //            case "AddSymbol":
        //                break;
        //            case "RemoveSymbol":
        //                break;
        //            case "ShowAllSymbol":
        //                break;

        //            //Account Panel
        //            case "ViewReport":
        //                break;

        //            //Charts
        //            case "ChangeChartStyle":
        //                break;
        //            case "ZoomIn": objChartFunction.ZoomIn();
        //                break;
        //            case "ZoomOut": objChartFunction.ZoomOut();
        //                break;
        //            case "AutoManualScaling":
        //                break;
        //            case "ShowAll":
        //                break;
        //            case "ShowHidedatawindow":
        //                break;
        //            case "TrackCursor":
        //                break;
        //            case "AddInstrument":
        //                break;
        //            case "AddIndicator":
        //                break;
        //            case "AddTradingSystem":
        //                break;
        //            case "SetPeriodtick":
        //                break;
        //            case "SetPeriod1m":
        //                break;
        //            case "SetPeriod5m":
        //                break;
        //            case "SetPeriod15m":
        //                break;
        //            case "SetPeriod":
        //                break;
        //            case "SetPeriod1H":
        //                break;
        //            case "SetPeriod4h":
        //                break;
        //            case "SetPeriod1D":
        //                break;
        //            case "SetPeriod1w":
        //                break;
        //            case "SetPeriod1Month":
        //                break;
        //            case "SetPeriod1Y":
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ServerLog.Write("frmMain::frmMain_HotKeyPressed" + ex.ToString() + ex.StackTrace, true);
        //        Logger.LogEx(ex, "frmMainGTS", "frmMainGTS_HotKeyPressed(object sender, HotKeyPressedEventArgs e)");
        //    }
        //}
        private void resetToolbar()
        {

            //m_MenuBar.RowIndex = 0;

            //m_CmdBarsManager.Toolbars.Remove(nAnalysisToolbar);
            //m_CmdBarsManager.Toolbars.Remove(nAccountToolbar);
            //m_CmdBarsManager.Toolbars.Remove(nChartToolbar);
            //m_CmdBarsManager.Toolbars.Remove(ConnectivityToolbar);

            //m_CmdBarsManager.Toolbars.Remove(nLineStudiesToolbar);
            //m_CmdBarsManager.Toolbars.Remove(periodicityToolbar);
            //m_CmdBarsManager.Toolbars.Remove(nstandardToolbar);

            //m_CmdBarsManager.Toolbars.Add(nAnalysisToolbar);
            //m_CmdBarsManager.Toolbars.Add(nAccountToolbar);
            //m_CmdBarsManager.Toolbars.Add(nChartToolbar);
            //m_CmdBarsManager.Toolbars.Add(ConnectivityToolbar);

            //m_CmdBarsManager.Toolbars.Add(nLineStudiesToolbar);
            //m_CmdBarsManager.Toolbars.Add(periodicityToolbar);
            //m_CmdBarsManager.Toolbars.Add(nstandardToolbar);


            //nAnalysisToolbar.RowIndex = 1;
            //nAccountToolbar.RowIndex = 1;
            //nChartToolbar.RowIndex = 1;
            //ConnectivityToolbar.RowIndex = 1;
            
            //nLineStudiesToolbar.RowIndex = 2;
            //periodicityToolbar.RowIndex = 2;
            //nstandardToolbar.RowIndex = 2;       
        }

        void TraycontextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //switch (e.ClickedItem.Text)
            //{
            //    case "Open": Application_Open();
            //        break;
            //    case "Close": Application.Exit();
            //        break;
            //    default:
            //        break;
            //}
        }
        void Application_Open()
        {
            //notifyIcon1.Visible = false;
            //this.Show();
            //this.WindowState = FormWindowState.Maximized;
        }

     
        private void LoadSkinTheme(string filename)
        {
            //if(File.Exists(filename))
            //{
            //    NSkin skin = new NSkin();
            //    System.Environment.CurrentDirectory = Application.StartupPath;
            //    if (skin.Load(filename))
            //    {
            //        NSkinManager.Instance.Enabled = true;
            //        NSkinManager.Instance.Skin = skin;
            //    }
            //}
        }
        private void EnableControls(bool Enable)
        {
            //try
            //{
            //    mnuFileSaveChart.Enabled = Enable;
            //    cboPriceStyles.Enabled = Enable;
            //    cboIndicators.Enabled = Enable;
            //    mnuFileSaveImage.Enabled = Enable;
            //    mnuFileSaveTemplate.Enabled = Enable;
            //    mnuFileExport.Enabled = Enable;
            //    mnuLogout.Enabled = Enable;
            //    mnuView3D.Enabled = Enable;
            //    mnuViewScaleType.Enabled = Enable;
            //    mnuViewSeparators.Enabled = Enable;
            //    mnuViewShowXGrid.Enabled = Enable;
            //    mnuViewYGrid.Enabled = Enable;
            //    mnuDarvasBoxes.Enabled = Enable;
            //    mnuColors.Enabled = Enable;
            //    //mnuTechAnalysis.Enabled = Enable;
            //    // mnuTechicalAnalysis.Enabled = Enable;
            //    mnuZoomIn.Enabled = Enable;
            //    mnuZoomOut.Enabled = Enable;
            //    mnuApplyTemplate.Enabled = Enable;
            //    mnuScrollLeft.Enabled = Enable;
            //    mnuScrollRight.Enabled = Enable;
            //    mnuPriceStyle.Enabled = Enable;
            //    mnuPatternRecognition.Enabled = Enable;
            //    Application.DoEvents();
            //}
            //catch (Exception ex)
            //{
            //    ServerLog.Write("frmMainGTS::EnableControls" + ex.ToString() + ex.StackTrace, true);
            //    Logger.LogEx(ex, "frmMainGTS", "EnableControls(bool Enable)");
            //}
        }

        //public void MenubarState(ctlChart Activechart)
        //{
        //    if (Activechart.mnuc1min.Checked != true)//mnuc1min
        //        {                    
        //            submnu1min.Checked = false;
        //            cmd_1minute.Checked = false;
                   
        //        }
        //        else
        //        {
        //            submnu1min.Checked = true;
        //            cmd_1minute.Checked = true;
                  
        //        }

        //        if (Activechart.mnuc5Minutes.Checked != true)
        //        {
        //            submnu5min.Checked = false;
        //            cmd_5minute.Checked = false;
                   
        //        }
        //        else
        //        {
        //            submnu5min.Checked = true;
        //            cmd_5minute.Checked = true;
                  
        //        }
        //        if (Activechart.mnuc15Min.Checked != true)
        //        {
        //            submnu15min.Checked = false;
        //            cmd_15minute.Checked = false;
                 
        //        }
        //        else
        //        {
        //            submnu15min.Checked = true;
        //            cmd_15minute.Checked = true;
                   
        //        }
        //        if (Activechart.mnuc30minutes.Checked != true)
        //        {
        //            submnu30min.Checked = false;
        //            cmd_30minute.Checked = false;
                   
        //        }
        //        else
        //        {
        //            submnu30min.Checked = true;
        //            cmd_30minute.Checked = true;
        //        }
        //        if (Activechart.mnuc1Hour.Checked != true)
        //        {
        //            submnu1hour.Checked = false;
        //            cmd_1Hour.Checked = false;
        //        }
        //        else
        //        {
        //            submnu1hour.Checked = true;
        //            cmd_1Hour.Checked = true;
        //        }
        //        if (Activechart.mnuc4Hours.Checked != true)
        //        {
        //            submnu4hours.Checked = false;
        //            cmd_4Hour.Checked = false;
        //        }
        //        else
        //        {
        //            submnu4hours.Checked = true;
        //            cmd_4Hour.Checked = true;
        //        }
        //        if (Activechart.mnucDaily.Checked != true)
        //        {
        //            submnuDaily.Checked = false;
        //            cmd_Daily.Checked = false;
        //        }
        //        else
        //        {
        //            submnuDaily.Checked = true;
        //            cmd_Daily.Checked = true;
        //        }
        //        if (Activechart.mnucWeekly.Checked != true)
        //        {
        //            submnuWeekly.Checked = false;
        //            cmd_Weekly.Checked = false;
        //        }
        //        else
        //        {
        //            submnuWeekly.Checked = true;
        //            cmd_Weekly.Checked = true;
        //        }
        //        if (Activechart.mnucGrid.Checked != true)
        //        {
        //            mnuGrid.Checked = false;
        //            nChart_Grid.Checked = false;
        //        }
        //        else
        //        {
        //            mnuGrid.Checked = true;
        //            nChart_Grid.Checked = true;
        //        }
        //        if (Activechart.mnucZoomIn.Checked != true)
        //        {
        //            mnuZoomFunctionIn.Checked = false;
        //            nChart_ZoomIn.Checked = false;
        //        }
        //        else
        //        {
        //            mnuZoomFunctionIn.Checked = true;
        //            nChart_ZoomIn.Checked = true;
        //        }
        //        if (Activechart.mnucVolumes.Checked != true)
        //        {
        //            mnuVolume.Checked = false;
        //            nChart_Volume.Checked = false;
        //        }
        //        else
        //        {
        //            mnuVolume.Checked = true;
        //            nChart_Volume.Checked = true;
        //        }
        //}

        public void EnableDisabelEcoCalandar(bool isEnable)
        {
            //submnu15min.Enabled = isEnable;
            //submnu1hour.Enabled = isEnable;
            //submnu1min.Enabled = isEnable;
            //submnu30min.Enabled = isEnable;
            //submnu4hours.Enabled = isEnable;
            //submnu5min.Enabled = isEnable;
            //submnuMonthly.Enabled = isEnable;
            //submnuWeekly.Enabled = isEnable;
            //submnuDaily.Enabled = isEnable;
            //periodicityToolbar.Enabled = isEnable;
        }

        public void RemoveChartInNavigator(string chartName)
        {
            //foreach (TreeNode node in uctlNavigatorInstance.galaxyTreeView.Nodes["Node_WalCapitalGalaxy"].Nodes["Node_Charts"].Nodes)
            //{
            //    if (chartName.Contains(node.Text))
            //    {
            //        node.Remove();
            //        nChart_Grid.Checked = false;
            //    }
            //}
        }
        private void RemoveChartFromMenu(string p)
        {
            //foreach (NCommand cmd in mnuWindows.Commands)
            //{
            //    if (p.Contains(cmd.Properties.Text))
            //    {
            //        mnuWindows.Commands.Remove(cmd);
            //        break;
            //    }

            //}
        }
        public void EnableChartControl()
        {
            //if (m_DockManager.DocumentManager.Documents.Length < 2)
            //{
            //    EnableControls(false);
            //}
        }
        public void UnCheckAll()
        {
            //submnu1min.Checked = false;
            //cmd_1minute.Checked = false;
            //submnu5min.Checked = false;
            //cmd_5minute.Checked = false;
            //submnu15min.Checked = false;
            //cmd_15minute.Checked = false;
            //submnu30min.Checked = false;
            //cmd_30minute.Checked = false;
            //submnu1hour.Checked = false;
            //cmd_1Hour.Checked = false;
            //submnu4hours.Checked = false;
            //cmd_4Hour.Checked = false;
            //submnuDaily.Checked = false;
            //cmd_Daily.Checked = false;
            //submnuWeekly.Checked = false;
            //cmd_Weekly.Checked = false;
            //submnuMonthly.Checked = false;
            //cmd_Monthly.Checked = false;
        }
        private void DockManagerSetting()
        {
            m_DocMan = m_DockManager.DocumentManager;
            //Set visual settings here
            m_DockManager.DocumentStyle.TabAlign = TabAlign.Bottom;
            m_DockManager.DocumentStyle.StripButtons = DocumentStripButtons.VS2005;
            m_DocMan.ActiveDocumentChanged += new DocumentEventHandler(m_DocMan_ActiveDocumentChanged);
            m_DocMan.DocumentActivated += new DocumentEventHandler(m_DocMan_DocumentActivated);
            m_DocMan.DocumentClosing += new DocumentCancelEventHandler(m_DocMan_DocumentClosing);
            m_DocMan.DocumentInserted += new DocumentEventHandler(m_DocMan_DocumentInserted);
            m_DocMan.DocumentClosed += new DocumentEventHandler(m_DocMan_DocumentClosed);
             
        }

        #region m_DocMan Events
        private void m_DocMan_DocumentClosed(object sender, DocumentEventArgs e)
        {
            NDocumentManager ndm = (NDocumentManager)sender;
            if (ndm != null && ndm.Documents.Length == 0)
            {
                EnableDisabelEcoCalandar(false);
                nChartToolbar.Enabled = false;
                nLineStudiesToolbar.Enabled = false;

            }
        }
        private void m_DocMan_DocumentInserted(object sender, DocumentEventArgs e)
        {
            m_ActiveDocument = e.Document;
            try
            {
                if (m_ActiveDocument.Client != null && m_ActiveDocument.Client.Name == "ctlChart")
                {
                    EnableDisabelEcoCalandar(true);
                    nLineStudiesToolbar.Enabled = true;
                    nChartToolbar.Enabled = true;
                }
                else
                {
                    //m_DocMan.CloseActiveDocument();
                }
            }
            catch(Exception ex)
            {
                Logger.LogEx(ex, "frmMainGTS", "void m_DocMan_DocumentInserted(object sender, DocumentEventArgs e)");
            }
        }
        private void m_DocMan_DocumentClosing(object sender, DocumentCancelEventArgs e)
        {
            EnableControls(true);
            try
            {
                if (e.Document.Client.Name != null)
                {
                    switch (e.Document.Client.Name)
                    {
                        case "ctlChart":
                            {
                                ctlChart chart = (ctlChart)e.Document.Client;

                                if (chart.Subscribers > 0)
                                {
                                    MessageBox.Show(frmMainGTS.rm.GetString("chart_currently_use"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    e.Cancel = true;
                                }
                                //resetToolbar();
                                RemoveChartInNavigator(e.Document.Text);
                                RemoveChartFromMenu(e.Document.Text);
                                EnableChartControl();
                                UnCheckAll();
                            }
                            break;
                        case "ctlData":
                            //This window may not be allowed to close                        

                            e.Cancel = true;
                            break;
                        case "ctlWeb":
                            if (e.Document.Text == "ForexFactory")
                            {

                                // mnuEcoCal.Enabled = true;

                                //mnuEcoCal.Enabled = true;

                            }
                            else if (e.Document.Text == "Dukascopy")
                            {

                                //  mnuNewsFeed.Enabled = true;

                                //mnuNewsFeed.Enabled = true;

                            }
                            break;
                        case "ctlPortfolio":
                            //This window may not be allowed to close
                            e.Cancel = true;
                            break;
                        case "ctlAlert":
                            //ctlAlert alert = (ctlAlert)e.Document.Client;
                            //alert.Disconnect();
                            //alert.SaveAlert(true);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ServerLog.Write("frmMainGTS::M_DocMan_OnDocumentClosing" + ex.ToString() + ex.StackTrace, true);
                Logger.LogEx(ex, "frmMainGTS", "m_DocMan_DocumentClosing(object sender, DocumentCancelEventArgs e)");
            }
            m_Closed = true;
        }
        private void m_DocMan_DocumentActivated(object sender, DocumentEventArgs e)
        {
            try
            {
                if (m_Closed)
                {
                    m_Closed = false;
                }
                else
                {
                    return;
                }
                if (e.Document.Client.Name == "ctlChart" && !e.Document.Text.Contains("Offline"))
                {
                    m_ActiveChart = (ctlChart)e.Document.Client;
                    m_ActiveChart.EnableControls(true);
                    MenubarState(m_ActiveChart);
                }
                m_ActiveDocument = e.Document;
                string symbol = m_ActiveDocument.Key.ToString();

            }
            catch (Exception ex)
            {
                ServerLog.Write("frmMainGTS::M_DocMan_OnDocumentActivated" + ex.ToString() + ex.StackTrace, true);
                Logger.LogEx(ex, "frmMainGTS", "m_DocMan_DocumentActivated(object sender, DocumentEventArgs e)");
            }
        }
        private void m_DocMan_ActiveDocumentChanged(object sender, DocumentEventArgs e)
        {
            bool enable = true;
            //Disable the controls if not the same type of form
            try
            {
                if (e.Document.Text == "Eco Calendar")
                {
                    mnu3DStyle.Checked = false;
                }
                if (e.Document.Client == null && e.Document.Text != "Eco Calendar")
                {
                    m_DocMan.RemoveDocument(e.Document);

                }

                if (e.Document.Client == null && e.Document.Text == "Eco Calendar")
                {
                    EcoCalendarDis = false;
                    m_DocMan.RemoveDocument(e.Document);
                    m_CmdBarsManager_CommandClicked(null, null);
                    //FundamentalAnalysisMenu_Click(null, null);
                }
                if (m_ActiveDocumentName != e.Document.Client.Name)
                {
                    enable = false;
                }
                int l = m_DockManager.DocumentManager.Documents.Length;

                if (!enable)
                {
                    EnableControls(false);
                }
                if (e.Document.Client.Name == "ctlChart")
                {


                    m_ActiveChart = (ctlChart)e.Document.Client;
                    mnu3DStyle.Checked = m_ActiveChart.StockChartX1.ThreeDStyle;
                    m_ActiveChart.DrawSelection();
                    m_ActiveChart.UpdateMenus();
                    if (!enable)
                    {
                        m_ActiveChart.EnableControls(true);

                    }
                    MenubarState(m_ActiveChart);
                    EnableDisabelEcoCalandar(true);
                    foreach (NCommand cmd in mnuWindows.Commands)
                    {
                        // if (cmd.ToString().Equals(e.Document.Text))
                        if (cmd.ToString().Contains(e.Document.Text))
                        {
                            m_ActiveChart.UncheckAllSelection_Charts();
                            cmd.Checked = true;
                        }
                    }


                }

                ShowStatus("");

                m_ActiveDocumentName = e.Document.Client.Name;

                if (e.Document.Client.Name == "crystalReportViewer1")
                {
                    EnableDisabelEcoCalandar(false);
                }
                //else
                //{
                //    EnableDisabelEcoCalandar(true);

                //}
                if (e.Document.Text.Contains("Offline") && e.Document.Client.Name == "ctlChart")
                {
                    EnableDisabelEcoCalandar(false);
                }

            }

            catch (Exception ex)
            {
                ServerLog.Write("frmMainGTS::M_DocMan_OnActiveDocumentChanged" + ex.ToString() + ex.StackTrace, true);
                Logger.LogEx(ex, "frmMainGTS", "m_DocMan_ActiveDocumentChanged(object sender, DocumentEventArgs e)");
            }
        }
        private void DocumentManager_DocumentClosing(object sender, DocumentCancelEventArgs e)
        {
            if (e.Document.Client != null && e.Document.Client.Name == "ctlChart")
            {
                ctlChart chart = (ctlChart)e.Document.Client;
                Ctrl.uctlNewHistoryManager.GetHistoryMgrInstance().RemoveHistoryWatchItem(chart);
                //uctlHistoryDataManager.GetHistoryMgrInstance().RemoveWatch(chart.m_Symbol, chart);
                chart = null;
            }
        }
        #endregion

        public static frmMainGTS GetReference()
        {
            if (FMInstanceGTS_ == null)
                FMInstanceGTS_ = new frmMainGTS();

            return FMInstanceGTS_;
        }

        private void LoadPreviousWorkspace()
        {
            NDockingFrameworkState state = new NDockingFrameworkState(m_DockManager);
            state.ResolveDocumentClient += state_ResolveDocumentClient;
            state.Format = Nevron.Serialization.PersistencyFormat.Binary;
            string CMDBarPath = string.Empty;
            string WrkSpacePath = string.Empty;
            getToolbarSettingPath(out WrkSpacePath, out CMDBarPath);
            state.Load(WrkSpacePath);
            m_state.Load(CMDBarPath);
            state.ResolveDocumentClient -= state_ResolveDocumentClient;
        }
        private void LoadSpecificWorkspace()
        {
            //NDockingFrameworkState state = new NDockingFrameworkState(m_DockManager);
            //state.ResolveDocumentClient +=state_ResolveDocumentClient;
            //state.Format = Nevron.Serialization.PersistencyFormat.Binary;
            //string CMDBarPath = string.Empty;
            //string WrkSpacePath = string.Empty;
            //getToolbarSettingPath(out WrkSpacePath, out CMDBarPath);
            //state.Load(WrkSpacePath);
            //m_state.Load(CMDBarPath);
            //state.ResolveDocumentClient -= state_ResolveDocumentClient;
        }
        private void SpalashDisplay()
        {
            //SplashScreen splash = new SplashScreen();
            //splash.Show();
            //Application.DoEvents();
            //while (!splash.Finished)
            //{
            //    Thread.Sleep(100);
            //    Application.DoEvents();
            //}
            //splash.Close();
            //Application.DoEvents();
        }
        private void LoadChartFromCommandLine()
        {
            //if (!string.IsNullOrEmpty(m_CmdArg))
            //{
            //    if (System.IO.File.Exists(m_CmdArg))
            //    {
            //        ctlChart ctl = new ctlChart(this, null, m_CmdArg) { Dock = DockStyle.Fill };
            //        NUIDocument document = new NUIDocument("", -1, ctl);
            //        m_DockManager.DocumentManager.AddDocument(document);
            //        ctl.StockChartX1.LoadFile(m_CmdArg);
            //        ctl.StockChartX1.LoadFile(m_CmdArg);
            //        document.Text = ctl.StockChartX1.Symbol;
            //        ctl.EnableControls(true);
            //    }
            //}

        }
        public void LoadSavedSettings4Application()
        {
            //if (!Properties.Settings.Default.LastPos.IsEmpty)
            //{
            //    Location = Properties.Settings.Default.LastPos;
            //    Size = Properties.Settings.Default.LastSize;
            //    if (Width < 800) Width = 800;
            //    if (Height < 550) Height = 550;
            //}
            //if (Properties.Settings.Default.WindowState == FormWindowState.Maximized)
            //{
            //    Visible = false;
            //    Application.DoEvents();
            //    Height = Screen.PrimaryScreen.Bounds.Height;
            //    Width = Screen.PrimaryScreen.Bounds.Width;
            //    WindowState = FormWindowState.Maximized;
            //    Width = Properties.Settings.Default.LastSize.Width;
            //    Height = Properties.Settings.Default.LastSize.Height;
            //    Application.DoEvents();
            //    Visible = true;
            //}
        }
        private void LoginLoad()
        {
            //frmDataLogin oLogin = new frmDataLogin(this, true);
            //DialogResult ret = oLogin.ShowDialog();
            //timeRefresh.Start();
            ////PopulateIndicatorlist();
            //m_CmdBarsManager.ImageList = imageList1;
            //m_state = new NCommandBarsState(m_CmdBarsManager);
            //m_state.PersistencyFlags = NCommandBarsStateFlags.All;
            //mnuExecView.Properties.Text = "Execution View";
            //GetchartSymbol();
            //SetDefaultSetting();
            //LoadRptContent();
            //SetPanelStatusInMainForm();
            //SymbolPanel.Select();
            //InitializeBackgroundWorker();         
        }
        private void PopulateIndicatorlist()
        {
            //TreeNode tNode = uctlNavigatorInstance.galaxyTreeView.Nodes["Node_WalCapitalGalaxy"].Nodes["Node_Indicator"];
            //tNode.ImageIndex = 0;
            //tNode.SelectedImageIndex = 0;
            //IndicatorList.PopulateIndicatorList(ref tNode);
            
        }
        public void GetchartSymbol()
        {
            //foreach (object objSymbol in uctlForex.GetSymbolObjectArray())
            //{
            //    myAL.Add(objSymbol);
            //}
            //AddSymbolinToolbarItem(myAL);
        }

        public void AddSymbolinToolbarItem(ArrayList myAL)
        {

            //NCommand commandItem = new NCommand();
            //commandItem.Properties.Text = "Forex";
            //commandItem.Properties.BeginGroup = true;
            //if (!nChart_newChart.Commands.Contains(commandItem))
            //{
            //    nChart_newChart.Commands.Insert(nChart_newChart.Commands.Count, commandItem);

            //    for (int i = 0; i < myAL.Count; i++)
            //    {
            //        NCommand subCmdItem = new NCommand();
            //        subCmdItem.Properties.Text = myAL[i].ToString();
            //        commandItem.Commands.Add(subCmdItem);
            //        subCmdItem.Click += new CommandEventHandler(commandItem_Click);
            //    }
            //}
        }

        private void commandItem_Click(object sender, CommandEventArgs e)
        {
            //objChartFunction.ShowSpecificChart(e.Command.ToString());
        }

        private void SetDefaultSetting()
        {
            //Properties.Settings.Default.setGeneral_chkSaveDataOfflineuse = true;
            //Properties.Settings.Default.setGeneral_chkAutomaticConnection = true;
            //Properties.Settings.Default.setWorkspace_chkAutoSaveWorkSpace = true;
            //Properties.Settings.Default.setWorkspace_rdoPrevious = true;
            //Properties.Settings.Default.setFXCell_chkShowArrows = true;
            //Properties.Settings.Default.setFXCell_rdoBuySell = true;
            //Properties.Settings.Default.setFXCell_rdoDefColor = true;
            //Properties.Settings.Default.Setgeneral_rdoDockable = true;
            //Properties.Settings.Default.Save();
        }

        public void SetPanelStatusInMainForm()
        {
                //if (TerminalPanel.Visible == true)
                //{
                //    mnuTerminal.Checked = true;
                //    nStdTerminal.Checked = true;
                //}
                //else
                //{
                //    mnuTerminal.Checked = false;
                //    nStdTerminal.Checked = false;
                //}
                //if (SymbolPanel.Visible == true)
                //{
                //    mnuMarketWatch.Checked = true;
                //    nStdMktWatch.Checked = true;
                //}
                //else
                //{
                //    mnuMarketWatch.Checked = false;
                //    nStdMktWatch.Checked = false;
                //}
                //if (navigatorPanel.Visible == true)
                //{
                //    mnuNavigator.Checked = true;
                //    nStdNavigtor.Checked = true;
                //}
                //else
                //{
                //    mnuNavigator.Checked = false;
                //    nStdNavigtor.Checked = false;
                //}
                //if (QuotePanel.Visible == true)
                //{
                //    mnuViewForex.Checked = true;
                //    // nStdQuote.Checked = true;
                //}
                //else
                //{
                //    mnuViewForex.Checked = false;
                //    // nStdQuote.Checked = false;
                //}
                //if (nStatusBar.Visible == true)
                //{
                //    mnuStatusbar.Checked = true;
                //}
                //else
                //{
                //    mnuStatusbar.Checked = false;
                //}
        }



        private void LoadRptContent()
        {
            //    try
            //    {
            //        string conString = ConfigurationManager.AppSettings["ConString"].ToString();
            //           strcon = new SqlConnection(conString);
            //           strcon.Open();
            //           SqlCommand cmd = new SqlCommand("sp_EconomicCalendar_Sel", strcon);
            //           cmd.Connection = strcon;
            //           cmd.CommandType = CommandType.StoredProcedure;
            //           System.Data.SqlClient.SqlParameter[] oParameters = new System.Data.SqlClient.SqlParameter[0];

            //           System.Data.DataSet dsReport_Performa = Helper.SqlHelper.ExecuteDataset(strcon, System.Data.CommandType.StoredProcedure, "sp_EconomicCalendar_Sel", oParameters);

            //        //dsReport_Performa.WriteXml(getApplicationPath() + @"\EconomicCalendar.xml", System.Data.XmlWriteMode.WriteSchema);

            //        string fileLoc = getApplicationPath() + @"\EconomicCalendar.xml";
            //        if (System.IO.File.Exists(fileLoc))
            //        {
            //          dsReport_Performa.ReadXml(fileLoc, System.Data.XmlReadMode.ReadSchema);
            //        }
            //        else
            //        {
            //            dsReport_Performa.WriteXml(getApplicationPath() + @"\EconomicCalendar.xml", System.Data.XmlWriteMode.WriteSchema);
            //        }
            //        if (dsReport_Performa.Tables[0].Rows.Count > 0)
            //        {
            //            FrmReport rptView = ShowReport("RptEconomicCalendar.rpt", "EconomicCalendar.xml");
            //            //Added by vivek for temporary sghowing 
            //            rptView.TopLevel = false;
            //            System.Action a = () =>
            //            {
            //                NUIDocument docWeb = new NUIDocument("Eco Calendar", -1, rptView);
            //                m_DockManager.DocumentManager.AddDocument(docWeb);
            //                this.m_DockManager.DocumentStyle.TabAlign = TabAlign.Bottom;
            //                rptView.Show();
            //                rptView.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            //            };
            //            if (this.InvokeRequired)
            //            {
            //                this.BeginInvoke(a);
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    { }
        }

        private void InitializeBackgroundWorker()
        {
            backgroundWorker1 = null;
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.RunWorkerAsync();
        }

        void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //if (e.Error != null)
            //{
            //}
            //else if (e.Cancelled)
            //{
            //}
            //else
            //{
            //    objFunction.AddSymbolinQ(myAL);
            //}
            
        }

        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            e.Result = "Completed";
        }

        public struct structServerStatus
        {
            public string ServerName;
            public bool isConnected;
        }

        public delegate void DlgConnection_QuotesStatus(bool IsQuotesStatus);
        public void Connection_QuotesStatus(bool IsQuotesStatus)
        {
            try
            {
                if (InvokeRequired)
                {
                    this.BeginInvoke(new DlgConnection_QuotesStatus(Connection_QuotesStatus), IsQuotesStatus);
                }
                else
                {
                    structServerStatus status = new structServerStatus
                    {
                        isConnected = IsQuotesStatus,
                        ServerName = "Quote Server"

                    };
                    ThreadPool.QueueUserWorkItem(new WaitCallback(ShowConnectivityPopUP), status);
                    // ShowConnectivityPopUP("Quote Server", IsQuotesStatus);
                    if (IsQuotesStatus == true)
                    {

                        Quotes_Connectivity.Properties.ImageIndex = 120;
                        Quotes_Connectivity.Properties.TooltipText = "Quote Server Connected";
                        Quotes_Connectivity.Properties.Text = "Quote Server Connection Status";
                        ConnectQuoteSrvr.Properties.ImageIndex = 120;
                        clsGlobal.IsQuoteServerConnected = true;
                        WSounds player = new WSounds();
                        player.Play(Initializer.GetReference().objSettingForm_.lstSoundPath_[0], player.SND_FILENAME | player.SND_ASYNC);
                    }
                    else
                    {
                        Quotes_Connectivity.Properties.ImageIndex = 84;
                        Quotes_Connectivity.Properties.TooltipText = "Quote Server Disconnected";
                        Quotes_Connectivity.Properties.Text = "Quote Server Connection Status";
                        ConnectQuoteSrvr.Properties.ImageIndex = 84;
                        clsGlobal.IsQuoteServerConnected = false;
                        WSounds player = new WSounds();
                        player.Play(Initializer.GetReference().objSettingForm_.lstSoundPath_[0], player.SND_FILENAME | player.SND_ASYNC);
                    }
                    //
                    //Load Default Chart
                    try
                    {
                        VerticallyAction();
                        //objChartFunction.LoadDefaultChart(Properties.Settings.Default.def_EURUSD);
                        //objChartFunction.LoadDefaultChart(Properties.Settings.Default.def_GBPUSD);
                        //objChartFunction.LoadDefaultChart(Properties.Settings.Default.def_USDCHF);
                        //objChartFunction.LoadDefaultChart(Properties.Settings.Default.def_USDJPY);
                    }
                    catch (Exception ex) 
                    {
                        Logger.LogEx(ex, "frmMainGTS", "Connection_QuotesStatus(bool IsQuotesStatus) Region1");
                    }

                    //For enabling Timer of Alert  

                    AlertManager.Getreference().enableTimer();
                }
            }
            catch (Exception ex)
            {
                Logger.LogEx(ex, "frmMainGTS", "Connection_QuotesStatus(bool IsQuotesStatus) Region2");
            }
        }
        public delegate void DlgConnection_OrderStatus(bool IsOrderStatus);
        public void Connection_OrderStatus(bool IsOrderStatus)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new DlgConnection_OrderStatus(Connection_OrderStatus), IsOrderStatus);
            }
            else
            {
                structServerStatus status = new structServerStatus
                {
                    isConnected = IsOrderStatus,
                    ServerName = "Order Server"

                };
                ThreadPool.QueueUserWorkItem(new WaitCallback(ShowConnectivityPopUP), status);

                if (IsOrderStatus == true)
                {
                    Order_Connectivity.Properties.ImageIndex = 120;
                    Order_Connectivity.Properties.TooltipText = "Order Server Connected";
                    Order_Connectivity.Properties.Text = "Order Server Connection Status";
                    ConnectTrdSrvr.Properties.ImageIndex = 120;
                }
                else
                {
                    Order_Connectivity.Properties.ImageIndex = 84;
                    Order_Connectivity.Properties.TooltipText = "Order Server Disconnected";
                    Order_Connectivity.Properties.Text = "Order Server Connection Status";
                    ConnectTrdSrvr.Properties.ImageIndex = 84;
                    WSounds player = new WSounds();
                    player.Play(Initializer.GetReference().objSettingForm_.lstSoundPath_[0], player.SND_FILENAME | player.SND_ASYNC);
                }
            }
        }

        private void ShowConnectivityPopUP(object objStat)
        {
            Action a = () =>
            {
                structServerStatus ServerStat = (structServerStatus)objStat;
                try
                {
                    string conn = "";
                    string color = "";
                    int ImageIndex = 120;
                    // string ServerName = "Quote Server";
                    if (ServerStat.isConnected)
                    {
                        conn = "connected";
                        color = "green";

                    }
                    else
                    {
                        conn = "disconnected";
                        color = "red";
                        ImageIndex = 84;
                    }
                    NPopupNotify SkinPopup = new NPopupNotify();
                    SkinPopup.PredefinedStyle = PredefinedPopupStyle.Skinned;
                    SkinPopup.PreferredBounds = new Rectangle(SkinPopup.PreferredBounds.Left, SkinPopup.PreferredBounds.Right, 120, 90);
                    SkinPopup.Font = new Font("Verdana", 9.0f);
                    SkinPopup.Caption.Content.Text = "Connection Status";

                    NImageAndTextItem content = SkinPopup.Content;
                    content.Image = Quotes_Connectivity.Properties.ImageList.Images[ImageIndex];
                    content.ImageSize = new Nevron.GraphicsCore.NSize(23, 23);

                    content.TextMargins = new NPadding(0, 4, 0, 0);
                    content.Text = "<b><font color='" + color + "'>" + ServerStat.ServerName + " " + conn + "</font></b>";

                    PopupAnimation animation = PopupAnimation.None;
                    animation |= PopupAnimation.Fade;
                    animation |= PopupAnimation.Slide;

                    SkinPopup.AutoHide = true;
                    SkinPopup.VisibleSpan = 4000;
                    SkinPopup.Opacity = 255;
                    SkinPopup.Animation = animation;
                    SkinPopup.AnimationDirection = PopupAnimationDirection.BottomToTop;
                    SkinPopup.VisibleOnMouseOver = false;
                    SkinPopup.FullOpacityOnMouseOver = false;
                    SkinPopup.AnimationInterval = 20;
                    SkinPopup.AnimationSteps = 19;
                    SkinPopup.Palette.Copy(NUIManager.Palette);

                    SkinPopup.Show();
                }
                catch (Exception ex)
                {
                    Logger.LogEx(ex, "frmMainGTS", "CShowConnectivityPopUP(object objStat)");
                }
            };

            if (this.InvokeRequired)
            {
                this.Invoke(a);
            }
            else
            {
                this.Invoke(a);
            }

        }

        private void InitializeApplication()
        {
            this.Activated += new EventHandler(frmMainGTS_Activated);
            this.Deactivate += new EventHandler(frmMainGTS_Deactivate);
            this.MdiChildActivate += new EventHandler(frmMainGTS_MdiChildActivate);

            int index = QuotePanel.TabIndex;

            QuotePanel.ParentChanged += new EventHandler(QuotePanel_ParentChanged);
            QuotePanel.AutoHideDisplay += new PanelEventHandler(QuotePanel_AutoHideDisplay);
            QuotePanel.AutoHideClose += new PanelEventHandler(QuotePanel_AutoHideClose);
            QuotePanel.Validated += new EventHandler(QuotePanel_Validated);
            QuotePanel.DoubleClick += new EventHandler(QuotePanel_DoubleClick);
            QuotePanel.MouseDoubleClick += new MouseEventHandler(QuotePanel_MouseDoubleClick);


            timeRefresh.Enabled = true;

        }



        #region FrmMainGTS Events

        private void frmMainGTS_Activated(object sender, EventArgs e)
        {
            objFunction.LoadHotKeysDefaultSetting();
        }

        private void frmMainGTS_Deactivate(object sender, EventArgs e)
        {
            _listHotKeysDefaultSetting.Clear();
            this.HotKeys.Clear();
        }

        private void frmMainGTS_MdiChildActivate(object sender, EventArgs e)
        {
            objFunction.LoadHotKeysDefaultSetting();
        }
        #endregion

        #region QuotePanel Events
        private void QuotePanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            QuotePanel.Hide();
            QuotePanel.Show();
        }
        private void QuotePanel_DoubleClick(object sender, EventArgs e)
        {
            QuotePanel.Hide();
            QuotePanel.Show();

        }
        private void QuotePanel_ParentChanged(object sender, EventArgs e)
        {
            QuotePanel.Hide();
            QuotePanel.Show();
        }
        private void QuotePanel_DragLeave(object sender, EventArgs e)
        {
            QuotePanel.Hide();
            QuotePanel.Show();
        }
        private void QuotePanel_AutoHideClose(object sender, PanelEventArgs e)
        {
            QuotePanel.Hide();
            QuotePanel.Show();
        }
        private void QuotePanel_Validated(object sender, EventArgs e)
        {
            // QuotePanel.Hide();
            // QuotePanel.Show();
        }
        private void QuotePanel_AutoHideDisplay(object sender, PanelEventArgs e)
        {
            QuotePanel.Hide();
            QuotePanel.Show();
        }
        private void QuotePanel_EndDragging(object sender, PanelDragEventArgs e)
        {
            QuotePanel.Hide();
            QuotePanel.Show();
        }
        #endregion

        public void InitClasses()
        {
            objFunction = new clsUtilityFunctions();
            objChartFunction = new clsChartFunctions();
            objOrderFunction = new clsOrderFunctions();
            objSaveLoadWorkSpace = new clsSaveAndLoadFunctions();
            objToolFunction = new clsToolbarFunctions();
            objConstructorFunc = new clsConstructorFunctions();
        }

        private void LoadInitialCharts()
        {
            try
            {
                VerticallyAction();
                objChartFunction.LoadDefaultChart(Properties.Settings.Default.def_EURUSD);
                //System.Threading.Thread.Sleep(200);
                objChartFunction.LoadDefaultChart(Properties.Settings.Default.def_GBPUSD);
                //System.Threading.Thread.Sleep(200);
                objChartFunction.LoadDefaultChart(Properties.Settings.Default.def_USDCHF);
                //System.Threading.Thread.Sleep(200);
                objChartFunction.LoadDefaultChart(Properties.Settings.Default.def_USDJPY);
            }
            catch (Exception ex) 
            {
                Logger.LogEx(ex, "frmMainGTS", "LoadInitialCharts()");
            }
        }

        public void VerticallyAction()
        {
            m_DockManager.DocumentStyle.DocumentViewStyle = DocumentViewStyle.MdiTabbed;
           
            Properties.Settings.Default.DVStd = DocumentViewStyle.MdiTabbed.ToString();
            Invalidate();
        }
        private void timeRefresh_Tick(object sender, EventArgs e)
        {
            if (GetReference() != null)
            {
                objFunction = new clsUtilityFunctions();
                if (Initializer.GetReference().objSettingForm_.ClockArray_ != null)
                    objFunction.SetTimeSetting();
            }
        }

        public void changeChartDocument(NUIDocument doc)
        {
            if (doc != null)
            {
                m_ActiveChart = (ctlChart)doc.Client;
                m_ActiveChart.DrawSelection();
                m_ActiveChart.UpdateMenus();
            }
            objChartFunction.MenubarState(m_ActiveChart);
        }
        public void TreeView_DoubleClick(object sender, EventArgs e)
        {
            foreach (NUIDocument doc in m_DocMan.Documents)
            {
                if (doc.Text == e.ToString())
                {
                    ctlChart chart = (ctlChart)doc.Client;
                    m_ActiveDocumentName = doc.Client.Name;
                    //doc.Client = GetChart();
                }
            }
        }
        public void SetChartToolBarVolume(bool status)
        {
          nChart_Volume.Checked = status;
        }
        
        public string GetDefaultVolumeOrderDefaultType(string symbol)
        {
            string Volume = string.Empty;
            foreach (SettingFormData.structOrderDefaultByType item in Initializer.GetReference().objSettingForm_.lstOrderDefaultByType)
            {
                if (item.strSymbol_.Equals(symbol,StringComparison.OrdinalIgnoreCase))
                {
                    if (Initializer.GetReference().objSettingForm_.bisPositionSizeInLot_)
                    {
                        Volume = item.strQuantityAmount_;
                        //Volume = ((int)(Convert.ToDouble(Volume) * 100000)).ToString();
                    }
                    else
                        Volume = item.strQuantityAmount_;
                    break;
                }
            }
            
            ////try
            ////{
            //    MemberInfo[] memberInfos = typeof(SettingFormData.OrderSymbolList).GetMembers(BindingFlags.Public | BindingFlags.Static);
            //    for (int loop1 = 0; loop1 < memberInfos.Length; loop1++)
            //    {
            //        if (symbol == memberInfos[loop1].Name.ToString())
            //        {
            //            if (Initializer.GetReference().objSettingForm_.bisPositionSizeInLot_)
            //            {
            //                Volume = Initializer.GetReference().objSettingForm_.lstOrderDefaultByType[loop1].strQuantityAmount_;
            //                //Volume = ((int)(Convert.ToDouble(Volume) * 100000)).ToString();
            //            }
            //            else
            //                Volume = Initializer.GetReference().objSettingForm_.lstOrderDefaultByType[loop1].strQuantityAmount_;
            //            break;
            //        }
            //    }
            ////}
            ////catch (Exception ex)
            ////{
            ////    MessageBox.Show(ex.Message);
            ////}
            return Volume;
        }
        public void ShowStatus(string Status)
        {
            // nStatusBar.Panels[0].Text = Status;
        }
        public void ChartName_Click(object sender, CommandEventArgs e)
        {
            foreach (NUIDocument doc in m_DocMan.Documents)
            {
                if (doc.Text == e.Command.ToString())
                {
                    ctlChart chart = (ctlChart)doc.Client;
                    m_ActiveDocumentName = doc.Client.Name;
                }
            }
        }


        public bool CheckYourMoney5PercentRule(double YourMoney)
        {
            if (Account_Bal != null)
            {
                double Check5perofBalance = (Convert.ToDouble(Account_Bal) * 5) / 100;
                if (Math.Round(Check5perofBalance, 2) >= Math.Round(YourMoney, 2))
                    return true;
                else
                    return false;
            }
            return true;
        }
        public void GetLoginCredential(string[] LoginInfo)
        {
            loginData = LoginInfo;
        }

        public void PlaySound(SettingFormData.eSoundIndex Index)
        {
            if (!Initializer.GetReference().objSettingForm_.bIsSoundEnable_)
                return;
            string soundFile = Initializer.GetReference().objSettingForm_.lstSoundPath_[(int)Index];
            if (!System.IO.File.Exists(soundFile))
                return;
            WSounds player = new WSounds();
            player.Play(soundFile, player.SND_FILENAME | player.SND_ASYNC);
        }
        private static string SaveDialog(string Filter)
        {
            SaveFileDialog flSaveDialog = new SaveFileDialog
            {
                Filter = "Stock Chart Files (*.icx)|*.icx|All files (*.*)|*.*",
                Title = "Save",
                CheckFileExists = false,
                InitialDirectory =
                Environment.GetFolderPath(
                Environment.SpecialFolder.DesktopDirectory)
            };
            flSaveDialog.ShowDialog();
            return flSaveDialog.FileName;
        }

        private void frmMainGTS_FormClosing(object sender, FormClosingEventArgs e)
        {
            Initializer objInit = Initializer.GetReference();
            if (!objInit.objSettingForm_.bAutoSaveWorkSpaceOnExit_)
            {
                DialogResult result = clsGlobal.ShowExitMessageBox("Are you want save this Workspace");
                switch (result)
                {
                    case DialogResult.Yes:
                        {
                            try
                            {
                                switch (objInit.objSettingForm_.strStartUpWorkSapce_)
                                {
                                    case "Previous":
                                        SaveWorkspaceB4Exit();
                                        SaveAndExit();
                                        break;
                                    case "Specific":
                                        SaveWorkspaceSpecific();
                                        SaveAndExit();
                                        break;
                                    default:
                                        break;
                                }
                                //objInit.objSettingForm_.strStartUpWorkSapce_

                                //if (Properties.Settings.Default.IsStartupWorkspacePrevious == true)
                                //{
                                //    SaveWorkspaceB4Exit();
                                //    SaveAndExit();
                                //}
                                //if (Properties.Settings.Default.IsStartupWorkspaceSpecific == true)
                                //{
                                //    SaveWorkspaceSpecific();
                                //    SaveAndExit();
                                //}
                                OrderManager.getOrderManager().Disconnect();
                                QuoteManager.getQuoteManager().Disconnect();
                                uctlHistoryDataManager.GetHistoryMgrInstance().StopAllThreads();
                                PlugInManager.PM_Instance.Plugins.ClosePlugins();
                                Process.GetCurrentProcess().Kill();
                                Environment.Exit(1);
                            }
                            catch (Exception ex)
                            {
                                Logger.LogEx(ex, "frmMainGTS", "frmMainGTS_FormClosing");
                                OrderManager.getOrderManager().Disconnect();
                                QuoteManager.getQuoteManager().Disconnect();
                                uctlHistoryDataManager.GetHistoryMgrInstance().StopAllThreads();
                                PlugInManager.PM_Instance.Plugins.ClosePlugins();
                                Process.GetCurrentProcess().Kill();
                                Environment.Exit(1);
                            }
                        }
                        break;
                    case DialogResult.No:
                        OrderManager.getOrderManager().Disconnect();
                        QuoteManager.getQuoteManager().Disconnect();
                        uctlHistoryDataManager.GetHistoryMgrInstance().StopAllThreads();
                        PlugInManager.PM_Instance.Plugins.ClosePlugins();
                        Process.GetCurrentProcess().Kill();
                        Environment.Exit(1);
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;

                    default:
                        break;
                }
            }

            else
            {
                DialogResult result = clsGlobal.ShowMessageBox("Are you sure want to Exit");
                switch (result)
                {
                    case DialogResult.Yes:
                        {
                            try
                            {
                                LastUsedInitializer.GetReference().SaveLastUsedSettings();
                                switch (objInit.objSettingForm_.strStartUpWorkSapce_)
                                {
                                    case "Previous":
                                        SaveWorkspaceB4Exit();
                                        SaveAndExit();
                                        break;
                                    case "Specific":
                                        SaveWorkspaceSpecific();
                                        SaveAndExit();
                                        break;
                                    case "None":
                                        SaveAndExit();
                                        break;
                                    default:
                                        break;
                                }
                                //if (Properties.Settings.Default.IsStartupWorkspacePrevious == true)
                                //{
                                //    SaveWorkspaceAutomatic();
                                //    SaveAndExit();
                                //}
                                //else if (Properties.Settings.Default.IsStartupWorkspaceSpecific == true)
                                //{
                                //    SaveWorkspaceSpecific();
                                //    SaveAndExit();
                                //}
                                //else if (Properties.Settings.Default.IsStartupWorkspaceNone == true)
                                //{
                                //    //SaveWorkspaceAutomatic();
                                //    SaveAndExit();

                                //}
                                OrderManager.getOrderManager().Disconnect();
                                QuoteManager.getQuoteManager().Disconnect();
                                uctlHistoryDataManager.GetHistoryMgrInstance().StopAllThreads();
                                PlugInManager.PM_Instance.Plugins.ClosePlugins();
                                Process.GetCurrentProcess().Kill();
                                Environment.Exit(1);
                            }
                            catch (Exception ex)
                            {
                                Logger.LogEx(ex, "frmMainGTS", "frmMainGTS_FormClosing");
                                OrderManager.getOrderManager().Disconnect();
                                QuoteManager.getQuoteManager().Disconnect();
                                uctlHistoryDataManager.GetHistoryMgrInstance().StopAllThreads();
                                PlugInManager.PM_Instance.Plugins.ClosePlugins();
                                Process.GetCurrentProcess().Kill();
                                Environment.Exit(1);
                            }
                        }
                        break;
                    case DialogResult.No:
                        e.Cancel = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private void SaveWorkspaceB4Exit()
        {
            NDockingFrameworkState state = new NDockingFrameworkState(m_DockManager);
            state.Format = Nevron.Serialization.PersistencyFormat.Binary;
            string CMDBarPath = string.Empty;
            string WrkSpacePath = string.Empty;
            getToolbarSettingPath(out WrkSpacePath, out CMDBarPath);
            SaveCommandBarSettings(CMDBarPath);
            state.Save(WrkSpacePath);
        }
        public static string FileName = string.Empty;
        public static string FileNameDefault = string.Empty;

        private void SaveWorkspaceAutomatic()
        {
            NDockingFrameworkState state = new NDockingFrameworkState(m_DockManager);
            //state.ResolveDocumentClient += new ResolveClientEventHandler(state_ResolveDocumentClient);
            state.Format = Nevron.Serialization.PersistencyFormat.Binary;
            state.StateSaved += new EventHandler(state_StateSaved);
            string str = Properties.Settings.Default.SaveWorkspacePath;
            SaveCommandBarSettings();
            FileNameDefault = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + "GalaxyTradestation\\Workspace\\GalaxyWorkspacePrevious.dfb";
            Properties.Settings.Default.SaveWorkspacePath = FileNameDefault;
            Properties.Settings.Default.Save();
            state.Save(Properties.Settings.Default.SaveWorkspacePath);
            //state.ResolveDocumentClient -= new ResolveClientEventHandler(state_ResolveDocumentClient);

        }

        void state_StateSaved(object sender, EventArgs e)
        {
            
        }
        private void SaveWorkspaceSpecific()
        {
            NDockingFrameworkState state = new NDockingFrameworkState(m_DockManager);
            state.Format = Nevron.Serialization.PersistencyFormat.Binary;
            string CMDBarPath = string.Empty;
            string WrkSpacePath = string.Empty;
            getToolbarSettingPath(out WrkSpacePath, out CMDBarPath);
            state.Save(WrkSpacePath);
            SaveCommandBarSettings(CMDBarPath);

        }

        string SettingFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + "GalaxyTradestation\\Workspace\\Galaxy.Settings";
        private void SaveAndExit()
        {
            DisposeAllThread();

            AlertManager.Getreference().SaveAlerts();         
            Initializer.GetReference().SaveSettingFormData(SettingFile);
            OrderManager.getOrderManager().saveAccountDetails();
            Properties.Settings.Default.setLanguageCulture = Thread.CurrentThread.CurrentUICulture;
            Properties.Settings.Default.setOrderForm_rdo = true;
            Properties.Settings.Default.setMarketOrder_rdo = false;
            Properties.Settings.Default.Save();
                       
        }
        private void DisposeAllThread()
        {
            if (timeRefresh != null)
            {
                timeRefresh.Enabled = false;
                timeRefresh.Dispose();
            }
            timeRefresh = null;

        }

        public void SetStdQuoteStatus(CheckedListBox.CheckedItemCollection data)
        {
            foreach (NCommand cmd in nStdQuote.Commands)
            {
                foreach (string symbol in data)
                {
                    if (cmd.Properties.Text == symbol)
                        cmd.Checked = true;
                }
            }
        }
        public void CheckQuotePair(bool status, string symbol)
        {
            //if (status == false)                                commented on 5july
            //{
            //    if (!symb_forexPair.Contains(symbol))
            //    {
            //        symb_forexPair.Add(symbol);
            //        //mnuViewForexinPanel();
            //        ctlForexPair pair = new ctlForexPair(symbol, this)
            //        {
            //            Parent = this.flowLayoutForexPair,
            //            Visible = true
            //        };
            //        pair.EnableControls(true);
            //        pair.Dock = DockStyle.None;
            //        pair.AutoSize = true;
            //        if (!QuoteSymbol.ContainsValue(pair.lblPairName.Text))
            //        {
            //            QuoteSymbol.Add(pair, pair.lblPairName.Text);
            //            flowLayoutForexPair.Controls.Add(pair);
            //            pair.Show();
            //        }
            //        status = true;
            //        ShowSettingColor(pair);
            //        pair.eventFPairSymbol += new dlgt_UpdateForexPairSymbolHandler(pair_eventFPairSymbol);
            //    }
            //    else
            //    {
            //        ShowForexPairInPanel(symbol);
            //        status = true;
            //    }

            //}
            //else
            //{
            //    status = false;
            //    HideForexPairInPanel(symbol);
            //}
        }

        private void LoadSelectedChartFromToolbar_Click(object sender, CommandEventArgs e)
        {
            objToolFunction.ShowSpecificChart(e.Command.ToString());
        }
        private void QuotePanel_Activated(object sender, PanelEventArgs e)
        {
            mnuViewForex.Checked = true;
            mnuLevel2.Checked = false;
            mnuMarketWatch.Checked = false;
            nStdLevel2.Checked = false;
            Level2Panel.Hide();
            this.uctlForex_.Show();
        }
        private void SymbolPanel_Activated(object sender, PanelEventArgs e)
        {
            mnuLevel2.Checked = false;
            nStdLevel2.Checked = false;
            mnuMarketWatch.Checked = true;
            mnuViewForex.Checked = false;
        }

        public enum ControlsName
        {
            nStatusBarBalancePanel,
            nStatusBarEquityPanel,
            nStatusBarUsedMarginPanel,
            nStatusBarAvailableMarginPanel,
            nStatusBarMarginLevelPanel,
            nStatusBarOpenPLPanel,
            nStatusBarDailyClosedPanel,
            nstatusbarLeverage,
            nStatusBarDailyGainsPanel,
            nStatusBarUsableCapitalPanel,
            nStatusBarLiquidation
        }
        public delegate void DelStatusBarTextHandler(ControlsName controlName, string value);
        public void SetControlTextStatus(ControlsName controlName, string value)
        {
            switch (controlName)
            {
                case ControlsName.nStatusBarBalancePanel: nStatusBarBalancePanel.Text = "Balance : " + value.ToString();
                    break;
                case ControlsName.nStatusBarEquityPanel: nStatusBarEquityPanel.Text = "Equity :" + value.ToString();
                    break;
                case ControlsName.nStatusBarUsedMarginPanel: nStatusBarUsedMarginPanel.Text = "Used Margin :" + value.ToString();
                    break;
                case ControlsName.nStatusBarAvailableMarginPanel: nStatusBarAvailableMarginPanel.Text = "Available Margin :" + value.ToString();
                    break;
                case ControlsName.nStatusBarMarginLevelPanel: nStatusBarMarginLevelPanel.Text = "Margin Level :" + value.ToString();
                    break;
                case ControlsName.nStatusBarOpenPLPanel: nStatusBarOpenPLPanel.Text = "Open P/L :" + value.ToString();
                    break;
                case ControlsName.nStatusBarDailyClosedPanel: nStatusBarDailyClosedPanel.Text = "Daily Closed P/L :" + value.ToString();
                    break;
                case ControlsName.nstatusbarLeverage: nstatusbarLeverage.Text = "Leverage: " + value.ToString();
                    break;
                case ControlsName.nStatusBarDailyGainsPanel: nStatusBarDailyGainsPanel.Text = "Daily Gain : " + value.ToString();
                    break;
                case ControlsName.nStatusBarUsableCapitalPanel: nStatusBarUsableCapitalPanel.Text = "Usable Capital : " + "5%";// + value.ToString();
                    break;
                case ControlsName.nStatusBarLiquidation: nStatusBarLiquidation.Text = "Liquidation Margin : " + "0";// + value.ToString();
                    break;
                default:
                    break;

            }
        }

        public void CheckLanguage(string lname)
        {
            UncheckAllLanguageStatus();
            foreach (NCommand cmd in mnuLanguage.Commands)
            {
                if (cmd.Properties.Text.Equals(lname))
                {
                    cmd.Checked = true;
                }
            }
        }

        public void UncheckAllLanguageStatus()
        {
            try
            {
                submnuEnglish.Checked = false;
                submnuVietnamese.Checked = false;
                submnuSpanish.Checked = false;
                submnuHindi.Checked = false;
                submnuChienese.Checked = false;
                submnuRussian.Checked = false;
                submnuArabic.Checked = false;
                submnuFrnch.Checked = false;
                submnuJapnese.Checked = false;
                submnuTurkish.Checked = false;
                submnuportuguese.Checked = false;
                submnuGerman.Checked = false;
                submnugreek.Checked = false;
                submnuBulgarian.Checked = false;
                submnuDutch.Checked = false;
                submnuHebrew.Checked = false;
                submnuHungarian.Checked = false;
                submnuCzech.Checked = false;
                submnuEstorian.Checked = false;
                submnuIndonesian.Checked = false;
                submnuLithuanian.Checked = false;
                submnuSerbian.Checked = false;
                submnuKorean.Checked = false;
                submnuItalian.Checked = false;
                submnuMalay.Checked = false;
                submnuChineseT.Checked = false;
                submnuSlovak.Checked = false;
                submnuThai.Checked = false;
            }
            catch (Exception ex)
            {
                ServerLog.Write("frmMainGTS::UncheckAllLanguageStatus" + ex.ToString() + ex.StackTrace, true);
                Logger.LogEx(ex, "frmMainGTS", "UncheckAllLanguageStatus()");
            }


        }

        private void frmMainGTS_DragEnter(object sender, DragEventArgs e)
        {
            if (m_ActiveDocument.Client.Name == "crystalReportViewer1")
                return;
            e.Effect = DragDropEffects.Copy;
            string data = e.Data.GetData(typeof(System.String)).ToString();
            m_ActiveChart.ChangeChartData(data);
        }

        private void QuotePanel_Closed(object sender, PanelEventArgs e)
        {
            mnuViewForex.Checked = false;
        }

        private void SymbolPanel_Closed(object sender, PanelEventArgs e)
        {
            mnuMarketWatch.Checked = false;
            nStdMktWatch.Checked = false;
        }

        private void navigatorPanel_Closed(object sender, PanelEventArgs e)
        {
            mnuNavigator.Checked = false;
            nStdNavigtor.Checked = false;
        }

        private void TerminalPanel_Closed(object sender, PanelEventArgs e)
        {
            mnuTerminal.Checked = false;
            nStdTerminal.Checked = false;
        }

        private void SymbolPanel_Closing(object sender, PanelCancelEventArgs e)
        {
            mnuMarketWatch.Checked = false;
            nStdMktWatch.Checked = false;
        }

        private void frmMainGTS_Resize(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            if (Initializer.GetReference().objSettingForm_.bMinimizeToTray_)
            {
                if (FormWindowState.Minimized == WindowState)
                {
                    Hide();
                    notifyIcon1.Visible = true;
                }
                else
                {
                    notifyIcon1.Visible = false;
                }
            }
            else
            {
                // this.WindowState = FormWindowState.Maximized;

            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Maximized;
        }

        private void Level2Panel_Activated(object sender, PanelEventArgs e)
        {
            QuotePanel.Hide();
            SymbolPanel.Hide();
            //uctlLevelInstance.Show();
            //uctlLevelInstance.ndgLevel.Show();         
        }

        public void SetChartsStyle()
        {
            m_DockManager.DocumentStyle.DocumentViewStyle = DocumentViewStyle.MdiStandard;
        }

        private void nStdQuote_Popup(object sender, CommandEventArgs e)
        {
            List<string> data = objConstructorFunc.SetForexPairinDataSymbol();

            foreach (NCommand cmd in nStdQuote.Commands)
            {
                cmd.Checked = false;
            }
            foreach (NCommand cmd in nStdQuote.Commands)
            {
                if (data.Contains(cmd.Properties.Text))
                {
                    cmd.Checked = true;
                }
            }
        }

        public struct ReversePosition
        {
            public string Account;
            //  public string ClOrdID;
            public string Side;
            public string OrderQuantity;
            public string Symbol;
        }

        public void SendDirectNewOrderAtMarketPrice(string Quantity, string Symbol, string Price, string TimeInForce, string ClientOrderID, string AccountID, bool isSell)
        {
        }

        private void SetDailyClosedProfit()
        {
            //double ProfitClose = 0;
            //try
            //{
            //    for (int RowIndex = 0; RowIndex < dgAccountGrid.Rows.Count; RowIndex++)
            //    {
            //        if (dgAccountGrid.Rows[RowIndex].Cells["colDate"].Value == null)
            //        {
            //            continue;
            //        }
            //        string DateString = dgAccountGrid.Rows[RowIndex].Cells["colDate"].Value.ToString();
            //        DateTime objDate = Convert.ToDateTime(DateString);
            //        DateTime dateObj = objDate.Date;
            //        DateTime dateobjNow = DateTime.Now.Date;
            //        if (dateObj.ToString() == dateobjNow.ToString())
            //        {
            //            ProfitClose += Convert.ToDouble(dgAccountGrid.Rows[RowIndex].Cells["colProfit"].Value.ToString());
            //        }
            //    }
            //    SetDailyClosedPL(ProfitClose);

            // SetDailyGain(Math.Round((ProfitClose) / Convert.ToDouble(Account_Bal) * 100, 2));
            //}
            //catch (Exception ex)
            //{
            //    //  MessageBox.Show(ex.Message);
            //}
        }

        private double GetDailyProfitCloseProfit()
        {
            string strDailyClose = nStatusBarDailyClosedPanel.Text;
            string[] arr = strDailyClose.Split(':');
            if (string.IsNullOrEmpty(arr[1]))
            {
                arr[1] = "0";
            }
            return Convert.ToDouble(arr[1].Trim());

        }

        private void SetDailyGainAfterLoad()
        {
            SetDailyGain(Math.Round((GetDailyProfitCloseProfit()) / Convert.ToDouble(Account_Bal) * 100, 2));
        }
        public void SetDailyGain(double dailyGain)
        {
            if (InvokeRequired)
            {
                DelStatusBarTextHandler delStatusBarText = new DelStatusBarTextHandler(this.SetControlTextStatus);
                delStatusBarText(ControlsName.nStatusBarDailyGainsPanel, dailyGain.ToString());
            }
            else
            {
                if (double.IsNaN(dailyGain))
                    dailyGain = 0;
                nStatusBarDailyGainsPanel.Text = "Daily Gain: " + dailyGain.ToString();
                nStatusBarDailyGainsPanel.ToolTipText = "Daily Gain: " + dailyGain.ToString();
                nStatusBarDailyGainsPanel.AutoSize = StatusBarPanelAutoSize.Spring;
                nStatusBar.Invalidate();
            }
        }
       
        public void SetBalance(double balance)
        {
            if (InvokeRequired)
            {
                DelStatusBarTextHandler delStatusBarText = new DelStatusBarTextHandler(this.SetControlTextStatus);
                delStatusBarText(ControlsName.nStatusBarBalancePanel, balance.ToString());
            }
            else
            {
                nStatusBarBalancePanel.Text = "Balance: " + balance.ToString();
                nStatusBarBalancePanel.ToolTipText = "Balance: " + balance.ToString();
                nStatusBarBalancePanel.AutoSize = StatusBarPanelAutoSize.Spring;
                //SetLeverage("100:1");
                nStatusBar.Invalidate();
            }
        }
        public void SetEquity(double equity)//, double openpnl)
        {

            if (InvokeRequired)
            {
                DelStatusBarTextHandler delStatusBarText = new DelStatusBarTextHandler(this.SetControlTextStatus);
                delStatusBarText(ControlsName.nStatusBarEquityPanel, equity.ToString());
            }
            else
            {
                nStatusBarEquityPanel.Text = "Equity: " + equity.ToString();
                nStatusBarEquityPanel.ToolTipText = "Equity: " + equity.ToString();
                nStatusBarEquityPanel.AutoSize = StatusBarPanelAutoSize.Spring;
                nStatusBar.Invalidate();
            }
        }

        //  public delegate void Dlg_SetUsedMargin(double usedmargin);
        private void SetUsedMargin(double usedmargin)
        {

            if (InvokeRequired)
            {

                //  this.BeginInvoke(new Dlg_SetUsedMargin(SetUsedMargin), usedmargin);
                DelStatusBarTextHandler delStatusBarText = new DelStatusBarTextHandler(this.SetControlTextStatus);
                delStatusBarText(ControlsName.nStatusBarUsedMarginPanel, usedmargin.ToString());
            }
            else
            {
                nStatusBarUsedMarginPanel.ToolTipText = "Used Margin: " + usedmargin.ToString();
                nStatusBarUsedMarginPanel.Text = "Used Margin: " + usedmargin.ToString();
                nStatusBarUsedMarginPanel.AutoSize = StatusBarPanelAutoSize.Spring;
                nStatusBar.Invalidate();
            }

        }
        public void SetAvailableMargin(double availableMargin)
        {
            if (InvokeRequired)
            {
                DelStatusBarTextHandler delStatusBarText = new DelStatusBarTextHandler(this.SetControlTextStatus);
                delStatusBarText(ControlsName.nStatusBarAvailableMarginPanel, availableMargin.ToString());
            }
            else
            {
                nStatusBarAvailableMarginPanel.ToolTipText = "Avail Margin: " + availableMargin.ToString();
                nStatusBarAvailableMarginPanel.Text = "Avail Margin: " + availableMargin.ToString();
                nStatusBarAvailableMarginPanel.AutoSize = StatusBarPanelAutoSize.Spring;
                nStatusBar.Invalidate();
            }
        }
        public void SetMarginLevel(double marginLevel)
        {
            if (InvokeRequired)
            {
                DelStatusBarTextHandler delStatusBarText = new DelStatusBarTextHandler(this.SetControlTextStatus);
                delStatusBarText(ControlsName.nStatusBarMarginLevelPanel, marginLevel.ToString() + "%");
            }
            else
            {
                nStatusBarMarginLevelPanel.Text = "Margin Level: " + Math.Round(marginLevel, 2).ToString() + "%";
                nStatusBarMarginLevelPanel.ToolTipText = "Margin: " + Math.Round(marginLevel, 2).ToString() + "%";
                nStatusBarMarginLevelPanel.AutoSize = StatusBarPanelAutoSize.Spring;
                nStatusBar.Invalidate();
            }
        }
        private void SetOpenPL(double openPL)
        {

            if (InvokeRequired)
            {
                DelStatusBarTextHandler delStatusBarText = new DelStatusBarTextHandler(this.SetControlTextStatus);
                delStatusBarText(ControlsName.nStatusBarOpenPLPanel, openPL.ToString());
            }
            else
            {
                nStatusBarOpenPLPanel.Text = "Open P/L: " + openPL.ToString();
                nStatusBarOpenPLPanel.ToolTipText = "Open P/L: " + openPL.ToString();
                nStatusBarOpenPLPanel.AutoSize = StatusBarPanelAutoSize.Spring;
                nStatusBar.Invalidate();
            }
        }
        //  public delegate void Dlg_setLiquidationMargin(string p);
        private void SetLiqudationMargin(double p)
        {
            if (InvokeRequired)
            {
                // this.BeginInvoke(new Dlg_setLiquidationMargin(SetLiqudationMargin), p);
                DelStatusBarTextHandler delStatusBarText = new DelStatusBarTextHandler(this.SetControlTextStatus);
                delStatusBarText(ControlsName.nStatusBarLiquidation, p.ToString());
            }
            else
            {
                nStatusBarLiquidation.Text = "Liquidation Margin:" + p.ToString();
                nStatusBarLiquidation.ToolTipText = "Liquidation Margin:" + p.ToString();
                nStatusBarLiquidation.AutoSize = StatusBarPanelAutoSize.Spring;
                nStatusBar.Invalidate();
            }
        }

        //  public delegate void Dlg_SetDailyClosedPL(double closedPL);
        static double ClosedPnL = 0;
        public void SetDailyClosedPL(double closedPL)
        {
            if (InvokeRequired)
            {
                // this.BeginInvoke(new Dlg_SetDailyClosedPL(SetDailyClosedPL), closedPL);    
                DelStatusBarTextHandler delStatusBarText = new DelStatusBarTextHandler(this.SetControlTextStatus);
                delStatusBarText(ControlsName.nStatusBarDailyClosedPanel, closedPL.ToString());
            }
            else
            {
                ClosedPnL += closedPL;
                nStatusBarDailyClosedPanel.Text = "Daily Closed P/L: " + ClosedPnL.ToString();
                nStatusBarDailyClosedPanel.ToolTipText = "Daily Closed P/L: " + ClosedPnL.ToString();
                nStatusBarDailyClosedPanel.AutoSize = StatusBarPanelAutoSize.Spring;
                nStatusBar.Invalidate();
            }
        }
        public void SetLeverage(string leverage)
        {
            if (InvokeRequired)
            {
                DelStatusBarTextHandler delStatusBarText = new DelStatusBarTextHandler(this.SetControlTextStatus);
                delStatusBarText(ControlsName.nstatusbarLeverage, leverage.ToString());
            }
            else
            {
                nstatusbarLeverage.Text = "Leverage: " + leverage.ToString();
                nstatusbarLeverage.ToolTipText = "Leverage: " + leverage.ToString();
                nstatusbarLeverage.AutoSize = StatusBarPanelAutoSize.Spring;
                nStatusBar.Invalidate();
            }
        }
        // public delegate void Dlg_SetDailyGain(double dailyGain);
        private void SetUsableCapital(double usableCapital)
        {
            if (InvokeRequired)
            {
                DelStatusBarTextHandler delStatusBarText = new DelStatusBarTextHandler(this.SetControlTextStatus);
                delStatusBarText(ControlsName.nStatusBarBalancePanel, usableCapital.ToString());
            }
            else
            {
                nStatusBarUsableCapitalPanel.Text = "Usable Capital: " + usableCapital.ToString();
                nStatusBarUsableCapitalPanel.ToolTipText = "Usable Capital: " + usableCapital.ToString();
                nStatusBarUsableCapitalPanel.AutoSize = StatusBarPanelAutoSize.Spring;
                nStatusBar.Invalidate();
            }
        }
        public bool CheckAvailableAmount(double _equity, double _margin)
        {
            if (_equity > _margin)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool CheckNewFivePercentRule(double Margin)
        {
            try
            {
                double Check5perofBalance = (Convert.ToDouble(Account_Bal) * 5) / 100;
                if (Check5perofBalance > Margin)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                Logger.LogEx(ex, "frmMainGTS", "CheckNewFivePercentRule(double Margin)");
            }
            return true;
        }
        public double GetBlanceAfter5Percent()
        {
            double balance = (Convert.ToDouble(Account_Bal) * 5) / 100;
            return balance;
        }
        public void SetChartToolBarChartShift(bool value)
        {
            nChart_Shift.Checked = value;
        }
        private void nChart_Shift_Click(object sender, CommandEventArgs e)
        {
            if (m_ActiveChart == null)
                return;
            if (cp.chartShift == true)
            {
                cp.chartShift = false;
                m_ActiveChart.SetChartProperties(cp);
                nChart_Shift.Checked = true;
            }
            else
            {
                cp.chartShift = true;
                m_ActiveChart.SetChartProperties(cp);
                nChart_Shift.Checked = false;
            }
        }

        public NUIDocument GetDocumentForSymbol(string p)
        {
                foreach (NUIDocument doc in m_DockManager.DocumentManager.Documents)
                {
                    if (doc.Text == p)
                    {
                        //doc.Client = frmMainGTS.GetChart();
                        m_DockManager.DocumentManager.ActiveDocument = doc;
                        return doc;
                    }
                }
                return null;
        }
        public void SetServiceData(bool LoginStatus)
        {
            //DataService.AccountDetails ADInstance = DataService.GetReference().GetAccountDetails();
            //test(ADInstance.Balance_.ToString(), ADInstance.Leverage_.ToString());
            //AccountID_ = ADInstance.AccID_;
            //AccountType_ = ADInstance.AccountType_;
            //CreationDate_ = ADInstance.CreationDate_;
            //UserName_ = ADInstance.Name_;
            //Password_ = ADInstance.Password_;
            ////uctlNavigatorInstance.AddAccountToNavigator(ADInstance.AccID_, ADInstance.LoginID_, ADInstance.Name_, true);          

            //setTradeAccountGridsVisibilty(true);
            //SetText_UI_ACSummary();
        }


        private void state_ResolveDocumentClient(object sender, DocumentEventArgs e)
        {
            string Title = string.Empty;
            ctlChart chart = GetChart(e.Document);
            frmMainGTS.GetReference().m_ActiveChart = chart;
            e.Document.Client = chart;
        }
        public static ctlChart GetChart(NUIDocument Doc)
        {
            ctlChart chart = new ctlChart(Doc);
            //chart.SetChartAttributes(Title);            
           // chart.Dock = DockStyle.Fill;
            return chart;
        }

        void getToolbarSettingPath(out string WorkSpacePath, out string ToolBarPath)
        {
            WorkSpacePath = string.Empty;
            ToolBarPath = string.Empty;
            
            switch (Initializer.GetReference().objSettingForm_.strStartUpWorkSapce_)
            {
                case "Previous":
                    WorkSpacePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + "GalaxyTradestation\\Workspace\\GalaxyWorkspacePrevious.dfb";
                    break;
                case "Specific":
                    WorkSpacePath = Initializer.GetReference().objSettingForm_.strStartUpWorkspacePath_;
                    break;
                default:
                    return;
                    break;
            }

            string folder = WorkSpacePath.Substring(0, WorkSpacePath.LastIndexOf("\\"));
            FileInfo objfile = new FileInfo(WorkSpacePath);
            FileName = objfile.Name.Remove(objfile.Name.IndexOf("."), 4);
            ToolBarPath = folder + "\\" + FileName + ".cbs";
           
        }

        private void SaveCommandBarSettings(string path)
        {
            if (!System.IO.Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + "GalaxyTradestation\\Workspace\\"))//\\Alerts.alert
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + "GalaxyTradestation\\Workspace\\");
            }
            m_state.Save(path);
        }

        private void SaveCommandBarSettings()
        {
            if (!System.IO.Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + "GalaxyTradestation\\Workspace\\"))//\\Alerts.alert
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + "GalaxyTradestation\\Workspace\\");
            }

            if (Properties.Settings.Default.IsStartupWorkspacePrevious == true)
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + "GalaxyTradestation\\Workspace\\GalaxyWorkspacePrevious.cbs";
                m_state.Save(path);
            }
            else if (Properties.Settings.Default.IsStartupWorkspaceSpecific == true)
            {
                string fullPath = Properties.Settings.Default.LoadWorkspacePath;
                string folder = fullPath.Substring(0, fullPath.LastIndexOf("\\"));
                FileInfo objfile = new FileInfo(fullPath);
                FileName = objfile.Name.Remove(objfile.Name.IndexOf("."), 4);
                string path = folder + "\\" + FileName + ".cbs";
                m_state.Save(path);
            }
            else if (Properties.Settings.Default.IsStartupWorkspaceNone == true)
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + "GalaxyTradestation\\Workspace\\ResetWorkspace.cbs";
                m_state.Save(path);
            }

        }

        private void LoadCommandBarSettings(string path)
        {
            m_state.Load(path);
            //if (Properties.Settings.Default.IsStartupWorkspacePrevious == true)
            //{
            //    string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + "GalaxyTradestation\\Workspace\\GalaxyWorkspacePrevious.cbs";
            //    m_state.Load(path);
            //}
            //else if (Properties.Settings.Default.IsStartupWorkspaceSpecific == true)
            //{
            //    string fullPath = Properties.Settings.Default.LoadWorkspacePath;
            //    string folder = fullPath.Substring(0, fullPath.LastIndexOf("\\"));
            //    FileInfo objfile = new FileInfo(fullPath);
            //    FileName = objfile.Name.Remove(objfile.Name.IndexOf("."), 4);
            //    string path = folder + "\\" + FileName + ".cbs";
            //    m_state.Load(path);
            //}
        }
        private void LoadCommandBarSettings()
        {
            if (Properties.Settings.Default.IsStartupWorkspacePrevious == true)
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + "GalaxyTradestation\\Workspace\\GalaxyWorkspacePrevious.cbs";
                m_state.Load(path);
            }
            else if (Properties.Settings.Default.IsStartupWorkspaceSpecific == true)
            {
                string fullPath = Properties.Settings.Default.LoadWorkspacePath;
                string folder = fullPath.Substring(0, fullPath.LastIndexOf("\\"));
                FileInfo objfile = new FileInfo(fullPath);
                FileName = objfile.Name.Remove(objfile.Name.IndexOf("."), 4);
                string path = folder + "\\" + FileName + ".cbs";
                m_state.Load(path);
            }
        }

        private void m_CmdBarsManager_CommandClicked(object sender, CommandEventArgs e)
        {           
            NDockingFrameworkState state = new NDockingFrameworkState(m_DockManager);
            state.ResolveDocumentClient += state_ResolveDocumentClient;
            state.Format = Nevron.Serialization.PersistencyFormat.Binary;

            string command = e.Command.ToString();//objFunction.GetCommand(e.Command.ToString());
            #region commented Code
            //            switch (command)
            //            {
            //                #region File Menu

            //                case "Load_WorkSpace":
            //                    state.Load();
            //                    LoadCommandBarSettings();
            //                    state.ResolveDocumentClient -= new ResolveClientEventHandler(state_ResolveDocumentClient);
            //                    break;

            //                case "Load Default Workspace":
            //                    string pathWorkspace = Application.StartupPath + @"\Resources\ResetWorkspace.dfb";
            //                    string pathToolbar = Application.StartupPath + @"\Resources\ResetWorkspace.cbs";      

            //                    if (File.Exists(pathWorkspace))
            //                    {
            //                        state.Load(pathWorkspace);
            //                    }
            //                    else 
            //                    {
            //                        clsGlobal.DisplayMessage("Sorry ! File not Found to Reset Workspace.....!");
            //                    }
            //                    if (File.Exists(pathToolbar))
            //                    {
            //                        m_state.Load(pathToolbar);
            //                    }
            //                    else
            //                    {
            //                        clsGlobal.DisplayMessage("Sorry ! File not Found to Reset Toolbars.....!");
            //                    }
            //                    break;
            //                case "Load_Chart_from_Disk":
            //                    objChartFunction.LoadChartFileFromDisk();
            //                    break;
            //                case "Import_Excel_Selection":
            //                    break;
            //                case "Save_Workspace":

            //                    if (Properties.Settings.Default.IsStartupWorkspaceSpecific == true)
            //                    {
            //                        state.Save(Properties.Settings.Default.LoadWorkspacePath);
            //                        SaveCommandBarSettings();
            //                        state.ResolveDocumentClient -= new ResolveClientEventHandler(state_ResolveDocumentClient);
            //                    }
            //                    if (Properties.Settings.Default.IsStartupWorkspacePrevious == true)
            //                    {
            //                        #region Reset Settings
            //                        //state.Save(@"C:\Documents and Settings\user\My Documents\GalaxyTradestation\Workspace\ResetWorkspace.dfb");//This line is used to generate default workspace settings
            //                        //state.Save(@"C:\Documents and Settings\user\My Documents\GalaxyTradestation\Workspace\ResetToolbar.cbs");//This line is used to generate default Toolbar settings
            //                        #endregion
            //                        SaveCommandBarSettings();
            //                        state.Save(clsGlobal.SaveWorkspacePath);
            //                        state.ResolveDocumentClient -= new ResolveClientEventHandler(state_ResolveDocumentClient);
            //                    }
            //                    if (Properties.Settings.Default.IsStartupWorkspaceNone == true)
            //                    {
            //                        SaveCommandBarSettings();
            //                        state.Save(clsGlobal.SaveWorkspacePath);
            //                        state.ResolveDocumentClient -= new ResolveClientEventHandler(state_ResolveDocumentClient);
            //                    }

            //                        break;

            //                case "SaveWorkspace As":
            //                     state.Save();
            //                     SaveCommandBarSettings();
            //                     state.ResolveDocumentClient -= new ResolveClientEventHandler(state_ResolveDocumentClient);
            //                    break;
            //                case "Save Chart":
            //                    try
            //                    {
            //                        if (m_ActiveChart == null) return;
            //                        string title = objChartFunction.GetChartTitle(m_ActiveChart.m_Symbol, m_ActiveChart.m_Periodicity, m_ActiveChart.m_BarSize);
            //                        m_ActiveChart.SaveChart(title);
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ServerLog.Write("frmMainGTS::mnuFileSaveChart_Click" + ex.ToString() + ex.StackTrace, true);
            //                    }
            //                    break;

            //                case "Save Chart as Image":
            //                    try
            //                    {
            //                        if (m_ActiveChart == null) return;
            //                        m_ActiveChart.SaveChartImage();
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ServerLog.Write("frmMainGTS::mnuFileSaveImage_Click" + ex.ToString() + ex.StackTrace, true);
            //                    }
            //                    break;

            //                case "Save Chart as Template":
            //                    try
            //                    {
            //                        if (m_ActiveChart == null) return;
            //                        m_ActiveChart.StockChartX1.SaveGeneralTemplate(SaveDialog("Chart Template|*.sct"));
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ServerLog.Write("frmMainGTS::mnuFileSaveTemplate_Click" + ex.ToString() + ex.StackTrace, true);
            //                    }
            //                    break;

            //                case "mnuFile_Login":
            //                    try
            //                    {
            //                        string str = frmMainGTS.GetReference().Text.Split(':')[0].Substring(0, frmMainGTS.GetReference().Text.Split(':')[0].Length - 1);
            //                        ActiveAccount account = OrderManager.getOrderManager().GetAccount(str);
            //                        frmDataLogin frmDL = new frmDataLogin(this, account.LoginID_, account.Password, true);
            //                        frmDL.ShowDialog();
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ServerLog.Write("frmMainGTS::mnuLogin_Click_1" + ex.ToString() + ex.StackTrace, true);
            //                    }
            //                    break;

            //                case "Logout":
            //                    this.Close();
            //                    break;

            //                case "mnuFile_Exit.text":
            //                    this.Close();
            //                    break;
            //#endregion

            //                #region View
            //                case "mnuView_Statusbar":
            //                    objToolFunction.StatusBarState();
            //                    break;
            //                case "mnuView_MarketWatch":
            //                    objToolFunction.MarketWatchState();
            //                    break;
            //                case "mnuView_DataWindow":
            //                    break;
            //                case "Navigator":
            //                    objToolFunction.NavigatorState();
            //                    break;
            //                case "mnuView_Terminal":
            //                    objToolFunction.TerminalState();
            //                    break;
            //                case "Strategy Tester":
            //                    break;
            //                case "mnuView_FullScreen":
            //                    this.WindowState = FormWindowState.Maximized;
            //                    break;
            //                case "Level2":
            //                    objToolFunction.Level2State();
            //                    break;
            //                case "mnuView_ForexScreen":
            //                    objToolFunction.ForexScreenState();
            //                    break;
            //                case "quote_Pair":
            //                    DataSymbol ds = new DataSymbol(this);
            //                    ds.ShowDialog();
            //                    break;
            //                case "Execution_View":
            //                    objToolFunction.ExecutionViewState();
            //                    break;
            //#endregion

            //                #region Tools Menu
            //                case "Connect_TD_Server":
            //                    break;
            //                case "Connect_Qt_Server":
            //                    break;
            //                case "Stock Scanner":
            //                    break;
            //                case "Alerts":
            //                    frmAlert objfrmAlert = new frmAlert(ALERT_MODE.CREATE, null);
            //                    objfrmAlert.ShowDialog();
            //                    break;
            //                case "mnuTools_CloseAllPositions":
            //                    break;
            //                case "mnuTools_CancelAllOrders":
            //                    break;
            //                case "Hedge All":
            //                    break;
            //                case "Galaxy Creator":
            //                    break;
            //                case "Forum":
            //                    break;
            //                case "mnuTools_NewOrder":
            //                    try
            //                    {
            //                        objOrderFunction.ShowOrderEntry();
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ServerLog.Write("frmMainGTS::mnuNewOrder_Click" + ex.ToString() + ex.StackTrace, true);
            //                    }
            //                    break;
            //#endregion

            //                #region Charts Menu 
            //                case "mnuFile_NewChart":

            //                    objChartFunction.CreateNewChart();
            //                    break;
            //                case "Zoom_In":
            //                    objChartFunction.ZoomIn();
            //                    break;
            //                case "Zoom_Out":
            //                    objChartFunction.ZoomOut();
            //                    break;
            //                case "Track_Cursor":
            //                    objChartFunction.TrackCursorDisplay();
            //                    break;
            //                case "Amount"://Volume
            //                    objChartFunction.VolumeDisplay();
            //                    break;
            //                case "Grid":
            //                    objChartFunction.GridDisplay();
            //                    break;
            //                case "Status":  //Position
            //                    //OrderEntryHandler.GetHandler().GetPositionInfo()
            //                    break;
            //                case "Orders":
            //                    //OrderEntryHandler.GetHandler().CFXAPIRequestOrders();
            //                    break;
            //                case "3D_Style":
            //                    objChartFunction.ThreeDStyle();
            //                    break;
            //#endregion

            //                #region Technical Analysis Menu

            //                case "Add Trading System":
            //                    break;
            //                case "Trading System List":
            //                    break;
            //                case "Create Trading System":
            //                    break;
            //                case "Add Visual Advisor":
            //                    break;
            //                case "Auto Trading":
            //                    break;
            //                case "Indicator_List":
            //                    objToolFunction.AddIndicatorList();
            //                    break;
            //                case "Create Indicator":
            //                    break;
            //#endregion

            //                #region Account Service Menu
            //                case "Transfer":
            //                    objToolFunction.TransferFund();
            //                    break;
            //                case "mnuAccountServices_DepositFund":
            //                    objToolFunction.DepositFund();
            //                    break;
            //                case "Withdraw_Fund":
            //                    objToolFunction.WithdrawFund();
            //                    break;
            //                case "mnuAccountServices_ChangeAddress":
            //                    GTS.Frm.frmChangeAddress frmAddress = new GTS.Frm.frmChangeAddress();
            //                    frmAddress.Show();
            //                    break;
            //                case "mnuAccountServices_ChangePassword":
            //                    GTS.Frm.frmChangePassword frmPassword = new GTS.Frm.frmChangePassword();
            //                    frmPassword.Show();
            //                    break;
            //                case "Edit Profile":
            //                    break;
            //                #endregion

            //                #region Reports Menu
            //                case "mnuReports_AccountHistory.text":
            //                    break;
            //                case "Account history":
            //                    break;
            //                case "AccountDetails":
            //                    break;
            //                case "Account Details":
            //                    break;
            //                case "mnuReports_BalanceReport.text":
            //                    break;
            //                case "mnuReport_ProfitLoss.text":
            //                    break;
            //                case "mnuReport_Deposit":
            //                    break;
            //                case "mnuReport_Withdrawal.text":
            //                    break;
            //                case "mnuReport_TradeReport.text":
            //                    break;
            //                #endregion

            //                #region Setting Menu
            //                case "Mac Skin":
            //                    objToolFunction.Mac();
            //                    break;
            //                case "vistaSkin":
            //                    objToolFunction.Vista();
            //                    break;
            //                case "Windows_2007_Skin":
            //                    objToolFunction.Office2007();
            //                    break;
            //                case "Office2007 Silver":
            //                    objToolFunction.Office2007Silver();
            //                    break;
            //                case "Reset Settings":
            //                    Initializer.GetReference().objSettingForm_.LoadDefault();
            //                    objFunction.ApplySetting();

            //                    break;
            //                case "Option":
            //                    objToolFunction.Option();
            //                    break;
            //                #endregion

            //                #region  Window Menu
            //                case "New Window":
            //                    break;
            //                case "mnuWindows_Cascade":
            //                    objToolFunction.Cascade();
            //                    break;
            //                case "mnuWindows_TileVertically":
            //                    VerticallyAction();
            //                    break;
            //                case "Arrange_Icons":
            //                    break;
            //                case "Tile Horizontally":
            //                    break;
            //                #endregion

            //                #region Help Menu
            //                case "Help & Contents":
            //                    objToolFunction.DisplayHelp();
            //                    break;
            //                case "User_Manual":
            //                    objToolFunction.HelpUserManual();
            //                    break;
            //                case "mnuHelp_HomePage.text":
            //                    break;
            //                case "mnuHelp_BugReport.text":
            //                    objToolFunction.BugReport();
            //                    break;
            //                case "mnuHelp_FeatureRequest":
            //                    objToolFunction.FeatureRequest();
            //                    break;
            //                case "mnuHelp_AskQuestion.text":
            //                    objToolFunction.AskQuestion();
            //                    break;
            //                case "Log_File":
            //                    break;
            //                case "Check_For_Update":
            //                    break;
            //                case "About_M4":
            //                    (new frmHelp()).ShowDialog();
            //                    break;
            //            #endregion

            //                #region Language (View Sub Menu)
            //                case "English":
            //                    try
            //                    {
            //                        lang_string = "en";
            //                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
            //                        objFunction.SetText();
            //                        objFunction.UncheckAllLanguageStatus();
            //                        CheckSelectedLanguage(submnuEnglish);
            //                        objFunction.SetLaguageInSettingForm("English");
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ServerLog.Write("frmMainGTS::submnuEnglish_Click" + ex.ToString() + ex.StackTrace, true);
            //                    }
            //                    break;
            //                case "Spanish":
            //                    try
            //                    {
            //                        lang_string = "es";
            //                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
            //                        objFunction.SetText();
            //                        objFunction.UncheckAllLanguageStatus();
            //                        CheckSelectedLanguage(submnuSpanish);
            //                        objFunction.SetLaguageInSettingForm("Spanish");
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ServerLog.Write("frmMainGTS::submnuSpanish_Click" + ex.ToString() + ex.StackTrace, true);
            //                    }
            //                    break;
            //                case "Vietnamese":
            //                    try
            //                    {
            //                        lang_string = "vi";
            //                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
            //                        objFunction.SetText();
            //                        objFunction.UncheckAllLanguageStatus();
            //                        CheckSelectedLanguage(submnuVietnamese);
            //                        objFunction.SetLaguageInSettingForm("Vietnamese");
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ServerLog.Write("frmMainGTS::submnuVietnamese_Click" + ex.ToString() + ex.StackTrace, true);
            //                    }
            //                    break;
            //                case "Chienese":
            //                    try
            //                    {
            //                        lang_string = "zh-CHS";
            //                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
            //                        objFunction.SetText();
            //                        objFunction.UncheckAllLanguageStatus();
            //                        CheckSelectedLanguage(submnuChienese);
            //                        objFunction.SetLaguageInSettingForm("Chienese");
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ServerLog.Write("frmMainGTS::submnuChienese_Click" + ex.ToString() + ex.StackTrace, true);
            //                    }
            //                    break;
            //                case "Hindi":
            //                    try
            //                    {
            //                        lang_string = "hi";
            //                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
            //                        objFunction.SetText();
            //                        objFunction.UncheckAllLanguageStatus();
            //                        CheckSelectedLanguage(submnuHindi);
            //                        objFunction.SetLaguageInSettingForm("Hindi");
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ServerLog.Write("frmMainGTS::submnuHindi_Click" + ex.ToString() + ex.StackTrace, true);
            //                    }
            //                    break;
            //                case "Russian":
            //                    try
            //                    {
            //                        lang_string = "ru";
            //                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
            //                        objFunction.SetText();
            //                        objFunction.UncheckAllLanguageStatus();
            //                        CheckSelectedLanguage(submnuRussian);
            //                        objFunction.SetLaguageInSettingForm("Russian");
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ServerLog.Write("frmMainGTS::submnuRussian_Click" + ex.ToString() + ex.StackTrace, true);
            //                    }
            //                    break;
            //                case "Arabic":
            //                    try
            //                    {
            //                        lang_string = "ar";
            //                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
            //                        objFunction.SetText();
            //                        objFunction.UncheckAllLanguageStatus();
            //                        CheckSelectedLanguage(submnuArabic);
            //                        objFunction.SetLaguageInSettingForm("Arabic");

            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ServerLog.Write("frmMainGTS::submnuArabic_Click" + ex.ToString() + ex.StackTrace, true);
            //                    }
            //                    break;
            //                case "French":
            //                    try
            //                    {
            //                        lang_string = "fr";
            //                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
            //                        objFunction.SetText();
            //                        objFunction.UncheckAllLanguageStatus();
            //                        CheckSelectedLanguage(submnuFrnch);
            //                        objFunction.SetLaguageInSettingForm("French");
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ServerLog.Write("frmMainGTS::submnuFrnch_Click" + ex.ToString() + ex.StackTrace, true);
            //                    }
            //                    break;
            //                case "Japanese":
            //                    try
            //                    {
            //                        lang_string = "ja";
            //                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
            //                        objFunction.SetText();
            //                        objFunction.UncheckAllLanguageStatus();
            //                        CheckSelectedLanguage(submnuJapnese);
            //                        objFunction.SetLaguageInSettingForm("Japanese");
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ServerLog.Write("frmMainGTS::submnuJapnese_Click" + ex.ToString() + ex.StackTrace, true);
            //                    }
            //                    break;
            //                case "Turkish":
            //                    try
            //                    {
            //                        lang_string = "tr-TR";
            //                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
            //                        objFunction.SetText();
            //                        objFunction.UncheckAllLanguageStatus();
            //                        CheckSelectedLanguage(submnuTurkish);
            //                        objFunction.SetLaguageInSettingForm("Turkish");
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ServerLog.Write("frmMainGTS::submnuTurkish_Click" + ex.ToString() + ex.StackTrace, true);
            //                    }
            //                    break;
            //                case "Portuguese":
            //                    try
            //                    {
            //                        lang_string = "pt";
            //                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
            //                        objFunction.SetText();
            //                        objFunction.UncheckAllLanguageStatus();
            //                        CheckSelectedLanguage(submnuportuguese);
            //                        objFunction.SetLaguageInSettingForm("Portuguese");
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ServerLog.Write("frmMainGTS::submnuportuguese_Click" + ex.ToString() + ex.StackTrace, true);
            //                    }

            //                    break;
            //                case "German":
            //                    try
            //                    {
            //                        lang_string = "de";
            //                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
            //                        objFunction.SetText();
            //                        objFunction.UncheckAllLanguageStatus();
            //                        CheckSelectedLanguage(submnuGerman);
            //                        objFunction.SetLaguageInSettingForm("German");
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ServerLog.Write("frmMainGTS::submnuGerman_Click" + ex.ToString() + ex.StackTrace, true);
            //                    }
            //                    break;
            //                case "Greek":
            //                    try
            //                    {
            //                        lang_string = "el";
            //                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
            //                        objFunction.SetText();
            //                        objFunction.UncheckAllLanguageStatus();
            //                        CheckSelectedLanguage(submnugreek);
            //                        objFunction.SetLaguageInSettingForm("Greek");
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ServerLog.Write("frmMainGTS::submnugreek_Click" + ex.ToString() + ex.StackTrace, true);
            //                    }
            //                    break;
            //                case "Bulgarian":
            //                    try
            //                    {
            //                        lang_string = "bg";
            //                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
            //                        objFunction.SetText();
            //                        objFunction.UncheckAllLanguageStatus();
            //                        CheckSelectedLanguage(submnuBulgarian);
            //                        objFunction.SetLaguageInSettingForm("Bulgarian");
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ServerLog.Write("frmMainGTS::submnuBulgarian_Click" + ex.ToString() + ex.StackTrace, true);
            //                    }
            //                    break;
            //                case "Dutch":
            //                    try
            //                    {
            //                        lang_string = "nl";
            //                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
            //                        objFunction.SetText();
            //                        objFunction.UncheckAllLanguageStatus();
            //                        CheckSelectedLanguage(submnuDutch);
            //                        objFunction.SetLaguageInSettingForm("Dutch");

            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ServerLog.Write("frmMainGTS::submnuDutch_Click" + ex.ToString() + ex.StackTrace, true);
            //                    }
            //                    break;
            //                case "Hebrew":
            //                    try
            //                    {
            //                        lang_string = "he";
            //                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
            //                        objFunction.SetText();
            //                        objFunction.UncheckAllLanguageStatus();
            //                        CheckSelectedLanguage(submnuHebrew);
            //                        objFunction.SetLaguageInSettingForm("Hebrew");
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ServerLog.Write("frmMainGTS::submnuHebrew_Click" + ex.ToString() + ex.StackTrace, true);
            //                    }
            //                    break;
            //                case "Hungarian":
            //                    try
            //                    {
            //                        lang_string = "hu";
            //                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
            //                        objFunction.SetText();
            //                        objFunction.UncheckAllLanguageStatus();
            //                        CheckSelectedLanguage(submnuHungarian);
            //                        objFunction.SetLaguageInSettingForm("Hungarian");
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ServerLog.Write("frmMainGTS::submnuHungarian_Click" + ex.ToString() + ex.StackTrace, true);
            //                    }
            //                    break;
            //                case "Czech":
            //                    try
            //                    {
            //                        lang_string = "cs";
            //                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
            //                        objFunction.SetText();
            //                        objFunction.UncheckAllLanguageStatus();
            //                        CheckSelectedLanguage(submnuCzech);
            //                        objFunction.SetLaguageInSettingForm("Czech");
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ServerLog.Write("frmMainGTS::submnuCzech_Click" + ex.ToString() + ex.StackTrace, true);
            //                    }
            //                    break;
            //                case "Estorian":
            //                    try
            //                    {
            //                        lang_string = "et";
            //                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
            //                        objFunction.SetText();
            //                        objFunction.UncheckAllLanguageStatus();
            //                        CheckSelectedLanguage(submnuEstorian);
            //                        objFunction.SetLaguageInSettingForm("Estorian");
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ServerLog.Write("frmMainGTS::submnuEstorian_Click" + ex.ToString() + ex.StackTrace, true);
            //                    }
            //                    break;
            //                case "Indonesian":
            //                    try
            //                    {
            //                        lang_string = "id";
            //                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
            //                        objFunction.SetText();
            //                        objFunction.UncheckAllLanguageStatus();
            //                        CheckSelectedLanguage(submnuIndonesian);
            //                        objFunction.SetLaguageInSettingForm("Indonasian");
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ServerLog.Write("frmMainGTS::submnuIndonesian_Click" + ex.ToString() + ex.StackTrace, true);
            //                    }
            //                    break;
            //                case "Lithuanian":
            //                    try
            //                    {
            //                        lang_string = "lt";
            //                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
            //                        objFunction.SetText();
            //                        objFunction.UncheckAllLanguageStatus();
            //                        CheckSelectedLanguage(submnuLithuanian);
            //                        objFunction.SetLaguageInSettingForm("Lithuanian");
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ServerLog.Write("frmMainGTS::submnuLithuanian_Click" + ex.ToString() + ex.StackTrace, true);
            //                    }
            //                    break;
            //                case "Serbian":
            //                    try
            //                    {
            //                        lang_string = "sr";
            //                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
            //                        objFunction.SetText();
            //                        objFunction.UncheckAllLanguageStatus();
            //                        CheckSelectedLanguage(submnuSerbian);
            //                        objFunction.SetLaguageInSettingForm("Serbian");
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ServerLog.Write("frmMainGTS::submnuSerbian_Click" + ex.ToString() + ex.StackTrace, true);
            //                    }
            //                    break;
            //                case "Korean":
            //                    try
            //                    {
            //                        lang_string = "ko";
            //                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
            //                        objFunction.SetText();
            //                        objFunction.UncheckAllLanguageStatus();
            //                        CheckSelectedLanguage(submnuKorean);
            //                        objFunction.SetLaguageInSettingForm("Korean");

            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ServerLog.Write("frmMainGTS::submnuKorean_Click" + ex.ToString() + ex.StackTrace, true);
            //                    }
            //                    break;
            //                case "Italian":
            //                    try
            //                    {
            //                        lang_string = "it";
            //                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
            //                        objFunction.SetText();
            //                        objFunction.UncheckAllLanguageStatus();
            //                        CheckSelectedLanguage(submnuItalian);
            //                        objFunction.SetLaguageInSettingForm("Italian");

            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ServerLog.Write("frmMainGTS::submnuItalian_Click" + ex.ToString() + ex.StackTrace, true);
            //                    }
            //                    break;
            //                case "Malay":
            //                    try
            //                    {
            //                        lang_string = "ms";
            //                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
            //                        objFunction.SetText();
            //                        objFunction.UncheckAllLanguageStatus();
            //                        CheckSelectedLanguage(submnuMalay);
            //                        objFunction.SetLaguageInSettingForm("Malayasian");
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ServerLog.Write("frmMainGTS::submnuMalay_Click" + ex.ToString() + ex.StackTrace, true);
            //                    }
            //                    break;
            //                case "ChineseT":
            //                    try
            //                    {
            //                        lang_string = "zh-CHT";
            //                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
            //                        objFunction.SetText();
            //                        objFunction.UncheckAllLanguageStatus();
            //                        CheckSelectedLanguage(submnuChineseT);
            //                        objFunction.SetLaguageInSettingForm("ChieneseT");
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ServerLog.Write("frmMainGTS::submnuChineseT_Click" + ex.ToString() + ex.StackTrace, true);
            //                    }
            //                    break;
            //                case "Slovak":
            //                    try
            //                    {
            //                        lang_string = "sk";
            //                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
            //                        objFunction.SetText();
            //                        objFunction.UncheckAllLanguageStatus();
            //                        CheckSelectedLanguage(submnuSlovak);
            //                        objFunction.SetLaguageInSettingForm("Slovak");
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ServerLog.Write("frmMainGTS::submnuSlovak_Click" + ex.ToString() + ex.StackTrace, true);
            //                    }
            //                    break;

            //                case "Thai":
            //                    try
            //                    {
            //                        lang_string = "th";
            //                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
            //                        objFunction.SetText();
            //                        objFunction.UncheckAllLanguageStatus();
            //                        CheckSelectedLanguage(submnuThai);
            //                        objFunction.SetLaguageInSettingForm("Thai");
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ServerLog.Write("frmMainGTS::submnuSlovak_Click" + ex.ToString() + ex.StackTrace, true);
            //                    }
            //                    break;
            //#endregion

            //                #region Toolbar Menu(View Sub Menu)
            //                case "Standard":
            //                    objToolFunction.StandardToolbarState();
            //                    break;
            //                case "Charts":
            //                    objToolFunction.ChartToolbarState();
            //                    break;
            //                case "Line_Studies":
            //                    objToolFunction.LineStudiesState();
            //                    break;
            //                case "Periodicity":
            //                    objToolFunction.PeriodicityToolbarState();
            //                    break;
            //                case "Account Toolbar":
            //                    objToolFunction.AccountToolbarState();
            //                    break;
            //                case "Analysis_Toolbar":
            //                    objToolFunction.AnalysisToolbarState();
            //                    break;
            //                case "Connectivity_Toolbar":
            //                    objToolFunction.ConnectivityToolbarState();
            //                    break;

            //                #endregion


            //                #region Account Open Sub menu
            //                case "Live_Account":
            //                    objToolFunction.LiveAccount();
            //                    break;
            //                case "Sub_Account":
            //                    objToolFunction.SubAccount();
            //                    break;
            //                case "Demo_Account":
            //                    objToolFunction.DemoAccount();
            //                    break;
            //                #endregion

            //                #region Economic Calendar
            //                case "mnuFundamentalAnalysis_EconomicCalander":
            //                    objToolFunction.EconomicCalender();
            //                    break;
            //        #endregion

            //                #region Technical analysis (ADD sub menu)
            //                case "Horizontal_Line":
            //                    objToolFunction.HorizontleLineDisplay();
            //                    break;
            //                case "Vertical_Line":
            //                    objToolFunction.VerticalLineDisplay();
            //                    break;
            //                case "Text":
            //                    objToolFunction.TextDisplay();
            //                    break;
            //                case "Trend_Lines":
            //                    objToolFunction.TrendLineDisplay();
            //                    break;
            //                case "Ellipse":
            //                    objToolFunction.EllipseDisplay();
            //                    break;
            //                case "Speed_Lines":
            //                    objToolFunction.SpeedLineDisplay();
            //                    break;
            //                case "Gann_Fan":
            //                    objToolFunction.GannFanDisplay();
            //                    break;
            //                case "Fibonacci_Arcs":
            //                    objToolFunction.FibonacciArcsDisplay();
            //                    break;
            //                case "Fibonacci_Retracement":
            //                    objToolFunction.FibonacciRetracementDisplay();//Crash the Application
            //                    break;
            //                case "Fibonacci_Fan":
            //                    objToolFunction.FibonacciFanDisplay();
            //                    break;
            //                case "Fibonacci_Timezone":
            //                    objToolFunction.FibonacciTimezoneDisplay();
            //                    break;
            //                case "Tirone_Level":
            //                    objToolFunction.TironeLevelDisplay();
            //                    break;
            //                case "Quadrent_lines":
            //                    objToolFunction.QuadrentLinesDisplay();
            //                    break;
            //                case "Raff_Regression":
            //                    objToolFunction.RafRegressionDisplay();
            //                    break;
            //                case "Error_channel":
            //                    objToolFunction.ErrorChannelDisplay();
            //                    break;
            //                case "Rectangle":
            //                    objToolFunction.RectangleDisplay();
            //                    break;
            //                case "Free_hand_drawing":
            //                    objToolFunction.FreeHandDrawingDisplay();
            //                    break;
            //            #endregion

            //                #region Snapshot
            //                case "Save":
            //                    objChartFunction.Snap_Save();
            //                    break;
            //                case "mnuFile_Print":
            //                    objChartFunction.SnapPrint();
            //                    break;
            //                #endregion

            //                #region Chart Price Type
            //                case "Point_figure":
            //                    objChartFunction.PointFigDisplay();
            //                    break;
            //                case "Renko":
            //                    objChartFunction.RenkoDisplay();
            //                    break;
            //                case "kagi":
            //                    objChartFunction.KagiDisplay();
            //                    break;
            //                case "Three_line_break":
            //                    objChartFunction.ThreeLineBreakDisplay();
            //                    break;
            //                case "heikin_Ashi":
            //                    objChartFunction.HeikinAshiDisplay();
            //                    break;
            //                case "Equi_volume":
            //                    objChartFunction.EquiVolumeDisplay();
            //                    break;
            //                case "Equi_volume_shadow":
            //                    objChartFunction.EquiVolumeShadowDisplay();
            //                    break;
            //                case "candle_volume":
            //                    objChartFunction.CandleVolumeDisplay();
            //                    break;
            //                case "Standard Chart":
            //                    objChartFunction.StandardChartDisplay();
            //                    break;
            //            #endregion

            //                #region Chart Type
            //                case "Bar_Chart":
            //                    objChartFunction.BarChartDisplay();
            //                    break;
            //                case "Candle_Stick":
            //                    objChartFunction.CandlestickChartDisplay();
            //                    break;
            //                case "Line_Chart":
            //                    objChartFunction.LineChartDisplay();
            //                    break;
            //            #endregion

            //                #region Chart Periodicity
            //                case "Minute_1":
            //                    objChartFunction.Period_1_min();
            //                    break;
            //                case "Minutes_5":
            //                    objChartFunction.Period_5_min();
            //                    break;
            //                case "Minutes_15":
            //                    objChartFunction.Period_15_min();
            //                    break;
            //                case "Minutes_30":
            //                    objChartFunction.Period_30_min();
            //                    break;
            //                case "Hour_1":
            //                    objChartFunction.Period_1_hour();
            //                    break;
            //                case "Hour_4":
            //                    objChartFunction.Period_4_hour();
            //                    break;
            //                case "Daily":
            //                    objChartFunction.Period_daily();
            //                    break;
            //                case "weekly":
            //                    objChartFunction.Period_weekly();
            //                    break;
            //                case "Monthly":
            //                    objChartFunction.Period_Month();
            //                    break;
            //            #endregion

            //                #region Chart Toolbar Items
            //                case "Empty Bar":
            //                    break;
            //                 case "Chart Shift":
            //                    objChartFunction.ChartShift();
            //                    break;
            //                case "Chart Autoscroll":
            //                    objChartFunction.ChartAutoScroll();
            //                    break;
            //                #endregion

            //                #region Standard toolbar Items
            //                case "Quotes":
            //                    objChartFunction.ShowSpecificChart(e.Command.ToString());
            //                    break;
            //                case "Close All":
            //                    break;
            //                case "Cancel All":
            //                    break;
            //           #endregion

            //                #region Connectivity Toolbar Items
            //                case "Reconnect":
            //                    break;
            //                case "24x7":
            //                    break;
            //                case "Chat":
            //                    break;
            //                case "Settings":
            //                    objToolFunction.Option();
            //                    break;
            //                case "Mail":
            //                    frmAlertSendMail objSendMail = new frmAlertSendMail(uctlTerminal1.uctlMailGrid1);
            //                    objSendMail.DispalyItem("Re:Registration");
            //                    objSendMail.ShowDialog();
            //                    break;
            //                case "Quote Connection":
            //                    break;
            //                case "Order Connection":
            //                    break;
            // #endregion

            //                #region Analysis Toolbar Item
            //                case "News Feed":
            //                    break;
            //                case "Galaxy Script Creator":
            //                    break;
            //                case "Wizard":
            //                    break;
            //                default:
            //                    break;

            //                #endregion
            //            }
            #endregion
            switch (command)
            {
                #region File Menu

                case "Load Workspace":
                    state.Load();
                    LoadCommandBarSettings();
                    //state.ResolveDocumentClient -= new ResolveClientEventHandler(state_ResolveDocumentClient);
                    break;

                case "Load Default Workspace":
                    string pathWorkspace = Application.StartupPath + @"\Resources\ResetWorkspace.dfb";
                    string pathToolbar = Application.StartupPath + @"\Resources\ResetWorkspace.cbs";

                    if (File.Exists(pathWorkspace))
                    {
                        state.Load(pathWorkspace);
                       // state.ResolveDocumentClient -= new ResolveClientEventHandler(state_ResolveDocumentClient);
                    }
                    else
                    {
                        clsGlobal.DisplayMessage("Sorry ! File not Found to Reset Workspace.....!");
                    }
                    if (File.Exists(pathToolbar))
                    {
                        m_state.Load(pathToolbar);
                    }
                    else
                    {
                        clsGlobal.DisplayMessage("Sorry ! File not Found to Reset Toolbars.....!");
                    }
                    objFunction.DisableMenuOptions();
                    break;

                case "Load Chart from Disk":
                    objChartFunction.LoadChartFileFromDisk();
                    break;
                case "Import Excel Selection":
                    {
                        try
                        {
                            if (m_ctlData == null) return;
                            ctlChart chart = new ctlChart(this, m_ctlData, "Excel Chart", true);

                            chart.Dock = DockStyle.Fill;
                            System.Environment.CurrentDirectory = Application.StartupPath;
                            NUIDocument doc = new NUIDocument("Excel Chart", -1, chart, Icon.ExtractAssociatedIcon(strExePath_ + "\\Resources\\New Icon.ico"));
                            m_DockManager.DocumentManager.AddDocument(doc);
                        }
                        catch (Exception ex)
                        {
                            ServerLog.Write("frmMain::cmdImportExcel_Click" + ex.ToString() + ex.StackTrace, true);
                            Logger.LogEx(ex, "frmMainGTS", "m_CmdBarsManager_CommandClicked(object sender, CommandEventArgs e) Import Excel Selection");
                        }

                    }
                    break;
                case "Save Workspace":
                    string CMDBarPath = string.Empty;
                    string WrkSpacePath = string.Empty;
                    getToolbarSettingPath(out WrkSpacePath, out CMDBarPath);
                    if (string.IsNullOrEmpty(CMDBarPath))
                    {
                        SaveFileDialog saveFileDia = new SaveFileDialog();
                        saveFileDia.DefaultExt = ".dfb";
                        saveFileDia.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + "GalaxyTradestation\\Workspace\\";
                        DialogResult res = saveFileDia.ShowDialog();
                        if (res == DialogResult.OK)
                        {
                            state.Save(saveFileDia.FileName);
                            string WorkSpacePath = saveFileDia.FileName;
                            string folder = WorkSpacePath.Substring(0, WorkSpacePath.LastIndexOf("\\"));
                            FileInfo objfile = new FileInfo(WorkSpacePath);
                            string FileName = objfile.Name.Remove(objfile.Name.IndexOf("."), 4);
                            string ToolBarPath = folder + "\\" + FileName + ".cbs";
                            SaveCommandBarSettings(ToolBarPath);

                        }
                    }
                    else
                    {
                        state.Save(WrkSpacePath);
                        SaveCommandBarSettings(CMDBarPath);
                    }
                    //if (Properties.Settings.Default.IsStartupWorkspaceSpecific == true)
                    //{
                    //    state.Save(Properties.Settings.Default.LoadWorkspacePath);
                    //    SaveCommandBarSettings();
                    //    //state.ResolveDocumentClient -= new ResolveClientEventHandler(state_ResolveDocumentClient);
                    //}
                    //if (Properties.Settings.Default.IsStartupWorkspacePrevious == true)
                    //{
                    //    #region Reset Settings
                    //    //state.Save(@"C:\Documents and Settings\user\My Documents\GalaxyTradestation\Workspace\ResetWorkspace.dfb");//This line is used to generate default workspace settings
                    //    //state.Save(@"C:\Documents and Settings\user\My Documents\GalaxyTradestation\Workspace\ResetToolbar.cbs");//This line is used to generate default Toolbar settings
                    //    #endregion
                    //    SaveCommandBarSettings();
                    //    state.Save(clsGlobal.SaveWorkspacePath);
                    //    //state.ResolveDocumentClient -= new ResolveClientEventHandler(state_ResolveDocumentClient);
                    //}
                    //if (Properties.Settings.Default.IsStartupWorkspaceNone == true)
                    //{
                    //    SaveCommandBarSettings();
                    //    state.Save(clsGlobal.SaveWorkspacePath);
                    //    //state.ResolveDocumentClient -= new ResolveClientEventHandler(state_ResolveDocumentClient);
                    //}

                    break;

                case "SaveWorkspace As":
                    {
                        SaveFileDialog saveFileDia = new SaveFileDialog();
                        saveFileDia.DefaultExt = ".dfb";
                        saveFileDia.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + "GalaxyTradestation\\Workspace\\";
                        DialogResult res = saveFileDia.ShowDialog();
                        if (res == DialogResult.OK)
                        {
                            state.Save(saveFileDia.FileName);
                            string WorkSpacePath = saveFileDia.FileName;
                            string folder = WorkSpacePath.Substring(0, WorkSpacePath.LastIndexOf("\\"));
                            FileInfo objfile = new FileInfo(WorkSpacePath);
                            string FileName = objfile.Name.Remove(objfile.Name.IndexOf("."), 4);
                            string ToolBarPath = folder + "\\" + FileName + ".cbs";
                            SaveCommandBarSettings(ToolBarPath);

                        }
                    }
                    break;
                case "Save Chart":
                    try
                    {
                        if (m_ActiveChart == null) return;
                        string title = clsChartFunctions.GetChartTitle(m_ActiveChart._ChartSymbol, m_ActiveChart.m_Periodicity, m_ActiveChart.m_BarSize);
                        m_ActiveChart.SaveChart(title);
                    }
                    catch (Exception ex)
                    {
                        ServerLog.Write("frmMainGTS::mnuFileSaveChart_Click" + ex.ToString() + ex.StackTrace, true);
                        Logger.LogEx(ex, "frmMainGTS", "m_CmdBarsManager_CommandClicked(object sender, CommandEventArgs e) Save Chart");
                    }
                    break;

                case "Save Chart as Image":
                    try
                    {
                        if (m_ActiveChart == null) return;
                        m_ActiveChart.SaveChartImage();
                    }
                    catch (Exception ex)
                    {
                        ServerLog.Write("frmMainGTS::mnuFileSaveImage_Click" + ex.ToString() + ex.StackTrace, true);
                        Logger.LogEx(ex, "frmMainGTS", "m_CmdBarsManager_CommandClicked(object sender, CommandEventArgs e) Save Chart as Image");
                    }
                    break;

                case "Save Chart as Template":
                    try
                    {
                        if (m_ActiveChart == null) return;
                        m_ActiveChart.StockChartX1.SaveGeneralTemplate(SaveDialog("Chart Template|*.sct"));
                    }
                    catch (Exception ex)
                    {
                        ServerLog.Write("frmMainGTS::mnuFileSaveTemplate_Click" + ex.ToString() + ex.StackTrace, true);
                        Logger.LogEx(ex, "frmMainGTS", "m_CmdBarsManager_CommandClicked(object sender, CommandEventArgs e) Save Chart as Template");
                    }
                    break;

                case "Login":
                    try
                    {
                        string str = frmMainGTS.GetReference().Text.Split(':')[0].Substring(0, frmMainGTS.GetReference().Text.Split(':')[0].Length - 1);
                        ActiveAccount account = OrderManager.getOrderManager().GetAccount(str);
                        frmDataLogin frmDL = new frmDataLogin(this, account.LoginID_, account.Password, true);
                        frmDL.ShowDialog();
                        objFunction.EnableApplicationMode();
                    }
                    catch (Exception ex)
                    {
                        ServerLog.Write("frmMainGTS::mnuLogin_Click_1" + ex.ToString() + ex.StackTrace, true);
                        Logger.LogEx(ex, "frmMainGTS", "m_CmdBarsManager_CommandClicked(object sender, CommandEventArgs e) Login");
                    }
                    break;

                case "Logout":
                    //this.Close();
                    //mnuLogin.Enabled = true;
                    //mnuLogout.Enabled = false;
                    //objFunction.DisableApplicationMode();
                    break;

                case "Exit":
                    this.Close();
                    break;
                #endregion

                #region View
                case "Status Bar":
                    objToolFunction.StatusBarState();
                    break;
                case "Market Watch":
                    objToolFunction.MarketWatchState();
                    break;
                case "Data Window":
                    break;
                case "Navigator":
                    objToolFunction.NavigatorState();
                    break;
                case "Terminal":
                    objToolFunction.TerminalState();
                    break;
                case "Strategy Tester":
                    break;
                case "Full Screen":
                    this.WindowState = FormWindowState.Maximized;
                    break;
                case "Level2":
                    objToolFunction.Level2State();
                    break;
                case "Forex Screen":
                    objToolFunction.ForexScreenState();
                    break;
                case "quote Pair":
                    DataSymbol ds = new DataSymbol(this);
                    ds.ShowDialog();
                    break;
                case "Execution View":
                    objToolFunction.ExecutionViewState();
                    break;
                #endregion

                #region Tools Menu
                case "Connect Trade Server":
                    break;
                case "Connect Quote Server":
                    break;
                case "Stock Scanner":
                    break;
                case "Alert":
                    frmAlert objfrmAlert = new frmAlert(ALERT_MODE.CREATE, null);
                    objfrmAlert.ShowDialog();
                    break;
                case "Close All Positions":
                    uctlTradeGrid.GetTradeGridInstance.CloseAll(false);
                    break;
                case "Cancel All Orders":
                    uctlTradeGrid.GetTradeGridInstance.CancelAll(false);
                    break;
                case "Hedge All":
                    break;
                case "Galaxy Creator":
                    break;
                case "Forum":
                    break;
                case "Order Entry":
                    try
                    {
                        Frm.frmOrderEntry.OpenOrderEntry("EURUSD");
                        
                        //ashutoshNewOrder
                        //objOrderFunction.ShowOrderEntry();
                    }
                    catch (Exception ex)
                    {
                        ServerLog.Write("frmMainGTS::mnuNewOrder_Click" + ex.ToString() + ex.StackTrace, true);
                        Logger.LogEx(ex, "frmMainGTS", "m_CmdBarsManager_CommandClicked(object sender, CommandEventArgs e) Order Entry");
                    }
                    break;
                #endregion

                #region Charts Menu
                case "New Chart":

                    objChartFunction.CreateNewChart();
                    break;
                case "Zoom In":
                    objChartFunction.ZoomIn();
                    break;
                case "Zoom Out":
                    objChartFunction.ZoomOut();
                    break;
                case "Track Cursor":
                    objChartFunction.TrackCursorDisplay();
                    break;
                case "Volume":
                    
                    //try
                    //    {
                    //        if (m_ActiveChart == null) return;

                    //        string symbol = m_ActiveChart.StockChartX1.Symbol;
                    //        bool bVisible = !m_ActiveChart.StockChartX1.get_SeriesVisible(symbol + ".Volume");
                    //        m_ActiveChart.StockChartX1.set_SeriesVisible(symbol + ".Volume", bVisible);
                    //        if (mnuVolume.Checked == true)
                    //        {
                    //            m_ActiveChart.mnucVolumes.Checked = false;
                    //            mnuVolume.Checked = false;
                    //        }
                    //        else
                    //        {
                    //            m_ActiveChart.mnucVolumes.Checked = true;
                    //            mnuVolume.Checked = true;
                    //        }
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        ServerLog.Write("frmMain::nChart_Volume_Click" + ex.ToString() + ex.StackTrace, true);
                    //    }
                   
                    objChartFunction.VolumeDisplay();
                    break;
                case "Grid":
                    objChartFunction.GridDisplay();
                    break;
                case "Positions":
                    //OrderEntryHandler.GetHandler().GetPositionInfo()
                    break;
                case "Orders":
                    //OrderEntryHandler.GetHandler().CFXAPIRequestOrders();
                    break;
                case "3D Style":
                    objChartFunction.ThreeDStyle();
                    break;
                #endregion

                #region Technical Analysis Menu

                case "Add Trading System":
                    break;
                case "Trading System List":
                    break;
                case "Create Trading System":
                    break;
                case "Add Visual Advisor":
                    break;
                case "Auto Trading":
                    break;
                case "Indicator List":
                    objToolFunction.AddIndicatorList();
                    break;
                case "Create Indicator":
                    break;
                #endregion

                #region Account Service Menu
                case "Transfer Fund":
                    objToolFunction.TransferFund();
                    break;
                case "Deposit Fund":
                    objToolFunction.DepositFund();
                    break;
                case "Withdraw Fund":
                    objToolFunction.WithdrawFund();
                    break;
                case "Change Address":
                    objToolFunction.ChangeAddress();
                    break;
                case "Change Password":
                    objToolFunction.ChangePassword();
                    break;
                case "Edit Profile":
                    break;
                #endregion

                #region Reports Menu
                case "Account History/Statement":
                    break;
                case "Account history":
                    break;
                case "Account Details(Currencies)":
                    break;
                case "Account Details":
                    break;
                case "Balance Report":
                    break;
                case "Profit/Loss":
                    break;
                case "Deposit":
                    break;
                case "Withdrawl":
                    break;
                case "Trade Report":
                    break;
                #endregion

                #region Setting Menu
                case "Mac Skin":
                    objToolFunction.Mac();
                    break;
                case "Vista Skin":
                    objToolFunction.Vista();
                    break;
                case "Office 2007 Skin":
                    objToolFunction.Office2007();
                    break;
                case "Office2007 Silver":
                    objToolFunction.Office2007Silver();
                    break;
                case "Reset Settings":
                    Initializer.GetReference().objSettingForm_.LoadDefault();
                    objFunction.ApplySetting();

                    break;
                case "User Preferences":
                case "User Preference":
                    objToolFunction.Option();
                    break;
                #endregion

                #region  Window Menu
                case "New Window":
                    break;
                case "Cascade":
                    objToolFunction.Cascade();
                    break;
                case "Tile Vertically":
                    VerticallyAction();
                    break;
                case "Arrange Icons":
                    break;
                case "Tile Horizontally":
                    break;
                #endregion

                #region Help Menu
                case "Help & Contents":
                    objToolFunction.DisplayHelp();
                    break;
                case "User Manual":
                    objToolFunction.HelpUserManual();
                    break;
                case "Home Page":
                    break;
                case "Bug Report":
                    objToolFunction.BugReport();
                    break;
                case "Feature Request":
                    objToolFunction.FeatureRequest();
                    break;
                case "Ask Question":
                    objToolFunction.AskQuestion();
                    break;
                case "Log File":
                    break;
                case "Check For Update":
                    break;
                case "About Galaxy":
                    (new frmHelp()).ShowDialog();
                    break;
                #endregion

                #region Language (View Sub Menu)
                case "English":
                    try
                    {
                        lang_string = "en";
                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
                        objFunction.SetText();
                        objFunction.UncheckAllLanguageStatus();
                        CheckSelectedLanguage(submnuEnglish);
                        objFunction.SetLaguageInSettingForm("English");
                    }
                    catch (Exception ex)
                    {
                        ServerLog.Write("frmMainGTS::submnuEnglish_Click" + ex.ToString() + ex.StackTrace, true);
                        Logger.LogEx(ex, "frmMainGTS", "m_CmdBarsManager_CommandClicked(object sender, CommandEventArgs e) English");
                    }
                    break;
                case "Spanish":
                    try
                    {
                        lang_string = "es";
                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
                        objFunction.SetText();
                        objFunction.UncheckAllLanguageStatus();
                        CheckSelectedLanguage(submnuSpanish);
                        objFunction.SetLaguageInSettingForm("Spanish");
                    }
                    catch (Exception ex)
                    {
                        ServerLog.Write("frmMainGTS::submnuSpanish_Click" + ex.ToString() + ex.StackTrace, true);
                        Logger.LogEx(ex, "frmMainGTS", "m_CmdBarsManager_CommandClicked(object sender, CommandEventArgs e) Spanish");
                    }
                    break;
                case "Vietnamese":
                    try
                    {
                        lang_string = "vi";
                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
                        objFunction.SetText();
                        objFunction.UncheckAllLanguageStatus();
                        CheckSelectedLanguage(submnuVietnamese);
                        objFunction.SetLaguageInSettingForm("Vietnamese");
                    }
                    catch (Exception ex)
                    {
                        ServerLog.Write("frmMainGTS::submnuVietnamese_Click" + ex.ToString() + ex.StackTrace, true);
                        Logger.LogEx(ex, "frmMainGTS", "m_CmdBarsManager_CommandClicked(object sender, CommandEventArgs e) Vietnamese");
                    }
                    break;
                case "Chienese":
                    try
                    {
                        lang_string = "zh-CHS";
                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
                        objFunction.SetText();
                        objFunction.UncheckAllLanguageStatus();
                        CheckSelectedLanguage(submnuChienese);
                        objFunction.SetLaguageInSettingForm("Chienese");
                    }
                    catch (Exception ex)
                    {
                        ServerLog.Write("frmMainGTS::submnuChienese_Click" + ex.ToString() + ex.StackTrace, true);
                        Logger.LogEx(ex, "frmMainGTS", "m_CmdBarsManager_CommandClicked(object sender, CommandEventArgs e) Chienese");
                    }
                    break;
                case "Hindi":
                    try
                    {
                        lang_string = "hi";
                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
                        objFunction.SetText();
                        objFunction.UncheckAllLanguageStatus();
                        CheckSelectedLanguage(submnuHindi);
                        objFunction.SetLaguageInSettingForm("Hindi");
                    }
                    catch (Exception ex)
                    {
                        ServerLog.Write("frmMainGTS::submnuHindi_Click" + ex.ToString() + ex.StackTrace, true);
                        Logger.LogEx(ex, "frmMainGTS", "m_CmdBarsManager_CommandClicked(object sender, CommandEventArgs e) Hindi");
                    }
                    break;
                case "Russian":
                    try
                    {
                        lang_string = "ru";
                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
                        objFunction.SetText();
                        objFunction.UncheckAllLanguageStatus();
                        CheckSelectedLanguage(submnuRussian);
                        objFunction.SetLaguageInSettingForm("Russian");
                    }
                    catch (Exception ex)
                    {
                        ServerLog.Write("frmMainGTS::submnuRussian_Click" + ex.ToString() + ex.StackTrace, true);
                        Logger.LogEx(ex, "frmMainGTS", "m_CmdBarsManager_CommandClicked(object sender, CommandEventArgs e) Russian");
                    }
                    break;
                case "Arabic":
                    try
                    {
                        lang_string = "ar";
                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
                        objFunction.SetText();
                        objFunction.UncheckAllLanguageStatus();
                        CheckSelectedLanguage(submnuArabic);
                        objFunction.SetLaguageInSettingForm("Arabic");

                    }
                    catch (Exception ex)
                    {
                        ServerLog.Write("frmMainGTS::submnuArabic_Click" + ex.ToString() + ex.StackTrace, true);
                        Logger.LogEx(ex, "frmMainGTS", "m_CmdBarsManager_CommandClicked(object sender, CommandEventArgs e) Arabic");
                    }
                    break;
                case "French":
                    try
                    {
                        lang_string = "fr";
                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
                        objFunction.SetText();
                        objFunction.UncheckAllLanguageStatus();
                        CheckSelectedLanguage(submnuFrnch);
                        objFunction.SetLaguageInSettingForm("French");
                    }
                    catch (Exception ex)
                    {
                        ServerLog.Write("frmMainGTS::submnuFrnch_Click" + ex.ToString() + ex.StackTrace, true);
                        Logger.LogEx(ex, "frmMainGTS", "m_CmdBarsManager_CommandClicked(object sender, CommandEventArgs e) French");
                    }
                    break;
                case "Japanese":
                    try
                    {
                        lang_string = "ja";
                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
                        objFunction.SetText();
                        objFunction.UncheckAllLanguageStatus();
                        CheckSelectedLanguage(submnuJapnese);
                        objFunction.SetLaguageInSettingForm("Japanese");
                    }
                    catch (Exception ex)
                    {
                        ServerLog.Write("frmMainGTS::submnuJapnese_Click" + ex.ToString() + ex.StackTrace, true);
                        Logger.LogEx(ex, "frmMainGTS", "m_CmdBarsManager_CommandClicked(object sender, CommandEventArgs e) Japanese");
                    }
                    break;
                case "Turkish":
                    try
                    {
                        lang_string = "tr-TR";
                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
                        objFunction.SetText();
                        objFunction.UncheckAllLanguageStatus();
                        CheckSelectedLanguage(submnuTurkish);
                        objFunction.SetLaguageInSettingForm("Turkish");
                    }
                    catch (Exception ex)
                    {
                        ServerLog.Write("frmMainGTS::submnuTurkish_Click" + ex.ToString() + ex.StackTrace, true);
                        Logger.LogEx(ex, "frmMainGTS", "m_CmdBarsManager_CommandClicked(object sender, CommandEventArgs e) Turkish");
                    }
                    break;
                case "Portuguese":
                    try
                    {
                        lang_string = "pt";
                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
                        objFunction.SetText();
                        objFunction.UncheckAllLanguageStatus();
                        CheckSelectedLanguage(submnuportuguese);
                        objFunction.SetLaguageInSettingForm("Portuguese");
                    }
                    catch (Exception ex)
                    {
                        ServerLog.Write("frmMainGTS::submnuportuguese_Click" + ex.ToString() + ex.StackTrace, true);
                        Logger.LogEx(ex, "frmMainGTS", "m_CmdBarsManager_CommandClicked(object sender, CommandEventArgs e) Portuguese");
                    }

                    break;
                case "German":
                    try
                    {
                        lang_string = "de";
                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
                        objFunction.SetText();
                        objFunction.UncheckAllLanguageStatus();
                        CheckSelectedLanguage(submnuGerman);
                        objFunction.SetLaguageInSettingForm("German");
                    }
                    catch (Exception ex)
                    {
                        ServerLog.Write("frmMainGTS::submnuGerman_Click" + ex.ToString() + ex.StackTrace, true);
                        Logger.LogEx(ex, "frmMainGTS", "m_CmdBarsManager_CommandClicked(object sender, CommandEventArgs e) German");
                    }
                    break;
                case "Greek":
                    try
                    {
                        lang_string = "el";
                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
                        objFunction.SetText();
                        objFunction.UncheckAllLanguageStatus();
                        CheckSelectedLanguage(submnugreek);
                        objFunction.SetLaguageInSettingForm("Greek");
                    }
                    catch (Exception ex)
                    {
                        ServerLog.Write("frmMainGTS::submnugreek_Click" + ex.ToString() + ex.StackTrace, true);
                        Logger.LogEx(ex, "frmMainGTS", "m_CmdBarsManager_CommandClicked(object sender, CommandEventArgs e) Greek");
                    }
                    break;
                case "Bulgarian":
                    try
                    {
                        lang_string = "bg";
                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
                        objFunction.SetText();
                        objFunction.UncheckAllLanguageStatus();
                        CheckSelectedLanguage(submnuBulgarian);
                        objFunction.SetLaguageInSettingForm("Bulgarian");
                    }
                    catch (Exception ex)
                    {
                        ServerLog.Write("frmMainGTS::submnuBulgarian_Click" + ex.ToString() + ex.StackTrace, true);
                        Logger.LogEx(ex, "frmMainGTS", "m_CmdBarsManager_CommandClicked(object sender, CommandEventArgs e) Bulgarian");
                    }
                    break;
                case "Dutch":
                    try
                    {
                        lang_string = "nl";
                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
                        objFunction.SetText();
                        objFunction.UncheckAllLanguageStatus();
                        CheckSelectedLanguage(submnuDutch);
                        objFunction.SetLaguageInSettingForm("Dutch");

                    }
                    catch (Exception ex)
                    {
                        ServerLog.Write("frmMainGTS::submnuDutch_Click" + ex.ToString() + ex.StackTrace, true);
                        Logger.LogEx(ex, "frmMainGTS", "m_CmdBarsManager_CommandClicked(object sender, CommandEventArgs e) Dutch");
                    }
                    break;
                case "Hebrew":
                    try
                    {
                        lang_string = "he";
                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
                        objFunction.SetText();
                        objFunction.UncheckAllLanguageStatus();
                        CheckSelectedLanguage(submnuHebrew);
                        objFunction.SetLaguageInSettingForm("Hebrew");
                    }
                    catch (Exception ex)
                    {
                        ServerLog.Write("frmMainGTS::submnuHebrew_Click" + ex.ToString() + ex.StackTrace, true);
                        Logger.LogEx(ex, "frmMainGTS", "m_CmdBarsManager_CommandClicked(object sender, CommandEventArgs e) Hebrew");
                    }
                    break;
                case "Hungarian":
                    try
                    {
                        lang_string = "hu";
                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
                        objFunction.SetText();
                        objFunction.UncheckAllLanguageStatus();
                        CheckSelectedLanguage(submnuHungarian);
                        objFunction.SetLaguageInSettingForm("Hungarian");
                    }
                    catch (Exception ex)
                    {
                        ServerLog.Write("frmMainGTS::submnuHungarian_Click" + ex.ToString() + ex.StackTrace, true);
                        Logger.LogEx(ex, "frmMainGTS", "m_CmdBarsManager_CommandClicked(object sender, CommandEventArgs e) Hungarian");
                    }
                    break;
                case "Czech":
                    try
                    {
                        lang_string = "cs";
                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
                        objFunction.SetText();
                        objFunction.UncheckAllLanguageStatus();
                        CheckSelectedLanguage(submnuCzech);
                        objFunction.SetLaguageInSettingForm("Czech");
                    }
                    catch (Exception ex)
                    {
                        ServerLog.Write("frmMainGTS::submnuCzech_Click" + ex.ToString() + ex.StackTrace, true);
                        Logger.LogEx(ex, "frmMainGTS", "m_CmdBarsManager_CommandClicked(object sender, CommandEventArgs e) Czech");
                    }
                    break;
                case "Estorian":
                    try
                    {
                        lang_string = "et";
                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
                        objFunction.SetText();
                        objFunction.UncheckAllLanguageStatus();
                        CheckSelectedLanguage(submnuEstorian);
                        objFunction.SetLaguageInSettingForm("Estorian");
                    }
                    catch (Exception ex)
                    {
                        ServerLog.Write("frmMainGTS::submnuEstorian_Click" + ex.ToString() + ex.StackTrace, true);
                        Logger.LogEx(ex, "frmMainGTS", "m_CmdBarsManager_CommandClicked(object sender, CommandEventArgs e) Estorian");
                    }
                    break;
                case "Indonesian":
                    try
                    {
                        lang_string = "id";
                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
                        objFunction.SetText();
                        objFunction.UncheckAllLanguageStatus();
                        CheckSelectedLanguage(submnuIndonesian);
                        objFunction.SetLaguageInSettingForm("Indonasian");
                    }
                    catch (Exception ex)
                    {
                        ServerLog.Write("frmMainGTS::submnuIndonesian_Click" + ex.ToString() + ex.StackTrace, true);
                        Logger.LogEx(ex, "frmMainGTS", "m_CmdBarsManager_CommandClicked(object sender, CommandEventArgs e) Indonesian");
                    }
                    break;
                case "Lithuanian":
                    try
                    {
                        lang_string = "lt";
                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
                        objFunction.SetText();
                        objFunction.UncheckAllLanguageStatus();
                        CheckSelectedLanguage(submnuLithuanian);
                        objFunction.SetLaguageInSettingForm("Lithuanian");
                    }
                    catch (Exception ex)
                    {
                        ServerLog.Write("frmMainGTS::submnuLithuanian_Click" + ex.ToString() + ex.StackTrace, true);
                        Logger.LogEx(ex, "frmMainGTS", "m_CmdBarsManager_CommandClicked(object sender, CommandEventArgs e) Lithuanian");
                    }
                    break;
                case "Serbian":
                    try
                    {
                        lang_string = "sr";
                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
                        objFunction.SetText();
                        objFunction.UncheckAllLanguageStatus();
                        CheckSelectedLanguage(submnuSerbian);
                        objFunction.SetLaguageInSettingForm("Serbian");
                    }
                    catch (Exception ex)
                    {
                        ServerLog.Write("frmMainGTS::submnuSerbian_Click" + ex.ToString() + ex.StackTrace, true);
                        Logger.LogEx(ex, "frmMainGTS", "m_CmdBarsManager_CommandClicked(object sender, CommandEventArgs e) Serbian");
                    }
                    break;
                case "Korean":
                    try
                    {
                        lang_string = "ko";
                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
                        objFunction.SetText();
                        objFunction.UncheckAllLanguageStatus();
                        CheckSelectedLanguage(submnuKorean);
                        objFunction.SetLaguageInSettingForm("Korean");

                    }
                    catch (Exception ex)
                    {
                        ServerLog.Write("frmMainGTS::submnuKorean_Click" + ex.ToString() + ex.StackTrace, true);
                        Logger.LogEx(ex, "frmMainGTS", "m_CmdBarsManager_CommandClicked(object sender, CommandEventArgs e) Korean");
                    }
                    break;
                case "Italian":
                    try
                    {
                        lang_string = "it";
                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
                        objFunction.SetText();
                        objFunction.UncheckAllLanguageStatus();
                        CheckSelectedLanguage(submnuItalian);
                        objFunction.SetLaguageInSettingForm("Italian");

                    }
                    catch (Exception ex)
                    {
                        ServerLog.Write("frmMainGTS::submnuItalian_Click" + ex.ToString() + ex.StackTrace, true);
                        Logger.LogEx(ex, "frmMainGTS", "m_CmdBarsManager_CommandClicked(object sender, CommandEventArgs e) Italian");
                    }
                    break;
                case "Malay":
                    try
                    {
                        lang_string = "ms";
                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
                        objFunction.SetText();
                        objFunction.UncheckAllLanguageStatus();
                        CheckSelectedLanguage(submnuMalay);
                        objFunction.SetLaguageInSettingForm("Malayasian");
                    }
                    catch (Exception ex)
                    {
                        ServerLog.Write("frmMainGTS::submnuMalay_Click" + ex.ToString() + ex.StackTrace, true);
                        Logger.LogEx(ex, "frmMainGTS", "m_CmdBarsManager_CommandClicked(object sender, CommandEventArgs e) Malay");
                    }
                    break;
                case "ChineseT":
                    try
                    {
                        lang_string = "zh-CHT";
                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
                        objFunction.SetText();
                        objFunction.UncheckAllLanguageStatus();
                        CheckSelectedLanguage(submnuChineseT);
                        objFunction.SetLaguageInSettingForm("ChieneseT");
                    }
                    catch (Exception ex)
                    {
                        ServerLog.Write("frmMainGTS::submnuChineseT_Click" + ex.ToString() + ex.StackTrace, true);
                        Logger.LogEx(ex, "frmMainGTS", "m_CmdBarsManager_CommandClicked(object sender, CommandEventArgs e) ChineseT");
                    }
                    break;
                case "Slovak":
                    try
                    {
                        lang_string = "sk";
                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
                        objFunction.SetText();
                        objFunction.UncheckAllLanguageStatus();
                        CheckSelectedLanguage(submnuSlovak);
                        objFunction.SetLaguageInSettingForm("Slovak");
                    }
                    catch (Exception ex)
                    {
                        ServerLog.Write("frmMainGTS::submnuSlovak_Click" + ex.ToString() + ex.StackTrace, true);
                        Logger.LogEx(ex, "frmMainGTS", "m_CmdBarsManager_CommandClicked(object sender, CommandEventArgs e) Slovak");
                    }
                    break;

                case "Thai":
                    try
                    {
                        lang_string = "th";
                        Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)new System.Globalization.CultureInfo(lang_string);
                        objFunction.SetText();
                        objFunction.UncheckAllLanguageStatus();
                        CheckSelectedLanguage(submnuThai);
                        objFunction.SetLaguageInSettingForm("Thai");
                    }
                    catch (Exception ex)
                    {
                        ServerLog.Write("frmMainGTS::submnuSlovak_Click" + ex.ToString() + ex.StackTrace, true);
                        Logger.LogEx(ex, "frmMainGTS", "m_CmdBarsManager_CommandClicked(object sender, CommandEventArgs e) Thai");
                    }
                    break;
                #endregion

                #region Toolbar Menu(View Sub Menu)
                case "Standard":
                    objToolFunction.StandardToolbarState();
                    break;
                case "Charts":
                    objToolFunction.ChartToolbarState();
                    break;
                case "Line Studies":
                    objToolFunction.LineStudiesState();
                    break;
                case "Periodicity":
                    objToolFunction.PeriodicityToolbarState();
                    break;
                case "Account Toolbar":
                    objToolFunction.AccountToolbarState();
                    break;
                case "Analysis Toolbar":
                    objToolFunction.AnalysisToolbarState();
                    break;
                case "Connectivity Toolbar":
                    objToolFunction.ConnectivityToolbarState();
                    break;

                #endregion

                #region Account Open Sub menu
                case "Live Account":
                    objToolFunction.LiveAccount();
                    break;
                case "Sub Account":
                    objToolFunction.SubAccount();
                    break;
                case "Demo Account":
                    objToolFunction.DemoAccount();
                    break;
                #endregion

                #region Economic Calendar
                case "Economic Calendar":
                    objToolFunction.EconomicCalender();
                    break;
                #endregion

                #region Technical analysis (ADD sub menu)
                case "Horizontal Line":
                    objToolFunction.HorizontleLineDisplay();
                    break;
                case "Vertical Line":
                    objToolFunction.VerticalLineDisplay();
                    break;
                case "Text":
                    objToolFunction.TextDisplay();
                    break;
                case "Trend Lines":
                    objToolFunction.TrendLineDisplay();
                    break;
                case "Ellipse":
                    objToolFunction.EllipseDisplay();
                    break;
                case "Speed Lines":
                    objToolFunction.SpeedLineDisplay();
                    break;
                case "Gann Fan":
                    objToolFunction.GannFanDisplay();
                    break;
                case "Fibonacci Arc":
                    objToolFunction.FibonacciArcsDisplay();
                    break;
                case "Fibonacci Retracement":
                    objToolFunction.FibonacciRetracementDisplay();//Crash the Application
                    break;
                case "Fibonacci Fan":
                    objToolFunction.FibonacciFanDisplay();
                    break;
                case "Fibonacci TimeZone":
                    objToolFunction.FibonacciTimezoneDisplay();
                    break;
                case "Tirone Level":
                    objToolFunction.TironeLevelDisplay();
                    break;
                case "Quadrent lines":
                    objToolFunction.QuadrentLinesDisplay();
                    break;
                case "Raff Regression":
                    objToolFunction.RafRegressionDisplay();
                    break;
                case "Error channel":
                    objToolFunction.ErrorChannelDisplay();
                    break;
                case "Rectangle":
                    objToolFunction.RectangleDisplay();
                    break;
                case "Free hand drawing":
                    objToolFunction.FreeHandDrawingDisplay();
                    break;
                #endregion

                #region Snapshot
                case "Save":
                    objChartFunction.Snap_Save();
                    break;
                case "Print":
                    objChartFunction.SnapPrint();
                    break;
                #endregion

                #region Chart Price Type
                case "Point figure":
                    objChartFunction.PointFigDisplay();
                    break;
                case "Renko":
                    objChartFunction.RenkoDisplay();
                    break;
                case "Kagi":
                    objChartFunction.KagiDisplay();
                    break;
                case "Three line break":
                    objChartFunction.ThreeLineBreakDisplay();
                    break;
                case "Heikin Ashi":
                    objChartFunction.HeikinAshiDisplay();
                    break;
                case "Equivolume":
                    objChartFunction.EquiVolumeDisplay();
                    break;
                case "Equi volume shadow":
                    objChartFunction.EquiVolumeShadowDisplay();
                    break;
                case "Candle volume":
                    objChartFunction.CandleVolumeDisplay();
                    break;
                case "Standard Chart":
                    objChartFunction.StandardChartDisplay();
                    break;
                #endregion

                #region Chart Type
                case "Bar Chart":
                    objChartFunction.BarChartDisplay();
                    break;
                case "Candle Stick":
                    objChartFunction.CandlestickChartDisplay();
                    break;
                case "Line Charts":
                    objChartFunction.LineChartDisplay();
                    break;
                #endregion

                #region Chart Periodicity
                case "1 Minute":
                    objChartFunction.Period_1_min();
                    break;
                case "5 Minutes":
                    objChartFunction.Period_5_min();
                    break;
                case "15 Minutes":
                    objChartFunction.Period_15_min();
                    break;
                case "30 Minutes":
                    objChartFunction.Period_30_min();
                    break;
                case "1 Hour":
                    objChartFunction.Period_1_hour();
                    break;
                case "4 Hours":
                    objChartFunction.Period_4_hour();
                    break;
                case "Daily":
                    objChartFunction.Period_daily();
                    break;
                case "Weekly":
                    objChartFunction.Period_weekly();
                    break;
                case "Monthly":
                    objChartFunction.Period_Month();
                    break;
                #endregion

                #region Chart Toolbar Items
                case "Empty Bar":
                    break;
                case "Chart Shift":
                    objChartFunction.ChartShift();
                    break;
                case "Chart Autoscroll":
                    objChartFunction.ChartAutoScroll();
                    break;
                #endregion

                #region Standard toolbar Items                   
                case "Quotes":
                    objChartFunction.ShowSpecificChart(e.Command.ToString());
                    break;
                case "Close All":     
                    uctlTradeGrid.GetTradeGridInstance.CloseAll(false);
                    break;
                case "Cancel All":
                    uctlTradeGrid.GetTradeGridInstance.CancelAll(false);
                    break;
                #endregion

                #region Connectivity Toolbar Items
                case "Reconnect":
                    break;
                case "24x7":
                    break;
                case "Chat":
                    break;
                case "Settings":
                    objToolFunction.Option();
                    break;
                case "Mail":
                    frmAlertSendMail objSendMail = new frmAlertSendMail(uctlTerminal1.uctlMailGrid1);
                    objSendMail.DispalyItem("Re:Registration");
                    objSendMail.ShowDialog();
                    break;
                case "Quote Connection":
                    break;
                case "Order Connection":
                    break;
                #endregion

                #region Analysis Toolbar Item
                case "News Feed":
                    break;
                case "Galaxy Script Creator":
                    break;
                case "Wizard":
                    break;
                default:
                    break;

                #endregion
            }
            state.ResolveDocumentClient -= state_ResolveDocumentClient;
        }


        public void CheckSelectedLanguage(NCommand lang_Item)
        {
            lang_Item.Checked = true;
        }

        private void mnuFileLoadWorkspace_QueryUIState(object sender, QueryCommandUIStateEventArgs e)
        {

        }

        public void StatusBarUpdate(string Balance, string Equity, string UsedMargin,string AvailableMargin,string MarginLevelPercent,
                                    string LiquidationMargin, string OpenPL,string DailyGain,string UsableCapital,string Leverage)
        {
            Action a = () =>
                            StatusBarUpdate(Balance, Equity, UsedMargin, AvailableMargin,
                                            MarginLevelPercent, LiquidationMargin, OpenPL,
                                            DailyGain, UsableCapital, Leverage);
            if (InvokeRequired)
                this.Invoke(a);
            else
            {
                this.nStatusBarDailyClosedPanel.Visible = false;

                nStatusBarBalancePanel.Text = "Balance : " + Balance;
                nStatusBarEquityPanel.Text = "Equity : "+Equity;
                nStatusBarUsedMarginPanel.Text = "UsedMargin : " + UsedMargin;
                nStatusBarAvailableMarginPanel.Text = "Available Margin : " + AvailableMargin;
                nStatusBarMarginLevelPanel.Text = "Margin Level % " + MarginLevelPercent;
                nStatusBarLiquidation.Text = "Liquidation Margin : " + LiquidationMargin;
                nStatusBarOpenPLPanel.Text = "OpenPL : "+OpenPL;
                nStatusBarDailyGainsPanel.Text = "Daily Gain : " + DailyGain;
                nStatusBarUsableCapitalPanel.Text = "Usable Capital: " + UsableCapital;
                nstatusbarLeverage.Text = "Leverage : " + Leverage;
                if (string.IsNullOrEmpty(MarginLevelPercent))
                {
                    nStatusBarMarginLevelPanel.Visible = false;
                    this.nStatusBar.Refresh();
                }
                else
                {
                    nStatusBarMarginLevelPanel.Visible = true;
                }
                
            }
        }

        public void InsertLogInGrid(string message)
        {
             frmMainGTS.GetReference().uctlTerminal1.uctlLogInstance.LogMessage(DateTime.Now.ToString(), message);
        }
    }
}