using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NeuroXChange.Controller;
using NeuroXChange.Model;
using NeuroXChange.Model.BioData;
using NeuroXChange.Model.BioDataProcessors;
using NeuroXChange.Model.FixApi;

namespace NeuroXChange.View
{
    public class MainNeuroXView : IMainNeuroXModelObserver, IBioDataObserver, IFixApiObserver, IBioDataProcessorEventObserver
    {
        private MainNeuroXModel model;
        private MainNeuroXController controller;

        // main application window
        public MainWindow mainWindow { get; private set; }

        // working application windows
        public RawInformationWindow rawInformationWindow { get; private set; }
        public BuySellWindow buySellWindow { get; private set; }
        public ChartsWindow chartsWindow { get; private set; }

        // other windows
        public CustomDialogWindow customDialogWindow { get; private set; }
        public LogoWindow logoWindow { get; private set; }

        private string[] lastPrice = { "0.0", "0.0" };     // array of 2 strings [buy price, sell price]
        private string[] directionName = { "Buy", "Sell" };

        public MainNeuroXView(MainNeuroXModel model, MainNeuroXController controller)
        {
            this.model = model;
            this.controller = controller;

            mainWindow = new MainWindow(this);

            rawInformationWindow = new RawInformationWindow();

            buySellWindow = new BuySellWindow();
            buySellWindow.Show();
            buySellWindow.Hide();

            chartsWindow = new ChartsWindow();

            logoWindow = new LogoWindow();
            logoWindow.ShowDialog(mainWindow);

            customDialogWindow = new CustomDialogWindow();
            customDialogWindow.Show();
            customDialogWindow.Hide();

            model.RegisterObserver(this);
            model.bioDataProvider.RegisterObserver(this);
            model.fixApiModel.RegisterObserver(this);
            model.heartRateProcessor.RegisterObserver(this);
        }

        public void RunApplication()
        {
            Application.Run(mainWindow);
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
                                customDialogWindow.Hide();
                                break;
                            }
                        case MainNeuroXModelEvent.StepReadyToTrade:
                            {
                                buySellWindow.Show();
                                customDialogWindow.Hide();
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
                                customDialogWindow.Show();
                                customDialogWindow.labInformation.Text = string.Format("Order Executed\r\nDirection: {0}\r\nContract size: 1\r\nPrice: {1}",
                                    directionName[direction], lastPrice[direction]);
                                break;
                            }
                        case MainNeuroXModelEvent.StepConfirmationFilled:
                            {
                                int direction = (int)data;
                                customDialogWindow.Show();
                                customDialogWindow.labInformation.Text = string.Format("Order Filled\r\nDirection: {0}\r\nContract size: 1\r\nPrice: {1}",
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

        public void OnNext(BioData data)
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
            builder.Append("Data: " + data.data);
            mainWindow.BeginInvoke(
                (Action)(() =>
                {
                    rawInformationWindow.bioDataRTB.Text = builder.ToString();
                    var temperaturePoints = chartsWindow.heartRateChart.Series["Temperature"].Points;
                    var hrPoints = chartsWindow.heartRateChart.Series["Heart Rate"].Points;
                    var skinCondPoints = chartsWindow.heartRateChart.Series["Skin Conductance"].Points;
                    temperaturePoints.AddXY(data.time, data.temperature);
                    hrPoints.AddXY(data.time, data.hartRate);
                    skinCondPoints.AddXY(data.time, data.skinConductance);
                    if (hrPoints.Count > 3000)
                    {
                        temperaturePoints.RemoveAt(0);
                        hrPoints.RemoveAt(0);
                        skinCondPoints.RemoveAt(0);
                        if (chartsWindow.Visible)
                        {
                            chartsWindow.heartRateChart.ChartAreas[0].RecalculateAxesScale();
                            chartsWindow.heartRateChart.ChartAreas[1].RecalculateAxesScale();
                            chartsWindow.heartRateChart.ChartAreas[2].RecalculateAxesScale();
                        }
                    }
                }));
        }

        public void OnNext(FixApiModelEvent modelEvent, object data)
        {
            if (modelEvent == FixApiModelEvent.PriceChanged)
            {
                mainWindow.BeginInvoke(
                                (Action)(() =>
                               {
                                   var prices = (string[])data;
                                   buySellWindow.btnBuy.Text = "BUY\n\r    " + prices[0];
                                   buySellWindow.btnSell.Text = "          SELL\n\r   " + prices[1];
                                   lastPrice = prices;
                               }));
            }
        }

        public void OnNext(BioDataProcessorEvent bioDataProcessorEvent, object data)
        {
            mainWindow.BeginInvoke(
                            (Action)(() =>
                            {
                                HeartRateInfo hrInfo = (HeartRateInfo)data;
                                switch (bioDataProcessorEvent)
                                {
                                    case BioDataProcessorEvent.HeartRateRawStatistics:
                                        {
                                            StringBuilder builder = new StringBuilder();
                                            builder.Append(string.Format("Heart rate 2 min average: {0:0.##}\r\n", hrInfo.heartRate2minAverage));
                                            builder.Append(string.Format("Heart rate innter state: {0}\r\n", hrInfo.heartRateInnerState));
                                            builder.Append(string.Format("Oscillations per min, 3 min average: {0:0.##}\r\n", hrInfo.oscillations3minAverage));
                                            builder.Append(string.Format("Oscillations per min, 5 min average: {0:0.##}", hrInfo.oscillations5minAverage));
                                            rawInformationWindow.heartRateRTB.Text = builder.ToString();

                                            if (hrInfo.heartRate2minAverage > 0)
                                            {
                                                var hrPoints = chartsWindow.heartRateChart.Series["AVG Heart Rate"].Points;
                                                hrPoints.AddXY(hrInfo.time, hrInfo.heartRate2minAverage);
                                                if (hrPoints.Count > 3000)
                                                {
                                                    hrPoints.RemoveAt(0);
                                                    if (chartsWindow.Visible)
                                                    {
                                                        chartsWindow.heartRateChart.ChartAreas[1].RecalculateAxesScale();
                                                    }
                                                }
                                            }

                                            break;
                                        }
                                }
                            }));
        }
    }
}
