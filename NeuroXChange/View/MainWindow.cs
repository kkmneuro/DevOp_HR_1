using NeuroXChange.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using WeifenLuo.WinFormsUI.Docking;

namespace NeuroXChange
{
    public partial class MainWindow : Form
    {
        private MainNeuroXView mainNeuroXView = null;

        public MainWindow(MainNeuroXView mainNeuroXView)
        {
            this.mainNeuroXView = mainNeuroXView;
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void rawInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainNeuroXView.rawInformationWindow.Show(dockPanel, DockState.Float);
        }

        private void chartsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainNeuroXView.chartsWindow.Show(dockPanel, DockState.Float);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainNeuroXView.logoWindow.Show();
        }

        private void newOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainNeuroXView.newOrderWindow.Show(dockPanel, DockState.Float);
        }
    }
}
