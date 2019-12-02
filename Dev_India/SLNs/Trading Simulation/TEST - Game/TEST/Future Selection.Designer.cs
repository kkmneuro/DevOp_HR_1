namespace TEST
{
    partial class FutureSelection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FutureSelection));
            this.button3 = new System.Windows.Forms.Button();
            this.axTTLLive = new AxTTLLiveCtrlLib.AxTTLLive();
            this.futuresSelection1 = new SelectionControl.FuturesSelection();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.axTTLLive)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(596, 588);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(177, 67);
            this.button3.TabIndex = 6;
            this.button3.Text = "TEST YOUR \r\nTRADING SKILS";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // axTTLLive
            // 
            this.axTTLLive.Enabled = true;
            this.axTTLLive.Location = new System.Drawing.Point(23, 25);
            this.axTTLLive.Name = "axTTLLive";
            this.axTTLLive.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTTLLive.OcxState")));
            this.axTTLLive.Size = new System.Drawing.Size(32, 32);
            this.axTTLLive.TabIndex = 9;
            // 
            // futuresSelection1
            // 
            this.futuresSelection1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.futuresSelection1.Location = new System.Drawing.Point(341, 155);
            this.futuresSelection1.Name = "futuresSelection1";
            this.futuresSelection1.Size = new System.Drawing.Size(688, 289);
            this.futuresSelection1.TabIndex = 10;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.futuresSelection1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.button3, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.63743F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 78.36257F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 256F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1370, 750);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // FutureSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1370, 750);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.axTTLLive);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FutureSelection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Future Selection";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Screen2_FormClosing);
            this.Load += new System.EventHandler(this.FutureSelection_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axTTLLive)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button3;
        private AxTTLLiveCtrlLib.AxTTLLive axTTLLive;
        private SelectionControl.FuturesSelection futuresSelection1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}