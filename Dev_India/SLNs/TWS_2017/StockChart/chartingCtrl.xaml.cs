using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ModulusFE;
using System.Data.OleDb;
using System.Data;
using System.Globalization;
using System.Windows.Threading;
using System.Collections;
using System.Threading;
using ModulusFE.OMS.Interface;
using ModulusFE.DS.Random;
using System.Diagnostics;
using ModulusFE.LineStudies;

namespace StockChart
{

    
    /// <summary>
    /// Interaction logic for chartingCtrl.xaml
    /// </summary>
    public partial class chartingCtrl : UserControl
    {
        DataTable dataTable = new DataTable();
        DispatcherTimer CHartingTimer;
        Hashtable ht_MinuteData = null;
        Dictionary<DateTime, OHLC> minuteData = new Dictionary<DateTime, OHLC>();
        static ChartPanel OhlcCHart;
        static ChartPanel VolCHart;
        readonly DsRandom _gen = new DsRandom();
        private List<BarData> _data;
        public chartingCtrl()
        {
            InitializeComponent();
            ht_MinuteData = new Hashtable();
            //readDatafromCSV();
            //CHartingTimer = new DispatcherTimer();
            //CHartingTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            //CHartingTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            //CHartingTimer.Start();
            //_stockChartX.Symbol = "SILVERM1";
            //CreateContextMenu();
        }

        public void setSymbol(String symbol)
        {
            _stockChartX.Symbol = symbol;
        }


        public void AddSymbolObject(string key, string type)
        {
            SymbolType symbtype = SymbolType.Signal;
            switch (type)
            {
                case "Buy Alert":
                    symbtype = SymbolType.Buy;
                    break;
                case "Sell Alert":
                    symbtype = SymbolType.Sell;
                    break;
                case "Exit-Long Alert":
                    symbtype = SymbolType.ExitLong;
                    break;
                case "Exit-Short Alert":
                    symbtype = SymbolType.ExitShort;
                    break;
                default:
                    break;
            }
            _stockChartX.AddSymbolObject(symbtype, key);
            _stockChartX.Update();
        }

        public void ApplyLineStudy(string applyLineStudy)
        {
            string sLineStudyType = applyLineStudy;
            LineStudy.StudyTypeEnum studyTypeEnum;
            object[] args = new object[0];
            double strokeThicknes = 1;

            if (sLineStudyType == "ImageFromFile")
            {
                studyTypeEnum = LineStudy.StudyTypeEnum.ImageObject;
                switch (sLineStudyType)
                {
                    case "ImageFromFile": args = new[] { @"Images\smiley.bmp" }; //load a file from disk
                        break;
                }
            }
            //else if (sLineStudyType == "WpfControl")
            //{
            //    studyTypeEnum = LineStudy.StudyTypeEnum.FrameworkElement;
            //    args = new[] { new CustomChartControl() };
            //}
            else
            {
                studyTypeEnum = (LineStudy.StudyTypeEnum)Enum.Parse(typeof(LineStudy.StudyTypeEnum), sLineStudyType);

                //set some extra parameters to line studies
                switch (studyTypeEnum)
                {
                    case LineStudy.StudyTypeEnum.StaticText:
                        args = new object[] { "Some text for testing" };
                        strokeThicknes = 12; //for text objects is FontSize
                        break;
                    case LineStudy.StudyTypeEnum.FibonacciRetracements:
                        //Sample how to set Fibonacii retracements in reverse order
                        //            args = new object[]
                        //                     {
                        //                       0.090138301,
                        //                       0.145854856,
                        //                       0.236011094,
                        //                       0.381894974,
                        //                       0.617953032,
                        //                       0.999924,
                        //                       1.618,
                        //                     };
                        break;
                    case LineStudy.StudyTypeEnum.VerticalLine:
                        //when first parameter is false, vertical line will display DataTime instead on record number
                        args = new object[]
                     {
                       false, //true - show record number, false - show datetime
                       true, //true - show text with line, false - show only line
                       "d", //custom datetime format, when args[0] == false. See MSDN:DateTime.ToString(string) for legal values
                     };
                        break;
                    default:
                        break;
                }
            }

            string studyName = studyTypeEnum.ToString();
            int count = _stockChartX.GetLineStudyCountByType(studyTypeEnum);
            if (count > 0)
                studyName += count;
            LineStudy lineStudy = _stockChartX.AddLineStudy(studyTypeEnum, studyName, Brushes.Red, args);
            lineStudy.StrokeThickness = strokeThicknes;
            if (studyTypeEnum == LineStudy.StudyTypeEnum.HorizontalLine)
                lineStudy.StrokeType = LinePattern.DashDot;

            //if linestudy is a text object we can change its text directly
            if (lineStudy.GetType() == typeof(StaticText))
                ((StaticText)lineStudy).Text = "Some other text for testing";
            //by default rectangle and ellipse have transparent background.
            //but since they implement the IShapeAble interface we can change the default background
            if (lineStudy is IShapeAble)
                ((IShapeAble)lineStudy).Fill = new LinearGradientBrush
                {
                    Opacity = 0.3,
                    StartPoint = new Point(0, 0.5),
                    EndPoint = new Point(1, 0.5),
                    GradientStops = new GradientStopCollection
                                                            {
                                                              new GradientStop(Colors.Yellow, 0),
                                                              new GradientStop(Colors.LightSteelBlue, 0.5),
                                                              new GradientStop(Colors.Blue, 1)
                                                            }
                };
            //if (lineStudy is IMouseAble)
            //{
            //    ((IMouseAble)lineStudy).MouseEnter += (o, eventArgs) => Debug.WriteLine("MouseEnter. object " + o);
            //    ((IMouseAble)lineStudy).MouseLeave += (o, eventArgs) => Debug.WriteLine("MouseLeave. object " + o);
            //}

            //if (studyTypeEnum == LineStudy.StudyTypeEnum.HorizontalLine)
            //{
            //    lineStudy.ValuePresenterAlignment = LineStudy.ValuePresenterAlignmentType.Right;
            //    lineStudy.LineStudyValue = new CustomHorLineValueGetter(_stockChartX);
            //}
        }

        public void TrackCursor(bool flag)
        {
            //_stockChartX.CrossHairs = flag;
            //_stockChartX.Update();
        }
        // #endregion Top Menu - Price Styles


        public void CrossHair()
        {
            _stockChartX.CrossHairs = !_stockChartX.CrossHairs;
        }

