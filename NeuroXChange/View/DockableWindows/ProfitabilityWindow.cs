using NeuroXChange.Model;
using System;
using System.Threading;

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
            openOrdersDGV.AutoResizeColumns();

            var closedOrdersList = portfolio.ClosedOrders;
            closedOrdersList.SynchronizationContext = SynchronizationContext.Current;
            closedOrdersBS.DataSource = closedOrdersList;
            closedOrdersDGV.AutoResizeColumns();
        }

        private void ProfitabilityWindow_Load(object sender, EventArgs e)
        {
            modelCB.SelectedIndex = Int32.Parse(model.iniFileReader.Read("ActiveModel", "BehavioralModels", "13")) - 1;
        }
    }
}
