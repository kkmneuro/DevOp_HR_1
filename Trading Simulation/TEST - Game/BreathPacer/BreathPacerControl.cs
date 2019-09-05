using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;

namespace BreathPacer
{

    public partial class BreathPacerControl : UserControl
    {
        #region Variables and props
        public event EventHandler<PacerEventArgs> CycleElapsed;
        public event EventHandler RoutineFinished;
        double totalTime;
        int x, y, x1, x2, y1, y2;
        bool directionSwitched = false;
        int inhalePrecentage, exhalePrecentage;
        double breathsPerMinute;
        int lineWidth;
        Brush ballBrush;
        Color lineColor;
        Form formToOpen;
        int cyclesToOpenClose;
        private int breathSettingsIndex;
        private double upTime;
        int peakX;
        int minuteInMs = 59640;//should be 60 000, but thanks to this little tweak the pacer is even more accurate ;)


        Stopwatch stopWatch = new Stopwatch();


        public bool TestMode { get; set; }
        public List<BreathSettings> BreathSettingsCollection { get; private set; }

        public bool Running { get; private set; }

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
                
                if (renderingTimer.Enabled)
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

            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            Size = new Size(200, 130);
            BreathsPerMinute = 5.5;
            LineWidth = 15;
            BallDiameter = 25;
            BallColor = Color.Yellow;
            LineColor = Color.DeepSkyBlue;
            InhalePrecentage = 50;
            exhalePrecentage = 100 - InhalePrecentage;
            lblBpm.Text = breathsPerMinute + " bpm";
            pbBall.Left = 0;
            pbBall.Top = pbPacer.Height - pbBall.Height;
            Running = false;
            renderingTimer.Interval = 10;
        }

        public void Start()
        {
            peakX = (int)(pbPacer.Width * InhalePrecentage / 100);
            setStartPosition();
            totalTime = minuteInMs / BreathsPerMinute * InhalePrecentage / 100;
            ElapsedCycleCount = 0;
            stopWatch.Start();
            Running = true;
            renderingTimer.Start();

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

        private void setStartPosition()
        {
            x = x1 = -pbBall.Size.Width / 2;
            y = y1 = pbPacer.Height - pbBall.Height / 2;
            x2 = (int)(pbPacer.Width * InhalePrecentage / 100) - pbBall.Width / 2;
            y2 = -pbBall.Width / 2;

            pbBall.Left = 0;
            pbBall.Top = pbPacer.Height;
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

        private void renderingTimer_Tick(object sender, EventArgs e)
        {
            if (x < peakX && y > -pbBall.Width / 2)
            {
                move();
            }
            else if (x < pbPacer.Width && y < pbPacer.Height)
            {
                if (!directionSwitched)
                {
                    x1 = x;
                    x2 = pbPacer.Width;
                    y1 = y;
                    y2 = pbPacer.Height;
                    upTime = stopWatch.Elapsed.TotalSeconds;
                    stopWatch.Restart();
                    totalTime = minuteInMs / BreathsPerMinute * exhalePrecentage / 100.0;
                    directionSwitched = true;
                }
                move();
            }
            else //cycle elapsed
            {
                directionSwitched = false;
                stopWatch.Stop();
                if (TestMode)
                {
                    lblBpm.Left = 2; 

                    lblBpm.Text = string.Format("{0} bpm\nmeasured time: {1}\nmeasured bpm: {2}\nmeasured nuptime:{3}\nmeasured downtime:{4}",
                    new object[] {
                    BreathsPerMinute,
                    upTime + stopWatch.Elapsed.TotalSeconds,
                    60 / (upTime + stopWatch.Elapsed.TotalSeconds),
                    upTime,
                    stopWatch.Elapsed.TotalSeconds });
                }

                OnCycleElapsedEvent(new PacerEventArgs(++ElapsedCycleCount));

                if (BreathSettingsCollection != null && BreathSettingsCollection[breathSettingsIndex].CyclesToFinish == ElapsedCycleCount)
                {
                    if (breathSettingsIndex < BreathSettingsCollection.Count - 1)
                    {
                        breathSettingsIndex++;
                        BreathsPerMinute = BreathSettingsCollection[breathSettingsIndex].BreathsPerMinute;
                        CyclesToFinish = BreathSettingsCollection[breathSettingsIndex].CyclesToFinish;
                        Start();
                    }
                    else
                    {
                        Stop();
                        OnRoutineFinished(EventArgs.Empty);
                    }
                }

                if (cyclesToOpenClose > 0 && ElapsedCycleCount == cyclesToOpenClose)
                {
                    if (formToOpen != null)
                    {
                         formToOpen.Show();
                    }

                        ParentForm.Close();
                }

                setStartPosition();
                totalTime = minuteInMs / BreathsPerMinute * InhalePrecentage / 100;

                stopWatch.Restart();
            }

            pbBall.Left = x; 
            pbBall.Top = y; 
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

        protected virtual void OnCycleElapsedEvent(PacerEventArgs e)
        {
            EventHandler<PacerEventArgs> handler = CycleElapsed;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnRoutineFinished(EventArgs e)
        {
            EventHandler handler = RoutineFinished;

            if (handler != null)
            {
                handler(this, e);
            }
        }
        private void pnlPacer_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            GraphicsPath gp = new GraphicsPath();
            gp.AddLines(new Point[] {
                new Point(0, pbPacer.Height),
                new Point(pbPacer.Width * InhalePrecentage / 100, 0),
                new Point(pbPacer.Width, pbPacer.Height),
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
            Running = false;
            renderingTimer.Stop();
        }

        private void move()
        {
            double delta = stopWatch.ElapsedMilliseconds / totalTime;
            x = x1 + (int)((x2 - x1) * delta);
            y = y1 + (int)((y2 - y1) * delta);
        }
    }
    #region Additional classes
    public class PacerEventArgs : EventArgs
    {
        public PacerEventArgs(int cycleCount)
        {
            ElapsedCycleCount = cycleCount;
        }

        public int ElapsedCycleCount { get; set; }
    }

    #endregion
}
