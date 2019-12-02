using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

using Common;
using MarketData;
using ChartLines.Lines;

namespace SelectionControl
{

    public partial class InterActionWithMarket : UserControl
    {

        private LinesManager _lm;
        private PriceChartControl _priceChart;

        public PriceChartControl PriceChart { get { return _priceChart; } set { _priceChart = value; } }

        private TimeFrame.TF _timeFrame;

        // ui elements begin

        //step 1
        //private System.Windows.Forms.ComboBox comboBox3FutureStep1;
        private SelectionControl.FuturesSelection futuresSelection1;

        //step 2
     //   private System.Windows.Forms.DataVisualization.Charting.Chart _priceChart;
       

        //step 3
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button button1ComandStep3;
        private System.Windows.Forms.Button button2ComandStep3;
        private System.Windows.Forms.Button button3ComandStep3;
        private System.Windows.Forms.Button button4ComandStep3;
        private System.Windows.Forms.Button button5ComandStep3;
        private System.Windows.Forms.Button button6ComandStep3;

        // ui elements end

        private IMarketData _md;

        public Color InfoTextColor
        {
            get { return label1Info.ForeColor; }
            set { label1Info.ForeColor = value; }
        }

        /// <summary>
        /// source of market data: list of securities, price feed, price history charts data
        /// </summary>
        public IMarketData Md
        {
            get { return _md;  }
            set { _md = value; }
        }

        /// <summary>
        ///  Properties set after each step
        /// </summary>
        /// 
        public Future Security { get; private set; }
        public DateTime DataFrom { get; set; }
        public string Command { get; private set; }



        public SelectionSteps CurrentStep { get; private set; }
        

        //  private Future ff;
        //  private TimeFrame.TF tff;


        public InterActionWithMarket()
        {
            InitializeComponent();            
            Security = null;

            _lm = new LinesManager();

        }




        /// <summary>
        /// Initialize content according to step 
        /// </summary>
        /// <param name="ss">Step</param>
        /// <param name="f">Future to display (usd/eur; aud/usd ...)</param>
        /// <param name="tf"> Time frame</param>
        /// <param name="datafrom"> date time data from can be set only on a first run, tu change time use property value</param>
        public void goToStep(SelectionSteps ss, Future f, TimeFrame.TF tf, DateTime datafrom)
        {

            CurrentStep = ss;
      /* !! */      if (Security == null) Security = f;  // if not set let set, if it is set lets keep old selection, if we will want to change selection let's make a method for it.
      /* !! */      if (DataFrom == null) DataFrom = datafrom; // if it is set once lets have it set once;

            switch (ss)
            {
                case SelectionSteps.SELECTION_securities: onlySecurities(); break;
                case SelectionSteps.SELECTION_Live_Data : live_data(Security, tf, datafrom);  break;
                case SelectionSteps.SELECTION_Comands   : commands(); break;
                case SelectionSteps.REVIEW_All_Lines:
                    live_data(Security, tf, datafrom);
                    label1Info.Text = "Review Your Selections";
                    _lm.stopPlacingLines(); // just in case stop placing lines
                    _lm.SetStaticAll(); // show all placed lines
                    break;
                case SelectionSteps.SELECTION_Line_Golden_Upper:
                    drawLines(Security, tf, ss, datafrom);
                    break;
                case SelectionSteps.SELECTION_Line_Golden_Lower: drawLines(Security, tf, ss, datafrom); break;
                case SelectionSteps.SELECTION_Line_Lime: drawLines(Security, tf, ss, datafrom); break;
                case SelectionSteps.SELECTION_2Lines_Aqua: drawLines(Security, tf, ss, datafrom); break;
                case SelectionSteps.SELECTION_4Lines_Blue: drawLines(Security, tf, ss, datafrom); break;
                case SelectionSteps.SELECTION_2Lines_DodgerBlue: drawLines(Security, tf, ss, datafrom); break;
                case SelectionSteps.SELECTION_Multiple_Dashed_Lines_RedWhite:


                    drawLines(Security, tf, ss, datafrom);

                    button2Reset.Visible = true;

                    button2Reset.Click += Button2Reset_Click;

                    

                    break;
                case SelectionSteps.SELECTION_Multiple_Dashed_Lines_White:


                    drawLines(Security, tf, ss, datafrom);

                    button2Reset.Visible = true;
                    button2Reset.Click += Button2Reset_Click;

                    

                    break;
                
            }
        }


