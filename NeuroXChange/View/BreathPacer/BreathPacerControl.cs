using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Diagnostics;//Stopwatch class for exact time measures

namespace BreathPacer
{
    public partial class BreathPacerControl : UserControl
    {

#region Variables and props
        public delegate void PacerEventHandler(object sender, PacerEventArgs cycleCount);
        public event EventHandler<PacerEventArgs> CycleElapsed;
        double speed;
        int steps;
        int step = 0;
        double x1, x2, y1, y2;      // coordinates in [0,1] range according to width and height of panel
        int inhalePrecentage, exhalePrecentage;
        double breathsPerMinute;
        int lineWidth;
        Brush ballBrush;
        Color lineColor;
        Form formToOpen;
        int cyclesToOpenClose;
        private int breathSettingsIndex;
        Stopwatch stopWatch = new Stopwatch();

        public List<BreathSettings> BreathSettingsCollection { get; private set; }

        public bool Running { get { return tmrPacer.Enabled; } }

        public int ElapsedCycleCount { get; set; }

        public Label BpmLabel
        {
            get { return lblBpm; }
        }

        public double BreathsPerMinute
        {
            get
            {
                return breathsPerMinute;
            }
            set
            {
                breathsPerMinute = value;
                lblBpm.Text = breathsPerMinute + " bpm";
                if (tmrPacer.Enabled)
                {
                    Start();//just for proper reset
                }
            }
        }

        public int BallDiameter
        {
            get
            {
                return pbBall.Width;
            }

            set
            {
                pbBall.Width = pbBall.Height = value;
                Refresh();
            }
        }

        public Color LineColor
        {
            get
            {
                return lineColor;
            }

            set
            {
                lineColor = value;
                Refresh();
            }
        }

        public Color BallColor
        {
            get
            {
                return ((SolidBrush)ballBrush).Color;
            }

            set
            {
                ballBrush = new SolidBrush(value);
                Refresh();
            }
        }

        public int LineWidth
        {
            get
            {
                return lineWidth;
            }

            set
            {
                lineWidth = value;
                Refresh();
            }
        }
        public int InhalePrecentage
        {
            get
            {
                return inhalePrecentage;
            }
            set
            {
                inhalePrecentage = value;
                exhalePrecentage = 100 - inhalePrecentage;
                Refresh();
            }
        }

        public object CyclesToFinish { get; private set; }
        #endregion

        public BreathPacerControl()
        {
            InitializeComponent();

            this.ResizeRedraw = true;
            this.DoubleBuffered = true;

            BreathsPerMinute = 5.5;
            LineWidth = 15;
            BallDiameter = 25;
            BallColor = Color.Yellow;
            LineColor = Color.DeepSkyBlue;
            InhalePrecentage = 45;
            exhalePrecentage = 100 - InhalePrecentage;
            lblBpm.Text = breathsPerMinute + " bpm";
            pbBall.Left = 0;
            pbBall.Top = Height - pbBall.Height;
        }

        /// <summary>
        /// Starts BreathPacerControl in scheduled manner
        /// </summary>
        /// <param name="breathSettings">Breath settings list to apply</param>
        public void Start(List<BreathSettings> breathSettings)
        {
            BreathSettingsCollection = breathSettings;
            breathSettingsIndex = 0;

            if (breathSettings != null && breathSettings.Count > 0)
            {
                BreathsPerMinute = breathSettings[breathSettingsIndex].BreathsPerMinute;
                CyclesToFinish = breathSettings[breathSettingsIndex].CyclesToFinish;
                Start();
            }
        }

        /// <summary>
        /// Starts the BreathPacerControl in order to open new form after given cycle count elapsed
        /// </summary>
        /// <param name="formToOpen">Form to open when the time comes</param>
        /// <param name="cycleCount">Cycles to elapse unless the new form will be shown (and the calling form closed)</param>
        public void Start(Form formToOpen, int cycleCount)
        {
            this.formToOpen = formToOpen;
            cyclesToOpenClose = cycleCount;
            Start();
        }

        public void Start()
        {
            speed = 5.444444f / BreathsPerMinute * 3.5f; //3.5 = 5.444444 bpm
            steps = (int)(InhalePrecentage * speed);
            step = 0;

            x1 = 0;
            x2 = ((double)InhalePrecentage) / 100;
            y1 = 1;
            y2 = 0;

            tmrPacer.Interval = 20;
            ElapsedCycleCount = 0;
            Refresh();
            stopWatch.Start();

            tmrPacer.Start();
        }

        public void StartResonantBTest()
        {
            List<BreathSettings> breathSettings = new List<BreathSettings>();

            for (double bpm = 4; bpm < 8.75; bpm += .25)
            {
                breathSettings.Add(new BreathSettings() { BreathsPerMinute = bpm, CyclesToFinish = 5 });
            }

            Start(breathSettings);
        }

