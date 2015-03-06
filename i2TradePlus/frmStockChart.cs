using i2TradePlus.Information;
using ITSNet.CefBrowser.WindowsForms;
using ITSNet.Common.BIZ;
using ITSNet.Common.BIZ.RealtimeMessage;
using STIControl.i2Chart.Finance;
using STIControl.i2Chart.Finance.DataProvider;
using STIControl.i2Chart.Object;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace i2TradePlus
{
	public class frmStockChart : ClientBaseForm, IRealtimeMessage
	{
		private delegate void SetNewStockInfoCallBack(string stockSymbol);
		private IContainer components = null;
		private ToolStripLabel tsStockLable;
		private ToolStripComboBox tstbStock;
		private ToolStrip tStripMenu;
		private ToolStripButton tsbtnEditIndicator;
		private ToolStripButton tstbAddIndicator;
		private ToolStripComboBox tscbCycle;
		private ToolStripButton tsDelIndicator;
		private ToolStripLabel toolStripLabel1;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripSeparator toolStripSeparator2;
		private ToolStripLabel toolStripLabel2;
		private ChartWinControl chart;
		private ToolStripLabel tslbName;
		private Panel panel1;
		private CefWebBrowser cefWebBrowser1;
		private Label lbError;
		private BackgroundWorker bgwReloadData = null;
		private DataSet tds = null;
		private string selectDate = string.Empty;
		private StockList.StockInformation _stockInfo = null;
		private string _cfCurrentStock = string.Empty;
		private bool _firstLoad = true;
		private decimal _openPrice;
		private bool _isChartFocus = false;
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
			ExchangeIntraday exchangeIntraday = new ExchangeIntraday();
			this.tsStockLable = new ToolStripLabel();
			this.tstbStock = new ToolStripComboBox();
			this.tStripMenu = new ToolStrip();
			this.tslbName = new ToolStripLabel();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.toolStripLabel2 = new ToolStripLabel();
			this.tstbAddIndicator = new ToolStripButton();
			this.tsbtnEditIndicator = new ToolStripButton();
			this.tsDelIndicator = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.toolStripLabel1 = new ToolStripLabel();
			this.tscbCycle = new ToolStripComboBox();
			this.panel1 = new Panel();
			this.chart = new ChartWinControl();
			this.cefWebBrowser1 = new CefWebBrowser();
			this.lbError = new Label();
			this.tStripMenu.SuspendLayout();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.tsStockLable.BackColor = Color.Black;
			this.tsStockLable.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsStockLable.Font = new Font("Microsoft Sans Serif", 8.25f);
			this.tsStockLable.ForeColor = Color.Gainsboro;
			this.tsStockLable.ImageTransparentColor = Color.Magenta;
			this.tsStockLable.Name = "tsStockLable";
			this.tsStockLable.Padding = new Padding(1, 0, 2, 0);
			this.tsStockLable.Size = new Size(38, 21);
			this.tsStockLable.Text = "Stock";
			this.tstbStock.BackColor = Color.FromArgb(45, 45, 45);
			this.tstbStock.Font = new Font("Microsoft Sans Serif", 8.25f);
			this.tstbStock.ForeColor = Color.Yellow;
			this.tstbStock.Margin = new Padding(1, 1, 1, 2);
			this.tstbStock.Name = "tstbStock";
			this.tstbStock.Size = new Size(130, 21);
			this.tstbStock.SelectedIndexChanged += new EventHandler(this.tstbStock_SelectedIndexChanged);
			this.tstbStock.KeyUp += new KeyEventHandler(this.tstbStock_KeyUp);
			this.tstbStock.KeyDown += new KeyEventHandler(this.tstbStock_KeyDown);
			this.tstbStock.KeyPress += new KeyPressEventHandler(this.tstbStock_KeyPress);
			this.tStripMenu.BackColor = Color.FromArgb(20, 20, 20);
			this.tStripMenu.BackgroundImageLayout = ImageLayout.None;
			this.tStripMenu.GripMargin = new Padding(0);
			this.tStripMenu.GripStyle = ToolStripGripStyle.Hidden;
			this.tStripMenu.Items.AddRange(new ToolStripItem[]
			{
				this.tsStockLable,
				this.tstbStock,
				this.tslbName,
				this.toolStripSeparator2,
				this.toolStripLabel2,
				this.tstbAddIndicator,
				this.tsbtnEditIndicator,
				this.tsDelIndicator,
				this.toolStripSeparator1,
				this.toolStripLabel1,
				this.tscbCycle
			});
			this.tStripMenu.Location = new Point(0, 0);
			this.tStripMenu.Name = "tStripMenu";
			this.tStripMenu.Padding = new Padding(2, 1, 1, 0);
			this.tStripMenu.RenderMode = ToolStripRenderMode.Professional;
			this.tStripMenu.Size = new Size(384, 25);
			this.tStripMenu.TabIndex = 51;
			this.tStripMenu.KeyDown += new KeyEventHandler(this.tstbStock_KeyDown);
			this.tslbName.ForeColor = Color.Yellow;
			this.tslbName.Margin = new Padding(5, 1, 0, 2);
			this.tslbName.Name = "tslbName";
			this.tslbName.Size = new Size(12, 21);
			this.tslbName.Text = "-";
			this.toolStripSeparator2.Margin = new Padding(5, 0, 5, 0);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new Size(6, 27);
			this.toolStripSeparator2.Visible = false;
			this.toolStripLabel2.ForeColor = Color.LightGray;
			this.toolStripLabel2.Name = "toolStripLabel2";
			this.toolStripLabel2.Size = new Size(60, 24);
			this.toolStripLabel2.Text = "Indicator :";
			this.toolStripLabel2.Visible = false;
			this.tstbAddIndicator.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tstbAddIndicator.ForeColor = Color.Lime;
			this.tstbAddIndicator.ImageTransparentColor = Color.Magenta;
			this.tstbAddIndicator.Name = "tstbAddIndicator";
			this.tstbAddIndicator.Size = new Size(33, 24);
			this.tstbAddIndicator.Text = "Add";
			this.tstbAddIndicator.Visible = false;
			this.tstbAddIndicator.Click += new EventHandler(this.tstbAddIndicator_Click);
			this.tsbtnEditIndicator.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnEditIndicator.ForeColor = Color.FromArgb(128, 255, 255);
			this.tsbtnEditIndicator.ImageTransparentColor = Color.Magenta;
			this.tsbtnEditIndicator.Name = "tsbtnEditIndicator";
			this.tsbtnEditIndicator.Size = new Size(58, 24);
			this.tsbtnEditIndicator.Text = "Indicator";
			this.tsbtnEditIndicator.Visible = false;
			this.tsbtnEditIndicator.Click += new EventHandler(this.tsbtnEditIndicator_Click);
			this.tsDelIndicator.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsDelIndicator.ForeColor = Color.FromArgb(255, 128, 128);
			this.tsDelIndicator.ImageTransparentColor = Color.Magenta;
			this.tsDelIndicator.Name = "tsDelIndicator";
			this.tsDelIndicator.Size = new Size(44, 21);
			this.tsDelIndicator.Text = "Delete";
			this.tsDelIndicator.Visible = false;
			this.tsDelIndicator.Click += new EventHandler(this.tsDelIndicator_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 24);
			this.toolStripSeparator1.Visible = false;
			this.toolStripLabel1.ForeColor = Color.WhiteSmoke;
			this.toolStripLabel1.Margin = new Padding(5, 1, 5, 2);
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new Size(41, 21);
			this.toolStripLabel1.Text = "Period";
			this.toolStripLabel1.Visible = false;
			this.tscbCycle.BackColor = Color.FromArgb(45, 45, 45);
			this.tscbCycle.DropDownStyle = ComboBoxStyle.DropDownList;
			this.tscbCycle.ForeColor = Color.LightGray;
			this.tscbCycle.Items.AddRange(new object[]
			{
				"DAY",
				"WEEK",
				"MONTH"
			});
			this.tscbCycle.Name = "tscbCycle";
			this.tscbCycle.Size = new Size(80, 24);
			this.tscbCycle.Visible = false;
			this.tscbCycle.SelectedIndexChanged += new EventHandler(this.tscbCycle_SelectedIndexChanged);
			this.panel1.Controls.Add(this.chart);
			this.panel1.Controls.Add(this.tStripMenu);
			this.panel1.Location = new Point(170, 25);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(384, 236);
			this.panel1.TabIndex = 53;
			this.chart.AreaPercent = "3;1";
			this.chart.BackColor = Color.FromArgb(64, 64, 64);
			this.chart.CausesValidation = false;
			this.chart.ChartDragMode = ChartDragMode.Axis;
			this.chart.CrossCursorMouseMode = MouseAction.MouseDown;
			this.chart.DefaultFormulas = null;
			this.chart.Designing = false;
			this.chart.EndTime = new DateTime(0L);
			this.chart.FavoriteFormulas = "";
			exchangeIntraday.NativeCycle = true;
			exchangeIntraday.ShowFirstXLabel = true;
			exchangeIntraday.TimePeriods = new TimePeriod[0];
			exchangeIntraday.TimeZone = 7.0;
			this.chart.IntradayInfo = exchangeIntraday;
			this.chart.LatestValueType = LatestValueType.StockOnly;
			this.chart.Location = new Point(13, 59);
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
			this.chart.Size = new Size(400, 136);
			this.chart.Skin = "GreenRed";
			this.chart.StartTime = new DateTime(0L);
			this.chart.StickRenderType = StickRenderType.Default;
			this.chart.StockRenderType = StockRenderType.Default;
			this.chart.Symbol = "";
			this.chart.TabIndex = 52;
			this.chart.TabStop = false;
			this.chart.ValueTextMode = ValueTextMode.Default;
			this.chart.Enter += new EventHandler(this.chart_Enter);
			this.chart.Leave += new EventHandler(this.chart_Leave);
			this.cefWebBrowser1.Location = new Point(12, 168);
			this.cefWebBrowser1.Name = "cefWebBrowser1";
			this.cefWebBrowser1.Size = new Size(152, 93);
			this.cefWebBrowser1.TabIndex = 54;
			this.cefWebBrowser1.Text = "cefWebBrowser1";
			this.lbError.AutoSize = true;
			this.lbError.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 222);
			this.lbError.ForeColor = Color.Maroon;
			this.lbError.Location = new Point(9, 9);
			this.lbError.Name = "lbError";
			this.lbError.Size = new Size(146, 16);
			this.lbError.TabIndex = 55;
			this.lbError.Text = "eFin user is empty!!!";
			this.lbError.Visible = false;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = SystemColors.ControlDark;
			base.ClientSize = new Size(661, 334);
			base.Controls.Add(this.lbError);
			base.Controls.Add(this.cefWebBrowser1);
			base.Controls.Add(this.panel1);
			base.FormBorderStyle = FormBorderStyle.Sizable;
			base.Margin = new Padding(3, 4, 3, 4);
			base.Name = "frmStockChart";
			this.Text = "Stock History";
			base.IDoShownDelay += new ClientBaseForm.OnShownDelayEventHandler(this.frmStockHistory_IDoShownDelay);
			base.IDoLoadData += new ClientBaseForm.OnIDoLoadDataEventHandler(this.frmStockHistory_IDoLoadData);
			base.IDoFontChanged += new ClientBaseForm.OnFontChangedEventHandler(this.frmStockHistory_IDoFontChanged);
			base.IDoCustomSizeChanged += new ClientBaseForm.CustomSizeChangedEventHandler(this.frmStockHistory_IDoCustomSizeChanged);
			base.IDoMainFormKeyUp += new ClientBaseForm.OnFormKeyUpEventHandler(this.frmStockHistory_IDoMainFormKeyUp);
			base.IDoReActivated += new ClientBaseForm.OnReActiveEventHandler(this.frmStockHistory_IDoReActivated);
			base.Controls.SetChildIndex(this.panel1, 0);
			base.Controls.SetChildIndex(this.cefWebBrowser1, 0);
			base.Controls.SetChildIndex(this.lbError, 0);
			this.tStripMenu.ResumeLayout(false);
			this.tStripMenu.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmStockChart()
		{
			this.InitializeComponent();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmStockChart(Dictionary<string, object> propertiesValue) : base(propertiesValue)
		{
			this.InitializeComponent();
			try
			{
				if (!base.DesignMode)
				{
					if (this.bgwReloadData == null)
					{
						this.bgwReloadData = new BackgroundWorker();
						this.bgwReloadData.WorkerReportsProgress = true;
						this.bgwReloadData.DoWork += new DoWorkEventHandler(this.bgwReloadData_DoWork);
						this.bgwReloadData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwReloadData_RunWorkerCompleted);
					}
				}
				if (ApplicationInfo.IsSupportEfinChart)
				{
					this.cefWebBrowser1.Visible = true;
					this.panel1.Visible = false;
				}
				else
				{
					if (!base.DesignMode)
					{
						this.tstbStock.Items.AddRange(ApplicationInfo.StockAutoCompStringArr);
						this.tstbStock.Sorted = true;
						this.tstbStock.AutoCompleteMode = AutoCompleteMode.Suggest;
						this.tstbStock.AutoCompleteSource = AutoCompleteSource.ListItems;
					}
					this.cefWebBrowser1.Visible = false;
					this.panel1.Visible = true;
					this.InitChart();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("frmStockChart", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void InitChart()
		{
			try
			{
				this.chart.DefaultFormulas = "MAIN;VOL";
				this.chart.AdjustData = true;
				this.chart.ContextMenu = null;
				this.chart.Chart.FixedTime = false;
				this.chart.Skin = "History";
				DataManagerBase dataManager = new DbDataManager(DataCycleBase.DAY);
				this.chart.DataManager = dataManager;
				this.chart.CurrentDataCycle = DataCycle.Day;
				this.chart.StockRenderType = StockRenderType.Candle;
				this.chart.PriceLabelFormat = "{CODE} {DD} H:{HIGH} L:{LOW} O:{OPEN} C:{CLOSE}";
			}
			catch (Exception ex)
			{
				this.ShowError("InitChart", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwReloadData_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				base.IsLoadingData = true;
				if (ApplicationInfo.IsSupportEfinChart)
				{
					if (this._firstLoad)
					{
						ApplicationInfo.WebService.ClearEfinSession(ApplicationInfo.UserEfin);
					}
				}
				else
				{
					if (this.tds == null)
					{
						this.tds = new DataSet();
					}
					else
					{
						this.tds.Clear();
					}
					base.IsLoadingData = true;
					string text = ApplicationInfo.WebService.StockChart(this._stockInfo.Number, 256);
					if (this.tds == null)
					{
						this.tds = new DataSet();
					}
					else
					{
						this.tds.Clear();
					}
					if (!string.IsNullOrEmpty(text))
					{
						MyDataHelper.StringToDataSet(text, this.tds);
					}
				}
			}
			catch (Exception ex)
			{
				base.IsLoadingData = false;
				this.ShowError("bgwReloadData_DoWork", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwReloadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				if (ApplicationInfo.IsSupportEfinChart)
				{
					bool flag = false;
					string text = "Please Re-Login to open chart.";
					this._cfCurrentStock = ApplicationInfo.CurrentSymbol;
					try
					{
						if (this._firstLoad)
						{
							string text2 = string.Concat(new string[]
							{
								ApplicationInfo.UrlEfinChart,
								"?userid=",
								ApplicationInfo.UserEfin,
								"&symbol=",
								this._cfCurrentStock.Replace("&", "and")
							});
							if (text2 != null && this.cefWebBrowser1 != null && this.cefWebBrowser1.Browser != null && this.cefWebBrowser1.Browser.GetMainFrame() != null)
							{
								this.cefWebBrowser1.Browser.GetMainFrame().LoadUrl(text2);
							}
							else
							{
								text = "Please Re-Login to open chart.";
								if (this.cefWebBrowser1 == null)
								{
									text = "[Error1001]";
								}
								else
								{
									if (this.cefWebBrowser1.Browser == null)
									{
										text = "[Error1002]";
									}
									else
									{
										if (this.cefWebBrowser1.Browser.GetMainFrame() == null)
										{
											text = "[Error1003]";
										}
									}
								}
								flag = true;
							}
							this._firstLoad = false;
						}
						else
						{
							this.cefWebBrowser1.Browser.GetMainFrame().ExecuteJavaScript("changesymbol(\"" + this._cfCurrentStock.Replace("&", "and") + "\")", null, 0);
						}
					}
					catch (Exception ex)
					{
						text = "Error:: " + ex.Message;
						flag = true;
					}
					if (flag)
					{
						this.lbError.Text = text;
						this.cefWebBrowser1.Visible = false;
						this.lbError.Visible = true;
					}
				}
				else
				{
					if (this.tds != null)
					{
						if (this.tds.Tables.Contains("INFO2") && this.tds.Tables["INFO2"].Rows.Count > 0)
						{
							this.tslbName.Text = this.tds.Tables["INFO2"].Rows[0]["security_name"].ToString().Trim();
						}
						if (this.tds.Tables.Contains("INFO") && this.tds.Tables["INFO"].Rows.Count > 0)
						{
							decimal.TryParse(this.tds.Tables["INFO"].Rows[0]["open_price1"].ToString(), out this._openPrice);
						}
						this.chart.Symbol = this._stockInfo.Symbol;
						((DbDataManager)this.chart.HistoryDataManager).Tds = this.tds;
						this.chart.BindData_History();
						this.chart.NeedRebind();
						this.chart.ShowCrossCursor = true;
						this.chart.SetCursorByPosition(this.chart.GetLastDataIndex());
						this.tds.Clear();
					}
				}
			}
			catch (Exception ex2)
			{
				this.ShowError("bgwReloadData_RunWorkerCompleted", ex2);
			}
			base.IsLoadingData = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override Dictionary<string, object> DoPackProperties()
		{
			base.PropertiesValue.Clear();
			return base.PropertiesValue;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void DoUnpackProperties()
		{
			if (base.PropertiesValue != null)
			{
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmStockHistory_IDoShownDelay()
		{
			this.tscbCycle.Text = "DAY";
			this.SetResize(true);
			base.Show();
			base.OpenedForm();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmStockHistory_IDoLoadData()
		{
			try
			{
				if (ApplicationInfo.IsSupportEfinChart)
				{
					if (!string.IsNullOrEmpty(ApplicationInfo.UserEfin))
					{
						try
						{
							if (!this.bgwReloadData.IsBusy)
							{
								this.bgwReloadData.RunWorkerAsync();
							}
						}
						catch (Exception ex)
						{
							this.lbError.Text = ex.Message;
							this.cefWebBrowser1.Visible = false;
							this.lbError.Visible = true;
						}
					}
					else
					{
						this.ShowError("StockChart", new Exception("eFin user is empty!!!"));
						this.lbError.Text = "eFin user is empty!!!";
						this.lbError.Visible = true;
						this.cefWebBrowser1.Visible = false;
					}
				}
				else
				{
					this.SetNewStock(ApplicationInfo.CurrentSymbol);
				}
			}
			catch (Exception ex2)
			{
				this.ShowError("frmStockHistory_IDoLoadData", ex2);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ReloadCfWeb(string symbol)
		{
			bool flag = false;
			try
			{
				this._cfCurrentStock = symbol;
				if (this._firstLoad)
				{
					this._firstLoad = false;
					string text = string.Concat(new string[]
					{
						ApplicationInfo.UrlEfinChart,
						"?userid=",
						ApplicationInfo.UserEfin,
						"&symbol=",
						this._cfCurrentStock.Replace("&", "and")
					});
					if (text != null && this.cefWebBrowser1 != null && this.cefWebBrowser1.Browser != null && this.cefWebBrowser1.Browser.GetMainFrame() != null)
					{
						this.cefWebBrowser1.Browser.GetMainFrame().LoadUrl(text);
					}
					else
					{
						flag = true;
					}
				}
				else
				{
					this.cefWebBrowser1.Browser.GetMainFrame().ExecuteJavaScript("changesymbol(\"" + this._cfCurrentStock.Replace("&", "and") + "\")", null, 0);
				}
				if (flag)
				{
					this.lbError.Text = "Please Re-Login to open chart.";
					this.cefWebBrowser1.Visible = false;
					this.lbError.Visible = true;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("ReloadCfWeb", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmStockHistory_IDoCustomSizeChanged()
		{
			if (!base.IsLoadingData)
			{
				this.SetResize(this.IsWidthChanged | this.IsHeightChanged);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmStockHistory_IDoFontChanged()
		{
			if (!base.IsLoadingData)
			{
				this.SetResize(true);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmStockHistory_IDoReActivated()
		{
			if (!base.IsLoadingData)
			{
				if (ApplicationInfo.IsSupportEfinChart)
				{
					this.SetResize(true);
					base.Show();
					if (this._cfCurrentStock != ApplicationInfo.CurrentSymbol)
					{
						if (!this.bgwReloadData.IsBusy)
						{
							this.bgwReloadData.RunWorkerAsync();
						}
					}
				}
				else
				{
					if (this._stockInfo == null || this._stockInfo.Symbol != ApplicationInfo.CurrentSymbol)
					{
						this.SetNewStock(ApplicationInfo.CurrentSymbol);
					}
					this.SetResize(this.IsWidthChanged | this.IsHeightChanged);
					base.Show();
					this.chart.NeedRedraw();
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmStockHistory_IDoMainFormKeyUp(KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != Keys.Space)
			{
				switch (keyCode)
				{
				case Keys.Left:
				case Keys.Right:
					if (!ApplicationInfo.IsSupportEfinChart)
					{
						if (this._isChartFocus)
						{
							this.chart.HandleKeyEvent(this, e);
						}
					}
					break;
				}
			}
			else
			{
				this.tstbStock.Focus();
				this.tstbStock.SelectAll();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetResize(bool isChanged)
		{
			if (isChanged)
			{
				if (ApplicationInfo.IsSupportEfinChart)
				{
					this.cefWebBrowser1.SetBounds(0, 0, base.Width, base.Height);
				}
				else
				{
					this.panel1.SetBounds(0, 0, base.Width, base.Height);
					this.chart.SetBounds(0, this.tStripMenu.Top + this.tStripMenu.Height, this.panel1.Width, this.panel1.Height - (this.tStripMenu.Top + this.tStripMenu.Height));
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReloadData()
		{
			if (!this.bgwReloadData.IsBusy)
			{
				if (this._stockInfo != null)
				{
					this.bgwReloadData.RunWorkerAsync();
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateLastPrice(decimal lastPrice, decimal highPrice, decimal lowPrice, decimal accVol, decimal accVal)
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReceiveMessage(IBroadcastMessage message, StockList.StockInformation realtimeStockInfo)
		{
			if (this.FormState != ClientBaseForm.ClientBaseFormState.Closing)
			{
				if (!base.IsLoadingData)
				{
					try
					{
						if (this._stockInfo != null)
						{
							if (realtimeStockInfo != null)
							{
								string messageType = message.MessageType;
								if (messageType != null)
								{
									if (messageType == "L+")
									{
										if (realtimeStockInfo.Number == this._stockInfo.Number)
										{
											LSAccumulate lSAccumulate = (LSAccumulate)message;
											if (lSAccumulate.Side == string.Empty)
											{
												this._openPrice = lSAccumulate.LastPrice;
											}
											this.UpdateChart_Realtime(Convert.ToDouble(this._openPrice), Convert.ToDouble(realtimeStockInfo.HighPrice), Convert.ToDouble(realtimeStockInfo.LowPrice), Convert.ToDouble(lSAccumulate.LastPrice), Convert.ToDouble(lSAccumulate.AccVolume * (long)realtimeStockInfo.BoardLot));
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
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReceiveTfexMessage(IBroadcastMessage message, SeriesList.SeriesInformation realtimeSeriesInfo)
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void controlOrder_Enter(object sender, EventArgs e)
		{
			((Control)sender).BackColor = Color.Yellow;
			((Control)sender).ForeColor = Color.Black;
			if (sender.GetType() == typeof(TextBox))
			{
				((TextBox)sender).SelectAll();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void controlOrder_Leave(object sender, EventArgs e)
		{
			if (sender.GetType() == typeof(CheckBox))
			{
				((Control)sender).BackColor = Color.Transparent;
				if (this.BackColor == Color.DarkGreen)
				{
					((Control)sender).ForeColor = Color.White;
				}
				else
				{
					((Control)sender).ForeColor = Color.Black;
				}
			}
			else
			{
				((Control)sender).BackColor = Color.White;
				((Control)sender).ForeColor = Color.Black;
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
					break;
				default:
					switch (keyCode)
					{
					case Keys.Up:
					case Keys.Down:
						break;
					case Keys.Right:
						goto IL_48;
					default:
						goto IL_48;
					}
					break;
				}
				e.SuppressKeyPress = true;
				IL_48:;
			}
			catch (Exception ex)
			{
				this.ShowError("tstbStock_KeyDown", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tstbStock_KeyUp(object sender, KeyEventArgs e)
		{
			try
			{
				Keys keyCode = e.KeyCode;
				if (keyCode == Keys.Return)
				{
					this.SetNewStock(this.tstbStock.Text.Trim());
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tstbStock_KeyUp", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tstbStock_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!base.IsLoadingData)
			{
				this.SetNewStock(this.tstbStock.Text.Trim());
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetNewStock(string stockSymbol)
		{
			if (this.tStripMenu.InvokeRequired)
			{
				this.tStripMenu.Invoke(new frmStockChart.SetNewStockInfoCallBack(this.SetNewStock), new object[]
				{
					stockSymbol
				});
			}
			else
			{
				try
				{
					StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[stockSymbol];
					if (stockInformation.Number > 0 && (this._stockInfo == null || (this._stockInfo != null && this._stockInfo.Symbol != stockSymbol)))
					{
						this.selectDate = string.Empty;
						this._stockInfo = stockInformation;
						ApplicationInfo.CurrentSymbol = stockInformation.Symbol;
						this.ReloadData();
					}
					if (this.tstbStock.Text != this._stockInfo.Symbol)
					{
						this.tstbStock.Text = this._stockInfo.Symbol;
					}
					this.tstbStock.Focus();
					this.tstbStock.SelectAll();
				}
				catch (Exception ex)
				{
					this.ShowError("SetNewStockInfo", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnPageFirst_Click(object sender, EventArgs e)
		{
			try
			{
				this.selectDate = string.Empty;
				this.ReloadData();
			}
			catch (Exception ex)
			{
				this.ShowError("tsbtnPageNext_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void AddFormula()
		{
			FormulaArea selectedArea = this.chart.Chart.SelectedArea;
			if (selectedArea != null)
			{
				string value = "MA(12)";
				ArrayList arrayList = new ArrayList();
				arrayList.AddRange(selectedArea.FormulaToStrings());
				if (arrayList.IndexOf(value) < 0)
				{
					arrayList.Add(value);
					selectedArea.Formulas.Clear();
					selectedArea.StringsToFormula((string[])arrayList.ToArray(typeof(string)));
					this.chart.NeedRebind();
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void chart_Enter(object sender, EventArgs e)
		{
			this._isChartFocus = true;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void chart_Leave(object sender, EventArgs e)
		{
			this._isChartFocus = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void chart_CursorPosChanged(object sender, FormulaChart Chart, int Pos, IDataProvider idp)
		{
			try
			{
			}
			catch (Exception ex)
			{
				this.ShowError("chart_CursorPosChanged", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			this.UpdateChart_Realtime(120.0, 130.0, 125.0, 127.0, 150000.0);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateChart_Realtime(double openPrice, double highPrice, double lowPrice, double closePrice, double volume)
		{
			IDataProvider dataProvider = this.chart.Chart.DataProvider;
			if (dataProvider != null)
			{
				DateTime minValue = DateTime.MinValue;
				DateTime.TryParse(DateTime.Now.ToString("yyyy/MM/dd"), out minValue);
				DataPacket dataPacket = new DataPacket(this._stockInfo.Symbol, minValue.ToOADate(), openPrice, highPrice, lowPrice, closePrice, volume, 0.0, closePrice);
				CommonDataProvider commonDataProvider = (CommonDataProvider)this.chart.Chart.DataProvider;
				bool flag = commonDataProvider.Merge(dataPacket);
				if (this.chart.Symbol == dataPacket.Symbol)
				{
					this.chart.Chart.Bind();
					this.chart.NeedDrawCursor = true;
					if (flag)
					{
						this.chart.Chart.AdjustStartEndTime();
					}
					if (base.Visible)
					{
						this.chart.NeedRedraw();
					}
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnEditIndicator_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.chart.Chart.Areas.Count > 0)
				{
					this.chart.EditFormula(this.chart.Chart[0]);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tsbtnIndicator_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsDelIndicator_Click(object sender, EventArgs e)
		{
			this.chart.CloseArea(this.chart.Chart.SelectedArea);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tstbAddIndicator_Click(object sender, EventArgs e)
		{
			this.chart.InsertNewFormula();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tscbCycle_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (!base.IsLoadingData)
				{
					this.chart.CurrentDataCycle = DataCycle.Parse(this.tscbCycle.Text);
					this.chart.Chart.DataProvider.DataCycle = DataCycle.Parse(this.tscbCycle.Text);
					this.chart.Chart.Bind();
					this.chart.NeedRedraw();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tscbCycle_SelectedIndexChanged", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void toolStripButton1_Click_1(object sender, EventArgs e)
		{
			this.UpdateChart_Realtime(100.0, 120.0, 110.0, 105.0, 200.0);
		}
	}
}