        private void drawLines(Future Security, TimeFrame.TF tf, SelectionSteps ss, DateTime datafrom)
        {
            live_data(Security, tf, datafrom);
            label1Info.Text = "Double Click On Chart To Draw Lines";            
            _lm.stopPlacingLines();

            _lm.SetStaticAll();
            _lm.StartPlacingLine(ss);
            _lm.ActivateSpecified(ss);

        }

        /// <summary>
        /// 1st Step
        /// </summary>
        private void onlySecurities()
        {

            this.panel1Content.Controls.Clear();

            label1Info.Text = "Select Your Security";
            button2Reset.Visible = false;
            
            // 
            // futuresSelection1
            // 
            this.futuresSelection1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.futuresSelection1.Location = new System.Drawing.Point(0, 0);
            this.futuresSelection1.Name = "futuresSelection1";
            this.futuresSelection1.Size = new System.Drawing.Size(788, 298);

            futuresSelection1.SelectionChanged += futuresSelection1_SelectionChanged;
            this.panel1Content.Controls.Add(this.futuresSelection1);
            this.futuresSelection1.Focus();
            Security = (Future)this.futuresSelection1.Future; // lest assign default selection

            
        }

        private void futuresSelection1_SelectionChanged(object sender, EventArgs e)
        {
            Security = (Future)this.futuresSelection1.Future; 
        }

        /// <summary>
        /// Live data step 2
        /// </summary>
        private void live_data(Future f, TimeFrame.TF tf, DateTime dt)
        {


            this.panel1Content.Controls.Clear();

            label1Info.Text = "Live Price Chart";
            button2Reset.Visible = false;

            _priceChart = new PriceChartControl();
            _priceChart.TimeFram = tf;  // seting time frame for charting control
            if (_md != null) _md.OnPriceTick += _priceChart.UpdateLastPrice; // lets bound matket data tick with UI chart control
            _lm.AddChart(_priceChart.Chart); // lets bound line placing with chart
            _priceChart.P = Md.getFutureData(f, tf, dt); // get historical data
            _priceChart.PrepareForLiveData(f, tf);

            this.panel1Content.Controls.Add(_priceChart);
            _priceChart.Dock = DockStyle.Fill;
        }

        



