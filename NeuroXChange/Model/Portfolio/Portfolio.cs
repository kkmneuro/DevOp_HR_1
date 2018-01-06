using NeuroXChange.Model.FixApi;
using System;
using System.ComponentModel;

namespace NeuroXChange.Model.Portfolio
{
    public class Portfolio
    {
        // portfolio management variables
        public int LotSize { get; set; }
        public int StopLossPips { get; set; }
        public int TakeProfitPips { get; set; }
        public double PipSize { get; set; }

        public int OrderCounter { get; private set; }
        public BindingList<Order> RunningOrders { get; private set; }
        public BindingList<Order> ClosedOrders { get; private set; }

        // TODO: move setings to ini file
        public const bool CanOpenMultipleOrdersOneDirection = false;
        public const bool CanOpenReverseOrders = false;

        public Portfolio()
        {
            RunningOrders = new BindingList<Order>();
            ClosedOrders = new BindingList<Order>();
            OrderCounter = 0;
        }

        public int Profitability(TickPrice price)
        {
            int result = 0;
            foreach (var order in RunningOrders)
            {
                result += order.Profitability(price);
            }
            return result;
        }

        public bool HasOrders()
        {
            return RunningOrders.Count != 0;
        }

        public bool HasOrdersByDirection(int direction)
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

        public void OpenOrder(
            int direction,
            TickPrice price)
        {
            // TODO: implement different behaviour depends on
            // CanOpenMultipleOrdersOneDirection and CanOpenReverseOrders

            if (HasOrdersByDirection(direction))
            {
                return;
            }

            if (HasOrdersByDirection(1 - direction))
            {
                CloseAllOrders(price, CloseReason.ReverseOrderRequested);
                return;
            }

            OrderCounter++;
            var order = new Order(OrderCounter, DateTime.Now, direction, price, 1, LotSize);
            RunningOrders.Add(order);
        }

        public void CloseAllOrders(TickPrice price, CloseReason closeReason)
        {
            foreach (var order in RunningOrders)
            {
                CloseOrder(order, price, closeReason);
            }
        }

        public void OnNext(TickPrice price)
        {
            foreach(var order in RunningOrders)
            {
                var closeReason = order.NeedToBeClosedAccordingToSLorTP(price, PipSize);
                if (closeReason == CloseReason.ShouldntBeClosed)
                {
                    continue;
                }

                CloseOrder(order, price, closeReason);
            }
        }

        private void CloseOrder(Order order, TickPrice price, CloseReason closeReason)
        {
            order.Close(DateTime.Now, price, closeReason, Profitability(price));
            ClosedOrders.Add(order);
            RunningOrders.Remove(order);
        }
    }
}
