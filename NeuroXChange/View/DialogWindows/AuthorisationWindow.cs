using System;
using System.Drawing;
using System.Windows.Forms;
using NeuroXChange.Model;

namespace NeuroXChange.View.DialogWindows
{
    public partial class AuthorisationWindow : Form
    {
        private MainNeuroXModel model;

        public AuthorisationWindow(MainNeuroXModel model)
        {
            InitializeComponent();

            this.model = model;
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
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

        private void exitBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Hide();
        }
    }
}
