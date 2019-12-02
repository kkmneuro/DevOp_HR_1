using BreathPacer;
using NeuroXChange.Common;
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

            controller.SetTrainingType(TrainingType.CompDay);
            controller.WriteUserAction(UserAction.TrainingStarted, UserActionDetail.TrainingCompDay);

            view.breathPacerWindow.breathPacerControl.CycleElapsed += bpCycleFinishedHandler;
            view.breathPacerWindow.breathPacerControl.Restart();
            currentStep = -1;
            lastCyclesToFinish = 0;
            textLabel.Text = string.Empty;
            pictureBox.ImageLocation = null;
            GetNextCycle(null, null);
        }

        public void StopCompDay(bool manualStop)
        {
            if (!isRunning)
            {
                return;
            }
            isRunning = false;

            controller.SetTrainingType(TrainingType.NoTraining);
            controller.WriteUserAction(manualStop ? UserAction.TrainingManuallyStopped : UserAction.TrainingFinished);

            view.breathPacerWindow.breathPacerControl.CycleElapsed -= bpCycleFinishedHandler;
            textLabel.Text = string.Empty;
            pictureBox.ImageLocation = null;

            // make start button available
            pauseButton.Enabled = false;
            startButton.Enabled = true;
        }

        private void GetNextCycle(object sender, PacerEventArgs e)
        {
            if (view.breathPacerWindow.breathPacerControl.ElapsedCycleCount < lastCyclesToFinish)
            {
                return;
            }

            if (currentStep >= stepsData.Count -1)
            {
                StopCompDay(false);
                return;
            }

            currentStep++;
            view.breathPacerWindow.breathPacerControl.ElapsedCycleCount = 0;

            var trainingStep = int.Parse(stepsData[currentStep][0]);
            var type = stepsData[currentStep][1];
            var text = stepsData[currentStep][2];
            var imagePath = stepsData[currentStep][3];
            double breathsPerMinute = StringHelpers.ParseDoubleCultureIndependent(stepsData[currentStep][4]);
            lastCyclesToFinish = int.Parse(stepsData[currentStep][5]);

            controller.SetTrainingStep(trainingStep);

            ShowContent(type, text, imagePath);
        }

        private void ShowContent(string type, string text, string imagePath)
        {
            textLabel.Text = text;

            if (!string.IsNullOrEmpty(imagePath))
            {
                pictureBox.ImageLocation = imagePath;
                pictureBox.Load();
            }
            else
            {
                pictureBox.ImageLocation = null;
            }

            if (type == "Selection_Manual_Position_Chose")
            {
                selectManualPosChoseTLP1.Visible = true;
            }
            else
            {
                selectManualPosChoseTLP1.Visible = false;
            }
        }

        private void CompDayWindow_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                startButton_Click(null, null);
            }
            else
            {
                StopCompDay(true);
                view.breathPacerWindow.breathPacerControl.Continue();
                controller.ResumeTraining();
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            bool newRun;
            if (!isRunning)
            {
                StartCompDay();
                newRun = true;
            }
            else
            {
                newRun = false;
            }

            view.breathPacerWindow.breathPacerControl.Continue();
            pauseButton.Enabled = true;
            startButton.Enabled = false;
            controller.ResumeTraining();

            if (!newRun)
            {
                controller.WriteUserAction(UserAction.TrainingResumed);
            }
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            view.breathPacerWindow.breathPacerControl.Stop();
            pauseButton.Enabled = false;
            startButton.Enabled = true;
            controller.PauseTraining();
            controller.WriteUserAction(UserAction.TrainingPaused);
        }

        private void variantBtnClicked(object sender, EventArgs e)
        {
            UserActionDetail detail = UserActionDetail.NoDetail;
            if (sender == variant1btn)
                detail = UserActionDetail.ManualPositionMLS1;
            else if (sender == variant2btn)
                detail = UserActionDetail.ManualPositionMLS2;
            else if (sender == variant3btn)
                detail = UserActionDetail.ManualPositionMSL1;
            else if (sender == variant4btn)
                detail = UserActionDetail.ManualPositionMSL2;
            else if (sender == variant5btn)
                detail = UserActionDetail.ManualPositionSingularLong;
            else if (sender == variant6btn)
                detail = UserActionDetail.ManualPositionSingularShort;

            controller.WriteUserAction(UserAction.ManualPositionChosen, detail);

            view.breathPacerWindow.breathPacerControl.ElapsedCycleCount = lastCyclesToFinish;
            GetNextCycle(null, null);
        }
    }
}
