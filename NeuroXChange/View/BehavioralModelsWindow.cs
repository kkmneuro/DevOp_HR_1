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
    public partial class BehavioralModelsWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public BehavioralModelsWindow()
        {
            InitializeComponent();
        }

        private void BehavioralModelsWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Hide();
                e.Cancel = true;
            }
        }

        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 1:
                case 2:
                case 3:
                    {
                        if (e.Value != null)
                        {
                            var value = e.Value.ToString();
                            if (value == "ON")
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                            }
                            if (value == "OFF")
                            {
                                e.CellStyle.BackColor = Color.Pink;
                            }
                        }
                        break;
                    }
                case 5:
                    {
                        if (e.Value != null)
                        {
                            var value = e.Value.ToString();
                            if (value == "LONG")
                            {
                                e.CellStyle.BackColor = Color.RoyalBlue;
                            }
                            if (value == "SHORT")
                            {
                                e.CellStyle.BackColor = Color.Red;
                            }
                        }
                        break;
                    }
            }
        }
    }
}
