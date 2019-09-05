using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

using ChartLines.Lines.LineCommands;
using ChartLines.Lines.LineTypes;

namespace ChartLines.Lines.LineCommands
{
    public class WhiteDashedCommand : Command, IMultiplLineReset , ISetLineState
    {

        
        public WhiteDashedCommand(Chart cc, List<HorizontalLineWithTextAnnotation> ll)
            :base(cc, ll)
        {
            _MaxLinesOFThisType = -1; // unlimited

            Type type = typeof(DashedWhiteLine);
            var list = ll.FindAll(o => o.GetType() == type);
            foreach (var a in list)
            {
                a.upddate(_c);
                _count++;
            }

        }

        /// <summary>
        /// Puting the line on chart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void Cc_MouseDoubleClick(object sender, MouseEventArgs e)
        {
                DashedWhiteLine annotation = new DashedWhiteLine(_c, 0, e.Y);
                _annotations.Add(annotation);
                _count++;
        }

        /// <summary>
        /// Cleans all lines of a type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Reset()
        {
            removeThisTypeAnnotations<DashedWhiteLine>();
        }

        public void SetActive()
        {
            SetActiveAnotation<DashedWhiteLine>();
        }

        public void SetHidden()
        {
            SetHiddenAnotation<DashedWhiteLine>();
        }

        public void SetStatic()
        {
            SetStaticAnotation<DashedWhiteLine>();
        }

        public void RemoveLines()
        {
            removeThisTypeAnnotations<DashedWhiteLine>();
        }
    }
}
