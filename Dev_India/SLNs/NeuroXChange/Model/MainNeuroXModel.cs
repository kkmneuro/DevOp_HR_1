using System;
using System.Collections.Generic;
using NeuroXChange.Model.BioData;
using System.Windows.Forms;
using NeuroXChange.Common;
using System.IO;
using NeuroXChange.Model.FixApi;
using NeuroXChange.Model.BehavioralModeling;
using NeuroXChange.Model.Training;
using NeuroXChange.Model.Database;
using NeuroXChange.Model.ServerConnection;
#if !SIMPLEST
using NeuroXChange.Model.BehavioralModeling.BehavioralModelCondition;
#endif

namespace NeuroXChange.Model
{
    public class MainNeuroXModel : IBioDataObserver, IFixApiObserver
    {
        private const string settingsFileName = "NeuroXChangeSettings.ini";

        // Was application loaded correctly or not
        public bool isStateGood { get; private set; }


        // unique objects for application
        public IniFileReader iniFileReader { get; private set; }
        public ServerConnector serverConnector { get; private set; }
        public LocalDatabaseConnector localDatabaseConnector { get; private set; }
        public AbstractBioDataProvider bioDataProvider { get; private set; }
        public AbstractFixApiModel fixApiModel { get; private set; }
        public BehavioralModelsContainer behavioralModelsContainer { get; private set; }

        public WsHelperOrders WsHelperOrders = WsHelperOrders.INSTANCE;
        public WsHelperQuotes WsHelperQuotes = WsHelperQuotes.INSTANCE;

        // ---- Observable pattern implementation
        private List<IMainNeuroXModelObserver> observers = new List<IMainNeuroXModelObserver>();

        // fields that determine state of application, but won't be logged
        public bool emulationOnHistoryMode { get; private set; }

        // application states that will be logged to database
        private Training.TrainingType trainingType;
        public Training.TrainingType TrainingType
        {
            get {
                return trainingType;
            }

            set {
                trainingType = value;

                if (trainingType == TrainingType.NoTraining)
                {
                    TrainingStep = 0;
                }
            }
        }
        public int TrainingStep { get; set; }
        public ApplicationState ApplicationStates { get; set; }

#if !SIMPLEST
        // Query condition only for showing popup message purpose
        // TODO: need to move this to anoter place or to remove completely
        private LogicQuery1Condition logicQuery1Condition;


        public SimpleBehavioralModel getActiveBehavioralModel()
        {
            return behavioralModelsContainer.behavioralModels[
                behavioralModelsContainer.ActiveBehavioralModelIndex];
        }
#endif

        public void setActiveBehavioralModelIndex(int modelInd)
        {
#if !SIMPLEST
            if (behavioralModelsContainer.ActiveBehavioralModelIndex == modelInd)
            {
                return;
            }
            behavioralModelsContainer.ActiveBehavioralModelIndex = modelInd;
            NotifyObservers(MainNeuroXModelEvent.ActiveModelChanged, null);
#endif
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

                emulationOnHistoryMode = Boolean.Parse(iniFileReader.Read("UseEmulationOnHistory", "EmulationOnHistory", "false"));

                serverConnector = new ServerConnector(this);

                localDatabaseConnector = new LocalDatabaseConnector(iniFileReader);



                if (!emulationOnHistoryMode)
                {
                    if (!Boolean.Parse(iniFileReader.Read("EmulateDevice", "BioData", "false")))
                    {
                        bioDataProvider = new TTLApiBioDataProvider(this, localDatabaseConnector, iniFileReader);
                    }
                    else
                    {
                        bioDataProvider = new RandomBioDataProvider(this, localDatabaseConnector, iniFileReader);
                    }
                }
                else
                {
                    bioDataProvider = new EmulationOnHistoryBioDataProvider(this, localDatabaseConnector, iniFileReader);
                }
                bioDataProvider.RegisterObserver(this);

                if (!emulationOnHistoryMode)
                {
                    if (!Boolean.Parse(iniFileReader.Read("EmulateFixApi", "FixApi", "false")))
                    {
                        fixApiModel = new FixApiModel(localDatabaseConnector, iniFileReader,this.WsHelperOrders, this.WsHelperQuotes);
                    }
                    else
                    {
                        fixApiModel = new RandomFixApiModel(localDatabaseConnector, iniFileReader);
                    }
                }
                else
                {
                    fixApiModel = new EmulationOnHistoryFixApiModel(localDatabaseConnector);
                }
                bioDataProvider.RegisterObserver(fixApiModel);
                fixApiModel.RegisterObserver(this);

                // initialization of behavioral models
                behavioralModelsContainer = new BehavioralModelsContainer(this, iniFileReader);
#if !SIMPLEST
                logicQuery1Condition = new LogicQuery1Condition(100, 60);
#endif


                ApplicationStates = ApplicationState.UsualState;
            }
            catch (Exception e)
            {
                isStateGood = false;
                StopProcessing(null, null);
                MessageBox.Show(e.Message, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ReInitalize()
        {
            if (!emulationOnHistoryMode)
            {
                if (!Boolean.Parse(iniFileReader.Read("EmulateDevice", "BioData", "false")))
                {
                    bioDataProvider = new TTLApiBioDataProvider(this, localDatabaseConnector, iniFileReader);
                }
                else
                {
                    bioDataProvider = new RandomBioDataProvider(this, localDatabaseConnector, iniFileReader);
                }
            }
            else
            {
                bioDataProvider = new EmulationOnHistoryBioDataProvider(this, localDatabaseConnector, iniFileReader);
            }
        }

        

        public void StartProcessing()
        {
            localDatabaseConnector.WriteUserAction(UserAction.ApplicationStarted);
            bioDataProvider.StartProcessing();
            fixApiModel.StartProcessing();
        }

        public void Synchronize()
        {
            if(!StopProcessingCalled)
                NotifyObservers(MainNeuroXModelEvent.SyncrhonizationStarted, null);
            //serverConnector.Synchronize();
            serverConnector.Disconnect();
            if (!StopProcessingCalled)
                NotifyObservers(MainNeuroXModelEvent.SynchronizationFinished, null);
        }

        public void PublishSynchonizationEvent(string message)
        {
            NotifyObservers(MainNeuroXModelEvent.SynchronizationEvent, message);
        }

        private bool StopProcessingCalled = false;

        /// <summary>
        /// Stop any processing. Called before application closing
        /// </summary>
        public void StopProcessing(object sender, EventArgs e)
        {
            if (StopProcessingCalled)
                return;
            StopProcessingCalled = true;

            if (bioDataProvider != null)
                bioDataProvider.StopProcessing();
            if (fixApiModel != null)
                fixApiModel.StopProcessing();
            localDatabaseConnector.WriteUserAction(UserAction.ApplicationClosed);

            // wait until data will be written to the local database
            System.Threading.Thread.Sleep(1000);
            Synchronize();

            localDatabaseConnector.Close();
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

        // ---- IBioDataObserver implementation
        public void OnNext(BioData.BioDataEvent bioDataEvent, object data)
        {
            if (bioDataEvent != BioDataEvent.NewBioDataTick)
            {
                return;
            }

            behavioralModelsContainer.OnNext((BioData.BioData)data);

#if !SIMPLEST
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
#endif
        }

        // ---- IFixApiObserver implementation
        public void OnNext(FixApiModelEvent modelEvent, object data)
        {
            if (modelEvent != FixApiModelEvent.PriceChanged)
            {
                return;
            }

#if !SIMPLEST
            behavioralModelsContainer.OnNext((TickPrice)data);
#endif
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
