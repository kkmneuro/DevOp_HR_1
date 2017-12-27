namespace NeuroXChange.View.DialogWindows
{
    partial class ManualOrderConfirmationWindow
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbOrderDirection = new System.Windows.Forms.TextBox();
            this.tbOrderType = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbProfitTarget = new System.Windows.Forms.TextBox();
            this.rbUserDefined = new System.Windows.Forms.RadioButton();
            this.rbSystemDefault = new System.Windows.Forms.RadioButton();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Order";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Order type";
            // 
            // tbOrderDirection
            // 
            this.tbOrderDirection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbOrderDirection.ForeColor = System.Drawing.Color.White;
            this.tbOrderDirection.Location = new System.Drawing.Point(15, 25);
            this.tbOrderDirection.Name = "tbOrderDirection";
            this.tbOrderDirection.ReadOnly = true;
            this.tbOrderDirection.Size = new System.Drawing.Size(85, 20);
            this.tbOrderDirection.TabIndex = 2;
            // 
            // tbOrderType
            // 
            this.tbOrderType.BackColor = System.Drawing.SystemColors.Window;
            this.tbOrderType.Location = new System.Drawing.Point(15, 80);
            this.tbOrderType.Name = "tbOrderType";
            this.tbOrderType.ReadOnly = true;
            this.tbOrderType.Size = new System.Drawing.Size(85, 20);
            this.tbOrderType.TabIndex = 3;
            this.tbOrderType.Text = "At market";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbProfitTarget);
            this.groupBox1.Controls.Add(this.rbUserDefined);
            this.groupBox1.Controls.Add(this.rbSystemDefault);
            this.groupBox1.Location = new System.Drawing.Point(117, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(124, 103);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Profit target";
            // 
            // tbProfitTarget
            // 
            this.tbProfitTarget.Enabled = false;
            this.tbProfitTarget.Location = new System.Drawing.Point(14, 71);
            this.tbProfitTarget.Name = "tbProfitTarget";
            this.tbProfitTarget.Size = new System.Drawing.Size(94, 20);
            this.tbProfitTarget.TabIndex = 2;
            this.tbProfitTarget.TextChanged += new System.EventHandler(this.tbProfitTarget_TextChanged);
            this.tbProfitTarget.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbProfitTarget_KeyPress);
            // 
            // rbUserDefined
            // 
            this.rbUserDefined.AutoSize = true;
            this.rbUserDefined.Location = new System.Drawing.Point(14, 48);
            this.rbUserDefined.Name = "rbUserDefined";
            this.rbUserDefined.Size = new System.Drawing.Size(85, 17);
            this.rbUserDefined.TabIndex = 1;
            this.rbUserDefined.Text = "User defined";
            this.rbUserDefined.UseVisualStyleBackColor = true;
            // 
            // rbSystemDefault
            // 
            this.rbSystemDefault.AutoSize = true;
            this.rbSystemDefault.Checked = true;
            this.rbSystemDefault.Location = new System.Drawing.Point(14, 25);
            this.rbSystemDefault.Name = "rbSystemDefault";
            this.rbSystemDefault.Size = new System.Drawing.Size(94, 17);
            this.rbSystemDefault.TabIndex = 0;
            this.rbSystemDefault.TabStop = true;
            this.rbSystemDefault.Text = "System default";
            this.rbSystemDefault.UseVisualStyleBackColor = true;
            this.rbSystemDefault.CheckedChanged += new System.EventHandler(this.rbSystemDefault_CheckedChanged);
            // 
            // btnConfirm
            // 
            this.btnConfirm.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConfirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.Location = new System.Drawing.Point(46, 128);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 32);
            this.btnConfirm.TabIndex = 6;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(141, 128);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 32);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // ManualOrderConfirmationWindow
            // 
            this.AcceptButton = this.btnConfirm;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(256, 172);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbOrderType);
            this.Controls.Add(this.tbOrderDirection);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManualOrderConfirmationWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ManualOrderConfirmation";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbOrderDirection;
        private System.Windows.Forms.TextBox tbOrderType;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbProfitTarget;
        private System.Windows.Forms.RadioButton rbUserDefined;
        private System.Windows.Forms.RadioButton rbSystemDefault;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
    }
}