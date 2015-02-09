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
namespace i2TradePlus.ITSNetBusinessWSTFEX
{
	[GeneratedCode("System.Web.Services", "2.0.50727.5483"), DesignerCategory("code"), DebuggerStepThrough, WebServiceBinding(Name = "ServiceSoap", Namespace = "http://tempuri.org/")]
	public class Service : SoapHttpClientProtocol
	{
		private SendOrPostCallback LoadSETindexOperationCompleted;
		private SendOrPostCallback LoadMktStatusOperationCompleted;
		private SendOrPostCallback TFEXInformationOperationCompleted;
		private SendOrPostCallback LoadTFEXInformationOperationCompleted;
		private SendOrPostCallback SeriesStateOperationCompleted;
		private SendOrPostCallback GetTotalMarketValueInfoOperationCompleted;
		private SendOrPostCallback SeriesByPricePageOperationCompleted;
		private SendOrPostCallback TopBBOTFEXOperationCompleted;
		private SendOrPostCallback TopBBOTFEXadOperationCompleted;
		private SendOrPostCallback TFEXTopActiveBBOOperationCompleted;
		private SendOrPostCallback BestProjected_TFEXOperationCompleted;
		private SendOrPostCallback BestBidOfferByListOperationCompleted;
		private SendOrPostCallback BestBidOfferByInstrumentOperationCompleted;
		private SendOrPostCallback BestBidOfferByOptionsListOperationCompleted;
		private SendOrPostCallback Get5BidOfferTFEXOperationCompleted;
		private SendOrPostCallback GetChartImageOperationCompleted;
		private SendOrPostCallback GetSwitchAccountInfoTFEXOperationCompleted;
		private SendOrPostCallback SeriesSaleByTimeOperationCompleted;
		private SendOrPostCallback SeriesSaleByPriceOperationCompleted;
		private SendOrPostCallback StockInPlayOperationCompleted;
		private SendOrPostCallback StockInPlayADOperationCompleted;
		private SendOrPostCallback SeriesSumaryOperationCompleted;
		private SendOrPostCallback TFEXBoardcastMessageOperationCompleted;
		private SendOrPostCallback ViewOrderTransactionOperationCompleted;
		private SendOrPostCallback ViewOrderByOrderNoOperationCompleted;
		private SendOrPostCallback ViewCustomersCreditOperationCompleted;
		private SendOrPostCallback ViewCustomersInfoOperationCompleted;
		private SendOrPostCallback ViewCustomersAllOperationCompleted;
		private SendOrPostCallback ViewOrderDealDataOperationCompleted;
		private SendOrPostCallback ViewCustomerCreditOnSendBoxOperationCompleted;
		private SendOrPostCallback SendTFEXNewOrderOperationCompleted;
		private SendOrPostCallback SendTFEXCancelOrderOperationCompleted;
		private SendOrPostCallback WriteLogOperationCompleted;
		private SendOrPostCallback GetGoldSpotOperationCompleted;
		private bool useDefaultCredentialsSetExplicitly;

