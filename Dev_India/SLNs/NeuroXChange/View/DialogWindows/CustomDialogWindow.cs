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
    public partial class CustomDialogWindow : Form
    {
        private int seconds;

        public CustomDialogWindow()
        {
            InitializeComponent();
        }

        public void ShowWithSeconds(int seconds)
        {
            this.seconds = seconds;
            UpdateTimeLabel();
            secondElapsedTimer.Enabled = true;
            Show();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void CustomDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                secondElapsedTimer.Enabled = false;
                Hide();
                e.Cancel = true;
            }
        }

        private void UpdateTimeLabel()
        {
            secondsRemainLabel.Text = "Message will be closed in " + seconds.ToString() + " seconds";
        }

        private void secondElapsedTime_Tick(object sender, EventArgs e)
        {
            seconds--;
            if (seconds == 0)
            {
                secondElapsedTimer.Enabled = false;
                Hide();
                return;
            }

            UpdateTimeLabel();
        }
    }
}
