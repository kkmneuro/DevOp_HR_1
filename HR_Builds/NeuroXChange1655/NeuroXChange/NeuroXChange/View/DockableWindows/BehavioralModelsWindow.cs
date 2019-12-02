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

        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 1:
                    {
                        if (e.Value != null)
                        {
                            var value = e.Value.ToString();
                            if (value == "Initial state")
                            {
                                e.CellStyle.BackColor = Color.FromArgb(230, 184, 175);
                            }
                            else if (value == "Ready to trade")
                            {
                                e.CellStyle.BackColor = Color.FromArgb(244, 204, 204);
                            }
                            else if (value == "Preactivation")
                            {
                                e.CellStyle.BackColor = Color.FromArgb(234, 209, 220);
                            }
                            else if (value == "Direction confirmed")
                            {
                                e.CellStyle.BackColor = Color.FromArgb(217, 210, 233);
                            }
                            else if (value == "Execute order")
                            {
                                e.CellStyle.BackColor = Color.FromArgb(201, 218, 248);
                            }
                            else if (value == "Confirmation filled")
                            {
                                e.CellStyle.BackColor = Color.FromArgb(217, 234, 211);
                            }
                        }
                        break;
                    }
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
