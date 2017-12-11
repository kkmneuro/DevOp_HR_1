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
            this.SuspendLayout();
            // 
            // btnBuy
            // 
            this.btnBuy.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnBuy.Enabled = false;
            this.btnBuy.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuy.ForeColor = System.Drawing.Color.White;
            this.btnBuy.Location = new System.Drawing.Point(135, 38);
            this.btnBuy.Name = "btnBuy";
            this.btnBuy.Size = new System.Drawing.Size(111, 56);
            this.btnBuy.TabIndex = 0;
            this.btnBuy.Text = "BUY\r\n    1.23435";
            this.btnBuy.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnBuy.UseVisualStyleBackColor = false;
            // 
            // btnSell
            // 
            this.btnSell.BackColor = System.Drawing.Color.Red;
            this.btnSell.Enabled = false;
            this.btnSell.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSell.ForeColor = System.Drawing.Color.White;
            this.btnSell.Location = new System.Drawing.Point(12, 38);
            this.btnSell.Name = "btnSell";
            this.btnSell.Size = new System.Drawing.Size(111, 56);
            this.btnSell.TabIndex = 1;
            this.btnSell.Text = "          SELL\r\n   1.23455";
            this.btnSell.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSell.UseVisualStyleBackColor = false;
            // 
            // labStepName
            // 
            this.labStepName.AutoSize = true;
            this.labStepName.Location = new System.Drawing.Point(12, 12);
            this.labStepName.Name = "labStepName";
            this.labStepName.Size = new System.Drawing.Size(220, 13);
            this.labStepName.TabIndex = 7;
            this.labStepName.Text = "Display the stage of the loop process - HERE";
            // 
            // NewOrderWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(258, 108);
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
    }
}