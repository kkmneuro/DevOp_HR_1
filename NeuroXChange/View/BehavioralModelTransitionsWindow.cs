using NeuroXChange.Model;
using NeuroXChange.Model.BehavioralModeling.BehavioralModels;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NeuroXChange.View
{
    public partial class BehavioralModelTransitionsWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private MainNeuroXModel model;

        private CheckBox[] filterCheckBoxes;

        public BehavioralModelTransitionsWindow(MainNeuroXModel model)
        {
            InitializeComponent();
            this.model = model;
            this.filterCheckBoxes =
                new CheckBox[]{ initialStateCB, readyToTradeCB, preactivationCB, directionConfirmedCB, executeOrderCB, confirmationFilledCB };
        }

        private void modelCB_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            behavioralModelTransitionsDGV.DataSource =
                model.behavioralModelsContainer.behavioralModels[modelCB.SelectedIndex].TransitionHistoryTable;
            behavioralModelTransitionsDGV.Sort(behavioralModelTransitionsDGV.Columns[0], ListSortDirection.Descending);
            stateCheckbox_CheckedChanged(sender, e);
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

        private void stateCheckbox_CheckedChanged(object sender, System.EventArgs e)
        {
            var table = (DataTable)behavioralModelTransitionsDGV.DataSource;
            if (table == null)
            {
                return;
            }

            StringBuilder filter = new StringBuilder("1 = 2 ");
            foreach (var checkBox in filterCheckBoxes)
            {
                if (checkBox.Checked)
                {
                    filter.Append(" OR ");
                    filter.Append("[To state] = '");
                    filter.Append(checkBox.Text);
                    filter.Append("'");
                }
            }
            table.DefaultView.RowFilter = filter.ToString();
        }
    }
}
