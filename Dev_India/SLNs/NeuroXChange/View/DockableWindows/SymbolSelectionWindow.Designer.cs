namespace NeuroXChange.View
{
    partial class SymbolSelectionWindow
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
            this.chkSymbols = new System.Windows.Forms.CheckedListBox();
            this.btnSaveExit = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chkSymbols
            // 
            this.chkSymbols.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSymbols.FormattingEnabled = true;
            this.chkSymbols.Location = new System.Drawing.Point(23, 35);
            this.chkSymbols.Name = "chkSymbols";
            this.chkSymbols.Size = new System.Drawing.Size(139, 260);
            this.chkSymbols.TabIndex = 0;
            // 
            // btnSaveExit
            // 
            this.btnSaveExit.Location = new System.Drawing.Point(23, 302);
            this.btnSaveExit.Name = "btnSaveExit";
            this.btnSaveExit.Size = new System.Drawing.Size(139, 23);
            this.btnSaveExit.TabIndex = 1;
            this.btnSaveExit.Text = "Save";
            this.btnSaveExit.UseVisualStyleBackColor = true;
            this.btnSaveExit.Click += new System.EventHandler(this.btnSaveExit_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(20, 19);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(114, 13);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Loading... Please wait!";
            // 
            // SymbolSelectionWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(191, 330);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnSaveExit);
            this.Controls.Add(this.chkSymbols);
            this.Name = "SymbolSelectionWindow";
            this.Text = "Symbols";
            this.Load += new System.EventHandler(this.SymbolSelectionWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox chkSymbols;
        private System.Windows.Forms.Button btnSaveExit;
        private System.Windows.Forms.Label lblStatus;
    }
}