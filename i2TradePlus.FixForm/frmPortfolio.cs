using CSVReaderTest;
using i2TradePlus.Information;
using i2TradePlus.Properties;
using ITSNet.Common.BIZ;
using ITSNet.Common.BIZ.RealtimeMessage;
using STIControl;
using STIControl.CustomGrid;
using STIControl.i2Chart.Finance;
using STIControl.i2Chart.Finance.DataProvider;
using STIControl.i2Chart.Object;
using STIControl.SortTableGrid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace i2TradePlus.FixForm
{
	public class frmPortfolio : ClientBaseForm, IRealtimeMessage
	{
		public struct RecViewCustomerConfirmSummary
		{
			public string Side;
			public string Stock;
			public string TTF;
			public long Volume;
			public decimal Price;
			public decimal Amount;
			public decimal CommVat;
			public decimal NetAmnt;
		}
		public struct RecViewCustomerSummary
		{
			public long order_number;
			public string side;
			public string stock;
			public string trustee_id;
			public long volume;
			public string price;
			public decimal matched_value;
			public string status;
			public long deal_volume;
			public decimal deal_price;
		}
		private delegate void StartReloadTimerCallBack(int interval, bool isRequest);
		private delegate void ShowPrintDailogCallBack();
		private delegate void ReportClickCallBack(object sender, EventArgs e);
		private class VCItem
		{
			public double value;
			public double profit;
			[MethodImpl(MethodImplOptions.NoInlining)]
			public VCItem(double value, double profit)
			{
				this.value = value;
				this.profit = profit;
			}
		}
		private delegate void ShowPrintDailog_TFEX_CallBack();
		private const string PORTFOLIO = "Portfolio";
		private const string HOLDING_CHART_PAGE = "Holding Chart";
		private const string NAV_CHART_PAGE = "NAV Chart";
		private const string SUB_SUMMARY_PAGE = "Summary";
		private const string SUB_COMFIRM_BY_STOCK_PAGE = "Confrim by Stock";
		private const string SUB_COMFIRM_SUMM_PAGE = "Confrim Summary";
		private const string SUB_CREDIT_BALANCE_PAGE = "Credit Balance";
		private const string SUB_REALIZE_PAGE = "Realize Profit/Loss";
		private string _currAccount = string.Empty;
		private string _accType = string.Empty;
		private bool _isForceUpdate = true;
		private bool _isSupportCBReport = true;
		private decimal _tradingFee = 0m;
		private decimal _clearingFee = 0m;
		private decimal _settlementFee = 0m;
		private BackgroundWorker bgwLoadReport = null;
		private int reportPageNo = 1;
		private int currentTop = 99999;
		private ArrayList _textPrint = null;
		private PrintPreviewDialog _previewPrinter = null;
		private PrintDocument _printDocument = null;
		private PrintDialog _printDialog = null;
		private int _PAGEPREVIEW = 0;
		private DataSet tdsR1 = null;
		private DataSet tdsR3 = null;
		private DataSet tdsR8 = null;
		private DataSet tdsNAV = null;
		private DataSet tdsHoldChart = null;
		private DataSet tdsTfex = null;
		private decimal _totalTJAmount = 0m;
		private string _reportType = "Portfolio";
		private string _subReportType = "Portfolio";
		private Timer tmReload = null;
		private bool _isAreadyLoadNAV = false;
		private string _navStartDate = string.Empty;
		private Dictionary<decimal, long> summaryMaper = null;
		private string _headerMessage = string.Empty;
		private int linesPrinted = 0;
		private decimal _totUnReal_Pct;
		private decimal _totUnReal;
		private decimal _totRealize;
		private decimal _totCost;
		private decimal _totCurrValue;
		private int _mcPos;
		private decimal _buyLimit = 0m;
		private int _iCommType = 0;
		private decimal _tfexTotMarketVal;
		private decimal _tfexUnrealSettle;
		private decimal _tfexUnrealCost;
		private decimal _tfexReal;
		private decimal _tfexCost;
		private int linesPrinted_TFEX = 0;
		private int lastFocus = -1;
		private bool isEditing = false;
		private DataSet dataSetTJ = null;
		private IContainer components = null;
		private IntzaCustomGrid intzaCB;
		private ToolStrip tStripMain;
		private ToolStripButton btnRefresh;
		private ToolStripButton tsbtnPortfolio;
		private IntzaCustomGrid intzaCB_Freewill;
		private ToolStripButton tsbtnHoldingChart;
		private ucVolumeAtPrice wcGraphVolume;
		private ucVolumeAtPrice wcGraphVolumeSector;
		private ToolStripButton tsbtnNAV;
		private Panel panelNav;
		private ChartWinControl chart;
		private ToolStrip tStripMenu;
		private ToolStripLabel toolStripLabel1;
		private ToolStripLabel tstbStartDate;
		private ToolStripButton tsbtnSelStartDate;
		private ToolStripLabel toolStripLabel2;
		private ToolStripLabel tstbEndDate;
		private ToolStripButton tsbtnSelEndDate;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripButton tsbtnReload;
		private MonthCalendar monthCalendar1;
		private SortGrid intzaReport;
		private SortGrid intzaSumReport;
		private IntzaCustomGrid intzaInfoHeader;
		private Panel panelSET;
		private Panel panelTFEXPort;
		private SortGrid sortGridTfex;
		private IntzaCustomGrid intzaCustBottTfex;
		private IntzaCustomGrid intzaCustHeadTfex;
		private ToolStrip tStripMainT;
		private ToolStripLabel tslbAccountT;
		private ToolStripButton btnRefreshT;
		private ToolStripSeparator toolStripSeparator3;
		private ToolStripButton tsbtnPrintT;
		private SortGrid sortGridTfexSumm;
		private Panel panelReportMenu;
		private ToolStrip toolStrip1;
		private ToolStripButton tsbtnCreditBalance;
		private ToolStripButton tsbtnConfrimByStock;
		private ToolStripSeparator tssepStock;
		private ToolStripLabel tslbStock;
		private ToolStripTextBox tstbStock2;
		private ToolStripButton tsbtnClearStock;
		private ToolStripButton tsbtnSummary;
		private ToolStripButton tsbtnTotRealizeProfit;
		private ToolStripSeparator tssepPrint;
		private ToolStripButton tsbtnPrint;
		private ToolStripButton tsbtnConfrimSumm;
		private Panel pnlTradeJ;
		private IntzaCustomGrid intzaTradeOverview;
		private SortGrid sortGrid1;
		private ComboBox cbText;
		private ToolStripButton tsbtnTradeJournal;
		private ToolStripButton tsbtnPort;
		private ToolStrip tStripTJ;
		private ToolStripLabel tslbAmountText;
		private ToolStripSeparator toolStripSeparator2;
		private ToolStripTextBox tstxtAmount;
		private ToolStripButton tsbtnAmount;
		private ToolStripButton tsbtnExportCSV;
		public string CurrAccount
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this._currAccount;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this._currAccount = value;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmPortfolio()
		{
			this.InitializeComponent();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmPortfolio(Dictionary<string, object> propertiesValue) : base(propertiesValue)
		{
			this.InitializeComponent();
			try
			{
				this.tstbStock2.AutoCompleteCustomSource = ApplicationInfo.StockAutoComp;
				this.intzaCB.Height = 0;
				this.tssepStock.Visible = false;
				this.tslbStock.Visible = false;
				this.tstbStock2.Visible = false;
				this.tsbtnClearStock.Visible = false;
				this.tssepPrint.Visible = false;
				this.tsbtnPrint.Visible = false;
				this.tsbtnCreditBalance.Visible = false;
				if (ApplicationInfo.SupportFreewill)
				{
					this.tsbtnConfrimByStock.Visible = false;
				}
				decimal.TryParse(this.tstxtAmount.Text.Replace(",", ""), out this._totalTJAmount);
				this.calOverview();
				this.tsbtnTradeJournal.Visible = ApplicationInfo.IsSupportTradeJournal;
				this.tsbtnPort.ForeColor = Color.Orange;
				this.tsbtnTradeJournal.ForeColor = Color.WhiteSmoke;
				if (!ApplicationInfo.SupportNAV)
				{
					this.tsbtnNAV.Visible = false;
				}
				if (ApplicationInfo.SupportFreewill)
				{
					this.intzaInfoHeader.Item("lbHighLimit").Text = "";
					this.intzaInfoHeader.Item("lbCustomerFlag").Visible = false;
					this.intzaInfoHeader.Item("tbCustomerFlag").Visible = false;
					this.tssepPrint.Visible = false;
					this.tsbtnPrint.Visible = false;
				}
				this.bgwLoadReport = new BackgroundWorker();
				this.bgwLoadReport.WorkerReportsProgress = true;
				this.bgwLoadReport.DoWork += new DoWorkEventHandler(this.bgwLoadReport_DoWork);
				this.bgwLoadReport.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwLoadReport_RunWorkerCompleted);
				this.InitChart();
			}
			catch (Exception ex)
			{
				this.ShowError("frmPortfolio", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void InitChart()
		{
			try
			{
				this.chart.DefaultFormulas = "MAIN#Compare(NAV)";
				this.chart.AdjustData = true;
				this.chart.ContextMenu = null;
				this.chart.Chart.FixedTime = false;
				this.chart.Skin = "History";
				DataManagerBase dataManager = new DbDataManager(DataCycleBase.DAY);
				this.chart.DataManager = dataManager;
				this.chart.CurrentDataCycle = DataCycle.Day;
				this.chart.StockRenderType = StockRenderType.Line;
				this.chart.PriceLabelFormat = "{DD} SET:{CLOSE} {Change}";
			}
			catch (Exception ex)
			{
				this.ShowError("InitChart", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReceiveMessage(IBroadcastMessage message, StockList.StockInformation realtimeStockInfo)
		{
			if (this.FormState == ClientBaseForm.ClientBaseFormState.Showed)
			{
				string messageType = message.MessageType;
				if (messageType != null)
				{
					if (messageType == "0I")
					{
						OrderInfoClient orderInfoClient = (OrderInfoClient)message;
						if (orderInfoClient.Account == ApplicationInfo.AccInfo.CurrentAccount)
						{
							this.StartReloadTimer(1500, false);
						}
					}
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReceiveTfexMessage(IBroadcastMessage message, SeriesList.SeriesInformation realtimeSeriesInfo)
		{
			if (this.FormState == ClientBaseForm.ClientBaseFormState.Showed)
			{
				string messageType = message.MessageType;
				if (messageType != null)
				{
					if (messageType == "#T9I")
					{
						OrderTFEXInfoClient orderTFEXInfoClient = (OrderTFEXInfoClient)message;
						if (orderTFEXInfoClient.Account == ApplicationInfo.AccInfo.CurrentAccount)
						{
							this.StartReloadTimer(1500, false);
						}
					}
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnRefresh_Click(object sender, EventArgs e)
		{
			this.StartReloadTimer(200, true);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmPortfolio_IDoMainFormKeyUp(KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
			case Keys.Left:
			case Keys.Right:
				if (this._reportType == "NAV Chart")
				{
					this.chart.HandleKeyEvent(this, e);
				}
				break;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmPortfolio_IDoReActivated()
		{
			if (!base.IsLoadingData)
			{
				this.SetResize(this.IsWidthChanged | this.IsHeightChanged);
				base.Show();
				this.StartReloadTimer(200, true);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void StartReloadTimer(int interval, bool isRequest)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new frmPortfolio.StartReloadTimerCallBack(this.StartReloadTimer), new object[]
				{
					interval,
					isRequest
				});
			}
			else
			{
				try
				{
					this._isForceUpdate = isRequest;
					if (this.tmReload == null)
					{
						this.tmReload = new Timer();
						this.tmReload.Tick += new EventHandler(this.tmReload_Tick);
					}
					this.tmReload.Enabled = false;
					this.tmReload.Interval = interval;
					this.tmReload.Enabled = true;
				}
				catch (Exception ex)
				{
					this.ShowError("StartReloadTimer", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmPortfolio_IDoFontChanged()
		{
			if (!base.IsLoadingData)
			{
				this.SetResize(true);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tmReload_Tick(object sender, EventArgs e)
		{
			try
			{
				this.tmReload.Enabled = false;
				this._currAccount = ApplicationInfo.AccInfo.CurrentAccount;
				if (this._currAccount != string.Empty)
				{
					this.ReportClick(this._reportType, this._subReportType);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tmReload_Tick", ex);
			}
			base.IsLoadingData = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmPortfolio_IDoShownDelay()
		{
			this.setVisibleControl("Portfolio", "Summary");
			this.SetResize(true);
			base.Show();
			base.OpenedForm();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmPortfolio_IDoLoadData()
		{
			try
			{
				if (this._navStartDate != string.Empty)
				{
					this.tstbStartDate.Text = this._navStartDate;
				}
				else
				{
					this.tstbStartDate.Text = DateTime.Now.AddDays(-100.0).ToString("dd/MM/yyyy");
				}
				this.tstbEndDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
				this.StartReloadTimer(200, true);
			}
			catch (Exception ex)
			{
				this.ShowError("frmPortfolio_IDoLoadData", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetResize(bool isChanged)
		{
			if (isChanged)
			{
				if (ApplicationInfo.IsEquityAccount)
				{
					this.panelSET.SetBounds(0, 0, base.Width, base.Height);
					if (this.intzaInfoHeader.Font != Settings.Default.Default_Font)
					{
						this.intzaInfoHeader.Font = Settings.Default.Default_Font;
					}
					this.intzaInfoHeader.SetBounds(0, this.tStripMain.Bottom, base.Width, this.intzaInfoHeader.GetHeightByRows() + 3);
					int num = this.intzaInfoHeader.Bottom;
					if (this._reportType == "Portfolio")
					{
						this.panelReportMenu.SetBounds(0, num + 1, base.Width, this.tStripMain.Height + 1);
						if (this._subReportType == "Summary" || this._subReportType == "Confrim by Stock" || this._subReportType == "Realize Profit/Loss" || this._subReportType == "Confrim Summary")
						{
							if (this._subReportType == "Summary")
							{
								this.intzaSumReport.Height = this.intzaSumReport.GetHeightByRows(1);
								this.intzaReport.SetBounds(0, this.panelReportMenu.Bottom + 1, base.Width, base.Height - (this.intzaSumReport.Height + 1) - (this.panelReportMenu.Bottom + 1) - 1);
								this.intzaSumReport.SetBounds(0, this.intzaReport.Bottom + 1, this.intzaReport.Width, this.intzaSumReport.Height);
							}
							else
							{
								this.intzaReport.SetBounds(0, num + this.panelReportMenu.Height + 1, base.Width, base.Height - (num + this.panelReportMenu.Height + 1) - 1);
							}
							if (this.intzaReport.Font != Settings.Default.Default_Font)
							{
								this.intzaReport.Font = new Font(Settings.Default.Default_Font.Name, Settings.Default.Default_Font.Size - 1f);
								this.intzaSumReport.Font = this.intzaReport.Font;
							}
						}
						else
						{
							if (this._subReportType == "Credit Balance")
							{
								if (ApplicationInfo.SupportFreewill)
								{
									this.intzaCB_Freewill.Font = Settings.Default.Default_Font;
									this.intzaCB_Freewill.SetBounds(0, this.panelReportMenu.Bottom + 1, this.intzaInfoHeader.Width, base.ClientSize.Height - this.panelReportMenu.Bottom - 1);
								}
								else
								{
									this.intzaCB.Font = Settings.Default.Default_Font;
									this.intzaCB.Top = this.panelReportMenu.Bottom + 1;
									this.intzaCB.Width = this.intzaInfoHeader.Width;
									this.intzaCB.Height = base.Height - (this.panelReportMenu.Bottom + 1) - 1;
								}
							}
						}
					}
					else
					{
						if (this._reportType == "Holding Chart")
						{
							int num2 = base.Height - num - 2;
							this.wcGraphVolume.SetBounds(0, num + 1, base.Width, num2 / 2);
							this.wcGraphVolumeSector.SetBounds(this.wcGraphVolume.Left, this.wcGraphVolume.Bottom, this.wcGraphVolume.Width, this.wcGraphVolume.Height);
						}
						else
						{
							if (this._reportType == "NAV Chart")
							{
								this.panelNav.SetBounds(0, num + 1, base.Width, base.Height - num - 2);
							}
						}
					}
				}
				else
				{
					this.panelTFEXPort.SetBounds(0, 0, base.Width, base.Height);
					if (this.sortGridTfex.Font.Size != Settings.Default.Default_Font.Size - 1f)
					{
						this.sortGridTfex.Font = new Font(Settings.Default.Default_Font.Name, Settings.Default.Default_Font.Size - 1f);
						this.sortGridTfexSumm.Font = this.sortGridTfex.Font;
						this.intzaCustBottTfex.Font = this.sortGridTfex.Font;
					}
					int num = this.intzaCustHeadTfex.Top + this.intzaCustHeadTfex.Height;
					this.intzaCustHeadTfex.SetBounds(0, this.tStripMainT.Height + 1, base.Width, this.intzaCustHeadTfex.GetHeightByRows());
					this.intzaCustBottTfex.SetBounds(0, this.intzaCustHeadTfex.Bottom + 1, this.intzaCustHeadTfex.Width, this.intzaCustBottTfex.GetHeightByRows());
					this.sortGridTfexSumm.SetBounds(0, this.panelTFEXPort.Height - this.sortGridTfexSumm.GetHeightByRows(1), this.panelTFEXPort.Width, this.sortGridTfexSumm.GetHeightByRows(1));
					this.sortGridTfex.SetBounds(0, this.intzaCustBottTfex.Bottom + 1, base.Width, base.Height - (this.intzaCustBottTfex.Bottom + 1) - this.sortGridTfexSumm.Height - 1);
					this.pnlTradeJ.SetBounds(0, this.tStripMainT.Height + 1, base.Width, base.Height - this.tStripMainT.Height);
					if (this.sortGrid1.Font.Size != Settings.Default.Default_Font.Size - 1f)
					{
						this.sortGrid1.Font = new Font(Settings.Default.Default_Font.Name, Settings.Default.Default_Font.Size - 1f);
						this.sortGrid1.Font = this.sortGridTfex.Font;
						this.intzaTradeOverview.Font = this.sortGrid1.Font;
					}
					this.intzaTradeOverview.SetBounds(0, 0, base.Width, this.intzaTradeOverview.GetHeightByRows());
					this.sortGrid1.SetBounds(0, this.intzaTradeOverview.Bottom + 1, base.Width, this.pnlTradeJ.Height - (this.intzaTradeOverview.Bottom + 1) - this.tStripTJ.Height - 1);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnRefreshReport_Click(object sender, EventArgs e)
		{
			this.SetReportPage();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetReportPage()
		{
			if (!this.bgwLoadReport.IsBusy)
			{
				if (ApplicationInfo.IsEquityAccount)
				{
					this.reportPageNo = 1;
					if (this._reportType == "NAV Chart" && this._isAreadyLoadNAV)
					{
						return;
					}
				}
				this.bgwLoadReport.RunWorkerAsync();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override Dictionary<string, object> DoPackProperties()
		{
			try
			{
				base.PropertiesValue.Clear();
				base.PropertiesValue.Add("NAVStartDate", this.tstbStartDate.Text);
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
					if (base.PropertiesValue.ContainsKey("NAVStartDate"))
					{
						this._navStartDate = base.PropertiesValue["NAVStartDate"].ToString();
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("MarketWatch.DoUnpackProperties", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmPortfolio_IDoCustomSizeChanged()
		{
			if (!base.IsLoadingData)
			{
				this.SetResize(this.IsHeightChanged | this.IsWidthChanged);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void CustomerSummarySideGroup(DataRow item)
		{
			try
			{
				bool flag = false;
				foreach (KeyValuePair<decimal, long> current in this.summaryMaper)
				{
					decimal d;
					decimal.TryParse(item["deal_price"].ToString(), out d);
					if (current.Key == d)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					foreach (KeyValuePair<decimal, long> current in this.summaryMaper)
					{
						decimal d2;
						decimal.TryParse(item["deal_price"].ToString(), out d2);
						if (current.Key == d2)
						{
							long num;
							long.TryParse(item["deal_volume"].ToString(), out num);
							decimal num2;
							decimal.TryParse(item["deal_price"].ToString(), out num2);
							long value = current.Value + num;
							if (this.summaryMaper.Count > 0)
							{
								this.summaryMaper.Remove(num2);
							}
							this.summaryMaper.Add(num2, value);
							break;
						}
					}
				}
				else
				{
					if (item["deal_price"] != null)
					{
						long num;
						long.TryParse(item["deal_volume"].ToString(), out num);
						decimal num2;
						decimal.TryParse(item["deal_price"].ToString(), out num2);
						if (num2 > 0m)
						{
							this.summaryMaper.Add(num2, num);
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SummarySideGroup", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string GetStockRep(string stock, string trusteeId, string stockType)
		{
			return (stock.Trim().Length > 8) ? (stock.Substring(0, 6) + "..") : (stock.Trim() + ((trusteeId == "0") ? string.Empty : ("(" + trusteeId + ")")) + ((stockType.Trim() == "") ? string.Empty : ("(" + stockType + ")")));
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void CreateHeaderReport()
		{
			try
			{
				string headerMessage = string.Empty;
				headerMessage = this.intzaReport.Columns[0].Text;
				this._headerMessage = headerMessage;
			}
			catch (Exception ex)
			{
				this.ShowError("CreateHeaderReport", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string GetString(string value, int maxLen, StringAlignment alignment)
		{
			string result;
			if (value.Length < maxLen)
			{
				if (alignment == StringAlignment.Near)
				{
					result = value.PadRight(maxLen, ' ');
				}
				else
				{
					result = value.PadLeft(maxLen, ' ');
				}
			}
			else
			{
				result = value;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnPrint_Click(object sender, EventArgs e)
		{
			base.BeginInvoke(new frmPortfolio.ShowPrintDailogCallBack(this.ShowPrintDailog));
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowPrintDailog()
		{
			try
			{
				if (this._textPrint == null)
				{
					this._textPrint = new ArrayList();
				}
				else
				{
					this._textPrint.Clear();
				}
				string text = string.Empty;
				SortGrid sortGrid = this.intzaReport;
				foreach (ColumnItem current in sortGrid.Columns)
				{
					text += ((current.Alignment == StringAlignment.Near) ? current.Text.PadRight(current.Width, ' ') : current.Text.PadLeft(current.Width, ' '));
				}
				this.SetDetailHeaderText(text);
				string text2 = string.Empty;
				string text3 = string.Empty;
				for (int i = 0; i < sortGrid.Rows; i++)
				{
					foreach (ColumnItem current in sortGrid.Columns)
					{
						text3 = sortGrid.Records(i).Fields(current.Name).Text.ToString();
						if (current.ValueFormat == FormatType.Symbol && !string.IsNullOrEmpty(sortGrid.Records(i).Fields(current.Name).Tag))
						{
							text3 = text3 + " (" + sortGrid.Records(i).Fields(current.Name).Tag + ")";
						}
						text2 += ((current.Alignment == StringAlignment.Near) ? text3.PadRight(current.Width, ' ') : text3.PadLeft(current.Width, ' '));
					}
					this.AddPrintText(text2);
					text2 = string.Empty;
				}
				string empty = string.Empty;
				this._PAGEPREVIEW = 0;
				this._previewPrinter = new PrintPreviewDialog();
				this._printDocument = new PrintDocument();
				this._printDialog = new PrintDialog();
				this._printDocument.BeginPrint += new PrintEventHandler(this.OnBeginPrint);
				this._printDocument.PrintPage += new PrintPageEventHandler(this.OnPrintPage);
				this._printDocument.DocumentName = this._reportType.Substring(2);
				this._printDialog.Document = this._printDocument;
				this._previewPrinter.Document = this._printDocument;
				this._previewPrinter.ShowDialog();
			}
			catch (Exception ex)
			{
				this.ShowError("ShowPrinter", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnBeginPrint(object sender, PrintEventArgs e)
		{
			if (e.PrintAction == PrintAction.PrintToPrinter)
			{
				if (this._printDialog.ShowDialog() == DialogResult.Cancel)
				{
					e.Cancel = true;
				}
				this._PAGEPREVIEW = 0;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnPrintPage(object sender, PrintPageEventArgs e)
		{
			try
			{
				int num = e.MarginBounds.Left - 30;
				int num2 = e.MarginBounds.Top - 30;
				Brush brush = new SolidBrush(Color.Black);
				this._PAGEPREVIEW++;
				e.Graphics.DrawString("#Page " + this._PAGEPREVIEW, new Font("Courier New", 8f, FontStyle.Italic), Brushes.Black, (float)e.MarginBounds.Right, (float)(e.MarginBounds.Bottom + 50));
				while (this.linesPrinted < this._textPrint.Count)
				{
					e.Graphics.DrawString(this._textPrint[this.linesPrinted++].ToString(), new Font("Courier New", 8f, FontStyle.Regular), brush, (float)num, (float)num2);
					num2 += 15;
					if (num2 >= e.MarginBounds.Bottom && this.linesPrinted < this._textPrint.Count)
					{
						e.HasMorePages = true;
						return;
					}
				}
				this.linesPrinted = 0;
				e.HasMorePages = false;
			}
			catch (Exception ex)
			{
				this.ShowError("OnPrintPage", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetDetailHeaderText(string columnsHeaderText)
		{
			try
			{
				if (this._accType == "B" || this._accType == "T")
				{
					this.AddPrintText_TFEX("                            " + ApplicationInfo.GetFullNameBroker(ApplicationInfo.BrokerId));
					this.AddPrintText(DateTime.Now.ToString("MMM dd,yyyy hh:mm:ss"));
					this.AddPrintText("Customer  :  " + this._currAccount + "         NAME        : " + this.intzaInfoHeader.Item("tbCustName").Text);
					this.AddPrintText(string.Concat(new string[]
					{
						"Cust Type :  ",
						this.intzaInfoHeader.Item("tbCustomerType").Text,
						"              ",
						"Acc. Type  : ",
						this.intzaInfoHeader.Item("tbAccountType").Text,
						"   Credit Type : ",
						this.intzaInfoHeader.Item("tbCreditType").Text,
						ApplicationInfo.SupportFreewill ? "" : "  Flag : ",
						this.intzaInfoHeader.Item("tbCustomerFlag").Text
					}));
					if (ApplicationInfo.SupportFreewill)
					{
						this.AddPrintText(string.Concat(new string[]
						{
							"EE :  ",
							this.intzaInfoHeader.Item("tbBuyLimit").Text,
							"  PP. :  ",
							this.intzaInfoHeader.Item("tbHighLimit").Text,
							"  Credit Line : ",
							this.intzaInfoHeader.Item("tbCreditLine").Text,
							"  Equity : ",
							this.intzaInfoHeader.Item("tbEquity").Text
						}));
					}
					else
					{
						this.AddPrintText(string.Concat(new string[]
						{
							"Excess Equity : ",
							this.intzaCB.Item("tbExcessEquityCurrent").Text,
							"  Previous EE. : ",
							this.intzaCB.Item("tbExcessEquityPrevious").Text,
							"  Credit Line : ",
							this.intzaInfoHeader.Item("tbCreditLine").Text,
							"  Equity : ",
							this.intzaInfoHeader.Item("tbEquity").Text
						}));
					}
					if (!ApplicationInfo.SupportFreewill)
					{
						this.AddPrintText("=================================================================================================");
						this.AddPrintText(this.GetString("PREVIOUS", 25, StringAlignment.Near) + this.GetString("CURRENT", 50, StringAlignment.Near) + "\r\n");
						this.AddPrintText("=================================================================================================");
						this.AddPrintText(this.GetString("Excess Equity : ", 15, StringAlignment.Near) + this.GetString(this.intzaCB.Item("tbExcessEquityPrevious").Text, 35, StringAlignment.Near) + this.GetString("Excess Equity : ", 15, StringAlignment.Near) + this.GetString(this.intzaCB.Item("tbExcessEquityCurrent").Text, 30, StringAlignment.Near));
						this.AddPrintText(string.Concat(new string[]
						{
							this.GetString(" ", 51, StringAlignment.Near),
							this.GetString("MM : ", 1, StringAlignment.Near),
							this.GetString(this.intzaCB.Item("tbMMpercentCurrent").Text, 15, StringAlignment.Near),
							this.GetString("Mark EE. : ", 10, StringAlignment.Near),
							this.GetString(this.intzaCB.Item("tbMarkToEECurrent").Text, 5, StringAlignment.Near)
						}));
						this.AddPrintText(this.GetString("Equity : ", 15, StringAlignment.Near) + this.GetString(this.intzaCB.Item("tbEquityPrevious").Text, 36, StringAlignment.Near) + this.GetString("Equity : ", 15, StringAlignment.Near) + this.GetString(this.intzaCB.Item("tbEquityCurrent").Text, 30, StringAlignment.Near));
						this.AddPrintText(this.GetString("MR : ", 15, StringAlignment.Near) + this.GetString(this.intzaCB.Item("tbMRPrevious").Text, 36, StringAlignment.Near) + this.GetString("MR : ", 15, StringAlignment.Near) + this.GetString(this.intzaCB.Item("tbMRCurrent").Text, 30, StringAlignment.Near));
						this.AddPrintText(this.GetString("Asset:", 51, StringAlignment.Near) + this.GetString("Asset:", 34, StringAlignment.Near));
						this.AddPrintText(string.Concat(new string[]
						{
							this.GetString(" ", 5, StringAlignment.Near),
							this.GetString("Cash Bal.  : ", 1, StringAlignment.Near),
							this.GetString(this.intzaCB.Item("tbCashBalancePrevious").Text, 15, StringAlignment.Near),
							this.GetString(" ", 18, StringAlignment.Near),
							this.GetString("Cash Bal.  : ", 1, StringAlignment.Near),
							this.GetString(this.intzaCB.Item("tbCashBalanceCurrent").Text, 15, StringAlignment.Near)
						}));
						this.AddPrintText(string.Concat(new string[]
						{
							this.GetString(" ", 5, StringAlignment.Near),
							this.GetString("LMV", 11, StringAlignment.Near),
							this.GetString(":", 2, StringAlignment.Near),
							this.GetString(this.intzaCB.Item("tbLmvPrevious").Text, 15, StringAlignment.Near),
							this.GetString(" ", 18, StringAlignment.Near),
							this.GetString("LMV", 11, StringAlignment.Near),
							this.GetString(":", 2, StringAlignment.Near),
							this.GetString(this.intzaCB.Item("tbLmvCurrent").Text, 15, StringAlignment.Near)
						}));
						this.AddPrintText(string.Concat(new string[]
						{
							this.GetString(" ", 5, StringAlignment.Near),
							this.GetString("Collateral : ", 10, StringAlignment.Near),
							this.GetString(this.intzaCB.Item("tbColleteralPrevious").Text, 15, StringAlignment.Near),
							this.GetString(" ", 18, StringAlignment.Near),
							this.GetString("Collateral : ", 10, StringAlignment.Near),
							this.GetString(this.intzaCB.Item("tbColleteralCurrent").Text, 15, StringAlignment.Near)
						}));
						this.AddPrintText(this.GetString("Liabilites:", 51, StringAlignment.Near) + this.GetString("Liabilites:", 34, StringAlignment.Near));
						this.AddPrintText(string.Concat(new string[]
						{
							this.GetString(" ", 5, StringAlignment.Near),
							this.GetString("Loan", 11, StringAlignment.Near),
							this.GetString(":", 2, StringAlignment.Near),
							this.GetString(this.intzaCB.Item("tbLoanPrevious").Text, 15, StringAlignment.Near),
							this.GetString(" ", 18, StringAlignment.Near),
							this.GetString("Loan", 11, StringAlignment.Near),
							this.GetString(":", 2, StringAlignment.Near),
							this.GetString(this.intzaCB.Item("tbLoanCurrent").Text, 15, StringAlignment.Near)
						}));
						this.AddPrintText(string.Concat(new string[]
						{
							this.GetString(" ", 5, StringAlignment.Near),
							this.GetString("SMV", 11, StringAlignment.Near),
							this.GetString(":", 2, StringAlignment.Near),
							this.GetString(this.intzaCB.Item("tbSmvPrevious").Text, 15, StringAlignment.Near),
							this.GetString(" ", 18, StringAlignment.Near),
							this.GetString("SMV", 11, StringAlignment.Near),
							this.GetString(":", 2, StringAlignment.Near),
							this.GetString(this.intzaCB.Item("tbSmvCurrent").Text, 15, StringAlignment.Near)
						}));
						this.AddPrintText(string.Concat(new string[]
						{
							this.GetString("Call : ", 1, StringAlignment.Near),
							this.GetString(this.intzaCB.Item("tbCallPrevious").Text, 25, StringAlignment.Near),
							this.GetString(" ", 19, StringAlignment.Near),
							this.GetString("Call : ", 1, StringAlignment.Near),
							this.GetString(this.intzaCB.Item("tbCallCurrent").Text, 25, StringAlignment.Near)
						}));
						this.AddPrintText(string.Concat(new string[]
						{
							this.GetString("Force: ", 1, StringAlignment.Near),
							this.GetString(this.intzaCB.Item("tbForcePrevious").Text, 25, StringAlignment.Near),
							this.GetString(" ", 19, StringAlignment.Near),
							this.GetString("Force: ", 1, StringAlignment.Near),
							this.GetString(this.intzaCB.Item("tbForceCurrent").Text, 25, StringAlignment.Near)
						}));
					}
				}
				else
				{
					this.AddPrintText_TFEX("                            " + ApplicationInfo.GetFullNameBroker(ApplicationInfo.BrokerId));
					this.AddPrintText(DateTime.Now.ToString("MMM dd,yyyy hh:mm:ss"));
					this.AddPrintText("Customer  :  " + this._currAccount + "         NAME        : " + this.intzaInfoHeader.Item("tbCustName").Text);
					this.AddPrintText(string.Concat(new string[]
					{
						"Cust Type :  ",
						this.intzaInfoHeader.Item("tbCustomerType").Text,
						"              ",
						"Credit Type : ",
						this.intzaInfoHeader.Item("tbCreditType").Text,
						"  Acc. Type  :   ",
						this._accType,
						ApplicationInfo.SupportFreewill ? "" : ("  Flag : " + this.intzaInfoHeader.Item("tbCustomerFlag").Text)
					}));
					this.AddPrintText(string.Concat(new string[]
					{
						"Buy Limit :  ",
						this.intzaInfoHeader.Item("tbBuyLimit").Text,
						"  Credit Line : ",
						this.intzaInfoHeader.Item("tbCreditLine").Text,
						"  Equity : ",
						this.intzaInfoHeader.Item("tbEquity").Text
					}));
				}
				this.AddPrintText("========================================================================================================");
				this.AddPrintText(columnsHeaderText + "\r\n");
				this.AddPrintText("========================================================================================================");
			}
			catch (Exception ex)
			{
				this.ShowError("SetDetailHeaderText", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void AddPrintText(string content)
		{
			try
			{
				this._textPrint.Add(content);
			}
			catch (Exception ex)
			{
				this.ShowError("AddPrintText", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void AddPrintText()
		{
			try
			{
				string content = string.Empty;
				for (int i = 0; i < this.intzaReport.Rows; i++)
				{
					content = this.intzaReport.Records(i).Fields("col1").Text.ToString();
					this.AddPrintText(content);
					content = string.Empty;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("AddPrintText", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void controlOrder_Enter(object sender, EventArgs e)
		{
			if (sender.GetType() == typeof(ToolStripComboBox))
			{
				((ToolStripComboBox)sender).ForeColor = Color.Black;
				((ToolStripComboBox)sender).BackColor = Color.Yellow;
				((ToolStripComboBox)sender).SelectAll();
			}
			else
			{
				if (sender.GetType() == typeof(ToolStripTextBox))
				{
					((ToolStripTextBox)sender).ForeColor = Color.Black;
					((ToolStripTextBox)sender).BackColor = Color.Yellow;
					((ToolStripTextBox)sender).SelectAll();
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void controlOrder_Leave(object sender, EventArgs e)
		{
			if (sender.GetType() == typeof(ToolStripComboBox))
			{
				((ToolStripComboBox)sender).ForeColor = Color.Yellow;
				((ToolStripComboBox)sender).BackColor = Color.FromArgb(60, 60, 60);
				((ToolStripComboBox)sender).SelectAll();
			}
			else
			{
				if (sender.GetType() == typeof(ToolStripTextBox))
				{
					((ToolStripTextBox)sender).ForeColor = Color.Yellow;
					((ToolStripTextBox)sender).BackColor = Color.FromArgb(60, 60, 60);
					((ToolStripTextBox)sender).SelectAll();
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void setVisibleControl(string reportType, string subReportType)
		{
			if (ApplicationInfo.IsEquityAccount)
			{
				this.tsbtnHoldingChart.ForeColor = Color.WhiteSmoke;
				this.tsbtnNAV.ForeColor = Color.WhiteSmoke;
				this.tsbtnPortfolio.ForeColor = Color.WhiteSmoke;
				this.tsbtnSummary.ForeColor = Color.WhiteSmoke;
				this.tsbtnConfrimByStock.ForeColor = Color.WhiteSmoke;
				this.tsbtnCreditBalance.ForeColor = Color.WhiteSmoke;
				this.tsbtnTotRealizeProfit.ForeColor = Color.WhiteSmoke;
				this.tsbtnConfrimSumm.ForeColor = Color.WhiteSmoke;
				this._reportType = string.Empty;
				bool visible = false;
				if (reportType == "Holding Chart")
				{
					this.tsbtnHoldingChart.ForeColor = Color.Orange;
					this._reportType = "Holding Chart";
					this.wcGraphVolume.Visible = true;
					this.wcGraphVolumeSector.Visible = true;
				}
				else
				{
					if (reportType == "NAV Chart")
					{
						this.tsbtnNAV.ForeColor = Color.Orange;
						this._reportType = "NAV Chart";
						this.panelNav.Visible = true;
					}
					else
					{
						if (reportType == "Portfolio")
						{
							this.tsbtnPortfolio.ForeColor = Color.Orange;
							visible = true;
							this._reportType = "Portfolio";
							if (subReportType == "Credit Balance")
							{
								this._subReportType = "Credit Balance";
								this.intzaSumReport.Visible = false;
								this.intzaReport.Visible = false;
								if (ApplicationInfo.SupportFreewill)
								{
									this.intzaCB_Freewill.Visible = true;
									this.intzaCB_Freewill.BringToFront();
								}
								else
								{
									this.intzaCB.Visible = true;
									this.intzaCB.BringToFront();
								}
								this.tsbtnCreditBalance.ForeColor = Color.Orange;
							}
							else
							{
								if (ApplicationInfo.SupportFreewill)
								{
									this.intzaCB_Freewill.Visible = false;
								}
								else
								{
									this.intzaCB.Visible = false;
								}
								if (subReportType == "Confrim by Stock")
								{
									this._subReportType = "Confrim by Stock";
									this.intzaSumReport.Visible = false;
									this.panelReportMenu.Visible = true;
									this.tsbtnConfrimByStock.ForeColor = Color.Orange;
								}
								else
								{
									if (subReportType == "Confrim Summary")
									{
										this._subReportType = "Confrim Summary";
										this.intzaSumReport.Visible = false;
										this.panelReportMenu.Visible = true;
										this.tsbtnConfrimSumm.ForeColor = Color.Orange;
									}
									else
									{
										if (subReportType == "Realize Profit/Loss")
										{
											this._subReportType = "Realize Profit/Loss";
											this.intzaSumReport.Visible = false;
											this.panelReportMenu.Visible = true;
											this.tsbtnTotRealizeProfit.ForeColor = Color.Orange;
										}
										else
										{
											this._subReportType = "Summary";
											this.intzaSumReport.Visible = true;
											this.intzaSumReport.BringToFront();
											this.tsbtnSummary.ForeColor = Color.Orange;
										}
									}
								}
								this.intzaReport.Visible = true;
								this.intzaReport.BringToFront();
							}
							this.panelReportMenu.Visible = true;
						}
					}
				}
				if (this._reportType != "Holding Chart")
				{
					this.wcGraphVolume.Visible = false;
					this.wcGraphVolumeSector.Visible = false;
				}
				if (this._reportType != "Portfolio")
				{
					this.intzaReport.Visible = false;
					this.intzaReport.ClearAllText();
					this.intzaSumReport.Visible = false;
					this.intzaSumReport.ClearAllText();
					this.panelReportMenu.Visible = false;
					this.intzaCB.Visible = false;
					this.intzaCB_Freewill.Visible = false;
				}
				if (this._reportType != "NAV Chart")
				{
					this.panelNav.Visible = false;
				}
				this.panelSET.Visible = true;
				this.panelTFEXPort.Visible = false;
				this.intzaInfoHeader.EndUpdate();
				this.tssepStock.Visible = visible;
				this.tslbStock.Visible = visible;
				this.tstbStock2.Visible = visible;
				this.tsbtnClearStock.Visible = visible;
				this.tssepPrint.Visible = visible;
				this.tsbtnPrint.Visible = visible;
			}
			else
			{
				this.panelTFEXPort.Visible = true;
				this.panelSET.Visible = false;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ReportClick(object sender, EventArgs e)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new frmPortfolio.ReportClickCallBack(this.ReportClick), new object[]
				{
					sender,
					e
				});
			}
			else
			{
				if (sender == this.tsbtnPortfolio)
				{
					this.ReportClick("Portfolio", (this._subReportType != string.Empty) ? this._subReportType : "Summary");
				}
				else
				{
					if (sender == this.tsbtnNAV)
					{
						this.ReportClick("NAV Chart", null);
					}
					else
					{
						if (sender == this.tsbtnHoldingChart)
						{
							this.ReportClick("Holding Chart", null);
						}
						else
						{
							if (sender == this.tsbtnSummary)
							{
								this.ReportClick("Portfolio", "Summary");
							}
							else
							{
								if (sender == this.tsbtnCreditBalance)
								{
									this.ReportClick("Portfolio", "Credit Balance");
								}
								else
								{
									if (sender == this.tsbtnConfrimByStock)
									{
										this.ReportClick("Portfolio", "Confrim by Stock");
									}
									else
									{
										if (sender == this.tsbtnTotRealizeProfit)
										{
											this.ReportClick("Portfolio", "Realize Profit/Loss");
										}
										else
										{
											if (sender == this.tsbtnConfrimSumm)
											{
												this.ReportClick("Portfolio", "Confrim Summary");
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
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ReportClick(string reportType, string subReportType)
		{
			this.setVisibleControl(reportType, subReportType);
			this.SetResize(true);
			this.SetReportPage();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tstbStock2_KeyUp(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode == Keys.Return)
			{
				string text = this.tstbStock2.Text.Trim();
				if (text == string.Empty)
				{
					this.SetReportPage();
				}
				else
				{
					StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[text];
					if (stockInformation.Number > -1)
					{
						this.tstbStock2.Text = stockInformation.Symbol;
						this.tstbStock2.SelectAll();
						this.SetReportPage();
					}
					else
					{
						this.tstbStock2.SelectAll();
					}
				}
				e.SuppressKeyPress = true;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnPrint_Click(object sender, EventArgs e)
		{
			base.BeginInvoke(new frmPortfolio.ShowPrintDailogCallBack(this.ShowPrintDailog));
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnClearStock_Click(object sender, EventArgs e)
		{
			this.tstbStock2.Text = string.Empty;
			this.SetReportPage();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwLoadReport_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				base.IsLoadingData = true;
				string text = this.tstbStock2.Text;
				string text2 = string.Empty;
				if (ApplicationInfo.IsEquityAccount)
				{
					if (this._reportType == "Portfolio")
					{
						if (this._subReportType == "Summary" || this._subReportType == "Credit Balance")
						{
							this._totUnReal_Pct = 0m;
							this._totUnReal = 0m;
							this._totRealize = 0m;
							this._totCost = 0m;
							this._totCurrValue = 0m;
							text2 = ApplicationInfo.WebOrderService.ViewCustomer_MobileReportAll(this._currAccount, this.tstbStock2.Text.Trim());
							if (!string.IsNullOrEmpty(text2))
							{
								if (this.tdsR1 == null)
								{
									this.tdsR1 = new DataSet();
								}
								else
								{
									this.tdsR1.Clear();
								}
								MyDataHelper.StringToDataSet(text2, this.tdsR1);
							}
						}
						else
						{
							if (this._subReportType == "Confrim by Stock" || this._subReportType == "Confrim Summary")
							{
								int startRow = (this.reportPageNo - 1) * this.currentTop + 1;
								if (this._subReportType == "Confrim by Stock")
								{
									text2 = ApplicationInfo.WebOrderService.ViewCustomer_ConfirmByStock(this._currAccount, this._iCommType, text, startRow, this.currentTop);
								}
								else
								{
									text2 = ApplicationInfo.WebOrderService.ViewCustomer_ConfirmSummary(this._currAccount, this._iCommType, text, startRow, this.currentTop);
								}
								if (!string.IsNullOrEmpty(text2))
								{
									if (this.tdsR8 == null)
									{
										this.tdsR8 = new DataSet();
									}
									else
									{
										this.tdsR8.Clear();
									}
									MyDataHelper.StringToDataSet(text2, this.tdsR8);
								}
								text2 = ApplicationInfo.WebOrderService.ViewCustomersInfo(this._currAccount, "");
								if (!string.IsNullOrEmpty(text2))
								{
									DataSet dataSet = new DataSet();
									MyDataHelper.StringToDataSet(text2, dataSet);
									if (this.tdsR8 != null)
									{
										this.tdsR8.Merge(dataSet);
									}
								}
							}
							else
							{
								if (this._subReportType == "Realize Profit/Loss")
								{
									int startRow = (this.reportPageNo - 1) * this.currentTop + 1;
									text2 = ApplicationInfo.WebOrderService.ViewCustomer_RealizeProfitLoss(this._currAccount, text, startRow, this.currentTop);
									if (!string.IsNullOrEmpty(text2))
									{
										if (this.tdsR3 == null)
										{
											this.tdsR3 = new DataSet();
										}
										else
										{
											this.tdsR3.Clear();
										}
										MyDataHelper.StringToDataSet(text2, this.tdsR3);
									}
									text2 = ApplicationInfo.WebOrderService.ViewCustomersInfo(this._currAccount, "");
									if (!string.IsNullOrEmpty(text2))
									{
										DataSet dataSet = new DataSet();
										MyDataHelper.StringToDataSet(text2, dataSet);
										if (this.tdsR3 != null)
										{
											this.tdsR3.Merge(dataSet);
										}
									}
								}
							}
						}
					}
					else
					{
						if (this._reportType == "Holding Chart")
						{
							int startRow = (this.reportPageNo - 1) * this.currentTop + 1;
							text2 = ApplicationInfo.WebOrderService.ViewCustomer_CreditPosition(this._currAccount, "", startRow, this.currentTop);
							if (!string.IsNullOrEmpty(text2))
							{
								if (this.tdsHoldChart == null)
								{
									this.tdsHoldChart = new DataSet();
								}
								else
								{
									this.tdsHoldChart.Clear();
								}
								MyDataHelper.StringToDataSet(text2, this.tdsHoldChart);
							}
						}
						else
						{
							if (this._reportType == "NAV Chart")
							{
								int num = Convert.ToInt32(this.tstbStartDate.Text.Substring(0, 2));
								int num2 = Convert.ToInt32(this.tstbStartDate.Text.Substring(3, 2));
								int num3 = Convert.ToInt32(this.tstbStartDate.Text.Substring(6, 4));
								if (num3 > 2500)
								{
									num3 -= 543;
								}
								string startDate = num3 + num2.ToString().PadLeft(2, '0') + num.ToString().PadLeft(2, '0');
								num = Convert.ToInt32(this.tstbEndDate.Text.Substring(0, 2));
								num2 = Convert.ToInt32(this.tstbEndDate.Text.Substring(3, 2));
								num3 = Convert.ToInt32(this.tstbEndDate.Text.Substring(6, 4));
								if (num3 > 2500)
								{
									num3 -= 543;
								}
								string endDate = num3 + num2.ToString().PadLeft(2, '0') + num.ToString().PadLeft(2, '0');
								text2 = ApplicationInfo.WebAlertService.NAVChart(this._currAccount, startDate, endDate);
								if (!string.IsNullOrEmpty(text2))
								{
									if (this.tdsNAV == null)
									{
										this.tdsNAV = new DataSet();
									}
									else
									{
										this.tdsNAV.Clear();
									}
									MyDataHelper.StringToDataSet(text2, this.tdsNAV);
								}
							}
						}
					}
				}
				else
				{
					text2 = ApplicationInfo.WebServiceTFEX.ViewCustomersAll(this._currAccount);
					if (!string.IsNullOrEmpty(text2))
					{
						if (this.tdsTfex == null)
						{
							this.tdsTfex = new DataSet();
						}
						else
						{
							this.tdsTfex.Clear();
						}
						MyDataHelper.StringToDataSet(text2, this.tdsTfex);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("bgwLoadReport_DoWork", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwLoadReport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				if (e.Error == null)
				{
					if (ApplicationInfo.IsEquityAccount)
					{
						if (this._reportType == "Holding Chart")
						{
							this.UpdateToVolumeChart(this.tdsHoldChart);
							this.tdsHoldChart.Clear();
						}
						else
						{
							if (this._reportType == "NAV Chart")
							{
								this.chart.Symbol = "SET";
								((DbDataManager)this.chart.HistoryDataManager).Tds = this.tdsNAV;
								this.chart.BindData_History();
								this.chart.NeedRebind();
								this.chart.ShowCrossCursor = true;
								this.chart.SetCursorByPosition(this.chart.GetLastDataIndex());
								this._isAreadyLoadNAV = true;
							}
							else
							{
								if (this._subReportType == "Summary" || this._subReportType == "Credit Balance")
								{
									this.createColumns("Summary");
									this.UpdateCustomerDataToGrid(this.tdsR1);
									this.UpdateToReport3(this.tdsR1);
									this.tdsR1.Clear();
									this.intzaReport.Redraw();
								}
								else
								{
									if (this._subReportType == "Confrim by Stock" || this._subReportType == "Confrim Summary")
									{
										this.createColumns("Confrim by Stock");
										if (this.tdsR8.Tables.Contains("ITDS_Get_Cust_Info") && this.tdsR8.Tables["ITDS_Get_Cust_Info"].Rows.Count > 0)
										{
											this.UpdateCustomerDataToGrid(this.tdsR8);
										}
										this.UpdateToConfirmByStock();
										this.intzaReport.Redraw();
									}
									else
									{
										if (this._subReportType == "Realize Profit/Loss")
										{
											this.createColumns("Realize Profit/Loss");
											if (this.tdsR3.Tables.Contains("view_realize_pl") && this.tdsR3.Tables["view_realize_pl"].Rows.Count > 0)
											{
												foreach (DataRow dr in this.tdsR3.Tables["view_realize_pl"].Rows)
												{
													this.UpdateToReport4(dr);
												}
											}
											if (this.tdsR3.Tables.Contains("ITDS_Get_Cust_Info") && this.tdsR3.Tables["ITDS_Get_Cust_Info"].Rows.Count > 0)
											{
												this.UpdateCustomerDataToGrid(this.tdsR3);
											}
											this.tdsR3.Clear();
											this.intzaReport.Redraw();
										}
									}
								}
								if (this._accType == "C" || this._accType == "H")
								{
									this.intzaInfoHeader.Item("tbEquity").Text = Utilities.VolumeFormat(this._buyLimit + this._totCurrValue, true);
								}
							}
						}
					}
					else
					{
						this.tslbAccountT.Text = this._currAccount;
						this.UpdateCustomerDataToGrid_TFEX();
						if (this.tdsTfex != null)
						{
							this.tdsTfex.Clear();
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("bgwLoadReport_RunWorkerCompleted", ex);
				this.intzaReport.Redraw();
			}
			base.IsLoadingData = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateToVolumeChart(DataSet ds)
		{
			try
			{
				this.wcGraphVolume.InitData();
				this.wcGraphVolumeSector.InitData();
				Dictionary<string, frmPortfolio.VCItem> dictionary = new Dictionary<string, frmPortfolio.VCItem>();
				Dictionary<string, frmPortfolio.VCItem> dictionary2 = new Dictionary<string, frmPortfolio.VCItem>();
				string key = string.Empty;
				foreach (DataRow dataRow in ds.Tables[0].Rows)
				{
					key = dataRow["stock"].ToString().Trim();
					decimal num;
					decimal.TryParse(dataRow["Cost"].ToString(), out num);
					decimal num2;
					decimal.TryParse(dataRow["Curr_val"].ToString(), out num2);
					decimal value;
					if (ApplicationInfo.SupportFreewill)
					{
						decimal.TryParse(dataRow["Unrl_pl"].ToString(), out value);
					}
					else
					{
						if (dataRow["position_type"].ToString() == "S")
						{
							value = Math.Round(num - num2, 0, MidpointRounding.AwayFromZero);
						}
						else
						{
							value = Math.Round(num2 - num, 0, MidpointRounding.AwayFromZero);
						}
					}
					if (num > 0m)
					{
						if (dictionary.ContainsKey(key))
						{
							frmPortfolio.VCItem vCItem = dictionary[key];
							vCItem.profit += (double)value;
							vCItem.value += (double)num;
						}
						else
						{
							dictionary.Add(key, new frmPortfolio.VCItem((double)num, (double)value));
						}
					}
				}
				decimal num3 = this._buyLimit;
				foreach (KeyValuePair<string, frmPortfolio.VCItem> current in dictionary)
				{
					num3 += (decimal)current.Value.value;
					StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[current.Key];
					if (stockInformation.Number > 0)
					{
						string key2 = string.Empty;
						IndexStat.IndexItem sector = ApplicationInfo.IndexStatInfo.GetSector(stockInformation.SectorNumber);
						if (sector != null)
						{
							key2 = sector.Symbol;
						}
						else
						{
							if (stockInformation.SecurityType == "W")
							{
								key2 = "WARRANT";
							}
							else
							{
								if (stockInformation.SecurityType == "F")
								{
									key2 = "FOREIGN";
								}
								else
								{
									key2 = "OTHER";
								}
							}
						}
						if (!dictionary2.ContainsKey(key2))
						{
							dictionary2.Add(key2, new frmPortfolio.VCItem(current.Value.value, current.Value.profit));
						}
						else
						{
							dictionary2[key2].value += current.Value.value;
							dictionary2[key2].profit += current.Value.profit;
						}
					}
				}
				decimal num4 = 0m;
				foreach (KeyValuePair<string, frmPortfolio.VCItem> current in dictionary)
				{
					decimal num5 = Math.Round((decimal)current.Value.value / num3 * 100m, 2);
					if (num5 == 0m)
					{
						num5 = 0.01m;
					}
					num4 += num5;
					this.wcGraphVolume.InputData(current.Key, (double)num5, current.Value.profit);
				}
				num4 = 0m;
				foreach (KeyValuePair<string, frmPortfolio.VCItem> current in dictionary2)
				{
					decimal num5 = Math.Round((decimal)current.Value.value / num3 * 100m, 2);
					if (num5 == 0m)
					{
						num5 = 0.01m;
					}
					num4 += num5;
					this.wcGraphVolumeSector.InputData(current.Key, (double)num5, current.Value.profit);
				}
				this.wcGraphVolumeSector.InputData("Credit", (double)(100m - num4), 0.0);
				dictionary.Clear();
				dictionary = null;
				dictionary2.Clear();
				dictionary2 = null;
				this.wcGraphVolume.EndUpdate();
				this.wcGraphVolumeSector.EndUpdate();
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateToVolumeChart", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private decimal GetSpTotolCommVAT(DataSet ds)
		{
			decimal result = 0m;
			try
			{
				if (ds != null && ds.Tables.Contains("RETURN") && ds.Tables["RETURN"].Rows.Count > 0)
				{
					DataRow dataRow = ds.Tables["RETURN"].Rows[0];
					decimal.TryParse(dataRow["TotalCommVAT"].ToString(), out result);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaListView1_KeyPress(object sender, KeyPressEventArgs e)
		{
			try
			{
				string text = e.KeyChar.ToString();
				switch (text)
				{
				case "0":
				case "1":
				case "2":
				case "3":
				case "4":
				case "5":
				case "6":
				case "7":
				case "8":
				case "9":
					goto IL_14F;
				}
				if (char.IsLetter(e.KeyChar))
				{
					this.tstbStock2.Focus();
					this.tstbStock2.Text = e.KeyChar.ToString();
					this.tstbStock2.Select(1, 0);
				}
				IL_14F:;
			}
			catch (Exception ex)
			{
				this.ShowError("intzaListView1_KeyPress", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmPortfolio_IDoSymbolLinked(object sender, SymbolLinkSource source, string newStock)
		{
			if (source == SymbolLinkSource.SwitchAccount)
			{
				if (!this.bgwLoadReport.IsBusy)
				{
					if (this._currAccount != ApplicationInfo.AccInfo.CurrentAccount)
					{
						this._reportType = "Portfolio";
						this._subReportType = "Summary";
						this.StartReloadTimer(200, true);
					}
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnSelStartDate_Click(object sender, EventArgs e)
		{
			try
			{
				this._mcPos = 1;
				this.monthCalendar1.Top = this.tStripMenu.Height;
				this.monthCalendar1.Left = this.tstbStartDate.Bounds.Left;
				this.monthCalendar1.SetDate(new DateTime(Convert.ToInt32(this.tstbStartDate.Text.Substring(6, 4)), Convert.ToInt32(this.tstbStartDate.Text.Substring(3, 2)), Convert.ToInt32(this.tstbStartDate.Text.Substring(0, 2))));
				this.monthCalendar1.Show();
				this.monthCalendar1.Focus();
			}
			catch (Exception ex)
			{
				this.ShowError("tsbtnSelStartDate_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnSelEndDate_Click(object sender, EventArgs e)
		{
			try
			{
				this._mcPos = 2;
				this.monthCalendar1.Top = this.tStripMenu.Height;
				this.monthCalendar1.Left = this.tstbEndDate.Bounds.Left;
				this.monthCalendar1.SetDate(new DateTime(Convert.ToInt32(this.tstbEndDate.Text.Substring(6, 4)), Convert.ToInt32(this.tstbEndDate.Text.Substring(3, 2)), Convert.ToInt32(this.tstbEndDate.Text.Substring(0, 2))));
				this.monthCalendar1.Show();
				this.monthCalendar1.Focus();
			}
			catch (Exception ex)
			{
				this.ShowError("tsbtnSelEndDate_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
		{
			try
			{
				if (this._mcPos == 1)
				{
					this.tstbStartDate.Text = e.Start.ToString("dd/MM/yyyy");
				}
				else
				{
					if (this._mcPos == 2)
					{
						this.tstbEndDate.Text = e.Start.ToString("dd/MM/yyyy");
					}
				}
				this.monthCalendar1.Hide();
			}
			catch (Exception ex)
			{
				this.ShowError("monthCalendar1_DateSelected", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void monthCalendar1_Leave(object sender, EventArgs e)
		{
			this.monthCalendar1.Hide();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnReload_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this.bgwLoadReport.IsBusy)
				{
					this.bgwLoadReport.RunWorkerAsync();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tsbtnReload_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateCustomerDataToGrid(DataSet ds)
		{
			try
			{
				this.intzaInfoHeader.BeginUpdate();
				this.intzaCB.BeginUpdate();
				this.intzaInfoHeader.ClearAllText();
				this.intzaCB.ClearAllText();
				if (ApplicationInfo.SupportFreewill)
				{
					if (ds != null && ds.Tables["ITDS_Get_Cust_Info"].Rows.Count > 0)
					{
						DataRow dataRow = ds.Tables["ITDS_Get_Cust_Info"].Rows[0];
						this._accType = dataRow["ACCTYPE"].ToString().Trim();
						this.intzaInfoHeader.Item("tbCustName").Text = ApplicationInfo.AccInfo.CurrentAccount + " " + dataRow["custname"].ToString().Trim();
						this.intzaInfoHeader.Item("tbAccountType").Text = Utilities.GetAccountTypeName(this._accType);
						this.intzaInfoHeader.Item("tbTrader").Text = dataRow["traderid"].ToString().Trim() + " " + dataRow["tradername"].ToString().Trim();
						this.intzaInfoHeader.Item("tbCustomerType").Text = dataRow["custtype"].ToString();
						this.intzaInfoHeader.Item("tbCreditType").Text = Utilities.GetCreditTypeName(dataRow["credittype"].ToString());
						decimal num = 0m;
						if (this._accType != "B")
						{
							decimal.TryParse(dataRow["buycredit"].ToString(), out num);
							this.intzaInfoHeader.Item("tbBuyLimit").Text = num.ToString("#,##0.00");
							this._buyLimit = num;
						}
						decimal num2 = 0m;
						decimal.TryParse(dataRow["creditlimit"].ToString(), out num2);
						this.intzaInfoHeader.Item("tbCreditLine").Text = num2.ToString("#,##0.00");
						this.intzaInfoHeader.Item("tbCantOverCredit").Text = ((dataRow["canovercredit"].ToString() == "Y") ? "N" : "Y");
						if (this._accType == "B")
						{
							this.intzaInfoHeader.Item("lbBuyLimit").Text = "Excess Equity";
							this.intzaInfoHeader.Item("lbHighLimit").Text = "Purchasing Power";
							if (ds.Tables.Contains("ITDS_Get_Cust_CrdtBal") && ds.Tables["ITDS_Get_Cust_CrdtBal"].Rows.Count > 0)
							{
								DataRow dataRow2 = ds.Tables["ITDS_Get_Cust_CrdtBal"].Rows[0];
								decimal.TryParse(dataRow2["PP"].ToString(), out num);
								this.intzaInfoHeader.Item("tbHighLimit").Text = num.ToString("#,##0.00");
								decimal num3;
								decimal.TryParse(dataRow2["equity"].ToString(), out num3);
								this.intzaInfoHeader.Item("tbEquity").Text = num3.ToString("#,##0.00");
								decimal.TryParse(dataRow2["EE"].ToString(), out num);
								this.intzaCB_Freewill.Item("tbAccEE").Text = num.ToString("#,##0.00");
								this.intzaCB_Freewill.Item("tbEE").Text = num.ToString("#,##0.00");
								this.intzaInfoHeader.Item("tbBuyLimit").Text = num.ToString("#,##0.00");
								this._buyLimit = num;
								decimal.TryParse(dataRow2["EE_50"].ToString(), out num);
								this.intzaCB_Freewill.Item("tbBuyCredit50").Text = num.ToString("#,##0.00");
								decimal.TryParse(dataRow2["EE_60"].ToString(), out num);
								this.intzaCB_Freewill.Item("tbBuyCredit60").Text = num.ToString("#,##0.00");
								decimal.TryParse(dataRow2["EE_70"].ToString(), out num);
								this.intzaCB_Freewill.Item("tbBuyCredit70").Text = num.ToString("#,##0.00");
								decimal.TryParse(dataRow2["ASSET"].ToString(), out num);
								this.intzaCB_Freewill.Item("tbAssets").Text = num.ToString("#,##0.00");
								decimal.TryParse(dataRow2["MR"].ToString(), out num);
								this.intzaCB_Freewill.Item("tbMR").Text = num.ToString("#,##0.00");
								decimal.TryParse(dataRow2["call_force"].ToString(), out num);
								this.intzaCB_Freewill.Item("tbCallForce").Text = num.ToString("#,##0.00");
								decimal.TryParse(dataRow2["call_force"].ToString(), out num);
								this.intzaCB_Freewill.Item("tbCallForce").Text = num.ToString("#,##0.00");
								decimal.TryParse(dataRow2["Liability"].ToString(), out num);
								this.intzaCB_Freewill.Item("tbLiabilities").Text = num.ToString("#,##0.00");
								decimal.TryParse(dataRow2["Equity"].ToString(), out num);
								this.intzaCB_Freewill.Item("tbEquity").Text = num.ToString("#,##0.00");
								this.intzaCB_Freewill.Item("tbEquity").FontColor = Utilities.ComparePriceColor(num, 0m);
								decimal.TryParse(dataRow2["BuyMR"].ToString(), out num);
								this.intzaCB_Freewill.Item("tbBuyMR").Text = num.ToString("#,##0.00");
								decimal.TryParse(dataRow2["SellMR"].ToString(), out num);
								this.intzaCB_Freewill.Item("tbSellMR").Text = num.ToString("#,##0.00");
								decimal.TryParse(dataRow2["Cash_balance"].ToString(), out num);
								this.intzaCB_Freewill.Item("tbCashBal").Text = num.ToString("#,##0.00");
								decimal.TryParse(dataRow2["EE"].ToString(), out num);
								this.intzaCB_Freewill.Item("tbEE").Text = num.ToString("#,##0.00");
								this.intzaCB_Freewill.Item("tbEE").FontColor = Utilities.ComparePriceColor(num, 0m);
								decimal.TryParse(dataRow2["PP"].ToString(), out num);
								this.intzaCB_Freewill.Item("tbPP").Text = num.ToString("#,##0.00");
								this.intzaCB_Freewill.Item("tbPP").FontColor = Utilities.ComparePriceColor(num, 0m);
								decimal.TryParse(dataRow2["brk_call_lmv"].ToString(), out num);
								this.intzaCB_Freewill.Item("tbCallLMV").Text = num.ToString("#,##0.00");
								decimal.TryParse(dataRow2["LMV"].ToString(), out num);
								this.intzaCB_Freewill.Item("tbLMV").Text = num.ToString("#,##0.00");
								decimal.TryParse(dataRow2["Collat"].ToString(), out num);
								this.intzaCB_Freewill.Item("tbCollateral").Text = num.ToString("#,##0.00");
								decimal.TryParse(dataRow2["call_margin"].ToString(), out num);
								this.intzaCB_Freewill.Item("tbCallMargin").Text = num.ToString("#,##0.00");
								decimal.TryParse(dataRow2["brk_call_smv"].ToString(), out num);
								this.intzaCB_Freewill.Item("tbCallSMV").Text = num.ToString("#,##0.00");
								decimal.TryParse(dataRow2["SMV"].ToString(), out num);
								this.intzaCB_Freewill.Item("tbSMV").Text = num.ToString("#,##0.00");
								decimal.TryParse(dataRow2["Debt"].ToString(), out num);
								this.intzaCB_Freewill.Item("tbDEBT").Text = num.ToString("#,##0.00");
								decimal.TryParse(dataRow2["brk_sell_lmv"].ToString(), out num);
								this.intzaCB_Freewill.Item("tbForceLMV").Text = num.ToString("#,##0.00");
								decimal.TryParse(dataRow2["BMV"].ToString(), out num);
								this.intzaCB_Freewill.Item("tbBMV").Text = num.ToString("#,##0.00");
								this.intzaCB_Freewill.Item("tbAction").Text = dataRow2["action"].ToString();
								decimal.TryParse(dataRow2["brk_sell_smv"].ToString(), out num);
								this.intzaCB_Freewill.Item("tbForceSMV").Text = num.ToString("#,##0.00");
								decimal.TryParse(dataRow2["Withdrawal"].ToString(), out num);
								this.intzaCB_Freewill.Item("tbWithdrawal").Text = num.ToString("#,##0.00");
								this.intzaCB_Freewill.Item("tbWithdrawal").FontColor = Utilities.ComparePriceColor(num, 0m);
								decimal.TryParse(dataRow["Mrgcode"].ToString(), out num);
								this.intzaCB_Freewill.Item("tbMarginRate").Text = num.ToString("#,##0.00");
							}
						}
						else
						{
							this.intzaInfoHeader.Item("lbBuyLimit").Text = "Buy Limit";
							this.intzaInfoHeader.Item("lbHighLimit").Text = "";
						}
						if (this._isSupportCBReport)
						{
							if (this._accType == "B")
							{
								this.tsbtnCreditBalance.Visible = true;
							}
							else
							{
								this.tsbtnCreditBalance.Visible = false;
							}
						}
					}
				}
				else
				{
					if (ds != null && ds.Tables["ITDS_Get_Cust_Info"].Rows.Count > 0)
					{
						DataRow dataRow = ds.Tables["ITDS_Get_Cust_Info"].Rows[0];
						if (dataRow["sServiceTypeFlag"].ToString().Trim() == "G")
						{
							this._iCommType = 1;
						}
						else
						{
							this._iCommType = 0;
						}
						decimal.TryParse(dataRow["nmrTradingFee"].ToString(), out this._tradingFee);
						decimal.TryParse(dataRow["nmrClearingFee"].ToString(), out this._clearingFee);
						decimal.TryParse(dataRow["nmrSettlementFee"].ToString(), out this._settlementFee);
						this._accType = dataRow["sAccType"].ToString().Trim();
						this.intzaInfoHeader.Item("tbCustName").Text = ApplicationInfo.AccInfo.CurrentAccount + " " + dataRow["sName"].ToString().Trim();
						this.intzaInfoHeader.Item("tbAccountType").Text = Utilities.GetAccountTypeName(this._accType);
						this.intzaInfoHeader.Item("tbTrader").Text = dataRow["sTraderName"].ToString().Trim() + " : " + dataRow["sTradUserNo"].ToString().Trim();
						this.intzaInfoHeader.Item("tbCustomerType").Text = dataRow["sPC"].ToString();
						this.intzaInfoHeader.Item("tbCreditType").Text = Utilities.GetCreditTypeName(dataRow["sCrdtType"].ToString());
						this.intzaInfoHeader.Item("tbCantOverCredit").Text = dataRow["sNotOverShort"].ToString();
						if (this._accType == "C" || this._accType == "H")
						{
							decimal num4 = 0m;
							this.intzaInfoHeader.Item("lbBuyLimit").Text = "Buy Limit";
							decimal.TryParse(dataRow["nmrBuyLmt"].ToString(), out this._buyLimit);
							this.intzaInfoHeader.Item("tbBuyLimit").Text = this._buyLimit.ToString("#,##0.00");
							this.intzaInfoHeader.Item("lbHighLimit").Text = "High Limit";
							decimal.TryParse(dataRow["nmrHighLmt"].ToString(), out num4);
							this.intzaInfoHeader.Item("tbHighLimit").Text = num4.ToString("#,##0.00");
							this.intzaInfoHeader.Item("tbCustomerFlag").Text = dataRow["sCustFlag"].ToString();
							this.intzaInfoHeader.Item("tbBuyLimit").FontColor = Color.Yellow;
							this.intzaInfoHeader.Item("tbHighLimit").FontColor = Color.Yellow;
							decimal num2 = 0m;
							decimal.TryParse(dataRow["nmrOrigBuy"].ToString(), out num2);
							this.intzaInfoHeader.Item("tbCreditLine").Text = num2.ToString("#,##0.00");
						}
						if (this._accType == "B" || this._accType == "T")
						{
							string text = string.Empty;
							text = dataRow["sTSFCFlag"].ToString();
							if (text.Trim() == "" || text.ToUpper() == "N")
							{
								this.intzaInfoHeader.Item("tbCustomerFlag").Text = "Normal";
							}
							else
							{
								if (text.ToUpper() == "Y")
								{
									this.intzaInfoHeader.Item("tbCustomerFlag").Text = "TSFC";
								}
							}
							decimal num2 = 0m;
							decimal.TryParse(dataRow["nmrOrigBuy"].ToString(), out num2);
							this.intzaInfoHeader.Item("tbCreditLine").Text = num2.ToString("#,##0.00");
							if (ds.Tables.Contains("ITDS_Get_Cust_CrdtBal"))
							{
								DataRow dataRow2 = ds.Tables["ITDS_Get_Cust_CrdtBal"].Rows[0];
								decimal num5 = 0m;
								int num6 = 0;
								decimal.TryParse(dataRow2["LoanLimit"].ToString(), out num5);
								int.TryParse(dataRow2["MarginRate"].ToString(), out num6);
								this.intzaCB.Item("tbLoanLimit").Text = num5.ToString("#,##0.00");
								this.intzaCB.Item("tbMarginRate").Text = num6.ToString("#,###") + "%";
								decimal price = 0m;
								decimal price2 = 0m;
								decimal price3 = 0m;
								decimal price4 = 0m;
								decimal price5 = 0m;
								decimal price6 = 0m;
								decimal price7 = 0m;
								decimal price8 = 0m;
								decimal num7 = 0m;
								decimal num8 = 0m;
								decimal.TryParse(dataRow2["PrevEE"].ToString(), out price);
								decimal.TryParse(dataRow2["PrevEquity"].ToString(), out price2);
								decimal.TryParse(dataRow2["PrevMR"].ToString(), out price3);
								decimal.TryParse(dataRow2["PrevCashBal"].ToString(), out price4);
								decimal.TryParse(dataRow2["PrevLMV"].ToString(), out price5);
								decimal.TryParse(dataRow2["PrevColl"].ToString(), out price6);
								decimal.TryParse(dataRow2["PrevLoan"].ToString(), out price7);
								decimal.TryParse(dataRow2["PrevSMV"].ToString(), out price8);
								decimal.TryParse(dataRow2["PrevCall"].ToString(), out num7);
								decimal.TryParse(dataRow2["PrevForce"].ToString(), out num8);
								this.intzaCB.Item("tbExcessEquityPrevious").Text = price.ToString("#,##0.00");
								this.intzaCB.Item("tbEquityPrevious").Text = price2.ToString("#,##0.00");
								this.intzaCB.Item("tbMRPrevious").Text = price3.ToString("#,##0.00");
								this.intzaCB.Item("tbCashBalancePrevious").Text = price4.ToString("#,##0.00");
								this.intzaCB.Item("tbLmvPrevious").Text = price5.ToString("#,##0.00");
								this.intzaCB.Item("tbColleteralPrevious").Text = price6.ToString("#,##0.00");
								this.intzaCB.Item("tbLoanPrevious").Text = price7.ToString("#,##0.00");
								this.intzaCB.Item("tbSmvPrevious").Text = price8.ToString("#,##0.00");
								this.intzaCB.Item("tbCallPrevious").Text = num7.ToString("#,##0.00");
								this.intzaCB.Item("tbForcePrevious").Text = num8.ToString("#,##0.00");
								decimal price9 = 0m;
								decimal price10 = 0m;
								decimal num9 = 0m;
								decimal price11 = 0m;
								decimal price12 = 0m;
								decimal price13 = 0m;
								decimal price14 = 0m;
								decimal price15 = 0m;
								decimal price16 = 0m;
								decimal price17 = 0m;
								decimal num10 = 0m;
								decimal num11 = 0m;
								decimal num12 = 0m;
								decimal.TryParse(dataRow2["CurEE"].ToString(), out num9);
								decimal.TryParse(dataRow2["CurEquity"].ToString(), out price10);
								decimal.TryParse(dataRow2["CurCashBal"].ToString(), out price11);
								decimal.TryParse(dataRow2["CurLoan"].ToString(), out price9);
								decimal.TryParse(dataRow2["MM%"].ToString(), out price16);
								decimal.TryParse(dataRow2["MarkEE"].ToString(), out price17);
								decimal.TryParse(dataRow2["CurMR"].ToString(), out price15);
								decimal.TryParse(dataRow2["CurLMV"].ToString(), out price12);
								decimal.TryParse(dataRow2["CurColl"].ToString(), out price13);
								decimal.TryParse(dataRow2["CurSMV"].ToString(), out price14);
								decimal.TryParse(dataRow2["CurCall"].ToString(), out num10);
								decimal.TryParse(dataRow2["CurForce"].ToString(), out num11);
								decimal.TryParse(dataRow2["PP"].ToString(), out num12);
								this.intzaCB.Item("tbExcessEquityCurrent").Text = num9.ToString("#,##0.00");
								this.intzaCB.Item("tbEquityCurrent").Text = price10.ToString("#,##0.00");
								this.intzaCB.Item("tbCashBalanceCurrent").Text = price11.ToString("#,##0.00");
								this.intzaCB.Item("tbLoanCurrent").Text = price9.ToString("#,##0.00");
								this.intzaCB.Item("tbMMpercentCurrent").Text = price16.ToString();
								this.intzaCB.Item("tbMarkToEECurrent").Text = price17.ToString("#,##0.00");
								this.intzaCB.Item("tbMRCurrent").Text = price15.ToString("#,##0.00");
								this.intzaCB.Item("tbLmvCurrent").Text = price12.ToString("#,##0.00");
								this.intzaCB.Item("tbColleteralCurrent").Text = price13.ToString("#,##0.00");
								this.intzaCB.Item("tbSmvCurrent").Text = price14.ToString("#,##0.00");
								this.intzaCB.Item("tbCallCurrent").Text = num10.ToString("#,##0.00");
								this.intzaCB.Item("tbForceCurrent").Text = num11.ToString("#,##0.00");
								this.intzaInfoHeader.Item("tbBuyLimit").Text = num9.ToString("#,##0.00");
								this._buyLimit = num9;
								this.intzaInfoHeader.Item("tbHighLimit").Text = num12.ToString("#,##0.00");
								this.intzaCB.Item("tbExcessEquityPrevious").FontColor = Utilities.ComparePriceColor(price, 0m);
								this.intzaCB.Item("tbExcessEquityCurrent").FontColor = Utilities.ComparePriceColor(num9, 0m);
								this.intzaCB.Item("tbMarkToEECurrent").FontColor = Utilities.ComparePriceColor(price17, 0m);
								this.intzaCB.Item("tbMMpercentCurrent").FontColor = Utilities.ComparePriceColor(price16, 0m);
								this.intzaCB.Item("tbEquityPrevious").FontColor = Utilities.ComparePriceColor(price2, 0m);
								this.intzaCB.Item("tbEquityCurrent").FontColor = Utilities.ComparePriceColor(price10, 0m);
								this.intzaCB.Item("tbCashBalancePrevious").FontColor = Utilities.ComparePriceColor(price4, 0m);
								this.intzaCB.Item("tbCashBalanceCurrent").FontColor = Utilities.ComparePriceColor(price11, 0m);
								this.intzaCB.Item("tbLmvPrevious").FontColor = Utilities.ComparePriceColor(price5, 0m);
								this.intzaCB.Item("tbLmvCurrent").FontColor = Utilities.ComparePriceColor(price12, 0m);
								this.intzaCB.Item("tbColleteralPrevious").FontColor = Utilities.ComparePriceColor(price13, 0m);
								this.intzaCB.Item("tbColleteralCurrent").FontColor = Utilities.ComparePriceColor(price6, 0m);
								this.intzaCB.Item("tbLoanPrevious").FontColor = Utilities.ComparePriceColor(price7, 0m);
								this.intzaCB.Item("tbLoanCurrent").FontColor = Utilities.ComparePriceColor(price9, 0m);
								this.intzaCB.Item("tbSmvPrevious").FontColor = Utilities.ComparePriceColor(price8, 0m);
								this.intzaCB.Item("tbSmvCurrent").FontColor = Utilities.ComparePriceColor(price14, 0m);
								this.intzaCB.Item("tbMRPrevious").FontColor = Utilities.ComparePriceColor(price3, 0m);
								this.intzaCB.Item("tbMRCurrent").FontColor = Utilities.ComparePriceColor(price15, 0m);
								decimal num3;
								decimal.TryParse(dataRow2["CurEquity"].ToString(), out num3);
								this.intzaInfoHeader.Item("tbEquity").Text = num3.ToString("#,##0.00");
							}
							else
							{
								decimal.TryParse(dataRow["nmrBuyLmt"].ToString(), out this._buyLimit);
								this.intzaInfoHeader.Item("tbBuyLimit").Text = this._buyLimit.ToString("#,##0.00");
								this.intzaInfoHeader.Item("tbHighLimit").Text = (this._buyLimit / ApplicationInfo.UserMarginRate * 100m).ToString("#,##0.00");
							}
							this.intzaInfoHeader.Item("lbBuyLimit").Text = "Excess Equity";
							this.intzaInfoHeader.Item("lbHighLimit").Text = "Purchasing Power";
						}
						if (this._isSupportCBReport)
						{
							if (this._accType == "B" || this._accType == "T")
							{
								this.tsbtnCreditBalance.Visible = true;
							}
							else
							{
								this.tsbtnCreditBalance.Visible = false;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				this.intzaInfoHeader.Redraw();
				this.intzaCB.Redraw();
				if (!base.Visible)
				{
					base.Show();
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateToReport3(DataSet ds)
		{
			try
			{
				long num = 0L;
				long num2 = 0L;
				decimal num3 = 0m;
				decimal num4 = 0m;
				decimal num5 = 0m;
				decimal num6 = 0m;
				decimal num7 = 0m;
				decimal d = 0m;
				decimal d2 = 0m;
				decimal num8 = 0m;
				this.intzaReport.Rows = ds.Tables["view_projected_pl"].Rows.Count;
				Dictionary<string, decimal> dictionary = new Dictionary<string, decimal>();
				decimal num9 = 0m;
				IEnumerator enumerator;
				if (!ApplicationInfo.SupportFreewill)
				{
					enumerator = ds.Tables["view_realize_pl"].Rows.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							DataRow dataRow = (DataRow)enumerator.Current;
							string key = string.Concat(new string[]
							{
								dataRow["stock"].ToString().Trim(),
								"_",
								dataRow["position_type"].ToString(),
								"_",
								dataRow["trustee_id"].ToString()
							});
							decimal.TryParse(dataRow["stock"].ToString(), out num9);
							decimal d3;
							decimal.TryParse(dataRow["costCommVat"].ToString(), out d3);
							decimal d4;
							decimal.TryParse(dataRow["sellComVat"].ToString(), out d4);
							decimal num10;
							decimal.TryParse(dataRow["sell_total_amount"].ToString(), out num10);
							decimal num11;
							decimal.TryParse(dataRow["cost_total_amount"].ToString(), out num11);
							if (ApplicationInfo.BrokerId == 2 || ApplicationInfo.BrokerId == 88)
							{
								if (dataRow["position_type"].ToString() == "S")
								{
									num9 = num11 - num10;
								}
								else
								{
									num9 = num10 - num11;
								}
							}
							else
							{
								if (dataRow["position_type"].ToString() == "S")
								{
									num9 = num11 - num10 - (d3 + d4);
								}
								else
								{
									num9 = num10 - num11 - (d3 + d4);
								}
							}
							dictionary.Add(key, num9);
						}
					}
					finally
					{
						IDisposable disposable = enumerator as IDisposable;
						if (disposable != null)
						{
							disposable.Dispose();
						}
					}
				}
				int num12 = 0;
				enumerator = ds.Tables["view_projected_pl"].Rows.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						DataRow dataRow = (DataRow)enumerator.Current;
						RecordItem recordItem = this.intzaReport.Records(num12);
						long.TryParse(dataRow["onhand"].ToString(), out num);
						decimal.TryParse(dataRow["average"].ToString(), out num3);
						decimal.TryParse(dataRow["last_price"].ToString(), out num5);
						long.TryParse(dataRow["sellable"].ToString(), out num2);
						decimal.TryParse(dataRow["Cost"].ToString(), out num4);
						decimal.TryParse(dataRow["Curr_val"].ToString(), out num6);
						if (ApplicationInfo.SupportFreewill)
						{
							if (num5 > 0m)
							{
								decimal.TryParse(dataRow["Unrl_pl"].ToString(), out num7);
								decimal.TryParse(dataRow["Unrl_pl_pct"].ToString(), out num8);
							}
							else
							{
								decimal.TryParse(dataRow["i2last_price"].ToString(), out num5);
								num6 = num5 * num2;
								decimal num13 = num6 * ApplicationInfo.AccInfo.TotalCommAndFee / 100m;
								num13 += num13 * ApplicationInfo.UserVAT / 100m;
								if (num4 != 0m)
								{
									if (dataRow["position_type"].ToString() == "20")
									{
										num7 = num4 - (num6 + num13);
									}
									else
									{
										num7 = num6 - num13 - num4;
									}
									num8 = num7 / num4 * 100m;
								}
								else
								{
									num7 = 0m;
									num8 = 0m;
								}
							}
							decimal.TryParse(dataRow["realize_pl"].ToString(), out num9);
						}
						else
						{
							decimal.TryParse(dataRow["costCommVat"].ToString(), out d);
							decimal.TryParse(dataRow["currValCommVat"].ToString(), out d2);
							num7 = 0m;
							num8 = 0m;
							num9 = 0m;
							if (ApplicationInfo.BrokerId == 2 || ApplicationInfo.BrokerId == 88)
							{
								if (dataRow["position_type"].ToString() == "S")
								{
									num7 = Math.Round(num4 - num6, 0, MidpointRounding.AwayFromZero);
								}
								else
								{
									num7 = Math.Round(num6 - num4, 0, MidpointRounding.AwayFromZero);
								}
							}
							else
							{
								if (num6 > 0m && num4 > 0m)
								{
									if (dataRow["position_type"].ToString() == "S")
									{
										num7 = Math.Round(num4 - num6 - (d + d2), 0, MidpointRounding.AwayFromZero);
									}
									else
									{
										num7 = Math.Round(num6 - num4 - (d + d2), 0, MidpointRounding.AwayFromZero);
									}
								}
							}
							dictionary.TryGetValue(string.Concat(new string[]
							{
								dataRow["stock"].ToString().Trim(),
								"_",
								dataRow["position_type"].ToString(),
								"_",
								dataRow["trustee_id"].ToString()
							}), out num9);
							if (num4 > 0m)
							{
								num8 = num7 / num4 * 100m;
							}
						}
						if (dataRow["position_type"].ToString() != "B")
						{
							this._totUnReal += num7;
							this._totRealize += num9;
							this._totCurrValue += num6;
							this._totCost += num4;
						}
						recordItem.Fields("stock").Text = dataRow["stock"].ToString();
						recordItem.Fields("stock").Tag = this.GetPositionType(dataRow["position_type"].ToString().Trim());
						recordItem.Fields("onhand").Text = FormatUtil.VolumeFormat(num, true);
						recordItem.Fields("avg").Text = Utilities.PriceFormat(num3, (ApplicationInfo.BrokerId == 11) ? 2 : 4);
						recordItem.Fields("last").Text = Utilities.PriceFormat(num5);
						recordItem.Fields("unreal_pct").Text = Utilities.PriceFormat(num8, true, "");
						recordItem.Fields("sellable").Text = Utilities.VolumeFormat(num2, true);
						recordItem.Fields("cost").Text = Utilities.VolumeFormat((long)num4, true);
						recordItem.Fields("curr_value").Text = Utilities.VolumeFormat((long)num6, true);
						recordItem.Fields("ttf").Text = Utilities.PriceFormat(dataRow["trustee_id"].ToString());
						recordItem.Fields("unreal").Text = Utilities.VolumeFormat((long)num7, true);
						recordItem.Fields("realize").Text = Utilities.PriceFormat(num9, 2);
						Color fontColor = Color.Yellow;
						if (num7 > 0m)
						{
							fontColor = Color.Lime;
						}
						else
						{
							if (num7 < 0m)
							{
								fontColor = Color.Red;
							}
							else
							{
								fontColor = Color.Yellow;
							}
						}
						recordItem.Fields("stock").FontColor = fontColor;
						recordItem.Fields("onhand").FontColor = fontColor;
						recordItem.Fields("avg").FontColor = fontColor;
						recordItem.Fields("last").FontColor = fontColor;
						recordItem.Fields("unreal_pct").FontColor = fontColor;
						recordItem.Fields("sellable").FontColor = fontColor;
						recordItem.Fields("cost").FontColor = fontColor;
						recordItem.Fields("curr_value").FontColor = fontColor;
						recordItem.Fields("ttf").FontColor = fontColor;
						recordItem.Fields("unreal").FontColor = fontColor;
						if (num9 > 0m)
						{
							fontColor = Color.Lime;
						}
						else
						{
							fontColor = Color.Red;
						}
						recordItem.Fields("realize").FontColor = fontColor;
						num12++;
					}
				}
				finally
				{
					IDisposable disposable = enumerator as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
				dictionary.Clear();
				dictionary = null;
				if (this._totCost > 0m)
				{
					this._totUnReal_Pct = this._totUnReal / this._totCost * 100m;
				}
				else
				{
					this._totUnReal_Pct = 0m;
				}
				this.setSummaryTab();
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateToReport3", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string GetPositionType(string positionType)
		{
			string result = string.Empty;
			if (ApplicationInfo.SupportFreewill)
			{
				switch (positionType)
				{
				case "2":
					result = "";
					goto IL_15A;
				case "4":
					result = "P";
					goto IL_15A;
				case "8":
					result = "N";
					goto IL_15A;
				case "9":
					result = "E";
					goto IL_15A;
				case "12":
					result = "R";
					goto IL_15A;
				case "20":
					result = "S";
					goto IL_15A;
				case "21":
					result = "p";
					goto IL_15A;
				case "22":
					result = "B";
					goto IL_15A;
				case "23":
					result = "r";
					goto IL_15A;
				}
				result = positionType;
				IL_15A:;
			}
			else
			{
				result = positionType;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void setSummaryTab()
		{
			try
			{
				RecordItem recordItem = this.intzaSumReport.Records(0);
				recordItem.BackColor = Color.FromArgb(25, 25, 25);
				recordItem.Fields("last").Text = "Total";
				recordItem.Fields("unreal_pct").Text = Utilities.PriceFormat(this._totUnReal_Pct, true, "");
				recordItem.Fields("unreal_pct").FontColor = ((this._totUnReal_Pct > 0m) ? Color.Lime : ((this._totUnReal_Pct < 0m) ? Color.Red : Color.Yellow));
				recordItem.Fields("cost").Text = Utilities.VolumeFormat(this._totCost, true);
				recordItem.Fields("cost").FontColor = Color.Yellow;
				recordItem.Fields("curr_value").Text = Utilities.VolumeFormat(this._totCurrValue, true);
				recordItem.Fields("curr_value").FontColor = Color.Yellow;
				recordItem.Fields("unreal").Text = Utilities.VolumeFormat(this._totUnReal, true);
				recordItem.Fields("unreal").FontColor = ((this._totUnReal > 0m) ? Color.Lime : ((this._totUnReal < 0m) ? Color.Red : Color.Yellow));
				recordItem.Fields("realize").Text = Utilities.PriceFormat(this._totRealize, 2);
				recordItem.Fields("realize").FontColor = ((this._totRealize > 0m) ? Color.Lime : ((this._totRealize < 0m) ? Color.Red : Color.Yellow));
				this.intzaSumReport.Redraw();
			}
			catch (Exception ex)
			{
				this.ShowError("setSummaryTab", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateCustomerDataToGrid_TFEX()
		{
			try
			{
				this.intzaCustHeadTfex.BeginUpdate();
				this.intzaCustHeadTfex.ClearAllText();
				if (this.tdsTfex != null && this.tdsTfex.Tables.Contains("ViewCustomers") && this.tdsTfex.Tables["ViewCustomers"].Rows.Count > 0)
				{
					DataRow dataRow = this.tdsTfex.Tables["ViewCustomers"].Rows[0];
					string text = string.Empty;
					text = dataRow["account_type"].ToString().Trim();
					this.intzaCustHeadTfex.Item("tbCustName").Text = dataRow["name"].ToString().Trim();
					this.intzaCustHeadTfex.Item("tbAccountType").Text = text;
					this.intzaCustHeadTfex.Item("tbTrader").Text = dataRow["trader_name"].ToString().Trim() + " : " + dataRow["trader_id"].ToString().Trim();
					this.intzaCustHeadTfex.Item("tbCustomerType").Text = dataRow["customer_type"].ToString();
					decimal num = 0m;
					decimal.TryParse(dataRow["CashBalancePrev"].ToString(), out num);
					this.intzaCustHeadTfex.Item("CashBalancePrev").Text = num.ToString("#,##0.00");
					decimal num2 = 0m;
					string empty = string.Empty;
					this.intzaCustHeadTfex.Item("lbBuyLimit").Text = "Buy Limit";
					decimal.TryParse(dataRow["buy_credit_limit"].ToString(), out num2);
					this.intzaCustHeadTfex.Item("tbBuyLimit").Text = num2.ToString("#,##0.00");
					decimal num3 = 0m;
					decimal.TryParse(dataRow["credit_line"].ToString(), out num3);
					this.intzaCustHeadTfex.Item("tbCreditLine").Text = num3.ToString("#,##0.00");
					if (ApplicationInfo.SupportSerisysTFEX)
					{
						decimal num4 = 0m;
						decimal.TryParse(dataRow["CashBalance"].ToString(), out num4);
						this.intzaCustHeadTfex.Item("tbCashBalance").Text = num4.ToString("#,##0.00");
						decimal num5 = 0m;
						decimal.TryParse(dataRow["ActualComm"].ToString(), out num5);
						this.intzaCustHeadTfex.Item("tbCommvat").Text = num5.ToString("#,##0.00");
					}
				}
				else
				{
					this.intzaCustHeadTfex.Item("tbCustName").Text = "Account not found!";
				}
				if (!ApplicationInfo.SupportSerisysTFEX)
				{
					if (this.tdsTfex != null && this.tdsTfex.Tables.Contains("ITDSD_Get_Cust_Info") && this.tdsTfex.Tables["ITDSD_Get_Cust_Info"].Rows.Count > 0)
					{
						DataRow dataRow = this.tdsTfex.Tables["ITDSD_Get_Cust_Info"].Rows[0];
						decimal num4 = 0m;
						decimal.TryParse(dataRow["cashbalance"].ToString(), out num4);
						this.intzaCustHeadTfex.Item("tbCashBalance").Text = num4.ToString("#,##0.00");
						decimal num5 = 0m;
						decimal.TryParse(dataRow["ActualComm"].ToString(), out num5);
						this.intzaCustHeadTfex.Item("tbCommvat").Text = num5.ToString("#,##0.00");
					}
				}
				this.sortGridTfex.BeginUpdate();
				this.sortGridTfex.Rows = 0;
				this.UpdateToBottomControl_TFEX();
				string text2 = string.Empty;
				string text3 = string.Empty;
				this._tfexTotMarketVal = 0m;
				this._tfexCost = 0m;
				this._tfexUnrealSettle = 0m;
				this._tfexUnrealCost = 0m;
				this._tfexReal = 0m;
				if (this.tdsTfex != null && this.tdsTfex.Tables.Contains("ITDSD_Get_Cust_Posi") && this.tdsTfex.Tables["ITDSD_Get_Cust_Posi"].Rows.Count > 0)
				{
					if (ApplicationInfo.SupportSerisysTFEX)
					{
						foreach (DataRow dataRow in this.tdsTfex.Tables["ITDSD_Get_Cust_Posi"].Rows)
						{
							this.UpdateToReport1_TFEX(dataRow, 0m);
						}
					}
					else
					{
						foreach (DataRow dataRow in this.tdsTfex.Tables["ITDSD_Get_Cust_Posi"].Rows)
						{
							text2 = dataRow["Series"].ToString().Trim();
							text3 = dataRow["Side"].ToString();
							SeriesList.SeriesInformation seriesInformation = ApplicationInfo.SeriesInfo[text2.ToString().Trim()];
							decimal num6 = 0m;
							DataRow[] array = this.tdsTfex.Tables["Realize"].Select(string.Concat(new string[]
							{
								"Series='",
								text2,
								"' and Side='",
								text3,
								"'"
							}));
							if (array != null && array.Length > 0)
							{
								if (text3 == "L")
								{
									decimal.TryParse(array[0]["realize"].ToString(), out num6);
									num6 *= seriesInformation.ContractSize;
								}
								else
								{
									if (text3 == "S")
									{
										decimal.TryParse(array[0]["realize"].ToString(), out num6);
										num6 *= seriesInformation.ContractSize;
									}
								}
							}
							this.UpdateToReport1_TFEX(dataRow, num6);
						}
					}
					string empty2 = string.Empty;
					this.sortGridTfexSumm.Records(0).Fields("last").Text = "Total";
					this.sortGridTfexSumm.Records(0).Fields("mkt_val").Text = Utilities.PriceFormat(this._tfexTotMarketVal, 0, "0");
					this.sortGridTfexSumm.Records(0).Fields("cost_val").Text = Utilities.PriceFormat(this._tfexCost, 0, "0");
					this.sortGridTfexSumm.Records(0).Fields("unreal_settle").Text = Utilities.PriceFormat(this._tfexUnrealSettle, 0, "0");
					this.sortGridTfexSumm.Records(0).Fields("unreal_cost").Text = Utilities.PriceFormat(this._tfexUnrealCost, 0, "0");
					this.sortGridTfexSumm.Records(0).Fields("realize").Text = Utilities.PriceFormat(this._tfexReal, 0, "0");
					this.sortGridTfexSumm.Records(0).Fields("last").FontColor = Color.LightGray;
					this.sortGridTfexSumm.Records(0).Fields("mkt_val").FontColor = Color.Yellow;
					this.sortGridTfexSumm.Records(0).Fields("cost_val").FontColor = Color.Yellow;
					this.sortGridTfexSumm.Records(0).Fields("unreal_settle").FontColor = Utilities.ComparePriceColor(this._tfexUnrealSettle, 0m);
					this.sortGridTfexSumm.Records(0).Fields("unreal_cost").FontColor = Utilities.ComparePriceColor(this._tfexUnrealCost, 0m);
					this.sortGridTfexSumm.Records(0).Fields("realize").FontColor = Utilities.ComparePriceColor(this._tfexReal, 0m);
					this.sortGridTfexSumm.Redraw();
				}
				if (this.tdsTfex != null)
				{
					this.tdsTfex.Clear();
				}
				this.sortGridTfex.Redraw();
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateCustomerDataToGrid_TFEX", ex);
			}
			finally
			{
				this.intzaCustHeadTfex.EndUpdate();
				if (!base.Visible)
				{
					base.Show();
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateToReport1_TFEX(DataRow dr, decimal realize)
		{
			try
			{
				SeriesList.SeriesInformation seriesInformation = ApplicationInfo.SeriesInfo[dr["Series"].ToString().Trim()];
				long num = 0L;
				long num2 = 0L;
				long num3 = 0L;
				decimal num4 = 0m;
				decimal num5 = 0m;
				decimal num6 = 0m;
				decimal num7 = 0m;
				decimal num8 = 0m;
				decimal num9 = 0m;
				decimal num10 = 0m;
				long.TryParse(dr["StartQty"].ToString(), out num3);
				long.TryParse(dr["CurrentQty"].ToString(), out num);
				long.TryParse(dr["SellableQty"].ToString(), out num2);
				decimal.TryParse(dr["CurrentAvgPrice"].ToString(), out num4);
				decimal.TryParse(dr["LastPrice"].ToString(), out num6);
				string text = string.Empty;
				decimal num11 = 0m;
				if (ApplicationInfo.SupportSerisysTFEX)
				{
					decimal.TryParse(dr["MktValue"].ToString(), out num5);
					decimal.TryParse(dr["CostValue"].ToString(), out num7);
					decimal.TryParse(dr["UnReal"].ToString(), out num8);
					decimal.TryParse(dr["Realize"].ToString(), out realize);
					if (dr["Side"].ToString() == "L")
					{
						text = "Long";
					}
					else
					{
						if (dr["Side"].ToString() == "S")
						{
							text = "Short";
						}
					}
				}
				else
				{
					decimal.TryParse(dr["MarkAvgPrice"].ToString(), out num9);
					decimal.TryParse(dr["FixingPrice"].ToString(), out num10);
					string a = string.Empty;
					if (seriesInformation.MarketId == "TXI")
					{
						a = ApplicationInfo.IndexInfoTfex.TXIState;
					}
					else
					{
						if (seriesInformation.MarketId == "TXM")
						{
							a = ApplicationInfo.IndexInfoTfex.TXMState;
						}
						else
						{
							if (seriesInformation.MarketId == "TXC")
							{
								a = ApplicationInfo.IndexInfoTfex.TXCState;
							}
							else
							{
								if (seriesInformation.MarketId == "TXE")
								{
									a = ApplicationInfo.IndexInfoTfex.TXEState;
								}
								else
								{
									if (seriesInformation.MarketId == "TXR")
									{
										a = ApplicationInfo.IndexInfoTfex.TXRState;
									}
									else
									{
										if (seriesInformation.MarketId == "TXS")
										{
											a = ApplicationInfo.IndexInfoTfex.TXSState;
										}
									}
								}
							}
						}
					}
					decimal num12;
					if (a == "3C")
					{
						if (seriesInformation.FixPrice > 0m)
						{
							num12 = seriesInformation.FixPrice;
							num6 = seriesInformation.FixPrice;
						}
						else
						{
							if (num6 > 0m)
							{
								num12 = num6;
							}
							else
							{
								num12 = seriesInformation.PrevFixPrice;
								num6 = seriesInformation.PrevFixPrice;
							}
						}
					}
					else
					{
						if (num6 > 0m)
						{
							num12 = num6;
						}
						else
						{
							num12 = seriesInformation.PrevFixPrice;
							num6 = seriesInformation.PrevFixPrice;
						}
					}
					num7 = num2 * num4 * seriesInformation.ContractSize;
					num5 = num2 * num6 * seriesInformation.ContractSize;
					decimal num13 = num2 * num4;
					if (dr["Side"].ToString() == "L")
					{
						text = "Long";
						num8 = (num12 - num9) * num2 * seriesInformation.ContractSize;
						num11 = (num12 - num4) * num2 * seriesInformation.ContractSize;
					}
					else
					{
						if (dr["Side"].ToString() == "S")
						{
							text = "Short";
							num8 = (num9 - num12) * num2 * seriesInformation.ContractSize;
							num11 = (num4 - num12) * num2 * seriesInformation.ContractSize;
						}
					}
				}
				Color color = Color.Yellow;
				RecordItem recordItem = this.sortGridTfex.AddRecord(-1, false);
				recordItem.Fields("series").Text = dr["Series"].ToString().Trim();
				recordItem.Fields("side").Text = text;
				recordItem.Fields("start_vol").Text = Utilities.VolumeFormat(num3, true);
				recordItem.Fields("onhand").Text = Utilities.VolumeFormat(num, true);
				recordItem.Fields("sellable").Text = Utilities.VolumeFormat(num2, true);
				recordItem.Fields("cost_avg").Text = Utilities.PriceFormat(num4, 4);
				recordItem.Fields("cost_settle").Text = Utilities.PriceFormat(num9, 4);
				recordItem.Fields("last").Text = Utilities.PriceFormat(num6);
				recordItem.Fields("mkt_val").Text = Utilities.PriceFormat(num5);
				recordItem.Fields("cost_val").Text = Utilities.VolumeFormat(num7, true);
				recordItem.Fields("unreal_settle").Text = Utilities.VolumeFormat(num8, true);
				recordItem.Fields("unreal_cost").Text = Utilities.VolumeFormat(num11, true);
				recordItem.Fields("realize").Text = Utilities.VolumeFormat(realize, true);
				recordItem.Fields("series").FontColor = ((dr["Side"].ToString() == "L") ? Color.Cyan : Color.Magenta);
				recordItem.Fields("side").FontColor = ((dr["Side"].ToString() == "L") ? Color.Cyan : Color.Magenta);
				recordItem.Fields("start_vol").FontColor = Color.Yellow;
				recordItem.Fields("onhand").FontColor = Color.Yellow;
				recordItem.Fields("sellable").FontColor = Color.Yellow;
				color = Utilities.ComparePriceCFColor(num9, seriesInformation);
				recordItem.Fields("cost_settle").FontColor = Color.Yellow;
				color = Utilities.ComparePriceCFColor(num4, seriesInformation);
				recordItem.Fields("cost_avg").FontColor = Color.Yellow;
				recordItem.Fields("cost_val").FontColor = Color.Yellow;
				color = Utilities.ComparePriceCFColor(num6, seriesInformation);
				recordItem.Fields("last").FontColor = Color.Yellow;
				recordItem.Fields("mkt_val").FontColor = Color.Yellow;
				recordItem.Fields("unreal_settle").FontColor = Utilities.ComparePriceColor(num8, 0m);
				recordItem.Fields("unreal_cost").FontColor = Utilities.ComparePriceColor(num11, 0m);
				recordItem.Fields("realize").FontColor = Utilities.ComparePriceColor(realize, 0m);
				this._tfexTotMarketVal += num5;
				this._tfexCost += num7;
				this._tfexUnrealSettle += num8;
				this._tfexUnrealCost += num11;
				this._tfexReal += realize;
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateToReport1_TFEX", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateToBottomControl_TFEX()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(this.UpdateToBottomControl_TFEX));
			}
			else
			{
				try
				{
					this.intzaCustBottTfex.BeginUpdate();
					this.intzaCustBottTfex.ClearAllText();
					if (this.tdsTfex != null && this.tdsTfex.Tables.Contains("ITDSD_Get_Cust_MarginInfo"))
					{
						decimal num = 0m;
						decimal num2 = 0m;
						decimal num3 = 0m;
						decimal num4 = 0m;
						decimal num5 = 0m;
						decimal num6 = 0m;
						string str = string.Empty;
						foreach (DataRow dataRow in this.tdsTfex.Tables["ITDSD_Get_Cust_MarginInfo"].Rows)
						{
							decimal.TryParse(dataRow["EquityBal"].ToString(), out num);
							decimal.TryParse(dataRow["EE"].ToString(), out num2);
							decimal.TryParse(dataRow["PrevFloatPL"].ToString(), out num3);
							decimal.TryParse(dataRow["UnrealPLFuture"].ToString(), out num4);
							decimal.TryParse(dataRow["MarginBal"].ToString(), out num5);
							decimal.TryParse(dataRow["CallForceAmount"].ToString(), out num6);
							if (string.IsNullOrEmpty(dataRow["CallForceFlag"].ToString().Trim()))
							{
								str = "Normal";
							}
							else
							{
								str = dataRow["CallForceFlag"].ToString().Trim();
							}
							if (dataRow["PortType"].ToString().Trim() == "1")
							{
								this.intzaCustBottTfex.Item("tbEquityBalancePrevious").Text = Utilities.PriceFormat(num);
								this.intzaCustBottTfex.Item("tbEquityBalancePrevious").FontColor = Utilities.ComparePriceColor(num, 0m);
								this.intzaCustBottTfex.Item("tbEEBalancePrevious").Text = Utilities.PriceFormat(num2);
								this.intzaCustBottTfex.Item("tbEEBalancePrevious").FontColor = Utilities.ComparePriceColor(num2, 0m);
								this.intzaCustBottTfex.Item("tbUnrealizedPLPrevious").Text = Utilities.PriceFormat(num3);
								this.intzaCustBottTfex.Item("tbUnrealizedPLPrevious").FontColor = Utilities.ComparePriceColor(num3, 0m);
								this.intzaCustBottTfex.Item("tbMarginBalancePrevious").Text = Utilities.PriceFormat(num5);
								this.intzaCustBottTfex.Item("tbMarginBalancePrevious").FontColor = Utilities.ComparePriceColor(num5, 0m);
								this.intzaCustBottTfex.Item("tbCallForcePrevious").Text = str + " / " + Utilities.PriceFormat(num6);
								this.intzaCustBottTfex.Item("tbCallForcePrevious").FontColor = Utilities.ComparePriceColor(num6, 0m);
								this.intzaCustHeadTfex.Item("tbDepositWithdraw").Text = Utilities.PriceFormat(dataRow["DepositWithdraw"].ToString());
							}
							else
							{
								if (dataRow["PortType"].ToString().Trim() == "3")
								{
									this.intzaCustBottTfex.Item("tbEquityBalanceCurrent").Text = Utilities.PriceFormat(num);
									this.intzaCustBottTfex.Item("tbEquityBalanceCurrent").FontColor = Utilities.ComparePriceColor(num, 0m);
									this.intzaCustBottTfex.Item("tbEEBalanceCurrent").Text = Utilities.PriceFormat(num2);
									this.intzaCustBottTfex.Item("tbEEBalanceCurrent").FontColor = Utilities.ComparePriceColor(num2, 0m);
									this.intzaCustBottTfex.Item("tbMarginBalanceCurrent").Text = Utilities.PriceFormat(num5);
									this.intzaCustBottTfex.Item("tbMarginBalanceCurrent").FontColor = Utilities.ComparePriceColor(num5, 0m);
									this.intzaCustBottTfex.Item("tbCallForceCurrent").Text = str + " / " + Utilities.PriceFormat(num6);
									this.intzaCustBottTfex.Item("tbCallForceCurrent").FontColor = Utilities.ComparePriceColor(num6, 0m);
								}
								else
								{
									if (dataRow["PortType"].ToString().Trim() == "2")
									{
										this.intzaCustBottTfex.Item("tbEquityBalanceCurrentPort").Text = Utilities.PriceFormat(num);
										this.intzaCustBottTfex.Item("tbEquityBalanceCurrentPort").FontColor = Utilities.ComparePriceColor(num, 0m);
										this.intzaCustBottTfex.Item("tbEEBalanceCurerntPort").Text = Utilities.PriceFormat(num2);
										this.intzaCustBottTfex.Item("tbEEBalanceCurerntPort").FontColor = Utilities.ComparePriceColor(num2, 0m);
										this.intzaCustBottTfex.Item("tbUnrealizedPLCurrentPort").Text = Utilities.PriceFormat(num4);
										this.intzaCustBottTfex.Item("tbUnrealizedPLCurrentPort").FontColor = Utilities.ComparePriceColor(num4, 0m);
										this.intzaCustBottTfex.Item("tbMarginBalanceCurrentPort").Text = Utilities.PriceFormat(num5);
										this.intzaCustBottTfex.Item("tbMarginBalanceCurrentPort").FontColor = Utilities.ComparePriceColor(num5, 0m);
										this.intzaCustBottTfex.Item("tbCallForceCurrentPort").Text = str + " / " + Utilities.PriceFormat(num6);
										this.intzaCustBottTfex.Item("tbCallForceCurrentPort").FontColor = Utilities.ComparePriceColor(num6, 0m);
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
				finally
				{
					this.intzaCustBottTfex.EndUpdate();
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void sortGridTfex_TableMouseClick(object sender, TableMouseEventArgs e)
		{
			if (e.RowIndex == -1)
			{
				if (e.Column.Name == "unreal_settle")
				{
					this.sortGridTfex.GetColumn("unreal_settle").Visible = false;
					this.sortGridTfex.GetColumn("cost_settle").Visible = false;
					this.sortGridTfex.GetColumn("unreal_cost").Visible = true;
					this.sortGridTfex.GetColumn("cost_avg").Visible = true;
					this.sortGridTfexSumm.GetColumn("unreal_settle").Visible = false;
					this.sortGridTfexSumm.GetColumn("unreal_cost").Visible = true;
					this.sortGridTfex.Redraw();
					this.sortGridTfexSumm.Redraw();
				}
				else
				{
					if (e.Column.Name == "unreal_cost")
					{
						this.sortGridTfex.GetColumn("unreal_settle").Visible = true;
						this.sortGridTfex.GetColumn("cost_settle").Visible = true;
						this.sortGridTfex.GetColumn("unreal_cost").Visible = false;
						this.sortGridTfex.GetColumn("cost_avg").Visible = false;
						this.sortGridTfexSumm.GetColumn("unreal_cost").Visible = false;
						this.sortGridTfexSumm.GetColumn("unreal_settle").Visible = true;
						this.sortGridTfex.Redraw();
						this.sortGridTfexSumm.Redraw();
					}
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateToConfirmByStock()
		{
			try
			{
				string name = "view_confirm_bystock";
				if (this._subReportType == "Confrim Summary")
				{
					name = "view_customer_confirm";
				}
				if (this.tdsR8.Tables.Contains(name) && this.tdsR8.Tables[name].Rows.Count > 0)
				{
					List<frmPortfolio.RecViewCustomerConfirmSummary> list = new List<frmPortfolio.RecViewCustomerConfirmSummary>();
					List<frmPortfolio.RecViewCustomerConfirmSummary> list2 = new List<frmPortfolio.RecViewCustomerConfirmSummary>();
					List<frmPortfolio.RecViewCustomerConfirmSummary> list3 = new List<frmPortfolio.RecViewCustomerConfirmSummary>();
					List<frmPortfolio.RecViewCustomerConfirmSummary> list4 = new List<frmPortfolio.RecViewCustomerConfirmSummary>();
					foreach (DataRow dataRow in this.tdsR8.Tables[name].Rows)
					{
						frmPortfolio.RecViewCustomerConfirmSummary item = default(frmPortfolio.RecViewCustomerConfirmSummary);
						item.Side = dataRow["side"].ToString();
						if (ApplicationInfo.SupportFreewill)
						{
							if (dataRow["stock"].ToString().IndexOf(":") > -1)
							{
								item.Stock = dataRow["stock"].ToString().Replace(":", "");
								item.TTF = "2";
							}
							else
							{
								if (dataRow["stock"].ToString().IndexOf("+") > -1)
								{
									item.Stock = dataRow["stock"].ToString().Replace("+", "");
									item.TTF = "1";
								}
								else
								{
									item.Stock = dataRow["stock"].ToString();
									item.TTF = "";
								}
							}
						}
						else
						{
							item.Stock = dataRow["stock"].ToString();
							item.TTF = dataRow["trustee_id"].ToString();
						}
						long.TryParse(dataRow["volume"].ToString(), out item.Volume);
						decimal.TryParse(dataRow["price"].ToString(), out item.Price);
						decimal.TryParse(dataRow["amount"].ToString(), out item.Amount);
						decimal.TryParse(dataRow["Comm_Vat"].ToString(), out item.CommVat);
						if (dataRow["side"].ToString().ToLower() == "b")
						{
							item.NetAmnt = item.Amount + item.CommVat;
							list.Add(item);
						}
						else
						{
							if (dataRow["side"].ToString().ToLower() == "s")
							{
								item.NetAmnt = item.Amount - item.CommVat;
								list2.Add(item);
							}
							else
							{
								if (dataRow["side"].ToString().ToLower() == "c")
								{
									item.NetAmnt = item.Amount + item.CommVat;
									list3.Add(item);
								}
								else
								{
									if (dataRow["side"].ToString().ToLower() == "h")
									{
										item.NetAmnt = item.Amount - item.CommVat;
										list4.Add(item);
									}
								}
							}
						}
					}
					if (this._subReportType == "Confrim by Stock")
					{
						this.UpdateToReport9(list, list2, list3, list4);
					}
					else
					{
						this.UpdateToReport6(list, list2, list3, list4);
					}
					this.ShowCommSlide(this.tdsR8);
					list.Clear();
					list2.Clear();
					list3.Clear();
					list4.Clear();
					list = null;
					list2 = null;
					list3 = null;
					list4 = null;
				}
				this.tdsR8.Clear();
			}
			catch (Exception ex)
			{
				this.ShowError("bgwLoadReport_RunWorkerCompleted", ex);
			}
			finally
			{
				base.IsLoadingData = false;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateToReport9(List<frmPortfolio.RecViewCustomerConfirmSummary> recB, List<frmPortfolio.RecViewCustomerConfirmSummary> recS, List<frmPortfolio.RecViewCustomerConfirmSummary> recC, List<frmPortfolio.RecViewCustomerConfirmSummary> recH)
		{
			try
			{
				decimal num = 0m;
				decimal num2 = 0m;
				decimal num3 = 0m;
				decimal num4 = 0m;
				decimal num5 = 0m;
				decimal num6 = 0m;
				decimal num7 = 0m;
				decimal num8 = 0m;
				decimal num9 = 0m;
				decimal num10 = 0m;
				decimal num11 = 0m;
				decimal num12 = 0m;
				decimal num13 = 0m;
				decimal num14 = 0m;
				decimal num15 = 0m;
				decimal price = 0m;
				string empty = string.Empty;
				string text = string.Empty;
				long num16 = 0L;
				if (recB.Count > 0)
				{
					for (int i = 0; i < recB.Count; i++)
					{
						if (string.IsNullOrEmpty(text))
						{
							text = recB[i].Stock;
						}
						if (text != recB[i].Stock)
						{
							if (num16 > 0L)
							{
								price = num13 / num16;
							}
							this.UpdateR9toGrid("", "*** Sub Total ***", "", num16, price, num13, num14, num15, Color.LightGray);
							num += num13;
							num2 += num14;
							num3 += num15;
							num13 = 0m;
							num14 = 0m;
							num15 = 0m;
							num16 = 0L;
							price = 0m;
							text = recB[i].Stock;
						}
						num13 += recB[i].Amount;
						num14 += Math.Round(recB[i].CommVat, 2, MidpointRounding.AwayFromZero);
						num15 += Math.Round(recB[i].NetAmnt, 2, MidpointRounding.AwayFromZero);
						num16 += recB[i].Volume;
						this.UpdateR9toGrid(recB[i].Side, recB[i].Stock, recB[i].TTF, recB[i].Volume, recB[i].Price, recB[i].Amount, recB[i].CommVat, recB[i].NetAmnt, Color.Lime);
					}
					if (num16 > 0L)
					{
						price = num13 / num16;
					}
					this.UpdateR9toGrid("", "*** Sub Total ***", "", num16, price, num13, num14, num15, Color.LightGray);
					num += num13;
					num2 += Math.Round(num14, 2, MidpointRounding.AwayFromZero);
					num3 += Math.Round(num15, 2, MidpointRounding.AwayFromZero);
					this.UpdateR9toGrid("", "*** TOTAL BOUGHT **", "", 0L, 0m, num, num2, num3, Color.Lime);
				}
				text = string.Empty;
				num13 = 0m;
				num14 = 0m;
				num15 = 0m;
				num16 = 0L;
				price = 0m;
				if (recS.Count > 0)
				{
					for (int i = 0; i < recS.Count; i++)
					{
						if (string.IsNullOrEmpty(text))
						{
							text = recS[i].Stock;
						}
						if (text != recS[i].Stock)
						{
							if (num16 > 0L)
							{
								price = num13 / num16;
							}
							this.UpdateR9toGrid("", "*** Sub Total ***", "", num16, price, num13, num14, num15, Color.LightGray);
							num4 += num13;
							num5 += num14;
							num6 += num15;
							num13 = 0m;
							num14 = 0m;
							num15 = 0m;
							num16 = 0L;
							price = 0m;
							text = recS[i].Stock;
						}
						num13 += recS[i].Amount;
						num14 += Math.Round(recS[i].CommVat, 2, MidpointRounding.AwayFromZero);
						num15 += Math.Round(recS[i].NetAmnt, 2, MidpointRounding.AwayFromZero);
						num16 += recS[i].Volume;
						this.UpdateR9toGrid(recS[i].Side, recS[i].Stock, recS[i].TTF, recS[i].Volume, recS[i].Price, recS[i].Amount, recS[i].CommVat, recS[i].NetAmnt, Color.Red);
					}
					if (num16 > 0L)
					{
						price = num13 / num16;
					}
					this.UpdateR9toGrid("", "*** Sub Total ***", "", num16, price, num13, num14, num15, Color.LightGray);
					num4 += num13;
					num5 += Math.Round(num14, 2, MidpointRounding.AwayFromZero);
					num6 += Math.Round(num15, 2, MidpointRounding.AwayFromZero);
					this.UpdateR9toGrid("", "*** TOTAL SOLD ***", "", 0L, 0m, num4, num5, num6, Color.Red);
				}
				text = string.Empty;
				num13 = 0m;
				num14 = 0m;
				num15 = 0m;
				num16 = 0L;
				price = 0m;
				if (recC.Count > 0)
				{
					for (int i = 0; i < recC.Count; i++)
					{
						if (string.IsNullOrEmpty(text))
						{
							text = recC[i].Stock;
						}
						if (text != recC[i].Stock)
						{
							if (num16 > 0L)
							{
								price = num13 / num16;
							}
							this.UpdateR9toGrid("", "*** Sub Total ***", "", num16, price, num13, num14, num15, Color.LightGray);
							num7 += num13;
							num8 += num14;
							num9 += num15;
							num13 = 0m;
							num14 = 0m;
							num15 = 0m;
							num16 = 0L;
							price = 0m;
							text = recC[i].Stock;
						}
						num13 += recC[i].Amount;
						num14 += Math.Round(recC[i].CommVat, 2, MidpointRounding.AwayFromZero);
						num15 += Math.Round(recC[i].NetAmnt, 2, MidpointRounding.AwayFromZero);
						num16 += recC[i].Volume;
						this.UpdateR9toGrid(recC[i].Side, recC[i].Stock, recC[i].TTF, recC[i].Volume, recC[i].Price, recC[i].Amount, recC[i].CommVat, recC[i].NetAmnt, Color.Cyan);
					}
					if (num16 > 0L)
					{
						price = num13 / num16;
					}
					this.UpdateR9toGrid("", "*** Sub Total ***", "", num16, price, num13, num14, num15, Color.LightGray);
					num7 += num13;
					num8 += Math.Round(num14, 2, MidpointRounding.AwayFromZero);
					num9 += Math.Round(num15, 2, MidpointRounding.AwayFromZero);
					this.UpdateR9toGrid("", "*** TOTAL COVER ***", "", 0L, 0m, num7, num8, num9, Color.Cyan);
				}
				text = string.Empty;
				num13 = 0m;
				num14 = 0m;
				num15 = 0m;
				num16 = 0L;
				price = 0m;
				if (recH.Count > 0)
				{
					for (int i = 0; i < recH.Count; i++)
					{
						if (string.IsNullOrEmpty(text))
						{
							text = recH[i].Stock;
						}
						if (text != recH[i].Stock)
						{
							if (num16 > 0L)
							{
								price = num13 / num16;
							}
							this.UpdateR9toGrid("", "*** Sub Total ***", "", num16, price, num13, num14, num15, Color.LightGray);
							num10 += num13;
							num11 += num14;
							num12 += num15;
							num13 = 0m;
							num14 = 0m;
							num15 = 0m;
							num16 = 0L;
							price = 0m;
							text = recH[i].Stock;
						}
						num13 += recH[i].Amount;
						num14 += Math.Round(recH[i].CommVat, 2, MidpointRounding.AwayFromZero);
						num15 += Math.Round(recH[i].NetAmnt, 2, MidpointRounding.AwayFromZero);
						num16 += recH[i].Volume;
						this.UpdateR9toGrid(recH[i].Side, recH[i].Stock, recH[i].TTF, recH[i].Volume, recH[i].Price, recH[i].Amount, recH[i].CommVat, recH[i].NetAmnt, Color.Magenta);
					}
					if (num16 > 0L)
					{
						price = num13 / num16;
					}
					this.UpdateR9toGrid("", "*** Sub Total ***", "", num16, price, num13, num14, num15, Color.LightGray);
					num10 += num13;
					num11 += Math.Round(num14, 2, MidpointRounding.AwayFromZero);
					num12 += Math.Round(num15, 2, MidpointRounding.AwayFromZero);
					this.UpdateR9toGrid("", "*** TOTAL SHORT ***", "", 0L, 0m, num10, num11, num12, Color.Magenta);
				}
				decimal num17 = num4 + num10 - (num + num7);
				decimal num18 = this.GetSpTotolCommVAT(this.tdsR8);
				if (num18 == 0m)
				{
					num18 = num2 + num5 + num8 + num11;
				}
				decimal num19 = num18;
				decimal netAmount = num17 - num19;
				this.UpdateR9toGrid("", "*** TOTAL NET ***", "", 0L, 0m, num17, num19, netAmount, Color.Orange);
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateToReport9", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateR9toGrid(string side, string stock, string ttf, long volume, decimal price, decimal amount, decimal commVAT, decimal netAmount, Color color)
		{
			try
			{
				RecordItem recordItem = this.intzaReport.AddRecord(-1, false);
				recordItem.Fields("side").Text = side;
				recordItem.Fields("stock").Text = stock;
				recordItem.Fields("ttf").Text = ttf;
				recordItem.Fields("volume").Text = Utilities.VolumeFormat(volume, true);
				recordItem.Fields("price").Text = price;
				recordItem.Fields("amount").Text = amount;
				recordItem.Fields("commvat").Text = commVAT;
				recordItem.Fields("netamount").Text = netAmount;
				recordItem.Fields("side").FontColor = color;
				recordItem.Fields("stock").FontColor = color;
				recordItem.Fields("ttf").FontColor = color;
				recordItem.Fields("volume").FontColor = color;
				recordItem.Fields("price").FontColor = color;
				recordItem.Fields("amount").FontColor = color;
				recordItem.Fields("commvat").FontColor = color;
				recordItem.Fields("netamount").FontColor = color;
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateR9toGrid", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateR9_2toGrid(string message, string subMessage, decimal value)
		{
			try
			{
				RecordItem recordItem = this.intzaReport.AddRecord(-1, false);
				recordItem.Fields("stock").Text = message;
				recordItem.Fields("volume").Text = subMessage;
				recordItem.Fields("price").Text = value;
				recordItem.Fields("stock").FontColor = Color.Yellow;
				recordItem.Fields("volume").FontColor = Color.Yellow;
				recordItem.Fields("price").FontColor = Color.Yellow;
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateR9_2toGrid", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateToReport6(List<frmPortfolio.RecViewCustomerConfirmSummary> recB, List<frmPortfolio.RecViewCustomerConfirmSummary> recS, List<frmPortfolio.RecViewCustomerConfirmSummary> recC, List<frmPortfolio.RecViewCustomerConfirmSummary> recH)
		{
			try
			{
				decimal num = 0m;
				decimal num2 = 0m;
				decimal num3 = 0m;
				decimal num4 = 0m;
				decimal num5 = 0m;
				decimal num6 = 0m;
				decimal num7 = 0m;
				decimal num8 = 0m;
				decimal num9 = 0m;
				decimal num10 = 0m;
				decimal num11 = 0m;
				decimal num12 = 0m;
				if (recB.Count > 0)
				{
					for (int i = 0; i < recB.Count; i++)
					{
						num += recB[i].Amount;
						num2 += Math.Round(recB[i].CommVat, 2, MidpointRounding.AwayFromZero);
						num3 += Math.Round(recB[i].NetAmnt, 2, MidpointRounding.AwayFromZero);
						this.UpdateR9toGrid(recB[i].Side, recB[i].Stock, recB[i].TTF, recB[i].Volume, recB[i].Price, recB[i].Amount, recB[i].CommVat, recB[i].NetAmnt, Color.Lime);
					}
					this.UpdateR9toGrid("", "*** TOTAL BOUGHT **", "", 0L, 0m, num, num2, num3, Color.Lime);
				}
				if (recS.Count > 0)
				{
					for (int i = 0; i < recS.Count; i++)
					{
						num4 += recS[i].Amount;
						num5 += Math.Round(recS[i].CommVat, 2, MidpointRounding.AwayFromZero);
						num6 += Math.Round(recS[i].NetAmnt, 2, MidpointRounding.AwayFromZero);
						this.UpdateR9toGrid(recS[i].Side, recS[i].Stock, recS[i].TTF, recS[i].Volume, recS[i].Price, recS[i].Amount, recS[i].CommVat, recS[i].NetAmnt, Color.Red);
					}
					this.UpdateR9toGrid("", "*** TOTAL SOLD ***", "", 0L, 0m, num4, num5, num6, Color.Red);
				}
				if (recC.Count > 0)
				{
					for (int i = 0; i < recC.Count; i++)
					{
						num7 += recC[i].Amount;
						num8 += Math.Round(recC[i].CommVat, 2, MidpointRounding.AwayFromZero);
						num9 += Math.Round(recC[i].NetAmnt, 2, MidpointRounding.AwayFromZero);
						this.UpdateR9toGrid(recC[i].Side, recC[i].Stock, recC[i].TTF, recC[i].Volume, recC[i].Price, recC[i].Amount, recC[i].CommVat, recC[i].NetAmnt, Color.Cyan);
					}
					this.UpdateR9toGrid("", "*** TOTAL COVER ***", "", 0L, 0m, num7, num8, num9, Color.Cyan);
				}
				if (recH.Count > 0)
				{
					for (int i = 0; i < recH.Count; i++)
					{
						num10 += recH[i].Amount;
						num11 += Math.Round(recH[i].CommVat, 2, MidpointRounding.AwayFromZero);
						num12 += Math.Round(recH[i].NetAmnt, 2, MidpointRounding.AwayFromZero);
						this.UpdateR9toGrid(recH[i].Side, recH[i].Stock, recH[i].TTF, recH[i].Volume, recH[i].Price, recH[i].Amount, recH[i].CommVat, recH[i].NetAmnt, Color.Magenta);
					}
					this.UpdateR9toGrid("", "*** TOTAL SHORT ***", "", 0L, 0m, num10, num11, num12, Color.Magenta);
				}
				decimal num13 = num4 + num10 - (num + num7);
				decimal num14 = this.GetSpTotolCommVAT(this.tdsR8);
				if (num14 == 0m)
				{
					num14 = num2 + num5 + num8 + num11;
				}
				decimal netAmount = num13 - num14;
				this.UpdateR9toGrid("", "*** TOTAL NET ***", "", 0L, 0m, num13, num14, netAmount, Color.Orange);
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateToReport9", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateToReport4(DataRow dr)
		{
			try
			{
				long num = 0L;
				long num2 = 0L;
				long num3 = 0L;
				decimal num4 = 0m;
				decimal num5 = 0m;
				decimal num6 = 0m;
				decimal num7 = 0m;
				decimal num8 = 0m;
				decimal d = 0m;
				decimal d2 = 0m;
				long.TryParse(dr["start_share"].ToString(), out num);
				long.TryParse(dr["buy_volume"].ToString(), out num2);
				long.TryParse(dr["sell_volume"].ToString(), out num3);
				decimal.TryParse(dr["sell_avg"].ToString(), out num4);
				decimal.TryParse(dr["cost_avg"].ToString(), out num5);
				decimal.TryParse(dr["sell_total_amount"].ToString(), out num6);
				decimal.TryParse(dr["cost_total_amount"].ToString(), out num7);
				if (ApplicationInfo.SupportFreewill)
				{
					decimal.TryParse(dr["realize_pl"].ToString(), out num8);
				}
				else
				{
					decimal.TryParse(dr["costCommVat"].ToString(), out d2);
					decimal.TryParse(dr["sellComVat"].ToString(), out d);
					if (ApplicationInfo.BrokerId == 2 || ApplicationInfo.BrokerId == 88)
					{
						if (dr["position_type"].ToString() == "S")
						{
							num8 = Math.Round(num7 - num6, 2, MidpointRounding.AwayFromZero);
						}
						else
						{
							num8 = Math.Round(num6 - num7, 2, MidpointRounding.AwayFromZero);
						}
					}
					else
					{
						if (dr["position_type"].ToString() == "S")
						{
							num8 = Math.Round(num7 - num6 - (d2 + d), 0, MidpointRounding.AwayFromZero);
						}
						else
						{
							num8 = Math.Round(num6 - num7 - (d2 + d), 0, MidpointRounding.AwayFromZero);
						}
					}
				}
				Color fontColor = Color.Yellow;
				if (num8 > 0m)
				{
					fontColor = Color.Lime;
				}
				else
				{
					if (num8 < 0m)
					{
						fontColor = Color.Red;
					}
				}
				RecordItem recordItem = this.intzaReport.AddRecord(-1, false);
				recordItem.Fields("stock").Text = dr["stock"].ToString().Trim();
				recordItem.Fields("stock").Tag = this.GetPositionType(dr["position_type"].ToString().Trim());
				recordItem.Fields("ttf").Text = ((dr["trustee_id"].ToString() == "0") ? string.Empty : dr["trustee_id"].ToString());
				if (dr["position_type"].ToString() != "B")
				{
					recordItem.Fields("start").Text = Utilities.VolumeFormat(num, true);
					recordItem.Fields("today_bh").Text = Utilities.VolumeFormat(num2, true);
					recordItem.Fields("today_sc").Text = Utilities.VolumeFormat(num3, true);
					recordItem.Fields("sc_avg").Text = Utilities.PriceFormat(num4, 4);
					recordItem.Fields("cost_avg").Text = Utilities.PriceFormat(num5, 4);
					recordItem.Fields("sc_amount").Text = Utilities.PriceFormat(num6);
					recordItem.Fields("cost_amount").Text = Utilities.PriceFormat(num7);
					recordItem.Fields("realize").Text = Utilities.PriceFormat(num8);
				}
				recordItem.Fields("stock").FontColor = fontColor;
				recordItem.Fields("ttf").FontColor = fontColor;
				recordItem.Fields("start").FontColor = fontColor;
				recordItem.Fields("today_bh").FontColor = fontColor;
				recordItem.Fields("today_sc").FontColor = fontColor;
				recordItem.Fields("sc_avg").FontColor = fontColor;
				recordItem.Fields("cost_avg").FontColor = fontColor;
				recordItem.Fields("sc_amount").FontColor = fontColor;
				recordItem.Fields("cost_amount").FontColor = fontColor;
				recordItem.Fields("realize").FontColor = fontColor;
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateToReport4", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowCommSlide(DataSet tds)
		{
			try
			{
				if (tds.Tables.Contains("RETURN") && tds.Tables["RETURN"].Rows.Count > 0)
				{
					DataRow dataRow = tds.Tables["RETURN"].Rows[0];
					this.UpdateR9toGrid("", "", "", 0L, 0m, 0m, 0m, 0m, Color.Yellow);
					decimal value;
					decimal.TryParse(dataRow["TotalCommission"].ToString(), out value);
					this.UpdateR9_2toGrid("COMMISSION", "", value);
					decimal.TryParse(dataRow["TotalTradingFee"].ToString(), out value);
					this.UpdateR9_2toGrid("TRADING FEE", "(" + this._tradingFee.ToString("0.0000") + "%)", value);
					decimal.TryParse(dataRow["TotalClearingFee"].ToString(), out value);
					this.UpdateR9_2toGrid("CLEARING FEE", "(" + this._clearingFee.ToString("0.0000") + "%)", value);
					decimal.TryParse(dataRow["TotalSettlementFee"].ToString(), out value);
					this.UpdateR9_2toGrid("SETTLEMENT FEE", "(" + this._settlementFee.ToString("0.00") + " BAHT)", value);
					decimal.TryParse(dataRow["TotalVAT"].ToString(), out value);
					this.UpdateR9_2toGrid("VAT", "(" + ApplicationInfo.UserVAT.ToString("0.00") + "%)", value);
					decimal.TryParse(dataRow["TotalCommVAT"].ToString(), out value);
					this.UpdateR9_2toGrid("COMMISSION & VAT", "", value);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("ShowCommSlide", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnCreditBalance_Click(object sender, EventArgs e)
		{
			this.setVisibleControl("Portfolio", "Credit Balance");
			this.SetResize(true);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void createColumns(string type)
		{
			try
			{
				this.intzaReport.Rows = 0;
				this.intzaReport.Columns.Clear();
				if (type == "Summary")
				{
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
					columnItem.Alignment = StringAlignment.Near;
					columnItem.BackColor = Color.FromArgb(64, 64, 64);
					columnItem.ColumnAlignment = StringAlignment.Center;
					columnItem.FontColor = Color.LightGray;
					columnItem.MyStyle = FontStyle.Regular;
					columnItem.Name = "stock";
					columnItem.Text = "Stock";
					columnItem.ValueFormat = FormatType.Symbol;
					columnItem.Visible = true;
					columnItem.Width = 13;
					columnItem2.Alignment = StringAlignment.Center;
					columnItem2.BackColor = Color.FromArgb(64, 64, 64);
					columnItem2.ColumnAlignment = StringAlignment.Center;
					columnItem2.FontColor = Color.LightGray;
					columnItem2.MyStyle = FontStyle.Regular;
					columnItem2.Name = "ttf";
					columnItem2.Text = "TTF";
					columnItem2.ValueFormat = FormatType.Text;
					columnItem2.Visible = true;
					columnItem2.Width = 4;
					columnItem3.Alignment = StringAlignment.Far;
					columnItem3.BackColor = Color.FromArgb(64, 64, 64);
					columnItem3.ColumnAlignment = StringAlignment.Center;
					columnItem3.FontColor = Color.LightGray;
					columnItem3.MyStyle = FontStyle.Regular;
					columnItem3.Name = "onhand";
					columnItem3.Text = "OnHand";
					columnItem3.ValueFormat = FormatType.Text;
					columnItem3.Visible = true;
					columnItem3.Width = 10;
					columnItem4.Alignment = StringAlignment.Far;
					columnItem4.BackColor = Color.FromArgb(64, 64, 64);
					columnItem4.ColumnAlignment = StringAlignment.Center;
					columnItem4.FontColor = Color.LightGray;
					columnItem4.MyStyle = FontStyle.Regular;
					columnItem4.Name = "sellable";
					columnItem4.Text = "Sellable";
					columnItem4.ValueFormat = FormatType.Text;
					columnItem4.Visible = true;
					columnItem4.Width = 10;
					columnItem5.Alignment = StringAlignment.Far;
					columnItem5.BackColor = Color.FromArgb(64, 64, 64);
					columnItem5.ColumnAlignment = StringAlignment.Center;
					columnItem5.FontColor = Color.LightGray;
					columnItem5.MyStyle = FontStyle.Regular;
					columnItem5.Name = "avg";
					columnItem5.Text = "Avg";
					columnItem5.ValueFormat = FormatType.Text;
					columnItem5.Visible = true;
					columnItem5.Width = 9;
					columnItem6.Alignment = StringAlignment.Far;
					columnItem6.BackColor = Color.FromArgb(64, 64, 64);
					columnItem6.ColumnAlignment = StringAlignment.Center;
					columnItem6.FontColor = Color.LightGray;
					columnItem6.MyStyle = FontStyle.Regular;
					columnItem6.Name = "last";
					columnItem6.Text = "Last";
					columnItem6.ValueFormat = FormatType.Text;
					columnItem6.Visible = true;
					columnItem6.Width = 7;
					columnItem7.Alignment = StringAlignment.Far;
					columnItem7.BackColor = Color.FromArgb(64, 64, 64);
					columnItem7.ColumnAlignment = StringAlignment.Center;
					columnItem7.FontColor = Color.LightGray;
					columnItem7.MyStyle = FontStyle.Regular;
					columnItem7.Name = "cost";
					columnItem7.Text = "Cost";
					columnItem7.ValueFormat = FormatType.Text;
					columnItem7.Visible = true;
					columnItem7.Width = 10;
					columnItem8.Alignment = StringAlignment.Far;
					columnItem8.BackColor = Color.FromArgb(64, 64, 64);
					columnItem8.ColumnAlignment = StringAlignment.Center;
					columnItem8.FontColor = Color.LightGray;
					columnItem8.MyStyle = FontStyle.Regular;
					columnItem8.Name = "curr_value";
					columnItem8.Text = "Curr Val";
					columnItem8.ValueFormat = FormatType.Text;
					columnItem8.Visible = true;
					columnItem8.Width = 10;
					columnItem9.Alignment = StringAlignment.Far;
					columnItem9.BackColor = Color.FromArgb(64, 64, 64);
					columnItem9.ColumnAlignment = StringAlignment.Center;
					columnItem9.FontColor = Color.LightGray;
					columnItem9.MyStyle = FontStyle.Regular;
					columnItem9.Name = "unreal_pct";
					columnItem9.Text = "%Unrl";
					columnItem9.ValueFormat = FormatType.Text;
					columnItem9.Visible = true;
					columnItem9.Width = 7;
					columnItem10.Alignment = StringAlignment.Far;
					columnItem10.BackColor = Color.FromArgb(64, 64, 64);
					columnItem10.ColumnAlignment = StringAlignment.Center;
					columnItem10.FontColor = Color.LightGray;
					columnItem10.MyStyle = FontStyle.Regular;
					columnItem10.Name = "unreal";
					columnItem10.Text = "Unrl P/L";
					columnItem10.ValueFormat = FormatType.Text;
					columnItem10.Visible = true;
					columnItem10.Width = 10;
					columnItem11.Alignment = StringAlignment.Far;
					columnItem11.BackColor = Color.FromArgb(64, 64, 64);
					columnItem11.ColumnAlignment = StringAlignment.Center;
					columnItem11.FontColor = Color.LightGray;
					columnItem11.MyStyle = FontStyle.Regular;
					columnItem11.Name = "realize";
					columnItem11.Text = "Real P/L";
					columnItem11.ValueFormat = FormatType.Text;
					columnItem11.Visible = true;
					columnItem11.Width = 10;
					this.intzaReport.Columns.Add(columnItem);
					this.intzaReport.Columns.Add(columnItem2);
					this.intzaReport.Columns.Add(columnItem3);
					this.intzaReport.Columns.Add(columnItem4);
					this.intzaReport.Columns.Add(columnItem5);
					this.intzaReport.Columns.Add(columnItem6);
					this.intzaReport.Columns.Add(columnItem7);
					this.intzaReport.Columns.Add(columnItem8);
					this.intzaReport.Columns.Add(columnItem9);
					this.intzaReport.Columns.Add(columnItem10);
					this.intzaReport.Columns.Add(columnItem11);
				}
				else
				{
					if (type == "Confrim by Stock")
					{
						ColumnItem columnItem12 = new ColumnItem();
						ColumnItem columnItem13 = new ColumnItem();
						ColumnItem columnItem14 = new ColumnItem();
						ColumnItem columnItem15 = new ColumnItem();
						ColumnItem columnItem16 = new ColumnItem();
						ColumnItem columnItem17 = new ColumnItem();
						ColumnItem columnItem18 = new ColumnItem();
						ColumnItem columnItem19 = new ColumnItem();
						columnItem12.Alignment = StringAlignment.Center;
						columnItem12.BackColor = Color.FromArgb(64, 64, 64);
						columnItem12.ColumnAlignment = StringAlignment.Center;
						columnItem12.FontColor = Color.LightGray;
						columnItem12.MyStyle = FontStyle.Regular;
						columnItem12.Name = "side";
						columnItem12.Text = "Side";
						columnItem12.ValueFormat = FormatType.Text;
						columnItem12.Visible = true;
						columnItem12.Width = 6;
						columnItem13.Alignment = StringAlignment.Near;
						columnItem13.BackColor = Color.FromArgb(64, 64, 64);
						columnItem13.ColumnAlignment = StringAlignment.Center;
						columnItem13.FontColor = Color.LightGray;
						columnItem13.MyStyle = FontStyle.Regular;
						columnItem13.Name = "stock";
						columnItem13.Text = "Stock";
						columnItem13.ValueFormat = FormatType.Text;
						columnItem13.Visible = true;
						columnItem13.Width = 19;
						columnItem14.Alignment = StringAlignment.Center;
						columnItem14.BackColor = Color.FromArgb(64, 64, 64);
						columnItem14.ColumnAlignment = StringAlignment.Center;
						columnItem14.FontColor = Color.LightGray;
						columnItem14.MyStyle = FontStyle.Regular;
						columnItem14.Name = "ttf";
						columnItem14.Text = "TTF";
						columnItem14.ValueFormat = FormatType.Volume;
						columnItem14.Visible = true;
						columnItem14.Width = 6;
						columnItem15.Alignment = StringAlignment.Far;
						columnItem15.BackColor = Color.FromArgb(64, 64, 64);
						columnItem15.ColumnAlignment = StringAlignment.Center;
						columnItem15.FontColor = Color.LightGray;
						columnItem15.MyStyle = FontStyle.Regular;
						columnItem15.Name = "volume";
						columnItem15.Text = "Volume";
						columnItem15.ValueFormat = FormatType.Text;
						columnItem15.Visible = true;
						columnItem15.Width = 15;
						columnItem16.Alignment = StringAlignment.Far;
						columnItem16.BackColor = Color.FromArgb(64, 64, 64);
						columnItem16.ColumnAlignment = StringAlignment.Center;
						columnItem16.FontColor = Color.LightGray;
						columnItem16.MyStyle = FontStyle.Regular;
						columnItem16.Name = "price";
						columnItem16.Text = "Price";
						columnItem16.ValueFormat = FormatType.Price;
						columnItem16.Visible = true;
						columnItem16.Width = 10;
						columnItem17.Alignment = StringAlignment.Far;
						columnItem17.BackColor = Color.FromArgb(64, 64, 64);
						columnItem17.ColumnAlignment = StringAlignment.Center;
						columnItem17.FontColor = Color.LightGray;
						columnItem17.MyStyle = FontStyle.Regular;
						columnItem17.Name = "amount";
						columnItem17.Text = "Amount";
						columnItem17.ValueFormat = FormatType.Price;
						columnItem17.Visible = true;
						columnItem17.Width = 16;
						columnItem18.Alignment = StringAlignment.Far;
						columnItem18.BackColor = Color.FromArgb(64, 64, 64);
						columnItem18.ColumnAlignment = StringAlignment.Center;
						columnItem18.FontColor = Color.LightGray;
						columnItem18.MyStyle = FontStyle.Regular;
						columnItem18.Name = "commvat";
						columnItem18.Text = "Comm+VAT";
						columnItem18.ValueFormat = FormatType.Price;
						columnItem18.Visible = true;
						columnItem18.Width = 12;
						columnItem19.Alignment = StringAlignment.Far;
						columnItem19.BackColor = Color.FromArgb(64, 64, 64);
						columnItem19.ColumnAlignment = StringAlignment.Center;
						columnItem19.FontColor = Color.LightGray;
						columnItem19.MyStyle = FontStyle.Regular;
						columnItem19.Name = "netamount";
						columnItem19.Text = "Net Amt";
						columnItem19.ValueFormat = FormatType.Price;
						columnItem19.Visible = true;
						columnItem19.Width = 16;
						this.intzaReport.Columns.Add(columnItem12);
						this.intzaReport.Columns.Add(columnItem13);
						this.intzaReport.Columns.Add(columnItem14);
						this.intzaReport.Columns.Add(columnItem15);
						this.intzaReport.Columns.Add(columnItem16);
						this.intzaReport.Columns.Add(columnItem17);
						this.intzaReport.Columns.Add(columnItem18);
						this.intzaReport.Columns.Add(columnItem19);
					}
					else
					{
						if (type == "Realize Profit/Loss")
						{
							ColumnItem columnItem = new ColumnItem();
							ColumnItem columnItem2 = new ColumnItem();
							ColumnItem columnItem20 = new ColumnItem();
							ColumnItem columnItem3 = new ColumnItem();
							ColumnItem columnItem4 = new ColumnItem();
							ColumnItem columnItem5 = new ColumnItem();
							ColumnItem columnItem6 = new ColumnItem();
							ColumnItem columnItem7 = new ColumnItem();
							ColumnItem columnItem8 = new ColumnItem();
							ColumnItem columnItem9 = new ColumnItem();
							columnItem.Alignment = StringAlignment.Near;
							columnItem.BackColor = Color.FromArgb(64, 64, 64);
							columnItem.ColumnAlignment = StringAlignment.Center;
							columnItem.FontColor = Color.LightGray;
							columnItem.MyStyle = FontStyle.Regular;
							columnItem.Name = "stock";
							columnItem.Text = "Stock";
							columnItem.ValueFormat = FormatType.Symbol;
							columnItem.Visible = true;
							columnItem.Width = 13;
							columnItem2.Alignment = StringAlignment.Center;
							columnItem2.BackColor = Color.FromArgb(64, 64, 64);
							columnItem2.ColumnAlignment = StringAlignment.Center;
							columnItem2.FontColor = Color.LightGray;
							columnItem2.MyStyle = FontStyle.Regular;
							columnItem2.Name = "ttf";
							columnItem2.Text = "TTF";
							columnItem2.ValueFormat = FormatType.Text;
							columnItem2.Visible = true;
							columnItem2.Width = 4;
							columnItem20.Alignment = StringAlignment.Far;
							columnItem20.BackColor = Color.FromArgb(64, 64, 64);
							columnItem20.ColumnAlignment = StringAlignment.Center;
							columnItem20.FontColor = Color.LightGray;
							columnItem20.MyStyle = FontStyle.Regular;
							columnItem20.Name = "start";
							columnItem20.Text = "Start";
							columnItem20.ValueFormat = FormatType.Text;
							columnItem20.Visible = true;
							columnItem20.Width = 10;
							columnItem3.Alignment = StringAlignment.Far;
							columnItem3.BackColor = Color.FromArgb(64, 64, 64);
							columnItem3.ColumnAlignment = StringAlignment.Center;
							columnItem3.FontColor = Color.LightGray;
							columnItem3.MyStyle = FontStyle.Regular;
							columnItem3.Name = "today_bh";
							columnItem3.Text = "B/H Today";
							columnItem3.ValueFormat = FormatType.Text;
							columnItem3.Visible = true;
							columnItem3.Width = 10;
							columnItem4.Alignment = StringAlignment.Far;
							columnItem4.BackColor = Color.FromArgb(64, 64, 64);
							columnItem4.ColumnAlignment = StringAlignment.Center;
							columnItem4.FontColor = Color.LightGray;
							columnItem4.MyStyle = FontStyle.Regular;
							columnItem4.Name = "today_sc";
							columnItem4.Text = "S/C Today";
							columnItem4.ValueFormat = FormatType.Text;
							columnItem4.Visible = true;
							columnItem4.Width = 10;
							columnItem5.Alignment = StringAlignment.Far;
							columnItem5.BackColor = Color.FromArgb(64, 64, 64);
							columnItem5.ColumnAlignment = StringAlignment.Center;
							columnItem5.FontColor = Color.LightGray;
							columnItem5.MyStyle = FontStyle.Regular;
							columnItem5.Name = "sc_avg";
							columnItem5.Text = "S/C Avg";
							columnItem5.ValueFormat = FormatType.Text;
							columnItem5.Visible = true;
							columnItem5.Width = 9;
							columnItem6.Alignment = StringAlignment.Far;
							columnItem6.BackColor = Color.FromArgb(64, 64, 64);
							columnItem6.ColumnAlignment = StringAlignment.Center;
							columnItem6.FontColor = Color.LightGray;
							columnItem6.MyStyle = FontStyle.Regular;
							columnItem6.Name = "cost_avg";
							columnItem6.Text = "Cost Avg";
							columnItem6.ValueFormat = FormatType.Text;
							columnItem6.Visible = true;
							columnItem6.Width = 9;
							columnItem7.Alignment = StringAlignment.Far;
							columnItem7.BackColor = Color.FromArgb(64, 64, 64);
							columnItem7.ColumnAlignment = StringAlignment.Center;
							columnItem7.FontColor = Color.LightGray;
							columnItem7.MyStyle = FontStyle.Regular;
							columnItem7.Name = "sc_amount";
							columnItem7.Text = "S/C Amount";
							columnItem7.ValueFormat = FormatType.Text;
							columnItem7.Visible = true;
							columnItem7.Width = 11;
							columnItem8.Alignment = StringAlignment.Far;
							columnItem8.BackColor = Color.FromArgb(64, 64, 64);
							columnItem8.ColumnAlignment = StringAlignment.Center;
							columnItem8.FontColor = Color.LightGray;
							columnItem8.MyStyle = FontStyle.Regular;
							columnItem8.Name = "cost_amount";
							columnItem8.Text = "Cost Amount";
							columnItem8.ValueFormat = FormatType.Text;
							columnItem8.Visible = true;
							columnItem8.Width = 12;
							columnItem9.Alignment = StringAlignment.Far;
							columnItem9.BackColor = Color.FromArgb(64, 64, 64);
							columnItem9.ColumnAlignment = StringAlignment.Center;
							columnItem9.FontColor = Color.LightGray;
							columnItem9.MyStyle = FontStyle.Regular;
							columnItem9.Name = "realize";
							columnItem9.Text = "Realize P/L";
							columnItem9.ValueFormat = FormatType.Text;
							columnItem9.Visible = true;
							columnItem9.Width = 12;
							this.intzaReport.Columns.Add(columnItem);
							this.intzaReport.Columns.Add(columnItem2);
							this.intzaReport.Columns.Add(columnItem20);
							this.intzaReport.Columns.Add(columnItem3);
							this.intzaReport.Columns.Add(columnItem4);
							this.intzaReport.Columns.Add(columnItem5);
							this.intzaReport.Columns.Add(columnItem6);
							this.intzaReport.Columns.Add(columnItem7);
							this.intzaReport.Columns.Add(columnItem8);
							this.intzaReport.Columns.Add(columnItem9);
						}
					}
				}
				this.intzaReport.Redraw();
			}
			catch (Exception ex)
			{
				this.ShowError("createColumns", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnPrintT_Click(object sender, EventArgs e)
		{
			base.BeginInvoke(new frmPortfolio.ShowPrintDailog_TFEX_CallBack(this.ShowPrintDailog_TFEX));
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void CreateHeaderReport_TFEX()
		{
			try
			{
				string text = string.Empty;
				for (int i = 0; i < this.sortGridTfex.Columns.Count; i++)
				{
					if (this.sortGridTfex.Columns[i].Visible)
					{
						text += this.GetString(this.sortGridTfex.Columns[i].Text, this.sortGridTfex.Columns[i].Width, this.sortGridTfex.Columns[i].ColumnAlignment);
					}
				}
				this._headerMessage = text;
			}
			catch (Exception ex)
			{
				this.ShowError("CreateHeaderReport", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowPrintDailog_TFEX()
		{
			try
			{
				if (this._textPrint == null)
				{
					this._textPrint = new ArrayList();
				}
				else
				{
					this._textPrint.Clear();
				}
				SortGrid sortGrid = this.sortGridTfex;
				this._headerMessage = "";
				foreach (ColumnItem current in sortGrid.Columns)
				{
					if (current.Visible)
					{
						this._headerMessage += ((current.Alignment == StringAlignment.Near) ? current.Text.PadRight(current.Width, ' ') : current.Text.PadLeft(current.Width, ' '));
					}
				}
				this.SetDetailHeaderText_TFEX(this._headerMessage);
				string text = string.Empty;
				string text2 = string.Empty;
				for (int i = 0; i < sortGrid.Rows; i++)
				{
					foreach (ColumnItem current in sortGrid.Columns)
					{
						if (current.Visible)
						{
							text2 = sortGrid.Records(i).Fields(current.Name).Text.ToString();
							if (current.ValueFormat == FormatType.Symbol && !string.IsNullOrEmpty(sortGrid.Records(i).Fields(current.Name).Tag))
							{
								text2 = text2 + " (" + sortGrid.Records(i).Fields(current.Name).Tag + ")";
							}
							text += ((current.Alignment == StringAlignment.Near) ? text2.PadRight(current.Width, ' ') : text2.PadLeft(current.Width, ' '));
						}
					}
					this.AddPrintText(text);
					text = string.Empty;
				}
				string empty = string.Empty;
				this._PAGEPREVIEW = 0;
				this._previewPrinter = new PrintPreviewDialog();
				this._printDocument = new PrintDocument();
				this._printDialog = new PrintDialog();
				this._printDocument.BeginPrint += new PrintEventHandler(this.OnBeginPrint_TFEX);
				this._printDocument.PrintPage += new PrintPageEventHandler(this.OnPrintPage_TFEX);
				this._printDocument.DocumentName = "Realize  Profit / Loss";
				this._printDialog.Document = this._printDocument;
				this._previewPrinter.Document = this._printDocument;
				this._previewPrinter.ShowDialog();
			}
			catch (Exception ex)
			{
				this.ShowError("ShowPrinter_TFEX", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnBeginPrint_TFEX(object sender, PrintEventArgs e)
		{
			if (e.PrintAction == PrintAction.PrintToPrinter)
			{
				if (this._printDialog.ShowDialog() == DialogResult.Cancel)
				{
					e.Cancel = true;
				}
				this._PAGEPREVIEW = 0;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnPrintPage_TFEX(object sender, PrintPageEventArgs e)
		{
			int num = e.MarginBounds.Left - 30;
			int num2 = e.MarginBounds.Top - 30;
			Brush brush = new SolidBrush(Color.Black);
			this._PAGEPREVIEW++;
			e.Graphics.DrawString("#Page " + this._PAGEPREVIEW, new Font("Courier New", 8f, FontStyle.Italic), Brushes.Black, (float)e.MarginBounds.Right, (float)(e.MarginBounds.Bottom + 50));
			while (this.linesPrinted_TFEX < this._textPrint.Count)
			{
				e.Graphics.DrawString(this._textPrint[this.linesPrinted_TFEX++].ToString(), new Font("Courier New", 8f, FontStyle.Regular), brush, (float)num, (float)num2);
				num2 += 15;
				if (num2 >= e.MarginBounds.Bottom && this.linesPrinted_TFEX < this._textPrint.Count)
				{
					e.HasMorePages = true;
					return;
				}
			}
			this.linesPrinted_TFEX = 0;
			e.HasMorePages = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetDetailHeaderText_TFEX(string columnsHeaderText)
		{
			string empty = string.Empty;
			try
			{
				this.AddPrintText_TFEX("                            " + ApplicationInfo.GetFullNameBroker(ApplicationInfo.BrokerId));
				this.AddPrintText_TFEX(DateTime.Now.ToString("MMM dd,yyyy hh:mm:ss"));
				this.AddPrintText_TFEX("Customer  :  " + this._currAccount + "                NAME : " + this.intzaCustHeadTfex.Item("tbCustName").Text);
				this.AddPrintText_TFEX(string.Concat(new string[]
				{
					"Cust Type :  ",
					this.intzaCustHeadTfex.Item("tbCustomerType").Text,
					"                    Acc. Type : ",
					this.intzaCustHeadTfex.Item("tbAccountType").Text,
					"                      Flag : ",
					this.intzaCustHeadTfex.Item("tbCustomerFlag").Text
				}));
				this.AddPrintText_TFEX("Buy Limit :  " + this.intzaCustHeadTfex.Item("tbBuyLimit").Text + "          Credit Line : " + this.intzaCustHeadTfex.Item("tbCreditLine").Text);
				this.AddPrintText_TFEX("===========================================================================================================");
				this.AddPrintText_TFEX(columnsHeaderText + "\r\n");
				this.AddPrintText_TFEX("===========================================================================================================");
			}
			catch (Exception ex)
			{
				this.ShowError("SetDetailHeaderText_TFEX", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void AddPrintText_TFEX()
		{
			try
			{
				string content = string.Empty;
				for (int i = 0; i < this.sortGridTfex.Rows; i++)
				{
					content = this.sortGridTfex.Records(i).Fields("col1").Text.ToString();
					this.AddPrintText(content);
					content = string.Empty;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("AddPrintText_TFEX", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void AddPrintText_TFEX(string content)
		{
			try
			{
				this._textPrint.Add(content);
			}
			catch (Exception ex)
			{
				this.ShowError("AddPrintText_TFEX", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void AddPrintText_TFEX(ListView lv)
		{
			try
			{
				string text = string.Empty;
				for (int i = 0; i < lv.Items.Count; i++)
				{
					ListViewItem listViewItem = lv.Items[i];
					for (int j = 0; j < listViewItem.SubItems.Count; j++)
					{
						text += this.GetString(listViewItem.SubItems[j].Text, lv.Columns[j].Width / 10, (StringAlignment)lv.Columns[j].TextAlign);
					}
					this.AddPrintText_TFEX(text);
					text = string.Empty;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("AddPrintText_TFEX", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void sortGrid1_TableMouseClick(object sender, TableMouseEventArgs e)
		{
			try
			{
				if (e.RowIndex == -1)
				{
					string name = e.Column.Name;
					if (name != null)
					{
						if (name == "side" || name == "stock" || name == "volume" || name == "price" || name == "pubvol")
						{
							if (this.sortGrid1.SortType == SortType.Asc)
							{
								this.sortGrid1.Sort(e.Column.Name, SortType.Desc);
							}
							else
							{
								this.sortGrid1.Sort(e.Column.Name, SortType.Asc);
							}
							this.sortGrid1.Redraw();
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("intza_TableMouseClick", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void sortGrid1_TableMouseDoubleClick(object sender, TableMouseEventArgs e)
		{
			try
			{
				if ((float)e.Mouse.Y > this.sortGrid1.RowHeight)
				{
					string name = e.Column.Name;
					switch (name)
					{
					case "dateopen":
					case "dateclose":
					case "direction":
					case "positionsize":
					case "entryprice":
					case "stoploss":
					case "takeprofit":
					case "exitprice":
					case "loss":
					case "profit":
						this.SetTextPosition(e.RowIndex, e.Column.Name);
						break;
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("intza_TableMouseClick", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetTextPosition(int rowIndex, string columnName)
		{
			try
			{
				this.isEditing = true;
				this.lastFocus = rowIndex;
				Rectangle position = this.sortGrid1.GetPosition(rowIndex, columnName);
				this.cbText.DataSource = null;
				this.cbText.Items.Clear();
				this.sortGrid1.SetFocusItem(rowIndex);
				this.cbText.Tag = columnName;
				this.cbText.SetBounds(position.X + this.sortGrid1.Left, position.Y + this.sortGrid1.Top + 1, position.Width, position.Height);
				if (columnName != null)
				{
					if (!(columnName == "dateopen"))
					{
						if (!(columnName == "dateclose"))
						{
							if (columnName == "direction")
							{
								this.cbText.Items.Add("L");
								this.cbText.Items.Add("S");
							}
						}
						else
						{
							this.cbText.DataSource = frmPortfolio.GetDates(DateTime.Now.Year, DateTime.Now.Month);
						}
					}
					else
					{
						this.cbText.DataSource = frmPortfolio.GetDates(DateTime.Now.Year, DateTime.Now.Month);
					}
				}
				this.cbText.BringToFront();
				this.cbText.Show();
				this.cbText.Text = this.sortGrid1.Records(rowIndex).Fields(columnName).Text.ToString();
				this.cbText.Focus();
				this.cbText.SelectAll();
				this.isEditing = false;
			}
			catch (Exception ex)
			{
				this.isEditing = false;
				this.ShowError("SetTextPosition", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbText_KeyUp(object sender, KeyEventArgs e)
		{
			try
			{
				Keys keyCode = e.KeyCode;
				if (keyCode != Keys.Return)
				{
					if (keyCode == Keys.Escape)
					{
						this.cbText.Hide();
						goto IL_55F;
					}
					switch (keyCode)
					{
					case Keys.Left:
					{
						string text = this.cbText.Tag.ToString();
						switch (text)
						{
						case "profit":
							this.SetTextPosition(this.sortGrid1.FocusItemIndex, "loss");
							break;
						case "loss":
							this.SetTextPosition(this.sortGrid1.FocusItemIndex, "exitprice");
							break;
						case "exitprice":
							this.SetTextPosition(this.sortGrid1.FocusItemIndex, "takeprofit");
							break;
						case "takeprofit":
							this.SetTextPosition(this.sortGrid1.FocusItemIndex, "stoploss");
							break;
						case "stoploss":
							this.SetTextPosition(this.sortGrid1.FocusItemIndex, "entryprice");
							break;
						case "entryprice":
							this.SetTextPosition(this.sortGrid1.FocusItemIndex, "positionsize");
							break;
						case "positionsize":
							this.SetTextPosition(this.sortGrid1.FocusItemIndex, "direction");
							break;
						case "direction":
							this.SetTextPosition(this.sortGrid1.FocusItemIndex, "dateclose");
							break;
						case "dateclose":
							this.SetTextPosition(this.sortGrid1.FocusItemIndex, "dateopen");
							break;
						}
						e.SuppressKeyPress = true;
						goto IL_55F;
					}
					case Keys.Up:
						if (this.sortGrid1.FocusItemIndex - 1 > -1)
						{
							this.sortGrid1.SetFocusItem(this.sortGrid1.FocusItemIndex - 1);
							this.SetTextPosition(this.sortGrid1.FocusItemIndex, this.cbText.Tag.ToString());
						}
						e.SuppressKeyPress = true;
						goto IL_55F;
					case Keys.Right:
						break;
					case Keys.Down:
						if (this.sortGrid1.FocusItemIndex + 1 < this.sortGrid1.Rows)
						{
							this.sortGrid1.SetFocusItem(this.sortGrid1.FocusItemIndex);
							this.SetTextPosition(this.sortGrid1.FocusItemIndex + 1, this.cbText.Tag.ToString());
						}
						e.SuppressKeyPress = true;
						goto IL_55F;
					default:
						goto IL_55F;
					}
				}
				if (this.SetText())
				{
					string text = this.cbText.Tag.ToString();
					switch (text)
					{
					case "dateopen":
						this.SetTextPosition(this.sortGrid1.FocusItemIndex, "dateclose");
						break;
					case "dateclose":
						this.SetTextPosition(this.sortGrid1.FocusItemIndex, "direction");
						break;
					case "direction":
						this.SetTextPosition(this.sortGrid1.FocusItemIndex, "entryprice");
						this.calOverview();
						break;
					case "entryprice":
						this.SetTextPosition(this.sortGrid1.FocusItemIndex, "stoploss");
						this.calRiskRewardByRow(this.sortGrid1.FocusItemIndex);
						break;
					case "stoploss":
						this.SetTextPosition(this.sortGrid1.FocusItemIndex, "takeprofit");
						this.calRiskRewardByRow(this.sortGrid1.FocusItemIndex);
						break;
					case "takeprofit":
						this.SetTextPosition(this.sortGrid1.FocusItemIndex, "exitprice");
						this.calRiskRewardByRow(this.sortGrid1.FocusItemIndex);
						break;
					case "exitprice":
						this.SetTextPosition(this.sortGrid1.FocusItemIndex, "loss");
						break;
					case "loss":
						this.SetTextPosition(this.sortGrid1.FocusItemIndex, "profit");
						this.calBalanceByRow(this.sortGrid1.FocusItemIndex);
						this.calOverview();
						break;
					case "profit":
						this.calBalanceByRow(this.sortGrid1.FocusItemIndex);
						this.calOverview();
						break;
					}
				}
				e.SuppressKeyPress = true;
				IL_55F:;
			}
			catch (Exception ex)
			{
				this.ShowError("cbText_KeyUp", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool SetText()
		{
			bool result = false;
			try
			{
				string text = this.cbText.Tag.ToString();
				switch (text)
				{
				case "direction":
					text = this.cbText.Text.Trim();
					if (text != null)
					{
						if (text == "L" || text == "S")
						{
							result = true;
						}
					}
					break;
				case "entryprice":
				case "stoploss":
				case "takeprofit":
				case "exitprice":
					if (FormatUtil.Isnumeric(this.cbText.Text.Trim()))
					{
						result = true;
					}
					break;
				case "loss":
					if (FormatUtil.Isnumeric(this.cbText.Text.Trim()))
					{
						if (Convert.ToDecimal(this.cbText.Text.Trim()) != 0m)
						{
							this.sortGrid1.Records(this.sortGrid1.FocusItemIndex).Fields("profit").Text = "";
							this.sortGrid1.Records(this.sortGrid1.FocusItemIndex).Fields("winloss").Text = "loss";
							this.sortGrid1.EndUpdate();
						}
						else
						{
							if (Convert.ToDecimal(this.cbText.Text.Trim()) == 0m)
							{
								if (this.sortGrid1.Records(this.sortGrid1.FocusItemIndex).Fields("profit").Text.ToString().Trim() == "")
								{
									this.sortGrid1.Records(this.sortGrid1.FocusItemIndex).Fields("winloss").Text = "";
								}
							}
						}
						result = true;
					}
					else
					{
						if (string.IsNullOrEmpty(this.cbText.Text.Trim()))
						{
							if (this.sortGrid1.Records(this.sortGrid1.FocusItemIndex).Fields("profit").Text.ToString().Trim() == "")
							{
								this.sortGrid1.Records(this.sortGrid1.FocusItemIndex).Fields("winloss").Text = "";
							}
							result = true;
						}
					}
					break;
				case "profit":
					if (FormatUtil.Isnumeric(this.cbText.Text.Trim()))
					{
						if (Convert.ToDecimal(this.cbText.Text.Trim()) != 0m)
						{
							this.sortGrid1.Records(this.sortGrid1.FocusItemIndex).Fields("loss").Text = "";
							this.sortGrid1.Records(this.sortGrid1.FocusItemIndex).Fields("winloss").Text = "win";
							this.sortGrid1.EndUpdate();
						}
						else
						{
							if (Convert.ToDecimal(this.cbText.Text.Trim()) == 0m)
							{
								if (this.sortGrid1.Records(this.sortGrid1.FocusItemIndex).Fields("loss").Text.ToString().Trim() == "")
								{
									this.sortGrid1.Records(this.sortGrid1.FocusItemIndex).Fields("winloss").Text = "";
								}
							}
						}
						result = true;
					}
					else
					{
						if (string.IsNullOrEmpty(this.cbText.Text.Trim()))
						{
							if (this.sortGrid1.Records(this.sortGrid1.FocusItemIndex).Fields("loss").Text.ToString().Trim() == "")
							{
								this.sortGrid1.Records(this.sortGrid1.FocusItemIndex).Fields("winloss").Text = "";
							}
							result = true;
						}
					}
					break;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SetText", ex);
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbStock_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
			case Keys.Prior:
			case Keys.Next:
			case Keys.Left:
			case Keys.Up:
			case Keys.Right:
			case Keys.Down:
				e.SuppressKeyPress = true;
				break;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbText_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbText_TextChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.cbText.Tag.ToString() == "entryprice" || this.cbText.Tag.ToString() == "stoploss" || this.cbText.Tag.ToString() == "takeprofit" || this.cbText.Tag.ToString() == "exitprice" || this.cbText.Tag.ToString() == "loss" || this.cbText.Tag.ToString() == "profit")
				{
					if (this.cbText.Text.Trim() != string.Empty)
					{
						if (FormatUtil.Isnumeric(this.cbText.Text))
						{
							try
							{
								decimal num = Convert.ToDecimal(this.cbText.Text.Replace(",", ""));
								this.cbText.Text = num.ToString("#,###.##");
								this.cbText.Select(this.cbText.Text.Length, 0);
							}
							catch
							{
								this.cbText.Text = this.cbText.Text.Substring(0, this.cbText.Text.Length - 1);
							}
						}
						else
						{
							bool flag = false;
							string text = string.Empty;
							if (this.cbText.Text.Length > 0)
							{
								if (this.cbText.Text.Substring(0, 1) == "-")
								{
									flag = true;
								}
								text = this.cbText.Text.Replace("-", "");
								if (FormatUtil.Isnumeric(text) || string.IsNullOrEmpty(text))
								{
									if (FormatUtil.Isnumeric(text))
									{
										try
										{
											decimal num = Convert.ToDecimal(this.cbText.Text.Replace(",", ""));
											this.cbText.Text = num.ToString("#,###.##");
											this.cbText.Select(this.cbText.Text.Length, 0);
										}
										catch
										{
											this.cbText.Text = this.cbText.Text.Substring(0, this.cbText.Text.Length - 1);
										}
									}
									if (flag)
									{
										this.cbText.Text = "-" + text;
									}
									else
									{
										this.cbText.Text = this.cbText.Text.Substring(0, this.cbText.Text.Length - 1);
									}
								}
								else
								{
									this.cbText.Text = this.cbText.Text.Substring(0, this.cbText.Text.Length - 1);
								}
							}
							else
							{
								this.cbText.Text = this.cbText.Text.Substring(0, this.cbText.Text.Length - 1);
							}
						}
					}
				}
				if (!this.isEditing && this.sortGrid1.FocusItemIndex > -1 && this.cbText.Tag != null)
				{
					this.sortGrid1.Records(this.sortGrid1.FocusItemIndex).Fields(this.cbText.Tag.ToString()).Text = this.cbText.Text;
					if (this.cbText.Tag.ToString() == "direction")
					{
						if (this.cbText.Text.ToString().Trim().ToUpper() == "L")
						{
							this.sortGrid1.Records(this.sortGrid1.FocusItemIndex).Fields("direction").FontColor = Color.Lime;
						}
						else
						{
							if (this.cbText.Text.ToString().Trim().ToUpper() == "S")
							{
								this.sortGrid1.Records(this.sortGrid1.FocusItemIndex).Fields("direction").FontColor = Color.Red;
							}
						}
					}
					this.sortGrid1.Records(this.sortGrid1.FocusItemIndex).Changed = true;
					this.sortGrid1.EndUpdate();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("cbText_TextChanged", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbText_Leave(object sender, EventArgs e)
		{
			this.cbText.Hide();
			if (this.SetText())
			{
				string text = this.cbText.Tag.ToString();
				switch (text)
				{
				case "dateopen":
					this.SetTextPosition(this.sortGrid1.FocusItemIndex, "dateclose");
					break;
				case "dateclose":
					this.SetTextPosition(this.sortGrid1.FocusItemIndex, "direction");
					break;
				case "direction":
					this.SetTextPosition(this.sortGrid1.FocusItemIndex, "entryprice");
					break;
				case "entryprice":
					this.SetTextPosition(this.sortGrid1.FocusItemIndex, "stoploss");
					this.calRiskRewardByRow(this.sortGrid1.FocusItemIndex);
					break;
				case "stoploss":
					this.SetTextPosition(this.sortGrid1.FocusItemIndex, "takeprofit");
					this.calRiskRewardByRow(this.sortGrid1.FocusItemIndex);
					break;
				case "takeprofit":
					this.SetTextPosition(this.sortGrid1.FocusItemIndex, "exitprice");
					this.calRiskRewardByRow(this.sortGrid1.FocusItemIndex);
					break;
				case "exitprice":
					this.SetTextPosition(this.sortGrid1.FocusItemIndex, "loss");
					break;
				case "loss":
					this.SetTextPosition(this.sortGrid1.FocusItemIndex, "profit");
					this.calBalanceByRow(this.sortGrid1.FocusItemIndex);
					this.calOverview();
					break;
				case "profit":
					this.calBalanceByRow(this.sortGrid1.FocusItemIndex);
					this.calOverview();
					break;
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnTradeJournal_Click(object sender, EventArgs e)
		{
			if (!ApplicationInfo.IsEquityAccount)
			{
				this.tsbtnTradeJournal.ForeColor = Color.Orange;
				this.tsbtnPort.ForeColor = Color.WhiteSmoke;
				this.sortGridTfex.Visible = false;
				this.sortGridTfexSumm.Visible = false;
				this.intzaCustBottTfex.Visible = false;
				this.pnlTradeJ.Visible = true;
				this.tsbtnExportCSV.Visible = true;
				this.CalAndUpdateBalance();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnPort_Click(object sender, EventArgs e)
		{
			if (!ApplicationInfo.IsEquityAccount)
			{
				this.tsbtnTradeJournal.ForeColor = Color.WhiteSmoke;
				this.tsbtnPort.ForeColor = Color.Orange;
				this.sortGridTfex.Visible = true;
				this.sortGridTfexSumm.Visible = true;
				this.intzaCustBottTfex.Visible = true;
				this.pnlTradeJ.Visible = false;
				this.tsbtnExportCSV.Visible = false;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tstxtAmount_KeyUp(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode == Keys.Return)
			{
				decimal.TryParse(this.tstxtAmount.Text.Replace(",", ""), out this._totalTJAmount);
				this.tsbtnAmount.Text = FormatUtil.PriceFormat(this._totalTJAmount, 2, "0.00");
				this.CalAndUpdateBalance();
				this.calOverview();
				this.tsbtnAmount.Visible = true;
				this.tstxtAmount.Visible = false;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tstxtAmount_Leave(object sender, EventArgs e)
		{
			decimal.TryParse(this.tstxtAmount.Text.Replace(",", ""), out this._totalTJAmount);
			this.tsbtnAmount.Text = FormatUtil.PriceFormat(this._totalTJAmount, 2, "0.00");
			this.CalAndUpdateBalance();
			this.calOverview();
			this.tsbtnAmount.Visible = true;
			this.tstxtAmount.Visible = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnAmount_Click(object sender, EventArgs e)
		{
			this.tsbtnAmount.Visible = false;
			this.tstxtAmount.Visible = true;
			this.tstxtAmount.Text = this.tsbtnAmount.Text;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void CalAndUpdateBalance()
		{
			try
			{
				decimal.TryParse(this.tsbtnAmount.Text.Replace(",", ""), out this._totalTJAmount);
				decimal num = this._totalTJAmount;
				decimal d = 0m;
				decimal num2 = 0m;
				decimal num3 = 0m;
				for (int i = 0; i < this.sortGrid1.Rows; i++)
				{
					RecordItem recordItem = this.sortGrid1.Records(i);
					decimal.TryParse(recordItem.Fields("loss").Text.ToString().Replace(",", ""), out d);
					decimal.TryParse(recordItem.Fields("profit").Text.ToString().Replace(",", ""), out num2);
					decimal num4;
					decimal num5;
					if (i == 0)
					{
						num = this._totalTJAmount + (d + num2);
						if (this._totalTJAmount != 0m)
						{
							num4 = num2 / this._totalTJAmount * 100m;
							num5 = d / this._totalTJAmount * 100m;
						}
						else
						{
							num4 = 0m;
							num5 = 0m;
						}
					}
					else
					{
						decimal.TryParse(this.sortGrid1.Records(i - 1).Fields("tmpbalance").Text.ToString().Replace(",", ""), out num3);
						num = num3 + (d + num2);
						if (num3 != 0m)
						{
							num4 = num2 / num3 * 100m;
							num5 = d / num3 * 100m;
						}
						else
						{
							num4 = 0m;
							num5 = 0m;
						}
					}
					if (d != 0m || num2 != 0m)
					{
						recordItem.Fields("profitpct").Text = FormatUtil.PriceFormat(num4, false, "%", 2);
						recordItem.Fields("losspct").Text = FormatUtil.PriceFormat(num5, false, "%", 2);
						recordItem.Fields("balance").Text = FormatUtil.PriceFormat(num);
					}
					recordItem.Fields("tmpbalance").Text = num;
					recordItem.Fields("riskreward").BackColor = Color.FromArgb(30, 30, 30);
					recordItem.Fields("balance").BackColor = Color.FromArgb(30, 30, 30);
					recordItem.Fields("profitpct").BackColor = Color.FromArgb(30, 30, 30);
					recordItem.Fields("losspct").BackColor = Color.FromArgb(30, 30, 30);
					recordItem.Fields("riskreward").FontColor = Color.Cyan;
					recordItem.Fields("balance").FontColor = Color.Cyan;
					recordItem.Fields("profitpct").FontColor = Color.Cyan;
					recordItem.Fields("losspct").FontColor = Color.Cyan;
				}
				this.sortGrid1.EndUpdate();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void calBalanceByRow(int rowIndex)
		{
			try
			{
				decimal d = 0m;
				decimal num = 0m;
				decimal num2 = 0m;
				int i = rowIndex;
				while (i < this.sortGrid1.Rows)
				{
					RecordItem recordItem = this.sortGrid1.Records(i);
					decimal.TryParse(recordItem.Fields("loss").Text.ToString().Replace(",", ""), out d);
					decimal.TryParse(recordItem.Fields("profit").Text.ToString().Replace(",", ""), out num);
					decimal num3;
					decimal num4;
					decimal num5;
					if (i == 0)
					{
						num3 = this._totalTJAmount + (d + num);
						if (this._totalTJAmount != 0m)
						{
							num4 = num / this._totalTJAmount * 100m;
							num5 = d / this._totalTJAmount * 100m;
						}
						else
						{
							num4 = 0m;
							num5 = 0m;
						}
					}
					else
					{
						decimal.TryParse(this.sortGrid1.Records(i - 1).Fields("tmpbalance").Text.ToString().Replace(",", ""), out num2);
						num3 = num2 + (d + num);
						if (num2 != 0m)
						{
							num4 = num / num2 * 100m;
							num5 = d / num2 * 100m;
						}
						else
						{
							num4 = 0m;
							num5 = 0m;
						}
					}
					if (d == 0m && num == 0m)
					{
						recordItem.Fields("balance").Text = "";
					}
					else
					{
						recordItem.Fields("balance").Text = FormatUtil.PriceFormat(num3);
					}
					recordItem.Fields("tmpbalance").Text = num3;
					recordItem.Fields("profitpct").Text = FormatUtil.PriceFormat(num4, false, "%", 2);
					recordItem.Fields("losspct").Text = FormatUtil.PriceFormat(num5, false, "%", 2);
					this.sortGrid1.EndUpdate();
					rowIndex++;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void calRiskRewardByRow(int rowIndex)
		{
			try
			{
				decimal d = 0m;
				decimal num = 0m;
				decimal d2 = 0m;
				decimal.TryParse(this.sortGrid1.Records(rowIndex).Fields("entryprice").Text.ToString().Replace(",", ""), out num);
				decimal.TryParse(this.sortGrid1.Records(rowIndex).Fields("stoploss").Text.ToString().Replace(",", ""), out d2);
				decimal.TryParse(this.sortGrid1.Records(rowIndex).Fields("takeprofit").Text.ToString().Replace(",", ""), out d);
				decimal num2;
				if (num - d2 == 0m)
				{
					num2 = 0m;
				}
				else
				{
					num2 = (d - num) / (num - d2);
				}
				this.sortGrid1.Records(rowIndex).Fields("riskreward").Text = FormatUtil.PriceFormat(num2, 2, "0");
				this.sortGrid1.EndUpdate();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void calOverview()
		{
			try
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				decimal num5 = 0m;
				string text = string.Empty;
				string a = string.Empty;
				for (int i = 0; i < this.sortGrid1.Rows; i++)
				{
					text = this.sortGrid1.Records(i).Fields("winloss").Text.ToString().Trim();
					a = this.sortGrid1.Records(i).Fields("direction").Text.ToString().Trim();
					if (!string.IsNullOrEmpty(text))
					{
						if (text == "win")
						{
							if (a == "L")
							{
								num++;
							}
							else
							{
								if (a == "S")
								{
									num2++;
								}
							}
						}
						else
						{
							if (text == "loss")
							{
								if (a == "L")
								{
									num3++;
								}
								else
								{
									if (a == "S")
									{
										num4++;
									}
								}
							}
						}
					}
				}
				decimal.TryParse(this.sortGrid1.Records(this.sortGrid1.Rows - 1).Fields("tmpbalance").Text.ToString().Replace(",", ""), out num5);
				decimal d;
				if (this._totalTJAmount != 0m)
				{
					d = (num5 - this._totalTJAmount) / this._totalTJAmount;
				}
				else
				{
					d = 0m;
				}
				this.intzaTradeOverview.Item("tbEquityInvest").Text = FormatUtil.PriceFormat(this._totalTJAmount, 2, "0.00");
				this.intzaTradeOverview.Item("tbCurrEquity").Text = FormatUtil.PriceFormat(num5, 2, "0.00");
				this.intzaTradeOverview.Item("tbTotalPL").Text = FormatUtil.PriceFormat(d * this._totalTJAmount, 2, "0.00");
				this.intzaTradeOverview.Item("tbTotalPLPct").Text = FormatUtil.PriceFormat(d * 100m, 2, "0.00") + "%";
				this.intzaTradeOverview.Item("tbTotalTrade").Text = FormatUtil.PriceFormat(num + num2 + num3 + num4, 2, "0");
				this.intzaTradeOverview.Item("tbTotalTradePct").Text = FormatUtil.PriceFormat(100, false, "%", 2);
				this.intzaTradeOverview.Item("tbLongTrade").Text = FormatUtil.PriceFormat(num + num3, 2, "0");
				if ((double)(num + num2 + num3 + num4) != 0.0)
				{
					this.intzaTradeOverview.Item("tbLongTradePct").Text = FormatUtil.PriceFormat((double)(num + num3) / (double)(num + num2 + num3 + num4) * 100.0, 2, "0.00") + "%";
				}
				else
				{
					this.intzaTradeOverview.Item("tbLongTradePct").Text = "0.00%";
				}
				this.intzaTradeOverview.Item("tbShortTrade").Text = FormatUtil.PriceFormat(num2 + num4, 2, "0");
				if ((double)(num + num2 + num3 + num4) != 0.0)
				{
					this.intzaTradeOverview.Item("tbShortTradePct").Text = FormatUtil.PriceFormat((double)(num2 + num4) / (double)(num + num2 + num3 + num4) * 100.0, 2, "0.00") + "%";
				}
				else
				{
					this.intzaTradeOverview.Item("tbShortTradePct").Text = "0.00%";
				}
				this.intzaTradeOverview.Item("tbWinTrade").Text = FormatUtil.PriceFormat(num + num2, 2, "0");
				if ((double)(num + num2 + num3 + num4) != 0.0)
				{
					this.intzaTradeOverview.Item("tbWinTradePct").Text = FormatUtil.PriceFormat((double)(num + num2) / (double)(num + num2 + num3 + num4) * 100.0, 2, "0.00") + "%";
				}
				else
				{
					this.intzaTradeOverview.Item("tbWinTradePct").Text = "0.00%";
				}
				this.intzaTradeOverview.Item("tbWinLong").Text = FormatUtil.PriceFormat(num, 2, "0");
				if ((double)(num + num2 + num3 + num4) != 0.0)
				{
					this.intzaTradeOverview.Item("tbWinLongPct").Text = FormatUtil.PriceFormat((double)num / (double)(num + num2 + num3 + num4) * 100.0, 2, "0.00") + "%";
				}
				else
				{
					this.intzaTradeOverview.Item("tbWinLongPct").Text = "0.00%";
				}
				this.intzaTradeOverview.Item("tbWinShort").Text = FormatUtil.PriceFormat(num2, 2, "0");
				if ((double)(num + num2 + num3 + num4) != 0.0)
				{
					this.intzaTradeOverview.Item("tbwinShortPct").Text = FormatUtil.PriceFormat((double)num2 / (double)(num + num2 + num3 + num4) * 100.0, 2, "0.00") + "%";
				}
				else
				{
					this.intzaTradeOverview.Item("tbwinShortPct").Text = "0.00%";
				}
				this.intzaTradeOverview.Item("tbLoseTrade").Text = FormatUtil.PriceFormat(num3 + num4, 2, "0");
				if ((double)(num + num2 + num3 + num4) != 0.0)
				{
					this.intzaTradeOverview.Item("tbLoseTradePct").Text = FormatUtil.PriceFormat((double)(num3 + num4) / (double)(num + num2 + num3 + num4) * 100.0, 2, "0.00") + "%";
				}
				else
				{
					this.intzaTradeOverview.Item("tbLoseTradePct").Text = "0.00%";
				}
				this.intzaTradeOverview.Item("tbLoseLong").Text = FormatUtil.PriceFormat(num3, 2, "0");
				if ((double)(num + num2 + num3 + num4) != 0.0)
				{
					this.intzaTradeOverview.Item("tbLoseLongPct").Text = FormatUtil.PriceFormat((double)num3 / (double)(num + num2 + num3 + num4) * 100.0, 2, "0.00") + "%";
				}
				else
				{
					this.intzaTradeOverview.Item("tbLoseLongPct").Text = "0.00%";
				}
				this.intzaTradeOverview.Item("tbLoseShort").Text = FormatUtil.PriceFormat(num4, 2, "0");
				if ((double)(num + num2 + num3 + num4) != 0.0)
				{
					this.intzaTradeOverview.Item("tbLoseShortPct").Text = FormatUtil.PriceFormat((double)num4 / (double)(num + num2 + num3 + num4) * 100.0, 2, "0.00") + "%";
				}
				else
				{
					this.intzaTradeOverview.Item("tbLoseShortPct").Text = "0.00%";
				}
				this.intzaTradeOverview.EndUpdate();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tStripMainT_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static List<DateTime> GetDates(int year, int month)
		{
			List<DateTime> list = new List<DateTime>();
			DateTime item = new DateTime(year, month, 1);
			while (item.Month == month)
			{
				list.Add(item);
				item = item.AddDays(1.0);
			}
			return list;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnExportCSV_Click(object sender, EventArgs e)
		{
			try
			{
				this.gridToDataSet();
				if (this.dataSetTJ != null && this.dataSetTJ.Tables.Count > 0)
				{
					CSVReadWrite.exportToCSV(this.dataSetTJ);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private DataTable CreateOrderTable()
		{
			return new DataTable("orderQueue")
			{
				Columns = 
				{
					"dateopen",
					"dateclose",
					"direction",
					"entryprice",
					"stoploss",
					"takeprofit",
					"exitprice",
					"riskreward",
					"loss",
					"profit",
					"profitpct",
					"losspct",
					"balance"
				}
			};
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private DataTable CreateOverviewTable()
		{
			return new DataTable("overview")
			{
				Columns = 
				{
					"equitytext",
					"equityValue",
					"tradetext",
					"tradevalue",
					"tradepct",
					"wintext",
					"winvalue",
					"winpct",
					"losstext",
					"losevalue",
					"losspct"
				}
			};
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void gridToDataSet()
		{
			try
			{
				this.dataSetTJ = new DataSet();
				DataTable dataTable = this.CreateOverviewTable();
				dataTable.Rows.Add(new object[0]);
				dataTable.Rows[0][0] = this.intzaTradeOverview.Item("lbEquityInvest").Text.ToString().Trim().Replace(",", "");
				dataTable.Rows[0][1] = this.intzaTradeOverview.Item("tbEquityInvest").Text.ToString().Trim().Replace(",", "");
				dataTable.Rows[0][2] = this.intzaTradeOverview.Item("lbTotalTrade").Text.ToString().Trim().Replace(",", "");
				dataTable.Rows[0][3] = this.intzaTradeOverview.Item("tbTotalTrade").Text.ToString().Trim().Replace(",", "");
				dataTable.Rows[0][4] = this.intzaTradeOverview.Item("tbTotalTradePct").Text.ToString().Trim().Replace(",", "");
				dataTable.Rows[0][5] = this.intzaTradeOverview.Item("lbWinTrade").Text.ToString().Trim().Replace(",", "");
				dataTable.Rows[0][6] = this.intzaTradeOverview.Item("tbWinTrade").Text.ToString().Trim().Replace(",", "");
				dataTable.Rows[0][7] = this.intzaTradeOverview.Item("tbWinTradePct").Text.ToString().Trim().Replace(",", "");
				dataTable.Rows[0][8] = this.intzaTradeOverview.Item("lbLoseTrade").Text.ToString().Trim().Replace(",", "");
				dataTable.Rows[0][9] = this.intzaTradeOverview.Item("tbLoseTrade").Text.ToString().Trim().Replace(",", "");
				dataTable.Rows[0][10] = this.intzaTradeOverview.Item("tbLoseTradePct").Text.ToString().Trim().Replace(",", "");
				dataTable.Rows.Add(new object[0]);
				dataTable.Rows[1][0] = this.intzaTradeOverview.Item("lbCurrEquity").Text.ToString().Trim().Replace(",", "");
				dataTable.Rows[1][1] = this.intzaTradeOverview.Item("tbCurrEquity").Text.ToString().Trim().Replace(",", "");
				dataTable.Rows[1][2] = this.intzaTradeOverview.Item("lbLongTrade").Text.ToString().Trim().Replace(",", "");
				dataTable.Rows[1][3] = this.intzaTradeOverview.Item("tbLongTrade").Text.ToString().Trim().Replace(",", "");
				dataTable.Rows[1][4] = this.intzaTradeOverview.Item("tbLongTradePct").Text.ToString().Trim().Replace(",", "");
				dataTable.Rows[1][5] = this.intzaTradeOverview.Item("lbWinLong").Text.ToString().Trim().Replace(",", "");
				dataTable.Rows[1][6] = this.intzaTradeOverview.Item("tbWinLong").Text.ToString().Trim().Replace(",", "");
				dataTable.Rows[1][7] = this.intzaTradeOverview.Item("tbWinLongPct").Text.ToString().Trim().Replace(",", "");
				dataTable.Rows[1][8] = this.intzaTradeOverview.Item("lbLoseLong").Text.ToString().Trim().Replace(",", "");
				dataTable.Rows[1][9] = this.intzaTradeOverview.Item("tbLoseLong").Text.ToString().Trim().Replace(",", "");
				dataTable.Rows[1][10] = this.intzaTradeOverview.Item("tbLoseLongPct").Text.ToString().Trim().Replace(",", "");
				dataTable.Rows.Add(new object[0]);
				dataTable.Rows[2][0] = this.intzaTradeOverview.Item("lbTotalPL").Text.ToString().Trim().Replace(",", "");
				dataTable.Rows[2][1] = this.intzaTradeOverview.Item("tbTotalPL").Text.ToString().Trim().Replace(",", "");
				dataTable.Rows[2][2] = this.intzaTradeOverview.Item("lbShortTrade").Text.ToString().Trim().Replace(",", "");
				dataTable.Rows[2][3] = this.intzaTradeOverview.Item("tbShortTrade").Text.ToString().Trim().Replace(",", "");
				dataTable.Rows[2][4] = this.intzaTradeOverview.Item("tbShortTradePct").Text.ToString().Trim().Replace(",", "");
				dataTable.Rows[2][5] = this.intzaTradeOverview.Item("lbWinShort").Text.ToString().Trim().Replace(",", "");
				dataTable.Rows[2][6] = this.intzaTradeOverview.Item("tbWinShort").Text.ToString().Trim().Replace(",", "");
				dataTable.Rows[2][7] = this.intzaTradeOverview.Item("tbwinShortPct").Text.ToString().Trim().Replace(",", "");
				dataTable.Rows[2][8] = this.intzaTradeOverview.Item("lbLoseShort").Text.ToString().Trim().Replace(",", "");
				dataTable.Rows[2][9] = this.intzaTradeOverview.Item("tbLoseShort").Text.ToString().Trim().Replace(",", "");
				dataTable.Rows[2][10] = this.intzaTradeOverview.Item("tbLoseShortPct").Text.ToString().Trim().Replace(",", "");
				dataTable.Rows.Add(new object[0]);
				dataTable.Rows[3][0] = this.intzaTradeOverview.Item("lbTotalPLPct").Text.ToString().Trim().Replace(",", "");
				dataTable.Rows[3][1] = this.intzaTradeOverview.Item("tbTotalPLPct").Text.ToString().Trim().Replace(",", "");
				dataTable.Rows[3][2] = string.Empty;
				dataTable.Rows[3][3] = string.Empty;
				dataTable.Rows[3][4] = string.Empty;
				dataTable.Rows[3][5] = string.Empty;
				dataTable.Rows[3][6] = string.Empty;
				dataTable.Rows[3][7] = string.Empty;
				dataTable.Rows[3][8] = string.Empty;
				dataTable.Rows[3][9] = string.Empty;
				dataTable.Rows[3][10] = string.Empty;
				DataTable dataTable2 = this.CreateOrderTable();
				dataTable2.Rows.Add(new object[0]);
				dataTable2.Rows[0][0] = "Date Open";
				dataTable2.Rows[0][1] = "Date Close";
				dataTable2.Rows[0][2] = "Direction";
				dataTable2.Rows[0][3] = "Entry Price";
				dataTable2.Rows[0][4] = "Stop Loss";
				dataTable2.Rows[0][5] = "Take Profit";
				dataTable2.Rows[0][6] = "Exit Price";
				dataTable2.Rows[0][7] = "Risk/Reward";
				dataTable2.Rows[0][8] = "Loss";
				dataTable2.Rows[0][9] = "Profit";
				dataTable2.Rows[0][10] = "Profit%";
				dataTable2.Rows[0][11] = "Loss%";
				dataTable2.Rows[0][12] = "Balance";
				for (int i = 0; i < this.sortGrid1.Rows; i++)
				{
					RecordItem recordItem = this.sortGrid1.Records(i);
					if (recordItem.Fields("direction").Text.ToString() != string.Empty && recordItem.Fields("entryprice").Text.ToString() != string.Empty && recordItem.Fields("stoploss").Text.ToString() != string.Empty && recordItem.Fields("takeprofit").Text.ToString() != string.Empty && (recordItem.Fields("loss").Text.ToString() != string.Empty || recordItem.Fields("profit").Text.ToString() != string.Empty))
					{
						dataTable2.Rows.Add(new object[0]);
						dataTable2.Rows[i + 1][0] = recordItem.Fields("dateopen").Text.ToString();
						dataTable2.Rows[i + 1][1] = recordItem.Fields("dateclose").Text.ToString();
						dataTable2.Rows[i + 1][2] = recordItem.Fields("direction").Text.ToString();
						dataTable2.Rows[i + 1][3] = recordItem.Fields("entryprice").Text.ToString().Replace(",", "");
						dataTable2.Rows[i + 1][4] = recordItem.Fields("stoploss").Text.ToString().Replace(",", "");
						dataTable2.Rows[i + 1][5] = recordItem.Fields("takeprofit").Text.ToString().Replace(",", "");
						dataTable2.Rows[i + 1][6] = recordItem.Fields("exitprice").Text.ToString().Replace(",", "");
						dataTable2.Rows[i + 1][7] = recordItem.Fields("riskreward").Text.ToString().Replace(",", "");
						dataTable2.Rows[i + 1][8] = recordItem.Fields("loss").Text.ToString().Replace(",", "");
						dataTable2.Rows[i + 1][9] = recordItem.Fields("profit").Text.ToString().Replace(",", "");
						dataTable2.Rows[i + 1][10] = recordItem.Fields("profitpct").Text.ToString().Replace(",", "");
						dataTable2.Rows[i + 1][11] = recordItem.Fields("losspct").Text.ToString().Replace(",", "");
						dataTable2.Rows[i + 1][12] = recordItem.Fields("balance").Text.ToString().Replace(",", "");
					}
				}
				this.dataSetTJ.Tables.Add(dataTable);
				this.dataSetTJ.Tables.Add(dataTable2);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tstxtAmount_TextChanged(object sender, EventArgs e)
		{
			if (this.tstxtAmount.Text.Trim() != string.Empty)
			{
				if (FormatUtil.Isnumeric(this.tstxtAmount.Text))
				{
					try
					{
						decimal num = Convert.ToDecimal(this.tstxtAmount.Text.Replace(",", ""));
						this.tstxtAmount.Text = num.ToString("#,###.##");
						this.tstxtAmount.Select(this.tstxtAmount.Text.Length, 0);
					}
					catch
					{
						this.tstxtAmount.Text = this.tstxtAmount.Text.Substring(0, this.tstxtAmount.Text.Length - 1);
					}
				}
				else
				{
					this.tstxtAmount.Text = this.tstxtAmount.Text.Substring(0, this.tstxtAmount.Text.Length - 1);
				}
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmPortfolio));
			clsPermission clsPermission = new clsPermission();
			clsPermission clsPermission2 = new clsPermission();
			clsPermission clsPermission3 = new clsPermission();
			clsPermission clsPermission4 = new clsPermission();
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
			ColumnItem columnItem29 = new ColumnItem();
			ColumnItem columnItem30 = new ColumnItem();
			ColumnItem columnItem31 = new ColumnItem();
			ColumnItem columnItem32 = new ColumnItem();
			ColumnItem columnItem33 = new ColumnItem();
			ColumnItem columnItem34 = new ColumnItem();
			ColumnItem columnItem35 = new ColumnItem();
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
			ItemGrid itemGrid84 = new ItemGrid();
			ItemGrid itemGrid85 = new ItemGrid();
			ItemGrid itemGrid86 = new ItemGrid();
			ItemGrid itemGrid87 = new ItemGrid();
			ItemGrid itemGrid88 = new ItemGrid();
			ItemGrid itemGrid89 = new ItemGrid();
			ItemGrid itemGrid90 = new ItemGrid();
			ItemGrid itemGrid91 = new ItemGrid();
			ItemGrid itemGrid92 = new ItemGrid();
			ItemGrid itemGrid93 = new ItemGrid();
			ItemGrid itemGrid94 = new ItemGrid();
			ItemGrid itemGrid95 = new ItemGrid();
			ItemGrid itemGrid96 = new ItemGrid();
			ItemGrid itemGrid97 = new ItemGrid();
			ItemGrid itemGrid98 = new ItemGrid();
			ItemGrid itemGrid99 = new ItemGrid();
			ItemGrid itemGrid100 = new ItemGrid();
			ItemGrid itemGrid101 = new ItemGrid();
			ItemGrid itemGrid102 = new ItemGrid();
			ItemGrid itemGrid103 = new ItemGrid();
			ItemGrid itemGrid104 = new ItemGrid();
			ItemGrid itemGrid105 = new ItemGrid();
			ItemGrid itemGrid106 = new ItemGrid();
			ItemGrid itemGrid107 = new ItemGrid();
			ItemGrid itemGrid108 = new ItemGrid();
			ItemGrid itemGrid109 = new ItemGrid();
			ItemGrid itemGrid110 = new ItemGrid();
			ItemGrid itemGrid111 = new ItemGrid();
			ItemGrid itemGrid112 = new ItemGrid();
			ItemGrid itemGrid113 = new ItemGrid();
			ItemGrid itemGrid114 = new ItemGrid();
			ItemGrid itemGrid115 = new ItemGrid();
			ItemGrid itemGrid116 = new ItemGrid();
			ItemGrid itemGrid117 = new ItemGrid();
			ItemGrid itemGrid118 = new ItemGrid();
			ItemGrid itemGrid119 = new ItemGrid();
			ItemGrid itemGrid120 = new ItemGrid();
			ItemGrid itemGrid121 = new ItemGrid();
			ItemGrid itemGrid122 = new ItemGrid();
			ItemGrid itemGrid123 = new ItemGrid();
			ItemGrid itemGrid124 = new ItemGrid();
			ItemGrid itemGrid125 = new ItemGrid();
			ItemGrid itemGrid126 = new ItemGrid();
			ItemGrid itemGrid127 = new ItemGrid();
			ItemGrid itemGrid128 = new ItemGrid();
			ItemGrid itemGrid129 = new ItemGrid();
			ItemGrid itemGrid130 = new ItemGrid();
			ItemGrid itemGrid131 = new ItemGrid();
			ItemGrid itemGrid132 = new ItemGrid();
			ItemGrid itemGrid133 = new ItemGrid();
			ItemGrid itemGrid134 = new ItemGrid();
			ItemGrid itemGrid135 = new ItemGrid();
			ItemGrid itemGrid136 = new ItemGrid();
			ItemGrid itemGrid137 = new ItemGrid();
			ItemGrid itemGrid138 = new ItemGrid();
			ItemGrid itemGrid139 = new ItemGrid();
			ItemGrid itemGrid140 = new ItemGrid();
			ItemGrid itemGrid141 = new ItemGrid();
			ItemGrid itemGrid142 = new ItemGrid();
			ItemGrid itemGrid143 = new ItemGrid();
			ItemGrid itemGrid144 = new ItemGrid();
			ItemGrid itemGrid145 = new ItemGrid();
			ItemGrid itemGrid146 = new ItemGrid();
			ItemGrid itemGrid147 = new ItemGrid();
			ItemGrid itemGrid148 = new ItemGrid();
			ItemGrid itemGrid149 = new ItemGrid();
			ItemGrid itemGrid150 = new ItemGrid();
			ItemGrid itemGrid151 = new ItemGrid();
			ItemGrid itemGrid152 = new ItemGrid();
			ItemGrid itemGrid153 = new ItemGrid();
			ItemGrid itemGrid154 = new ItemGrid();
			ItemGrid itemGrid155 = new ItemGrid();
			ItemGrid itemGrid156 = new ItemGrid();
			ItemGrid itemGrid157 = new ItemGrid();
			ItemGrid itemGrid158 = new ItemGrid();
			ItemGrid itemGrid159 = new ItemGrid();
			ItemGrid itemGrid160 = new ItemGrid();
			ItemGrid itemGrid161 = new ItemGrid();
			ItemGrid itemGrid162 = new ItemGrid();
			ItemGrid itemGrid163 = new ItemGrid();
			ItemGrid itemGrid164 = new ItemGrid();
			ItemGrid itemGrid165 = new ItemGrid();
			ItemGrid itemGrid166 = new ItemGrid();
			ItemGrid itemGrid167 = new ItemGrid();
			ItemGrid itemGrid168 = new ItemGrid();
			ItemGrid itemGrid169 = new ItemGrid();
			ItemGrid itemGrid170 = new ItemGrid();
			ItemGrid itemGrid171 = new ItemGrid();
			ItemGrid itemGrid172 = new ItemGrid();
			ItemGrid itemGrid173 = new ItemGrid();
			ItemGrid itemGrid174 = new ItemGrid();
			ItemGrid itemGrid175 = new ItemGrid();
			ItemGrid itemGrid176 = new ItemGrid();
			ItemGrid itemGrid177 = new ItemGrid();
			ItemGrid itemGrid178 = new ItemGrid();
			ItemGrid itemGrid179 = new ItemGrid();
			ItemGrid itemGrid180 = new ItemGrid();
			ItemGrid itemGrid181 = new ItemGrid();
			ItemGrid itemGrid182 = new ItemGrid();
			ItemGrid itemGrid183 = new ItemGrid();
			ItemGrid itemGrid184 = new ItemGrid();
			ItemGrid itemGrid185 = new ItemGrid();
			ItemGrid itemGrid186 = new ItemGrid();
			ItemGrid itemGrid187 = new ItemGrid();
			ItemGrid itemGrid188 = new ItemGrid();
			ItemGrid itemGrid189 = new ItemGrid();
			ItemGrid itemGrid190 = new ItemGrid();
			ItemGrid itemGrid191 = new ItemGrid();
			ItemGrid itemGrid192 = new ItemGrid();
			ItemGrid itemGrid193 = new ItemGrid();
			ItemGrid itemGrid194 = new ItemGrid();
			ItemGrid itemGrid195 = new ItemGrid();
			ItemGrid itemGrid196 = new ItemGrid();
			ItemGrid itemGrid197 = new ItemGrid();
			ItemGrid itemGrid198 = new ItemGrid();
			ItemGrid itemGrid199 = new ItemGrid();
			ItemGrid itemGrid200 = new ItemGrid();
			ItemGrid itemGrid201 = new ItemGrid();
			ItemGrid itemGrid202 = new ItemGrid();
			ItemGrid itemGrid203 = new ItemGrid();
			ItemGrid itemGrid204 = new ItemGrid();
			ItemGrid itemGrid205 = new ItemGrid();
			ItemGrid itemGrid206 = new ItemGrid();
			ItemGrid itemGrid207 = new ItemGrid();
			ItemGrid itemGrid208 = new ItemGrid();
			ItemGrid itemGrid209 = new ItemGrid();
			ItemGrid itemGrid210 = new ItemGrid();
			ItemGrid itemGrid211 = new ItemGrid();
			ItemGrid itemGrid212 = new ItemGrid();
			ItemGrid itemGrid213 = new ItemGrid();
			ItemGrid itemGrid214 = new ItemGrid();
			ItemGrid itemGrid215 = new ItemGrid();
			ItemGrid itemGrid216 = new ItemGrid();
			ItemGrid itemGrid217 = new ItemGrid();
			ItemGrid itemGrid218 = new ItemGrid();
			ItemGrid itemGrid219 = new ItemGrid();
			ItemGrid itemGrid220 = new ItemGrid();
			ItemGrid itemGrid221 = new ItemGrid();
			ItemGrid itemGrid222 = new ItemGrid();
			ColumnItem columnItem36 = new ColumnItem();
			ColumnItem columnItem37 = new ColumnItem();
			ColumnItem columnItem38 = new ColumnItem();
			ColumnItem columnItem39 = new ColumnItem();
			ColumnItem columnItem40 = new ColumnItem();
			ColumnItem columnItem41 = new ColumnItem();
			ColumnItem columnItem42 = new ColumnItem();
			ColumnItem columnItem43 = new ColumnItem();
			ColumnItem columnItem44 = new ColumnItem();
			ColumnItem columnItem45 = new ColumnItem();
			ColumnItem columnItem46 = new ColumnItem();
			ColumnItem columnItem47 = new ColumnItem();
			ColumnItem columnItem48 = new ColumnItem();
			ColumnItem columnItem49 = new ColumnItem();
			ColumnItem columnItem50 = new ColumnItem();
			ColumnItem columnItem51 = new ColumnItem();
			ColumnItem columnItem52 = new ColumnItem();
			ExchangeIntraday exchangeIntraday = new ExchangeIntraday();
			this.tStripMain = new ToolStrip();
			this.tsbtnHoldingChart = new ToolStripButton();
			this.tsbtnNAV = new ToolStripButton();
			this.tsbtnPortfolio = new ToolStripButton();
			this.btnRefresh = new ToolStripButton();
			this.wcGraphVolumeSector = new ucVolumeAtPrice();
			this.wcGraphVolume = new ucVolumeAtPrice();
			this.panelNav = new Panel();
			this.monthCalendar1 = new MonthCalendar();
			this.tStripMenu = new ToolStrip();
			this.toolStripLabel1 = new ToolStripLabel();
			this.tstbStartDate = new ToolStripLabel();
			this.tsbtnSelStartDate = new ToolStripButton();
			this.toolStripLabel2 = new ToolStripLabel();
			this.tstbEndDate = new ToolStripLabel();
			this.tsbtnSelEndDate = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.tsbtnReload = new ToolStripButton();
			this.panelSET = new Panel();
			this.panelReportMenu = new Panel();
			this.toolStrip1 = new ToolStrip();
			this.tsbtnSummary = new ToolStripButton();
			this.tsbtnCreditBalance = new ToolStripButton();
			this.tsbtnTotRealizeProfit = new ToolStripButton();
			this.tsbtnConfrimSumm = new ToolStripButton();
			this.tsbtnConfrimByStock = new ToolStripButton();
			this.tssepStock = new ToolStripSeparator();
			this.tslbStock = new ToolStripLabel();
			this.tstbStock2 = new ToolStripTextBox();
			this.tsbtnClearStock = new ToolStripButton();
			this.tssepPrint = new ToolStripSeparator();
			this.tsbtnPrint = new ToolStripButton();
			this.panelTFEXPort = new Panel();
			this.pnlTradeJ = new Panel();
			this.tStripTJ = new ToolStrip();
			this.tslbAmountText = new ToolStripLabel();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.tsbtnAmount = new ToolStripButton();
			this.tstxtAmount = new ToolStripTextBox();
			this.cbText = new ComboBox();
			this.tStripMainT = new ToolStrip();
			this.tslbAccountT = new ToolStripLabel();
			this.btnRefreshT = new ToolStripButton();
			this.toolStripSeparator3 = new ToolStripSeparator();
			this.tsbtnPrintT = new ToolStripButton();
			this.tsbtnPort = new ToolStripButton();
			this.tsbtnTradeJournal = new ToolStripButton();
			this.tsbtnExportCSV = new ToolStripButton();
			this.sortGrid1 = new SortGrid();
			this.intzaTradeOverview = new IntzaCustomGrid();
			this.sortGridTfexSumm = new SortGrid();
			this.sortGridTfex = new SortGrid();
			this.intzaCustBottTfex = new IntzaCustomGrid();
			this.intzaCustHeadTfex = new IntzaCustomGrid();
			this.intzaCB_Freewill = new IntzaCustomGrid();
			this.intzaCB = new IntzaCustomGrid();
			this.intzaInfoHeader = new IntzaCustomGrid();
			this.intzaSumReport = new SortGrid();
			this.intzaReport = new SortGrid();
			this.chart = new ChartWinControl();
			this.tStripMain.SuspendLayout();
			this.panelNav.SuspendLayout();
			this.tStripMenu.SuspendLayout();
			this.panelSET.SuspendLayout();
			this.panelReportMenu.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.panelTFEXPort.SuspendLayout();
			this.pnlTradeJ.SuspendLayout();
			this.tStripTJ.SuspendLayout();
			this.tStripMainT.SuspendLayout();
			base.SuspendLayout();
			this.tStripMain.BackColor = Color.Black;
			this.tStripMain.GripStyle = ToolStripGripStyle.Hidden;
			this.tStripMain.Items.AddRange(new ToolStripItem[]
			{
				this.tsbtnHoldingChart,
				this.tsbtnNAV,
				this.tsbtnPortfolio,
				this.btnRefresh
			});
			this.tStripMain.Location = new Point(0, 0);
			this.tStripMain.Name = "tStripMain";
			this.tStripMain.Padding = new Padding(1, 1, 1, 2);
			this.tStripMain.RenderMode = ToolStripRenderMode.Professional;
			this.tStripMain.Size = new Size(863, 26);
			this.tStripMain.TabIndex = 20;
			this.tStripMain.Tag = "-1";
			this.tStripMain.Text = "toolStrip1";
			this.tsbtnHoldingChart.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnHoldingChart.ForeColor = Color.WhiteSmoke;
			this.tsbtnHoldingChart.ImageTransparentColor = Color.Magenta;
			this.tsbtnHoldingChart.Name = "tsbtnHoldingChart";
			this.tsbtnHoldingChart.Size = new Size(91, 20);
			this.tsbtnHoldingChart.Text = "Holdings Chart";
			this.tsbtnHoldingChart.Click += new EventHandler(this.ReportClick);
			this.tsbtnNAV.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnNAV.ForeColor = Color.WhiteSmoke;
			this.tsbtnNAV.ImageTransparentColor = Color.Magenta;
			this.tsbtnNAV.Name = "tsbtnNAV";
			this.tsbtnNAV.Size = new Size(35, 20);
			this.tsbtnNAV.Text = "NAV";
			this.tsbtnNAV.Click += new EventHandler(this.ReportClick);
			this.tsbtnPortfolio.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnPortfolio.ForeColor = Color.WhiteSmoke;
			this.tsbtnPortfolio.ImageTransparentColor = Color.Magenta;
			this.tsbtnPortfolio.Margin = new Padding(2, 1, 0, 2);
			this.tsbtnPortfolio.Name = "tsbtnPortfolio";
			this.tsbtnPortfolio.Size = new Size(57, 20);
			this.tsbtnPortfolio.Text = "Portfolio";
			this.tsbtnPortfolio.ToolTipText = "Credit Position";
			this.tsbtnPortfolio.Click += new EventHandler(this.ReportClick);
			this.btnRefresh.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.btnRefresh.Font = new Font("Microsoft Sans Serif", 8.25f);
			this.btnRefresh.Image = (Image)componentResourceManager.GetObject("btnRefresh.Image");
			this.btnRefresh.ImageTransparentColor = Color.Magenta;
			this.btnRefresh.Margin = new Padding(5, 1, 0, 2);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new Size(23, 20);
			this.btnRefresh.Text = "Refresh";
			this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);
			clsPermission.DisplayBuySell = enumDisplayBuySell.Yes;
			clsPermission.HistoricalDay = 30.0;
			clsPermission.Permission = enumPermission.Visible;
			clsPermission.VolType = null;
			clsPermission.WordingType = null;
			this.wcGraphVolumeSector.ActiveSET = clsPermission;
			clsPermission2.DisplayBuySell = enumDisplayBuySell.Yes;
			clsPermission2.HistoricalDay = 30.0;
			clsPermission2.Permission = enumPermission.Visible;
			clsPermission2.VolType = null;
			clsPermission2.WordingType = null;
			this.wcGraphVolumeSector.ActiveTFEX = clsPermission2;
			this.wcGraphVolumeSector.BackColor = Color.FromArgb(10, 10, 10);
			this.wcGraphVolumeSector.ColorBg = Color.FromArgb(10, 10, 10);
			this.wcGraphVolumeSector.ColorBuy = Color.Lime;
			this.wcGraphVolumeSector.ColorCeiling = Color.Aqua;
			this.wcGraphVolumeSector.ColorDown = Color.Red;
			this.wcGraphVolumeSector.ColorFloor = Color.Fuchsia;
			this.wcGraphVolumeSector.ColorGrid = Color.DarkGray;
			this.wcGraphVolumeSector.ColorNoChg = Color.Yellow;
			this.wcGraphVolumeSector.ColorSell = Color.Red;
			this.wcGraphVolumeSector.ColorUp = Color.Lime;
			this.wcGraphVolumeSector.ColorValue = Color.White;
			this.wcGraphVolumeSector.ColorVolume = Color.Yellow;
			this.wcGraphVolumeSector.CurDate = null;
			this.wcGraphVolumeSector.dictIPO = (Dictionary<string, float>)componentResourceManager.GetObject("wcGraphVolumeSector.dictIPO");
			this.wcGraphVolumeSector.FontName = "Arial";
			this.wcGraphVolumeSector.FontSize = 10f;
			this.wcGraphVolumeSector.Location = new Point(388, 118);
			this.wcGraphVolumeSector.Mode = 1;
			this.wcGraphVolumeSector.Name = "wcGraphVolumeSector";
			this.wcGraphVolumeSector.Size = new Size(87, 36);
			this.wcGraphVolumeSector.SymbolList = null;
			this.wcGraphVolumeSector.SymbolType = enumType.eSet;
			this.wcGraphVolumeSector.TabIndex = 31;
			this.wcGraphVolumeSector.TextBoxBgColor = Color.Empty;
			this.wcGraphVolumeSector.TextBoxForeColor = Color.Empty;
			this.wcGraphVolumeSector.TypeMode = enumMode.Previous;
			this.wcGraphVolumeSector.Visible = false;
			clsPermission3.DisplayBuySell = enumDisplayBuySell.Yes;
			clsPermission3.HistoricalDay = 30.0;
			clsPermission3.Permission = enumPermission.Visible;
			clsPermission3.VolType = null;
			clsPermission3.WordingType = null;
			this.wcGraphVolume.ActiveSET = clsPermission3;
			clsPermission4.DisplayBuySell = enumDisplayBuySell.Yes;
			clsPermission4.HistoricalDay = 30.0;
			clsPermission4.Permission = enumPermission.Visible;
			clsPermission4.VolType = null;
			clsPermission4.WordingType = null;
			this.wcGraphVolume.ActiveTFEX = clsPermission4;
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
			this.wcGraphVolume.Location = new Point(388, 76);
			this.wcGraphVolume.Mode = 1;
			this.wcGraphVolume.Name = "wcGraphVolume";
			this.wcGraphVolume.Size = new Size(87, 36);
			this.wcGraphVolume.SymbolList = null;
			this.wcGraphVolume.SymbolType = enumType.eSet;
			this.wcGraphVolume.TabIndex = 30;
			this.wcGraphVolume.TextBoxBgColor = Color.Empty;
			this.wcGraphVolume.TextBoxForeColor = Color.Empty;
			this.wcGraphVolume.TypeMode = enumMode.Previous;
			this.wcGraphVolume.Visible = false;
			this.panelNav.Controls.Add(this.monthCalendar1);
			this.panelNav.Controls.Add(this.chart);
			this.panelNav.Controls.Add(this.tStripMenu);
			this.panelNav.Location = new Point(481, 30);
			this.panelNav.Name = "panelNav";
			this.panelNav.Size = new Size(356, 208);
			this.panelNav.TabIndex = 54;
			this.panelNav.Visible = false;
			this.monthCalendar1.Location = new Point(42, 25);
			this.monthCalendar1.MaxDate = new DateTime(2020, 12, 31, 0, 0, 0, 0);
			this.monthCalendar1.MaxSelectionCount = 1;
			this.monthCalendar1.MinDate = new DateTime(2009, 1, 1, 0, 0, 0, 0);
			this.monthCalendar1.Name = "monthCalendar1";
			this.monthCalendar1.TabIndex = 66;
			this.monthCalendar1.Visible = false;
			this.monthCalendar1.DateSelected += new DateRangeEventHandler(this.monthCalendar1_DateSelected);
			this.monthCalendar1.Leave += new EventHandler(this.monthCalendar1_Leave);
			this.tStripMenu.BackColor = Color.FromArgb(20, 20, 20);
			this.tStripMenu.GripStyle = ToolStripGripStyle.Hidden;
			this.tStripMenu.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripLabel1,
				this.tstbStartDate,
				this.tsbtnSelStartDate,
				this.toolStripLabel2,
				this.tstbEndDate,
				this.tsbtnSelEndDate,
				this.toolStripSeparator1,
				this.tsbtnReload
			});
			this.tStripMenu.Location = new Point(0, 0);
			this.tStripMenu.Name = "tStripMenu";
			this.tStripMenu.Size = new Size(356, 25);
			this.tStripMenu.TabIndex = 64;
			this.tStripMenu.Text = "toolStrip1";
			this.toolStripLabel1.ForeColor = Color.LightGray;
			this.toolStripLabel1.Margin = new Padding(5, 1, 0, 2);
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new Size(64, 22);
			this.toolStripLabel1.Text = "Start Date :";
			this.tstbStartDate.BackColor = Color.Gray;
			this.tstbStartDate.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tstbStartDate.ForeColor = Color.Yellow;
			this.tstbStartDate.Margin = new Padding(5, 1, 5, 2);
			this.tstbStartDate.Name = "tstbStartDate";
			this.tstbStartDate.Size = new Size(55, 22);
			this.tstbStartDate.Text = "20090101";
			this.tsbtnSelStartDate.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbtnSelStartDate.Image = (Image)componentResourceManager.GetObject("tsbtnSelStartDate.Image");
			this.tsbtnSelStartDate.ImageTransparentColor = Color.Magenta;
			this.tsbtnSelStartDate.Name = "tsbtnSelStartDate";
			this.tsbtnSelStartDate.Size = new Size(23, 22);
			this.tsbtnSelStartDate.Text = "toolStripButton1";
			this.tsbtnSelStartDate.ToolTipText = "Select Date";
			this.tsbtnSelStartDate.Click += new EventHandler(this.tsbtnSelStartDate_Click);
			this.toolStripLabel2.ForeColor = Color.LightGray;
			this.toolStripLabel2.Name = "toolStripLabel2";
			this.toolStripLabel2.Size = new Size(60, 22);
			this.toolStripLabel2.Text = "End Date :";
			this.tstbEndDate.BackColor = Color.Gray;
			this.tstbEndDate.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tstbEndDate.ForeColor = Color.Yellow;
			this.tstbEndDate.Margin = new Padding(5, 1, 5, 2);
			this.tstbEndDate.Name = "tstbEndDate";
			this.tstbEndDate.Size = new Size(55, 22);
			this.tstbEndDate.Text = "20090501";
			this.tsbtnSelEndDate.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbtnSelEndDate.Image = (Image)componentResourceManager.GetObject("tsbtnSelEndDate.Image");
			this.tsbtnSelEndDate.ImageTransparentColor = Color.Magenta;
			this.tsbtnSelEndDate.Name = "tsbtnSelEndDate";
			this.tsbtnSelEndDate.Size = new Size(23, 22);
			this.tsbtnSelEndDate.Text = "toolStripButton2";
			this.tsbtnSelEndDate.ToolTipText = "Select Date";
			this.tsbtnSelEndDate.Click += new EventHandler(this.tsbtnSelEndDate_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 25);
			this.tsbtnReload.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbtnReload.Image = Resources.refresh;
			this.tsbtnReload.ImageTransparentColor = Color.Magenta;
			this.tsbtnReload.Margin = new Padding(5, 1, 0, 2);
			this.tsbtnReload.Name = "tsbtnReload";
			this.tsbtnReload.Size = new Size(23, 22);
			this.tsbtnReload.Text = "Reload";
			this.tsbtnReload.Click += new EventHandler(this.tsbtnReload_Click);
			this.panelSET.Controls.Add(this.intzaCB_Freewill);
			this.panelSET.Controls.Add(this.panelReportMenu);
			this.panelSET.Controls.Add(this.intzaCB);
			this.panelSET.Controls.Add(this.intzaInfoHeader);
			this.panelSET.Controls.Add(this.tStripMain);
			this.panelSET.Controls.Add(this.intzaSumReport);
			this.panelSET.Controls.Add(this.intzaReport);
			this.panelSET.Controls.Add(this.wcGraphVolume);
			this.panelSET.Controls.Add(this.panelNav);
			this.panelSET.Controls.Add(this.wcGraphVolumeSector);
			this.panelSET.Location = new Point(4, 5);
			this.panelSET.Name = "panelSET";
			this.panelSET.Size = new Size(863, 330);
			this.panelSET.TabIndex = 71;
			this.panelSET.Visible = false;
			this.panelReportMenu.Controls.Add(this.toolStrip1);
			this.panelReportMenu.Location = new Point(3, 261);
			this.panelReportMenu.Name = "panelReportMenu";
			this.panelReportMenu.Size = new Size(854, 35);
			this.panelReportMenu.TabIndex = 71;
			this.toolStrip1.BackColor = Color.Black;
			this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.tsbtnSummary,
				this.tsbtnCreditBalance,
				this.tsbtnTotRealizeProfit,
				this.tsbtnConfrimSumm,
				this.tsbtnConfrimByStock,
				this.tssepStock,
				this.tslbStock,
				this.tstbStock2,
				this.tsbtnClearStock,
				this.tssepPrint,
				this.tsbtnPrint
			});
			this.toolStrip1.Location = new Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Padding = new Padding(1, 1, 1, 2);
			this.toolStrip1.RenderMode = ToolStripRenderMode.Professional;
			this.toolStrip1.Size = new Size(854, 27);
			this.toolStrip1.TabIndex = 21;
			this.toolStrip1.Tag = "-1";
			this.toolStrip1.Text = "toolStrip1";
			this.tsbtnSummary.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnSummary.ForeColor = Color.WhiteSmoke;
			this.tsbtnSummary.ImageTransparentColor = Color.Magenta;
			this.tsbtnSummary.Margin = new Padding(3, 1, 3, 2);
			this.tsbtnSummary.Name = "tsbtnSummary";
			this.tsbtnSummary.Size = new Size(68, 21);
			this.tsbtnSummary.Text = "Profit/Loss";
			this.tsbtnSummary.ToolTipText = "Credit Position";
			this.tsbtnSummary.Click += new EventHandler(this.ReportClick);
			this.tsbtnCreditBalance.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnCreditBalance.Font = new Font("Microsoft Sans Serif", 8.25f);
			this.tsbtnCreditBalance.ForeColor = Color.WhiteSmoke;
			this.tsbtnCreditBalance.ImageTransparentColor = Color.Magenta;
			this.tsbtnCreditBalance.Margin = new Padding(3, 1, 3, 2);
			this.tsbtnCreditBalance.Name = "tsbtnCreditBalance";
			this.tsbtnCreditBalance.Size = new Size(38, 21);
			this.tsbtnCreditBalance.Text = "Credit";
			this.tsbtnCreditBalance.ToolTipText = "Credit Balance Information";
			this.tsbtnCreditBalance.Click += new EventHandler(this.tsbtnCreditBalance_Click);
			this.tsbtnTotRealizeProfit.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnTotRealizeProfit.ForeColor = Color.WhiteSmoke;
			this.tsbtnTotRealizeProfit.Image = (Image)componentResourceManager.GetObject("tsbtnTotRealizeProfit.Image");
			this.tsbtnTotRealizeProfit.ImageTransparentColor = Color.Magenta;
			this.tsbtnTotRealizeProfit.Margin = new Padding(3, 1, 3, 2);
			this.tsbtnTotRealizeProfit.Name = "tsbtnTotRealizeProfit";
			this.tsbtnTotRealizeProfit.Size = new Size(111, 21);
			this.tsbtnTotRealizeProfit.Text = "Total Realize/Profit";
			this.tsbtnTotRealizeProfit.Click += new EventHandler(this.ReportClick);
			this.tsbtnConfrimSumm.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnConfrimSumm.ForeColor = Color.WhiteSmoke;
			this.tsbtnConfrimSumm.Image = (Image)componentResourceManager.GetObject("tsbtnConfrimSumm.Image");
			this.tsbtnConfrimSumm.ImageTransparentColor = Color.Magenta;
			this.tsbtnConfrimSumm.Margin = new Padding(3, 1, 3, 2);
			this.tsbtnConfrimSumm.Name = "tsbtnConfrimSumm";
			this.tsbtnConfrimSumm.Size = new Size(109, 21);
			this.tsbtnConfrimSumm.Text = "Confirm Summary";
			this.tsbtnConfrimSumm.Click += new EventHandler(this.ReportClick);
			this.tsbtnConfrimByStock.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnConfrimByStock.ForeColor = Color.WhiteSmoke;
			this.tsbtnConfrimByStock.Image = (Image)componentResourceManager.GetObject("tsbtnConfrimByStock.Image");
			this.tsbtnConfrimByStock.ImageTransparentColor = Color.Magenta;
			this.tsbtnConfrimByStock.Margin = new Padding(3, 1, 3, 2);
			this.tsbtnConfrimByStock.Name = "tsbtnConfrimByStock";
			this.tsbtnConfrimByStock.Size = new Size(103, 21);
			this.tsbtnConfrimByStock.Text = "Confirm by Stock";
			this.tsbtnConfrimByStock.Click += new EventHandler(this.ReportClick);
			this.tssepStock.Name = "tssepStock";
			this.tssepStock.Size = new Size(6, 24);
			this.tslbStock.ForeColor = Color.WhiteSmoke;
			this.tslbStock.Margin = new Padding(5, 1, 0, 2);
			this.tslbStock.Name = "tslbStock";
			this.tslbStock.Size = new Size(71, 21);
			this.tslbStock.Text = "Filter Stock :";
			this.tstbStock2.AutoCompleteMode = AutoCompleteMode.Suggest;
			this.tstbStock2.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.tstbStock2.BackColor = Color.FromArgb(60, 60, 60);
			this.tstbStock2.BorderStyle = BorderStyle.FixedSingle;
			this.tstbStock2.CharacterCasing = CharacterCasing.Upper;
			this.tstbStock2.ForeColor = Color.LightGray;
			this.tstbStock2.Margin = new Padding(1, 0, 1, 1);
			this.tstbStock2.MaxLength = 12;
			this.tstbStock2.Name = "tstbStock2";
			this.tstbStock2.Size = new Size(100, 23);
			this.tstbStock2.Leave += new EventHandler(this.controlOrder_Leave);
			this.tstbStock2.Enter += new EventHandler(this.controlOrder_Enter);
			this.tstbStock2.KeyUp += new KeyEventHandler(this.tstbStock2_KeyUp);
			this.tsbtnClearStock.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnClearStock.ForeColor = Color.WhiteSmoke;
			this.tsbtnClearStock.ImageTransparentColor = Color.Magenta;
			this.tsbtnClearStock.Margin = new Padding(5, 1, 0, 2);
			this.tsbtnClearStock.Name = "tsbtnClearStock";
			this.tsbtnClearStock.Size = new Size(38, 21);
			this.tsbtnClearStock.Text = "Clear";
			this.tsbtnClearStock.ToolTipText = "Clear Stock Filter";
			this.tsbtnClearStock.Click += new EventHandler(this.tsbtnClearStock_Click);
			this.tssepPrint.Name = "tssepPrint";
			this.tssepPrint.Size = new Size(6, 24);
			this.tsbtnPrint.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnPrint.ForeColor = Color.WhiteSmoke;
			this.tsbtnPrint.ImageTransparentColor = Color.Magenta;
			this.tsbtnPrint.Margin = new Padding(5, 1, 0, 2);
			this.tsbtnPrint.Name = "tsbtnPrint";
			this.tsbtnPrint.Size = new Size(36, 21);
			this.tsbtnPrint.Text = "Print";
			this.tsbtnPrint.Click += new EventHandler(this.tsbtnPrint_Click);
			this.panelTFEXPort.Controls.Add(this.pnlTradeJ);
			this.panelTFEXPort.Controls.Add(this.sortGridTfexSumm);
			this.panelTFEXPort.Controls.Add(this.sortGridTfex);
			this.panelTFEXPort.Controls.Add(this.intzaCustBottTfex);
			this.panelTFEXPort.Controls.Add(this.intzaCustHeadTfex);
			this.panelTFEXPort.Controls.Add(this.tStripMainT);
			this.panelTFEXPort.Location = new Point(12, 331);
			this.panelTFEXPort.Name = "panelTFEXPort";
			this.panelTFEXPort.Size = new Size(852, 255);
			this.panelTFEXPort.TabIndex = 72;
			this.panelTFEXPort.Visible = false;
			this.pnlTradeJ.Controls.Add(this.tStripTJ);
			this.pnlTradeJ.Controls.Add(this.cbText);
			this.pnlTradeJ.Controls.Add(this.sortGrid1);
			this.pnlTradeJ.Controls.Add(this.intzaTradeOverview);
			this.pnlTradeJ.Location = new Point(0, 141);
			this.pnlTradeJ.Name = "pnlTradeJ";
			this.pnlTradeJ.Size = new Size(849, 111);
			this.pnlTradeJ.TabIndex = 29;
			this.pnlTradeJ.Visible = false;
			this.tStripTJ.BackColor = Color.Black;
			this.tStripTJ.Dock = DockStyle.Bottom;
			this.tStripTJ.GripStyle = ToolStripGripStyle.Hidden;
			this.tStripTJ.Items.AddRange(new ToolStripItem[]
			{
				this.tslbAmountText,
				this.toolStripSeparator2,
				this.tsbtnAmount,
				this.tstxtAmount
			});
			this.tStripTJ.Location = new Point(0, 83);
			this.tStripTJ.Margin = new Padding(1);
			this.tStripTJ.Name = "tStripTJ";
			this.tStripTJ.Padding = new Padding(1);
			this.tStripTJ.RenderMode = ToolStripRenderMode.Professional;
			this.tStripTJ.Size = new Size(849, 28);
			this.tStripTJ.TabIndex = 27;
			this.tStripTJ.Text = "toolStrip1";
			this.tslbAmountText.ForeColor = Color.WhiteSmoke;
			this.tslbAmountText.Name = "tslbAmountText";
			this.tslbAmountText.Size = new Size(144, 23);
			this.tslbAmountText.Text = "Account Start Transaction";
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new Size(6, 26);
			this.tsbtnAmount.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnAmount.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
			this.tsbtnAmount.ForeColor = Color.Cyan;
			this.tsbtnAmount.Image = (Image)componentResourceManager.GetObject("tsbtnAmount.Image");
			this.tsbtnAmount.ImageTransparentColor = Color.Magenta;
			this.tsbtnAmount.Name = "tsbtnAmount";
			this.tsbtnAmount.Size = new Size(41, 23);
			this.tsbtnAmount.Text = "0.00";
			this.tsbtnAmount.Click += new EventHandler(this.tsbtnAmount_Click);
			this.tstxtAmount.Name = "tstxtAmount";
			this.tstxtAmount.Size = new Size(100, 26);
			this.tstxtAmount.Visible = false;
			this.tstxtAmount.Leave += new EventHandler(this.tstxtAmount_Leave);
			this.tstxtAmount.KeyUp += new KeyEventHandler(this.tstxtAmount_KeyUp);
			this.tstxtAmount.TextChanged += new EventHandler(this.tstxtAmount_TextChanged);
			this.cbText.FormattingEnabled = true;
			this.cbText.Location = new Point(706, 65);
			this.cbText.Name = "cbText";
			this.cbText.Size = new Size(121, 21);
			this.cbText.TabIndex = 2;
			this.cbText.Visible = false;
			this.cbText.Leave += new EventHandler(this.cbText_Leave);
			this.cbText.KeyPress += new KeyPressEventHandler(this.cbText_KeyPress);
			this.cbText.KeyUp += new KeyEventHandler(this.cbText_KeyUp);
			this.cbText.KeyDown += new KeyEventHandler(this.cbStock_KeyDown);
			this.cbText.TextChanged += new EventHandler(this.cbText_TextChanged);
			this.tStripMainT.BackColor = Color.Black;
			this.tStripMainT.GripStyle = ToolStripGripStyle.Hidden;
			this.tStripMainT.Items.AddRange(new ToolStripItem[]
			{
				this.tslbAccountT,
				this.btnRefreshT,
				this.toolStripSeparator3,
				this.tsbtnPrintT,
				this.tsbtnPort,
				this.tsbtnTradeJournal,
				this.tsbtnExportCSV
			});
			this.tStripMainT.Location = new Point(0, 0);
			this.tStripMainT.Margin = new Padding(1);
			this.tStripMainT.Name = "tStripMainT";
			this.tStripMainT.Padding = new Padding(1);
			this.tStripMainT.RenderMode = ToolStripRenderMode.Professional;
			this.tStripMainT.Size = new Size(852, 25);
			this.tStripMainT.TabIndex = 26;
			this.tStripMainT.Text = "toolStrip1";
			this.tStripMainT.ItemClicked += new ToolStripItemClickedEventHandler(this.tStripMainT_ItemClicked);
			this.tslbAccountT.BackColor = Color.Black;
			this.tslbAccountT.Font = new Font("Tahoma", 8.25f);
			this.tslbAccountT.ForeColor = Color.Turquoise;
			this.tslbAccountT.Margin = new Padding(1);
			this.tslbAccountT.Name = "tslbAccountT";
			this.tslbAccountT.Padding = new Padding(3, 0, 3, 0);
			this.tslbAccountT.Size = new Size(49, 21);
			this.tslbAccountT.Text = "000000";
			this.tslbAccountT.ToolTipText = "Account";
			this.btnRefreshT.BackColor = Color.Black;
			this.btnRefreshT.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.btnRefreshT.Font = new Font("Microsoft Sans Serif", 8.25f);
			this.btnRefreshT.ForeColor = Color.LightGray;
			this.btnRefreshT.Image = (Image)componentResourceManager.GetObject("btnRefreshT.Image");
			this.btnRefreshT.ImageTransparentColor = Color.Magenta;
			this.btnRefreshT.Name = "btnRefreshT";
			this.btnRefreshT.Size = new Size(23, 20);
			this.btnRefreshT.Text = "Refresh";
			this.btnRefreshT.Click += new EventHandler(this.btnRefresh_Click);
			this.toolStripSeparator3.BackColor = Color.Black;
			this.toolStripSeparator3.ForeColor = Color.LightGray;
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new Size(6, 23);
			this.tsbtnPrintT.Alignment = ToolStripItemAlignment.Right;
			this.tsbtnPrintT.BackColor = Color.Black;
			this.tsbtnPrintT.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnPrintT.ForeColor = Color.LightGray;
			this.tsbtnPrintT.ImageTransparentColor = Color.Magenta;
			this.tsbtnPrintT.Name = "tsbtnPrintT";
			this.tsbtnPrintT.Size = new Size(36, 20);
			this.tsbtnPrintT.Text = "Print";
			this.tsbtnPrintT.Click += new EventHandler(this.tsbtnPrintT_Click);
			this.tsbtnPort.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnPort.ForeColor = Color.WhiteSmoke;
			this.tsbtnPort.Image = (Image)componentResourceManager.GetObject("tsbtnPort.Image");
			this.tsbtnPort.ImageTransparentColor = Color.Magenta;
			this.tsbtnPort.Name = "tsbtnPort";
			this.tsbtnPort.Size = new Size(57, 20);
			this.tsbtnPort.Text = "Portfolio";
			this.tsbtnPort.Click += new EventHandler(this.tsbtnPort_Click);
			this.tsbtnTradeJournal.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnTradeJournal.ForeColor = Color.WhiteSmoke;
			this.tsbtnTradeJournal.Image = (Image)componentResourceManager.GetObject("tsbtnTradeJournal.Image");
			this.tsbtnTradeJournal.ImageTransparentColor = Color.Magenta;
			this.tsbtnTradeJournal.Name = "tsbtnTradeJournal";
			this.tsbtnTradeJournal.Size = new Size(82, 20);
			this.tsbtnTradeJournal.Text = "Trade Journal";
			this.tsbtnTradeJournal.Visible = false;
			this.tsbtnTradeJournal.Click += new EventHandler(this.tsbtnTradeJournal_Click);
			this.tsbtnExportCSV.ForeColor = Color.WhiteSmoke;
			this.tsbtnExportCSV.Image = (Image)componentResourceManager.GetObject("tsbtnExportCSV.Image");
			this.tsbtnExportCSV.ImageTransparentColor = Color.Magenta;
			this.tsbtnExportCSV.Name = "tsbtnExportCSV";
			this.tsbtnExportCSV.Size = new Size(60, 20);
			this.tsbtnExportCSV.Text = "Export";
			this.tsbtnExportCSV.Visible = false;
			this.tsbtnExportCSV.Click += new EventHandler(this.tsbtnExportCSV_Click);
			this.sortGrid1.AllowDrop = true;
			this.sortGrid1.BackColor = Color.FromArgb(10, 10, 10);
			this.sortGrid1.CanBlink = true;
			this.sortGrid1.CanDrag = false;
			this.sortGrid1.CanGetMouseMove = false;
			columnItem.Alignment = StringAlignment.Center;
			columnItem.BackColor = Color.FromArgb(64, 64, 64);
			columnItem.ColumnAlignment = StringAlignment.Center;
			columnItem.FontColor = Color.LightGray;
			columnItem.MyStyle = FontStyle.Regular;
			columnItem.Name = "dateopen";
			columnItem.Text = "Date Open";
			columnItem.ValueFormat = FormatType.Text;
			columnItem.Visible = true;
			columnItem.Width = 8;
			columnItem2.Alignment = StringAlignment.Center;
			columnItem2.BackColor = Color.FromArgb(64, 64, 64);
			columnItem2.ColumnAlignment = StringAlignment.Center;
			columnItem2.FontColor = Color.LightGray;
			columnItem2.MyStyle = FontStyle.Regular;
			columnItem2.Name = "dateclose";
			columnItem2.Text = "Date Close";
			columnItem2.ValueFormat = FormatType.Text;
			columnItem2.Visible = true;
			columnItem2.Width = 8;
			columnItem3.Alignment = StringAlignment.Center;
			columnItem3.BackColor = Color.FromArgb(64, 64, 64);
			columnItem3.ColumnAlignment = StringAlignment.Center;
			columnItem3.FontColor = Color.LightGray;
			columnItem3.MyStyle = FontStyle.Regular;
			columnItem3.Name = "direction";
			columnItem3.Text = "L/S";
			columnItem3.ValueFormat = FormatType.Text;
			columnItem3.Visible = true;
			columnItem3.Width = 4;
			columnItem4.Alignment = StringAlignment.Far;
			columnItem4.BackColor = Color.FromArgb(64, 64, 64);
			columnItem4.ColumnAlignment = StringAlignment.Center;
			columnItem4.FontColor = Color.LightGray;
			columnItem4.MyStyle = FontStyle.Regular;
			columnItem4.Name = "positionsize";
			columnItem4.Text = "Position Size";
			columnItem4.ValueFormat = FormatType.Text;
			columnItem4.Visible = false;
			columnItem4.Width = 10;
			columnItem5.Alignment = StringAlignment.Far;
			columnItem5.BackColor = Color.FromArgb(64, 64, 64);
			columnItem5.ColumnAlignment = StringAlignment.Center;
			columnItem5.FontColor = Color.LightGray;
			columnItem5.MyStyle = FontStyle.Regular;
			columnItem5.Name = "entryprice";
			columnItem5.Text = "Entry Price";
			columnItem5.ValueFormat = FormatType.Text;
			columnItem5.Visible = true;
			columnItem5.Width = 8;
			columnItem6.Alignment = StringAlignment.Far;
			columnItem6.BackColor = Color.FromArgb(64, 64, 64);
			columnItem6.ColumnAlignment = StringAlignment.Center;
			columnItem6.FontColor = Color.LightGray;
			columnItem6.MyStyle = FontStyle.Regular;
			columnItem6.Name = "stoploss";
			columnItem6.Text = "Stop Loss";
			columnItem6.ValueFormat = FormatType.Text;
			columnItem6.Visible = true;
			columnItem6.Width = 8;
			columnItem7.Alignment = StringAlignment.Far;
			columnItem7.BackColor = Color.FromArgb(64, 64, 64);
			columnItem7.ColumnAlignment = StringAlignment.Center;
			columnItem7.FontColor = Color.LightGray;
			columnItem7.MyStyle = FontStyle.Regular;
			columnItem7.Name = "takeprofit";
			columnItem7.Text = "Take Profit";
			columnItem7.ValueFormat = FormatType.Text;
			columnItem7.Visible = true;
			columnItem7.Width = 8;
			columnItem8.Alignment = StringAlignment.Far;
			columnItem8.BackColor = Color.FromArgb(64, 64, 64);
			columnItem8.ColumnAlignment = StringAlignment.Center;
			columnItem8.FontColor = Color.LightGray;
			columnItem8.MyStyle = FontStyle.Regular;
			columnItem8.Name = "exitprice";
			columnItem8.Text = "Exit Price";
			columnItem8.ValueFormat = FormatType.Text;
			columnItem8.Visible = true;
			columnItem8.Width = 8;
			columnItem9.Alignment = StringAlignment.Center;
			columnItem9.BackColor = Color.Teal;
			columnItem9.ColumnAlignment = StringAlignment.Center;
			columnItem9.FontColor = Color.LightGray;
			columnItem9.MyStyle = FontStyle.Regular;
			columnItem9.Name = "riskreward";
			columnItem9.Text = "Risk/Reward";
			columnItem9.ValueFormat = FormatType.Text;
			columnItem9.Visible = true;
			columnItem9.Width = 8;
			columnItem10.Alignment = StringAlignment.Far;
			columnItem10.BackColor = Color.FromArgb(64, 64, 64);
			columnItem10.ColumnAlignment = StringAlignment.Center;
			columnItem10.FontColor = Color.LightGray;
			columnItem10.MyStyle = FontStyle.Regular;
			columnItem10.Name = "loss";
			columnItem10.Text = "Loss";
			columnItem10.ValueFormat = FormatType.Text;
			columnItem10.Visible = true;
			columnItem10.Width = 8;
			columnItem11.Alignment = StringAlignment.Far;
			columnItem11.BackColor = Color.FromArgb(64, 64, 64);
			columnItem11.ColumnAlignment = StringAlignment.Center;
			columnItem11.FontColor = Color.LightGray;
			columnItem11.MyStyle = FontStyle.Regular;
			columnItem11.Name = "profit";
			columnItem11.Text = "Profit";
			columnItem11.ValueFormat = FormatType.Text;
			columnItem11.Visible = true;
			columnItem11.Width = 8;
			columnItem12.Alignment = StringAlignment.Far;
			columnItem12.BackColor = Color.Teal;
			columnItem12.ColumnAlignment = StringAlignment.Center;
			columnItem12.FontColor = Color.LightGray;
			columnItem12.MyStyle = FontStyle.Regular;
			columnItem12.Name = "profitpct";
			columnItem12.Text = "% Profit";
			columnItem12.ValueFormat = FormatType.Text;
			columnItem12.Visible = true;
			columnItem12.Width = 7;
			columnItem13.Alignment = StringAlignment.Far;
			columnItem13.BackColor = Color.Teal;
			columnItem13.ColumnAlignment = StringAlignment.Center;
			columnItem13.FontColor = Color.LightGray;
			columnItem13.MyStyle = FontStyle.Regular;
			columnItem13.Name = "losspct";
			columnItem13.Text = "% Loss";
			columnItem13.ValueFormat = FormatType.Text;
			columnItem13.Visible = true;
			columnItem13.Width = 7;
			columnItem14.Alignment = StringAlignment.Far;
			columnItem14.BackColor = Color.Teal;
			columnItem14.ColumnAlignment = StringAlignment.Center;
			columnItem14.FontColor = Color.LightGray;
			columnItem14.MyStyle = FontStyle.Regular;
			columnItem14.Name = "balance";
			columnItem14.Text = "Balance";
			columnItem14.ValueFormat = FormatType.Text;
			columnItem14.Visible = true;
			columnItem14.Width = 10;
			columnItem15.Alignment = StringAlignment.Near;
			columnItem15.BackColor = Color.FromArgb(64, 64, 64);
			columnItem15.ColumnAlignment = StringAlignment.Center;
			columnItem15.FontColor = Color.LightGray;
			columnItem15.MyStyle = FontStyle.Regular;
			columnItem15.Name = "tmpbalance";
			columnItem15.Text = "None";
			columnItem15.ValueFormat = FormatType.Text;
			columnItem15.Visible = false;
			columnItem15.Width = 10;
			columnItem16.Alignment = StringAlignment.Near;
			columnItem16.BackColor = Color.FromArgb(64, 64, 64);
			columnItem16.ColumnAlignment = StringAlignment.Center;
			columnItem16.FontColor = Color.LightGray;
			columnItem16.MyStyle = FontStyle.Regular;
			columnItem16.Name = "winloss";
			columnItem16.Text = "None";
			columnItem16.ValueFormat = FormatType.Text;
			columnItem16.Visible = false;
			columnItem16.Width = 10;
			this.sortGrid1.Columns.Add(columnItem);
			this.sortGrid1.Columns.Add(columnItem2);
			this.sortGrid1.Columns.Add(columnItem3);
			this.sortGrid1.Columns.Add(columnItem4);
			this.sortGrid1.Columns.Add(columnItem5);
			this.sortGrid1.Columns.Add(columnItem6);
			this.sortGrid1.Columns.Add(columnItem7);
			this.sortGrid1.Columns.Add(columnItem8);
			this.sortGrid1.Columns.Add(columnItem9);
			this.sortGrid1.Columns.Add(columnItem10);
			this.sortGrid1.Columns.Add(columnItem11);
			this.sortGrid1.Columns.Add(columnItem12);
			this.sortGrid1.Columns.Add(columnItem13);
			this.sortGrid1.Columns.Add(columnItem14);
			this.sortGrid1.Columns.Add(columnItem15);
			this.sortGrid1.Columns.Add(columnItem16);
			this.sortGrid1.CurrentScroll = 0;
			this.sortGrid1.FocusItemIndex = -1;
			this.sortGrid1.ForeColor = Color.Black;
			this.sortGrid1.GridColor = Color.FromArgb(45, 45, 45);
			this.sortGrid1.HeaderPctHeight = 100f;
			this.sortGrid1.IsAutoRepaint = true;
			this.sortGrid1.IsDrawFullRow = false;
			this.sortGrid1.IsDrawGrid = true;
			this.sortGrid1.IsDrawHeader = true;
			this.sortGrid1.IsScrollable = true;
			this.sortGrid1.Location = new Point(1, 46);
			this.sortGrid1.MainColumn = "";
			this.sortGrid1.Name = "sortGrid1";
			this.sortGrid1.Rows = 25;
			this.sortGrid1.RowSelectColor = Color.MediumBlue;
			this.sortGrid1.RowSelectType = 0;
			this.sortGrid1.RowsVisible = 25;
			this.sortGrid1.ScrollChennelColor = Color.LightGray;
			this.sortGrid1.Size = new Size(846, 40);
			this.sortGrid1.SortColumnName = "";
			this.sortGrid1.SortType = SortType.Desc;
			this.sortGrid1.TabIndex = 1;
			this.sortGrid1.TableMouseClick += new SortGrid.TableMouseClickEventHandler(this.sortGrid1_TableMouseClick);
			this.sortGrid1.TableMouseDoubleClick += new SortGrid.TableMouseDoubleClickEventHandler(this.sortGrid1_TableMouseDoubleClick);
			this.intzaTradeOverview.AllowDrop = true;
			this.intzaTradeOverview.BackColor = Color.Black;
			this.intzaTradeOverview.CanDrag = false;
			this.intzaTradeOverview.IsAutoRepaint = true;
			this.intzaTradeOverview.IsDroped = false;
			itemGrid.AdjustFontSize = 0f;
			itemGrid.Alignment = StringAlignment.Center;
			itemGrid.BackColor = Color.DimGray;
			itemGrid.Changed = false;
			itemGrid.FieldType = ItemType.TextGradient;
			itemGrid.FontColor = Color.White;
			itemGrid.FontStyle = FontStyle.Underline;
			itemGrid.Height = 1;
			itemGrid.IsBlink = 0;
			itemGrid.Name = "lbTJOverviewHeader";
			itemGrid.Text = "Trade Journal Overview";
			itemGrid.ValueFormat = FormatType.Text;
			itemGrid.Visible = true;
			itemGrid.Width = 100;
			itemGrid.X = 0;
			itemGrid.Y = 0;
			itemGrid2.AdjustFontSize = 0f;
			itemGrid2.Alignment = StringAlignment.Near;
			itemGrid2.BackColor = Color.Black;
			itemGrid2.Changed = false;
			itemGrid2.FieldType = ItemType.Label2;
			itemGrid2.FontColor = Color.White;
			itemGrid2.FontStyle = FontStyle.Regular;
			itemGrid2.Height = 1;
			itemGrid2.IsBlink = 0;
			itemGrid2.Name = "lbEquityInvest";
			itemGrid2.Text = "Equity Invested ";
			itemGrid2.ValueFormat = FormatType.Text;
			itemGrid2.Visible = true;
			itemGrid2.Width = 12;
			itemGrid2.X = 0;
			itemGrid2.Y = 1;
			itemGrid3.AdjustFontSize = 0f;
			itemGrid3.Alignment = StringAlignment.Far;
			itemGrid3.BackColor = Color.Black;
			itemGrid3.Changed = false;
			itemGrid3.FieldType = ItemType.Text;
			itemGrid3.FontColor = Color.Yellow;
			itemGrid3.FontStyle = FontStyle.Regular;
			itemGrid3.Height = 1;
			itemGrid3.IsBlink = 0;
			itemGrid3.Name = "tbEquityInvest";
			itemGrid3.Text = "";
			itemGrid3.ValueFormat = FormatType.Text;
			itemGrid3.Visible = true;
			itemGrid3.Width = 10;
			itemGrid3.X = 12;
			itemGrid3.Y = 1;
			itemGrid4.AdjustFontSize = 0f;
			itemGrid4.Alignment = StringAlignment.Near;
			itemGrid4.BackColor = Color.Black;
			itemGrid4.Changed = false;
			itemGrid4.FieldType = ItemType.Label2;
			itemGrid4.FontColor = Color.White;
			itemGrid4.FontStyle = FontStyle.Regular;
			itemGrid4.Height = 1;
			itemGrid4.IsBlink = 0;
			itemGrid4.Name = "lbCurrEquity";
			itemGrid4.Text = "Current Equity";
			itemGrid4.ValueFormat = FormatType.Text;
			itemGrid4.Visible = true;
			itemGrid4.Width = 12;
			itemGrid4.X = 0;
			itemGrid4.Y = 2;
			itemGrid5.AdjustFontSize = 0f;
			itemGrid5.Alignment = StringAlignment.Far;
			itemGrid5.BackColor = Color.Black;
			itemGrid5.Changed = false;
			itemGrid5.FieldType = ItemType.Text;
			itemGrid5.FontColor = Color.Yellow;
			itemGrid5.FontStyle = FontStyle.Regular;
			itemGrid5.Height = 1;
			itemGrid5.IsBlink = 0;
			itemGrid5.Name = "tbCurrEquity";
			itemGrid5.Text = "";
			itemGrid5.ValueFormat = FormatType.Text;
			itemGrid5.Visible = true;
			itemGrid5.Width = 10;
			itemGrid5.X = 12;
			itemGrid5.Y = 2;
			itemGrid6.AdjustFontSize = 0f;
			itemGrid6.Alignment = StringAlignment.Near;
			itemGrid6.BackColor = Color.Black;
			itemGrid6.Changed = false;
			itemGrid6.FieldType = ItemType.Label2;
			itemGrid6.FontColor = Color.White;
			itemGrid6.FontStyle = FontStyle.Regular;
			itemGrid6.Height = 1;
			itemGrid6.IsBlink = 0;
			itemGrid6.Name = "lbTotalPL";
			itemGrid6.Text = "Total Profit/Loss";
			itemGrid6.ValueFormat = FormatType.Text;
			itemGrid6.Visible = true;
			itemGrid6.Width = 12;
			itemGrid6.X = 0;
			itemGrid6.Y = 3;
			itemGrid7.AdjustFontSize = 0f;
			itemGrid7.Alignment = StringAlignment.Far;
			itemGrid7.BackColor = Color.Black;
			itemGrid7.Changed = false;
			itemGrid7.FieldType = ItemType.Text;
			itemGrid7.FontColor = Color.Yellow;
			itemGrid7.FontStyle = FontStyle.Regular;
			itemGrid7.Height = 1;
			itemGrid7.IsBlink = 0;
			itemGrid7.Name = "tbTotalPL";
			itemGrid7.Text = "";
			itemGrid7.ValueFormat = FormatType.Text;
			itemGrid7.Visible = true;
			itemGrid7.Width = 10;
			itemGrid7.X = 12;
			itemGrid7.Y = 3;
			itemGrid8.AdjustFontSize = 0f;
			itemGrid8.Alignment = StringAlignment.Near;
			itemGrid8.BackColor = Color.Black;
			itemGrid8.Changed = false;
			itemGrid8.FieldType = ItemType.Label2;
			itemGrid8.FontColor = Color.White;
			itemGrid8.FontStyle = FontStyle.Regular;
			itemGrid8.Height = 1;
			itemGrid8.IsBlink = 0;
			itemGrid8.Name = "lbTotalPLPct";
			itemGrid8.Text = "Total Profit/Loss %";
			itemGrid8.ValueFormat = FormatType.Text;
			itemGrid8.Visible = true;
			itemGrid8.Width = 12;
			itemGrid8.X = 0;
			itemGrid8.Y = 4;
			itemGrid9.AdjustFontSize = 0f;
			itemGrid9.Alignment = StringAlignment.Far;
			itemGrid9.BackColor = Color.Black;
			itemGrid9.Changed = false;
			itemGrid9.FieldType = ItemType.Text;
			itemGrid9.FontColor = Color.Yellow;
			itemGrid9.FontStyle = FontStyle.Regular;
			itemGrid9.Height = 1;
			itemGrid9.IsBlink = 0;
			itemGrid9.Name = "tbTotalPLPct";
			itemGrid9.Text = "";
			itemGrid9.ValueFormat = FormatType.Text;
			itemGrid9.Visible = true;
			itemGrid9.Width = 10;
			itemGrid9.X = 12;
			itemGrid9.Y = 4;
			itemGrid10.AdjustFontSize = 0f;
			itemGrid10.Alignment = StringAlignment.Near;
			itemGrid10.BackColor = Color.Black;
			itemGrid10.Changed = false;
			itemGrid10.FieldType = ItemType.Label2;
			itemGrid10.FontColor = Color.White;
			itemGrid10.FontStyle = FontStyle.Regular;
			itemGrid10.Height = 1;
			itemGrid10.IsBlink = 0;
			itemGrid10.Name = "lbTotalTrade";
			itemGrid10.Text = "Total Trades";
			itemGrid10.ValueFormat = FormatType.Text;
			itemGrid10.Visible = true;
			itemGrid10.Width = 10;
			itemGrid10.X = 24;
			itemGrid10.Y = 1;
			itemGrid11.AdjustFontSize = 0f;
			itemGrid11.Alignment = StringAlignment.Center;
			itemGrid11.BackColor = Color.Black;
			itemGrid11.Changed = false;
			itemGrid11.FieldType = ItemType.Text;
			itemGrid11.FontColor = Color.Yellow;
			itemGrid11.FontStyle = FontStyle.Regular;
			itemGrid11.Height = 1;
			itemGrid11.IsBlink = 0;
			itemGrid11.Name = "tbTotalTrade";
			itemGrid11.Text = "";
			itemGrid11.ValueFormat = FormatType.Text;
			itemGrid11.Visible = true;
			itemGrid11.Width = 5;
			itemGrid11.X = 34;
			itemGrid11.Y = 1;
			itemGrid12.AdjustFontSize = 0f;
			itemGrid12.Alignment = StringAlignment.Center;
			itemGrid12.BackColor = Color.Black;
			itemGrid12.Changed = false;
			itemGrid12.FieldType = ItemType.Text;
			itemGrid12.FontColor = Color.Yellow;
			itemGrid12.FontStyle = FontStyle.Regular;
			itemGrid12.Height = 1;
			itemGrid12.IsBlink = 0;
			itemGrid12.Name = "tbTotalTradePct";
			itemGrid12.Text = "";
			itemGrid12.ValueFormat = FormatType.Text;
			itemGrid12.Visible = true;
			itemGrid12.Width = 7;
			itemGrid12.X = 39;
			itemGrid12.Y = 1;
			itemGrid13.AdjustFontSize = 0f;
			itemGrid13.Alignment = StringAlignment.Near;
			itemGrid13.BackColor = Color.Black;
			itemGrid13.Changed = false;
			itemGrid13.FieldType = ItemType.Label2;
			itemGrid13.FontColor = Color.White;
			itemGrid13.FontStyle = FontStyle.Regular;
			itemGrid13.Height = 1;
			itemGrid13.IsBlink = 0;
			itemGrid13.Name = "lbLongTrade";
			itemGrid13.Text = "Long Trades";
			itemGrid13.ValueFormat = FormatType.Text;
			itemGrid13.Visible = true;
			itemGrid13.Width = 10;
			itemGrid13.X = 24;
			itemGrid13.Y = 2;
			itemGrid14.AdjustFontSize = 0f;
			itemGrid14.Alignment = StringAlignment.Center;
			itemGrid14.BackColor = Color.Black;
			itemGrid14.Changed = false;
			itemGrid14.FieldType = ItemType.Text;
			itemGrid14.FontColor = Color.Yellow;
			itemGrid14.FontStyle = FontStyle.Regular;
			itemGrid14.Height = 1;
			itemGrid14.IsBlink = 0;
			itemGrid14.Name = "tbLongTrade";
			itemGrid14.Text = "";
			itemGrid14.ValueFormat = FormatType.Text;
			itemGrid14.Visible = true;
			itemGrid14.Width = 5;
			itemGrid14.X = 34;
			itemGrid14.Y = 2;
			itemGrid15.AdjustFontSize = 0f;
			itemGrid15.Alignment = StringAlignment.Center;
			itemGrid15.BackColor = Color.Black;
			itemGrid15.Changed = false;
			itemGrid15.FieldType = ItemType.Text;
			itemGrid15.FontColor = Color.Yellow;
			itemGrid15.FontStyle = FontStyle.Regular;
			itemGrid15.Height = 1;
			itemGrid15.IsBlink = 0;
			itemGrid15.Name = "tbLongTradePct";
			itemGrid15.Text = "";
			itemGrid15.ValueFormat = FormatType.Text;
			itemGrid15.Visible = true;
			itemGrid15.Width = 7;
			itemGrid15.X = 39;
			itemGrid15.Y = 2;
			itemGrid16.AdjustFontSize = 0f;
			itemGrid16.Alignment = StringAlignment.Near;
			itemGrid16.BackColor = Color.Black;
			itemGrid16.Changed = false;
			itemGrid16.FieldType = ItemType.Label2;
			itemGrid16.FontColor = Color.White;
			itemGrid16.FontStyle = FontStyle.Regular;
			itemGrid16.Height = 1;
			itemGrid16.IsBlink = 0;
			itemGrid16.Name = "lbShortTrade";
			itemGrid16.Text = "Short Trades";
			itemGrid16.ValueFormat = FormatType.Text;
			itemGrid16.Visible = true;
			itemGrid16.Width = 10;
			itemGrid16.X = 24;
			itemGrid16.Y = 3;
			itemGrid17.AdjustFontSize = 0f;
			itemGrid17.Alignment = StringAlignment.Center;
			itemGrid17.BackColor = Color.Black;
			itemGrid17.Changed = false;
			itemGrid17.FieldType = ItemType.Text;
			itemGrid17.FontColor = Color.Yellow;
			itemGrid17.FontStyle = FontStyle.Regular;
			itemGrid17.Height = 1;
			itemGrid17.IsBlink = 0;
			itemGrid17.Name = "tbShortTrade";
			itemGrid17.Text = "";
			itemGrid17.ValueFormat = FormatType.Text;
			itemGrid17.Visible = true;
			itemGrid17.Width = 5;
			itemGrid17.X = 34;
			itemGrid17.Y = 3;
			itemGrid18.AdjustFontSize = 0f;
			itemGrid18.Alignment = StringAlignment.Center;
			itemGrid18.BackColor = Color.Black;
			itemGrid18.Changed = false;
			itemGrid18.FieldType = ItemType.Text;
			itemGrid18.FontColor = Color.Yellow;
			itemGrid18.FontStyle = FontStyle.Regular;
			itemGrid18.Height = 1;
			itemGrid18.IsBlink = 0;
			itemGrid18.Name = "tbShortTradePct";
			itemGrid18.Text = "";
			itemGrid18.ValueFormat = FormatType.Text;
			itemGrid18.Visible = true;
			itemGrid18.Width = 7;
			itemGrid18.X = 39;
			itemGrid18.Y = 3;
			itemGrid19.AdjustFontSize = 0f;
			itemGrid19.Alignment = StringAlignment.Near;
			itemGrid19.BackColor = Color.Black;
			itemGrid19.Changed = false;
			itemGrid19.FieldType = ItemType.Label2;
			itemGrid19.FontColor = Color.White;
			itemGrid19.FontStyle = FontStyle.Regular;
			itemGrid19.Height = 1;
			itemGrid19.IsBlink = 0;
			itemGrid19.Name = "lbWinTrade";
			itemGrid19.Text = "Winning Trades";
			itemGrid19.ValueFormat = FormatType.Text;
			itemGrid19.Visible = true;
			itemGrid19.Width = 11;
			itemGrid19.X = 47;
			itemGrid19.Y = 1;
			itemGrid20.AdjustFontSize = 0f;
			itemGrid20.Alignment = StringAlignment.Center;
			itemGrid20.BackColor = Color.Black;
			itemGrid20.Changed = false;
			itemGrid20.FieldType = ItemType.Text;
			itemGrid20.FontColor = Color.Yellow;
			itemGrid20.FontStyle = FontStyle.Regular;
			itemGrid20.Height = 1;
			itemGrid20.IsBlink = 0;
			itemGrid20.Name = "tbWinTrade";
			itemGrid20.Text = "";
			itemGrid20.ValueFormat = FormatType.Text;
			itemGrid20.Visible = true;
			itemGrid20.Width = 5;
			itemGrid20.X = 58;
			itemGrid20.Y = 1;
			itemGrid21.AdjustFontSize = 0f;
			itemGrid21.Alignment = StringAlignment.Center;
			itemGrid21.BackColor = Color.Black;
			itemGrid21.Changed = false;
			itemGrid21.FieldType = ItemType.Text;
			itemGrid21.FontColor = Color.Yellow;
			itemGrid21.FontStyle = FontStyle.Regular;
			itemGrid21.Height = 1;
			itemGrid21.IsBlink = 0;
			itemGrid21.Name = "tbWinTradePct";
			itemGrid21.Text = "";
			itemGrid21.ValueFormat = FormatType.Text;
			itemGrid21.Visible = true;
			itemGrid21.Width = 7;
			itemGrid21.X = 63;
			itemGrid21.Y = 1;
			itemGrid22.AdjustFontSize = 0f;
			itemGrid22.Alignment = StringAlignment.Near;
			itemGrid22.BackColor = Color.Black;
			itemGrid22.Changed = false;
			itemGrid22.FieldType = ItemType.Label2;
			itemGrid22.FontColor = Color.White;
			itemGrid22.FontStyle = FontStyle.Regular;
			itemGrid22.Height = 1;
			itemGrid22.IsBlink = 0;
			itemGrid22.Name = "lbWinLong";
			itemGrid22.Text = "Winning Long";
			itemGrid22.ValueFormat = FormatType.Text;
			itemGrid22.Visible = true;
			itemGrid22.Width = 11;
			itemGrid22.X = 47;
			itemGrid22.Y = 2;
			itemGrid23.AdjustFontSize = 0f;
			itemGrid23.Alignment = StringAlignment.Center;
			itemGrid23.BackColor = Color.Black;
			itemGrid23.Changed = false;
			itemGrid23.FieldType = ItemType.Text;
			itemGrid23.FontColor = Color.Yellow;
			itemGrid23.FontStyle = FontStyle.Regular;
			itemGrid23.Height = 1;
			itemGrid23.IsBlink = 0;
			itemGrid23.Name = "tbWinLong";
			itemGrid23.Text = "";
			itemGrid23.ValueFormat = FormatType.Text;
			itemGrid23.Visible = true;
			itemGrid23.Width = 5;
			itemGrid23.X = 58;
			itemGrid23.Y = 2;
			itemGrid24.AdjustFontSize = 0f;
			itemGrid24.Alignment = StringAlignment.Center;
			itemGrid24.BackColor = Color.Black;
			itemGrid24.Changed = false;
			itemGrid24.FieldType = ItemType.Text;
			itemGrid24.FontColor = Color.Yellow;
			itemGrid24.FontStyle = FontStyle.Regular;
			itemGrid24.Height = 1;
			itemGrid24.IsBlink = 0;
			itemGrid24.Name = "tbWinLongPct";
			itemGrid24.Text = "";
			itemGrid24.ValueFormat = FormatType.Text;
			itemGrid24.Visible = true;
			itemGrid24.Width = 7;
			itemGrid24.X = 63;
			itemGrid24.Y = 2;
			itemGrid25.AdjustFontSize = 0f;
			itemGrid25.Alignment = StringAlignment.Near;
			itemGrid25.BackColor = Color.Black;
			itemGrid25.Changed = false;
			itemGrid25.FieldType = ItemType.Label2;
			itemGrid25.FontColor = Color.White;
			itemGrid25.FontStyle = FontStyle.Regular;
			itemGrid25.Height = 1;
			itemGrid25.IsBlink = 0;
			itemGrid25.Name = "lbWinShort";
			itemGrid25.Text = "Winning Short";
			itemGrid25.ValueFormat = FormatType.Text;
			itemGrid25.Visible = true;
			itemGrid25.Width = 11;
			itemGrid25.X = 47;
			itemGrid25.Y = 3;
			itemGrid26.AdjustFontSize = 0f;
			itemGrid26.Alignment = StringAlignment.Center;
			itemGrid26.BackColor = Color.Black;
			itemGrid26.Changed = false;
			itemGrid26.FieldType = ItemType.Text;
			itemGrid26.FontColor = Color.Yellow;
			itemGrid26.FontStyle = FontStyle.Regular;
			itemGrid26.Height = 1;
			itemGrid26.IsBlink = 0;
			itemGrid26.Name = "tbWinShort";
			itemGrid26.Text = "";
			itemGrid26.ValueFormat = FormatType.Text;
			itemGrid26.Visible = true;
			itemGrid26.Width = 5;
			itemGrid26.X = 58;
			itemGrid26.Y = 3;
			itemGrid27.AdjustFontSize = 0f;
			itemGrid27.Alignment = StringAlignment.Center;
			itemGrid27.BackColor = Color.Black;
			itemGrid27.Changed = false;
			itemGrid27.FieldType = ItemType.Text;
			itemGrid27.FontColor = Color.Yellow;
			itemGrid27.FontStyle = FontStyle.Regular;
			itemGrid27.Height = 1;
			itemGrid27.IsBlink = 0;
			itemGrid27.Name = "tbwinShortPct";
			itemGrid27.Text = "";
			itemGrid27.ValueFormat = FormatType.Text;
			itemGrid27.Visible = true;
			itemGrid27.Width = 7;
			itemGrid27.X = 63;
			itemGrid27.Y = 3;
			itemGrid28.AdjustFontSize = 0f;
			itemGrid28.Alignment = StringAlignment.Near;
			itemGrid28.BackColor = Color.Black;
			itemGrid28.Changed = false;
			itemGrid28.FieldType = ItemType.Label2;
			itemGrid28.FontColor = Color.White;
			itemGrid28.FontStyle = FontStyle.Regular;
			itemGrid28.Height = 1;
			itemGrid28.IsBlink = 0;
			itemGrid28.Name = "lbLoseTrade";
			itemGrid28.Text = "Losing Trades";
			itemGrid28.ValueFormat = FormatType.Text;
			itemGrid28.Visible = true;
			itemGrid28.Width = 10;
			itemGrid28.X = 71;
			itemGrid28.Y = 1;
			itemGrid29.AdjustFontSize = 0f;
			itemGrid29.Alignment = StringAlignment.Center;
			itemGrid29.BackColor = Color.Black;
			itemGrid29.Changed = false;
			itemGrid29.FieldType = ItemType.Text;
			itemGrid29.FontColor = Color.Yellow;
			itemGrid29.FontStyle = FontStyle.Regular;
			itemGrid29.Height = 1;
			itemGrid29.IsBlink = 0;
			itemGrid29.Name = "tbLoseTrade";
			itemGrid29.Text = "";
			itemGrid29.ValueFormat = FormatType.Text;
			itemGrid29.Visible = true;
			itemGrid29.Width = 5;
			itemGrid29.X = 81;
			itemGrid29.Y = 1;
			itemGrid30.AdjustFontSize = 0f;
			itemGrid30.Alignment = StringAlignment.Center;
			itemGrid30.BackColor = Color.Black;
			itemGrid30.Changed = false;
			itemGrid30.FieldType = ItemType.Text;
			itemGrid30.FontColor = Color.Yellow;
			itemGrid30.FontStyle = FontStyle.Regular;
			itemGrid30.Height = 1;
			itemGrid30.IsBlink = 0;
			itemGrid30.Name = "tbLoseTradePct";
			itemGrid30.Text = "";
			itemGrid30.ValueFormat = FormatType.Text;
			itemGrid30.Visible = true;
			itemGrid30.Width = 7;
			itemGrid30.X = 86;
			itemGrid30.Y = 1;
			itemGrid31.AdjustFontSize = 0f;
			itemGrid31.Alignment = StringAlignment.Near;
			itemGrid31.BackColor = Color.Black;
			itemGrid31.Changed = false;
			itemGrid31.FieldType = ItemType.Label2;
			itemGrid31.FontColor = Color.White;
			itemGrid31.FontStyle = FontStyle.Regular;
			itemGrid31.Height = 1;
			itemGrid31.IsBlink = 0;
			itemGrid31.Name = "lbLoseLong";
			itemGrid31.Text = "Losing Long";
			itemGrid31.ValueFormat = FormatType.Text;
			itemGrid31.Visible = true;
			itemGrid31.Width = 10;
			itemGrid31.X = 71;
			itemGrid31.Y = 2;
			itemGrid32.AdjustFontSize = 0f;
			itemGrid32.Alignment = StringAlignment.Center;
			itemGrid32.BackColor = Color.Black;
			itemGrid32.Changed = false;
			itemGrid32.FieldType = ItemType.Text;
			itemGrid32.FontColor = Color.Yellow;
			itemGrid32.FontStyle = FontStyle.Regular;
			itemGrid32.Height = 1;
			itemGrid32.IsBlink = 0;
			itemGrid32.Name = "tbLoseLong";
			itemGrid32.Text = "";
			itemGrid32.ValueFormat = FormatType.Text;
			itemGrid32.Visible = true;
			itemGrid32.Width = 5;
			itemGrid32.X = 81;
			itemGrid32.Y = 2;
			itemGrid33.AdjustFontSize = 0f;
			itemGrid33.Alignment = StringAlignment.Center;
			itemGrid33.BackColor = Color.Black;
			itemGrid33.Changed = false;
			itemGrid33.FieldType = ItemType.Text;
			itemGrid33.FontColor = Color.Yellow;
			itemGrid33.FontStyle = FontStyle.Regular;
			itemGrid33.Height = 1;
			itemGrid33.IsBlink = 0;
			itemGrid33.Name = "tbLoseLongPct";
			itemGrid33.Text = "";
			itemGrid33.ValueFormat = FormatType.Text;
			itemGrid33.Visible = true;
			itemGrid33.Width = 7;
			itemGrid33.X = 86;
			itemGrid33.Y = 2;
			itemGrid34.AdjustFontSize = 0f;
			itemGrid34.Alignment = StringAlignment.Near;
			itemGrid34.BackColor = Color.Black;
			itemGrid34.Changed = false;
			itemGrid34.FieldType = ItemType.Label2;
			itemGrid34.FontColor = Color.White;
			itemGrid34.FontStyle = FontStyle.Regular;
			itemGrid34.Height = 1;
			itemGrid34.IsBlink = 0;
			itemGrid34.Name = "lbLoseShort";
			itemGrid34.Text = "Losing Short";
			itemGrid34.ValueFormat = FormatType.Text;
			itemGrid34.Visible = true;
			itemGrid34.Width = 10;
			itemGrid34.X = 71;
			itemGrid34.Y = 3;
			itemGrid35.AdjustFontSize = 0f;
			itemGrid35.Alignment = StringAlignment.Center;
			itemGrid35.BackColor = Color.Black;
			itemGrid35.Changed = false;
			itemGrid35.FieldType = ItemType.Text;
			itemGrid35.FontColor = Color.Yellow;
			itemGrid35.FontStyle = FontStyle.Regular;
			itemGrid35.Height = 1;
			itemGrid35.IsBlink = 0;
			itemGrid35.Name = "tbLoseShort";
			itemGrid35.Text = "";
			itemGrid35.ValueFormat = FormatType.Text;
			itemGrid35.Visible = true;
			itemGrid35.Width = 5;
			itemGrid35.X = 81;
			itemGrid35.Y = 3;
			itemGrid36.AdjustFontSize = 0f;
			itemGrid36.Alignment = StringAlignment.Center;
			itemGrid36.BackColor = Color.Black;
			itemGrid36.Changed = false;
			itemGrid36.FieldType = ItemType.Text;
			itemGrid36.FontColor = Color.Yellow;
			itemGrid36.FontStyle = FontStyle.Regular;
			itemGrid36.Height = 1;
			itemGrid36.IsBlink = 0;
			itemGrid36.Name = "tbLoseShortPct";
			itemGrid36.Text = "";
			itemGrid36.ValueFormat = FormatType.Text;
			itemGrid36.Visible = true;
			itemGrid36.Width = 7;
			itemGrid36.X = 86;
			itemGrid36.Y = 3;
			this.intzaTradeOverview.Items.Add(itemGrid);
			this.intzaTradeOverview.Items.Add(itemGrid2);
			this.intzaTradeOverview.Items.Add(itemGrid3);
			this.intzaTradeOverview.Items.Add(itemGrid4);
			this.intzaTradeOverview.Items.Add(itemGrid5);
			this.intzaTradeOverview.Items.Add(itemGrid6);
			this.intzaTradeOverview.Items.Add(itemGrid7);
			this.intzaTradeOverview.Items.Add(itemGrid8);
			this.intzaTradeOverview.Items.Add(itemGrid9);
			this.intzaTradeOverview.Items.Add(itemGrid10);
			this.intzaTradeOverview.Items.Add(itemGrid11);
			this.intzaTradeOverview.Items.Add(itemGrid12);
			this.intzaTradeOverview.Items.Add(itemGrid13);
			this.intzaTradeOverview.Items.Add(itemGrid14);
			this.intzaTradeOverview.Items.Add(itemGrid15);
			this.intzaTradeOverview.Items.Add(itemGrid16);
			this.intzaTradeOverview.Items.Add(itemGrid17);
			this.intzaTradeOverview.Items.Add(itemGrid18);
			this.intzaTradeOverview.Items.Add(itemGrid19);
			this.intzaTradeOverview.Items.Add(itemGrid20);
			this.intzaTradeOverview.Items.Add(itemGrid21);
			this.intzaTradeOverview.Items.Add(itemGrid22);
			this.intzaTradeOverview.Items.Add(itemGrid23);
			this.intzaTradeOverview.Items.Add(itemGrid24);
			this.intzaTradeOverview.Items.Add(itemGrid25);
			this.intzaTradeOverview.Items.Add(itemGrid26);
			this.intzaTradeOverview.Items.Add(itemGrid27);
			this.intzaTradeOverview.Items.Add(itemGrid28);
			this.intzaTradeOverview.Items.Add(itemGrid29);
			this.intzaTradeOverview.Items.Add(itemGrid30);
			this.intzaTradeOverview.Items.Add(itemGrid31);
			this.intzaTradeOverview.Items.Add(itemGrid32);
			this.intzaTradeOverview.Items.Add(itemGrid33);
			this.intzaTradeOverview.Items.Add(itemGrid34);
			this.intzaTradeOverview.Items.Add(itemGrid35);
			this.intzaTradeOverview.Items.Add(itemGrid36);
			this.intzaTradeOverview.LineColor = Color.Red;
			this.intzaTradeOverview.Location = new Point(0, -38);
			this.intzaTradeOverview.Name = "intzaTradeOverview";
			this.intzaTradeOverview.Size = new Size(846, 84);
			this.intzaTradeOverview.TabIndex = 0;
			this.sortGridTfexSumm.AllowDrop = true;
			this.sortGridTfexSumm.BackColor = Color.FromArgb(25, 25, 25);
			this.sortGridTfexSumm.CanBlink = true;
			this.sortGridTfexSumm.CanDrag = false;
			this.sortGridTfexSumm.CanGetMouseMove = false;
			columnItem17.Alignment = StringAlignment.Far;
			columnItem17.BackColor = Color.FromArgb(64, 64, 64);
			columnItem17.ColumnAlignment = StringAlignment.Center;
			columnItem17.FontColor = Color.LightGray;
			columnItem17.MyStyle = FontStyle.Regular;
			columnItem17.Name = "last";
			columnItem17.Text = "Last";
			columnItem17.ValueFormat = FormatType.Text;
			columnItem17.Visible = true;
			columnItem17.Width = 56;
			columnItem18.Alignment = StringAlignment.Far;
			columnItem18.BackColor = Color.FromArgb(64, 64, 64);
			columnItem18.ColumnAlignment = StringAlignment.Center;
			columnItem18.FontColor = Color.LightGray;
			columnItem18.MyStyle = FontStyle.Regular;
			columnItem18.Name = "mkt_val";
			columnItem18.Text = "Mkt Value";
			columnItem18.ValueFormat = FormatType.Text;
			columnItem18.Visible = true;
			columnItem18.Width = 11;
			columnItem19.Alignment = StringAlignment.Far;
			columnItem19.BackColor = Color.FromArgb(64, 64, 64);
			columnItem19.ColumnAlignment = StringAlignment.Center;
			columnItem19.FontColor = Color.LightGray;
			columnItem19.MyStyle = FontStyle.Regular;
			columnItem19.Name = "cost_val";
			columnItem19.Text = "Cost Val";
			columnItem19.ValueFormat = FormatType.Text;
			columnItem19.Visible = true;
			columnItem19.Width = 11;
			columnItem20.Alignment = StringAlignment.Far;
			columnItem20.BackColor = Color.FromArgb(64, 64, 64);
			columnItem20.ColumnAlignment = StringAlignment.Center;
			columnItem20.FontColor = Color.LightGray;
			columnItem20.MyStyle = FontStyle.Regular;
			columnItem20.Name = "unreal_settle";
			columnItem20.Text = "Unreal(Settle)";
			columnItem20.ValueFormat = FormatType.Text;
			columnItem20.Visible = true;
			columnItem20.Width = 11;
			columnItem21.Alignment = StringAlignment.Far;
			columnItem21.BackColor = Color.FromArgb(64, 64, 64);
			columnItem21.ColumnAlignment = StringAlignment.Center;
			columnItem21.FontColor = Color.LightGray;
			columnItem21.MyStyle = FontStyle.Regular;
			columnItem21.Name = "unreal_cost";
			columnItem21.Text = "Unreal(Cost)";
			columnItem21.ValueFormat = FormatType.Text;
			columnItem21.Visible = false;
			columnItem21.Width = 11;
			columnItem22.Alignment = StringAlignment.Far;
			columnItem22.BackColor = Color.FromArgb(64, 64, 64);
			columnItem22.ColumnAlignment = StringAlignment.Center;
			columnItem22.FontColor = Color.LightGray;
			columnItem22.MyStyle = FontStyle.Regular;
			columnItem22.Name = "realize";
			columnItem22.Text = "Realize";
			columnItem22.ValueFormat = FormatType.Text;
			columnItem22.Visible = true;
			columnItem22.Width = 11;
			this.sortGridTfexSumm.Columns.Add(columnItem17);
			this.sortGridTfexSumm.Columns.Add(columnItem18);
			this.sortGridTfexSumm.Columns.Add(columnItem19);
			this.sortGridTfexSumm.Columns.Add(columnItem20);
			this.sortGridTfexSumm.Columns.Add(columnItem21);
			this.sortGridTfexSumm.Columns.Add(columnItem22);
			this.sortGridTfexSumm.CurrentScroll = 0;
			this.sortGridTfexSumm.FocusItemIndex = -1;
			this.sortGridTfexSumm.ForeColor = Color.Black;
			this.sortGridTfexSumm.GridColor = Color.FromArgb(45, 45, 45);
			this.sortGridTfexSumm.HeaderPctHeight = 80f;
			this.sortGridTfexSumm.IsAutoRepaint = true;
			this.sortGridTfexSumm.IsDrawFullRow = false;
			this.sortGridTfexSumm.IsDrawGrid = true;
			this.sortGridTfexSumm.IsDrawHeader = false;
			this.sortGridTfexSumm.IsScrollable = true;
			this.sortGridTfexSumm.Location = new Point(3, 116);
			this.sortGridTfexSumm.MainColumn = "";
			this.sortGridTfexSumm.Name = "sortGridTfexSumm";
			this.sortGridTfexSumm.Rows = 1;
			this.sortGridTfexSumm.RowSelectColor = Color.FromArgb(95, 158, 160);
			this.sortGridTfexSumm.RowSelectType = 0;
			this.sortGridTfexSumm.RowsVisible = 1;
			this.sortGridTfexSumm.ScrollChennelColor = Color.DimGray;
			this.sortGridTfexSumm.Size = new Size(846, 22);
			this.sortGridTfexSumm.SortColumnName = "";
			this.sortGridTfexSumm.SortType = SortType.Desc;
			this.sortGridTfexSumm.TabIndex = 28;
			this.sortGridTfex.AllowDrop = true;
			this.sortGridTfex.BackColor = Color.FromArgb(10, 10, 10);
			this.sortGridTfex.CanBlink = true;
			this.sortGridTfex.CanDrag = false;
			this.sortGridTfex.CanGetMouseMove = false;
			columnItem23.Alignment = StringAlignment.Near;
			columnItem23.BackColor = Color.FromArgb(64, 64, 64);
			columnItem23.ColumnAlignment = StringAlignment.Center;
			columnItem23.FontColor = Color.LightGray;
			columnItem23.MyStyle = FontStyle.Regular;
			columnItem23.Name = "series";
			columnItem23.Text = "Series";
			columnItem23.ValueFormat = FormatType.Text;
			columnItem23.Visible = true;
			columnItem23.Width = 11;
			columnItem24.Alignment = StringAlignment.Center;
			columnItem24.BackColor = Color.FromArgb(64, 64, 64);
			columnItem24.ColumnAlignment = StringAlignment.Center;
			columnItem24.FontColor = Color.LightGray;
			columnItem24.MyStyle = FontStyle.Regular;
			columnItem24.Name = "side";
			columnItem24.Text = "Side";
			columnItem24.ValueFormat = FormatType.Text;
			columnItem24.Visible = true;
			columnItem24.Width = 6;
			columnItem25.Alignment = StringAlignment.Far;
			columnItem25.BackColor = Color.FromArgb(64, 64, 64);
			columnItem25.ColumnAlignment = StringAlignment.Center;
			columnItem25.FontColor = Color.LightGray;
			columnItem25.MyStyle = FontStyle.Regular;
			columnItem25.Name = "start_vol";
			columnItem25.Text = "Str.Vol";
			columnItem25.ValueFormat = FormatType.Text;
			columnItem25.Visible = true;
			columnItem25.Width = 7;
			columnItem26.Alignment = StringAlignment.Far;
			columnItem26.BackColor = Color.FromArgb(64, 64, 64);
			columnItem26.ColumnAlignment = StringAlignment.Center;
			columnItem26.FontColor = Color.LightGray;
			columnItem26.MyStyle = FontStyle.Regular;
			columnItem26.Name = "onhand";
			columnItem26.Text = "OnHand";
			columnItem26.ValueFormat = FormatType.Text;
			columnItem26.Visible = true;
			columnItem26.Width = 7;
			columnItem27.Alignment = StringAlignment.Far;
			columnItem27.BackColor = Color.FromArgb(64, 64, 64);
			columnItem27.ColumnAlignment = StringAlignment.Center;
			columnItem27.FontColor = Color.LightGray;
			columnItem27.MyStyle = FontStyle.Regular;
			columnItem27.Name = "sellable";
			columnItem27.Text = "Sellable";
			columnItem27.ValueFormat = FormatType.Text;
			columnItem27.Visible = true;
			columnItem27.Width = 7;
			columnItem28.Alignment = StringAlignment.Far;
			columnItem28.BackColor = Color.FromArgb(64, 64, 64);
			columnItem28.ColumnAlignment = StringAlignment.Center;
			columnItem28.FontColor = Color.LightGray;
			columnItem28.MyStyle = FontStyle.Regular;
			columnItem28.Name = "cost_avg";
			columnItem28.Text = "C.Avg";
			columnItem28.ValueFormat = FormatType.Text;
			columnItem28.Visible = false;
			columnItem28.Width = 9;
			columnItem29.Alignment = StringAlignment.Far;
			columnItem29.BackColor = Color.FromArgb(64, 64, 64);
			columnItem29.ColumnAlignment = StringAlignment.Center;
			columnItem29.FontColor = Color.LightGray;
			columnItem29.MyStyle = FontStyle.Regular;
			columnItem29.Name = "cost_settle";
			columnItem29.Text = "C.Settle";
			columnItem29.ValueFormat = FormatType.Text;
			columnItem29.Visible = true;
			columnItem29.Width = 9;
			columnItem30.Alignment = StringAlignment.Far;
			columnItem30.BackColor = Color.FromArgb(64, 64, 64);
			columnItem30.ColumnAlignment = StringAlignment.Center;
			columnItem30.FontColor = Color.LightGray;
			columnItem30.MyStyle = FontStyle.Regular;
			columnItem30.Name = "last";
			columnItem30.Text = "Last";
			columnItem30.ValueFormat = FormatType.Text;
			columnItem30.Visible = true;
			columnItem30.Width = 9;
			columnItem31.Alignment = StringAlignment.Far;
			columnItem31.BackColor = Color.FromArgb(64, 64, 64);
			columnItem31.ColumnAlignment = StringAlignment.Center;
			columnItem31.FontColor = Color.LightGray;
			columnItem31.MyStyle = FontStyle.Regular;
			columnItem31.Name = "mkt_val";
			columnItem31.Text = "MktVal";
			columnItem31.ValueFormat = FormatType.Text;
			columnItem31.Visible = true;
			columnItem31.Width = 11;
			columnItem32.Alignment = StringAlignment.Far;
			columnItem32.BackColor = Color.FromArgb(64, 64, 64);
			columnItem32.ColumnAlignment = StringAlignment.Center;
			columnItem32.FontColor = Color.LightGray;
			columnItem32.MyStyle = FontStyle.Regular;
			columnItem32.Name = "cost_val";
			columnItem32.Text = "C.Val";
			columnItem32.ValueFormat = FormatType.Text;
			columnItem32.Visible = true;
			columnItem32.Width = 11;
			columnItem33.Alignment = StringAlignment.Far;
			columnItem33.BackColor = Color.Teal;
			columnItem33.ColumnAlignment = StringAlignment.Center;
			columnItem33.FontColor = Color.LightGray;
			columnItem33.MyStyle = FontStyle.Underline;
			columnItem33.Name = "unreal_settle";
			columnItem33.Text = "Unrl(Settle)";
			columnItem33.ValueFormat = FormatType.Text;
			columnItem33.Visible = true;
			columnItem33.Width = 11;
			columnItem34.Alignment = StringAlignment.Far;
			columnItem34.BackColor = Color.Teal;
			columnItem34.ColumnAlignment = StringAlignment.Center;
			columnItem34.FontColor = Color.LightGray;
			columnItem34.MyStyle = FontStyle.Underline;
			columnItem34.Name = "unreal_cost";
			columnItem34.Text = "Unrl.Cost";
			columnItem34.ValueFormat = FormatType.Text;
			columnItem34.Visible = false;
			columnItem34.Width = 11;
			columnItem35.Alignment = StringAlignment.Far;
			columnItem35.BackColor = Color.FromArgb(64, 64, 64);
			columnItem35.ColumnAlignment = StringAlignment.Center;
			columnItem35.FontColor = Color.LightGray;
			columnItem35.MyStyle = FontStyle.Regular;
			columnItem35.Name = "realize";
			columnItem35.Text = "Realize";
			columnItem35.ValueFormat = FormatType.Text;
			columnItem35.Visible = true;
			columnItem35.Width = 11;
			this.sortGridTfex.Columns.Add(columnItem23);
			this.sortGridTfex.Columns.Add(columnItem24);
			this.sortGridTfex.Columns.Add(columnItem25);
			this.sortGridTfex.Columns.Add(columnItem26);
			this.sortGridTfex.Columns.Add(columnItem27);
			this.sortGridTfex.Columns.Add(columnItem28);
			this.sortGridTfex.Columns.Add(columnItem29);
			this.sortGridTfex.Columns.Add(columnItem30);
			this.sortGridTfex.Columns.Add(columnItem31);
			this.sortGridTfex.Columns.Add(columnItem32);
			this.sortGridTfex.Columns.Add(columnItem33);
			this.sortGridTfex.Columns.Add(columnItem34);
			this.sortGridTfex.Columns.Add(columnItem35);
			this.sortGridTfex.CurrentScroll = 0;
			this.sortGridTfex.FocusItemIndex = -1;
			this.sortGridTfex.ForeColor = Color.Black;
			this.sortGridTfex.GridColor = Color.FromArgb(45, 45, 45);
			this.sortGridTfex.HeaderPctHeight = 80f;
			this.sortGridTfex.IsAutoRepaint = true;
			this.sortGridTfex.IsDrawFullRow = false;
			this.sortGridTfex.IsDrawGrid = true;
			this.sortGridTfex.IsDrawHeader = true;
			this.sortGridTfex.IsScrollable = true;
			this.sortGridTfex.Location = new Point(3, 79);
			this.sortGridTfex.MainColumn = "";
			this.sortGridTfex.Name = "sortGridTfex";
			this.sortGridTfex.Rows = 0;
			this.sortGridTfex.RowSelectColor = Color.FromArgb(95, 158, 160);
			this.sortGridTfex.RowSelectType = 0;
			this.sortGridTfex.RowsVisible = 0;
			this.sortGridTfex.ScrollChennelColor = Color.DimGray;
			this.sortGridTfex.Size = new Size(846, 36);
			this.sortGridTfex.SortColumnName = "";
			this.sortGridTfex.SortType = SortType.Desc;
			this.sortGridTfex.TabIndex = 27;
			this.sortGridTfex.TableMouseClick += new SortGrid.TableMouseClickEventHandler(this.sortGridTfex_TableMouseClick);
			this.intzaCustBottTfex.AllowDrop = true;
			this.intzaCustBottTfex.BackColor = Color.Black;
			this.intzaCustBottTfex.CanDrag = false;
			this.intzaCustBottTfex.IsAutoRepaint = true;
			this.intzaCustBottTfex.IsDroped = false;
			itemGrid37.AdjustFontSize = -1f;
			itemGrid37.Alignment = StringAlignment.Near;
			itemGrid37.BackColor = Color.DimGray;
			itemGrid37.Changed = false;
			itemGrid37.FieldType = ItemType.TextGradient;
			itemGrid37.FontColor = Color.LightGray;
			itemGrid37.FontStyle = FontStyle.Regular;
			itemGrid37.Height = 1;
			itemGrid37.IsBlink = 0;
			itemGrid37.Name = "lbBankCol";
			itemGrid37.Text = "";
			itemGrid37.ValueFormat = FormatType.Text;
			itemGrid37.Visible = true;
			itemGrid37.Width = 25;
			itemGrid37.X = 0;
			itemGrid37.Y = 0;
			itemGrid38.AdjustFontSize = -1f;
			itemGrid38.Alignment = StringAlignment.Center;
			itemGrid38.BackColor = Color.DimGray;
			itemGrid38.Changed = false;
			itemGrid38.FieldType = ItemType.TextGradient;
			itemGrid38.FontColor = Color.LightGray;
			itemGrid38.FontStyle = FontStyle.Regular;
			itemGrid38.Height = 1;
			itemGrid38.IsBlink = 0;
			itemGrid38.Name = "lbPrevious";
			itemGrid38.Text = "Previous";
			itemGrid38.ValueFormat = FormatType.Text;
			itemGrid38.Visible = true;
			itemGrid38.Width = 25;
			itemGrid38.X = 25;
			itemGrid38.Y = 0;
			itemGrid39.AdjustFontSize = -1f;
			itemGrid39.Alignment = StringAlignment.Center;
			itemGrid39.BackColor = Color.DimGray;
			itemGrid39.Changed = false;
			itemGrid39.FieldType = ItemType.TextGradient;
			itemGrid39.FontColor = Color.LightGray;
			itemGrid39.FontStyle = FontStyle.Regular;
			itemGrid39.Height = 1;
			itemGrid39.IsBlink = 0;
			itemGrid39.Name = "lbCurrent";
			itemGrid39.Text = "Current (Expected)";
			itemGrid39.ValueFormat = FormatType.Text;
			itemGrid39.Visible = true;
			itemGrid39.Width = 25;
			itemGrid39.X = 50;
			itemGrid39.Y = 0;
			itemGrid40.AdjustFontSize = -1f;
			itemGrid40.Alignment = StringAlignment.Center;
			itemGrid40.BackColor = Color.DimGray;
			itemGrid40.Changed = false;
			itemGrid40.FieldType = ItemType.TextGradient;
			itemGrid40.FontColor = Color.LightGray;
			itemGrid40.FontStyle = FontStyle.Regular;
			itemGrid40.Height = 1;
			itemGrid40.IsBlink = 0;
			itemGrid40.Name = "lbCurrentPort";
			itemGrid40.Text = "Current (Port)";
			itemGrid40.ValueFormat = FormatType.Text;
			itemGrid40.Visible = true;
			itemGrid40.Width = 25;
			itemGrid40.X = 75;
			itemGrid40.Y = 0;
			itemGrid41.AdjustFontSize = 0f;
			itemGrid41.Alignment = StringAlignment.Near;
			itemGrid41.BackColor = Color.Black;
			itemGrid41.Changed = false;
			itemGrid41.FieldType = ItemType.Label;
			itemGrid41.FontColor = Color.LightGray;
			itemGrid41.FontStyle = FontStyle.Regular;
			itemGrid41.Height = 1;
			itemGrid41.IsBlink = 0;
			itemGrid41.Name = "lbEquityBalance";
			itemGrid41.Text = "Equity Balance";
			itemGrid41.ValueFormat = FormatType.Text;
			itemGrid41.Visible = true;
			itemGrid41.Width = 25;
			itemGrid41.X = 0;
			itemGrid41.Y = 1;
			itemGrid42.AdjustFontSize = 0f;
			itemGrid42.Alignment = StringAlignment.Near;
			itemGrid42.BackColor = Color.Black;
			itemGrid42.Changed = false;
			itemGrid42.FieldType = ItemType.Text;
			itemGrid42.FontColor = Color.White;
			itemGrid42.FontStyle = FontStyle.Regular;
			itemGrid42.Height = 1;
			itemGrid42.IsBlink = 0;
			itemGrid42.Name = "tbEquityBalancePrevious";
			itemGrid42.Text = "";
			itemGrid42.ValueFormat = FormatType.Text;
			itemGrid42.Visible = true;
			itemGrid42.Width = 25;
			itemGrid42.X = 25;
			itemGrid42.Y = 1;
			itemGrid43.AdjustFontSize = 0f;
			itemGrid43.Alignment = StringAlignment.Near;
			itemGrid43.BackColor = Color.Black;
			itemGrid43.Changed = false;
			itemGrid43.FieldType = ItemType.Text;
			itemGrid43.FontColor = Color.White;
			itemGrid43.FontStyle = FontStyle.Regular;
			itemGrid43.Height = 1;
			itemGrid43.IsBlink = 0;
			itemGrid43.Name = "tbEquityBalanceCurrent";
			itemGrid43.Text = "";
			itemGrid43.ValueFormat = FormatType.Text;
			itemGrid43.Visible = true;
			itemGrid43.Width = 25;
			itemGrid43.X = 50;
			itemGrid43.Y = 1;
			itemGrid44.AdjustFontSize = 0f;
			itemGrid44.Alignment = StringAlignment.Near;
			itemGrid44.BackColor = Color.Black;
			itemGrid44.Changed = false;
			itemGrid44.FieldType = ItemType.Text;
			itemGrid44.FontColor = Color.White;
			itemGrid44.FontStyle = FontStyle.Regular;
			itemGrid44.Height = 1;
			itemGrid44.IsBlink = 0;
			itemGrid44.Name = "tbEquityBalanceCurrentPort";
			itemGrid44.Text = "";
			itemGrid44.ValueFormat = FormatType.Text;
			itemGrid44.Visible = true;
			itemGrid44.Width = 25;
			itemGrid44.X = 75;
			itemGrid44.Y = 1;
			itemGrid45.AdjustFontSize = 0f;
			itemGrid45.Alignment = StringAlignment.Near;
			itemGrid45.BackColor = Color.Black;
			itemGrid45.Changed = false;
			itemGrid45.FieldType = ItemType.Label;
			itemGrid45.FontColor = Color.LightGray;
			itemGrid45.FontStyle = FontStyle.Regular;
			itemGrid45.Height = 1;
			itemGrid45.IsBlink = 0;
			itemGrid45.Name = "lbEEBalance";
			itemGrid45.Text = "Excess Equity Balance";
			itemGrid45.ValueFormat = FormatType.Text;
			itemGrid45.Visible = true;
			itemGrid45.Width = 25;
			itemGrid45.X = 0;
			itemGrid45.Y = 2;
			itemGrid46.AdjustFontSize = 0f;
			itemGrid46.Alignment = StringAlignment.Near;
			itemGrid46.BackColor = Color.Black;
			itemGrid46.Changed = false;
			itemGrid46.FieldType = ItemType.Text;
			itemGrid46.FontColor = Color.White;
			itemGrid46.FontStyle = FontStyle.Regular;
			itemGrid46.Height = 1;
			itemGrid46.IsBlink = 0;
			itemGrid46.Name = "tbEEBalancePrevious";
			itemGrid46.Text = "";
			itemGrid46.ValueFormat = FormatType.Text;
			itemGrid46.Visible = true;
			itemGrid46.Width = 25;
			itemGrid46.X = 25;
			itemGrid46.Y = 2;
			itemGrid47.AdjustFontSize = 0f;
			itemGrid47.Alignment = StringAlignment.Near;
			itemGrid47.BackColor = Color.Black;
			itemGrid47.Changed = false;
			itemGrid47.FieldType = ItemType.Text;
			itemGrid47.FontColor = Color.White;
			itemGrid47.FontStyle = FontStyle.Regular;
			itemGrid47.Height = 1;
			itemGrid47.IsBlink = 0;
			itemGrid47.Name = "tbEEBalanceCurrent";
			itemGrid47.Text = "";
			itemGrid47.ValueFormat = FormatType.Text;
			itemGrid47.Visible = true;
			itemGrid47.Width = 25;
			itemGrid47.X = 50;
			itemGrid47.Y = 2;
			itemGrid48.AdjustFontSize = 0f;
			itemGrid48.Alignment = StringAlignment.Near;
			itemGrid48.BackColor = Color.Black;
			itemGrid48.Changed = false;
			itemGrid48.FieldType = ItemType.Text;
			itemGrid48.FontColor = Color.White;
			itemGrid48.FontStyle = FontStyle.Regular;
			itemGrid48.Height = 1;
			itemGrid48.IsBlink = 0;
			itemGrid48.Name = "tbEEBalanceCurerntPort";
			itemGrid48.Text = "";
			itemGrid48.ValueFormat = FormatType.Text;
			itemGrid48.Visible = true;
			itemGrid48.Width = 25;
			itemGrid48.X = 75;
			itemGrid48.Y = 2;
			itemGrid49.AdjustFontSize = 0f;
			itemGrid49.Alignment = StringAlignment.Near;
			itemGrid49.BackColor = Color.Black;
			itemGrid49.Changed = false;
			itemGrid49.FieldType = ItemType.Label;
			itemGrid49.FontColor = Color.LightGray;
			itemGrid49.FontStyle = FontStyle.Regular;
			itemGrid49.Height = 1;
			itemGrid49.IsBlink = 0;
			itemGrid49.Name = "lbUnrealizedPL";
			itemGrid49.Text = "Unrealized P/L";
			itemGrid49.ValueFormat = FormatType.Text;
			itemGrid49.Visible = true;
			itemGrid49.Width = 25;
			itemGrid49.X = 0;
			itemGrid49.Y = 3;
			itemGrid50.AdjustFontSize = 0f;
			itemGrid50.Alignment = StringAlignment.Near;
			itemGrid50.BackColor = Color.Black;
			itemGrid50.Changed = false;
			itemGrid50.FieldType = ItemType.Text;
			itemGrid50.FontColor = Color.White;
			itemGrid50.FontStyle = FontStyle.Regular;
			itemGrid50.Height = 1;
			itemGrid50.IsBlink = 0;
			itemGrid50.Name = "tbUnrealizedPLPrevious";
			itemGrid50.Text = "";
			itemGrid50.ValueFormat = FormatType.Text;
			itemGrid50.Visible = true;
			itemGrid50.Width = 25;
			itemGrid50.X = 25;
			itemGrid50.Y = 3;
			itemGrid51.AdjustFontSize = 0f;
			itemGrid51.Alignment = StringAlignment.Near;
			itemGrid51.BackColor = Color.Black;
			itemGrid51.Changed = false;
			itemGrid51.FieldType = ItemType.Text;
			itemGrid51.FontColor = Color.White;
			itemGrid51.FontStyle = FontStyle.Regular;
			itemGrid51.Height = 1;
			itemGrid51.IsBlink = 0;
			itemGrid51.Name = "tbUnrealizedPLCurrent";
			itemGrid51.Text = "";
			itemGrid51.ValueFormat = FormatType.Text;
			itemGrid51.Visible = true;
			itemGrid51.Width = 25;
			itemGrid51.X = 50;
			itemGrid51.Y = 3;
			itemGrid52.AdjustFontSize = 0f;
			itemGrid52.Alignment = StringAlignment.Near;
			itemGrid52.BackColor = Color.Black;
			itemGrid52.Changed = false;
			itemGrid52.FieldType = ItemType.Text;
			itemGrid52.FontColor = Color.White;
			itemGrid52.FontStyle = FontStyle.Regular;
			itemGrid52.Height = 1;
			itemGrid52.IsBlink = 0;
			itemGrid52.Name = "tbUnrealizedPLCurrentPort";
			itemGrid52.Text = "";
			itemGrid52.ValueFormat = FormatType.Text;
			itemGrid52.Visible = true;
			itemGrid52.Width = 25;
			itemGrid52.X = 75;
			itemGrid52.Y = 3;
			itemGrid53.AdjustFontSize = 0f;
			itemGrid53.Alignment = StringAlignment.Near;
			itemGrid53.BackColor = Color.Black;
			itemGrid53.Changed = false;
			itemGrid53.FieldType = ItemType.Label;
			itemGrid53.FontColor = Color.LightGray;
			itemGrid53.FontStyle = FontStyle.Regular;
			itemGrid53.Height = 1;
			itemGrid53.IsBlink = 0;
			itemGrid53.Name = "lbMarginBalance";
			itemGrid53.Text = "Margin Balance";
			itemGrid53.ValueFormat = FormatType.Text;
			itemGrid53.Visible = true;
			itemGrid53.Width = 25;
			itemGrid53.X = 0;
			itemGrid53.Y = 4;
			itemGrid54.AdjustFontSize = 0f;
			itemGrid54.Alignment = StringAlignment.Near;
			itemGrid54.BackColor = Color.Black;
			itemGrid54.Changed = false;
			itemGrid54.FieldType = ItemType.Text;
			itemGrid54.FontColor = Color.White;
			itemGrid54.FontStyle = FontStyle.Regular;
			itemGrid54.Height = 1;
			itemGrid54.IsBlink = 0;
			itemGrid54.Name = "tbMarginBalancePrevious";
			itemGrid54.Text = "";
			itemGrid54.ValueFormat = FormatType.Text;
			itemGrid54.Visible = true;
			itemGrid54.Width = 25;
			itemGrid54.X = 25;
			itemGrid54.Y = 4;
			itemGrid55.AdjustFontSize = 0f;
			itemGrid55.Alignment = StringAlignment.Near;
			itemGrid55.BackColor = Color.Black;
			itemGrid55.Changed = false;
			itemGrid55.FieldType = ItemType.Text;
			itemGrid55.FontColor = Color.White;
			itemGrid55.FontStyle = FontStyle.Regular;
			itemGrid55.Height = 1;
			itemGrid55.IsBlink = 0;
			itemGrid55.Name = "tbMarginBalanceCurrent";
			itemGrid55.Text = "";
			itemGrid55.ValueFormat = FormatType.Price;
			itemGrid55.Visible = true;
			itemGrid55.Width = 25;
			itemGrid55.X = 50;
			itemGrid55.Y = 4;
			itemGrid56.AdjustFontSize = 0f;
			itemGrid56.Alignment = StringAlignment.Near;
			itemGrid56.BackColor = Color.Black;
			itemGrid56.Changed = false;
			itemGrid56.FieldType = ItemType.Text;
			itemGrid56.FontColor = Color.White;
			itemGrid56.FontStyle = FontStyle.Regular;
			itemGrid56.Height = 1;
			itemGrid56.IsBlink = 0;
			itemGrid56.Name = "tbMarginBalanceCurrentPort";
			itemGrid56.Text = "";
			itemGrid56.ValueFormat = FormatType.Text;
			itemGrid56.Visible = true;
			itemGrid56.Width = 25;
			itemGrid56.X = 75;
			itemGrid56.Y = 4;
			itemGrid57.AdjustFontSize = 0f;
			itemGrid57.Alignment = StringAlignment.Near;
			itemGrid57.BackColor = Color.Black;
			itemGrid57.Changed = false;
			itemGrid57.FieldType = ItemType.Label;
			itemGrid57.FontColor = Color.LightGray;
			itemGrid57.FontStyle = FontStyle.Regular;
			itemGrid57.Height = 1;
			itemGrid57.IsBlink = 0;
			itemGrid57.Name = "lbCallForce";
			itemGrid57.Text = "Call Force Flag / Amount";
			itemGrid57.ValueFormat = FormatType.Text;
			itemGrid57.Visible = true;
			itemGrid57.Width = 25;
			itemGrid57.X = 0;
			itemGrid57.Y = 5;
			itemGrid58.AdjustFontSize = 0f;
			itemGrid58.Alignment = StringAlignment.Near;
			itemGrid58.BackColor = Color.Black;
			itemGrid58.Changed = false;
			itemGrid58.FieldType = ItemType.Text;
			itemGrid58.FontColor = Color.White;
			itemGrid58.FontStyle = FontStyle.Regular;
			itemGrid58.Height = 1;
			itemGrid58.IsBlink = 0;
			itemGrid58.Name = "tbCallForcePrevious";
			itemGrid58.Text = "";
			itemGrid58.ValueFormat = FormatType.Text;
			itemGrid58.Visible = true;
			itemGrid58.Width = 25;
			itemGrid58.X = 25;
			itemGrid58.Y = 5;
			itemGrid59.AdjustFontSize = 0f;
			itemGrid59.Alignment = StringAlignment.Near;
			itemGrid59.BackColor = Color.Black;
			itemGrid59.Changed = false;
			itemGrid59.FieldType = ItemType.Text;
			itemGrid59.FontColor = Color.White;
			itemGrid59.FontStyle = FontStyle.Regular;
			itemGrid59.Height = 1;
			itemGrid59.IsBlink = 0;
			itemGrid59.Name = "tbCallForceCurrent";
			itemGrid59.Text = "";
			itemGrid59.ValueFormat = FormatType.Text;
			itemGrid59.Visible = true;
			itemGrid59.Width = 25;
			itemGrid59.X = 50;
			itemGrid59.Y = 5;
			itemGrid60.AdjustFontSize = 0f;
			itemGrid60.Alignment = StringAlignment.Near;
			itemGrid60.BackColor = Color.Black;
			itemGrid60.Changed = false;
			itemGrid60.FieldType = ItemType.Text;
			itemGrid60.FontColor = Color.White;
			itemGrid60.FontStyle = FontStyle.Regular;
			itemGrid60.Height = 1;
			itemGrid60.IsBlink = 0;
			itemGrid60.Name = "tbCallForceCurrentPort";
			itemGrid60.Text = "";
			itemGrid60.ValueFormat = FormatType.Text;
			itemGrid60.Visible = true;
			itemGrid60.Width = 25;
			itemGrid60.X = 75;
			itemGrid60.Y = 5;
			this.intzaCustBottTfex.Items.Add(itemGrid37);
			this.intzaCustBottTfex.Items.Add(itemGrid38);
			this.intzaCustBottTfex.Items.Add(itemGrid39);
			this.intzaCustBottTfex.Items.Add(itemGrid40);
			this.intzaCustBottTfex.Items.Add(itemGrid41);
			this.intzaCustBottTfex.Items.Add(itemGrid42);
			this.intzaCustBottTfex.Items.Add(itemGrid43);
			this.intzaCustBottTfex.Items.Add(itemGrid44);
			this.intzaCustBottTfex.Items.Add(itemGrid45);
			this.intzaCustBottTfex.Items.Add(itemGrid46);
			this.intzaCustBottTfex.Items.Add(itemGrid47);
			this.intzaCustBottTfex.Items.Add(itemGrid48);
			this.intzaCustBottTfex.Items.Add(itemGrid49);
			this.intzaCustBottTfex.Items.Add(itemGrid50);
			this.intzaCustBottTfex.Items.Add(itemGrid51);
			this.intzaCustBottTfex.Items.Add(itemGrid52);
			this.intzaCustBottTfex.Items.Add(itemGrid53);
			this.intzaCustBottTfex.Items.Add(itemGrid54);
			this.intzaCustBottTfex.Items.Add(itemGrid55);
			this.intzaCustBottTfex.Items.Add(itemGrid56);
			this.intzaCustBottTfex.Items.Add(itemGrid57);
			this.intzaCustBottTfex.Items.Add(itemGrid58);
			this.intzaCustBottTfex.Items.Add(itemGrid59);
			this.intzaCustBottTfex.Items.Add(itemGrid60);
			this.intzaCustBottTfex.LineColor = Color.Red;
			this.intzaCustBottTfex.Location = new Point(0, 56);
			this.intzaCustBottTfex.Margin = new Padding(1);
			this.intzaCustBottTfex.Name = "intzaCustBottTfex";
			this.intzaCustBottTfex.Size = new Size(851, 28);
			this.intzaCustBottTfex.TabIndex = 26;
			this.intzaCustBottTfex.TabStop = false;
			this.intzaCustHeadTfex.AllowDrop = true;
			this.intzaCustHeadTfex.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.intzaCustHeadTfex.BackColor = Color.Black;
			this.intzaCustHeadTfex.CanDrag = false;
			this.intzaCustHeadTfex.IsAutoRepaint = true;
			this.intzaCustHeadTfex.IsDroped = false;
			itemGrid61.AdjustFontSize = -1f;
			itemGrid61.Alignment = StringAlignment.Near;
			itemGrid61.BackColor = Color.Black;
			itemGrid61.Changed = false;
			itemGrid61.FieldType = ItemType.Label2;
			itemGrid61.FontColor = Color.LightGray;
			itemGrid61.FontStyle = FontStyle.Regular;
			itemGrid61.Height = 1;
			itemGrid61.IsBlink = 0;
			itemGrid61.Name = "lbCustName";
			itemGrid61.Text = "Name";
			itemGrid61.ValueFormat = FormatType.Text;
			itemGrid61.Visible = true;
			itemGrid61.Width = 14;
			itemGrid61.X = 0;
			itemGrid61.Y = 0;
			itemGrid62.AdjustFontSize = 0f;
			itemGrid62.Alignment = StringAlignment.Near;
			itemGrid62.BackColor = Color.Black;
			itemGrid62.Changed = false;
			itemGrid62.FieldType = ItemType.Text;
			itemGrid62.FontColor = Color.Yellow;
			itemGrid62.FontStyle = FontStyle.Regular;
			itemGrid62.Height = 1;
			itemGrid62.IsBlink = 0;
			itemGrid62.Name = "tbCustName";
			itemGrid62.Text = "";
			itemGrid62.ValueFormat = FormatType.Text;
			itemGrid62.Visible = true;
			itemGrid62.Width = 32;
			itemGrid62.X = 14;
			itemGrid62.Y = 0;
			itemGrid63.AdjustFontSize = -1f;
			itemGrid63.Alignment = StringAlignment.Near;
			itemGrid63.BackColor = Color.Black;
			itemGrid63.Changed = false;
			itemGrid63.FieldType = ItemType.Label2;
			itemGrid63.FontColor = Color.LightGray;
			itemGrid63.FontStyle = FontStyle.Regular;
			itemGrid63.Height = 1;
			itemGrid63.IsBlink = 0;
			itemGrid63.Name = "lbVipFlag";
			itemGrid63.Text = "Vip Flag";
			itemGrid63.ValueFormat = FormatType.Text;
			itemGrid63.Visible = false;
			itemGrid63.Width = 14;
			itemGrid63.X = 0;
			itemGrid63.Y = 1;
			itemGrid64.AdjustFontSize = 0f;
			itemGrid64.Alignment = StringAlignment.Near;
			itemGrid64.BackColor = Color.Black;
			itemGrid64.Changed = false;
			itemGrid64.FieldType = ItemType.Text;
			itemGrid64.FontColor = Color.Yellow;
			itemGrid64.FontStyle = FontStyle.Regular;
			itemGrid64.Height = 1;
			itemGrid64.IsBlink = 0;
			itemGrid64.Name = "tbVipFlag";
			itemGrid64.Text = "";
			itemGrid64.ValueFormat = FormatType.Text;
			itemGrid64.Visible = false;
			itemGrid64.Width = 17;
			itemGrid64.X = 14;
			itemGrid64.Y = 1;
			itemGrid65.AdjustFontSize = -1f;
			itemGrid65.Alignment = StringAlignment.Near;
			itemGrid65.BackColor = Color.Black;
			itemGrid65.Changed = false;
			itemGrid65.FieldType = ItemType.Label2;
			itemGrid65.FontColor = Color.LightGray;
			itemGrid65.FontStyle = FontStyle.Regular;
			itemGrid65.Height = 1;
			itemGrid65.IsBlink = 0;
			itemGrid65.Name = "lbAccountType";
			itemGrid65.Text = "Account Type";
			itemGrid65.ValueFormat = FormatType.Text;
			itemGrid65.Visible = true;
			itemGrid65.Width = 14;
			itemGrid65.X = 0;
			itemGrid65.Y = 1;
			itemGrid66.AdjustFontSize = 0f;
			itemGrid66.Alignment = StringAlignment.Near;
			itemGrid66.BackColor = Color.Black;
			itemGrid66.Changed = false;
			itemGrid66.FieldType = ItemType.Text;
			itemGrid66.FontColor = Color.Yellow;
			itemGrid66.FontStyle = FontStyle.Regular;
			itemGrid66.Height = 1;
			itemGrid66.IsBlink = 0;
			itemGrid66.Name = "tbAccountType";
			itemGrid66.Text = "";
			itemGrid66.ValueFormat = FormatType.Text;
			itemGrid66.Visible = true;
			itemGrid66.Width = 17;
			itemGrid66.X = 14;
			itemGrid66.Y = 1;
			itemGrid67.AdjustFontSize = -1f;
			itemGrid67.Alignment = StringAlignment.Near;
			itemGrid67.BackColor = Color.Black;
			itemGrid67.Changed = false;
			itemGrid67.FieldType = ItemType.Label2;
			itemGrid67.FontColor = Color.LightGray;
			itemGrid67.FontStyle = FontStyle.Regular;
			itemGrid67.Height = 1;
			itemGrid67.IsBlink = 0;
			itemGrid67.Name = "lbCantOverCredit";
			itemGrid67.Text = "Can't over credit";
			itemGrid67.ValueFormat = FormatType.Text;
			itemGrid67.Visible = false;
			itemGrid67.Width = 14;
			itemGrid67.X = 0;
			itemGrid67.Y = 2;
			itemGrid68.AdjustFontSize = 0f;
			itemGrid68.Alignment = StringAlignment.Near;
			itemGrid68.BackColor = Color.Black;
			itemGrid68.Changed = false;
			itemGrid68.FieldType = ItemType.Text;
			itemGrid68.FontColor = Color.Yellow;
			itemGrid68.FontStyle = FontStyle.Regular;
			itemGrid68.Height = 1;
			itemGrid68.IsBlink = 0;
			itemGrid68.Name = "tbCantOverCredit";
			itemGrid68.Text = "";
			itemGrid68.ValueFormat = FormatType.Text;
			itemGrid68.Visible = false;
			itemGrid68.Width = 17;
			itemGrid68.X = 14;
			itemGrid68.Y = 2;
			itemGrid69.AdjustFontSize = -1f;
			itemGrid69.Alignment = StringAlignment.Near;
			itemGrid69.BackColor = Color.Black;
			itemGrid69.Changed = false;
			itemGrid69.FieldType = ItemType.Label2;
			itemGrid69.FontColor = Color.LightGray;
			itemGrid69.FontStyle = FontStyle.Regular;
			itemGrid69.Height = 1;
			itemGrid69.IsBlink = 0;
			itemGrid69.Name = "lbBuyLimit";
			itemGrid69.Text = "Buy Limit";
			itemGrid69.ValueFormat = FormatType.Text;
			itemGrid69.Visible = true;
			itemGrid69.Width = 14;
			itemGrid69.X = 0;
			itemGrid69.Y = 2;
			itemGrid70.AdjustFontSize = 0f;
			itemGrid70.Alignment = StringAlignment.Near;
			itemGrid70.BackColor = Color.Black;
			itemGrid70.Changed = false;
			itemGrid70.FieldType = ItemType.Text;
			itemGrid70.FontColor = Color.Yellow;
			itemGrid70.FontStyle = FontStyle.Regular;
			itemGrid70.Height = 1;
			itemGrid70.IsBlink = 0;
			itemGrid70.Name = "tbBuyLimit";
			itemGrid70.Text = "";
			itemGrid70.ValueFormat = FormatType.Text;
			itemGrid70.Visible = true;
			itemGrid70.Width = 17;
			itemGrid70.X = 14;
			itemGrid70.Y = 2;
			itemGrid71.AdjustFontSize = -1f;
			itemGrid71.Alignment = StringAlignment.Near;
			itemGrid71.BackColor = Color.Black;
			itemGrid71.Changed = false;
			itemGrid71.FieldType = ItemType.Label2;
			itemGrid71.FontColor = Color.LightGray;
			itemGrid71.FontStyle = FontStyle.Regular;
			itemGrid71.Height = 1;
			itemGrid71.IsBlink = 0;
			itemGrid71.Name = "lbCustomerType";
			itemGrid71.Text = "Customer Type";
			itemGrid71.ValueFormat = FormatType.Text;
			itemGrid71.Visible = true;
			itemGrid71.Width = 17;
			itemGrid71.X = 33;
			itemGrid71.Y = 1;
			itemGrid72.AdjustFontSize = 0f;
			itemGrid72.Alignment = StringAlignment.Near;
			itemGrid72.BackColor = Color.Black;
			itemGrid72.Changed = false;
			itemGrid72.FieldType = ItemType.Text;
			itemGrid72.FontColor = Color.Yellow;
			itemGrid72.FontStyle = FontStyle.Regular;
			itemGrid72.Height = 1;
			itemGrid72.IsBlink = 0;
			itemGrid72.Name = "tbCustomerType";
			itemGrid72.Text = "";
			itemGrid72.ValueFormat = FormatType.Text;
			itemGrid72.Visible = true;
			itemGrid72.Width = 15;
			itemGrid72.X = 50;
			itemGrid72.Y = 1;
			itemGrid73.AdjustFontSize = -1f;
			itemGrid73.Alignment = StringAlignment.Near;
			itemGrid73.BackColor = Color.Black;
			itemGrid73.Changed = false;
			itemGrid73.FieldType = ItemType.Label2;
			itemGrid73.FontColor = Color.LightGray;
			itemGrid73.FontStyle = FontStyle.Regular;
			itemGrid73.Height = 1;
			itemGrid73.IsBlink = 0;
			itemGrid73.Name = "lbCashBalance";
			itemGrid73.Text = "Prev Cash Balance";
			itemGrid73.ValueFormat = FormatType.Text;
			itemGrid73.Visible = true;
			itemGrid73.Width = 14;
			itemGrid73.X = 0;
			itemGrid73.Y = 3;
			itemGrid74.AdjustFontSize = 0f;
			itemGrid74.Alignment = StringAlignment.Near;
			itemGrid74.BackColor = Color.Black;
			itemGrid74.Changed = false;
			itemGrid74.FieldType = ItemType.Text;
			itemGrid74.FontColor = Color.Yellow;
			itemGrid74.FontStyle = FontStyle.Regular;
			itemGrid74.Height = 1;
			itemGrid74.IsBlink = 0;
			itemGrid74.Name = "CashBalancePrev";
			itemGrid74.Text = "";
			itemGrid74.ValueFormat = FormatType.Text;
			itemGrid74.Visible = true;
			itemGrid74.Width = 17;
			itemGrid74.X = 14;
			itemGrid74.Y = 3;
			itemGrid75.AdjustFontSize = -1f;
			itemGrid75.Alignment = StringAlignment.Near;
			itemGrid75.BackColor = Color.Black;
			itemGrid75.Changed = false;
			itemGrid75.FieldType = ItemType.Label2;
			itemGrid75.FontColor = Color.LightGray;
			itemGrid75.FontStyle = FontStyle.Regular;
			itemGrid75.Height = 1;
			itemGrid75.IsBlink = 0;
			itemGrid75.Name = "lbCustomerFlag";
			itemGrid75.Text = "Customer Flag";
			itemGrid75.ValueFormat = FormatType.Text;
			itemGrid75.Visible = true;
			itemGrid75.Width = 15;
			itemGrid75.X = 67;
			itemGrid75.Y = 1;
			itemGrid76.AdjustFontSize = 0f;
			itemGrid76.Alignment = StringAlignment.Near;
			itemGrid76.BackColor = Color.Black;
			itemGrid76.Changed = false;
			itemGrid76.FieldType = ItemType.Text;
			itemGrid76.FontColor = Color.Yellow;
			itemGrid76.FontStyle = FontStyle.Regular;
			itemGrid76.Height = 1;
			itemGrid76.IsBlink = 0;
			itemGrid76.Name = "tbCustomerFlag";
			itemGrid76.Text = "";
			itemGrid76.ValueFormat = FormatType.Text;
			itemGrid76.Visible = true;
			itemGrid76.Width = 18;
			itemGrid76.X = 82;
			itemGrid76.Y = 1;
			itemGrid77.AdjustFontSize = -1f;
			itemGrid77.Alignment = StringAlignment.Near;
			itemGrid77.BackColor = Color.Black;
			itemGrid77.Changed = false;
			itemGrid77.FieldType = ItemType.Label2;
			itemGrid77.FontColor = Color.LightGray;
			itemGrid77.FontStyle = FontStyle.Regular;
			itemGrid77.Height = 1;
			itemGrid77.IsBlink = 0;
			itemGrid77.Name = "lbCreditLine";
			itemGrid77.Text = "Credit Line";
			itemGrid77.ValueFormat = FormatType.Text;
			itemGrid77.Visible = true;
			itemGrid77.Width = 15;
			itemGrid77.X = 67;
			itemGrid77.Y = 2;
			itemGrid78.AdjustFontSize = 0f;
			itemGrid78.Alignment = StringAlignment.Near;
			itemGrid78.BackColor = Color.Black;
			itemGrid78.Changed = false;
			itemGrid78.FieldType = ItemType.Text;
			itemGrid78.FontColor = Color.Yellow;
			itemGrid78.FontStyle = FontStyle.Regular;
			itemGrid78.Height = 1;
			itemGrid78.IsBlink = 0;
			itemGrid78.Name = "tbCreditLine";
			itemGrid78.Text = "";
			itemGrid78.ValueFormat = FormatType.Text;
			itemGrid78.Visible = true;
			itemGrid78.Width = 18;
			itemGrid78.X = 82;
			itemGrid78.Y = 2;
			itemGrid79.AdjustFontSize = -1f;
			itemGrid79.Alignment = StringAlignment.Near;
			itemGrid79.BackColor = Color.Black;
			itemGrid79.Changed = false;
			itemGrid79.FieldType = ItemType.Label2;
			itemGrid79.FontColor = Color.LightGray;
			itemGrid79.FontStyle = FontStyle.Regular;
			itemGrid79.Height = 1;
			itemGrid79.IsBlink = 0;
			itemGrid79.Name = "lbDepositWithdraw";
			itemGrid79.Text = "Deposit/Withdraw";
			itemGrid79.ValueFormat = FormatType.Text;
			itemGrid79.Visible = true;
			itemGrid79.Width = 17;
			itemGrid79.X = 33;
			itemGrid79.Y = 2;
			itemGrid80.AdjustFontSize = 0f;
			itemGrid80.Alignment = StringAlignment.Near;
			itemGrid80.BackColor = Color.Black;
			itemGrid80.Changed = false;
			itemGrid80.FieldType = ItemType.Text;
			itemGrid80.FontColor = Color.Yellow;
			itemGrid80.FontStyle = FontStyle.Regular;
			itemGrid80.Height = 1;
			itemGrid80.IsBlink = 0;
			itemGrid80.Name = "tbDepositWithdraw";
			itemGrid80.Text = "";
			itemGrid80.ValueFormat = FormatType.Text;
			itemGrid80.Visible = true;
			itemGrid80.Width = 15;
			itemGrid80.X = 50;
			itemGrid80.Y = 2;
			itemGrid81.AdjustFontSize = -1f;
			itemGrid81.Alignment = StringAlignment.Near;
			itemGrid81.BackColor = Color.Black;
			itemGrid81.Changed = false;
			itemGrid81.FieldType = ItemType.Label2;
			itemGrid81.FontColor = Color.LightGray;
			itemGrid81.FontStyle = FontStyle.Regular;
			itemGrid81.Height = 1;
			itemGrid81.IsBlink = 0;
			itemGrid81.Name = "lbTrader";
			itemGrid81.Text = "Trader";
			itemGrid81.ValueFormat = FormatType.Text;
			itemGrid81.Visible = true;
			itemGrid81.Width = 6;
			itemGrid81.X = 46;
			itemGrid81.Y = 0;
			itemGrid82.AdjustFontSize = 0f;
			itemGrid82.Alignment = StringAlignment.Near;
			itemGrid82.BackColor = Color.Black;
			itemGrid82.Changed = false;
			itemGrid82.FieldType = ItemType.Text;
			itemGrid82.FontColor = Color.Yellow;
			itemGrid82.FontStyle = FontStyle.Regular;
			itemGrid82.Height = 1;
			itemGrid82.IsBlink = 0;
			itemGrid82.Name = "tbTrader";
			itemGrid82.Text = "";
			itemGrid82.ValueFormat = FormatType.Text;
			itemGrid82.Visible = true;
			itemGrid82.Width = 35;
			itemGrid82.X = 52;
			itemGrid82.Y = 0;
			itemGrid83.AdjustFontSize = -1f;
			itemGrid83.Alignment = StringAlignment.Near;
			itemGrid83.BackColor = Color.Black;
			itemGrid83.Changed = false;
			itemGrid83.FieldType = ItemType.Label2;
			itemGrid83.FontColor = Color.LightGray;
			itemGrid83.FontStyle = FontStyle.Regular;
			itemGrid83.Height = 1;
			itemGrid83.IsBlink = 0;
			itemGrid83.Name = "lbCashBalance";
			itemGrid83.Text = "Cash Balance";
			itemGrid83.ValueFormat = FormatType.Text;
			itemGrid83.Visible = true;
			itemGrid83.Width = 17;
			itemGrid83.X = 33;
			itemGrid83.Y = 3;
			itemGrid84.AdjustFontSize = 0f;
			itemGrid84.Alignment = StringAlignment.Near;
			itemGrid84.BackColor = Color.Black;
			itemGrid84.Changed = false;
			itemGrid84.FieldType = ItemType.Text;
			itemGrid84.FontColor = Color.Yellow;
			itemGrid84.FontStyle = FontStyle.Regular;
			itemGrid84.Height = 1;
			itemGrid84.IsBlink = 0;
			itemGrid84.Name = "tbCashBalance";
			itemGrid84.Text = "";
			itemGrid84.ValueFormat = FormatType.Text;
			itemGrid84.Visible = true;
			itemGrid84.Width = 15;
			itemGrid84.X = 50;
			itemGrid84.Y = 3;
			itemGrid85.AdjustFontSize = -1f;
			itemGrid85.Alignment = StringAlignment.Near;
			itemGrid85.BackColor = Color.Black;
			itemGrid85.Changed = false;
			itemGrid85.FieldType = ItemType.Label2;
			itemGrid85.FontColor = Color.LightGray;
			itemGrid85.FontStyle = FontStyle.Regular;
			itemGrid85.Height = 1;
			itemGrid85.IsBlink = 0;
			itemGrid85.Name = "lbMMR";
			itemGrid85.Text = "MMR";
			itemGrid85.ValueFormat = FormatType.Text;
			itemGrid85.Visible = false;
			itemGrid85.Width = 17;
			itemGrid85.X = 33;
			itemGrid85.Y = 3;
			itemGrid86.AdjustFontSize = 0f;
			itemGrid86.Alignment = StringAlignment.Near;
			itemGrid86.BackColor = Color.Black;
			itemGrid86.Changed = false;
			itemGrid86.FieldType = ItemType.Text;
			itemGrid86.FontColor = Color.Yellow;
			itemGrid86.FontStyle = FontStyle.Regular;
			itemGrid86.Height = 1;
			itemGrid86.IsBlink = 0;
			itemGrid86.Name = "tbMMR";
			itemGrid86.Text = "";
			itemGrid86.ValueFormat = FormatType.Text;
			itemGrid86.Visible = false;
			itemGrid86.Width = 15;
			itemGrid86.X = 50;
			itemGrid86.Y = 3;
			itemGrid87.AdjustFontSize = -1f;
			itemGrid87.Alignment = StringAlignment.Near;
			itemGrid87.BackColor = Color.Black;
			itemGrid87.Changed = false;
			itemGrid87.FieldType = ItemType.Label2;
			itemGrid87.FontColor = Color.LightGray;
			itemGrid87.FontStyle = FontStyle.Regular;
			itemGrid87.Height = 1;
			itemGrid87.IsBlink = 0;
			itemGrid87.Name = "lbCommvat";
			itemGrid87.Text = "Comm+Vat";
			itemGrid87.ValueFormat = FormatType.Text;
			itemGrid87.Visible = true;
			itemGrid87.Width = 15;
			itemGrid87.X = 67;
			itemGrid87.Y = 3;
			itemGrid88.AdjustFontSize = 0f;
			itemGrid88.Alignment = StringAlignment.Near;
			itemGrid88.BackColor = Color.Black;
			itemGrid88.Changed = false;
			itemGrid88.FieldType = ItemType.Text;
			itemGrid88.FontColor = Color.Yellow;
			itemGrid88.FontStyle = FontStyle.Regular;
			itemGrid88.Height = 1;
			itemGrid88.IsBlink = 0;
			itemGrid88.Name = "tbCommvat";
			itemGrid88.Text = "";
			itemGrid88.ValueFormat = FormatType.Text;
			itemGrid88.Visible = true;
			itemGrid88.Width = 18;
			itemGrid88.X = 82;
			itemGrid88.Y = 3;
			this.intzaCustHeadTfex.Items.Add(itemGrid61);
			this.intzaCustHeadTfex.Items.Add(itemGrid62);
			this.intzaCustHeadTfex.Items.Add(itemGrid63);
			this.intzaCustHeadTfex.Items.Add(itemGrid64);
			this.intzaCustHeadTfex.Items.Add(itemGrid65);
			this.intzaCustHeadTfex.Items.Add(itemGrid66);
			this.intzaCustHeadTfex.Items.Add(itemGrid67);
			this.intzaCustHeadTfex.Items.Add(itemGrid68);
			this.intzaCustHeadTfex.Items.Add(itemGrid69);
			this.intzaCustHeadTfex.Items.Add(itemGrid70);
			this.intzaCustHeadTfex.Items.Add(itemGrid71);
			this.intzaCustHeadTfex.Items.Add(itemGrid72);
			this.intzaCustHeadTfex.Items.Add(itemGrid73);
			this.intzaCustHeadTfex.Items.Add(itemGrid74);
			this.intzaCustHeadTfex.Items.Add(itemGrid75);
			this.intzaCustHeadTfex.Items.Add(itemGrid76);
			this.intzaCustHeadTfex.Items.Add(itemGrid77);
			this.intzaCustHeadTfex.Items.Add(itemGrid78);
			this.intzaCustHeadTfex.Items.Add(itemGrid79);
			this.intzaCustHeadTfex.Items.Add(itemGrid80);
			this.intzaCustHeadTfex.Items.Add(itemGrid81);
			this.intzaCustHeadTfex.Items.Add(itemGrid82);
			this.intzaCustHeadTfex.Items.Add(itemGrid83);
			this.intzaCustHeadTfex.Items.Add(itemGrid84);
			this.intzaCustHeadTfex.Items.Add(itemGrid85);
			this.intzaCustHeadTfex.Items.Add(itemGrid86);
			this.intzaCustHeadTfex.Items.Add(itemGrid87);
			this.intzaCustHeadTfex.Items.Add(itemGrid88);
			this.intzaCustHeadTfex.LineColor = Color.Red;
			this.intzaCustHeadTfex.Location = new Point(0, 17);
			this.intzaCustHeadTfex.Margin = new Padding(0);
			this.intzaCustHeadTfex.Name = "intzaCustHeadTfex";
			this.intzaCustHeadTfex.Size = new Size(852, 68);
			this.intzaCustHeadTfex.TabIndex = 26;
			this.intzaCustHeadTfex.TabStop = false;
			this.intzaCB_Freewill.AllowDrop = true;
			this.intzaCB_Freewill.BackColor = Color.Black;
			this.intzaCB_Freewill.CanDrag = false;
			this.intzaCB_Freewill.IsAutoRepaint = true;
			this.intzaCB_Freewill.IsDroped = false;
			itemGrid89.AdjustFontSize = 0f;
			itemGrid89.Alignment = StringAlignment.Near;
			itemGrid89.BackColor = Color.Black;
			itemGrid89.Changed = false;
			itemGrid89.FieldType = ItemType.Label2;
			itemGrid89.FontColor = Color.LightGray;
			itemGrid89.FontStyle = FontStyle.Regular;
			itemGrid89.Height = 1;
			itemGrid89.IsBlink = 0;
			itemGrid89.Name = "lbAccEE";
			itemGrid89.Text = "Acc EE";
			itemGrid89.ValueFormat = FormatType.Text;
			itemGrid89.Visible = true;
			itemGrid89.Width = 10;
			itemGrid89.X = 0;
			itemGrid89.Y = 0;
			itemGrid90.AdjustFontSize = 0f;
			itemGrid90.Alignment = StringAlignment.Near;
			itemGrid90.BackColor = Color.Black;
			itemGrid90.Changed = false;
			itemGrid90.FieldType = ItemType.Text;
			itemGrid90.FontColor = Color.Yellow;
			itemGrid90.FontStyle = FontStyle.Regular;
			itemGrid90.Height = 1;
			itemGrid90.IsBlink = 0;
			itemGrid90.Name = "tbAccEE";
			itemGrid90.Text = "";
			itemGrid90.ValueFormat = FormatType.Text;
			itemGrid90.Visible = true;
			itemGrid90.Width = 15;
			itemGrid90.X = 10;
			itemGrid90.Y = 0;
			itemGrid91.AdjustFontSize = 0f;
			itemGrid91.Alignment = StringAlignment.Near;
			itemGrid91.BackColor = Color.Black;
			itemGrid91.Changed = false;
			itemGrid91.FieldType = ItemType.Label2;
			itemGrid91.FontColor = Color.LightGray;
			itemGrid91.FontStyle = FontStyle.Regular;
			itemGrid91.Height = 1;
			itemGrid91.IsBlink = 0;
			itemGrid91.Name = "lbBuyCredit50";
			itemGrid91.Text = "BCrd 50%";
			itemGrid91.ValueFormat = FormatType.Text;
			itemGrid91.Visible = true;
			itemGrid91.Width = 10;
			itemGrid91.X = 25;
			itemGrid91.Y = 0;
			itemGrid92.AdjustFontSize = 0f;
			itemGrid92.Alignment = StringAlignment.Near;
			itemGrid92.BackColor = Color.Black;
			itemGrid92.Changed = false;
			itemGrid92.FieldType = ItemType.Text;
			itemGrid92.FontColor = Color.Yellow;
			itemGrid92.FontStyle = FontStyle.Regular;
			itemGrid92.Height = 1;
			itemGrid92.IsBlink = 0;
			itemGrid92.Name = "tbBuyCredit50";
			itemGrid92.Text = "";
			itemGrid92.ValueFormat = FormatType.Text;
			itemGrid92.Visible = true;
			itemGrid92.Width = 15;
			itemGrid92.X = 35;
			itemGrid92.Y = 0;
			itemGrid93.AdjustFontSize = 0f;
			itemGrid93.Alignment = StringAlignment.Near;
			itemGrid93.BackColor = Color.Black;
			itemGrid93.Changed = false;
			itemGrid93.FieldType = ItemType.Label2;
			itemGrid93.FontColor = Color.LightGray;
			itemGrid93.FontStyle = FontStyle.Regular;
			itemGrid93.Height = 1;
			itemGrid93.IsBlink = 0;
			itemGrid93.Name = "lbBuyCredit60";
			itemGrid93.Text = "BCrd 60%";
			itemGrid93.ValueFormat = FormatType.Text;
			itemGrid93.Visible = true;
			itemGrid93.Width = 10;
			itemGrid93.X = 50;
			itemGrid93.Y = 0;
			itemGrid94.AdjustFontSize = 0f;
			itemGrid94.Alignment = StringAlignment.Near;
			itemGrid94.BackColor = Color.Black;
			itemGrid94.Changed = false;
			itemGrid94.FieldType = ItemType.Text;
			itemGrid94.FontColor = Color.Yellow;
			itemGrid94.FontStyle = FontStyle.Regular;
			itemGrid94.Height = 1;
			itemGrid94.IsBlink = 0;
			itemGrid94.Name = "tbBuyCredit60";
			itemGrid94.Text = "";
			itemGrid94.ValueFormat = FormatType.Text;
			itemGrid94.Visible = true;
			itemGrid94.Width = 15;
			itemGrid94.X = 60;
			itemGrid94.Y = 0;
			itemGrid95.AdjustFontSize = 0f;
			itemGrid95.Alignment = StringAlignment.Near;
			itemGrid95.BackColor = Color.Black;
			itemGrid95.Changed = false;
			itemGrid95.FieldType = ItemType.Label2;
			itemGrid95.FontColor = Color.LightGray;
			itemGrid95.FontStyle = FontStyle.Regular;
			itemGrid95.Height = 1;
			itemGrid95.IsBlink = 0;
			itemGrid95.Name = "lbBuyCredit70";
			itemGrid95.Text = "BCrd 70%";
			itemGrid95.ValueFormat = FormatType.Text;
			itemGrid95.Visible = true;
			itemGrid95.Width = 10;
			itemGrid95.X = 75;
			itemGrid95.Y = 0;
			itemGrid96.AdjustFontSize = 0f;
			itemGrid96.Alignment = StringAlignment.Near;
			itemGrid96.BackColor = Color.Black;
			itemGrid96.Changed = false;
			itemGrid96.FieldType = ItemType.Text;
			itemGrid96.FontColor = Color.Yellow;
			itemGrid96.FontStyle = FontStyle.Regular;
			itemGrid96.Height = 1;
			itemGrid96.IsBlink = 0;
			itemGrid96.Name = "tbBuyCredit70";
			itemGrid96.Text = "";
			itemGrid96.ValueFormat = FormatType.Text;
			itemGrid96.Visible = true;
			itemGrid96.Width = 15;
			itemGrid96.X = 85;
			itemGrid96.Y = 0;
			itemGrid97.AdjustFontSize = 0f;
			itemGrid97.Alignment = StringAlignment.Near;
			itemGrid97.BackColor = Color.Black;
			itemGrid97.Changed = false;
			itemGrid97.FieldType = ItemType.Label2;
			itemGrid97.FontColor = Color.LightGray;
			itemGrid97.FontStyle = FontStyle.Regular;
			itemGrid97.Height = 1;
			itemGrid97.IsBlink = 0;
			itemGrid97.Name = "lbAssets";
			itemGrid97.Text = "Assets";
			itemGrid97.ValueFormat = FormatType.Text;
			itemGrid97.Visible = true;
			itemGrid97.Width = 10;
			itemGrid97.X = 0;
			itemGrid97.Y = 1;
			itemGrid98.AdjustFontSize = 0f;
			itemGrid98.Alignment = StringAlignment.Near;
			itemGrid98.BackColor = Color.Black;
			itemGrid98.Changed = false;
			itemGrid98.FieldType = ItemType.Text;
			itemGrid98.FontColor = Color.Lime;
			itemGrid98.FontStyle = FontStyle.Regular;
			itemGrid98.Height = 1;
			itemGrid98.IsBlink = 0;
			itemGrid98.Name = "tbAssets";
			itemGrid98.Text = "";
			itemGrid98.ValueFormat = FormatType.Text;
			itemGrid98.Visible = true;
			itemGrid98.Width = 15;
			itemGrid98.X = 10;
			itemGrid98.Y = 1;
			itemGrid99.AdjustFontSize = 0f;
			itemGrid99.Alignment = StringAlignment.Near;
			itemGrid99.BackColor = Color.Black;
			itemGrid99.Changed = false;
			itemGrid99.FieldType = ItemType.Label2;
			itemGrid99.FontColor = Color.LightGray;
			itemGrid99.FontStyle = FontStyle.Regular;
			itemGrid99.Height = 1;
			itemGrid99.IsBlink = 0;
			itemGrid99.Name = "lbMR";
			itemGrid99.Text = "MR";
			itemGrid99.ValueFormat = FormatType.Text;
			itemGrid99.Visible = true;
			itemGrid99.Width = 10;
			itemGrid99.X = 25;
			itemGrid99.Y = 1;
			itemGrid100.AdjustFontSize = 0f;
			itemGrid100.Alignment = StringAlignment.Near;
			itemGrid100.BackColor = Color.Black;
			itemGrid100.Changed = false;
			itemGrid100.FieldType = ItemType.Text;
			itemGrid100.FontColor = Color.Yellow;
			itemGrid100.FontStyle = FontStyle.Regular;
			itemGrid100.Height = 1;
			itemGrid100.IsBlink = 0;
			itemGrid100.Name = "tbMR";
			itemGrid100.Text = "";
			itemGrid100.ValueFormat = FormatType.Text;
			itemGrid100.Visible = true;
			itemGrid100.Width = 15;
			itemGrid100.X = 35;
			itemGrid100.Y = 1;
			itemGrid101.AdjustFontSize = 0f;
			itemGrid101.Alignment = StringAlignment.Near;
			itemGrid101.BackColor = Color.Black;
			itemGrid101.Changed = false;
			itemGrid101.FieldType = ItemType.Label2;
			itemGrid101.FontColor = Color.LightGray;
			itemGrid101.FontStyle = FontStyle.Regular;
			itemGrid101.Height = 1;
			itemGrid101.IsBlink = 0;
			itemGrid101.Name = "lbCallForce";
			itemGrid101.Text = "Call Force";
			itemGrid101.ValueFormat = FormatType.Text;
			itemGrid101.Visible = true;
			itemGrid101.Width = 10;
			itemGrid101.X = 50;
			itemGrid101.Y = 1;
			itemGrid102.AdjustFontSize = 0f;
			itemGrid102.Alignment = StringAlignment.Near;
			itemGrid102.BackColor = Color.Black;
			itemGrid102.Changed = false;
			itemGrid102.FieldType = ItemType.Text;
			itemGrid102.FontColor = Color.Yellow;
			itemGrid102.FontStyle = FontStyle.Regular;
			itemGrid102.Height = 1;
			itemGrid102.IsBlink = 0;
			itemGrid102.Name = "tbCallForce";
			itemGrid102.Text = "";
			itemGrid102.ValueFormat = FormatType.Text;
			itemGrid102.Visible = true;
			itemGrid102.Width = 15;
			itemGrid102.X = 60;
			itemGrid102.Y = 1;
			itemGrid103.AdjustFontSize = 0f;
			itemGrid103.Alignment = StringAlignment.Near;
			itemGrid103.BackColor = Color.Black;
			itemGrid103.Changed = false;
			itemGrid103.FieldType = ItemType.Label2;
			itemGrid103.FontColor = Color.LightGray;
			itemGrid103.FontStyle = FontStyle.Regular;
			itemGrid103.Height = 1;
			itemGrid103.IsBlink = 0;
			itemGrid103.Name = "lbShortForce";
			itemGrid103.Text = "Shortage Force";
			itemGrid103.ValueFormat = FormatType.Text;
			itemGrid103.Visible = true;
			itemGrid103.Width = 10;
			itemGrid103.X = 75;
			itemGrid103.Y = 1;
			itemGrid104.AdjustFontSize = 0f;
			itemGrid104.Alignment = StringAlignment.Near;
			itemGrid104.BackColor = Color.Black;
			itemGrid104.Changed = false;
			itemGrid104.FieldType = ItemType.Text;
			itemGrid104.FontColor = Color.Yellow;
			itemGrid104.FontStyle = FontStyle.Regular;
			itemGrid104.Height = 1;
			itemGrid104.IsBlink = 0;
			itemGrid104.Name = "tbShortForce";
			itemGrid104.Text = "";
			itemGrid104.ValueFormat = FormatType.Text;
			itemGrid104.Visible = true;
			itemGrid104.Width = 15;
			itemGrid104.X = 85;
			itemGrid104.Y = 1;
			itemGrid105.AdjustFontSize = 0f;
			itemGrid105.Alignment = StringAlignment.Near;
			itemGrid105.BackColor = Color.Black;
			itemGrid105.Changed = false;
			itemGrid105.FieldType = ItemType.Label2;
			itemGrid105.FontColor = Color.LightGray;
			itemGrid105.FontStyle = FontStyle.Regular;
			itemGrid105.Height = 1;
			itemGrid105.IsBlink = 0;
			itemGrid105.Name = "lbLiabilities";
			itemGrid105.Text = "Liabilities";
			itemGrid105.ValueFormat = FormatType.Text;
			itemGrid105.Visible = true;
			itemGrid105.Width = 10;
			itemGrid105.X = 0;
			itemGrid105.Y = 2;
			itemGrid106.AdjustFontSize = 0f;
			itemGrid106.Alignment = StringAlignment.Near;
			itemGrid106.BackColor = Color.Black;
			itemGrid106.Changed = false;
			itemGrid106.FieldType = ItemType.Text;
			itemGrid106.FontColor = Color.Red;
			itemGrid106.FontStyle = FontStyle.Regular;
			itemGrid106.Height = 1;
			itemGrid106.IsBlink = 0;
			itemGrid106.Name = "tbLiabilities";
			itemGrid106.Text = "";
			itemGrid106.ValueFormat = FormatType.Text;
			itemGrid106.Visible = true;
			itemGrid106.Width = 15;
			itemGrid106.X = 10;
			itemGrid106.Y = 2;
			itemGrid107.AdjustFontSize = 0f;
			itemGrid107.Alignment = StringAlignment.Near;
			itemGrid107.BackColor = Color.Black;
			itemGrid107.Changed = false;
			itemGrid107.FieldType = ItemType.Label2;
			itemGrid107.FontColor = Color.LightGray;
			itemGrid107.FontStyle = FontStyle.Regular;
			itemGrid107.Height = 1;
			itemGrid107.IsBlink = 0;
			itemGrid107.Name = "lbEquity";
			itemGrid107.Text = "Equity";
			itemGrid107.ValueFormat = FormatType.Text;
			itemGrid107.Visible = true;
			itemGrid107.Width = 10;
			itemGrid107.X = 25;
			itemGrid107.Y = 2;
			itemGrid108.AdjustFontSize = 0f;
			itemGrid108.Alignment = StringAlignment.Near;
			itemGrid108.BackColor = Color.Black;
			itemGrid108.Changed = false;
			itemGrid108.FieldType = ItemType.Text;
			itemGrid108.FontColor = Color.Yellow;
			itemGrid108.FontStyle = FontStyle.Regular;
			itemGrid108.Height = 1;
			itemGrid108.IsBlink = 0;
			itemGrid108.Name = "tbEquity";
			itemGrid108.Text = "";
			itemGrid108.ValueFormat = FormatType.Text;
			itemGrid108.Visible = true;
			itemGrid108.Width = 15;
			itemGrid108.X = 35;
			itemGrid108.Y = 2;
			itemGrid109.AdjustFontSize = 0f;
			itemGrid109.Alignment = StringAlignment.Near;
			itemGrid109.BackColor = Color.Black;
			itemGrid109.Changed = false;
			itemGrid109.FieldType = ItemType.Label2;
			itemGrid109.FontColor = Color.LightGray;
			itemGrid109.FontStyle = FontStyle.Regular;
			itemGrid109.Height = 1;
			itemGrid109.IsBlink = 0;
			itemGrid109.Name = "lbBuyMR";
			itemGrid109.Text = "Buy MR";
			itemGrid109.ValueFormat = FormatType.Text;
			itemGrid109.Visible = true;
			itemGrid109.Width = 10;
			itemGrid109.X = 50;
			itemGrid109.Y = 2;
			itemGrid110.AdjustFontSize = 0f;
			itemGrid110.Alignment = StringAlignment.Near;
			itemGrid110.BackColor = Color.Black;
			itemGrid110.Changed = false;
			itemGrid110.FieldType = ItemType.Text;
			itemGrid110.FontColor = Color.Yellow;
			itemGrid110.FontStyle = FontStyle.Regular;
			itemGrid110.Height = 1;
			itemGrid110.IsBlink = 0;
			itemGrid110.Name = "tbBuyMR";
			itemGrid110.Text = "";
			itemGrid110.ValueFormat = FormatType.Text;
			itemGrid110.Visible = true;
			itemGrid110.Width = 15;
			itemGrid110.X = 60;
			itemGrid110.Y = 2;
			itemGrid111.AdjustFontSize = 0f;
			itemGrid111.Alignment = StringAlignment.Near;
			itemGrid111.BackColor = Color.Black;
			itemGrid111.Changed = false;
			itemGrid111.FieldType = ItemType.Label2;
			itemGrid111.FontColor = Color.LightGray;
			itemGrid111.FontStyle = FontStyle.Regular;
			itemGrid111.Height = 1;
			itemGrid111.IsBlink = 0;
			itemGrid111.Name = "lbSellMR";
			itemGrid111.Text = "Sell MR";
			itemGrid111.ValueFormat = FormatType.Text;
			itemGrid111.Visible = true;
			itemGrid111.Width = 10;
			itemGrid111.X = 75;
			itemGrid111.Y = 2;
			itemGrid112.AdjustFontSize = 0f;
			itemGrid112.Alignment = StringAlignment.Near;
			itemGrid112.BackColor = Color.Black;
			itemGrid112.Changed = false;
			itemGrid112.FieldType = ItemType.Text;
			itemGrid112.FontColor = Color.Yellow;
			itemGrid112.FontStyle = FontStyle.Regular;
			itemGrid112.Height = 1;
			itemGrid112.IsBlink = 0;
			itemGrid112.Name = "tbSellMR";
			itemGrid112.Text = "";
			itemGrid112.ValueFormat = FormatType.Text;
			itemGrid112.Visible = true;
			itemGrid112.Width = 15;
			itemGrid112.X = 85;
			itemGrid112.Y = 2;
			itemGrid113.AdjustFontSize = 0f;
			itemGrid113.Alignment = StringAlignment.Near;
			itemGrid113.BackColor = Color.Black;
			itemGrid113.Changed = false;
			itemGrid113.FieldType = ItemType.Label2;
			itemGrid113.FontColor = Color.LightGray;
			itemGrid113.FontStyle = FontStyle.Regular;
			itemGrid113.Height = 1;
			itemGrid113.IsBlink = 0;
			itemGrid113.Name = "lbCashBal";
			itemGrid113.Text = "Cash Bal";
			itemGrid113.ValueFormat = FormatType.Text;
			itemGrid113.Visible = true;
			itemGrid113.Width = 10;
			itemGrid113.X = 0;
			itemGrid113.Y = 3;
			itemGrid114.AdjustFontSize = 0f;
			itemGrid114.Alignment = StringAlignment.Near;
			itemGrid114.BackColor = Color.Black;
			itemGrid114.Changed = false;
			itemGrid114.FieldType = ItemType.Text;
			itemGrid114.FontColor = Color.Lime;
			itemGrid114.FontStyle = FontStyle.Regular;
			itemGrid114.Height = 1;
			itemGrid114.IsBlink = 0;
			itemGrid114.Name = "tbCashBal";
			itemGrid114.Text = "";
			itemGrid114.ValueFormat = FormatType.Text;
			itemGrid114.Visible = true;
			itemGrid114.Width = 15;
			itemGrid114.X = 10;
			itemGrid114.Y = 3;
			itemGrid115.AdjustFontSize = 0f;
			itemGrid115.Alignment = StringAlignment.Near;
			itemGrid115.BackColor = Color.Black;
			itemGrid115.Changed = false;
			itemGrid115.FieldType = ItemType.Label2;
			itemGrid115.FontColor = Color.LightGray;
			itemGrid115.FontStyle = FontStyle.Regular;
			itemGrid115.Height = 1;
			itemGrid115.IsBlink = 0;
			itemGrid115.Name = "lbEE";
			itemGrid115.Text = "EE";
			itemGrid115.ValueFormat = FormatType.Text;
			itemGrid115.Visible = true;
			itemGrid115.Width = 10;
			itemGrid115.X = 25;
			itemGrid115.Y = 3;
			itemGrid116.AdjustFontSize = 0f;
			itemGrid116.Alignment = StringAlignment.Near;
			itemGrid116.BackColor = Color.Black;
			itemGrid116.Changed = false;
			itemGrid116.FieldType = ItemType.Text;
			itemGrid116.FontColor = Color.Yellow;
			itemGrid116.FontStyle = FontStyle.Regular;
			itemGrid116.Height = 1;
			itemGrid116.IsBlink = 0;
			itemGrid116.Name = "tbEE";
			itemGrid116.Text = "";
			itemGrid116.ValueFormat = FormatType.Text;
			itemGrid116.Visible = true;
			itemGrid116.Width = 15;
			itemGrid116.X = 35;
			itemGrid116.Y = 3;
			itemGrid117.AdjustFontSize = 0f;
			itemGrid117.Alignment = StringAlignment.Near;
			itemGrid117.BackColor = Color.Black;
			itemGrid117.Changed = false;
			itemGrid117.FieldType = ItemType.Label2;
			itemGrid117.FontColor = Color.LightGray;
			itemGrid117.FontStyle = FontStyle.Regular;
			itemGrid117.Height = 1;
			itemGrid117.IsBlink = 0;
			itemGrid117.Name = "lbPP";
			itemGrid117.Text = "PP";
			itemGrid117.ValueFormat = FormatType.Text;
			itemGrid117.Visible = true;
			itemGrid117.Width = 10;
			itemGrid117.X = 50;
			itemGrid117.Y = 3;
			itemGrid118.AdjustFontSize = 0f;
			itemGrid118.Alignment = StringAlignment.Near;
			itemGrid118.BackColor = Color.Black;
			itemGrid118.Changed = false;
			itemGrid118.FieldType = ItemType.Text;
			itemGrid118.FontColor = Color.Yellow;
			itemGrid118.FontStyle = FontStyle.Regular;
			itemGrid118.Height = 1;
			itemGrid118.IsBlink = 0;
			itemGrid118.Name = "tbPP";
			itemGrid118.Text = "";
			itemGrid118.ValueFormat = FormatType.Text;
			itemGrid118.Visible = true;
			itemGrid118.Width = 15;
			itemGrid118.X = 60;
			itemGrid118.Y = 3;
			itemGrid119.AdjustFontSize = 0f;
			itemGrid119.Alignment = StringAlignment.Near;
			itemGrid119.BackColor = Color.Black;
			itemGrid119.Changed = false;
			itemGrid119.FieldType = ItemType.Label2;
			itemGrid119.FontColor = Color.LightGray;
			itemGrid119.FontStyle = FontStyle.Regular;
			itemGrid119.Height = 1;
			itemGrid119.IsBlink = 0;
			itemGrid119.Name = "lbCallLMV";
			itemGrid119.Text = "Call LMV";
			itemGrid119.ValueFormat = FormatType.Text;
			itemGrid119.Visible = true;
			itemGrid119.Width = 10;
			itemGrid119.X = 75;
			itemGrid119.Y = 3;
			itemGrid120.AdjustFontSize = 0f;
			itemGrid120.Alignment = StringAlignment.Near;
			itemGrid120.BackColor = Color.Black;
			itemGrid120.Changed = false;
			itemGrid120.FieldType = ItemType.Text;
			itemGrid120.FontColor = Color.Yellow;
			itemGrid120.FontStyle = FontStyle.Regular;
			itemGrid120.Height = 1;
			itemGrid120.IsBlink = 0;
			itemGrid120.Name = "tbCallLMV";
			itemGrid120.Text = "";
			itemGrid120.ValueFormat = FormatType.Text;
			itemGrid120.Visible = true;
			itemGrid120.Width = 15;
			itemGrid120.X = 85;
			itemGrid120.Y = 3;
			itemGrid121.AdjustFontSize = 0f;
			itemGrid121.Alignment = StringAlignment.Near;
			itemGrid121.BackColor = Color.Black;
			itemGrid121.Changed = false;
			itemGrid121.FieldType = ItemType.Label2;
			itemGrid121.FontColor = Color.LightGray;
			itemGrid121.FontStyle = FontStyle.Regular;
			itemGrid121.Height = 1;
			itemGrid121.IsBlink = 0;
			itemGrid121.Name = "lbLMV";
			itemGrid121.Text = "LMV";
			itemGrid121.ValueFormat = FormatType.Text;
			itemGrid121.Visible = true;
			itemGrid121.Width = 10;
			itemGrid121.X = 0;
			itemGrid121.Y = 4;
			itemGrid122.AdjustFontSize = 0f;
			itemGrid122.Alignment = StringAlignment.Near;
			itemGrid122.BackColor = Color.Black;
			itemGrid122.Changed = false;
			itemGrid122.FieldType = ItemType.Text;
			itemGrid122.FontColor = Color.Lime;
			itemGrid122.FontStyle = FontStyle.Regular;
			itemGrid122.Height = 1;
			itemGrid122.IsBlink = 0;
			itemGrid122.Name = "tbLMV";
			itemGrid122.Text = "";
			itemGrid122.ValueFormat = FormatType.Text;
			itemGrid122.Visible = true;
			itemGrid122.Width = 15;
			itemGrid122.X = 10;
			itemGrid122.Y = 4;
			itemGrid123.AdjustFontSize = 0f;
			itemGrid123.Alignment = StringAlignment.Near;
			itemGrid123.BackColor = Color.Black;
			itemGrid123.Changed = false;
			itemGrid123.FieldType = ItemType.Label2;
			itemGrid123.FontColor = Color.LightGray;
			itemGrid123.FontStyle = FontStyle.Regular;
			itemGrid123.Height = 1;
			itemGrid123.IsBlink = 0;
			itemGrid123.Name = "lbCollateral";
			itemGrid123.Text = "Collateral";
			itemGrid123.ValueFormat = FormatType.Text;
			itemGrid123.Visible = true;
			itemGrid123.Width = 10;
			itemGrid123.X = 25;
			itemGrid123.Y = 4;
			itemGrid124.AdjustFontSize = 0f;
			itemGrid124.Alignment = StringAlignment.Near;
			itemGrid124.BackColor = Color.Black;
			itemGrid124.Changed = false;
			itemGrid124.FieldType = ItemType.Text;
			itemGrid124.FontColor = Color.Lime;
			itemGrid124.FontStyle = FontStyle.Regular;
			itemGrid124.Height = 1;
			itemGrid124.IsBlink = 0;
			itemGrid124.Name = "tbCollateral";
			itemGrid124.Text = "";
			itemGrid124.ValueFormat = FormatType.Text;
			itemGrid124.Visible = true;
			itemGrid124.Width = 15;
			itemGrid124.X = 35;
			itemGrid124.Y = 4;
			itemGrid125.AdjustFontSize = 0f;
			itemGrid125.Alignment = StringAlignment.Near;
			itemGrid125.BackColor = Color.Black;
			itemGrid125.Changed = false;
			itemGrid125.FieldType = ItemType.Label2;
			itemGrid125.FontColor = Color.LightGray;
			itemGrid125.FontStyle = FontStyle.Regular;
			itemGrid125.Height = 1;
			itemGrid125.IsBlink = 0;
			itemGrid125.Name = "lbCallMargin";
			itemGrid125.Text = "Call Margin";
			itemGrid125.ValueFormat = FormatType.Text;
			itemGrid125.Visible = true;
			itemGrid125.Width = 10;
			itemGrid125.X = 50;
			itemGrid125.Y = 4;
			itemGrid126.AdjustFontSize = 0f;
			itemGrid126.Alignment = StringAlignment.Near;
			itemGrid126.BackColor = Color.Black;
			itemGrid126.Changed = false;
			itemGrid126.FieldType = ItemType.Text;
			itemGrid126.FontColor = Color.Yellow;
			itemGrid126.FontStyle = FontStyle.Regular;
			itemGrid126.Height = 1;
			itemGrid126.IsBlink = 0;
			itemGrid126.Name = "tbCallMargin";
			itemGrid126.Text = "";
			itemGrid126.ValueFormat = FormatType.Text;
			itemGrid126.Visible = true;
			itemGrid126.Width = 15;
			itemGrid126.X = 60;
			itemGrid126.Y = 4;
			itemGrid127.AdjustFontSize = 0f;
			itemGrid127.Alignment = StringAlignment.Near;
			itemGrid127.BackColor = Color.Black;
			itemGrid127.Changed = false;
			itemGrid127.FieldType = ItemType.Label2;
			itemGrid127.FontColor = Color.LightGray;
			itemGrid127.FontStyle = FontStyle.Regular;
			itemGrid127.Height = 1;
			itemGrid127.IsBlink = 0;
			itemGrid127.Name = "lbCallSMV";
			itemGrid127.Text = "Call SMV";
			itemGrid127.ValueFormat = FormatType.Text;
			itemGrid127.Visible = true;
			itemGrid127.Width = 10;
			itemGrid127.X = 75;
			itemGrid127.Y = 4;
			itemGrid128.AdjustFontSize = 0f;
			itemGrid128.Alignment = StringAlignment.Near;
			itemGrid128.BackColor = Color.Black;
			itemGrid128.Changed = false;
			itemGrid128.FieldType = ItemType.Text;
			itemGrid128.FontColor = Color.Yellow;
			itemGrid128.FontStyle = FontStyle.Regular;
			itemGrid128.Height = 1;
			itemGrid128.IsBlink = 0;
			itemGrid128.Name = "tbCallSMV";
			itemGrid128.Text = "";
			itemGrid128.ValueFormat = FormatType.Text;
			itemGrid128.Visible = true;
			itemGrid128.Width = 15;
			itemGrid128.X = 85;
			itemGrid128.Y = 4;
			itemGrid129.AdjustFontSize = 0f;
			itemGrid129.Alignment = StringAlignment.Near;
			itemGrid129.BackColor = Color.Black;
			itemGrid129.Changed = false;
			itemGrid129.FieldType = ItemType.Label2;
			itemGrid129.FontColor = Color.LightGray;
			itemGrid129.FontStyle = FontStyle.Regular;
			itemGrid129.Height = 1;
			itemGrid129.IsBlink = 0;
			itemGrid129.Name = "lbSMV";
			itemGrid129.Text = "SMV";
			itemGrid129.ValueFormat = FormatType.Text;
			itemGrid129.Visible = true;
			itemGrid129.Width = 10;
			itemGrid129.X = 0;
			itemGrid129.Y = 5;
			itemGrid130.AdjustFontSize = 0f;
			itemGrid130.Alignment = StringAlignment.Near;
			itemGrid130.BackColor = Color.Black;
			itemGrid130.Changed = false;
			itemGrid130.FieldType = ItemType.Text;
			itemGrid130.FontColor = Color.Red;
			itemGrid130.FontStyle = FontStyle.Regular;
			itemGrid130.Height = 1;
			itemGrid130.IsBlink = 0;
			itemGrid130.Name = "tbSMV";
			itemGrid130.Text = "";
			itemGrid130.ValueFormat = FormatType.Text;
			itemGrid130.Visible = true;
			itemGrid130.Width = 15;
			itemGrid130.X = 10;
			itemGrid130.Y = 5;
			itemGrid131.AdjustFontSize = 0f;
			itemGrid131.Alignment = StringAlignment.Near;
			itemGrid131.BackColor = Color.Black;
			itemGrid131.Changed = false;
			itemGrid131.FieldType = ItemType.Label2;
			itemGrid131.FontColor = Color.LightGray;
			itemGrid131.FontStyle = FontStyle.Regular;
			itemGrid131.Height = 1;
			itemGrid131.IsBlink = 0;
			itemGrid131.Name = "lbDEBT";
			itemGrid131.Text = "DEBT";
			itemGrid131.ValueFormat = FormatType.Text;
			itemGrid131.Visible = true;
			itemGrid131.Width = 10;
			itemGrid131.X = 25;
			itemGrid131.Y = 5;
			itemGrid132.AdjustFontSize = 0f;
			itemGrid132.Alignment = StringAlignment.Near;
			itemGrid132.BackColor = Color.Black;
			itemGrid132.Changed = false;
			itemGrid132.FieldType = ItemType.Text;
			itemGrid132.FontColor = Color.Red;
			itemGrid132.FontStyle = FontStyle.Regular;
			itemGrid132.Height = 1;
			itemGrid132.IsBlink = 0;
			itemGrid132.Name = "tbDEBT";
			itemGrid132.Text = "";
			itemGrid132.ValueFormat = FormatType.Text;
			itemGrid132.Visible = true;
			itemGrid132.Width = 15;
			itemGrid132.X = 35;
			itemGrid132.Y = 5;
			itemGrid133.AdjustFontSize = 0f;
			itemGrid133.Alignment = StringAlignment.Near;
			itemGrid133.BackColor = Color.Black;
			itemGrid133.Changed = false;
			itemGrid133.FieldType = ItemType.Label2;
			itemGrid133.FontColor = Color.LightGray;
			itemGrid133.FontStyle = FontStyle.Regular;
			itemGrid133.Height = 1;
			itemGrid133.IsBlink = 0;
			itemGrid133.Name = "lbShortageCall";
			itemGrid133.Text = "Shortage Call";
			itemGrid133.ValueFormat = FormatType.Text;
			itemGrid133.Visible = true;
			itemGrid133.Width = 10;
			itemGrid133.X = 50;
			itemGrid133.Y = 5;
			itemGrid134.AdjustFontSize = 0f;
			itemGrid134.Alignment = StringAlignment.Near;
			itemGrid134.BackColor = Color.Black;
			itemGrid134.Changed = false;
			itemGrid134.FieldType = ItemType.Text;
			itemGrid134.FontColor = Color.Yellow;
			itemGrid134.FontStyle = FontStyle.Regular;
			itemGrid134.Height = 1;
			itemGrid134.IsBlink = 0;
			itemGrid134.Name = "tbShortageCall";
			itemGrid134.Text = "";
			itemGrid134.ValueFormat = FormatType.Text;
			itemGrid134.Visible = true;
			itemGrid134.Width = 15;
			itemGrid134.X = 60;
			itemGrid134.Y = 5;
			itemGrid135.AdjustFontSize = 0f;
			itemGrid135.Alignment = StringAlignment.Near;
			itemGrid135.BackColor = Color.Black;
			itemGrid135.Changed = false;
			itemGrid135.FieldType = ItemType.Label2;
			itemGrid135.FontColor = Color.LightGray;
			itemGrid135.FontStyle = FontStyle.Regular;
			itemGrid135.Height = 1;
			itemGrid135.IsBlink = 0;
			itemGrid135.Name = "lbForceLMV";
			itemGrid135.Text = "Force LMV";
			itemGrid135.ValueFormat = FormatType.Text;
			itemGrid135.Visible = true;
			itemGrid135.Width = 10;
			itemGrid135.X = 75;
			itemGrid135.Y = 5;
			itemGrid136.AdjustFontSize = 0f;
			itemGrid136.Alignment = StringAlignment.Near;
			itemGrid136.BackColor = Color.Black;
			itemGrid136.Changed = false;
			itemGrid136.FieldType = ItemType.Text;
			itemGrid136.FontColor = Color.Yellow;
			itemGrid136.FontStyle = FontStyle.Regular;
			itemGrid136.Height = 1;
			itemGrid136.IsBlink = 0;
			itemGrid136.Name = "tbForceLMV";
			itemGrid136.Text = "";
			itemGrid136.ValueFormat = FormatType.Text;
			itemGrid136.Visible = true;
			itemGrid136.Width = 15;
			itemGrid136.X = 85;
			itemGrid136.Y = 5;
			itemGrid137.AdjustFontSize = 0f;
			itemGrid137.Alignment = StringAlignment.Near;
			itemGrid137.BackColor = Color.Black;
			itemGrid137.Changed = false;
			itemGrid137.FieldType = ItemType.Label2;
			itemGrid137.FontColor = Color.LightGray;
			itemGrid137.FontStyle = FontStyle.Regular;
			itemGrid137.Height = 1;
			itemGrid137.IsBlink = 0;
			itemGrid137.Name = "lbBMV";
			itemGrid137.Text = "BMV";
			itemGrid137.ValueFormat = FormatType.Text;
			itemGrid137.Visible = true;
			itemGrid137.Width = 10;
			itemGrid137.X = 0;
			itemGrid137.Y = 6;
			itemGrid138.AdjustFontSize = 0f;
			itemGrid138.Alignment = StringAlignment.Near;
			itemGrid138.BackColor = Color.Black;
			itemGrid138.Changed = false;
			itemGrid138.FieldType = ItemType.Text;
			itemGrid138.FontColor = Color.Yellow;
			itemGrid138.FontStyle = FontStyle.Regular;
			itemGrid138.Height = 1;
			itemGrid138.IsBlink = 0;
			itemGrid138.Name = "tbBMV";
			itemGrid138.Text = "";
			itemGrid138.ValueFormat = FormatType.Text;
			itemGrid138.Visible = true;
			itemGrid138.Width = 15;
			itemGrid138.X = 10;
			itemGrid138.Y = 6;
			itemGrid139.AdjustFontSize = 0f;
			itemGrid139.Alignment = StringAlignment.Near;
			itemGrid139.BackColor = Color.Black;
			itemGrid139.Changed = false;
			itemGrid139.FieldType = ItemType.Label2;
			itemGrid139.FontColor = Color.LightGray;
			itemGrid139.FontStyle = FontStyle.Regular;
			itemGrid139.Height = 1;
			itemGrid139.IsBlink = 0;
			itemGrid139.Name = "lbAction";
			itemGrid139.Text = "Action";
			itemGrid139.ValueFormat = FormatType.Text;
			itemGrid139.Visible = true;
			itemGrid139.Width = 10;
			itemGrid139.X = 25;
			itemGrid139.Y = 6;
			itemGrid140.AdjustFontSize = 0f;
			itemGrid140.Alignment = StringAlignment.Near;
			itemGrid140.BackColor = Color.Black;
			itemGrid140.Changed = false;
			itemGrid140.FieldType = ItemType.Text;
			itemGrid140.FontColor = Color.Yellow;
			itemGrid140.FontStyle = FontStyle.Regular;
			itemGrid140.Height = 1;
			itemGrid140.IsBlink = 0;
			itemGrid140.Name = "tbAction";
			itemGrid140.Text = "";
			itemGrid140.ValueFormat = FormatType.Text;
			itemGrid140.Visible = true;
			itemGrid140.Width = 15;
			itemGrid140.X = 35;
			itemGrid140.Y = 6;
			itemGrid141.AdjustFontSize = 0f;
			itemGrid141.Alignment = StringAlignment.Near;
			itemGrid141.BackColor = Color.Black;
			itemGrid141.Changed = false;
			itemGrid141.FieldType = ItemType.Label2;
			itemGrid141.FontColor = Color.LightGray;
			itemGrid141.FontStyle = FontStyle.Regular;
			itemGrid141.Height = 1;
			itemGrid141.IsBlink = 0;
			itemGrid141.Name = "lbBorrowMR";
			itemGrid141.Text = "Borrow MR";
			itemGrid141.ValueFormat = FormatType.Text;
			itemGrid141.Visible = true;
			itemGrid141.Width = 10;
			itemGrid141.X = 50;
			itemGrid141.Y = 6;
			itemGrid142.AdjustFontSize = 0f;
			itemGrid142.Alignment = StringAlignment.Near;
			itemGrid142.BackColor = Color.Black;
			itemGrid142.Changed = false;
			itemGrid142.FieldType = ItemType.Text;
			itemGrid142.FontColor = Color.Yellow;
			itemGrid142.FontStyle = FontStyle.Regular;
			itemGrid142.Height = 1;
			itemGrid142.IsBlink = 0;
			itemGrid142.Name = "tbBorrowMR";
			itemGrid142.Text = "";
			itemGrid142.ValueFormat = FormatType.Text;
			itemGrid142.Visible = true;
			itemGrid142.Width = 15;
			itemGrid142.X = 60;
			itemGrid142.Y = 6;
			itemGrid143.AdjustFontSize = 0f;
			itemGrid143.Alignment = StringAlignment.Near;
			itemGrid143.BackColor = Color.Black;
			itemGrid143.Changed = false;
			itemGrid143.FieldType = ItemType.Label2;
			itemGrid143.FontColor = Color.LightGray;
			itemGrid143.FontStyle = FontStyle.Regular;
			itemGrid143.Height = 1;
			itemGrid143.IsBlink = 0;
			itemGrid143.Name = "lbForceSMV";
			itemGrid143.Text = "Force SMV";
			itemGrid143.ValueFormat = FormatType.Text;
			itemGrid143.Visible = true;
			itemGrid143.Width = 10;
			itemGrid143.X = 75;
			itemGrid143.Y = 6;
			itemGrid144.AdjustFontSize = 0f;
			itemGrid144.Alignment = StringAlignment.Near;
			itemGrid144.BackColor = Color.Black;
			itemGrid144.Changed = false;
			itemGrid144.FieldType = ItemType.Text;
			itemGrid144.FontColor = Color.Yellow;
			itemGrid144.FontStyle = FontStyle.Regular;
			itemGrid144.Height = 1;
			itemGrid144.IsBlink = 0;
			itemGrid144.Name = "tbForceSMV";
			itemGrid144.Text = "";
			itemGrid144.ValueFormat = FormatType.Text;
			itemGrid144.Visible = true;
			itemGrid144.Width = 15;
			itemGrid144.X = 85;
			itemGrid144.Y = 6;
			itemGrid145.AdjustFontSize = 0f;
			itemGrid145.Alignment = StringAlignment.Near;
			itemGrid145.BackColor = Color.Black;
			itemGrid145.Changed = false;
			itemGrid145.FieldType = ItemType.Label2;
			itemGrid145.FontColor = Color.LightGray;
			itemGrid145.FontStyle = FontStyle.Regular;
			itemGrid145.Height = 1;
			itemGrid145.IsBlink = 0;
			itemGrid145.Name = "lbWithdrawal";
			itemGrid145.Text = "Withdrawal";
			itemGrid145.ValueFormat = FormatType.Text;
			itemGrid145.Visible = true;
			itemGrid145.Width = 10;
			itemGrid145.X = 0;
			itemGrid145.Y = 7;
			itemGrid146.AdjustFontSize = 0f;
			itemGrid146.Alignment = StringAlignment.Near;
			itemGrid146.BackColor = Color.Black;
			itemGrid146.Changed = false;
			itemGrid146.FieldType = ItemType.Text;
			itemGrid146.FontColor = Color.Yellow;
			itemGrid146.FontStyle = FontStyle.Regular;
			itemGrid146.Height = 1;
			itemGrid146.IsBlink = 0;
			itemGrid146.Name = "tbWithdrawal";
			itemGrid146.Text = "";
			itemGrid146.ValueFormat = FormatType.Text;
			itemGrid146.Visible = true;
			itemGrid146.Width = 15;
			itemGrid146.X = 10;
			itemGrid146.Y = 7;
			itemGrid147.AdjustFontSize = 0f;
			itemGrid147.Alignment = StringAlignment.Near;
			itemGrid147.BackColor = Color.Black;
			itemGrid147.Changed = false;
			itemGrid147.FieldType = ItemType.Label2;
			itemGrid147.FontColor = Color.LightGray;
			itemGrid147.FontStyle = FontStyle.Regular;
			itemGrid147.Height = 1;
			itemGrid147.IsBlink = 0;
			itemGrid147.Name = "lbMarginRate";
			itemGrid147.Text = "Margin Rate";
			itemGrid147.ValueFormat = FormatType.Text;
			itemGrid147.Visible = true;
			itemGrid147.Width = 10;
			itemGrid147.X = 25;
			itemGrid147.Y = 7;
			itemGrid148.AdjustFontSize = 0f;
			itemGrid148.Alignment = StringAlignment.Near;
			itemGrid148.BackColor = Color.Black;
			itemGrid148.Changed = false;
			itemGrid148.FieldType = ItemType.Text;
			itemGrid148.FontColor = Color.Yellow;
			itemGrid148.FontStyle = FontStyle.Regular;
			itemGrid148.Height = 1;
			itemGrid148.IsBlink = 0;
			itemGrid148.Name = "tbMarginRate";
			itemGrid148.Text = "";
			itemGrid148.ValueFormat = FormatType.Text;
			itemGrid148.Visible = true;
			itemGrid148.Width = 15;
			itemGrid148.X = 35;
			itemGrid148.Y = 7;
			this.intzaCB_Freewill.Items.Add(itemGrid89);
			this.intzaCB_Freewill.Items.Add(itemGrid90);
			this.intzaCB_Freewill.Items.Add(itemGrid91);
			this.intzaCB_Freewill.Items.Add(itemGrid92);
			this.intzaCB_Freewill.Items.Add(itemGrid93);
			this.intzaCB_Freewill.Items.Add(itemGrid94);
			this.intzaCB_Freewill.Items.Add(itemGrid95);
			this.intzaCB_Freewill.Items.Add(itemGrid96);
			this.intzaCB_Freewill.Items.Add(itemGrid97);
			this.intzaCB_Freewill.Items.Add(itemGrid98);
			this.intzaCB_Freewill.Items.Add(itemGrid99);
			this.intzaCB_Freewill.Items.Add(itemGrid100);
			this.intzaCB_Freewill.Items.Add(itemGrid101);
			this.intzaCB_Freewill.Items.Add(itemGrid102);
			this.intzaCB_Freewill.Items.Add(itemGrid103);
			this.intzaCB_Freewill.Items.Add(itemGrid104);
			this.intzaCB_Freewill.Items.Add(itemGrid105);
			this.intzaCB_Freewill.Items.Add(itemGrid106);
			this.intzaCB_Freewill.Items.Add(itemGrid107);
			this.intzaCB_Freewill.Items.Add(itemGrid108);
			this.intzaCB_Freewill.Items.Add(itemGrid109);
			this.intzaCB_Freewill.Items.Add(itemGrid110);
			this.intzaCB_Freewill.Items.Add(itemGrid111);
			this.intzaCB_Freewill.Items.Add(itemGrid112);
			this.intzaCB_Freewill.Items.Add(itemGrid113);
			this.intzaCB_Freewill.Items.Add(itemGrid114);
			this.intzaCB_Freewill.Items.Add(itemGrid115);
			this.intzaCB_Freewill.Items.Add(itemGrid116);
			this.intzaCB_Freewill.Items.Add(itemGrid117);
			this.intzaCB_Freewill.Items.Add(itemGrid118);
			this.intzaCB_Freewill.Items.Add(itemGrid119);
			this.intzaCB_Freewill.Items.Add(itemGrid120);
			this.intzaCB_Freewill.Items.Add(itemGrid121);
			this.intzaCB_Freewill.Items.Add(itemGrid122);
			this.intzaCB_Freewill.Items.Add(itemGrid123);
			this.intzaCB_Freewill.Items.Add(itemGrid124);
			this.intzaCB_Freewill.Items.Add(itemGrid125);
			this.intzaCB_Freewill.Items.Add(itemGrid126);
			this.intzaCB_Freewill.Items.Add(itemGrid127);
			this.intzaCB_Freewill.Items.Add(itemGrid128);
			this.intzaCB_Freewill.Items.Add(itemGrid129);
			this.intzaCB_Freewill.Items.Add(itemGrid130);
			this.intzaCB_Freewill.Items.Add(itemGrid131);
			this.intzaCB_Freewill.Items.Add(itemGrid132);
			this.intzaCB_Freewill.Items.Add(itemGrid133);
			this.intzaCB_Freewill.Items.Add(itemGrid134);
			this.intzaCB_Freewill.Items.Add(itemGrid135);
			this.intzaCB_Freewill.Items.Add(itemGrid136);
			this.intzaCB_Freewill.Items.Add(itemGrid137);
			this.intzaCB_Freewill.Items.Add(itemGrid138);
			this.intzaCB_Freewill.Items.Add(itemGrid139);
			this.intzaCB_Freewill.Items.Add(itemGrid140);
			this.intzaCB_Freewill.Items.Add(itemGrid141);
			this.intzaCB_Freewill.Items.Add(itemGrid142);
			this.intzaCB_Freewill.Items.Add(itemGrid143);
			this.intzaCB_Freewill.Items.Add(itemGrid144);
			this.intzaCB_Freewill.Items.Add(itemGrid145);
			this.intzaCB_Freewill.Items.Add(itemGrid146);
			this.intzaCB_Freewill.Items.Add(itemGrid147);
			this.intzaCB_Freewill.Items.Add(itemGrid148);
			this.intzaCB_Freewill.LineColor = Color.Red;
			this.intzaCB_Freewill.Location = new Point(3, 105);
			this.intzaCB_Freewill.Name = "intzaCB_Freewill";
			this.intzaCB_Freewill.Size = new Size(763, 153);
			this.intzaCB_Freewill.TabIndex = 24;
			this.intzaCB_Freewill.Visible = false;
			this.intzaCB.AllowDrop = true;
			this.intzaCB.BackColor = Color.Black;
			this.intzaCB.BorderStyle = BorderStyle.FixedSingle;
			this.intzaCB.CanDrag = false;
			this.intzaCB.IsAutoRepaint = true;
			this.intzaCB.IsDroped = false;
			itemGrid149.AdjustFontSize = 0f;
			itemGrid149.Alignment = StringAlignment.Near;
			itemGrid149.BackColor = Color.DimGray;
			itemGrid149.Changed = true;
			itemGrid149.FieldType = ItemType.TextGradient;
			itemGrid149.FontColor = Color.WhiteSmoke;
			itemGrid149.FontStyle = FontStyle.Regular;
			itemGrid149.Height = 1;
			itemGrid149.IsBlink = 0;
			itemGrid149.Name = "lbBankCol";
			itemGrid149.Text = "";
			itemGrid149.ValueFormat = FormatType.Text;
			itemGrid149.Visible = true;
			itemGrid149.Width = 30;
			itemGrid149.X = 0;
			itemGrid149.Y = 1;
			itemGrid150.AdjustFontSize = 0f;
			itemGrid150.Alignment = StringAlignment.Center;
			itemGrid150.BackColor = Color.DimGray;
			itemGrid150.Changed = true;
			itemGrid150.FieldType = ItemType.TextGradient;
			itemGrid150.FontColor = Color.WhiteSmoke;
			itemGrid150.FontStyle = FontStyle.Regular;
			itemGrid150.Height = 1;
			itemGrid150.IsBlink = 0;
			itemGrid150.Name = "lbPrevious";
			itemGrid150.Text = "Previous";
			itemGrid150.ValueFormat = FormatType.Text;
			itemGrid150.Visible = true;
			itemGrid150.Width = 35;
			itemGrid150.X = 30;
			itemGrid150.Y = 1;
			itemGrid151.AdjustFontSize = 0f;
			itemGrid151.Alignment = StringAlignment.Center;
			itemGrid151.BackColor = Color.DimGray;
			itemGrid151.Changed = true;
			itemGrid151.FieldType = ItemType.TextGradient;
			itemGrid151.FontColor = Color.WhiteSmoke;
			itemGrid151.FontStyle = FontStyle.Regular;
			itemGrid151.Height = 1;
			itemGrid151.IsBlink = 0;
			itemGrid151.Name = "lbCurrent";
			itemGrid151.Text = "Current";
			itemGrid151.ValueFormat = FormatType.Text;
			itemGrid151.Visible = true;
			itemGrid151.Width = 35;
			itemGrid151.X = 65;
			itemGrid151.Y = 1;
			itemGrid152.AdjustFontSize = -1f;
			itemGrid152.Alignment = StringAlignment.Far;
			itemGrid152.BackColor = Color.FromArgb(64, 64, 64);
			itemGrid152.Changed = true;
			itemGrid152.FieldType = ItemType.Label;
			itemGrid152.FontColor = Color.WhiteSmoke;
			itemGrid152.FontStyle = FontStyle.Regular;
			itemGrid152.Height = 1;
			itemGrid152.IsBlink = 0;
			itemGrid152.Name = "lbExcessEquity";
			itemGrid152.Text = "Excess Equity";
			itemGrid152.ValueFormat = FormatType.Text;
			itemGrid152.Visible = true;
			itemGrid152.Width = 30;
			itemGrid152.X = 0;
			itemGrid152.Y = 2;
			itemGrid153.AdjustFontSize = 0f;
			itemGrid153.Alignment = StringAlignment.Near;
			itemGrid153.BackColor = Color.Black;
			itemGrid153.Changed = false;
			itemGrid153.FieldType = ItemType.Text;
			itemGrid153.FontColor = Color.White;
			itemGrid153.FontStyle = FontStyle.Regular;
			itemGrid153.Height = 1;
			itemGrid153.IsBlink = 0;
			itemGrid153.Name = "tbExcessEquityPrevious";
			itemGrid153.Text = "";
			itemGrid153.ValueFormat = FormatType.Text;
			itemGrid153.Visible = true;
			itemGrid153.Width = 35;
			itemGrid153.X = 30;
			itemGrid153.Y = 2;
			itemGrid154.AdjustFontSize = 0f;
			itemGrid154.Alignment = StringAlignment.Near;
			itemGrid154.BackColor = Color.Black;
			itemGrid154.Changed = false;
			itemGrid154.FieldType = ItemType.Text;
			itemGrid154.FontColor = Color.White;
			itemGrid154.FontStyle = FontStyle.Regular;
			itemGrid154.Height = 1;
			itemGrid154.IsBlink = 0;
			itemGrid154.Name = "tbExcessEquityCurrent";
			itemGrid154.Text = "";
			itemGrid154.ValueFormat = FormatType.Text;
			itemGrid154.Visible = true;
			itemGrid154.Width = 35;
			itemGrid154.X = 65;
			itemGrid154.Y = 2;
			itemGrid155.AdjustFontSize = -1f;
			itemGrid155.Alignment = StringAlignment.Far;
			itemGrid155.BackColor = Color.FromArgb(64, 64, 64);
			itemGrid155.Changed = true;
			itemGrid155.FieldType = ItemType.Label;
			itemGrid155.FontColor = Color.WhiteSmoke;
			itemGrid155.FontStyle = FontStyle.Regular;
			itemGrid155.Height = 1;
			itemGrid155.IsBlink = 0;
			itemGrid155.Name = "lbMarkToEE";
			itemGrid155.Text = "Mark to Market EE";
			itemGrid155.ValueFormat = FormatType.Text;
			itemGrid155.Visible = true;
			itemGrid155.Width = 30;
			itemGrid155.X = 0;
			itemGrid155.Y = 3;
			itemGrid156.AdjustFontSize = 0f;
			itemGrid156.Alignment = StringAlignment.Near;
			itemGrid156.BackColor = Color.Black;
			itemGrid156.Changed = false;
			itemGrid156.FieldType = ItemType.Text;
			itemGrid156.FontColor = Color.White;
			itemGrid156.FontStyle = FontStyle.Regular;
			itemGrid156.Height = 1;
			itemGrid156.IsBlink = 0;
			itemGrid156.Name = "tbMarkToEEPrevious";
			itemGrid156.Text = "";
			itemGrid156.ValueFormat = FormatType.Text;
			itemGrid156.Visible = true;
			itemGrid156.Width = 35;
			itemGrid156.X = 30;
			itemGrid156.Y = 3;
			itemGrid157.AdjustFontSize = 0f;
			itemGrid157.Alignment = StringAlignment.Near;
			itemGrid157.BackColor = Color.Black;
			itemGrid157.Changed = false;
			itemGrid157.FieldType = ItemType.Text;
			itemGrid157.FontColor = Color.White;
			itemGrid157.FontStyle = FontStyle.Regular;
			itemGrid157.Height = 1;
			itemGrid157.IsBlink = 0;
			itemGrid157.Name = "tbMarkToEECurrent";
			itemGrid157.Text = "";
			itemGrid157.ValueFormat = FormatType.Text;
			itemGrid157.Visible = true;
			itemGrid157.Width = 35;
			itemGrid157.X = 65;
			itemGrid157.Y = 3;
			itemGrid158.AdjustFontSize = -1f;
			itemGrid158.Alignment = StringAlignment.Far;
			itemGrid158.BackColor = Color.FromArgb(64, 64, 64);
			itemGrid158.Changed = true;
			itemGrid158.FieldType = ItemType.Label;
			itemGrid158.FontColor = Color.WhiteSmoke;
			itemGrid158.FontStyle = FontStyle.Regular;
			itemGrid158.Height = 1;
			itemGrid158.IsBlink = 0;
			itemGrid158.Name = "lbMMpercent";
			itemGrid158.Text = "MM%";
			itemGrid158.ValueFormat = FormatType.Text;
			itemGrid158.Visible = true;
			itemGrid158.Width = 30;
			itemGrid158.X = 0;
			itemGrid158.Y = 4;
			itemGrid159.AdjustFontSize = 0f;
			itemGrid159.Alignment = StringAlignment.Near;
			itemGrid159.BackColor = Color.Black;
			itemGrid159.Changed = false;
			itemGrid159.FieldType = ItemType.Text;
			itemGrid159.FontColor = Color.White;
			itemGrid159.FontStyle = FontStyle.Regular;
			itemGrid159.Height = 1;
			itemGrid159.IsBlink = 0;
			itemGrid159.Name = "tbMMpercentPrevious";
			itemGrid159.Text = "";
			itemGrid159.ValueFormat = FormatType.PercentChange;
			itemGrid159.Visible = true;
			itemGrid159.Width = 35;
			itemGrid159.X = 30;
			itemGrid159.Y = 4;
			itemGrid160.AdjustFontSize = 0f;
			itemGrid160.Alignment = StringAlignment.Near;
			itemGrid160.BackColor = Color.Black;
			itemGrid160.Changed = false;
			itemGrid160.FieldType = ItemType.Text;
			itemGrid160.FontColor = Color.White;
			itemGrid160.FontStyle = FontStyle.Regular;
			itemGrid160.Height = 1;
			itemGrid160.IsBlink = 0;
			itemGrid160.Name = "tbMMpercentCurrent";
			itemGrid160.Text = "";
			itemGrid160.ValueFormat = FormatType.PercentChange;
			itemGrid160.Visible = true;
			itemGrid160.Width = 35;
			itemGrid160.X = 65;
			itemGrid160.Y = 4;
			itemGrid161.AdjustFontSize = -1f;
			itemGrid161.Alignment = StringAlignment.Far;
			itemGrid161.BackColor = Color.FromArgb(64, 64, 64);
			itemGrid161.Changed = true;
			itemGrid161.FieldType = ItemType.Label;
			itemGrid161.FontColor = Color.WhiteSmoke;
			itemGrid161.FontStyle = FontStyle.Regular;
			itemGrid161.Height = 1;
			itemGrid161.IsBlink = 0;
			itemGrid161.Name = "lbEquity";
			itemGrid161.Text = "Equity";
			itemGrid161.ValueFormat = FormatType.Text;
			itemGrid161.Visible = true;
			itemGrid161.Width = 30;
			itemGrid161.X = 0;
			itemGrid161.Y = 5;
			itemGrid162.AdjustFontSize = 0f;
			itemGrid162.Alignment = StringAlignment.Near;
			itemGrid162.BackColor = Color.Black;
			itemGrid162.Changed = false;
			itemGrid162.FieldType = ItemType.Text;
			itemGrid162.FontColor = Color.White;
			itemGrid162.FontStyle = FontStyle.Regular;
			itemGrid162.Height = 1;
			itemGrid162.IsBlink = 0;
			itemGrid162.Name = "tbEquityPrevious";
			itemGrid162.Text = "";
			itemGrid162.ValueFormat = FormatType.Price;
			itemGrid162.Visible = true;
			itemGrid162.Width = 35;
			itemGrid162.X = 30;
			itemGrid162.Y = 5;
			itemGrid163.AdjustFontSize = 0f;
			itemGrid163.Alignment = StringAlignment.Near;
			itemGrid163.BackColor = Color.Black;
			itemGrid163.Changed = false;
			itemGrid163.FieldType = ItemType.Text;
			itemGrid163.FontColor = Color.White;
			itemGrid163.FontStyle = FontStyle.Regular;
			itemGrid163.Height = 1;
			itemGrid163.IsBlink = 0;
			itemGrid163.Name = "tbEquityCurrent";
			itemGrid163.Text = "";
			itemGrid163.ValueFormat = FormatType.Price;
			itemGrid163.Visible = true;
			itemGrid163.Width = 35;
			itemGrid163.X = 65;
			itemGrid163.Y = 5;
			itemGrid164.AdjustFontSize = -1f;
			itemGrid164.Alignment = StringAlignment.Far;
			itemGrid164.BackColor = Color.FromArgb(64, 64, 64);
			itemGrid164.Changed = true;
			itemGrid164.FieldType = ItemType.Label;
			itemGrid164.FontColor = Color.WhiteSmoke;
			itemGrid164.FontStyle = FontStyle.Regular;
			itemGrid164.Height = 1;
			itemGrid164.IsBlink = 0;
			itemGrid164.Name = "lbMR";
			itemGrid164.Text = "MR";
			itemGrid164.ValueFormat = FormatType.Text;
			itemGrid164.Visible = true;
			itemGrid164.Width = 30;
			itemGrid164.X = 0;
			itemGrid164.Y = 6;
			itemGrid165.AdjustFontSize = 0f;
			itemGrid165.Alignment = StringAlignment.Near;
			itemGrid165.BackColor = Color.Black;
			itemGrid165.Changed = false;
			itemGrid165.FieldType = ItemType.Text;
			itemGrid165.FontColor = Color.White;
			itemGrid165.FontStyle = FontStyle.Regular;
			itemGrid165.Height = 1;
			itemGrid165.IsBlink = 0;
			itemGrid165.Name = "tbMRPrevious";
			itemGrid165.Text = "";
			itemGrid165.ValueFormat = FormatType.Text;
			itemGrid165.Visible = true;
			itemGrid165.Width = 35;
			itemGrid165.X = 30;
			itemGrid165.Y = 6;
			itemGrid166.AdjustFontSize = 0f;
			itemGrid166.Alignment = StringAlignment.Near;
			itemGrid166.BackColor = Color.Black;
			itemGrid166.Changed = false;
			itemGrid166.FieldType = ItemType.Text;
			itemGrid166.FontColor = Color.White;
			itemGrid166.FontStyle = FontStyle.Regular;
			itemGrid166.Height = 1;
			itemGrid166.IsBlink = 0;
			itemGrid166.Name = "tbMRCurrent";
			itemGrid166.Text = "";
			itemGrid166.ValueFormat = FormatType.Text;
			itemGrid166.Visible = true;
			itemGrid166.Width = 35;
			itemGrid166.X = 65;
			itemGrid166.Y = 6;
			itemGrid167.AdjustFontSize = 0f;
			itemGrid167.Alignment = StringAlignment.Near;
			itemGrid167.BackColor = Color.DimGray;
			itemGrid167.Changed = true;
			itemGrid167.FieldType = ItemType.TextGradient;
			itemGrid167.FontColor = Color.WhiteSmoke;
			itemGrid167.FontStyle = FontStyle.Regular;
			itemGrid167.Height = 1;
			itemGrid167.IsBlink = 0;
			itemGrid167.Name = "lbAsset";
			itemGrid167.Text = "ASSET";
			itemGrid167.ValueFormat = FormatType.Text;
			itemGrid167.Visible = true;
			itemGrid167.Width = 30;
			itemGrid167.X = 0;
			itemGrid167.Y = 7;
			itemGrid168.AdjustFontSize = 0f;
			itemGrid168.Alignment = StringAlignment.Near;
			itemGrid168.BackColor = Color.DimGray;
			itemGrid168.Changed = true;
			itemGrid168.FieldType = ItemType.TextGradient;
			itemGrid168.FontColor = Color.WhiteSmoke;
			itemGrid168.FontStyle = FontStyle.Regular;
			itemGrid168.Height = 1;
			itemGrid168.IsBlink = 0;
			itemGrid168.Name = "lbAssetPrevious";
			itemGrid168.Text = "";
			itemGrid168.ValueFormat = FormatType.Text;
			itemGrid168.Visible = true;
			itemGrid168.Width = 35;
			itemGrid168.X = 30;
			itemGrid168.Y = 7;
			itemGrid169.AdjustFontSize = 0f;
			itemGrid169.Alignment = StringAlignment.Near;
			itemGrid169.BackColor = Color.DimGray;
			itemGrid169.Changed = true;
			itemGrid169.FieldType = ItemType.TextGradient;
			itemGrid169.FontColor = Color.WhiteSmoke;
			itemGrid169.FontStyle = FontStyle.Regular;
			itemGrid169.Height = 1;
			itemGrid169.IsBlink = 0;
			itemGrid169.Name = "lbAssetCurrent";
			itemGrid169.Text = "";
			itemGrid169.ValueFormat = FormatType.Text;
			itemGrid169.Visible = true;
			itemGrid169.Width = 35;
			itemGrid169.X = 65;
			itemGrid169.Y = 7;
			itemGrid170.AdjustFontSize = -1f;
			itemGrid170.Alignment = StringAlignment.Far;
			itemGrid170.BackColor = Color.FromArgb(64, 64, 64);
			itemGrid170.Changed = true;
			itemGrid170.FieldType = ItemType.Label;
			itemGrid170.FontColor = Color.WhiteSmoke;
			itemGrid170.FontStyle = FontStyle.Regular;
			itemGrid170.Height = 1;
			itemGrid170.IsBlink = 0;
			itemGrid170.Name = "lbCashBalance";
			itemGrid170.Text = "Cash Balance";
			itemGrid170.ValueFormat = FormatType.Text;
			itemGrid170.Visible = true;
			itemGrid170.Width = 30;
			itemGrid170.X = 0;
			itemGrid170.Y = 8;
			itemGrid171.AdjustFontSize = 0f;
			itemGrid171.Alignment = StringAlignment.Near;
			itemGrid171.BackColor = Color.Black;
			itemGrid171.Changed = false;
			itemGrid171.FieldType = ItemType.Text;
			itemGrid171.FontColor = Color.White;
			itemGrid171.FontStyle = FontStyle.Regular;
			itemGrid171.Height = 1;
			itemGrid171.IsBlink = 0;
			itemGrid171.Name = "tbCashBalancePrevious";
			itemGrid171.Text = "";
			itemGrid171.ValueFormat = FormatType.Text;
			itemGrid171.Visible = true;
			itemGrid171.Width = 35;
			itemGrid171.X = 30;
			itemGrid171.Y = 8;
			itemGrid172.AdjustFontSize = 0f;
			itemGrid172.Alignment = StringAlignment.Near;
			itemGrid172.BackColor = Color.Black;
			itemGrid172.Changed = false;
			itemGrid172.FieldType = ItemType.Text;
			itemGrid172.FontColor = Color.White;
			itemGrid172.FontStyle = FontStyle.Regular;
			itemGrid172.Height = 1;
			itemGrid172.IsBlink = 0;
			itemGrid172.Name = "tbCashBalanceCurrent";
			itemGrid172.Text = "";
			itemGrid172.ValueFormat = FormatType.Text;
			itemGrid172.Visible = true;
			itemGrid172.Width = 35;
			itemGrid172.X = 65;
			itemGrid172.Y = 8;
			itemGrid173.AdjustFontSize = -1f;
			itemGrid173.Alignment = StringAlignment.Far;
			itemGrid173.BackColor = Color.FromArgb(64, 64, 64);
			itemGrid173.Changed = true;
			itemGrid173.FieldType = ItemType.Label;
			itemGrid173.FontColor = Color.WhiteSmoke;
			itemGrid173.FontStyle = FontStyle.Regular;
			itemGrid173.Height = 1;
			itemGrid173.IsBlink = 0;
			itemGrid173.Name = "lbLmv";
			itemGrid173.Text = "LMV";
			itemGrid173.ValueFormat = FormatType.Text;
			itemGrid173.Visible = true;
			itemGrid173.Width = 30;
			itemGrid173.X = 0;
			itemGrid173.Y = 9;
			itemGrid174.AdjustFontSize = 0f;
			itemGrid174.Alignment = StringAlignment.Near;
			itemGrid174.BackColor = Color.Black;
			itemGrid174.Changed = false;
			itemGrid174.FieldType = ItemType.Text;
			itemGrid174.FontColor = Color.White;
			itemGrid174.FontStyle = FontStyle.Regular;
			itemGrid174.Height = 1;
			itemGrid174.IsBlink = 0;
			itemGrid174.Name = "tbLmvPrevious";
			itemGrid174.Text = "";
			itemGrid174.ValueFormat = FormatType.Text;
			itemGrid174.Visible = true;
			itemGrid174.Width = 35;
			itemGrid174.X = 30;
			itemGrid174.Y = 9;
			itemGrid175.AdjustFontSize = 0f;
			itemGrid175.Alignment = StringAlignment.Near;
			itemGrid175.BackColor = Color.Black;
			itemGrid175.Changed = false;
			itemGrid175.FieldType = ItemType.Text;
			itemGrid175.FontColor = Color.White;
			itemGrid175.FontStyle = FontStyle.Regular;
			itemGrid175.Height = 1;
			itemGrid175.IsBlink = 0;
			itemGrid175.Name = "tbLmvCurrent";
			itemGrid175.Text = "";
			itemGrid175.ValueFormat = FormatType.Text;
			itemGrid175.Visible = true;
			itemGrid175.Width = 35;
			itemGrid175.X = 65;
			itemGrid175.Y = 9;
			itemGrid176.AdjustFontSize = -1f;
			itemGrid176.Alignment = StringAlignment.Far;
			itemGrid176.BackColor = Color.FromArgb(64, 64, 64);
			itemGrid176.Changed = true;
			itemGrid176.FieldType = ItemType.Label;
			itemGrid176.FontColor = Color.WhiteSmoke;
			itemGrid176.FontStyle = FontStyle.Regular;
			itemGrid176.Height = 1;
			itemGrid176.IsBlink = 0;
			itemGrid176.Name = "lbColleteral";
			itemGrid176.Text = "Colleteral";
			itemGrid176.ValueFormat = FormatType.Text;
			itemGrid176.Visible = true;
			itemGrid176.Width = 30;
			itemGrid176.X = 0;
			itemGrid176.Y = 10;
			itemGrid177.AdjustFontSize = 0f;
			itemGrid177.Alignment = StringAlignment.Near;
			itemGrid177.BackColor = Color.Black;
			itemGrid177.Changed = false;
			itemGrid177.FieldType = ItemType.Text;
			itemGrid177.FontColor = Color.White;
			itemGrid177.FontStyle = FontStyle.Regular;
			itemGrid177.Height = 1;
			itemGrid177.IsBlink = 0;
			itemGrid177.Name = "tbColleteralPrevious";
			itemGrid177.Text = "";
			itemGrid177.ValueFormat = FormatType.Text;
			itemGrid177.Visible = true;
			itemGrid177.Width = 35;
			itemGrid177.X = 30;
			itemGrid177.Y = 10;
			itemGrid178.AdjustFontSize = 0f;
			itemGrid178.Alignment = StringAlignment.Near;
			itemGrid178.BackColor = Color.Black;
			itemGrid178.Changed = false;
			itemGrid178.FieldType = ItemType.Text;
			itemGrid178.FontColor = Color.White;
			itemGrid178.FontStyle = FontStyle.Regular;
			itemGrid178.Height = 1;
			itemGrid178.IsBlink = 0;
			itemGrid178.Name = "tbColleteralCurrent";
			itemGrid178.Text = "";
			itemGrid178.ValueFormat = FormatType.Text;
			itemGrid178.Visible = true;
			itemGrid178.Width = 35;
			itemGrid178.X = 65;
			itemGrid178.Y = 10;
			itemGrid179.AdjustFontSize = 0f;
			itemGrid179.Alignment = StringAlignment.Near;
			itemGrid179.BackColor = Color.DimGray;
			itemGrid179.Changed = true;
			itemGrid179.FieldType = ItemType.TextGradient;
			itemGrid179.FontColor = Color.WhiteSmoke;
			itemGrid179.FontStyle = FontStyle.Regular;
			itemGrid179.Height = 1;
			itemGrid179.IsBlink = 0;
			itemGrid179.Name = "lbLiabilities";
			itemGrid179.Text = "LIABILITIES";
			itemGrid179.ValueFormat = FormatType.Text;
			itemGrid179.Visible = true;
			itemGrid179.Width = 30;
			itemGrid179.X = 0;
			itemGrid179.Y = 11;
			itemGrid180.AdjustFontSize = 0f;
			itemGrid180.Alignment = StringAlignment.Near;
			itemGrid180.BackColor = Color.DimGray;
			itemGrid180.Changed = true;
			itemGrid180.FieldType = ItemType.TextGradient;
			itemGrid180.FontColor = Color.WhiteSmoke;
			itemGrid180.FontStyle = FontStyle.Regular;
			itemGrid180.Height = 1;
			itemGrid180.IsBlink = 0;
			itemGrid180.Name = "lbLiabilitiesPrevious";
			itemGrid180.Text = "";
			itemGrid180.ValueFormat = FormatType.Text;
			itemGrid180.Visible = true;
			itemGrid180.Width = 35;
			itemGrid180.X = 30;
			itemGrid180.Y = 11;
			itemGrid181.AdjustFontSize = 0f;
			itemGrid181.Alignment = StringAlignment.Near;
			itemGrid181.BackColor = Color.DimGray;
			itemGrid181.Changed = true;
			itemGrid181.FieldType = ItemType.TextGradient;
			itemGrid181.FontColor = Color.WhiteSmoke;
			itemGrid181.FontStyle = FontStyle.Regular;
			itemGrid181.Height = 1;
			itemGrid181.IsBlink = 0;
			itemGrid181.Name = "lbLiabilitiesCurrent";
			itemGrid181.Text = "";
			itemGrid181.ValueFormat = FormatType.Text;
			itemGrid181.Visible = true;
			itemGrid181.Width = 35;
			itemGrid181.X = 65;
			itemGrid181.Y = 11;
			itemGrid182.AdjustFontSize = -1f;
			itemGrid182.Alignment = StringAlignment.Far;
			itemGrid182.BackColor = Color.FromArgb(64, 64, 64);
			itemGrid182.Changed = true;
			itemGrid182.FieldType = ItemType.Label;
			itemGrid182.FontColor = Color.WhiteSmoke;
			itemGrid182.FontStyle = FontStyle.Regular;
			itemGrid182.Height = 1;
			itemGrid182.IsBlink = 0;
			itemGrid182.Name = "lbLoan";
			itemGrid182.Text = "Loan";
			itemGrid182.ValueFormat = FormatType.Text;
			itemGrid182.Visible = true;
			itemGrid182.Width = 30;
			itemGrid182.X = 0;
			itemGrid182.Y = 12;
			itemGrid183.AdjustFontSize = 0f;
			itemGrid183.Alignment = StringAlignment.Near;
			itemGrid183.BackColor = Color.Black;
			itemGrid183.Changed = false;
			itemGrid183.FieldType = ItemType.Text;
			itemGrid183.FontColor = Color.White;
			itemGrid183.FontStyle = FontStyle.Regular;
			itemGrid183.Height = 1;
			itemGrid183.IsBlink = 0;
			itemGrid183.Name = "tbLoanPrevious";
			itemGrid183.Text = "";
			itemGrid183.ValueFormat = FormatType.Text;
			itemGrid183.Visible = true;
			itemGrid183.Width = 35;
			itemGrid183.X = 30;
			itemGrid183.Y = 12;
			itemGrid184.AdjustFontSize = 0f;
			itemGrid184.Alignment = StringAlignment.Near;
			itemGrid184.BackColor = Color.Black;
			itemGrid184.Changed = false;
			itemGrid184.FieldType = ItemType.Text;
			itemGrid184.FontColor = Color.White;
			itemGrid184.FontStyle = FontStyle.Regular;
			itemGrid184.Height = 1;
			itemGrid184.IsBlink = 0;
			itemGrid184.Name = "tbLoanCurrent";
			itemGrid184.Text = "";
			itemGrid184.ValueFormat = FormatType.Text;
			itemGrid184.Visible = true;
			itemGrid184.Width = 35;
			itemGrid184.X = 65;
			itemGrid184.Y = 12;
			itemGrid185.AdjustFontSize = -1f;
			itemGrid185.Alignment = StringAlignment.Far;
			itemGrid185.BackColor = Color.FromArgb(64, 64, 64);
			itemGrid185.Changed = true;
			itemGrid185.FieldType = ItemType.Label;
			itemGrid185.FontColor = Color.WhiteSmoke;
			itemGrid185.FontStyle = FontStyle.Regular;
			itemGrid185.Height = 1;
			itemGrid185.IsBlink = 0;
			itemGrid185.Name = "lbSmv";
			itemGrid185.Text = "SMV";
			itemGrid185.ValueFormat = FormatType.Price;
			itemGrid185.Visible = true;
			itemGrid185.Width = 30;
			itemGrid185.X = 0;
			itemGrid185.Y = 13;
			itemGrid186.AdjustFontSize = 0f;
			itemGrid186.Alignment = StringAlignment.Near;
			itemGrid186.BackColor = Color.Black;
			itemGrid186.Changed = false;
			itemGrid186.FieldType = ItemType.Text;
			itemGrid186.FontColor = Color.White;
			itemGrid186.FontStyle = FontStyle.Regular;
			itemGrid186.Height = 1;
			itemGrid186.IsBlink = 0;
			itemGrid186.Name = "tbSmvPrevious";
			itemGrid186.Text = "";
			itemGrid186.ValueFormat = FormatType.Text;
			itemGrid186.Visible = true;
			itemGrid186.Width = 35;
			itemGrid186.X = 30;
			itemGrid186.Y = 13;
			itemGrid187.AdjustFontSize = 0f;
			itemGrid187.Alignment = StringAlignment.Near;
			itemGrid187.BackColor = Color.Black;
			itemGrid187.Changed = false;
			itemGrid187.FieldType = ItemType.Text;
			itemGrid187.FontColor = Color.White;
			itemGrid187.FontStyle = FontStyle.Regular;
			itemGrid187.Height = 1;
			itemGrid187.IsBlink = 0;
			itemGrid187.Name = "tbSmvCurrent";
			itemGrid187.Text = "";
			itemGrid187.ValueFormat = FormatType.Text;
			itemGrid187.Visible = true;
			itemGrid187.Width = 35;
			itemGrid187.X = 65;
			itemGrid187.Y = 13;
			itemGrid188.AdjustFontSize = 0f;
			itemGrid188.Alignment = StringAlignment.Near;
			itemGrid188.BackColor = Color.DimGray;
			itemGrid188.Changed = true;
			itemGrid188.FieldType = ItemType.TextGradient;
			itemGrid188.FontColor = Color.WhiteSmoke;
			itemGrid188.FontStyle = FontStyle.Regular;
			itemGrid188.Height = 1;
			itemGrid188.IsBlink = 0;
			itemGrid188.Name = "lbBankCol1Previous";
			itemGrid188.Text = "";
			itemGrid188.ValueFormat = FormatType.Text;
			itemGrid188.Visible = true;
			itemGrid188.Width = 35;
			itemGrid188.X = 30;
			itemGrid188.Y = 14;
			itemGrid189.AdjustFontSize = 0f;
			itemGrid189.Alignment = StringAlignment.Near;
			itemGrid189.BackColor = Color.DimGray;
			itemGrid189.Changed = true;
			itemGrid189.FieldType = ItemType.TextGradient;
			itemGrid189.FontColor = Color.WhiteSmoke;
			itemGrid189.FontStyle = FontStyle.Regular;
			itemGrid189.Height = 1;
			itemGrid189.IsBlink = 0;
			itemGrid189.Name = "lbBankCol1";
			itemGrid189.Text = "CALL & FORCE";
			itemGrid189.ValueFormat = FormatType.Text;
			itemGrid189.Visible = true;
			itemGrid189.Width = 30;
			itemGrid189.X = 0;
			itemGrid189.Y = 14;
			itemGrid190.AdjustFontSize = 0f;
			itemGrid190.Alignment = StringAlignment.Near;
			itemGrid190.BackColor = Color.DimGray;
			itemGrid190.Changed = true;
			itemGrid190.FieldType = ItemType.TextGradient;
			itemGrid190.FontColor = Color.WhiteSmoke;
			itemGrid190.FontStyle = FontStyle.Regular;
			itemGrid190.Height = 1;
			itemGrid190.IsBlink = 0;
			itemGrid190.Name = "lbBankCol1Current";
			itemGrid190.Text = "";
			itemGrid190.ValueFormat = FormatType.Text;
			itemGrid190.Visible = true;
			itemGrid190.Width = 35;
			itemGrid190.X = 65;
			itemGrid190.Y = 14;
			itemGrid191.AdjustFontSize = -1f;
			itemGrid191.Alignment = StringAlignment.Far;
			itemGrid191.BackColor = Color.FromArgb(64, 64, 64);
			itemGrid191.Changed = true;
			itemGrid191.FieldType = ItemType.Label;
			itemGrid191.FontColor = Color.WhiteSmoke;
			itemGrid191.FontStyle = FontStyle.Regular;
			itemGrid191.Height = 1;
			itemGrid191.IsBlink = 0;
			itemGrid191.Name = "lbCall";
			itemGrid191.Text = "Call";
			itemGrid191.ValueFormat = FormatType.Text;
			itemGrid191.Visible = true;
			itemGrid191.Width = 30;
			itemGrid191.X = 0;
			itemGrid191.Y = 15;
			itemGrid192.AdjustFontSize = 0f;
			itemGrid192.Alignment = StringAlignment.Near;
			itemGrid192.BackColor = Color.Black;
			itemGrid192.Changed = true;
			itemGrid192.FieldType = ItemType.Text;
			itemGrid192.FontColor = Color.Red;
			itemGrid192.FontStyle = FontStyle.Regular;
			itemGrid192.Height = 1;
			itemGrid192.IsBlink = 0;
			itemGrid192.Name = "tbCallPrevious";
			itemGrid192.Text = "";
			itemGrid192.ValueFormat = FormatType.Text;
			itemGrid192.Visible = true;
			itemGrid192.Width = 35;
			itemGrid192.X = 30;
			itemGrid192.Y = 15;
			itemGrid193.AdjustFontSize = 0f;
			itemGrid193.Alignment = StringAlignment.Near;
			itemGrid193.BackColor = Color.Black;
			itemGrid193.Changed = true;
			itemGrid193.FieldType = ItemType.Text;
			itemGrid193.FontColor = Color.Red;
			itemGrid193.FontStyle = FontStyle.Regular;
			itemGrid193.Height = 1;
			itemGrid193.IsBlink = 0;
			itemGrid193.Name = "tbCallCurrent";
			itemGrid193.Text = "";
			itemGrid193.ValueFormat = FormatType.Text;
			itemGrid193.Visible = true;
			itemGrid193.Width = 35;
			itemGrid193.X = 65;
			itemGrid193.Y = 15;
			itemGrid194.AdjustFontSize = -1f;
			itemGrid194.Alignment = StringAlignment.Far;
			itemGrid194.BackColor = Color.FromArgb(64, 64, 64);
			itemGrid194.Changed = true;
			itemGrid194.FieldType = ItemType.Label;
			itemGrid194.FontColor = Color.WhiteSmoke;
			itemGrid194.FontStyle = FontStyle.Regular;
			itemGrid194.Height = 1;
			itemGrid194.IsBlink = 0;
			itemGrid194.Name = "lbForce";
			itemGrid194.Text = "Force";
			itemGrid194.ValueFormat = FormatType.Text;
			itemGrid194.Visible = true;
			itemGrid194.Width = 30;
			itemGrid194.X = 0;
			itemGrid194.Y = 16;
			itemGrid195.AdjustFontSize = 0f;
			itemGrid195.Alignment = StringAlignment.Near;
			itemGrid195.BackColor = Color.Black;
			itemGrid195.Changed = true;
			itemGrid195.FieldType = ItemType.Text;
			itemGrid195.FontColor = Color.Red;
			itemGrid195.FontStyle = FontStyle.Regular;
			itemGrid195.Height = 1;
			itemGrid195.IsBlink = 0;
			itemGrid195.Name = "tbForcePrevious";
			itemGrid195.Text = "";
			itemGrid195.ValueFormat = FormatType.Text;
			itemGrid195.Visible = true;
			itemGrid195.Width = 35;
			itemGrid195.X = 30;
			itemGrid195.Y = 16;
			itemGrid196.AdjustFontSize = 0f;
			itemGrid196.Alignment = StringAlignment.Near;
			itemGrid196.BackColor = Color.Black;
			itemGrid196.Changed = true;
			itemGrid196.FieldType = ItemType.Text;
			itemGrid196.FontColor = Color.Red;
			itemGrid196.FontStyle = FontStyle.Regular;
			itemGrid196.Height = 1;
			itemGrid196.IsBlink = 0;
			itemGrid196.Name = "tbForceCurrent";
			itemGrid196.Text = "";
			itemGrid196.ValueFormat = FormatType.Text;
			itemGrid196.Visible = true;
			itemGrid196.Width = 35;
			itemGrid196.X = 65;
			itemGrid196.Y = 16;
			itemGrid197.AdjustFontSize = 0f;
			itemGrid197.Alignment = StringAlignment.Near;
			itemGrid197.BackColor = Color.Black;
			itemGrid197.Changed = true;
			itemGrid197.FieldType = ItemType.Label2;
			itemGrid197.FontColor = Color.WhiteSmoke;
			itemGrid197.FontStyle = FontStyle.Regular;
			itemGrid197.Height = 1;
			itemGrid197.IsBlink = 0;
			itemGrid197.Name = "lbMarginRate";
			itemGrid197.Text = "Margin Rate";
			itemGrid197.ValueFormat = FormatType.Text;
			itemGrid197.Visible = true;
			itemGrid197.Width = 11;
			itemGrid197.X = 0;
			itemGrid197.Y = 0;
			itemGrid198.AdjustFontSize = 0f;
			itemGrid198.Alignment = StringAlignment.Near;
			itemGrid198.BackColor = Color.Black;
			itemGrid198.Changed = true;
			itemGrid198.FieldType = ItemType.Text;
			itemGrid198.FontColor = Color.Yellow;
			itemGrid198.FontStyle = FontStyle.Regular;
			itemGrid198.Height = 1;
			itemGrid198.IsBlink = 0;
			itemGrid198.Name = "tbMarginRate";
			itemGrid198.Text = "";
			itemGrid198.ValueFormat = FormatType.Volume;
			itemGrid198.Visible = true;
			itemGrid198.Width = 14;
			itemGrid198.X = 11;
			itemGrid198.Y = 0;
			itemGrid199.AdjustFontSize = 0f;
			itemGrid199.Alignment = StringAlignment.Near;
			itemGrid199.BackColor = Color.Black;
			itemGrid199.Changed = true;
			itemGrid199.FieldType = ItemType.Label2;
			itemGrid199.FontColor = Color.WhiteSmoke;
			itemGrid199.FontStyle = FontStyle.Regular;
			itemGrid199.Height = 1;
			itemGrid199.IsBlink = 0;
			itemGrid199.Name = "lbLoanLimit";
			itemGrid199.Text = "Loan Limit";
			itemGrid199.ValueFormat = FormatType.Text;
			itemGrid199.Visible = true;
			itemGrid199.Width = 11;
			itemGrid199.X = 25;
			itemGrid199.Y = 0;
			itemGrid200.AdjustFontSize = 0f;
			itemGrid200.Alignment = StringAlignment.Near;
			itemGrid200.BackColor = Color.Black;
			itemGrid200.Changed = true;
			itemGrid200.FieldType = ItemType.Text;
			itemGrid200.FontColor = Color.Yellow;
			itemGrid200.FontStyle = FontStyle.Regular;
			itemGrid200.Height = 1;
			itemGrid200.IsBlink = 0;
			itemGrid200.Name = "tbLoanLimit";
			itemGrid200.Text = "";
			itemGrid200.ValueFormat = FormatType.Volume;
			itemGrid200.Visible = true;
			itemGrid200.Width = 14;
			itemGrid200.X = 36;
			itemGrid200.Y = 0;
			this.intzaCB.Items.Add(itemGrid149);
			this.intzaCB.Items.Add(itemGrid150);
			this.intzaCB.Items.Add(itemGrid151);
			this.intzaCB.Items.Add(itemGrid152);
			this.intzaCB.Items.Add(itemGrid153);
			this.intzaCB.Items.Add(itemGrid154);
			this.intzaCB.Items.Add(itemGrid155);
			this.intzaCB.Items.Add(itemGrid156);
			this.intzaCB.Items.Add(itemGrid157);
			this.intzaCB.Items.Add(itemGrid158);
			this.intzaCB.Items.Add(itemGrid159);
			this.intzaCB.Items.Add(itemGrid160);
			this.intzaCB.Items.Add(itemGrid161);
			this.intzaCB.Items.Add(itemGrid162);
			this.intzaCB.Items.Add(itemGrid163);
			this.intzaCB.Items.Add(itemGrid164);
			this.intzaCB.Items.Add(itemGrid165);
			this.intzaCB.Items.Add(itemGrid166);
			this.intzaCB.Items.Add(itemGrid167);
			this.intzaCB.Items.Add(itemGrid168);
			this.intzaCB.Items.Add(itemGrid169);
			this.intzaCB.Items.Add(itemGrid170);
			this.intzaCB.Items.Add(itemGrid171);
			this.intzaCB.Items.Add(itemGrid172);
			this.intzaCB.Items.Add(itemGrid173);
			this.intzaCB.Items.Add(itemGrid174);
			this.intzaCB.Items.Add(itemGrid175);
			this.intzaCB.Items.Add(itemGrid176);
			this.intzaCB.Items.Add(itemGrid177);
			this.intzaCB.Items.Add(itemGrid178);
			this.intzaCB.Items.Add(itemGrid179);
			this.intzaCB.Items.Add(itemGrid180);
			this.intzaCB.Items.Add(itemGrid181);
			this.intzaCB.Items.Add(itemGrid182);
			this.intzaCB.Items.Add(itemGrid183);
			this.intzaCB.Items.Add(itemGrid184);
			this.intzaCB.Items.Add(itemGrid185);
			this.intzaCB.Items.Add(itemGrid186);
			this.intzaCB.Items.Add(itemGrid187);
			this.intzaCB.Items.Add(itemGrid188);
			this.intzaCB.Items.Add(itemGrid189);
			this.intzaCB.Items.Add(itemGrid190);
			this.intzaCB.Items.Add(itemGrid191);
			this.intzaCB.Items.Add(itemGrid192);
			this.intzaCB.Items.Add(itemGrid193);
			this.intzaCB.Items.Add(itemGrid194);
			this.intzaCB.Items.Add(itemGrid195);
			this.intzaCB.Items.Add(itemGrid196);
			this.intzaCB.Items.Add(itemGrid197);
			this.intzaCB.Items.Add(itemGrid198);
			this.intzaCB.Items.Add(itemGrid199);
			this.intzaCB.Items.Add(itemGrid200);
			this.intzaCB.LineColor = Color.Red;
			this.intzaCB.Location = new Point(3, 176);
			this.intzaCB.Margin = new Padding(1);
			this.intzaCB.Name = "intzaCB";
			this.intzaCB.Size = new Size(472, 62);
			this.intzaCB.TabIndex = 19;
			this.intzaCB.TabStop = false;
			this.intzaCB.KeyPress += new KeyPressEventHandler(this.intzaListView1_KeyPress);
			this.intzaInfoHeader.AllowDrop = true;
			this.intzaInfoHeader.BackColor = Color.Black;
			this.intzaInfoHeader.BorderStyle = BorderStyle.FixedSingle;
			this.intzaInfoHeader.CanDrag = false;
			this.intzaInfoHeader.IsAutoRepaint = true;
			this.intzaInfoHeader.IsDroped = false;
			itemGrid201.AdjustFontSize = -1f;
			itemGrid201.Alignment = StringAlignment.Near;
			itemGrid201.BackColor = Color.Black;
			itemGrid201.Changed = false;
			itemGrid201.FieldType = ItemType.Label2;
			itemGrid201.FontColor = Color.LightGray;
			itemGrid201.FontStyle = FontStyle.Regular;
			itemGrid201.Height = 1;
			itemGrid201.IsBlink = 0;
			itemGrid201.Name = "lbCustName";
			itemGrid201.Text = "Name";
			itemGrid201.ValueFormat = FormatType.Text;
			itemGrid201.Visible = true;
			itemGrid201.Width = 11;
			itemGrid201.X = 0;
			itemGrid201.Y = 0;
			itemGrid202.AdjustFontSize = 0f;
			itemGrid202.Alignment = StringAlignment.Near;
			itemGrid202.BackColor = Color.Black;
			itemGrid202.Changed = false;
			itemGrid202.FieldType = ItemType.Text;
			itemGrid202.FontColor = Color.Yellow;
			itemGrid202.FontStyle = FontStyle.Regular;
			itemGrid202.Height = 1;
			itemGrid202.IsBlink = 0;
			itemGrid202.Name = "tbCustName";
			itemGrid202.Text = "";
			itemGrid202.ValueFormat = FormatType.Text;
			itemGrid202.Visible = true;
			itemGrid202.Width = 25;
			itemGrid202.X = 11;
			itemGrid202.Y = 0;
			itemGrid203.AdjustFontSize = -1f;
			itemGrid203.Alignment = StringAlignment.Near;
			itemGrid203.BackColor = Color.Black;
			itemGrid203.Changed = false;
			itemGrid203.FieldType = ItemType.Label2;
			itemGrid203.FontColor = Color.LightGray;
			itemGrid203.FontStyle = FontStyle.Regular;
			itemGrid203.Height = 1;
			itemGrid203.IsBlink = 0;
			itemGrid203.Name = "lbTrader";
			itemGrid203.Text = "Trader";
			itemGrid203.ValueFormat = FormatType.Text;
			itemGrid203.Visible = true;
			itemGrid203.Width = 7;
			itemGrid203.X = 36;
			itemGrid203.Y = 0;
			itemGrid204.AdjustFontSize = 0f;
			itemGrid204.Alignment = StringAlignment.Near;
			itemGrid204.BackColor = Color.Black;
			itemGrid204.Changed = false;
			itemGrid204.FieldType = ItemType.Text;
			itemGrid204.FontColor = Color.Yellow;
			itemGrid204.FontStyle = FontStyle.Regular;
			itemGrid204.Height = 1;
			itemGrid204.IsBlink = 0;
			itemGrid204.Name = "tbTrader";
			itemGrid204.Text = "";
			itemGrid204.ValueFormat = FormatType.Text;
			itemGrid204.Visible = true;
			itemGrid204.Width = 30;
			itemGrid204.X = 43;
			itemGrid204.Y = 0;
			itemGrid205.AdjustFontSize = -1f;
			itemGrid205.Alignment = StringAlignment.Near;
			itemGrid205.BackColor = Color.Black;
			itemGrid205.Changed = false;
			itemGrid205.FieldType = ItemType.Label2;
			itemGrid205.FontColor = Color.LightGray;
			itemGrid205.FontStyle = FontStyle.Regular;
			itemGrid205.Height = 1;
			itemGrid205.IsBlink = 0;
			itemGrid205.Name = "lbCustomerFlag";
			itemGrid205.Text = "Cust Flag";
			itemGrid205.ValueFormat = FormatType.Text;
			itemGrid205.Visible = true;
			itemGrid205.Width = 13;
			itemGrid205.X = 75;
			itemGrid205.Y = 0;
			itemGrid206.AdjustFontSize = 0f;
			itemGrid206.Alignment = StringAlignment.Near;
			itemGrid206.BackColor = Color.Black;
			itemGrid206.Changed = false;
			itemGrid206.FieldType = ItemType.Text;
			itemGrid206.FontColor = Color.Yellow;
			itemGrid206.FontStyle = FontStyle.Regular;
			itemGrid206.Height = 1;
			itemGrid206.IsBlink = 0;
			itemGrid206.Name = "tbCustomerFlag";
			itemGrid206.Text = "";
			itemGrid206.ValueFormat = FormatType.Text;
			itemGrid206.Visible = true;
			itemGrid206.Width = 12;
			itemGrid206.X = 88;
			itemGrid206.Y = 0;
			itemGrid207.AdjustFontSize = -1f;
			itemGrid207.Alignment = StringAlignment.Near;
			itemGrid207.BackColor = Color.Black;
			itemGrid207.Changed = false;
			itemGrid207.FieldType = ItemType.Label2;
			itemGrid207.FontColor = Color.LightGray;
			itemGrid207.FontStyle = FontStyle.Regular;
			itemGrid207.Height = 1;
			itemGrid207.IsBlink = 0;
			itemGrid207.Name = "lbBuyLimit";
			itemGrid207.Text = "Buy Limit";
			itemGrid207.ValueFormat = FormatType.Text;
			itemGrid207.Visible = true;
			itemGrid207.Width = 11;
			itemGrid207.X = 0;
			itemGrid207.Y = 2;
			itemGrid208.AdjustFontSize = 0f;
			itemGrid208.Alignment = StringAlignment.Near;
			itemGrid208.BackColor = Color.Black;
			itemGrid208.Changed = false;
			itemGrid208.FieldType = ItemType.Text;
			itemGrid208.FontColor = Color.Yellow;
			itemGrid208.FontStyle = FontStyle.Regular;
			itemGrid208.Height = 1;
			itemGrid208.IsBlink = 0;
			itemGrid208.Name = "tbBuyLimit";
			itemGrid208.Text = "";
			itemGrid208.ValueFormat = FormatType.Text;
			itemGrid208.Visible = true;
			itemGrid208.Width = 14;
			itemGrid208.X = 11;
			itemGrid208.Y = 2;
			itemGrid209.AdjustFontSize = -1f;
			itemGrid209.Alignment = StringAlignment.Near;
			itemGrid209.BackColor = Color.Black;
			itemGrid209.Changed = false;
			itemGrid209.FieldType = ItemType.Label2;
			itemGrid209.FontColor = Color.LightGray;
			itemGrid209.FontStyle = FontStyle.Regular;
			itemGrid209.Height = 1;
			itemGrid209.IsBlink = 0;
			itemGrid209.Name = "lbCustomerType";
			itemGrid209.Text = "Cust Type";
			itemGrid209.ValueFormat = FormatType.Text;
			itemGrid209.Visible = true;
			itemGrid209.Width = 11;
			itemGrid209.X = 0;
			itemGrid209.Y = 1;
			itemGrid210.AdjustFontSize = 0f;
			itemGrid210.Alignment = StringAlignment.Near;
			itemGrid210.BackColor = Color.Black;
			itemGrid210.Changed = false;
			itemGrid210.FieldType = ItemType.Text;
			itemGrid210.FontColor = Color.Yellow;
			itemGrid210.FontStyle = FontStyle.Regular;
			itemGrid210.Height = 1;
			itemGrid210.IsBlink = 0;
			itemGrid210.Name = "tbCustomerType";
			itemGrid210.Text = "";
			itemGrid210.ValueFormat = FormatType.Text;
			itemGrid210.Visible = true;
			itemGrid210.Width = 14;
			itemGrid210.X = 11;
			itemGrid210.Y = 1;
			itemGrid211.AdjustFontSize = -1f;
			itemGrid211.Alignment = StringAlignment.Near;
			itemGrid211.BackColor = Color.Black;
			itemGrid211.Changed = false;
			itemGrid211.FieldType = ItemType.Label2;
			itemGrid211.FontColor = Color.LightGray;
			itemGrid211.FontStyle = FontStyle.Regular;
			itemGrid211.Height = 1;
			itemGrid211.IsBlink = 0;
			itemGrid211.Name = "lbAccountType";
			itemGrid211.Text = "Acc Type";
			itemGrid211.ValueFormat = FormatType.Text;
			itemGrid211.Visible = true;
			itemGrid211.Width = 14;
			itemGrid211.X = 25;
			itemGrid211.Y = 1;
			itemGrid212.AdjustFontSize = -1f;
			itemGrid212.Alignment = StringAlignment.Near;
			itemGrid212.BackColor = Color.Black;
			itemGrid212.Changed = false;
			itemGrid212.FieldType = ItemType.Text;
			itemGrid212.FontColor = Color.Yellow;
			itemGrid212.FontStyle = FontStyle.Regular;
			itemGrid212.Height = 1;
			itemGrid212.IsBlink = 0;
			itemGrid212.Name = "tbAccountType";
			itemGrid212.Text = "";
			itemGrid212.ValueFormat = FormatType.Text;
			itemGrid212.Visible = true;
			itemGrid212.Width = 14;
			itemGrid212.X = 39;
			itemGrid212.Y = 1;
			itemGrid213.AdjustFontSize = -1f;
			itemGrid213.Alignment = StringAlignment.Near;
			itemGrid213.BackColor = Color.Black;
			itemGrid213.Changed = false;
			itemGrid213.FieldType = ItemType.Label2;
			itemGrid213.FontColor = Color.LightGray;
			itemGrid213.FontStyle = FontStyle.Regular;
			itemGrid213.Height = 1;
			itemGrid213.IsBlink = 0;
			itemGrid213.Name = "lbCreditType";
			itemGrid213.Text = "Credit Type";
			itemGrid213.ValueFormat = FormatType.Text;
			itemGrid213.Visible = true;
			itemGrid213.Width = 10;
			itemGrid213.X = 53;
			itemGrid213.Y = 1;
			itemGrid214.AdjustFontSize = -1f;
			itemGrid214.Alignment = StringAlignment.Near;
			itemGrid214.BackColor = Color.Black;
			itemGrid214.Changed = false;
			itemGrid214.FieldType = ItemType.Text;
			itemGrid214.FontColor = Color.Yellow;
			itemGrid214.FontStyle = FontStyle.Regular;
			itemGrid214.Height = 1;
			itemGrid214.IsBlink = 0;
			itemGrid214.Name = "tbCreditType";
			itemGrid214.Text = "";
			itemGrid214.ValueFormat = FormatType.Text;
			itemGrid214.Visible = true;
			itemGrid214.Width = 12;
			itemGrid214.X = 63;
			itemGrid214.Y = 1;
			itemGrid215.AdjustFontSize = -1f;
			itemGrid215.Alignment = StringAlignment.Near;
			itemGrid215.BackColor = Color.Black;
			itemGrid215.Changed = false;
			itemGrid215.FieldType = ItemType.Label2;
			itemGrid215.FontColor = Color.LightGray;
			itemGrid215.FontStyle = FontStyle.Regular;
			itemGrid215.Height = 1;
			itemGrid215.IsBlink = 0;
			itemGrid215.Name = "lbCantOverCredit";
			itemGrid215.Text = "Can't Over Credit";
			itemGrid215.ValueFormat = FormatType.Text;
			itemGrid215.Visible = true;
			itemGrid215.Width = 13;
			itemGrid215.X = 75;
			itemGrid215.Y = 1;
			itemGrid216.AdjustFontSize = 0f;
			itemGrid216.Alignment = StringAlignment.Near;
			itemGrid216.BackColor = Color.Black;
			itemGrid216.Changed = false;
			itemGrid216.FieldType = ItemType.Text;
			itemGrid216.FontColor = Color.Yellow;
			itemGrid216.FontStyle = FontStyle.Regular;
			itemGrid216.Height = 1;
			itemGrid216.IsBlink = 0;
			itemGrid216.Name = "tbCantOverCredit";
			itemGrid216.Text = "";
			itemGrid216.ValueFormat = FormatType.Text;
			itemGrid216.Visible = true;
			itemGrid216.Width = 12;
			itemGrid216.X = 88;
			itemGrid216.Y = 1;
			itemGrid217.AdjustFontSize = -1f;
			itemGrid217.Alignment = StringAlignment.Near;
			itemGrid217.BackColor = Color.Black;
			itemGrid217.Changed = false;
			itemGrid217.FieldType = ItemType.Label2;
			itemGrid217.FontColor = Color.LightGray;
			itemGrid217.FontStyle = FontStyle.Regular;
			itemGrid217.Height = 1;
			itemGrid217.IsBlink = 0;
			itemGrid217.Name = "lbHighLimit";
			itemGrid217.Text = "High Limit";
			itemGrid217.ValueFormat = FormatType.Text;
			itemGrid217.Visible = true;
			itemGrid217.Width = 14;
			itemGrid217.X = 25;
			itemGrid217.Y = 2;
			itemGrid218.AdjustFontSize = 0f;
			itemGrid218.Alignment = StringAlignment.Near;
			itemGrid218.BackColor = Color.Black;
			itemGrid218.Changed = false;
			itemGrid218.FieldType = ItemType.Text;
			itemGrid218.FontColor = Color.Yellow;
			itemGrid218.FontStyle = FontStyle.Regular;
			itemGrid218.Height = 1;
			itemGrid218.IsBlink = 0;
			itemGrid218.Name = "tbHighLimit";
			itemGrid218.Text = "";
			itemGrid218.ValueFormat = FormatType.Text;
			itemGrid218.Visible = true;
			itemGrid218.Width = 14;
			itemGrid218.X = 39;
			itemGrid218.Y = 2;
			itemGrid219.AdjustFontSize = -1f;
			itemGrid219.Alignment = StringAlignment.Near;
			itemGrid219.BackColor = Color.Black;
			itemGrid219.Changed = false;
			itemGrid219.FieldType = ItemType.Label2;
			itemGrid219.FontColor = Color.LightGray;
			itemGrid219.FontStyle = FontStyle.Regular;
			itemGrid219.Height = 1;
			itemGrid219.IsBlink = 0;
			itemGrid219.Name = "lbCreditLine";
			itemGrid219.Text = "Credit Line";
			itemGrid219.ValueFormat = FormatType.Text;
			itemGrid219.Visible = true;
			itemGrid219.Width = 10;
			itemGrid219.X = 53;
			itemGrid219.Y = 2;
			itemGrid220.AdjustFontSize = 0f;
			itemGrid220.Alignment = StringAlignment.Near;
			itemGrid220.BackColor = Color.Black;
			itemGrid220.Changed = false;
			itemGrid220.FieldType = ItemType.Text;
			itemGrid220.FontColor = Color.Yellow;
			itemGrid220.FontStyle = FontStyle.Regular;
			itemGrid220.Height = 1;
			itemGrid220.IsBlink = 0;
			itemGrid220.Name = "tbCreditLine";
			itemGrid220.Text = "";
			itemGrid220.ValueFormat = FormatType.Text;
			itemGrid220.Visible = true;
			itemGrid220.Width = 12;
			itemGrid220.X = 63;
			itemGrid220.Y = 2;
			itemGrid221.AdjustFontSize = -1f;
			itemGrid221.Alignment = StringAlignment.Near;
			itemGrid221.BackColor = Color.Black;
			itemGrid221.Changed = false;
			itemGrid221.FieldType = ItemType.Label2;
			itemGrid221.FontColor = Color.LightGray;
			itemGrid221.FontStyle = FontStyle.Regular;
			itemGrid221.Height = 1;
			itemGrid221.IsBlink = 0;
			itemGrid221.Name = "lbEquity";
			itemGrid221.Text = "Equity";
			itemGrid221.ValueFormat = FormatType.Text;
			itemGrid221.Visible = true;
			itemGrid221.Width = 13;
			itemGrid221.X = 75;
			itemGrid221.Y = 2;
			itemGrid222.AdjustFontSize = 0f;
			itemGrid222.Alignment = StringAlignment.Near;
			itemGrid222.BackColor = Color.Black;
			itemGrid222.Changed = false;
			itemGrid222.FieldType = ItemType.Text;
			itemGrid222.FontColor = Color.Yellow;
			itemGrid222.FontStyle = FontStyle.Regular;
			itemGrid222.Height = 1;
			itemGrid222.IsBlink = 0;
			itemGrid222.Name = "tbEquity";
			itemGrid222.Text = "";
			itemGrid222.ValueFormat = FormatType.Text;
			itemGrid222.Visible = true;
			itemGrid222.Width = 12;
			itemGrid222.X = 88;
			itemGrid222.Y = 2;
			this.intzaInfoHeader.Items.Add(itemGrid201);
			this.intzaInfoHeader.Items.Add(itemGrid202);
			this.intzaInfoHeader.Items.Add(itemGrid203);
			this.intzaInfoHeader.Items.Add(itemGrid204);
			this.intzaInfoHeader.Items.Add(itemGrid205);
			this.intzaInfoHeader.Items.Add(itemGrid206);
			this.intzaInfoHeader.Items.Add(itemGrid207);
			this.intzaInfoHeader.Items.Add(itemGrid208);
			this.intzaInfoHeader.Items.Add(itemGrid209);
			this.intzaInfoHeader.Items.Add(itemGrid210);
			this.intzaInfoHeader.Items.Add(itemGrid211);
			this.intzaInfoHeader.Items.Add(itemGrid212);
			this.intzaInfoHeader.Items.Add(itemGrid213);
			this.intzaInfoHeader.Items.Add(itemGrid214);
			this.intzaInfoHeader.Items.Add(itemGrid215);
			this.intzaInfoHeader.Items.Add(itemGrid216);
			this.intzaInfoHeader.Items.Add(itemGrid217);
			this.intzaInfoHeader.Items.Add(itemGrid218);
			this.intzaInfoHeader.Items.Add(itemGrid219);
			this.intzaInfoHeader.Items.Add(itemGrid220);
			this.intzaInfoHeader.Items.Add(itemGrid221);
			this.intzaInfoHeader.Items.Add(itemGrid222);
			this.intzaInfoHeader.LineColor = Color.Red;
			this.intzaInfoHeader.Location = new Point(0, 29);
			this.intzaInfoHeader.Margin = new Padding(1);
			this.intzaInfoHeader.Name = "intzaInfoHeader";
			this.intzaInfoHeader.Size = new Size(513, 58);
			this.intzaInfoHeader.TabIndex = 70;
			this.intzaInfoHeader.TabStop = false;
			this.intzaSumReport.AllowDrop = true;
			this.intzaSumReport.BackColor = Color.FromArgb(25, 25, 25);
			this.intzaSumReport.CanBlink = true;
			this.intzaSumReport.CanDrag = false;
			this.intzaSumReport.CanGetMouseMove = false;
			columnItem36.Alignment = StringAlignment.Far;
			columnItem36.BackColor = Color.FromArgb(64, 64, 64);
			columnItem36.ColumnAlignment = StringAlignment.Center;
			columnItem36.FontColor = Color.White;
			columnItem36.MyStyle = FontStyle.Bold;
			columnItem36.Name = "last";
			columnItem36.Text = "Last";
			columnItem36.ValueFormat = FormatType.Text;
			columnItem36.Visible = true;
			columnItem36.Width = 53;
			columnItem37.Alignment = StringAlignment.Far;
			columnItem37.BackColor = Color.FromArgb(64, 64, 64);
			columnItem37.ColumnAlignment = StringAlignment.Center;
			columnItem37.FontColor = Color.LightGray;
			columnItem37.MyStyle = FontStyle.Regular;
			columnItem37.Name = "cost";
			columnItem37.Text = "Cost";
			columnItem37.ValueFormat = FormatType.Text;
			columnItem37.Visible = true;
			columnItem37.Width = 10;
			columnItem38.Alignment = StringAlignment.Far;
			columnItem38.BackColor = Color.FromArgb(64, 64, 64);
			columnItem38.ColumnAlignment = StringAlignment.Center;
			columnItem38.FontColor = Color.LightGray;
			columnItem38.MyStyle = FontStyle.Regular;
			columnItem38.Name = "curr_value";
			columnItem38.Text = "Curr Val";
			columnItem38.ValueFormat = FormatType.Text;
			columnItem38.Visible = true;
			columnItem38.Width = 10;
			columnItem39.Alignment = StringAlignment.Far;
			columnItem39.BackColor = Color.FromArgb(64, 64, 64);
			columnItem39.ColumnAlignment = StringAlignment.Center;
			columnItem39.FontColor = Color.LightGray;
			columnItem39.MyStyle = FontStyle.Bold;
			columnItem39.Name = "unreal_pct";
			columnItem39.Text = "%Unrl";
			columnItem39.ValueFormat = FormatType.Text;
			columnItem39.Visible = true;
			columnItem39.Width = 7;
			columnItem40.Alignment = StringAlignment.Far;
			columnItem40.BackColor = Color.FromArgb(64, 64, 64);
			columnItem40.ColumnAlignment = StringAlignment.Center;
			columnItem40.FontColor = Color.LightGray;
			columnItem40.MyStyle = FontStyle.Regular;
			columnItem40.Name = "unreal";
			columnItem40.Text = "Unrl P/L";
			columnItem40.ValueFormat = FormatType.Text;
			columnItem40.Visible = true;
			columnItem40.Width = 10;
			columnItem41.Alignment = StringAlignment.Far;
			columnItem41.BackColor = Color.FromArgb(64, 64, 64);
			columnItem41.ColumnAlignment = StringAlignment.Center;
			columnItem41.FontColor = Color.LightGray;
			columnItem41.MyStyle = FontStyle.Regular;
			columnItem41.Name = "realize";
			columnItem41.Text = "Real P/L";
			columnItem41.ValueFormat = FormatType.Text;
			columnItem41.Visible = true;
			columnItem41.Width = 10;
			this.intzaSumReport.Columns.Add(columnItem36);
			this.intzaSumReport.Columns.Add(columnItem37);
			this.intzaSumReport.Columns.Add(columnItem38);
			this.intzaSumReport.Columns.Add(columnItem39);
			this.intzaSumReport.Columns.Add(columnItem40);
			this.intzaSumReport.Columns.Add(columnItem41);
			this.intzaSumReport.CurrentScroll = 0;
			this.intzaSumReport.FocusItemIndex = -1;
			this.intzaSumReport.ForeColor = Color.Black;
			this.intzaSumReport.GridColor = Color.FromArgb(45, 45, 45);
			this.intzaSumReport.HeaderPctHeight = 100f;
			this.intzaSumReport.IsAutoRepaint = true;
			this.intzaSumReport.IsDrawFullRow = false;
			this.intzaSumReport.IsDrawGrid = true;
			this.intzaSumReport.IsDrawHeader = false;
			this.intzaSumReport.IsScrollable = true;
			this.intzaSumReport.Location = new Point(3, 302);
			this.intzaSumReport.MainColumn = "";
			this.intzaSumReport.Name = "intzaSumReport";
			this.intzaSumReport.Rows = 1;
			this.intzaSumReport.RowSelectColor = Color.FromArgb(95, 158, 160);
			this.intzaSumReport.RowSelectType = 0;
			this.intzaSumReport.RowsVisible = 1;
			this.intzaSumReport.ScrollChennelColor = Color.Gray;
			this.intzaSumReport.Size = new Size(685, 25);
			this.intzaSumReport.SortColumnName = "";
			this.intzaSumReport.SortType = SortType.Desc;
			this.intzaSumReport.TabIndex = 69;
			this.intzaReport.AllowDrop = true;
			this.intzaReport.BackColor = Color.FromArgb(10, 10, 10);
			this.intzaReport.CanBlink = true;
			this.intzaReport.CanDrag = false;
			this.intzaReport.CanGetMouseMove = false;
			columnItem42.Alignment = StringAlignment.Near;
			columnItem42.BackColor = Color.FromArgb(64, 64, 64);
			columnItem42.ColumnAlignment = StringAlignment.Center;
			columnItem42.FontColor = Color.LightGray;
			columnItem42.MyStyle = FontStyle.Regular;
			columnItem42.Name = "stock";
			columnItem42.Text = "Stock";
			columnItem42.ValueFormat = FormatType.Text;
			columnItem42.Visible = true;
			columnItem42.Width = 11;
			columnItem43.Alignment = StringAlignment.Center;
			columnItem43.BackColor = Color.FromArgb(64, 64, 64);
			columnItem43.ColumnAlignment = StringAlignment.Center;
			columnItem43.FontColor = Color.LightGray;
			columnItem43.MyStyle = FontStyle.Regular;
			columnItem43.Name = "ttf";
			columnItem43.Text = "TTF";
			columnItem43.ValueFormat = FormatType.Text;
			columnItem43.Visible = true;
			columnItem43.Width = 4;
			columnItem44.Alignment = StringAlignment.Far;
			columnItem44.BackColor = Color.FromArgb(64, 64, 64);
			columnItem44.ColumnAlignment = StringAlignment.Center;
			columnItem44.FontColor = Color.LightGray;
			columnItem44.MyStyle = FontStyle.Regular;
			columnItem44.Name = "onhand";
			columnItem44.Text = "OnHand";
			columnItem44.ValueFormat = FormatType.Text;
			columnItem44.Visible = true;
			columnItem44.Width = 10;
			columnItem45.Alignment = StringAlignment.Far;
			columnItem45.BackColor = Color.FromArgb(64, 64, 64);
			columnItem45.ColumnAlignment = StringAlignment.Center;
			columnItem45.FontColor = Color.LightGray;
			columnItem45.MyStyle = FontStyle.Regular;
			columnItem45.Name = "sellable";
			columnItem45.Text = "Sellable";
			columnItem45.ValueFormat = FormatType.Text;
			columnItem45.Visible = true;
			columnItem45.Width = 10;
			columnItem46.Alignment = StringAlignment.Far;
			columnItem46.BackColor = Color.FromArgb(64, 64, 64);
			columnItem46.ColumnAlignment = StringAlignment.Center;
			columnItem46.FontColor = Color.LightGray;
			columnItem46.MyStyle = FontStyle.Regular;
			columnItem46.Name = "avg";
			columnItem46.Text = "Avg";
			columnItem46.ValueFormat = FormatType.Text;
			columnItem46.Visible = true;
			columnItem46.Width = 7;
			columnItem47.Alignment = StringAlignment.Far;
			columnItem47.BackColor = Color.FromArgb(64, 64, 64);
			columnItem47.ColumnAlignment = StringAlignment.Center;
			columnItem47.FontColor = Color.LightGray;
			columnItem47.MyStyle = FontStyle.Regular;
			columnItem47.Name = "last";
			columnItem47.Text = "Last";
			columnItem47.ValueFormat = FormatType.Text;
			columnItem47.Visible = true;
			columnItem47.Width = 7;
			columnItem48.Alignment = StringAlignment.Far;
			columnItem48.BackColor = Color.FromArgb(64, 64, 64);
			columnItem48.ColumnAlignment = StringAlignment.Center;
			columnItem48.FontColor = Color.LightGray;
			columnItem48.MyStyle = FontStyle.Regular;
			columnItem48.Name = "cost";
			columnItem48.Text = "Cost";
			columnItem48.ValueFormat = FormatType.Text;
			columnItem48.Visible = true;
			columnItem48.Width = 10;
			columnItem49.Alignment = StringAlignment.Far;
			columnItem49.BackColor = Color.FromArgb(64, 64, 64);
			columnItem49.ColumnAlignment = StringAlignment.Center;
			columnItem49.FontColor = Color.LightGray;
			columnItem49.MyStyle = FontStyle.Regular;
			columnItem49.Name = "curr_value";
			columnItem49.Text = "Curr Val";
			columnItem49.ValueFormat = FormatType.Text;
			columnItem49.Visible = true;
			columnItem49.Width = 10;
			columnItem50.Alignment = StringAlignment.Far;
			columnItem50.BackColor = Color.FromArgb(64, 64, 64);
			columnItem50.ColumnAlignment = StringAlignment.Center;
			columnItem50.FontColor = Color.LightGray;
			columnItem50.MyStyle = FontStyle.Regular;
			columnItem50.Name = "unreal_pct";
			columnItem50.Text = "%Unrl";
			columnItem50.ValueFormat = FormatType.Text;
			columnItem50.Visible = true;
			columnItem50.Width = 7;
			columnItem51.Alignment = StringAlignment.Far;
			columnItem51.BackColor = Color.FromArgb(64, 64, 64);
			columnItem51.ColumnAlignment = StringAlignment.Center;
			columnItem51.FontColor = Color.LightGray;
			columnItem51.MyStyle = FontStyle.Regular;
			columnItem51.Name = "unreal";
			columnItem51.Text = "Unrl P/L";
			columnItem51.ValueFormat = FormatType.Text;
			columnItem51.Visible = true;
			columnItem51.Width = 10;
			columnItem52.Alignment = StringAlignment.Far;
			columnItem52.BackColor = Color.FromArgb(64, 64, 64);
			columnItem52.ColumnAlignment = StringAlignment.Center;
			columnItem52.FontColor = Color.LightGray;
			columnItem52.MyStyle = FontStyle.Regular;
			columnItem52.Name = "realize";
			columnItem52.Text = "Real P/L";
			columnItem52.ValueFormat = FormatType.Text;
			columnItem52.Visible = true;
			columnItem52.Width = 10;
			this.intzaReport.Columns.Add(columnItem42);
			this.intzaReport.Columns.Add(columnItem43);
			this.intzaReport.Columns.Add(columnItem44);
			this.intzaReport.Columns.Add(columnItem45);
			this.intzaReport.Columns.Add(columnItem46);
			this.intzaReport.Columns.Add(columnItem47);
			this.intzaReport.Columns.Add(columnItem48);
			this.intzaReport.Columns.Add(columnItem49);
			this.intzaReport.Columns.Add(columnItem50);
			this.intzaReport.Columns.Add(columnItem51);
			this.intzaReport.Columns.Add(columnItem52);
			this.intzaReport.CurrentScroll = 0;
			this.intzaReport.FocusItemIndex = -1;
			this.intzaReport.ForeColor = Color.Black;
			this.intzaReport.GridColor = Color.FromArgb(45, 45, 45);
			this.intzaReport.HeaderPctHeight = 80f;
			this.intzaReport.IsAutoRepaint = true;
			this.intzaReport.IsDrawFullRow = false;
			this.intzaReport.IsDrawGrid = true;
			this.intzaReport.IsDrawHeader = true;
			this.intzaReport.IsScrollable = true;
			this.intzaReport.Location = new Point(0, 244);
			this.intzaReport.MainColumn = "";
			this.intzaReport.Name = "intzaReport";
			this.intzaReport.Rows = 0;
			this.intzaReport.RowSelectColor = Color.FromArgb(95, 158, 160);
			this.intzaReport.RowSelectType = 1;
			this.intzaReport.RowsVisible = 0;
			this.intzaReport.ScrollChennelColor = Color.Gray;
			this.intzaReport.Size = new Size(685, 52);
			this.intzaReport.SortColumnName = "";
			this.intzaReport.SortType = SortType.Desc;
			this.intzaReport.TabIndex = 68;
			this.chart.AreaPercent = "3;1";
			this.chart.BackColor = Color.FromArgb(30, 30, 30);
			this.chart.CausesValidation = false;
			this.chart.ChartDragMode = ChartDragMode.Axis;
			this.chart.CrossCursorMouseMode = MouseAction.MouseDown;
			this.chart.DefaultFormulas = null;
			this.chart.Designing = false;
			this.chart.Dock = DockStyle.Fill;
			this.chart.EndTime = new DateTime(0L);
			this.chart.FavoriteFormulas = "";
			exchangeIntraday.NativeCycle = true;
			exchangeIntraday.ShowFirstXLabel = true;
			exchangeIntraday.TimePeriods = new TimePeriod[0];
			exchangeIntraday.TimeZone = 7.0;
			this.chart.IntradayInfo = exchangeIntraday;
			this.chart.LatestValueType = LatestValueType.None;
			this.chart.Location = new Point(0, 25);
			this.chart.MaxColumnWidth = 30.0;
			this.chart.MaxPrice = 0.0;
			this.chart.MinPrice = 0.0;
			this.chart.Name = "chart";
			this.chart.NeedDrawCursor = false;
			this.chart.PriceLabelFormat = null;
			this.chart.ScaleType = ScaleType.Default;
			this.chart.SelectFormulaMouseMode = MouseAction.MouseDown;
			this.chart.ShowCursorLabel = false;
			this.chart.ShowHorizontalGrid = ShowLineMode.Default;
			this.chart.ShowStatistic = false;
			this.chart.ShowVerticalGrid = ShowLineMode.Default;
			this.chart.Size = new Size(356, 183);
			this.chart.Skin = "GreenRed";
			this.chart.StartTime = new DateTime(0L);
			this.chart.StickRenderType = StickRenderType.Default;
			this.chart.StockBars = 50;
			this.chart.StockRenderType = StockRenderType.Default;
			this.chart.Symbol = "";
			this.chart.TabIndex = 65;
			this.chart.TabStop = false;
			this.chart.ValueTextMode = ValueTextMode.Default;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.FromArgb(64, 64, 64);
			base.ClientSize = new Size(879, 589);
			base.Controls.Add(this.panelTFEXPort);
			base.Controls.Add(this.panelSET);
			base.Name = "frmPortfolio";
			this.Text = "Portfolio";
			base.IDoShownDelay += new ClientBaseForm.OnShownDelayEventHandler(this.frmPortfolio_IDoShownDelay);
			base.IDoLoadData += new ClientBaseForm.OnIDoLoadDataEventHandler(this.frmPortfolio_IDoLoadData);
			base.IDoFontChanged += new ClientBaseForm.OnFontChangedEventHandler(this.frmPortfolio_IDoFontChanged);
			base.IDoCustomSizeChanged += new ClientBaseForm.CustomSizeChangedEventHandler(this.frmPortfolio_IDoCustomSizeChanged);
			base.IDoSymbolLinked += new ClientBaseForm.OnSymbolLinkEventHandler(this.frmPortfolio_IDoSymbolLinked);
			base.IDoMainFormKeyUp += new ClientBaseForm.OnFormKeyUpEventHandler(this.frmPortfolio_IDoMainFormKeyUp);
			base.IDoReActivated += new ClientBaseForm.OnReActiveEventHandler(this.frmPortfolio_IDoReActivated);
			base.Controls.SetChildIndex(this.panelSET, 0);
			base.Controls.SetChildIndex(this.panelTFEXPort, 0);
			this.tStripMain.ResumeLayout(false);
			this.tStripMain.PerformLayout();
			this.panelNav.ResumeLayout(false);
			this.panelNav.PerformLayout();
			this.tStripMenu.ResumeLayout(false);
			this.tStripMenu.PerformLayout();
			this.panelSET.ResumeLayout(false);
			this.panelSET.PerformLayout();
			this.panelReportMenu.ResumeLayout(false);
			this.panelReportMenu.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.panelTFEXPort.ResumeLayout(false);
			this.panelTFEXPort.PerformLayout();
			this.pnlTradeJ.ResumeLayout(false);
			this.pnlTradeJ.PerformLayout();
			this.tStripTJ.ResumeLayout(false);
			this.tStripTJ.PerformLayout();
			this.tStripMainT.ResumeLayout(false);
			this.tStripMainT.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
