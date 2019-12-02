using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using TradingAccount;

namespace TEST
{
    public partial class TradingManager : UserControl
    {

        public TPSForNeuroTrader.TPSData LastBioData { get; set; }
        public Helpers.TemperatureLimits TemperatureLimits { get; set; }

        /// <summary>
        /// Currently system is designe to trade with one symbol and only one. This symbol indicates what symbol we are trading
        /// </summary>
        public string TradingSymbol { get; set; }


        private string _selectedTrade = null;
        private int _selectedTradeIndex = -1;
        private Account _trading;
        private int _tradeID = 0;
        private string _accountCurrency;
        private List<Price> _lastPrices;

        public Entities Dt { get; set; }
        public Sessions UserSession { get; set; }

        private TradingData tradeToDB;


        public TradingManager()
        {
            InitializeComponent();

            Dt = new Entities();

            _accountCurrency = "USD";

            TradingSymbol = "EUR/USD";
            _lastPrices = new List<Price>();

            _trading = new Account(_accountCurrency, 1000, 100, 50);



            UpdateAccountGrid(_trading);
            UpdateTradesGrid(_trading);

            _trading.OnPriceChange += _trading_OnPriceChange;
            _trading.OnTradeClose += _trading_OnTradeClose;
            _trading.OnMarginCall += _trading_OnMarginCall;
            
        }

        private void _trading_OnMarginCall(Account a, string message)
        {
            UpdateAccountGrid(_trading);
            UpdateTradesGrid(_trading);

            alertMSG("Acount is closed you have reached a critical level of funds.");

        }

        private void _trading_OnTradeClose(Account a, string message)
        {
            UpdateAccountGrid(_trading);
            UpdateTradesGrid(_trading);
        }



        /// <summary>
        /// Update acount with a new prices (Update price list)
        /// </summary>
        /// <param name="p"></param>
        public void receiveNewPrice(Price p)
        {

            var x = _lastPrices.Find(price => price.Symbol == p.Symbol);
            if (x != null)
            {
                x.Ask = p.Ask;
                x.Bid = p.Bid;
            }
            else _lastPrices.Add(p);

            if (this.TradingSymbol == p.Symbol) _trading.priceChange(p);
        }

        /// <summary>
        /// Uptade comes from trading class
        /// </summary>
        /// <param name="a"></param>
        /// <param name="message"></param>
        private void _trading_OnPriceChange(Account a, string message)
        {
            UpdateAccountGrid(_trading);
            UpdateTradesGrid(_trading);
        }

        /// <summary>
        /// Add new Trades to list of trades
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="action"></param>
        /// <param name="amount"></param>
        /// <param name="price"></param>
        public void AddTrade(string action, decimal amount, Price priceSymbol)
        {
            AbstractTrade at;
            if (action == "buy") at = new Buy(++_tradeID, _accountCurrency, amount, priceSymbol);
            else at = new Sell(++_tradeID, _accountCurrency, amount, priceSymbol);



            SaveNewTradeToDb(at); // TODO sheck ID 
            _trading.AddTradeUpdateAccount(at);
        

            UpdateAccountGrid(_trading);
            UpdateTradesGrid(_trading);

        }

        /// <summary>
        /// retuns a trade ID
        /// </summary>
        /// <param name="at"></param>
        /// <returns></returns>
        private void SaveNewTradeToDb(AbstractTrade at)
        {
            TradingData td = new TradingData();

            if (at.GetType() == typeof(Buy)) {
                td.Action = "Buy";
                td.OpenPrice = at.AskInitial;
            }
            else {
                td.Action = "Sell";
                td.OpenPrice = at.BidInitial;
            }

            
            td.OpenTime = System.DateTime.Now;
            td.SessionID = UserSession.SessionID;
            td.Symbol = at.Symbol;
            td.Amount = at.Amount;

            td = Dt.TradingData.Add(td);


            int x =  Dt.SaveChanges();
            
            at.TradeID = td.TradeID;

   //         return td.TradeID;



        }


