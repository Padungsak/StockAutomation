using i2TradePlus.Information;
using i2TradePlus.ITSNetBusinessWS;
using i2TradePlus.ITSNetBusinessWSTFEX;
using i2TradePlus.Templates;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Web.Services.Protocols;
using System.Windows.Forms;
namespace i2TradePlus
{
	internal class ApplicationInfo
	{
		public class SendNewOrderResult
		{
			public long OrderNo;
			public string ResultMessage;
			public bool IsFwOfflineOrder = false;
			[MethodImpl(MethodImplOptions.NoInlining)]
			public SendNewOrderResult()
			{
			}
		}
		public const string USER_APPINFO_FILENAME = "ApplicationInfo";
		public const string CUSTOMER_LOGIN_MODE = "C";
		public const string INVESTOR_LOGIN_MODE = "I";
		public const string TRADING_LOGIN_MODE = "T";
		public const string USER_PROFILE_FILENAME = "UserProfile";
		public const int GROUP_CALL = 1;
		public const int GROUP_PUT = 2;
		public const int GROUP_FUTURE = 4;
		public const int GROUP_MSTOCK = 5;
		public const int BROKER_CNS = 1;
		public const int BROKER_KTZ = 2;
		public const int BROKER_BFIT = 3;
		public const int BROKER_KIMENG = 4;
		public const int BROKER_AIRA = 7;
		public const int BROKER_ASIA_PLUS = 8;
		public const int BROKER_KTB = 9;
		public const int BROKER_AWS = 10;
		public const int BROKER_CIMB = 11;
		public const int BROKER_KK = 12;
		public const int BROKER_LH = 13;
		public const int BROKER_AEC = 14;
		public const int BROKER_DEMO = 88;
		public const int BROKER_CNS_DEMO = 89;
		private static AccountInfo accInfo;
		public static string UserLoginID = string.Empty;
		public static bool IsEquityAccount = false;
		public static bool IsOpenFromWeb = false;
		public static bool IsNewPortStyle = false;
		public static string UserLoginMode = "I";
		public static string UserSessionID = string.Empty;
		public static string UserPincode = string.Empty;
		public static string UserPincodeLastEntry = string.Empty;
		private static DateTime UserPincodeLastAccess = DateTime.MinValue;
		public static int UserMaxPincodeTimeout = 300;
		public static int UserPincodeWrongCount = 0;
		public static int UserMaxRetryPincode = 3;
		public static string AuthenKey = string.Empty;
		public static decimal UserVAT = 0m;
		public static string UserEfin = string.Empty;
		public static int UserMarginRate = 50;
		public static string NewsHomeLink = "http://www.i2trade.com/NewPortalPage.aspx";
		public static string StockFocusHomeLink = "http://www.set.or.th/set/commonslookup.do";
		public static string NewsSymbolLink = "http://www.set.or.th/set/companynews.do?symbol=XXXXX&language=th&country=TH";
		public static string StockFocusSymbolLink = "http://www.set.or.th/set/companyprofile.do?symbol=XXXXX&language=th&country=TH";
		public static string EserviceServer = string.Empty;
		public static bool IsSupportEservice = false;
		public static string UPDATE_VERSION = "UPDATE_VERSION";
		public static string KE_Session = string.Empty;
		public static string KE_AuthenUrl = string.Empty;
		public static string KE_LOCAL = string.Empty;
		public static string KE_SBL = string.Empty;
		public static string KTZ_Session = string.Empty;
		public static string KTZ_custMapKey = string.Empty;
		public static string ASP_Ticket = string.Empty;
		public static bool Isi2infoLink2 = false;
		public static bool IsAutoGetOrderInfo = false;
		public static int AutoGetOrderInfoInterval = 0;
		public static bool IsOrderBoxFocus = false;
		public static bool SupportOrderTimes = true;
		public static bool SupportFreewill = false;
		public static bool SupportSerisysTFEX = false;
		public static bool IsUseProxy = false;
		public static string ProxyPassword = string.Empty;
		public static bool IsFeedsStarted = false;
		public static bool IsScreen125 = false;
		public static bool IsResumeState = false;
		public static int SendHeartBeatInterval = 30;
		public static bool IsLockSendBox = false;
		private static string currentSymbol = string.Empty;
		private static string currStockInMktWatch = string.Empty;
		private static string marketState = string.Empty;
		private static int marketSession;
		private static DateTime marketTime = DateTime.MinValue;
		public static int MinPasswordLength = 1;
		public static int MinPincodeLength = 4;
		public static AutoCompleteStringCollection StockAutoComp = null;
		private static string[] stockAutoCompStringArr;
		public static bool IsLoadInformation = false;
		public static bool IsAreadyLogin = false;
		public static int BrokerId = 0;
		public static bool IsRiskActive = true;
		public static string SuuportSBL = "N";
		public static string SupportCollateral = "N";
		public static string SuuportSplash = "Y";
		public static bool SupportNAV = false;
		public static bool SupportNod = false;
		public static int PullInterval = 1000;
		public static string PCCanEditorder = "N";
		public static bool SupportReOrderBox = false;
		public static bool IsSupportTfex = false;
		public static bool IsSupportTPBlinkColor = false;
		public static string SupportBatchOrder = "N";
		public static string NewsSymbol = string.Empty;
		public static bool AlertOpen = false;
		public static bool AlertAutoPopup = true;
		public static bool AlertSound = false;
		public static bool IsSupportEfinChart = false;
		public static string UrlEfinChart = string.Empty;
		public static bool IsSupportTradeJournal = false;
		public static bool StopOrderSupported = false;
		public static bool StopOrderAccepted = false;
		public static bool IsPushMode = true;
		public static int TunnelCounter = 0;
		private static List<TunnelInfo> tunnelHosts = null;
		public static string WebUrl = string.Empty;
		public static string WebOrderUrl = string.Empty;
		public static string WebUserUrl = string.Empty;
		public static string WebAlertUrl = string.Empty;
		public static string WebTfexUrl = string.Empty;
		private static i2TradePlus.ITSNetBusinessWS.Service webService = null;
		private static i2TradePlus.ITSNetBusinessWS.Service webOrderService = null;
		private static i2TradePlus.ITSNetBusinessWS.Service webUserService = null;
		private static i2TradePlus.ITSNetBusinessWS.Service webAlertService = null;
		private static i2TradePlus.ITSNetBusinessWSTFEX.Service webServiceTFEX = null;
		public static bool CanRecvSpace = false;
		private static StockList _stockInfo = new StockList();
		private static SeriesList _seriesInfo = new SeriesList();
		private static TFEXIndex indexInfoTfex = new TFEXIndex();
		private static UnderlyingInfo _underlyingInfo = new UnderlyingInfo();
		public static AutoCompleteStringCollection MultiAutoComp = null;
		private static string[] multiAutoCompStringArr;
		public static AutoCompleteStringCollection SeriesAutoComp = null;
		private static string[] seriesAutoCompStringArr;
		private static IndexStat _indexStatInfo = new IndexStat();
		private static string urlSyncHandler = string.Empty;
		public static int FavStockPerPage = 15;
		public static bool FavStockChanged = false;
		public static Dictionary<int, List<string>> FavStockList = null;
		public static List<string> TickerStockList = new List<string>();
		public static Dictionary<string, DateTime> AutoGetOrderNoList = new Dictionary<string, DateTime>();
		public static Dictionary<string, DateTime> AutoGetOrderNoList_TFEX = new Dictionary<string, DateTime>();
		public static List<string> DWParentStockList = new List<string>();
		private static string _ip = string.Empty;
		public static string PINCODE_TIMEOUT = "PINCODE_TIMEOUT";
		public static string WORKING_ROOTPATH
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\i2Template";
			}
		}
		public static AccountInfo AccInfo
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (ApplicationInfo.accInfo == null)
				{
					ApplicationInfo.accInfo = new AccountInfo();
				}
				return ApplicationInfo.accInfo;
			}
		}
		public static string CurrentSymbol
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return ApplicationInfo.currentSymbol;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				try
				{
					if (ApplicationInfo.currentSymbol != value)
					{
						ApplicationInfo.currentSymbol = value;
						if (!ApplicationInfo.IsLockSendBox)
						{
							TemplateManager.Instance.MainForm.SendOrderBox.SetCurrentSymbol(value);
						}
					}
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
		}
		public static string CurrStockInMktWatch
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return ApplicationInfo.currStockInMktWatch;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				ApplicationInfo.currStockInMktWatch = value;
			}
		}
		public static string MarketState
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return ApplicationInfo.marketState;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				ApplicationInfo.marketState = value;
			}
		}
		public static int MarketSession
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return ApplicationInfo.marketSession;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				ApplicationInfo.marketSession = value;
			}
		}
		public static DateTime MarketTime
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return ApplicationInfo.marketTime;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				ApplicationInfo.marketTime = value;
			}
		}
		public static string[] StockAutoCompStringArr
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (ApplicationInfo.stockAutoCompStringArr == null && ApplicationInfo.StockAutoComp != null)
				{
					ApplicationInfo.stockAutoCompStringArr = new string[ApplicationInfo.StockAutoComp.Count];
					ApplicationInfo.StockAutoComp.CopyTo(ApplicationInfo.stockAutoCompStringArr, 0);
				}
				return ApplicationInfo.stockAutoCompStringArr;
			}
		}
		public static List<TunnelInfo> TunnelHosts
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return ApplicationInfo.tunnelHosts;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				ApplicationInfo.tunnelHosts = value;
			}
		}
		public static i2TradePlus.ITSNetBusinessWS.Service WebService
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (ApplicationInfo.webService == null)
				{
					try
					{
						SoapHttpClientProtocol soapHttpClientProtocol = new WebServiceFactory().Create(ITSNetWS.StockService);
						ApplicationInfo.webService = (i2TradePlus.ITSNetBusinessWS.Service)soapHttpClientProtocol;
					}
					catch (Exception ex)
					{
						throw ex;
					}
				}
				return ApplicationInfo.webService;
			}
		}
		public static i2TradePlus.ITSNetBusinessWS.Service WebOrderService
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (ApplicationInfo.webOrderService == null)
				{
					try
					{
						SoapHttpClientProtocol soapHttpClientProtocol = new WebServiceFactory().Create(ITSNetWS.OrderService);
						ApplicationInfo.webOrderService = (i2TradePlus.ITSNetBusinessWS.Service)soapHttpClientProtocol;
					}
					catch (Exception ex)
					{
						throw ex;
					}
				}
				return ApplicationInfo.webOrderService;
			}
		}
		public static i2TradePlus.ITSNetBusinessWS.Service WebUserService
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (ApplicationInfo.webUserService == null)
				{
					try
					{
						SoapHttpClientProtocol soapHttpClientProtocol = new WebServiceFactory().Create(ITSNetWS.UserService);
						ApplicationInfo.webUserService = (i2TradePlus.ITSNetBusinessWS.Service)soapHttpClientProtocol;
					}
					catch (Exception ex)
					{
						throw ex;
					}
				}
				return ApplicationInfo.webUserService;
			}
		}
		public static i2TradePlus.ITSNetBusinessWS.Service WebAlertService
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (ApplicationInfo.webAlertService == null)
				{
					try
					{
						SoapHttpClientProtocol soapHttpClientProtocol = new WebServiceFactory().Create(ITSNetWS.AlertService);
						ApplicationInfo.webAlertService = (i2TradePlus.ITSNetBusinessWS.Service)soapHttpClientProtocol;
					}
					catch (Exception ex)
					{
						throw ex;
					}
				}
				return ApplicationInfo.webAlertService;
			}
		}
		public static i2TradePlus.ITSNetBusinessWSTFEX.Service WebServiceTFEX
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (ApplicationInfo.webServiceTFEX == null)
				{
					try
					{
						SoapHttpClientProtocol soapHttpClientProtocol = new WebServiceFactory().Create(ITSNetWS.TfexService);
						ApplicationInfo.webServiceTFEX = (i2TradePlus.ITSNetBusinessWSTFEX.Service)soapHttpClientProtocol;
					}
					catch (Exception ex)
					{
						throw ex;
					}
				}
				return ApplicationInfo.webServiceTFEX;
			}
		}
		public static StockList StockInfo
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return ApplicationInfo._stockInfo;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				ApplicationInfo._stockInfo = value;
			}
		}
		public static SeriesList SeriesInfo
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return ApplicationInfo._seriesInfo;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				ApplicationInfo._seriesInfo = value;
			}
		}
		public static TFEXIndex IndexInfoTfex
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return ApplicationInfo.indexInfoTfex;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				ApplicationInfo.indexInfoTfex = value;
			}
		}
		public static UnderlyingInfo UnderlyingInfo
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return ApplicationInfo._underlyingInfo;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				ApplicationInfo._underlyingInfo = value;
			}
		}
		public static string[] MultiAutoCompStringArr
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (ApplicationInfo.multiAutoCompStringArr == null && ApplicationInfo.MultiAutoComp != null)
				{
					ApplicationInfo.multiAutoCompStringArr = new string[ApplicationInfo.MultiAutoComp.Count];
					ApplicationInfo.MultiAutoComp.CopyTo(ApplicationInfo.multiAutoCompStringArr, 0);
				}
				return ApplicationInfo.multiAutoCompStringArr;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				ApplicationInfo.multiAutoCompStringArr = value;
			}
		}
		public static string[] SeriesAutoCompStringArr
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (ApplicationInfo.seriesAutoCompStringArr == null && ApplicationInfo.SeriesAutoComp != null)
				{
					ApplicationInfo.seriesAutoCompStringArr = new string[ApplicationInfo.SeriesAutoComp.Count];
					ApplicationInfo.SeriesAutoComp.CopyTo(ApplicationInfo.seriesAutoCompStringArr, 0);
				}
				return ApplicationInfo.seriesAutoCompStringArr;
			}
		}
		public static IndexStat IndexStatInfo
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return ApplicationInfo._indexStatInfo;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				ApplicationInfo._indexStatInfo = value;
			}
		}
		public static string UrlSyncHandler
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return ApplicationInfo.urlSyncHandler;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				ApplicationInfo.urlSyncHandler = value;
			}
		}
		public static string IP
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (ApplicationInfo._ip == string.Empty)
				{
					try
					{
						IPHostEntry hostEntry = Dns.GetHostEntry(Environment.MachineName.ToString());
						IPAddress[] addressList = hostEntry.AddressList;
						for (int i = 0; i < addressList.Length; i++)
						{
							if (!addressList[i].IsIPv6LinkLocal && !addressList[i].IsIPv6Multicast && !addressList[i].IsIPv6SiteLocal && addressList[i].AddressFamily != AddressFamily.InterNetworkV6)
							{
								ApplicationInfo._ip = addressList[i].ToString();
							}
						}
					}
					catch
					{
						ApplicationInfo._ip = "None";
					}
				}
				return ApplicationInfo._ip;
			}
		}
		[DllImport("kernel32.dll")]
		public static extern bool Beep(int freq, int duration);
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string ConvertPrice(string price)
		{
			string text = price;
			if (text != null)
			{
				if (!(text == "MTL"))
				{
					if (text == "MKT")
					{
						price = "MO";
					}
				}
				else
				{
					price = "ML";
				}
			}
			return price;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static bool CanReceiveMessage(string customerAccount)
		{
			return ApplicationInfo.AccInfo.Items.ContainsKey(customerAccount);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void AddOrderNoToAutoRefreshList(string sOrderNo, int actionType)
		{
			try
			{
				if (ApplicationInfo.IsAutoGetOrderInfo)
				{
					object syncRoot;
					Monitor.Enter(syncRoot = ((ICollection)ApplicationInfo.AutoGetOrderNoList).SyncRoot);
					try
					{
						string key = string.Empty;
						if (actionType == 1)
						{
							if (ApplicationInfo.SupportFreewill)
							{
								key = "R" + sOrderNo;
							}
							else
							{
								key = sOrderNo;
							}
						}
						else
						{
							if (actionType == 2)
							{
								key = sOrderNo;
							}
							else
							{
								if (actionType != 3)
								{
									return;
								}
								key = "F" + sOrderNo;
							}
						}
						ApplicationInfo.AutoGetOrderNoList.Add(key, DateTime.Now);
					}
					finally
					{
						Monitor.Exit(syncRoot);
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void RemoveOrderNoFromAutoRefreshList(string sOrderNo, string refOrdNo)
		{
			try
			{
				object syncRoot;
				Monitor.Enter(syncRoot = ((ICollection)ApplicationInfo.AutoGetOrderNoList).SyncRoot);
				try
				{
					if (sOrderNo != string.Empty && ApplicationInfo.AutoGetOrderNoList.ContainsKey(sOrderNo))
					{
						ApplicationInfo.AutoGetOrderNoList.Remove(sOrderNo);
					}
					if (refOrdNo != string.Empty && ApplicationInfo.AutoGetOrderNoList.ContainsKey(refOrdNo))
					{
						ApplicationInfo.AutoGetOrderNoList.Remove(refOrdNo);
					}
				}
				finally
				{
					Monitor.Exit(syncRoot);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void AddOrderNoToAutoRefreshList_TFEX(string sOrderNo)
		{
			try
			{
				if (ApplicationInfo.IsAutoGetOrderInfo)
				{
					Dictionary<string, DateTime> autoGetOrderNoList_TFEX;
					Monitor.Enter(autoGetOrderNoList_TFEX = ApplicationInfo.AutoGetOrderNoList_TFEX);
					try
					{
						if (ApplicationInfo.AutoGetOrderNoList_TFEX.ContainsKey(sOrderNo))
						{
							ApplicationInfo.AutoGetOrderNoList_TFEX.Remove(sOrderNo);
						}
						ApplicationInfo.AutoGetOrderNoList_TFEX.Add(sOrderNo, DateTime.Now);
					}
					finally
					{
						Monitor.Exit(autoGetOrderNoList_TFEX);
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void RemoveOrderNoFromAutoRefreshList_TFEX(string sOrderNo)
		{
			try
			{
				if (ApplicationInfo.AutoGetOrderNoList_TFEX.ContainsKey(sOrderNo))
				{
					Dictionary<string, DateTime> autoGetOrderNoList_TFEX;
					Monitor.Enter(autoGetOrderNoList_TFEX = ApplicationInfo.AutoGetOrderNoList_TFEX);
					try
					{
						ApplicationInfo.AutoGetOrderNoList_TFEX.Remove(sOrderNo);
					}
					finally
					{
						Monitor.Exit(autoGetOrderNoList_TFEX);
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string GetTemplatePathByUser()
		{
			return ApplicationInfo.WORKING_ROOTPATH + "\\PlusTemplates\\" + ApplicationInfo.UserLoginID;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static bool CheckFormCanShowByLoginMode(Type formType)
		{
			return ApplicationInfo.UserLoginMode == "T" || ApplicationInfo.UserLoginMode == "C";
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void SetupUsers(string users, char spliter)
		{
			try
			{
				string[] array = users.Split(new char[]
				{
					spliter
				});
				if (array.Length > 0)
				{
					int num = 0;
					if (ApplicationInfo.IsOpenFromWeb)
					{
						num = 1;
					}
					for (int i = num; i < array.Length; i++)
					{
						string[] array2 = array[i].ToString().Split(new char[]
						{
							':'
						});
						string key = string.Empty;
						AccountInfo.ItemInfo itemInfo = new AccountInfo.ItemInfo();
						if (array2.Length > 1)
						{
							key = array2[1];
							itemInfo.Market = array2[0];
						}
						else
						{
							key = array2[0];
							itemInfo.Market = "E";
						}
						if (array2.Length > 2)
						{
							itemInfo.CanTrade = array2[2];
						}
						else
						{
							itemInfo.CanTrade = "Y";
						}
						if (itemInfo.Market == "E" || (itemInfo.Market == "T" && ApplicationInfo.IsSupportTfex))
						{
							ApplicationInfo.AccInfo.Items.Add(key, itemInfo);
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
		public static string GetTermicalId()
		{
			return "S";
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string GetSession()
		{
			string result;
			if (ApplicationInfo.BrokerId == 4)
			{
				result = ApplicationInfo.KE_Session;
			}
			else
			{
				if (ApplicationInfo.BrokerId == 2)
				{
					result = ApplicationInfo.KTZ_Session;
				}
				else
				{
					if (ApplicationInfo.BrokerId == 8)
					{
						result = ApplicationInfo.ASP_Ticket;
					}
					else
					{
						result = "";
					}
				}
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static ApplicationInfo.SendNewOrderResult SendNewOrder(string symbol, string side, long volume, string price, long publishVol, string condition, int ttf, string deposit)
		{
			ApplicationInfo.SendNewOrderResult sendNewOrderResult = new ApplicationInfo.SendNewOrderResult();
			try
			{
				string data = ApplicationInfo.WebOrderService.SendNewOrder(symbol, side, volume, price, (ApplicationInfo.BrokerId == 4) ? ApplicationInfo.UserLoginID : ApplicationInfo.AccInfo.CurrentAccount, publishVol, condition, ttf.ToString(), ApplicationInfo.AccInfo.CurrInternetUser, ApplicationInfo.GetSession(), "", deposit, ApplicationInfo.AuthenKey, ApplicationInfo.IP, "", "", ApplicationInfo.GetTermicalId());
				using (DataSet dataSet = new DataSet())
				{
					MyDataHelper.StringToDataSet(data, dataSet);
					if (dataSet.Tables.Contains("Results") && dataSet.Tables["Results"].Rows.Count > 0)
					{
						long.TryParse(dataSet.Tables["Results"].Rows[0]["code"].ToString(), out sendNewOrderResult.OrderNo);
						sendNewOrderResult.ResultMessage = dataSet.Tables["Results"].Rows[0]["message"].ToString().Trim();
						if (ApplicationInfo.SupportFreewill)
						{
							sendNewOrderResult.IsFwOfflineOrder = (sendNewOrderResult.ResultMessage == "ok_offline");
						}
						dataSet.Clear();
					}
				}
			}
			catch (Exception ex)
			{
				sendNewOrderResult.OrderNo = -1L;
				sendNewOrderResult.ResultMessage = ex.Message;
			}
			return sendNewOrderResult;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static bool CheckPinTimeout()
		{
			bool result;
			if (ApplicationInfo.UserMaxPincodeTimeout > 0)
			{
				TimeSpan timeSpan = DateTime.Now.Subtract(ApplicationInfo.UserPincodeLastAccess);
				double num = (double)ApplicationInfo.UserMaxPincodeTimeout - timeSpan.TotalSeconds;
				result = (num < 0.0 && !string.IsNullOrEmpty(ApplicationInfo.UserPincodeLastEntry));
			}
			else
			{
				result = false;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static bool VerifyPincode(string pincodeEntry, ref string verifyPinResultStr)
		{
			bool flag = false;
			bool result;
			try
			{
				if (pincodeEntry == string.Empty)
				{
					verifyPinResultStr = "Pin is empty!!!";
					result = false;
					return result;
				}
				ApplicationInfo.UserPincodeLastEntry = pincodeEntry;
				if (ApplicationInfo.UserPincode != string.Empty && pincodeEntry == ApplicationInfo.UserPincode)
				{
					if (ApplicationInfo.UserMaxPincodeTimeout > 0)
					{
						if (ApplicationInfo.CheckPinTimeout())
						{
							verifyPinResultStr = ApplicationInfo.PINCODE_TIMEOUT;
							flag = false;
						}
						else
						{
							flag = true;
						}
					}
					else
					{
						flag = true;
					}
				}
				else
				{
					verifyPinResultStr = ApplicationInfo.WebUserService.VerifyPincode2(ApplicationInfo.UserLoginID, ApplicationInfo.UserPincodeLastEntry, ApplicationInfo.GetSession(), ApplicationInfo.KTZ_custMapKey);
					if (verifyPinResultStr == "ok")
					{
						flag = true;
					}
				}
				if (flag)
				{
					ApplicationInfo.UserPincodeLastAccess = DateTime.Now;
					ApplicationInfo.UserPincode = ApplicationInfo.UserPincodeLastEntry;
					ApplicationInfo.UserPincodeWrongCount = 0;
				}
				else
				{
					ApplicationInfo.UserPincodeWrongCount++;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			result = flag;
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void GetFavListByPage(int favPage, int maxRecord, bool isTfexSigleQuote, ref string setList, ref string tfexList)
		{
			try
			{
				setList = string.Empty;
				tfexList = string.Empty;
				foreach (string current in ApplicationInfo.FavStockList[favPage])
				{
					if (current != string.Empty)
					{
						StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[current];
						if (stockInformation.Number > 0)
						{
							setList += "," + stockInformation.Number;
						}
						else
						{
							if (ApplicationInfo.IsSupportTfex)
							{
								SeriesList.SeriesInformation seriesInformation = ApplicationInfo.SeriesInfo[current];
								if (seriesInformation.Symbol != string.Empty)
								{
									if (isTfexSigleQuote)
									{
										tfexList = tfexList + (",'" + seriesInformation.Symbol) + "'";
									}
									else
									{
										tfexList += "," + seriesInformation.Symbol;
									}
								}
							}
						}
					}
				}
				if (setList != string.Empty)
				{
					setList = setList.Substring(1);
				}
				if (tfexList != string.Empty)
				{
					tfexList = tfexList.Substring(1);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void InitFavStock(string[] fav)
		{
			try
			{
				ApplicationInfo.FavStockList = new Dictionary<int, List<string>>();
				ApplicationInfo.FavStockList.Add(1, new List<string>());
				ApplicationInfo.FavStockList.Add(2, new List<string>());
				ApplicationInfo.FavStockList.Add(3, new List<string>());
				ApplicationInfo.FavStockList.Add(4, new List<string>());
				ApplicationInfo.FavStockList.Add(5, new List<string>());
				for (int i = 0; i < ApplicationInfo.FavStockPerPage; i++)
				{
					ApplicationInfo.FavStockList[1].Add(string.Empty);
					ApplicationInfo.FavStockList[2].Add(string.Empty);
					ApplicationInfo.FavStockList[3].Add(string.Empty);
					ApplicationInfo.FavStockList[4].Add(string.Empty);
					ApplicationInfo.FavStockList[5].Add(string.Empty);
				}
				if (fav != null)
				{
					for (int j = 1; j <= 5; j++)
					{
						string[] array = fav[j - 1].Split(new char[]
						{
							';'
						});
						for (int i = 0; i < array.Length; i++)
						{
							ApplicationInfo.FavStockList[j][i] = array[i];
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
		public static string GetFullNameBroker(int brokerID)
		{
			string text = string.Empty;
			string result;
			try
			{
				if (ApplicationInfo.BrokerId == 7)
				{
					text = "AIRA Securities Public Company Limited.";
				}
				else
				{
					if (ApplicationInfo.BrokerId == 8)
					{
						text = "ASIA PLUS Securities Public Company Limited.";
					}
					else
					{
						if (ApplicationInfo.BrokerId == 10)
						{
							text = "Asia Wealth Securities Company Limited.";
						}
						else
						{
							if (ApplicationInfo.BrokerId == 3)
							{
								text = "RHB OSK SECURITIES (THAILAND) PUBLIC COMPANY LIMITED.";
							}
							else
							{
								if (ApplicationInfo.BrokerId == 11)
								{
									text = "CIMB Securities (Thailand) Co.,Ltd.";
								}
								else
								{
									if (ApplicationInfo.BrokerId == 1)
									{
										text = "CAPITAL NOMURA Securities Public Company Limited.";
									}
									else
									{
										if (ApplicationInfo.BrokerId == 4)
										{
											text = "Maybank Kim Eng Securities (Thailand) Plc.";
										}
										else
										{
											if (ApplicationInfo.BrokerId == 12)
											{
												text = "KKTRADE Securities Company Limited.";
											}
											else
											{
												if (ApplicationInfo.BrokerId == 2)
												{
													text = "KT ZMICO Securities Company Limited.";
												}
												else
												{
													if (ApplicationInfo.BrokerId == 13)
													{
														text = "Land and House Securities Public Company Limited.";
													}
													else
													{
														if (ApplicationInfo.BrokerId != 14)
														{
															result = "";
															return result;
														}
														text = "AEC Securities Public Company Limited";
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			result = text;
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ApplicationInfo()
		{
		}
	}
}
