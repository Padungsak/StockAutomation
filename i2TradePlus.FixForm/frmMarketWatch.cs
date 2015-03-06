using i2TradePlus.Controls;
using i2TradePlus.Information;
using i2TradePlus.Properties;
using i2TradePlus.Templates;
using ITSNet.Common.BIZ;
using ITSNet.Common.BIZ.RealtimeMessage;
using ITSNet.Common.BIZ.RealtimeMessage.TFEX;
using STIControl;
using STIControl.CustomGrid;
using STIControl.ExpandTableGrid;
using STIControl.SortTableGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace i2TradePlus.FixForm
{
	public class frmMarketWatch : ClientBaseForm, IRealtimeMessage
	{
		private delegate void ShowSplashInfoCallBack(bool visible);
		private delegate void ShowSplashBBOCallBack(bool visible);
		private delegate void UpdateFromSSCallBack(StockList.StockInformation realtimeStockInfo);
		private delegate void DrawTPBlinkColorCallBack(LSAccumulate msgLS);
		private delegate void UpdateOpenOrProjectOpenPriceCallBack(string state, int session, decimal price);
		private delegate void UpdateProjectedVolumeCallBack(long volume);
		private delegate void UpdateProjectedClosePriceCallBack(decimal closePrice);
		private delegate void UpdateAllVolumeCallBack(int deals, long accVolume, decimal accValue, long openVolume, long buyVolume, long sellVolume);
		private delegate void UpdateSectorCallBack(decimal price, decimal chg, decimal pchg);
		private delegate void UpdateLastPriceCallBack(decimal lastPrice, string comparePrice);
		private delegate void UpdatePriceInfoCallBack(decimal lastPrice, decimal high, decimal low);
		private delegate void UpdateLastSaleTickerInfoCallBack(decimal price, string side, long volume, string lastUpdate, int index);
		private delegate void UpdateMainBoardValueCallBack(int deals, long accVolume, decimal accValue);
		private delegate void UpdateBigLotValueCallBack(int bDeal, decimal bValue, long bVolume);
		private delegate void SetNewStockInfoCallBack(string stockSymbol, bool isFocus);
		private delegate void UpdateBBOTopPriceCallBack(int rowIndex, string side, decimal price, long volume, decimal prior, decimal lastSalePrice, StockList.StockInformation sf);
		private delegate void UpdateBBO_LS_CallBack(int rowIndex, decimal lastPrice, long accVolume, decimal accValue, int deals, string comparePrice, decimal highPrice, decimal lowPrice, long buyVol, long sellVol, StockList.StockInformation sf);
		public delegate void ShowDisplayFlagBBOCallBack(int rowIndex, string displayFlag);
		private delegate void ClearStockBBOCallBack(int lineNo);
		public delegate void ShowTextBoxPositionCallBack(KeyEventArgs e);
		private delegate void SetBBOPageCallBack(string page);
		private delegate void ShowSplashChartCallBack(bool visible);
		private delegate void setTopPriceColumeCallBack(bool isEquity);
		private delegate void setBBOColumeCallBack(bool isEquity);
		private delegate void UpdateHeaderPriceCallBack(decimal highPrice, decimal lowPrice);
		private delegate void UpdateTickerTFEXInfoCallBack(decimal price, string side, long volume, string lastUpdate, int index);
		private delegate void UpdateTfexAvgCallBack(decimal avg);
		public delegate void UpdateTopPriceBBOOptionsCallBack(int rowIndex, string type, string side, string volume, string price, SeriesList.SeriesInformation seriesInfoForOptionCrtl);
		public delegate void UpdateLastPriceBBOOptionsCallBack(int rowIndex, string type, decimal lastPrice, decimal chg, SeriesList.SeriesInformation stockInfo);
		public delegate void UpdateLastPriceBBO_TFEXCallBack(int rowIndex, decimal lastPrice, decimal prior, long accVolume, decimal accValue, long deals, string comparePrice, SeriesList.SeriesInformation serieInfo, decimal highPrice, decimal lowPrice, long buyVol, long sellVol);
		public delegate void UpdateTopPriceBBOTFEXCallBack(int rowIndex, string side, string price, long volume, decimal prior, decimal lastSalePrice, SeriesList.SeriesInformation serieInfo);
		private delegate void SwitchColumnsCallBack(string currColumns);
		private const string MGROUP_FAVOURIT = "Favorites";
		private const string MGROUP_SET = "SET";
		private const string MGROUP_DW = "DerivativeWarrant";
		private const string MGROUP_FUTURES = "Futures";
		private const string MGROUP_OPTION = "Option";
		private const string MOSTACTIVE_VALUE_PAGE = "Most Active Value";
		private const string MOSTACTIVE_VOLUME_PAGE = "Most Active Volume";
		private const string MOSTACTIVE_WARRANT_PAGE = "Most Active Warrant";
		private const string GAINER_PAGE = "Top Gainer";
		private const string LOSER_PAGE = "Top Loser";
		private const string SWING_PAGE = "Top Swing";
		private const string BEST_PREOPEN_PAGE = "Top Projected Open";
		private const string BEST_PRECLOSE_PAGE = "Top Projected Close";
		private const string BEST_OPEN1_PAGE = "Top Open Price-1";
		private const string BEST_OPEN2_PAGE = "Top Open Price-2";
		private const string BIG_LOT_PAGE = "Big-Lot";
		private const string BENEFIT_PAGE = "Benefit";
		private const string TURN_OVER_PAGE = "Turnover List";
		private const string SUBGROUP_SET_SECTOR = "Set Sector";
		private const string SUBGROUP_FUTURES_INSTRUMENT = "Futures Instrument";
		private const string SUBGROUP_OPTION_SET50 = "SET50Option";
		private const string ITEM_FAV_1 = "Favorites-1";
		private const string ITEM_FAV_2 = "Favorites-2";
		private const string ITEM_FAV_3 = "Favorites-3";
		private const string ITEM_FAV_4 = "Favorites-4";
		private const string ITEM_FAV_5 = "Favorites-5";
		private const string ITEM_FUT_MOSTACTIVE_VALUE = "Futures - Most Active Value";
		private const string ITEM_FUT_VOLUME = "Futures - Most Active Volume";
		private const string ITEM_FUT_GAINER = "Futures - Gainer";
		private const string ITEM_FUT_LOSER = "Futures - Loser";
		private const string ITEM_FUT_SWING = "Futures - Most Swing";
		private const string ITEM_OPT_MOSTACTIVE_VALUE = "Options - Most Active Value";
		private const string ITEM_OPT_MOSTACTIVE_VOLUME = "Options - Most Active Volume";
		private const string ITEM_OPT_GAINER = "Options - Gainer";
		private const string ITEM_OPT_LOSER = "Options - Loser";
		private const string ITEM_OPT_SWING = "Options - Most Swing";
		private const string ITEM_SECTOR_SORT_VALUE = "Value";
		private const string ITEM_SECTOR_SORT_SYMBOL = "Stock";
		private const string ITEM_SECTOR_SORT_CHG = "%Change";
		private const int _bboGridMaxRecord = 40;
		private const int _bboGridMinRecord = 15;
		private BackgroundWorker bgwReloadData = null;
		private BackgroundWorker bgwReloadDataBBO = null;
		private BackgroundWorker bgwReloadChart = null;
		private BackgroundWorker bgwReloadBBOExp = null;
		private bool _currentIsSET = true;
		private StockList.StockInformation _stockInfoSET = null;
		private SeriesList.SeriesInformation _seriesInfoTFEX = null;
		private DataSet tdsMainInfo = null;
		private DataSet tdsSector = null;
		private DataSet tdsInstrument = null;
		private DataSet tdsSet50Option = null;
		private DataSet tdsTopActive = null;
		private DataSet tdsBBOFavSET = null;
		private DataSet tdsBBOFavTFEX = null;
		private string _checkSpread = string.Empty;
		private string _currentStock = string.Empty;
		private List<string> _itemsUnderlying = new List<string>();
		private bool isInfoLoading = false;
		private bool isBBOLoading = false;
		private int _bboPage = 0;
		private string _mainGroupType = string.Empty;
		private string _subGroupType = string.Empty;
		private string _currentBBOpage = string.Empty;
		private Dictionary<string, string> _set50OptList = null;
		private string _lastFAV = string.Empty;
		private string _lastSETsel = string.Empty;
		private string _lastDWsel = string.Empty;
		private string _lastFuturesSel = string.Empty;
		private string _lastOptionsSel = string.Empty;
		private string _lastSectorSortSel = string.Empty;
		private string _lastDWSortSel = string.Empty;
		private string _bboOptionsHeaderText = string.Empty;
		private string _bboQuerySymbol = string.Empty;
		private string _bboQuerySymbolTFEX = string.Empty;
		private string _bboQuerySymbolSector = string.Empty;
		private bool _selectNewSector = false;
		private bool _selectNewDW = false;
		private string _colsEdit = string.Empty;
		private long _bVol1;
		private long _bVol2;
		private long _bVol3;
		private long _bVol4;
		private long _bVol5;
		private long _ofVol1;
		private long _ofVol2;
		private long _ofVol3;
		private long _ofVol4;
		private long _ofVol5;
		private string _sectorSymbol = string.Empty;
		private bool _expCurrentIsSET = true;
		private DataSet tdsBBOExp = null;
		private int _expStockNo = 0;
		private int _expRowId = -1;
		private int _expRows = 0;
		private string _expSeries = "";
		private Timer tmTest = null;
		private bool _isDWGroup = false;
		private string _currentParentStock = string.Empty;
		private bool _chartVisible = false;
		private frmColumnEditor _frmColEdit = null;
		private bool _bboFocused = false;
		private string _isTT = string.Empty;
		private BackgroundWorker _bgwVolAs = null;
		private bool _volAsVisible = false;
		private DataSet dsSaleByPrice = null;
		private IContainer components = null;
		private IntzaCustomGrid intzaInfo;
		private ToolStrip tStripMenu;
		private ToolStripLabel tsStockLable;
		private ToolStripComboBox tstbStock;
		private ToolStripLabel tsPrice;
		private ToolStripLabel tsSectorName;
		private ToolStripLabel tsSectorIndex;
		private ToolStripLabel tslbCompare;
		private Label lbSplashInfo;
		private ToolStripButton tsbtnSwitchChart;
		private TextBox tbStockBBO;
		private Label lbBBOLoading;
		private Panel panelBidOffer;
		private ExpandGrid intzaBBO;
		private SortGrid intzaVolumeByBoard;
		private ContextMenuStrip contextMenuStrip1;
		private ToolStripMenuItem tsmiShow3BO;
		private ToolStripMenuItem tsmiShowBestBO;
		private ToolStripMenuItem tsmiShow5BO;
		private PictureBox pictureBox1;
		private ToolStripButton tsbtnRefreshChart;
		private Label lbChartLoading;
		private IntzaCustomGrid intzaBF;
		private ContextMenuStrip contextLink;
		private ToolStripMenuItem tsmCallHistoricalChart;
		private ToolStripMenuItem tsmCallStockInPlay;
		private ToolStripMenuItem tsmCallSaleByPrice;
		private ToolStripMenuItem tsmCallSaleByTime;
		private ToolStripMenuItem tsmCallOddlot;
		private SortGrid intzaLS2;
		private SortGrid intzaTP;
		private ToolStrip tStripBBO;
		private ToolStripLabel tslbSelection;
		private ToolStripComboBox tscbBBOSelection;
		private ToolStripButton tsbtnBBOAddStock;
		private ToolStripButton tsbtnBBODelStock;
		private ToolStripLabel tslbSortBy;
		private ToolStripButton tsbtnColumnSetup;
		private ToolStripMenuItem tsmCallNews;
		private ToolStripButton tsbtnSETNews;
		private Button btnCloseChart;
		private ToolStripButton tsbtnHChart;
		private IntzaCustomGrid intzaInfoTFEX;
		private ToolStripButton tsbtnBBO_FAV;
		private ToolStripButton tsbtnBBO_SET;
		private ToolStripButton tsbtnBBO_FUTURE;
		private ToolStripButton tsbtnBBO_Option;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripLabel tslbTfexHigh;
		private ToolStripLabel tstbTfexHigh;
		private ToolStripLabel tslbTfexLow;
		private ToolStripLabel tstbTfexLow;
		private ToolStripLabel tslbTfexAvg;
		private ToolStripLabel tstbTfexAvg;
		private ExpandGrid intzaOption;
		private ToolStrip tStripCP;
		private ToolStripLabel tStripCall;
		private ToolStripLabel tStripPUT;
		private ToolTip toolTip1;
		private ToolStripSeparator toolStripMenuItem1;
		private ToolStripButton tsbtnVolAs;
		private ucVolumeAtPrice wcGraphVolume;
		private Panel panelVolAs;
		private Button btnVolAsClose;
		private ToolStripButton tsbtnBBODW;
		private ToolStripComboBox tscbSortByDW;
		private bool IsInfoLoading
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isInfoLoading;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.isInfoLoading = value;
				if (!base.DesignMode)
				{
					this.ShowSplashInfo(value);
				}
			}
		}
		private bool IsBBOLoading
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isBBOLoading;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.isBBOLoading = value;
				if (!base.DesignMode)
				{
					this.ShowSplashBBO(value);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowSplashInfo(bool visible)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new frmMarketWatch.ShowSplashInfoCallBack(this.ShowSplashInfo), new object[]
				{
					visible
				});
			}
			else
			{
				if (!base.DesignMode)
				{
					if (ApplicationInfo.SuuportSplash == "Y")
					{
						if (visible)
						{
							this.lbSplashInfo.Left = this.intzaInfo.Left + (this.intzaInfo.Width - this.lbBBOLoading.Width) / 2;
							this.lbSplashInfo.Top = this.intzaInfo.Top + (this.intzaInfo.Height - this.lbBBOLoading.Height) / 2;
							this.lbSplashInfo.Visible = true;
							this.lbSplashInfo.BringToFront();
						}
						else
						{
							this.lbSplashInfo.Visible = false;
						}
					}
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowSplashBBO(bool visible)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new frmMarketWatch.ShowSplashBBOCallBack(this.ShowSplashBBO), new object[]
				{
					visible
				});
			}
			else
			{
				if (!base.DesignMode)
				{
					if (ApplicationInfo.SuuportSplash == "Y")
					{
						if (visible)
						{
							this.lbBBOLoading.Left = this.panelBidOffer.Left + (this.panelBidOffer.Width - this.lbBBOLoading.Width) / 2;
							this.lbBBOLoading.Top = this.panelBidOffer.Top + (this.panelBidOffer.Height - this.lbBBOLoading.Height) / 2;
							this.lbBBOLoading.Visible = true;
							this.lbBBOLoading.BringToFront();
						}
						else
						{
							this.lbBBOLoading.Visible = false;
						}
					}
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmMarketWatch()
		{
			this.InitializeComponent();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmMarketWatch(Dictionary<string, object> propertiesValue) : base(propertiesValue)
		{
			try
			{
				this.InitializeComponent();
				this.tstbStock.Sorted = true;
				this.tstbStock.AutoCompleteMode = AutoCompleteMode.Suggest;
				this.tstbStock.AutoCompleteSource = AutoCompleteSource.ListItems;
				this.tbStockBBO.AutoCompleteMode = AutoCompleteMode.Suggest;
				this.tbStockBBO.AutoCompleteSource = AutoCompleteSource.CustomSource;
				if (ApplicationInfo.IsSupportTfex)
				{
					this.tstbStock.Items.AddRange(ApplicationInfo.MultiAutoCompStringArr);
					this.tbStockBBO.AutoCompleteCustomSource = ApplicationInfo.MultiAutoComp;
				}
				else
				{
					this.tstbStock.Items.AddRange(ApplicationInfo.StockAutoCompStringArr);
					this.tbStockBBO.AutoCompleteCustomSource = ApplicationInfo.StockAutoComp;
				}
				this.bgwReloadData = new BackgroundWorker();
				this.bgwReloadData.WorkerReportsProgress = true;
				this.bgwReloadData.DoWork += new DoWorkEventHandler(this.bgwReloadData_DoWork);
				this.bgwReloadData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwReloadData_RunWorkerCompleted);
				this.bgwReloadDataBBO = new BackgroundWorker();
				this.bgwReloadDataBBO.WorkerReportsProgress = true;
				this.bgwReloadDataBBO.DoWork += new DoWorkEventHandler(this.bgwReloadDataBBO_DoWork);
				this.bgwReloadDataBBO.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwReloadDataBBO_RunWorkerCompleted);
				this.bgwReloadBBOExp = new BackgroundWorker();
				this.bgwReloadBBOExp.WorkerReportsProgress = true;
				this.bgwReloadBBOExp.DoWork += new DoWorkEventHandler(this.bgwReloadBBOExp_DoWork);
				this.bgwReloadBBOExp.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwReloadBBOExp_RunWorkerCompleted);
				this.bgwReloadChart = new BackgroundWorker();
				this.bgwReloadChart.WorkerReportsProgress = true;
				this.bgwReloadChart.DoWork += new DoWorkEventHandler(this.bgwReloadChart_DoWork);
				this.bgwReloadChart.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwReloadChart_RunWorkerCompleted);
				this._chartVisible = false;
				this.pictureBox1.Hide();
				this.tsbtnRefreshChart.Visible = false;
				this.tsbtnBBOAddStock.Visible = false;
				this.tsbtnBBODelStock.Visible = false;
				this.SetBlinkModeTopPrice();
				this.intzaVolumeByBoard.Records(0).Fields("h1").Text = "Main";
				this.intzaVolumeByBoard.Records(1).Fields("h1").Text = "Biglot";
				this.intzaVolumeByBoard.Records(0).Fields("h1").FontColor = Color.LightGray;
				this.intzaVolumeByBoard.Records(1).Fields("h1").FontColor = Color.LightGray;
				if (ApplicationInfo.SupportFreewill)
				{
					this.intzaInfo.Item("lbMarginRate").Visible = false;
					this.intzaInfo.Item("tbMarginRate").Visible = false;
				}
				if (!string.IsNullOrEmpty(this._colsEdit))
				{
					string[] array = this._colsEdit.Split(new char[]
					{
						'|'
					});
					List<STIControl.ExpandTableGrid.ColumnItem> list = new List<STIControl.ExpandTableGrid.ColumnItem>();
					STIControl.ExpandTableGrid.ColumnItem[] array2 = new STIControl.ExpandTableGrid.ColumnItem[this.intzaBBO.Columns.Count];
					this.intzaBBO.Columns.CopyTo(array2);
					string[] array3 = array;
					for (int i = 0; i < array3.Length; i++)
					{
						string b = array3[i];
						for (int j = 0; j < array2.Length; j++)
						{
							if (array2[j] != null && array2[j].Name == b)
							{
								array2[j].Visible = true;
								list.Add(array2[j]);
								array2[j] = null;
								break;
							}
						}
					}
					for (int j = 0; j < array2.Length; j++)
					{
						if (array2[j] != null)
						{
							array2[j].Visible = false;
							list.Add(array2[j]);
						}
					}
					this._colsEdit = string.Empty;
					this.intzaBBO.Columns.Clear();
					this.intzaBBO.Columns = list;
					this.tslbTfexHigh.Visible = false;
					this.tstbTfexHigh.Visible = false;
					this.tslbTfexLow.Visible = false;
					this.tstbTfexLow.Visible = false;
					this.tslbTfexAvg.Visible = false;
					this.tstbTfexAvg.Visible = false;
					this.intzaOption.Visible = false;
				}
				if (!ApplicationInfo.IsSupportTfex)
				{
					this.tsbtnBBO_FUTURE.Visible = false;
					this.tsbtnBBO_Option.Visible = false;
				}
				if (ApplicationInfo.SupportFreewill)
				{
					this.tsbtnBBODW.Visible = false;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("frmMarketWatch", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override Dictionary<string, object> DoPackProperties()
		{
			try
			{
				base.PropertiesValue.Clear();
				base.PropertiesValue.Add("CurrentStock", this._currentStock);
				base.PropertiesValue.Add("ActiveGroupType", this._mainGroupType);
				base.PropertiesValue.Add("SelectionText", this._currentBBOpage);
				string text = string.Empty;
				foreach (STIControl.ExpandTableGrid.ColumnItem current in this.intzaBBO.Columns)
				{
					if (current.Visible)
					{
						text = text + "|" + current.Name;
					}
				}
				if (text != string.Empty)
				{
					text = text.Substring(1);
				}
				base.PropertiesValue.Add("Cols2", text);
				text = null;
			}
			catch (Exception ex)
			{
				this.ShowError("MarketWatch.DoPackProperties", ex);
			}
			return base.PropertiesValue;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void DoUnpackProperties()
		{
			try
			{
				if (base.PropertiesValue != null)
				{
					if (base.PropertiesValue.ContainsKey("CurrentStock"))
					{
						ApplicationInfo.CurrentSymbol = base.PropertiesValue["CurrentStock"].ToString();
					}
					else
					{
						if (this._currentStock == string.Empty)
						{
							if (base.PropertiesValue.ContainsKey("DefaultStock"))
							{
								ApplicationInfo.CurrentSymbol = base.PropertiesValue["DefaultStock"].ToString();
							}
						}
					}
					if (base.PropertiesValue.ContainsKey("SelectionText"))
					{
						this._currentBBOpage = base.PropertiesValue["SelectionText"].ToString();
					}
					if (base.PropertiesValue.ContainsKey("ActiveGroupType"))
					{
						this._mainGroupType = base.PropertiesValue["ActiveGroupType"].ToString();
					}
					if (base.PropertiesValue.ContainsKey("Cols2"))
					{
						this._colsEdit = base.PropertiesValue["Cols2"].ToString();
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("MarketWatch.DoUnpackProperties", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmMarketWatch_IDoShownDelay()
		{
			this.IsInfoLoading = true;
			this.IsBBOLoading = true;
			this.SetResize(true, true);
			base.Show();
			this.IsInfoLoading = false;
			base.OpenedForm();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmMarketWatch_IDoLoadData()
		{
			try
			{
				if (ApplicationInfo.CurrentSymbol == string.Empty)
				{
					ApplicationInfo.CurrentSymbol = "BBL";
				}
				if (ApplicationInfo.MarketState == "S" || ApplicationInfo.MarketState == "P" || ApplicationInfo.MarketState == "B")
				{
					this.SwitchColumns("po");
				}
				else
				{
					if (ApplicationInfo.MarketState == "M" || ApplicationInfo.MarketState == "R")
					{
						this.SwitchColumns("pc");
					}
					else
					{
						this.SwitchColumns("avg");
					}
				}
				this.SetNewStock_Info(ApplicationInfo.CurrentSymbol, true);
				if (!ApplicationInfo.IsSupportTfex)
				{
					if (this._mainGroupType == "Futures" || this._mainGroupType == "Option")
					{
						this._mainGroupType = "Favorites";
						this._currentBBOpage = "Favorites-1";
					}
				}
				foreach (KeyValuePair<int, UnderlyingInfo.UnderlyingList> current in ApplicationInfo.UnderlyingInfo.Items)
				{
					this._itemsUnderlying.Add("." + current.Value.Symbol + " Futures");
				}
				this.SetBBOGroup(this._mainGroupType, this._currentBBOpage);
				this.IsBBOLoading = false;
			}
			catch (Exception ex)
			{
				this.ShowError("frmMarketWatch_IDoLoadData", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmMarketWatch_IDoCustomSizeChanged()
		{
			if (!this.IsInfoLoading)
			{
				this.SetResize(this.IsWidthChanged, this.IsHeightChanged);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmMarketWatch_IDoReActivated()
		{
			if (!this.IsInfoLoading)
			{
				this.SetResize(this.IsWidthChanged, this.IsHeightChanged);
				base.Show();
				this.SetNewStock_Info(ApplicationInfo.CurrentSymbol, true);
				if (this._mainGroupType == "Favorites")
				{
					if (ApplicationInfo.FavStockChanged)
					{
						this.SetBBOPage(this._currentBBOpage);
					}
				}
				this.tstbStock.Focus();
				this.tstbStock.SelectAll();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmMarketWatch_IDoFontChanged()
		{
			if (!this.IsInfoLoading)
			{
				this.SetResize(true, true);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmMarketWatch_IDoMainFormKeyUp(KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
			case Keys.Space:
				this.tstbStock.Focus();
				this.tstbStock.SelectAll();
				break;
			case Keys.End:
				e.SuppressKeyPress = true;
				break;
			case Keys.Home:
				if (!this.tbStockBBO.Visible)
				{
					this.ShowTextBoxPosition(e);
				}
				else
				{
					this.tbStockBBO.Hide();
				}
				break;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmMarketWatch_IDoSymbolLinked(object sender, SymbolLinkSource source, string newStock)
		{
			if (source == SymbolLinkSource.StockSymbol)
			{
				this.SetNewStock_Info(newStock, false);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwReloadData_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				if (this.FormState != ClientBaseForm.ClientBaseFormState.Closing)
				{
					this.IsInfoLoading = true;
					if (this.tdsMainInfo != null)
					{
						this.tdsMainInfo.Clear();
						this.tdsMainInfo.Dispose();
					}
					this.tdsMainInfo = new DataSet();
					string text = string.Empty;
					if (this._currentIsSET)
					{
						text = ApplicationInfo.WebService.StockByPricePage(this._stockInfoSET.Number, this.intzaLS2.Rows);
					}
					else
					{
						text = ApplicationInfo.WebServiceTFEX.SeriesByPricePage(this._seriesInfoTFEX.Symbol, this._seriesInfoTFEX.SeriesType, this.intzaLS2.Rows);
					}
					if (!string.IsNullOrEmpty(text))
					{
						MyDataHelper.StringToDataSet(text, this.tdsMainInfo);
					}
					this.ReloadChart();
					this.ReloadVolAs();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("bgwReloadData_DoWork", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwReloadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				if (this.FormState != ClientBaseForm.ClientBaseFormState.Closing)
				{
					if (this._currentIsSET)
					{
						this.UpdateToControl();
					}
					else
					{
						this.UpdateToControl_TFEX();
					}
					this.tdsMainInfo.Clear();
					this.IsInfoLoading = false;
				}
			}
			catch (Exception ex)
			{
				this.IsInfoLoading = false;
				this.ShowError("SecurityInfo:RunWorkerCompleted", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReceiveMessage(IBroadcastMessage message, StockList.StockInformation realtimeStockInfo)
		{
			try
			{
				if (!this.IsInfoLoading && this._currentIsSET)
				{
					this.ReceiveMessageInfo(message, realtimeStockInfo);
				}
				if (!this.IsBBOLoading)
				{
					if (this._mainGroupType == "SET" || this._mainGroupType == "Favorites" || this._mainGroupType == "DerivativeWarrant")
					{
						this.ReceiveMessageBBO(message, realtimeStockInfo);
					}
				}
				if (message.MessageType == "SC")
				{
					if (this._mainGroupType == "SET" || this._mainGroupType == "Favorites")
					{
						if (ApplicationInfo.MarketState == "M" || ApplicationInfo.MarketState == "R" || ApplicationInfo.MarketState == "C")
						{
							this.SwitchColumns("pc");
						}
						else
						{
							this.SwitchColumns("po");
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("ReceiveMessage", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateFromSS(StockList.StockInformation realtimeStockInfo)
		{
			if (this.intzaInfo.InvokeRequired)
			{
				this.intzaInfo.Invoke(new frmMarketWatch.UpdateFromSSCallBack(this.UpdateFromSS), new object[]
				{
					realtimeStockInfo
				});
			}
			else
			{
				try
				{
					this.intzaInfo.Item("ceiling").Text = realtimeStockInfo.Ceiling.ToString();
					this.intzaInfo.Item("floor").Text = realtimeStockInfo.Floor.ToString();
					this.intzaInfo.Item("prior").Text = Utilities.PriceFormat(realtimeStockInfo.PriorPrice, 2, 0);
					this.intzaInfo.Item("flag").Text = realtimeStockInfo.DisplayFlag;
				}
				catch (Exception ex)
				{
					this.ShowError("UpdateFromSS", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ReceiveMessageInfo(IBroadcastMessage message, StockList.StockInformation realtimeStockInfo)
		{
			try
			{
				if (this._stockInfoSET != null)
				{
					if (realtimeStockInfo != null)
					{
						string messageType = message.MessageType;
						if (messageType != null)
						{
							if (!(messageType == "TP"))
							{
								if (!(messageType == "L+"))
								{
									if (!(messageType == "PO"))
									{
										if (!(messageType == "SS"))
										{
											if (messageType == "PD")
											{
												if (realtimeStockInfo.Number == this._stockInfoSET.Number)
												{
													PDMessage pDMessage = (PDMessage)message;
													this.UpdateBigLotValue(pDMessage.BiglotDeals, pDMessage.BiglotAccValue, pDMessage.BiglotAccVolume);
													if (base.IsAllowRender)
													{
														this.intzaVolumeByBoard.EndUpdate();
													}
												}
											}
										}
										else
										{
											if (realtimeStockInfo.Number == this._stockInfoSET.Number)
											{
												this.UpdateFromSS(realtimeStockInfo);
												if (base.IsAllowRender)
												{
													this.intzaInfo.EndUpdate();
												}
												if (ApplicationInfo.MarketState == "S")
												{
													this.UpdateLastPrice(realtimeStockInfo.PriorPrice, "");
												}
											}
										}
									}
									else
									{
										if (realtimeStockInfo.Number == this._stockInfoSET.Number)
										{
											POMessage pOMessage = (POMessage)message;
											if (ApplicationInfo.MarketState == "M")
											{
												this.UpdateProjectedClosePrice(pOMessage.ProjectedPrice);
											}
											else
											{
												this.UpdateOpenOrProjectOpenPrice(ApplicationInfo.MarketState, ApplicationInfo.MarketSession, pOMessage.ProjectedPrice);
											}
											this.UpdateProjectedVolume(pOMessage.ProjectedVolume);
											if (base.IsAllowRender)
											{
												this.intzaInfo.EndUpdate();
											}
										}
									}
								}
								else
								{
									if (realtimeStockInfo.Number == this._stockInfoSET.Number)
									{
										LSAccumulate lSAccumulate = (LSAccumulate)message;
										this.UpdateLastPrice(lSAccumulate.LastPrice, lSAccumulate.ComparePrice);
										this.UpdatePriceInfo(lSAccumulate.LastPrice, realtimeStockInfo.HighPrice, realtimeStockInfo.LowPrice);
										this.UpdateAllVolume(lSAccumulate.DealInMain, lSAccumulate.AccVolume * (long)this._stockInfoSET.BoardLot, lSAccumulate.AccValue, lSAccumulate.OpenVolume * (long)this._stockInfoSET.BoardLot, lSAccumulate.BuyVolume * (long)this._stockInfoSET.BoardLot, lSAccumulate.SellVolume * (long)this._stockInfoSET.BoardLot);
										if (lSAccumulate.Side == string.Empty)
										{
											this.UpdateOpenOrProjectOpenPrice(ApplicationInfo.MarketState, ApplicationInfo.MarketSession, lSAccumulate.LastPrice);
										}
										this.UpdateMainBoardValue(lSAccumulate.DealInMain, lSAccumulate.AccVolume * (long)this._stockInfoSET.BoardLot, lSAccumulate.AccValue);
										if (base.IsAllowRender)
										{
											this.intzaInfo.EndUpdate();
											this.intzaVolumeByBoard.EndUpdate();
										}
										this.UpdateStockTicker(lSAccumulate.LastPrice, lSAccumulate.Side, Convert.ToInt64(lSAccumulate.Volume * (long)this._stockInfoSET.BoardLot), lSAccumulate.LastTime, -1);
										if (base.IsAllowRender)
										{
											this.intzaLS2.Redraw();
										}
										this.DrawTPBlinkColor(lSAccumulate);
										if (this._volAsVisible)
										{
											if (lSAccumulate.Side == "B")
											{
												this.wcGraphVolume.UpdateData((double)lSAccumulate.LastPrice, lSAccumulate.Volume * (long)realtimeStockInfo.BoardLot, lSAccumulate.Volume * (long)realtimeStockInfo.BoardLot, 0L);
											}
											else
											{
												if (lSAccumulate.Side == "S")
												{
													this.wcGraphVolume.UpdateData((double)lSAccumulate.LastPrice, lSAccumulate.Volume * (long)realtimeStockInfo.BoardLot, 0L, lSAccumulate.Volume * (long)realtimeStockInfo.BoardLot);
												}
												else
												{
													this.wcGraphVolume.UpdateData((double)lSAccumulate.LastPrice, lSAccumulate.Volume * (long)realtimeStockInfo.BoardLot, 0L, 0L);
												}
											}
											if (base.IsAllowRender)
											{
												this.wcGraphVolume.EndUpdate();
											}
										}
									}
								}
							}
							else
							{
								if (realtimeStockInfo.Number == this._stockInfoSET.Number)
								{
									TPMessage tPMessage = (TPMessage)message;
									this.UpdateTopPrice(tPMessage.Side, tPMessage.Price1, tPMessage.Price2, tPMessage.Price3, tPMessage.Price4, tPMessage.Price5, tPMessage.Volume1 * (long)this._stockInfoSET.BoardLot, tPMessage.Volume2 * (long)this._stockInfoSET.BoardLot, tPMessage.Volume3 * (long)this._stockInfoSET.BoardLot, tPMessage.Volume4 * (long)this._stockInfoSET.BoardLot, tPMessage.Volume5 * (long)this._stockInfoSET.BoardLot);
									if (base.IsAllowRender)
									{
										this.intzaTP.EndUpdate();
										this.intzaBF.Redraw();
									}
								}
							}
						}
					}
					else
					{
						if (message.MessageType == "IE")
						{
							IEMessage iEMessage = (IEMessage)message;
							if (iEMessage.Symbol == this._sectorSymbol && iEMessage.OriginalNumber == this._stockInfoSET.SectorNumber)
							{
								IndexStat.IndexItem sector = ApplicationInfo.IndexStatInfo.GetSector(this._stockInfoSET.SectorNumber);
								this.UpdateSector(iEMessage.IndexValue, sector.IndexChg, sector.IndexChgPct);
							}
						}
						else
						{
							if (message.MessageType == "SC")
							{
								this.UpdateProjectedVolume(-1L);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SecurityInfo::RecvMessage", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void DrawTPBlinkColor(LSAccumulate msgLS)
		{
			if (this.intzaTP.InvokeRequired)
			{
				this.intzaTP.Invoke(new frmMarketWatch.DrawTPBlinkColorCallBack(this.DrawTPBlinkColor), new object[]
				{
					msgLS
				});
			}
			else
			{
				try
				{
					if (ApplicationInfo.IsSupportTPBlinkColor)
					{
						int isBlink = 3;
						if (msgLS.Side == "S")
						{
							string b = Utilities.PriceFormat(msgLS.LastPrice);
							for (int i = 0; i < 5; i++)
							{
								if (this.intzaTP.Records(i).Fields("bidvolume").TempText == b)
								{
									this.intzaTP.Records(i).Fields("bidvolume").IsBlink = isBlink;
									break;
								}
							}
						}
						else
						{
							if (msgLS.Side == "B")
							{
								string b = Utilities.PriceFormat(msgLS.LastPrice);
								for (int i = 0; i < 5; i++)
								{
									if (this.intzaTP.Records(i).Fields("offervolume").TempText == b)
									{
										this.intzaTP.Records(i).Fields("offervolume").IsBlink = isBlink;
										break;
									}
								}
							}
						}
						if (base.IsAllowRender)
						{
							this.intzaTP.EndUpdate();
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("DrawTPBlinkColor", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ReceiveMessageBBO(IBroadcastMessage message, StockList.StockInformation realtimeStockInfo)
		{
			try
			{
				string messageType = message.MessageType;
				if (messageType != null)
				{
					if (!(messageType == "TP"))
					{
						if (!(messageType == "L+"))
						{
							if (!(messageType == "SS"))
							{
								if (messageType == "PO")
								{
									int num;
									if (this._mainGroupType == "Favorites")
									{
										num = ApplicationInfo.FavStockList[this._bboPage].IndexOf(realtimeStockInfo.Symbol);
									}
									else
									{
										num = this.intzaBBO.FindIndex("stock", realtimeStockInfo.Symbol);
									}
									if (num > -1)
									{
										POMessage pOMessage = (POMessage)message;
										if (ApplicationInfo.MarketState == "M")
										{
											this.UpdateBBOPreClosePrice(num, pOMessage.ProjectedPrice, realtimeStockInfo);
										}
										else
										{
											this.UpdateBBOPreOpenPrice(num, pOMessage.ProjectedPrice, realtimeStockInfo);
										}
										if (base.IsAllowRender)
										{
											this.intzaBBO.EndUpdate(num);
										}
									}
								}
							}
							else
							{
								if (realtimeStockInfo != null)
								{
									int num2;
									if (this._mainGroupType == "Favorites")
									{
										num2 = ApplicationInfo.FavStockList[this._bboPage].IndexOf(realtimeStockInfo.Symbol);
									}
									else
									{
										num2 = this.intzaBBO.FindIndex("stock", realtimeStockInfo.Symbol);
									}
									if (num2 > -1)
									{
										this.intzaBBO.Records(num2).Fields("prior").Text = realtimeStockInfo.PriorPrice;
										this.ShowDisplayFlagBBO(num2, realtimeStockInfo.DisplayFlag);
										if (base.IsAllowRender)
										{
											this.intzaBBO.EndUpdate(num2);
										}
									}
								}
							}
						}
						else
						{
							if (realtimeStockInfo != null)
							{
								int num3;
								if (this._mainGroupType == "Favorites")
								{
									num3 = ApplicationInfo.FavStockList[this._bboPage].IndexOf(realtimeStockInfo.Symbol);
								}
								else
								{
									num3 = this.intzaBBO.FindIndex("stock", realtimeStockInfo.Symbol);
								}
								if (num3 > -1)
								{
									LSAccumulate lSAccumulate = (LSAccumulate)message;
									this.ShowDisplayFlagBBO(num3, realtimeStockInfo.DisplayFlag);
									this.UpdateBBO_LS(num3, lSAccumulate.LastPrice, lSAccumulate.AccVolume * (long)realtimeStockInfo.BoardLot, lSAccumulate.AccValue, lSAccumulate.DealInMain, lSAccumulate.ComparePrice, realtimeStockInfo.HighPrice, realtimeStockInfo.LowPrice, lSAccumulate.BuyVolume * (long)realtimeStockInfo.BoardLot, lSAccumulate.SellVolume * (long)realtimeStockInfo.BoardLot, realtimeStockInfo);
									this.ShowUnderLineBBO(num3, lSAccumulate.LastPrice, realtimeStockInfo.BidPrice1, realtimeStockInfo.OfferPrice1);
									if (base.IsAllowRender)
									{
										this.intzaBBO.EndUpdate(num3);
									}
									this.DrawTPBBoBlink(num3, lSAccumulate);
								}
							}
						}
					}
					else
					{
						if (realtimeStockInfo != null)
						{
							int num4;
							if (this._mainGroupType == "Favorites")
							{
								num4 = ApplicationInfo.FavStockList[this._bboPage].IndexOf(realtimeStockInfo.Symbol);
							}
							else
							{
								num4 = this.intzaBBO.FindIndex("stock", realtimeStockInfo.Symbol);
							}
							if (num4 > -1)
							{
								TPMessage tPMessage = (TPMessage)message;
								if (tPMessage.Price1 > -1m)
								{
									this.UpdateBBOTopPrice(num4, tPMessage.Side, tPMessage.Price1, tPMessage.Volume1 * (long)realtimeStockInfo.BoardLot, realtimeStockInfo.PriorPrice, realtimeStockInfo.LastSalePrice, realtimeStockInfo);
								}
								if (this.intzaBBO.Records(num4).Rows > 1)
								{
									this.UpdateBBOBids(this.intzaBBO.Records(num4), realtimeStockInfo, tPMessage.Side, tPMessage.Price2, tPMessage.Volume2 * (long)realtimeStockInfo.BoardLot, tPMessage.Price3, tPMessage.Volume3 * (long)realtimeStockInfo.BoardLot, tPMessage.Price4, tPMessage.Volume4 * (long)realtimeStockInfo.BoardLot, tPMessage.Price5, tPMessage.Volume5 * (long)realtimeStockInfo.BoardLot);
								}
								if (base.IsAllowRender)
								{
									this.intzaBBO.EndUpdate(num4);
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SecurityInfo::RecvMessage", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void DrawTPBBoBlink(int indexBBO, LSAccumulate msgLS)
		{
			try
			{
				if (ApplicationInfo.IsSupportTPBlinkColor)
				{
					if (msgLS.Side != string.Empty)
					{
						if (indexBBO > -1 && indexBBO < ApplicationInfo.FavStockPerPage)
						{
							int isBlink = 3;
							STIControl.ExpandTableGrid.RecordItem recordItem = this.intzaBBO.Records(indexBBO);
							decimal d = 0m;
							decimal.TryParse(recordItem.Fields("bid").Text.ToString(), out d);
							if (msgLS.LastPrice == d)
							{
								recordItem.Fields("bidvol").IsBlink = isBlink;
								if (base.IsAllowRender)
								{
									this.intzaBBO.EndUpdate(indexBBO);
								}
							}
							else
							{
								d = 0m;
								decimal.TryParse(recordItem.Fields("offer").Text.ToString(), out d);
								if (msgLS.LastPrice == d)
								{
									recordItem.Fields("offvol").IsBlink = isBlink;
									if (base.IsAllowRender)
									{
										this.intzaBBO.EndUpdate(indexBBO);
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("DrawTPBBoBlink", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ReloadData()
		{
			if (!this.bgwReloadData.IsBusy)
			{
				this.bgwReloadData.RunWorkerAsync();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateToControl()
		{
			try
			{
				this.intzaInfo.BeginUpdate();
				this.intzaLS2.BeginUpdate();
				this.intzaVolumeByBoard.BeginUpdate();
				this.intzaTP.BeginUpdate();
				this.intzaInfo.ClearAllText();
				this.intzaLS2.ClearAllText();
				this.intzaVolumeByBoard.ClearAllText();
				this.intzaTP.ClearAllText();
				this.intzaBF.ClearAllText();
				if (this.tdsMainInfo.Tables.Count > 0)
				{
					this.setTopPriceColume(true);
					if (this.tdsMainInfo.Tables.Contains("security_info") && this.tdsMainInfo.Tables["security_info"].Rows.Count > 0)
					{
						decimal num = 0m;
						if (decimal.TryParse(this.tdsMainInfo.Tables["security_info"].Rows[0]["ceiling"].ToString(), out num))
						{
							if (num != this._stockInfoSET.Ceiling)
							{
								this._stockInfoSET.Ceiling = num;
							}
						}
						if (decimal.TryParse(this.tdsMainInfo.Tables["security_info"].Rows[0]["floor"].ToString(), out num))
						{
							if (num != this._stockInfoSET.Floor)
							{
								this._stockInfoSET.Floor = num;
							}
						}
					}
					this.intzaInfo.Item("ceiling").Text = this._stockInfoSET.Ceiling.ToString();
					this.intzaInfo.Item("floor").Text = this._stockInfoSET.Floor.ToString();
					if (this.tdsMainInfo.Tables.Contains("security_stat") && this.tdsMainInfo.Tables["security_stat"].Rows.Count > 0)
					{
						DataRow dataRow = this.tdsMainInfo.Tables["security_stat"].Rows[0];
						decimal num = 0m;
						if (decimal.TryParse(dataRow["prior_close_price"].ToString(), out num))
						{
							if (num != this._stockInfoSET.PriorPrice)
							{
								this._stockInfoSET.PriorPrice = num;
							}
						}
						this.intzaInfo.Item("prior").Text = Utilities.PriceFormat(this._stockInfoSET.PriorPrice, 2, 0);
						this.UpdateTopPrice("B", Convert.ToDecimal(dataRow["bid_price1"].ToString()), Convert.ToDecimal(dataRow["bid_price2"].ToString()), Convert.ToDecimal(dataRow["bid_price3"].ToString()), Convert.ToDecimal(dataRow["bid_price4"].ToString()), Convert.ToDecimal(dataRow["bid_price5"].ToString()), Convert.ToInt64(dataRow["bid_volume1"]), Convert.ToInt64(dataRow["bid_volume2"]), Convert.ToInt64(dataRow["bid_volume3"]), Convert.ToInt64(dataRow["bid_volume4"]), Convert.ToInt64(dataRow["bid_volume5"]));
						this.UpdateTopPrice("S", Convert.ToDecimal(dataRow["offer_price1"].ToString()), Convert.ToDecimal(dataRow["offer_price2"].ToString()), Convert.ToDecimal(dataRow["offer_price3"].ToString()), Convert.ToDecimal(dataRow["offer_price4"].ToString()), Convert.ToDecimal(dataRow["offer_price5"].ToString()), Convert.ToInt64(dataRow["offer_volume1"]), Convert.ToInt64(dataRow["offer_volume2"]), Convert.ToInt64(dataRow["offer_volume3"]), Convert.ToInt64(dataRow["offer_volume4"]), Convert.ToInt64(dataRow["offer_volume5"]));
						this.intzaInfo.Item("par").Text = Utilities.PriceFormat(dataRow["par_value"], 5);
						this._checkSpread = dataRow["check_spread"].ToString();
						this.intzaInfo.Item("flag").Text = dataRow["display_flag"].ToString();
						decimal num2 = 0m;
						decimal num3 = 0m;
						decimal pchg = 0m;
						IndexStat.IndexItem sector = ApplicationInfo.IndexStatInfo.GetSector(this._stockInfoSET.SectorNumber);
						if (sector != null)
						{
							this._sectorSymbol = sector.Symbol;
							this.tsSectorName.Text = sector.Symbol;
							decimal prior = sector.Prior;
							if (decimal.TryParse(dataRow["sector_index"].ToString(), out num2))
							{
								if (num2 > 0m && prior > 0m)
								{
									num3 = num2 - prior;
									pchg = num3 / prior * 100m;
								}
								else
								{
									num2 = prior;
								}
							}
						}
						else
						{
							this._sectorSymbol = string.Empty;
							this.tsSectorName.Text = string.Empty;
						}
						this.UpdateSector(num2, num3, pchg);
						this._stockInfoSET.LastSalePrice = Convert.ToDecimal(dataRow["last_sale_price"]);
						this._stockInfoSET.HighPrice = Convert.ToDecimal(dataRow["high_price"]);
						this._stockInfoSET.LowPrice = Convert.ToDecimal(dataRow["low_price"]);
						this.UpdateLastPrice(this._stockInfoSET.LastSalePrice, dataRow["compare_price"].ToString());
						this.UpdatePriceInfo(this._stockInfoSET.LastSalePrice, this._stockInfoSET.HighPrice, this._stockInfoSET.LowPrice);
						if (ApplicationInfo.MarketSession == 1)
						{
							if (ApplicationInfo.MarketState == "P")
							{
								this.UpdateOpenOrProjectOpenPrice("P", 1, Convert.ToDecimal(dataRow["projected_open"]));
							}
							else
							{
								this.UpdateOpenOrProjectOpenPrice("O", 1, Convert.ToDecimal(dataRow["open_price1"]));
							}
						}
						else
						{
							if (ApplicationInfo.MarketSession == 2)
							{
								this.UpdateOpenOrProjectOpenPrice("O", 1, Convert.ToDecimal(dataRow["open_price1"]));
								if (ApplicationInfo.MarketState == "P")
								{
									this.UpdateOpenOrProjectOpenPrice("P", 2, Convert.ToDecimal(dataRow["projected_open"]));
								}
								else
								{
									this.UpdateOpenOrProjectOpenPrice("O", 2, Convert.ToDecimal(dataRow["open_price2"]));
								}
							}
						}
						if (ApplicationInfo.MarketState == "P")
						{
							this.UpdateProjectedVolume(Convert.ToInt64(dataRow["projected_open_volume"]));
						}
						this.UpdateProjectedClosePrice(Convert.ToDecimal(dataRow["projected_close"]));
						if (ApplicationInfo.MarketState == "M")
						{
							this.UpdateProjectedVolume(Convert.ToInt64(dataRow["projected_close_volume"]));
						}
						this.UpdateAllVolume(Convert.ToInt32(dataRow["deals"]), Convert.ToInt64(dataRow["accvolume"]), Convert.ToDecimal(dataRow["accvalue"]), Convert.ToInt64(dataRow["open_volume"]), Convert.ToInt64(dataRow["buy_volume"]), Convert.ToInt64(dataRow["sell_volume"]));
						this.UpdateMainBoardValue(Convert.ToInt32(dataRow["deals"]), Convert.ToInt64(dataRow["accvolume"]), Convert.ToDecimal(dataRow["accvalue"]));
						if (this.tdsMainInfo.Tables.Contains("last_sale"))
						{
							int num4 = 0;
							foreach (DataRow dataRow2 in this.tdsMainInfo.Tables["last_sale"].Rows)
							{
								this.UpdateStockTicker(Convert.ToDecimal(dataRow2["price"]), dataRow2["side"].ToString(), Convert.ToInt64(dataRow2["volume"]), dataRow2["last_update"].ToString(), num4);
								num4++;
							}
						}
						this.UpdateBigLotValue(Convert.ToInt32(dataRow["deal_in_biglot"]), Convert.ToDecimal(dataRow["biglot_accvalue"]), Convert.ToInt64(dataRow["biglot_accvolume"]));
						this.UpdateStockInfoExpand(this.tdsMainInfo);
						dataRow = null;
					}
					else
					{
						this.tsPrice.Text = Utilities.PriceFormat(this._stockInfoSET.PriorPrice) + "           ";
						this.tsPrice.ForeColor = Color.Yellow;
					}
				}
				this.intzaTP.Redraw();
				this.intzaVolumeByBoard.Redraw();
				this.intzaInfo.Redraw();
				this.intzaLS2.Redraw();
				this.intzaBF.Redraw();
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateToControl", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateTopPrice(string side, decimal price1, decimal price2, decimal price3, decimal price4, decimal price5, long volume1, long volume2, long volume3, long volume4, long volume5)
		{
			try
			{
				Color fontColor = Color.Yellow;
				if (side == "B")
				{
					if (price1 > -1m)
					{
						fontColor = Utilities.ComparePriceCFColor(price1, this._stockInfoSET);
						this.intzaTP.Records(0).Fields("bidvolume").SetBidOfferVolumeText(volume1.ToString(), price1);
						this.intzaTP.Records(0).Fields("bidvolume").FontColor = fontColor;
						this.intzaTP.Records(0).Fields("bid").Text = Utilities.BidOfferPriceFormat(price1, volume1);
						this.intzaTP.Records(0).Fields("bid").FontColor = fontColor;
						this._bVol1 = volume1;
					}
					if (price2 > -1m)
					{
						fontColor = Utilities.ComparePriceCFColor(price2, this._stockInfoSET);
						this.intzaTP.Records(1).Fields("bidvolume").SetBidOfferVolumeText(volume2.ToString(), price2);
						this.intzaTP.Records(1).Fields("bidvolume").FontColor = fontColor;
						this.intzaTP.Records(1).Fields("bid").Text = Utilities.PriceFormat(price2);
						this.intzaTP.Records(1).Fields("bid").FontColor = fontColor;
						this._bVol2 = volume2;
					}
					if (price3 > -1m)
					{
						fontColor = Utilities.ComparePriceCFColor(price3, this._stockInfoSET);
						this.intzaTP.Records(2).Fields("bidvolume").SetBidOfferVolumeText(volume3.ToString(), price3);
						this.intzaTP.Records(2).Fields("bidvolume").FontColor = fontColor;
						this.intzaTP.Records(2).Fields("bid").Text = Utilities.PriceFormat(price3);
						this.intzaTP.Records(2).Fields("bid").FontColor = fontColor;
						this._bVol3 = volume3;
					}
					if (price4 > -1m)
					{
						fontColor = Utilities.ComparePriceCFColor(price4, this._stockInfoSET);
						this.intzaTP.Records(3).Fields("bidvolume").SetBidOfferVolumeText(volume4.ToString(), price4);
						this.intzaTP.Records(3).Fields("bidvolume").FontColor = fontColor;
						this.intzaTP.Records(3).Fields("bid").Text = Utilities.PriceFormat(price4);
						this.intzaTP.Records(3).Fields("bid").FontColor = fontColor;
						this._bVol4 = volume4;
					}
					if (price5 > -1m)
					{
						fontColor = Utilities.ComparePriceCFColor(price5, this._stockInfoSET);
						this.intzaTP.Records(4).Fields("bidvolume").SetBidOfferVolumeText(volume5.ToString(), price5);
						this.intzaTP.Records(4).Fields("bidvolume").FontColor = fontColor;
						this.intzaTP.Records(4).Fields("bid").Text = Utilities.PriceFormat(price5);
						this.intzaTP.Records(4).Fields("bid").FontColor = fontColor;
						this._bVol5 = volume5;
					}
				}
				else
				{
					if (side == "S")
					{
						if (price1 > -1m)
						{
							fontColor = Utilities.ComparePriceCFColor(price1, this._stockInfoSET);
							this.intzaTP.Records(0).Fields("offervolume").SetBidOfferVolumeText(volume1.ToString(), price1);
							this.intzaTP.Records(0).Fields("offervolume").FontColor = fontColor;
							this.intzaTP.Records(0).Fields("offer").Text = Utilities.BidOfferPriceFormat(price1, volume1);
							this.intzaTP.Records(0).Fields("offer").FontColor = fontColor;
							this._ofVol1 = volume1;
						}
						if (price2 > -1m)
						{
							fontColor = Utilities.ComparePriceCFColor(price2, this._stockInfoSET);
							this.intzaTP.Records(1).Fields("offervolume").SetBidOfferVolumeText(volume2.ToString(), price2);
							this.intzaTP.Records(1).Fields("offervolume").FontColor = fontColor;
							this.intzaTP.Records(1).Fields("offer").Text = Utilities.PriceFormat(price2);
							this.intzaTP.Records(1).Fields("offer").FontColor = fontColor;
							this._ofVol2 = volume2;
						}
						if (price3 > -1m)
						{
							fontColor = Utilities.ComparePriceCFColor(price3, this._stockInfoSET);
							this.intzaTP.Records(2).Fields("offervolume").SetBidOfferVolumeText(volume3.ToString(), price3);
							this.intzaTP.Records(2).Fields("offervolume").FontColor = fontColor;
							this.intzaTP.Records(2).Fields("offer").Text = Utilities.PriceFormat(price3);
							this.intzaTP.Records(2).Fields("offer").FontColor = fontColor;
							this._ofVol3 = volume3;
						}
						if (price4 > -1m)
						{
							fontColor = Utilities.ComparePriceCFColor(price4, this._stockInfoSET);
							this.intzaTP.Records(3).Fields("offervolume").SetBidOfferVolumeText(volume4.ToString(), price4);
							this.intzaTP.Records(3).Fields("offervolume").FontColor = fontColor;
							this.intzaTP.Records(3).Fields("offer").Text = Utilities.PriceFormat(price4);
							this.intzaTP.Records(3).Fields("offer").FontColor = fontColor;
							this._ofVol4 = volume4;
						}
						if (price5 > -1m)
						{
							fontColor = Utilities.ComparePriceCFColor(price5, this._stockInfoSET);
							this.intzaTP.Records(4).Fields("offervolume").SetBidOfferVolumeText(volume5.ToString(), price5);
							this.intzaTP.Records(4).Fields("offervolume").FontColor = fontColor;
							this.intzaTP.Records(4).Fields("offer").Text = Utilities.PriceFormat(price5);
							this.intzaTP.Records(4).Fields("offer").FontColor = fontColor;
							this._ofVol5 = volume5;
						}
					}
				}
				long num = this._bVol1 + this._bVol2 + this._bVol3 + this._bVol4 + this._bVol5;
				long num2 = this._ofVol1 + this._ofVol2 + this._ofVol3 + this._ofVol4 + this._ofVol5;
				if (num + num2 > 0L)
				{
					this.intzaBF.Item("item").Text = Utilities.PriceFormat(num / (num + num2) * 100m, 2, "0");
				}
				else
				{
					this.intzaBF.Item("item").Text = "";
				}
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateTopPrice", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateOpenOrProjectOpenPrice(string state, int session, decimal price)
		{
			if (this.intzaInfo.InvokeRequired)
			{
				this.intzaInfo.Invoke(new frmMarketWatch.UpdateOpenOrProjectOpenPriceCallBack(this.UpdateOpenOrProjectOpenPrice), new object[]
				{
					state,
					session,
					price
				});
			}
			else
			{
				try
				{
					if (state == "P")
					{
						Color backColor = Utilities.ComparePriceCFColor(price, this._stockInfoSET);
						if (session == 1)
						{
							if (price > 0m)
							{
								this.intzaInfo.Item("open1").Text = price.ToString();
								this.intzaInfo.Item("open1").BackColor = backColor;
								this.intzaInfo.Item("open1").FontColor = Color.Black;
							}
							else
							{
								this.intzaInfo.Item("open1").BackColor = this.intzaInfo.BackColor;
							}
						}
						else
						{
							if (session == 2)
							{
								if (price > 0m)
								{
									this.intzaInfo.Item("open2").Text = price.ToString();
									this.intzaInfo.Item("open2").BackColor = backColor;
									this.intzaInfo.Item("open2").FontColor = Color.Black;
								}
								else
								{
									this.intzaInfo.Item("open2").BackColor = this.intzaInfo.BackColor;
								}
							}
						}
					}
					else
					{
						if (state == "O")
						{
							if (session == 1)
							{
								this.intzaInfo.Item("open1").Text = price.ToString();
								this.intzaInfo.Item("open1").BackColor = this.intzaInfo.BackColor;
								this.intzaInfo.Item("open1").FontColor = Utilities.ComparePriceCFColor(price, this._stockInfoSET);
							}
							else
							{
								if (session == 2)
								{
									this.intzaInfo.Item("open2").Text = price.ToString();
									this.intzaInfo.Item("open2").BackColor = this.intzaInfo.BackColor;
									this.intzaInfo.Item("open2").FontColor = Utilities.ComparePriceCFColor(price, this._stockInfoSET);
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("UpdateOpenOrProjectOpenPrice", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateProjectedVolume(long volume)
		{
			if (this.intzaInfo.InvokeRequired)
			{
				this.intzaInfo.Invoke(new frmMarketWatch.UpdateProjectedVolumeCallBack(this.UpdateProjectedVolume), new object[]
				{
					volume
				});
			}
			else
			{
				try
				{
					if (volume > -1L)
					{
						if (volume > 10000000L)
						{
							volume /= 1000L;
							this.intzaInfo.Item("povol").Text = volume.ToString("#,##0") + "K";
						}
						else
						{
							this.intzaInfo.Item("povol").Text = volume.ToString("#,##0");
						}
					}
					else
					{
						this.intzaInfo.Item("povol").Text = "";
					}
				}
				catch (Exception ex)
				{
					this.ShowError("UpdateProjectedVolume", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateProjectedClosePrice(decimal closePrice)
		{
			if (this.intzaInfo.InvokeRequired)
			{
				this.intzaInfo.Invoke(new frmMarketWatch.UpdateProjectedClosePriceCallBack(this.UpdateProjectedClosePrice), new object[]
				{
					closePrice
				});
			}
			else
			{
				try
				{
					this.intzaInfo.Item("poclose").Text = closePrice.ToString();
					if (closePrice == 0m)
					{
						this.intzaInfo.Item("poclose").BackColor = this.intzaInfo.BackColor;
						this.intzaInfo.Item("poclose").FontColor = Color.White;
					}
					else
					{
						if (ApplicationInfo.MarketState == "M")
						{
							Color backColor = Utilities.ComparePriceCFColor(closePrice, this._stockInfoSET);
							this.intzaInfo.Item("poclose").BackColor = backColor;
							this.intzaInfo.Item("poclose").FontColor = Color.Black;
						}
						else
						{
							this.intzaInfo.Item("poclose").BackColor = this.intzaInfo.BackColor;
							this.intzaInfo.Item("poclose").FontColor = Utilities.ComparePriceCFColor(closePrice, this._stockInfoSET);
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("UpdateProjectedClosePrice", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateAllVolume(int deals, long accVolume, decimal accValue, long openVolume, long buyVolume, long sellVolume)
		{
			if (this.intzaInfo.InvokeRequired)
			{
				this.intzaInfo.Invoke(new frmMarketWatch.UpdateAllVolumeCallBack(this.UpdateAllVolume), new object[]
				{
					deals,
					accVolume,
					accValue,
					openVolume,
					buyVolume,
					sellVolume
				});
			}
			else
			{
				try
				{
					decimal price = 0m;
					decimal num = 0m;
					decimal num2 = 0m;
					decimal num3 = 0m;
					if (accVolume > 0L)
					{
						price = Math.Round(accValue / accVolume, 2);
						num = openVolume / accVolume * 100m;
						num2 = buyVolume / accVolume * 100m;
						num3 = ((sellVolume > 0L) ? (100m - num2 - num) : 0m);
					}
					this.intzaInfo.Item("open_vol").Text = openVolume.ToString();
					this.intzaInfo.Item("buy_vol").Text = buyVolume.ToString();
					this.intzaInfo.Item("sel_vol").Text = sellVolume.ToString();
					this.intzaInfo.Item("p_open_vol").Text = Utilities.PriceFormat(num, "%");
					this.intzaInfo.Item("p_buy_vol").Text = Utilities.PriceFormat(num2, "%");
					this.intzaInfo.Item("p_sel_vol").Text = Utilities.PriceFormat(num3, "%");
					this.intzaInfo.Item("pie").Text = string.Concat(new string[]
					{
						num.ToString("0.00"),
						";",
						num2.ToString("0.00"),
						";",
						num3.ToString("0.00")
					});
					this.intzaInfo.Item("avg").Text = price.ToString();
					this.intzaInfo.Item("avg").FontColor = Utilities.ComparePriceCFColor(price, this._stockInfoSET);
				}
				catch (Exception ex)
				{
					this.ShowError("UpdateAllVolume", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateSector(decimal price, decimal chg, decimal pchg)
		{
			if (this.tStripMenu.InvokeRequired)
			{
				this.tStripMenu.Invoke(new frmMarketWatch.UpdateSectorCallBack(this.UpdateSector), new object[]
				{
					price,
					chg,
					pchg
				});
			}
			else
			{
				try
				{
					Color foreColor = Utilities.ComparePriceColor(chg, 0m);
					this.tsSectorIndex.Text = string.Concat(new string[]
					{
						Utilities.PriceFormat(price),
						"   ",
						Utilities.PriceFormat(chg, true, ""),
						"   ",
						Utilities.PriceFormat(pchg, true, "%")
					});
					this.tsSectorIndex.ForeColor = foreColor;
				}
				catch (Exception ex)
				{
					this.ShowError("UpdateSector", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateLastPrice(decimal lastPrice, string comparePrice)
		{
			if (this.tStripMenu.InvokeRequired)
			{
				this.tStripMenu.Invoke(new frmMarketWatch.UpdateLastPriceCallBack(this.UpdateLastPrice), new object[]
				{
					lastPrice,
					comparePrice
				});
			}
			else
			{
				try
				{
					if (this._currentIsSET)
					{
						if (this._stockInfoSET != null)
						{
							if (lastPrice == 0m)
							{
								lastPrice = this._stockInfoSET.PriorPrice;
							}
							Color foreColor = Utilities.ComparePriceCFColor(lastPrice, this._stockInfoSET);
							this.tsPrice.Text = string.Concat(new string[]
							{
								Utilities.PriceFormat(lastPrice),
								"   ",
								Utilities.PriceFormat(this._stockInfoSET.ChangePrice, true, ""),
								"   ",
								Utilities.PriceFormat(this._stockInfoSET.ChangePricePct, true, "%")
							});
							this.tsPrice.ForeColor = foreColor;
							if (comparePrice == "+")
							{
								this.tslbCompare.Text = "้";
								this.tslbCompare.ForeColor = Color.Lime;
							}
							else
							{
								if (comparePrice == "-")
								{
									this.tslbCompare.Text = "๊";
									this.tslbCompare.ForeColor = Color.Red;
								}
								else
								{
									this.tslbCompare.Text = "";
								}
							}
						}
					}
					else
					{
						if (this._seriesInfoTFEX != null)
						{
							Color foreColor = Color.Yellow;
							if (lastPrice == 0m)
							{
								lastPrice = this._seriesInfoTFEX.PrevFixPrice;
							}
							decimal num = 0m;
							decimal num2 = 0m;
							if (this._seriesInfoTFEX.PrevFixPrice > 0m && lastPrice > 0m)
							{
								num = lastPrice - this._seriesInfoTFEX.PrevFixPrice;
								num2 = (lastPrice - this._seriesInfoTFEX.PrevFixPrice) / this._seriesInfoTFEX.PrevFixPrice * 100m;
							}
							foreColor = Utilities.ComparePriceCFColor(lastPrice, this._seriesInfoTFEX);
							this.tsPrice.Text = string.Concat(new string[]
							{
								Utilities.PriceFormat(lastPrice, this._seriesInfoTFEX.NumOfDec),
								"   ",
								Utilities.PriceFormat(num, true, this._seriesInfoTFEX.NumOfDec),
								"   ",
								Utilities.PriceFormat(num2, true, "%")
							});
							this.tsPrice.ForeColor = foreColor;
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("UpdateLastPrice", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdatePriceInfo(decimal lastPrice, decimal high, decimal low)
		{
			if (this.intzaInfo.InvokeRequired)
			{
				this.intzaInfo.Invoke(new frmMarketWatch.UpdatePriceInfoCallBack(this.UpdatePriceInfo), new object[]
				{
					lastPrice,
					high,
					low
				});
			}
			else
			{
				try
				{
					this.intzaInfo.Item("spread").Text = Utilities.GetSpreadPrice(lastPrice, this._stockInfoSET.PriorPrice, this._checkSpread).ToString();
					this.intzaInfo.Item("high").Text = high.ToString();
					this.intzaInfo.Item("low").Text = low.ToString();
					this.intzaInfo.Item("high").FontColor = Utilities.ComparePriceCFColor(high, this._stockInfoSET);
					this.intzaInfo.Item("low").FontColor = Utilities.ComparePriceCFColor(low, this._stockInfoSET);
				}
				catch (Exception ex)
				{
					this.ShowError("UpdateLastSalePrice", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateStockTicker(decimal price, string side, long volume, string lastUpdate, int index)
		{
			if (this.intzaLS2.InvokeRequired)
			{
				this.intzaLS2.Invoke(new frmMarketWatch.UpdateLastSaleTickerInfoCallBack(this.UpdateStockTicker), new object[]
				{
					price,
					side,
					volume,
					lastUpdate,
					index
				});
			}
			else
			{
				try
				{
					STIControl.SortTableGrid.RecordItem recordItem;
					if (index == -1)
					{
						recordItem = this.intzaLS2.AddRecord(1, true);
					}
					else
					{
						recordItem = this.intzaLS2.Records(index);
					}
					recordItem.Fields("volume").Text = volume.ToString();
					recordItem.Fields("side").Text = side;
					recordItem.Fields("price").Text = Utilities.PriceFormat(price);
					recordItem.Fields("time").Text = Utilities.GetTime(lastUpdate);
					Color fontColor = Utilities.ComparePriceCFColor(price, this._stockInfoSET);
					if (side == "B")
					{
						recordItem.Fields("side").FontColor = Color.Cyan;
						recordItem.Fields("volume").FontColor = Color.Cyan;
					}
					else
					{
						if (side == "S")
						{
							recordItem.Fields("side").FontColor = Color.Magenta;
							recordItem.Fields("volume").FontColor = Color.Magenta;
						}
						else
						{
							recordItem.Fields("side").FontColor = Color.Yellow;
							recordItem.Fields("volume").FontColor = Color.Yellow;
						}
					}
					recordItem.Fields("price").FontColor = fontColor;
					recordItem.Fields("time").FontColor = Color.LightGray;
					recordItem.Changed = true;
				}
				catch (Exception ex)
				{
					this.ShowError("UpdateStockTicker", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateMainBoardValue(int deals, long accVolume, decimal accValue)
		{
			if (this.intzaVolumeByBoard.InvokeRequired)
			{
				this.intzaVolumeByBoard.Invoke(new frmMarketWatch.UpdateMainBoardValueCallBack(this.UpdateMainBoardValue), new object[]
				{
					deals,
					accVolume,
					accValue
				});
			}
			else
			{
				try
				{
					this.intzaVolumeByBoard.Records(0).Fields("deals").Text = deals.ToString();
					this.intzaVolumeByBoard.Records(0).Fields("volume").Text = accVolume.ToString();
					this.intzaVolumeByBoard.Records(0).Fields("value").Text = Utilities.VolumeFormat(accValue, true);
					this.intzaVolumeByBoard.Records(0).Fields("deals").FontColor = Color.Yellow;
					this.intzaVolumeByBoard.Records(0).Fields("volume").FontColor = Color.Yellow;
					this.intzaVolumeByBoard.Records(0).Fields("value").FontColor = Color.Yellow;
				}
				catch (Exception ex)
				{
					this.ShowError("UpdateMainBoardValue", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateBigLotValue(int bDeal, decimal bValue, long bVolume)
		{
			if (this.intzaVolumeByBoard.InvokeRequired)
			{
				this.intzaVolumeByBoard.Invoke(new frmMarketWatch.UpdateBigLotValueCallBack(this.UpdateBigLotValue), new object[]
				{
					bDeal,
					bValue,
					bVolume
				});
			}
			else
			{
				try
				{
					this.intzaVolumeByBoard.Records(1).Fields("deals").Text = bDeal.ToString();
					this.intzaVolumeByBoard.Records(1).Fields("volume").Text = bVolume.ToString();
					this.intzaVolumeByBoard.Records(1).Fields("value").Text = Utilities.VolumeFormat(bValue, true);
					this.intzaVolumeByBoard.Records(1).Fields("deals").FontColor = Color.Yellow;
					this.intzaVolumeByBoard.Records(1).Fields("volume").FontColor = Color.Yellow;
					this.intzaVolumeByBoard.Records(1).Fields("value").FontColor = Color.Yellow;
				}
				catch (Exception ex)
				{
					this.ShowError("UpdateBigLotValue", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateStockInfoExpand(DataSet ds)
		{
			try
			{
				if (ds.Tables.Contains("security_info") && ds.Tables["security_info"].Rows.Count > 0)
				{
					DataRow dataRow = ds.Tables["security_info"].Rows[0];
					this.intzaInfo.Item("tbMarginRate").Text = Utilities.PriceFormat(dataRow["margin_rate"], "%");
				}
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateStockInfoExpand", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tstbStock_KeyUp(object sender, KeyEventArgs e)
		{
			try
			{
				Keys keyCode = e.KeyCode;
				if (keyCode != Keys.Return)
				{
					if (keyCode != Keys.End)
					{
					}
				}
				else
				{
					this.SetNewStock_Info(this.tstbStock.Text.Trim(), true);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tstbStock_KeyUp", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tstbStock_KeyPress(object sender, KeyPressEventArgs e)
		{
			try
			{
				e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
			}
			catch (Exception ex)
			{
				this.ShowError("tstbStock_KeyPress", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tstbStock_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				Keys keyCode = e.KeyCode;
				switch (keyCode)
				{
				case Keys.Prior:
				case Keys.Next:
				case Keys.Home:
				case Keys.Up:
				case Keys.Down:
					break;
				case Keys.End:
				case Keys.Left:
				case Keys.Right:
					goto IL_5E;
				default:
					if (keyCode != Keys.Subtract && keyCode != Keys.NumLock)
					{
						goto IL_5E;
					}
					break;
				}
				e.SuppressKeyPress = true;
				IL_5E:;
			}
			catch (Exception ex)
			{
				this.ShowError("tstbStock_KeyDown", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetNewStock_Info(string stockSymbol, bool isFocus)
		{
			if (this.tStripMenu.InvokeRequired)
			{
				this.tStripMenu.Invoke(new frmMarketWatch.SetNewStockInfoCallBack(this.SetNewStock_Info), new object[]
				{
					stockSymbol,
					isFocus
				});
			}
			else
			{
				try
				{
					if (!this.IsInfoLoading)
					{
						if ((stockSymbol != string.Empty && stockSymbol != this._currentStock) || ApplicationInfo.IsResumeState)
						{
							StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[stockSymbol];
							if (stockInformation.Number > 0)
							{
								this._bVol1 = 0L;
								this._bVol2 = 0L;
								this._bVol3 = 0L;
								this._bVol4 = 0L;
								this._bVol5 = 0L;
								this._ofVol1 = 0L;
								this._ofVol2 = 0L;
								this._ofVol3 = 0L;
								this._ofVol4 = 0L;
								this._ofVol5 = 0L;
								this._stockInfoSET = stockInformation;
								this._seriesInfoTFEX = null;
								ApplicationInfo.CurrentSymbol = this._stockInfoSET.Symbol;
								ApplicationInfo.CurrStockInMktWatch = this._stockInfoSET.Symbol;
								this._currentStock = this._stockInfoSET.Symbol;
								TemplateManager.Instance.SendSymbolLink(this, SymbolLinkSource.SmartStock, this._currentStock);
								this.intzaInfo.Visible = true;
								this.intzaInfoTFEX.Visible = false;
								this.tStripBBO.SuspendLayout();
								this.tsStockLable.Text = "Stock";
								this.tsSectorName.Visible = true;
								this.tsSectorIndex.Visible = true;
								this.tsbtnHChart.Visible = true;
								this.tsbtnSETNews.Visible = true;
								this.tsbtnVolAs.Visible = true;
								this.tslbTfexHigh.Visible = false;
								this.tstbTfexHigh.Visible = false;
								this.tslbTfexLow.Visible = false;
								this.tstbTfexLow.Visible = false;
								this.tslbTfexAvg.Visible = false;
								this.tstbTfexAvg.Visible = false;
								this.tStripBBO.ResumeLayout();
								this._currentIsSET = true;
								this.ReloadData();
							}
							else
							{
								SeriesList.SeriesInformation seriesInformation = ApplicationInfo.SeriesInfo[stockSymbol];
								if (seriesInformation != null & seriesInformation.Symbol != string.Empty)
								{
									this._stockInfoSET = null;
									this._bVol1 = 0L;
									this._bVol2 = 0L;
									this._bVol3 = 0L;
									this._bVol4 = 0L;
									this._bVol5 = 0L;
									this._ofVol1 = 0L;
									this._ofVol2 = 0L;
									this._ofVol3 = 0L;
									this._ofVol4 = 0L;
									this._ofVol5 = 0L;
									this._currentIsSET = false;
									this._seriesInfoTFEX = seriesInformation;
									ApplicationInfo.CurrentSymbol = seriesInformation.Symbol;
									this._currentStock = seriesInformation.Symbol;
									this.intzaInfoTFEX.Visible = true;
									this.intzaInfo.Visible = false;
									this.tStripBBO.SuspendLayout();
									this.tsStockLable.Text = "Series";
									this.tsSectorName.Visible = false;
									this.tsSectorIndex.Visible = false;
									this.tsbtnHChart.Visible = false;
									this.tsbtnSETNews.Visible = false;
									this.tsbtnVolAs.Visible = false;
									this._volAsVisible = false;
									this.panelVolAs.Hide();
									this.tslbTfexHigh.Visible = true;
									this.tstbTfexHigh.Visible = true;
									this.tslbTfexLow.Visible = true;
									this.tstbTfexLow.Visible = true;
									this.tslbTfexAvg.Visible = true;
									this.tstbTfexAvg.Visible = true;
									this.tStripBBO.ResumeLayout();
									this.ReloadData();
								}
							}
						}
						if (this.tstbStock.Text != this._currentStock)
						{
							this.tstbStock.Text = this._currentStock;
							if (!ApplicationInfo.IsOrderBoxFocus && isFocus)
							{
								this.tstbStock.Focus();
								this.tstbStock.SelectAll();
							}
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("SetNewStockInfo", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateToControl_BBO(DataSet ds)
		{
			try
			{
				if (ds.Tables.Count > 0 && ds.Tables["security_info_stat"].Rows.Count > 0)
				{
					this.intzaBBO.BeginUpdate();
					decimal price = 0m;
					decimal price2 = 0m;
					foreach (DataRow dataRow in ds.Tables["security_info_stat"].Rows)
					{
						StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[Convert.ToInt32(dataRow["security_number"])];
						int num = ApplicationInfo.FavStockList[this._bboPage].IndexOf(stockInformation.Symbol);
						if (num > -1)
						{
							this.intzaBBO.Records(num).Fields("stock").Text = stockInformation.Symbol;
							stockInformation.LastSalePrice = Convert.ToDecimal(dataRow["last_sale_price"].ToString());
							stockInformation.HighPrice = Convert.ToDecimal(dataRow["high_price"].ToString());
							stockInformation.LowPrice = Convert.ToDecimal(dataRow["low_price"].ToString());
							this.ShowDisplayFlagBBO(num, dataRow["display_flag"].ToString());
							this.UpdateBBOTopPrice(num, "B", Convert.ToDecimal(dataRow["bid_price1"].ToString()), Convert.ToInt64(dataRow["bid_volume1"]), stockInformation.PriorPrice, stockInformation.LastSalePrice, stockInformation);
							this.UpdateBBOTopPrice(num, "S", Convert.ToDecimal(dataRow["offer_price1"].ToString()), Convert.ToInt64(dataRow["offer_volume1"]), stockInformation.PriorPrice, stockInformation.LastSalePrice, stockInformation);
							decimal.TryParse(dataRow["projected_open"].ToString(), out price);
							decimal.TryParse(dataRow["projected_close"].ToString(), out price2);
							decimal lastPrice = (stockInformation.LastSalePrice > 0m) ? stockInformation.LastSalePrice : stockInformation.PriorPrice;
							this.UpdateBBO_LS(num, lastPrice, Convert.ToInt64(dataRow["accvolume"]), Convert.ToDecimal(dataRow["accvalue"]), Convert.ToInt32(dataRow["deals"]), dataRow["compare_price"].ToString(), Convert.ToDecimal(dataRow["high_price"]), Convert.ToDecimal(dataRow["low_price"]), Convert.ToInt64(dataRow["buy_volume"]), Convert.ToInt64(dataRow["sell_volume"]), stockInformation);
							this.UpdateBBOPreOpenPrice(num, price, stockInformation);
							this.UpdateBBOPreClosePrice(num, price2, stockInformation);
							this.intzaBBO.Records(num).Fields("prior").Text = stockInformation.PriorPrice;
							this.intzaBBO.Records(num).Fields("prior").FontColor = Color.Yellow;
						}
					}
				}
				this.intzaBBO.Sort(string.Empty, SortType.None);
				this.intzaBBO.Redraw();
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateToControlBBO", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateBBOTopPrice(int rowIndex, string side, decimal price, long volume, decimal prior, decimal lastSalePrice, StockList.StockInformation sf)
		{
			if (this.intzaBBO.InvokeRequired)
			{
				this.intzaBBO.Invoke(new frmMarketWatch.UpdateBBOTopPriceCallBack(this.UpdateBBOTopPrice), new object[]
				{
					rowIndex,
					side,
					price,
					volume,
					prior,
					lastSalePrice,
					sf
				});
			}
			else
			{
				try
				{
					if (rowIndex >= 0)
					{
						if (price > -1m)
						{
							Color fontColor = Color.Yellow;
							string text = Utilities.BidOfferPriceFormat(price, volume);
							if (side == "B")
							{
								this.intzaBBO.Records(rowIndex).Fields("bid").Text = text;
								this.intzaBBO.Records(rowIndex).Fields("bidvol").Text = volume;
								if (lastSalePrice > 0m)
								{
									if (Utilities.PriceFormat(lastSalePrice) == text)
									{
										this.intzaBBO.Records(rowIndex).Fields("bid").FontStyle = FontStyle.Underline;
										this.intzaBBO.Records(rowIndex).Fields("offer").FontStyle = FontStyle.Regular;
									}
									else
									{
										this.intzaBBO.Records(rowIndex).Fields("bid").FontStyle = FontStyle.Regular;
									}
								}
								fontColor = Utilities.ComparePriceCFColor(price, sf);
								this.intzaBBO.Records(rowIndex).Fields("bid").FontColor = fontColor;
								this.intzaBBO.Records(rowIndex).Fields("bidvol").FontColor = fontColor;
							}
							else
							{
								this.intzaBBO.Records(rowIndex).Fields("offer").Text = text;
								this.intzaBBO.Records(rowIndex).Fields("offvol").Text = volume;
								if (lastSalePrice > 0m)
								{
									if (Utilities.PriceFormat(lastSalePrice) == text)
									{
										this.intzaBBO.Records(rowIndex).Fields("bid").FontStyle = FontStyle.Regular;
										this.intzaBBO.Records(rowIndex).Fields("offer").FontStyle = FontStyle.Underline;
									}
									else
									{
										this.intzaBBO.Records(rowIndex).Fields("offer").FontStyle = FontStyle.Regular;
									}
								}
								fontColor = Utilities.ComparePriceCFColor(price, sf);
								this.intzaBBO.Records(rowIndex).Fields("offer").FontColor = fontColor;
								this.intzaBBO.Records(rowIndex).Fields("offvol").FontColor = fontColor;
							}
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("UpdateTopPriceBBO", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateBBOPreOpenPrice(int rowIndex, decimal price, StockList.StockInformation sf)
		{
			this.intzaBBO.Records(rowIndex).Fields("po").Text = price;
			this.intzaBBO.Records(rowIndex).Fields("po").BackColor = Color.FromArgb(64, 64, 64);
			this.intzaBBO.Records(rowIndex).Fields("po").FontColor = Utilities.ComparePriceCFColor(price, sf);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateBBOPreClosePrice(int rowIndex, decimal price, StockList.StockInformation sf)
		{
			this.intzaBBO.Records(rowIndex).Fields("pc").Text = price;
			this.intzaBBO.Records(rowIndex).Fields("pc").BackColor = Color.FromArgb(64, 64, 64);
			this.intzaBBO.Records(rowIndex).Fields("pc").FontColor = Utilities.ComparePriceCFColor(price, sf);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateBBO_LS(int rowIndex, decimal lastPrice, long accVolume, decimal accValue, int deals, string comparePrice, decimal highPrice, decimal lowPrice, long buyVol, long sellVol, StockList.StockInformation sf)
		{
			if (this.intzaBBO.InvokeRequired)
			{
				this.intzaBBO.Invoke(new frmMarketWatch.UpdateBBO_LS_CallBack(this.UpdateBBO_LS), new object[]
				{
					rowIndex,
					lastPrice,
					accVolume,
					accValue,
					deals,
					comparePrice,
					highPrice,
					lowPrice,
					buyVol,
					sellVol,
					sf
				});
			}
			else
			{
				try
				{
					STIControl.ExpandTableGrid.RecordItem recordItem = this.intzaBBO.Records(rowIndex);
					decimal num = 0m;
					if (accVolume > 0L)
					{
						num = Math.Round(accValue / accVolume, 2);
					}
					decimal num2 = 0m;
					decimal num3 = 0m;
					if (accVolume > 0L)
					{
						num3 = sellVol / accVolume * 100m;
						num2 = buyVol / accVolume * 100m;
					}
					recordItem.Fields("avg").Text = num;
					recordItem.Fields("high").Text = highPrice;
					recordItem.Fields("low").Text = lowPrice;
					if (lastPrice > 0m)
					{
						recordItem.Fields("last").Text = lastPrice;
						recordItem.Fields("last").Tag = "@" + comparePrice;
					}
					else
					{
						recordItem.Fields("last").Text = sf.PriorPrice;
						recordItem.Fields("last").Tag = "";
					}
					recordItem.Fields("chg").Text = sf.ChangePrice;
					recordItem.Fields("pchg").Text = sf.ChangePricePct;
					recordItem.Fields("mvol").Text = accVolume;
					recordItem.Fields("mval").Text = accValue / 1000m;
					recordItem.Fields("deals").Text = deals;
					recordItem.Fields("buyvolpct").Text = num2;
					recordItem.Fields("selvolpct").Text = num3;
					recordItem.Fields("high").FontColor = Utilities.ComparePriceCFColor(highPrice, sf);
					recordItem.Fields("low").FontColor = Utilities.ComparePriceCFColor(lowPrice, sf);
					recordItem.Fields("avg").FontColor = Utilities.ComparePriceCFColor(num, sf);
					Color fontColor = Utilities.ComparePriceCFColor(lastPrice, sf);
					recordItem.Fields("stock").FontColor = fontColor;
					recordItem.Fields("last").FontColor = fontColor;
					recordItem.Fields("chg").FontColor = fontColor;
					recordItem.Fields("pchg").FontColor = fontColor;
					recordItem.Fields("mvol").FontColor = Color.Yellow;
					recordItem.Fields("mval").FontColor = Color.Yellow;
					recordItem.Fields("deals").FontColor = Color.Yellow;
					recordItem.Fields("buyvolpct").FontColor = Color.Cyan;
					recordItem.Fields("selvolpct").FontColor = Color.Magenta;
				}
				catch (Exception ex)
				{
					this.ShowError("UpdateLastPriceBBO", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowDisplayFlagBBO(int rowIndex, string displayFlag)
		{
			if (this.intzaBBO.InvokeRequired)
			{
				this.intzaBBO.Invoke(new frmMarketWatch.ShowDisplayFlagBBOCallBack(this.ShowDisplayFlagBBO), new object[]
				{
					rowIndex,
					displayFlag
				});
			}
			else
			{
				try
				{
					this.intzaBBO.Records(rowIndex).Fields("stock").Tag = displayFlag;
				}
				catch (Exception ex)
				{
					this.ShowError("ShowDisplayFlagBBO", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetBBONewStock(string newStock)
		{
			try
			{
				if (newStock != string.Empty)
				{
					bool flag = false;
					bool flag2 = false;
					StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[newStock.ToUpper()];
					SeriesList.SeriesInformation seriesInformation = null;
					if (stockInformation.Number < 0)
					{
						seriesInformation = ApplicationInfo.SeriesInfo[newStock.ToUpper()];
						if (!string.IsNullOrEmpty(seriesInformation.Symbol))
						{
							flag2 = true;
						}
					}
					else
					{
						flag = true;
					}
					int num = this.intzaBBO.FocusItemIndex + 1;
					this._bboQuerySymbol = string.Empty;
					this._bboQuerySymbolTFEX = string.Empty;
					if (flag)
					{
						this.CheckExist(stockInformation.Symbol);
						this.intzaBBO.ClearAllTextByRow(num - 1);
						this.intzaBBO.Records(num - 1).Fields("stock").Text = stockInformation.Symbol;
						this._bboQuerySymbol = "'" + stockInformation.Number.ToString() + "'";
						ApplicationInfo.FavStockChanged = true;
						ApplicationInfo.FavStockList[this._bboPage][this.intzaBBO.FocusItemIndex] = stockInformation.Symbol;
						this.ReloadDataBBO();
						if (this.intzaBBO.Records(this.intzaBBO.FocusItemIndex).Rows > 1)
						{
							if (!this.bgwReloadBBOExp.IsBusy)
							{
								this._expStockNo = stockInformation.Number;
								this._expRows = this.intzaBBO.Records(this.intzaBBO.FocusItemIndex).Rows;
								this.bgwReloadBBOExp.RunWorkerAsync();
							}
						}
						this.tbStockBBO.Text = stockInformation.Symbol;
						this.tbStockBBO.SelectAll();
					}
					else
					{
						if (flag2)
						{
							this.CheckExist(seriesInformation.Symbol);
							this.intzaBBO.ClearAllTextByRow(num - 1);
							this.intzaBBO.Records(num - 1).Fields("stock").Text = seriesInformation.Symbol;
							this._bboQuerySymbolTFEX = "'" + seriesInformation.Symbol.ToString() + "'";
							ApplicationInfo.FavStockChanged = true;
							ApplicationInfo.FavStockList[this._bboPage][this.intzaBBO.FocusItemIndex] = seriesInformation.Symbol;
							this.ReloadDataBBO();
							if (this.intzaBBO.Records(this.intzaBBO.FocusItemIndex).Rows > 1)
							{
								if (!this.bgwReloadBBOExp.IsBusy)
								{
									this._expSeries = seriesInformation.Symbol;
									this._expRows = this.intzaBBO.Records(this.intzaBBO.FocusItemIndex).Rows;
									this.bgwReloadBBOExp.RunWorkerAsync();
								}
							}
							this.tbStockBBO.Text = seriesInformation.Symbol;
							this.tbStockBBO.SelectAll();
						}
						else
						{
							this.tbStockBBO.Text = this.intzaBBO.Records(num - 1).Fields("stock").Text.ToString();
							this.tbStockBBO.SelectAll();
						}
					}
				}
				else
				{
					this.ClearStockBBO(this.intzaBBO.FocusItemIndex);
					this.tbStockBBO.Hide();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tbStockBBO_KeyUp", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ClearStockBBO(int lineNo)
		{
			if (this.intzaBBO.InvokeRequired)
			{
				this.intzaBBO.Invoke(new frmMarketWatch.ClearStockBBOCallBack(this.ClearStockBBO), new object[]
				{
					lineNo
				});
			}
			else
			{
				try
				{
					if (this._mainGroupType == "Favorites")
					{
						ApplicationInfo.FavStockList[this._bboPage][lineNo] = string.Empty;
						ApplicationInfo.FavStockChanged = true;
						this.intzaBBO.ClearAllTextByRow(lineNo);
						this.intzaBBO.Redraw();
					}
				}
				catch (Exception ex)
				{
					this.ShowError("ClearStockBBO", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbStockBBO_Enter(object sender, EventArgs e)
		{
			this.SetBtnBBODelStock();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbStockBBO_Leave(object sender, EventArgs e)
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbStockBBO_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				Keys keyCode = e.KeyCode;
				if (keyCode != Keys.Return)
				{
					if (keyCode != Keys.Escape)
					{
						switch (keyCode)
						{
						case Keys.Up:
							this.ShowTextBoxPosition(e);
							e.SuppressKeyPress = true;
							break;
						case Keys.Down:
							this.ShowTextBoxPosition(e);
							e.SuppressKeyPress = true;
							break;
						}
					}
					else
					{
						this.tbStockBBO.Hide();
						this.intzaBBO.Redraw();
					}
				}
				else
				{
					this.SetBBONewStock(this.tbStockBBO.Text.Trim());
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tbStockBBO_KeyDown", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwReloadBBOExp_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				if (this.tdsBBOExp == null)
				{
					this.tdsBBOExp = new DataSet();
				}
				else
				{
					this.tdsBBOExp.Clear();
				}
				if (this._expCurrentIsSET)
				{
					string text = ApplicationInfo.WebService.Get5BidOffer(this._expStockNo);
					if (!string.IsNullOrEmpty(text))
					{
						MyDataHelper.StringToDataSet(text, this.tdsBBOExp);
					}
				}
				else
				{
					string text2 = ApplicationInfo.WebServiceTFEX.Get5BidOfferTFEX(this._expSeries);
					if (!string.IsNullOrEmpty(text2))
					{
						MyDataHelper.StringToDataSet(text2, this.tdsBBOExp);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("bgwReloadBBOExp_DoWork", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwReloadBBOExp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				if (this._expCurrentIsSET)
				{
					if (this.tdsBBOExp != null && this.tdsBBOExp.Tables.Contains("top_price") && this.tdsBBOExp.Tables["top_price"].Rows.Count > 0)
					{
						DataRow dataRow = this.tdsBBOExp.Tables["top_price"].Rows[0];
						if (this.intzaBBO.Records(this._expRowId).Rows != this._expRows)
						{
							this.intzaBBO.ExpandRows(this._expRowId, this._expRows, "stock", this._expRows == 5, false);
						}
						else
						{
							this.intzaBBO.Records(this._expRowId).SubRecord[0].ClearAllText();
							this.intzaBBO.Records(this._expRowId).SubRecord[1].ClearAllText();
							if (this.intzaBBO.Records(this._expRowId).Rows > 3)
							{
								this.intzaBBO.Records(this._expRowId).SubRecord[2].ClearAllText();
								this.intzaBBO.Records(this._expRowId).SubRecord[3].ClearAllText();
							}
						}
						StockList.StockInformation sf = ApplicationInfo.StockInfo[this._expStockNo];
						this.UpdateBBOBids(this.intzaBBO.Records(this._expRowId), sf, "B", Convert.ToDecimal(dataRow["bid_price2"].ToString()), Convert.ToInt64(dataRow["bid_volume2"].ToString()), Convert.ToDecimal(dataRow["bid_price3"].ToString()), Convert.ToInt64(dataRow["bid_volume3"].ToString()), Convert.ToDecimal(dataRow["bid_price4"].ToString()), Convert.ToInt64(dataRow["bid_volume4"].ToString()), Convert.ToDecimal(dataRow["bid_price5"].ToString()), Convert.ToInt64(dataRow["bid_volume5"].ToString()));
						this.UpdateBBOBids(this.intzaBBO.Records(this._expRowId), sf, "S", Convert.ToDecimal(dataRow["offer_price2"].ToString()), Convert.ToInt64(dataRow["offer_volume2"].ToString()), Convert.ToDecimal(dataRow["offer_price3"].ToString()), Convert.ToInt64(dataRow["offer_volume3"].ToString()), Convert.ToDecimal(dataRow["offer_price4"].ToString()), Convert.ToInt64(dataRow["offer_volume4"].ToString()), Convert.ToDecimal(dataRow["offer_price5"].ToString()), Convert.ToInt64(dataRow["offer_volume5"].ToString()));
						this.tdsBBOExp.Clear();
					}
				}
				else
				{
					if (this.tdsBBOExp != null && this.tdsBBOExp.Tables.Contains("top_price") && this.tdsBBOExp.Tables["top_price"].Rows.Count > 0)
					{
						DataRow dataRow = this.tdsBBOExp.Tables["top_price"].Rows[0];
						if (this.intzaBBO.Records(this._expRowId).Rows != this._expRows)
						{
							this.intzaBBO.ExpandRows(this._expRowId, this._expRows, "stock", this._expRows == 5, false);
						}
						else
						{
							this.intzaBBO.Records(this._expRowId).SubRecord[0].ClearAllText();
							this.intzaBBO.Records(this._expRowId).SubRecord[1].ClearAllText();
							if (this.intzaBBO.Records(this._expRowId).Rows > 3)
							{
								this.intzaBBO.Records(this._expRowId).SubRecord[2].ClearAllText();
								this.intzaBBO.Records(this._expRowId).SubRecord[3].ClearAllText();
							}
						}
						SeriesList.SeriesInformation sf2 = ApplicationInfo.SeriesInfo[this._expSeries];
						long volume;
						long.TryParse(dataRow["iBidQuantity2"].ToString(), out volume);
						long volume2;
						long.TryParse(dataRow["iBidQuantity3"].ToString(), out volume2);
						long volume3;
						long.TryParse(dataRow["iBidQuantity4"].ToString(), out volume3);
						long volume4;
						long.TryParse(dataRow["iBidQuantity5"].ToString(), out volume4);
						this.UpdateBBOBidsTFEX(this.intzaBBO.Records(this._expRowId), sf2, "B", dataRow["nmrBidPrice2"].ToString(), volume, dataRow["nmrBidPrice3"].ToString(), volume2, dataRow["nmrBidPrice4"].ToString(), volume3, dataRow["nmrBidPrice5"].ToString(), volume4);
						long.TryParse(dataRow["iAskQuantity2"].ToString(), out volume);
						long.TryParse(dataRow["iAskQuantity3"].ToString(), out volume2);
						long.TryParse(dataRow["iAskQuantity4"].ToString(), out volume3);
						long.TryParse(dataRow["iAskQuantity5"].ToString(), out volume4);
						this.UpdateBBOBidsTFEX(this.intzaBBO.Records(this._expRowId), sf2, "A", dataRow["nmrAskPrice2"].ToString(), volume, dataRow["nmrAskPrice3"].ToString(), volume2, dataRow["nmrAskPrice4"].ToString(), volume3, dataRow["nmrAskPrice5"].ToString(), volume4);
						this.tdsBBOExp.Clear();
					}
				}
				this.intzaBBO.Invalidate();
			}
			catch (Exception ex)
			{
				this.ShowError("bgwReloadBBOExp_RunWorkerCompleted", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateBBOBids(STIControl.ExpandTableGrid.RecordItem rec, StockList.StockInformation sf, string side, decimal price2, long volume2, decimal price3, long volume3, decimal price4, long volume4, decimal price5, long volume5)
		{
			try
			{
				Color fontColor = Color.Yellow;
				if (side == "B")
				{
					if (price2 > -1m)
					{
						fontColor = Utilities.ComparePriceCFColor(price2, sf);
						rec.SubRecord[0].Fields("bidvol").Text = volume2;
						rec.SubRecord[0].Fields("bidvol").FontColor = fontColor;
						rec.SubRecord[0].Fields("bid").Text = Utilities.BidOfferPriceFormat(price2, volume2);
						rec.SubRecord[0].Fields("bid").FontColor = fontColor;
					}
					if (price3 > -1m)
					{
						fontColor = Utilities.ComparePriceCFColor(price3, sf);
						rec.SubRecord[1].Fields("bidvol").Text = volume3;
						rec.SubRecord[1].Fields("bidvol").FontColor = fontColor;
						rec.SubRecord[1].Fields("bid").Text = Utilities.BidOfferPriceFormat(price3, volume3);
						rec.SubRecord[1].Fields("bid").FontColor = fontColor;
					}
					if (rec.Rows == 5)
					{
						if (price4 > -1m)
						{
							fontColor = Utilities.ComparePriceCFColor(price4, sf);
							rec.SubRecord[2].Fields("bidvol").Text = volume4;
							rec.SubRecord[2].Fields("bidvol").FontColor = fontColor;
							rec.SubRecord[2].Fields("bid").Text = Utilities.BidOfferPriceFormat(price4, volume4);
							rec.SubRecord[2].Fields("bid").FontColor = fontColor;
						}
						if (price5 > -1m)
						{
							fontColor = Utilities.ComparePriceCFColor(price5, sf);
							rec.SubRecord[3].Fields("bidvol").Text = volume5;
							rec.SubRecord[3].Fields("bidvol").FontColor = fontColor;
							rec.SubRecord[3].Fields("bid").Text = Utilities.BidOfferPriceFormat(price5, volume5);
							rec.SubRecord[3].Fields("bid").FontColor = fontColor;
						}
					}
				}
				else
				{
					if (side == "S")
					{
						if (price2 > -1m)
						{
							fontColor = Utilities.ComparePriceCFColor(price2, sf);
							rec.SubRecord[0].Fields("offvol").Text = volume2;
							rec.SubRecord[0].Fields("offvol").FontColor = fontColor;
							rec.SubRecord[0].Fields("offer").Text = Utilities.BidOfferPriceFormat(price2, volume2);
							rec.SubRecord[0].Fields("offer").FontColor = fontColor;
						}
						if (price3 > -1m)
						{
							fontColor = Utilities.ComparePriceCFColor(price3, sf);
							rec.SubRecord[1].Fields("offvol").Text = volume3;
							rec.SubRecord[1].Fields("offvol").FontColor = fontColor;
							rec.SubRecord[1].Fields("offer").Text = Utilities.BidOfferPriceFormat(price3, volume3);
							rec.SubRecord[1].Fields("offer").FontColor = fontColor;
						}
						if (rec.Rows == 5)
						{
							if (price4 > -1m)
							{
								fontColor = Utilities.ComparePriceCFColor(price4, sf);
								rec.SubRecord[2].Fields("offvol").Text = volume4;
								rec.SubRecord[2].Fields("offvol").FontColor = fontColor;
								rec.SubRecord[2].Fields("offer").Text = Utilities.BidOfferPriceFormat(price4, volume4);
								rec.SubRecord[2].Fields("offer").FontColor = fontColor;
							}
							if (price5 > -1m)
							{
								fontColor = Utilities.ComparePriceCFColor(price5, sf);
								rec.SubRecord[3].Fields("offvol").Text = volume5;
								rec.SubRecord[3].Fields("offvol").FontColor = fontColor;
								rec.SubRecord[3].Fields("offer").Text = Utilities.BidOfferPriceFormat(price5, volume5);
								rec.SubRecord[3].Fields("offer").FontColor = fontColor;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateTopPrice", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void CheckExist(string stockName)
		{
			try
			{
				if (this._mainGroupType == "Favorites")
				{
					int num = ApplicationInfo.FavStockList[this._bboPage].IndexOf(stockName);
					if (num > -1)
					{
						ApplicationInfo.FavStockList[this._bboPage][num] = string.Empty;
						this.intzaBBO.ClearAllTextByRow(num);
						this.intzaBBO.EndUpdate(num);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("CheckExist", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowTextBoxPosition(KeyEventArgs e)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new frmMarketWatch.ShowTextBoxPositionCallBack(this.ShowTextBoxPosition), new object[]
				{
					e
				});
			}
			else
			{
				try
				{
					if (!(this._mainGroupType != "Favorites"))
					{
						int num = this.intzaBBO.StartDrawIndex - 1;
						int num2 = this.intzaBBO.EndDrawIndex - 1;
						int num3 = this.intzaBBO.FocusItemIndex;
						if (e.KeyCode == Keys.Up)
						{
							num3--;
						}
						else
						{
							if (e.KeyCode == Keys.Down)
							{
								num3++;
							}
							else
							{
								if (e.KeyCode != Keys.Home)
								{
									return;
								}
								if (num3 < num)
								{
									num3 = num;
								}
								else
								{
									if (num3 > num2)
									{
										num3 = num;
									}
								}
							}
						}
						if (num3 > -1 && num3 < ApplicationInfo.FavStockPerPage)
						{
							this.intzaBBO.SetFocusItem(num3);
							Rectangle fieldPosition = this.intzaBBO.GetFieldPosition(num3, "stock");
							this.tbStockBBO.Text = string.Empty;
							this.tbStockBBO.Left = this.intzaBBO.Margin.Left;
							this.tbStockBBO.Top = this.intzaBBO.Top + fieldPosition.Top + 1;
							this.tbStockBBO.Width = fieldPosition.Width - 1;
							this.tbStockBBO.Height = fieldPosition.Height;
							this.tbStockBBO.Text = this.intzaBBO.Records(num3).Fields("stock").Text.ToString();
							this.tbStockBBO.Visible = true;
							this.tbStockBBO.BringToFront();
							this.tbStockBBO.Focus();
							this.tbStockBBO.SelectAll();
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("ShowTextBoxPosition", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateToControl_TA(DataSet ds)
		{
			try
			{
				if (ds != null && ds.Tables.Count > 0)
				{
					this.intzaBBO.BeginUpdate();
					this.intzaBBO.Rows = ds.Tables[0].Rows.Count;
					decimal price = 0m;
					decimal price2 = 0m;
					int num = 0;
					foreach (DataRow dataRow in ds.Tables[0].Rows)
					{
						StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[Convert.ToInt32(dataRow["security_number"])];
						this.intzaBBO.Records(num).Fields("stock").Text = stockInformation.Symbol;
						stockInformation.LastSalePrice = Convert.ToDecimal(dataRow["last_sale_price"].ToString());
						stockInformation.HighPrice = Convert.ToDecimal(dataRow["high_price"].ToString());
						stockInformation.LowPrice = Convert.ToDecimal(dataRow["low_price"].ToString());
						this.ShowDisplayFlagBBO(num, dataRow["display_flag"].ToString());
						this.UpdateBBOTopPrice(num, "B", Convert.ToDecimal(dataRow["bid_price1"].ToString()), Convert.ToInt64(dataRow["bid_volume1"]), stockInformation.PriorPrice, stockInformation.LastSalePrice, stockInformation);
						this.UpdateBBOTopPrice(num, "S", Convert.ToDecimal(dataRow["offer_price1"].ToString()), Convert.ToInt64(dataRow["offer_volume1"]), stockInformation.PriorPrice, stockInformation.LastSalePrice, stockInformation);
						decimal.TryParse(dataRow["projected_open"].ToString(), out price);
						decimal.TryParse(dataRow["projected_close"].ToString(), out price2);
						decimal lastPrice = (stockInformation.LastSalePrice > 0m) ? stockInformation.LastSalePrice : stockInformation.PriorPrice;
						this.UpdateBBO_LS(num, lastPrice, Convert.ToInt64(dataRow["accvolume"]), Convert.ToDecimal(dataRow["accvalue"]), Convert.ToInt32(dataRow["deals"]), dataRow["compare_price"].ToString(), Convert.ToDecimal(dataRow["high_price"]), Convert.ToDecimal(dataRow["low_price"]), Convert.ToInt64(dataRow["buy_volume"]), Convert.ToInt64(dataRow["sell_volume"]), stockInformation);
						this.UpdateBBOPreOpenPrice(num, price, stockInformation);
						this.UpdateBBOPreClosePrice(num, price2, stockInformation);
						this.intzaBBO.Records(num).Fields("prior").Text = stockInformation.PriorPrice;
						this.intzaBBO.Records(num).Fields("prior").FontColor = Color.Yellow;
						num++;
					}
					if (this._selectNewSector)
					{
						if (this._lastSectorSortSel == string.Empty)
						{
							this.tscbSortByDW.Text = "Value";
							this.intzaBBO.Sort("mval", SortType.Desc);
							this._lastSectorSortSel = "Value";
						}
						else
						{
							if (this._lastSectorSortSel == "Value")
							{
								this.tscbSortByDW.Text = "Value";
								this.intzaBBO.Sort("mval", SortType.Desc);
							}
							else
							{
								if (this._lastSectorSortSel == "Stock")
								{
									this.tscbSortByDW.Text = "Stock";
									this.intzaBBO.Sort("stock", SortType.Asc);
								}
								else
								{
									if (this._lastSectorSortSel == "%Change")
									{
										this.tscbSortByDW.Text = "%Change";
										this.intzaBBO.Sort("pchg", SortType.Desc);
									}
								}
							}
						}
						this._selectNewSector = false;
					}
					else
					{
						if (this._selectNewDW)
						{
							if (this._lastDWSortSel == string.Empty)
							{
								this.tscbSortByDW.Text = "Value";
								this.intzaBBO.Sort("mval", SortType.Desc);
								this._lastDWSortSel = "Value";
							}
							else
							{
								if (this._lastDWSortSel == "Value")
								{
									this.tscbSortByDW.Text = "Value";
									this.intzaBBO.Sort("mval", SortType.Desc);
								}
								else
								{
									if (this._lastDWSortSel == "Stock")
									{
										this.tscbSortByDW.Text = "Stock";
										this.intzaBBO.Sort("stock", SortType.Asc);
									}
									else
									{
										if (this._lastDWSortSel == "%Change")
										{
											this.tscbSortByDW.Text = "%Change";
											this.intzaBBO.Sort("pchg", SortType.Desc);
										}
									}
								}
							}
							this._selectNewDW = false;
						}
						else
						{
							this.intzaBBO.Sort(string.Empty, SortType.None);
						}
					}
				}
				this.intzaBBO.Redraw();
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateToControl", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tscbSelection_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (!this.IsBBOLoading)
				{
					if (this.tmTest == null)
					{
						this.tmTest = new Timer();
						this.tmTest.Interval = 100;
						this.tmTest.Tick += new EventHandler(this.tmTest_Tick);
					}
					this.tmTest.Stop();
					this.tmTest.Start();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tscbSelection_SelectedIndexChanged", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tmTest_Tick(object sender, EventArgs e)
		{
			this.tmTest.Stop();
			this.SetBBOPage(this.tscbBBOSelection.Text.Trim());
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ReloadDataBBO()
		{
			if (this.tStripBBO.InvokeRequired)
			{
				this.tStripBBO.Invoke(new MethodInvoker(this.ReloadDataBBO));
			}
			else
			{
				if (!this.bgwReloadDataBBO.IsBusy)
				{
					this.bgwReloadDataBBO.RunWorkerAsync();
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ReloadDataTA(string activeGroupType)
		{
			if (!this.bgwReloadDataBBO.IsBusy)
			{
				this.bgwReloadDataBBO.RunWorkerAsync();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ReloadDataSector(string sector)
		{
			if (sector != string.Empty)
			{
				if (!this.bgwReloadDataBBO.IsBusy)
				{
					this.bgwReloadDataBBO.RunWorkerAsync(sector);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwReloadDataBBO_DoWork(object sender, DoWorkEventArgs e)
		{
			if (this.FormState != ClientBaseForm.ClientBaseFormState.Closing)
			{
				this.IsBBOLoading = true;
				try
				{
					string text = string.Empty;
					string text2 = this._mainGroupType;
					if (text2 != null)
					{
						if (!(text2 == "Favorites"))
						{
							if (!(text2 == "SET"))
							{
								if (!(text2 == "DerivativeWarrant"))
								{
									if (!(text2 == "Futures"))
									{
										if (text2 == "Option")
										{
											if (this._subGroupType == "SET50Option")
											{
												if (this.tdsSet50Option == null)
												{
													this.tdsSet50Option = new DataSet();
												}
												else
												{
													this.tdsSet50Option.Clear();
												}
												this.RequestWebOptionsData();
											}
											else
											{
												text2 = this._currentBBOpage;
												if (text2 != null)
												{
													if (!(text2 == "Options - Most Active Value"))
													{
														if (!(text2 == "Options - Most Active Volume"))
														{
															if (!(text2 == "Options - Gainer"))
															{
																if (!(text2 == "Options - Loser"))
																{
																	if (!(text2 == "Options - Most Swing"))
																	{
																		goto IL_889;
																	}
																	text = ApplicationInfo.WebServiceTFEX.TFEXTopActiveBBO("SW", false);
																}
																else
																{
																	text = ApplicationInfo.WebServiceTFEX.TFEXTopActiveBBO("LN", false);
																}
															}
															else
															{
																text = ApplicationInfo.WebServiceTFEX.TFEXTopActiveBBO("GN", false);
															}
														}
														else
														{
															text = ApplicationInfo.WebServiceTFEX.TFEXTopActiveBBO("MV", false);
														}
													}
													else
													{
														text = ApplicationInfo.WebServiceTFEX.TFEXTopActiveBBO("MA", false);
													}
													if (this.tdsTopActive == null)
													{
														this.tdsTopActive = new DataSet();
													}
													else
													{
														this.tdsTopActive.Clear();
														this.tdsTopActive = null;
														this.tdsTopActive = new DataSet();
													}
													if (!string.IsNullOrEmpty(text))
													{
														MyDataHelper.StringToDataSet(text, this.tdsTopActive);
													}
												}
												IL_889:;
											}
										}
									}
									else
									{
										if (this._subGroupType == "Futures Instrument")
										{
											if (this.tdsInstrument != null)
											{
												this.tdsInstrument.Clear();
												this.tdsInstrument = null;
											}
											this.tdsInstrument = new DataSet();
											UnderlyingInfo.UnderlyingList underlyingList = ApplicationInfo.UnderlyingInfo[this._bboQuerySymbolTFEX];
											if (underlyingList != null)
											{
												text = ApplicationInfo.WebServiceTFEX.BestBidOfferByInstrument(underlyingList.OrderBookId);
											}
											if (!string.IsNullOrEmpty(text))
											{
												MyDataHelper.StringToDataSet(text, this.tdsInstrument);
											}
										}
										else
										{
											text2 = this._currentBBOpage;
											if (text2 != null)
											{
												if (!(text2 == "Futures - Most Active Value"))
												{
													if (!(text2 == "Futures - Most Active Volume"))
													{
														if (!(text2 == "Futures - Gainer"))
														{
															if (!(text2 == "Futures - Loser"))
															{
																if (text2 == "Futures - Most Swing")
																{
																	text = ApplicationInfo.WebServiceTFEX.TFEXTopActiveBBO("SW", true);
																}
															}
															else
															{
																text = ApplicationInfo.WebServiceTFEX.TFEXTopActiveBBO("LN", true);
															}
														}
														else
														{
															text = ApplicationInfo.WebServiceTFEX.TFEXTopActiveBBO("GN", true);
														}
													}
													else
													{
														text = ApplicationInfo.WebServiceTFEX.TFEXTopActiveBBO("MV", true);
													}
												}
												else
												{
													text = ApplicationInfo.WebServiceTFEX.TFEXTopActiveBBO("MA", true);
												}
											}
											if (this.tdsTopActive == null)
											{
												this.tdsTopActive = new DataSet();
											}
											else
											{
												this.tdsTopActive.Clear();
												this.tdsTopActive = null;
												this.tdsTopActive = new DataSet();
											}
											if (!string.IsNullOrEmpty(text))
											{
												MyDataHelper.StringToDataSet(text, this.tdsTopActive);
											}
										}
									}
								}
								else
								{
									if (this.tdsTopActive != null)
									{
										this.tdsTopActive.Clear();
										this.tdsTopActive = null;
									}
									this.tdsTopActive = new DataSet();
									text = ApplicationInfo.WebService.TopActiveBBO_DW(this._bboQuerySymbolSector);
									if (!string.IsNullOrEmpty(text))
									{
										MyDataHelper.StringToDataSet(text, this.tdsTopActive);
									}
								}
							}
							else
							{
								if (this._subGroupType == "Set Sector")
								{
									if (this.tdsSector != null)
									{
										this.tdsSector.Clear();
										this.tdsSector = null;
									}
									this.tdsSector = new DataSet();
									IndexStat.IndexItem indexItem = ApplicationInfo.IndexStatInfo[this._bboQuerySymbolSector];
									if (indexItem != null)
									{
										text = ApplicationInfo.WebService.TopActiveBBO_Sector(indexItem.Number);
									}
									if (!string.IsNullOrEmpty(text))
									{
										MyDataHelper.StringToDataSet(text, this.tdsSector);
									}
								}
								else
								{
									text2 = this._currentBBOpage;
									switch (text2)
									{
									case "Most Active Value":
										text = ApplicationInfo.WebService.TopActiveBBO("MA", "M", "S", 40);
										break;
									case "Most Active Volume":
										text = ApplicationInfo.WebService.TopActiveBBO("MV", "M", "S", 40);
										break;
									case "Top Gainer":
										text = ApplicationInfo.WebService.TopActiveBBO("GN", "M", "S", 40);
										break;
									case "Top Loser":
										text = ApplicationInfo.WebService.TopActiveBBO("LN", "M", "S", 40);
										break;
									case "Benefit":
										text = ApplicationInfo.WebService.TopActiveBBO_Benefit();
										break;
									case "Turnover List":
										text = ApplicationInfo.WebService.TopActiveBBO_TurnOver();
										break;
									case "Big-Lot":
										text = ApplicationInfo.WebService.TopActiveBBO("MB", "M", "S", 40);
										break;
									case "Top Swing":
										text = ApplicationInfo.WebService.TopActiveBBO("SW", "M", "S", 40);
										break;
									case "Top Projected Open":
										text = ApplicationInfo.WebService.TopActiveBBO("PO", "M", "S", 40);
										break;
									case "Top Projected Close":
										text = ApplicationInfo.WebService.TopActiveBBO("PC", "M", "S", 40);
										break;
									case "Top Open Price-1":
										text = ApplicationInfo.WebService.TopActiveBBO("OP1", "M", "S", 40);
										break;
									case "Top Open Price-2":
										text = ApplicationInfo.WebService.TopActiveBBO("OP2", "M", "S", 40);
										break;
									}
									if (this.tdsTopActive == null)
									{
										this.tdsTopActive = new DataSet();
									}
									else
									{
										this.tdsTopActive.Clear();
										this.tdsTopActive = null;
										this.tdsTopActive = new DataSet();
									}
									if (!string.IsNullOrEmpty(text))
									{
										MyDataHelper.StringToDataSet(text, this.tdsTopActive);
									}
								}
							}
						}
						else
						{
							if (this.tdsBBOFavTFEX == null)
							{
								this.tdsBBOFavTFEX = new DataSet();
							}
							else
							{
								this.tdsBBOFavTFEX.Clear();
							}
							if (this.tdsBBOFavSET == null)
							{
								this.tdsBBOFavSET = new DataSet();
							}
							else
							{
								this.tdsBBOFavSET.Clear();
							}
							if (this._bboQuerySymbolTFEX != string.Empty)
							{
								text = ApplicationInfo.WebServiceTFEX.BestBidOfferByList(this._bboQuerySymbolTFEX);
								if (!string.IsNullOrEmpty(text))
								{
									MyDataHelper.StringToDataSet(text, this.tdsBBOFavTFEX);
								}
							}
							if (this._bboQuerySymbol != string.Empty)
							{
								text = ApplicationInfo.WebService.BestBidOffer(this._bboQuerySymbol);
								if (!string.IsNullOrEmpty(text))
								{
									MyDataHelper.StringToDataSet(text, this.tdsBBOFavSET);
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("RequestWebData", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwReloadDataBBO_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (this.FormState != ClientBaseForm.ClientBaseFormState.Closing)
			{
				try
				{
					string mainGroupType = this._mainGroupType;
					if (mainGroupType != null)
					{
						if (!(mainGroupType == "Favorites"))
						{
							if (!(mainGroupType == "SET"))
							{
								if (!(mainGroupType == "DerivativeWarrant"))
								{
									if (!(mainGroupType == "Futures"))
									{
										if (mainGroupType == "Option")
										{
											if (this._subGroupType == "SET50Option")
											{
												this.intzaOption.BeginUpdate();
												this.UpdateToControl_BBO_Option();
												this.tdsSet50Option.Clear();
												this.intzaOption.Redraw();
											}
											else
											{
												this.intzaBBO.BeginUpdate();
												this.UpdateToControl_BBO_TFEX(this.tdsTopActive);
												this.tdsTopActive.Clear();
												this.intzaBBO.Redraw();
											}
										}
									}
									else
									{
										if (this._subGroupType == "Futures Instrument")
										{
											this.intzaBBO.BeginUpdate();
											this.UpdateToControl_BBO_TFEX(this.tdsInstrument);
											this.tdsInstrument.Clear();
											this.intzaBBO.Redraw();
										}
										else
										{
											this.intzaBBO.BeginUpdate();
											this.UpdateToControl_BBO_TFEX(this.tdsTopActive);
											this.tdsTopActive.Clear();
											this.intzaBBO.Redraw();
										}
									}
								}
								else
								{
									if (this.tdsTopActive != null)
									{
										this.intzaBBO.BeginUpdate();
										this.UpdateToControl_TA(this.tdsTopActive);
										this.tdsTopActive.Clear();
									}
									this.intzaBBO.Redraw();
								}
							}
							else
							{
								if (this._subGroupType == "Set Sector")
								{
									this.intzaBBO.BeginUpdate();
									this.UpdateToControl_TA(this.tdsSector);
									this.tdsSector.Clear();
									this.intzaBBO.Invalidate();
								}
								else
								{
									this.intzaBBO.BeginUpdate();
									this.UpdateToControl_TA(this.tdsTopActive);
									this.tdsTopActive.Clear();
									this.intzaBBO.Redraw();
								}
							}
						}
						else
						{
							this.intzaBBO.BeginUpdate();
							if (this._bboQuerySymbol != string.Empty)
							{
								this.UpdateToControl_BBO(this.tdsBBOFavSET);
							}
							if (this._bboQuerySymbolTFEX != string.Empty)
							{
								this.UpdateToControl_BBO_TFEX(this.tdsBBOFavTFEX);
							}
							this.intzaBBO.Redraw();
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("bgwReloadDataBBBO", ex);
				}
				this.IsBBOLoading = false;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetResize(bool isWidthChanged, bool isHeightChanged)
		{
			try
			{
				if (isWidthChanged || isHeightChanged)
				{
					this.intzaBF.Font = new Font(Settings.Default.Default_Font.Name, Settings.Default.Default_Font.Size - 1f, Settings.Default.Default_Font.Style);
					int num = this.tStripMenu.Top + this.tStripMenu.Height;
					int width = base.Width;
					int num2 = this.intzaInfo.GetHeightByRows() + 1;
					this.intzaTP.SetBounds(0, num, width * 36 / 100, this.intzaTP.GetHeightByRows());
					this.intzaBF.SetBounds(0, this.intzaTP.Top + this.intzaTP.Height, this.intzaTP.Width, this.intzaBF.GetHeightByRows());
					this.intzaVolumeByBoard.SetBounds(0, this.intzaBF.Top + this.intzaBF.Height, this.intzaTP.Width, num2 - (this.intzaTP.Height + this.intzaBF.Height));
					this.intzaInfo.SetBounds(this.intzaTP.Right + 1, num, width * 32 / 100, num2);
					this.intzaInfoTFEX.Bounds = this.intzaInfo.Bounds;
					this.intzaLS2.SetBounds(this.intzaInfo.Right + 1, num, width - this.intzaInfo.Right - 2, this.intzaInfo.Height);
					this.panelVolAs.SetBounds(this.intzaInfo.Left, this.intzaInfo.Top, this.intzaLS2.Right - this.intzaInfo.Left, this.intzaInfo.Height);
					this.pictureBox1.Bounds = this.intzaLS2.Bounds;
					this.btnCloseChart.Top = this.pictureBox1.Top + 1;
					this.btnCloseChart.Left = this.pictureBox1.Left + this.pictureBox1.Width - this.btnCloseChart.Width - 1;
					this.panelBidOffer.SetBounds(0, num + num2 + 1, width, base.Height - (num + num2 + 1));
					this.intzaBBO.SetBounds(0, this.tStripBBO.Top + this.tStripBBO.Height + 1, this.panelBidOffer.Width, this.panelBidOffer.Height - (this.tStripBBO.Top + this.tStripBBO.Height + 1));
					this.intzaOption.SetBounds(0, this.tStripCP.Top + this.tStripCP.Height + 1, this.panelBidOffer.Width, this.panelBidOffer.Height - (this.tStripCP.Top + this.tStripCP.Height + 1));
					Graphics graphics = base.CreateGraphics();
					if (this.intzaBBO.Height > 30)
					{
						this.tscbBBOSelection.DropDownHeight = this.intzaBBO.Height;
					}
					this.tscbBBOSelection.Width = (int)graphics.MeasureString("Most Active Warrant..........", Settings.Default.Default_Font).Width + 20;
					this.tscbBBOSelection.DropDownWidth = this.tscbBBOSelection.Width;
					this.tscbSortByDW.Width = (int)graphics.MeasureString("%Change", Settings.Default.Default_Font).Width + 25;
					this.tscbSortByDW.DropDownWidth = this.tscbSortByDW.Width;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SetResize", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetBBOPage(string page)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new frmMarketWatch.SetBBOPageCallBack(this.SetBBOPage), new object[]
				{
					page
				});
			}
			else
			{
				try
				{
					this.tbStockBBO.Hide();
					this.tStripBBO.SuspendLayout();
					this._bboQuerySymbolTFEX = string.Empty;
					this._bboQuerySymbol = string.Empty;
					this.SaveFavFromGrid();
					this._bboPage = 1;
					this.tslbSortBy.Visible = false;
					this.tscbSortByDW.Visible = false;
					this.tsbtnBBOAddStock.Visible = false;
					this.tsbtnBBODelStock.Visible = false;
					this._subGroupType = string.Empty;
					switch (page)
					{
					case "Favorites-1":
					case "Favorites-2":
					case "Favorites-3":
					case "Favorites-4":
					case "Favorites-5":
						this.tsbtnBBOAddStock.Visible = true;
						this.tsbtnBBODelStock.Visible = true;
						if (page == "Favorites-1")
						{
							this._bboPage = 1;
						}
						else
						{
							if (page == "Favorites-2")
							{
								this._bboPage = 2;
							}
							else
							{
								if (page == "Favorites-3")
								{
									this._bboPage = 3;
								}
								else
								{
									if (page == "Favorites-4")
									{
										this._bboPage = 4;
									}
									else
									{
										if (page == "Favorites-5")
										{
											this._bboPage = 5;
										}
									}
								}
							}
						}
						this._lastFAV = page;
						this._currentBBOpage = page;
						if (this.intzaBBO.Rows != 15)
						{
							this.intzaBBO.Rows = 15;
						}
						this.intzaBBO.ClearAllText();
						this.intzaBBO.Redraw();
						ApplicationInfo.GetFavListByPage(this._bboPage, 15, true, ref this._bboQuerySymbol, ref this._bboQuerySymbolTFEX);
						this.ReloadDataBBO();
						goto IL_584;
					case "Most Active Value":
					case "Most Active Volume":
					case "Top Gainer":
					case "Top Loser":
					case "Big-Lot":
					case "Top Projected Open":
					case "Top Projected Close":
					case "Top Open Price-1":
					case "Top Open Price-2":
					case "Top Swing":
					case "Benefit":
					case "Turnover List":
						this._lastSETsel = page;
						this._currentBBOpage = page;
						this.ReloadDataBBO();
						goto IL_584;
					case "Futures - Most Active Value":
					case "Futures - Most Active Volume":
					case "Futures - Gainer":
					case "Futures - Loser":
					case "Futures - Most Swing":
						this._lastFuturesSel = page;
						this._mainGroupType = "Futures";
						this._currentBBOpage = page;
						this.ReloadDataBBO();
						goto IL_584;
					case "Options - Most Active Value":
					case "Options - Most Active Volume":
					case "Options - Gainer":
					case "Options - Loser":
					case "Options - Most Swing":
						this._lastOptionsSel = page;
						this._currentBBOpage = page;
						this.ReloadDataBBO();
						goto IL_584;
					}
					if (page.IndexOf("Index Options") > -1)
					{
						this._lastOptionsSel = page;
						this._subGroupType = "SET50Option";
						this._currentBBOpage = page;
						this.ReloadDataBBO();
					}
					else
					{
						if (page.IndexOf("Futures") > -1)
						{
							this._lastFuturesSel = page;
							this._subGroupType = "Futures Instrument";
							this._currentBBOpage = page;
							this._bboQuerySymbolTFEX = page.Substring(1, page.IndexOf(" ")).Trim();
							this.ReloadDataBBO();
						}
						else
						{
							if (page.StartsWith("."))
							{
								if (this._isDWGroup)
								{
									this._lastDWsel = page;
									this._currentBBOpage = page;
									this._bboQuerySymbolSector = page.Substring(1);
									this._selectNewDW = true;
								}
								else
								{
									this._lastSETsel = page;
									this._subGroupType = "Set Sector";
									this._currentBBOpage = page;
									this._bboQuerySymbolSector = page.Trim();
									this._selectNewSector = true;
								}
								this.tslbSortBy.Visible = true;
								this.tscbSortByDW.Visible = true;
								this.ReloadDataBBO();
							}
						}
					}
					IL_584:
					if (this._subGroupType == "SET50Option")
					{
						if (!this.intzaOption.Visible)
						{
							this.panelBidOffer.SuspendLayout();
							this.intzaBBO.Visible = false;
							this.tStripCP.Visible = true;
							this.intzaOption.Visible = true;
							this.panelBidOffer.ResumeLayout();
						}
					}
					else
					{
						this.intzaOption.Visible = false;
						this.tStripCP.Visible = false;
						this.intzaBBO.Visible = true;
					}
				}
				catch (Exception ex)
				{
					this.ShowError("SetBBOSetPage", ex);
				}
				this.tStripBBO.ResumeLayout();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowUnderLineBBO(int rowIndex, decimal lastPrice, string bidPrice, string offerPrice)
		{
			try
			{
				if (lastPrice > 0m)
				{
					if (FormatUtil.Isnumeric(bidPrice) && Convert.ToDecimal(bidPrice) == lastPrice)
					{
						this.intzaBBO.Records(rowIndex).Fields("bid").FontStyle = FontStyle.Underline;
					}
					else
					{
						this.intzaBBO.Records(rowIndex).Fields("bid").FontStyle = FontStyle.Regular;
					}
					if (FormatUtil.Isnumeric(offerPrice) && Convert.ToDecimal(offerPrice) == lastPrice)
					{
						this.intzaBBO.Records(rowIndex).Fields("offer").FontStyle = FontStyle.Underline;
					}
					else
					{
						this.intzaBBO.Records(rowIndex).Fields("offer").FontStyle = FontStyle.Regular;
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("ShowUnderLineBBO", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaLS2_ItemDragDrop(object sender, STIControl.ExpandTableGrid.TableMouseEventArgs e, string dragValue)
		{
			this.SetNewStock_Info(dragValue, false);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaInfo_ItemDragDrop(object sender, ItemGridMouseEventArgs e, string dragValue)
		{
			this.SetNewStock_Info(dragValue, false);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaLS2_ItemDragDrop(object sender, STIControl.SortTableGrid.TableMouseEventArgs e, string dragValue)
		{
			this.SetNewStock_Info(dragValue, false);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tstbStock_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!this.IsInfoLoading)
			{
				this.SetNewStock_Info(this.tstbStock.Text.Trim(), false);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaTP_TableMouseClick(object sender, STIControl.SortTableGrid.TableMouseEventArgs e)
		{
			try
			{
				if (Settings.Default.SmartOneClick && e.RowIndex >= 0)
				{
					if (e.Column.Name == "bid" || e.Column.Name == "bidvolume")
					{
						string price = this.intzaTP.Records(e.RowIndex).Fields("bid").Text.ToString();
						string side = (e.Mouse.Button == MouseButtons.Left) ? "B" : "S";
						TemplateManager.Instance.MainForm.SendOrderBox.SetSmartOneClick(side, this._currentStock, price, Settings.Default.SmartClickVolume);
					}
					else
					{
						if (e.Column.Name == "offer" || e.Column.Name == "offervolume")
						{
							string price = this.intzaTP.Records(e.RowIndex).Fields("offer").Text.ToString();
							string side = (e.Mouse.Button == MouseButtons.Left) ? "B" : "S";
							TemplateManager.Instance.MainForm.SendOrderBox.SetSmartOneClick(side, this._currentStock, price, Settings.Default.SmartClickVolume);
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("intzaTP_TableMouseClick", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tscbTickerFilter_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaLS2_TableMouseClick(object sender, STIControl.ExpandTableGrid.TableMouseEventArgs e)
		{
			try
			{
				if (Settings.Default.SmartOneClick && e.RowIndex >= 0)
				{
					string side = (e.Mouse.Button == MouseButtons.Left) ? "B" : "S";
					string text = this.intzaLS2.Records(e.RowIndex).Fields("price").Text.ToString();
					text = text.Replace("+", "");
					text = text.Replace("-", "");
					TemplateManager.Instance.MainForm.SendOrderBox.SetSmartOneClick(side, this._currentStock, text, Settings.Default.SmartClickVolume);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("intzaLS2_TableMouseClick", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetBlinkModeTopPrice()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(this.SetBlinkModeTopPrice));
			}
			else
			{
				try
				{
					this.IsInfoLoading = true;
					FormatType columnFormat = FormatType.BidOfferVolWhite;
					if (ApplicationInfo.IsSupportTPBlinkColor)
					{
						columnFormat = FormatType.BidOfferVolume;
					}
					for (int i = 0; i < this.intzaTP.Rows; i++)
					{
						this.intzaTP.Records(i).Fields("bidvolume").ColumnFormat = columnFormat;
						this.intzaTP.Records(i).Fields("offervolume").ColumnFormat = columnFormat;
					}
					for (int i = 0; i < this.intzaBBO.Rows; i++)
					{
						this.intzaBBO.Records(i).Fields("bidvol").ColumnFormat = columnFormat;
						this.intzaBBO.Records(i).Fields("offvol").ColumnFormat = columnFormat;
					}
				}
				catch (Exception ex)
				{
					this.ShowError("SetBlinkModeTopPrice", ex);
				}
				this.IsInfoLoading = false;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnAddStock_Click(object sender, EventArgs e)
		{
			if (this._mainGroupType == "Favorites")
			{
				Keys keyData = Keys.Home;
				if (this.tbStockBBO.Visible)
				{
					keyData = Keys.Down;
				}
				this.ShowTextBoxPosition(new KeyEventArgs(keyData));
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tstbStock_Leave(object sender, EventArgs e)
		{
			this.tstbStock.Text = this._currentStock;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetBtnBBODelStock()
		{
			if (this.tStripBBO.InvokeRequired)
			{
				this.tStripBBO.Invoke(new MethodInvoker(this.SetBtnBBODelStock));
			}
			else
			{
				this.tsbtnBBODelStock.Enabled = this._bboFocused;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnSwitchChart_Click(object sender, EventArgs e)
		{
			if (this._volAsVisible)
			{
				this._volAsVisible = false;
				this.panelVolAs.Hide();
			}
			this._chartVisible = !this._chartVisible;
			this.pictureBox1.Visible = this._chartVisible;
			this.pictureBox1.BringToFront();
			this.tsbtnRefreshChart.Visible = this._chartVisible;
			this.btnCloseChart.Visible = this._chartVisible;
			if (this._chartVisible)
			{
				this.btnCloseChart.BringToFront();
			}
			this.ReloadChart();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ReloadChart()
		{
			if (this._chartVisible)
			{
				if (!this.bgwReloadChart.IsBusy)
				{
					this.bgwReloadChart.RunWorkerAsync();
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwReloadChart_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				this.ShowSplashChart(true);
				if (this._currentIsSET)
				{
					string text = ApplicationInfo.WebService.GetChartImage(this._stockInfoSET.Number, true, this.intzaLS2.Width, this.intzaLS2.Height);
					byte[] buffer = Convert.FromBase64String(text);
					using (MemoryStream memoryStream = new MemoryStream(buffer))
					{
						this.pictureBox1.Image = Image.FromStream(memoryStream);
					}
					if (text != null)
					{
						text = string.Empty;
						text = null;
					}
				}
				else
				{
					string chartImage = ApplicationInfo.WebServiceTFEX.GetChartImage(this._seriesInfoTFEX.Symbol, true, this.intzaLS2.Width, this.intzaLS2.Height, this._seriesInfoTFEX.MarketCode);
					byte[] buffer = Convert.FromBase64String(chartImage);
					using (MemoryStream memoryStream = new MemoryStream(buffer))
					{
						this.pictureBox1.Image = Image.FromStream(memoryStream);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("bgwReloadChart_DoWork", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwReloadChart_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.ShowSplashChart(false);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tscbSortByDW_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (!this.IsBBOLoading)
				{
					if (this.tscbSortByDW.Text.ToLower() == "stock")
					{
						if (this._isDWGroup)
						{
							this._lastDWSortSel = "Stock";
						}
						else
						{
							this._lastSectorSortSel = "Stock";
						}
						this.intzaBBO.Sort("stock", SortType.Asc);
					}
					else
					{
						if (this.tscbSortByDW.Text.ToLower() == "value")
						{
							if (this._isDWGroup)
							{
								this._lastDWSortSel = "Value";
							}
							else
							{
								this._lastSectorSortSel = "Value";
							}
							this.intzaBBO.Sort("mval", SortType.Desc);
						}
						else
						{
							if (this.tscbSortByDW.Text.ToLower() == "%change")
							{
								if (this._isDWGroup)
								{
									this._lastDWSortSel = "%Change";
								}
								else
								{
									this._lastSectorSortSel = "%Change";
								}
								this.intzaBBO.Sort("pchg", SortType.Desc);
							}
						}
					}
					this.intzaBBO.Redraw();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tscbSortByDW_SelectedIndexChanged", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsmiShowExpandBBO_Click(object sender, EventArgs e)
		{
			if (sender.Equals(this.tsmiShowBestBO))
			{
				this.intzaBBO.ExpandRows(this._expRowId, 1, "stock", false, false);
				this.intzaBBO.Redraw();
			}
			else
			{
				if (!this.bgwReloadBBOExp.IsBusy)
				{
					if (sender.Equals(this.tsmiShow3BO))
					{
						this._expRows = 3;
					}
					else
					{
						this._expRows = 5;
					}
					this.bgwReloadBBOExp.RunWorkerAsync();
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
		{
			try
			{
				this.tsmiShowBestBO.Checked = false;
				this.tsmiShow3BO.Checked = false;
				this.tsmiShow5BO.Checked = false;
				if (this.intzaBBO.Records(this._expRowId).Rows == 3)
				{
					this.tsmiShow3BO.Checked = true;
				}
				else
				{
					if (this.intzaBBO.Records(this._expRowId).Rows == 1)
					{
						this.tsmiShowBestBO.Checked = true;
					}
					else
					{
						this.tsmiShow5BO.Checked = true;
					}
				}
			}
			catch
			{
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnRefreshChart_Click(object sender, EventArgs e)
		{
			this.ReloadChart();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowSplashChart(bool visible)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new frmMarketWatch.ShowSplashChartCallBack(this.ShowSplashChart), new object[]
				{
					visible
				});
			}
			else
			{
				if (!base.DesignMode)
				{
					if (visible)
					{
						this.lbChartLoading.Left = this.pictureBox1.Left + (this.pictureBox1.Width - this.lbChartLoading.Width) / 2;
						this.lbChartLoading.Top = this.pictureBox1.Top + this.lbChartLoading.Height;
						this.lbChartLoading.Visible = true;
						this.lbChartLoading.BringToFront();
					}
					else
					{
						this.lbChartLoading.Visible = false;
					}
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnColEdit_Click(object sender, EventArgs e)
		{
			this.OpenColumnEditor();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OpenColumnEditor()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(this.OpenColumnEditor));
			}
			else
			{
				try
				{
					if (this._frmColEdit == null || this._frmColEdit.IsDisposed)
					{
						this._frmColEdit = new frmColumnEditor(this.intzaBBO);
					}
					this._frmColEdit.TopLevel = false;
					this._frmColEdit.Parent = this;
					if (base.Left < 0)
					{
						this._frmColEdit.Left = base.Left + (base.Width - this._frmColEdit.Width) / 2;
					}
					else
					{
						this._frmColEdit.Left = (base.Width - this._frmColEdit.Width) / 2;
					}
					if (base.Top < 0)
					{
						this._frmColEdit.Top = base.Top + (base.Height - this._frmColEdit.Height) / 2;
					}
					else
					{
						this._frmColEdit.Top = (base.Height - this._frmColEdit.Height) / 2;
					}
					this._frmColEdit.FormClosed -= new FormClosedEventHandler(this.frmColEdit_FormClosed);
					this._frmColEdit.FormClosed += new FormClosedEventHandler(this.frmColEdit_FormClosed);
					this._frmColEdit.Show();
					this._frmColEdit.BringToFront();
				}
				catch (Exception ex)
				{
					this.ShowError("OpenSystemOptionForm", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmColEdit_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (this._frmColEdit.DialogResult == DialogResult.OK)
			{
				this.intzaBBO.Columns.Clear();
				this.intzaBBO.Columns = this._frmColEdit.AdjColumns;
				this.intzaBBO.CalcColumnWidth();
				this.intzaBBO.Redraw();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaBBO_ItemDragDrop(object sender, STIControl.ExpandTableGrid.TableMouseEventArgs e, string dragValue)
		{
			try
			{
				if (this._mainGroupType == "Favorites")
				{
					if (dragValue != null && !string.IsNullOrEmpty(dragValue))
					{
						this.tbStockBBO.Hide();
						if (e != null && e.RowIndex <= this.intzaBBO.Rows - 1)
						{
							this.intzaBBO.FocusItemIndex = e.RowIndex;
							this.SetBBONewStock(dragValue);
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("intzaBBO_ItemDragDrop", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaBBO_TableClickExpand(object sender, STIControl.ExpandTableGrid.TableMouseEventArgs e)
		{
			try
			{
				if (e.Column.Name == "stock")
				{
					if (!this.bgwReloadBBOExp.IsBusy)
					{
						string text = this.intzaBBO.Records(e.RowIndex).Fields("stock").Text.ToString();
						StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[text];
						if (stockInformation.Number > 0)
						{
							this._expStockNo = stockInformation.Number;
							this._expCurrentIsSET = true;
						}
						else
						{
							SeriesList.SeriesInformation seriesInformation = ApplicationInfo.SeriesInfo[text];
							if (!(seriesInformation.Symbol != string.Empty))
							{
								return;
							}
							this._expSeries = seriesInformation.Symbol;
							this._expCurrentIsSET = false;
						}
						this._expRowId = e.RowIndex;
						this.contextMenuStrip1.Show(this.intzaBBO, e.Mouse.Location);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("intzaBBO_TableClickExpand", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaBBO_TableMouseClick(object sender, STIControl.ExpandTableGrid.TableMouseEventArgs e)
		{
			try
			{
				if (e.RowIndex > -1 && e.RowIndex < this.intzaBBO.Rows)
				{
					this.tbStockBBO.Hide();
					string text = this.intzaBBO.Records(e.RowIndex).Fields("stock").Text.ToString();
					if (!string.IsNullOrEmpty(text.Trim()))
					{
						if (Settings.Default.SmartOneClick && (e.Column.Name == "bid" || e.Column.Name == "offer" || e.Column.Name == "prior" || e.Column.Name == "last" || e.Column.Name == "high" || e.Column.Name == "low" || e.Column.Name == "po" || e.Column.Name == "pc"))
						{
							this.SetNewStock_Info(this.intzaBBO.Records(e.RowIndex).Fields("stock").Text.ToString(), false);
							string side = (e.Mouse.Button == MouseButtons.Left) ? "B" : "S";
							string price = this.intzaBBO.Records(e.RowIndex).Fields(e.Column.Name).Text.ToString();
							TemplateManager.Instance.MainForm.SendOrderBox.SetSmartOneClick(side, text, price, Settings.Default.SmartClickVolume);
						}
						else
						{
							if (e.Mouse.Button == MouseButtons.Left)
							{
								this.SetNewStock_Info(this.intzaBBO.Records(e.RowIndex).Fields("stock").Text.ToString(), false);
							}
							else
							{
								if (e.Mouse.Button == MouseButtons.Right && e.Column.Name == "stock")
								{
									this.contextLink.Tag = this.intzaBBO.Records(e.RowIndex).Fields("stock").Text.ToString();
									this.contextLink.Show(this.intzaBBO, e.Mouse.Location);
								}
							}
						}
					}
				}
				else
				{
					string text2 = e.Column.Name.ToLower();
					if (text2 != null)
					{
						if (!(text2 == "pc") && !(text2 == "po"))
						{
							if (!(text2 == "avg"))
							{
								if (!(text2 == "chg"))
								{
									if (text2 == "pchg")
									{
										this.SwitchColumns("chg");
									}
								}
								else
								{
									this.SwitchColumns("pchg");
								}
							}
							else
							{
								if (ApplicationInfo.MarketState == "M" || ApplicationInfo.MarketState == "R" || ApplicationInfo.MarketState == "C")
								{
									this.SwitchColumns("pc");
								}
								else
								{
									this.SwitchColumns("po");
								}
							}
						}
						else
						{
							this.SwitchColumns("avg");
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("TableMouseClick", ex);
			}
			this.isBBOLoading = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaBBO_TableMouseDoubleClick(object sender, STIControl.ExpandTableGrid.TableMouseEventArgs e)
		{
			try
			{
				if (this._mainGroupType == "Favorites")
				{
					if (e.Mouse.Button == MouseButtons.Left && e.RowIndex > -1 && e.Column.Name == "stock")
					{
						this.ShowTextBoxPosition(new KeyEventArgs(Keys.Home));
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("intzaBBO_TableMouseDoubleClick", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void panelBidOffer_Enter(object sender, EventArgs e)
		{
			this._bboFocused = true;
			this.intzaBBO.RowSelectType = 3;
			this.SetBtnBBODelStock();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void panelBidOffer_Leave(object sender, EventArgs e)
		{
			this._bboFocused = false;
			this.intzaBBO.RowSelectType = 2;
			this.tbStockBBO.Hide();
			this.intzaBBO.Redraw();
			this.SetBtnBBODelStock();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnBBODelStock_Click(object sender, EventArgs e)
		{
			if (this._mainGroupType == "Favorites")
			{
				this.ClearStockBBO(this.intzaBBO.FocusItemIndex);
				this.tbStockBBO.Hide();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnSETNews_Click(object sender, EventArgs e)
		{
			try
			{
				if (this._currentStock != string.Empty)
				{
					ApplicationInfo.NewsSymbol = this._currentStock;
					TemplateManager.Instance.MainForm.SetTemplateLink("News Center", string.Empty, this._currentStock);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tsbtnSETNews_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnHChart_Click(object sender, EventArgs e)
		{
			try
			{
				if (this._currentStock != string.Empty)
				{
					TemplateManager.Instance.MainForm.SetTemplateLink("Historical Chart", string.Empty, this._currentStock);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tsbtnHChart_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void setTopPriceColume(bool isEquity)
		{
			if (this.intzaTP.InvokeRequired)
			{
				this.intzaTP.Invoke(new frmMarketWatch.setTopPriceColumeCallBack(this.setTopPriceColume), new object[]
				{
					isEquity
				});
			}
			else
			{
				try
				{
					if (isEquity)
					{
						this.intzaTP.GetColumn("bidvolume").Width = 31;
						this.intzaTP.GetColumn("bid").Width = 19;
						this.intzaTP.GetColumn("offer").Width = 19;
						this.intzaTP.GetColumn("offervolume").Width = 31;
						this.intzaLS2.GetColumn("volume").Width = 36;
						this.intzaLS2.GetColumn("price").Width = 24;
					}
					else
					{
						this.intzaTP.GetColumn("bidvolume").Width = 23;
						this.intzaTP.GetColumn("bid").Width = 27;
						this.intzaTP.GetColumn("offer").Width = 27;
						this.intzaTP.GetColumn("offervolume").Width = 23;
						this.intzaLS2.GetColumn("volume").Width = 26;
						this.intzaLS2.GetColumn("price").Width = 34;
					}
				}
				catch (Exception ex)
				{
					this.ShowError("setTopPriceColume", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void setBBOColume(bool isEquity)
		{
			if (this.intzaTP.InvokeRequired)
			{
				this.intzaTP.Invoke(new frmMarketWatch.setBBOColumeCallBack(this.setBBOColume), new object[]
				{
					isEquity
				});
			}
			else
			{
				try
				{
					if (isEquity)
					{
						this.intzaBBO.GetColumn("bidvolume").Width = 12;
						this.intzaBBO.GetColumn("bid").Width = 8;
						this.intzaBBO.GetColumn("offer").Width = 8;
						this.intzaBBO.GetColumn("offervolume").Width = 12;
						this.intzaBBO.GetColumn("avg").Width = 7;
						this.intzaBBO.GetColumn("mvol").Width = 12;
					}
					else
					{
						this.intzaBBO.GetColumn("bidvolume").Width = 8;
						this.intzaBBO.GetColumn("bid").Width = 12;
						this.intzaBBO.GetColumn("offer").Width = 12;
						this.intzaBBO.GetColumn("offervolume").Width = 8;
						this.intzaBBO.GetColumn("avg").Width = 11;
						this.intzaBBO.GetColumn("mvol").Width = 8;
					}
				}
				catch (Exception ex)
				{
					this.ShowError("setBBOColume", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateToControl_TFEX()
		{
			try
			{
				this.intzaLS2.BeginUpdate();
				this.intzaVolumeByBoard.BeginUpdate();
				this.intzaTP.BeginUpdate();
				this.intzaInfoTFEX.BeginUpdate();
				this.intzaBF.BeginUpdate();
				this.intzaLS2.ClearAllText();
				this.intzaVolumeByBoard.ClearAllText();
				this.intzaTP.ClearAllText();
				this.intzaInfoTFEX.ClearAllText();
				this.intzaBF.ClearAllText();
				if (this.tdsMainInfo.Tables.Count > 0)
				{
					if (this.tdsMainInfo.Tables.Contains("series_info") && this.tdsMainInfo.Tables["series_info"].Rows.Count > 0)
					{
						DataRow dataRow = this.tdsMainInfo.Tables["series_info"].Rows[0];
						Color yellow = Color.Yellow;
						decimal num;
						decimal.TryParse(dataRow["Basis"].ToString(), out num);
						decimal num2;
						decimal.TryParse(dataRow["lastIndex"].ToString(), out num2);
						decimal num3;
						decimal.TryParse(dataRow["priorIndex"].ToString(), out num3);
						decimal num4;
						decimal.TryParse(dataRow["PrevClosePrice"].ToString(), out num4);
						decimal num5;
						decimal.TryParse(dataRow["PrjOpenPrice"].ToString(), out num5);
						decimal num6;
						decimal.TryParse(dataRow["PrjOpenPrice2"].ToString(), out num6);
						decimal num7;
						decimal.TryParse(dataRow["PrjOpenPrice3"].ToString(), out num7);
						decimal num8;
						decimal.TryParse(dataRow["PriceOpen1"].ToString(), out num8);
						decimal num9;
						decimal.TryParse(dataRow["PriceOpen2"].ToString(), out num9);
						decimal num10;
						decimal.TryParse(dataRow["PriceOpen3"].ToString(), out num10);
						this.intzaInfoTFEX.Item("basis").Text = num.ToString();
						this.intzaInfoTFEX.Item("basis").FontColor = Utilities.ComparePriceColor(num, 0m);
						this.intzaInfoTFEX.Item("ceiling").Text = Utilities.PriceFormat(this._seriesInfoTFEX.Ceiling, this._seriesInfoTFEX.NumOfDec);
						this.intzaInfoTFEX.Item("floor").Text = Utilities.PriceFormat(this._seriesInfoTFEX.Floor, this._seriesInfoTFEX.NumOfDec);
						this.intzaInfoTFEX.Item("psettle").Text = Utilities.PriceFormat(this._seriesInfoTFEX.PrevFixPrice, this._seriesInfoTFEX.NumOfDec);
						this.intzaInfoTFEX.Item("psettle").FontColor = Color.Yellow;
						this.intzaInfoTFEX.Item("settle").Text = Utilities.PriceFormat(this._seriesInfoTFEX.FixPrice, this._seriesInfoTFEX.NumOfDec);
						this.intzaInfoTFEX.Item("settle").FontColor = Color.Yellow;
						if (dataRow["FirstTradingDate"].ToString().Length == 8)
						{
							this.intzaInfoTFEX.Item("first_date").Text = Utilities.GetDateFormat(dataRow["FirstTradingDate"].ToString());
						}
						if (dataRow["LastTradingDate"].ToString().Length == 8)
						{
							this.intzaInfoTFEX.Item("last_date").Text = Utilities.GetDateFormat(dataRow["LastTradingDate"].ToString());
						}
						this.intzaInfoTFEX.Item("lastIndex").Text = Utilities.PriceFormat(num2, this._seriesInfoTFEX.NumOfDec);
						this.intzaInfoTFEX.Item("lastIndex").FontColor = Color.Yellow;
						UnderlyingInfo.UnderlyingList underlyingList = ApplicationInfo.UnderlyingInfo[this._seriesInfoTFEX.UnderOrderBookId];
						if (underlyingList.OrderBookId > 0)
						{
							this.intzaInfoTFEX.Item("lastIndex_label").Text = underlyingList.Symbol;
							this.intzaInfoTFEX.Item("lastIndex_label").FontColor = Color.White;
						}
						this.intzaInfoTFEX.Item("poclose").Text = Utilities.PriceFormat(num4, this._seriesInfoTFEX.NumOfDec);
						this.intzaInfoTFEX.Item("multiplier").Text = dataRow["Multiplier"].ToString();
						this.intzaInfoTFEX.Item("tickSize").Text = Utilities.PriceFormat(dataRow["tickSize"].ToString(), this._seriesInfoTFEX.NumOfDec);
						if (ApplicationInfo.IndexInfoTfex.TXISession == 1)
						{
							if (num8 != 0m && ApplicationInfo.IndexInfoTfex.TXIState != "7")
							{
								this.UpdateOpenOrProjectOpenPriceTFEX("11", num8, this._seriesInfoTFEX);
							}
							else
							{
								if (num5 != 0m && ApplicationInfo.IndexInfoTfex.TXIState == "7")
								{
									this.UpdateOpenOrProjectOpenPriceTFEX("7", num5, this._seriesInfoTFEX);
								}
								else
								{
									this.intzaInfoTFEX.Item("open1").BackColor = Color.Black;
								}
							}
						}
						else
						{
							if (ApplicationInfo.IndexInfoTfex.TXISession == 2)
							{
								if (num8 != 0m)
								{
									this.UpdateOpenOrProjectOpenPriceTFEX("11", num8, this._seriesInfoTFEX);
								}
								if (num9 != 0m)
								{
									this.UpdateOpenOrProjectOpenPriceTFEX("10", num9, this._seriesInfoTFEX);
								}
								else
								{
									if (num6 != 0m && ApplicationInfo.IndexInfoTfex.TXIState == "9")
									{
										this.UpdateOpenOrProjectOpenPriceTFEX("9", num6, this._seriesInfoTFEX);
									}
									else
									{
										this.intzaInfoTFEX.Item("open2").BackColor = Color.Black;
									}
								}
								if (num10 != 0m)
								{
									this.UpdateOpenOrProjectOpenPriceTFEX("24", num10, this._seriesInfoTFEX);
								}
								else
								{
									if (num7 != 0m && ApplicationInfo.IndexInfoTfex.TXMState == "23")
									{
										this.UpdateOpenOrProjectOpenPriceTFEX("23", num7, this._seriesInfoTFEX);
									}
									else
									{
										this.intzaInfoTFEX.Item("open3").BackColor = Color.Black;
									}
								}
							}
						}
						decimal num11;
						decimal.TryParse(dataRow["Multiplier"].ToString(), out num11);
						int num12;
						int.TryParse(dataRow["OpenInterest"].ToString(), out num12);
						this.intzaInfoTFEX.Item("oi").Text = Utilities.VolumeFormat(num12, true);
						decimal highPrice;
						decimal.TryParse(dataRow["HighPrice"].ToString(), out highPrice);
						decimal lowPrice;
						decimal.TryParse(dataRow["LowPrice"].ToString(), out lowPrice);
						decimal lastPrice;
						decimal.TryParse(dataRow["LastPrice"].ToString(), out lastPrice);
						int num13;
						int.TryParse(dataRow["TurnOverQty"].ToString(), out num13);
						decimal num14;
						decimal.TryParse(dataRow["TurnOverValue"].ToString(), out num14);
						long openVolume;
						long.TryParse(dataRow["TotalOpenQty"].ToString(), out openVolume);
						long longVolume;
						long.TryParse(dataRow["LongQty"].ToString(), out longVolume);
						long shortVolume;
						long.TryParse(dataRow["ShortQty"].ToString(), out shortVolume);
						int deals;
						int.TryParse(dataRow["NumOfDeal"].ToString(), out deals);
						decimal d;
						decimal.TryParse(dataRow["TotalOpenValue"].ToString(), out d);
						decimal d2;
						decimal.TryParse(dataRow["LongValue"].ToString(), out d2);
						decimal d3;
						decimal.TryParse(dataRow["ShortValue"].ToString(), out d3);
						if (num13 > 0)
						{
							decimal num15 = num14 / num13;
						}
						this.UpdateLastPrice(lastPrice, "");
						this.UpdateHeaderPrice(highPrice, lowPrice);
						if (num11 > 0m)
						{
							this.UpdateAllVolumeTFEX(deals, (long)num13, num14, openVolume, longVolume, shortVolume, d / num11, d2 / num11, d3 / num11, num11, num);
						}
						else
						{
							this.UpdateMainBoardValue(deals, (long)num13, 0m);
						}
						long volume = 0L;
						if (this.tdsMainInfo.Tables.Contains("last_sale"))
						{
							for (int i = 0; i < this.tdsMainInfo.Tables["last_sale"].Rows.Count; i++)
							{
								DataRow dataRow2 = this.tdsMainInfo.Tables["last_sale"].Rows[i];
								long.TryParse(dataRow2["iQuantity"].ToString(), out volume);
								decimal price;
								decimal.TryParse(dataRow2["nmrPrice"].ToString(), out price);
								this.UpdateTickerInfo_TFEX(price, dataRow2["sSide"].ToString(), volume, Utilities.GetTimeLastSale(dataRow2["dtLastUpd"].ToString()), i);
							}
						}
					}
					this.setTopPriceColume(false);
					if (this.tdsMainInfo.Tables.Contains("top_price") && this.tdsMainInfo.Tables["top_price"].Rows.Count > 0)
					{
						int seriesNo = 0;
						DataRow dataRow = this.tdsMainInfo.Tables["top_price"].Rows[0];
						long volume2;
						long.TryParse(dataRow["BidQty1"].ToString(), out volume2);
						long volume3;
						long.TryParse(dataRow["BidQty2"].ToString(), out volume3);
						long volume4;
						long.TryParse(dataRow["BidQty3"].ToString(), out volume4);
						long volume5;
						long.TryParse(dataRow["BidQty4"].ToString(), out volume5);
						long volume6;
						long.TryParse(dataRow["BidQty5"].ToString(), out volume6);
						decimal price2;
						decimal.TryParse(dataRow["BidPrice1"].ToString(), out price2);
						decimal price3;
						decimal.TryParse(dataRow["BidPrice2"].ToString(), out price3);
						decimal price4;
						decimal.TryParse(dataRow["BidPrice3"].ToString(), out price4);
						decimal price5;
						decimal.TryParse(dataRow["BidPrice4"].ToString(), out price5);
						decimal price6;
						decimal.TryParse(dataRow["BidPrice5"].ToString(), out price6);
						int.TryParse(dataRow["lOrderBookId"].ToString(), out seriesNo);
						this.UpdateTopPriceInfo_TFEX(seriesNo, "B", price2, price3, price4, price5, price6, volume2, volume3, volume4, volume5, volume6);
						long.TryParse(dataRow["AskQty1"].ToString(), out volume2);
						long.TryParse(dataRow["AskQty2"].ToString(), out volume3);
						long.TryParse(dataRow["AskQty3"].ToString(), out volume4);
						long.TryParse(dataRow["AskQty4"].ToString(), out volume5);
						long.TryParse(dataRow["AskQty5"].ToString(), out volume6);
						decimal.TryParse(dataRow["AskPrice1"].ToString(), out price2);
						decimal.TryParse(dataRow["AskPrice2"].ToString(), out price3);
						decimal.TryParse(dataRow["AskPrice3"].ToString(), out price4);
						decimal.TryParse(dataRow["AskPrice4"].ToString(), out price5);
						decimal.TryParse(dataRow["AskPrice5"].ToString(), out price6);
						this.UpdateTopPriceInfo_TFEX(seriesNo, "A", price2, price3, price4, price5, price6, volume2, volume3, volume4, volume5, volume6);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateToControl_TFEX", ex);
			}
			finally
			{
				this.intzaTP.Redraw();
				this.intzaLS2.Redraw();
				this.intzaVolumeByBoard.Redraw();
				this.intzaInfoTFEX.Redraw();
				this.intzaBF.Redraw();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateOpenOrProjectOpenPriceTFEX(string state, decimal price, SeriesList.SeriesInformation sf)
		{
			try
			{
				if (state == "7")
				{
					if (price != 0m)
					{
						this.intzaInfoTFEX.Item("open1").Text = Utilities.PriceFormat(price, sf.NumOfDec);
						this.intzaInfoTFEX.Item("open1").BackColor = Utilities.ComparePriceCFColor(price, sf);
						this.intzaInfoTFEX.Item("open1").FontColor = Color.Black;
					}
					else
					{
						this.intzaInfoTFEX.Item("open1").BackColor = Color.Black;
					}
				}
				else
				{
					if (state == "9")
					{
						if (price != 0m)
						{
							this.intzaInfoTFEX.Item("open2").Text = Utilities.PriceFormat(price, sf.NumOfDec);
							this.intzaInfoTFEX.Item("open2").BackColor = Utilities.ComparePriceCFColor(price, sf);
							this.intzaInfoTFEX.Item("open2").FontColor = Color.Black;
						}
						else
						{
							this.intzaInfoTFEX.Item("open2").BackColor = Color.Black;
						}
					}
					else
					{
						if (state == "11")
						{
							if (price != 0m)
							{
								this.intzaInfoTFEX.Item("open1").Text = Utilities.PriceFormat(price, sf.NumOfDec);
								this.intzaInfoTFEX.Item("open1").BackColor = Color.Black;
								this.intzaInfoTFEX.Item("open1").FontColor = Utilities.ComparePriceCFColor(price, sf);
							}
						}
						else
						{
							if (state == "10")
							{
								if (price != 0m)
								{
									this.intzaInfoTFEX.Item("open2").Text = Utilities.PriceFormat(price, sf.NumOfDec);
									this.intzaInfoTFEX.Item("open2").BackColor = Color.Black;
									this.intzaInfoTFEX.Item("open2").FontColor = Utilities.ComparePriceCFColor(price, sf);
								}
							}
							else
							{
								if (state == "23")
								{
									if (price != 0m)
									{
										this.intzaInfoTFEX.Item("open3").Text = Utilities.PriceFormat(price, sf.NumOfDec);
										this.intzaInfoTFEX.Item("open3").BackColor = Utilities.ComparePriceCFColor(price, sf);
										this.intzaInfoTFEX.Item("open3").FontColor = Color.Black;
									}
									else
									{
										this.intzaInfoTFEX.Item("open3").BackColor = Color.Black;
									}
								}
								else
								{
									if (state == "24")
									{
										if (price != 0m)
										{
											this.intzaInfoTFEX.Item("open3").Text = Utilities.PriceFormat(price, sf.NumOfDec);
											this.intzaInfoTFEX.Item("open3").BackColor = Color.Black;
											this.intzaInfoTFEX.Item("open3").FontColor = Utilities.ComparePriceCFColor(price, sf);
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
				this.ShowError("UpdateOpenOrProjectOpenPriceTFEX", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateHeaderPrice(decimal highPrice, decimal lowPrice)
		{
			if (this.tStripMenu.InvokeRequired)
			{
				this.tStripMenu.Invoke(new frmMarketWatch.UpdateHeaderPriceCallBack(this.UpdateHeaderPrice), new object[]
				{
					highPrice,
					lowPrice
				});
			}
			else
			{
				try
				{
					if (this._seriesInfoTFEX != null)
					{
						Color foreColor = default(Color);
						foreColor = Utilities.ComparePriceCFColor(highPrice, this._seriesInfoTFEX);
						this.tstbTfexHigh.Text = Utilities.PriceFormat(highPrice, this._seriesInfoTFEX.NumOfDec);
						this.tstbTfexHigh.ForeColor = foreColor;
						foreColor = Utilities.ComparePriceCFColor(lowPrice, this._seriesInfoTFEX);
						this.tstbTfexLow.Text = Utilities.PriceFormat(lowPrice, this._seriesInfoTFEX.NumOfDec);
						this.tstbTfexLow.ForeColor = foreColor;
					}
				}
				catch (Exception ex)
				{
					this.ShowError("UpdateHeaderPrice", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateAllVolumeTFEX(int deals, long accVolume, decimal accValue, long openVolume, long longVolume, long shortVolume, decimal openValue, decimal longValue, decimal shortValue, decimal priceQuoteFactor, decimal basis)
		{
			decimal avg = 0m;
			decimal num = 0m;
			decimal num2 = 0m;
			decimal num3 = 0m;
			decimal num4 = 0m;
			decimal num5 = 0m;
			decimal num6 = 0m;
			try
			{
				if (accVolume > 0L)
				{
					avg = Math.Round(accValue / accVolume, 2);
					accValue *= priceQuoteFactor;
					if (openVolume > 0L)
					{
						num = Math.Round(openValue / openVolume, 2);
						num4 = openVolume / accVolume * 100m;
					}
					if (longVolume > 0L)
					{
						num2 = Math.Round(longValue / longVolume, 2);
						num5 = longVolume / accVolume * 100m;
					}
					if (shortVolume > 0L)
					{
						num3 = Math.Round(shortValue / shortVolume, 2);
						num6 = 100m - num5 - num4;
					}
				}
				this.UpdateMainBoardValue(deals, accVolume, accValue);
				this.intzaInfoTFEX.Item("open_vol").Text = openVolume.ToString();
				this.intzaInfoTFEX.Item("long_vol").Text = longVolume.ToString();
				this.intzaInfoTFEX.Item("short_vol").Text = shortVolume.ToString();
				this.intzaInfoTFEX.Item("open_pvol").Text = Utilities.PriceFormat(num4, "%");
				this.intzaInfoTFEX.Item("long_pvol").Text = Utilities.PriceFormat(num5, "%");
				this.intzaInfoTFEX.Item("short_pvol").Text = Utilities.PriceFormat(num6, "%");
				this.intzaInfoTFEX.Item("open_avg").Text = Utilities.PriceFormat(num, this._seriesInfoTFEX.NumOfDec);
				this.intzaInfoTFEX.Item("open_avg").FontColor = Utilities.ComparePriceCFColor(num, this._seriesInfoTFEX);
				this.intzaInfoTFEX.Item("long_avg").Text = Utilities.PriceFormat(num2, this._seriesInfoTFEX.NumOfDec);
				this.intzaInfoTFEX.Item("long_avg").FontColor = Utilities.ComparePriceCFColor(num2, this._seriesInfoTFEX);
				this.intzaInfoTFEX.Item("short_avg").Text = Utilities.PriceFormat(num3, this._seriesInfoTFEX.NumOfDec);
				this.intzaInfoTFEX.Item("short_avg").FontColor = Utilities.ComparePriceCFColor(num3, this._seriesInfoTFEX);
				this.intzaInfoTFEX.Item("turnover").Text = accVolume.ToString();
				this.intzaInfoTFEX.Item("basis").Text = basis.ToString();
				this.intzaInfoTFEX.Item("basis").FontColor = Utilities.ComparePriceColor(basis, 0m);
				if (num4 + num5 + num6 > 0m)
				{
					this.intzaInfoTFEX.Item("pie").Text = string.Concat(new string[]
					{
						num4.ToString("0.00"),
						";",
						num5.ToString("0.00"),
						";",
						num6.ToString("0.00")
					});
				}
				else
				{
					this.intzaInfoTFEX.Item("pie").Text = "0;0;0";
				}
				this.UpdateTfexAvg(avg);
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateAllVolumeTFEX", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateTickerInfo_TFEX(decimal price, string side, long volume, string lastUpdate, int index)
		{
			if (this.intzaLS2.InvokeRequired)
			{
				this.intzaLS2.Invoke(new frmMarketWatch.UpdateTickerTFEXInfoCallBack(this.UpdateTickerInfo_TFEX), new object[]
				{
					price,
					side,
					volume,
					lastUpdate,
					index
				});
			}
			else
			{
				try
				{
					STIControl.SortTableGrid.RecordItem recordItem;
					if (index == -1)
					{
						recordItem = this.intzaLS2.AddRecord(1, true);
					}
					else
					{
						recordItem = this.intzaLS2.Records(index);
					}
					recordItem.Fields("side").Text = side.ToString();
					recordItem.Fields("volume").Text = volume.ToString();
					recordItem.Fields("price").Text = Utilities.PriceFormat(price, this._seriesInfoTFEX.NumOfDec);
					recordItem.Fields("time").Text = Utilities.GetTime(lastUpdate);
					Color fontColor = Utilities.ComparePriceCFColor(price, this._seriesInfoTFEX);
					recordItem.Fields("price").FontColor = fontColor;
					recordItem.Fields("side").FontColor = fontColor;
					recordItem.Fields("volume").FontColor = fontColor;
					recordItem.Fields("time").FontColor = Color.LightGray;
					recordItem.Changed = true;
					if (side == "B")
					{
						recordItem.Fields("side").FontColor = Color.Cyan;
						recordItem.Fields("volume").FontColor = Color.Cyan;
					}
					else
					{
						if (side == "S")
						{
							recordItem.Fields("side").FontColor = Color.Magenta;
							recordItem.Fields("volume").FontColor = Color.Magenta;
						}
						else
						{
							recordItem.Fields("side").FontColor = Color.Yellow;
							recordItem.Fields("volume").FontColor = Color.Yellow;
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("UpdateTickerInfo_TFEX", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateTopPriceInfo_TFEX(int seriesNo, string side, decimal price1, decimal price2, decimal price3, decimal price4, decimal price5, long volume1, long volume2, long volume3, long volume4, long volume5)
		{
			try
			{
				if (seriesNo > 0)
				{
					Color fontColor = (Brushes.Yellow as SolidBrush).Color;
					if (side == "B")
					{
						fontColor = Utilities.ComparePriceCFColor(price1, this._seriesInfoTFEX);
						if (volume1 != -1L)
						{
							this.intzaTP.Records(0).Fields("bidvolume").Text = volume1;
							this.intzaTP.Records(0).Fields("bidvolume").FontColor = fontColor;
							if (Utilities.GetTopPriceZero(volume1, price1))
							{
								this.intzaTP.Records(0).Fields("bid").Text = "0.00";
							}
							else
							{
								this.intzaTP.Records(0).Fields("bid").Text = Utilities.PriceFormat(price1, this._seriesInfoTFEX.NumOfDec);
							}
							this.intzaTP.Records(0).Fields("bid").FontColor = fontColor;
							this._bVol1 = volume1;
						}
						if (volume2 != -1L)
						{
							fontColor = Utilities.ComparePriceCFColor(price2, this._seriesInfoTFEX);
							this.intzaTP.Records(1).Fields("bidvolume").Text = volume2;
							this.intzaTP.Records(1).Fields("bidvolume").FontColor = fontColor;
							if (Utilities.GetTopPriceZero(volume2, price2))
							{
								this.intzaTP.Records(1).Fields("bid").Text = "0.00";
							}
							else
							{
								this.intzaTP.Records(1).Fields("bid").Text = Utilities.PriceFormat(price2, this._seriesInfoTFEX.NumOfDec);
							}
							this.intzaTP.Records(1).Fields("bid").FontColor = fontColor;
							this._bVol2 = volume2;
						}
						if (volume3 != -1L)
						{
							fontColor = Utilities.ComparePriceCFColor(price3, this._seriesInfoTFEX);
							this.intzaTP.Records(2).Fields("bidvolume").Text = volume3;
							this.intzaTP.Records(2).Fields("bidvolume").FontColor = fontColor;
							if (Utilities.GetTopPriceZero(volume3, price3))
							{
								this.intzaTP.Records(2).Fields("bid").Text = "0.00";
							}
							else
							{
								this.intzaTP.Records(2).Fields("bid").Text = Utilities.PriceFormat(price3, this._seriesInfoTFEX.NumOfDec);
							}
							this.intzaTP.Records(2).Fields("bid").FontColor = fontColor;
							this._bVol3 = volume3;
						}
						if (volume4 != -1L)
						{
							fontColor = Utilities.ComparePriceCFColor(price4, this._seriesInfoTFEX);
							this.intzaTP.Records(3).Fields("bidvolume").Text = volume4;
							this.intzaTP.Records(3).Fields("bidvolume").FontColor = fontColor;
							if (Utilities.GetTopPriceZero(volume4, price4))
							{
								this.intzaTP.Records(3).Fields("bid").Text = "0.00";
							}
							else
							{
								this.intzaTP.Records(3).Fields("bid").Text = Utilities.PriceFormat(price4, this._seriesInfoTFEX.NumOfDec);
							}
							this.intzaTP.Records(3).Fields("bid").FontColor = fontColor;
							this._bVol4 = volume4;
						}
						if (volume5 != -1L)
						{
							fontColor = Utilities.ComparePriceCFColor(price5, this._seriesInfoTFEX);
							this.intzaTP.Records(4).Fields("bidvolume").Text = volume5;
							this.intzaTP.Records(4).Fields("bidvolume").FontColor = fontColor;
							if (Utilities.GetTopPriceZero(volume5, price5))
							{
								this.intzaTP.Records(4).Fields("bid").Text = "0.00";
							}
							else
							{
								this.intzaTP.Records(4).Fields("bid").Text = Utilities.PriceFormat(price5, this._seriesInfoTFEX.NumOfDec);
							}
							this.intzaTP.Records(4).Fields("bid").FontColor = fontColor;
							this._bVol5 = volume5;
						}
					}
					else
					{
						if (side == "A")
						{
							if (volume1 != -1L)
							{
								fontColor = Utilities.ComparePriceCFColor(price1, this._seriesInfoTFEX);
								this.intzaTP.Records(0).Fields("offervolume").Text = volume1;
								this.intzaTP.Records(0).Fields("offervolume").FontColor = fontColor;
								if (Utilities.GetTopPriceZero(volume1, price1))
								{
									this.intzaTP.Records(0).Fields("offer").Text = "0.00";
								}
								else
								{
									this.intzaTP.Records(0).Fields("offer").Text = Utilities.PriceFormat(price1, this._seriesInfoTFEX.NumOfDec);
								}
								this.intzaTP.Records(0).Fields("offer").FontColor = fontColor;
								this._ofVol1 = volume1;
							}
							if (volume2 != -1L)
							{
								fontColor = Utilities.ComparePriceCFColor(price2, this._seriesInfoTFEX);
								this.intzaTP.Records(1).Fields("offervolume").Text = volume2;
								this.intzaTP.Records(1).Fields("offervolume").FontColor = fontColor;
								if (Utilities.GetTopPriceZero(volume2, price2))
								{
									this.intzaTP.Records(1).Fields("offer").Text = "0.00";
								}
								else
								{
									this.intzaTP.Records(1).Fields("offer").Text = Utilities.PriceFormat(price2, this._seriesInfoTFEX.NumOfDec);
								}
								this.intzaTP.Records(1).Fields("offer").FontColor = fontColor;
								this._ofVol2 = volume2;
							}
							if (volume3 != -1L)
							{
								fontColor = Utilities.ComparePriceCFColor(price3, this._seriesInfoTFEX);
								this.intzaTP.Records(2).Fields("offervolume").Text = volume3;
								this.intzaTP.Records(2).Fields("offervolume").FontColor = fontColor;
								if (Utilities.GetTopPriceZero(volume3, price3))
								{
									this.intzaTP.Records(2).Fields("offer").Text = "0.00";
								}
								else
								{
									this.intzaTP.Records(2).Fields("offer").Text = Utilities.PriceFormat(price3, this._seriesInfoTFEX.NumOfDec);
								}
								this.intzaTP.Records(2).Fields("offer").FontColor = fontColor;
								this._ofVol3 = volume3;
							}
							if (volume4 != -1L)
							{
								fontColor = Utilities.ComparePriceCFColor(price4, this._seriesInfoTFEX);
								this.intzaTP.Records(3).Fields("offervolume").Text = volume4;
								this.intzaTP.Records(3).Fields("offervolume").FontColor = fontColor;
								if (Utilities.GetTopPriceZero(volume4, price4))
								{
									this.intzaTP.Records(3).Fields("offer").Text = "0.00";
								}
								else
								{
									this.intzaTP.Records(3).Fields("offer").Text = Utilities.PriceFormat(price4, this._seriesInfoTFEX.NumOfDec);
								}
								this.intzaTP.Records(3).Fields("offer").FontColor = fontColor;
								this._ofVol4 = volume4;
							}
							if (volume5 != -1L)
							{
								fontColor = Utilities.ComparePriceCFColor(price5, this._seriesInfoTFEX);
								this.intzaTP.Records(4).Fields("offervolume").Text = volume5;
								this.intzaTP.Records(4).Fields("offervolume").FontColor = fontColor;
								if (Utilities.GetTopPriceZero(volume5, price5))
								{
									this.intzaTP.Records(4).Fields("offer").Text = "0.00";
								}
								else
								{
									this.intzaTP.Records(4).Fields("offer").Text = Utilities.PriceFormat(price5, this._seriesInfoTFEX.NumOfDec);
								}
								this.intzaTP.Records(4).Fields("offer").FontColor = fontColor;
								this._ofVol5 = volume5;
							}
						}
					}
					long num = this._bVol1 + this._bVol2 + this._bVol3 + this._bVol4 + this._bVol5;
					long num2 = this._ofVol1 + this._ofVol2 + this._ofVol3 + this._ofVol4 + this._ofVol5;
					if (num + num2 > 0L)
					{
						this.intzaBF.Item("item").Text = Utilities.PriceFormat(num / (num + num2) * 100m, 2, "0");
					}
					else
					{
						this.intzaBF.Item("item").Text = "";
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateTopPriceInfo_TFEX", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateTfexAvg(decimal avg)
		{
			if (this.tStripMenu.InvokeRequired)
			{
				this.tStripMenu.Invoke(new frmMarketWatch.UpdateTfexAvgCallBack(this.UpdateTfexAvg), new object[]
				{
					avg
				});
			}
			else
			{
				try
				{
					this.tstbTfexAvg.Text = Utilities.PriceFormat(avg, this._seriesInfoTFEX.NumOfDec);
					this.tstbTfexAvg.ForeColor = Utilities.ComparePriceCFColor(avg, this._seriesInfoTFEX);
				}
				catch (Exception ex)
				{
					this.ShowError("UpdateAvg", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateTopPriceBBO_Options(int rowIndex, string type, string side, string volume, string price, SeriesList.SeriesInformation seriesInfoForOptionCrtl)
		{
			if (this.intzaOption.InvokeRequired)
			{
				frmMarketWatch.UpdateTopPriceBBOOptionsCallBack method = new frmMarketWatch.UpdateTopPriceBBOOptionsCallBack(this.UpdateTopPriceBBO_Options);
				this.intzaOption.Invoke(method, new object[]
				{
					rowIndex,
					type,
					side,
					volume,
					price,
					seriesInfoForOptionCrtl
				});
			}
			else
			{
				try
				{
					if (rowIndex > -1)
					{
						Color fontColor = Color.Yellow;
						if (volume != "-1")
						{
							if (type == "CALL")
							{
								this.intzaOption.Records(rowIndex).Fields("sSeriesOC").Text = seriesInfoForOptionCrtl.Symbol;
								if (side == "B")
								{
									fontColor = Utilities.ComparePriceCFColor(price, seriesInfoForOptionCrtl);
									this.intzaOption.Records(rowIndex).Fields("callbid").Text = Utilities.PriceFormat(price);
									this.intzaOption.Records(rowIndex).Fields("callbid").FontColor = fontColor;
									this.intzaOption.Records(rowIndex).Fields("callbidvol").Text = volume;
									this.intzaOption.Records(rowIndex).Fields("callbidvol").FontColor = fontColor;
								}
								else
								{
									fontColor = Utilities.ComparePriceCFColor(price, seriesInfoForOptionCrtl);
									this.intzaOption.Records(rowIndex).Fields("calloffer").Text = Utilities.PriceFormat(price);
									this.intzaOption.Records(rowIndex).Fields("calloffer").FontColor = fontColor;
									this.intzaOption.Records(rowIndex).Fields("calloffvol").Text = volume;
									this.intzaOption.Records(rowIndex).Fields("calloffvol").FontColor = fontColor;
								}
							}
							else
							{
								this.intzaOption.Records(rowIndex).Fields("sSeriesOP").Text = seriesInfoForOptionCrtl.Symbol;
								if (side == "B")
								{
									fontColor = Utilities.ComparePriceCFColor(price, seriesInfoForOptionCrtl);
									this.intzaOption.Records(rowIndex).Fields("putbid").Text = Utilities.PriceFormat(price);
									this.intzaOption.Records(rowIndex).Fields("putbid").FontColor = fontColor;
									this.intzaOption.Records(rowIndex).Fields("putbidvol").Text = volume;
									this.intzaOption.Records(rowIndex).Fields("putbidvol").FontColor = fontColor;
								}
								else
								{
									fontColor = Utilities.ComparePriceCFColor(price, seriesInfoForOptionCrtl);
									this.intzaOption.Records(rowIndex).Fields("putoffer").Text = Utilities.PriceFormat(price);
									this.intzaOption.Records(rowIndex).Fields("putoffer").FontColor = fontColor;
									this.intzaOption.Records(rowIndex).Fields("putoffvol").Text = volume;
									this.intzaOption.Records(rowIndex).Fields("putoffvol").FontColor = fontColor;
								}
							}
						}
					}
					this.intzaOption.Redraw();
				}
				catch (Exception ex)
				{
					this.ShowError("UpdateTopPriceBBO_Options", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateLastPriceBBOOption(int rowIndex, string type, decimal lastPrice, decimal chg, SeriesList.SeriesInformation stockInfo)
		{
			if (this.intzaOption.InvokeRequired)
			{
				this.intzaOption.Invoke(new frmMarketWatch.UpdateLastPriceBBOOptionsCallBack(this.UpdateLastPriceBBOOption), new object[]
				{
					rowIndex,
					type,
					lastPrice,
					chg,
					stockInfo
				});
			}
			else
			{
				try
				{
					Color fontColor = Color.Yellow;
					if (lastPrice > 0m)
					{
						fontColor = Utilities.ComparePriceCFColor(lastPrice, stockInfo);
					}
					if (type == "CALL")
					{
						this.intzaOption.Records(rowIndex).Fields("calllast").Text = lastPrice;
						this.intzaOption.Records(rowIndex).Fields("calllast").FontColor = fontColor;
						this.intzaOption.Records(rowIndex).Fields("callchg").Text = chg;
						this.intzaOption.Records(rowIndex).Fields("callchg").FontColor = fontColor;
					}
					else
					{
						this.intzaOption.Records(rowIndex).Fields("putlast").Text = lastPrice;
						this.intzaOption.Records(rowIndex).Fields("putlast").FontColor = fontColor;
						this.intzaOption.Records(rowIndex).Fields("putchg").Text = chg;
						this.intzaOption.Records(rowIndex).Fields("putchg").FontColor = fontColor;
					}
				}
				catch (Exception ex)
				{
					this.ShowError("UpdateLastPriceBBOOption", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReceiveTfexMessage(IBroadcastMessage message, SeriesList.SeriesInformation realtimeSeriesInfo)
		{
			if (this.FormState != ClientBaseForm.ClientBaseFormState.Closing)
			{
				try
				{
					if (!this.IsInfoLoading && !this._currentIsSET)
					{
						if (this._seriesInfoTFEX != null && realtimeSeriesInfo != null)
						{
							string text = message.MessageType;
							switch (text)
							{
							case "TP":
								if (realtimeSeriesInfo.Symbol == this._seriesInfoTFEX.Symbol)
								{
									TPMessageTFEX tPMessageTFEX = (TPMessageTFEX)message;
									this.UpdateTopPriceInfo_TFEX(tPMessageTFEX.OrderBookId, tPMessageTFEX.Side, tPMessageTFEX.Price1, tPMessageTFEX.Price2, tPMessageTFEX.Price3, tPMessageTFEX.Price4, tPMessageTFEX.Price5, (long)tPMessageTFEX.Vol1, (long)tPMessageTFEX.Vol2, (long)tPMessageTFEX.Vol3, (long)tPMessageTFEX.Vol4, (long)tPMessageTFEX.Vol5);
									if (base.IsAllowRender)
									{
										this.intzaTP.EndUpdate();
									}
								}
								break;
							case "LS":
								if (realtimeSeriesInfo.Symbol == this._seriesInfoTFEX.Symbol)
								{
									LSMessageTFEX lSMessageTFEX = (LSMessageTFEX)message;
									if (lSMessageTFEX.DealSource == 20)
									{
										if (ApplicationInfo.IndexInfoTfex.TXIState != "3C")
										{
											this.UpdateOpenOrProjectOpenPriceTFEX(ApplicationInfo.IndexInfoTfex.TXIState, lSMessageTFEX.Price, realtimeSeriesInfo);
										}
										else
										{
											this.UpdateOpenOrProjectOpenPriceTFEX(ApplicationInfo.IndexInfoTfex.TXMState, lSMessageTFEX.Price, realtimeSeriesInfo);
										}
									}
									this.UpdateLastPrice(lSMessageTFEX.Price, "");
									this.UpdateHeaderPrice(lSMessageTFEX.High, lSMessageTFEX.Low);
									this.UpdateAllVolumeTFEX(lSMessageTFEX.Deals, (long)lSMessageTFEX.AccVolume, lSMessageTFEX.AccValue, (long)lSMessageTFEX.OpenQty, (long)lSMessageTFEX.LongQty, (long)lSMessageTFEX.ShortQty, lSMessageTFEX.OpenValue, lSMessageTFEX.LongValue, lSMessageTFEX.ShortValue, realtimeSeriesInfo.ContractSize, lSMessageTFEX.Basis);
									if (base.IsAllowRender)
									{
										this.intzaInfoTFEX.EndUpdate();
										this.intzaVolumeByBoard.EndUpdate();
									}
									int num = lSMessageTFEX.Vol;
									int num2;
									int.TryParse(num.ToString(), out num2);
									this.UpdateTickerInfo_TFEX(lSMessageTFEX.Price, lSMessageTFEX.Side, (long)num2, lSMessageTFEX.LastTime, -1);
									if (base.IsAllowRender)
									{
										this.intzaLS2.Redraw();
									}
								}
								break;
							case "PO":
								if (realtimeSeriesInfo.Symbol == this._seriesInfoTFEX.Symbol)
								{
									POMessageTFEX pOMessageTFEX = (POMessageTFEX)message;
									if (ApplicationInfo.IndexInfoTfex.TXIState != "3C")
									{
										this.UpdateOpenOrProjectOpenPriceTFEX(ApplicationInfo.IndexInfoTfex.TXIState, pOMessageTFEX.Equilibrium_price_I, realtimeSeriesInfo);
									}
									else
									{
										this.UpdateOpenOrProjectOpenPriceTFEX(ApplicationInfo.IndexInfoTfex.TXMState, pOMessageTFEX.Equilibrium_price_I, realtimeSeriesInfo);
									}
									if (base.IsAllowRender)
									{
										this.intzaInfoTFEX.EndUpdate();
									}
								}
								break;
							case "SD":
								if (realtimeSeriesInfo.Symbol == this._seriesInfoTFEX.Symbol)
								{
									SDMessageTFEX sDMessageTFEX = (SDMessageTFEX)message;
									if (sDMessageTFEX.Lastdate.ToString().Length == 8)
									{
										this.intzaInfoTFEX.Item("last_date").Text = Utilities.GetDateFormat(sDMessageTFEX.Lastdate.ToString());
									}
									IntzaBaseItem arg_487_0 = this.intzaInfoTFEX.Item("multiplier");
									int num = sDMessageTFEX.Price_quot_factor;
									arg_487_0.Text = num.ToString();
									if (base.IsAllowRender)
									{
										this.intzaInfoTFEX.EndUpdate();
									}
								}
								break;
							case "CA8":
								if (realtimeSeriesInfo.Symbol == this._seriesInfoTFEX.Symbol)
								{
									CA8MessageTFEX cA8MessageTFEX = (CA8MessageTFEX)message;
									if (ApplicationInfo.IndexInfoTfex.TXISession == 2)
									{
										this.intzaInfoTFEX.Item("settle").Text = Utilities.PriceFormat(cA8MessageTFEX.FixingPrice.ToString(), this._seriesInfoTFEX.NumOfDec);
										this.intzaInfoTFEX.Item("settle").FontColor = Utilities.ComparePriceColor(cA8MessageTFEX.FixingPrice, realtimeSeriesInfo.PrevFixPrice);
										if (realtimeSeriesInfo.MarketCode == 4 || realtimeSeriesInfo.MarketCode == 5)
										{
											this.intzaInfoTFEX.Item("psettle").Text = Utilities.PriceFormat(cA8MessageTFEX.FixingPrice, this._seriesInfoTFEX.NumOfDec);
											this.intzaInfoTFEX.Item("psettle").FontColor = Color.Yellow;
										}
									}
									else
									{
										this.intzaInfoTFEX.Item("psettle").Text = Utilities.PriceFormat(cA8MessageTFEX.FixingPrice, this._seriesInfoTFEX.NumOfDec);
										this.intzaInfoTFEX.Item("psettle").FontColor = Color.Yellow;
									}
									if (base.IsAllowRender)
									{
										this.intzaInfoTFEX.EndUpdate();
									}
								}
								break;
							case "BU10":
								if (realtimeSeriesInfo.Symbol == this._seriesInfoTFEX.Symbol)
								{
									BU10MessageTFEX bU10MessageTFEX = (BU10MessageTFEX)message;
									this.intzaInfoTFEX.Item("tickSize").Text = Utilities.PriceFormat(bU10MessageTFEX.StepSize, this._seriesInfoTFEX.NumOfDec);
									if (base.IsAllowRender)
									{
										this.intzaInfoTFEX.EndUpdate();
									}
								}
								break;
							case "TCF":
								if (realtimeSeriesInfo.Symbol == this._seriesInfoTFEX.Symbol)
								{
									TCFMessageTFEX tCFMessageTFEX = (TCFMessageTFEX)message;
									this.intzaInfoTFEX.Item("ceiling").Text = FormatUtil.PriceFormat(realtimeSeriesInfo.Ceiling.ToString(), realtimeSeriesInfo.NumOfDec, "");
									this.intzaInfoTFEX.Item("floor").Text = FormatUtil.PriceFormat(realtimeSeriesInfo.Floor.ToString(), realtimeSeriesInfo.NumOfDec, "");
									this.intzaInfoTFEX.Item("psettle").Text = Utilities.PriceFormat(tCFMessageTFEX.PrevFixPrice, realtimeSeriesInfo.NumOfDec);
									this.intzaInfoTFEX.Item("psettle").FontColor = Color.Yellow;
									if (base.IsAllowRender)
									{
										this.intzaInfoTFEX.EndUpdate();
									}
								}
								break;
							}
						}
						else
						{
							if (message.MessageType == "IU")
							{
								IUMessageTFEX iUMessageTFEX = (IUMessageTFEX)message;
								if (this._seriesInfoTFEX != null && this._seriesInfoTFEX.UnderOrderBookId == iUMessageTFEX.OrderBookId)
								{
									if (this._seriesInfoTFEX.Group == 1 && this._seriesInfoTFEX.LastSalePrice != 0m)
									{
										this.intzaInfoTFEX.Item("basis").Text = (iUMessageTFEX.LastIndx - this._seriesInfoTFEX.StrikPrice).ToString();
										this.intzaInfoTFEX.Item("basis").FontColor = Utilities.ComparePriceColor(iUMessageTFEX.LastIndx - this._seriesInfoTFEX.StrikPrice, 0m);
									}
									else
									{
										if (this._seriesInfoTFEX.Group == 2 && this._seriesInfoTFEX.LastSalePrice != 0m)
										{
											this.intzaInfoTFEX.Item("basis").Text = (this._seriesInfoTFEX.StrikPrice - iUMessageTFEX.LastIndx).ToString();
											this.intzaInfoTFEX.Item("basis").FontColor = Utilities.ComparePriceColor(this._seriesInfoTFEX.StrikPrice - iUMessageTFEX.LastIndx, 0m);
										}
										else
										{
											if (this._seriesInfoTFEX.Group != 1 && this._seriesInfoTFEX.Group != 2 && this._seriesInfoTFEX.LastSalePrice != 0m && iUMessageTFEX.LastIndx != 0m)
											{
												this.intzaInfoTFEX.Item("basis").Text = (this._seriesInfoTFEX.LastSalePrice - iUMessageTFEX.LastIndx).ToString();
												this.intzaInfoTFEX.Item("basis").FontColor = Utilities.ComparePriceColor(this._seriesInfoTFEX.LastSalePrice - iUMessageTFEX.LastIndx, 0m);
											}
										}
									}
									this.intzaInfoTFEX.Item("lastIndex").Text = iUMessageTFEX.LastIndx.ToString();
									this.intzaInfoTFEX.Item("lastIndex").FontColor = Color.Yellow;
								}
								if (base.IsAllowRender)
								{
									this.intzaInfoTFEX.EndUpdate();
								}
							}
						}
					}
					if (!this.IsBBOLoading)
					{
						string text = this._mainGroupType;
						if (text != null)
						{
							if (!(text == "Futures") && !(text == "Option"))
							{
								if (text == "Favorites")
								{
									this.ReceiveMessageBBO_TFEX(message, realtimeSeriesInfo);
								}
							}
							else
							{
								if (this._subGroupType == "SET50Option")
								{
									this.ReceiveMessageBBO_Options(message, realtimeSeriesInfo);
								}
								else
								{
									this.ReceiveMessageBBO_TFEX(message, realtimeSeriesInfo);
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("ReceiveTfexMessage", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ReceiveMessageBBO_Options(IBroadcastMessage message, SeriesList.SeriesInformation realtimeSeriesInfo)
		{
			try
			{
				string messageType = message.MessageType;
				if (messageType != null)
				{
					if (!(messageType == "TP"))
					{
						if (messageType == "LS")
						{
							if (realtimeSeriesInfo != null)
							{
								int num = this.intzaOption.FindIndex("strike", realtimeSeriesInfo.StrikPrice.ToString().Replace(".000000", ""));
								if (num > -1)
								{
									LSMessageTFEX lSMessageTFEX = (LSMessageTFEX)message;
									if (realtimeSeriesInfo.Group == 1 && lSMessageTFEX.Sec == this.intzaOption.Records(num).Fields("sSeriesOC").Text.ToString())
									{
										this.UpdateLastPriceBBOOption(num, "CALL", lSMessageTFEX.Price, lSMessageTFEX.Price - realtimeSeriesInfo.PrevFixPrice, realtimeSeriesInfo);
										if (base.IsAllowRender)
										{
											this.intzaOption.EndUpdate(num);
										}
									}
									else
									{
										if (realtimeSeriesInfo.Group == 2 && lSMessageTFEX.Sec == this.intzaOption.Records(num).Fields("sSeriesOP").Text.ToString())
										{
											this.UpdateLastPriceBBOOption(num, "PUT", lSMessageTFEX.Price, lSMessageTFEX.Price - realtimeSeriesInfo.PrevFixPrice, realtimeSeriesInfo);
											if (base.IsAllowRender)
											{
												this.intzaOption.EndUpdate(num);
											}
										}
									}
								}
							}
						}
					}
					else
					{
						if (realtimeSeriesInfo != null)
						{
							string keyValue = Utilities.PriceFormat(realtimeSeriesInfo.StrikPrice);
							int num = this.intzaOption.FindIndex("strike", keyValue);
							if (num > -1)
							{
								TPMessageTFEX tPMessageTFEX = (TPMessageTFEX)message;
								if (realtimeSeriesInfo.Group == 1 && tPMessageTFEX.OrderBookId.ToString() == this.intzaOption.Records(num).Fields("sSeriesOC").Text.ToString())
								{
									this.UpdateTopPriceBBO_Options(num, "CALL", tPMessageTFEX.Side, tPMessageTFEX.Vol1.ToString(), tPMessageTFEX.Price1.ToString(), realtimeSeriesInfo);
									if (base.IsAllowRender)
									{
										this.intzaOption.EndUpdate(num);
									}
								}
								else
								{
									if (realtimeSeriesInfo.Group == 2 && tPMessageTFEX.OrderBookId.ToString() == this.intzaOption.Records(num).Fields("sSeriesOP").Text.ToString())
									{
										this.UpdateTopPriceBBO_Options(num, "PUT", tPMessageTFEX.Side, tPMessageTFEX.Vol1.ToString(), tPMessageTFEX.Price1.ToString(), realtimeSeriesInfo);
										if (base.IsAllowRender)
										{
											this.intzaOption.EndUpdate(num);
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
				this.ShowError("SecurityInfo::RecvMessage", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ReceiveMessageBBO_TFEX(IBroadcastMessage message, SeriesList.SeriesInformation realtimeSeriesInfo)
		{
			try
			{
				if (realtimeSeriesInfo != null)
				{
					string messageType = message.MessageType;
					if (messageType != null)
					{
						if (!(messageType == "TP"))
						{
							if (!(messageType == "LS"))
							{
								if (!(messageType == "CA8"))
								{
									if (messageType == "TCF")
									{
										if (realtimeSeriesInfo.Symbol != "")
										{
											int num = this.intzaBBO.FindIndex("stock", realtimeSeriesInfo.Symbol);
											if (num > -1)
											{
												TCFMessageTFEX tCFMessageTFEX = (TCFMessageTFEX)message;
												STIControl.ExpandTableGrid.RecordItem recordItem = this.intzaBBO.Records(num);
												recordItem.Fields("prior").Text = Utilities.PriceFormat(tCFMessageTFEX.PrevFixPrice, realtimeSeriesInfo.NumOfDec);
												if (base.IsAllowRender)
												{
													this.intzaBBO.EndUpdate(num);
												}
											}
										}
									}
								}
								else
								{
									if (realtimeSeriesInfo.Symbol != "")
									{
										int num = this.intzaBBO.FindIndex("stock", realtimeSeriesInfo.Symbol);
										if (num > -1)
										{
											CA8MessageTFEX cA8MessageTFEX = (CA8MessageTFEX)message;
											STIControl.ExpandTableGrid.RecordItem recordItem = this.intzaBBO.Records(num);
											recordItem.Fields("prior").Text = Utilities.PriceFormat(cA8MessageTFEX.FixingPrice, realtimeSeriesInfo.NumOfDec);
											if (base.IsAllowRender)
											{
												this.intzaBBO.EndUpdate(num);
											}
										}
									}
								}
							}
							else
							{
								if (realtimeSeriesInfo.Symbol != "")
								{
									int num = this.intzaBBO.FindIndex("stock", realtimeSeriesInfo.Symbol);
									if (num > -1)
									{
										LSMessageTFEX lSMessageTFEX = (LSMessageTFEX)message;
										this.UpdateLastPriceBBO_TFEX(num, lSMessageTFEX.Price, realtimeSeriesInfo.PrevFixPrice, (long)lSMessageTFEX.AccVolume, lSMessageTFEX.AccValue, (long)lSMessageTFEX.Deals, "", realtimeSeriesInfo, lSMessageTFEX.High, lSMessageTFEX.Low, (long)lSMessageTFEX.LongQty, (long)lSMessageTFEX.ShortQty);
										this.ShowUnderLineBBOTFEX(num, lSMessageTFEX.Price, realtimeSeriesInfo.BidPrice1.ToString(), realtimeSeriesInfo.OfferPrice1.ToString());
										if (base.IsAllowRender)
										{
											this.intzaBBO.EndUpdate(num);
										}
									}
								}
							}
						}
						else
						{
							if (realtimeSeriesInfo.Symbol != string.Empty)
							{
								int num = this.intzaBBO.FindIndex("stock", realtimeSeriesInfo.Symbol);
								if (num > -1)
								{
									TPMessageTFEX tPMessageTFEX = (TPMessageTFEX)message;
									this.UpdateTopPriceBBO_TFEX(num, tPMessageTFEX.Side, tPMessageTFEX.Price1.ToString(), (long)tPMessageTFEX.Vol1, realtimeSeriesInfo.PrevFixPrice, realtimeSeriesInfo.LastSalePrice, realtimeSeriesInfo);
									if (this.intzaBBO.Records(num).Rows > 1)
									{
										this.UpdateBBOBidsTFEX(this.intzaBBO.Records(num), realtimeSeriesInfo, tPMessageTFEX.Side, tPMessageTFEX.Price2.ToString(), (long)tPMessageTFEX.Vol2, tPMessageTFEX.Price3.ToString(), (long)tPMessageTFEX.Vol3, tPMessageTFEX.Price4.ToString(), (long)tPMessageTFEX.Vol4, tPMessageTFEX.Price5.ToString(), (long)tPMessageTFEX.Vol5);
									}
									if (base.IsAllowRender)
									{
										this.intzaBBO.EndUpdate(num);
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SecurityInfo::RecvMessage", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowUnderLineBBOTFEX(int rowIndex, decimal lastPrice, string bidPrice, string offerPrice)
		{
			try
			{
				decimal d;
				decimal.TryParse(bidPrice, out d);
				if (d == lastPrice)
				{
					this.intzaBBO.Records(rowIndex).Fields("bid").FontStyle = FontStyle.Underline;
				}
				else
				{
					this.intzaBBO.Records(rowIndex).Fields("bid").FontStyle = FontStyle.Regular;
				}
				decimal.TryParse(offerPrice, out d);
				if (d == lastPrice)
				{
					this.intzaBBO.Records(rowIndex).Fields("offer").FontStyle = FontStyle.Underline;
				}
				else
				{
					this.intzaBBO.Records(rowIndex).Fields("offer").FontStyle = FontStyle.Regular;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateUnderLineBBO_RealtimeLS_TFEX", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void RequestWebOptionsData()
		{
			string text = string.Empty;
			IFormatProvider provider = new CultureInfo("en-US", true);
			try
			{
				if (this._set50OptList == null)
				{
					this._set50OptList = Utilities.GetFullOptionsName();
				}
				if (this._set50OptList.ContainsValue(this._currentBBOpage))
				{
					string text2 = this.findMyValue(this._set50OptList, this._currentBBOpage);
					text = ApplicationInfo.WebServiceTFEX.BestBidOfferByOptionsList(text2.Trim());
					DateTime dateValue = Utilities.GetDateValue(text2.Trim());
					this._bboOptionsHeaderText = dateValue.ToString("dd MMM yyyy", provider);
					int num = dateValue.Subtract(DateTime.Now).Days + 1;
					object bboOptionsHeaderText = this._bboOptionsHeaderText;
					this._bboOptionsHeaderText = string.Concat(new object[]
					{
						bboOptionsHeaderText,
						"  ",
						num,
						" Days to expiration"
					});
				}
				if (!string.IsNullOrEmpty(text))
				{
					this.tdsSet50Option.Clear();
					MyDataHelper.StringToDataSet(text, this.tdsSet50Option);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("RequestWebOptionsData", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateToControl_BBO_TFEX(DataSet ds)
		{
			try
			{
				if (ds != null && ds.Tables.Contains("Table") && ds.Tables["Table"].Rows.Count > 0)
				{
					if (this._mainGroupType != "Favorites")
					{
						int count = ds.Tables["Table"].Rows.Count;
						this.intzaBBO.BeginUpdate();
						if (this.intzaBBO.Rows != count)
						{
							this.intzaBBO.Rows = count;
						}
						else
						{
							this.intzaBBO.ClearAllText();
						}
					}
					long num = 0L;
					long buyVol = 0L;
					long sellVol = 0L;
					long deals = 0L;
					decimal prior = 0m;
					decimal num2 = 0m;
					decimal num3 = 0m;
					decimal accValue = 0m;
					decimal highPrice = 0m;
					decimal lowPrice = 0m;
					int num4 = -1;
					foreach (DataRow dataRow in ds.Tables["Table"].Rows)
					{
						SeriesList.SeriesInformation seriesInformation = ApplicationInfo.SeriesInfo[dataRow["sSeries"].ToString().Trim()];
						if (this._mainGroupType == "Favorites")
						{
							num4 = ApplicationInfo.FavStockList[this._bboPage].IndexOf(seriesInformation.Symbol);
						}
						else
						{
							num4++;
						}
						if (num4 > -1)
						{
							this.intzaBBO.Records(num4).Changed = true;
							this.intzaBBO.Records(num4).Fields("stock").Text = seriesInformation.Symbol;
							long.TryParse(dataRow["iBidQuantity1"].ToString(), out num);
							decimal.TryParse(dataRow["nmrFixPrice"].ToString(), out num2);
							decimal.TryParse(dataRow["nmrPrevFixPrice"].ToString(), out prior);
							decimal.TryParse(dataRow["AccValue"].ToString(), out accValue);
							decimal.TryParse(dataRow["nmrPrice"].ToString(), out num3);
							decimal.TryParse(dataRow["nmrHigh"].ToString(), out highPrice);
							decimal.TryParse(dataRow["nmrLow"].ToString(), out lowPrice);
							long.TryParse(dataRow["iSumLongVol"].ToString(), out buyVol);
							long.TryParse(dataRow["iSumShortVol"].ToString(), out sellVol);
							long.TryParse(dataRow["NumOfDeal"].ToString(), out deals);
							this.UpdateTopPriceBBO_TFEX(num4, "B", dataRow["nmrBidPrice1"].ToString(), num, prior, num3, seriesInformation);
							long.TryParse(dataRow["iAskQuantity1"].ToString(), out num);
							this.UpdateTopPriceBBO_TFEX(num4, "A", dataRow["nmrAskPrice1"].ToString(), num, prior, num3, seriesInformation);
							long.TryParse(dataRow["iTurnOver"].ToString(), out num);
							this.UpdateLastPriceBBO_TFEX(num4, num3, prior, num, accValue, deals, "", seriesInformation, highPrice, lowPrice, buyVol, sellVol);
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateToControl_BBO_TFEX", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateLastPriceBBO_TFEX(int rowIndex, decimal lastPrice, decimal prior, long accVolume, decimal accValue, long deals, string comparePrice, SeriesList.SeriesInformation serieInfo, decimal highPrice, decimal lowPrice, long buyVol, long sellVol)
		{
			if (this.intzaBBO.InvokeRequired)
			{
				this.intzaBBO.Invoke(new frmMarketWatch.UpdateLastPriceBBO_TFEXCallBack(this.UpdateLastPriceBBO_TFEX), new object[]
				{
					rowIndex,
					lastPrice,
					prior,
					accVolume,
					accValue,
					deals,
					comparePrice,
					serieInfo,
					highPrice,
					lowPrice,
					buyVol,
					sellVol
				});
			}
			else
			{
				try
				{
					decimal num = 0m;
					decimal num2 = 0m;
					decimal num3 = 0m;
					if (lastPrice > 0m && prior > 0m)
					{
						num = lastPrice - prior;
						num2 = num / prior * 100m;
					}
					if (accVolume > 0L)
					{
						num3 = accValue / accVolume;
					}
					decimal num4 = 0m;
					decimal num5 = 0m;
					if (accVolume > 0L)
					{
						num5 = sellVol / accVolume * 100m;
						num4 = buyVol / accVolume * 100m;
					}
					Color fontColor = Color.Yellow;
					if (lastPrice > 0m)
					{
						fontColor = Utilities.ComparePriceCFColor(lastPrice, serieInfo);
					}
					STIControl.ExpandTableGrid.RecordItem recordItem = this.intzaBBO.Records(rowIndex);
					recordItem.Fields("prior").Text = Utilities.PriceFormat(prior, serieInfo.NumOfDec);
					recordItem.Fields("last").Text = Utilities.PriceFormat(lastPrice, serieInfo.NumOfDec);
					recordItem.Fields("chg").Text = num;
					recordItem.Fields("high").Text = highPrice;
					recordItem.Fields("low").Text = lowPrice;
					recordItem.Fields("avg").Text = num3;
					recordItem.Fields("pchg").Text = num2;
					recordItem.Fields("buyvolpct").Text = num4;
					recordItem.Fields("selvolpct").Text = num5;
					recordItem.Fields("mvol").Text = accVolume;
					recordItem.Fields("mval").Text = accValue * serieInfo.ContractSize / 1000m;
					recordItem.Fields("deals").Text = deals;
					recordItem.Fields("stock").FontColor = fontColor;
					recordItem.Fields("last").FontColor = fontColor;
					recordItem.Fields("chg").FontColor = fontColor;
					recordItem.Fields("pchg").FontColor = fontColor;
					recordItem.Fields("prior").FontColor = Color.Yellow;
					recordItem.Fields("mvol").FontColor = Color.Yellow;
					recordItem.Fields("mval").FontColor = Color.Yellow;
					recordItem.Fields("deals").FontColor = Color.Yellow;
					recordItem.Fields("high").FontColor = Utilities.ComparePriceCFColor(highPrice, serieInfo);
					recordItem.Fields("low").FontColor = Utilities.ComparePriceCFColor(lowPrice, serieInfo);
					recordItem.Fields("avg").FontColor = Utilities.ComparePriceCFColor(num3, serieInfo);
					recordItem.Fields("pchg").FontColor = fontColor;
					recordItem.Fields("buyvolpct").FontColor = Color.Cyan;
					recordItem.Fields("selvolpct").FontColor = Color.Magenta;
				}
				catch (Exception ex)
				{
					this.ShowError("UpdateLastPriceBBOTFEX", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateToControl_BBO_Option()
		{
			try
			{
				decimal num = 0m;
				this.tStripCall.Text = "CALL " + this._bboOptionsHeaderText;
				this.tStripPUT.Text = "PUT " + this._bboOptionsHeaderText;
				if (this.tdsSet50Option != null && this.tdsSet50Option.Tables.Contains("BBO_OptionList"))
				{
					int num2 = -1;
					this.intzaOption.Rows = this.tdsSet50Option.Tables["BBO_OptionList"].Rows.Count;
					if (this.tdsSet50Option.Tables.Contains("tbSET50") && this.tdsSet50Option.Tables["tbSET50"].Rows.Count > 0)
					{
						decimal.TryParse(this.tdsSet50Option.Tables["tbSET50"].Rows[0]["nmrLastIndex"].ToString(), out num);
						num = Math.Round(num / 10m) * 10m;
					}
					int num3 = -1;
					foreach (DataRow dataRow in this.tdsSet50Option.Tables["BBO_OptionList"].Rows)
					{
						SeriesList.SeriesInformation seriesInformation = ApplicationInfo.SeriesInfo[dataRow["sSeriesOC"].ToString().Trim()];
						SeriesList.SeriesInformation seriesInformation2 = ApplicationInfo.SeriesInfo[dataRow["sSeriesOP"].ToString().Trim()];
						if (seriesInformation != null && seriesInformation2 != null)
						{
							num2++;
							decimal num4;
							decimal.TryParse(dataRow["Strike"].ToString(), out num4);
							this.intzaOption.Records(num2).Changed = true;
							this.intzaOption.Records(num2).Fields("strike").Text = Utilities.PriceFormat(num4);
							this.intzaOption.Records(num2).Fields("strike").BackColor = Color.FromArgb(64, 64, 64);
							this.intzaOption.Records(num2).Fields("strike").FontColor = Color.Cyan;
							this.UpdateTopPriceBBO_Options(num2, "CALL", "B", dataRow["BidVolOC"].ToString(), dataRow["BidPriceOC"].ToString(), seriesInformation);
							this.UpdateTopPriceBBO_Options(num2, "CALL", "S", dataRow["AskVolOC"].ToString(), dataRow["AskPriceOC"].ToString(), seriesInformation);
							this.UpdateTopPriceBBO_Options(num2, "PUT", "B", dataRow["BidVolOP"].ToString(), dataRow["BidPriceOP"].ToString(), seriesInformation2);
							this.UpdateTopPriceBBO_Options(num2, "PUT", "S", dataRow["AskVolOP"].ToString(), dataRow["AskPriceOP"].ToString(), seriesInformation2);
							decimal lastPrice;
							decimal.TryParse(dataRow["PriceOC"].ToString(), out lastPrice);
							decimal chg;
							decimal.TryParse(dataRow["nmrChangOC"].ToString(), out chg);
							this.UpdateLastPriceBBOOption(num2, "CALL", lastPrice, chg, seriesInformation);
							decimal lastPrice2;
							decimal.TryParse(dataRow["PriceOP"].ToString(), out lastPrice2);
							decimal chg2;
							decimal.TryParse(dataRow["nmrChangOP"].ToString(), out chg2);
							this.UpdateLastPriceBBOOption(num2, "PUT", lastPrice2, chg2, seriesInformation2);
							if (num4 == num)
							{
								this.intzaOption.Records(num2).BackColor = Color.FromArgb(64, 64, 64);
								num3 = num2;
							}
						}
					}
					if (num3 > -1)
					{
						int num5 = 5;
						if (num3 - num5 > -1)
						{
							this.intzaOption.CurrentScroll = num3 - num5;
						}
						else
						{
							this.intzaOption.CurrentScroll = 0;
						}
					}
				}
				this.intzaOption.Redraw();
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateToControl_BBO_Option", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateTopPriceBBO_TFEX(int rowIndex, string side, string price, long volume, decimal prior, decimal lastSalePrice, SeriesList.SeriesInformation serieInfo)
		{
			if (this.intzaBBO.InvokeRequired)
			{
				this.intzaBBO.Invoke(new frmMarketWatch.UpdateTopPriceBBOTFEXCallBack(this.UpdateTopPriceBBO_TFEX), new object[]
				{
					rowIndex,
					side,
					price,
					volume,
					prior,
					lastSalePrice,
					serieInfo
				});
			}
			else
			{
				try
				{
					if (rowIndex > -1)
					{
						Color fontColor = Color.Yellow;
						if (side == "B")
						{
							if (volume != -1L)
							{
								if (Utilities.GetTopPriceZero(volume, price))
								{
									this.intzaBBO.Records(rowIndex).Fields("bid").Text = "0.00";
								}
								else
								{
									this.intzaBBO.Records(rowIndex).Fields("bid").Text = Utilities.PriceFormat(price, serieInfo.NumOfDec);
								}
								this.intzaBBO.Records(rowIndex).Fields("bidvol").Text = volume;
								if (Utilities.PriceFormat(lastSalePrice, serieInfo.NumOfDec) == Utilities.PriceFormat(price, serieInfo.NumOfDec))
								{
									if (ApplicationInfo.IndexInfoTfex.TXIState == "7" || ApplicationInfo.IndexInfoTfex.TXIState == "9")
									{
										this.intzaBBO.Records(rowIndex).Fields("bid").FontStyle = FontStyle.Underline;
									}
									else
									{
										this.intzaBBO.Records(rowIndex).Fields("bid").FontStyle = FontStyle.Underline;
										this.intzaBBO.Records(rowIndex).Fields("offer").FontStyle = FontStyle.Regular;
									}
								}
								else
								{
									this.intzaBBO.Records(rowIndex).Fields("bid").FontStyle = FontStyle.Regular;
								}
								fontColor = Utilities.ComparePriceCFColor(price, serieInfo);
								this.intzaBBO.Records(rowIndex).Fields("bid").FontColor = fontColor;
								this.intzaBBO.Records(rowIndex).Fields("bidvol").FontColor = fontColor;
							}
						}
						else
						{
							if (side == "A")
							{
								if (volume != -1L)
								{
									if (Utilities.GetTopPriceZero(volume, price))
									{
										this.intzaBBO.Records(rowIndex).Fields("offer").Text = "0.00";
									}
									else
									{
										this.intzaBBO.Records(rowIndex).Fields("offer").Text = Utilities.PriceFormat(price, serieInfo.NumOfDec);
									}
									this.intzaBBO.Records(rowIndex).Fields("offvol").Text = volume;
									if (Utilities.PriceFormat(lastSalePrice, serieInfo.NumOfDec) == Utilities.PriceFormat(price, serieInfo.NumOfDec))
									{
										if (ApplicationInfo.IndexInfoTfex.TXIState == "7" || ApplicationInfo.IndexInfoTfex.TXIState == "9")
										{
											this.intzaBBO.Records(rowIndex).Fields("offer").FontStyle = FontStyle.Underline;
										}
										else
										{
											this.intzaBBO.Records(rowIndex).Fields("bid").FontStyle = FontStyle.Regular;
											this.intzaBBO.Records(rowIndex).Fields("offer").FontStyle = FontStyle.Underline;
										}
									}
									else
									{
										this.intzaBBO.Records(rowIndex).Fields("offer").FontStyle = FontStyle.Regular;
									}
									fontColor = Utilities.ComparePriceCFColor(price, serieInfo);
									this.intzaBBO.Records(rowIndex).Fields("offer").FontColor = fontColor;
									this.intzaBBO.Records(rowIndex).Fields("offvol").FontColor = fontColor;
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("UpdateTopPriceBBOTFEX", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string findMyValue(Dictionary<string, string> myDic, string val)
		{
			string result;
			foreach (string current in myDic.Keys)
			{
				if (myDic[current] == val)
				{
					result = current;
					return result;
				}
			}
			result = string.Empty;
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateBBOBidsTFEX(STIControl.ExpandTableGrid.RecordItem rec, SeriesList.SeriesInformation sf, string side, string price2, long volume2, string price3, long volume3, string price4, long volume4, string price5, long volume5)
		{
			try
			{
				Color fontColor = Color.Yellow;
				if (side == "B")
				{
					if (volume2 != -1L)
					{
						fontColor = Utilities.ComparePriceCFColor(price2, sf);
						rec.SubRecord[0].Fields("bidvol").Text = volume2;
						rec.SubRecord[0].Fields("bidvol").FontColor = fontColor;
						rec.SubRecord[0].Fields("bid").Text = Utilities.BidOfferPriceFormat(price2, volume2);
						rec.SubRecord[0].Fields("bid").FontColor = fontColor;
					}
					if (volume3 != -1L)
					{
						fontColor = Utilities.ComparePriceCFColor(price3, sf);
						rec.SubRecord[1].Fields("bidvol").Text = volume3;
						rec.SubRecord[1].Fields("bidvol").FontColor = fontColor;
						rec.SubRecord[1].Fields("bid").Text = Utilities.BidOfferPriceFormat(price3, volume3);
						rec.SubRecord[1].Fields("bid").FontColor = fontColor;
					}
					if (rec.Rows == 5)
					{
						if (volume4 != -1L)
						{
							fontColor = Utilities.ComparePriceCFColor(price4, sf);
							rec.SubRecord[2].Fields("bidvol").Text = volume4;
							rec.SubRecord[2].Fields("bidvol").FontColor = fontColor;
							rec.SubRecord[2].Fields("bid").Text = Utilities.BidOfferPriceFormat(price4, volume4);
							rec.SubRecord[2].Fields("bid").FontColor = fontColor;
						}
						if (volume5 != -1L)
						{
							fontColor = Utilities.ComparePriceCFColor(price5, sf);
							rec.SubRecord[3].Fields("bidvol").Text = volume5;
							rec.SubRecord[3].Fields("bidvol").FontColor = fontColor;
							rec.SubRecord[3].Fields("bid").Text = Utilities.BidOfferPriceFormat(price5, volume5);
							rec.SubRecord[3].Fields("bid").FontColor = fontColor;
						}
					}
				}
				else
				{
					if (side == "A")
					{
						if (volume2 != -1L)
						{
							fontColor = Utilities.ComparePriceCFColor(price2, sf);
							rec.SubRecord[0].Fields("offvol").Text = volume2;
							rec.SubRecord[0].Fields("offvol").FontColor = fontColor;
							rec.SubRecord[0].Fields("offer").Text = Utilities.BidOfferPriceFormat(price2, volume2);
							rec.SubRecord[0].Fields("offer").FontColor = fontColor;
						}
						if (volume3 != -1L)
						{
							fontColor = Utilities.ComparePriceCFColor(price3, sf);
							rec.SubRecord[1].Fields("offvol").Text = volume3;
							rec.SubRecord[1].Fields("offvol").FontColor = fontColor;
							rec.SubRecord[1].Fields("offer").Text = Utilities.BidOfferPriceFormat(price3, volume3);
							rec.SubRecord[1].Fields("offer").FontColor = fontColor;
						}
						if (rec.Rows == 5)
						{
							if (volume4 != -1L)
							{
								fontColor = Utilities.ComparePriceCFColor(price4, sf);
								rec.SubRecord[2].Fields("offvol").Text = volume4;
								rec.SubRecord[2].Fields("offvol").FontColor = fontColor;
								rec.SubRecord[2].Fields("offer").Text = Utilities.BidOfferPriceFormat(price4, volume4);
								rec.SubRecord[2].Fields("offer").FontColor = fontColor;
							}
							if (volume5 != -1L)
							{
								fontColor = Utilities.ComparePriceCFColor(price5, sf);
								rec.SubRecord[3].Fields("offvol").Text = volume5;
								rec.SubRecord[3].Fields("offvol").FontColor = fontColor;
								rec.SubRecord[3].Fields("offer").Text = Utilities.BidOfferPriceFormat(price5, volume5);
								rec.SubRecord[3].Fields("offer").FontColor = fontColor;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateTopPrice", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnBBO_SET_Click(object sender, EventArgs e)
		{
			try
			{
				this.isBBOLoading = true;
				this._isDWGroup = false;
				this.tsbtnColumnSetup.Visible = true;
				if (sender == this.tsbtnBBO_FAV)
				{
					this.SetBBOGroup("Favorites", this._lastFAV);
				}
				else
				{
					if (sender == this.tsbtnBBO_SET)
					{
						this.SetBBOGroup("SET", this._lastSETsel);
					}
					else
					{
						if (sender == this.tsbtnBBO_FUTURE)
						{
							this.SetBBOGroup("Futures", this._lastFuturesSel);
						}
						else
						{
							if (sender == this.tsbtnBBO_Option)
							{
								this.tsbtnColumnSetup.Visible = false;
								this.SetBBOGroup("Option", this._lastOptionsSel);
							}
							else
							{
								if (sender == this.tsbtnBBODW)
								{
									this._isDWGroup = true;
									this.tsbtnColumnSetup.Visible = false;
									this.SetBBOGroup("DerivativeWarrant", this._lastDWsel);
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tsbtnBBO_SET_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetBBOGroup(string group, string page)
		{
			try
			{
				this.tsbtnBBO_FAV.ForeColor = Color.LightGray;
				this.tsbtnBBO_SET.ForeColor = Color.LightGray;
				this.tsbtnBBO_FUTURE.ForeColor = Color.LightGray;
				this.tsbtnBBO_Option.ForeColor = Color.LightGray;
				this.tsbtnBBODW.ForeColor = Color.LightGray;
				this.tscbBBOSelection.Enabled = false;
				this.tscbBBOSelection.Items.Clear();
				string text = group;
				if (text != null)
				{
					if (text == "Favorites" || text == "SET" || text == "DerivativeWarrant" || text == "Option" || text == "Futures")
					{
						goto IL_F8;
					}
				}
				group = "Favorites";
				page = string.Empty;
				IL_F8:
				if (group == "Favorites")
				{
					this.tsbtnBBO_FAV.ForeColor = Color.Aquamarine;
					this.tscbBBOSelection.Items.Add("Favorites-1");
					this.tscbBBOSelection.Items.Add("Favorites-2");
					this.tscbBBOSelection.Items.Add("Favorites-3");
					this.tscbBBOSelection.Items.Add("Favorites-4");
					this.tscbBBOSelection.Items.Add("Favorites-5");
				}
				else
				{
					if (group == "SET")
					{
						this.tsbtnBBO_SET.ForeColor = Color.Aquamarine;
						this.tscbBBOSelection.Items.Add("Most Active Value");
						this.tscbBBOSelection.Items.Add("Most Active Volume");
						this.tscbBBOSelection.Items.Add("Top Gainer");
						this.tscbBBOSelection.Items.Add("Top Loser");
						this.tscbBBOSelection.Items.Add("Top Swing");
						this.tscbBBOSelection.Items.Add("Top Open Price-1");
						this.tscbBBOSelection.Items.Add("Top Open Price-2");
						this.tscbBBOSelection.Items.Add("Top Projected Open");
						this.tscbBBOSelection.Items.Add("Top Projected Close");
						this.tscbBBOSelection.Items.Add("Big-Lot");
						this.tscbBBOSelection.Items.Add("-----------FLAG------------");
						this.tscbBBOSelection.Items.Add("Benefit");
						if (!ApplicationInfo.SupportFreewill)
						{
							this.tscbBBOSelection.Items.Add("Turnover List");
						}
						this.tscbBBOSelection.Items.Add("---------SECTOR---------");
						SortedDictionary<string, IndexStat.IndexItem> sortedDictionary = new SortedDictionary<string, IndexStat.IndexItem>();
						foreach (IndexStat.IndexItem current in ApplicationInfo.IndexStatInfo.Items)
						{
							if (current.Type == "S")
							{
								sortedDictionary.Add(current.Symbol, current);
							}
						}
						foreach (KeyValuePair<string, IndexStat.IndexItem> current2 in sortedDictionary)
						{
							this.tscbBBOSelection.Items.Add(current2.Value.Symbol);
						}
					}
					else
					{
						if (group == "DerivativeWarrant")
						{
							this.tsbtnBBODW.ForeColor = Color.Aquamarine;
							foreach (string current3 in ApplicationInfo.DWParentStockList)
							{
								this.tscbBBOSelection.Items.Add("." + current3);
							}
						}
						else
						{
							if (group == "Futures")
							{
								this.tsbtnBBO_FUTURE.ForeColor = Color.Aquamarine;
								this.tscbBBOSelection.Items.Add("Futures - Most Active Value");
								this.tscbBBOSelection.Items.Add("Futures - Most Active Volume");
								this.tscbBBOSelection.Items.Add("Futures - Gainer");
								this.tscbBBOSelection.Items.Add("Futures - Loser");
								this.tscbBBOSelection.Items.Add("Futures - Most Swing");
								this.tscbBBOSelection.Items.Add("-------------------------");
								foreach (string current4 in this._itemsUnderlying)
								{
									this.tscbBBOSelection.Items.Add(current4);
								}
							}
							else
							{
								if (!(group == "Option"))
								{
									return;
								}
								this.tsbtnBBO_Option.ForeColor = Color.Aquamarine;
								this.tscbBBOSelection.Items.Add("Options - Most Active Value");
								this.tscbBBOSelection.Items.Add("Options - Most Active Volume");
								this.tscbBBOSelection.Items.Add("Options - Gainer");
								this.tscbBBOSelection.Items.Add("Options - Loser");
								this.tscbBBOSelection.Items.Add("Options - Most Swing");
								this.tscbBBOSelection.Items.Add("-------------------------");
								if (this._set50OptList == null)
								{
									this._set50OptList = Utilities.GetFullOptionsName();
								}
								if (this._set50OptList != null)
								{
									foreach (KeyValuePair<string, string> current5 in this._set50OptList)
									{
										this.tscbBBOSelection.Items.Add(current5.Value);
									}
								}
							}
						}
					}
				}
				this._mainGroupType = group;
				this.tscbBBOSelection.Enabled = true;
				this.tsbtnBBOAddStock.Visible = (group == "Favorites");
				this.tsbtnBBOAddStock.Visible = (group == "Favorites");
				if (page == string.Empty)
				{
					if (this.tscbBBOSelection.Items.Count > 0)
					{
						page = this.tscbBBOSelection.Items[0].ToString();
					}
				}
				else
				{
					this.tscbBBOSelection.Text = page;
					if (group == "SET")
					{
						if (page.StartsWith("."))
						{
							if (this._lastSectorSortSel != string.Empty)
							{
								if (this._lastSectorSortSel == "Value")
								{
									this._lastSectorSortSel = "";
									this._lastSectorSortSel = "Value";
								}
								else
								{
									if (this._lastSectorSortSel == "Stock")
									{
										this._lastSectorSortSel = "";
										this._lastSectorSortSel = "Stock";
									}
									else
									{
										if (this._lastSectorSortSel == "%Change")
										{
											this._lastSectorSortSel = "";
											this._lastSectorSortSel = "%Change";
										}
									}
								}
							}
							else
							{
								if (this._lastSectorSortSel == string.Empty)
								{
									this._lastSectorSortSel = "Value";
								}
							}
							this.tscbSortByDW.Text = this._lastSectorSortSel;
						}
					}
					else
					{
						if (group == "DerivativeWarrant")
						{
							if (page.StartsWith("."))
							{
								if (this._lastDWSortSel != string.Empty)
								{
									if (this._lastDWSortSel == "Value")
									{
										this._lastDWSortSel = "";
										this._lastDWSortSel = "Value";
									}
									else
									{
										if (this._lastDWSortSel == "Stock")
										{
											this._lastDWSortSel = "";
											this._lastDWSortSel = "Stock";
										}
										else
										{
											if (this._lastDWSortSel == "%Change")
											{
												this._lastDWSortSel = "";
												this._lastDWSortSel = "%Change";
											}
										}
									}
								}
								else
								{
									if (this._lastDWSortSel == string.Empty)
									{
										this._lastDWSortSel = "Value";
										this._isDWGroup = true;
									}
								}
								this.tscbSortByDW.Text = this._lastDWSortSel;
							}
						}
					}
				}
				this.tscbBBOSelection.Text = page;
				this.SetBBOPage(page);
			}
			catch (Exception ex)
			{
				this.ShowError("SetBBOGroup", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string SaveFavFromGrid()
		{
			return string.Empty;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaOption_TableMouseClick(object sender, STIControl.ExpandTableGrid.TableMouseEventArgs e)
		{
			try
			{
				if (e.RowIndex > -1 && e.RowIndex <= this.intzaOption.Rows - 1)
				{
					string text = string.Empty;
					if (e.Column.Name == "callbidvol" || e.Column.Name == "callbid" || e.Column.Name == "calloffer" || e.Column.Name == "calloffvol" || e.Column.Name == "calllast" || e.Column.Name == "callchg")
					{
						text = this.intzaOption.Records(e.RowIndex).Fields("sSeriesOC").Text.ToString();
					}
					else
					{
						if (e.Column.Name == "putbidvol" || e.Column.Name == "putbid" || e.Column.Name == "putoffer" || e.Column.Name == "putoffvol" || e.Column.Name == "putlast" || e.Column.Name == "putchg")
						{
							text = this.intzaOption.Records(e.RowIndex).Fields("sSeriesOP").Text.ToString();
						}
					}
					if (!string.IsNullOrEmpty(text.Trim()))
					{
						this.SetNewStock_Info(text, false);
						if (Settings.Default.SmartOneClick)
						{
							string side = (e.Mouse.Button == MouseButtons.Left) ? "B" : "S";
							string price = "";
							if (e.Column.Name == "callbid" || e.Column.Name == "calloffer" || e.Column.Name == "calllast")
							{
								price = this.intzaOption.Records(e.RowIndex).Fields(e.Column.Name).Text.ToString();
							}
							else
							{
								if (e.Column.Name == "putbid" || e.Column.Name == "putoffer" || e.Column.Name == "putlast")
								{
									price = this.intzaOption.Records(e.RowIndex).Fields(e.Column.Name).Text.ToString();
								}
							}
							TemplateManager.Instance.MainForm.SendOrderBox.SetSmartOneClick(side, text, price, Settings.Default.SmartClickVolume);
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("TableMouseClick", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaBBO_TableHeaderMouseMove(STIControl.ExpandTableGrid.TableMouseEventArgs e)
		{
			try
			{
				if (e != null && e.Column != null && e.RowIndex == -1)
				{
					if (this._isTT != e.Column.Name)
					{
						this.toolTip1.Hide(this.intzaBBO);
						this._isTT = e.Column.Name;
						if (e.Column.Name == "po")
						{
							this.toolTip1.SetToolTip(this.intzaBBO, "Click Swap to Avg");
						}
						else
						{
							if (e.Column.Name == "avg")
							{
								this.toolTip1.SetToolTip(this.intzaBBO, "Click Swap to " + this.getPOorPC());
							}
							else
							{
								if (e.Column.Name == "chg")
								{
									this.toolTip1.SetToolTip(this.intzaBBO, "Click Swap to %Chg");
								}
								else
								{
									if (e.Column.Name == "pchg")
									{
										this.toolTip1.SetToolTip(this.intzaBBO, "Click Swap to Chg");
									}
									else
									{
										this.toolTip1.SetToolTip(this.intzaBBO, "");
									}
								}
							}
						}
					}
				}
				else
				{
					this._isTT = string.Empty;
					this.toolTip1.Hide(this.intzaBBO);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("intzaBBO_TableMouseMove", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string getPOorPC()
		{
			string result = string.Empty;
			string marketState = ApplicationInfo.MarketState;
			if (marketState != null)
			{
				if (marketState == "S" || marketState == "P" || marketState == "O" || marketState == "B")
				{
					result = "PO";
					return result;
				}
			}
			result = "PC";
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SwitchColumns(string currColumns)
		{
			if (this.intzaBBO.InvokeRequired)
			{
				this.intzaBBO.Invoke(new frmMarketWatch.SwitchColumnsCallBack(this.SwitchColumns), new object[]
				{
					currColumns
				});
			}
			else
			{
				try
				{
					if (currColumns == "po" || currColumns == "pc" || currColumns == "avg")
					{
						this.isBBOLoading = true;
						List<STIControl.ExpandTableGrid.ColumnItem> list = new List<STIControl.ExpandTableGrid.ColumnItem>();
						foreach (STIControl.ExpandTableGrid.ColumnItem current in this.intzaBBO.Columns)
						{
							list.Add(current);
						}
						STIControl.ExpandTableGrid.ColumnItem column = this.intzaBBO.GetColumn(currColumns);
						STIControl.ExpandTableGrid.ColumnItem columnItem = null;
						STIControl.ExpandTableGrid.ColumnItem columnItem2 = null;
						if (currColumns != null)
						{
							if (!(currColumns == "po"))
							{
								if (!(currColumns == "pc"))
								{
									if (currColumns == "avg")
									{
										columnItem = this.intzaBBO.GetColumn("pc");
										columnItem2 = this.intzaBBO.GetColumn("po");
									}
								}
								else
								{
									columnItem = this.intzaBBO.GetColumn("avg");
									columnItem2 = this.intzaBBO.GetColumn("po");
								}
							}
							else
							{
								columnItem = this.intzaBBO.GetColumn("pc");
								columnItem2 = this.intzaBBO.GetColumn("avg");
							}
						}
						if (!column.Visible)
						{
							column.Visible = true;
							if (columnItem.Visible)
							{
								list.Remove(column);
								int index = list.IndexOf(columnItem);
								list.Insert(index, column);
							}
							else
							{
								if (columnItem2.Visible)
								{
									list.Remove(column);
									int index = list.IndexOf(columnItem2);
									list.Insert(index, column);
								}
							}
							columnItem.Visible = false;
							columnItem2.Visible = false;
						}
						this.intzaBBO.Columns = list;
						this.intzaBBO.CalcColumnWidth();
						this.intzaBBO.Redraw();
					}
					else
					{
						if (currColumns == "chg" || currColumns == "pchg")
						{
							this.isBBOLoading = true;
							List<STIControl.ExpandTableGrid.ColumnItem> list = new List<STIControl.ExpandTableGrid.ColumnItem>();
							foreach (STIControl.ExpandTableGrid.ColumnItem current in this.intzaBBO.Columns)
							{
								list.Add(current);
							}
							STIControl.ExpandTableGrid.ColumnItem column = this.intzaBBO.GetColumn(currColumns);
							STIControl.ExpandTableGrid.ColumnItem columnItem = null;
							if (currColumns != null)
							{
								if (!(currColumns == "chg"))
								{
									if (currColumns == "pchg")
									{
										columnItem = this.intzaBBO.GetColumn("chg");
									}
								}
								else
								{
									columnItem = this.intzaBBO.GetColumn("pchg");
								}
							}
							if (!column.Visible)
							{
								column.Visible = true;
								if (columnItem.Visible)
								{
									list.Remove(column);
									int index = list.IndexOf(columnItem);
									list.Insert(index, column);
								}
								columnItem.Visible = false;
							}
							this.intzaBBO.Columns = list;
							this.intzaBBO.CalcColumnWidth();
							this.intzaBBO.Redraw();
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("SwitchColumns", ex);
				}
				this.isBBOLoading = false;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsmCallHistoricalChart_Click(object sender, EventArgs e)
		{
			if (this.contextLink.Tag != null && this.contextLink.Tag.ToString() != string.Empty)
			{
				TemplateManager.Instance.MainForm.SetTemplateLink("Historical Chart", "", this.contextLink.Tag.ToString());
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsmCallStockSummary_Click(object sender, EventArgs e)
		{
			if (this.contextLink.Tag != null && this.contextLink.Tag.ToString() != string.Empty)
			{
				TemplateManager.Instance.MainForm.SetTemplateLink("Stock Summary", "Stock in Play", this.contextLink.Tag.ToString());
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsmCallSaleByPrice_Click(object sender, EventArgs e)
		{
			if (this.contextLink.Tag != null && this.contextLink.Tag.ToString() != string.Empty)
			{
				TemplateManager.Instance.MainForm.SetTemplateLink("Stock Summary", "Sale by Price", this.contextLink.Tag.ToString());
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsmCallSaleByTime_Click(object sender, EventArgs e)
		{
			if (this.contextLink.Tag != null && this.contextLink.Tag.ToString() != string.Empty)
			{
				TemplateManager.Instance.MainForm.SetTemplateLink("Stock Summary", "Sale by Time", this.contextLink.Tag.ToString());
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsmCallOddlot_Click(object sender, EventArgs e)
		{
			if (this.contextLink.Tag != null && this.contextLink.Tag.ToString() != string.Empty)
			{
				TemplateManager.Instance.MainForm.SetTemplateLink("Stock Summary", "View Oddlot", this.contextLink.Tag.ToString());
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsmCallNews_Click(object sender, EventArgs e)
		{
			if (this.contextLink.Tag != null && this.contextLink.Tag.ToString() != string.Empty)
			{
				ApplicationInfo.NewsSymbol = this.contextLink.Tag.ToString();
				TemplateManager.Instance.MainForm.SetTemplateLink("News Center", string.Empty, this.contextLink.Tag.ToString());
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnVolAs_Click(object sender, EventArgs e)
		{
			if (this._chartVisible)
			{
				this._chartVisible = false;
				this.btnCloseChart.Visible = this._chartVisible;
				this.pictureBox1.Visible = this._chartVisible;
				this.tsbtnRefreshChart.Visible = this._chartVisible;
			}
			this._volAsVisible = !this._volAsVisible;
			if (this._volAsVisible)
			{
				this.ReloadVolAs();
				this.panelVolAs.Visible = true;
				this.panelVolAs.BringToFront();
			}
			else
			{
				this.panelVolAs.Hide();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ReloadVolAs()
		{
			try
			{
				if (this._volAsVisible)
				{
					if (this._bgwVolAs == null)
					{
						this._bgwVolAs = new BackgroundWorker();
						this._bgwVolAs.DoWork += new DoWorkEventHandler(this.bgwVolAs_DoWork);
						this._bgwVolAs.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwVolAs_RunWorkerCompleted);
					}
					if (!this._bgwVolAs.IsBusy)
					{
						this._bgwVolAs.RunWorkerAsync();
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("ReloadVolAs", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwVolAs_DoWork(object sender, DoWorkEventArgs e)
		{
			if (this.FormState != ClientBaseForm.ClientBaseFormState.Closing)
			{
				base.IsLoadingData = true;
				try
				{
					if (this._stockInfoSET != null)
					{
						string text = ApplicationInfo.WebService.SaleByPrice(this._stockInfoSET.Number, 1, 999, 0);
						if (!string.IsNullOrEmpty(text))
						{
							if (this.dsSaleByPrice == null)
							{
								this.dsSaleByPrice = new DataSet();
							}
							else
							{
								this.dsSaleByPrice.Clear();
							}
							MyDataHelper.StringToDataSet(text, this.dsSaleByPrice);
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("bgwSaleByPriceLoadData_DoWork", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwVolAs_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				if (this.FormState != ClientBaseForm.ClientBaseFormState.Closing)
				{
					if (e.Error == null)
					{
						if (this._stockInfoSET != null)
						{
							if (this.dsSaleByPrice.Tables.Contains("sale_by_price"))
							{
								this.wcGraphVolume.InitData(this._stockInfoSET.Symbol, (double)this._stockInfoSET.PriorPrice, (double)this._stockInfoSET.LastSalePrice, (double)this._stockInfoSET.Ceiling, (double)this._stockInfoSET.Floor);
								foreach (DataRow dataRow in this.dsSaleByPrice.Tables["sale_by_price"].Rows)
								{
									decimal value;
									decimal.TryParse(dataRow["price"].ToString(), out value);
									int num;
									int.TryParse(dataRow["buy_deals"].ToString(), out num);
									long num2;
									long.TryParse(dataRow["buy_volume"].ToString(), out num2);
									int.TryParse(dataRow["sell_deals"].ToString(), out num);
									long num3;
									long.TryParse(dataRow["sell_volume"].ToString(), out num3);
									long num4;
									long.TryParse(dataRow["other_volume"].ToString(), out num4);
									this.wcGraphVolume.InputData((double)value, num2 + num3 + num4, num2, num3);
								}
								this.wcGraphVolume.EndUpdate();
								this.wcGraphVolume.Sort();
							}
							this.dsSaleByPrice.Clear();
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("bgwSaleByPriceLoadData_RunWorkerCompleted", ex);
			}
			base.IsLoadingData = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnVolAsClose_Click(object sender, EventArgs e)
		{
			this._volAsVisible = false;
			this.panelVolAs.Hide();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void InitializeComponent()
		{
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmMarketWatch));
			STIControl.ExpandTableGrid.ColumnItem columnItem = new STIControl.ExpandTableGrid.ColumnItem();
			STIControl.ExpandTableGrid.ColumnItem columnItem2 = new STIControl.ExpandTableGrid.ColumnItem();
			STIControl.ExpandTableGrid.ColumnItem columnItem3 = new STIControl.ExpandTableGrid.ColumnItem();
			STIControl.ExpandTableGrid.ColumnItem columnItem4 = new STIControl.ExpandTableGrid.ColumnItem();
			STIControl.ExpandTableGrid.ColumnItem columnItem5 = new STIControl.ExpandTableGrid.ColumnItem();
			STIControl.ExpandTableGrid.ColumnItem columnItem6 = new STIControl.ExpandTableGrid.ColumnItem();
			STIControl.ExpandTableGrid.ColumnItem columnItem7 = new STIControl.ExpandTableGrid.ColumnItem();
			STIControl.ExpandTableGrid.ColumnItem columnItem8 = new STIControl.ExpandTableGrid.ColumnItem();
			STIControl.ExpandTableGrid.ColumnItem columnItem9 = new STIControl.ExpandTableGrid.ColumnItem();
			STIControl.ExpandTableGrid.ColumnItem columnItem10 = new STIControl.ExpandTableGrid.ColumnItem();
			STIControl.ExpandTableGrid.ColumnItem columnItem11 = new STIControl.ExpandTableGrid.ColumnItem();
			STIControl.ExpandTableGrid.ColumnItem columnItem12 = new STIControl.ExpandTableGrid.ColumnItem();
			STIControl.ExpandTableGrid.ColumnItem columnItem13 = new STIControl.ExpandTableGrid.ColumnItem();
			STIControl.ExpandTableGrid.ColumnItem columnItem14 = new STIControl.ExpandTableGrid.ColumnItem();
			STIControl.ExpandTableGrid.ColumnItem columnItem15 = new STIControl.ExpandTableGrid.ColumnItem();
			STIControl.ExpandTableGrid.ColumnItem columnItem16 = new STIControl.ExpandTableGrid.ColumnItem();
			STIControl.ExpandTableGrid.ColumnItem columnItem17 = new STIControl.ExpandTableGrid.ColumnItem();
			STIControl.ExpandTableGrid.ColumnItem columnItem18 = new STIControl.ExpandTableGrid.ColumnItem();
			STIControl.ExpandTableGrid.ColumnItem columnItem19 = new STIControl.ExpandTableGrid.ColumnItem();
			STIControl.ExpandTableGrid.ColumnItem columnItem20 = new STIControl.ExpandTableGrid.ColumnItem();
			STIControl.ExpandTableGrid.ColumnItem columnItem21 = new STIControl.ExpandTableGrid.ColumnItem();
			STIControl.ExpandTableGrid.ColumnItem columnItem22 = new STIControl.ExpandTableGrid.ColumnItem();
			STIControl.ExpandTableGrid.ColumnItem columnItem23 = new STIControl.ExpandTableGrid.ColumnItem();
			STIControl.ExpandTableGrid.ColumnItem columnItem24 = new STIControl.ExpandTableGrid.ColumnItem();
			STIControl.ExpandTableGrid.ColumnItem columnItem25 = new STIControl.ExpandTableGrid.ColumnItem();
			STIControl.ExpandTableGrid.ColumnItem columnItem26 = new STIControl.ExpandTableGrid.ColumnItem();
			STIControl.ExpandTableGrid.ColumnItem columnItem27 = new STIControl.ExpandTableGrid.ColumnItem();
			STIControl.ExpandTableGrid.ColumnItem columnItem28 = new STIControl.ExpandTableGrid.ColumnItem();
			STIControl.ExpandTableGrid.ColumnItem columnItem29 = new STIControl.ExpandTableGrid.ColumnItem();
			STIControl.ExpandTableGrid.ColumnItem columnItem30 = new STIControl.ExpandTableGrid.ColumnItem();
			STIControl.ExpandTableGrid.ColumnItem columnItem31 = new STIControl.ExpandTableGrid.ColumnItem();
			STIControl.ExpandTableGrid.ColumnItem columnItem32 = new STIControl.ExpandTableGrid.ColumnItem();
			STIControl.ExpandTableGrid.ColumnItem columnItem33 = new STIControl.ExpandTableGrid.ColumnItem();
			STIControl.ExpandTableGrid.ColumnItem columnItem34 = new STIControl.ExpandTableGrid.ColumnItem();
			ItemGrid itemGrid = new ItemGrid();
			ItemGrid itemGrid2 = new ItemGrid();
			ItemGrid itemGrid3 = new ItemGrid();
			ItemGrid itemGrid4 = new ItemGrid();
			ItemGrid itemGrid5 = new ItemGrid();
			ItemGrid itemGrid6 = new ItemGrid();
			ItemGrid itemGrid7 = new ItemGrid();
			ItemGrid itemGrid8 = new ItemGrid();
			ItemGrid itemGrid9 = new ItemGrid();
			ItemGrid itemGrid10 = new ItemGrid();
			ItemGrid itemGrid11 = new ItemGrid();
			ItemGrid itemGrid12 = new ItemGrid();
			ItemGrid itemGrid13 = new ItemGrid();
			ItemGrid itemGrid14 = new ItemGrid();
			ItemGrid itemGrid15 = new ItemGrid();
			ItemGrid itemGrid16 = new ItemGrid();
			ItemGrid itemGrid17 = new ItemGrid();
			ItemGrid itemGrid18 = new ItemGrid();
			ItemGrid itemGrid19 = new ItemGrid();
			ItemGrid itemGrid20 = new ItemGrid();
			ItemGrid itemGrid21 = new ItemGrid();
			ItemGrid itemGrid22 = new ItemGrid();
			ItemGrid itemGrid23 = new ItemGrid();
			ItemGrid itemGrid24 = new ItemGrid();
			ItemGrid itemGrid25 = new ItemGrid();
			ItemGrid itemGrid26 = new ItemGrid();
			ItemGrid itemGrid27 = new ItemGrid();
			ItemGrid itemGrid28 = new ItemGrid();
			ItemGrid itemGrid29 = new ItemGrid();
			ItemGrid itemGrid30 = new ItemGrid();
			ItemGrid itemGrid31 = new ItemGrid();
			ItemGrid itemGrid32 = new ItemGrid();
			ItemGrid itemGrid33 = new ItemGrid();
			ItemGrid itemGrid34 = new ItemGrid();
			ItemGrid itemGrid35 = new ItemGrid();
			ItemGrid itemGrid36 = new ItemGrid();
			ItemGrid itemGrid37 = new ItemGrid();
			ItemGrid itemGrid38 = new ItemGrid();
			ItemGrid itemGrid39 = new ItemGrid();
			ItemGrid itemGrid40 = new ItemGrid();
			ItemGrid itemGrid41 = new ItemGrid();
			ItemGrid itemGrid42 = new ItemGrid();
			ItemGrid itemGrid43 = new ItemGrid();
			ItemGrid itemGrid44 = new ItemGrid();
			ItemGrid itemGrid45 = new ItemGrid();
			STIControl.SortTableGrid.ColumnItem columnItem35 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem36 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem37 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem38 = new STIControl.SortTableGrid.ColumnItem();
			ItemGrid itemGrid46 = new ItemGrid();
			ItemGrid itemGrid47 = new ItemGrid();
			ItemGrid itemGrid48 = new ItemGrid();
			ItemGrid itemGrid49 = new ItemGrid();
			ItemGrid itemGrid50 = new ItemGrid();
			ItemGrid itemGrid51 = new ItemGrid();
			ItemGrid itemGrid52 = new ItemGrid();
			ItemGrid itemGrid53 = new ItemGrid();
			ItemGrid itemGrid54 = new ItemGrid();
			ItemGrid itemGrid55 = new ItemGrid();
			ItemGrid itemGrid56 = new ItemGrid();
			ItemGrid itemGrid57 = new ItemGrid();
			ItemGrid itemGrid58 = new ItemGrid();
			ItemGrid itemGrid59 = new ItemGrid();
			ItemGrid itemGrid60 = new ItemGrid();
			ItemGrid itemGrid61 = new ItemGrid();
			ItemGrid itemGrid62 = new ItemGrid();
			ItemGrid itemGrid63 = new ItemGrid();
			ItemGrid itemGrid64 = new ItemGrid();
			ItemGrid itemGrid65 = new ItemGrid();
			ItemGrid itemGrid66 = new ItemGrid();
			ItemGrid itemGrid67 = new ItemGrid();
			ItemGrid itemGrid68 = new ItemGrid();
			ItemGrid itemGrid69 = new ItemGrid();
			ItemGrid itemGrid70 = new ItemGrid();
			ItemGrid itemGrid71 = new ItemGrid();
			ItemGrid itemGrid72 = new ItemGrid();
			ItemGrid itemGrid73 = new ItemGrid();
			ItemGrid itemGrid74 = new ItemGrid();
			ItemGrid itemGrid75 = new ItemGrid();
			ItemGrid itemGrid76 = new ItemGrid();
			ItemGrid itemGrid77 = new ItemGrid();
			ItemGrid itemGrid78 = new ItemGrid();
			ItemGrid itemGrid79 = new ItemGrid();
			ItemGrid itemGrid80 = new ItemGrid();
			ItemGrid itemGrid81 = new ItemGrid();
			ItemGrid itemGrid82 = new ItemGrid();
			ItemGrid itemGrid83 = new ItemGrid();
			STIControl.SortTableGrid.ColumnItem columnItem39 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem40 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem41 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem42 = new STIControl.SortTableGrid.ColumnItem();
			ItemGrid itemGrid84 = new ItemGrid();
			STIControl.SortTableGrid.ColumnItem columnItem43 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem44 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem45 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem46 = new STIControl.SortTableGrid.ColumnItem();
			clsPermission clsPermission = new clsPermission();
			clsPermission clsPermission2 = new clsPermission();
			this.tStripMenu = new ToolStrip();
			this.tsStockLable = new ToolStripLabel();
			this.tstbStock = new ToolStripComboBox();
			this.tslbCompare = new ToolStripLabel();
			this.tsPrice = new ToolStripLabel();
			this.tsSectorName = new ToolStripLabel();
			this.tsSectorIndex = new ToolStripLabel();
			this.tsbtnRefreshChart = new ToolStripButton();
			this.tsbtnSwitchChart = new ToolStripButton();
			this.tsbtnHChart = new ToolStripButton();
			this.tsbtnSETNews = new ToolStripButton();
			this.tslbTfexHigh = new ToolStripLabel();
			this.tstbTfexHigh = new ToolStripLabel();
			this.tslbTfexLow = new ToolStripLabel();
			this.tstbTfexLow = new ToolStripLabel();
			this.tslbTfexAvg = new ToolStripLabel();
			this.tstbTfexAvg = new ToolStripLabel();
			this.tsbtnVolAs = new ToolStripButton();
			this.lbSplashInfo = new Label();
			this.tbStockBBO = new TextBox();
			this.lbBBOLoading = new Label();
			this.panelBidOffer = new Panel();
			this.tStripCP = new ToolStrip();
			this.tStripCall = new ToolStripLabel();
			this.tStripPUT = new ToolStripLabel();
			this.intzaOption = new ExpandGrid();
			this.tStripBBO = new ToolStrip();
			this.tsbtnBBO_FAV = new ToolStripButton();
			this.tsbtnBBO_SET = new ToolStripButton();
			this.tsbtnBBODW = new ToolStripButton();
			this.tsbtnBBO_FUTURE = new ToolStripButton();
			this.tsbtnBBO_Option = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.tslbSelection = new ToolStripLabel();
			this.tscbBBOSelection = new ToolStripComboBox();
			this.tsbtnBBOAddStock = new ToolStripButton();
			this.tsbtnBBODelStock = new ToolStripButton();
			this.tslbSortBy = new ToolStripLabel();
			this.tscbSortByDW = new ToolStripComboBox();
			this.tsbtnColumnSetup = new ToolStripButton();
			this.intzaBBO = new ExpandGrid();
			this.contextMenuStrip1 = new ContextMenuStrip(this.components);
			this.tsmiShowBestBO = new ToolStripMenuItem();
			this.tsmiShow3BO = new ToolStripMenuItem();
			this.tsmiShow5BO = new ToolStripMenuItem();
			this.pictureBox1 = new PictureBox();
			this.lbChartLoading = new Label();
			this.contextLink = new ContextMenuStrip(this.components);
			this.tsmCallHistoricalChart = new ToolStripMenuItem();
			this.tsmCallNews = new ToolStripMenuItem();
			this.toolStripMenuItem1 = new ToolStripSeparator();
			this.tsmCallStockInPlay = new ToolStripMenuItem();
			this.tsmCallSaleByPrice = new ToolStripMenuItem();
			this.tsmCallSaleByTime = new ToolStripMenuItem();
			this.tsmCallOddlot = new ToolStripMenuItem();
			this.btnCloseChart = new Button();
			this.intzaInfoTFEX = new IntzaCustomGrid();
			this.intzaVolumeByBoard = new SortGrid();
			this.intzaInfo = new IntzaCustomGrid();
			this.intzaLS2 = new SortGrid();
			this.intzaBF = new IntzaCustomGrid();
			this.intzaTP = new SortGrid();
			this.toolTip1 = new ToolTip(this.components);
			this.wcGraphVolume = new ucVolumeAtPrice();
			this.panelVolAs = new Panel();
			this.btnVolAsClose = new Button();
			this.tStripMenu.SuspendLayout();
			this.panelBidOffer.SuspendLayout();
			this.tStripCP.SuspendLayout();
			this.tStripBBO.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			this.contextLink.SuspendLayout();
			this.panelVolAs.SuspendLayout();
			base.SuspendLayout();
			this.tStripMenu.BackColor = Color.FromArgb(20, 20, 20);
			this.tStripMenu.BackgroundImageLayout = ImageLayout.None;
			this.tStripMenu.GripMargin = new Padding(0);
			this.tStripMenu.GripStyle = ToolStripGripStyle.Hidden;
			this.tStripMenu.Items.AddRange(new ToolStripItem[]
			{
				this.tsStockLable,
				this.tstbStock,
				this.tslbCompare,
				this.tsPrice,
				this.tsSectorName,
				this.tsSectorIndex,
				this.tsbtnRefreshChart,
				this.tsbtnSwitchChart,
				this.tsbtnHChart,
				this.tsbtnSETNews,
				this.tslbTfexHigh,
				this.tstbTfexHigh,
				this.tslbTfexLow,
				this.tstbTfexLow,
				this.tslbTfexAvg,
				this.tstbTfexAvg,
				this.tsbtnVolAs
			});
			this.tStripMenu.Location = new Point(0, 0);
			this.tStripMenu.Name = "tStripMenu";
			this.tStripMenu.Padding = new Padding(1, 1, 1, 2);
			this.tStripMenu.RenderMode = ToolStripRenderMode.Professional;
			this.tStripMenu.Size = new Size(963, 26);
			this.tStripMenu.TabIndex = 21;
			this.tStripMenu.TabStop = true;
			this.tsStockLable.BackColor = Color.Transparent;
			this.tsStockLable.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsStockLable.Font = new Font("Microsoft Sans Serif", 8.25f);
			this.tsStockLable.ForeColor = Color.LightGray;
			this.tsStockLable.ImageTransparentColor = Color.Magenta;
			this.tsStockLable.Name = "tsStockLable";
			this.tsStockLable.Padding = new Padding(1, 0, 2, 0);
			this.tsStockLable.Size = new Size(38, 20);
			this.tsStockLable.Text = "Stock";
			this.tstbStock.BackColor = Color.FromArgb(30, 30, 30);
			this.tstbStock.Font = new Font("Microsoft Sans Serif", 8.25f);
			this.tstbStock.ForeColor = Color.Yellow;
			this.tstbStock.Name = "tstbStock";
			this.tstbStock.Size = new Size(120, 23);
			this.tstbStock.SelectedIndexChanged += new EventHandler(this.tstbStock_SelectedIndexChanged);
			this.tstbStock.KeyUp += new KeyEventHandler(this.tstbStock_KeyUp);
			this.tstbStock.KeyDown += new KeyEventHandler(this.tstbStock_KeyDown);
			this.tstbStock.Leave += new EventHandler(this.tstbStock_Leave);
			this.tstbStock.KeyPress += new KeyPressEventHandler(this.tstbStock_KeyPress);
			this.tslbCompare.BackColor = Color.Transparent;
			this.tslbCompare.Font = new Font("Wingdings", 9f, FontStyle.Regular, GraphicsUnit.Point, 2);
			this.tslbCompare.ForeColor = Color.Lime;
			this.tslbCompare.Margin = new Padding(5, 1, 0, 2);
			this.tslbCompare.Name = "tslbCompare";
			this.tslbCompare.Size = new Size(0, 20);
			this.tslbCompare.Tag = "0";
			this.tsPrice.BackColor = Color.Transparent;
			this.tsPrice.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsPrice.Font = new Font("Microsoft Sans Serif", 8.25f);
			this.tsPrice.ForeColor = Color.Yellow;
			this.tsPrice.Name = "tsPrice";
			this.tsPrice.Padding = new Padding(0, 0, 2, 0);
			this.tsPrice.Size = new Size(30, 20);
			this.tsPrice.Text = "0.00";
			this.tsSectorName.BackColor = Color.Transparent;
			this.tsSectorName.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsSectorName.Font = new Font("Microsoft Sans Serif", 8.25f);
			this.tsSectorName.ForeColor = Color.Aqua;
			this.tsSectorName.Margin = new Padding(2, 1, 2, 2);
			this.tsSectorName.Name = "tsSectorName";
			this.tsSectorName.Padding = new Padding(2, 0, 2, 0);
			this.tsSectorName.Size = new Size(42, 20);
			this.tsSectorName.Text = "Sector";
			this.tsSectorIndex.BackColor = Color.Transparent;
			this.tsSectorIndex.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsSectorIndex.Font = new Font("Microsoft Sans Serif", 8.25f);
			this.tsSectorIndex.ForeColor = Color.Yellow;
			this.tsSectorIndex.Name = "tsSectorIndex";
			this.tsSectorIndex.Padding = new Padding(2, 0, 3, 0);
			this.tsSectorIndex.Size = new Size(33, 20);
			this.tsSectorIndex.Text = "0.00";
			this.tsbtnRefreshChart.Alignment = ToolStripItemAlignment.Right;
			this.tsbtnRefreshChart.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbtnRefreshChart.Image = Resources.refresh;
			this.tsbtnRefreshChart.ImageTransparentColor = Color.Magenta;
			this.tsbtnRefreshChart.Name = "tsbtnRefreshChart";
			this.tsbtnRefreshChart.Size = new Size(23, 20);
			this.tsbtnRefreshChart.Text = "Reload Chart";
			this.tsbtnRefreshChart.Click += new EventHandler(this.tsbtnRefreshChart_Click);
			this.tsbtnSwitchChart.Alignment = ToolStripItemAlignment.Right;
			this.tsbtnSwitchChart.BackColor = Color.Transparent;
			this.tsbtnSwitchChart.ForeColor = Color.LightGray;
			this.tsbtnSwitchChart.Image = (Image)componentResourceManager.GetObject("tsbtnSwitchChart.Image");
			this.tsbtnSwitchChart.ImageTransparentColor = Color.Magenta;
			this.tsbtnSwitchChart.Margin = new Padding(2, 1, 2, 2);
			this.tsbtnSwitchChart.Name = "tsbtnSwitchChart";
			this.tsbtnSwitchChart.Size = new Size(23, 20);
			this.tsbtnSwitchChart.ToolTipText = "Intraday Chart";
			this.tsbtnSwitchChart.Click += new EventHandler(this.tsbtnSwitchChart_Click);
			this.tsbtnHChart.Alignment = ToolStripItemAlignment.Right;
			this.tsbtnHChart.ForeColor = Color.WhiteSmoke;
			this.tsbtnHChart.Image = (Image)componentResourceManager.GetObject("tsbtnHChart.Image");
			this.tsbtnHChart.ImageTransparentColor = Color.Magenta;
			this.tsbtnHChart.Margin = new Padding(2, 1, 2, 2);
			this.tsbtnHChart.Name = "tsbtnHChart";
			this.tsbtnHChart.Size = new Size(23, 20);
			this.tsbtnHChart.ToolTipText = "Historical Chart";
			this.tsbtnHChart.Click += new EventHandler(this.tsbtnHChart_Click);
			this.tsbtnSETNews.Alignment = ToolStripItemAlignment.Right;
			this.tsbtnSETNews.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbtnSETNews.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.tsbtnSETNews.ForeColor = Color.SandyBrown;
			this.tsbtnSETNews.Image = (Image)componentResourceManager.GetObject("tsbtnSETNews.Image");
			this.tsbtnSETNews.ImageTransparentColor = Color.Magenta;
			this.tsbtnSETNews.Margin = new Padding(2, 1, 2, 2);
			this.tsbtnSETNews.Name = "tsbtnSETNews";
			this.tsbtnSETNews.Size = new Size(23, 20);
			this.tsbtnSETNews.Tag = "S";
			this.tsbtnSETNews.Text = "News";
			this.tsbtnSETNews.ToolTipText = "SET News";
			this.tsbtnSETNews.Click += new EventHandler(this.tsbtnSETNews_Click);
			this.tslbTfexHigh.BackColor = Color.Black;
			this.tslbTfexHigh.ForeColor = Color.Gainsboro;
			this.tslbTfexHigh.Margin = new Padding(1, 1, 5, 2);
			this.tslbTfexHigh.Name = "tslbTfexHigh";
			this.tslbTfexHigh.Size = new Size(33, 20);
			this.tslbTfexHigh.Text = "High";
			this.tstbTfexHigh.BackColor = Color.Black;
			this.tstbTfexHigh.ForeColor = Color.Yellow;
			this.tstbTfexHigh.Name = "tstbTfexHigh";
			this.tstbTfexHigh.Padding = new Padding(1, 0, 1, 0);
			this.tstbTfexHigh.Size = new Size(30, 20);
			this.tstbTfexHigh.Text = "0.00";
			this.tslbTfexLow.BackColor = Color.Black;
			this.tslbTfexLow.ForeColor = Color.Gainsboro;
			this.tslbTfexLow.Margin = new Padding(1, 1, 5, 2);
			this.tslbTfexLow.Name = "tslbTfexLow";
			this.tslbTfexLow.Size = new Size(29, 20);
			this.tslbTfexLow.Text = "Low";
			this.tstbTfexLow.BackColor = Color.Black;
			this.tstbTfexLow.ForeColor = Color.Yellow;
			this.tstbTfexLow.Name = "tstbTfexLow";
			this.tstbTfexLow.Padding = new Padding(1, 0, 1, 0);
			this.tstbTfexLow.Size = new Size(30, 20);
			this.tstbTfexLow.Text = "0.00";
			this.tslbTfexAvg.BackColor = Color.Black;
			this.tslbTfexAvg.ForeColor = Color.Gainsboro;
			this.tslbTfexAvg.Margin = new Padding(1, 1, 5, 2);
			this.tslbTfexAvg.Name = "tslbTfexAvg";
			this.tslbTfexAvg.Size = new Size(28, 20);
			this.tslbTfexAvg.Text = "Avg";
			this.tstbTfexAvg.BackColor = Color.Black;
			this.tstbTfexAvg.ForeColor = Color.Yellow;
			this.tstbTfexAvg.Name = "tstbTfexAvg";
			this.tstbTfexAvg.Padding = new Padding(1, 0, 1, 0);
			this.tstbTfexAvg.Size = new Size(30, 20);
			this.tstbTfexAvg.Text = "0.00";
			this.tsbtnVolAs.Alignment = ToolStripItemAlignment.Right;
			this.tsbtnVolAs.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbtnVolAs.Image = (Image)componentResourceManager.GetObject("tsbtnVolAs.Image");
			this.tsbtnVolAs.ImageTransparentColor = Color.Magenta;
			this.tsbtnVolAs.Name = "tsbtnVolAs";
			this.tsbtnVolAs.Size = new Size(23, 20);
			this.tsbtnVolAs.ToolTipText = "Volume Analysis";
			this.tsbtnVolAs.Click += new EventHandler(this.tsbtnVolAs_Click);
			this.lbSplashInfo.AutoSize = true;
			this.lbSplashInfo.BackColor = Color.FromArgb(64, 64, 64);
			this.lbSplashInfo.BorderStyle = BorderStyle.FixedSingle;
			this.lbSplashInfo.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.lbSplashInfo.ForeColor = Color.Yellow;
			this.lbSplashInfo.Location = new Point(602, 140);
			this.lbSplashInfo.Name = "lbSplashInfo";
			this.lbSplashInfo.Padding = new Padding(5, 3, 5, 3);
			this.lbSplashInfo.Size = new Size(69, 21);
			this.lbSplashInfo.TabIndex = 75;
			this.lbSplashInfo.Text = "Loading ...";
			this.lbSplashInfo.TextAlign = ContentAlignment.MiddleCenter;
			this.lbSplashInfo.Visible = false;
			this.tbStockBBO.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			this.tbStockBBO.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.tbStockBBO.BackColor = Color.WhiteSmoke;
			this.tbStockBBO.BorderStyle = BorderStyle.FixedSingle;
			this.tbStockBBO.CharacterCasing = CharacterCasing.Upper;
			this.tbStockBBO.ForeColor = Color.Black;
			this.tbStockBBO.Location = new Point(8, 80);
			this.tbStockBBO.Margin = new Padding(0);
			this.tbStockBBO.MaxLength = 12;
			this.tbStockBBO.Name = "tbStockBBO";
			this.tbStockBBO.Size = new Size(83, 20);
			this.tbStockBBO.TabIndex = 68;
			this.tbStockBBO.Visible = false;
			this.tbStockBBO.KeyDown += new KeyEventHandler(this.tbStockBBO_KeyDown);
			this.tbStockBBO.Leave += new EventHandler(this.tbStockBBO_Leave);
			this.tbStockBBO.Enter += new EventHandler(this.tbStockBBO_Enter);
			this.lbBBOLoading.AutoSize = true;
			this.lbBBOLoading.BackColor = Color.FromArgb(64, 64, 64);
			this.lbBBOLoading.BorderStyle = BorderStyle.FixedSingle;
			this.lbBBOLoading.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.lbBBOLoading.ForeColor = Color.Yellow;
			this.lbBBOLoading.Location = new Point(602, 166);
			this.lbBBOLoading.Name = "lbBBOLoading";
			this.lbBBOLoading.Padding = new Padding(5, 3, 5, 3);
			this.lbBBOLoading.Size = new Size(69, 21);
			this.lbBBOLoading.TabIndex = 73;
			this.lbBBOLoading.Text = "Loading ...";
			this.lbBBOLoading.TextAlign = ContentAlignment.MiddleCenter;
			this.lbBBOLoading.Visible = false;
			this.panelBidOffer.Controls.Add(this.tStripCP);
			this.panelBidOffer.Controls.Add(this.intzaOption);
			this.panelBidOffer.Controls.Add(this.tStripBBO);
			this.panelBidOffer.Controls.Add(this.tbStockBBO);
			this.panelBidOffer.Controls.Add(this.intzaBBO);
			this.panelBidOffer.Location = new Point(2, 192);
			this.panelBidOffer.Margin = new Padding(0);
			this.panelBidOffer.Name = "panelBidOffer";
			this.panelBidOffer.Size = new Size(952, 197);
			this.panelBidOffer.TabIndex = 1;
			this.panelBidOffer.Leave += new EventHandler(this.panelBidOffer_Leave);
			this.panelBidOffer.Enter += new EventHandler(this.panelBidOffer_Enter);
			this.tStripCP.AllowMerge = false;
			this.tStripCP.BackColor = Color.FromArgb(20, 20, 20);
			this.tStripCP.GripMargin = new Padding(0);
			this.tStripCP.GripStyle = ToolStripGripStyle.Hidden;
			this.tStripCP.Items.AddRange(new ToolStripItem[]
			{
				this.tStripCall,
				this.tStripPUT
			});
			this.tStripCP.Location = new Point(0, 26);
			this.tStripCP.Name = "tStripCP";
			this.tStripCP.Padding = new Padding(0);
			this.tStripCP.RenderMode = ToolStripRenderMode.System;
			this.tStripCP.Size = new Size(952, 25);
			this.tStripCP.TabIndex = 81;
			this.tStripCP.Text = "toolStrip1";
			this.tStripCP.Visible = false;
			this.tStripCall.BackColor = Color.Black;
			this.tStripCall.ForeColor = Color.Cyan;
			this.tStripCall.Margin = new Padding(0, 1, 3, 2);
			this.tStripCall.Name = "tStripCall";
			this.tStripCall.Padding = new Padding(2, 0, 2, 0);
			this.tStripCall.Size = new Size(16, 22);
			this.tStripCall.Text = "-";
			this.tStripPUT.Alignment = ToolStripItemAlignment.Right;
			this.tStripPUT.BackColor = Color.Black;
			this.tStripPUT.ForeColor = Color.Cyan;
			this.tStripPUT.Margin = new Padding(0, 1, 3, 2);
			this.tStripPUT.Name = "tStripPUT";
			this.tStripPUT.Padding = new Padding(2, 0, 2, 0);
			this.tStripPUT.Size = new Size(16, 22);
			this.tStripPUT.Text = "-";
			this.intzaOption.AllowDrop = true;
			this.intzaOption.BackColor = Color.Black;
			this.intzaOption.CanBlink = true;
			this.intzaOption.CanDrag = false;
			this.intzaOption.CanGetMouseMove = false;
			columnItem.Alignment = StringAlignment.Near;
			columnItem.BackColor = Color.FromArgb(64, 64, 64);
			columnItem.FontColor = Color.LightGray;
			columnItem.IsExpand = false;
			columnItem.MyStyle = FontStyle.Regular;
			columnItem.Name = "callbidvol";
			columnItem.Text = "Vol";
			columnItem.ValueFormat = FormatType.BidOfferVolume;
			columnItem.Visible = true;
			columnItem.Width = 5;
			columnItem2.Alignment = StringAlignment.Near;
			columnItem2.BackColor = Color.FromArgb(64, 64, 64);
			columnItem2.FontColor = Color.LightGray;
			columnItem2.IsExpand = false;
			columnItem2.MyStyle = FontStyle.Regular;
			columnItem2.Name = "callbid";
			columnItem2.Text = "Bid";
			columnItem2.ValueFormat = FormatType.Price;
			columnItem2.Visible = true;
			columnItem2.Width = 9;
			columnItem3.Alignment = StringAlignment.Near;
			columnItem3.BackColor = Color.FromArgb(64, 64, 64);
			columnItem3.FontColor = Color.LightGray;
			columnItem3.IsExpand = false;
			columnItem3.MyStyle = FontStyle.Regular;
			columnItem3.Name = "calloffer";
			columnItem3.Text = "Ask";
			columnItem3.ValueFormat = FormatType.Text;
			columnItem3.Visible = true;
			columnItem3.Width = 9;
			columnItem4.Alignment = StringAlignment.Near;
			columnItem4.BackColor = Color.FromArgb(64, 64, 64);
			columnItem4.FontColor = Color.LightGray;
			columnItem4.IsExpand = false;
			columnItem4.MyStyle = FontStyle.Regular;
			columnItem4.Name = "calloffvol";
			columnItem4.Text = "Vol";
			columnItem4.ValueFormat = FormatType.BidOfferVolume;
			columnItem4.Visible = true;
			columnItem4.Width = 5;
			columnItem5.Alignment = StringAlignment.Near;
			columnItem5.BackColor = Color.FromArgb(64, 64, 64);
			columnItem5.FontColor = Color.LightGray;
			columnItem5.IsExpand = false;
			columnItem5.MyStyle = FontStyle.Regular;
			columnItem5.Name = "calllast";
			columnItem5.Text = "Last";
			columnItem5.ValueFormat = FormatType.Price;
			columnItem5.Visible = true;
			columnItem5.Width = 8;
			columnItem6.Alignment = StringAlignment.Near;
			columnItem6.BackColor = Color.FromArgb(64, 64, 64);
			columnItem6.FontColor = Color.LightGray;
			columnItem6.IsExpand = false;
			columnItem6.MyStyle = FontStyle.Regular;
			columnItem6.Name = "callchg";
			columnItem6.Text = "Chg";
			columnItem6.ValueFormat = FormatType.ChangePrice;
			columnItem6.Visible = true;
			columnItem6.Width = 8;
			columnItem7.Alignment = StringAlignment.Center;
			columnItem7.BackColor = Color.FromArgb(64, 64, 64);
			columnItem7.FontColor = Color.LightGray;
			columnItem7.IsExpand = false;
			columnItem7.MyStyle = FontStyle.Regular;
			columnItem7.Name = "strike";
			columnItem7.Text = "Strike Price";
			columnItem7.ValueFormat = FormatType.Price;
			columnItem7.Visible = true;
			columnItem7.Width = 12;
			columnItem8.Alignment = StringAlignment.Near;
			columnItem8.BackColor = Color.FromArgb(64, 64, 64);
			columnItem8.FontColor = Color.LightGray;
			columnItem8.IsExpand = false;
			columnItem8.MyStyle = FontStyle.Regular;
			columnItem8.Name = "putbidvol";
			columnItem8.Text = "Vol";
			columnItem8.ValueFormat = FormatType.BidOfferVolume;
			columnItem8.Visible = true;
			columnItem8.Width = 5;
			columnItem9.Alignment = StringAlignment.Near;
			columnItem9.BackColor = Color.FromArgb(64, 64, 64);
			columnItem9.FontColor = Color.LightGray;
			columnItem9.IsExpand = false;
			columnItem9.MyStyle = FontStyle.Regular;
			columnItem9.Name = "putbid";
			columnItem9.Text = "Bid";
			columnItem9.ValueFormat = FormatType.Text;
			columnItem9.Visible = true;
			columnItem9.Width = 9;
			columnItem10.Alignment = StringAlignment.Near;
			columnItem10.BackColor = Color.FromArgb(64, 64, 64);
			columnItem10.FontColor = Color.LightGray;
			columnItem10.IsExpand = false;
			columnItem10.MyStyle = FontStyle.Regular;
			columnItem10.Name = "putoffer";
			columnItem10.Text = "Ask";
			columnItem10.ValueFormat = FormatType.Text;
			columnItem10.Visible = true;
			columnItem10.Width = 9;
			columnItem11.Alignment = StringAlignment.Near;
			columnItem11.BackColor = Color.FromArgb(64, 64, 64);
			columnItem11.FontColor = Color.LightGray;
			columnItem11.IsExpand = false;
			columnItem11.MyStyle = FontStyle.Regular;
			columnItem11.Name = "putoffvol";
			columnItem11.Text = "Vol";
			columnItem11.ValueFormat = FormatType.BidOfferVolume;
			columnItem11.Visible = true;
			columnItem11.Width = 5;
			columnItem12.Alignment = StringAlignment.Near;
			columnItem12.BackColor = Color.FromArgb(64, 64, 64);
			columnItem12.FontColor = Color.LightGray;
			columnItem12.IsExpand = false;
			columnItem12.MyStyle = FontStyle.Regular;
			columnItem12.Name = "putlast";
			columnItem12.Text = "Last";
			columnItem12.ValueFormat = FormatType.Price;
			columnItem12.Visible = true;
			columnItem12.Width = 8;
			columnItem13.Alignment = StringAlignment.Near;
			columnItem13.BackColor = Color.FromArgb(64, 64, 64);
			columnItem13.FontColor = Color.LightGray;
			columnItem13.IsExpand = false;
			columnItem13.MyStyle = FontStyle.Regular;
			columnItem13.Name = "putchg";
			columnItem13.Text = "Chg";
			columnItem13.ValueFormat = FormatType.ChangePrice;
			columnItem13.Visible = true;
			columnItem13.Width = 8;
			columnItem14.Alignment = StringAlignment.Near;
			columnItem14.BackColor = Color.FromArgb(64, 64, 64);
			columnItem14.FontColor = Color.LightGray;
			columnItem14.IsExpand = false;
			columnItem14.MyStyle = FontStyle.Regular;
			columnItem14.Name = "sSeriesOC";
			columnItem14.Text = "IntzaItem";
			columnItem14.ValueFormat = FormatType.Text;
			columnItem14.Visible = false;
			columnItem14.Width = 10;
			columnItem15.Alignment = StringAlignment.Near;
			columnItem15.BackColor = Color.FromArgb(64, 64, 64);
			columnItem15.FontColor = Color.LightGray;
			columnItem15.IsExpand = false;
			columnItem15.MyStyle = FontStyle.Regular;
			columnItem15.Name = "sSeriesOP";
			columnItem15.Text = "IntzaItem";
			columnItem15.ValueFormat = FormatType.Text;
			columnItem15.Visible = false;
			columnItem15.Width = 10;
			this.intzaOption.Columns.Add(columnItem);
			this.intzaOption.Columns.Add(columnItem2);
			this.intzaOption.Columns.Add(columnItem3);
			this.intzaOption.Columns.Add(columnItem4);
			this.intzaOption.Columns.Add(columnItem5);
			this.intzaOption.Columns.Add(columnItem6);
			this.intzaOption.Columns.Add(columnItem7);
			this.intzaOption.Columns.Add(columnItem8);
			this.intzaOption.Columns.Add(columnItem9);
			this.intzaOption.Columns.Add(columnItem10);
			this.intzaOption.Columns.Add(columnItem11);
			this.intzaOption.Columns.Add(columnItem12);
			this.intzaOption.Columns.Add(columnItem13);
			this.intzaOption.Columns.Add(columnItem14);
			this.intzaOption.Columns.Add(columnItem15);
			this.intzaOption.CurrentScroll = 0;
			this.intzaOption.Cursor = Cursors.Hand;
			this.intzaOption.FocusItemIndex = -1;
			this.intzaOption.ForeColor = Color.Black;
			this.intzaOption.GridColor = Color.FromArgb(45, 45, 45);
			this.intzaOption.HeaderPctHeight = 80f;
			this.intzaOption.IsAutoRepaint = true;
			this.intzaOption.IsDrawGrid = true;
			this.intzaOption.IsDrawHeader = true;
			this.intzaOption.IsScrollable = true;
			this.intzaOption.Location = new Point(134, 114);
			this.intzaOption.MainColumn = "";
			this.intzaOption.Margin = new Padding(0);
			this.intzaOption.Name = "intzaOption";
			this.intzaOption.Rows = 0;
			this.intzaOption.RowSelectColor = Color.FromArgb(50, 50, 50);
			this.intzaOption.RowSelectType = 2;
			this.intzaOption.ScrollChennelColor = Color.Gray;
			this.intzaOption.Size = new Size(684, 56);
			this.intzaOption.SortColumnName = "";
			this.intzaOption.SortType = SortType.Desc;
			this.intzaOption.TabIndex = 80;
			this.intzaOption.TableMouseClick += new ExpandGrid.TableMouseClickEventHandler(this.intzaOption_TableMouseClick);
			this.tStripBBO.AllowMerge = false;
			this.tStripBBO.BackColor = Color.FromArgb(20, 20, 20);
			this.tStripBBO.GripMargin = new Padding(0);
			this.tStripBBO.GripStyle = ToolStripGripStyle.Hidden;
			this.tStripBBO.Items.AddRange(new ToolStripItem[]
			{
				this.tsbtnBBO_FAV,
				this.tsbtnBBO_SET,
				this.tsbtnBBODW,
				this.tsbtnBBO_FUTURE,
				this.tsbtnBBO_Option,
				this.toolStripSeparator1,
				this.tslbSelection,
				this.tscbBBOSelection,
				this.tsbtnBBOAddStock,
				this.tsbtnBBODelStock,
				this.tslbSortBy,
				this.tscbSortByDW,
				this.tsbtnColumnSetup
			});
			this.tStripBBO.Location = new Point(0, 0);
			this.tStripBBO.Name = "tStripBBO";
			this.tStripBBO.Padding = new Padding(1, 1, 1, 2);
			this.tStripBBO.RenderMode = ToolStripRenderMode.Professional;
			this.tStripBBO.Size = new Size(952, 26);
			this.tStripBBO.TabIndex = 78;
			this.tStripBBO.Tag = "-1";
			this.tsbtnBBO_FAV.ForeColor = Color.LightGray;
			this.tsbtnBBO_FAV.Image = (Image)componentResourceManager.GetObject("tsbtnBBO_FAV.Image");
			this.tsbtnBBO_FAV.ImageTransparentColor = Color.Magenta;
			this.tsbtnBBO_FAV.Name = "tsbtnBBO_FAV";
			this.tsbtnBBO_FAV.Size = new Size(74, 20);
			this.tsbtnBBO_FAV.Text = "Favorites";
			this.tsbtnBBO_FAV.Click += new EventHandler(this.tsbtnBBO_SET_Click);
			this.tsbtnBBO_SET.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnBBO_SET.ForeColor = Color.LightGray;
			this.tsbtnBBO_SET.ImageTransparentColor = Color.Magenta;
			this.tsbtnBBO_SET.Name = "tsbtnBBO_SET";
			this.tsbtnBBO_SET.Size = new Size(30, 20);
			this.tsbtnBBO_SET.Text = "SET";
			this.tsbtnBBO_SET.Click += new EventHandler(this.tsbtnBBO_SET_Click);
			this.tsbtnBBODW.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnBBODW.ForeColor = Color.LightGray;
			this.tsbtnBBODW.Image = (Image)componentResourceManager.GetObject("tsbtnBBODW.Image");
			this.tsbtnBBODW.ImageTransparentColor = Color.Magenta;
			this.tsbtnBBODW.Name = "tsbtnBBODW";
			this.tsbtnBBODW.Size = new Size(108, 20);
			this.tsbtnBBODW.Text = "Derivative Warrant";
			this.tsbtnBBODW.Click += new EventHandler(this.tsbtnBBO_SET_Click);
			this.tsbtnBBO_FUTURE.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnBBO_FUTURE.ForeColor = Color.LightGray;
			this.tsbtnBBO_FUTURE.ImageTransparentColor = Color.Magenta;
			this.tsbtnBBO_FUTURE.Name = "tsbtnBBO_FUTURE";
			this.tsbtnBBO_FUTURE.Size = new Size(50, 20);
			this.tsbtnBBO_FUTURE.Text = "Futures";
			this.tsbtnBBO_FUTURE.Click += new EventHandler(this.tsbtnBBO_SET_Click);
			this.tsbtnBBO_Option.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnBBO_Option.ForeColor = Color.LightGray;
			this.tsbtnBBO_Option.ImageTransparentColor = Color.Magenta;
			this.tsbtnBBO_Option.Name = "tsbtnBBO_Option";
			this.tsbtnBBO_Option.Size = new Size(53, 20);
			this.tsbtnBBO_Option.Text = "Options";
			this.tsbtnBBO_Option.Click += new EventHandler(this.tsbtnBBO_SET_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 23);
			this.tslbSelection.ForeColor = Color.LightGray;
			this.tslbSelection.Margin = new Padding(5, 1, 2, 2);
			this.tslbSelection.Name = "tslbSelection";
			this.tslbSelection.Size = new Size(61, 20);
			this.tslbSelection.Text = "Selection :";
			this.tscbBBOSelection.AutoSize = false;
			this.tscbBBOSelection.BackColor = Color.FromArgb(30, 30, 30);
			this.tscbBBOSelection.DropDownStyle = ComboBoxStyle.DropDownList;
			this.tscbBBOSelection.ForeColor = Color.LightGray;
			this.tscbBBOSelection.Name = "tscbBBOSelection";
			this.tscbBBOSelection.Size = new Size(170, 23);
			this.tscbBBOSelection.SelectedIndexChanged += new EventHandler(this.tscbSelection_SelectedIndexChanged);
			this.tsbtnBBOAddStock.ForeColor = Color.LightGray;
			this.tsbtnBBOAddStock.Image = (Image)componentResourceManager.GetObject("tsbtnBBOAddStock.Image");
			this.tsbtnBBOAddStock.ImageTransparentColor = Color.Magenta;
			this.tsbtnBBOAddStock.Margin = new Padding(10, 1, 0, 2);
			this.tsbtnBBOAddStock.Name = "tsbtnBBOAddStock";
			this.tsbtnBBOAddStock.Size = new Size(49, 20);
			this.tsbtnBBOAddStock.Text = "Add";
			this.tsbtnBBOAddStock.Click += new EventHandler(this.tsbtnAddStock_Click);
			this.tsbtnBBODelStock.ForeColor = Color.LightGray;
			this.tsbtnBBODelStock.Image = (Image)componentResourceManager.GetObject("tsbtnBBODelStock.Image");
			this.tsbtnBBODelStock.ImageTransparentColor = Color.Magenta;
			this.tsbtnBBODelStock.Name = "tsbtnBBODelStock";
			this.tsbtnBBODelStock.Size = new Size(60, 20);
			this.tsbtnBBODelStock.Text = "Delete";
			this.tsbtnBBODelStock.Click += new EventHandler(this.tsbtnBBODelStock_Click);
			this.tslbSortBy.ForeColor = Color.LightGray;
			this.tslbSortBy.Margin = new Padding(5, 1, 0, 2);
			this.tslbSortBy.Name = "tslbSortBy";
			this.tslbSortBy.Size = new Size(50, 20);
			this.tslbSortBy.Text = "Sort by :";
			this.tscbSortByDW.BackColor = Color.FromArgb(30, 30, 30);
			this.tscbSortByDW.DropDownStyle = ComboBoxStyle.DropDownList;
			this.tscbSortByDW.ForeColor = Color.LightGray;
			this.tscbSortByDW.Items.AddRange(new object[]
			{
				"Stock",
				"Value",
				"%Change"
			});
			this.tscbSortByDW.Name = "tscbSortByDW";
			this.tscbSortByDW.Size = new Size(120, 23);
			this.tscbSortByDW.SelectedIndexChanged += new EventHandler(this.tscbSortByDW_SelectedIndexChanged);
			this.tsbtnColumnSetup.Alignment = ToolStripItemAlignment.Right;
			this.tsbtnColumnSetup.ForeColor = Color.LightGray;
			this.tsbtnColumnSetup.Image = (Image)componentResourceManager.GetObject("tsbtnColumnSetup.Image");
			this.tsbtnColumnSetup.ImageTransparentColor = Color.Magenta;
			this.tsbtnColumnSetup.Name = "tsbtnColumnSetup";
			this.tsbtnColumnSetup.Size = new Size(75, 20);
			this.tsbtnColumnSetup.Text = "Columns";
			this.tsbtnColumnSetup.Click += new EventHandler(this.tsbtnColEdit_Click);
			this.intzaBBO.AllowDrop = true;
			this.intzaBBO.BackColor = Color.FromArgb(10, 10, 10);
			this.intzaBBO.CanBlink = true;
			this.intzaBBO.CanDrag = true;
			this.intzaBBO.CanGetMouseMove = true;
			columnItem16.Alignment = StringAlignment.Near;
			columnItem16.BackColor = Color.FromArgb(64, 64, 64);
			columnItem16.FontColor = Color.LightGray;
			columnItem16.IsExpand = true;
			columnItem16.MyStyle = FontStyle.Regular;
			columnItem16.Name = "stock";
			columnItem16.Text = "Stock";
			columnItem16.ValueFormat = FormatType.Symbol;
			columnItem16.Visible = true;
			columnItem16.Width = 13;
			columnItem17.Alignment = StringAlignment.Far;
			columnItem17.BackColor = Color.FromArgb(64, 64, 64);
			columnItem17.FontColor = Color.LightGray;
			columnItem17.IsExpand = false;
			columnItem17.MyStyle = FontStyle.Regular;
			columnItem17.Name = "bidvol";
			columnItem17.Text = "BidVol";
			columnItem17.ValueFormat = FormatType.BidOfferVolume;
			columnItem17.Visible = true;
			columnItem17.Width = 11;
			columnItem18.Alignment = StringAlignment.Far;
			columnItem18.BackColor = Color.FromArgb(64, 64, 64);
			columnItem18.FontColor = Color.LightGray;
			columnItem18.IsExpand = false;
			columnItem18.MyStyle = FontStyle.Regular;
			columnItem18.Name = "bid";
			columnItem18.Text = "Bid";
			columnItem18.ValueFormat = FormatType.Text;
			columnItem18.Visible = true;
			columnItem18.Width = 8;
			columnItem19.Alignment = StringAlignment.Far;
			columnItem19.BackColor = Color.FromArgb(64, 64, 64);
			columnItem19.FontColor = Color.LightGray;
			columnItem19.IsExpand = false;
			columnItem19.MyStyle = FontStyle.Regular;
			columnItem19.Name = "offer";
			columnItem19.Text = "Offer";
			columnItem19.ValueFormat = FormatType.Text;
			columnItem19.Visible = true;
			columnItem19.Width = 8;
			columnItem20.Alignment = StringAlignment.Far;
			columnItem20.BackColor = Color.FromArgb(64, 64, 64);
			columnItem20.FontColor = Color.LightGray;
			columnItem20.IsExpand = false;
			columnItem20.MyStyle = FontStyle.Regular;
			columnItem20.Name = "offvol";
			columnItem20.Text = "OffVol";
			columnItem20.ValueFormat = FormatType.BidOfferVolume;
			columnItem20.Visible = true;
			columnItem20.Width = 11;
			columnItem21.Alignment = StringAlignment.Far;
			columnItem21.BackColor = Color.FromArgb(64, 64, 64);
			columnItem21.FontColor = Color.LightGray;
			columnItem21.IsExpand = false;
			columnItem21.MyStyle = FontStyle.Regular;
			columnItem21.Name = "prior";
			columnItem21.Text = "Prior";
			columnItem21.ValueFormat = FormatType.Price;
			columnItem21.Visible = false;
			columnItem21.Width = 8;
			columnItem22.Alignment = StringAlignment.Far;
			columnItem22.BackColor = Color.FromArgb(64, 64, 64);
			columnItem22.FontColor = Color.LightGray;
			columnItem22.IsExpand = false;
			columnItem22.MyStyle = FontStyle.Regular;
			columnItem22.Name = "last";
			columnItem22.Text = "Last";
			columnItem22.ValueFormat = FormatType.PriceAndCompare;
			columnItem22.Visible = true;
			columnItem22.Width = 10;
			columnItem23.Alignment = StringAlignment.Far;
			columnItem23.BackColor = Color.FromArgb(64, 64, 64);
			columnItem23.FontColor = Color.LightGray;
			columnItem23.IsExpand = false;
			columnItem23.MyStyle = FontStyle.Regular;
			columnItem23.Name = "high";
			columnItem23.Text = "High";
			columnItem23.ValueFormat = FormatType.Price;
			columnItem23.Visible = false;
			columnItem23.Width = 8;
			columnItem24.Alignment = StringAlignment.Far;
			columnItem24.BackColor = Color.FromArgb(64, 64, 64);
			columnItem24.FontColor = Color.LightGray;
			columnItem24.IsExpand = false;
			columnItem24.MyStyle = FontStyle.Regular;
			columnItem24.Name = "low";
			columnItem24.Text = "Low";
			columnItem24.ValueFormat = FormatType.Price;
			columnItem24.Visible = false;
			columnItem24.Width = 8;
			columnItem25.Alignment = StringAlignment.Far;
			columnItem25.BackColor = Color.FromArgb(64, 64, 64);
			columnItem25.FontColor = Color.LightGray;
			columnItem25.IsExpand = false;
			columnItem25.MyStyle = FontStyle.Underline;
			columnItem25.Name = "chg";
			columnItem25.Text = "Chg";
			columnItem25.ValueFormat = FormatType.ChangePrice;
			columnItem25.Visible = true;
			columnItem25.Width = 8;
			columnItem26.Alignment = StringAlignment.Far;
			columnItem26.BackColor = Color.FromArgb(64, 64, 64);
			columnItem26.FontColor = Color.LightGray;
			columnItem26.IsExpand = false;
			columnItem26.MyStyle = FontStyle.Underline;
			columnItem26.Name = "pchg";
			columnItem26.Text = "%Chg";
			columnItem26.ValueFormat = FormatType.ChangePrice;
			columnItem26.Visible = false;
			columnItem26.Width = 8;
			columnItem27.Alignment = StringAlignment.Far;
			columnItem27.BackColor = Color.FromArgb(64, 64, 64);
			columnItem27.FontColor = Color.LightGray;
			columnItem27.IsExpand = false;
			columnItem27.MyStyle = FontStyle.Underline;
			columnItem27.Name = "avg";
			columnItem27.Text = "Avg";
			columnItem27.ValueFormat = FormatType.Price;
			columnItem27.Visible = true;
			columnItem27.Width = 9;
			columnItem28.Alignment = StringAlignment.Far;
			columnItem28.BackColor = Color.FromArgb(64, 64, 64);
			columnItem28.FontColor = Color.LightGray;
			columnItem28.IsExpand = false;
			columnItem28.MyStyle = FontStyle.Regular;
			columnItem28.Name = "deals";
			columnItem28.Text = "Deals";
			columnItem28.ValueFormat = FormatType.Volume;
			columnItem28.Visible = false;
			columnItem28.Width = 8;
			columnItem29.Alignment = StringAlignment.Far;
			columnItem29.BackColor = Color.FromArgb(64, 64, 64);
			columnItem29.FontColor = Color.LightGray;
			columnItem29.IsExpand = false;
			columnItem29.MyStyle = FontStyle.Regular;
			columnItem29.Name = "mvol";
			columnItem29.Text = "Volume";
			columnItem29.ValueFormat = FormatType.Volume;
			columnItem29.Visible = true;
			columnItem29.Width = 12;
			columnItem30.Alignment = StringAlignment.Far;
			columnItem30.BackColor = Color.FromArgb(64, 64, 64);
			columnItem30.FontColor = Color.LightGray;
			columnItem30.IsExpand = false;
			columnItem30.MyStyle = FontStyle.Regular;
			columnItem30.Name = "mval";
			columnItem30.Text = "Value(K฿)";
			columnItem30.ValueFormat = FormatType.Volume;
			columnItem30.Visible = true;
			columnItem30.Width = 10;
			columnItem31.Alignment = StringAlignment.Far;
			columnItem31.BackColor = Color.FromArgb(64, 64, 64);
			columnItem31.FontColor = Color.LightGray;
			columnItem31.IsExpand = false;
			columnItem31.MyStyle = FontStyle.Underline;
			columnItem31.Name = "po";
			columnItem31.Text = "PO";
			columnItem31.ValueFormat = FormatType.Price;
			columnItem31.Visible = false;
			columnItem31.Width = 9;
			columnItem32.Alignment = StringAlignment.Far;
			columnItem32.BackColor = Color.FromArgb(64, 64, 100);
			columnItem32.FontColor = Color.LightGray;
			columnItem32.IsExpand = false;
			columnItem32.MyStyle = FontStyle.Underline;
			columnItem32.Name = "pc";
			columnItem32.Text = "PC";
			columnItem32.ValueFormat = FormatType.Price;
			columnItem32.Visible = false;
			columnItem32.Width = 9;
			columnItem33.Alignment = StringAlignment.Far;
			columnItem33.BackColor = Color.FromArgb(64, 64, 64);
			columnItem33.FontColor = Color.LightGray;
			columnItem33.IsExpand = false;
			columnItem33.MyStyle = FontStyle.Regular;
			columnItem33.Name = "buyvolpct";
			columnItem33.Text = "BVol%";
			columnItem33.ValueFormat = FormatType.Price;
			columnItem33.Visible = false;
			columnItem33.Width = 8;
			columnItem34.Alignment = StringAlignment.Far;
			columnItem34.BackColor = Color.FromArgb(64, 64, 64);
			columnItem34.FontColor = Color.LightGray;
			columnItem34.IsExpand = false;
			columnItem34.MyStyle = FontStyle.Regular;
			columnItem34.Name = "selvolpct";
			columnItem34.Text = "SVol%";
			columnItem34.ValueFormat = FormatType.Price;
			columnItem34.Visible = false;
			columnItem34.Width = 8;
			this.intzaBBO.Columns.Add(columnItem16);
			this.intzaBBO.Columns.Add(columnItem17);
			this.intzaBBO.Columns.Add(columnItem18);
			this.intzaBBO.Columns.Add(columnItem19);
			this.intzaBBO.Columns.Add(columnItem20);
			this.intzaBBO.Columns.Add(columnItem21);
			this.intzaBBO.Columns.Add(columnItem22);
			this.intzaBBO.Columns.Add(columnItem23);
			this.intzaBBO.Columns.Add(columnItem24);
			this.intzaBBO.Columns.Add(columnItem25);
			this.intzaBBO.Columns.Add(columnItem26);
			this.intzaBBO.Columns.Add(columnItem27);
			this.intzaBBO.Columns.Add(columnItem28);
			this.intzaBBO.Columns.Add(columnItem29);
			this.intzaBBO.Columns.Add(columnItem30);
			this.intzaBBO.Columns.Add(columnItem31);
			this.intzaBBO.Columns.Add(columnItem32);
			this.intzaBBO.Columns.Add(columnItem33);
			this.intzaBBO.Columns.Add(columnItem34);
			this.intzaBBO.CurrentScroll = 0;
			this.intzaBBO.Cursor = Cursors.Hand;
			this.intzaBBO.FocusItemIndex = -1;
			this.intzaBBO.ForeColor = Color.Black;
			this.intzaBBO.GridColor = Color.FromArgb(45, 45, 45);
			this.intzaBBO.HeaderPctHeight = 80f;
			this.intzaBBO.IsAutoRepaint = true;
			this.intzaBBO.IsDrawGrid = true;
			this.intzaBBO.IsDrawHeader = true;
			this.intzaBBO.IsScrollable = true;
			this.intzaBBO.Location = new Point(4, 35);
			this.intzaBBO.MainColumn = "";
			this.intzaBBO.Margin = new Padding(0);
			this.intzaBBO.Name = "intzaBBO";
			this.intzaBBO.Rows = 0;
			this.intzaBBO.RowSelectColor = Color.FromArgb(50, 50, 50);
			this.intzaBBO.RowSelectType = 2;
			this.intzaBBO.ScrollChennelColor = Color.Gray;
			this.intzaBBO.Size = new Size(684, 65);
			this.intzaBBO.SortColumnName = "";
			this.intzaBBO.SortType = SortType.Desc;
			this.intzaBBO.TabIndex = 74;
			this.intzaBBO.TableClickExpand += new ExpandGrid.TableClickExpandEventHandler(this.intzaBBO_TableClickExpand);
			this.intzaBBO.TableMouseClick += new ExpandGrid.TableMouseClickEventHandler(this.intzaBBO_TableMouseClick);
			this.intzaBBO.ItemDragDrop += new ExpandGrid.ItemDragDropEventHandler(this.intzaBBO_ItemDragDrop);
			this.intzaBBO.TableHeaderMouseMove += new ExpandGrid.TableHeaderMouseMoveEventHandler(this.intzaBBO_TableHeaderMouseMove);
			this.intzaBBO.TableMouseDoubleClick += new ExpandGrid.TableMouseDoubleClickEventHandler(this.intzaBBO_TableMouseDoubleClick);
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.tsmiShowBestBO,
				this.tsmiShow3BO,
				this.tsmiShow5BO
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new Size(106, 70);
			this.contextMenuStrip1.Opening += new CancelEventHandler(this.contextMenuStrip1_Opening);
			this.tsmiShowBestBO.Name = "tsmiShowBestBO";
			this.tsmiShowBestBO.Size = new Size(105, 22);
			this.tsmiShowBestBO.Text = "1 Bids";
			this.tsmiShowBestBO.Click += new EventHandler(this.tsmiShowExpandBBO_Click);
			this.tsmiShow3BO.Name = "tsmiShow3BO";
			this.tsmiShow3BO.Size = new Size(105, 22);
			this.tsmiShow3BO.Text = "3 Bids";
			this.tsmiShow3BO.Click += new EventHandler(this.tsmiShowExpandBBO_Click);
			this.tsmiShow5BO.Name = "tsmiShow5BO";
			this.tsmiShow5BO.Size = new Size(105, 22);
			this.tsmiShow5BO.Text = "5 Bids";
			this.tsmiShow5BO.Click += new EventHandler(this.tsmiShowExpandBBO_Click);
			this.pictureBox1.BackColor = Color.Black;
			this.pictureBox1.Location = new Point(511, 29);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new Size(58, 64);
			this.pictureBox1.TabIndex = 81;
			this.pictureBox1.TabStop = false;
			this.lbChartLoading.AutoSize = true;
			this.lbChartLoading.BackColor = Color.FromArgb(64, 64, 64);
			this.lbChartLoading.BorderStyle = BorderStyle.FixedSingle;
			this.lbChartLoading.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.lbChartLoading.ForeColor = Color.Yellow;
			this.lbChartLoading.Location = new Point(602, 118);
			this.lbChartLoading.Name = "lbChartLoading";
			this.lbChartLoading.Padding = new Padding(5, 3, 5, 3);
			this.lbChartLoading.Size = new Size(69, 21);
			this.lbChartLoading.TabIndex = 82;
			this.lbChartLoading.Text = "Loading ...";
			this.lbChartLoading.TextAlign = ContentAlignment.MiddleCenter;
			this.lbChartLoading.Visible = false;
			this.contextLink.Items.AddRange(new ToolStripItem[]
			{
				this.tsmCallHistoricalChart,
				this.tsmCallNews,
				this.toolStripMenuItem1,
				this.tsmCallStockInPlay,
				this.tsmCallSaleByPrice,
				this.tsmCallSaleByTime,
				this.tsmCallOddlot
			});
			this.contextLink.Name = "contextMenuStrip1";
			this.contextLink.Size = new Size(212, 142);
			this.tsmCallHistoricalChart.Name = "tsmCallHistoricalChart";
			this.tsmCallHistoricalChart.Size = new Size(211, 22);
			this.tsmCallHistoricalChart.Text = "Historical Chart";
			this.tsmCallHistoricalChart.Click += new EventHandler(this.tsmCallHistoricalChart_Click);
			this.tsmCallNews.Name = "tsmCallNews";
			this.tsmCallNews.Size = new Size(211, 22);
			this.tsmCallNews.Text = "News - ข่าวตลาดหลักทรัพย์ฯ";
			this.tsmCallNews.Click += new EventHandler(this.tsmCallNews_Click);
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new Size(208, 6);
			this.tsmCallStockInPlay.Name = "tsmCallStockInPlay";
			this.tsmCallStockInPlay.Size = new Size(211, 22);
			this.tsmCallStockInPlay.Text = "Stock in Play";
			this.tsmCallStockInPlay.Click += new EventHandler(this.tsmCallStockSummary_Click);
			this.tsmCallSaleByPrice.Name = "tsmCallSaleByPrice";
			this.tsmCallSaleByPrice.Size = new Size(211, 22);
			this.tsmCallSaleByPrice.Text = "Sale by Price";
			this.tsmCallSaleByPrice.Click += new EventHandler(this.tsmCallSaleByPrice_Click);
			this.tsmCallSaleByTime.Name = "tsmCallSaleByTime";
			this.tsmCallSaleByTime.Size = new Size(211, 22);
			this.tsmCallSaleByTime.Text = "Sale by Time";
			this.tsmCallSaleByTime.Click += new EventHandler(this.tsmCallSaleByTime_Click);
			this.tsmCallOddlot.Name = "tsmCallOddlot";
			this.tsmCallOddlot.Size = new Size(211, 22);
			this.tsmCallOddlot.Text = "View Oddlot";
			this.tsmCallOddlot.Click += new EventHandler(this.tsmCallOddlot_Click);
			this.btnCloseChart.BackColor = Color.Black;
			this.btnCloseChart.FlatAppearance.BorderSize = 0;
			this.btnCloseChart.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnCloseChart.FlatAppearance.MouseOverBackColor = Color.Teal;
			this.btnCloseChart.FlatStyle = FlatStyle.Flat;
			this.btnCloseChart.Image = (Image)componentResourceManager.GetObject("btnCloseChart.Image");
			this.btnCloseChart.Location = new Point(523, 70);
			this.btnCloseChart.Name = "btnCloseChart";
			this.btnCloseChart.Size = new Size(19, 19);
			this.btnCloseChart.TabIndex = 88;
			this.btnCloseChart.UseVisualStyleBackColor = false;
			this.btnCloseChart.Visible = false;
			this.btnCloseChart.Click += new EventHandler(this.tsbtnSwitchChart_Click);
			this.intzaInfoTFEX.AllowDrop = true;
			this.intzaInfoTFEX.BackColor = Color.FromArgb(10, 10, 10);
			this.intzaInfoTFEX.CanDrag = false;
			this.intzaInfoTFEX.IsAutoRepaint = true;
			this.intzaInfoTFEX.IsDroped = false;
			itemGrid.AdjustFontSize = 0f;
			itemGrid.Alignment = StringAlignment.Near;
			itemGrid.BackColor = Color.Black;
			itemGrid.Changed = false;
			itemGrid.FieldType = ItemType.Label;
			itemGrid.FontColor = Color.Gainsboro;
			itemGrid.FontStyle = FontStyle.Regular;
			itemGrid.Height = 1f;
			itemGrid.IsBlink = 0;
			itemGrid.Name = "open_label";
			itemGrid.Text = "Open";
			itemGrid.ValueFormat = FormatType.Text;
			itemGrid.Visible = true;
			itemGrid.Width = 20;
			itemGrid.X = 0;
			itemGrid.Y = 0f;
			itemGrid2.AdjustFontSize = -1f;
			itemGrid2.Alignment = StringAlignment.Far;
			itemGrid2.BackColor = Color.Black;
			itemGrid2.Changed = false;
			itemGrid2.FieldType = ItemType.Text;
			itemGrid2.FontColor = Color.Yellow;
			itemGrid2.FontStyle = FontStyle.Regular;
			itemGrid2.Height = 1f;
			itemGrid2.IsBlink = 0;
			itemGrid2.Name = "open_vol";
			itemGrid2.Text = "";
			itemGrid2.ValueFormat = FormatType.Volume;
			itemGrid2.Visible = true;
			itemGrid2.Width = 35;
			itemGrid2.X = 20;
			itemGrid2.Y = 0f;
			itemGrid3.AdjustFontSize = -3f;
			itemGrid3.Alignment = StringAlignment.Far;
			itemGrid3.BackColor = Color.Black;
			itemGrid3.Changed = false;
			itemGrid3.FieldType = ItemType.Text;
			itemGrid3.FontColor = Color.Yellow;
			itemGrid3.FontStyle = FontStyle.Regular;
			itemGrid3.Height = 1f;
			itemGrid3.IsBlink = 0;
			itemGrid3.Name = "open_pvol";
			itemGrid3.Text = "";
			itemGrid3.ValueFormat = FormatType.ChangePrice;
			itemGrid3.Visible = true;
			itemGrid3.Width = 20;
			itemGrid3.X = 55;
			itemGrid3.Y = 0f;
			itemGrid4.AdjustFontSize = 0f;
			itemGrid4.Alignment = StringAlignment.Far;
			itemGrid4.BackColor = Color.Black;
			itemGrid4.Changed = false;
			itemGrid4.FieldType = ItemType.Text;
			itemGrid4.FontColor = Color.Yellow;
			itemGrid4.FontStyle = FontStyle.Regular;
			itemGrid4.Height = 1f;
			itemGrid4.IsBlink = 0;
			itemGrid4.Name = "open_avg";
			itemGrid4.Text = "";
			itemGrid4.ValueFormat = FormatType.Text;
			itemGrid4.Visible = false;
			itemGrid4.Width = 27;
			itemGrid4.X = 73;
			itemGrid4.Y = 0f;
			itemGrid5.AdjustFontSize = 0f;
			itemGrid5.Alignment = StringAlignment.Near;
			itemGrid5.BackColor = Color.Black;
			itemGrid5.Changed = false;
			itemGrid5.FieldType = ItemType.Label;
			itemGrid5.FontColor = Color.Gainsboro;
			itemGrid5.FontStyle = FontStyle.Regular;
			itemGrid5.Height = 1f;
			itemGrid5.IsBlink = 0;
			itemGrid5.Name = "long_label";
			itemGrid5.Text = "Long";
			itemGrid5.ValueFormat = FormatType.Text;
			itemGrid5.Visible = true;
			itemGrid5.Width = 20;
			itemGrid5.X = 0;
			itemGrid5.Y = 1f;
			itemGrid6.AdjustFontSize = -1f;
			itemGrid6.Alignment = StringAlignment.Far;
			itemGrid6.BackColor = Color.Black;
			itemGrid6.Changed = false;
			itemGrid6.FieldType = ItemType.Text;
			itemGrid6.FontColor = Color.Lime;
			itemGrid6.FontStyle = FontStyle.Regular;
			itemGrid6.Height = 1f;
			itemGrid6.IsBlink = 0;
			itemGrid6.Name = "long_vol";
			itemGrid6.Text = "";
			itemGrid6.ValueFormat = FormatType.Volume;
			itemGrid6.Visible = true;
			itemGrid6.Width = 35;
			itemGrid6.X = 20;
			itemGrid6.Y = 1f;
			itemGrid7.AdjustFontSize = -3f;
			itemGrid7.Alignment = StringAlignment.Far;
			itemGrid7.BackColor = Color.Black;
			itemGrid7.Changed = false;
			itemGrid7.FieldType = ItemType.Text;
			itemGrid7.FontColor = Color.Lime;
			itemGrid7.FontStyle = FontStyle.Regular;
			itemGrid7.Height = 1f;
			itemGrid7.IsBlink = 0;
			itemGrid7.Name = "long_pvol";
			itemGrid7.Text = "";
			itemGrid7.ValueFormat = FormatType.ChangePrice;
			itemGrid7.Visible = true;
			itemGrid7.Width = 20;
			itemGrid7.X = 55;
			itemGrid7.Y = 1f;
			itemGrid8.AdjustFontSize = 0f;
			itemGrid8.Alignment = StringAlignment.Far;
			itemGrid8.BackColor = Color.Black;
			itemGrid8.Changed = false;
			itemGrid8.FieldType = ItemType.Text;
			itemGrid8.FontColor = Color.Yellow;
			itemGrid8.FontStyle = FontStyle.Regular;
			itemGrid8.Height = 1f;
			itemGrid8.IsBlink = 0;
			itemGrid8.Name = "long_avg";
			itemGrid8.Text = "";
			itemGrid8.ValueFormat = FormatType.Text;
			itemGrid8.Visible = false;
			itemGrid8.Width = 27;
			itemGrid8.X = 73;
			itemGrid8.Y = 1f;
			itemGrid9.AdjustFontSize = 0f;
			itemGrid9.Alignment = StringAlignment.Near;
			itemGrid9.BackColor = Color.Black;
			itemGrid9.Changed = false;
			itemGrid9.FieldType = ItemType.Label;
			itemGrid9.FontColor = Color.Gainsboro;
			itemGrid9.FontStyle = FontStyle.Regular;
			itemGrid9.Height = 1f;
			itemGrid9.IsBlink = 0;
			itemGrid9.Name = "short_label";
			itemGrid9.Text = "Short";
			itemGrid9.ValueFormat = FormatType.Text;
			itemGrid9.Visible = true;
			itemGrid9.Width = 20;
			itemGrid9.X = 0;
			itemGrid9.Y = 2f;
			itemGrid10.AdjustFontSize = -1f;
			itemGrid10.Alignment = StringAlignment.Far;
			itemGrid10.BackColor = Color.Black;
			itemGrid10.Changed = false;
			itemGrid10.FieldType = ItemType.Text;
			itemGrid10.FontColor = Color.Red;
			itemGrid10.FontStyle = FontStyle.Regular;
			itemGrid10.Height = 1f;
			itemGrid10.IsBlink = 0;
			itemGrid10.Name = "short_vol";
			itemGrid10.Text = "";
			itemGrid10.ValueFormat = FormatType.Volume;
			itemGrid10.Visible = true;
			itemGrid10.Width = 35;
			itemGrid10.X = 20;
			itemGrid10.Y = 2f;
			itemGrid11.AdjustFontSize = -3f;
			itemGrid11.Alignment = StringAlignment.Far;
			itemGrid11.BackColor = Color.Black;
			itemGrid11.Changed = false;
			itemGrid11.FieldType = ItemType.Text;
			itemGrid11.FontColor = Color.Red;
			itemGrid11.FontStyle = FontStyle.Regular;
			itemGrid11.Height = 1f;
			itemGrid11.IsBlink = 0;
			itemGrid11.Name = "short_pvol";
			itemGrid11.Text = "";
			itemGrid11.ValueFormat = FormatType.ChangePrice;
			itemGrid11.Visible = true;
			itemGrid11.Width = 20;
			itemGrid11.X = 55;
			itemGrid11.Y = 2f;
			itemGrid12.AdjustFontSize = 0f;
			itemGrid12.Alignment = StringAlignment.Far;
			itemGrid12.BackColor = Color.Black;
			itemGrid12.Changed = false;
			itemGrid12.FieldType = ItemType.Text;
			itemGrid12.FontColor = Color.Yellow;
			itemGrid12.FontStyle = FontStyle.Regular;
			itemGrid12.Height = 1f;
			itemGrid12.IsBlink = 0;
			itemGrid12.Name = "short_avg";
			itemGrid12.Text = "";
			itemGrid12.ValueFormat = FormatType.Text;
			itemGrid12.Visible = false;
			itemGrid12.Width = 27;
			itemGrid12.X = 73;
			itemGrid12.Y = 2f;
			itemGrid13.AdjustFontSize = 0f;
			itemGrid13.Alignment = StringAlignment.Near;
			itemGrid13.BackColor = Color.Black;
			itemGrid13.Changed = false;
			itemGrid13.FieldType = ItemType.Label;
			itemGrid13.FontColor = Color.Gainsboro;
			itemGrid13.FontStyle = FontStyle.Regular;
			itemGrid13.Height = 1f;
			itemGrid13.IsBlink = 0;
			itemGrid13.Name = "oi_lable";
			itemGrid13.Text = "OI";
			itemGrid13.ValueFormat = FormatType.Text;
			itemGrid13.Visible = true;
			itemGrid13.Width = 23;
			itemGrid13.X = 0;
			itemGrid13.Y = 3f;
			itemGrid14.AdjustFontSize = 0f;
			itemGrid14.Alignment = StringAlignment.Near;
			itemGrid14.BackColor = Color.Black;
			itemGrid14.Changed = false;
			itemGrid14.FieldType = ItemType.Text;
			itemGrid14.FontColor = Color.Yellow;
			itemGrid14.FontStyle = FontStyle.Regular;
			itemGrid14.Height = 1f;
			itemGrid14.IsBlink = 0;
			itemGrid14.Name = "oi";
			itemGrid14.Text = "";
			itemGrid14.ValueFormat = FormatType.Price;
			itemGrid14.Visible = true;
			itemGrid14.Width = 26;
			itemGrid14.X = 23;
			itemGrid14.Y = 3f;
			itemGrid15.AdjustFontSize = 0f;
			itemGrid15.Alignment = StringAlignment.Near;
			itemGrid15.BackColor = Color.Black;
			itemGrid15.Changed = false;
			itemGrid15.FieldType = ItemType.Label;
			itemGrid15.FontColor = Color.Gainsboro;
			itemGrid15.FontStyle = FontStyle.Regular;
			itemGrid15.Height = 1f;
			itemGrid15.IsBlink = 0;
			itemGrid15.Name = "psettle_label";
			itemGrid15.Text = "P.Settle";
			itemGrid15.ValueFormat = FormatType.Text;
			itemGrid15.Visible = true;
			itemGrid15.Width = 23;
			itemGrid15.X = 0;
			itemGrid15.Y = 4f;
			itemGrid16.AdjustFontSize = 0f;
			itemGrid16.Alignment = StringAlignment.Near;
			itemGrid16.BackColor = Color.Black;
			itemGrid16.Changed = false;
			itemGrid16.FieldType = ItemType.Text;
			itemGrid16.FontColor = Color.Yellow;
			itemGrid16.FontStyle = FontStyle.Regular;
			itemGrid16.Height = 1f;
			itemGrid16.IsBlink = 0;
			itemGrid16.Name = "psettle";
			itemGrid16.Text = "";
			itemGrid16.ValueFormat = FormatType.Text;
			itemGrid16.Visible = true;
			itemGrid16.Width = 26;
			itemGrid16.X = 23;
			itemGrid16.Y = 4f;
			itemGrid17.AdjustFontSize = 0f;
			itemGrid17.Alignment = StringAlignment.Near;
			itemGrid17.BackColor = Color.Black;
			itemGrid17.Changed = false;
			itemGrid17.FieldType = ItemType.Label;
			itemGrid17.FontColor = Color.Gainsboro;
			itemGrid17.FontStyle = FontStyle.Regular;
			itemGrid17.Height = 1f;
			itemGrid17.IsBlink = 0;
			itemGrid17.Name = "settle_label";
			itemGrid17.Text = "Settle";
			itemGrid17.ValueFormat = FormatType.Text;
			itemGrid17.Visible = true;
			itemGrid17.Width = 23;
			itemGrid17.X = 0;
			itemGrid17.Y = 5f;
			itemGrid18.AdjustFontSize = 0f;
			itemGrid18.Alignment = StringAlignment.Near;
			itemGrid18.BackColor = Color.Black;
			itemGrid18.Changed = false;
			itemGrid18.FieldType = ItemType.Text;
			itemGrid18.FontColor = Color.Yellow;
			itemGrid18.FontStyle = FontStyle.Regular;
			itemGrid18.Height = 1f;
			itemGrid18.IsBlink = 0;
			itemGrid18.Name = "settle";
			itemGrid18.Text = "";
			itemGrid18.ValueFormat = FormatType.Text;
			itemGrid18.Visible = true;
			itemGrid18.Width = 26;
			itemGrid18.X = 23;
			itemGrid18.Y = 5f;
			itemGrid19.AdjustFontSize = 0f;
			itemGrid19.Alignment = StringAlignment.Near;
			itemGrid19.BackColor = Color.Black;
			itemGrid19.Changed = false;
			itemGrid19.FieldType = ItemType.Label;
			itemGrid19.FontColor = Color.Gainsboro;
			itemGrid19.FontStyle = FontStyle.Regular;
			itemGrid19.Height = 1f;
			itemGrid19.IsBlink = 0;
			itemGrid19.Name = "ceiling_lable";
			itemGrid19.Text = "Ceiling";
			itemGrid19.ValueFormat = FormatType.Text;
			itemGrid19.Visible = true;
			itemGrid19.Width = 23;
			itemGrid19.X = 0;
			itemGrid19.Y = 6f;
			itemGrid20.AdjustFontSize = 0f;
			itemGrid20.Alignment = StringAlignment.Near;
			itemGrid20.BackColor = Color.Black;
			itemGrid20.Changed = false;
			itemGrid20.FieldType = ItemType.Text;
			itemGrid20.FontColor = Color.Cyan;
			itemGrid20.FontStyle = FontStyle.Regular;
			itemGrid20.Height = 1f;
			itemGrid20.IsBlink = 0;
			itemGrid20.Name = "ceiling";
			itemGrid20.Text = "";
			itemGrid20.ValueFormat = FormatType.Text;
			itemGrid20.Visible = true;
			itemGrid20.Width = 26;
			itemGrid20.X = 23;
			itemGrid20.Y = 6f;
			itemGrid21.AdjustFontSize = 0f;
			itemGrid21.Alignment = StringAlignment.Near;
			itemGrid21.BackColor = Color.Black;
			itemGrid21.Changed = false;
			itemGrid21.FieldType = ItemType.Label;
			itemGrid21.FontColor = Color.Gainsboro;
			itemGrid21.FontStyle = FontStyle.Regular;
			itemGrid21.Height = 1f;
			itemGrid21.IsBlink = 0;
			itemGrid21.Name = "floor_label";
			itemGrid21.Text = "Floor";
			itemGrid21.ValueFormat = FormatType.Text;
			itemGrid21.Visible = true;
			itemGrid21.Width = 23;
			itemGrid21.X = 0;
			itemGrid21.Y = 7f;
			itemGrid22.AdjustFontSize = 0f;
			itemGrid22.Alignment = StringAlignment.Near;
			itemGrid22.BackColor = Color.Black;
			itemGrid22.Changed = false;
			itemGrid22.FieldType = ItemType.Text;
			itemGrid22.FontColor = Color.Magenta;
			itemGrid22.FontStyle = FontStyle.Regular;
			itemGrid22.Height = 1f;
			itemGrid22.IsBlink = 0;
			itemGrid22.Name = "floor";
			itemGrid22.Text = "";
			itemGrid22.ValueFormat = FormatType.Text;
			itemGrid22.Visible = true;
			itemGrid22.Width = 26;
			itemGrid22.X = 23;
			itemGrid22.Y = 7f;
			itemGrid23.AdjustFontSize = 0f;
			itemGrid23.Alignment = StringAlignment.Near;
			itemGrid23.BackColor = Color.Black;
			itemGrid23.Changed = false;
			itemGrid23.FieldType = ItemType.Label;
			itemGrid23.FontColor = Color.White;
			itemGrid23.FontStyle = FontStyle.Regular;
			itemGrid23.Height = 1f;
			itemGrid23.IsBlink = 0;
			itemGrid23.Name = "Multiplier";
			itemGrid23.Text = "Multiplier";
			itemGrid23.ValueFormat = FormatType.Text;
			itemGrid23.Visible = true;
			itemGrid23.Width = 25;
			itemGrid23.X = 0;
			itemGrid23.Y = 8f;
			itemGrid24.AdjustFontSize = 0f;
			itemGrid24.Alignment = StringAlignment.Near;
			itemGrid24.BackColor = Color.Black;
			itemGrid24.Changed = false;
			itemGrid24.FieldType = ItemType.Text;
			itemGrid24.FontColor = Color.Yellow;
			itemGrid24.FontStyle = FontStyle.Regular;
			itemGrid24.Height = 1f;
			itemGrid24.IsBlink = 0;
			itemGrid24.Name = "multiplier";
			itemGrid24.Text = "";
			itemGrid24.ValueFormat = FormatType.Text;
			itemGrid24.Visible = true;
			itemGrid24.Width = 26;
			itemGrid24.X = 23;
			itemGrid24.Y = 8f;
			itemGrid25.AdjustFontSize = 0f;
			itemGrid25.Alignment = StringAlignment.Near;
			itemGrid25.BackColor = Color.Black;
			itemGrid25.Changed = false;
			itemGrid25.FieldType = ItemType.Label;
			itemGrid25.FontColor = Color.Gainsboro;
			itemGrid25.FontStyle = FontStyle.Regular;
			itemGrid25.Height = 1f;
			itemGrid25.IsBlink = 0;
			itemGrid25.Name = "tickSize_lable";
			itemGrid25.Text = "Spread";
			itemGrid25.ValueFormat = FormatType.Text;
			itemGrid25.Visible = true;
			itemGrid25.Width = 23;
			itemGrid25.X = 0;
			itemGrid25.Y = 9f;
			itemGrid26.AdjustFontSize = 0f;
			itemGrid26.Alignment = StringAlignment.Near;
			itemGrid26.BackColor = Color.Black;
			itemGrid26.Changed = false;
			itemGrid26.FieldType = ItemType.Text;
			itemGrid26.FontColor = Color.Yellow;
			itemGrid26.FontStyle = FontStyle.Regular;
			itemGrid26.Height = 1f;
			itemGrid26.IsBlink = 0;
			itemGrid26.Name = "tickSize";
			itemGrid26.Text = "";
			itemGrid26.ValueFormat = FormatType.Text;
			itemGrid26.Visible = true;
			itemGrid26.Width = 26;
			itemGrid26.X = 23;
			itemGrid26.Y = 9f;
			itemGrid27.AdjustFontSize = 0f;
			itemGrid27.Alignment = StringAlignment.Near;
			itemGrid27.BackColor = Color.Black;
			itemGrid27.Changed = false;
			itemGrid27.FieldType = ItemType.Label;
			itemGrid27.FontColor = Color.Gainsboro;
			itemGrid27.FontStyle = FontStyle.Regular;
			itemGrid27.Height = 1f;
			itemGrid27.IsBlink = 0;
			itemGrid27.Name = "turnover_label";
			itemGrid27.Text = "Turn Over";
			itemGrid27.ValueFormat = FormatType.Text;
			itemGrid27.Visible = false;
			itemGrid27.Width = 23;
			itemGrid27.X = 49;
			itemGrid27.Y = 3f;
			itemGrid28.AdjustFontSize = 0f;
			itemGrid28.Alignment = StringAlignment.Near;
			itemGrid28.BackColor = Color.Black;
			itemGrid28.Changed = false;
			itemGrid28.FieldType = ItemType.Text;
			itemGrid28.FontColor = Color.Yellow;
			itemGrid28.FontStyle = FontStyle.Regular;
			itemGrid28.Height = 1f;
			itemGrid28.IsBlink = 0;
			itemGrid28.Name = "turnover";
			itemGrid28.Text = "";
			itemGrid28.ValueFormat = FormatType.Price;
			itemGrid28.Visible = false;
			itemGrid28.Width = 29;
			itemGrid28.X = 72;
			itemGrid28.Y = 3f;
			itemGrid29.AdjustFontSize = 0f;
			itemGrid29.Alignment = StringAlignment.Near;
			itemGrid29.BackColor = Color.Black;
			itemGrid29.Changed = false;
			itemGrid29.FieldType = ItemType.Label;
			itemGrid29.FontColor = Color.Gainsboro;
			itemGrid29.FontStyle = FontStyle.Regular;
			itemGrid29.Height = 1f;
			itemGrid29.IsBlink = 0;
			itemGrid29.Name = "basis_label";
			itemGrid29.Text = "Basis";
			itemGrid29.ValueFormat = FormatType.Text;
			itemGrid29.Visible = true;
			itemGrid29.Width = 23;
			itemGrid29.X = 49;
			itemGrid29.Y = 3f;
			itemGrid30.AdjustFontSize = 0f;
			itemGrid30.Alignment = StringAlignment.Near;
			itemGrid30.BackColor = Color.Black;
			itemGrid30.Changed = false;
			itemGrid30.FieldType = ItemType.Text;
			itemGrid30.FontColor = Color.Yellow;
			itemGrid30.FontStyle = FontStyle.Regular;
			itemGrid30.Height = 1f;
			itemGrid30.IsBlink = 0;
			itemGrid30.Name = "basis";
			itemGrid30.Text = "";
			itemGrid30.ValueFormat = FormatType.Price;
			itemGrid30.Visible = true;
			itemGrid30.Width = 29;
			itemGrid30.X = 72;
			itemGrid30.Y = 3f;
			itemGrid31.AdjustFontSize = 0f;
			itemGrid31.Alignment = StringAlignment.Near;
			itemGrid31.BackColor = Color.Black;
			itemGrid31.Changed = false;
			itemGrid31.FieldType = ItemType.Label;
			itemGrid31.FontColor = Color.Gainsboro;
			itemGrid31.FontStyle = FontStyle.Regular;
			itemGrid31.Height = 1f;
			itemGrid31.IsBlink = 0;
			itemGrid31.Name = "open1_label";
			itemGrid31.Text = "Open 1";
			itemGrid31.ValueFormat = FormatType.Text;
			itemGrid31.Visible = true;
			itemGrid31.Width = 23;
			itemGrid31.X = 49;
			itemGrid31.Y = 4f;
			itemGrid32.AdjustFontSize = 0f;
			itemGrid32.Alignment = StringAlignment.Near;
			itemGrid32.BackColor = Color.Black;
			itemGrid32.Changed = false;
			itemGrid32.FieldType = ItemType.Text;
			itemGrid32.FontColor = Color.Yellow;
			itemGrid32.FontStyle = FontStyle.Regular;
			itemGrid32.Height = 1f;
			itemGrid32.IsBlink = 0;
			itemGrid32.Name = "open1";
			itemGrid32.Text = "";
			itemGrid32.ValueFormat = FormatType.Text;
			itemGrid32.Visible = true;
			itemGrid32.Width = 29;
			itemGrid32.X = 72;
			itemGrid32.Y = 4f;
			itemGrid33.AdjustFontSize = 0f;
			itemGrid33.Alignment = StringAlignment.Near;
			itemGrid33.BackColor = Color.Black;
			itemGrid33.Changed = false;
			itemGrid33.FieldType = ItemType.Label;
			itemGrid33.FontColor = Color.Gainsboro;
			itemGrid33.FontStyle = FontStyle.Regular;
			itemGrid33.Height = 1f;
			itemGrid33.IsBlink = 0;
			itemGrid33.Name = "open2_label";
			itemGrid33.Text = "Open 2";
			itemGrid33.ValueFormat = FormatType.Text;
			itemGrid33.Visible = true;
			itemGrid33.Width = 23;
			itemGrid33.X = 49;
			itemGrid33.Y = 5f;
			itemGrid34.AdjustFontSize = 0f;
			itemGrid34.Alignment = StringAlignment.Near;
			itemGrid34.BackColor = Color.Black;
			itemGrid34.Changed = false;
			itemGrid34.FieldType = ItemType.Text;
			itemGrid34.FontColor = Color.Yellow;
			itemGrid34.FontStyle = FontStyle.Regular;
			itemGrid34.Height = 1f;
			itemGrid34.IsBlink = 0;
			itemGrid34.Name = "open2";
			itemGrid34.Text = "";
			itemGrid34.ValueFormat = FormatType.Text;
			itemGrid34.Visible = true;
			itemGrid34.Width = 29;
			itemGrid34.X = 72;
			itemGrid34.Y = 5f;
			itemGrid35.AdjustFontSize = 0f;
			itemGrid35.Alignment = StringAlignment.Near;
			itemGrid35.BackColor = Color.Black;
			itemGrid35.Changed = false;
			itemGrid35.FieldType = ItemType.Label;
			itemGrid35.FontColor = Color.Gainsboro;
			itemGrid35.FontStyle = FontStyle.Regular;
			itemGrid35.Height = 1f;
			itemGrid35.IsBlink = 0;
			itemGrid35.Name = "poclose_label";
			itemGrid35.Text = "P.Close";
			itemGrid35.ValueFormat = FormatType.Text;
			itemGrid35.Visible = true;
			itemGrid35.Width = 23;
			itemGrid35.X = 49;
			itemGrid35.Y = 7f;
			itemGrid36.AdjustFontSize = 0f;
			itemGrid36.Alignment = StringAlignment.Near;
			itemGrid36.BackColor = Color.Black;
			itemGrid36.Changed = false;
			itemGrid36.FieldType = ItemType.Text;
			itemGrid36.FontColor = Color.Yellow;
			itemGrid36.FontStyle = FontStyle.Regular;
			itemGrid36.Height = 1f;
			itemGrid36.IsBlink = 0;
			itemGrid36.Name = "poclose";
			itemGrid36.Text = "";
			itemGrid36.ValueFormat = FormatType.Text;
			itemGrid36.Visible = true;
			itemGrid36.Width = 29;
			itemGrid36.X = 72;
			itemGrid36.Y = 7f;
			itemGrid37.AdjustFontSize = 0f;
			itemGrid37.Alignment = StringAlignment.Near;
			itemGrid37.BackColor = Color.Black;
			itemGrid37.Changed = false;
			itemGrid37.FieldType = ItemType.Label;
			itemGrid37.FontColor = Color.Gainsboro;
			itemGrid37.FontStyle = FontStyle.Regular;
			itemGrid37.Height = 1f;
			itemGrid37.IsBlink = 0;
			itemGrid37.Name = "open3_label";
			itemGrid37.Text = "Open 3";
			itemGrid37.ValueFormat = FormatType.Text;
			itemGrid37.Visible = true;
			itemGrid37.Width = 23;
			itemGrid37.X = 49;
			itemGrid37.Y = 6f;
			itemGrid38.AdjustFontSize = 0f;
			itemGrid38.Alignment = StringAlignment.Near;
			itemGrid38.BackColor = Color.Black;
			itemGrid38.Changed = false;
			itemGrid38.FieldType = ItemType.Text;
			itemGrid38.FontColor = Color.Yellow;
			itemGrid38.FontStyle = FontStyle.Regular;
			itemGrid38.Height = 1f;
			itemGrid38.IsBlink = 0;
			itemGrid38.Name = "open3";
			itemGrid38.Text = "";
			itemGrid38.ValueFormat = FormatType.Text;
			itemGrid38.Visible = true;
			itemGrid38.Width = 29;
			itemGrid38.X = 72;
			itemGrid38.Y = 6f;
			itemGrid39.AdjustFontSize = 0f;
			itemGrid39.Alignment = StringAlignment.Near;
			itemGrid39.BackColor = Color.Black;
			itemGrid39.Changed = false;
			itemGrid39.FieldType = ItemType.Label;
			itemGrid39.FontColor = Color.Gainsboro;
			itemGrid39.FontStyle = FontStyle.Regular;
			itemGrid39.Height = 1f;
			itemGrid39.IsBlink = 0;
			itemGrid39.Name = "first_date_label";
			itemGrid39.Text = "First";
			itemGrid39.ValueFormat = FormatType.Text;
			itemGrid39.Visible = false;
			itemGrid39.Width = 23;
			itemGrid39.X = 49;
			itemGrid39.Y = 7f;
			itemGrid40.AdjustFontSize = 0f;
			itemGrid40.Alignment = StringAlignment.Near;
			itemGrid40.BackColor = Color.Black;
			itemGrid40.Changed = false;
			itemGrid40.FieldType = ItemType.Text;
			itemGrid40.FontColor = Color.Yellow;
			itemGrid40.FontStyle = FontStyle.Regular;
			itemGrid40.Height = 1f;
			itemGrid40.IsBlink = 0;
			itemGrid40.Name = "first_date";
			itemGrid40.Text = "";
			itemGrid40.ValueFormat = FormatType.Text;
			itemGrid40.Visible = false;
			itemGrid40.Width = 29;
			itemGrid40.X = 72;
			itemGrid40.Y = 7f;
			itemGrid41.AdjustFontSize = 0f;
			itemGrid41.Alignment = StringAlignment.Near;
			itemGrid41.BackColor = Color.Black;
			itemGrid41.Changed = false;
			itemGrid41.FieldType = ItemType.Label;
			itemGrid41.FontColor = Color.Gainsboro;
			itemGrid41.FontStyle = FontStyle.Regular;
			itemGrid41.Height = 1f;
			itemGrid41.IsBlink = 0;
			itemGrid41.Name = "last_date_label";
			itemGrid41.Text = "Last";
			itemGrid41.ValueFormat = FormatType.Text;
			itemGrid41.Visible = true;
			itemGrid41.Width = 23;
			itemGrid41.X = 49;
			itemGrid41.Y = 8f;
			itemGrid42.AdjustFontSize = 0f;
			itemGrid42.Alignment = StringAlignment.Near;
			itemGrid42.BackColor = Color.Black;
			itemGrid42.Changed = false;
			itemGrid42.FieldType = ItemType.Text;
			itemGrid42.FontColor = Color.Yellow;
			itemGrid42.FontStyle = FontStyle.Regular;
			itemGrid42.Height = 1f;
			itemGrid42.IsBlink = 0;
			itemGrid42.Name = "last_date";
			itemGrid42.Text = "";
			itemGrid42.ValueFormat = FormatType.Text;
			itemGrid42.Visible = true;
			itemGrid42.Width = 29;
			itemGrid42.X = 72;
			itemGrid42.Y = 8f;
			itemGrid43.AdjustFontSize = 0f;
			itemGrid43.Alignment = StringAlignment.Near;
			itemGrid43.BackColor = Color.Black;
			itemGrid43.Changed = false;
			itemGrid43.FieldType = ItemType.Label;
			itemGrid43.FontColor = Color.Gainsboro;
			itemGrid43.FontStyle = FontStyle.Regular;
			itemGrid43.Height = 1f;
			itemGrid43.IsBlink = 0;
			itemGrid43.Name = "lastIndex_label";
			itemGrid43.Text = "Index";
			itemGrid43.ValueFormat = FormatType.Text;
			itemGrid43.Visible = true;
			itemGrid43.Width = 23;
			itemGrid43.X = 49;
			itemGrid43.Y = 9f;
			itemGrid44.AdjustFontSize = 0f;
			itemGrid44.Alignment = StringAlignment.Near;
			itemGrid44.BackColor = Color.Black;
			itemGrid44.Changed = false;
			itemGrid44.FieldType = ItemType.Text;
			itemGrid44.FontColor = Color.Yellow;
			itemGrid44.FontStyle = FontStyle.Regular;
			itemGrid44.Height = 1f;
			itemGrid44.IsBlink = 0;
			itemGrid44.Name = "lastIndex";
			itemGrid44.Text = "";
			itemGrid44.ValueFormat = FormatType.Text;
			itemGrid44.Visible = true;
			itemGrid44.Width = 29;
			itemGrid44.X = 72;
			itemGrid44.Y = 9f;
			itemGrid45.AdjustFontSize = 0f;
			itemGrid45.Alignment = StringAlignment.Near;
			itemGrid45.BackColor = Color.Black;
			itemGrid45.Changed = false;
			itemGrid45.FieldType = ItemType.Text;
			itemGrid45.FontColor = Color.White;
			itemGrid45.FontStyle = FontStyle.Regular;
			itemGrid45.Height = 3f;
			itemGrid45.IsBlink = 0;
			itemGrid45.Name = "pie";
			itemGrid45.Text = "";
			itemGrid45.ValueFormat = FormatType.PieChart;
			itemGrid45.Visible = true;
			itemGrid45.Width = 25;
			itemGrid45.X = 75;
			itemGrid45.Y = 0f;
			this.intzaInfoTFEX.Items.Add(itemGrid);
			this.intzaInfoTFEX.Items.Add(itemGrid2);
			this.intzaInfoTFEX.Items.Add(itemGrid3);
			this.intzaInfoTFEX.Items.Add(itemGrid4);
			this.intzaInfoTFEX.Items.Add(itemGrid5);
			this.intzaInfoTFEX.Items.Add(itemGrid6);
			this.intzaInfoTFEX.Items.Add(itemGrid7);
			this.intzaInfoTFEX.Items.Add(itemGrid8);
			this.intzaInfoTFEX.Items.Add(itemGrid9);
			this.intzaInfoTFEX.Items.Add(itemGrid10);
			this.intzaInfoTFEX.Items.Add(itemGrid11);
			this.intzaInfoTFEX.Items.Add(itemGrid12);
			this.intzaInfoTFEX.Items.Add(itemGrid13);
			this.intzaInfoTFEX.Items.Add(itemGrid14);
			this.intzaInfoTFEX.Items.Add(itemGrid15);
			this.intzaInfoTFEX.Items.Add(itemGrid16);
			this.intzaInfoTFEX.Items.Add(itemGrid17);
			this.intzaInfoTFEX.Items.Add(itemGrid18);
			this.intzaInfoTFEX.Items.Add(itemGrid19);
			this.intzaInfoTFEX.Items.Add(itemGrid20);
			this.intzaInfoTFEX.Items.Add(itemGrid21);
			this.intzaInfoTFEX.Items.Add(itemGrid22);
			this.intzaInfoTFEX.Items.Add(itemGrid23);
			this.intzaInfoTFEX.Items.Add(itemGrid24);
			this.intzaInfoTFEX.Items.Add(itemGrid25);
			this.intzaInfoTFEX.Items.Add(itemGrid26);
			this.intzaInfoTFEX.Items.Add(itemGrid27);
			this.intzaInfoTFEX.Items.Add(itemGrid28);
			this.intzaInfoTFEX.Items.Add(itemGrid29);
			this.intzaInfoTFEX.Items.Add(itemGrid30);
			this.intzaInfoTFEX.Items.Add(itemGrid31);
			this.intzaInfoTFEX.Items.Add(itemGrid32);
			this.intzaInfoTFEX.Items.Add(itemGrid33);
			this.intzaInfoTFEX.Items.Add(itemGrid34);
			this.intzaInfoTFEX.Items.Add(itemGrid35);
			this.intzaInfoTFEX.Items.Add(itemGrid36);
			this.intzaInfoTFEX.Items.Add(itemGrid37);
			this.intzaInfoTFEX.Items.Add(itemGrid38);
			this.intzaInfoTFEX.Items.Add(itemGrid39);
			this.intzaInfoTFEX.Items.Add(itemGrid40);
			this.intzaInfoTFEX.Items.Add(itemGrid41);
			this.intzaInfoTFEX.Items.Add(itemGrid42);
			this.intzaInfoTFEX.Items.Add(itemGrid43);
			this.intzaInfoTFEX.Items.Add(itemGrid44);
			this.intzaInfoTFEX.Items.Add(itemGrid45);
			this.intzaInfoTFEX.LineColor = Color.Red;
			this.intzaInfoTFEX.Location = new Point(361, 26);
			this.intzaInfoTFEX.Margin = new Padding(2);
			this.intzaInfoTFEX.Name = "intzaInfoTFEX";
			this.intzaInfoTFEX.Size = new Size(136, 80);
			this.intzaInfoTFEX.TabIndex = 90;
			this.intzaInfoTFEX.TabStop = false;
			this.intzaVolumeByBoard.AllowDrop = true;
			this.intzaVolumeByBoard.BackColor = Color.FromArgb(10, 10, 10);
			this.intzaVolumeByBoard.CanBlink = true;
			this.intzaVolumeByBoard.CanDrag = false;
			this.intzaVolumeByBoard.CanGetMouseMove = false;
			columnItem35.Alignment = StringAlignment.Near;
			columnItem35.BackColor = Color.FromArgb(64, 64, 64);
			columnItem35.ColumnAlignment = StringAlignment.Center;
			columnItem35.FontColor = Color.LightGray;
			columnItem35.MyStyle = FontStyle.Regular;
			columnItem35.Name = "h1";
			columnItem35.Text = "";
			columnItem35.ValueFormat = FormatType.Label;
			columnItem35.Visible = true;
			columnItem35.Width = 17;
			columnItem36.Alignment = StringAlignment.Far;
			columnItem36.BackColor = Color.DimGray;
			columnItem36.ColumnAlignment = StringAlignment.Center;
			columnItem36.FontColor = Color.White;
			columnItem36.MyStyle = FontStyle.Regular;
			columnItem36.Name = "deals";
			columnItem36.Text = "Deals";
			columnItem36.ValueFormat = FormatType.Volume;
			columnItem36.Visible = true;
			columnItem36.Width = 20;
			columnItem37.Alignment = StringAlignment.Far;
			columnItem37.BackColor = Color.DimGray;
			columnItem37.ColumnAlignment = StringAlignment.Center;
			columnItem37.FontColor = Color.White;
			columnItem37.MyStyle = FontStyle.Regular;
			columnItem37.Name = "volume";
			columnItem37.Text = "Volume";
			columnItem37.ValueFormat = FormatType.Volume;
			columnItem37.Visible = true;
			columnItem37.Width = 29;
			columnItem38.Alignment = StringAlignment.Far;
			columnItem38.BackColor = Color.DimGray;
			columnItem38.ColumnAlignment = StringAlignment.Center;
			columnItem38.FontColor = Color.White;
			columnItem38.MyStyle = FontStyle.Regular;
			columnItem38.Name = "value";
			columnItem38.Text = "Value";
			columnItem38.ValueFormat = FormatType.Text;
			columnItem38.Visible = true;
			columnItem38.Width = 34;
			this.intzaVolumeByBoard.Columns.Add(columnItem35);
			this.intzaVolumeByBoard.Columns.Add(columnItem36);
			this.intzaVolumeByBoard.Columns.Add(columnItem37);
			this.intzaVolumeByBoard.Columns.Add(columnItem38);
			this.intzaVolumeByBoard.CurrentScroll = 0;
			this.intzaVolumeByBoard.FocusItemIndex = -1;
			this.intzaVolumeByBoard.ForeColor = Color.Black;
			this.intzaVolumeByBoard.GridColor = Color.FromArgb(45, 45, 45);
			this.intzaVolumeByBoard.HeaderPctHeight = 80f;
			this.intzaVolumeByBoard.IsAutoRepaint = true;
			this.intzaVolumeByBoard.IsDrawFullRow = false;
			this.intzaVolumeByBoard.IsDrawGrid = false;
			this.intzaVolumeByBoard.IsDrawHeader = true;
			this.intzaVolumeByBoard.IsScrollable = false;
			this.intzaVolumeByBoard.Location = new Point(2, 125);
			this.intzaVolumeByBoard.MainColumn = "";
			this.intzaVolumeByBoard.Margin = new Padding(0);
			this.intzaVolumeByBoard.Name = "intzaVolumeByBoard";
			this.intzaVolumeByBoard.Rows = 3;
			this.intzaVolumeByBoard.RowSelectColor = Color.FromArgb(95, 158, 160);
			this.intzaVolumeByBoard.RowSelectType = 0;
			this.intzaVolumeByBoard.RowsVisible = 3;
			this.intzaVolumeByBoard.ScrollChennelColor = Color.FromArgb(100, 100, 100);
			this.intzaVolumeByBoard.Size = new Size(221, 62);
			this.intzaVolumeByBoard.SortColumnName = "";
			this.intzaVolumeByBoard.SortType = SortType.Desc;
			this.intzaVolumeByBoard.TabIndex = 80;
			this.intzaVolumeByBoard.ItemDragDrop += new SortGrid.ItemDragDropEventHandler(this.intzaLS2_ItemDragDrop);
			this.intzaInfo.AllowDrop = true;
			this.intzaInfo.BackColor = Color.FromArgb(10, 10, 10);
			this.intzaInfo.CanDrag = false;
			this.intzaInfo.IsAutoRepaint = true;
			this.intzaInfo.IsDroped = false;
			itemGrid46.AdjustFontSize = 0f;
			itemGrid46.Alignment = StringAlignment.Near;
			itemGrid46.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid46.Changed = false;
			itemGrid46.FieldType = ItemType.Label2;
			itemGrid46.FontColor = Color.Gainsboro;
			itemGrid46.FontStyle = FontStyle.Regular;
			itemGrid46.Height = 1f;
			itemGrid46.IsBlink = 0;
			itemGrid46.Name = "lb_prior";
			itemGrid46.Text = "Prior";
			itemGrid46.ValueFormat = FormatType.Text;
			itemGrid46.Visible = true;
			itemGrid46.Width = 22;
			itemGrid46.X = 0;
			itemGrid46.Y = 3f;
			itemGrid47.AdjustFontSize = 0f;
			itemGrid47.Alignment = StringAlignment.Near;
			itemGrid47.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid47.Changed = false;
			itemGrid47.FieldType = ItemType.Text;
			itemGrid47.FontColor = Color.Yellow;
			itemGrid47.FontStyle = FontStyle.Regular;
			itemGrid47.Height = 1f;
			itemGrid47.IsBlink = 0;
			itemGrid47.Name = "prior";
			itemGrid47.Text = "";
			itemGrid47.ValueFormat = FormatType.Text;
			itemGrid47.Visible = true;
			itemGrid47.Width = 25;
			itemGrid47.X = 22;
			itemGrid47.Y = 3f;
			itemGrid48.AdjustFontSize = 0f;
			itemGrid48.Alignment = StringAlignment.Near;
			itemGrid48.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid48.Changed = false;
			itemGrid48.FieldType = ItemType.Label2;
			itemGrid48.FontColor = Color.Gainsboro;
			itemGrid48.FontStyle = FontStyle.Regular;
			itemGrid48.Height = 1f;
			itemGrid48.IsBlink = 0;
			itemGrid48.Name = "lb_ceiling";
			itemGrid48.Text = "Ceiling";
			itemGrid48.ValueFormat = FormatType.Text;
			itemGrid48.Visible = true;
			itemGrid48.Width = 22;
			itemGrid48.X = 0;
			itemGrid48.Y = 6f;
			itemGrid49.AdjustFontSize = 0f;
			itemGrid49.Alignment = StringAlignment.Near;
			itemGrid49.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid49.Changed = false;
			itemGrid49.FieldType = ItemType.Text;
			itemGrid49.FontColor = Color.Cyan;
			itemGrid49.FontStyle = FontStyle.Regular;
			itemGrid49.Height = 1f;
			itemGrid49.IsBlink = 0;
			itemGrid49.Name = "ceiling";
			itemGrid49.Text = "";
			itemGrid49.ValueFormat = FormatType.Price;
			itemGrid49.Visible = true;
			itemGrid49.Width = 25;
			itemGrid49.X = 22;
			itemGrid49.Y = 6f;
			itemGrid50.AdjustFontSize = 0f;
			itemGrid50.Alignment = StringAlignment.Near;
			itemGrid50.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid50.Changed = false;
			itemGrid50.FieldType = ItemType.Label2;
			itemGrid50.FontColor = Color.Gainsboro;
			itemGrid50.FontStyle = FontStyle.Regular;
			itemGrid50.Height = 1f;
			itemGrid50.IsBlink = 0;
			itemGrid50.Name = "lb_spread";
			itemGrid50.Text = "Spread";
			itemGrid50.ValueFormat = FormatType.Text;
			itemGrid50.Visible = true;
			itemGrid50.Width = 22;
			itemGrid50.X = 0;
			itemGrid50.Y = 8f;
			itemGrid51.AdjustFontSize = 0f;
			itemGrid51.Alignment = StringAlignment.Near;
			itemGrid51.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid51.Changed = false;
			itemGrid51.FieldType = ItemType.Text;
			itemGrid51.FontColor = Color.Yellow;
			itemGrid51.FontStyle = FontStyle.Regular;
			itemGrid51.Height = 1f;
			itemGrid51.IsBlink = 0;
			itemGrid51.Name = "spread";
			itemGrid51.Text = "";
			itemGrid51.ValueFormat = FormatType.Price;
			itemGrid51.Visible = true;
			itemGrid51.Width = 25;
			itemGrid51.X = 22;
			itemGrid51.Y = 8f;
			itemGrid52.AdjustFontSize = 0f;
			itemGrid52.Alignment = StringAlignment.Near;
			itemGrid52.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid52.Changed = false;
			itemGrid52.FieldType = ItemType.Label2;
			itemGrid52.FontColor = Color.Gainsboro;
			itemGrid52.FontStyle = FontStyle.Regular;
			itemGrid52.Height = 1f;
			itemGrid52.IsBlink = 0;
			itemGrid52.Name = "lb_flag";
			itemGrid52.Text = "Flag";
			itemGrid52.ValueFormat = FormatType.Text;
			itemGrid52.Visible = true;
			itemGrid52.Width = 22;
			itemGrid52.X = 0;
			itemGrid52.Y = 9f;
			itemGrid53.AdjustFontSize = 0f;
			itemGrid53.Alignment = StringAlignment.Near;
			itemGrid53.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid53.Changed = false;
			itemGrid53.FieldType = ItemType.Text;
			itemGrid53.FontColor = Color.Yellow;
			itemGrid53.FontStyle = FontStyle.Regular;
			itemGrid53.Height = 1f;
			itemGrid53.IsBlink = 0;
			itemGrid53.Name = "flag";
			itemGrid53.Text = "";
			itemGrid53.ValueFormat = FormatType.Text;
			itemGrid53.Visible = true;
			itemGrid53.Width = 25;
			itemGrid53.X = 22;
			itemGrid53.Y = 9f;
			itemGrid54.AdjustFontSize = 0f;
			itemGrid54.Alignment = StringAlignment.Near;
			itemGrid54.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid54.Changed = false;
			itemGrid54.FieldType = ItemType.Label2;
			itemGrid54.FontColor = Color.Gainsboro;
			itemGrid54.FontStyle = FontStyle.Regular;
			itemGrid54.Height = 1f;
			itemGrid54.IsBlink = 0;
			itemGrid54.Name = "lb_floor";
			itemGrid54.Text = "Floor";
			itemGrid54.ValueFormat = FormatType.Text;
			itemGrid54.Visible = true;
			itemGrid54.Width = 25;
			itemGrid54.X = 48;
			itemGrid54.Y = 6f;
			itemGrid55.AdjustFontSize = 0f;
			itemGrid55.Alignment = StringAlignment.Near;
			itemGrid55.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid55.Changed = false;
			itemGrid55.FieldType = ItemType.Text;
			itemGrid55.FontColor = Color.Magenta;
			itemGrid55.FontStyle = FontStyle.Regular;
			itemGrid55.Height = 1f;
			itemGrid55.IsBlink = 0;
			itemGrid55.Name = "floor";
			itemGrid55.Text = "";
			itemGrid55.ValueFormat = FormatType.Price;
			itemGrid55.Visible = true;
			itemGrid55.Width = 26;
			itemGrid55.X = 73;
			itemGrid55.Y = 6f;
			itemGrid56.AdjustFontSize = 0f;
			itemGrid56.Alignment = StringAlignment.Near;
			itemGrid56.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid56.Changed = false;
			itemGrid56.FieldType = ItemType.Label2;
			itemGrid56.FontColor = Color.Gainsboro;
			itemGrid56.FontStyle = FontStyle.Regular;
			itemGrid56.Height = 1f;
			itemGrid56.IsBlink = 0;
			itemGrid56.Name = "lb_par";
			itemGrid56.Text = "Par";
			itemGrid56.ValueFormat = FormatType.Text;
			itemGrid56.Visible = true;
			itemGrid56.Width = 22;
			itemGrid56.X = 0;
			itemGrid56.Y = 7f;
			itemGrid57.AdjustFontSize = 0f;
			itemGrid57.Alignment = StringAlignment.Near;
			itemGrid57.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid57.Changed = false;
			itemGrid57.FieldType = ItemType.Text;
			itemGrid57.FontColor = Color.Yellow;
			itemGrid57.FontStyle = FontStyle.Regular;
			itemGrid57.Height = 1f;
			itemGrid57.IsBlink = 0;
			itemGrid57.Name = "par";
			itemGrid57.Text = "";
			itemGrid57.ValueFormat = FormatType.Text;
			itemGrid57.Visible = true;
			itemGrid57.Width = 25;
			itemGrid57.X = 22;
			itemGrid57.Y = 7f;
			itemGrid58.AdjustFontSize = 0f;
			itemGrid58.Alignment = StringAlignment.Near;
			itemGrid58.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid58.Changed = false;
			itemGrid58.FieldType = ItemType.Label;
			itemGrid58.FontColor = Color.Gainsboro;
			itemGrid58.FontStyle = FontStyle.Regular;
			itemGrid58.Height = 1f;
			itemGrid58.IsBlink = 0;
			itemGrid58.Name = "lb_openvol";
			itemGrid58.Text = "OpnVol";
			itemGrid58.ValueFormat = FormatType.Text;
			itemGrid58.Visible = true;
			itemGrid58.Width = 25;
			itemGrid58.X = 0;
			itemGrid58.Y = 0f;
			itemGrid59.AdjustFontSize = 0f;
			itemGrid59.Alignment = StringAlignment.Far;
			itemGrid59.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid59.Changed = false;
			itemGrid59.FieldType = ItemType.Text;
			itemGrid59.FontColor = Color.Yellow;
			itemGrid59.FontStyle = FontStyle.Regular;
			itemGrid59.Height = 1f;
			itemGrid59.IsBlink = 0;
			itemGrid59.Name = "open_vol";
			itemGrid59.Text = "";
			itemGrid59.ValueFormat = FormatType.Volume;
			itemGrid59.Visible = true;
			itemGrid59.Width = 32;
			itemGrid59.X = 25;
			itemGrid59.Y = 0f;
			itemGrid60.AdjustFontSize = -2f;
			itemGrid60.Alignment = StringAlignment.Far;
			itemGrid60.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid60.Changed = false;
			itemGrid60.FieldType = ItemType.Text;
			itemGrid60.FontColor = Color.Yellow;
			itemGrid60.FontStyle = FontStyle.Regular;
			itemGrid60.Height = 1f;
			itemGrid60.IsBlink = 0;
			itemGrid60.Name = "p_open_vol";
			itemGrid60.Text = "";
			itemGrid60.ValueFormat = FormatType.Text;
			itemGrid60.Visible = true;
			itemGrid60.Width = 19;
			itemGrid60.X = 57;
			itemGrid60.Y = 0f;
			itemGrid61.AdjustFontSize = 0f;
			itemGrid61.Alignment = StringAlignment.Near;
			itemGrid61.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid61.Changed = false;
			itemGrid61.FieldType = ItemType.Label;
			itemGrid61.FontColor = Color.Gainsboro;
			itemGrid61.FontStyle = FontStyle.Regular;
			itemGrid61.Height = 1f;
			itemGrid61.IsBlink = 0;
			itemGrid61.Name = "lb_buyvol";
			itemGrid61.Text = "BuyVol";
			itemGrid61.ValueFormat = FormatType.Text;
			itemGrid61.Visible = true;
			itemGrid61.Width = 25;
			itemGrid61.X = 0;
			itemGrid61.Y = 1f;
			itemGrid62.AdjustFontSize = 0f;
			itemGrid62.Alignment = StringAlignment.Far;
			itemGrid62.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid62.Changed = false;
			itemGrid62.FieldType = ItemType.Text;
			itemGrid62.FontColor = Color.Lime;
			itemGrid62.FontStyle = FontStyle.Regular;
			itemGrid62.Height = 1f;
			itemGrid62.IsBlink = 0;
			itemGrid62.Name = "buy_vol";
			itemGrid62.Text = "";
			itemGrid62.ValueFormat = FormatType.Volume;
			itemGrid62.Visible = true;
			itemGrid62.Width = 32;
			itemGrid62.X = 25;
			itemGrid62.Y = 1f;
			itemGrid63.AdjustFontSize = -2f;
			itemGrid63.Alignment = StringAlignment.Far;
			itemGrid63.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid63.Changed = false;
			itemGrid63.FieldType = ItemType.Text;
			itemGrid63.FontColor = Color.Lime;
			itemGrid63.FontStyle = FontStyle.Regular;
			itemGrid63.Height = 1f;
			itemGrid63.IsBlink = 0;
			itemGrid63.Name = "p_buy_vol";
			itemGrid63.Text = "";
			itemGrid63.ValueFormat = FormatType.Text;
			itemGrid63.Visible = true;
			itemGrid63.Width = 19;
			itemGrid63.X = 57;
			itemGrid63.Y = 1f;
			itemGrid64.AdjustFontSize = 0f;
			itemGrid64.Alignment = StringAlignment.Near;
			itemGrid64.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid64.Changed = false;
			itemGrid64.FieldType = ItemType.Label;
			itemGrid64.FontColor = Color.Gainsboro;
			itemGrid64.FontStyle = FontStyle.Regular;
			itemGrid64.Height = 1f;
			itemGrid64.IsBlink = 0;
			itemGrid64.Name = "lb_selvol";
			itemGrid64.Text = "SelVol";
			itemGrid64.ValueFormat = FormatType.Text;
			itemGrid64.Visible = true;
			itemGrid64.Width = 25;
			itemGrid64.X = 0;
			itemGrid64.Y = 2f;
			itemGrid65.AdjustFontSize = 0f;
			itemGrid65.Alignment = StringAlignment.Far;
			itemGrid65.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid65.Changed = false;
			itemGrid65.FieldType = ItemType.Text;
			itemGrid65.FontColor = Color.Red;
			itemGrid65.FontStyle = FontStyle.Regular;
			itemGrid65.Height = 1f;
			itemGrid65.IsBlink = 0;
			itemGrid65.Name = "sel_vol";
			itemGrid65.Text = "";
			itemGrid65.ValueFormat = FormatType.Volume;
			itemGrid65.Visible = true;
			itemGrid65.Width = 32;
			itemGrid65.X = 25;
			itemGrid65.Y = 2f;
			itemGrid66.AdjustFontSize = -2f;
			itemGrid66.Alignment = StringAlignment.Far;
			itemGrid66.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid66.Changed = false;
			itemGrid66.FieldType = ItemType.Text;
			itemGrid66.FontColor = Color.Red;
			itemGrid66.FontStyle = FontStyle.Regular;
			itemGrid66.Height = 1f;
			itemGrid66.IsBlink = 0;
			itemGrid66.Name = "p_sel_vol";
			itemGrid66.Text = "";
			itemGrid66.ValueFormat = FormatType.Text;
			itemGrid66.Visible = true;
			itemGrid66.Width = 19;
			itemGrid66.X = 57;
			itemGrid66.Y = 2f;
			itemGrid67.AdjustFontSize = 0f;
			itemGrid67.Alignment = StringAlignment.Near;
			itemGrid67.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid67.Changed = false;
			itemGrid67.FieldType = ItemType.Label2;
			itemGrid67.FontColor = Color.Gainsboro;
			itemGrid67.FontStyle = FontStyle.Regular;
			itemGrid67.Height = 1f;
			itemGrid67.IsBlink = 0;
			itemGrid67.Name = "lbHigh";
			itemGrid67.Text = "High";
			itemGrid67.ValueFormat = FormatType.Text;
			itemGrid67.Visible = true;
			itemGrid67.Width = 22;
			itemGrid67.X = 0;
			itemGrid67.Y = 5f;
			itemGrid68.AdjustFontSize = 0f;
			itemGrid68.Alignment = StringAlignment.Near;
			itemGrid68.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid68.Changed = false;
			itemGrid68.FieldType = ItemType.Text;
			itemGrid68.FontColor = Color.White;
			itemGrid68.FontStyle = FontStyle.Regular;
			itemGrid68.Height = 1f;
			itemGrid68.IsBlink = 0;
			itemGrid68.Name = "high";
			itemGrid68.Text = "";
			itemGrid68.ValueFormat = FormatType.Price;
			itemGrid68.Visible = true;
			itemGrid68.Width = 25;
			itemGrid68.X = 22;
			itemGrid68.Y = 5f;
			itemGrid69.AdjustFontSize = 0f;
			itemGrid69.Alignment = StringAlignment.Near;
			itemGrid69.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid69.Changed = false;
			itemGrid69.FieldType = ItemType.Label2;
			itemGrid69.FontColor = Color.Gainsboro;
			itemGrid69.FontStyle = FontStyle.Regular;
			itemGrid69.Height = 1f;
			itemGrid69.IsBlink = 0;
			itemGrid69.Name = "lbLow";
			itemGrid69.Text = "Low";
			itemGrid69.ValueFormat = FormatType.Text;
			itemGrid69.Visible = true;
			itemGrid69.Width = 25;
			itemGrid69.X = 48;
			itemGrid69.Y = 5f;
			itemGrid70.AdjustFontSize = 0f;
			itemGrid70.Alignment = StringAlignment.Near;
			itemGrid70.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid70.Changed = false;
			itemGrid70.FieldType = ItemType.Text;
			itemGrid70.FontColor = Color.White;
			itemGrid70.FontStyle = FontStyle.Regular;
			itemGrid70.Height = 1f;
			itemGrid70.IsBlink = 0;
			itemGrid70.Name = "low";
			itemGrid70.Text = "";
			itemGrid70.ValueFormat = FormatType.Price;
			itemGrid70.Visible = true;
			itemGrid70.Width = 26;
			itemGrid70.X = 73;
			itemGrid70.Y = 5f;
			itemGrid71.AdjustFontSize = 0f;
			itemGrid71.Alignment = StringAlignment.Near;
			itemGrid71.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid71.Changed = false;
			itemGrid71.FieldType = ItemType.Label2;
			itemGrid71.FontColor = Color.Gainsboro;
			itemGrid71.FontStyle = FontStyle.Regular;
			itemGrid71.Height = 1f;
			itemGrid71.IsBlink = 0;
			itemGrid71.Name = "lb_avg";
			itemGrid71.Text = "Avg";
			itemGrid71.ValueFormat = FormatType.Text;
			itemGrid71.Visible = true;
			itemGrid71.Width = 25;
			itemGrid71.X = 48;
			itemGrid71.Y = 3f;
			itemGrid72.AdjustFontSize = 0f;
			itemGrid72.Alignment = StringAlignment.Near;
			itemGrid72.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid72.Changed = false;
			itemGrid72.FieldType = ItemType.Text;
			itemGrid72.FontColor = Color.White;
			itemGrid72.FontStyle = FontStyle.Regular;
			itemGrid72.Height = 1f;
			itemGrid72.IsBlink = 0;
			itemGrid72.Name = "avg";
			itemGrid72.Text = "";
			itemGrid72.ValueFormat = FormatType.Price;
			itemGrid72.Visible = true;
			itemGrid72.Width = 26;
			itemGrid72.X = 73;
			itemGrid72.Y = 3f;
			itemGrid73.AdjustFontSize = 0f;
			itemGrid73.Alignment = StringAlignment.Near;
			itemGrid73.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid73.Changed = false;
			itemGrid73.FieldType = ItemType.Label2;
			itemGrid73.FontColor = Color.Gainsboro;
			itemGrid73.FontStyle = FontStyle.Regular;
			itemGrid73.Height = 1f;
			itemGrid73.IsBlink = 0;
			itemGrid73.Name = "lbPoClose";
			itemGrid73.Text = "PrjClose";
			itemGrid73.ValueFormat = FormatType.Text;
			itemGrid73.Visible = true;
			itemGrid73.Width = 25;
			itemGrid73.X = 48;
			itemGrid73.Y = 7f;
			itemGrid74.AdjustFontSize = 0f;
			itemGrid74.Alignment = StringAlignment.Near;
			itemGrid74.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid74.Changed = false;
			itemGrid74.FieldType = ItemType.Text;
			itemGrid74.FontColor = Color.White;
			itemGrid74.FontStyle = FontStyle.Regular;
			itemGrid74.Height = 1f;
			itemGrid74.IsBlink = 0;
			itemGrid74.Name = "poclose";
			itemGrid74.Text = "";
			itemGrid74.ValueFormat = FormatType.Price;
			itemGrid74.Visible = true;
			itemGrid74.Width = 26;
			itemGrid74.X = 73;
			itemGrid74.Y = 7f;
			itemGrid75.AdjustFontSize = 0f;
			itemGrid75.Alignment = StringAlignment.Near;
			itemGrid75.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid75.Changed = false;
			itemGrid75.FieldType = ItemType.Label2;
			itemGrid75.FontColor = Color.Gainsboro;
			itemGrid75.FontStyle = FontStyle.Regular;
			itemGrid75.Height = 1f;
			itemGrid75.IsBlink = 0;
			itemGrid75.Name = "lbOpen";
			itemGrid75.Text = "Open-1";
			itemGrid75.ValueFormat = FormatType.Text;
			itemGrid75.Visible = true;
			itemGrid75.Width = 22;
			itemGrid75.X = 0;
			itemGrid75.Y = 4f;
			itemGrid76.AdjustFontSize = 0f;
			itemGrid76.Alignment = StringAlignment.Near;
			itemGrid76.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid76.Changed = false;
			itemGrid76.FieldType = ItemType.Text;
			itemGrid76.FontColor = Color.White;
			itemGrid76.FontStyle = FontStyle.Regular;
			itemGrid76.Height = 1f;
			itemGrid76.IsBlink = 0;
			itemGrid76.Name = "open1";
			itemGrid76.Text = "";
			itemGrid76.ValueFormat = FormatType.Price;
			itemGrid76.Visible = true;
			itemGrid76.Width = 25;
			itemGrid76.X = 22;
			itemGrid76.Y = 4f;
			itemGrid77.AdjustFontSize = 0f;
			itemGrid77.Alignment = StringAlignment.Near;
			itemGrid77.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid77.Changed = false;
			itemGrid77.FieldType = ItemType.Label2;
			itemGrid77.FontColor = Color.Gainsboro;
			itemGrid77.FontStyle = FontStyle.Regular;
			itemGrid77.Height = 1f;
			itemGrid77.IsBlink = 0;
			itemGrid77.Name = "lbOpen2";
			itemGrid77.Text = "Open-2";
			itemGrid77.ValueFormat = FormatType.Text;
			itemGrid77.Visible = true;
			itemGrid77.Width = 25;
			itemGrid77.X = 48;
			itemGrid77.Y = 4f;
			itemGrid78.AdjustFontSize = 0f;
			itemGrid78.Alignment = StringAlignment.Near;
			itemGrid78.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid78.Changed = false;
			itemGrid78.FieldType = ItemType.Text;
			itemGrid78.FontColor = Color.White;
			itemGrid78.FontStyle = FontStyle.Regular;
			itemGrid78.Height = 1f;
			itemGrid78.IsBlink = 0;
			itemGrid78.Name = "open2";
			itemGrid78.Text = "";
			itemGrid78.ValueFormat = FormatType.Price;
			itemGrid78.Visible = true;
			itemGrid78.Width = 26;
			itemGrid78.X = 73;
			itemGrid78.Y = 4f;
			itemGrid79.AdjustFontSize = 0f;
			itemGrid79.Alignment = StringAlignment.Near;
			itemGrid79.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid79.Changed = false;
			itemGrid79.FieldType = ItemType.Label2;
			itemGrid79.FontColor = Color.Gainsboro;
			itemGrid79.FontStyle = FontStyle.Regular;
			itemGrid79.Height = 1f;
			itemGrid79.IsBlink = 0;
			itemGrid79.Name = "lb_povol";
			itemGrid79.Text = "PrjVol";
			itemGrid79.ValueFormat = FormatType.Text;
			itemGrid79.Visible = true;
			itemGrid79.Width = 25;
			itemGrid79.X = 48;
			itemGrid79.Y = 8f;
			itemGrid80.AdjustFontSize = 0f;
			itemGrid80.Alignment = StringAlignment.Near;
			itemGrid80.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid80.Changed = false;
			itemGrid80.FieldType = ItemType.Text;
			itemGrid80.FontColor = Color.Yellow;
			itemGrid80.FontStyle = FontStyle.Regular;
			itemGrid80.Height = 1f;
			itemGrid80.IsBlink = 0;
			itemGrid80.Name = "povol";
			itemGrid80.Text = "";
			itemGrid80.ValueFormat = FormatType.Text;
			itemGrid80.Visible = true;
			itemGrid80.Width = 26;
			itemGrid80.X = 73;
			itemGrid80.Y = 8f;
			itemGrid81.AdjustFontSize = 0f;
			itemGrid81.Alignment = StringAlignment.Near;
			itemGrid81.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid81.Changed = false;
			itemGrid81.FieldType = ItemType.Text;
			itemGrid81.FontColor = Color.White;
			itemGrid81.FontStyle = FontStyle.Regular;
			itemGrid81.Height = 3f;
			itemGrid81.IsBlink = 0;
			itemGrid81.Name = "pie";
			itemGrid81.Text = "";
			itemGrid81.ValueFormat = FormatType.PieChart;
			itemGrid81.Visible = true;
			itemGrid81.Width = 24;
			itemGrid81.X = 76;
			itemGrid81.Y = 0f;
			itemGrid82.AdjustFontSize = 0f;
			itemGrid82.Alignment = StringAlignment.Near;
			itemGrid82.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid82.Changed = false;
			itemGrid82.FieldType = ItemType.Label2;
			itemGrid82.FontColor = Color.Gainsboro;
			itemGrid82.FontStyle = FontStyle.Regular;
			itemGrid82.Height = 1f;
			itemGrid82.IsBlink = 0;
			itemGrid82.Name = "lbMarginRate";
			itemGrid82.Text = "IM";
			itemGrid82.ValueFormat = FormatType.Text;
			itemGrid82.Visible = true;
			itemGrid82.Width = 25;
			itemGrid82.X = 48;
			itemGrid82.Y = 9f;
			itemGrid83.AdjustFontSize = 0f;
			itemGrid83.Alignment = StringAlignment.Near;
			itemGrid83.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid83.Changed = false;
			itemGrid83.FieldType = ItemType.Text;
			itemGrid83.FontColor = Color.Yellow;
			itemGrid83.FontStyle = FontStyle.Regular;
			itemGrid83.Height = 1f;
			itemGrid83.IsBlink = 0;
			itemGrid83.Name = "tbMarginRate";
			itemGrid83.Text = "";
			itemGrid83.ValueFormat = FormatType.Text;
			itemGrid83.Visible = true;
			itemGrid83.Width = 26;
			itemGrid83.X = 73;
			itemGrid83.Y = 9f;
			this.intzaInfo.Items.Add(itemGrid46);
			this.intzaInfo.Items.Add(itemGrid47);
			this.intzaInfo.Items.Add(itemGrid48);
			this.intzaInfo.Items.Add(itemGrid49);
			this.intzaInfo.Items.Add(itemGrid50);
			this.intzaInfo.Items.Add(itemGrid51);
			this.intzaInfo.Items.Add(itemGrid52);
			this.intzaInfo.Items.Add(itemGrid53);
			this.intzaInfo.Items.Add(itemGrid54);
			this.intzaInfo.Items.Add(itemGrid55);
			this.intzaInfo.Items.Add(itemGrid56);
			this.intzaInfo.Items.Add(itemGrid57);
			this.intzaInfo.Items.Add(itemGrid58);
			this.intzaInfo.Items.Add(itemGrid59);
			this.intzaInfo.Items.Add(itemGrid60);
			this.intzaInfo.Items.Add(itemGrid61);
			this.intzaInfo.Items.Add(itemGrid62);
			this.intzaInfo.Items.Add(itemGrid63);
			this.intzaInfo.Items.Add(itemGrid64);
			this.intzaInfo.Items.Add(itemGrid65);
			this.intzaInfo.Items.Add(itemGrid66);
			this.intzaInfo.Items.Add(itemGrid67);
			this.intzaInfo.Items.Add(itemGrid68);
			this.intzaInfo.Items.Add(itemGrid69);
			this.intzaInfo.Items.Add(itemGrid70);
			this.intzaInfo.Items.Add(itemGrid71);
			this.intzaInfo.Items.Add(itemGrid72);
			this.intzaInfo.Items.Add(itemGrid73);
			this.intzaInfo.Items.Add(itemGrid74);
			this.intzaInfo.Items.Add(itemGrid75);
			this.intzaInfo.Items.Add(itemGrid76);
			this.intzaInfo.Items.Add(itemGrid77);
			this.intzaInfo.Items.Add(itemGrid78);
			this.intzaInfo.Items.Add(itemGrid79);
			this.intzaInfo.Items.Add(itemGrid80);
			this.intzaInfo.Items.Add(itemGrid81);
			this.intzaInfo.Items.Add(itemGrid82);
			this.intzaInfo.Items.Add(itemGrid83);
			this.intzaInfo.LineColor = Color.Red;
			this.intzaInfo.Location = new Point(223, 26);
			this.intzaInfo.Margin = new Padding(2);
			this.intzaInfo.Name = "intzaInfo";
			this.intzaInfo.Size = new Size(138, 80);
			this.intzaInfo.TabIndex = 61;
			this.intzaInfo.TabStop = false;
			this.intzaInfo.ItemDragDrop += new IntzaCustomGrid.ItemDragDropEventHandler(this.intzaInfo_ItemDragDrop);
			this.intzaLS2.AllowDrop = true;
			this.intzaLS2.BackColor = Color.FromArgb(10, 10, 10);
			this.intzaLS2.CanBlink = true;
			this.intzaLS2.CanDrag = false;
			this.intzaLS2.CanGetMouseMove = false;
			columnItem39.Alignment = StringAlignment.Center;
			columnItem39.BackColor = Color.FromArgb(64, 64, 64);
			columnItem39.ColumnAlignment = StringAlignment.Center;
			columnItem39.FontColor = Color.LightGray;
			columnItem39.MyStyle = FontStyle.Regular;
			columnItem39.Name = "side";
			columnItem39.Text = "B/S";
			columnItem39.ValueFormat = FormatType.Text;
			columnItem39.Visible = true;
			columnItem39.Width = 13;
			columnItem40.Alignment = StringAlignment.Far;
			columnItem40.BackColor = Color.FromArgb(64, 64, 64);
			columnItem40.ColumnAlignment = StringAlignment.Center;
			columnItem40.FontColor = Color.LightGray;
			columnItem40.MyStyle = FontStyle.Regular;
			columnItem40.Name = "volume";
			columnItem40.Text = "Volume";
			columnItem40.ValueFormat = FormatType.Volume;
			columnItem40.Visible = true;
			columnItem40.Width = 36;
			columnItem41.Alignment = StringAlignment.Far;
			columnItem41.BackColor = Color.FromArgb(64, 64, 64);
			columnItem41.ColumnAlignment = StringAlignment.Center;
			columnItem41.FontColor = Color.LightGray;
			columnItem41.MyStyle = FontStyle.Regular;
			columnItem41.Name = "price";
			columnItem41.Text = "Price";
			columnItem41.ValueFormat = FormatType.Text;
			columnItem41.Visible = true;
			columnItem41.Width = 24;
			columnItem42.Alignment = StringAlignment.Far;
			columnItem42.BackColor = Color.FromArgb(64, 64, 64);
			columnItem42.ColumnAlignment = StringAlignment.Center;
			columnItem42.FontColor = Color.LightGray;
			columnItem42.MyStyle = FontStyle.Regular;
			columnItem42.Name = "time";
			columnItem42.Text = "Time";
			columnItem42.ValueFormat = FormatType.Text;
			columnItem42.Visible = true;
			columnItem42.Width = 27;
			this.intzaLS2.Columns.Add(columnItem39);
			this.intzaLS2.Columns.Add(columnItem40);
			this.intzaLS2.Columns.Add(columnItem41);
			this.intzaLS2.Columns.Add(columnItem42);
			this.intzaLS2.CurrentScroll = 0;
			this.intzaLS2.FocusItemIndex = -1;
			this.intzaLS2.ForeColor = Color.Black;
			this.intzaLS2.GridColor = Color.FromArgb(45, 45, 45);
			this.intzaLS2.HeaderPctHeight = 80f;
			this.intzaLS2.IsAutoRepaint = true;
			this.intzaLS2.IsDrawFullRow = true;
			this.intzaLS2.IsDrawGrid = false;
			this.intzaLS2.IsDrawHeader = true;
			this.intzaLS2.IsScrollable = false;
			this.intzaLS2.Location = new Point(575, 29);
			this.intzaLS2.MainColumn = "";
			this.intzaLS2.Margin = new Padding(2);
			this.intzaLS2.Name = "intzaLS2";
			this.intzaLS2.Rows = 9;
			this.intzaLS2.RowSelectColor = Color.FromArgb(95, 158, 160);
			this.intzaLS2.RowSelectType = 0;
			this.intzaLS2.RowsVisible = 9;
			this.intzaLS2.ScrollChennelColor = Color.LightGray;
			this.intzaLS2.Size = new Size(121, 77);
			this.intzaLS2.SortColumnName = "";
			this.intzaLS2.SortType = SortType.Desc;
			this.intzaLS2.TabIndex = 85;
			this.intzaLS2.ItemDragDrop += new SortGrid.ItemDragDropEventHandler(this.intzaLS2_ItemDragDrop);
			this.intzaBF.AllowDrop = true;
			this.intzaBF.BackColor = Color.FromArgb(10, 10, 10);
			this.intzaBF.CanDrag = false;
			this.intzaBF.IsAutoRepaint = true;
			this.intzaBF.IsDroped = false;
			itemGrid84.AdjustFontSize = 0f;
			itemGrid84.Alignment = StringAlignment.Near;
			itemGrid84.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid84.Changed = false;
			itemGrid84.FieldType = ItemType.Text;
			itemGrid84.FontColor = Color.White;
			itemGrid84.FontStyle = FontStyle.Regular;
			itemGrid84.Height = 1f;
			itemGrid84.IsBlink = 0;
			itemGrid84.Name = "item";
			itemGrid84.Text = "0";
			itemGrid84.ValueFormat = FormatType.BidOfferPct;
			itemGrid84.Visible = true;
			itemGrid84.Width = 100;
			itemGrid84.X = 0;
			itemGrid84.Y = 0f;
			this.intzaBF.Items.Add(itemGrid84);
			this.intzaBF.LineColor = Color.Red;
			this.intzaBF.Location = new Point(2, 107);
			this.intzaBF.Name = "intzaBF";
			this.intzaBF.Size = new Size(221, 17);
			this.intzaBF.TabIndex = 83;
			this.intzaTP.AllowDrop = true;
			this.intzaTP.BackColor = Color.FromArgb(10, 10, 10);
			this.intzaTP.CanBlink = true;
			this.intzaTP.CanDrag = false;
			this.intzaTP.CanGetMouseMove = false;
			columnItem43.Alignment = StringAlignment.Far;
			columnItem43.BackColor = Color.FromArgb(64, 64, 64);
			columnItem43.ColumnAlignment = StringAlignment.Center;
			columnItem43.FontColor = Color.LightGray;
			columnItem43.MyStyle = FontStyle.Regular;
			columnItem43.Name = "bidvolume";
			columnItem43.Text = "Volume";
			columnItem43.ValueFormat = FormatType.BidOfferVolume;
			columnItem43.Visible = true;
			columnItem43.Width = 31;
			columnItem44.Alignment = StringAlignment.Far;
			columnItem44.BackColor = Color.FromArgb(64, 64, 64);
			columnItem44.ColumnAlignment = StringAlignment.Center;
			columnItem44.FontColor = Color.LightGray;
			columnItem44.MyStyle = FontStyle.Regular;
			columnItem44.Name = "bid";
			columnItem44.Text = "Bid";
			columnItem44.ValueFormat = FormatType.Text;
			columnItem44.Visible = true;
			columnItem44.Width = 19;
			columnItem45.Alignment = StringAlignment.Far;
			columnItem45.BackColor = Color.FromArgb(64, 64, 64);
			columnItem45.ColumnAlignment = StringAlignment.Center;
			columnItem45.FontColor = Color.LightGray;
			columnItem45.MyStyle = FontStyle.Regular;
			columnItem45.Name = "offer";
			columnItem45.Text = "Offer";
			columnItem45.ValueFormat = FormatType.Text;
			columnItem45.Visible = true;
			columnItem45.Width = 19;
			columnItem46.Alignment = StringAlignment.Far;
			columnItem46.BackColor = Color.FromArgb(64, 64, 64);
			columnItem46.ColumnAlignment = StringAlignment.Center;
			columnItem46.FontColor = Color.LightGray;
			columnItem46.MyStyle = FontStyle.Regular;
			columnItem46.Name = "offervolume";
			columnItem46.Text = "Volume";
			columnItem46.ValueFormat = FormatType.BidOfferVolume;
			columnItem46.Visible = true;
			columnItem46.Width = 31;
			this.intzaTP.Columns.Add(columnItem43);
			this.intzaTP.Columns.Add(columnItem44);
			this.intzaTP.Columns.Add(columnItem45);
			this.intzaTP.Columns.Add(columnItem46);
			this.intzaTP.CurrentScroll = 0;
			this.intzaTP.FocusItemIndex = -1;
			this.intzaTP.ForeColor = Color.Black;
			this.intzaTP.GridColor = Color.FromArgb(45, 45, 45);
			this.intzaTP.HeaderPctHeight = 80f;
			this.intzaTP.IsAutoRepaint = true;
			this.intzaTP.IsDrawFullRow = false;
			this.intzaTP.IsDrawGrid = false;
			this.intzaTP.IsDrawHeader = true;
			this.intzaTP.IsScrollable = false;
			this.intzaTP.Location = new Point(2, 26);
			this.intzaTP.MainColumn = "";
			this.intzaTP.Margin = new Padding(2);
			this.intzaTP.Name = "intzaTP";
			this.intzaTP.Rows = 5;
			this.intzaTP.RowSelectColor = Color.FromArgb(95, 158, 160);
			this.intzaTP.RowSelectType = 0;
			this.intzaTP.RowsVisible = 5;
			this.intzaTP.ScrollChennelColor = Color.LightGray;
			this.intzaTP.Size = new Size(218, 80);
			this.intzaTP.SortColumnName = "";
			this.intzaTP.SortType = SortType.Desc;
			this.intzaTP.TabIndex = 86;
			this.intzaTP.TableMouseClick += new SortGrid.TableMouseClickEventHandler(this.intzaTP_TableMouseClick);
			this.intzaTP.ItemDragDrop += new SortGrid.ItemDragDropEventHandler(this.intzaLS2_ItemDragDrop);
			this.toolTip1.AutoPopDelay = 5000;
			this.toolTip1.InitialDelay = 300;
			this.toolTip1.IsBalloon = true;
			this.toolTip1.ReshowDelay = 500;
			this.toolTip1.ShowAlways = true;
			this.toolTip1.ToolTipIcon = ToolTipIcon.Info;
			this.toolTip1.ToolTipTitle = "Info guide";
			clsPermission.DisplayBuySell = enumDisplayBuySell.Yes;
			clsPermission.HistoricalDay = 30.0;
			clsPermission.Permission = enumPermission.Visible;
			clsPermission.VolType = null;
			clsPermission.WordingType = null;
			this.wcGraphVolume.ActiveSET = clsPermission;
			clsPermission2.DisplayBuySell = enumDisplayBuySell.Yes;
			clsPermission2.HistoricalDay = 30.0;
			clsPermission2.Permission = enumPermission.Visible;
			clsPermission2.VolType = null;
			clsPermission2.WordingType = null;
			this.wcGraphVolume.ActiveTFEX = clsPermission2;
			this.wcGraphVolume.BackColor = Color.FromArgb(10, 10, 10);
			this.wcGraphVolume.ColorBg = Color.FromArgb(10, 10, 10);
			this.wcGraphVolume.ColorBuy = Color.Lime;
			this.wcGraphVolume.ColorCeiling = Color.Aqua;
			this.wcGraphVolume.ColorDown = Color.Red;
			this.wcGraphVolume.ColorFloor = Color.Fuchsia;
			this.wcGraphVolume.ColorGrid = Color.DarkGray;
			this.wcGraphVolume.ColorNoChg = Color.Yellow;
			this.wcGraphVolume.ColorSell = Color.Red;
			this.wcGraphVolume.ColorUp = Color.Lime;
			this.wcGraphVolume.ColorValue = Color.White;
			this.wcGraphVolume.ColorVolume = Color.Yellow;
			this.wcGraphVolume.CurDate = null;
			this.wcGraphVolume.dictIPO = (Dictionary<string, float>)componentResourceManager.GetObject("wcGraphVolume.dictIPO");
			this.wcGraphVolume.Dock = DockStyle.Fill;
			this.wcGraphVolume.FontName = "Arial";
			this.wcGraphVolume.FontSize = 10f;
			this.wcGraphVolume.Location = new Point(1, 1);
			this.wcGraphVolume.Mode = 0;
			this.wcGraphVolume.Name = "wcGraphVolume";
			this.wcGraphVolume.Size = new Size(197, 69);
			this.wcGraphVolume.SymbolList = null;
			this.wcGraphVolume.SymbolType = enumType.eSet;
			this.wcGraphVolume.TabIndex = 91;
			this.wcGraphVolume.TextBoxBgColor = Color.Empty;
			this.wcGraphVolume.TextBoxForeColor = Color.Empty;
			this.wcGraphVolume.TypeMode = enumMode.Previous;
			this.panelVolAs.BackColor = Color.Gray;
			this.panelVolAs.Controls.Add(this.btnVolAsClose);
			this.panelVolAs.Controls.Add(this.wcGraphVolume);
			this.panelVolAs.Location = new Point(732, 41);
			this.panelVolAs.Name = "panelVolAs";
			this.panelVolAs.Padding = new Padding(1);
			this.panelVolAs.Size = new Size(199, 71);
			this.panelVolAs.TabIndex = 92;
			this.panelVolAs.Visible = false;
			this.btnVolAsClose.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.btnVolAsClose.BackColor = Color.Black;
			this.btnVolAsClose.FlatAppearance.BorderSize = 0;
			this.btnVolAsClose.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnVolAsClose.FlatAppearance.MouseOverBackColor = Color.Teal;
			this.btnVolAsClose.FlatStyle = FlatStyle.Flat;
			this.btnVolAsClose.Image = (Image)componentResourceManager.GetObject("btnVolAsClose.Image");
			this.btnVolAsClose.Location = new Point(178, 1);
			this.btnVolAsClose.Name = "btnVolAsClose";
			this.btnVolAsClose.Size = new Size(18, 18);
			this.btnVolAsClose.TabIndex = 92;
			this.btnVolAsClose.UseVisualStyleBackColor = false;
			this.btnVolAsClose.Click += new EventHandler(this.btnVolAsClose_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.DimGray;
			base.ClientSize = new Size(963, 398);
			base.Controls.Add(this.panelVolAs);
			base.Controls.Add(this.intzaInfoTFEX);
			base.Controls.Add(this.btnCloseChart);
			base.Controls.Add(this.intzaVolumeByBoard);
			base.Controls.Add(this.intzaInfo);
			base.Controls.Add(this.lbBBOLoading);
			base.Controls.Add(this.intzaLS2);
			base.Controls.Add(this.intzaBF);
			base.Controls.Add(this.lbSplashInfo);
			base.Controls.Add(this.lbChartLoading);
			base.Controls.Add(this.pictureBox1);
			base.Controls.Add(this.panelBidOffer);
			base.Controls.Add(this.tStripMenu);
			base.Controls.Add(this.intzaTP);
			base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			base.Margin = new Padding(4);
			base.Name = "frmMarketWatch";
			this.Text = "Market Watch";
			base.IDoShownDelay += new ClientBaseForm.OnShownDelayEventHandler(this.frmMarketWatch_IDoShownDelay);
			base.IDoLoadData += new ClientBaseForm.OnIDoLoadDataEventHandler(this.frmMarketWatch_IDoLoadData);
			base.IDoFontChanged += new ClientBaseForm.OnFontChangedEventHandler(this.frmMarketWatch_IDoFontChanged);
			base.IDoCustomSizeChanged += new ClientBaseForm.CustomSizeChangedEventHandler(this.frmMarketWatch_IDoCustomSizeChanged);
			base.IDoSymbolLinked += new ClientBaseForm.OnSymbolLinkEventHandler(this.frmMarketWatch_IDoSymbolLinked);
			base.IDoMainFormKeyUp += new ClientBaseForm.OnFormKeyUpEventHandler(this.frmMarketWatch_IDoMainFormKeyUp);
			base.IDoReActivated += new ClientBaseForm.OnReActiveEventHandler(this.frmMarketWatch_IDoReActivated);
			base.Controls.SetChildIndex(this.intzaTP, 0);
			base.Controls.SetChildIndex(this.tStripMenu, 0);
			base.Controls.SetChildIndex(this.panelBidOffer, 0);
			base.Controls.SetChildIndex(this.pictureBox1, 0);
			base.Controls.SetChildIndex(this.lbChartLoading, 0);
			base.Controls.SetChildIndex(this.lbSplashInfo, 0);
			base.Controls.SetChildIndex(this.intzaBF, 0);
			base.Controls.SetChildIndex(this.intzaLS2, 0);
			base.Controls.SetChildIndex(this.lbBBOLoading, 0);
			base.Controls.SetChildIndex(this.intzaInfo, 0);
			base.Controls.SetChildIndex(this.intzaVolumeByBoard, 0);
			base.Controls.SetChildIndex(this.btnCloseChart, 0);
			base.Controls.SetChildIndex(this.intzaInfoTFEX, 0);
			base.Controls.SetChildIndex(this.panelVolAs, 0);
			this.tStripMenu.ResumeLayout(false);
			this.tStripMenu.PerformLayout();
			this.panelBidOffer.ResumeLayout(false);
			this.panelBidOffer.PerformLayout();
			this.tStripCP.ResumeLayout(false);
			this.tStripCP.PerformLayout();
			this.tStripBBO.ResumeLayout(false);
			this.tStripBBO.PerformLayout();
			this.contextMenuStrip1.ResumeLayout(false);
			((ISupportInitialize)this.pictureBox1).EndInit();
			this.contextLink.ResumeLayout(false);
			this.panelVolAs.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
