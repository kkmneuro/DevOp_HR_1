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

namespace NeuroXChange
{
    public partial class MainForm : Form
    {
        private MainNeuroXView mainNeuroXView = null;

        public MainForm(MainNeuroXView mainNeuroXView)
        {
            this.mainNeuroXView = mainNeuroXView;
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void chartsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainNeuroXView.chartsWindow.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainNeuroXView.logoWindow.Show();
        }
    }
}
