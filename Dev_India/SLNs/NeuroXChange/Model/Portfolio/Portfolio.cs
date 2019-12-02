using NeuroXChange.Common;
using NeuroXChange.Model.Database;
using NeuroXChange.Model.FixApi;
using System;

namespace NeuroXChange.Model.Portfolio
{
    public class Portfolio
    {
        private LocalDatabaseConnector localDatabaseConnector;

        // portfolio management variables
        public int DefaultLotSize { get; set; }
        public int? DefaultStopLossPips { get; set; }
        public int? DefaultTakeProfitPips { get; set; }
        public double DefaultPipSize { get; set; }

        public ThreadedBindingList<Order> RunningOrders { get; private set; }
        public ThreadedBindingList<Order> ClosedOrders { get; private set; }
        public int ClosedProfitability {get; private set;}

        // TODO: move setings to ini file
        public const bool CanOpenMultipleOrdersOneDirection = false;
        public const bool CanOpenReverseOrders = false;

        public Portfolio(LocalDatabaseConnector localDatabaseConnector)
        {
            this.localDatabaseConnector = localDatabaseConnector;

            RunningOrders = new ThreadedBindingList<Order>();
            ClosedOrders = new ThreadedBindingList<Order>();
            ClosedProfitability = 0;
        }

        public void AddHistoryOrder(Order order)
        {
            ClosedProfitability += order.Profitability.Value;
            order.CumulativeBalance = ClosedProfitability;
            ClosedOrders.Insert(0, order);
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

        // only for trading hierarchies
        bool isTradingHierarchyStarted = false;
        int tradingHierarchyOrderGroup;
        public void StartTradingHierarchy(int bmModelID, OpenReason openReason, TickPrice price, DateTime time)
        {
            if (openReason == OpenReason.UnknownReason)
            {
                return;
            }

            CloseAllOrders(price, CloseReason.NewTradingHierarchyInstance, time);

            isTradingHierarchyStarted = true;
            tradingHierarchyOrderGroup = localDatabaseConnector.InitiateNewGroupID();

            if (openReason == OpenReason.SingularLong || openReason == OpenReason.SingularShort)
            {
                for (int orderInd = 1; orderInd <= 3; orderInd++)
                {
                    var order = new Order(
                        localDatabaseConnector.InitiateNewOrderID(),
                        tradingHierarchyOrderGroup,
                        orderInd, bmModelID, time, time, price,
                        openReason == OpenReason.SingularLong ? OrderDirection.Buy : OrderDirection.Sell, 
                        1, DefaultLotSize, openReason);
                    order.StopLossPips = 60;
                    order.TakeProfitPips = 100 + (orderInd - 1) * 50;
                    RunningOrders.Insert(0, order);
                }
            }
            else if (openReason == OpenReason.MLS1 || openReason == OpenReason.MLS2 ||
                openReason == OpenReason.MSL1 || openReason == OpenReason.MSL2)
            {
                var order = new Order(
                    localDatabaseConnector.InitiateNewOrderID(),
                    tradingHierarchyOrderGroup,
                    1, bmModelID, time, time, price,
                    openReason == OpenReason.MLS1 || openReason == OpenReason.MLS2 ? OrderDirection.Buy : OrderDirection.Sell,
                    openReason == OpenReason.MLS1 || openReason == OpenReason.MSL1 ? 1 : 0,
                    DefaultLotSize, openReason);
                order.StopLossPips = 60;
                order.TakeProfitPips = openReason == OpenReason.MLS1 || openReason == OpenReason.MSL1 ? 100 : 60;
                RunningOrders.Insert(0, order);
            }
        }

        // returns if order was open or reverse orders was closed
        public bool OpenOrder(
            int bmModelID,
            OrderDirection direction,
            TickPrice price,
            OpenReason openReason,
            out Order order,
            DateTime openTime)
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
                CloseAllOrders(price, CloseReason.ReverseOrderRequested, openTime);
                return true;
            }

            order = new Order(
                localDatabaseConnector.InitiateNewOrderID(),
                localDatabaseConnector.InitiateNewGroupID(),
                1, bmModelID, openTime, openTime, price, direction, 1,
                DefaultLotSize, openReason);
            order.StopLossPips = DefaultStopLossPips;
            order.TakeProfitPips = DefaultTakeProfitPips;
            RunningOrders.Insert(0, order);

