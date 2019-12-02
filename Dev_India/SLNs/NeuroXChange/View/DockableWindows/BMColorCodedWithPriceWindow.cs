using NeuroXChange.Model;
using System;
using System.Windows.Forms;

namespace NeuroXChange.View
{
    public partial class BMColorCodedWithPriceWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private MainNeuroXModel model;

        public BMColorCodedWithPriceWindow(MainNeuroXModel model)
        {
            this.model = model;
            InitializeComponent();
        }

        private void ChartUpdate(object sender, EventArgs e)
        {
            ControlPaintUtils.Update(chart);
        }

        private void BMColorCodedWithPriceWindow_Load(object sender, EventArgs e)
        {
            var allChecked = Boolean.Parse(model.iniFileReader.Read("BMcoloredAllChecked", "Interface", "true"));
            inCheckingState = true;
            BModelsCLB.SetItemChecked(0, allChecked);
            inCheckingState = false;
            BModelsCLB_ItemCheck(sender, new ItemCheckEventArgs(
                0,
                allChecked ? CheckState.Checked : CheckState.Unchecked,
                allChecked ? CheckState.Unchecked : CheckState.Checked));
        }

        private void collapseBtn_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
            collapseBtn.Text = splitContainer1.Panel1Collapsed ? ">>" : "<<";
        }


        private bool inCheckingState = false;
        private void BModelsCLB_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (inCheckingState)
            {
                return;
            }
            inCheckingState = true;

            if (e.Index == 0)
            {
                for (int i = 1; i < BModelsCLB.Items.Count; i++)
                {
                    BModelsCLB.SetItemChecked(i, e.NewValue == CheckState.Checked);
                }
            }
            else
            {
                if (e.NewValue == CheckState.Unchecked)
                {
                    BModelsCLB.SetItemChecked(0, false);
                }
                else
                {
                    bool allChecked = true;
                    for (int i = 1; i < BModelsCLB.Items.Count; i++)
                    {
                        allChecked = e.Index == i || (e.Index != i && BModelsCLB.GetItemCheckState(i) == CheckState.Checked);
                        if (!allChecked)
                        {
                            break;
                        }
                    }
                    BModelsCLB.SetItemChecked(0, allChecked);
                }
            }
            inCheckingState = false;
            for (int i = 1; i < BModelsCLB.Items.Count; i++)
            {
                chart.Series[i - 1].Enabled = 
                    (i == e.Index ? e.NewValue : BModelsCLB.GetItemCheckState(i)) == CheckState.Checked;
            }
        }
    }
}
