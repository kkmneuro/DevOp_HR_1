namespace PALSA
{
  partial class SplashScreen
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
            this.tmrUnload = new System.Windows.Forms.Timer(this.components);
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblLoadingMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tmrUnload
            // 
            this.tmrUnload.Enabled = true;
            this.tmrUnload.Interval = 1000;
            this.tmrUnload.Tick += new System.EventHandler(this.tmrUnload_Tick);
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVersion.AutoSize = true;
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.lblVersion.Location = new System.Drawing.Point(322, 331);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(66, 19);
            this.lblVersion.TabIndex = 0;
            this.lblVersion.Text = "version";
            this.lblVersion.UseWaitCursor = true;
            // 
            // lblLoadingMessage
            // 
            this.lblLoadingMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLoadingMessage.AutoSize = true;
            this.lblLoadingMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblLoadingMessage.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoadingMessage.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.lblLoadingMessage.Location = new System.Drawing.Point(8, 331);
            this.lblLoadingMessage.Name = "lblLoadingMessage";
            this.lblLoadingMessage.Size = new System.Drawing.Size(159, 18);
            this.lblLoadingMessage.TabIndex = 1;
            this.lblLoadingMessage.Text = "...loading Order Book";
            this.lblLoadingMessage.UseWaitCursor = true;
            // 
            // SplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::PALSA.Properties.Resources.LTechSplsh;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(472, 355);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblLoadingMessage);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SplashScreen";
            this.Opacity = 0.95D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Maroon;
            this.Load += new System.EventHandler(this.SplashScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    internal System.Windows.Forms.Timer tmrUnload;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.Label lblLoadingMessage;
    public System.Windows.Forms.Label lblVersion;
  }
}