namespace FXCharts
{
    partial class Form2
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
            this.futuresSelection1 = new SelectionControl.FuturesSelection();
            this.SuspendLayout();
            // 
            // futuresSelection1
            // 
            this.futuresSelection1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.futuresSelection1.Location = new System.Drawing.Point(0, 0);
            this.futuresSelection1.Name = "futuresSelection1";
            this.futuresSelection1.Size = new System.Drawing.Size(788, 298);
            this.futuresSelection1.TabIndex = 6;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 298);
            this.Controls.Add(this.futuresSelection1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);

        }

        #endregion
        private SelectionControl.FuturesSelection futuresSelection1;
    }
}