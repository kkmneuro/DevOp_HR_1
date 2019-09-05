using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MarketData;
using SelectionControl;
using Common;




namespace FXCharts
{
    public partial class Form1 : Form
    {

        private MarketDataDDF md;

        public Form1()
        {
            InitializeComponent();

            md = new MarketDataDDF();

            //    md.OnPriceTick += interActionWithMarket1.UpdateLastPrice; // it was wover to setter of interActionWithMarket1.Md

            interActionWithMarket1.Md = md;

            BindingSource bs = new BindingSource();

            bs.DataSource = md.getAvailableFutures();

            comboBox1Step.DataSource = Enum.GetValues(typeof(SelectionSteps));
            comboBox2TimeFrame.DisplayMember = "Name";

            comboBox3Future.DataSource = bs; // md.getAvailableFutures();
            comboBox3Future.DisplayMember = "Name";
          //  comboBox3Future.ValueMember = "Name";

            comboBox2TimeFrame.DataSource = Enum.GetValues(typeof(TimeFrame.TF));
            comboBox2TimeFrame.DisplayMember = "Name";
            comboBox2TimeFrame.SelectedItem = TimeFrame.TF.Min;

            dateTimePicker1From.Format = DateTimePickerFormat.Custom;
            dateTimePicker1From.CustomFormat = "MM dd yyyy HH mm ss";
        }


        private void Form1_Load(object sender, EventArgs e)
        {


            dateTimePicker1From.Value = System.DateTime.Now.AddHours(-3);


        }

        private void button3_Click(object sender, EventArgs e)
        {
            SelectionSteps stepToGo = (SelectionSteps)comboBox1Step.SelectedItem;
            TimeFrame.TF tf = (TimeFrame.TF)comboBox2TimeFrame.SelectedItem;
            Future f = (Future)comboBox3Future.SelectedItem;

            interActionWithMarket1.SetSecurity(f);

            switch (stepToGo)
            {
                case SelectionSteps.SELECTION_securities:
                    interActionWithMarket1.goToStep(stepToGo, null, TimeFrame.TF.None, dateTimePicker1From.Value);
                    break;
                case SelectionSteps.SELECTION_Live_Data:
                    interActionWithMarket1.goToStep(stepToGo, f, tf, dateTimePicker1From.Value);
                    break;
                case SelectionSteps.SELECTION_Comands:
                    interActionWithMarket1.goToStep(stepToGo, f, tf, dateTimePicker1From.Value);
                    break;
                case SelectionSteps.SELECTION_Line_Golden_Upper:
                    interActionWithMarket1.goToStep(stepToGo, f, tf, dateTimePicker1From.Value);
                    break;
                case SelectionSteps.SELECTION_Line_Golden_Lower:
                    interActionWithMarket1.goToStep(stepToGo, f, tf, dateTimePicker1From.Value);
                    break;
                case SelectionSteps.SELECTION_Line_Lime:
                    interActionWithMarket1.goToStep(stepToGo, f, tf, dateTimePicker1From.Value);
                    break;
                case SelectionSteps.SELECTION_2Lines_Aqua:
                    interActionWithMarket1.goToStep(stepToGo, f, tf, dateTimePicker1From.Value);
                    break;
                case SelectionSteps.SELECTION_4Lines_Blue:
                    interActionWithMarket1.goToStep(stepToGo, f, tf, dateTimePicker1From.Value);
                    break;
                case SelectionSteps.SELECTION_2Lines_DodgerBlue:
                    interActionWithMarket1.goToStep(stepToGo, f, tf, dateTimePicker1From.Value);
                    break;
                case SelectionSteps.SELECTION_Multiple_Dashed_Lines_RedWhite:
                    interActionWithMarket1.goToStep(stepToGo, f, tf, dateTimePicker1From.Value);
                    break;
                case SelectionSteps.SELECTION_Multiple_Dashed_Lines_White:
                    interActionWithMarket1.goToStep(stepToGo, f, tf, dateTimePicker1From.Value);
                    break;
                case SelectionSteps.REVIEW_All_Lines:
                    interActionWithMarket1.goToStep(stepToGo, f, tf, dateTimePicker1From.Value);
                    break;

                default:
                    

                        
                    break;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int sel = comboBox1Step.SelectedIndex;
            if (comboBox1Step.Items.Count - 1 > sel)
                comboBox1Step.SelectedIndex = sel + 1;
            else comboBox1Step.SelectedIndex = 0;

            button3_Click(sender, e);

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            md.Close_Connection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }

        bool b = true;
        private void button2_Click(object sender, EventArgs e)
        {

            interActionWithMarket1.PriceChart.ShowLegend(b);
            if (b) b = false;
            else b = true;
        }
    }
}