        public LoadSETindexCompletedEventHandler _LoadSETindexCompleted;
		public event LoadSETindexCompletedEventHandler LoadSETindexCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._LoadSETindexCompleted = value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._LoadSETindexCompleted = value;
			}
		}
		public LoadMktStatusCompletedEventHandler _LoadMktStatusCompleted;
		public event LoadMktStatusCompletedEventHandler LoadMktStatusCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._LoadMktStatusCompleted = value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._LoadMktStatusCompleted = value;
			}
		}
		public  TFEXInformationCompletedEventHandler _TFEXInformationCompleted;
		public event TFEXInformationCompletedEventHandler TFEXInformationCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._TFEXInformationCompleted = value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._TFEXInformationCompleted = value;
			}
		}
		public  LoadTFEXInformationCompletedEventHandler _LoadTFEXInformationCompleted;
		public event LoadTFEXInformationCompletedEventHandler LoadTFEXInformationCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._LoadTFEXInformationCompleted = value;
            }
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._LoadTFEXInformationCompleted = value;
            }
		}
		public SeriesStateCompletedEventHandler _SeriesStateCompleted;
		public event SeriesStateCompletedEventHandler SeriesStateCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._SeriesStateCompleted = value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._SeriesStateCompleted = value;
			}
		}
		public GetTotalMarketValueInfoCompletedEventHandler _GetTotalMarketValueInfoCompleted;
		public event GetTotalMarketValueInfoCompletedEventHandler GetTotalMarketValueInfoCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._GetTotalMarketValueInfoCompleted = value;
            }
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._GetTotalMarketValueInfoCompleted = value;
            }
		}
		public SeriesByPricePageCompletedEventHandler _SeriesByPricePageCompleted;
		public event SeriesByPricePageCompletedEventHandler SeriesByPricePageCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._SeriesByPricePageCompleted = value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._SeriesByPricePageCompleted = value;
            }
		}
		public TopBBOTFEXCompletedEventHandler _TopBBOTFEXCompleted;
		public event TopBBOTFEXCompletedEventHandler TopBBOTFEXCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._TopBBOTFEXCompleted = value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._TopBBOTFEXCompleted = value;
			}
		}
		public TopBBOTFEXadCompletedEventHandler _TopBBOTFEXadCompleted;
		public event TopBBOTFEXadCompletedEventHandler TopBBOTFEXadCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._TopBBOTFEXadCompleted = value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._TopBBOTFEXadCompleted = value;
			}
		}
		public TFEXTopActiveBBOCompletedEventHandler _TFEXTopActiveBBOCompleted;
		public event TFEXTopActiveBBOCompletedEventHandler TFEXTopActiveBBOCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._TFEXTopActiveBBOCompleted = value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._TFEXTopActiveBBOCompleted = value;
			}
		}
		public  BestProjected_TFEXCompletedEventHandler _BestProjected_TFEXCompleted;
		public event BestProjected_TFEXCompletedEventHandler BestProjected_TFEXCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._BestProjected_TFEXCompleted = value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._BestProjected_TFEXCompleted = value;
			}
		}
		public BestBidOfferByListCompletedEventHandler _BestBidOfferByListCompleted;
		public event BestBidOfferByListCompletedEventHandler BestBidOfferByListCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._BestBidOfferByListCompleted = value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._BestBidOfferByListCompleted = value;
			}
		}
        public BestBidOfferByInstrumentCompletedEventHandler _BestBidOfferByInstrumentCompleted;
		public event BestBidOfferByInstrumentCompletedEventHandler BestBidOfferByInstrumentCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._BestBidOfferByInstrumentCompleted = value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._BestBidOfferByInstrumentCompleted = value;
			}
		}
		public BestBidOfferByOptionsListCompletedEventHandler _BestBidOfferByOptionsListCompleted;
		public event BestBidOfferByOptionsListCompletedEventHandler BestBidOfferByOptionsListCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._BestBidOfferByOptionsListCompleted = value;
            }
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._BestBidOfferByOptionsListCompleted = value;
            }
		}
		public Get5BidOfferTFEXCompletedEventHandler _Get5BidOfferTFEXCompleted;
		public event Get5BidOfferTFEXCompletedEventHandler Get5BidOfferTFEXCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._Get5BidOfferTFEXCompleted = value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._Get5BidOfferTFEXCompleted = value;
			}
		}
		public GetChartImageCompletedEventHandler _GetChartImageCompleted;
		public event GetChartImageCompletedEventHandler GetChartImageCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._GetChartImageCompleted = value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._GetChartImageCompleted = value;
			}
		}
		public GetSwitchAccountInfoTFEXCompletedEventHandler _GetSwitchAccountInfoTFEXCompleted;
		public event GetSwitchAccountInfoTFEXCompletedEventHandler GetSwitchAccountInfoTFEXCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._GetSwitchAccountInfoTFEXCompleted = value;
            }
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._GetSwitchAccountInfoTFEXCompleted = value;
            }
		}
		public SeriesSaleByTimeCompletedEventHandler _SeriesSaleByTimeCompleted;
		public event SeriesSaleByTimeCompletedEventHandler SeriesSaleByTimeCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._SeriesSaleByTimeCompleted = value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._SeriesSaleByTimeCompleted = value;
			}
		}
		public SeriesSaleByPriceCompletedEventHandler _SeriesSaleByPriceCompleted;
		public event SeriesSaleByPriceCompletedEventHandler SeriesSaleByPriceCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._SeriesSaleByPriceCompleted = value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._SeriesSaleByPriceCompleted = value;
			}
		}
		public StockInPlayCompletedEventHandler _StockInPlayCompleted;
		public event StockInPlayCompletedEventHandler StockInPlayCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._StockInPlayCompleted = value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._StockInPlayCompleted = value;
			}
		}
		public StockInPlayADCompletedEventHandler _StockInPlayADCompleted;
		public event StockInPlayADCompletedEventHandler StockInPlayADCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._StockInPlayADCompleted = value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._StockInPlayADCompleted = value;
			}
		}
		public SeriesSumaryCompletedEventHandler _SeriesSumaryCompleted;
		public event SeriesSumaryCompletedEventHandler SeriesSumaryCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._SeriesSumaryCompleted = value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._SeriesSumaryCompleted = value;
			}
		}
		public TFEXBoardcastMessageCompletedEventHandler _TFEXBoardcastMessageCompleted;
		public event TFEXBoardcastMessageCompletedEventHandler TFEXBoardcastMessageCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._TFEXBoardcastMessageCompleted = value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._TFEXBoardcastMessageCompleted = value;
			}
		}
		public ViewOrderTransactionCompletedEventHandler _ViewOrderTransactionCompleted;
		public event ViewOrderTransactionCompletedEventHandler ViewOrderTransactionCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._ViewOrderTransactionCompleted = value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._ViewOrderTransactionCompleted = value;
			}
		}
		public ViewOrderByOrderNoCompletedEventHandler _ViewOrderByOrderNoCompleted;
		public event ViewOrderByOrderNoCompletedEventHandler ViewOrderByOrderNoCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._ViewOrderByOrderNoCompleted = value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._ViewOrderByOrderNoCompleted = value;
			}
		}
		public ViewCustomersCreditCompletedEventHandler _ViewCustomersCreditCompleted;
		public event ViewCustomersCreditCompletedEventHandler ViewCustomersCreditCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._ViewCustomersCreditCompleted = value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._ViewCustomersCreditCompleted = value;
			}
		}
		public ViewCustomersInfoCompletedEventHandler _ViewCustomersInfoCompleted;
		public event ViewCustomersInfoCompletedEventHandler ViewCustomersInfoCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._ViewCustomersInfoCompleted = value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._ViewCustomersInfoCompleted = value;
			}
		}
		public ViewCustomersAllCompletedEventHandler _ViewCustomersAllCompleted;
		public event ViewCustomersAllCompletedEventHandler ViewCustomersAllCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._ViewCustomersAllCompleted = (ViewCustomersAllCompletedEventHandler)value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._ViewCustomersAllCompleted = (ViewCustomersAllCompletedEventHandler)value;
			}
		}
		public ViewOrderDealDataCompletedEventHandler _ViewOrderDealDataCompleted;
		public event ViewOrderDealDataCompletedEventHandler ViewOrderDealDataCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._ViewOrderDealDataCompleted = (ViewOrderDealDataCompletedEventHandler)value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._ViewOrderDealDataCompleted = (ViewOrderDealDataCompletedEventHandler)value;
			}
		}
		public ViewCustomerCreditOnSendBoxCompletedEventHandler _ViewCustomerCreditOnSendBoxCompleted;
		public event ViewCustomerCreditOnSendBoxCompletedEventHandler ViewCustomerCreditOnSendBoxCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._ViewCustomerCreditOnSendBoxCompleted = (ViewCustomerCreditOnSendBoxCompletedEventHandler)value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._ViewCustomerCreditOnSendBoxCompleted = (ViewCustomerCreditOnSendBoxCompletedEventHandler)value;

			}
		}
		public SendTFEXNewOrderCompletedEventHandler _SendTFEXNewOrderCompleted;
		public event SendTFEXNewOrderCompletedEventHandler SendTFEXNewOrderCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._SendTFEXNewOrderCompleted = value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._SendTFEXNewOrderCompleted = value;
			}
		}
		public SendTFEXCancelOrderCompletedEventHandler _SendTFEXCancelOrderCompleted;
		public event SendTFEXCancelOrderCompletedEventHandler SendTFEXCancelOrderCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._SendTFEXCancelOrderCompleted = value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._SendTFEXCancelOrderCompleted = value;
			}
		}
		public WriteLogCompletedEventHandler _WriteLogCompleted;
		public event WriteLogCompletedEventHandler WriteLogCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._WriteLogCompleted = value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._WriteLogCompleted = value;
			}
		}
		public GetGoldSpotCompletedEventHandler _GetGoldSpotCompleted;
		public event GetGoldSpotCompletedEventHandler GetGoldSpotCompleted
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._GetGoldSpotCompleted = value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._GetGoldSpotCompleted = value;
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
			this.Url = Settings.Default.i2_TradePlus_ITSNetBusinessWSTFEX_Service;
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
		[SoapDocumentMethod("http://tempuri.org/LoadSETindex", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string LoadSETindex()
		{
			object[] array = base.Invoke("LoadSETindex", new object[0]);
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void LoadSETindexAsync()
		{
			this.LoadSETindexAsync(null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void LoadSETindexAsync(object userState)
		{
			if (this.LoadSETindexOperationCompleted == null)
			{
				this.LoadSETindexOperationCompleted = new SendOrPostCallback(this.OnLoadSETindexOperationCompleted);
			}
			base.InvokeAsync("LoadSETindex", new object[0], this.LoadSETindexOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnLoadSETindexOperationCompleted(object arg)
		{
			if (this._LoadSETindexCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._LoadSETindexCompleted(this, new LoadSETindexCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/LoadMktStatus", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string LoadMktStatus()
		{
			object[] array = base.Invoke("LoadMktStatus", new object[0]);
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void LoadMktStatusAsync()
		{
			this.LoadMktStatusAsync(null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void LoadMktStatusAsync(object userState)
		{
			if (this.LoadMktStatusOperationCompleted == null)
			{
				this.LoadMktStatusOperationCompleted = new SendOrPostCallback(this.OnLoadMktStatusOperationCompleted);
			}
			base.InvokeAsync("LoadMktStatus", new object[0], this.LoadMktStatusOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnLoadMktStatusOperationCompleted(object arg)
		{
			if (this._LoadMktStatusCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._LoadMktStatusCompleted(this, new LoadMktStatusCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/TFEXInformation", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string TFEXInformation()
		{
			object[] array = base.Invoke("TFEXInformation", new object[0]);
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void TFEXInformationAsync()
		{
			this.TFEXInformationAsync(null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void TFEXInformationAsync(object userState)
		{
			if (this.TFEXInformationOperationCompleted == null)
			{
				this.TFEXInformationOperationCompleted = new SendOrPostCallback(this.OnTFEXInformationOperationCompleted);
			}
			base.InvokeAsync("TFEXInformation", new object[0], this.TFEXInformationOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnTFEXInformationOperationCompleted(object arg)
		{
			if (this._TFEXInformationCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._TFEXInformationCompleted(this, new TFEXInformationCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/LoadTFEXInformation", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string LoadTFEXInformation(int orderBookId, int topSelect)
		{
			object[] array = base.Invoke("LoadTFEXInformation", new object[]
			{
				orderBookId,
				topSelect
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void LoadTFEXInformationAsync(int orderBookId, int topSelect)
		{
			this.LoadTFEXInformationAsync(orderBookId, topSelect, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void LoadTFEXInformationAsync(int orderBookId, int topSelect, object userState)
		{
			if (this.LoadTFEXInformationOperationCompleted == null)
			{
				this.LoadTFEXInformationOperationCompleted = new SendOrPostCallback(this.OnLoadTFEXInformationOperationCompleted);
			}
			base.InvokeAsync("LoadTFEXInformation", new object[]
			{
				orderBookId,
				topSelect
			}, this.LoadTFEXInformationOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnLoadTFEXInformationOperationCompleted(object arg)
		{
			if (this._LoadTFEXInformationCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._LoadTFEXInformationCompleted(this, new LoadTFEXInformationCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/SeriesState", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string SeriesState()
		{
			object[] array = base.Invoke("SeriesState", new object[0]);
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SeriesStateAsync()
		{
			this.SeriesStateAsync(null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SeriesStateAsync(object userState)
		{
			if (this.SeriesStateOperationCompleted == null)
			{
				this.SeriesStateOperationCompleted = new SendOrPostCallback(this.OnSeriesStateOperationCompleted);
			}
			base.InvokeAsync("SeriesState", new object[0], this.SeriesStateOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnSeriesStateOperationCompleted(object arg)
		{
			if (this._SeriesStateCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._SeriesStateCompleted(this, new SeriesStateCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/GetTotalMarketValueInfo", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string GetTotalMarketValueInfo()
		{
			object[] array = base.Invoke("GetTotalMarketValueInfo", new object[0]);
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetTotalMarketValueInfoAsync()
		{
			this.GetTotalMarketValueInfoAsync(null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetTotalMarketValueInfoAsync(object userState)
		{
			if (this.GetTotalMarketValueInfoOperationCompleted == null)
			{
				this.GetTotalMarketValueInfoOperationCompleted = new SendOrPostCallback(this.OnGetTotalMarketValueInfoOperationCompleted);
			}
			base.InvokeAsync("GetTotalMarketValueInfo", new object[0], this.GetTotalMarketValueInfoOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnGetTotalMarketValueInfoOperationCompleted(object arg)
		{
			if (this._GetTotalMarketValueInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._GetTotalMarketValueInfoCompleted(this, new GetTotalMarketValueInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/SeriesByPricePage", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string SeriesByPricePage(string seriesInfo, string seriesType, int maxTicker)
		{
			object[] array = base.Invoke("SeriesByPricePage", new object[]
			{
				seriesInfo,
				seriesType,
				maxTicker
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SeriesByPricePageAsync(string seriesInfo, string seriesType, int maxTicker)
		{
			this.SeriesByPricePageAsync(seriesInfo, seriesType, maxTicker, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SeriesByPricePageAsync(string seriesInfo, string seriesType, int maxTicker, object userState)
		{
			if (this.SeriesByPricePageOperationCompleted == null)
			{
				this.SeriesByPricePageOperationCompleted = new SendOrPostCallback(this.OnSeriesByPricePageOperationCompleted);
			}
			base.InvokeAsync("SeriesByPricePage", new object[]
			{
				seriesInfo,
				seriesType,
				maxTicker
			}, this.SeriesByPricePageOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnSeriesByPricePageOperationCompleted(object arg)
		{
			if (this._SeriesByPricePageCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._SeriesByPricePageCompleted(this, new SeriesByPricePageCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/TopBBOTFEX", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string TopBBOTFEX(string[] seriesList, int maxTicker)
		{
			object[] array = base.Invoke("TopBBOTFEX", new object[]
			{
				seriesList,
				maxTicker
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void TopBBOTFEXAsync(string[] seriesList, int maxTicker)
		{
			this.TopBBOTFEXAsync(seriesList, maxTicker, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void TopBBOTFEXAsync(string[] seriesList, int maxTicker, object userState)
		{
			if (this.TopBBOTFEXOperationCompleted == null)
			{
				this.TopBBOTFEXOperationCompleted = new SendOrPostCallback(this.OnTopBBOTFEXOperationCompleted);
			}
			base.InvokeAsync("TopBBOTFEX", new object[]
			{
				seriesList,
				maxTicker
			}, this.TopBBOTFEXOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnTopBBOTFEXOperationCompleted(object arg)
		{
			if (this._TopBBOTFEXCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._TopBBOTFEXCompleted(this, new TopBBOTFEXCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/TopBBOTFEXad", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string TopBBOTFEXad(string series, int maxTicker)
		{
			object[] array = base.Invoke("TopBBOTFEXad", new object[]
			{
				series,
				maxTicker
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void TopBBOTFEXadAsync(string series, int maxTicker)
		{
			this.TopBBOTFEXadAsync(series, maxTicker, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void TopBBOTFEXadAsync(string series, int maxTicker, object userState)
		{
			if (this.TopBBOTFEXadOperationCompleted == null)
			{
				this.TopBBOTFEXadOperationCompleted = new SendOrPostCallback(this.OnTopBBOTFEXadOperationCompleted);
			}
			base.InvokeAsync("TopBBOTFEXad", new object[]
			{
				series,
				maxTicker
			}, this.TopBBOTFEXadOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnTopBBOTFEXadOperationCompleted(object arg)
		{
			if (this._TopBBOTFEXadCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._TopBBOTFEXadCompleted(this, new TopBBOTFEXadCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/TFEXTopActiveBBO", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string TFEXTopActiveBBO(string viewType, bool isFuture)
		{
			object[] array = base.Invoke("TFEXTopActiveBBO", new object[]
			{
				viewType,
				isFuture
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void TFEXTopActiveBBOAsync(string viewType, bool isFuture)
		{
			this.TFEXTopActiveBBOAsync(viewType, isFuture, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void TFEXTopActiveBBOAsync(string viewType, bool isFuture, object userState)
		{
			if (this.TFEXTopActiveBBOOperationCompleted == null)
			{
				this.TFEXTopActiveBBOOperationCompleted = new SendOrPostCallback(this.OnTFEXTopActiveBBOOperationCompleted);
			}
			base.InvokeAsync("TFEXTopActiveBBO", new object[]
			{
				viewType,
				isFuture
			}, this.TFEXTopActiveBBOOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnTFEXTopActiveBBOOperationCompleted(object arg)
		{
			if (this._TFEXTopActiveBBOCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._TFEXTopActiveBBOCompleted(this, new TFEXTopActiveBBOCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/BestProjected_TFEX", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string BestProjected_TFEX(bool isFutures, string viewType, int rowPerPage)
		{
			object[] array = base.Invoke("BestProjected_TFEX", new object[]
			{
				isFutures,
				viewType,
				rowPerPage
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void BestProjected_TFEXAsync(bool isFutures, string viewType, int rowPerPage)
		{
			this.BestProjected_TFEXAsync(isFutures, viewType, rowPerPage, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void BestProjected_TFEXAsync(bool isFutures, string viewType, int rowPerPage, object userState)
		{
			if (this.BestProjected_TFEXOperationCompleted == null)
			{
				this.BestProjected_TFEXOperationCompleted = new SendOrPostCallback(this.OnBestProjected_TFEXOperationCompleted);
			}
			base.InvokeAsync("BestProjected_TFEX", new object[]
			{
				isFutures,
				viewType,
				rowPerPage
			}, this.BestProjected_TFEXOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnBestProjected_TFEXOperationCompleted(object arg)
		{
			if (this._BestProjected_TFEXCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._BestProjected_TFEXCompleted(this, new BestProjected_TFEXCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/BestBidOfferByList", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string BestBidOfferByList(string seriesListName)
		{
			object[] array = base.Invoke("BestBidOfferByList", new object[]
			{
				seriesListName
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void BestBidOfferByListAsync(string seriesListName)
		{
			this.BestBidOfferByListAsync(seriesListName, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void BestBidOfferByListAsync(string seriesListName, object userState)
		{
			if (this.BestBidOfferByListOperationCompleted == null)
			{
				this.BestBidOfferByListOperationCompleted = new SendOrPostCallback(this.OnBestBidOfferByListOperationCompleted);
			}
			base.InvokeAsync("BestBidOfferByList", new object[]
			{
				seriesListName
			}, this.BestBidOfferByListOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnBestBidOfferByListOperationCompleted(object arg)
		{
			if (this._BestBidOfferByListCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._BestBidOfferByListCompleted(this, new BestBidOfferByListCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/BestBidOfferByInstrument", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string BestBidOfferByInstrument(int orderBookID)
		{
			object[] array = base.Invoke("BestBidOfferByInstrument", new object[]
			{
				orderBookID
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void BestBidOfferByInstrumentAsync(int orderBookID)
		{
			this.BestBidOfferByInstrumentAsync(orderBookID, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void BestBidOfferByInstrumentAsync(int orderBookID, object userState)
		{
			if (this.BestBidOfferByInstrumentOperationCompleted == null)
			{
				this.BestBidOfferByInstrumentOperationCompleted = new SendOrPostCallback(this.OnBestBidOfferByInstrumentOperationCompleted);
			}
			base.InvokeAsync("BestBidOfferByInstrument", new object[]
			{
				orderBookID
			}, this.BestBidOfferByInstrumentOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnBestBidOfferByInstrumentOperationCompleted(object arg)
		{
			if (this._BestBidOfferByInstrumentCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._BestBidOfferByInstrumentCompleted(this, new BestBidOfferByInstrumentCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/BestBidOfferByOptionsList", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string BestBidOfferByOptionsList(string expDate)
		{
			object[] array = base.Invoke("BestBidOfferByOptionsList", new object[]
			{
				expDate
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void BestBidOfferByOptionsListAsync(string expDate)
		{
			this.BestBidOfferByOptionsListAsync(expDate, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void BestBidOfferByOptionsListAsync(string expDate, object userState)
		{
			if (this.BestBidOfferByOptionsListOperationCompleted == null)
			{
				this.BestBidOfferByOptionsListOperationCompleted = new SendOrPostCallback(this.OnBestBidOfferByOptionsListOperationCompleted);
			}
			base.InvokeAsync("BestBidOfferByOptionsList", new object[]
			{
				expDate
			}, this.BestBidOfferByOptionsListOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnBestBidOfferByOptionsListOperationCompleted(object arg)
		{
			if (this._BestBidOfferByOptionsListCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._BestBidOfferByOptionsListCompleted(this, new BestBidOfferByOptionsListCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/Get5BidOfferTFEX", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Get5BidOfferTFEX(string series)
		{
			object[] array = base.Invoke("Get5BidOfferTFEX", new object[]
			{
				series
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Get5BidOfferTFEXAsync(string series)
		{
			this.Get5BidOfferTFEXAsync(series, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Get5BidOfferTFEXAsync(string series, object userState)
		{
			if (this.Get5BidOfferTFEXOperationCompleted == null)
			{
				this.Get5BidOfferTFEXOperationCompleted = new SendOrPostCallback(this.OnGet5BidOfferTFEXOperationCompleted);
			}
			base.InvokeAsync("Get5BidOfferTFEX", new object[]
			{
				series
			}, this.Get5BidOfferTFEXOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnGet5BidOfferTFEXOperationCompleted(object arg)
		{
			if (this._Get5BidOfferTFEXCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._Get5BidOfferTFEXCompleted(this, new Get5BidOfferTFEXCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/GetChartImage", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string GetChartImage(string seriesName, bool showVolumn, int width, int height, int marketCode)
		{
			object[] array = base.Invoke("GetChartImage", new object[]
			{
				seriesName,
				showVolumn,
				width,
				height,
				marketCode
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetChartImageAsync(string seriesName, bool showVolumn, int width, int height, int marketCode)
		{
			this.GetChartImageAsync(seriesName, showVolumn, width, height, marketCode, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetChartImageAsync(string seriesName, bool showVolumn, int width, int height, int marketCode, object userState)
		{
			if (this.GetChartImageOperationCompleted == null)
			{
				this.GetChartImageOperationCompleted = new SendOrPostCallback(this.OnGetChartImageOperationCompleted);
			}
			base.InvokeAsync("GetChartImage", new object[]
			{
				seriesName,
				showVolumn,
				width,
				height,
				marketCode
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
		[SoapDocumentMethod("http://tempuri.org/GetSwitchAccountInfoTFEX", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string GetSwitchAccountInfoTFEX(string account)
		{
			object[] array = base.Invoke("GetSwitchAccountInfoTFEX", new object[]
			{
				account
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetSwitchAccountInfoTFEXAsync(string account)
		{
			this.GetSwitchAccountInfoTFEXAsync(account, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetSwitchAccountInfoTFEXAsync(string account, object userState)
		{
			if (this.GetSwitchAccountInfoTFEXOperationCompleted == null)
			{
				this.GetSwitchAccountInfoTFEXOperationCompleted = new SendOrPostCallback(this.OnGetSwitchAccountInfoTFEXOperationCompleted);
			}
			base.InvokeAsync("GetSwitchAccountInfoTFEX", new object[]
			{
				account
			}, this.GetSwitchAccountInfoTFEXOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnGetSwitchAccountInfoTFEXOperationCompleted(object arg)
		{
			if (this._GetSwitchAccountInfoTFEXCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._GetSwitchAccountInfoTFEXCompleted(this, new GetSwitchAccountInfoTFEXCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/SeriesSaleByTime", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string SeriesSaleByTime(string seriesName, string seriesType, int pageNo, int recordPerPage, int maxTicker, string timeSearch)
		{
			object[] array = base.Invoke("SeriesSaleByTime", new object[]
			{
				seriesName,
				seriesType,
				pageNo,
				recordPerPage,
				maxTicker,
				timeSearch
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SeriesSaleByTimeAsync(string seriesName, string seriesType, int pageNo, int recordPerPage, int maxTicker, string timeSearch)
		{
			this.SeriesSaleByTimeAsync(seriesName, seriesType, pageNo, recordPerPage, maxTicker, timeSearch, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SeriesSaleByTimeAsync(string seriesName, string seriesType, int pageNo, int recordPerPage, int maxTicker, string timeSearch, object userState)
		{
			if (this.SeriesSaleByTimeOperationCompleted == null)
			{
				this.SeriesSaleByTimeOperationCompleted = new SendOrPostCallback(this.OnSeriesSaleByTimeOperationCompleted);
			}
			base.InvokeAsync("SeriesSaleByTime", new object[]
			{
				seriesName,
				seriesType,
				pageNo,
				recordPerPage,
				maxTicker,
				timeSearch
			}, this.SeriesSaleByTimeOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnSeriesSaleByTimeOperationCompleted(object arg)
		{
			if (this._SeriesSaleByTimeCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._SeriesSaleByTimeCompleted(this, new SeriesSaleByTimeCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/SeriesSaleByPrice", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string SeriesSaleByPrice(string seriesName, string seriesType, int maxTicker)
		{
			object[] array = base.Invoke("SeriesSaleByPrice", new object[]
			{
				seriesName,
				seriesType,
				maxTicker
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SeriesSaleByPriceAsync(string seriesName, string seriesType, int maxTicker)
		{
			this.SeriesSaleByPriceAsync(seriesName, seriesType, maxTicker, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SeriesSaleByPriceAsync(string seriesName, string seriesType, int maxTicker, object userState)
		{
			if (this.SeriesSaleByPriceOperationCompleted == null)
			{
				this.SeriesSaleByPriceOperationCompleted = new SendOrPostCallback(this.OnSeriesSaleByPriceOperationCompleted);
			}
			base.InvokeAsync("SeriesSaleByPrice", new object[]
			{
				seriesName,
				seriesType,
				maxTicker
			}, this.SeriesSaleByPriceOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnSeriesSaleByPriceOperationCompleted(object arg)
		{
			if (this._SeriesSaleByPriceCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._SeriesSaleByPriceCompleted(this, new SeriesSaleByPriceCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/StockInPlay", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string StockInPlay(string seriesName, string seriesType, decimal tickSize, int TFEXSession, decimal StartPrice, string Side, int rowPerPage, int maxTicker)
		{
			object[] array = base.Invoke("StockInPlay", new object[]
			{
				seriesName,
				seriesType,
				tickSize,
				TFEXSession,
				StartPrice,
				Side,
				rowPerPage,
				maxTicker
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void StockInPlayAsync(string seriesName, string seriesType, decimal tickSize, int TFEXSession, decimal StartPrice, string Side, int rowPerPage, int maxTicker)
		{
			this.StockInPlayAsync(seriesName, seriesType, tickSize, TFEXSession, StartPrice, Side, rowPerPage, maxTicker, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void StockInPlayAsync(string seriesName, string seriesType, decimal tickSize, int TFEXSession, decimal StartPrice, string Side, int rowPerPage, int maxTicker, object userState)
		{
			if (this.StockInPlayOperationCompleted == null)
			{
				this.StockInPlayOperationCompleted = new SendOrPostCallback(this.OnStockInPlayOperationCompleted);
			}
			base.InvokeAsync("StockInPlay", new object[]
			{
				seriesName,
				seriesType,
				tickSize,
				TFEXSession,
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
		[SoapDocumentMethod("http://tempuri.org/StockInPlayAD", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string StockInPlayAD(string seriesName, string seriesType, decimal tickSize, int TFEXSession, decimal StartPrice, string Side, int rowPerPage, int maxTicker)
		{
			object[] array = base.Invoke("StockInPlayAD", new object[]
			{
				seriesName,
				seriesType,
				tickSize,
				TFEXSession,
				StartPrice,
				Side,
				rowPerPage,
				maxTicker
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void StockInPlayADAsync(string seriesName, string seriesType, decimal tickSize, int TFEXSession, decimal StartPrice, string Side, int rowPerPage, int maxTicker)
		{
			this.StockInPlayADAsync(seriesName, seriesType, tickSize, TFEXSession, StartPrice, Side, rowPerPage, maxTicker, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void StockInPlayADAsync(string seriesName, string seriesType, decimal tickSize, int TFEXSession, decimal StartPrice, string Side, int rowPerPage, int maxTicker, object userState)
		{
			if (this.StockInPlayADOperationCompleted == null)
			{
				this.StockInPlayADOperationCompleted = new SendOrPostCallback(this.OnStockInPlayADOperationCompleted);
			}
			base.InvokeAsync("StockInPlayAD", new object[]
			{
				seriesName,
				seriesType,
				tickSize,
				TFEXSession,
				StartPrice,
				Side,
				rowPerPage,
				maxTicker
			}, this.StockInPlayADOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnStockInPlayADOperationCompleted(object arg)
		{
			if (this._StockInPlayADCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._StockInPlayADCompleted(this, new StockInPlayADCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/SeriesSumary", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string SeriesSumary(string seriesName, string seriesType, int pageNo, int maxTicker, string timeSearch)
		{
			object[] array = base.Invoke("SeriesSumary", new object[]
			{
				seriesName,
				seriesType,
				pageNo,
				maxTicker,
				timeSearch
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SeriesSumaryAsync(string seriesName, string seriesType, int pageNo, int maxTicker, string timeSearch)
		{
			this.SeriesSumaryAsync(seriesName, seriesType, pageNo, maxTicker, timeSearch, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SeriesSumaryAsync(string seriesName, string seriesType, int pageNo, int maxTicker, string timeSearch, object userState)
		{
			if (this.SeriesSumaryOperationCompleted == null)
			{
				this.SeriesSumaryOperationCompleted = new SendOrPostCallback(this.OnSeriesSumaryOperationCompleted);
			}
			base.InvokeAsync("SeriesSumary", new object[]
			{
				seriesName,
				seriesType,
				pageNo,
				maxTicker,
				timeSearch
			}, this.SeriesSumaryOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnSeriesSumaryOperationCompleted(object arg)
		{
			if (this._SeriesSumaryCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._SeriesSumaryCompleted(this, new SeriesSumaryCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/TFEXBoardcastMessage", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string TFEXBoardcastMessage(int recordPerPage)
		{
			object[] array = base.Invoke("TFEXBoardcastMessage", new object[]
			{
				recordPerPage
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void TFEXBoardcastMessageAsync(int recordPerPage)
		{
			this.TFEXBoardcastMessageAsync(recordPerPage, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void TFEXBoardcastMessageAsync(int recordPerPage, object userState)
		{
			if (this.TFEXBoardcastMessageOperationCompleted == null)
			{
				this.TFEXBoardcastMessageOperationCompleted = new SendOrPostCallback(this.OnTFEXBoardcastMessageOperationCompleted);
			}
			base.InvokeAsync("TFEXBoardcastMessage", new object[]
			{
				recordPerPage
			}, this.TFEXBoardcastMessageOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnTFEXBoardcastMessageOperationCompleted(object arg)
		{
			if (this._TFEXBoardcastMessageCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._TFEXBoardcastMessageCompleted(this, new TFEXBoardcastMessageCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
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
		[SoapDocumentMethod("http://tempuri.org/ViewOrderByOrderNo", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string ViewOrderByOrderNo(string account, string orderNo)
		{
			object[] array = base.Invoke("ViewOrderByOrderNo", new object[]
			{
				account,
				orderNo
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewOrderByOrderNoAsync(string account, string orderNo)
		{
			this.ViewOrderByOrderNoAsync(account, orderNo, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewOrderByOrderNoAsync(string account, string orderNo, object userState)
		{
			if (this.ViewOrderByOrderNoOperationCompleted == null)
			{
				this.ViewOrderByOrderNoOperationCompleted = new SendOrPostCallback(this.OnViewOrderByOrderNoOperationCompleted);
			}
			base.InvokeAsync("ViewOrderByOrderNo", new object[]
			{
				account,
				orderNo
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
		[SoapDocumentMethod("http://tempuri.org/ViewCustomersCredit", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string ViewCustomersCredit(string custAccID)
		{
			object[] array = base.Invoke("ViewCustomersCredit", new object[]
			{
				custAccID
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewCustomersCreditAsync(string custAccID)
		{
			this.ViewCustomersCreditAsync(custAccID, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewCustomersCreditAsync(string custAccID, object userState)
		{
			if (this.ViewCustomersCreditOperationCompleted == null)
			{
				this.ViewCustomersCreditOperationCompleted = new SendOrPostCallback(this.OnViewCustomersCreditOperationCompleted);
			}
			base.InvokeAsync("ViewCustomersCredit", new object[]
			{
				custAccID
			}, this.ViewCustomersCreditOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnViewCustomersCreditOperationCompleted(object arg)
		{
			if (this._ViewCustomersCreditCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._ViewCustomersCreditCompleted(this, new ViewCustomersCreditCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/ViewCustomersInfo", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string ViewCustomersInfo(string custAccID)
		{
			object[] array = base.Invoke("ViewCustomersInfo", new object[]
			{
				custAccID
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewCustomersInfoAsync(string custAccID)
		{
			this.ViewCustomersInfoAsync(custAccID, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewCustomersInfoAsync(string custAccID, object userState)
		{
			if (this.ViewCustomersInfoOperationCompleted == null)
			{
				this.ViewCustomersInfoOperationCompleted = new SendOrPostCallback(this.OnViewCustomersInfoOperationCompleted);
			}
			base.InvokeAsync("ViewCustomersInfo", new object[]
			{
				custAccID
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
		[SoapDocumentMethod("http://tempuri.org/ViewCustomersAll", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string ViewCustomersAll(string custAccID)
		{
			object[] array = base.Invoke("ViewCustomersAll", new object[]
			{
				custAccID
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewCustomersAllAsync(string custAccID)
		{
			this.ViewCustomersAllAsync(custAccID, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewCustomersAllAsync(string custAccID, object userState)
		{
			if (this.ViewCustomersAllOperationCompleted == null)
			{
				this.ViewCustomersAllOperationCompleted = new SendOrPostCallback(this.OnViewCustomersAllOperationCompleted);
			}
			base.InvokeAsync("ViewCustomersAll", new object[]
			{
				custAccID
			}, this.ViewCustomersAllOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnViewCustomersAllOperationCompleted(object arg)
		{
			if (this._ViewCustomersAllCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._ViewCustomersAllCompleted(this, new ViewCustomersAllCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/ViewOrderDealData", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string ViewOrderDealData(long orderNumber, int startRow, int rowPerPage, int dbType, string ordType, string sendDate)
		{
			object[] array = base.Invoke("ViewOrderDealData", new object[]
			{
				orderNumber,
				startRow,
				rowPerPage,
				dbType,
				ordType,
				sendDate
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewOrderDealDataAsync(long orderNumber, int startRow, int rowPerPage, int dbType, string ordType, string sendDate)
		{
			this.ViewOrderDealDataAsync(orderNumber, startRow, rowPerPage, dbType, ordType, sendDate, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ViewOrderDealDataAsync(long orderNumber, int startRow, int rowPerPage, int dbType, string ordType, string sendDate, object userState)
		{
			if (this.ViewOrderDealDataOperationCompleted == null)
			{
				this.ViewOrderDealDataOperationCompleted = new SendOrPostCallback(this.OnViewOrderDealDataOperationCompleted);
			}
			base.InvokeAsync("ViewOrderDealData", new object[]
			{
				orderNumber,
				startRow,
				rowPerPage,
				dbType,
				ordType,
				sendDate
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
		[SoapDocumentMethod("http://tempuri.org/SendTFEXNewOrder", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string SendTFEXNewOrder(string series, string side, long volume, string price, string account, long publishVolume, string position, string stopPrice, string stopCond, string stopSeries, string Validity, string ValidityDate, string sessionID, string requstID, string pinCode, string authenKey, string TraderID, string localIp, string kimengSession, string kimengLocal, string AppSymbol, string priceType)
		{
			object[] array = base.Invoke("SendTFEXNewOrder", new object[]
			{
				series,
				side,
				volume,
				price,
				account,
				publishVolume,
				position,
				stopPrice,
				stopCond,
				stopSeries,
				Validity,
				ValidityDate,
				sessionID,
				requstID,
				pinCode,
				authenKey,
				TraderID,
				localIp,
				kimengSession,
				kimengLocal,
				AppSymbol,
				priceType
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SendTFEXNewOrderAsync(string series, string side, long volume, string price, string account, long publishVolume, string position, string stopPrice, string stopCond, string stopSeries, string Validity, string ValidityDate, string sessionID, string requstID, string pinCode, string authenKey, string TraderID, string localIp, string kimengSession, string kimengLocal, string AppSymbol, string priceType)
		{
			this.SendTFEXNewOrderAsync(series, side, volume, price, account, publishVolume, position, stopPrice, stopCond, stopSeries, Validity, ValidityDate, sessionID, requstID, pinCode, authenKey, TraderID, localIp, kimengSession, kimengLocal, AppSymbol, priceType, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SendTFEXNewOrderAsync(string series, string side, long volume, string price, string account, long publishVolume, string position, string stopPrice, string stopCond, string stopSeries, string Validity, string ValidityDate, string sessionID, string requstID, string pinCode, string authenKey, string TraderID, string localIp, string kimengSession, string kimengLocal, string AppSymbol, string priceType, object userState)
		{
			if (this.SendTFEXNewOrderOperationCompleted == null)
			{
				this.SendTFEXNewOrderOperationCompleted = new SendOrPostCallback(this.OnSendTFEXNewOrderOperationCompleted);
			}
			base.InvokeAsync("SendTFEXNewOrder", new object[]
			{
				series,
				side,
				volume,
				price,
				account,
				publishVolume,
				position,
				stopPrice,
				stopCond,
				stopSeries,
				Validity,
				ValidityDate,
				sessionID,
				requstID,
				pinCode,
				authenKey,
				TraderID,
				localIp,
				kimengSession,
				kimengLocal,
				AppSymbol,
				priceType
			}, this.SendTFEXNewOrderOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnSendTFEXNewOrderOperationCompleted(object arg)
		{
			if (this._SendTFEXNewOrderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._SendTFEXNewOrderCompleted(this, new SendTFEXNewOrderCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/SendTFEXCancelOrder", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string SendTFEXCancelOrder(long orderNumber, string entryID, string sendDate, string internetUser, string authenKey, string localIp, string kimengSession, string AppSymbol)
		{
			object[] array = base.Invoke("SendTFEXCancelOrder", new object[]
			{
				orderNumber,
				entryID,
				sendDate,
				internetUser,
				authenKey,
				localIp,
				kimengSession,
				AppSymbol
			});
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SendTFEXCancelOrderAsync(long orderNumber, string entryID, string sendDate, string internetUser, string authenKey, string localIp, string kimengSession, string AppSymbol)
		{
			this.SendTFEXCancelOrderAsync(orderNumber, entryID, sendDate, internetUser, authenKey, localIp, kimengSession, AppSymbol, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SendTFEXCancelOrderAsync(long orderNumber, string entryID, string sendDate, string internetUser, string authenKey, string localIp, string kimengSession, string AppSymbol, object userState)
		{
			if (this.SendTFEXCancelOrderOperationCompleted == null)
			{
				this.SendTFEXCancelOrderOperationCompleted = new SendOrPostCallback(this.OnSendTFEXCancelOrderOperationCompleted);
			}
			base.InvokeAsync("SendTFEXCancelOrder", new object[]
			{
				orderNumber,
				entryID,
				sendDate,
				internetUser,
				authenKey,
				localIp,
				kimengSession,
				AppSymbol
			}, this.SendTFEXCancelOrderOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnSendTFEXCancelOrderOperationCompleted(object arg)
		{
			if (this._SendTFEXCancelOrderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._SendTFEXCancelOrderCompleted(this, new SendTFEXCancelOrderCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/WriteLog", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void WriteLog(string log)
		{
			base.Invoke("WriteLog", new object[]
			{
				log
			});
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void WriteLogAsync(string log)
		{
			this.WriteLogAsync(log, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void WriteLogAsync(string log, object userState)
		{
			if (this.WriteLogOperationCompleted == null)
			{
				this.WriteLogOperationCompleted = new SendOrPostCallback(this.OnWriteLogOperationCompleted);
			}
			base.InvokeAsync("WriteLog", new object[]
			{
				log
			}, this.WriteLogOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnWriteLogOperationCompleted(object arg)
		{
			if (this._WriteLogCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._WriteLogCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}
		[SoapDocumentMethod("http://tempuri.org/GetGoldSpot", Use = SoapBindingUse.Literal, RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", ParameterStyle = SoapParameterStyle.Wrapped)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string GetGoldSpot()
		{
			object[] array = base.Invoke("GetGoldSpot", new object[0]);
			return (string)array[0];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetGoldSpotAsync()
		{
			this.GetGoldSpotAsync(null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void GetGoldSpotAsync(object userState)
		{
			if (this.GetGoldSpotOperationCompleted == null)
			{
				this.GetGoldSpotOperationCompleted = new SendOrPostCallback(this.OnGetGoldSpotOperationCompleted);
			}
			base.InvokeAsync("GetGoldSpot", new object[0], this.GetGoldSpotOperationCompleted, userState);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnGetGoldSpotOperationCompleted(object arg)
		{
			if (this._GetGoldSpotCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this._GetGoldSpotCompleted(this, new GetGoldSpotCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
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
