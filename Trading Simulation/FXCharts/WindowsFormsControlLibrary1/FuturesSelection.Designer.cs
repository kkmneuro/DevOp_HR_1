namespace SelectionControl
{
    partial class FuturesSelection
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Tech");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Industrial");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Resources");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Banking");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Stocks", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("US 30yr");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("US 10yr");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("US 5yr");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Bund");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Gilt");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Fixed Income", new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10});
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Oil");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Stock Indexes");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Fixed Income");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Metals");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Grains");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Softs");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Currencies");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Livestock");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Futures", new System.Windows.Forms.TreeNode[] {
            treeNode12,
            treeNode13,
            treeNode14,
            treeNode15,
            treeNode16,
            treeNode17,
            treeNode18,
            treeNode19});
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Stocs");
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("?");
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("??");
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("???");
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("????");
            System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("CDF", new System.Windows.Forms.TreeNode[] {
            treeNode21,
            treeNode22,
            treeNode23,
            treeNode24,
            treeNode25});
            System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("EUR/USD");
            System.Windows.Forms.TreeNode treeNode28 = new System.Windows.Forms.TreeNode("AUD/USD");
            System.Windows.Forms.TreeNode treeNode29 = new System.Windows.Forms.TreeNode("GBP/USD");
            System.Windows.Forms.TreeNode treeNode30 = new System.Windows.Forms.TreeNode("USD/CHF");
            System.Windows.Forms.TreeNode treeNode31 = new System.Windows.Forms.TreeNode("USD/JPY");
            System.Windows.Forms.TreeNode treeNode32 = new System.Windows.Forms.TreeNode("XAU/USD");
            System.Windows.Forms.TreeNode treeNode33 = new System.Windows.Forms.TreeNode("Majors", new System.Windows.Forms.TreeNode[] {
            treeNode27,
            treeNode28,
            treeNode29,
            treeNode30,
            treeNode31,
            treeNode32});
            System.Windows.Forms.TreeNode treeNode34 = new System.Windows.Forms.TreeNode("Minors");
            System.Windows.Forms.TreeNode treeNode35 = new System.Windows.Forms.TreeNode("Exotics");
            System.Windows.Forms.TreeNode treeNode36 = new System.Windows.Forms.TreeNode("Gold");
            System.Windows.Forms.TreeNode treeNode37 = new System.Windows.Forms.TreeNode("Silver");
            System.Windows.Forms.TreeNode treeNode38 = new System.Windows.Forms.TreeNode("FX", new System.Windows.Forms.TreeNode[] {
            treeNode33,
            treeNode34,
            treeNode35,
            treeNode36,
            treeNode37});
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.treeView5 = new System.Windows.Forms.TreeView();
            this.treeView3 = new System.Windows.Forms.TreeView();
            this.treeView4 = new System.Windows.Forms.TreeView();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(688, 289);
            this.panel1.TabIndex = 6;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 9;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1.25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1.25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1.25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1.25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19F));
            this.tableLayoutPanel1.Controls.Add(this.treeView2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.treeView5, 8, 0);
            this.tableLayoutPanel1.Controls.Add(this.treeView3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.treeView4, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.treeView1, 4, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(688, 289);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // treeView2
            // 
            this.treeView2.BackColor = System.Drawing.Color.Black;
            this.treeView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView2.Enabled = false;
            this.treeView2.ForeColor = System.Drawing.Color.White;
            this.treeView2.LineColor = System.Drawing.Color.White;
            this.treeView2.Location = new System.Drawing.Point(3, 3);
            this.treeView2.Name = "treeView2";
            treeNode1.Checked = true;
            treeNode1.Name = "Node1";
            treeNode1.Text = "Tech";
            treeNode2.Name = "Node2";
            treeNode2.Text = "Industrial";
            treeNode3.Name = "Node3";
            treeNode3.Text = "Resources";
            treeNode4.Name = "Node4";
            treeNode4.Text = "Banking";
            treeNode5.Checked = true;
            treeNode5.Name = "Node0";
            treeNode5.Text = "Stocks";
            this.treeView2.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5});
            this.treeView2.Size = new System.Drawing.Size(124, 283);
            this.treeView2.TabIndex = 1;
            // 
            // treeView5
            // 
            this.treeView5.BackColor = System.Drawing.Color.Black;
            this.treeView5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView5.Enabled = false;
            this.treeView5.ForeColor = System.Drawing.Color.White;
            this.treeView5.LineColor = System.Drawing.Color.White;
            this.treeView5.Location = new System.Drawing.Point(555, 3);
            this.treeView5.Name = "treeView5";
            treeNode6.Name = "Node1";
            treeNode6.Text = "US 30yr";
            treeNode7.Name = "Node2";
            treeNode7.Text = "US 10yr";
            treeNode8.Name = "Node3";
            treeNode8.Text = "US 5yr";
            treeNode9.Name = "Node4";
            treeNode9.Text = "Bund";
            treeNode10.Name = "Node5";
            treeNode10.Text = "Gilt";
            treeNode11.Name = "Node0";
            treeNode11.Text = "Fixed Income";
            this.treeView5.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode11});
            this.treeView5.Size = new System.Drawing.Size(130, 283);
            this.treeView5.TabIndex = 4;
            // 
            // treeView3
            // 
            this.treeView3.BackColor = System.Drawing.Color.Black;
            this.treeView3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView3.Enabled = false;
            this.treeView3.ForeColor = System.Drawing.Color.White;
            this.treeView3.LineColor = System.Drawing.Color.White;
            this.treeView3.Location = new System.Drawing.Point(141, 3);
            this.treeView3.Name = "treeView3";
            treeNode12.Name = "Node1";
            treeNode12.Text = "Oil";
            treeNode13.Name = "Node2";
            treeNode13.Text = "Stock Indexes";
            treeNode14.Name = "Node3";
            treeNode14.Text = "Fixed Income";
            treeNode15.Name = "Node4";
            treeNode15.Text = "Metals";
            treeNode16.Name = "Node5";
            treeNode16.Text = "Grains";
            treeNode17.Name = "Node6";
            treeNode17.Text = "Softs";
            treeNode18.Name = "Node7";
            treeNode18.Text = "Currencies";
            treeNode19.Name = "Node8";
            treeNode19.Text = "Livestock";
            treeNode20.Name = "Node0";
            treeNode20.Text = "Futures";
            this.treeView3.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode20});
            this.treeView3.Size = new System.Drawing.Size(124, 283);
            this.treeView3.TabIndex = 2;
            // 
            // treeView4
            // 
            this.treeView4.BackColor = System.Drawing.Color.Black;
            this.treeView4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView4.Enabled = false;
            this.treeView4.ForeColor = System.Drawing.Color.White;
            this.treeView4.LineColor = System.Drawing.Color.White;
            this.treeView4.Location = new System.Drawing.Point(417, 3);
            this.treeView4.Name = "treeView4";
            treeNode21.Name = "Node1";
            treeNode21.Text = "Stocs";
            treeNode22.Name = "Node2";
            treeNode22.Text = "?";
            treeNode23.Name = "Node3";
            treeNode23.Text = "??";
            treeNode24.Name = "Node4";
            treeNode24.Text = "???";
            treeNode25.Name = "Node5";
            treeNode25.Text = "????";
            treeNode26.Name = "Node0";
            treeNode26.Text = "CDF";
            this.treeView4.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode26});
            this.treeView4.Size = new System.Drawing.Size(124, 283);
            this.treeView4.TabIndex = 3;
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.Color.Black;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ForeColor = System.Drawing.Color.White;
            this.treeView1.LineColor = System.Drawing.Color.White;
            this.treeView1.Location = new System.Drawing.Point(279, 3);
            this.treeView1.Name = "treeView1";
            treeNode27.Checked = true;
            treeNode27.Name = "Node21";
            treeNode27.Text = "EUR/USD";
            treeNode28.Name = "Node22";
            treeNode28.Text = "AUD/USD";
            treeNode29.Name = "Node23";
            treeNode29.Text = "GBP/USD";
            treeNode30.Name = "Node24";
            treeNode30.Text = "USD/CHF";
            treeNode31.Name = "Node25";
            treeNode31.Text = "USD/JPY";
            treeNode32.Name = "Node26";
            treeNode32.Text = "XAU/USD";
            treeNode33.Name = "Node16";
            treeNode33.Text = "Majors";
            treeNode34.Name = "Node17";
            treeNode34.Text = "Minors";
            treeNode35.Name = "Node18";
            treeNode35.Text = "Exotics";
            treeNode36.Name = "Node19";
            treeNode36.Text = "Gold";
            treeNode37.Name = "Node20";
            treeNode37.Text = "Silver";
            treeNode38.Name = "Node12";
            treeNode38.Text = "FX";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode38});
            this.treeView1.Size = new System.Drawing.Size(124, 283);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            // 
            // FuturesSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "FuturesSelection";
            this.Size = new System.Drawing.Size(688, 289);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TreeView treeView2;
        private System.Windows.Forms.TreeView treeView5;
        private System.Windows.Forms.TreeView treeView3;
        private System.Windows.Forms.TreeView treeView4;
        private System.Windows.Forms.TreeView treeView1;
    }
}
