using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingAccount
{

    public enum AccountStatus { Open =  1, ClosedOnMarginCall = 2, Closed = 3 }


    /// <summary>
    /// Info related to account and ballance (base currentcy, balance, open and closed trades)
    /// </summary>
    public class Account
    {

        #region comment
        /*
         *                              
        +AccountValue = Balance + UnrealizedProfit; changing in live mode depending on price fluctuations;
        
        +UnrealizedProfitLoss - gain or loss from open positons, fluctuationg on price change 
                  (gain or loss on trade close moves to balance)
                  changing in live mode depending on price fluctuations;
        
        +Balance - amount of accounts base currency (money availale for trades and money used in trades) 
                  (initialy  --> Deposit*Leverage)
                  Changes only when trade(s) is closed on gain or loss 
                  (gain or loss on trade close moves to balance and deposit)

        +AvailableForTrade  = Balance - OpenPositionsValue; 
        
        +OpenPositionsValue - amount of accounts currency on open trades 
                              (do not changes on price change only change on trades close or open)


        +Deposit - real money (Changes only when trade(s) is closed on gain or loss devided by leverage)
                     Deposit = Balance / leverage               
        
        +Leverage - Number by which deposit is multiplied.

        +MarginLevel -          MarginLevel - percentage of deposit 
              (for example 10% means that 10% of innitial deposit is safe other can be lost.
              - means if gains or losses are greather that 90% of initial deposit  
              all trades are closed and account is suspended)
        
        +MarginValue = ............  . 

    */
        #endregion

        #region properties

        /// <summary>
        /// 
        /// </summary>
        public AccountStatus AccountStatus { get { return _accountStatus; } private set { } }

        /// <summary>
        /// Balance + UnrealizedProfit; changing in live mode depending on price fluctuations;
        /// </summary>
        public decimal AccountValue { get { return _balance + _unrealisedProfitLoss; } private set { } }

        /// gain or loss from open positons, fluctuationg on price change 
        /// (gain or loss on trade close moves to balance)       
        public decimal UnrealisedProfitLoss { get { return _unrealisedProfitLoss; } private set { } }

        /// <summary>
        /// Balance of virtual money (leveraged deposit), available money for trading
        ///          amount of accounts base currency (money availale for trades and money used in trades) 
        ///          (initialy  --> Deposit* Leverage)
        ///          Changes only when trade(s) is closed on gain or loss
        ///          (gain or loss on trade close moves to balance)
        /// </summary>
        public decimal Balance { get { return _balance; } private set { } }

        /// <summary>
        /// AvailableForTrade  = Balance - OpenPositions; 
        /// </summary>
        public decimal AvailableForTrade { get { return _balance - _openPositionsValue; } private set { } }

        /// <summary>
        /// Amount of accounts currency on open trades 
        /// (do not changes on price change only on trades close or open)
        /// </summary>
        public decimal OpenPositionsValue { get { return _openPositionsValue; } private set { } }

        /// <summary>
        ///  Real money. Changes/Updates on each traded is closed
        ///  on gain or loss is not devided by leverage.
        /// </summary>
        public decimal Deposit { get { return _deposit; } private set { } }

        /// <summary>
        /// How many times to multiply deposit to get leveraged balance 
        /// (Number by which deposit is multiplied)
        /// </summary>
        public int Leverage { get { return _leverage; } private set { } }

        /// <summary>
        /// MarginLevel - percentage of deposit 
        ///  (for example 10% means that 10% of innitial deposit is safe other can be lost.
        ///  - means if gains or losses are greather that 90% of initial deposit  
        ///  all trades are closed and account is suspended)
        /// </summary>
        public decimal MarginLevel { get { return _marginLevel; } private set { } }

        /// <summary>
        /// MarginValue = deposit*leverage * MarginLevel; if this value reached in _accountValue all trades are 
        ///closed for securyt prpose.Defalut value 0. 
        ///if account loss reaches Margin value then trades and account are closed
        /// </summary>
        public decimal MarginValue { get { return _marginValue; } private set { } }

        /// <summary>
        /// Currreccy of an account ussually USD
        /// </summary>
        public string BaseCurrency { get { return _baseCurrency; } private set { } }

        /// <summary>
        /// All Open and closed trades
        /// </summary>
        public List<AbstractTrade> Trades { get { return _trades; } private set { } }


        #endregion

        #region Events

        /// <summary>
        /// Fires when we closing accont due to big losses
        /// </summary>
        /// <param name="a"></param>
        /// <param name="message"></param>
        public delegate void MarginCall(Account a, string message);
        public event MarginCall OnMarginCall;

        public delegate void PriceChange(Account a, string message);
        /// <summary>
        /// After price was received in account, price post receive event
        /// </summary>
        public event PriceChange OnPriceChange;


        public delegate void TradeClose(Account a, string message);
        /// <summary>
        /// After trade was closed in account, price post receive event
        /// </summary>
        public event PriceChange OnTradeClose;

        #endregion

        #region private vars


        /// <summary>
        /// 
        /// </summary>
        private AccountStatus _accountStatus;

        /// <summary>
        /// gain or loss from open positons, fluctuationg on price change 
        /// (gain or loss on trade close moves to balance)
        /// </summary>
        private decimal _unrealisedProfitLoss;

        /// <summary>
        /// Balance of virtual money (leveraged deposit), available money for trading
        /// </summary>
        private decimal _balance; // 10 000 USD  - leveraged deposit

        /// <summary>
        /// AvailableForTrade  = Balance - OpenPositions; 
        /// </summary>
        // private decimal _availableForTrade; -- calculated from private vars

        /// <summary>
        /// USD in Open possitions, Amount of base currency in open trades this is not deviate according to currency fluctuations
        /// Amount of accounts currency on open trades 
        /// (do not changes on price change only on trades close or open)
        /// </summary>
        private decimal _openPositionsValue;

        /// <summary>
        ///  Real initial amoutn of money on account opening. 
        ///  
        /// </summary>
         private decimal _initialDeposit;

        /// <summary>
        /// Fluctuating deposit depending on open trades and price. Depending on success of trading.
        /// profit of loss is not leveraged
        /// Changes only when trades are closed.
        /// </summary>
        private decimal _deposit;

        /// <summary>
        /// How many times to multiply deposit to get leveraged balance  
        /// </summary>
        private int _leverage;

        /// <summary>
        /// MarginLevel - percentage of balance 
        ///  (10% - means if account value (in _accountValue) reaches 10% 
        ///  or inital acount balance all trades are closed )
        /// </summary>
        private decimal _marginLevel;

        /// <summary>
        /// MarginValue = deposit*leverage * MarginLevel; if this value reached in _accountValue all trades are 
        ///closed for securyt prpose.Defalut value 0. 
        ///if account loss reaches Margin value then trades and account are closed
        /// </summary>
        private decimal _marginValue;

        /// <summary>
        /// Currreccy of an account ussually USD
        /// </summary>
        private string _baseCurrency;

        /// <summary>
        /// List of trades open and closed
        /// </summary>
        private List<AbstractTrade> _trades;

        /// <summary>
        /// Id generator/counted or Unique trade IDs
        /// </summary>
        private int _tradeID = 0;

        #endregion


        public Account()
        {
            /// lets set initial values for trading game
            /// 
            _accountStatus = AccountStatus.Open;

            _baseCurrency = "USD";
            _initialDeposit = 100; // deposit 100USD
            _deposit = _initialDeposit;
            _leverage = 100;
            _balance = _initialDeposit * _leverage; // 10 000

            _trades = new List<AbstractTrade>();  // trades

            _openPositionsValue = 0;  // Amount in cash in open positions


        }

        public Account(string baseCurrency, decimal deposit, int leverage, decimal marginLevel)
        {
            _accountStatus = AccountStatus.Open;
            _baseCurrency = baseCurrency;
            _initialDeposit = deposit; // deposit 100USD  // changes on trade close. Or on margin call            
            _deposit = _initialDeposit;
            _leverage = leverage;
            _balance = _initialDeposit * _leverage; // 10 000  // changes on trade close. Or on margin call  
            _marginLevel = marginLevel; // value in percents
            _marginValue = _marginLevel/100 * _initialDeposit; // if account loss reaches Margin value then trades and account are closed

            
            


            AccountValue = _balance; // initialy
            _unrealisedProfitLoss = 0; // initialy
            AvailableForTrade = _balance; //initialy
            _openPositionsValue = 0; //initialy, Amount in cash in open positions 


            _trades = new List<AbstractTrade>();  // trades
        }

        /// <summary>
        /// Receives price change and Updades Account trades accordignly to a price change.
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="price"></param>
        public void priceChange(Price price)
        {

            /// UpdateTrades and UpdateAccountValues comes together it is like one trasaction.
            /// It could be taken approach that on each trade update folows account update but for now it is only when all trades updated acount is updated
            if (this.AccountStatus == AccountStatus.Open)
            {
                UpdateTradesPriceChange(price, _trades); // update for each open trade gain/loss 
                _unrealisedProfitLoss = UpdateAccountGainLoss(_trades); // calculare Account gain/loss 

                if (MarginValue >= Deposit + _unrealisedProfitLoss) // Closing account due to big losses
                {
                    foreach (AbstractTrade at in _trades)
                    {
                        CloseTradeUpdateAccount(at, false);
                    }
                    _accountStatus = AccountStatus.ClosedOnMarginCall;
                    // Rise eVentt margin call.
                    OnMarginCall?.Invoke(this, "Not enogth resources to cover price change!");
                }

                if (OnPriceChange != null)
                {
                    OnPriceChange(this, "Price Change!");
                }
            }

        }

        /// <summary>
        ///  Add new trade if enouth deposit and free margin. Update account figures. 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="amount"></param>
        /// <param name="price"></param>
        public bool AddTradeUpdateAccount(AbstractTrade t)
        {
            if ((AvailableForTrade > t.TotalPayment)&&(this.AccountStatus == AccountStatus.Open)) // if still available funds for new trade
            {                
                _openPositionsValue += t.TotalPayment; // update value in open positions
                _trades.Add(t);
                return true;
            }
            else return false;                         
        }


        /// <summary>
        /// Close trade. Update account values when trade is closed add gain/loss to balance.
        /// </summary>
        /// <param name="TradeID"></param>
        /// <param name="fireEvent"></param>
        public void CloseTradeUpdateAccount(int TradeID, bool fireEvent)
        {

            var trad = _trades.Find(tradde => tradde.TradeID == TradeID);
            CloseTradeUpdateAccount(trad, fireEvent);
            
        }

        /// <summary>
        /// Close trade. Update account values when trade is closed add gain/loss to balance.
        /// </summary>
        /// <param name="TradeID"></param>
        public void CloseTradeUpdateAccount(int TradeID)
        {

            var trad = _trades.Find(tradde => tradde.TradeID == TradeID);
            CloseTradeUpdateAccount(trad, true);

        }




        /// <summary>
        /// Close trade. Update account values when trade is closed add gain/loss to balance.
        /// </summary>
        /// <param name="TradeID"></param>
        public void CloseTradeUpdateAccount(AbstractTrade t, bool fireEvent)
        {
            if (t.TradeStatus != Status.Closed)
            {
                t.Close(); // use this methos only when prices and trade values updated automaticaly
                _balance += t.BaseGainLoss; // add profit or loss to balance
                _deposit += t.BaseGainLoss;
                _unrealisedProfitLoss -= t.BaseGainLoss;
                _openPositionsValue -= t.TotalPayment; //BaseTradeValue - t.BaseGainLoss; // position is closed, open positions now is less by initial value of trade
                if (fireEvent)
                {
                    OnTradeClose?.Invoke(this, "Trade is Closed");
                }
            }
        }


        public void CloseTradeUpdateAccount(AbstractTrade t)
        {
            CloseTradeUpdateAccount(t, true);
        }


        /// <summary>
        /// Recalculate open trades values to a new price change
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="priceAsk"></param>
        /// <param name="priceBid"></param>
        /// <param name="trades"></param>
        private void UpdateTradesPriceChange(Price price, List<AbstractTrade> trades)
        {
            foreach(AbstractTrade at in trades)
            {
                if (at.TradeStatus == Status.Open)
                UpdateTradePriceChange(price, at);
            }
        }

        /// <summary>
        /// Recalculate trade value  to a
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="priceAsk"></param>
        /// <param name="priceBid"></param>
        /// <param name="trade"></param>
        private void UpdateTradePriceChange(Price price, AbstractTrade trade)
        {
            if (trade.TradeStatus == Status.Open) {
                trade.UpdateTradeValue(price); // recalculate trade value
            }
        }

        /// <summary>
        /// Method is executed to recalculate value of account of open trades when price changes
        /// it updates account unrealized gain/loss
        /// returns gain/loss
        /// </summary>
        /// <param name="trades"></param>
        private decimal UpdateAccountGainLoss(List<AbstractTrade> trades)
        {
            decimal gainLoss = 0M;
            foreach(AbstractTrade at in trades)
            {
                /// Closed trades already calculated in balance so we will skip them (ets count only open ones)
                if (at.TradeStatus == Status.Open)
                {
                    gainLoss += at.GainLoss;
                }
            }
             return gainLoss;   
                     
        }


    }
}
