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
            this.rbPTUserDefined = new System.Windows.Forms.RadioButton();
            this.rbPTSystemDefault = new System.Windows.Forms.RadioButton();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbStopLoss = new System.Windows.Forms.TextBox();
            this.rbSLUserDefined = new System.Windows.Forms.RadioButton();
            this.rbSLSystemDefault = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Direction";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(155, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Type";
            // 
            // tbOrderDirection
            // 
            this.tbOrderDirection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbOrderDirection.ForeColor = System.Drawing.Color.White;
            this.tbOrderDirection.Location = new System.Drawing.Point(68, 6);
            this.tbOrderDirection.Name = "tbOrderDirection";
            this.tbOrderDirection.ReadOnly = true;
            this.tbOrderDirection.Size = new System.Drawing.Size(58, 20);
            this.tbOrderDirection.TabIndex = 2;
            // 
            // tbOrderType
            // 
            this.tbOrderType.BackColor = System.Drawing.SystemColors.Window;
            this.tbOrderType.Location = new System.Drawing.Point(192, 6);
            this.tbOrderType.Name = "tbOrderType";
            this.tbOrderType.ReadOnly = true;
            this.tbOrderType.Size = new System.Drawing.Size(68, 20);
            this.tbOrderType.TabIndex = 3;
            this.tbOrderType.Text = "At market";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbProfitTarget);
            this.groupBox1.Controls.Add(this.rbPTUserDefined);
            this.groupBox1.Controls.Add(this.rbPTSystemDefault);
            this.groupBox1.Location = new System.Drawing.Point(12, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(124, 103);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Profit target";
            // 
            // tbProfitTarget
            // 
            this.tbProfitTarget.Enabled = false;
            this.tbProfitTarget.Location = new System.Drawing.Point(11, 69);
            this.tbProfitTarget.Name = "tbProfitTarget";
            this.tbProfitTarget.Size = new System.Drawing.Size(94, 20);
            this.tbProfitTarget.TabIndex = 2;
            this.tbProfitTarget.TextChanged += new System.EventHandler(this.tbProfitTarget_TextChanged);
            this.tbProfitTarget.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbProfitTarget_KeyPress);
            // 
            // rbPTUserDefined
            // 
            this.rbPTUserDefined.AutoSize = true;
            this.rbPTUserDefined.Location = new System.Drawing.Point(11, 46);
            this.rbPTUserDefined.Name = "rbPTUserDefined";
            this.rbPTUserDefined.Size = new System.Drawing.Size(85, 17);
            this.rbPTUserDefined.TabIndex = 1;
            this.rbPTUserDefined.Text = "User defined";
            this.rbPTUserDefined.UseVisualStyleBackColor = true;
            // 
            // rbPTSystemDefault
            // 
            this.rbPTSystemDefault.AutoSize = true;
            this.rbPTSystemDefault.Checked = true;
            this.rbPTSystemDefault.Location = new System.Drawing.Point(11, 23);
            this.rbPTSystemDefault.Name = "rbPTSystemDefault";
            this.rbPTSystemDefault.Size = new System.Drawing.Size(94, 17);
            this.rbPTSystemDefault.TabIndex = 0;
            this.rbPTSystemDefault.TabStop = true;
            this.rbPTSystemDefault.Text = "System default";
            this.rbPTSystemDefault.UseVisualStyleBackColor = true;
            this.rbPTSystemDefault.CheckedChanged += new System.EventHandler(this.rbSystemDefault_CheckedChanged);
            // 
            // btnConfirm
            // 
            this.btnConfirm.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConfirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.Location = new System.Drawing.Point(61, 161);
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
            this.btnCancel.Location = new System.Drawing.Point(150, 161);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 32);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbStopLoss);
            this.groupBox2.Controls.Add(this.rbSLUserDefined);
            this.groupBox2.Controls.Add(this.rbSLSystemDefault);
            this.groupBox2.Location = new System.Drawing.Point(150, 42);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(124, 103);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Stop loss";
            // 
            // tbStopLoss
            // 
            this.tbStopLoss.Enabled = false;
            this.tbStopLoss.Location = new System.Drawing.Point(11, 69);
            this.tbStopLoss.Name = "tbStopLoss";
            this.tbStopLoss.Size = new System.Drawing.Size(94, 20);
            this.tbStopLoss.TabIndex = 2;
            // 
            // rbSLUserDefined
            // 
            this.rbSLUserDefined.AutoSize = true;
            this.rbSLUserDefined.Enabled = false;
            this.rbSLUserDefined.Location = new System.Drawing.Point(11, 46);
            this.rbSLUserDefined.Name = "rbSLUserDefined";
            this.rbSLUserDefined.Size = new System.Drawing.Size(85, 17);
            this.rbSLUserDefined.TabIndex = 1;
            this.rbSLUserDefined.Text = "User defined";
            this.rbSLUserDefined.UseVisualStyleBackColor = true;
            // 
            // rbSLSystemDefault
            // 
            this.rbSLSystemDefault.AutoSize = true;
            this.rbSLSystemDefault.Checked = true;
            this.rbSLSystemDefault.Location = new System.Drawing.Point(11, 23);
            this.rbSLSystemDefault.Name = "rbSLSystemDefault";
            this.rbSLSystemDefault.Size = new System.Drawing.Size(94, 17);
            this.rbSLSystemDefault.TabIndex = 0;
            this.rbSLSystemDefault.TabStop = true;
            this.rbSLSystemDefault.Text = "System default";
            this.rbSLSystemDefault.UseVisualStyleBackColor = true;
            // 
            // ManualOrderConfirmationWindow
            // 
            this.AcceptButton = this.btnConfirm;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(286, 208);
            this.Controls.Add(this.groupBox2);
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
            this.Text = "Order confirmation";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.RadioButton rbPTUserDefined;
        private System.Windows.Forms.RadioButton rbPTSystemDefault;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbStopLoss;
        private System.Windows.Forms.RadioButton rbSLUserDefined;
        private System.Windows.Forms.RadioButton rbSLSystemDefault;
    }
}