using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroXChange.Model.BioData
{
    abstract public class AbstractBioDataProvider
    {
        private List<IBioDataObserver> observers = new List<IBioDataObserver>();

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

        protected void NotifyObservers(BioData data)
        {
            foreach (var observer in observers)
                observer.OnNext(data);
        }

        // ---- other methods that need to be implemented
        abstract public void StopProcessing();
    }
}
