using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TEST
{
    public partial class Temporary : UserControl
    {
        public Temporary()
        {
            InitializeComponent();
        }



        private void toolStripTextBox2_ModifiedChanged(object sender, EventArgs e)
        {
            try
            {
                System.Convert.ToDouble( toolStripTextBox2LowerTemp.Text);
            }       
            catch(Exception exc)
            {
                MessageBox.Show("Not a Valid Number!", "Eror", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void toolStripTextBox1_ModifiedChanged(object sender, EventArgs e)
        {
            try
            {
                System.Convert.ToDouble(toolStripTextBox2LowerTemp.Text);
            }
            catch (Exception ee)
            {
                MessageBox.Show("Not a Valid Number!", "Eror", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
