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

        // ---- IBioDataObserver implementation
        public void OnNext(Sub_Component_Protocol_Psychophysiological_Session_Data_TPS data)
        {
            // ----- application steps main loop ------

            // check ready to trade condition
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
                            NotifyObservers(lastEvent = MainNeuroXModelEvent.StepDirectionConfirmed, null);
                            break;
                        }
                    case MainNeuroXModelEvent.StepDirectionConfirmed:
                        {
                            NotifyObservers(lastEvent = MainNeuroXModelEvent.StepExecuteOrder, null);
                            break;
                        }
                    case MainNeuroXModelEvent.StepExecuteOrder:
                        {
                            NotifyObservers(lastEvent = MainNeuroXModelEvent.StepConfirmationFilled, null);
                            break;
                        }
                    case MainNeuroXModelEvent.StepConfirmationFilled:
                        {
                            NotifyObservers(lastEvent = MainNeuroXModelEvent.StepInitialState, null);
                            break;
                        }
                }
            }

            // ----- LOGIC QUERY 1 (Direction) ------
            if (data.sub_Protocol_ID != 74 && data.hartRate < logicQueryDirectionHeartRate)
            {
                logicQueryDirectionSubProtocolID = data.sub_Protocol_ID;
            }

            if (data.sub_Protocol_ID == 74 && !logicQueryDirectionFired && logicQueryDirectionSubProtocolID > -1)
            {
                logicQueryDirectionFired = true;
                NotifyObservers(MainNeuroXModelEvent.LogicQueryDirection, logicQueryDirectionSubProtocolID);
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
