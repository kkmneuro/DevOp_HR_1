using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace TEST.Helpers
{
    class ContextMenu
    {

        public TemperatureLimits TempLimits { get; set; }
        private BIODataWindow bioData;
        private BIOHeartRateAxeleration bioHartAxeleration;
        private Pacer pacer;


        public System.Windows.Forms.ContextMenuStrip contextMenuStrip1; // main class for context menu, assigne for all windows where it is needed
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showOnTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem1;
  //      private System.Windows.Forms.ToolStripMenuItem hideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hartAxelerationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hartAccOnTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pacerOnTop;

        private System.Windows.Forms.ToolStripMenuItem temperatureLimitsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem upperLimitToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1UpperTemp;
        private System.Windows.Forms.ToolStripMenuItem lowerLimitToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox2LowerTemp;


        public ContextMenu(BIODataWindow biod, BIOHeartRateAxeleration bioha, Pacer pacer)
        {
            TempLimits = new TemperatureLimits();
            TempLimits.LowerLimit = 20;
            TempLimits.UpperLimit = 40;
            bioData = biod;
            bioHartAxeleration = bioha;
            this.pacer = pacer;
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(); 
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
    //        this.hideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hartAxelerationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hartAccOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();

            this.temperatureLimitsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.upperLimitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1UpperTemp = new System.Windows.Forms.ToolStripTextBox();
            this.lowerLimitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox2LowerTemp = new System.Windows.Forms.ToolStripTextBox();

            this.pacerOnTop = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();

            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
      //      this.hideToolStripMenuItem,
            this.hartAxelerationToolStripMenuItem,
            hartAccOnTopToolStripMenuItem,
            this.pacerOnTop});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 70);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showOnTopToolStripMenuItem,
            this.showToolStripMenuItem1});
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.showToolStripMenuItem.Text = "BIO Data";
            // 
            // showOnTopToolStripMenuItem
            // 
            this.showOnTopToolStripMenuItem.Name = "showOnTopToolStripMenuItem";
            this.showOnTopToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.showOnTopToolStripMenuItem.Text = "Show On Top";
            this.showOnTopToolStripMenuItem.Click += new System.EventHandler(this.showOnTopToolStripMenuItem_Click);
            // 
            // showToolStripMenuItem1
            // 
            this.showToolStripMenuItem1.Name = "showToolStripMenuItem1";
            this.showToolStripMenuItem1.Size = new System.Drawing.Size(146, 22);
            this.showToolStripMenuItem1.Text = "Show";
            this.showToolStripMenuItem1.Click += new System.EventHandler(this.showToolStripMenuItem1_Click);
            // 
            // hideToolStripMenuItem
            // 
     /*       this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            this.hideToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.hideToolStripMenuItem.Text = "Hide";
            this.hideToolStripMenuItem.Click += new System.EventHandler(this.hideToolStripMenuItem_Click);
            */

            // 
            // hartAxelerationToolStripMenuItem
            // 
            this.hartAxelerationToolStripMenuItem.Name = "hartAxelerationToolStripMenuItem";
            this.hartAxelerationToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.hartAxelerationToolStripMenuItem.Text = "NeurAlgoΩ";
            this.hartAxelerationToolStripMenuItem.Click += new System.EventHandler(this.hartAxelerationToolStripMenuItem_Click);

            // 
            // hartAccOnTopToolStripMenuItem
            // 
            this.hartAccOnTopToolStripMenuItem.Name = "hartAxelerationToolStripMenuItem";
            this.hartAccOnTopToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.hartAccOnTopToolStripMenuItem.Text = "NeurAlgoΩ On Top";
            this.hartAccOnTopToolStripMenuItem.Click += new System.EventHandler(this.hartAccelerationOnTopToolStripMenuItem_Click);

            //
            //            pacerOnTop
            //
            this.pacerOnTop.Name = "pacerOnTopToolStripMenuItem";
            this.pacerOnTop.Size = new System.Drawing.Size(152, 22);
            this.pacerOnTop.Text = "Pacer On Top";
            this.pacerOnTop.Click += PacerOnTop_Click;


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
            this.toolStripTextBox1UpperTemp.TextChanged += new System.EventHandler(this.toolStripTextBox1_TextChanged);
            this.toolStripTextBox1UpperTemp.Text = "38";
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
            this.toolStripTextBox2LowerTemp.TextChanged += new System.EventHandler(this.toolStripTextBox2_TextChanged);
            this.toolStripTextBox2LowerTemp.Text = "-20";
            this.contextMenuStrip1.ResumeLayout(false);

        }

        private void PacerOnTop_Click(object sender, EventArgs e)
        {
            pacer.TopMost = true;
            pacer.Show();
        }

        private void showOnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bioData.TopMost = true;
            bioData.Show();
        }

        private void showToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bioData.TopMost = false;
            bioData.Show();
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bioData.Hide();
        }

        
        /// <summary>
        /// Show a window with a hart rate axeleration info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hartAxelerationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bioHartAxeleration.TopMost = false;
            bioHartAxeleration.Show();
        }

        /// <summary>
        /// Show a window with a hart rate axeleration info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hartAccelerationOnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bioHartAxeleration.TopMost = true;
            bioHartAxeleration.Show();
        }

        private void toolStripTextBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (toolStripTextBox2LowerTemp.Text != "-")
                TempLimits.LowerLimit = System.Convert.ToDouble(toolStripTextBox2LowerTemp.Text);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Not a Valid Number!", "Eror", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripTextBox2LowerTemp.Text = "";
            }

        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (toolStripTextBox1UpperTemp.Text != "-")
                TempLimits.UpperLimit = System.Convert.ToDouble(toolStripTextBox1UpperTemp.Text);
            }
            catch (Exception ee)
            {
                MessageBox.Show("Not a Valid Number!", "Eror", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripTextBox1UpperTemp.Text = "";

            }
        }

    }
}
