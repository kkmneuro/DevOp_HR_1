using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NeuroXChange.Model;
using NeuroXChange.Model.BioData;
using NeuroXChange.Controller;

namespace NeuroXChange.View
{
    public class MainNeuroXView : IMainNeuroXModelObserver, IBioDataObserver
    {
        private MainNeuroXModel model;
        private MainNeuroXController controller;
        private MainForm mainForm;

        public MainNeuroXView(MainNeuroXModel model, MainNeuroXController controller)
        {
            this.model = model;
            this.controller = controller;

            mainForm = new MainForm();

            model.bioDataProvider.RegisterObserver(this);
        }

        public void RunApplication()
        {
            Application.Run(mainForm);
        }

        public void OnNext()
        {
            throw new NotImplementedException();
        }

        public void OnNext(Sub_Component_Protocol_Psychophysiological_Session_Data_TPS data)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Psychophysiological_Session_Data_ID: " + data.psychophysiological_Session_Data_ID + "\r\n");
            builder.Append("Time: " + data.time + "\r\n");
            builder.Append("Temperature: " + data.temperature.ToString("0.##") + "\r\n");
            builder.Append("HartRate: " + data.hartRate.ToString("0.##") + "\r\n");
            builder.Append("SkinConductance: " + data.skinConductance.ToString("0.##") + "\r\n");
            builder.Append("AccX: " + data.accX.ToString("0.##") + "\r\n");
            builder.Append("AccY: " + data.accY.ToString("0.##") + "\r\n");
            builder.Append("AccZ: " + data.accZ.ToString("0.##") + "\r\n");
            builder.Append("Session_Component_ID: " + data.session_Component_ID + "\r\n");
            builder.Append("Sub_Component_ID: " + data.sub_Component_ID + "\r\n");
            builder.Append("Sub_Component_Protocol_ID: " + data.sub_Component_Protocol_ID + "\r\n");
            builder.Append("Sub_Protocol_ID: " + data.sub_Protocol_ID + "\r\n");
            builder.Append("Participant_ID: " + data.participant_ID + "\r\n");
            builder.Append("Data: " + data.data + "\r\n");
            mainForm.bioDataRTB.Text = builder.ToString();
        }
    }
}
