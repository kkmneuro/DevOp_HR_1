using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NeuroXChange.Controller;
using NeuroXChange.Model;
using NeuroXChange.Model.BioData;
using NeuroXChange.Model.BehavioralModeling.BioDataProcessors;
using NeuroXChange.Model.FixApi;
using System.Linq;
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
        public BehavioralModelTransitionsWindow behavioralModelTransitionsWindow { get; private set; }
        public BMColorCodedWithPriceWindow bMColorCodedWithPriceWindow { get; private set; }

        // other windows
        public CustomDialogWindow customDialogWindow { get; private set; }
        public LogoWindow logoWindow { get; private set; }

        private TickPrice lastPrice = new TickPrice();
        private string[] directionName = { "Buy", "Sell", "No direction" };

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

            behavioralModelTransitionsWindow = new BehavioralModelTransitionsWindow(model);
            behavioralModelTransitionsWindow.Owner = mainWindow;

            bMColorCodedWithPriceWindow = new BMColorCodedWithPriceWindow();
            bMColorCodedWithPriceWindow.Owner = mainWindow;

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
                        else if (direction == 1)
                        {
                            newOrderWindow.btnBuy.BackColor = SystemColors.Control;
                            newOrderWindow.btnSell.BackColor = Color.Red;
                        }
                        else
                        {
                            newOrderWindow.btnBuy.BackColor = SystemColors.Control;
                            newOrderWindow.btnSell.BackColor = SystemColors.Control;
                        }
                        break;
                    }
                case BehavioralModelState.ExecuteOrder:
                    {
                        int direction = activeModel.OrderDirection;
                        customDialogWindow.labInformation.Text = string.Format("Order executed\r\nDirection: {0}\r\nContract size: 1\r\nPrice: {1}",
                            directionName[direction], direction == 0 ? lastPrice.buy : lastPrice.sell);
                        customDialogWindow.Show();
                        break;
                    }
                case BehavioralModelState.ConfirmationFilled:
                    {
                        int direction = activeModel.OrderDirection;
                        customDialogWindow.labInformation.Text = string.Format("Order filled\r\nDirection: {0}\r\nContract size: 1\r\nPrice: {1}",
                            directionName[direction], direction == 0 ? lastPrice.buy : lastPrice.sell);
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
                    string message = 66 <= sub_Protocol_ID && sub_Protocol_ID <= 73 ? messages[sub_Protocol_ID - 66] : "No direction confirmed!";
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
                    if (temperaturePoints.Count > 0)
                    {
                        double point = temperaturePoints[temperaturePoints.Count - 1].XValue;
                        DateTime pointTime = DateTime.FromOADate(point);
                        DateTime dataTime = new DateTime(pointTime.Year, pointTime.Month, pointTime.Day, data.time.Hour, data.time.Minute, data.time.Second, data.time.Millisecond);
                        if (dataTime - pointTime > TimeSpan.FromHours(1) || dataTime < pointTime)
                        {
                            temperaturePoints.Clear();
                            hrPoints.Clear();
                            skinCondPoints.Clear();
                            chartsWindow.heartRateChart.Series["AVG Heart Rate"].Points.Clear();

                            foreach (var series in bMColorCodedWithPriceWindow.chart.Series)
                            {
                                series.Points.Clear();
                            }
                        }
                        else
                        {
                            point = temperaturePoints[0].XValue;
                            pointTime = DateTime.FromOADate(point);
                            dataTime = new DateTime(pointTime.Year, pointTime.Month, pointTime.Day, data.time.Hour, data.time.Minute, data.time.Second, data.time.Millisecond);
                            if (dataTime - pointTime > TimeSpan.FromMinutes(5))
                            {
                                temperaturePoints.RemoveAt(0);
                                hrPoints.RemoveAt(0);
                                skinCondPoints.RemoveAt(0);
                                chartsWindow.heartRateChart.ChartAreas[0].RecalculateAxesScale();
                                chartsWindow.heartRateChart.ChartAreas[1].RecalculateAxesScale();
                                chartsWindow.heartRateChart.ChartAreas[2].RecalculateAxesScale();
                            }
                        }
                    }

                    temperaturePoints.AddXY(data.time, data.temperature);
                    hrPoints.AddXY(data.time, data.hartRate);
                    skinCondPoints.AddXY(data.time, data.skinConductance);

                    // update HR oscillations info
                    builder = new StringBuilder();
                    builder.Append(string.Format("Heart rate 2 min average: {0:0.##}\r\n", hrInfo.heartRate2minAverage));
                    builder.Append(string.Format("Heart rate innter state: {0}\r\n", hrInfo.heartRateInnerState));
                    builder.Append(string.Format("Oscillations per min, 1 min average: {0:0.##}\r\n", hrInfo.oscillations1minAverage));
                    builder.Append(string.Format("Oscillations per min, 3 min average: {0:0.##}\r\n", hrInfo.oscillations3minAverage));
                    builder.Append(string.Format("Oscillations per min, 5 min average: {0:0.##}", hrInfo.oscillations5minAverage));

                    rawInformationWindow.heartRateRTB.Text = builder.ToString();
                    if (hrInfo.heartRate2minAverage > 0)
                    {
                        var hrOscPoints = chartsWindow.heartRateChart.Series["AVG Heart Rate"].Points;
                        hrOscPoints.AddXY(hrInfo.time, hrInfo.heartRate2minAverage);
                        if (hrOscPoints[0].XValue < hrPoints[0].XValue)
                        {
                            hrOscPoints.RemoveAt(0);
                            chartsWindow.heartRateChart.ChartAreas[1].RecalculateAxesScale();
                        }
                    }

                    // update indicators
                    int peakIndValue = 1;
                    double hr1min = hrInfo.oscillations1minAverage;
                    double hr3min = hrInfo.oscillations3minAverage;
                    double hr5min = hrInfo.oscillations5minAverage;
                    if (5 < hr5min && hr5min < 6.5 && 5 < hr3min && hr3min < 6.5 && 5 < hr1min && hr1min < 6.5)
                    {
                        peakIndValue = 10;
                    }
                    else if (5 < hr3min && hr3min < 6.5 && 5 < hr1min && hr1min < 6.5)
                    {
                        peakIndValue = 9;
                    }
                    else if (5 < hr1min && hr1min < 6.5)
                    {
                        peakIndValue = 8;
                    }
                    else if (5 < hr5min && hr5min < 6.5 && 5 < hr3min && hr3min < 6.5 && 4 < hr1min && hr1min < 12)
                    {
                        peakIndValue = 7;
                    }
                    else if (5 < hr5min && hr5min < 6.5 && 4 < hr3min && hr3min < 12)
                    {
                        peakIndValue = 6;
                    }
                    else if (4 < hr5min && hr5min < 12)
                    {
                        peakIndValue = 5;
                    }
                    else if (4 < hr3min && hr3min < 12 && 12 < hr1min)
                    {
                        peakIndValue = 4;
                    }
                    else if (4 < hr5min && hr5min < 12 && 12 < hr3min)
                    {
                        peakIndValue = 3;
                    }
                    else if (12 < hr5min)
                    {
                        peakIndValue = 2;
                    }
                    else
                    {
                        peakIndValue = 1;
                    }
                    indicatorsWindow.peakPerformanceGauge.Value = ((float)peakIndValue) - 0.5f;

                    // update BM color-coded charts
                    bool addBmPoints = bMColorCodedWithPriceWindow.chart.Series[0].Points.Count < 10;
                    if (!addBmPoints)
                    {
                        foreach (var bModel in model.behavioralModelsContainer.behavioralModels)
                        {
                            addBmPoints |= bModel.PreviousTickState != bModel.CurrentTickState;
                        }
                    }
                    if (!addBmPoints)
                    {
                        var point = bMColorCodedWithPriceWindow.chart.Series[0].Points.Last().XValue;
                        var pointTime = DateTime.FromOADate(point);
                        var dataTime = new DateTime(pointTime.Year, pointTime.Month, pointTime.Day, data.time.Hour, data.time.Minute, data.time.Second, data.time.Millisecond);
                        addBmPoints = dataTime - pointTime > TimeSpan.FromSeconds(2);

                        var pointFirst = bMColorCodedWithPriceWindow.chart.Series[0].Points[0].XValue;
                        var pointTimeFirst = DateTime.FromOADate(pointFirst);
                        while (dataTime - pointTimeFirst > TimeSpan.FromMinutes(3))
                        {
                            foreach (var series in bMColorCodedWithPriceWindow.chart.Series)
                            {
                                if (series.Points.Count > 0)
                                {
                                    series.Points.RemoveAt(0);
                                }
                            }
                            pointFirst = bMColorCodedWithPriceWindow.chart.Series[0].Points[0].XValue;
                            pointTimeFirst = DateTime.FromOADate(pointFirst);
                            bMColorCodedWithPriceWindow.chart.ChartAreas[0].RecalculateAxesScale();
                        }

                        // remove price points
                        if (bMColorCodedWithPriceWindow.chart.Series["Price"].Points.Count > 0)
                        {
                            pointFirst = bMColorCodedWithPriceWindow.chart.Series["Price"].Points[0].XValue;
                            pointTimeFirst = DateTime.FromOADate(pointFirst);
                            while (dataTime - pointTimeFirst > TimeSpan.FromMinutes(3) ||
                                 (bMColorCodedWithPriceWindow.chart.Series[0].Points.Count > 0 && bMColorCodedWithPriceWindow.chart.Series["Price"].Points[0].XValue < bMColorCodedWithPriceWindow.chart.Series[0].Points[0].XValue))
                            {
                                bMColorCodedWithPriceWindow.chart.Series["Price"].Points.RemoveAt(0);
                                if (bMColorCodedWithPriceWindow.chart.Series["Price"].Points.Count == 0)
                                {
                                    break;
                                }
                                pointFirst = bMColorCodedWithPriceWindow.chart.Series["Price"].Points[0].XValue;
                                pointTimeFirst = DateTime.FromOADate(pointFirst);
                                bMColorCodedWithPriceWindow.chart.ChartAreas[0].RecalculateAxesScale();
                            }
                        }
                    }
                        for (int i = 0; i < model.behavioralModelsContainer.BehavioralModelsCount; i++)
                        {
                            var bModel = model.behavioralModelsContainer.behavioralModels[i];
                            double value = 0;
                            switch (bModel.CurrentTickState)
                            {
                                case BehavioralModelState.InitialState:
                                    value = 1;
                                    break;
                                case BehavioralModelState.ReadyToTrade:
                                    value = 1.5;
                                    break;
                                case BehavioralModelState.Preactivation:
                                    value = 2;
                                    break;
                                case BehavioralModelState.DirectionConfirmed:
                                    value = 2.5;
                                    break;
                                case BehavioralModelState.ExecuteOrder:
                                    value = 4;
                                    break;
                            }
                        if (addBmPoints)
                        {
                            bMColorCodedWithPriceWindow.chart.Series[i].Points.AddXY(data.time, value);
                        }
                        else
                        {
                            //bMColorCodedWithPriceWindow.chart.Series[i].Points.Last().SetValueXY(data.time, value);
                        }
                        //int pointCount = bMColorCodedWithPriceWindow.chart.Series.Select(s => s.Points.Count).Sum();
                        //bMColorCodedWithPriceWindow.Text = pointCount.ToString();
                    }
                }));
        }

        private DateTime previousPriceTickTime = DateTime.FromOADate(0);

        public void OnNext(FixApiModelEvent modelEvent, object data)
        {
            // optimize view on emulation mode with extra-small ticks
            if (model.emulationOnHistory && (DateTime.Now - previousPriceTickTime) < TimeSpan.FromMilliseconds(100))
            {
                return;
            }
            previousPriceTickTime = DateTime.Now;

            if (modelEvent == FixApiModelEvent.PriceChanged)
            {
                mainWindow.BeginInvoke(
                                (Action)(() =>
                               {
                                   var price = (TickPrice)data;
                                   newOrderWindow.btnBuy.Text = "BUY\n\r    " + price.buy;
                                   newOrderWindow.btnSell.Text = "          SELL\n\r   " + price.sell;
                                   lastPrice = price;

                                   if (price.buy.Length > 0)
                                   {
                                       var value = Double.Parse(price.buy);
                                       if (value < bMColorCodedWithPriceWindow.chart.ChartAreas[0].AxisY2.Minimum)
                                       {
                                           bMColorCodedWithPriceWindow.chart.ChartAreas[0].AxisY2.Minimum = value;
                                       }
                                       bMColorCodedWithPriceWindow.chart.Series["Price"].Points.AddXY(price.time, value);

                                       var point = bMColorCodedWithPriceWindow.chart.Series["Price"].Points.Last().XValue;
                                       var pointTime = DateTime.FromOADate(point);
                                       var dataTime = new DateTime(pointTime.Year, pointTime.Month, pointTime.Day, price.time.Hour, price.time.Minute, price.time.Second, price.time.Millisecond);
                                       var pointFirst = bMColorCodedWithPriceWindow.chart.Series["Price"].Points[0].XValue;
                                       var pointTimeFirst = DateTime.FromOADate(pointFirst);
                                       while (dataTime - pointTimeFirst > TimeSpan.FromMinutes(3) ||
                                            (bMColorCodedWithPriceWindow.chart.Series[0].Points.Count > 0 && bMColorCodedWithPriceWindow.chart.Series["Price"].Points[0].XValue < bMColorCodedWithPriceWindow.chart.Series[0].Points[0].XValue) )
                                       {
                                           bMColorCodedWithPriceWindow.chart.Series["Price"].Points.RemoveAt(0);
                                           if (bMColorCodedWithPriceWindow.chart.Series["Price"].Points.Count == 0)
                                           {
                                            break;
                                           }
                                           pointFirst = bMColorCodedWithPriceWindow.chart.Series["Price"].Points[0].XValue;
                                           pointTimeFirst = DateTime.FromOADate(pointFirst);
                                           bMColorCodedWithPriceWindow.chart.ChartAreas[0].RecalculateAxesScale();
                                       }
                                   }
                               }));
            }
        }
    }
}
