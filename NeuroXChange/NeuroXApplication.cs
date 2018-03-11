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
            try
            {
                model = new MainNeuroXModel();
                if (!model.isStateGood)
                    return;
                Application.ApplicationExit += new EventHandler(model.StopProcessing);

                controller = new MainNeuroXController(model);

                view = new MainNeuroXView(model, controller);
                if (!view.isStateGood)
                    return;

                model.StartProcessing();

                view.RunApplication();
            } catch (Exception e)
            {
                StopProcessing();
                MessageBox.Show(e.Message, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void StopProcessing()
        {
            model.StopProcessing(null, null);
        }
    }
}
