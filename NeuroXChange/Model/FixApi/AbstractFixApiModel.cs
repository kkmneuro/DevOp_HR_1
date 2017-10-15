using System.Collections.Generic;
using NeuroXChange.Model.BioData;

namespace NeuroXChange.Model.FixApi
{
    public abstract class AbstractFixApiModel : IBioDataObserver
    {
        private List<IFixApiObserver> observers = new List<IFixApiObserver>();

        public abstract void OnNext(BioData.BioData data);

        public abstract void StartProcessing();
        public abstract void StopProcessing();

        // ---- Observable pattern implementation
        public void RegisterObserver(IFixApiObserver observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
        }

        public void RemoveObserver(IFixApiObserver observer)
        {
            if (observers.Contains(observer))
                observers.Remove(observer);
        }

        protected void NotifyObservers(FixApiModelEvent modelEvent, object data)
        {
            foreach (var observer in observers)
                observer.OnNext(modelEvent, data);
        }

    }
}
