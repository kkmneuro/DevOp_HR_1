using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NeuroXChange.Controller;
using NeuroXChange.Model;
using NeuroXChange.Model.BioData;
using NeuroXChange.Model.BehavioralModeling.BioDataProcessors;
using NeuroXChange.Model.FixApi;
using System.Data;
using System.Threading;
using WeifenLuo.WinFormsUI.Docking;
using NeuroXChange.Model.BehavioralModeling.BehavioralModels;

namespace NeuroXChange.View
{
    public class MainNeuroXView : IMainNeuroXModelObserver, IBioDataObserver, IFixApiObserver
    {
        private MainNeuroXModel model;
        private MainNeuroXController controller;
        private Thread mainNeuroXViewThread;

        // main application window
        public MainWindow mainWindow { get; private set; }
        public bool allWindowsOnTop { get; private set; }

        // working application windows
        public RawInformationWindow rawInformationWindow { get; private set; }
        public NewOrderWindow newOrderWindow { get; private set; }
        public ChartsWindow chartsWindow { get; private set; }
        public BreathPacerWindow breathPacerWindow { get; private set; }
        public IndicatorsWindow indicatorsWindow { get; private set; }
        public BehavioralModelsWindow behavioralModelWindow { get; private set; }

        // other windows
        public CustomDialogWindow customDialogWindow { get; private set; }
        public LogoWindow logoWindow { get; private set; }

        private string[] lastPrice = { "0.0", "0.0" };     // array of 2 strings [buy price, sell price]
        private string[] directionName = { "Buy", "Sell" };

        public MainNeuroXView(MainNeuroXModel model, MainNeuroXController controller)
        {
            this.model = model;
            this.controller = controller;
            mainNeuroXViewThread = Thread.CurrentThread;

            mainWindow = new MainWindow(this, model.iniFileReader);
            mainWindow.modeNameSL.Text = "Mode: " + (model.emulationOnHistory ? "emulation on history" : "real-time");

            rawInformationWindow = new RawInformationWindow();
            rawInformationWindow.Owner = mainWindow;

            newOrderWindow = new NewOrderWindow();
            newOrderWindow.Owner = mainWindow;

            chartsWindow = new ChartsWindow();
            chartsWindow.Owner = mainWindow;

            breathPacerWindow = new BreathPacerWindow();
            breathPacerWindow.Owner = mainWindow;

            indicatorsWindow = new IndicatorsWindow();
            indicatorsWindow.Owner = mainWindow;

            behavioralModelWindow = new BehavioralModelsWindow(controller);
            behavioralModelWindow.Owner = mainWindow;
            behavioralModelWindow.dataGridView.AutoGenerateColumns = true;
            behavioralModelWindow.dataGridView.DataSource = model.behavioralModelsContainer.behavioralModelsDataSet;
            behavioralModelWindow.dataGridView.DataMember = model.behavioralModelsContainer.behavioralModelsDataTableName;

            logoWindow = new LogoWindow();
            logoWindow.ShowDialog(mainWindow);

            customDialogWindow = new CustomDialogWindow();
            customDialogWindow.Show();
            customDialogWindow.Hide();

            model.RegisterObserver(this);
            model.bioDataProvider.RegisterObserver(this);
            model.fixApiModel.RegisterObserver(this);

            allWindowsOnTop = Boolean.Parse(model.iniFileReader.Read("AllWindowsOnTop", "Interface"));
            if (allWindowsOnTop)
            {
                mainWindow.TopMost = true;
            }

            // move view to initial state
            UpdateInterfaceFromModelState(BehavioralModelState.InitialState);
        }

        public void RunApplication()
        {
            Application.Run(mainWindow);
        }

        private void UpdateInterfaceFromModelState(BehavioralModelState state)
        {
            newOrderWindow.labStepName.Text = BehavioralModelStateHelper.StateToString(state);
            var activeModel = model.behavioralModelsContainer.behavioralModels[
                                     model.behavioralModelsContainer.ActiveBehavioralModelIndex];

            switch (state)
            {
                case BehavioralModelState.InitialState:
                    {
                        newOrderWindow.btnBuy.Enabled = false;
                        newOrderWindow.btnSell.Enabled = false;
                        newOrderWindow.btnBuy.BackColor = SystemColors.Control;
                        newOrderWindow.btnSell.BackColor = SystemColors.Control;
                        break;
                    }
                case BehavioralModelState.ReadyToTrade:
                    {
                        newOrderWindow.Show();
                        newOrderWindow.btnBuy.Enabled = false;
                        newOrderWindow.btnSell.Enabled = false;
                        newOrderWindow.btnBuy.BackColor = SystemColors.Control;
                        newOrderWindow.btnSell.BackColor = SystemColors.Control;
                        break;
                    }
                case BehavioralModelState.Preactivation:
                    {
                        newOrderWindow.Show();
                        newOrderWindow.btnBuy.Enabled = true;
                        newOrderWindow.btnSell.Enabled = true;
                        newOrderWindow.btnBuy.BackColor = Color.RoyalBlue;
                        newOrderWindow.btnSell.BackColor = Color.Red;
                        break;
                    }
                case BehavioralModelState.DirectionConfirmed:
                    {
                        int direction = activeModel.OrderDirection;
                        newOrderWindow.Show();
                        newOrderWindow.labStepName.Text = string.Format("Direction confirmed ({0})", directionName[direction]);
                        newOrderWindow.btnBuy.Enabled = direction == 0;
                        newOrderWindow.btnSell.Enabled = direction == 1;
                        if (direction == 0)
                        {
                            newOrderWindow.btnBuy.BackColor = Color.RoyalBlue;
                            newOrderWindow.btnSell.BackColor = SystemColors.Control;
                        }
                        else
                        {
                            newOrderWindow.btnBuy.BackColor = SystemColors.Control;
                            newOrderWindow.btnSell.BackColor = Color.Red;
                        }
                        break;
                    }
                case BehavioralModelState.ExecuteOrder:
                    {
                        int direction = activeModel.OrderDirection;
                        customDialogWindow.labInformation.Text = string.Format("Order executed\r\nDirection: {0}\r\nContract size: 1\r\nPrice: {1}",
                            directionName[direction], lastPrice[direction]);
                        customDialogWindow.Show();
                        break;
                    }
                case BehavioralModelState.ConfirmationFilled:
                    {
                        int direction = activeModel.OrderDirection;
                        customDialogWindow.labInformation.Text = string.Format("Order filled\r\nDirection: {0}\r\nContract size: 1\r\nPrice: {1}",
                            directionName[direction], lastPrice[direction]);
                        customDialogWindow.Show();
                        break;
                    }
            }

        }

