using NeuroXChange.Model.BioData;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PostTradingAnalysis
{
    public class PostTradingAnalysisApplication
    {
        public List<BioData> bioData;

        public MainWindow mainWindow;

        public Dictionary<string, ChartWindow> chartWindows;

        public PostTradingAnalysisApplication()
        {
            mainWindow = new MainWindow(this);
            bioData = new List<BioData>();

            // registering windows
            chartWindows = new Dictionary<string, ChartWindow>();
            chartWindows["Temperature"] = new ChartWindow(this, Color.Red);
            chartWindows["Heart rate"] = new ChartWindow(this, Color.Green);
            chartWindows["Skin conductance"] = new ChartWindow(this, Color.Blue);
            chartWindows["Price"] = new ChartWindow(this, Color.Brown);
            chartWindows["Training step"] = new ChartWindow(this, Color.Orange);

            chartWindows["Temperature stddev"] = new ChartWindow(this, Color.Red);
            chartWindows["Heart rate stddev"] = new ChartWindow(this, Color.Green);
            chartWindows["Skin conductance stddev"] = new ChartWindow(this, Color.Blue);
            chartWindows["Price stddev"] = new ChartWindow(this, Color.Brown);

            chartWindows["Temperature stddev away"] = new ChartWindow(this, Color.Red);
            chartWindows["Heart rate stddev away"] = new ChartWindow(this, Color.Green);
            chartWindows["Skin conductance stddev away"] = new ChartWindow(this, Color.Blue);
            chartWindows["Price stddev away"] = new ChartWindow(this, Color.Brown);

            chartWindows["SC stddev * Price stddev"] = new ChartWindow(this, Color.DarkMagenta);
            chartWindows["SC stdev away - Price stddev away"] = new ChartWindow(this, Color.DarkMagenta);

            // setup windows
            foreach (var kv in chartWindows)
            {
                var chartName = kv.Key;
                var window = kv.Value;
                window.Text = chartName;
                window.Owner = mainWindow;

                // update toolstrip
                ToolStripMenuItem menuItem = new ToolStripMenuItem(chartName);
                mainWindow.chartsToolStripMenuItem.DropDownItems.Add(menuItem);
                menuItem.Click += new System.EventHandler(
                    delegate (Object sender, EventArgs e) {
                        window.Show();
                        window.BringToFront();
                    });

                if (chartName == "Training step" || chartName == "Price stddev" || chartName == "Price stddev away")
                {
                    mainWindow.chartsToolStripMenuItem.DropDownItems.Add("-");
                }
            }

            Application.Run(mainWindow);
        }

        public void LoadData(string fileName, DateTime dateFrom, DateTime dateTo)
        {
            if (!File.Exists(fileName))
            {
                throw new Exception("There are no such file: \"" + fileName + "\"");
            }

            if (dateFrom >= dateTo)
            {
                throw new Exception("First date should be less than second date!");
            }

            bioData.Clear();

            var connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"{0}\"", fileName);
            var connection = new OleDbConnection(connectionString);
            connection.Open();
            var cmd = new OleDbCommand();
            cmd.Connection = connection;

            // biodata Loading
            var biodataSQL = string.Format(
                @"SELECT BioData.*, SellPrice, BuyPrice From BioData
            LEFT OUTER JOIN PriceAtBioDataTick ON BioData.ID = PriceAtBioDataTick.ID
            WHERE BioData.Time BETWEEN #{0:yyyy-MM-dd HH:mm:ss}# AND #{1:yyyy-MM-dd HH:mm:ss}#
            ORDER BY BioData.ID",
                dateFrom, dateTo);
            cmd.CommandText = biodataSQL;
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var data = BioData.FromOleDbDataReader(reader);
                bioData.Add(data);
            }

            foreach (var kv in chartWindows)
            {
                var chartName = kv.Key;
                var window = kv.Value;

                var series = new LineSeries();
                series.Title = chartName;
                series.StrokeThickness = 1;
                series.Color = OxyColor.FromUInt32((uint)window.color.ToArgb());

                // chose what data to show
                if (chartName == "Temperature")
                    for (int i = 0; i < bioData.Count; i++)
                        series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(bioData[i].time), bioData[i].temperature));
                if (chartName == "Heart rate")
                    for (int i = 0; i < bioData.Count; i++)
                        series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(bioData[i].time), bioData[i].heartRate));
                if (chartName == "Skin conductance")
                    for (int i = 0; i < bioData.Count; i++)
                        series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(bioData[i].time), bioData[i].skinConductance));
                if (chartName == "Training step")
                    for (int i = 0; i < bioData.Count; i++)
                        if (bioData[i].trainingStep != 0)
                            series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(bioData[i].time), bioData[i].trainingStep));
                if (chartName == "Price")
                    for (int i = 0; i < bioData.Count; i++)
                        series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(bioData[i].time),
                            bioData[i].buyPrice.HasValue ? bioData[i].buyPrice.Value : double.NaN));

                var model = new PlotModel();
                model.PlotMargins = new OxyThickness(28, -8, -7, 6);
                model.DefaultFontSize = 10;
                model.Series.Add(series);

                var xAxis = new DateTimeAxis { Position = AxisPosition.Bottom, StringFormat = "H:mm:ss" };
                xAxis.MajorGridlineStyle = LineStyle.Solid;
                if (bioData.Count > 0)
                {
                    xAxis.Minimum = DateTimeAxis.ToDouble(bioData[0].time);
                    xAxis.Maximum = DateTimeAxis.ToDouble(bioData[bioData.Count - 1].time);
                }
                xAxis.AxisChanged += HandleXAxisChanged;
                xAxis.AxisTickToLabelDistance = 0;
                xAxis.AxisTitleDistance = 0;
                xAxis.MajorTickSize = 0;
                model.Axes.Add(xAxis);
                var yAxis = new LinearAxis { Position = AxisPosition.Left };
                yAxis.MajorGridlineStyle = LineStyle.Solid;
                yAxis.MajorTickSize = -1;
                model.Axes.Add(yAxis);

                var controller = new PlotController();
                controller.BindMouseDown(OxyMouseButton.Left, PlotCommands.PanAt);
                controller.BindMouseDown(OxyMouseButton.Right, PlotCommands.HoverTrack);
                window.plotView.Controller = controller;

                window.plotView.Model = model;
            }

            // update std charts
            SetStdDevPeriod(stdDevPeriod);
        }

        private int stdDevPeriod = 120;
        public void SetStdDevPeriod(int stdDevPeriod)
        {
            this.stdDevPeriod = stdDevPeriod;

            if (chartWindows == null || chartWindows.Count == 0)
            {
                return;
            }

            foreach (var kv in chartWindows)
            {
                var chartName = kv.Key;
                var window = kv.Value;
                var model = window.plotView.Model;
                if (!chartName.EndsWith(" stddev") && !chartName.EndsWith(" stddev away"))
                {
                    continue;
                }

                var series = new LineSeries();
                series.Title = chartName;
                series.StrokeThickness = 1;
                series.Color = OxyColor.FromUInt32((uint)window.color.ToArgb());

                for (int i = 0; i < bioData.Count; i++)
                {
                    if (i < stdDevPeriod - 1)
                    {
                        series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(bioData[i].time), double.NaN));
                        continue;
                    }

                    double mean = 0;
                    int realCount = 0;
                    for (int j = i - stdDevPeriod + 1; j <= i; j++)
                    {
                        realCount++;
                        if (chartName == "Temperature stddev" || chartName == "Temperature stddev away")
                            mean += bioData[j].temperature;
                        if (chartName == "Heart rate stddev" || chartName == "Heart rate stddev away")
                            mean += bioData[j].heartRate;
                        if (chartName == "Skin conductance stddev" || chartName == "Skin conductance stddev away")
                            mean += bioData[j].skinConductance;
                        if (chartName == "Price stddev" || chartName == "Price stddev away")
                            if (bioData[j].buyPrice.HasValue)
                            {
                                mean += bioData[j].buyPrice.Value;
                            }
                            else
                            {
                                realCount--;
                            }
                    }

                    if (realCount <= stdDevPeriod / 2)
                    {
                        series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(bioData[i].time), double.NaN));
                        continue;
                    }

                    mean /= realCount;

                    double sum = 0;
                    double val = mean;
                    for (int j = i - stdDevPeriod + 1; j <= i; j++)
                    {
                        if (chartName == "Temperature stddev" || chartName == "Temperature stddev away")
                            val = bioData[j].temperature;
                        if (chartName == "Heart rate stddev" || chartName == "Heart rate stddev away")
                            val = bioData[j].heartRate;
                        if (chartName == "Skin conductance stddev" || chartName == "Skin conductance stddev away")
                            val = bioData[j].skinConductance;
                        if (chartName == "Price stddev" || chartName == "Price stddev away")
                            val = bioData[j].buyPrice.HasValue ? bioData[j].buyPrice.Value : double.NaN;
                        sum += (val - mean) * (val - mean);
                    }

                    sum /= realCount;
                    double stddev = Math.Sqrt(sum);

                    double stddevAway = stddev > 0 ? (val - mean) / stddev : 0;

                    if (chartName.EndsWith(" stddev away"))
                        series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(bioData[i].time), stddevAway));
                    else
                        series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(bioData[i].time), stddev));
                }

                model.Series.Clear();
                model.Series.Add(series);

                window.plotView.Model.InvalidatePlot(true);
            }


            // update complex charts
            {
                var chartName = "SC stddev * Price stddev";
                var window = chartWindows[chartName];
                var model = window.plotView.Model;

                var series = new LineSeries();
                series.Title = chartName;
                series.StrokeThickness = 1;
                series.Color = OxyColor.FromUInt32((uint)window.color.ToArgb());

                var seriesSC = (LineSeries)chartWindows["Skin conductance stddev"].plotView.Model.Series[0];
                var seriesPrice = (LineSeries)chartWindows["Price stddev"].plotView.Model.Series[0];

                for (int i = 0; i < seriesSC.Points.Count; i++)
                {
                    var pointSc = seriesSC.Points[i];
                    var pointPrice = seriesPrice.Points[i];
                    series.Points.Add(new DataPoint(pointSc.X, pointSc.Y * pointPrice.Y));
                }

                model.Series.Clear();
                model.Series.Add(series);

                window.plotView.Model.InvalidatePlot(true);
            }

            {
                var chartName = "SC stdev away - Price stddev away";
                var window = chartWindows[chartName];
                var model = window.plotView.Model;

                var seriesUp = new LineSeries();
                seriesUp.Title = "Uptrend";
                seriesUp.StrokeThickness = 1;
                seriesUp.Color = OxyColor.FromUInt32((uint)Color.Green.ToArgb());

                var seriesDown = new LineSeries();
                seriesDown.Title = "Downtrend";
                seriesDown.StrokeThickness = 1;
                seriesDown.Color = OxyColor.FromUInt32((uint)Color.Red.ToArgb());

                var seriesSCAway = (LineSeries)chartWindows["Skin conductance stddev away"].plotView.Model.Series[0];
                var seriesPriceAway = (LineSeries)chartWindows["Price stddev away"].plotView.Model.Series[0];

                double prevValue = 0;
                double prevPriceDiff = 0;
                double value = 0;
                double priceDiff = 0;
                for (int i = 0; i < seriesSCAway.Points.Count; i++)
                {
                    var pointSc = seriesSCAway.Points[i];
                    if (i < stdDevPeriod - 1)
                    {
                        seriesUp.Points.Add(new DataPoint(pointSc.X, double.NaN));
                        seriesDown.Points.Add(new DataPoint(pointSc.X, double.NaN));
                        continue;
                    }
                    var pointPriceStart = seriesPriceAway.Points[i - stdDevPeriod + 1];
                    var pointPriceEnd = seriesPriceAway.Points[i];
                    double scVal = pointSc.Y;
                    double priceVal = pointPriceEnd.Y;
                    priceDiff = pointPriceEnd.Y - pointPriceStart.Y;
                    value = (scVal - priceVal) * priceDiff;
                    if (priceDiff >= 0)
                    {
                        if (i > 0 && prevPriceDiff < 0)
                            seriesUp.Points.Add(new DataPoint(seriesSCAway.Points[i-1].X, prevValue));
                        seriesUp.Points.Add(new DataPoint(pointSc.X, value));
                        seriesDown.Points.Add(new DataPoint(pointSc.X, double.NaN));
                    }
                    else
                    {
                        if (i > 0 && prevPriceDiff >= 0)
                            seriesDown.Points.Add(new DataPoint(seriesSCAway.Points[i - 1].X, prevValue));
                        seriesDown.Points.Add(new DataPoint(pointSc.X, value));
                        seriesUp.Points.Add(new DataPoint(pointSc.X, double.NaN));
                    }
                    prevValue = value;
                    prevPriceDiff = priceDiff;
                }

                model.Series.Clear();
                model.Series.Add(seriesUp);
                model.Series.Add(seriesDown);

                window.plotView.Model.InvalidatePlot(true);
            }
        }

        public int GetStdDevPeriod()
        {
            return stdDevPeriod;
        }

        private bool manualChanging = false;
        void HandleXAxisChanged(object sender, AxisChangedEventArgs e)
        {
            if (manualChanging)
            {
                return;
            }
            manualChanging = true;

            var senderAxis = (LinearAxis)sender;
            foreach (var kv in chartWindows)
            {
                var chartName = kv.Key;
                var window = kv.Value;
                var axis = window.plotView.Model.Axes[0];
                if (axis != sender)
                {
                    axis.Zoom(senderAxis.ActualMinimum, senderAxis.ActualMaximum);
                }
                
                window.plotView.Refresh();
            }

            manualChanging = false;
        }
    }

}
