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
        public ManualOrderConfirmationWindow()
        {
            InitializeComponent();
        }

        // 0 - buy, 1 - sell
        public DialogResult ShowDialog(int direction)
        {
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
            var dialogResult = ShowDialog();
            return dialogResult;
        }

        private void rbSystemDefault_CheckedChanged(object sender, EventArgs e)
        {
            tbProfitTarget.Enabled = !rbSystemDefault.Checked;
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
