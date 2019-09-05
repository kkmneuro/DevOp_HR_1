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
    public partial class ChartsWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public ChartsWindow()
        {
            InitializeComponent();
        }

        private void heartRateChart_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            var result = heartRateChart.HitTest(pos.X, pos.Y);
            if (result.ChartArea != null)
            {
                var xVal = result.ChartArea.AxisX.PixelPositionToValue(pos.X);
                var yVal = result.ChartArea.AxisY.PixelPositionToValue(pos.Y);

                DateTime dt = DateTime.FromOADate(xVal);

                toolStripStatusLabel.Text = string.Format("Time: {0:HH:mm:ss}, Value: {1:0.##}", dt, yVal);
            }
        }

        private void heartRateChart_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = string.Empty;
        }

        private void ChartsWindow_VisibleChanged(object sender, EventArgs e)
        {
            if (!heartRateChart.Created)
            {
                return;
            }

            if (this.Visible)
            {
                heartRateChart.Series.ResumeUpdates();
                heartRateChart.ChartAreas[0].RecalculateAxesScale();
                heartRateChart.ChartAreas[1].RecalculateAxesScale();
            }
            else
            {
                heartRateChart.Series.SuspendUpdates();
            }
        }
    }
}
