using System.Collections.Generic;
using NeuroXChange.Model.BioData;

namespace NeuroXChange.Model.BehavioralModeling.BioDataProcessors
{
    public abstract class AbstractBioDataProcessor
    {
        // ---- IBioDataObserver implementation
        public abstract void OnNext(BioData.BioData data);
    }
}
