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

namespace NeuroXChange.View
{
    public partial class TradeControlWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        //public TradeControlWindow()
        //{
        //    InitializeComponent();
        //}

        private MainNeuroXModel model;

        public TradeControlWindow(MainNeuroXModel model)
        {
            this.model = model;
            InitializeComponent();
        }      

        private void TradeControlWindow_Load(object sender, EventArgs e)
        {
            this.model.fixApiModel.RefreshClient -= updateText;
            this.model.fixApiModel.RefreshClient += updateText;
            this.model.fixApiModel.RefreshQuotes -= updateSymbols;
            this.model.fixApiModel.RefreshQuotes += updateSymbols;
            grdSymbols.DataSource = Globals.TradeTable;
            this.grdSymbols.Columns["CLOSE"].Visible = false;
            this.grdSymbols.Columns["OPEN"].Visible = false;
            this.grdSymbols.Columns["HIGH"].Visible = false;
            this.grdSymbols.Columns["LTP"].Visible = false;
            this.grdSymbols.Columns["LOW"].Visible = false;
            this.grdSymbols.Columns["SIZE"].Visible = false;
            this.grdSymbols.Columns["VOL"].Visible = false;
        }


        private void updateSymbols(DataTable Symbols)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<DataTable>(updateSymbols), new object[] { Symbols });
                return;
            }

            //grdSymbols.DataSource = Symbols;
            grdSymbols.Update();
            grdSymbols.Refresh();

        }

        private void updateText(string msg)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(updateText), new object[] { msg });
                return;
            }
            
            textBox1.Text = msg + Environment.NewLine;

        }
    }
}
