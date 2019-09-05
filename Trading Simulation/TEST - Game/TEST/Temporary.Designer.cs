namespace TEST
{
    partial class Temporary
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.temperatureLimitsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.upperLimitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1UpperTemp = new System.Windows.Forms.ToolStripTextBox();
            this.lowerLimitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox2LowerTemp = new System.Windows.Forms.ToolStripTextBox();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.temperatureLimitsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(178, 48);
            // 
            // temperatureLimitsToolStripMenuItem
            // 
            this.temperatureLimitsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.upperLimitToolStripMenuItem,
            this.lowerLimitToolStripMenuItem});
            this.temperatureLimitsToolStripMenuItem.Name = "temperatureLimitsToolStripMenuItem";
            this.temperatureLimitsToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.temperatureLimitsToolStripMenuItem.Text = "Temperature Limits";
            // 
            // upperLimitToolStripMenuItem
            // 
            this.upperLimitToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1UpperTemp});
            this.upperLimitToolStripMenuItem.Name = "upperLimitToolStripMenuItem";
            this.upperLimitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.upperLimitToolStripMenuItem.Text = "Upper Limit";
            // 
            // toolStripTextBox1UpperTemp
            // 
            this.toolStripTextBox1UpperTemp.Name = "toolStripTextBox1UpperTemp";
            this.toolStripTextBox1UpperTemp.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBox1UpperTemp.ModifiedChanged += new System.EventHandler(this.toolStripTextBox1_ModifiedChanged);
            // 
            // lowerLimitToolStripMenuItem
            // 
            this.lowerLimitToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox2LowerTemp});
            this.lowerLimitToolStripMenuItem.Name = "lowerLimitToolStripMenuItem";
            this.lowerLimitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.lowerLimitToolStripMenuItem.Text = "Lower Limit";
            // 
            // toolStripTextBox2LowerTemp
            // 
            this.toolStripTextBox2LowerTemp.Name = "toolStripTextBox2LowerTemp";
            this.toolStripTextBox2LowerTemp.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBox2LowerTemp.ModifiedChanged += new System.EventHandler(this.toolStripTextBox2_ModifiedChanged);
            // 
            // Temporary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "Temporary";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem temperatureLimitsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem upperLimitToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1UpperTemp;
        private System.Windows.Forms.ToolStripMenuItem lowerLimitToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox2LowerTemp;
    }
}
