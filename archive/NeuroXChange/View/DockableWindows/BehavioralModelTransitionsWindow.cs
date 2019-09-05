using NeuroXChange.Model;
using NeuroXChange.Model.BehavioralModeling.BehavioralModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace NeuroXChange.View
{
    public partial class BehavioralModelTransitionsWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private MainNeuroXModel model;

        private CheckBox[] filterCheckBoxes;

        private DataTable trackedData;

        private int selectedIndex;

        public BehavioralModelTransitionsWindow(MainNeuroXModel model)
        {
            InitializeComponent();

            trackedData = new DataTable("TransitionHistory");
            trackedData.Columns.Add("Time", typeof(string));
            trackedData.Columns.Add("To state", typeof(string));
            trackedData.Columns.Add("From state", typeof(string));
            trackedData.Columns.Add("Transition", typeof(string));

            bindingSource.DataSource = trackedData;
            behavioralModelTransitionsDGV.AutoGenerateColumns = true;
            this.model = model;
            this.filterCheckBoxes =
                new CheckBox[]{ initialStateCB, readyToTradeCB, preactivationCB, directionConfirmedCB, executeOrderCB, confirmationFilledCB };
            selectedIndex = -1;
        }

        private void modelCB_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            dataUpdaterTimer.Enabled = false;
            bindingSource.DataSource = null;
            selectedIndex = modelCB.SelectedIndex;
            trackedData.Clear();
            dataUpdaterTimer_Tick(sender, e);
            bindingSource.DataSource = trackedData;
            dataUpdaterTimer.Enabled = true;
            behavioralModelTransitionsDGV.Sort(behavioralModelTransitionsDGV.Columns[0], ListSortDirection.Descending);
        }

        private void BehavioralModelTransitionsWindow_Load(object sender, System.EventArgs e)
        {
            modelCB.SelectedIndex = Int32.Parse(model.iniFileReader.Read("ActiveModel", "BehavioralModels", "13")) - 1;
        }

        private void stateCheckbox_CheckedChanged(object sender, System.EventArgs e)
        {
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
            bindingSource.Filter = filter.ToString();
        }

        private void dataUpdaterTimer_Tick(object sender, EventArgs e)
        {
#if !SIMPLEST
            if (selectedIndex < 0 || selectedIndex >= model.behavioralModelsContainer.BehavioralModelsCount)
            {
                return;
            }

            var listToTrack = model.behavioralModelsContainer.behavioralModels[selectedIndex].TransitionHistory;
            var lastItem = listToTrack.Last;
            if (lastItem == null)
            {
                return;
            }

            if (lastItem.Value.ID < trackedData.Rows.Count)
            {
                return;
            }
            // if only new item is available
            if (lastItem.Value.ID == trackedData.Rows.Count)
            {
                AddItemToTrackedData(lastItem.Value);
            }
            else
            {
                // more than one new item is available
                var listToCopy = new LinkedList<TransitionHistoryItem>();
                while (lastItem!= null && lastItem.Value.ID >= trackedData.Rows.Count)
                {
                    listToCopy.AddFirst(lastItem.Value);
                    lastItem = lastItem.Previous;
                }
                foreach(var item in listToCopy)
                {
                    AddItemToTrackedData(item);
                }
            }
#endif
        }

#if !SIMPLEST
        private void AddItemToTrackedData(TransitionHistoryItem item)
        {
            var row = trackedData.NewRow();
            row["Time"] = item.Time;
            row["To state"] = item.ToState;
            row["From state"] = item.FromState;
            row["Transition"] = item.Transition;
            trackedData.Rows.Add(row);
            trackedData.AcceptChanges();
        }
#endif
    }
}
