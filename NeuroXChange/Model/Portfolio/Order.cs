using NeuroXChange.Model.FixApi;
using System;

namespace NeuroXChange.Model.Portfolio
{
    public class Order
    {
        public bool OrderWasClosed { get; private set; }

        public int OrderID { get; private set; }
        public DateTime OpenTime {get; private set;}
        public double OpenPrice { get; private set; }
        public int Direction { get; private set; }
        public int Value { get; private set; }
        public int LotSize { get; private set; }

        // for closed orders
        public DateTime? CloseTime { get; private set; }
        public double? ClosePrice { get; private set; }
        public CloseReason? closeReason { get; private set; }
        public int? Profitability { get; private set; }

        // account balance on close moment, doesn't need to be set
        public int? AccountBalance { get; set; }

        // StopLoss and TakeProfit values in pips
        public int? StopLossPips { get; set; }
        public int? TakeProfitPips { get; set; }

        // create new running order
        public Order(
            int orderID,
            DateTime openTime,
            int direction,
            double openPrice,
            int value,
            int lotSize)
        {
            this.OrderID = orderID;
            this.OpenTime = openTime;
            this.OrderWasClosed = false;
            this.Direction = direction;
            this.OpenPrice = openPrice;
            this.Value = value;
            this.LotSize = lotSize;
        }

        public Order(
            int orderID,
            DateTime openTime,
            int direction,
            TickPrice price,
            int value,
            int lotSize) : 
            this(orderID, openTime, direction, direction == 0 ? price.buy : price.sell, value, lotSize)
        {
        }

        // If order wasn't closed, returns current profitability for given price
        // price argument should be not null
        // If order was closed, returns profitability on close moment
        // price argument is not used
        public int GeneralizedProfitability(TickPrice price = null)
        {
            if (!OrderWasClosed)
            {
                var currentPrice = Direction == 0 ? price.sell : price.buy;
                var priceDifference = Direction == 0 ? currentPrice - OpenPrice : OpenPrice - currentPrice;
                return (int)(priceDifference * Value * LotSize);
            }
            else
            {
                return Profitability.Value;
            }
        }

        public void Close(
            DateTime closeTime,
            TickPrice closePrice,
            CloseReason closeReason,
            int? accountBalance = null)
        {
            if (OrderWasClosed)
            {
                throw new Exception("Order was already closed!");
            }

            OrderWasClosed = true;
            this.CloseTime = closeTime;
            this.ClosePrice = Direction == 0 ? closePrice.sell : closePrice.buy;
            this.closeReason = closeReason;
            AccountBalance = accountBalance;
            var priceDifference = Direction == 0 ? ClosePrice - OpenPrice : OpenPrice - ClosePrice;
            Profitability = (int)(priceDifference * Value * LotSize);
        }

        // TODO: should PipSize will be class field?
        public CloseReason NeedToBeClosedAccordingToSLorTP(TickPrice price, double PipSize)
        {
            var currentPrice = Direction == 0 ? price.sell : price.buy;
            var priceDiff = Direction == 0 ? currentPrice - OpenPrice : OpenPrice - currentPrice;
            int pipDifference = (int)(priceDiff / PipSize);

            if (StopLossPips.HasValue && pipDifference <= -StopLossPips.Value)
            {
                return CloseReason.StopLossExecuted;
            }

            if (TakeProfitPips.HasValue && pipDifference >= TakeProfitPips.Value)
            {
                return CloseReason.TakeProfitExecuted;
            }

            return CloseReason.ShouldntBeClosed;
        }

    }
}
