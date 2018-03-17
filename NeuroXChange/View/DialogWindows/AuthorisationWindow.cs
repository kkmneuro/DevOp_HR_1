using System;
using System.Drawing;
using System.Windows.Forms;
using NeuroXChange.Model;
using NeuroXChange.Model.ServerConnection;

namespace NeuroXChange.View.DialogWindows
{
    public partial class AuthorisationWindow : Form
    {
        private MainNeuroXModel model;
        private ServerConnector serverConnector;

        public AuthorisationWindow(MainNeuroXModel model)
        {
            InitializeComponent();

            this.model = model;
            this.serverConnector = model.serverConnector;

            cbSaveCredentials.Checked = serverConnector.SaveCredentials;
            tbLogin.Text = serverConnector.UserLogin;
            tbPassword.Text = serverConnector.UserPassword;
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            serverConnector.SaveCredentials = cbSaveCredentials.Checked;
            serverConnector.UserLogin = tbLogin.Text;
            serverConnector.UserPassword = tbPassword.Text;

            string errorMessage;
            if (!serverConnector.Connect(out errorMessage))
            {
                MessageBox.Show(errorMessage, "Authorisation error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult = DialogResult.OK;

            serverConnector.UpdateINICredentials();

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
