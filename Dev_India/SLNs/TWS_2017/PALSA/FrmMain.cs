///////REVISION HISTORY/////////////////////////////////////////////////////////////////////////////////////////////////////
//DATE			INITIALS	DESCRIPTION	
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using CommonLibrary.Cls;
using CommonLibrary.UserControls;
//using Logging;
using Nevron.GraphicsCore;
using Nevron.UI;
using Nevron.UI.WinForm.Controls;
using PALSA.Cls;
using PALSA.DS;
using PALSA.Frm;
using PALSA.Properties;
using System.Data;
using System.ComponentModel;
using PALSA.uctl;
using Nevron.UI.WinForm.Docking;
using Nevron.Serialization;
using M4.Workspace;


namespace PALSA
{
    /// <summary>
    /// Contains code for Mainform (PALSA)
    /// </summary>
    public partial class FrmMain : NForm
    {
        private static FrmMain _instance;
        private readonly List<Form> _ChildFormList = new List<Form>();
        private readonly Hashtable _CommandBarHash = new Hashtable();
        private readonly UctlTickerTape _objTickerTape = new UctlTickerTape();
        private readonly UctlLogin _objuctlLogin = new UctlLogin();
        //public bool IsAnnonymous = false;
        private readonly Form objForm = new Form();
        public Hashtable _hotKeySettingsHashTable;
        private ConnectionIPs _objConnectionIPs = new ConnectionIPs();
        private object _profiles;
        private Hashtable _revercedHotKeySettingsHashTable;
        NDockingPanelHost nDockPanelHost1;
        NDockZone nDockZone1;
        NDockingPanelHost nDockPanelHost2;
        NDockingFrameworkState state = null;
        private Thread _splashThread;
        public string UserName = string.Empty;

        private Keys _shortcutKeyOrderEntry;
        private Keys _shortcutKeyCancelAllOrders;
        private static Keys _shortcutKeyFilter;

        private Keys _shortcutKeyMarketPicture = Keys.None;
        private string _DeafultWorkSpacePath = string.Empty;
        private Keys _shortcutKeyModifyTrades;
        private PALSASettings objPALSASettings = new PALSASettings();
        BackgroundWorker backgroundWorker1 = new BackgroundWorker();
        public ctlMarketWatch _ctlMW;
        ctlMarketQuote _ctlMQ;
        ctlHistory _ctlTH;
        ctlNetPosition _ctlNP;
        ctlOrders _ctlOB;
        ctlScanner _ctlScanner;
        uctlAlert _ctlAlert;
        uctlMailGrid _ctlMail;
        ctlRadarMain _ctlRadar;
        ctlAccountsToTrade _ctlAccount;
        ctlPendingOrders _ctlPendingOrders;
        private MarqueeLabel marqueeLabel1;
        private static object _portfolio;
        private static Mutex _mutex;

        /////////////////////////////////////

        private static bool isSingleInstance()
        {
            _mutex = new Mutex(false, "Terminal");

            // keep the mutex reference alive until the normal 
            //termination of the program
            GC.KeepAlive(_mutex);

            try
            {
                return _mutex.WaitOne(0, false);
            }
            catch (AbandonedMutexException)
            {
                // if one thread acquires a Mutex object 
                //that another thread has abandoned 
                //by exiting without releasing it

                _mutex.ReleaseMutex();
                return _mutex.WaitOne(0, false);
            }
        }



        /// <summary>
        /// Called when form componenets is initialized
        /// </summary>
        public FrmMain()
        {
            if (!isSingleInstance())
            {
                MessageBox.Show("Instance already running");
                this.Close();
            }
            else
            {
                this.Visible = false;
                InitializeComponent();
                marqueeLabel1 = new MarqueeLabel();
                Thread contract = new Thread(ContractThreadHandler);
                contract.IsBackground = true;
                contract.Start();
            }

        }

        private void ContractThreadHandler()
        {
            try
            {
                ClsTWSContractManager.INSTANCE.LoadIntialData();
            }
            catch (Exception)
            {

            }
        }

        public static FrmMain INSTANCE
        {
            get { return _instance ?? (_instance = new FrmMain()); }
        }

        public List<string> GetSymbolsOfPortfolio(string portfolioName)
        {
            List<string> lstSymbols = new List<string>();
            ClsPortfolio portfolio = ((Dictionary<string, ClsPortfolio>)_portfolio)[portfolioName];
            foreach (string instrumentId in portfolio.Products.Keys)
            {
                ClsContracts contract = portfolio.Products[instrumentId];
                lstSymbols.Add(contract.Contract);
            }
            return lstSymbols;
        }

        public List<string> GetKeysOfPortfolio(string portfolioName)
        {
            ClsPortfolio portfolio = ((Dictionary<string, ClsPortfolio>)_portfolio)[portfolioName];
            return portfolio.Products.Keys.ToList<string>();
        }

        public object Profiles
        {
            get { return _profiles; }
            set { _profiles = value; }
        }

        public string StatusMessage
        {
            get { return tlstrplblStatusMsg.Text; }
            set { tlstrplblStatusMsg.Text = value; }
        }

        public static object Portfolio
        {
            get { return _portfolio; }
            set { _portfolio = value; }
        }

        /// <summary>
        /// Set ticker values from preferences
        /// </summary>
        /// <param name="tickerSpeed"></param>
        public void SetTickerValues(int tickerSpeed)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into SetTickerValues Method");

            _objTickerTape.SetTickerSpeed(tickerSpeed);
            if (_objTickerTape._currentTickerPortfolio != null && _objTickerTape._currentTickerPortfolio != "---SELECT---")
            {
                AddDataToTicker(_objTickerTape._currentTickerPortfolio);
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from SetTickerValues Method");
        }


        private void LoadServerIps()
        {
            

            //string strSetting = Application.StartupPath + "\\ServerHostIps.xml";
            //var objStreamReader = new StreamReader(strSetting);
            //var objXmlSerializer = new XmlSerializer(typeof(ConnectionIPs));
            //_objConnectionIPs = (ConnectionIPs)objXmlSerializer.Deserialize(objStreamReader);

            string strSetting = Application.StartupPath + "\\ServerHostIps.xml";
            var objStreamReader = new StreamReader(strSetting);
            try
            {
                XmlSerializer _objXmlSerializer = XmlSerializer.FromTypes(new[] { typeof(ConnectionIPs) })[0];
                _objConnectionIPs = (ConnectionIPs)_objXmlSerializer.Deserialize(objStreamReader);
            }
            catch (Exception)
            {
            }
        }

        //public string GetSharedDocsFolder()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    int CommonDocumentsFolder = 0x002e;
        //    SHGetFolderPath(IntPtr.Zero, CommonDocumentsFolder, IntPtr.Zero, 0x0000, sb);
        //    return sb.ToString();
        //}


        private static void LoadSplashScreen()
        {
            using (var splash = new SplashScreen { TopMost = true })
            {
                splash.ShowDialog();
            }
            //clsSplashScreen.UdpateStatusText("Loading Items.");
            //Thread.Sleep(200);
            //clsSplashScreen.UdpateStatusText("Loading Items..");
            //Thread.Sleep(200);
            //clsSplashScreen.UdpateStatusText("Loading Items...");
            //Thread.Sleep(200);
            //clsSplashScreen.UdpateStatusText("Loading Items....");
            //Thread.Sleep(200);
            //clsSplashScreen.UdpateStatusText("Items Loaded !!");
            //Thread.Sleep(100);           
        }
        /// <summary>
        /// Sets the initial values for the form(Loads last workspace and theme)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_Load(object sender, EventArgs e)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into FrmMain_Load Method");

            this.Visible = false;
            this.Icon = Resources.favicon;
            _splashThread = new Thread(LoadSplashScreen);
            _splashThread.Start();
            _DeafultWorkSpacePath = ClsPalsaUtility.DeafultWorkSpacePath();
            this.marqueeLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.marqueeLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.marqueeLabel1.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.marqueeLabel1.ForeColor = System.Drawing.Color.Navy;
            this.marqueeLabel1.Location = new System.Drawing.Point(-2, 7);
            this.marqueeLabel1.Name = "marqueeLabel1";
            this.marqueeLabel1.tmrScroll.Interval = 100;
            this.marqueeLabel1.Size = new System.Drawing.Size(384, 156);
            this.marqueeLabel1.TabIndex = 7;
            this.marqueeLabel1.Tag = "";
            this.marqueeLabel1.Dock = DockStyle.Fill;
            this.marqueeLabel1.DisplayText = this.Text;
            //"Dawish Financial Management Limited, Regulated by Financial Service Planning, New Zealand (FSP - NZD)";

            nDockPanelHost1 = new Nevron.UI.WinForm.Docking.NDockingPanelHost();
            nDockZone1 = new Nevron.UI.WinForm.Docking.NDockZone();
            nDockPanelHost2 = new Nevron.UI.WinForm.Docking.NDockingPanelHost();
            //if(!DesignMode)
            //{
            //  
            // nDockPanelHost1
            //   
            nDockPanelHost1.Name = "nDockPanelHost1";
            nDockPanelHost1.Orientation = System.Windows.Forms.Orientation.Vertical;
            nDockPanelHost1.Index = 0;
            //  
            // nDockZone1
            // 
            nDockZone1.AddChild(nDockPanelHost1, 0);
            nDockZone1.AddChild(this.m_DockManager.DocumentManager.DocumentViewHost, 1);
            nDockZone1.Name = "nDockZone1";
            nDockZone1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            nDockZone1.Index = 0;
            //  
            // nDockPanelHost2
            //  
            nDockPanelHost2.Name = "nDockPanelHost2";
            nDockPanelHost2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            nDockPanelHost2.Index = 1;

            this.m_DockManager.RootContainer.RootZone.AddChild(nDockZone1, 0);
            this.m_DockManager.RootContainer.RootZone.AddChild(nDockPanelHost2, 1);
            this.m_DockManager.DocumentManager.DocumentInserted += new DocumentEventHandler(DocumentManager_DocumentInserted);
            this.m_DockManager.DocumentManager.DocumentClosing += new DocumentCancelEventHandler(DocumentManager_DocumentClosing);
            ////}

            ui_ndtTicker.Controls.Add(marqueeLabel1);
            ui_ndtTicker.CommandSize = new Size(ClientSize.Width, 25);
            ui_ndtTicker.MaximumSize = new Size(ClientSize.Width, 25);
            //ToolbarChart.CommandSize = new Size(ClientSize.Width, 25);
            ToolbarChart.Size = new Size(ClientSize.Width, ToolbarChart.Size.Height);

            //Properties.Settings.Default.UserName = "LTech India";
            Properties.Settings.Default.UserName = "FutureX Trading";
            //string osName = getOSInfo();
            state = new NDockingFrameworkState(m_DockManager);
            state.PersistDocuments = true;
            state.PersistStyles = true;
            state.StateRestored += new EventHandler(state_StateRestored);//By Kuldeep for chats context menu issue
            state.ResolveDocumentClient += new ResolveClientEventHandler(state_ResolveDocumentClient);
            state.Format = PersistencyFormat.Binary;

            //if (osName.Contains("XP") || osName.Contains("2000") || osName.Contains("NT"))
            //{
            //    string message = string.Empty;
            //    FileInfo fsInfo = new FileInfo(Application.StartupPath + "\\xlogger.ini");
            //    if (fsInfo.Exists)
            //        fsInfo.Delete();
            //    message = "[xlogger]" + Environment.NewLine +
            //                  "path='" + System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\DAWISH_IT\\ClientDllLogs'" + Environment.NewLine +
            //                  "prefix=ClientDlls" + Environment.NewLine + "max_size=1024000" + Environment.NewLine +
            //                  "# 0 - none" + Environment.NewLine +
            //                  "# 1 - error" + Environment.NewLine +
            //                  "# 2 - info" + Environment.NewLine +
            //                  "# 3 - verbose" + Environment.NewLine +
            //                  "# 4 - debug" + Environment.NewLine +
            //                  "# 5 - always" + Environment.NewLine +
            //                  "log_level=5";

            //    var fsInOut = new FileStream(Application.StartupPath + "\\xlogger.ini", FileMode.OpenOrCreate,
            //    FileAccess.ReadWrite,
            //    FileShare.ReadWrite);
            //    StreamWriter swInOut = new StreamWriter(fsInOut);
            //    swInOut.Write(message);
            //    swInOut.AutoFlush = true;
            //}

            SetCommandsIDs();
            DisableCommandIDs();
            //this.m_DockManager.RootContainer.Enabled = false;
            var obj = new CommandEventArgs(new NCommand());
            obj.Command.Properties.ID = Properties.Settings.Default.Theme;
            ui_ncbmPALSA_CommandClicked(null, obj);
            LoadImages();
            //TickerMenuHandeler();

            clsTWSOrderManagerJSON.INSTANCE.OnLogonResponse -= new Action<string, string, bool>(INSTANCE_OnLogonResponce);
            clsTWSOrderManagerJSON.INSTANCE.OnParticipantResponse -= new Action<Dictionary<int, DataRow>>(INSTANCE_OnParticipantResponse);
            clsTWSOrderManagerJSON.INSTANCE.OnLogonResponse += new Action<string, string, bool>(INSTANCE_OnLogonResponce);
            clsTWSOrderManagerJSON.INSTANCE.OnParticipantResponse += new Action<Dictionary<int, DataRow>>(INSTANCE_OnParticipantResponse);
            //Namo 21 March
            //clsTWSOrderManagerJSON.INSTANCE.SendMessage -= new Action<string>(INSTANCE_SendMessage);
            //clsTWSOrderManagerJSON.INSTANCE.SendMessage += new Action<string>(INSTANCE_SendMessage);
            //if (LoginMenuHandler() != DialogResult.Cancel)
            //    ui_nmnuBar.Enabled = true;

            ui_nmnuWindows.Select -= ui_nmnuWindows_Select;
            ui_nmnuWindows.Select += ui_nmnuWindows_Select;
            Portfolio = GetPortfolios(_objuctlLogin.UserCode);
            //PALSA.Cls.ClsGlobal.LatestPortfolio = Portfolio;
            GetProfiles();

            _ctlMW = new ctlMarketWatch(Portfolio) { ShortcutKeyBOE = _shortcutKeyOrderEntry };
            _ctlMW.OnScriptPortfolioApplyClick += new Action<string>(_ctlMW_OnScriptPortfolioApplyClick);
            _ctlMW.ShortcutKeyMarketPicture = _shortcutKeyMarketPicture;
            _ctlMW.OnSymbolChartClick -= new Action<DataGridViewRow>(_ctlMW_OnSymbolChartClick);
            _ctlMW.OnSymbolChartClick += new Action<DataGridViewRow>(_ctlMW_OnSymbolChartClick);
            _ctlMW.OnSymbolLevel2Click -= new Action<DataGridViewRow>(_ctlMW_OnSymbolLevel2Click);
            _ctlMW.OnSymbolLevel2Click += new Action<DataGridViewRow>(_ctlMW_OnSymbolLevel2Click);
            _ctlMW.OnSymbolMatrixClick -= new Action<DataGridViewRow>(_ctlMW_OnSymbolMatrixClick);
            _ctlMW.OnSymbolMatrixClick += new Action<DataGridViewRow>(_ctlMW_OnSymbolMatrixClick);

            //_ctlTH = new ctlTradeHistory(Profiles, string.Empty, _shortcutKeyFilter);
            _ctlTH = new ctlHistory(Profiles, string.Empty, _shortcutKeyFilter);

            _ctlNP = new ctlNetPosition(Profiles, string.Empty);
            //_ctlOB = new ctlOrderBook(Profiles, string.Empty, _shortcutKeyFilter);
            //var orderStatus = new List<string>();
            //orderStatus.Clear();
            //orderStatus.Add("All");
            //foreach (string x in PALSA.Cls.ClsGlobal.DDOrderStatus.Keys.ToArray())
            //{
            //    string c = x.Replace('_', ' ').ToLower();
            //    c = ClsPalsaUtility.UppercaseWords(c);
            //    orderStatus.Add(c);
            //}           
            _ctlOB = new ctlOrders(Profiles, string.Empty);
            _ctlAlert = new uctlAlert();
            _ctlAccount = new ctlAccountsToTrade();
            _ctlMail = new uctlMailGrid();
            _ctlScanner = new ctlScanner(this);
            _ctlMQ = new ctlMarketQuote(Portfolio);
            _ctlPendingOrders = new ctlPendingOrders(Profiles, "");
            _ctlRadar = new ctlRadarMain(Portfolio, this);

            //ThreadPool.QueueUserWorkItem(new WaitCallback( delegate(object stat)
            //{ 

            if (Properties.Settings.Default.TradeHistoryInDoc)
            {
                CreateNuiDocument(_ctlTH, "Market Quote");
            }
            else
            {
                if (Properties.Settings.Default.TradeHistoryZone == 1)
                {
                    nDockPanelHost1.AddChild(this.History, Properties.Settings.Default.TradeHistoryIndex);
                }
                else
                {
                    nDockPanelHost2.AddChild(this.History, Properties.Settings.Default.TradeHistoryIndex);
                }
                AddToPanel(_ctlTH);
            }

            if (Properties.Settings.Default.NetPositionInDoc)
            {
                CreateNuiDocument(_ctlNP, "Positions");
            }
            else
            {
                if (Properties.Settings.Default.NetPositionZone == 1)
                {
                    nDockPanelHost1.AddChild(this.Openpositions, Properties.Settings.Default.NetPositionIndex);
                }
                else
                {
                    nDockPanelHost2.AddChild(this.Openpositions, Properties.Settings.Default.NetPositionIndex);
                }
                AddToPanel(_ctlNP);
            }


            if (Properties.Settings.Default.OrderHistoryInDoc)
            {
                CreateNuiDocument(_ctlOB, "Order History");
            }
            else
            {
                if (Properties.Settings.Default.OrderHistoryZone == 1)
                {
                    nDockPanelHost1.AddChild(this.OrderBook, Properties.Settings.Default.OrderHistoryIndex);
                }
                else
                {
                    nDockPanelHost2.AddChild(this.OrderBook, Properties.Settings.Default.OrderHistoryIndex);
                }
                AddToPanel(_ctlOB);
            }

            if (Properties.Settings.Default.AlertsInDoc)
            {
                CreateNuiDocument(_ctlAlert, "Alerts");
            }
            else
            {
                if (Properties.Settings.Default.AlertsZone == 1)
                {
                    nDockPanelHost1.AddChild(this.Alerts, Properties.Settings.Default.AlertsIndex);
                }
                else
                {
                    nDockPanelHost2.AddChild(this.Alerts, Properties.Settings.Default.AlertsIndex);
                }
                AddToPanel(_ctlAlert);
            }

            if (Properties.Settings.Default.AccountsInDoc)
            {
                CreateNuiDocument(_ctlAccount, "Accounts");
            }
            else
            {
                if (Properties.Settings.Default.AccountsZone == 1)
                {
                    nDockPanelHost1.AddChild(this.Accounts, Properties.Settings.Default.AccountsIndex);
                }
                else
                {
                    nDockPanelHost2.AddChild(this.Accounts, Properties.Settings.Default.AccountsIndex);
                }
                AddToPanel(_ctlAccount);
            }

            if (Properties.Settings.Default.MailInDoc)
            {
                CreateNuiDocument(_ctlMail, "Mails");
            }
            else
            {
                if (Properties.Settings.Default.MailZone == 1)
                {
                    nDockPanelHost1.AddChild(this.MailBox, Properties.Settings.Default.MailIndex);
                }
                else
                {
                    nDockPanelHost2.AddChild(this.MailBox, Properties.Settings.Default.MailIndex);
                }
                AddToPanel(_ctlMail);
            }
            nDockPanelHost2.AddChild(this.PendingOrders, Properties.Settings.Default.NetPositionIndex);
            AddToPanel(_ctlPendingOrders);
            if (Properties.Settings.Default.ScannerInDoc)
            {
                CreateNuiDocument(_ctlScanner, "Scanner");
            }
            else
            {
                if (Properties.Settings.Default.ScannerZone == 1)
                {
                    nDockPanelHost1.AddChild(this.Scanner, Properties.Settings.Default.ScannerIndex);
                }
                else
                {
                    nDockPanelHost2.AddChild(this.Scanner, Properties.Settings.Default.ScannerIndex);
                }
                AddToPanel(_ctlScanner);
            }

            if (Properties.Settings.Default.RadarInDoc)
            {
                CreateNuiDocument(_ctlRadar, "Radar");
            }
            else
            {
                if (Properties.Settings.Default.RadarZone == 1)
                {
                    nDockPanelHost1.AddChild(this.Radar, Properties.Settings.Default.RadarIndex);
                }
                else
                {
                    nDockPanelHost2.AddChild(this.Radar, Properties.Settings.Default.RadarIndex);
                }
                AddToPanel(_ctlRadar);
            }

            if (Properties.Settings.Default.MarketWatchInDoc)
            {
                CreateNuiDocument(_ctlMW, "Market Watch");
            }
            else
            {
                if (Properties.Settings.Default.MarketWatchZone == 1)
                {
                    nDockPanelHost1.AddChild(this.SymbolPanel, Properties.Settings.Default.MarketWatchIndex);
                }
                else
                {
                    nDockPanelHost2.AddChild(this.SymbolPanel, Properties.Settings.Default.MarketWatchIndex);
                }
                AddToPanel(_ctlMW);
            }

            if (Properties.Settings.Default.MarketQuoteInDoc)
            {
                CreateNuiDocument(_ctlMQ, "Market Quote");
            }
            else
            {
                if (Properties.Settings.Default.MarketQuoteZone == 1)
                {
                    nDockPanelHost1.AddChild(this.QuotePanel, Properties.Settings.Default.MarketQuoteIndex);
                }
                else
                {
                    nDockPanelHost2.AddChild(this.QuotePanel, Properties.Settings.Default.MarketQuoteIndex);
                }
                AddToPanel(_ctlMQ);
            }
            nDockPanelHost2.SelectedIndex = 0;
            nDockPanelHost1.SelectedIndex = 0;
            if (m_DockManager.DocumentManager.Documents.Any())
            {
                m_DockManager.DocumentManager.ActiveDocument = m_DockManager.DocumentManager.Documents[0];
            }
            clsTWSOrderManagerJSON.INSTANCE.Refresh();
            clsTWSDataManagerJSON.INSTANCE.Refresh();

            //if (ClsTWSContractManager.INSTANCE.ReadSymbolsFromFile(ClsPalsaUtility.GetSymbolsFilePath()))
            //{
            LoadServerIps();
            clsTWSDataManagerJSON.INSTANCE.OnDataServerConnectionEvnt -= INSTANCE_OnDataServerConnectionEvnt;
            clsTWSDataManagerJSON.INSTANCE.OnDataServerConnectionEvnt += INSTANCE_OnDataServerConnectionEvnt;
            clsTWSOrderManagerJSON.INSTANCE.OnOrderServerConnectionEvnt -= INSTANCE_OnOrderServerConnectionEvnt;
            clsTWSOrderManagerJSON.INSTANCE.OnOrderServerConnectionEvnt += INSTANCE_OnOrderServerConnectionEvnt;
            clsTWSOrderManagerJSON.INSTANCE.OnBothServerConnectionEvnt -= new Action<string>(INSTANCE_OnBothServerConnectionEvnt);
            clsTWSOrderManagerJSON.INSTANCE.OnBothServerConnectionEvnt += new Action<string>(INSTANCE_OnBothServerConnectionEvnt);

            if (Properties.Settings.Default.SavePassword && Portfolio != null)
            {
                string username = Properties.Settings.Default.LoginName;
                string pwd = Properties.Settings.Default.LoginPassword;
                _objuctlLogin.UserCode = username;
                _objuctlLogin.Password = pwd;                
                clsTWSDataManagerJSON.INSTANCE.Init(username, pwd, _objConnectionIPs.QuotesIP.WebSocketHostUrl);
                clsTWSOrderManagerJSON.INSTANCE.Init(username, pwd, _objConnectionIPs.OrderIP.WebSocketHostUrl);
                
            }
            
            LoadDefaultWorkSpace();
            cbIndicators.Properties.Visible = false;//Namo
            ui_nmnuTechnicalAnalysisIndicatorList.Properties.Visible = false;//Namo

            
        }

        void DocumentManager_DocumentClosing(object sender, DocumentCancelEventArgs e)
        {
            if (e.Document.Client is ctlNewChart)
            {
                ((ctlNewChart)e.Document.Client).SaveInFile(ClsPalsaUtility.GetChartFolder() + e.Document.Key + ".icx");
            }
        }

        void DocumentManager_DocumentInserted(object sender, DocumentEventArgs e)
        {
            m_DockManager.DocumentManager.ActiveDocument = e.Document;
        }

        void _ctlMW_OnSymbolMatrixClick(DataGridViewRow obj)
        {
            string InsID = obj.Cells["ClmInstrumentId"].Value.ToString();
            string symbol = obj.Cells["ClmContractName"].Value.ToString();
            double High = 0;
            double Low = 0;
            if (!DBNull.Value.Equals(obj.Cells["ClmHigh"].Value))
            {
                High = Convert.ToDouble(obj.Cells["ClmHigh"].Value);
            }
            if (!DBNull.Value.Equals(obj.Cells["ClmLow"].Value))
            {
                Low = Convert.ToDouble(obj.Cells["ClmLow"].Value);
            }
            string Expiry = obj.Cells["ClmExpiry"].Value.ToString();


            if (!DocumentWindowIsAlreadyOpen("Matrix"))
            {
                if (High != 0 && Low != 0)
                {
                    //string high = High.ToString("0.00000");
                    ctlMatrix ctlMat = new ctlMatrix(this, InsID, symbol, Expiry, High, Low);
                    CreateNuiDocument(ctlMat, symbol + "-Matrix");
                }
                else
                {
                    MessageBox.Show("Please Check High/Low Prices", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Matrix is already opened", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }



        void _ctlMW_OnSymbolLevel2Click(DataGridViewRow obj)
        {
            string InsID = obj.Cells["ClmInstrumentId"].Value.ToString();
            string symbol = obj.Cells["ClmContractName"].Value.ToString();

            if (!DocumentWindowIsAlreadyOpen("Market Depth"))
            {
                ctlMarketDepth ctlMkt = new ctlMarketDepth(this, InsID);
                ctlMkt.InitializeDepthFromGrid(obj);
                //ctlChart.InitChartData(obj);
                CreateNuiDocument(ctlMkt, symbol + "-Market Depth");
            }
            else
            {
                MessageBox.Show("Market Depth is already opened", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void _ctlMW_OnScriptPortfolioApplyClick(string obj)
        {
            foreach (NUIDocument doc in m_DockManager.DocumentManager.Documents)
            {
                if (doc.Key.Contains("Market Watch"))
                {
                    doc.Key = obj + "-" + "Market Watch";
                }
            }
        }

        void state_StateRestored(object sender, EventArgs e)
        {
            m_DockManager.DocumentManager.DocumentView.Activate();

            // AddIndicatorsInCombo();
        }

        private object GetDefaultPortfolio()
        {
            object objportfolio = null;
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into GetPortfolios Method");

            if (File.Exists(ClsPalsaUtility.GetDefaultPortfolioFile()))
            {
                FileStream streamRead = File.OpenRead(ClsPalsaUtility.GetDefaultPortfolioFile());

                var binaryRead = new BinaryFormatter();
                objportfolio = binaryRead.Deserialize(streamRead);
                streamRead.Close();
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from GetPortfolios Method");
            return objportfolio;
        }

        private bool DocumentWindowIsAlreadyOpen(string documentName)
        {
            NUIDocument document = m_DockManager.DocumentManager.GetDocumentByText(documentName);
            if (null == document)
            {
                document = m_DockManager.DocumentManager.GetDocumentByKey(documentName);
            }

            //if (m_ExternalWindow != null && m_ExternalWindow.Count > 0)
            //{
            //    document = ((frmExternalChartWindow)m_ExternalWindow[0]).m_DockManager.DocumentManager.GetDocumentByText(documentName);

            //    if (null == document)
            //    {
            //        document = ((frmExternalChartWindow)m_ExternalWindow[0]).m_DockManager.DocumentManager.GetDocumentByKey(documentName);
            //    }
            //}

            return document != null;
        }


        public static bool LoggedInSuccess = false;

        //private void INSTANCE_OnLogonResponce(string reason, string brokerName, string traderType)
        //{
        //    //FileHandling.WriteDevelopmentLog("LogOnResponce : Enter into LogonResponce Method");
        //    if (reason == "VALID")
        //    {
        //        UserName = _objuctlLogin.UserCode;
        //        this.m_DockManager.RootContainer.Enabled = true;
        //        //if (File.Exists(Application.StartupPath + "\\" + _objuctlLogin.UserCode + ".xml"))
        //        //{
        //        //    clsTWSOrderManagerJSON.INSTANCE.messageLogDS.dtMessageLog.ReadXml(Application.StartupPath + "\\" + _objuctlLogin.UserCode + ".xml");
        //        //}
        //        //FileHandling.WriteDevelopmentLog("LogOnResponce : Login SuccessFull.");
        //        LoggedInSuccess = true;
        //        clsTWSOrderManagerJSON.INSTANCE.Refresh();
        //        clsTWSDataManagerJSON.INSTANCE.Refresh();

        //        Portfolio = GetPortfolios(_objuctlLogin.UserCode);
        //        _ctlMW.ObjPortfolio = Portfolio;
        //        PALSA.Cls.ClsGlobal.LatestPortfolio = Portfolio;
        //        //if (this.IsAnnonymous)
        //        //{
        //        //    _ctlMQ.uctlForex1.contextForexMenuStrip.Enabled = false;
        //        //    _ctlMW.uctlMarketWatch1.ui_uctlGridMarketWatch.ContextMenuStrip.Enabled = false;
        //        //}
        //        //else
        //        //{
        //        if (_objuctlLogin.SavePassword)
        //        {
        //            Properties.Settings.Default.SavePassword = _objuctlLogin.SavePassword;
        //            Properties.Settings.Default.LoginPassword = _objuctlLogin.Password;
        //            Properties.Settings.Default.LoginName = _objuctlLogin.UserCode;
        //        }

        //        Properties.Settings.Default.Save();
        //        EnableCommandIDs();
        //        _ctlMQ.uctlForex1.contextForexMenuStrip.Enabled = true;
        //        _ctlMW.uctlMarketWatch1.ui_uctlGridMarketWatch.ContextMenuStrip.Enabled = true;
        //        if (_ctlMW.CurrentPortfolio != string.Empty)
        //        {
        //            List<string> lstSymbolKeys = GetKeysOfPortfolio(_ctlMW.CurrentPortfolio);
        //            List<Symbol> lstSym = new List<Symbol>();
        //            foreach (var item in lstSymbolKeys)
        //            {
        //                Symbol sym = Symbol.GetSymbol(item);
        //                lstSym.Add(sym);
        //            }
        //            clsTWSDataManagerJSON.INSTANCE.SubscribeForQuoteSnapShot(true, eMarketRequest.MARKET_QUOTE_SNAP, lstSym);//Namo
        //                                                                                                                     //  clsTWSDataManagerJSON.INSTANCE.SubscribeForQuotes(true, eMarketRequest.MARKET_QUOTE_REQUEST, lstSym);
        //        }
        //        foreach (NUIDocument doc in m_DockManager.DocumentManager.Documents)
        //        {
        //            if (doc.Client is ctlNewChart)
        //            {
        //                ((ctlNewChart)doc.Client).InitChartData(doc.Key);
        //            }
        //        }
        //        //    IsAnnonymous = false;
        //        //}
        //        if (Portfolio == null)
        //            Portfolio = GetDefaultPortfolio();
        //        GetProfiles();
        //        //_ctlMQ.uctlForex1.Account =                
        //        ClsLocalizationHandler.INSTANCE.Init();
        //        SetFilterBarValues();
        //        SetContextMenuItemHotKeys();
        //        backgroundWorker2.DoWork -= new DoWorkEventHandler(backgroundWorker2_DoWork);
        //        backgroundWorker2.DoWork += new DoWorkEventHandler(backgroundWorker2_DoWork);
        //        backgroundWorker2.RunWorkerAsync();
        //        //clsTWSOrderManagerJSON.INSTANCE.DoHandleExecutionReport -= INSTANCE_DoHandleExecutionReport;
        //        //clsTWSOrderManagerJSON.INSTANCE.OnBusinessLevelReject -= INSTANCE_OnBusinessLevelReject;
        //        //clsTWSOrderManagerJSON.INSTANCE.DoHandleExecutionReport += INSTANCE_DoHandleExecutionReport;
        //        //clsTWSOrderManagerJSON.INSTANCE.OnBusinessLevelReject += INSTANCE_OnBusinessLevelReject;

        //        tlstrplblStatusMsg.Text = "logged in.";
        //    }
        //    else
        //    {
        //        LoggedInSuccess = false;
        //        //FileHandling.WriteDevelopmentLog("LogOnResponce : Login Failed .");
        //        ui_ncmdFileLogin.Properties.ID = (int)CommandIDS.LOGIN;
        //        SetHotkeyHashTable(CommandIDS.LOGIN, ui_ncmdFileLogin);
        //        ui_ncmdFileLogin.Enabled = true;
        //        tlstrplblStatusMsg.Text = "Invalid login.";
        //    }
        //    objForm.KeyDown -= objForm_KeyDown;
        //    _objuctlLogin.OnOkClick -= objuctlLogin_OnOkClick;
        //    _objuctlLogin.OnCancelClick -= objuctlLogin_OnCancelClick;

        //    //FileHandling.WriteDevelopmentLog("LogOnResponce : Exit from LogonResponce Method");
        //}

        private void INSTANCE_OnLogonResponce(string reason, string brokerName, bool isRelogin)
        {
            //FileHandling.WriteDevelopmentLog("LogOnResponce : Enter into LogonResponce Method");
            if (reason == "VALID")
            {
                UserName = _objuctlLogin.UserCode;
                this.m_DockManager.RootContainer.Enabled = true;
                //FileHandling.WriteDevelopmentLog("LogOnResponce : Login SuccessFull.");
                LoggedInSuccess = true;
                clsTWSOrderManagerJSON.INSTANCE.Refresh();
                clsTWSDataManagerJSON.INSTANCE.Refresh();
                Portfolio = GetPortfolios(_objuctlLogin.UserCode);
                _ctlMW.ObjPortfolio = Portfolio;
                if (_objuctlLogin.SavePassword)
                {
                    Properties.Settings.Default.SavePassword = _objuctlLogin.SavePassword;
                    Properties.Settings.Default.LoginPassword = _objuctlLogin.Password;
                    Properties.Settings.Default.LoginName = _objuctlLogin.UserCode;
                }

                Properties.Settings.Default.Save();
                EnableCommandIDs();
                _ctlMQ.uctlForex1.contextForexMenuStrip.Enabled = true;
                _ctlMW.uctlMarketWatch1.ui_uctlGridMarketWatch.ContextMenuStrip.Enabled = true;
                if (_ctlMW.CurrentPortfolio != string.Empty)
                {
                    List<string> lstSymbolKeys = GetKeysOfPortfolio(_ctlMW.CurrentPortfolio);
                    List<Symbol> lstSym = new List<Symbol>();
                    foreach (var item in lstSymbolKeys)
                    {
                        Symbol sym = Symbol.GetSymbol(item);
                        lstSym.Add(sym);
                    }
                    clsTWSDataManagerJSON.INSTANCE.SubscribeForQuotes(SubscribeRequestType.SUBSCRIBE, lstSym);
                    //clsTWSDataManagerJSON.INSTANCE.SubscribeForQuoteSnapShot(true, eMarketRequest.MARKET_QUOTE_SNAP, lstSym);//Namo
                    //  clsTWSDataManagerJSON.INSTANCE.SubscribeForQuotes(true, eMarketRequest.MARKET_QUOTE_REQUEST, lstSym);
                }
                foreach (NUIDocument doc in m_DockManager.DocumentManager.Documents)
                {
                    if (doc.Client is ctlNewChart)
                    {
                        ((ctlNewChart)doc.Client).InitChartData(doc.Key);
                    }
                }
                if (Portfolio == null)
                    Portfolio = GetDefaultPortfolio();
                GetProfiles();
                ClsLocalizationHandler.INSTANCE.Init();
                SetFilterBarValues();
                SetContextMenuItemHotKeys();
                backgroundWorker2.DoWork -= new DoWorkEventHandler(backgroundWorker2_DoWork);
                backgroundWorker2.DoWork += new DoWorkEventHandler(backgroundWorker2_DoWork);
                backgroundWorker2.RunWorkerAsync();

                tlstrplblStatusMsg.Text = "logged in.";
            }
            else
            {
                LoggedInSuccess = false;
                //FileHandling.WriteDevelopmentLog("LogOnResponce : Login Failed .");
                ui_ncmdFileLogin.Properties.ID = (int)CommandIDS.LOGIN;
                SetHotkeyHashTable(CommandIDS.LOGIN, ui_ncmdFileLogin);
                ui_ncmdFileLogin.Enabled = true;
                tlstrplblStatusMsg.Text = "Invalid login.";
            }
            objForm.KeyDown -= objForm_KeyDown;
            _objuctlLogin.OnOkClick -= objuctlLogin_OnOkClick;
            _objuctlLogin.OnCancelClick -= objuctlLogin_OnCancelClick;

            //FileHandling.WriteDevelopmentLog("LogOnResponce : Exit from LogonResponce Method");
        }



        void INSTANCE_SendMessage(string obj)
        {
            tlstrplblStatusMsg.Text = obj;
        }

        //private void INSTANCE_OnBusinessLevelReject(DataRow obj, BusinessReject objBusinessReject)
        //{
        //    Action A = () =>
        //    {
        //        if (obj != null)
        //        {
        //            string orderStatus = obj["OrderStatus"].ToString(); //PALSA.Cls.ClsGlobal.DDReverseOrderStatus[Convert.ToSByte(obj["OrderStatus"].ToString())];
        //            string side = obj["Side"].ToString();// PALSA.Cls.ClsGlobal.DDReverseSide[Convert.ToSByte(obj["Side"].ToString())];
        //            string orderid = obj["OrderId"].ToString();
        //            string contract = obj["Contract"].ToString();
        //            string ordQty = obj["orderQty"].ToString();
        //            string price = obj["price"].ToString();
        //            string Reason = obj["Text"].ToString();
        //            string date = obj["TransactTime"].ToString();
        //            tlstrplblStatusMsg.Text = "Your " + side + " Order with OrderID > " + orderid + " for Symbol > " + contract + " Qty > " + ordQty + " Price > " + price + " is " + orderStatus + " due to reason > " + Reason + " on Date > " + date + " .";
        //        }
        //        else
        //        {
        //            DateTime date = DateTime.UtcNow;
        //            string str = string.Empty;
        //            if (Properties.Settings.Default.TimeFormat.Contains("24"))
        //            {
        //                str = string.Format("{0:HH:mm:ss tt dd/MM/yyyy}", date);
        //            }
        //            else
        //            {
        //                str = string.Format("{0:hh:mm:ss tt dd/MM/yyyy}", date);
        //            }

        //            string reason = Enum.GetName(typeof(BusinessRejectReason), objBusinessReject.BusinessRejectReason);
        //            string text = objBusinessReject.Text;

        //            tlstrplblStatusMsg.Text = "Your New Order with OrderID > " + objBusinessReject.BusinessRejectRefID + " is REJECTED due to reason > " + reason + " on Date > " + str + " .";
        //        }
        //        tlstrplblStatusMsg.BackColor = Color.Yellow;

        //    };
        //    if (InvokeRequired)
        //    {
        //        BeginInvoke(A);
        //    }
        //    else
        //    {
        //        A();
        //    }
        //}
        //private void INSTANCE_DoHandleExecutionReport(ClientDLL_Model.Cls.Order.ExecutionReport executionReport)
        //{
        //    Action A = () =>
        //    {
        //        string orderStatus = PALSA.Cls.ClsGlobal.DDReverseOrderStatus[Convert.ToSByte(executionReport.OrderStatus)];
        //        string side = PALSA.Cls.ClsGlobal.DDReverseSide[Convert.ToSByte(executionReport.Side)];
        //        string orderType = PALSA.Cls.ClsGlobal.DDReverseOrderType[Convert.ToSByte(executionReport.OrderType)];
        //        if (orderStatus.ToUpper() != "FILLED")
        //        {
        //            tlstrplblStatusMsg.ForeColor = Color.Black;
        //            tlstrplblStatusMsg.Text = "Your " + side + " " + orderType + " Order From Account >" + Convert.ToString(executionReport.Account) + " with OrderID > " + executionReport.OrderID + " for Symbol > " + executionReport.Contract + " Qty > " + executionReport.OrdQty + " Price > " + executionReport.Price + " is " + orderStatus + " on Date > " + clsTWSOrderManagerJSON.INSTANCE.GetDateTime(executionReport.TransactTime) + " .";
        //            if (orderType.ToUpper() == "LIMIT")
        //            {
        //                tlstrplblStatusMsg.BackColor = Color.Yellow;
        //            }
        //            else
        //            {
        //                if (side.ToUpper() == "BUY")
        //                {
        //                    tlstrplblStatusMsg.BackColor = Properties.Settings.Default.BuyOrderColor;
        //                }
        //                else if (side.ToUpper() == "SELL")
        //                {
        //                    tlstrplblStatusMsg.BackColor = Properties.Settings.Default.SellOrderColor;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (orderStatus.ToUpper() == "FILLED" && orderType.ToUpper() == "LIMIT")
        //            {
        //                tlstrplblStatusMsg.ForeColor = Color.White;
        //                if (side.ToUpper() == "BUY")
        //                {
        //                    tlstrplblStatusMsg.BackColor = Color.Green;
        //                }
        //                else if (side.ToUpper() == "SELL")
        //                {
        //                    tlstrplblStatusMsg.BackColor = Color.Red;
        //                }
        //            }
        //            tlstrplblStatusMsg.Text = "Your " + side + " " + orderType + " Order From Account >" + Convert.ToString(executionReport.Account) + " with OrderID > " + executionReport.OrderID + " for Symbol > " + executionReport.Contract + " Qty > " + executionReport.OrdQty + " Price > " + executionReport.Price + " is " + orderStatus + " at " + executionReport.LastPx + " with Trade No. > " + executionReport.ExecID + " on Date > " + clsTWSOrderManagerJSON.INSTANCE.GetDateTime(executionReport.TransactTime) + " .";
        //        }
        //    };
        //    if (InvokeRequired)
        //    {
        //        BeginInvoke(A);
        //    }
        //    else
        //    {
        //        A();
        //    }
        //}

        void INSTANCE_OnParticipantResponse(Dictionary<int, DataRow> obj)
        {
            foreach (string PrdctType in ClsTWSContractManager.INSTANCE.ddProductTypesProductContract.Keys)
            {
                try
                {
                    if (ClsTWSContractManager.INSTANCE.ddProductTypesProductContract.ContainsKey(PrdctType))
                    {
                        foreach (string prdct in ClsTWSContractManager.INSTANCE.ddProductTypesProductContract[PrdctType].Keys)
                        {
                            try
                            {
                                foreach (string symbl in ClsTWSContractManager.INSTANCE.ddProductTypesProductContract[PrdctType][prdct])
                                {
                                    if (!PALSA.Cls.ClsGlobal.FutureSymbolList.Contains(symbl))
                                        PALSA.Cls.ClsGlobal.FutureSymbolList.Add(symbl);
                                    InstrumentSpec insSpec = ClsTWSContractManager.INSTANCE.ddContractDetails[symbl];
                                    if (!PALSA.Cls.ClsGlobal.DDContractSize.ContainsKey(symbl))
                                    {
                                        PALSA.Cls.ClsGlobal.DDContractSize.Add(symbl, insSpec.ContractSize);
                                    }
                                }
                            }
                            catch (Exception)
                            {

                            }

                        }
                    }
                }
                catch (Exception)
                {


                }

            }
            if (_ctlMail != null)
            {
                DateTime toDate = DateTime.UtcNow.Subtract(TimeSpan.FromDays(7));
                DateTime fromDate = DateTime.UtcNow;
                Ticker displayTicker = new Ticker();
                int acountid = obj.Keys.ToArray()[0];
                DataRow dr = obj[acountid];
                int groupid = Convert.ToInt32(dr["Group"].ToString());
                string tickerData = string.Empty;
                try
                {
                    string[] t = displayTicker.GetTickerText(groupid);
                    if (t.Contains(groupid.ToString()))
                    {
                        tickerData = displayTicker.GetTickerText(groupid)[0].ToString();
                    }

                }
                catch
                { }
                if (tickerData != string.Empty)
                {
                    this.marqueeLabel1.DisplayText = tickerData;
                }
                List<MailData> lstMail = new List<MailData>();
                try
                {
                    lstMail = clsTWSOrderManagerJSON.INSTANCE.GetMailBoxInfo(_objuctlLogin.UserCode, _objuctlLogin.Password, 2, toDate, fromDate);
                    _ctlMail.PopulateMail(lstMail);
                }
                catch { }
            }
        }




        private void DisableAll()
        {
            ui_nmnuBar.Enabled = false;
            ui_ncmdFileLogin.Enabled = true;
            ui_ncmdFileLogOff.Enabled = true;
            ui_ncmdFileExit.Enabled = true;
        }

        private void EnableAll()
        {
            ui_nmnuBar.Enabled = true;
            //ui_ncmdFileLogin.Enabled = true;
            //ui_ncmdFileLogOff.Enabled = true;
            //ui_ncmdFileExit.Enabled = true;
        }

        public void TimeSpanHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into TimeSpanHandler Method");

            //while (true)
            //{
            //    //this.Text = "Everest Commodity Exchange - Powered by IntelliTrade" + timeSpace + DateTime.UtcNow.ToString("hh:mm:ss tt");
            //    //ui_lblNepalTime.Text = "Nepal Time :" + DateTime.UtcNow.AddHours(5).AddMinutes(45).ToString("hh:mm:ss tt");
            //    ui_lblNepalTime.Text = "Time :" + DateTime.UtcNow.ToString("hh:mm:ss tt");
            //    Thread.Sleep(1000);
            //}

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from TimeSpanHandler Method");
        }

        private void INSTANCE_OnNews(List<News> obj)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into INSTANCE_OnNews Method");

            if (Properties.Settings.Default.MessageBarMessageType.Contains("News") && clsTWSDataManagerJSON.INSTANCE.IsDataMgrConnected)
            {
                lblMessages.Text = "|";
                int count = 0;
                foreach (News news in obj)
                {
                    lblMessages.Text = count < 25
                                           ? "News : " + news._TimeStamp + " ->" + news._NewsTopic + " ->" +
                                             news._BodyText + " ->" + news._URL + Environment.NewLine + lblMessages.Text
                                           : lblMessages.Text.Remove(lblMessages.Text.LastIndexOf(Environment.NewLine),
                                                                     lblMessages.Text.LastIndexOf("|"));
                    count += 1;
                }
            }
            else if (Properties.Settings.Default.MessageBarMessageType.Contains("News") && !clsTWSDataManagerJSON.INSTANCE.IsDataMgrConnected)
            {
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from INSTANCE_OnNews Method");
        }


        private void SetTickerComponents()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into SetTickerComponents Method");

            //_objTickerTape.OnPortFolioApplyClick += objTickerTape_OnPortFolioApplyClick;
            _objTickerTape.SetTickerSpeed(Properties.Settings.Default.TickerSpeed);
            //_objTickerTape.PortfolioList = Portfolio;
            _objTickerTape.BackColor = Color.Transparent;

            NControlHostCommand positionTickerCommand = new NControlHostCommand();
            positionTickerCommand.SetControl(_objTickerTape);

            positionTickerCommand.PrefferedHeight = 25;
            positionTickerCommand.PrefferedWidth = ClientSize.Width - 15;
            ui_ndtTicker.Commands.Add(positionTickerCommand);
            ui_ndtTicker.CommandSize = new Size(ClientSize.Width, 25);
            ui_ndtTicker.MaximumSize = new Size(ClientSize.Width, 25);


            //_objTickerTape.Dock = DockStyle.Fill;
            //_objTickerTape.Height = ui_ndtTicker.Height;
            //_objTickerTape.Width = ClientSize.Width;
            //_objTickerTape.SetStartX(ClientSize.Width);

            //Commented by vijay on 18/04/2012

            //if (Properties.Settings.Default.TickerPortfolio == null || Properties.Settings.Default.TickerPortfolio == "---SELECT---")
            //{
            //    AddDataToTicker(Properties.Settings.Default.LastTickerPortfolio);
            //}
            //else
            //{
            //    AddDataToTicker(Properties.Settings.Default.TickerPortfolio);
            //}
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from SetTickerComponents Method");
        }

        private void INSTANCE_onPriceUpdate(Dictionary<string, Quote> obj)
        {
            ////FileHandling.WriteDevelopmentLog("Main Form : Enter into INSTANCE_onPriceUpdate Method");

            //ImageType imageType;
            //ClsTickerInfo objclsTickerInfo;
            //foreach (var item in obj)
            //{
            //    foreach (QuoteItem item2 in item.Value._lstItem)
            //    {
            //        if (item2._quoteType == QuoteStreamType.LAST)
            //        {
            //            objclsTickerInfo = new ClsTickerInfo();
            //            objclsTickerInfo.ID = item.Key;
            //            objclsTickerInfo.LastTradedPrice = item2._Price;
            //            objclsTickerInfo.LastTradedQuantity = item2._Size.ToString();
            //            switch (item2._status)
            //            {
            //                case QuoteItemStatus.DOWN:
            //                    imageType = ImageType.DOWN_ARROW;
            //                    break;
            //                case QuoteItemStatus.UP:
            //                    imageType = ImageType.UP_ARROW;
            //                    break;
            //                default:
            //                    imageType = ImageType.NO_CHANGE;
            //                    break;
            //            }
            //            _objTickerTape.UpdateControl(objclsTickerInfo, imageType);
            //        }
            //    }
            //}

            ////FileHandling.WriteDevelopmentLog("Main Form : Exit from INSTANCE_onPriceUpdate Method");
        }

        private void objTickerTape_OnPortFolioApplyClick(string portfolioName)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into objTickerTape_OnPortFolioApplyClick Method");

            AddDataToTicker(portfolioName);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from objTickerTape_OnPortFolioApplyClick Method");
        }

        private void AddDataToTicker(string portFolioName)
        {
            //var lstTickerInfo =new List<ClsTickerInfo>();
            //var objclsTickerInfo =new ClsTickerInfo();
            //objclsTickerInfo.Symbol = portFolioName;
            //lstTickerInfo.Add(objclsTickerInfo);
            //_objTickerTape.AddSensArticles(lstTickerInfo);
            //_objTickerTape.Refresh();
            ////FileHandling.WriteDevelopmentLog("Main Form : Enter into AddDataToTicker Method");

            //_objTickerTape._currentTickerPortfolio = portFolioName;
            //var lstSymbols = new List<Symbol>(); //new line of code
            //ThreadPool.QueueUserWorkItem(delegate
            //                                 {
            //                                     var lstTickerInfo =
            //                                         new List<ClsTickerInfo>();

            //                                     if (Portfolio != null && portFolioName != null &&
            //                                         ((Dictionary<string, ClsPortfolio>)Portfolio).
            //                                             ContainsKey(portFolioName))
            //                                     {
            //                                         foreach (Symbol objSymbol in ((Dictionary<string, ClsPortfolio>)
            //                                                                       Portfolio)[portFolioName].Products.
            //                                             Select(item => Symbol.GetSymbol(item.Key)))
            //                                         {
            //                                             lstSymbols.Add(objSymbol); //new line of code
            //                                             InstrumentSpec objInstrumentSpec =
            //                                                 ClsTWSContractManager.INSTANCE.GetContractSpec(
            //                                                     objSymbol._ContractName, objSymbol._ProductType,
            //                                                     objSymbol._ProductName);

            //                                             if (objInstrumentSpec != null)
            //                                             {
            //                                                 var objclsTickerInfo =
            //                                                     new ClsTickerInfo();
            //                                                 string tickerDisplayType =
            //                                                     Properties.Settings.Default.TickerDisplay;
            //                                                 objclsTickerInfo.ID = objSymbol.KEY;
            //                                                 if (tickerDisplayType != null &&
            //                                                     tickerDisplayType == "Symbol")
            //                                                 {
            //                                                     objclsTickerInfo.Symbol =
            //                                                         objInstrumentSpec.Product;
            //                                                 }
            //                                                 else if (tickerDisplayType != null &&
            //                                                          tickerDisplayType == "Description")
            //                                                 {
            //                                                     objclsTickerInfo.Symbol = objInstrumentSpec.Information;
            //                                                 }
            //                                                 else
            //                                                 {
            //                                                     objclsTickerInfo.Symbol = objInstrumentSpec.Product;
            //                                                 }
            //                                                 objclsTickerInfo.ExpiryDate = clsTWSOrderManagerJSON.INSTANCE.GetDateTime(objInstrumentSpec.ExpiryDate);
            //                                                 objclsTickerInfo.LastTradedQuantity =
            //                                                     objInstrumentSpec.PriceQuantity.ToString();
            //                                                 objclsTickerInfo.LastTradedPrice =
            //                                                     objInstrumentSpec.PriceTick;
            //                                                 lstTickerInfo.Add(objclsTickerInfo);
            //                                             }
            //                                         }

            //                                         clsTWSDataManagerJSON.INSTANCE.SubscribeForQuotes(true,
            //                                                                                       eMarketRequest.
            //                                                                                           MARKET_QUOTE_REQUEST,
            //                                                                                       lstSymbols);
            //                                     }
            //                                     _objTickerTape.AddSensArticles(lstTickerInfo);
            //                                     _objTickerTape.Refresh();

            //                                     #region "Dummy data ticker code"

            //                                     //clsTickerInfo objSens = new clsTickerInfo();
            //                                     //objSens.Symbol = "USD";
            //                                     //objSens.ExpiryDate = DateTime.UtcNow; 
            //                                     //objSens.LastTradedQuantity = "1";
            //                                     //objSens.LastTradedPrice = 56; 
            //                                     //objSens.ID = "123";
            //                                     //sensList.Add(objSens);

            //                                     //clsTickerInfo objSens1 = new clsTickerInfo();
            //                                     //objSens1.Symbol = "JPY";
            //                                     //objSens1.ExpiryDate = DateTime.UtcNow; 
            //                                     //objSens1.LastTradedQuantity = "1";
            //                                     //objSens1.LastTradedPrice = 56.56;
            //                                     //objSens1.ID = "13";
            //                                     //sensList.Add(objSens1);

            //                                     //clsTickerInfo objSens2 = new clsTickerInfo();
            //                                     //objSens2.Symbol = "NKP";
            //                                     //objSens2.ExpiryDate = DateTime.UtcNow; 
            //                                     //objSens2.LastTradedQuantity = "1";
            //                                     //objSens2.LastTradedPrice = 56.56;
            //                                     //objSens2.ID = "1445";
            //                                     //sensList.Add(objSens2);

            //                                     //clsTickerInfo objSens3 = new clsTickerInfo();
            //                                     //objSens3.Symbol = "kpj";
            //                                     //objSens3.ExpiryDate = DateTime.UtcNow; 
            //                                     //objSens3.LastTradedQuantity = "1";
            //                                     //objSens3.LastTradedPrice = 56.56;
            //                                     //objSens3.ID = "145555";
            //                                     //sensList.Add(objSens3);

            //                                     //clsTickerInfo objSens4 = new clsTickerInfo();
            //                                     //objSens4.Symbol = "DDD";
            //                                     //objSens4.ExpiryDate = DateTime.UtcNow;
            //                                     //objSens4.LastTradedQuantity = "1";
            //                                     //objSens4.LastTradedPrice = 56.56;
            //                                     //objSens4.ID = "145666";
            //                                     //sensList.Add(objSens4);

            //                                     //clsTickerInfo objSens5 = new clsTickerInfo();
            //                                     //objSens5.Symbol = "KKK";
            //                                     //objSens5.ExpiryDate = DateTime.UtcNow;
            //                                     //objSens5.LastTradedQuantity = "1";
            //                                     //objSens5.LastTradedPrice = 56.56;
            //                                     //objSens5.ID = "155465";
            //                                     //sensList.Add(objSens5);

            //                                     //clsTickerInfo objSens6 = new clsTickerInfo();
            //                                     //objSens6.Symbol = "lll";
            //                                     //objSens6.ExpiryDate = DateTime.UtcNow;
            //                                     //objSens6.LastTradedQuantity = "1";
            //                                     //objSens6.LastTradedPrice = 56.56;
            //                                     //objSens6.ID = "156656";
            //                                     //sensList.Add(objSens6);

            //                                     //clsTickerInfo objSens7 = new clsTickerInfo();
            //                                     //objSens7.Symbol = "l";
            //                                     //objSens7.ExpiryDate = DateTime.UtcNow;
            //                                     //objSens7.LastTradedQuantity = "1";
            //                                     //objSens7.LastTradedPrice = 56.56;
            //                                     //objSens7.ID = "1454";
            //                                     //sensList.Add(objSens7);

            //                                     //objTickerTape.AddSensArticles(sensList);
            //                                     //objTickerTape.Refresh();

            //                                     #endregion "Dummy data ticker code"
            //                                 });

            ////FileHandling.WriteDevelopmentLog("Main Form : Exit from AddDataToTicker Method");
        }

        private void SensTimer_Tick(object sender, EventArgs e)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into _SensTimer_Tick Method");
            //Namo 21 March
            //ThreadPool.QueueUserWorkItem(delegate
            //                                 {
            //                                     var lstTickerInfo =
            //                                         new List<ClsTickerInfo>();

            //                                     ClsPortfolio port =
            //                                         ((Dictionary<string, ClsPortfolio>)Portfolio)[
            //                                             Properties.Settings.Default.TickerPortfolio];

            //                                     if (Properties.Settings.Default.TickerPortfolio !=
            //                                         null)
            //                                     {
            //                                         foreach (
            //                                             var item in
            //                                                 ((Dictionary<string, ClsPortfolio>)
            //                                                  Portfolio)[
            //                                                      Properties.Settings.Default.
            //                                                          TickerPortfolio].Products)
            //                                         {
            //                                             string[] strA = item.Key.Split('_');
            //                                             string instrumentName = strA[0];
            //                                             string symbol = strA[1];
            //                                             string expiryDate = strA[2];
            //                                             IEnumerable<string> res =
            //                                                 ClsTWSContractManager.INSTANCE.GetProductsInfo(instrumentName, symbol, eSEARCH_CRITERIA.IS);
            //                                             foreach (string product in res)
            //                                             {
            //                                                 List<string> contracts =
            //                                                     ClsTWSContractManager.INSTANCE.
            //                                                         GetAllContracts(instrumentName,
            //                                                                         product);

            //                                                 foreach (string item2 in contracts)
            //                                                 {
            //                                                     InstrumentSpec objInstrumentSpec =
            //                                                         ClsTWSContractManager.INSTANCE.
            //                                                             GetInstrumentSpecification(
            //                                                                 instrumentName, item2,
            //                                                                 product);
            //                                                     if (expiryDate != null &&
            //                                                         expiryDate ==
            //                                                         clsTWSOrderManagerJSON.INSTANCE.GetDateTime(
            //                                                             objInstrumentSpec.ExpiryDate)
            //                                                             )
            //                                                     {
            //                                                         var objclsTickerInfo =
            //                                                             new ClsTickerInfo();

            //                                                         objclsTickerInfo.ID = item.Key;
            //                                                         //objclsTickerInfo.Symbol = objInstrumentSpec.Symbol;
            //                                                         objclsTickerInfo.ExpiryDate =
            //                                                             clsTWSOrderManagerJSON.INSTANCE.GetDateTime(objInstrumentSpec.ExpiryDate);
            //                                                         objclsTickerInfo.
            //                                                             LastTradedQuantity =
            //                                                             objInstrumentSpec.
            //                                                                 PriceQuantity.ToString();
            //                                                         objclsTickerInfo.LastTradedPrice =
            //                                                             objInstrumentSpec.PriceTick;

            //                                                         lstTickerInfo.Add(objclsTickerInfo);
            //                                                     }
            //                                                 }
            //                                             }
            //                                         }
            //                                     }
            //                                     _objTickerTape.AddSensArticles(lstTickerInfo);
            //                                     _objTickerTape.Refresh();

            //                                     #region "Dummy data ticker code"

            //                                     //clsTickerInfo objSens = new clsTickerInfo();
            //                                     //objSens.Symbol = "USD";
            //                                     //objSens.ExpiryDate = DateTime.UtcNow; 
            //                                     //objSens.LastTradedQuantity = "1";
            //                                     //objSens.LastTradedPrice = 56; 
            //                                     //objSens.ID = "123";
            //                                     //sensList.Add(objSens);

            //                                     //clsTickerInfo objSens1 = new clsTickerInfo();
            //                                     //objSens1.Symbol = "JPY";
            //                                     //objSens1.ExpiryDate = DateTime.UtcNow; 
            //                                     //objSens1.LastTradedQuantity = "1";
            //                                     //objSens1.LastTradedPrice = 56.56;
            //                                     //objSens1.ID = "13";
            //                                     //sensList.Add(objSens1);

            //                                     //clsTickerInfo objSens2 = new clsTickerInfo();
            //                                     //objSens2.Symbol = "NKP";
            //                                     //objSens2.ExpiryDate = DateTime.UtcNow; 
            //                                     //objSens2.LastTradedQuantity = "1";
            //                                     //objSens2.LastTradedPrice = 56.56;
            //                                     //objSens2.ID = "1445";
            //                                     //sensList.Add(objSens2);

            //                                     //clsTickerInfo objSens3 = new clsTickerInfo();
            //                                     //objSens3.Symbol = "kpj";
            //                                     //objSens3.ExpiryDate = DateTime.UtcNow; 
            //                                     //objSens3.LastTradedQuantity = "1";
            //                                     //objSens3.LastTradedPrice = 56.56;
            //                                     //objSens3.ID = "145555";
            //                                     //sensList.Add(objSens3);

            //                                     //clsTickerInfo objSens4 = new clsTickerInfo();
            //                                     //objSens4.Symbol = "DDD";
            //                                     //objSens4.ExpiryDate = DateTime.UtcNow;
            //                                     //objSens4.LastTradedQuantity = "1";
            //                                     //objSens4.LastTradedPrice = 56.56;
            //                                     //objSens4.ID = "145666";
            //                                     //sensList.Add(objSens4);

            //                                     //clsTickerInfo objSens5 = new clsTickerInfo();
            //                                     //objSens5.Symbol = "KKK";
            //                                     //objSens5.ExpiryDate = DateTime.UtcNow;
            //                                     //objSens5.LastTradedQuantity = "1";
            //                                     //objSens5.LastTradedPrice = 56.56;
            //                                     //objSens5.ID = "155465";
            //                                     //sensList.Add(objSens5);

            //                                     //clsTickerInfo objSens6 = new clsTickerInfo();
            //                                     //objSens6.Symbol = "lll";
            //                                     //objSens6.ExpiryDate = DateTime.UtcNow;
            //                                     //objSens6.LastTradedQuantity = "1";
            //                                     //objSens6.LastTradedPrice = 56.56;
            //                                     //objSens6.ID = "156656";
            //                                     //sensList.Add(objSens6);

            //                                     //clsTickerInfo objSens7 = new clsTickerInfo();
            //                                     //objSens7.Symbol = "l";
            //                                     //objSens7.ExpiryDate = DateTime.UtcNow;
            //                                     //objSens7.LastTradedQuantity = "1";
            //                                     //objSens7.LastTradedPrice = 56.56;
            //                                     //objSens7.ID = "1454";
            //                                     //sensList.Add(objSens7);

            //                                     //objTickerTape.AddSensArticles(sensList);
            //                                     //objTickerTape.Refresh();

            //                                     #endregion "Dummy data ticker code"
            //                                 });

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from _SensTimer_Tick Method");
        }

        private void ui_nmnuWindows_Select(object sender, CommandEventArgs e)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into ui_nmnuWindows_Select Method");

            switch (MdiChildren.Count())
            {
                case 0:
                    foreach (NCommand item in ui_nmnuWindows.Commands)
                    {
                        item.Enabled = false;
                    }
                    break;
                default:
                    foreach (NCommand item in ui_nmnuWindows.Commands)
                    {
                        item.Enabled = true;
                    }
                    break;
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from ui_nmnuWindows_Select Method");
        }

        private void SetFilterBarValues()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into SetFilterBarValues Method");

            //ui_ncmbInstrumentType.Items.AddRange(ClsTWSContractManager.INSTANCE.GetAllProductTypes().ToArray());
            //string[] x = ClsTWSContractManager.INSTANCE.GetAllProductTypes().ToArray();
            //ui_ncmbInstrumentType.Click += ui_ncmbInstrumentType_Click;
            //ui_ncmbSymbol.Click += ui_ncmbSymbol_Click;

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from SetFilterBarValues Method");
        }

        private void ui_ncmbSymbol_Click(object sender, CommandEventArgs e)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into ui_ncmbSymbol_Click Method");

            //ui_ncmbExpiryDate.Items.Clear();

            //List<string> lstContacts = ClsTWSContractManager.INSTANCE.GetAllContracts(
            //    ui_ncmbInstrumentType.ControlText, ui_ncmbSymbol.ControlText);

            //ui_ncmbExpiryDate.Items.AddRange(lstContacts.ToArray());


            //FileHandling.WriteDevelopmentLog("Main Form : Exit from ui_ncmbSymbol_Click Method");
        }

        private void ui_ncmbInstrumentType_Click(object sender, CommandEventArgs e)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into ui_ncmbInstrumentType_Click Method");

            //ui_ncmbSymbol.Items.Clear();

            //ui_ncmbSymbol.Items.AddRange(
            //    ClsTWSContractManager.INSTANCE.GetAllProducts(ui_ncmbInstrumentType.ControlText).ToArray());

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from ui_ncmbInstrumentType_Click Method");
        }

        public void LoadDefaultWorkSpace()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into LoadDefaultWorkSpace Method");

            if (Properties.Settings.Default.DefaultWorkSpace != "")
            {
                LoadWorkSpace(Properties.Settings.Default.DefaultWorkSpace);
            }
            else
            {
                //CloseAllMenuHandler();
                string path = _DeafultWorkSpacePath;
                string p = path;
                string e = System.IO.Path.GetExtension(p);
                string Path = path.Replace(e, ".dat");
                LoadWorkSpace(Path);
                Thread.Sleep(10);
                state.Load(path);

            }
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from LoadDefaultWorkSpace Method");
        }

        private void PopulateReverceHotKeySettingsHashTable()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into PopulateReverceHotKeySettingsHashTable Method");

            _revercedHotKeySettingsHashTable = new Hashtable();
            foreach (string s in _hotKeySettingsHashTable.Keys)
            {
                if (_hotKeySettingsHashTable[s].ToString() != "[NONE]" &&
                    !_hotKeySettingsHashTable[s].ToString().Contains("+"))
                    _revercedHotKeySettingsHashTable.Add((_hotKeySettingsHashTable[s]).ToString(), s);
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from PopulateReverceHotKeySettingsHashTable Method");
        }

        private void SetContextMenuItemHotKeys()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into SetContextMenuItemHotKeys Method");

            foreach (DictionaryEntry deEntry in _hotKeySettingsHashTable)
            {
                string Hotkey = deEntry.Key.ToString();
                var commandID =
                    (CommandIDS)Enum.Parse(typeof(CommandIDS), Hotkey);
                switch (commandID)
                {
                    case CommandIDS.CANCEL_ALL_ORDERS:
                        Enum.TryParse(_hotKeySettingsHashTable[deEntry.Key].ToString(),
                                      out _shortcutKeyCancelAllOrders);
                        break;
                    case CommandIDS.MODIFY_TRDES:
                        Enum.TryParse(_hotKeySettingsHashTable[deEntry.Key].ToString(), out _shortcutKeyModifyTrades);
                        break;
                    case CommandIDS.FILTER:
                        Enum.TryParse(_hotKeySettingsHashTable[deEntry.Key].ToString(), out _shortcutKeyFilter);
                        break;
                    case CommandIDS.PLACE_ORDER:
                        Enum.TryParse(_hotKeySettingsHashTable[deEntry.Key].ToString(),
                                      out _shortcutKeyOrderEntry);
                        break;
                    //case CommandIDS.PLACE_SELL_ORDER:
                    //    Enum.TryParse(_hotKeySettingsHashTable[deEntry.Key].ToString(),
                    //                  out _shortcutKeySellOrderEntry);
                    //    break;
                    case CommandIDS.MARKET_PICTURE:
                        Enum.TryParse(_hotKeySettingsHashTable[deEntry.Key].ToString(),
                                      out _shortcutKeyMarketPicture);
                        break;
                }
            }
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from SetContextMenuItemHotKeys Method");
        }

        public void AddIndicatorList()
        {
            try
            {
                //IndicatorSelection selection = (new frmIndicator()).GetIndicatorSelection();
                //Application.DoEvents();
                //if (selection.node == -1)
                //    return;

                //if (objMdiGTS.m_ActiveChart == null) return;

                //objMdiGTS.m_ActiveChart.AddIndicator(selection, -1);
            }
            catch
            {
                //ServerLog.Write("frmMainGTS::nAnalysis_indicatorList_Click" + ex.ToString() + ex.StackTrace, true);
            }
        }

        private void CallShortcut(CommandIDS ID)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into CallShortcut Method");

            switch (ID)
            {
                case CommandIDS.LOGIN:
                    LoginMenuHandler();
                    break;
                case CommandIDS.LOGOFF:
                    LogoffMenuHandler();
                    break;
                case CommandIDS.LOAD_WORKSPACE:
                    LoadWorkSpaceMenuHandler();
                    break;
                case CommandIDS.SAVE_WORKSPACE:
                    SaveWorkSpaceMenuHandler();
                    break;
                case CommandIDS.EXIT:
                    //this.Close();
                    break;
                case CommandIDS.LANGUAGES:
                    LanguagesMenuHandeler();
                    break;
                case CommandIDS.CHANGE_PASSWORD:
                    ChangePasswordMenuHandeler();
                    break;
                case CommandIDS.TICKER:
                    TickerMenuHandeler();
                    break;
                //case :
                //    TradeMenuHandler();
                //    break;
                case CommandIDS.NET_POSITION:
                    NetPositionMenuHandler();
                    break;
                case CommandIDS.MESSAGE_LOG:
                    MessageLogMenuHandler();
                    break;
                case CommandIDS.CONTRACT_INFORMATION:
                    ContractInformationMenuHandler();
                    break;
                case CommandIDS.TOOL_BAR:
                    ToolBarMenuHandler();
                    break;
                case CommandIDS.FILTER_BAR:
                    FilterBarMenuHandler();
                    break;
                case CommandIDS.MESSAGE_BAR:
                    MessageBarMenuHandler();
                    break;
                case CommandIDS.STATUS_BAR:
                    StatusBarMenuHandler();
                    break;
                case CommandIDS.TOP_STATUS_BAR:
                    TopStatusBarMenuHandler();
                    break;
                case CommandIDS.MIDDLE_STATUS_BAR:
                    MiddleStatusBarMenuHandler();
                    break;
                case CommandIDS.BOTTOM_STATUS_BAR:
                    BottomStatusBarMenuHandler();
                    break;
                case CommandIDS.ADMIN_MESSAGE_BAR:
                    AdminMessageBarMenuHandler();
                    break;
                case CommandIDS.INDICES_VIEW:
                    IndicesViewMenuHandler();
                    break;
                case CommandIDS.FULL_SCREEN:
                    FullScreenMenuHandler();
                    break;
                case CommandIDS.MARKET_WATCH:
                    MarketWatchMenuHandler();
                    break;
                case CommandIDS.MARKET_PICTURE:
                    MarketPictureMenuHandler();
                    break;
                case CommandIDS.SNAP_QUOTE:
                    QuoteSnapMenuHandler();
                    break;
                case CommandIDS.MARKET_STATUS:
                    MarketStatusMenuHandler();
                    break;
                case CommandIDS.TOP_GAINERS_LOSERS:
                    TopGainersLosersMenuHandler();
                    break;
                //case CommandIDS.PLACE_BUY_ORDER:
                //    PlaceBuyOrderMenuHandler();
                //    break;
                //case CommandIDS.PLACE_SELL_ORDER:
                //    PlaceSellOrderMenuHandler();
                //    break;
                case CommandIDS.PLACE_ORDER:
                    //PlaceOrderMenuHandler();
                    break;
                case CommandIDS.ORDER_BOOK:
                    OrderBookMenuHandler();
                    break;
                case CommandIDS.TRADES:
                case CommandIDS.TRADE:
                    TradeMenuHandler();
                    break;
                case CommandIDS.CUSTOMIZE:
                    CustomizeMenuHandler();
                    break;
                case CommandIDS.LOCK_WORKSTATION:
                    LockWorkStationMenuHandler();
                    break;
                case CommandIDS.PORTFOLIO:
                    PortfolioMenuHandler();
                    break;
                case CommandIDS.PREFERENCES:
                    PreferencesMenuHandler();
                    break;
                case CommandIDS.NEW_WINDOW:
                    NewWindowMenuHandler();
                    break;
                case CommandIDS.CLOSE:
                    CloseMenuHandler();
                    break;
                case CommandIDS.CLOSE_ALL:
                    CloseAllMenuHandler();
                    break;
                case CommandIDS.CASCADE:
                    CascadeMenuHandler();
                    break;
                case CommandIDS.TILE_HORIZONTALLY:
                    TileHorizontallyMenuHandler();
                    break;
                case CommandIDS.TILE_VERTICALLY:
                    TileVerticallyMenuHandler();
                    break;
                case CommandIDS.WINDOW:
                    WindowMenuHandler();
                    break;
                case CommandIDS.HELP:
                    HelpMenuHandler();
                    break;
                case CommandIDS.ONLINE_BACKUP:
                    OnlineBackToolBarHandler();
                    break;
                case CommandIDS.PRINT:
                    PrintToolBarHandler();
                    break;
                case CommandIDS.MODIFY_ORDER:
                    ModifyOrderToolBarHandler();
                    break;
                case CommandIDS.CANCEL_SELECTED_ORDER:
                    CancelOrderToolBarHandler();
                    break;
                case CommandIDS.CANCEL_ALL_ORDERS:
                    CancelAllOrdersToolBarHandler();
                    break;
                case CommandIDS.ENGLISH:
                    EnglishMenuHandler();
                    break;
                case CommandIDS.HINDI:
                    HindiMenuHandler();
                    break;
                case CommandIDS.MAC_OS:
                    MacOSMenuHandler(ui_ncmbThemeMacOS);
                    Properties.Settings.Default.Theme = (int)CommandIDS.MAC_OS;
                    break;
                case CommandIDS.OFFICE_2007_BLACk:
                    OfficeBlackMenuHandler(ui_ncmbThemeOffice2007Black);
                    Properties.Settings.Default.Theme = (int)CommandIDS.OFFICE_2007_BLACk;
                    break;
                case CommandIDS.OFFICE_2007_BLUE:
                    OfficeBlueMenuHandler(ui_ncmbThemeOffice2007Blue);
                    Properties.Settings.Default.Theme = (int)CommandIDS.OFFICE_2007_BLUE;
                    break;
                case CommandIDS.ORANGE:
                    OrangeMenuHandler(ui_ncmbThemeOrange);
                    Properties.Settings.Default.Theme = (int)CommandIDS.ORANGE;
                    break;
                case CommandIDS.VISTA:
                    VistaMenuHandler(ui_ncmbThemeVista);
                    Properties.Settings.Default.Theme = (int)CommandIDS.VISTA;
                    break;
                case CommandIDS.VISTA_ROYAL:
                    VistaRoyalMenuHandler(ui_ncmdThemeVistaRoyal);
                    Properties.Settings.Default.Theme = (int)CommandIDS.VISTA_ROYAL;
                    break;
                case CommandIDS.OFFICE_2007_AQUA:
                    Office2007AquaMenuHandler(ui_ncmdThemeOffice2007Aqua);
                    Properties.Settings.Default.Theme = (int)CommandIDS.OFFICE_2007_AQUA;
                    break;
                case CommandIDS.OPUS_ALPHA:
                    OpusAlphaMenuHandler(ui_ncmdThemeOpusAlpha);
                    Properties.Settings.Default.Theme = (int)CommandIDS.OPUS_ALPHA;
                    break;
                case CommandIDS.VISTA_PLUS:
                    VistaPlusMenuHandler(ui_ncmdThemeVistaPlus);
                    Properties.Settings.Default.Theme = (int)CommandIDS.VISTA_PLUS;
                    break;
                case CommandIDS.VISTA_SLATE:
                    VistaSlateMenuHandler(ui_ncmdThemeVistaSlate);
                    Properties.Settings.Default.Theme = (int)CommandIDS.VISTA_SLATE;
                    break;
                case CommandIDS.INSPIRANT:
                    InspirantMenuHandler(ui_ncmdThemeInspirant);
                    Properties.Settings.Default.Theme = (int)CommandIDS.INSPIRANT;
                    break;
                case CommandIDS.SIMPLE:
                    SimpleMenuHandler(ui_ncmdThemeSimple);
                    Properties.Settings.Default.Theme = (int)CommandIDS.SIMPLE;
                    break;
                case CommandIDS.PARTICIPANT_LIST:
                    ParticipaintListMenuHandler();
                    break;
                case CommandIDS.INDEX_BAR:
                    IndexBarToolBarHandler();
                    break;
                case CommandIDS.NEW_CHART:
                    NewChartMenuHandler();
                    break;
                case CommandIDS.MARKET_QUOTE:
                    MarketQuoteMenuHandler();
                    break;
                case CommandIDS.PENDING_ORDERS:
                    PendingOrdersMenuHandeler();
                    break;
                case CommandIDS.ACCOUNTS_TO_TRADE:
                    AccountsInfoMenuHandler();
                    break;
                case CommandIDS.ROYAL:
                    RoyalThemeMenuHandler(ui_ncmdThemeRoyal);
                    Properties.Settings.Default.Theme = (int)CommandIDS.ROYAL;
                    break;
                case CommandIDS.AQUA:
                    AquaThemeMenuHandler(ui_ncmdThemeAqua);
                    Properties.Settings.Default.Theme = (int)CommandIDS.AQUA;
                    break;
                case CommandIDS.MOONLIGHT:
                    MoonLightThemeMenuHandler(ui_ncmdThemeMoonlight);
                    Properties.Settings.Default.Theme = (int)CommandIDS.MOONLIGHT;
                    break;
                case CommandIDS.WOOD:
                    WoodThemeMenuHandler(ui_ncmdThemeWood);
                    Properties.Settings.Default.Theme = (int)CommandIDS.WOOD;
                    break;
                case CommandIDS.GREEN:
                    GreenThemeMenuHandler(ui_ncmdThemeGreen);
                    Properties.Settings.Default.Theme = (int)CommandIDS.GREEN;
                    break;
                //===================Chart=============
                case CommandIDS.PERIODICITY_1_MINUTE:
                    Periodicity1MinuteMenuHandler();
                    break;
                case CommandIDS.PERIODICITY_5_MINUTE:
                    Periodicity5MinuteMenuHandler();
                    break;
                case CommandIDS.PERIODICITY_15_MINUTE:
                    Periodicity15MinuteMenuHandler();
                    break;
                case CommandIDS.PERIODICITY_30_MINUTE:
                    Periodicity30MinuteMenuHandler();
                    break;
                case CommandIDS.PERIODICITY_1_HOUR:
                    Periodicity1HourMenuHandler();
                    break;
                case CommandIDS.PERIODICITY_4_HOUR:
                    Periodicity4HourMenuHandler();
                    break;
                case CommandIDS.PERIODICITY_DAILY:
                    PeriodicityDailyMenuHandler();
                    break;
                case CommandIDS.PERIODICITY_WEEKLY:
                    PeriodicityWeeklyMenuHandler();
                    break;
                case CommandIDS.PERIODICITY_MONTHLY:
                    PeriodicityMonthlyMenuHandler();
                    break;
                case CommandIDS.CHARTTYPE_BAR_CHART:
                    BarChartTypeMenuHandler();
                    break;
                case CommandIDS.CHARTTYPE_CANDLE_CHART:
                    CandleChartTypeMenuHandler();
                    break;
                case CommandIDS.CHARTTYPE_LINE_CHART:
                    LineChartTypeMenuHandler();
                    break;
                case CommandIDS.PRICETYPE_POINT_AND_FIGURE:
                    PointandFigurePriceTypeMenuHandler();
                    break;
                case CommandIDS.PRICETYPE_RENKO:
                    RenkoPriceTypeMenuHandler();
                    break;
                case CommandIDS.PRICETYPE_KAGI:
                    KagiPriceTypeMenuHandler();
                    break;
                case CommandIDS.PRICETYPE_THREE_LINE_BREAK:
                    ThreeLineBreakPriceTypeMenuHandler();
                    break;
                case CommandIDS.PRICETYPE_EQUI_VOLUME:
                    EquiVolumePriceTypeMenuHandler();
                    break;
                case CommandIDS.PRICETYPE_EQUI_VOLUME_SHADOW:
                    EquiVolumeShadowPriceTypeMenuHandler();
                    break;
                case CommandIDS.PRICETYPE_CANDLE_VOLUME:
                    CandleVolumePriceTypeMenuHandler();
                    break;
                case CommandIDS.PRICETYPE_HEIKIN_ASHI:
                    HeikinAshiPriceTypeMenuHandler();
                    break;
                case CommandIDS.PRICETYPE_STANDARD_CHART:
                    StandardChartPriceTypeMenuHandler();
                    break;
                case CommandIDS.ZOOM_IN:
                    ZoomInMenuHandler();
                    break;
                case CommandIDS.ZOOM_OUT:
                    ZoomOutMenuHandler();
                    break;
                case CommandIDS.TRACK_CURSOR:
                    TrackCursorMenuHandler();
                    break;
                case CommandIDS.VOLUME:
                    VolumeMenuHandler();
                    break;
                case CommandIDS.GRID:
                    GridMenuHandler();
                    break;
                case CommandIDS.RADAR:
                    RadarMenuHandeler();
                    break;
                case CommandIDS.CHART_3D_STYLE:
                    Chart3DStyleMenuHandler();
                    break;
                case CommandIDS.SNAPSHOT_PRINT:
                    SnapshotPrintMenuHandler();
                    break;
                case CommandIDS.SNAPSHOT_SAVE:
                    SnapshotSaveMenuHandler();
                    break;
                case CommandIDS.INDICATOR_LIST:
                    IndicatorListMenuHandler();
                    break;
                //===================Chart=============
                //case CommandIDS.SURVEILLANCE:
                //    SurveillanceMenuHandler();
                //    break;
                case CommandIDS.ABOUTUS:
                    AboutUsMenuHandler();
                    break;
                case CommandIDS.SCANNER:
                    ScannerMenuHandler();
                    break;
                case CommandIDS.CREATE_DEMO_ACCOUNT:
                    CreateDemoAccountHandler();
                    break;

                #region "    Technical Analysis Add Submenu Handlers    "

                case CommandIDS.HORIZONTAL_LINE:
                    HorizontalMenuHandler();
                    break;
                case CommandIDS.VERTICAL_LINE:
                    VerticalMenuHandler();
                    break;
                case CommandIDS.TEXT:
                    TextMenuHandler();
                    break;
                case CommandIDS.TREND_LINE:
                    TrednLineMenuHandler();
                    break;
                case CommandIDS.ELLIPSE:
                    EllipseMenuHandler();
                    break;
                case CommandIDS.SPEED_LINES:
                    SpeedLineMenuHandler();
                    break;
                case CommandIDS.GANN_FAN:
                    GannFanMenuHandler();
                    break;
                case CommandIDS.FIBONACCI_ARC:
                    FibonacciArcMenuHandler();
                    break;
                case CommandIDS.FIBONACCI_RETRACEMENT:
                    FibonacciRetracementMenuHandler();
                    break;
                case CommandIDS.FIBONACCI_FAN:
                    FibonacciFanMenuHandler();
                    break;
                case CommandIDS.FIBONACCI_TIMEZONE:
                    FibonacciTimeZoneMenuHandler();
                    break;
                case CommandIDS.TIRONE_LEVEL:
                    TironeLevelMenuHandler();
                    break;
                case CommandIDS.QUADRENT_LINES:
                    QuadrentLinesMenuHandler();
                    break;
                case CommandIDS.RAFF_REGRESSION:
                    RaffRegressionMenuHandler();
                    break;
                case CommandIDS.ERROR_CHANNEL:
                    ErrorChannelMenuHandler();
                    break;
                case CommandIDS.RECTANGLE:
                    RectangleMenuHandler();
                    break;
                case CommandIDS.FREE_HAND_DRAWING:
                    FreeHandDrawingMenuHandler();
                    break;

                    #endregion "  Technical Analysis Add Submenu Handlers    "
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from CallShortcut Method");
        }


        private void PendingOrdersMenuHandeler()
        {
            if (_ctlPendingOrders == null)
            {
                _ctlPendingOrders = new ctlPendingOrders(Profiles, "");
            }
            nDockPanelHost2.AddChild(this.PendingOrders, nDockPanelHost2.Children.Count);
            AddToPanel(_ctlPendingOrders);
        }


        private void RadarMenuHandeler()
        {
            if (Properties.Settings.Default.RadarInDoc)
            {
                CreateNuiDocument(_ctlRadar, "Radar");
            }
            else
            {
                if (Properties.Settings.Default.RadarZone == 1)
                {
                    nDockPanelHost1.AddChild(this.Radar, Properties.Settings.Default.RadarIndex);
                }
                else
                {
                    nDockPanelHost2.AddChild(this.Radar, Properties.Settings.Default.RadarIndex);
                }
                AddToPanel(_ctlRadar);
            }
        }


        private void CreateDemoAccountHandler()
        {
            backgroundWorker1.DoWork -= new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerAsync();
        }

        private void AboutUsMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into AboutUsMenuHandler Method");

            var objfrmAboutUs = new FrmAboutUs();
            //objfrmAboutUs.MdiParent = this;
            objfrmAboutUs.ShowDialog();

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from AboutUsMenuHandler Method");
        }

        private void SurveillanceMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into SurveillanceMenuHandler Method");
            if (frmSurveillance.Count < 1)
            {
                var objfrmSurveillance = new frmSurveillance();
                objfrmSurveillance.MdiParent = this;
                objfrmSurveillance.Show();
            }
            else
            {
                //this.MdiChildren.Contains(frmSurveillance)
            }
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from SurveillanceMenuHandler Method");
        }

        private void RoyalThemeMenuHandler(NCommand cmd)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into RoyalThemeMenuHandler Method");
            cmd.Checked = true;
            SetTheme("LtechRoyal", cmd);
            //ThreadPool.QueueUserWorkItem(() => SetTheme("LtechRoyal")); 


            //FileHandling.WriteDevelopmentLog("Main Form : Exit from RoyalThemeMenuHandler Method");
        }

        private void AquaThemeMenuHandler(NCommand cmd)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into AquaThemeMenuHandler Method");

            cmd.Checked = true;
            SetTheme("LtechAqua", cmd);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from AquaThemeMenuHandler Method");
        }

        private void MoonLightThemeMenuHandler(NCommand cmd)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into MoonLightThemeMenuHandler Method");

            cmd.Checked = true;
            SetTheme("LtechMoonLight", cmd);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from MoonLightThemeMenuHandler Method");
        }

        private void WoodThemeMenuHandler(NCommand cmd)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into WoodThemeMenuHandler Method");

            cmd.Checked = true;
            SetTheme("Wood", cmd);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from WoodThemeMenuHandler Method");
        }
        private void GreenThemeMenuHandler(NCommand cmd)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into WoodThemeMenuHandler Method");

            cmd.Checked = true;
            SetTheme("Green", cmd);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from WoodThemeMenuHandler Method");
        }
        private void AccountsInfoMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into AccountsInfoMenuHandler Method");

            //frmAccountsToTrade objfrmAccountsToTrade = new frmAccountsToTrade();
            //objfrmAccountsToTrade.MdiParent = this;
            //objfrmAccountsToTrade.Show();
            ////objfrmAccountsToTrade.Show(this);
            if (_ctlAccount == null)
            {
                _ctlAccount = new ctlAccountsToTrade();
            }
            AddToPanel(_ctlAccount);
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from AccountsInfoMenuHandler Method");
        }

        private void MarketQuoteMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into MarketQuoteMenuHandler Method");

            //if (frmMarketQuote.Count < 4)
            //{
            //    var objfrmMarketQuote = new frmMarketQuote();
            //    objfrmMarketQuote.MdiParent = this;
            //    objfrmMarketQuote.PortFolios = Portfolio;
            //    objfrmMarketQuote.Show();
            //}
            if (_ctlMQ == null)
            {
                _ctlMQ = new ctlMarketQuote(Portfolio);

            }
            _ctlMQ.Portfolios = Portfolio;
            if (Properties.Settings.Default.MarketQuoteInDoc)
            {
                CreateNuiDocument(_ctlMQ, "Market Quote");
            }
            else
            {
                if (Properties.Settings.Default.MarketQuoteZone == 1)
                {
                    nDockPanelHost1.AddChild(this.QuotePanel, Properties.Settings.Default.MarketQuoteIndex);
                }
                else
                {
                    nDockPanelHost2.AddChild(this.QuotePanel, Properties.Settings.Default.MarketQuoteIndex);
                }
                AddToPanel(_ctlMQ);
            }
            //_ctlMQ.SetValuesFromWorkSpace(portfolio);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from MarketQuoteMenuHandler Method");
        }

        private void NewChartMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into NewChartMenuHandler Method");
            {
                frmBase objfrmBase = new frmBase();
                uctl.uctlSelectChartSymbol objuctluctlSelectChartSymbol = new uctlSelectChartSymbol();
                objuctluctlSelectChartSymbol.OnOkClick += new Action<string, string>(objuctluctlSelectChartSymbol_OnOkClick);
                objfrmBase.Controls.Clear();
                objfrmBase.Controls.Add(objuctluctlSelectChartSymbol);
                objfrmBase.ClientSize = objuctluctlSelectChartSymbol.Size;
                objfrmBase.Title = "Select Symbol";
                objfrmBase.MaximizeBox = false;
                objuctluctlSelectChartSymbol.Dock = DockStyle.Fill;
                objfrmBase.StartPosition = FormStartPosition.CenterParent;
                objfrmBase.ShowDialog();
            }
            //var objfrmChart = new frmNewChart { MdiParent = this, Text = "New Chart" };
            //objfrmChart.Show();
            //ctlNewChart newChart = new ctlNewChart();
            //CreateNuiDocument(newChart, "Chart");
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from NewChartMenuHandler Method");
        }

        void objuctluctlSelectChartSymbol_OnOkClick(string symbol, string periodicity)
        {
            string docname = symbol + "-" + periodicity + "_" + "Chart";
            if (!IsDocumentExists(docname))
            {

                WPFChartControl objWPFChartControl = new WPFChartControl(symbol, Convert.ToInt32(periodicity.Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries)[0]), this);
                CreateNuiDocument(objWPFChartControl, docname);
                NCommand objNCommand = null;
                switch (periodicity)
                {
                    case "1 MINUTE":
                        objNCommand = ui_mnuPeriodicity1Minute;
                        break;
                    case "5 MINUTE":
                        objNCommand = ui_mnuPeriodicity5Minute;
                        break;
                    case "15 MINUTE":
                        objNCommand = ui_mnuPeriodicity15Minute;
                        break;
                    case "30 MINUTE":
                        objNCommand = ui_mnuPeriodicity30Minute;
                        break;
                    case "DAILY":
                        objNCommand = ui_mnuPeriodicityDaily;
                        break;
                    case "WEEKLY":
                        objNCommand = ui_mnuPeriodicityWeekly;
                        break;
                    case "MONTHLY":
                        objNCommand = ui_mnuPeriodicityMonthly;
                        break;
                }
                if (objNCommand == null)
                    return;
                ManagePeriodicityMenuChecking(objNCommand);
            }
        }

        public object GetPortfolios(string UserCode)
        {
            object objportfolio = null;
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into GetPortfolios Method");
            string defaultportfoliopath = ClsPalsaUtility.GetDefaultPortfolioFile();
            string userportfoliopath = ClsPalsaUtility.GetPortfolioFileName(UserCode);
            if (File.Exists(userportfoliopath))
            {
                FileStream streamRead = File.OpenRead(userportfoliopath);

                var binaryRead = new BinaryFormatter();
                if (streamRead.Length > 0)
                    objportfolio = binaryRead.Deserialize(streamRead);
                streamRead.Close();
            }
            else if (File.Exists(defaultportfoliopath))
            {
                FileStream streamRead = File.OpenRead(defaultportfoliopath);
                var binaryRead = new BinaryFormatter();
                if (streamRead.Length > 0)
                    objportfolio = binaryRead.Deserialize(streamRead);
                streamRead.Close();
            }
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from GetPortfolios Method");
            return objportfolio;
        }

        private void GetProfiles()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into GetProfiles Method");

            if (File.Exists(ClsPalsaUtility.GetProfileFileName()))
            {
                FileStream streamRead = File.OpenRead(ClsPalsaUtility.GetProfileFileName());

                var binaryRead = new BinaryFormatter();
                _profiles = binaryRead.Deserialize(streamRead);
                streamRead.Close();
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from GetProfiles Method");
        }

        private void LoadImages()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into LoadImages Method");

            Image imgFile = new Bitmap(Resources.file_strip);
            Image imgView = new Bitmap(Resources.viewstrip_png);
            Image imgMarket = new Bitmap(Resources.marketstip);
            Image imgWindow = new Bitmap(Resources.windowstrip);
            Image imgOrder = new Bitmap(Resources.order_strip);
            Image imgTool = new Bitmap(Resources.tool_strip);

            Image imgToolbar = new Bitmap(Resources.toolbarstrip20);

            Image imgTrackCursor = new Bitmap(Resources.cursor);
            Image imgVertical = new Bitmap(Resources.vertical_line);
            Image imgHori = new Bitmap(Resources.horizontal_line);
            Image imgText = new Bitmap(Resources.text);
            Image imgTextLevel = new Bitmap(Resources.text_level);
            Image imgCrossHair = new Bitmap(Resources.cross_hair);
            Image imgEquidistance = new Bitmap(Resources.equidistance);
            Image imgFabiconArc = new Bitmap(Resources.fabiconn_arc);
            Image imgFabiconFan = new Bitmap(Resources.fabiconn_fan);
            Image imgFabiconnRetracement = new Bitmap(Resources.fabicoon_retracement);
            Image imgGannFan = new Bitmap(Resources.gann_fan);
            Image imgGrid = new Bitmap(Resources.grid);
            Image imgVolume = new Bitmap(Resources.volume);
            Image imgZoomIn = new Bitmap(Resources.zoom_out);
            Image imgZoomOut = new Bitmap(Resources.zoom_in);

            Image imgM1 = new Bitmap(Resources.m1);
            Image imgM5 = new Bitmap(Resources.m5);
            Image imgM15 = new Bitmap(Resources.m15);
            Image imgM30 = new Bitmap(Resources.m30);
            Image imgH1 = new Bitmap(Resources.h1);
            Image imgD1 = new Bitmap(Resources.daily);
            Image imgW1 = new Bitmap(Resources.W1);
            Image imgMN = new Bitmap(Resources.MN);

            Image imgAutoScroll = new Bitmap(Resources.auto_scroll);
            Image imgChartShift = new Bitmap(Resources.chart_shift);
            Image imgBarChart = new Bitmap(Resources.bar);
            Image imgCandleChart = new Bitmap(Resources.candle_chart);
            Image imgLineChart = new Bitmap(Resources.line_chart);
            Image imgIndicators = new Bitmap(Resources.indicator);
            var imListCharts = new ImageList();
            imListCharts.ImageSize = new Size(24, 24);

            var toolImgList = new ImageList();
            toolImgList.ImageSize = new Size(20, 20);
            toolImgList.Images.AddStrip(imgToolbar);



            //imListCharts.Images.AddStrip(imgTrackCursor);
            //imListCharts.Images.AddStrip(imgVertical);
            //imListCharts.Images.AddStrip(imgHori);
            //imListCharts.Images.AddStrip(imgText);
            //imListCharts.Images.AddStrip(imgTextLevel);
            //imListCharts.Images.AddStrip(imgCrossHair);
            //imListCharts.Images.AddStrip(imgEquidistance);
            //imListCharts.Images.AddStrip(imgFabiconArc);
            //imListCharts.Images.AddStrip(imgFabiconFan);
            //imListCharts.Images.AddStrip(imgFabiconnRetracement);
            //imListCharts.Images.AddStrip(imgGannFan);
            //imListCharts.Images.AddStrip(imgGrid);
            //imListCharts.Images.AddStrip(imgVolume);
            //imListCharts.Images.AddStrip(imgZoomIn);
            //imListCharts.Images.AddStrip(imgZoomOut);

            //imListCharts.Images.AddStrip(imgM1);
            //imListCharts.Images.AddStrip(imgM5);
            //imListCharts.Images.AddStrip(imgM15);
            //imListCharts.Images.AddStrip(imgM30);
            //imListCharts.Images.AddStrip(imgH1);
            //imListCharts.Images.AddStrip(imgD1);
            //imListCharts.Images.AddStrip(imgW1);
            //imListCharts.Images.AddStrip(imgMN);
            //imListCharts.Images.AddStrip(imgAutoScroll);
            //imListCharts.Images.AddStrip(imgChartShift);
            //imListCharts.Images.AddStrip(imgBarChart);
            //imListCharts.Images.AddStrip(imgCandleChart);
            //imListCharts.Images.AddStrip(imgLineChart);
            //imListCharts.Images.AddStrip(imgIndicators);
            var imList = new ImageList();
            imList.Images.AddStrip(imgFile);
            imList.Images.AddStrip(imgView);
            imList.Images.AddStrip(imgMarket);
            imList.Images.AddStrip(imgWindow);
            imList.Images.AddStrip(imgOrder);
            imList.Images.AddStrip(imgTool);

            ui_nmnuBar.ImageList = imList;

            ui_ndtToolBar.ImageList = toolImgList;

            ui_ncmdFileLogin.Properties.ImageIndex = 0;
            ui_ncmdFileLogOff.Properties.ImageIndex = 1;
            ui_ncmdFileSaveWorkSpace.Properties.ImageIndex = 2;
            ui_ncmdFileLoadWorkSpace.Properties.ImageIndex = 3;
            ui_ncmdFileExit.Properties.ImageIndex = 4;

            ui_ncmdViewLanguages.Properties.ImageIndex = 5;
            ui_ncmdViewThemes.Properties.ImageIndex = 6;
            ui_ncmdViewTrade.Properties.ImageIndex = 7;
            ui_ncmdViewNetPosition.Properties.ImageIndex = 8;
            ui_ncmdViewMsgLog.Properties.ImageIndex = 9;
            ui_ncmdViewParticipantList.Properties.ImageIndex = 13;
            ui_ncmdViewAccountsInfo.Properties.ImageIndex = 12;
            ui_ncmdViewIndicesView.Properties.ImageIndex = 11;
            ui_ncmdViewContractInfo.Properties.ImageIndex = 10;


            ui_ncmdMarketMarketWatch.Properties.ImageIndex = 14;
            ui_ncmdMarketQuote.Properties.ImageIndex = 19;
            ui_ncmdMarketMarketPicture.Properties.ImageIndex = 15;
            ui_ncmdMarketSnapQuote.Properties.ImageIndex = 16;
            ui_ncmdMarketMarketStatus.Properties.ImageIndex = 17;
            ui_ncmdMarketTopGainerLosers.Properties.ImageIndex = 18;

            ui_ncmdWindowCascade.Properties.ImageIndex = 23;
            ui_ncmdWindowClose.Properties.ImageIndex = 21;
            ui_ncmdWindowCloseAll.Properties.ImageIndex = 22;
            ui_ncmdWindowNewWindow.Properties.ImageIndex = 20;
            ui_ncmdWindowTileHorizontally.Properties.ImageIndex = 25;
            ui_ncmdWindowTileVertically.Properties.ImageIndex = 24;

            ui_ncmdOrdersOrderBook.Properties.ImageIndex = 26;
            ui_ncmdOrdersPlaceOrder.Properties.ImageIndex = 27;
            //ui_ncmdOrdersPlaceSellOrders.Properties.ImageIndex = 28;

            ui_ncmdTradesTrades.Properties.ImageIndex = 7;

            ui_ncmdToolsCustomize.Properties.ImageIndex = 29;
            ui_ncmdToolsLockWorkStation.Properties.ImageIndex = 30;
            ui_ncmdToolsPortfolio.Properties.ImageIndex = 31;
            ui_ncmdToolsPreferences.Properties.ImageIndex = 32;

            ui_ntbLogin.Properties.ImageIndex = 0;
            ui_ntbLogoff.Properties.ImageIndex = 1;
            ui_ntbBackup.Properties.ImageIndex = 2;
            ui_ntbPrint.Properties.ImageIndex = 3;
            ui_ntbMessageLog.Properties.ImageIndex = 4;
            ui_ntbOrderEntry.Properties.ImageIndex = 5;
            ui_ntbOrderBook.Properties.ImageIndex = 6;
            ui_ntbTrades.Properties.ImageIndex = 7;
            ui_ntbNetPosition.Properties.ImageIndex = 8;
            ui_ntbMarketWatch.Properties.ImageIndex = 9;
            ui_ntbMarketPicture.Properties.ImageIndex = 10;
            ui_ntbContractInfo.Properties.ImageIndex = 11;
            ui_ntbModifyOrder.Properties.ImageIndex = 12;
            ui_ntbCancelOrder.Properties.ImageIndex = 13;
            ui_ntbCancelAllOrders.Properties.ImageIndex = 14;



            //ToolbarChart.ImageList = imListCharts;
            //TrackCursor.Properties.ImageIndex = 0;
            //cmdVerticalLine.Properties.ImageIndex = 1;
            //cmdHoriLine.Properties.ImageIndex = 2;
            //cmdText.Properties.ImageIndex = 3;
            //TextLevel.Properties.ImageIndex = 4;
            //CrossHair.Properties.ImageIndex = 5;
            //Equidistance.Properties.ImageIndex = 6;
            //Fabiconn_arc.Properties.ImageIndex = 7;
            //Fabiconn_fan.Properties.ImageIndex = 8;
            //Fabiconn_retracement.Properties.ImageIndex = 9;
            //Gann_fan.Properties.ImageIndex = 10;
            //Grid.Properties.ImageIndex = 11;
            //Volume.Properties.ImageIndex = 12;

            //ZoomIn.Properties.ImageIndex = 13;
            //ZoomOut.Properties.ImageIndex = 14;
            //M1.Properties.ImageIndex = 15;
            //M5.Properties.ImageIndex = 16;
            //M15.Properties.ImageIndex = 17;
            //M30.Properties.ImageIndex = 18;
            //H1.Properties.ImageIndex = 19;
            //D1.Properties.ImageIndex = 20;
            //W1.Properties.ImageIndex = 21;
            //MN.Properties.ImageIndex = 22;
            //AutoScroll.Properties.ImageIndex = 23;
            //ChartShift.Properties.ImageIndex = 24;
            //BarChart.Properties.ImageIndex = 25;
            //CandleChart.Properties.ImageIndex = 26;
            //LineChart.Properties.ImageIndex = 27;

            TrackCursor.Properties.ImageInfo.Image = imgTrackCursor;
            cmdVerticalLine.Properties.ImageInfo.Image = imgVertical;
            cmdHoriLine.Properties.ImageInfo.Image = imgHori;
            cmdText.Properties.ImageInfo.Image = imgText;
            TextLevel.Properties.ImageInfo.Image = imgTextLevel;
            CrossHair.Properties.ImageInfo.Image = imgCrossHair;
            Equidistance.Properties.ImageInfo.Image = imgEquidistance;
            Fabiconn_arc.Properties.ImageInfo.Image = imgFabiconArc;
            Fabiconn_fan.Properties.ImageInfo.Image = imgFabiconFan;
            Fabiconn_retracement.Properties.ImageInfo.Image = imgFabiconnRetracement;
            Gann_fan.Properties.ImageInfo.Image = imgGannFan;
            Grid.Properties.ImageInfo.Image = imgGrid;
            Volume.Properties.ImageInfo.Image = imgVolume;

            ZoomIn.Properties.ImageInfo.Image = imgZoomIn;
            ZoomOut.Properties.ImageInfo.Image = imgZoomOut;
            M1.Properties.ImageInfo.Image = imgM1;
            M5.Properties.ImageInfo.Image = imgM5;
            M15.Properties.ImageInfo.Image = imgM15;
            M30.Properties.ImageInfo.Image = imgM30;
            H1.Properties.ImageInfo.Image = imgH1;
            D1.Properties.ImageInfo.Image = imgD1;
            W1.Properties.ImageInfo.Image = imgW1;
            MN.Properties.ImageInfo.Image = imgMN;
            AutoScroll.Properties.ImageInfo.Image = imgAutoScroll;
            ChartShift.Properties.ImageInfo.Image = imgChartShift;
            BarChart.Properties.ImageInfo.Image = imgBarChart;
            CandleChart.Properties.ImageInfo.Image = imgCandleChart;
            LineChart.Properties.ImageInfo.Image = imgLineChart;

            //Image imgNewChart=new Bitmap(Resources.new_chart);
            //Image imgChartPeriodicity=new Bitmap(Resources.period);
            //Image imgChartZoomIn=new Bitmap(Resources.zoom_in);
            //Image imgChartZoomOut=new Bitmap(Resources.zoom_out);
            //Image imgChartTrackCursor=new Bitmap(Resources.cursor);
            //var imMenuChart = new ImageList();
            //imMenuChart.ImageSize = new Size(24, 24);
            //imMenuChart.Images.AddStrip(imgNewChart);
            //imMenuChart.Images.AddStrip(imgChartPeriodicity);
            //imMenuChart.Images.AddStrip(imgChartZoomIn);
            //imMenuChart.Images.AddStrip(imgChartZoomOut);
            //imMenuChart.Images.AddStrip(imgChartTrackCursor);
            //ui_mnuCharts.Properties.ImageList = imMenuChart;
            //this.ui_mnuChartsNewChart.Properties.ImageIndex = 0;
            //this.ui_mnuChartsPeriodicity.Properties.ImageIndex = 1;
            //this.ui_mnuChartsZoomIn.Properties.ImageIndex = 2;
            //this.ui_mnuChartsZoomOut.Properties.ImageIndex = 3;
            //this.ui_mnuChartsTrackCursor.Properties.ImageIndex =4;
            //Image imgPeriod1Min=new Bitmap(Resources.m1);
            //Image imgPerion5Min=new Bitmap(Resources.m5);
            //Image imgPeriod15Min=new Bitmap(Resources.m15);
            //Image imgPeriod30Min=new Bitmap(Resources.m30);
            //Image imgPeriod1Hr=new Bitmap(Resources.h1);
            //Image imgPeriodDaily=new Bitmap(Resources.daily);
            //Image imgPeriodWeekly=new Bitmap(Resources.W1);
            //Image imgPeriodMonthly=new Bitmap(Resources.MN);
            //Image imgBarChart=new Bitmap(Resources.bar);
            //Image imgCandleChart=new Bitmap(Resources.candle_chart);
            //Image imgLineChart=new Bitmap(Resources.line_chart);


            //imMenuChart.Images.AddStrip(imgPeriod1Min);
            //imMenuChart.Images.AddStrip(imgPerion5Min);
            //imMenuChart.Images.AddStrip(imgPeriod15Min);
            //imMenuChart.Images.AddStrip(imgPeriod30Min);
            //imMenuChart.Images.AddStrip(imgPeriod1Hr);
            //imMenuChart.Images.AddStrip(imgPeriodDaily);
            //imMenuChart.Images.AddStrip(imgPeriodWeekly);
            //imMenuChart.Images.AddStrip(imgPeriodMonthly);

            //imMenuChart.Images.AddStrip(imgBarChart);
            //imMenuChart.Images.AddStrip(imgCandleChart);
            //imMenuChart.Images.AddStrip(imgLineChart);


            //this.ui_mnuChartsChartType,
            //this.ui_mnuChartsPriceType,

            //this.ui_mnuChartsVolume,
            //this.ui_mnuChartsGrid,
            //this.ui_mnuChart3DStyle,
            //this.ui_mnuChartsSnapshot}

            //this.ui_mnuPeriodicity1Minute.Properties.ImageIndex = 2;
            //this.ui_mnuPeriodicity5Minute.Properties.ImageIndex = 3;
            //this.ui_mnuPeriodicity15Minute.Properties.ImageIndex =4;
            //this.ui_mnuPeriodicity30Minute.Properties.ImageIndex = 5;
            //this.ui_mnuPeriodicity1Hour.Properties.ImageIndex = 6;
            //this.ui_mnuPeriodicityDaily.Properties.ImageIndex = 7;
            //this.ui_mnuPeriodicityWeekly.Properties.ImageIndex = 8;
            //this.ui_mnuPeriodicityMonthly.Properties.ImageIndex = 9;


            //this.ui_mnuChartTypeBarChart.Properties.ImageIndex = 11;
            //this.ui_mnuChartTypeCandleChart.Properties.ImageIndex = 12;
            //this.ui_mnuChartTypeLineChart.Properties.ImageIndex = 13;


            //Images for charts

            TrackCursor.Properties.Visible = false;
            cmdText.Properties.Visible = true;
            TextLevel.Properties.Visible = true;
            CrossHair.Properties.Visible = true;
            Equidistance.Properties.Visible = true;
            Fabiconn_arc.Properties.Visible = true;
            Fabiconn_fan.Properties.Visible = true;
            Fabiconn_retracement.Properties.Visible = true;
            Gann_fan.Properties.Visible = true;
            Grid.Properties.Visible = true;
            Volume.Properties.Visible = true;

            H1.Properties.Visible = true;
            D1.Properties.Visible = true;
            W1.Properties.Visible = true;
            MN.Properties.Visible = true;
            M1.Properties.Visible = true;
            M5.Properties.Visible = true;
            M15.Properties.Visible = true;
            M30.Properties.Visible = true;
            AutoScroll.Properties.Visible = true;
            ChartShift.Properties.Visible = true;
            BarChart.Properties.Visible = true;
            CandleChart.Properties.Visible = true;
            LineChart.Properties.Visible = true;

            //To visible false for time being
            ChartShift.Properties.Visible = false;
            Volume.Properties.Visible = false;
            Equidistance.Properties.Visible = false;
            ui_mnuChartsTrackCursor.Properties.Visible = false;
            ui_mnuChartsVolume.Properties.Visible = false;
            cmdText.Properties.Visible = false;

            AddIndicatorsInCombo();


            ui_mnuSnapshotPrint.Enabled = true;
            ui_mnuSnapshotSave.Enabled = true;


            //FileHandling.WriteDevelopmentLog("Main Form : Exit from LoadImages Method");
        }

        private void AddIndicatorsInCombo()
        {
            List<string> lstIndictors = PALSA.Cls.ClsGlobal.GetIndicators();

            //Add Indicators to menu Also
            foreach (var item in lstIndictors)
            {
                cbIndicators.Items.Add(item);

                NCommandProperties Prop = new NCommandProperties();
                Prop.Text = item;
                NCommand cmd = new NCommand(Prop);
                //cmd.Click -= new CommandEventHandler(cmd_Click);
                cmd.Click += new CommandEventHandler(cmd_Click);
                ui_nmnuTechnicalAnalysisIndicatorList.Commands.Add(cmd);
            }
        }

        private void cbIndicators_Click(object sender, CommandEventArgs e)
        {
            NCommand cmd = sender as NCommand;
            string Ind = cbIndicators.HostedControl.Text;
            int node = cbIndicators.Items.IndexOf(Ind);
            ApplyIndicatorNew(Ind, node);
        }

        void cmd_Click(object sender, CommandEventArgs e)
        {
            NCommand cmd = sender as NCommand;
            string Ind = cmd.Properties.Text;
            int node = ui_nmnuTechnicalAnalysisIndicatorList.Commands.IndexOf(cmd);
            ApplyIndicatorNew(Ind, node);
        }

        private void ApplyIndicatorNew(string str, int node)
        {
            if (m_DockManager.DocumentManager.ActiveDocument != null)
            {
                if (m_DockManager.DocumentManager.ActiveDocument.Key.Contains("Chart"))
                {
                    IndicatorSelection Ind = new IndicatorSelection { IndicatorName = str, node = node };
                    //((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).AddIndicator(Ind, node);
                    ((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).AddIndicator(str);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="str"></param>
        /// <param name="ncmd"></param>
        public void SetHotKey(string name, string str, NCommand ncmd)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into SetHotKey Method");

            if (ncmd != null)
            {
                ncmd.Properties.Shortcut.Modifiers = Keys.None;


                if ((str.Equals("[NONE]", StringComparison.InvariantCultureIgnoreCase)) || (!str.Contains("+")))
                {
                    if (!str.Contains("[NONE]"))
                    {
                        var key = (Keys)Enum.Parse(typeof(Keys), str);
                        ncmd.Properties.Shortcut = new NShortcut(key, 0);
                    }
                    else
                        return;
                }
                string[] strArray = str.Split('+'); //strArray[1].Split('+');
                for (int strLoop = 0; strLoop < strArray.Length; strLoop++)
                {
                    string item = strArray[strLoop];
                    item = item.Trim();

                    if (item.Equals("Alt", StringComparison.InvariantCultureIgnoreCase))
                    {
                        ncmd.Properties.Shortcut.Modifiers = ncmd.Properties.Shortcut.Modifiers | Keys.Alt;
                    }
                    else if (item.Equals("Shift", StringComparison.InvariantCultureIgnoreCase))
                    {
                        ncmd.Properties.Shortcut.Modifiers = ncmd.Properties.Shortcut.Modifiers | Keys.Shift;
                    }
                    else if (item.Equals("Ctrl", StringComparison.InvariantCultureIgnoreCase))
                    {
                        ncmd.Properties.Shortcut.Modifiers = ncmd.Properties.Shortcut.Modifiers | Keys.Control;
                    }
                    else
                    {
                        switch (item)
                        {
                            case "0":
                                item = "D0";
                                break;
                            case "1":
                                item = "D1";
                                break;
                            case "2":
                                item = "D2";
                                break;
                            case "3":
                                item = "D3";
                                break;
                            case "4":
                                item = "D4";
                                break;
                            case "5":
                                item = "D5";
                                break;
                            case "6":
                                item = "D6";
                                break;
                            case "7":
                                item = "D7";
                                break;
                            case "8":
                                item = "D8";
                                break;
                            case "9":
                                item = "D9";
                                break;
                            default:
                                break;
                        }
                        ncmd.Properties.Shortcut.Key = (Keys)Enum.Parse(typeof(Keys), item);
                    }
                }
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from SetHotKey Method");
        }

        /// <summary>
        /// Close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nmnuCmdExit_Click(object sender, CommandEventArgs e)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into nmnuCmdExit_Click Method");

            Close();

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from nmnuCmdExit_Click Method");
        }


        /// <summary>
        /// Close the form on Escape keyboad key 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into FrmMain_KeyDown Method");
            if (e.KeyCode == Keys.Enter)
            {
                AddFilterDataToMarketWatch();
            }
            PopulateReverceHotKeySettingsHashTable();

            if (_revercedHotKeySettingsHashTable.ContainsKey(e.KeyCode.ToString()) && e.Modifiers == Keys.None)
            {
                var x = e.KeyCode + "";
                var commandID =
                    (CommandIDS)Enum.Parse(typeof(CommandIDS), _revercedHotKeySettingsHashTable[x].ToString());
                CallShortcut(commandID);
            }
            if (e.KeyCode == Keys.Add)
                CallShortcut((CommandIDS)Enum.Parse(typeof(CommandIDS), "PLACE_ORDER"));
            //if (e.KeyCode == Keys.Subtract)
            //    CallShortcut((CommandIDS)Enum.Parse(typeof(CommandIDS), "PLACE_SELL_ORDER"));
            //if (e.KeyCode == Keys.Escape)
            //{
            //    if (this.ActiveMdiChild != null)
            //    {
            //        this.ActiveMdiChild.Close();
            //    }
            //}
            if (e.Modifiers == Keys.Alt && e.KeyCode == Keys.W)
            {
                ui_nmnuWindows_Select(null, null);
            }

            //Code by vijay on 29 June 2012.
            if (e.KeyCode == Keys.Escape)
            {
                if (ActiveMdiChild != null && ActiveMdiChild as frmMarketWatch == null)
                {
                    ActiveMdiChild.Close();
                }
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from FrmMain_KeyDown Method");
        }

        private void AddFilterDataToMarketWatch()
        {
            ////FileHandling.WriteDevelopmentLog("Main Form : Enter into AddFilterDataToMarketWatch Method");

            //if (ActiveMdiChild != null)
            //{
            //    if (((frmBase)ActiveMdiChild).Formkey.Contains("MARKET_WATCH"))
            //    {
            //        var formKey =
            //            (((frmMarketWatch)ActiveMdiChild).Formkey.Split(new[] { '/' },
            //                                                             StringSplitOptions.RemoveEmptyEntries))[0];
            //        //if (formKey == CommandIDS.MARKET_WATCH.ToString())
            //        {
            //            //string keyText = ui_ncmbInstrumentType.ControlText + "_" + ui_ncmbSymbol.ControlText + "_" + ui_ncmbExpiryDate.ControlText;

            //            //List<string> lstContracts = clsTWSContractManager.INSTANCE.GetAllContracts(ui_ncmbInstrumentType.ControlText, ui_ncmbSymbol.ControlText);
            //            //foreach (string contract in lstContracts)
            //            //{
            //            InstrumentSpec objInstrumentSpec = ClsTWSContractManager.INSTANCE.GetContractSpec(ui_ncmbExpiryDate.ControlText,
            //                                                                                         ui_ncmbInstrumentType.ControlText, ui_ncmbSymbol.ControlText
            //                                                                                         );

            //            if (objInstrumentSpec != null)
            //            {
            //                string keyText = Symbol.getKey(objInstrumentSpec)[0];
            //                ((frmMarketWatch)ActiveMdiChild).AddRowToMarketWatch(keyText, objInstrumentSpec);
            //            }
            //            //}
            //        }
            //    }
            //}

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from AddFilterDataToMarketWatch Method");
        }

        /// <summary>
        /// Opens login dialog
        /// </summary>
        public DialogResult LoginMenuHandler()
        {
            DialogResult objDialogResult = DialogResult.Cancel;
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into LoginMenuHandler Method");
            if (MdiChildren.Count() == 0)
            {
                objForm.Controls.Clear();
                _objuctlLogin.Visible = true;
                if (Properties.Settings.Default.SavePassword)
                {
                    _objuctlLogin.ui_ntxtUserCode.Text = Properties.Settings.Default.LoginName;
                    _objuctlLogin.ui_ntxtPassword.Text = Properties.Settings.Default.LoginPassword;
                }
                else
                {
                    _objuctlLogin.ui_ntxtUserCode.Text = string.Empty;
                    _objuctlLogin.ui_ntxtPassword.Text = string.Empty;
                }
                _objuctlLogin.SavePassword = Properties.Settings.Default.SavePassword;
                _objuctlLogin.OnOkClick -= objuctlLogin_OnOkClick;
                _objuctlLogin.OnCancelClick -= objuctlLogin_OnCancelClick;
                _objuctlLogin.OnOkClick += objuctlLogin_OnOkClick;
                _objuctlLogin.OnCancelClick += objuctlLogin_OnCancelClick;
                objForm.KeyDown -= objForm_KeyDown;
                objForm.KeyDown += objForm_KeyDown;
                objForm.Controls.Add(_objuctlLogin);
                objForm.Size = _objuctlLogin.Size;
                objForm.FormBorderStyle = FormBorderStyle.None;
                objForm.StartPosition = FormStartPosition.CenterScreen;
                //objForm.TopMost = true;

                //objForm.MdiParent = this;
                objForm.ShowInTaskbar = false;
                objForm.KeyPreview = true;
                objDialogResult = objForm.ShowDialog();
            }
            else
            {
                if (ClsCommonMethods.ShowMessageBox("All opened windows will be closed are you sure to relogin.") == DialogResult.Yes)
                {
                    foreach (frmBase frm in MdiChildren)
                    {
                        frm.Close();
                    }
                    LoginMenuHandler();
                }
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from LoginMenuHandler Method");
            return objDialogResult;
        }


        public void LogoffMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into LogoffMenuHandler Method");

            DialogResult result = ClsCommonMethods.ShowMessageBox("Are you sure want to Logoff?");
            if (result == DialogResult.Yes)
            {
                Process.GetCurrentProcess().Kill();
                Environment.Exit(1);
            }
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from LogoffMenuHandler Method");
        }


        private void objForm_KeyDown(object sender, KeyEventArgs e)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into objForm_KeyDown Method");

            if (e.KeyCode == Keys.Enter)
            {
                objuctlLogin_OnOkClick(null, null);
            }
            if (e.KeyData == Keys.Escape)
            {
                objuctlLogin_OnCancelClick(null, null);
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from objForm_KeyDown Method");
        }

        /// <summary>
        /// Closes login form
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        private void objuctlLogin_OnCancelClick(object arg1, EventArgs arg2)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into objuctlLogin_OnCancelClick Method");

            objForm.Close();

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from objuctlLogin_OnCancelClick Method");
        }

        //string getOSInfo()
        //{
        //    //Get Operating system information.    
        //    OperatingSystem os = Environment.OSVersion;
        //    //Get version information about the os.    
        //    Version vs = os.Version;
        //    //Variable to hold our return value    
        //    string operatingSystem = "";
        //    if (os.Platform == PlatformID.Win32Windows)
        //    {
        //        //This is a pre-NT version of Windows        
        //        switch (vs.Minor)
        //        {
        //            case 0:
        //                operatingSystem = "95";
        //                break;
        //            case 10:
        //                if (vs.Revision.ToString() == "2222A")
        //                    operatingSystem = "98SE";
        //                else
        //                    operatingSystem = "98";
        //                break;
        //            case 90:
        //                operatingSystem = "Me";
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    else if (os.Platform == PlatformID.Win32NT)
        //    {
        //        switch (vs.Major)
        //        {
        //            case 3:
        //                operatingSystem = "NT 3.51";
        //                break;
        //            case 4:
        //                operatingSystem = "NT 4.0";
        //                break;
        //            case 5:
        //                if (vs.Minor == 0)
        //                    operatingSystem = "2000";
        //                else
        //                    operatingSystem = "XP";
        //                break;
        //            case 6:
        //                if (vs.Minor == 0)
        //                    operatingSystem = "Vista";
        //                else
        //                    operatingSystem = "7";
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    //Make sure we actually got something in our OS check    
        //    //We don't want to just return " Service Pack 2" or " 32-bit"    
        //    //That information is useless without the OS version.    
        //    if (operatingSystem != "")
        //    {
        //        //Got something.  Let's prepend "Windows" and get more info.        
        //        operatingSystem = "Windows " + operatingSystem;
        //        //See if there's a service pack installed.       
        //        if (os.ServicePack != "")
        //        {
        //            //Append it to the OS name.  i.e. "Windows XP Service Pack 3"            
        //            operatingSystem += " " + os.ServicePack;
        //        }
        //        //Append the OS architecture.  i.e. "Windows XP Service Pack 3 32-bit"        
        //        //operatingSystem += " " + getOSArchitecture().ToString() + "-bit";    
        //    }
        //    //Return the information we've gathered.    
        //    return operatingSystem;
        //}

        /// <summary>
        /// Perform authentication of user 
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        private void objuctlLogin_OnOkClick(object arg1, EventArgs arg2)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into objuctlLogin_OnOkClick Method");
            clsTWSDataManagerJSON.INSTANCE.Refresh();
            clsTWSOrderManagerJSON.INSTANCE.Refresh();

            if (_objuctlLogin.ui_ntxtUserCode.Text == string.Empty)
            {
                ClsCommonMethods.ShowErrorBox("Please enter user code");
                _objuctlLogin.ui_ntxtUserCode.Focus();
                return;
            }
            if (_objuctlLogin.ui_ntxtPassword.Text == string.Empty)
            {
                ClsCommonMethods.ShowErrorBox("Please enter user password");
                _objuctlLogin.ui_ntxtPassword.Focus();
                return;
            }

            ui_lblTopStatus1.Text = _objuctlLogin.UserCode;
            string username = _objuctlLogin.UserCode; //string.Empty;
            string pwd = _objuctlLogin.Password; // string.Empty;

            if (_objConnectionIPs != null && _objConnectionIPs.QuotesIP.ServerIP != string.Empty)
            {
                LoadServerIps();
            }



            //clsTWSDataManagerJSON.INSTANCE.Init(username, pwd, _objConnectionIPs.QuotesIP.ServerIP,
            //                                _objConnectionIPs.QuotesIP.HostIP, _objConnectionIPs.QuotesIP.PortNo);

            //clsTWSOrderManagerJSON.INSTANCE.Init(username, pwd, _objConnectionIPs.OrderIP.ServerIP,
            //                                 _objConnectionIPs.OrderIP.HostIP, _objConnectionIPs.OrderIP.PortNo);
            //ClsTWSContractManager.INSTANCE.Init(username, pwd, _objConnectionIPs.OrderIP.ServerIP,
            //                                    _objConnectionIPs.OrderIP.HostIP, _objConnectionIPs.OrderIP.PortNo);

            clsTWSDataManagerJSON.INSTANCE.Init(username, pwd, _objConnectionIPs.QuotesIP.WebSocketHostUrl);
            clsTWSOrderManagerJSON.INSTANCE.Init(username, pwd, _objConnectionIPs.OrderIP.WebSocketHostUrl);


            if (_objuctlLogin.SavePassword)
            {
                Properties.Settings.Default.SavePassword = true;
            }
            else
            {
                Properties.Settings.Default.SavePassword = false;
            }
            objForm.Close();
            this.Refresh();

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from objuctlLogin_OnOkClick Method");
        }

        void INSTANCE_OnBothServerConnectionEvnt(string str)
        {
            if (str.ToUpper() == "DISCONNECTED")
            {
                //ui_ncmdDataServerStatus.Properties.ImageInfo.Image = Resources.Circle_Red;
                //ui_ncmdOrderServerStatus.Properties.ImageInfo.Image = Resources.Circle_Red;
                tlsOrderServerStatus.Image = Resources.Circle_Red;
                tlsDataServerStatus.Image = Resources.Circle_Red;
            }
            //ui_nServerStatus.Refresh();
        }




        private void INSTANCE_OnOrderServerConnectionEvnt(string str)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into INSTANCE_OnOrderServerConnectionEvnt Method");

            //Action A = () =>
            //               {
            //if (System.Threading.Monitor.TryEnter(ui_ncmdOrderServerStatus, 1000))
            //{
            //    try
            //    {
            //        if (str.ToUpper() == "CONNECTED")
            //        {
            //            //DisplayPopUp("Order Server", "OrderServer " + str, "green", str);
            //            ui_ncmdOrderServerStatus.Properties.ImageInfo.Image = Resources.Circle_Green;
            //        }
            //        else
            //        {
            //            //DisplayPopUp("Order Server", "OrderServer " + str, "Red", str);
            //            ui_ncmdOrderServerStatus.Properties.ImageInfo.Image = Resources.Circle_Red;
            //        }
            //        //ui_nServerStatus.Refresh();
            //    }
            //    finally
            //    {
            //        System.Threading.Monitor.Exit(ui_ncmdOrderServerStatus);
            //    }
            //}
            //                   if (str.ToUpper() == "CONNECTED")
            //                   {
            //                       DisplayPopUp("Order Server", "OrderServer " + str, "green", str);
            //                       ui_ncmdOrderServerStatus.Properties.ImageInfo.Image =
            //                           Resources.Circle_Green;
            //                   }
            //                   else
            //                   {
            //                       DisplayPopUp("Order Server", "OrderServer " + str, "Red", str);
            //                       ui_ncmdOrderServerStatus.Properties.ImageInfo.Image = Resources.Circle_Red;
            //                   }
            //                   ui_nServerStatus.Refresh();
            //               };
            //if (InvokeRequired)
            //{
            //    BeginInvoke(A);
            //}
            //else
            //{
            //    A();
            //}
            if (System.Threading.Monitor.TryEnter(tlsOrderServerStatus, 1000))
            {
                try
                {
                    if (str.ToUpper() == "CONNECTED")
                    {
                        tlsOrderServerStatus.Image = Resources.Circle_Green;
                    }
                    else
                    {
                        tlsOrderServerStatus.Image = Resources.Circle_Red;
                    }
                }
                finally
                {
                    System.Threading.Monitor.Exit(tlsOrderServerStatus);
                }
            }
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from INSTANCE_OnOrderServerConnectionEvnt Method");
        }

        public void ShowCurrentServerStatus()
        {
            //Logging.//FileHandling.WriteDevelopmentLog("MarketStatus" + count + " : Enter into AddDataToGrid Method");

            //Action A = () =>
            //               {
            //if (System.Threading.Monitor.TryEnter(ui_ncmdDataServerStatus, 1000))
            //{
            //    try
            //    {
            //        if (clsTWSDataManagerJSON.INSTANCE.IsDataMgrConnected == true)
            //            ui_ncmdDataServerStatus.Properties.ImageInfo.Image =
            //                Resources.Circle_Green;
            //        else
            //            ui_ncmdDataServerStatus.Properties.ImageInfo.Image =
            //                Resources.Circle_Red;
            //        //ui_nServerStatus.Refresh();
            //    }
            //    finally
            //    {
            //        System.Threading.Monitor.Exit(ui_ncmdDataServerStatus);
            //    }
            //}

            //if (System.Threading.Monitor.TryEnter(ui_ncmdOrderServerStatus, 1000))
            //{
            //    try
            //    {
            //        if (clsTWSOrderManagerJSON.INSTANCE.IsOrderMgrLoaded == true)
            //            ui_ncmdOrderServerStatus.Properties.ImageInfo.Image =
            //                Resources.Circle_Green;
            //        else
            //            ui_ncmdOrderServerStatus.Properties.ImageInfo.Image =
            //                Resources.Circle_Red;

            //        //ui_nServerStatus.Refresh();
            //    }
            //    finally
            //    {
            //        System.Threading.Monitor.Exit(ui_ncmdOrderServerStatus);
            //    }
            //}
            //               };
            //if (InvokeRequired)
            //{
            //    BeginInvoke(A);
            //}
            //else
            //{
            //    A();
            //}
            if (System.Threading.Monitor.TryEnter(tlsDataServerStatus, 1000))
            {
                try
                {
                    if (clsTWSDataManagerJSON.INSTANCE.IsDataMgrConnected == true)
                        tlsDataServerStatus.Image = Resources.Circle_Green;
                    else
                        tlsDataServerStatus.Image = Resources.Circle_Red;
                }
                finally
                {
                    System.Threading.Monitor.Exit(tlsDataServerStatus);
                }
            }

            if (System.Threading.Monitor.TryEnter(tlsOrderServerStatus, 1000))
            {
                try
                {
                    if (clsTWSOrderManagerJSON.INSTANCE.IsOrderMgrLoaded == true)
                        tlsOrderServerStatus.Image = Resources.Circle_Green;
                    else
                        tlsOrderServerStatus.Image = Resources.Circle_Red;
                }
                finally
                {
                    System.Threading.Monitor.Exit(tlsOrderServerStatus);
                }
            }
            //Logging.//FileHandling.WriteDevelopmentLog("MarketStatus" + count + " : Exit from AddDataToGrid Method");
        }

        private void INSTANCE_OnDataServerConnectionEvnt(string str)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into INSTANCE_OnDataServerConnectionEvnt Method");

            //Action A = () =>
            //               {
            //if (System.Threading.Monitor.TryEnter(ui_ncmdDataServerStatus, 1000))
            //{
            //    try
            //    {
            //        if (str.ToUpper() == "CONNECTED")
            //        {
            //            //DisplayPopUp("DataServer", "DataServer " + str, "green", str);
            //            ui_ncmdDataServerStatus.Properties.ImageInfo.Image = Resources.Circle_Green;
            //        }
            //        else
            //        {
            //            //DisplayPopUp("DataServer", "DataServer" + str, "Red", str);
            //            ui_ncmdDataServerStatus.Properties.ImageInfo.Image = Resources.Circle_Red;
            //        }
            //        //ui_nServerStatus.Refresh();
            //    }
            //    finally
            //    {
            //        System.Threading.Monitor.Exit(ui_ncmdDataServerStatus);
            //    }
            //}
            //                   if (str.ToUpper() == "CONNECTED")
            //                   {
            //                       //DisplayPopUp("DataServer", "DataServer " + str, "green", str);
            //                       ui_ncmdDataServerStatus.Properties.ImageInfo.Image = Resources.Circle_Green;
            //                   }
            //                   else
            //                   {
            //                       //DisplayPopUp("DataServer", "DataServer" + str, "Red", str);
            //                       ui_ncmdDataServerStatus.Properties.ImageInfo.Image = Resources.Circle_Red;
            //                   }
            //                   //ui_nServerStatus.Refresh();
            //               };
            //if (InvokeRequired)
            //{
            //    BeginInvoke(A);
            //}
            //else
            //{
            //    A();
            //}
            if (System.Threading.Monitor.TryEnter(tlsDataServerStatus, 1000))
            {
                try
                {
                    if (str.ToUpper() == "CONNECTED")
                    {
                        tlsDataServerStatus.Image = Resources.Circle_Green;
                    }
                    else
                    {
                        tlsDataServerStatus.Image = Resources.Circle_Red;
                    }
                    //ui_nServerStatus.Refresh();
                }
                finally
                {
                    System.Threading.Monitor.Exit(tlsDataServerStatus);
                }
            }
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from INSTANCE_OnDataServerConnectionEvnt Method");
        }

        /// <summary>
        /// Performs action on click of menus and commands of command bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ui_ncbmPALSA_CommandClicked(object sender, CommandEventArgs e)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into ui_ncbmPALSA_CommandClicked Method");

            if (e.Command.ParentCommand == ui_ncmdWindowWindow)
            {
                windowCommandHandler(e.Command.Properties.Text);
                return;
            }
            int commandID = -1;
            if (e.Command.Properties.Text.ToUpper() == ui_ncmdViewTicker.Properties.Text.ToUpper())
            {
                commandID = ui_ncmdViewTicker.Properties.ID;
            }
            else
            {
                commandID = e.Command.Properties.ID;
            }
            CallShortcut((CommandIDS)commandID);
            Properties.Settings.Default.Save();
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from ui_ncbmPALSA_CommandClicked Method");
        }

        private void windowCommandHandler(string title)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into windowCommandHandler Method");

            foreach (Form item in _ChildFormList.Where(item => item.Text == title))
            {
                item.Activate();
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from windowCommandHandler Method");
        }

        /// <summary>
        /// Displays indexbar window
        /// </summary>
        private void IndexBarToolBarHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into IndexBarToolBarHandler Method");

            if (ui_ncmdViewIndexBar.Checked)
            {
                ui_ncmdViewIndexBar.Checked = false;
                //ui_ndtIndexBar.Visible = false;
            }
            else
            {
                ui_ncmdViewIndexBar.Checked = true;
                //ui_ndtIndexBar.Visible = true;
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from IndexBarToolBarHandler Method");
        }

        /// <summary>
        /// Displays ParticipaintList window
        /// </summary>
        private void ParticipaintListMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into ParticipaintListMenuHandler Method");

            var objfrmParticipaintList = new frmParticipaintList();
            objfrmParticipaintList.MdiParent = this;
            objfrmParticipaintList.Show();

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from ParticipaintListMenuHandler Method");
        }

        /// <summary>
        /// Applys simple theme to the form and its components
        /// </summary>
        /// <param name="cmd">command information</param>
        private void SimpleMenuHandler(NCommand cmd)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into SimpleMenuHandler Method");

            SetTheme(PredefinedFrame.Simple, ColorScheme.Longhorn, cmd);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from SimpleMenuHandler Method");
        }

        /// <summary>
        /// Applys Inspirant theme to the form and its components
        /// </summary>
        /// <param name="cmd">command information</param>
        private void InspirantMenuHandler(NCommand cmd)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into InspirantMenuHandler Method");

            SetTheme(PredefinedFrame.Inspirat, ColorScheme.LunaBlue, cmd);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from InspirantMenuHandler Method");
        }

        /// <summary>
        /// Applys VistaSlate theme to the form and its components
        /// </summary>
        /// <param name="cmd">command information</param>
        private void VistaSlateMenuHandler(NCommand cmd)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into VistaSlateMenuHandler Method");

            SetTheme(PredefinedFrame.VistaSlate, ColorScheme.Gnome, cmd);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from VistaSlateMenuHandler Method");
        }

        /// <summary>
        /// Applys VistaPlus theme to the form and its components
        /// </summary>
        /// <param name="cmd">command information</param>
        private void VistaPlusMenuHandler(NCommand cmd)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into VistaPlusMenuHandler Method");

            SetTheme(PredefinedFrame.VistaPlus, ColorScheme.VistaPlus, cmd);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from VistaPlusMenuHandler Method");
        }

        /// <summary>
        /// Applys OpusAlpha theme to the form and its components
        /// </summary>
        /// <param name="cmd">command information</param>
        private void OpusAlphaMenuHandler(NCommand cmd)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into OpusAlphaMenuHandler Method");

            SetTheme(PredefinedFrame.OpusAlpha, ColorScheme.Arctic, cmd);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from OpusAlphaMenuHandler Method");
        }

        /// <summary>
        /// Applys Office2007Aqua theme to the form and its components
        /// </summary>
        /// <param name="cmd">command information</param>
        private void Office2007AquaMenuHandler(NCommand cmd)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into Office2007AquaMenuHandler Method");

            SetTheme(PredefinedFrame.Office2007Aqua, ColorScheme.Office2007Aqua, cmd);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from Office2007AquaMenuHandler Method");
        }

        /// <summary>
        /// Applys VistaRoyal theme to the form and its components
        /// </summary>
        /// <param name="cmd">command information</param>
        private void VistaRoyalMenuHandler(NCommand cmd)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into VistaRoyalMenuHandler Method");

            SetTheme(PredefinedFrame.VistaRoyal, ColorScheme.Longhorn, cmd);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from VistaRoyalMenuHandler Method");
        }

        /// <summary>
        /// Applys the given theme to the form
        /// </summary>
        /// <param name="frameName">Name of Theme to be applied</param>
        /// <param name="scheme">Name of colorscheme to be applied</param>
        /// <param name="cmd">command information</param>
        private void SetTheme(PredefinedFrame frameName, ColorScheme scheme, NCommand cmd)
        {
            Action A = () =>
                          {
                              //FileHandling.WriteDevelopmentLog("Main Form : Enter into SetTheme Method");

                              foreach (NCommand item in ui_ncmdViewThemes.Commands)
                              {
                                  item.Checked = false;
                              }
                              cmd.Checked = true;
                              NSkinManager.Instance.Enabled = false;
                              NUIManager.SetPredefinedFrame(frameName);
                              ClsPalsaUtility.SetFormProperties(this, scheme);
                              UpdateFrame();

                              //FileHandling.WriteDevelopmentLog("Main Form : Exit from SetTheme Method");
                          };
            if (InvokeRequired)
            {
                BeginInvoke(A);
            }
            else
            {
                A();
            }
        }

        /// <summary>
        /// Applys Vista theme to the form and its components
        /// </summary>
        /// <param name="cmd">command information</param>
        private void VistaMenuHandler(NCommand cmd)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into VistaMenuHandler Method");

            SetTheme("Vista", cmd);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from VistaMenuHandler Method");
        }

        /// <summary>
        /// Applys Orange theme to the form and its components
        /// </summary>
        /// <param name="cmd">command information</param>
        private void OrangeMenuHandler(NCommand cmd)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into OrangeMenuHandler Method");

            SetTheme("Orange", cmd);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from OrangeMenuHandler Method");
        }

        /// <summary>
        /// Applys OfficeBlue theme to the form and its components
        /// </summary>
        /// <param name="cmd">command information</param>
        private void OfficeBlueMenuHandler(NCommand cmd)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into OfficeBlueMenuHandler Method");

            SetTheme("Office 2007 Blue", cmd);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from OfficeBlueMenuHandler Method");
        }

        /// <summary>
        /// Applys OfficeBalck theme to the form and its components
        /// </summary>
        /// <param name="cmd">command information</param>
        private void OfficeBlackMenuHandler(NCommand cmd)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into OfficeBlackMenuHandler Method");

            SetTheme("Office 2007 Black", cmd);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from OfficeBlackMenuHandler Method");
        }

        /// <summary>
        /// Applys MacOS theme to the form and its components
        /// </summary>
        /// <param name="cmd">command information</param>
        private void MacOSMenuHandler(NCommand cmd)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into MacOSMenuHandler Method");

            SetTheme("MacOS", cmd);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from MacOSMenuHandler Method");
        }


        /// <summary>
        /// Applys the given theme to the form from XML file
        /// </summary>
        /// <param name="themeName">Name of the theme</param>
        /// <param name="cmd">Command information</param>

        private void SetTheme(string themeName, NCommand cmd)
        {
            //test
            //new Thread(delegate() { Method1(x,y); }).Start(); 
            //Action A = () =>
            //   {
            foreach (NCommand item in ui_ncmdViewThemes.Commands)
            {
                item.Checked = false;
            }
            cmd.Checked = true;
            var objNSkin = new NSkin();
            try
            {
                //objNSkin.Load("E:\\Edrive\\bin\\Skins\\LtechAqua.xml");
                objNSkin.Load(Application.StartupPath + "\\Skins\\" + themeName + ".xml");
                if (objNSkin != null)
                {
                    NSkinManager.Instance.Skin = objNSkin;
                    NSkinManager.Instance.Enabled = true;
                }

            }
            catch (Exception)
            {


            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from SetTheme Method");
            //   };
            //if (InvokeRequired)
            //{
            //    BeginInvoke(A);
            //}
            //else
            //{
            //    A();
            //}
        }

        /// <summary>
        /// Manages the checking and unchecking of theme submenus
        /// </summary>
        /// <param name="cmd">Command information</param>
        public void UncheckAllThemeMenus()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into ManageThemeMenusChecking Method");

            foreach (NCommand item in ui_ncmdViewThemes.Commands)
            {
                //if (item != cmd)
                item.Checked = false;
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from ManageThemeMenusChecking Method");
        }

        /// <summary>
        /// Sets the form language to hindi
        /// </summary>
        private void HindiMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into HindiMenuHandler Method");

            ui_ncmdLanguagesHindi.Checked = true;
            ManageCheckingOfLanguages(ui_ncmdLanguagesHindi);
            SetLanguage("hi");

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from HindiMenuHandler Method");
        }

        /// <summary>
        /// Sets the form language to English
        /// </summary>
        private void EnglishMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into EnglishMenuHandler Method");

            ui_ncmdLanguagesEnglish.Checked = true;
            ManageCheckingOfLanguages(ui_ncmdLanguagesEnglish);
            SetLanguage("en");

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from EnglishMenuHandler Method");
        }

        /// <summary>
        /// Manages the checking and unchecking of Languages submenus
        /// </summary>
        /// <param name="cmd"></param>
        public void ManageCheckingOfLanguages(NCommand cmd)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into ManageCheckingOfLanguages Method");

            foreach (NCommand item in ui_ncmdViewLanguages.Commands)
            {
                if (item != cmd)
                    item.Checked = false;
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from ManageCheckingOfLanguages Method");
        }

        /// <summary>
        /// 
        /// </summary>
        private void CancelAllOrdersToolBarHandler()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void CancelOrderToolBarHandler()
        {
            var objfrmOrderBook = ActiveMdiChild as frmOrderBook;
            if (objfrmOrderBook != null)
            {
                if (MessageBox.Show("Are you sure to cancel the selected order?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    objfrmOrderBook.uctlOrderBook1_HandleCancelOrder(null, null);
            }
            else
            {
                MessageBox.Show("Please Open OrderBook and select the Order which you want to cancel.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ModifyOrderToolBarHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into ModifyOrderToolBarHandler Method");
            var objfrmOrderBook = ActiveMdiChild as frmOrderBook;
            if (objfrmOrderBook != null)
            {
                objfrmOrderBook.uctlOrderBook1_HandleModifyOrderClick(null, null);
            }
            else
            {
                MessageBox.Show("Please Open OrderBook and select the Order which you want to modify.");
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from ModifyOrderToolBarHandler Method");
        }

        /// <summary>
        /// 
        /// </summary>
        private void PrintToolBarHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into PrintToolBarHandler Method");
            //Market Watch, Order Book, Net Position, TradeWindow, Logs
            var objfrmMarketWatch = ActiveMdiChild as frmMarketWatch;
            var objfrmOrderBook = ActiveMdiChild as frmOrderBook;
            var objfrmNetPosition = ActiveMdiChild as frmNetPosition;
            var objfrmTradeWindow = ActiveMdiChild as frmTradeWindow;
            var objfrmMessageLog = ActiveMdiChild as frmMessageLog;
            if (objfrmMarketWatch != null)
            {
                var objSaveFileDialog = new SaveFileDialog { DefaultExt = ".xls", Filter = "(*.xls)|*.xls" };
                DialogResult objDialogResult = objSaveFileDialog.ShowDialog();
                if (objDialogResult == DialogResult.OK)
                {
                    string filePath = objSaveFileDialog.FileName;
                    ClsCommonMethods.SaveGridDataInExcel(filePath, ((frmMarketWatch)objfrmMarketWatch).uctlMarketWatch1.ui_uctlGridMarketWatch);
                }
            }
            else if (objfrmOrderBook != null)
            {
                var objSaveFileDialog = new SaveFileDialog { DefaultExt = ".xls", Filter = "(*.xls)|*.xls" };
                DialogResult objDialogResult = objSaveFileDialog.ShowDialog();
                if (objDialogResult == DialogResult.OK)
                {
                    string filePath = objSaveFileDialog.FileName;
                    ClsCommonMethods.SaveGridDataInExcel(filePath, ((frmOrderBook)objfrmOrderBook).uctlOrderBook1.ui_uctlGridOrderBook);
                }
            }
            else if (objfrmNetPosition != null)
            {
                var objSaveFileDialog = new SaveFileDialog { DefaultExt = ".xls", Filter = "(*.xls)|*.xls" };
                DialogResult objDialogResult = objSaveFileDialog.ShowDialog();
                if (objDialogResult == DialogResult.OK)
                {
                    string filePath = objSaveFileDialog.FileName;
                    ClsCommonMethods.SaveGridDataInExcel(filePath, ((frmNetPosition)objfrmNetPosition).uctlNetPosition1.ui_uctlGridNetPosition);
                }
            }
            else if (objfrmTradeWindow != null)
            {
                var objSaveFileDialog = new SaveFileDialog { DefaultExt = ".xls", Filter = "(*.xls)|*.xls" };
                DialogResult objDialogResult = objSaveFileDialog.ShowDialog();
                if (objDialogResult == DialogResult.OK)
                {
                    string filePath = objSaveFileDialog.FileName;
                    ClsCommonMethods.SaveGridDataInExcel(filePath, ((frmTradeWindow)objfrmTradeWindow).uctlTradeWindow1.ui_uctlGridTradeWindow);
                }
            }
            else if (objfrmMessageLog != null)
            {
                var objSaveFileDialog = new SaveFileDialog { DefaultExt = ".xls", Filter = "(*.xls)|*.xls" };
                DialogResult objDialogResult = objSaveFileDialog.ShowDialog();
                if (objDialogResult == DialogResult.OK)
                {
                    string filePath = objSaveFileDialog.FileName;
                    ClsCommonMethods.SaveGridDataInExcel(filePath, ((frmMessageLog)objfrmMessageLog).uctlMessagLog1.ui_uctlGridMessageLog);
                }
            }
            else
            {
                ClsCommonMethods.ShowInformation("There is no printable windows open.");
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from PrintToolBarHandler Method");
        }

        /// <summary>
        /// Display dialog for taking online backup 
        /// </summary>
        private void OnlineBackToolBarHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into OnlineBackToolBarHandler Method");

            var objSaveFileDialog = new SaveFileDialog();

            objSaveFileDialog.DefaultExt = ".txt";
            objSaveFileDialog.Filter = "(*.txt)|*.txt";
            DialogResult objDialogResult = objSaveFileDialog.ShowDialog();

            if (objDialogResult == DialogResult.OK)
            {
                string filePath = objSaveFileDialog.FileName;
                var objFileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);

                var objStreamWriter = new StreamWriter(objFileStream);
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from OnlineBackToolBarHandler Method");
        }

        /// <summary>
        /// 
        /// </summary>
        private void LanguagesMenuHandeler()
        {
        }
        //frmChangePassword frmPassword;
        private void ChangePasswordMenuHandeler()
        {
            frmChangePassword frmPassword = new frmChangePassword();
            //frmPassword.OnOnChangePassword += new Action<string>(frmPassword_OnOnChangePassword);
            frmPassword.ShowDialog();
        }

        //void frmPassword_OnOnChangePassword(string obj)
        //{            
        //    //frmPassword.Close();
        //    //ClsCommonMethods.ShowInformation("Password changed successfuly.");
        //}
        /// <summary>
        /// Called when Ticker menu clicked
        /// </summary>
        private void TickerMenuHandeler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into TickerMenuHandeler Method");

            //if (ui_ncmdViewTicker.Checked == false)
            //{
            //    ui_ndtTicker.Visible = true;
            //    ui_ncmdViewTicker.Checked = true;
            //    if (Properties.Settings.Default.TickerPortfolio == null ||
            //        Properties.Settings.Default.TickerPortfolio == "---SELECT---")
            //    {
            //        string str = Properties.Settings.Default.LastTickerPortfolio;
            //        //AddDataToTicker(Properties.Settings.Default.LastTickerPortfolio);
            //    }
            //    else
            //    {
            //        string str = Properties.Settings.Default.LastTickerPortfolio;
            //        string str1 = Properties.Settings.Default.TickerPortfolio;
            //        if (Properties.Settings.Default.TickerPortfolio == "0" ||
            //            Properties.Settings.Default.TickerPortfolio == null ||
            //            Properties.Settings.Default.TickerPortfolio == "---SELECT---")
            //        {
            //            AddDataToTicker(Properties.Settings.Default.LastTickerPortfolio);
            //        }
            //        else
            //        {
            //            AddDataToTicker(Properties.Settings.Default.TickerPortfolio);
            //        }
            //        //AddDataToTicker(Properties.Settings.Default.TickerPortfolio);
            //    }

            //}
            //else
            //{
            //    ui_ndtTicker.Visible = false;
            //    ui_ncmdViewTicker.Checked = false;
            //}
            AddDataToTicker("LTech India Trader");
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from TickerMenuHandeler Method");
        }

        /// <summary>
        /// Called when Help menu clicked
        /// </summary>
        private void HelpMenuHandler()
        {
        }

        /// <summary>
        /// Called when Window menu clicked
        /// </summary>
        private void WindowMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into WindowMenuHandler Method");

            if (MdiChildren.Count() == 0)
            {
                foreach (NCommand item in ui_nmnuWindows.Commands)
                {
                    item.Enabled = false;
                }
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from WindowMenuHandler Method");
        }

        /// <summary>
        /// Called when TileVertically menu clicked
        /// </summary>
        private void TileVerticallyMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into TileVerticallyMenuHandler Method");

            LayoutMdi(MdiLayout.TileVertical);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from TileVerticallyMenuHandler Method");
        }

        /// <summary>
        /// Called when TileHorizontally menu clicked
        /// </summary>
        private void TileHorizontallyMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into TileHorizontallyMenuHandler Method");

            LayoutMdi(MdiLayout.TileHorizontal);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from TileHorizontallyMenuHandler Method");
        }

        /// <summary>
        /// Called when Cascade menu clicked
        /// </summary>
        private void CascadeMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into CascadeMenuHandler Method");

            LayoutMdi(MdiLayout.Cascade);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from CascadeMenuHandler Method");
        }

        /// <summary>
        /// Called when CloseAll menu clicked
        /// </summary>
        private void CloseAllMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into CloseAllMenuHandler Method");

            foreach (frmBase frm in MdiChildren)
            {
                frm.Close();
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from CloseAllMenuHandler Method");
        }

        /// <summary>
        /// Called when Close menu clicked
        /// </summary>
        private void CloseMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into CloseMenuHandler Method");

            if (ActiveMdiChild != null)
                ActiveMdiChild.Close();

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from CloseMenuHandler Method");
        }

        /// <summary>
        /// Called when NewWindow menu clicked
        /// </summary>
        private void NewWindowMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into NewWindowMenuHandler Method");

            var objAlertTestForm = new AlertTestForm();
            objAlertTestForm.MdiParent = this;
            objAlertTestForm.Show();

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from NewWindowMenuHandler Method");
        }

        /// <summary>
        /// Called when Preferences menu clicked
        /// </summary>
        private void PreferencesMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into PreferencesMenuHandler Method");
            frmPreferences.GetInstance(_hotKeySettingsHashTable, Portfolio).OnHotKeyChanged += new Action<Hashtable>(FrmMain_OnHotKeyChanged);
            frmPreferences.GetInstance(_hotKeySettingsHashTable, Portfolio).OnDocumentSettingsChanged += new Action(FrmMain_OnDocumentSettingsChanged);
            if (frmPreferences.GetInstance(_hotKeySettingsHashTable, Portfolio).ShowDialog() == DialogResult.Yes)
            {
                _hotKeySettingsHashTable = frmPreferences._hotKeySettingsHashTable;
                ApplyHotkeys();
                SetContextMenuItemHotKeys();
                SetTickerValues(Properties.Settings.Default.TickerSpeed);
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from PreferencesMenuHandler Method");
        }

        void FrmMain_OnHotKeyChanged(Hashtable obj)
        {
            _hotKeySettingsHashTable = obj;
        }

        void FrmMain_OnDocumentSettingsChanged()
        {

        }

        /// <summary>
        /// Called when PortFolio menu clicked
        /// </summary>
        private void PortfolioMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into PortfolioMenuHandler Method");

            using (var objfrmPortfolio = new frmPortfolio(Portfolio, string.Empty, _objuctlLogin.UserCode))
            {
                objfrmPortfolio.OnSavePortfolio += objfrmPortfolio_OnSavePortfolio;
                DialogResult dgR = objfrmPortfolio.ShowDialog();
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from PortfolioMenuHandler Method");
        }

        private void objfrmPortfolio_OnSavePortfolio(object portfolio)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into objfrmPortfolio_OnSavePortfolio Method");
                        
            //PALSA.Cls.ClsGlobal.LatestPortfolio = portfolio;
            _ctlMQ.Portfolios = portfolio;
            _ctlMQ.uctlForex1.Portfolios = portfolio;
            _ctlMW.ObjPortfolio = portfolio;
            _ctlMW.uctlMarketWatch1.Portfolios = portfolio;
            _ctlRadar.Portfolios = portfolio;
            foreach (Form frm in MdiChildren)
            {
                if (frm is frmMarketWatch)
                {
                    ((frmMarketWatch)frm).ObjPortfolio = INSTANCE.GetPortfolios(_objuctlLogin.UserCode);
                    ((frmMarketWatch)frm).uctlMarketWatch1.Portfolios = ((frmMarketWatch)frm).ObjPortfolio;
                }
            }
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from objfrmPortfolio_OnSavePortfolio Method");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        private void objuctlPortfolio_OnCancelClick(object arg1, EventArgs arg2)
        {
            //ui_ndmPALSA.DocumentManager.RemoveDocument(ui_ndmPALSA.DocumentManager.ActiveDocument);
        }


        /// <summary>
        /// Called when LockWorkStation menu clicked
        /// </summary>
        private void LockWorkStationMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into LockWorkStationMenuHandler Method");
            if (LoggedInSuccess)
            {
                int padding = 10;

                frmLockWorkStation.INSTANCE.StartPosition = FormStartPosition.Manual;
                int yLocation = Screen.PrimaryScreen.WorkingArea.Height -
                                frmLockWorkStation.INSTANCE.Size.Height;
                int xLocation = Screen.PrimaryScreen.WorkingArea.Width -
                                frmLockWorkStation.INSTANCE.Size.Width;
                frmLockWorkStation.INSTANCE.Location = new Point(Screen.PrimaryScreen.WorkingArea.X,
                                                                 yLocation +
                                                                 Screen.PrimaryScreen.WorkingArea.Y -
                                                                 padding);
                frmLockWorkStation.INSTANCE.Width = Screen.PrimaryScreen.WorkingArea.Width;
                frmLockWorkStation.INSTANCE.ui_ntxtPassword.Text = null;

                if (frmLockWorkStation.INSTANCE.Visible != true)
                {
                    frmLockWorkStation.INSTANCE.ShowDialog();
                }
            }
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from LockWorkStationMenuHandler Method");
        }

        private void ScannerMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into ScannerMenuHandler Method");
            if (Properties.Settings.Default.ScannerInDoc)
            {
                CreateNuiDocument(_ctlScanner, "Scanner");
            }
            else
            {
                if (Properties.Settings.Default.ScannerZone == 1)
                {
                    nDockPanelHost1.AddChild(this.Scanner, Properties.Settings.Default.ScannerIndex);
                }
                else
                {
                    nDockPanelHost2.AddChild(this.Scanner, Properties.Settings.Default.ScannerIndex);
                }
                AddToPanel(_ctlScanner);
            }

            //if (_ctlScanner != null)//Kul
            //{
            //    AddToPanel(_ctlScanner);
            //}
            //var objfrmOrderBook = new frmOrderBook(_profiles, string.Empty, _shortcutKeyFilter);
            //var orderStatus = new List<string>();
            //orderStatus.Clear();
            //orderStatus.Add("All");
            //foreach (string x in PALSA.Cls.ClsGlobal.DDOrderStatus.Keys.ToArray())
            //{
            //    string c = x.Replace('_', ' ').ToLower();
            //    c = ClsPalsaUtility.UppercaseWords(c);
            //    orderStatus.Add(c);
            //}
            //objfrmOrderBook.uctlOrderBook1.CreateContextMenu(orderStatus.ToArray());
            //SetHotkeyHashTable(CommandIDS.CANCEL_SELECTED_ORDER, objfrmOrderBook.uctlOrderBook1.ContextMenuItems[3]);

            //objfrmOrderBook.MdiParent = this;
            //objfrmOrderBook.Show();

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from ScannerMenuHandler Method");
        }

        /// <summary>
        /// Called when OrderBook menu clicked
        /// </summary>
        private void OrderBookMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into OrderBookMenuHandler Method");
            if (Properties.Settings.Default.OrderHistoryInDoc)
            {
                CreateNuiDocument(_ctlOB, "Order Book");
            }
            else
            {
                if (Properties.Settings.Default.ScannerZone == 1)
                {
                    nDockPanelHost1.AddChild(this.OrderBook, Properties.Settings.Default.OrderHistoryIndex);
                }
                else
                {
                    nDockPanelHost2.AddChild(this.OrderBook, Properties.Settings.Default.OrderHistoryIndex);
                }
                AddToPanel(_ctlOB);
            }
            //var objfrmOrderBook = new frmOrderBook(_profiles, string.Empty, _shortcutKeyFilter);
            //var orderStatus = new List<string>();
            //orderStatus.Clear();
            //orderStatus.Add("All");
            //foreach (string x in PALSA.Cls.ClsGlobal.DDOrderStatus.Keys.ToArray())
            //{
            //    string c = x.Replace('_', ' ').ToLower();
            //    c = ClsPalsaUtility.UppercaseWords(c);
            //    orderStatus.Add(c);
            //}
            //objfrmOrderBook.uctlOrderBook1.CreateContextMenu(orderStatus.ToArray());
            //SetHotkeyHashTable(CommandIDS.CANCEL_SELECTED_ORDER, objfrmOrderBook.uctlOrderBook1.ContextMenuItems[3]);

            //objfrmOrderBook.MdiParent = this;
            //objfrmOrderBook.Show();

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from OrderBookMenuHandler Method");
        }

        /// <summary>
        /// Called when PlaceSell menu clicked
        /// </summary>
        private void PlaceSellOrderMenuHandler()
        {
            ////FileHandling.WriteDevelopmentLog("Main Form : Enter into PlaceSellOrderMenuHandler Method");

            ////DisplayOrderEntryDialog("SELL", Properties.Settings.Default.SellOrderColor, "Sell Order Entry");
            ////frmOrderEntry.INSTANCE.Formkey = CommandIDS.PLACE_SELL_ORDER.ToString();
            //var objfrmMarketWatch = ActiveMdiChild as frmMarketWatch;
            //if (objfrmMarketWatch != null)
            //{
            //    objfrmMarketWatch.OnSellOrderClick(null, null);
            //}
            //else
            //{
            //    DisplayOrderEntryDialog("SELL", Properties.Settings.Default.SellOrderColor, "Sell Order Entry");
            //    frmOrderEntry.INSTANCE.Formkey = CommandIDS.PLACE_SELL_ORDER.ToString();
            //}
            ////FileHandling.WriteDevelopmentLog("Main Form : Exit from PlaceSellOrderMenuHandler Method");
        }

        private void PlaceOrderMenuHandler()
        {
            if (_ctlMW != null)
            {
                _ctlMW.OnPlaceOrderClick(null, null);
            }
        }
        /// <summary>
        /// Called when PlaceBuy menu clicked
        /// </summary>
        private void PlaceBuyOrderMenuHandler()
        {
            ////FileHandling.WriteDevelopmentLog("Main Form : Enter into PlaceBuyOrderMenuHandler Method");

            ////DisplayOrderEntryDialog("BUY", Properties.Settings.Default.BuyOrderColor, "Buy Order Entry");
            ////frmOrderEntry.INSTANCE.Formkey = CommandIDS.PLACE_BUY_ORDER.ToString();
            ////frmOrderEntry.INSTANCE.ConfirmationMessage = "Submit Market Order?";
            //var objfrmMarketWatch = ActiveMdiChild as frmMarketWatch;
            //if (objfrmMarketWatch != null)
            //{
            //    objfrmMarketWatch.OnBuyOrderClick(null, null);
            //}
            //else
            //{
            //    DisplayOrderEntryDialog("BUY", Properties.Settings.Default.BuyOrderColor, "Buy Order Entry");
            //    frmOrderEntry.INSTANCE.Formkey = CommandIDS.PLACE_BUY_ORDER.ToString();
            //    frmOrderEntry.INSTANCE.ConfirmationMessage = "Submit Market Order?";
            //}
            ////FileHandling.WriteDevelopmentLog("Main Form : Exit from PlaceBuyOrderMenuHandler Method");
        }

        private void DisplayOrderEntryDialog(string title, Color color, string formTitle)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into DisplayOrderEntryDialog Method");

            if (frmOrderEntry.INSTANCE.Visible)
            {
                frmOrderEntry.INSTANCE.Close();
            }
            frmOrderEntry.INSTANCE = new frmOrderEntry();
            //frmOrderEntry.INSTANCE.FormText = formTitle;
            //frmOrderEntry.INSTANCE.uctlOrderEntry1.Caption = title;
            //frmOrderEntry.INSTANCE.uctlOrderEntry1.ui_npnlOrderEntry.BackColor = color;
            //frmOrderEntry.INSTANCE.MdiParent = this;
            frmOrderEntry.INSTANCE.Show();

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from DisplayOrderEntryDialog Method");
        }

        /// <summary>
        /// Called when TopGainersLosers menu clicked
        /// </summary>
        private void TopGainersLosersMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into TopGainersLosersMenuHandler Method");

            var objfrmTopGainersLosers = new frmTopGainersLosers();
            objfrmTopGainersLosers.MdiParent = this;
            objfrmTopGainersLosers.Show();

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from TopGainersLosersMenuHandler Method");
        }

        /// <summary>
        /// Called when MarketStatus menu clicked
        /// </summary>
        private void MarketStatusMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into MarketStatusMenuHandler Method");

            var objfrmMarketStatus = new frmMarketStatus();
            objfrmMarketStatus.MdiParent = this;
            objfrmMarketStatus.Show();

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from MarketStatusMenuHandler Method");
        }

        /// <summary>
        /// Called when QuoteSnap menu clicked
        /// </summary>
        private void QuoteSnapMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into QuoteSnapMenuHandler Method");

            if (frmSnapQuote.INSTANCE.IsDisposed)
            {
                frmSnapQuote.INSTANCE = new frmSnapQuote();
            }
            var objfrmBase = ((frmBase)ActiveMdiChild);

            frmSnapQuote.INSTANCE.ShowInTaskbar = false;
            frmSnapQuote.INSTANCE.MdiParent = this;
            frmSnapQuote.INSTANCE.Show();
            if (objfrmBase != null && objfrmBase.Formkey.Contains("MARKET_WATCH"))
            {
                if (((frmMarketWatch)objfrmBase).uctlMarketWatch1.ui_uctlGridMarketWatch.Rows.Count != 0)
                {
                    DataGridViewRow row =
                        ((frmMarketWatch)objfrmBase).uctlMarketWatch1.ui_uctlGridMarketWatch.SelectedRows[0];
                    frmSnapQuote.INSTANCE.SetMarketWatchValues(row);
                }
            }
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from QuoteSnapMenuHandler Method");
        }

        /// <summary>
        /// Called when MarketPicture menu clicked
        /// </summary>
        private void MarketPictureMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into MarketPictureMenuHandler Method");
            if (frmMarketPicture.Count < 4)
            {
                var objfrmMarketWatch = ActiveMdiChild as frmMarketWatch;
                if (objfrmMarketWatch != null)
                {
                    objfrmMarketWatch.OnMarketPictureClick(null, null);
                }
                else
                {
                    bool isMarketWatchFound = false;
                    foreach (Form x in MdiChildren)
                    {
                        if (x is frmMarketWatch)
                        {
                            ((frmMarketWatch)x).OnMarketPictureClick(null, null);
                            isMarketWatchFound = true;
                            break;
                        }
                    }
                    if (isMarketWatchFound == false)
                    {
                        ClsCommonMethods.ShowInformation("Please open the MarketWatch and select the symbol for which you want MarketPicture.");
                        //var objMarketPicture = new frmMarketPicture { MdiParent = this };
                        //objMarketPicture.Show();
                    }
                }
            }
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from MarketPictureMenuHandler Method");
        }

        /// <summary>
        /// Called when MarketWatch menu clicked
        /// </summary>
        private void MarketWatchMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into MarketWatchMenuHandler Method");
            _ctlMW = new ctlMarketWatch(Portfolio);
            _ctlMW.uctlMarketWatch1.Portfolios = Portfolio;
            _ctlMW.ShortcutKeyBOE = _shortcutKeyOrderEntry;
            //_ctlMW.ShortcutKeySOE = _shortcutKeySellOrderEntry;
            _ctlMW.OnScriptPortfolioApplyClick -= new Action<string>(_ctlMW_OnScriptPortfolioApplyClick);
            _ctlMW.OnScriptPortfolioApplyClick += new Action<string>(_ctlMW_OnScriptPortfolioApplyClick);
            _ctlMW.ShortcutKeyMarketPicture = _shortcutKeyMarketPicture;
            _ctlMW.OnSymbolChartClick -= new Action<DataGridViewRow>(_ctlMW_OnSymbolChartClick);
            _ctlMW.OnSymbolChartClick += new Action<DataGridViewRow>(_ctlMW_OnSymbolChartClick);

            if (Properties.Settings.Default.MarketWatchInDoc)
            {
                CreateNuiDocument(_ctlMW, "Market Watch");
            }
            else
            {
                if (Properties.Settings.Default.MarketWatchZone == 1)
                {
                    nDockPanelHost1.AddChild(this.SymbolPanel, Properties.Settings.Default.MarketWatchIndex);
                }
                else
                {
                    nDockPanelHost2.AddChild(this.SymbolPanel, Properties.Settings.Default.MarketWatchIndex);
                }
                AddToPanel(_ctlMW);
            }

            //if (frmMarketWatch.Count < 4)
            //{
            //    var objMarketWatch = new frmMarketWatch(_portfolio, _profiles, string.Empty, string.Empty, _objuctlLogin.UserCode);
            //    objMarketWatch.ShortcutKeyBOE = _shortcutKeyBuyOrderEntry;
            //    objMarketWatch.ShortcutKeySOE = _shortcutKeySellOrderEntry;
            //    objMarketWatch.ShortcutKeyMarketPicture = _shortcutKeyMarketPicture;
            //    objMarketWatch.MdiParent = this;
            //    //objMarketWatch.OnNewChart += new Action(objMarketWatch_OnNewChart);
            //    objMarketWatch.Show();
            //}

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from MarketWatchMenuHandler Method");
        }

        void _ctlMW_OnSymbolChartClick(DataGridViewRow obj)
        {
            string symbol = obj.Cells["ClmContractName"].Value.ToString();

            //ctlNewChart ctlChart = new ctlNewChart();
            //ctlChart.InitChartData(obj);

            if (!IsDocumentExists(symbol + "-1 MINUTE" + "_" + "Chart"))
            {
                ctlNewChart ctlChart = new ctlNewChart();
                ctlChart.InitChartData(obj);

                //WPFChartControl ctlChart = new WPFChartControl(symbol, this);
                CreateNuiDocument(ctlChart, symbol + "-1 MINUTE" + "_" + "Chart");
            }
        }


        public void AddToPanel(Control childControl)
        {
            childControl.Dock = DockStyle.Fill;
            if (childControl is ctlMarketWatch)
            {
                if (SymbolPanel.DockState == Nevron.UI.WinForm.Docking.DockState.Hidden)
                {
                    INDockZone target = Openpositions.ParentZone != null ? Openpositions.ParentZone : (Scanner.ParentZone != null ? Scanner.ParentZone : (MailBox.ParentZone != null ? MailBox.ParentZone : (OrderBook.ParentZone != null ? OrderBook.ParentZone : null)));
                    if (target == null)
                    {
                        this.SymbolPanel.PerformDock(this.m_DockManager.RootContainer.RootZone.Children[0].RootZone.Children[0].RootZone.Children[0].RootZone, DockStyle.Left);
                    }
                    else
                    {
                        SymbolPanel.PerformDock(target);
                    }
                }
                else
                {
                    SymbolPanel.Activate();
                }
                if (SymbolPanel.Controls.Contains(childControl))
                {
                    SymbolPanel.Controls.Clear();
                }
                SymbolPanel.Controls.Add(childControl);
            }
            else if (childControl is ctlMarketQuote)
            {
                if (QuotePanel.DockState == Nevron.UI.WinForm.Docking.DockState.Hidden)
                {
                    INDockZone target = Openpositions.ParentZone != null ? Openpositions.ParentZone : (Scanner.ParentZone != null ? Scanner.ParentZone : (MailBox.ParentZone != null ? MailBox.ParentZone : (OrderBook.ParentZone != null ? OrderBook.ParentZone : null)));
                    if (target == null)
                    {
                        this.QuotePanel.PerformDock(this.m_DockManager.RootContainer.RootZone.Children[0].RootZone.Children[0].RootZone.Children[0].RootZone, DockStyle.Left);
                    }
                    else
                    {
                        QuotePanel.PerformDock(target);
                    }
                }
                else
                {
                    QuotePanel.Activate();
                }
                if (QuotePanel.Controls.Contains(childControl))
                {
                    QuotePanel.Controls.Clear();
                }
                QuotePanel.Controls.Add(childControl);
            }
            else if (childControl is ctlHistory)
            {
                if (History.DockState == Nevron.UI.WinForm.Docking.DockState.Hidden)
                {
                    INDockZone target = Openpositions.ParentZone != null ? Openpositions.ParentZone : (Scanner.ParentZone != null ? Scanner.ParentZone : (MailBox.ParentZone != null ? MailBox.ParentZone : (OrderBook.ParentZone != null ? OrderBook.ParentZone : null)));
                    if (target == null)
                    {
                        this.History.PerformDock(this.m_DockManager.RootContainer.RootZone.Children[0].RootZone.Children[0].RootZone.Children[0].RootZone, DockStyle.Bottom);
                    }
                    else
                    {
                        History.PerformDock(target);
                    }

                }
                else if (History.DockState == Nevron.UI.WinForm.Docking.DockState.Docked)
                {
                    History.Activate();
                }
                if (History.Controls.Contains(childControl))
                {
                    History.Controls.Clear();
                }
                History.Controls.Add(childControl);
            }
            else if (childControl is ctlNetPosition)
            {
                if (Openpositions.DockState == Nevron.UI.WinForm.Docking.DockState.Hidden)
                {
                    INDockZone target = History.ParentZone != null ? History.ParentZone : (Scanner.ParentZone != null ? Scanner.ParentZone : (MailBox.ParentZone != null ? MailBox.ParentZone : (OrderBook.ParentZone != null ? OrderBook.ParentZone : null)));
                    if (target == null)
                    {
                        this.Openpositions.PerformDock(this.m_DockManager.RootContainer.RootZone.Children[0].RootZone.Children[0].RootZone.Children[0].RootZone, DockStyle.Bottom);
                    }
                    else
                    {
                        Openpositions.PerformDock(target);
                    }
                }
                else if (Openpositions.DockState == Nevron.UI.WinForm.Docking.DockState.Docked)
                {
                    Openpositions.Activate();
                }
                if (Openpositions.Controls.Contains(childControl))
                {
                    Openpositions.Controls.Clear();
                }
                Openpositions.Controls.Add(childControl);
            }
            else if (childControl is ctlOrders)
            {
                if (OrderBook.DockState == Nevron.UI.WinForm.Docking.DockState.Hidden)
                {
                    INDockZone target = History.ParentZone != null ? History.ParentZone : (Scanner.ParentZone != null ? Scanner.ParentZone : (MailBox.ParentZone != null ? MailBox.ParentZone : (Openpositions.ParentZone != null ? Openpositions.ParentZone : null)));
                    if (target == null)
                    {
                        this.OrderBook.PerformDock(this.m_DockManager.RootContainer.RootZone.Children[0].RootZone.Children[0].RootZone.Children[0].RootZone, DockStyle.Bottom);
                    }
                    else
                    {
                        OrderBook.PerformDock(target);
                    }
                }
                else
                {
                    OrderBook.Activate();
                }
                if (OrderBook.Controls.Contains(childControl))
                {
                    OrderBook.Controls.Clear();
                }
                OrderBook.Controls.Add(childControl);
            }
            else if (childControl is ctlScanner)
            {
                if (Scanner.DockState == Nevron.UI.WinForm.Docking.DockState.Hidden)
                {
                    INDockZone target = History.ParentZone != null ? History.ParentZone : (Scanner.ParentZone != null ? Scanner.ParentZone : (MailBox.ParentZone != null ? MailBox.ParentZone : (Openpositions.ParentZone != null ? Openpositions.ParentZone : null)));
                    if (target == null)
                    {
                        this.Scanner.PerformDock(this.m_DockManager.RootContainer.RootZone.Children[0].RootZone.Children[0].RootZone.Children[0].RootZone, DockStyle.Bottom);
                    }
                    else
                    {
                        Scanner.PerformDock(target);
                    }
                }
                else
                {
                    Scanner.Activate();
                }
                if (Scanner.Controls.Contains(childControl))
                {
                    Scanner.Controls.Clear();
                }
                Scanner.Controls.Add(childControl);
            }
            else if (childControl is uctlAlert)
            {
                if (Alerts.DockState == Nevron.UI.WinForm.Docking.DockState.Hidden)
                {
                    INDockZone target = History.ParentZone != null ? History.ParentZone : (Scanner.ParentZone != null ? Scanner.ParentZone : (MailBox.ParentZone != null ? MailBox.ParentZone : (OrderBook.ParentZone != null ? OrderBook.ParentZone : null)));
                    if (target == null)
                    {
                        this.Alerts.PerformDock(this.m_DockManager.RootContainer.RootZone.Children[0].RootZone.Children[0].RootZone.Children[0].RootZone, DockStyle.Bottom);
                    }
                    else
                    {
                        Alerts.PerformDock(target);
                    }
                }
                else
                {
                    Alerts.Activate();
                }
                if (Alerts.Controls.Contains(childControl))
                {
                    Alerts.Controls.Clear();
                }
                Alerts.Controls.Add(childControl);
            }
            else if (childControl is uctlMailGrid)
            {
                if (MailBox.DockState == Nevron.UI.WinForm.Docking.DockState.Hidden)
                {
                    INDockZone target = History.ParentZone != null ? History.ParentZone : (Scanner.ParentZone != null ? Scanner.ParentZone : (MailBox.ParentZone != null ? MailBox.ParentZone : (OrderBook.ParentZone != null ? OrderBook.ParentZone : null)));
                    if (target == null)
                    {
                        this.MailBox.PerformDock(this.m_DockManager.RootContainer.RootZone.Children[0].RootZone.Children[0].RootZone.Children[0].RootZone, DockStyle.Bottom);
                    }
                    else
                    {
                        MailBox.PerformDock(target);
                    }
                }
                else
                {
                    MailBox.Activate();
                }
                if (MailBox.Controls.Contains(childControl))
                {
                    MailBox.Controls.Clear();
                }
                MailBox.Controls.Add(childControl);
            }
            else if (childControl is ctlRadarMain)
            {
                if (Radar.DockState == Nevron.UI.WinForm.Docking.DockState.Hidden)
                {
                    INDockZone target = History.ParentZone != null ? History.ParentZone : (Scanner.ParentZone != null ? Scanner.ParentZone : (MailBox.ParentZone != null ? MailBox.ParentZone : (OrderBook.ParentZone != null ? OrderBook.ParentZone : null)));
                    if (target == null)
                    {
                        this.Radar.PerformDock(this.m_DockManager.RootContainer.RootZone.Children[0].RootZone.Children[0].RootZone.Children[0].RootZone, DockStyle.Bottom);
                    }
                    else
                    {
                        Radar.PerformDock(target);
                    }
                }
                else
                {
                    Radar.Activate();
                }
                if (Radar.Controls.Contains(childControl))
                {
                    Radar.Controls.Clear();
                }
                Radar.Controls.Add(childControl);
            }
            else if (childControl is ctlAccountsToTrade)
            {
                if (Accounts.DockState == Nevron.UI.WinForm.Docking.DockState.Hidden)
                {
                    INDockZone target = History.ParentZone != null ? History.ParentZone : (Scanner.ParentZone != null ? Scanner.ParentZone : (MailBox.ParentZone != null ? MailBox.ParentZone : (OrderBook.ParentZone != null ? OrderBook.ParentZone : null)));
                    if (target == null)
                    {
                        this.Accounts.PerformDock(this.m_DockManager.RootContainer.RootZone.Children[0].RootZone.Children[0].RootZone.Children[0].RootZone, DockStyle.Bottom);
                    }
                    else
                    {
                        Accounts.PerformDock(target);
                    }
                }
                else
                {
                    Accounts.Activate();
                }
                if (Accounts.Controls.Contains(childControl))
                {
                    Accounts.Controls.Clear();
                }
                Accounts.Controls.Add(childControl);
            }
            else if (childControl is ctlPendingOrders)
            {
                if (PendingOrders.DockState == Nevron.UI.WinForm.Docking.DockState.Hidden)
                {
                    INDockZone target = History.ParentZone != null ? History.ParentZone : (Scanner.ParentZone != null ? Scanner.ParentZone : (MailBox.ParentZone != null ? MailBox.ParentZone : (OrderBook.ParentZone != null ? OrderBook.ParentZone : null)));
                    if (target == null)
                    {
                        this.PendingOrders.PerformDock(this.m_DockManager.RootContainer.RootZone.Children[0].RootZone.Children[0].RootZone.Children[0].RootZone, DockStyle.Bottom);
                    }
                    else
                    {
                        PendingOrders.PerformDock(target);
                    }
                }
                else
                {
                    PendingOrders.Activate();
                }
                if (PendingOrders.Controls.Contains(childControl))
                {
                    PendingOrders.Controls.Clear();
                }
                PendingOrders.Controls.Add(childControl);
            }
        }
        //public void AddToSymbolPanel(Control childControl)
        //{
        //    childControl.Dock = DockStyle.Fill;
        //    SymbolPanel.Controls.Add(childControl);
        //}
        public void AddDocument(string documentName, Control childControl)
        {
            AddDocument(documentName, Guid.NewGuid(), childControl);
        }

        public void AddDocument(string documentName, Guid documentId, Control childControl)
        {
            //if (!IsDocumentExists(documentName))
            //{
            childControl.Dock = DockStyle.Fill;
            NUIDocument document = new NUIDocument(documentName, -1, childControl);
            document.ID = documentId;
            document.Key = documentName;

            if (documentName.Contains("Chart"))//For Charts
            {
                string sym = documentName.Split('_')[0];
                document.Text = sym;
                ToolbarChart.Enabled = true;
            }
            else if (documentName.Contains("Depth"))//For Market Depth
            {
                document.Key = documentName;
                document.Text = "Market Depth";
            }
            else if (documentName.Contains("Matrix"))//For Matrix Depth
            {
                document.Key = documentName;
                document.Text = "Matrix";
            }

            m_DockManager.DocumentManager.AddDocument(document);
            //}
            //else
            //{
            //    m_DockManager.DocumentManager.DocumentView.Activate();
            //   // m_DockManager.DocumentManager.DocumentView.ContextMenuEnabled = true;
            //}
        }

        private bool IsDocumentExists(string documentName)
        {
            bool x = false;
            NUIDocument doc = m_DockManager.DocumentManager.GetDocumentByText(documentName);
            if (doc != null)
            {
                NUIDocument doc2 = m_DockManager.DocumentManager.GetDocumentById(doc.ID);
                x = true;
            }
            return x;
        }

        private void objMarketWatch_OnNewChart()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into objMarketWatch_OnNewChart Method");

            ui_mnuChart3DStyle.Checked = false;
            ui_mnuChartsTrackCursor.Checked = false;
            ui_mnuChartsVolume.Checked = false;
            ui_mnuChartsGrid.Checked = true;

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from objMarketWatch_OnNewChart Method");
        }

        /// <summary>
        /// Create new documents with specfied control and key name
        /// </summary>
        /// <param name="uctl">The client value of the document</param>
        /// <param name="keyValue">Key value of the document</param>
        private void CreateNuiDocument(Control uctl, string keyValue)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into CreateNuiDocument Method");

            //uctl.Dock = DockStyle.Fill;
            //var doc = new NUIDocument("ctrl"); //, 0, objuctlMarketWatch);           
            //doc.Key = keyValue;
            //doc.PrefferedBounds = new Rectangle(uctl.Location.X, uctl.Location.Y, uctl.Width + 10, uctl.Height + 35);
            //doc.Client = uctl;
            //ui_ndmPALSA.DocumentManager.AddDocument(doc);
            AddDocument(keyValue, uctl);
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from CreateNuiDocument Method");
        }

        /// <summary>
        /// Called when FullScreen menu clicked
        /// </summary>
        private void FullScreenMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into FullScreenMenuHandler Method");

            if (ui_ncmdViewFullScreen.Checked == false)
            {
                WindowState = FormWindowState.Normal;
                FormBorderStyle = FormBorderStyle.None;
                Bounds = Screen.PrimaryScreen.Bounds;
                Movable = false;
                Sizable = false;
                MaximizeBox = false;
                MinimizeBox = false;
                ui_ncmdViewFullScreen.Checked = true;
                BringToFront();
                TopMost = true;
            }
            else
            {
                WindowState = FormWindowState.Maximized;
                FormBorderStyle = FormBorderStyle.Sizable;
                Movable = true;
                Sizable = true;
                MaximizeBox = true;
                MinimizeBox = true;
                ui_ncmdViewFullScreen.Checked = false;
                TopMost = false;
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from FullScreenMenuHandler Method");
        }

        /// <summary>
        /// Called when IndicesView menu clicked
        /// </summary>
        private void IndicesViewMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into IndicesViewMenuHandler Method");

            if (frmIndexView.INSTANCE.IsDisposed)
            {
                frmIndexView.INSTANCE = new frmIndexView();
            }
            int xLocation = Screen.PrimaryScreen.WorkingArea.Width -
                            frmIndexView.INSTANCE.Width;
            frmIndexView.INSTANCE.Location = new Point(
                Screen.PrimaryScreen.WorkingArea.X + xLocation, Screen.PrimaryScreen.WorkingArea.Y + 21);
            frmIndexView.INSTANCE.ShowInTaskbar = false;
            frmIndexView.INSTANCE.TopMost = true;
            frmIndexView.INSTANCE.Show();

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from IndicesViewMenuHandler Method");
        }


        /// <summary>
        /// Called when AdminMessage menu clicked
        /// </summary>
        private void AdminMessageBarMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into AdminMessageBarMenuHandler Method");

            if (frmAdminMessageLog.INSTANCE.IsDisposed)
            {
                frmAdminMessageLog.INSTANCE = new frmAdminMessageLog();
            }
            frmAdminMessageLog.INSTANCE.ShowInTaskbar = false;
            frmAdminMessageLog.INSTANCE.MdiParent = this;
            frmAdminMessageLog.INSTANCE.Show();
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from AdminMessageBarMenuHandler Method");
        }

        /// <summary>
        /// Called when BottomStatus menu clicked
        /// </summary>
        private void BottomStatusBarMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into BottomStatusBarMenuHandler Method");

            if (nmnuCmdViewBottomStatusBar.Checked)
            {
                nmnuCmdViewBottomStatusBar.Checked = false;
                nuiPanel4.Visible = false;
                nuiPnlStatusBars.Height -= nuiPanel4.Size.Height;
            }
            else
            {
                nmnuCmdViewBottomStatusBar.Checked = true;
                nuiPanel4.Visible = true;
                nuiPnlStatusBars.Height += nuiPanel4.Size.Height;
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from BottomStatusBarMenuHandler Method");
        }

        /// <summary>
        /// Called when MiddleStatus menu clicked
        /// </summary>
        private void MiddleStatusBarMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into MiddleStatusBarMenuHandler Method");

            if (nmnuCmdViewMiddleStatusBar.Checked)
            {
                nmnuCmdViewMiddleStatusBar.Checked = false;
                nuiPanel3.Visible = false;
                nuiPnlStatusBars.Height -= nuiPanel3.Size.Height;
            }
            else
            {
                nmnuCmdViewMiddleStatusBar.Checked = true;
                nuiPanel3.Visible = true;
                nuiPnlStatusBars.Height += nuiPanel3.Size.Height;
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from MiddleStatusBarMenuHandler Method");
        }

        /// <summary>
        /// Called when TopStatusBar menu clicked
        /// </summary>
        private void TopStatusBarMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into TopStatusBarMenuHandler Method");

            if (nmnuCmdViewTopStatusBar.Checked)
            {
                nmnuCmdViewTopStatusBar.Checked = false;
                nuiPanel2.Visible = false;
                nuiPnlStatusBars.Height -= nuiPanel2.Size.Height;
            }
            else
            {
                nmnuCmdViewTopStatusBar.Checked = true;
                nuiPanel2.Visible = true;
                nuiPnlStatusBars.Height += nuiPanel2.Size.Height;
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from TopStatusBarMenuHandler Method");
        }

        /// <summary>
        /// Called when StatusBar menu clicked
        /// </summary>
        private void StatusBarMenuHandler()
        {
        }

        /// <summary>
        /// Called when MessageBar menu clicked
        /// </summary>
        private void MessageBarMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into MessageBarMenuHandler Method");

            if (ui_ncmdViewMessageBar.Checked)
            {
                nuiPnlMessageBar.Visible = false;
                ui_ncmdViewMessageBar.Checked = false;
            }
            else
            {
                nuiPnlMessageBar.Visible = true;
                ui_ncmdViewMessageBar.Checked = true;
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from MessageBarMenuHandler Method");
        }

        /// <summary>
        /// Called when FilterBar menu clicked
        /// </summary>
        private void FilterBarMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into FilterBarMenuHandler Method");

            if (ui_ncmdViewFilterBar.Checked == false)
            {
                //ui_ndtFilter.Visible = true;
                ui_ncmdViewFilterBar.Checked = true;
            }
            else
            {
                //ui_ndtFilter.Visible = false;
                ui_ncmdViewFilterBar.Checked = false;
            }
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from FilterBarMenuHandler Method");
        }

        /// <summary>
        /// Called when ToolBar menu clicked
        /// </summary>
        private void ToolBarMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into ToolBarMenuHandler Method");

            if (ui_ncmdViewToolBar.Checked == false)
            {
                ui_ndtToolBar.Visible = true;
                ui_ncmdViewToolBar.Checked = true;
            }
            else
            {
                ui_ndtToolBar.Visible = false;
                ui_ncmdViewToolBar.Checked = false;
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from ToolBarMenuHandler Method");
        }

        /// <summary>
        /// Called when ContractInformation menu clicked
        /// </summary>
        private void ContractInformationMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into ContractInformationMenuHandler Method");

            var objfrm = ActiveMdiChild as frmMarketWatch;
            string selectedInstrument = string.Empty;
            if (objfrm != null &&
                (objfrm).uctlMarketWatch1.ui_uctlGridMarketWatch.ui_ndgvGrid.Rows.Count > 0)
            {
                selectedInstrument =
                    (objfrm).uctlMarketWatch1.ui_uctlGridMarketWatch.SelectedRows[0].Cells[0].Value.
                        ToString();
            }

            //frmProductInfo.GetInstance(selectedInstrument).TopMost=true;
            frmProductInfo.GetInstance(selectedInstrument).MdiParent = this;
            frmProductInfo.GetInstance(selectedInstrument).Show();

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from ContractInformationMenuHandler Method");
        }

        /// <summary>
        /// Called when MessageLog menu clicked
        /// </summary>
        private void MessageLogMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into MessageLogMenuHandler Method");
            //backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            //backgroundWorker1.WorkerSupportsCancellation = true;
            //backgroundWorker1.RunWorkerAsync();

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from MessageLogMenuHandler Method");
        }

        /// <summary>
        /// Called when NetPosition menu clicked
        /// </summary>
        private void NetPositionMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into NetPositionMenuHandler Method");
            if (_ctlNP == null)
                _ctlNP = new ctlNetPosition(Profiles, string.Empty);
            if (Properties.Settings.Default.NetPositionInDoc)
            {
                CreateNuiDocument(_ctlNP, "Positions");
            }
            else
            {
                if (Properties.Settings.Default.NetPositionZone == 1)
                {
                    nDockPanelHost1.AddChild(this.Openpositions, Properties.Settings.Default.NetPositionIndex);
                }
                else
                {
                    nDockPanelHost2.AddChild(this.Openpositions, Properties.Settings.Default.NetPositionIndex);
                }
                AddToPanel(_ctlNP);
            }
            //var objfrmNetPosition = new frmNetPosition(_profiles, string.Empty);
            //objfrmNetPosition.MdiParent = this;
            //objfrmNetPosition.Show();

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from NetPositionMenuHandler Method");
        }

        /// <summary>
        /// Called when Trade menu clicked
        /// </summary>
        private void TradeMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into TradeMenuHandler Method");
            if (_ctlTH == null)
                _ctlTH = new ctlHistory(Profiles, string.Empty, _shortcutKeyFilter);
            if (Properties.Settings.Default.TradeHistoryInDoc)
            {
                CreateNuiDocument(_ctlTH, "Trade History");
            }
            else
            {
                if (Properties.Settings.Default.TradeHistoryZone == 1)
                {
                    nDockPanelHost1.AddChild(this.History, Properties.Settings.Default.TradeHistoryIndex);
                }
                else
                {
                    nDockPanelHost2.AddChild(this.History, Properties.Settings.Default.TradeHistoryIndex);
                }
                AddToPanel(_ctlTH);
            }
            //var objfrmTradeWindow = new frmTradeWindow(_profiles, string.Empty, _shortcutKeyFilter);
            //objfrmTradeWindow.MdiParent = this;
            //objfrmTradeWindow.Show();

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from TradeMenuHandler Method");
        }
        /// <summary>
        /// Called when Trades menu clicked
        /// </summary>
        //private void TradesMenuHandler()
        //{
        //    //FileHandling.WriteDevelopmentLog("Main Form : Enter into TradesMenuHandler Method");

        //    var objfrmTradeWindow = new frmTradeWindow(_profiles, string.Empty, _shortcutKeyFilter);
        //    objfrmTradeWindow.MdiParent = this;
        //    objfrmTradeWindow.Show();

        //    //FileHandling.WriteDevelopmentLog("Main Form : Exit from TradesMenuHandler Method");
        //}
        /// <summary>
        /// Loads the workspace settings
        /// </summary>
        private void LoadWorkSpaceMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into LoadWorkSpaceMenuHandler Method");

            string dir = Application.StartupPath;
            var objOpenFileDialog = new OpenFileDialog
            {
                InitialDirectory = _DeafultWorkSpacePath,
                DefaultExt = ".dfb",
                Filter = "(*.dfb)|*.dfb"
            };
            objOpenFileDialog.Multiselect = false;
            DialogResult objDialogResult = objOpenFileDialog.ShowDialog();
            Environment.CurrentDirectory = dir;
            if (objDialogResult == DialogResult.OK)
            {
                CloseAllMenuHandler();
                string p = objOpenFileDialog.FileName;
                string e = System.IO.Path.GetExtension(p);
                string Path = objOpenFileDialog.FileName.Replace(e, ".dat");
                LoadWorkSpace(Path);
                state.Load(objOpenFileDialog.FileName);
            }



            //FileHandling.WriteDevelopmentLog("Main Form : Exit from LoadWorkSpaceMenuHandler Method");
        }

        private void CustomizeMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into CustomizeMenuHandler Method");

            var objfrmcommoncustomizeWindow = new frmCommonCustomize();
            objfrmcommoncustomizeWindow.Sizable = false;
            objfrmcommoncustomizeWindow.StartPosition = FormStartPosition.CenterScreen;
            objfrmcommoncustomizeWindow.ShowDialog(this);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from CustomizeMenuHandler Method");
        }

        private void LoadWorkSpace(string filepath)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into LoadWorkSpace Method");


            if (File.Exists(filepath))
            {
                using (Stream streamRead = File.OpenRead(filepath))
                {
                    var sf = new BinaryFormatter();
                    objPALSASettings = (PALSASettings)sf.Deserialize(streamRead);
                }
            }
            //LoadFormsSettings();
            //LoadMenuBarSettings();
            //LoadCommandBarSettings();
            //LoadDocumentsSettings();
            Thread t = new Thread(LoadPanelsSettings);
            t.IsBackground = true;
            t.Start();
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from LoadWorkSpace Method");
        }
        volatile NUIDocument document = null;
        private void LoadPanelsSettings()
        {
            Action A = () =>
               {
                   //PALSA.uctl.UctlBase uctl=new uctl.UctlBase();
                   foreach (var docs in objPALSASettings.LstDockPanel1Items)
                   {

                       switch ((DockItems)docs.DockItem)
                       {
                           case DockItems.MarketQuote:
                               if (_ctlMQ == null)
                               {
                                   _ctlMQ = new ctlMarketQuote(Portfolio);
                               }
                               _ctlMQ.Portfolios = Portfolio;
                               _ctlMQ.uctlForex1.Portfolios = Portfolio;
                               _ctlMQ.uctlForex1.CurrentPortfolioName = docs.CtrlKey;
                               if (objPALSASettings.DDPortfolioQuotes != null)
                               {
                                   _ctlMQ.SetValuesFromWorkSpace(docs.CtrlKey, objPALSASettings.DDPortfolioQuotes[docs.CtrlKey]);
                               }
                               else
                               {
                                   _ctlMQ.SetValuesFromWorkSpace(docs.CtrlKey, null);
                               }
                               nDockPanelHost1.AddChild(this.QuotePanel, docs.DocIndex);
                               AddToPanel(_ctlMQ);
                               break;
                           case DockItems.Chart:
                               var uctl = new ctlNewChart();
                               ((ctlNewChart)uctl).InitChartData(docs.DocKey);
                               break;
                           case DockItems.MarketWatch:
                               if (_ctlMW == null)
                               {
                                   _ctlMW = new ctlMarketWatch(Portfolio);
                               }
                               _ctlMW.uctlMarketWatch1.CurrentProfileName = docs.CtrlKey;
                               _ctlMW.ShortcutKeyBOE = _shortcutKeyOrderEntry;
                               _ctlMW.OnScriptPortfolioApplyClick -= new Action<string>(_ctlMW_OnScriptPortfolioApplyClick);
                               _ctlMW.OnScriptPortfolioApplyClick += new Action<string>(_ctlMW_OnScriptPortfolioApplyClick);
                               _ctlMW.OnSymbolChartClick -= new Action<DataGridViewRow>(_ctlMW_OnSymbolChartClick);
                               _ctlMW.OnSymbolChartClick += new Action<DataGridViewRow>(_ctlMW_OnSymbolChartClick);
                               _ctlMW.OnSymbolLevel2Click -= new Action<DataGridViewRow>(_ctlMW_OnSymbolLevel2Click);
                               _ctlMW.OnSymbolLevel2Click += new Action<DataGridViewRow>(_ctlMW_OnSymbolLevel2Click);
                               if (objPALSASettings.DDPortfolioQuotes != null)
                                   _ctlMW.uctlMarketWatch1_OnScriptPortfolioApplyClick(docs.CtrlKey, objPALSASettings.DDPortfolioQuotes[docs.CtrlKey].Tables[0]);
                               else
                                   _ctlMW.uctlMarketWatch1_OnScriptPortfolioApplyClick(docs.CtrlKey);
                               //if (objPALSASettings.DDPortfolioQuotes != null)
                               //{
                               //    _ctlMW.LoadFromDataset(objPALSASettings.DDPortfolioQuotes[docs.CtrlKey]);
                               //}
                               //nDockPanelHost1.RemoveChild(this.SymbolPanel);
                               nDockPanelHost1.AddChild(this.SymbolPanel, docs.DocIndex);
                               AddToPanel(_ctlMW);
                               break;
                           case DockItems.MarketDepth:
                               break;
                           case DockItems.PendingOrder:
                               if (_ctlPendingOrders == null)
                               {
                                   _ctlPendingOrders = new ctlPendingOrders(Profiles, "");
                               }
                               nDockPanelHost2.AddChild(this.PendingOrders, 0);
                               AddToPanel(_ctlPendingOrders);
                               break;
                           case DockItems.OrderBook:
                           case DockItems.Radar:
                           case DockItems.Scanner:
                           case DockItems.Trade:
                           case DockItems.Accounts:
                           case DockItems.Alert:
                           case DockItems.Mail:
                               break;
                       }
                   }
                   // clsSplashScreen.CloseSplashScreen();
                   this.Visible = true;
                   try
                   {
                       if (_splashThread.IsAlive)
                       {
                           _splashThread.Abort();
                       }
                   }
                   catch
                   { }
               };
            if (InvokeRequired)
            {
                BeginInvoke(A);
            }
            else
            {
                A();
            }

        }


        public void LoadDocumentsSettings()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into LoadFormsSettings Method");
            Action A = () =>
                {
                    m_DockManager.DocumentManager.CloseAllDocuments();
                    NUIDocument actDoc = new NUIDocument();
                    foreach (string ndocKey in objPALSASettings.DDDocumentsSetting.Keys)
                    {
                        DockPanelSettings docs = objPALSASettings.DDDocumentsSetting[ndocKey];
                        PALSA.uctl.UctlBase uctl = new uctl.UctlBase();
                        switch ((DockItems)docs.DockItem)
                        {
                            case DockItems.MarketQuote:
                                uctl = _ctlMQ = new ctlMarketQuote(Portfolio);
                                _ctlMQ.Portfolios = Portfolio;
                                _ctlMQ.uctlForex1.Portfolios = Portfolio;
                                _ctlMQ.uctlForex1.CurrentPortfolioName = docs.CtrlKey;
                                break;
                            case DockItems.Chart:
                                //uctl = new ctlNewChart();
                                string[] sym = docs.DocKey.Split('-');
                                string[] sym2 = sym[1].Split(' ');
                                PeriodEnum periodicity = PeriodEnum.Minute;
                                if (sym2[1].ToLower().StartsWith("min"))
                                {
                                    periodicity = PeriodEnum.Minute;
                                }
                                else if (sym2[1].ToLower().StartsWith("d"))
                                {
                                    periodicity = PeriodEnum.Day;
                                }
                                else if (sym2[1].ToLower().StartsWith("h"))
                                {
                                    periodicity = PeriodEnum.Hour;
                                }
                                else if (sym2[1].ToLower().StartsWith("mon"))
                                {
                                    periodicity = PeriodEnum.Month;
                                }
                                else if (sym2[1].ToLower().StartsWith("w"))
                                {
                                    periodicity = PeriodEnum.Week;
                                }
                                else if (sym2[1].ToLower().StartsWith("y"))
                                {
                                    periodicity = PeriodEnum.Year;
                                }
                                uctl = new ctlNewChart();//WPFChartControl(sym[0], Convert.ToInt32(sym2[0]), periodicity, this);
                                //uctl.InitChartData(
                                uctl.Dock = DockStyle.Fill;
                                ToolbarChart.Enabled = true;
                                CreateNuiDocument(uctl, sym[0] + "-" + Convert.ToInt32(sym2[0]) + periodicity + "_" + "Chart");
                                break;
                            case DockItems.MarketWatch:
                                uctl = _ctlMW = new ctlMarketWatch(Portfolio);
                                _ctlMW.ShortcutKeyBOE = _shortcutKeyOrderEntry;
                                _ctlMW.ShortcutKeyMarketPicture = _shortcutKeyMarketPicture;
                                _ctlMW.OnScriptPortfolioApplyClick -= new Action<string>(_ctlMW_OnScriptPortfolioApplyClick);
                                _ctlMW.OnScriptPortfolioApplyClick += new Action<string>(_ctlMW_OnScriptPortfolioApplyClick);
                                _ctlMW.OnSymbolChartClick -= new Action<DataGridViewRow>(_ctlMW_OnSymbolChartClick);
                                _ctlMW.OnSymbolChartClick += new Action<DataGridViewRow>(_ctlMW_OnSymbolChartClick);
                                _ctlMW.OnSymbolLevel2Click -= new Action<DataGridViewRow>(_ctlMW_OnSymbolLevel2Click);
                                _ctlMW.OnSymbolLevel2Click += new Action<DataGridViewRow>(_ctlMW_OnSymbolLevel2Click);
                                break;
                            case DockItems.MarketDepth:
                                string sym1 = docs.DocKey.Split('-')[0];
                                uctl = new ctlMarketDepth(this, sym1);
                                uctl.Dock = DockStyle.Fill;

                                break;
                            case DockItems.PendingOrder:

                                break;
                            case DockItems.OrderBook:
                            case DockItems.Radar:
                            case DockItems.Scanner:
                            case DockItems.Trade:
                            case DockItems.Accounts:
                            case DockItems.Alert:
                            case DockItems.Mail:
                                break;
                        }


                        //var ndoc = new NUIDocument("ctrl"); //, 0, objuctlMarketWatch);           
                        //ndoc.Key = docs.DocKey;

                        //ndoc.PrefferedBounds = new Rectangle(uctl.Location.X, uctl.Location.Y, uctl.Width + 10, uctl.Height + 35);
                        //ndoc.Client = uctl;
                        var document = new NUIDocument(docs.Text, -1, uctl);
                        uctl.Dock = DockStyle.Fill;
                        document.ID = Guid.NewGuid();
                        document.Key = docs.DocKey;
                        document.Text = docs.Text;
                        document.Client = uctl;

                        if (docs.IsActive)
                        {
                            actDoc = document;
                        }
                        m_DockManager.DocumentManager.AddDocument(document);
                    }
                    m_DockManager.DocumentManager.DocumentView.Activate();


                };
            if (InvokeRequired)
            {
                BeginInvoke(A);
            }
            else
            {
                A();
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from LoadFormsSettings Method");
        }

        private void LoadCommandBarSettings()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into LoadCommandBarSettings Method");

            //foreach (int s in objPALSASettings.DDCommandBarSetting.Keys)
            //{
            //    CommandBarSetting cmdSetting = objPALSASettings.DDCommandBarSetting[s];
            //    ui_ncbmPALSA.Toolbars[s].RowIndex = cmdSetting.RowIndex;
            //    ui_ncbmPALSA.Toolbars[s].Visible = cmdSetting.Visible;
            //}

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from LoadCommandBarSettings Method");
        }

        private void LoadMenuBarSettings()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into LoadMenuBarSettings Method");

            //foreach (int s in objPALSASettings.DDmenuItemsSetting.Keys)
            //{
            //    NCommand nCommand = FindCommand(s);
            //    MenuSetting mnuSetting = objPALSASettings.DDmenuItemsSetting[s];
            //    nCommand.Checked = mnuSetting.Checked;
            //}

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from LoadMenuBarSettings Method");
        }

        public void LoadFormsSettings()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into LoadFormsSettings Method");

            //foreach (string s in objPALSASettings.DDformSetting.Keys)
            //{
            //    frmBase objfrmBase = null;
            //    var commandID =
            //        (CommandIDS)
            //        Enum.Parse(typeof(CommandIDS), (s.Split('/')[0].ToUpper().Trim().Replace(" ", "_")));
            //    switch (commandID)
            //    {
            //        case CommandIDS.TICKER:
            //            break;
            //        case CommandIDS.TRADE:
            //            string TradeProfileName = s.Split('/')[2];
            //            objfrmBase = new frmTradeWindow(_profiles, TradeProfileName, _shortcutKeyFilter);
            //            break;
            //        case CommandIDS.NET_POSITION:
            //            string NetProfileName = s.Split('/')[2];
            //            objfrmBase = new frmNetPosition(_profiles, NetProfileName);
            //            break;
            //        case CommandIDS.MESSAGE_LOG:
            //            objfrmBase = new frmMessageLog(_shortcutKeyFilter);
            //            //((frmMessageLog)objfrmBase).MessageLogFilterKey = shortcutKeyFilter;
            //            break;
            //        case CommandIDS.CONTRACT_INFORMATION:
            //            break;
            //        case CommandIDS.INDICES_VIEW:
            //            break;
            //        case CommandIDS.MARKET_WATCH:
            //            //string portfolioName = s.Split('/')[2];
            //            //string profileName = s.Split('/')[3];
            //            //objfrmBase = new frmMarketWatch(Portfolio, _profiles, portfolioName, profileName, _objuctlLogin.UserCode);
            //            if (_ctlMW == null)
            //                _ctlMW = new ctlMarketWatch(Portfolio);
            //            AddToPanel(_ctlMW);
            //            break;
            //        case CommandIDS.MARKET_PICTURE:
            //            {
            //                objfrmBase = new frmMarketPicture();
            //            }
            //            break;
            //        case CommandIDS.SNAP_QUOTE:
            //            objfrmBase = new frmSnapQuote();
            //            break;
            //        case CommandIDS.MARKET_STATUS:
            //            objfrmBase = new frmMarketStatus();
            //            break;
            //        case CommandIDS.TOP_GAINERS_LOSERS:
            //            objfrmBase = new frmTopGainersLosers();
            //            break;
            //        case CommandIDS.PLACE_ORDER:
            //            PlaceOrderMenuHandler();
            //            objfrmBase = frmOrderEntry.INSTANCE;
            //            break;
            //        //case CommandIDS.PLACE_BUY_ORDER:
            //        //    DisplayOrderEntryDialog("BUY", Color.Green, "Buy Order Entry");
            //        //    objfrmBase = frmOrderEntry.INSTANCE;
            //        //    break;
            //        //case CommandIDS.PLACE_SELL_ORDER:
            //        //    DisplayOrderEntryDialog("SELL", Color.Blue, "Sell Order Entry");
            //        //    objfrmBase = frmOrderEntry.INSTANCE;
            //        //    break;
            //        case CommandIDS.ORDER_BOOK:
            //            string OrderProfileName = s.Split('/')[2];
            //            objfrmBase = new frmOrderBook(_profiles, OrderProfileName, _shortcutKeyFilter);
            //            break;
            //        case CommandIDS.TRADES:
            //            string TradesProfileName = s.Split('/')[2];
            //            objfrmBase = new frmTradeWindow(_profiles, TradesProfileName, _shortcutKeyFilter);
            //            break;
            //        case CommandIDS.MODIFY_ORDER:
            //            break;
            //        case CommandIDS.CANCEL_SELECTED_ORDER:
            //            break;
            //        case CommandIDS.CANCEL_ALL_ORDERS:
            //            break;
            //        case CommandIDS.PARTICIPANT_LIST:
            //            objfrmBase = new frmParticipaintList();
            //            break;
            //        case CommandIDS.INDEX_BAR:
            //            break;
            //        case CommandIDS.MOST_ACTIVE_PRODCUTS:
            //            objfrmBase = new frmMostActiveProducts();
            //            break;
            //        case CommandIDS.MARKET_QUOTE:
            //            //string portfolio = s.Split('/')[2];
            //            //objfrmBase = new frmMarketQuote { _objPortfolio = Portfolio };
            //            //((frmMarketQuote)objfrmBase).SetValuesFromWorkSpace(portfolio);
            //            if (_ctlMQ == null)
            //                _ctlMQ = new ctlMarketQuote(Portfolio);
            //            //_ctlMQ.Portfolios = Portfolio;
            //            //_ctlMQ.SetValuesFromWorkSpace(portfolio);
            //            AddToPanel(_ctlMQ);
            //            break;
            //        case CommandIDS.NEW_CHART:
            //            {
            //                string[] charts = s.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            //                objfrmBase = new frmNewChart();
            //                ((frmNewChart)objfrmBase).ChartSymbol = charts[2];
            //                (objfrmBase).Formkey = s;
            //                if (File.Exists(ClsPalsaUtility.GetChartsPath() + "\\" + charts[2] + "_" + charts[1]))
            //                {
            //                    ((frmNewChart)objfrmBase).ui_ocxStockChart.LoadFile(ClsPalsaUtility.GetChartsPath() +
            //                                                                         "\\" + charts[2] + "_" + charts[1]);
            //                }
            //                else
            //                {
            //                    ((frmNewChart)objfrmBase).InitChartData(charts[2],
            //                                                             (PALSA.Frm.ePeriodicity)
            //                                                             Enum.Parse(typeof(PALSA.Frm.ePeriodicity), charts[1]));
            //                }
            //            }
            //            break;
            //        case CommandIDS.SURVEILLANCE:
            //            objfrmBase = new frmSurveillance();
            //            break;
            //        default:
            //            break;
            //    }
            //    FormSettings frmSettings;
            //    if (null != objfrmBase)
            //    {
            //        if (objPALSASettings.DDformSetting.TryGetValue(s, out frmSettings))
            //        {
            //            objfrmBase.Location = frmSettings.FormLocation;

            //            if (frmSettings.WindowState == FormWindowState.Maximized)
            //            {
            //                objfrmBase.WindowState = FormWindowState.Maximized;
            //            }
            //            else
            //            {
            //                objfrmBase.Text = frmSettings.Title;
            //                objfrmBase.Width = frmSettings.Width;
            //                objfrmBase.Height = frmSettings.Height;
            //            }
            //        }
            //        objfrmBase.MaximizeBox = false;
            //        objfrmBase.MdiParent = this;
            //        objfrmBase.Show();
            //        string[] sArr = s.Split('/');
            //        if (commandID == CommandIDS.MARKET_PICTURE)
            //        {
            //            string marketWatchKey = string.Empty;
            //            string expiryDate = string.Empty;
            //            if (sArr.Length > 2)
            //                marketWatchKey = sArr[2];
            //            if (sArr.Length > 3)
            //                expiryDate = sArr[3] + "/" + sArr[4];
            //            ((frmMarketPicture)objfrmBase).SetValuesFromWorkSpace(marketWatchKey, expiryDate);
            //        }
            //        else if (commandID == CommandIDS.SNAP_QUOTE)
            //        {
            //            string expiryDate = string.Empty;
            //            string snapMarketKey = string.Empty;
            //            if (sArr.Length > 1)
            //                snapMarketKey = sArr[1];
            //            if (sArr.Length > 2)
            //                expiryDate = sArr[2] + "/" + sArr[3];

            //            ((frmSnapQuote)objfrmBase).SetValuesFromWorkSpace(snapMarketKey, expiryDate);
            //        }
            //    }
            //}

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from LoadFormsSettings Method");
        }

        /// <summary>
        /// Saves workspace settings
        /// </summary>
        private void SaveWorkSpaceMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into SaveWorkSpaceMenuHandler Method");

            SaveWorkSpace();

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from SaveWorkSpaceMenuHandler Method");
        }

        private void SaveWorkSpace()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into SaveWorkSpace Method");

            var objSaveFileDialog = new SaveFileDialog();
            objSaveFileDialog.DefaultExt = ".dfb";
            objSaveFileDialog.Filter = "(*.dfb)|*.dfb";
            objSaveFileDialog.InitialDirectory = _DeafultWorkSpacePath;
            DialogResult objDialogResult = objSaveFileDialog.ShowDialog();

            if (objDialogResult == DialogResult.OK)
            {

                //SaveFormsSettings();
                //SaveMenuBarSettings();
                //SaveCommandBarSettings();
                SavePanelsSettings();
                SaveDocumentsSettings();
                string p = objSaveFileDialog.FileName;
                string e = System.IO.Path.GetExtension(p);
                string Path = objSaveFileDialog.FileName.Replace(e, ".dat");
                using (
                    var stream = new FileStream(Path, FileMode.Create, FileAccess.ReadWrite,
                                                FileShare.None))
                {
                    var sf = new BinaryFormatter();
                    sf.Serialize(stream, objPALSASettings);
                }
                state.Save(objSaveFileDialog.FileName);
            }
            //state.Save();
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from SaveWorkSpace Method");
        }



        private void SavePanelsSettings()
        {

            //objPALSASettings.LstDockPanel1Items.Clear();
            //foreach (NDockZone dpnl in nDockPanelHost1.Children)
            //{

            //}
            //foreach (frmBase frm in MdiChildren)
            //{
            //    var settings = new FormSettings(frm.Location, frm.WindowState, frm.Height, frm.Width, frm.Title);
            //    objPALSASettings.DDformSetting.Add(frm.Formkey, settings);

            //    if (frm.Formkey.Contains("NEW_CHART"))
            //    {
            //        ((frmNewChart)frm).ui_ocxStockChart.SaveFile(ClsPalsaUtility.GetChartsPath() + "\\" +
            //                                                      ((frmNewChart)frm).ChartSymbol +
            //                                                      "_" +
            //                                                      frm.Formkey.Split(new[] { '/' },
            //                                                                        StringSplitOptions.
            //                                                                            RemoveEmptyEntries)[1]);
            //    }
            //}

        }

        private void SaveDocumentsSettings()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into SaveDocumentsSettings Method");
            #region "documentsetting"
            int activeDocIndex = Array.IndexOf(m_DockManager.DocumentManager.Documents, m_DockManager.DocumentManager.ActiveDocument);
            foreach (NUIDocument ndoc in m_DockManager.DocumentManager.Documents)
            {
                int docindex = Array.IndexOf(m_DockManager.DocumentManager.Documents, ndoc);

                string key = ndoc.Key;
                string text = ndoc.Text;
                //bool isDoc = true;
                bool isActive = false;
                if (docindex == activeDocIndex)
                    isActive = true;
                DockItems doctype = DockItems.MarketWatch;
                if (ndoc.Client is WPFChartControl)
                {
                    doctype = DockItems.Chart;
                }
                if (ndoc.Client is ctlNewChart)
                {
                    doctype = DockItems.Chart;
                    ((ctlNewChart)ndoc.Client).SaveInFile(ClsPalsaUtility.GetChartFolder() + key + ".icx");
                }
                else if (ndoc.Client is ctlMarketWatch)
                {
                    doctype = DockItems.MarketWatch;
                    if (objPALSASettings.DDPortfolioQuotes.ContainsKey(((ctlMarketWatch)ndoc.Client).CurrentPortfolio))
                    {
                        objPALSASettings.DDPortfolioQuotes[((ctlMarketWatch)ndoc.Client).CurrentPortfolio] =
                            ((ctlMarketWatch)ndoc.Client).marketDS;
                    }
                    else
                    {
                        objPALSASettings.DDPortfolioQuotes.Add(((ctlMarketWatch)ndoc.Client).CurrentPortfolio,
                                                               ((ctlMarketWatch)ndoc.Client).marketDS);
                    }
                }
                else if (ndoc.Client is ctlMarketQuote)
                {
                    doctype = DockItems.MarketQuote;
                }
                var objDocumentsSetting = new DockPanelSettings(doctype, docindex, key, key, text, isActive);
                if (objPALSASettings.DDDocumentsSetting.ContainsKey(key))
                    objPALSASettings.DDDocumentsSetting[key] = objDocumentsSetting;
                else
                    objPALSASettings.DDDocumentsSetting.Add(key, objDocumentsSetting);
            }
            #endregion

            objPALSASettings.LstDockPanel1Items = new List<DockPanelSettings>();
            objPALSASettings.LstDockPanel2Items = new List<DockPanelSettings>();

            //DockPanelSettings objMQSetting = null;
            //DockPanelSettings objTHSetting = null;
            //DockPanelSettings objNPSetting = null;
            //DockPanelSettings objOBSetting = null;
            //DockPanelSettings objScannerSetting = null;
            //DockPanelSettings objAlertSetting = null;
            //DockPanelSettings objMailSetting = null;

            //if (SymbolPanel.DockState == Nevron.UI.WinForm.Docking.DockState.Hidden)
            //{
            DockPanelSettings objPanelSetting = new DockPanelSettings();
            objPanelSetting.CtrlKey = ((ctlMarketWatch)SymbolPanel.Controls[0]).CurrentPortfolio;
            objPanelSetting.DocKey = "Market Watch" + "-" + ((ctlMarketWatch)SymbolPanel.Controls[0]).CurrentPortfolio;
            objPanelSetting.DockItem = DockItems.MarketWatch;
            objPanelSetting.DocIndex = SymbolPanel.Index;
            if (SymbolPanel.Index == 0)
                objPanelSetting.IsActive = true;
            else
                objPanelSetting.IsActive = false;
            if (!Properties.Settings.Default.MarketWatchInDoc)
            {
                if (Properties.Settings.Default.MarketWatchZone == 1)
                {
                    objPALSASettings.LstDockPanel1Items.Add(objPanelSetting);
                }
                else
                {
                    objPALSASettings.LstDockPanel2Items.Add(objPanelSetting);
                }
                objPALSASettings.DDPortfolioQuotes = new Dictionary<string, DS4MarketWatch>();
                //if (objPALSASettings.DDPortpolioQuotes.ContainsKey(((ctlMarketWatch)SymbolPanel.Controls[0]).CurrentPortfolio))
                //{
                //    objPALSASettings.DDPortpolioQuotes[((ctlMarketWatch)SymbolPanel.Controls[0]).CurrentPortfolio] =
                //        ((ctlMarketWatch)SymbolPanel.Controls[0]).marketDS;
                //}
                //else
                //{
                objPALSASettings.DDPortfolioQuotes.Add(((ctlMarketWatch)SymbolPanel.Controls[0]).CurrentPortfolio,
                                                       ((ctlMarketWatch)SymbolPanel.Controls[0]).marketDS);
                //}
            }
            objPanelSetting = new DockPanelSettings();
            objPanelSetting.CtrlKey = ((ctlMarketQuote)QuotePanel.Controls[0]).uctlForex1.CurrentPortfolioName;
            objPanelSetting.DocKey = "Market Quote" + "-" + ((ctlMarketQuote)QuotePanel.Controls[0]).uctlForex1.CurrentPortfolioName;
            objPanelSetting.DockItem = DockItems.MarketQuote;
            objPanelSetting.DocIndex = QuotePanel.Index;
            if (QuotePanel.Index == 0)
                objPanelSetting.IsActive = true;
            else
                objPanelSetting.IsActive = false;
            if (!Properties.Settings.Default.MarketQuoteInDoc)
            {
                if (Properties.Settings.Default.MarketQuoteZone == 1)
                {
                    objPALSASettings.LstDockPanel1Items.Add(objPanelSetting);
                }
                else
                {
                    objPALSASettings.LstDockPanel2Items.Add(objPanelSetting);
                }
            }

            ////}
            ////else
            ////{
            ////    objMWSetting = new DockPanelSettings(true, DockItems.MW);
            ////}
            //lstDockpanel1.Add(objPanelSetting);
            //if (QuotePanel.DockState == Nevron.UI.WinForm.Docking.DockState.Hidden)
            //{
            //    objMQSetting = new DockPanelSettings(false, DockItems.MQ);
            //}
            //else
            //{
            //    objMQSetting = new DockPanelSettings(true, DockItems.MQ);
            //}
            //lstDockpanel1.Add(objMQSetting);
            //if (History.DockState == Nevron.UI.WinForm.Docking.DockState.Hidden)
            //{
            //    objTHSetting = new DockPanelSettings(false, DockItems.TH);
            //}
            //else
            //{
            //    objTHSetting = new DockPanelSettings(true, DockItems.TH);
            //}
            //lstDockpanel1.Add(objTHSetting);
            //if (Openpositions.DockState == Nevron.UI.WinForm.Docking.DockState.Hidden)
            //{
            //    objNPSetting = new DockPanelSettings(false, DockItems.NP);
            //}
            //else
            //{
            //    objNPSetting = new DockPanelSettings(true, DockItems.NP);
            //}
            //lstDockpanel1.Add(objNPSetting);
            //if (OrderBook.DockState == Nevron.UI.WinForm.Docking.DockState.Hidden)
            //{
            //    objOBSetting = new DockPanelSettings(false, DockItems.OB);
            //}
            //else
            //{
            //    objOBSetting = new DockPanelSettings(true, DockItems.OB);
            //}
            //lstDockpanel1.Add(objOBSetting);
            //if (Scanner.DockState == Nevron.UI.WinForm.Docking.DockState.Hidden)
            //{
            //    objScannerSetting = new DockPanelSettings(false, DockItems.Scanner);
            //}
            //else
            //{
            //    objScannerSetting = new DockPanelSettings(true, DockItems.Scanner);
            //}
            //lstDockpanel1.Add(objScannerSetting);
            //if (Alerts.DockState == Nevron.UI.WinForm.Docking.DockState.Hidden)
            //{
            //    objAlertSetting = new DockPanelSettings(false, DockItems.Alert);
            //}
            //else
            //{
            //    objAlertSetting = new DockPanelSettings(true, DockItems.Alert);
            //}
            //lstDockpanel1.Add(objAlertSetting);
            //if (MailBox.DockState == Nevron.UI.WinForm.Docking.DockState.Hidden)
            //{
            //    objMailSetting = new DockPanelSettings(false, DockItems.Mail);
            //}
            //else
            //{
            //    objMailSetting = new DockPanelSettings(true, DockItems.Mail);
            //}
            //lstDockpanel1.Add(objMailSetting);
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from SaveDocumentsSettings Method");
        }

        //private void SaveOHLC()
        //{
        //    //FileHandling.WriteDevelopmentLog("Main Form : Enter into SaveOHLC Method");  
        //    foreach (KeyValuePair<string,List<OHLC>> item in clsTWSDataManagerJSON.INSTANCE.DDSymbolsOHLC)
        //    {
        //        if (!objPALSASettings.DDsymbolsOhlc.ContainsKey(item.Key))
        //            objPALSASettings.DDsymbolsOhlc.Add(item.Key, item.Value);
        //        else
        //            objPALSASettings.DDsymbolsOhlc[item.Key] = item.Value;
        //    }
        //    //FileHandling.WriteDevelopmentLog("Main Form : Exit from SaveOHLC Method");
        //}

        private void SaveCommandBarSettings()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into SaveCommandBarSettings Method");

            //foreach (NDockingToolbar ntoolBar in ui_ncbmPALSA.Toolbars)
            //{
            //    int floatingX = ntoolBar.FloatingLocation.X;
            //    int floatingY = ntoolBar.FloatingLocation.Y;
            //    int floatingWeight = ntoolBar.FloatingSize.Width;
            //    int floatingHeight = ntoolBar.FloatingSize.Height;
            //    int locationX = ntoolBar.Location.X;
            //    int locationY = ntoolBar.Location.Y;
            //    int Height = ntoolBar.Height;
            //    int Width = ntoolBar.Width;
            //    int rowIndex = ntoolBar.RowIndex;
            //    int index = ui_ncbmPALSA.Toolbars.IndexOf(ntoolBar);
            //    bool visible = ntoolBar.Visible;
            //    string parent = ntoolBar.Parent.GetType().Name;
            //    bool canFloat = ntoolBar.CanFloat;
            //    var objCommandBarSetting = new CommandBarSetting(floatingX, floatingY, floatingWeight,
            //                                                     floatingHeight, locationX, locationY,
            //                                                     Height, Width, rowIndex, index, visible,
            //                                                     parent, canFloat);
            //    //if (index!=0 && objPALSASettings.DDCommandBarSetting.Count != null)
            //    //    objPALSASettings.DDCommandBarSetting[index] = objCommandBarSetting;
            //    //else
            //    if (objPALSASettings.DDCommandBarSetting.ContainsKey(index))
            //        objPALSASettings.DDCommandBarSetting[index] = objCommandBarSetting;
            //    else
            //        objPALSASettings.DDCommandBarSetting.Add(index, objCommandBarSetting);
            //    //MessageBox.Show(ntoolBar.Parent.GetType().Name + ":" + ntoolBar.CanFloat.ToString() + ":" + Convert.ToString(ntoolBar.FloatingLocation.X) + "," + Convert.ToString(ntoolBar.FloatingLocation.Y) + ":" + Convert.ToString(ntoolBar.RowIndex));
            //}

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from SaveCommandBarSettings Method");
        }

        /// <summary>
        /// Saves the settings of Forms
        /// </summary>
        /// <param name="filePath">Path with file name of the file</param>
        public void SaveFormsSettings()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into SaveFormsSettings Method");

            //objPALSASettings.DDformSetting.Clear();
            //foreach (frmBase frm in MdiChildren)
            //{
            //    var settings = new FormSettings(frm.Location, frm.WindowState, frm.Height, frm.Width, frm.Title);
            //    objPALSASettings.DDformSetting.Add(frm.Formkey, settings);

            //    if (frm.Formkey.Contains("NEW_CHART"))
            //    {
            //        ((frmNewChart)frm).ui_ocxStockChart.SaveFile(ClsPalsaUtility.GetChartsPath() + "\\" +
            //                                                      ((frmNewChart)frm).ChartSymbol +
            //                                                      "_" +
            //                                                      frm.Formkey.Split(new[] { '/' },
            //                                                                        StringSplitOptions.
            //                                                                            RemoveEmptyEntries)[1]);
            //    }
            //}

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from SaveFormsSettings Method");
        }

        /// <summary>
        /// Saves the settings of command bar
        /// </summary>
        /// <param name="filePath">Path with file name of the file</param>
        public void SaveMenuBarSettings()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into SaveMenuBarSettings Method");

            //objPALSASettings.DDmenuItemsSetting.Clear();
            //MenuSetting menusetting = null;
            //foreach (NCommand cmd in ui_nmnuBar.Commands)
            //{
            //    menusetting = new MenuSetting(cmd.Properties.ID, cmd.Properties.Text, cmd.Checked);
            //    if (cmd.Properties.ID != -1)
            //        objPALSASettings.DDmenuItemsSetting.Add(cmd.Properties.ID, menusetting);
            //    if (cmd.Commands.Count > 0)
            //    {
            //        foreach (NCommand cmd1 in cmd.Commands)
            //        {
            //            menusetting = new MenuSetting(cmd1.Properties.ID, cmd1.Properties.Text, cmd1.Checked);
            //            if (cmd1.Properties.ID != -1)
            //            {
            //                if (!objPALSASettings.DDmenuItemsSetting.Keys.Contains(cmd1.Properties.ID))
            //                    objPALSASettings.DDmenuItemsSetting.Add(cmd1.Properties.ID, menusetting);
            //                else
            //                    objPALSASettings.DDmenuItemsSetting[cmd1.Properties.ID] = menusetting;
            //            }
            //            if (cmd1.Commands.Count > 0)
            //            {
            //                foreach (NCommand cmd2 in cmd1.Commands)
            //                {
            //                    menusetting = new MenuSetting(cmd2.Properties.ID, cmd2.Properties.Text, cmd2.Checked);
            //                    if (cmd2.Properties.ID != -1)
            //                        objPALSASettings.DDmenuItemsSetting.Add(cmd2.Properties.ID, menusetting);
            //                }
            //            }
            //        }
            //    }
            //}

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from SaveMenuBarSettings Method");
        }

        //private void SaveCommonBarSettings(string filePath)
        //{  
        //    objNCommandBarsState.PersistencyFlags = NCommandBarsStateFlags.All;
        //    objNCommandBarsState.Save(filePath);
        //}
        public class ThreadObject
        {
            public object sender;
            public DocumentEventArgs e;
        }



        public delegate void ResultDelegate(ThreadObject threadObject);
        /// <summary>
        /// Desiarilizes the documents and workspace settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void state_ResolveDocumentClient(object sender, DocumentEventArgs e)
        {
            //Thread.Sleep(1000);
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into objNDockingFrameworkState_ResolveDocumentClient Method");
            string keyValue = e.Document.Key;

            if (keyValue.Contains("Market Watch"))
            {
                string x = keyValue.Split('-')[0];

                _ctlMW = new ctlMarketWatch(Portfolio); _ctlMW.Dock = DockStyle.Fill; _ctlMW.CurrentPortfolio = x;
                _ctlMW.uctlMarketWatch1.CurrentPortfolioName = x;
                _ctlMW.ShortcutKeyBOE = _shortcutKeyOrderEntry;
                _ctlMW.OnScriptPortfolioApplyClick -= new Action<string>(_ctlMW_OnScriptPortfolioApplyClick);
                _ctlMW.OnScriptPortfolioApplyClick += new Action<string>(_ctlMW_OnScriptPortfolioApplyClick);
                _ctlMW.ShortcutKeyMarketPicture = _shortcutKeyMarketPicture;
                _ctlMW.OnSymbolChartClick -= new Action<DataGridViewRow>(_ctlMW_OnSymbolChartClick);
                _ctlMW.OnSymbolChartClick += new Action<DataGridViewRow>(_ctlMW_OnSymbolChartClick);
                _ctlMW.OnSymbolLevel2Click -= new Action<DataGridViewRow>(_ctlMW_OnSymbolLevel2Click);
                _ctlMW.OnSymbolLevel2Click += new Action<DataGridViewRow>(_ctlMW_OnSymbolLevel2Click);
                _ctlMW.OnSymbolMatrixClick -= new Action<DataGridViewRow>(_ctlMW_OnSymbolMatrixClick);
                _ctlMW.OnSymbolMatrixClick += new Action<DataGridViewRow>(_ctlMW_OnSymbolMatrixClick);

                e.Document.Client = _ctlMW;
            }
            else if (keyValue.Contains("Market Quote"))
            {
                string x = keyValue.Split('-')[0];
                _ctlMQ = new ctlMarketQuote(Portfolio);
                _ctlMQ.Dock = DockStyle.Fill;
                _ctlMQ.uctlForex1.CurrentPortfolioName = x;
                e.Document.Client = _ctlMQ;
            }
            else if (keyValue.Contains("Chart"))
            {
                //Symbol sym = Symbol.GetSymbol(keyValue);
                string[] sym = keyValue.Split('-');
                PeriodEnum periodicity = PeriodEnum.Minute;
                if (sym[1].Split(' ')[1].ToLower().StartsWith("min"))
                {
                    periodicity = PeriodEnum.Minute;
                }
                else if (sym[1].Split(' ')[1].ToLower().StartsWith("d"))
                {
                    periodicity = PeriodEnum.Day;
                }
                else if (sym[1].Split(' ')[1].ToLower().StartsWith("h"))
                {
                    periodicity = PeriodEnum.Hour;
                }
                else if (sym[1].Split(' ')[1].ToLower().StartsWith("mon"))
                {
                    periodicity = PeriodEnum.Month;
                }
                else if (sym[1].Split(' ')[1].ToLower().StartsWith("w"))
                {
                    periodicity = PeriodEnum.Week;
                }
                else if (sym[1].Split(' ')[1].ToLower().StartsWith("y"))
                {
                    periodicity = PeriodEnum.Year;
                }

                //WPFChartControl ctlChart = new WPFChartControl(sym[0], Convert.ToInt32(sym[1].Split(' ')[0]), periodicity, this);
                ctlNewChart ctlChart = new ctlNewChart();

                if (File.Exists(ClsPalsaUtility.GetChartFolder() + keyValue + ".icx"))
                    ctlChart.LoadFromFile(ClsPalsaUtility.GetChartFolder() + keyValue + ".icx");
                else
                    ctlChart.InitChartData(keyValue);
                //ctlNewChart ctlChart = new ctlNewChart();
                //ctlChart.InitChartData(keyValue);

                ctlChart.Dock = DockStyle.Fill;
                e.Document.Client = ctlChart;// This is to add it to dock pannel

                //AddIndicatorsInCombo();
            }
            else if (keyValue.Contains("Depth"))
            {
                string sym = keyValue.Split('-')[0];
                ctlMarketDepth ctlDepth = new ctlMarketDepth(this, sym);
                ctlDepth.Dock = DockStyle.Fill;
                e.Document.Client = ctlDepth;
            }
            else if (keyValue.Contains("Matrix"))
            {
                //string sym = keyValue.Split('-')[0];
                //ctlMatrix ctlMat = new ctlMatrix(this, sym);
                //ctlMat.Dock = DockStyle.Fill;
                //e.Document.Client = ctlMat;
            }

            Thread.Sleep(100);
            try
            {
                if (_splashThread.IsAlive)
                {
                    _splashThread.Abort();
                }
            }
            catch { }
            this.Visible = true;

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from objNDockingFrameworkState_ResolveDocumentClient Method");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into FrmMain_FormClosed Method");
            //clsTWSOrderManagerJSON.INSTANCE.OnLogonResponse -= new Action<string, string, string>(INSTANCE_OnLogonResponce);
            clsTWSOrderManagerJSON.INSTANCE.OnLogonResponse -= new Action<string, string, bool>(INSTANCE_OnLogonResponce);
            //timeThread.Abort();

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from FrmMain_FormClosed Method");
        }

        private void DisableCommandIDs()
        {
            ui_ncmdFileLogin.Properties.ID = (int)CommandIDS.LOGIN;
            SetHotkeyHashTable(CommandIDS.LOGIN, ui_ncmdFileLogin);

            //ui_ncmdFileExit.Properties.ID = (int)CommandIDS.EXIT;
            //SetHotkeyHashTable(CommandIDS.EXIT, ui_ncmdFileExit);
            //ui_ncmdFileLogin.Enabled = false;
            //ui_ncmdFileLogOff.Enabled = false;
            ui_ncmdFileLoadWorkSpace.Enabled = false;
            ui_ncmdFileSaveWorkSpace.Enabled = false;
            ui_ncmdFileChangePassword.Enabled = false;
            ui_ncmdViewLanguages.Enabled = false;
            ui_ncmdViewTicker.Enabled = false;
            ui_ncmdViewTrade.Enabled = false;
            ui_ncmdViewNetPosition.Enabled = false;
            ui_ncmdViewMsgLog.Enabled = false;
            ui_ncmdViewContractInfo.Enabled = false;
            ui_ncmdViewToolBar.Enabled = false;

            ui_ncmdViewFilterBar.Enabled = false;
            ui_ncmdViewMessageBar.Enabled = false;
            ui_ncmdViewStatusBar.Enabled = false;

            nmnuCmdViewTopStatusBar.Enabled = false;
            nmnuCmdViewMiddleStatusBar.Enabled = false;
            nmnuCmdViewBottomStatusBar.Enabled = false;
            ui_ncmdViewAdminMsgBar.Enabled = false;
            ui_ncmdViewIndicesView.Enabled = false;

            ui_ncmdViewFullScreen.Enabled = false;
            ui_ncmdMarketMarketWatch.Enabled = false;
            ui_ncmdMarketMarketPicture.Enabled = false;

            ui_ncmdMarketSnapQuote.Enabled = false;

            ui_ncmdMarketMarketStatus.Enabled = false;
            ui_ncmdMarketTopGainerLosers.Enabled = false;
            ui_ncmdOrdersPlaceOrder.Enabled = false;
            //ui_ncmdOrdersPlaceSellOrders.Enabled = false;
            ui_ncmdOrdersOrderBook.Enabled = false;
            ui_ncmdTradesTrades.Enabled = false;
            ui_ncmdToolsCustomize.Enabled = false;
            ui_ncmdToolsLockWorkStation.Enabled = false;
            ui_ncmdToolsPortfolio.Enabled = false;
            ui_ncmdToolsPreferences.Enabled = false;
            ui_ncmdWindowNewWindow.Enabled = false;
            ui_ncmdWindowClose.Enabled = false;
            ui_ncmdWindowCloseAll.Enabled = false;
            ui_ncmdWindowCascade.Enabled = false;
            ui_ncmdWindowTileHorizontally.Enabled = false;
            ui_ncmdWindowTileVertically.Enabled = false;
            ui_ncmdWindowWindow.Enabled = false;
            ui_ncmdLanguagesEnglish.Enabled = false;
            ui_ncmdLanguagesHindi.Enabled = false;
            ui_ncmbThemeMacOS.Enabled = false;
            ui_ncmbThemeOffice2007Black.Enabled = false;
            ui_ncmbThemeOffice2007Blue.Enabled = false;
            ui_ncmbThemeOrange.Enabled = false;
            ui_ncmbThemeVista.Enabled = false;
            ui_ncmdThemeVistaRoyal.Enabled = false;
            ui_ncmdThemeInspirant.Enabled = false;
            ui_ncmdThemeVistaPlus.Enabled = false;
            ui_ncmdThemeVistaSlate.Enabled = false;
            ui_ncmdThemeOpusAlpha.Enabled = false;
            ui_ncmdThemeOffice2007Aqua.Enabled = false;
            ui_ncmdThemeSimple.Enabled = false;
            ui_ntbBackup.Enabled = false;
            ui_ntbPrint.Enabled = false;
            ui_ntbModifyOrder.Enabled = false;
            ui_ntbCancelOrder.Enabled = false;
            ui_ntbCancelAllOrders.Enabled = false;
            ui_ncmdViewParticipantList.Enabled = false;
            ui_ncmdViewIndexBar.Enabled = false;
            ui_ntbFilter.Enabled = false;
            ui_mnuChartsNewChart.Enabled = false;
            ui_ntbLogin.Properties.ID = (int)CommandIDS.LOGIN;
            ui_ntbLogoff.Enabled = false;
            ui_ntbChangePassword.Enabled = false;
            ui_ntbMessageLog.Enabled = false;
            ui_ntbOrderEntry.Enabled = false;
            ui_ntbOrderBook.Enabled = false;
            ui_ntbNetPosition.Enabled = false;
            ui_ntbTrades.Enabled = false;
            ui_ntbMarketWatch.Enabled = false;
            ui_ntbMarketPicture.Enabled = false;
            ui_ntbContractInfo.Enabled = false;
            ui_nmnuWindows.Properties.ID = (int)CommandIDS.WINDOW;
            ui_ncmdMarketQuote.Enabled = false;
            ui_ncmdViewAccountsInfo.Enabled = false;
            ui_ncmdThemeRoyal.Enabled = false;
            ui_ncmdThemeAqua.Enabled = false;
            ui_ncmdThemeMoonlight.Enabled = false;
            ui_ncmdThemeWood.Enabled = false;
            ui_ncmdFileChangePassword.Enabled = false;
            ui_mnuPeriodicity1Minute.Enabled = false;
            ui_mnuPeriodicity5Minute.Enabled = false;
            ui_mnuPeriodicity15Minute.Enabled = false;
            ui_mnuPeriodicity30Minute.Enabled = false;
            ui_mnuPeriodicity1Hour.Enabled = false;
            ui_mnuPeriodicityDaily.Enabled = false;
            ui_mnuPeriodicityWeekly.Enabled = false;
            ui_mnuPeriodicityMonthly.Enabled = false;
            ui_mnuChartTypeBarChart.Enabled = false;
            ui_mnuChartTypeCandleChart.Enabled = false;
            ui_mnuChartTypeLineChart.Enabled = false;
            ui_nmunPriceTypePointandFigure.Enabled = false;
            ui_nmunPriceTypeRenko.Enabled = false;
            ui_nmunPriceTypeKagi.Enabled = false;
            ui_nmunPriceTypeThreeLineBreak.Enabled = false;
            ui_nmunPriceTypeEquiVolume.Enabled = false;
            ui_nmunPriceTypeEquiVolumeShadow.Enabled = false;
            ui_nmunPriceTypeCandleVolume.Enabled = false;
            ui_nmunPriceTypeHeikinAshi.Enabled = false;
            ui_nmunPriceTypeStandardChart.Enabled = false;
            ui_mnuChartsZoomIn.Enabled = false;
            ui_mnuChartsZoomOut.Enabled = false;
            ui_mnuChartsTrackCursor.Enabled = false;
            ui_mnuChartsVolume.Enabled = false;
            ui_mnuChartsGrid.Enabled = false;
            ui_mnuChart3DStyle.Enabled = false;
            ui_mnuSnapshotPrint.Enabled = false;
            ui_mnuSnapshotSave.Enabled = false;
            ui_nmnuTechnicalAnalysisIndicatorList.Enabled = false;
            ui_mnuAddHorizontalLine.Enabled = false;
            ui_mnuAddVerticalLine.Enabled = false;
            ui_mnuAddText.Enabled = false;
            ui_mnuAddTrendLine.Enabled = false;
            ui_mnuAddEllipse.Enabled = false;
            ui_mnuAddSpeedLines.Enabled = false;
            ui_mnuAddGannFan.Enabled = false;
            ui_mnuAddFibonacciArcs.Enabled = false;
            ui_mnuAddFibonacciRetracement.Enabled = false;
            ui_mnuAddFibonacciFan.Enabled = false;
            ui_mnuAddFibonacciTimezone.Enabled = false;
            ui_mnuAddTironeLevel.Enabled = false;
            ui_mnuAddRafRegression.Enabled = false;
            ui_mnuAddErrorChannel.Enabled = false;
            ui_mnuAddRectangle.Enabled = false;
            ui_mnuAddFreeHandDrawing.Enabled = false;
            ui_nmnuSurveillanceSurveillance.Enabled = false;
        }

        private void EnableCommandIDs()
        {
            ui_ncmdFileLogin.Properties.ID = (int)CommandIDS.LOGIN;
            SetHotkeyHashTable(CommandIDS.LOGIN, ui_ncmdFileLogin);

            ui_ncmdFileExit.Properties.ID = (int)CommandIDS.EXIT;
            SetHotkeyHashTable(CommandIDS.EXIT, ui_ncmdFileExit);
            ui_ntbLogin.Properties.ID = (int)CommandIDS.LOGIN;

            ui_ncmdFileLogin.Enabled = true;
            ui_ncmdFileLogOff.Enabled = true;
            ui_ncmdFileLoadWorkSpace.Enabled = true;
            ui_ncmdFileSaveWorkSpace.Enabled = true;
            ui_ncmdFileChangePassword.Enabled = true;
            ui_ncmdViewLanguages.Enabled = true;
            ui_ncmdViewTicker.Enabled = true;
            ui_ncmdViewTrade.Enabled = true;
            ui_ncmdViewNetPosition.Enabled = true;
            ui_ncmdViewMsgLog.Enabled = true;
            ui_ncmdViewContractInfo.Enabled = true;
            ui_ncmdViewToolBar.Enabled = true;

            ui_ncmdViewFilterBar.Enabled = true;
            ui_ncmdViewMessageBar.Enabled = true;
            ui_ncmdViewStatusBar.Enabled = true;

            nmnuCmdViewTopStatusBar.Enabled = true;
            nmnuCmdViewMiddleStatusBar.Enabled = true;
            nmnuCmdViewBottomStatusBar.Enabled = true;
            ui_ncmdViewAdminMsgBar.Enabled = true;
            ui_ncmdViewIndicesView.Enabled = true;

            ui_ncmdViewFullScreen.Enabled = true;
            ui_ncmdMarketMarketWatch.Enabled = true;
            ui_ncmdMarketMarketPicture.Enabled = true;

            ui_ncmdMarketSnapQuote.Enabled = true;

            ui_ncmdMarketMarketStatus.Enabled = true;
            ui_ncmdMarketTopGainerLosers.Enabled = true;
            ui_ncmdOrdersPlaceOrder.Enabled = true;
            //ui_ncmdOrdersPlaceSellOrders.Enabled = true;
            ui_ncmdOrdersOrderBook.Enabled = true;
            ui_ncmdTradesTrades.Enabled = true;
            ui_ncmdToolsCustomize.Enabled = true;
            ui_ncmdToolsLockWorkStation.Enabled = true;
            ui_ncmdToolsPortfolio.Enabled = true;
            ui_ncmdToolsPreferences.Enabled = true;
            ui_ncmdWindowNewWindow.Enabled = true;
            ui_ncmdWindowClose.Enabled = true;
            ui_ncmdWindowCloseAll.Enabled = true;
            ui_ncmdWindowCascade.Enabled = true;
            ui_ncmdWindowTileHorizontally.Enabled = true;
            ui_ncmdWindowTileVertically.Enabled = true;
            ui_ncmdWindowWindow.Enabled = true;
            ui_ncmdLanguagesEnglish.Enabled = true;
            ui_ncmdLanguagesHindi.Enabled = true;
            ui_ncmbThemeMacOS.Enabled = true;
            ui_ncmbThemeOffice2007Black.Enabled = true;
            ui_ncmbThemeOffice2007Blue.Enabled = true;
            ui_ncmbThemeOrange.Enabled = true;
            ui_ncmbThemeVista.Enabled = true;
            ui_ncmdThemeVistaRoyal.Enabled = true;
            ui_ncmdThemeInspirant.Enabled = true;
            ui_ncmdThemeVistaPlus.Enabled = true;
            ui_ncmdThemeVistaSlate.Enabled = true;
            ui_ncmdThemeOpusAlpha.Enabled = true;
            ui_ncmdThemeOffice2007Aqua.Enabled = true;
            ui_ncmdThemeSimple.Enabled = true;
            ui_ntbBackup.Enabled = true;
            ui_ntbPrint.Enabled = true;
            ui_ntbModifyOrder.Enabled = true;
            ui_ntbCancelOrder.Enabled = true;
            ui_ntbCancelAllOrders.Enabled = true;
            ui_ncmdViewParticipantList.Enabled = true;
            ui_ncmdViewIndexBar.Enabled = true;
            ui_ntbFilter.Enabled = true;
            ui_mnuChartsNewChart.Enabled = true;

            ui_ntbLogoff.Enabled = true;
            ui_ntbChangePassword.Enabled = true;
            ui_ntbMessageLog.Enabled = true;
            ui_ntbOrderEntry.Enabled = true;
            ui_ntbOrderBook.Enabled = true;
            ui_ntbNetPosition.Enabled = true;
            ui_ntbTrades.Enabled = true;
            ui_ntbMarketWatch.Enabled = true;
            ui_ntbMarketPicture.Enabled = true;
            ui_ntbContractInfo.Enabled = true;
            ui_nmnuWindows.Enabled = true;
            ui_ncmdMarketQuote.Enabled = true;
            ui_ncmdViewAccountsInfo.Enabled = true;
            ui_ncmdThemeRoyal.Enabled = true;
            ui_ncmdThemeAqua.Enabled = true;
            ui_ncmdThemeMoonlight.Enabled = true;
            ui_ncmdThemeWood.Enabled = true;
            ui_ncmdFileChangePassword.Enabled = true;

            ui_mnuPeriodicity1Minute.Enabled = true;
            ui_mnuPeriodicity5Minute.Enabled = true;
            ui_mnuPeriodicity15Minute.Enabled = true;
            ui_mnuPeriodicity30Minute.Enabled = true;
            ui_mnuPeriodicity1Hour.Enabled = true;
            ui_mnuPeriodicityDaily.Enabled = true;
            ui_mnuPeriodicityWeekly.Enabled = true;
            ui_mnuPeriodicityMonthly.Enabled = true;
            ui_mnuChartTypeBarChart.Enabled = true;
            ui_mnuChartTypeCandleChart.Enabled = true;
            ui_mnuChartTypeLineChart.Enabled = true;
            ui_nmunPriceTypePointandFigure.Enabled = true;
            ui_nmunPriceTypeRenko.Enabled = true;
            ui_nmunPriceTypeKagi.Enabled = true;
            ui_nmunPriceTypeThreeLineBreak.Enabled = true;
            ui_nmunPriceTypeEquiVolume.Enabled = true;
            ui_nmunPriceTypeEquiVolumeShadow.Enabled = true;
            ui_nmunPriceTypeCandleVolume.Enabled = true;
            ui_nmunPriceTypeHeikinAshi.Enabled = true;
            ui_nmunPriceTypeStandardChart.Enabled = true;
            ui_mnuChartsZoomIn.Enabled = true;
            ui_mnuChartsZoomOut.Enabled = true;
            ui_mnuChartsTrackCursor.Enabled = true;
            ui_mnuChartsVolume.Enabled = true;
            ui_mnuChartsGrid.Enabled = true;
            ui_mnuChart3DStyle.Enabled = true;

            ui_nmnuTechnicalAnalysisIndicatorList.Enabled = true;

            //ui_mnuSnapshotPrint.Enabled = true;
            //ui_mnuSnapshotSave.Enabled = true;

            ui_mnuAddHorizontalLine.Enabled = true;
            ui_mnuAddVerticalLine.Enabled = true;
            ui_mnuAddText.Enabled = true;
            ui_mnuAddTrendLine.Enabled = true;
            ui_mnuAddEllipse.Enabled = true;
            ui_mnuAddSpeedLines.Enabled = true;
            ui_mnuAddGannFan.Enabled = true;
            ui_mnuAddFibonacciArcs.Enabled = true;
            ui_mnuAddFibonacciRetracement.Enabled = true;
            ui_mnuAddFibonacciFan.Enabled = true;
            ui_mnuAddFibonacciTimezone.Enabled = true;
            ui_mnuAddTironeLevel.Enabled = true;
            ui_mnuAddRafRegression.Enabled = true;
            ui_mnuAddErrorChannel.Enabled = true;
            ui_mnuAddRectangle.Enabled = true;
            ui_mnuAddFreeHandDrawing.Enabled = true;

            ui_nmnuSurveillanceSurveillance.Enabled = true;
            H1.Enabled = true;
            M1.Enabled = true;
        }
        /// <summary>
        /// Sets the ID value of the commands
        /// </summary>
        private void SetCommandsIDs()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into SetCommandsIDs Method");

            if (File.Exists(ClsPalsaUtility.GetHotKeysFileName()))
            {
                try
                {
                    Stream streamRead = File.OpenRead(ClsPalsaUtility.GetHotKeysFileName());
                    var binaryRead = new BinaryFormatter();
                    _hotKeySettingsHashTable = (Hashtable)binaryRead.Deserialize(streamRead);
                    streamRead.Close();
                }
                catch
                {
                    _hotKeySettingsHashTable = null;
                }
            }
            ui_ncmdFileLogin.Properties.ID = (int)CommandIDS.LOGIN;
            SetHotkeyHashTable(CommandIDS.LOGIN, ui_ncmdFileLogin);

            ui_ncmdFileLogOff.Properties.ID = (int)CommandIDS.LOGOFF;
            SetHotkeyHashTable(CommandIDS.LOGOFF, ui_ncmdFileLogOff);

            ui_ncmdFileLoadWorkSpace.Properties.ID = (int)CommandIDS.LOAD_WORKSPACE;
            SetHotkeyHashTable(CommandIDS.LOAD_WORKSPACE, ui_ncmdFileLoadWorkSpace);

            ui_ncmdFileSaveWorkSpace.Properties.ID = (int)CommandIDS.SAVE_WORKSPACE;
            SetHotkeyHashTable(CommandIDS.SAVE_WORKSPACE, ui_ncmdFileSaveWorkSpace);

            ui_ncmdFileChangePassword.Properties.ID = (int)CommandIDS.CHANGE_PASSWORD;
            SetHotkeyHashTable(CommandIDS.CHANGE_PASSWORD, ui_ncmdFileChangePassword);

            ui_ncmdFileExit.Properties.ID = (int)CommandIDS.EXIT;
            SetHotkeyHashTable(CommandIDS.EXIT, ui_ncmdFileExit);

            ui_ncmdViewLanguages.Properties.ID = (int)CommandIDS.LANGUAGES;
            SetHotkeyHashTable(CommandIDS.LANGUAGES, ui_ncmdViewLanguages);

            ui_ncmdViewTicker.Properties.ID = (int)CommandIDS.TICKER;
            SetHotkeyHashTable(CommandIDS.TICKER, ui_ncmdViewTicker);

            ui_ncmdViewTrade.Properties.ID = (int)CommandIDS.TRADE;
            SetHotkeyHashTable(CommandIDS.TRADE, ui_ncmdViewTrade);

            ui_ncmdViewNetPosition.Properties.ID = (int)CommandIDS.NET_POSITION;
            SetHotkeyHashTable(CommandIDS.NET_POSITION, ui_ncmdViewNetPosition);

            ui_ncmdViewMsgLog.Properties.ID = (int)CommandIDS.MESSAGE_LOG;
            SetHotkeyHashTable(CommandIDS.MESSAGE_LOG, ui_ncmdViewMsgLog);

            ui_ncmdViewContractInfo.Properties.ID = (int)CommandIDS.CONTRACT_INFORMATION;
            SetHotkeyHashTable(CommandIDS.CONTRACT_INFORMATION, ui_ncmdViewContractInfo);

            ui_ncmdViewToolBar.Properties.ID = (int)CommandIDS.TOOL_BAR;
            SetHotkeyHashTable(CommandIDS.TOOL_BAR, ui_ncmdViewToolBar);

            ui_ncmdViewFilterBar.Properties.ID = (int)CommandIDS.FILTER_BAR;
            SetHotkeyHashTable(CommandIDS.FILTER_BAR, ui_ncmdViewFilterBar);

            ui_ncmdViewMessageBar.Properties.ID = (int)CommandIDS.MESSAGE_BAR;
            SetHotkeyHashTable(CommandIDS.MESSAGE_BAR, ui_ncmdViewMessageBar);

            ui_ncmdViewStatusBar.Properties.ID = (int)CommandIDS.STATUS_BAR;
            SetHotkeyHashTable(CommandIDS.STATUS_BAR, ui_ncmdViewStatusBar);

            nmnuCmdViewTopStatusBar.Properties.ID = (int)CommandIDS.TOP_STATUS_BAR;
            SetHotkeyHashTable(CommandIDS.TOP_STATUS_BAR, nmnuCmdViewTopStatusBar);

            nmnuCmdViewMiddleStatusBar.Properties.ID = (int)CommandIDS.MIDDLE_STATUS_BAR;
            SetHotkeyHashTable(CommandIDS.MIDDLE_STATUS_BAR, nmnuCmdViewMiddleStatusBar);

            nmnuCmdViewBottomStatusBar.Properties.ID = (int)CommandIDS.BOTTOM_STATUS_BAR;
            SetHotkeyHashTable(CommandIDS.BOTTOM_STATUS_BAR, nmnuCmdViewBottomStatusBar);

            ui_ncmdViewAdminMsgBar.Properties.ID = (int)CommandIDS.ADMIN_MESSAGE_BAR;
            SetHotkeyHashTable(CommandIDS.ADMIN_MESSAGE_BAR, ui_ncmdViewAdminMsgBar);

            ui_ncmdViewIndicesView.Properties.ID = (int)CommandIDS.INDICES_VIEW;
            SetHotkeyHashTable(CommandIDS.INDICES_VIEW, ui_ncmdViewIndicesView);

            ui_ncmdViewFullScreen.Properties.ID = (int)CommandIDS.FULL_SCREEN;
            SetHotkeyHashTable(CommandIDS.FULL_SCREEN, ui_ncmdViewFullScreen);

            ui_ncmdMarketMarketWatch.Properties.ID = (int)CommandIDS.MARKET_WATCH;
            SetHotkeyHashTable(CommandIDS.MARKET_WATCH, ui_ncmdMarketMarketWatch);

            ui_ncmdMarketMarketPicture.Properties.ID = (int)CommandIDS.MARKET_PICTURE;
            SetHotkeyHashTable(CommandIDS.MARKET_PICTURE, ui_ncmdMarketMarketPicture);

            ui_ncmdMarketSnapQuote.Properties.ID = (int)CommandIDS.SNAP_QUOTE;
            SetHotkeyHashTable(CommandIDS.SNAP_QUOTE, ui_ncmdMarketSnapQuote);

            ui_ncmdMarketMarketStatus.Properties.ID = (int)CommandIDS.MARKET_STATUS;
            SetHotkeyHashTable(CommandIDS.MARKET_STATUS, ui_ncmdMarketMarketStatus);

            ui_ncmdMarketTopGainerLosers.Properties.ID = (int)CommandIDS.TOP_GAINERS_LOSERS;
            SetHotkeyHashTable(CommandIDS.TOP_GAINERS_LOSERS, ui_ncmdMarketTopGainerLosers);

            ui_ncmdOrdersPlaceOrder.Properties.ID = (int)CommandIDS.PLACE_ORDER;
            SetHotkeyHashTable(CommandIDS.PLACE_ORDER, ui_ncmdOrdersPlaceOrder);
            //ui_ncmdOrdersPlaceOrder.Properties.ID = (int)CommandIDS.PLACE_BUY_ORDER;
            //SetHotkeyHashTable(CommandIDS.PLACE_ORDER, ui_ncmdOrdersPlaceOrder);

            //ui_ncmdOrdersPlaceSellOrders.Properties.ID = (int)CommandIDS.PLACE_SELL_ORDER;
            //SetHotkeyHashTable(CommandIDS.PLACE_SELL_ORDER, ui_ncmdOrdersPlaceSellOrders);


            ui_ncmdOrdersOrderBook.Properties.ID = (int)CommandIDS.ORDER_BOOK;
            SetHotkeyHashTable(CommandIDS.ORDER_BOOK, ui_ncmdOrdersOrderBook);

            ui_ncmdTradesTrades.Properties.ID = (int)CommandIDS.TRADES;
            SetHotkeyHashTable(CommandIDS.TRADES, ui_ncmdTradesTrades);

            ui_ncmdToolsCustomize.Properties.ID = (int)CommandIDS.CUSTOMIZE;
            SetHotkeyHashTable(CommandIDS.CUSTOMIZE, ui_ncmdToolsCustomize);

            ui_ncmdToolsLockWorkStation.Properties.ID = (int)CommandIDS.LOCK_WORKSTATION;
            SetHotkeyHashTable(CommandIDS.LOCK_WORKSTATION, ui_ncmdToolsLockWorkStation);

            ui_ncmdToolsPortfolio.Properties.ID = (int)CommandIDS.PORTFOLIO;
            SetHotkeyHashTable(CommandIDS.PORTFOLIO, ui_ncmdToolsPortfolio);

            ui_ncmdToolsPreferences.Properties.ID = (int)CommandIDS.PREFERENCES;
            SetHotkeyHashTable(CommandIDS.PREFERENCES, ui_ncmdToolsPreferences);

            ui_ncmdWindowNewWindow.Properties.ID = (int)CommandIDS.NEW_WINDOW;
            SetHotkeyHashTable(CommandIDS.NEW_WINDOW, ui_ncmdWindowNewWindow);

            ui_ncmdWindowClose.Properties.ID = (int)CommandIDS.CLOSE;
            SetHotkeyHashTable(CommandIDS.CLOSE, ui_ncmdWindowClose);

            ui_ncmdWindowCloseAll.Properties.ID = (int)CommandIDS.CLOSE_ALL;
            SetHotkeyHashTable(CommandIDS.CLOSE_ALL, ui_ncmdWindowCloseAll);

            ui_ncmdWindowCascade.Properties.ID = (int)CommandIDS.CASCADE;
            SetHotkeyHashTable(CommandIDS.CASCADE, ui_ncmdWindowCascade);

            ui_ncmdWindowTileHorizontally.Properties.ID = (int)CommandIDS.TILE_HORIZONTALLY;
            SetHotkeyHashTable(CommandIDS.TILE_HORIZONTALLY, ui_ncmdWindowTileHorizontally);

            ui_ncmdWindowTileVertically.Properties.ID = (int)CommandIDS.TILE_VERTICALLY;
            SetHotkeyHashTable(CommandIDS.TILE_VERTICALLY, ui_ncmdWindowTileVertically);

            ui_ncmdWindowWindow.Properties.ID = (int)CommandIDS.WINDOW;
            SetHotkeyHashTable(CommandIDS.WINDOW, ui_ncmdWindowWindow);

            ui_ncmdLanguagesEnglish.Properties.ID = (int)CommandIDS.ENGLISH;
            SetHotkeyHashTable(CommandIDS.ENGLISH, ui_ncmdLanguagesEnglish);

            ui_ncmdLanguagesHindi.Properties.ID = (int)CommandIDS.HINDI;
            SetHotkeyHashTable(CommandIDS.HINDI, ui_ncmdLanguagesHindi);

            ui_ncmbThemeMacOS.Properties.ID = (int)CommandIDS.MAC_OS;
            SetHotkeyHashTable(CommandIDS.MAC_OS, ui_ncmbThemeMacOS);

            ui_ncmbThemeOffice2007Black.Properties.ID = (int)CommandIDS.OFFICE_2007_BLACk;
            SetHotkeyHashTable(CommandIDS.OFFICE_2007_BLACk, ui_ncmbThemeOffice2007Black);

            ui_ncmbThemeOffice2007Blue.Properties.ID = (int)CommandIDS.OFFICE_2007_BLUE;
            SetHotkeyHashTable(CommandIDS.OFFICE_2007_BLUE, ui_ncmbThemeOffice2007Blue);

            ui_ncmbThemeOrange.Properties.ID = (int)CommandIDS.ORANGE;
            SetHotkeyHashTable(CommandIDS.ORANGE, ui_ncmbThemeOrange);

            ui_ncmbThemeVista.Properties.ID = (int)CommandIDS.VISTA;
            SetHotkeyHashTable(CommandIDS.VISTA, ui_ncmbThemeVista);

            ui_ncmdThemeVistaRoyal.Properties.ID = (int)CommandIDS.VISTA_ROYAL;
            SetHotkeyHashTable(CommandIDS.VISTA_ROYAL, ui_ncmdThemeVistaRoyal);

            ui_ncmdThemeInspirant.Properties.ID = (int)CommandIDS.INSPIRANT;
            SetHotkeyHashTable(CommandIDS.INSPIRANT, ui_ncmdThemeInspirant);

            ui_ncmdThemeVistaPlus.Properties.ID = (int)CommandIDS.VISTA_PLUS;
            SetHotkeyHashTable(CommandIDS.VISTA_PLUS, ui_ncmdThemeVistaPlus);

            ui_ncmdThemeVistaSlate.Properties.ID = (int)CommandIDS.VISTA_SLATE;
            SetHotkeyHashTable(CommandIDS.VISTA_SLATE, ui_ncmdThemeVistaSlate);

            ui_ncmdThemeOpusAlpha.Properties.ID = (int)CommandIDS.OPUS_ALPHA;
            SetHotkeyHashTable(CommandIDS.OPUS_ALPHA, ui_ncmdThemeOpusAlpha);

            ui_ncmdThemeOffice2007Aqua.Properties.ID = (int)CommandIDS.OFFICE_2007_AQUA;
            SetHotkeyHashTable(CommandIDS.OFFICE_2007_AQUA, ui_ncmdThemeOffice2007Aqua);

            ui_ncmdThemeSimple.Properties.ID = (int)CommandIDS.SIMPLE;
            SetHotkeyHashTable(CommandIDS.SIMPLE, ui_ncmdThemeSimple);

            ui_ntbBackup.Properties.ID = (int)CommandIDS.ONLINE_BACKUP;
            SetHotkeyHashTable(CommandIDS.ONLINE_BACKUP, ui_ntbBackup);

            ui_ntbPrint.Properties.ID = (int)CommandIDS.PRINT;
            SetHotkeyHashTable(CommandIDS.PRINT, ui_ntbPrint);

            ui_ntbModifyOrder.Properties.ID = (int)CommandIDS.MODIFY_ORDER;
            SetHotkeyHashTable(CommandIDS.MODIFY_ORDER, ui_ntbModifyOrder);

            ui_ntbCancelOrder.Properties.ID = (int)CommandIDS.CANCEL_SELECTED_ORDER;
            SetHotkeyHashTable(CommandIDS.CANCEL_SELECTED_ORDER, ui_ntbCancelOrder);

            ui_ntbCancelAllOrders.Properties.ID = (int)CommandIDS.CANCEL_ALL_ORDERS;
            SetHotkeyHashTable(CommandIDS.CANCEL_ALL_ORDERS, ui_ntbCancelAllOrders);

            ui_ncmdViewParticipantList.Properties.ID = (int)CommandIDS.PARTICIPANT_LIST;
            ui_ncmdViewIndexBar.Properties.ID = (int)CommandIDS.INDEX_BAR;

            ui_ntbFilter.Properties.ID = (int)CommandIDS.FILTER;
            SetHotkeyHashTable(CommandIDS.FILTER, ui_ntbFilter);

            ui_mnuChartsNewChart.Properties.ID = (int)CommandIDS.NEW_CHART;
            ui_ntbLogin.Properties.ID = (int)CommandIDS.LOGIN;
            ui_ntbLogoff.Properties.ID = (int)CommandIDS.LOGOFF;
            ui_ntbChangePassword.Properties.ID = (int)CommandIDS.CHANGE_PASSWORD;
            ui_ntbMessageLog.Properties.ID = (int)CommandIDS.MESSAGE_LOG;
            ui_ntbOrderEntry.Properties.ID = (int)CommandIDS.PLACE_ORDER;
            ui_ntbOrderBook.Properties.ID = (int)CommandIDS.ORDER_BOOK;
            ui_ntbNetPosition.Properties.ID = (int)CommandIDS.NET_POSITION;
            ui_ntbTrades.Properties.ID = (int)CommandIDS.TRADES;
            ui_ntbMarketWatch.Properties.ID = (int)CommandIDS.MARKET_WATCH;
            ui_ntbMarketPicture.Properties.ID = (int)CommandIDS.MARKET_PICTURE;
            ui_ntbContractInfo.Properties.ID = (int)CommandIDS.CONTRACT_INFORMATION;
            ui_nmnuWindows.Properties.ID = (int)CommandIDS.WINDOW;
            ui_ncmdMarketQuote.Properties.ID = (int)CommandIDS.MARKET_QUOTE;
            ui_ncmdViewAccountsInfo.Properties.ID = (int)CommandIDS.ACCOUNTS_TO_TRADE;
            ui_ncmdThemeRoyal.Properties.ID = (int)CommandIDS.ROYAL;
            ui_ncmdThemeAqua.Properties.ID = (int)CommandIDS.AQUA;
            ui_ncmdThemeMoonlight.Properties.ID = (int)CommandIDS.MOONLIGHT;
            ui_ncmdThemeWood.Properties.ID = (int)CommandIDS.WOOD;
            ui_ncmdThemeGreen.Properties.ID = (int)CommandIDS.GREEN;
            ui_ncmdViewRadar.Properties.ID = (int)CommandIDS.RADAR;
            ui_ncmdFileChangePassword.Properties.ID = (int)CommandIDS.CHANGE_PASSWORD;
            ui_mnuPeriodicity1Minute.Properties.ID = (int)CommandIDS.PERIODICITY_1_MINUTE;
            ui_mnuPeriodicity5Minute.Properties.ID = (int)CommandIDS.PERIODICITY_5_MINUTE;
            ui_mnuPeriodicity15Minute.Properties.ID = (int)CommandIDS.PERIODICITY_15_MINUTE;
            ui_mnuPeriodicity30Minute.Properties.ID = (int)CommandIDS.PERIODICITY_30_MINUTE;
            ui_mnuPeriodicity1Hour.Properties.ID = (int)CommandIDS.PERIODICITY_1_HOUR;
            //ui_mnuPeriodicity4Hour.Properties.ID = (int)CommandIDS.PERIODICITY_4_HOUR;
            ui_mnuPeriodicityDaily.Properties.ID = (int)CommandIDS.PERIODICITY_DAILY;
            ui_mnuPeriodicityWeekly.Properties.ID = (int)CommandIDS.PERIODICITY_WEEKLY;
            ui_mnuPeriodicityMonthly.Properties.ID = (int)CommandIDS.PERIODICITY_MONTHLY;
            ui_mnuChartTypeBarChart.Properties.ID = (int)CommandIDS.CHARTTYPE_BAR_CHART;
            ui_mnuChartTypeCandleChart.Properties.ID = (int)CommandIDS.CHARTTYPE_CANDLE_CHART;
            ui_mnuChartTypeLineChart.Properties.ID = (int)CommandIDS.CHARTTYPE_LINE_CHART;
            ui_nmunPriceTypePointandFigure.Properties.ID = (int)CommandIDS.PRICETYPE_POINT_AND_FIGURE;
            ui_nmunPriceTypeRenko.Properties.ID = (int)CommandIDS.PRICETYPE_RENKO;
            ui_nmunPriceTypeKagi.Properties.ID = (int)CommandIDS.PRICETYPE_KAGI;
            ui_nmunPriceTypeThreeLineBreak.Properties.ID = (int)CommandIDS.PRICETYPE_THREE_LINE_BREAK;
            ui_nmunPriceTypeEquiVolume.Properties.ID = (int)CommandIDS.PRICETYPE_EQUI_VOLUME;
            ui_nmunPriceTypeEquiVolumeShadow.Properties.ID = (int)CommandIDS.PRICETYPE_EQUI_VOLUME_SHADOW;
            ui_nmunPriceTypeCandleVolume.Properties.ID = (int)CommandIDS.PRICETYPE_CANDLE_VOLUME;
            ui_nmunPriceTypeHeikinAshi.Properties.ID = (int)CommandIDS.PRICETYPE_HEIKIN_ASHI;
            ui_nmunPriceTypeStandardChart.Properties.ID = (int)CommandIDS.PRICETYPE_STANDARD_CHART;
            ui_mnuChartsZoomIn.Properties.ID = (int)CommandIDS.ZOOM_IN;
            ui_mnuChartsZoomOut.Properties.ID = (int)CommandIDS.ZOOM_OUT;
            ui_mnuChartsTrackCursor.Properties.ID = (int)CommandIDS.TRACK_CURSOR;
            ui_mnuChartsVolume.Properties.ID = (int)CommandIDS.VOLUME;
            ui_mnuChartsGrid.Properties.ID = (int)CommandIDS.GRID;
            ui_mnuChart3DStyle.Properties.ID = (int)CommandIDS.CHART_3D_STYLE;
            ui_mnuSnapshotPrint.Properties.ID = (int)CommandIDS.SNAPSHOT_PRINT;
            ui_mnuSnapshotSave.Properties.ID = (int)CommandIDS.SNAPSHOT_SAVE;
            ui_nmnuTechnicalAnalysisIndicatorList.Properties.ID = (int)CommandIDS.INDICATOR_LIST;
            ui_mnuAddHorizontalLine.Properties.ID = (int)CommandIDS.HORIZONTAL_LINE;
            ui_mnuAddVerticalLine.Properties.ID = (int)CommandIDS.VERTICAL_LINE;
            ui_mnuAddText.Properties.ID = (int)CommandIDS.TEXT;
            ui_mnuAddTrendLine.Properties.ID = (int)CommandIDS.TREND_LINE;
            ui_mnuAddEllipse.Properties.ID = (int)CommandIDS.ELLIPSE;
            ui_mnuAddSpeedLines.Properties.ID = (int)CommandIDS.SPEED_LINES;
            ui_mnuAddGannFan.Properties.ID = (int)CommandIDS.GANN_FAN;
            ui_mnuAddFibonacciArcs.Properties.ID = (int)CommandIDS.FIBONACCI_ARC;
            ui_mnuAddFibonacciRetracement.Properties.ID = (int)CommandIDS.FIBONACCI_RETRACEMENT;
            ui_mnuAddFibonacciFan.Properties.ID = (int)CommandIDS.FIBONACCI_FAN;
            ui_mnuAddFibonacciTimezone.Properties.ID = (int)CommandIDS.FIBONACCI_TIMEZONE;
            ui_mnuAddTironeLevel.Properties.ID = (int)CommandIDS.TIRONE_LEVEL;
            ui_mnuAddRafRegression.Properties.ID = (int)CommandIDS.RAFF_REGRESSION;
            ui_mnuAddErrorChannel.Properties.ID = (int)CommandIDS.ERROR_CHANNEL;
            ui_mnuAddRectangle.Properties.ID = (int)CommandIDS.RECTANGLE;
            ui_mnuAddFreeHandDrawing.Properties.ID = (int)CommandIDS.FREE_HAND_DRAWING;
            ui_nmnuSurveillanceSurveillance.Properties.ID = (int)CommandIDS.SURVEILLANCE;
            ui_nmnuHelpAboutUs.Properties.ID = (int)CommandIDS.ABOUTUS;
            ui_ncmdViewScanner.Properties.ID = (int)CommandIDS.SCANNER;
            ui_ncmdPendingOrders.Properties.ID = (int)CommandIDS.PENDING_ORDERS;

            ui_ncmdFileCreateDemoAccount.Properties.ID = (int)CommandIDS.CREATE_DEMO_ACCOUNT;
            M1.Properties.ID = (int)CommandIDS.PERIODICITY_1_MINUTE;
            M5.Properties.ID = (int)CommandIDS.PERIODICITY_5_MINUTE;
            M15.Properties.ID = (int)CommandIDS.PERIODICITY_15_MINUTE;
            M30.Properties.ID = (int)CommandIDS.PERIODICITY_30_MINUTE;
            H1.Properties.ID = (int)CommandIDS.PERIODICITY_1_HOUR;
            D1.Properties.ID = (int)CommandIDS.PERIODICITY_DAILY;
            MN.Properties.ID = (int)CommandIDS.PERIODICITY_MONTHLY;
            ZoomIn.Properties.ID = (int)CommandIDS.ZOOM_IN;
            ZoomOut.Properties.ID = (int)CommandIDS.ZOOM_OUT;
            cmdVerticalLine.Properties.ID = (int)CommandIDS.VERTICAL_LINE;
            cmdHoriLine.Properties.ID = (int)CommandIDS.HORIZONTAL_LINE;
            TrackCursor.Properties.ID = (int)CommandIDS.TRACK_CURSOR;

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from SetCommandsIDs Method");
        }

        private void SetHotkeyHashTable(CommandIDS cmd, ToolStripMenuItem ntlsMnuItem)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into SetHotkeyHashTable Method");
            if (!_CommandBarHash.ContainsKey(cmd.ToString()))
                _CommandBarHash.Add(cmd.ToString(), ntlsMnuItem);
            string shortcut = ntlsMnuItem.ShortcutKeys.ToString();
            if (_hotKeySettingsHashTable == null)
            {
                _hotKeySettingsHashTable = new Hashtable();
            }

            if (shortcut.ToLower() == "none")
            {
                shortcut = UctlHotKeysSettings.NONE_HOTEKEY;
            }
            //else
            //{
            if (_hotKeySettingsHashTable.ContainsKey(cmd.ToString()))
            {
                ntlsMnuItem.ShortcutKeyDisplayString = _hotKeySettingsHashTable[cmd.ToString()].ToString();
                ntlsMnuItem.ShowShortcutKeys = true;
                string[] x = _hotKeySettingsHashTable[cmd.ToString()].ToString().Split('+');
                if (x.Count() == 1)
                {
                    if (x[0].ToUpper() != "[NONE]")
                    {
                        ntlsMnuItem.ShortcutKeys = (Keys)Enum.Parse(typeof(Keys), _hotKeySettingsHashTable[cmd.ToString()].ToString());
                    }
                    else
                    {
                        ntlsMnuItem.ShortcutKeyDisplayString = string.Empty;
                    }
                }
                else
                {
                    ntlsMnuItem.ShortcutKeys = (Keys)Enum.Parse(typeof(Keys), x[0].ToString()) | (Keys)Enum.Parse(typeof(Keys), x[1].ToString());
                }
            }
            else
            {
                _hotKeySettingsHashTable.Add(cmd.ToString(), shortcut);
            }
            //}


            //FileHandling.WriteDevelopmentLog("Main Form : Exit from SetHotkeyHashTable Method");
        }

        /// <summary>
        /// Saves the hot key settings
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="ncmd"></param>
        private void SetHotkeyHashTable(CommandIDS cmd, NCommand ncmd)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into SetHotkeyHashTable Method");
            if (!_CommandBarHash.ContainsKey(cmd.ToString()))
                _CommandBarHash.Add(cmd.ToString(), ncmd);
            string shortcut = ncmd.Properties.Shortcut.ToString();
            if (shortcut.ToLower() == "none")
            {
                shortcut = UctlHotKeysSettings.NONE_HOTEKEY;
            }
            if (_hotKeySettingsHashTable == null)
            {
                _hotKeySettingsHashTable = new Hashtable();
            }
            //  else
            {
                if (_hotKeySettingsHashTable.ContainsKey(cmd.ToString()))
                {
                    SetHotKey("", _hotKeySettingsHashTable[cmd.ToString()].ToString(), ncmd);
                }
                else
                {
                    _hotKeySettingsHashTable.Add(cmd.ToString(), shortcut);
                }
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from SetHotkeyHashTable Method");
        }

        /// <summary>
        /// Applys the specified hot keys to windows
        /// </summary>
        private void ApplyHotkeys()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into ApplyHotkeys Method");

            foreach (string Key in _hotKeySettingsHashTable.Keys)
            {
                var ncmd = ((NCommand)_CommandBarHash[Key]);
                ncmd.Properties.Shortcut.Modifiers = Keys.None;
                if (ncmd != null)
                {
                    string[] strArray = _hotKeySettingsHashTable[Key].ToString().Split('+');
                    if (
                        (strArray[0].Equals(UctlHotKeysSettings.NONE_HOTEKEY,
                                            StringComparison.InvariantCultureIgnoreCase)) || (strArray.Contains("+")))
                    {
                        continue;
                    }
                    for (int strLoop = 0; strLoop < strArray.Length; strLoop++)
                    {
                        string item = strArray[strLoop];
                        item = item.Trim();

                        if (item.Equals("Alt", StringComparison.InvariantCultureIgnoreCase))
                        {
                            ncmd.Properties.Shortcut.Modifiers = ncmd.Properties.Shortcut.Modifiers | Keys.Alt;
                        }
                        else if (item.Equals("Shift", StringComparison.InvariantCultureIgnoreCase))
                        {
                            ncmd.Properties.Shortcut.Modifiers = ncmd.Properties.Shortcut.Modifiers | Keys.Shift;
                        }
                        else if (item.Equals("Ctrl", StringComparison.InvariantCultureIgnoreCase))
                        {
                            ncmd.Properties.Shortcut.Modifiers = ncmd.Properties.Shortcut.Modifiers | Keys.Control;
                        }
                        else
                        {
                            switch (item)
                            {
                                case "0":
                                    item = "D0";
                                    break;
                                case "1":
                                    item = "D1";
                                    break;
                                case "2":
                                    item = "D2";
                                    break;
                                case "3":
                                    item = "D3";
                                    break;
                                case "4":
                                    item = "D4";
                                    break;
                                case "5":
                                    item = "D5";
                                    break;
                                case "6":
                                    item = "D6";
                                    break;
                                case "7":
                                    item = "D7";
                                    break;
                                case "8":
                                    item = "D8";
                                    break;
                                case "9":
                                    item = "D9";
                                    break;
                                default:
                                    break;
                            }
                            ncmd.Properties.Shortcut.Key = (Keys)Enum.Parse(typeof(Keys), item);
                        }
                    }
                }
            }
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from ApplyHotkeys Method");
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            //this.ui_ndmPALSA.DocumentManager.LayoutMdi(_currentlayOut);
        }

        /// <summary>
        /// Sets the specified language to the form and its components
        /// </summary>
        /// <param name="languageCode">Language code</param>
        public void SetLanguage(string languageCode)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into SetLanguage Method");

            ClsLocalizationHandler.LangaugeCode = languageCode;
            ClsLocalizationHandler.INSTANCE.Init();
            SetMenuLanguage();
            //foreach (NUIDocument item in ui_ndmPALSA.DocumentManager.Documents)
            //{
            //    if (item.Client is uctlBase)
            //    {
            //        (item.Client as uctlBase).SetLocalization();
            //    }
            //}

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from SetLanguage Method");
        }

        /// <summary>
        /// Sets the values of the menus corresponding to respective value
        /// </summary>
        private void SetMenuLanguage()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into SetMenuLanguage Method");

            ui_nmnuFile.Properties.Text = ClsLocalizationHandler.File;
            ui_nmnuView.Properties.Text = ClsLocalizationHandler.View;
            ui_nmnuMarket.Properties.Text = ClsLocalizationHandler.Market;
            ui_nmnuOrders.Properties.Text = ClsLocalizationHandler.Order;
            ui_nmnuTrades.Properties.Text = ClsLocalizationHandler.Trades;
            ui_nmnuTools.Properties.Text = ClsLocalizationHandler.Tools;
            ui_nmnuWindows.Properties.Text = ClsLocalizationHandler.Window;
            ui_nmnuHelp.Properties.Text = ClsLocalizationHandler.Help;
            ui_ncmdFileLogin.Properties.Text = ClsLocalizationHandler.Login;
            ui_ncmdFileLogOff.Properties.Text = ClsLocalizationHandler.LogOff;
            ui_ncmdFileLoadWorkSpace.Properties.Text = ClsLocalizationHandler.Load + " " +
                                                       ClsLocalizationHandler.WorkSpace;
            ui_ncmdFileSaveWorkSpace.Properties.Text = ClsLocalizationHandler.Save + " " +
                                                       ClsLocalizationHandler.WorkSpace;
            ui_ncmdFileExit.Properties.Text = ClsLocalizationHandler.Exit;
            ui_ncmdViewTicker.Properties.Text = ClsLocalizationHandler.Ticker;
            ui_ncmdViewTrade.Properties.Text = ClsLocalizationHandler.Trade;
            ui_ncmdViewNetPosition.Properties.Text = ClsLocalizationHandler.Net + " " +
                                                     ClsLocalizationHandler.Position;
            ui_ncmdViewMsgLog.Properties.Text = ClsLocalizationHandler.Message + " " +
                                                ClsLocalizationHandler.Log;
            ui_ncmdViewContractInfo.Properties.Text = ClsLocalizationHandler.Contract + " " +
                                                      ClsLocalizationHandler.Information;
            ui_ncmdViewToolBar.Properties.Text = ClsLocalizationHandler.ToolBar;
            ui_ncmdViewFilterBar.Properties.Text = ClsLocalizationHandler.FilterBar;
            ui_ncmdViewMessageBar.Properties.Text = ClsLocalizationHandler.MessageBar;
            ui_ncmdViewStatusBar.Properties.Text = ClsLocalizationHandler.StatusBar;
            nmnuCmdViewTopStatusBar.Properties.Text = ClsLocalizationHandler.Top + " " +
                                                      ClsLocalizationHandler.StatusBar;
            nmnuCmdViewMiddleStatusBar.Properties.Text = ClsLocalizationHandler.Middle + " " +
                                                         ClsLocalizationHandler.StatusBar;
            nmnuCmdViewBottomStatusBar.Properties.Text = ClsLocalizationHandler.Bottom + " " +
                                                         ClsLocalizationHandler.StatusBar;
            ui_ncmdViewAdminMsgBar.Properties.Text = ClsLocalizationHandler.Admin + " " +
                                                     ClsLocalizationHandler.MessageBar;
            ui_ncmdViewIndicesView.Properties.Text = ClsLocalizationHandler.IndiciesView;
            ui_ncmdViewFullScreen.Properties.Text = ClsLocalizationHandler.FullScreen;
            ui_ncmdMarketMarketWatch.Properties.Text = ClsLocalizationHandler.Market + " " +
                                                       ClsLocalizationHandler.Watch;
            ui_ncmdMarketMarketPicture.Properties.Text = ClsLocalizationHandler.Market + " " +
                                                         ClsLocalizationHandler.Picture;
            ui_ncmdMarketSnapQuote.Properties.Text = ClsLocalizationHandler.Snap + " " +
                                                     ClsLocalizationHandler.Quote;
            ui_ncmdMarketMarketStatus.Properties.Text = ClsLocalizationHandler.Market + " " +
                                                        ClsLocalizationHandler.Status;
            ui_ncmdMarketTopGainerLosers.Properties.Text = ClsLocalizationHandler.Top + " " +
                                                           ClsLocalizationHandler.Gainers + "/" +
                                                           ClsLocalizationHandler.Losers;
            ui_ncmdOrdersPlaceOrder.Properties.Text = ClsLocalizationHandler.PlaceBuyOrder;
            //ui_ncmdOrdersPlaceSellOrders.Properties.Text = ClsLocalizationHandler.PlaceSellOrder;
            ui_ncmdOrdersOrderBook.Properties.Text = ClsLocalizationHandler.Order + " " +
                                                     ClsLocalizationHandler.Book;
            ui_ncmdTradesTrades.Properties.Text = ClsLocalizationHandler.Trades;
            ui_ncmdToolsCustomize.Properties.Text = ClsLocalizationHandler.Customize;
            ui_ncmdToolsLockWorkStation.Properties.Text = ClsLocalizationHandler.LockWorkStation;
            ui_ncmdToolsPortfolio.Properties.Text = ClsLocalizationHandler.Portfolio;
            ui_ncmdToolsPreferences.Properties.Text = ClsLocalizationHandler.Preferences;
            ui_ncmdWindowNewWindow.Properties.Text = ClsLocalizationHandler.New + " " +
                                                     ClsLocalizationHandler.Window;
            ui_ncmdWindowClose.Properties.Text = ClsLocalizationHandler.Close;
            ui_ncmdWindowCloseAll.Properties.Text = ClsLocalizationHandler.Close + " " +
                                                    ClsLocalizationHandler.All;
            ui_ncmdWindowCascade.Properties.Text = ClsLocalizationHandler.Cascade;
            ui_ncmdWindowTileHorizontally.Properties.Text = ClsLocalizationHandler.TileHorizontally;
            ui_ncmdWindowTileVertically.Properties.Text = ClsLocalizationHandler.TileVertically;
            ui_ncmdWindowWindow.Properties.Text = ClsLocalizationHandler.Window;
            ui_ncmdViewThemes.Properties.Text = ClsLocalizationHandler.Themes;

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from SetMenuLanguage Method");
        }

        /// <summary>
        /// Disactivates the timer for workspace locking
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_Activated(object sender, EventArgs e)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into FrmMain_Activated Method");

            ui_tmrLockWorkstation.Enabled = false;

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from FrmMain_Activated Method");
        }

        public int CalClientSize()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into CalClientSize Method");


            //FileHandling.WriteDevelopmentLog("Main Form : Exit from CalClientSize Method");
            return ui_ncbmPALSA.CommandManager.GetAllCommands(true).Count;
        }

        /// <summary>
        /// Activates the timer for workspace locking
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_Deactivate(object sender, EventArgs e)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into FrmMain_Deactivate Method");

            if (Application.OpenForms.Count > 1)
            {
                Application.OpenForms[Application.OpenForms.Count - 1].TopMost = false;
            }

            if (Properties.Settings.Default.IsLockWorkStation)
            {
                ui_tmrLockWorkstation.Enabled = true;
                ui_tmrLockWorkstation.Interval = Properties.Settings.Default.LockWorkStationTime;
            }
            else
            {
                ui_tmrLockWorkstation.Enabled = false;
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from FrmMain_Deactivate Method");
        }

        /// <summary>
        /// Performs workspace locking
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ui_tmrLockWorkstation_Tick(object sender, EventArgs e)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into ui_tmrLockWorkstation_Tick Method");

            LockWorkStationMenuHandler();

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from ui_tmrLockWorkstation_Tick Method");
        }

        /// <summary>
        /// Displalys dialog for modifying the order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ui_ntbModifyOrder_Click(object sender, CommandEventArgs e)
        {
            //uctlOrderEntry objuctlOrderEntry = new uctlOrderEntry();
            //frmDialogForm objfrmDialogForm = new frmDialogForm();
            //objfrmDialogForm.Controls.Clear();
            //Size objSize = new System.Drawing.Size(objuctlOrderEntry.Width + 30, objuctlOrderEntry.Height + 45);
            //objfrmDialogForm.Size = objSize;
            //objfrmDialogForm.Controls.Add(objuctlOrderEntry);
            //objuctlOrderEntry.Dock = DockStyle.Fill;
            //objfrmDialogForm.Text = "Modify Order ";
            //objfrmDialogForm.ShowDialog();
        }


        private void ui_tmrTicker_Tick(object sender, EventArgs e)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into ui_tmrTicker_Tick Method");

            var objSens = new ClsTickerInfo();

            objSens.ID = "OEC_OEC_1_ES_ESH2";

            _objTickerTape.UpdateControl(objSens, ImageType.NO_CHANGE);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from ui_tmrTicker_Tick Method");
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into FrmMain_FormClosing Method");

            //Properties.Settings.Default.LastTickerPortfolio = _objTickerTape._currentTickerPortfolio;
            //Properties.Settings.Default.Save();

            //if (objForm != null)
            //{
            //    objForm.Close();
            //}
            DialogResult result = ClsCommonMethods.ShowMessageBox("Are you sure want to Exit?");
            if (result == DialogResult.Yes)
            {
                clsTWSOrderManagerJSON.INSTANCE.messageLogDS.dtMessageLog.WriteXml(Application.StartupPath + "\\" + _objuctlLogin.UserCode + ".xml", true);
                //timeThread.Abort();
                Process.GetCurrentProcess().Kill();
                Environment.Exit(1);
            }
            else
            {
                e.Cancel = true;
            }
            if (_mutex != null)
            {
                _mutex.ReleaseMutex();
            }
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from FrmMain_FormClosing Method");
        }

        private void FrmMain_MdiChildActivate(object sender, EventArgs e)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into FrmMain_MdiChildActivate Method");

            var objfrmBase = ((frmBase)ActiveMdiChild);
            if (objfrmBase == null)
            {
                return;
            }
            if (objfrmBase.Text.Contains("Chart") ||
                (objfrmBase.Formkey != null && objfrmBase.Formkey.Contains("NEW_CHART")))
            {
                CheckFormAsChart(objfrmBase);
            }
            if (objfrmBase != null)
            {
                if (!_ChildFormList.Contains(ActiveMdiChild))
                    _ChildFormList.Add(ActiveMdiChild);
                //else
                //    _ChildFormList[_ChildFormList.IndexOf(this.ActiveMdiChild)].Text = this.ActiveMdiChild.Text;
                objfrmBase.FormClosed += objfrmBase_FormClosed;
                objfrmBase.TextChanged += objfrmBase_TextChanged;
            }

            AddItemsToWindow();

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from FrmMain_MdiChildActivate Method");
        }

        private void CheckFormAsChart(frmBase objfrmBase)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into CheckFormAsChart Method");

            //ui_mnuChartsVolume.Checked = ((frmNewChart)objfrmBase).ui_ocxStockChart.ShowVolumes;
            //ui_mnuChartsTrackCursor.Checked = ((frmNewChart)objfrmBase).ui_ocxStockChart.CrossHairs;
            //ui_mnuChartsGrid.Checked = ((frmNewChart)objfrmBase).ui_ocxStockChart.XGrid;
            //ui_mnuChart3DStyle.Checked = ((frmNewChart)objfrmBase).ui_ocxStockChart.ThreeDStyle;

            //ManageChartTypeMenuChecking(null);
            //switch (((frmNewChart)objfrmBase).crtChartType)
            //{
            //    case PALSA.Frm.ChartType.BAR:
            //        ui_mnuChartTypeBarChart.Checked = true;
            //        break;
            //    case PALSA.Frm.ChartType.CANDLE:
            //        ui_mnuChartTypeCandleChart.Checked = true;
            //        break;
            //    case PALSA.Frm.ChartType.LINE:
            //        ui_mnuChartTypeLineChart.Checked = true;
            //        break;
            //}
            //ManagePriceTypeMenuChecking(null);
            //switch (((frmNewChart)objfrmBase).crtPriceType)
            //{
            //    case PriceType.POINT_AND_FIGURE:
            //        ui_nmunPriceTypePointandFigure.Checked = true;
            //        break;
            //    case PriceType.RENKO:
            //        ui_nmunPriceTypeRenko.Checked = true;
            //        break;
            //    case PriceType.KAGI:
            //        ui_nmunPriceTypeKagi.Checked = true;
            //        break;
            //    case PriceType.THREE_LINE_BREAK:
            //        ui_nmunPriceTypeThreeLineBreak.Checked = true;
            //        break;
            //    case PriceType.EQUI_VOLUME:
            //        ui_nmunPriceTypeEquiVolume.Checked = true;
            //        break;
            //    case PriceType.EQUI_VOLUME_SHADOW:
            //        ui_nmunPriceTypeEquiVolumeShadow.Checked = true;
            //        break;
            //    case PriceType.CANDLE_VOLUME:
            //        ui_nmunPriceTypeCandleVolume.Checked = true;
            //        break;
            //    case PriceType.HEIKIN_ASHI:
            //        ui_nmunPriceTypeHeikinAshi.Checked = true;
            //        break;
            //    case PriceType.STANDARD_CHART:
            //        ui_nmunPriceTypeStandardChart.Checked = true;
            //        break;
            //}
            //ManagePeriodicityMenuChecking(null);
            //switch (((frmNewChart)objfrmBase)._chartBarType)
            //{
            //    case PALSA.Frm.ePeriodicity.Minutely_1:
            //        {
            //            switch (((frmNewChart)objfrmBase).BarInterval)
            //            {
            //                case 1:
            //                    ui_mnuPeriodicity1Minute.Checked = true;
            //                    break;
            //                case 5:
            //                    ui_mnuPeriodicity5Minute.Checked = true;
            //                    break;
            //                case 15:
            //                    ui_mnuPeriodicity15Minute.Checked = true;
            //                    break;
            //                case 30:
            //                    ui_mnuPeriodicity30Minute.Checked = true;
            //                    break;
            //            }
            //        }
            //        break;
            //    case PALSA.Frm.ePeriodicity.Hourly_1:
            //        {
            //            switch (((frmNewChart)objfrmBase).BarInterval)
            //            {
            //                case 1:
            //                    ui_mnuPeriodicity1Hour.Checked = true;
            //                    break;
            //                //case 4:
            //                //    ui_mnuPeriodicity4Hour.Checked = true;
            //                //    break;
            //            }
            //        }
            //        break;
            //    //case ePeriodicity.Hourly_4:
            //    //    ui_mnuPeriodicity4Hour.Checked = true;
            //    //    break;
            //    case PALSA.Frm.ePeriodicity.Daily:
            //        ui_mnuPeriodicityDaily.Checked = true;
            //        break;
            //    case PALSA.Frm.ePeriodicity.Weekly:
            //        ui_mnuPeriodicityWeekly.Checked = true;
            //        break;
            //    case PALSA.Frm.ePeriodicity.Monthly:
            //        ui_mnuPeriodicityMonthly.Checked = true;
            //        break;
            //}

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from CheckFormAsChart Method");
        }

        private void objfrmBase_TextChanged(object sender, EventArgs e)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into objfrmBase_TextChanged Method");

            var f = (Form)sender;
            int index = _ChildFormList.IndexOf(f);
            if (index >= 0)
                _ChildFormList[_ChildFormList.IndexOf(f)].Text = f.Text;
            AddItemsToWindow();

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from objfrmBase_TextChanged Method");
        }

        private void AddItemsToWindow()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into AddItemsToWindow Method");

            ui_ncmdWindowWindow.Commands.Clear();
            foreach (frmBase item in _ChildFormList)
            {
                var objNCommand = new NCommand();
                objNCommand.Properties.Text = item.Text;
                //objNCommand.Properties.ID = (int)(CommandIDS)Enum.Parse(typeof(CommandIDS), item.Formkey.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries)[0]);
                if (item != null)
                {
                    if (item.IsActive)
                        objNCommand.Checked = true;
                    ui_ncmdWindowWindow.Commands.Add(objNCommand);
                }
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from AddItemsToWindow Method");
        }

        private void objfrmBase_FormClosed(object sender, FormClosedEventArgs e)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into objfrmBase_FormClosed Method");

            var f = (Form)sender;
            _ChildFormList.Remove(f);
            AddItemsToWindow();

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from objfrmBase_FormClosed Method");
        }

        public void DisplayPopUp(string popTitle, string msg, string msgColor, string imgType)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into DisplayPopUp Method");

            #region "  OLD CODE "

            //InitPopup(msg, msgColor, imgType);
            //_popupNotify.Caption.Content.Text = "<b><font size='10' color='black'>" + popTitle + "</font></b>";
            //PopupAnimation animation = PopupAnimation.None;
            //animation |= PopupAnimation.Slide;
            //_popupNotify.AutoHide = true;
            //_popupNotify.Animation = animation;
            //_popupNotify.AnimationDirection = PopupAnimationDirection.BottomToTop;
            //_popupNotify.Palette.Copy(NUIManager.Palette);
            //_popupNotify.Show();

            #endregion "  OLD CODE "

            var skinPopup = new NPopupNotify();
            skinPopup.PredefinedStyle = PredefinedPopupStyle.Skinned;
            skinPopup.PreferredBounds = new Rectangle(skinPopup.PreferredBounds.Left, skinPopup.PreferredBounds.Right,
                                                      110, 50);
            skinPopup.Font = new Font("Verdana", 8.0f);
            skinPopup.Caption.Visible = false;
            NImageAndTextItem content = skinPopup.Content;
            content.Image = imgType == "Connected"
                                ? Image.FromFile(Application.StartupPath + "\\Resx\\thumb-up-20.png")
                                : Image.FromFile(Application.StartupPath + "\\Resx\\red-thumb-20.png");
            content.ImageSize = new NSize(23, 23);

            content.TextMargins = new NPadding(0, 4, 0, 0);
            content.Text = msg;

            PopupAnimation animation = PopupAnimation.None;
            animation |= PopupAnimation.Fade;
            animation |= PopupAnimation.Slide;

            skinPopup.AutoHide = true;
            skinPopup.VisibleSpan = 4000;
            skinPopup.Opacity = 255;
            skinPopup.Animation = animation;
            skinPopup.AnimationDirection = PopupAnimationDirection.BottomToTop;
            skinPopup.VisibleOnMouseOver = false;
            skinPopup.FullOpacityOnMouseOver = false;
            skinPopup.AnimationInterval = 20;
            skinPopup.AnimationSteps = 19;
            skinPopup.Palette.Copy(NUIManager.Palette);
            skinPopup.Show();

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from DisplayPopUp Method");
        }

        #region " Vinod have commited this lines as CommandBarSetting saving in different way  "

        //private void ui_ndtIndexBar_LocationChanged(object sender, EventArgs e)
        //{
        //    SetCommandBarSettingsDD(sender, e);
        //}

        //private void nDockFilter_LocationChanged(object sender, EventArgs e)
        //{
        //    SetCommandBarSettingsDD(sender, e);
        //}

        //private void nDockTicker_VisibleChanged(object sender, EventArgs e)
        //{
        //    SetCommandBarSettingsDD(sender, e);
        //}

        //private void nDockTicker_LocationChanged(object sender, EventArgs e)
        //{
        //    SetCommandBarSettingsDD(sender, e);
        //}

        //private void nDockFilter_VisibleChanged(object sender, EventArgs e)
        //{
        //    SetCommandBarSettingsDD(sender, e);
        //}

        //private void ui_ndtIndexBar_VisibleChanged(object sender, EventArgs e)
        //{
        //    SetCommandBarSettingsDD(sender, e);
        //}

        //private void ui_ndtToolBar_VisibleChanged(object sender, EventArgs e)
        //{
        //    SetCommandBarSettingsDD(sender, e);
        //}

        //private void SetCommandBarSettingsDD(object sender, EventArgs e)
        //{
        //    int floatingX = ((NDockingToolbar)(sender)).FloatingLocation.X;
        //    int floatingY = ((NDockingToolbar)(sender)).FloatingLocation.X;
        //    int floatingHeight = ((NDockingToolbar)(sender)).FloatingSize.Height;
        //    int floatingWidth = ((NDockingToolbar)(sender)).FloatingSize.Width;
        //    int rowIndex = ((NDockingToolbar)(sender)).RowIndex;
        //    int index = ui_ncbmPALSA.Toolbars.IndexOf(((NDockingToolbar)(sender)));
        //    bool visible = ((NDockingToolbar)(sender)).Visible;
        //    CommandBarSetting objCommandBarSetting = new CommandBarSetting(floatingX, floatingY, floatingHeight, floatingWidth, rowIndex, index,visible);
        //    if (objPALSASettings.DDCommandBarSetting[index] != null)
        //        objPALSASettings.DDCommandBarSetting[index] = objCommandBarSetting;
        //    else
        //        objPALSASettings.DDCommandBarSetting.Add(index, objCommandBarSetting);
        //}


        //private void ui_ndtToolBar_LocationChanged(object sender, EventArgs e)
        //{
        //    SetCommandBarSettingsDD(sender, e);
        //}

        #endregion  " Vinod have commited this lines as it has been handeled in different way  "

        #region "     Chart Menu Handlers  "

        private void PeriodicityWeeklyMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into PeriodicityWeeklyMenuHandler Method");

            SetPeriodicity(ePeriodicity.Weekly, 1, NewHistoryType.WEEK, ui_mnuPeriodicityWeekly);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from PeriodicityWeeklyMenuHandler Method");
        }

        private void PeriodicityMonthlyMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into PeriodicityMonthlyMenuHandler Method");

            SetPeriodicity(ePeriodicity.Monthly, 1, NewHistoryType.MONTH, ui_mnuPeriodicityMonthly);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from PeriodicityMonthlyMenuHandler Method");
        }

        private void PeriodicityDailyMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into PeriodicityDailyMenuHandler Method");

            SetPeriodicity(ePeriodicity.Daily, 1, NewHistoryType.DAY, ui_mnuPeriodicityDaily);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from PeriodicityDailyMenuHandler Method");
        }

        private void Periodicity4HourMenuHandler()
        {
            ////FileHandling.WriteDevelopmentLog("Main Form : Enter into Periodicity4HourMenuHandler Method");

            //SetPeriodicity(ePeriodicity.Hourly_1, 4, NewHistoryType.HOUR, ui_mnuPeriodicity4Hour);

            ////FileHandling.WriteDevelopmentLog("Main Form : Exit from Periodicity4HourMenuHandler Method");
        }

        private void Periodicity1HourMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into Periodicity1HourMenuHandler Method");

            SetPeriodicity(ePeriodicity.Hourly_1, 1, NewHistoryType.HOUR, ui_mnuPeriodicity1Hour);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from Periodicity1HourMenuHandler Method");
        }

        private void Periodicity30MinuteMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into Periodicity30MinuteMenuHandler Method");

            SetPeriodicity(ePeriodicity.Minutely_1, 30, NewHistoryType.MINUTE, ui_mnuPeriodicity30Minute);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from Periodicity30MinuteMenuHandler Method");
        }

        private void Periodicity15MinuteMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into Periodicity15MinuteMenuHandler Method");

            SetPeriodicity(ePeriodicity.Minutely_1, 15, NewHistoryType.MINUTE, ui_mnuPeriodicity15Minute);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from Periodicity15MinuteMenuHandler Method");
        }

        private void Periodicity5MinuteMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into Periodicity5MinuteMenuHandler Method");

            SetPeriodicity(ePeriodicity.Minutely_1, 5, NewHistoryType.MINUTE, ui_mnuPeriodicity5Minute);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from Periodicity5MinuteMenuHandler Method");
        }

        private void Periodicity1MinuteMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into Periodicity1MinuteMenuHandler Method");

            SetPeriodicity(ePeriodicity.Minutely_1, 1, NewHistoryType.MINUTE, ui_mnuPeriodicity1Minute);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from Periodicity1MinuteMenuHandler Method");
        }

        public void SetPeriodicity(ePeriodicity periodicity, int interval, NewHistoryType historyType, NCommand cmd)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into SetPeriodicity Method");

            //var objfrmBase = ((frmBase)ActiveMdiChild);

            //if (objfrmBase == null)
            //    return;

            //ManagePeriodicityMenuChecking(cmd);
            //if (objfrmBase.Formkey.Contains("NEW_CHART"))
            //{
            //    ((frmNewChart)objfrmBase).ChangePeriodicity(periodicity, interval, historyType);
            //}
            ManagePeriodicityMenuChecking(cmd);

            if (m_DockManager.DocumentManager.ActiveDocument != null && m_DockManager.DocumentManager.ActiveDocument.Key.Contains("Chart"))
            {
                ((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).ChangePeriodicity(periodicity, interval, historyType);

                //((WPFChartControl)m_DockManager.DocumentManager.ActiveDocument.Client).ChangePeriodicity(periodicity, interval, historyType);

                string oldKey = m_DockManager.DocumentManager.ActiveDocument.Text;//By Kuldeep
                string[] splt = oldKey.Split('-');
                m_DockManager.DocumentManager.ActiveDocument.Text = splt[0] + "-" + interval + historyType.ToString();
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from SetPeriodicity Method");
        }

        private void ManagePeriodicityMenuChecking(NCommand cmd)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into ManagePeriodicityMenuChecking Method");

            foreach (NCommand item in ui_mnuChartsPeriodicity.Commands)
            {
                item.Checked = false;
            }
            if (cmd != null)
                cmd.Checked = true;

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from ManagePeriodicityMenuChecking Method");
        }

        private void LineChartTypeMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into LineChartTypeMenuHandler Method");

            SetChartType(ChartType.LINE, ui_mnuChartTypeLineChart);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from LineChartTypeMenuHandler Method");
        }

        private void CandleChartTypeMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into CandleChartTypeMenuHandler Method");

            SetChartType(ChartType.CANDLE, ui_mnuChartTypeCandleChart);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from CandleChartTypeMenuHandler Method");
        }

        private void BarChartTypeMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into BarChartTypeMenuHandler Method");

            SetChartType(ChartType.BAR, ui_mnuChartTypeBarChart);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from BarChartTypeMenuHandler Method");
        }


        public void SetChartType(ChartType chartType, NCommand cmd)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into SetChartType Method");

            if (m_DockManager.DocumentManager.ActiveDocument != null)
            {
                ManageChartTypeMenuChecking(cmd);
                if (m_DockManager.DocumentManager.ActiveDocument.Key.Contains("Chart"))
                {
                    ((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).ChangeChartType(chartType);
                    //((WPFChartControl)m_DockManager.DocumentManager.ActiveDocument.Client).LoadChartType(chartType);
                }
            }
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from SetChartType Method");
        }

        private void ManageChartTypeMenuChecking(NCommand cmd)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into ManageChartTypeMenuChecking Method");

            foreach (NCommand item in ui_mnuChartsChartType.Commands)
            {
                item.Checked = false;
            }

            if (cmd != null)
                cmd.Checked = true;

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from ManageChartTypeMenuChecking Method");
        }

        private void StandardChartPriceTypeMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into StandardChartPriceTypeMenuHandler Method");

            SetPriceType(PriceType.STANDARD_CHART, ui_nmunPriceTypeStandardChart);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from StandardChartPriceTypeMenuHandler Method");
        }

        private void HeikinAshiPriceTypeMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into HeikinAshiPriceTypeMenuHandler Method");

            SetPriceType(PriceType.HEIKIN_ASHI, ui_nmunPriceTypeHeikinAshi);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from HeikinAshiPriceTypeMenuHandler Method");
        }

        private void CandleVolumePriceTypeMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into CandleVolumePriceTypeMenuHandler Method");

            SetPriceType(PriceType.CANDLE_VOLUME, ui_nmunPriceTypeCandleVolume);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from CandleVolumePriceTypeMenuHandler Method");
        }

        private void EquiVolumeShadowPriceTypeMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into EquiVolumeShadowPriceTypeMenuHandler Method");

            SetPriceType(PriceType.EQUI_VOLUME_SHADOW, ui_nmunPriceTypeEquiVolumeShadow);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from EquiVolumeShadowPriceTypeMenuHandler Method");
        }

        private void EquiVolumePriceTypeMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into EquiVolumePriceTypeMenuHandler Method");

            SetPriceType(PriceType.EQUI_VOLUME, ui_nmunPriceTypeEquiVolume);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from EquiVolumePriceTypeMenuHandler Method");
        }

        private void ThreeLineBreakPriceTypeMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into ThreeLineBreakPriceTypeMenuHandler Method");

            SetPriceType(PriceType.THREE_LINE_BREAK, ui_nmunPriceTypeThreeLineBreak);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from ThreeLineBreakPriceTypeMenuHandler Method");
        }

        private void KagiPriceTypeMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into KagiPriceTypeMenuHandler Method");

            SetPriceType(PriceType.KAGI, ui_nmunPriceTypeKagi);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from KagiPriceTypeMenuHandler Method");
        }

        private void RenkoPriceTypeMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into RenkoPriceTypeMenuHandler Method");

            SetPriceType(PriceType.RENKO, ui_nmunPriceTypeRenko);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from RenkoPriceTypeMenuHandler Method");
        }

        private void PointandFigurePriceTypeMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into PointandFigurePriceTypeMenuHandler Method");

            SetPriceType(PriceType.POINT_AND_FIGURE, ui_nmunPriceTypePointandFigure);

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from PointandFigurePriceTypeMenuHandler Method");
        }

        public void SetPriceType(PriceType priceType, NCommand cmd)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into SetPriceType Method");

            //var objfrmBase = ((frmBase)ActiveMdiChild);

            //if (objfrmBase == null)
            //    return;
            if (m_DockManager.DocumentManager.ActiveDocument != null)
            {
                ManagePriceTypeMenuChecking(cmd);
                if (m_DockManager.DocumentManager.ActiveDocument.Key.Contains("Chart"))
                {
                    ((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).ChangePriceStyle(priceType);
                    //((WPFChartControl)m_DockManager.DocumentManager.ActiveDocument.Client).LoadPriceType(priceType);
                }
            }
            //if (objfrmBase.Formkey.Contains("NEW_CHART"))
            //{
            //((frmNewChart)objfrmBase).ChangePriceStyle(priceType);
            //}

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from SetPriceType Method");
        }

        private void ManagePriceTypeMenuChecking(NCommand cmd)
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into ManagePriceTypeMenuChecking Method");

            foreach (NCommand item in ui_mnuChartsPriceType.Commands)
            {
                item.Checked = false;
            }
            if (cmd != null)
                cmd.Checked = true;

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from ManagePriceTypeMenuChecking Method");
        }

        private void Chart3DStyleMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into Chart3DStyleMenuHandler Method");

            if (m_DockManager.DocumentManager.ActiveDocument != null)
            {
                ui_mnuChart3DStyle.Checked = !ui_mnuChart3DStyle.Checked;

                if (m_DockManager.DocumentManager.ActiveDocument.Key.Contains("Chart"))
                {
                    ((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).Set3DStyle();
                    //((WPFChartControl)m_DockManager.DocumentManager.ActiveDocument.Client).Chart3DStyle(ui_mnuChart3DStyle.Checked);
                }
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from Chart3DStyleMenuHandler Method");
        }

        private void GridMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into GridMenuHandler Method");

            if (m_DockManager.DocumentManager.ActiveDocument != null)
            {
                ui_mnuChartsGrid.Checked = !ui_mnuChartsGrid.Checked;
                if (m_DockManager.DocumentManager.ActiveDocument.Key.Contains("Chart"))
                {
                    ((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).SetGridDisplay(ui_mnuChartsGrid.Checked);
                    //if (!ui_mnuChartsGrid.Checked)
                    //{
                    //    ((WPFChartControl)m_DockManager.DocumentManager.ActiveDocument.Client).VisibleGrid();
                    //    ui_mnuChartsGrid.Checked = true;
                    //}
                    //else
                    //{
                    //    ((WPFChartControl)m_DockManager.DocumentManager.ActiveDocument.Client).HideGrid();
                    //    ui_mnuChartsGrid.Checked = false;
                    //}
                    //((WPFChartControl)m_DockManager.DocumentManager.ActiveDocument.Client).Grid(ui_mnuChartsGrid.Checked);
                }
            }
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from GridMenuHandler Method");
        }

        private void VolumeMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into VolumeMenuHandler Method");

            if (m_DockManager.DocumentManager.ActiveDocument != null)
            {
                ui_mnuChartsVolume.Checked = !ui_mnuChartsVolume.Checked;
                ManageChartTypeMenuChecking(ui_mnuChartsTrackCursor);
                if (m_DockManager.DocumentManager.ActiveDocument.Key.Contains("Chart"))
                {
                    ((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).SetVolumeDisplay();
                    //((WPFChartControl)m_DockManager.DocumentManager.ActiveDocument.Client).Volume(true);
                }
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from VolumeMenuHandler Method");
        }

        private void TrackCursorMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into TrackCursorMenuHandler Method");

            if (m_DockManager.DocumentManager.ActiveDocument != null)
            {
                ui_mnuChartsTrackCursor.Checked = !ui_mnuChartsTrackCursor.Checked;
                if (m_DockManager.DocumentManager.ActiveDocument.Key.Contains("Chart"))
                {
                    ((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).TrackCursor();
                    //((WPFChartControl)m_DockManager.DocumentManager.ActiveDocument.Client).TrackCursor(ui_mnuChartsTrackCursor.Checked);
                }
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from TrackCursorMenuHandler Method");
        }

        private void ZoomOutMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into ZoomOutMenuHandler Method");

            ui_mnuChartsZoomOut.Checked = !ui_mnuChartsZoomOut.Checked;
            if (m_DockManager.DocumentManager.ActiveDocument != null)
            {
                if (m_DockManager.DocumentManager.ActiveDocument.Key.Contains("Chart"))
                {
                    ((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).ZoomOut();
                    //((WPFChartControl)m_DockManager.DocumentManager.ActiveDocument.Client).Zoom("zoomout");
                }
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from ZoomOutMenuHandler Method");
        }

        private void ZoomInMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into ZoomInMenuHandler Method");

            ui_mnuChartsZoomIn.Checked = !ui_mnuChartsZoomIn.Checked;
            if (m_DockManager.DocumentManager.ActiveDocument != null)
            {
                if (m_DockManager.DocumentManager.ActiveDocument.Key.Contains("Chart"))
                {
                    ((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).ZoomIn();
                    //((WPFChartControl)m_DockManager.DocumentManager.ActiveDocument.Client).Zoom("zoomin");
                }
            }
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from ZoomInMenuHandler Method");
        }


        private void SnapshotSaveMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into SnapshotSaveMenuHandler Method");

            //ui_mnuSnapshotSave.Checked = !ui_mnuSnapshotSave.Checked;
            if (m_DockManager.DocumentManager.ActiveDocument != null)
            {
                if (m_DockManager.DocumentManager.ActiveDocument.Key.Contains("Chart"))
                {
                    ((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).SaveChartDialog();
                    //((WPFChartControl)m_DockManager.DocumentManager.ActiveDocument.Client).SaveChart();
                }
            }
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from SnapshotSaveMenuHandler Method");
        }

        private void SnapshotPrintMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into SnapshotPrintMenuHandler Method");

            //ui_mnuSnapshotPrint.Checked = !ui_mnuSnapshotPrint.Checked;
            if (m_DockManager.DocumentManager.ActiveDocument != null)
            {
                if (m_DockManager.DocumentManager.ActiveDocument.Key.Contains("Chart"))
                {
                    ((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).PrintChart();
                    //((WPFChartControl)m_DockManager.DocumentManager.ActiveDocument.Client).PrintChart();
                }
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from SnapshotPrintMenuHandler Method");
        }


        private void IndicatorListMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into IndicatorListMenuHandler Method");


            //if (m_DockManager.DocumentManager.ActiveDocument != null)
            //{
            //    if (m_DockManager.DocumentManager.ActiveDocument.Key.Contains("Chart"))
            //    {
            //        //((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).AddIndicatorList();
            //        ((WPFChartControl)m_DockManager.DocumentManager.ActiveDocument.Client).AddIndicatorList();
            //    }
            //}

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from IndicatorListMenuHandler Method");
        }

        private void FreeHandDrawingMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into FreeHandDrawingMenuHandler Method");

            //var objfrmBase = ((frmBase)ActiveMdiChild);

            //if (objfrmBase == null)
            //    return;

            //if (objfrmBase.Formkey != null && objfrmBase.Formkey.Contains("NEW_CHART"))
            //{
            //    ((frmNewChart)objfrmBase).FreeHandDrawingDisplay();
            //}
            if (m_DockManager.DocumentManager.ActiveDocument != null)
            {
                if (m_DockManager.DocumentManager.ActiveDocument.Key.Contains("Chart"))
                {
                    ((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).DrawLineStudy(STOCKCHARTXLib.StudyType.lsFreehand);
                    //((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).FreeHandDrawingDisplay();
                    //((WPFChartControl)m_DockManager.DocumentManager.ActiveDocument.Client).ApplyLineStudy("Rectangle");
                }
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from FreeHandDrawingMenuHandler Method");
        }

        private void RectangleMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into RectangleMenuHandler Method");

            //var objfrmBase = ((frmBase)ActiveMdiChild);

            //if (objfrmBase == null)
            //    return;

            //if (objfrmBase.Formkey != null && objfrmBase.Formkey.Contains("NEW_CHART"))
            //{
            //    ((frmNewChart)objfrmBase).RectangleDisplay();
            //}
            ui_mnuAddRectangle.Checked = !ui_mnuAddRectangle.Checked;
            if (m_DockManager.DocumentManager.ActiveDocument != null)
            {
                if (m_DockManager.DocumentManager.ActiveDocument.Key.Contains("Chart"))
                {
                    ((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).DrawLineStudy(STOCKCHARTXLib.StudyType.lsRectangle);
                    //((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).RectangleDisplay();
                    //((WPFChartControl)m_DockManager.DocumentManager.ActiveDocument.Client).ApplyLineStudy("Rectangle");
                }
            }
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from RectangleMenuHandler Method");
        }

        private void ErrorChannelMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into ErrorChannelMenuHandler Method");

            //var objfrmBase = ((frmBase)ActiveMdiChild);

            //if (objfrmBase == null)
            //    return;

            //if (objfrmBase.Formkey != null && objfrmBase.Formkey.Contains("NEW_CHART"))
            //{
            //    ((frmNewChart)objfrmBase).ErrorChannelDisplay();
            //}
            ui_mnuAddErrorChannel.Checked = !ui_mnuAddErrorChannel.Checked;
            if (m_DockManager.DocumentManager.ActiveDocument != null)
            {
                if (m_DockManager.DocumentManager.ActiveDocument.Key.Contains("Chart"))
                {
                    ((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).DrawLineStudy(STOCKCHARTXLib.StudyType.lsErrorChannel);
                    //((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).ErrorChannelDisplay();
                    //((WPFChartControl)m_DockManager.DocumentManager.ActiveDocument.Client).ApplyLineStudy("ErrorChannel");
                }
            }
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from ErrorChannelMenuHandler Method");
        }

        private void RaffRegressionMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into RaffRegressionMenuHandler Method");

            //var objfrmBase = ((frmBase)ActiveMdiChild);

            //if (objfrmBase == null)
            //    return;

            //if (objfrmBase.Formkey != null && objfrmBase.Formkey.Contains("NEW_CHART"))
            //{
            //    ((frmNewChart)objfrmBase).RafRegressionDisplay();
            //}
            ui_mnuAddRafRegression.Checked = !ui_mnuAddRafRegression.Checked;
            if (m_DockManager.DocumentManager.ActiveDocument != null)
            {
                if (m_DockManager.DocumentManager.ActiveDocument.Key.Contains("Chart"))
                {
                    ((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).DrawLineStudy(STOCKCHARTXLib.StudyType.lsRaffRegression);
                    //((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).RafRegressionDisplay();
                    //((WPFChartControl)m_DockManager.DocumentManager.ActiveDocument.Client).ApplyLineStudy("RaffRegression");
                }
            }
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from RaffRegressionMenuHandler Method");
        }

        private void QuadrentLinesMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into QuadrentLinesMenuHandler Method");

            //var objfrmBase = ((frmBase)ActiveMdiChild);

            //if (objfrmBase == null)
            //    return;

            //if (objfrmBase.Formkey != null && objfrmBase.Formkey.Contains("NEW_CHART"))
            //{
            //    ((frmNewChart)objfrmBase).QuadrentLinesDisplay();
            //}
            ui_mnuAddQuadrentLines.Checked = !ui_mnuAddQuadrentLines.Checked;
            if (m_DockManager.DocumentManager.ActiveDocument != null)
            {
                if (m_DockManager.DocumentManager.ActiveDocument.Key.Contains("Chart"))
                {
                    ((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).DrawLineStudy(STOCKCHARTXLib.StudyType.lsQuadrantLines);
                    //((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).QuadrentLinesDisplay();
                    //((WPFChartControl)m_DockManager.DocumentManager.ActiveDocument.Client).ApplyLineStudy("QuadrantLines");
                }
            }
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from QuadrentLinesMenuHandler Method");
        }

        private void TironeLevelMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into TironeLevelMenuHandler Method");

            //var objfrmBase = ((frmBase)ActiveMdiChild);

            //if (objfrmBase == null)
            //    return;

            //if (objfrmBase.Formkey != null && objfrmBase.Formkey.Contains("NEW_CHART"))
            //{
            //    ((frmNewChart)objfrmBase).TironeLevelDisplay();
            //}
            ui_mnuAddTironeLevel.Checked = !ui_mnuAddTironeLevel.Checked;
            if (m_DockManager.DocumentManager.ActiveDocument != null)
            {
                if (m_DockManager.DocumentManager.ActiveDocument.Key.Contains("Chart"))
                {
                    ((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).DrawLineStudy(STOCKCHARTXLib.StudyType.lsTironeLevels);
                    //((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).TironeLevelDisplay();
                    //((WPFChartControl)m_DockManager.DocumentManager.ActiveDocument.Client).ApplyLineStudy("TironeLevels");
                }
            }
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from TironeLevelMenuHandler Method");
        }

        private void FibonacciTimeZoneMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into FibonacciTimeZoneMenuHandler Method");

            //var objfrmBase = ((frmBase)ActiveMdiChild);

            //if (objfrmBase == null)
            //    return;

            //if (objfrmBase.Formkey != null && objfrmBase.Formkey.Contains("NEW_CHART"))
            //{
            //    ((frmNewChart)objfrmBase).FibonacciTimezoneDisplay();
            //}
            ui_mnuAddFibonacciTimezone.Checked = !ui_mnuAddFibonacciTimezone.Checked;
            if (m_DockManager.DocumentManager.ActiveDocument != null)
            {
                if (m_DockManager.DocumentManager.ActiveDocument.Key.Contains("Chart"))
                {
                    ((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).DrawLineStudy(STOCKCHARTXLib.StudyType.lsFibonacciTimeZones);
                    //((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).FibonacciTimezoneDisplay();
                    //((WPFChartControl)m_DockManager.DocumentManager.ActiveDocument.Client).ApplyLineStudy("FibonacciTimeZones");
                }
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from FibonacciTimeZoneMenuHandler Method");
        }

        private void FibonacciFanMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into FibonacciFanMenuHandler Method");

            //var objfrmBase = ((frmBase)ActiveMdiChild);

            //if (objfrmBase == null)
            //    return;

            //if (objfrmBase.Formkey != null && objfrmBase.Formkey.Contains("NEW_CHART"))
            //{
            //    ((frmNewChart)objfrmBase).FibonacciFanDisplay();
            //}
            ui_mnuAddFibonacciFan.Checked = !ui_mnuAddFibonacciFan.Checked;
            if (m_DockManager.DocumentManager.ActiveDocument != null)
            {
                if (m_DockManager.DocumentManager.ActiveDocument.Key.Contains("Chart"))
                {
                    ((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).DrawLineStudy(STOCKCHARTXLib.StudyType.lsFibonacciFan);
                    //((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).FibonacciFanDisplay();
                    //((WPFChartControl)m_DockManager.DocumentManager.ActiveDocument.Client).ApplyLineStudy("FibonacciFan");
                }
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from FibonacciFanMenuHandler Method");
        }

        private void FibonacciRetracementMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into FibonacciRetracementMenuHandler Method");

            //var objfrmBase = ((frmBase)ActiveMdiChild);

            //if (objfrmBase == null)
            //    return;

            //if (objfrmBase.Formkey != null && objfrmBase.Formkey.Contains("NEW_CHART"))
            //{
            //    ((frmNewChart)objfrmBase).FibonacciRetracementDisplay();
            //}
            ui_mnuAddFibonacciRetracement.Checked = !ui_mnuAddFibonacciRetracement.Checked;
            if (m_DockManager.DocumentManager.ActiveDocument != null)
            {
                if (m_DockManager.DocumentManager.ActiveDocument.Key.Contains("Chart"))
                {
                    ((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).DrawLineStudy(STOCKCHARTXLib.StudyType.lsFibonacciRetracements);
                    //((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).FibonacciRetracementDisplay();
                    //((WPFChartControl)m_DockManager.DocumentManager.ActiveDocument.Client).ApplyLineStudy("FibonacciRetracements");
                }
            }
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from FibonacciRetracementMenuHandler Method");
        }

        private void FibonacciArcMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into FibonacciArcMenuHandler Method");

            //var objfrmBase = ((frmBase)ActiveMdiChild);

            //if (objfrmBase == null)
            //    return;

            //if (objfrmBase.Formkey != null && objfrmBase.Formkey.Contains("NEW_CHART"))
            //{
            //    ((frmNewChart)objfrmBase).FibonacciArcsDisplay();
            //}
            ui_mnuAddFibonacciArcs.Checked = !ui_mnuAddFibonacciArcs.Checked;
            if (m_DockManager.DocumentManager.ActiveDocument != null)
            {
                if (m_DockManager.DocumentManager.ActiveDocument.Key.Contains("Chart"))
                {
                    ((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).DrawLineStudy(STOCKCHARTXLib.StudyType.lsFibonacciArcs);
                    //((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).FibonacciArcsDisplay();
                    //((WPFChartControl)m_DockManager.DocumentManager.ActiveDocument.Client).ApplyLineStudy("FibonacciArcs");
                }
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from FibonacciArcMenuHandler Method");
        }

        private void GannFanMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into GannFanMenuHandler Method");

            ui_mnuAddGannFan.Checked = !ui_mnuAddGannFan.Checked;
            if (m_DockManager.DocumentManager.ActiveDocument != null)
            {
                if (m_DockManager.DocumentManager.ActiveDocument.Key.Contains("Chart"))
                {
                    ((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).DrawLineStudy(STOCKCHARTXLib.StudyType.lsGannFan);
                    //((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).GannFanDisplay();
                    //((WPFChartControl)m_DockManager.DocumentManager.ActiveDocument.Client).ApplyLineStudy("GannFan");
                }
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from GannFanMenuHandler Method");
        }

        private void SpeedLineMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into SpeedLineMenuHandler Method");

            //var objfrmBase = ((frmBase)ActiveMdiChild);

            //if (objfrmBase == null)
            //    return;

            //if (objfrmBase.Formkey != null && objfrmBase.Formkey.Contains("NEW_CHART"))
            //{
            //    ((frmNewChart)objfrmBase).SpeedLineDisplay();
            //}
            ui_mnuAddSpeedLines.Checked = !ui_mnuAddSpeedLines.Checked;
            if (m_DockManager.DocumentManager.ActiveDocument != null)
            {
                if (m_DockManager.DocumentManager.ActiveDocument.Key.Contains("Chart"))
                {
                    ((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).DrawLineStudy(STOCKCHARTXLib.StudyType.lsSpeedLines);
                    //((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).SpeedLineDisplay();
                    //((WPFChartControl)m_DockManager.DocumentManager.ActiveDocument.Client).ApplyLineStudy("SpeedLines");
                }
            }
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from SpeedLineMenuHandler Method");
        }

        private void EllipseMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into EllipseMenuHandler Method");

            //var objfrmBase = ((frmBase)ActiveMdiChild);

            //if (objfrmBase == null)
            //    return;

            //if (objfrmBase.Formkey != null && objfrmBase.Formkey.Contains("NEW_CHART"))
            //{
            //    ((frmNewChart)objfrmBase).EllipseDisplay();
            //}
            ui_mnuAddEllipse.Checked = !ui_mnuAddEllipse.Checked;
            if (m_DockManager.DocumentManager.ActiveDocument != null)
            {
                if (m_DockManager.DocumentManager.ActiveDocument.Key.Contains("Chart"))
                {
                    ((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).DrawLineStudy(STOCKCHARTXLib.StudyType.lsEllipse);
                    //((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).EllipseDisplay();
                    //((WPFChartControl)m_DockManager.DocumentManager.ActiveDocument.Client).ApplyLineStudy("Ellipse");
                }
            }
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from EllipseMenuHandler Method");
        }

        private void TrednLineMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into TrednLineMenuHandler Method");

            //var objfrmBase = ((frmBase)ActiveMdiChild);

            //if (objfrmBase == null)
            //    return;

            //if (objfrmBase.Formkey != null && objfrmBase.Formkey.Contains("NEW_CHART"))
            //{
            //    ((frmNewChart)objfrmBase).TrendLineDisplay();
            //}
            ui_mnuAddTrendLine.Checked = !ui_mnuAddTrendLine.Checked;
            if (m_DockManager.DocumentManager.ActiveDocument != null)
            {
                if (m_DockManager.DocumentManager.ActiveDocument.Key.Contains("Chart"))
                {
                    ((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).DrawTrendLine();
                    //((WPFChartControl)m_DockManager.DocumentManager.ActiveDocument.Client).ApplyLineStudy("TrendLine");
                }
            }
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from TrednLineMenuHandler Method");
        }

        private void TextMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into TextMenuHandler Method");

            //var objfrmBase = ((frmBase)ActiveMdiChild);

            //if (objfrmBase == null)
            //    return;

            //if (objfrmBase.Formkey != null && objfrmBase.Formkey.Contains("NEW_CHART"))
            //{
            //    ((frmNewChart)objfrmBase).AddText();
            //}

            if (m_DockManager.DocumentManager.ActiveDocument != null)
            {
                if (m_DockManager.DocumentManager.ActiveDocument.Key.Contains("Chart"))
                {

                    ((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).AddText();
                    //((WPFChartControl)m_DockManager.DocumentManager.ActiveDocument.Client).ApplyLineStudy("User Defined Text");
                }

            }
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from TextMenuHandler Method");
        }

        private void VerticalMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into VerticalMenuHandler Method");

            //var objfrmBase = ((frmBase)ActiveMdiChild);

            //if (objfrmBase == null)
            //    return;

            //if (objfrmBase.Formkey != null && objfrmBase.Formkey.Contains("NEW_CHART"))
            //{
            //    ((frmNewChart)objfrmBase).AddVerticalLine();
            //}
            ui_mnuAddVerticalLine.Checked = !ui_mnuAddVerticalLine.Checked;
            if (m_DockManager.DocumentManager.ActiveDocument != null)
            {
                if (m_DockManager.DocumentManager.ActiveDocument.Key.Contains("Chart"))
                {
                    ((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).AddVerticalLine();
                    //((WPFChartControl)m_DockManager.DocumentManager.ActiveDocument.Client).ApplyLineStudy("VerticalLine");
                }
            }
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from VerticalMenuHandler Method");
        }

        private void HorizontalMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into HorizontalMenuHandler Method");

            //var objfrmBase = ((frmBase)ActiveMdiChild);

            //if (objfrmBase == null)
            //    return;

            //if (objfrmBase.Formkey != null && objfrmBase.Formkey.Contains("NEW_CHART"))
            //{
            //    ((frmNewChart)objfrmBase).AddHorizontalLine();
            //}
            ui_mnuAddHorizontalLine.Checked = !ui_mnuAddHorizontalLine.Checked;
            if (m_DockManager.DocumentManager.ActiveDocument != null)
            {
                if (m_DockManager.DocumentManager.ActiveDocument.Key.Contains("Chart"))
                {
                    ((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).AddHorizontalLine();
                    //((WPFChartControl)m_DockManager.DocumentManager.ActiveDocument.Client).ApplyLineStudy("HorizontalLine");
                }
            }

            //FileHandling.WriteDevelopmentLog("Main Form : Exit from HorizontalMenuHandler Method");
        }

        #endregion "  Chart Menu Handlers  "

        #region "      OBSOLETE       "

        // Obsolete
        private void ApplyHotKeySettings()
        {
            string HotKeyFile = ClsPalsaUtility.GetHotKeysFileName();

            if (File.Exists(HotKeyFile))
            {
                Stream streamRead = File.OpenRead(HotKeyFile);
                var binaryRead = new BinaryFormatter();
                _hotKeySettingsHashTable = (Hashtable)binaryRead.Deserialize(streamRead);
                streamRead.Close();
                foreach (string key in _hotKeySettingsHashTable.Keys)
                {
                    int y = ui_nmnuBar.Commands.Count;
                    foreach (string x in Enum.GetNames(typeof(CommandIDS)))
                    {
                        if (key.ToLower() == x.ToLower())
                        {
                            var cid = (int)(CommandIDS)Enum.Parse(typeof(CommandIDS), key);
                            SetHotKey(key, _hotKeySettingsHashTable[key].ToString(), FindCommand(cid));
                            break;
                        }
                    }
                }
            }
            else //Prepare Hash table for Default Hot key settings 
            {
                _hotKeySettingsHashTable = new Hashtable();
                foreach (NCommand cmd in ui_nmnuBar.Commands)
                {
                    if (cmd.Commands.Count > 0)
                    {
                        foreach (NCommand cmd1 in cmd.Commands)
                        {
                            string Keyvalue = "";
                            if (cmd1.Properties.Shortcut.ToString().ToLower() == "none")
                            {
                                Keyvalue = "[NONE]";
                            }
                            else
                            {
                                Keyvalue = cmd1.Properties.Shortcut.ToString();
                            }
                            _hotKeySettingsHashTable.Add(
                                cmd1.Properties.Text.ToUpper().Trim().Replace(" ", "_"), Keyvalue);
                            if (cmd1.Commands.Count > 0)
                            {
                                foreach (NCommand cmd2 in cmd1.Commands)
                                {
                                    string Keyvalue1 = "";
                                    if (cmd2.Properties.Shortcut.ToString().ToLower() == "none")
                                    {
                                        Keyvalue1 = "[NONE]";
                                    }
                                    else
                                    {
                                        Keyvalue1 = cmd2.Properties.Shortcut.ToString();
                                    }
                                    _hotKeySettingsHashTable.Add(
                                        cmd2.Properties.Text.ToUpper().Trim().Replace(" ", "_"), Keyvalue1);
                                }
                            }
                        }
                    }
                }
            }
        }

        // Obsolete
        private NCommand FindCommand(int cid)
        {
            NCommand c = null;
            foreach (NCommand cmd in ui_nmnuBar.Commands)
            {
                if (cmd.Properties.ID == cid)
                {
                    c = cmd;
                    break;
                }
                else if (cmd.Commands.Count > 0)
                {
                    foreach (NCommand cmd1 in cmd.Commands)
                    {
                        if (cmd1.Properties.ID == cid)
                        {
                            c = cmd1;
                            break;
                        }
                        else if (cmd1.Commands.Count > 0)
                        {
                            foreach (NCommand cmd2 in cmd1.Commands)
                            {
                                if (cmd2.Properties.ID == cid)
                                {
                                    c = cmd2;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return c;
        }

        /// <summary>
        /// Displays dialogs for user controls
        /// </summary>
        //public void DisplayDialog(CommonLibrary.UserControls.UctlBase uctlBase)
        //{
        //    var objfrmCommonForm = new frmCommonForm();
        //    objfrmCommonForm.MdiParent = this;
        //    objfrmCommonForm.Controls.Clear();
        //    objfrmCommonForm.Controls.Add(uctlBase);
        //    objfrmCommonForm.Size = new Size(uctlBase.Width + 25, uctlBase.Height + 45);
        //    uctlBase.Dock = DockStyle.Fill;
        //    objfrmCommonForm.Text = uctlBase.Title;
        //    objfrmCommonForm.Show();
        //}

        #endregion "      OBSOLETE       "
        frmBase objfrmBase = new frmBase();
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Action A = () =>
            {
                uctl.uctlCreateDemoAccount objuctlCreateDemoAccount = new uctlCreateDemoAccount();
                objfrmBase.Icon = Resources.favicon;
                objfrmBase.Controls.Clear();
                objuctlCreateDemoAccount.Dock = DockStyle.Fill;
                objuctlCreateDemoAccount.OnLoginClicked += new Action<string, string>(objuctlCreateDemoAccount_OnLoginClicked);
                objfrmBase.ClientSize = objuctlCreateDemoAccount.Size;
                objfrmBase.Title = "Create Demo Account";
                objfrmBase.MaximizeBox = false;
                objfrmBase.StartPosition = FormStartPosition.CenterParent;
                objfrmBase.Controls.Add(objuctlCreateDemoAccount);
                objfrmBase.ShowDialog();
            };
            if (InvokeRequired)
            {
                BeginInvoke(A);
            }
            else
            {
                A();
            }
        }
        private void backgroundWorker2_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            //Namo 21 March
            //Action A = () => ClsTWSContractManager.INSTANCE.GetSymbolsFromWebService(ClsPalsaUtility.GetSymbolsFilePath(), 2);
            //if (InvokeRequired)
            //{
            //    BeginInvoke(A);
            //}
            //else
            //{
            //    A();
            //}
        }
        void objuctlCreateDemoAccount_OnLoginClicked(string loginId, string password)
        {
            clsTWSOrderManagerJSON.INSTANCE.Refresh();
            clsTWSDataManagerJSON.INSTANCE.Refresh();
            string username = loginId;
            _objuctlLogin.UserCode = loginId;
            _objuctlLogin.Password = password;
            string pwd = password;
            if (_objConnectionIPs != null && _objConnectionIPs.QuotesIP.ServerIP != string.Empty)
            {
                LoadServerIps();
            }


            //clsTWSDataManagerJSON.INSTANCE.Init(username, pwd, _objConnectionIPs.QuotesIP.ServerIP,
            //                                _objConnectionIPs.QuotesIP.HostIP, _objConnectionIPs.QuotesIP.PortNo);
            //clsTWSOrderManagerJSON.INSTANCE.Init(username, pwd, _objConnectionIPs.OrderIP.ServerIP,
            //                                 _objConnectionIPs.OrderIP.HostIP, _objConnectionIPs.OrderIP.PortNo);
            //ClsTWSContractManager.INSTANCE.Init(username, pwd, _objConnectionIPs.OrderIP.ServerIP,
            //                                _objConnectionIPs.OrderIP.HostIP, _objConnectionIPs.OrderIP.PortNo);
            clsTWSDataManagerJSON.INSTANCE.Init(username, pwd, _objConnectionIPs.QuotesIP.WebSocketHostUrl);
            clsTWSOrderManagerJSON.INSTANCE.Init(username, pwd, _objConnectionIPs.OrderIP.WebSocketHostUrl);
            objfrmBase.Close();
        }

        internal void CreateNewChart(FrmScanner.ChartSelection selection)
        {
            string symbol = selection.Symbol;
            if (!IsDocumentExists(symbol + "-1 MINUTE" + "_" + "Chart"))
            {
                //WPFChartControl ctlChart = new WPFChartControl(symbol, this);

                ctlNewChart cht = new ctlNewChart();
                cht.InitChartSelection(selection.Symbol, selection.Periodicity, selection.Interval, selection.Bars);
                CreateNuiDocument(cht, selection.Symbol + "-" + selection.Interval + " " + selection.Periodicity.ToString() + "_Chart");
                //CreateNuiDocument(ctlChart, symbol + "-1 MINUTE" + "_" + "Chart");
            }

        }

        private void TrackCursor_Click(object sender, CommandEventArgs e)
        {
            TrackCursorMenuHandler();
        }

        private void cmdVerticalLine_Click(object sender, CommandEventArgs e)
        {
            //VerticalMenuHandler();
        }

        private void cmdHoriLine_Click(object sender, CommandEventArgs e)
        {
            //HorizontalMenuHandler();
        }

        private void cmdText_Click(object sender, CommandEventArgs e)
        {
            //TextMenuHandler(); Not supported
        }

        private void ZoomIn_Click(object sender, CommandEventArgs e)
        {
            ZoomInMenuHandler();
        }

        private void ZoomOut_Click(object sender, CommandEventArgs e)
        {
            ZoomOutMenuHandler();
        }

        private void M1_Click(object sender, CommandEventArgs e)
        {
            //Periodicity1MinuteMenuHandler();
        }

        private void M5_Click(object sender, CommandEventArgs e)
        {
            //Periodicity5MinuteMenuHandler();
        }

        private void M15_Click(object sender, CommandEventArgs e)
        {
            //Periodicity15MinuteMenuHandler();
        }

        private void M30_Click(object sender, CommandEventArgs e)
        {
            //Periodicity30MinuteMenuHandler();
        }

        private void H1_Click(object sender, CommandEventArgs e)
        {
            // Periodicity1HourMenuHandler();
        }

        private void D1_Click(object sender, CommandEventArgs e)
        {
            PeriodicityDailyMenuHandler();
        }

        private void W1_Click(object sender, CommandEventArgs e)
        {
            PeriodicityWeeklyMenuHandler();
        }

        private void MN_Click(object sender, CommandEventArgs e)
        {
            PeriodicityMonthlyMenuHandler();
        }

        void LineChart_Click(object sender, CommandEventArgs e)
        {
            LineChartTypeMenuHandler();
        }


        void CandleChart_Click(object sender, CommandEventArgs e)
        {
            CandleChartTypeMenuHandler();
        }

        void BarChart_Click(object sender, CommandEventArgs e)
        {
            BarChartTypeMenuHandler();
        }

        void ChartShift_Click(object sender, CommandEventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        void Equidistance_Click(object sender, CommandEventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        void Volume_Click(object sender, CommandEventArgs e)
        {
            //VolumeMenuHandler();//Not implemented yet as issue in volume panel hiding
        }

        void AutoScroll_Click(object sender, CommandEventArgs e)
        {
            AutoScrollMenuHandler();//Already in the chart
        }

        private void AutoScrollMenuHandler()
        {
            if (m_DockManager.DocumentManager.ActiveDocument != null)
            {
                if (m_DockManager.DocumentManager.ActiveDocument.Key.Contains("Chart"))
                {
                    //((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).Au();
                    //((WPFChartControl)m_DockManager.DocumentManager.ActiveDocument.Client).SetAutoScroll();
                }
            }
        }

        void TextLevel_Click(object sender, CommandEventArgs e)
        {
            if (m_DockManager.DocumentManager.ActiveDocument != null)
            {
                if (m_DockManager.DocumentManager.ActiveDocument.Key.Contains("Chart"))
                {
                    ((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).AddText();
                    //((WPFChartControl)m_DockManager.DocumentManager.ActiveDocument.Client).ApplyLineStudy("StaticText");
                }
            }
        }

        void CrossHair_Click(object sender, CommandEventArgs e)
        {
            CrossHairMenuHandler();
        }

        private void CrossHairMenuHandler()
        {
            //FileHandling.WriteDevelopmentLog("Main Form : Enter into FibonacciRetracementMenuHandler Method");

            if (m_DockManager.DocumentManager.ActiveDocument != null)
            {
                if (m_DockManager.DocumentManager.ActiveDocument.Key.Contains("Chart"))
                {
                    ((ctlNewChart)m_DockManager.DocumentManager.ActiveDocument.Client).TrackCursor();
                    //((WPFChartControl)m_DockManager.DocumentManager.ActiveDocument.Client).SetCrossHairs();
                }
            }
            //FileHandling.WriteDevelopmentLog("Main Form : Exit from FibonacciRetracementMenuHandler Method");
        }

        void Fabiconn_arc_Click(object sender, CommandEventArgs e)
        {
            FibonacciArcMenuHandler();
        }

        void Fabiconn_fan_Click(object sender, CommandEventArgs e)
        {
            FibonacciFanMenuHandler();
        }

        void Fabiconn_retracement_Click(object sender, CommandEventArgs e)
        {
            FibonacciRetracementMenuHandler();
        }

        void Gann_fan_Click(object sender, CommandEventArgs e)
        {
            GannFanMenuHandler();
        }

        void Grid_Click(object sender, CommandEventArgs e)
        {
            GridMenuHandler();
        }



        private void ui_ncmdViewExpertAdvisor_Click(object sender, CommandEventArgs e)
        {
            //string InsID = obj.Cells["ClmInstrumentId"].Value.ToString();
            //string symbol = obj.Cells["ClmContractName"].Value.ToString();

            if (!DocumentWindowIsAlreadyOpen("Expert Advisor"))
            {
                ctlAlert ctExpert = new ctlAlert(this);
                CreateNuiDocument(ctExpert, "Expert Advisor");
            }
            else
            {
                MessageBox.Show("Expert Advisor is already opened", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        internal void OpenURL(string path, string filename)
        {
            ctlDoc DOC;
            foreach (NUIDocument doc in m_DockManager.DocumentManager.Documents)
            {
                if (doc.Client.Name == "ctlDoc")
                {
                    DOC = (ctlDoc)doc.Client;
                    if (DOC.Title == filename)
                    {
                        DOC.webBrowser1.Navigate(new Uri(path));
                        m_DockManager.DocumentManager.ActiveDocument = doc;
                        return;
                    }
                }
            }
            DOC = new ctlDoc(path, filename) { Dock = DockStyle.Fill };
            NUIDocument document = new NUIDocument(filename, -1, DOC);
            document.ID = Guid.NewGuid();
            document.Key = "Trade Script Help";
            m_DockManager.DocumentManager.AddDocument(document);
        }

        internal WPFChartControl LoadRealTimeChart(FrmScanner.ChartSelection selection)
        {
            PeriodEnum pr = GetPeriodEnum(selection.Periodicity);
            //return new WPFChartControl(selection.Symbol, selection.Interval, pr, selection.Bars.ToString(), this, true);
            WPFChartControl cht = new WPFChartControl(selection.Symbol, selection.Interval, pr, this);
            CreateNuiDocument(cht, selection.Symbol + "-" + selection.Interval + " " + selection.Periodicity.ToString() + "_" + "Chart");
            return cht;
        }

        private PeriodEnum GetPeriodEnum(Periodicity periodicity)
        {
            switch (periodicity)
            {
                case Periodicity.Secondly:
                    return PeriodEnum.Minute;
                //case Periodicity.Minutely:
                //    break;
                case Periodicity.Hourly:
                    return PeriodEnum.Hour;
                case Periodicity.Daily:
                    return PeriodEnum.Day;
                case Periodicity.Weekly:
                    return PeriodEnum.Week;
                case Periodicity.Monthly:
                    return PeriodEnum.Month;
                    //default:
                    //    break;
            }
            return PeriodEnum.Minute;
        }

        internal void Speak(string str)
        {
            try
            {
                Speaker speaker = new Speaker();
                speaker.Speak(str);
            }
            catch
            {
                //ErrorService.LogError("frmMain", "Speak", ex.ToString());
            }

        }

        private void ui_ncmdViewBackTest_Click(object sender, CommandEventArgs e)
        {
            if (!DocumentWindowIsAlreadyOpen("Back Test"))
            {
                ctlBacktest ctlBack = new ctlBacktest(this);
                CreateNuiDocument(ctlBack, "Back Test");
            }
            else
            {
                MessageBox.Show("Back Test is already opened", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }




    }

    public class MarqueeLabel : Control
    {
        public string DisplayText;
        public int ScrollPixelAmount = 10;
        public System.Windows.Forms.Timer tmrScroll;
        private int position = 0;

        public MarqueeLabel()
        {
            this.tmrScroll = new System.Windows.Forms.Timer(new System.ComponentModel.Container());
            this.tmrScroll.Tick += new System.EventHandler(this.tmrScroll_Tick);
            this.Size = new System.Drawing.Size(360, 104);

            DoubleBuffered = true;
            ResizeRedraw = true;

            tmrScroll.Enabled = true;
        }

        private void tmrScroll_Tick(object sender, System.EventArgs e)
        {
            //position += ScrollPixelAmount;
            position -= ScrollPixelAmount;
            Invalidate();
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            //if (position > Width)
            //{
            //    position = -(int)e.Graphics.MeasureString(DisplayText, Font).Width;
            //}
            if (position < -Width)
            {
                position = Width;// -(int)e.Graphics.MeasureString(DisplayText, Font).Width;
            }
            e.Graphics.DrawString(DisplayText, Font, new SolidBrush(ForeColor), position, 0);
        }

    }
}