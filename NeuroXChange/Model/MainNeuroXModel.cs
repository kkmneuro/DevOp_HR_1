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

        // step constants
        int stepChangeStart = -60;
        int stepChangeEnd = -20;

        // Logic Query 1 (direction)
        int logicQueryDirectionHeartRate = 60;

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

                // load logic conditions constants
                stepChangeStart = Int32.Parse(iniFileReader.Read("StepChangeStart", "LogicConditions"));
                stepChangeEnd = Int32.Parse(iniFileReader.Read("StepChangeEnd", "LogicConditions"));
                logicQueryDirectionHeartRate = Int32.Parse(iniFileReader.Read("LogicQueryDirectionHeartRate", "LogicConditions"));

                fixApiModel = new FixApiModel(iniFileReader);
                bioDataProvider.RegisterObserver(fixApiModel);

                // initialization of behavioral models
                behavioralModelsContainer = new BehavioralModelsContainer(iniFileReader);
                bioDataProvider.RegisterObserver(behavioralModelsContainer);

                // initialize model variables
                lastEvent = BehavioralModelState.InitialState;
                orderDirection = 0;
                logicQueryDirectionSubProtocolID = -1;
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

        // are we buying or selling
        public int orderDirection { get; private set; }  // 0 - buy, 1 - sell

        // TEMP. TODO: update logic
        // application steps main loop variables
        public BehavioralModelState lastEvent { get; private set; }

        // AccY event condition
        private bool returnedBack = false;

        // Logic query 1 (Direction) variables
        public int logicQueryDirectionSubProtocolID { get; private set; }

        // Logic query 2 (Entry trigger) variables
        DateTime? lq2LastHartRateBigger100 = null;
        DateTime? lq2LastHartRateSmaller60 = null;

        // ---- IBioDataObserver implementation
        public void OnNext(BioData.BioData data)
        {
            // ----- application steps main loop ------

            // ----- AccY event ------
            if (data.accY > stepChangeEnd)
            {
                returnedBack = true;
            }
            if (data.accY < stepChangeStart && returnedBack)
            {
                returnedBack = false;
                switch (lastEvent) {
                    case BehavioralModelState.InitialState:
                        {
                            lastEvent = BehavioralModelState.ReadyToTrade;
                            break;
                        }
                    case BehavioralModelState.ReadyToTrade:
                        {
                            lastEvent = BehavioralModelState.Preactivation;
                            break;
                        }
                    case BehavioralModelState.Preactivation:
                        {
                            lastEvent = BehavioralModelState.DirectionConfirmed;
                            break;
                        }
                    case BehavioralModelState.DirectionConfirmed:
                        {
                            lastEvent = BehavioralModelState.ExecuteOrder;
                            break;
                        }
                    case BehavioralModelState.ExecuteOrder:
                        {
                            lastEvent = BehavioralModelState.ConfirmationFilled;
                            break;
                        }
                    case BehavioralModelState.ConfirmationFilled:
                        {
                            lastEvent = BehavioralModelState.InitialState;
                            break;
                        }
                }
                NotifyObservers(MainNeuroXModelEvent.AvtiveModelStateChanged, null);
            }


            // ----- HR Oscillations procesing -----
            HeartRateInfo info = behavioralModelsContainer.heartRateProcessor.heartRateInfo;
            if (5 < info.oscillations5minAverage && info.oscillations5minAverage < 6.5)
            {
                if (lastEvent != BehavioralModelState.Preactivation)
                {
                    lastEvent = BehavioralModelState.Preactivation;
                    NotifyObservers(MainNeuroXModelEvent.AvtiveModelStateChanged, null);
                }
            }
            else if (5 < info.oscillations3minAverage && info.oscillations3minAverage < 6.5)
            {
                if (lastEvent != BehavioralModelState.ReadyToTrade)
                {
                    lastEvent = BehavioralModelState.ReadyToTrade;
                    NotifyObservers(MainNeuroXModelEvent.AvtiveModelStateChanged, null);
                }
            }


            // ----- LOGIC QUERY 1 (Direction) event ------
            if (lastEvent != BehavioralModelState.DirectionConfirmed)
            {
                if (data.sub_Protocol_ID != 74 && data.hartRate < logicQueryDirectionHeartRate)
                {
                    logicQueryDirectionSubProtocolID = data.sub_Protocol_ID;
                }

                if (data.sub_Protocol_ID == 74
                    && 65 <= logicQueryDirectionSubProtocolID
                    && logicQueryDirectionSubProtocolID < 74)
                {
                    // change application state
                    int[] buyIDs = { 66, 68, 71, 72 };
                    int[] sellIDs = { 67, 69, 70, 73 };
                    if (buyIDs.Contains(logicQueryDirectionSubProtocolID))
                    {
                        orderDirection = 0;
                    }
                    else if (sellIDs.Contains(logicQueryDirectionSubProtocolID))
                    {
                        orderDirection = 1;
                    }
                    lastEvent = BehavioralModelState.DirectionConfirmed;
                    NotifyObservers(MainNeuroXModelEvent.AvtiveModelStateChanged, null);

                    // inform about logic query direction sub protocol ID
                    NotifyObservers(MainNeuroXModelEvent.LogicQueryDirection, logicQueryDirectionSubProtocolID);

                    // reset global variables
                    logicQueryDirectionSubProtocolID = -1;
                }
            }


            // ----- LOGIC QUERY 2 (Entry trigger) event ------
            if (lq2LastHartRateBigger100.HasValue && (data.time - lq2LastHartRateBigger100.Value) > TimeSpan.FromMinutes(1))
            {
                lq2LastHartRateBigger100 = null;
            }
            if (lq2LastHartRateSmaller60.HasValue && (data.time - lq2LastHartRateSmaller60.Value) > TimeSpan.FromMinutes(1))
            {
                lq2LastHartRateSmaller60 = null;
            }

            bool lq2ConditionsMet = false;
            if (lq2LastHartRateBigger100.HasValue && data.hartRate < 60)
            {
                lq2LastHartRateBigger100 = null;
                lq2ConditionsMet = true;
                orderDirection = 0;
            }
            if (lq2LastHartRateSmaller60.HasValue && data.hartRate > 100)
            {
                lq2LastHartRateSmaller60 = null;
                lq2ConditionsMet = true;
                orderDirection = 1;
            }
            if (lq2ConditionsMet)
            {
                lastEvent = BehavioralModelState.ExecuteOrder;
                NotifyObservers(MainNeuroXModelEvent.AvtiveModelStateChanged, null);
            }
            if (data.hartRate < 60)
            {
                lq2LastHartRateSmaller60 = data.time;
            }
            if (data.hartRate > 100)
            {
                lq2LastHartRateBigger100 = data.time;
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
