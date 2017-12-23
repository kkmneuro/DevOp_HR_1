using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuroXChange.Model;
using NeuroXChange.Model.Training;

namespace NeuroXChange.Controller
{
    public class MainNeuroXController
    {
        private MainNeuroXModel model;

        public MainNeuroXController(MainNeuroXModel model)
        {
            this.model = model;
        }

        public void ChangeActiveModel(int modelInd)
        {
            model.setActiveBehavioralModelIndex(modelInd);
        }


        // Actions from Emulation Mode Control Window
        public void StartEmulation()
        {
            if (model.emulationOnHistoryMode)
            {
                model.StartEmulation();
            }
        }

        public void PauseEmulation()
        {
            if (model.emulationOnHistoryMode)
            {
                model.PauseEmulation();
            }
        }

        public void NextTickEmulation()
        {
            if (model.emulationOnHistoryMode)
            {
                model.NextTickEmulation();
            }
        }

        public void ChangeEmulationModeTickInterval(int tickInterval)
        {
            model.ChangeEmulationModeTickInterval(tickInterval);
        }


        // controlling training windows
        public void SetTrainingType(TrainingType trainingType)
        {
            model.SetTrainingType(trainingType);
        }

        public void SetTrainingStep(int trainingStep)
        {
            model.SetTrainingStep(trainingStep);
        }
    }
}