        private void commands()
        {


            Command = "";
            // 
            // chart1Step3
            // 
            button2Reset.Visible = false;

            label1Info.Text = "Click To Choose Your Option";

            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.button1ComandStep3 = new System.Windows.Forms.Button();
            this.button2ComandStep3 = new System.Windows.Forms.Button();
            this.button3ComandStep3 = new System.Windows.Forms.Button();
            this.button4ComandStep3 = new System.Windows.Forms.Button();
            this.button5ComandStep3 = new System.Windows.Forms.Button();
            this.button6ComandStep3 = new System.Windows.Forms.Button();


            this.panel1Content.Controls.Clear();
            this.panel1Content.Controls.Add(this.tableLayoutPanel2);

            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel2.Controls.Add(this.button1ComandStep3, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.button2ComandStep3, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.button3ComandStep3, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.button4ComandStep3, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.button5ComandStep3, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.button6ComandStep3, 1, 5);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(528, 203);
            this.tableLayoutPanel2.TabIndex = 4;

            // 
            // button1ComandStep3
            // 
            this.button1ComandStep3.Dock = System.Windows.Forms.DockStyle.Top;
            this.button1ComandStep3.Location = new System.Drawing.Point(178, 3);
            this.button1ComandStep3.Name = "button1ComandStep3";
            this.button1ComandStep3.Size = new System.Drawing.Size(170, 23);
            this.button1ComandStep3.TabIndex = 0;
            this.button1ComandStep3.Text = "Multi_Long-Short_1";
            this.button1ComandStep3.UseVisualStyleBackColor = true;
            // 
            // button2ComandStep3
            // 
            this.button2ComandStep3.Dock = System.Windows.Forms.DockStyle.Top;
            this.button2ComandStep3.Location = new System.Drawing.Point(178, 36);
            this.button2ComandStep3.Name = "button2ComandStep3";
            this.button2ComandStep3.Size = new System.Drawing.Size(170, 23);
            this.button2ComandStep3.TabIndex = 1;
            this.button2ComandStep3.Text = "Multi_Long-Short_2";
            this.button2ComandStep3.UseVisualStyleBackColor = true;
            // 
            // button3ComandStep3
            // 
            this.button3ComandStep3.Dock = System.Windows.Forms.DockStyle.Top;
            this.button3ComandStep3.Location = new System.Drawing.Point(178, 69);
            this.button3ComandStep3.Name = "button3ComandStep3";
            this.button3ComandStep3.Size = new System.Drawing.Size(170, 23);
            this.button3ComandStep3.TabIndex = 2;
            this.button3ComandStep3.Text = "Multi_Short-Long_1";
            this.button3ComandStep3.UseVisualStyleBackColor = true;
            // 
            // button4ComandStep3
            // 
            this.button4ComandStep3.Dock = System.Windows.Forms.DockStyle.Top;
            this.button4ComandStep3.Location = new System.Drawing.Point(178, 102);
            this.button4ComandStep3.Name = "button4ComandStep3";
            this.button4ComandStep3.Size = new System.Drawing.Size(170, 23);
            this.button4ComandStep3.TabIndex = 3;
            this.button4ComandStep3.Text = "Multi_Short-Long_2";
            this.button4ComandStep3.UseVisualStyleBackColor = true;
            // 
            // button5ComandStep3
            // 
            this.button5ComandStep3.Dock = System.Windows.Forms.DockStyle.Top;
            this.button5ComandStep3.Location = new System.Drawing.Point(178, 135);
            this.button5ComandStep3.Name = "button5ComandStep3";
            this.button5ComandStep3.Size = new System.Drawing.Size(170, 23);
            this.button5ComandStep3.TabIndex = 4;
            this.button5ComandStep3.Text = "Singular_Long";
            this.button5ComandStep3.UseVisualStyleBackColor = true;
            // 
            // button6ComandStep3
            // 
            this.button6ComandStep3.Dock = System.Windows.Forms.DockStyle.Top;
            this.button6ComandStep3.Location = new System.Drawing.Point(178, 168);
            this.button6ComandStep3.Name = "button6ComandStep3";
            this.button6ComandStep3.Size = new System.Drawing.Size(170, 23);
            this.button6ComandStep3.TabIndex = 5;
            this.button6ComandStep3.Text = "Singular_Short";
            this.button6ComandStep3.UseVisualStyleBackColor = true;

            this.button1ComandStep3.Click += ButtonComandStep3_Click;
            this.button2ComandStep3.Click += ButtonComandStep3_Click;
            this.button3ComandStep3.Click += ButtonComandStep3_Click;
            this.button4ComandStep3.Click += ButtonComandStep3_Click;
            this.button5ComandStep3.Click += ButtonComandStep3_Click;
            this.button6ComandStep3.Click += ButtonComandStep3_Click;


        }

        private void ButtonComandStep3_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Command = btn.Text;
        }



        /// <summary>
        /// Clean multiple lines
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button2Reset_Click(object sender, EventArgs e)
        {
            _lm.removeSpecified(CurrentStep);
        }





        private void InterActionWithMarket_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// In case if needed to change symbol/security ussually it is set only once
        /// </summary>
        /// <param name="f"></param>
        public void SetSecurity(Future f)
        {
            Security = f;
        }

        new public void Focus()
        {
            if (CurrentStep == SelectionSteps.SELECTION_securities)
            {
                if (futuresSelection1 != null) futuresSelection1.Focus();
            }
        }


    }
}
