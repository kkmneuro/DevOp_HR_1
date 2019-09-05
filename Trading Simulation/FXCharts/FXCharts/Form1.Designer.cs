namespace FXCharts
{
    partial class Form1
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
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox2TimeFrame = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dateTimePicker1From = new System.Windows.Forms.DateTimePicker();
            this.comboBox3Future = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1Step = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Content = new System.Windows.Forms.GroupBox();
            this.interActionWithMarket1 = new SelectionControl.InterActionWithMarket();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.Content.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(112, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Time Frame";
            // 
            // comboBox2TimeFrame
            // 
            this.comboBox2TimeFrame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2TimeFrame.FormattingEnabled = true;
            this.comboBox2TimeFrame.Location = new System.Drawing.Point(180, 67);
            this.comboBox2TimeFrame.Name = "comboBox2TimeFrame";
            this.comboBox2TimeFrame.Size = new System.Drawing.Size(332, 21);
            this.comboBox2TimeFrame.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(30, 103);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(144, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Amount Of History Data From";
            // 
            // dateTimePicker1From
            // 
            this.dateTimePicker1From.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1From.Location = new System.Drawing.Point(180, 99);
            this.dateTimePicker1From.Name = "dateTimePicker1From";
            this.dateTimePicker1From.Size = new System.Drawing.Size(332, 20);
            this.dateTimePicker1From.TabIndex = 14;
            // 
            // comboBox3Future
            // 
            this.comboBox3Future.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3Future.FormattingEnabled = true;
            this.comboBox3Future.Location = new System.Drawing.Point(180, 37);
            this.comboBox3Future.Name = "comboBox3Future";
            this.comboBox3Future.Size = new System.Drawing.Size(332, 21);
            this.comboBox3Future.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(55, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Step/Selection/Review";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(518, 37);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(88, 23);
            this.button3.TabIndex = 19;
            this.button3.Text = "Show Current";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(518, 63);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(88, 23);
            this.button4.TabIndex = 20;
            this.button4.Text = "Show Next";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.comboBox1Step);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.comboBox2TimeFrame);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.comboBox3Future);
            this.groupBox2.Controls.Add(this.dateTimePicker1From);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(935, 137);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Content Parameters";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(677, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 23);
            this.button1.TabIndex = 23;
            this.button1.Text = "Show Securities";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1Step
            // 
            this.comboBox1Step.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1Step.FormattingEnabled = true;
            this.comboBox1Step.Items.AddRange(new object[] {
            "SELECTION: List of possible securities to choose",
            "SELECTION: Display Live Data of chosen security",
            "SELECTION: 6 comand buttons",
            "SELECTION: Put 1 solid line, color Golden Rod",
            "SELECTION: Put 1 solid line, color Lime.",
            "SELECTION: Put 2 solid lines, color Aqua.",
            "SELECTION: Put 2 solid linse, color Dodger Blue.",
            "SELECTION: Put 2 solid lines, color Blue.",
            "SELECTION: Put multiple dashed lines, color white.",
            "SELECTION: Put multiple dashed lines, Red white.",
            "REVIEW: 122? selected security with all the lines placed on it from previous rout" +
                "ine",
            "REVIEW: 124? selected security with all lines",
            "REVIEW: 5 min chart selected security with all the lines placed on it from previo" +
                "us 1 hour chart (same as 122)"});
            this.comboBox1Step.Location = new System.Drawing.Point(180, 10);
            this.comboBox1Step.Name = "comboBox1Step";
            this.comboBox1Step.Size = new System.Drawing.Size(332, 21);
            this.comboBox1Step.TabIndex = 22;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(137, 42);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Future";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.Content, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 149F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(947, 404);
            this.tableLayoutPanel1.TabIndex = 22;
            // 
            // Content
            // 
            this.Content.Controls.Add(this.interActionWithMarket1);
            this.Content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Content.Location = new System.Drawing.Point(3, 152);
            this.Content.Name = "Content";
            this.Content.Size = new System.Drawing.Size(941, 249);
            this.Content.TabIndex = 1;
            this.Content.TabStop = false;
            this.Content.Text = "Content";
            // 
            // interActionWithMarket1
            // 
            this.interActionWithMarket1.DataFrom = new System.DateTime(((long)(0)));
            this.interActionWithMarket1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.interActionWithMarket1.InfoTextColor = System.Drawing.SystemColors.ControlText;
            this.interActionWithMarket1.Location = new System.Drawing.Point(3, 16);
            this.interActionWithMarket1.Md = null;
            this.interActionWithMarket1.Name = "interActionWithMarket1";
            this.interActionWithMarket1.Size = new System.Drawing.Size(935, 230);
            this.interActionWithMarket1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(941, 143);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(677, 48);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(113, 23);
            this.button2.TabIndex = 24;
            this.button2.Text = "Show/Hide Legend";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 404);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.Content.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox2TimeFrame;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dateTimePicker1From;
        private System.Windows.Forms.ComboBox comboBox3Future;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox Content;
        private System.Windows.Forms.ComboBox comboBox1Step;
        private System.Windows.Forms.Label label9;
        private SelectionControl.InterActionWithMarket interActionWithMarket1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

