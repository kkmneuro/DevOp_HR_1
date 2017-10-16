using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuroXChange.View
{
    public partial class LogoWindow : Form
    {
        public LogoWindow()
        {
            InitializeComponent();
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private Point lastMouseLocation;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            lastMouseLocation = e.Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Left += e.X - lastMouseLocation.X;
                Top += e.Y - lastMouseLocation.Y;
            }
        }
    }
}
