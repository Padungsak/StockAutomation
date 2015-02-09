using i2TradePlus.Information;
using i2TradePlus.Properties;
using i2TradePlus.Templates;
using ITSNet.Common.BIZ;
using ITSNet.Common.BIZ.RealtimeMessage;
using ITSNet.Common.BIZ.RealtimeMessage.TFEX;
using STIControl;
using STIControl.CustomGrid;
using STIControl.SortTableGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace i2TradePlus.FixForm
{
	public class frmStockSummary : ClientBaseForm, IRealtimeMessage
	{
		private delegate void ReloadDataPageCallBack(string page, bool isForce);
		private delegate void UpdateLastSaleTicker_TFEXCallBack(decimal price, string side, long volume, string lastUpdate, int index, SeriesList.SeriesInformation seriesInfo);
		private delegate void UpdateMainBoardValue_TFEXCallBack(int deals, long accVolume, decimal accValue);
		private delegate bool SaleByPriceUpdateBuySellCallBack(decimal price, string side, int sideDeals, long sideVolume, bool isUpdate);
		private IContainer components = null;
		private ToolStrip tStripMenu;
		private ToolStripLabel tslblStockInPlayStock;
		private ToolStripComboBox tscbStock;
		private ToolStripButton tsbtnStockInPlayPrevPage;
		private ToolStripButton tsbtnStockInPlayNextPage;
		private IntzaCustomGrid intzaInfo;
		private IntzaCustomGrid intzaViewOddLotInfo;
		private ToolStripLabel tslbHour;
		private ToolStripTextBox tstxtSaleByTimeSearchTimeHour;
		private ToolStripLabel tslbMinute;
		private ToolStripTextBox tstxtSaleByTimeSearchTimeMinute;
		private ToolStripButton tsbtnSaleByTimeClearTime2;
		private ToolStripSeparator tssepSaleByTime2;
		private ToolStripButton tsbtnSaleByTimeFirstPage;
		private ToolStripButton tsbtnSaleByTimePrevPage;
		private ToolStripLabel tslblSaleByTimePageNo;
		private ToolStripButton tsbtnSaleByTimeNextPage;
		private ToolStripComboBox tscbSelection;
		private ToolStripLabel toolStripLabel1;
		private ToolStripSeparator toolStripSeparator1;
		private SortGrid intzaLS;
		private SortGrid intzaStockInPlay;
		private SortGrid intzaViewOddLot;
		private SortGrid intzaSaleByPrice;
		private SortGrid intzaSaleByTime;
		private ucVolumeAtPrice wcGraphVolume;
		private string currentPage = "Stock in Play";
		private string _subPage = string.Empty;
		private StockList.StockInformation _stockInfo = null;
		private SeriesList.SeriesInformation _seriesInfo = null;
		private int maxTopStockInPlay = 19;
		private decimal priceAtTopGrid = 0.0m;
		private decimal priceAtBottomGrid = 0.0m;
		private int _buyDeals = 0;
		private int _sellDeals = 0;
		private BackgroundWorker bgwStockInPlayLoadData = null;
		private DataSet dsStockInPlay = null;
		private BackgroundWorker bgwSaleByPriceLoadData = null;
		private DataSet dsSaleByPrice = null;
		private int saleByTimePageNo = 1;
		private BackgroundWorker bgwSaleByTimeLoadData = null;
		private DataSet dsSaleByTime = null;
		private BackgroundWorker bgwViewOddLotLoadData = null;
		private DataSet dsViewOddLot = null;
		private string _currentSymbol = string.Empty;
		private bool _isNewStock = false;
		private decimal _tfexBidPrice1 = 0m;
		private decimal _tfexBidPrice2 = 0m;
		private decimal _tfexBidPrice3 = 0m;
		private decimal _tfexBidPrice4 = 0m;
		private decimal _tfexBidPrice5 = 0m;
		private decimal _tfexAskPrice1 = 0m;
		private decimal _tfexAskPrice2 = 0m;
		private decimal _tfexAskPrice3 = 0m;
		private decimal _tfexAskPrice4 = 0m;
		private decimal _tfexAskPrice5 = 0m;
		public string SubPage
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this._subPage;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this._subPage = value;
			}
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmStockSummary));
			clsPermission clsPermission = new clsPermission();
			clsPermission clsPermission2 = new clsPermission();
			ColumnItem columnItem = new ColumnItem();
			ColumnItem columnItem2 = new ColumnItem();
			ColumnItem columnItem3 = new ColumnItem();
			ColumnItem columnItem4 = new ColumnItem();
			ColumnItem columnItem5 = new ColumnItem();
			ColumnItem columnItem6 = new ColumnItem();
			ColumnItem columnItem7 = new ColumnItem();
			ColumnItem columnItem8 = new ColumnItem();
			ColumnItem columnItem9 = new ColumnItem();
			ColumnItem columnItem10 = new ColumnItem();
			ColumnItem columnItem11 = new ColumnItem();
			ColumnItem columnItem12 = new ColumnItem();
			ColumnItem columnItem13 = new ColumnItem();
			ColumnItem columnItem14 = new ColumnItem();
			ColumnItem columnItem15 = new ColumnItem();
			ColumnItem columnItem16 = new ColumnItem();
			ColumnItem columnItem17 = new ColumnItem();
			ColumnItem columnItem18 = new ColumnItem();
			ColumnItem columnItem19 = new ColumnItem();
			ColumnItem columnItem20 = new ColumnItem();
			ColumnItem columnItem21 = new ColumnItem();
			ColumnItem columnItem22 = new ColumnItem();
			ColumnItem columnItem23 = new ColumnItem();
			ColumnItem columnItem24 = new ColumnItem();
			ColumnItem columnItem25 = new ColumnItem();
			ColumnItem columnItem26 = new ColumnItem();
			ColumnItem columnItem27 = new ColumnItem();
			ColumnItem columnItem28 = new ColumnItem();
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
			this.tStripMenu = new ToolStrip();
			this.toolStripLabel1 = new ToolStripLabel();
			this.tscbSelection = new ToolStripComboBox();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.tslblStockInPlayStock = new ToolStripLabel();
			this.tscbStock = new ToolStripComboBox();
			this.tsbtnStockInPlayPrevPage = new ToolStripButton();
			this.tsbtnStockInPlayNextPage = new ToolStripButton();
			this.tslbHour = new ToolStripLabel();
			this.tstxtSaleByTimeSearchTimeHour = new ToolStripTextBox();
			this.tslbMinute = new ToolStripLabel();
			this.tstxtSaleByTimeSearchTimeMinute = new ToolStripTextBox();
			this.tsbtnSaleByTimeClearTime2 = new ToolStripButton();
			this.tssepSaleByTime2 = new ToolStripSeparator();
			this.tsbtnSaleByTimeFirstPage = new ToolStripButton();
			this.tsbtnSaleByTimePrevPage = new ToolStripButton();
			this.tslblSaleByTimePageNo = new ToolStripLabel();
			this.tsbtnSaleByTimeNextPage = new ToolStripButton();
			this.wcGraphVolume = new ucVolumeAtPrice();
			this.intzaSaleByTime = new SortGrid();
			this.intzaSaleByPrice = new SortGrid();
			this.intzaViewOddLot = new SortGrid();
			this.intzaStockInPlay = new SortGrid();
			this.intzaLS = new SortGrid();
			this.intzaViewOddLotInfo = new IntzaCustomGrid();
			this.intzaInfo = new IntzaCustomGrid();
			this.tStripMenu.SuspendLayout();
			base.SuspendLayout();
			this.tStripMenu.AllowMerge = false;
			this.tStripMenu.BackColor = Color.FromArgb(20, 20, 20);
			this.tStripMenu.GripStyle = ToolStripGripStyle.Hidden;
			this.tStripMenu.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripLabel1,
				this.tscbSelection,
				this.toolStripSeparator1,
				this.tslblStockInPlayStock,
				this.tscbStock,
				this.tsbtnStockInPlayPrevPage,
				this.tsbtnStockInPlayNextPage,
				this.tslbHour,
				this.tstxtSaleByTimeSearchTimeHour,
				this.tslbMinute,
				this.tstxtSaleByTimeSearchTimeMinute,
				this.tsbtnSaleByTimeClearTime2,
				this.tssepSaleByTime2,
				this.tsbtnSaleByTimeFirstPage,
				this.tsbtnSaleByTimePrevPage,
				this.tslblSaleByTimePageNo,
				this.tsbtnSaleByTimeNextPage
			});
			this.tStripMenu.Location = new Point(0, 0);
			this.tStripMenu.Name = "tStripMenu";
			this.tStripMenu.Padding = new Padding(1, 1, 1, 2);
			this.tStripMenu.RenderMode = ToolStripRenderMode.Professional;
			this.tStripMenu.Size = new Size(888, 26);
			this.tStripMenu.TabIndex = 17;
			this.tStripMenu.TabStop = true;
			this.tStripMenu.Tag = "-1";
			this.toolStripLabel1.ForeColor = Color.LightGray;
			this.toolStripLabel1.Margin = new Padding(2, 1, 2, 2);
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new Size(33, 20);
			this.toolStripLabel1.Text = "Page";
			this.tscbSelection.BackColor = Color.FromArgb(45, 45, 45);
			this.tscbSelection.DropDownStyle = ComboBoxStyle.DropDownList;
			this.tscbSelection.ForeColor = Color.LightGray;
			this.tscbSelection.Items.AddRange(new object[]
			{
				"Stock in Play",
				"Sale by Price",
				"Sale by Time",
				"View Oddlot"
			});
			this.tscbSelection.Name = "tscbSelection";
			this.tscbSelection.Size = new Size(130, 23);
			this.tscbSelection.SelectedIndexChanged += new EventHandler(this.tscbSelection_SelectedIndexChanged);
			this.toolStripSeparator1.Margin = new Padding(5, 0, 5, 0);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 23);
			this.tslblStockInPlayStock.BackColor = Color.Transparent;
			this.tslblStockInPlayStock.Font = new Font("Microsoft Sans Serif", 8.25f);
			this.tslblStockInPlayStock.ForeColor = Color.LightGray;
			this.tslblStockInPlayStock.Margin = new Padding(5, 1, 0, 2);
			this.tslblStockInPlayStock.Name = "tslblStockInPlayStock";
			this.tslblStockInPlayStock.Size = new Size(35, 20);
			this.tslblStockInPlayStock.Text = "Stock";
			this.tscbStock.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.tscbStock.BackColor = Color.FromArgb(45, 45, 45);
			this.tscbStock.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.tscbStock.ForeColor = Color.Yellow;
			this.tscbStock.MaxLength = 25;
			this.tscbStock.Name = "tscbStock";
			this.tscbStock.Size = new Size(125, 23);
			this.tscbStock.SelectedIndexChanged += new EventHandler(this.tscbStock_SelectedIndexChanged);
			this.tscbStock.KeyUp += new KeyEventHandler(this.tscbStock_KeyUp);
			this.tscbStock.KeyDown += new KeyEventHandler(this.comboStock_KeyDown);
			this.tscbStock.KeyPress += new KeyPressEventHandler(this.tscbStock_KeyPress);
			this.tsbtnStockInPlayPrevPage.Font = new Font("Microsoft Sans Serif", 8.25f);
			this.tsbtnStockInPlayPrevPage.ForeColor = Color.LightGray;
			this.tsbtnStockInPlayPrevPage.Image = (Image)componentResourceManager.GetObject("tsbtnStockInPlayPrevPage.Image");
			this.tsbtnStockInPlayPrevPage.ImageTransparentColor = Color.Magenta;
			this.tsbtnStockInPlayPrevPage.Margin = new Padding(5, 1, 0, 2);
			this.tsbtnStockInPlayPrevPage.Name = "tsbtnStockInPlayPrevPage";
			this.tsbtnStockInPlayPrevPage.Size = new Size(69, 20);
			this.tsbtnStockInPlayPrevPage.Text = "Page Up";
			this.tsbtnStockInPlayPrevPage.ToolTipText = "Up Page";
			this.tsbtnStockInPlayPrevPage.Click += new EventHandler(this.tsbtnStockInPlayPrevPage_Click);
			this.tsbtnStockInPlayNextPage.Font = new Font("Microsoft Sans Serif", 8.25f);
			this.tsbtnStockInPlayNextPage.ForeColor = Color.LightGray;
			this.tsbtnStockInPlayNextPage.Image = (Image)componentResourceManager.GetObject("tsbtnStockInPlayNextPage.Image");
			this.tsbtnStockInPlayNextPage.ImageTransparentColor = Color.Magenta;
			this.tsbtnStockInPlayNextPage.Name = "tsbtnStockInPlayNextPage";
			this.tsbtnStockInPlayNextPage.Size = new Size(83, 20);
			this.tsbtnStockInPlayNextPage.Text = "Page Down";
			this.tsbtnStockInPlayNextPage.ToolTipText = "Down Page";
			this.tsbtnStockInPlayNextPage.Click += new EventHandler(this.tsbtnStockInPlayNextPage_Click);
			this.tslbHour.Font = new Font("Microsoft Sans Serif", 8.25f);
			this.tslbHour.ForeColor = Color.LightGray;
			this.tslbHour.Name = "tslbHour";
			this.tslbHour.Size = new Size(36, 20);
			this.tslbHour.Text = "Hour :";
			this.tstxtSaleByTimeSearchTimeHour.BackColor = Color.FromArgb(45, 45, 45);
			this.tstxtSaleByTimeSearchTimeHour.BorderStyle = BorderStyle.FixedSingle;
			this.tstxtSaleByTimeSearchTimeHour.Font = new Font("Microsoft Sans Serif", 8.25f);
			this.tstxtSaleByTimeSearchTimeHour.ForeColor = Color.LightGray;
			this.tstxtSaleByTimeSearchTimeHour.MaxLength = 2;
			this.tstxtSaleByTimeSearchTimeHour.Name = "tstxtSaleByTimeSearchTimeHour";
			this.tstxtSaleByTimeSearchTimeHour.Size = new Size(26, 23);
			this.tstxtSaleByTimeSearchTimeHour.TextBoxTextAlign = HorizontalAlignment.Center;
			this.tstxtSaleByTimeSearchTimeHour.ToolTipText = "{ เวลา ที่ต้องการค้นหา หน่วยเป็น ชม.}";
			this.tstxtSaleByTimeSearchTimeHour.KeyUp += new KeyEventHandler(this.tstxtSaleByTimeSearchTimeHour_KeyUp);
			this.tslbMinute.Font = new Font("Microsoft Sans Serif", 8.25f);
			this.tslbMinute.ForeColor = Color.LightGray;
			this.tslbMinute.Margin = new Padding(2, 1, 0, 2);
			this.tslbMinute.Name = "tslbMinute";
			this.tslbMinute.Size = new Size(45, 20);
			this.tslbMinute.Text = "Minute :";
			this.tstxtSaleByTimeSearchTimeMinute.BackColor = Color.FromArgb(45, 45, 45);
			this.tstxtSaleByTimeSearchTimeMinute.BorderStyle = BorderStyle.FixedSingle;
			this.tstxtSaleByTimeSearchTimeMinute.Font = new Font("Microsoft Sans Serif", 8.25f);
			this.tstxtSaleByTimeSearchTimeMinute.ForeColor = Color.LightGray;
			this.tstxtSaleByTimeSearchTimeMinute.MaxLength = 2;
			this.tstxtSaleByTimeSearchTimeMinute.Name = "tstxtSaleByTimeSearchTimeMinute";
			this.tstxtSaleByTimeSearchTimeMinute.Size = new Size(26, 23);
			this.tstxtSaleByTimeSearchTimeMinute.TextBoxTextAlign = HorizontalAlignment.Center;
			this.tstxtSaleByTimeSearchTimeMinute.ToolTipText = "{ เวลา ที่ต้องการค้นหา หน่วยเป็น นาที}";
			this.tstxtSaleByTimeSearchTimeMinute.KeyUp += new KeyEventHandler(this.tstxtSaleByTimeSearchTimeMinute_KeyUp);
			this.tsbtnSaleByTimeClearTime2.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnSaleByTimeClearTime2.ForeColor = Color.LightGray;
			this.tsbtnSaleByTimeClearTime2.ImageTransparentColor = Color.Magenta;
			this.tsbtnSaleByTimeClearTime2.Margin = new Padding(5, 1, 3, 2);
			this.tsbtnSaleByTimeClearTime2.Name = "tsbtnSaleByTimeClearTime2";
			this.tsbtnSaleByTimeClearTime2.Size = new Size(38, 20);
			this.tsbtnSaleByTimeClearTime2.Text = "Clear";
			this.tsbtnSaleByTimeClearTime2.ToolTipText = "Clear Filter";
			this.tsbtnSaleByTimeClearTime2.Click += new EventHandler(this.tsbtnSaleByTimeClearTime_Click);
			this.tssepSaleByTime2.Margin = new Padding(2, 0, 5, 0);
			this.tssepSaleByTime2.Name = "tssepSaleByTime2";
			this.tssepSaleByTime2.Size = new Size(6, 23);
			this.tsbtnSaleByTimeFirstPage.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbtnSaleByTimeFirstPage.Font = new Font("Microsoft Sans Serif", 8.25f);
			this.tsbtnSaleByTimeFirstPage.Image = Resources.MoveFirstHS;
			this.tsbtnSaleByTimeFirstPage.ImageTransparentColor = Color.Magenta;
			this.tsbtnSaleByTimeFirstPage.Name = "tsbtnSaleByTimeFirstPage";
			this.tsbtnSaleByTimeFirstPage.Size = new Size(23, 20);
			this.tsbtnSaleByTimeFirstPage.ToolTipText = "First";
			this.tsbtnSaleByTimeFirstPage.Click += new EventHandler(this.tsbtnSaleByTimeFirstPage_Click);
			this.tsbtnSaleByTimePrevPage.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbtnSaleByTimePrevPage.Font = new Font("Microsoft Sans Serif", 8.25f);
			this.tsbtnSaleByTimePrevPage.Image = Resources.MovePreviousHS;
			this.tsbtnSaleByTimePrevPage.ImageTransparentColor = Color.Magenta;
			this.tsbtnSaleByTimePrevPage.Name = "tsbtnSaleByTimePrevPage";
			this.tsbtnSaleByTimePrevPage.Size = new Size(23, 20);
			this.tsbtnSaleByTimePrevPage.ToolTipText = "Previous";
			this.tsbtnSaleByTimePrevPage.Click += new EventHandler(this.tsbtnSaleByTimePrevPage_Click);
			this.tslblSaleByTimePageNo.BackColor = Color.Transparent;
			this.tslblSaleByTimePageNo.Font = new Font("Microsoft Sans Serif", 8.25f);
			this.tslblSaleByTimePageNo.ForeColor = Color.LightGray;
			this.tslblSaleByTimePageNo.Name = "tslblSaleByTimePageNo";
			this.tslblSaleByTimePageNo.Size = new Size(13, 20);
			this.tslblSaleByTimePageNo.Text = "0";
			this.tsbtnSaleByTimeNextPage.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbtnSaleByTimeNextPage.Font = new Font("Microsoft Sans Serif", 8.25f);
			this.tsbtnSaleByTimeNextPage.Image = Resources.MoveNextHS;
			this.tsbtnSaleByTimeNextPage.ImageTransparentColor = Color.Magenta;
			this.tsbtnSaleByTimeNextPage.Name = "tsbtnSaleByTimeNextPage";
			this.tsbtnSaleByTimeNextPage.Size = new Size(23, 20);
			this.tsbtnSaleByTimeNextPage.ToolTipText = "Next";
			this.tsbtnSaleByTimeNextPage.Click += new EventHandler(this.tsbtnSaleByTimeNextPage_Click);
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
			this.wcGraphVolume.FontName = "Arial";
			this.wcGraphVolume.FontSize = 10f;
			this.wcGraphVolume.Location = new Point(401, 32);
			this.wcGraphVolume.Mode = 0;
			this.wcGraphVolume.Name = "wcGraphVolume";
			this.wcGraphVolume.Size = new Size(87, 36);
			this.wcGraphVolume.SymbolList = null;
			this.wcGraphVolume.SymbolType = enumType.eSet;
			this.wcGraphVolume.TabIndex = 29;
			this.wcGraphVolume.TextBoxBgColor = Color.Empty;
			this.wcGraphVolume.TextBoxForeColor = Color.Empty;
			this.wcGraphVolume.TypeMode = enumMode.Previous;
			this.intzaSaleByTime.AllowDrop = true;
			this.intzaSaleByTime.BackColor = Color.FromArgb(10, 10, 10);
			this.intzaSaleByTime.CanBlink = true;
			this.intzaSaleByTime.CanDrag = false;
			this.intzaSaleByTime.CanGetMouseMove = false;
			columnItem.Alignment = StringAlignment.Center;
			columnItem.BackColor = Color.FromArgb(64, 64, 64);
			columnItem.ColumnAlignment = StringAlignment.Center;
			columnItem.FontColor = Color.LightGray;
			columnItem.MyStyle = FontStyle.Regular;
			columnItem.Name = "time";
			columnItem.Text = "Time";
			columnItem.ValueFormat = FormatType.Text;
			columnItem.Visible = true;
			columnItem.Width = 22;
			columnItem2.Alignment = StringAlignment.Center;
			columnItem2.BackColor = Color.FromArgb(64, 64, 64);
			columnItem2.ColumnAlignment = StringAlignment.Center;
			columnItem2.FontColor = Color.LightGray;
			columnItem2.MyStyle = FontStyle.Regular;
			columnItem2.Name = "side";
			columnItem2.Text = "B/S";
			columnItem2.ValueFormat = FormatType.Text;
			columnItem2.Visible = true;
			columnItem2.Width = 11;
			columnItem3.Alignment = StringAlignment.Far;
			columnItem3.BackColor = Color.FromArgb(64, 64, 64);
			columnItem3.ColumnAlignment = StringAlignment.Center;
			columnItem3.FontColor = Color.LightGray;
			columnItem3.MyStyle = FontStyle.Regular;
			columnItem3.Name = "volume";
			columnItem3.Text = "Volume";
			columnItem3.ValueFormat = FormatType.Volume;
			columnItem3.Visible = true;
			columnItem3.Width = 22;
			columnItem4.Alignment = StringAlignment.Far;
			columnItem4.BackColor = Color.FromArgb(64, 64, 64);
			columnItem4.ColumnAlignment = StringAlignment.Center;
			columnItem4.FontColor = Color.LightGray;
			columnItem4.MyStyle = FontStyle.Regular;
			columnItem4.Name = "price";
			columnItem4.Text = "Price";
			columnItem4.ValueFormat = FormatType.Price;
			columnItem4.Visible = true;
			columnItem4.Width = 15;
			columnItem5.Alignment = StringAlignment.Far;
			columnItem5.BackColor = Color.FromArgb(64, 64, 64);
			columnItem5.ColumnAlignment = StringAlignment.Center;
			columnItem5.FontColor = Color.LightGray;
			columnItem5.MyStyle = FontStyle.Regular;
			columnItem5.Name = "chg";
			columnItem5.Text = "Change";
			columnItem5.ValueFormat = FormatType.ChangePrice;
			columnItem5.Visible = true;
			columnItem5.Width = 15;
			columnItem6.Alignment = StringAlignment.Far;
			columnItem6.BackColor = Color.FromArgb(64, 64, 64);
			columnItem6.ColumnAlignment = StringAlignment.Center;
			columnItem6.FontColor = Color.LightGray;
			columnItem6.MyStyle = FontStyle.Regular;
			columnItem6.Name = "avg";
			columnItem6.Text = "Average";
			columnItem6.ValueFormat = FormatType.Price;
			columnItem6.Visible = true;
			columnItem6.Width = 15;
			this.intzaSaleByTime.Columns.Add(columnItem);
			this.intzaSaleByTime.Columns.Add(columnItem2);
			this.intzaSaleByTime.Columns.Add(columnItem3);
			this.intzaSaleByTime.Columns.Add(columnItem4);
			this.intzaSaleByTime.Columns.Add(columnItem5);
			this.intzaSaleByTime.Columns.Add(columnItem6);
			this.intzaSaleByTime.CurrentScroll = 0;
			this.intzaSaleByTime.FocusItemIndex = -1;
			this.intzaSaleByTime.ForeColor = Color.Black;
			this.intzaSaleByTime.GridColor = Color.FromArgb(45, 45, 45);
			this.intzaSaleByTime.HeaderPctHeight = 100f;
			this.intzaSaleByTime.IsAutoRepaint = true;
			this.intzaSaleByTime.IsDrawFullRow = false;
			this.intzaSaleByTime.IsDrawGrid = true;
			this.intzaSaleByTime.IsDrawHeader = true;
			this.intzaSaleByTime.IsScrollable = false;
			this.intzaSaleByTime.Location = new Point(12, 74);
			this.intzaSaleByTime.MainColumn = "";
			this.intzaSaleByTime.Name = "intzaSaleByTime";
			this.intzaSaleByTime.Rows = 0;
			this.intzaSaleByTime.RowSelectColor = Color.FromArgb(95, 158, 160);
			this.intzaSaleByTime.RowSelectType = 0;
			this.intzaSaleByTime.RowsVisible = 0;
			this.intzaSaleByTime.ScrollChennelColor = Color.LightGray;
			this.intzaSaleByTime.Size = new Size(540, 85);
			this.intzaSaleByTime.SortColumnName = "";
			this.intzaSaleByTime.SortType = SortType.Desc;
			this.intzaSaleByTime.TabIndex = 28;
			this.intzaSaleByTime.TableMouseClick += new SortGrid.TableMouseClickEventHandler(this.intzaSaleByTime_TableMouseClick);
			this.intzaSaleByPrice.AllowDrop = true;
			this.intzaSaleByPrice.BackColor = Color.FromArgb(10, 10, 10);
			this.intzaSaleByPrice.CanBlink = true;
			this.intzaSaleByPrice.CanDrag = false;
			this.intzaSaleByPrice.CanGetMouseMove = false;
			columnItem7.Alignment = StringAlignment.Far;
			columnItem7.BackColor = Color.FromArgb(64, 64, 64);
			columnItem7.ColumnAlignment = StringAlignment.Center;
			columnItem7.FontColor = Color.LightGray;
			columnItem7.MyStyle = FontStyle.Regular;
			columnItem7.Name = "buy_deal";
			columnItem7.Text = "Deals";
			columnItem7.ValueFormat = FormatType.Volume;
			columnItem7.Visible = true;
			columnItem7.Width = 10;
			columnItem8.Alignment = StringAlignment.Far;
			columnItem8.BackColor = Color.FromArgb(64, 64, 64);
			columnItem8.ColumnAlignment = StringAlignment.Center;
			columnItem8.FontColor = Color.LightGray;
			columnItem8.MyStyle = FontStyle.Regular;
			columnItem8.Name = "buy_vol";
			columnItem8.Text = "Buy Volume";
			columnItem8.ValueFormat = FormatType.Volume;
			columnItem8.Visible = true;
			columnItem8.Width = 17;
			columnItem9.Alignment = StringAlignment.Center;
			columnItem9.BackColor = Color.FromArgb(64, 64, 64);
			columnItem9.ColumnAlignment = StringAlignment.Center;
			columnItem9.FontColor = Color.LightGray;
			columnItem9.MyStyle = FontStyle.Regular;
			columnItem9.Name = "price";
			columnItem9.Text = "Price";
			columnItem9.ValueFormat = FormatType.Text;
			columnItem9.Visible = true;
			columnItem9.Width = 12;
			columnItem10.Alignment = StringAlignment.Far;
			columnItem10.BackColor = Color.FromArgb(64, 64, 64);
			columnItem10.ColumnAlignment = StringAlignment.Center;
			columnItem10.FontColor = Color.LightGray;
			columnItem10.MyStyle = FontStyle.Regular;
			columnItem10.Name = "sell_vol";
			columnItem10.Text = "Sell Volume";
			columnItem10.ValueFormat = FormatType.Volume;
			columnItem10.Visible = true;
			columnItem10.Width = 17;
			columnItem11.Alignment = StringAlignment.Far;
			columnItem11.BackColor = Color.FromArgb(64, 64, 64);
			columnItem11.ColumnAlignment = StringAlignment.Center;
			columnItem11.FontColor = Color.LightGray;
			columnItem11.MyStyle = FontStyle.Regular;
			columnItem11.Name = "sell_deal";
			columnItem11.Text = "Deals";
			columnItem11.ValueFormat = FormatType.Volume;
			columnItem11.Visible = true;
			columnItem11.Width = 10;
			columnItem12.Alignment = StringAlignment.Far;
			columnItem12.BackColor = Color.FromArgb(64, 64, 64);
			columnItem12.ColumnAlignment = StringAlignment.Center;
			columnItem12.FontColor = Color.LightGray;
			columnItem12.MyStyle = FontStyle.Regular;
			columnItem12.Name = "mvol";
			columnItem12.Text = "Volume";
			columnItem12.ValueFormat = FormatType.Volume;
			columnItem12.Visible = true;
			columnItem12.Width = 15;
			columnItem13.Alignment = StringAlignment.Far;
			columnItem13.BackColor = Color.FromArgb(64, 64, 64);
			columnItem13.ColumnAlignment = StringAlignment.Center;
			columnItem13.FontColor = Color.LightGray;
			columnItem13.MyStyle = FontStyle.Regular;
			columnItem13.Name = "mval";
			columnItem13.Text = "Value";
			columnItem13.ValueFormat = FormatType.Volume;
			columnItem13.Visible = true;
			columnItem13.Width = 19;
			this.intzaSaleByPrice.Columns.Add(columnItem7);
			this.intzaSaleByPrice.Columns.Add(columnItem8);
			this.intzaSaleByPrice.Columns.Add(columnItem9);
			this.intzaSaleByPrice.Columns.Add(columnItem10);
			this.intzaSaleByPrice.Columns.Add(columnItem11);
			this.intzaSaleByPrice.Columns.Add(columnItem12);
			this.intzaSaleByPrice.Columns.Add(columnItem13);
			this.intzaSaleByPrice.CurrentScroll = 0;
			this.intzaSaleByPrice.FocusItemIndex = -1;
			this.intzaSaleByPrice.ForeColor = Color.Black;
			this.intzaSaleByPrice.GridColor = Color.FromArgb(45, 45, 45);
			this.intzaSaleByPrice.HeaderPctHeight = 80f;
			this.intzaSaleByPrice.IsAutoRepaint = true;
			this.intzaSaleByPrice.IsDrawFullRow = false;
			this.intzaSaleByPrice.IsDrawGrid = true;
			this.intzaSaleByPrice.IsDrawHeader = true;
			this.intzaSaleByPrice.IsScrollable = true;
			this.intzaSaleByPrice.Location = new Point(12, 165);
			this.intzaSaleByPrice.MainColumn = "";
			this.intzaSaleByPrice.Name = "intzaSaleByPrice";
			this.intzaSaleByPrice.Rows = 0;
			this.intzaSaleByPrice.RowSelectColor = Color.FromArgb(95, 158, 160);
			this.intzaSaleByPrice.RowSelectType = 0;
			this.intzaSaleByPrice.RowsVisible = 0;
			this.intzaSaleByPrice.ScrollChennelColor = Color.Gray;
			this.intzaSaleByPrice.Size = new Size(540, 53);
			this.intzaSaleByPrice.SortColumnName = "";
			this.intzaSaleByPrice.SortType = SortType.Desc;
			this.intzaSaleByPrice.TabIndex = 27;
			this.intzaSaleByPrice.TableMouseClick += new SortGrid.TableMouseClickEventHandler(this.intzaSaleByPrice_TableMouseClick);
			this.intzaViewOddLot.AllowDrop = true;
			this.intzaViewOddLot.BackColor = Color.FromArgb(10, 10, 10);
			this.intzaViewOddLot.CanBlink = true;
			this.intzaViewOddLot.CanDrag = false;
			this.intzaViewOddLot.CanGetMouseMove = false;
			columnItem14.Alignment = StringAlignment.Center;
			columnItem14.BackColor = Color.FromArgb(64, 64, 64);
			columnItem14.ColumnAlignment = StringAlignment.Center;
			columnItem14.FontColor = Color.LightGray;
			columnItem14.MyStyle = FontStyle.Regular;
			columnItem14.Name = "bid_vol";
			columnItem14.Text = "Volume";
			columnItem14.ValueFormat = FormatType.Volume;
			columnItem14.Visible = true;
			columnItem14.Width = 30;
			columnItem15.Alignment = StringAlignment.Center;
			columnItem15.BackColor = Color.FromArgb(64, 64, 64);
			columnItem15.ColumnAlignment = StringAlignment.Center;
			columnItem15.FontColor = Color.LightGray;
			columnItem15.MyStyle = FontStyle.Regular;
			columnItem15.Name = "bid";
			columnItem15.Text = "Bid";
			columnItem15.ValueFormat = FormatType.Price;
			columnItem15.Visible = true;
			columnItem15.Width = 20;
			columnItem16.Alignment = StringAlignment.Center;
			columnItem16.BackColor = Color.FromArgb(64, 64, 64);
			columnItem16.ColumnAlignment = StringAlignment.Center;
			columnItem16.FontColor = Color.LightGray;
			columnItem16.MyStyle = FontStyle.Regular;
			columnItem16.Name = "offer";
			columnItem16.Text = "Offer";
			columnItem16.ValueFormat = FormatType.Price;
			columnItem16.Visible = true;
			columnItem16.Width = 20;
			columnItem17.Alignment = StringAlignment.Center;
			columnItem17.BackColor = Color.FromArgb(64, 64, 64);
			columnItem17.ColumnAlignment = StringAlignment.Center;
			columnItem17.FontColor = Color.LightGray;
			columnItem17.MyStyle = FontStyle.Regular;
			columnItem17.Name = "offer_vol";
			columnItem17.Text = "Volume";
			columnItem17.ValueFormat = FormatType.Volume;
			columnItem17.Visible = true;
			columnItem17.Width = 30;
			this.intzaViewOddLot.Columns.Add(columnItem14);
			this.intzaViewOddLot.Columns.Add(columnItem15);
			this.intzaViewOddLot.Columns.Add(columnItem16);
			this.intzaViewOddLot.Columns.Add(columnItem17);
			this.intzaViewOddLot.CurrentScroll = 0;
			this.intzaViewOddLot.FocusItemIndex = -1;
			this.intzaViewOddLot.ForeColor = Color.Black;
			this.intzaViewOddLot.GridColor = Color.FromArgb(45, 45, 45);
			this.intzaViewOddLot.HeaderPctHeight = 100f;
			this.intzaViewOddLot.IsAutoRepaint = true;
			this.intzaViewOddLot.IsDrawFullRow = false;
			this.intzaViewOddLot.IsDrawGrid = true;
			this.intzaViewOddLot.IsDrawHeader = true;
			this.intzaViewOddLot.IsScrollable = true;
			this.intzaViewOddLot.Location = new Point(9, 336);
			this.intzaViewOddLot.MainColumn = "";
			this.intzaViewOddLot.Name = "intzaViewOddLot";
			this.intzaViewOddLot.Rows = 5;
			this.intzaViewOddLot.RowSelectColor = Color.FromArgb(95, 158, 160);
			this.intzaViewOddLot.RowSelectType = 0;
			this.intzaViewOddLot.RowsVisible = 5;
			this.intzaViewOddLot.ScrollChennelColor = Color.Gray;
			this.intzaViewOddLot.Size = new Size(543, 73);
			this.intzaViewOddLot.SortColumnName = "";
			this.intzaViewOddLot.SortType = SortType.Desc;
			this.intzaViewOddLot.TabIndex = 26;
			this.intzaStockInPlay.AllowDrop = true;
			this.intzaStockInPlay.BackColor = Color.FromArgb(10, 10, 10);
			this.intzaStockInPlay.CanBlink = true;
			this.intzaStockInPlay.CanDrag = false;
			this.intzaStockInPlay.CanGetMouseMove = false;
			columnItem18.Alignment = StringAlignment.Center;
			columnItem18.BackColor = Color.FromArgb(64, 64, 64);
			columnItem18.ColumnAlignment = StringAlignment.Center;
			columnItem18.FontColor = Color.LightGray;
			columnItem18.MyStyle = FontStyle.Regular;
			columnItem18.Name = "buy_deal";
			columnItem18.Text = "Deals";
			columnItem18.ValueFormat = FormatType.Volume;
			columnItem18.Visible = true;
			columnItem18.Width = 10;
			columnItem19.Alignment = StringAlignment.Far;
			columnItem19.BackColor = Color.FromArgb(64, 64, 64);
			columnItem19.ColumnAlignment = StringAlignment.Center;
			columnItem19.FontColor = Color.LightGray;
			columnItem19.MyStyle = FontStyle.Regular;
			columnItem19.Name = "buy_volume";
			columnItem19.Text = "Buy Volume";
			columnItem19.ValueFormat = FormatType.Volume;
			columnItem19.Visible = true;
			columnItem19.Width = 17;
			columnItem20.Alignment = StringAlignment.Far;
			columnItem20.BackColor = Color.FromArgb(64, 64, 64);
			columnItem20.ColumnAlignment = StringAlignment.Center;
			columnItem20.FontColor = Color.LightGray;
			columnItem20.MyStyle = FontStyle.Regular;
			columnItem20.Name = "bid";
			columnItem20.Text = "Bid Volume";
			columnItem20.ValueFormat = FormatType.BidOfferVolume;
			columnItem20.Visible = true;
			columnItem20.Width = 17;
			columnItem21.Alignment = StringAlignment.Center;
			columnItem21.BackColor = Color.FromArgb(64, 64, 64);
			columnItem21.ColumnAlignment = StringAlignment.Center;
			columnItem21.FontColor = Color.LightGray;
			columnItem21.MyStyle = FontStyle.Regular;
			columnItem21.Name = "price";
			columnItem21.Text = "Price";
			columnItem21.ValueFormat = FormatType.Text;
			columnItem21.Visible = true;
			columnItem21.Width = 12;
			columnItem22.Alignment = StringAlignment.Far;
			columnItem22.BackColor = Color.FromArgb(64, 64, 64);
			columnItem22.ColumnAlignment = StringAlignment.Center;
			columnItem22.FontColor = Color.LightGray;
			columnItem22.MyStyle = FontStyle.Regular;
			columnItem22.Name = "offer";
			columnItem22.Text = "Offer Volume";
			columnItem22.ValueFormat = FormatType.BidOfferVolume;
			columnItem22.Visible = true;
			columnItem22.Width = 17;
			columnItem23.Alignment = StringAlignment.Far;
			columnItem23.BackColor = Color.FromArgb(64, 64, 64);
			columnItem23.ColumnAlignment = StringAlignment.Center;
			columnItem23.FontColor = Color.LightGray;
			columnItem23.MyStyle = FontStyle.Regular;
			columnItem23.Name = "sell_vol";
			columnItem23.Text = "Sell Volume";
			columnItem23.ValueFormat = FormatType.Volume;
			columnItem23.Visible = true;
			columnItem23.Width = 17;
			columnItem24.Alignment = StringAlignment.Center;
			columnItem24.BackColor = Color.FromArgb(64, 64, 64);
			columnItem24.ColumnAlignment = StringAlignment.Center;
			columnItem24.FontColor = Color.LightGray;
			columnItem24.MyStyle = FontStyle.Regular;
			columnItem24.Name = "sell_deal";
			columnItem24.Text = "Deals";
			columnItem24.ValueFormat = FormatType.Volume;
			columnItem24.Visible = true;
			columnItem24.Width = 10;
			this.intzaStockInPlay.Columns.Add(columnItem18);
			this.intzaStockInPlay.Columns.Add(columnItem19);
			this.intzaStockInPlay.Columns.Add(columnItem20);
			this.intzaStockInPlay.Columns.Add(columnItem21);
			this.intzaStockInPlay.Columns.Add(columnItem22);
			this.intzaStockInPlay.Columns.Add(columnItem23);
			this.intzaStockInPlay.Columns.Add(columnItem24);
			this.intzaStockInPlay.CurrentScroll = 0;
			this.intzaStockInPlay.FocusItemIndex = -1;
			this.intzaStockInPlay.ForeColor = Color.Black;
			this.intzaStockInPlay.GridColor = Color.FromArgb(45, 45, 45);
			this.intzaStockInPlay.HeaderPctHeight = 80f;
			this.intzaStockInPlay.IsAutoRepaint = true;
			this.intzaStockInPlay.IsDrawFullRow = false;
			this.intzaStockInPlay.IsDrawGrid = true;
			this.intzaStockInPlay.IsDrawHeader = true;
			this.intzaStockInPlay.IsScrollable = false;
			this.intzaStockInPlay.Location = new Point(12, 252);
			this.intzaStockInPlay.MainColumn = "";
			this.intzaStockInPlay.Name = "intzaStockInPlay";
			this.intzaStockInPlay.Rows = 0;
			this.intzaStockInPlay.RowSelectColor = Color.FromArgb(95, 158, 160);
			this.intzaStockInPlay.RowSelectType = 0;
			this.intzaStockInPlay.RowsVisible = 0;
			this.intzaStockInPlay.ScrollChennelColor = Color.LightGray;
			this.intzaStockInPlay.Size = new Size(540, 53);
			this.intzaStockInPlay.SortColumnName = "";
			this.intzaStockInPlay.SortType = SortType.Desc;
			this.intzaStockInPlay.TabIndex = 25;
			this.intzaStockInPlay.TableMouseClick += new SortGrid.TableMouseClickEventHandler(this.intzaStockInPlay_TableMouseClick);
			this.intzaLS.AllowDrop = true;
			this.intzaLS.BackColor = Color.FromArgb(10, 10, 10);
			this.intzaLS.CanBlink = false;
			this.intzaLS.CanDrag = false;
			this.intzaLS.CanGetMouseMove = false;
			columnItem25.Alignment = StringAlignment.Center;
			columnItem25.BackColor = Color.FromArgb(64, 64, 64);
			columnItem25.ColumnAlignment = StringAlignment.Center;
			columnItem25.FontColor = Color.LightGray;
			columnItem25.MyStyle = FontStyle.Regular;
			columnItem25.Name = "side";
			columnItem25.Text = "B/S";
			columnItem25.ValueFormat = FormatType.Text;
			columnItem25.Visible = true;
			columnItem25.Width = 14;
			columnItem26.Alignment = StringAlignment.Far;
			columnItem26.BackColor = Color.FromArgb(64, 64, 64);
			columnItem26.ColumnAlignment = StringAlignment.Center;
			columnItem26.FontColor = Color.LightGray;
			columnItem26.MyStyle = FontStyle.Regular;
			columnItem26.Name = "volume";
			columnItem26.Text = "Volume";
			columnItem26.ValueFormat = FormatType.Volume;
			columnItem26.Visible = true;
			columnItem26.Width = 36;
			columnItem27.Alignment = StringAlignment.Far;
			columnItem27.BackColor = Color.FromArgb(64, 64, 64);
			columnItem27.ColumnAlignment = StringAlignment.Center;
			columnItem27.FontColor = Color.LightGray;
			columnItem27.MyStyle = FontStyle.Regular;
			columnItem27.Name = "price";
			columnItem27.Text = "Price";
			columnItem27.ValueFormat = FormatType.Price;
			columnItem27.Visible = true;
			columnItem27.Width = 22;
			columnItem28.Alignment = StringAlignment.Far;
			columnItem28.BackColor = Color.FromArgb(64, 64, 64);
			columnItem28.ColumnAlignment = StringAlignment.Center;
			columnItem28.FontColor = Color.LightGray;
			columnItem28.MyStyle = FontStyle.Regular;
			columnItem28.Name = "time";
			columnItem28.Text = "Time";
			columnItem28.ValueFormat = FormatType.Text;
			columnItem28.Visible = true;
			columnItem28.Width = 28;
			this.intzaLS.Columns.Add(columnItem25);
			this.intzaLS.Columns.Add(columnItem26);
			this.intzaLS.Columns.Add(columnItem27);
			this.intzaLS.Columns.Add(columnItem28);
			this.intzaLS.CurrentScroll = 0;
			this.intzaLS.FocusItemIndex = -1;
			this.intzaLS.ForeColor = Color.Black;
			this.intzaLS.GridColor = Color.FromArgb(45, 45, 45);
			this.intzaLS.HeaderPctHeight = 80f;
			this.intzaLS.IsAutoRepaint = true;
			this.intzaLS.IsDrawFullRow = true;
			this.intzaLS.IsDrawGrid = false;
			this.intzaLS.IsDrawHeader = true;
			this.intzaLS.IsScrollable = false;
			this.intzaLS.Location = new Point(587, 32);
			this.intzaLS.MainColumn = "";
			this.intzaLS.Name = "intzaLS";
			this.intzaLS.Rows = 8;
			this.intzaLS.RowSelectColor = Color.FromArgb(95, 158, 160);
			this.intzaLS.RowSelectType = 0;
			this.intzaLS.RowsVisible = 8;
			this.intzaLS.ScrollChennelColor = Color.LightGray;
			this.intzaLS.Size = new Size(286, 109);
			this.intzaLS.SortColumnName = "";
			this.intzaLS.SortType = SortType.Desc;
			this.intzaLS.TabIndex = 24;
			this.intzaLS.TableMouseClick += new SortGrid.TableMouseClickEventHandler(this.intzaStockInPlayLS_TableMouseClick);
			this.intzaViewOddLotInfo.AllowDrop = true;
			this.intzaViewOddLotInfo.BackColor = Color.Black;
			this.intzaViewOddLotInfo.CanDrag = false;
			this.intzaViewOddLotInfo.IsAutoRepaint = true;
			this.intzaViewOddLotInfo.IsDroped = false;
			itemGrid.AdjustFontSize = 0f;
			itemGrid.Alignment = StringAlignment.Near;
			itemGrid.BackColor = Color.Black;
			itemGrid.Changed = false;
			itemGrid.FieldType = ItemType.Label2;
			itemGrid.FontColor = Color.WhiteSmoke;
			itemGrid.FontStyle = FontStyle.Regular;
			itemGrid.Height = 1;
			itemGrid.IsBlink = 0;
			itemGrid.Name = "lboddavg";
			itemGrid.Text = "Oddlot Avg";
			itemGrid.ValueFormat = FormatType.Text;
			itemGrid.Visible = true;
			itemGrid.Width = 25;
			itemGrid.X = 0;
			itemGrid.Y = 0;
			itemGrid2.AdjustFontSize = 0f;
			itemGrid2.Alignment = StringAlignment.Near;
			itemGrid2.BackColor = Color.Black;
			itemGrid2.Changed = false;
			itemGrid2.FieldType = ItemType.Text;
			itemGrid2.FontColor = Color.White;
			itemGrid2.FontStyle = FontStyle.Regular;
			itemGrid2.Height = 1;
			itemGrid2.IsBlink = 0;
			itemGrid2.Name = "oddavg";
			itemGrid2.Text = "";
			itemGrid2.ValueFormat = FormatType.Text;
			itemGrid2.Visible = true;
			itemGrid2.Width = 25;
			itemGrid2.X = 25;
			itemGrid2.Y = 0;
			itemGrid3.AdjustFontSize = 0f;
			itemGrid3.Alignment = StringAlignment.Near;
			itemGrid3.BackColor = Color.Black;
			itemGrid3.Changed = false;
			itemGrid3.FieldType = ItemType.Label2;
			itemGrid3.FontColor = Color.WhiteSmoke;
			itemGrid3.FontStyle = FontStyle.Regular;
			itemGrid3.Height = 1;
			itemGrid3.IsBlink = 0;
			itemGrid3.Name = "lbodddeal";
			itemGrid3.Text = "Oddlot Deal";
			itemGrid3.ValueFormat = FormatType.Text;
			itemGrid3.Visible = true;
			itemGrid3.Width = 25;
			itemGrid3.X = 0;
			itemGrid3.Y = 1;
			itemGrid4.AdjustFontSize = 0f;
			itemGrid4.Alignment = StringAlignment.Near;
			itemGrid4.BackColor = Color.Black;
			itemGrid4.Changed = false;
			itemGrid4.FieldType = ItemType.Text;
			itemGrid4.FontColor = Color.Yellow;
			itemGrid4.FontStyle = FontStyle.Regular;
			itemGrid4.Height = 1;
			itemGrid4.IsBlink = 0;
			itemGrid4.Name = "odddeal";
			itemGrid4.Text = "";
			itemGrid4.ValueFormat = FormatType.Text;
			itemGrid4.Visible = true;
			itemGrid4.Width = 25;
			itemGrid4.X = 25;
			itemGrid4.Y = 1;
			itemGrid5.AdjustFontSize = 0f;
			itemGrid5.Alignment = StringAlignment.Near;
			itemGrid5.BackColor = Color.Black;
			itemGrid5.Changed = false;
			itemGrid5.FieldType = ItemType.Label2;
			itemGrid5.FontColor = Color.WhiteSmoke;
			itemGrid5.FontStyle = FontStyle.Regular;
			itemGrid5.Height = 1;
			itemGrid5.IsBlink = 0;
			itemGrid5.Name = "lboddvol";
			itemGrid5.Text = "Oddlot Volume";
			itemGrid5.ValueFormat = FormatType.Text;
			itemGrid5.Visible = true;
			itemGrid5.Width = 25;
			itemGrid5.X = 0;
			itemGrid5.Y = 2;
			itemGrid6.AdjustFontSize = 0f;
			itemGrid6.Alignment = StringAlignment.Near;
			itemGrid6.BackColor = Color.Black;
			itemGrid6.Changed = false;
			itemGrid6.FieldType = ItemType.Text;
			itemGrid6.FontColor = Color.Yellow;
			itemGrid6.FontStyle = FontStyle.Regular;
			itemGrid6.Height = 1;
			itemGrid6.IsBlink = 0;
			itemGrid6.Name = "oddvol";
			itemGrid6.Text = "";
			itemGrid6.ValueFormat = FormatType.Text;
			itemGrid6.Visible = true;
			itemGrid6.Width = 25;
			itemGrid6.X = 25;
			itemGrid6.Y = 2;
			itemGrid7.AdjustFontSize = 0f;
			itemGrid7.Alignment = StringAlignment.Near;
			itemGrid7.BackColor = Color.Black;
			itemGrid7.Changed = false;
			itemGrid7.FieldType = ItemType.Label2;
			itemGrid7.FontColor = Color.WhiteSmoke;
			itemGrid7.FontStyle = FontStyle.Regular;
			itemGrid7.Height = 1;
			itemGrid7.IsBlink = 0;
			itemGrid7.Name = "lboddvalue";
			itemGrid7.Text = "Oddlot Value";
			itemGrid7.ValueFormat = FormatType.Text;
			itemGrid7.Visible = true;
			itemGrid7.Width = 25;
			itemGrid7.X = 50;
			itemGrid7.Y = 2;
			itemGrid8.AdjustFontSize = 0f;
			itemGrid8.Alignment = StringAlignment.Near;
			itemGrid8.BackColor = Color.Black;
			itemGrid8.Changed = false;
			itemGrid8.FieldType = ItemType.Text;
			itemGrid8.FontColor = Color.Yellow;
			itemGrid8.FontStyle = FontStyle.Regular;
			itemGrid8.Height = 1;
			itemGrid8.IsBlink = 0;
			itemGrid8.Name = "oddval";
			itemGrid8.Text = "";
			itemGrid8.ValueFormat = FormatType.Text;
			itemGrid8.Visible = true;
			itemGrid8.Width = 25;
			itemGrid8.X = 75;
			itemGrid8.Y = 2;
			itemGrid9.AdjustFontSize = 0f;
			itemGrid9.Alignment = StringAlignment.Near;
			itemGrid9.BackColor = Color.Black;
			itemGrid9.Changed = false;
			itemGrid9.FieldType = ItemType.Label2;
			itemGrid9.FontColor = Color.WhiteSmoke;
			itemGrid9.FontStyle = FontStyle.Regular;
			itemGrid9.Height = 1;
			itemGrid9.IsBlink = 0;
			itemGrid9.Name = "lbceiling";
			itemGrid9.Text = "Ceiling";
			itemGrid9.ValueFormat = FormatType.Text;
			itemGrid9.Visible = true;
			itemGrid9.Width = 25;
			itemGrid9.X = 0;
			itemGrid9.Y = 3;
			itemGrid10.AdjustFontSize = 0f;
			itemGrid10.Alignment = StringAlignment.Near;
			itemGrid10.BackColor = Color.Black;
			itemGrid10.Changed = false;
			itemGrid10.FieldType = ItemType.Text;
			itemGrid10.FontColor = Color.Cyan;
			itemGrid10.FontStyle = FontStyle.Regular;
			itemGrid10.Height = 1;
			itemGrid10.IsBlink = 0;
			itemGrid10.Name = "ceiling";
			itemGrid10.Text = "";
			itemGrid10.ValueFormat = FormatType.Price;
			itemGrid10.Visible = true;
			itemGrid10.Width = 25;
			itemGrid10.X = 25;
			itemGrid10.Y = 3;
			itemGrid11.AdjustFontSize = 0f;
			itemGrid11.Alignment = StringAlignment.Near;
			itemGrid11.BackColor = Color.Black;
			itemGrid11.Changed = false;
			itemGrid11.FieldType = ItemType.Label2;
			itemGrid11.FontColor = Color.WhiteSmoke;
			itemGrid11.FontStyle = FontStyle.Regular;
			itemGrid11.Height = 1;
			itemGrid11.IsBlink = 0;
			itemGrid11.Name = "lbfloor";
			itemGrid11.Text = "Floor";
			itemGrid11.ValueFormat = FormatType.Text;
			itemGrid11.Visible = true;
			itemGrid11.Width = 25;
			itemGrid11.X = 50;
			itemGrid11.Y = 3;
			itemGrid12.AdjustFontSize = 0f;
			itemGrid12.Alignment = StringAlignment.Near;
			itemGrid12.BackColor = Color.Black;
			itemGrid12.Changed = false;
			itemGrid12.FieldType = ItemType.Text;
			itemGrid12.FontColor = Color.Magenta;
			itemGrid12.FontStyle = FontStyle.Regular;
			itemGrid12.Height = 1;
			itemGrid12.IsBlink = 0;
			itemGrid12.Name = "floor";
			itemGrid12.Text = "";
			itemGrid12.ValueFormat = FormatType.Price;
			itemGrid12.Visible = true;
			itemGrid12.Width = 25;
			itemGrid12.X = 75;
			itemGrid12.Y = 3;
			itemGrid13.AdjustFontSize = 0f;
			itemGrid13.Alignment = StringAlignment.Near;
			itemGrid13.BackColor = Color.Black;
			itemGrid13.Changed = false;
			itemGrid13.FieldType = ItemType.Label2;
			itemGrid13.FontColor = Color.WhiteSmoke;
			itemGrid13.FontStyle = FontStyle.Regular;
			itemGrid13.Height = 1;
			itemGrid13.IsBlink = 0;
			itemGrid13.Name = "lbTotOddMktVol";
			itemGrid13.Text = "Total Odd Mkt Volume";
			itemGrid13.ValueFormat = FormatType.Text;
			itemGrid13.Visible = true;
			itemGrid13.Width = 30;
			itemGrid13.X = 50;
			itemGrid13.Y = 0;
			itemGrid14.AdjustFontSize = 0f;
			itemGrid14.Alignment = StringAlignment.Near;
			itemGrid14.BackColor = Color.Black;
			itemGrid14.Changed = false;
			itemGrid14.FieldType = ItemType.Text;
			itemGrid14.FontColor = Color.Yellow;
			itemGrid14.FontStyle = FontStyle.Regular;
			itemGrid14.Height = 1;
			itemGrid14.IsBlink = 0;
			itemGrid14.Name = "totvolume";
			itemGrid14.Text = "";
			itemGrid14.ValueFormat = FormatType.Volume;
			itemGrid14.Visible = true;
			itemGrid14.Width = 20;
			itemGrid14.X = 80;
			itemGrid14.Y = 0;
			itemGrid15.AdjustFontSize = 0f;
			itemGrid15.Alignment = StringAlignment.Near;
			itemGrid15.BackColor = Color.Black;
			itemGrid15.Changed = false;
			itemGrid15.FieldType = ItemType.Label2;
			itemGrid15.FontColor = Color.WhiteSmoke;
			itemGrid15.FontStyle = FontStyle.Regular;
			itemGrid15.Height = 1;
			itemGrid15.IsBlink = 0;
			itemGrid15.Name = "lbTotOddMktVal";
			itemGrid15.Text = "Total Odd Mkt Value";
			itemGrid15.ValueFormat = FormatType.Text;
			itemGrid15.Visible = true;
			itemGrid15.Width = 30;
			itemGrid15.X = 50;
			itemGrid15.Y = 1;
			itemGrid16.AdjustFontSize = 0f;
			itemGrid16.Alignment = StringAlignment.Near;
			itemGrid16.BackColor = Color.Black;
			itemGrid16.Changed = false;
			itemGrid16.FieldType = ItemType.Text;
			itemGrid16.FontColor = Color.Yellow;
			itemGrid16.FontStyle = FontStyle.Regular;
			itemGrid16.Height = 1;
			itemGrid16.IsBlink = 0;
			itemGrid16.Name = "totvalue";
			itemGrid16.Text = "";
			itemGrid16.ValueFormat = FormatType.Volume;
			itemGrid16.Visible = true;
			itemGrid16.Width = 20;
			itemGrid16.X = 80;
			itemGrid16.Y = 1;
			itemGrid17.AdjustFontSize = 0f;
			itemGrid17.Alignment = StringAlignment.Near;
			itemGrid17.BackColor = Color.Black;
			itemGrid17.Changed = false;
			itemGrid17.FieldType = ItemType.Label2;
			itemGrid17.FontColor = Color.WhiteSmoke;
			itemGrid17.FontStyle = FontStyle.Regular;
			itemGrid17.Height = 1;
			itemGrid17.IsBlink = 0;
			itemGrid17.Name = "lblast";
			itemGrid17.Text = "Last";
			itemGrid17.ValueFormat = FormatType.Text;
			itemGrid17.Visible = true;
			itemGrid17.Width = 25;
			itemGrid17.X = 0;
			itemGrid17.Y = 4;
			itemGrid18.AdjustFontSize = 0f;
			itemGrid18.Alignment = StringAlignment.Near;
			itemGrid18.BackColor = Color.Black;
			itemGrid18.Changed = false;
			itemGrid18.FieldType = ItemType.Text;
			itemGrid18.FontColor = Color.White;
			itemGrid18.FontStyle = FontStyle.Regular;
			itemGrid18.Height = 1;
			itemGrid18.IsBlink = 0;
			itemGrid18.Name = "oddlast";
			itemGrid18.Text = "";
			itemGrid18.ValueFormat = FormatType.Text;
			itemGrid18.Visible = true;
			itemGrid18.Width = 25;
			itemGrid18.X = 25;
			itemGrid18.Y = 4;
			itemGrid19.AdjustFontSize = 0f;
			itemGrid19.Alignment = StringAlignment.Near;
			itemGrid19.BackColor = Color.Black;
			itemGrid19.Changed = false;
			itemGrid19.FieldType = ItemType.Label2;
			itemGrid19.FontColor = Color.White;
			itemGrid19.FontStyle = FontStyle.Regular;
			itemGrid19.Height = 1;
			itemGrid19.IsBlink = 0;
			itemGrid19.Name = "lbprior";
			itemGrid19.Text = "Prior";
			itemGrid19.ValueFormat = FormatType.Text;
			itemGrid19.Visible = true;
			itemGrid19.Width = 25;
			itemGrid19.X = 50;
			itemGrid19.Y = 4;
			itemGrid20.AdjustFontSize = 0f;
			itemGrid20.Alignment = StringAlignment.Near;
			itemGrid20.BackColor = Color.Black;
			itemGrid20.Changed = false;
			itemGrid20.FieldType = ItemType.Text;
			itemGrid20.FontColor = Color.Yellow;
			itemGrid20.FontStyle = FontStyle.Regular;
			itemGrid20.Height = 1;
			itemGrid20.IsBlink = 0;
			itemGrid20.Name = "prior";
			itemGrid20.Text = "";
			itemGrid20.ValueFormat = FormatType.Price;
			itemGrid20.Visible = true;
			itemGrid20.Width = 25;
			itemGrid20.X = 75;
			itemGrid20.Y = 4;
			this.intzaViewOddLotInfo.Items.Add(itemGrid);
			this.intzaViewOddLotInfo.Items.Add(itemGrid2);
			this.intzaViewOddLotInfo.Items.Add(itemGrid3);
			this.intzaViewOddLotInfo.Items.Add(itemGrid4);
			this.intzaViewOddLotInfo.Items.Add(itemGrid5);
			this.intzaViewOddLotInfo.Items.Add(itemGrid6);
			this.intzaViewOddLotInfo.Items.Add(itemGrid7);
			this.intzaViewOddLotInfo.Items.Add(itemGrid8);
			this.intzaViewOddLotInfo.Items.Add(itemGrid9);
			this.intzaViewOddLotInfo.Items.Add(itemGrid10);
			this.intzaViewOddLotInfo.Items.Add(itemGrid11);
			this.intzaViewOddLotInfo.Items.Add(itemGrid12);
			this.intzaViewOddLotInfo.Items.Add(itemGrid13);
			this.intzaViewOddLotInfo.Items.Add(itemGrid14);
			this.intzaViewOddLotInfo.Items.Add(itemGrid15);
			this.intzaViewOddLotInfo.Items.Add(itemGrid16);
			this.intzaViewOddLotInfo.Items.Add(itemGrid17);
			this.intzaViewOddLotInfo.Items.Add(itemGrid18);
			this.intzaViewOddLotInfo.Items.Add(itemGrid19);
			this.intzaViewOddLotInfo.Items.Add(itemGrid20);
			this.intzaViewOddLotInfo.LineColor = Color.Red;
			this.intzaViewOddLotInfo.Location = new Point(12, 31);
			this.intzaViewOddLotInfo.Margin = new Padding(0);
			this.intzaViewOddLotInfo.Name = "intzaViewOddLotInfo";
			this.intzaViewOddLotInfo.Size = new Size(572, 87);
			this.intzaViewOddLotInfo.TabIndex = 10;
			this.intzaInfo.AllowDrop = true;
			this.intzaInfo.BackColor = Color.FromArgb(10, 10, 10);
			this.intzaInfo.CanDrag = false;
			this.intzaInfo.IsAutoRepaint = true;
			this.intzaInfo.IsDroped = false;
			itemGrid21.AdjustFontSize = 0f;
			itemGrid21.Alignment = StringAlignment.Near;
			itemGrid21.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid21.Changed = false;
			itemGrid21.FieldType = ItemType.Label2;
			itemGrid21.FontColor = Color.LightGray;
			itemGrid21.FontStyle = FontStyle.Regular;
			itemGrid21.Height = 1;
			itemGrid21.IsBlink = 0;
			itemGrid21.Name = "lbOpenVolume";
			itemGrid21.Text = "Opn Vol";
			itemGrid21.ValueFormat = FormatType.Text;
			itemGrid21.Visible = true;
			itemGrid21.Width = 22;
			itemGrid21.X = 0;
			itemGrid21.Y = 9;
			itemGrid22.AdjustFontSize = 0f;
			itemGrid22.Alignment = StringAlignment.Far;
			itemGrid22.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid22.Changed = false;
			itemGrid22.FieldType = ItemType.Text;
			itemGrid22.FontColor = Color.Yellow;
			itemGrid22.FontStyle = FontStyle.Regular;
			itemGrid22.Height = 1;
			itemGrid22.IsBlink = 0;
			itemGrid22.Name = "tbOpenVolume";
			itemGrid22.Text = "";
			itemGrid22.ValueFormat = FormatType.Volume;
			itemGrid22.Visible = true;
			itemGrid22.Width = 31;
			itemGrid22.X = 22;
			itemGrid22.Y = 9;
			itemGrid23.AdjustFontSize = -1f;
			itemGrid23.Alignment = StringAlignment.Far;
			itemGrid23.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid23.Changed = false;
			itemGrid23.FieldType = ItemType.Text;
			itemGrid23.FontColor = Color.Yellow;
			itemGrid23.FontStyle = FontStyle.Regular;
			itemGrid23.Height = 1;
			itemGrid23.IsBlink = 0;
			itemGrid23.Name = "tbOpenVolPct";
			itemGrid23.Text = "";
			itemGrid23.ValueFormat = FormatType.Text;
			itemGrid23.Visible = true;
			itemGrid23.Width = 20;
			itemGrid23.X = 55;
			itemGrid23.Y = 9;
			itemGrid24.AdjustFontSize = 0f;
			itemGrid24.Alignment = StringAlignment.Near;
			itemGrid24.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid24.Changed = false;
			itemGrid24.FieldType = ItemType.Label2;
			itemGrid24.FontColor = Color.LightGray;
			itemGrid24.FontStyle = FontStyle.Regular;
			itemGrid24.Height = 1;
			itemGrid24.IsBlink = 0;
			itemGrid24.Name = "lbBuyVolume";
			itemGrid24.Text = "Buy Vol";
			itemGrid24.ValueFormat = FormatType.Text;
			itemGrid24.Visible = true;
			itemGrid24.Width = 22;
			itemGrid24.X = 0;
			itemGrid24.Y = 10;
			itemGrid25.AdjustFontSize = 0f;
			itemGrid25.Alignment = StringAlignment.Far;
			itemGrid25.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid25.Changed = false;
			itemGrid25.FieldType = ItemType.Text;
			itemGrid25.FontColor = Color.Lime;
			itemGrid25.FontStyle = FontStyle.Regular;
			itemGrid25.Height = 1;
			itemGrid25.IsBlink = 0;
			itemGrid25.Name = "tbBuyVolume";
			itemGrid25.Text = "";
			itemGrid25.ValueFormat = FormatType.Volume;
			itemGrid25.Visible = true;
			itemGrid25.Width = 31;
			itemGrid25.X = 22;
			itemGrid25.Y = 10;
			itemGrid26.AdjustFontSize = -1f;
			itemGrid26.Alignment = StringAlignment.Far;
			itemGrid26.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid26.Changed = false;
			itemGrid26.FieldType = ItemType.Text;
			itemGrid26.FontColor = Color.Lime;
			itemGrid26.FontStyle = FontStyle.Regular;
			itemGrid26.Height = 1;
			itemGrid26.IsBlink = 0;
			itemGrid26.Name = "tbBuyVolPct";
			itemGrid26.Text = "";
			itemGrid26.ValueFormat = FormatType.Text;
			itemGrid26.Visible = true;
			itemGrid26.Width = 20;
			itemGrid26.X = 55;
			itemGrid26.Y = 10;
			itemGrid27.AdjustFontSize = 0f;
			itemGrid27.Alignment = StringAlignment.Near;
			itemGrid27.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid27.Changed = false;
			itemGrid27.FieldType = ItemType.Label2;
			itemGrid27.FontColor = Color.LightGray;
			itemGrid27.FontStyle = FontStyle.Regular;
			itemGrid27.Height = 1;
			itemGrid27.IsBlink = 0;
			itemGrid27.Name = "lbSellVolume";
			itemGrid27.Text = "Sell Vol";
			itemGrid27.ValueFormat = FormatType.Text;
			itemGrid27.Visible = true;
			itemGrid27.Width = 22;
			itemGrid27.X = 0;
			itemGrid27.Y = 11;
			itemGrid28.AdjustFontSize = 0f;
			itemGrid28.Alignment = StringAlignment.Far;
			itemGrid28.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid28.Changed = false;
			itemGrid28.FieldType = ItemType.Text;
			itemGrid28.FontColor = Color.Red;
			itemGrid28.FontStyle = FontStyle.Regular;
			itemGrid28.Height = 1;
			itemGrid28.IsBlink = 0;
			itemGrid28.Name = "tbSellVolume";
			itemGrid28.Text = "";
			itemGrid28.ValueFormat = FormatType.Volume;
			itemGrid28.Visible = true;
			itemGrid28.Width = 31;
			itemGrid28.X = 22;
			itemGrid28.Y = 11;
			itemGrid29.AdjustFontSize = -1f;
			itemGrid29.Alignment = StringAlignment.Far;
			itemGrid29.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid29.Changed = false;
			itemGrid29.FieldType = ItemType.Text;
			itemGrid29.FontColor = Color.Red;
			itemGrid29.FontStyle = FontStyle.Regular;
			itemGrid29.Height = 1;
			itemGrid29.IsBlink = 0;
			itemGrid29.Name = "tbSellVolPct";
			itemGrid29.Text = "";
			itemGrid29.ValueFormat = FormatType.Text;
			itemGrid29.Visible = true;
			itemGrid29.Width = 20;
			itemGrid29.X = 55;
			itemGrid29.Y = 11;
			itemGrid30.AdjustFontSize = 0f;
			itemGrid30.Alignment = StringAlignment.Near;
			itemGrid30.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid30.Changed = false;
			itemGrid30.FieldType = ItemType.Label2;
			itemGrid30.FontColor = Color.LightGray;
			itemGrid30.FontStyle = FontStyle.Regular;
			itemGrid30.Height = 1;
			itemGrid30.IsBlink = 0;
			itemGrid30.Name = "lbMVolume";
			itemGrid30.Text = "Main Vol";
			itemGrid30.ValueFormat = FormatType.Text;
			itemGrid30.Visible = true;
			itemGrid30.Width = 26;
			itemGrid30.X = 0;
			itemGrid30.Y = 6;
			itemGrid31.AdjustFontSize = 0f;
			itemGrid31.Alignment = StringAlignment.Far;
			itemGrid31.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid31.Changed = false;
			itemGrid31.FieldType = ItemType.Text;
			itemGrid31.FontColor = Color.Yellow;
			itemGrid31.FontStyle = FontStyle.Regular;
			itemGrid31.Height = 1;
			itemGrid31.IsBlink = 0;
			itemGrid31.Name = "tbMVolume";
			itemGrid31.Text = "";
			itemGrid31.ValueFormat = FormatType.Volume;
			itemGrid31.Visible = true;
			itemGrid31.Width = 34;
			itemGrid31.X = 26;
			itemGrid31.Y = 6;
			itemGrid32.AdjustFontSize = 0f;
			itemGrid32.Alignment = StringAlignment.Near;
			itemGrid32.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid32.Changed = false;
			itemGrid32.FieldType = ItemType.Label2;
			itemGrid32.FontColor = Color.LightGray;
			itemGrid32.FontStyle = FontStyle.Regular;
			itemGrid32.Height = 1;
			itemGrid32.IsBlink = 0;
			itemGrid32.Name = "lbMValue";
			itemGrid32.Text = "Main Val";
			itemGrid32.ValueFormat = FormatType.Text;
			itemGrid32.Visible = true;
			itemGrid32.Width = 26;
			itemGrid32.X = 0;
			itemGrid32.Y = 7;
			itemGrid33.AdjustFontSize = 0f;
			itemGrid33.Alignment = StringAlignment.Far;
			itemGrid33.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid33.Changed = false;
			itemGrid33.FieldType = ItemType.Text;
			itemGrid33.FontColor = Color.Yellow;
			itemGrid33.FontStyle = FontStyle.Regular;
			itemGrid33.Height = 1;
			itemGrid33.IsBlink = 0;
			itemGrid33.Name = "tbMValue";
			itemGrid33.Text = "";
			itemGrid33.ValueFormat = FormatType.Text;
			itemGrid33.Visible = true;
			itemGrid33.Width = 34;
			itemGrid33.X = 26;
			itemGrid33.Y = 7;
			itemGrid34.AdjustFontSize = 0f;
			itemGrid34.Alignment = StringAlignment.Near;
			itemGrid34.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid34.Changed = false;
			itemGrid34.FieldType = ItemType.Label2;
			itemGrid34.FontColor = Color.LightGray;
			itemGrid34.FontStyle = FontStyle.Regular;
			itemGrid34.Height = 1;
			itemGrid34.IsBlink = 0;
			itemGrid34.Name = "lbMDeal";
			itemGrid34.Text = "MDeal";
			itemGrid34.ValueFormat = FormatType.Text;
			itemGrid34.Visible = true;
			itemGrid34.Width = 18;
			itemGrid34.X = 61;
			itemGrid34.Y = 7;
			itemGrid35.AdjustFontSize = 0f;
			itemGrid35.Alignment = StringAlignment.Far;
			itemGrid35.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid35.Changed = false;
			itemGrid35.FieldType = ItemType.Text;
			itemGrid35.FontColor = Color.Yellow;
			itemGrid35.FontStyle = FontStyle.Regular;
			itemGrid35.Height = 1;
			itemGrid35.IsBlink = 0;
			itemGrid35.Name = "tbMDeal";
			itemGrid35.Text = "";
			itemGrid35.ValueFormat = FormatType.Price;
			itemGrid35.Visible = true;
			itemGrid35.Width = 20;
			itemGrid35.X = 79;
			itemGrid35.Y = 7;
			itemGrid36.AdjustFontSize = 0f;
			itemGrid36.Alignment = StringAlignment.Near;
			itemGrid36.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid36.Changed = false;
			itemGrid36.FieldType = ItemType.Label2;
			itemGrid36.FontColor = Color.LightGray;
			itemGrid36.FontStyle = FontStyle.Regular;
			itemGrid36.Height = 1;
			itemGrid36.IsBlink = 0;
			itemGrid36.Name = "lbBigValue";
			itemGrid36.Text = "Big Val";
			itemGrid36.ValueFormat = FormatType.Text;
			itemGrid36.Visible = true;
			itemGrid36.Width = 26;
			itemGrid36.X = 0;
			itemGrid36.Y = 8;
			itemGrid37.AdjustFontSize = 0f;
			itemGrid37.Alignment = StringAlignment.Far;
			itemGrid37.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid37.Changed = false;
			itemGrid37.FieldType = ItemType.Text;
			itemGrid37.FontColor = Color.Yellow;
			itemGrid37.FontStyle = FontStyle.Regular;
			itemGrid37.Height = 1;
			itemGrid37.IsBlink = 0;
			itemGrid37.Name = "tbBigValue";
			itemGrid37.Text = "";
			itemGrid37.ValueFormat = FormatType.Price;
			itemGrid37.Visible = true;
			itemGrid37.Width = 34;
			itemGrid37.X = 26;
			itemGrid37.Y = 8;
			itemGrid38.AdjustFontSize = 0f;
			itemGrid38.Alignment = StringAlignment.Near;
			itemGrid38.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid38.Changed = false;
			itemGrid38.FieldType = ItemType.Text;
			itemGrid38.FontColor = Color.White;
			itemGrid38.FontStyle = FontStyle.Regular;
			itemGrid38.Height = 3;
			itemGrid38.IsBlink = 0;
			itemGrid38.Name = "pie";
			itemGrid38.Text = "";
			itemGrid38.ValueFormat = FormatType.PieChart;
			itemGrid38.Visible = true;
			itemGrid38.Width = 24;
			itemGrid38.X = 76;
			itemGrid38.Y = 9;
			itemGrid39.AdjustFontSize = 0f;
			itemGrid39.Alignment = StringAlignment.Near;
			itemGrid39.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid39.Changed = false;
			itemGrid39.FieldType = ItemType.Label2;
			itemGrid39.FontColor = Color.LightGray;
			itemGrid39.FontStyle = FontStyle.Regular;
			itemGrid39.Height = 1;
			itemGrid39.IsBlink = 0;
			itemGrid39.Name = "lbFlag";
			itemGrid39.Text = "Flag";
			itemGrid39.ValueFormat = FormatType.Text;
			itemGrid39.Visible = true;
			itemGrid39.Width = 25;
			itemGrid39.X = 50;
			itemGrid39.Y = 5;
			itemGrid40.AdjustFontSize = 0f;
			itemGrid40.Alignment = StringAlignment.Far;
			itemGrid40.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid40.Changed = false;
			itemGrid40.FieldType = ItemType.Text;
			itemGrid40.FontColor = Color.Yellow;
			itemGrid40.FontStyle = FontStyle.Regular;
			itemGrid40.Height = 1;
			itemGrid40.IsBlink = 0;
			itemGrid40.Name = "tbFlag";
			itemGrid40.Text = "";
			itemGrid40.ValueFormat = FormatType.Text;
			itemGrid40.Visible = true;
			itemGrid40.Width = 25;
			itemGrid40.X = 75;
			itemGrid40.Y = 5;
			itemGrid41.AdjustFontSize = 0f;
			itemGrid41.Alignment = StringAlignment.Near;
			itemGrid41.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid41.Changed = false;
			itemGrid41.FieldType = ItemType.Label2;
			itemGrid41.FontColor = Color.LightGray;
			itemGrid41.FontStyle = FontStyle.Regular;
			itemGrid41.Height = 1;
			itemGrid41.IsBlink = 0;
			itemGrid41.Name = "lbHigh";
			itemGrid41.Text = "High";
			itemGrid41.ValueFormat = FormatType.Text;
			itemGrid41.Visible = true;
			itemGrid41.Width = 22;
			itemGrid41.X = 0;
			itemGrid41.Y = 0;
			itemGrid42.AdjustFontSize = 0f;
			itemGrid42.Alignment = StringAlignment.Far;
			itemGrid42.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid42.Changed = false;
			itemGrid42.FieldType = ItemType.Text;
			itemGrid42.FontColor = Color.White;
			itemGrid42.FontStyle = FontStyle.Regular;
			itemGrid42.Height = 1;
			itemGrid42.IsBlink = 0;
			itemGrid42.Name = "tbHigh";
			itemGrid42.Text = "";
			itemGrid42.ValueFormat = FormatType.Price;
			itemGrid42.Visible = true;
			itemGrid42.Width = 25;
			itemGrid42.X = 22;
			itemGrid42.Y = 0;
			itemGrid43.AdjustFontSize = 0f;
			itemGrid43.Alignment = StringAlignment.Near;
			itemGrid43.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid43.Changed = false;
			itemGrid43.FieldType = ItemType.Label2;
			itemGrid43.FontColor = Color.LightGray;
			itemGrid43.FontStyle = FontStyle.Regular;
			itemGrid43.Height = 1;
			itemGrid43.IsBlink = 0;
			itemGrid43.Name = "lbPrior";
			itemGrid43.Text = "Prior";
			itemGrid43.ValueFormat = FormatType.Text;
			itemGrid43.Visible = true;
			itemGrid43.Width = 22;
			itemGrid43.X = 0;
			itemGrid43.Y = 1;
			itemGrid44.AdjustFontSize = 0f;
			itemGrid44.Alignment = StringAlignment.Far;
			itemGrid44.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid44.Changed = false;
			itemGrid44.FieldType = ItemType.Text;
			itemGrid44.FontColor = Color.Yellow;
			itemGrid44.FontStyle = FontStyle.Regular;
			itemGrid44.Height = 1;
			itemGrid44.IsBlink = 0;
			itemGrid44.Name = "tbPrior";
			itemGrid44.Text = "";
			itemGrid44.ValueFormat = FormatType.Price;
			itemGrid44.Visible = true;
			itemGrid44.Width = 25;
			itemGrid44.X = 22;
			itemGrid44.Y = 1;
			itemGrid45.AdjustFontSize = 0f;
			itemGrid45.Alignment = StringAlignment.Near;
			itemGrid45.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid45.Changed = false;
			itemGrid45.FieldType = ItemType.Label2;
			itemGrid45.FontColor = Color.LightGray;
			itemGrid45.FontStyle = FontStyle.Regular;
			itemGrid45.Height = 1;
			itemGrid45.IsBlink = 0;
			itemGrid45.Name = "lbChange";
			itemGrid45.Text = "Change";
			itemGrid45.ValueFormat = FormatType.Text;
			itemGrid45.Visible = true;
			itemGrid45.Width = 22;
			itemGrid45.X = 0;
			itemGrid45.Y = 2;
			itemGrid46.AdjustFontSize = 0f;
			itemGrid46.Alignment = StringAlignment.Far;
			itemGrid46.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid46.Changed = false;
			itemGrid46.FieldType = ItemType.Text;
			itemGrid46.FontColor = Color.White;
			itemGrid46.FontStyle = FontStyle.Regular;
			itemGrid46.Height = 1;
			itemGrid46.IsBlink = 0;
			itemGrid46.Name = "tbChange";
			itemGrid46.Text = "";
			itemGrid46.ValueFormat = FormatType.Text;
			itemGrid46.Visible = true;
			itemGrid46.Width = 25;
			itemGrid46.X = 22;
			itemGrid46.Y = 2;
			itemGrid47.AdjustFontSize = 0f;
			itemGrid47.Alignment = StringAlignment.Near;
			itemGrid47.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid47.Changed = false;
			itemGrid47.FieldType = ItemType.Label2;
			itemGrid47.FontColor = Color.LightGray;
			itemGrid47.FontStyle = FontStyle.Regular;
			itemGrid47.Height = 1;
			itemGrid47.IsBlink = 0;
			itemGrid47.Name = "lbCeiling";
			itemGrid47.Text = "Ceiling";
			itemGrid47.ValueFormat = FormatType.Text;
			itemGrid47.Visible = true;
			itemGrid47.Width = 22;
			itemGrid47.X = 0;
			itemGrid47.Y = 3;
			itemGrid48.AdjustFontSize = 0f;
			itemGrid48.Alignment = StringAlignment.Far;
			itemGrid48.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid48.Changed = false;
			itemGrid48.FieldType = ItemType.Text;
			itemGrid48.FontColor = Color.Cyan;
			itemGrid48.FontStyle = FontStyle.Regular;
			itemGrid48.Height = 1;
			itemGrid48.IsBlink = 0;
			itemGrid48.Name = "tbCeiling";
			itemGrid48.Text = "";
			itemGrid48.ValueFormat = FormatType.Price;
			itemGrid48.Visible = true;
			itemGrid48.Width = 25;
			itemGrid48.X = 22;
			itemGrid48.Y = 3;
			itemGrid49.AdjustFontSize = 0f;
			itemGrid49.Alignment = StringAlignment.Near;
			itemGrid49.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid49.Changed = false;
			itemGrid49.FieldType = ItemType.Label2;
			itemGrid49.FontColor = Color.LightGray;
			itemGrid49.FontStyle = FontStyle.Regular;
			itemGrid49.Height = 1;
			itemGrid49.IsBlink = 0;
			itemGrid49.Name = "lbOpen1";
			itemGrid49.Text = "Open-1";
			itemGrid49.ValueFormat = FormatType.Text;
			itemGrid49.Visible = true;
			itemGrid49.Width = 22;
			itemGrid49.X = 0;
			itemGrid49.Y = 4;
			itemGrid50.AdjustFontSize = 0f;
			itemGrid50.Alignment = StringAlignment.Far;
			itemGrid50.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid50.Changed = false;
			itemGrid50.FieldType = ItemType.Text;
			itemGrid50.FontColor = Color.White;
			itemGrid50.FontStyle = FontStyle.Regular;
			itemGrid50.Height = 1;
			itemGrid50.IsBlink = 0;
			itemGrid50.Name = "tbOpen1";
			itemGrid50.Text = "";
			itemGrid50.ValueFormat = FormatType.Price;
			itemGrid50.Visible = true;
			itemGrid50.Width = 25;
			itemGrid50.X = 22;
			itemGrid50.Y = 4;
			itemGrid51.AdjustFontSize = 0f;
			itemGrid51.Alignment = StringAlignment.Near;
			itemGrid51.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid51.Changed = false;
			itemGrid51.FieldType = ItemType.Label2;
			itemGrid51.FontColor = Color.LightGray;
			itemGrid51.FontStyle = FontStyle.Regular;
			itemGrid51.Height = 1;
			itemGrid51.IsBlink = 0;
			itemGrid51.Name = "lbLow";
			itemGrid51.Text = "Low";
			itemGrid51.ValueFormat = FormatType.Text;
			itemGrid51.Visible = true;
			itemGrid51.Width = 25;
			itemGrid51.X = 50;
			itemGrid51.Y = 0;
			itemGrid52.AdjustFontSize = 0f;
			itemGrid52.Alignment = StringAlignment.Far;
			itemGrid52.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid52.Changed = false;
			itemGrid52.FieldType = ItemType.Text;
			itemGrid52.FontColor = Color.White;
			itemGrid52.FontStyle = FontStyle.Regular;
			itemGrid52.Height = 1;
			itemGrid52.IsBlink = 0;
			itemGrid52.Name = "tbLow";
			itemGrid52.Text = "";
			itemGrid52.ValueFormat = FormatType.Price;
			itemGrid52.Visible = true;
			itemGrid52.Width = 25;
			itemGrid52.X = 75;
			itemGrid52.Y = 0;
			itemGrid53.AdjustFontSize = 0f;
			itemGrid53.Alignment = StringAlignment.Near;
			itemGrid53.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid53.Changed = false;
			itemGrid53.FieldType = ItemType.Label2;
			itemGrid53.FontColor = Color.LightGray;
			itemGrid53.FontStyle = FontStyle.Regular;
			itemGrid53.Height = 1;
			itemGrid53.IsBlink = 0;
			itemGrid53.Name = "lbAverage";
			itemGrid53.Text = "Average";
			itemGrid53.ValueFormat = FormatType.Text;
			itemGrid53.Visible = true;
			itemGrid53.Width = 25;
			itemGrid53.X = 50;
			itemGrid53.Y = 1;
			itemGrid54.AdjustFontSize = 0f;
			itemGrid54.Alignment = StringAlignment.Far;
			itemGrid54.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid54.Changed = false;
			itemGrid54.FieldType = ItemType.Text;
			itemGrid54.FontColor = Color.White;
			itemGrid54.FontStyle = FontStyle.Regular;
			itemGrid54.Height = 1;
			itemGrid54.IsBlink = 0;
			itemGrid54.Name = "tbAverage";
			itemGrid54.Text = "";
			itemGrid54.ValueFormat = FormatType.Price;
			itemGrid54.Visible = true;
			itemGrid54.Width = 25;
			itemGrid54.X = 75;
			itemGrid54.Y = 1;
			itemGrid55.AdjustFontSize = 0f;
			itemGrid55.Alignment = StringAlignment.Near;
			itemGrid55.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid55.Changed = false;
			itemGrid55.FieldType = ItemType.Label2;
			itemGrid55.FontColor = Color.LightGray;
			itemGrid55.FontStyle = FontStyle.Regular;
			itemGrid55.Height = 1;
			itemGrid55.IsBlink = 0;
			itemGrid55.Name = "lbChangePct";
			itemGrid55.Text = "%Change";
			itemGrid55.ValueFormat = FormatType.Text;
			itemGrid55.Visible = true;
			itemGrid55.Width = 25;
			itemGrid55.X = 50;
			itemGrid55.Y = 2;
			itemGrid56.AdjustFontSize = 0f;
			itemGrid56.Alignment = StringAlignment.Far;
			itemGrid56.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid56.Changed = false;
			itemGrid56.FieldType = ItemType.Text;
			itemGrid56.FontColor = Color.White;
			itemGrid56.FontStyle = FontStyle.Regular;
			itemGrid56.Height = 1;
			itemGrid56.IsBlink = 0;
			itemGrid56.Name = "tbChangePct";
			itemGrid56.Text = "";
			itemGrid56.ValueFormat = FormatType.Text;
			itemGrid56.Visible = true;
			itemGrid56.Width = 25;
			itemGrid56.X = 75;
			itemGrid56.Y = 2;
			itemGrid57.AdjustFontSize = 0f;
			itemGrid57.Alignment = StringAlignment.Near;
			itemGrid57.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid57.Changed = false;
			itemGrid57.FieldType = ItemType.Label2;
			itemGrid57.FontColor = Color.LightGray;
			itemGrid57.FontStyle = FontStyle.Regular;
			itemGrid57.Height = 1;
			itemGrid57.IsBlink = 0;
			itemGrid57.Name = "lbFloor";
			itemGrid57.Text = "Floor";
			itemGrid57.ValueFormat = FormatType.Text;
			itemGrid57.Visible = true;
			itemGrid57.Width = 25;
			itemGrid57.X = 50;
			itemGrid57.Y = 3;
			itemGrid58.AdjustFontSize = 0f;
			itemGrid58.Alignment = StringAlignment.Far;
			itemGrid58.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid58.Changed = false;
			itemGrid58.FieldType = ItemType.Text;
			itemGrid58.FontColor = Color.Magenta;
			itemGrid58.FontStyle = FontStyle.Regular;
			itemGrid58.Height = 1;
			itemGrid58.IsBlink = 0;
			itemGrid58.Name = "tbFloor";
			itemGrid58.Text = "";
			itemGrid58.ValueFormat = FormatType.Price;
			itemGrid58.Visible = true;
			itemGrid58.Width = 25;
			itemGrid58.X = 75;
			itemGrid58.Y = 3;
			itemGrid59.AdjustFontSize = 0f;
			itemGrid59.Alignment = StringAlignment.Near;
			itemGrid59.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid59.Changed = false;
			itemGrid59.FieldType = ItemType.Label2;
			itemGrid59.FontColor = Color.LightGray;
			itemGrid59.FontStyle = FontStyle.Regular;
			itemGrid59.Height = 1;
			itemGrid59.IsBlink = 0;
			itemGrid59.Name = "lbOpen2";
			itemGrid59.Text = "Open-2";
			itemGrid59.ValueFormat = FormatType.Text;
			itemGrid59.Visible = true;
			itemGrid59.Width = 25;
			itemGrid59.X = 50;
			itemGrid59.Y = 4;
			itemGrid60.AdjustFontSize = 0f;
			itemGrid60.Alignment = StringAlignment.Far;
			itemGrid60.BackColor = Color.FromArgb(10, 10, 10);
			itemGrid60.Changed = false;
			itemGrid60.FieldType = ItemType.Text;
			itemGrid60.FontColor = Color.White;
			itemGrid60.FontStyle = FontStyle.Regular;
			itemGrid60.Height = 1;
			itemGrid60.IsBlink = 0;
			itemGrid60.Name = "tbOpen2";
			itemGrid60.Text = "";
			itemGrid60.ValueFormat = FormatType.Price;
			itemGrid60.Visible = true;
			itemGrid60.Width = 25;
			itemGrid60.X = 75;
			itemGrid60.Y = 4;
			itemGrid61.AdjustFontSize = 0f;
			itemGrid61.Alignment = StringAlignment.Near;
			itemGrid61.BackColor = Color.Black;
			itemGrid61.Changed = false;
			itemGrid61.FieldType = ItemType.Label2;
			itemGrid61.FontColor = Color.LightGray;
			itemGrid61.FontStyle = FontStyle.Regular;
			itemGrid61.Height = 1;
			itemGrid61.IsBlink = 0;
			itemGrid61.Name = "lbOpen3";
			itemGrid61.Text = "Open-3";
			itemGrid61.ValueFormat = FormatType.Text;
			itemGrid61.Visible = true;
			itemGrid61.Width = 22;
			itemGrid61.X = 0;
			itemGrid61.Y = 5;
			itemGrid62.AdjustFontSize = 0f;
			itemGrid62.Alignment = StringAlignment.Near;
			itemGrid62.BackColor = Color.Black;
			itemGrid62.Changed = false;
			itemGrid62.FieldType = ItemType.Text;
			itemGrid62.FontColor = Color.Yellow;
			itemGrid62.FontStyle = FontStyle.Regular;
			itemGrid62.Height = 1;
			itemGrid62.IsBlink = 0;
			itemGrid62.Name = "tbOpen3";
			itemGrid62.Text = "";
			itemGrid62.ValueFormat = FormatType.Text;
			itemGrid62.Visible = true;
			itemGrid62.Width = 25;
			itemGrid62.X = 22;
			itemGrid62.Y = 5;
			this.intzaInfo.Items.Add(itemGrid21);
			this.intzaInfo.Items.Add(itemGrid22);
			this.intzaInfo.Items.Add(itemGrid23);
			this.intzaInfo.Items.Add(itemGrid24);
			this.intzaInfo.Items.Add(itemGrid25);
			this.intzaInfo.Items.Add(itemGrid26);
			this.intzaInfo.Items.Add(itemGrid27);
			this.intzaInfo.Items.Add(itemGrid28);
			this.intzaInfo.Items.Add(itemGrid29);
			this.intzaInfo.Items.Add(itemGrid30);
			this.intzaInfo.Items.Add(itemGrid31);
			this.intzaInfo.Items.Add(itemGrid32);
			this.intzaInfo.Items.Add(itemGrid33);
			this.intzaInfo.Items.Add(itemGrid34);
			this.intzaInfo.Items.Add(itemGrid35);
			this.intzaInfo.Items.Add(itemGrid36);
			this.intzaInfo.Items.Add(itemGrid37);
			this.intzaInfo.Items.Add(itemGrid38);
			this.intzaInfo.Items.Add(itemGrid39);
			this.intzaInfo.Items.Add(itemGrid40);
			this.intzaInfo.Items.Add(itemGrid41);
			this.intzaInfo.Items.Add(itemGrid42);
			this.intzaInfo.Items.Add(itemGrid43);
			this.intzaInfo.Items.Add(itemGrid44);
			this.intzaInfo.Items.Add(itemGrid45);
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
			this.intzaInfo.LineColor = Color.Red;
			this.intzaInfo.Location = new Point(587, 144);
			this.intzaInfo.Margin = new Padding(0);
			this.intzaInfo.Name = "intzaInfo";
			this.intzaInfo.Size = new Size(286, 228);
			this.intzaInfo.TabIndex = 20;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.DimGray;
			base.ClientSize = new Size(888, 419);
			base.Controls.Add(this.intzaViewOddLotInfo);
			base.Controls.Add(this.wcGraphVolume);
			base.Controls.Add(this.intzaSaleByTime);
			base.Controls.Add(this.intzaSaleByPrice);
			base.Controls.Add(this.intzaViewOddLot);
			base.Controls.Add(this.intzaStockInPlay);
			base.Controls.Add(this.intzaLS);
			base.Controls.Add(this.tStripMenu);
			base.Controls.Add(this.intzaInfo);
			base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			base.Name = "frmStockSummary";
			this.Text = "Stock Summary";
			base.IDoShownDelay += new ClientBaseForm.OnShownDelayEventHandler(this.frmStockSummary_IDoShownDelay);
			base.IDoLoadData += new ClientBaseForm.OnIDoLoadDataEventHandler(this.frmStockSummary_IDoLoadData);
			base.IDoFontChanged += new ClientBaseForm.OnFontChangedEventHandler(this.frmStockSummary_IDoFontChanged);
			base.IDoCustomSizeChanged += new ClientBaseForm.CustomSizeChangedEventHandler(this.frmStockSummary_IDoCustomSizeChanged);
			base.IDoSymbolLinked += new ClientBaseForm.OnSymbolLinkEventHandler(this.frmStockSummary_IDoSymbolLinked);
			base.IDoMainFormKeyUp += new ClientBaseForm.OnFormKeyUpEventHandler(this.frmStockSummary_IDoMainFormKeyUp);
			base.IDoReActivated += new ClientBaseForm.OnReActiveEventHandler(this.frmStockSummary_IDoReActivated);
			base.Controls.SetChildIndex(this.intzaInfo, 0);
			base.Controls.SetChildIndex(this.tStripMenu, 0);
			base.Controls.SetChildIndex(this.intzaLS, 0);
			base.Controls.SetChildIndex(this.intzaStockInPlay, 0);
			base.Controls.SetChildIndex(this.intzaViewOddLot, 0);
			base.Controls.SetChildIndex(this.intzaSaleByPrice, 0);
			base.Controls.SetChildIndex(this.intzaSaleByTime, 0);
			base.Controls.SetChildIndex(this.wcGraphVolume, 0);
			base.Controls.SetChildIndex(this.intzaViewOddLotInfo, 0);
			this.tStripMenu.ResumeLayout(false);
			this.tStripMenu.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmStockSummary()
		{
			this.InitializeComponent();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmStockSummary(Dictionary<string, object> propertiesValue, string currentPage) : base(propertiesValue)
		{
			this.currentPage = currentPage;
			this.Init();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmStockSummary(Dictionary<string, object> propertiesValue) : base(propertiesValue)
		{
			this.Init();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void Init()
		{
			this.InitializeComponent();
			try
			{
				this.intzaStockInPlay.Hide();
				this.intzaSaleByTime.Hide();
				this.intzaSaleByPrice.Hide();
				this.wcGraphVolume.Hide();
				this.intzaViewOddLot.Hide();
				this.intzaViewOddLotInfo.Hide();
				if (ApplicationInfo.IsSupportTfex)
				{
					this.tscbStock.Items.AddRange(ApplicationInfo.MultiAutoCompStringArr);
				}
				else
				{
					this.tscbStock.Items.AddRange(ApplicationInfo.StockAutoCompStringArr);
				}
				this.tscbStock.Sorted = true;
				this.tscbStock.AutoCompleteMode = AutoCompleteMode.Suggest;
				this.tscbStock.AutoCompleteSource = AutoCompleteSource.ListItems;
				this.bgwStockInPlayLoadData = new BackgroundWorker();
				this.bgwStockInPlayLoadData.DoWork += new DoWorkEventHandler(this.bgwStockInPlayLoadData_DoWork);
				this.bgwStockInPlayLoadData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwStockInPlayLoadData_RunWorkerCompleted);
				this.bgwSaleByPriceLoadData = new BackgroundWorker();
				this.bgwSaleByPriceLoadData.DoWork += new DoWorkEventHandler(this.bgwSaleByPriceLoadData_DoWork);
				this.bgwSaleByPriceLoadData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwSaleByPriceLoadData_RunWorkerCompleted);
				this.bgwSaleByTimeLoadData = new BackgroundWorker();
				this.bgwSaleByTimeLoadData.DoWork += new DoWorkEventHandler(this.bgwSaleByTimeLoadData_DoWork);
				this.bgwSaleByTimeLoadData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwSaleByTimeLoadData_RunWorkerCompleted);
				this.bgwViewOddLotLoadData = new BackgroundWorker();
				this.bgwViewOddLotLoadData.DoWork += new DoWorkEventHandler(this.bgwViewOddLotLoadData_DoWork);
				this.bgwViewOddLotLoadData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwViewOddLotLoadData_RunWorkerCompleted);
				this.tscbSelection.Items.Clear();
				this.tscbSelection.Items.Add("Stock in Play");
				this.tscbSelection.Items.Add("Sale by Price");
				this.tscbSelection.Items.Add("Sale by Time");
				this.tscbSelection.Items.Add("View Oddlot");
			}
			catch (Exception ex)
			{
				this.ShowError("Init", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmStockSummary_IDoShownDelay()
		{
			this.SetPage(this.currentPage);
			this.SetResize();
			base.Show();
			base.OpenedForm();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmStockSummary_IDoLoadData()
		{
			this.ReloadDataPage(this.currentPage, true);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmStockSummary_IDoReActivated()
		{
			if (!base.IsLoadingData)
			{
				if (this._subPage != string.Empty && this._subPage != this.currentPage)
				{
					this.currentPage = this._subPage;
					this._subPage = string.Empty;
					this.SetResize();
					base.Show();
					this.ReloadDataPage(this.currentPage, true);
				}
				else
				{
					this.SetResize();
					base.Show();
					this.ReloadDataPage(this.currentPage, this.IsHeightChanged);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmStockSummary_IDoCustomSizeChanged()
		{
			if (!base.IsLoadingData)
			{
				this.SetResize();
				if (this.IsHeightChanged)
				{
					string text = this.currentPage;
					if (text != null)
					{
						if (!(text == "Sale by Time"))
						{
							if (text == "Stock in Play")
							{
								if (this._stockInfo != null)
								{
									this.StockInPlayReloadData(0m, "", this._stockInfo.Number);
								}
								else
								{
									if (this._seriesInfo != null)
									{
										this.StockInPlayReloadData_TFEX(this._seriesInfo.Symbol, this._seriesInfo.SeriesType, this._seriesInfo.TickSize, 0m, "");
									}
								}
							}
						}
						else
						{
							this.SaleByTimeReloadData();
						}
					}
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmStockSummary_IDoFontChanged()
		{
			if (!base.IsLoadingData)
			{
				this.SetResize();
				if (this.currentPage == "Stock in Play")
				{
					this.StockInPlay_SetNewStock(ApplicationInfo.CurrentSymbol, true);
				}
				else
				{
					if (this.currentPage == "Sale by Time")
					{
						this.SaleByTime_SetNewStock(ApplicationInfo.CurrentSymbol, true);
					}
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmStockSummary_IDoMainFormKeyUp(KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != Keys.Space)
			{
				if (keyCode == Keys.Home)
				{
					if (this.currentPage == "Stock in Play")
					{
						this.currentPage = "Sale by Price";
					}
					else
					{
						if (this.currentPage == "Sale by Price")
						{
							this.currentPage = "Sale by Time";
						}
						else
						{
							if (this.currentPage == "Sale by Time")
							{
								this.currentPage = "View Oddlot";
							}
							else
							{
								if (this.currentPage == "View Oddlot")
								{
									this.currentPage = "Stock in Play";
								}
							}
						}
					}
					this.SetPage(this.currentPage);
					this.SetResize();
					this.ReloadDataPage(this.currentPage, true);
				}
			}
			else
			{
				this.tscbStock.Focus();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmStockSummary_IDoSymbolLinked(object sender, SymbolLinkSource source, string newStock)
		{
			try
			{
				if (source == SymbolLinkSource.StockSymbol)
				{
					if (this.currentPage == "Stock in Play")
					{
						this.StockInPlay_SetNewStock(newStock, false);
					}
					else
					{
						if (this.currentPage == "Sale by Price")
						{
							this.SaleByPrice_SetNewStock(newStock, false);
						}
						else
						{
							if (this.currentPage == "Sale by Time")
							{
								this.SaleByTime_SetNewStock(newStock, false);
							}
							else
							{
								if (this.currentPage == "View Oddlot")
								{
									this.ViewOddLotSetNewStock(newStock, false);
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("frmStockSummary_IDoSymbolLinked", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetResize()
		{
			try
			{
				int height = this.tStripMenu.Height;
				int num = (int)((double)base.ClientSize.Width * 0.68);
				int num2 = base.ClientSize.Height - height;
				this.tscbSelection.Text = this.currentPage;
				this.tscbStock.DropDownWidth = this.tscbStock.Width;
				if (this.currentPage == "Stock in Play")
				{
					this.intzaStockInPlay.SetBounds(0, height, num, num2);
					if (this.intzaStockInPlay.GetRecordPerPage() != this.intzaStockInPlay.Rows)
					{
						this.intzaStockInPlay.SetRowByHeight();
						this.maxTopStockInPlay = this.intzaStockInPlay.Rows - 1;
					}
				}
				else
				{
					if (this.currentPage == "Sale by Price")
					{
						this.intzaSaleByPrice.SetBounds(0, height, base.ClientSize.Width, num2 / 2);
						this.wcGraphVolume.SetBounds(0, this.intzaSaleByPrice.Bottom + 1, this.intzaSaleByPrice.Width, num2 - (this.intzaSaleByPrice.Height + 1));
					}
					else
					{
						if (this.currentPage == "Sale by Time")
						{
							this.intzaSaleByTime.SetBounds(0, height, num, num2);
							if (this.intzaSaleByTime.GetRecordPerPage() != this.intzaSaleByTime.Rows)
							{
								this.intzaSaleByTime.SetRowByHeight();
							}
						}
						else
						{
							if (this.currentPage == "View Oddlot")
							{
								this.intzaViewOddLotInfo.SetBounds(0, height, num, this.intzaViewOddLotInfo.GetHeightByRows());
								this.intzaViewOddLot.SetBounds(0, this.intzaViewOddLotInfo.Bottom + 1, num, num2 - (this.intzaViewOddLotInfo.Height + 1));
							}
						}
					}
				}
				if (this.currentPage == "View Oddlot")
				{
					this.intzaLS.SetBounds(num + 1, height, base.ClientSize.Width - (num + 1), num2);
				}
				else
				{
					int num3 = this.intzaInfo.GetHeightByRows() + 2;
					this.intzaLS.SetBounds(num + 1, height, base.ClientSize.Width - (num + 1), base.Height - num3 - height - 1);
					this.intzaInfo.SetBounds(this.intzaLS.Left, this.intzaLS.Bottom + 1, this.intzaLS.Width, num3);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SetResizeByTab", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tscbStock_KeyPress(object sender, KeyPressEventArgs e)
		{
			try
			{
				e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
			}
			catch (Exception ex)
			{
				this.ShowError("tscmbStock_KeyPress", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tscbStock_KeyUp(object sender, KeyEventArgs e)
		{
			if (this.currentPage == "Stock in Play")
			{
				if (e.KeyCode == Keys.Return)
				{
					this.StockInPlay_SetNewStock(this.tscbStock.Text.Trim(), false);
				}
			}
			else
			{
				if (this.currentPage == "Sale by Price")
				{
					if (e.KeyCode == Keys.Return)
					{
						this.SaleByPrice_SetNewStock(this.tscbStock.Text.Trim(), false);
					}
				}
				else
				{
					if (this.currentPage == "Sale by Time")
					{
						if (e.KeyCode == Keys.Return)
						{
							this.SaleByTime_SetNewStock(this.tscbStock.Text.Trim(), false);
						}
						else
						{
							if (e.KeyCode == Keys.Right)
							{
								this.tstxtSaleByTimeSearchTimeHour.Focus();
								this.tstxtSaleByTimeSearchTimeHour.SelectAll();
							}
						}
					}
					else
					{
						if (this.currentPage == "View Oddlot")
						{
							if (e.KeyCode == Keys.Return)
							{
								this.ViewOddLotSetNewStock(this.tscbStock.Text.Trim(), false);
							}
						}
					}
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnStockInPlayPrevPage_Click(object sender, EventArgs e)
		{
			if (this._stockInfo != null && this._stockInfo.Number > 0)
			{
				if (this._stockInfo.Number > 0 && this.priceAtTopGrid > 0m && this.priceAtTopGrid < this._stockInfo.Ceiling)
				{
					this.StockInPlayReloadData(this.priceAtTopGrid, "+", this._stockInfo.Number);
				}
			}
			else
			{
				if (this._seriesInfo != null)
				{
					if (this.priceAtTopGrid > 0m && this.priceAtTopGrid < this._seriesInfo.Ceiling)
					{
						this.StockInPlayReloadData_TFEX(this._seriesInfo.Symbol, this._seriesInfo.SeriesType, this._seriesInfo.TickSize, this.priceAtTopGrid, "+");
					}
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnStockInPlayNextPage_Click(object sender, EventArgs e)
		{
			if (this._stockInfo != null && this._stockInfo.Number > 0)
			{
				if (this._stockInfo.Number > 0 && this.priceAtBottomGrid > 0m && this.priceAtBottomGrid > this._stockInfo.Floor)
				{
					this.StockInPlayReloadData(this.priceAtBottomGrid, "-", this._stockInfo.Number);
				}
			}
			else
			{
				if (this._seriesInfo != null)
				{
					if (this.priceAtTopGrid > 0m && this.priceAtTopGrid < this._seriesInfo.Ceiling)
					{
						this.StockInPlayReloadData_TFEX(this._seriesInfo.Symbol, this._seriesInfo.SeriesType, this._seriesInfo.TickSize, this.priceAtBottomGrid, "-");
					}
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tstxtSaleByTimeSearchTimeHour_KeyUp(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != Keys.Return)
			{
				switch (keyCode)
				{
				case Keys.Left:
					this.tscbStock.Focus();
					e.SuppressKeyPress = true;
					return;
				case Keys.Up:
					return;
				case Keys.Right:
					break;
				default:
					return;
				}
			}
			this.tstxtSaleByTimeSearchTimeMinute.Focus();
			this.tstxtSaleByTimeSearchTimeMinute.SelectAll();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tstxtSaleByTimeSearchTimeMinute_KeyUp(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != Keys.Return)
			{
				switch (keyCode)
				{
				case Keys.Left:
					this.tstxtSaleByTimeSearchTimeHour.Focus();
					this.tstxtSaleByTimeSearchTimeHour.SelectAll();
					break;
				case Keys.Right:
					e.SuppressKeyPress = true;
					break;
				}
			}
			else
			{
				this.SaleByTime_SetNewStock(this.tscbStock.Text.Trim(), true);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnSaleByTimeFirstPage_Click(object sender, EventArgs e)
		{
			if (this.saleByTimePageNo != 1)
			{
				this.saleByTimePageNo = 1;
				this.SaleByTimeReloadData();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnSaleByTimePrevPage_Click(object sender, EventArgs e)
		{
			if (this.saleByTimePageNo - 1 > 0)
			{
				this.saleByTimePageNo--;
				this.SaleByTimeReloadData();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnSaleByTimeNextPage_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.intzaSaleByTime.Records(this.intzaSaleByTime.Rows - 1).Fields("price").Text.ToString() != string.Empty)
				{
					this.saleByTimePageNo++;
					this.SaleByTimeReloadData();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tsbtnSaleByTimeNextPage_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwStockInPlayLoadData_DoWork(object sender, DoWorkEventArgs e)
		{
			if (this.FormState != ClientBaseForm.ClientBaseFormState.Closing)
			{
				try
				{
					if (this._stockInfo != null)
					{
						string[] array = ((string)e.Argument).Split(new char[]
						{
							'|'
						});
						decimal startPrice = 0m;
						decimal.TryParse(array[0].ToString(), out startPrice);
						string text = array[1].ToString();
						int securityNumber = 0;
						int.TryParse(array[2].ToString(), out securityNumber);
						base.IsLoadingData = true;
						string text2 = ApplicationInfo.WebService.StockInPlay(securityNumber, startPrice, text, this.maxTopStockInPlay, this.intzaLS.GetRecordPerPage());
						if (this.dsStockInPlay != null)
						{
							this.dsStockInPlay.Clear();
							this.dsStockInPlay = null;
						}
						this.dsStockInPlay = new DataSet();
						if (!string.IsNullOrEmpty(text2))
						{
							MyDataHelper.StringToDataSet(text2, this.dsStockInPlay);
						}
					}
					else
					{
						if (this._seriesInfo != null)
						{
							string[] array = ((string)e.Argument).Split(new char[]
							{
								'|'
							});
							decimal startPrice = 0m;
							decimal.TryParse(array[0].ToString(), out startPrice);
							string text = array[1].ToString();
							string seriesName = array[2];
							string seriesType = array[3];
							decimal tickSize;
							decimal.TryParse(array[4].ToString(), out tickSize);
							base.IsLoadingData = true;
							string text2 = string.Empty;
							if (text.Equals(""))
							{
								text2 = ApplicationInfo.WebServiceTFEX.StockInPlay(seriesName, seriesType, tickSize, ApplicationInfo.IndexInfoTfex.TXISession, startPrice, text, this.maxTopStockInPlay - 1, this.intzaLS.GetRecordPerPage());
							}
							else
							{
								text2 = ApplicationInfo.WebServiceTFEX.StockInPlay(seriesName, seriesType, tickSize, ApplicationInfo.IndexInfoTfex.TXISession, startPrice, text, this.maxTopStockInPlay - 1, this.intzaLS.GetRecordPerPage());
							}
							if (this.dsStockInPlay != null)
							{
								this.dsStockInPlay.Clear();
								this.dsStockInPlay = null;
							}
							this.dsStockInPlay = new DataSet();
							if (!string.IsNullOrEmpty(text2))
							{
								MyDataHelper.StringToDataSet(text2, this.dsStockInPlay);
							}
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("bgwStockInPlayLoadData_DoWork", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwStockInPlayLoadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				if (this.FormState != ClientBaseForm.ClientBaseFormState.Closing)
				{
					if (e.Error == null)
					{
						if (this._stockInfo != null)
						{
							this.StockInPlayUpdateToControl();
						}
						else
						{
							this.StockInPlayUpdateToControl_TFEX();
						}
						this.dsStockInPlay.Clear();
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("bgwStockInPlayLoadData_RunWorkerCompleted", ex);
			}
			base.IsLoadingData = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwSaleByPriceLoadData_DoWork(object sender, DoWorkEventArgs e)
		{
			if (this.FormState != ClientBaseForm.ClientBaseFormState.Closing)
			{
				base.IsLoadingData = true;
				try
				{
					if (this._stockInfo != null)
					{
						string text = ApplicationInfo.WebService.SaleByPrice(this._stockInfo.Number, 1, 999, 0);
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
					else
					{
						string text = ApplicationInfo.WebServiceTFEX.SeriesSaleByPrice(this._seriesInfo.Symbol, this._seriesInfo.SeriesType, 999);
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
		private void bgwSaleByPriceLoadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				if (this.FormState != ClientBaseForm.ClientBaseFormState.Closing)
				{
					if (e.Error == null)
					{
						if (this._stockInfo != null)
						{
							this.SaleByPriceUpdateToControl();
							this.dsSaleByPrice.Clear();
						}
						else
						{
							this.SaleByPriceUpdateToControl_TFEX();
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
		private void bgwSaleByTimeLoadData_DoWork(object sender, DoWorkEventArgs e)
		{
			if (this.FormState != ClientBaseForm.ClientBaseFormState.Closing)
			{
				try
				{
					base.IsLoadingData = true;
					string text = string.Empty;
					if (!string.IsNullOrEmpty(this.tstxtSaleByTimeSearchTimeHour.Text))
					{
						int num = 0;
						int.TryParse(this.tstxtSaleByTimeSearchTimeHour.Text, out num);
						if (num > 0 || num < 24)
						{
							string text2 = string.IsNullOrEmpty(this.tstxtSaleByTimeSearchTimeMinute.Text) ? "00" : this.tstxtSaleByTimeSearchTimeMinute.Text;
							int num2 = 0;
							int.TryParse(this.tstxtSaleByTimeSearchTimeHour.Text, out num2);
							if (num2 > 0)
							{
								if (this._stockInfo != null)
								{
									text = ((num2 < 10) ? "09" : num2.ToString()) + ":" + text2 + ":59";
								}
								else
								{
									if (this._seriesInfo != null)
									{
										text = ((num2 < 10) ? "09" : num2.ToString()) + text2 + "59";
									}
								}
							}
							else
							{
								text = string.Empty;
							}
						}
						else
						{
							text = string.Empty;
						}
					}
					else
					{
						text = string.Empty;
					}
					if (this._stockInfo != null)
					{
						ApplicationInfo.CurrentSymbol = this._stockInfo.Symbol;
						string text3 = ApplicationInfo.WebService.SaleByTime2(this._stockInfo.Number, (this.saleByTimePageNo - 1) * this.intzaSaleByTime.Rows + 1, this.intzaSaleByTime.Rows, this.intzaLS.GetRecordPerPage(), text);
						if (this.dsSaleByTime == null)
						{
							this.dsSaleByTime = new DataSet();
						}
						else
						{
							this.dsSaleByTime.Clear();
						}
						if (!string.IsNullOrEmpty(text3))
						{
							MyDataHelper.StringToDataSet(text3, this.dsSaleByTime);
						}
					}
					else
					{
						if (this._seriesInfo != null)
						{
							string text3 = ApplicationInfo.WebServiceTFEX.SeriesSaleByTime(this._seriesInfo.Symbol, this._seriesInfo.SeriesType, this.saleByTimePageNo, this.intzaSaleByTime.Rows, this.intzaLS.GetRecordPerPage(), text);
							if (this.dsSaleByTime == null)
							{
								this.dsSaleByTime = new DataSet();
							}
							else
							{
								this.dsSaleByTime.Clear();
							}
							if (!string.IsNullOrEmpty(text3))
							{
								MyDataHelper.StringToDataSet(text3, this.dsSaleByTime);
							}
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("bgwSaleByTimeLoadData_DoWork", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwSaleByTimeLoadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (this.FormState != ClientBaseForm.ClientBaseFormState.Closing)
			{
				if (e.Error == null)
				{
					if (this._stockInfo != null)
					{
						this.SaleByTimeUpdateToControl();
					}
					else
					{
						if (this._seriesInfo != null)
						{
							this.SaleByTimeUpdateToControl_TFEX();
						}
					}
					this.dsSaleByTime.Clear();
				}
				base.IsLoadingData = false;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwViewOddLotLoadData_DoWork(object sender, DoWorkEventArgs e)
		{
			if (this.FormState != ClientBaseForm.ClientBaseFormState.Closing)
			{
				try
				{
					string text = ApplicationInfo.WebService.ViewOddlot(this._stockInfo.Number, this.intzaLS.GetRecordPerPage());
					if (this.dsViewOddLot == null)
					{
						this.dsViewOddLot = new DataSet();
					}
					else
					{
						this.dsViewOddLot.Clear();
					}
					if (!string.IsNullOrEmpty(text))
					{
						MyDataHelper.StringToDataSet(text, this.dsViewOddLot);
					}
				}
				catch (Exception ex)
				{
					this.ShowError("bgwViewOddLotLoadData_DoWork", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwViewOddLotLoadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (this.FormState != ClientBaseForm.ClientBaseFormState.Closing)
			{
				this.ViewOddLotUpdateToControl(this.dsViewOddLot);
				this.dsViewOddLot.Clear();
				base.IsLoadingData = false;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override Dictionary<string, object> DoPackProperties()
		{
			return base.PropertiesValue;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void DoUnpackProperties()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ReloadDataPage(string page, bool isForce)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new frmStockSummary.ReloadDataPageCallBack(this.ReloadDataPage), new object[]
				{
					page,
					isForce
				});
			}
			else
			{
				try
				{
					if (page == "Stock in Play")
					{
						if (isForce || this.tscbStock.Text != ApplicationInfo.CurrentSymbol)
						{
							this.tscbStock.Text = ApplicationInfo.CurrentSymbol;
							this.StockInPlay_SetNewStock(ApplicationInfo.CurrentSymbol, isForce);
						}
						else
						{
							this.tscbStock.Focus();
						}
					}
					else
					{
						if (page == "Sale by Price")
						{
							if (isForce || this.tscbStock.Text != ApplicationInfo.CurrentSymbol)
							{
								this.tscbStock.Text = ApplicationInfo.CurrentSymbol;
								this.SaleByPrice_SetNewStock(ApplicationInfo.CurrentSymbol, isForce);
							}
							else
							{
								this.tscbStock.Focus();
							}
						}
						else
						{
							if (page == "Sale by Time")
							{
								if (isForce || this.tscbStock.Text != ApplicationInfo.CurrentSymbol)
								{
									this.tscbStock.Text = ApplicationInfo.CurrentSymbol;
									this.SaleByTime_SetNewStock(ApplicationInfo.CurrentSymbol, isForce);
								}
								else
								{
									this.tscbStock.Focus();
								}
							}
							else
							{
								if (page == "View Oddlot")
								{
									if (isForce || this.tscbStock.Text != ApplicationInfo.CurrentSymbol)
									{
										this.tscbStock.Text = ApplicationInfo.CurrentSymbol;
										this.ViewOddLotSetNewStock(ApplicationInfo.CurrentSymbol, isForce);
									}
									else
									{
										this.tscbStock.Focus();
									}
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("ReloadDataPage", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void StockInPlay_SetNewStock(string symbol, bool isForce)
		{
			try
			{
				if (symbol != string.Empty && (isForce || symbol != this._currentSymbol))
				{
					StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[symbol];
					if (stockInformation.Number > 0)
					{
						this._stockInfo = stockInformation;
						this._seriesInfo = null;
						ApplicationInfo.CurrentSymbol = this._stockInfo.Symbol;
						this._currentSymbol = this._stockInfo.Symbol;
						this.StockInPlayReloadData(0m, "", this._stockInfo.Number);
					}
					else
					{
						SeriesList.SeriesInformation seriesInformation = ApplicationInfo.SeriesInfo[symbol];
						if (seriesInformation.Symbol != string.Empty)
						{
							this._isNewStock = true;
							this._currentSymbol = seriesInformation.Symbol;
							this._seriesInfo = seriesInformation;
							this._stockInfo = null;
							ApplicationInfo.CurrentSymbol = seriesInformation.Symbol;
							this.StockInPlayReloadData_TFEX(seriesInformation.Symbol, seriesInformation.SeriesType, seriesInformation.TickSize, 0m, "");
						}
					}
				}
				if (this.tscbStock.Text != ApplicationInfo.CurrentSymbol)
				{
					this.tscbStock.Text = ApplicationInfo.CurrentSymbol;
				}
				this.tscbStock.Focus();
				this.tscbStock.SelectAll();
			}
			catch (Exception ex)
			{
				this.ShowError("StockInPlaySetNewStock", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void StockInPlayReloadData(decimal currentPrice, string sign, int stockNumber)
		{
			if (!this.bgwStockInPlayLoadData.IsBusy)
			{
				this.bgwStockInPlayLoadData.RunWorkerAsync(string.Concat(new object[]
				{
					currentPrice,
					"|",
					sign,
					"|",
					stockNumber
				}));
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void StockInPlayReloadData_TFEX(string seriesName, string seriesType, decimal tickSize, decimal currentPrice, string sign)
		{
			if (!this.bgwStockInPlayLoadData.IsBusy)
			{
				this.bgwStockInPlayLoadData.RunWorkerAsync(string.Concat(new object[]
				{
					currentPrice,
					"|",
					sign,
					"|",
					seriesName,
					"|",
					seriesType,
					"|",
					tickSize.ToString()
				}));
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void StockInPlayUpdateToControl()
		{
			try
			{
				this.intzaStockInPlay.BeginUpdate();
				if (this.dsStockInPlay.Tables.Contains("security_stat") && this.dsStockInPlay.Tables["security_stat"].Rows.Count > 0)
				{
					this.intzaStockInPlay.ClearAllText();
				}
				else
				{
					for (int i = 0; i < this.intzaStockInPlay.Rows - 1; i++)
					{
						this.intzaStockInPlay.ClearAllTextByRow(i);
					}
				}
				this.intzaLS.BeginUpdate();
				this.intzaLS.ClearAllText();
				this.priceAtBottomGrid = 0m;
				this.priceAtTopGrid = 0m;
				if (this.dsStockInPlay.Tables.Count > 0)
				{
					if (this.dsStockInPlay.Tables.Contains("stockinplay_price"))
					{
						if (this.dsStockInPlay.Tables["stockinplay_price"].Rows.Count > 0)
						{
							DataRow dataRow = this.dsStockInPlay.Tables["stockinplay_price"].Rows[0];
							string[] array = dataRow["price_list"].ToString().Split(new char[]
							{
								';'
							});
							int num = 0;
							for (int j = array.Length - 1; j >= 0; j--)
							{
								decimal num2;
								if (decimal.TryParse(array[j], out num2))
								{
									if ((num2 >= this._stockInfo.Floor && num2 <= this._stockInfo.Ceiling) || num2 == 0m)
									{
										this.intzaStockInPlay.Records(num).Fields("price").Text = Utilities.PriceFormat(array[j]);
										this.intzaStockInPlay.Records(num).Fields("price").FontColor = Utilities.ComparePriceCFColor(num2, this._stockInfo);
										this.intzaStockInPlay.Records(num).Fields("price").BackColor = Color.FromArgb(30, 30, 30);
										num++;
									}
								}
							}
							this.priceAtBottomGrid = 0m;
							if (array[0] != string.Empty)
							{
								this.priceAtBottomGrid = Convert.ToDecimal(array[0]);
							}
							this.priceAtTopGrid = 0m;
							if (array[array.Length - 1] != string.Empty)
							{
								this.priceAtTopGrid = Convert.ToDecimal(array[array.Length - 1]);
							}
							int.TryParse(dataRow["buy_deals"].ToString(), out this._buyDeals);
							int.TryParse(dataRow["sell_deals"].ToString(), out this._sellDeals);
						}
					}
					if (this.dsStockInPlay.Tables.Contains("security_stat") && this.dsStockInPlay.Tables["security_stat"].Rows.Count > 0)
					{
						long num3;
						long.TryParse(this.dsStockInPlay.Tables["security_stat"].Rows[0]["buy_volume"].ToString(), out num3);
						long num4;
						long.TryParse(this.dsStockInPlay.Tables["security_stat"].Rows[0]["sell_volume"].ToString(), out num4);
						long openVolume;
						long.TryParse(this.dsStockInPlay.Tables["security_stat"].Rows[0]["open_volume"].ToString(), out openVolume);
						this.StockInPlayUpdateTotalVolumeAndTotalDeals(num3, num4, openVolume);
					}
					if (this.dsStockInPlay.Tables.Contains("sale_by_price"))
					{
						foreach (DataRow dataRow in this.dsStockInPlay.Tables["sale_by_price"].Rows)
						{
							decimal num2;
							decimal.TryParse(dataRow["price"].ToString(), out num2);
							long num3;
							long.TryParse(dataRow["buy_volume"].ToString(), out num3);
							int deals;
							int.TryParse(dataRow["buy_deals"].ToString(), out deals);
							this.StockInPlayUpdateBuySellVolume("B", num2, num3, deals);
							long num4;
							long.TryParse(dataRow["sell_volume"].ToString(), out num4);
							int deals2;
							int.TryParse(dataRow["sell_deals"].ToString(), out deals2);
							this.StockInPlayUpdateBuySellVolume("S", num2, num4, deals2);
						}
					}
					if (this.dsStockInPlay.Tables.Contains("security_stat") && this.dsStockInPlay.Tables["security_stat"].Rows.Count > 0)
					{
						DataRow dataRow = this.dsStockInPlay.Tables["security_stat"].Rows[0];
						decimal.TryParse(dataRow["bid_price1"].ToString(), out this._tfexBidPrice1);
						decimal.TryParse(dataRow["bid_price2"].ToString(), out this._tfexBidPrice2);
						decimal.TryParse(dataRow["bid_price3"].ToString(), out this._tfexBidPrice3);
						decimal.TryParse(dataRow["bid_price4"].ToString(), out this._tfexBidPrice4);
						decimal.TryParse(dataRow["bid_price5"].ToString(), out this._tfexBidPrice5);
						long volume;
						long.TryParse(dataRow["bid_volume1"].ToString(), out volume);
						long volume2;
						long.TryParse(dataRow["bid_volume2"].ToString(), out volume2);
						long volume3;
						long.TryParse(dataRow["bid_volume3"].ToString(), out volume3);
						long volume4;
						long.TryParse(dataRow["bid_volume4"].ToString(), out volume4);
						long volume5;
						long.TryParse(dataRow["bid_volume5"].ToString(), out volume5);
						this.StockInPlayUpdateTopPrice("B", this._tfexBidPrice1, this._tfexBidPrice2, this._tfexBidPrice3, this._tfexBidPrice4, this._tfexBidPrice5, volume, volume2, volume3, volume4, volume5);
						decimal.TryParse(dataRow["offer_price1"].ToString(), out this._tfexAskPrice1);
						decimal.TryParse(dataRow["offer_price2"].ToString(), out this._tfexAskPrice2);
						decimal.TryParse(dataRow["offer_price3"].ToString(), out this._tfexAskPrice3);
						decimal.TryParse(dataRow["offer_price4"].ToString(), out this._tfexAskPrice4);
						decimal.TryParse(dataRow["offer_price5"].ToString(), out this._tfexAskPrice5);
						long.TryParse(dataRow["offer_volume1"].ToString(), out volume);
						long.TryParse(dataRow["offer_volume2"].ToString(), out volume2);
						long.TryParse(dataRow["offer_volume3"].ToString(), out volume3);
						long.TryParse(dataRow["offer_volume4"].ToString(), out volume4);
						long.TryParse(dataRow["offer_volume5"].ToString(), out volume5);
						this.StockInPlayUpdateTopPrice("S", this._tfexAskPrice1, this._tfexAskPrice2, this._tfexAskPrice3, this._tfexAskPrice4, this._tfexAskPrice5, volume, volume2, volume3, volume4, volume5);
					}
					this.UpdateStockInfo(this.dsStockInPlay, this._stockInfo);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateToControl", ex);
			}
			finally
			{
				this.intzaStockInPlay.Redraw();
				this.intzaLS.Redraw();
				this.intzaInfo.Redraw();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void StockInPlayUpdateToControl_TFEX()
		{
			try
			{
				this.intzaStockInPlay.BeginUpdate();
				this.intzaLS.BeginUpdate();
				this.intzaInfo.BeginUpdate();
				if (this._isNewStock)
				{
					this.intzaInfo.ClearAllText();
					this.intzaLS.ClearAllText();
					this.intzaStockInPlay.ClearAllText();
				}
				else
				{
					for (int i = 0; i < this.intzaStockInPlay.Rows - 1; i++)
					{
						this.intzaStockInPlay.ClearAllTextByRow(i);
					}
				}
				this._isNewStock = false;
				this.priceAtBottomGrid = 0m;
				this.priceAtTopGrid = 0m;
				if (this.dsStockInPlay.Tables.Count > 0)
				{
					if (this.dsStockInPlay.Tables.Contains("seriesinplay_price"))
					{
						if (this.dsStockInPlay.Tables["seriesinplay_price"].Rows.Count > 0)
						{
							DataRow dataRow = this.dsStockInPlay.Tables["seriesinplay_price"].Rows[0];
							string[] array = dataRow["price_list"].ToString().Split(new char[]
							{
								';'
							});
							int j = 0;
							for (int k = array.Length - 1; k >= 0; k--)
							{
								decimal num;
								if (decimal.TryParse(array[k], out num))
								{
									if ((num >= this._seriesInfo.Floor && num <= this._seriesInfo.Ceiling) || num == 0m)
									{
										if (num == 0m)
										{
											this.intzaStockInPlay.Records(j).Fields("price").Text = "0";
										}
										else
										{
											this.intzaStockInPlay.Records(j).Fields("price").Text = Utilities.PriceFormat(array[k], this._seriesInfo.NumOfDec);
										}
										this.intzaStockInPlay.Records(j).Fields("price").FontColor = Utilities.ComparePriceCFColor(num, this._seriesInfo);
									}
								}
								else
								{
									this.intzaStockInPlay.Records(j).Fields("price").Text = "";
								}
								this.intzaStockInPlay.Records(j).Fields("price").BackColor = Color.FromArgb(50, 50, 50);
								j++;
							}
							this.priceAtBottomGrid = 0m;
							for (int l = 0; l < array.Length; l++)
							{
								if (array[l] != string.Empty)
								{
									this.priceAtBottomGrid = Convert.ToDecimal(array[l]);
									break;
								}
							}
							this.priceAtTopGrid = 0m;
							for (int l = array.Length - 1; l > -1; l--)
							{
								if (array[l] != string.Empty)
								{
									this.priceAtTopGrid = Convert.ToDecimal(array[l]);
									break;
								}
							}
							int.TryParse(dataRow["buy_deals"].ToString(), out this._buyDeals);
							int.TryParse(dataRow["sell_deals"].ToString(), out this._sellDeals);
						}
					}
					if (this.dsStockInPlay.Tables.Contains("series_info_stat") && this.dsStockInPlay.Tables["series_info_stat"].Rows.Count > 0)
					{
						long num2;
						long.TryParse(this.dsStockInPlay.Tables["series_info_stat"].Rows[0]["LongQty"].ToString(), out num2);
						long num3;
						long.TryParse(this.dsStockInPlay.Tables["series_info_stat"].Rows[0]["ShortQty"].ToString(), out num3);
						long openVolume;
						long.TryParse(this.dsStockInPlay.Tables["series_info_stat"].Rows[0]["TotalOpenQty"].ToString(), out openVolume);
						this.StockInPlayUpdateTotalVolumeAndTotalDeals(num2, num3, openVolume);
					}
					if (this.dsStockInPlay.Tables.Contains("sale_by_price"))
					{
						foreach (DataRow dataRow2 in this.dsStockInPlay.Tables["sale_by_price"].Rows)
						{
							decimal num;
							decimal.TryParse(dataRow2["price"].ToString(), out num);
							long num2;
							long.TryParse(dataRow2["long_volume"].ToString(), out num2);
							int dealBySide;
							int.TryParse(dataRow2["long_deals"].ToString(), out dealBySide);
							this.StockInPlayUpdateBuySellVolume_TFEX("B", num, num2, dealBySide);
							long num3;
							long.TryParse(dataRow2["short_volume"].ToString(), out num3);
							int dealBySide2;
							int.TryParse(dataRow2["short_deals"].ToString(), out dealBySide2);
							this.StockInPlayUpdateBuySellVolume_TFEX("S", num, num3, dealBySide2);
						}
					}
					if (this.dsStockInPlay.Tables.Contains("top_price") && this.dsStockInPlay.Tables["top_price"].Rows.Count > 0)
					{
						DataRow dataRow3 = this.dsStockInPlay.Tables["top_price"].Rows[0];
						decimal.TryParse(dataRow3["bidprice1"].ToString(), out this._tfexBidPrice1);
						decimal.TryParse(dataRow3["bidprice2"].ToString(), out this._tfexBidPrice2);
						decimal.TryParse(dataRow3["bidprice3"].ToString(), out this._tfexBidPrice3);
						decimal.TryParse(dataRow3["bidprice4"].ToString(), out this._tfexBidPrice4);
						decimal.TryParse(dataRow3["bidprice5"].ToString(), out this._tfexBidPrice5);
						long volume;
						long.TryParse(dataRow3["bidQty1"].ToString(), out volume);
						long volume2;
						long.TryParse(dataRow3["bidQty2"].ToString(), out volume2);
						long volume3;
						long.TryParse(dataRow3["bidQty3"].ToString(), out volume3);
						long volume4;
						long.TryParse(dataRow3["bidQty4"].ToString(), out volume4);
						long volume5;
						long.TryParse(dataRow3["bidQty5"].ToString(), out volume5);
						this.StockInPlayUpdateTopPrice_TFEX("B", this._tfexBidPrice1, this._tfexBidPrice2, this._tfexBidPrice3, this._tfexBidPrice4, this._tfexBidPrice5, volume, volume2, volume3, volume4, volume5);
						decimal.TryParse(dataRow3["Askprice1"].ToString(), out this._tfexAskPrice1);
						decimal.TryParse(dataRow3["Askprice2"].ToString(), out this._tfexAskPrice2);
						decimal.TryParse(dataRow3["Askprice3"].ToString(), out this._tfexAskPrice3);
						decimal.TryParse(dataRow3["Askprice4"].ToString(), out this._tfexAskPrice4);
						decimal.TryParse(dataRow3["Askprice5"].ToString(), out this._tfexAskPrice5);
						long.TryParse(dataRow3["AskQty1"].ToString(), out volume);
						long.TryParse(dataRow3["AskQty2"].ToString(), out volume2);
						long.TryParse(dataRow3["AskQty3"].ToString(), out volume3);
						long.TryParse(dataRow3["AskQty4"].ToString(), out volume4);
						long.TryParse(dataRow3["AskQty5"].ToString(), out volume5);
						this.StockInPlayUpdateTopPrice_TFEX("A", this._tfexAskPrice1, this._tfexAskPrice2, this._tfexAskPrice3, this._tfexAskPrice4, this._tfexAskPrice5, volume, volume2, volume3, volume4, volume5);
					}
					if (this.dsStockInPlay.Tables.Contains("series_info_stat") && this.dsStockInPlay.Tables["series_info_stat"].Rows.Count > 0)
					{
						DataRow dataRow2 = this.dsStockInPlay.Tables["series_info_stat"].Rows[0];
						decimal num4;
						decimal.TryParse(dataRow2["PrevFixing"].ToString(), out num4);
						decimal num5;
						decimal.TryParse(dataRow2["FixingPrice"].ToString(), out num5);
						decimal num6;
						decimal.TryParse(dataRow2["PrjOpenPrice"].ToString(), out num6);
						decimal num7;
						decimal.TryParse(dataRow2["PrjOpenPrice2"].ToString(), out num7);
						decimal num8;
						decimal.TryParse(dataRow2["PrjOpenPrice3"].ToString(), out num8);
						decimal num9;
						decimal.TryParse(dataRow2["PriceOpen1"].ToString(), out num9);
						decimal num10;
						decimal.TryParse(dataRow2["PriceOpen2"].ToString(), out num10);
						decimal num11;
						decimal.TryParse(dataRow2["PriceOpen3"].ToString(), out num11);
						int deals;
						int.TryParse(dataRow2["NumOfDeal"].ToString(), out deals);
						int num12;
						int.TryParse(dataRow2["TurnOverQty"].ToString(), out num12);
						decimal accValue;
						decimal.TryParse(dataRow2["TurnOverValue"].ToString(), out accValue);
						long openVolume2;
						long.TryParse(dataRow2["TotalOpenQty"].ToString(), out openVolume2);
						long buyVolume;
						long.TryParse(dataRow2["LongQty"].ToString(), out buyVolume);
						long sellVolume;
						long.TryParse(dataRow2["ShortQty"].ToString(), out sellVolume);
						decimal num13;
						decimal.TryParse(dataRow2["Multiplier"].ToString(), out num13);
						this.intzaInfo.Item("tbCeiling").Text = Utilities.PriceFormat(dataRow2["CeilingPrice"].ToString(), this._seriesInfo.NumOfDec);
						this.intzaInfo.Item("tbFloor").Text = Utilities.PriceFormat(dataRow2["FloorPrice"].ToString(), this._seriesInfo.NumOfDec);
						this.intzaInfo.Item("tbPrior").Text = Utilities.PriceFormat(num4, this._seriesInfo.NumOfDec);
						decimal lastPrice;
						decimal.TryParse(dataRow2["LastPrice"].ToString(), out lastPrice);
						decimal high;
						decimal.TryParse(dataRow2["HighPrice"].ToString(), out high);
						decimal low;
						decimal.TryParse(dataRow2["LowPrice"].ToString(), out low);
						decimal accValue2;
						decimal.TryParse(dataRow2["TurnOverValue"].ToString(), out accValue2);
						long accVolume;
						long.TryParse(dataRow2["TurnOverQty"].ToString(), out accVolume);
						this.MainUpdateLastSalePrice_TFEX(lastPrice, high, low, accValue2, accVolume, this._seriesInfo);
						if (ApplicationInfo.IndexInfoTfex.TXISession == 1)
						{
							if (num9 != 0m)
							{
								this.UpdateOpenOrProjectOpenPriceTFEX("11", num9, this._seriesInfo);
							}
							else
							{
								if (num6 != 0m && ApplicationInfo.IndexInfoTfex.TXIState == "7")
								{
									this.UpdateOpenOrProjectOpenPriceTFEX("7", num6, this._seriesInfo);
								}
								else
								{
									this.intzaInfo.Item("tbOpen1").BackColor = Color.Black;
								}
							}
						}
						else
						{
							if (ApplicationInfo.IndexInfoTfex.TXISession == 2)
							{
								if (num9 != 0m)
								{
									this.UpdateOpenOrProjectOpenPriceTFEX("11", num9, this._seriesInfo);
								}
								if (num10 != 0m)
								{
									this.UpdateOpenOrProjectOpenPriceTFEX("10", num10, this._seriesInfo);
								}
								else
								{
									if (num7 != 0m && ApplicationInfo.IndexInfoTfex.TXIState == "9")
									{
										this.UpdateOpenOrProjectOpenPriceTFEX("9", num7, this._seriesInfo);
									}
									else
									{
										this.intzaInfo.Item("tbOpen2").BackColor = Color.Black;
									}
								}
								if (num11 != 0m)
								{
									this.UpdateOpenOrProjectOpenPriceTFEX("24", num11, this._seriesInfo);
								}
								else
								{
									if (num8 != 0m && ApplicationInfo.IndexInfoTfex.TXMState == "23")
									{
										this.UpdateOpenOrProjectOpenPriceTFEX("23", num8, this._seriesInfo);
									}
									else
									{
										this.intzaInfo.Item("tbOpen3").BackColor = Color.Black;
									}
								}
							}
						}
						this.MainUpdateAllVolume(deals, (long)num12, accValue, openVolume2, buyVolume, sellVolume);
					}
					if (this.dsStockInPlay.Tables.Contains("last_sale_tfex"))
					{
						if (this.intzaLS.Rows != this.intzaLS.GetRecordPerPage())
						{
							this.intzaLS.Rows = this.intzaLS.GetRecordPerPage();
						}
						for (int j = 0; j < this.dsStockInPlay.Tables["last_sale_tfex"].Rows.Count; j++)
						{
							DataRow dataRow4 = this.dsStockInPlay.Tables["last_sale_tfex"].Rows[j];
							decimal num;
							decimal.TryParse(dataRow4["nmrPrice"].ToString(), out num);
							long volume6;
							long.TryParse(dataRow4["iQuantity"].ToString(), out volume6);
							this.UpdateLastSaleTicker_TFEX(num, dataRow4["sSide"].ToString(), volume6, dataRow4["dtLastUpd"].ToString(), j, this._seriesInfo);
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateToControl", ex);
			}
			finally
			{
				this.intzaStockInPlay.Redraw();
				this.intzaLS.Redraw();
				this.intzaInfo.Redraw();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateLastSaleTicker_TFEX(decimal price, string side, long volume, string lastUpdate, int index, SeriesList.SeriesInformation seriesInfo)
		{
			try
			{
				if (this.intzaLS.InvokeRequired)
				{
					this.intzaLS.Invoke(new frmStockSummary.UpdateLastSaleTicker_TFEXCallBack(this.UpdateLastSaleTicker_TFEX), new object[]
					{
						price,
						side,
						volume,
						lastUpdate,
						index,
						seriesInfo
					});
				}
				else
				{
					RecordItem recordItem;
					if (index == -1)
					{
						recordItem = this.intzaLS.AddRecord(1, true);
					}
					else
					{
						recordItem = this.intzaLS.Records(index);
					}
					recordItem.Fields("side").Text = side.ToString();
					recordItem.Fields("volume").Text = volume.ToString();
					recordItem.Fields("price").Text = Utilities.PriceFormat(price, seriesInfo.NumOfDec);
					recordItem.Fields("time").Text = Utilities.GetTimeLastSale(lastUpdate);
					Color fontColor = Utilities.ComparePriceCFColor(price, seriesInfo);
					recordItem.Fields("price").FontColor = fontColor;
					recordItem.Fields("time").FontColor = Color.LightGray;
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
					recordItem.Changed = true;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateLastSaleTicker_TFEX", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateMainBoardValue_TFEX(int deals, long accVolume, decimal accValue)
		{
			if (this.intzaInfo.InvokeRequired)
			{
				this.intzaInfo.Invoke(new frmStockSummary.UpdateMainBoardValue_TFEXCallBack(this.UpdateMainBoardValue_TFEX), new object[]
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
					this.intzaInfo.Item("tbMDeal").Text = deals.ToString();
					this.intzaInfo.Item("tbMVolume").Text = accVolume.ToString();
					this.intzaInfo.Item("tbMValue").Text = Utilities.PriceFormat(accValue);
				}
				catch (Exception ex)
				{
					this.ShowError("UpdateMainBoardValue_TFEX", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void MainUpdateLastSalePrice_TFEX(decimal lastPrice, decimal high, decimal low, decimal accValue, long accVolume, SeriesList.SeriesInformation sf_TFEX)
		{
			try
			{
				decimal num = 0m;
				decimal num2 = 0m;
				Color fontColor = Color.Yellow;
				if (accVolume > 0L)
				{
					decimal num3 = Math.Round(accValue / accVolume, 2);
					this.intzaInfo.Item("tbAverage").Text = Utilities.PriceFormat(num3, sf_TFEX.NumOfDec);
					this.intzaInfo.Item("tbAverage").FontColor = Utilities.ComparePriceCFColor(num3, sf_TFEX);
				}
				if (sf_TFEX.PrevFixPrice > 0m && lastPrice > 0m)
				{
					num = lastPrice - sf_TFEX.PrevFixPrice;
					num2 = (lastPrice - sf_TFEX.PrevFixPrice) / sf_TFEX.PrevFixPrice * 100m;
					fontColor = Utilities.ComparePriceColor(num, 0m);
				}
				this.intzaInfo.Item("tbChange").Text = Utilities.PriceFormat(num, true, sf_TFEX.NumOfDec);
				this.intzaInfo.Item("tbChangePct").Text = Utilities.PriceFormat(num2, true, "%");
				this.intzaInfo.Item("tbChange").FontColor = fontColor;
				this.intzaInfo.Item("tbChangePct").FontColor = fontColor;
				this.intzaInfo.Item("tbHigh").Text = Utilities.PriceFormat(high, sf_TFEX.NumOfDec);
				this.intzaInfo.Item("tbLow").Text = Utilities.PriceFormat(low, sf_TFEX.NumOfDec);
				this.intzaInfo.Item("tbHigh").FontColor = Utilities.ComparePriceCFColor(high, sf_TFEX);
				this.intzaInfo.Item("tbLow").FontColor = Utilities.ComparePriceCFColor(low, sf_TFEX);
			}
			catch (Exception ex)
			{
				this.ShowError("MainUpdateLastSalePrice_TFEX", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void StockInPlayUpdateBuySellVolume_TFEX(string side, decimal price, long volume, int dealBySide)
		{
			try
			{
				RecordItem recordItem = this.intzaStockInPlay.Find("price", Utilities.PriceFormat(price, this._seriesInfo.NumOfDec));
				if (recordItem != null)
				{
					int num = 0;
					decimal value = 0m;
					Color fontColor = recordItem.Fields("price").FontColor;
					if (side == "B")
					{
						int.TryParse(recordItem.Fields("buy_deal").Text.ToString().Replace(",", ""), out num);
						recordItem.Fields("buy_deal").Text = num + dealBySide;
						decimal.TryParse(recordItem.Fields("buy_volume").Text.ToString().Replace(",", ""), out value);
						recordItem.Fields("buy_volume").Text = Utilities.VolumeFormat((long)value + volume, true);
						recordItem.Fields("buy_deal").FontColor = fontColor;
						recordItem.Fields("buy_volume").FontColor = fontColor;
					}
					else
					{
						if (side == "S")
						{
							int.TryParse(recordItem.Fields("sell_deal").Text.ToString().Replace(",", ""), out num);
							recordItem.Fields("sell_deal").Text = num + dealBySide;
							decimal.TryParse(recordItem.Fields("sell_vol").Text.ToString().Replace(",", ""), out value);
							recordItem.Fields("sell_vol").Text = Utilities.VolumeFormat((long)value + volume, true);
							recordItem.Fields("sell_vol").FontColor = fontColor;
							recordItem.Fields("sell_deal").FontColor = fontColor;
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateBuySellVolume_TFEX", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void StockInPlayUpdateTopPrice_TFEX(string side, decimal price1, decimal price2, decimal price3, decimal price4, decimal price5, long volume1, long volume2, long volume3, long volume4, long volume5)
		{
			try
			{
				string columnName = string.Empty;
				if (side == "B")
				{
					columnName = "bid";
				}
				else
				{
					if (side == "A")
					{
						columnName = "offer";
					}
				}
				decimal num = (side == "B") ? this._tfexBidPrice1 : this._tfexAskPrice1;
				if (volume1 != -1L && price1 != num)
				{
					RecordItem recordItem = this.intzaStockInPlay.Find("price", Utilities.PriceFormat(num, this._seriesInfo.NumOfDec));
					if (recordItem != null)
					{
						recordItem.Fields(columnName).Text = string.Empty;
					}
				}
				num = ((side == "B") ? this._tfexBidPrice2 : this._tfexAskPrice2);
				if (volume2 != -1L && price2 != num)
				{
					RecordItem recordItem = this.intzaStockInPlay.Find("price", Utilities.PriceFormat(num, this._seriesInfo.NumOfDec));
					if (recordItem != null)
					{
						recordItem.Fields(columnName).Text = string.Empty;
					}
				}
				num = ((side == "B") ? this._tfexBidPrice3 : this._tfexAskPrice3);
				if (volume3 != -1L && price3 != num)
				{
					RecordItem recordItem = this.intzaStockInPlay.Find("price", Utilities.PriceFormat(num, this._seriesInfo.NumOfDec));
					if (recordItem != null)
					{
						recordItem.Fields(columnName).Text = string.Empty;
					}
				}
				num = ((side == "B") ? this._tfexBidPrice4 : this._tfexAskPrice4);
				if (volume4 != -1L && price4 != num)
				{
					RecordItem recordItem = this.intzaStockInPlay.Find("price", Utilities.PriceFormat(num, this._seriesInfo.NumOfDec));
					if (recordItem != null)
					{
						recordItem.Fields(columnName).Text = string.Empty;
					}
				}
				num = ((side == "B") ? this._tfexBidPrice5 : this._tfexAskPrice5);
				if (volume5 != -1L && price5 != num)
				{
					RecordItem recordItem = this.intzaStockInPlay.Find("price", Utilities.PriceFormat(num, this._seriesInfo.NumOfDec));
					if (recordItem != null)
					{
						recordItem.Fields(columnName).Text = string.Empty;
					}
				}
				if (volume1 != -1L)
				{
					RecordItem recordItem = this.intzaStockInPlay.Find("price", Utilities.PriceFormat(price1, this._seriesInfo.NumOfDec));
					if (recordItem != null)
					{
						recordItem.Fields(columnName).Text = volume1;
						recordItem.Fields(columnName).FontColor = recordItem.Fields("price").FontColor;
					}
					if (side == "B")
					{
						this._tfexBidPrice1 = price1;
					}
					else
					{
						this._tfexAskPrice1 = price1;
					}
				}
				if (volume2 != -1L)
				{
					RecordItem recordItem = this.intzaStockInPlay.Find("price", Utilities.PriceFormat(price2, this._seriesInfo.NumOfDec));
					if (recordItem != null)
					{
						recordItem.Fields(columnName).Text = volume2;
						recordItem.Fields(columnName).FontColor = recordItem.Fields("price").FontColor;
					}
					if (side == "B")
					{
						this._tfexBidPrice2 = price2;
					}
					else
					{
						this._tfexAskPrice2 = price2;
					}
				}
				if (volume3 != -1L)
				{
					RecordItem recordItem = this.intzaStockInPlay.Find("price", Utilities.PriceFormat(price3, this._seriesInfo.NumOfDec));
					if (recordItem != null)
					{
						recordItem.Fields(columnName).Text = volume3;
						recordItem.Fields(columnName).FontColor = recordItem.Fields("price").FontColor;
					}
					if (side == "B")
					{
						this._tfexBidPrice3 = price3;
					}
					else
					{
						this._tfexAskPrice3 = price3;
					}
				}
				if (volume4 != -1L)
				{
					RecordItem recordItem = this.intzaStockInPlay.Find("price", Utilities.PriceFormat(price4, this._seriesInfo.NumOfDec));
					if (recordItem != null)
					{
						recordItem.Fields(columnName).Text = volume4;
						recordItem.Fields(columnName).FontColor = recordItem.Fields("price").FontColor;
					}
					if (side == "B")
					{
						this._tfexBidPrice4 = price4;
					}
					else
					{
						this._tfexAskPrice4 = price4;
					}
				}
				if (volume5 != -1L)
				{
					RecordItem recordItem = this.intzaStockInPlay.Find("price", Utilities.PriceFormat(price5, this._seriesInfo.NumOfDec));
					if (recordItem != null)
					{
						recordItem.Fields(columnName).Text = volume5;
						recordItem.Fields(columnName).FontColor = recordItem.Fields("price").FontColor;
					}
					if (side == "B")
					{
						this._tfexBidPrice5 = price5;
					}
					else
					{
						this._tfexAskPrice5 = price5;
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("StockInPlayUpdateTopPrice", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateStockInfo(DataSet ds, StockList.StockInformation sfMain)
		{
			if (ds.Tables.Contains("security_stat") && ds.Tables["security_stat"].Rows.Count > 0)
			{
				DataRow dataRow = ds.Tables["security_stat"].Rows[0];
				this.intzaInfo.BeginUpdate();
				this.intzaInfo.ClearAllText();
				this.intzaInfo.Item("tbCeiling").Text = sfMain.Ceiling.ToString();
				this.intzaInfo.Item("tbFloor").Text = sfMain.Floor.ToString();
				this.intzaInfo.Item("tbPrior").Text = sfMain.PriorPrice.ToString();
				this.intzaInfo.Item("tbFlag").Text = dataRow["display_flag"].ToString();
				decimal lastPrice;
				decimal.TryParse(dataRow["last_sale_price"].ToString(), out lastPrice);
				decimal high;
				decimal.TryParse(dataRow["high_price"].ToString(), out high);
				decimal low;
				decimal.TryParse(dataRow["low_price"].ToString(), out low);
				decimal num;
				decimal.TryParse(dataRow["accvalue"].ToString(), out num);
				long num2;
				long.TryParse(dataRow["accvolume"].ToString(), out num2);
				this.MainUpdatePrice(lastPrice, high, low, num, num2 / (long)sfMain.BoardLot, sfMain);
				if (ApplicationInfo.MarketSession == 1)
				{
					if (ApplicationInfo.MarketState == "P")
					{
						decimal price;
						decimal.TryParse(dataRow["projected_open"].ToString(), out price);
						this.MainUpdateOpenOrProjectOpenPrice("P", 1, price, sfMain);
					}
					else
					{
						decimal price2;
						decimal.TryParse(dataRow["open_price1"].ToString(), out price2);
						this.MainUpdateOpenOrProjectOpenPrice("O", 1, price2, sfMain);
					}
				}
				else
				{
					if (ApplicationInfo.MarketSession == 2)
					{
						decimal price3;
						decimal.TryParse(dataRow["open_price1"].ToString(), out price3);
						this.MainUpdateOpenOrProjectOpenPrice("O", 1, price3, sfMain);
						if (ApplicationInfo.MarketState == "P")
						{
							decimal price;
							decimal.TryParse(dataRow["projected_open"].ToString(), out price);
							this.MainUpdateOpenOrProjectOpenPrice("P", 2, price, sfMain);
						}
						else
						{
							decimal price4;
							decimal.TryParse(dataRow["open_price2"].ToString(), out price4);
							this.MainUpdateOpenOrProjectOpenPrice("O", 2, price4, sfMain);
						}
					}
				}
				int deals;
				int.TryParse(dataRow["deals"].ToString(), out deals);
				long.TryParse(dataRow["accvolume"].ToString(), out num2);
				long openVolume;
				long.TryParse(dataRow["open_volume"].ToString(), out openVolume);
				long buyVolume;
				long.TryParse(dataRow["buy_volume"].ToString(), out buyVolume);
				long sellVolume;
				long.TryParse(dataRow["sell_volume"].ToString(), out sellVolume);
				decimal.TryParse(dataRow["accvalue"].ToString(), out num);
				this.MainUpdateAllVolume(deals, num2, num, openVolume, buyVolume, sellVolume);
				decimal.TryParse(dataRow["biglot_accvalue"].ToString(), out num);
				this.MainUpdateBigLotValue(num);
			}
			if (ds.Tables.Contains("last_sale"))
			{
				int num3 = 0;
				if (this.intzaLS.Rows != this.intzaLS.GetRecordPerPage())
				{
					this.intzaLS.Rows = this.intzaLS.GetRecordPerPage();
				}
				foreach (DataRow dataRow in ds.Tables["last_sale"].Rows)
				{
					decimal price5;
					decimal.TryParse(dataRow["price"].ToString(), out price5);
					long volume;
					long.TryParse(dataRow["volume"].ToString(), out volume);
					this.MainUpdateStockTicker(price5, dataRow["side"].ToString(), volume, dataRow["last_update"].ToString(), num3, sfMain);
					num3++;
				}
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
						this.intzaInfo.Item("tbOpen1").Text = Utilities.PriceFormat(price, sf.NumOfDec);
						this.intzaInfo.Item("tbOpen1").BackColor = Utilities.ComparePriceCFColor(price, sf);
						this.intzaInfo.Item("tbOpen1").FontColor = Color.Black;
					}
					else
					{
						this.intzaInfo.Item("tbOpen1").BackColor = Color.Black;
					}
				}
				else
				{
					if (state == "9")
					{
						if (price != 0m)
						{
							this.intzaInfo.Item("tbOpen2").Text = Utilities.PriceFormat(price, sf.NumOfDec);
							this.intzaInfo.Item("tbOpen2").BackColor = Utilities.ComparePriceCFColor(price, sf);
							this.intzaInfo.Item("tbOpen2").FontColor = Color.Black;
						}
						else
						{
							this.intzaInfo.Item("tbOpen2").BackColor = Color.Black;
						}
					}
					else
					{
						if (state == "11")
						{
							if (price != 0m)
							{
								this.intzaInfo.Item("tbOpen1").Text = Utilities.PriceFormat(price, sf.NumOfDec);
								this.intzaInfo.Item("tbOpen1").BackColor = Color.Black;
								this.intzaInfo.Item("tbOpen1").FontColor = Utilities.ComparePriceCFColor(price, sf);
							}
						}
						else
						{
							if (state == "10")
							{
								if (price != 0m)
								{
									this.intzaInfo.Item("tbOpen2").Text = Utilities.PriceFormat(price, sf.NumOfDec);
									this.intzaInfo.Item("tbOpen2").BackColor = Color.Black;
									this.intzaInfo.Item("tbOpen2").FontColor = Utilities.ComparePriceCFColor(price, sf);
								}
							}
							else
							{
								if (state == "23")
								{
									if (price != 0m)
									{
										this.intzaInfo.Item("tbOpen3").Text = Utilities.PriceFormat(price, sf.NumOfDec);
										this.intzaInfo.Item("tbOpen3").BackColor = Utilities.ComparePriceCFColor(price, sf);
										this.intzaInfo.Item("tbOpen3").FontColor = Color.Black;
									}
									else
									{
										this.intzaInfo.Item("tbOpen3").BackColor = Color.Black;
									}
								}
								else
								{
									if (state == "24")
									{
										if (price != 0m)
										{
											this.intzaInfo.Item("tbOpen3").Text = Utilities.PriceFormat(price, sf.NumOfDec);
											this.intzaInfo.Item("tbOpen3").BackColor = Color.Black;
											this.intzaInfo.Item("tbOpen3").FontColor = Utilities.ComparePriceCFColor(price, sf);
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
		private void StockInPlayUpdateTotalVolumeAndTotalDeals(long buyVolume, long sellVolume, long openVolume)
		{
			try
			{
				long num = buyVolume + sellVolume + openVolume;
				decimal num2 = 0m;
				decimal num3 = 0m;
				if (num > 0L)
				{
					num2 = buyVolume / num * 100m;
					num3 = sellVolume / num * 100m;
				}
				this.intzaStockInPlay.Records(this.maxTopStockInPlay).Fields("buy_deal").Text = this._buyDeals;
				this.intzaStockInPlay.Records(this.maxTopStockInPlay).Fields("sell_deal").Text = this._sellDeals;
				this.intzaStockInPlay.Records(this.maxTopStockInPlay).Fields("buy_volume").Text = Utilities.PriceFormat(num2, "%");
				this.intzaStockInPlay.Records(this.maxTopStockInPlay).Fields("sell_vol").Text = Utilities.PriceFormat(num3, "%");
				this.intzaStockInPlay.Records(this.maxTopStockInPlay).Fields("buy_deal").FontColor = Color.Yellow;
				this.intzaStockInPlay.Records(this.maxTopStockInPlay).Fields("sell_deal").FontColor = Color.Yellow;
				this.intzaStockInPlay.Records(this.maxTopStockInPlay).Fields("buy_volume").FontColor = Color.Yellow;
				this.intzaStockInPlay.Records(this.maxTopStockInPlay).Fields("sell_vol").FontColor = Color.Yellow;
				this.intzaStockInPlay.Records(this.maxTopStockInPlay).BackColor = Color.FromArgb(45, 45, 45);
				this.intzaStockInPlay.Records(this.maxTopStockInPlay).Changed = true;
			}
			catch (Exception ex)
			{
				this.ShowError("StockInPlayUpdateTotalVolumeAndTotalDeals", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void StockInPlayUpdateBuySellVolume(string side, decimal price, long volume, int deals)
		{
			try
			{
				RecordItem recordItem = this.intzaStockInPlay.Find("price", Utilities.PriceFormat(price));
				if (recordItem != null)
				{
					int num = 0;
					decimal value = 0m;
					Color fontColor = recordItem.Fields("price").FontColor;
					if (side == "B")
					{
						int.TryParse(recordItem.Fields("buy_deal").Text.ToString().Replace(",", ""), out num);
						recordItem.Fields("buy_deal").Text = num + deals;
						decimal.TryParse(recordItem.Fields("buy_volume").Text.ToString().Replace(",", ""), out value);
						recordItem.Fields("buy_volume").Text = Utilities.VolumeFormat((long)value + volume, true);
						recordItem.Fields("buy_deal").FontColor = fontColor;
						recordItem.Fields("buy_volume").FontColor = fontColor;
						if (!base.IsLoadingData)
						{
							recordItem.Fields("offer").IsBlink = 3;
						}
					}
					else
					{
						if (side == "S")
						{
							int.TryParse(recordItem.Fields("sell_deal").Text.ToString().Replace(",", ""), out num);
							recordItem.Fields("sell_deal").Text = num + deals;
							decimal.TryParse(recordItem.Fields("sell_vol").Text.ToString().Replace(",", ""), out value);
							recordItem.Fields("sell_vol").Text = Utilities.VolumeFormat((long)value + volume, true);
							recordItem.Fields("sell_vol").FontColor = fontColor;
							recordItem.Fields("sell_deal").FontColor = fontColor;
							if (!base.IsLoadingData)
							{
								recordItem.Fields("bid").IsBlink = 3;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("StockInPlayUpdateBuySellVolume", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void StockInPlayUpdateTopPrice(string side, decimal price1, decimal price2, decimal price3, decimal price4, decimal price5, long volume1, long volume2, long volume3, long volume4, long volume5)
		{
			try
			{
				string columnName = string.Empty;
				if (side == "B")
				{
					columnName = "bid";
				}
				else
				{
					if (side == "S")
					{
						columnName = "offer";
					}
				}
				if (price1 > -1m)
				{
					decimal num = (side == "B") ? this._tfexBidPrice1 : this._tfexAskPrice1;
					if (price1 != num)
					{
						RecordItem recordItem = this.intzaStockInPlay.Find("price", Utilities.PriceFormat(num));
						if (recordItem != null)
						{
							recordItem.Fields(columnName).Text = string.Empty;
						}
					}
				}
				if (price2 > -1m)
				{
					decimal num = (side == "B") ? this._tfexBidPrice2 : this._tfexAskPrice2;
					if (price2 != num)
					{
						RecordItem recordItem = this.intzaStockInPlay.Find("price", Utilities.PriceFormat(num));
						if (recordItem != null)
						{
							recordItem.Fields(columnName).Text = string.Empty;
						}
					}
				}
				if (price3 > -1m)
				{
					decimal num = (side == "B") ? this._tfexBidPrice3 : this._tfexAskPrice3;
					if (price3 != num)
					{
						RecordItem recordItem = this.intzaStockInPlay.Find("price", Utilities.PriceFormat(num));
						if (recordItem != null)
						{
							recordItem.Fields(columnName).Text = string.Empty;
						}
					}
				}
				if (price4 > -1m)
				{
					decimal num = (side == "B") ? this._tfexBidPrice4 : this._tfexAskPrice4;
					if (price4 != num)
					{
						RecordItem recordItem = this.intzaStockInPlay.Find("price", Utilities.PriceFormat(num));
						if (recordItem != null)
						{
							recordItem.Fields(columnName).Text = string.Empty;
						}
					}
				}
				if (price5 > -1m)
				{
					decimal num = (side == "B") ? this._tfexBidPrice5 : this._tfexAskPrice5;
					if (price5 != num)
					{
						RecordItem recordItem = this.intzaStockInPlay.Find("price", Utilities.PriceFormat(num));
						if (recordItem != null)
						{
							recordItem.Fields(columnName).Text = string.Empty;
						}
					}
				}
				if (volume1 != -1L)
				{
					if (price1 > 0m)
					{
						RecordItem recordItem = this.intzaStockInPlay.Find("price", Utilities.PriceFormat(price1));
						if (recordItem != null)
						{
							recordItem.Fields(columnName).Text = volume1;
							recordItem.Fields(columnName).FontColor = recordItem.Fields("price").FontColor;
						}
					}
					if (side == "B")
					{
						this._tfexBidPrice1 = price1;
					}
					else
					{
						this._tfexAskPrice1 = price1;
					}
				}
				if (volume2 != -1L)
				{
					if (price2 > 0m)
					{
						RecordItem recordItem = this.intzaStockInPlay.Find("price", Utilities.PriceFormat(price2));
						if (recordItem != null)
						{
							recordItem.Fields(columnName).Text = volume2;
							recordItem.Fields(columnName).FontColor = recordItem.Fields("price").FontColor;
						}
					}
					if (side == "B")
					{
						this._tfexBidPrice2 = price2;
					}
					else
					{
						this._tfexAskPrice2 = price2;
					}
				}
				if (volume3 != -1L)
				{
					if (price3 > 0m)
					{
						RecordItem recordItem = this.intzaStockInPlay.Find("price", Utilities.PriceFormat(price3));
						if (recordItem != null)
						{
							recordItem.Fields(columnName).Text = volume3;
							recordItem.Fields(columnName).FontColor = recordItem.Fields("price").FontColor;
						}
					}
					if (side == "B")
					{
						this._tfexBidPrice3 = price3;
					}
					else
					{
						this._tfexAskPrice3 = price3;
					}
				}
				if (volume4 != -1L)
				{
					if (price4 > 0m)
					{
						RecordItem recordItem = this.intzaStockInPlay.Find("price", Utilities.PriceFormat(price4));
						if (recordItem != null)
						{
							recordItem.Fields(columnName).Text = volume4;
							recordItem.Fields(columnName).FontColor = recordItem.Fields("price").FontColor;
						}
					}
					if (side == "B")
					{
						this._tfexBidPrice4 = price4;
					}
					else
					{
						this._tfexAskPrice4 = price4;
					}
				}
				if (volume5 != -1L)
				{
					if (price5 > 0m)
					{
						RecordItem recordItem = this.intzaStockInPlay.Find("price", Utilities.PriceFormat(price5));
						if (recordItem != null)
						{
							recordItem.Fields(columnName).Text = volume5;
							recordItem.Fields(columnName).FontColor = recordItem.Fields("price").FontColor;
						}
					}
					if (side == "B")
					{
						this._tfexBidPrice5 = price5;
					}
					else
					{
						this._tfexAskPrice5 = price5;
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("StockInPlayUpdateTopPrice", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void MainUpdatePrice(decimal lastPrice, decimal high, decimal low, decimal accValue, long accVolume, StockList.StockInformation sf)
		{
			try
			{
				decimal num = 0m;
				decimal num2 = 0m;
				Color fontColor = Color.Yellow;
				if (accVolume > 0L)
				{
					accVolume *= (long)sf.BoardLot;
					decimal price = Math.Round(accValue / accVolume, 2);
					this.intzaInfo.Item("tbAverage").Text = price.ToString();
					this.intzaInfo.Item("tbAverage").FontColor = Utilities.ComparePriceCFColor(price, sf);
				}
				if (sf.PriorPrice > 0m && lastPrice > 0m)
				{
					num = lastPrice - sf.PriorPrice;
					num2 = (lastPrice - sf.PriorPrice) / sf.PriorPrice * 100m;
					fontColor = Utilities.ComparePriceColor(num, 0m);
				}
				this.intzaInfo.Item("tbChange").Text = Utilities.PriceFormat(num, true, "");
				this.intzaInfo.Item("tbChangePct").Text = Utilities.PriceFormat(num2, true, "%");
				this.intzaInfo.Item("tbChange").FontColor = fontColor;
				this.intzaInfo.Item("tbChangePct").FontColor = fontColor;
				this.intzaInfo.Item("tbHigh").Text = high.ToString();
				this.intzaInfo.Item("tbLow").Text = low.ToString();
				this.intzaInfo.Item("tbHigh").FontColor = Utilities.ComparePriceCFColor(high, sf);
				this.intzaInfo.Item("tbLow").FontColor = Utilities.ComparePriceCFColor(low, sf);
			}
			catch (Exception ex)
			{
				this.ShowError("MainUpdateLastSalePrice", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void MainUpdateOpenOrProjectOpenPrice(string state, int session, decimal price, StockList.StockInformation sf)
		{
			try
			{
				if (state == "P")
				{
					Color backColor = Utilities.ComparePriceCFColor(price, this._stockInfo);
					if (session == 1)
					{
						if (price > 0m)
						{
							this.intzaInfo.Item("tbOpen1").Text = price.ToString();
							this.intzaInfo.Item("tbOpen1").BackColor = backColor;
							this.intzaInfo.Item("tbOpen1").FontColor = Color.Black;
						}
						else
						{
							this.intzaInfo.Item("tbOpen1").BackColor = this.intzaInfo.BackColor;
						}
					}
					else
					{
						if (session == 2)
						{
							if (price > 0m)
							{
								this.intzaInfo.Item("tbOpen2").Text = price.ToString();
								this.intzaInfo.Item("tbOpen2").BackColor = backColor;
								this.intzaInfo.Item("tbOpen2").FontColor = Color.Black;
							}
							else
							{
								this.intzaInfo.Item("tbOpen2").BackColor = this.intzaInfo.BackColor;
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
							this.intzaInfo.Item("tbOpen1").Text = price.ToString();
							this.intzaInfo.Item("tbOpen1").BackColor = this.intzaInfo.BackColor;
							this.intzaInfo.Item("tbOpen1").FontColor = Utilities.ComparePriceCFColor(price, sf);
						}
						else
						{
							if (session == 2)
							{
								this.intzaInfo.Item("tbOpen2").Text = price.ToString();
								this.intzaInfo.Item("tbOpen2").BackColor = this.intzaInfo.BackColor;
								this.intzaInfo.Item("tbOpen2").FontColor = Utilities.ComparePriceCFColor(price, sf);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("MainUpdateOpenOrProjectOpenPrice", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void MainUpdateAllVolume(int deals, long accVolume, decimal accValue, long openVolume, long buyVolume, long sellVolume)
		{
			try
			{
				this.intzaInfo.Item("tbOpenVolume").Text = openVolume.ToString();
				this.intzaInfo.Item("tbBuyVolume").Text = buyVolume.ToString();
				this.intzaInfo.Item("tbSellVolume").Text = sellVolume.ToString();
				this.intzaInfo.Item("tbMDeal").Text = deals.ToString();
				this.intzaInfo.Item("tbMVolume").Text = accVolume.ToString();
				if (accValue >= 100000000m)
				{
					this.intzaInfo.Item("tbMValue").Text = Utilities.VolumeFormat(accValue / 1000m, true) + "K";
				}
				else
				{
					this.intzaInfo.Item("tbMValue").Text = Utilities.VolumeFormat(accValue, true);
				}
				decimal num = 0m;
				decimal num2 = 0m;
				decimal num3 = 0m;
				if (accVolume > 0L)
				{
					num = openVolume / accVolume * 100m;
					num2 = buyVolume / accVolume * 100m;
					num3 = 100m - num2 - num;
				}
				this.intzaInfo.Item("tbOpenVolPct").Text = Utilities.PriceFormat(num, "%");
				this.intzaInfo.Item("tbBuyVolPct").Text = Utilities.PriceFormat(num2, "%");
				this.intzaInfo.Item("tbSellVolPct").Text = Utilities.PriceFormat(num3, "%");
				this.intzaInfo.Item("pie").Text = string.Concat(new string[]
				{
					num.ToString("0.00"),
					";",
					num2.ToString("0.00"),
					";",
					num3.ToString("0.00")
				});
			}
			catch (Exception ex)
			{
				this.ShowError("MainUpdateAllVolume", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void MainUpdateBigLotValue(decimal biglotValue)
		{
			try
			{
				this.intzaInfo.Item("tbBigValue").Text = biglotValue.ToString();
			}
			catch (Exception ex)
			{
				this.ShowError("MainUpdateBigLotValue", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ViewOddLotMktTotal(decimal value, long volume)
		{
			try
			{
				this.intzaViewOddLotInfo.Item("totvalue").Text = value.ToString();
				this.intzaViewOddLotInfo.Item("totvolume").Text = volume.ToString();
			}
			catch (Exception ex)
			{
				this.ShowError("MainUpdateOddLotValue", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void MainUpdateStockTicker(decimal price, string side, long volume, string lastUpdate, int index, StockList.StockInformation sf)
		{
			try
			{
				RecordItem recordItem;
				if (index == -1)
				{
					recordItem = this.intzaLS.AddRecord(1, true);
				}
				else
				{
					recordItem = this.intzaLS.Records(index);
				}
				recordItem.Fields("volume").Text = volume.ToString();
				recordItem.Fields("side").Text = side;
				recordItem.Fields("price").Text = Utilities.PriceFormat(price);
				recordItem.Fields("time").Text = Utilities.GetTime(lastUpdate);
				Color fontColor = Utilities.ComparePriceCFColor(price, sf);
				recordItem.Fields("price").FontColor = fontColor;
				fontColor = Color.Yellow;
				if (side == "B")
				{
					fontColor = Color.Cyan;
				}
				else
				{
					if (side == "S")
					{
						fontColor = Color.Magenta;
					}
				}
				recordItem.Fields("side").FontColor = fontColor;
				recordItem.Fields("volume").FontColor = fontColor;
				recordItem.Fields("time").FontColor = Color.LightGray;
				recordItem.Changed = true;
			}
			catch (Exception ex)
			{
				this.ShowError("MainUpdateLastSaleTicker", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SaleByPrice_SetNewStock(string symbol, bool isForce)
		{
			try
			{
				if (symbol != string.Empty && (isForce || symbol != this._currentSymbol))
				{
					StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[symbol];
					if (stockInformation.Number > 0)
					{
						this._stockInfo = stockInformation;
						this._seriesInfo = null;
						ApplicationInfo.CurrentSymbol = this._stockInfo.Symbol;
						this._currentSymbol = this._stockInfo.Symbol;
						this.SaleByPriceReloadData();
					}
					else
					{
						SeriesList.SeriesInformation seriesInformation = ApplicationInfo.SeriesInfo[symbol];
						if (seriesInformation.Symbol != string.Empty)
						{
							this._isNewStock = true;
							this._currentSymbol = seriesInformation.Symbol;
							this._seriesInfo = seriesInformation;
							this._stockInfo = null;
							ApplicationInfo.CurrentSymbol = this._seriesInfo.Symbol;
							this.SaleByPriceReloadData();
						}
					}
				}
				if (this.tscbStock.Text != ApplicationInfo.CurrentSymbol)
				{
					this.tscbStock.Text = ApplicationInfo.CurrentSymbol;
				}
				this.tscbStock.Focus();
				this.tscbStock.SelectAll();
			}
			catch (Exception ex)
			{
				this.ShowError("SaleByPriceSetNewStock", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SaleByPriceReloadData()
		{
			if (!this.bgwSaleByPriceLoadData.IsBusy)
			{
				this.bgwSaleByPriceLoadData.RunWorkerAsync();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SaleByPriceUpdateToControl()
		{
			try
			{
				this.intzaSaleByPrice.BeginUpdate();
				this.intzaLS.BeginUpdate();
				this.intzaLS.ClearAllText();
				if (this.dsSaleByPrice.Tables.Count > 0)
				{
					if (this.dsSaleByPrice.Tables.Contains("sale_by_price"))
					{
						this.intzaSaleByPrice.Rows = 0;
						this.wcGraphVolume.InitData(this._stockInfo.Symbol, (double)this._stockInfo.PriorPrice, (double)this._stockInfo.LastSalePrice, (double)this._stockInfo.Ceiling, (double)this._stockInfo.Floor);
						foreach (DataRow dataRow in this.dsSaleByPrice.Tables["sale_by_price"].Rows)
						{
							decimal num;
							decimal.TryParse(dataRow["price"].ToString(), out num);
							int sideDeals;
							int.TryParse(dataRow["buy_deals"].ToString(), out sideDeals);
							long num2;
							long.TryParse(dataRow["buy_volume"].ToString(), out num2);
							this.SaleByPriceUpdateBuySell(num, "B", sideDeals, num2, true);
							int.TryParse(dataRow["sell_deals"].ToString(), out sideDeals);
							long num3;
							long.TryParse(dataRow["sell_volume"].ToString(), out num3);
							this.SaleByPriceUpdateBuySell(num, "S", sideDeals, num3, true);
							long num4;
							long.TryParse(dataRow["other_volume"].ToString(), out num4);
							this.SaleByPriceUpdateBuySell(num, "", 0, num4, true);
							this.wcGraphVolume.InputData((double)num, num2 + num3 + num4, num2, num3);
						}
						this.wcGraphVolume.EndUpdate();
						this.wcGraphVolume.Sort();
					}
					this.intzaSaleByPrice.Sort("price", SortType.Desc);
					this.UpdateStockInfo(this.dsSaleByPrice, this._stockInfo);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SaleByPriceUpdateToControl", ex);
			}
			finally
			{
				this.intzaSaleByPrice.Redraw();
				this.intzaLS.Redraw();
				this.intzaInfo.Redraw();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool SaleByPriceUpdateBuySell(decimal price, string side, int sideDeals, long sideVolume, bool isNoBlink)
		{
			bool result;
			if (this.intzaSaleByPrice.InvokeRequired)
			{
				result = (bool)this.intzaSaleByPrice.Invoke(new frmStockSummary.SaleByPriceUpdateBuySellCallBack(this.SaleByPriceUpdateBuySell), new object[]
				{
					price,
					side,
					sideDeals,
					sideVolume,
					isNoBlink
				});
			}
			else
			{
				bool flag = false;
				try
				{
					long num = 0L;
					long num2 = 0L;
					long num3 = 0L;
					long num4 = 0L;
					long num5 = 0L;
					Color fontColor = Color.Yellow;
					string text = Utilities.PriceFormat(price);
					if (this._seriesInfo != null)
					{
						text = Utilities.PriceFormat(price, this._seriesInfo.NumOfDec);
					}
					RecordItem recordItem = this.intzaSaleByPrice.Find("price", text);
					if (recordItem == null)
					{
						flag = true;
						recordItem = this.intzaSaleByPrice.AddRecord(1, false);
						recordItem.Fields("price").Text = text;
						recordItem.Fields("price").BackColor = Color.FromArgb(30, 30, 30);
						if (this._stockInfo != null)
						{
							fontColor = Utilities.ComparePriceCFColor(price, this._stockInfo);
						}
						else
						{
							fontColor = Utilities.ComparePriceCFColor(price, this._seriesInfo);
						}
						recordItem.Fields("buy_vol").FontColor = fontColor;
						recordItem.Fields("buy_deal").FontColor = fontColor;
						recordItem.Fields("sell_vol").FontColor = fontColor;
						recordItem.Fields("sell_deal").FontColor = fontColor;
						recordItem.Fields("price").FontColor = fontColor;
						recordItem.Fields("mvol").FontColor = fontColor;
						recordItem.Fields("mval").FontColor = fontColor;
					}
					string s = string.Empty;
					s = recordItem.Fields("mvol").Text.ToString().Replace(",", "");
					long.TryParse(s, out num5);
					s = recordItem.Fields("buy_deal").Text.ToString().Replace(",", "");
					long.TryParse(s, out num2);
					s = recordItem.Fields("buy_vol").Text.ToString().Replace(",", "");
					long.TryParse(s, out num);
					s = recordItem.Fields("sell_deal").Text.ToString().Replace(",", "");
					long.TryParse(s, out num4);
					s = recordItem.Fields("sell_vol").Text.ToString().Replace(",", "");
					long.TryParse(s, out num3);
					if (side == "B")
					{
						recordItem.Fields("buy_vol").Text = num + sideVolume;
						recordItem.Fields("buy_deal").Text = num2 + (long)sideDeals;
					}
					else
					{
						if (side == "S")
						{
							recordItem.Fields("sell_vol").Text = num3 + sideVolume;
							recordItem.Fields("sell_deal").Text = num4 + (long)sideDeals;
						}
					}
					if (isNoBlink)
					{
						recordItem.Fields("mvol").Text = "";
						recordItem.Fields("mval").Text = "";
					}
					recordItem.Fields("mvol").Text = num5 + sideVolume;
					recordItem.Fields("mval").Text = (num5 + sideVolume) * price;
					recordItem.Changed = true;
				}
				catch (Exception ex)
				{
					this.ShowError("SaleByPriceUpdateBuySell", ex);
				}
				result = flag;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SaleByTime_SetNewStock(string stockSymbol, bool isForce)
		{
			try
			{
				if (stockSymbol != string.Empty)
				{
					if (stockSymbol != string.Empty && (this._stockInfo == null || isForce || (this._stockInfo != null && stockSymbol != this._stockInfo.Symbol)))
					{
						StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[stockSymbol];
						if (stockInformation.Number > 0)
						{
							this._seriesInfo = null;
							this._stockInfo = stockInformation;
							ApplicationInfo.CurrentSymbol = this._stockInfo.Symbol;
							this.saleByTimePageNo = 1;
							this.SaleByTimeReloadData();
						}
						else
						{
							SeriesList.SeriesInformation seriesInformation = ApplicationInfo.SeriesInfo[stockSymbol];
							if (seriesInformation != null && seriesInformation.Symbol != string.Empty)
							{
								this._stockInfo = null;
								this._seriesInfo = seriesInformation;
								ApplicationInfo.CurrentSymbol = this._seriesInfo.Symbol;
								this.saleByTimePageNo = 1;
								this.SaleByTimeReloadData();
							}
						}
					}
				}
				if (this.tscbStock.Text != ApplicationInfo.CurrentSymbol)
				{
					this.tscbStock.Text = ApplicationInfo.CurrentSymbol;
				}
				this.tscbStock.Focus();
				this.tscbStock.SelectAll();
			}
			catch (Exception ex)
			{
				this.ShowError("SaleByTimeSetNewStock", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SaleByTimeReloadData()
		{
			if (!this.bgwSaleByTimeLoadData.IsBusy)
			{
				this.bgwSaleByTimeLoadData.RunWorkerAsync();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SaleByTimeUpdateToControl()
		{
			try
			{
				this.tslblSaleByTimePageNo.Text = this.saleByTimePageNo.ToString();
				this.intzaSaleByTime.BeginUpdate();
				this.intzaSaleByTime.ClearAllText();
				this.intzaLS.BeginUpdate();
				this.intzaLS.ClearAllText();
				if (this.dsSaleByTime != null && this.dsSaleByTime.Tables.Contains("last_sale_time"))
				{
					int num = 0;
					foreach (DataRow dataRow in this.dsSaleByTime.Tables["last_sale_time"].Rows)
					{
						decimal price;
						decimal.TryParse(dataRow["price"].ToString(), out price);
						long volume;
						long.TryParse(dataRow["volume"].ToString(), out volume);
						decimal chg;
						decimal.TryParse(dataRow["change_price"].ToString(), out chg);
						decimal avg;
						decimal.TryParse(dataRow["average_price"].ToString(), out avg);
						if (!this.SaleByTimeUpdateGridData(dataRow["side"].ToString(), price, volume, chg, avg, dataRow["last_update"].ToString(), num))
						{
							break;
						}
						num++;
					}
					this.UpdateStockInfo(this.dsSaleByTime, this._stockInfo);
				}
				this.intzaSaleByTime.Redraw();
				this.intzaLS.Redraw();
				this.intzaInfo.Redraw();
			}
			catch (Exception ex)
			{
				this.ShowError("SaleByTimeUpdateToControl", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SaleByTimeUpdateToControl_TFEX()
		{
			try
			{
				this.tslblSaleByTimePageNo.Text = this.saleByTimePageNo.ToString();
				this.intzaSaleByTime.BeginUpdate();
				this.intzaSaleByTime.ClearAllText();
				if (this.dsSaleByTime.Tables.Count > 0)
				{
					if (this.dsSaleByTime.Tables.Contains("last_sale_time_tfex"))
					{
						int num = 0;
						foreach (DataRow dataRow in this.dsSaleByTime.Tables["last_sale_time_tfex"].Rows)
						{
							string side;
							if (dataRow["sSide"].ToString().Trim() == "B")
							{
								side = "B";
							}
							else
							{
								if (dataRow["sSide"].ToString().Trim() == "S")
								{
									side = "S";
								}
								else
								{
									side = "";
								}
							}
							long volume;
							long.TryParse(dataRow["iQuantity"].ToString(), out volume);
							decimal num2;
							decimal.TryParse(dataRow["nmrPrice"].ToString(), out num2);
							decimal avg;
							decimal.TryParse(dataRow["nmrAvgPrice"].ToString(), out avg);
							bool flag = this.SaleByTimeUpdateGridData_TFEX(side, num2, volume, num2 - this._seriesInfo.PrevFixPrice, avg, Utilities.GetTimeLastSale(dataRow["dtLastUpd"].ToString()), num);
							num++;
							if (!flag)
							{
								break;
							}
						}
					}
					if (this.dsSaleByTime.Tables.Contains("series_info_stat") && this.dsSaleByTime.Tables["series_info_stat"].Rows.Count > 0)
					{
						DataRow dataRow = this.dsSaleByTime.Tables["series_info_stat"].Rows[0];
						decimal num3;
						decimal.TryParse(dataRow["PrevFixing"].ToString(), out num3);
						decimal num4;
						decimal.TryParse(dataRow["FixingPrice"].ToString(), out num4);
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
						int deals;
						int.TryParse(dataRow["NumOfDeal"].ToString(), out deals);
						int num11;
						int.TryParse(dataRow["TurnOverQty"].ToString(), out num11);
						decimal accValue;
						decimal.TryParse(dataRow["TurnOverValue"].ToString(), out accValue);
						long openVolume;
						long.TryParse(dataRow["TotalOpenQty"].ToString(), out openVolume);
						long buyVolume;
						long.TryParse(dataRow["LongQty"].ToString(), out buyVolume);
						long sellVolume;
						long.TryParse(dataRow["ShortQty"].ToString(), out sellVolume);
						decimal num12;
						decimal.TryParse(dataRow["Multiplier"].ToString(), out num12);
						this.intzaInfo.Item("tbCeiling").Text = Utilities.PriceFormat(dataRow["CeilingPrice"].ToString(), this._seriesInfo.NumOfDec);
						this.intzaInfo.Item("tbFloor").Text = Utilities.PriceFormat(dataRow["FloorPrice"].ToString(), this._seriesInfo.NumOfDec);
						this.intzaInfo.Item("tbPrior").Text = Utilities.PriceFormat(num3, this._seriesInfo.NumOfDec);
						decimal lastPrice;
						decimal.TryParse(dataRow["LastPrice"].ToString(), out lastPrice);
						decimal high;
						decimal.TryParse(dataRow["HighPrice"].ToString(), out high);
						decimal low;
						decimal.TryParse(dataRow["LowPrice"].ToString(), out low);
						decimal accValue2;
						decimal.TryParse(dataRow["TurnOverValue"].ToString(), out accValue2);
						long accVolume;
						long.TryParse(dataRow["TurnOverQty"].ToString(), out accVolume);
						this.MainUpdateLastSalePrice_TFEX(lastPrice, high, low, accValue2, accVolume, this._seriesInfo);
						if (ApplicationInfo.IndexInfoTfex.TXISession == 1)
						{
							if (num8 != 0m)
							{
								this.UpdateOpenOrProjectOpenPriceTFEX("11", num8, this._seriesInfo);
							}
							else
							{
								if (num5 != 0m && ApplicationInfo.IndexInfoTfex.TXIState == "7")
								{
									this.UpdateOpenOrProjectOpenPriceTFEX("7", num5, this._seriesInfo);
								}
								else
								{
									this.intzaInfo.Item("tbOpen1").BackColor = Color.Black;
								}
							}
						}
						else
						{
							if (ApplicationInfo.IndexInfoTfex.TXISession == 2)
							{
								if (num8 != 0m)
								{
									this.UpdateOpenOrProjectOpenPriceTFEX("11", num8, this._seriesInfo);
								}
								if (num9 != 0m)
								{
									this.UpdateOpenOrProjectOpenPriceTFEX("10", num9, this._seriesInfo);
								}
								else
								{
									if (num6 != 0m && ApplicationInfo.IndexInfoTfex.TXIState == "9")
									{
										this.UpdateOpenOrProjectOpenPriceTFEX("9", num6, this._seriesInfo);
									}
									else
									{
										this.intzaInfo.Item("tbOpen2").BackColor = Color.Black;
									}
								}
								if (num10 != 0m)
								{
									this.UpdateOpenOrProjectOpenPriceTFEX("24", num10, this._seriesInfo);
								}
								else
								{
									if (num7 != 0m && ApplicationInfo.IndexInfoTfex.TXMState == "23")
									{
										this.UpdateOpenOrProjectOpenPriceTFEX("23", num7, this._seriesInfo);
									}
									else
									{
										this.intzaInfo.Item("tbOpen3").BackColor = Color.Black;
									}
								}
							}
						}
						this.MainUpdateAllVolume(deals, (long)num11, accValue, openVolume, buyVolume, sellVolume);
					}
					if (this.dsSaleByTime.Tables.Contains("last_sale_tfex"))
					{
						for (int i = 0; i < this.dsSaleByTime.Tables["last_sale_tfex"].Rows.Count; i++)
						{
							DataRow dataRow2 = this.dsSaleByTime.Tables["last_sale_tfex"].Rows[i];
							decimal num2;
							decimal.TryParse(dataRow2["nmrPrice"].ToString(), out num2);
							long volume2;
							long.TryParse(dataRow2["iQuantity"].ToString(), out volume2);
							this.UpdateLastSaleTicker_TFEX(num2, dataRow2["sSide"].ToString(), volume2, dataRow2["dtLastUpd"].ToString(), i, this._seriesInfo);
						}
					}
				}
				this.intzaSaleByTime.Redraw();
				this.intzaLS.Redraw();
				this.intzaInfo.Redraw();
			}
			catch (Exception ex)
			{
				this.ShowError("SaleByTimeUpdateToControl", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool SaleByTimeUpdateGridData(string side, decimal price, long volume, decimal chg, decimal avg, string last_update, int rowId)
		{
			bool result;
			try
			{
				RecordItem recordItem;
				if (rowId == -1)
				{
					recordItem = this.intzaSaleByTime.AddRecord(1, true);
				}
				else
				{
					recordItem = this.intzaSaleByTime.Records(rowId);
				}
				Color fontColor = Utilities.ComparePriceCFColor(price, this._stockInfo);
				recordItem.Fields("side").Text = side;
				recordItem.Fields("volume").Text = volume;
				recordItem.Fields("price").Text = price;
				recordItem.Fields("chg").Text = chg;
				recordItem.Fields("avg").Text = Utilities.PriceFormat(avg, 4);
				recordItem.Fields("time").Text = last_update;
				Color fontColor2 = Color.Yellow;
				if (side == "B")
				{
					fontColor2 = Color.Cyan;
				}
				else
				{
					if (side == "S")
					{
						fontColor2 = Color.Magenta;
					}
				}
				recordItem.Fields("side").FontColor = fontColor2;
				recordItem.Fields("volume").FontColor = fontColor2;
				recordItem.Fields("price").FontColor = fontColor;
				recordItem.Fields("time").FontColor = Color.LightGray;
				recordItem.Fields("chg").FontColor = fontColor;
				recordItem.Fields("avg").FontColor = Utilities.ComparePriceCFColor(avg, this._stockInfo);
				recordItem.Changed = true;
			}
			catch (Exception ex)
			{
				this.ShowError("SaleByTimeUpdateGridData", ex);
				result = false;
				return result;
			}
			result = true;
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool SaleByTimeUpdateGridData_TFEX(string side, decimal price, long volume, decimal chg, decimal avg, string last_update, int rowId)
		{
			bool result;
			try
			{
				RecordItem recordItem;
				if (rowId == -1)
				{
					recordItem = this.intzaSaleByTime.AddRecord(1, true);
				}
				else
				{
					recordItem = this.intzaSaleByTime.Records(rowId);
				}
				Color fontColor = Utilities.ComparePriceCFColor(price, this._seriesInfo);
				recordItem.Fields("side").Text = side;
				recordItem.Fields("volume").Text = volume;
				recordItem.Fields("price").Text = Utilities.PriceFormat(price, this._seriesInfo.NumOfDec);
				recordItem.Fields("chg").Text = Utilities.PriceFormat(chg, this._seriesInfo.NumOfDec);
				recordItem.Fields("avg").Text = Utilities.PriceFormat(avg, this._seriesInfo.NumOfDec);
				recordItem.Fields("time").Text = Utilities.GetTime(last_update);
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
				}
				recordItem.Fields("price").FontColor = fontColor;
				recordItem.Fields("time").FontColor = fontColor;
				recordItem.Fields("chg").FontColor = fontColor;
				recordItem.Fields("avg").FontColor = Utilities.ComparePriceCFColor(avg, this._seriesInfo);
				recordItem.Changed = true;
			}
			catch (Exception ex)
			{
				this.ShowError("SaleByTimeUpdateGridData_TFEX", ex);
				result = false;
				return result;
			}
			result = true;
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ViewOddLotSetNewStock(string stockSymbol, bool isForce)
		{
			try
			{
				if (stockSymbol != string.Empty && (this._stockInfo == null || isForce || (this._stockInfo != null && stockSymbol != this._stockInfo.Symbol)))
				{
					StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[stockSymbol + "_ODD"];
					if (stockInformation.Number > 0)
					{
						this._seriesInfo = null;
						this._stockInfo = stockInformation;
						ApplicationInfo.CurrentSymbol = stockSymbol;
						this.ViewOddLotReloadData();
					}
					else
					{
						this._stockInfo = null;
						this._seriesInfo = null;
						this.intzaLS.ClearAllText();
						this.intzaViewOddLot.ClearAllText();
						this.intzaViewOddLotInfo.ClearAllText();
						this.intzaLS.Redraw();
						this.intzaViewOddLot.Redraw();
						this.intzaViewOddLotInfo.Redraw();
					}
				}
				if (this.tscbStock.Text != ApplicationInfo.CurrentSymbol)
				{
					this.tscbStock.Text = ApplicationInfo.CurrentSymbol;
				}
				this.tscbStock.Focus();
				this.tscbStock.SelectAll();
			}
			catch (Exception ex)
			{
				this.ShowError("ViewOddLotSetNewStock", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ViewOddLotReloadData()
		{
			if (this._stockInfo != null && this._stockInfo.Number > 0)
			{
				if (!this.bgwViewOddLotLoadData.IsBusy)
				{
					this.bgwViewOddLotLoadData.RunWorkerAsync(this._stockInfo.Number);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ViewOddLotUpdateToControl(DataSet dsViewOddLot)
		{
			try
			{
				this.intzaViewOddLot.BeginUpdate();
				this.intzaViewOddLot.ClearAllText();
				this.intzaViewOddLotInfo.BeginUpdate();
				this.intzaViewOddLotInfo.ClearAllText();
				this.intzaLS.ClearAllText();
				this.intzaInfo.ClearAllText();
				this.intzaViewOddLotInfo.Item("ceiling").Text = this._stockInfo.Ceiling.ToString();
				this.intzaViewOddLotInfo.Item("floor").Text = this._stockInfo.Floor.ToString();
				this.intzaViewOddLotInfo.Item("prior").Text = this._stockInfo.PriorPrice.ToString();
				if (dsViewOddLot.Tables.Count > 0)
				{
					if (dsViewOddLot.Tables.Contains("oddlot_info_stat") && dsViewOddLot.Tables["oddlot_info_stat"].Rows.Count > 0)
					{
						DataRow dataRow = dsViewOddLot.Tables["oddlot_info_stat"].Rows[0];
						int deal;
						int.TryParse(dataRow["deal_in_oddlot"].ToString(), out deal);
						long volume;
						long.TryParse(dataRow["oddlot_accvolume"].ToString(), out volume);
						decimal value;
						decimal.TryParse(dataRow["oddlot_accvalue"].ToString(), out value);
						decimal lastPrice;
						decimal.TryParse(dataRow["last_sale_price"].ToString(), out lastPrice);
						this.ViewOddLotShowStockVolume(lastPrice, deal, volume, value);
						this.ViewOddLot_TopPrice("B", Convert.ToDecimal(dataRow["bid_price1"].ToString()), Convert.ToDecimal(dataRow["bid_price2"].ToString()), Convert.ToDecimal(dataRow["bid_price3"].ToString()), Convert.ToDecimal(dataRow["bid_price4"].ToString()), Convert.ToDecimal(dataRow["bid_price5"].ToString()), Convert.ToInt64(dataRow["bid_volume1"].ToString()), Convert.ToInt64(dataRow["bid_volume2"].ToString()), Convert.ToInt64(dataRow["bid_volume3"].ToString()), Convert.ToInt64(dataRow["bid_volume4"].ToString()), Convert.ToInt64(dataRow["bid_volume5"].ToString()));
						this.ViewOddLot_TopPrice("S", Convert.ToDecimal(dataRow["offer_price1"].ToString()), Convert.ToDecimal(dataRow["offer_price2"].ToString()), Convert.ToDecimal(dataRow["offer_price3"].ToString()), Convert.ToDecimal(dataRow["offer_price4"].ToString()), Convert.ToDecimal(dataRow["offer_price5"].ToString()), Convert.ToInt64(dataRow["offer_volume1"].ToString()), Convert.ToInt64(dataRow["offer_volume2"].ToString()), Convert.ToInt64(dataRow["offer_volume3"].ToString()), Convert.ToInt64(dataRow["offer_volume4"].ToString()), Convert.ToInt64(dataRow["offer_volume5"].ToString()));
					}
					if (dsViewOddLot.Tables.Contains("last_sale"))
					{
						this.intzaLS.Rows = dsViewOddLot.Tables["last_sale"].Rows.Count;
						int num = 0;
						foreach (DataRow dataRow in dsViewOddLot.Tables["last_sale"].Rows)
						{
							decimal price;
							decimal.TryParse(dataRow["price"].ToString(), out price);
							long volume2;
							long.TryParse(dataRow["volume"].ToString(), out volume2);
							this.MainUpdateStockTicker(price, dataRow["side"].ToString(), volume2, dataRow["last_update"].ToString(), num, this._stockInfo);
							num++;
						}
					}
					if (dsViewOddLot.Tables.Contains("market_info") && dsViewOddLot.Tables["market_info"].Rows.Count > 0)
					{
						this.ViewOddLotMktTotal(Convert.ToDecimal(dsViewOddLot.Tables["market_info"].Rows[0]["oddlot_accvalue"].ToString()), Convert.ToInt64(dsViewOddLot.Tables["market_info"].Rows[0]["oddlot_accvolume"].ToString()));
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("ViewOddLotUpdateToControl", ex);
			}
			finally
			{
				this.intzaViewOddLotInfo.Redraw();
				this.intzaViewOddLot.Redraw();
				this.intzaLS.Redraw();
				this.intzaInfo.Redraw();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ViewOddLotShowStockVolume(decimal lastPrice, int deal, long volume, decimal value)
		{
			try
			{
				decimal num = 0m;
				if (volume > 0L)
				{
					num = Math.Round(value / volume, 2);
				}
				this.intzaViewOddLotInfo.Item("oddlast").Text = FormatUtil.PriceFormat(lastPrice, 2, "0");
				this.intzaViewOddLotInfo.Item("oddlast").FontColor = Utilities.ComparePriceCFColor(lastPrice, this._stockInfo);
				this.intzaViewOddLotInfo.Item("oddavg").Text = FormatUtil.PriceFormat(num, 2, "0");
				this.intzaViewOddLotInfo.Item("odddeal").Text = FormatUtil.PriceFormat(deal, 0, "0");
				this.intzaViewOddLotInfo.Item("oddvol").Text = FormatUtil.PriceFormat(volume, 0, "0");
				this.intzaViewOddLotInfo.Item("oddval").Text = FormatUtil.PriceFormat(value, 2, "0");
				this.intzaViewOddLotInfo.Item("oddavg").FontColor = ((num > 0m) ? Utilities.ComparePriceColor(num, this._stockInfo.PriorPrice) : Color.Yellow);
			}
			catch (Exception ex)
			{
				this.ShowError("ViewOddLotShowStockVolume", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ViewOddLot_TopPrice(string side, decimal price1, decimal price2, decimal price3, decimal price4, decimal price5, long volume1, long volume2, long volume3, long volume4, long volume5)
		{
			if (side == "B")
			{
				this.intzaViewOddLot.Records(0).Fields("bid").Text = price1;
				this.intzaViewOddLot.Records(0).Fields("bid_vol").Text = volume1;
				this.intzaViewOddLot.Records(1).Fields("bid").Text = price2;
				this.intzaViewOddLot.Records(1).Fields("bid_vol").Text = volume2;
				this.intzaViewOddLot.Records(2).Fields("bid").Text = price3;
				this.intzaViewOddLot.Records(2).Fields("bid_vol").Text = volume3;
				this.intzaViewOddLot.Records(3).Fields("bid").Text = price4;
				this.intzaViewOddLot.Records(3).Fields("bid_vol").Text = volume4;
				this.intzaViewOddLot.Records(4).Fields("bid").Text = price5;
				this.intzaViewOddLot.Records(4).Fields("bid_vol").Text = volume5;
				Color fontColor = Utilities.ComparePriceCFColor(price1, this._stockInfo);
				this.intzaViewOddLot.Records(0).Fields("bid").FontColor = fontColor;
				this.intzaViewOddLot.Records(0).Fields("bid_vol").FontColor = fontColor;
				fontColor = Utilities.ComparePriceCFColor(price2, this._stockInfo);
				this.intzaViewOddLot.Records(1).Fields("bid").FontColor = fontColor;
				this.intzaViewOddLot.Records(1).Fields("bid_vol").FontColor = fontColor;
				fontColor = Utilities.ComparePriceCFColor(price3, this._stockInfo);
				this.intzaViewOddLot.Records(2).Fields("bid").FontColor = fontColor;
				this.intzaViewOddLot.Records(2).Fields("bid_vol").FontColor = fontColor;
				fontColor = Utilities.ComparePriceCFColor(price4, this._stockInfo);
				this.intzaViewOddLot.Records(3).Fields("bid").FontColor = fontColor;
				this.intzaViewOddLot.Records(3).Fields("bid_vol").FontColor = fontColor;
				fontColor = Utilities.ComparePriceCFColor(price5, this._stockInfo);
				this.intzaViewOddLot.Records(4).Fields("bid").FontColor = fontColor;
				this.intzaViewOddLot.Records(4).Fields("bid_vol").FontColor = fontColor;
			}
			else
			{
				if (side == "S")
				{
					this.intzaViewOddLot.Records(0).Fields("offer").Text = price1;
					this.intzaViewOddLot.Records(0).Fields("offer_vol").Text = volume1;
					this.intzaViewOddLot.Records(1).Fields("offer").Text = price2;
					this.intzaViewOddLot.Records(1).Fields("offer_vol").Text = volume2;
					this.intzaViewOddLot.Records(2).Fields("offer").Text = price3;
					this.intzaViewOddLot.Records(2).Fields("offer_vol").Text = volume3;
					this.intzaViewOddLot.Records(3).Fields("offer").Text = price4;
					this.intzaViewOddLot.Records(3).Fields("offer_vol").Text = volume4;
					this.intzaViewOddLot.Records(4).Fields("offer").Text = price5;
					this.intzaViewOddLot.Records(4).Fields("offer_vol").Text = volume5;
					Color fontColor = Utilities.ComparePriceCFColor(price1, this._stockInfo);
					this.intzaViewOddLot.Records(0).Fields("offer").FontColor = fontColor;
					this.intzaViewOddLot.Records(0).Fields("offer_vol").FontColor = fontColor;
					fontColor = Utilities.ComparePriceCFColor(price2, this._stockInfo);
					this.intzaViewOddLot.Records(1).Fields("offer").FontColor = fontColor;
					this.intzaViewOddLot.Records(1).Fields("offer_vol").FontColor = fontColor;
					fontColor = Utilities.ComparePriceCFColor(price3, this._stockInfo);
					this.intzaViewOddLot.Records(2).Fields("offer").FontColor = fontColor;
					this.intzaViewOddLot.Records(2).Fields("offer_vol").FontColor = fontColor;
					fontColor = Utilities.ComparePriceCFColor(price4, this._stockInfo);
					this.intzaViewOddLot.Records(3).Fields("offer").FontColor = fontColor;
					this.intzaViewOddLot.Records(3).Fields("offer_vol").FontColor = fontColor;
					fontColor = Utilities.ComparePriceCFColor(price5, this._stockInfo);
					this.intzaViewOddLot.Records(4).Fields("offer").FontColor = fontColor;
					this.intzaViewOddLot.Records(4).Fields("offer_vol").FontColor = fontColor;
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReceiveMessage(IBroadcastMessage message, StockList.StockInformation realtimeStockInfo)
		{
			if (!base.IsLoadingData)
			{
				try
				{
					if (message.MessageType == "IS")
					{
						if (this.currentPage == "View Oddlot")
						{
							if (this._stockInfo != null)
							{
								ISMessage iSMessage = (ISMessage)message;
								if (iSMessage.Symbol == ".SET")
								{
									this.ViewOddLotMktTotal(iSMessage.OddlotAccValue, iSMessage.OddlotAccVolume);
									if (base.IsAllowRender)
									{
										this.intzaViewOddLotInfo.EndUpdate();
									}
								}
							}
						}
					}
					if (realtimeStockInfo != null)
					{
						if (this._stockInfo != null && realtimeStockInfo.Number == this._stockInfo.Number)
						{
							if (message.MessageType == "TP")
							{
								if (this.currentPage == "Stock in Play")
								{
									TPMessage tPMessage = (TPMessage)message;
									this.StockInPlayUpdateTopPrice(tPMessage.Side, tPMessage.Price1, tPMessage.Price2, tPMessage.Price3, tPMessage.Price4, tPMessage.Price5, tPMessage.Volume1 * (long)realtimeStockInfo.BoardLot, tPMessage.Volume2 * (long)realtimeStockInfo.BoardLot, tPMessage.Volume3 * (long)realtimeStockInfo.BoardLot, tPMessage.Volume4 * (long)realtimeStockInfo.BoardLot, tPMessage.Volume5 * (long)realtimeStockInfo.BoardLot);
									if (base.IsAllowRender)
									{
										this.intzaStockInPlay.EndUpdate();
									}
								}
								else
								{
									if (this.currentPage == "View Oddlot")
									{
										TPMessage tPMessage = (TPMessage)message;
										this.ViewOddLot_TopPrice(tPMessage.Side, tPMessage.Price1, tPMessage.Price2, tPMessage.Price3, tPMessage.Price4, tPMessage.Price5, tPMessage.Volume1, tPMessage.Volume2, tPMessage.Volume3, tPMessage.Volume4, tPMessage.Volume5);
										if (base.IsAllowRender)
										{
											this.intzaViewOddLot.Redraw();
										}
									}
								}
							}
							else
							{
								if (message.MessageType == "L+")
								{
									LSAccumulate lSAccumulate = (LSAccumulate)message;
									if (this.currentPage == "Stock in Play")
									{
										this.StockInPlayUpdateBuySellVolume(lSAccumulate.Side, lSAccumulate.LastPrice, lSAccumulate.Volume * (long)realtimeStockInfo.BoardLot, lSAccumulate.Deals);
										if (lSAccumulate.Side == "B")
										{
											this._buyDeals += lSAccumulate.Deals;
										}
										else
										{
											if (lSAccumulate.Side == "S")
											{
												this._sellDeals += lSAccumulate.Deals;
											}
										}
										this.StockInPlayUpdateTotalVolumeAndTotalDeals(lSAccumulate.BuyVolume, lSAccumulate.SellVolume, lSAccumulate.OpenVolume);
										if (base.IsAllowRender)
										{
											this.intzaStockInPlay.EndUpdate();
										}
									}
									else
									{
										if (this.currentPage == "Sale by Price")
										{
											if (this.SaleByPriceUpdateBuySell(lSAccumulate.LastPrice, lSAccumulate.Side, lSAccumulate.Deals, lSAccumulate.Volume * (long)realtimeStockInfo.BoardLot, false))
											{
												this.intzaSaleByPrice.Sort("price", SortType.Desc);
											}
											if (base.IsAllowRender)
											{
												this.intzaSaleByPrice.EndUpdate();
											}
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
										else
										{
											if (this.currentPage == "Sale by Time")
											{
												decimal avg = 0m;
												if (lSAccumulate.AccVolume > 0L)
												{
													avg = lSAccumulate.AccValue / (lSAccumulate.AccVolume * (long)realtimeStockInfo.BoardLot);
												}
												this.SaleByTimeUpdateGridData(lSAccumulate.Side, lSAccumulate.LastPrice, lSAccumulate.Volume * (long)realtimeStockInfo.BoardLot, realtimeStockInfo.ChangePrice, avg, lSAccumulate.LastTime, -1);
												if (base.IsAllowRender)
												{
													this.intzaSaleByTime.EndUpdate();
												}
											}
										}
									}
									this.MainUpdatePrice(lSAccumulate.LastPrice, realtimeStockInfo.HighPrice, realtimeStockInfo.LowPrice, lSAccumulate.AccValue, lSAccumulate.AccVolume, realtimeStockInfo);
									this.MainUpdateAllVolume(lSAccumulate.DealInMain, lSAccumulate.AccVolume * (long)realtimeStockInfo.BoardLot, lSAccumulate.AccValue, lSAccumulate.OpenVolume * (long)realtimeStockInfo.BoardLot, lSAccumulate.BuyVolume * (long)realtimeStockInfo.BoardLot, lSAccumulate.SellVolume * (long)realtimeStockInfo.BoardLot);
									if (lSAccumulate.Side == string.Empty)
									{
										this.MainUpdateOpenOrProjectOpenPrice(ApplicationInfo.MarketState, ApplicationInfo.MarketSession, lSAccumulate.LastPrice, realtimeStockInfo);
									}
									this.MainUpdateStockTicker(lSAccumulate.LastPrice, lSAccumulate.Side, Convert.ToInt64(lSAccumulate.Volume * (long)realtimeStockInfo.BoardLot), lSAccumulate.LastTime, -1, realtimeStockInfo);
									if (base.IsAllowRender)
									{
										this.intzaLS.Redraw();
										this.intzaInfo.EndUpdate();
									}
								}
								else
								{
									if (message.MessageType == "PD")
									{
										PDMessage pDMessage = (PDMessage)message;
										this.MainUpdateBigLotValue(pDMessage.BiglotAccValue);
										if (base.IsAllowRender)
										{
											this.intzaInfo.EndUpdate();
										}
									}
									else
									{
										if (message.MessageType == "LO")
										{
											LOMessage lOMessage = (LOMessage)message;
											if (this.currentPage == "View Oddlot")
											{
												this.ViewOddLotShowStockVolume(lOMessage.Price, lOMessage.OddlotDeals, lOMessage.OddlotAccVolume, lOMessage.OddlotAccValue);
												if (base.IsAllowRender)
												{
													this.intzaInfo.EndUpdate();
												}
												this.MainUpdateStockTicker(lOMessage.Price, lOMessage.Side, lOMessage.Volume, lOMessage.LastTime, -1, realtimeStockInfo);
												if (base.IsAllowRender)
												{
													this.intzaLS.Redraw();
												}
											}
										}
										else
										{
											if (message.MessageType == "PO")
											{
												POMessage pOMessage = (POMessage)message;
												if (ApplicationInfo.MarketState != "M")
												{
													this.MainUpdateOpenOrProjectOpenPrice(ApplicationInfo.MarketState, ApplicationInfo.MarketSession, pOMessage.ProjectedPrice, realtimeStockInfo);
												}
												if (base.IsAllowRender)
												{
													this.intzaInfo.EndUpdate();
												}
											}
											else
											{
												if (message.MessageType == "SS")
												{
													this.intzaInfo.Item("tbCeiling").Text = realtimeStockInfo.Ceiling.ToString();
													this.intzaInfo.Item("tbFloor").Text = realtimeStockInfo.Floor.ToString();
													this.intzaInfo.Item("tbPrior").Text = realtimeStockInfo.PriorPrice.ToString();
													if (base.IsAllowRender)
													{
														this.intzaInfo.EndUpdate();
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
					this.ShowError("ReceiveMessage", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReceiveTfexMessage(IBroadcastMessage message, SeriesList.SeriesInformation realtimeSeriesInfo)
		{
			if (this.FormState != ClientBaseForm.ClientBaseFormState.Closing)
			{
				if (!base.IsLoadingData)
				{
					try
					{
						if (this._seriesInfo != null && realtimeSeriesInfo != null && realtimeSeriesInfo.Symbol == this._seriesInfo.Symbol)
						{
							string messageType = message.MessageType;
							if (messageType != null)
							{
								if (!(messageType == "TP"))
								{
									if (!(messageType == "LS"))
									{
										if (!(messageType == "PO"))
										{
											if (messageType == "TCF")
											{
												if (realtimeSeriesInfo.Symbol == this._seriesInfo.Symbol)
												{
													TCFMessageTFEX tCFMessageTFEX = (TCFMessageTFEX)message;
													this.intzaInfo.Item("tbCeiling").Text = realtimeSeriesInfo.Ceiling.ToString();
													this.intzaInfo.Item("tbFloor").Text = realtimeSeriesInfo.Floor.ToString();
													if (base.IsAllowRender)
													{
														this.intzaInfo.EndUpdate();
													}
												}
											}
										}
										else
										{
											if (realtimeSeriesInfo.Symbol == this._seriesInfo.Symbol)
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
													this.intzaInfo.EndUpdate();
												}
											}
										}
									}
									else
									{
										LSMessageTFEX lSMessageTFEX = (LSMessageTFEX)message;
										if (this.currentPage == "Sale by Time")
										{
											decimal avg = 0m;
											if (lSMessageTFEX.AccVolume > 0)
											{
												avg = lSMessageTFEX.AccValue / lSMessageTFEX.AccVolume;
											}
											this.SaleByTimeUpdateGridData_TFEX(lSMessageTFEX.Side, lSMessageTFEX.Price, (long)lSMessageTFEX.Vol, lSMessageTFEX.Price - realtimeSeriesInfo.PrevFixPrice, avg, lSMessageTFEX.LastTime, -1);
											if (base.IsAllowRender)
											{
												this.intzaSaleByTime.EndUpdate();
											}
										}
										else
										{
											if (this.currentPage == "Sale by Price")
											{
												if (this.SaleByPriceUpdateBuySell(lSMessageTFEX.Price, lSMessageTFEX.Side, 1, (long)lSMessageTFEX.Vol, false))
												{
													this.intzaSaleByPrice.Sort("price", SortType.Desc);
												}
												if (base.IsAllowRender)
												{
													this.intzaSaleByPrice.EndUpdate();
												}
												if (lSMessageTFEX.Side == "B" && lSMessageTFEX.DealSource != 20)
												{
													this.wcGraphVolume.UpdateData((double)lSMessageTFEX.Price, (long)lSMessageTFEX.Vol, (long)lSMessageTFEX.Vol, 0L);
												}
												else
												{
													if (lSMessageTFEX.Side == "S" && lSMessageTFEX.DealSource != 20)
													{
														this.wcGraphVolume.UpdateData((double)lSMessageTFEX.Price, (long)lSMessageTFEX.Vol, 0L, (long)lSMessageTFEX.Vol);
													}
													else
													{
														this.wcGraphVolume.UpdateData((double)lSMessageTFEX.Price, (long)lSMessageTFEX.Vol, 0L, 0L);
													}
												}
												if (base.IsAllowRender)
												{
													this.wcGraphVolume.EndUpdate();
												}
											}
											else
											{
												if (this.currentPage == "Stock in Play")
												{
													this.StockInPlayUpdateBuySellVolume_TFEX(lSMessageTFEX.Side, lSMessageTFEX.Price, (long)lSMessageTFEX.Vol, 1);
													if (lSMessageTFEX.Side == "B")
													{
														this._buyDeals++;
													}
													else
													{
														if (lSMessageTFEX.Side == "S")
														{
															this._sellDeals++;
														}
													}
													this.StockInPlayUpdateTotalVolumeAndTotalDeals((long)lSMessageTFEX.LongQty, (long)lSMessageTFEX.ShortQty, (long)lSMessageTFEX.OpenQty);
													if (base.IsAllowRender)
													{
														this.intzaStockInPlay.EndUpdate();
													}
												}
											}
										}
										this.MainUpdateLastSalePrice_TFEX(lSMessageTFEX.Price, lSMessageTFEX.High, lSMessageTFEX.Low, lSMessageTFEX.AccValue, (long)lSMessageTFEX.AccVolume, realtimeSeriesInfo);
										this.MainUpdateAllVolume(lSMessageTFEX.Deals, (long)lSMessageTFEX.AccVolume, lSMessageTFEX.AccValue, (long)lSMessageTFEX.OpenQty, (long)lSMessageTFEX.LongQty, (long)lSMessageTFEX.ShortQty);
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
										if (base.IsAllowRender)
										{
											this.intzaInfo.EndUpdate();
										}
										this.UpdateLastSaleTicker_TFEX(lSMessageTFEX.Price, lSMessageTFEX.Side.ToString(), (long)lSMessageTFEX.Vol, lSMessageTFEX.LastTime, -1, realtimeSeriesInfo);
										if (base.IsAllowRender)
										{
											this.intzaLS.EndUpdate();
										}
									}
								}
								else
								{
									if (this.currentPage == "Stock in Play")
									{
										TPMessageTFEX tPMessageTFEX = (TPMessageTFEX)message;
										this.StockInPlayUpdateTopPrice_TFEX(tPMessageTFEX.Side, tPMessageTFEX.Price1, tPMessageTFEX.Price2, tPMessageTFEX.Price3, tPMessageTFEX.Price4, tPMessageTFEX.Price5, (long)tPMessageTFEX.Vol1, (long)tPMessageTFEX.Vol2, (long)tPMessageTFEX.Vol3, (long)tPMessageTFEX.Vol4, (long)tPMessageTFEX.Vol5);
										if (base.IsAllowRender)
										{
											this.intzaStockInPlay.Redraw();
										}
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
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tscbStock_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!base.IsLoadingData)
			{
				this.RefreshData();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void RefreshData()
		{
			if (this.currentPage == "Stock in Play")
			{
				this.StockInPlay_SetNewStock(this.tscbStock.Text.Trim(), true);
			}
			else
			{
				if (this.currentPage == "Sale by Price")
				{
					this.SaleByPrice_SetNewStock(this.tscbStock.Text.Trim(), true);
				}
				else
				{
					if (this.currentPage == "Sale by Time")
					{
						this.SaleByTime_SetNewStock(this.tscbStock.Text.Trim(), true);
					}
					else
					{
						if (this.currentPage == "View Oddlot")
						{
							this.ViewOddLotSetNewStock(this.tscbStock.Text.Trim(), true);
						}
					}
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnSaleByTimeClearTime_Click(object sender, EventArgs e)
		{
			try
			{
				this.tstxtSaleByTimeSearchTimeHour.Clear();
				this.tstxtSaleByTimeSearchTimeMinute.Clear();
				this.SaleByTime_SetNewStock(this.tscbStock.Text.Trim(), true);
			}
			catch (Exception ex)
			{
				this.ShowError("tsbtnSaleByTimeClearTime_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaStockInPlay_TableMouseClick(object sender, TableMouseEventArgs e)
		{
			try
			{
				if (e.RowIndex >= 0)
				{
					if (Settings.Default.SmartOneClick)
					{
						string side = (e.Mouse.Button == MouseButtons.Left) ? "B" : "S";
						string text = this.intzaStockInPlay.Records(e.RowIndex).Fields("price").Text.ToString();
						text = text.Replace("+", "");
						text = text.Replace("-", "");
						TemplateManager.Instance.MainForm.SendOrderBox.SetSmartOneClick(side, ApplicationInfo.CurrentSymbol, text, Settings.Default.SmartClickVolume);
					}
					else
					{
						TemplateManager.Instance.MainForm.SendOrderBox.SetCurrentSymbol(ApplicationInfo.CurrentSymbol);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("intzaStockInPlay_TableMouseClick", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaStockInPlayLS_TableMouseClick(object sender, TableMouseEventArgs e)
		{
			try
			{
				if (Settings.Default.SmartOneClick && e.RowIndex >= 0)
				{
					string side = (e.Mouse.Button == MouseButtons.Left) ? "B" : "S";
					string text = this.intzaLS.Records(e.RowIndex).Fields("price").Text.ToString();
					text = text.Replace("+", "");
					text = text.Replace("-", "");
					TemplateManager.Instance.MainForm.SendOrderBox.SetSmartOneClick(side, ApplicationInfo.CurrentSymbol, text, Settings.Default.SmartClickVolume);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("intzaStockInPlayLS_TableMouseClick", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaSaleByPrice_TableMouseClick(object sender, TableMouseEventArgs e)
		{
			try
			{
				if (e.RowIndex >= 0)
				{
					if (Settings.Default.SmartOneClick)
					{
						string side = (e.Mouse.Button == MouseButtons.Left) ? "B" : "S";
						string price = this.intzaSaleByPrice.Records(e.RowIndex).Fields("price").Text.ToString();
						TemplateManager.Instance.MainForm.SendOrderBox.SetSmartOneClick(side, ApplicationInfo.CurrentSymbol, price, Settings.Default.SmartClickVolume);
					}
					else
					{
						TemplateManager.Instance.MainForm.SendOrderBox.SetCurrentSymbol(ApplicationInfo.CurrentSymbol);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("intzaSaleByPrice_TableMouseClick", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaSaleByTime_TableMouseClick(object sender, TableMouseEventArgs e)
		{
			try
			{
				if (e.RowIndex >= 0)
				{
					if (Settings.Default.SmartOneClick)
					{
						string side = (e.Mouse.Button == MouseButtons.Left) ? "B" : "S";
						string text = this.intzaSaleByTime.Records(e.RowIndex).Fields("price").Text.ToString();
						text = text.Replace("+", "");
						text = text.Replace("-", "");
						TemplateManager.Instance.MainForm.SendOrderBox.SetSmartOneClick(side, ApplicationInfo.CurrentSymbol, text, Settings.Default.SmartClickVolume);
					}
					else
					{
						TemplateManager.Instance.MainForm.SendOrderBox.SetCurrentSymbol(ApplicationInfo.CurrentSymbol);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("intzaSaleByTime_TableMouseClick", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaViewOddLot_TableMouseClick(object sender, TableMouseEventArgs e)
		{
			try
			{
				if (e.RowIndex >= 0)
				{
					if (Settings.Default.SmartOneClick)
					{
						string price = string.Empty;
						string side = (e.Mouse.Button == MouseButtons.Left) ? "B" : "S";
						if (e.Column.Name == "bid" || e.Column.Name == "offer")
						{
							price = this.intzaViewOddLot.Records(e.RowIndex).Fields(e.Column.Name).Text.ToString();
						}
						TemplateManager.Instance.MainForm.SendOrderBox.SetSmartOneClick(side, ApplicationInfo.CurrentSymbol, price, Settings.Default.SmartClickVolume);
					}
					else
					{
						TemplateManager.Instance.MainForm.SendOrderBox.SetCurrentSymbol(ApplicationInfo.CurrentSymbol);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("intzaViewOddLot_TableMouseClick", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetPage(string currentPage)
		{
			try
			{
				this.currentPage = currentPage;
				if (this.currentPage == "Stock in Play")
				{
					this.tsbtnStockInPlayPrevPage.Visible = true;
					this.tsbtnStockInPlayNextPage.Visible = true;
					this.intzaStockInPlay.Show();
				}
				else
				{
					if (this.currentPage == "Sale by Price")
					{
						this.intzaSaleByPrice.Show();
						this.wcGraphVolume.Show();
					}
					else
					{
						if (this.currentPage == "Sale by Time")
						{
							this.tslbHour.Visible = true;
							this.tstxtSaleByTimeSearchTimeHour.Visible = true;
							this.tslbMinute.Visible = true;
							this.tstxtSaleByTimeSearchTimeMinute.Visible = true;
							this.tsbtnSaleByTimeClearTime2.Visible = true;
							this.tssepSaleByTime2.Visible = true;
							this.tsbtnSaleByTimeFirstPage.Visible = true;
							this.tsbtnSaleByTimePrevPage.Visible = true;
							this.tslblSaleByTimePageNo.Visible = true;
							this.tsbtnSaleByTimeNextPage.Visible = true;
							this.intzaSaleByTime.Show();
						}
						else
						{
							if (this.currentPage == "View Oddlot")
							{
							}
						}
					}
				}
				if (this.currentPage == "View Oddlot")
				{
					this.intzaViewOddLotInfo.Show();
					this.intzaViewOddLot.Show();
					this.intzaLS.Visible = true;
					this.intzaInfo.Visible = false;
				}
				else
				{
					if (this.currentPage == "Sale by Price")
					{
						this.intzaLS.Visible = false;
						this.intzaInfo.Visible = false;
					}
					else
					{
						this.intzaLS.Visible = true;
						this.intzaInfo.Visible = true;
					}
				}
				if (this.currentPage != "Stock in Play")
				{
					this.tsbtnStockInPlayPrevPage.Visible = false;
					this.tsbtnStockInPlayNextPage.Visible = false;
					this.intzaStockInPlay.Hide();
				}
				if (this.currentPage != "Sale by Price")
				{
					this.intzaSaleByPrice.Hide();
					this.wcGraphVolume.Hide();
				}
				if (this.currentPage != "Sale by Time")
				{
					this.tslbHour.Visible = false;
					this.tstxtSaleByTimeSearchTimeHour.Visible = false;
					this.tslbMinute.Visible = false;
					this.tstxtSaleByTimeSearchTimeMinute.Visible = false;
					this.tsbtnSaleByTimeClearTime2.Visible = false;
					this.tssepSaleByTime2.Visible = false;
					this.tsbtnSaleByTimeFirstPage.Visible = false;
					this.tsbtnSaleByTimePrevPage.Visible = false;
					this.tslblSaleByTimePageNo.Visible = false;
					this.tsbtnSaleByTimeNextPage.Visible = false;
					this.intzaSaleByTime.Hide();
				}
				if (this.currentPage != "View Oddlot")
				{
					this.intzaViewOddLot.Hide();
					this.intzaViewOddLotInfo.Hide();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SetPage", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void comboStock_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				switch (e.KeyCode)
				{
				case Keys.Prior:
					if (this.currentPage == "Stock in Play")
					{
						this.tsbtnStockInPlayPrevPage.PerformClick();
					}
					else
					{
						if (this.currentPage == "Sale by Time")
						{
							this.tsbtnSaleByTimePrevPage.PerformClick();
						}
					}
					e.SuppressKeyPress = true;
					break;
				case Keys.Next:
					if (this.currentPage == "Stock in Play")
					{
						this.tsbtnStockInPlayNextPage.PerformClick();
					}
					else
					{
						if (this.currentPage == "Sale by Time")
						{
							this.tsbtnSaleByTimeNextPage.PerformClick();
						}
					}
					e.SuppressKeyPress = true;
					break;
				case Keys.Home:
				case Keys.Up:
				case Keys.Down:
					e.SuppressKeyPress = true;
					break;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("comboStock_KeyDown", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tscbSelection_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (!base.IsLoadingData)
				{
					this.SetPage(this.tscbSelection.Text.Trim());
					this.SetResize();
					this.ReloadDataPage(this.currentPage, true);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tscbSelection_SelectedIndexChanged", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SaleByPriceUpdateToControl_TFEX()
		{
			try
			{
				this.intzaSaleByPrice.BeginUpdate();
				this.intzaLS.BeginUpdate();
				this.intzaLS.ClearAllText();
				if (this.dsSaleByPrice != null && this.dsSaleByPrice.Tables.Contains("Sell_by_price"))
				{
					this.intzaSaleByPrice.Rows = 0;
					this.wcGraphVolume.InitData(this._seriesInfo.Symbol, (double)this._seriesInfo.PrevFixPrice, (double)this._seriesInfo.LastSalePrice, (double)this._seriesInfo.Ceiling, (double)this._seriesInfo.Floor);
					foreach (DataRow dataRow in this.dsSaleByPrice.Tables["Sell_by_price"].Rows)
					{
						decimal num;
						decimal.TryParse(dataRow["nmrPrice"].ToString(), out num);
						int sideDeals;
						int.TryParse(dataRow["LongDeal"].ToString(), out sideDeals);
						long num2;
						long.TryParse(dataRow["LongVolume"].ToString(), out num2);
						this.SaleByPriceUpdateBuySell(num, "B", sideDeals, num2, true);
						int.TryParse(dataRow["ShortDeal"].ToString(), out sideDeals);
						long num3;
						long.TryParse(dataRow["ShortVolume"].ToString(), out num3);
						this.SaleByPriceUpdateBuySell(num, "S", sideDeals, num3, true);
						long num4;
						long.TryParse(dataRow["TotalVolume"].ToString(), out num4);
						this.SaleByPriceUpdateBuySell(num, "", 0, num4 - (num2 + num3), true);
						this.wcGraphVolume.InputData((double)num, num4, num2, num3);
					}
					this.wcGraphVolume.EndUpdate();
					this.wcGraphVolume.Sort();
					this.intzaSaleByPrice.Sort("price", SortType.Desc);
				}
				this.intzaSaleByPrice.Redraw();
				this.intzaLS.Redraw();
				this.intzaInfo.EndUpdate();
			}
			catch (Exception ex)
			{
				this.ShowError("SaleByPriceUpdateToControl", ex);
			}
		}
	}
}
