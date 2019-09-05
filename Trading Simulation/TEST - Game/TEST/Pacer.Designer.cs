namespace TEST
{
    partial class Pacer
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
            this.breathPacerControl1 = new BreathPacer.BreathPacerControl();
            this.SuspendLayout();
            // 
            // breathPacerControl1
            // 
            this.breathPacerControl1.BallColor = System.Drawing.Color.Yellow;
            this.breathPacerControl1.BallDiameter = 25;
            this.breathPacerControl1.BreathsPerMinute = 5.5D;
            this.breathPacerControl1.ElapsedCycleCount = 0;
            this.breathPacerControl1.InhalePrecentage = 50;
            this.breathPacerControl1.LineColor = System.Drawing.Color.DeepSkyBlue;
            this.breathPacerControl1.LineWidth = 15;
            this.breathPacerControl1.Location = new System.Drawing.Point(0, 12);
            this.breathPacerControl1.Name = "breathPacerControl1";
            this.breathPacerControl1.Size = new System.Drawing.Size(198, 128);
            this.breathPacerControl1.TabIndex = 0;
            this.breathPacerControl1.TestMode = false;
            // 
            // Pacer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(198, 139);
            this.Controls.Add(this.breathPacerControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Pacer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Pacer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Pacer_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private BreathPacer.BreathPacerControl breathPacerControl1;
    }
}