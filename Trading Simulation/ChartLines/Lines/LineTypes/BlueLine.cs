using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace ChartLines.Lines.LineTypes
{
    class BlueLine : HorizontalLineWithTextAnnotation
    {
        public BlueLine(Chart c, int chartArea, int y)
            : base(c, chartArea, y)
        {
            updateMe("Blue", Color.Blue, ChartDashStyle.Solid);
        }
    }
}
