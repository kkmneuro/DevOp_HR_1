using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuroXChange.Model;
using NeuroXChange.Model.Training;
using NeuroXChange.Model.FixApi;

namespace NeuroXChange.Controller
{
    public class MainNeuroXController
    {
        private MainNeuroXModel model;

        public MainNeuroXController(MainNeuroXModel model)
        {
            this.model = model;
        }

        public void WriteUserAction(UserAction action, UserActionDetail detail = UserActionDetail.NoDetail)
        {
            model.localDatabaseConnector.WriteUserAction(action, detail);
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
            model.TrainingType = trainingType;
        }

        public void SetTrainingStep(int trainingStep)
        {
            model.TrainingStep = trainingStep;
        }

        public void PauseTraining()
        {
            model.ApplicationStates |= ApplicationState.LiveModePaused;
        }

        public void ResumeTraining()
        {
            model.ApplicationStates &= ~ApplicationState.LiveModePaused;
        }

        public bool ManualTrade(
            int direction, TickPrice price, double takeProfit, double stopLoss)
        {
#if !SIMPLEST
            // TODO: generalise manual trade model number
            return model.behavioralModelsContainer.behavioralModels[16].ManualTrade(
                direction, price, takeProfit, stopLoss);
#else
            return true;
#endif
        }
    }
}
