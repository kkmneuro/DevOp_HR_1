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

        private void peakPerformanceGauge_Resize(object sender, EventArgs e)
        {
            var center = peakPerformanceGauge.Center;
            center.X = Width / 2;
            center.Y = Height / 2 + 90 / 2 - 4;
            peakPerformanceGauge.Center = center;

            var capPos = center;
            peakPerformanceGauge.Cap_Idx = 0;
            capPos.X = center.X - 9;
            capPos.Y = center.Y - 32;
            peakPerformanceGauge.CapPosition = capPos;
            peakPerformanceGauge.Cap_Idx = 1;
            capPos.X = center.X - 25;
            capPos.Y = center.Y - 20;
            peakPerformanceGauge.CapPosition = capPos;
        }

        private void peakPerformanceGauge_SizeChanged(object sender, EventArgs e)
        {
            peakPerformanceGauge_Resize(sender, e);
        }
    }
}
