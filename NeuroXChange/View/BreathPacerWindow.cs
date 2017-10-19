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
    public partial class BreathPacerWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public BreathPacerWindow()
        {
            InitializeComponent();
            breathPacerControl.Start();
        }

        private void BreathPacerWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Hide();
                e.Cancel = true;
            }
        }
    }
}
