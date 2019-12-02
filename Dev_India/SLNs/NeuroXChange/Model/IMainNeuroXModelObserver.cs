using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroXChange.Model
{
    public interface IMainNeuroXModelObserver
    {
        void OnNext(MainNeuroXModelEvent modelEvent, object data);
    }
}
