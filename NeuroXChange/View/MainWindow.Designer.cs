namespace NeuroXChange
{
    partial class MainWindow
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
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emulationModeControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItemSimplestMode = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItemSimplestMode = new System.Windows.Forms.ToolStripMenuItem();
            this.trainingToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.compDayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.marketSentimentSurveyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.breathPacerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indicatorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.behavioralModelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.behavioralModelTransitonsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rawInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chartsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bMColorCodedWithPriceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ordersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applicationControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tradeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trainingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.symbolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsToolStripMenuItemSimplestMode = new System.Windows.Forms.ToolStripMenuItem();
            this.breathPacerToolStripMenuItemSimplestMode = new System.Windows.Forms.ToolStripMenuItem();
            this.indicatorsToolStripMenuItemSimplestMode = new System.Windows.Forms.ToolStripMenuItem();
            this.newOrderToolStripMenuItemSimplestMode = new System.Windows.Forms.ToolStripMenuItem();
            this.chartsToolStripMenuItemSimplestMode = new System.Windows.Forms.ToolStripMenuItem();
            this.applicationControlToolStripMenuItemSimplestMode = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.modeNameSL = new System.Windows.Forms.ToolStripStatusLabel();
            this.behavioralModelSL = new System.Windows.Forms.ToolStripStatusLabel();
            this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.vS2015BlueTheme1 = new WeifenLuo.WinFormsUI.Docking.VS2015BlueTheme();
            this.timerStartRecording = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.mainMenuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.fileToolStripMenuItemSimplestMode,
            this.trainingToolStripMenuItem2,
            this.windowsToolStripMenuItem,
            this.windowsToolStripMenuItemSimplestMode,
            this.helpToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(342, 24);
            this.mainMenuStrip.TabIndex = 6;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.emulationModeControlToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // emulationModeControlToolStripMenuItem
            // 
            this.emulationModeControlToolStripMenuItem.Name = "emulationModeControlToolStripMenuItem";
            this.emulationModeControlToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.emulationModeControlToolStripMenuItem.Text = "Emulation mode control";
            this.emulationModeControlToolStripMenuItem.Visible = false;
            this.emulationModeControlToolStripMenuItem.Click += new System.EventHandler(this.windowToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // fileToolStripMenuItemSimplestMode
            // 
            this.fileToolStripMenuItemSimplestMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItemSimplestMode});
            this.fileToolStripMenuItemSimplestMode.Name = "fileToolStripMenuItemSimplestMode";
            this.fileToolStripMenuItemSimplestMode.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItemSimplestMode.Text = "File";
            // 
            // exitToolStripMenuItemSimplestMode
            // 
            this.exitToolStripMenuItemSimplestMode.Name = "exitToolStripMenuItemSimplestMode";
            this.exitToolStripMenuItemSimplestMode.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItemSimplestMode.Text = "Exit";
            this.exitToolStripMenuItemSimplestMode.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // trainingToolStripMenuItem2
            // 
            this.trainingToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compDayToolStripMenuItem,
            this.marketSentimentSurveyToolStripMenuItem});
            this.trainingToolStripMenuItem2.Name = "trainingToolStripMenuItem2";
            this.trainingToolStripMenuItem2.Size = new System.Drawing.Size(63, 20);
            this.trainingToolStripMenuItem2.Text = "Training";
            this.trainingToolStripMenuItem2.Visible = false;
            // 
            // compDayToolStripMenuItem
            // 
            this.compDayToolStripMenuItem.Name = "compDayToolStripMenuItem";
            this.compDayToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.compDayToolStripMenuItem.Text = "Comp day";
            this.compDayToolStripMenuItem.Click += new System.EventHandler(this.windowToolStripMenuItem_Click);
            // 
            // marketSentimentSurveyToolStripMenuItem
            // 
            this.marketSentimentSurveyToolStripMenuItem.Name = "marketSentimentSurveyToolStripMenuItem";
            this.marketSentimentSurveyToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.marketSentimentSurveyToolStripMenuItem.Text = "Market sentiment - survey";
            this.marketSentimentSurveyToolStripMenuItem.Click += new System.EventHandler(this.windowToolStripMenuItem_Click);
            // 
            // windowsToolStripMenuItem
            // 
            this.windowsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.breathPacerToolStripMenuItem,
            this.indicatorsToolStripMenuItem,
            this.newOrderToolStripMenuItem,
            this.behavioralModelsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.behavioralModelTransitonsToolStripMenuItem,
            this.rawInformationToolStripMenuItem,
            this.chartsToolStripMenuItem,
            this.bMColorCodedWithPriceToolStripMenuItem,
            this.ordersToolStripMenuItem,
            this.applicationControlToolStripMenuItem,
            this.tradeToolStripMenuItem,
            this.trainingToolStripMenuItem,
            this.symbolsToolStripMenuItem});
            this.windowsToolStripMenuItem.Name = "windowsToolStripMenuItem";
            this.windowsToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.windowsToolStripMenuItem.Text = "Windows";
            // 
            // breathPacerToolStripMenuItem
            // 
            this.breathPacerToolStripMenuItem.Name = "breathPacerToolStripMenuItem";
            this.breathPacerToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.breathPacerToolStripMenuItem.Text = "Breath pacer";
            this.breathPacerToolStripMenuItem.Visible = false;
            this.breathPacerToolStripMenuItem.Click += new System.EventHandler(this.windowToolStripMenuItem_Click);
            // 
            // indicatorsToolStripMenuItem
            // 
            this.indicatorsToolStripMenuItem.Name = "indicatorsToolStripMenuItem";
            this.indicatorsToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.indicatorsToolStripMenuItem.Text = "Indicators";
            this.indicatorsToolStripMenuItem.Visible = false;
            this.indicatorsToolStripMenuItem.Click += new System.EventHandler(this.windowToolStripMenuItem_Click);
            // 
            // newOrderToolStripMenuItem
            // 
            this.newOrderToolStripMenuItem.Name = "newOrderToolStripMenuItem";
            this.newOrderToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.newOrderToolStripMenuItem.Text = "New order";
            this.newOrderToolStripMenuItem.Visible = false;
            this.newOrderToolStripMenuItem.Click += new System.EventHandler(this.windowToolStripMenuItem_Click);
            // 
            // behavioralModelsToolStripMenuItem
            // 
            this.behavioralModelsToolStripMenuItem.Name = "behavioralModelsToolStripMenuItem";
            this.behavioralModelsToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.behavioralModelsToolStripMenuItem.Text = "Behavioral models";
            this.behavioralModelsToolStripMenuItem.Visible = false;
            this.behavioralModelsToolStripMenuItem.Click += new System.EventHandler(this.windowToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(218, 6);
            // 
            // behavioralModelTransitonsToolStripMenuItem
            // 
            this.behavioralModelTransitonsToolStripMenuItem.Name = "behavioralModelTransitonsToolStripMenuItem";
            this.behavioralModelTransitonsToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.behavioralModelTransitonsToolStripMenuItem.Text = "Behavioral model transitons";
            this.behavioralModelTransitonsToolStripMenuItem.Visible = false;
            this.behavioralModelTransitonsToolStripMenuItem.Click += new System.EventHandler(this.windowToolStripMenuItem_Click);
            // 
            // rawInformationToolStripMenuItem
            // 
            this.rawInformationToolStripMenuItem.Name = "rawInformationToolStripMenuItem";
            this.rawInformationToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.rawInformationToolStripMenuItem.Text = "Raw information";
            this.rawInformationToolStripMenuItem.Visible = false;
            this.rawInformationToolStripMenuItem.Click += new System.EventHandler(this.windowToolStripMenuItem_Click);
            // 
            // chartsToolStripMenuItem
            // 
            this.chartsToolStripMenuItem.Name = "chartsToolStripMenuItem";
            this.chartsToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.chartsToolStripMenuItem.Text = "Charts";
            this.chartsToolStripMenuItem.Click += new System.EventHandler(this.windowToolStripMenuItem_Click);
            // 
            // bMColorCodedWithPriceToolStripMenuItem
            // 
            this.bMColorCodedWithPriceToolStripMenuItem.Name = "bMColorCodedWithPriceToolStripMenuItem";
            this.bMColorCodedWithPriceToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.bMColorCodedWithPriceToolStripMenuItem.Text = "BM color coded with price";
            this.bMColorCodedWithPriceToolStripMenuItem.Visible = false;
            this.bMColorCodedWithPriceToolStripMenuItem.Click += new System.EventHandler(this.windowToolStripMenuItem_Click);
            // 
            // ordersToolStripMenuItem
            // 
            this.ordersToolStripMenuItem.Name = "ordersToolStripMenuItem";
            this.ordersToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.ordersToolStripMenuItem.Text = "Orders";
            this.ordersToolStripMenuItem.Visible = false;
            this.ordersToolStripMenuItem.Click += new System.EventHandler(this.windowToolStripMenuItem_Click);
            // 
            // applicationControlToolStripMenuItem
            // 
            this.applicationControlToolStripMenuItem.Name = "applicationControlToolStripMenuItem";
            this.applicationControlToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.applicationControlToolStripMenuItem.Text = "Application control";
            this.applicationControlToolStripMenuItem.Click += new System.EventHandler(this.windowToolStripMenuItem_Click);
            // 
            // tradeToolStripMenuItem
            // 
            this.tradeToolStripMenuItem.Name = "tradeToolStripMenuItem";
            this.tradeToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.tradeToolStripMenuItem.Text = "Trade";
            this.tradeToolStripMenuItem.Visible = false;
            // 
            // trainingToolStripMenuItem
            // 
            this.trainingToolStripMenuItem.Name = "trainingToolStripMenuItem";
            this.trainingToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.trainingToolStripMenuItem.Text = "Training";
            this.trainingToolStripMenuItem.Visible = false;
            this.trainingToolStripMenuItem.Click += new System.EventHandler(this.windowToolStripMenuItem_Click);
            // 
            // symbolsToolStripMenuItem
            // 
            this.symbolsToolStripMenuItem.Name = "symbolsToolStripMenuItem";
            this.symbolsToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.symbolsToolStripMenuItem.Text = "Symbols";
            this.symbolsToolStripMenuItem.Visible = false;
            this.symbolsToolStripMenuItem.Click += new System.EventHandler(this.windowToolStripMenuItem_Click);
            // 
            // windowsToolStripMenuItemSimplestMode
            // 
            this.windowsToolStripMenuItemSimplestMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.breathPacerToolStripMenuItemSimplestMode,
            this.indicatorsToolStripMenuItemSimplestMode,
            this.newOrderToolStripMenuItemSimplestMode,
            this.chartsToolStripMenuItemSimplestMode,
            this.applicationControlToolStripMenuItemSimplestMode});
            this.windowsToolStripMenuItemSimplestMode.Name = "windowsToolStripMenuItemSimplestMode";
            this.windowsToolStripMenuItemSimplestMode.Size = new System.Drawing.Size(68, 20);
            this.windowsToolStripMenuItemSimplestMode.Text = "Windows";
            // 
            // breathPacerToolStripMenuItemSimplestMode
            // 
            this.breathPacerToolStripMenuItemSimplestMode.Name = "breathPacerToolStripMenuItemSimplestMode";
            this.breathPacerToolStripMenuItemSimplestMode.Size = new System.Drawing.Size(176, 22);
            this.breathPacerToolStripMenuItemSimplestMode.Text = "Breath pacer";
            this.breathPacerToolStripMenuItemSimplestMode.Click += new System.EventHandler(this.windowToolStripMenuItem_Click);
            // 
            // indicatorsToolStripMenuItemSimplestMode
            // 
            this.indicatorsToolStripMenuItemSimplestMode.Name = "indicatorsToolStripMenuItemSimplestMode";
            this.indicatorsToolStripMenuItemSimplestMode.Size = new System.Drawing.Size(176, 22);
            this.indicatorsToolStripMenuItemSimplestMode.Text = "Indicators";
            this.indicatorsToolStripMenuItemSimplestMode.Click += new System.EventHandler(this.windowToolStripMenuItem_Click);
            // 
            // newOrderToolStripMenuItemSimplestMode
            // 
            this.newOrderToolStripMenuItemSimplestMode.Name = "newOrderToolStripMenuItemSimplestMode";
            this.newOrderToolStripMenuItemSimplestMode.Size = new System.Drawing.Size(176, 22);
            this.newOrderToolStripMenuItemSimplestMode.Text = "New order";
            this.newOrderToolStripMenuItemSimplestMode.Click += new System.EventHandler(this.windowToolStripMenuItem_Click);
            // 
            // chartsToolStripMenuItemSimplestMode
            // 
            this.chartsToolStripMenuItemSimplestMode.Name = "chartsToolStripMenuItemSimplestMode";
            this.chartsToolStripMenuItemSimplestMode.Size = new System.Drawing.Size(176, 22);
            this.chartsToolStripMenuItemSimplestMode.Text = "Charts";
            this.chartsToolStripMenuItemSimplestMode.Click += new System.EventHandler(this.windowToolStripMenuItem_Click);
            // 
            // applicationControlToolStripMenuItemSimplestMode
            // 
            this.applicationControlToolStripMenuItemSimplestMode.Name = "applicationControlToolStripMenuItemSimplestMode";
            this.applicationControlToolStripMenuItemSimplestMode.Size = new System.Drawing.Size(176, 22);
            this.applicationControlToolStripMenuItemSimplestMode.Text = "Application control";
            this.applicationControlToolStripMenuItemSimplestMode.Click += new System.EventHandler(this.windowToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modeNameSL,
            this.behavioralModelSL});
            this.statusStrip.Location = new System.Drawing.Point(0, 237);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(342, 24);
            this.statusStrip.TabIndex = 10;
            this.statusStrip.Text = "statusStrip1";
            // 
            // modeNameSL
            // 
            this.modeNameSL.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.modeNameSL.Name = "modeNameSL";
            this.modeNameSL.Size = new System.Drawing.Size(96, 19);
            this.modeNameSL.Text = "Mode: real-time";
            // 
            // behavioralModelSL
            // 
            this.behavioralModelSL.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.behavioralModelSL.Name = "behavioralModelSL";
            this.behavioralModelSL.Size = new System.Drawing.Size(115, 19);
            this.behavioralModelSL.Text = "Behavioral model: 1";
            this.behavioralModelSL.Click += new System.EventHandler(this.behavioralModelSL_Click);
            // 
            // dockPanel
            // 
            this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel.DockBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.dockPanel.Location = new System.Drawing.Point(0, 24);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Padding = new System.Windows.Forms.Padding(6);
            this.dockPanel.ShowAutoHideContentOnHover = false;
            this.dockPanel.Size = new System.Drawing.Size(342, 213);
            this.dockPanel.TabIndex = 7;
            this.dockPanel.Theme = this.vS2015BlueTheme1;
            // 
            // timerStartRecording
            // 
            this.timerStartRecording.Interval = 5000;
            this.timerStartRecording.Tick += new System.EventHandler(this.timerStartRecording_Tick);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(342, 261);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.mainMenuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainWindow";
            this.Text = "NeuroTrader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chartsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rawInformationToolStripMenuItem;
        public WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
        private System.Windows.Forms.ToolStripMenuItem newOrderToolStripMenuItem;
        public System.Windows.Forms.StatusStrip statusStrip;
        public System.Windows.Forms.ToolStripStatusLabel modeNameSL;
        private System.Windows.Forms.ToolStripMenuItem breathPacerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indicatorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem behavioralModelsToolStripMenuItem;
        public System.Windows.Forms.ToolStripStatusLabel behavioralModelSL;
        private System.Windows.Forms.ToolStripMenuItem behavioralModelTransitonsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bMColorCodedWithPriceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emulationModeControlToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ordersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trainingToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem compDayToolStripMenuItem;
        private WeifenLuo.WinFormsUI.Docking.VS2015BlueTheme vS2015BlueTheme1;
        private System.Windows.Forms.ToolStripMenuItem marketSentimentSurveyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItemSimplestMode;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItemSimplestMode;
        private System.Windows.Forms.ToolStripMenuItem windowsToolStripMenuItemSimplestMode;
        private System.Windows.Forms.ToolStripMenuItem breathPacerToolStripMenuItemSimplestMode;
        private System.Windows.Forms.ToolStripMenuItem indicatorsToolStripMenuItemSimplestMode;
        private System.Windows.Forms.ToolStripMenuItem chartsToolStripMenuItemSimplestMode;
        private System.Windows.Forms.ToolStripMenuItem applicationControlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applicationControlToolStripMenuItemSimplestMode;
        private System.Windows.Forms.ToolStripMenuItem newOrderToolStripMenuItemSimplestMode;
        private System.Windows.Forms.ToolStripMenuItem tradeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trainingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem symbolsToolStripMenuItem;
        private System.Windows.Forms.Timer timerStartRecording;
        private System.Windows.Forms.Timer timer1;
    }
}

