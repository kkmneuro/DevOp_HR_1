using NeuroXChange.Common;
using NeuroXChange.Model.FixApi;
using System;

namespace NeuroXChange.Model.Portfolio
{
    public class Portfolio
    {
        // portfolio management variables
        public int DefaultLotSize { get; set; }
        public int? DefaultHardStopLossPips { get; set; }
        public int? DefaultTrailingStopLossPips { get; set; }
        public int? DefaultTakeProfitPips { get; set; }
        public double DefaultPipSize { get; set; }

        public int OrderCounter { get; private set; }
        public int OrderGroupCounter { get; private set; }
        public ThreadedBindingList<Order> RunningOrders { get; private set; }
        public ThreadedBindingList<Order> ClosedOrders { get; private set; }
        public int ClosedProfitability {get; private set;}

        // TODO: move setings to ini file
        public const bool CanOpenMultipleOrdersOneDirection = false;
        public const bool CanOpenReverseOrders = false;

        public Portfolio()
        {
            RunningOrders = new ThreadedBindingList<Order>();
            ClosedOrders = new ThreadedBindingList<Order>();
            OrderCounter = 0;
            OrderGroupCounter = 0;
            ClosedProfitability = 0;
        }

        public int RunningProfitability(TickPrice price)
        {
            int result = 0;
            foreach (var order in RunningOrders)
            {
                result += order.GeneralizedProfitability(price);
            }
            return result;
        }

        public bool HasOrders()
        {
            return RunningOrders.Count != 0;
        }

        public bool HasOrdersByDirection(OrderDirection direction)
        {
            if (RunningOrders.Count == 0)
            {
                return false;
            }

            foreach (var order in RunningOrders)
            {
                if (order.Direction == direction)
                {
                    return true;
                }
            }

            return false;
        }

        // returns if order was open or reverse orders was closed
        public bool OpenOrder(
            int bmModelID,
            OrderDirection direction,
            TickPrice price,
            OpenReason openReason,
            out Order order)
        {
            order = null;

            // TODO: implement different behaviour depends on
            // CanOpenMultipleOrdersOneDirection and CanOpenReverseOrders

            if (HasOrdersByDirection(direction))
            {
                return false;
            }

            if (HasOrdersByDirection(direction == OrderDirection.Buy ? OrderDirection.Sell : OrderDirection.Buy))
            {
                CloseAllOrders(price, CloseReason.ReverseOrderRequested);
                return true;
            }

            OrderCounter++;
            OrderGroupCounter++;
            order = new Order(OrderCounter, OrderGroupCounter, bmModelID, DateTime.Now, DateTime.Now, price, direction, 1, DefaultLotSize, openReason);
            order.HardStopLossPips = DefaultHardStopLossPips;
            order.TrailingStopLossPips = DefaultTrailingStopLossPips;
            order.TakeProfitPips = DefaultTakeProfitPips;
            RunningOrders.Add(order);

            return true;
        }

        public void CloseAllOrders(TickPrice price, CloseReason closeReason)
        {
            for (int orderInd = 0; orderInd < RunningOrders.Count; orderInd++)
            {
                var order = RunningOrders[orderInd];
                CloseOrder(order, price, closeReason);
                orderInd--;
            }
        }

        public void OnNext(TickPrice price)
        {
            for(int orderInd = 0; orderInd < RunningOrders.Count; orderInd++)
            {
                var order = RunningOrders[orderInd];
                var closeReason = order.NeedToBeClosedAccordingToSLorTP(price, DefaultPipSize);
                if (closeReason == CloseReason.ShouldntBeClosed)
                {
                    continue;
                }

                CloseOrder(order, price, closeReason);
                orderInd--;
            }
        }

        public int TakeProfitValueToPips(OrderDirection direction, TickPrice price, double takeProfitValue)
        {
            if (direction == OrderDirection.Buy)
            {
                return (int)Math.Round((takeProfitValue - price.sell) / DefaultPipSize);
            }
            else
            {
                return (int)Math.Round((price.buy - takeProfitValue) / DefaultPipSize);
            }
        }

        public int HardStopLossValueToPips(OrderDirection direction, TickPrice price, double hardStopLossValue)
        {
            if (direction == OrderDirection.Buy)
            {
                return (int)Math.Round((price.sell - hardStopLossValue) / DefaultPipSize);
            }
            else
            {
                return (int)Math.Round((hardStopLossValue - price.buy) / DefaultPipSize);
            }
        }

        private void CloseOrder(Order order, TickPrice price, CloseReason closeReason)
        {
            var balance = ClosedProfitability + RunningProfitability(price);
            order.Close(DateTime.Now, price, closeReason, balance);
            ClosedProfitability += order.Profitability.Value;
            ClosedOrders.Add(order);
            RunningOrders.Remove(order);
        }
    }
}
