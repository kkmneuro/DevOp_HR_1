using NeuroXChange.Controller;
using NeuroXChange.Model;
using NeuroXChange.Model.BehavioralModeling.BehavioralModels;
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
    public partial class NewOrderWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private MainNeuroXModel model;
        private MainNeuroXController controller;
        private MainNeuroXView view;

        public NewOrderWindow(MainNeuroXModel model, MainNeuroXController controller, MainNeuroXView view)
        {
            InitializeComponent();

            this.model = model;
            
            this.controller = controller;
            this.view = view;
        }

#if !SIMPLEST
        public void UpdateInterfaceFromModelState(BehavioralModelState state)
        {
            labStepName.Text = BehavioralModelStateHelper.StateToString(state);
            var activeModel = model.behavioralModelsContainer.behavioralModels[
                                     model.behavioralModelsContainer.ActiveBehavioralModelIndex];

            // check it before switch
            if (state == BehavioralModelState.DirectionConfirmed)
            {
                int direction = activeModel.Direction;
                Show();
                labStepName.Text = string.Format("Direction confirmed ({0})", BehavioralModelStateHelper.directionName[direction]);
            }

            if (!CBlinkButtonsToActiveBM.Checked)
            {
                return;
            }

            switch (state)
            {
                case BehavioralModelState.InitialState:
                    {
                        btnBuy.Enabled = false;
                        btnSell.Enabled = false;
                        btnBuy.BackColor = SystemColors.Control;
                        btnSell.BackColor = SystemColors.Control;
                        break;
                    }
                case BehavioralModelState.ReadyToTrade:
                    {
                        Show();
                        btnBuy.Enabled = false;
                        btnSell.Enabled = false;
                        btnBuy.BackColor = SystemColors.Control;
                        btnSell.BackColor = SystemColors.Control;
                        break;
                    }
                case BehavioralModelState.Preactivation:
                    {
                        Show();
                        btnBuy.Enabled = true;
                        btnSell.Enabled = true;
                        btnBuy.BackColor = Color.RoyalBlue;
                        btnSell.BackColor = Color.Red;
                        break;
                    }
                case BehavioralModelState.DirectionConfirmed:
                    {
                        int direction = activeModel.Direction;
                        btnBuy.Enabled = direction == 0;
                        btnSell.Enabled = direction == 1;
                        if (direction == 0)
                        {
                            btnBuy.BackColor = Color.RoyalBlue;
                            btnSell.BackColor = SystemColors.Control;
                        }
                        else if (direction == 1)
                        {
                            btnBuy.BackColor = SystemColors.Control;
                            btnSell.BackColor = Color.Red;
                        }
                        else
                        {
                            btnBuy.BackColor = SystemColors.Control;
                            btnSell.BackColor = SystemColors.Control;
                        }
                        break;
                    }
                case BehavioralModelState.ExecuteOrder:
                case BehavioralModelState.ConfirmationFilled:
                    {
                        break;
                    }
            }
        }
#endif

        private void buysell_button_Click(object sender, EventArgs e)
        {
            int direction = sender == btnBuy ? 0 : 1;
            view.manualOrderConfirmationWindow.ShowDialog(direction);
        }

        private void CBlinkButtonsToActiveBM_CheckedChanged(object sender, EventArgs e)
        {
#if !SIMPLEST
            UpdateInterfaceFromModelState(model.getActiveBehavioralModel().CurrentTickState);

            if (!CBlinkButtonsToActiveBM.Checked)
            {
                btnBuy.Enabled = true;
                btnSell.Enabled = true;
                btnBuy.BackColor = Color.RoyalBlue;
                btnSell.BackColor = Color.Red;
            }
#endif
        }
    }
}
