using NeuroXChange.Model.BioData;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace PostTradingAnalysis
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tbFile.Text = openFileDialog.FileName;
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            openFileDialog.InitialDirectory = "Data";

            var now = DateTime.Now;
            var timeFrom = new DateTime(now.Year, now.Month, now.Day);
            var timeTo = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);

            //dtpFrom.Value = timeFrom;
            dtpTo.Value = timeTo;

            btnLoad_Click(null, null);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        List<BioData> bioData = new List<BioData>();

        private void LoadData()
        {
            var fileName = tbFile.Text;
            if (!File.Exists(fileName))
            {
                throw new Exception("There are no such file: \"" + fileName + "\"");
            }

            var dateFrom = dtpFrom.Value;
            var dateTo = dtpTo.Value;
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
                @"SELECT BioData.*, sellPrice, buyPrice From BioData
            LEFT OUTER JOIN PriceAtBioDataTick ON BioData.ID = PriceAtBioDataTick.ID
            WHERE BioData.Time BETWEEN #{0:MM/dd/yyyy HH:mm:ss}# AND #{1:MM/dd/yyyy HH:mm:ss}#
            ORDER BY BioData.ID",
                dateFrom, dateTo);
            cmd.CommandText = biodataSQL;
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var data = BioData.FromOleDbDataReader(reader, true);
                bioData.Add(data);
            }

            var hrSeries = new LineSeries();
            hrSeries.Title = "Heart rate";
            hrSeries.StrokeThickness = 1;

            for (int i = 0; i < bioData.Count && i < 10000; i++)
            {
                hrSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(bioData[i].time), bioData[i].heartRate));
            }

            var model = new PlotModel();
            model.Series.Add(hrSeries);
            var xAxis = new DateTimeAxis { Position = AxisPosition.Bottom, StringFormat = "HH:mm:ss" };
            xAxis.MajorGridlineColor = OxyColor.FromRgb(200, 200, 200);
            xAxis.MajorGridlineStyle = LineStyle.Solid;
            model.Axes.Add(xAxis);
            var yAxis = new LinearAxis { Position = AxisPosition.Left};
            yAxis.MajorGridlineColor = OxyColor.FromRgb(200, 200, 200);
            yAxis.MajorGridlineStyle = LineStyle.Solid;
            model.Axes.Add(yAxis);

            var controller = new PlotController();
            controller.BindMouseDown(OxyMouseButton.Left, PlotCommands.PanAt);
            controller.BindMouseDown(OxyMouseButton.Right, PlotCommands.HoverTrack);
            //controller.BindMouseDown(OxyMouseButton.Left, PlotCommands.)
            plotView.Controller = controller;

            plotView.Model = model;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnZoom_Click(object sender, EventArgs e)
        {
            var axis = plotView.Model.Axes[0];
            if (sender == btnVMinus || sender == btnVPlus)
            {
                axis = plotView.Model.Axes[1];
            }

            var mult = 0.5 * 2.0 / 3.0;
            if (sender == btnHMinus || sender == btnVMinus)
            {
                mult = 0.5 * 3.0 / 2.0;
            }

            var min = axis.ActualMinimum;
            var max = axis.ActualMaximum;
            var dist = (max - min) * mult;
            var mid = (max + min) / 2;
            axis.Zoom(mid - dist, mid + dist);
            plotView.Refresh();
        }
    }
}
