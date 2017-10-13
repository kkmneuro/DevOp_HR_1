using System;
using NeuroXChange.Model.BioData;

namespace NeuroXChange.Model.BioDataProcessors
{
    public abstract class BioDataProcessor : IBioDataObserver
    {
        protected MainNeuroXModel mainNeuroXModel;

        public BioDataProcessor(MainNeuroXModel mainNeuroXModel)
        {
            this.mainNeuroXModel = mainNeuroXModel;
        }

        public virtual void OnNext(Sub_Component_Protocol_Psychophysiological_Session_Data_TPS data)
        {
            throw new NotImplementedException();
        }
    }
}