        public void ApplyIndicatorNew(string _selectedindicator)
        {
            _selectedindicator = _selectedindicator.Trim();
            //StockChartX_IndicatorsParameters.IndicatorParameters indicator = new StockChartX_IndicatorsParameters.IndicatorParameters(_selectedindicator);
            StockChartX_IndicatorsParameters.IndicatorParameters indicator = (StockChartX_IndicatorsParameters.IndicatorParameters)((from indicators in StockChartX_IndicatorsParameters.Indicators
                                                                                                                                     where indicators.IndicatorRealName == _selectedindicator
                                                                                                                                     select indicators).First());

            //StockChartX_IndicatorsParameters.IndicatorParameters indicator = obj.

            // _selectedindicator as StockChartX_IndicatorsParameters.IndicatorParameters;
            if (indicator == null)
            {
                MessageBox.Show("Could not get indicator");
                return;
            }
            if (indicator.IndicatorType == IndicatorType.CustomIndicator)
            {
                MessageBox.Show("Custom indicator can't be added via this. Use menu point [Data/Add Custom Indicator]", "Error",
                  MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int indicatorCount = _stockChartX.GetIndicatorCountByType(indicator.IndicatorType);
            string name = indicator.IndicatorRealName + (indicatorCount > 0 ? indicatorCount.ToString() : "");
            bool newPanel = !_stockChartX.IsOverlayIndicator(indicator.IndicatorType);
            ChartPanel panel = !newPanel
                                 ? _stockChartX.GetPanelByIndex(0)
                                 : _stockChartX.AddChartPanel();

            // ChartPanel panel = _stockChartX.AddChartPanel();

            double totalPanelHeight = _stockChartX.PanelsCollection.Sum(p => p.Height);
            double pnlheight = totalPanelHeight / _stockChartX.PanelsCount;
            for (int i = 0; i < _stockChartX.PanelsCount; i++)
            {
                _stockChartX.SetPanelHeight(i, pnlheight);
            }

            //chart will return when creating a new panel in casw it doesn't have enough space to place it
            if (panel == null)
            {
                MessageBox.Show("Chart doesn't have enough place to a new panel. Please remove some or resize existing.",
                                "Not enough space", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var ind = _stockChartX.AddIndicator(indicator.IndicatorType, name, panel, true);

            if (indicator.IndicatorType == IndicatorType.BollingerBands)
            {
                ind.SetParameterValue(1, 28);
            }

            if (newPanel)
                panel.Background = Brushes.Black;

            _stockChartX.Update();
        }

        private void stockChartX_SeriesRightClick(object sender, StockChartX.SeriesRightClickEventArgs e)
        {

        }

        private void stockChartX_IndicatorBeforeDelete(object sender, StockChartX.SeriesBeforeDeleteEventArgs e)
        {

        }

        private void stockChartX_LineStudyRightClick(object sender, StockChartX.LineStudiesRightClickEventArgs e)
        {

        }

        private void stockChartX_LineStudyBeforeDelete(object sender, StockChartX.LineStudyBeforeDeleteEventArgs e)
        {

        }

        private void _stockChartX_LineStudyDoubleClick(object sender, StockChartX.LineStudyMouseEventArgs e)
        {

        }

        private void _stockChartX_TrendLinePenetration(object sender, StockChartX.TrendLinePenetrationArgs e)
        {

        }

        private void _stockChartX_ChartPanelBeforeClose(object sender, StockChartX.ChartPanelBeforeCloseEventArgs e)
        {

        }

        private void _stockChartX_IndicatorDoubleClick(object sender, StockChartX.IndicatorDoubleClickEventArgs e)
        {

        }

        private void _stockChartX_OnCustomIndicatorNeedsData(object sender, StockChartX.CustomIndicatorNeedsDataEventArgs e)
        {

        }


        private void _stockChartX_SeriesMoved(object sender, StockChartX.SeriesMovedEventArgs e)
        {

        }

        private void _stockChartX_ShowInfoPanel(object sender, StockChartX.ShowInfoPanelEventArgs e)
        {

        }

        private void _stockChartX_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void _stockChartX_UserDrawingComplete(object sender, StockChartX.UserDrawingCompleteEventArgs e)
        {

        }

        private void _stockChartX_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void _stockChartX_ChartScroll(object sender, EventArgs e)
        {

        }

        private void _stockChartX_ChartZoom(object sender, EventArgs e)
        {

        }

        private DataTable dtData;
        public DataTable DTDAta
        {
            set
            {
                dtData = value;
            }
        }

        private void _stockChartX_ChartPanelMouseLeftClick(object sender, StockChartX.ChartPanelMouseLeftClickEventArgs e)
        {
            try
            {
                Series close = _stockChartX[SeriesTypeOHLC.Close];
                int recordIndex = _stockChartX.GetReverseX(e.X);

                Debug.WriteLine(string.Format("Panel Index: {0}, MouseX: {1}, MouseY: {2}, PriceValue: {3}, TimeStamp: {4}. Close value: {5}",
                                              e.Panel.Index, e.X, e.Y, e.Price, e.Timestamp,
                                              recordIndex == -1 ? -1 : close[recordIndex + _stockChartX.FirstVisibleRecord].Value));
            }
            catch (Exception ex) { }
        }

        private void PlaceOrder()
        {
            int tt = 100;
        }

        private void IndicatorList()
        {
            int tt = 100;
        }
        private void Perodicity(int inteval)
        {
            int tt = 100;
        }

        private void TrendLine()
        {
            ApplyLineStudy("TrendLine");
        }

        private void Horizontal()
        {
            ApplyLineStudy("HorizontalLine");
        }

        private void Vertical()
        {
            ApplyLineStudy("VerticalLine");
        }

        private MenuItem AddNewIndicatorMenu(MenuItem mnuItem_ParentIndicator, String indicator)
        {
            MenuItem mnuItem_Indicator = new MenuItem { Header = indicator };
            mnuItem_Indicator.Click += (sender1, e1) => { ApplyIndicatorNew(indicator); };
            mnuItem_ParentIndicator.Items.Add(mnuItem_Indicator);
            return mnuItem_ParentIndicator;
        }

        public event Action<string, int, string> OnPeriodicity;
        public event Action<string> OnOrder;
        public event Action OnProperties;
        private void _stockChartX_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _stockChartX.ContextMenu = new ContextMenu();
            MenuItem menuItem = new MenuItem { Header = "New Order" };
            menuItem.Click += (sender1, e1) =>
            {
                OnOrder(_stockChartX.Symbol);
                //_stockChartX.Update();
            };
            _stockChartX.ContextMenu.Items.Add(menuItem);
            //MenuItem mnuItem_Ind1 = new MenuItem { Header = "-----------------" };            
            //_stockChartX.ContextMenu.Items.Add(mnuItem_Ind1);
            
            MenuItem mnuItem_Indicator = new MenuItem { Header = "IndicatorList" };
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Exponential Moving Average");//// 1,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Time Series Moving Average");//// 2,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Triangular Moving Average");//// 3,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Variable Moving Average");//// 4,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "VIDYA");//// 5,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Welles Wilder Smoothing");//// 6,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Weighted Moving Average");//// 7,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Williams PctR");//// 8,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Williams Accumulation Distribution");//// 9,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Volume Oscillator");//// 10,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Vertical Horizontal Filter");//// 11,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Ultimate Oscillator");//// 12,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "True Range");//// 13,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "TRIX");//// 14,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Rainbow Oscillator");//// 15,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Price Oscillator");//// 16,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Parabolic SAR");//// 17,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Momentum Oscillator");//// 18,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "MACD");//// 19,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Ease Of Movement");//// 20,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Directional Movement System");//// 21,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Detrended Price Oscillator");//// 22,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Chande Momentum Oscillator");//// 23,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Chaikin Volatility");//// 24,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Aroon");//// 25,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Aroon Oscillator");//// 26,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Linear Regression RSquared");//// 27,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Linear Regression Forecast");//// 28,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Linear Regression Slope");//// 29,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Linear Regression Intercept");//// 30,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Price Volume Trend");//// 31,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Performance Index");//// 32,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Commodity Channel Index");//// 33,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Chaikin Money Flow");//// 34,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Weighted Close");//// 35,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Volume ROC");//// 36,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Typical Price");//// 37,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Standard Deviation");//// 38,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Price ROC");//// 39,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Median");//// 40,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "High Minus Low");//// 41,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Bollinger Bands");//// 42,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Fractal Chaos Bands");//// 43,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "High Low Bands");//// 44,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Moving Average Envelope");//// 45,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Swing Index");//// 46,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Accumulative Swing Index");//// 47,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Comparative Relative Strength");//// 48,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Mass Index");//// 49,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Money Flow Index");//// 50,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Negative Volume Index");//// 51,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "OnBalanceVolume");//// 52,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Positive Volume Index");//// 53,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Relative Strength Index");//// 54,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Trade Volume Index");//// 55,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Stochastic Oscillator");//// 56,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Stochastic Momentum Index");//// 57,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Fractal Chaos Oscillator");//// 58,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Prime Number Oscillator");//// 59,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Prime Number Bands");//// 60,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Historical Volatility");//// 61,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "MACD Histogram");//// 62,
            // mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator,"Ichimoku");//// 63,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Elder Ray BullPower");//// 64,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Elder Ray Bear Power");//// 65,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Ehler Fisher Transform");//// 66,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Elder Force Index");//// 67,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Elder Thermometer");//// 68,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Keltner Channel");//// 69,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Stoller Average Range Channels");//// 70,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Market Facilitation Index");//// 71,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Schaff Trend Cycle");//// 72,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Q Stick");//// 73,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Center Of Gravity");//// 74,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Coppock Curve");//// 75,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Chande Forecast Oscillator");//// 76,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Gopalakrishnan Range Index");//// 77,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Intraday Momentum Index");//// 78,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Klinger Volume Oscillator");//// 79,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Pretty Good Oscillator");//// 80,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "RAVI");//// 81,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Random Walk Index");//// 82,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Twiggs Money Flow");//// 83,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Custom Indicator");//// 84,

            _stockChartX.ContextMenu.Items.Add(mnuItem_Indicator);

            MenuItem mnuItem_Periodcity = new MenuItem { Header = "Periodicity" };
            MenuItem mnuItem_Periodcity_1min = new MenuItem { Header = "1 Minute" };
            mnuItem_Periodcity_1min.Click += (sender1, e1) => { OnPeriodicity("Minutely_1", 1, "MINUTE"); };

            MenuItem mnuItem_Periodcity_5min = new MenuItem { Header = "5 Minute" };
            mnuItem_Periodcity_5min.Click += (sender1, e1) => { OnPeriodicity("Minutely_5", 1, "MINUTE"); };

            MenuItem mnuItem_Periodcity_15min = new MenuItem { Header = "15 Minute" };
            mnuItem_Periodcity_15min.Click += (sender1, e1) => { OnPeriodicity("Minutely_15", 1, "MINUTE"); };


            MenuItem mnuItem_Periodcity_30min = new MenuItem { Header = "30 Minute" };
            mnuItem_Periodcity_30min.Click += (sender1, e1) => { OnPeriodicity("Minutely_30", 1, "MINUTE"); };

            MenuItem mnuItem_Periodcity_1Hr = new MenuItem { Header = "1 Hour" };
            mnuItem_Periodcity_1Hr.Click += (sender1, e1) => { OnPeriodicity("Hourly_1", 1, "HOUR"); };

            MenuItem mnuItem_Periodcity_24Hr = new MenuItem { Header = "Daily" };
            mnuItem_Periodcity_24Hr.Click += (sender1, e1) => { OnPeriodicity("Daily", 1, "Day"); };

            MenuItem mnuItem_Periodcity_weekly = new MenuItem { Header = "Weekly" };
            mnuItem_Periodcity_weekly.Click += (sender1, e1) =>
            {
                OnPeriodicity("Weekly", 1, "Week");
            };
            MenuItem mnuItem_Periodcity_monthly = new MenuItem { Header = "Monthly" };
            mnuItem_Periodcity_monthly.Click += (sender1, e1) => { OnPeriodicity("Monthly", 1, "Month"); };

            mnuItem_Periodcity.Items.Add(mnuItem_Periodcity_1min);
            mnuItem_Periodcity.Items.Add(mnuItem_Periodcity_5min);
            mnuItem_Periodcity.Items.Add(mnuItem_Periodcity_15min);
            mnuItem_Periodcity.Items.Add(mnuItem_Periodcity_30min);
            mnuItem_Periodcity.Items.Add(mnuItem_Periodcity_1Hr);
            mnuItem_Periodcity.Items.Add(mnuItem_Periodcity_24Hr);
            mnuItem_Periodcity.Items.Add(mnuItem_Periodcity_weekly);
            mnuItem_Periodcity.Items.Add(mnuItem_Periodcity_monthly);

            _stockChartX.ContextMenu.Items.Add(mnuItem_Periodcity);


            //MenuItem mnuItem_Ind2 = new MenuItem { Header = "--------------------------" };
            //_stockChartX.ContextMenu.Items.Add(mnuItem_Ind2);

            MenuItem mnuItem_Trend = new MenuItem { Header = "Trend Line" };
            mnuItem_Trend.Click += (sender1, e1) =>
            {
                TrendLine();
            };
            _stockChartX.ContextMenu.Items.Add(mnuItem_Trend);

            MenuItem mnuItem_Horizontal = new MenuItem { Header = "Horizontal Line" };
            mnuItem_Horizontal.Click += (sender1, e1) =>
            {
                Horizontal();
            };
            _stockChartX.ContextMenu.Items.Add(mnuItem_Horizontal);

            MenuItem mnuItem_Vertical = new MenuItem { Header = "Vertical Line" };
            mnuItem_Vertical.Click += (sender1, e1) =>
            {
                Vertical();
            };
            _stockChartX.ContextMenu.Items.Add(mnuItem_Vertical);

            MenuItem mnuItem_Zoom = new MenuItem { Header = "Zoom In" };

            mnuItem_Zoom.Click += (sender1, e1) => { ZoomChart("zoomin"); };
            _stockChartX.ContextMenu.Items.Add(mnuItem_Zoom);

            MenuItem mnuItem_ZoomOut = new MenuItem { Header = "Zoom Out" };
            mnuItem_ZoomOut.Click += (sender1, e1) => { ZoomChart("zoomout"); };
            _stockChartX.ContextMenu.Items.Add(mnuItem_ZoomOut);

            MenuItem mnuItem_Properties = new MenuItem { Header = "Properties" };
            mnuItem_Properties.Click += (sender1, e1) => { OnProperties(); };
            _stockChartX.ContextMenu.Items.Add(mnuItem_Properties);
        }

        private int _mouseWheelSteps = 50;
        public int mouseWheelSteps
        {
            get { return _mouseWheelSteps; }
            set { _mouseWheelSteps = value; }
        }
        ////private void _stockChartX_MouseWheel(object sender, MouseWheelEventArgs e)
        ////{
        ////    if (e.Delta > 0)
        ////    {
        ////        _stockChartX.ScrollRight(mouseWheelSteps);
        ////    }
        ////    else
        ////    {
        ////        _stockChartX.ScrollLeft(mouseWheelSteps);
        ////    }
        ////}

        public void ResetAndFillCHartAgain()
        {
            _stockChartX.Freeze();
            _stockChartX.ClearAll();

            ChartPanel topPanel = _stockChartX.AddChartPanel();
            //ChartPanel volumePanel = _stockChartX.AddChartPanel(ChartPanel.PositionType.AlwaysBottom);volume
            OhlcCHart = topPanel;
            OhlcCHart.CloseBox = false;
            // VolCHart = volumePanel;volume
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString("#FFF1FF");

            topPanel.Background = brush;
            //volumePanel.Background = Brushes.Black;volume


            Series[] ohlcSeries = _stockChartX.AddOHLCSeries(_stockChartX.Symbol, topPanel.Index);
            //Series seriesVolume = _stockChartX.AddVolumeSeries(_stockChartX.Symbol, volumePanel.Index);volume

            //ohlcSeries[0].StrokeColor = Colors.Red;
            //ohlcSeries[1].StrokeColor = Colors.Aqua;
            //ohlcSeries[2].StrokeColor = Colors.White;
            //ohlcSeries[3].StrokeColor = Colors.Lime;
            //ohlcSeries[3].UpColor = Colors.Lime;
            //ohlcSeries[3].DownColor = Colors.Yellow;
            ohlcSeries[3].TickBox = TickBoxPosition.Right;

            _stockChartX.CandleDownOutlineColor = Colors.Red;
            _stockChartX.CandleUpOutlineColor = Colors.Blue;
            _stockChartX.UpColor = Colors.Blue;
            _stockChartX.DownColor = Colors.Red;
            _stockChartX.YGridStepType = YGridStepType.NiceStep;
            SetColor("Grid", System.Drawing.Color.FromArgb(153, 204, 255));
            _stockChartX.YGrid = true;
            _stockChartX.XGrid = true;
            _stockChartX.ShowAnimations = true;
            _stockChartX.Opacity = 50;
            _stockChartX.InfoPanelValuesBackground = Brushes.Black;
            _stockChartX.InfoPanelValuesForeground = Brushes.White;
            _stockChartX.InfoPanelLabelsBackground = Brushes.Black;
            _stockChartX.InfoPanelLabelsForeground = Brushes.White;
            _stockChartX.RightChartSpace = 5;
            _stockChartX.SetPanelHeight(0, _stockChartX.ActualHeight * 0.75);
            _stockChartX.UseVolumeUpDownColors = true;
            _stockChartX.Melt();
            _stockChartX.MaxVisibleRecords = 150;
            _stockChartX.RealTimeXLabels = true;
            _stockChartX.ScrollLeft(50);
            _stockChartX.ScrollRight(50);
            _stockChartX.OptimizePainting = true;
            _stockChartX.ScalingType = ScalingTypeEnum.Linear;
            _stockChartX[0].YAxisScalePrecision = 5;




            //_stockChartX[SeriesTypeOHLC.Close].TickBox = TickBoxPosition.Right;
            //seriesVolume.StrokeColor = Colors.Green;volume
            //seriesVolume.StrokeThickness = 1;volume
            // If dividing volume by millions as we have above,
            // you should add an "M" to the volume panel like this:
            //StockChartX1.VolumePostfix = "M"; // M for "millions"
            // You could also divide by 1000 and show "K" for "thousands".
            //_stockChartX.VolumePostfixLetter = "M";
            //_stockChartX.VolumeDivisor = 1000000;           
            //      _stockChartX.IsChartScrollerVisible = false;            
            //_stockChartX.GridStroke = new SolidColorBrush(Color.FromArgb(0x33, 0xCC, 0xCC, 0xCC));           
            //_stockChartX.ThreeDStyle = false;
            //_stockChartX.GridStroke = new SolidColorBrush(Color.FromArgb(255, 153, 204, 255));
            //_stockChartX[1].YAxisScalePrecision = 2;volume
            //_stockChartX.ScalingType = ScalingTypeEnum.Linear;

            _stockChartX.Update();
            CreateHistoricalCHart(dtData);

        }
        public void CreateContextMenu()
        {
            // if (_stockChartX.ContextMenu == null)
            _stockChartX.ContextMenu = null;
            _stockChartX.ContextMenu = new ContextMenu();
            MenuItem menuItemOrder = new MenuItem { Header = "New Order" };
            menuItemOrder.Click += (sender1, e1) =>
            {
                if (OnOrder != null)
                    OnOrder(_stockChartX.Symbol);
            };
            _stockChartX.ContextMenu.Items.Add(menuItemOrder);


            MenuItem mnuItem_Indicator = new MenuItem { Header = "IndicatorList" };
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Exponential Moving Average");//// 1,
            //  _stockChartX.ContextMenu.Items.Add(mnuItem_Indicator);

            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Time Series Moving Average");//// 2,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Triangular Moving Average");//// 3,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Variable Moving Average");//// 4,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "VIDYA");//// 5,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Welles Wilder Smoothing");//// 6,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Weighted Moving Average");//// 7,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Williams PctR");//// 8,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Williams Accumulation Distribution");//// 9,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Volume Oscillator");//// 10,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Vertical Horizontal Filter");//// 11,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Ultimate Oscillator");//// 12,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "True Range");//// 13,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "TRIX");//// 14,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Rainbow Oscillator");//// 15,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Price Oscillator");//// 16,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Parabolic SAR");//// 17,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Momentum Oscillator");//// 18,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "MACD");//// 19,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Ease Of Movement");//// 20,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Directional Movement System");//// 21,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Detrended Price Oscillator");//// 22,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Chande Momentum Oscillator");//// 23,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Chaikin Volatility");//// 24,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Aroon");//// 25,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Aroon Oscillator");//// 26,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Linear Regression RSquared");//// 27,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Linear Regression Forecast");//// 28,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Linear Regression Slope");//// 29,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Linear Regression Intercept");//// 30,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Price Volume Trend");//// 31,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Performance Index");//// 32,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Commodity Channel Index");//// 33,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Chaikin Money Flow");//// 34,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Weighted Close");//// 35,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Volume ROC");//// 36,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Typical Price");//// 37,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Standard Deviation");//// 38,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Price ROC");//// 39,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Median");//// 40,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "High Minus Low");//// 41,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Bollinger Bands");//// 42,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Fractal Chaos Bands");//// 43,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "High Low Bands");//// 44,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Moving Average Envelope");//// 45,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Swing Index");//// 46,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Accumulative Swing Index");//// 47,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Comparative Relative Strength");//// 48,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Mass Index");//// 49,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Money Flow Index");//// 50,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Negative Volume Index");//// 51,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "OnBalanceVolume");//// 52,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Positive Volume Index");//// 53,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Relative Strength Index");//// 54,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Trade Volume Index");//// 55,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Stochastic Oscillator");//// 56,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Stochastic Momentum Index");//// 57,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Fractal Chaos Oscillator");//// 58,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Prime Number Oscillator");//// 59,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Prime Number Bands");//// 60,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Historical Volatility");//// 61,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "MACD Histogram");//// 62,
            // mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator,"Ichimoku");//// 63,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Elder Ray BullPower");//// 64,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Elder Ray Bear Power");//// 65,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Ehler Fisher Transform");//// 66,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Elder Force Index");//// 67,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Elder Thermometer");//// 68,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Keltner Channel");//// 69,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Stoller Average Range Channels");//// 70,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Market Facilitation Index");//// 71,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Schaff Trend Cycle");//// 72,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Q Stick");//// 73,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Center Of Gravity");//// 74,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Coppock Curve");//// 75,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Chande Forecast Oscillator");//// 76,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Gopalakrishnan Range Index");//// 77,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Intraday Momentum Index");//// 78,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Klinger Volume Oscillator");//// 79,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Pretty Good Oscillator");//// 80,
            mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "RAVI");//// 81,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Random Walk Index");//// 82,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Twiggs Money Flow");//// 83,
            //mnuItem_Indicator = AddNewIndicatorMenu(mnuItem_Indicator, "Custom Indicator");//// 84,

            _stockChartX.ContextMenu.Items.Add(mnuItem_Indicator);

            MenuItem mnuItem_Periodcity = new MenuItem { Header = "Periodicity" };
            MenuItem mnuItem_Periodcity_1min = new MenuItem { Header = "1 Minute" };
            mnuItem_Periodcity_1min.Click += (sender1, e1) => { OnPeriodicity("Minutely_1", 1, "MINUTE"); };//{ Perodicity(1); };

            MenuItem mnuItem_Periodcity_5min = new MenuItem { Header = "5 Minute" };
            mnuItem_Periodcity_5min.Click += (sender1, e1) => { OnPeriodicity("Minutely_5", 5, "MINUTE"); };//{ Perodicity(5); };

            MenuItem mnuItem_Periodcity_15min = new MenuItem { Header = "15 Minute" };
            mnuItem_Periodcity_15min.Click += (sender1, e1) => { OnPeriodicity("Minutely_15", 15, "MINUTE"); };


            MenuItem mnuItem_Periodcity_30min = new MenuItem { Header = "30 Minute" };
            mnuItem_Periodcity_30min.Click += (sender1, e1) => { OnPeriodicity("Minutely_30", 30, "MINUTE"); };//Hourly_1

            MenuItem mnuItem_Periodcity_1Hr = new MenuItem { Header = "1 Hour" };
            mnuItem_Periodcity_1Hr.Click += (sender1, e1) => { OnPeriodicity("Hourly_1", 1, "HOUR"); };

            MenuItem mnuItem_Periodcity_4Hr = new MenuItem { Header = "4 Hour" };
            mnuItem_Periodcity_4Hr.Click += (sender1, e1) => { };//{ Perodicity(240); };

            MenuItem mnuItem_Periodcity_24Hr = new MenuItem { Header = "Daily" };
            mnuItem_Periodcity_24Hr.Click += (sender1, e1) => { };//{ OnPeriodicity("Daily", 1, "DAY"); };

            MenuItem mnuItem_Periodcity_weekly = new MenuItem { Header = "Weekly" };
            mnuItem_Periodcity_weekly.Click += (sender1, e1) => { };//{ OnPeriodicity("Weekly", 1, "WEEK"); };
            MenuItem mnuItem_Periodcity_monthly = new MenuItem { Header = "Monthly" };
            mnuItem_Periodcity_monthly.Click += (sender1, e1) => { };//{ OnPeriodicity("Monthly", 1, "MONTH"); };

            mnuItem_Periodcity.Items.Add(mnuItem_Periodcity_1min);
            mnuItem_Periodcity.Items.Add(mnuItem_Periodcity_5min);
            mnuItem_Periodcity.Items.Add(mnuItem_Periodcity_15min);
            mnuItem_Periodcity.Items.Add(mnuItem_Periodcity_30min);
            mnuItem_Periodcity.Items.Add(mnuItem_Periodcity_1Hr);
            mnuItem_Periodcity.Items.Add(mnuItem_Periodcity_24Hr);
            mnuItem_Periodcity.Items.Add(mnuItem_Periodcity_weekly);
            mnuItem_Periodcity.Items.Add(mnuItem_Periodcity_monthly);

            _stockChartX.ContextMenu.Items.Add(mnuItem_Periodcity);


            MenuItem mnuItem_Trend = new MenuItem { Header = "Trend Line" };
            mnuItem_Trend.Click += (sender1, e1) =>
            {
                TrendLine();
            };
            _stockChartX.ContextMenu.Items.Add(mnuItem_Trend);

            MenuItem mnuItem_Horizontal = new MenuItem { Header = "Horizontal Line" };
            mnuItem_Horizontal.Click += (sender1, e1) =>
            {
                Horizontal();
            };
            _stockChartX.ContextMenu.Items.Add(mnuItem_Horizontal);

            MenuItem mnuItem_Vertical = new MenuItem { Header = "Vertical Line" };
            mnuItem_Vertical.Click += (sender1, e1) =>
            {
                Vertical();
            };
            _stockChartX.ContextMenu.Items.Add(mnuItem_Vertical);

            MenuItem mnuItem_Zoom = new MenuItem { Header = "Zoom In" };
            mnuItem_Zoom.Click += (sender1, e1) => { ZoomChart("zoomin"); };
            _stockChartX.ContextMenu.Items.Add(mnuItem_Zoom);

            MenuItem mnuItem_ZoomOut = new MenuItem { Header = "Zoom Out" };
            mnuItem_ZoomOut.Click += (sender1, e1) => { ZoomChart("zoomout"); };
            _stockChartX.ContextMenu.Items.Add(mnuItem_ZoomOut);

            MenuItem mnuItem_Properties = new MenuItem { Header = "Properties" };
            mnuItem_Properties.Click += (sender1, e1) => { OnProperties(); };
            _stockChartX.ContextMenu.Items.Add(mnuItem_Properties);
        }
        int i = 0;

