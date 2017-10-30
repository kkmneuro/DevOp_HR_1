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
    public partial class IndicatorsWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public IndicatorsWindow()
        {
            InitializeComponent();
        }

        private void IndicatorsWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Hide();
                e.Cancel = true;
            }
        }

        private void peakPerformanceGauge_Resize(object sender, EventArgs e)
        {
            var center = peakPerformanceGauge.Center;
            center.X = Width / 2;
            center.Y = Height / 2 + 90 / 2 - 4;
            peakPerformanceGauge.Center = center;
        }

        private void peakPerformanceGauge_SizeChanged(object sender, EventArgs e)
        {
            peakPerformanceGauge_Resize(sender, e);
        }
    }
}
