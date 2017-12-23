using BreathPacer;
using NeuroXChange.Controller;
using NeuroXChange.Model;
using NeuroXChange.Model.Training;
using System;
using System.Collections.Generic;
using static BreathPacer.BreathPacerControl;

namespace NeuroXChange.View.Training
{
    public partial class CompDayWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private MainNeuroXController controller;
        private MainNeuroXView view;

        EventHandler<PacerEventArgs> bpCycleFinishedHandler;

        // steps
        private bool isRunning = false;
        private List<string[]> stepsData;
        private int currentStep = -1;
        private int lastCyclesToFinish = 0;

        public CompDayWindow(MainNeuroXController controller, MainNeuroXView view)
        {
            InitializeComponent();

            this.controller = controller;
            this.view = view;

            bpCycleFinishedHandler = new EventHandler<PacerEventArgs>(GetNextCycle);

            // load steps
            stepsData = new List<string[]>();
            System.IO.StreamReader file = new System.IO.StreamReader(@"Data\Training\CompDay.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line) || line[0] == ';')
                {
                    continue;
                }

                stepsData.Add(line.Split('\t'));
            }
            file.Close();
        }

        public void StartCompDay()
        {
            if (isRunning)
            {
                return;
            }
            isRunning = true;

            controller.SetTraining(TrainingType.CompDay);

            view.breathPacerWindow.breathPacerControl.CycleElapsed += bpCycleFinishedHandler;
            currentStep = -1;
            lastCyclesToFinish = 0;
            textLabel.Text = string.Empty;
            pictureBox.ImageLocation = null;
            GetNextCycle(null, null);
        }

        public void StopCompDay()
        {
            if (!isRunning)
            {
                return;
            }
            isRunning = false;

            controller.SetTraining(TrainingType.NoTraining);

            view.breathPacerWindow.breathPacerControl.CycleElapsed -= bpCycleFinishedHandler;
            textLabel.Text = string.Empty;
            pictureBox.ImageLocation = null;

            pauseButton_Click(null, null);
        }

        private void GetNextCycle(object sender, PacerEventArgs e)
        {
            if (view.breathPacerWindow.breathPacerControl.ElapsedCycleCount < lastCyclesToFinish)
            {
                return;
            }

            if (currentStep >= stepsData.Count -1)
            {
                StopCompDay();
                return;
            }

            currentStep++;
            view.breathPacerWindow.breathPacerControl.ElapsedCycleCount = 0;

            var sub_Protocol_ID = int.Parse(stepsData[currentStep][0]);
            var type = stepsData[currentStep][1];
            var text = stepsData[currentStep][2];
            var imagePath = stepsData[currentStep][3];
            double breathsPerMinute = double.Parse(stepsData[currentStep][4]);
            lastCyclesToFinish = int.Parse(stepsData[currentStep][5]);

            controller.SetTrainingSubProtocolId(sub_Protocol_ID);

            ShowContent(type, text, imagePath);
        }

        private void ShowContent(string type, string text, string imagePath)
        {
            textLabel.Text = text;

            if (!string.IsNullOrEmpty(imagePath))
            {
                pictureBox.ImageLocation = imagePath;
                pictureBox.Load();
            } else
            {
                pictureBox.ImageLocation = null;
            }
        }

        private void CompDayWindow_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                StartCompDay();
                startButton_Click(null, null);
            }
            else
            {
                StopCompDay();
                view.breathPacerWindow.breathPacerControl.Continue();
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                StartCompDay();
            }

            view.breathPacerWindow.breathPacerControl.Continue();
            pauseButton.Enabled = true;
            startButton.Enabled = false;
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            view.breathPacerWindow.breathPacerControl.Stop();
            pauseButton.Enabled = false;
            startButton.Enabled = true;
        }
    }
}
