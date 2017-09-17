using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NeuroXChange.Model;
using NeuroXChange.Controller;
using NeuroXChange.View;

namespace NeuroXChange
{
    public class NeuroXApplication
    {
        private MainNeuroXModel model;
        private MainNeuroXController controller;
        private MainNeuroXView view;

        public NeuroXApplication()
        {
            model = new MainNeuroXModel();
            controller = new MainNeuroXController(model);
            view = new MainNeuroXView(model, controller);

            view.RunApplication();
        }
    }
}
