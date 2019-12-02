using System;
using System.Windows.Forms;

namespace NeuroXChange.View.DialogWindows
{
    public partial class SynchronizationWindow : Form
    {
        public SynchronizationWindow()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
