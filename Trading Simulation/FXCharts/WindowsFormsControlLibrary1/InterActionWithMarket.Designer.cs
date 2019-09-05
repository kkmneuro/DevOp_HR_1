namespace SelectionControl
{
    partial class InterActionWithMarket
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1Content = new System.Windows.Forms.Panel();
            this.button2Reset = new System.Windows.Forms.Button();
            this.label1Info = new System.Windows.Forms.Label();
            this.futuresSelection1 = new SelectionControl.FuturesSelection();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1Content, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(631, 250);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1Content
            // 
            this.panel1Content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1Content.Location = new System.Drawing.Point(3, 43);
            this.panel1Content.Name = "panel1Content";
            this.panel1Content.Size = new System.Drawing.Size(625, 204);
            this.panel1Content.TabIndex = 4;
            // 
            // button2Reset
            // 
            this.button2Reset.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button2Reset.Location = new System.Drawing.Point(540, 8);
            this.button2Reset.Name = "button2Reset";
            this.button2Reset.Size = new System.Drawing.Size(82, 23);
            this.button2Reset.TabIndex = 6;
            this.button2Reset.Text = "Reset";
            this.button2Reset.UseVisualStyleBackColor = true;
            // 
            // label1Info
            // 
            this.label1Info.AutoSize = true;
            this.label1Info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1Info.Location = new System.Drawing.Point(90, 0);
            this.label1Info.Name = "label1Info";
            this.label1Info.Size = new System.Drawing.Size(444, 34);
            this.label1Info.TabIndex = 7;
            this.label1Info.Text = "Empty selection";
            this.label1Info.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // futuresSelection1
            // 
            this.futuresSelection1.Location = new System.Drawing.Point(0, 0);
            this.futuresSelection1.Name = "futuresSelection1";
            this.futuresSelection1.Size = new System.Drawing.Size(688, 289);
            this.futuresSelection1.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tableLayoutPanel3.Controls.Add(this.button2Reset, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.label1Info, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(625, 34);
            this.tableLayoutPanel3.TabIndex = 8;
            // 
            // InterActionWithMarket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "InterActionWithMarket";
            this.Size = new System.Drawing.Size(631, 250);
            this.Load += new System.EventHandler(this.InterActionWithMarket_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1Content;
        private System.Windows.Forms.Button button2Reset;
        private System.Windows.Forms.Label label1Info;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    }
}
