namespace NeuroXChange.View
{
    partial class NewOrderWindow
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
            this.btnBuy = new System.Windows.Forms.Button();
            this.btnSell = new System.Windows.Forms.Button();
            this.labStepName = new System.Windows.Forms.Label();
            this.CBlinkButtonsToActiveBM = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnBuy
            // 
            this.btnBuy.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnBuy.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnBuy.Enabled = false;
            this.btnBuy.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuy.ForeColor = System.Drawing.Color.White;
            this.btnBuy.Location = new System.Drawing.Point(135, 32);
            this.btnBuy.Name = "btnBuy";
            this.btnBuy.Size = new System.Drawing.Size(111, 56);
            this.btnBuy.TabIndex = 0;
            this.btnBuy.Text = "BUY\r\n    1.23435";
            this.btnBuy.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnBuy.UseVisualStyleBackColor = false;
            this.btnBuy.Click += new System.EventHandler(this.buysell_button_Click);
            // 
            // btnSell
            // 
            this.btnSell.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSell.BackColor = System.Drawing.Color.Red;
            this.btnSell.Enabled = false;
            this.btnSell.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSell.ForeColor = System.Drawing.Color.White;
            this.btnSell.Location = new System.Drawing.Point(12, 32);
            this.btnSell.Name = "btnSell";
            this.btnSell.Size = new System.Drawing.Size(111, 56);
            this.btnSell.TabIndex = 1;
            this.btnSell.Text = "          SELL\r\n   1.23455";
            this.btnSell.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSell.UseVisualStyleBackColor = false;
            this.btnSell.Click += new System.EventHandler(this.buysell_button_Click);
            // 
            // labStepName
            // 
            this.labStepName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labStepName.AutoSize = true;
            this.labStepName.Location = new System.Drawing.Point(12, 10);
            this.labStepName.Name = "labStepName";
            this.labStepName.Size = new System.Drawing.Size(0, 13);
            this.labStepName.TabIndex = 7;
            // 
            // CBlinkButtonsToActiveBM
            // 
            this.CBlinkButtonsToActiveBM.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.CBlinkButtonsToActiveBM.AutoSize = true;
            this.CBlinkButtonsToActiveBM.Checked = true;
            this.CBlinkButtonsToActiveBM.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CBlinkButtonsToActiveBM.Location = new System.Drawing.Point(15, 96);
            this.CBlinkButtonsToActiveBM.Name = "CBlinkButtonsToActiveBM";
            this.CBlinkButtonsToActiveBM.Size = new System.Drawing.Size(147, 17);
            this.CBlinkButtonsToActiveBM.TabIndex = 8;
            this.CBlinkButtonsToActiveBM.Text = "Link buttons to active BM";
            this.CBlinkButtonsToActiveBM.UseVisualStyleBackColor = true;
            this.CBlinkButtonsToActiveBM.CheckedChanged += new System.EventHandler(this.CBlinkButtonsToActiveBM_CheckedChanged);
            // 
            // NewOrderWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(258, 130);
            this.Controls.Add(this.CBlinkButtonsToActiveBM);
            this.Controls.Add(this.labStepName);
            this.Controls.Add(this.btnSell);
            this.Controls.Add(this.btnBuy);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HideOnClose = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewOrderWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New order";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button btnBuy;
        public System.Windows.Forms.Button btnSell;
        public System.Windows.Forms.Label labStepName;
        public System.Windows.Forms.CheckBox CBlinkButtonsToActiveBM;
    }
}