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
    public partial class TraningControlWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        //public TradeControlWindow()
        //{
        //    InitializeComponent();
        //}

        private MainNeuroXModel model;
        private string currentselectedsymbol = "";

        public TraningControlWindow(MainNeuroXModel model)
        {
            this.model = model;
            InitializeComponent();
        }

        private void TradeControlWindow_Load(object sender, EventArgs e)
        {
            this.cmbSymbols.Items.Clear();
            this.model.fixApiModel.RefreshClient -= updateText;
            this.model.fixApiModel.RefreshClient += updateText;
            this.model.fixApiModel.RefreshSymbols -= LoadSymbols;
            this.model.fixApiModel.RefreshSymbols += LoadSymbols;
            currentselectedsymbol = model.iniFileReader.Read("Symbol", "TrainingSubscription");

        }

        private void LoadSymbols(string[] Symbols)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string[]>(LoadSymbols), new object[] { Symbols });
                return;
            }

            this.cmbSymbols.Text = "";
            this.cmbSymbols.Items.AddRange(Symbols);
            //load symbols from the ini file.
            if (currentselectedsymbol.Trim().Length>0)
            {
                cmbSymbols.SelectedItem = currentselectedsymbol.Trim();
                this.model.fixApiModel.SubscribeForQuotes(SubscribeRequestType.SUBSCRIBE,currentselectedsymbol.Trim());
            }
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

        private void btnSubscribe_Click(object sender, EventArgs e)
        {
            model.iniFileReader.Write("Symbol", cmbSymbols.SelectedItem.ToString(), "TrainingSubscription");
            if (currentselectedsymbol.Length!=0)
            {
                this.model.fixApiModel.SubscribeForQuotes(SubscribeRequestType.UNSUBSCRIBE, currentselectedsymbol.Trim());
            }

            this.model.fixApiModel.SubscribeForQuotes(SubscribeRequestType.SUBSCRIBE, cmbSymbols.SelectedItem.ToString());
        }

        private void cmbSymbols_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
