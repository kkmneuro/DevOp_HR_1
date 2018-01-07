namespace NeuroXChange.View
{
    partial class ProfitabilityWindow
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.modelCB = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openOrdersDGV = new System.Windows.Forms.DataGridView();
            this.openOrdersBS = new System.Windows.Forms.BindingSource(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.closedOrdersBS = new System.Windows.Forms.BindingSource(this.components);
            this.closedOrdersDGV = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.openOrdersDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.openOrdersBS)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.closedOrdersBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.closedOrdersDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.modelCB);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(452, 31);
            this.panel1.TabIndex = 1;
            // 
            // modelCB
            // 
            this.modelCB.FormattingEnabled = true;
            this.modelCB.Items.AddRange(new object[] {
            "Behavioral model 1",
            "Behavioral model 2",
            "Behavioral model 3",
            "Behavioral model 4",
            "Behavioral model 5",
            "Behavioral model 6",
            "Behavioral model 7",
            "Behavioral model 8",
            "Behavioral model 9",
            "Behavioral model 10",
            "Behavioral model 11",
            "Behavioral model 12",
            "Behavioral model 13",
            "Behavioral model 14",
            "Behavioral model 15",
            "Behavioral model 16",
            "Manual trade model"});
            this.modelCB.Location = new System.Drawing.Point(41, 4);
            this.modelCB.Name = "modelCB";
            this.modelCB.Size = new System.Drawing.Size(124, 21);
            this.modelCB.TabIndex = 1;
            this.modelCB.SelectedIndexChanged += new System.EventHandler(this.modelCB_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Model:";
            // 
            // openOrdersDGV
            // 
            this.openOrdersDGV.AllowUserToAddRows = false;
            this.openOrdersDGV.AllowUserToDeleteRows = false;
            this.openOrdersDGV.AllowUserToOrderColumns = true;
            this.openOrdersDGV.AllowUserToResizeRows = false;
            this.openOrdersDGV.AutoGenerateColumns = false;
            this.openOrdersDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.openOrdersDGV.DataSource = this.openOrdersBS;
            this.openOrdersDGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openOrdersDGV.Location = new System.Drawing.Point(3, 3);
            this.openOrdersDGV.Name = "openOrdersDGV";
            this.openOrdersDGV.ReadOnly = true;
            this.openOrdersDGV.RowHeadersVisible = false;
            this.openOrdersDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.openOrdersDGV.Size = new System.Drawing.Size(438, 317);
            this.openOrdersDGV.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 31);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(452, 349);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.openOrdersDGV);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(444, 323);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "OpenOrders";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.closedOrdersDGV);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(444, 323);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "ClosedOrders";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // closedOrdersDGV
            // 
            this.closedOrdersDGV.AllowUserToAddRows = false;
            this.closedOrdersDGV.AllowUserToDeleteRows = false;
            this.closedOrdersDGV.AllowUserToOrderColumns = true;
            this.closedOrdersDGV.AllowUserToResizeRows = false;
            this.closedOrdersDGV.AutoGenerateColumns = false;
            this.closedOrdersDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.closedOrdersDGV.DataSource = this.closedOrdersBS;
            this.closedOrdersDGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.closedOrdersDGV.Location = new System.Drawing.Point(3, 3);
            this.closedOrdersDGV.Name = "closedOrdersDGV";
            this.closedOrdersDGV.ReadOnly = true;
            this.closedOrdersDGV.RowHeadersVisible = false;
            this.closedOrdersDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.closedOrdersDGV.Size = new System.Drawing.Size(438, 317);
            this.closedOrdersDGV.TabIndex = 0;
            // 
            // ProfitabilityWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 380);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.HideOnClose = true;
            this.Name = "ProfitabilityWindow";
            this.Text = "Profitability";
            this.Load += new System.EventHandler(this.ProfitabilityWindow_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.openOrdersDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.openOrdersBS)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.closedOrdersBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.closedOrdersDGV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.ComboBox modelCB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView openOrdersDGV;
        private System.Windows.Forms.BindingSource openOrdersBS;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView closedOrdersDGV;
        private System.Windows.Forms.BindingSource closedOrdersBS;
    }
}