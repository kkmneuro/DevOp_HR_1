using NeuroXChange.Model.BioData;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

using LiveCharts; //Core of the library
using LiveCharts.Wpf; //The WPF controls
using LiveCharts.WinForms; //the WinForm wrappers
using System.Windows.Media;
using LiveCharts.Configurations;

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

        ChartValues<BioData> bioData = new ChartValues<BioData>();

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
                //if (bioData.Count < 1000)
                {
                    bioData.Add(data);
                }
            }

            var hrValues = bioData;

            //var hrValues = new ChartValues<double>();

            //// biodata charts
            //foreach (var bioDataPoint in bioData)
            //{
            //    hrValues.Add(bioDataPoint.heartRate);
            //    if(hrValues.Count >= 2000)
            //    {
            //        //break;
            //    }
            //}

            var mapper = Mappers.Xy<BioData>()
                .X(point => point.time.ToOADate())
                .Y(point => point.heartRate);

            var hrSeries = new LineSeries(mapper) {
                Title = "Heart rate",
                Values = hrValues
            };
            hrSeries.LineSmoothness = 0;

            hrSeries.PointGeometry = null;
            hrSeries.StrokeThickness = 1;
            hrSeries.Fill = Brushes.Transparent;
            
            mainChart.DisableAnimations = true;
            mainChart.DataTooltip = null;
            mainChart.Pan = PanningOptions.X;
            mainChart.Zoom = ZoomingOptions.X;
            mainChart.Hoverable = false;
            mainChart.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                LabelFormatter = value => DateTime.FromOADate(value).ToString("MM/dd HH:mm:ss")
            });

            var collection = new LiveCharts.SeriesCollection { hrSeries };
            mainChart.Series = collection;
        }
    }
}
