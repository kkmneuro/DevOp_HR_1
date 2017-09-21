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
        private List<IMainNeuroXModelObserver> observers = new List<IMainNeuroXModelObserver>();
        public AbstractBioDataProvider bioDataProvider { get; private set; }
        public FixApiModel fixApiModel;
        public IniFileReader iniFileReader { get; private set; }

        // step conditions
        int StepChangeStart = -60;
        int StepChangeEnd = -20;

        public MainNeuroXModel()
        {
            if (!File.Exists("NeuroConfig.ini"))
            {
                throw new Exception("No configuration file \"NeuroConfig.ini\" found!\nHint: place \"NeuroConfig.ini\" in the same folder as this exe file");
            }

            iniFileReader = new IniFileReader("NeuroConfig.ini");

            bioDataProvider = new RealTimeMSAccessBioDataProvider(iniFileReader);
            //bioDataProvider = new MSAccessBioDataProvider(databaseLocation);
            //bioDataProvider = new RandomBioDataProvider();
            //bioDataProvider = new UdpBioDataProvider(14321);
            bioDataProvider.RegisterObserver(this);
            Application.ApplicationExit += new EventHandler(this.StopProcessing);

            // load step change conditions
            StepChangeStart = Int32.Parse(iniFileReader.Read("StepChangeStart", "LogicConditions"));
            StepChangeEnd = Int32.Parse(iniFileReader.Read("StepChangeEnd", "LogicConditions"));

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
        private MainNeuroXModelEvent lastEvent = MainNeuroXModelEvent.StepInitialState;
        private bool returnedBack = false;

        // ---- IBioDataObserver implementation
        public void OnNext(Sub_Component_Protocol_Psychophysiological_Session_Data_TPS data)
        {
            // check ready to trade condition
            //if (data.temperature > 30 && data.hartRate > 120)
            if (data.accY > StepChangeEnd)
            {
                returnedBack = true;
            }

            if (data.accY < StepChangeStart && returnedBack)
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
