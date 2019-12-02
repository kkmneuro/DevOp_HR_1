namespace TEST
{
    partial class Register
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Register));
            this.textBox1Email = new System.Windows.Forms.TextBox();
            this.textBox2Pass = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox5Company = new System.Windows.Forms.TextBox();
            this.textBox4Name = new System.Windows.Forms.TextBox();
            this.textBox3PassConf = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1Email
            // 
            this.textBox1Email.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.textBox1Email.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1Email.ForeColor = System.Drawing.SystemColors.Window;
            this.textBox1Email.Location = new System.Drawing.Point(10, 47);
            this.textBox1Email.Name = "textBox1Email";
            this.textBox1Email.Size = new System.Drawing.Size(206, 26);
            this.textBox1Email.TabIndex = 3;
            this.textBox1Email.Text = "Email";
            this.textBox1Email.Enter += new System.EventHandler(this.textBox1Email_Enter);
            this.textBox1Email.Leave += new System.EventHandler(this.textBox1Email_Leave);
            // 
            // textBox2Pass
            // 
            this.textBox2Pass.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.textBox2Pass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2Pass.ForeColor = System.Drawing.SystemColors.Window;
            this.textBox2Pass.Location = new System.Drawing.Point(10, 79);
            this.textBox2Pass.Name = "textBox2Pass";
            this.textBox2Pass.Size = new System.Drawing.Size(206, 26);
            this.textBox2Pass.TabIndex = 4;
            this.textBox2Pass.Text = "Password";
            this.textBox2Pass.Enter += new System.EventHandler(this.textBox2Pass_Enter);
            this.textBox2Pass.Leave += new System.EventHandler(this.textBox2Pass_Leave);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Maroon;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(234, 54);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(167, 73);
            this.button1.TabIndex = 1;
            this.button1.Text = "Register";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(711, 307);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.textBox5Company);
            this.panel2.Controls.Add(this.textBox4Name);
            this.panel2.Controls.Add(this.textBox3PassConf);
            this.panel2.Controls.Add(this.textBox1Email);
            this.panel2.Controls.Add(this.textBox2Pass);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Location = new System.Drawing.Point(144, 58);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(423, 191);
            this.panel2.TabIndex = 6;
            // 
            // textBox5Company
            // 
            this.textBox5Company.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.textBox5Company.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox5Company.ForeColor = System.Drawing.SystemColors.Window;
            this.textBox5Company.Location = new System.Drawing.Point(10, 143);
            this.textBox5Company.Name = "textBox5Company";
            this.textBox5Company.Size = new System.Drawing.Size(206, 26);
            this.textBox5Company.TabIndex = 6;
            this.textBox5Company.Text = "Company";
            this.textBox5Company.Enter += new System.EventHandler(this.textBox5Company_Enter);
            this.textBox5Company.Leave += new System.EventHandler(this.textBox5Company_Leave);
            // 
            // textBox4Name
            // 
            this.textBox4Name.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.textBox4Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4Name.ForeColor = System.Drawing.SystemColors.Window;
            this.textBox4Name.Location = new System.Drawing.Point(10, 15);
            this.textBox4Name.Name = "textBox4Name";
            this.textBox4Name.Size = new System.Drawing.Size(206, 26);
            this.textBox4Name.TabIndex = 2;
            this.textBox4Name.Text = "Name";
            this.textBox4Name.Enter += new System.EventHandler(this.textBox4Name_Enter);
            this.textBox4Name.Leave += new System.EventHandler(this.textBox4Name_Leave);
            // 
            // textBox3PassConf
            // 
            this.textBox3PassConf.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.textBox3PassConf.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3PassConf.ForeColor = System.Drawing.SystemColors.Window;
            this.textBox3PassConf.Location = new System.Drawing.Point(10, 111);
            this.textBox3PassConf.Name = "textBox3PassConf";
            this.textBox3PassConf.Size = new System.Drawing.Size(206, 26);
            this.textBox3PassConf.TabIndex = 5;
            this.textBox3PassConf.Text = "Confirm Password";
            this.textBox3PassConf.Enter += new System.EventHandler(this.textBox3PassConf_Enter);
            this.textBox3PassConf.Leave += new System.EventHandler(this.textBox3PassConf_Leave);
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(711, 307);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Register";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "User Registration";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox textBox1Email;
        private System.Windows.Forms.TextBox textBox2Pass;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox5Company;
        private System.Windows.Forms.TextBox textBox4Name;
        private System.Windows.Forms.TextBox textBox3PassConf;
    }
}