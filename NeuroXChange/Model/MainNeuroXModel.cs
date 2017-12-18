using System;
using System.Collections.Generic;
using System.Linq;
using NeuroXChange.Model.BioData;
using System.Windows.Forms;
using NeuroXChange.Common;
using System.IO;
using NeuroXChange.Model.FixApi;
using NeuroXChange.Model.BehavioralModeling;
using NeuroXChange.Model.BehavioralModeling.BioDataProcessors;
using NeuroXChange.Model.BehavioralModeling.BehavioralModels;
using NeuroXChange.Model.BehavioralModeling.BehavioralModelCondition;
using NeuroXChange.Model.Training;

namespace NeuroXChange.Model
{
    public class MainNeuroXModel : IBioDataObserver, IFixApiObserver
    {
        private string settingsFileName = "NeuroXChangeSettings.ini";

        public bool emulationOnHistoryMode { get; private set; }
        public bool isStateGood { get; private set; }

        private List<IMainNeuroXModelObserver> observers = new List<IMainNeuroXModelObserver>();
        public AbstractBioDataProvider bioDataProvider { get; private set; }
        public AbstractFixApiModel fixApiModel;
        public IniFileReader iniFileReader { get; private set; }

        // Behavioral Models
        public BehavioralModelsContainer behavioralModelsContainer { get; private set; }

        // Query condition only for showing popup message purpose
        // TODO: need to move this to anoter place or to remove completely
        private LogicQuery1Condition logicQuery1Condition;

        public SimpleBehavioralModel getActiveBehavioralModel()
        {
            return behavioralModelsContainer.behavioralModels[
                behavioralModelsContainer.ActiveBehavioralModelIndex];
        }

        public void setActiveBehavioralModelIndex(int modelInd)
        {
            if (behavioralModelsContainer.ActiveBehavioralModelIndex == modelInd)
            {
                return;
            }
            behavioralModelsContainer.ActiveBehavioralModelIndex = modelInd;
            NotifyObservers(MainNeuroXModelEvent.ActiveModelChanged, null);
        }

