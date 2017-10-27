using NeuroXChange.Controller;
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
        MainNeuroXController controller;

        public BehavioralModelsWindow(MainNeuroXController controller)
        {
            this.controller = controller;
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
                case 2:
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

        private void BehavioralModelsWindow_Load(object sender, EventArgs e)
        {
            dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void dataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            controller.ChangeActiveModel(e.RowIndex);
        }
    }
}
