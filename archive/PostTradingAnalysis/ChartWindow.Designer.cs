namespace PostTradingAnalysis
{
    partial class ChartWindow
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
            this.plotView = new OxyPlot.WindowsForms.PlotView();
            this.btnHMinus = new System.Windows.Forms.Button();
            this.btnHPlus = new System.Windows.Forms.Button();
            this.btnVMinus = new System.Windows.Forms.Button();
            this.btnVPlus = new System.Windows.Forms.Button();
            this.btnUserActions = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // plotView
            // 
            this.plotView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotView.Location = new System.Drawing.Point(0, 0);
            this.plotView.Name = "plotView";
            this.plotView.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotView.Size = new System.Drawing.Size(335, 204);
            this.plotView.TabIndex = 0;
            this.plotView.Text = "plotView1";
            this.plotView.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotView.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotView.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // btnHMinus
            // 
            this.btnHMinus.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnHMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHMinus.Location = new System.Drawing.Point(68, 2);
            this.btnHMinus.Name = "btnHMinus";
            this.btnHMinus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnHMinus.Size = new System.Drawing.Size(20, 20);
            this.btnHMinus.TabIndex = 19;
            this.btnHMinus.Text = "H-";
            this.btnHMinus.UseMnemonic = false;
            this.btnHMinus.UseVisualStyleBackColor = true;
            this.btnHMinus.Click += new System.EventHandler(this.btnZoom_Click);
            // 
            // btnHPlus
            // 
            this.btnHPlus.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnHPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHPlus.Location = new System.Drawing.Point(46, 2);
            this.btnHPlus.Name = "btnHPlus";
            this.btnHPlus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnHPlus.Size = new System.Drawing.Size(20, 20);
            this.btnHPlus.TabIndex = 18;
            this.btnHPlus.Text = "H+";
            this.btnHPlus.UseMnemonic = false;
            this.btnHPlus.UseVisualStyleBackColor = true;
            this.btnHPlus.Click += new System.EventHandler(this.btnZoom_Click);
            // 
            // btnVMinus
            // 
            this.btnVMinus.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnVMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVMinus.Location = new System.Drawing.Point(24, 2);
            this.btnVMinus.Name = "btnVMinus";
            this.btnVMinus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnVMinus.Size = new System.Drawing.Size(20, 20);
            this.btnVMinus.TabIndex = 17;
            this.btnVMinus.Text = "V-";
            this.btnVMinus.UseMnemonic = false;
            this.btnVMinus.UseVisualStyleBackColor = true;
            this.btnVMinus.Click += new System.EventHandler(this.btnZoom_Click);
            // 
            // btnVPlus
            // 
            this.btnVPlus.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnVPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVPlus.Location = new System.Drawing.Point(2, 2);
            this.btnVPlus.Name = "btnVPlus";
            this.btnVPlus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnVPlus.Size = new System.Drawing.Size(20, 20);
            this.btnVPlus.TabIndex = 16;
            this.btnVPlus.Text = "V+";
            this.btnVPlus.UseMnemonic = false;
            this.btnVPlus.UseVisualStyleBackColor = true;
            this.btnVPlus.Click += new System.EventHandler(this.btnZoom_Click);
            // 
            // btnUserActions
            // 
            this.btnUserActions.BackColor = System.Drawing.SystemColors.Control;
            this.btnUserActions.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUserActions.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUserActions.Location = new System.Drawing.Point(90, 2);
            this.btnUserActions.Name = "btnUserActions";
            this.btnUserActions.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnUserActions.Size = new System.Drawing.Size(20, 20);
            this.btnUserActions.TabIndex = 20;
            this.btnUserActions.Text = "U";
            this.btnUserActions.UseMnemonic = false;
            this.btnUserActions.UseVisualStyleBackColor = false;
            this.btnUserActions.Click += new System.EventHandler(this.btnUserActions_Click);
            // 
            // ChartWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 204);
            this.Controls.Add(this.btnUserActions);
            this.Controls.Add(this.btnHMinus);
            this.Controls.Add(this.btnHPlus);
            this.Controls.Add(this.btnVMinus);
            this.Controls.Add(this.btnVPlus);
            this.Controls.Add(this.plotView);
            this.HideOnClose = true;
            this.Name = "ChartWindow";
            this.Text = "ChartWindow";
            this.ResumeLayout(false);

        }

        #endregion

        public OxyPlot.WindowsForms.PlotView plotView;
        private System.Windows.Forms.Button btnHMinus;
        private System.Windows.Forms.Button btnHPlus;
        private System.Windows.Forms.Button btnVMinus;
        private System.Windows.Forms.Button btnVPlus;
        private System.Windows.Forms.Button btnUserActions;
    }
}