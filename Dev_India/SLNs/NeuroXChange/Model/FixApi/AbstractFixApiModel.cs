using System.Collections.Generic;
using NeuroXChange.Model.BioData;
using NeuroXChange.Model.Database;
using System;
using System.Data;

namespace NeuroXChange.Model.FixApi
{
    public abstract class AbstractFixApiModel : IBioDataObserver
    {
        public abstract void OnNext(BioDataEvent bioDataEvent, object data);

        public abstract void StartProcessing();
        public abstract void StopProcessing();
        public abstract void SubscribeForQuotes(Enum ReqType, string contract);
        public abstract void SubscribeForQuotes(Enum ReqType, List<string> lst);
        public virtual  event Action<string> RefreshClient;
        public virtual event Action<string[]> RefreshSymbols;
        public virtual event Action<DataTable> RefreshQuotes;

        private List<IFixApiObserver> observers = new List<IFixApiObserver>();
        protected LocalDatabaseConnector localDatabaseConnector;
        

        public AbstractFixApiModel(LocalDatabaseConnector localDatabaseConnector)
        {
            this.localDatabaseConnector = localDatabaseConnector;
        }

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
