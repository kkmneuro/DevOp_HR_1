namespace NeuroXChange.View
{
    partial class BehavioralModelsWindow
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.behavioralModelsDataSet = new System.Data.DataSet();
            this.dataTable1 = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataColumn5 = new System.Data.DataColumn();
            this.dataColumn6 = new System.Data.DataColumn();
            this.dataColumn7 = new System.Data.DataColumn();
            this.dataColumn8 = new System.Data.DataColumn();
            this.modelDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.initialStateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.preactivationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.activationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.securityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inPositionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tradesTodayDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.profitabilityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.behavioralModelsDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AutoGenerateColumns = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.modelDataGridViewTextBoxColumn,
            this.initialStateDataGridViewTextBoxColumn,
            this.preactivationDataGridViewTextBoxColumn,
            this.activationDataGridViewTextBoxColumn,
            this.securityDataGridViewTextBoxColumn,
            this.inPositionDataGridViewTextBoxColumn,
            this.tradesTodayDataGridViewTextBoxColumn,
            this.profitabilityDataGridViewTextBoxColumn});
            this.dataGridView.DataMember = "BehavioralModels";
            this.dataGridView.DataSource = this.behavioralModelsDataSet;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(688, 110);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView_CellFormatting);
            // 
            // behavioralModelsDataSet
            // 
            this.behavioralModelsDataSet.DataSetName = "NewDataSet";
            this.behavioralModelsDataSet.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable1});
            // 
            // dataTable1
            // 
            this.dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3,
            this.dataColumn4,
            this.dataColumn5,
            this.dataColumn6,
            this.dataColumn7,
            this.dataColumn8});
            this.dataTable1.TableName = "BehavioralModels";
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "Model";
            // 
            // dataColumn2
            // 
            this.dataColumn2.Caption = "Initial state";
            this.dataColumn2.ColumnName = "Initial state";
            // 
            // dataColumn3
            // 
            this.dataColumn3.ColumnName = "Preactivation";
            // 
            // dataColumn4
            // 
            this.dataColumn4.ColumnName = "Activation";
            // 
            // dataColumn5
            // 
            this.dataColumn5.ColumnName = "Security";
            // 
            // dataColumn6
            // 
            this.dataColumn6.ColumnName = "In position";
            // 
            // dataColumn7
            // 
            this.dataColumn7.ColumnName = "Trades today";
            // 
            // dataColumn8
            // 
            this.dataColumn8.ColumnName = "Profitability";
            // 
            // modelDataGridViewTextBoxColumn
            // 
            this.modelDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.modelDataGridViewTextBoxColumn.DataPropertyName = "Model";
            this.modelDataGridViewTextBoxColumn.HeaderText = "Model";
            this.modelDataGridViewTextBoxColumn.Name = "modelDataGridViewTextBoxColumn";
            this.modelDataGridViewTextBoxColumn.Width = 61;
            // 
            // initialStateDataGridViewTextBoxColumn
            // 
            this.initialStateDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.initialStateDataGridViewTextBoxColumn.DataPropertyName = "Initial state";
            this.initialStateDataGridViewTextBoxColumn.HeaderText = "Initial state";
            this.initialStateDataGridViewTextBoxColumn.Name = "initialStateDataGridViewTextBoxColumn";
            this.initialStateDataGridViewTextBoxColumn.Width = 82;
            // 
            // preactivationDataGridViewTextBoxColumn
            // 
            this.preactivationDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.preactivationDataGridViewTextBoxColumn.DataPropertyName = "Preactivation";
            this.preactivationDataGridViewTextBoxColumn.HeaderText = "Preactivation";
            this.preactivationDataGridViewTextBoxColumn.Name = "preactivationDataGridViewTextBoxColumn";
            this.preactivationDataGridViewTextBoxColumn.Width = 94;
            // 
            // activationDataGridViewTextBoxColumn
            // 
            this.activationDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.activationDataGridViewTextBoxColumn.DataPropertyName = "Activation";
            this.activationDataGridViewTextBoxColumn.HeaderText = "Activation";
            this.activationDataGridViewTextBoxColumn.Name = "activationDataGridViewTextBoxColumn";
            this.activationDataGridViewTextBoxColumn.Width = 79;
            // 
            // securityDataGridViewTextBoxColumn
            // 
            this.securityDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.securityDataGridViewTextBoxColumn.DataPropertyName = "Security";
            this.securityDataGridViewTextBoxColumn.HeaderText = "Security";
            this.securityDataGridViewTextBoxColumn.Name = "securityDataGridViewTextBoxColumn";
            this.securityDataGridViewTextBoxColumn.Width = 70;
            // 
            // inPositionDataGridViewTextBoxColumn
            // 
            this.inPositionDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.inPositionDataGridViewTextBoxColumn.DataPropertyName = "In position";
            this.inPositionDataGridViewTextBoxColumn.HeaderText = "In position";
            this.inPositionDataGridViewTextBoxColumn.Name = "inPositionDataGridViewTextBoxColumn";
            this.inPositionDataGridViewTextBoxColumn.Width = 80;
            // 
            // tradesTodayDataGridViewTextBoxColumn
            // 
            this.tradesTodayDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.tradesTodayDataGridViewTextBoxColumn.DataPropertyName = "Trades today";
            this.tradesTodayDataGridViewTextBoxColumn.HeaderText = "Trades today";
            this.tradesTodayDataGridViewTextBoxColumn.Name = "tradesTodayDataGridViewTextBoxColumn";
            this.tradesTodayDataGridViewTextBoxColumn.Width = 94;
            // 
            // profitabilityDataGridViewTextBoxColumn
            // 
            this.profitabilityDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.profitabilityDataGridViewTextBoxColumn.DataPropertyName = "Profitability";
            this.profitabilityDataGridViewTextBoxColumn.HeaderText = "Profitability";
            this.profitabilityDataGridViewTextBoxColumn.Name = "profitabilityDataGridViewTextBoxColumn";
            this.profitabilityDataGridViewTextBoxColumn.Width = 82;
            // 
            // BehavioralModelsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(688, 110);
            this.Controls.Add(this.dataGridView);
            this.Name = "BehavioralModelsWindow";
            this.Text = "BehavioralModelsWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BehavioralModelsWindow_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.behavioralModelsDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Data.DataTable dataTable1;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        public System.Windows.Forms.DataGridView dataGridView;
        public System.Data.DataSet behavioralModelsDataSet;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn4;
        private System.Data.DataColumn dataColumn5;
        private System.Data.DataColumn dataColumn6;
        private System.Data.DataColumn dataColumn7;
        private System.Data.DataColumn dataColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn modelDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn initialStateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn preactivationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn activationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn securityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn inPositionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tradesTodayDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn profitabilityDataGridViewTextBoxColumn;
    }
}