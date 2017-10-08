using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NeuroXChange.Model;
using NeuroXChange.Model.BioData;
using NeuroXChange.Controller;
using System.Drawing;
using NeuroXChange.Model.FixApi;

namespace NeuroXChange.View
{
    public class MainNeuroXView : IMainNeuroXModelObserver, IBioDataObserver, IFixApiObserver
    {
        private MainNeuroXModel model;
        private MainNeuroXController controller;
        private MainForm mainForm;
        private BuySellWindow buySellWindow;
        private CustomDialog customDialog;
        private string lastPriceBuy;

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
            model.fixApiModel.RegisterObserver(this);
        }

        public void RunApplication()
        {
            Application.Run(mainForm);
        }

        public void OnNext(MainNeuroXModelEvent modelEvent, object data)
        {
            buySellWindow.BeginInvoke(
                (Action)( () => {
                    switch (modelEvent)
                    {
                        case MainNeuroXModelEvent.StepInitialState:
                            {
                                buySellWindow.Hide();
                                customDialog.Hide();
                                break;
                            }
                        case MainNeuroXModelEvent.StepReadyToTrade:
                            {
                                buySellWindow.Show();
                                customDialog.Hide();
                                buySellWindow.labStepName.Text = "Ready To Trade";
                                buySellWindow.btnBuy.Enabled = false;
                                buySellWindow.btnSell.Enabled = false;
                                buySellWindow.btnBuy.BackColor = SystemColors.Control;
                                buySellWindow.btnSell.BackColor = SystemColors.Control;
                                break;
                            }
                        case MainNeuroXModelEvent.StepPreactivation:
                            {
                                buySellWindow.Show();
                                buySellWindow.labStepName.Text = "Preactivation";
                                buySellWindow.btnBuy.Enabled = true;
                                buySellWindow.btnSell.Enabled = true;
                                buySellWindow.btnBuy.BackColor = Color.RoyalBlue;
                                buySellWindow.btnSell.BackColor = Color.Red;
                                break;
                            }
                        case MainNeuroXModelEvent.StepDirectionConfirmed:
                            {
                                buySellWindow.Show();
                                buySellWindow.labStepName.Text = "Direction confirmed (buy)";
                                buySellWindow.btnBuy.Enabled = true;
                                buySellWindow.btnSell.Enabled = false;
                                buySellWindow.btnSell.BackColor = SystemColors.Control;
                                break;
                            }
                        case MainNeuroXModelEvent.StepExecuteOrder:
                            {
                                buySellWindow.Hide();
                                customDialog.Show();
                                customDialog.labInformation.Text = "Order Executed\r\nDirection: Buy\r\nContract size: 1\r\nPrice: " + lastPriceBuy;
                                break;
                            }
                        case MainNeuroXModelEvent.StepConfirmationFilled:
                            {
                                customDialog.Show();
                                customDialog.labInformation.Text = "Order filled\r\nDirection: Buy\r\nContract size: 1\r\nPrice: " + lastPriceBuy;
                                break;
                            }
                        case MainNeuroXModelEvent.LogicQueryDirection:
                            {
                                string message;
                                int sub_Protocol_ID = (int)data;
                                switch (sub_Protocol_ID)
                                {
                                    case 65:
                                        {
                                            message = "Direction";
                                            break;
                                        }
                                    case 66:
                                        {
                                            message = "LONG";
                                            break;
                                        }
                                    case 67:
                                        {
                                            message = "SHORT";
                                            break;
                                        }
                                    case 68:
                                        {
                                            message = "M_L_S_1";
                                            break;
                                        }
                                    case 69:
                                        {
                                            message = "M_L_S_2";
                                            break;
                                        }
                                    case 70:
                                        {
                                            message = "M_S_L_1";
                                            break;
                                        }
                                    case 71:
                                        {
                                            message = "M_S_L_2";
                                            break;
                                        }
                                    case 72:
                                        {
                                            message = "Singular LONG";
                                            break;
                                        }
                                    case 73:
                                        {
                                            message = "Singular SHORT";
                                            break;
                                        }
                                    default:
                                        {
                                            message = "Empty message!";
                                            break;
                                        }
                                }
                                MessageBox.Show(message, "NeuroXChange", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            }
                    }
                } ));
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

        public void OnNext(FixApiModelEvent modelEvent, object data)
        {
            mainForm.BeginInvoke(
                                (Action)(() =>
                               {
                                   //if (modelEvent == FixApiModelEvent.RawMessageReceived)
                                   //{
                                   //    if (mainForm.listBoxRawData.Items.Count > 0)
                                   //        mainForm.listBoxRawData.Items.Insert(0, data);
                                   //    else
                                   //        mainForm.listBoxRawData.Items.Add(data);
                                   //}
                                   if(modelEvent == FixApiModelEvent.PriceChanged)
                                   {
                                       var prices = (List<string>)data;
                                       buySellWindow.btnBuy.Text = "BUY\n\r    " + prices[0];
                                       buySellWindow.btnSell.Text = "          SELL\n\r   " + prices[1];
                                       lastPriceBuy = prices[0];
                                   }
                               }));
        }
    }
}
