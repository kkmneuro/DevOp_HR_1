using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

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
            bool flag = disposing && this.components != null;
            if (flag)
            {
                this.components.Dispose();
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
            this.pbPacer = new System.Windows.Forms.PictureBox();
            this.lblBpm = new System.Windows.Forms.Label();
            this.pbBall = new System.Windows.Forms.PictureBox();
            this.renderingTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbPacer)).BeginInit();
            this.pbPacer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBall)).BeginInit();
            this.SuspendLayout();
            // 
            // pbPacer
            // 
            this.pbPacer.Controls.Add(this.lblBpm);
            this.pbPacer.Controls.Add(this.pbBall);
            this.pbPacer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbPacer.Location = new System.Drawing.Point(0, 0);
            this.pbPacer.Name = "pbPacer";
            this.pbPacer.Size = new System.Drawing.Size(284, 226);
            this.pbPacer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPacer.TabIndex = 0;
            this.pbPacer.TabStop = false;
            this.pbPacer.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlPacer_Paint);
            // 
            // lblBpm
            // 
            this.lblBpm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBpm.AutoSize = true;
            this.lblBpm.ForeColor = System.Drawing.Color.Red;
            this.lblBpm.Location = new System.Drawing.Point(233, 12);
            this.lblBpm.Name = "lblBpm";
            this.lblBpm.Size = new System.Drawing.Size(27, 13);
            this.lblBpm.TabIndex = 1;
            this.lblBpm.Text = "bpm";
            // 
            // pbBall
            // 
            this.pbBall.BackColor = System.Drawing.Color.Transparent;
            this.pbBall.Location = new System.Drawing.Point(3, 203);
            this.pbBall.Name = "pbBall";
            this.pbBall.Size = new System.Drawing.Size(26, 26);
            this.pbBall.TabIndex = 0;
            this.pbBall.TabStop = false;
            this.pbBall.Paint += new System.Windows.Forms.PaintEventHandler(this.pbBall_Paint);
            // 
            // renderingTimer
            // 
            this.renderingTimer.Interval = 10;
            this.renderingTimer.Tick += new System.EventHandler(this.renderingTimer_Tick);
            // 
            // BreathPacerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pbPacer);
            this.Name = "BreathPacerControl";
            this.Size = new System.Drawing.Size(284, 226);
            this.Resize += new System.EventHandler(this.BreathPacerControl_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pbPacer)).EndInit();
            this.pbPacer.ResumeLayout(false);
            this.pbPacer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBall)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbBall;
        private System.Windows.Forms.Label lblBpm;
    }
}
