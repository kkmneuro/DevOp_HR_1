using NeuroXChange.Model;
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
    public partial class ProfitabilityWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private MainNeuroXModel model;

        private int selectedIndex = -1;

        public ProfitabilityWindow(MainNeuroXModel model)
        {
            InitializeComponent();

            this.model = model;
            profitabilityDGV.AutoGenerateColumns = true;
        }

        private void modelCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedIndex = modelCB.SelectedIndex;
            var tableToShow = model.behavioralModelsContainer.behavioralModels[selectedIndex].ProfitabilityHistory;
            bindingSource.DataSource = tableToShow;
            profitabilityDGV.AutoResizeColumns();
        }

        private void ProfitabilityWindow_Load(object sender, EventArgs e)
        {
            modelCB.SelectedIndex = Int32.Parse(model.iniFileReader.Read("ActiveModel", "BehavioralModels", "13")) - 1;
        }
    }
}
