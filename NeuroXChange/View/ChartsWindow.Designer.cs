namespace NeuroXChange.View
{
    partial class ChartsWindow
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.heartRateChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.heartRateChart)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // heartRateChart
            // 
            chartArea1.AxisX.IsStartedFromZero = false;
            chartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisY.IsStartedFromZero = false;
            chartArea1.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisY.Title = "Temperature";
            chartArea1.InnerPlotPosition.Auto = false;
            chartArea1.InnerPlotPosition.Height = 80F;
            chartArea1.InnerPlotPosition.Width = 88.36893F;
            chartArea1.InnerPlotPosition.X = 10.46062F;
            chartArea1.InnerPlotPosition.Y = 2F;
            chartArea1.Name = "ChartArea1";
            chartArea1.Position.Auto = false;
            chartArea1.Position.Height = 32F;
            chartArea1.Position.Width = 99F;
            chartArea1.Position.Y = 1F;
            chartArea2.AlignWithChartArea = "ChartArea1";
            chartArea2.AxisX.IsStartedFromZero = false;
            chartArea2.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea2.AxisY.IsStartedFromZero = false;
            chartArea2.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea2.AxisY.Title = "Heart Rate";
            chartArea2.Name = "ChartArea2";
            chartArea2.Position.Auto = false;
            chartArea2.Position.Height = 32F;
            chartArea2.Position.Width = 99F;
            chartArea2.Position.Y = 33F;
            chartArea3.AlignWithChartArea = "ChartArea1";
            chartArea3.AxisX.IsStartedFromZero = false;
            chartArea3.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea3.AxisY.IsStartedFromZero = false;
            chartArea3.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea3.AxisY.Title = "Conductance";
            chartArea3.Name = "ChartArea3";
            chartArea3.Position.Auto = false;
            chartArea3.Position.Height = 32F;
            chartArea3.Position.Width = 99F;
            chartArea3.Position.Y = 64F;
            this.heartRateChart.ChartAreas.Add(chartArea1);
            this.heartRateChart.ChartAreas.Add(chartArea2);
            this.heartRateChart.ChartAreas.Add(chartArea3);
            this.heartRateChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.heartRateChart.Location = new System.Drawing.Point(0, 0);
            this.heartRateChart.Name = "heartRateChart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Name = "Temperature";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series2.ChartArea = "ChartArea2";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.Color = System.Drawing.Color.Red;
            series2.Name = "Heart Rate";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            series2.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series3.ChartArea = "ChartArea2";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series3.Name = "AVG Heart Rate";
            series3.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            series3.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series4.ChartArea = "ChartArea3";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series4.Name = "Skin Conductance";
            series4.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            series4.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.heartRateChart.Series.Add(series1);
            this.heartRateChart.Series.Add(series2);
            this.heartRateChart.Series.Add(series3);
            this.heartRateChart.Series.Add(series4);
            this.heartRateChart.Size = new System.Drawing.Size(598, 359);
            this.heartRateChart.TabIndex = 5;
            this.heartRateChart.Text = "chart1";
            this.heartRateChart.Click += new System.EventHandler(this.heartRateChart_Click);
            this.heartRateChart.MouseLeave += new System.EventHandler(this.heartRateChart_MouseLeave);
            this.heartRateChart.MouseMove += new System.Windows.Forms.MouseEventHandler(this.heartRateChart_MouseMove);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 337);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(598, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(60, 17);
            this.toolStripStatusLabel.Text = "some hint";
            // 
            // ChartsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 359);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.heartRateChart);
            this.MinimizeBox = false;
            this.Name = "ChartsWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Charts";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChartsWindow_FormClosing);
            this.VisibleChanged += new System.EventHandler(this.ChartsWindow_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.heartRateChart)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataVisualization.Charting.Chart heartRateChart;
        public System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
    }
}