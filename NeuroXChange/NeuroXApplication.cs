using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NeuroXChange.Model;
using NeuroXChange.Controller;
using NeuroXChange.View;
using PostTradingAnalysis;

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
                Globals.CurrentLoginStatus = LoginStatus.Failure;
                model = new MainNeuroXModel();
                if (!model.isStateGood)
                    return;
                Application.ApplicationExit += new EventHandler(model.StopProcessing);

                controller = new MainNeuroXController(model);
                view = new MainNeuroXView(model, controller);
                if (!view.isStateGood)
                    return;
                
                model.Synchronize();
                model.StartProcessing();


                //MessageBox.Show(view.status.ToString());

                //if (view.status== Common.SelectionEnum.Training||
                //    view.status== Common.SelectionEnum.Trade)                   
                //view.RunApplication();

                Application.Run(view.selectionWindow);

              
            } catch (Exception e)
            {

                StopProcessing();
                MessageBox.Show(e.Message, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                if (view.status == Common.SelectionEnum.Reports)
                {

                    PostTradingAnalysisApplication pta = new PostTradingAnalysisApplication();
                    PostTradingAnalysis.MainWindow mwin = new PostTradingAnalysis.MainWindow(pta);
                    Application.Run(mwin);
                    view.mainWindow.Close();
                }
            }
            catch { }
        }

        public void StopProcessing()
        {
            model.StopProcessing(null, null);
        }
    }
}
