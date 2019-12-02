using System;
using System.Drawing;
using System.Windows.Forms;
using NeuroXChange.Model;
using NeuroXChange.Model.ServerConnection;
using System.IO.Ports;
using NeuroXChange.Common;
using System.Threading.Tasks;

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

            //SerialPortInterface sp = new Common.SerialPortInterface();
            //sp.Open();
            
            cmbPorts.Items.AddRange(SerialPort.GetPortNames());
            //cmbPorts.Items.Add(sp.PortName);
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            serverConnector.SaveCredentials = cbSaveCredentials.Checked;
            serverConnector.UserLogin = tbLogin.Text;
            serverConnector.UserPassword = tbPassword.Text;

            if (cmbPorts.SelectedItem != null)
            {
                Globals.SelectedPort = cmbPorts.SelectedItem.ToString();
                serverConnector.UsbPort = cmbPorts.SelectedItem.ToString();
            }
            bool connect= serverConnector.Connect();


            Globals.CurrentUserId = tbLogin.Text.Trim();

            //System.Threading.Thread.Sleep(5000);           


            if (!connect)
            {
                MessageBox.Show(serverConnector.ErrorMessage, "Authorisation error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            //write to log
            NeuroXChange.Model.Globals.LoggerClient.WriteLog(Globals.AccountId.ToString(), "Login Canceled", DateTime.Now);
            DialogResult = DialogResult.Cancel;
            Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AuthorizationWindow_Load(object sender, EventArgs e)
        {
            Globals.SelectedPort = model.iniFileReader.Read("TPSUSBPort", "BioData", "").Replace("\\\\.\\","");

            if (Globals.SelectedPort.Trim().Length > 0)
                cmbPorts.SelectedItem = Globals.SelectedPort;
        }
    }
}
