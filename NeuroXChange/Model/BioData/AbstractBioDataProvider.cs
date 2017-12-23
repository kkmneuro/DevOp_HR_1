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
        public Training.TrainingType TrainingType { get; set; }

        public int TrainingStep { get; set; }

        private List<IBioDataObserver> observers = new List<IBioDataObserver>();
        protected LocalDatabaseConnector localDatabaseConnector;


        public AbstractBioDataProvider(LocalDatabaseConnector localDatabaseConnector)
        {
            this.localDatabaseConnector = localDatabaseConnector;
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
