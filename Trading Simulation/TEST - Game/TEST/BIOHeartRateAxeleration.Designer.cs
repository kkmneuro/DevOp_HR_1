namespace TEST
{
    partial class BIOHeartRateAxeleration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BIOHeartRateAxeleration));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.bioHeartRateAccelerationControl1 = new TEST.BIOHeartRateAccelerationControl();
            this.SuspendLayout();
            // 
            // bioHeartRateAccelerationControl1
            // 
            this.bioHeartRateAccelerationControl1.AutoSize = true;
            this.bioHeartRateAccelerationControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bioHeartRateAccelerationControl1.Location = new System.Drawing.Point(0, 0);
            this.bioHeartRateAccelerationControl1.Name = "bioHeartRateAccelerationControl1";
            this.bioHeartRateAccelerationControl1.Size = new System.Drawing.Size(733, 204);
            this.bioHeartRateAccelerationControl1.TabIndex = 1;
            // 
            // BIOHeartRateAxeleration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 204);
            this.Controls.Add(this.bioHeartRateAccelerationControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BIOHeartRateAxeleration";
            this.Text = "NeurAlgoΩ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BIOHeartRateAxeleration_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private BIOHeartRateAccelerationControl bioHeartRateAccelerationControl1;
    }
}