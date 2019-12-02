using NeuroXChange.Controller;
using NeuroXChange.Model;
using System;
using System.Collections;
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
    public partial class SymbolSelectionWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private MainNeuroXModel model;
        private MainNeuroXController controller;
        private List<string> currentselectedsymbol;

        public SymbolSelectionWindow(MainNeuroXModel model, MainNeuroXController controller)
        {
            InitializeComponent();
            this.model = model;
            this.controller = controller;
        }

        private void SymbolSelectionWindow_Load(object sender, EventArgs e)
        {
            this.model.fixApiModel.RefreshSymbols -= LoadSymbols;
            this.model.fixApiModel.RefreshSymbols += LoadSymbols;
            var inidata = model.iniFileReader.Read("Symbols", "MarketWatchSymbol");

            if (inidata.Length > 0)
               currentselectedsymbol= inidata.Split(',').ToList<string>();
        }

        private void LoadSymbols(string[] Symbols)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string[]>(LoadSymbols), new object[] { Symbols });
                return;
            }
           
            this.chkSymbols.Text = "";
            this.chkSymbols.Items.AddRange(Symbols);
            //load symbols from the ini file.
            if (currentselectedsymbol!=null && currentselectedsymbol.Count > 0)
            {                
             //   this.model.fixApiModel.SubscribeForQuotes(SubscribeRequestType.SUBSCRIBE, currentselectedsymbol.Trim());
            foreach (var item in currentselectedsymbol)
                {
                    if (chkSymbols.FindStringExact(item) != -1)
                    {
                        chkSymbols.SetItemChecked(chkSymbols.FindStringExact(item), true);
                        DataRow dr = Globals.TradeTable.NewRow();                        
                        dr["CONTRACT"] = item;
                        dr["BUY"] = "0.0";
                        dr["SELL"] = "0.0";
                        Globals.TradeTable.Rows.Add(dr);
                    }
                }
             }           
            

            this.model.fixApiModel.SubscribeForQuotes(SubscribeRequestType.SUBSCRIBE, Symbols.ToList<string>());

            this.lblStatus.Visible = false;
        }

        private void btnSaveExit_Click(object sender, EventArgs e)
        {
            List<string> checkedsymbols = new List<string>();
            Globals.TradeTable.Clear();

            foreach (object itemChecked in chkSymbols.CheckedItems)
            {
               checkedsymbols.Add(itemChecked as string);
                //Add rows to datable
                DataRow dr = Globals.TradeTable.NewRow();
                dr["CONTRACT"] = itemChecked;
                dr["BUY"] = "0.0";
                dr["SELL"] = "0.0";
                Globals.TradeTable.Rows.Add(dr);
            }   
               
            model.iniFileReader.Write("Symbols", string.Join(",",checkedsymbols.ToArray()), "MarketWatchSymbol");
            this.model.fixApiModel.SubscribeForQuotes(SubscribeRequestType.SUBSCRIBE, checkedsymbols);

            //Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
