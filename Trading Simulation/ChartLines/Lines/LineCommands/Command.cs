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
    public abstract class Command : ICommand
    {
        protected Chart _c;
        protected List<HorizontalLineWithTextAnnotation> _annotations;
        protected int _count; // line count
        protected int _MaxLinesOFThisType; //max line count
        protected Type _t;

        public Command(Chart cc, List<HorizontalLineWithTextAnnotation> ll)
        {
            _c = cc;
            _annotations = ll;
            

       //     cc.AnnotationPlaced += Cc_AnnotationPlaced;
            
        }


        /// <summary>
        ///  Adding a line to chart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        public abstract void Cc_MouseDoubleClick(object sender, MouseEventArgs e);


        /// <summary>
        /// 
        /// </summary>
        public void activateLinePlacing()
        {
            _c.MouseDoubleClick += Cc_MouseDoubleClick;
        }


        public void deActivateLinePlacing()
        {
            _c.MouseDoubleClick -= Cc_MouseDoubleClick;
        }




        protected void removeThisTypeAnnotations<T>()
        {

            Type type = typeof(T);
            
            var itemsToRemove = _annotations.Where(o => o.GetType() == type).ToList();

            foreach (var item in itemsToRemove)
            {
                ((HorizontalLineWithTextAnnotation)(object)item).RemoveFromAnotations();
                _count--;
                _annotations.Remove(item);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        protected void SetStaticAnotation<T>()
        {
            Type type = typeof(T);
            var itemsToSetStatic = _annotations.Where(o => o.GetType() == type).ToList();
            foreach (var item in itemsToSetStatic)
                ((HorizontalLineWithTextAnnotation)(object)item).SetStatic();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        protected void RemoveLines<T>()
        {
            Type type = typeof(T);
            var itemsToSetStatic = _annotations.Where(o => o.GetType() == type).ToList();
            foreach (var item in itemsToSetStatic)
                ((HorizontalLineWithTextAnnotation)(object)item).SetStatic();
        }


        protected void SetActiveAnotation<T>()
        {
            Type type = typeof(T);
            var itemsToSetStatic = _annotations.Where(o => o.GetType() == type).ToList();
            foreach (var item in itemsToSetStatic)
                ((HorizontalLineWithTextAnnotation)(object)item).SetActive();
        }


        protected void SetHiddenAnotation<T>()
        {
            Type type = typeof(T);
            var itemsToSetStatic = _annotations.Where(o => o.GetType() == type).ToList();
            foreach (var item in itemsToSetStatic)
                ((HorizontalLineWithTextAnnotation)(object)item).SetInvisible();
        }


        private void Cc_AnnotationPlaced(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }





    }
}
