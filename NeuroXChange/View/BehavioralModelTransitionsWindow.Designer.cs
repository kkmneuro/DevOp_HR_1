namespace NeuroXChange.View
{
    partial class BehavioralModelTransitionsWindow
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.modelCB = new System.Windows.Forms.ComboBox();
            this.behavioralModelTransitionsDGV = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.behavioralModelTransitionsDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.modelCB);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(366, 28);
            this.panel1.TabIndex = 0;
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
            "Behavioral model 16"});
            this.modelCB.Location = new System.Drawing.Point(41, 4);
            this.modelCB.Name = "modelCB";
            this.modelCB.Size = new System.Drawing.Size(124, 21);
            this.modelCB.TabIndex = 1;
            this.modelCB.SelectedIndexChanged += new System.EventHandler(this.modelCB_SelectedIndexChanged);
            // 
            // behavioralModelTransitionsDGV
            // 
            this.behavioralModelTransitionsDGV.AllowUserToAddRows = false;
            this.behavioralModelTransitionsDGV.AllowUserToDeleteRows = false;
            this.behavioralModelTransitionsDGV.AllowUserToOrderColumns = true;
            this.behavioralModelTransitionsDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.behavioralModelTransitionsDGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.behavioralModelTransitionsDGV.Location = new System.Drawing.Point(0, 28);
            this.behavioralModelTransitionsDGV.Name = "behavioralModelTransitionsDGV";
            this.behavioralModelTransitionsDGV.ReadOnly = true;
            this.behavioralModelTransitionsDGV.RowHeadersVisible = false;
            this.behavioralModelTransitionsDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.behavioralModelTransitionsDGV.Size = new System.Drawing.Size(366, 206);
            this.behavioralModelTransitionsDGV.TabIndex = 1;
            // 
            // BehavioralModelTransitionsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 234);
            this.Controls.Add(this.behavioralModelTransitionsDGV);
            this.Controls.Add(this.panel1);
            this.Name = "BehavioralModelTransitionsWindow";
            this.Text = "BehavioralModelTransitionsWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BehavioralModelTransitionsWindow_FormClosing);
            this.Load += new System.EventHandler(this.BehavioralModelTransitionsWindow_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.behavioralModelTransitionsDGV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.ComboBox modelCB;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.DataGridView behavioralModelTransitionsDGV;
    }
}