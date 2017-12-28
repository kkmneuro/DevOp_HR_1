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
            this.profitabilityDGV = new System.Windows.Forms.DataGridView();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profitabilityDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.modelCB);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(352, 31);
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
            // profitabilityDGV
            // 
            this.profitabilityDGV.AllowUserToAddRows = false;
            this.profitabilityDGV.AllowUserToDeleteRows = false;
            this.profitabilityDGV.AllowUserToOrderColumns = true;
            this.profitabilityDGV.AllowUserToResizeRows = false;
            this.profitabilityDGV.AutoGenerateColumns = false;
            this.profitabilityDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.profitabilityDGV.DataSource = this.bindingSource;
            this.profitabilityDGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.profitabilityDGV.Location = new System.Drawing.Point(0, 31);
            this.profitabilityDGV.Name = "profitabilityDGV";
            this.profitabilityDGV.ReadOnly = true;
            this.profitabilityDGV.RowHeadersVisible = false;
            this.profitabilityDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.profitabilityDGV.Size = new System.Drawing.Size(352, 231);
            this.profitabilityDGV.TabIndex = 2;
            // 
            // ProfitabilityWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 262);
            this.Controls.Add(this.profitabilityDGV);
            this.Controls.Add(this.panel1);
            this.HideOnClose = true;
            this.Name = "ProfitabilityWindow";
            this.Text = "Profitability";
            this.Load += new System.EventHandler(this.ProfitabilityWindow_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profitabilityDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.ComboBox modelCB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView profitabilityDGV;
        private System.Windows.Forms.BindingSource bindingSource;
    }
}