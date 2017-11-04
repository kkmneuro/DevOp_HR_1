using NeuroXChange.Model;
using System.ComponentModel;
using System.Windows.Forms;

namespace NeuroXChange.View
{
    public partial class BehavioralModelTransitionsWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private MainNeuroXModel model;
        public BehavioralModelTransitionsWindow(MainNeuroXModel model)
        {
            InitializeComponent();
            this.model = model;
        }

        private void modelCB_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            behavioralModelTransitionsDGV.DataSource =
                model.behavioralModelsContainer.behavioralModels[modelCB.SelectedIndex].TransitionHistorySet;
            behavioralModelTransitionsDGV.DataMember = "TransitionHistory";
            behavioralModelTransitionsDGV.Sort(behavioralModelTransitionsDGV.Columns[0], ListSortDirection.Descending);
        }

        private void BehavioralModelTransitionsWindow_Load(object sender, System.EventArgs e)
        {
            modelCB.SelectedIndex = 0;
        }

        private void BehavioralModelTransitionsWindow_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Hide();
                e.Cancel = true;
            }
        }
    }
}
