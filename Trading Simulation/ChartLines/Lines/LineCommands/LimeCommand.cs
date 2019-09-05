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
    public class LimeCommand : Command, ISetLineState
    {

        
        public LimeCommand(Chart cc, List<HorizontalLineWithTextAnnotation> ll)
            :base(cc, ll)
        {
            _MaxLinesOFThisType = 1;


            Type type = typeof(LimeLine);
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
                LimeLine annotation = new LimeLine(_c, 0, e.Y);
                _annotations.Add(annotation);
                _count++;
            }
            else
            {
                removeThisTypeAnnotations<LimeLine>();
            }
        }


        public void SetActive()
        {
            SetActiveAnotation<LimeLine>();
        }

        public void SetHidden()
        {
            SetHiddenAnotation<LimeLine>();
        }

        public void SetStatic()
        {
            SetStaticAnotation<LimeLine>();
        }

        public void RemoveLines()
        {
            removeThisTypeAnnotations<DashedWhiteLine>();
        }
    }
}
