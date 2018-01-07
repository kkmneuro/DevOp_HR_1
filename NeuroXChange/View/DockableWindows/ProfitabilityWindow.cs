using NeuroXChange.Model;
using System;
using System.Threading;
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
            openOrdersDGV.AutoGenerateColumns = true;
            closedOrdersDGV.AutoGenerateColumns = true;
        }

        private void modelCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedIndex = modelCB.SelectedIndex;
            var portfolio = model.behavioralModelsContainer.behavioralModels[selectedIndex].portfolio;

            var openOrdersList = portfolio.RunningOrders;
            openOrdersList.SynchronizationContext = SynchronizationContext.Current;
            openOrdersBS.DataSource = openOrdersList;
            foreach (DataGridViewColumn column in openOrdersDGV.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            openOrdersDGV.AutoResizeColumns();

            var closedOrdersList = portfolio.ClosedOrders;
            closedOrdersList.SynchronizationContext = SynchronizationContext.Current;
            closedOrdersBS.DataSource = closedOrdersList;
            foreach (DataGridViewColumn column in closedOrdersDGV.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            closedOrdersDGV.AutoResizeColumns();
        }

        private void ProfitabilityWindow_Load(object sender, EventArgs e)
        {
            modelCB.SelectedIndex = Int32.Parse(model.iniFileReader.Read("ActiveModel", "BehavioralModels", "13")) - 1;
        }

        private void openOrdersDGV_CellFormatting(object sender, System.Windows.Forms.DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value is DateTime)
            {
                e.Value = ((DateTime)e.Value).ToString("yyyy/MM/dd HH:mm:ss");
            }
        }

        private void closedOrdersDGV_CellFormatting(object sender, System.Windows.Forms.DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value is DateTime)
            {
                e.Value = ((DateTime)e.Value).ToString("yyyy/MM/dd HH:mm:ss");
            }
        }
    }
}
