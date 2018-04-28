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
        private MainNeuroXModel model;
        private MainNeuroXController controller;

        public ApplicationControlWindow(MainNeuroXModel model, MainNeuroXController controller)
        {
            this.model = model;
            this.controller = controller;
            InitializeComponent();
        }
    }
}
