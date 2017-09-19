using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuroXChange.Model.BioData;
using System.Windows.Forms;
using NeuroXChange.Common;

namespace NeuroXChange.Model
{
    public class MainNeuroXModel : IBioDataObserver
    {
        private List<IMainNeuroXModelObserver> observers = new List<IMainNeuroXModelObserver>();
        public AbstractBioDataProvider bioDataProvider { get; private set; }
        public IniFileReader iniFileReader { get; private set; }

        public MainNeuroXModel()
        {
            iniFileReader = new IniFileReader("NeuroConfig.ini");
            var databaseLocation = iniFileReader.Read("Location", "Database");
            var tableName = iniFileReader.Read("Table", "Database");

            bioDataProvider = new RealTimeMSAccessBioDataProvider(databaseLocation, tableName);
            //bioDataProvider = new MSAccessBioDataProvider(databaseLocation);
            //bioDataProvider = new RandomBioDataProvider();
            //bioDataProvider = new UdpBioDataProvider(14321);
            bioDataProvider.RegisterObserver(this);
            Application.ApplicationExit += new EventHandler(this.StopProcessing);
        }

        /// <summary>
        /// Stop any processing. Called before application closing
        /// </summary>
        public void StopProcessing(object sender, EventArgs e)
        {
            if (bioDataProvider != null)
                bioDataProvider.StopProcessing();
        }

        // TEMP. TODO: update logic
        private MainNeuroXModelEvent lastEvent = MainNeuroXModelEvent.StepInitialState;
        private bool returnedBack = false;

        // ---- IBioDataObserver implementation
        public void OnNext(Sub_Component_Protocol_Psychophysiological_Session_Data_TPS data)
        {
            // check ready to trade condition
            //if (data.temperature > 30 && data.hartRate > 120)
            if (data.accY > -10)
            {
                returnedBack = true;
            }

            if (data.accY < -70 && returnedBack)
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