        public void OnNext(MainNeuroXModelEvent modelEvent, object data)
        {
            var modelEventAction = (Action)(() =>
            {
                if (modelEvent == MainNeuroXModelEvent.ActiveModelChanged)
                {
                    mainWindow.behavioralModelSL.Text = "Behavioral model: " + (model.behavioralModelsContainer.ActiveBehavioralModelIndex + 1);
                }
                else if (modelEvent == MainNeuroXModelEvent.AvtiveModelStateChanged)
                {
                    UpdateInterfaceFromModelState(model.getActiveBehavioralModel().CurrentTickState);
                }
                else if (modelEvent == MainNeuroXModelEvent.LogicQueryDirection)
                {
                    int sub_Protocol_ID = (int)data;
                    string[] messages = { "LONG", "SHORT", "M_L_S_1", "M_L_S_2", "M_S_L_1", "M_S_L_2", "Singular LONG", "Singular SHORT" };
                    string message = 66 <= sub_Protocol_ID && sub_Protocol_ID <= 73 ? messages[sub_Protocol_ID - 66] : "Empty message!";
                    MessageBox.Show(message, "NeuroXChange", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            });

            if (mainNeuroXViewThread == Thread.CurrentThread)
            {
                modelEventAction();
            }
            else
            {
                newOrderWindow.BeginInvoke(modelEventAction);
            }
        }

        private DateTime previousBioTickTime = DateTime.Now;

        public void OnNext(BioData data)
        {
            // optimize view on emulation mode with extra-small ticks
            if (model.emulationOnHistory && (DateTime.Now - previousBioTickTime) < TimeSpan.FromMilliseconds(20))
            {
                return;
            }
            previousBioTickTime = DateTime.Now;

            HeartRateInfo hrInfo = model.behavioralModelsContainer.heartRateProcessor.heartRateInfo;

            mainWindow.BeginInvoke(
                (Action)(() =>
                {

            // update biodata information
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
                chartsWindow.heartRateChart.ChartAreas[0].RecalculateAxesScale();
                chartsWindow.heartRateChart.ChartAreas[1].RecalculateAxesScale();
                chartsWindow.heartRateChart.ChartAreas[2].RecalculateAxesScale();
            }

            // update HR oscillations info
            builder = new StringBuilder();
            builder.Append(string.Format("Heart rate 2 min average: {0:0.##}\r\n", hrInfo.heartRate2minAverage));
            builder.Append(string.Format("Heart rate innter state: {0}\r\n", hrInfo.heartRateInnerState));
            builder.Append(string.Format("Oscillations per min, 3 min average: {0:0.##}\r\n", hrInfo.oscillations3minAverage));
            builder.Append(string.Format("Oscillations per min, 5 min average: {0:0.##}", hrInfo.oscillations5minAverage));

            rawInformationWindow.heartRateRTB.Text = builder.ToString();
            if (hrInfo.heartRate2minAverage > 0)
            {
                hrPoints = chartsWindow.heartRateChart.Series["AVG Heart Rate"].Points;
                hrPoints.AddXY(hrInfo.time, hrInfo.heartRate2minAverage);
                if (hrPoints.Count > 3000)
                {
                    hrPoints.RemoveAt(0);
                    chartsWindow.heartRateChart.ChartAreas[1].RecalculateAxesScale();
                }
            }

                }));
        }

        private DateTime previousPriceTickTime = DateTime.Now;

        public void OnNext(FixApiModelEvent modelEvent, object data)
        {
            // optimize view on emulation mode with extra-small ticks
            if (model.emulationOnHistory && (DateTime.Now - previousPriceTickTime) < TimeSpan.FromMilliseconds(20))
            {
                return;
            }
            previousPriceTickTime = DateTime.Now;

            if (modelEvent == FixApiModelEvent.PriceChanged)
            {
                mainWindow.BeginInvoke(
                                (Action)(() =>
                               {
                                   var prices = (string[])data;
                                   newOrderWindow.btnBuy.Text = "BUY\n\r    " + prices[0];
                                   newOrderWindow.btnSell.Text = "          SELL\n\r   " + prices[1];
                                   lastPrice = prices;
                               }));
            }
        }
    }
}
