using NeuroXChange.Controller;
using NeuroXChange.Model;
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
    public partial class ApplicationControlWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private MainNeuroXView view;
        private MainNeuroXController controller;

        public ApplicationControlWindow(MainNeuroXView view, MainNeuroXController controller)
        {
            this.view = view;
            this.controller = controller;
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            view.breathPacerWindow.breathPacerControl.Restart();
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            controller.WriteUserAction(UserAction.ApplicationControlStart);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            view.breathPacerWindow.breathPacerControl.Stop();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            controller.WriteUserAction(UserAction.ApplicationControlStop);
        }
    }
}
