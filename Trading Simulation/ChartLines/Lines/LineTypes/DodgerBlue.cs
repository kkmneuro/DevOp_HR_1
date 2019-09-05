using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace ChartLines.Lines.LineTypes
{
    class DodgerBlueLine : HorizontalLineWithTextAnnotation
    {
        public DodgerBlueLine(Chart c, int chartArea, int y)
            : base(c, chartArea, y)
        {
            updateMe("DodgerBlue", Color.DodgerBlue, ChartDashStyle.Solid);
        }
    }
}
