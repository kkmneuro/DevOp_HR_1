using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MarketData;

namespace SelectionControl
{
    public partial class PriceChartControl : UserControl
    {
        public Chart Chart { get { return _priceChart; }  }


        private ChartArea chartArea1;
        private Legend legend1;
        private Series series1;

        public string SeriasName { get; set; }
        

        /// <summary>
        /// time frame of the chart
        /// </summary>
        public TimeFrame.TF TimeFram { get; set; }
        private PriceList p;

        public PriceList P { get { return p; } set { p = value; } }

        public PriceChartControl()
        {
            InitializeComponent();

            /*_priceChart.Legends["Legend1"].Position.Auto = false;
            _priceChart.Legends["Legend1"].Position = new ElementPosition(30, 5, 100, 20); */


            _priceChart.Legends["Legend1"].Docking = Docking.Top;
            _priceChart.Legends["Legend1"].DockedToChartArea = "ChartArea1";
            _priceChart.Legends["Legend1"].IsDockedInsideChartArea = true;
            _priceChart.Legends["Legend1"].Alignment = StringAlignment.Far;



            adjustChart(_priceChart);

        }

        /// <summary>
        /// Show or hide legend
        /// </summary>
        /// <param name="b"></param>
        public void ShowLegend(bool b)
        {
                foreach (var s in _priceChart.Series) s.IsVisibleInLegend = b;
        }

        private void adjustChart(Chart c)
        {
            c.ChartAreas[0].CursorY.IsUserEnabled = true;
            c.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            c.ChartAreas[0].CursorY.Interval = 0;
            c.ChartAreas[0].AxisY.ScaleView.Zoomable = false;
            c.ChartAreas[0].CursorY.LineColor = Color.Black;
            c.ChartAreas[0].CursorY.LineWidth = 1;
            c.ChartAreas[0].CursorY.LineDashStyle = ChartDashStyle.Dot;
            c.ChartAreas[0].CursorY.Interval = 0;

            //  _priceChart.MouseClick += _priceChart_MouseClick;

            c.MouseMove += _priceChart_MouseMove;  //  _priceChart_MouseMove;
        }

        private void _priceChart_MouseMove(object sender, MouseEventArgs e)
        {

            Point mousePoint = new Point(e.X, e.Y);

            //    _priceChart.ChartAreas[0].CursorX.SetCursorPixelPosition(mousePoint, true);
            _priceChart.ChartAreas[0].CursorY.SetCursorPixelPosition(mousePoint, true);

            // ...
        }


        /// <summary>
        /// Check is it a time to start a new candle 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="evArg"></param>
        /// <returns></returns>
        private bool NewCandle(PriceCandle p, PriceTickEventArgs evArg, TimeFrame.TF _timeFrame)
        {

            bool makeANewCandle = false;

            switch (_timeFrame) //evArg.TimeFrame)
            {
                case TimeFrame.TF.Day:
                    if (evArg.PriceTime.Day != p.Date.Day)
                        makeANewCandle = true;
                    break;
                case TimeFrame.TF.Hour:
                    if (evArg.PriceTime.Hour != p.Date.Hour)
                        makeANewCandle = true;
                    break;
                case TimeFrame.TF.Min20:
                    if (evArg.PriceTime.Subtract(p.Date).TotalMinutes > 20)
                        makeANewCandle = true;
                    break;
                case TimeFrame.TF.Min5:
                    if (evArg.PriceTime.Subtract(p.Date).TotalMinutes > 5)
                        makeANewCandle = true;
                    break;
                case TimeFrame.TF.Min:
                    if (evArg.PriceTime.Minute != p.Date.Minute)
                        makeANewCandle = true;
                    break;
                case TimeFrame.TF.Tick:
                    makeANewCandle = true;
                    break;
            }
            return makeANewCandle;
        }

        /// <summary>
        /// updating price from live data feed Tick is coming from event in 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void UpdateLastPrice(object source, PriceTickEventArgs e)
        {
            if (p != null)
            {
                if (p.p.Count > 0)
                    if (NewCandle(p.p[p.p.Count - 1], e, TimeFram)) // last candle and new tick will desice will we need a  new candle
                    {
                        PriceCandle pp = new PriceCandle();
                        //switch (e.TimeFrame)
                        pp.Date = p.p[p.p.Count - 1].Date.AddMinutes(p.LengthOfCandle().Minutes);
                        pp.Open = pp.Close = pp.High = pp.Low = e.Price;
                        //  p.RemoveAt(0);                 
                        p.p.Add(pp);
                    }
                    else
                    {
                        p.p[p.p.Count - 1].Close = e.Price;

                        if (p.p[p.p.Count - 1].High < e.Price) p.p[p.p.Count - 1].High = e.Price;
                        if (p.p[p.p.Count - 1].Low > e.Price) p.p[p.p.Count - 1].Low = e.Price;
                    }
            }

            if (_priceChart != null)
            {
                if (_priceChart.InvokeRequired)
                {
                    _priceChart.Invoke(new MethodInvoker(delegate
                    {
                        _priceChart.DataBind(); // perfomance ???
                    }));
                }



            }

        }

        public void PrepareForLiveData(Future Security, TimeFrame.TF tf)
        {
            chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            series1 = new System.Windows.Forms.DataVisualization.Charting.Series();



            this._priceChart.Dock = System.Windows.Forms.DockStyle.Fill;

            this._priceChart.Location = new System.Drawing.Point(0, 0);
            this._priceChart.Name = "_priceChart";
            this._priceChart.Size = new System.Drawing.Size(528, 203);
            this._priceChart.TabIndex = 0;
            this._priceChart.Text = "_priceChart";

            adjustChart(_priceChart);

            

            if (Security != null) SeriasName = Security.Name + " " + tf;
            else SeriasName = Security.Name + " " + tf;

            


            if (_priceChart.Series.Count > 0) _priceChart.Series.RemoveAt(_priceChart.Series.Count - 1);
            _priceChart.Series.Add(new Series(SeriasName));


            _priceChart.Series[SeriasName].ChartType = SeriesChartType.Candlestick;
            _priceChart.DataSource = p.p;

            _priceChart.Series[SeriasName].XValueMember = "Date";
            _priceChart.Series[SeriasName].YValueMembers = "High, Low, Open, Close";
            _priceChart.DataBind();
            _priceChart.Series[SeriasName].BorderColor = System.Drawing.Color.Black;
            _priceChart.Series[SeriasName].Color = System.Drawing.Color.Black;
            _priceChart.Series[SeriasName].CustomProperties = "PriceDownColor=Red, PriceUpColor=Green";
            //  _priceChart.Series[seriasName].XValueType = ChartValueType.DateTime;
            _priceChart.ChartAreas[0].AxisX.LabelStyle.Format = "MM.dd HH:mm";
            //     _priceChart.Series[seriasName].IsXValueIndexed = true;
            //  _priceChart.DataManipulator.IsEmptyPointIgnored = true;
            //   _priceChart.Series[seriasName].XValueType = ChartValueType.Time; 
            _priceChart.ChartAreas[0].AxisX.MajorGrid.LineWidth = 1;
            _priceChart.ChartAreas[0].AxisY.MajorGrid.LineWidth = 1;

            //       _priceChart.DataManipulator.IsStartFromFirst = true;


            _priceChart.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;
            _priceChart.ChartAreas[0].AxisY.IsStartedFromZero = false;
            _priceChart.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
        } 
    }
}