        public void StartResiliancyBTest()
        {
            List<BreathSettings> breathSettings = new List<BreathSettings>()
            {
                new BreathSettings() { BreathsPerMinute = 4,    CyclesToFinish = 5 },
                new BreathSettings() { BreathsPerMinute = 7.25, CyclesToFinish = 3 },
                new BreathSettings() { BreathsPerMinute = 6,    CyclesToFinish = 2 },
                new BreathSettings() { BreathsPerMinute = 4,    CyclesToFinish = 1 },
                new BreathSettings() { BreathsPerMinute = 5,    CyclesToFinish = 5 },
                new BreathSettings() { BreathsPerMinute = 7.5,  CyclesToFinish = 2 },
                new BreathSettings() { BreathsPerMinute = 5.25, CyclesToFinish = 1 },
                new BreathSettings() { BreathsPerMinute = 3,    CyclesToFinish = 4 },
                new BreathSettings() { BreathsPerMinute = 6,    CyclesToFinish = 2 },
                new BreathSettings() { BreathsPerMinute = 3.25, CyclesToFinish = 4 },
                new BreathSettings() { BreathsPerMinute = 6.25, CyclesToFinish = 4 },
                new BreathSettings() { BreathsPerMinute = 7,    CyclesToFinish = 3 },
                new BreathSettings() { BreathsPerMinute = 7.25, CyclesToFinish = 5 },
                new BreathSettings() { BreathsPerMinute = 5,    CyclesToFinish = 5 },
                new BreathSettings() { BreathsPerMinute = 5.5,  CyclesToFinish = 2 }
            };

            Start(breathSettings);
        }

        private void pbPacer_SizeChanged(object sender, EventArgs e)
        {
        }

        private void BreathPacerControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            GraphicsPath gp = new GraphicsPath();
            gp.AddLines(new Point[] {
                new Point(0, Height),
                new Point(Width * InhalePrecentage / 100, 0),
                new Point(Width, Height),
            });

            e.Graphics.DrawPath(new Pen(LineColor, LineWidth), gp);
        }

        protected virtual void OnCycleElapsedEvent(PacerEventArgs e)
        {
            EventHandler<PacerEventArgs> handler = CycleElapsed;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void pnlPacer_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(SystemColors.Control);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            GraphicsPath gp = new GraphicsPath();
            gp.AddLines(new Point[] {
                new Point(0, Height),
                new Point(Width * InhalePrecentage / 100, 0),
                new Point(Width, Height),
            });

            e.Graphics.DrawPath(new Pen(LineColor, LineWidth), gp);
        }

        private void pbBall_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.FillEllipse(ballBrush, new Rectangle(0, 0, pbBall.Width, pbBall.Height));
        }

        public void Stop()
        {
            tmrPacer.Stop();
        }

        public void Continue()
        {
            tmrPacer.Start();
        }

        private void tmrPacer_Tick(object sender, EventArgs e)
        {
            if (step < steps)
            {
                move();
            }
            else
            {
                if (y1 > y2)
                {
                    x1 = ((double)InhalePrecentage) / 100;
                    x2 = 1;
                    y1 = 0;
                    y2 = 1;
                    steps = (int)(exhalePrecentage * speed);
                } else // cycle elapsed
                {
                    x1 = 0;
                    x2 = ((double)InhalePrecentage) / 100;
                    y1 = 1;
                    y2 = 0;
                    steps = (int)(InhalePrecentage * speed);

                    OnCycleElapsedEvent(new PacerEventArgs(++ElapsedCycleCount));
                }
                step = 0;
                move();
            }
            //else //cycle elapsed
            //{
            //    stopWatch.Stop();

            //    OnCycleElapsedEvent(new PacerEventArgs(++ElapsedCycleCount));

            //    if(BreathSettingsCollection != null  && BreathSettingsCollection[breathSettingsIndex].CyclesToFinish  == ElapsedCycleCount)
            //    {
            //        if(breathSettingsIndex < BreathSettingsCollection.Count - 1)
            //        {
            //            breathSettingsIndex++;
            //            BreathsPerMinute = BreathSettingsCollection[breathSettingsIndex].BreathsPerMinute;
            //            CyclesToFinish = BreathSettingsCollection[breathSettingsIndex].CyclesToFinish;
            //            Start();
            //        }
            //        else
            //        {
            //            Stop();
            //            ParentForm.Close();
            //        }
            //    }

            //    if(cyclesToOpenClose > 0 && ElapsedCycleCount == cyclesToOpenClose)
            //    {
            //        if (formToOpen != null)
            //        {
            //            formToOpen.Show();
            //        }
            //        this.ParentForm.Close();
            //    }

            //    stopWatch.Restart();

            //    steps = (int)(InhalePrecentage * speed);
            //    step = 0;
            //    pbBall.Left = 0;
            //    pbBall.Top = Height;
            //}
        }

        private void move()
        {
            step++;
            pbBall.Left = (int) (((steps - step) * x1 + step * x2) / steps * Width) - pbBall.Width / 2;
            pbBall.Top = (int)(((steps - step) * y1 + step * y2) / steps * Height) - pbBall.Height / 2;
        }
    }

    public class PacerEventArgs : EventArgs
    {
        public PacerEventArgs(int cycleCount)
        {
            ElapsedCycleCount = cycleCount;
        }

        public int ElapsedCycleCount { get; set; }
    }
}
