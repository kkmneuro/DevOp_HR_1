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
        private BuySellWindow buySellWindow;
        private CustomDialog customDialog;

        public MainNeuroXView(MainNeuroXModel model, MainNeuroXController controller)
        {
            this.model = model;
            this.controller = controller;

            mainForm = new MainForm();
            buySellWindow = new BuySellWindow();
            buySellWindow.Show();
            buySellWindow.Hide();

            customDialog = new CustomDialog();
            customDialog.Show();
            customDialog.Hide();

            model.RegisterObserver(this);
            model.bioDataProvider.RegisterObserver(this);
        }

        public void RunApplication()
        {
            Application.Run(mainForm);
        }

        public void OnNext(MainNeuroXModelEvent modelEvent, object data)
        {
            switch(modelEvent)
            {
                case MainNeuroXModelEvent.StepInitialState:
                    {
                        buySellWindow.BeginInvoke((Action)(
                            () => {
                                buySellWindow.Hide();
                                customDialog.Hide();
                            }
                        ));
                        break;
                    }
                case MainNeuroXModelEvent.StepReadyToTrade:
                    {
                        buySellWindow.BeginInvoke((Action)(
                            () => {
                                buySellWindow.Show();
                                customDialog.Hide();
                                buySellWindow.Text = "Ready To Trade";
                                buySellWindow.btnBuy.Enabled = false;
                                buySellWindow.btnSell.Enabled = false;
                            }
                        ));
                        break;
                    }
                case MainNeuroXModelEvent.StepPreactivation:
                    {
                        buySellWindow.BeginInvoke((Action)(
                            () => {
                                buySellWindow.Show();
                                buySellWindow.Text = "Preactivation";
                                buySellWindow.btnBuy.Enabled = true;
                                buySellWindow.btnSell.Enabled = true;
                            }
                        ));
                        break;
                    }
                case MainNeuroXModelEvent.StepDirectionConfirmed:
                    {
                        buySellWindow.BeginInvoke((Action)(
                            () => {
                                buySellWindow.Show();
                                buySellWindow.Text = "Direction confirmed (buy)";
                                buySellWindow.btnBuy.Enabled = true;
                                buySellWindow.btnSell.Enabled = false;
                            }
                        ));
                        break;
                    }
                case MainNeuroXModelEvent.StepExecuteOrder:
                    {
                        buySellWindow.BeginInvoke((Action)(
                            () =>
                            {
                                buySellWindow.Hide();
                                customDialog.Show();
                                customDialog.Text = "Execute order";
                                customDialog.labInformation.Text = "Order Executed\r\nDirection: Buy\r\nContract size: 1";
                            }
                        ));
                        break;
                    }
                case MainNeuroXModelEvent.StepConfirmationFilled:
                    {
                        buySellWindow.BeginInvoke((Action)(
                            () =>
                            {
                                customDialog.Show();
                                customDialog.Text = "Order filled";
                                customDialog.labInformation.Text = "Order filled\r\nDirection: Buy\r\nContract size: 1\r\nPrice: Unknown";
                            }
                        ));
                        break;
                    }
            }
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
            mainForm.BeginInvoke((Action)(() => mainForm.bioDataRTB.Text = builder.ToString()));
            //mainForm.bioDataRTB.Text = builder.ToString();
        }
    }
}
