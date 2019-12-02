using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketData
{
    public interface IMarketData
    {

        event PriceTickEventHandler OnPriceTick; //live price tich event

        List<Future> getAvailableFutures(); // Gets the list of available futures
        PriceList getFutureData(Future future, TimeFrame.TF frame, DateTime from); // gets price list
    }
}
