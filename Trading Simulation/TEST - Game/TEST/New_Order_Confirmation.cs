using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TEST
{
    public partial class New_Order_Confirmation : Form
    {
        public New_Order_Confirmation()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            breathPacerControl1.Stop();
            Close();
        }

        private void New_Order_Confirmation_Load(object sender, EventArgs e)
        {
            breathPacerControl1.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
