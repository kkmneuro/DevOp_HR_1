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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.modelCB = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.confirmationFilledCB = new System.Windows.Forms.CheckBox();
            this.executeOrderCB = new System.Windows.Forms.CheckBox();
            this.directionConfirmedCB = new System.Windows.Forms.CheckBox();
            this.preactivationCB = new System.Windows.Forms.CheckBox();
            this.readyToTradeCB = new System.Windows.Forms.CheckBox();
            this.initialStateCB = new System.Windows.Forms.CheckBox();
            this.behavioralModelTransitionsDGV = new System.Windows.Forms.DataGridView();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.dataUpdaterTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.behavioralModelTransitionsDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.modelCB);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(588, 31);
            this.panel1.TabIndex = 0;
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
            // confirmationFilledCB
            // 
            this.confirmationFilledCB.AutoSize = true;
            this.confirmationFilledCB.Checked = true;
            this.confirmationFilledCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.confirmationFilledCB.Location = new System.Drawing.Point(3, 26);
            this.confirmationFilledCB.Name = "confirmationFilledCB";
            this.confirmationFilledCB.Size = new System.Drawing.Size(108, 17);
            this.confirmationFilledCB.TabIndex = 7;
            this.confirmationFilledCB.Tag = "";
            this.confirmationFilledCB.Text = "Confirmation filled";
            this.confirmationFilledCB.UseVisualStyleBackColor = true;
            this.confirmationFilledCB.CheckedChanged += new System.EventHandler(this.stateCheckbox_CheckedChanged);
            // 
            // executeOrderCB
            // 
            this.executeOrderCB.AutoSize = true;
            this.executeOrderCB.Checked = true;
            this.executeOrderCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.executeOrderCB.Location = new System.Drawing.Point(404, 3);
            this.executeOrderCB.Name = "executeOrderCB";
            this.executeOrderCB.Size = new System.Drawing.Size(92, 17);
            this.executeOrderCB.TabIndex = 6;
            this.executeOrderCB.Tag = "";
            this.executeOrderCB.Text = "Execute order";
            this.executeOrderCB.UseVisualStyleBackColor = true;
            this.executeOrderCB.CheckedChanged += new System.EventHandler(this.stateCheckbox_CheckedChanged);
            // 
            // directionConfirmedCB
            // 
            this.directionConfirmedCB.AutoSize = true;
            this.directionConfirmedCB.Checked = true;
            this.directionConfirmedCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.directionConfirmedCB.Location = new System.Drawing.Point(281, 3);
            this.directionConfirmedCB.Name = "directionConfirmedCB";
            this.directionConfirmedCB.Size = new System.Drawing.Size(117, 17);
            this.directionConfirmedCB.TabIndex = 5;
            this.directionConfirmedCB.Tag = "";
            this.directionConfirmedCB.Text = "Direction confirmed";
            this.directionConfirmedCB.UseVisualStyleBackColor = true;
            this.directionConfirmedCB.CheckedChanged += new System.EventHandler(this.stateCheckbox_CheckedChanged);
            // 
            // preactivationCB
            // 
            this.preactivationCB.AutoSize = true;
            this.preactivationCB.Checked = true;
            this.preactivationCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.preactivationCB.Location = new System.Drawing.Point(187, 3);
            this.preactivationCB.Name = "preactivationCB";
            this.preactivationCB.Size = new System.Drawing.Size(88, 17);
            this.preactivationCB.TabIndex = 4;
            this.preactivationCB.Tag = "";
            this.preactivationCB.Text = "Preactivation";
            this.preactivationCB.UseVisualStyleBackColor = true;
            this.preactivationCB.CheckedChanged += new System.EventHandler(this.stateCheckbox_CheckedChanged);
            // 
            // readyToTradeCB
            // 
            this.readyToTradeCB.AutoSize = true;
            this.readyToTradeCB.Checked = true;
            this.readyToTradeCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.readyToTradeCB.Location = new System.Drawing.Point(85, 3);
            this.readyToTradeCB.Name = "readyToTradeCB";
            this.readyToTradeCB.Size = new System.Drawing.Size(96, 17);
            this.readyToTradeCB.TabIndex = 3;
            this.readyToTradeCB.Tag = "";
            this.readyToTradeCB.Text = "Ready to trade";
            this.readyToTradeCB.UseVisualStyleBackColor = true;
            this.readyToTradeCB.CheckedChanged += new System.EventHandler(this.stateCheckbox_CheckedChanged);
            // 
            // initialStateCB
            // 
            this.initialStateCB.AutoSize = true;
            this.initialStateCB.Checked = true;
            this.initialStateCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.initialStateCB.Location = new System.Drawing.Point(3, 3);
            this.initialStateCB.Name = "initialStateCB";
            this.initialStateCB.Size = new System.Drawing.Size(76, 17);
            this.initialStateCB.TabIndex = 2;
            this.initialStateCB.Tag = "";
            this.initialStateCB.Text = "Initial state";
            this.initialStateCB.UseVisualStyleBackColor = true;
            this.initialStateCB.CheckedChanged += new System.EventHandler(this.stateCheckbox_CheckedChanged);
            // 
            // behavioralModelTransitionsDGV
            // 
            this.behavioralModelTransitionsDGV.AllowUserToAddRows = false;
            this.behavioralModelTransitionsDGV.AllowUserToDeleteRows = false;
            this.behavioralModelTransitionsDGV.AllowUserToOrderColumns = true;
            this.behavioralModelTransitionsDGV.AutoGenerateColumns = false;
            this.behavioralModelTransitionsDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.behavioralModelTransitionsDGV.DataSource = this.bindingSource;
            this.behavioralModelTransitionsDGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.behavioralModelTransitionsDGV.Location = new System.Drawing.Point(0, 77);
            this.behavioralModelTransitionsDGV.Name = "behavioralModelTransitionsDGV";
            this.behavioralModelTransitionsDGV.ReadOnly = true;
            this.behavioralModelTransitionsDGV.RowHeadersVisible = false;
            this.behavioralModelTransitionsDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.behavioralModelTransitionsDGV.Size = new System.Drawing.Size(588, 156);
            this.behavioralModelTransitionsDGV.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.initialStateCB);
            this.flowLayoutPanel1.Controls.Add(this.readyToTradeCB);
            this.flowLayoutPanel1.Controls.Add(this.preactivationCB);
            this.flowLayoutPanel1.Controls.Add(this.directionConfirmedCB);
            this.flowLayoutPanel1.Controls.Add(this.executeOrderCB);
            this.flowLayoutPanel1.Controls.Add(this.confirmationFilledCB);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 31);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(588, 46);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // dataUpdaterTimer
            // 
            this.dataUpdaterTimer.Enabled = true;
            this.dataUpdaterTimer.Tick += new System.EventHandler(this.dataUpdaterTimer_Tick);
            // 
            // BehavioralModelTransitionsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 233);
            this.Controls.Add(this.behavioralModelTransitionsDGV);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.HideOnClose = true;
            this.Name = "BehavioralModelTransitionsWindow";
            this.Text = "BehavioralModelTransitionsWindow";
            this.Load += new System.EventHandler(this.BehavioralModelTransitionsWindow_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.behavioralModelTransitionsDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.ComboBox modelCB;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.DataGridView behavioralModelTransitionsDGV;
        private System.Windows.Forms.CheckBox directionConfirmedCB;
        private System.Windows.Forms.CheckBox preactivationCB;
        private System.Windows.Forms.CheckBox readyToTradeCB;
        private System.Windows.Forms.CheckBox initialStateCB;
        private System.Windows.Forms.CheckBox confirmationFilledCB;
        private System.Windows.Forms.CheckBox executeOrderCB;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.BindingSource bindingSource;
        private System.Windows.Forms.Timer dataUpdaterTimer;
    }
}