using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroXChange.Model.BehavioralModeling.BehavioralModels
{
    public class TransitionHistoryItem
    {
        // unique identificator per one model
        public int ID { get; set; }

        public string Time { get; set; }
        public string ToState { get; set; }
        public string FromState { get; set; }
        public string Transition { get; set; }

        public TransitionHistoryItem(int ID, string Time, string ToState, string FromState, string Transition)
        {
            this.ID = ID;
            this.Time = Time;
            this.ToState = ToState;
            this.FromState = FromState;
            this.Transition = Transition;
        }

        public TransitionHistoryItem()
        {
            ID = -1;
            Time = string.Empty;
            ToState = string.Empty;
            FromState = string.Empty;
            Transition = string.Empty;
        }
    }
}
