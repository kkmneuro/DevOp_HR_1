using OxyPlot;
using OxyPlot.Annotations;
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

        public bool ShowUserActions { get; private set; }

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
            if (plotView.Model == null)
                return;

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

        private void btnUserActions_Click(object sender, EventArgs e)
        {
            if (plotView.Model == null)
                return;

            ShowUserActions = !ShowUserActions;
            UpdateUserActionsAnnotations();
            plotView.Refresh();
        }

        public void UpdateUserActionsAnnotations()
        {
            if (ShowUserActions)
            {
                foreach (var action in Application.userActions)
                {
                    var annotation = new LineAnnotation();
                    annotation.Type = LineAnnotationType.Vertical;
                    annotation.X = action.time.ToOADate();
                    annotation.Color = OxyColors.Black;

                    switch (action.actionId)
                    {
                        case 3:
                            annotation.Text = "Start";
                            break;
                        case 4:
                            annotation.Text = "Stop";
                            break;
                        case 50:
                            switch (action.detail)
                            {
                                case 100:
                                    annotation.Text = "HIGH";
                                    break;
                                case 101:
                                    annotation.Text = "LOW";
                                    break;
                                case 102:
                                    annotation.Text = "AGAINST";
                                    break;
                                case 103:
                                    annotation.Text = "FAVOR";
                                    break;
                                case 104:
                                    annotation.Text = "EXIT";
                                    break;
                                case 105:
                                    annotation.Text = "ENTER";
                                    break;
                                case 106:
                                    annotation.Text = "LONG";
                                    annotation.Color = OxyColors.DarkBlue;
                                    annotation.TextColor = OxyColors.DarkBlue;
                                    break;
                                case 107:
                                    annotation.Text = "SHORT";
                                    annotation.Color = OxyColors.DarkRed;
                                    annotation.TextColor = OxyColors.DarkRed;
                                    break;
                                case 108:
                                    annotation.Text = "NO DIRECT";
                                    break;
                            }
                            break;
                    }

                    annotation.FontSize = 7;
                    annotation.TextMargin = 5;
                    plotView.Model.Annotations.Add(annotation);
                    annotation.EnsureAxes();
                }

                btnUserActions.BackColor = Color.PaleGreen;
            }
            else
            {
                plotView.Model.Annotations.Clear();
                btnUserActions.BackColor = SystemColors.Control;
            }
        }
    }
}
