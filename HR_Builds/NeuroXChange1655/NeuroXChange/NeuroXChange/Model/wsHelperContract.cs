using NeuroXChange.ExchangeInstrumentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroXChange.Model
{
     public class wsHelperContract
    {
        #region "         MEMBERS          "

        private static wsHelperContract _instance;
        public List<string> LstProductTypes;
        public List<string> LstContract;
        public List<string> LstProducts;
        public Dictionary<string, Dictionary<string, List<string>>> ddProductTypesProductContract;
        public Dictionary<string, InstrumentSpec> ddContractDetails;

        #endregion "      MEMBERS          "

        #region "        PROPERTIES        "

        public static wsHelperContract INSTANCE
        {
            get { return _instance ?? (_instance = new wsHelperContract()); }
        }

        private wsHelperContract()
        {
            try
            {
                LstProductTypes = new List<string>();
                LstContract = new List<string>();
                LstProducts = new List<string>();
                ddContractDetails = new Dictionary<string, InstrumentSpec>();
                ddProductTypesProductContract = new Dictionary<string, Dictionary<string, List<string>>>();

            }
            catch (Exception)
            {
            }
        }

        public void LoadIntialData(int groupid)
        {
            try
            {
                ExchangeInstrumentServiceClient InstService = new ExchangeInstrumentServiceClient();
                InstrumentData[] InstrumentDataArr = InstService.GetInstrumentSpec(groupid);
                foreach (var item in InstrumentDataArr)
                {
                    try

                    {
                        if (!string.IsNullOrEmpty(item.SecurityTypeID) && !LstProductTypes.Contains(item.SecurityTypeID))
                        {
                            LstProductTypes.Add(item.SecurityTypeID);
                        }

                        if (!string.IsNullOrEmpty(item.Symbol) && !LstContract.Contains(item.Symbol))
                        {
                            LstContract.Add(item.Symbol);
                        }

                        if (!string.IsNullOrEmpty(item.Source) && !LstProductTypes.Contains(item.Source))
                        {
                            LstProducts.Add(item.Source);
                        }

                        if (!string.IsNullOrEmpty(item.SecurityTypeID) && !string.IsNullOrEmpty(item.Source) && !string.IsNullOrEmpty(item.Symbol))
                        {
                            if (ddProductTypesProductContract.ContainsKey(item.SecurityTypeID))
                            {
                                if (!ddProductTypesProductContract[item.SecurityTypeID].ContainsKey(item.Source))
                                {
                                    List<string> tempSymb = new List<string>();
                                    tempSymb.Add(item.Symbol);
                                    ddProductTypesProductContract[item.SecurityTypeID].Add(item.Source, tempSymb);
                                }
                                else
                                {
                                    if (!ddProductTypesProductContract[item.SecurityTypeID][item.Source].Contains(item.Symbol))
                                    {
                                        ddProductTypesProductContract[item.SecurityTypeID][item.Source].Add(item.Symbol);
                                    }
                                }
                            }
                            else
                            {
                                List<string> tempSymb = new List<string>();
                                tempSymb.Add(item.Symbol);
                                Dictionary<string, List<string>> tempProduct = new Dictionary<string, List<string>>();
                                tempProduct.Add(item.Source, tempSymb);
                                ddProductTypesProductContract.Add(item.SecurityTypeID.Trim(), tempProduct);

                            }
                        }

                        #region "    Contract Detail       "
                        if (!ddContractDetails.ContainsKey(item.Symbol))
                        {
                            InstrumentSpec inst = new InstrumentSpec();
                            int.TryParse(item.BuySideTurnover, out inst.BuySideTurnover);
                            int.TryParse(item.Digits, out inst.Digits);
                            float.TryParse(item.DPRInitialPercentage, out inst.DPRInitialPercentage);
                            int.TryParse(item.DPRInterval, out inst.DPRIntervalSecs);
                            int.TryParse(item.ContractSize, out inst.ContractSize);
                            int.TryParse(item.FreezeLevel, out inst.FreezeLevel);
                            int.TryParse(item.Hedged, out inst.Hedged);
                            int.TryParse(item.InitialBuyMargin, out inst.InitialBuyMargin);
                            int.TryParse(item.InitialSellMargin, out inst.InitialSellMargin);
                            int.TryParse(item.LimitAndStopLevel, out inst.Limit_Stop_Level);
                            int.TryParse(item.LPID, out inst.LongPositions);
                            int.TryParse(item.MarginCurrency, out inst.MarginCurrency);
                            int.TryParse(item.MarketLot, out inst.MarketLot);
                            int.TryParse(item.MaximumLots, out inst.MaxDPR);
                            int.TryParse(item.MaximumAllowablePosition, out inst.MaximumAllowablePosition);
                            int.TryParse(item.MaximumLots, out inst.MaximumLotsForIE);
                            int.TryParse(item.MaximumLots, out inst.MaxOrderSize);
                            int.TryParse(item.MaximumOrderValue, out inst.MaxOrderValue);
                            int.TryParse(item.Orders, out inst.Orders);
                            int.TryParse(item.QuotationBaseValue, out inst.PriceQuantity);
                            int.TryParse(item.SellSideTurnover, out inst.SellSideTurnover);
                            int.TryParse(item.SpreadBalance, out inst.SpreadBalance);
                            int.TryParse(item.Spread, out inst.SpreadByDefault);
                            int.TryParse(item.TickSize, out inst.TickSize);

                            if (!String.IsNullOrEmpty(item.Symbol))
                            {
                                inst.Contract = item.Symbol;
                            }
                            else
                            {
                                inst.Contract = string.Empty;
                            }
                            if (!String.IsNullOrEmpty(item.DeliveryEndDate))
                            {
                                inst.DeliveryEndDate = item.DeliveryEndDate;
                            }
                            else
                            {
                                inst.DeliveryEndDate = string.Empty;
                            }
                            if (!String.IsNullOrEmpty(item.DeliveryQuantity))
                            {
                                inst.DeliveryQuantity = item.DeliveryQuantity;
                            }
                            else
                            {
                                inst.DeliveryQuantity = string.Empty;
                            }
                            if (!String.IsNullOrEmpty(item.DeliveryStartDate))
                            {
                                inst.DeliveryStartDate = item.DeliveryStartDate;
                            }
                            else
                            {
                                inst.DeliveryStartDate = string.Empty;
                            }
                            if (!String.IsNullOrEmpty(item.DeliveryType))
                            {
                                inst.DeliveryType = item.DeliveryType;
                            }
                            else
                            {
                                inst.DeliveryType = string.Empty;
                            }
                            if (!String.IsNullOrEmpty(item.DeliveryUnit))
                            {
                                inst.DeliveryUnit = item.DeliveryUnit;
                            }
                            else
                            {
                                inst.DeliveryUnit = string.Empty;
                            }
                            if (!String.IsNullOrEmpty(item.EndDate))
                            {
                                inst.EndDate = item.EndDate;
                            }
                            else
                            {
                                inst.EndDate = string.Empty;
                            }
                            if (!String.IsNullOrEmpty(item.ExpiryDate))
                            {
                                inst.ExpiryDate = item.ExpiryDate;
                            }
                            else
                            {
                                inst.ExpiryDate = string.Empty;
                            }
                            if (!String.IsNullOrEmpty(item.TradingGateway))
                            {
                                inst.Gateway = item.TradingGateway;
                            }
                            else
                            {
                                inst.Gateway = string.Empty;
                            }
                            if (!String.IsNullOrEmpty(item.Source))
                            {
                                inst.Product = item.Source;
                            }
                            else
                            {
                                inst.Product = string.Empty;
                            }
                            if (!String.IsNullOrEmpty(item.SecurityTypeID))
                            {
                                inst.ProductType = item.SecurityTypeID;
                            }
                            else
                            {
                                inst.ProductType = string.Empty;
                            }
                            if (!String.IsNullOrEmpty(item.SettlingCurrency))
                            {
                                inst.SettlingCurrency = item.SettlingCurrency;
                            }
                            else
                            {
                                inst.SettlingCurrency = string.Empty;
                            }
                            if (!String.IsNullOrEmpty(item.StartDate))
                            {
                                inst.StartDate = item.StartDate;
                            }
                            else
                            {
                                inst.StartDate = string.Empty;
                            }
                            if (!String.IsNullOrEmpty(item.TenderStartDate))
                            {
                                inst.TenderStartDate = item.TenderStartDate;
                            }
                            else
                            {
                                inst.TenderStartDate = string.Empty;
                            }
                            if (!String.IsNullOrEmpty(item.TenderEndDate))
                            {
                                inst.TenderEndDate = item.TenderEndDate;
                            }
                            else
                            {
                                inst.TenderEndDate = string.Empty;
                            }
                            if (!String.IsNullOrEmpty(item.TradingCurrency))
                            {
                                inst.TradingCurrency = item.TradingCurrency;
                            }
                            else
                            {
                                inst.TradingCurrency = string.Empty;
                            }
                            if (!String.IsNullOrEmpty(item.ULAssest))
                            {
                                inst.UL_Asset = item.ULAssest;
                            }
                            else
                            {
                                inst.UL_Asset = string.Empty;

                            }

                            inst.Information = string.Empty;
                            inst.reserved = string.Empty;
                            inst.Specification = string.Empty;

                            ddContractDetails.Add(item.Symbol, inst);
                        }

                        #endregion "    Contract Detail       "
                    }
                    catch (Exception)
                    {


                    }


                }
                LstProductTypes.Insert(0, "All");
                LstContract.Insert(0, "All");
            }
            catch (Exception ex)
            {
                //ClsCommonMethods.ShowMessageBox(ex.Message);
            }

        }

        #endregion "     PROPERTIES        "

        #region "         METHODS          "


        public List<string> GetProductTypes()
        {
            return LstProductTypes;
        }


        #endregion "         METHODS          "
    }
}
