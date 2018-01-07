namespace NeuroXChange.View
{
    partial class OrdersWindow
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
            this.closedOrdersDGV = new System.Windows.Forms.DataGridView();
            this.closedOrdersBS = new System.Windows.Forms.BindingSource(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.openOrdersDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.openOrdersBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.closedOrdersDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.closedOrdersBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.modelCB);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(464, 31);
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
            this.openOrdersDGV.Location = new System.Drawing.Point(0, 0);
            this.openOrdersDGV.Name = "openOrdersDGV";
            this.openOrdersDGV.ReadOnly = true;
            this.openOrdersDGV.RowHeadersVisible = false;
            this.openOrdersDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.openOrdersDGV.Size = new System.Drawing.Size(464, 60);
            this.openOrdersDGV.TabIndex = 2;
            this.openOrdersDGV.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.openOrdersDGV_CellFormatting);
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
            this.closedOrdersDGV.Location = new System.Drawing.Point(0, 0);
            this.closedOrdersDGV.Name = "closedOrdersDGV";
            this.closedOrdersDGV.ReadOnly = true;
            this.closedOrdersDGV.RowHeadersVisible = false;
            this.closedOrdersDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.closedOrdersDGV.Size = new System.Drawing.Size(464, 171);
            this.closedOrdersDGV.TabIndex = 0;
            this.closedOrdersDGV.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.closedOrdersDGV_CellFormatting);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 31);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.openOrdersDGV);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.closedOrdersDGV);
            this.splitContainer1.Size = new System.Drawing.Size(464, 235);
            this.splitContainer1.SplitterDistance = 60;
            this.splitContainer1.TabIndex = 3;
            // 
            // OrdersWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 266);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.HideOnClose = true;
            this.Name = "OrdersWindow";
            this.Text = "Orders";
            this.Load += new System.EventHandler(this.ProfitabilityWindow_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.openOrdersDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.openOrdersBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.closedOrdersDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.closedOrdersBS)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.ComboBox modelCB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView openOrdersDGV;
        private System.Windows.Forms.BindingSource openOrdersBS;
        private System.Windows.Forms.DataGridView closedOrdersDGV;
        private System.Windows.Forms.BindingSource closedOrdersBS;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}