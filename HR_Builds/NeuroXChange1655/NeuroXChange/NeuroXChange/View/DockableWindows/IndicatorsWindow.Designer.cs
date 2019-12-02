namespace NeuroXChange.View
{
    partial class IndicatorsWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.peakPerformanceGauge = new AGaugeApp.AGauge();
            this.SuspendLayout();
            // 
            // peakPerformanceGauge
            // 
            this.peakPerformanceGauge.BaseArcColor = System.Drawing.Color.Gray;
            this.peakPerformanceGauge.BaseArcRadius = 60;
            this.peakPerformanceGauge.BaseArcStart = 180;
            this.peakPerformanceGauge.BaseArcSweep = 180;
            this.peakPerformanceGauge.BaseArcWidth = 2;
            this.peakPerformanceGauge.Cap_Idx = ((byte)(1));
            this.peakPerformanceGauge.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.peakPerformanceGauge.CapFont = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.peakPerformanceGauge.CapPosition = new System.Drawing.Point(65, 70);
            this.peakPerformanceGauge.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(81, 60),
        new System.Drawing.Point(65, 70),
        new System.Drawing.Point(120, 200),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.peakPerformanceGauge.CapsText = new string[] {
        "peak",
        "performance",
        "",
        "",
        ""};
            this.peakPerformanceGauge.CapText = "performance";
            this.peakPerformanceGauge.Center = new System.Drawing.Point(90, 90);
            this.peakPerformanceGauge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.peakPerformanceGauge.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.peakPerformanceGauge.Location = new System.Drawing.Point(0, 0);
            this.peakPerformanceGauge.MaxValue = 10F;
            this.peakPerformanceGauge.MinValue = 0F;
            this.peakPerformanceGauge.Name = "peakPerformanceGauge";
            this.peakPerformanceGauge.NeedleColor1 = AGaugeApp.AGauge.NeedleColorEnum.Yellow;
            this.peakPerformanceGauge.NeedleColor2 = System.Drawing.Color.DimGray;
            this.peakPerformanceGauge.NeedleRadius = 60;
            this.peakPerformanceGauge.NeedleType = 0;
            this.peakPerformanceGauge.NeedleWidth = 1;
            this.peakPerformanceGauge.Range_Idx = ((byte)(4));
            this.peakPerformanceGauge.RangeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(225)))), ((int)(((byte)(170)))));
            this.peakPerformanceGauge.RangeEnabled = true;
            this.peakPerformanceGauge.RangeEndValue = 5F;
            this.peakPerformanceGauge.RangeInnerRadius = 35;
            this.peakPerformanceGauge.RangeOuterRadius = 60;
            this.peakPerformanceGauge.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0))))),
        System.Drawing.Color.Yellow,
        System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(208))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(225)))), ((int)(((byte)(170))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(209)))), ((int)(((byte)(133))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(178)))), ((int)(((byte)(78))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(144)))), ((int)(((byte)(69))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(121)))), ((int)(((byte)(63))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(86)))), ((int)(((byte)(46)))))};
            this.peakPerformanceGauge.RangesEnabled = new bool[] {
        true,
        true,
        true,
        true,
        true,
        true,
        true,
        true,
        true,
        true};
            this.peakPerformanceGauge.RangesEndValue = new float[] {
        1F,
        2F,
        3F,
        4F,
        5F,
        6F,
        7F,
        8F,
        9F,
        10F};
            this.peakPerformanceGauge.RangesInnerRadius = new int[] {
        35,
        35,
        35,
        35,
        35,
        35,
        35,
        35,
        35,
        35};
            this.peakPerformanceGauge.RangesOuterRadius = new int[] {
        60,
        60,
        60,
        60,
        60,
        60,
        60,
        60,
        60,
        60};
            this.peakPerformanceGauge.RangesStartValue = new float[] {
        0F,
        1F,
        2F,
        3F,
        4F,
        5F,
        6F,
        7F,
        8F,
        9F};
            this.peakPerformanceGauge.RangeStartValue = 4F;
            this.peakPerformanceGauge.ScaleLinesInterColor = System.Drawing.Color.Black;
            this.peakPerformanceGauge.ScaleLinesInterInnerRadius = 60;
            this.peakPerformanceGauge.ScaleLinesInterOuterRadius = 65;
            this.peakPerformanceGauge.ScaleLinesInterWidth = 1;
            this.peakPerformanceGauge.ScaleLinesMajorColor = System.Drawing.Color.Black;
            this.peakPerformanceGauge.ScaleLinesMajorInnerRadius = 60;
            this.peakPerformanceGauge.ScaleLinesMajorOuterRadius = 72;
            this.peakPerformanceGauge.ScaleLinesMajorStepValue = 1F;
            this.peakPerformanceGauge.ScaleLinesMajorWidth = 3;
            this.peakPerformanceGauge.ScaleLinesMinorColor = System.Drawing.Color.Gray;
            this.peakPerformanceGauge.ScaleLinesMinorInnerRadius = 120;
            this.peakPerformanceGauge.ScaleLinesMinorNumOf = 1;
            this.peakPerformanceGauge.ScaleLinesMinorOuterRadius = 70;
            this.peakPerformanceGauge.ScaleLinesMinorWidth = 1;
            this.peakPerformanceGauge.ScaleNumbersColor = System.Drawing.Color.Black;
            this.peakPerformanceGauge.ScaleNumbersFormat = null;
            this.peakPerformanceGauge.ScaleNumbersRadius = 82;
            this.peakPerformanceGauge.ScaleNumbersRotation = 0;
            this.peakPerformanceGauge.ScaleNumbersStartScaleLine = 0;
            this.peakPerformanceGauge.ScaleNumbersStepScaleLines = 1;
            this.peakPerformanceGauge.Size = new System.Drawing.Size(207, 166);
            this.peakPerformanceGauge.TabIndex = 22;
            this.peakPerformanceGauge.Text = "aGauge13";
            this.peakPerformanceGauge.Value = 0.5F;
            this.peakPerformanceGauge.SizeChanged += new System.EventHandler(this.peakPerformanceGauge_SizeChanged);
            this.peakPerformanceGauge.Resize += new System.EventHandler(this.peakPerformanceGauge_Resize);
            // 
            // IndicatorsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(207, 166);
            this.Controls.Add(this.peakPerformanceGauge);
            this.HideOnClose = true;
            this.Name = "IndicatorsWindow";
            this.Text = "Indicators";
            this.ResumeLayout(false);

        }

        #endregion

        public AGaugeApp.AGauge peakPerformanceGauge;
    }
}