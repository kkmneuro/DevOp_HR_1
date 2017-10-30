namespace BreathPacer
{
    partial class BreathPacerControl
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tmrPacer = new System.Windows.Forms.Timer(this.components);
            this.pbBall = new System.Windows.Forms.PictureBox();
            this.lblBpm = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbBall)).BeginInit();
            this.SuspendLayout();
            // 
            // tmrPacer
            // 
            this.tmrPacer.Tick += new System.EventHandler(this.tmrPacer_Tick);
            // 
            // pbBall
            // 
            this.pbBall.BackColor = System.Drawing.Color.Transparent;
            this.pbBall.Location = new System.Drawing.Point(0, 197);
            this.pbBall.Name = "pbBall";
            this.pbBall.Size = new System.Drawing.Size(26, 26);
            this.pbBall.TabIndex = 0;
            this.pbBall.TabStop = false;
            this.pbBall.Paint += new System.Windows.Forms.PaintEventHandler(this.pbBall_Paint);
            // 
            // lblBpm
            // 
            this.lblBpm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBpm.AutoSize = true;
            this.lblBpm.ForeColor = System.Drawing.Color.Red;
            this.lblBpm.Location = new System.Drawing.Point(244, 10);
            this.lblBpm.Name = "lblBpm";
            this.lblBpm.Size = new System.Drawing.Size(27, 13);
            this.lblBpm.TabIndex = 1;
            this.lblBpm.Text = "bpm";
            // 
            // BreathPacerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblBpm);
            this.Controls.Add(this.pbBall);
            this.Name = "BreathPacerControl";
            this.Size = new System.Drawing.Size(284, 226);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.BreathPacerControl_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.pbBall)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmrPacer;
        private System.Windows.Forms.PictureBox pbBall;
        private System.Windows.Forms.Label lblBpm;
    }
}
