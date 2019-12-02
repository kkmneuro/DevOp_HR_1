
using System.Windows.Forms;
using Nevron.UI.WinForm.Docking;
using Nevron.UI.WinForm.Controls;
namespace PALSA
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.m_DockManager = new Nevron.UI.WinForm.Docking.NDockManager(this.components);
            this.SymbolPanel = new Nevron.UI.WinForm.Docking.NDockingPanel();
            this.TerminalPanel = new Nevron.UI.WinForm.Docking.NDockingPanel();
            this.QuotePanel = new Nevron.UI.WinForm.Docking.NDockingPanel();
            this.Openpositions = new Nevron.UI.WinForm.Docking.NDockingPanel();
            this.OrderBook = new Nevron.UI.WinForm.Docking.NDockingPanel();
            this.PendingOrders = new Nevron.UI.WinForm.Docking.NDockingPanel();
            this.History = new Nevron.UI.WinForm.Docking.NDockingPanel();
            this.MailBox = new Nevron.UI.WinForm.Docking.NDockingPanel();
            this.Accounts = new Nevron.UI.WinForm.Docking.NDockingPanel();
            this.ui_ncbmPALSA = new Nevron.UI.WinForm.Controls.NCommandBarsManager(this.components);
            this.ui_nmnuBar = new Nevron.UI.WinForm.Controls.NMenuBar();
            this.ui_nmnuFile = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdFileLogin = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdFileCreateDemoAccount = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdFileLogOff = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdFileSaveWorkSpace = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdFileLoadWorkSpace = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdFileChangePassword = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdFileExit = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_nmnuView = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdViewLanguages = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdLanguagesEnglish = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdLanguagesHindi = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdLanguagesNepali = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdViewThemes = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmbThemeMacOS = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmbThemeOffice2007Black = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmbThemeOffice2007Blue = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdThemeOffice2007Aqua = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmbThemeOrange = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmbThemeVista = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdThemeOpusAlpha = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdThemeVistaPlus = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdThemeVistaRoyal = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdThemeVistaSlate = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdThemeInspirant = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdThemeSimple = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdThemeRoyal = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdThemeMoonlight = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdThemeAqua = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdThemeWood = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdThemeGreen = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdViewTicker = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdViewTrade = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdViewNetPosition = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdViewMsgLog = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdViewContractInfo = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdViewToolBar = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdViewFilterBar = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdViewMessageBar = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdViewIndexBar = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdViewAccountsInfo = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdViewStatusBar = new Nevron.UI.WinForm.Controls.NCommand();
            this.nmnuCmdViewTopStatusBar = new Nevron.UI.WinForm.Controls.NCommand();
            this.nmnuCmdViewMiddleStatusBar = new Nevron.UI.WinForm.Controls.NCommand();
            this.nmnuCmdViewBottomStatusBar = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdViewAdminMsgBar = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdViewIndicesView = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdViewParticipantList = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdViewFullScreen = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdViewExpertAdvisor = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdViewBackTest = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdViewScanner = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdPendingOrders = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdViewRadar = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_nmnuMarket = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdMarketMarketWatch = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdMarketQuote = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_nmnuOrders = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdOrdersPlaceOrder = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdOrdersOrderBook = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_nmnuTrades = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdTradesTrades = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_nmnuTools = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdToolsCustomize = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdToolsLockWorkStation = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdToolsPortfolio = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdToolsPreferences = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuCharts = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuChartsNewChart = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuChartsPeriodicity = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuPeriodicity1Minute = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuPeriodicity5Minute = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuPeriodicity15Minute = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuPeriodicity30Minute = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuPeriodicity1Hour = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuPeriodicityDaily = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuPeriodicityWeekly = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuPeriodicityMonthly = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuChartsChartType = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuChartTypeBarChart = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuChartTypeCandleChart = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuChartTypeLineChart = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuChartsPriceType = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_nmunPriceTypePointandFigure = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_nmunPriceTypeRenko = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_nmunPriceTypeKagi = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_nmunPriceTypeThreeLineBreak = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_nmunPriceTypeEquiVolume = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_nmunPriceTypeEquiVolumeShadow = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_nmunPriceTypeCandleVolume = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_nmunPriceTypeHeikinAshi = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_nmunPriceTypeStandardChart = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuChartsZoomIn = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuChartsZoomOut = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuChartsTrackCursor = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuChartsVolume = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuChartsGrid = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuChart3DStyle = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuChartsSnapshot = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuSnapshotPrint = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuSnapshotSave = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_nmnuTechnicalAnalysis = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_nmnuTechnicalAnalysisIndicatorList = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_nmnuTechnicalAnalysisAdd = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuAddHorizontalLine = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuAddVerticalLine = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuAddText = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuAddTrendLine = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuAddEllipse = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuAddSpeedLines = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuAddGannFan = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuAddFibonacciArcs = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuAddFibonacciRetracement = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuAddFibonacciFan = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuAddFibonacciTimezone = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuAddTironeLevel = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuAddQuadrentLines = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuAddRafRegression = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuAddErrorChannel = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuAddRectangle = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_mnuAddFreeHandDrawing = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_nmnuWindows = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdWindowNewWindow = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdWindowClose = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdWindowCloseAll = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdWindowCascade = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdWindowTileHorizontally = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdWindowTileVertically = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdWindowWindow = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_nmnuSurveillance = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_nmnuSurveillanceSurveillance = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_nmnuHelp = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_nmnuHelpAboutUs = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ndtToolBar = new Nevron.UI.WinForm.Controls.NDockingToolbar();
            this.ui_ntbLogin = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ntbLogoff = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ntbChangePassword = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ntbPrint = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ntbOrderBook = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ntbTrades = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ntbNetPosition = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ntbMarketWatch = new Nevron.UI.WinForm.Controls.NCommand();
            this.nCommand1 = new Nevron.UI.WinForm.Controls.NCommand();
            this.TrackCursor = new Nevron.UI.WinForm.Controls.NCommand();
            this.cmdVerticalLine = new Nevron.UI.WinForm.Controls.NCommand();
            this.cmdHoriLine = new Nevron.UI.WinForm.Controls.NCommand();
            this.cmdText = new Nevron.UI.WinForm.Controls.NCommand();
            this.TextLevel = new Nevron.UI.WinForm.Controls.NCommand();
            this.CrossHair = new Nevron.UI.WinForm.Controls.NCommand();
            this.Equidistance = new Nevron.UI.WinForm.Controls.NCommand();
            this.Fabiconn_arc = new Nevron.UI.WinForm.Controls.NCommand();
            this.Fabiconn_fan = new Nevron.UI.WinForm.Controls.NCommand();
            this.Fabiconn_retracement = new Nevron.UI.WinForm.Controls.NCommand();
            this.Gann_fan = new Nevron.UI.WinForm.Controls.NCommand();
            this.Grid = new Nevron.UI.WinForm.Controls.NCommand();
            this.Volume = new Nevron.UI.WinForm.Controls.NCommand();
            this.ZoomIn = new Nevron.UI.WinForm.Controls.NCommand();
            this.ZoomOut = new Nevron.UI.WinForm.Controls.NCommand();
            this.M1 = new Nevron.UI.WinForm.Controls.NCommand();
            this.M5 = new Nevron.UI.WinForm.Controls.NCommand();
            this.M15 = new Nevron.UI.WinForm.Controls.NCommand();
            this.M30 = new Nevron.UI.WinForm.Controls.NCommand();
            this.H1 = new Nevron.UI.WinForm.Controls.NCommand();
            this.D1 = new Nevron.UI.WinForm.Controls.NCommand();
            this.W1 = new Nevron.UI.WinForm.Controls.NCommand();
            this.MN = new Nevron.UI.WinForm.Controls.NCommand();
            this.AutoScroll = new Nevron.UI.WinForm.Controls.NCommand();
            this.ChartShift = new Nevron.UI.WinForm.Controls.NCommand();
            this.BarChart = new Nevron.UI.WinForm.Controls.NCommand();
            this.CandleChart = new Nevron.UI.WinForm.Controls.NCommand();
            this.LineChart = new Nevron.UI.WinForm.Controls.NCommand();
            this.cbIndicators = new Nevron.UI.WinForm.Controls.NComboBoxCommand();
            this.ui_ndtTicker = new Nevron.UI.WinForm.Controls.NDockingToolbar();
            this.ToolbarChart = new Nevron.UI.WinForm.Controls.NDockingToolbar();
            this.ui_ncmdMarketMarketPicture = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdMarketSnapQuote = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdMarketMarketStatus = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdMarketTopGainerLosers = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ntbBackup = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ntbMessageLog = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ntbOrderEntry = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ntbMarketPicture = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ntbContractInfo = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ntbModifyOrder = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ntbCancelOrder = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ntbCancelAllOrders = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ntbFilter = new Nevron.UI.WinForm.Controls.NCommand();
            this.nuiPnlMessageBar = new Nevron.UI.WinForm.Controls.NUIPanel();
            this.lblMessages = new System.Windows.Forms.Label();
            this.nSplitter1 = new Nevron.UI.WinForm.Controls.NSplitter();
            this.nuiPnlStatusBars = new Nevron.UI.WinForm.Controls.NUIPanel();
            this.nuiPanel4 = new Nevron.UI.WinForm.Controls.NUIPanel();
            this.lblBottomStatus1 = new System.Windows.Forms.Label();
            this.lblBottomStatus4 = new System.Windows.Forms.Label();
            this.lblBottomStatus2 = new System.Windows.Forms.Label();
            this.lblBottomStatus3 = new System.Windows.Forms.Label();
            this.nuiPanel3 = new Nevron.UI.WinForm.Controls.NUIPanel();
            this.lblMiddleStatus1 = new System.Windows.Forms.Label();
            this.lblMiddleStatus2 = new System.Windows.Forms.Label();
            this.lblMiddleStatus3 = new System.Windows.Forms.Label();
            this.lblMiddleStatus4 = new System.Windows.Forms.Label();
            this.nuiPanel2 = new Nevron.UI.WinForm.Controls.NUIPanel();
            this.ui_lblTopStatus1 = new System.Windows.Forms.Label();
            this.lblTopStatus2 = new System.Windows.Forms.Label();
            this.lblTopStatus4 = new System.Windows.Forms.Label();
            this.lblTopStatus3 = new System.Windows.Forms.Label();
            this.nuiPanel1 = new Nevron.UI.WinForm.Controls.NUIPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.ui_tmrLockWorkstation = new System.Windows.Forms.Timer(this.components);
            this.ui_tmrTicker = new System.Windows.Forms.Timer(this.components);
            this.ui_lblNepalTime = new System.Windows.Forms.Label();
            this.ui_npnlHeader = new Nevron.UI.WinForm.Controls.NUIPanel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tlstrplblStatusMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.tlsOrderServerStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tlsDataServerStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.Alerts = new Nevron.UI.WinForm.Docking.NDockingPanel();
            this.Scanner = new Nevron.UI.WinForm.Docking.NDockingPanel();
            this.Radar = new Nevron.UI.WinForm.Docking.NDockingPanel();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.m_DockManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ui_ncbmPALSA)).BeginInit();
            this.nuiPnlMessageBar.SuspendLayout();
            this.nuiPnlStatusBars.SuspendLayout();
            this.nuiPanel4.SuspendLayout();
            this.nuiPanel3.SuspendLayout();
            this.nuiPanel2.SuspendLayout();
            this.nuiPanel1.SuspendLayout();
            this.ui_npnlHeader.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_DockManager
            // 
            this.m_DockManager.CaptionStyle.AutomaticHeight = false;
            this.m_DockManager.CaptionStyle.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_DockManager.CaptionStyle.GripperVisibility = Nevron.UI.WinForm.Controls.GripperVisibility.Hide;
            this.m_DockManager.CaptionStyle.Height = 20;
            this.m_DockManager.DockingHintStyle.StickerTemplateIndex = 3;
            this.m_DockManager.DocumentStyle.DocumentBorderStyle = Nevron.UI.BorderStyle3D.Flat;
            this.m_DockManager.DocumentStyle.DocumentViewBorderStyle = Nevron.UI.BorderStyle3D.Flat;
            this.m_DockManager.DocumentStyle.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_DockManager.Form = this;
            this.m_DockManager.GroupBorderStyle = Nevron.UI.BorderStyle3D.Flat;
            this.m_DockManager.Palette.Scheme = Nevron.UI.WinForm.Controls.ColorScheme.LunaSilver;
            this.m_DockManager.RootContainerZIndex = 1;
            this.m_DockManager.StickToMainForm = false;
            this.m_DockManager.StickToWorkingArea = false;
            this.m_DockManager.StickyFloatingFrames = false;
            this.m_DockManager.StickyOptions.StickyEdges = Nevron.UI.Edges.None;
            this.m_DockManager.TabStyle.AutomaticTabStyle = false;
            this.m_DockManager.TabStyle.TabStyle = Nevron.UI.WinForm.Controls.TabStyle.Standard;
            this.m_DockManager.UndockToleranceSize = 2;
            //  
            // Root Zone
            //  
            this.m_DockManager.RootContainer.RootZone.Orientation = System.Windows.Forms.Orientation.Vertical;
            // 
            // SymbolPanel
            // 
            this.SymbolPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SymbolPanel.Key = "symbolkey";
            this.SymbolPanel.Location = new System.Drawing.Point(1, 19);
            this.SymbolPanel.Name = "SymbolPanel";
            this.SymbolPanel.Size = new System.Drawing.Size(194, 247);
            this.SymbolPanel.TabIndex = 1;
            this.SymbolPanel.Text = "Market Watch";
            // 
            // TerminalPanel
            // 
            this.TerminalPanel.Location = new System.Drawing.Point(0, 0);
            this.TerminalPanel.Name = "TerminalPanel";
            this.TerminalPanel.Size = new System.Drawing.Size(200, 200);
            this.TerminalPanel.TabIndex = 0;
            this.TerminalPanel.Text = "Docking Panel";
            // 
            // QuotePanel
            // 
            this.QuotePanel.Caption.ImageIndex = 35;
            this.QuotePanel.Dock = System.Windows.Forms.DockStyle.None;
            this.QuotePanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuotePanel.Key = "QuoteKey";
            this.QuotePanel.Location = new System.Drawing.Point(1, 19);
            this.QuotePanel.Name = "QuotePanel";
            this.QuotePanel.PrefferedState = Nevron.UI.WinForm.Docking.DockState.Docked;
            this.QuotePanel.Size = new System.Drawing.Size(198, 224);
            this.QuotePanel.TabIndex = 2;
            this.QuotePanel.Text = "Quote";
            // 
            // Openpositions
            // 
            this.Openpositions.Dock = System.Windows.Forms.DockStyle.None;
            this.Openpositions.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Openpositions.Key = "OpenPositionkey";
            this.Openpositions.Location = new System.Drawing.Point(1, 19);
            this.Openpositions.Name = "Openpositions";
            this.Openpositions.Size = new System.Drawing.Size(848, 47);
            this.Openpositions.TabIndex = 4;
            this.Openpositions.Text = "Positions";
            // 
            // OrderBook
            // 
            this.OrderBook.Dock = System.Windows.Forms.DockStyle.None;
            this.OrderBook.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OrderBook.Key = "OrderBook";
            this.OrderBook.Location = new System.Drawing.Point(1, 19);
            this.OrderBook.Name = "OrderBook";
            this.OrderBook.Size = new System.Drawing.Size(271, 21);
            this.OrderBook.TabIndex = 5;
            this.OrderBook.Text = "Order Book";
            // 
            // PendingOrders
            // 
            this.PendingOrders.Dock = System.Windows.Forms.DockStyle.None;
            this.PendingOrders.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PendingOrders.Key = "PendingOrders";
            this.PendingOrders.Location = new System.Drawing.Point(1, 19);
            this.PendingOrders.Name = "PendingOrders";
            this.PendingOrders.Size = new System.Drawing.Size(271, 21);
            this.PendingOrders.TabIndex = 9;
            this.PendingOrders.Text = "Pending Orders";
            // 
            // History
            // 
            this.History.Dock = System.Windows.Forms.DockStyle.None;
            this.History.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.History.Key = "History";
            this.History.Location = new System.Drawing.Point(1, 19);
            this.History.Name = "History";
            this.History.Size = new System.Drawing.Size(271, 21);
            this.History.TabIndex = 6;
            this.History.Text = "History";
            // 
            // MailBox
            // 
            this.MailBox.Dock = System.Windows.Forms.DockStyle.None;
            this.MailBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MailBox.Key = "MailBox";
            this.MailBox.Location = new System.Drawing.Point(1, 19);
            this.MailBox.Name = "MailBox";
            this.MailBox.Size = new System.Drawing.Size(271, 21);
            this.MailBox.TabIndex = 8;
            this.MailBox.Text = "Mail Box";
            // 
            // Accounts
            // 
            this.Accounts.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Accounts.Key = "Accounts";
            this.Accounts.Location = new System.Drawing.Point(1, 19);
            this.Accounts.Name = "Accounts";
            this.Accounts.Size = new System.Drawing.Size(271, 21);
            this.Accounts.TabIndex = 7;
            this.Accounts.Text = "Accounts";
            // 
            // ui_ncbmPALSA
            // 
            this.ui_ncbmPALSA.EditorConfig.HasMenuOptionsButton = false;
            this.ui_ncbmPALSA.MenuBarMnemonicsVisibility = Nevron.UI.WinForm.Controls.MenuBarMnemonicsVisibility.Hide;
            this.ui_ncbmPALSA.ParentControl = this;
            this.ui_ncbmPALSA.Toolbars.Add(this.ui_nmnuBar);
            this.ui_ncbmPALSA.Toolbars.Add(this.ui_ndtToolBar);
            this.ui_ncbmPALSA.Toolbars.Add(this.ui_ndtTicker);
            this.ui_ncbmPALSA.Toolbars.Add(this.ToolbarChart);
            this.ui_ncbmPALSA.CommandClicked += new Nevron.UI.WinForm.Controls.CommandEventHandler(this.ui_ncbmPALSA_CommandClicked);
            // 
            // ui_nmnuBar
            // 
            this.ui_nmnuBar.AllowDelete = false;
            this.ui_nmnuBar.AllowHide = false;
            this.ui_nmnuBar.AllowRename = false;
            this.ui_nmnuBar.AllowReset = false;
            this.ui_nmnuBar.AutoDropDownDelay = false;
            this.ui_nmnuBar.BackgroundType = Nevron.UI.WinForm.Controls.BackgroundType.Transparent;
            this.ui_nmnuBar.CanFloat = false;
            this.ui_nmnuBar.Commands.AddRange(new Nevron.UI.WinForm.Controls.NCommand[] {
            this.ui_nmnuFile,
            this.ui_nmnuView,
            this.ui_nmnuMarket,
            this.ui_nmnuOrders,
            this.ui_nmnuTrades,
            this.ui_nmnuTools,
            this.ui_mnuCharts,
            this.ui_nmnuTechnicalAnalysis,
            this.ui_nmnuWindows,
            this.ui_nmnuSurveillance,
            this.ui_nmnuHelp});
            this.ui_nmnuBar.DefaultCommandStyle = Nevron.UI.WinForm.Controls.CommandStyle.Text;
            this.ui_nmnuBar.DefaultLocation = new System.Drawing.Point(0, 0);
            this.ui_nmnuBar.HasGripper = false;
            this.ui_nmnuBar.HasPendantCommand = false;
            this.ui_nmnuBar.Name = "ui_nmnuBar";
            this.ui_nmnuBar.Palette.BlendStyle = Nevron.UI.BlendStyle.Glass;
            this.ui_nmnuBar.PrefferedRowIndex = 0;
            this.ui_nmnuBar.RowIndex = 0;
            this.ui_nmnuBar.ShowTooltips = false;
            this.ui_nmnuBar.Text = "Menu Bar";
            // 
            // ui_nmnuFile
            // 
            this.ui_nmnuFile.Commands.AddRange(new Nevron.UI.WinForm.Controls.NCommand[] {
            this.ui_ncmdFileLogin,
            this.ui_ncmdFileCreateDemoAccount,
            this.ui_ncmdFileLogOff,
            this.ui_ncmdFileSaveWorkSpace,
            this.ui_ncmdFileLoadWorkSpace,
            this.ui_ncmdFileChangePassword,
            this.ui_ncmdFileExit});
            this.ui_nmnuFile.Properties.ShowArrowStyle = Nevron.UI.WinForm.Controls.ShowArrowStyle.Never;
            this.ui_nmnuFile.Properties.Text = "&File";
            // 
            // ui_ncmdFileLogin
            // 
            this.ui_ncmdFileLogin.Properties.ID = 0;
            this.ui_ncmdFileLogin.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(76, 131072);
            this.ui_ncmdFileLogin.Properties.Text = "Login";
            // 
            // ui_ncmdFileCreateDemoAccount
            // 
            this.ui_ncmdFileCreateDemoAccount.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(76, 131072);
            this.ui_ncmdFileCreateDemoAccount.Properties.Text = "Create Demo Account";
            // 
            // ui_ncmdFileLogOff
            // 
            this.ui_ncmdFileLogOff.Properties.ID = 1;
            this.ui_ncmdFileLogOff.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(79, 131072);
            this.ui_ncmdFileLogOff.Properties.Text = "Logoff";
            // 
            // ui_ncmdFileSaveWorkSpace
            // 
            this.ui_ncmdFileSaveWorkSpace.Properties.ID = 3;
            this.ui_ncmdFileSaveWorkSpace.Properties.Text = "Save Workspace";
            // 
            // ui_ncmdFileLoadWorkSpace
            // 
            this.ui_ncmdFileLoadWorkSpace.Properties.ID = 2;
            this.ui_ncmdFileLoadWorkSpace.Properties.Text = "Load Workspace";
            // 
            // ui_ncmdFileChangePassword
            // 
            this.ui_ncmdFileChangePassword.Properties.Text = "Change Password";
            // 
            // ui_ncmdFileExit
            // 
            this.ui_ncmdFileExit.Properties.ID = 4;
            this.ui_ncmdFileExit.Properties.Text = "Exit";
            this.ui_ncmdFileExit.Click += new Nevron.UI.WinForm.Controls.CommandEventHandler(this.nmnuCmdExit_Click);
            // 
            // ui_nmnuView
            // 
            this.ui_nmnuView.Commands.AddRange(new Nevron.UI.WinForm.Controls.NCommand[] {
            this.ui_ncmdViewLanguages,
            this.ui_ncmdViewThemes,
            this.ui_ncmdViewTicker,
            this.ui_ncmdViewTrade,
            this.ui_ncmdViewNetPosition,
            this.ui_ncmdViewMsgLog,
            this.ui_ncmdViewContractInfo,
            this.ui_ncmdViewToolBar,
            this.ui_ncmdViewFilterBar,
            this.ui_ncmdViewMessageBar,
            this.ui_ncmdViewIndexBar,
            this.ui_ncmdViewAccountsInfo,
            this.ui_ncmdViewStatusBar,
            this.ui_ncmdViewAdminMsgBar,
            this.ui_ncmdViewIndicesView,
            this.ui_ncmdViewParticipantList,
            this.ui_ncmdViewFullScreen,
            this.ui_ncmdViewExpertAdvisor,
            this.ui_ncmdViewBackTest,
            this.ui_ncmdViewScanner,
            this.ui_ncmdPendingOrders,
            this.ui_ncmdViewRadar});
            this.ui_nmnuView.Properties.ShowArrowStyle = Nevron.UI.WinForm.Controls.ShowArrowStyle.Never;
            this.ui_nmnuView.Properties.Text = "&View";
            // 
            // ui_ncmdViewLanguages
            // 
            this.ui_ncmdViewLanguages.Commands.AddRange(new Nevron.UI.WinForm.Controls.NCommand[] {
            this.ui_ncmdLanguagesEnglish,
            this.ui_ncmdLanguagesHindi,
            this.ui_ncmdLanguagesNepali});
            this.ui_ncmdViewLanguages.Properties.ID = 41;
            this.ui_ncmdViewLanguages.Properties.Text = "Languages";
            this.ui_ncmdViewLanguages.Properties.Visible = false;
            // 
            // ui_ncmdLanguagesEnglish
            // 
            this.ui_ncmdLanguagesEnglish.Properties.Text = "English";
            // 
            // ui_ncmdLanguagesHindi
            // 
            this.ui_ncmdLanguagesHindi.Properties.Text = "Hindi";
            this.ui_ncmdLanguagesHindi.Properties.Visible = false;
            // 
            // ui_ncmdLanguagesNepali
            // 
            this.ui_ncmdLanguagesNepali.Properties.Text = "Nepali";
            // 
            // ui_ncmdViewThemes
            // 
            this.ui_ncmdViewThemes.Commands.AddRange(new Nevron.UI.WinForm.Controls.NCommand[] {
            this.ui_ncmbThemeMacOS,
            this.ui_ncmbThemeOffice2007Black,
            this.ui_ncmbThemeOffice2007Blue,
            this.ui_ncmdThemeOffice2007Aqua,
            this.ui_ncmbThemeOrange,
            this.ui_ncmbThemeVista,
            this.ui_ncmdThemeOpusAlpha,
            this.ui_ncmdThemeVistaPlus,
            this.ui_ncmdThemeVistaRoyal,
            this.ui_ncmdThemeVistaSlate,
            this.ui_ncmdThemeInspirant,
            this.ui_ncmdThemeSimple,
            this.ui_ncmdThemeRoyal,
            this.ui_ncmdThemeMoonlight,
            this.ui_ncmdThemeAqua,
            this.ui_ncmdThemeWood,
            this.ui_ncmdThemeGreen});
            this.ui_ncmdViewThemes.Properties.Text = "Themes";
            this.ui_ncmdViewThemes.Properties.Visible = false;
            // 
            // ui_ncmbThemeMacOS
            // 
            this.ui_ncmbThemeMacOS.Properties.Text = "MacOS";
            // 
            // ui_ncmbThemeOffice2007Black
            // 
            this.ui_ncmbThemeOffice2007Black.Properties.Text = "Office 2007 Black";
            // 
            // ui_ncmbThemeOffice2007Blue
            // 
            this.ui_ncmbThemeOffice2007Blue.Properties.Text = "Office 2007 Blue";
            // 
            // ui_ncmdThemeOffice2007Aqua
            // 
            this.ui_ncmdThemeOffice2007Aqua.Properties.Text = "Office 2007 Aqua";
            // 
            // ui_ncmbThemeOrange
            // 
            this.ui_ncmbThemeOrange.Properties.Text = "Orange";
            // 
            // ui_ncmbThemeVista
            // 
            this.ui_ncmbThemeVista.Properties.Text = "Vista";
            // 
            // ui_ncmdThemeOpusAlpha
            // 
            this.ui_ncmdThemeOpusAlpha.Properties.Text = "Opus Alpha";
            // 
            // ui_ncmdThemeVistaPlus
            // 
            this.ui_ncmdThemeVistaPlus.Properties.Text = "VistaPlus";
            // 
            // ui_ncmdThemeVistaRoyal
            // 
            this.ui_ncmdThemeVistaRoyal.Properties.Text = "Vista Royal";
            // 
            // ui_ncmdThemeVistaSlate
            // 
            this.ui_ncmdThemeVistaSlate.Properties.Text = "Vista Slate";
            // 
            // ui_ncmdThemeInspirant
            // 
            this.ui_ncmdThemeInspirant.Properties.Text = "Inspirant";
            // 
            // ui_ncmdThemeSimple
            // 
            this.ui_ncmdThemeSimple.Properties.Text = "Simple";
            // 
            // ui_ncmdThemeRoyal
            // 
            this.ui_ncmdThemeRoyal.Properties.Text = "Royal";
            this.ui_ncmdThemeRoyal.Properties.Visible = false;
            // 
            // ui_ncmdThemeMoonlight
            // 
            this.ui_ncmdThemeMoonlight.Properties.Text = "Moon Light";
            this.ui_ncmdThemeMoonlight.Properties.Visible = false;
            // 
            // ui_ncmdThemeAqua
            // 
            this.ui_ncmdThemeAqua.Properties.Text = "Aqua";
            this.ui_ncmdThemeAqua.Properties.Visible = false;
            // 
            // ui_ncmdThemeWood
            // 
            this.ui_ncmdThemeWood.Properties.Text = "Wood";
            // 
            // ui_ncmdThemeGreen
            // 
            this.ui_ncmdThemeGreen.Properties.Text = "Green";
            // 
            // ui_ncmdViewTicker
            // 
            this.ui_ncmdViewTicker.Properties.ID = 5;
            this.ui_ncmdViewTicker.Properties.MenuOptions.DisplayTooltips = true;
            this.ui_ncmdViewTicker.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(115, 196608);
            this.ui_ncmdViewTicker.Properties.Text = "Ticker";
            this.ui_ncmdViewTicker.Properties.Visible = false;
            // 
            // ui_ncmdViewTrade
            // 
            this.ui_ncmdViewTrade.Properties.ID = 5;
            this.ui_ncmdViewTrade.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(119, 0);
            this.ui_ncmdViewTrade.Properties.Text = "History";
            // 
            // ui_ncmdViewNetPosition
            // 
            this.ui_ncmdViewNetPosition.Properties.ID = 7;
            this.ui_ncmdViewNetPosition.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(117, 131072);
            this.ui_ncmdViewNetPosition.Properties.Text = "Net Position";
            this.ui_ncmdViewNetPosition.Properties.Visible = false;
            // 
            // ui_ncmdViewMsgLog
            // 
            this.ui_ncmdViewMsgLog.Properties.ID = 8;
            this.ui_ncmdViewMsgLog.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(121, 0);
            this.ui_ncmdViewMsgLog.Properties.Text = "Message Log";
            this.ui_ncmdViewMsgLog.Properties.Visible = false;
            // 
            // ui_ncmdViewContractInfo
            // 
            this.ui_ncmdViewContractInfo.Properties.ID = 9;
            this.ui_ncmdViewContractInfo.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(119, 65536);
            this.ui_ncmdViewContractInfo.Properties.Text = "Contract Information";
            this.ui_ncmdViewContractInfo.Properties.Visible = false;
            // 
            // ui_ncmdViewToolBar
            // 
            this.ui_ncmdViewToolBar.Checked = true;
            this.ui_ncmdViewToolBar.Properties.ID = 10;
            this.ui_ncmdViewToolBar.Properties.Text = "Toolbar";
            this.ui_ncmdViewToolBar.Properties.Visible = false;
            // 
            // ui_ncmdViewFilterBar
            // 
            this.ui_ncmdViewFilterBar.Checked = true;
            this.ui_ncmdViewFilterBar.Properties.ID = 11;
            this.ui_ncmdViewFilterBar.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(83, 131072);
            this.ui_ncmdViewFilterBar.Properties.Text = "Filter Bar";
            this.ui_ncmdViewFilterBar.Properties.Visible = false;
            // 
            // ui_ncmdViewMessageBar
            // 
            this.ui_ncmdViewMessageBar.Checked = true;
            this.ui_ncmdViewMessageBar.Properties.ID = 12;
            this.ui_ncmdViewMessageBar.Properties.Text = "Message Bar";
            this.ui_ncmdViewMessageBar.Properties.Visible = false;
            // 
            // ui_ncmdViewIndexBar
            // 
            this.ui_ncmdViewIndexBar.Properties.Text = "Index Bar";
            this.ui_ncmdViewIndexBar.Properties.Visible = false;
            // 
            // ui_ncmdViewAccountsInfo
            // 
            this.ui_ncmdViewAccountsInfo.Properties.Text = "Accounts Info";
            this.ui_ncmdViewAccountsInfo.Properties.Visible = false;
            // 
            // ui_ncmdViewStatusBar
            // 
            this.ui_ncmdViewStatusBar.Commands.AddRange(new Nevron.UI.WinForm.Controls.NCommand[] {
            this.nmnuCmdViewTopStatusBar,
            this.nmnuCmdViewMiddleStatusBar,
            this.nmnuCmdViewBottomStatusBar});
            this.ui_ncmdViewStatusBar.Enabled = false;
            this.ui_ncmdViewStatusBar.Properties.ID = 13;
            this.ui_ncmdViewStatusBar.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(83, 196608);
            this.ui_ncmdViewStatusBar.Properties.Text = "Status Bar";
            this.ui_ncmdViewStatusBar.Properties.Visible = false;
            // 
            // nmnuCmdViewTopStatusBar
            // 
            this.nmnuCmdViewTopStatusBar.Enabled = false;
            this.nmnuCmdViewTopStatusBar.Properties.ID = 14;
            this.nmnuCmdViewTopStatusBar.Properties.Text = "Top Status Bar";
            // 
            // nmnuCmdViewMiddleStatusBar
            // 
            this.nmnuCmdViewMiddleStatusBar.Enabled = false;
            this.nmnuCmdViewMiddleStatusBar.Properties.ID = 15;
            this.nmnuCmdViewMiddleStatusBar.Properties.Text = "Middle Status Bar";
            // 
            // nmnuCmdViewBottomStatusBar
            // 
            this.nmnuCmdViewBottomStatusBar.Enabled = false;
            this.nmnuCmdViewBottomStatusBar.Properties.ID = 16;
            this.nmnuCmdViewBottomStatusBar.Properties.Text = "Bottom Status Bar";
            // 
            // ui_ncmdViewAdminMsgBar
            // 
            this.ui_ncmdViewAdminMsgBar.Properties.ID = 17;
            this.ui_ncmdViewAdminMsgBar.Properties.Text = "Admin Message Bar";
            this.ui_ncmdViewAdminMsgBar.Properties.Visible = false;
            // 
            // ui_ncmdViewIndicesView
            // 
            this.ui_ncmdViewIndicesView.Properties.ID = 18;
            this.ui_ncmdViewIndicesView.Properties.Text = "Indices View";
            this.ui_ncmdViewIndicesView.Properties.Visible = false;
            // 
            // ui_ncmdViewParticipantList
            // 
            this.ui_ncmdViewParticipantList.Properties.Text = "Participant List";
            this.ui_ncmdViewParticipantList.Properties.Visible = false;
            // 
            // ui_ncmdViewFullScreen
            // 
            this.ui_ncmdViewFullScreen.Properties.ID = 19;
            this.ui_ncmdViewFullScreen.Properties.Text = "Full Screen";
            // 
            // ui_ncmdViewExpertAdvisor
            // 
            this.ui_ncmdViewExpertAdvisor.Properties.Text = "ExpertAdvisor";
            this.ui_ncmdViewExpertAdvisor.Properties.TooltipText = "ExpertAdvisor";
            this.ui_ncmdViewExpertAdvisor.Click += new Nevron.UI.WinForm.Controls.CommandEventHandler(this.ui_ncmdViewExpertAdvisor_Click);
            // 
            // ui_ncmdViewBackTest
            // 
            this.ui_ncmdViewBackTest.Properties.Text = "BackTest";
            this.ui_ncmdViewBackTest.Properties.TooltipText = "BackTest";
            this.ui_ncmdViewBackTest.Click += new Nevron.UI.WinForm.Controls.CommandEventHandler(this.ui_ncmdViewBackTest_Click);
            // 
            // ui_ncmdViewScanner
            // 
            this.ui_ncmdViewScanner.Properties.Text = "Scanner";
            // 
            // ui_ncmdPendingOrders
            // 
            this.ui_ncmdPendingOrders.Properties.Text = "Pending Orders";
            // 
            // ui_ncmdViewRadar
            // 
            this.ui_ncmdViewRadar.Properties.Text = "Radar";
            // 
            // ui_nmnuMarket
            // 
            this.ui_nmnuMarket.Commands.AddRange(new Nevron.UI.WinForm.Controls.NCommand[] {
            this.ui_ncmdMarketMarketWatch,
            this.ui_ncmdMarketQuote});
            this.ui_nmnuMarket.Properties.ShowArrowStyle = Nevron.UI.WinForm.Controls.ShowArrowStyle.Never;
            this.ui_nmnuMarket.Properties.Text = "&Market";
            // 
            // ui_ncmdMarketMarketWatch
            // 
            this.ui_ncmdMarketMarketWatch.Properties.ID = 20;
            this.ui_ncmdMarketMarketWatch.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(115, 0);
            this.ui_ncmdMarketMarketWatch.Properties.Text = "Market Watch";
            // 
            // ui_ncmdMarketQuote
            // 
            this.ui_ncmdMarketQuote.Enabled = false;
            this.ui_ncmdMarketQuote.Properties.Text = "Market Quote";
            // 
            // ui_nmnuOrders
            // 
            this.ui_nmnuOrders.Commands.AddRange(new Nevron.UI.WinForm.Controls.NCommand[] {
            this.ui_ncmdOrdersPlaceOrder,
            this.ui_ncmdOrdersOrderBook});
            this.ui_nmnuOrders.Properties.ShowArrowStyle = Nevron.UI.WinForm.Controls.ShowArrowStyle.Never;
            this.ui_nmnuOrders.Properties.Text = "&Orders";
            // 
            // ui_ncmdOrdersPlaceOrder
            // 
            this.ui_ncmdOrdersPlaceOrder.Properties.ID = 25;
            this.ui_ncmdOrdersPlaceOrder.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(112, 0);
            this.ui_ncmdOrdersPlaceOrder.Properties.Text = "Place Order";
            this.ui_ncmdOrdersPlaceOrder.Properties.Visible = false;
            // 
            // ui_ncmdOrdersOrderBook
            // 
            this.ui_ncmdOrdersOrderBook.Properties.ID = 27;
            this.ui_ncmdOrdersOrderBook.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(114, 0);
            this.ui_ncmdOrdersOrderBook.Properties.Text = "Order Book";
            // 
            // ui_nmnuTrades
            // 
            this.ui_nmnuTrades.Commands.AddRange(new Nevron.UI.WinForm.Controls.NCommand[] {
            this.ui_ncmdTradesTrades});
            this.ui_nmnuTrades.Properties.ShowArrowStyle = Nevron.UI.WinForm.Controls.ShowArrowStyle.Never;
            this.ui_nmnuTrades.Properties.Text = "&History";
            // 
            // ui_ncmdTradesTrades
            // 
            this.ui_ncmdTradesTrades.Properties.ID = 28;
            this.ui_ncmdTradesTrades.Properties.Text = "History";
            // 
            // ui_nmnuTools
            // 
            this.ui_nmnuTools.Commands.AddRange(new Nevron.UI.WinForm.Controls.NCommand[] {
            this.ui_ncmdToolsCustomize,
            this.ui_ncmdToolsLockWorkStation,
            this.ui_ncmdToolsPortfolio,
            this.ui_ncmdToolsPreferences});
            this.ui_nmnuTools.Properties.ShowArrowStyle = Nevron.UI.WinForm.Controls.ShowArrowStyle.Never;
            this.ui_nmnuTools.Properties.Text = "Too&ls";
            // 
            // ui_ncmdToolsCustomize
            // 
            this.ui_ncmdToolsCustomize.Properties.ID = 29;
            this.ui_ncmdToolsCustomize.Properties.Text = "Customize";
            this.ui_ncmdToolsCustomize.Properties.Visible = false;
            // 
            // ui_ncmdToolsLockWorkStation
            // 
            this.ui_ncmdToolsLockWorkStation.Properties.ID = 30;
            this.ui_ncmdToolsLockWorkStation.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(87, 131072);
            this.ui_ncmdToolsLockWorkStation.Properties.Text = "Lock Workstation";
            // 
            // ui_ncmdToolsPortfolio
            // 
            this.ui_ncmdToolsPortfolio.Properties.ID = 31;
            this.ui_ncmdToolsPortfolio.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(114, 262144);
            this.ui_ncmdToolsPortfolio.Properties.Text = "Portfolio";
            // 
            // ui_ncmdToolsPreferences
            // 
            this.ui_ncmdToolsPreferences.Properties.ID = 32;
            this.ui_ncmdToolsPreferences.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(82, 131072);
            this.ui_ncmdToolsPreferences.Properties.Text = "Preferences";
            // 
            // ui_mnuCharts
            // 
            this.ui_mnuCharts.Commands.AddRange(new Nevron.UI.WinForm.Controls.NCommand[] {
            this.ui_mnuChartsNewChart,
            this.ui_mnuChartsPeriodicity,
            this.ui_mnuChartsChartType,
            this.ui_mnuChartsPriceType,
            this.ui_mnuChartsZoomIn,
            this.ui_mnuChartsZoomOut,
            this.ui_mnuChartsTrackCursor,
            this.ui_mnuChartsVolume,
            this.ui_mnuChartsGrid,
            this.ui_mnuChart3DStyle,
            this.ui_mnuChartsSnapshot});
            this.ui_mnuCharts.Properties.ShowArrowStyle = Nevron.UI.WinForm.Controls.ShowArrowStyle.Never;
            this.ui_mnuCharts.Properties.Text = "Charts";
            // 
            // ui_mnuChartsNewChart
            // 
            this.ui_mnuChartsNewChart.Properties.Text = "New Chart";
            // 
            // ui_mnuChartsPeriodicity
            // 
            this.ui_mnuChartsPeriodicity.Commands.AddRange(new Nevron.UI.WinForm.Controls.NCommand[] {
            this.ui_mnuPeriodicity1Minute,
            this.ui_mnuPeriodicity5Minute,
            this.ui_mnuPeriodicity15Minute,
            this.ui_mnuPeriodicity30Minute,
            this.ui_mnuPeriodicity1Hour,
            this.ui_mnuPeriodicityDaily,
            this.ui_mnuPeriodicityWeekly,
            this.ui_mnuPeriodicityMonthly});
            this.ui_mnuChartsPeriodicity.Properties.Text = "Periodicity";
            // 
            // ui_mnuPeriodicity1Minute
            // 
            this.ui_mnuPeriodicity1Minute.Checked = true;
            this.ui_mnuPeriodicity1Minute.Properties.Text = "1 Minute";
            // 
            // ui_mnuPeriodicity5Minute
            // 
            this.ui_mnuPeriodicity5Minute.Properties.Text = "5 Minute";
            // 
            // ui_mnuPeriodicity15Minute
            // 
            this.ui_mnuPeriodicity15Minute.Properties.Text = "15 Minute";
            // 
            // ui_mnuPeriodicity30Minute
            // 
            this.ui_mnuPeriodicity30Minute.Properties.Text = "30 Minute";
            // 
            // ui_mnuPeriodicity1Hour
            // 
            this.ui_mnuPeriodicity1Hour.Enabled = false;
            this.ui_mnuPeriodicity1Hour.Properties.Text = "1 Hour";
            // 
            // ui_mnuPeriodicityDaily
            // 
            this.ui_mnuPeriodicityDaily.Enabled = false;
            this.ui_mnuPeriodicityDaily.Properties.Text = "Daily";
            // 
            // ui_mnuPeriodicityWeekly
            // 
            this.ui_mnuPeriodicityWeekly.Enabled = false;
            this.ui_mnuPeriodicityWeekly.Properties.Text = "Weekly";
            // 
            // ui_mnuPeriodicityMonthly
            // 
            this.ui_mnuPeriodicityMonthly.Enabled = false;
            this.ui_mnuPeriodicityMonthly.Properties.Text = "Monthly";
            // 
            // ui_mnuChartsChartType
            // 
            this.ui_mnuChartsChartType.Commands.AddRange(new Nevron.UI.WinForm.Controls.NCommand[] {
            this.ui_mnuChartTypeBarChart,
            this.ui_mnuChartTypeCandleChart,
            this.ui_mnuChartTypeLineChart});
            this.ui_mnuChartsChartType.Properties.Text = "Chart Type";
            // 
            // ui_mnuChartTypeBarChart
            // 
            this.ui_mnuChartTypeBarChart.Properties.Text = "Bar Chart";
            // 
            // ui_mnuChartTypeCandleChart
            // 
            this.ui_mnuChartTypeCandleChart.Checked = true;
            this.ui_mnuChartTypeCandleChart.Properties.Text = "Candle Chart";
            // 
            // ui_mnuChartTypeLineChart
            // 
            this.ui_mnuChartTypeLineChart.Properties.Text = "Line Chart";
            // 
            // ui_mnuChartsPriceType
            // 
            this.ui_mnuChartsPriceType.Commands.AddRange(new Nevron.UI.WinForm.Controls.NCommand[] {
            this.ui_nmunPriceTypePointandFigure,
            this.ui_nmunPriceTypeRenko,
            this.ui_nmunPriceTypeKagi,
            this.ui_nmunPriceTypeThreeLineBreak,
            this.ui_nmunPriceTypeEquiVolume,
            this.ui_nmunPriceTypeEquiVolumeShadow,
            this.ui_nmunPriceTypeCandleVolume,
            this.ui_nmunPriceTypeHeikinAshi,
            this.ui_nmunPriceTypeStandardChart});
            this.ui_mnuChartsPriceType.Properties.Text = "Price Type";
            // 
            // ui_nmunPriceTypePointandFigure
            // 
            this.ui_nmunPriceTypePointandFigure.Properties.Text = "Point and Figure";
            // 
            // ui_nmunPriceTypeRenko
            // 
            this.ui_nmunPriceTypeRenko.Properties.Text = "Renko";
            // 
            // ui_nmunPriceTypeKagi
            // 
            this.ui_nmunPriceTypeKagi.Properties.Text = "Kagi";
            // 
            // ui_nmunPriceTypeThreeLineBreak
            // 
            this.ui_nmunPriceTypeThreeLineBreak.Properties.Text = "Three Line Break";
            // 
            // ui_nmunPriceTypeEquiVolume
            // 
            this.ui_nmunPriceTypeEquiVolume.Properties.Text = "Equi Volume";
            // 
            // ui_nmunPriceTypeEquiVolumeShadow
            // 
            this.ui_nmunPriceTypeEquiVolumeShadow.Properties.Text = "Equi Volume Shadow";
            // 
            // ui_nmunPriceTypeCandleVolume
            // 
            this.ui_nmunPriceTypeCandleVolume.Properties.Text = "Candle Volume";
            // 
            // ui_nmunPriceTypeHeikinAshi
            // 
            this.ui_nmunPriceTypeHeikinAshi.Properties.Text = "Heikin Ashi";
            // 
            // ui_nmunPriceTypeStandardChart
            // 
            this.ui_nmunPriceTypeStandardChart.Checked = true;
            this.ui_nmunPriceTypeStandardChart.Properties.Text = "Standard Chart";
            // 
            // ui_mnuChartsZoomIn
            // 
            this.ui_mnuChartsZoomIn.Properties.Text = "Zoom In";
            // 
            // ui_mnuChartsZoomOut
            // 
            this.ui_mnuChartsZoomOut.Properties.Text = "Zoom Out";
            // 
            // ui_mnuChartsTrackCursor
            // 
            this.ui_mnuChartsTrackCursor.Enabled = false;
            this.ui_mnuChartsTrackCursor.Properties.Text = "Track Cursor";
            // 
            // ui_mnuChartsVolume
            // 
            this.ui_mnuChartsVolume.Enabled = false;
            this.ui_mnuChartsVolume.Properties.Text = "Volume";
            // 
            // ui_mnuChartsGrid
            // 
            this.ui_mnuChartsGrid.Checked = true;
            this.ui_mnuChartsGrid.Properties.Text = "Grid";
            // 
            // ui_mnuChart3DStyle
            // 
            this.ui_mnuChart3DStyle.Enabled = false;
            this.ui_mnuChart3DStyle.Properties.Text = "3D Style";
            // 
            // ui_mnuChartsSnapshot
            // 
            this.ui_mnuChartsSnapshot.Commands.AddRange(new Nevron.UI.WinForm.Controls.NCommand[] {
            this.ui_mnuSnapshotPrint,
            this.ui_mnuSnapshotSave});
            this.ui_mnuChartsSnapshot.Properties.Text = "Snapshot";
            // 
            // ui_mnuSnapshotPrint
            // 
            this.ui_mnuSnapshotPrint.Properties.Text = "Print";
            // 
            // ui_mnuSnapshotSave
            // 
            this.ui_mnuSnapshotSave.Properties.Text = "Save";
            // 
            // ui_nmnuTechnicalAnalysis
            // 
            this.ui_nmnuTechnicalAnalysis.Commands.AddRange(new Nevron.UI.WinForm.Controls.NCommand[] {
            this.ui_nmnuTechnicalAnalysisIndicatorList,
            this.ui_nmnuTechnicalAnalysisAdd});
            this.ui_nmnuTechnicalAnalysis.Properties.ShowArrowStyle = Nevron.UI.WinForm.Controls.ShowArrowStyle.Never;
            this.ui_nmnuTechnicalAnalysis.Properties.Text = "Technical Analysis";
            // 
            // ui_nmnuTechnicalAnalysisIndicatorList
            // 
            this.ui_nmnuTechnicalAnalysisIndicatorList.Properties.Text = "Indicator List";
            // 
            // ui_nmnuTechnicalAnalysisAdd
            // 
            this.ui_nmnuTechnicalAnalysisAdd.Commands.AddRange(new Nevron.UI.WinForm.Controls.NCommand[] {
            this.ui_mnuAddHorizontalLine,
            this.ui_mnuAddVerticalLine,
            this.ui_mnuAddText,
            this.ui_mnuAddTrendLine,
            this.ui_mnuAddEllipse,
            this.ui_mnuAddSpeedLines,
            this.ui_mnuAddGannFan,
            this.ui_mnuAddFibonacciArcs,
            this.ui_mnuAddFibonacciRetracement,
            this.ui_mnuAddFibonacciFan,
            this.ui_mnuAddFibonacciTimezone,
            this.ui_mnuAddTironeLevel,
            this.ui_mnuAddQuadrentLines,
            this.ui_mnuAddRafRegression,
            this.ui_mnuAddErrorChannel,
            this.ui_mnuAddRectangle,
            this.ui_mnuAddFreeHandDrawing});
            this.ui_nmnuTechnicalAnalysisAdd.Properties.Text = "Line Study";
            // 
            // ui_mnuAddHorizontalLine
            // 
            this.ui_mnuAddHorizontalLine.Properties.Text = "Horizontal Line";
            // 
            // ui_mnuAddVerticalLine
            // 
            this.ui_mnuAddVerticalLine.Properties.Text = "Vertical Line";
            // 
            // ui_mnuAddText
            // 
            this.ui_mnuAddText.Properties.Text = "Text";
            this.ui_mnuAddText.Properties.Visible = false;
            // 
            // ui_mnuAddTrendLine
            // 
            this.ui_mnuAddTrendLine.Properties.Text = "Trend Line";
            // 
            // ui_mnuAddEllipse
            // 
            this.ui_mnuAddEllipse.Properties.Text = "Ellipse";
            // 
            // ui_mnuAddSpeedLines
            // 
            this.ui_mnuAddSpeedLines.Properties.Text = "Speed Lines";
            // 
            // ui_mnuAddGannFan
            // 
            this.ui_mnuAddGannFan.Properties.Text = "Gann Fan";
            // 
            // ui_mnuAddFibonacciArcs
            // 
            this.ui_mnuAddFibonacciArcs.Properties.Text = "Fibonacci Arcs";
            // 
            // ui_mnuAddFibonacciRetracement
            // 
            this.ui_mnuAddFibonacciRetracement.Properties.Text = "Fibonacci Retracement";
            // 
            // ui_mnuAddFibonacciFan
            // 
            this.ui_mnuAddFibonacciFan.Properties.Text = "Fibonacci Fan";
            // 
            // ui_mnuAddFibonacciTimezone
            // 
            this.ui_mnuAddFibonacciTimezone.Properties.Text = "Fibonacci Timezone";
            // 
            // ui_mnuAddTironeLevel
            // 
            this.ui_mnuAddTironeLevel.Properties.Text = "Tirone Level";
            // 
            // ui_mnuAddQuadrentLines
            // 
            this.ui_mnuAddQuadrentLines.Properties.Text = "Quadrent Lines";
            // 
            // ui_mnuAddRafRegression
            // 
            this.ui_mnuAddRafRegression.Properties.Text = "Raf Regression";
            // 
            // ui_mnuAddErrorChannel
            // 
            this.ui_mnuAddErrorChannel.Properties.Text = "Error Channel";
            // 
            // ui_mnuAddRectangle
            // 
            this.ui_mnuAddRectangle.Properties.Text = "Rectangle";
            // 
            // ui_mnuAddFreeHandDrawing
            // 
            this.ui_mnuAddFreeHandDrawing.Properties.Text = "Free Hand Drawing";
            this.ui_mnuAddFreeHandDrawing.Properties.Visible = false;
            // 
            // ui_nmnuWindows
            // 
            this.ui_nmnuWindows.Commands.AddRange(new Nevron.UI.WinForm.Controls.NCommand[] {
            this.ui_ncmdWindowNewWindow,
            this.ui_ncmdWindowClose,
            this.ui_ncmdWindowCloseAll,
            this.ui_ncmdWindowCascade,
            this.ui_ncmdWindowTileHorizontally,
            this.ui_ncmdWindowTileVertically,
            this.ui_ncmdWindowWindow});
            this.ui_nmnuWindows.Properties.ShowArrowStyle = Nevron.UI.WinForm.Controls.ShowArrowStyle.Never;
            this.ui_nmnuWindows.Properties.Text = "&Windows";
            this.ui_nmnuWindows.Properties.Visible = false;
            // 
            // ui_ncmdWindowNewWindow
            // 
            this.ui_ncmdWindowNewWindow.Enabled = false;
            this.ui_ncmdWindowNewWindow.Properties.ID = 33;
            this.ui_ncmdWindowNewWindow.Properties.Text = "New Window";
            // 
            // ui_ncmdWindowClose
            // 
            this.ui_ncmdWindowClose.Enabled = false;
            this.ui_ncmdWindowClose.Properties.ID = 34;
            this.ui_ncmdWindowClose.Properties.Text = "Close";
            // 
            // ui_ncmdWindowCloseAll
            // 
            this.ui_ncmdWindowCloseAll.Enabled = false;
            this.ui_ncmdWindowCloseAll.Properties.ID = 35;
            this.ui_ncmdWindowCloseAll.Properties.Text = "Close All";
            // 
            // ui_ncmdWindowCascade
            // 
            this.ui_ncmdWindowCascade.Enabled = false;
            this.ui_ncmdWindowCascade.Properties.ID = 36;
            this.ui_ncmdWindowCascade.Properties.Text = "Cascade";
            // 
            // ui_ncmdWindowTileHorizontally
            // 
            this.ui_ncmdWindowTileHorizontally.Enabled = false;
            this.ui_ncmdWindowTileHorizontally.Properties.ID = 37;
            this.ui_ncmdWindowTileHorizontally.Properties.Text = "Tile Horizontally";
            // 
            // ui_ncmdWindowTileVertically
            // 
            this.ui_ncmdWindowTileVertically.Enabled = false;
            this.ui_ncmdWindowTileVertically.Properties.ID = 38;
            this.ui_ncmdWindowTileVertically.Properties.Text = "Tile Vertically";
            // 
            // ui_ncmdWindowWindow
            // 
            this.ui_ncmdWindowWindow.Enabled = false;
            this.ui_ncmdWindowWindow.Properties.ID = 39;
            this.ui_ncmdWindowWindow.Properties.Text = "Window";
            // 
            // ui_nmnuSurveillance
            // 
            this.ui_nmnuSurveillance.Commands.AddRange(new Nevron.UI.WinForm.Controls.NCommand[] {
            this.ui_nmnuSurveillanceSurveillance});
            this.ui_nmnuSurveillance.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(83, 262144);
            this.ui_nmnuSurveillance.Properties.ShowArrowStyle = Nevron.UI.WinForm.Controls.ShowArrowStyle.Never;
            this.ui_nmnuSurveillance.Properties.Text = "&Surveillance";
            this.ui_nmnuSurveillance.Properties.Visible = false;
            // 
            // ui_nmnuSurveillanceSurveillance
            // 
            this.ui_nmnuSurveillanceSurveillance.Properties.Text = "Surveillance";
            // 
            // ui_nmnuHelp
            // 
            this.ui_nmnuHelp.Commands.AddRange(new Nevron.UI.WinForm.Controls.NCommand[] {
            this.ui_nmnuHelpAboutUs});
            this.ui_nmnuHelp.Properties.ShowArrowStyle = Nevron.UI.WinForm.Controls.ShowArrowStyle.Never;
            this.ui_nmnuHelp.Properties.Text = "&Help";
            // 
            // ui_nmnuHelpAboutUs
            // 
            this.ui_nmnuHelpAboutUs.Properties.Text = "About Us";
            // 
            // ui_ndtToolBar
            // 
            this.ui_ndtToolBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ui_ndtToolBar.BackgroundType = Nevron.UI.WinForm.Controls.BackgroundType.Transparent;
            this.ui_ndtToolBar.CanDockBottom = false;
            this.ui_ndtToolBar.CanFloat = false;
            this.ui_ndtToolBar.Commands.AddRange(new Nevron.UI.WinForm.Controls.NCommand[] {
            this.ui_ntbLogin,
            this.ui_ntbLogoff,
            this.ui_ntbChangePassword,
            this.ui_ntbPrint,
            this.ui_ntbOrderBook,
            this.ui_ntbTrades,
            this.ui_ntbNetPosition,
            this.ui_ntbMarketWatch,
            this.nCommand1,
            this.TrackCursor,
            this.cmdVerticalLine,
            this.cmdHoriLine,
            this.cmdText,
            this.TextLevel,
            this.CrossHair,
            this.Equidistance,
            this.Fabiconn_arc,
            this.Fabiconn_fan,
            this.Fabiconn_retracement,
            this.Gann_fan,
            this.Grid,
            this.Volume,
            this.ZoomIn,
            this.ZoomOut,
            this.M1,
            this.M5,
            this.M15,
            this.M30,
            this.H1,
            this.D1,
            this.W1,
            this.MN,
            this.AutoScroll,
            this.ChartShift,
            this.BarChart,
            this.CandleChart,
            this.LineChart,
            this.cbIndicators});
            this.ui_ndtToolBar.DefaultLocation = new System.Drawing.Point(2, 26);
            this.ui_ndtToolBar.HasBorder = false;
            this.ui_ndtToolBar.HasPendantCommand = false;
            this.ui_ndtToolBar.ImageSize = new System.Drawing.Size(20, 20);
            this.ui_ndtToolBar.Name = "ui_ndtToolBar";
            this.ui_ndtToolBar.Palette.BlendStyle = Nevron.UI.BlendStyle.Glass;
            this.ui_ndtToolBar.PrefferedRowIndex = 1;
            this.ui_ndtToolBar.RowIndex = 1;
            this.ui_ndtToolBar.Text = "ToolBar";
            // 
            // ui_ntbLogin
            // 
            this.ui_ntbLogin.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(76, 131072);
            this.ui_ntbLogin.Properties.Text = "Login";
            // 
            // ui_ntbLogoff
            // 
            this.ui_ntbLogoff.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(79, 131072);
            this.ui_ntbLogoff.Properties.Text = "Logoff";
            // 
            // ui_ntbChangePassword
            // 
            this.ui_ntbChangePassword.Properties.Text = "Change Password";
            this.ui_ntbChangePassword.Properties.Visible = false;
            // 
            // ui_ntbPrint
            // 
            this.ui_ntbPrint.Properties.Text = "Print";
            // 
            // ui_ntbOrderBook
            // 
            this.ui_ntbOrderBook.Properties.Text = "Order Book";
            // 
            // ui_ntbTrades
            // 
            this.ui_ntbTrades.Properties.Text = "History";
            // 
            // ui_ntbNetPosition
            // 
            this.ui_ntbNetPosition.Properties.Text = "Net Position";
            this.ui_ntbNetPosition.Properties.Visible = false;
            // 
            // ui_ntbMarketWatch
            // 
            this.ui_ntbMarketWatch.Properties.BeginGroup = true;
            this.ui_ntbMarketWatch.Properties.Text = "Market Watch";
            // 
            // nCommand1
            // 
            this.nCommand1.Enabled = false;
            this.nCommand1.Properties.Selectable = false;
            this.nCommand1.Properties.Text = "                             ";
            // 
            // TrackCursor
            // 
            this.TrackCursor.Properties.Text = "TrackCursor";
            this.TrackCursor.Properties.TooltipText = "TrackCursor";
            this.TrackCursor.Click += new Nevron.UI.WinForm.Controls.CommandEventHandler(this.TrackCursor_Click);
            // 
            // cmdVerticalLine
            // 
            this.cmdVerticalLine.Properties.Text = "Vertical Line";
            this.cmdVerticalLine.Properties.TooltipText = "Vertical Line";
            this.cmdVerticalLine.Click += new Nevron.UI.WinForm.Controls.CommandEventHandler(this.cmdVerticalLine_Click);
            // 
            // cmdHoriLine
            // 
            this.cmdHoriLine.Properties.Text = "Horizontal Line";
            this.cmdHoriLine.Properties.TooltipText = "Horizontal Line";
            this.cmdHoriLine.Click += new Nevron.UI.WinForm.Controls.CommandEventHandler(this.cmdHoriLine_Click);
            // 
            // cmdText
            // 
            this.cmdText.Properties.Text = "Text";
            this.cmdText.Properties.TooltipText = "Text";
            this.cmdText.Click += new Nevron.UI.WinForm.Controls.CommandEventHandler(this.cmdText_Click);
            // 
            // TextLevel
            // 
            this.TextLevel.Properties.Text = "Text Level";
            this.TextLevel.Properties.TooltipText = "Text Level";
            this.TextLevel.Click += new Nevron.UI.WinForm.Controls.CommandEventHandler(this.TextLevel_Click);
            // 
            // CrossHair
            // 
            this.CrossHair.Properties.Text = "Cross Hair";
            this.CrossHair.Properties.TooltipText = "Cross Hair";
            this.CrossHair.Click += new Nevron.UI.WinForm.Controls.CommandEventHandler(this.CrossHair_Click);
            // 
            // Equidistance
            // 
            this.Equidistance.Properties.Text = "Equidistance";
            this.Equidistance.Properties.TooltipText = "Equidistance";
            this.Equidistance.Click += new Nevron.UI.WinForm.Controls.CommandEventHandler(this.Equidistance_Click);
            // 
            // Fabiconn_arc
            // 
            this.Fabiconn_arc.Properties.Text = "Fabiconn_arc";
            this.Fabiconn_arc.Properties.TooltipText = "Fabiconn_arc";
            this.Fabiconn_arc.Click += new Nevron.UI.WinForm.Controls.CommandEventHandler(this.Fabiconn_arc_Click);
            // 
            // Fabiconn_fan
            // 
            this.Fabiconn_fan.Properties.Text = "Fabiconn_fan";
            this.Fabiconn_fan.Properties.TooltipText = "Fabiconn_fan";
            this.Fabiconn_fan.Click += new Nevron.UI.WinForm.Controls.CommandEventHandler(this.Fabiconn_fan_Click);
            // 
            // Fabiconn_retracement
            // 
            this.Fabiconn_retracement.Properties.Text = "Fabiconn Retracement";
            this.Fabiconn_retracement.Properties.TooltipText = "Fabiconn Retracement";
            this.Fabiconn_retracement.Click += new Nevron.UI.WinForm.Controls.CommandEventHandler(this.Fabiconn_retracement_Click);
            // 
            // Gann_fan
            // 
            this.Gann_fan.Properties.Text = "Gann Fan";
            this.Gann_fan.Properties.TooltipText = "Gann Fan";
            this.Gann_fan.Click += new Nevron.UI.WinForm.Controls.CommandEventHandler(this.Gann_fan_Click);
            // 
            // Grid
            // 
            this.Grid.Properties.Text = "Grid";
            this.Grid.Properties.TooltipText = "Grid";
            this.Grid.Click += new Nevron.UI.WinForm.Controls.CommandEventHandler(this.Grid_Click);
            // 
            // Volume
            // 
            this.Volume.Properties.Text = "Volume";
            this.Volume.Properties.TooltipText = "Volume";
            this.Volume.Click += new Nevron.UI.WinForm.Controls.CommandEventHandler(this.Volume_Click);
            // 
            // ZoomIn
            // 
            this.ZoomIn.Properties.Text = "ZoomIn";
            this.ZoomIn.Properties.TooltipText = "ZoomIn";
            this.ZoomIn.Click += new Nevron.UI.WinForm.Controls.CommandEventHandler(this.ZoomIn_Click);
            // 
            // ZoomOut
            // 
            this.ZoomOut.Properties.Text = "ZoomOut";
            this.ZoomOut.Properties.TooltipText = "ZoomOut";
            this.ZoomOut.Click += new Nevron.UI.WinForm.Controls.CommandEventHandler(this.ZoomOut_Click);
            // 
            // M1
            // 
            this.M1.Properties.BeginGroup = true;
            this.M1.Properties.Text = "Minutely 1";
            this.M1.Properties.TooltipText = "Minutely 1";
            this.M1.Click += new Nevron.UI.WinForm.Controls.CommandEventHandler(this.M1_Click);
            // 
            // M5
            // 
            this.M5.Properties.Text = "Minutely 5";
            this.M5.Properties.TooltipText = "Minutely 5";
            this.M5.Click += new Nevron.UI.WinForm.Controls.CommandEventHandler(this.M5_Click);
            // 
            // M15
            // 
            this.M15.Properties.Text = "Minutely 15";
            this.M15.Properties.TooltipText = "Minutely 15";
            this.M15.Click += new Nevron.UI.WinForm.Controls.CommandEventHandler(this.M15_Click);
            // 
            // M30
            // 
            this.M30.Properties.Text = "Minutely 30";
            this.M30.Properties.TooltipText = "Minutely 30";
            this.M30.Click += new Nevron.UI.WinForm.Controls.CommandEventHandler(this.M30_Click);
            // 
            // H1
            // 
            this.H1.Properties.Text = "Hourly";
            this.H1.Properties.TooltipText = "Hourly";
            this.H1.Click += new Nevron.UI.WinForm.Controls.CommandEventHandler(this.H1_Click);
            // 
            // D1
            // 
            this.D1.Properties.Text = "Daily";
            this.D1.Properties.TooltipText = "Daily";
            this.D1.Click += new Nevron.UI.WinForm.Controls.CommandEventHandler(this.D1_Click);
            // 
            // W1
            // 
            this.W1.Properties.Text = "Weekly";
            this.W1.Properties.TooltipText = "Weekly";
            this.W1.Click += new Nevron.UI.WinForm.Controls.CommandEventHandler(this.W1_Click);
            // 
            // MN
            // 
            this.MN.Properties.Text = "Monthly";
            this.MN.Properties.TooltipText = "Monthly";
            this.MN.Click += new Nevron.UI.WinForm.Controls.CommandEventHandler(this.MN_Click);
            // 
            // AutoScroll
            // 
            this.AutoScroll.Properties.Text = "AutoScroll";
            this.AutoScroll.Properties.TooltipText = "AutoScroll";
            this.AutoScroll.Click += new Nevron.UI.WinForm.Controls.CommandEventHandler(this.AutoScroll_Click);
            // 
            // ChartShift
            // 
            this.ChartShift.Properties.Text = "Chart Shift";
            this.ChartShift.Properties.TooltipText = "Chart Shift";
            this.ChartShift.Click += new Nevron.UI.WinForm.Controls.CommandEventHandler(this.ChartShift_Click);
            // 
            // BarChart
            // 
            this.BarChart.Properties.BeginGroup = true;
            this.BarChart.Properties.Text = "Bar Chart";
            this.BarChart.Properties.TooltipText = "Bar Chart";
            this.BarChart.Click += new Nevron.UI.WinForm.Controls.CommandEventHandler(this.BarChart_Click);
            // 
            // CandleChart
            // 
            this.CandleChart.Properties.Text = "Candle Chart";
            this.CandleChart.Properties.TooltipText = "Candle Chart";
            this.CandleChart.Click += new Nevron.UI.WinForm.Controls.CommandEventHandler(this.CandleChart_Click);
            // 
            // LineChart
            // 
            this.LineChart.Properties.Text = "Line Chart";
            this.LineChart.Properties.TooltipText = "Line Chart";
            this.LineChart.Click += new Nevron.UI.WinForm.Controls.CommandEventHandler(this.LineChart_Click);
            // 
            // cbIndicators
            // 
            this.cbIndicators.ControlText = "";
            this.cbIndicators.ListProperties.ColumnOnLeft = false;
            this.cbIndicators.ListProperties.ItemHeight = 14;
            this.cbIndicators.PrefferedWidth = 180;
            this.cbIndicators.Properties.MenuOptions.Alignment = Nevron.UI.WinForm.Controls.MenuAlignment.Right;
            this.cbIndicators.Properties.MenuOptions.ColumnOnLeft = false;
            this.cbIndicators.Properties.ShowArrowStyle = Nevron.UI.WinForm.Controls.ShowArrowStyle.Never;
            this.cbIndicators.Properties.Style = Nevron.UI.WinForm.Controls.CommandStyle.Text;
            this.cbIndicators.Properties.Text = "Indicators";
            this.cbIndicators.Properties.TooltipText = "Indicators";
            this.cbIndicators.Click += new Nevron.UI.WinForm.Controls.CommandEventHandler(this.cbIndicators_Click);
            // 
            // ui_ndtTicker
            // 
            this.ui_ndtTicker.BackgroundType = Nevron.UI.WinForm.Controls.BackgroundType.Transparent;
            this.ui_ndtTicker.CommandSize = new System.Drawing.Size(775, 20);
            this.ui_ndtTicker.DefaultLocation = new System.Drawing.Point(0, 0);
            this.ui_ndtTicker.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ui_ndtTicker.HasBorder = false;
            this.ui_ndtTicker.HasGripper = false;
            this.ui_ndtTicker.HasPendantCommand = false;
            this.ui_ndtTicker.Moveable = false;
            this.ui_ndtTicker.Name = "ui_ndtTicker";
            this.ui_ndtTicker.Palette.BlendStyle = Nevron.UI.BlendStyle.Glass;
            this.ui_ndtTicker.PrefferedRowIndex = 0;
            this.ui_ndtTicker.RowIndex = 0;
            this.ui_ndtTicker.Text = "Ticker";
            // 
            // ToolbarChart
            // 
            this.ToolbarChart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ToolbarChart.BackgroundType = Nevron.UI.WinForm.Controls.BackgroundType.Transparent;
            this.ToolbarChart.DefaultLocation = new System.Drawing.Point(0, 57);
            this.ToolbarChart.HasBorder = false;
            this.ToolbarChart.HasPendantCommand = false;
            this.ToolbarChart.ImageSize = new System.Drawing.Size(20, 20);
            this.ToolbarChart.Moveable = false;
            this.ToolbarChart.Name = "ToolbarChart";
            this.ToolbarChart.Palette.BlendStyle = Nevron.UI.BlendStyle.Glass;
            this.ToolbarChart.PrefferedRowIndex = 2;
            this.ToolbarChart.RowIndex = 2;
            this.ToolbarChart.Text = "chartToolbar";
            this.ToolbarChart.Visible = false;
            // 
            // ui_ncmdMarketMarketPicture
            // 
            this.ui_ncmdMarketMarketPicture.Properties.ID = 21;
            this.ui_ncmdMarketMarketPicture.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(117, 0);
            this.ui_ncmdMarketMarketPicture.Properties.Text = "Market Picture";
            this.ui_ncmdMarketMarketPicture.Properties.Visible = false;
            // 
            // ui_ncmdMarketSnapQuote
            // 
            this.ui_ncmdMarketSnapQuote.Properties.ID = 22;
            this.ui_ncmdMarketSnapQuote.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(120, 131072);
            this.ui_ncmdMarketSnapQuote.Properties.Text = "Snap Quote";
            this.ui_ncmdMarketSnapQuote.Properties.Visible = false;
            // 
            // ui_ncmdMarketMarketStatus
            // 
            this.ui_ncmdMarketMarketStatus.Properties.ID = 23;
            this.ui_ncmdMarketMarketStatus.Properties.Text = "Market Status";
            this.ui_ncmdMarketMarketStatus.Properties.Visible = false;
            // 
            // ui_ncmdMarketTopGainerLosers
            // 
            this.ui_ncmdMarketTopGainerLosers.Enabled = false;
            this.ui_ncmdMarketTopGainerLosers.Properties.ID = 24;
            this.ui_ncmdMarketTopGainerLosers.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(119, 196608);
            this.ui_ncmdMarketTopGainerLosers.Properties.Text = "Top Gainers/Losers";
            this.ui_ncmdMarketTopGainerLosers.Properties.Visible = false;
            // 
            // ui_ntbBackup
            // 
            this.ui_ntbBackup.Properties.BeginGroup = true;
            this.ui_ntbBackup.Properties.Text = "Online Backup";
            this.ui_ntbBackup.Properties.Visible = false;
            // 
            // ui_ntbMessageLog
            // 
            this.ui_ntbMessageLog.Properties.Text = "Message Log";
            // 
            // ui_ntbOrderEntry
            // 
            this.ui_ntbOrderEntry.Properties.BeginGroup = true;
            this.ui_ntbOrderEntry.Properties.Text = "Order Entry";
            // 
            // ui_ntbMarketPicture
            // 
            this.ui_ntbMarketPicture.Properties.Text = "Market Picture";
            // 
            // ui_ntbContractInfo
            // 
            this.ui_ntbContractInfo.Properties.BeginGroup = true;
            this.ui_ntbContractInfo.Properties.Text = "Contract Information";
            // 
            // ui_ntbModifyOrder
            // 
            this.ui_ntbModifyOrder.Properties.BeginGroup = true;
            this.ui_ntbModifyOrder.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(113, 65536);
            this.ui_ntbModifyOrder.Properties.Text = "Modify Order";
            this.ui_ntbModifyOrder.Click += new Nevron.UI.WinForm.Controls.CommandEventHandler(this.ui_ntbModifyOrder_Click);
            // 
            // ui_ntbCancelOrder
            // 
            this.ui_ntbCancelOrder.Properties.Text = "Cancel Selected Order(s)";
            // 
            // ui_ntbCancelAllOrders
            // 
            this.ui_ntbCancelAllOrders.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(114, 65536);
            this.ui_ntbCancelAllOrders.Properties.Text = "Cancel All Orders";
            // 
            // ui_ntbFilter
            // 
            this.ui_ntbFilter.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(116, 0);
            this.ui_ntbFilter.Properties.Text = "Filter";
            this.ui_ntbFilter.Properties.Visible = false;
            // 
            // nuiPnlMessageBar
            // 
            this.nuiPnlMessageBar.Controls.Add(this.lblMessages);
            this.nuiPnlMessageBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.nuiPnlMessageBar.Location = new System.Drawing.Point(0, 450);
            this.nuiPnlMessageBar.MinimumSize = new System.Drawing.Size(0, 25);
            this.nuiPnlMessageBar.Name = "nuiPnlMessageBar";
            this.nuiPnlMessageBar.Palette.BlendStyle = Nevron.UI.BlendStyle.Glass;
            this.nuiPnlMessageBar.Size = new System.Drawing.Size(780, 25);
            this.nuiPnlMessageBar.TabIndex = 8;
            this.nuiPnlMessageBar.Visible = false;
            // 
            // lblMessages
            // 
            this.lblMessages.BackColor = System.Drawing.Color.Transparent;
            this.lblMessages.Location = new System.Drawing.Point(3, 5);
            this.lblMessages.Name = "lblMessages";
            this.lblMessages.Size = new System.Drawing.Size(67, 13);
            this.lblMessages.TabIndex = 0;
            this.lblMessages.Text = "Messages....";
            // 
            // nSplitter1
            // 
            this.nSplitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.nSplitter1.Location = new System.Drawing.Point(0, 445);
            this.nSplitter1.MinimumSize = new System.Drawing.Size(34, 5);
            this.nSplitter1.MinSize = 15;
            this.nSplitter1.Name = "nSplitter1";
            this.nSplitter1.Size = new System.Drawing.Size(780, 5);
            this.nSplitter1.TabIndex = 9;
            this.nSplitter1.TabStop = false;
            // 
            // nuiPnlStatusBars
            // 
            this.nuiPnlStatusBars.Controls.Add(this.nuiPanel4);
            this.nuiPnlStatusBars.Controls.Add(this.nuiPanel3);
            this.nuiPnlStatusBars.Controls.Add(this.nuiPanel2);
            this.nuiPnlStatusBars.Controls.Add(this.nuiPanel1);
            this.nuiPnlStatusBars.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.nuiPnlStatusBars.Location = new System.Drawing.Point(0, 353);
            this.nuiPnlStatusBars.Name = "nuiPnlStatusBars";
            this.nuiPnlStatusBars.Palette.BlendStyle = Nevron.UI.BlendStyle.Glass;
            this.nuiPnlStatusBars.Size = new System.Drawing.Size(780, 92);
            this.nuiPnlStatusBars.TabIndex = 10;
            this.nuiPnlStatusBars.Text = "nuiPanel2";
            this.nuiPnlStatusBars.Visible = false;
            // 
            // nuiPanel4
            // 
            this.nuiPanel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.nuiPanel4.Border.Style = Nevron.UI.BorderStyle3D.None;
            this.nuiPanel4.Controls.Add(this.lblBottomStatus1);
            this.nuiPanel4.Controls.Add(this.lblBottomStatus4);
            this.nuiPanel4.Controls.Add(this.lblBottomStatus2);
            this.nuiPanel4.Controls.Add(this.lblBottomStatus3);
            this.nuiPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.nuiPanel4.FillInfo.FillStyle = Nevron.UI.WinForm.Controls.FillStyle.Solid;
            this.nuiPanel4.Location = new System.Drawing.Point(0, 66);
            this.nuiPanel4.Name = "nuiPanel4";
            this.nuiPanel4.Palette.BlendStyle = Nevron.UI.BlendStyle.Glass;
            this.nuiPanel4.Size = new System.Drawing.Size(778, 22);
            this.nuiPanel4.TabIndex = 18;
            this.nuiPanel4.Text = "nuiPanel4";
            this.nuiPanel4.Visible = false;
            // 
            // lblBottomStatus1
            // 
            this.lblBottomStatus1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lblBottomStatus1.BackColor = System.Drawing.Color.Transparent;
            this.lblBottomStatus1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBottomStatus1.Location = new System.Drawing.Point(3, 3);
            this.lblBottomStatus1.Name = "lblBottomStatus1";
            this.lblBottomStatus1.Size = new System.Drawing.Size(109, 16);
            this.lblBottomStatus1.TabIndex = 10;
            this.lblBottomStatus1.Text = "Bottom";
            this.lblBottomStatus1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBottomStatus4
            // 
            this.lblBottomStatus4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lblBottomStatus4.BackColor = System.Drawing.Color.Transparent;
            this.lblBottomStatus4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBottomStatus4.Location = new System.Drawing.Point(352, 3);
            this.lblBottomStatus4.Name = "lblBottomStatus4";
            this.lblBottomStatus4.Size = new System.Drawing.Size(141, 16);
            this.lblBottomStatus4.TabIndex = 13;
            this.lblBottomStatus4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBottomStatus2
            // 
            this.lblBottomStatus2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lblBottomStatus2.BackColor = System.Drawing.Color.Transparent;
            this.lblBottomStatus2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBottomStatus2.Location = new System.Drawing.Point(116, 3);
            this.lblBottomStatus2.Name = "lblBottomStatus2";
            this.lblBottomStatus2.Size = new System.Drawing.Size(111, 16);
            this.lblBottomStatus2.TabIndex = 11;
            this.lblBottomStatus2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBottomStatus3
            // 
            this.lblBottomStatus3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lblBottomStatus3.BackColor = System.Drawing.Color.Transparent;
            this.lblBottomStatus3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBottomStatus3.Location = new System.Drawing.Point(231, 3);
            this.lblBottomStatus3.Name = "lblBottomStatus3";
            this.lblBottomStatus3.Size = new System.Drawing.Size(117, 16);
            this.lblBottomStatus3.TabIndex = 12;
            this.lblBottomStatus3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nuiPanel3
            // 
            this.nuiPanel3.Border.Style = Nevron.UI.BorderStyle3D.None;
            this.nuiPanel3.Controls.Add(this.lblMiddleStatus1);
            this.nuiPanel3.Controls.Add(this.lblMiddleStatus2);
            this.nuiPanel3.Controls.Add(this.lblMiddleStatus3);
            this.nuiPanel3.Controls.Add(this.lblMiddleStatus4);
            this.nuiPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.nuiPanel3.FillInfo.FillStyle = Nevron.UI.WinForm.Controls.FillStyle.Solid;
            this.nuiPanel3.Location = new System.Drawing.Point(0, 44);
            this.nuiPanel3.Name = "nuiPanel3";
            this.nuiPanel3.Palette.BlendStyle = Nevron.UI.BlendStyle.Glass;
            this.nuiPanel3.Size = new System.Drawing.Size(778, 22);
            this.nuiPanel3.TabIndex = 17;
            this.nuiPanel3.Text = "nuiPanel3";
            this.nuiPanel3.Visible = false;
            // 
            // lblMiddleStatus1
            // 
            this.lblMiddleStatus1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMiddleStatus1.BackColor = System.Drawing.Color.Transparent;
            this.lblMiddleStatus1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMiddleStatus1.Location = new System.Drawing.Point(3, 3);
            this.lblMiddleStatus1.Name = "lblMiddleStatus1";
            this.lblMiddleStatus1.Size = new System.Drawing.Size(109, 16);
            this.lblMiddleStatus1.TabIndex = 6;
            this.lblMiddleStatus1.Text = "Middle";
            this.lblMiddleStatus1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMiddleStatus2
            // 
            this.lblMiddleStatus2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMiddleStatus2.BackColor = System.Drawing.Color.Transparent;
            this.lblMiddleStatus2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMiddleStatus2.Location = new System.Drawing.Point(116, 3);
            this.lblMiddleStatus2.Name = "lblMiddleStatus2";
            this.lblMiddleStatus2.Size = new System.Drawing.Size(111, 16);
            this.lblMiddleStatus2.TabIndex = 7;
            this.lblMiddleStatus2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMiddleStatus3
            // 
            this.lblMiddleStatus3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMiddleStatus3.BackColor = System.Drawing.Color.Transparent;
            this.lblMiddleStatus3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMiddleStatus3.Location = new System.Drawing.Point(231, 3);
            this.lblMiddleStatus3.Name = "lblMiddleStatus3";
            this.lblMiddleStatus3.Size = new System.Drawing.Size(117, 16);
            this.lblMiddleStatus3.TabIndex = 8;
            this.lblMiddleStatus3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMiddleStatus4
            // 
            this.lblMiddleStatus4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMiddleStatus4.BackColor = System.Drawing.Color.Transparent;
            this.lblMiddleStatus4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMiddleStatus4.Location = new System.Drawing.Point(352, 3);
            this.lblMiddleStatus4.Name = "lblMiddleStatus4";
            this.lblMiddleStatus4.Size = new System.Drawing.Size(141, 16);
            this.lblMiddleStatus4.TabIndex = 9;
            this.lblMiddleStatus4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nuiPanel2
            // 
            this.nuiPanel2.Border.Style = Nevron.UI.BorderStyle3D.None;
            this.nuiPanel2.Controls.Add(this.ui_lblTopStatus1);
            this.nuiPanel2.Controls.Add(this.lblTopStatus2);
            this.nuiPanel2.Controls.Add(this.lblTopStatus4);
            this.nuiPanel2.Controls.Add(this.lblTopStatus3);
            this.nuiPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.nuiPanel2.FillInfo.FillStyle = Nevron.UI.WinForm.Controls.FillStyle.Solid;
            this.nuiPanel2.Location = new System.Drawing.Point(0, 22);
            this.nuiPanel2.Name = "nuiPanel2";
            this.nuiPanel2.Palette.BlendStyle = Nevron.UI.BlendStyle.Glass;
            this.nuiPanel2.Size = new System.Drawing.Size(778, 22);
            this.nuiPanel2.TabIndex = 16;
            this.nuiPanel2.Text = "nuiPanel2";
            this.nuiPanel2.Visible = false;
            // 
            // ui_lblTopStatus1
            // 
            this.ui_lblTopStatus1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.ui_lblTopStatus1.BackColor = System.Drawing.Color.Transparent;
            this.ui_lblTopStatus1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ui_lblTopStatus1.Location = new System.Drawing.Point(3, 3);
            this.ui_lblTopStatus1.Name = "ui_lblTopStatus1";
            this.ui_lblTopStatus1.Size = new System.Drawing.Size(109, 16);
            this.ui_lblTopStatus1.TabIndex = 2;
            this.ui_lblTopStatus1.Text = "Top";
            this.ui_lblTopStatus1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTopStatus2
            // 
            this.lblTopStatus2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTopStatus2.BackColor = System.Drawing.Color.Transparent;
            this.lblTopStatus2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTopStatus2.Location = new System.Drawing.Point(116, 3);
            this.lblTopStatus2.Name = "lblTopStatus2";
            this.lblTopStatus2.Size = new System.Drawing.Size(111, 16);
            this.lblTopStatus2.TabIndex = 3;
            this.lblTopStatus2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTopStatus4
            // 
            this.lblTopStatus4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTopStatus4.BackColor = System.Drawing.Color.Transparent;
            this.lblTopStatus4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTopStatus4.Location = new System.Drawing.Point(352, 3);
            this.lblTopStatus4.Name = "lblTopStatus4";
            this.lblTopStatus4.Size = new System.Drawing.Size(140, 16);
            this.lblTopStatus4.TabIndex = 5;
            this.lblTopStatus4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTopStatus3
            // 
            this.lblTopStatus3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTopStatus3.BackColor = System.Drawing.Color.Transparent;
            this.lblTopStatus3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTopStatus3.Location = new System.Drawing.Point(231, 3);
            this.lblTopStatus3.Name = "lblTopStatus3";
            this.lblTopStatus3.Size = new System.Drawing.Size(117, 16);
            this.lblTopStatus3.TabIndex = 4;
            this.lblTopStatus3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nuiPanel1
            // 
            this.nuiPanel1.Border.Style = Nevron.UI.BorderStyle3D.None;
            this.nuiPanel1.Controls.Add(this.label3);
            this.nuiPanel1.Controls.Add(this.lblCompanyName);
            this.nuiPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.nuiPanel1.FillInfo.FillStyle = Nevron.UI.WinForm.Controls.FillStyle.Solid;
            this.nuiPanel1.Location = new System.Drawing.Point(0, 0);
            this.nuiPanel1.Name = "nuiPanel1";
            this.nuiPanel1.Palette.BlendStyle = Nevron.UI.BlendStyle.Glass;
            this.nuiPanel1.Size = new System.Drawing.Size(778, 22);
            this.nuiPanel1.TabIndex = 15;
            this.nuiPanel1.Text = "nuiPanel1";
            this.nuiPanel1.Visible = false;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Location = new System.Drawing.Point(320, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(313, 16);
            this.label3.TabIndex = 1;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCompanyName.BackColor = System.Drawing.Color.Transparent;
            this.lblCompanyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCompanyName.Location = new System.Drawing.Point(3, 3);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(313, 16);
            this.lblCompanyName.TabIndex = 0;
            this.lblCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ui_tmrLockWorkstation
            // 
            this.ui_tmrLockWorkstation.Interval = 300000;
            this.ui_tmrLockWorkstation.Tick += new System.EventHandler(this.ui_tmrLockWorkstation_Tick);
            // 
            // ui_tmrTicker
            // 
            this.ui_tmrTicker.Interval = 500;
            this.ui_tmrTicker.Tick += new System.EventHandler(this.ui_tmrTicker_Tick);
            // 
            // ui_lblNepalTime
            // 
            this.ui_lblNepalTime.AutoSize = true;
            this.ui_lblNepalTime.BackColor = System.Drawing.Color.Transparent;
            this.ui_lblNepalTime.Dock = System.Windows.Forms.DockStyle.Right;
            this.ui_lblNepalTime.Location = new System.Drawing.Point(699, 0);
            this.ui_lblNepalTime.Name = "ui_lblNepalTime";
            this.ui_lblNepalTime.Size = new System.Drawing.Size(79, 13);
            this.ui_lblNepalTime.TabIndex = 0;
            this.ui_lblNepalTime.Text = "Nepal Time :    ";
            this.ui_lblNepalTime.Visible = false;
            // 
            // ui_npnlHeader
            // 
            this.ui_npnlHeader.Controls.Add(this.ui_lblNepalTime);
            this.ui_npnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.ui_npnlHeader.Location = new System.Drawing.Point(0, 0);
            this.ui_npnlHeader.Name = "ui_npnlHeader";
            this.ui_npnlHeader.Palette.BlendStyle = Nevron.UI.BlendStyle.Glass;
            this.ui_npnlHeader.Size = new System.Drawing.Size(780, 17);
            this.ui_npnlHeader.TabIndex = 14;
            this.ui_npnlHeader.Text = "nuiPanel5";
            this.ui_npnlHeader.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tlstrplblStatusMsg,
            this.tlsOrderServerStatus,
            this.tlsDataServerStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 553);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1146, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(45, 17);
            this.toolStripStatusLabel1.Text = "Status :";
            // 
            // tlstrplblStatusMsg
            // 
            this.tlstrplblStatusMsg.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlstrplblStatusMsg.Name = "tlstrplblStatusMsg";
            this.tlstrplblStatusMsg.Size = new System.Drawing.Size(916, 17);
            this.tlstrplblStatusMsg.Spring = true;
            this.tlstrplblStatusMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tlstrplblStatusMsg.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            // 
            // tlsOrderServerStatus
            // 
            this.tlsOrderServerStatus.Image = global::PALSA.Properties.Resources.Circle_Red;
            this.tlsOrderServerStatus.Name = "tlsOrderServerStatus";
            this.tlsOrderServerStatus.Size = new System.Drawing.Size(88, 17);
            this.tlsOrderServerStatus.Text = "Order Server";
            this.tlsOrderServerStatus.ToolTipText = "Order Server Connection Status";
            // 
            // tlsDataServerStatus
            // 
            this.tlsDataServerStatus.Image = global::PALSA.Properties.Resources.Circle_Red;
            this.tlsDataServerStatus.Name = "tlsDataServerStatus";
            this.tlsDataServerStatus.Size = new System.Drawing.Size(82, 17);
            this.tlsDataServerStatus.Text = "Data Server";
            this.tlsDataServerStatus.ToolTipText = "Data Server Connection Status";
            // 
            // Alerts
            // 
            this.Alerts.Dock = System.Windows.Forms.DockStyle.None;
            this.Alerts.Location = new System.Drawing.Point(1, 19);
            this.Alerts.Name = "Alerts";
            this.Alerts.Size = new System.Drawing.Size(848, 149);
            this.Alerts.TabIndex = 9;
            this.Alerts.Text = "Alerts";
            // 
            // Scanner
            // 
            this.Scanner.Dock = System.Windows.Forms.DockStyle.None;
            this.Scanner.Location = new System.Drawing.Point(1, 19);
            this.Scanner.Name = "Scanner";
            this.Scanner.Size = new System.Drawing.Size(271, 21);
            this.Scanner.TabIndex = 10;
            this.Scanner.Text = "Scanner";
            // 
            // Radar
            // 
            this.Radar.Dock = System.Windows.Forms.DockStyle.None;
            this.Radar.Location = new System.Drawing.Point(1, 19);
            this.Radar.Name = "Radar";
            this.Radar.Size = new System.Drawing.Size(271, 21);
            this.Radar.TabIndex = 11;
            this.Radar.Text = "Radar";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1146, 575);
            this.Controls.Add(this.statusStrip1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::PALSA.Properties.Settings.Default, "UserName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.Name = "FrmMain";
            this.Palette.BlendStyle = Nevron.UI.BlendStyle.Glass;
            this.Text = global::PALSA.Properties.Settings.Default.UserName;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.FrmMain_Activated);
            this.Deactivate += new System.EventHandler(this.FrmMain_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.MdiChildActivate += new System.EventHandler(this.FrmMain_MdiChildActivate);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMain_KeyDown);
            this.Resize += new System.EventHandler(this.FrmMain_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.m_DockManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ui_ncbmPALSA)).EndInit();
            this.nuiPnlMessageBar.ResumeLayout(false);
            this.nuiPnlStatusBars.ResumeLayout(false);
            this.nuiPanel4.ResumeLayout(false);
            this.nuiPanel3.ResumeLayout(false);
            this.nuiPanel2.ResumeLayout(false);
            this.nuiPanel1.ResumeLayout(false);
            this.ui_npnlHeader.ResumeLayout(false);
            this.ui_npnlHeader.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion


        public Nevron.UI.WinForm.Docking.NDockManager m_DockManager;
        public Nevron.UI.WinForm.Docking.NDockingPanel SymbolPanel;
        public Nevron.UI.WinForm.Docking.NDockingPanel TerminalPanel;
        public Nevron.UI.WinForm.Docking.NDockingPanel Openpositions;
        public Nevron.UI.WinForm.Docking.NDockingPanel History;
        public Nevron.UI.WinForm.Docking.NDockingPanel OrderBook;
        public Nevron.UI.WinForm.Docking.NDockingPanel MailBox;
        public Nevron.UI.WinForm.Docking.NDockingPanel Accounts;
        public Nevron.UI.WinForm.Docking.NDockingPanel QuotePanel;
        private Nevron.UI.WinForm.Docking.NDockingPanel Alerts;
        private Nevron.UI.WinForm.Docking.NDockingPanel Scanner;
        private Nevron.UI.WinForm.Docking.NDockingPanel Radar;
        private Nevron.UI.WinForm.Docking.NDockingPanel PendingOrders;
        private Nevron.UI.WinForm.Controls.NCommandBarsManager ui_ncbmPALSA;
        private Nevron.UI.WinForm.Controls.NMenuBar ui_nmnuBar;
        private Nevron.UI.WinForm.Controls.NCommand ui_nmnuFile;

        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdFileLogOff;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdFileLoadWorkSpace;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdFileSaveWorkSpace;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdFileExit;
        private Nevron.UI.WinForm.Controls.NCommand ui_nmnuView;

        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdViewTicker;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdViewTrade;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdViewMsgLog;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdViewContractInfo;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdViewToolBar;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdViewFilterBar;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdViewMessageBar;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdViewStatusBar;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdViewAdminMsgBar;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdViewIndicesView;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdViewFullScreen;
        private Nevron.UI.WinForm.Controls.NCommand ui_nmnuMarket;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdMarketMarketWatch;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdMarketMarketPicture;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdMarketSnapQuote;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdMarketMarketStatus;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdMarketTopGainerLosers;
        private Nevron.UI.WinForm.Controls.NCommand ui_nmnuOrders;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdOrdersPlaceOrder;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdOrdersOrderBook;
        private Nevron.UI.WinForm.Controls.NCommand ui_nmnuTrades;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdTradesTrades;
        private Nevron.UI.WinForm.Controls.NCommand ui_nmnuTools;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdToolsCustomize;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdToolsLockWorkStation;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdToolsPortfolio;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdToolsPreferences;
        private Nevron.UI.WinForm.Controls.NCommand ui_nmnuWindows;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdWindowNewWindow;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdWindowClose;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdWindowCloseAll;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdWindowCascade;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdWindowTileHorizontally;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdWindowTileVertically;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdWindowWindow;
        private Nevron.UI.WinForm.Controls.NCommand ui_nmnuHelp;
        private Nevron.UI.WinForm.Controls.NUIPanel nuiPnlMessageBar;
        private System.Windows.Forms.Label lblMessages;
        private Nevron.UI.WinForm.Controls.NUIPanel nuiPnlStatusBars;
        private Nevron.UI.WinForm.Controls.NSplitter nSplitter1;
        private System.Windows.Forms.Label lblTopStatus4;
        private System.Windows.Forms.Label lblTopStatus3;
        private System.Windows.Forms.Label lblTopStatus2;
        private System.Windows.Forms.Label ui_lblTopStatus1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCompanyName;
        private System.Windows.Forms.Label lblBottomStatus4;
        private System.Windows.Forms.Label lblBottomStatus3;
        private System.Windows.Forms.Label lblBottomStatus2;
        private System.Windows.Forms.Label lblBottomStatus1;
        private System.Windows.Forms.Label lblMiddleStatus4;
        private System.Windows.Forms.Label lblMiddleStatus3;
        private System.Windows.Forms.Label lblMiddleStatus2;
        private System.Windows.Forms.Label lblMiddleStatus1;
        private Nevron.UI.WinForm.Controls.NCommand ui_ntbLogin;
        private Nevron.UI.WinForm.Controls.NCommand ui_ntbLogoff;
        private Nevron.UI.WinForm.Controls.NCommand ui_ntbBackup;
        private Nevron.UI.WinForm.Controls.NCommand ui_ntbPrint;
        private Nevron.UI.WinForm.Controls.NCommand ui_ntbMessageLog;
        private Nevron.UI.WinForm.Controls.NCommand ui_ntbOrderEntry;
        private Nevron.UI.WinForm.Controls.NCommand ui_ntbOrderBook;
        private Nevron.UI.WinForm.Controls.NCommand ui_ntbTrades;
        private Nevron.UI.WinForm.Controls.NCommand ui_ntbNetPosition;
        private Nevron.UI.WinForm.Controls.NCommand ui_ntbMarketWatch;
        private Nevron.UI.WinForm.Controls.NCommand ui_ntbMarketPicture;
        private Nevron.UI.WinForm.Controls.NCommand ui_ntbContractInfo;
        private Nevron.UI.WinForm.Controls.NCommand ui_ntbModifyOrder;
        private Nevron.UI.WinForm.Controls.NCommand ui_ntbCancelOrder;
        private Nevron.UI.WinForm.Controls.NCommand ui_ntbCancelAllOrders;
        private Nevron.UI.WinForm.Controls.NDockingToolbar ui_ndtToolBar;
        private Nevron.UI.WinForm.Controls.NCommand nmnuCmdViewTopStatusBar;
        private Nevron.UI.WinForm.Controls.NCommand nmnuCmdViewMiddleStatusBar;
        private Nevron.UI.WinForm.Controls.NCommand nmnuCmdViewBottomStatusBar;
        private Nevron.UI.WinForm.Controls.NUIPanel nuiPanel4;
        private Nevron.UI.WinForm.Controls.NUIPanel nuiPanel3;
        private Nevron.UI.WinForm.Controls.NUIPanel nuiPanel2;
        private Nevron.UI.WinForm.Controls.NUIPanel nuiPanel1;
        private Nevron.UI.WinForm.Controls.NDockingToolbar ui_ndtTicker;

        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdViewLanguages;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdLanguagesEnglish;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdLanguagesHindi;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdViewThemes;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmbThemeMacOS;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmbThemeOffice2007Black;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmbThemeOffice2007Blue;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmbThemeOrange;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmbThemeVista;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdThemeVistaRoyal;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdThemeInspirant;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdThemeVistaPlus;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdThemeOpusAlpha;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdThemeOffice2007Aqua;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdThemeVistaSlate;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdThemeSimple;
        private System.Windows.Forms.Timer ui_tmrLockWorkstation;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdViewParticipantList;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdViewIndexBar;

        private Nevron.UI.WinForm.Controls.NCommand ui_ntbFilter;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuCharts;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuChartsNewChart;
        private System.Windows.Forms.Timer ui_tmrTicker;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdMarketQuote;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdViewAccountsInfo;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdThemeRoyal;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdThemeMoonlight;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdThemeAqua;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdThemeWood;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuChartsPeriodicity;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuPeriodicity1Minute;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuPeriodicity5Minute;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuPeriodicity15Minute;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuPeriodicity30Minute;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuPeriodicity1Hour;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuPeriodicityDaily;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuPeriodicityWeekly;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuPeriodicityMonthly;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuChartsChartType;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuChartTypeBarChart;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuChartTypeCandleChart;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuChartTypeLineChart;
        private Nevron.UI.WinForm.Controls.NCommand ui_nmunPriceTypeRenko;
        private Nevron.UI.WinForm.Controls.NCommand ui_nmunPriceTypeKagi;
        private Nevron.UI.WinForm.Controls.NCommand ui_nmunPriceTypeThreeLineBreak;
        private Nevron.UI.WinForm.Controls.NCommand ui_nmunPriceTypeEquiVolume;
        private Nevron.UI.WinForm.Controls.NCommand ui_nmunPriceTypeEquiVolumeShadow;
        private Nevron.UI.WinForm.Controls.NCommand ui_nmunPriceTypeCandleVolume;
        private Nevron.UI.WinForm.Controls.NCommand ui_nmunPriceTypeHeikinAshi;
        private Nevron.UI.WinForm.Controls.NCommand ui_nmunPriceTypeStandardChart;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuChartsZoomIn;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuChartsZoomOut;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuChartsTrackCursor;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuChartsVolume;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuChartsGrid;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuChart3DStyle;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuChartsSnapshot;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuSnapshotPrint;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuSnapshotSave;
        private Nevron.UI.WinForm.Controls.NCommand ui_nmnuTechnicalAnalysisIndicatorList;
        private Nevron.UI.WinForm.Controls.NCommand ui_nmnuTechnicalAnalysisAdd;
        private System.Windows.Forms.Label ui_lblNepalTime;
        private Nevron.UI.WinForm.Controls.NUIPanel ui_npnlHeader;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdLanguagesNepali;
        private Nevron.UI.WinForm.Controls.NCommand ui_nmnuSurveillance;
        private Nevron.UI.WinForm.Controls.NCommand ui_nmnuHelpAboutUs;
        public Nevron.UI.WinForm.Controls.NCommand ui_nmnuTechnicalAnalysis;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuChartsPriceType;
        private Nevron.UI.WinForm.Controls.NCommand ui_nmunPriceTypePointandFigure;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        public System.Windows.Forms.ToolStripStatusLabel tlstrplblStatusMsg;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdFileChangePassword;
        private Nevron.UI.WinForm.Controls.NCommand ui_ntbChangePassword;
        public Nevron.UI.WinForm.Controls.NCommand ui_ncmdFileLogin;
        public Nevron.UI.WinForm.Controls.NCommand ui_ncmdFileCreateDemoAccount;
        public Nevron.UI.WinForm.Controls.NCommand ui_ncmdViewNetPosition;
        public Nevron.UI.WinForm.Controls.NCommand ui_nmnuSurveillanceSurveillance;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdThemeGreen;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdViewScanner;
        private Nevron.UI.WinForm.Controls.NCommand nCommand1;

        private Nevron.UI.WinForm.Controls.NDockingToolbar ToolbarChart;
        private Nevron.UI.WinForm.Controls.NCommand cmdVerticalLine;
        private Nevron.UI.WinForm.Controls.NCommand cmdHoriLine;
        private Nevron.UI.WinForm.Controls.NCommand cmdText;
        private Nevron.UI.WinForm.Controls.NCommand M1;
        private Nevron.UI.WinForm.Controls.NCommand M5;
        private Nevron.UI.WinForm.Controls.NCommand M15;
        private Nevron.UI.WinForm.Controls.NCommand M30;
        private Nevron.UI.WinForm.Controls.NCommand H1;
        private Nevron.UI.WinForm.Controls.NCommand D1;
        private Nevron.UI.WinForm.Controls.NCommand MN;
        private Nevron.UI.WinForm.Controls.NCommand W1;
        private Nevron.UI.WinForm.Controls.NCommand ZoomIn;
        private Nevron.UI.WinForm.Controls.NCommand ZoomOut;
        public Nevron.UI.WinForm.Controls.NCommand TrackCursor;

        private Nevron.UI.WinForm.Controls.NComboBoxCommand cbIndicators;
        private NCommand TextLevel;
        private NCommand CrossHair;
        private NCommand Equidistance;
        private NCommand Fabiconn_arc;
        private NCommand Fabiconn_fan;
        private NCommand Fabiconn_retracement;
        private NCommand Gann_fan;
        private NCommand Grid;
        private NCommand Volume;
        private NCommand AutoScroll;
        private NCommand ChartShift;
        private NCommand BarChart;
        private NCommand CandleChart;
        private NCommand LineChart;
        private NCommand ui_ncmdPendingOrders;
        private NCommand ui_ncmdViewRadar;
        private NCommand ui_ncmdViewExpertAdvisor;
        private NCommand ui_ncmdViewBackTest;
        private ToolStripStatusLabel tlsOrderServerStatus;
        private ToolStripStatusLabel tlsDataServerStatus;
        public NCommand ui_mnuAddHorizontalLine;
        public NCommand ui_mnuAddVerticalLine;
        public NCommand ui_mnuAddText;
        public NCommand ui_mnuAddTrendLine;
        public NCommand ui_mnuAddEllipse;
        public NCommand ui_mnuAddSpeedLines;
        public NCommand ui_mnuAddGannFan;
        public NCommand ui_mnuAddFibonacciArcs;
        public NCommand ui_mnuAddFibonacciRetracement;
        public NCommand ui_mnuAddFibonacciFan;
        public NCommand ui_mnuAddFibonacciTimezone;
        public NCommand ui_mnuAddTironeLevel;
        public NCommand ui_mnuAddQuadrentLines;
        public NCommand ui_mnuAddRafRegression;
        public NCommand ui_mnuAddErrorChannel;
        public NCommand ui_mnuAddRectangle;
        public NCommand ui_mnuAddFreeHandDrawing;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
    }
}