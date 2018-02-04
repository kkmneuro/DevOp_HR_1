using NeuroXChange.Model.FixApi;
using System;
using System.Data.OleDb;

namespace NeuroXChange.Model.Portfolio
{
    public class Order
    {
        [System.ComponentModel.DisplayName("Order state")]
        public OrderState orderState { get; private set; }


        [System.ComponentModel.DisplayName("Order ID")]
        public int OrderID { get; private set; }

        [System.ComponentModel.DisplayName("Order group")]
        public int OrderGroup { get; private set; }

        [System.ComponentModel.DisplayName("Order in group ID")]
        public int OrderInGroupID { get; private set; }

        [System.ComponentModel.DisplayName("BM Model ID")]
        public int BMModelID { get; private set; }

        [System.ComponentModel.DisplayName("Place time")]
        public DateTime PlaceTime { get; private set; }

        [System.ComponentModel.DisplayName("Open time")]
        public DateTime OpenTime {get; private set;}

        [System.ComponentModel.DisplayName("Open price")]
        public double OpenPrice { get; private set; }

        [System.ComponentModel.DisplayName("Direction")]
        public OrderDirection Direction { get; private set; }

        [System.ComponentModel.DisplayName("Value")]
        public int Value { get; private set; }

        [System.ComponentModel.DisplayName("Lot size")]
        public int LotSize { get; private set; }

        [System.ComponentModel.DisplayName("Open reason")]
        public OpenReason openReason { get; private set; }


        // for closed orders

        [System.ComponentModel.DisplayName("Close time")]
        public DateTime? CloseTime { get; private set; }

        [System.ComponentModel.DisplayName("Close price")]
        public double? ClosePrice { get; private set; }

        [System.ComponentModel.DisplayName("Close reason")]
        public CloseReason? closeReason { get; private set; }

        [System.ComponentModel.DisplayName("Profitability")]
        public int? Profitability { get; private set; }


        // account balance on close moment, doesn't need to be set
        [System.ComponentModel.DisplayName("Cumulative balance")]
        public int? CumulativeBalance { get; set; }


        // StopLoss and TakeProfit values in pips

        [System.ComponentModel.DisplayName("Stop loss pips")]
        public int? StopLossPips { get; set; }

        [System.ComponentModel.DisplayName("Take profit pips")]
        public int? TakeProfitPips { get; set; }

        // create new running order
        public Order(
            int orderID,
            int orderGroup,
            int orderInGroupId,
            int bmModelID,
            DateTime placeTime,
            DateTime openTime,
            double openPrice,
            OrderDirection direction,
            int value,
            int lotSize,
            OpenReason openReason)
        {
            this.OrderID = orderID;
            this.OrderGroup = orderGroup;
            this.OrderInGroupID = orderInGroupId;
            this.BMModelID = bmModelID;
            this.PlaceTime = placeTime;
            this.OpenTime = openTime;
            this.orderState = OrderState.Running;
            this.Direction = direction;
            this.OpenPrice = openPrice;
            this.Value = value;
            this.LotSize = lotSize;
            this.openReason = openReason;
        }

        public Order(
            int orderID,
            int orderGroup,
            int orderInGroupId,
            int bmModelID,
            DateTime placeTime,
            DateTime openTime,
            TickPrice openPrice,
            OrderDirection direction,
            int value,
            int lotSize,
            OpenReason openReason) : 
            this(orderID, orderGroup, orderInGroupId, bmModelID, placeTime, openTime, direction == OrderDirection.Buy ? openPrice.buy : openPrice.sell, direction, value, lotSize, openReason)
        {
        }

        public Order(OleDbDataReader reader)
        {
            orderState = OrderState.Closed;
            OrderID = Int32.Parse(reader["ID"].ToString());
            OrderGroup = Int32.Parse(reader["OrderGroup"].ToString());
            OrderInGroupID = Int32.Parse(reader["OrderInGroupID"].ToString());
            BMModelID = Int32.Parse(reader["BMModelID"].ToString());
            PlaceTime = DateTime.Parse(reader["PlaceTime"].ToString());
            OpenTime = DateTime.Parse(reader["OpenTime"].ToString());
            OpenPrice = Double.Parse(reader["OpenPrice"].ToString());
            Direction = (OrderDirection) Int32.Parse(reader["Direction"].ToString());
            Value = Int32.Parse(reader["Value"].ToString());
            LotSize = Int32.Parse(reader["LotSize"].ToString());
            openReason = (OpenReason)Int32.Parse(reader["OpenReason"].ToString());
            CloseTime = DateTime.Parse(reader["CloseTime"].ToString());
            ClosePrice = Double.Parse(reader["ClosePrice"].ToString());
            closeReason = (CloseReason)Double.Parse(reader["CloseReason"].ToString());
            Profitability = Int32.Parse(reader["Profitability"].ToString());
            CumulativeBalance = null;
            StopLossPips = Int32.Parse(reader["StopLossPips"].ToString());
            TakeProfitPips = Int32.Parse(reader["TakeProfitPips"].ToString());
        }

        // If order wasn't closed, returns current profitability for given price
        // price argument should be not null
        // If order was closed, returns profitability on close moment
        // price argument is not used
        public int GeneralizedProfitability(TickPrice price = null)
        {
            if (orderState == OrderState.Running)
            {
                var currentPrice = Direction == OrderDirection.Buy ? price.sell : price.buy;
                var priceDifference = Direction == OrderDirection.Buy ? currentPrice - OpenPrice : OpenPrice - currentPrice;
                return (int)(priceDifference * Value * LotSize);
            }
            else if (orderState == OrderState.Closed)
            {
                return Profitability.Value;
            } else
            {
                throw new Exception("Can't calculate profitability for pending orders!");
            }
        }

        public void Close(
            DateTime closeTime,
            TickPrice closePrice,
            CloseReason closeReason,
            int? accountBalance = null)
        {
            if (orderState == OrderState.Pending)
            {
                return;
            }
            if (orderState == OrderState.Closed)
            {
                return;
            }

            orderState = OrderState.Closed;
            this.CloseTime = closeTime;
            this.ClosePrice = Direction == OrderDirection.Buy ? closePrice.sell : closePrice.buy;
            this.closeReason = closeReason;
            CumulativeBalance = accountBalance;
            var priceDifference = Direction == OrderDirection.Buy ? ClosePrice - OpenPrice : OpenPrice - ClosePrice;
            Profitability = (int)(priceDifference * Value * LotSize);
        }

        // TODO: should PipSize will be class field?
        public CloseReason NeedToBeClosedAccordingToSLorTP(TickPrice price, double PipSize)
        {
            var currentPrice = Direction == OrderDirection.Buy ? price.sell : price.buy;
            var priceDiff = Direction == OrderDirection.Buy ? currentPrice - OpenPrice : OpenPrice - currentPrice;
            int pipDifference = (int)(priceDiff / PipSize);

            if (StopLossPips.HasValue && pipDifference <= -StopLossPips.Value)
            {
                return CloseReason.StopLossExecuted;
            }

            if (TakeProfitPips.HasValue && pipDifference >= TakeProfitPips.Value)
            {
                return CloseReason.TakeProfitExecuted;
            }

            // TODO: implement trailing stops!

            return CloseReason.ShouldntBeClosed;
        }

    }
}
