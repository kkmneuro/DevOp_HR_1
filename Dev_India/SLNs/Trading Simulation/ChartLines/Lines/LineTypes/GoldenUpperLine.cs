using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace ChartLines.Lines.LineTypes
{
    class GoldenUpperLine : HorizontalLineWithTextAnnotation
    {
        public GoldenUpperLine(Chart c, int chartArea, int y)
            : base(c, chartArea, y)
        {
            updateMe("GoldenUpper", Color.Goldenrod, ChartDashStyle.Solid);
        }
    }
}

