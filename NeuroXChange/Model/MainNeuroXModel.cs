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

namespace NeuroXChange.Model
{
    public class MainNeuroXModel : IBioDataObserver
    {
        private string settingsFileName = "NeuroXChangeSettings.ini";

        public bool emulationOnHistory { get; private set; }
        public bool isStateGood { get; private set; }

        private List<IMainNeuroXModelObserver> observers = new List<IMainNeuroXModelObserver>();
        public AbstractBioDataProvider bioDataProvider { get; private set; }
        public AbstractFixApiModel fixApiModel;
        public IniFileReader iniFileReader { get; private set; }

        // Behavioral Models
        public BehavioralModelsContainer behavioralModelsContainer { get; private set; }

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

                emulationOnHistory = Boolean.Parse(iniFileReader.Read("UseEmulationOnHistory", "EmulationOnHistory"));

                if (!emulationOnHistory)
                {
                    bioDataProvider = new RealTimeMSAccessBioDataProvider(iniFileReader);
                    //bioDataProvider = new RandomBioDataProvider();
                    //bioDataProvider = new UdpBioDataProvider(14321);
                }
                else
                {
                    bioDataProvider = new EmulationOnHistoryBioDataProvider(iniFileReader);
                }
                bioDataProvider.RegisterObserver(this);

                if (!emulationOnHistory)
                {
                    fixApiModel = new FixApiModel(iniFileReader);
                }
                else
                {
                    fixApiModel = new EmulationOnHistoryFixApiModel();
                }
                bioDataProvider.RegisterObserver(fixApiModel);

                // initialization of behavioral models
                behavioralModelsContainer = new BehavioralModelsContainer(iniFileReader);
                //bioDataProvider.RegisterObserver(behavioralModelsContainer);
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

        // ---- IBioDataObserver implementation
        public void OnNext(BioData.BioData data)
        {
            behavioralModelsContainer.OnNext(data);

            var activeModel = getActiveBehavioralModel();
            if (activeModel.PreviousTickState != activeModel.CurrentTickState)
            {
                NotifyObservers(MainNeuroXModelEvent.AvtiveModelStateChanged, null);

                if (activeModel.CurrentTickState == BehavioralModelState.DirectionConfirmed)
                {
                    NotifyObservers(MainNeuroXModelEvent.LogicQueryDirection, behavioralModelsContainer.logicQuery1Condition.lastNot74SubProtocolID);
                }
            }
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