        public void CloseTradeUpdateDB(AbstractTrade at)
        {
            TradingData td = Dt.TradingData.Find(at.TradeID);

            if (at.GetType() == typeof(Buy))
            {   
                td.ClosePrice = at.Ask;
            }
            else
            {
                td.ClosePrice = at.Bid;
            }

            td.CloseTime = System.DateTime.Now;
            td.ProfitLoss = at.BaseGainLoss;
            Dt.TradingData.Add(td);
            int x = Dt.SaveChanges();

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tradeId"></param>
        public void CloseTrade(int tradeId)
        {

            UpdateAccountGrid(_trading);
            UpdateTradesGrid(_trading);

        }

       
        /// <summary>
        /// get last trading price frol last prices
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        private Price getPrice(string symbol)
        {
           var x = _lastPrices.Find( price => price.Symbol== symbol);
           return x;
        }

        /// <summary>
        /// Updates info about account
        /// </summary>
        /// <param name="a"></param>
        private void UpdateAccountGrid(Account a)
        {
            if (AccountDataGridView1.InvokeRequired)
            {
                AccountDataGridView1.Invoke(new MethodInvoker(delegate
                {
                    AccountDataGridView1.Rows.Clear();
                    string[] row = new string[] { "Account Status", a.AccountStatus.ToString() };
                    AccountDataGridView1.Rows.Add(row);

                    row = new string[] { "Account Value", a.AccountValue + "" };
                    AccountDataGridView1.Rows.Add(row);

                    row = new string[] { "Balance", a.Balance + "" };
                    AccountDataGridView1.Rows.Add(row);

                    row = new string[] { "Account Currency", a.BaseCurrency };
                    AccountDataGridView1.Rows.Add(row);

                    row = new string[] { "Deposit + Profit", a.Deposit + "" };
                    AccountDataGridView1.Rows.Add(row);

                    row = new string[] { "Leverage (times)", a.Leverage + "" };
                    AccountDataGridView1.Rows.Add(row);

                    row = new string[] { "Margin %", a.MarginLevel + "%" };
                    AccountDataGridView1.Rows.Add(row);

                    row = new string[] { "Critical Margin Level", a.MarginValue + "" };
                    AccountDataGridView1.Rows.Add(row);

                    row = new string[] { "Open Postions Value", a.OpenPositionsValue + "" };
                    AccountDataGridView1.Rows.Add(row);

                    row = new string[] { "Unrealized Profit/Loss", a.UnrealisedProfitLoss + "" };
                    AccountDataGridView1.Rows.Add(row);
                }));
            }
            

        }

        delegate void delegateTradesGride(Account a);

        /// <summary>
        /// Updates info about trades
        /// </summary>
        /// <param name="a"></param>
        private void UpdateTradesGrid(Account a)
        {
            if (TradesDataGridView1.InvokeRequired)
            {
                TradesDataGridView1.BeginInvoke(new delegateTradesGride(UpdateTradesGrid), a);
                return;
            }


            int index = -1;
            if (TradesDataGridView1.SelectedRows.Count > 0) // remember what row is selected
                index = TradesDataGridView1.SelectedRows[0].Index;

            string[] row;
            TradesDataGridView1.Rows.Clear();
            string action;
            foreach (AbstractTrade at in a.Trades)
            {
                decimal p, pInit;
                if (at.GetType() == typeof(Buy))
                {
                    p = at.Ask;
                    pInit = at.AskInitial;
                    action = "Buy";
                }
                else
                {
                    p = at.Bid;
                    pInit = at.BidInitial;
                    action = "Sell";
                }


                row = new string[] {
                                    at.TradeID+"",
                                    at.TradeStatus.ToString(),
                                    at.Symbol,
                                    action , // action
                                    at.Amount+"",
                                    pInit +"", // change accordingly to type of action
                                    p+"",
                                    at.GainLoss+""
                        };
                TradesDataGridView1.Rows.Add(row);
                var lr = TradesDataGridView1.Rows.GetLastRow(DataGridViewElementStates.Displayed);
                if (at.GainLoss > 0)
                {
                    if (at.TradeStatus == Status.Open) TradesDataGridView1.Rows[lr].DefaultCellStyle.BackColor = Color.Green;
                    else TradesDataGridView1.Rows[lr].DefaultCellStyle.BackColor = Color.LightGreen;
                }
                if (at.GainLoss < 0)
                {
                    if (at.TradeStatus == Status.Open) TradesDataGridView1.Rows[lr].DefaultCellStyle.BackColor = Color.Red;
                    else TradesDataGridView1.Rows[lr].DefaultCellStyle.BackColor = Color.MistyRose;
                }

            }

            // keep selection of selected row
            if ((TradesDataGridView1.RowCount > index) && (index != -1))
            {
                TradesDataGridView1.Rows[index].Selected = true;
            }
        }

        private void CloseToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (_selectedTrade != null)
            {

                
                _trading.CloseTradeUpdateAccount(System.Convert.ToInt32(_selectedTrade));
                CloseTradeUpdateDB(_trading.Trades.Find(x => x.TradeID == System.Convert.ToInt32(_selectedTrade)));
                _selectedTrade = null;
            }
        }

