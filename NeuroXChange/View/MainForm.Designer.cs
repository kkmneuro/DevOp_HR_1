namespace NeuroXChange
{
    partial class MainForm
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
            this.bioDataRTB = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.heartRateRTB = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // bioDataRTB
            // 
            this.bioDataRTB.Location = new System.Drawing.Point(3, 15);
            this.bioDataRTB.Name = "bioDataRTB";
            this.bioDataRTB.ReadOnly = true;
            this.bioDataRTB.Size = new System.Drawing.Size(278, 191);
            this.bioDataRTB.TabIndex = 0;
            this.bioDataRTB.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Bio Data:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 209);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Heart Rate Statistics:";
            // 
            // heartRateRTB
            // 
            this.heartRateRTB.BackColor = System.Drawing.SystemColors.Control;
            this.heartRateRTB.Location = new System.Drawing.Point(3, 225);
            this.heartRateRTB.Name = "heartRateRTB";
            this.heartRateRTB.Size = new System.Drawing.Size(278, 60);
            this.heartRateRTB.TabIndex = 3;
            this.heartRateRTB.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 287);
            this.Controls.Add(this.heartRateRTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bioDataRTB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NeuroXChange";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.RichTextBox bioDataRTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.RichTextBox heartRateRTB;
    }
}

