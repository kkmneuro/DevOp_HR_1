using System.Collections.Generic;
using NeuroXChange.Model.BioData;

namespace NeuroXChange.Model.BehavioralModeling.BioDataProcessors
{
    public abstract class AbstractBioDataProcessor : IBioDataObserver
    {
        private List<IBioDataProcessorEventObserver> observers = new List<IBioDataProcessorEventObserver>();

        // ---- Observable pattern implementation
        public void RegisterObserver(IBioDataProcessorEventObserver observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
        }

        public void RemoveObserver(IBioDataProcessorEventObserver observer)
        {
            if (observers.Contains(observer))
                observers.Remove(observer);
        }

        protected void NotifyObservers(BioDataProcessorEvent bioDataProcessorEvent, object data)
        {
            foreach (var observer in observers)
                observer.OnNext(bioDataProcessorEvent, data);
        }

        // ---- IBioDataObserver implementation
        public abstract void OnNext(BioData.BioData data);
    }
}
