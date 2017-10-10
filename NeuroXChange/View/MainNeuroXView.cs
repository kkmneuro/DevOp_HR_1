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

        private string[] lastPrice = { "0.0", "0.0" };     // array of 2 strings [buy price, sell price]
        private string[] directionName = { "Buy", "Sell" };

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
                                int direction = (int)data;
                                buySellWindow.Show();
                                buySellWindow.labStepName.Text = string.Format("Direction confirmed ({0})", directionName[direction]);
                                buySellWindow.btnBuy.Enabled = direction == 0;
                                buySellWindow.btnSell.Enabled = direction == 1;
                                if (direction == 0)
                                {
                                    buySellWindow.btnSell.BackColor = SystemColors.Control;
                                }
                                else
                                {
                                    buySellWindow.btnBuy.BackColor = SystemColors.Control;
                                }
                                break;
                            }
                        case MainNeuroXModelEvent.StepExecuteOrder:
                            {
                                int direction = (int)data;
                                buySellWindow.Hide();
                                customDialog.Show();
                                customDialog.labInformation.Text = string.Format("Order Executed\r\nDirection: {0}\r\nContract size: 1\r\nPrice: {1}",
                                    directionName[direction], lastPrice[direction]);
                                break;
                            }
                        case MainNeuroXModelEvent.StepConfirmationFilled:
                            {
                                int direction = (int)data;
                                customDialog.Show();
                                customDialog.labInformation.Text = string.Format("Order Filled\r\nDirection: {0}\r\nContract size: 1\r\nPrice: {1}",
                                    directionName[direction], lastPrice[direction]);
                                break;
                            }
                        case MainNeuroXModelEvent.LogicQueryDirection:
                            {
                                int sub_Protocol_ID = (int)data;
                                string[] messages = { "Direction", "LONG" , "SHORT" , "M_L_S_1", "M_L_S_2", "M_S_L_1", "M_S_L_2" , "Singular LONG" , "Singular SHORT" };
                                string message = 65 <= sub_Protocol_ID && sub_Protocol_ID <= 73 ? messages[sub_Protocol_ID - 65] : "Empty message!";
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
        }

        public void OnNext(FixApiModelEvent modelEvent, object data)
        {
            if (modelEvent == FixApiModelEvent.PriceChanged)
            {
                mainForm.BeginInvoke(
                                (Action)(() =>
                               {
                                   var prices = (string[])data;
                                   buySellWindow.btnBuy.Text = "BUY\n\r    " + prices[0];
                                   buySellWindow.btnSell.Text = "          SELL\n\r   " + prices[1];
                                   lastPrice = prices;
                               }));
            }
        }
    }
}
