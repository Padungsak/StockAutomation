using i2TradePlus.Information;
using i2TradePlus.MyDataSet;
using ITSNet.Common.BIZ;
using ITSNet.Common.BIZ.RealtimeMessage;
using ITSNet.Common.BIZ.RealtimeMessage.TFEX;
using System;
using System.Runtime.CompilerServices;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;

namespace i2TradePlus
{
    internal class AutoTradeManager : IRealtimeMessage
    {
        internal delegate void OnStartAutoTradeHandler(string message);
        internal delegate void OnEndAutoTradeHandler();

        private BackgroundWorker bgwAutoSendOrder = null;
        private List<AutoTradeItem> _itemList;
        private AutoTradeItem _currentAutoItem = null;
        private static AutoTradeManager instance = null;
        internal static AutoTradeManager Instance
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                if (AutoTradeManager.instance == null)
                {
                    AutoTradeManager.instance = new AutoTradeManager();
                }
                return AutoTradeManager.instance;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                AutoTradeManager.instance = value;
            }
        }

        internal AutoTradeManager.OnStartAutoTradeHandler _OnStartAutoTrade;
        internal event AutoTradeManager.OnStartAutoTradeHandler OnStartAutoTrade
        {
            [MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
            add
            {
                this._OnStartAutoTrade += value;
            }
            [MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
            remove
            {
                this._OnStartAutoTrade -= value;
            }
        }

        internal AutoTradeManager.OnEndAutoTradeHandler _OnEndAutoTrade;
        internal event AutoTradeManager.OnEndAutoTradeHandler OnEndAutoTrade
        {
            [MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
            add
            {
                this._OnEndAutoTrade += value;
            }
            [MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
            remove
            {
                this._OnEndAutoTrade -= value;
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal AutoTradeManager()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Init()
        {
            try
            {
                UpdateItemList();
                this.bgwAutoSendOrder = new BackgroundWorker();
                this.bgwAutoSendOrder.WorkerReportsProgress = true;
                this.bgwAutoSendOrder.DoWork += new DoWorkEventHandler(this.bgwAutoSendOrder_DoWork);
                this.bgwAutoSendOrder.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwAutoSendOrder_RunWorkerCompleted);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void bgwAutoSendOrder_DoWork(object sender, DoWorkEventArgs e)
        {
            if (ApplicationInfo.IsEquityAccount)
            {
                try
                {
                     StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[Convert.ToInt32(_currentAutoItem.StockNumber.ToString())];
                     
                     if (this._OnStartAutoTrade != null)
                     {
                         this._OnStartAutoTrade("Start sending auto order of " + stockInformation.Symbol);
                         //System.Threading.Thread.Sleep(2000);
                     }

                     ApplicationInfo.SendNewOrderResult newOrderRet = ApplicationInfo.SendNewOrder(stockInformation.Symbol,
                                                                                                     _currentAutoItem.OrdSide,
                                                                                                     _currentAutoItem.OrdVolume,
                                                                                                     _currentAutoItem.OrdPrice.ToString(),
                                                                                                     _currentAutoItem.OrdPubvol,
                                                                                                     _currentAutoItem.OrdCondition,
                                                                                                     0,
                                                                                                     "");
                     if (newOrderRet.OrderNo > 0L)
                     {
                         ApplicationInfo.AddOrderNoToAutoRefreshList(newOrderRet.OrderNo.ToString(), newOrderRet.IsFwOfflineOrder ? 3 : 1);
                     }
                     else
                     {
                         //Indeicate some error message
                     }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void bgwAutoSendOrder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string data = string.Empty;
                try
                {
                    data = ApplicationInfo.WebAlertService.SendCancelStopOrder(ApplicationInfo.UserLoginID, 
                                                                                _currentAutoItem.RefNo,
                                                                                _currentAutoItem.StockNumber,
                                                                                ApplicationInfo.AuthenKey);

                    if (this._OnEndAutoTrade != null)
                    {
                        System.Threading.Thread.Sleep(2000);
                        UpdateItemList();
                        this._OnEndAutoTrade();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void UpdateItemList()
        {
            if (_itemList == null)
            {
                _itemList = new List<AutoTradeItem>();
            }
            else
            {
                _itemList.Clear();
            }
            try
            {
                string data = ApplicationInfo.WebAlertService.ViewStopOrder(ApplicationInfo.UserLoginID, ApplicationInfo.AccInfo.CurrentAccount, 0);
                using (DataSet dataSet = new DataSet())
                {
                    MyDataHelper.StringToDataSet(data, dataSet);
                    if (dataSet.Tables.Contains("ORDERS") && dataSet.Tables["ORDERS"].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataSet.Tables["ORDERS"].Rows)
                        {
                            if (dr["status"].ToString() != "X")
                            {
                                AutoTradeItem item = new AutoTradeItem();
                                item.RefNo = int.Parse(dr["ref_no"].ToString());
                                item.StockNumber = int.Parse(dr["stock"].ToString());
                                item.FieldType = int.Parse(dr["field_type"].ToString());
                                item.OperatorType = int.Parse(dr["operator_type"].ToString());
                                item.Price = decimal.Parse(Utilities.PriceFormat(dr["price"].ToString()));
                                item.OrdSide = dr["ord_side"].ToString();
                                item.OrdVolume = int.Parse(dr["ord_volume"].ToString());
                                item.OrdPubvol = int.Parse(dr["ord_pubvol"].ToString());
                                item.OrdPrice = decimal.Parse(Utilities.PriceFormat(dr["ord_price"].ToString()));
                                item.OrdCondition = dr["ord_condition"].ToString();
                                item.Status = dr["status"].ToString();
                                item.Time = int.Parse(dr["time"].ToString());
                                //item.Mtime = int.Parse(dr["mtime"].ToString());
                                item.OrderNumber = int.Parse(dr["order_number"].ToString());
                                item.Limit = dr["limit"].ToString();
                                _itemList.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ReceiveMessage(IBroadcastMessage message, StockList.StockInformation realtimeStockInfo)
        {
            if (!ApplicationInfo.StopOrderSupported)
            {
                return;
            }
            try
            {
                string messageType = message.MessageType;
                if (messageType != null)
                {
                    if (messageType == "L+")
                    {
                        this.Execute((LSAccumulate)message, realtimeStockInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ReceiveTfexMessage(IBroadcastMessage message, SeriesList.SeriesInformation realtimeSeriesInfo)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Execute(LSAccumulate msgLS, StockList.StockInformation realtimeStockInfo)
        {
            foreach (AutoTradeItem item in this._itemList)
            {
                if (item.StockNumber == msgLS.SecurityNumber && 
                    item.Status == "PO")
                {
                    if (IsConditionMeet(msgLS, item))
                    {
                        if (!this.bgwAutoSendOrder.IsBusy)
                        {
                            _currentAutoItem = item;
                            this.bgwAutoSendOrder.RunWorkerAsync();
                        }                      
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool IsConditionMeet(LSAccumulate msgLS, AutoTradeItem autoItem)
        {
            decimal leftOperand = -1;
            if (autoItem.FieldType == 1 || autoItem.FieldType == 4 || autoItem.FieldType == 5 || autoItem.FieldType == 6)
            {
                leftOperand = msgLS.LastPrice;
            }

            decimal rightOperator = autoItem.Price;

            int operatorType = autoItem.OperatorType;
            switch (operatorType)
            {
                case 1:
                    return (leftOperand >= rightOperator);
                case 2:
                    return (leftOperand <= rightOperator);
                case 3:
                    return (leftOperand > rightOperator);
                case 4:
                    return (leftOperand < rightOperator);
            }

            return false;
        }
    }
}
