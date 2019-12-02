using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace ChartLines.Lines.LineTypes
{
    class GoldenLowerLine : HorizontalLineWithTextAnnotation
    {
        public GoldenLowerLine(Chart c, int chartArea, int y)
            : base(c, chartArea, y)
        {
            updateMe("GoldenLower", Color.Goldenrod, ChartDashStyle.Solid);
        }
    }
}
