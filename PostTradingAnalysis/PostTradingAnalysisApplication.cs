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
            chartWindows["Temperature"] = new ChartWindow(this);
            chartWindows["Heart rate"] = new ChartWindow(this);
            chartWindows["Skin conductance"] = new ChartWindow(this);
            chartWindows["Price"] = new ChartWindow(this);

            // setup windows
            foreach (var kv in chartWindows)
            {
                var chartName = kv.Key;
                var window = kv.Value;
                window.Text = chartName;
                window.Owner = mainWindow;
                ToolStripMenuItem menuItem = new ToolStripMenuItem(kv.Key);
                mainWindow.chartsToolStripMenuItem.DropDownItems.Add(menuItem);
                menuItem.Click += new System.EventHandler(
                    delegate (Object sender, EventArgs e) {
                        window.Show();
                        window.BringToFront();
                    });
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
            WHERE BioData.Time BETWEEN #{0:MM/dd/yyyy HH:mm:ss}# AND #{1:MM/dd/yyyy HH:mm:ss}#
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
                if (chartName == "Price")
                    for (int i = 0; i < bioData.Count; i++)
                        if (bioData[i].buyPrice.HasValue)
                            series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(bioData[i].time), bioData[i].buyPrice.Value));

                Color color = Color.Green;
                if (chartName == "Temperature")
                    color = Color.Red;
                if (chartName == "Heart rate")
                    color = Color.Green;
                if (chartName == "Skin conductance")
                    color = Color.Blue;
                if (chartName == "Price")
                    color = Color.Brown;

                series.Color = OxyColor.FromUInt32((uint)color.ToArgb());

                var model = new PlotModel();
                model.PlotMargins = new OxyThickness(30, -8, -7, 6);
                model.DefaultFontSize = 10;
                model.Series.Add(series);

                var xAxis = new DateTimeAxis { Position = AxisPosition.Bottom, StringFormat = "H:mm:ss" };
                xAxis.MajorGridlineStyle = LineStyle.Solid;
                xAxis.AxisChanged += HandleXAxisChanged;
                xAxis.AxisTickToLabelDistance = 0;
                xAxis.AxisTitleDistance = 0;
                xAxis.MajorTickSize = 0;
                model.Axes.Add(xAxis);
                var yAxis = new LinearAxis { Position = AxisPosition.Left };
                yAxis.MajorGridlineStyle = LineStyle.Solid;
                yAxis.MajorTickSize = 0;
                model.Axes.Add(yAxis);

                var controller = new PlotController();
                controller.BindMouseDown(OxyMouseButton.Left, PlotCommands.PanAt);
                controller.BindMouseDown(OxyMouseButton.Right, PlotCommands.HoverTrack);
                window.plotView.Controller = controller;

                window.plotView.Model = model;
            }
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
