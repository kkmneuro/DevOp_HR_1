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
using NeuroXChange.Common;
using NeuroXChange.View.Training;
using NeuroXChange.View.DialogWindows;

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

        // training windows
        public CompDayWindow compDayWindow { get; private set; }

        // working application windows
        public RawInformationWindow rawInformationWindow { get; private set; }
        public NewOrderWindow newOrderWindow { get; private set; }
        public ChartsWindow chartsWindow { get; private set; }
        public BreathPacerWindow breathPacerWindow { get; private set; }
        public IndicatorsWindow indicatorsWindow { get; private set; }
        public BehavioralModelsWindow behavioralModelWindow { get; private set; }
        public BehavioralModelTransitionsWindow behavioralModelTransitionsWindow { get; private set; }
        public BMColorCodedWithPriceWindow bMColorCodedWithPriceWindow { get; private set; }
        public EmulationModeControlWindow emulationModeControlWindow { get; private set; }
        public ProfitabilityWindow profitabilityWindow { get; private set; }

        // other windows
        public CustomDialogWindow customDialogWindow { get; private set; }
        public LogoWindow logoWindow { get; private set; }
        public ManualOrderConfirmationWindow manualOrderConfirmationWindow { get; private set; }

        private TickPrice lastPrice = new TickPrice();

        public MainNeuroXView(MainNeuroXModel model, MainNeuroXController controller)
        {
            this.model = model;
            this.controller = controller;
            mainNeuroXViewThread = Thread.CurrentThread;

            mainWindow = new MainWindow(this, model.iniFileReader);
            mainWindow.modeNameSL.Text = "Mode: " + (model.emulationOnHistoryMode ? "emulation on history" : "real-time");

            // training windows creation
            compDayWindow = new CompDayWindow(controller, this);
            compDayWindow.Owner = mainWindow;

            // application windows creation
            rawInformationWindow = new RawInformationWindow();
            rawInformationWindow.Owner = mainWindow;

            newOrderWindow = new NewOrderWindow(model, controller, this);
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

            bMColorCodedWithPriceWindow = new BMColorCodedWithPriceWindow(model);
            bMColorCodedWithPriceWindow.Owner = mainWindow;

            emulationModeControlWindow = new EmulationModeControlWindow(model, controller);
            emulationModeControlWindow.Owner = mainWindow;
            emulationModeControlWindow.tickSizeUpDown.Value = Int32.Parse(model.iniFileReader.Read("HistoryTickInterval", "EmulationOnHistory", "100"));
            emulationModeControlWindow.Enabled = model.emulationOnHistoryMode;

            profitabilityWindow = new ProfitabilityWindow(model);
            profitabilityWindow.Owner = mainWindow;

            // dialog windows creation

            logoWindow = new LogoWindow();
            logoWindow.ShowDialog(mainWindow);

            customDialogWindow = new CustomDialogWindow();
            customDialogWindow.Show();
            customDialogWindow.Hide();

            manualOrderConfirmationWindow = new ManualOrderConfirmationWindow(model, controller);

            // events registering

            model.RegisterObserver(this);
            model.bioDataProvider.RegisterObserver(this);
            model.fixApiModel.RegisterObserver(this);

            // other stuff

            allWindowsOnTop = Boolean.Parse(model.iniFileReader.Read("AllWindowsOnTop", "Interface", "true"));
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
                case BehavioralModelState.ReadyToTrade:
                case BehavioralModelState.Preactivation:
                case BehavioralModelState.DirectionConfirmed:
                    {
                        break;
                    }
                case BehavioralModelState.ExecuteOrder:
                    {
                        int direction = activeModel.Direction;
                        customDialogWindow.labInformation.Text = string.Format("Order executed\r\nDirection: {0}\r\nContract size: 1\r\nPrice: {1}",
                            BehavioralModelStateHelper.directionName[direction], direction == 0 ? lastPrice.buyString : lastPrice.sellString);
                        customDialogWindow.ShowWithSeconds(2);
                        break;
                    }
                case BehavioralModelState.ConfirmationFilled:
                    {
                        int direction = activeModel.Direction;
                        customDialogWindow.labInformation.Text = string.Format("Order filled\r\nDirection: {0}\r\nContract size: 1\r\nPrice: {1}",
                            BehavioralModelStateHelper.directionName[direction], direction == 0 ? lastPrice.buyString : lastPrice.sellString);
                        customDialogWindow.ShowWithSeconds(2);
                        break;
                    }
            }

            newOrderWindow.UpdateInterfaceFromModelState(state);
        }

        public void OnNext(MainNeuroXModelEvent modelEvent, object data)
        {
            var modelEventAction = (Action)(() =>
            {
                if (modelEvent == MainNeuroXModelEvent.ActiveModelChanged)
                {
                    mainWindow.behavioralModelSL.Text = "Behavioral model: " + (model.behavioralModelsContainer.ActiveBehavioralModelIndex + 1);
                }
                else if (modelEvent == MainNeuroXModelEvent.ActiveModelStateChanged)
                {
                    UpdateInterfaceFromModelState(model.getActiveBehavioralModel().CurrentTickState);
                }
                else if (modelEvent == MainNeuroXModelEvent.LogicQueryDirection)
                {
                    int trainingStep = (int)data;
                    string[] messages = { "LONG", "SHORT", "M_L_S_1", "M_L_S_2", "M_S_L_1", "M_S_L_2", "Singular LONG", "Singular SHORT" };
                    string message = 66 <= trainingStep && trainingStep <= 73 ? messages[trainingStep - 66] : "No direction confirmed!";
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

        private DateTime previousRealTickTime = DateTime.Now;
        private DateTime previousBioTickTime;

        public void OnNext(BioDataEvent bioDataEvent, object data)
        {
            if (bioDataEvent != BioDataEvent.NewBioDataTick)
            {
                if (!mainWindow.IsHandleCreated)
                    return;
            mainWindow.BeginInvoke(
               (Action)(() =>
               {
                   if (bioDataEvent == BioDataEvent.EmulationModeBioDataFinished)
                   {
                       emulationModeControlWindow.pauseButton.Enabled = false;
                       breathPacerWindow.breathPacerControl.Stop();
                   }
                   else if (bioDataEvent == BioDataEvent.EmulationModePaused)
                   {
                       emulationModeControlWindow.startButton.Enabled = true;
                       emulationModeControlWindow.pauseButton.Enabled = false;
                       emulationModeControlWindow.nextButton.Enabled = true;
                       breathPacerWindow.breathPacerControl.Stop();
                   }
                   else if (bioDataEvent == BioDataEvent.EmulationModeContinued)
                   {
                       emulationModeControlWindow.startButton.Enabled = false;
                       emulationModeControlWindow.pauseButton.Enabled = true;
                       emulationModeControlWindow.nextButton.Enabled = false;
                       breathPacerWindow.breathPacerControl.Continue();
                   }
                   else if (bioDataEvent == BioDataEvent.EmulationModeProgress)
                   {
                       var progress = (int[])data;
                       emulationModeControlWindow.progressBar.Maximum = progress[1];
                       emulationModeControlWindow.progressBar.Value = progress[0];
                   }
               }));
                return;
            }
            var bioData = (BioData)data;

            // optimize view on emulation mode with extra-small ticks
            if (model.emulationOnHistoryMode && (DateTime.Now - previousRealTickTime) < TimeSpan.FromMilliseconds(20))
            {
                return;
            }
            previousRealTickTime = DateTime.Now;

            HeartRateInfo hrInfo = model.behavioralModelsContainer.heartRateProcessor.heartRateInfo;

            mainWindow.BeginInvoke(
                (Action)(() =>
                {
                try
                {
                    // update biodata information
                    StringBuilder builder = new StringBuilder();
                    builder.Append("ID: " + bioData.id + "\r\n");
                    builder.Append("Time: " + bioData.time + "\r\n");
                    builder.Append("Temperature: " + bioData.temperature.ToString("0.##") + "\r\n");
                    builder.Append("HeartRate: " + bioData.heartRate.ToString("0.##") + "\r\n");
                    builder.Append("SkinConductance: " + bioData.skinConductance.ToString("0.##") + "\r\n");
                    builder.Append("AccX: " + bioData.accX.ToString("0.##") + "\r\n");
                    builder.Append("AccY: " + bioData.accY.ToString("0.##") + "\r\n");
                    builder.Append("AccZ: " + bioData.accZ.ToString("0.##") + "\r\n");
                    builder.Append("TrainingType: " + bioData.trainingType + "\r\n");
                    builder.Append("TrainingStep: " + bioData.trainingStep + "\r\n");
                    builder.Append("ApplicationStates: " + bioData.applicationStates + "\r\n");

                    rawInformationWindow.bioDataRTB.Text = builder.ToString();

                    if (previousBioTickTime == bioData.time)
                    {
                        return;
                    }
                    previousBioTickTime = bioData.time;

                    var temperaturePoints = chartsWindow.heartRateChart.Series["Temperature"].Points;
                    var hrPoints = chartsWindow.heartRateChart.Series["Heart Rate"].Points;
                    var skinCondPoints = chartsWindow.heartRateChart.Series["Skin Conductance"].Points;
                    if (temperaturePoints.Count > 0)
                    {
                        double point = temperaturePoints[temperaturePoints.Count - 1].XValue;
                        DateTime pointTime = DateTime.FromOADate(point);
                        DateTime dataTime = new DateTime(pointTime.Year, pointTime.Month, pointTime.Day, bioData.time.Hour, bioData.time.Minute, bioData.time.Second, bioData.time.Millisecond);
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
                            dataTime = new DateTime(pointTime.Year, pointTime.Month, pointTime.Day, bioData.time.Hour, bioData.time.Minute, bioData.time.Second, bioData.time.Millisecond);
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

                    temperaturePoints.AddXY(bioData.time, bioData.temperature);
                    hrPoints.AddXY(bioData.time, bioData.heartRate);
                    skinCondPoints.AddXY(bioData.time, bioData.skinConductance);

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
                        var dataTime = new DateTime(pointTime.Year, pointTime.Month, pointTime.Day, bioData.time.Hour, bioData.time.Minute, bioData.time.Second, bioData.time.Millisecond);
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
                            bMColorCodedWithPriceWindow.chart.Series[i].Points.AddXY(bioData.time, value);
                        }
                        else
                        {
                            //bMColorCodedWithPriceWindow.chart.Series[i].Points.Last().SetValueXY(bioData.time, value);
                        }
                        //int pointCount = bMColorCodedWithPriceWindow.chart.Series.Select(s => s.Points.Count).Sum();
                        //bMColorCodedWithPriceWindow.Text = pointCount.ToString();
                    }
                }
                catch { }
                }));
        }

        private DateTime previousPlottedTime = DateTime.FromOADate(0);
        private DateTime previousBioDataTick;

        public void OnNext(FixApiModelEvent modelEvent, object data)
        {
            if (modelEvent != FixApiModelEvent.PriceChanged)
            {
                return;
            }

            // optimize view on emulation mode with extra-small ticks
            if (model.emulationOnHistoryMode && (DateTime.Now - previousPlottedTime) < TimeSpan.FromMilliseconds(100))
            {
                return;
            }
            previousPlottedTime = DateTime.Now;

            var price = (TickPrice)data;
            if (price.time == previousBioDataTick)
            {
                return;
            }
            previousBioDataTick = price.time;


            mainWindow.BeginInvoke(
                                (Action)(() =>
                               {
                                   manualOrderConfirmationWindow.OnNext(price);

                                   newOrderWindow.btnSell.Text = "SELL\n\r    " + price.sellString;
                                   newOrderWindow.btnBuy.Text = "            BUY\n\r    " + price.buyString;
                                   lastPrice = price;

                                   if (price.buyString.Length > 0)
                                   {
                                       var value = StringHelpers.ParseDoubleCultureIndependent(price.buyString);
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
