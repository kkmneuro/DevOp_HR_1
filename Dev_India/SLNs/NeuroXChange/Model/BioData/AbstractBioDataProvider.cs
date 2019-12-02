using NeuroXChange.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroXChange.Model.BioData
{
    abstract public class AbstractBioDataProvider
    {
        protected MainNeuroXModel model;
        private List<IBioDataObserver> observers = new List<IBioDataObserver>();
        protected LocalDatabaseConnector localDatabaseConnector;

        public AbstractBioDataProvider(MainNeuroXModel model, LocalDatabaseConnector localDatabaseConnector)
        {
            this.model = model;
            this.localDatabaseConnector = localDatabaseConnector;
        }

        protected void FillApplicaitonStates(BioData data)
        {
            data.trainingType = (int)model.TrainingType;
            data.trainingStep = model.TrainingStep;
            data.applicationStates = (int)model.ApplicationStates;
        }

        // ---- Observable pattern implementation
        public void RegisterObserver(IBioDataObserver observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
        }

        public void RemoveObserver(IBioDataObserver observer)
        {
            if (observers.Contains(observer))
                observers.Remove(observer);
        }

        protected void NotifyObservers(BioDataEvent bioDataEvent, object data)
        {
            foreach (var observer in observers)
                observer.OnNext(bioDataEvent, data);
        }

        // ---- other methods that need to be implemented
        abstract public void StartProcessing();
        abstract public void StopProcessing();
    }
}
