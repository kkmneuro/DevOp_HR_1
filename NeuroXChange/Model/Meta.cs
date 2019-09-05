using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroXChange.Model
{
    public struct userDetails
    {
        public string UserName;
        public string Password;
        public double Version;
        public string SenderID;
        public int msgtype;
    };

    public struct LogoutRequest
    {
        public string UserName;
        public int msgtype;
    };

    public struct logonResponce
    {
        public string AccountType;
        public string BrokerName;
        public int IsLive;
        public string Reason;
        public int msgtype;
    };

    public struct AccountDetails
    {
        public long AccountID;
        public string AccountType;
        public int Active;
        public double Balance;
        public double BuySideTurnOver;
        public double FreeMargin;
        public int Group;
        public int HedgingType;
        public int Leverage;
        public double LotSize;
        public double Margin;
        public int MarginCall1;
        public int MarginCall2;
        public int MarginCall3;
        public int NoOfParticipants;
        public double ReservedMargin;
        public double SellSideturnOver;
        public string TraderName;
        public double UsedMargin;
        // public string UserName;
        public int msgtype;
    }

    class ParticipantCollection
    {
        public int NoOfParticipants { get; set; }
        public string UserName { get; set; }
        public int islastPck;
        public List<AccountDetails> accountInfo { get; set; }
    }

    public struct SocketResponce
    {
        public int msgtype;
    };

    public struct participantRequest
    {
        public string UserName;
        public int msgtype;
    };

    public class QuotesStream
    {
        public int NoOfSymbols;
        public int msgtype;
        public int islastPck;
        public List<Quote> QuotesItem;
    }

    public class SnapShot
    {
        public double NoOfSymbols;
        public List<QuoteSnapShot> OHLC;
        public uint islastPck;
        public int msgtype;
    }

    public class QuoteSnapShot
    {
        public double Close;
        public string Contract;
        public string Gateway;
        public double High;
        public double LastPrice;
        public uint LastSize;
        public double LastTime;
        public double LastUpdatedTime;
        public double Low;
        public double Open;
        public uint MarketDepthLevel;
        public string Product;
        public string ProductType;
        public string UserName;
        public uint Volume;
        public double WeeksHigh52;
        public double WeeksLow52;
        public List<MarketDepthItem> MarketDepth;
    }

    public class MarketDepthItem
    {
        public uint Level;
        public double BidPrice;
        public double AskPrice;
        public uint BidSize;
        public uint AskSize;
        public string BidTime;
        public string AskTime;
    }



    public class InstrumentSpec
    {
        public string ProductType = string.Empty;
        public string Product = string.Empty;
        public string Contract = string.Empty;
        public string Gateway = string.Empty;
        public string reserved = string.Empty;
        public string Information = string.Empty;
        public string UL_Asset = string.Empty;
        public string TradingCurrency = string.Empty;
        public string SettlingCurrency = string.Empty;
        public string DeliveryUnit = string.Empty;
        public string DeliveryQuantity = string.Empty;
        public string Percentage = string.Empty;
        public string Specification = string.Empty;
        public string DeliveryType = string.Empty;
        public string ExpiryDate = string.Empty;
        public int MarketLot;
        public int PriceTick;
        public int PriceNumerator;
        public int PriceDenominator;
        public int GeneralNumerator;
        public int GeneralDenominator;
        public int PriceQuantity;
        public int InitialBuyMargin;
        public int InitialSellMargin;
        public int OtherBuyMargin;
        public int OtherSellMargin;
        public int MaxOrderSize;// (In Lots)
        public int MaxOrderValue;
        public int MinDPR;
        public int MaxDPR;
        public int Digits;
        public int MarginCurrency;
        public int MaximumLotsForIE;
        public int Orders;
        public int SpreadByDefault;
        public int BuySideTurnover;
        public int SellSideTurnover;
        public int MaximumAllowablePosition;
        public int QuotationBaseValue;
        public string StartDate = string.Empty;
        public string EndDate = string.Empty;
        public string TenderStartDate = string.Empty;
        public string TenderEndDate = string.Empty;
        public string DeliveryStartDate = string.Empty;
        public string DeliveryEndDate = string.Empty;
        public int Charges;
        public float DPRInitialPercentage;
        public int DPRIntervalSecs;
        public int Limit_Stop_Level;
        public int SpreadBalance;
        public int FreezeLevel;
        public int ContractSize;
        public int Hedged;
        public int TickSize;
        public int MarginCall1;
        public int MarginCall2;
        public int MarginCall3;
        public int LongPositions;
        public int ShortPositions;


    }

    public class Quote
    {
        public string Contract;
        public string Gateway;
        public string Product;
        public string ProductType;
        public string QuotesStreamType;
        public uint MarketLevel;
        public double Price;
        public long Size;
        public string Time;
        public string UserName;
    }

    public class SymbolSubscribeRequest
    {
        public int NoOfSymbols;
        public SubscribeRequestType isForSubscribe;
        public List<SymbolInfo> Symbol;
        public int msgtype;


    }

    public class BioDataRequest
    {
        public long Acc;
        public int NR;
        public List<BioDataValues> BDV;
        public int msgtype;

    }

    [Serializable]
    public class BioDataValues
    {
        public string BT;
        public double Tmp;
        public double HB;
        public double SC;
        public double X;
        public double Y;
        public double Z;
        public int TT;
        public int TS;
        public int AS;
    }




    [Serializable]
    public class SymbolInfo
    {

        public string Contract = string.Empty;
        public char ProductType = '0';
        public string Product = string.Empty;
        public string Gateway = string.Empty;
    }

    public enum SubscribeRequestType : int
    {
        UNSUBSCRIBE = 0,
        SUBSCRIBE = 1

    }

    [Serializable]
    public class Symbol
    {
        public const string _Seperator = "_";
        public string Contract = string.Empty;
        public string Product = string.Empty;
        public string ProductType = string.Empty;
        public string Gateway = string.Empty;
        public Quote QuotesItem = null;
        /// <summary>
        /// KEY = GATEWAY_PRODUCTTYPE_PRODUCT_CONTRACT
        /// </summary>
        public string KEY
        {
            get
            {

                return
                        Gateway + _Seperator + GetProductTypeinChar(ProductType).ToString() + _Seperator +
                       Product + _Seperator +
                       Contract;
            }
        }
        public static List<string> getKey(InstrumentSpec spec)
        {

            List<string> _lstKey = new List<string>();
            if (spec != null)
            {
                string gateways = string.Empty;
                gateways = spec.Gateway;
                List<string> arr = new List<string>();
                if (spec.Gateway.Contains(','))
                {
                    arr.AddRange(gateways.Split(','));
                }
                if (spec.Gateway.Contains('_'))
                {
                    arr.AddRange(gateways.Split('_'));
                }
                if (arr.Count == 0)
                {

                    _lstKey.Add(gateways + _Seperator + GetProductTypeinChar(spec.ProductType).ToString() + _Seperator +
                               spec.Product + _Seperator +
                               spec.Contract);
                }
                else
                {
                    for (int i = 0; i < arr.Count; i++)
                    {
                        _lstKey.Add(arr[i] + _Seperator + GetProductTypeinChar(spec.ProductType).ToString() + _Seperator +
                               spec.Product + _Seperator +
                               spec.Contract);
                    }
                }
                return _lstKey;

            }
            else
                return _lstKey;
        }
        /// <summary>
        /// To get the symbol object from key
        /// </summary>
        /// <param name="key"> PROVIDER_EXCHANGE_PRODUCTTYPE_PRODUCT_CONTRACT OR PRODUCTTYPE_PRODUCT_CONTRACT</param>
        /// <returns></returns>
        public static Symbol GetSymbol(string key)
        {
            Symbol sym = null;
            if (key != string.Empty)
            {
                sym = new Symbol();
                string[] arr = key.Split(new string[] { _Seperator }, StringSplitOptions.RemoveEmptyEntries);

                sym.Gateway = arr[0];
                sym.ProductType = getProductTypeInString(arr[1].ToCharArray()[0]);
                sym.Product = arr[2];
                sym.Contract = arr[3];
            }
            return sym;
        }
        public static string getProductTypeInString(char ProductType)
        {
            product prd = (product)ProductType;
            return prd.ToString();
        }
        public static char GetProductTypeinChar(string strProductType)
        {
            try
            {
                return (char)((product)Enum.Parse(typeof(product), strProductType, true));
            }
            catch
            {
                return (char)product.AUCTION;

            }
        }

    }

    public enum product : int
    {
        FUTURE = '1',
        OPTION = '2',
        EQUITY = '3',
        SPOT = '4',
        BOND = '5',
        FOREX = '0',
        PHYSICAL = '6',
        AUCTION = '7',
        INDEXES = '8',
        CFD = '9',
    }

    public enum LoginStatus
    {
        Success,
        Failure
    }

    public static class Globals
    {
        public static LoginStatus CurrentLoginStatus;
        public static string CurrentUserId;
        public static string SelectedPort;
        //public static DataTable TrainingTable;
        public static DataTable TradeTable;
        public static DataTable SymbolTable;
        public static long AccountId;
        public static bool ApplicationStop = false;
        public static bool StartRecording = false;
        public static bool TimerReset = false;
        public static bool FirstTick = true;
        private static NeuroTraderLoggerReference.LoggerClient _logger;
        public static NeuroTraderLoggerReference.LoggerClient LoggerClient
        {
            get
            {
                if (_logger == null)
                    _logger = new NeuroTraderLoggerReference.LoggerClient();
                return _logger;
            }
        }






        public static NeuroTraderLoggerReference.LoggerClient loggerClient = new NeuroTraderLoggerReference.LoggerClient();
        // public static DataTable SymbolTableTrade;

        public static void BuildTableSchema()
        {

            TradeTable = new DataTable();

            TradeTable.Columns.Add("CONTRACT", typeof(String));
            TradeTable.Columns.Add("CLOSE", typeof(String));
            TradeTable.Columns.Add("OPEN", typeof(String));
            TradeTable.Columns.Add("SELL", typeof(String));
            TradeTable.Columns.Add("BUY", typeof(String));
            TradeTable.Columns.Add("HIGH", typeof(String));
            TradeTable.Columns.Add("LTP", typeof(String));
            TradeTable.Columns.Add("LOW", typeof(String));
            TradeTable.Columns.Add("SIZE", typeof(String));
            TradeTable.Columns.Add("VOL", typeof(String));
            TradeTable.PrimaryKey = new DataColumn[] { TradeTable.Columns["CONTRACT"] };

            SymbolTable = new DataTable();
            SymbolTable.Columns.Add("CONTRACT", typeof(String));
            SymbolTable.Columns.Add("GATEWAY", typeof(String));
            SymbolTable.Columns.Add("PRODUCT", typeof(String));
            SymbolTable.Columns.Add("PRODUCTTYPE", typeof(String));
            SymbolTable.PrimaryKey = new DataColumn[] { SymbolTable.Columns["CONTRACT"] };

            //SymbolTableTrade = SymbolTable.Clone();

            //TradeTable = TrainingTable.Clone();

        }
    }




}
