using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace BreathPacer
{
    public partial class BreathPacerControl : UserControl
    {
        private double totalTime;

        private double x;
                
        private double y;
                
        private double x1;
                
        private double x2;
                
        private double y1;
                
        private double y2;

        private bool directionSwitched = false;

        private int inhalePrecentage;

        private int exhalePrecentage;

        private double breathsPerMinute;

        private int lineWidth;

        private Brush ballBrush;

        private Color lineColor;

        private Form formToOpen;

        private int cyclesToOpenClose;

        private int breathSettingsIndex;

        private double upTime;

        private double peakX;

        private int minuteInMs = 59640;

        private Stopwatch stopWatch = new Stopwatch();

        private PictureBox pbPacer;

        private Timer renderingTimer;

        [method: CompilerGenerated]
        //[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        public event EventHandler<PacerEventArgs> CycleElapsed;

        [method: CompilerGenerated]
        //[DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        public event EventHandler RoutineFinished;

        public class PacerEventArgs : EventArgs
        {
            public int ElapsedCycleCount
            {
                get;
                set;
            }

            public PacerEventArgs(int cycleCount)
            {
                ElapsedCycleCount = cycleCount;
            }
        }

        public bool TestMode
        {
            get;
            set;
        }

        public List<BreathSettings> BreathSettingsCollection
        {
            get;
            private set;
        }

        public bool Running
        {
            get;
            private set;
        }

        public int ElapsedCycleCount
        {
            get;
            set;
        }

        public Label BpmLabel
        {
            get
            {
                return lblBpm;
            }
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
                bool enabled = renderingTimer.Enabled;
                if (enabled)
                {
                    Start();
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
                Control arg_17_0 = pbBall;
                pbBall.Height = value;
                arg_17_0.Width = value;
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

        public object CyclesToFinish
        {
            get;
            private set;
        }

        public BreathPacerControl()
        {
            InitializeComponent();
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.Size = new Size(200, 130);
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

            setStartPosition();
        }

        public void Start()
        {
            directionSwitched = false;
            peakX = InhalePrecentage / 100.0;
            setStartPosition();
            totalTime = (double)minuteInMs / BreathsPerMinute * (double)InhalePrecentage / 100.0;
            ElapsedCycleCount = 0;
            stopWatch.Start();
            Running = true;
            renderingTimer.Start();
        }

        public void Start(List<BreathSettings> breathSettings)
        {
            BreathSettingsCollection = breathSettings;
            breathSettingsIndex = 0;
            bool flag = breathSettings != null && breathSettings.Count > 0;
            if (flag)
            {
                BreathsPerMinute = breathSettings[breathSettingsIndex].BreathsPerMinute;
                CyclesToFinish = breathSettings[breathSettingsIndex].CyclesToFinish;
                Start();
            }
        }

        public void Start(Form formToOpen, int cycleCount)
        {
            this.formToOpen = formToOpen;
            cyclesToOpenClose = cycleCount;
            Start();
        }

        public void Restart()
        {
            stopWatch.Restart();
            Start();
        }

        private void setStartPosition()
        {
            x = x1 = 0;
            y = y1 = 1.0;
            x2 = InhalePrecentage / 100.0;
            y2 = 0;
            pbBall.Left = 0;
            pbBall.Top = pbPacer.Height;

            pbBall.Left = (int)(x * Width) - pbBall.Width / 2;
            pbBall.Top = (int)(y * Height) - pbBall.Height / 2;
        }

        public void StartResonantBTest()
        {
            List<BreathSettings> breathSettings = new List<BreathSettings>();
            for (double bpm = 4.0; bpm < 8.75; bpm += 0.25)
            {
                breathSettings.Add(new BreathSettings
                {
                    BreathsPerMinute = bpm,
                    CyclesToFinish = 5
                });
            }
            Start(breathSettings);
        }

        private void renderingTimer_Tick(object sender, EventArgs e)
        {
            bool flag = x < peakX;
            if (flag)
            {
                move();
            }
            else
            {
                bool flag2 = x < 1.0;
                if (flag2)
                {
                    bool flag3 = !directionSwitched;
                    if (flag3)
                    {
                        x1 = x;
                        x2 = 1.0;
                        y1 = y;
                        y2 = 1.0;
                        upTime = stopWatch.Elapsed.TotalSeconds;
                        stopWatch.Restart();
                        totalTime = (double)minuteInMs / BreathsPerMinute * (double)exhalePrecentage / 100.0;
                        directionSwitched = true;
                    }
                    move();
                }
                else
                {
                    directionSwitched = false;
                    stopWatch.Stop();
                    bool testMode = TestMode;
                    if (testMode)
                    {
                        lblBpm.Left = 2;
                        lblBpm.Text = string.Format("{0} bpm\nmeasured time: {1}\nmeasured bpm: {2}\nmeasured nuptime:{3}\nmeasured downtime:{4}", new object[]
                        {
                            BreathsPerMinute,
                            upTime + stopWatch.Elapsed.TotalSeconds,
                            60.0 / (upTime + stopWatch.Elapsed.TotalSeconds),
                            upTime,
                            stopWatch.Elapsed.TotalSeconds
                        });
                    }
                    int num = ElapsedCycleCount + 1;
                    ElapsedCycleCount = num;
                    OnCycleElapsedEvent(new PacerEventArgs(num));
                    bool flag4 = BreathSettingsCollection != null && BreathSettingsCollection[breathSettingsIndex].CyclesToFinish == ElapsedCycleCount;
                    if (flag4)
                    {
                        bool flag5 = breathSettingsIndex < BreathSettingsCollection.Count - 1;
                        if (flag5)
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
                    bool flag6 = cyclesToOpenClose > 0 && ElapsedCycleCount == cyclesToOpenClose;
                    if (flag6)
                    {
                        bool flag7 = formToOpen != null;
                        if (flag7)
                        {
                            formToOpen.Show();
                        }
                        base.ParentForm.Close();
                    }
                    setStartPosition();
                    totalTime = (double)minuteInMs / BreathsPerMinute * (double)InhalePrecentage / 100.0;
                    stopWatch.Restart();
                }
            }

            pbBall.Left = (int)(x * Width) - pbBall.Width / 2;
            pbBall.Top = (int)(y * Height) - pbBall.Height / 2;
        }

        public void StartResiliancyBTest()
        {
            List<BreathSettings> breathSettings = new List<BreathSettings>
            {
                new BreathSettings
                {
                    BreathsPerMinute = 4.0,
                    CyclesToFinish = 5
                },
                new BreathSettings
                {
                    BreathsPerMinute = 7.25,
                    CyclesToFinish = 3
                },
                new BreathSettings
                {
                    BreathsPerMinute = 6.0,
                    CyclesToFinish = 2
                },
                new BreathSettings
                {
                    BreathsPerMinute = 4.0,
                    CyclesToFinish = 1
                },
                new BreathSettings
                {
                    BreathsPerMinute = 5.0,
                    CyclesToFinish = 5
                },
                new BreathSettings
                {
                    BreathsPerMinute = 7.5,
                    CyclesToFinish = 2
                },
                new BreathSettings
                {
                    BreathsPerMinute = 5.25,
                    CyclesToFinish = 1
                },
                new BreathSettings
                {
                    BreathsPerMinute = 3.0,
                    CyclesToFinish = 4
                },
                new BreathSettings
                {
                    BreathsPerMinute = 6.0,
                    CyclesToFinish = 2
                },
                new BreathSettings
                {
                    BreathsPerMinute = 3.25,
                    CyclesToFinish = 4
                },
                new BreathSettings
                {
                    BreathsPerMinute = 6.25,
                    CyclesToFinish = 4
                },
                new BreathSettings
                {
                    BreathsPerMinute = 7.0,
                    CyclesToFinish = 3
                },
                new BreathSettings
                {
                    BreathsPerMinute = 7.25,
                    CyclesToFinish = 5
                },
                new BreathSettings
                {
                    BreathsPerMinute = 5.0,
                    CyclesToFinish = 5
                },
                new BreathSettings
                {
                    BreathsPerMinute = 5.5,
                    CyclesToFinish = 2
                }
            };
            Start(breathSettings);
        }

        protected virtual void OnCycleElapsedEvent(PacerEventArgs e)
        {
            EventHandler<PacerEventArgs> handler = CycleElapsed;
            bool flag = handler != null;
            if (flag)
            {
                handler(this, e);
            }
        }

        protected virtual void OnRoutineFinished(EventArgs e)
        {
            EventHandler handler = RoutineFinished;
            bool flag = handler != null;
            if (flag)
            {
                handler(this, e);
            }
        }

        private void pnlPacer_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            GraphicsPath gp = new GraphicsPath();
            gp.AddLines(new Point[]
            {
                new Point(0, pbPacer.Height),
                new Point(pbPacer.Width * InhalePrecentage / 100, 0),
                new Point(pbPacer.Width, pbPacer.Height)
            });
            e.Graphics.DrawPath(new Pen(LineColor, (float)LineWidth), gp);
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
            stopWatch.Stop();
        }

        public void Continue()
        {
            Running = true;
            renderingTimer.Start();
            stopWatch.Start();
        }

        private void move()
        {
            double delta = (double)(stopWatch.ElapsedMilliseconds) / totalTime;
            x = x1 + (double)(x2 - x1) * delta;
            y = y1 + (double)(y2 - y1) * delta;
        }

        private void BreathPacerControl_Resize(object sender, EventArgs e)
        {
            pbBall.Left = (int)(x * Width) - pbBall.Width / 2;
            pbBall.Top = (int)(y * Height) - pbBall.Height / 2;
        }
    }
}
