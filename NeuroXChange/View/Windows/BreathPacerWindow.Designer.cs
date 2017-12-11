namespace NeuroXChange.View
{
    partial class BreathPacerWindow
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
            this.breathPacerControl = new BreathPacer.BreathPacerControl();
            this.SuspendLayout();
            // 
            // breathPacerControl
            // 
            this.breathPacerControl.BallColor = System.Drawing.Color.Yellow;
            this.breathPacerControl.BallDiameter = 25;
            this.breathPacerControl.BreathsPerMinute = 5.5D;
            this.breathPacerControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.breathPacerControl.ElapsedCycleCount = 0;
            this.breathPacerControl.InhalePrecentage = 45;
            this.breathPacerControl.LineColor = System.Drawing.Color.DeepSkyBlue;
            this.breathPacerControl.LineWidth = 15;
            this.breathPacerControl.Location = new System.Drawing.Point(0, 0);
            this.breathPacerControl.Name = "breathPacerControl";
            this.breathPacerControl.Size = new System.Drawing.Size(281, 133);
            this.breathPacerControl.TabIndex = 0;
            this.breathPacerControl.TestMode = false;
            // 
            // BreathPacerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(281, 133);
            this.Controls.Add(this.breathPacerControl);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
            this.HideOnClose = true;
            this.Name = "BreathPacerWindow";
            this.Text = "Breath pacer";
            this.ResumeLayout(false);

        }

        #endregion

        public BreathPacer.BreathPacerControl breathPacerControl;
    }
}