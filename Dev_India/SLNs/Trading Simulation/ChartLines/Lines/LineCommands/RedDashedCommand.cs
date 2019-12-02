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
    public class RedDashedCommand : Command, IMultiplLineReset, ISetLineState
    {

        
        public RedDashedCommand(Chart cc, List<HorizontalLineWithTextAnnotation> ll)
            :base(cc, ll)
        {
            _MaxLinesOFThisType = -1; // unlimited


            Type type = typeof(RedDashedLine);
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
            
                RedDashedLine annotation = new RedDashedLine(_c, 0, e.Y);
                _annotations.Add(annotation);
                _count++;
            
        }

        /// <summary>
        /// Cleans all lines of a type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Reset()
        {
            removeThisTypeAnnotations<RedDashedLine>();
        }


        public void SetActive()
        {
            SetActiveAnotation<RedDashedLine>();
        }

        public void SetHidden()
        {
            SetHiddenAnotation<RedDashedLine>();
        }

        public void SetStatic()
        {
            SetStaticAnotation<RedDashedLine>();
        }


        public void RemoveLines()
        {
            removeThisTypeAnnotations<RedDashedLine>();
        }

    }
}