        private void TradesDataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hti = TradesDataGridView1.HitTest(e.X, e.Y);
                TradesDataGridView1.ClearSelection();
                if (hti.RowIndex != -1) TradesDataGridView1.Rows[hti.RowIndex].Selected = true;
                else if (_selectedTradeIndex != -1)
                    TradesDataGridView1.Rows[_selectedTradeIndex].Selected = true;
            }
        }



        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (toolStripComboBox1.SelectedIndex != -1)
            {
                contextMenuStrip1.Hide();
                var x = validateAmount(toolStripComboBox1.Text);
                if (x != "") errorMsg(x);
                else
                {
                    infoMsg("Buy", toolStripComboBox1.Text);
                    contextMenuStrip1.Hide();
                    // do trade                    
                    this.AddTrade("buy", System.Convert.ToDecimal(toolStripComboBox1.Text), this.getPrice(TradingSymbol));
                }
                
            }
        }


        private void buyToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            toolStripComboBox1.SelectedIndex = -1;
        }

        private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(toolStripComboBox2.SelectedIndex != -1)
            {
                contextMenuStrip1.Hide();
                var x = validateAmount(toolStripComboBox2.Text);
                if (x != "") errorMsg(x);
                else
                {
                    infoMsg("Sell", toolStripComboBox2.Text);
                    //do trade
                    this.AddTrade("sell", System.Convert.ToDecimal(toolStripComboBox2.Text), this.getPrice(TradingSymbol));
                }
            }
        }

        private void sellToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            toolStripComboBox2.SelectedIndex = -1;
        }

        private void BuyToolStripMenuItem1_DropDownOpening(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";
        }

        private void SellToolStripMenuItem2_DropDownOpening(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";
        }

        private void toolStripTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
             if (e.KeyCode == Keys.Enter)
            {
                var x = validateAmount(toolStripTextBox2.Text);
                if (x != "") errorMsg(x);
                else
                {
                    infoMsg("Sell", toolStripTextBox2.Text);
                    //do trade
                    this.AddTrade("sell", System.Convert.ToDecimal(toolStripTextBox2.Text), this.getPrice(TradingSymbol));
                }
            }
        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            
             if (e.KeyCode == Keys.Enter)
            {
                var x = validateAmount(toolStripTextBox1.Text);
                if (x != "") errorMsg(x);
                else
                {
                    infoMsg("Buy", toolStripTextBox1.Text);
                    /// do trade
                    this.AddTrade("buy", System.Convert.ToDecimal(toolStripTextBox1.Text), this.getPrice(TradingSymbol));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="amount"></param>
        private void infoMsg(string action, string amount)
        {
            System.Windows.Forms.MessageBox.Show(action + " order sent for "+ amount + " units.", "Info",  MessageBoxButtons.OK, MessageBoxIcon.Information );
        }

        private void alertMSG(string str)
        {
            System.Windows.Forms.MessageBox.Show(str, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }


        private void errorMsg(string err)
        {
            System.Windows.Forms.MessageBox.Show(err, "Error",  MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="amout"></param>
        /// <returns></returns>
        private string validateAmount(string amount)
        {
            try
            {
                var x = System.Convert.ToDecimal(amount);
                if (x > _trading.AvailableForTrade)
                    return "Not enougth funds for this trade of amount " + amount + "."; 
            }
            catch (Exception e)
            {
                return "Amount \"" + amount + "\" for trade in not valid. Error msg: " + e.Message  ;
            }
            return "";
        }

        /// <summary>
        /// on context menu we select trade
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (TradesDataGridView1.SelectedRows.Count > 0)
            {
                var x = TradesDataGridView1.SelectedRows[0];
                _selectedTrade = (string)x.Cells[0].Value;
                _selectedTradeIndex = x.Index;
            }
            else if (_selectedTradeIndex != -1)
            {
                TradesDataGridView1.Rows[_selectedTradeIndex].Selected = true;
            }


            // Disable trading functions on exceeding temperature limits
            if (!((TemperatureLimits.UpperLimit > LastBioData.Temp) && (LastBioData.Temp > TemperatureLimits.LowerLimit)))
            {
                MessageBox.Show("In Current Condition Trading Is Not Allowed.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                e.Cancel = true;

            }

        }

        private void TradesDataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (TradesDataGridView1.SelectedRows.Count > 0)
            {
                var x = TradesDataGridView1.SelectedRows[0];
                _selectedTrade = (string)x.Cells[0].Value;
                _selectedTradeIndex = x.Index;
            }
        }

        private void buyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
