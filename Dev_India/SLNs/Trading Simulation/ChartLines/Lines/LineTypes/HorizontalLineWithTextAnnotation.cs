using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace ChartLines.Lines.LineTypes
{
    public class HorizontalLineWithTextAnnotation : HorizontalLineAnnotation
    {

        public RectangleAnnotation Ra
        {
            get { return _ra; }
            private set { }
        }

        protected RectangleAnnotation _ra;
        protected string _lineName = "Super Line";
        protected Color _lineColor = Color.Black;
        protected ChartDashStyle _dashStyle = ChartDashStyle.Solid;
        private Chart cc;

        public double Value { get; set; }

        public HorizontalLineWithTextAnnotation(Chart c, int chartArea, int y) {
            this.cc = c;
            this.LineWidth = 3;
            this.LineColor = _lineColor;
            this.LineDashStyle = ChartDashStyle.Solid;
            this.IsInfinitive = true;
            this.AllowMoving = true;
            this.ClipToChartArea = "ChartArea1"; //   harcoded val is not good;
            this.Y = c.ChartAreas[chartArea].AxisY.PixelPositionToValue(y);
            this.AxisY = c.ChartAreas[chartArea].AxisY;


            c.Annotations.Add(this);
            
            _ra = new RectangleAnnotation();
            _ra.AxisX = c.ChartAreas[chartArea].AxisX;
            //_ra.AxisX.IsMarginVisible = false;
            _ra.IsSizeAlwaysRelative = true;
            _ra.Width = 2;//* xFactor;         // use your numbers!
            _ra.Height = 1; // * yFactor;        // use your numbers!
                            //   RA.Name = "myRect";

            scaleAnnotation(_ra);



            _ra.LineColor = _lineColor; // Color.Red;
            _ra.BackColor = _lineColor;
            _ra.AxisY = c.ChartAreas[chartArea].AxisY;
            _ra.ClipToChartArea = "ChartArea1"; //   harcoded val is not good;
            _ra.Y = this.Y;
            _ra.X = this.cc.Series[0].Points[0].XValue; // chart1.Width-10;

            _ra.Text = _lineName;
            _ra.ForeColor = Color.White;
            _ra.Font = new System.Drawing.Font("Arial", 8f);

            c.Annotations.Add(_ra);

            AlowMoving();

        }

        public void upddate(Chart c)
        {
            cc = c;
            this.AxisY = c.ChartAreas[0].AxisY;
            c.Annotations.Add(this);

            this.AllowMoving = true;

            _ra.AxisX = c.ChartAreas[0].AxisX;
         //   scaleAnnotation(_ra);
            _ra.AxisY = c.ChartAreas[0].AxisY;

            c.Annotations.Add(_ra);

            AlowMoving();
        }

        private void AlowMoving()
        {
            cc.AnnotationPositionChanged += C_AnnotationPositionChanged;
            cc.AnnotationPositionChanging += C_AnnotationPositionChanging;
        }


        void scaleAnnotation(Annotation A)
        {
            ChartArea CA = cc.ChartAreas[0];// pick your chartarea..
         //   A.AxisX = CA.AxisX;                 // .. and axis!
            int N = cc.Series[0].Points.Count;            // S1 being your Series !
                                                // to keep the label width constant the chart's width must be considered
                                                // 60 is my 'magic' number; you must adapt for your chart's x-axis scale!
            double xFactor = 100 * N / cc.Width;
            A.Width = 2 * xFactor;
            // A.X = A.X - A.Width / 2;

            //N = (int)cc.Series[0].Points.Max(x => x.YValues);

            double yFactor = 80 / cc.Height;
            A.Height = 1 * xFactor;
        }


        /// Tree states is needed 
        /// 1. moving
        /// 2. visible and thin not moving (static)
        /// 3. invisible
        /**/


        /// <summary>
        ///  Set line to visible and not moving
        /// </summary>
        public void SetStatic()
        {
            this.AllowMoving = false;
            _ra.AllowMoving = false;
            this.LineWidth = 2;
            this.Visible = true;
            _ra.Visible = true;
        }

        /// <summary>
        /// Line is not visible
        /// </summary>
        public void SetInvisible()
        {
            this.Visible = false;
            _ra.Visible = false;
        }

        /// <summary>
        /// We are able to see line and we can move line
        /// </summary>
        public void SetActive()
        {
            this.AllowMoving = true;
            _ra.AllowMoving = false;  // we are moving only line
            this.LineWidth = 4;
            this.Visible = true;
            _ra.Visible = true;
            AlowMoving();

        }

        /// <summary>
        /// removes from chart
        /// </summary>
        public void RemoveFromAnotations()
        {
            cc.Annotations.Remove(_ra);
            cc.Annotations.Remove(this);
        }


        public void updateMe(string name, Color color, ChartDashStyle dashStyle)
        {
            _lineName = name;
            _lineColor = color;
            this.LineColor = _lineColor;
            this._ra.LineColor = _lineColor; // Color.Red;
            this._ra.BackColor = _lineColor;
            _ra.Text = _lineName;
            this._dashStyle = dashStyle;
            this.LineDashStyle = _dashStyle;
            _ra.LineDashStyle = _dashStyle;
        }

        private void C_AnnotationPositionChanging(object sender, AnnotationPositionChangingEventArgs e)
        {
            HorizontalLineWithTextAnnotation annotation = (HorizontalLineWithTextAnnotation)sender;
            annotation.Ra.Y = e.NewLocationY;
        }

        private void C_AnnotationPositionChanged(object sender, EventArgs e)
        {
            HorizontalLineWithTextAnnotation annotation = (HorizontalLineWithTextAnnotation)sender;
            annotation.Ra.Y = annotation.Y;
            Value = annotation.Y;
        }



        /// <summary>
        /// ////////////////////////////////// to delte objets 
        /// </summary>
/*
        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        // Protected implementation of Dispose pattern.
        protected override void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //

            disposed = true;
            // Call base class implementation.
            base.Dispose(disposing);
        }
        */


    /*    public void ShowLabel()
        {
            cc.Annotations.Add(_ra);
        } */

    }
}
