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
    public class DodgerBlueCommand : Command, ISetLineState
    {

        
        public DodgerBlueCommand(Chart cc, List<HorizontalLineWithTextAnnotation> ll)
            :base(cc, ll)
        {
            _MaxLinesOFThisType = 2;

            Type type = typeof(DodgerBlueLine);
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
                DodgerBlueLine annotation = new DodgerBlueLine(_c, 0, e.Y);
                _annotations.Add(annotation);
                _count++;
            }
            else
            {
                removeThisTypeAnnotations<DodgerBlueLine>();
            }
        }


        public void SetActive()
        {
            SetActiveAnotation<DodgerBlueLine>();
        }

        public void SetHidden()
        {
            SetHiddenAnotation<DodgerBlueLine>();
        }

        public void SetStatic()
        {
            SetStaticAnotation<DodgerBlueLine>();
        }

        public void RemoveLines()
        {
            removeThisTypeAnnotations<DashedWhiteLine>();
        }

    }
}
