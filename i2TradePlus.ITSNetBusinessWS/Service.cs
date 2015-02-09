using i2TradePlus.Properties;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
namespace i2TradePlus.ITSNetBusinessWS
{
	[GeneratedCode("System.Web.Services", "2.0.50727.5483"), DesignerCategory("code"), DebuggerStepThrough, WebServiceBinding(Name = "ServiceSoap", Namespace = "http://tempuri.org/")]
	public class Service : SoapHttpClientProtocol
	{
		private SendOrPostCallback CountOrderCancelForDumpOperationCompleted;
		private SendOrPostCallback ViewCustomerByStockForDumpOperationCompleted;
		private SendOrPostCallback GetBrokerMarginVolumeOperationCompleted;
		private SendOrPostCallback VerifyPincodeOperationCompleted;
		private SendOrPostCallback VerifyPincode2OperationCompleted;
		private SendOrPostCallback SendBAMessageOperationCompleted;
		private SendOrPostCallback StockHistoryOperationCompleted;
		private SendOrPostCallback StockChartOperationCompleted;
		private SendOrPostCallback StockHistDataOperationCompleted;
		private SendOrPostCallback LoadStockNicknameOperationCompleted;
		private SendOrPostCallback SaveStockNicknameOperationCompleted;
		private SendOrPostCallback DeleteStockNicknameOperationCompleted;
		private SendOrPostCallback LoadStockNicknameExtraOperationCompleted;
		private SendOrPostCallback SaveStockNicknameExtraOperationCompleted;
		private SendOrPostCallback DeleteStockNicknameExtraOperationCompleted;
		private SendOrPostCallback IntradayChartOperationCompleted;
		private SendOrPostCallback IntradayIndexChartOperationCompleted;
		private SendOrPostCallback GetChartImageOperationCompleted;
		private SendOrPostCallback GetSetIndexChartImageOperationCompleted;
		private SendOrPostCallback GetPortfolioStatAllStockOperationCompleted;
		private SendOrPostCallback GetPortfolioStatByStockOperationCompleted;
		private SendOrPostCallback SavePushAccountOperationCompleted;
		private SendOrPostCallback SaveStockAlertOperationCompleted;
		private SendOrPostCallback SavePortAlertOperationCompleted;
		private SendOrPostCallback SendStopOrderOperationCompleted;
		private SendOrPostCallback ViewStopOrderOperationCompleted;
		private SendOrPostCallback ViewStopOrder_FirstLSOperationCompleted;
		private SendOrPostCallback SendCancelStopOrderOperationCompleted;
		private SendOrPostCallback StopOrderRegisterOperationCompleted;
		private SendOrPostCallback StopOrderCheckDisclaimerOperationCompleted;
		private SendOrPostCallback SaveUserAlertOperationCompleted;
		private SendOrPostCallback SaveUserAlert2OperationCompleted;
		private SendOrPostCallback GetAlertLogOperationCompleted;
		private SendOrPostCallback GetUserAlertOperationCompleted;
		private SendOrPostCallback GetStockAlertItemsOperationCompleted;
		private SendOrPostCallback GetPortAlertItemsOperationCompleted;
		private SendOrPostCallback NAVChartOperationCompleted;
		private SendOrPostCallback GetDataForSmartClickOperationCompleted;
		private SendOrPostCallback GetOrderFor1ClickOperationCompleted;
		private SendOrPostCallback SaveSummaryMarketDataOperationCompleted;
		private SendOrPostCallback GetStockSMAOperationCompleted;
		private SendOrPostCallback SaveUserConfigOperationCompleted;
		private SendOrPostCallback SaveUserConfigAllOperationCompleted;
		private SendOrPostCallback SendHeartBeatOperationCompleted;
		private SendOrPostCallback SendHeartBeat2OperationCompleted;
		private SendOrPostCallback SaveUserEfinForwardOperationCompleted;
		private SendOrPostCallback LoadAllInformationOperationCompleted;
		private SendOrPostCallback LoadAllInformationIsoddOperationCompleted;
		private SendOrPostCallback LoadStockInformationOperationCompleted;
		private SendOrPostCallback LoadMarketInformationOperationCompleted;
		private SendOrPostCallback LoadOddLotInformationOperationCompleted;
		private SendOrPostCallback BoardcastMessageOperationCompleted;
		private SendOrPostCallback SendPushMessageOperationCompleted;
		private SendOrPostCallback BestBidOfferOperationCompleted;
		private SendOrPostCallback BestOpenPriceOperationCompleted;
		private SendOrPostCallback BestProjectedOperationCompleted;
		private SendOrPostCallback IndustryStatOperationCompleted;
		private SendOrPostCallback TopActiveOperationCompleted;
		private SendOrPostCallback TopActiveBBOOperationCompleted;
		private SendOrPostCallback TopActiveBBO_BenefitOperationCompleted;
		private SendOrPostCallback TopActiveBBO_WarrantOperationCompleted;
		private SendOrPostCallback TopActiveBBO_DWOperationCompleted;
		private SendOrPostCallback TopActiveBBO_NewsOperationCompleted;
		private SendOrPostCallback TopActiveBBO_TurnOverOperationCompleted;
		private SendOrPostCallback TopActiveBBO_SectorOperationCompleted;
		private SendOrPostCallback InvestorTypeOperationCompleted;
		private SendOrPostCallback MarketIndicatorOperationCompleted;
		private SendOrPostCallback SaleByPriceOperationCompleted;
		private SendOrPostCallback SectorStatOperationCompleted;
		private SendOrPostCallback SectorStatForDumpOperationCompleted;
		private SendOrPostCallback StockStatForDumpOperationCompleted;
		private SendOrPostCallback GetUserConfigForDumpOperationCompleted;
		private SendOrPostCallback SaveUserConfigForDumpOperationCompleted;
		private SendOrPostCallback SaleByTime2OperationCompleted;
		private SendOrPostCallback StockInPlayOperationCompleted;
		private SendOrPostCallback StockByPricePageOperationCompleted;
		private SendOrPostCallback MarketStatusOperationCompleted;
		private SendOrPostCallback TopBBOOperationCompleted;
		private SendOrPostCallback TopBBOadOperationCompleted;
		private SendOrPostCallback ViewOddlotOperationCompleted;
		private SendOrPostCallback Get5BidOfferOperationCompleted;
		private SendOrPostCallback ViewOrderTransactionOperationCompleted;
		private SendOrPostCallback ViewOrderHistoryOperationCompleted;
		private SendOrPostCallback ViewOrdersForDumpOperationCompleted;
		private SendOrPostCallback ViewOrderByOrderNoOperationCompleted;
		private SendOrPostCallback ViewNewsHeaderOperationCompleted;
		private SendOrPostCallback ViewNewsStoryOperationCompleted;
		private SendOrPostCallback ViewCustomersInfoOperationCompleted;
		private SendOrPostCallback ViewCustomerCreditOnSendBox_FreewillOperationCompleted;
		private SendOrPostCallback ViewCustomerCreditOnSendBoxOperationCompleted;
		private SendOrPostCallback GetSwitchAccountInfoOperationCompleted;
		private SendOrPostCallback GetSwitchAccountInfoEserviceOperationCompleted;
		private SendOrPostCallback ViewCustomer_MobileReportAllOperationCompleted;
		private SendOrPostCallback ViewCustomer_OrdersConfirmsOperationCompleted;
		private SendOrPostCallback ViewCustomer_CreditPositionOperationCompleted;
		private SendOrPostCallback ViewCustomer_ProjectedProfitLossOperationCompleted;
		private SendOrPostCallback ViewCustomer_RealizeProfitLossOperationCompleted;
		private SendOrPostCallback ViewCustomer_SummaryOperationCompleted;
		private SendOrPostCallback ViewCustomer_ConfirmSummaryOperationCompleted;
		private SendOrPostCallback ViewCustomer_ConfirmOperationCompleted;
		private SendOrPostCallback ViewCustomer_ConfirmByDealIDOperationCompleted;
		private SendOrPostCallback ViewCustomer_ConfirmByStockOperationCompleted;
		private SendOrPostCallback ViewOrderInfo_AfterCloseFwOperationCompleted;
		private SendOrPostCallback ViewOrderDealDataOperationCompleted;
		private SendOrPostCallback ViewOrderDealDataHistoryOperationCompleted;
		private SendOrPostCallback GetCometInfoOperationCompleted;
		private SendOrPostCallback GetTunnelConfigOperationCompleted;
		private SendOrPostCallback GetTunnelOperationCompleted;
		private SendOrPostCallback VerifyOrderOperationCompleted;
		private SendOrPostCallback VerifyOrderFwOperationCompleted;
		private SendOrPostCallback VerifyOrderMktOperationCompleted;
		private SendOrPostCallback GetMainInfoOperationCompleted;
		private SendOrPostCallback UserAuthenOperationCompleted;
		private SendOrPostCallback ClearEfinSessionOperationCompleted;
		private SendOrPostCallback GetUrlClientOperationCompleted;
		private SendOrPostCallback LogoutOperationCompleted;
		private SendOrPostCallback LogoutADOperationCompleted;
		private SendOrPostCallback ChangeCustomerPasswordOperationCompleted;
		private SendOrPostCallback ChangeTraderPasswordOperationCompleted;
		private SendOrPostCallback StockThresholdInformationOperationCompleted;
		private SendOrPostCallback SaveStockThresholdOperationCompleted;
		private SendOrPostCallback SendNewOrderOperationCompleted;
		private SendOrPostCallback SendNewOrderParamsOperationCompleted;
		private SendOrPostCallback SendCancelOrder_AfterCloseFwOperationCompleted;
		private SendOrPostCallback SendCancelOrderOperationCompleted;
		private SendOrPostCallback SendEditOrderOperationCompleted;
		private bool useDefaultCredentialsSetExplicitly;
        public CountOrderCancelForDumpCompletedEventHandler _CountOrderCancelForDumpCompleted;
		public event CountOrderCancelForDumpCompletedEventHandler CountOrderCancelForDumpCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._CountOrderCancelForDumpCompleted +=  value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._CountOrderCancelForDumpCompleted -=  value;
			}
		}
        public ViewCustomerByStockForDumpCompletedEventHandler _ViewCustomerByStockForDumpCompleted;
		public event ViewCustomerByStockForDumpCompletedEventHandler ViewCustomerByStockForDumpCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._ViewCustomerByStockForDumpCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._ViewCustomerByStockForDumpCompleted -= value;
			}
		}
        public GetBrokerMarginVolumeCompletedEventHandler _GetBrokerMarginVolumeCompleted;
		public event GetBrokerMarginVolumeCompletedEventHandler GetBrokerMarginVolumeCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._GetBrokerMarginVolumeCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._GetBrokerMarginVolumeCompleted -=value;
			}
		}
        public VerifyPincodeCompletedEventHandler _VerifyPincodeCompleted;
		public event VerifyPincodeCompletedEventHandler VerifyPincodeCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._VerifyPincodeCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._VerifyPincodeCompleted -= value;
			}
		}
        public VerifyPincode2CompletedEventHandler _VerifyPincode2Completed;
		public event VerifyPincode2CompletedEventHandler VerifyPincode2Completed
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._VerifyPincode2Completed += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._VerifyPincode2Completed -= value;
			}
		}
        public SendBAMessageCompletedEventHandler _SendBAMessageCompleted;
		public event SendBAMessageCompletedEventHandler SendBAMessageCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._SendBAMessageCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._SendBAMessageCompleted -= value;
			}
		}
        public StockHistoryCompletedEventHandler _StockHistoryCompleted;
		public event StockHistoryCompletedEventHandler StockHistoryCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._StockHistoryCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._StockHistoryCompleted -= value;
			}
		}
        public StockChartCompletedEventHandler _StockChartCompleted;
		public event StockChartCompletedEventHandler StockChartCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._StockChartCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._StockChartCompleted -= value;
			}
		}
		public StockHistDataCompletedEventHandler _StockHistDataCompleted;
		public event StockHistDataCompletedEventHandler StockHistDataCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._StockHistDataCompleted +=  value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._StockHistDataCompleted -= value;
			}
		}
		public LoadStockNicknameCompletedEventHandler _LoadStockNicknameCompleted;
		public event LoadStockNicknameCompletedEventHandler LoadStockNicknameCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._LoadStockNicknameCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._LoadStockNicknameCompleted -= value;
			}
		}
		public SaveStockNicknameCompletedEventHandler _SaveStockNicknameCompleted;
		public event SaveStockNicknameCompletedEventHandler SaveStockNicknameCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._SaveStockNicknameCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._SaveStockNicknameCompleted -= value;
			}
		}
		public DeleteStockNicknameCompletedEventHandler _DeleteStockNicknameCompleted;
		public event DeleteStockNicknameCompletedEventHandler DeleteStockNicknameCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._DeleteStockNicknameCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._DeleteStockNicknameCompleted -= value;
			}
		}
		public  LoadStockNicknameExtraCompletedEventHandler _LoadStockNicknameExtraCompleted;
		public event LoadStockNicknameExtraCompletedEventHandler LoadStockNicknameExtraCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._LoadStockNicknameExtraCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._LoadStockNicknameExtraCompleted -= value;
			}
		}
		public SaveStockNicknameExtraCompletedEventHandler _SaveStockNicknameExtraCompleted;
		public event SaveStockNicknameExtraCompletedEventHandler SaveStockNicknameExtraCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._SaveStockNicknameExtraCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._SaveStockNicknameExtraCompleted -= value;
			}
		}
		public DeleteStockNicknameExtraCompletedEventHandler _DeleteStockNicknameExtraCompleted;
		public event DeleteStockNicknameExtraCompletedEventHandler DeleteStockNicknameExtraCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._DeleteStockNicknameExtraCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._DeleteStockNicknameExtraCompleted -= value;
			}
		}
		public IntradayChartCompletedEventHandler _IntradayChartCompleted;
		public event IntradayChartCompletedEventHandler IntradayChartCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._IntradayChartCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._IntradayChartCompleted -= value;
			}
		}
		public IntradayIndexChartCompletedEventHandler _IntradayIndexChartCompleted;
		public event IntradayIndexChartCompletedEventHandler IntradayIndexChartCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._IntradayIndexChartCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._IntradayIndexChartCompleted -=  value;
			}
		}
		public GetChartImageCompletedEventHandler _GetChartImageCompleted;
		public event GetChartImageCompletedEventHandler GetChartImageCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._GetChartImageCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._GetChartImageCompleted -= value;
			}
		}
		public GetSetIndexChartImageCompletedEventHandler _GetSetIndexChartImageCompleted;
		public event GetSetIndexChartImageCompletedEventHandler GetSetIndexChartImageCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._GetSetIndexChartImageCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._GetSetIndexChartImageCompleted -=  value;
			}
		}
		public GetPortfolioStatAllStockCompletedEventHandler _GetPortfolioStatAllStockCompleted;
		public event GetPortfolioStatAllStockCompletedEventHandler GetPortfolioStatAllStockCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._GetPortfolioStatAllStockCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._GetPortfolioStatAllStockCompleted -= value;
			}
		}
		public GetPortfolioStatByStockCompletedEventHandler _GetPortfolioStatByStockCompleted;
		public event GetPortfolioStatByStockCompletedEventHandler GetPortfolioStatByStockCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._GetPortfolioStatByStockCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._GetPortfolioStatByStockCompleted -= value;
			}
		}
		public SavePushAccountCompletedEventHandler _SavePushAccountCompleted;
		public event SavePushAccountCompletedEventHandler SavePushAccountCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._SavePushAccountCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._SavePushAccountCompleted -= value;
			}
		}
		public SaveStockAlertCompletedEventHandler _SaveStockAlertCompleted;
		public event SaveStockAlertCompletedEventHandler SaveStockAlertCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._SaveStockAlertCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._SaveStockAlertCompleted -= value;
			}
		}
		public  SavePortAlertCompletedEventHandler _SavePortAlertCompleted;
		public event SavePortAlertCompletedEventHandler SavePortAlertCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._SavePortAlertCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._SavePortAlertCompleted -= value;
			}
		}
		public SendStopOrderCompletedEventHandler _SendStopOrderCompleted;
		public event SendStopOrderCompletedEventHandler SendStopOrderCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._SendStopOrderCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._SendStopOrderCompleted -= value;
			}
		}
		public ViewStopOrderCompletedEventHandler _ViewStopOrderCompleted;
		public event ViewStopOrderCompletedEventHandler ViewStopOrderCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._ViewStopOrderCompleted +=  value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._ViewStopOrderCompleted -= value;
			}
		}
		public ViewStopOrder_FirstLSCompletedEventHandler _ViewStopOrder_FirstLSCompleted;
		public event ViewStopOrder_FirstLSCompletedEventHandler ViewStopOrder_FirstLSCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._ViewStopOrder_FirstLSCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._ViewStopOrder_FirstLSCompleted -= value;
			}
		}
		public SendCancelStopOrderCompletedEventHandler _SendCancelStopOrderCompleted;
		public event SendCancelStopOrderCompletedEventHandler SendCancelStopOrderCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._SendCancelStopOrderCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._SendCancelStopOrderCompleted -= value;
			}
		}
		public StopOrderRegisterCompletedEventHandler _StopOrderRegisterCompleted;
		public event StopOrderRegisterCompletedEventHandler StopOrderRegisterCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._StopOrderRegisterCompleted +=  value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._StopOrderRegisterCompleted -=  value;
			}
		}
		public StopOrderCheckDisclaimerCompletedEventHandler _StopOrderCheckDisclaimerCompleted;
		public event StopOrderCheckDisclaimerCompletedEventHandler StopOrderCheckDisclaimerCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._StopOrderCheckDisclaimerCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._StopOrderCheckDisclaimerCompleted -= value;
			}
		}
		public SaveUserAlertCompletedEventHandler _SaveUserAlertCompleted;
		public event SaveUserAlertCompletedEventHandler SaveUserAlertCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._SaveUserAlertCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._SaveUserAlertCompleted -= value;
			}
		}
		public  SaveUserAlert2CompletedEventHandler _SaveUserAlert2Completed;
		public event SaveUserAlert2CompletedEventHandler SaveUserAlert2Completed
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._SaveUserAlert2Completed += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._SaveUserAlert2Completed -= value;
			}
		}
		public GetAlertLogCompletedEventHandler _GetAlertLogCompleted;
		public event GetAlertLogCompletedEventHandler GetAlertLogCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._GetAlertLogCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._GetAlertLogCompleted -= value;
			}
		}
		public GetUserAlertCompletedEventHandler _GetUserAlertCompleted;
		public event GetUserAlertCompletedEventHandler GetUserAlertCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._GetUserAlertCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._GetUserAlertCompleted -= value;
			}
		}
		public GetStockAlertItemsCompletedEventHandler _GetStockAlertItemsCompleted;
		public event GetStockAlertItemsCompletedEventHandler GetStockAlertItemsCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._GetStockAlertItemsCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._GetStockAlertItemsCompleted -= value;
			}
		}
        public GetPortAlertItemsCompletedEventHandler _GetPortAlertItemsCompleted;
		public event GetPortAlertItemsCompletedEventHandler GetPortAlertItemsCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._GetPortAlertItemsCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._GetPortAlertItemsCompleted -= value;
			}
		}
        public NAVChartCompletedEventHandler _NAVChartCompleted;
		public event NAVChartCompletedEventHandler NAVChartCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._NAVChartCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._NAVChartCompleted -= value;
			}
		}
        public GetDataForSmartClickCompletedEventHandler _GetDataForSmartClickCompleted;
		public event GetDataForSmartClickCompletedEventHandler GetDataForSmartClickCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._GetDataForSmartClickCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._GetDataForSmartClickCompleted -= value;
			}
		}
        public GetOrderFor1ClickCompletedEventHandler _GetOrderFor1ClickCompleted;
		public event GetOrderFor1ClickCompletedEventHandler GetOrderFor1ClickCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._GetOrderFor1ClickCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._GetOrderFor1ClickCompleted -= value;
			}
		}
        public SaveSummaryMarketDataCompletedEventHandler _SaveSummaryMarketDataCompleted;
		public event SaveSummaryMarketDataCompletedEventHandler SaveSummaryMarketDataCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._SaveSummaryMarketDataCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._SaveSummaryMarketDataCompleted -= value;
			}
		}
        public GetStockSMACompletedEventHandler _GetStockSMACompleted;
		public event GetStockSMACompletedEventHandler GetStockSMACompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._GetStockSMACompleted +=  value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._GetStockSMACompleted -= value;
			}
		}
        public SaveUserConfigCompletedEventHandler _SaveUserConfigCompleted;
		public event SaveUserConfigCompletedEventHandler SaveUserConfigCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._SaveUserConfigCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._SaveUserConfigCompleted -= value;
			}
		}
        public SaveUserConfigAllCompletedEventHandler _SaveUserConfigAllCompleted;
		public event SaveUserConfigAllCompletedEventHandler SaveUserConfigAllCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._SaveUserConfigAllCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._SaveUserConfigAllCompleted -= value;
			}
		}
        public SendHeartBeatCompletedEventHandler _SendHeartBeatCompleted;
		public event SendHeartBeatCompletedEventHandler SendHeartBeatCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._SendHeartBeatCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._SendHeartBeatCompleted -=  value;
			}
		}
        public SendHeartBeat2CompletedEventHandler _SendHeartBeat2Completed;
		public event SendHeartBeat2CompletedEventHandler SendHeartBeat2Completed
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._SendHeartBeat2Completed += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._SendHeartBeat2Completed -= value;
			}
		}
        public SaveUserEfinForwardCompletedEventHandler _SaveUserEfinForwardCompleted;
		public event SaveUserEfinForwardCompletedEventHandler SaveUserEfinForwardCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._SaveUserEfinForwardCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._SaveUserEfinForwardCompleted -= value;
			}
		}
        public LoadAllInformationCompletedEventHandler _LoadAllInformationCompleted;
		public event LoadAllInformationCompletedEventHandler LoadAllInformationCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._LoadAllInformationCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._LoadAllInformationCompleted -=  value;
			}
		}
        public LoadAllInformationIsoddCompletedEventHandler _LoadAllInformationIsoddCompleted;
		public event LoadAllInformationIsoddCompletedEventHandler LoadAllInformationIsoddCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._LoadAllInformationIsoddCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._LoadAllInformationIsoddCompleted -= value;
			}
		}
        public LoadStockInformationCompletedEventHandler _LoadStockInformationCompleted;
		public event LoadStockInformationCompletedEventHandler LoadStockInformationCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._LoadStockInformationCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._LoadStockInformationCompleted -= value;
			}
		}
        public LoadMarketInformationCompletedEventHandler _LoadMarketInformationCompleted;
		public event LoadMarketInformationCompletedEventHandler LoadMarketInformationCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._LoadMarketInformationCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._LoadMarketInformationCompleted -= value;
			}
		}
        public LoadOddLotInformationCompletedEventHandler _LoadOddLotInformationCompleted;
		public event LoadOddLotInformationCompletedEventHandler LoadOddLotInformationCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._LoadOddLotInformationCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._LoadOddLotInformationCompleted -= value;
			}
		}
        public BoardcastMessageCompletedEventHandler _BoardcastMessageCompleted;
		public event BoardcastMessageCompletedEventHandler BoardcastMessageCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._BoardcastMessageCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._BoardcastMessageCompleted -= value;
			}
		}
        public SendPushMessageCompletedEventHandler _SendPushMessageCompleted;
		public event SendPushMessageCompletedEventHandler SendPushMessageCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._SendPushMessageCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._SendPushMessageCompleted -= value;
			}
		}
        public BestBidOfferCompletedEventHandler _BestBidOfferCompleted;
		public event BestBidOfferCompletedEventHandler BestBidOfferCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._BestBidOfferCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._BestBidOfferCompleted -= value;
			}
		}
        public BestOpenPriceCompletedEventHandler _BestOpenPriceCompleted;
		public event BestOpenPriceCompletedEventHandler BestOpenPriceCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._BestOpenPriceCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._BestOpenPriceCompleted -= value;
			}
		}
        public BestProjectedCompletedEventHandler _BestProjectedCompleted;
		public event BestProjectedCompletedEventHandler BestProjectedCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._BestProjectedCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._BestProjectedCompleted -= value;
			}
		}
        public IndustryStatCompletedEventHandler _IndustryStatCompleted;
		public event IndustryStatCompletedEventHandler IndustryStatCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._IndustryStatCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._IndustryStatCompleted -= value;
			}
		}
        public TopActiveCompletedEventHandler _TopActiveCompleted;
		public event TopActiveCompletedEventHandler TopActiveCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._TopActiveCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._TopActiveCompleted -= value;
			}
		}
        public TopActiveBBOCompletedEventHandler _TopActiveBBOCompleted;
		public event TopActiveBBOCompletedEventHandler TopActiveBBOCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._TopActiveBBOCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._TopActiveBBOCompleted -= value;
			}
		}
        public TopActiveBBO_BenefitCompletedEventHandler _TopActiveBBO_BenefitCompleted;
		public event TopActiveBBO_BenefitCompletedEventHandler TopActiveBBO_BenefitCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._TopActiveBBO_BenefitCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._TopActiveBBO_BenefitCompleted -= value;
			}
		}
        public TopActiveBBO_WarrantCompletedEventHandler _TopActiveBBO_WarrantCompleted;
		public event TopActiveBBO_WarrantCompletedEventHandler TopActiveBBO_WarrantCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._TopActiveBBO_WarrantCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._TopActiveBBO_WarrantCompleted -= value;
			}
		}
        public TopActiveBBO_DWCompletedEventHandler _TopActiveBBO_DWCompleted;
		public event TopActiveBBO_DWCompletedEventHandler TopActiveBBO_DWCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._TopActiveBBO_DWCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._TopActiveBBO_DWCompleted -= value;
			}
		}
        public TopActiveBBO_NewsCompletedEventHandler _TopActiveBBO_NewsCompleted;
		public event TopActiveBBO_NewsCompletedEventHandler TopActiveBBO_NewsCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._TopActiveBBO_NewsCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._TopActiveBBO_NewsCompleted -= value;
			}
		}
        public TopActiveBBO_TurnOverCompletedEventHandler _TopActiveBBO_TurnOverCompleted;
		public event TopActiveBBO_TurnOverCompletedEventHandler TopActiveBBO_TurnOverCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._TopActiveBBO_TurnOverCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._TopActiveBBO_TurnOverCompleted -= value;
			}
		}
		public TopActiveBBO_SectorCompletedEventHandler _TopActiveBBO_SectorCompleted;
		public event TopActiveBBO_SectorCompletedEventHandler TopActiveBBO_SectorCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._TopActiveBBO_SectorCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._TopActiveBBO_SectorCompleted -= value;
			}
		}
		public InvestorTypeCompletedEventHandler _InvestorTypeCompleted;
		public event InvestorTypeCompletedEventHandler InvestorTypeCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._InvestorTypeCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._InvestorTypeCompleted -= value;
			}
		}
		public MarketIndicatorCompletedEventHandler _MarketIndicatorCompleted;
		public event MarketIndicatorCompletedEventHandler MarketIndicatorCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._MarketIndicatorCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._MarketIndicatorCompleted -= value;
			}
		}
		public SaleByPriceCompletedEventHandler _SaleByPriceCompleted;
		public event SaleByPriceCompletedEventHandler SaleByPriceCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._SaleByPriceCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._SaleByPriceCompleted -= value;
			}
		}
		public SectorStatCompletedEventHandler _SectorStatCompleted;
		public event SectorStatCompletedEventHandler SectorStatCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._SectorStatCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._SectorStatCompleted -= value;
			}
		}
		public SectorStatForDumpCompletedEventHandler _SectorStatForDumpCompleted;
		public event SectorStatForDumpCompletedEventHandler SectorStatForDumpCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._SectorStatForDumpCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._SectorStatForDumpCompleted -= value;
			}
		}
		public StockStatForDumpCompletedEventHandler _StockStatForDumpCompleted;
		public event StockStatForDumpCompletedEventHandler StockStatForDumpCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._StockStatForDumpCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._StockStatForDumpCompleted -= value;
			}
		}
		public GetUserConfigForDumpCompletedEventHandler _GetUserConfigForDumpCompleted;
		public event GetUserConfigForDumpCompletedEventHandler GetUserConfigForDumpCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._GetUserConfigForDumpCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._GetUserConfigForDumpCompleted -= value;
			}
		}
		public SaveUserConfigForDumpCompletedEventHandler _SaveUserConfigForDumpCompleted;
		public event SaveUserConfigForDumpCompletedEventHandler SaveUserConfigForDumpCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._SaveUserConfigForDumpCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._SaveUserConfigForDumpCompleted -= value;
			}
		}
		public SaleByTime2CompletedEventHandler _SaleByTime2Completed;
		public event SaleByTime2CompletedEventHandler SaleByTime2Completed
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._SaleByTime2Completed += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._SaleByTime2Completed -= value;
			}
		}
		public StockInPlayCompletedEventHandler _StockInPlayCompleted;
		public event StockInPlayCompletedEventHandler StockInPlayCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._StockInPlayCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._StockInPlayCompleted -= value;
			}
		}
		public StockByPricePageCompletedEventHandler _StockByPricePageCompleted;
		public event StockByPricePageCompletedEventHandler StockByPricePageCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._StockByPricePageCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._StockByPricePageCompleted -= value;
			}
		}
		public MarketStatusCompletedEventHandler _MarketStatusCompleted;
		public event MarketStatusCompletedEventHandler MarketStatusCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._MarketStatusCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._MarketStatusCompleted -= value;
			}
		}
		public TopBBOCompletedEventHandler _TopBBOCompleted;
		public event TopBBOCompletedEventHandler TopBBOCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._TopBBOCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._TopBBOCompleted -= value;
			}
		}
		public TopBBOadCompletedEventHandler _TopBBOadCompleted;
		public event TopBBOadCompletedEventHandler TopBBOadCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._TopBBOadCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._TopBBOadCompleted -= value;
			}
		}
		public ViewOddlotCompletedEventHandler _ViewOddlotCompleted;
		public event ViewOddlotCompletedEventHandler ViewOddlotCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._ViewOddlotCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._ViewOddlotCompleted -= value;
			}
		}
		public Get5BidOfferCompletedEventHandler _Get5BidOfferCompleted;
		public event Get5BidOfferCompletedEventHandler Get5BidOfferCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._Get5BidOfferCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._Get5BidOfferCompleted -= value;
			}
		}
		public ViewOrderTransactionCompletedEventHandler _ViewOrderTransactionCompleted;
		public event ViewOrderTransactionCompletedEventHandler ViewOrderTransactionCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._ViewOrderTransactionCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._ViewOrderTransactionCompleted -= value;
			}
		}
		public ViewOrderHistoryCompletedEventHandler _ViewOrderHistoryCompleted;
		public event ViewOrderHistoryCompletedEventHandler ViewOrderHistoryCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._ViewOrderHistoryCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._ViewOrderHistoryCompleted -= value;
			}
		}
		public ViewOrdersForDumpCompletedEventHandler _ViewOrdersForDumpCompleted;
		public event ViewOrdersForDumpCompletedEventHandler ViewOrdersForDumpCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._ViewOrdersForDumpCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._ViewOrdersForDumpCompleted -= value;
			}
		}
		public ViewOrderByOrderNoCompletedEventHandler _ViewOrderByOrderNoCompleted;
		public event ViewOrderByOrderNoCompletedEventHandler ViewOrderByOrderNoCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._ViewOrderByOrderNoCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._ViewOrderByOrderNoCompleted -= value;
			}
		}
		public ViewNewsHeaderCompletedEventHandler _ViewNewsHeaderCompleted;
		public event ViewNewsHeaderCompletedEventHandler ViewNewsHeaderCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._ViewNewsHeaderCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._ViewNewsHeaderCompleted -= value;
			}
		}
		public  ViewNewsStoryCompletedEventHandler _ViewNewsStoryCompleted;
		public event ViewNewsStoryCompletedEventHandler ViewNewsStoryCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._ViewNewsStoryCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._ViewNewsStoryCompleted -= value;
			}
		}
		public  ViewCustomersInfoCompletedEventHandler _ViewCustomersInfoCompleted;
		public event ViewCustomersInfoCompletedEventHandler ViewCustomersInfoCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._ViewCustomersInfoCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._ViewCustomersInfoCompleted -= value;
			}
		}
		public  ViewCustomerCreditOnSendBox_FreewillCompletedEventHandler _ViewCustomerCreditOnSendBox_FreewillCompleted;
		public event ViewCustomerCreditOnSendBox_FreewillCompletedEventHandler ViewCustomerCreditOnSendBox_FreewillCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._ViewCustomerCreditOnSendBox_FreewillCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._ViewCustomerCreditOnSendBox_FreewillCompleted -= value;
			}
		}
		public  ViewCustomerCreditOnSendBoxCompletedEventHandler _ViewCustomerCreditOnSendBoxCompleted;
		public event ViewCustomerCreditOnSendBoxCompletedEventHandler ViewCustomerCreditOnSendBoxCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._ViewCustomerCreditOnSendBoxCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._ViewCustomerCreditOnSendBoxCompleted -= value;
			}
		}
		public  GetSwitchAccountInfoCompletedEventHandler _GetSwitchAccountInfoCompleted;
		public event GetSwitchAccountInfoCompletedEventHandler GetSwitchAccountInfoCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._GetSwitchAccountInfoCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._GetSwitchAccountInfoCompleted -= value;
			}
		}
		public  GetSwitchAccountInfoEserviceCompletedEventHandler _GetSwitchAccountInfoEserviceCompleted;
		public event GetSwitchAccountInfoEserviceCompletedEventHandler GetSwitchAccountInfoEserviceCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._GetSwitchAccountInfoEserviceCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._GetSwitchAccountInfoEserviceCompleted -= value;
			}
		}
		public  ViewCustomer_MobileReportAllCompletedEventHandler _ViewCustomer_MobileReportAllCompleted;
		public event ViewCustomer_MobileReportAllCompletedEventHandler ViewCustomer_MobileReportAllCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._ViewCustomer_MobileReportAllCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._ViewCustomer_MobileReportAllCompleted -= value;
			}
		}
		public  ViewCustomer_OrdersConfirmsCompletedEventHandler _ViewCustomer_OrdersConfirmsCompleted;
		public event ViewCustomer_OrdersConfirmsCompletedEventHandler ViewCustomer_OrdersConfirmsCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._ViewCustomer_OrdersConfirmsCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._ViewCustomer_OrdersConfirmsCompleted -= value;
			}
		}
		public  ViewCustomer_CreditPositionCompletedEventHandler _ViewCustomer_CreditPositionCompleted;
		public event ViewCustomer_CreditPositionCompletedEventHandler ViewCustomer_CreditPositionCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._ViewCustomer_CreditPositionCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._ViewCustomer_CreditPositionCompleted -= value;
			}
		}
		public  ViewCustomer_ProjectedProfitLossCompletedEventHandler _ViewCustomer_ProjectedProfitLossCompleted;
		public event ViewCustomer_ProjectedProfitLossCompletedEventHandler ViewCustomer_ProjectedProfitLossCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._ViewCustomer_ProjectedProfitLossCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._ViewCustomer_ProjectedProfitLossCompleted -= value;
			}
		}
		public  ViewCustomer_RealizeProfitLossCompletedEventHandler _ViewCustomer_RealizeProfitLossCompleted;
		public event ViewCustomer_RealizeProfitLossCompletedEventHandler ViewCustomer_RealizeProfitLossCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._ViewCustomer_RealizeProfitLossCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._ViewCustomer_RealizeProfitLossCompleted -= value;
			}
		}
		public  ViewCustomer_SummaryCompletedEventHandler _ViewCustomer_SummaryCompleted;
		public event ViewCustomer_SummaryCompletedEventHandler ViewCustomer_SummaryCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._ViewCustomer_SummaryCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._ViewCustomer_SummaryCompleted -= value;
			}
		}
		public  ViewCustomer_ConfirmSummaryCompletedEventHandler _ViewCustomer_ConfirmSummaryCompleted;
		public event ViewCustomer_ConfirmSummaryCompletedEventHandler ViewCustomer_ConfirmSummaryCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._ViewCustomer_ConfirmSummaryCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._ViewCustomer_ConfirmSummaryCompleted -= value;
			}
		}
		public  ViewCustomer_ConfirmCompletedEventHandler _ViewCustomer_ConfirmCompleted;
		public event ViewCustomer_ConfirmCompletedEventHandler ViewCustomer_ConfirmCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._ViewCustomer_ConfirmCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._ViewCustomer_ConfirmCompleted -= value;
			}
		}
		public  ViewCustomer_ConfirmByDealIDCompletedEventHandler _ViewCustomer_ConfirmByDealIDCompleted;
		public event ViewCustomer_ConfirmByDealIDCompletedEventHandler ViewCustomer_ConfirmByDealIDCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._ViewCustomer_ConfirmByDealIDCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._ViewCustomer_ConfirmByDealIDCompleted -= value;
			}
		}
		public  ViewCustomer_ConfirmByStockCompletedEventHandler _ViewCustomer_ConfirmByStockCompleted;
		public event ViewCustomer_ConfirmByStockCompletedEventHandler ViewCustomer_ConfirmByStockCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._ViewCustomer_ConfirmByStockCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._ViewCustomer_ConfirmByStockCompleted -= value;
			}
		}
		public  ViewOrderInfo_AfterCloseFwCompletedEventHandler _ViewOrderInfo_AfterCloseFwCompleted;
		public event ViewOrderInfo_AfterCloseFwCompletedEventHandler ViewOrderInfo_AfterCloseFwCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._ViewOrderInfo_AfterCloseFwCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._ViewOrderInfo_AfterCloseFwCompleted -= value;
			}
		}
		public  ViewOrderDealDataCompletedEventHandler _ViewOrderDealDataCompleted;
		public event ViewOrderDealDataCompletedEventHandler ViewOrderDealDataCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._ViewOrderDealDataCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._ViewOrderDealDataCompleted -= value;
			}
		}
		public  ViewOrderDealDataHistoryCompletedEventHandler _ViewOrderDealDataHistoryCompleted;
		public event ViewOrderDealDataHistoryCompletedEventHandler ViewOrderDealDataHistoryCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._ViewOrderDealDataHistoryCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._ViewOrderDealDataHistoryCompleted -= value;
			}
		}
		public  GetCometInfoCompletedEventHandler _GetCometInfoCompleted;
		public event GetCometInfoCompletedEventHandler GetCometInfoCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._GetCometInfoCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._GetCometInfoCompleted -= value;
			}
		}
		public  GetTunnelConfigCompletedEventHandler _GetTunnelConfigCompleted;
		public event GetTunnelConfigCompletedEventHandler GetTunnelConfigCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._GetTunnelConfigCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._GetTunnelConfigCompleted -= value;
			}
		}
		public  GetTunnelCompletedEventHandler _GetTunnelCompleted;
		public event GetTunnelCompletedEventHandler GetTunnelCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._GetTunnelCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._GetTunnelCompleted -= value;
			}
		}
		public  VerifyOrderCompletedEventHandler _VerifyOrderCompleted;
		public event VerifyOrderCompletedEventHandler VerifyOrderCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._VerifyOrderCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._VerifyOrderCompleted -= value;
			}
		}
		public  VerifyOrderFwCompletedEventHandler _VerifyOrderFwCompleted;
		public event VerifyOrderFwCompletedEventHandler VerifyOrderFwCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._VerifyOrderFwCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._VerifyOrderFwCompleted -= value;
			}
		}
		public  VerifyOrderMktCompletedEventHandler _VerifyOrderMktCompleted;
		public event VerifyOrderMktCompletedEventHandler VerifyOrderMktCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._VerifyOrderMktCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._VerifyOrderMktCompleted -= value;
			}
		}
		public  GetMainInfoCompletedEventHandler _GetMainInfoCompleted;
		public event GetMainInfoCompletedEventHandler GetMainInfoCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._GetMainInfoCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._GetMainInfoCompleted -= value;
			}
		}
		public  UserAuthenCompletedEventHandler _UserAuthenCompleted;
		public event UserAuthenCompletedEventHandler UserAuthenCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._UserAuthenCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._UserAuthenCompleted -= value;
			}
		}
		public  ClearEfinSessionCompletedEventHandler _ClearEfinSessionCompleted;
		public event ClearEfinSessionCompletedEventHandler ClearEfinSessionCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._ClearEfinSessionCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._ClearEfinSessionCompleted -= value;
			}
		}
		public  GetUrlClientCompletedEventHandler _GetUrlClientCompleted;
		public event GetUrlClientCompletedEventHandler GetUrlClientCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._GetUrlClientCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._GetUrlClientCompleted -= value;
			}
		}
		public  LogoutCompletedEventHandler _LogoutCompleted;
		public event LogoutCompletedEventHandler LogoutCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._LogoutCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._LogoutCompleted -= value;
			}
		}
		public  LogoutADCompletedEventHandler _LogoutADCompleted;
		public event LogoutADCompletedEventHandler LogoutADCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._LogoutADCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._LogoutADCompleted -= value;
			}
		}
		public  ChangeCustomerPasswordCompletedEventHandler _ChangeCustomerPasswordCompleted;
		public event ChangeCustomerPasswordCompletedEventHandler ChangeCustomerPasswordCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._ChangeCustomerPasswordCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._ChangeCustomerPasswordCompleted -= value;
			}
		}
		public  ChangeTraderPasswordCompletedEventHandler _ChangeTraderPasswordCompleted;
		public event ChangeTraderPasswordCompletedEventHandler ChangeTraderPasswordCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._ChangeTraderPasswordCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._ChangeTraderPasswordCompleted -= value;
			}
		}
		public  StockThresholdInformationCompletedEventHandler _StockThresholdInformationCompleted;
		public event StockThresholdInformationCompletedEventHandler StockThresholdInformationCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._StockThresholdInformationCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._StockThresholdInformationCompleted -= value;
			}
		}
		public  SaveStockThresholdCompletedEventHandler _SaveStockThresholdCompleted;
		public event SaveStockThresholdCompletedEventHandler SaveStockThresholdCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._SaveStockThresholdCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._SaveStockThresholdCompleted -= value;
			}
		}
		public  SendNewOrderCompletedEventHandler _SendNewOrderCompleted;
		public event SendNewOrderCompletedEventHandler SendNewOrderCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._SendNewOrderCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._SendNewOrderCompleted -= value;
			}
		}
		public  SendNewOrderParamsCompletedEventHandler _SendNewOrderParamsCompleted;
		public event SendNewOrderParamsCompletedEventHandler SendNewOrderParamsCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._SendNewOrderParamsCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._SendNewOrderParamsCompleted -= value;
			}
		}
		public  SendCancelOrder_AfterCloseFwCompletedEventHandler _SendCancelOrder_AfterCloseFwCompleted;
		public event SendCancelOrder_AfterCloseFwCompletedEventHandler SendCancelOrder_AfterCloseFwCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._SendCancelOrder_AfterCloseFwCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._SendCancelOrder_AfterCloseFwCompleted -= value;
			}
		}
		public SendCancelOrderCompletedEventHandler _SendCancelOrderCompleted;
		public event SendCancelOrderCompletedEventHandler SendCancelOrderCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._SendCancelOrderCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._SendCancelOrderCompleted -= value;
			}
		}
		public SendEditOrderCompletedEventHandler _SendEditOrderCompleted;
		public event SendEditOrderCompletedEventHandler SendEditOrderCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._SendEditOrderCompleted += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._SendEditOrderCompleted -= value;
			}
		}
		public new string Url
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return base.Url;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				if (this.IsLocalFileSystemWebService(base.Url) && !this.useDefaultCredentialsSetExplicitly && !this.IsLocalFileSystemWebService(value))
				{
					base.UseDefaultCredentials = false;
				}
				base.Url = value;
			}
		}
		public new bool UseDefaultCredentials
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return base.UseDefaultCredentials;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				base.UseDefaultCredentials = value;
				this.useDefaultCredentialsSetExplicitly = true;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Service()
		{
			this.Url = Settings.Default.i2_TradePlus_ITSNetBusinessWS_Service;
			if (this.IsLocalFileSystemWebService(this.Url))
			{
				this.UseDefaultCredentials = true;
				this.useDefaultCredentialsSetExplicitly = false;
			}
			else
			{
				this.useDefaultCredentialsSetExplicitly = true;
			}
		}
		[SoapDocumentMethod("http://tempuri.org/CountOrderCancelForDump", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string CountOrderCancelForDump(string account, string stock, int trusteeId, string side, string price, long startOrderNo, long endOrderNo)
		{
			object[] array = base.Invoke("CountOrderCancelForDump", new object[]
			{
				account,
				stock,
				trusteeId,
				side,
				price,
				startOrderNo,
				endOrderNo
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void CountOrderCancelForDumpAsync(string account, string stock, int trusteeId, string side, string price, long startOrderNo, long endOrderNo)
		{
			this.CountOrderCancelForDumpAsync(account, stock, trusteeId, side, price, startOrderNo, endOrderNo, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void CountOrderCancelForDumpAsync(string account, string stock, int trusteeId, string side, string price, long startOrderNo, long endOrderNo, object userState)
		{
			if (this.CountOrderCancelForDumpOperationCompleted == null)
			{
				this.CountOrderCancelForDumpOperationCompleted = new SendOrPostCallback(this.OnCountOrderCancelForDumpOperationCompleted);
			}
			base.InvokeAsync("CountOrderCancelForDump", new object[]
			{
				account,
				stock,
				trusteeId,
				side,
				price,
				startOrderNo,
				endOrderNo
			}, this.CountOrderCancelForDumpOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnCountOrderCancelForDumpOperationCompleted(object arg)
		{
			if (this._CountOrderCancelForDumpCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._CountOrderCancelForDumpCompleted(this, new CountOrderCancelForDumpCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/ViewCustomerByStockForDump", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string ViewCustomerByStockForDump(string account, string stock, int trusteeId)
		{
			object[] array = base.Invoke("ViewCustomerByStockForDump", new object[]
			{
				account,
				stock,
				trusteeId
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewCustomerByStockForDumpAsync(string account, string stock, int trusteeId)
		{
			this.ViewCustomerByStockForDumpAsync(account, stock, trusteeId, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewCustomerByStockForDumpAsync(string account, string stock, int trusteeId, object userState)
		{
			if (this.ViewCustomerByStockForDumpOperationCompleted == null)
			{
				this.ViewCustomerByStockForDumpOperationCompleted = new SendOrPostCallback(this.OnViewCustomerByStockForDumpOperationCompleted);
			}
			base.InvokeAsync("ViewCustomerByStockForDump", new object[]
			{
				account,
				stock,
				trusteeId
			}, this.ViewCustomerByStockForDumpOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnViewCustomerByStockForDumpOperationCompleted(object arg)
		{
			if (this._ViewCustomerByStockForDumpCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._ViewCustomerByStockForDumpCompleted(this, new ViewCustomerByStockForDumpCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/GetBrokerMarginVolume", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string GetBrokerMarginVolume(string stock)
		{
			object[] array = base.Invoke("GetBrokerMarginVolume", new object[]
			{
				stock
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetBrokerMarginVolumeAsync(string stock)
		{
			this.GetBrokerMarginVolumeAsync(stock, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetBrokerMarginVolumeAsync(string stock, object userState)
		{
			if (this.GetBrokerMarginVolumeOperationCompleted == null)
			{
				this.GetBrokerMarginVolumeOperationCompleted = new SendOrPostCallback(this.OnGetBrokerMarginVolumeOperationCompleted);
			}
			base.InvokeAsync("GetBrokerMarginVolume", new object[]
			{
				stock
			}, this.GetBrokerMarginVolumeOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnGetBrokerMarginVolumeOperationCompleted(object arg)
		{
			if (this._GetBrokerMarginVolumeCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._GetBrokerMarginVolumeCompleted(this, new GetBrokerMarginVolumeCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/VerifyPincode", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool VerifyPincode(string userId, string pincode, string sessionid, string ktzCustMapKey)
		{
			object[] array = base.Invoke("VerifyPincode", new object[]
			{
				userId,
				pincode,
				sessionid,
				ktzCustMapKey
			});
			return (bool)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void VerifyPincodeAsync(string userId, string pincode, string sessionid, string ktzCustMapKey)
		{
			this.VerifyPincodeAsync(userId, pincode, sessionid, ktzCustMapKey, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void VerifyPincodeAsync(string userId, string pincode, string sessionid, string ktzCustMapKey, object userState)
		{
			if (this.VerifyPincodeOperationCompleted == null)
			{
				this.VerifyPincodeOperationCompleted = new SendOrPostCallback(this.OnVerifyPincodeOperationCompleted);
			}
			base.InvokeAsync("VerifyPincode", new object[]
			{
				userId,
				pincode,
				sessionid,
				ktzCustMapKey
			}, this.VerifyPincodeOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnVerifyPincodeOperationCompleted(object arg)
		{
			if (this._VerifyPincodeCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._VerifyPincodeCompleted(this, new VerifyPincodeCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/VerifyPincode2", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string VerifyPincode2(string userId, string pincode, string sessionid, string ktzCustMapKey)
		{
			object[] array = base.Invoke("VerifyPincode2", new object[]
			{
				userId,
				pincode,
				sessionid,
				ktzCustMapKey
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void VerifyPincode2Async(string userId, string pincode, string sessionid, string ktzCustMapKey)
		{
			this.VerifyPincode2Async(userId, pincode, sessionid, ktzCustMapKey, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void VerifyPincode2Async(string userId, string pincode, string sessionid, string ktzCustMapKey, object userState)
		{
			if (this.VerifyPincode2OperationCompleted == null)
			{
				this.VerifyPincode2OperationCompleted = new SendOrPostCallback(this.OnVerifyPincode2OperationCompleted);
			}
			base.InvokeAsync("VerifyPincode2", new object[]
			{
				userId,
				pincode,
				sessionid,
				ktzCustMapKey
			}, this.VerifyPincode2OperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnVerifyPincode2OperationCompleted(object arg)
		{
			if (this._VerifyPincode2Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._VerifyPincode2Completed(this, new VerifyPincode2CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/SendBAMessage", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool SendBAMessage(string message)
		{
			object[] array = base.Invoke("SendBAMessage", new object[]
			{
				message
			});
			return (bool)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SendBAMessageAsync(string message)
		{
			this.SendBAMessageAsync(message, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SendBAMessageAsync(string message, object userState)
		{
			if (this.SendBAMessageOperationCompleted == null)
			{
				this.SendBAMessageOperationCompleted = new SendOrPostCallback(this.OnSendBAMessageOperationCompleted);
			}
			base.InvokeAsync("SendBAMessage", new object[]
			{
				message
			}, this.SendBAMessageOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnSendBAMessageOperationCompleted(object arg)
		{
			if (this._SendBAMessageCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._SendBAMessageCompleted(this, new SendBAMessageCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/StockHistory", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string StockHistory(string stock, string sDate, bool isPageNext, int recordPerPage)
		{
			object[] array = base.Invoke("StockHistory", new object[]
			{
				stock,
				sDate,
				isPageNext,
				recordPerPage
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void StockHistoryAsync(string stock, string sDate, bool isPageNext, int recordPerPage)
		{
			this.StockHistoryAsync(stock, sDate, isPageNext, recordPerPage, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void StockHistoryAsync(string stock, string sDate, bool isPageNext, int recordPerPage, object userState)
		{
			if (this.StockHistoryOperationCompleted == null)
			{
				this.StockHistoryOperationCompleted = new SendOrPostCallback(this.OnStockHistoryOperationCompleted);
			}
			base.InvokeAsync("StockHistory", new object[]
			{
				stock,
				sDate,
				isPageNext,
				recordPerPage
			}, this.StockHistoryOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnStockHistoryOperationCompleted(object arg)
		{
			if (this._StockHistoryCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._StockHistoryCompleted(this, new StockHistoryCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/StockChart", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string StockChart(int stockNo, int recordPerPage)
		{
			object[] array = base.Invoke("StockChart", new object[]
			{
				stockNo,
				recordPerPage
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void StockChartAsync(int stockNo, int recordPerPage)
		{
			this.StockChartAsync(stockNo, recordPerPage, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void StockChartAsync(int stockNo, int recordPerPage, object userState)
		{
			if (this.StockChartOperationCompleted == null)
			{
				this.StockChartOperationCompleted = new SendOrPostCallback(this.OnStockChartOperationCompleted);
			}
			base.InvokeAsync("StockChart", new object[]
			{
				stockNo,
				recordPerPage
			}, this.StockChartOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnStockChartOperationCompleted(object arg)
		{
			if (this._StockChartCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._StockChartCompleted(this, new StockChartCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/StockHistData", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string StockHistData(string symbol, string startDate, string key)
		{
			object[] array = base.Invoke("StockHistData", new object[]
			{
				symbol,
				startDate,
				key
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void StockHistDataAsync(string symbol, string startDate, string key)
		{
			this.StockHistDataAsync(symbol, startDate, key, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void StockHistDataAsync(string symbol, string startDate, string key, object userState)
		{
			if (this.StockHistDataOperationCompleted == null)
			{
				this.StockHistDataOperationCompleted = new SendOrPostCallback(this.OnStockHistDataOperationCompleted);
			}
			base.InvokeAsync("StockHistData", new object[]
			{
				symbol,
				startDate,
				key
			}, this.StockHistDataOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnStockHistDataOperationCompleted(object arg)
		{
			if (this._StockHistDataCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._StockHistDataCompleted(this, new StockHistDataCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/LoadStockNickname", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string LoadStockNickname(string custId)
		{
			object[] array = base.Invoke("LoadStockNickname", new object[]
			{
				custId
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void LoadStockNicknameAsync(string custId)
		{
			this.LoadStockNicknameAsync(custId, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void LoadStockNicknameAsync(string custId, object userState)
		{
			if (this.LoadStockNicknameOperationCompleted == null)
			{
				this.LoadStockNicknameOperationCompleted = new SendOrPostCallback(this.OnLoadStockNicknameOperationCompleted);
			}
			base.InvokeAsync("LoadStockNickname", new object[]
			{
				custId
			}, this.LoadStockNicknameOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnLoadStockNicknameOperationCompleted(object arg)
		{
			if (this._LoadStockNicknameCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._LoadStockNicknameCompleted(this, new LoadStockNicknameCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/SaveStockNickname", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool SaveStockNickname(string custId, string oldStock, string stock, string nickName)
		{
			object[] array = base.Invoke("SaveStockNickname", new object[]
			{
				custId,
				oldStock,
				stock,
				nickName
			});
			return (bool)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SaveStockNicknameAsync(string custId, string oldStock, string stock, string nickName)
		{
			this.SaveStockNicknameAsync(custId, oldStock, stock, nickName, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SaveStockNicknameAsync(string custId, string oldStock, string stock, string nickName, object userState)
		{
			if (this.SaveStockNicknameOperationCompleted == null)
			{
				this.SaveStockNicknameOperationCompleted = new SendOrPostCallback(this.OnSaveStockNicknameOperationCompleted);
			}
			base.InvokeAsync("SaveStockNickname", new object[]
			{
				custId,
				oldStock,
				stock,
				nickName
			}, this.SaveStockNicknameOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnSaveStockNicknameOperationCompleted(object arg)
		{
			if (this._SaveStockNicknameCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._SaveStockNicknameCompleted(this, new SaveStockNicknameCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/DeleteStockNickname", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool DeleteStockNickname(string custId, string stock)
		{
			object[] array = base.Invoke("DeleteStockNickname", new object[]
			{
				custId,
				stock
			});
			return (bool)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void DeleteStockNicknameAsync(string custId, string stock)
		{
			this.DeleteStockNicknameAsync(custId, stock, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void DeleteStockNicknameAsync(string custId, string stock, object userState)
		{
			if (this.DeleteStockNicknameOperationCompleted == null)
			{
				this.DeleteStockNicknameOperationCompleted = new SendOrPostCallback(this.OnDeleteStockNicknameOperationCompleted);
			}
			base.InvokeAsync("DeleteStockNickname", new object[]
			{
				custId,
				stock
			}, this.DeleteStockNicknameOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnDeleteStockNicknameOperationCompleted(object arg)
		{
			if (this._DeleteStockNicknameCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._DeleteStockNicknameCompleted(this, new DeleteStockNicknameCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/LoadStockNicknameExtra", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string LoadStockNicknameExtra(string custId)
		{
			object[] array = base.Invoke("LoadStockNicknameExtra", new object[]
			{
				custId
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void LoadStockNicknameExtraAsync(string custId)
		{
			this.LoadStockNicknameExtraAsync(custId, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void LoadStockNicknameExtraAsync(string custId, object userState)
		{
			if (this.LoadStockNicknameExtraOperationCompleted == null)
			{
				this.LoadStockNicknameExtraOperationCompleted = new SendOrPostCallback(this.OnLoadStockNicknameExtraOperationCompleted);
			}
			base.InvokeAsync("LoadStockNicknameExtra", new object[]
			{
				custId
			}, this.LoadStockNicknameExtraOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnLoadStockNicknameExtraOperationCompleted(object arg)
		{
			if (this._LoadStockNicknameExtraCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._LoadStockNicknameExtraCompleted(this, new LoadStockNicknameExtraCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/SaveStockNicknameExtra", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool SaveStockNicknameExtra(string custId, string oldStock, string stock, string nickName, string type)
		{
			object[] array = base.Invoke("SaveStockNicknameExtra", new object[]
			{
				custId,
				oldStock,
				stock,
				nickName,
				type
			});
			return (bool)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SaveStockNicknameExtraAsync(string custId, string oldStock, string stock, string nickName, string type)
		{
			this.SaveStockNicknameExtraAsync(custId, oldStock, stock, nickName, type, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SaveStockNicknameExtraAsync(string custId, string oldStock, string stock, string nickName, string type, object userState)
		{
			if (this.SaveStockNicknameExtraOperationCompleted == null)
			{
				this.SaveStockNicknameExtraOperationCompleted = new SendOrPostCallback(this.OnSaveStockNicknameExtraOperationCompleted);
			}
			base.InvokeAsync("SaveStockNicknameExtra", new object[]
			{
				custId,
				oldStock,
				stock,
				nickName,
				type
			}, this.SaveStockNicknameExtraOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnSaveStockNicknameExtraOperationCompleted(object arg)
		{
			if (this._SaveStockNicknameExtraCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._SaveStockNicknameExtraCompleted(this, new SaveStockNicknameExtraCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/DeleteStockNicknameExtra", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool DeleteStockNicknameExtra(string custId, string stock)
		{
			object[] array = base.Invoke("DeleteStockNicknameExtra", new object[]
			{
				custId,
				stock
			});
			return (bool)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void DeleteStockNicknameExtraAsync(string custId, string stock)
		{
			this.DeleteStockNicknameExtraAsync(custId, stock, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void DeleteStockNicknameExtraAsync(string custId, string stock, object userState)
		{
			if (this.DeleteStockNicknameExtraOperationCompleted == null)
			{
				this.DeleteStockNicknameExtraOperationCompleted = new SendOrPostCallback(this.OnDeleteStockNicknameExtraOperationCompleted);
			}
			base.InvokeAsync("DeleteStockNicknameExtra", new object[]
			{
				custId,
				stock
			}, this.DeleteStockNicknameExtraOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnDeleteStockNicknameExtraOperationCompleted(object arg)
		{
			if (this._DeleteStockNicknameExtraCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._DeleteStockNicknameExtraCompleted(this, new DeleteStockNicknameExtraCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/IntradayChart", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string IntradayChart(int stockNumber, string authenKey)
		{
			object[] array = base.Invoke("IntradayChart", new object[]
			{
				stockNumber,
				authenKey
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void IntradayChartAsync(int stockNumber, string authenKey)
		{
			this.IntradayChartAsync(stockNumber, authenKey, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void IntradayChartAsync(int stockNumber, string authenKey, object userState)
		{
			if (this.IntradayChartOperationCompleted == null)
			{
				this.IntradayChartOperationCompleted = new SendOrPostCallback(this.OnIntradayChartOperationCompleted);
			}
			base.InvokeAsync("IntradayChart", new object[]
			{
				stockNumber,
				authenKey
			}, this.IntradayChartOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnIntradayChartOperationCompleted(object arg)
		{
			if (this._IntradayChartCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._IntradayChartCompleted(this, new IntradayChartCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/IntradayIndexChart", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string IntradayIndexChart(string symbol, string authenKey)
		{
			object[] array = base.Invoke("IntradayIndexChart", new object[]
			{
				symbol,
				authenKey
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void IntradayIndexChartAsync(string symbol, string authenKey)
		{
			this.IntradayIndexChartAsync(symbol, authenKey, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void IntradayIndexChartAsync(string symbol, string authenKey, object userState)
		{
			if (this.IntradayIndexChartOperationCompleted == null)
			{
				this.IntradayIndexChartOperationCompleted = new SendOrPostCallback(this.OnIntradayIndexChartOperationCompleted);
			}
			base.InvokeAsync("IntradayIndexChart", new object[]
			{
				symbol,
				authenKey
			}, this.IntradayIndexChartOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnIntradayIndexChartOperationCompleted(object arg)
		{
			if (this._IntradayIndexChartCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._IntradayIndexChartCompleted(this, new IntradayIndexChartCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/GetChartImage", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string GetChartImage(int stockNumber, bool showVolumn, int width, int height)
		{
			object[] array = base.Invoke("GetChartImage", new object[]
			{
				stockNumber,
				showVolumn,
				width,
				height
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetChartImageAsync(int stockNumber, bool showVolumn, int width, int height)
		{
			this.GetChartImageAsync(stockNumber, showVolumn, width, height, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetChartImageAsync(int stockNumber, bool showVolumn, int width, int height, object userState)
		{
			if (this.GetChartImageOperationCompleted == null)
			{
				this.GetChartImageOperationCompleted = new SendOrPostCallback(this.OnGetChartImageOperationCompleted);
			}
			base.InvokeAsync("GetChartImage", new object[]
			{
				stockNumber,
				showVolumn,
				width,
				height
			}, this.GetChartImageOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnGetChartImageOperationCompleted(object arg)
		{
			if (this._GetChartImageCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._GetChartImageCompleted(this, new GetChartImageCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/GetSetIndexChartImage", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string GetSetIndexChartImage(string symbol, double prior, int width, int height)
		{
			object[] array = base.Invoke("GetSetIndexChartImage", new object[]
			{
				symbol,
				prior,
				width,
				height
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetSetIndexChartImageAsync(string symbol, double prior, int width, int height)
		{
			this.GetSetIndexChartImageAsync(symbol, prior, width, height, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetSetIndexChartImageAsync(string symbol, double prior, int width, int height, object userState)
		{
			if (this.GetSetIndexChartImageOperationCompleted == null)
			{
				this.GetSetIndexChartImageOperationCompleted = new SendOrPostCallback(this.OnGetSetIndexChartImageOperationCompleted);
			}
			base.InvokeAsync("GetSetIndexChartImage", new object[]
			{
				symbol,
				prior,
				width,
				height
			}, this.GetSetIndexChartImageOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnGetSetIndexChartImageOperationCompleted(object arg)
		{
			if (this._GetSetIndexChartImageCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._GetSetIndexChartImageCompleted(this, new GetSetIndexChartImageCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/GetPortfolioStatAllStock", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string GetPortfolioStatAllStock(string custId, string startDate, string endDate)
		{
			object[] array = base.Invoke("GetPortfolioStatAllStock", new object[]
			{
				custId,
				startDate,
				endDate
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetPortfolioStatAllStockAsync(string custId, string startDate, string endDate)
		{
			this.GetPortfolioStatAllStockAsync(custId, startDate, endDate, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetPortfolioStatAllStockAsync(string custId, string startDate, string endDate, object userState)
		{
			if (this.GetPortfolioStatAllStockOperationCompleted == null)
			{
				this.GetPortfolioStatAllStockOperationCompleted = new SendOrPostCallback(this.OnGetPortfolioStatAllStockOperationCompleted);
			}
			base.InvokeAsync("GetPortfolioStatAllStock", new object[]
			{
				custId,
				startDate,
				endDate
			}, this.GetPortfolioStatAllStockOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnGetPortfolioStatAllStockOperationCompleted(object arg)
		{
			if (this._GetPortfolioStatAllStockCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._GetPortfolioStatAllStockCompleted(this, new GetPortfolioStatAllStockCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/GetPortfolioStatByStock", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string GetPortfolioStatByStock(string custId, string stock, string startDate, string endDate)
		{
			object[] array = base.Invoke("GetPortfolioStatByStock", new object[]
			{
				custId,
				stock,
				startDate,
				endDate
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetPortfolioStatByStockAsync(string custId, string stock, string startDate, string endDate)
		{
			this.GetPortfolioStatByStockAsync(custId, stock, startDate, endDate, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetPortfolioStatByStockAsync(string custId, string stock, string startDate, string endDate, object userState)
		{
			if (this.GetPortfolioStatByStockOperationCompleted == null)
			{
				this.GetPortfolioStatByStockOperationCompleted = new SendOrPostCallback(this.OnGetPortfolioStatByStockOperationCompleted);
			}
			base.InvokeAsync("GetPortfolioStatByStock", new object[]
			{
				custId,
				stock,
				startDate,
				endDate
			}, this.GetPortfolioStatByStockOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnGetPortfolioStatByStockOperationCompleted(object arg)
		{
			if (this._GetPortfolioStatByStockCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._GetPortfolioStatByStockCompleted(this, new GetPortfolioStatByStockCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/SavePushAccount", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool SavePushAccount(string accountlogin, string registrationID, string device, string accList)
		{
			object[] array = base.Invoke("SavePushAccount", new object[]
			{
				accountlogin,
				registrationID,
				device,
				accList
			});
			return (bool)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SavePushAccountAsync(string accountlogin, string registrationID, string device, string accList)
		{
			this.SavePushAccountAsync(accountlogin, registrationID, device, accList, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SavePushAccountAsync(string accountlogin, string registrationID, string device, string accList, object userState)
		{
			if (this.SavePushAccountOperationCompleted == null)
			{
				this.SavePushAccountOperationCompleted = new SendOrPostCallback(this.OnSavePushAccountOperationCompleted);
			}
			base.InvokeAsync("SavePushAccount", new object[]
			{
				accountlogin,
				registrationID,
				device,
				accList
			}, this.SavePushAccountOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnSavePushAccountOperationCompleted(object arg)
		{
			if (this._SavePushAccountCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._SavePushAccountCompleted(this, new SavePushAccountCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/SaveStockAlert", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string SaveStockAlert(string userId, string stock, string field, int operatorType, string mobileAlert, decimal value, int updateMode, string memo)
		{
			object[] array = base.Invoke("SaveStockAlert", new object[]
			{
				userId,
				stock,
				field,
				operatorType,
				mobileAlert,
				value,
				updateMode,
				memo
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SaveStockAlertAsync(string userId, string stock, string field, int operatorType, string mobileAlert, decimal value, int updateMode, string memo)
		{
			this.SaveStockAlertAsync(userId, stock, field, operatorType, mobileAlert, value, updateMode, memo, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SaveStockAlertAsync(string userId, string stock, string field, int operatorType, string mobileAlert, decimal value, int updateMode, string memo, object userState)
		{
			if (this.SaveStockAlertOperationCompleted == null)
			{
				this.SaveStockAlertOperationCompleted = new SendOrPostCallback(this.OnSaveStockAlertOperationCompleted);
			}
			base.InvokeAsync("SaveStockAlert", new object[]
			{
				userId,
				stock,
				field,
				operatorType,
				mobileAlert,
				value,
				updateMode,
				memo
			}, this.SaveStockAlertOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnSaveStockAlertOperationCompleted(object arg)
		{
			if (this._SaveStockAlertCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._SaveStockAlertCompleted(this, new SaveStockAlertCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/SavePortAlert", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public long SavePortAlert(string userId, string account, string stock, string sType, int trusteeId, decimal costPct, decimal pricePct, decimal sellPct)
		{
			object[] array = base.Invoke("SavePortAlert", new object[]
			{
				userId,
				account,
				stock,
				sType,
				trusteeId,
				costPct,
				pricePct,
				sellPct
			});
			return (long)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SavePortAlertAsync(string userId, string account, string stock, string sType, int trusteeId, decimal costPct, decimal pricePct, decimal sellPct)
		{
			this.SavePortAlertAsync(userId, account, stock, sType, trusteeId, costPct, pricePct, sellPct, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SavePortAlertAsync(string userId, string account, string stock, string sType, int trusteeId, decimal costPct, decimal pricePct, decimal sellPct, object userState)
		{
			if (this.SavePortAlertOperationCompleted == null)
			{
				this.SavePortAlertOperationCompleted = new SendOrPostCallback(this.OnSavePortAlertOperationCompleted);
			}
			base.InvokeAsync("SavePortAlert", new object[]
			{
				userId,
				account,
				stock,
				sType,
				trusteeId,
				costPct,
				pricePct,
				sellPct
			}, this.SavePortAlertOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnSavePortAlertOperationCompleted(object arg)
		{
			if (this._SavePortAlertCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._SavePortAlertCompleted(this, new SavePortAlertCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/SendStopOrder", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string SendStopOrder(string userId, int stockNumber, int fieldType, int operatorType, decimal price, string ordAccount, string ordSide, int ordTtf, long ordVolume, string ordPrice, long ordPublish, string ordCondition, string ordEntryId, string kimengSession, string kimengLocal, int limit, string authenKey)
		{
			object[] array = base.Invoke("SendStopOrder", new object[]
			{
				userId,
				stockNumber,
				fieldType,
				operatorType,
				price,
				ordAccount,
				ordSide,
				ordTtf,
				ordVolume,
				ordPrice,
				ordPublish,
				ordCondition,
				ordEntryId,
				kimengSession,
				kimengLocal,
				limit,
				authenKey
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SendStopOrderAsync(string userId, int stockNumber, int fieldType, int operatorType, decimal price, string ordAccount, string ordSide, int ordTtf, long ordVolume, string ordPrice, long ordPublish, string ordCondition, string ordEntryId, string kimengSession, string kimengLocal, int limit, string authenKey)
		{
			this.SendStopOrderAsync(userId, stockNumber, fieldType, operatorType, price, ordAccount, ordSide, ordTtf, ordVolume, ordPrice, ordPublish, ordCondition, ordEntryId, kimengSession, kimengLocal, limit, authenKey, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SendStopOrderAsync(string userId, int stockNumber, int fieldType, int operatorType, decimal price, string ordAccount, string ordSide, int ordTtf, long ordVolume, string ordPrice, long ordPublish, string ordCondition, string ordEntryId, string kimengSession, string kimengLocal, int limit, string authenKey, object userState)
		{
			if (this.SendStopOrderOperationCompleted == null)
			{
				this.SendStopOrderOperationCompleted = new SendOrPostCallback(this.OnSendStopOrderOperationCompleted);
			}
			base.InvokeAsync("SendStopOrder", new object[]
			{
				userId,
				stockNumber,
				fieldType,
				operatorType,
				price,
				ordAccount,
				ordSide,
				ordTtf,
				ordVolume,
				ordPrice,
				ordPublish,
				ordCondition,
				ordEntryId,
				kimengSession,
				kimengLocal,
				limit,
				authenKey
			}, this.SendStopOrderOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnSendStopOrderOperationCompleted(object arg)
		{
			if (this._SendStopOrderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._SendStopOrderCompleted(this, new SendStopOrderCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/ViewStopOrder", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string ViewStopOrder(string userId, string account, int refNo)
		{
			object[] array = base.Invoke("ViewStopOrder", new object[]
			{
				userId,
				account,
				refNo
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewStopOrderAsync(string userId, string account, int refNo)
		{
			this.ViewStopOrderAsync(userId, account, refNo, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewStopOrderAsync(string userId, string account, int refNo, object userState)
		{
			if (this.ViewStopOrderOperationCompleted == null)
			{
				this.ViewStopOrderOperationCompleted = new SendOrPostCallback(this.OnViewStopOrderOperationCompleted);
			}
			base.InvokeAsync("ViewStopOrder", new object[]
			{
				userId,
				account,
				refNo
			}, this.ViewStopOrderOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnViewStopOrderOperationCompleted(object arg)
		{
			if (this._ViewStopOrderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._ViewStopOrderCompleted(this, new ViewStopOrderCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/ViewStopOrder_FirstLS", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string ViewStopOrder_FirstLS(int stockNo, string condition, string time)
		{
			object[] array = base.Invoke("ViewStopOrder_FirstLS", new object[]
			{
				stockNo,
				condition,
				time
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewStopOrder_FirstLSAsync(int stockNo, string condition, string time)
		{
			this.ViewStopOrder_FirstLSAsync(stockNo, condition, time, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewStopOrder_FirstLSAsync(int stockNo, string condition, string time, object userState)
		{
			if (this.ViewStopOrder_FirstLSOperationCompleted == null)
			{
				this.ViewStopOrder_FirstLSOperationCompleted = new SendOrPostCallback(this.OnViewStopOrder_FirstLSOperationCompleted);
			}
			base.InvokeAsync("ViewStopOrder_FirstLS", new object[]
			{
				stockNo,
				condition,
				time
			}, this.ViewStopOrder_FirstLSOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnViewStopOrder_FirstLSOperationCompleted(object arg)
		{
			if (this._ViewStopOrder_FirstLSCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._ViewStopOrder_FirstLSCompleted(this, new ViewStopOrder_FirstLSCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/SendCancelStopOrder", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string SendCancelStopOrder(string userId, int refNo, int stockNo, string authenKey)
		{
			object[] array = base.Invoke("SendCancelStopOrder", new object[]
			{
				userId,
				refNo,
				stockNo,
				authenKey
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SendCancelStopOrderAsync(string userId, int refNo, int stockNo, string authenKey)
		{
			this.SendCancelStopOrderAsync(userId, refNo, stockNo, authenKey, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SendCancelStopOrderAsync(string userId, int refNo, int stockNo, string authenKey, object userState)
		{
			if (this.SendCancelStopOrderOperationCompleted == null)
			{
				this.SendCancelStopOrderOperationCompleted = new SendOrPostCallback(this.OnSendCancelStopOrderOperationCompleted);
			}
			base.InvokeAsync("SendCancelStopOrder", new object[]
			{
				userId,
				refNo,
				stockNo,
				authenKey
			}, this.SendCancelStopOrderOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnSendCancelStopOrderOperationCompleted(object arg)
		{
			if (this._SendCancelStopOrderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._SendCancelStopOrderCompleted(this, new SendCancelStopOrderCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/StopOrderRegister", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string StopOrderRegister(string userId, bool isRegister)
		{
			object[] array = base.Invoke("StopOrderRegister", new object[]
			{
				userId,
				isRegister
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void StopOrderRegisterAsync(string userId, bool isRegister)
		{
			this.StopOrderRegisterAsync(userId, isRegister, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void StopOrderRegisterAsync(string userId, bool isRegister, object userState)
		{
			if (this.StopOrderRegisterOperationCompleted == null)
			{
				this.StopOrderRegisterOperationCompleted = new SendOrPostCallback(this.OnStopOrderRegisterOperationCompleted);
			}
			base.InvokeAsync("StopOrderRegister", new object[]
			{
				userId,
				isRegister
			}, this.StopOrderRegisterOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnStopOrderRegisterOperationCompleted(object arg)
		{
			if (this._StopOrderRegisterCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._StopOrderRegisterCompleted(this, new StopOrderRegisterCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/StopOrderCheckDisclaimer", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string StopOrderCheckDisclaimer(string userId)
		{
			object[] array = base.Invoke("StopOrderCheckDisclaimer", new object[]
			{
				userId
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void StopOrderCheckDisclaimerAsync(string userId)
		{
			this.StopOrderCheckDisclaimerAsync(userId, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void StopOrderCheckDisclaimerAsync(string userId, object userState)
		{
			if (this.StopOrderCheckDisclaimerOperationCompleted == null)
			{
				this.StopOrderCheckDisclaimerOperationCompleted = new SendOrPostCallback(this.OnStopOrderCheckDisclaimerOperationCompleted);
			}
			base.InvokeAsync("StopOrderCheckDisclaimer", new object[]
			{
				userId
			}, this.StopOrderCheckDisclaimerOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnStopOrderCheckDisclaimerOperationCompleted(object arg)
		{
			if (this._StopOrderCheckDisclaimerCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._StopOrderCheckDisclaimerCompleted(this, new StopOrderCheckDisclaimerCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/SaveUserAlert", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string SaveUserAlert(string userId, bool isRecvAdvertise, bool isMktSummary, bool isRecvPort, string device, int mode)
		{
			object[] array = base.Invoke("SaveUserAlert", new object[]
			{
				userId,
				isRecvAdvertise,
				isMktSummary,
				isRecvPort,
				device,
				mode
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SaveUserAlertAsync(string userId, bool isRecvAdvertise, bool isMktSummary, bool isRecvPort, string device, int mode)
		{
			this.SaveUserAlertAsync(userId, isRecvAdvertise, isMktSummary, isRecvPort, device, mode, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SaveUserAlertAsync(string userId, bool isRecvAdvertise, bool isMktSummary, bool isRecvPort, string device, int mode, object userState)
		{
			if (this.SaveUserAlertOperationCompleted == null)
			{
				this.SaveUserAlertOperationCompleted = new SendOrPostCallback(this.OnSaveUserAlertOperationCompleted);
			}
			base.InvokeAsync("SaveUserAlert", new object[]
			{
				userId,
				isRecvAdvertise,
				isMktSummary,
				isRecvPort,
				device,
				mode
			}, this.SaveUserAlertOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnSaveUserAlertOperationCompleted(object arg)
		{
			if (this._SaveUserAlertCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._SaveUserAlertCompleted(this, new SaveUserAlertCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/SaveUserAlert2", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string SaveUserAlert2(string userId, bool isRecvAdvertise, bool isMktSummary, bool isRecvPort, string lstActiveGroup)
		{
			object[] array = base.Invoke("SaveUserAlert2", new object[]
			{
				userId,
				isRecvAdvertise,
				isMktSummary,
				isRecvPort,
				lstActiveGroup
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SaveUserAlert2Async(string userId, bool isRecvAdvertise, bool isMktSummary, bool isRecvPort, string lstActiveGroup)
		{
			this.SaveUserAlert2Async(userId, isRecvAdvertise, isMktSummary, isRecvPort, lstActiveGroup, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SaveUserAlert2Async(string userId, bool isRecvAdvertise, bool isMktSummary, bool isRecvPort, string lstActiveGroup, object userState)
		{
			if (this.SaveUserAlert2OperationCompleted == null)
			{
				this.SaveUserAlert2OperationCompleted = new SendOrPostCallback(this.OnSaveUserAlert2OperationCompleted);
			}
			base.InvokeAsync("SaveUserAlert2", new object[]
			{
				userId,
				isRecvAdvertise,
				isMktSummary,
				isRecvPort,
				lstActiveGroup
			}, this.SaveUserAlert2OperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnSaveUserAlert2OperationCompleted(object arg)
		{
			if (this._SaveUserAlert2Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._SaveUserAlert2Completed(this, new SaveUserAlert2CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/GetAlertLog", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string GetAlertLog(string userId)
		{
			object[] array = base.Invoke("GetAlertLog", new object[]
			{
				userId
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetAlertLogAsync(string userId)
		{
			this.GetAlertLogAsync(userId, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetAlertLogAsync(string userId, object userState)
		{
			if (this.GetAlertLogOperationCompleted == null)
			{
				this.GetAlertLogOperationCompleted = new SendOrPostCallback(this.OnGetAlertLogOperationCompleted);
			}
			base.InvokeAsync("GetAlertLog", new object[]
			{
				userId
			}, this.GetAlertLogOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnGetAlertLogOperationCompleted(object arg)
		{
			if (this._GetAlertLogCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._GetAlertLogCompleted(this, new GetAlertLogCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/GetUserAlert", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string GetUserAlert(string userId)
		{
			object[] array = base.Invoke("GetUserAlert", new object[]
			{
				userId
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetUserAlertAsync(string userId)
		{
			this.GetUserAlertAsync(userId, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetUserAlertAsync(string userId, object userState)
		{
			if (this.GetUserAlertOperationCompleted == null)
			{
				this.GetUserAlertOperationCompleted = new SendOrPostCallback(this.OnGetUserAlertOperationCompleted);
			}
			base.InvokeAsync("GetUserAlert", new object[]
			{
				userId
			}, this.GetUserAlertOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnGetUserAlertOperationCompleted(object arg)
		{
			if (this._GetUserAlertCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._GetUserAlertCompleted(this, new GetUserAlertCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/GetStockAlertItems", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string GetStockAlertItems(string userId)
		{
			object[] array = base.Invoke("GetStockAlertItems", new object[]
			{
				userId
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetStockAlertItemsAsync(string userId)
		{
			this.GetStockAlertItemsAsync(userId, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetStockAlertItemsAsync(string userId, object userState)
		{
			if (this.GetStockAlertItemsOperationCompleted == null)
			{
				this.GetStockAlertItemsOperationCompleted = new SendOrPostCallback(this.OnGetStockAlertItemsOperationCompleted);
			}
			base.InvokeAsync("GetStockAlertItems", new object[]
			{
				userId
			}, this.GetStockAlertItemsOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnGetStockAlertItemsOperationCompleted(object arg)
		{
			if (this._GetStockAlertItemsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._GetStockAlertItemsCompleted(this, new GetStockAlertItemsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/GetPortAlertItems", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string GetPortAlertItems(string account)
		{
			object[] array = base.Invoke("GetPortAlertItems", new object[]
			{
				account
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetPortAlertItemsAsync(string account)
		{
			this.GetPortAlertItemsAsync(account, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetPortAlertItemsAsync(string account, object userState)
		{
			if (this.GetPortAlertItemsOperationCompleted == null)
			{
				this.GetPortAlertItemsOperationCompleted = new SendOrPostCallback(this.OnGetPortAlertItemsOperationCompleted);
			}
			base.InvokeAsync("GetPortAlertItems", new object[]
			{
				account
			}, this.GetPortAlertItemsOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnGetPortAlertItemsOperationCompleted(object arg)
		{
			if (this._GetPortAlertItemsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._GetPortAlertItemsCompleted(this, new GetPortAlertItemsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/NAVChart", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string NAVChart(string account, string startDate, string endDate)
		{
			object[] array = base.Invoke("NAVChart", new object[]
			{
				account,
				startDate,
				endDate
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void NAVChartAsync(string account, string startDate, string endDate)
		{
			this.NAVChartAsync(account, startDate, endDate, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void NAVChartAsync(string account, string startDate, string endDate, object userState)
		{
			if (this.NAVChartOperationCompleted == null)
			{
				this.NAVChartOperationCompleted = new SendOrPostCallback(this.OnNAVChartOperationCompleted);
			}
			base.InvokeAsync("NAVChart", new object[]
			{
				account,
				startDate,
				endDate
			}, this.NAVChartOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnNAVChartOperationCompleted(object arg)
		{
			if (this._NAVChartCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._NAVChartCompleted(this, new NAVChartCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/GetDataForSmartClick", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string GetDataForSmartClick(string stock, string account)
		{
			object[] array = base.Invoke("GetDataForSmartClick", new object[]
			{
				stock,
				account
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetDataForSmartClickAsync(string stock, string account)
		{
			this.GetDataForSmartClickAsync(stock, account, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetDataForSmartClickAsync(string stock, string account, object userState)
		{
			if (this.GetDataForSmartClickOperationCompleted == null)
			{
				this.GetDataForSmartClickOperationCompleted = new SendOrPostCallback(this.OnGetDataForSmartClickOperationCompleted);
			}
			base.InvokeAsync("GetDataForSmartClick", new object[]
			{
				stock,
				account
			}, this.GetDataForSmartClickOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnGetDataForSmartClickOperationCompleted(object arg)
		{
			if (this._GetDataForSmartClickCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._GetDataForSmartClickCompleted(this, new GetDataForSmartClickCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/GetOrderFor1Click", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string GetOrderFor1Click(string stock, string account, string price, string side)
		{
			object[] array = base.Invoke("GetOrderFor1Click", new object[]
			{
				stock,
				account,
				price,
				side
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetOrderFor1ClickAsync(string stock, string account, string price, string side)
		{
			this.GetOrderFor1ClickAsync(stock, account, price, side, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetOrderFor1ClickAsync(string stock, string account, string price, string side, object userState)
		{
			if (this.GetOrderFor1ClickOperationCompleted == null)
			{
				this.GetOrderFor1ClickOperationCompleted = new SendOrPostCallback(this.OnGetOrderFor1ClickOperationCompleted);
			}
			base.InvokeAsync("GetOrderFor1Click", new object[]
			{
				stock,
				account,
				price,
				side
			}, this.GetOrderFor1ClickOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnGetOrderFor1ClickOperationCompleted(object arg)
		{
			if (this._GetOrderFor1ClickCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._GetOrderFor1ClickCompleted(this, new GetOrderFor1ClickCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/SaveSummaryMarketData", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public int SaveSummaryMarketData(string date, string sumBy, string investor, decimal buyValue, decimal pctBuyValue, decimal sellValue, decimal pctSellValue, decimal sumValue, decimal pctSumValue)
		{
			object[] array = base.Invoke("SaveSummaryMarketData", new object[]
			{
				date,
				sumBy,
				investor,
				buyValue,
				pctBuyValue,
				sellValue,
				pctSellValue,
				sumValue,
				pctSumValue
			});
			return (int)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SaveSummaryMarketDataAsync(string date, string sumBy, string investor, decimal buyValue, decimal pctBuyValue, decimal sellValue, decimal pctSellValue, decimal sumValue, decimal pctSumValue)
		{
			this.SaveSummaryMarketDataAsync(date, sumBy, investor, buyValue, pctBuyValue, sellValue, pctSellValue, sumValue, pctSumValue, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SaveSummaryMarketDataAsync(string date, string sumBy, string investor, decimal buyValue, decimal pctBuyValue, decimal sellValue, decimal pctSellValue, decimal sumValue, decimal pctSumValue, object userState)
		{
			if (this.SaveSummaryMarketDataOperationCompleted == null)
			{
				this.SaveSummaryMarketDataOperationCompleted = new SendOrPostCallback(this.OnSaveSummaryMarketDataOperationCompleted);
			}
			base.InvokeAsync("SaveSummaryMarketData", new object[]
			{
				date,
				sumBy,
				investor,
				buyValue,
				pctBuyValue,
				sellValue,
				pctSellValue,
				sumValue,
				pctSumValue
			}, this.SaveSummaryMarketDataOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnSaveSummaryMarketDataOperationCompleted(object arg)
		{
			if (this._SaveSummaryMarketDataCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._SaveSummaryMarketDataCompleted(this, new SaveSummaryMarketDataCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/GetStockSMA", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public decimal GetStockSMA(string stock, int period)
		{
			object[] array = base.Invoke("GetStockSMA", new object[]
			{
				stock,
				period
			});
			return (decimal)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetStockSMAAsync(string stock, int period)
		{
			this.GetStockSMAAsync(stock, period, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetStockSMAAsync(string stock, int period, object userState)
		{
			if (this.GetStockSMAOperationCompleted == null)
			{
				this.GetStockSMAOperationCompleted = new SendOrPostCallback(this.OnGetStockSMAOperationCompleted);
			}
			base.InvokeAsync("GetStockSMA", new object[]
			{
				stock,
				period
			}, this.GetStockSMAOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnGetStockSMAOperationCompleted(object arg)
		{
			if (this._GetStockSMACompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._GetStockSMACompleted(this, new GetStockSMACompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/SaveUserConfig", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string SaveUserConfig(string userId, string keyValue, string value)
		{
			object[] array = base.Invoke("SaveUserConfig", new object[]
			{
				userId,
				keyValue,
				value
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SaveUserConfigAsync(string userId, string keyValue, string value)
		{
			this.SaveUserConfigAsync(userId, keyValue, value, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SaveUserConfigAsync(string userId, string keyValue, string value, object userState)
		{
			if (this.SaveUserConfigOperationCompleted == null)
			{
				this.SaveUserConfigOperationCompleted = new SendOrPostCallback(this.OnSaveUserConfigOperationCompleted);
			}
			base.InvokeAsync("SaveUserConfig", new object[]
			{
				userId,
				keyValue,
				value
			}, this.SaveUserConfigOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnSaveUserConfigOperationCompleted(object arg)
		{
			if (this._SaveUserConfigCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._SaveUserConfigCompleted(this, new SaveUserConfigCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/SaveUserConfigAll", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string SaveUserConfigAll(string userId, string value)
		{
			object[] array = base.Invoke("SaveUserConfigAll", new object[]
			{
				userId,
				value
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SaveUserConfigAllAsync(string userId, string value)
		{
			this.SaveUserConfigAllAsync(userId, value, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SaveUserConfigAllAsync(string userId, string value, object userState)
		{
			if (this.SaveUserConfigAllOperationCompleted == null)
			{
				this.SaveUserConfigAllOperationCompleted = new SendOrPostCallback(this.OnSaveUserConfigAllOperationCompleted);
			}
			base.InvokeAsync("SaveUserConfigAll", new object[]
			{
				userId,
				value
			}, this.SaveUserConfigAllOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnSaveUserConfigAllOperationCompleted(object arg)
		{
			if (this._SaveUserConfigAllCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._SaveUserConfigAllCompleted(this, new SaveUserConfigAllCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/SendHeartBeat", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool SendHeartBeat(string userID, string session)
		{
			object[] array = base.Invoke("SendHeartBeat", new object[]
			{
				userID,
				session
			});
			return (bool)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SendHeartBeatAsync(string userID, string session)
		{
			this.SendHeartBeatAsync(userID, session, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SendHeartBeatAsync(string userID, string session, object userState)
		{
			if (this.SendHeartBeatOperationCompleted == null)
			{
				this.SendHeartBeatOperationCompleted = new SendOrPostCallback(this.OnSendHeartBeatOperationCompleted);
			}
			base.InvokeAsync("SendHeartBeat", new object[]
			{
				userID,
				session
			}, this.SendHeartBeatOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnSendHeartBeatOperationCompleted(object arg)
		{
			if (this._SendHeartBeatCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._SendHeartBeatCompleted(this, new SendHeartBeatCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/SendHeartBeat2", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool SendHeartBeat2(string param)
		{
			object[] array = base.Invoke("SendHeartBeat2", new object[]
			{
				param
			});
			return (bool)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SendHeartBeat2Async(string param)
		{
			this.SendHeartBeat2Async(param, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SendHeartBeat2Async(string param, object userState)
		{
			if (this.SendHeartBeat2OperationCompleted == null)
			{
				this.SendHeartBeat2OperationCompleted = new SendOrPostCallback(this.OnSendHeartBeat2OperationCompleted);
			}
			base.InvokeAsync("SendHeartBeat2", new object[]
			{
				param
			}, this.SendHeartBeat2OperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnSendHeartBeat2OperationCompleted(object arg)
		{
			if (this._SendHeartBeat2Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._SendHeartBeat2Completed(this, new SendHeartBeat2CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/SaveUserEfinForward", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SaveUserEfinForward(string fullname, string nickname, string userId, string regisDate, string emailAccount)
		{
			base.Invoke("SaveUserEfinForward", new object[]
			{
				fullname,
				nickname,
				userId,
				regisDate,
				emailAccount
			});
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SaveUserEfinForwardAsync(string fullname, string nickname, string userId, string regisDate, string emailAccount)
		{
			this.SaveUserEfinForwardAsync(fullname, nickname, userId, regisDate, emailAccount, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SaveUserEfinForwardAsync(string fullname, string nickname, string userId, string regisDate, string emailAccount, object userState)
		{
			if (this.SaveUserEfinForwardOperationCompleted == null)
			{
				this.SaveUserEfinForwardOperationCompleted = new SendOrPostCallback(this.OnSaveUserEfinForwardOperationCompleted);
			}
			base.InvokeAsync("SaveUserEfinForward", new object[]
			{
				fullname,
				nickname,
				userId,
				regisDate,
				emailAccount
			}, this.SaveUserEfinForwardOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnSaveUserEfinForwardOperationCompleted(object arg)
		{
			if (this._SaveUserEfinForwardCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._SaveUserEfinForwardCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/LoadAllInformation", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string LoadAllInformation()
		{
			object[] array = base.Invoke("LoadAllInformation", new object[0]);
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void LoadAllInformationAsync()
		{
			this.LoadAllInformationAsync(null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void LoadAllInformationAsync(object userState)
		{
			if (this.LoadAllInformationOperationCompleted == null)
			{
				this.LoadAllInformationOperationCompleted = new SendOrPostCallback(this.OnLoadAllInformationOperationCompleted);
			}
			base.InvokeAsync("LoadAllInformation", new object[0], this.LoadAllInformationOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnLoadAllInformationOperationCompleted(object arg)
		{
			if (this._LoadAllInformationCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._LoadAllInformationCompleted(this, new LoadAllInformationCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/LoadAllInformationIsodd", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string LoadAllInformationIsodd(bool isLoadOddlot)
		{
			object[] array = base.Invoke("LoadAllInformationIsodd", new object[]
			{
				isLoadOddlot
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void LoadAllInformationIsoddAsync(bool isLoadOddlot)
		{
			this.LoadAllInformationIsoddAsync(isLoadOddlot, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void LoadAllInformationIsoddAsync(bool isLoadOddlot, object userState)
		{
			if (this.LoadAllInformationIsoddOperationCompleted == null)
			{
				this.LoadAllInformationIsoddOperationCompleted = new SendOrPostCallback(this.OnLoadAllInformationIsoddOperationCompleted);
			}
			base.InvokeAsync("LoadAllInformationIsodd", new object[]
			{
				isLoadOddlot
			}, this.LoadAllInformationIsoddOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnLoadAllInformationIsoddOperationCompleted(object arg)
		{
			if (this._LoadAllInformationIsoddCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._LoadAllInformationIsoddCompleted(this, new LoadAllInformationIsoddCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/LoadStockInformation", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string LoadStockInformation(int startSecNo, int topSelect)
		{
			object[] array = base.Invoke("LoadStockInformation", new object[]
			{
				startSecNo,
				topSelect
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void LoadStockInformationAsync(int startSecNo, int topSelect)
		{
			this.LoadStockInformationAsync(startSecNo, topSelect, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void LoadStockInformationAsync(int startSecNo, int topSelect, object userState)
		{
			if (this.LoadStockInformationOperationCompleted == null)
			{
				this.LoadStockInformationOperationCompleted = new SendOrPostCallback(this.OnLoadStockInformationOperationCompleted);
			}
			base.InvokeAsync("LoadStockInformation", new object[]
			{
				startSecNo,
				topSelect
			}, this.LoadStockInformationOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnLoadStockInformationOperationCompleted(object arg)
		{
			if (this._LoadStockInformationCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._LoadStockInformationCompleted(this, new LoadStockInformationCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/LoadMarketInformation", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string LoadMarketInformation()
		{
			object[] array = base.Invoke("LoadMarketInformation", new object[0]);
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void LoadMarketInformationAsync()
		{
			this.LoadMarketInformationAsync(null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void LoadMarketInformationAsync(object userState)
		{
			if (this.LoadMarketInformationOperationCompleted == null)
			{
				this.LoadMarketInformationOperationCompleted = new SendOrPostCallback(this.OnLoadMarketInformationOperationCompleted);
			}
			base.InvokeAsync("LoadMarketInformation", new object[0], this.LoadMarketInformationOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnLoadMarketInformationOperationCompleted(object arg)
		{
			if (this._LoadMarketInformationCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._LoadMarketInformationCompleted(this, new LoadMarketInformationCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/LoadOddLotInformation", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string LoadOddLotInformation()
		{
			object[] array = base.Invoke("LoadOddLotInformation", new object[0]);
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void LoadOddLotInformationAsync()
		{
			this.LoadOddLotInformationAsync(null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void LoadOddLotInformationAsync(object userState)
		{
			if (this.LoadOddLotInformationOperationCompleted == null)
			{
				this.LoadOddLotInformationOperationCompleted = new SendOrPostCallback(this.OnLoadOddLotInformationOperationCompleted);
			}
			base.InvokeAsync("LoadOddLotInformation", new object[0], this.LoadOddLotInformationOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnLoadOddLotInformationOperationCompleted(object arg)
		{
			if (this._LoadOddLotInformationCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._LoadOddLotInformationCompleted(this, new LoadOddLotInformationCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/BoardcastMessage", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string BoardcastMessage(string account, int recordPerPage)
		{
			object[] array = base.Invoke("BoardcastMessage", new object[]
			{
				account,
				recordPerPage
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void BoardcastMessageAsync(string account, int recordPerPage)
		{
			this.BoardcastMessageAsync(account, recordPerPage, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void BoardcastMessageAsync(string account, int recordPerPage, object userState)
		{
			if (this.BoardcastMessageOperationCompleted == null)
			{
				this.BoardcastMessageOperationCompleted = new SendOrPostCallback(this.OnBoardcastMessageOperationCompleted);
			}
			base.InvokeAsync("BoardcastMessage", new object[]
			{
				account,
				recordPerPage
			}, this.BoardcastMessageOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnBoardcastMessageOperationCompleted(object arg)
		{
			if (this._BoardcastMessageCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._BoardcastMessageCompleted(this, new BoardcastMessageCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/SendPushMessage", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string SendPushMessage(string key, string groupId, string message)
		{
			object[] array = base.Invoke("SendPushMessage", new object[]
			{
				key,
				groupId,
				message
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SendPushMessageAsync(string key, string groupId, string message)
		{
			this.SendPushMessageAsync(key, groupId, message, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SendPushMessageAsync(string key, string groupId, string message, object userState)
		{
			if (this.SendPushMessageOperationCompleted == null)
			{
				this.SendPushMessageOperationCompleted = new SendOrPostCallback(this.OnSendPushMessageOperationCompleted);
			}
			base.InvokeAsync("SendPushMessage", new object[]
			{
				key,
				groupId,
				message
			}, this.SendPushMessageOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnSendPushMessageOperationCompleted(object arg)
		{
			if (this._SendPushMessageCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._SendPushMessageCompleted(this, new SendPushMessageCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/BestBidOffer", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string BestBidOffer(string stockList)
		{
			object[] array = base.Invoke("BestBidOffer", new object[]
			{
				stockList
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void BestBidOfferAsync(string stockList)
		{
			this.BestBidOfferAsync(stockList, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void BestBidOfferAsync(string stockList, object userState)
		{
			if (this.BestBidOfferOperationCompleted == null)
			{
				this.BestBidOfferOperationCompleted = new SendOrPostCallback(this.OnBestBidOfferOperationCompleted);
			}
			base.InvokeAsync("BestBidOffer", new object[]
			{
				stockList
			}, this.BestBidOfferOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnBestBidOfferOperationCompleted(object arg)
		{
			if (this._BestBidOfferCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._BestBidOfferCompleted(this, new BestBidOfferCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/BestOpenPrice", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string BestOpenPrice(string viewType, string compareMode, string marketID, int sesssionID, int startRow, int rowPerPage)
		{
			object[] array = base.Invoke("BestOpenPrice", new object[]
			{
				viewType,
				compareMode,
				marketID,
				sesssionID,
				startRow,
				rowPerPage
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void BestOpenPriceAsync(string viewType, string compareMode, string marketID, int sesssionID, int startRow, int rowPerPage)
		{
			this.BestOpenPriceAsync(viewType, compareMode, marketID, sesssionID, startRow, rowPerPage, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void BestOpenPriceAsync(string viewType, string compareMode, string marketID, int sesssionID, int startRow, int rowPerPage, object userState)
		{
			if (this.BestOpenPriceOperationCompleted == null)
			{
				this.BestOpenPriceOperationCompleted = new SendOrPostCallback(this.OnBestOpenPriceOperationCompleted);
			}
			base.InvokeAsync("BestOpenPrice", new object[]
			{
				viewType,
				compareMode,
				marketID,
				sesssionID,
				startRow,
				rowPerPage
			}, this.BestOpenPriceOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnBestOpenPriceOperationCompleted(object arg)
		{
			if (this._BestOpenPriceCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._BestOpenPriceCompleted(this, new BestOpenPriceCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/BestProjected", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string BestProjected(string boardType, string compareMode, string poType, string marketID, int startRow, int rowPerPage)
		{
			object[] array = base.Invoke("BestProjected", new object[]
			{
				boardType,
				compareMode,
				poType,
				marketID,
				startRow,
				rowPerPage
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void BestProjectedAsync(string boardType, string compareMode, string poType, string marketID, int startRow, int rowPerPage)
		{
			this.BestProjectedAsync(boardType, compareMode, poType, marketID, startRow, rowPerPage, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void BestProjectedAsync(string boardType, string compareMode, string poType, string marketID, int startRow, int rowPerPage, object userState)
		{
			if (this.BestProjectedOperationCompleted == null)
			{
				this.BestProjectedOperationCompleted = new SendOrPostCallback(this.OnBestProjectedOperationCompleted);
			}
			base.InvokeAsync("BestProjected", new object[]
			{
				boardType,
				compareMode,
				poType,
				marketID,
				startRow,
				rowPerPage
			}, this.BestProjectedOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnBestProjectedOperationCompleted(object arg)
		{
			if (this._BestProjectedCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._BestProjectedCompleted(this, new BestProjectedCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/IndustryStat", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string IndustryStat()
		{
			object[] array = base.Invoke("IndustryStat", new object[0]);
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void IndustryStatAsync()
		{
			this.IndustryStatAsync(null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void IndustryStatAsync(object userState)
		{
			if (this.IndustryStatOperationCompleted == null)
			{
				this.IndustryStatOperationCompleted = new SendOrPostCallback(this.OnIndustryStatOperationCompleted);
			}
			base.InvokeAsync("IndustryStat", new object[0], this.IndustryStatOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnIndustryStatOperationCompleted(object arg)
		{
			if (this._IndustryStatCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._IndustryStatCompleted(this, new IndustryStatCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/TopActive", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string TopActive(string viewType, string board, string marketID, int rowPerPage)
		{
			object[] array = base.Invoke("TopActive", new object[]
			{
				viewType,
				board,
				marketID,
				rowPerPage
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void TopActiveAsync(string viewType, string board, string marketID, int rowPerPage)
		{
			this.TopActiveAsync(viewType, board, marketID, rowPerPage, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void TopActiveAsync(string viewType, string board, string marketID, int rowPerPage, object userState)
		{
			if (this.TopActiveOperationCompleted == null)
			{
				this.TopActiveOperationCompleted = new SendOrPostCallback(this.OnTopActiveOperationCompleted);
			}
			base.InvokeAsync("TopActive", new object[]
			{
				viewType,
				board,
				marketID,
				rowPerPage
			}, this.TopActiveOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnTopActiveOperationCompleted(object arg)
		{
			if (this._TopActiveCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._TopActiveCompleted(this, new TopActiveCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/TopActiveBBO", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string TopActiveBBO(string viewType, string board, string marketID, int rowPerPage)
		{
			object[] array = base.Invoke("TopActiveBBO", new object[]
			{
				viewType,
				board,
				marketID,
				rowPerPage
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void TopActiveBBOAsync(string viewType, string board, string marketID, int rowPerPage)
		{
			this.TopActiveBBOAsync(viewType, board, marketID, rowPerPage, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void TopActiveBBOAsync(string viewType, string board, string marketID, int rowPerPage, object userState)
		{
			if (this.TopActiveBBOOperationCompleted == null)
			{
				this.TopActiveBBOOperationCompleted = new SendOrPostCallback(this.OnTopActiveBBOOperationCompleted);
			}
			base.InvokeAsync("TopActiveBBO", new object[]
			{
				viewType,
				board,
				marketID,
				rowPerPage
			}, this.TopActiveBBOOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnTopActiveBBOOperationCompleted(object arg)
		{
			if (this._TopActiveBBOCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._TopActiveBBOCompleted(this, new TopActiveBBOCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/TopActiveBBO_Benefit", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string TopActiveBBO_Benefit()
		{
			object[] array = base.Invoke("TopActiveBBO_Benefit", new object[0]);
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void TopActiveBBO_BenefitAsync()
		{
			this.TopActiveBBO_BenefitAsync(null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void TopActiveBBO_BenefitAsync(object userState)
		{
			if (this.TopActiveBBO_BenefitOperationCompleted == null)
			{
				this.TopActiveBBO_BenefitOperationCompleted = new SendOrPostCallback(this.OnTopActiveBBO_BenefitOperationCompleted);
			}
			base.InvokeAsync("TopActiveBBO_Benefit", new object[0], this.TopActiveBBO_BenefitOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnTopActiveBBO_BenefitOperationCompleted(object arg)
		{
			if (this._TopActiveBBO_BenefitCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._TopActiveBBO_BenefitCompleted(this, new TopActiveBBO_BenefitCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/TopActiveBBO_Warrant", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string TopActiveBBO_Warrant()
		{
			object[] array = base.Invoke("TopActiveBBO_Warrant", new object[0]);
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void TopActiveBBO_WarrantAsync()
		{
			this.TopActiveBBO_WarrantAsync(null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void TopActiveBBO_WarrantAsync(object userState)
		{
			if (this.TopActiveBBO_WarrantOperationCompleted == null)
			{
				this.TopActiveBBO_WarrantOperationCompleted = new SendOrPostCallback(this.OnTopActiveBBO_WarrantOperationCompleted);
			}
			base.InvokeAsync("TopActiveBBO_Warrant", new object[0], this.TopActiveBBO_WarrantOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnTopActiveBBO_WarrantOperationCompleted(object arg)
		{
			if (this._TopActiveBBO_WarrantCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._TopActiveBBO_WarrantCompleted(this, new TopActiveBBO_WarrantCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/TopActiveBBO_DW", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string TopActiveBBO_DW(string parentStock)
		{
			object[] array = base.Invoke("TopActiveBBO_DW", new object[]
			{
				parentStock
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void TopActiveBBO_DWAsync(string parentStock)
		{
			this.TopActiveBBO_DWAsync(parentStock, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void TopActiveBBO_DWAsync(string parentStock, object userState)
		{
			if (this.TopActiveBBO_DWOperationCompleted == null)
			{
				this.TopActiveBBO_DWOperationCompleted = new SendOrPostCallback(this.OnTopActiveBBO_DWOperationCompleted);
			}
			base.InvokeAsync("TopActiveBBO_DW", new object[]
			{
				parentStock
			}, this.TopActiveBBO_DWOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnTopActiveBBO_DWOperationCompleted(object arg)
		{
			if (this._TopActiveBBO_DWCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._TopActiveBBO_DWCompleted(this, new TopActiveBBO_DWCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/TopActiveBBO_News", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string TopActiveBBO_News()
		{
			object[] array = base.Invoke("TopActiveBBO_News", new object[0]);
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void TopActiveBBO_NewsAsync()
		{
			this.TopActiveBBO_NewsAsync(null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void TopActiveBBO_NewsAsync(object userState)
		{
			if (this.TopActiveBBO_NewsOperationCompleted == null)
			{
				this.TopActiveBBO_NewsOperationCompleted = new SendOrPostCallback(this.OnTopActiveBBO_NewsOperationCompleted);
			}
			base.InvokeAsync("TopActiveBBO_News", new object[0], this.TopActiveBBO_NewsOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnTopActiveBBO_NewsOperationCompleted(object arg)
		{
			if (this._TopActiveBBO_NewsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._TopActiveBBO_NewsCompleted(this, new TopActiveBBO_NewsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/TopActiveBBO_TurnOver", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string TopActiveBBO_TurnOver()
		{
			object[] array = base.Invoke("TopActiveBBO_TurnOver", new object[0]);
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void TopActiveBBO_TurnOverAsync()
		{
			this.TopActiveBBO_TurnOverAsync(null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void TopActiveBBO_TurnOverAsync(object userState)
		{
			if (this.TopActiveBBO_TurnOverOperationCompleted == null)
			{
				this.TopActiveBBO_TurnOverOperationCompleted = new SendOrPostCallback(this.OnTopActiveBBO_TurnOverOperationCompleted);
			}
			base.InvokeAsync("TopActiveBBO_TurnOver", new object[0], this.TopActiveBBO_TurnOverOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnTopActiveBBO_TurnOverOperationCompleted(object arg)
		{
			if (this._TopActiveBBO_TurnOverCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._TopActiveBBO_TurnOverCompleted(this, new TopActiveBBO_TurnOverCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/TopActiveBBO_Sector", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string TopActiveBBO_Sector(int sectorNumber)
		{
			object[] array = base.Invoke("TopActiveBBO_Sector", new object[]
			{
				sectorNumber
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void TopActiveBBO_SectorAsync(int sectorNumber)
		{
			this.TopActiveBBO_SectorAsync(sectorNumber, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void TopActiveBBO_SectorAsync(int sectorNumber, object userState)
		{
			if (this.TopActiveBBO_SectorOperationCompleted == null)
			{
				this.TopActiveBBO_SectorOperationCompleted = new SendOrPostCallback(this.OnTopActiveBBO_SectorOperationCompleted);
			}
			base.InvokeAsync("TopActiveBBO_Sector", new object[]
			{
				sectorNumber
			}, this.TopActiveBBO_SectorOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnTopActiveBBO_SectorOperationCompleted(object arg)
		{
			if (this._TopActiveBBO_SectorCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._TopActiveBBO_SectorCompleted(this, new TopActiveBBO_SectorCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/InvestorType", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string InvestorType(string summaryType)
		{
			object[] array = base.Invoke("InvestorType", new object[]
			{
				summaryType
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void InvestorTypeAsync(string summaryType)
		{
			this.InvestorTypeAsync(summaryType, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void InvestorTypeAsync(string summaryType, object userState)
		{
			if (this.InvestorTypeOperationCompleted == null)
			{
				this.InvestorTypeOperationCompleted = new SendOrPostCallback(this.OnInvestorTypeOperationCompleted);
			}
			base.InvokeAsync("InvestorType", new object[]
			{
				summaryType
			}, this.InvestorTypeOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnInvestorTypeOperationCompleted(object arg)
		{
			if (this._InvestorTypeCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._InvestorTypeCompleted(this, new InvestorTypeCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/MarketIndicator", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string MarketIndicator()
		{
			object[] array = base.Invoke("MarketIndicator", new object[0]);
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void MarketIndicatorAsync()
		{
			this.MarketIndicatorAsync(null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void MarketIndicatorAsync(object userState)
		{
			if (this.MarketIndicatorOperationCompleted == null)
			{
				this.MarketIndicatorOperationCompleted = new SendOrPostCallback(this.OnMarketIndicatorOperationCompleted);
			}
			base.InvokeAsync("MarketIndicator", new object[0], this.MarketIndicatorOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnMarketIndicatorOperationCompleted(object arg)
		{
			if (this._MarketIndicatorCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._MarketIndicatorCompleted(this, new MarketIndicatorCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/SaleByPrice", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string SaleByPrice(int securityNumber, int startRow, int rowPerPage, int maxTicker)
		{
			object[] array = base.Invoke("SaleByPrice", new object[]
			{
				securityNumber,
				startRow,
				rowPerPage,
				maxTicker
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SaleByPriceAsync(int securityNumber, int startRow, int rowPerPage, int maxTicker)
		{
			this.SaleByPriceAsync(securityNumber, startRow, rowPerPage, maxTicker, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SaleByPriceAsync(int securityNumber, int startRow, int rowPerPage, int maxTicker, object userState)
		{
			if (this.SaleByPriceOperationCompleted == null)
			{
				this.SaleByPriceOperationCompleted = new SendOrPostCallback(this.OnSaleByPriceOperationCompleted);
			}
			base.InvokeAsync("SaleByPrice", new object[]
			{
				securityNumber,
				startRow,
				rowPerPage,
				maxTicker
			}, this.SaleByPriceOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnSaleByPriceOperationCompleted(object arg)
		{
			if (this._SaleByPriceCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._SaleByPriceCompleted(this, new SaleByPriceCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/SectorStat", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string SectorStat()
		{
			object[] array = base.Invoke("SectorStat", new object[0]);
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SectorStatAsync()
		{
			this.SectorStatAsync(null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SectorStatAsync(object userState)
		{
			if (this.SectorStatOperationCompleted == null)
			{
				this.SectorStatOperationCompleted = new SendOrPostCallback(this.OnSectorStatOperationCompleted);
			}
			base.InvokeAsync("SectorStat", new object[0], this.SectorStatOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnSectorStatOperationCompleted(object arg)
		{
			if (this._SectorStatCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._SectorStatCompleted(this, new SectorStatCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/SectorStatForDump", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string SectorStatForDump(int industryNumber)
		{
			object[] array = base.Invoke("SectorStatForDump", new object[]
			{
				industryNumber
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SectorStatForDumpAsync(int industryNumber)
		{
			this.SectorStatForDumpAsync(industryNumber, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SectorStatForDumpAsync(int industryNumber, object userState)
		{
			if (this.SectorStatForDumpOperationCompleted == null)
			{
				this.SectorStatForDumpOperationCompleted = new SendOrPostCallback(this.OnSectorStatForDumpOperationCompleted);
			}
			base.InvokeAsync("SectorStatForDump", new object[]
			{
				industryNumber
			}, this.SectorStatForDumpOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnSectorStatForDumpOperationCompleted(object arg)
		{
			if (this._SectorStatForDumpCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._SectorStatForDumpCompleted(this, new SectorStatForDumpCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/StockStatForDump", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string StockStatForDump(int sectorNumber)
		{
			object[] array = base.Invoke("StockStatForDump", new object[]
			{
				sectorNumber
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void StockStatForDumpAsync(int sectorNumber)
		{
			this.StockStatForDumpAsync(sectorNumber, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void StockStatForDumpAsync(int sectorNumber, object userState)
		{
			if (this.StockStatForDumpOperationCompleted == null)
			{
				this.StockStatForDumpOperationCompleted = new SendOrPostCallback(this.OnStockStatForDumpOperationCompleted);
			}
			base.InvokeAsync("StockStatForDump", new object[]
			{
				sectorNumber
			}, this.StockStatForDumpOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnStockStatForDumpOperationCompleted(object arg)
		{
			if (this._StockStatForDumpCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._StockStatForDumpCompleted(this, new StockStatForDumpCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/GetUserConfigForDump", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string GetUserConfigForDump(string userId)
		{
			object[] array = base.Invoke("GetUserConfigForDump", new object[]
			{
				userId
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetUserConfigForDumpAsync(string userId)
		{
			this.GetUserConfigForDumpAsync(userId, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetUserConfigForDumpAsync(string userId, object userState)
		{
			if (this.GetUserConfigForDumpOperationCompleted == null)
			{
				this.GetUserConfigForDumpOperationCompleted = new SendOrPostCallback(this.OnGetUserConfigForDumpOperationCompleted);
			}
			base.InvokeAsync("GetUserConfigForDump", new object[]
			{
				userId
			}, this.GetUserConfigForDumpOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnGetUserConfigForDumpOperationCompleted(object arg)
		{
			if (this._GetUserConfigForDumpCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._GetUserConfigForDumpCompleted(this, new GetUserConfigForDumpCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/SaveUserConfigForDump", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string SaveUserConfigForDump(string userId, string configName, string configValue)
		{
			object[] array = base.Invoke("SaveUserConfigForDump", new object[]
			{
				userId,
				configName,
				configValue
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SaveUserConfigForDumpAsync(string userId, string configName, string configValue)
		{
			this.SaveUserConfigForDumpAsync(userId, configName, configValue, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SaveUserConfigForDumpAsync(string userId, string configName, string configValue, object userState)
		{
			if (this.SaveUserConfigForDumpOperationCompleted == null)
			{
				this.SaveUserConfigForDumpOperationCompleted = new SendOrPostCallback(this.OnSaveUserConfigForDumpOperationCompleted);
			}
			base.InvokeAsync("SaveUserConfigForDump", new object[]
			{
				userId,
				configName,
				configValue
			}, this.SaveUserConfigForDumpOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnSaveUserConfigForDumpOperationCompleted(object arg)
		{
			if (this._SaveUserConfigForDumpCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._SaveUserConfigForDumpCompleted(this, new SaveUserConfigForDumpCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/SaleByTime2", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string SaleByTime2(int securityNumber, int startRow, int rowPerPage, int maxTicker, string startTime)
		{
			object[] array = base.Invoke("SaleByTime2", new object[]
			{
				securityNumber,
				startRow,
				rowPerPage,
				maxTicker,
				startTime
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SaleByTime2Async(int securityNumber, int startRow, int rowPerPage, int maxTicker, string startTime)
		{
			this.SaleByTime2Async(securityNumber, startRow, rowPerPage, maxTicker, startTime, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SaleByTime2Async(int securityNumber, int startRow, int rowPerPage, int maxTicker, string startTime, object userState)
		{
			if (this.SaleByTime2OperationCompleted == null)
			{
				this.SaleByTime2OperationCompleted = new SendOrPostCallback(this.OnSaleByTime2OperationCompleted);
			}
			base.InvokeAsync("SaleByTime2", new object[]
			{
				securityNumber,
				startRow,
				rowPerPage,
				maxTicker,
				startTime
			}, this.SaleByTime2OperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnSaleByTime2OperationCompleted(object arg)
		{
			if (this._SaleByTime2Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._SaleByTime2Completed(this, new SaleByTime2CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/StockInPlay", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string StockInPlay(int SecurityNumber, decimal StartPrice, string Side, int rowPerPage, int maxTicker)
		{
			object[] array = base.Invoke("StockInPlay", new object[]
			{
				SecurityNumber,
				StartPrice,
				Side,
				rowPerPage,
				maxTicker
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void StockInPlayAsync(int SecurityNumber, decimal StartPrice, string Side, int rowPerPage, int maxTicker)
		{
			this.StockInPlayAsync(SecurityNumber, StartPrice, Side, rowPerPage, maxTicker, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void StockInPlayAsync(int SecurityNumber, decimal StartPrice, string Side, int rowPerPage, int maxTicker, object userState)
		{
			if (this.StockInPlayOperationCompleted == null)
			{
				this.StockInPlayOperationCompleted = new SendOrPostCallback(this.OnStockInPlayOperationCompleted);
			}
			base.InvokeAsync("StockInPlay", new object[]
			{
				SecurityNumber,
				StartPrice,
				Side,
				rowPerPage,
				maxTicker
			}, this.StockInPlayOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnStockInPlayOperationCompleted(object arg)
		{
			if (this._StockInPlayCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._StockInPlayCompleted(this, new StockInPlayCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/StockByPricePage", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string StockByPricePage(int stockNumber, int maxTicker)
		{
			object[] array = base.Invoke("StockByPricePage", new object[]
			{
				stockNumber,
				maxTicker
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void StockByPricePageAsync(int stockNumber, int maxTicker)
		{
			this.StockByPricePageAsync(stockNumber, maxTicker, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void StockByPricePageAsync(int stockNumber, int maxTicker, object userState)
		{
			if (this.StockByPricePageOperationCompleted == null)
			{
				this.StockByPricePageOperationCompleted = new SendOrPostCallback(this.OnStockByPricePageOperationCompleted);
			}
			base.InvokeAsync("StockByPricePage", new object[]
			{
				stockNumber,
				maxTicker
			}, this.StockByPricePageOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnStockByPricePageOperationCompleted(object arg)
		{
			if (this._StockByPricePageCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._StockByPricePageCompleted(this, new StockByPricePageCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/MarketStatus", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string MarketStatus()
		{
			object[] array = base.Invoke("MarketStatus", new object[0]);
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void MarketStatusAsync()
		{
			this.MarketStatusAsync(null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void MarketStatusAsync(object userState)
		{
			if (this.MarketStatusOperationCompleted == null)
			{
				this.MarketStatusOperationCompleted = new SendOrPostCallback(this.OnMarketStatusOperationCompleted);
			}
			base.InvokeAsync("MarketStatus", new object[0], this.MarketStatusOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnMarketStatusOperationCompleted(object arg)
		{
			if (this._MarketStatusCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._MarketStatusCompleted(this, new MarketStatusCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/TopBBO", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string TopBBO(int[] stockNumber, int maxTicker)
		{
			object[] array = base.Invoke("TopBBO", new object[]
			{
				stockNumber,
				maxTicker
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void TopBBOAsync(int[] stockNumber, int maxTicker)
		{
			this.TopBBOAsync(stockNumber, maxTicker, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void TopBBOAsync(int[] stockNumber, int maxTicker, object userState)
		{
			if (this.TopBBOOperationCompleted == null)
			{
				this.TopBBOOperationCompleted = new SendOrPostCallback(this.OnTopBBOOperationCompleted);
			}
			base.InvokeAsync("TopBBO", new object[]
			{
				stockNumber,
				maxTicker
			}, this.TopBBOOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnTopBBOOperationCompleted(object arg)
		{
			if (this._TopBBOCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._TopBBOCompleted(this, new TopBBOCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/TopBBOad", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string TopBBOad(string stocks, int maxTicker)
		{
			object[] array = base.Invoke("TopBBOad", new object[]
			{
				stocks,
				maxTicker
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void TopBBOadAsync(string stocks, int maxTicker)
		{
			this.TopBBOadAsync(stocks, maxTicker, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void TopBBOadAsync(string stocks, int maxTicker, object userState)
		{
			if (this.TopBBOadOperationCompleted == null)
			{
				this.TopBBOadOperationCompleted = new SendOrPostCallback(this.OnTopBBOadOperationCompleted);
			}
			base.InvokeAsync("TopBBOad", new object[]
			{
				stocks,
				maxTicker
			}, this.TopBBOadOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnTopBBOadOperationCompleted(object arg)
		{
			if (this._TopBBOadCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._TopBBOadCompleted(this, new TopBBOadCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/ViewOddlot", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string ViewOddlot(int stockNo, int maxTicker)
		{
			object[] array = base.Invoke("ViewOddlot", new object[]
			{
				stockNo,
				maxTicker
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewOddlotAsync(int stockNo, int maxTicker)
		{
			this.ViewOddlotAsync(stockNo, maxTicker, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewOddlotAsync(int stockNo, int maxTicker, object userState)
		{
			if (this.ViewOddlotOperationCompleted == null)
			{
				this.ViewOddlotOperationCompleted = new SendOrPostCallback(this.OnViewOddlotOperationCompleted);
			}
			base.InvokeAsync("ViewOddlot", new object[]
			{
				stockNo,
				maxTicker
			}, this.ViewOddlotOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnViewOddlotOperationCompleted(object arg)
		{
			if (this._ViewOddlotCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._ViewOddlotCompleted(this, new ViewOddlotCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/Get5BidOffer", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Get5BidOffer(int stockNumber)
		{
			object[] array = base.Invoke("Get5BidOffer", new object[]
			{
				stockNumber
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Get5BidOfferAsync(int stockNumber)
		{
			this.Get5BidOfferAsync(stockNumber, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Get5BidOfferAsync(int stockNumber, object userState)
		{
			if (this.Get5BidOfferOperationCompleted == null)
			{
				this.Get5BidOfferOperationCompleted = new SendOrPostCallback(this.OnGet5BidOfferOperationCompleted);
			}
			base.InvokeAsync("Get5BidOffer", new object[]
			{
				stockNumber
			}, this.Get5BidOfferOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnGet5BidOfferOperationCompleted(object arg)
		{
			if (this._Get5BidOfferCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._Get5BidOfferCompleted(this, new Get5BidOfferCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/ViewOrderTransaction", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string ViewOrderTransaction(string id, string senderType, int readType, string account, string stock, string side, string price, string status, long startOrderNo, int rowPerPage, int expression)
		{
			object[] array = base.Invoke("ViewOrderTransaction", new object[]
			{
				id,
				senderType,
				readType,
				account,
				stock,
				side,
				price,
				status,
				startOrderNo,
				rowPerPage,
				expression
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewOrderTransactionAsync(string id, string senderType, int readType, string account, string stock, string side, string price, string status, long startOrderNo, int rowPerPage, int expression)
		{
			this.ViewOrderTransactionAsync(id, senderType, readType, account, stock, side, price, status, startOrderNo, rowPerPage, expression, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewOrderTransactionAsync(string id, string senderType, int readType, string account, string stock, string side, string price, string status, long startOrderNo, int rowPerPage, int expression, object userState)
		{
			if (this.ViewOrderTransactionOperationCompleted == null)
			{
				this.ViewOrderTransactionOperationCompleted = new SendOrPostCallback(this.OnViewOrderTransactionOperationCompleted);
			}
			base.InvokeAsync("ViewOrderTransaction", new object[]
			{
				id,
				senderType,
				readType,
				account,
				stock,
				side,
				price,
				status,
				startOrderNo,
				rowPerPage,
				expression
			}, this.ViewOrderTransactionOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnViewOrderTransactionOperationCompleted(object arg)
		{
			if (this._ViewOrderTransactionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._ViewOrderTransactionCompleted(this, new ViewOrderTransactionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/ViewOrderHistory", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string ViewOrderHistory(string account, string stock, string side, string selDate1, string selDate2, int compareDays)
		{
			object[] array = base.Invoke("ViewOrderHistory", new object[]
			{
				account,
				stock,
				side,
				selDate1,
				selDate2,
				compareDays
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewOrderHistoryAsync(string account, string stock, string side, string selDate1, string selDate2, int compareDays)
		{
			this.ViewOrderHistoryAsync(account, stock, side, selDate1, selDate2, compareDays, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewOrderHistoryAsync(string account, string stock, string side, string selDate1, string selDate2, int compareDays, object userState)
		{
			if (this.ViewOrderHistoryOperationCompleted == null)
			{
				this.ViewOrderHistoryOperationCompleted = new SendOrPostCallback(this.OnViewOrderHistoryOperationCompleted);
			}
			base.InvokeAsync("ViewOrderHistory", new object[]
			{
				account,
				stock,
				side,
				selDate1,
				selDate2,
				compareDays
			}, this.ViewOrderHistoryOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnViewOrderHistoryOperationCompleted(object arg)
		{
			if (this._ViewOrderHistoryCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._ViewOrderHistoryCompleted(this, new ViewOrderHistoryCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/ViewOrdersForDump", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string ViewOrdersForDump(string account, string stock, string side, string price, string status, long startOrderNumber, int recordPerPage, int page)
		{
			object[] array = base.Invoke("ViewOrdersForDump", new object[]
			{
				account,
				stock,
				side,
				price,
				status,
				startOrderNumber,
				recordPerPage,
				page
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewOrdersForDumpAsync(string account, string stock, string side, string price, string status, long startOrderNumber, int recordPerPage, int page)
		{
			this.ViewOrdersForDumpAsync(account, stock, side, price, status, startOrderNumber, recordPerPage, page, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewOrdersForDumpAsync(string account, string stock, string side, string price, string status, long startOrderNumber, int recordPerPage, int page, object userState)
		{
			if (this.ViewOrdersForDumpOperationCompleted == null)
			{
				this.ViewOrdersForDumpOperationCompleted = new SendOrPostCallback(this.OnViewOrdersForDumpOperationCompleted);
			}
			base.InvokeAsync("ViewOrdersForDump", new object[]
			{
				account,
				stock,
				side,
				price,
				status,
				startOrderNumber,
				recordPerPage,
				page
			}, this.ViewOrdersForDumpOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnViewOrdersForDumpOperationCompleted(object arg)
		{
			if (this._ViewOrdersForDumpCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._ViewOrdersForDumpCompleted(this, new ViewOrdersForDumpCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/ViewOrderByOrderNo", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string ViewOrderByOrderNo(string account, string orderNoList)
		{
			object[] array = base.Invoke("ViewOrderByOrderNo", new object[]
			{
				account,
				orderNoList
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewOrderByOrderNoAsync(string account, string orderNoList)
		{
			this.ViewOrderByOrderNoAsync(account, orderNoList, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewOrderByOrderNoAsync(string account, string orderNoList, object userState)
		{
			if (this.ViewOrderByOrderNoOperationCompleted == null)
			{
				this.ViewOrderByOrderNoOperationCompleted = new SendOrPostCallback(this.OnViewOrderByOrderNoOperationCompleted);
			}
			base.InvokeAsync("ViewOrderByOrderNo", new object[]
			{
				account,
				orderNoList
			}, this.ViewOrderByOrderNoOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnViewOrderByOrderNoOperationCompleted(object arg)
		{
			if (this._ViewOrderByOrderNoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._ViewOrderByOrderNoCompleted(this, new ViewOrderByOrderNoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/ViewNewsHeader", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string ViewNewsHeader(bool isCurrentDate, string selectDate, string selectStock, int recordPerPage, int page)
		{
			object[] array = base.Invoke("ViewNewsHeader", new object[]
			{
				isCurrentDate,
				selectDate,
				selectStock,
				recordPerPage,
				page
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewNewsHeaderAsync(bool isCurrentDate, string selectDate, string selectStock, int recordPerPage, int page)
		{
			this.ViewNewsHeaderAsync(isCurrentDate, selectDate, selectStock, recordPerPage, page, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewNewsHeaderAsync(bool isCurrentDate, string selectDate, string selectStock, int recordPerPage, int page, object userState)
		{
			if (this.ViewNewsHeaderOperationCompleted == null)
			{
				this.ViewNewsHeaderOperationCompleted = new SendOrPostCallback(this.OnViewNewsHeaderOperationCompleted);
			}
			base.InvokeAsync("ViewNewsHeader", new object[]
			{
				isCurrentDate,
				selectDate,
				selectStock,
				recordPerPage,
				page
			}, this.ViewNewsHeaderOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnViewNewsHeaderOperationCompleted(object arg)
		{
			if (this._ViewNewsHeaderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._ViewNewsHeaderCompleted(this, new ViewNewsHeaderCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/ViewNewsStory", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string ViewNewsStory(bool isCurrentDate, string selectDate, long newNo)
		{
			object[] array = base.Invoke("ViewNewsStory", new object[]
			{
				isCurrentDate,
				selectDate,
				newNo
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewNewsStoryAsync(bool isCurrentDate, string selectDate, long newNo)
		{
			this.ViewNewsStoryAsync(isCurrentDate, selectDate, newNo, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewNewsStoryAsync(bool isCurrentDate, string selectDate, long newNo, object userState)
		{
			if (this.ViewNewsStoryOperationCompleted == null)
			{
				this.ViewNewsStoryOperationCompleted = new SendOrPostCallback(this.OnViewNewsStoryOperationCompleted);
			}
			base.InvokeAsync("ViewNewsStory", new object[]
			{
				isCurrentDate,
				selectDate,
				newNo
			}, this.ViewNewsStoryOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnViewNewsStoryOperationCompleted(object arg)
		{
			if (this._ViewNewsStoryCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._ViewNewsStoryCompleted(this, new ViewNewsStoryCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/ViewCustomersInfo", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string ViewCustomersInfo(string custAccID, string userLoginID)
		{
			object[] array = base.Invoke("ViewCustomersInfo", new object[]
			{
				custAccID,
				userLoginID
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewCustomersInfoAsync(string custAccID, string userLoginID)
		{
			this.ViewCustomersInfoAsync(custAccID, userLoginID, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewCustomersInfoAsync(string custAccID, string userLoginID, object userState)
		{
			if (this.ViewCustomersInfoOperationCompleted == null)
			{
				this.ViewCustomersInfoOperationCompleted = new SendOrPostCallback(this.OnViewCustomersInfoOperationCompleted);
			}
			base.InvokeAsync("ViewCustomersInfo", new object[]
			{
				custAccID,
				userLoginID
			}, this.ViewCustomersInfoOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnViewCustomersInfoOperationCompleted(object arg)
		{
			if (this._ViewCustomersInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._ViewCustomersInfoCompleted(this, new ViewCustomersInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/ViewCustomerCreditOnSendBox_Freewill", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string ViewCustomerCreditOnSendBox_Freewill(string account, string accType, string stock)
		{
			object[] array = base.Invoke("ViewCustomerCreditOnSendBox_Freewill", new object[]
			{
				account,
				accType,
				stock
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewCustomerCreditOnSendBox_FreewillAsync(string account, string accType, string stock)
		{
			this.ViewCustomerCreditOnSendBox_FreewillAsync(account, accType, stock, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewCustomerCreditOnSendBox_FreewillAsync(string account, string accType, string stock, object userState)
		{
			if (this.ViewCustomerCreditOnSendBox_FreewillOperationCompleted == null)
			{
				this.ViewCustomerCreditOnSendBox_FreewillOperationCompleted = new SendOrPostCallback(this.OnViewCustomerCreditOnSendBox_FreewillOperationCompleted);
			}
			base.InvokeAsync("ViewCustomerCreditOnSendBox_Freewill", new object[]
			{
				account,
				accType,
				stock
			}, this.ViewCustomerCreditOnSendBox_FreewillOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnViewCustomerCreditOnSendBox_FreewillOperationCompleted(object arg)
		{
			if (this._ViewCustomerCreditOnSendBox_FreewillCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._ViewCustomerCreditOnSendBox_FreewillCompleted(this, new ViewCustomerCreditOnSendBox_FreewillCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/ViewCustomerCreditOnSendBox", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string ViewCustomerCreditOnSendBox(string account, string stock)
		{
			object[] array = base.Invoke("ViewCustomerCreditOnSendBox", new object[]
			{
				account,
				stock
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewCustomerCreditOnSendBoxAsync(string account, string stock)
		{
			this.ViewCustomerCreditOnSendBoxAsync(account, stock, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewCustomerCreditOnSendBoxAsync(string account, string stock, object userState)
		{
			if (this.ViewCustomerCreditOnSendBoxOperationCompleted == null)
			{
				this.ViewCustomerCreditOnSendBoxOperationCompleted = new SendOrPostCallback(this.OnViewCustomerCreditOnSendBoxOperationCompleted);
			}
			base.InvokeAsync("ViewCustomerCreditOnSendBox", new object[]
			{
				account,
				stock
			}, this.ViewCustomerCreditOnSendBoxOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnViewCustomerCreditOnSendBoxOperationCompleted(object arg)
		{
			if (this._ViewCustomerCreditOnSendBoxCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._ViewCustomerCreditOnSendBoxCompleted(this, new ViewCustomerCreditOnSendBoxCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/GetSwitchAccountInfo", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string GetSwitchAccountInfo(string account)
		{
			object[] array = base.Invoke("GetSwitchAccountInfo", new object[]
			{
				account
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetSwitchAccountInfoAsync(string account)
		{
			this.GetSwitchAccountInfoAsync(account, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetSwitchAccountInfoAsync(string account, object userState)
		{
			if (this.GetSwitchAccountInfoOperationCompleted == null)
			{
				this.GetSwitchAccountInfoOperationCompleted = new SendOrPostCallback(this.OnGetSwitchAccountInfoOperationCompleted);
			}
			base.InvokeAsync("GetSwitchAccountInfo", new object[]
			{
				account
			}, this.GetSwitchAccountInfoOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnGetSwitchAccountInfoOperationCompleted(object arg)
		{
			if (this._GetSwitchAccountInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._GetSwitchAccountInfoCompleted(this, new GetSwitchAccountInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/GetSwitchAccountInfoEservice", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string GetSwitchAccountInfoEservice(string brokerID, string userId, string session, string accountNo, string system, string service)
		{
			object[] array = base.Invoke("GetSwitchAccountInfoEservice", new object[]
			{
				brokerID,
				userId,
				session,
				accountNo,
				system,
				service
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetSwitchAccountInfoEserviceAsync(string brokerID, string userId, string session, string accountNo, string system, string service)
		{
			this.GetSwitchAccountInfoEserviceAsync(brokerID, userId, session, accountNo, system, service, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetSwitchAccountInfoEserviceAsync(string brokerID, string userId, string session, string accountNo, string system, string service, object userState)
		{
			if (this.GetSwitchAccountInfoEserviceOperationCompleted == null)
			{
				this.GetSwitchAccountInfoEserviceOperationCompleted = new SendOrPostCallback(this.OnGetSwitchAccountInfoEserviceOperationCompleted);
			}
			base.InvokeAsync("GetSwitchAccountInfoEservice", new object[]
			{
				brokerID,
				userId,
				session,
				accountNo,
				system,
				service
			}, this.GetSwitchAccountInfoEserviceOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnGetSwitchAccountInfoEserviceOperationCompleted(object arg)
		{
			if (this._GetSwitchAccountInfoEserviceCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._GetSwitchAccountInfoEserviceCompleted(this, new GetSwitchAccountInfoEserviceCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/ViewCustomer_MobileReportAll", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string ViewCustomer_MobileReportAll(string custAccID, string stockSymbol)
		{
			object[] array = base.Invoke("ViewCustomer_MobileReportAll", new object[]
			{
				custAccID,
				stockSymbol
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewCustomer_MobileReportAllAsync(string custAccID, string stockSymbol)
		{
			this.ViewCustomer_MobileReportAllAsync(custAccID, stockSymbol, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewCustomer_MobileReportAllAsync(string custAccID, string stockSymbol, object userState)
		{
			if (this.ViewCustomer_MobileReportAllOperationCompleted == null)
			{
				this.ViewCustomer_MobileReportAllOperationCompleted = new SendOrPostCallback(this.OnViewCustomer_MobileReportAllOperationCompleted);
			}
			base.InvokeAsync("ViewCustomer_MobileReportAll", new object[]
			{
				custAccID,
				stockSymbol
			}, this.ViewCustomer_MobileReportAllOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnViewCustomer_MobileReportAllOperationCompleted(object arg)
		{
			if (this._ViewCustomer_MobileReportAllCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._ViewCustomer_MobileReportAllCompleted(this, new ViewCustomer_MobileReportAllCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/ViewCustomer_OrdersConfirms", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string ViewCustomer_OrdersConfirms(string custAccID, string stockSymbol, int startRow, int rowPerPage)
		{
			object[] array = base.Invoke("ViewCustomer_OrdersConfirms", new object[]
			{
				custAccID,
				stockSymbol,
				startRow,
				rowPerPage
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewCustomer_OrdersConfirmsAsync(string custAccID, string stockSymbol, int startRow, int rowPerPage)
		{
			this.ViewCustomer_OrdersConfirmsAsync(custAccID, stockSymbol, startRow, rowPerPage, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewCustomer_OrdersConfirmsAsync(string custAccID, string stockSymbol, int startRow, int rowPerPage, object userState)
		{
			if (this.ViewCustomer_OrdersConfirmsOperationCompleted == null)
			{
				this.ViewCustomer_OrdersConfirmsOperationCompleted = new SendOrPostCallback(this.OnViewCustomer_OrdersConfirmsOperationCompleted);
			}
			base.InvokeAsync("ViewCustomer_OrdersConfirms", new object[]
			{
				custAccID,
				stockSymbol,
				startRow,
				rowPerPage
			}, this.ViewCustomer_OrdersConfirmsOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnViewCustomer_OrdersConfirmsOperationCompleted(object arg)
		{
			if (this._ViewCustomer_OrdersConfirmsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._ViewCustomer_OrdersConfirmsCompleted(this, new ViewCustomer_OrdersConfirmsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/ViewCustomer_CreditPosition", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string ViewCustomer_CreditPosition(string custAccID, string stockSymbol, int startRow, int rowPerPage)
		{
			object[] array = base.Invoke("ViewCustomer_CreditPosition", new object[]
			{
				custAccID,
				stockSymbol,
				startRow,
				rowPerPage
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewCustomer_CreditPositionAsync(string custAccID, string stockSymbol, int startRow, int rowPerPage)
		{
			this.ViewCustomer_CreditPositionAsync(custAccID, stockSymbol, startRow, rowPerPage, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewCustomer_CreditPositionAsync(string custAccID, string stockSymbol, int startRow, int rowPerPage, object userState)
		{
			if (this.ViewCustomer_CreditPositionOperationCompleted == null)
			{
				this.ViewCustomer_CreditPositionOperationCompleted = new SendOrPostCallback(this.OnViewCustomer_CreditPositionOperationCompleted);
			}
			base.InvokeAsync("ViewCustomer_CreditPosition", new object[]
			{
				custAccID,
				stockSymbol,
				startRow,
				rowPerPage
			}, this.ViewCustomer_CreditPositionOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnViewCustomer_CreditPositionOperationCompleted(object arg)
		{
			if (this._ViewCustomer_CreditPositionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._ViewCustomer_CreditPositionCompleted(this, new ViewCustomer_CreditPositionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/ViewCustomer_ProjectedProfitLoss", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string ViewCustomer_ProjectedProfitLoss(string custAccID, string stockSymbol, int startRow, int rowPerPage)
		{
			object[] array = base.Invoke("ViewCustomer_ProjectedProfitLoss", new object[]
			{
				custAccID,
				stockSymbol,
				startRow,
				rowPerPage
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewCustomer_ProjectedProfitLossAsync(string custAccID, string stockSymbol, int startRow, int rowPerPage)
		{
			this.ViewCustomer_ProjectedProfitLossAsync(custAccID, stockSymbol, startRow, rowPerPage, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewCustomer_ProjectedProfitLossAsync(string custAccID, string stockSymbol, int startRow, int rowPerPage, object userState)
		{
			if (this.ViewCustomer_ProjectedProfitLossOperationCompleted == null)
			{
				this.ViewCustomer_ProjectedProfitLossOperationCompleted = new SendOrPostCallback(this.OnViewCustomer_ProjectedProfitLossOperationCompleted);
			}
			base.InvokeAsync("ViewCustomer_ProjectedProfitLoss", new object[]
			{
				custAccID,
				stockSymbol,
				startRow,
				rowPerPage
			}, this.ViewCustomer_ProjectedProfitLossOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnViewCustomer_ProjectedProfitLossOperationCompleted(object arg)
		{
			if (this._ViewCustomer_ProjectedProfitLossCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._ViewCustomer_ProjectedProfitLossCompleted(this, new ViewCustomer_ProjectedProfitLossCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/ViewCustomer_RealizeProfitLoss", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string ViewCustomer_RealizeProfitLoss(string custAccID, string stockSymbol, int startRow, int rowPerPage)
		{
			object[] array = base.Invoke("ViewCustomer_RealizeProfitLoss", new object[]
			{
				custAccID,
				stockSymbol,
				startRow,
				rowPerPage
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewCustomer_RealizeProfitLossAsync(string custAccID, string stockSymbol, int startRow, int rowPerPage)
		{
			this.ViewCustomer_RealizeProfitLossAsync(custAccID, stockSymbol, startRow, rowPerPage, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewCustomer_RealizeProfitLossAsync(string custAccID, string stockSymbol, int startRow, int rowPerPage, object userState)
		{
			if (this.ViewCustomer_RealizeProfitLossOperationCompleted == null)
			{
				this.ViewCustomer_RealizeProfitLossOperationCompleted = new SendOrPostCallback(this.OnViewCustomer_RealizeProfitLossOperationCompleted);
			}
			base.InvokeAsync("ViewCustomer_RealizeProfitLoss", new object[]
			{
				custAccID,
				stockSymbol,
				startRow,
				rowPerPage
			}, this.ViewCustomer_RealizeProfitLossOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnViewCustomer_RealizeProfitLossOperationCompleted(object arg)
		{
			if (this._ViewCustomer_RealizeProfitLossCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._ViewCustomer_RealizeProfitLossCompleted(this, new ViewCustomer_RealizeProfitLossCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/ViewCustomer_Summary", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string ViewCustomer_Summary(string custAccID, string stockSymbol, int startRow, int rowPerPage)
		{
			object[] array = base.Invoke("ViewCustomer_Summary", new object[]
			{
				custAccID,
				stockSymbol,
				startRow,
				rowPerPage
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewCustomer_SummaryAsync(string custAccID, string stockSymbol, int startRow, int rowPerPage)
		{
			this.ViewCustomer_SummaryAsync(custAccID, stockSymbol, startRow, rowPerPage, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewCustomer_SummaryAsync(string custAccID, string stockSymbol, int startRow, int rowPerPage, object userState)
		{
			if (this.ViewCustomer_SummaryOperationCompleted == null)
			{
				this.ViewCustomer_SummaryOperationCompleted = new SendOrPostCallback(this.OnViewCustomer_SummaryOperationCompleted);
			}
			base.InvokeAsync("ViewCustomer_Summary", new object[]
			{
				custAccID,
				stockSymbol,
				startRow,
				rowPerPage
			}, this.ViewCustomer_SummaryOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnViewCustomer_SummaryOperationCompleted(object arg)
		{
			if (this._ViewCustomer_SummaryCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._ViewCustomer_SummaryCompleted(this, new ViewCustomer_SummaryCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/ViewCustomer_ConfirmSummary", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string ViewCustomer_ConfirmSummary(string custAccID, int commGroup, string stockSymbol, int startRow, int rowPerPage)
		{
			object[] array = base.Invoke("ViewCustomer_ConfirmSummary", new object[]
			{
				custAccID,
				commGroup,
				stockSymbol,
				startRow,
				rowPerPage
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewCustomer_ConfirmSummaryAsync(string custAccID, int commGroup, string stockSymbol, int startRow, int rowPerPage)
		{
			this.ViewCustomer_ConfirmSummaryAsync(custAccID, commGroup, stockSymbol, startRow, rowPerPage, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewCustomer_ConfirmSummaryAsync(string custAccID, int commGroup, string stockSymbol, int startRow, int rowPerPage, object userState)
		{
			if (this.ViewCustomer_ConfirmSummaryOperationCompleted == null)
			{
				this.ViewCustomer_ConfirmSummaryOperationCompleted = new SendOrPostCallback(this.OnViewCustomer_ConfirmSummaryOperationCompleted);
			}
			base.InvokeAsync("ViewCustomer_ConfirmSummary", new object[]
			{
				custAccID,
				commGroup,
				stockSymbol,
				startRow,
				rowPerPage
			}, this.ViewCustomer_ConfirmSummaryOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnViewCustomer_ConfirmSummaryOperationCompleted(object arg)
		{
			if (this._ViewCustomer_ConfirmSummaryCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._ViewCustomer_ConfirmSummaryCompleted(this, new ViewCustomer_ConfirmSummaryCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/ViewCustomer_Confirm", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string ViewCustomer_Confirm(string custAccID, int commGroup, string stockSymbol, int startRow, int rowPerPage)
		{
			object[] array = base.Invoke("ViewCustomer_Confirm", new object[]
			{
				custAccID,
				commGroup,
				stockSymbol,
				startRow,
				rowPerPage
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewCustomer_ConfirmAsync(string custAccID, int commGroup, string stockSymbol, int startRow, int rowPerPage)
		{
			this.ViewCustomer_ConfirmAsync(custAccID, commGroup, stockSymbol, startRow, rowPerPage, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewCustomer_ConfirmAsync(string custAccID, int commGroup, string stockSymbol, int startRow, int rowPerPage, object userState)
		{
			if (this.ViewCustomer_ConfirmOperationCompleted == null)
			{
				this.ViewCustomer_ConfirmOperationCompleted = new SendOrPostCallback(this.OnViewCustomer_ConfirmOperationCompleted);
			}
			base.InvokeAsync("ViewCustomer_Confirm", new object[]
			{
				custAccID,
				commGroup,
				stockSymbol,
				startRow,
				rowPerPage
			}, this.ViewCustomer_ConfirmOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnViewCustomer_ConfirmOperationCompleted(object arg)
		{
			if (this._ViewCustomer_ConfirmCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._ViewCustomer_ConfirmCompleted(this, new ViewCustomer_ConfirmCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/ViewCustomer_ConfirmByDealID", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string ViewCustomer_ConfirmByDealID(string custAccID, int commGroup, string stockSymbol, int startRow, int rowPerPage)
		{
			object[] array = base.Invoke("ViewCustomer_ConfirmByDealID", new object[]
			{
				custAccID,
				commGroup,
				stockSymbol,
				startRow,
				rowPerPage
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewCustomer_ConfirmByDealIDAsync(string custAccID, int commGroup, string stockSymbol, int startRow, int rowPerPage)
		{
			this.ViewCustomer_ConfirmByDealIDAsync(custAccID, commGroup, stockSymbol, startRow, rowPerPage, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewCustomer_ConfirmByDealIDAsync(string custAccID, int commGroup, string stockSymbol, int startRow, int rowPerPage, object userState)
		{
			if (this.ViewCustomer_ConfirmByDealIDOperationCompleted == null)
			{
				this.ViewCustomer_ConfirmByDealIDOperationCompleted = new SendOrPostCallback(this.OnViewCustomer_ConfirmByDealIDOperationCompleted);
			}
			base.InvokeAsync("ViewCustomer_ConfirmByDealID", new object[]
			{
				custAccID,
				commGroup,
				stockSymbol,
				startRow,
				rowPerPage
			}, this.ViewCustomer_ConfirmByDealIDOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnViewCustomer_ConfirmByDealIDOperationCompleted(object arg)
		{
			if (this._ViewCustomer_ConfirmByDealIDCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._ViewCustomer_ConfirmByDealIDCompleted(this, new ViewCustomer_ConfirmByDealIDCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/ViewCustomer_ConfirmByStock", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string ViewCustomer_ConfirmByStock(string custAccID, int commGroup, string stockSymbol, int startRow, int rowPerPage)
		{
			object[] array = base.Invoke("ViewCustomer_ConfirmByStock", new object[]
			{
				custAccID,
				commGroup,
				stockSymbol,
				startRow,
				rowPerPage
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewCustomer_ConfirmByStockAsync(string custAccID, int commGroup, string stockSymbol, int startRow, int rowPerPage)
		{
			this.ViewCustomer_ConfirmByStockAsync(custAccID, commGroup, stockSymbol, startRow, rowPerPage, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewCustomer_ConfirmByStockAsync(string custAccID, int commGroup, string stockSymbol, int startRow, int rowPerPage, object userState)
		{
			if (this.ViewCustomer_ConfirmByStockOperationCompleted == null)
			{
				this.ViewCustomer_ConfirmByStockOperationCompleted = new SendOrPostCallback(this.OnViewCustomer_ConfirmByStockOperationCompleted);
			}
			base.InvokeAsync("ViewCustomer_ConfirmByStock", new object[]
			{
				custAccID,
				commGroup,
				stockSymbol,
				startRow,
				rowPerPage
			}, this.ViewCustomer_ConfirmByStockOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnViewCustomer_ConfirmByStockOperationCompleted(object arg)
		{
			if (this._ViewCustomer_ConfirmByStockCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._ViewCustomer_ConfirmByStockCompleted(this, new ViewCustomer_ConfirmByStockCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/ViewOrderInfo_AfterCloseFw", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string ViewOrderInfo_AfterCloseFw(long orderNumber)
		{
			object[] array = base.Invoke("ViewOrderInfo_AfterCloseFw", new object[]
			{
				orderNumber
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewOrderInfo_AfterCloseFwAsync(long orderNumber)
		{
			this.ViewOrderInfo_AfterCloseFwAsync(orderNumber, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewOrderInfo_AfterCloseFwAsync(long orderNumber, object userState)
		{
			if (this.ViewOrderInfo_AfterCloseFwOperationCompleted == null)
			{
				this.ViewOrderInfo_AfterCloseFwOperationCompleted = new SendOrPostCallback(this.OnViewOrderInfo_AfterCloseFwOperationCompleted);
			}
			base.InvokeAsync("ViewOrderInfo_AfterCloseFw", new object[]
			{
				orderNumber
			}, this.ViewOrderInfo_AfterCloseFwOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnViewOrderInfo_AfterCloseFwOperationCompleted(object arg)
		{
			if (this._ViewOrderInfo_AfterCloseFwCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._ViewOrderInfo_AfterCloseFwCompleted(this, new ViewOrderInfo_AfterCloseFwCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/ViewOrderDealData", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string ViewOrderDealData(string sSendDate, long orderNumber, int startRow, int rowPerPage, int dbType)
		{
			object[] array = base.Invoke("ViewOrderDealData", new object[]
			{
				sSendDate,
				orderNumber,
				startRow,
				rowPerPage,
				dbType
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewOrderDealDataAsync(string sSendDate, long orderNumber, int startRow, int rowPerPage, int dbType)
		{
			this.ViewOrderDealDataAsync(sSendDate, orderNumber, startRow, rowPerPage, dbType, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewOrderDealDataAsync(string sSendDate, long orderNumber, int startRow, int rowPerPage, int dbType, object userState)
		{
			if (this.ViewOrderDealDataOperationCompleted == null)
			{
				this.ViewOrderDealDataOperationCompleted = new SendOrPostCallback(this.OnViewOrderDealDataOperationCompleted);
			}
			base.InvokeAsync("ViewOrderDealData", new object[]
			{
				sSendDate,
				orderNumber,
				startRow,
				rowPerPage,
				dbType
			}, this.ViewOrderDealDataOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnViewOrderDealDataOperationCompleted(object arg)
		{
			if (this._ViewOrderDealDataCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._ViewOrderDealDataCompleted(this, new ViewOrderDealDataCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/ViewOrderDealDataHistory", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string ViewOrderDealDataHistory(string sSendDate, long orderNumber, int startRow, int rowPerPage)
		{
			object[] array = base.Invoke("ViewOrderDealDataHistory", new object[]
			{
				sSendDate,
				orderNumber,
				startRow,
				rowPerPage
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewOrderDealDataHistoryAsync(string sSendDate, long orderNumber, int startRow, int rowPerPage)
		{
			this.ViewOrderDealDataHistoryAsync(sSendDate, orderNumber, startRow, rowPerPage, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewOrderDealDataHistoryAsync(string sSendDate, long orderNumber, int startRow, int rowPerPage, object userState)
		{
			if (this.ViewOrderDealDataHistoryOperationCompleted == null)
			{
				this.ViewOrderDealDataHistoryOperationCompleted = new SendOrPostCallback(this.OnViewOrderDealDataHistoryOperationCompleted);
			}
			base.InvokeAsync("ViewOrderDealDataHistory", new object[]
			{
				sSendDate,
				orderNumber,
				startRow,
				rowPerPage
			}, this.ViewOrderDealDataHistoryOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnViewOrderDealDataHistoryOperationCompleted(object arg)
		{
			if (this._ViewOrderDealDataHistoryCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._ViewOrderDealDataHistoryCompleted(this, new ViewOrderDealDataHistoryCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/GetCometInfo", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string GetCometInfo()
		{
			object[] array = base.Invoke("GetCometInfo", new object[0]);
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetCometInfoAsync()
		{
			this.GetCometInfoAsync(null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetCometInfoAsync(object userState)
		{
			if (this.GetCometInfoOperationCompleted == null)
			{
				this.GetCometInfoOperationCompleted = new SendOrPostCallback(this.OnGetCometInfoOperationCompleted);
			}
			base.InvokeAsync("GetCometInfo", new object[0], this.GetCometInfoOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnGetCometInfoOperationCompleted(object arg)
		{
			if (this._GetCometInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._GetCometInfoCompleted(this, new GetCometInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/GetTunnelConfig", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string GetTunnelConfig()
		{
			object[] array = base.Invoke("GetTunnelConfig", new object[0]);
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetTunnelConfigAsync()
		{
			this.GetTunnelConfigAsync(null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetTunnelConfigAsync(object userState)
		{
			if (this.GetTunnelConfigOperationCompleted == null)
			{
				this.GetTunnelConfigOperationCompleted = new SendOrPostCallback(this.OnGetTunnelConfigOperationCompleted);
			}
			base.InvokeAsync("GetTunnelConfig", new object[0], this.GetTunnelConfigOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnGetTunnelConfigOperationCompleted(object arg)
		{
			if (this._GetTunnelConfigCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._GetTunnelConfigCompleted(this, new GetTunnelConfigCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/GetTunnel", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string GetTunnel(bool isRequestTfex)
		{
			object[] array = base.Invoke("GetTunnel", new object[]
			{
				isRequestTfex
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetTunnelAsync(bool isRequestTfex)
		{
			this.GetTunnelAsync(isRequestTfex, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetTunnelAsync(bool isRequestTfex, object userState)
		{
			if (this.GetTunnelOperationCompleted == null)
			{
				this.GetTunnelOperationCompleted = new SendOrPostCallback(this.OnGetTunnelOperationCompleted);
			}
			base.InvokeAsync("GetTunnel", new object[]
			{
				isRequestTfex
			}, this.GetTunnelOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnGetTunnelOperationCompleted(object arg)
		{
			if (this._GetTunnelCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._GetTunnelCompleted(this, new GetTunnelCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/VerifyOrder", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string VerifyOrder(string stock, string side, long volume, string price, string account, bool isValidatePolicy)
		{
			object[] array = base.Invoke("VerifyOrder", new object[]
			{
				stock,
				side,
				volume,
				price,
				account,
				isValidatePolicy
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void VerifyOrderAsync(string stock, string side, long volume, string price, string account, bool isValidatePolicy)
		{
			this.VerifyOrderAsync(stock, side, volume, price, account, isValidatePolicy, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void VerifyOrderAsync(string stock, string side, long volume, string price, string account, bool isValidatePolicy, object userState)
		{
			if (this.VerifyOrderOperationCompleted == null)
			{
				this.VerifyOrderOperationCompleted = new SendOrPostCallback(this.OnVerifyOrderOperationCompleted);
			}
			base.InvokeAsync("VerifyOrder", new object[]
			{
				stock,
				side,
				volume,
				price,
				account,
				isValidatePolicy
			}, this.VerifyOrderOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnVerifyOrderOperationCompleted(object arg)
		{
			if (this._VerifyOrderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._VerifyOrderCompleted(this, new VerifyOrderCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/VerifyOrderFw", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string VerifyOrderFw(string stock, string side, long volume, string price, long pubVol, string condition)
		{
			object[] array = base.Invoke("VerifyOrderFw", new object[]
			{
				stock,
				side,
				volume,
				price,
				pubVol,
				condition
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void VerifyOrderFwAsync(string stock, string side, long volume, string price, long pubVol, string condition)
		{
			this.VerifyOrderFwAsync(stock, side, volume, price, pubVol, condition, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void VerifyOrderFwAsync(string stock, string side, long volume, string price, long pubVol, string condition, object userState)
		{
			if (this.VerifyOrderFwOperationCompleted == null)
			{
				this.VerifyOrderFwOperationCompleted = new SendOrPostCallback(this.OnVerifyOrderFwOperationCompleted);
			}
			base.InvokeAsync("VerifyOrderFw", new object[]
			{
				stock,
				side,
				volume,
				price,
				pubVol,
				condition
			}, this.VerifyOrderFwOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnVerifyOrderFwOperationCompleted(object arg)
		{
			if (this._VerifyOrderFwCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._VerifyOrderFwCompleted(this, new VerifyOrderFwCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/VerifyOrderMkt", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string VerifyOrderMkt()
		{
			object[] array = base.Invoke("VerifyOrderMkt", new object[0]);
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void VerifyOrderMktAsync()
		{
			this.VerifyOrderMktAsync(null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void VerifyOrderMktAsync(object userState)
		{
			if (this.VerifyOrderMktOperationCompleted == null)
			{
				this.VerifyOrderMktOperationCompleted = new SendOrPostCallback(this.OnVerifyOrderMktOperationCompleted);
			}
			base.InvokeAsync("VerifyOrderMkt", new object[0], this.VerifyOrderMktOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnVerifyOrderMktOperationCompleted(object arg)
		{
			if (this._VerifyOrderMktCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._VerifyOrderMktCompleted(this, new VerifyOrderMktCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/GetMainInfo", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string GetMainInfo(string userId, string userKey, string localIp)
		{
			object[] array = base.Invoke("GetMainInfo", new object[]
			{
				userId,
				userKey,
				localIp
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetMainInfoAsync(string userId, string userKey, string localIp)
		{
			this.GetMainInfoAsync(userId, userKey, localIp, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetMainInfoAsync(string userId, string userKey, string localIp, object userState)
		{
			if (this.GetMainInfoOperationCompleted == null)
			{
				this.GetMainInfoOperationCompleted = new SendOrPostCallback(this.OnGetMainInfoOperationCompleted);
			}
			base.InvokeAsync("GetMainInfo", new object[]
			{
				userId,
				userKey,
				localIp
			}, this.GetMainInfoOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnGetMainInfoOperationCompleted(object arg)
		{
			if (this._GetMainInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._GetMainInfoCompleted(this, new GetMainInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/UserAuthen", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string UserAuthen(string userId, string password, string localIp, string device, string versionCode, bool isGetMainInfo)
		{
			object[] array = base.Invoke("UserAuthen", new object[]
			{
				userId,
				password,
				localIp,
				device,
				versionCode,
				isGetMainInfo
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void UserAuthenAsync(string userId, string password, string localIp, string device, string versionCode, bool isGetMainInfo)
		{
			this.UserAuthenAsync(userId, password, localIp, device, versionCode, isGetMainInfo, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void UserAuthenAsync(string userId, string password, string localIp, string device, string versionCode, bool isGetMainInfo, object userState)
		{
			if (this.UserAuthenOperationCompleted == null)
			{
				this.UserAuthenOperationCompleted = new SendOrPostCallback(this.OnUserAuthenOperationCompleted);
			}
			base.InvokeAsync("UserAuthen", new object[]
			{
				userId,
				password,
				localIp,
				device,
				versionCode,
				isGetMainInfo
			}, this.UserAuthenOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnUserAuthenOperationCompleted(object arg)
		{
			if (this._UserAuthenCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._UserAuthenCompleted(this, new UserAuthenCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/ClearEfinSession", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool ClearEfinSession(string userefin)
		{
			object[] array = base.Invoke("ClearEfinSession", new object[]
			{
				userefin
			});
			return (bool)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ClearEfinSessionAsync(string userefin)
		{
			this.ClearEfinSessionAsync(userefin, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ClearEfinSessionAsync(string userefin, object userState)
		{
			if (this.ClearEfinSessionOperationCompleted == null)
			{
				this.ClearEfinSessionOperationCompleted = new SendOrPostCallback(this.OnClearEfinSessionOperationCompleted);
			}
			base.InvokeAsync("ClearEfinSession", new object[]
			{
				userefin
			}, this.ClearEfinSessionOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnClearEfinSessionOperationCompleted(object arg)
		{
			if (this._ClearEfinSessionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._ClearEfinSessionCompleted(this, new ClearEfinSessionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/GetUrlClient", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string GetUrlClient()
		{
			object[] array = base.Invoke("GetUrlClient", new object[0]);
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetUrlClientAsync()
		{
			this.GetUrlClientAsync(null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetUrlClientAsync(object userState)
		{
			if (this.GetUrlClientOperationCompleted == null)
			{
				this.GetUrlClientOperationCompleted = new SendOrPostCallback(this.OnGetUrlClientOperationCompleted);
			}
			base.InvokeAsync("GetUrlClient", new object[0], this.GetUrlClientOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnGetUrlClientOperationCompleted(object arg)
		{
			if (this._GetUrlClientCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._GetUrlClientCompleted(this, new GetUrlClientCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/Logout", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Logout(int sessionID, string loginMode, string userLoginID)
		{
			base.Invoke("Logout", new object[]
			{
				sessionID,
				loginMode,
				userLoginID
			});
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void LogoutAsync(int sessionID, string loginMode, string userLoginID)
		{
			this.LogoutAsync(sessionID, loginMode, userLoginID, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void LogoutAsync(int sessionID, string loginMode, string userLoginID, object userState)
		{
			if (this.LogoutOperationCompleted == null)
			{
				this.LogoutOperationCompleted = new SendOrPostCallback(this.OnLogoutOperationCompleted);
			}
			base.InvokeAsync("Logout", new object[]
			{
				sessionID,
				loginMode,
				userLoginID
			}, this.LogoutOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnLogoutOperationCompleted(object arg)
		{
			if (this._LogoutCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._LogoutCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/LogoutAD", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void LogoutAD(int sessionID, string loginMode, string userLoginID, string brokerParams)
		{
			base.Invoke("LogoutAD", new object[]
			{
				sessionID,
				loginMode,
				userLoginID,
				brokerParams
			});
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void LogoutADAsync(int sessionID, string loginMode, string userLoginID, string brokerParams)
		{
			this.LogoutADAsync(sessionID, loginMode, userLoginID, brokerParams, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void LogoutADAsync(int sessionID, string loginMode, string userLoginID, string brokerParams, object userState)
		{
			if (this.LogoutADOperationCompleted == null)
			{
				this.LogoutADOperationCompleted = new SendOrPostCallback(this.OnLogoutADOperationCompleted);
			}
			base.InvokeAsync("LogoutAD", new object[]
			{
				sessionID,
				loginMode,
				userLoginID,
				brokerParams
			}, this.LogoutADOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnLogoutADOperationCompleted(object arg)
		{
			if (this._LogoutADCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._LogoutADCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/ChangeCustomerPassword", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool ChangeCustomerPassword(string custAccID, string oldPassword, string newPassword)
		{
			object[] array = base.Invoke("ChangeCustomerPassword", new object[]
			{
				custAccID,
				oldPassword,
				newPassword
			});
			return (bool)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ChangeCustomerPasswordAsync(string custAccID, string oldPassword, string newPassword)
		{
			this.ChangeCustomerPasswordAsync(custAccID, oldPassword, newPassword, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ChangeCustomerPasswordAsync(string custAccID, string oldPassword, string newPassword, object userState)
		{
			if (this.ChangeCustomerPasswordOperationCompleted == null)
			{
				this.ChangeCustomerPasswordOperationCompleted = new SendOrPostCallback(this.OnChangeCustomerPasswordOperationCompleted);
			}
			base.InvokeAsync("ChangeCustomerPassword", new object[]
			{
				custAccID,
				oldPassword,
				newPassword
			}, this.ChangeCustomerPasswordOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnChangeCustomerPasswordOperationCompleted(object arg)
		{
			if (this._ChangeCustomerPasswordCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._ChangeCustomerPasswordCompleted(this, new ChangeCustomerPasswordCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/ChangeTraderPassword", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool ChangeTraderPassword(string traderID, string oldPassword, string newPassword)
		{
			object[] array = base.Invoke("ChangeTraderPassword", new object[]
			{
				traderID,
				oldPassword,
				newPassword
			});
			return (bool)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ChangeTraderPasswordAsync(string traderID, string oldPassword, string newPassword)
		{
			this.ChangeTraderPasswordAsync(traderID, oldPassword, newPassword, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ChangeTraderPasswordAsync(string traderID, string oldPassword, string newPassword, object userState)
		{
			if (this.ChangeTraderPasswordOperationCompleted == null)
			{
				this.ChangeTraderPasswordOperationCompleted = new SendOrPostCallback(this.OnChangeTraderPasswordOperationCompleted);
			}
			base.InvokeAsync("ChangeTraderPassword", new object[]
			{
				traderID,
				oldPassword,
				newPassword
			}, this.ChangeTraderPasswordOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnChangeTraderPasswordOperationCompleted(object arg)
		{
			if (this._ChangeTraderPasswordCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._ChangeTraderPasswordCompleted(this, new ChangeTraderPasswordCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/StockThresholdInformation", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string StockThresholdInformation(string loginID)
		{
			object[] array = base.Invoke("StockThresholdInformation", new object[]
			{
				loginID
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void StockThresholdInformationAsync(string loginID)
		{
			this.StockThresholdInformationAsync(loginID, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void StockThresholdInformationAsync(string loginID, object userState)
		{
			if (this.StockThresholdInformationOperationCompleted == null)
			{
				this.StockThresholdInformationOperationCompleted = new SendOrPostCallback(this.OnStockThresholdInformationOperationCompleted);
			}
			base.InvokeAsync("StockThresholdInformation", new object[]
			{
				loginID
			}, this.StockThresholdInformationOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnStockThresholdInformationOperationCompleted(object arg)
		{
			if (this._StockThresholdInformationCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._StockThresholdInformationCompleted(this, new StockThresholdInformationCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/SaveStockThreshold", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool SaveStockThreshold(string customerId, string items)
		{
			object[] array = base.Invoke("SaveStockThreshold", new object[]
			{
				customerId,
				items
			});
			return (bool)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SaveStockThresholdAsync(string customerId, string items)
		{
			this.SaveStockThresholdAsync(customerId, items, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SaveStockThresholdAsync(string customerId, string items, object userState)
		{
			if (this.SaveStockThresholdOperationCompleted == null)
			{
				this.SaveStockThresholdOperationCompleted = new SendOrPostCallback(this.OnSaveStockThresholdOperationCompleted);
			}
			base.InvokeAsync("SaveStockThreshold", new object[]
			{
				customerId,
				items
			}, this.SaveStockThresholdOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnSaveStockThresholdOperationCompleted(object arg)
		{
			if (this._SaveStockThresholdCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._SaveStockThresholdCompleted(this, new SaveStockThresholdCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/SendNewOrder", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string SendNewOrder(string stock, string side, long volume, string price, string account, long publishVolume, string condition, string trusteeID, string entryID, string kimengSessionId, string kimengLocal, string deposit, string authenKey, string localIp, string pstWebIP, string pstIspIP, string terminalID)
		{
			object[] array = base.Invoke("SendNewOrder", new object[]
			{
				stock,
				side,
				volume,
				price,
				account,
				publishVolume,
				condition,
				trusteeID,
				entryID,
				kimengSessionId,
				kimengLocal,
				deposit,
				authenKey,
				localIp,
				pstWebIP,
				pstIspIP,
				terminalID
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SendNewOrderAsync(string stock, string side, long volume, string price, string account, long publishVolume, string condition, string trusteeID, string entryID, string kimengSessionId, string kimengLocal, string deposit, string authenKey, string localIp, string pstWebIP, string pstIspIP, string terminalID)
		{
			this.SendNewOrderAsync(stock, side, volume, price, account, publishVolume, condition, trusteeID, entryID, kimengSessionId, kimengLocal, deposit, authenKey, localIp, pstWebIP, pstIspIP, terminalID, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SendNewOrderAsync(string stock, string side, long volume, string price, string account, long publishVolume, string condition, string trusteeID, string entryID, string kimengSessionId, string kimengLocal, string deposit, string authenKey, string localIp, string pstWebIP, string pstIspIP, string terminalID, object userState)
		{
			if (this.SendNewOrderOperationCompleted == null)
			{
				this.SendNewOrderOperationCompleted = new SendOrPostCallback(this.OnSendNewOrderOperationCompleted);
			}
			base.InvokeAsync("SendNewOrder", new object[]
			{
				stock,
				side,
				volume,
				price,
				account,
				publishVolume,
				condition,
				trusteeID,
				entryID,
				kimengSessionId,
				kimengLocal,
				deposit,
				authenKey,
				localIp,
				pstWebIP,
				pstIspIP,
				terminalID
			}, this.SendNewOrderOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnSendNewOrderOperationCompleted(object arg)
		{
			if (this._SendNewOrderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._SendNewOrderCompleted(this, new SendNewOrderCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/SendNewOrderParams", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string SendNewOrderParams(string orderParams)
		{
			object[] array = base.Invoke("SendNewOrderParams", new object[]
			{
				orderParams
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SendNewOrderParamsAsync(string orderParams)
		{
			this.SendNewOrderParamsAsync(orderParams, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SendNewOrderParamsAsync(string orderParams, object userState)
		{
			if (this.SendNewOrderParamsOperationCompleted == null)
			{
				this.SendNewOrderParamsOperationCompleted = new SendOrPostCallback(this.OnSendNewOrderParamsOperationCompleted);
			}
			base.InvokeAsync("SendNewOrderParams", new object[]
			{
				orderParams
			}, this.SendNewOrderParamsOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnSendNewOrderParamsOperationCompleted(object arg)
		{
			if (this._SendNewOrderParamsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._SendNewOrderParamsCompleted(this, new SendNewOrderParamsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/SendCancelOrder_AfterCloseFw", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string SendCancelOrder_AfterCloseFw(string orderDate, string orderTime, long orderNumber, string endtryId, string authenKey)
		{
			object[] array = base.Invoke("SendCancelOrder_AfterCloseFw", new object[]
			{
				orderDate,
				orderTime,
				orderNumber,
				endtryId,
				authenKey
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SendCancelOrder_AfterCloseFwAsync(string orderDate, string orderTime, long orderNumber, string endtryId, string authenKey)
		{
			this.SendCancelOrder_AfterCloseFwAsync(orderDate, orderTime, orderNumber, endtryId, authenKey, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SendCancelOrder_AfterCloseFwAsync(string orderDate, string orderTime, long orderNumber, string endtryId, string authenKey, object userState)
		{
			if (this.SendCancelOrder_AfterCloseFwOperationCompleted == null)
			{
				this.SendCancelOrder_AfterCloseFwOperationCompleted = new SendOrPostCallback(this.OnSendCancelOrder_AfterCloseFwOperationCompleted);
			}
			base.InvokeAsync("SendCancelOrder_AfterCloseFw", new object[]
			{
				orderDate,
				orderTime,
				orderNumber,
				endtryId,
				authenKey
			}, this.SendCancelOrder_AfterCloseFwOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnSendCancelOrder_AfterCloseFwOperationCompleted(object arg)
		{
			if (this._SendCancelOrder_AfterCloseFwCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._SendCancelOrder_AfterCloseFwCompleted(this, new SendCancelOrder_AfterCloseFwCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/SendCancelOrder", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string SendCancelOrder(string sendDate, long orderNumber, string entryID, string sessionId, string authenKey, string internetUser, string terminalID)
		{
			object[] array = base.Invoke("SendCancelOrder", new object[]
			{
				sendDate,
				orderNumber,
				entryID,
				sessionId,
				authenKey,
				internetUser,
				terminalID
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SendCancelOrderAsync(string sendDate, long orderNumber, string entryID, string sessionId, string authenKey, string internetUser, string terminalID)
		{
			this.SendCancelOrderAsync(sendDate, orderNumber, entryID, sessionId, authenKey, internetUser, terminalID, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SendCancelOrderAsync(string sendDate, long orderNumber, string entryID, string sessionId, string authenKey, string internetUser, string terminalID, object userState)
		{
			if (this.SendCancelOrderOperationCompleted == null)
			{
				this.SendCancelOrderOperationCompleted = new SendOrPostCallback(this.OnSendCancelOrderOperationCompleted);
			}
			base.InvokeAsync("SendCancelOrder", new object[]
			{
				sendDate,
				orderNumber,
				entryID,
				sessionId,
				authenKey,
				internetUser,
				terminalID
			}, this.SendCancelOrderOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnSendCancelOrderOperationCompleted(object arg)
		{
			if (this._SendCancelOrderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._SendCancelOrderCompleted(this, new SendCancelOrderCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/SendEditOrder", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string SendEditOrder(string authenKey, string sessionId, long orderNumber, string entryDate, long volume, string price, string logIn, long pubVol, int trusteeId, string userNo)
		{
			object[] array = base.Invoke("SendEditOrder", new object[]
			{
				authenKey,
				sessionId,
				orderNumber,
				entryDate,
				volume,
				price,
				logIn,
				pubVol,
				trusteeId,
				userNo
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SendEditOrderAsync(string authenKey, string sessionId, long orderNumber, string entryDate, long volume, string price, string logIn, long pubVol, int trusteeId, string userNo)
		{
			this.SendEditOrderAsync(authenKey, sessionId, orderNumber, entryDate, volume, price, logIn, pubVol, trusteeId, userNo, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SendEditOrderAsync(string authenKey, string sessionId, long orderNumber, string entryDate, long volume, string price, string logIn, long pubVol, int trusteeId, string userNo, object userState)
		{
			if (this.SendEditOrderOperationCompleted == null)
			{
				this.SendEditOrderOperationCompleted = new SendOrPostCallback(this.OnSendEditOrderOperationCompleted);
			}
			base.InvokeAsync("SendEditOrder", new object[]
			{
				authenKey,
				sessionId,
				orderNumber,
				entryDate,
				volume,
				price,
				logIn,
				pubVol,
				trusteeId,
				userNo
			}, this.SendEditOrderOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnSendEditOrderOperationCompleted(object arg)
		{
			if (this._SendEditOrderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._SendEditOrderCompleted(this, new SendEditOrderCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool IsLocalFileSystemWebService(string url)
		{
			bool result;
			if (url == null || url == string.Empty)
			{
				result = false;
			}
			else
			{
				Uri uri = new Uri(url);
				result = (uri.Port >= 1024 && string.Compare(uri.Host, "localHost", StringComparison.OrdinalIgnoreCase) == 0);
			}
			return result;
		}
	}
}