        //private int _mouseWheelSteps = 50;
        //public int mouseWheelSteps
        //{
        //    get { return _mouseWheelSteps; }
        //    set { _mouseWheelSteps = value; }
        //}

        private void _stockChartX_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (_stockChartX.LineStudySelectedCount > 0)
                {
                    foreach (LineStudy ls in _stockChartX.LineStudiesCollection)
                    {
                        if (ls.Selected)
                        {
                            _stockChartX.RemoveObject(ls);
                            break;
                        }
                    }

                }
            }
        }

        private void _stockChartX_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                _stockChartX.ScrollRight(mouseWheelSteps);
            }
            else
            {
                _stockChartX.ScrollLeft(mouseWheelSteps);
            }
        }

        private void CreateHistoricalCHart(DataTable dataTable)
        {
            try
            {
                String symbol = "Chart";
                for (int i = 0; i < dataTable.Rows.Count - 1; i++)
                {
                    //symbol = Convert.ToString(dataTable.Rows[i][0]);
                    DateTime dt = Convert.ToDateTime(dataTable.Rows[i][5]);
                    //DateTime dtnew = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0);
                    //if (!minuteData.ContainsKey(dtnew))
                    {
                        OHLC obj = new OHLC();
                        obj.Open = Convert.ToDouble(dataTable.Rows[i][0]);
                        obj.High = Convert.ToDouble(dataTable.Rows[i][1]);
                        obj.Low = Convert.ToDouble(dataTable.Rows[i][2]);
                        obj.CLose = Convert.ToDouble(dataTable.Rows[i][3]);
                        obj.volume = Convert.ToInt64(dataTable.Rows[i][4]);
                        //minuteData.Add(dtnew, obj);
                        CreateNewOHLC(dt, obj.Open, obj.High, obj.Low, obj.CLose, obj.volume);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private Hashtable ht_RealTimeData = new System.Collections.Hashtable();
        public void UpdateChart(string sym, DateTime dtNow, double price)
        {

            {

                bool isData = false;

                DateTime dt = System.DateTime.UtcNow;
                DateTime dtnew = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0);

                OHLC ohlc = new OHLC();
                if (ht_RealTimeData.Contains(sym))
                {
                    Hashtable ht_Feed_OHCL = (Hashtable)ht_RealTimeData[sym];
                    if (ht_Feed_OHCL.Contains(dtnew))
                    {
                        ohlc = (OHLC)ht_Feed_OHCL[dtnew];

                        if (price > ohlc.High)
                            ohlc.High = price;

                        if (price < ohlc.CLose)
                            ohlc.CLose = price;

                        ohlc.CLose = price;
                        ohlc.volume = 0;
                        ht_Feed_OHCL[dtnew] = ohlc;
                    }
                    else
                    {
                        ht_Feed_OHCL.Clear();
                        ohlc.Open = price;
                        ohlc.CLose = price;
                        ohlc.Low = price;
                        ohlc.High = price;
                        ohlc.volume = 0;
                        isData = true;
                        ht_Feed_OHCL.Add(dtnew, ohlc);
                    }
                    ht_RealTimeData[sym] = ht_Feed_OHCL;
                }
                else
                {
                    ohlc.Open = price;
                    ohlc.CLose = price;
                    ohlc.Low = price;
                    ohlc.High = price;
                    ohlc.volume = 0;
                    Hashtable ht_Feed_OHCL = new Hashtable();
                    ht_Feed_OHCL.Add(dtnew, ohlc);
                    ht_RealTimeData.Add(sym, ht_Feed_OHCL);
                }

                if (ohlc.CLose > 0 && ohlc.High > 0 && ohlc.Low > 0 && ohlc.Open > 0)
                {
                    CreateNewOHLC(dtnew, ohlc.Open, ohlc.High, ohlc.Low, ohlc.CLose,0);
                }
            }
        }


        public void CreateNewOHLC(DateTime dtOHLC, double open, double high, double low, double close, long volume)
        {
            try
            {
                BarData bd = new BarData
                {
                    TradeDate = dtOHLC,
                    OpenPrice = open,
                    HighPrice = high,
                    LowPrice = low,
                    ClosePrice = close,
                };

                //var item = _stockChartX.SeriesCollection;
               //_stockChartX.AppendValue(_stockChartX.Symbol, bd.TradeDate, bd.OpenPrice, bd.HighPrice, bd.LowPrice, bd.ClosePrice);
                _stockChartX.AppendOHLCValues(_stockChartX.Symbol, bd.TradeDate, bd.OpenPrice, bd.HighPrice, bd.LowPrice, bd.ClosePrice);
                // _stockChartX.AppendVolumeValue(_stockChartX.Symbol, bd.TradeDate, bd.Volume);
                _stockChartX.Update();

            }
            catch (Exception ex)
            {

            }
        }


        #region Bar Style

        private string barstyle;
        public string setBarStyle
        {
            set
            {
                barstyle = value;
                BarStyle(barstyle);
            }
        }

        private void BarStyle(string barstyle)
        {
            SeriesTypeEnum stType;
            switch (barstyle.ToLower().Trim())
            {
                case "linear":
                    {
                        stType = SeriesTypeEnum.stLineChart;
                        UpdateBarStyle(stType);
                    }
                    break;
                case "standard":
                    {
                        stType = SeriesTypeEnum.stStockBarChart;
                        UpdateBarStyle(stType);
                    }
                    break;
                case "candlestick":
                    {
                        stType = SeriesTypeEnum.stCandleChart;
                        UpdateBarStyle(stType);
                    }
                    break;
            }

        }

        private void UpdateBarStyle(SeriesTypeEnum stType)
        {
            Series seriesOpen = _stockChartX.GetSeriesByName(_stockChartX.Symbol + ".open");
            Series seriesHigh = _stockChartX.GetSeriesByName(_stockChartX.Symbol + ".high");
            Series seriesLow = _stockChartX.GetSeriesByName(_stockChartX.Symbol + ".low");
            Series seriesClose = _stockChartX.GetSeriesByName(_stockChartX.Symbol + ".close");

            seriesOpen.SeriesType = stType;
            seriesHigh.SeriesType = stType;
            seriesLow.SeriesType = stType;
            seriesClose.SeriesType = stType;
            _stockChartX.Update();
        }

        #endregion

        #region Price Styles

        private string Pricestyle;
        public string setPriceStyle
        {
            set
            {
                Pricestyle = value;
                LoadPriceStyle(Pricestyle);
            }
        }

        private string Linestyle;
        public string setLineStyle
        {
            set
            {
                Linestyle = value;
                ApplyLineStudy(Linestyle);
            }
        }

        private string Indiacatorstyle;
        public string setIndicator
        {
            set
            {
                Indiacatorstyle = value;
                ApplyIndicatorNew(Indiacatorstyle);
            }
        }

        public void Grid(bool p)
        {
            _stockChartX.XGrid = p;
            _stockChartX.YGrid = p;
            _stockChartX.Update();
        }
        public void Volume(bool flag)
        {
        }

        public void SetColor(string prop, System.Drawing.Color color)
        {

            var sld = new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B));
            var brush = (Brush)sld;//converter.ConvertFrom(color.);
            switch (prop)
            {
                case "backGround":
                    OhlcCHart.Background = brush;
                    break;
                case "foreGround":
                    OhlcCHart.Foreground = brush;
                    break;
                case "barDown":
                    _stockChartX.DownColor = sld.Color;
                    break;
                case "barUp":
                    _stockChartX.UpColor = sld.Color;
                    break;
                case "UpOutLine":
                    _stockChartX.CandleUpOutlineColor = sld.Color;
                    break;
                case "DownOutLine":
                    _stockChartX.CandleDownOutlineColor = sld.Color;
                    break;
                case "Crosshair":
                    _stockChartX.CrossHairsStroke = brush;
                    break;
                case "Grid":
                    _stockChartX.GridStroke = brush;
                    break;
                default:
                    break;
            }
            _stockChartX.Update();
        }

        public System.Drawing.Color GetChartColor(string prop)
        {
            switch (prop)
            {
                case "backGround":
                    return GetColor(((SolidColorBrush)OhlcCHart.Background));
                case "foreGround":
                    return GetColor(((SolidColorBrush)OhlcCHart.Foreground));
                case "barUp":
                    return System.Drawing.Color.FromArgb(_stockChartX.UpColor.A, _stockChartX.UpColor.R, _stockChartX.UpColor.G, _stockChartX.UpColor.B);
                case "barDown":
                    return System.Drawing.Color.FromArgb(_stockChartX.DownColor.A, _stockChartX.DownColor.R, _stockChartX.DownColor.G, _stockChartX.DownColor.B);
                case "UpOutLine":
                    return System.Drawing.Color.FromArgb(_stockChartX.CandleUpOutlineColor.Value.A, _stockChartX.CandleUpOutlineColor.Value.R, _stockChartX.CandleUpOutlineColor.Value.G, _stockChartX.CandleUpOutlineColor.Value.B);
                case "DownOutLine":
                    return System.Drawing.Color.FromArgb(_stockChartX.CandleDownOutlineColor.Value.A, _stockChartX.CandleDownOutlineColor.Value.R, _stockChartX.CandleDownOutlineColor.Value.G, _stockChartX.CandleDownOutlineColor.Value.B);
                case "Crosshair":
                    return GetColor(((SolidColorBrush)_stockChartX.CrossHairsStroke));
                case "Grid":
                    return GetColor(((SolidColorBrush)_stockChartX.GridStroke));
            }
            return System.Drawing.Color.White;
        }

        private System.Drawing.Color GetColor(SolidColorBrush mediaColor)
        {
            System.Drawing.Color myColor = System.Drawing.Color.FromArgb(mediaColor.Color.A,
                                                              mediaColor.Color.R,
                                                              mediaColor.Color.G,
                                                              mediaColor.Color.B);
            return myColor;
        }

        public string GetChartType()
        {
            Series srs= _stockChartX.GetSeriesByName(_stockChartX.Symbol + ".open");
            switch (srs.SeriesType)
            {
                case SeriesTypeEnum.stLineChart:
                    return "LineChart";
                case SeriesTypeEnum.stStockBarChart:
                    return "BarChart";
            }
            return "CandleChart";
        }

        public bool GetBoolProperty(string prop)
        {
            switch (prop)
            {
                //case "foreGround":
                //    return GetColor(((SolidColorBrush)_stockChartX.Background));
                //    break;
                case "chartAutoScroll":
                    return true;
                case "showOHLC":
                    return true;
                case "periodSeperator":
                    return true;
                case "showGrid":
                    return _stockChartX.XGrid;
                case "showVolume":
                    return false;
                case "ThreeDStyle":
                    return _stockChartX.ThreeDStyle;
                //default:
                //    break;
            }
            return true;
        }
       
        private void LoadPriceStyle(string Pricestyle)
        {
            PriceStyleEnum priceStyle = PriceStyleEnum.psStandard;

            switch (Pricestyle.Trim())
            {
                case "standard": priceStyle = PriceStyleEnum.psStandard; break;
                case "kagi":
                    priceStyle = PriceStyleEnum.psKagi;
                    _stockChartX.SetPriceStyleParam(0, 0); //Reversal size
                    _stockChartX.SetPriceStyleParam(1, (double)ChartDataType.Points);
                    break;
                case "equivolume": priceStyle = PriceStyleEnum.psEquiVolume; break;
                case "candlevolume": priceStyle = PriceStyleEnum.psCandleVolume; break;
                case "equivolumeshadow": priceStyle = PriceStyleEnum.psEquiVolumeShadow; break;
                case "pointandfigure":
                    priceStyle = PriceStyleEnum.psPointAndFigure;
                    _stockChartX.SetPriceStyleParam(0, 0); //Allow StockChartX to figure the box size
                    _stockChartX.SetPriceStyleParam(1, 3); //Reversal size
                    break;
                case "renko":
                    priceStyle = PriceStyleEnum.psRenko;
                    _stockChartX.SetPriceStyleParam(0, 1); //Box size
                    break;
                case "threelinebreak":
                    priceStyle = PriceStyleEnum.psThreeLineBreak;
                    _stockChartX.SetPriceStyleParam(0, 3); //Three line break (could be 1 to 50 line break)
                    break;
                case "heikinashi": priceStyle = PriceStyleEnum.psHeikinAshi; break;
            }

            _stockChartX.PriceStyle = priceStyle;
            _stockChartX.Update();
        }

        #endregion Top Menu - Price Styles

        public void ZoomChart(string Zoom)
        {
            switch (Zoom.Trim())
            {
                case "zoomin":
                    _stockChartX.ZoomIn(2);
                    break;
                case "zoomout":
                    _stockChartX.ZoomOut(2);
                    break;
                case "resetzoom":
                    _stockChartX.ResetZoom();
                    break;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //ResetAndFillCHartAgain();
        }

        public void ClearSeries()
        {
            _stockChartX.Freeze();
            _stockChartX.RemoveSeries(_stockChartX.Symbol + ".open");
            _stockChartX.RemoveSeries(_stockChartX.Symbol + ".high");
            _stockChartX.RemoveSeries(_stockChartX.Symbol + ".low");
            _stockChartX.RemoveSeries(_stockChartX.Symbol + ".close");
            Series[] ohlcSeries = _stockChartX.AddOHLCSeries(_stockChartX.Symbol, OhlcCHart.Index);

            _stockChartX.CandleDownOutlineColor = Colors.Lime;
            _stockChartX.CandleUpOutlineColor = Colors.Lime;
            _stockChartX.UpColor = Colors.LightGreen;
            _stockChartX.DownColor = Colors.LightGreen;
            _stockChartX.ThreeDStyle = true;
            _stockChartX.XGrid = true;
            _stockChartX.YGridStepType = YGridStepType.NiceStep;
            _stockChartX.ShowAnimations = true;
            _stockChartX.Opacity = 50;
            _stockChartX.InfoPanelValuesBackground = Brushes.Black;
            _stockChartX.InfoPanelValuesForeground = Brushes.White;
            _stockChartX.InfoPanelLabelsBackground = Brushes.Black;
            _stockChartX.InfoPanelLabelsForeground = Brushes.White;


            _stockChartX.RightChartSpace = 5;
            _stockChartX.RealTimeXLabels = false;

            _stockChartX.SetPanelHeight(0, _stockChartX.ActualHeight * 0.75);

            _stockChartX.UseVolumeUpDownColors = true;
            _stockChartX.GridStroke = new SolidColorBrush(Color.FromArgb(0x33, 0xCC, 0xCC, 0xCC));
            _stockChartX.Melt();

            _stockChartX.MaxVisibleRecords = 150;
            _stockChartX.RealTimeXLabels = true;
            _stockChartX.ScrollLeft(50);
            _stockChartX.ScrollRight(50);
            _stockChartX.OptimizePainting = true;

            _stockChartX.LineStudyPropertyDialogBackground = Brushes.Black;
            _stockChartX.UseVolumeUpDownColors = true;
            _stockChartX.YGridStepType = YGridStepType.NiceStep;
            _stockChartX.ScalePrecision = 5;
            _stockChartX.XGrid = true;
            _stockChartX.YGrid = true;
            _stockChartX.CrossHairsStroke = new SolidColorBrush(Colors.Red);
            _stockChartX.Update();
            CreateHistoricalCHart(dtData);
        }

        public void VisibleGrid()
        {
            //bool temp = !_stockChartX.XGrid;
            //_stockChartX.XGrid = !_stockChartX.XGrid;
            //_stockChartX.YGrid = !_stockChartX.YGrid;
            //_stockChartX.Update();
            _stockChartX.XGrid = true;
            _stockChartX.YGrid = true;
            _stockChartX.Update();
        }
        public void HideGrid()
        {
            _stockChartX.XGrid = false;
            _stockChartX.YGrid = false;
            _stockChartX.Update();
        }
        public void SaveChart(string file)
        {
            _stockChartX.SaveFile(file);//, StockChartX.SerializationTypeEnum.Objects);            
        }

        public void PrintChart(string file)
        {
            _stockChartX.SaveAsImage(file);
        }

        public void Chart3dStyle(bool p)
        {
            _stockChartX.ThreeDStyle = !_stockChartX.ThreeDStyle;
            _stockChartX.Update();
        }

        private void _stockChartX_ChartLoaded(object sender, EventArgs e)
        {

        }
    }
    public class OHLC
    {
        public DateTime FeedTime;
        public double Open;
        public double High;
        public double Low;
        public double CLose;
        public long volume;
    }
}
