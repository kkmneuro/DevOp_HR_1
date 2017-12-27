namespace NeuroXChange.View
{
    partial class CustomDialogWindow
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
            this.labInformation = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.secondElapsedTimer = new System.Windows.Forms.Timer(this.components);
            this.secondsRemainLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labInformation
            // 
            this.labInformation.AutoSize = true;
            this.labInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labInformation.Location = new System.Drawing.Point(7, 6);
            this.labInformation.Name = "labInformation";
            this.labInformation.Size = new System.Drawing.Size(45, 16);
            this.labInformation.TabIndex = 0;
            this.labInformation.Text = "label1";
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Location = new System.Drawing.Point(87, 77);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 24);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // secondElapsedTimer
            // 
            this.secondElapsedTimer.Interval = 1000;
            this.secondElapsedTimer.Tick += new System.EventHandler(this.secondElapsedTime_Tick);
            // 
            // secondsRemainLabel
            // 
            this.secondsRemainLabel.AutoSize = true;
            this.secondsRemainLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.secondsRemainLabel.Location = new System.Drawing.Point(7, 104);
            this.secondsRemainLabel.Name = "secondsRemainLabel";
            this.secondsRemainLabel.Size = new System.Drawing.Size(178, 13);
            this.secondsRemainLabel.TabIndex = 2;
            this.secondsRemainLabel.Text = "Message will be closed in x seconds";
            // 
            // CustomDialogWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 122);
            this.Controls.Add(this.secondsRemainLabel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.labInformation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomDialogWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Order confirmation";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CustomDialog_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label labInformation;
        public System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Timer secondElapsedTimer;
        private System.Windows.Forms.Label secondsRemainLabel;
    }
}