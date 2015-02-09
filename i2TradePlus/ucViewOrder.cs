using i2TradePlus.Information;
using i2TradePlus.ITSNetBusinessWS;
using i2TradePlus.ITSNetBusinessWSTFEX;
using i2TradePlus.Properties;
using ITSNet.Common.BIZ;
using ITSNet.Common.BIZ.RealtimeMessage;
using STIControl;
using STIControl.ExpandTableGrid;
using STIControl.SortTableGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
namespace i2TradePlus
{
	internal class ucViewOrder : UserControl, IRealtimeMessage
	{
		public delegate void OnDisplaySummaryOrdersHandler();
		private struct RecordData
		{
			public long OrderNumber;
			public string Stock;
			public string Side;
			public long Volume;
			public long Matched;
			public long PubVol;
			public string price;
			public string OrderStatus;
			public int TrusteeID;
			public string OrderTimes;
			public string Quote;
			public string ApprUserNo;
			public string OrderDate;
			public string OrderTime;
			public bool IsAfterCloseOrder;
			public string Position;
			public string Series;
			public string OrdType;
			public string Price
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.price;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.price = value.Trim();
					if (ApplicationInfo.SupportFreewill)
					{
						this.price = ApplicationInfo.ConvertPrice(this.price);
					}
				}
			}
		}
		private delegate void ShowSplashCallBack(bool visible, string message, bool isAutoClose);
		private delegate void ReceiveOrderInfoTFEXCallBack(IBroadcastMessage message);
		private delegate void ReceiveOrderInfoCallBack(IBroadcastMessage message);
		private delegate void SetGridFocusCallBack(bool forceIndex);
		private delegate void UpdateDataToControlTFEXCallBack();
		private class CancelItem
		{
			public long OrderNo = 0L;
			public string OrderDate = string.Empty;
			public string OrderTime = string.Empty;
			public string AfterClose = "N";
			public bool Complete = false;
			[MethodImpl(MethodImplOptions.NoInlining)]
			public CancelItem(long orderNo, string orderDate, string orderTime, string afterClose)
			{
				this.OrderNo = orderNo;
				this.OrderDate = orderDate;
				this.OrderTime = orderTime;
				this.AfterClose = afterClose;
			}
		}
		private delegate void ShowMessageInFormConfirmCallBack(string message, frmOrderFormConfirm.OpenStyle openStyle);
		private delegate void SelectAllOrderForCancelCallBack(bool setSelectAll);
		private delegate void UpdateOrderResultCallback(string text, Color color, int currRow);
		private const int VIEW_EQUITY_ORDER = 1;
		private const int VIEW_STOP_ORDER = 2;
		private const int VIEW_TFEX_ORDER = 3;
		private const int VIEW_RE_ORDER = 4;
		private IContainer components = null;
		private ColumnHeader colConfirm;
		private ColumnHeader colVolume;
		private ColumnHeader colPrice;
		private ColumnHeader colTime;
		private ToolStrip tStripMenu;
		private ToolStripLabel tslbStatus;
		private ToolStripLabel tslbStock;
		private ToolStripButton tsbtnSearch;
		private ToolStripButton tsbtnCancelOrder;
		private ToolStripButton tsbtnClearCondition;
		private ToolStripComboBox tscbStatus;
		private ToolStripTextBox tstbStock;
		private ToolStripComboBox tscbSide;
		private ToolStripLabel tslbSide;
		private Label lbLoading;
		private ToolStripTextBox tstbPrice;
		private ToolStripLabel tslbPrice;
		private ContextMenuStrip contextMenuStrip1;
		private ToolStripMenuItem tsmRefresh;
		private SortGrid intzaOrderList;
		private ToolStripButton tsbtnEditOrder;
		private SortGrid intzaOrderListTFEX;
		private ToolStripButton tsbtnReorder;
		private SortGrid intzaReOrderList;
		private ToolStripButton tsbtnReloadReorder;
		private Panel panelStopOrderFLS;
		private Label lbFLS;
		private Label tbStopOrderFLS;
		private Button btnStopOrderFLSclose;
		private ExpandGrid intzaStopOrder;
		private int _viewType = 1;
		private string selStatus = string.Empty;
		private string _selAccount = string.Empty;
		private string selStock = string.Empty;
		private string selSide = string.Empty;
		private string selPrice = string.Empty;
		private frmViewOrderInfo _viewOrderInfo = null;
		private frmViewOrderInfoTFEX _viewOrderInfoTfex = null;
		private DataSet tdsOrder = null;
		private bool _isSelectAll = false;
		private Thread thrSendOrder;
		private bool isActive = false;
		private bool isLoadingData = false;
		private System.Windows.Forms.Timer tmCloseSplash = null;
		private bool isShowLoadingControl = false;
		private bool showOnMainForm = false;
		private bool isShowToolsBar = true;
		private bool isShowNextPage = true;
		private long totalBuyVolume = 0L;
		private long totalBuyMatchedVolume = 0L;
		private decimal totalBuyMatchedValue = 0m;
		private long totalSellVolume = 0L;
		private long totalSellMatchedVolume = 0L;
		private decimal totalSellMatchedValue = 0m;
		private decimal unMatchedBuyVol = 0m;
		private decimal unMatchedSellVol = 0m;
		private BackgroundWorker bgwViewOrder = null;
		private bool _isSetHeaderDone = false;
		private bool focusFlag = false;
		private List<ucViewOrder.CancelItem> _listOfOrderCancel = null;
		private System.Windows.Forms.Timer tmTest = null;
		private frmOrderFormConfirm frmConfirm = null;
		private frmEditOrder _editOrderBox = null;
		private Dictionary<int, int> _stopOrderCancelLst = null;
		private int _orderCount = 0;
		private bool _loadStopOrderFLS = false;
        public ucViewOrder.OnDisplaySummaryOrdersHandler _OnDisplaySummaryOrders;
		public event ucViewOrder.OnDisplaySummaryOrdersHandler OnDisplaySummaryOrders
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._OnDisplaySummaryOrders += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._OnDisplaySummaryOrders -= value;
			}
		}
		public int ViewType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this._viewType;
			}
		}
		public bool IsActive
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isActive;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.isActive = value;
				if (!base.DesignMode)
				{
					if (this.isActive)
					{
						this.intzaOrderList.ClearAllText();
						this.intzaOrderList.EndUpdate();
					}
					else
					{
						this._selAccount = string.Empty;
					}
				}
			}
		}
		public bool IsLoadingData
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isLoadingData;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.isLoadingData = value;
				if (!base.DesignMode)
				{
					this.ShowSplash(this.isLoadingData, "Loading...", false);
				}
			}
		}
		public bool IsShowLoadingControl
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isShowLoadingControl;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.isShowLoadingControl = value;
			}
		}
		public bool ShowOnMainForm
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.showOnMainForm;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.showOnMainForm = value;
			}
		}
		public bool IsShowToolsBar
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isShowToolsBar;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.isShowToolsBar = value;
			}
		}
		public bool IsShowNextPage
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isShowNextPage;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.isShowNextPage = value;
			}
		}
		public long TotalBuyVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.totalBuyVolume;
			}
		}
		public long TotalBuyMatchedVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.totalBuyMatchedVolume;
			}
		}
		public decimal TotalBuyMatchedValue
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.totalBuyMatchedValue;
			}
		}
		public long TotalSellVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.totalSellVolume;
			}
		}
		public long TotalSellMatchedVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.totalSellMatchedVolume;
			}
		}
		public decimal TotalSellMatchedValue
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.totalSellMatchedValue;
			}
		}
		public decimal UnMatchedBuyVol
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.unMatchedBuyVol;
			}
		}
		public decimal UnMatchedSellVol
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.unMatchedSellVol;
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
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ucViewOrder));
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
			STIControl.SortTableGrid.ColumnItem columnItem20 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem21 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem22 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem23 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem24 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem25 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem26 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem27 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem28 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem29 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem30 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem31 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem32 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem33 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem34 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem35 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem36 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem37 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem38 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem39 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem40 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem41 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem42 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem43 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem44 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem45 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem46 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem47 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem48 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem49 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem50 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem51 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem52 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem53 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem54 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem55 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem56 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem57 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem58 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem59 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem60 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem61 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem62 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem63 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem64 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem65 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem66 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem67 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem68 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem69 = new STIControl.SortTableGrid.ColumnItem();
			this.colConfirm = new ColumnHeader();
			this.colVolume = new ColumnHeader();
			this.colPrice = new ColumnHeader();
			this.colTime = new ColumnHeader();
			this.tStripMenu = new ToolStrip();
			this.tslbStatus = new ToolStripLabel();
			this.tscbStatus = new ToolStripComboBox();
			this.tslbStock = new ToolStripLabel();
			this.tstbStock = new ToolStripTextBox();
			this.tslbPrice = new ToolStripLabel();
			this.tstbPrice = new ToolStripTextBox();
			this.tslbSide = new ToolStripLabel();
			this.tscbSide = new ToolStripComboBox();
			this.tsbtnClearCondition = new ToolStripButton();
			this.tsbtnCancelOrder = new ToolStripButton();
			this.tsbtnSearch = new ToolStripButton();
			this.tsbtnEditOrder = new ToolStripButton();
			this.tsbtnReorder = new ToolStripButton();
			this.tsbtnReloadReorder = new ToolStripButton();
			this.lbLoading = new Label();
			this.contextMenuStrip1 = new ContextMenuStrip(this.components);
			this.tsmRefresh = new ToolStripMenuItem();
			this.panelStopOrderFLS = new Panel();
			this.btnStopOrderFLSclose = new Button();
			this.lbFLS = new Label();
			this.tbStopOrderFLS = new Label();
			this.intzaStopOrder = new ExpandGrid();
			this.intzaReOrderList = new SortGrid();
			this.intzaOrderListTFEX = new SortGrid();
			this.intzaOrderList = new SortGrid();
			this.tStripMenu.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.panelStopOrderFLS.SuspendLayout();
			base.SuspendLayout();
			this.colConfirm.Text = "Confirm#";
			this.colConfirm.Width = 67;
			this.colVolume.Text = "Volume";
			this.colVolume.Width = 78;
			this.colPrice.Text = "Price";
			this.colPrice.Width = 56;
			this.colTime.Text = "Time";
			this.colTime.Width = 64;
			this.tStripMenu.AllowMerge = false;
			this.tStripMenu.BackColor = Color.FromArgb(10, 10, 10);
			this.tStripMenu.CanOverflow = false;
			this.tStripMenu.GripMargin = new Padding(0);
			this.tStripMenu.GripStyle = ToolStripGripStyle.Hidden;
			this.tStripMenu.Items.AddRange(new ToolStripItem[]
			{
				this.tslbStatus,
				this.tscbStatus,
				this.tslbStock,
				this.tstbStock,
				this.tslbPrice,
				this.tstbPrice,
				this.tslbSide,
				this.tscbSide,
				this.tsbtnClearCondition,
				this.tsbtnCancelOrder,
				this.tsbtnSearch,
				this.tsbtnEditOrder,
				this.tsbtnReorder,
				this.tsbtnReloadReorder
			});
			this.tStripMenu.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.tStripMenu.Location = new Point(0, 0);
			this.tStripMenu.Name = "tStripMenu";
			this.tStripMenu.Padding = new Padding(1, 2, 1, 1);
			this.tStripMenu.RenderMode = ToolStripRenderMode.Professional;
			this.tStripMenu.Size = new Size(833, 28);
			this.tStripMenu.TabIndex = 57;
			this.tslbStatus.BackColor = Color.Transparent;
			this.tslbStatus.ForeColor = Color.Gainsboro;
			this.tslbStatus.Margin = new Padding(1);
			this.tslbStatus.Name = "tslbStatus";
			this.tslbStatus.Size = new Size(39, 23);
			this.tslbStatus.Text = "Status";
			this.tscbStatus.AutoCompleteCustomSource.AddRange(new string[]
			{
				"ALL",
				"O",
				"PO",
				"M",
				"C",
				"PX",
				"R",
				"X"
			});
			this.tscbStatus.AutoCompleteMode = AutoCompleteMode.Append;
			this.tscbStatus.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.tscbStatus.BackColor = Color.FromArgb(45, 45, 45);
			this.tscbStatus.ForeColor = Color.LightGray;
			this.tscbStatus.Items.AddRange(new object[]
			{
				"ALL",
				"O",
				"PO",
				"M",
				"C",
				"PX",
				"R",
				"X"
			});
			this.tscbStatus.Margin = new Padding(1, 0, 1, 2);
			this.tscbStatus.MaxLength = 3;
			this.tscbStatus.Name = "tscbStatus";
			this.tscbStatus.Size = new Size(75, 23);
			this.tscbStatus.KeyUp += new KeyEventHandler(this.tscbStatus_KeyUp);
			this.tscbStatus.KeyDown += new KeyEventHandler(this.tscbStatus_KeyDown);
			this.tscbStatus.Leave += new EventHandler(this.controlOrder_Leave);
			this.tscbStatus.Enter += new EventHandler(this.controlOrder_Enter);
			this.tscbStatus.TextChanged += new EventHandler(this.tscbStatus_TextChanged);
			this.tslbStock.BackColor = Color.Transparent;
			this.tslbStock.ForeColor = Color.Gainsboro;
			this.tslbStock.Margin = new Padding(1);
			this.tslbStock.Name = "tslbStock";
			this.tslbStock.Size = new Size(36, 23);
			this.tslbStock.Text = "Stock";
			this.tstbStock.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			this.tstbStock.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.tstbStock.BackColor = Color.FromArgb(45, 45, 45);
			this.tstbStock.BorderStyle = BorderStyle.FixedSingle;
			this.tstbStock.CharacterCasing = CharacterCasing.Upper;
			this.tstbStock.Font = new Font("Microsoft Sans Serif", 9f);
			this.tstbStock.ForeColor = Color.LightGray;
			this.tstbStock.Margin = new Padding(1, 0, 1, 2);
			this.tstbStock.MaxLength = 12;
			this.tstbStock.Name = "tstbStock";
			this.tstbStock.Size = new Size(80, 23);
			this.tstbStock.Leave += new EventHandler(this.controlOrder_Leave);
			this.tstbStock.KeyDown += new KeyEventHandler(this.tstbStock_KeyDown);
			this.tstbStock.Enter += new EventHandler(this.controlOrder_Enter);
			this.tstbStock.KeyUp += new KeyEventHandler(this.tstbStock_KeyUp);
			this.tslbPrice.BackColor = Color.Transparent;
			this.tslbPrice.ForeColor = Color.Gainsboro;
			this.tslbPrice.Margin = new Padding(1);
			this.tslbPrice.Name = "tslbPrice";
			this.tslbPrice.Size = new Size(33, 23);
			this.tslbPrice.Text = "Price";
			this.tstbPrice.BackColor = Color.FromArgb(45, 45, 45);
			this.tstbPrice.BorderStyle = BorderStyle.FixedSingle;
			this.tstbPrice.ForeColor = Color.LightGray;
			this.tstbPrice.Margin = new Padding(1, 0, 1, 2);
			this.tstbPrice.MaxLength = 8;
			this.tstbPrice.Name = "tstbPrice";
			this.tstbPrice.Size = new Size(65, 23);
			this.tstbPrice.Leave += new EventHandler(this.controlOrder_Leave);
			this.tstbPrice.Enter += new EventHandler(this.controlOrder_Enter);
			this.tstbPrice.KeyUp += new KeyEventHandler(this.tstbPrice_KeyUp);
			this.tslbSide.BackColor = Color.Transparent;
			this.tslbSide.ForeColor = Color.Gainsboro;
			this.tslbSide.Margin = new Padding(1);
			this.tslbSide.Name = "tslbSide";
			this.tslbSide.Size = new Size(29, 23);
			this.tslbSide.Text = "Side";
			this.tscbSide.AutoCompleteCustomSource.AddRange(new string[]
			{
				"ALL",
				"B",
				"S",
				"H",
				"C"
			});
			this.tscbSide.AutoCompleteMode = AutoCompleteMode.Append;
			this.tscbSide.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.tscbSide.BackColor = Color.FromArgb(45, 45, 45);
			this.tscbSide.ForeColor = Color.LightGray;
			this.tscbSide.Items.AddRange(new object[]
			{
				"ALL",
				"B",
				"S",
				"H",
				"C"
			});
			this.tscbSide.Margin = new Padding(1, 0, 1, 2);
			this.tscbSide.MaxLength = 3;
			this.tscbSide.Name = "tscbSide";
			this.tscbSide.Size = new Size(75, 23);
			this.tscbSide.KeyUp += new KeyEventHandler(this.tscbSide_KeyUp);
			this.tscbSide.KeyDown += new KeyEventHandler(this.tscbStatus_KeyDown);
			this.tscbSide.Leave += new EventHandler(this.controlOrder_Leave);
			this.tscbSide.Enter += new EventHandler(this.controlOrder_Enter);
			this.tscbSide.TextChanged += new EventHandler(this.tscbSide_TextChanged);
			this.tsbtnClearCondition.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnClearCondition.ForeColor = Color.Gainsboro;
			this.tsbtnClearCondition.ImageTransparentColor = Color.Magenta;
			this.tsbtnClearCondition.Margin = new Padding(5, 1, 0, 2);
			this.tsbtnClearCondition.Name = "tsbtnClearCondition";
			this.tsbtnClearCondition.Size = new Size(38, 22);
			this.tsbtnClearCondition.Text = "Clear";
			this.tsbtnClearCondition.ToolTipText = "Clear Condition";
			this.tsbtnClearCondition.Click += new EventHandler(this.tsbtnClearCondition_Click);
			this.tsbtnCancelOrder.Alignment = ToolStripItemAlignment.Right;
			this.tsbtnCancelOrder.ForeColor = Color.Tomato;
			this.tsbtnCancelOrder.Image = (Image)componentResourceManager.GetObject("tsbtnCancelOrder.Image");
			this.tsbtnCancelOrder.ImageTransparentColor = Color.Magenta;
			this.tsbtnCancelOrder.Name = "tsbtnCancelOrder";
			this.tsbtnCancelOrder.Size = new Size(63, 22);
			this.tsbtnCancelOrder.Text = "Cancel";
			this.tsbtnCancelOrder.ToolTipText = "Cancel Order";
			this.tsbtnCancelOrder.Click += new EventHandler(this.tsbtnCancelOrder_Click);
			this.tsbtnSearch.Font = new Font("Microsoft Sans Serif", 9f);
			this.tsbtnSearch.ForeColor = Color.Gainsboro;
			this.tsbtnSearch.Image = Resources.refresh;
			this.tsbtnSearch.ImageTransparentColor = Color.Magenta;
			this.tsbtnSearch.Margin = new Padding(5, 1, 0, 2);
			this.tsbtnSearch.Name = "tsbtnSearch";
			this.tsbtnSearch.Size = new Size(66, 22);
			this.tsbtnSearch.Text = "Search";
			this.tsbtnSearch.Click += new EventHandler(this.tsbtnSearch_Click);
			this.tsbtnEditOrder.Alignment = ToolStripItemAlignment.Right;
			this.tsbtnEditOrder.ForeColor = Color.Yellow;
			this.tsbtnEditOrder.Image = (Image)componentResourceManager.GetObject("tsbtnEditOrder.Image");
			this.tsbtnEditOrder.ImageTransparentColor = Color.Magenta;
			this.tsbtnEditOrder.Margin = new Padding(0, 1, 5, 2);
			this.tsbtnEditOrder.Name = "tsbtnEditOrder";
			this.tsbtnEditOrder.Size = new Size(47, 22);
			this.tsbtnEditOrder.Text = "Edit";
			this.tsbtnEditOrder.ToolTipText = "Edit Order";
			this.tsbtnEditOrder.Click += new EventHandler(this.tsbtnEditOrder_Click);
			this.tsbtnReorder.Alignment = ToolStripItemAlignment.Right;
			this.tsbtnReorder.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnReorder.ForeColor = Color.Gold;
			this.tsbtnReorder.ImageTransparentColor = Color.Magenta;
			this.tsbtnReorder.Name = "tsbtnReorder";
			this.tsbtnReorder.Size = new Size(59, 22);
			this.tsbtnReorder.Text = "Re-Order";
			this.tsbtnReorder.Visible = false;
			this.tsbtnReorder.Click += new EventHandler(this.tsbtnReorder_Click);
			this.tsbtnReloadReorder.Alignment = ToolStripItemAlignment.Right;
			this.tsbtnReloadReorder.ForeColor = Color.LightGray;
			this.tsbtnReloadReorder.Image = Resources.refresh;
			this.tsbtnReloadReorder.ImageTransparentColor = Color.Magenta;
			this.tsbtnReloadReorder.Name = "tsbtnReloadReorder";
			this.tsbtnReloadReorder.Size = new Size(66, 22);
			this.tsbtnReloadReorder.Text = "Refresh";
			this.tsbtnReloadReorder.Click += new EventHandler(this.tsbtnReloadReorder_Click);
			this.lbLoading.AutoSize = true;
			this.lbLoading.BackColor = Color.FromArgb(64, 64, 64);
			this.lbLoading.BorderStyle = BorderStyle.FixedSingle;
			this.lbLoading.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.lbLoading.ForeColor = Color.Yellow;
			this.lbLoading.Location = new Point(378, 138);
			this.lbLoading.Name = "lbLoading";
			this.lbLoading.Padding = new Padding(5, 3, 5, 3);
			this.lbLoading.Size = new Size(76, 23);
			this.lbLoading.TabIndex = 61;
			this.lbLoading.Text = "Loading ...";
			this.lbLoading.Visible = false;
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.tsmRefresh
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new Size(111, 26);
			this.tsmRefresh.Image = Resources.refresh;
			this.tsmRefresh.Name = "tsmRefresh";
			this.tsmRefresh.Size = new Size(110, 22);
			this.tsmRefresh.Text = "Reload";
			this.tsmRefresh.Click += new EventHandler(this.tsmRefresh_Click);
			this.panelStopOrderFLS.BackColor = Color.FromArgb(50, 50, 50);
			this.panelStopOrderFLS.Controls.Add(this.btnStopOrderFLSclose);
			this.panelStopOrderFLS.Controls.Add(this.lbFLS);
			this.panelStopOrderFLS.Controls.Add(this.tbStopOrderFLS);
			this.panelStopOrderFLS.Location = new Point(337, 231);
			this.panelStopOrderFLS.Name = "panelStopOrderFLS";
			this.panelStopOrderFLS.Size = new Size(262, 46);
			this.panelStopOrderFLS.TabIndex = 69;
			this.panelStopOrderFLS.Visible = false;
			this.panelStopOrderFLS.Paint += new PaintEventHandler(this.panelStopOrderFLS_Paint);
			this.btnStopOrderFLSclose.FlatAppearance.BorderSize = 0;
			this.btnStopOrderFLSclose.FlatStyle = FlatStyle.Flat;
			this.btnStopOrderFLSclose.Image = Resources.fileclose;
			this.btnStopOrderFLSclose.Location = new Point(228, 16);
			this.btnStopOrderFLSclose.Name = "btnStopOrderFLSclose";
			this.btnStopOrderFLSclose.Size = new Size(29, 26);
			this.btnStopOrderFLSclose.TabIndex = 2;
			this.btnStopOrderFLSclose.UseVisualStyleBackColor = true;
			this.btnStopOrderFLSclose.Click += new EventHandler(this.btnStopOrderFLSclose_Click);
			this.lbFLS.AutoSize = true;
			this.lbFLS.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Underline, GraphicsUnit.Point, 222);
			this.lbFLS.ForeColor = Color.Gainsboro;
			this.lbFLS.Location = new Point(4, 3);
			this.lbFLS.Name = "lbFLS";
			this.lbFLS.Size = new Size(177, 13);
			this.lbFLS.TabIndex = 1;
			this.lbFLS.Text = "รายการจับคู่ที่เกิดหลังจากการส่งคำสั่ง";
			this.tbStopOrderFLS.AutoSize = true;
			this.tbStopOrderFLS.ForeColor = Color.Yellow;
			this.tbStopOrderFLS.Location = new Point(7, 26);
			this.tbStopOrderFLS.Name = "tbStopOrderFLS";
			this.tbStopOrderFLS.Size = new Size(49, 13);
			this.tbStopOrderFLS.TabIndex = 0;
			this.tbStopOrderFLS.Text = "00:00:00";
			this.intzaStopOrder.AllowDrop = true;
			this.intzaStopOrder.BackColor = Color.Black;
			this.intzaStopOrder.CanBlink = true;
			this.intzaStopOrder.CanDrag = false;
			this.intzaStopOrder.CanGetMouseMove = false;
			columnItem.Alignment = StringAlignment.Near;
			columnItem.BackColor = Color.FromArgb(64, 64, 64);
			columnItem.FontColor = Color.LightGray;
			columnItem.IsExpand = false;
			columnItem.MyStyle = FontStyle.Regular;
			columnItem.Name = "checkbox";
			columnItem.Text = "";
			columnItem.ValueFormat = FormatType.Bitmap;
			columnItem.Visible = true;
			columnItem.Width = 3;
			columnItem2.Alignment = StringAlignment.Center;
			columnItem2.BackColor = Color.FromArgb(64, 64, 64);
			columnItem2.FontColor = Color.LightGray;
			columnItem2.IsExpand = false;
			columnItem2.MyStyle = FontStyle.Regular;
			columnItem2.Name = "side";
			columnItem2.Text = "B/S";
			columnItem2.ValueFormat = FormatType.Text;
			columnItem2.Visible = true;
			columnItem2.Width = 5;
			columnItem3.Alignment = StringAlignment.Near;
			columnItem3.BackColor = Color.FromArgb(64, 64, 64);
			columnItem3.FontColor = Color.LightGray;
			columnItem3.IsExpand = false;
			columnItem3.MyStyle = FontStyle.Regular;
			columnItem3.Name = "stock";
			columnItem3.Text = "Stock";
			columnItem3.ValueFormat = FormatType.Text;
			columnItem3.Visible = true;
			columnItem3.Width = 12;
			columnItem4.Alignment = StringAlignment.Far;
			columnItem4.BackColor = Color.FromArgb(64, 64, 64);
			columnItem4.FontColor = Color.LightGray;
			columnItem4.IsExpand = false;
			columnItem4.MyStyle = FontStyle.Regular;
			columnItem4.Name = "volume";
			columnItem4.Text = "Volume";
			columnItem4.ValueFormat = FormatType.Volume;
			columnItem4.Visible = true;
			columnItem4.Width = 13;
			columnItem5.Alignment = StringAlignment.Far;
			columnItem5.BackColor = Color.FromArgb(64, 64, 64);
			columnItem5.FontColor = Color.LightGray;
			columnItem5.IsExpand = false;
			columnItem5.MyStyle = FontStyle.Regular;
			columnItem5.Name = "price";
			columnItem5.Text = "Price";
			columnItem5.ValueFormat = FormatType.Text;
			columnItem5.Visible = true;
			columnItem5.Width = 10;
			columnItem6.Alignment = StringAlignment.Near;
			columnItem6.BackColor = Color.FromArgb(64, 64, 64);
			columnItem6.FontColor = Color.LightGray;
			columnItem6.IsExpand = false;
			columnItem6.MyStyle = FontStyle.Regular;
			columnItem6.Name = "condition";
			columnItem6.Text = "Condition";
			columnItem6.ValueFormat = FormatType.Text;
			columnItem6.Visible = true;
			columnItem6.Width = 32;
			columnItem7.Alignment = StringAlignment.Center;
			columnItem7.BackColor = Color.FromArgb(64, 64, 64);
			columnItem7.FontColor = Color.LightGray;
			columnItem7.IsExpand = false;
			columnItem7.MyStyle = FontStyle.Regular;
			columnItem7.Name = "status";
			columnItem7.Text = "Status";
			columnItem7.ValueFormat = FormatType.Text;
			columnItem7.Visible = true;
			columnItem7.Width = 13;
			columnItem8.Alignment = StringAlignment.Center;
			columnItem8.BackColor = Color.FromArgb(64, 64, 64);
			columnItem8.FontColor = Color.LightGray;
			columnItem8.IsExpand = false;
			columnItem8.MyStyle = FontStyle.Regular;
			columnItem8.Name = "sent_time";
			columnItem8.Text = "Time";
			columnItem8.ValueFormat = FormatType.Text;
			columnItem8.Visible = true;
			columnItem8.Width = 12;
			columnItem9.Alignment = StringAlignment.Far;
			columnItem9.BackColor = Color.FromArgb(64, 64, 64);
			columnItem9.FontColor = Color.LightGray;
			columnItem9.IsExpand = false;
			columnItem9.MyStyle = FontStyle.Regular;
			columnItem9.Name = "pubvol";
			columnItem9.Text = "Publish";
			columnItem9.ValueFormat = FormatType.Volume;
			columnItem9.Visible = true;
			columnItem9.Width = 12;
			columnItem10.Alignment = StringAlignment.Center;
			columnItem10.BackColor = Color.FromArgb(64, 64, 64);
			columnItem10.FontColor = Color.LightGray;
			columnItem10.IsExpand = false;
			columnItem10.MyStyle = FontStyle.Regular;
			columnItem10.Name = "account";
			columnItem10.Text = "Account";
			columnItem10.ValueFormat = FormatType.Text;
			columnItem10.Visible = true;
			columnItem10.Width = 10;
			columnItem11.Alignment = StringAlignment.Center;
			columnItem11.BackColor = Color.FromArgb(64, 64, 64);
			columnItem11.FontColor = Color.LightGray;
			columnItem11.IsExpand = false;
			columnItem11.MyStyle = FontStyle.Regular;
			columnItem11.Name = "price_cond";
			columnItem11.Text = "Price Cond";
			columnItem11.ValueFormat = FormatType.Text;
			columnItem11.Visible = true;
			columnItem11.Width = 12;
			columnItem12.Alignment = StringAlignment.Center;
			columnItem12.BackColor = Color.FromArgb(64, 64, 64);
			columnItem12.FontColor = Color.LightGray;
			columnItem12.IsExpand = false;
			columnItem12.MyStyle = FontStyle.Regular;
			columnItem12.Name = "ttf";
			columnItem12.Text = "TTF";
			columnItem12.ValueFormat = FormatType.Volume;
			columnItem12.Visible = true;
			columnItem12.Width = 5;
			columnItem13.Alignment = StringAlignment.Center;
			columnItem13.BackColor = Color.FromArgb(64, 64, 64);
			columnItem13.FontColor = Color.LightGray;
			columnItem13.IsExpand = false;
			columnItem13.MyStyle = FontStyle.Regular;
			columnItem13.Name = "limit";
			columnItem13.Text = "Cancel End Day";
			columnItem13.ValueFormat = FormatType.Text;
			columnItem13.Visible = true;
			columnItem13.Width = 16;
			columnItem14.Alignment = StringAlignment.Center;
			columnItem14.BackColor = Color.FromArgb(64, 64, 64);
			columnItem14.FontColor = Color.LightGray;
			columnItem14.IsExpand = false;
			columnItem14.MyStyle = FontStyle.Regular;
			columnItem14.Name = "ref_no";
			columnItem14.Text = "Ref No.";
			columnItem14.ValueFormat = FormatType.Text;
			columnItem14.Visible = true;
			columnItem14.Width = 10;
			columnItem15.Alignment = StringAlignment.Center;
			columnItem15.BackColor = Color.FromArgb(64, 64, 64);
			columnItem15.FontColor = Color.LightGray;
			columnItem15.IsExpand = false;
			columnItem15.MyStyle = FontStyle.Regular;
			columnItem15.Name = "matched_time";
			columnItem15.Text = "S-Time";
			columnItem15.ValueFormat = FormatType.Text;
			columnItem15.Visible = true;
			columnItem15.Width = 10;
			columnItem16.Alignment = StringAlignment.Center;
			columnItem16.BackColor = Color.FromArgb(64, 64, 64);
			columnItem16.FontColor = Color.LightGray;
			columnItem16.IsExpand = false;
			columnItem16.MyStyle = FontStyle.Regular;
			columnItem16.Name = "order_no";
			columnItem16.Text = "Order No";
			columnItem16.ValueFormat = FormatType.Text;
			columnItem16.Visible = true;
			columnItem16.Width = 12;
			columnItem17.Alignment = StringAlignment.Near;
			columnItem17.BackColor = Color.FromArgb(64, 64, 64);
			columnItem17.FontColor = Color.LightGray;
			columnItem17.IsExpand = false;
			columnItem17.MyStyle = FontStyle.Regular;
			columnItem17.Name = "error";
			columnItem17.Text = "Error";
			columnItem17.ValueFormat = FormatType.Text;
			columnItem17.Visible = true;
			columnItem17.Width = 70;
			columnItem18.Alignment = StringAlignment.Near;
			columnItem18.BackColor = Color.FromArgb(64, 64, 64);
			columnItem18.FontColor = Color.LightGray;
			columnItem18.IsExpand = false;
			columnItem18.MyStyle = FontStyle.Regular;
			columnItem18.Name = "con_price";
			columnItem18.Text = "Price Cond";
			columnItem18.ValueFormat = FormatType.Text;
			columnItem18.Visible = false;
			columnItem18.Width = 10;
			columnItem19.Alignment = StringAlignment.Near;
			columnItem19.BackColor = Color.FromArgb(64, 64, 64);
			columnItem19.FontColor = Color.LightGray;
			columnItem19.IsExpand = false;
			columnItem19.MyStyle = FontStyle.Regular;
			columnItem19.Name = "con_operator";
			columnItem19.Text = "None";
			columnItem19.ValueFormat = FormatType.Text;
			columnItem19.Visible = false;
			columnItem19.Width = 10;
			this.intzaStopOrder.Columns.Add(columnItem);
			this.intzaStopOrder.Columns.Add(columnItem2);
			this.intzaStopOrder.Columns.Add(columnItem3);
			this.intzaStopOrder.Columns.Add(columnItem4);
			this.intzaStopOrder.Columns.Add(columnItem5);
			this.intzaStopOrder.Columns.Add(columnItem6);
			this.intzaStopOrder.Columns.Add(columnItem7);
			this.intzaStopOrder.Columns.Add(columnItem8);
			this.intzaStopOrder.Columns.Add(columnItem9);
			this.intzaStopOrder.Columns.Add(columnItem10);
			this.intzaStopOrder.Columns.Add(columnItem11);
			this.intzaStopOrder.Columns.Add(columnItem12);
			this.intzaStopOrder.Columns.Add(columnItem13);
			this.intzaStopOrder.Columns.Add(columnItem14);
			this.intzaStopOrder.Columns.Add(columnItem15);
			this.intzaStopOrder.Columns.Add(columnItem16);
			this.intzaStopOrder.Columns.Add(columnItem17);
			this.intzaStopOrder.Columns.Add(columnItem18);
			this.intzaStopOrder.Columns.Add(columnItem19);
			this.intzaStopOrder.CurrentScroll = 0;
			this.intzaStopOrder.FocusItemIndex = -1;
			this.intzaStopOrder.ForeColor = Color.Black;
			this.intzaStopOrder.GridColor = Color.FromArgb(45, 45, 45);
			this.intzaStopOrder.HeaderPctHeight = 100f;
			this.intzaStopOrder.IsAutoRepaint = true;
			this.intzaStopOrder.IsDrawGrid = true;
			this.intzaStopOrder.IsDrawHeader = true;
			this.intzaStopOrder.IsScrollable = true;
			this.intzaStopOrder.Location = new Point(3, 315);
			this.intzaStopOrder.MainColumn = "";
			this.intzaStopOrder.Name = "intzaStopOrder";
			this.intzaStopOrder.Rows = 0;
			this.intzaStopOrder.RowSelectColor = Color.Navy;
			this.intzaStopOrder.RowSelectType = 3;
			this.intzaStopOrder.ScrollChennelColor = Color.Gray;
			this.intzaStopOrder.Size = new Size(815, 63);
			this.intzaStopOrder.SortColumnName = "";
			this.intzaStopOrder.SortType = SortType.Desc;
			this.intzaStopOrder.TabIndex = 70;
			this.intzaStopOrder.TableMouseClick += new ExpandGrid.TableMouseClickEventHandler(this.intzaStopOrder_TableMouseClick);
			this.intzaReOrderList.AllowDrop = true;
			this.intzaReOrderList.BackColor = Color.FromArgb(10, 10, 10);
			this.intzaReOrderList.CanBlink = false;
			this.intzaReOrderList.CanDrag = false;
			this.intzaReOrderList.CanGetMouseMove = false;
			columnItem20.Alignment = StringAlignment.Center;
			columnItem20.BackColor = Color.FromArgb(64, 64, 64);
			columnItem20.ColumnAlignment = StringAlignment.Center;
			columnItem20.FontColor = Color.LightGray;
			columnItem20.MyStyle = FontStyle.Regular;
			columnItem20.Name = "checkbox";
			columnItem20.Text = "";
			columnItem20.ValueFormat = FormatType.Bitmap;
			columnItem20.Visible = false;
			columnItem20.Width = 3;
			columnItem21.Alignment = StringAlignment.Near;
			columnItem21.BackColor = Color.FromArgb(64, 64, 64);
			columnItem21.ColumnAlignment = StringAlignment.Center;
			columnItem21.FontColor = Color.LightGray;
			columnItem21.MyStyle = FontStyle.Regular;
			columnItem21.Name = "order_number";
			columnItem21.Text = "Order No.";
			columnItem21.ValueFormat = FormatType.Text;
			columnItem21.Visible = true;
			columnItem21.Width = 8;
			columnItem22.Alignment = StringAlignment.Center;
			columnItem22.BackColor = Color.FromArgb(64, 64, 64);
			columnItem22.ColumnAlignment = StringAlignment.Center;
			columnItem22.FontColor = Color.LightGray;
			columnItem22.MyStyle = FontStyle.Regular;
			columnItem22.Name = "side";
			columnItem22.Text = "B/S";
			columnItem22.ValueFormat = FormatType.Text;
			columnItem22.Visible = true;
			columnItem22.Width = 4;
			columnItem23.Alignment = StringAlignment.Near;
			columnItem23.BackColor = Color.FromArgb(64, 64, 64);
			columnItem23.ColumnAlignment = StringAlignment.Center;
			columnItem23.FontColor = Color.LightGray;
			columnItem23.MyStyle = FontStyle.Regular;
			columnItem23.Name = "stock";
			columnItem23.Text = "Stock";
			columnItem23.ValueFormat = FormatType.Text;
			columnItem23.Visible = true;
			columnItem23.Width = 10;
			columnItem24.Alignment = StringAlignment.Center;
			columnItem24.BackColor = Color.FromArgb(64, 64, 64);
			columnItem24.ColumnAlignment = StringAlignment.Center;
			columnItem24.FontColor = Color.LightGray;
			columnItem24.MyStyle = FontStyle.Regular;
			columnItem24.Name = "ttf";
			columnItem24.Text = "TTF";
			columnItem24.ValueFormat = FormatType.Text;
			columnItem24.Visible = true;
			columnItem24.Width = 4;
			columnItem25.Alignment = StringAlignment.Far;
			columnItem25.BackColor = Color.FromArgb(64, 64, 64);
			columnItem25.ColumnAlignment = StringAlignment.Center;
			columnItem25.FontColor = Color.LightGray;
			columnItem25.MyStyle = FontStyle.Regular;
			columnItem25.Name = "volume";
			columnItem25.Text = "Volume";
			columnItem25.ValueFormat = FormatType.Volume;
			columnItem25.Visible = true;
			columnItem25.Width = 9;
			columnItem26.Alignment = StringAlignment.Far;
			columnItem26.BackColor = Color.FromArgb(64, 64, 64);
			columnItem26.ColumnAlignment = StringAlignment.Center;
			columnItem26.FontColor = Color.LightGray;
			columnItem26.MyStyle = FontStyle.Regular;
			columnItem26.Name = "price";
			columnItem26.Text = "Price";
			columnItem26.ValueFormat = FormatType.Text;
			columnItem26.Visible = true;
			columnItem26.Width = 7;
			columnItem27.Alignment = StringAlignment.Near;
			columnItem27.BackColor = Color.FromArgb(64, 64, 64);
			columnItem27.ColumnAlignment = StringAlignment.Center;
			columnItem27.FontColor = Color.LightGray;
			columnItem27.MyStyle = FontStyle.Regular;
			columnItem27.Name = "cond";
			columnItem27.Text = "Cond";
			columnItem27.ValueFormat = FormatType.Text;
			columnItem27.Visible = true;
			columnItem27.Width = 6;
			columnItem28.Alignment = StringAlignment.Near;
			columnItem28.BackColor = Color.FromArgb(64, 64, 64);
			columnItem28.ColumnAlignment = StringAlignment.Center;
			columnItem28.FontColor = Color.LightGray;
			columnItem28.MyStyle = FontStyle.Regular;
			columnItem28.Name = "deposit";
			columnItem28.Text = "Dep";
			columnItem28.ValueFormat = FormatType.Text;
			columnItem28.Visible = false;
			columnItem28.Width = 6;
			columnItem29.Alignment = StringAlignment.Far;
			columnItem29.BackColor = Color.FromArgb(64, 64, 64);
			columnItem29.ColumnAlignment = StringAlignment.Center;
			columnItem29.FontColor = Color.LightGray;
			columnItem29.MyStyle = FontStyle.Regular;
			columnItem29.Name = "matched";
			columnItem29.Text = "Matched";
			columnItem29.ValueFormat = FormatType.Volume;
			columnItem29.Visible = true;
			columnItem29.Width = 8;
			columnItem30.Alignment = StringAlignment.Far;
			columnItem30.BackColor = Color.FromArgb(64, 64, 64);
			columnItem30.ColumnAlignment = StringAlignment.Center;
			columnItem30.FontColor = Color.LightGray;
			columnItem30.MyStyle = FontStyle.Regular;
			columnItem30.Name = "published";
			columnItem30.Text = "Publish";
			columnItem30.ValueFormat = FormatType.Volume;
			columnItem30.Visible = true;
			columnItem30.Width = 7;
			columnItem31.Alignment = StringAlignment.Center;
			columnItem31.BackColor = Color.FromArgb(64, 64, 64);
			columnItem31.ColumnAlignment = StringAlignment.Center;
			columnItem31.FontColor = Color.LightGray;
			columnItem31.MyStyle = FontStyle.Regular;
			columnItem31.Name = "status";
			columnItem31.Text = "Status";
			columnItem31.ValueFormat = FormatType.Text;
			columnItem31.Visible = true;
			columnItem31.Width = 9;
			columnItem32.Alignment = StringAlignment.Center;
			columnItem32.BackColor = Color.FromArgb(64, 64, 64);
			columnItem32.ColumnAlignment = StringAlignment.Center;
			columnItem32.FontColor = Color.LightGray;
			columnItem32.MyStyle = FontStyle.Regular;
			columnItem32.Name = "time";
			columnItem32.Text = "Time";
			columnItem32.ValueFormat = FormatType.Text;
			columnItem32.Visible = true;
			columnItem32.Width = 7;
			columnItem33.Alignment = StringAlignment.Center;
			columnItem33.BackColor = Color.FromArgb(64, 64, 64);
			columnItem33.ColumnAlignment = StringAlignment.Center;
			columnItem33.FontColor = Color.LightGray;
			columnItem33.MyStyle = FontStyle.Regular;
			columnItem33.Name = "quote";
			columnItem33.Text = "Quote";
			columnItem33.ValueFormat = FormatType.Text;
			columnItem33.Visible = true;
			columnItem33.Width = 5;
			columnItem34.Alignment = StringAlignment.Near;
			columnItem34.BackColor = Color.FromArgb(64, 64, 64);
			columnItem34.ColumnAlignment = StringAlignment.Center;
			columnItem34.FontColor = Color.LightGray;
			columnItem34.MyStyle = FontStyle.Regular;
			columnItem34.Name = "result";
			columnItem34.Text = "Result";
			columnItem34.ValueFormat = FormatType.Text;
			columnItem34.Visible = true;
			columnItem34.Width = 16;
			columnItem35.Alignment = StringAlignment.Near;
			columnItem35.BackColor = Color.FromArgb(64, 64, 64);
			columnItem35.ColumnAlignment = StringAlignment.Center;
			columnItem35.FontColor = Color.LightGray;
			columnItem35.MyStyle = FontStyle.Regular;
			columnItem35.Name = "send_date";
			columnItem35.Text = "None";
			columnItem35.ValueFormat = FormatType.Text;
			columnItem35.Visible = false;
			columnItem35.Width = 7;
			columnItem36.Alignment = StringAlignment.Near;
			columnItem36.BackColor = Color.FromArgb(64, 64, 64);
			columnItem36.ColumnAlignment = StringAlignment.Center;
			columnItem36.FontColor = Color.LightGray;
			columnItem36.MyStyle = FontStyle.Regular;
			columnItem36.Name = "key";
			columnItem36.Text = "None";
			columnItem36.ValueFormat = FormatType.Text;
			columnItem36.Visible = false;
			columnItem36.Width = 10;
			columnItem37.Alignment = StringAlignment.Near;
			columnItem37.BackColor = Color.FromArgb(64, 64, 64);
			columnItem37.ColumnAlignment = StringAlignment.Center;
			columnItem37.FontColor = Color.LightGray;
			columnItem37.MyStyle = FontStyle.Regular;
			columnItem37.Name = "info";
			columnItem37.Text = "";
			columnItem37.ValueFormat = FormatType.Bitmap;
			columnItem37.Visible = true;
			columnItem37.Width = 3;
			this.intzaReOrderList.Columns.Add(columnItem20);
			this.intzaReOrderList.Columns.Add(columnItem21);
			this.intzaReOrderList.Columns.Add(columnItem22);
			this.intzaReOrderList.Columns.Add(columnItem23);
			this.intzaReOrderList.Columns.Add(columnItem24);
			this.intzaReOrderList.Columns.Add(columnItem25);
			this.intzaReOrderList.Columns.Add(columnItem26);
			this.intzaReOrderList.Columns.Add(columnItem27);
			this.intzaReOrderList.Columns.Add(columnItem28);
			this.intzaReOrderList.Columns.Add(columnItem29);
			this.intzaReOrderList.Columns.Add(columnItem30);
			this.intzaReOrderList.Columns.Add(columnItem31);
			this.intzaReOrderList.Columns.Add(columnItem32);
			this.intzaReOrderList.Columns.Add(columnItem33);
			this.intzaReOrderList.Columns.Add(columnItem34);
			this.intzaReOrderList.Columns.Add(columnItem35);
			this.intzaReOrderList.Columns.Add(columnItem36);
			this.intzaReOrderList.Columns.Add(columnItem37);
			this.intzaReOrderList.CurrentScroll = 0;
			this.intzaReOrderList.FocusItemIndex = -1;
			this.intzaReOrderList.ForeColor = Color.Black;
			this.intzaReOrderList.GridColor = Color.FromArgb(45, 45, 45);
			this.intzaReOrderList.HeaderPctHeight = 80f;
			this.intzaReOrderList.IsAutoRepaint = true;
			this.intzaReOrderList.IsDrawFullRow = false;
			this.intzaReOrderList.IsDrawGrid = true;
			this.intzaReOrderList.IsDrawHeader = true;
			this.intzaReOrderList.IsScrollable = true;
			this.intzaReOrderList.Location = new Point(0, 177);
			this.intzaReOrderList.MainColumn = "";
			this.intzaReOrderList.Name = "intzaReOrderList";
			this.intzaReOrderList.Rows = 0;
			this.intzaReOrderList.RowSelectColor = Color.FromArgb(0, 0, 128);
			this.intzaReOrderList.RowSelectType = 3;
			this.intzaReOrderList.RowsVisible = 0;
			this.intzaReOrderList.ScrollChennelColor = Color.Gray;
			this.intzaReOrderList.Size = new Size(815, 55);
			this.intzaReOrderList.SortColumnName = "";
			this.intzaReOrderList.SortType = SortType.Desc;
			this.intzaReOrderList.TabIndex = 68;
			this.intzaReOrderList.Visible = false;
			this.intzaOrderListTFEX.AllowDrop = true;
			this.intzaOrderListTFEX.BackColor = Color.FromArgb(10, 10, 10);
			this.intzaOrderListTFEX.CanBlink = false;
			this.intzaOrderListTFEX.CanDrag = false;
			this.intzaOrderListTFEX.CanGetMouseMove = false;
			columnItem38.Alignment = StringAlignment.Center;
			columnItem38.BackColor = Color.FromArgb(64, 64, 64);
			columnItem38.ColumnAlignment = StringAlignment.Center;
			columnItem38.FontColor = Color.LightGray;
			columnItem38.MyStyle = FontStyle.Regular;
			columnItem38.Name = "checkbox";
			columnItem38.Text = "";
			columnItem38.ValueFormat = FormatType.Bitmap;
			columnItem38.Visible = true;
			columnItem38.Width = 3;
			columnItem39.Alignment = StringAlignment.Near;
			columnItem39.BackColor = Color.FromArgb(64, 64, 64);
			columnItem39.ColumnAlignment = StringAlignment.Center;
			columnItem39.FontColor = Color.LightGray;
			columnItem39.MyStyle = FontStyle.Regular;
			columnItem39.Name = "order_number";
			columnItem39.Text = "Order No.";
			columnItem39.ValueFormat = FormatType.Text;
			columnItem39.Visible = true;
			columnItem39.Width = 10;
			columnItem40.Alignment = StringAlignment.Near;
			columnItem40.BackColor = Color.FromArgb(64, 64, 64);
			columnItem40.ColumnAlignment = StringAlignment.Center;
			columnItem40.FontColor = Color.LightGray;
			columnItem40.MyStyle = FontStyle.Regular;
			columnItem40.Name = "position";
			columnItem40.Text = "Pos";
			columnItem40.ValueFormat = FormatType.Text;
			columnItem40.Visible = true;
			columnItem40.Width = 6;
			columnItem41.Alignment = StringAlignment.Center;
			columnItem41.BackColor = Color.FromArgb(64, 64, 64);
			columnItem41.ColumnAlignment = StringAlignment.Center;
			columnItem41.FontColor = Color.LightGray;
			columnItem41.MyStyle = FontStyle.Regular;
			columnItem41.Name = "side";
			columnItem41.Text = "B/S";
			columnItem41.ValueFormat = FormatType.Text;
			columnItem41.Visible = true;
			columnItem41.Width = 5;
			columnItem42.Alignment = StringAlignment.Near;
			columnItem42.BackColor = Color.FromArgb(64, 64, 64);
			columnItem42.ColumnAlignment = StringAlignment.Center;
			columnItem42.FontColor = Color.LightGray;
			columnItem42.MyStyle = FontStyle.Regular;
			columnItem42.Name = "stock";
			columnItem42.Text = "Series";
			columnItem42.ValueFormat = FormatType.Text;
			columnItem42.Visible = true;
			columnItem42.Width = 10;
			columnItem43.Alignment = StringAlignment.Far;
			columnItem43.BackColor = Color.FromArgb(64, 64, 64);
			columnItem43.ColumnAlignment = StringAlignment.Center;
			columnItem43.FontColor = Color.LightGray;
			columnItem43.MyStyle = FontStyle.Regular;
			columnItem43.Name = "volume";
			columnItem43.Text = "Volume";
			columnItem43.ValueFormat = FormatType.Volume;
			columnItem43.Visible = true;
			columnItem43.Width = 7;
			columnItem44.Alignment = StringAlignment.Far;
			columnItem44.BackColor = Color.FromArgb(64, 64, 64);
			columnItem44.ColumnAlignment = StringAlignment.Center;
			columnItem44.FontColor = Color.LightGray;
			columnItem44.MyStyle = FontStyle.Regular;
			columnItem44.Name = "price";
			columnItem44.Text = "Price";
			columnItem44.ValueFormat = FormatType.Text;
			columnItem44.Visible = true;
			columnItem44.Width = 12;
			columnItem45.Alignment = StringAlignment.Far;
			columnItem45.BackColor = Color.FromArgb(64, 64, 64);
			columnItem45.ColumnAlignment = StringAlignment.Center;
			columnItem45.FontColor = Color.LightGray;
			columnItem45.MyStyle = FontStyle.Regular;
			columnItem45.Name = "matched";
			columnItem45.Text = "Matched";
			columnItem45.ValueFormat = FormatType.Volume;
			columnItem45.Visible = true;
			columnItem45.Width = 10;
			columnItem46.Alignment = StringAlignment.Far;
			columnItem46.BackColor = Color.FromArgb(64, 64, 64);
			columnItem46.ColumnAlignment = StringAlignment.Center;
			columnItem46.FontColor = Color.LightGray;
			columnItem46.MyStyle = FontStyle.Regular;
			columnItem46.Name = "published";
			columnItem46.Text = "Publish";
			columnItem46.ValueFormat = FormatType.Volume;
			columnItem46.Visible = true;
			columnItem46.Width = 9;
			columnItem47.Alignment = StringAlignment.Near;
			columnItem47.BackColor = Color.FromArgb(64, 64, 64);
			columnItem47.ColumnAlignment = StringAlignment.Center;
			columnItem47.FontColor = Color.LightGray;
			columnItem47.MyStyle = FontStyle.Regular;
			columnItem47.Name = "valid";
			columnItem47.Text = "Valid";
			columnItem47.ValueFormat = FormatType.Text;
			columnItem47.Visible = false;
			columnItem47.Width = 5;
			columnItem48.Alignment = StringAlignment.Center;
			columnItem48.BackColor = Color.FromArgb(64, 64, 64);
			columnItem48.ColumnAlignment = StringAlignment.Center;
			columnItem48.FontColor = Color.LightGray;
			columnItem48.MyStyle = FontStyle.Regular;
			columnItem48.Name = "status";
			columnItem48.Text = "Status";
			columnItem48.ValueFormat = FormatType.Text;
			columnItem48.Visible = true;
			columnItem48.Width = 13;
			columnItem49.Alignment = StringAlignment.Center;
			columnItem49.BackColor = Color.FromArgb(64, 64, 64);
			columnItem49.ColumnAlignment = StringAlignment.Center;
			columnItem49.FontColor = Color.LightGray;
			columnItem49.MyStyle = FontStyle.Regular;
			columnItem49.Name = "time";
			columnItem49.Text = "Time";
			columnItem49.ValueFormat = FormatType.Text;
			columnItem49.Visible = true;
			columnItem49.Width = 9;
			columnItem50.Alignment = StringAlignment.Center;
			columnItem50.BackColor = Color.FromArgb(64, 64, 64);
			columnItem50.ColumnAlignment = StringAlignment.Center;
			columnItem50.FontColor = Color.LightGray;
			columnItem50.MyStyle = FontStyle.Regular;
			columnItem50.Name = "quote";
			columnItem50.Text = "Quote";
			columnItem50.ValueFormat = FormatType.Text;
			columnItem50.Visible = true;
			columnItem50.Width = 6;
			columnItem51.Alignment = StringAlignment.Near;
			columnItem51.BackColor = Color.FromArgb(64, 64, 64);
			columnItem51.ColumnAlignment = StringAlignment.Center;
			columnItem51.FontColor = Color.LightGray;
			columnItem51.MyStyle = FontStyle.Regular;
			columnItem51.Name = "send_date";
			columnItem51.Text = "None";
			columnItem51.ValueFormat = FormatType.Text;
			columnItem51.Visible = false;
			columnItem51.Width = 10;
			columnItem52.Alignment = StringAlignment.Near;
			columnItem52.BackColor = Color.FromArgb(64, 64, 64);
			columnItem52.ColumnAlignment = StringAlignment.Center;
			columnItem52.FontColor = Color.LightGray;
			columnItem52.MyStyle = FontStyle.Regular;
			columnItem52.Name = "key";
			columnItem52.Text = "None";
			columnItem52.ValueFormat = FormatType.Text;
			columnItem52.Visible = false;
			columnItem52.Width = 10;
			columnItem53.Alignment = StringAlignment.Near;
			columnItem53.BackColor = Color.FromArgb(64, 64, 64);
			columnItem53.ColumnAlignment = StringAlignment.Center;
			columnItem53.FontColor = Color.LightGray;
			columnItem53.MyStyle = FontStyle.Regular;
			columnItem53.Name = "ordType";
			columnItem53.Text = "ordType";
			columnItem53.ValueFormat = FormatType.Bitmap;
			columnItem53.Visible = false;
			columnItem53.Width = 10;
			this.intzaOrderListTFEX.Columns.Add(columnItem38);
			this.intzaOrderListTFEX.Columns.Add(columnItem39);
			this.intzaOrderListTFEX.Columns.Add(columnItem40);
			this.intzaOrderListTFEX.Columns.Add(columnItem41);
			this.intzaOrderListTFEX.Columns.Add(columnItem42);
			this.intzaOrderListTFEX.Columns.Add(columnItem43);
			this.intzaOrderListTFEX.Columns.Add(columnItem44);
			this.intzaOrderListTFEX.Columns.Add(columnItem45);
			this.intzaOrderListTFEX.Columns.Add(columnItem46);
			this.intzaOrderListTFEX.Columns.Add(columnItem47);
			this.intzaOrderListTFEX.Columns.Add(columnItem48);
			this.intzaOrderListTFEX.Columns.Add(columnItem49);
			this.intzaOrderListTFEX.Columns.Add(columnItem50);
			this.intzaOrderListTFEX.Columns.Add(columnItem51);
			this.intzaOrderListTFEX.Columns.Add(columnItem52);
			this.intzaOrderListTFEX.Columns.Add(columnItem53);
			this.intzaOrderListTFEX.CurrentScroll = 0;
			this.intzaOrderListTFEX.FocusItemIndex = -1;
			this.intzaOrderListTFEX.ForeColor = Color.Black;
			this.intzaOrderListTFEX.GridColor = Color.FromArgb(45, 45, 45);
			this.intzaOrderListTFEX.HeaderPctHeight = 80f;
			this.intzaOrderListTFEX.IsAutoRepaint = true;
			this.intzaOrderListTFEX.IsDrawFullRow = false;
			this.intzaOrderListTFEX.IsDrawGrid = true;
			this.intzaOrderListTFEX.IsDrawHeader = true;
			this.intzaOrderListTFEX.IsScrollable = true;
			this.intzaOrderListTFEX.Location = new Point(0, 124);
			this.intzaOrderListTFEX.MainColumn = "";
			this.intzaOrderListTFEX.Name = "intzaOrderListTFEX";
			this.intzaOrderListTFEX.Rows = 0;
			this.intzaOrderListTFEX.RowSelectColor = Color.FromArgb(0, 0, 128);
			this.intzaOrderListTFEX.RowSelectType = 3;
			this.intzaOrderListTFEX.RowsVisible = 0;
			this.intzaOrderListTFEX.ScrollChennelColor = Color.Gray;
			this.intzaOrderListTFEX.Size = new Size(818, 47);
			this.intzaOrderListTFEX.SortColumnName = "";
			this.intzaOrderListTFEX.SortType = SortType.Desc;
			this.intzaOrderListTFEX.TabIndex = 67;
			this.intzaOrderListTFEX.Visible = false;
			this.intzaOrderListTFEX.MouseClick += new MouseEventHandler(this.intzaOrderListTFEX_MouseClick);
			this.intzaOrderListTFEX.TableMouseClick += new SortGrid.TableMouseClickEventHandler(this.intzaOrderListTFEX_TableMouseClick);
			this.intzaOrderListTFEX.TableMouseDoubleClick += new SortGrid.TableMouseDoubleClickEventHandler(this.intzaOrderListTFEX_TableMouseDoubleClick);
			this.intzaOrderList.AllowDrop = true;
			this.intzaOrderList.BackColor = Color.FromArgb(10, 10, 10);
			this.intzaOrderList.CanBlink = false;
			this.intzaOrderList.CanDrag = false;
			this.intzaOrderList.CanGetMouseMove = false;
			columnItem54.Alignment = StringAlignment.Center;
			columnItem54.BackColor = Color.FromArgb(64, 64, 64);
			columnItem54.ColumnAlignment = StringAlignment.Center;
			columnItem54.FontColor = Color.LightGray;
			columnItem54.MyStyle = FontStyle.Regular;
			columnItem54.Name = "checkbox";
			columnItem54.Text = "";
			columnItem54.ValueFormat = FormatType.Bitmap;
			columnItem54.Visible = true;
			columnItem54.Width = 3;
			columnItem55.Alignment = StringAlignment.Near;
			columnItem55.BackColor = Color.FromArgb(64, 64, 64);
			columnItem55.ColumnAlignment = StringAlignment.Center;
			columnItem55.FontColor = Color.LightGray;
			columnItem55.MyStyle = FontStyle.Regular;
			columnItem55.Name = "order_number";
			columnItem55.Text = "Order No.";
			columnItem55.ValueFormat = FormatType.Text;
			columnItem55.Visible = true;
			columnItem55.Width = 11;
			columnItem56.Alignment = StringAlignment.Center;
			columnItem56.BackColor = Color.FromArgb(64, 64, 64);
			columnItem56.ColumnAlignment = StringAlignment.Center;
			columnItem56.FontColor = Color.LightGray;
			columnItem56.MyStyle = FontStyle.Regular;
			columnItem56.Name = "side";
			columnItem56.Text = "B/S";
			columnItem56.ValueFormat = FormatType.Text;
			columnItem56.Visible = true;
			columnItem56.Width = 4;
			columnItem57.Alignment = StringAlignment.Near;
			columnItem57.BackColor = Color.FromArgb(64, 64, 64);
			columnItem57.ColumnAlignment = StringAlignment.Center;
			columnItem57.FontColor = Color.LightGray;
			columnItem57.MyStyle = FontStyle.Regular;
			columnItem57.Name = "stock";
			columnItem57.Text = "Stock";
			columnItem57.ValueFormat = FormatType.Text;
			columnItem57.Visible = true;
			columnItem57.Width = 10;
			columnItem58.Alignment = StringAlignment.Center;
			columnItem58.BackColor = Color.FromArgb(64, 64, 64);
			columnItem58.ColumnAlignment = StringAlignment.Center;
			columnItem58.FontColor = Color.LightGray;
			columnItem58.MyStyle = FontStyle.Regular;
			columnItem58.Name = "ttf";
			columnItem58.Text = "TTF";
			columnItem58.ValueFormat = FormatType.Text;
			columnItem58.Visible = true;
			columnItem58.Width = 4;
			columnItem59.Alignment = StringAlignment.Far;
			columnItem59.BackColor = Color.FromArgb(64, 64, 64);
			columnItem59.ColumnAlignment = StringAlignment.Center;
			columnItem59.FontColor = Color.LightGray;
			columnItem59.MyStyle = FontStyle.Regular;
			columnItem59.Name = "volume";
			columnItem59.Text = "Volume";
			columnItem59.ValueFormat = FormatType.Volume;
			columnItem59.Visible = true;
			columnItem59.Width = 10;
			columnItem60.Alignment = StringAlignment.Far;
			columnItem60.BackColor = Color.FromArgb(64, 64, 64);
			columnItem60.ColumnAlignment = StringAlignment.Center;
			columnItem60.FontColor = Color.LightGray;
			columnItem60.MyStyle = FontStyle.Regular;
			columnItem60.Name = "price";
			columnItem60.Text = "Price";
			columnItem60.ValueFormat = FormatType.Text;
			columnItem60.Visible = true;
			columnItem60.Width = 7;
			columnItem61.Alignment = StringAlignment.Far;
			columnItem61.BackColor = Color.FromArgb(64, 64, 64);
			columnItem61.ColumnAlignment = StringAlignment.Center;
			columnItem61.FontColor = Color.LightGray;
			columnItem61.MyStyle = FontStyle.Regular;
			columnItem61.Name = "matched";
			columnItem61.Text = "Matched";
			columnItem61.ValueFormat = FormatType.Volume;
			columnItem61.Visible = true;
			columnItem61.Width = 10;
			columnItem62.Alignment = StringAlignment.Far;
			columnItem62.BackColor = Color.FromArgb(64, 64, 64);
			columnItem62.ColumnAlignment = StringAlignment.Center;
			columnItem62.FontColor = Color.LightGray;
			columnItem62.MyStyle = FontStyle.Regular;
			columnItem62.Name = "published";
			columnItem62.Text = "Publish";
			columnItem62.ValueFormat = FormatType.Volume;
			columnItem62.Visible = true;
			columnItem62.Width = 10;
			columnItem63.Alignment = StringAlignment.Center;
			columnItem63.BackColor = Color.FromArgb(64, 64, 64);
			columnItem63.ColumnAlignment = StringAlignment.Center;
			columnItem63.FontColor = Color.LightGray;
			columnItem63.MyStyle = FontStyle.Regular;
			columnItem63.Name = "status";
			columnItem63.Text = "Status";
			columnItem63.ValueFormat = FormatType.Text;
			columnItem63.Visible = true;
			columnItem63.Width = 14;
			columnItem64.Alignment = StringAlignment.Center;
			columnItem64.BackColor = Color.FromArgb(64, 64, 64);
			columnItem64.ColumnAlignment = StringAlignment.Center;
			columnItem64.FontColor = Color.LightGray;
			columnItem64.MyStyle = FontStyle.Regular;
			columnItem64.Name = "time";
			columnItem64.Text = "Time";
			columnItem64.ValueFormat = FormatType.Text;
			columnItem64.Visible = true;
			columnItem64.Width = 9;
			columnItem65.Alignment = StringAlignment.Center;
			columnItem65.BackColor = Color.FromArgb(64, 64, 64);
			columnItem65.ColumnAlignment = StringAlignment.Center;
			columnItem65.FontColor = Color.LightGray;
			columnItem65.MyStyle = FontStyle.Regular;
			columnItem65.Name = "quote";
			columnItem65.Text = "Quote";
			columnItem65.ValueFormat = FormatType.Text;
			columnItem65.Visible = true;
			columnItem65.Width = 5;
			columnItem66.Alignment = StringAlignment.Near;
			columnItem66.BackColor = Color.FromArgb(64, 64, 64);
			columnItem66.ColumnAlignment = StringAlignment.Center;
			columnItem66.FontColor = Color.LightGray;
			columnItem66.MyStyle = FontStyle.Regular;
			columnItem66.Name = "send_date";
			columnItem66.Text = "None";
			columnItem66.ValueFormat = FormatType.Text;
			columnItem66.Visible = false;
			columnItem66.Width = 7;
			columnItem67.Alignment = StringAlignment.Near;
			columnItem67.BackColor = Color.FromArgb(64, 64, 64);
			columnItem67.ColumnAlignment = StringAlignment.Center;
			columnItem67.FontColor = Color.LightGray;
			columnItem67.MyStyle = FontStyle.Regular;
			columnItem67.Name = "key";
			columnItem67.Text = "None";
			columnItem67.ValueFormat = FormatType.Text;
			columnItem67.Visible = false;
			columnItem67.Width = 10;
			columnItem68.Alignment = StringAlignment.Near;
			columnItem68.BackColor = Color.FromArgb(64, 64, 64);
			columnItem68.ColumnAlignment = StringAlignment.Center;
			columnItem68.FontColor = Color.LightGray;
			columnItem68.MyStyle = FontStyle.Regular;
			columnItem68.Name = "info";
			columnItem68.Text = "";
			columnItem68.ValueFormat = FormatType.Bitmap;
			columnItem68.Visible = true;
			columnItem68.Width = 3;
			columnItem69.Alignment = StringAlignment.Near;
			columnItem69.BackColor = Color.FromArgb(64, 64, 64);
			columnItem69.ColumnAlignment = StringAlignment.Center;
			columnItem69.FontColor = Color.LightGray;
			columnItem69.MyStyle = FontStyle.Regular;
			columnItem69.Name = "offline";
			columnItem69.Text = "Offline";
			columnItem69.ValueFormat = FormatType.Text;
			columnItem69.Visible = false;
			columnItem69.Width = 10;
			this.intzaOrderList.Columns.Add(columnItem54);
			this.intzaOrderList.Columns.Add(columnItem55);
			this.intzaOrderList.Columns.Add(columnItem56);
			this.intzaOrderList.Columns.Add(columnItem57);
			this.intzaOrderList.Columns.Add(columnItem58);
			this.intzaOrderList.Columns.Add(columnItem59);
			this.intzaOrderList.Columns.Add(columnItem60);
			this.intzaOrderList.Columns.Add(columnItem61);
			this.intzaOrderList.Columns.Add(columnItem62);
			this.intzaOrderList.Columns.Add(columnItem63);
			this.intzaOrderList.Columns.Add(columnItem64);
			this.intzaOrderList.Columns.Add(columnItem65);
			this.intzaOrderList.Columns.Add(columnItem66);
			this.intzaOrderList.Columns.Add(columnItem67);
			this.intzaOrderList.Columns.Add(columnItem68);
			this.intzaOrderList.Columns.Add(columnItem69);
			this.intzaOrderList.CurrentScroll = 0;
			this.intzaOrderList.FocusItemIndex = -1;
			this.intzaOrderList.ForeColor = Color.Black;
			this.intzaOrderList.GridColor = Color.FromArgb(45, 45, 45);
			this.intzaOrderList.HeaderPctHeight = 80f;
			this.intzaOrderList.IsAutoRepaint = true;
			this.intzaOrderList.IsDrawFullRow = false;
			this.intzaOrderList.IsDrawGrid = true;
			this.intzaOrderList.IsDrawHeader = true;
			this.intzaOrderList.IsScrollable = false;
			this.intzaOrderList.Location = new Point(3, 30);
			this.intzaOrderList.MainColumn = "";
			this.intzaOrderList.Name = "intzaOrderList";
			this.intzaOrderList.Rows = 0;
			this.intzaOrderList.RowSelectColor = Color.FromArgb(0, 0, 128);
			this.intzaOrderList.RowSelectType = 3;
			this.intzaOrderList.RowsVisible = 0;
			this.intzaOrderList.ScrollChennelColor = Color.Gray;
			this.intzaOrderList.Size = new Size(815, 55);
			this.intzaOrderList.SortColumnName = "";
			this.intzaOrderList.SortType = SortType.Desc;
			this.intzaOrderList.TabIndex = 64;
			this.intzaOrderList.MouseClick += new MouseEventHandler(this.intzaOrderList_MouseClick);
			this.intzaOrderList.TableMouseClick += new SortGrid.TableMouseClickEventHandler(this.intzaOrderList_TableMouseClick);
			this.intzaOrderList.TableMouseDoubleClick += new SortGrid.TableMouseDoubleClickEventHandler(this.intzaOrderList_TableMouseDoubleClick);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.FromArgb(64, 64, 64);
			base.Controls.Add(this.panelStopOrderFLS);
			base.Controls.Add(this.intzaStopOrder);
			base.Controls.Add(this.intzaReOrderList);
			base.Controls.Add(this.lbLoading);
			base.Controls.Add(this.intzaOrderListTFEX);
			base.Controls.Add(this.intzaOrderList);
			base.Controls.Add(this.tStripMenu);
			base.Margin = new Padding(0);
			base.Name = "ucViewOrder";
			base.Size = new Size(833, 447);
			base.Load += new EventHandler(this.ucViewOrder_Load);
			base.VisibleChanged += new EventHandler(this.ucViewOrder_VisibleChanged);
			base.KeyDown += new KeyEventHandler(this.ucViewOrder_KeyDown);
			this.tStripMenu.ResumeLayout(false);
			this.tStripMenu.PerformLayout();
			this.contextMenuStrip1.ResumeLayout(false);
			this.panelStopOrderFLS.ResumeLayout(false);
			this.panelStopOrderFLS.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void DisposeMe()
		{
			try
			{
				if (this._viewOrderInfo != null)
				{
					this._viewOrderInfo.FormClosed -= new FormClosedEventHandler(this.viewOrderInfo_FormClosed);
					this._viewOrderInfo.Close();
					this._viewOrderInfo = null;
				}
				if (this._viewOrderInfoTfex != null)
				{
					this._viewOrderInfoTfex.FormClosed += new FormClosedEventHandler(this.viewOrderInfoTfex_FormClosed);
					this._viewOrderInfoTfex.Close();
					this._viewOrderInfoTfex = null;
				}
				this.bgwViewOrder = null;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowSplash(bool visible, string message, bool isAutoClose)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new ucViewOrder.ShowSplashCallBack(this.ShowSplash), new object[]
				{
					visible,
					message,
					isAutoClose
				});
			}
			else
			{
				try
				{
					if (ApplicationInfo.SuuportSplash == "Y")
					{
						if (this.tmCloseSplash == null)
						{
							this.tmCloseSplash = new System.Windows.Forms.Timer();
							this.tmCloseSplash.Interval = 1000;
							this.tmCloseSplash.Tick += new EventHandler(this.tmCloseSplash_Tick);
						}
						if (visible)
						{
							this.lbLoading.Text = message;
							this.lbLoading.Left = (base.Width - this.lbLoading.Width) / 2;
							this.lbLoading.Top = (base.Height - this.lbLoading.Height) / 2;
							this.lbLoading.Visible = true;
							this.lbLoading.BringToFront();
							this.tmCloseSplash.Enabled = false;
							if (isAutoClose)
							{
								this.tmCloseSplash.Enabled = true;
							}
						}
						else
						{
							this.lbLoading.Visible = false;
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("ShowSplash", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tmCloseSplash_Tick(object sender, EventArgs e)
		{
			this.tmCloseSplash.Enabled = false;
			this.ShowSplash(false, "", false);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ucViewOrder()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
			base.UpdateStyles();
			if (!base.DesignMode)
			{
				this.tscbSide.Items.Clear();
				this.tscbSide.Items.Add("ALL");
				this.tscbSide.Items.Add("B");
				this.tscbSide.Items.Add("S");
				if (ApplicationInfo.SuuportSBL == "Y" && ApplicationInfo.AccInfo.CurrentAccountType == "B")
				{
					this.tscbSide.Items.Add("C");
					this.tscbSide.Items.Add("H");
				}
				this.tscbStatus.Items.Clear();
				this.tscbStatus.Items.Add("ALL");
				this.tscbStatus.Items.Add("O");
				this.tscbStatus.Items.Add("PO");
				this.tscbStatus.Items.Add("M");
				this.tscbStatus.Items.Add("C");
				this.tscbStatus.Items.Add("R");
				this.tscbStatus.Items.Add("X");
				if (ApplicationInfo.SupportFreewill)
				{
					this.tscbStatus.Items.Add("D");
				}
				this.tscbSide.Text = "ALL";
				this.tscbStatus.Text = "ALL";
			}
			this.bgwViewOrder = new BackgroundWorker();
			this.bgwViewOrder.WorkerReportsProgress = true;
			this.bgwViewOrder.DoWork += new DoWorkEventHandler(this.bgwViewOrder_DoWork);
			this.bgwViewOrder.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwViewOrder_RunWorkerCompleted);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwViewOrder_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				string data = string.Empty;
				if (this._viewType == 1)
				{
					data = ApplicationInfo.WebOrderService.ViewOrderTransaction(this._selAccount, ApplicationInfo.UserLoginMode, 0, this._selAccount, this.selStock, this.selSide, this.selPrice, this.selStatus, 0L, Settings.Default.ViewOrderRows, 0);
				}
				else
				{
					if (this._viewType == 4)
					{
						data = ApplicationInfo.WebOrderService.ViewOrderTransaction(this._selAccount, ApplicationInfo.UserLoginMode, 0, this._selAccount, "", "", "", "O", 0L, Settings.Default.ViewOrderRows, 0);
					}
				}
				if (this.tdsOrder == null)
				{
					this.tdsOrder = new DataSet();
				}
				else
				{
					this.tdsOrder.Clear();
				}
				MyDataHelper.StringToDataSet(data, this.tdsOrder);
			}
			catch (Exception ex)
			{
				this.ShowError("bgwViewOrder_DoWork", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwViewOrder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				if (e.Error == null)
				{
					if (this._viewType == 1)
					{
						this.UpdateToControl();
					}
					else
					{
						if (this._viewType == 4)
						{
							this.UpdateToControlReOrder();
						}
					}
					this.tdsOrder.Clear();
				}
				else
				{
					this.ShowError("bgwViewOrder_RunWorkerCompleted", new Exception(e.Error.Message));
				}
			}
			catch (Exception ex)
			{
				this.ShowError("bgwViewOrder_RunWorkerCompleted", ex);
			}
			this.IsLoadingData = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetViewHeader()
		{
			try
			{
				if (!this._isSetHeaderDone)
				{
					this._isSetHeaderDone = true;
					if (this.showOnMainForm)
					{
						this.tsbtnCancelOrder.Text = "Cancel";
						this.intzaOrderList.GetColumn("order_number").Text = "Order#";
						this.intzaOrderList.GetColumn("status").Text = "ST";
						this.intzaOrderList.GetColumn("quote").Text = "QT";
						this.intzaOrderList.GetColumn("ttf").Text = "T";
						this.intzaOrderList.GetColumn("order_number").Width = 11;
						this.intzaOrderList.GetColumn("side").Width = 4;
						this.intzaOrderList.GetColumn("stock").Width = 11;
						this.intzaOrderList.GetColumn("volume").Width = 12;
						this.intzaOrderList.GetColumn("matched").Width = 12;
						this.intzaOrderList.GetColumn("published").Width = 12;
						this.intzaOrderList.GetColumn("status").Width = 6;
						this.intzaOrderList.GetColumn("time").Width = 10;
						this.intzaOrderList.GetColumn("ttf").Width = 3;
						this.intzaOrderList.GetColumn("quote").Width = 5;
						this.intzaOrderList.GetColumn("checkbox").Width = 3;
						this.intzaOrderList.GetColumn("price").Width = 8;
					}
					else
					{
						if (ApplicationInfo.SupportFreewill)
						{
							this.tsbtnCancelOrder.Text = "Cancel";
							this.intzaOrderList.GetColumn("order_number").Text = "Order#";
							this.intzaOrderList.GetColumn("quote").Text = "QT";
							this.intzaOrderList.GetColumn("order_number").Width = 10;
							this.intzaOrderList.GetColumn("side").Width = 4;
							this.intzaOrderList.GetColumn("stock").Width = 12;
							this.intzaOrderList.GetColumn("volume").Width = 11;
							this.intzaOrderList.GetColumn("matched").Width = 11;
							this.intzaOrderList.GetColumn("published").Width = 11;
							this.intzaOrderList.GetColumn("status").Width = 9;
							this.intzaOrderList.GetColumn("time").Width = 9;
							this.intzaOrderList.GetColumn("ttf").Width = 4;
							this.intzaOrderList.GetColumn("quote").Width = 5;
							this.intzaOrderList.GetColumn("checkbox").Width = 3;
							this.intzaOrderList.GetColumn("price").Width = 8;
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SetViewHeader", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ucViewOrder_Load(object sender, EventArgs e)
		{
			try
			{
				if (!base.DesignMode)
				{
					this.tstbStock.AutoCompleteMode = AutoCompleteMode.Suggest;
					this.tstbStock.AutoCompleteCustomSource = ApplicationInfo.StockAutoComp;
					this.tStripMenu.Visible = this.isShowToolsBar;
					this.intzaOrderList.IsScrollable = true;
					this.SetHeader();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("ucViewOrder_Load", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetResize(bool isChanged)
		{
			try
			{
				if (ApplicationInfo.IsAreadyLogin)
				{
					this.intzaOrderList.Font = Settings.Default.Default_Font;
					this.intzaStopOrder.Font = Settings.Default.Default_Font;
					this.intzaOrderListTFEX.Font = Settings.Default.Default_Font;
					this.intzaReOrderList.Font = Settings.Default.Default_Font;
					Font font = new Font(Settings.Default.Default_Font.Name, Settings.Default.Default_Font.Size - 1f, FontStyle.Regular);
					if (this.tStripMenu.Font != font)
					{
						this.tStripMenu.Font = font;
						foreach (ToolStripItem toolStripItem in this.tStripMenu.Items)
						{
							toolStripItem.Font = font;
						}
						this.tStripMenu.Invalidate();
					}
					if (this.tStripMenu.Visible != this.isShowToolsBar)
					{
						this.tStripMenu.Visible = this.isShowToolsBar;
					}
					Rectangle bounds;
					if (this.isShowToolsBar)
					{
						bounds = new Rectangle(0, this.tStripMenu.Height + 1, base.ClientSize.Width, base.ClientSize.Height - this.tStripMenu.Height - 1);
					}
					else
					{
						bounds = new Rectangle(0, 0, base.ClientSize.Width, base.ClientSize.Height);
					}
					if (this._viewType == 1)
					{
						this.SetViewHeader();
						this.intzaOrderList.Bounds = bounds;
					}
					else
					{
						if (this._viewType == 2)
						{
							this.intzaStopOrder.Bounds = bounds;
						}
						else
						{
							if (this._viewType == 4)
							{
								this.intzaReOrderList.Bounds = bounds;
							}
							else
							{
								this.intzaOrderListTFEX.Bounds = bounds;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SetResize", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void FormKeyUp(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != Keys.Space)
			{
				if (keyCode == Keys.R)
				{
					if (e.Control)
					{
						this.SelectAllOrderForCancel(!this._isSelectAll);
					}
				}
			}
			else
			{
				this.SetFocus();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReceiveMessage(IBroadcastMessage message, StockList.StockInformation realtimeStockInfo)
		{
			if (!this.isLoadingData)
			{
				try
				{
					string messageType = message.MessageType;
					if (messageType != null)
					{
						if (!(messageType == "0I"))
						{
							if (messageType == "0B")
							{
								if (ApplicationInfo.SupportFreewill)
								{
									BroadCastOrderMessageClient broadCastOrderMessageClient = (BroadCastOrderMessageClient)message;
									if (ApplicationInfo.CanReceiveMessage(broadCastOrderMessageClient.EntryID))
									{
										ApplicationInfo.RemoveOrderNoFromAutoRefreshList("", broadCastOrderMessageClient.Reserve2);
									}
								}
							}
						}
						else
						{
							this.ReceiveOrderInfo(message);
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
			if (!this.isLoadingData)
			{
				try
				{
					if (message.MessageType == "#T9I")
					{
						this.ReceiveOrderInfoTFEX(message);
					}
				}
				catch (Exception ex)
				{
					this.ShowError("ReceiveMessage", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ReceiveOrderInfoTFEX(IBroadcastMessage message)
		{
			if (this.intzaOrderListTFEX.InvokeRequired)
			{
				this.intzaOrderListTFEX.Invoke(new ucViewOrder.ReceiveOrderInfoTFEXCallBack(this.ReceiveOrderInfoTFEX), new object[]
				{
					message
				});
			}
			else
			{
				try
				{
					OrderTFEXInfoClient orderTFEXInfoClient = (OrderTFEXInfoClient)message;
					if (ApplicationInfo.CanReceiveMessage(orderTFEXInfoClient.Account))
					{
						ucViewOrder.RecordData recordData = default(ucViewOrder.RecordData);
						recordData.OrderNumber = orderTFEXInfoClient.OrderNumber;
						recordData.Position = orderTFEXInfoClient.Position;
						recordData.Side = orderTFEXInfoClient.Side;
						recordData.Series = orderTFEXInfoClient.Series;
						recordData.Volume = orderTFEXInfoClient.Volume;
						recordData.Price = orderTFEXInfoClient.Price;
						recordData.Matched = orderTFEXInfoClient.MatchedVolume;
						recordData.PubVol = orderTFEXInfoClient.PublicVolume;
						recordData.OrderStatus = orderTFEXInfoClient.Status;
						recordData.Quote = orderTFEXInfoClient.Quote;
						recordData.OrderDate = orderTFEXInfoClient.SendDate;
						recordData.OrdType = orderTFEXInfoClient.OrderType;
						recordData.OrderTimes = orderTFEXInfoClient.OrderTime;
						bool flag = false;
						int num = this.intzaOrderListTFEX.FindIndex("key", recordData.OrderNumber + "_" + recordData.OrderDate);
						if (num > -1)
						{
							flag = true;
						}
						bool flag2 = false;
						if (flag)
						{
							flag2 = true;
						}
						else
						{
							if (!flag && (orderTFEXInfoClient.OriginalMessageType == "1I" || orderTFEXInfoClient.OriginalMessageType == "2G"))
							{
								flag2 = true;
							}
						}
						if (flag2)
						{
							this.UpdateToGrid_TFEX(num, recordData);
							this.intzaOrderListTFEX.EndUpdate();
						}
						if (!this.Focused)
						{
							if (num == -1 && !ApplicationInfo.IsEquityAccount)
							{
								if (this.intzaOrderListTFEX.Rows > 0)
								{
									this.intzaOrderListTFEX.SetFocusItem(0);
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("ReceiveOrderInfo_TFEX", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ReceiveOrderInfo(IBroadcastMessage message)
		{
			if (this.intzaOrderList.InvokeRequired)
			{
				this.intzaOrderList.Invoke(new ucViewOrder.ReceiveOrderInfoCallBack(this.ReceiveOrderInfo), new object[]
				{
					message
				});
			}
			else
			{
				try
				{
					OrderInfoClient orderInfoClient = (OrderInfoClient)message;
					if (orderInfoClient.Account.Trim() == ApplicationInfo.AccInfo.CurrentAccount)
					{
						ucViewOrder.RecordData recordData = default(ucViewOrder.RecordData);
						recordData.OrderNumber = orderInfoClient.OrderNumber;
						recordData.Side = orderInfoClient.Side;
						recordData.Stock = orderInfoClient.Stock;
						recordData.Volume = orderInfoClient.Volume;
						recordData.Price = Utilities.PriceFormat(orderInfoClient.PriceToSET);
						if (!ApplicationInfo.SupportFreewill)
						{
							if (orderInfoClient.MatchedValue != orderInfoClient.PriceForCal * orderInfoClient.MatchedVolume)
							{
								recordData.Price = "*" + recordData.Price;
							}
						}
						recordData.Matched = orderInfoClient.MatchedVolume;
						recordData.PubVol = orderInfoClient.PublicVolume;
						recordData.OrderTimes = Utilities.GetTime(orderInfoClient.OrderTime.Trim());
						int.TryParse(orderInfoClient.TrusteeID, out recordData.TrusteeID);
						recordData.Quote = orderInfoClient.Quote;
						recordData.OrderStatus = orderInfoClient.Status;
						recordData.ApprUserNo = orderInfoClient.ApproverId;
						recordData.OrderDate = orderInfoClient.OrderDate.Trim();
						recordData.OrderTime = orderInfoClient.OrderTime.Trim();
						recordData.IsAfterCloseOrder = (orderInfoClient.Reserve2 == "OFFLINE");
						bool flag = false;
						int num;
						if (ApplicationInfo.SupportFreewill)
						{
							num = this.intzaOrderList.FindIndex("order_number", recordData.OrderNumber.ToString());
						}
						else
						{
							num = this.intzaOrderList.FindIndex("key", recordData.OrderNumber + "_" + recordData.OrderDate.Trim());
						}
						if (num > -1)
						{
							flag = true;
						}
						else
						{
							if (orderInfoClient.OriginalMessageType == "1I")
							{
								flag = true;
							}
						}
						if (flag)
						{
							this.intzaOrderList.BeginUpdate();
							this.UpdateToGrid(num, recordData);
							if (num == -1)
							{
								if (this.intzaOrderList.Rows > 0)
								{
									this.intzaOrderList.SetFocusItem(0);
								}
							}
							this.intzaOrderList.Redraw();
						}
						if (ApplicationInfo.SupportFreewill && this.frmConfirm != null)
						{
							foreach (ucViewOrder.CancelItem current in this._listOfOrderCancel)
							{
								if (current.OrderNo == orderInfoClient.OrderNumber && !current.Complete)
								{
									current.Complete = true;
									this.frmConfirm.CloseMe();
									break;
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("ReceiveOrderInfo", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateToGrid(int rowIndex, ucViewOrder.RecordData recordData)
		{
			try
			{
				STIControl.SortTableGrid.RecordItem recordItem;
				if (rowIndex == -1)
				{
					recordItem = this.intzaOrderList.AddRecord(1, false);
				}
				else
				{
					recordItem = this.intzaOrderList.Records(rowIndex);
				}
				recordItem.BackColor = Color.Black;
				recordItem.Fields("order_number").Text = recordData.OrderNumber;
				recordItem.Fields("side").Text = recordData.Side;
				recordItem.Fields("stock").Text = recordData.Stock;
				recordItem.Fields("volume").Text = recordData.Volume;
				recordItem.Fields("price").Text = recordData.Price;
				recordItem.Fields("matched").Text = recordData.Matched;
				recordItem.Fields("time").Text = recordData.OrderTimes;
				recordItem.Fields("info").Text = "2";
				recordItem.Fields("published").Text = Utilities.GetPublishedVolumeDisplay(recordData.Volume, recordData.PubVol, recordData.Matched, recordData.OrderStatus);
				string text = string.Empty;
				text = recordData.OrderStatus.Trim();
				if (text != "A" && recordData.ApprUserNo.Trim() != string.Empty)
				{
					text = recordData.OrderStatus + "A";
				}
				if (this.showOnMainForm || ApplicationInfo.SupportFreewill)
				{
					recordItem.Fields("status").Text = text;
				}
				else
				{
					recordItem.Fields("status").Text = Utilities.GetDisplayOrderStatus(text) + " (" + text + ")";
				}
				if (recordData.TrusteeID == 0)
				{
					recordItem.Fields("ttf").Text = string.Empty;
				}
				else
				{
					recordItem.Fields("ttf").Text = recordData.TrusteeID;
				}
				recordItem.Fields("quote").Text = recordData.Quote;
				recordItem.Fields("send_date").Text = recordData.OrderDate;
				recordItem.Fields("key").Text = recordData.OrderNumber + "_" + recordData.OrderDate;
				recordItem.Fields("order_number").FontColor = Color.White;
				if (recordData.Side == "B")
				{
					recordItem.Fields("side").FontColor = Color.Lime;
				}
				else
				{
					if (recordData.Side == "S")
					{
						recordItem.Fields("side").FontColor = Color.Red;
					}
					else
					{
						if (recordData.Side == "C")
						{
							recordItem.Fields("side").FontColor = Color.Cyan;
						}
						else
						{
							if (recordData.Side == "H")
							{
								recordItem.Fields("side").FontColor = Color.Pink;
							}
							else
							{
								recordItem.Fields("side").FontColor = Color.Yellow;
							}
						}
					}
				}
				recordItem.Fields("stock").FontColor = Color.White;
				recordItem.Fields("volume").FontColor = Color.White;
				recordItem.Fields("price").FontColor = Color.White;
				recordItem.Fields("matched").FontColor = Color.Cyan;
				recordItem.Fields("published").FontColor = Color.White;
				recordItem.Fields("time").FontColor = Color.White;
				recordItem.Fields("status").FontColor = Color.Cyan;
				recordItem.Fields("ttf").FontColor = Color.Cyan;
				recordItem.Fields("quote").FontColor = Color.Yellow;
				if (this.CanShowCheckBox(recordData.OrderStatus))
				{
					recordItem.Fields("checkbox").Text = "0";
				}
				else
				{
					recordItem.Fields("checkbox").Text = "";
				}
				if (ApplicationInfo.SupportFreewill)
				{
					recordItem.Fields("offline").Text = (recordData.IsAfterCloseOrder ? "Y" : "N");
				}
				recordItem.Changed = true;
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateToGrid", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateToGridReOrder(int rowIndex, ucViewOrder.RecordData recordData)
		{
			try
			{
				STIControl.SortTableGrid.RecordItem recordItem;
				if (rowIndex == -1)
				{
					recordItem = this.intzaReOrderList.AddRecord(1, false);
				}
				else
				{
					recordItem = this.intzaReOrderList.Records(rowIndex);
				}
				recordItem.BackColor = Color.Black;
				recordItem.Fields("order_number").Text = recordData.OrderNumber;
				recordItem.Fields("side").Text = recordData.Side;
				recordItem.Fields("stock").Text = recordData.Stock;
				recordItem.Fields("volume").Text = recordData.Volume;
				recordItem.Fields("price").Text = recordData.Price;
				recordItem.Fields("matched").Text = recordData.Matched;
				recordItem.Fields("time").Text = recordData.OrderTimes;
				recordItem.Fields("info").Text = "2";
				recordItem.Fields("published").Text = Utilities.GetPublishedVolumeDisplay(recordData.Volume, recordData.PubVol, recordData.Matched, recordData.OrderStatus);
				string text = string.Empty;
				text = recordData.OrderStatus.Trim();
				if (text != "A" && recordData.ApprUserNo.Trim() != string.Empty)
				{
					text = recordData.OrderStatus + "A";
				}
				recordItem.Fields("status").Text = text;
				if (recordData.TrusteeID == 0)
				{
					recordItem.Fields("ttf").Text = string.Empty;
				}
				else
				{
					recordItem.Fields("ttf").Text = recordData.TrusteeID;
				}
				if (recordData.Quote != "Y")
				{
					recordItem.Fields("quote").Text = string.Empty;
				}
				else
				{
					recordItem.Fields("quote").Text = recordData.Quote;
				}
				recordItem.Fields("send_date").Text = recordData.OrderDate;
				recordItem.Fields("key").Text = recordData.OrderNumber + "_" + recordData.OrderDate;
				recordItem.Fields("order_number").FontColor = Color.White;
				if (recordData.Side == "B")
				{
					recordItem.Fields("side").FontColor = Color.Lime;
				}
				else
				{
					if (recordData.Side == "S")
					{
						recordItem.Fields("side").FontColor = Color.Red;
					}
					else
					{
						if (recordData.Side == "C")
						{
							recordItem.Fields("side").FontColor = Color.Cyan;
						}
						else
						{
							if (recordData.Side == "H")
							{
								recordItem.Fields("side").FontColor = Color.Pink;
							}
							else
							{
								recordItem.Fields("side").FontColor = Color.Yellow;
							}
						}
					}
				}
				recordItem.Fields("stock").FontColor = Color.White;
				recordItem.Fields("volume").FontColor = Color.White;
				recordItem.Fields("price").FontColor = Color.White;
				recordItem.Fields("matched").FontColor = Color.Cyan;
				recordItem.Fields("published").FontColor = Color.White;
				recordItem.Fields("time").FontColor = Color.White;
				recordItem.Fields("status").FontColor = Color.Cyan;
				recordItem.Fields("ttf").FontColor = Color.Cyan;
				recordItem.Fields("quote").FontColor = Color.Yellow;
				if (this.CanShowCheckBox(recordData.OrderStatus))
				{
					recordItem.Fields("checkbox").Text = "0";
				}
				else
				{
					recordItem.Fields("checkbox").Text = "";
				}
				recordItem.Changed = true;
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateToGridReOrder", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReloadData()
		{
			if (this._selAccount != ApplicationInfo.AccInfo.CurrentAccount && ApplicationInfo.AccInfo.CurrentAccount != string.Empty)
			{
				if (ApplicationInfo.IsEquityAccount)
				{
					this.SetView(1);
				}
				else
				{
					this.SetView(3);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetBoxFocus(bool focusFlag)
		{
			try
			{
				this.focusFlag = focusFlag;
				this.tscbStatus.Focus();
			}
			catch (Exception ex)
			{
				this.ShowError("SetBoxFocus", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetGridFocus(bool forceIndex)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new ucViewOrder.SetGridFocusCallBack(this.SetGridFocus), new object[]
				{
					forceIndex
				});
			}
			else
			{
				try
				{
					if (forceIndex)
					{
						if (this.intzaOrderList.Rows > 0)
						{
							this.intzaOrderList.FocusItemIndex = 0;
						}
					}
					else
					{
						if (this.intzaOrderList.FocusItemIndex < 0)
						{
							this.intzaOrderList.FocusItemIndex = 0;
						}
					}
					this.intzaOrderList.Focus();
					this.intzaOrderList.Redraw();
				}
				catch (Exception ex)
				{
					this.ShowError("SetStockBoxFocus", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetFocus()
		{
			if (this.tStripMenu.Visible)
			{
				if (this.tscbStatus.Focused && this.intzaOrderList.Rows > 0)
				{
					this.SetGridFocus(false);
				}
				else
				{
					this.SetBoxFocus(false);
				}
			}
			else
			{
				this.SetGridFocus(false);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void RequestWebData()
		{
			string empty = string.Empty;
			try
			{
				if (!this.IsLoadingData)
				{
					this._selAccount = ApplicationInfo.AccInfo.CurrentAccount;
					if (!string.IsNullOrEmpty(this._selAccount))
					{
						this.IsLoadingData = true;
						this.selStatus = string.Empty;
						if (this.tscbStatus.Text.Trim() != string.Empty && this.tscbStatus.Text.Trim() != "ALL")
						{
							this.selStatus = this.tscbStatus.Text.Trim();
						}
						this.selStock = this.tstbStock.Text.Trim();
						this.selPrice = this.tstbPrice.Text.Trim();
						if (this.tscbSide.Text == "B")
						{
							this.selSide = "B";
						}
						else
						{
							if (this.tscbSide.Text == "S")
							{
								this.selSide = "S";
							}
							else
							{
								if (this.tscbSide.Text == "H")
								{
									this.selSide = "H";
								}
								else
								{
									if (this.tscbSide.Text == "C")
									{
										this.selSide = "C";
									}
									else
									{
										this.selSide = string.Empty;
									}
								}
							}
						}
						if (this._viewType == 1)
						{
							if (!this.bgwViewOrder.IsBusy)
							{
								this.bgwViewOrder.RunWorkerAsync();
							}
						}
						else
						{
							if (this._viewType == 2)
							{
								ApplicationInfo.WebAlertService.ViewStopOrderCompleted -= new ViewStopOrderCompletedEventHandler(this.MyWebService_ViewStopOrderCompleted);
								ApplicationInfo.WebAlertService.ViewStopOrderCompleted += new ViewStopOrderCompletedEventHandler(this.MyWebService_ViewStopOrderCompleted);
								ApplicationInfo.WebAlertService.ViewStopOrderAsync(ApplicationInfo.UserLoginID, this._selAccount, 0);
							}
							else
							{
								if (this._viewType == 3)
								{
									ApplicationInfo.WebServiceTFEX.ViewOrderTransactionCompleted -= new i2TradePlus.ITSNetBusinessWSTFEX.ViewOrderTransactionCompletedEventHandler(this.MyWebTfex_ViewOrderTransactionCompleted);
									ApplicationInfo.WebServiceTFEX.ViewOrderTransactionCompleted += new i2TradePlus.ITSNetBusinessWSTFEX.ViewOrderTransactionCompletedEventHandler(this.MyWebTfex_ViewOrderTransactionCompleted);
									ApplicationInfo.WebServiceTFEX.ViewOrderTransactionAsync(ApplicationInfo.AccInfo.CurrentAccount, ApplicationInfo.UserLoginMode, 0, this._selAccount, this.selStock, this.selSide, this.selPrice, this.selStatus, 0L, Settings.Default.ViewOrderRows, 0);
								}
								else
								{
									if (this._viewType == 4)
									{
										if (!this.bgwViewOrder.IsBusy)
										{
											this.bgwViewOrder.RunWorkerAsync();
										}
									}
								}
							}
						}
					}
					else
					{
						this.IsLoadingData = false;
						this.ShowSplash(true, "Current Account is null", true);
						this.ShowError("RequestWebData", new Exception("Current Account is null"));
					}
				}
			}
			catch (Exception ex)
			{
				this.IsLoadingData = false;
				this.ShowError("RequestWebData", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void MyWebTfex_ViewOrderTransactionCompleted(object sender, i2TradePlus.ITSNetBusinessWSTFEX.ViewOrderTransactionCompletedEventArgs e)
		{
			try
			{
				ApplicationInfo.WebServiceTFEX.ViewOrderTransactionCompleted -= new i2TradePlus.ITSNetBusinessWSTFEX.ViewOrderTransactionCompletedEventHandler(this.MyWebTfex_ViewOrderTransactionCompleted);
				if (e.Error == null)
				{
					if (this.tdsOrder == null)
					{
						this.tdsOrder = new DataSet();
					}
					else
					{
						this.tdsOrder.Clear();
					}
					if (!string.IsNullOrEmpty(e.Result))
					{
						MyDataHelper.StringToDataSet(e.Result, this.tdsOrder);
						this.UpdateToControlTFEX();
						this.tdsOrder.Clear();
					}
				}
				else
				{
					this.ShowError("WebService_ViewOrderTFEXTransactionCompleted", new Exception(e.Error.Message));
				}
			}
			catch (Exception ex)
			{
				this.ShowError("MyWebTfex_ViewOrderTransactionCompleted", ex);
			}
			this.IsLoadingData = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateToControlTFEX()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new ucViewOrder.UpdateDataToControlTFEXCallBack(this.UpdateToControlTFEX));
			}
			else
			{
				try
				{
					if (this.tdsOrder.Tables.Contains("ORDERS"))
					{
						this.intzaOrderListTFEX.BeginUpdate();
						this.intzaOrderListTFEX.Rows = this.tdsOrder.Tables["ORDERS"].Rows.Count;
						long num = 0L;
						long num2 = 0L;
						long num3 = 0L;
						long orderNumber = 0L;
						for (int i = 0; i < this.tdsOrder.Tables["ORDERS"].Rows.Count; i++)
						{
							DataRow dataRow = this.tdsOrder.Tables["ORDERS"].Rows[i];
							ucViewOrder.RecordData recordData = default(ucViewOrder.RecordData);
							long.TryParse(dataRow["order_number"].ToString(), out orderNumber);
							recordData.OrderNumber = orderNumber;
							recordData.Position = dataRow["position"].ToString();
							recordData.Side = dataRow["side"].ToString();
							recordData.Series = dataRow["series"].ToString().Trim();
							long.TryParse(dataRow["volume"].ToString(), out num);
							recordData.Volume = num;
							recordData.Price = dataRow["price"].ToString();
							long.TryParse(dataRow["matched_volume"].ToString(), out num2);
							recordData.Matched = num2;
							long.TryParse(dataRow["pub_vol"].ToString(), out num3);
							if (num - num2 > 0L && num3 != 0L)
							{
								recordData.PubVol = (num - num2) % num3;
								if (recordData.PubVol == 0L)
								{
									recordData.PubVol = num3;
								}
							}
							else
							{
								recordData.PubVol = 0L;
							}
							recordData.OrderStatus = dataRow["status"].ToString();
							recordData.OrderTimes = dataRow["order_time"].ToString().Trim();
							recordData.Quote = dataRow["quote"].ToString().Trim();
							recordData.OrdType = dataRow["sOrdType"].ToString();
							recordData.OrderDate = dataRow["sSendDate"].ToString();
							recordData.OrderTime = string.Empty;
							this.UpdateToGrid_TFEX(i, recordData);
						}
						if (this.intzaOrderListTFEX.Rows > 0)
						{
							this.intzaOrderListTFEX.FocusItemIndex = 0;
						}
						this.intzaOrderListTFEX.Redraw();
					}
				}
				catch (Exception ex)
				{
					this.intzaOrderListTFEX.Redraw();
					this.ShowError("UpdateToControlTFEX", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateToGrid_TFEX(int rowIndex, ucViewOrder.RecordData recordData)
		{
			try
			{
				SeriesList.SeriesInformation seriesInformation = ApplicationInfo.SeriesInfo[recordData.Series];
				STIControl.SortTableGrid.RecordItem recordItem;
				if (rowIndex == -1)
				{
					recordItem = this.intzaOrderListTFEX.AddRecord(1, false);
				}
				else
				{
					recordItem = this.intzaOrderListTFEX.Records(rowIndex);
				}
				recordItem.BackColor = Color.Black;
				recordItem.Fields("order_number").Text = recordData.OrderNumber;
				if (recordData.Position == "1")
				{
					recordItem.Fields("position").Text = "Open";
				}
				else
				{
					if (recordData.Position == "2")
					{
						recordItem.Fields("position").Text = "Close";
					}
				}
				recordItem.Fields("side").Text = recordData.Side;
				recordItem.Fields("stock").Text = recordData.Series;
				recordItem.Fields("volume").Text = recordData.Volume;
				if (recordData.Price == "-1")
				{
					recordItem.Fields("price").Text = "MP";
				}
				else
				{
					if (seriesInformation != null)
					{
						recordItem.Fields("price").Text = Utilities.PriceFormat(recordData.Price, seriesInformation.NumOfDec, 0);
					}
					else
					{
						recordItem.Fields("price").Text = Utilities.PriceFormat(recordData.Price, 2, 0);
					}
				}
				recordItem.Fields("matched").Text = recordData.Matched;
				if (recordData.OrderTimes == null)
				{
					recordData.OrderTimes = string.Empty;
				}
				recordItem.Fields("time").Text = recordData.OrderTimes;
				string text = string.Empty;
				text = recordData.OrderStatus.Trim();
				if (text == "M" || text == "O")
				{
					if (recordData.Volume - recordData.Matched == 0L)
					{
						recordItem.Fields("published").Text = "0";
					}
					else
					{
						recordItem.Fields("published").Text = recordData.PubVol;
					}
				}
				else
				{
					if (text == "R")
					{
						recordItem.Fields("published").Text = string.Empty;
					}
					else
					{
						recordItem.Fields("published").Text = recordData.PubVol;
					}
				}
				recordItem.Fields("status").Text = text;
				if (recordData.Quote == null)
				{
					recordData.Quote = string.Empty;
				}
				if (recordData.Quote != "Y")
				{
					recordItem.Fields("quote").Text = string.Empty;
				}
				else
				{
					recordItem.Fields("quote").Text = recordData.Quote;
				}
				recordItem.Fields("send_date").Text = recordData.OrderDate;
				recordItem.Fields("key").Text = recordData.OrderNumber + "_" + recordData.OrderDate;
				recordItem.Fields("ordType").Text = recordData.OrdType;
				Color fontColor = Color.Yellow;
				if (recordData.Side == "1")
				{
					fontColor = Color.Cyan;
				}
				else
				{
					if (recordData.Side == "2")
					{
						fontColor = Color.Magenta;
					}
				}
				recordItem.Fields("order_number").FontColor = Color.White;
				recordItem.Fields("position").FontColor = ((recordData.Position == "1") ? Color.Lime : Color.Red);
				recordItem.Fields("side").FontColor = fontColor;
				recordItem.Fields("stock").FontColor = Color.White;
				recordItem.Fields("volume").FontColor = Color.White;
				recordItem.Fields("price").FontColor = Color.White;
				recordItem.Fields("matched").FontColor = Color.Cyan;
				recordItem.Fields("published").FontColor = Color.White;
				recordItem.Fields("status").FontColor = Color.Cyan;
				recordItem.Fields("time").FontColor = Color.White;
				recordItem.Fields("quote").FontColor = Color.Yellow;
				recordItem.Fields("valid").FontColor = fontColor;
				if (this.CanShowCheckBox(recordData.OrderStatus))
				{
					recordItem.Fields("checkbox").Text = 0;
				}
				else
				{
					recordItem.Fields("checkbox").Text = -1;
				}
				recordItem.Changed = true;
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateToGrid_TFXE", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void MyWebService_ViewStopOrderCompleted(object sender, ViewStopOrderCompletedEventArgs e)
		{
			try
			{
				ApplicationInfo.WebAlertService.ViewStopOrderCompleted -= new ViewStopOrderCompletedEventHandler(this.MyWebService_ViewStopOrderCompleted);
				if (e.Error == null)
				{
					using (DataSet dataSet = new DataSet())
					{
						MyDataHelper.StringToDataSet(e.Result.ToString(), dataSet);
						if (dataSet.Tables.Contains("ORDERS"))
						{
							this.intzaStopOrder.SortColumnName = string.Empty;
							this.intzaStopOrder.BeginUpdate();
							this.intzaStopOrder.Rows = dataSet.Tables["ORDERS"].Rows.Count;
							int num = 0;
							foreach (DataRow dr in dataSet.Tables["ORDERS"].Rows)
							{
								this.UpdateStopOrderToGrid(dr, num, false);
								num++;
							}
							this.intzaStopOrder.Redraw();
							if (this.intzaStopOrder.Rows > 0)
							{
								this.intzaStopOrder.Focus();
								this.intzaStopOrder.SetFocusItem(0);
							}
						}
						dataSet.Clear();
					}
				}
			}
			catch (Exception ex)
			{
				this.intzaStopOrder.Redraw();
				this.ShowError("ViewStopOrderCompleted", ex);
			}
			this.IsLoadingData = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void UpdateStopOrderToGrid(DataRow dr, int i, bool isRedraw)
		{
			try
			{
				if (this._viewType == 2)
				{
					long num = 0L;
					long num2 = 0L;
					long num3 = 0L;
					string text = string.Empty;
					string text2 = string.Empty;
					string text3 = string.Empty;
					long.TryParse(dr["ref_no"].ToString(), out num3);
					STIControl.ExpandTableGrid.RecordItem recordItem;
					if (i == -1)
					{
						recordItem = this.intzaStopOrder.Find("ref_no", num3.ToString());
						if (recordItem == null)
						{
							recordItem = this.intzaStopOrder.AddRecord(1, false);
						}
					}
					else
					{
						recordItem = this.intzaStopOrder.Records(i);
					}
					recordItem.Fields("ref_no").Text = num3;
					text = dr["ord_side"].ToString();
					recordItem.Fields("side").Text = text;
					StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[Convert.ToInt32(dr["stock"].ToString())];
					if (Utilities.PriceFormat(dr["ord_ttf"].ToString()) == string.Empty)
					{
						recordItem.Fields("stock").Text = stockInformation.Symbol;
					}
					else
					{
						recordItem.Fields("stock").Text = stockInformation.Symbol + "(" + dr["ord_ttf"].ToString() + ")";
					}
					long.TryParse(dr["ord_volume"].ToString(), out num);
					recordItem.Fields("volume").Text = num;
					long.TryParse(dr["ord_pubvol"].ToString(), out num2);
					recordItem.Fields("pubvol").Text = num2;
					recordItem.Fields("limit").Text = (Convert.ToBoolean(dr["limit"].ToString()) ? "Y" : "N");
					string text4 = dr["ord_condition"].ToString();
					if (text4 != null)
					{
						if (text4 == "I")
						{
							recordItem.Fields("price_cond").Text = "IOC";
							goto IL_2CB;
						}
						if (text4 == "F")
						{
							recordItem.Fields("price_cond").Text = "FOK";
							goto IL_2CB;
						}
					}
					recordItem.Fields("price_cond").Text = dr["ord_condition"].ToString();
					IL_2CB:
					text3 = dr["status"].ToString().Trim();
					text4 = text3;
					if (text4 != null)
					{
						if (!(text4 == "PO"))
						{
							if (!(text4 == "O"))
							{
								if (!(text4 == "F"))
								{
									if (!(text4 == "S"))
									{
										if (!(text4 == "M"))
										{
											if (text4 == "X")
											{
												text2 = "Cancel";
											}
										}
										else
										{
											text2 = "InComplete";
										}
									}
									else
									{
										text2 = "Sent";
									}
								}
								else
								{
									text2 = "Fail";
								}
							}
							else
							{
								text2 = "Pending";
							}
						}
						else
						{
							text2 = "Wait";
						}
					}
					recordItem.Fields("status").Text = text2;
					recordItem.Fields("price").Text = Utilities.PriceFormat(dr["ord_price"].ToString());
					recordItem.Fields("account").Text = Utilities.PriceFormat(dr["ord_account"].ToString());
					recordItem.Fields("sent_time").Text = Utilities.GetTime(dr["time"].ToString());
					recordItem.Fields("matched_time").Text = Utilities.GetTime(dr["mtime"].ToString());
					string text5 = "  ";
					int num4 = 0;
					int.TryParse(dr["field_type"].ToString(), out num4);
					if (num4 == 1 || num4 == 4 || num4 == 5 || num4 == 6)
					{
						text5 += "Last";
					}
					else
					{
						text5 += "Unknow";
					}
					int num5 = 0;
					int.TryParse(dr["operator_type"].ToString(), out num5);
					if (num5 == 1)
					{
						text5 += " >= ";
					}
					else
					{
						if (num5 == 2)
						{
							text5 += " <= ";
						}
						else
						{
							if (num5 == 3)
							{
								text5 += " > ";
							}
							else
							{
								if (num5 == 4)
								{
									text5 += " < ";
								}
							}
						}
					}
					if (num4 == 4 || num4 == 5 || num4 == 6)
					{
						if (num4 == 4)
						{
							text5 += "SMA ";
						}
						else
						{
							if (num4 == 5)
							{
								text5 += "Break High ";
							}
							else
							{
								if (num4 == 6)
								{
									text5 += "Break Low ";
								}
							}
						}
						text5 += Utilities.PriceFormat(dr["price"].ToString());
						decimal num6 = 0m;
						decimal.TryParse(dr["sma_currprice"].ToString(), out num6);
						if (num6 > 0m)
						{
							text5 = text5 + " (@" + Utilities.PriceFormat(num6) + " )";
						}
						else
						{
							text5 += " (...)";
						}
					}
					else
					{
						text5 += Utilities.PriceFormat(dr["price"].ToString());
					}
					recordItem.Fields("condition").Text = text5;
					if (num4 == 1)
					{
						recordItem.Fields("con_price").Text = dr["price"].ToString();
					}
					else
					{
						if (num4 == 4)
						{
							recordItem.Fields("con_price").Text = dr["sma_currprice"].ToString();
						}
					}
					if (num5 == 1)
					{
						recordItem.Fields("con_operator").Text = ">=";
					}
					else
					{
						if (num5 == 2)
						{
							recordItem.Fields("con_operator").Text = "<=";
						}
						else
						{
							if (num5 == 3)
							{
								recordItem.Fields("con_operator").Text = ">";
							}
							else
							{
								if (num5 == 4)
								{
									recordItem.Fields("con_operator").Text = "<";
								}
							}
						}
					}
					recordItem.Fields("ttf").Text = dr["ord_ttf"].ToString().Trim();
					long num7;
					long.TryParse(dr["order_number"].ToString(), out num7);
					recordItem.Fields("order_no").Text = ((num7 > 0L) ? num7.ToString() : "");
					recordItem.Fields("error").Text = dr["message"].ToString().Trim();
					recordItem.Fields("ref_no").FontColor = Color.Orange;
					if (text == "B")
					{
						recordItem.Fields("side").FontColor = Color.Lime;
					}
					else
					{
						if (text == "S")
						{
							recordItem.Fields("side").FontColor = Color.Red;
						}
						else
						{
							if (text == "C")
							{
								recordItem.Fields("side").FontColor = Color.Cyan;
							}
							else
							{
								if (text == "H")
								{
									recordItem.Fields("side").FontColor = Color.Pink;
								}
								else
								{
									recordItem.Fields("side").FontColor = Color.Yellow;
								}
							}
						}
					}
					recordItem.Fields("stock").FontColor = Color.LightGray;
					recordItem.Fields("volume").FontColor = Color.LightGray;
					recordItem.Fields("price").FontColor = Color.LightGray;
					recordItem.Fields("limit").FontColor = Color.LightGray;
					recordItem.Fields("sent_time").FontColor = Color.LightGray;
					recordItem.Fields("matched_time").FontColor = Color.LightGray;
					recordItem.Fields("status").FontColor = Color.Cyan;
					recordItem.Fields("order_no").FontColor = Color.Yellow;
					recordItem.Fields("error").FontColor = Color.Red;
					recordItem.Fields("condition").FontColor = Color.Yellow;
					recordItem.Fields("price_cond").FontColor = Color.LightGray;
					recordItem.Fields("pubvol").FontColor = Color.LightGray;
					recordItem.Fields("ttf").FontColor = Color.LightGray;
					recordItem.Fields("account").FontColor = Color.LightGray;
					if (text3 == "PO" || text3 == "O")
					{
						recordItem.Fields("checkbox").Text = "0";
					}
					else
					{
						recordItem.Fields("checkbox").Text = "";
					}
					recordItem.Changed = true;
					if (isRedraw)
					{
						this.intzaStopOrder.Redraw();
					}
				}
			}
			catch (Exception ex)
			{
				this.intzaOrderList.Redraw();
				this.ShowError("UpdateToControl", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateToControl()
		{
			try
			{
				if (this.tdsOrder.Tables.Contains("ORDERS"))
				{
					this.intzaOrderList.SortColumnName = string.Empty;
					this.intzaOrderList.BeginUpdate();
					this.intzaOrderList.ClearAllText();
					int num = 0;
					if (ApplicationInfo.SupportFreewill)
					{
						if (this.tdsOrder.Tables.Contains("OFFLINE"))
						{
							this.intzaOrderList.Rows = this.tdsOrder.Tables["ORDERS"].Rows.Count + this.tdsOrder.Tables["OFFLINE"].Rows.Count;
							DataRow[] array = this.tdsOrder.Tables["OFFLINE"].Select("", "OrderNo desc");
							DataRow[] array2 = array;
							int i = 0;
							while (i < array2.Length)
							{
								DataRow dataRow = array2[i];
								ucViewOrder.RecordData recordData = default(ucViewOrder.RecordData);
								long.TryParse(dataRow["OrderNo"].ToString(), out recordData.OrderNumber);
								recordData.Side = dataRow["side"].ToString();
								recordData.Stock = dataRow["SecSymbol"].ToString();
								long.TryParse(dataRow["volume"].ToString(), out recordData.Volume);
								long.TryParse(dataRow["matchvolume"].ToString(), out recordData.Matched);
								long.TryParse(dataRow["pubvolume"].ToString(), out recordData.PubVol);
								recordData.OrderStatus = dataRow["orderstatus"].ToString();
								string text = dataRow["conditionprice"].ToString().Trim();
								if (text == null)
								{
									goto IL_2A0;
								}
								if (!(text == "A"))
								{
									if (!(text == "C"))
									{
										if (!(text == "M"))
										{
											if (!(text == "K"))
											{
												if (!(text == "L"))
												{
													goto IL_2A0;
												}
												recordData.Price = "ML";
											}
											else
											{
												recordData.Price = "MO";
											}
										}
										else
										{
											recordData.Price = "MP";
										}
									}
									else
									{
										recordData.Price = "ATC";
									}
								}
								else
								{
									recordData.Price = "ATO";
								}
								IL_2C7:
								recordData.OrderTimes = Utilities.GetTime(dataRow["ordertime"].ToString());
								int.TryParse(dataRow["TrusteeId"].ToString(), out recordData.TrusteeID);
								recordData.Quote = string.Empty;
								recordData.ApprUserNo = "";
								recordData.OrderDate = dataRow["OrderDate"].ToString().Trim();
								recordData.OrderTime = dataRow["OrderTime"].ToString().Trim();
								recordData.IsAfterCloseOrder = true;
								this.UpdateToGrid(num, recordData);
								num++;
								i++;
								continue;
								IL_2A0:
								recordData.Price = Utilities.PriceFormat(dataRow["price"].ToString().Trim());
								goto IL_2C7;
							}
						}
						else
						{
							this.intzaOrderList.Rows = this.tdsOrder.Tables["ORDERS"].Rows.Count;
						}
					}
					else
					{
						this.intzaOrderList.Rows = this.tdsOrder.Tables["ORDERS"].Rows.Count;
					}
					bool flag = this.tdsOrder.Tables["ORDERS"].Columns.Contains("sSendDate");
					bool flag2 = this.tdsOrder.Tables["ORDERS"].Columns.Contains("bitApproval");
					foreach (DataRow dataRow in this.tdsOrder.Tables["ORDERS"].Rows)
					{
						ucViewOrder.RecordData recordData = default(ucViewOrder.RecordData);
						//DataRow dataRow;
						long.TryParse(dataRow["order_number"].ToString(), out recordData.OrderNumber);
						if (ApplicationInfo.SupportFreewill)
						{
							if (dataRow["side"].ToString() == "B" && dataRow["ordertype"].ToString() == "S")
							{
								recordData.Side = "C";
							}
							else
							{
								if (dataRow["side"].ToString() == "S" && dataRow["ordertype"].ToString() == "S")
								{
									recordData.Side = "H";
								}
								else
								{
									recordData.Side = dataRow["side"].ToString();
								}
							}
						}
						else
						{
							recordData.Side = dataRow["side"].ToString();
						}
						recordData.Stock = dataRow["stock"].ToString();
						long.TryParse(dataRow["volume"].ToString(), out recordData.Volume);
						long.TryParse(dataRow["matched_volume"].ToString(), out recordData.Matched);
						long.TryParse(dataRow["pub_vol"].ToString(), out recordData.PubVol);
						recordData.OrderStatus = dataRow["status"].ToString();
						recordData.Price = Utilities.PriceFormat(dataRow["price_to_set"].ToString().Trim());
						decimal d = 0m;
						decimal.TryParse(dataRow["price"].ToString(), out d);
						if (!ApplicationInfo.SupportFreewill)
						{
							decimal d2 = 0m;
							decimal.TryParse(dataRow["matched_value"].ToString(), out d2);
							if (d * recordData.Matched != d2)
							{
								recordData.Price = "*" + recordData.Price;
							}
						}
						recordData.OrderTimes = Utilities.GetTime(dataRow["order_time"].ToString());
						int.TryParse(dataRow["trustee_id"].ToString(), out recordData.TrusteeID);
						recordData.Quote = dataRow["quote"].ToString();
						if (flag2)
						{
							if (dataRow["bitApproval"].ToString().ToUpper() == "TRUE" || dataRow["bitApproval"].ToString().ToUpper() == "1")
							{
								recordData.ApprUserNo = "True";
							}
							else
							{
								recordData.ApprUserNo = string.Empty;
							}
						}
						else
						{
							recordData.ApprUserNo = "";
						}
						if (flag)
						{
							recordData.OrderDate = dataRow["sSendDate"].ToString().Trim();
						}
						else
						{
							recordData.OrderDate = string.Empty;
						}
						recordData.OrderTime = string.Empty;
						this.UpdateToGrid(num, recordData);
						num++;
					}
					if (this._OnDisplaySummaryOrders != null)
					{
						this.UpdateDisplaySummary(this.tdsOrder.Tables["ORDERS"]);
						this._OnDisplaySummaryOrders();
					}
					this.intzaOrderList.Redraw();
					if (!this.showOnMainForm)
					{
						if (this.intzaOrderList.Rows > 0)
						{
							this.intzaOrderList.Focus();
							this.intzaOrderList.SetFocusItem(0);
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.intzaOrderList.Redraw();
				this.ShowError("UpdateToControl", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateToControlReOrder()
		{
			try
			{
				if (this.tdsOrder.Tables.Contains("ORDERS"))
				{
					this.intzaReOrderList.SortColumnName = string.Empty;
					this.intzaReOrderList.BeginUpdate();
					this.intzaReOrderList.ClearAllText();
					this.intzaReOrderList.Rows = this.tdsOrder.Tables["ORDERS"].Rows.Count;
					bool flag = this.tdsOrder.Tables["ORDERS"].Columns.Contains("sSendDate");
					bool flag2 = this.tdsOrder.Tables["ORDERS"].Columns.Contains("bitApproval");
					this._orderCount = this.tdsOrder.Tables["ORDERS"].Rows.Count;
					int num = 0;
					foreach (DataRow dataRow in this.tdsOrder.Tables["ORDERS"].Rows)
					{
						ucViewOrder.RecordData recordData = default(ucViewOrder.RecordData);
						long.TryParse(dataRow["order_number"].ToString(), out recordData.OrderNumber);
						recordData.Side = dataRow["side"].ToString();
						recordData.Stock = dataRow["stock"].ToString();
						long.TryParse(dataRow["volume"].ToString(), out recordData.Volume);
						long.TryParse(dataRow["matched_volume"].ToString(), out recordData.Matched);
						long.TryParse(dataRow["pub_vol"].ToString(), out recordData.PubVol);
						recordData.OrderStatus = dataRow["status"].ToString();
						recordData.Price = Utilities.PriceFormat(dataRow["price_to_set"].ToString());
						decimal d = 0m;
						decimal.TryParse(dataRow["price"].ToString(), out d);
						if (!ApplicationInfo.SupportFreewill)
						{
							decimal d2 = 0m;
							decimal.TryParse(dataRow["matched_value"].ToString(), out d2);
							if (d * recordData.Matched != d2)
							{
								recordData.Price = "*" + recordData.Price;
							}
						}
						recordData.OrderTimes = Utilities.GetTime(dataRow["order_time"].ToString());
						int.TryParse(dataRow["trustee_id"].ToString(), out recordData.TrusteeID);
						recordData.Quote = dataRow["quote"].ToString();
						if (flag2)
						{
							if (dataRow["bitApproval"].ToString().ToUpper() == "TRUE" || dataRow["bitApproval"].ToString().ToUpper() == "1")
							{
								recordData.ApprUserNo = "True";
							}
							else
							{
								recordData.ApprUserNo = string.Empty;
							}
						}
						else
						{
							recordData.ApprUserNo = "";
						}
						if (flag)
						{
							recordData.OrderDate = dataRow["sSendDate"].ToString().Trim();
						}
						else
						{
							recordData.OrderDate = string.Empty;
						}
						this.UpdateToGridReOrder(num, recordData);
						num++;
					}
					if (this._OnDisplaySummaryOrders != null)
					{
						this.UpdateDisplaySummary(this.tdsOrder.Tables["ORDERS"]);
						this._OnDisplaySummaryOrders();
					}
					this.intzaReOrderList.Redraw();
					if (!this.showOnMainForm)
					{
						if (this.intzaReOrderList.Rows > 0)
						{
							this.intzaReOrderList.Focus();
							this.intzaReOrderList.SetFocusItem(0);
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.intzaReOrderList.Redraw();
				this.ShowError("UpdateToControlReOrder", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tstbStock_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
			case Keys.Left:
			case Keys.Up:
			case Keys.Right:
			case Keys.Down:
				e.SuppressKeyPress = true;
				break;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tstbStock_KeyUp(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != Keys.Return)
			{
				switch (keyCode)
				{
				case Keys.Left:
					this.tscbStatus.Focus();
					break;
				case Keys.Right:
					this.tstbPrice.Focus();
					break;
				case Keys.Down:
					e.SuppressKeyPress = true;
					break;
				}
			}
			else
			{
				string text = this.tstbStock.Text.Trim();
				if (text == string.Empty)
				{
					this.RequestWebData();
				}
				else
				{
					StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[text];
					if (stockInformation.Number > -1)
					{
						this.tstbStock.Text = stockInformation.Symbol;
						this.tstbStock.SelectAll();
						this.RequestWebData();
					}
					if (this.tstbStock.Text != this.selStock)
					{
						this.tstbStock.Text = this.selStock;
						this.tstbStock.Focus();
						this.tstbStock.SelectAll();
					}
				}
				e.SuppressKeyPress = true;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tscbStatus_KeyUp(object sender, KeyEventArgs e)
		{
			try
			{
				Keys keyCode = e.KeyCode;
				if (keyCode != Keys.Return)
				{
					switch (keyCode)
					{
					case Keys.Left:
						this.tscbStatus.SelectAll();
						e.SuppressKeyPress = true;
						break;
					case Keys.Right:
						this.tstbStock.Focus();
						break;
					}
				}
				else
				{
					this.RequestWebData();
					this.tscbStatus.AutoCompleteSource = AutoCompleteSource.None;
					this.tscbStatus.AutoCompleteSource = AutoCompleteSource.CustomSource;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tscbStatus_KeyUp", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tscbSide_KeyUp(object sender, KeyEventArgs e)
		{
			try
			{
				Keys keyCode = e.KeyCode;
				if (keyCode != Keys.Return)
				{
					switch (keyCode)
					{
					case Keys.Left:
						this.tstbPrice.Focus();
						break;
					case Keys.Right:
						this.tscbSide.SelectAll();
						e.SuppressKeyPress = true;
						break;
					}
				}
				else
				{
					this.RequestWebData();
					this.tscbSide.AutoCompleteSource = AutoCompleteSource.None;
					this.tscbSide.AutoCompleteSource = AutoCompleteSource.CustomSource;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tscbSide_KeyUp", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool DoSendCancelOrder(string sendDate, string sendTime, long orderNumber, string isAfterCloseFw)
		{
			bool result = false;
			string data = string.Empty;
			try
			{
				this.ShowSplash(true, "Send Cancel Order " + orderNumber + "...", false);
				if (this._viewType == 1)
				{
					if (ApplicationInfo.SupportFreewill)
					{
						this.ShowMessageInFormConfirm("Please wait for confirmation.", frmOrderFormConfirm.OpenStyle.WaitingForm);
					}
					if (isAfterCloseFw == "Y")
					{
						data = ApplicationInfo.WebOrderService.SendCancelOrder_AfterCloseFw(sendDate, sendTime.Replace(":", ""), orderNumber, ApplicationInfo.AccInfo.CurrInternetUser, ApplicationInfo.AuthenKey);
						using (DataSet dataSet = new DataSet())
						{
							MyDataHelper.StringToDataSet(data, dataSet);
							if (dataSet.Tables.Contains("Results") && dataSet.Tables["Results"].Rows.Count > 0)
							{
								if (Convert.ToInt32(dataSet.Tables["Results"].Rows[0]["code"]) >= 0)
								{
									this.ShowSplash(true, "Cancel Successful. [" + orderNumber + "]", true);
									ApplicationInfo.AddOrderNoToAutoRefreshList(orderNumber.ToString(), 3);
								}
								else
								{
									this.ShowMessageInFormConfirm("Fail >> " + dataSet.Tables["Results"].Rows[0]["message"].ToString(), frmOrderFormConfirm.OpenStyle.ShowBox);
									STIControl.SortTableGrid.RecordItem recordItem = this.intzaOrderList.Find("order_number", orderNumber.ToString());
									if (recordItem != null && recordItem.Fields("checkbox").Text.ToString() == "1")
									{
										recordItem.Fields("checkbox").Text = "0";
										this.intzaOrderList.Redraw();
									}
								}
							}
							dataSet.Clear();
						}
					}
					else
					{
						data = ApplicationInfo.WebOrderService.SendCancelOrder(sendDate, orderNumber, ApplicationInfo.AccInfo.CurrentAccount, ApplicationInfo.GetSession(), ApplicationInfo.AuthenKey, ApplicationInfo.AccInfo.CurrInternetUser, ApplicationInfo.GetTermicalId());
						using (DataSet dataSet = new DataSet())
						{
							MyDataHelper.StringToDataSet(data, dataSet);
							if (dataSet.Tables.Contains("Results") && dataSet.Tables["Results"].Rows.Count > 0)
							{
								if (Convert.ToInt32(dataSet.Tables["Results"].Rows[0]["code"]) == 0)
								{
									ApplicationInfo.AddOrderNoToAutoRefreshList(orderNumber.ToString(), 2);
									this.ShowSplash(true, "Cancel Successful. [" + orderNumber + "]", true);
								}
								else
								{
									this.ShowMessageInFormConfirm("Fail >> " + dataSet.Tables["Results"].Rows[0]["message"].ToString(), frmOrderFormConfirm.OpenStyle.ShowBox);
									STIControl.SortTableGrid.RecordItem recordItem = this.intzaOrderList.Find("order_number", orderNumber.ToString());
									if (recordItem != null && recordItem.Fields("checkbox").Text.ToString() == "1")
									{
										recordItem.Fields("checkbox").Text = "0";
										this.intzaOrderList.Redraw();
									}
								}
							}
						}
					}
				}
				else
				{
					if (this._viewType == 3)
					{
						data = ApplicationInfo.WebServiceTFEX.SendTFEXCancelOrder(orderNumber, ApplicationInfo.AccInfo.CurrentAccount, sendDate, ApplicationInfo.AccInfo.InternetUserTFEX, ApplicationInfo.AuthenKey, ApplicationInfo.IP, ApplicationInfo.GetSession(), ApplicationInfo.GetTermicalId());
						using (DataSet dataSet = new DataSet())
						{
							MyDataHelper.StringToDataSet(data, dataSet);
							if (dataSet.Tables.Contains("Results") && dataSet.Tables["Results"].Rows.Count > 0)
							{
								if (Convert.ToInt32(dataSet.Tables["Results"].Rows[0]["code"]) == 0)
								{
									ApplicationInfo.AddOrderNoToAutoRefreshList_TFEX(orderNumber.ToString());
									this.ShowSplash(true, "Cancel Successful. [" + orderNumber + "]", true);
								}
								else
								{
									this.ShowMessageInFormConfirm("Fail >> " + dataSet.Tables["Results"].Rows[0]["message"].ToString(), frmOrderFormConfirm.OpenStyle.ShowBox);
									STIControl.SortTableGrid.RecordItem recordItem = this.intzaOrderListTFEX.Find("order_number", orderNumber.ToString());
									if (recordItem != null && recordItem.Fields("checkbox").Text.ToString() == "1")
									{
										recordItem.Fields("checkbox").Text = "0";
										this.intzaOrderListTFEX.Redraw();
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowMessageInFormConfirm(ex.Message, frmOrderFormConfirm.OpenStyle.ShowBox);
			}
			this.ShowSplash(false, "", false);
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool DoCancelStopOrder(int refNo, int stockNo)
		{
			bool result = false;
			try
			{
				string data = string.Empty;
				try
				{
					this.ShowSplash(true, "Send Cancel Order " + refNo + "...", true);
					data = ApplicationInfo.WebAlertService.SendCancelStopOrder(ApplicationInfo.UserLoginID, refNo, stockNo, ApplicationInfo.AuthenKey);
					using (DataSet dataSet = new DataSet())
					{
						MyDataHelper.StringToDataSet(data, dataSet);
						if (dataSet.Tables.Contains("ORDERS") && dataSet.Tables["ORDERS"].Rows.Count > 0)
						{
							this.ShowSplash(true, "Cancel Successful. [" + refNo + "]", true);
							this.UpdateStopOrderToGrid(dataSet.Tables["ORDERS"].Rows[0], -1, false);
						}
						else
						{
							if (dataSet.Tables.Contains("Results") && dataSet.Tables["Results"].Rows.Count > 0)
							{
								long num;
								long.TryParse(dataSet.Tables["Results"].Rows[0]["code"].ToString(), out num);
								if (num <= 0L)
								{
									this.ShowMessageInFormConfirm("Fail >> " + dataSet.Tables["Results"].Rows[0]["message"].ToString(), frmOrderFormConfirm.OpenStyle.ShowBox);
								}
							}
							STIControl.ExpandTableGrid.RecordItem recordItem = this.intzaStopOrder.Find("ref_no", refNo.ToString());
							if (recordItem != null && recordItem.Fields("checkbox").Text.ToString() == "1")
							{
								recordItem.Fields("checkbox").Text = "0";
								this.intzaStopOrder.Redraw();
							}
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowMessageInFormConfirm(ex.Message, frmOrderFormConfirm.OpenStyle.ShowBox);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SendCancelOrder", ex);
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnCancelOrder_Click(object sender, EventArgs e)
		{
			try
			{
				if (this._viewType == 1)
				{
					if (this.intzaOrderList.FocusItemIndex >= 0)
					{
						if (this.intzaOrderList.Records(this.intzaOrderList.FocusItemIndex).Fields("checkbox").Text.ToString() == "0")
						{
							this.intzaOrderList.Records(this.intzaOrderList.FocusItemIndex).Fields("checkbox").Text = "1";
						}
					}
					this.CallCancelOrder();
				}
				else
				{
					if (this._viewType == 2)
					{
						if (this.intzaStopOrder.FocusItemIndex >= 0)
						{
							if (this.intzaStopOrder.Records(this.intzaStopOrder.FocusItemIndex).Fields("checkbox").Text.ToString() == "0")
							{
								this.intzaStopOrder.Records(this.intzaStopOrder.FocusItemIndex).Fields("checkbox").Text = "1";
							}
						}
						this.CancelStopOrder();
					}
					else
					{
						if (this._viewType == 3)
						{
							if (this.intzaOrderListTFEX.FocusItemIndex >= 0)
							{
								if (this.intzaOrderListTFEX.Records(this.intzaOrderListTFEX.FocusItemIndex).Fields("checkbox").Text.ToString() == "0")
								{
									this.intzaOrderListTFEX.Records(this.intzaOrderListTFEX.FocusItemIndex).Fields("checkbox").Text = "1";
								}
							}
							this.CallCancelOrder();
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tsbtnCancelOrder_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void CallCancelOrder()
		{
			if (base.InvokeRequired)
			{
				base.BeginInvoke(new MethodInvoker(this.CallCancelOrder));
			}
			else
			{
				try
				{
					if (this._listOfOrderCancel == null)
					{
						this._listOfOrderCancel = new List<ucViewOrder.CancelItem>();
					}
					else
					{
						this._listOfOrderCancel.Clear();
					}
					if (this._viewType == 1)
					{
						string text = string.Empty;
						for (int i = 0; i < this.intzaOrderList.Rows; i++)
						{
							if (this.intzaOrderList.Records(i).Fields("checkbox").Text.ToString() == "1")
							{
								text = text + "," + this.intzaOrderList.Records(i).Fields("order_number").Text;
								this._listOfOrderCancel.Add(new ucViewOrder.CancelItem(Convert.ToInt64(this.intzaOrderList.Records(i).Fields("order_number").Text), this.intzaOrderList.Records(i).Fields("send_date").Text.ToString().PadRight(10, ' '), this.intzaOrderList.Records(i).Fields("time").Text.ToString(), this.intzaOrderList.Records(i).Fields("offline").Text.ToString()));
							}
						}
						if (this._listOfOrderCancel.Count > 0)
						{
							if (text != string.Empty)
							{
								text = text.Substring(1);
							}
							this.ShowMessageInFormConfirm("Do you want to cancel order number " + text + " ?", frmOrderFormConfirm.OpenStyle.ConfirmCancel);
						}
						else
						{
							this.ShowMessageInFormConfirm("Can not find the item you want to cancel.", frmOrderFormConfirm.OpenStyle.ShowBox);
						}
					}
					else
					{
						if (this._viewType == 3)
						{
							string text = string.Empty;
							for (int i = 0; i < this.intzaOrderListTFEX.Rows; i++)
							{
								if (this.intzaOrderListTFEX.Records(i).Fields("checkbox").Text.ToString() == "1")
								{
									text = text + "," + this.intzaOrderListTFEX.Records(i).Fields("order_number").Text;
									this._listOfOrderCancel.Add(new ucViewOrder.CancelItem(Convert.ToInt64(this.intzaOrderListTFEX.Records(i).Fields("order_number").Text), this.intzaOrderListTFEX.Records(i).Fields("send_date").Text.ToString(), "", ""));
								}
							}
							if (this._listOfOrderCancel.Count > 0)
							{
								if (text != string.Empty)
								{
									text = text.Substring(1);
								}
								this.ShowMessageInFormConfirm("Do you want to cancel order number " + text + " ?", frmOrderFormConfirm.OpenStyle.ConfirmCancel);
							}
							else
							{
								this.ShowMessageInFormConfirm("Can not find the item you want to cancel.", frmOrderFormConfirm.OpenStyle.ShowBox);
							}
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("CallCancelOrder", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void CallOrderInfo(string sendDate, long orderNumber, string isFwAfterClose)
		{
			try
			{
				if (this._viewType == 1)
				{
					if (this._viewOrderInfo == null)
					{
						if (this.showOnMainForm)
						{
							this._viewOrderInfo = new frmViewOrderInfo(false, sendDate, orderNumber, base.Parent.Top + base.Top + base.Height, isFwAfterClose);
						}
						else
						{
							this._viewOrderInfo = new frmViewOrderInfo(false, sendDate, orderNumber, 0, isFwAfterClose);
						}
						this._viewOrderInfo.FormClosed -= new FormClosedEventHandler(this.viewOrderInfo_FormClosed);
						this._viewOrderInfo.FormClosed += new FormClosedEventHandler(this.viewOrderInfo_FormClosed);
						this._viewOrderInfo.TopLevel = false;
						if (base.Parent.GetType().BaseType == typeof(ClientBaseForm))
						{
							this._viewOrderInfo.Parent = base.Parent;
						}
						else
						{
							this._viewOrderInfo.Parent = base.Parent.Parent;
						}
						this._viewOrderInfo.Font = Settings.Default.Default_Font;
						this._viewOrderInfo.Show();
						this._viewOrderInfo.BringToFront();
						this._viewOrderInfo.Focus();
					}
					else
					{
						this._viewOrderInfo.Initial(sendDate, orderNumber, isFwAfterClose);
						this._viewOrderInfo.Show();
						this._viewOrderInfo.BringToFront();
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("CallOrderInfo", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void CallOrderInfoTfex(string sendDate, string orderType, long orderNumber)
		{
			try
			{
				if (this._viewOrderInfoTfex == null)
				{
					if (this.showOnMainForm)
					{
						this._viewOrderInfoTfex = new frmViewOrderInfoTFEX(orderNumber, orderType, sendDate, base.Parent.Top + base.Top + base.Height);
					}
					else
					{
						this._viewOrderInfoTfex = new frmViewOrderInfoTFEX(orderNumber, orderType, sendDate, 0);
					}
					this._viewOrderInfoTfex.FormClosed -= new FormClosedEventHandler(this.viewOrderInfoTfex_FormClosed);
					this._viewOrderInfoTfex.FormClosed += new FormClosedEventHandler(this.viewOrderInfoTfex_FormClosed);
					this._viewOrderInfoTfex.TopLevel = false;
					if (base.Parent.GetType().BaseType == typeof(ClientBaseForm))
					{
						this._viewOrderInfoTfex.Parent = base.Parent;
					}
					else
					{
						this._viewOrderInfoTfex.Parent = base.Parent.Parent;
					}
					this._viewOrderInfoTfex.Font = Settings.Default.Default_Font;
					this._viewOrderInfoTfex.Show();
					this._viewOrderInfoTfex.BringToFront();
					this._viewOrderInfoTfex.Focus();
				}
				else
				{
					this._viewOrderInfoTfex.Initial(orderNumber, orderType, sendDate);
					this._viewOrderInfoTfex.Show();
					this._viewOrderInfoTfex.BringToFront();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("CallOrderInfoTfex", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnFontChanged(EventArgs e)
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void viewOrderInfo_FormClosed(object sender, FormClosedEventArgs e)
		{
			try
			{
				this._viewOrderInfo.FormClosed -= new FormClosedEventHandler(this.viewOrderInfo_FormClosed);
				this._viewOrderInfo = null;
				if (this.tmTest == null)
				{
					this.tmTest = new System.Windows.Forms.Timer();
					this.tmTest.Interval = 100;
					this.tmTest.Tick += new EventHandler(this.tmTest_Tick);
				}
				this.tmTest.Stop();
				this.tmTest.Start();
			}
			catch (Exception ex)
			{
				this.ShowError("viewOrderInfo_FormClosed", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void viewOrderInfoTfex_FormClosed(object sender, FormClosedEventArgs e)
		{
			try
			{
				this._viewOrderInfoTfex.FormClosed -= new FormClosedEventHandler(this.viewOrderInfoTfex_FormClosed);
				this._viewOrderInfoTfex = null;
				if (this.tmTest == null)
				{
					this.tmTest = new System.Windows.Forms.Timer();
					this.tmTest.Interval = 100;
					this.tmTest.Tick += new EventHandler(this.tmTest_Tick);
				}
				this.tmTest.Stop();
				this.tmTest.Start();
			}
			catch (Exception ex)
			{
				this.ShowError("viewOrderInfo_FormClosed", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tmTest_Tick(object sender, EventArgs e)
		{
			this.tmTest.Stop();
			if (this._viewType == 1)
			{
				this.intzaOrderList.Focus();
			}
			else
			{
				if (this._viewType == 3)
				{
					this.intzaOrderListTFEX.Focus();
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnSearch_Click(object sender, EventArgs e)
		{
			this.RequestWebData();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowMessageInFormConfirm(string message, frmOrderFormConfirm.OpenStyle openStyle)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new ucViewOrder.ShowMessageInFormConfirmCallBack(this.ShowMessageInFormConfirm), new object[]
				{
					message,
					openStyle
				});
			}
			else
			{
				try
				{
					if (this.frmConfirm != null)
					{
						if (!this.frmConfirm.IsDisposed)
						{
							this.frmConfirm.Dispose();
						}
						this.frmConfirm = null;
					}
					this.frmConfirm = new frmOrderFormConfirm(message, openStyle);
					this.frmConfirm.FormClosing -= new FormClosingEventHandler(this.frmConfirm_FormClosing);
					this.frmConfirm.FormClosing += new FormClosingEventHandler(this.frmConfirm_FormClosing);
					this.frmConfirm.TopLevel = false;
					if (base.Parent.GetType() == typeof(Panel))
					{
						this.frmConfirm.Parent = base.Parent.Parent;
					}
					else
					{
						if (base.Parent.GetType().BaseType == typeof(ClientBaseForm))
						{
							this.frmConfirm.Parent = base.Parent;
						}
						else
						{
							if (base.Parent.Parent.Parent.GetType() == typeof(frmMain))
							{
								this.frmConfirm.Parent = base.Parent.Parent.Parent;
							}
						}
					}
					this.frmConfirm.Location = new Point((this.frmConfirm.Parent.Width - this.frmConfirm.Width) / 2, (this.frmConfirm.Parent.Height - this.frmConfirm.Height) / 2);
					this.frmConfirm.TopMost = true;
					this.frmConfirm.Show();
					this.frmConfirm.BringToFront();
				}
				catch (Exception ex)
				{
					this.ShowError("ShowMessageInFormConfirm", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void LoopCancelOrder()
		{
			try
			{
				if (this._viewType == 1)
				{
					foreach (ucViewOrder.CancelItem current in this._listOfOrderCancel)
					{
						if (current.OrderNo > 0L)
						{
							this.DoSendCancelOrder(current.OrderDate, current.OrderTime, current.OrderNo, current.AfterClose);
							Thread.Sleep(20);
						}
					}
					this.SetGridFocus(false);
				}
				else
				{
					if (this._viewType == 2)
					{
						foreach (KeyValuePair<int, int> current2 in this._stopOrderCancelLst)
						{
							this.DoCancelStopOrder(current2.Key, current2.Value);
							Thread.Sleep(20);
						}
						this.intzaStopOrder.Redraw();
					}
					else
					{
						if (this._viewType == 3)
						{
							foreach (ucViewOrder.CancelItem current in this._listOfOrderCancel)
							{
								if (current.OrderNo > 0L)
								{
									this.DoSendCancelOrder(current.OrderDate, "", current.OrderNo, "N");
									Thread.Sleep(20);
								}
							}
							this.SetGridFocus(false);
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("LoopCancelOrder", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmConfirm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				DialogResult result = ((frmOrderFormConfirm)sender).Result;
				frmOrderFormConfirm.OpenStyle openFormStyle = ((frmOrderFormConfirm)sender).OpenFormStyle;
				if (openFormStyle == frmOrderFormConfirm.OpenStyle.ConfirmCancel)
				{
					if (result == DialogResult.OK)
					{
						Thread thread = new Thread(new ThreadStart(this.LoopCancelOrder));
						thread.Start();
					}
					else
					{
						if (this._viewType == 1 || this._viewType == 3)
						{
							foreach (ucViewOrder.CancelItem current in this._listOfOrderCancel)
							{
								if (current.OrderNo > 0L)
								{
									if (this._viewType == 1)
									{
										int index = this.intzaOrderList.FindIndex("order_number", current.OrderNo.ToString());
										if (this.intzaOrderList.Records(index).Fields("checkbox").Text.ToString() == "1")
										{
											this.intzaOrderList.Records(index).Fields("checkbox").Text = "0";
										}
									}
									else
									{
										if (this._viewType == 3)
										{
											int index = this.intzaOrderListTFEX.FindIndex("order_number", current.OrderNo.ToString());
											if (this.intzaOrderListTFEX.Records(index).Fields("checkbox").Text.ToString() == "1")
											{
												this.intzaOrderListTFEX.Records(index).Fields("checkbox").Text = "0";
											}
										}
									}
								}
							}
							if (this._viewType == 1 || this._viewType == 2)
							{
								this.intzaOrderList.Redraw();
							}
							else
							{
								this.intzaOrderListTFEX.Redraw();
							}
							this.SetGridFocus(false);
						}
					}
				}
				else
				{
					if (openFormStyle == frmOrderFormConfirm.OpenStyle.ConfirmSendNew)
					{
						if (result == DialogResult.OK)
						{
							this.tsbtnReorder.Enabled = false;
							this.thrSendOrder = new Thread(new ThreadStart(this.LoopReOrderThroughGrid));
							this.thrSendOrder.Start();
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("ConfirmForm", ex);
			}
			this._isSelectAll = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void controlOrder_Enter(object sender, EventArgs e)
		{
			try
			{
				if (sender.GetType() == typeof(ToolStripTextBox))
				{
					((ToolStripTextBox)sender).BackColor = Color.Yellow;
					((ToolStripTextBox)sender).ForeColor = Color.Black;
					((ToolStripTextBox)sender).SelectAll();
				}
				else
				{
					if (sender.GetType() == typeof(ToolStripComboBox))
					{
						((ToolStripComboBox)sender).BackColor = Color.Yellow;
						((ToolStripComboBox)sender).ForeColor = Color.Black;
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("controlOrder_Enter", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void controlOrder_Leave(object sender, EventArgs e)
		{
			try
			{
				if (sender.GetType() == typeof(ToolStripTextBox))
				{
					((ToolStripTextBox)sender).BackColor = Color.FromArgb(45, 45, 45);
					((ToolStripTextBox)sender).ForeColor = Color.Cyan;
				}
				else
				{
					if (sender.GetType() == typeof(ToolStripComboBox))
					{
						((ToolStripComboBox)sender).BackColor = Color.FromArgb(45, 45, 45);
						((ToolStripComboBox)sender).ForeColor = Color.Cyan;
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("controlOrder_Leave", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tscbStatus_TextChanged(object sender, EventArgs e)
		{
			if (!base.DesignMode)
			{
				if (this.isActive)
				{
					this.tscbStatus.Text = this.tscbStatus.Text.ToUpper();
					this.tscbStatus.SelectionStart = this.tscbStatus.Text.Length;
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tscbSide_TextChanged(object sender, EventArgs e)
		{
			if (!base.DesignMode)
			{
				if (this.isActive)
				{
					this.tscbSide.Text = this.tscbSide.Text.ToUpper();
					this.tscbSide.SelectionStart = this.tscbSide.Text.Length;
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ucViewOrder_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
			case Keys.Space:
				if (this.tscbStatus.Focused)
				{
					this.tscbStatus.Text = "ALL";
					this.tscbStatus.SelectAll();
				}
				else
				{
					if (this.tstbStock.Focused)
					{
						this.tstbStock.Text = string.Empty;
					}
				}
				e.SuppressKeyPress = true;
				break;
			case Keys.Prior:
			case Keys.Next:
				e.SuppressKeyPress = true;
				break;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool ClearCondition()
		{
			bool result;
			try
			{
				this.tscbStatus.Text = "ALL";
				this.tscbSide.Text = "ALL";
				this.tstbStock.Text = string.Empty;
				this.tstbPrice.Text = string.Empty;
				if (this.selStatus != string.Empty || this.selSide != string.Empty || this.selStock != string.Empty)
				{
					result = true;
					return result;
				}
				result = false;
				return result;
			}
			catch (Exception ex)
			{
				this.ShowError("ClearCondition", ex);
			}
			result = false;
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnClearCondition_Click(object sender, EventArgs e)
		{
			this.ClearCondition();
			this.RequestWebData();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected virtual void ShowError(string functionName, Exception ex)
		{
			ExceptionManager.Show(new ErrorItem(DateTime.Now, base.Name, functionName, ex.Message, ex.ToString()));
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateDisplaySummary(DataTable dt)
		{
			try
			{
				this.totalBuyVolume = 0L;
				this.totalBuyMatchedVolume = 0L;
				this.totalBuyMatchedValue = 0m;
				this.totalSellVolume = 0L;
				this.totalSellMatchedVolume = 0L;
				this.totalSellMatchedValue = 0m;
				this.unMatchedBuyVol = 0m;
				this.unMatchedSellVol = 0m;
				if (dt.Rows.Count > 0)
				{
					long num = 0L;
					long num2 = 0L;
					decimal d = 0m;
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						long.TryParse(dt.Rows[i]["volume"].ToString(), out num);
						long.TryParse(dt.Rows[i]["matched_volume"].ToString(), out num2);
						decimal.TryParse(dt.Rows[i]["matched_value"].ToString(), out d);
						if (dt.Rows[i]["side"].ToString().ToLower() == "b")
						{
							this.totalBuyVolume += num;
							this.totalBuyMatchedVolume += num2;
							this.totalBuyMatchedValue += d;
							string text = dt.Rows[i]["status"].ToString().Trim();
							if (text != null)
							{
								if (text == "O" || text == "OA" || text == "OC" || text == "OAC" || text == "PO" || text == "POA")
								{
									this.unMatchedBuyVol += num - num2;
								}
							}
						}
						else
						{
							if (dt.Rows[i]["side"].ToString().ToLower() == "s")
							{
								this.totalSellVolume += num;
								this.totalSellMatchedVolume += num2;
								this.totalSellMatchedValue += d;
								string text = dt.Rows[i]["status"].ToString().Trim();
								if (text != null)
								{
									if (text == "O" || text == "OA" || text == "OC" || text == "OAC" || text == "PO" || text == "POA")
									{
										this.unMatchedSellVol += num - num2;
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateDisplaySummary", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool CanShowCheckBox(string status)
		{
			string text = status.Trim();
			bool result;
			switch (text)
			{
			case "O":
			case "OA":
			case "OC":
			case "OAC":
			case "PO":
			case "POA":
			case "A":
			case "AM":
				result = true;
				return result;
			}
			result = false;
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaOrderList_TableMouseDoubleClick(object sender, STIControl.SortTableGrid.TableMouseEventArgs e)
		{
			try
			{
				if (e.Column.Name == "checkbox" && e.RowIndex == -1)
				{
					this.tsbtnCancelOrder.PerformClick();
				}
				else
				{
					long orderNumber = Convert.ToInt64(this.intzaOrderList.Records(e.RowIndex).Fields("order_number").Text);
					string sendDate = this.intzaOrderList.Records(this.intzaOrderList.FocusItemIndex).Fields("send_date").Text.ToString().Trim();
					string isFwAfterClose = this.intzaOrderList.Records(this.intzaOrderList.FocusItemIndex).Fields("offline").Text.ToString().Trim();
					this.CallOrderInfo(sendDate, orderNumber, isFwAfterClose);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("lsvOrderList_DoubleClick", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaOrderList_TableMouseClick(object sender, STIControl.SortTableGrid.TableMouseEventArgs e)
		{
			try
			{
				if (e.Mouse.Button == MouseButtons.Left)
				{
					if (e.RowIndex == -1)
					{
						string name = e.Column.Name;
						switch (name)
						{
						case "checkbox":
							this.SelectAllOrderForCancel(!this._isSelectAll);
							break;
						case "order_number":
						case "side":
						case "stock":
						case "status":
						case "time":
						case "ttf":
						case "quote":
							if (this.intzaOrderList.SortType == SortType.Asc)
							{
								this.intzaOrderList.Sort(e.Column.Name, SortType.Desc);
							}
							else
							{
								this.intzaOrderList.Sort(e.Column.Name, SortType.Asc);
							}
							this.intzaOrderList.Redraw();
							break;
						}
					}
					else
					{
						string name = e.Column.Name;
						if (name != null)
						{
							if (!(name == "checkbox"))
							{
								if (name == "info")
								{
									if (this.intzaOrderList.FocusItemIndex >= 0)
									{
										long orderNumber = Convert.ToInt64(this.intzaOrderList.Records(this.intzaOrderList.FocusItemIndex).Fields("order_number").Text);
										string sendDate = this.intzaOrderList.Records(this.intzaOrderList.FocusItemIndex).Fields("send_date").Text.ToString().Trim();
										string isFwAfterClose = this.intzaOrderList.Records(this.intzaOrderList.FocusItemIndex).Fields("offline").Text.ToString().Trim();
										this.CallOrderInfo(sendDate, orderNumber, isFwAfterClose);
									}
								}
							}
							else
							{
								if (this.intzaOrderList.Records(e.RowIndex).Fields("checkbox").Text.ToString() == "1")
								{
									this.CallCancelOrder();
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("intzaOrderList_TableMouseClick", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaOrderList_TableKeyDown(KeyEventArgs e)
		{
			try
			{
				if (this.isActive)
				{
					Keys keyCode = e.KeyCode;
					if (keyCode != Keys.Return)
					{
						switch (keyCode)
						{
						case Keys.Left:
						case Keys.Right:
							e.SuppressKeyPress = true;
							break;
						case Keys.Up:
							break;
						default:
							if (keyCode == Keys.Insert)
							{
								if (!e.Control)
								{
									this.CallCancelOrder();
								}
							}
							break;
						}
					}
					else
					{
						if (this.intzaOrderList.Rows > 0 && this.intzaOrderList.FocusItemIndex > -1 && this.intzaOrderList.FocusItemIndex < this.intzaOrderList.Rows)
						{
							long num;
							long.TryParse(this.intzaOrderList.Records(this.intzaOrderList.FocusItemIndex).Fields("order_number").Text.ToString(), out num);
							string sendDate = this.intzaOrderList.Records(this.intzaOrderList.FocusItemIndex).Fields("send_date").Text.ToString().Trim();
							string isFwAfterClose = this.intzaOrderList.Records(this.intzaOrderList.FocusItemIndex).Fields("offline").Text.ToString().Trim();
							if (num > 0L)
							{
								this.CallOrderInfo(sendDate, num, isFwAfterClose);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("intzaOrderList_TableKeyDown", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ucViewOrder_VisibleChanged(object sender, EventArgs e)
		{
			if (!base.DesignMode)
			{
				if (!base.Visible)
				{
					this.CloseViewOrderInfoBox();
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void CloseViewOrderInfoBox()
		{
			try
			{
				if (this._viewOrderInfo != null)
				{
					this._viewOrderInfo.Close();
					this._viewOrderInfo = null;
				}
				if (this.frmConfirm != null)
				{
					this.frmConfirm.FormClosing -= new FormClosingEventHandler(this.frmConfirm_FormClosing);
					if (!this.frmConfirm.IsDisposed)
					{
						this.frmConfirm.Dispose();
					}
					this.frmConfirm = null;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("CloseViewOrderInfoBox", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tstbPrice_KeyUp(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != Keys.Return)
			{
				switch (keyCode)
				{
				case Keys.Left:
					this.tstbStock.Focus();
					break;
				case Keys.Right:
					this.tscbSide.Focus();
					break;
				case Keys.Down:
					e.SuppressKeyPress = true;
					break;
				}
			}
			else
			{
				this.RequestWebData();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tscbStatus_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
			case Keys.Prior:
			case Keys.Next:
				e.SuppressKeyPress = true;
				break;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SelectAllOrderForCancel(bool setSelectAll)
		{
			try
			{
				if (this.intzaOrderList.InvokeRequired)
				{
					this.intzaOrderList.Invoke(new ucViewOrder.SelectAllOrderForCancelCallBack(this.SelectAllOrderForCancel), new object[]
					{
						setSelectAll
					});
				}
				else
				{
					this._isSelectAll = setSelectAll;
					if (this._viewType == 1)
					{
						for (int i = 0; i < this.intzaOrderList.Rows; i++)
						{
							if (this.intzaOrderList.Records(i).Fields("checkbox").Text.ToString() == "1" || this.intzaOrderList.Records(i).Fields("checkbox").Text.ToString() == "0")
							{
								if (this._isSelectAll)
								{
									this.intzaOrderList.Records(i).Fields("checkbox").Text = "1";
								}
								else
								{
									this.intzaOrderList.Records(i).Fields("checkbox").Text = "0";
								}
								this.intzaOrderList.Records(i).Changed = true;
							}
						}
						this.intzaOrderList.EndUpdate();
					}
					else
					{
						if (this._viewType == 3)
						{
							for (int i = 0; i < this.intzaOrderListTFEX.Rows; i++)
							{
								if (this.intzaOrderListTFEX.Records(i).Fields("checkbox").Text.ToString() == "1" || this.intzaOrderListTFEX.Records(i).Fields("checkbox").Text.ToString() == "0")
								{
									if (this._isSelectAll)
									{
										this.intzaOrderListTFEX.Records(i).Fields("checkbox").Text = "1";
									}
									else
									{
										this.intzaOrderListTFEX.Records(i).Fields("checkbox").Text = "0";
									}
									this.intzaOrderListTFEX.Records(i).Changed = true;
								}
							}
							this.intzaOrderListTFEX.EndUpdate();
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SelectAllOrderForCancel", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsmRefresh_Click(object sender, EventArgs e)
		{
			this.RequestWebData();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsmCancelOrder_Click(object sender, EventArgs e)
		{
			this.CallCancelOrder();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetHeader()
		{
			try
			{
				foreach (STIControl.SortTableGrid.ColumnItem current in this.intzaOrderList.Columns)
				{
					current.BackColor = Settings.Default.HeaderBackGColor;
					current.FontColor = Settings.Default.HeaderFontColor;
				}
				this.intzaOrderList.GridColor = Settings.Default.GridColor;
				this.intzaOrderList.Redraw();
			}
			catch (Exception ex)
			{
				this.ShowError("SetHeader", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetHeaderHeightPct(int pct)
		{
			try
			{
				this.intzaOrderList.HeaderPctHeight = (float)pct;
			}
			catch (Exception ex)
			{
				this.ShowError("SetHeaderHeightPct", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaOrderList_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				this.contextMenuStrip1.Show(this.intzaOrderList, e.X, e.Y);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnEditOrder_Click(object sender, EventArgs e)
		{
			this.ShowEditForm();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowEditForm()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(this.ShowEditForm));
			}
			else
			{
				try
				{
					STIControl.SortTableGrid.RecordItem recordItem = this.intzaOrderList.Records(this.intzaOrderList.FocusItemIndex);
					string text = recordItem.Fields("status").Text.ToString();
					if (text == "O" || text == "OC" || text == "OA" || text == "OAC" || text == "M" || text == "MA" || text == "MC" || text == "MAC" || (ApplicationInfo.SupportFreewill && (text == "PO" || text == "POC")) || text.IndexOf("(O)") > 0 || text.IndexOf("(OC)") > 0 || text.IndexOf("(OA)") > 0 || text.IndexOf("(OAC)") > 0 || text.IndexOf("(M)") > 0 || text.IndexOf("(MA)") > 0 || text.IndexOf("(MC)") > 0 || text.IndexOf("(MAC)") > 0)
					{
						if (this._editOrderBox != null)
						{
							if (!this._editOrderBox.IsDisposed)
							{
								this._editOrderBox.Dispose();
							}
							this._editOrderBox = null;
						}
						if (ApplicationInfo.SupportFreewill)
						{
							if (recordItem.Fields("offline").Text.ToString() == "Y")
							{
								this.ShowMessageInFormConfirm("AfterClose order cannot edit order.", frmOrderFormConfirm.OpenStyle.ShowBox);
								return;
							}
						}
						frmEditOrder.OrderEditRecord recordData;
						recordData.OrderNumber = Convert.ToInt64(recordItem.Fields("order_number").Text.ToString());
						recordData.Side = recordItem.Fields("side").Text.ToString();
						recordData.Stock = recordItem.Fields("stock").Text.ToString();
						int trusteeID = 0;
						int.TryParse(recordItem.Fields("ttf").Text.ToString(), out trusteeID);
						recordData.TrusteeID = trusteeID;
						recordData.Volume = Convert.ToInt64(recordItem.Fields("volume").Text.ToString().Replace(",", ""));
						recordData.Price = recordItem.Fields("price").Text.ToString();
						recordData.EntryDate = recordItem.Fields("send_date").Text.ToString();
						long.TryParse(recordItem.Fields("published").Text.ToString().Replace(",", ""), out recordData.PubVol);
						this._editOrderBox = new frmEditOrder(recordData);
						this._editOrderBox.TopLevel = false;
						this._editOrderBox.Parent = this;
						Rectangle position = this.intzaOrderList.GetPosition(this.intzaOrderList.FocusItemIndex, "order_number");
						this._editOrderBox.Location = new Point(0, this.intzaOrderList.Top + position.Top + position.Height);
						this._editOrderBox.TopMost = true;
						this._editOrderBox.Show();
						this._editOrderBox.BringToFront();
					}
					else
					{
						this.ShowMessageInFormConfirm("Invalid order status.", frmOrderFormConfirm.OpenStyle.ShowBox);
					}
				}
				catch (Exception ex)
				{
					this.ShowError("ShowEditForm", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnViewOrder_Click(object sender, EventArgs e)
		{
			this.SetView(1);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnStopOrder_Click(object sender, EventArgs e)
		{
			this.SetView(2);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetView(int viewType)
		{
			try
			{
				this._viewType = viewType;
				this._isSelectAll = false;
				if (this._viewType == 1)
				{
					this.intzaOrderList.Visible = true;
					this.intzaStopOrder.Visible = false;
					this.intzaOrderListTFEX.Visible = false;
					this.intzaReOrderList.Visible = false;
					this.tslbStatus.Visible = true;
					this.tscbStatus.Visible = true;
					this.tslbStock.Visible = true;
					this.tstbStock.Visible = true;
					this.tslbPrice.Visible = true;
					this.tstbPrice.Visible = true;
					this.tslbSide.Visible = true;
					this.tscbSide.Visible = true;
					if (ApplicationInfo.PCCanEditorder == "Y")
					{
						this.tsbtnEditOrder.Visible = true;
					}
					else
					{
						this.tsbtnEditOrder.Visible = false;
					}
					this.tsbtnClearCondition.Visible = !this.showOnMainForm;
					this.tsbtnSearch.Visible = true;
					this.tsbtnReloadReorder.Visible = false;
					this.tsbtnCancelOrder.Visible = true;
					this.tsbtnReorder.Visible = false;
				}
				else
				{
					if (this._viewType == 2)
					{
						this.intzaStopOrder.Visible = true;
						this.intzaOrderList.Visible = false;
						this.intzaOrderListTFEX.Visible = false;
						this.intzaReOrderList.Visible = false;
						this.tsbtnClearCondition.Visible = false;
						this.tscbSide.Visible = false;
						this.tslbSide.Visible = false;
						this.tslbPrice.Visible = false;
						this.tstbPrice.Visible = false;
						this.tstbStock.Visible = false;
						this.tslbStock.Visible = false;
						this.tscbStatus.Visible = false;
						this.tslbStatus.Visible = false;
						this.tsbtnSearch.Visible = true;
						this.tsbtnCancelOrder.Visible = true;
						this.tsbtnEditOrder.Visible = false;
						this.tsbtnReloadReorder.Visible = false;
						this.tsbtnReorder.Visible = false;
					}
					else
					{
						if (this._viewType == 3)
						{
							this.intzaOrderListTFEX.Visible = true;
							this.intzaOrderList.Visible = false;
							this.intzaStopOrder.Visible = false;
							this.intzaReOrderList.Visible = false;
							this.tsbtnReorder.Visible = false;
							this.tsbtnSearch.Visible = true;
							this.tsbtnReloadReorder.Visible = false;
							this.tsbtnCancelOrder.Visible = true;
							this.tsbtnEditOrder.Visible = false;
						}
						else
						{
							if (this._viewType != 4)
							{
								return;
							}
							this.intzaReOrderList.Visible = true;
							this.intzaOrderList.Visible = false;
							this.intzaStopOrder.Visible = false;
							this.intzaOrderListTFEX.Visible = false;
							this.tslbStatus.Visible = false;
							this.tscbStatus.Visible = false;
							this.tslbStock.Visible = false;
							this.tstbStock.Visible = false;
							this.tslbPrice.Visible = false;
							this.tstbPrice.Visible = false;
							this.tslbSide.Visible = false;
							this.tscbSide.Visible = false;
							this.tsbtnEditOrder.Visible = false;
							this.tsbtnClearCondition.Visible = false;
							this.tsbtnCancelOrder.Visible = false;
							this.tsbtnSearch.Visible = false;
							this.tsbtnReloadReorder.Visible = true;
							this.tsbtnReorder.Visible = true;
							this.tsbtnReorder.Enabled = true;
						}
					}
				}
				this.SetResize(true);
				this.RequestWebData();
			}
			catch (Exception ex)
			{
				this.ShowError("SetView", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void CancelStopOrder()
		{
			if (base.InvokeRequired)
			{
				base.BeginInvoke(new MethodInvoker(this.CancelStopOrder));
			}
			else
			{
				try
				{
					if (this._stopOrderCancelLst == null)
					{
						this._stopOrderCancelLst = new Dictionary<int, int>();
					}
					else
					{
						this._stopOrderCancelLst.Clear();
					}
					string text = string.Empty;
					for (int i = 0; i < this.intzaStopOrder.Rows; i++)
					{
						if (this.intzaStopOrder.Records(i).Fields("checkbox").Text.ToString() == "1")
						{
							text = text + "," + this.intzaStopOrder.Records(i).Fields("ref_no").Text;
							StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[this.intzaStopOrder.Records(i).Fields("stock").Text.ToString()];
							if (stockInformation.Number > 0)
							{
								this._stopOrderCancelLst.Add(Convert.ToInt32(this.intzaStopOrder.Records(i).Fields("ref_no").Text), stockInformation.Number);
							}
						}
					}
					if (this._stopOrderCancelLst.Count > 0)
					{
						if (text != string.Empty)
						{
							text = text.Substring(1);
						}
						this.ShowMessageInFormConfirm("Do you want to cancel stop order number " + text + " ?", frmOrderFormConfirm.OpenStyle.ConfirmCancel);
					}
					else
					{
						this.ShowMessageInFormConfirm("Can not find the item you want to cancel.", frmOrderFormConfirm.OpenStyle.ShowBox);
					}
				}
				catch (Exception ex)
				{
					this.ShowError("CallCancelOrder", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaOrderListTFEX_TableMouseClick(object sender, STIControl.SortTableGrid.TableMouseEventArgs e)
		{
			try
			{
				if (e.Mouse.Button == MouseButtons.Left)
				{
					if (e.RowIndex == -1)
					{
						string name = e.Column.Name;
						if (name != null)
						{
							if (name == "checkbox")
							{
								this.SelectAllOrderForCancel(!this._isSelectAll);
							}
						}
					}
					else
					{
						string name = e.Column.Name;
						if (name != null)
						{
							if (!(name == "checkbox"))
							{
								if (name == "info")
								{
									if (this.intzaOrderListTFEX.FocusItemIndex >= 0)
									{
										long orderNumber = Convert.ToInt64(this.intzaOrderListTFEX.Records(e.RowIndex).Fields("order_number").Text);
										string sendDate = this.intzaOrderListTFEX.Records(this.intzaOrderListTFEX.FocusItemIndex).Fields("send_date").Text.ToString().Trim();
										string orderType = this.intzaOrderListTFEX.Records(this.intzaOrderListTFEX.FocusItemIndex).Fields("ordType").Text.ToString().Trim();
										this.CallOrderInfoTfex(sendDate, orderType, orderNumber);
									}
								}
							}
							else
							{
								if (this.intzaOrderListTFEX.Records(e.RowIndex).Fields("checkbox").Text.ToString() == "1")
								{
									this.CallCancelOrder();
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("intzaOrderList_TableMouseClick", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaOrderListTFEX_TableMouseDoubleClick(object sender, STIControl.SortTableGrid.TableMouseEventArgs e)
		{
			try
			{
				if (!(e.Column.Name == "checkbox") || e.RowIndex != -1)
				{
					long orderNumber = Convert.ToInt64(this.intzaOrderListTFEX.Records(e.RowIndex).Fields("order_number").Text);
					string sendDate = this.intzaOrderListTFEX.Records(this.intzaOrderListTFEX.FocusItemIndex).Fields("send_date").Text.ToString().Trim();
					string orderType = this.intzaOrderListTFEX.Records(this.intzaOrderListTFEX.FocusItemIndex).Fields("ordType").Text.ToString().Trim();
					this.CallOrderInfoTfex(sendDate, orderType, orderNumber);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("lsvOrderList_DoubleClick", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnReorder_Click(object sender, EventArgs e)
		{
			if (ApplicationInfo.MarketState != "C")
			{
				this.ShowMessageInFormConfirm("This function can be run on 'Market Close' only", frmOrderFormConfirm.OpenStyle.ShowBox);
			}
			else
			{
				if (this._orderCount > 0)
				{
					this.ShowMessageInFormConfirm("Confirm to send " + this.intzaReOrderList.Rows + " orders?", frmOrderFormConfirm.OpenStyle.ConfirmSendNew);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void LoopReOrderThroughGrid()
		{
			try
			{
				string symbol = string.Empty;
				string side = string.Empty;
				long num = 0L;
				string price = string.Empty;
				long num2 = 0L;
				long num3 = 0L;
				string condition = string.Empty;
				int ttf = 0;
				string empty = string.Empty;
				string empty2 = string.Empty;
				int num4 = 1;
				int num5 = 0;
				int num6 = 0;
				int num7 = 0;
				for (int i = 0; i < this.intzaReOrderList.Rows; i++)
				{
					STIControl.SortTableGrid.RecordItem recordItem = this.intzaReOrderList.Records(i);
					side = recordItem.Fields("side").Text.ToString();
					symbol = recordItem.Fields("stock").Text.ToString();
					long.TryParse(recordItem.Fields("volume").Text.ToString().Replace(",", ""), out num);
					price = recordItem.Fields("price").Text.ToString().Replace(",", "");
					long.TryParse(recordItem.Fields("published").Text.ToString().Replace(",", ""), out num2);
					long.TryParse(recordItem.Fields("matched").Text.ToString().Replace(",", ""), out num3);
					condition = recordItem.Fields("cond").Text.ToString();
					int.TryParse(recordItem.Fields("ttf").Text.ToString(), out ttf);
					empty = string.Empty;
					num -= num3;
					num6++;
					ApplicationInfo.SendNewOrderResult sendNewOrderResult = ApplicationInfo.SendNewOrder(symbol, side, num, price, num, condition, ttf, empty);
					if (sendNewOrderResult.OrderNo > 0L)
					{
						ApplicationInfo.AddOrderNoToAutoRefreshList(sendNewOrderResult.OrderNo.ToString(), sendNewOrderResult.IsFwOfflineOrder ? 3 : 1);
						this.UpdateOrderResult("Success [" + sendNewOrderResult.OrderNo.ToString() + "]", Color.Lime, i);
						num5++;
					}
					else
					{
						this.UpdateOrderResult(sendNewOrderResult.ResultMessage, Color.Yellow, i);
					}
					if (num4 == 50)
					{
						num4 = 0;
						Thread.Sleep(2000);
					}
					num4++;
				}
				this.ShowMessageInFormConfirm(string.Concat(new object[]
				{
					"Send order :: Succeed ",
					num5,
					" orders, Failed ",
					num6 - num5,
					" orders, Invalid orders ",
					num7,
					" orders."
				}), frmOrderFormConfirm.OpenStyle.ShowBox);
			}
			catch (Exception ex)
			{
				this.ShowError("LoopThroughGrid", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateOrderResult(string text, Color color, int currRow)
		{
			if (this.intzaReOrderList.InvokeRequired)
			{
				this.intzaReOrderList.Invoke(new ucViewOrder.UpdateOrderResultCallback(this.UpdateOrderResult), new object[]
				{
					text,
					color,
					currRow
				});
			}
			else
			{
				try
				{
					this.intzaReOrderList.Records(currRow).Fields("result").Text = text;
					this.intzaReOrderList.Records(currRow).Fields("result").FontColor = color;
					this.intzaReOrderList.Redraw();
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnReloadReorder_Click(object sender, EventArgs e)
		{
			if (this._viewType == 4)
			{
				this.RequestWebData();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaStopOrder_TableMouseDoubleClick(object sender, STIControl.SortTableGrid.TableMouseEventArgs e)
		{
			try
			{
				if (!this._loadStopOrderFLS && e.Column.Name == "status")
				{
					STIControl.ExpandTableGrid.RecordItem recordItem = this.intzaStopOrder.Records(e.RowIndex);
					string stockSymbol = recordItem.Fields("stock").Text.ToString();
					string text = recordItem.Fields("sent_time").Text.ToString().Replace(":", "");
					string a = recordItem.Fields("status").Text.ToString();
					if (a == "F")
					{
						this.lbFLS.Text = "พบข้อผิดพลาด";
						this.tbStopOrderFLS.Text = recordItem.Fields("message").Text.ToString().Trim();
					}
					else
					{
						if (a != "X" && text.Length == 6 && recordItem.Fields("con_price").Text.ToString().Trim() != string.Empty)
						{
							this._loadStopOrderFLS = true;
							this.panelStopOrderFLS.Location = new Point(this.intzaStopOrder.Left + e.FieldPosition.X, this.intzaStopOrder.Top + e.FieldPosition.Y + (int)this.intzaStopOrder.RowHeight);
							ApplicationInfo.WebAlertService.ViewStopOrder_FirstLSCompleted -= new ViewStopOrder_FirstLSCompletedEventHandler(this.MyWebSET_ViewStopOrder_FirstLSCompleted);
							ApplicationInfo.WebAlertService.ViewStopOrder_FirstLSCompleted += new ViewStopOrder_FirstLSCompletedEventHandler(this.MyWebSET_ViewStopOrder_FirstLSCompleted);
							ApplicationInfo.WebAlertService.ViewStopOrder_FirstLSAsync(ApplicationInfo.StockInfo[stockSymbol].Number, "price" + recordItem.Fields("con_operator").Text.ToString() + recordItem.Fields("con_price").Text.ToString(), text);
						}
					}
				}
			}
			catch (Exception ex)
			{
				this._loadStopOrderFLS = false;
				this.ShowError("intzaStopOrder_TableMouseDoubleClick", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void MyWebSET_ViewStopOrder_FirstLSCompleted(object sender, ViewStopOrder_FirstLSCompletedEventArgs e)
		{
			try
			{
				ApplicationInfo.WebAlertService.ViewStopOrder_FirstLSCompleted -= new ViewStopOrder_FirstLSCompletedEventHandler(this.MyWebSET_ViewStopOrder_FirstLSCompleted);
				if (e.Error == null)
				{
					using (DataSet dataSet = new DataSet())
					{
						if (!string.IsNullOrEmpty(e.Result))
						{
							MyDataHelper.StringToDataSet(e.Result, dataSet);
							if (dataSet != null && dataSet.Tables.Contains("TAB"))
							{
								string text = "ไม่พบรายการที่ค้นหา!";
								if (dataSet.Tables["TAB"].Rows.Count > 0)
								{
									DataRow dataRow = dataSet.Tables["TAB"].Rows[0];
									text = string.Concat(new string[]
									{
										Utilities.GetTime(dataRow["server_time"].ToString()),
										", side ",
										dataRow["side"].ToString(),
										", @",
										dataRow["price"].ToString(),
										", vol ",
										dataRow["volume"].ToString()
									});
								}
								this.lbFLS.Text = "รายการจับคู่ที่เกิดหลังจากการส่งคำสั่ง";
								this.tbStopOrderFLS.Text = text;
								this.panelStopOrderFLS.Show();
								this.panelStopOrderFLS.BringToFront();
							}
							dataSet.Clear();
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("MyWebSET_ViewStopOrder_FirstLSCompleted", ex);
			}
			this._loadStopOrderFLS = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnStopOrderFLSclose_Click(object sender, EventArgs e)
		{
			this.panelStopOrderFLS.Hide();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void panelStopOrderFLS_Paint(object sender, PaintEventArgs e)
		{
			try
			{
				e.Graphics.DrawRectangle(Pens.DimGray, 0, 0, this.panelStopOrderFLS.Width - 1, this.panelStopOrderFLS.Height - 1);
			}
			catch
			{
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaOrderListTFEX_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				this.contextMenuStrip1.Show(this.intzaOrderListTFEX, e.X, e.Y);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaStopOrder_TableMouseClick(object sender, STIControl.ExpandTableGrid.TableMouseEventArgs e)
		{
			try
			{
				if (e.Mouse.Button == MouseButtons.Left)
				{
					string name = e.Column.Name;
					if (name != null)
					{
						if (name == "checkbox")
						{
							if (this.intzaStopOrder.Records(e.RowIndex).Fields("checkbox").Text.ToString() == "1")
							{
								this.CancelStopOrder();
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("intzaOrderList_TableMouseClick", ex);
			}
		}
	}
}
