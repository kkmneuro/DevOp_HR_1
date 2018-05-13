using System;
using System.Drawing;
using System.Windows.Forms;

namespace PostTradingAnalysis
{
    public partial class ChartWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private PostTradingAnalysisApplication Application;
        public Color Color { get; private set; }
        public ToolStripMenuItem ChartGroupItem { get; private set; }

        public string WindowName { get; private set; }

        public ChartWindow(PostTradingAnalysisApplication application, Color color, ToolStripMenuItem chartGroupItem, string windowName = "")
        {
            InitializeComponent();
            this.Application = application;
            this.Color = color;
            this.HideOnClose = true;
            this.ChartGroupItem = chartGroupItem;
            this.WindowName = windowName;
        }

        private void btnZoom_Click(object sender, EventArgs e)
        {
            var axis = plotView.Model.Axes[0];
            if (sender == btnVMinus || sender == btnVPlus)
            {
                axis = plotView.Model.Axes[1];
            }

            var mult = 0.5 * 2.0 / 3.0;
            if (sender == btnHMinus || sender == btnVMinus)
            {
                mult = 0.5 * 3.0 / 2.0;
            }

            var min = axis.ActualMinimum;
            var max = axis.ActualMaximum;
            var dist = (max - min) * mult;
            var mid = (max + min) / 2;
            axis.Zoom(mid - dist, mid + dist);
            plotView.Refresh();
        }

        protected override string GetPersistString()
        {
            return Text;
        }
    }
}
