using i2TradePlus.FixForm;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace i2TradePlus
{
	internal class MainFixedTemplate
	{
		public class ControlClient
		{
			private Type controlType;
			private DockStyle controlDock;
			private Dictionary<string, object> property = null;
			private int formIndex = 0;
			internal Type ControlType
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.controlType;
				}
			}
			internal DockStyle ControlDock
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.controlDock;
				}
			}
			internal Dictionary<string, object> Property
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.property;
				}
			}
			internal int FormIndex
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.formIndex;
				}
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			public ControlClient(Type controlType, DockStyle controlDock, Dictionary<string, object> property)
			{
				this.controlType = controlType;
				this.formIndex = 0;
				this.controlDock = controlDock;
				this.property = property;
			}
		}
		public class TemplateProperty
		{
			private string tempateName = string.Empty;
			private int hotkey;
			internal string TempateName
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.tempateName;
				}
			}
			internal int Hotkey
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.hotkey;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.hotkey = value;
				}
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			public TemplateProperty(string templateName, int hotkey)
			{
				this.tempateName = templateName;
				this.hotkey = hotkey;
			}
		}
		internal const string TOP_ACTIVE = "Top Active";
		internal const string BEST_PROJECTED_OPEN = "Best Projected Open";
		internal const string BEST_PROJECTED_CLOSE = "Best Projected Close";
		internal const string BEST_OPEN = "Best Open Price";
		internal const string MARKET_INFO = "Market Information";
		internal const string SALE_BY_TIME = "Sale by Time";
		internal const string SALE_BY_PRICE = "Sale by Price";
		internal const string VIEW_ODDLOT = "View Oddlot";
		internal const string STOCK_IN_PLAY = "Stock in Play";
		internal const string VIEW_CUSTOMER = "Portfolio";
		internal const string VIEW_ORDER = "View Order";
		internal const string BUY_ORDER = "Buy Order";
		internal const string SELL_ORDER = "Sell Order";
		internal const string SHORTSELL_ORDER = "Short Sell Order";
		internal const string COVER_BUY_ORDER = "Cover Buy Order";
		internal const string ORDER_HISTORY = "Order History";
		internal const string MARKET_WATCH = "Market Watch";
		internal const string TOP_BBO = "Top BBO";
		internal const string STOCK_SUMMARY = "Stock Summary";
		internal const string STOCK_RANKING = "Stock Ranking";
		internal const string STOCK_THRESHOLD = "Stock Threshold";
		internal const string STOCK_HISTORY = "Historical Chart";
		internal const string NEWS_CENTER = "News Center";
		internal const string BATCH_ORDER = "Batch Order";
		internal const string E_SERVICE = "e-Service";
		internal const string AUTOTRADE = "AutoTrade";
		private static List<MainFixedTemplate.TemplateProperty> templateNameList = null;
		private static List<MainFixedTemplate.TemplateProperty> orderTemplateNameList = null;
		[MethodImpl(MethodImplOptions.NoInlining)]
		public MainFixedTemplate()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static List<MainFixedTemplate.ControlClient> GetFixedTemplate(string templateName)
		{
			List<MainFixedTemplate.ControlClient> result;
			if (templateName == "Portfolio")
			{
				result = MainFixedTemplate.Portfolio();
			}
			else
			{
				if (templateName == "View Order")
				{
					result = MainFixedTemplate.ViewOrder();
				}
				else
				{
					if (templateName == "Market Watch")
					{
						result = MainFixedTemplate.MarketWatch();
					}
					else
					{
						if (templateName == "Top BBO")
						{
							result = MainFixedTemplate.TopBBOs();
						}
						else
						{
							if (templateName == "Stock Threshold")
							{
								result = MainFixedTemplate.StockThreshold();
							}
							else
							{
								if (templateName == "Market Information")
								{
									result = MainFixedTemplate.MarketInfo();
								}
								else
								{
									if (templateName == "Historical Chart")
									{
										result = MainFixedTemplate.StockHistory();
									}
									else
									{
										if (templateName == "News Center")
										{
											result = MainFixedTemplate.NewsCenter();
										}
										else
										{
											if (templateName == "Batch Order")
											{
												result = MainFixedTemplate.BatchOrder();
											}
											else
											{
												if (templateName == "e-Service")
												{
													result = MainFixedTemplate.Eservice();
												}
												else
												{
													if (templateName == "Stock Summary")
													{
														result = MainFixedTemplate.StockSummary("");
													}
													else
													{
														if (templateName == "Stock Ranking")
														{
															result = MainFixedTemplate.StockRanking("");
														}
														else
														{
															if (templateName == "AutoTrade")
															{
																result = MainFixedTemplate.AutoTrade();
															}
															else
															{
																result = null;
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
				}
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static List<MainFixedTemplate.ControlClient> NewsCenter()
		{
			return new List<MainFixedTemplate.ControlClient>
			{
				new MainFixedTemplate.ControlClient(typeof(frmBrowser), DockStyle.Fill, new Dictionary<string, object>())
			};
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static List<MainFixedTemplate.ControlClient> BatchOrder()
		{
			return new List<MainFixedTemplate.ControlClient>
			{
				new MainFixedTemplate.ControlClient(typeof(frmSmartOrder), DockStyle.Fill, new Dictionary<string, object>())
			};
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static List<MainFixedTemplate.ControlClient> Portfolio()
		{
			List<MainFixedTemplate.ControlClient> list = new List<MainFixedTemplate.ControlClient>();
			Dictionary<string, object> property = new Dictionary<string, object>();
			list.Add(new MainFixedTemplate.ControlClient(typeof(frmPortfolio), DockStyle.Fill, property));
			return list;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static List<MainFixedTemplate.ControlClient> StockThreshold()
		{
			List<MainFixedTemplate.ControlClient> list = new List<MainFixedTemplate.ControlClient>();
			Dictionary<string, object> property = new Dictionary<string, object>();
			list.Add(new MainFixedTemplate.ControlClient(typeof(frmRiskControl), DockStyle.Fill, property));
			return list;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static List<MainFixedTemplate.ControlClient> MarketInfo()
		{
			List<MainFixedTemplate.ControlClient> list = new List<MainFixedTemplate.ControlClient>();
			Dictionary<string, object> property = new Dictionary<string, object>();
			list.Add(new MainFixedTemplate.ControlClient(typeof(frmMarketInfo), DockStyle.Fill, property));
			return list;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static List<MainFixedTemplate.ControlClient> ViewOrder()
		{
			List<MainFixedTemplate.ControlClient> list = new List<MainFixedTemplate.ControlClient>();
			Dictionary<string, object> property = new Dictionary<string, object>();
			list.Add(new MainFixedTemplate.ControlClient(typeof(frmViewOrder), DockStyle.Fill, property));
			return list;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static List<MainFixedTemplate.ControlClient> MarketWatch()
		{
			List<MainFixedTemplate.ControlClient> list = new List<MainFixedTemplate.ControlClient>();
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("DefaultStock", "BBL");
			list.Add(new MainFixedTemplate.ControlClient(typeof(frmMarketWatch), DockStyle.Fill, dictionary));
			return list;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static List<MainFixedTemplate.ControlClient> StockHistory()
		{
			List<MainFixedTemplate.ControlClient> list = new List<MainFixedTemplate.ControlClient>();
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("DefaultStock", "BBL");
			list.Add(new MainFixedTemplate.ControlClient(typeof(frmStockChart), DockStyle.Fill, dictionary));
			return list;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static List<MainFixedTemplate.ControlClient> Eservice()
		{
			List<MainFixedTemplate.ControlClient> list = new List<MainFixedTemplate.ControlClient>();
			Dictionary<string, object> property = new Dictionary<string, object>();
			list.Add(new MainFixedTemplate.ControlClient(typeof(frmEservice), DockStyle.Fill, property));
			return list;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static List<MainFixedTemplate.ControlClient> TopBBOs()
		{
			List<MainFixedTemplate.ControlClient> list = new List<MainFixedTemplate.ControlClient>();
			Dictionary<string, object> property = new Dictionary<string, object>();
			list.Add(new MainFixedTemplate.ControlClient(typeof(frmTopBBOs), DockStyle.Fill, property));
			return list;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static List<MainFixedTemplate.ControlClient> StockSummary(string page)
		{
			List<MainFixedTemplate.ControlClient> list = new List<MainFixedTemplate.ControlClient>();
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (page != string.Empty)
			{
				dictionary.Add("Page", page);
			}
			dictionary.Add("DefaultStock", "BBL");
			list.Add(new MainFixedTemplate.ControlClient(typeof(frmStockSummary), DockStyle.Fill, dictionary));
			return list;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static List<MainFixedTemplate.ControlClient> StockRanking(string page)
		{
			List<MainFixedTemplate.ControlClient> list = new List<MainFixedTemplate.ControlClient>();
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (page != string.Empty)
			{
				dictionary.Add("Page", page);
			}
			list.Add(new MainFixedTemplate.ControlClient(typeof(frmStockRanking), DockStyle.Fill, dictionary));
			return list;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static List<MainFixedTemplate.ControlClient> AutoTrade()
		{
			List<MainFixedTemplate.ControlClient> list = new List<MainFixedTemplate.ControlClient>();
			Dictionary<string, object> property = new Dictionary<string, object>();
			list.Add(new MainFixedTemplate.ControlClient(typeof(frmAutoTrade), DockStyle.Fill, property));
			return list;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static List<MainFixedTemplate.TemplateProperty> GetFixedTemplateName()
		{
			if (MainFixedTemplate.templateNameList == null)
			{
				MainFixedTemplate.templateNameList = new List<MainFixedTemplate.TemplateProperty>();
			}
			MainFixedTemplate.templateNameList.Clear();
			MainFixedTemplate.templateNameList.Add(new MainFixedTemplate.TemplateProperty("Top BBO", 112));
			MainFixedTemplate.templateNameList.Add(new MainFixedTemplate.TemplateProperty("Market Watch", 113));
			if (!ApplicationInfo.SupportFreewill)
			{
				MainFixedTemplate.templateNameList.Add(new MainFixedTemplate.TemplateProperty("Historical Chart", 131143));
			}
			MainFixedTemplate.templateNameList.Add(new MainFixedTemplate.TemplateProperty("Stock Summary", 114));
			MainFixedTemplate.templateNameList.Add(new MainFixedTemplate.TemplateProperty("Stock Ranking", 115));
			MainFixedTemplate.templateNameList.Add(new MainFixedTemplate.TemplateProperty("Market Information", 116));
			MainFixedTemplate.templateNameList.Add(new MainFixedTemplate.TemplateProperty("News Center", 118));
			return MainFixedTemplate.templateNameList;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static List<MainFixedTemplate.TemplateProperty> GetOrderTemplateName()
		{
			if (MainFixedTemplate.orderTemplateNameList == null)
			{
				MainFixedTemplate.orderTemplateNameList = new List<MainFixedTemplate.TemplateProperty>();
			}
			MainFixedTemplate.orderTemplateNameList.Clear();
			if (ApplicationInfo.UserLoginMode != "I")
			{
				MainFixedTemplate.orderTemplateNameList.Add(new MainFixedTemplate.TemplateProperty("View Order", 117));
				MainFixedTemplate.orderTemplateNameList.Add(new MainFixedTemplate.TemplateProperty("Buy Order", 144));
				MainFixedTemplate.orderTemplateNameList.Add(new MainFixedTemplate.TemplateProperty("Sell Order", 109));
				if (ApplicationInfo.SuuportSBL == "Y")
				{
					MainFixedTemplate.orderTemplateNameList.Add(new MainFixedTemplate.TemplateProperty("Short Sell Order", 111));
					MainFixedTemplate.orderTemplateNameList.Add(new MainFixedTemplate.TemplateProperty("Cover Buy Order", 106));
				}
				MainFixedTemplate.orderTemplateNameList.Add(new MainFixedTemplate.TemplateProperty("Portfolio", 119));
			}
			return MainFixedTemplate.orderTemplateNameList;
		}
	}
}
