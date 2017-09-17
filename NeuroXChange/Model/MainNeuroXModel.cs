using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuroXChange.Model.BioData;
using System.Windows.Forms;

namespace NeuroXChange.Model
{
    public class MainNeuroXModel : IBioDataObserver
    {
        private List<IMainNeuroXModelObserver> observers = new List<IMainNeuroXModelObserver>();
        public AbstractBioDataProvider bioDataProvider { get; private set; }

        public MainNeuroXModel()
        {
            bioDataProvider = new MSAccessBioDataProvider(@"C:\tmp\neurotrader\Neuro-Xchange_Psychophysiology1.mdb");
            //bioDataProvider = new RandomBioDataProvider();
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

        // ---- IBioDataObserver implementation
        public void OnNext(Sub_Component_Protocol_Psychophysiological_Session_Data_TPS data)
        {
            // check ready to trade condition
            if (data.temperature > 30 && data.hartRate > 120)
                NotifyObservers(MainNeuroXModelEvent.StepReadyToTrade, null);
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
