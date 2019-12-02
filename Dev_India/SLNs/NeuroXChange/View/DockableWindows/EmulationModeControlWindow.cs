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
    public partial class EmulationModeControlWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private MainNeuroXModel model;
        private MainNeuroXController controller;

        public EmulationModeControlWindow(MainNeuroXModel model, MainNeuroXController controller)
        {
            this.model = model;
            this.controller = controller;
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            controller.StartEmulation();
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            controller.PauseEmulation();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            controller.NextTickEmulation();
        }

        private void tickSizeUpDown_ValueChanged(object sender, EventArgs e)
        {
            controller.ChangeEmulationModeTickInterval((int)tickSizeUpDown.Value);
        }
    }
}
