using NeuroXChange.Model.BioData;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using global::PostTradingAnalysis.Properties;

namespace PostTradingAnalysis
{
    public class PostTradingAnalysisApplication
    {
        public List<User> users = new List<User>();
        public Dictionary<int, long> cbUserInd2UserID = new Dictionary<int, long>();

        public Dictionary<long, List<Tuple<DateTime,DateTime>>> activeDates = new Dictionary<long, List<Tuple<DateTime, DateTime>>>();

        public List<BioData> bioData = new List<BioData>();

        public List<UserAction> userActions = new List<UserAction>();

        public MainWindow mainWindow;

        public Dictionary<string, ChartWindow> chartWindows;

        private SqlConnection connection;

        public PostTradingAnalysisApplication()
        {
            // create windows
            mainWindow = new MainWindow(this);

            // connect database
            var cb = new SqlConnectionStringBuilder();
            cb.DataSource = Settings.Default.DataSource;
            cb.UserID = Settings.Default.UserID;
            cb.Password = Settings.Default.Password;
            cb.InitialCatalog = Settings.Default.InitialCatalog;

            Console.WriteLine($"Connecting to database {cb.InitialCatalog} on {cb.DataSource} ...");
            connection = new SqlConnection(cb.ConnectionString);
            connection.Open();

            // Load Users
            int kensIndex = -1;
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = @"SELECT ID, FullName, Mail, Login From Users ORDER BY FullName";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = User.FromSqlDataReader(reader);
                        users.Add(user);
                        var itemIndex = mainWindow.cbUsers.Items.Add($"{user.fullName}  ({user.login})  -  {user.mail}");
                        cbUserInd2UserID[itemIndex] = user.id;
                        if (user.login == "KenMedanic")
                        {
                            kensIndex = itemIndex;
                        }
                        activeDates[user.id] = new List<Tuple<DateTime, DateTime>>();
                    }
                }
            }

            // load dates where users traded
            foreach (var user in users)
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = @"SELECT Time,ActionID From UserActions WHERE UserID = @UserID AND (ActionID = 3 OR ActionID = 4) ORDER BY Time";
                    cmd.Parameters.AddWithValue("@UserId", user.id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        int prevAction = -1;
                        DateTime prevDate = new DateTime();
                        while (reader.Read())
                        {
                            var action = int.Parse(reader["ActionID"].ToString());
                            var date = DateTime.Parse(reader["Time"].ToString());
                            if (prevAction == -1 && action == 3)
                            {
                                prevAction = action;
                                prevDate = date;
                            }else if (prevAction == 3 && action == 4)
                            {
                                activeDates[user.id].Add(new Tuple<DateTime,DateTime>(prevDate, date));
                                prevAction = -1;
                            }
                        }
                    }
                    activeDates[user.id].Reverse();
                }
            }

            if (kensIndex != -1)
            {
                mainWindow.cbUsers.SelectedIndex = kensIndex;
            }

            // registering windows
            chartWindows = new Dictionary<string, ChartWindow>();
            chartWindows["Temperature"] = new ChartWindow(this, Color.Red, mainWindow.mainChartsToolStripMenuItem, "Temperature");
            chartWindows["Heart rate"] = new ChartWindow(this, Color.Green, mainWindow.mainChartsToolStripMenuItem, "Heart rate");
            chartWindows["Skin conductance"] = new ChartWindow(this, Color.Blue, mainWindow.mainChartsToolStripMenuItem, "Skin conductance");
            chartWindows["Price"] = new ChartWindow(this, Color.Brown, mainWindow.mainChartsToolStripMenuItem, "Price");
            chartWindows["Training step"] = new ChartWindow(this, Color.Orange, mainWindow.mainChartsToolStripMenuItem, "Training step");

            chartWindows["Temperature stddev"] = new ChartWindow(this, Color.Red, mainWindow.stddevToolStripMenuItem, "T_1");
            chartWindows["Heart rate stddev"] = new ChartWindow(this, Color.Green, mainWindow.stddevToolStripMenuItem, "H_1");
            chartWindows["Skin conductance stddev"] = new ChartWindow(this, Color.Blue, mainWindow.stddevToolStripMenuItem, "S_1");
            chartWindows["Price stddev"] = new ChartWindow(this, Color.Brown, mainWindow.stddevToolStripMenuItem, "P_1");

            chartWindows["Temperature stddev away"] = new ChartWindow(this, Color.Red, mainWindow.stddevAwayToolStripMenuItem, "T_2");
            chartWindows["Heart rate stddev away"] = new ChartWindow(this, Color.Green, mainWindow.stddevAwayToolStripMenuItem, "H_2");
            chartWindows["Skin conductance stddev away"] = new ChartWindow(this, Color.Blue, mainWindow.stddevAwayToolStripMenuItem, "S_2");
            chartWindows["Price stddev away"] = new ChartWindow(this, Color.Brown, mainWindow.stddevAwayToolStripMenuItem, "P_2");

            chartWindows["Temperature velocity"] = new ChartWindow(this, Color.Red, mainWindow.velocityToolStripMenuItem, "T_3");
            chartWindows["Heart rate velocity"] = new ChartWindow(this, Color.Green, mainWindow.velocityToolStripMenuItem, "H_3");
            chartWindows["Skin conductance velocity"] = new ChartWindow(this, Color.Blue, mainWindow.velocityToolStripMenuItem, "S_3");
            chartWindows["Price velocity"] = new ChartWindow(this, Color.Brown, mainWindow.velocityToolStripMenuItem, "P_3");

            chartWindows["Temperature velocity stdaway"] = new ChartWindow(this, Color.Red, mainWindow.velocityStdAwayToolStripMenuItem, "T_4");
            chartWindows["Heart rate velocity stdaway"] = new ChartWindow(this, Color.Green, mainWindow.velocityStdAwayToolStripMenuItem, "H_4");
            chartWindows["Skin conductance velocity stdaway"] = new ChartWindow(this, Color.Blue, mainWindow.velocityStdAwayToolStripMenuItem, "S_4");
            chartWindows["Price velocity stdaway"] = new ChartWindow(this, Color.Brown, mainWindow.velocityStdAwayToolStripMenuItem, "P_4");

            chartWindows["SC stddev * Price stddev"] = new ChartWindow(this, Color.DarkMagenta, mainWindow.signalsToolStripMenuItem, "SC stddev * Price stddev");
            chartWindows["SC stdev away - Price stddev away"] = new ChartWindow(this, Color.DarkMagenta, mainWindow.signalsToolStripMenuItem, "SC stdev away - Price stddev away");

            // setup windows
            foreach (var kv in chartWindows)
            {
                var chartName = kv.Key;
                var window = kv.Value;
                window.Text = window.WindowName;
                window.Owner = mainWindow;

                // update toolstrip
                ToolStripMenuItem menuItem = new ToolStripMenuItem(chartName);
                window.ChartGroupItem.DropDownItems.Add(menuItem);
                menuItem.Click += new System.EventHandler(
                    delegate (Object sender, EventArgs e)
                    {
                        window.Show();
                        window.BringToFront();
                    });
            }

            Application.Run(mainWindow);
        }

        public void LoadData(DateTime dateFrom, DateTime dateTo)
        {
            if (dateFrom >= dateTo)
            {
                throw new Exception("First date should be less than second date!");
            }

            // biodata Loading
            bioData.Clear();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = @"SELECT * From BioDataWithPrice
                WHERE UserID = @UserId AND Time BETWEEN @StartDate AND @EndDate
                ORDER BY Time";
                cmd.Parameters.AddWithValue("@UserId", cbUserInd2UserID[mainWindow.cbUsers.SelectedIndex]);
                cmd.Parameters.AddWithValue("@StartDate", dateFrom);
                cmd.Parameters.AddWithValue("@EndDate", dateTo);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var data = BioData.FromSqlDataReader(reader);
                        bioData.Add(data);
                    }
                    reader.Close();
                }
            }

            // user acitons Loading
            userActions.Clear();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = @"SELECT * From UserActions
                WHERE UserID = @UserId AND Time BETWEEN @StartDate AND @EndDate
                ORDER BY Time";
                cmd.Parameters.AddWithValue("@UserId", cbUserInd2UserID[mainWindow.cbUsers.SelectedIndex]);
                cmd.Parameters.AddWithValue("@StartDate", dateFrom);
                cmd.Parameters.AddWithValue("@EndDate", dateTo);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var data = UserAction.FromSqlDataReader(reader);
                        userActions.Add(data);
                    }
                    reader.Close();
                }
            }

            foreach (var kv in chartWindows)
            {
                var chartName = kv.Key;
                var window = kv.Value;

                var series = new LineSeries();
                series.Title = chartName;
                series.StrokeThickness = 1;
                series.Color = OxyColor.FromUInt32((uint)window.Color.ToArgb());

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
            SetChartPeriod(stdDevPeriod);

            // update annotations
            foreach (var kv in chartWindows)
            {
                kv.Value.UpdateUserActionsAnnotations();
            }
        }

        private int stdDevPeriod = 120;
        public void SetChartPeriod(int stdDevPeriod)
        {
            this.stdDevPeriod = stdDevPeriod;

            if (chartWindows == null || chartWindows.Count == 0)
            {
                return;
            }

            // update stddev and stddev away charts only
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
                series.Color = OxyColor.FromUInt32((uint)window.Color.ToArgb());

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


            // update velocity charts only
            foreach (var kv in chartWindows)
            {
                var chartName = kv.Key;
                var window = kv.Value;
                var model = window.plotView.Model;
                if (!chartName.EndsWith(" velocity"))
                {
                    continue;
                }

                var series = new LineSeries();
                series.Title = chartName;
                series.StrokeThickness = 1;
                series.Color = OxyColor.FromUInt32((uint)window.Color.ToArgb());

                double sum = 0;
                int realCount = 0;
                series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(bioData[0].time), double.NaN));
                for (int i = 1; i < bioData.Count; i++)
                {
                    realCount++;
                    if (chartName == "Temperature velocity")
                        sum += Math.Abs(bioData[i - 1].temperature - bioData[i].temperature);
                    if (chartName == "Heart rate velocity")
                        sum += Math.Abs(bioData[i - 1].heartRate - bioData[i].heartRate);
                    if (chartName == "Skin conductance velocity")
                        sum += Math.Abs(bioData[i - 1].skinConductance - bioData[i].skinConductance);
                    if (chartName == "Price velocity")
                    {
                        if (bioData[i - 1].buyPrice.HasValue && bioData[i].buyPrice.HasValue)
                        {
                            sum += Math.Abs(bioData[i - 1].buyPrice.Value - bioData[i].buyPrice.Value);
                        }
                        else
                        {
                            realCount--;
                        }
                    }

                    if (i < stdDevPeriod - 1)
                    {
                        series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(bioData[i].time), double.NaN));
                        continue;
                    }

                    double velocity = realCount < stdDevPeriod - 1 ? double.NaN : sum / realCount;
                    //double velocity = sum / (stdDevPeriod - 1);
                    //if (realCount < stdDevPeriod - 1)
                    //    velocity = double.NaN;
                    series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(bioData[i].time), velocity));

                    realCount--;
                    if (chartName == "Temperature velocity")
                        sum -= Math.Abs(bioData[i - stdDevPeriod + 1].temperature - bioData[i - stdDevPeriod + 2].temperature);
                    if (chartName == "Heart rate velocity")
                        sum -= Math.Abs(bioData[i - stdDevPeriod + 1].heartRate - bioData[i - stdDevPeriod + 2].heartRate);
                    if (chartName == "Skin conductance velocity")
                        sum -= Math.Abs(bioData[i - stdDevPeriod + 1].skinConductance - bioData[i - stdDevPeriod + 2].skinConductance);
                    if (chartName == "Price velocity")
                    {
                        if (bioData[i - stdDevPeriod + 1].buyPrice.HasValue && bioData[i - stdDevPeriod + 2].buyPrice.HasValue)
                        {
                            sum -= Math.Abs(bioData[i - stdDevPeriod + 1].buyPrice.Value - bioData[i - stdDevPeriod + 2].buyPrice.Value);
                        }
                        else
                        {
                            realCount--;
                        }
                    }
                }

                model.Series.Clear();
                model.Series.Add(series);

                window.plotView.Model.InvalidatePlot(true);
            }



            // update velocity stddev away charts only
            foreach (var kv in chartWindows)
            {
                var chartName = kv.Key;
                var window = kv.Value;
                var model = window.plotView.Model;
                if (!chartName.EndsWith(" velocity stdaway"))
                {
                    continue;
                }

                LineSeries velocity = null;
                if (chartName == "Temperature velocity stdaway")
                    velocity = (LineSeries)chartWindows["Temperature velocity"].plotView.Model.Series[0];
                if (chartName == "Heart rate velocity stdaway")
                    velocity = (LineSeries)chartWindows["Heart rate velocity"].plotView.Model.Series[0];
                if (chartName == "Skin conductance velocity stdaway")
                    velocity = (LineSeries)chartWindows["Skin conductance velocity"].plotView.Model.Series[0];
                if (chartName == "Price velocity stdaway")
                    velocity = (LineSeries)chartWindows["Price velocity"].plotView.Model.Series[0];

                var series = new LineSeries();
                series.Title = chartName;
                series.StrokeThickness = 1;
                series.Color = OxyColor.FromUInt32((uint)window.Color.ToArgb());

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
                        mean += velocity.Points[j].Y;
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
                        val = velocity.Points[j].Y;
                        sum += (val - mean) * (val - mean);
                    }

                    sum /= realCount;
                    double stddev = Math.Sqrt(sum);

                    double stddevAway = stddev > 0 ? (val - mean) / stddev : 0;

                    series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(bioData[i].time), stddevAway));
                }

                model.Series.Clear();
                model.Series.Add(series);

                window.plotView.Model.InvalidatePlot(true);
            }


            // update signal charts
            {
                var chartName = "SC stddev * Price stddev";
                var window = chartWindows[chartName];
                var model = window.plotView.Model;

                var series = new LineSeries();
                series.Title = chartName;
                series.StrokeThickness = 1;
                series.Color = OxyColor.FromUInt32((uint)window.Color.ToArgb());

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
                    if (scVal != 0)
                        value = Math.Abs(scVal) - Math.Abs(priceVal);
                    else
                        value = double.NaN;
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
