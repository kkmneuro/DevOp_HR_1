using NeuroXChange.Model;
using NeuroXChange.Model.FixApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuroXChange.View.DialogWindows
{
    public partial class ManualOrderConfirmationWindow : Form
    {
        private MainNeuroXModel model;
        private int stopLossPips;
        private int takeProfitPips;
        private double pipSize;
        private int currentDirection;
        private TickPrice lastPrice;

        public ManualOrderConfirmationWindow(MainNeuroXModel model)
        {
            InitializeComponent();

            this.model = model;

            stopLossPips = Int32.Parse(model.iniFileReader.Read("StopLossPips", "MarketOrders", "200"));
            takeProfitPips = Int32.Parse(model.iniFileReader.Read("TakeProfitPips", "MarketOrders", "200"));
            pipSize = double.Parse(model.iniFileReader.Read("PipSize", "MarketOrders", "0.00001"));

            currentDirection = 0;
        }

        // 0 - buy, 1 - sell
        public DialogResult ShowDialog(int direction)
        {
            currentDirection = direction;

            btnConfirm.Focus();
            if (direction == 0)
            {
                tbOrderDirection.Text = "BUY";
                tbOrderDirection.BackColor = Color.RoyalBlue;
            }
            else
            {
                tbOrderDirection.Text = "SELL";
                tbOrderDirection.BackColor = Color.Red;
            }
            tbOrderDirection.Select(0, 0);
            UpdateStopLossTakeProfit(lastPrice, true);
            var dialogResult = ShowDialog();

            if (dialogResult == DialogResult.OK)
            {

            }

            return dialogResult;
        }

        public void UpdateStopLossTakeProfit(TickPrice price, bool forceUpdate = false)
        {
            lastPrice = price;

            if (!forceUpdate && !Visible)
            {
                return;
            }

            if (rbPTSystemDefault.Checked)
            {
                double takeProfit;
                if (currentDirection == 0)
                {
                    takeProfit = price.buy + takeProfitPips * pipSize;
                }
                else
                {
                    takeProfit = price.sell - takeProfitPips * pipSize;
                }
                tbProfitTarget.Text = takeProfit.ToString();
            }

            if (rbSLSystemDefault.Checked)
            {
                double stopLoss;
                if (currentDirection == 0)
                {
                    stopLoss = price.buy - stopLossPips * pipSize;
                }
                else
                {
                    stopLoss = price.sell + stopLossPips * pipSize;
                }
                tbStopLoss.Text = stopLoss.ToString();
            }
        }

        private void rbSystemDefault_CheckedChanged(object sender, EventArgs e)
        {
            tbProfitTarget.Enabled = !rbPTSystemDefault.Checked;
            UpdateStopLossTakeProfit(lastPrice);
        }

        private void tbProfitTarget_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void tbProfitTarget_TextChanged(object sender, EventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(tbProfitTarget.Text, "^[0-9.]+$"))
            {
                tbProfitTarget.Text = "";
            }
        }
    }
}
