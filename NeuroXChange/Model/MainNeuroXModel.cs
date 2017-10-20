using System;
using System.Collections.Generic;
using System.Linq;
using NeuroXChange.Model.BioData;
using System.Windows.Forms;
using NeuroXChange.Common;
using System.IO;
using NeuroXChange.Model.FixApi;
using NeuroXChange.Model.BioDataProcessors;

namespace NeuroXChange.Model
{
    public class MainNeuroXModel : IBioDataObserver, IBioDataProcessorEventObserver
    {
        private string settingsFileName = "NeuroXChangeSettings.ini";

        public bool emulationOnHistory { get; private set; }
        public bool isStateGood { get; private set; }

        private List<IMainNeuroXModelObserver> observers = new List<IMainNeuroXModelObserver>();
        public AbstractBioDataProvider bioDataProvider { get; private set; }
        public AbstractFixApiModel fixApiModel;
        public IniFileReader iniFileReader { get; private set; }

        // bio data processors
        public HeartRateProcessor heartRateProcessor = null;

        // are we buying or selling
        private int orderDirection = 0;  // 0 - buy, 1 - sell

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

                // bio data processors
                heartRateProcessor = new HeartRateProcessor();
                bioDataProvider.RegisterObserver(heartRateProcessor);
                heartRateProcessor.RegisterObserver(this);
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


        // TEMP. TODO: update logic
        // application steps main loop variables
        private MainNeuroXModelEvent lastEvent = MainNeuroXModelEvent.StepInitialState;
        private bool returnedBack = false;

        // Logic query 1 (Direction) variables
        private int logicQueryDirectionSubProtocolID = -1;

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
                    case MainNeuroXModelEvent.StepInitialState:
                        {
                            NotifyObservers(lastEvent = MainNeuroXModelEvent.StepReadyToTrade, null);
                            break;
                        }
                    case MainNeuroXModelEvent.StepReadyToTrade:
                        {
                            NotifyObservers(lastEvent = MainNeuroXModelEvent.StepPreactivation, null);
                            break;
                        }
                    case MainNeuroXModelEvent.StepPreactivation:
                        {
                            NotifyObservers(lastEvent = MainNeuroXModelEvent.StepDirectionConfirmed, orderDirection);
                            break;
                        }
                    case MainNeuroXModelEvent.StepDirectionConfirmed:
                        {
                            NotifyObservers(lastEvent = MainNeuroXModelEvent.StepExecuteOrder, orderDirection);
                            break;
                        }
                    case MainNeuroXModelEvent.StepExecuteOrder:
                        {
                            NotifyObservers(lastEvent = MainNeuroXModelEvent.StepConfirmationFilled, orderDirection);
                            break;
                        }
                    case MainNeuroXModelEvent.StepConfirmationFilled:
                        {
                            NotifyObservers(lastEvent = MainNeuroXModelEvent.StepInitialState, null);
                            break;
                        }
                }
            }


            // ----- LOGIC QUERY 1 (Direction) event ------
            if (lastEvent != MainNeuroXModelEvent.StepDirectionConfirmed)
            {
                if (data.sub_Protocol_ID != 74 && data.hartRate < logicQueryDirectionHeartRate)
                {
                    logicQueryDirectionSubProtocolID = data.sub_Protocol_ID;
                }

                if (data.sub_Protocol_ID == 74 && logicQueryDirectionSubProtocolID > -1)
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
                    NotifyObservers(lastEvent = MainNeuroXModelEvent.StepDirectionConfirmed, orderDirection);

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
                NotifyObservers(lastEvent = MainNeuroXModelEvent.StepExecuteOrder, orderDirection);
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

        // IBioDataProcessorEventObserver implementation
        public void OnNext(BioDataProcessorEvent bioDataProcessorEvent, object data)
        {
            if (bioDataProcessorEvent == BioDataProcessorEvent.HeartRateRawStatistics)
            {
                HeartRateInfo info = (HeartRateInfo)data;
                if (5 < info.oscillations5minAverage && info.oscillations5minAverage < 6.5)
                {
                    if (lastEvent != MainNeuroXModelEvent.StepPreactivation)
                    {
                        NotifyObservers(lastEvent = MainNeuroXModelEvent.StepPreactivation, null);
                    }
                }
                else if (5 < info.oscillations3minAverage && info.oscillations3minAverage < 6.5)
                {
                    if (lastEvent != MainNeuroXModelEvent.StepReadyToTrade)
                    {
                        NotifyObservers(lastEvent = MainNeuroXModelEvent.StepReadyToTrade, null);
                    }
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
