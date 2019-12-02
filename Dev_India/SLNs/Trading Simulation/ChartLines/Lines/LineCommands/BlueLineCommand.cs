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
    public class BlueLineCommand : Command, ISetLineState
    {

        
        public BlueLineCommand(Chart cc, List<HorizontalLineWithTextAnnotation> ll)
            :base(cc, ll)
        {
            
            _MaxLinesOFThisType = 4;

            Type type = typeof(BlueLine);
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
                BlueLine annotation = new BlueLine(_c, 0, e.Y);
                _annotations.Add(annotation);
                _count++;
            }
            else
            {
                removeThisTypeAnnotations<BlueLine>();
            }
        }

        public void SetActive()
        {
            SetActiveAnotation<BlueLine>();
        }

        public void SetHidden()
        {
            SetHiddenAnotation<BlueLine>();
        }

        public void SetStatic()
        {
            SetStaticAnotation<BlueLine>();
        }

        public void RemoveLines()
        {
            removeThisTypeAnnotations<BlueLine>();
        }
    }
}
