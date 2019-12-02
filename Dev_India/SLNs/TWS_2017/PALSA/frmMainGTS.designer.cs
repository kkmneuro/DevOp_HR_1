namespace GTS
{
    partial class frmMainGTS
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
            Nevron.UI.WinForm.Docking.NDockZone nDockZone1 = new Nevron.UI.WinForm.Docking.NDockZone();
            Nevron.UI.WinForm.Docking.NDockZone nDockZone2 = new Nevron.UI.WinForm.Docking.NDockZone();
            Nevron.UI.WinForm.Docking.NDockingPanelHost nDockZone3 = new Nevron.UI.WinForm.Docking.NDockingPanelHost();
            Nevron.UI.WinForm.Docking.NDockingPanelHost nDockZone4 = new Nevron.UI.WinForm.Docking.NDockingPanelHost();
            this.m_DockManager = new Nevron.UI.WinForm.Docking.NDockManager(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SymbolPanel = new Nevron.UI.WinForm.Docking.NDockingPanel();
            this.TerminalPanel = new Nevron.UI.WinForm.Docking.NDockingPanel();
            this.QuotePanel = new Nevron.UI.WinForm.Docking.NDockingPanel();
            this.Level2Panel = new Nevron.UI.WinForm.Docking.NDockingPanel();
            this.nuiPanel1 = new Nevron.UI.WinForm.Controls.NUIPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelTimeAll = new Nevron.UI.WinForm.Controls.NUIPanel();
            this.nuiPanel5 = new Nevron.UI.WinForm.Controls.NUIPanel();
            this.lblLondonTime = new System.Windows.Forms.Label();
            this.nuiPanel2 = new Nevron.UI.WinForm.Controls.NUIPanel();
            this.lblTokyoTime = new System.Windows.Forms.Label();
            this.nuiPanel4 = new Nevron.UI.WinForm.Controls.NUIPanel();
            this.lblNYTime = new System.Windows.Forms.Label();
            this.nuiPanel3 = new Nevron.UI.WinForm.Controls.NUIPanel();
            this.lblSydTime = new System.Windows.Forms.Label();
            this.timeRefresh = new System.Windows.Forms.Timer(this.components);
            this.nuiPanel6 = new Nevron.UI.WinForm.Controls.NUIPanel();
            this.TraycontextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripAccount = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsAllHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.tslast3months = new System.Windows.Forms.ToolStripMenuItem();
            this.tsLastMonth = new System.Windows.Forms.ToolStripMenuItem();
            this.tsCustomPeriod = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSaveasReport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSaveasDetailedReport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsCommissions = new System.Windows.Forms.ToolStripMenuItem();
            this.tsTaxes = new System.Windows.Forms.ToolStripMenuItem();
            this.tsComments = new System.Windows.Forms.ToolStripMenuItem();
            this.toolstripAutoArrange = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.ui_ncbmPALSA = new Nevron.UI.WinForm.Controls.NCommandBarsManager(this.components);
            this.ui_nmnuBar = new Nevron.UI.WinForm.Controls.NMenuBar();
            this.ui_nmnuFile = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdFileLogin = new Nevron.UI.WinForm.Controls.NCommand();
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
            this.ui_nmnuMarket = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdMarketMarketWatch = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdMarketQuote = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdMarketMarketPicture = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdMarketSnapQuote = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdMarketMarketStatus = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdMarketTopGainerLosers = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_nmnuOrders = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdOrdersPlaceBuyOrders = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdOrdersPlaceSellOrders = new Nevron.UI.WinForm.Controls.NCommand();
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
            this.ui_mnuPeriodicity4Hour = new Nevron.UI.WinForm.Controls.NCommand();
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
            this.ui_ntbBackup = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ntbPrint = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ntbMessageLog = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ntbOrderEntry = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ntbOrderBook = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ntbTrades = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ntbNetPosition = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ntbMarketWatch = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ntbMarketPicture = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ntbContractInfo = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ntbModifyOrder = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ntbCancelOrder = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ntbCancelAllOrders = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ntbFilter = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ndtTicker = new Nevron.UI.WinForm.Controls.NDockingToolbar();
            this.ui_ndtFilter = new Nevron.UI.WinForm.Controls.NDockingToolbar();
            this.ui_ncmbInstrumentType = new Nevron.UI.WinForm.Controls.NComboBoxCommand();
            this.ui_ncmbSymbol = new Nevron.UI.WinForm.Controls.NComboBoxCommand();
            this.ui_ncmbExpiryDate = new Nevron.UI.WinForm.Controls.NComboBoxCommand();
            this.ui_nServerStatus = new Nevron.UI.WinForm.Controls.NDockingToolbar();
            this.ui_ncmdOrderServerStatus = new Nevron.UI.WinForm.Controls.NCommand();
            this.ui_ncmdDataServerStatus = new Nevron.UI.WinForm.Controls.NCommand();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tlstrplblStatusMsg = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.m_DockManager)).BeginInit();
            this.nuiPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelTimeAll.SuspendLayout();
            this.nuiPanel5.SuspendLayout();
            this.nuiPanel2.SuspendLayout();
            this.nuiPanel4.SuspendLayout();
            this.nuiPanel3.SuspendLayout();
            this.TraycontextMenuStrip.SuspendLayout();
            this.contextMenuStripAccount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ui_ncbmPALSA)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_DockManager
            // 
            this.m_DockManager.CaptionStyle.AutomaticHeight = false;
            this.m_DockManager.CaptionStyle.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_DockManager.CaptionStyle.GripperVisibility = Nevron.UI.WinForm.Controls.GripperVisibility.Hide;
            this.m_DockManager.CaptionStyle.Height = 19;
            this.m_DockManager.DocumentStyle.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_DockManager.Form = this;
            this.m_DockManager.GroupBorderStyle = Nevron.UI.BorderStyle3D.Flat;
            this.m_DockManager.Palette.Scheme = Nevron.UI.WinForm.Controls.ColorScheme.LunaSilver;
            this.m_DockManager.RootContainerZIndex = 1;
            this.m_DockManager.UndockToleranceSize = 2;
            //  
            // Root Zone
            //  
            this.m_DockManager.RootContainer.RootZone.AddChild(nDockZone1);
            this.m_DockManager.RootContainer.RootZone.Orientation = System.Windows.Forms.Orientation.Vertical;
            //  
            // nDockZone1
            //  
            nDockZone1.AddChild(nDockZone2);
            nDockZone1.AddChild(nDockZone4);
            nDockZone1.Name = "nDockZone1";
            nDockZone1.Orientation = System.Windows.Forms.Orientation.Vertical;
            nDockZone1.Index = 0;
            //  
            // nDockZone2
            //  
            nDockZone2.AddChild(nDockZone3);
            nDockZone2.AddChild(this.m_DockManager.DocumentManager.DocumentViewHost);
            nDockZone2.Name = "nDockZone2";
            nDockZone2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            nDockZone2.Index = 0;
            nDockZone2.SizeInfo.PrefferedSize = new System.Drawing.Size(200, 273);
            //  
            // nDockZone3
            //  
            nDockZone3.AddChild(this.QuotePanel);
            nDockZone3.AddChild(this.SymbolPanel);
            nDockZone3.AddChild(this.Level2Panel);
            nDockZone3.Name = "nDockZone3";
            nDockZone3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            nDockZone3.Index = 0;
            //  
            // nDockZone4
            //  
            nDockZone4.AddChild(this.TerminalPanel);
            nDockZone4.Name = "nDockZone4";
            nDockZone4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            nDockZone4.Index = 1;
            nDockZone4.SizeInfo.PrefferedSize = new System.Drawing.Size(200, 171);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // SymbolPanel
            // 
            this.SymbolPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SymbolPanel.Key = "symbolkey";
            this.SymbolPanel.Location = new System.Drawing.Point(1, 21);
            this.SymbolPanel.Name = "SymbolPanel";
            this.SymbolPanel.Size = new System.Drawing.Size(18, -2);
            this.SymbolPanel.TabIndex = 1;
            this.SymbolPanel.Text = "Symbol";
            this.SymbolPanel.Closing += new Nevron.UI.WinForm.Docking.PanelCancelEventHandler(this.SymbolPanel_Closing);
            this.SymbolPanel.Closed += new Nevron.UI.WinForm.Docking.PanelEventHandler(this.SymbolPanel_Closed);
            this.SymbolPanel.Activated += new Nevron.UI.WinForm.Docking.PanelEventHandler(this.SymbolPanel_Activated);
            // 
            // TerminalPanel
            // 
            this.TerminalPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TerminalPanel.Key = "terminalkey";
            this.TerminalPanel.Location = new System.Drawing.Point(1, 21);
            this.TerminalPanel.Name = "TerminalPanel";
            this.TerminalPanel.Size = new System.Drawing.Size(1007, 145);
            this.TerminalPanel.SizeInfo.PrefferedSize = new System.Drawing.Size(200, 171);
            this.TerminalPanel.TabIndex = 4;
            this.TerminalPanel.Text = "Terminal Panel";
            this.TerminalPanel.Closed += new Nevron.UI.WinForm.Docking.PanelEventHandler(this.TerminalPanel_Closed);
            // 
            // QuotePanel
            // 
            this.QuotePanel.Caption.ImageIndex = 35;
            this.QuotePanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuotePanel.Key = "QuoteKey";
            this.QuotePanel.Location = new System.Drawing.Point(1, 21);
            this.QuotePanel.Name = "QuotePanel";
            this.QuotePanel.Size = new System.Drawing.Size(198, 330);
            this.QuotePanel.TabIndex = 2;
            this.QuotePanel.Text = "Quote";
            this.QuotePanel.Closed += new Nevron.UI.WinForm.Docking.PanelEventHandler(this.QuotePanel_Closed);
            this.QuotePanel.Activated += new Nevron.UI.WinForm.Docking.PanelEventHandler(this.QuotePanel_Activated);
            this.QuotePanel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.QuotePanel_MouseDoubleClick);
            // 
            // Level2Panel
            // 
            this.Level2Panel.Caption.ImageIndex = 33;
            this.Level2Panel.Caption.ImageList = this.imageList1;
            this.Level2Panel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Level2Panel.Key = "Level2Key";
            this.Level2Panel.Location = new System.Drawing.Point(1, 21);
            this.Level2Panel.Name = "Level2Panel";
            this.Level2Panel.Size = new System.Drawing.Size(194, 466);
            this.Level2Panel.TabIndex = 3;
            this.Level2Panel.Text = "Level 2";
            this.Level2Panel.Activated += new Nevron.UI.WinForm.Docking.PanelEventHandler(this.Level2Panel_Activated);
            // 
            // nuiPanel1
            // 
            this.nuiPanel1.Border.Style = Nevron.UI.BorderStyle3D.RaisedInner;
            this.nuiPanel1.Controls.Add(this.pictureBox1);
            this.nuiPanel1.Controls.Add(this.panelTimeAll);
            this.nuiPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.nuiPanel1.FillInfo.Gradient2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.nuiPanel1.Location = new System.Drawing.Point(0, 0);
            this.nuiPanel1.Name = "nuiPanel1";
            this.nuiPanel1.Size = new System.Drawing.Size(1009, 24);
            this.nuiPanel1.TabIndex = 5;
            this.nuiPanel1.Text = "nuiPanel1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(121, 22);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // panelTimeAll
            // 
            this.panelTimeAll.Controls.Add(this.nuiPanel5);
            this.panelTimeAll.Controls.Add(this.nuiPanel2);
            this.panelTimeAll.Controls.Add(this.nuiPanel4);
            this.panelTimeAll.Controls.Add(this.nuiPanel3);
            this.panelTimeAll.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelTimeAll.Location = new System.Drawing.Point(587, 0);
            this.panelTimeAll.Name = "panelTimeAll";
            this.panelTimeAll.Size = new System.Drawing.Size(420, 22);
            this.panelTimeAll.TabIndex = 1;
            this.panelTimeAll.Text = "nuiPanel1";
            // 
            // nuiPanel5
            // 
            this.nuiPanel5.Border.Style = Nevron.UI.BorderStyle3D.RaisedInner;
            this.nuiPanel5.Controls.Add(this.lblLondonTime);
            this.nuiPanel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.nuiPanel5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nuiPanel5.Location = new System.Drawing.Point(315, 0);
            this.nuiPanel5.Name = "nuiPanel5";
            this.nuiPanel5.Size = new System.Drawing.Size(105, 20);
            this.nuiPanel5.TabIndex = 1;
            this.nuiPanel5.Text = "nuiPanel2";
            // 
            // lblLondonTime
            // 
            this.lblLondonTime.AutoSize = true;
            this.lblLondonTime.BackColor = System.Drawing.Color.Transparent;
            this.lblLondonTime.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblLondonTime.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLondonTime.Location = new System.Drawing.Point(0, 0);
            this.lblLondonTime.Name = "lblLondonTime";
            this.lblLondonTime.Size = new System.Drawing.Size(29, 13);
            this.lblLondonTime.TabIndex = 4;
            this.lblLondonTime.Text = "Time";
            // 
            // nuiPanel2
            // 
            this.nuiPanel2.Border.Style = Nevron.UI.BorderStyle3D.RaisedInner;
            this.nuiPanel2.Controls.Add(this.lblTokyoTime);
            this.nuiPanel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.nuiPanel2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nuiPanel2.Location = new System.Drawing.Point(210, 0);
            this.nuiPanel2.Name = "nuiPanel2";
            this.nuiPanel2.Size = new System.Drawing.Size(105, 20);
            this.nuiPanel2.TabIndex = 1;
            this.nuiPanel2.Text = "nuiPanel2";
            // 
            // lblTokyoTime
            // 
            this.lblTokyoTime.AutoSize = true;
            this.lblTokyoTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTokyoTime.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTokyoTime.Location = new System.Drawing.Point(0, 0);
            this.lblTokyoTime.Name = "lblTokyoTime";
            this.lblTokyoTime.Size = new System.Drawing.Size(29, 13);
            this.lblTokyoTime.TabIndex = 4;
            this.lblTokyoTime.Text = "Time";
            // 
            // nuiPanel4
            // 
            this.nuiPanel4.BackColor = System.Drawing.Color.Transparent;
            this.nuiPanel4.Border.Style = Nevron.UI.BorderStyle3D.RaisedInner;
            this.nuiPanel4.Controls.Add(this.lblNYTime);
            this.nuiPanel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.nuiPanel4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nuiPanel4.Location = new System.Drawing.Point(105, 0);
            this.nuiPanel4.Name = "nuiPanel4";
            this.nuiPanel4.Size = new System.Drawing.Size(105, 20);
            this.nuiPanel4.TabIndex = 1;
            this.nuiPanel4.Text = "nuiPanel2";
            // 
            // lblNYTime
            // 
            this.lblNYTime.AutoSize = true;
            this.lblNYTime.BackColor = System.Drawing.Color.Transparent;
            this.lblNYTime.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblNYTime.Location = new System.Drawing.Point(0, 0);
            this.lblNYTime.Name = "lblNYTime";
            this.lblNYTime.Size = new System.Drawing.Size(29, 13);
            this.lblNYTime.TabIndex = 3;
            this.lblNYTime.Text = "Time";
            // 
            // nuiPanel3
            // 
            this.nuiPanel3.Border.Style = Nevron.UI.BorderStyle3D.RaisedInner;
            this.nuiPanel3.Controls.Add(this.lblSydTime);
            this.nuiPanel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.nuiPanel3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nuiPanel3.Location = new System.Drawing.Point(0, 0);
            this.nuiPanel3.Name = "nuiPanel3";
            this.nuiPanel3.Size = new System.Drawing.Size(105, 20);
            this.nuiPanel3.TabIndex = 1;
            this.nuiPanel3.Text = "nuiPanel2";
            // 
            // lblSydTime
            // 
            this.lblSydTime.AutoSize = true;
            this.lblSydTime.BackColor = System.Drawing.Color.Transparent;
            this.lblSydTime.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSydTime.Location = new System.Drawing.Point(0, 0);
            this.lblSydTime.Name = "lblSydTime";
            this.lblSydTime.Size = new System.Drawing.Size(29, 13);
            this.lblSydTime.TabIndex = 2;
            this.lblSydTime.Text = "Time";
            // 
            // timeRefresh
            // 
            this.timeRefresh.Interval = 1000;
            this.timeRefresh.Tick += new System.EventHandler(this.timeRefresh_Tick);
            // 
            // nuiPanel6
            // 
            this.nuiPanel6.Location = new System.Drawing.Point(465, 149);
            this.nuiPanel6.Name = "nuiPanel6";
            this.nuiPanel6.Size = new System.Drawing.Size(75, 23);
            this.nuiPanel6.TabIndex = 8;
            this.nuiPanel6.Text = "nuiPanel6";
            // 
            // TraycontextMenuStrip
            // 
            this.TraycontextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem4,
            this.toolStripMenuItem6});
            this.TraycontextMenuStrip.Name = "contextMenuStrip1";
            this.TraycontextMenuStrip.Size = new System.Drawing.Size(112, 48);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(111, 22);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(111, 22);
            this.toolStripMenuItem6.Text = "Close";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.TraycontextMenuStrip;
            this.notifyIcon1.Text = "Galaxy TradeStation";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStripAccount
            // 
            this.contextMenuStripAccount.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsAllHistory,
            this.tslast3months,
            this.tsLastMonth,
            this.tsCustomPeriod,
            this.tsSaveasReport,
            this.tsSaveasDetailedReport,
            this.tsCommissions,
            this.tsTaxes,
            this.tsComments,
            this.toolstripAutoArrange,
            this.toolStripGrid});
            this.contextMenuStripAccount.Name = "contextMenuStripAccount";
            this.contextMenuStripAccount.Size = new System.Drawing.Size(202, 246);
            // 
            // tsAllHistory
            // 
            this.tsAllHistory.Enabled = false;
            this.tsAllHistory.Name = "tsAllHistory";
            this.tsAllHistory.Size = new System.Drawing.Size(201, 22);
            this.tsAllHistory.Text = "All History";
            // 
            // tslast3months
            // 
            this.tslast3months.Enabled = false;
            this.tslast3months.Name = "tslast3months";
            this.tslast3months.Size = new System.Drawing.Size(201, 22);
            this.tslast3months.Text = "Last 3 Months";
            // 
            // tsLastMonth
            // 
            this.tsLastMonth.Enabled = false;
            this.tsLastMonth.Name = "tsLastMonth";
            this.tsLastMonth.Size = new System.Drawing.Size(201, 22);
            this.tsLastMonth.Text = "Last Month ";
            // 
            // tsCustomPeriod
            // 
            this.tsCustomPeriod.Enabled = false;
            this.tsCustomPeriod.Name = "tsCustomPeriod";
            this.tsCustomPeriod.Size = new System.Drawing.Size(201, 22);
            this.tsCustomPeriod.Text = "Custom Period";
            // 
            // tsSaveasReport
            // 
            this.tsSaveasReport.Name = "tsSaveasReport";
            this.tsSaveasReport.Size = new System.Drawing.Size(201, 22);
            this.tsSaveasReport.Text = "Save as Report";
            // 
            // tsSaveasDetailedReport
            // 
            this.tsSaveasDetailedReport.Name = "tsSaveasDetailedReport";
            this.tsSaveasDetailedReport.Size = new System.Drawing.Size(201, 22);
            this.tsSaveasDetailedReport.Text = "Save as Detailed Report";
            // 
            // tsCommissions
            // 
            this.tsCommissions.Enabled = false;
            this.tsCommissions.Name = "tsCommissions";
            this.tsCommissions.Size = new System.Drawing.Size(201, 22);
            this.tsCommissions.Text = "Commissions";
            // 
            // tsTaxes
            // 
            this.tsTaxes.Enabled = false;
            this.tsTaxes.Name = "tsTaxes";
            this.tsTaxes.Size = new System.Drawing.Size(201, 22);
            this.tsTaxes.Text = "Taxes";
            // 
            // tsComments
            // 
            this.tsComments.Enabled = false;
            this.tsComments.Name = "tsComments";
            this.tsComments.Size = new System.Drawing.Size(201, 22);
            this.tsComments.Text = "Comments";
            // 
            // toolstripAutoArrange
            // 
            this.toolstripAutoArrange.Enabled = false;
            this.toolstripAutoArrange.Name = "toolstripAutoArrange";
            this.toolstripAutoArrange.Size = new System.Drawing.Size(201, 22);
            this.toolstripAutoArrange.Text = "Auto Arrange";
            // 
            // toolStripGrid
            // 
            this.toolStripGrid.Enabled = false;
            this.toolStripGrid.Name = "toolStripGrid";
            this.toolStripGrid.Size = new System.Drawing.Size(201, 22);
            this.toolStripGrid.Text = "Grid";
            // 
            // ui_ncbmPALSA
            // 
            this.ui_ncbmPALSA.AllowCustomize = false;
            this.ui_ncbmPALSA.EditorConfig.HasMenuOptionsButton = false;
            this.ui_ncbmPALSA.MenuBarMnemonicsVisibility = Nevron.UI.WinForm.Controls.MenuBarMnemonicsVisibility.Hide;
            this.ui_ncbmPALSA.ParentControl = this;
            this.ui_ncbmPALSA.Toolbars.Add(this.ui_nmnuBar);
            this.ui_ncbmPALSA.Toolbars.Add(this.ui_ndtToolBar);
            this.ui_ncbmPALSA.Toolbars.Add(this.ui_ndtTicker);
            this.ui_ncbmPALSA.Toolbars.Add(this.ui_ndtFilter);
            this.ui_ncbmPALSA.Toolbars.Add(this.ui_nServerStatus);
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
            this.ui_nmnuBar.PrefferedRowIndex = 0;
            this.ui_nmnuBar.RowIndex = 0;
            this.ui_nmnuBar.ShowTooltips = false;
            this.ui_nmnuBar.Text = "Menu Bar";
            // 
            // ui_nmnuFile
            // 
            this.ui_nmnuFile.Commands.AddRange(new Nevron.UI.WinForm.Controls.NCommand[] {
            this.ui_ncmdFileLogin,
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
            this.ui_ncmdViewFullScreen});
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
            // 
            // ui_ncmdLanguagesEnglish
            // 
            this.ui_ncmdLanguagesEnglish.Properties.Text = "English";
            // 
            // ui_ncmdLanguagesHindi
            // 
            this.ui_ncmdLanguagesHindi.Properties.Text = "Hindi";
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
            this.ui_ncmdThemeWood});
            this.ui_ncmdViewThemes.Properties.Text = "Themes";
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
            // ui_ncmdViewTicker
            // 
            this.ui_ncmdViewTicker.Properties.ID = 5;
            this.ui_ncmdViewTicker.Properties.MenuOptions.DisplayTooltips = true;
            this.ui_ncmdViewTicker.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(115, 196608);
            this.ui_ncmdViewTicker.Properties.Text = "Ticker";
            // 
            // ui_ncmdViewTrade
            // 
            this.ui_ncmdViewTrade.Properties.ID = 5;
            this.ui_ncmdViewTrade.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(119, 0);
            this.ui_ncmdViewTrade.Properties.Text = "Trade";
            // 
            // ui_ncmdViewNetPosition
            // 
            this.ui_ncmdViewNetPosition.Properties.ID = 7;
            this.ui_ncmdViewNetPosition.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(117, 131072);
            this.ui_ncmdViewNetPosition.Properties.Text = "Net Position";
            // 
            // ui_ncmdViewMsgLog
            // 
            this.ui_ncmdViewMsgLog.Properties.ID = 8;
            this.ui_ncmdViewMsgLog.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(121, 0);
            this.ui_ncmdViewMsgLog.Properties.Text = "Message Log";
            // 
            // ui_ncmdViewContractInfo
            // 
            this.ui_ncmdViewContractInfo.Properties.ID = 9;
            this.ui_ncmdViewContractInfo.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(119, 65536);
            this.ui_ncmdViewContractInfo.Properties.Text = "Contract Information";
            // 
            // ui_ncmdViewToolBar
            // 
            this.ui_ncmdViewToolBar.Checked = true;
            this.ui_ncmdViewToolBar.Properties.ID = 10;
            this.ui_ncmdViewToolBar.Properties.Text = "Toolbar";
            // 
            // ui_ncmdViewFilterBar
            // 
            this.ui_ncmdViewFilterBar.Checked = true;
            this.ui_ncmdViewFilterBar.Properties.ID = 11;
            this.ui_ncmdViewFilterBar.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(83, 131072);
            this.ui_ncmdViewFilterBar.Properties.Text = "Filter Bar";
            // 
            // ui_ncmdViewMessageBar
            // 
            this.ui_ncmdViewMessageBar.Checked = true;
            this.ui_ncmdViewMessageBar.Properties.ID = 12;
            this.ui_ncmdViewMessageBar.Properties.Text = "Message Bar";
            // 
            // ui_ncmdViewIndexBar
            // 
            this.ui_ncmdViewIndexBar.Properties.Text = "Index Bar";
            this.ui_ncmdViewIndexBar.Properties.Visible = false;
            // 
            // ui_ncmdViewAccountsInfo
            // 
            this.ui_ncmdViewAccountsInfo.Properties.Text = "Accounts Info";
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
            // 
            // ui_ncmdViewFullScreen
            // 
            this.ui_ncmdViewFullScreen.Properties.ID = 19;
            this.ui_ncmdViewFullScreen.Properties.Text = "Full Screen";
            // 
            // ui_nmnuMarket
            // 
            this.ui_nmnuMarket.Commands.AddRange(new Nevron.UI.WinForm.Controls.NCommand[] {
            this.ui_ncmdMarketMarketWatch,
            this.ui_ncmdMarketQuote,
            this.ui_ncmdMarketMarketPicture,
            this.ui_ncmdMarketSnapQuote,
            this.ui_ncmdMarketMarketStatus,
            this.ui_ncmdMarketTopGainerLosers});
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
            // ui_ncmdMarketMarketPicture
            // 
            this.ui_ncmdMarketMarketPicture.Properties.ID = 21;
            this.ui_ncmdMarketMarketPicture.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(117, 0);
            this.ui_ncmdMarketMarketPicture.Properties.Text = "Market Picture";
            // 
            // ui_ncmdMarketSnapQuote
            // 
            this.ui_ncmdMarketSnapQuote.Properties.ID = 22;
            this.ui_ncmdMarketSnapQuote.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(120, 131072);
            this.ui_ncmdMarketSnapQuote.Properties.Text = "Snap Quote";
            // 
            // ui_ncmdMarketMarketStatus
            // 
            this.ui_ncmdMarketMarketStatus.Properties.ID = 23;
            this.ui_ncmdMarketMarketStatus.Properties.Text = "Market Status";
            // 
            // ui_ncmdMarketTopGainerLosers
            // 
            this.ui_ncmdMarketTopGainerLosers.Enabled = false;
            this.ui_ncmdMarketTopGainerLosers.Properties.ID = 24;
            this.ui_ncmdMarketTopGainerLosers.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(119, 196608);
            this.ui_ncmdMarketTopGainerLosers.Properties.Text = "Top Gainers/Losers";
            // 
            // ui_nmnuOrders
            // 
            this.ui_nmnuOrders.Commands.AddRange(new Nevron.UI.WinForm.Controls.NCommand[] {
            this.ui_ncmdOrdersPlaceBuyOrders,
            this.ui_ncmdOrdersPlaceSellOrders,
            this.ui_ncmdOrdersOrderBook});
            this.ui_nmnuOrders.Properties.ShowArrowStyle = Nevron.UI.WinForm.Controls.ShowArrowStyle.Never;
            this.ui_nmnuOrders.Properties.Text = "&Orders";
            // 
            // ui_ncmdOrdersPlaceBuyOrders
            // 
            this.ui_ncmdOrdersPlaceBuyOrders.Properties.ID = 25;
            this.ui_ncmdOrdersPlaceBuyOrders.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(112, 0);
            this.ui_ncmdOrdersPlaceBuyOrders.Properties.Text = "Place Buy Orders";
            // 
            // ui_ncmdOrdersPlaceSellOrders
            // 
            this.ui_ncmdOrdersPlaceSellOrders.Properties.ID = 26;
            this.ui_ncmdOrdersPlaceSellOrders.Properties.Shortcut = new Nevron.UI.WinForm.Controls.NShortcut(113, 0);
            this.ui_ncmdOrdersPlaceSellOrders.Properties.Text = "Place Sell Orders";
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
            this.ui_nmnuTrades.Properties.Text = "&Trades";
            // 
            // ui_ncmdTradesTrades
            // 
            this.ui_ncmdTradesTrades.Properties.ID = 28;
            this.ui_ncmdTradesTrades.Properties.Text = "Trades";
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
            this.ui_mnuCharts.Enabled = false;
            this.ui_mnuCharts.Properties.ShowArrowStyle = Nevron.UI.WinForm.Controls.ShowArrowStyle.Never;
            this.ui_mnuCharts.Properties.Text = "Charts";
            // 
            // ui_mnuChartsNewChart
            // 
            this.ui_mnuChartsNewChart.Enabled = false;
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
            this.ui_mnuPeriodicity4Hour,
            this.ui_mnuPeriodicityDaily,
            this.ui_mnuPeriodicityWeekly,
            this.ui_mnuPeriodicityMonthly});
            this.ui_mnuChartsPeriodicity.Properties.Text = "Periodicity";
            // 
            // ui_mnuPeriodicity1Minute
            // 
            this.ui_mnuPeriodicity1Minute.Enabled = false;
            this.ui_mnuPeriodicity1Minute.Properties.Text = "1 Minute";
            // 
            // ui_mnuPeriodicity5Minute
            // 
            this.ui_mnuPeriodicity5Minute.Enabled = false;
            this.ui_mnuPeriodicity5Minute.Properties.Text = "5 Minute";
            // 
            // ui_mnuPeriodicity15Minute
            // 
            this.ui_mnuPeriodicity15Minute.Enabled = false;
            this.ui_mnuPeriodicity15Minute.Properties.Text = "15 Minute";
            // 
            // ui_mnuPeriodicity30Minute
            // 
            this.ui_mnuPeriodicity30Minute.Enabled = false;
            this.ui_mnuPeriodicity30Minute.Properties.Text = "30 Minute";
            // 
            // ui_mnuPeriodicity1Hour
            // 
            this.ui_mnuPeriodicity1Hour.Enabled = false;
            this.ui_mnuPeriodicity1Hour.Properties.Text = "1 Hour";
            // 
            // ui_mnuPeriodicity4Hour
            // 
            this.ui_mnuPeriodicity4Hour.Enabled = false;
            this.ui_mnuPeriodicity4Hour.Properties.Text = "4 Hour";
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
            this.ui_mnuChartTypeBarChart.Enabled = false;
            this.ui_mnuChartTypeBarChart.Properties.Text = "Bar Chart";
            // 
            // ui_mnuChartTypeCandleChart
            // 
            this.ui_mnuChartTypeCandleChart.Enabled = false;
            this.ui_mnuChartTypeCandleChart.Properties.Text = "Candle Chart";
            // 
            // ui_mnuChartTypeLineChart
            // 
            this.ui_mnuChartTypeLineChart.Enabled = false;
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
            this.ui_nmunPriceTypePointandFigure.Enabled = false;
            this.ui_nmunPriceTypePointandFigure.Properties.Text = "Point and Figure";
            // 
            // ui_nmunPriceTypeRenko
            // 
            this.ui_nmunPriceTypeRenko.Enabled = false;
            this.ui_nmunPriceTypeRenko.Properties.Text = "Renko";
            // 
            // ui_nmunPriceTypeKagi
            // 
            this.ui_nmunPriceTypeKagi.Enabled = false;
            this.ui_nmunPriceTypeKagi.Properties.Text = "Kagi";
            // 
            // ui_nmunPriceTypeThreeLineBreak
            // 
            this.ui_nmunPriceTypeThreeLineBreak.Enabled = false;
            this.ui_nmunPriceTypeThreeLineBreak.Properties.Text = "Three Line Break";
            // 
            // ui_nmunPriceTypeEquiVolume
            // 
            this.ui_nmunPriceTypeEquiVolume.Enabled = false;
            this.ui_nmunPriceTypeEquiVolume.Properties.Text = "Equi Volume";
            // 
            // ui_nmunPriceTypeEquiVolumeShadow
            // 
            this.ui_nmunPriceTypeEquiVolumeShadow.Enabled = false;
            this.ui_nmunPriceTypeEquiVolumeShadow.Properties.Text = "Equi Volume Shadow";
            // 
            // ui_nmunPriceTypeCandleVolume
            // 
            this.ui_nmunPriceTypeCandleVolume.Enabled = false;
            this.ui_nmunPriceTypeCandleVolume.Properties.Text = "Candle Volume";
            // 
            // ui_nmunPriceTypeHeikinAshi
            // 
            this.ui_nmunPriceTypeHeikinAshi.Enabled = false;
            this.ui_nmunPriceTypeHeikinAshi.Properties.Text = "Heikin Ashi";
            // 
            // ui_nmunPriceTypeStandardChart
            // 
            this.ui_nmunPriceTypeStandardChart.Enabled = false;
            this.ui_nmunPriceTypeStandardChart.Properties.Text = "Standard Chart";
            // 
            // ui_mnuChartsZoomIn
            // 
            this.ui_mnuChartsZoomIn.Enabled = false;
            this.ui_mnuChartsZoomIn.Properties.Text = "Zoom In";
            // 
            // ui_mnuChartsZoomOut
            // 
            this.ui_mnuChartsZoomOut.Enabled = false;
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
            this.ui_mnuChartsGrid.Enabled = false;
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
            this.ui_mnuSnapshotPrint.Enabled = false;
            this.ui_mnuSnapshotPrint.Properties.Text = "Print";
            // 
            // ui_mnuSnapshotSave
            // 
            this.ui_mnuSnapshotSave.Enabled = false;
            this.ui_mnuSnapshotSave.Properties.Text = "Save";
            // 
            // ui_nmnuTechnicalAnalysis
            // 
            this.ui_nmnuTechnicalAnalysis.Commands.AddRange(new Nevron.UI.WinForm.Controls.NCommand[] {
            this.ui_nmnuTechnicalAnalysisIndicatorList,
            this.ui_nmnuTechnicalAnalysisAdd});
            this.ui_nmnuTechnicalAnalysis.Enabled = false;
            this.ui_nmnuTechnicalAnalysis.Properties.ShowArrowStyle = Nevron.UI.WinForm.Controls.ShowArrowStyle.Never;
            this.ui_nmnuTechnicalAnalysis.Properties.Text = "Technical Analysis";
            // 
            // ui_nmnuTechnicalAnalysisIndicatorList
            // 
            this.ui_nmnuTechnicalAnalysisIndicatorList.Enabled = false;
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
            this.ui_nmnuTechnicalAnalysisAdd.Properties.Text = "Add";
            // 
            // ui_mnuAddHorizontalLine
            // 
            this.ui_mnuAddHorizontalLine.Enabled = false;
            this.ui_mnuAddHorizontalLine.Properties.Text = "Horizontal Line";
            // 
            // ui_mnuAddVerticalLine
            // 
            this.ui_mnuAddVerticalLine.Enabled = false;
            this.ui_mnuAddVerticalLine.Properties.Text = "Vertical Line";
            // 
            // ui_mnuAddText
            // 
            this.ui_mnuAddText.Enabled = false;
            this.ui_mnuAddText.Properties.Text = "Text";
            // 
            // ui_mnuAddTrendLine
            // 
            this.ui_mnuAddTrendLine.Enabled = false;
            this.ui_mnuAddTrendLine.Properties.Text = "Trend Line";
            // 
            // ui_mnuAddEllipse
            // 
            this.ui_mnuAddEllipse.Enabled = false;
            this.ui_mnuAddEllipse.Properties.Text = "Ellipse";
            // 
            // ui_mnuAddSpeedLines
            // 
            this.ui_mnuAddSpeedLines.Enabled = false;
            this.ui_mnuAddSpeedLines.Properties.Text = "Speed Lines";
            // 
            // ui_mnuAddGannFan
            // 
            this.ui_mnuAddGannFan.Enabled = false;
            this.ui_mnuAddGannFan.Properties.Text = "Gann Fan";
            // 
            // ui_mnuAddFibonacciArcs
            // 
            this.ui_mnuAddFibonacciArcs.Enabled = false;
            this.ui_mnuAddFibonacciArcs.Properties.Text = "Fibonacci Arcs";
            // 
            // ui_mnuAddFibonacciRetracement
            // 
            this.ui_mnuAddFibonacciRetracement.Enabled = false;
            this.ui_mnuAddFibonacciRetracement.Properties.Text = "Fibonacci Retracement";
            // 
            // ui_mnuAddFibonacciFan
            // 
            this.ui_mnuAddFibonacciFan.Enabled = false;
            this.ui_mnuAddFibonacciFan.Properties.Text = "Fibonacci Fan";
            // 
            // ui_mnuAddFibonacciTimezone
            // 
            this.ui_mnuAddFibonacciTimezone.Enabled = false;
            this.ui_mnuAddFibonacciTimezone.Properties.Text = "Fibonacci Timezone";
            // 
            // ui_mnuAddTironeLevel
            // 
            this.ui_mnuAddTironeLevel.Enabled = false;
            this.ui_mnuAddTironeLevel.Properties.Text = "Tirone Level";
            // 
            // ui_mnuAddQuadrentLines
            // 
            this.ui_mnuAddQuadrentLines.Enabled = false;
            this.ui_mnuAddQuadrentLines.Properties.Text = "Quadrent Lines";
            // 
            // ui_mnuAddRafRegression
            // 
            this.ui_mnuAddRafRegression.Enabled = false;
            this.ui_mnuAddRafRegression.Properties.Text = "Raf Regression";
            // 
            // ui_mnuAddErrorChannel
            // 
            this.ui_mnuAddErrorChannel.Enabled = false;
            this.ui_mnuAddErrorChannel.Properties.Text = "Error Channel";
            // 
            // ui_mnuAddRectangle
            // 
            this.ui_mnuAddRectangle.Enabled = false;
            this.ui_mnuAddRectangle.Properties.Text = "Rectangle";
            // 
            // ui_mnuAddFreeHandDrawing
            // 
            this.ui_mnuAddFreeHandDrawing.Enabled = false;
            this.ui_mnuAddFreeHandDrawing.Properties.Text = "Free Hand Drawing";
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
            this.ui_ntbBackup,
            this.ui_ntbPrint,
            this.ui_ntbMessageLog,
            this.ui_ntbOrderEntry,
            this.ui_ntbOrderBook,
            this.ui_ntbTrades,
            this.ui_ntbNetPosition,
            this.ui_ntbMarketWatch,
            this.ui_ntbMarketPicture,
            this.ui_ntbContractInfo,
            this.ui_ntbModifyOrder,
            this.ui_ntbCancelOrder,
            this.ui_ntbCancelAllOrders,
            this.ui_ntbFilter});
            this.ui_ndtToolBar.DefaultLocation = new System.Drawing.Point(2, 26);
            this.ui_ndtToolBar.HasBorder = false;
            this.ui_ndtToolBar.HasPendantCommand = false;
            this.ui_ndtToolBar.ImageSize = new System.Drawing.Size(20, 20);
            this.ui_ndtToolBar.Name = "ui_ndtToolBar";
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
            // ui_ntbBackup
            // 
            this.ui_ntbBackup.Properties.BeginGroup = true;
            this.ui_ntbBackup.Properties.Text = "Online Backup";
            this.ui_ntbBackup.Properties.Visible = false;
            // 
            // ui_ntbPrint
            // 
            this.ui_ntbPrint.Properties.Text = "Print";
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
            // ui_ntbOrderBook
            // 
            this.ui_ntbOrderBook.Properties.Text = "Order Book";
            // 
            // ui_ntbTrades
            // 
            this.ui_ntbTrades.Properties.Text = "Trades";
            // 
            // ui_ntbNetPosition
            // 
            this.ui_ntbNetPosition.Properties.Text = "Net Position";
            // 
            // ui_ntbMarketWatch
            // 
            this.ui_ntbMarketWatch.Properties.BeginGroup = true;
            this.ui_ntbMarketWatch.Properties.Text = "Market Watch";
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
            // ui_ndtTicker
            // 
            this.ui_ndtTicker.BackgroundType = Nevron.UI.WinForm.Controls.BackgroundType.Transparent;
            this.ui_ndtTicker.CommandSize = new System.Drawing.Size(775, 20);
            this.ui_ndtTicker.DefaultLocation = new System.Drawing.Point(0, 57);
            this.ui_ndtTicker.HasBorder = false;
            this.ui_ndtTicker.HasGripper = false;
            this.ui_ndtTicker.HasPendantCommand = false;
            this.ui_ndtTicker.Moveable = false;
            this.ui_ndtTicker.Name = "ui_ndtTicker";
            this.ui_ndtTicker.PrefferedRowIndex = 2;
            this.ui_ndtTicker.RowIndex = 2;
            this.ui_ndtTicker.Text = "Ticker";
            // 
            // ui_ndtFilter
            // 
            this.ui_ndtFilter.CanFloat = false;
            this.ui_ndtFilter.Commands.AddRange(new Nevron.UI.WinForm.Controls.NCommand[] {
            this.ui_ncmbInstrumentType,
            this.ui_ncmbSymbol,
            this.ui_ncmbExpiryDate});
            this.ui_ndtFilter.DefaultLocation = new System.Drawing.Point(0, 82);
            this.ui_ndtFilter.HasBorder = false;
            this.ui_ndtFilter.HasPendantCommand = false;
            this.ui_ndtFilter.Name = "ui_ndtFilter";
            this.ui_ndtFilter.PrefferedRowIndex = 3;
            this.ui_ndtFilter.RowIndex = 3;
            this.ui_ndtFilter.Text = "FilterBar";
            // 
            // ui_ncmbInstrumentType
            // 
            this.ui_ncmbInstrumentType.ControlText = "";
            this.ui_ncmbInstrumentType.ListProperties.ColumnOnLeft = false;
            this.ui_ncmbInstrumentType.Properties.ShowArrowStyle = Nevron.UI.WinForm.Controls.ShowArrowStyle.Never;
            // 
            // ui_ncmbSymbol
            // 
            this.ui_ncmbSymbol.ControlText = "";
            this.ui_ncmbSymbol.ListProperties.ColumnOnLeft = false;
            this.ui_ncmbSymbol.Properties.ShowArrowStyle = Nevron.UI.WinForm.Controls.ShowArrowStyle.Never;
            // 
            // ui_ncmbExpiryDate
            // 
            this.ui_ncmbExpiryDate.ControlText = "";
            this.ui_ncmbExpiryDate.ListProperties.ColumnOnLeft = false;
            this.ui_ncmbExpiryDate.Properties.ShowArrowStyle = Nevron.UI.WinForm.Controls.ShowArrowStyle.Never;
            // 
            // ui_nServerStatus
            // 
            this.ui_nServerStatus.Commands.AddRange(new Nevron.UI.WinForm.Controls.NCommand[] {
            this.ui_ncmdOrderServerStatus,
            this.ui_ncmdDataServerStatus});
            this.ui_nServerStatus.DefaultLocation = new System.Drawing.Point(0, 109);
            this.ui_nServerStatus.HasBorder = false;
            this.ui_nServerStatus.HasGripper = false;
            this.ui_nServerStatus.HasPendantCommand = false;
            this.ui_nServerStatus.Name = "ui_nServerStatus";
            this.ui_nServerStatus.PrefferedRowIndex = 4;
            this.ui_nServerStatus.RowIndex = 4;
            this.ui_nServerStatus.Text = "ServerStatusBar";
            // 
            // ui_ncmdOrderServerStatus
            // 
            this.ui_ncmdOrderServerStatus.Properties.ImageInfo.Image = global::PALSA.Properties.Resources.Circle_Red;
            this.ui_ncmdOrderServerStatus.Properties.MenuOptions.ColumnOnLeft = false;
            this.ui_ncmdOrderServerStatus.Properties.MenuOptions.FitInWorkingArea = true;
            this.ui_ncmdOrderServerStatus.Properties.Selectable = false;
            this.ui_ncmdOrderServerStatus.Properties.ShowArrowStyle = Nevron.UI.WinForm.Controls.ShowArrowStyle.Never;
            this.ui_ncmdOrderServerStatus.Properties.Style = Nevron.UI.WinForm.Controls.CommandStyle.ImageAndText;
            this.ui_ncmdOrderServerStatus.Properties.Text = "Order Server";
            // 
            // ui_ncmdDataServerStatus
            // 
            this.ui_ncmdDataServerStatus.Properties.ImageInfo.Image = global::PALSA.Properties.Resources.Circle_Red;
            this.ui_ncmdDataServerStatus.Properties.Selectable = false;
            this.ui_ncmdDataServerStatus.Properties.ShowArrowStyle = Nevron.UI.WinForm.Controls.ShowArrowStyle.Never;
            this.ui_ncmdDataServerStatus.Properties.Style = Nevron.UI.WinForm.Controls.CommandStyle.ImageAndText;
            this.ui_ncmdDataServerStatus.Properties.Text = "Data Server";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tlstrplblStatusMsg});
            this.statusStrip1.Location = new System.Drawing.Point(0, 710);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1009, 22);
            this.statusStrip1.TabIndex = 11;
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
            this.tlstrplblStatusMsg.Size = new System.Drawing.Size(949, 17);
            this.tlstrplblStatusMsg.Spring = true;
            this.tlstrplblStatusMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tlstrplblStatusMsg.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            // 
            // frmMainGTS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1009, 732);
            this.Controls.Add(this.ui_ndtToolBar);
            this.Controls.Add(this.ui_ndtTicker);
            this.Controls.Add(this.ui_ndtFilter);
            this.Controls.Add(this.ui_nServerStatus);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ui_nmnuBar);
            this.Controls.Add(this.nuiPanel6);
            this.Controls.Add(this.nuiPanel1);
            this.IsMdiContainer = true;
            this.Name = "frmMainGTS";
            this.Text = "ECX TRADER";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMainGTS_FormClosing);
            this.Load += new System.EventHandler(this.frmMainGTS_Load);
            this.MdiChildActivate += new System.EventHandler(this.frmMainGTS_MdiChildActivate);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.frmMainGTS_DragEnter);
            this.Resize += new System.EventHandler(this.frmMainGTS_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.m_DockManager)).EndInit();
            this.nuiPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelTimeAll.ResumeLayout(false);
            this.nuiPanel5.ResumeLayout(false);
            this.nuiPanel5.PerformLayout();
            this.nuiPanel2.ResumeLayout(false);
            this.nuiPanel2.PerformLayout();
            this.nuiPanel4.ResumeLayout(false);
            this.nuiPanel4.PerformLayout();
            this.nuiPanel3.ResumeLayout(false);
            this.nuiPanel3.PerformLayout();
            this.TraycontextMenuStrip.ResumeLayout(false);
            this.contextMenuStripAccount.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ui_ncbmPALSA)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Nevron.UI.WinForm.Docking.NDockManager m_DockManager;
        //public Nevron.UI.WinForm.Docking.NDockingPanel navigatorPanel;
        public Nevron.UI.WinForm.Docking.NDockingPanel SymbolPanel;
        public Nevron.UI.WinForm.Docking.NDockingPanel TerminalPanel;
        //public Nevron.UI.WinForm.Docking.NDockingPanel ExecuitionPanel;
        public Nevron.UI.WinForm.Docking.NDockingPanel QuotePanel;
        public Nevron.UI.WinForm.Docking.NDockingPanel Level2Panel;
        //public uctlAlert uctlAlert1;
        public Nevron.UI.WinForm.Controls.NUIPanel nuiPanel1;
        public Nevron.UI.WinForm.Controls.NUIPanel panelTimeAll;
        public Nevron.UI.WinForm.Controls.NUIPanel nuiPanel5;
        public Nevron.UI.WinForm.Controls.NUIPanel nuiPanel2;
        public Nevron.UI.WinForm.Controls.NUIPanel nuiPanel4;
        public Nevron.UI.WinForm.Controls.NUIPanel nuiPanel3;
        public System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Timer timeRefresh;
        public System.Windows.Forms.Label lblSydTime;
        public System.Windows.Forms.Label lblLondonTime;
        public System.Windows.Forms.Label lblTokyoTime;
        public System.Windows.Forms.Label lblNYTime;
        //public uctlNavigator uctlNavigatorInstance;
        //public uctlMarketWatch uctlMarketWatchInstance;
        //public uctlForex uctlForex_;
        //public GTS.Ctrl.uctlTerminal uctlTerminal1;
        //public uctlExecutionDetail uctlExecutionDetail1;
        public Nevron.UI.WinForm.Controls.NUIPanel nuiPanel6;
        private System.Windows.Forms.ContextMenuStrip TraycontextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripAccount;
        private System.Windows.Forms.ToolStripMenuItem tsAllHistory;
        private System.Windows.Forms.ToolStripMenuItem tslast3months;
        private System.Windows.Forms.ToolStripMenuItem tsLastMonth;
        private System.Windows.Forms.ToolStripMenuItem tsCustomPeriod;
        private System.Windows.Forms.ToolStripMenuItem tsSaveasReport;
        private System.Windows.Forms.ToolStripMenuItem tsSaveasDetailedReport;
        private System.Windows.Forms.ToolStripMenuItem tsCommissions;
        private System.Windows.Forms.ToolStripMenuItem tsTaxes;
        private System.Windows.Forms.ToolStripMenuItem tsComments;
        private System.Windows.Forms.ToolStripMenuItem toolstripAutoArrange;
        private System.Windows.Forms.ToolStripMenuItem toolStripGrid;
        public System.Windows.Forms.ImageList imageList1;
        private Nevron.UI.WinForm.Controls.NMenuBar ui_nmnuBar;
        private Nevron.UI.WinForm.Controls.NCommand ui_nmnuFile;
        public Nevron.UI.WinForm.Controls.NCommand ui_ncmdFileLogin;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdFileLogOff;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdFileSaveWorkSpace;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdFileLoadWorkSpace;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdFileChangePassword;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdFileExit;
        private Nevron.UI.WinForm.Controls.NCommand ui_nmnuView;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdViewLanguages;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdLanguagesEnglish;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdLanguagesHindi;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdLanguagesNepali;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdViewThemes;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmbThemeMacOS;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmbThemeOffice2007Black;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmbThemeOffice2007Blue;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdThemeOffice2007Aqua;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmbThemeOrange;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmbThemeVista;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdThemeOpusAlpha;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdThemeVistaPlus;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdThemeVistaRoyal;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdThemeVistaSlate;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdThemeInspirant;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdThemeSimple;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdThemeRoyal;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdThemeMoonlight;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdThemeAqua;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdThemeWood;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdViewTicker;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdViewTrade;
        public Nevron.UI.WinForm.Controls.NCommand ui_ncmdViewNetPosition;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdViewMsgLog;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdViewContractInfo;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdViewToolBar;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdViewFilterBar;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdViewMessageBar;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdViewIndexBar;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdViewAccountsInfo;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdViewStatusBar;
        private Nevron.UI.WinForm.Controls.NCommand nmnuCmdViewTopStatusBar;
        private Nevron.UI.WinForm.Controls.NCommand nmnuCmdViewMiddleStatusBar;
        private Nevron.UI.WinForm.Controls.NCommand nmnuCmdViewBottomStatusBar;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdViewAdminMsgBar;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdViewIndicesView;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdViewParticipantList;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdViewFullScreen;
        private Nevron.UI.WinForm.Controls.NCommand ui_nmnuMarket;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdMarketMarketWatch;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdMarketQuote;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdMarketMarketPicture;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdMarketSnapQuote;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdMarketMarketStatus;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdMarketTopGainerLosers;
        private Nevron.UI.WinForm.Controls.NCommand ui_nmnuOrders;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdOrdersPlaceBuyOrders;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdOrdersPlaceSellOrders;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdOrdersOrderBook;
        private Nevron.UI.WinForm.Controls.NCommand ui_nmnuTrades;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdTradesTrades;
        private Nevron.UI.WinForm.Controls.NCommand ui_nmnuTools;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdToolsCustomize;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdToolsLockWorkStation;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdToolsPortfolio;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdToolsPreferences;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuCharts;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuChartsNewChart;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuChartsPeriodicity;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuPeriodicity1Minute;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuPeriodicity5Minute;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuPeriodicity15Minute;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuPeriodicity30Minute;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuPeriodicity1Hour;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuPeriodicity4Hour;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuPeriodicityDaily;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuPeriodicityWeekly;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuPeriodicityMonthly;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuChartsChartType;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuChartTypeBarChart;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuChartTypeCandleChart;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuChartTypeLineChart;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuChartsPriceType;
        private Nevron.UI.WinForm.Controls.NCommand ui_nmunPriceTypePointandFigure;
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
        public Nevron.UI.WinForm.Controls.NCommand ui_nmnuTechnicalAnalysis;
        private Nevron.UI.WinForm.Controls.NCommand ui_nmnuTechnicalAnalysisIndicatorList;
        private Nevron.UI.WinForm.Controls.NCommand ui_nmnuTechnicalAnalysisAdd;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuAddHorizontalLine;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuAddVerticalLine;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuAddText;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuAddTrendLine;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuAddEllipse;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuAddSpeedLines;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuAddGannFan;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuAddFibonacciArcs;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuAddFibonacciRetracement;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuAddFibonacciFan;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuAddFibonacciTimezone;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuAddTironeLevel;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuAddQuadrentLines;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuAddRafRegression;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuAddErrorChannel;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuAddRectangle;
        private Nevron.UI.WinForm.Controls.NCommand ui_mnuAddFreeHandDrawing;
        private Nevron.UI.WinForm.Controls.NCommand ui_nmnuWindows;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdWindowNewWindow;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdWindowClose;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdWindowCloseAll;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdWindowCascade;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdWindowTileHorizontally;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdWindowTileVertically;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdWindowWindow;
        private Nevron.UI.WinForm.Controls.NCommand ui_nmnuSurveillance;
        public Nevron.UI.WinForm.Controls.NCommand ui_nmnuSurveillanceSurveillance;
        private Nevron.UI.WinForm.Controls.NCommand ui_nmnuHelp;
        private Nevron.UI.WinForm.Controls.NCommand ui_nmnuHelpAboutUs;
        private Nevron.UI.WinForm.Controls.NDockingToolbar ui_ndtToolBar;
        private Nevron.UI.WinForm.Controls.NCommand ui_ntbLogin;
        private Nevron.UI.WinForm.Controls.NCommand ui_ntbLogoff;
        private Nevron.UI.WinForm.Controls.NCommand ui_ntbChangePassword;
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
        private Nevron.UI.WinForm.Controls.NCommand ui_ntbFilter;
        private Nevron.UI.WinForm.Controls.NDockingToolbar ui_ndtTicker;
        private Nevron.UI.WinForm.Controls.NDockingToolbar ui_ndtFilter;
        private Nevron.UI.WinForm.Controls.NComboBoxCommand ui_ncmbInstrumentType;
        private Nevron.UI.WinForm.Controls.NComboBoxCommand ui_ncmbSymbol;
        private Nevron.UI.WinForm.Controls.NComboBoxCommand ui_ncmbExpiryDate;
        private Nevron.UI.WinForm.Controls.NDockingToolbar ui_nServerStatus;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdOrderServerStatus;
        private Nevron.UI.WinForm.Controls.NCommand ui_ncmdDataServerStatus;
        private Nevron.UI.WinForm.Controls.NCommandBarsManager ui_ncbmPALSA;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        public System.Windows.Forms.ToolStripStatusLabel tlstrplblStatusMsg;
        //public Nevron.UI.WinForm.Controls.NStatusBarPanel nStatusBarOpenPLPanel;
       
    }
}