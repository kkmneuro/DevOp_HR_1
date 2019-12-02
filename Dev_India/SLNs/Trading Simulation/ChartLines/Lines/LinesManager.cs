using System;
using System.Collections.Generic;
using ChartLines.Lines.LineTypes;
using System.Windows.Forms.DataVisualization.Charting;

using Common;
using ChartLines.Lines.LineCommands;

namespace ChartLines.Lines
{
    public class LinesManager
    {
        private List<HorizontalLineWithTextAnnotation> al = new List<HorizontalLineWithTextAnnotation>();
        private Dictionary<int, object> map = new Dictionary<int, object>();


        public LinesManager(Chart c)
        {
            AddChart(c);
        }

        public LinesManager() { }

        public void AddChart(Chart c)
        {
            map.Clear();
            map.Add((int)SelectionSteps.SELECTION_2Lines_Aqua, new AquaLineCommand(c, al));
            map.Add((int)SelectionSteps.SELECTION_4Lines_Blue, new BlueLineCommand(c, al));
            map.Add((int)SelectionSteps.SELECTION_2Lines_DodgerBlue, new DodgerBlueCommand(c, al));
            map.Add((int)SelectionSteps.SELECTION_Line_Golden_Lower, new GoldenLowerCommand(c, al));
            map.Add((int)SelectionSteps.SELECTION_Line_Golden_Upper, new GoldenUpperCommand(c, al));
            map.Add((int)SelectionSteps.SELECTION_Line_Lime, new LineCommands.LimeCommand(c, al));
            map.Add((int)SelectionSteps.SELECTION_Multiple_Dashed_Lines_RedWhite, new RedDashedCommand(c, al));
            map.Add((int)SelectionSteps.SELECTION_Multiple_Dashed_Lines_White, new WhiteDashedCommand(c, al));
            map.Add((int)SelectionSteps.REVIEW_All_Lines, null);

            //lets add all old anotation to new chart if therer is any
          /*  foreach (var a in al) { 
                c.Annotations.Add(a);
               // a.ShowLabel();
            }*/


        }

        /// <summary>
        /// Seting All Lines from moving to static 
        /// </summary>
        public void SetStaticAll()
        {
            foreach (HorizontalLineWithTextAnnotation a in al)
            {
                a.SetStatic();
            }
        }


        /// <summary>
        /// Sest static specific lines
        /// </summary>
        /// <param name="stepToGo"></param>
        public void SetStatic(SelectionSteps stepToGo)
        {
            Object o = map[(int)stepToGo];
            if (o is ISetLineState) ((ISetLineState)o).SetStatic();
        }

        /// <summary>
        /// Hiddin all Lines from chart
        /// </summary>
        public void HideAllLines()
        {
            foreach (HorizontalLineWithTextAnnotation a in al)
            {
                a.SetInvisible();
            }
        }


        public void HideSpecified(SelectionSteps stepToGo)
        {
            Object o = map[(int)stepToGo];
            if (o is ISetLineState) ((ISetLineState)o).SetHidden();
        }


        // Set all lines visible and movable 
        public void SetActiveAllLines()
        {
            foreach (HorizontalLineWithTextAnnotation a in al)
            {
                a.SetActive();
            }
        }

        public void ActivateSpecified(SelectionSteps stepToGo)
        {
            Object o = map[(int)stepToGo];
            if (o is ISetLineState) ((ISetLineState)o).SetActive();
        }

        public void removeAllLines()
        {
            while (al.Count > 0)
            {
                ((HorizontalLineWithTextAnnotation)al[0]).RemoveFromAnotations();
                al[0].Dispose();
                al[0] = null;
                al.RemoveAt(0);
            }

        }


        public void removeSpecified(SelectionSteps stepToGo)
        {
            Object o = map[(int)stepToGo];
            if (o is ISetLineState) ((ISetLineState)o).RemoveLines();
        }

        /// <summary>
        /// Stop placing all lines mouse doubleclick is not placing any more lines
        /// </summary>
        public void stopPlacingLines()
        {
            foreach (KeyValuePair<int, object> o in map)
            {
                //  KeyValuePair<int, Commands.Command> kvp = (KeyValuePair<int, Commands.Command>)o;
               if (o.Value != null) ((LineCommands.Command)o.Value).deActivateLinePlacing();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void StartPlacingLine(SelectionSteps stepToGo)
        {
            ((LineCommands.Command)map[(int)stepToGo]).activateLinePlacing();
        }


        /// <summary>
        /// If this step is multi line removet all line of this step
        /// </summary>
        /// <param name="stepToGo"></param>
        public void resetMultiLine(SelectionSteps stepToGo)
        {
            Object o = map[(int)stepToGo];
            if (o is IMultiplLineReset) ((IMultiplLineReset)o).Reset();
        }



    }
}
