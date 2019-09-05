namespace TEST
{
    partial class TradingManager
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.AccountDataGridView1 = new System.Windows.Forms.DataGridView();
            this.Labels = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Values = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TradesDataGridView1 = new System.Windows.Forms.DataGridView();
            this.TradeIDd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TradeStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Symbol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Action = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpenPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UPL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpenTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Close = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.buyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.sellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox2 = new System.Windows.Forms.ToolStripComboBox();
            this.BuyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.SellToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox2 = new System.Windows.Forms.ToolStripTextBox();
            this.CloseToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.AccountDataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TradesDataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // AccountDataGridView1
            // 
            this.AccountDataGridView1.AllowUserToAddRows = false;
            this.AccountDataGridView1.AllowUserToDeleteRows = false;
            this.AccountDataGridView1.AllowUserToResizeRows = false;
            this.AccountDataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.AccountDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AccountDataGridView1.ColumnHeadersVisible = false;
            this.AccountDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Labels,
            this.Values});
            this.AccountDataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AccountDataGridView1.Location = new System.Drawing.Point(3, 3);
            this.AccountDataGridView1.MultiSelect = false;
            this.AccountDataGridView1.Name = "AccountDataGridView1";
            this.AccountDataGridView1.ReadOnly = true;
            this.AccountDataGridView1.RowHeadersVisible = false;
            this.AccountDataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.AccountDataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AccountDataGridView1.Size = new System.Drawing.Size(391, 224);
            this.AccountDataGridView1.TabIndex = 0;
            // 
            // Labels
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            this.Labels.DefaultCellStyle = dataGridViewCellStyle1;
            this.Labels.Frozen = true;
            this.Labels.HeaderText = "Names";
            this.Labels.Name = "Labels";
            this.Labels.ReadOnly = true;
            this.Labels.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Labels.Width = 180;
            // 
            // Values
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Values.DefaultCellStyle = dataGridViewCellStyle2;
            this.Values.HeaderText = "Values";
            this.Values.Name = "Values";
            this.Values.ReadOnly = true;
            this.Values.Width = 220;
            // 
            // TradesDataGridView1
            // 
            this.TradesDataGridView1.AllowUserToAddRows = false;
            this.TradesDataGridView1.AllowUserToDeleteRows = false;
            this.TradesDataGridView1.AllowUserToResizeRows = false;
            this.TradesDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TradesDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TradeIDd,
            this.TradeStatus,
            this.Symbol,
            this.Action,
            this.Amount,
            this.OpenPrice,
            this.Rate,
            this.UPL,
            this.OpenTime,
            this.Close});
            this.TradesDataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.TradesDataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TradesDataGridView1.Location = new System.Drawing.Point(3, 233);
            this.TradesDataGridView1.MultiSelect = false;
            this.TradesDataGridView1.Name = "TradesDataGridView1";
            this.TradesDataGridView1.ReadOnly = true;
            this.TradesDataGridView1.RowHeadersVisible = false;
            this.TradesDataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.TradesDataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TradesDataGridView1.Size = new System.Drawing.Size(391, 154);
            this.TradesDataGridView1.TabIndex = 1;
            this.TradesDataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.TradesDataGridView1_CellMouseClick);
            this.TradesDataGridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TradesDataGridView1_MouseDown);
            // 
            // TradeIDd
            // 
            this.TradeIDd.HeaderText = "TradeIDd";
            this.TradeIDd.Name = "TradeIDd";
            this.TradeIDd.ReadOnly = true;
            this.TradeIDd.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TradeIDd.Visible = false;
            // 
            // TradeStatus
            // 
            this.TradeStatus.HeaderText = "Status";
            this.TradeStatus.Name = "TradeStatus";
            this.TradeStatus.ReadOnly = true;
            this.TradeStatus.Width = 40;
            // 
            // Symbol
            // 
            this.Symbol.HeaderText = "Symbol";
            this.Symbol.Name = "Symbol";
            this.Symbol.ReadOnly = true;
            this.Symbol.Width = 70;
            // 
            // Action
            // 
            this.Action.HeaderText = "Action";
            this.Action.Name = "Action";
            this.Action.ReadOnly = true;
            this.Action.Width = 50;
            // 
            // Amount
            // 
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.Width = 50;
            // 
            // OpenPrice
            // 
            this.OpenPrice.HeaderText = "OpenPrice";
            this.OpenPrice.Name = "OpenPrice";
            this.OpenPrice.ReadOnly = true;
            this.OpenPrice.Width = 60;
            // 
            // Rate
            // 
            this.Rate.HeaderText = "Rate";
            this.Rate.Name = "Rate";
            this.Rate.ReadOnly = true;
            this.Rate.Width = 60;
            // 
            // UPL
            // 
            this.UPL.HeaderText = "UPL (USD)";
            this.UPL.Name = "UPL";
            this.UPL.ReadOnly = true;
            this.UPL.Width = 60;
            // 
            // OpenTime
            // 
            this.OpenTime.HeaderText = "Open";
            this.OpenTime.Name = "OpenTime";
            this.OpenTime.ReadOnly = true;
            this.OpenTime.Width = 50;
            // 
            // Close
            // 
            this.Close.HeaderText = "Close";
            this.Close.Name = "Close";
            this.Close.ReadOnly = true;
            this.Close.Width = 50;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buyToolStripMenuItem,
            this.sellToolStripMenuItem,
            this.BuyToolStripMenuItem1,
            this.SellToolStripMenuItem2,
            this.CloseToolStripMenuItem3});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 136);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // buyToolStripMenuItem
            // 
            this.buyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox1});
            this.buyToolStripMenuItem.Name = "buyToolStripMenuItem";
            this.buyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.buyToolStripMenuItem.Text = "Buy ...";
            this.buyToolStripMenuItem.DropDownOpening += new System.EventHandler(this.buyToolStripMenuItem_DropDownOpening);
            this.buyToolStripMenuItem.Click += new System.EventHandler(this.buyToolStripMenuItem_Click);
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox1.Items.AddRange(new object[] {
            "1000",
            "2000",
            "3000",
            "5000",
            "10000",
            "15000",
            "20000",
            "50000"});
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBox1.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            // 
            // sellToolStripMenuItem
            // 
            this.sellToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox2});
            this.sellToolStripMenuItem.Name = "sellToolStripMenuItem";
            this.sellToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.sellToolStripMenuItem.Text = "Sell ...";
            this.sellToolStripMenuItem.DropDownOpening += new System.EventHandler(this.sellToolStripMenuItem_DropDownOpening);
            // 
            // toolStripComboBox2
            // 
            this.toolStripComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox2.Items.AddRange(new object[] {
            "1000",
            "2000",
            "3000",
            "5000",
            "10000",
            "15000",
            "20000",
            "50000"});
            this.toolStripComboBox2.Name = "toolStripComboBox2";
            this.toolStripComboBox2.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBox2.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox2_SelectedIndexChanged);
            // 
            // BuyToolStripMenuItem1
            // 
            this.BuyToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1});
            this.BuyToolStripMenuItem1.Name = "BuyToolStripMenuItem1";
            this.BuyToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.BuyToolStripMenuItem1.Text = "Buy";
            this.BuyToolStripMenuItem1.DropDownOpening += new System.EventHandler(this.BuyToolStripMenuItem1_DropDownOpening);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBox1_KeyDown);
            // 
            // SellToolStripMenuItem2
            // 
            this.SellToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox2});
            this.SellToolStripMenuItem2.Name = "SellToolStripMenuItem2";
            this.SellToolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.SellToolStripMenuItem2.Text = "Sell";
            this.SellToolStripMenuItem2.DropDownOpening += new System.EventHandler(this.SellToolStripMenuItem2_DropDownOpening);
            // 
            // toolStripTextBox2
            // 
            this.toolStripTextBox2.Name = "toolStripTextBox2";
            this.toolStripTextBox2.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBox2_KeyDown);
            // 
            // CloseToolStripMenuItem3
            // 
            this.CloseToolStripMenuItem3.Name = "CloseToolStripMenuItem3";
            this.CloseToolStripMenuItem3.Size = new System.Drawing.Size(152, 22);
            this.CloseToolStripMenuItem3.Text = "Close Position";
            this.CloseToolStripMenuItem3.Click += new System.EventHandler(this.CloseToolStripMenuItem3_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.AccountDataGridView1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.TradesDataGridView1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 230F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(397, 390);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // TradingManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "TradingManager";
            this.Size = new System.Drawing.Size(397, 390);
            ((System.ComponentModel.ISupportInitialize)(this.AccountDataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TradesDataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView AccountDataGridView1;
        private System.Windows.Forms.DataGridView TradesDataGridView1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem CloseToolStripMenuItem3;
        private System.Windows.Forms.DataGridViewTextBoxColumn TradeIDd;
        private System.Windows.Forms.DataGridViewTextBoxColumn TradeStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn Symbol;
        private System.Windows.Forms.DataGridViewTextBoxColumn Action;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpenPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn UPL;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpenTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Close;
        private System.Windows.Forms.DataGridViewTextBoxColumn Labels;
        private System.Windows.Forms.DataGridViewTextBoxColumn Values;
        private System.Windows.Forms.ToolStripMenuItem buyToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStripMenuItem sellToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox2;
        private System.Windows.Forms.ToolStripMenuItem BuyToolStripMenuItem1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripMenuItem SellToolStripMenuItem2;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox2;
    }
}
