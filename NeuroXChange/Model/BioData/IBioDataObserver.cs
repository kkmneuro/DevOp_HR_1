using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroXChange.Model.BioData
{
    public interface IBioDataObserver
    {
        void OnNext(Sub_Component_Protocol_Psychophysiological_Session_Data_TPS data);
    }
}