        public MainNeuroXModel()
        {
            try
            {
                isStateGood = true;

                if (!File.Exists(settingsFileName))
                {
                    throw new Exception(
                        string.Format("No configuration file \"{0}\" found!\nHint: place \"{0}\" in the same folder as this exe file", settingsFileName)
                        );
                }

                iniFileReader = new IniFileReader(settingsFileName);

                emulationOnHistoryMode = Boolean.Parse(iniFileReader.Read("UseEmulationOnHistory", "EmulationOnHistory"));

                if (!emulationOnHistoryMode)
                {
                    bioDataProvider = new TTLApiBioDataProvider(iniFileReader);
                    //bioDataProvider = new RealTimeMSAccessBioDataProvider(iniFileReader);
                    //bioDataProvider = new RandomBioDataProvider();
                    //bioDataProvider = new UdpBioDataProvider(14321);
                }
                else
                {
                    bioDataProvider = new EmulationOnHistoryBioDataProvider(iniFileReader);
                }
                bioDataProvider.RegisterObserver(this);

                if (!emulationOnHistoryMode)
                {
                    fixApiModel = new FixApiModel(iniFileReader);
                }
                else
                {
                    fixApiModel = new EmulationOnHistoryFixApiModel();
                }
                bioDataProvider.RegisterObserver(fixApiModel);
                fixApiModel.RegisterObserver(this);

                // initialization of behavioral models
                behavioralModelsContainer = new BehavioralModelsContainer(iniFileReader);
                logicQuery1Condition = new LogicQuery1Condition(100, 60);
            }
            catch (Exception e)
            {
                isStateGood = false;
                StopProcessing(null, null);
                MessageBox.Show(e.Message, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void StartProcessing()
        {
            bioDataProvider.StartProcessing();
            fixApiModel.StartProcessing();
        }

        /// <summary>
        /// Stop any processing. Called before application closing
        /// </summary>
        public void StopProcessing(object sender, EventArgs e)
        {
            if (bioDataProvider != null)
                bioDataProvider.StopProcessing();
            if (fixApiModel != null)
                fixApiModel.StopProcessing();
        }


        // Emulation on history mode control
        public void StartEmulation()
        {
            if (!emulationOnHistoryMode)
            {
                return;
            }
            var emulationOnHistoryProvider = (EmulationOnHistoryBioDataProvider)bioDataProvider;
            emulationOnHistoryProvider.StartEmulation();
        }

        public void PauseEmulation()
        {
            if (!emulationOnHistoryMode)
            {
                return;
            }
            var emulationOnHistoryProvider = (EmulationOnHistoryBioDataProvider)bioDataProvider;
            emulationOnHistoryProvider.PauseEmulation();
        }

        public void NextTickEmulation()
        {
            if (!emulationOnHistoryMode)
            {
                return;
            }
            var emulationOnHistoryProvider = (EmulationOnHistoryBioDataProvider)bioDataProvider;
            emulationOnHistoryProvider.NextTickEmulation();
        }

        public void ChangeEmulationModeTickInterval(int tickInterval)
        {
            if (!emulationOnHistoryMode)
            {
                return;
            }
            var emulationOnHistoryProvider = (EmulationOnHistoryBioDataProvider)bioDataProvider;
            emulationOnHistoryProvider.ChangeEmulationModeTickInterval(tickInterval);
        }


        // controlling training windows
        public void SetTraining(TrainingType trainingType)
        {
            if (emulationOnHistoryMode)
            {
                return;
            }

            var provider = (TTLApiBioDataProvider)bioDataProvider;
            provider.Sub_Component_Protocol_ID = (int)trainingType;

            if (trainingType == TrainingType.NoTraining)
            {
                provider.Sub_Protocol_ID = 0;
            }
        }

        public void SetTrainingSubProtocolId(int id)
        {
            if (emulationOnHistoryMode)
            {
                return;
            }
            ((TTLApiBioDataProvider)bioDataProvider).Sub_Protocol_ID = (int)id;
        }


        // ---- IBioDataObserver implementation
        public void OnNext(BioData.BioDataEvent bioDataEvent, object data)
        {
            if (bioDataEvent != BioDataEvent.NewBioDataTick)
            {
                return;
            }

            behavioralModelsContainer.OnNext((BioData.BioData)data);

            var activeModel = getActiveBehavioralModel();
            if (activeModel.PreviousTickState != activeModel.CurrentTickState)
            {
                NotifyObservers(MainNeuroXModelEvent.ActiveModelStateChanged, null);

                // TODO: this is temporal code disabling. Need to understand do we need this functionality or not
                //if (activeModel.CurrentTickState == BehavioralModelState.DirectionConfirmed)
                //{
                //    NotifyObservers(MainNeuroXModelEvent.LogicQueryDirection, getActiveBehavioralModel().lastNot74SubProtocolID);
                //}
            }

            logicQuery1Condition.OnNext((BioData.BioData)data);
            if (logicQuery1Condition.isConditionMet)
            {
                NotifyObservers(MainNeuroXModelEvent.LogicQueryDirection, logicQuery1Condition.lastNot74SubProtocolID);
            }
        }

        // ---- IFixApiObserver implementation
        public void OnNext(FixApiModelEvent modelEvent, object data)
        {
            if (modelEvent != FixApiModelEvent.PriceChanged)
            {
                return;
            }

            behavioralModelsContainer.OnNext((TickPrice)data);
        }

        // ---- Observable pattern implementation
        public void RegisterObserver(IMainNeuroXModelObserver observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
        }

        public void RemoveObserver(IMainNeuroXModelObserver observer)
        {
            if (observers.Contains(observer))
                observers.Remove(observer);
        }

        private void NotifyObservers(MainNeuroXModelEvent modelEvent, object data)
        {
            foreach (var observer in observers)
                observer.OnNext(modelEvent, data);
        }

    }
}
