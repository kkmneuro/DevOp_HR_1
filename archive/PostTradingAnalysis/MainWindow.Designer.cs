namespace PostTradingAnalysis
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
            this.label1 = new System.Windows.Forms.Label();
            this.panelLoadData = new System.Windows.Forms.Panel();
            this.btnLoad = new System.Windows.Forms.Button();
            this.cbDates = new System.Windows.Forms.ComboBox();
            this.cbUsers = new System.Windows.Forms.ComboBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbStddevInterval = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadDataPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chartsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainChartsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stddevToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stddevAwayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.velocityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.velocityStdAwayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.signalsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.vS2015BlueTheme1 = new WeifenLuo.WinFormsUI.Docking.VS2015BlueTheme();
            this.panelLoadData.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "User:";
            // 
            // panelLoadData
            // 
            this.panelLoadData.Controls.Add(this.btnLoad);
            this.panelLoadData.Controls.Add(this.cbDates);
            this.panelLoadData.Controls.Add(this.cbUsers);
            this.panelLoadData.Controls.Add(this.btnExport);
            this.panelLoadData.Controls.Add(this.label4);
            this.panelLoadData.Controls.Add(this.cbStddevInterval);
            this.panelLoadData.Controls.Add(this.label2);
            this.panelLoadData.Controls.Add(this.label1);
            this.panelLoadData.Controls.Add(this.dtFrom);
            this.panelLoadData.Controls.Add(this.dtTo);
            this.panelLoadData.Controls.Add(this.label5);
            this.panelLoadData.Controls.Add(this.label3);
            this.panelLoadData.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLoadData.Location = new System.Drawing.Point(0, 24);
            this.panelLoadData.Name = "panelLoadData";
            this.panelLoadData.Size = new System.Drawing.Size(1041, 59);
            this.panelLoadData.TabIndex = 3;
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoad.Location = new System.Drawing.Point(977, 5);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(58, 48);
            this.btnLoad.TabIndex = 7;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // cbDates
            // 
            this.cbDates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDates.FormattingEnabled = true;
            this.cbDates.Location = new System.Drawing.Point(62, 33);
            this.cbDates.Name = "cbDates";
            this.cbDates.Size = new System.Drawing.Size(230, 21);
            this.cbDates.TabIndex = 12;
            this.cbDates.SelectedIndexChanged += new System.EventHandler(this.cbDates_SelectedIndexChanged);
            // 
            // cbUsers
            // 
            this.cbUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbUsers.FormattingEnabled = true;
            this.cbUsers.Location = new System.Drawing.Point(61, 5);
            this.cbUsers.Name = "cbUsers";
            this.cbUsers.Size = new System.Drawing.Size(464, 23);
            this.cbUsers.TabIndex = 11;
            this.cbUsers.SelectedIndexChanged += new System.EventHandler(this.cbUsers_SelectedIndexChanged);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(731, 13);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 33);
            this.btnExport.TabIndex = 10;
            this.btnExport.Text = "Export...";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(309, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Stddev interval:";
            // 
            // cbStddevInterval
            // 
            this.cbStddevInterval.DisplayMember = "3";
            this.cbStddevInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStddevInterval.FormattingEnabled = true;
            this.cbStddevInterval.Items.AddRange(new object[] {
            "15 seconds",
            "30 seconds",
            "1 minute",
            "3 minutes",
            "5 minutes"});
            this.cbStddevInterval.Location = new System.Drawing.Point(396, 33);
            this.cbStddevInterval.Name = "cbStddevInterval";
            this.cbStddevInterval.Size = new System.Drawing.Size(129, 21);
            this.cbStddevInterval.TabIndex = 8;
            this.cbStddevInterval.SelectedIndexChanged += new System.EventHandler(this.cbStddevInterval_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Date:";
            // 
            // dtFrom
            // 
            this.dtFrom.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFrom.Location = new System.Drawing.Point(574, 7);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.ShowUpDown = true;
            this.dtFrom.Size = new System.Drawing.Size(146, 20);
            this.dtFrom.TabIndex = 15;
            this.dtFrom.Value = new System.DateTime(2018, 6, 21, 0, 0, 0, 0);
            // 
            // dtTo
            // 
            this.dtTo.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTo.Location = new System.Drawing.Point(574, 33);
            this.dtTo.Name = "dtTo";
            this.dtTo.ShowUpDown = true;
            this.dtTo.Size = new System.Drawing.Size(146, 20);
            this.dtTo.TabIndex = 17;
            this.dtTo.Value = new System.DateTime(2018, 6, 21, 0, 0, 0, 0);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(535, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "From:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(545, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "To:";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.chartsToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1041, 24);
            this.menuStrip.TabIndex = 4;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadDataPanelToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadDataPanelToolStripMenuItem
            // 
            this.loadDataPanelToolStripMenuItem.Checked = true;
            this.loadDataPanelToolStripMenuItem.CheckOnClick = true;
            this.loadDataPanelToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.loadDataPanelToolStripMenuItem.Name = "loadDataPanelToolStripMenuItem";
            this.loadDataPanelToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.loadDataPanelToolStripMenuItem.Text = "Load data panel";
            this.loadDataPanelToolStripMenuItem.Click += new System.EventHandler(this.loadDataPanelToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(155, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // chartsToolStripMenuItem
            // 
            this.chartsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainChartsToolStripMenuItem,
            this.stddevToolStripMenuItem,
            this.stddevAwayToolStripMenuItem,
            this.velocityToolStripMenuItem,
            this.velocityStdAwayToolStripMenuItem,
            this.signalsToolStripMenuItem});
            this.chartsToolStripMenuItem.Name = "chartsToolStripMenuItem";
            this.chartsToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.chartsToolStripMenuItem.Text = "Charts";
            // 
            // mainChartsToolStripMenuItem
            // 
            this.mainChartsToolStripMenuItem.Name = "mainChartsToolStripMenuItem";
            this.mainChartsToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.mainChartsToolStripMenuItem.Text = "Main Charts";
            // 
            // stddevToolStripMenuItem
            // 
            this.stddevToolStripMenuItem.Name = "stddevToolStripMenuItem";
            this.stddevToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.stddevToolStripMenuItem.Text = "Stddev";
            // 
            // stddevAwayToolStripMenuItem
            // 
            this.stddevAwayToolStripMenuItem.Name = "stddevAwayToolStripMenuItem";
            this.stddevAwayToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.stddevAwayToolStripMenuItem.Text = "Stddev Away";
            // 
            // velocityToolStripMenuItem
            // 
            this.velocityToolStripMenuItem.Name = "velocityToolStripMenuItem";
            this.velocityToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.velocityToolStripMenuItem.Text = "Velocity";
            // 
            // velocityStdAwayToolStripMenuItem
            // 
            this.velocityStdAwayToolStripMenuItem.Name = "velocityStdAwayToolStripMenuItem";
            this.velocityStdAwayToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.velocityStdAwayToolStripMenuItem.Text = "Velocity StdAway";
            // 
            // signalsToolStripMenuItem
            // 
            this.signalsToolStripMenuItem.Name = "signalsToolStripMenuItem";
            this.signalsToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.signalsToolStripMenuItem.Text = "Signals";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.FileName = "AnalysisData";
            this.saveFileDialog.Filter = "Tab-delemited text files (*.tsv)|*.tsv";
            this.saveFileDialog.Title = "Export biodata to...";
            // 
            // dockPanel
            // 
            this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel.DockBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.dockPanel.Location = new System.Drawing.Point(0, 83);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Padding = new System.Windows.Forms.Padding(6);
            this.dockPanel.ShowAutoHideContentOnHover = false;
            this.dockPanel.Size = new System.Drawing.Size(1041, 596);
            this.dockPanel.TabIndex = 5;
            this.dockPanel.Theme = this.vS2015BlueTheme1;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 679);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.panelLoadData);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Post trading analysis";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.panelLoadData.ResumeLayout(false);
            this.panelLoadData.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelLoadData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private WeifenLuo.WinFormsUI.Docking.VS2015BlueTheme vS2015BlueTheme1;
        public System.Windows.Forms.MenuStrip menuStrip;
        public WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
        private System.Windows.Forms.ToolStripMenuItem loadDataPanelToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbStddevInterval;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem chartsToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem mainChartsToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem stddevAwayToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem velocityToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem signalsToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem stddevToolStripMenuItem;
        public System.Windows.Forms.ComboBox cbUsers;
        public System.Windows.Forms.ComboBox cbDates;
        public System.Windows.Forms.ToolStripMenuItem velocityStdAwayToolStripMenuItem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.DateTimePicker dtFrom;
        public System.Windows.Forms.DateTimePicker dtTo;
    }
}

