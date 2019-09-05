using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroXChange.Model.BehavioralModeling.BehavioralModels
{
    public static class BehavioralModelStateHelper
    {
        public static readonly string[] directionName = { "Buy", "Sell", "No direction" };

        public static string StateToString(BehavioralModelState state)
        {
            string result = string.Empty;
            switch(state)
            {
                case BehavioralModelState.InitialState:
                    result = "Initial state";
                    break;
                case BehavioralModelState.ReadyToTrade:
                    result = "Ready to trade";
                    break;
                case BehavioralModelState.Preactivation:
                    result = "Preactivation";
                    break;
                case BehavioralModelState.DirectionConfirmed:
                    result = "Direction confirmed";
                    break;
                case BehavioralModelState.ExecuteOrder:
                    result = "Execute order";
                    break;
                case BehavioralModelState.ConfirmationFilled:
                    result = "Confirmation filled";
                    break;
            }
            return result;
        }
    }
}