            return true;
        }

        public void CloseAllOrders(TickPrice price, CloseReason closeReason, DateTime closeTime)
        {
            for (int orderInd = 0; orderInd < RunningOrders.Count; orderInd++)
            {
                var order = RunningOrders[orderInd];
                CloseOrder(order, price, closeReason, closeTime);
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

                CloseOrder(order, price, closeReason, price.time);
                orderInd--;

                // trading hierarchy
                if (isTradingHierarchyStarted)
                {
                    if (order.openReason == OpenReason.SingularLong || order.openReason == OpenReason.SingularShort)
                    {
                        if (closeReason == CloseReason.StopLossExecuted &&
                            (order.OrderInGroupID == 1 || order.OrderInGroupID == 3))
                        {
                            isTradingHierarchyStarted = false;
                            continue;
                        }
                        if (closeReason == CloseReason.TakeProfitExecuted)
                        {
                            if (order.OrderInGroupID == 1 || order.OrderInGroupID == 2)
                            {
                                foreach (var rOrder in RunningOrders)
                                {
                                    if (rOrder.OrderGroup == order.OrderGroup)
                                    {
                                        rOrder.StopLossPips = (order.OrderInGroupID - 1) * 50;
                                    }
                                }
                            }
                            else if (order.OrderInGroupID == 3)
                            {
                                isTradingHierarchyStarted = false;
                                continue;
                            }
                        }
                    }
                    else if (order.openReason == OpenReason.MLS1 || order.openReason == OpenReason.MLS2 ||
                        order.openReason == OpenReason.MSL1 || order.openReason == OpenReason.MSL2)
                    {
                        if (closeReason == CloseReason.StopLossExecuted &&
                            (order.OrderInGroupID == 1 || order.OrderInGroupID == 4))
                        {
                            isTradingHierarchyStarted = false;
                            continue;
                        }
                        if (closeReason == CloseReason.TakeProfitExecuted)
                        {
                            if (order.OrderInGroupID == 1)
                            {
                                for (int newOrderInd = 2; newOrderInd <= 4; newOrderInd++)
                                {
                                    var newOrder = new Order(
                                        localDatabaseConnector.InitiateNewOrderID(),
                                        order.OrderGroup,
                                        newOrderInd, order.BMModelID, price.time, price.time, price,
                                        order.Direction == OrderDirection.Buy ? OrderDirection.Sell : OrderDirection.Buy,
                                        1, DefaultLotSize, order.openReason);
                                    order.StopLossPips = 60;
                                    order.TakeProfitPips = 100 + (newOrderInd - 2) * 50;
                                    RunningOrders.Insert(0, order);
                                }
                            }
                            else if (order.OrderInGroupID == 2 || order.OrderInGroupID == 3)
                            {
                                foreach (var rOrder in RunningOrders)
                                {
                                    if (rOrder.OrderGroup == order.OrderGroup)
                                    {
                                        rOrder.StopLossPips = (order.OrderInGroupID - 2) * 50;
                                    }
                                }
                            }
                            else if (order.OrderInGroupID == 4)
                            {
                                isTradingHierarchyStarted = false;
                                continue;
                            }
                        }
                    }
                }
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

        public int StopLossValueToPips(OrderDirection direction, TickPrice price, double stopLossValue)
        {
            if (direction == OrderDirection.Buy)
            {
                return (int)Math.Round((price.sell - stopLossValue) / DefaultPipSize);
            }
            else
            {
                return (int)Math.Round((stopLossValue - price.buy) / DefaultPipSize);
            }
        }

        private void CloseOrder(Order order, TickPrice price, CloseReason closeReason, DateTime closeTime)
        {
            var balance = ClosedProfitability + order.GeneralizedProfitability(price);
            order.Close(closeTime, price, closeReason, balance);
            ClosedProfitability += order.Profitability.Value;
            ClosedOrders.Insert(0,order);
            RunningOrders.Remove(order);

            localDatabaseConnector.WriteClosedOrder(order);
        }

    }
}
