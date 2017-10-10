using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuroXChange.Model.BioData;
using System.Windows.Forms;
using NeuroXChange.Common;
using System.IO;
using NeuroXChange.Model.FixApi;

namespace NeuroXChange.Model
{
    public class MainNeuroXModel : IBioDataObserver
    {
        private string settingsFileName = "NeuroXChangeSettings.ini";

        private List<IMainNeuroXModelObserver> observers = new List<IMainNeuroXModelObserver>();
        public AbstractBioDataProvider bioDataProvider { get; private set; }
        public FixApiModel fixApiModel;
        public IniFileReader iniFileReader { get; private set; }

        // are we buying or selling
        private int orderDirection = 0;  // 0 - buy, 1 - sell

        // step constants
        int stepChangeStart = -60;
        int stepChangeEnd = -20;

        // Logic Query 1 (direction)
        int logicQueryDirectionHeartRate = 60;

        public MainNeuroXModel()
        {
            if (!File.Exists(settingsFileName))
            {
                throw new Exception(
                    string.Format("No configuration file \"{0}\" found!\nHint: place \"{0}\" in the same folder as this exe file", settingsFileName)
                    );
            }

            iniFileReader = new IniFileReader(settingsFileName);

            bioDataProvider = new RealTimeMSAccessBioDataProvider(iniFileReader);
            //bioDataProvider = new MSAccessBioDataProvider(databaseLocation);
            //bioDataProvider = new RandomBioDataProvider();
            //bioDataProvider = new UdpBioDataProvider(14321);
            bioDataProvider.RegisterObserver(this);
            Application.ApplicationExit += new EventHandler(this.StopProcessing);

            // load logic conditions constants
            stepChangeStart = Int32.Parse(iniFileReader.Read("StepChangeStart", "LogicConditions"));
            stepChangeEnd = Int32.Parse(iniFileReader.Read("StepChangeEnd", "LogicConditions"));
            logicQueryDirectionHeartRate = Int32.Parse(iniFileReader.Read("LogicQueryDirectionHeartRate", "LogicConditions"));

            fixApiModel = new FixApiModel(iniFileReader);
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
        private bool logicQueryDirectionFired = false;

        // Logic query 2 (Entry trigger) variables
        private Queue<Sub_Component_Protocol_Psychophysiological_Session_Data_TPS> last60secData = 
            new Queue<Sub_Component_Protocol_Psychophysiological_Session_Data_TPS>();

        // ---- IBioDataObserver implementation
        public void OnNext(Sub_Component_Protocol_Psychophysiological_Session_Data_TPS data)
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
            if (data.sub_Protocol_ID != 74 && data.hartRate < logicQueryDirectionHeartRate)
            {
                logicQueryDirectionSubProtocolID = data.sub_Protocol_ID;
            }

            if (data.sub_Protocol_ID == 74 && !logicQueryDirectionFired && logicQueryDirectionSubProtocolID > -1)
            {
                logicQueryDirectionFired = true;

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
            }


            // ----- LOGIC QUERY 2 (Entry trigger) event ------
            last60secData.Enqueue(data);
            Sub_Component_Protocol_Psychophysiological_Session_Data_TPS? prev60secData = null;
            while (last60secData.Count > 0)
            {
                var peekData = last60secData.Peek();
                System.TimeSpan span = data.time - peekData.time;
                if (span > TimeSpan.FromMinutes(1))
                {
                    prev60secData = peekData;
                    last60secData.Dequeue();
                }
                else
                {
                    break;
                }
            }
            if (prev60secData.HasValue && (data.time - prev60secData.Value.time < TimeSpan.FromSeconds(100)))
            {
                bool conditionsMet = false;
                if (data.hartRate > 100 && prev60secData.Value.hartRate < 60)
                {
                    conditionsMet = true;
                    orderDirection = 0;
                }
                if (data.hartRate < 60 && prev60secData.Value.hartRate > 100)
                {
                    conditionsMet = true;
                    orderDirection = 1;
                }
                if (conditionsMet)
                {
                    NotifyObservers(lastEvent = MainNeuroXModelEvent.StepExecuteOrder, orderDirection);
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
