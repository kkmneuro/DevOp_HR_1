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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emulationModeControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.profitabilityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.modeNameSL = new System.Windows.Forms.ToolStripStatusLabel();
            this.behavioralModelSL = new System.Windows.Forms.ToolStripStatusLabel();
            this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.vS2015LightTheme1 = new WeifenLuo.WinFormsUI.Docking.VS2015LightTheme();
            this.trainingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compDayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.trainingToolStripMenuItem,
            this.windowsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(397, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
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
            this.emulationModeControlToolStripMenuItem.Click += new System.EventHandler(this.windowToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
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
            this.profitabilityToolStripMenuItem});
            this.windowsToolStripMenuItem.Name = "windowsToolStripMenuItem";
            this.windowsToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.windowsToolStripMenuItem.Text = "Windows";
            // 
            // breathPacerToolStripMenuItem
            // 
            this.breathPacerToolStripMenuItem.Name = "breathPacerToolStripMenuItem";
            this.breathPacerToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.breathPacerToolStripMenuItem.Text = "Breath pacer";
            this.breathPacerToolStripMenuItem.Click += new System.EventHandler(this.windowToolStripMenuItem_Click);
            // 
            // indicatorsToolStripMenuItem
            // 
            this.indicatorsToolStripMenuItem.Name = "indicatorsToolStripMenuItem";
            this.indicatorsToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.indicatorsToolStripMenuItem.Text = "Indicators";
            this.indicatorsToolStripMenuItem.Click += new System.EventHandler(this.windowToolStripMenuItem_Click);
            // 
            // newOrderToolStripMenuItem
            // 
            this.newOrderToolStripMenuItem.Name = "newOrderToolStripMenuItem";
            this.newOrderToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.newOrderToolStripMenuItem.Text = "New order";
            this.newOrderToolStripMenuItem.Click += new System.EventHandler(this.windowToolStripMenuItem_Click);
            // 
            // behavioralModelsToolStripMenuItem
            // 
            this.behavioralModelsToolStripMenuItem.Name = "behavioralModelsToolStripMenuItem";
            this.behavioralModelsToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.behavioralModelsToolStripMenuItem.Text = "Behavioral models";
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
            this.behavioralModelTransitonsToolStripMenuItem.Click += new System.EventHandler(this.windowToolStripMenuItem_Click);
            // 
            // rawInformationToolStripMenuItem
            // 
            this.rawInformationToolStripMenuItem.Name = "rawInformationToolStripMenuItem";
            this.rawInformationToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.rawInformationToolStripMenuItem.Text = "Raw information";
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
            this.bMColorCodedWithPriceToolStripMenuItem.Click += new System.EventHandler(this.windowToolStripMenuItem_Click);
            // 
            // profitabilityToolStripMenuItem
            // 
            this.profitabilityToolStripMenuItem.Name = "profitabilityToolStripMenuItem";
            this.profitabilityToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.profitabilityToolStripMenuItem.Text = "Profitability";
            this.profitabilityToolStripMenuItem.Click += new System.EventHandler(this.windowToolStripMenuItem_Click);
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
            this.statusStrip.Location = new System.Drawing.Point(0, 320);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(397, 24);
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
            // 
            // dockPanel
            // 
            this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel.DockBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(242)))));
            this.dockPanel.Location = new System.Drawing.Point(0, 24);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Padding = new System.Windows.Forms.Padding(6);
            this.dockPanel.ShowAutoHideContentOnHover = false;
            this.dockPanel.Size = new System.Drawing.Size(397, 296);
            this.dockPanel.TabIndex = 7;
            this.dockPanel.Theme = this.vS2015LightTheme1;
            // 
            // trainingToolStripMenuItem
            // 
            this.trainingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compDayToolStripMenuItem});
            this.trainingToolStripMenuItem.Name = "trainingToolStripMenuItem";
            this.trainingToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.trainingToolStripMenuItem.Text = "Training";
            // 
            // compDayToolStripMenuItem
            // 
            this.compDayToolStripMenuItem.Name = "compDayToolStripMenuItem";
            this.compDayToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.compDayToolStripMenuItem.Text = "Comp day";
            this.compDayToolStripMenuItem.Click += new System.EventHandler(this.windowToolStripMenuItem_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 344);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "NeuroTrader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
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
        private WeifenLuo.WinFormsUI.Docking.VS2015LightTheme vS2015LightTheme1;
        private System.Windows.Forms.ToolStripMenuItem bMColorCodedWithPriceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emulationModeControlToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem profitabilityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trainingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compDayToolStripMenuItem;
    }
}

