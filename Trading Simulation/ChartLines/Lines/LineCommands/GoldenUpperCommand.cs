using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

using ChartLines.Lines.LineTypes;

namespace ChartLines.Lines.LineCommands
{
    public class GoldenUpperCommand : Command, ISetLineState
    {

        
        public GoldenUpperCommand(Chart cc, List<HorizontalLineWithTextAnnotation> ll)
            :base(cc, ll)
        {




            _MaxLinesOFThisType = 1;


            Type type = typeof(GoldenUpperLine);
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
            if (_count < _MaxLinesOFThisType)
            {
                GoldenUpperLine annotation = new GoldenUpperLine(_c, 0, e.Y);
                _annotations.Add(annotation);
                _count++;
            }
            else
            {
                removeThisTypeAnnotations<GoldenUpperLine>();
            }
        }


        public void SetActive()
        {
            SetActiveAnotation<GoldenUpperLine>();
        }

        public void SetHidden()
        {
            SetHiddenAnotation<GoldenUpperLine>();
        }

        public void SetStatic()
        {
            SetStaticAnotation<GoldenUpperLine>();
        }

        public void RemoveLines()
        {
            removeThisTypeAnnotations<DashedWhiteLine>();
        }
    }
}
