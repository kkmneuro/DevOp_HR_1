using System;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using ChartLines.Lines.LineTypes;

namespace ChartLines.Lines.LineCommands
{
    public class AquaLineCommand : Command, ISetLineState
    {

        public AquaLineCommand(Chart cc, List<HorizontalLineWithTextAnnotation> ll)
            :base(cc, ll)
        {

            _MaxLinesOFThisType = 2;

            Type type = typeof(AquaLine);

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
        public override void Cc_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            if (_count < _MaxLinesOFThisType)
            {
                AquaLine annotation = new AquaLine(_c, 0, e.Y);
                _annotations.Add(annotation);
                _count++;
            }
            else
            {
                removeThisTypeAnnotations<AquaLine>();
            }
        }



        /// <summary>
        /// Cleans all lines of a type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Reset()
        {
            removeThisTypeAnnotations<AquaLine>();
        }

        public void SetActive()
        {
            SetActiveAnotation<AquaLine>();
        }

        public void SetHidden()
        {
            SetHiddenAnotation<AquaLine>();
        }

        public void SetStatic()
        {
            SetStaticAnotation<AquaLine>();
        }


        public void RemoveLines()
        {
            removeThisTypeAnnotations<AquaLine>();
        }
    }
}
