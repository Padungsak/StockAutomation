using i2TradePlus.Information;
using i2TradePlus.Properties;
using i2TradePlus.Templates;
using i2TradePlus.WindowsForms;
using ITSNet.Common.BIZ;
using ITSNet.Common.BIZ.RealtimeMessage;
using ITSNet.Common.BIZ.RealtimeMessage.TFEX;
using STIControl;
using STIControl.ExpandTableGrid;
using STIControl.SortTableGrid;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace i2TradePlus
{
	public class ucTickerSlide : UserControl, IRealtimeMessage
	{
		private delegate void UpdateLastSaleTickerCallBack(LSAccumulate msg, StockList.StockInformation realtimeStockInfo);
		private delegate void UpdateLastSaleTickerTFEXCallBack(LSMessageTFEX msg, SeriesList.SeriesInformation realtimeSeriesInfo);
		private delegate void ShowTickerSettingFormCallBack();
		private bool _isTickerLoading = true;
		private bool _isInit = false;
		private frmTickerSetting _frmTickerSetting = null;
		private int _favListPage = 1;
		private frmTickerSetting.filterType _filterType = frmTickerSetting.filterType.ALL_MARKET;
		private Color iColor;
		private Color sColor;
		private IContainer components = null;
		private SortGrid intzaTicker;
		private Panel panelTop;
		private SortGrid intzaTickerTFEX;
		private Button btnFilterSetting;
		private ToolTip toolTip1;
		private Label lbFilterType;
		public bool IsInit
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this._isInit;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ucTickerSlide()
		{
			this.InitializeComponent();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Init()
		{
			try
			{
				if (!this._isInit)
				{
					if (!ApplicationInfo.IsSupportTfex)
					{
						this._filterType = frmTickerSetting.filterType.SET_ONLY;
					}
					this.toolTip1.SetToolTip(this.btnFilterSetting, "Ticker Setting");
					this.SetHeaderColor(false);
					this._isTickerLoading = false;
					this._isInit = true;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("Init", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReceiveMessage(IBroadcastMessage message, StockList.StockInformation realtimeStockInfo)
		{
			try
			{
				if (!this._isTickerLoading)
				{
					if (message.MessageType == "L+")
					{
						if (this._filterType == frmTickerSetting.filterType.ALL_MARKET || this._filterType == frmTickerSetting.filterType.SET_ONLY)
						{
							this.UpdateTicker((LSAccumulate)message, realtimeStockInfo);
						}
						else
						{
							if (this._filterType == frmTickerSetting.filterType.SYMBOL)
							{
								if (ApplicationInfo.TickerStockList.Contains(realtimeStockInfo.Symbol))
								{
									this.UpdateTicker((LSAccumulate)message, realtimeStockInfo);
								}
							}
							else
							{
								if (this._filterType == frmTickerSetting.filterType.FAV1 || this._filterType == frmTickerSetting.filterType.FAV2 || this._filterType == frmTickerSetting.filterType.FAV3 || this._filterType == frmTickerSetting.filterType.FAV4 || this._filterType == frmTickerSetting.filterType.FAV5)
								{
									if (ApplicationInfo.FavStockList[this._favListPage].Contains(realtimeStockInfo.Symbol))
									{
										this.UpdateTicker((LSAccumulate)message, realtimeStockInfo);
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
		public void ReceiveTfexMessage(IBroadcastMessage message, SeriesList.SeriesInformation realtimeSeriesInfo)
		{
			if (message.MessageType == "LS")
			{
				if (this._filterType == frmTickerSetting.filterType.ALL_MARKET || this._filterType == frmTickerSetting.filterType.TFEX_ONLY)
				{
					this.UpdateTickerTFEX((LSMessageTFEX)message, realtimeSeriesInfo);
				}
				else
				{
					if (this._filterType == frmTickerSetting.filterType.SYMBOL)
					{
						if (ApplicationInfo.TickerStockList.Contains(realtimeSeriesInfo.Symbol))
						{
							this.UpdateTickerTFEX((LSMessageTFEX)message, realtimeSeriesInfo);
						}
					}
					else
					{
						if (this._filterType == frmTickerSetting.filterType.FAV1 || this._filterType == frmTickerSetting.filterType.FAV2 || this._filterType == frmTickerSetting.filterType.FAV3 || this._filterType == frmTickerSetting.filterType.FAV4 || this._filterType == frmTickerSetting.filterType.FAV5)
						{
							if (ApplicationInfo.FavStockList[this._favListPage].Contains(realtimeSeriesInfo.Symbol))
							{
								this.UpdateTickerTFEX((LSMessageTFEX)message, realtimeSeriesInfo);
							}
						}
					}
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateTicker(LSAccumulate msg, StockList.StockInformation realtimeStockInfo)
		{
			if (this.intzaTicker.InvokeRequired)
			{
				this.intzaTicker.Invoke(new ucTickerSlide.UpdateLastSaleTickerCallBack(this.UpdateTicker), new object[]
				{
					msg,
					realtimeStockInfo
				});
			}
			else
			{
				try
				{
					if (realtimeStockInfo.Number > -1)
					{
						int num = this.intzaTicker.FocusItemIndex + 1;
						num++;
						if (num > this.intzaTicker.Rows)
						{
							num = 1;
						}
						if (num + 1 <= this.intzaTicker.Rows)
						{
							this.intzaTicker.ClearAllTextByRow(num);
							this.intzaTicker.EndUpdate(num);
						}
						STIControl.SortTableGrid.RecordItem recordItem = this.intzaTicker.Records(num - 1);
						recordItem.Fields("side").Text = msg.Side;
						if (realtimeStockInfo.Symbol.Length > 8)
						{
							recordItem.Fields("stock").Text = realtimeStockInfo.Symbol.Substring(0, 8) + "...";
						}
						else
						{
							recordItem.Fields("stock").Text = realtimeStockInfo.Symbol;
						}
						recordItem.Fields("volume").Text = msg.Volume * (long)realtimeStockInfo.BoardLot;
						recordItem.Fields("price").Text = msg.LastPrice;
						recordItem.Fields("change").Text = realtimeStockInfo.ChangePrice;
						if (realtimeStockInfo.Symbol == ApplicationInfo.CurrStockInMktWatch)
						{
							recordItem.Fields("stock").Tag = "@F";
						}
						else
						{
							recordItem.Fields("stock").Tag = string.Empty;
						}
						this.iColor = Utilities.ComparePriceCFColor(msg.LastPrice, realtimeStockInfo);
						this.sColor = Color.Yellow;
						if (msg.Side == "B")
						{
							this.sColor = Color.Cyan;
						}
						else
						{
							if (msg.Side == "S")
							{
								this.sColor = Color.Magenta;
							}
						}
						recordItem.Fields("stock").FontColor = this.iColor;
						recordItem.Fields("side").FontColor = this.sColor;
						recordItem.Fields("volume").FontColor = this.sColor;
						recordItem.Fields("price").FontColor = this.iColor;
						recordItem.Fields("change").FontColor = this.iColor;
						this.intzaTicker.SetFocusItem(num - 1);
					}
				}
				catch (Exception ex)
				{
					this.ShowError("UpdateLastSaleTicker", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateTickerTFEX(LSMessageTFEX msg, SeriesList.SeriesInformation realtimeSeriesInfo)
		{
			if (this.intzaTickerTFEX.InvokeRequired)
			{
				this.intzaTickerTFEX.Invoke(new ucTickerSlide.UpdateLastSaleTickerTFEXCallBack(this.UpdateTickerTFEX), new object[]
				{
					msg,
					realtimeSeriesInfo
				});
			}
			else
			{
				try
				{
					if (!string.IsNullOrEmpty(realtimeSeriesInfo.Symbol))
					{
						int num = this.intzaTickerTFEX.FocusItemIndex + 1;
						num++;
						if (num > this.intzaTickerTFEX.Rows)
						{
							num = 1;
						}
						if (num + 1 <= this.intzaTickerTFEX.Rows)
						{
							this.intzaTickerTFEX.ClearAllTextByRow(num);
							this.intzaTickerTFEX.EndUpdate(num);
						}
						STIControl.SortTableGrid.RecordItem recordItem = this.intzaTickerTFEX.Records(num - 1);
						decimal num2 = msg.Price - realtimeSeriesInfo.PrevFixPrice;
						recordItem.Fields("stock").Text = realtimeSeriesInfo.Symbol;
						recordItem.Fields("side").Text = msg.Side;
						recordItem.Fields("volume").Text = msg.Vol;
						recordItem.Fields("price").Text = Utilities.PriceFormat(msg.Price, realtimeSeriesInfo.NumOfDec);
						recordItem.Fields("change").Text = Utilities.PriceFormat(num2, true, "");
						Color fontColor = Utilities.ComparePriceCFColor(msg.Price, realtimeSeriesInfo);
						Color fontColor2 = Color.Yellow;
						if (msg.Side.ToString() == "B")
						{
							fontColor2 = Color.Cyan;
						}
						else
						{
							if (msg.Side.ToString() == "S")
							{
								fontColor2 = Color.Magenta;
							}
						}
						recordItem.Fields("stock").FontColor = fontColor;
						recordItem.Fields("side").FontColor = fontColor2;
						recordItem.Fields("volume").FontColor = fontColor2;
						recordItem.Fields("price").FontColor = fontColor;
						recordItem.Fields("change").FontColor = fontColor;
						this.intzaTickerTFEX.SetFocusItem(num - 1);
					}
				}
				catch (Exception ex)
				{
					this.ShowError("UpdateLastSaleTicker", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaTicker_TableMouseClick(object sender, STIControl.SortTableGrid.TableMouseEventArgs e)
		{
			try
			{
				if (e.Mouse.Button == MouseButtons.Left)
				{
					if (e.RowIndex >= 0)
					{
						string text = this.intzaTicker.Records(e.RowIndex).Fields("stock").Text.ToString();
						if (!string.IsNullOrEmpty(text))
						{
							TemplateManager.Instance.SendSymbolLink(this, SymbolLinkSource.StockSymbol, text);
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("intzaLS2_TableMouseClick", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowError(string functionName, Exception ex)
		{
			ExceptionManager.Show(new ErrorItem(DateTime.Now, base.Name, functionName, ex.Message, ex.ToString()));
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetHeaderColor(bool isRedraw)
		{
			try
			{
				foreach (Control control in base.Controls)
				{
					if (control.GetType() == typeof(SortGrid) || control.GetType() == typeof(ExpandGrid))
					{
						this.SetHeader(control, isRedraw);
					}
					else
					{
						if (control.GetType() == typeof(Panel))
						{
							foreach (Control control2 in control.Controls)
							{
								if (control2.GetType() == typeof(SortGrid) || control2.GetType() == typeof(ExpandGrid))
								{
									this.SetHeader(control2, isRedraw);
								}
								else
								{
									if (control2.GetType() == typeof(Panel))
									{
										foreach (Control control3 in control2.Controls)
										{
											if (control3.GetType() == typeof(SortGrid) || control3.GetType() == typeof(ExpandGrid))
											{
												this.SetHeader(control3, isRedraw);
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
				this.ShowError("SetHeaderBackColor", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetHeader(Control control, bool isRedraw)
		{
			try
			{
				if (control.GetType() == typeof(ExpandGrid))
				{
					foreach (STIControl.ExpandTableGrid.ColumnItem current in ((ExpandGrid)control).Columns)
					{
						current.BackColor = Settings.Default.HeaderBackGColor;
						current.FontColor = Settings.Default.HeaderFontColor;
					}
					((ExpandGrid)control).GridColor = Settings.Default.GridColor;
					if (isRedraw)
					{
						((ExpandGrid)control).Redraw();
					}
				}
				else
				{
					if (control.GetType() == typeof(SortGrid))
					{
						foreach (STIControl.SortTableGrid.ColumnItem current2 in ((SortGrid)control).Columns)
						{
							current2.BackColor = Settings.Default.HeaderBackGColor;
							current2.FontColor = Settings.Default.HeaderFontColor;
						}
						((SortGrid)control).GridColor = Settings.Default.GridColor;
						if (isRedraw)
						{
							((SortGrid)control).Redraw();
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
		private void tStripMenu_SizeChanged(object sender, EventArgs e)
		{
			this.SetResize();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetResize()
		{
			try
			{
				this._isTickerLoading = true;
				int num = this.panelTop.Top + this.panelTop.Height + 1;
				if (this.intzaTicker.Font != Settings.Default.Default_Font)
				{
					this.intzaTicker.Font = Settings.Default.Default_Font;
				}
				if (this.intzaTickerTFEX.Font != Settings.Default.Default_Font)
				{
					this.intzaTickerTFEX.Font = Settings.Default.Default_Font;
				}
				if (this._filterType == frmTickerSetting.filterType.SET_ONLY || !ApplicationInfo.IsSupportTfex)
				{
					this.intzaTicker.SetBounds(0, num, base.Width, base.Height - num);
					if (this.intzaTicker.GetRecordPerPage() != this.intzaTicker.Rows)
					{
						this.intzaTicker.SetRowByHeight();
					}
					this.intzaTicker.Visible = true;
					this.intzaTickerTFEX.Visible = false;
				}
				else
				{
					if (this._filterType == frmTickerSetting.filterType.TFEX_ONLY)
					{
						this.intzaTickerTFEX.SetBounds(0, num, base.Width, base.Height - num);
						if (this.intzaTickerTFEX.GetRecordPerPage() != this.intzaTickerTFEX.Rows)
						{
							this.intzaTickerTFEX.SetRowByHeight();
						}
						this.intzaTickerTFEX.Visible = true;
						this.intzaTicker.Visible = false;
					}
					else
					{
						int heightByRows = this.intzaTickerTFEX.GetHeightByRows(6);
						this.intzaTicker.SetBounds(0, num, base.Width, base.Height - (heightByRows + num + 1));
						this.intzaTickerTFEX.SetBounds(0, this.intzaTicker.Top + this.intzaTicker.Height + 1, base.Width, base.Height - (this.intzaTicker.Top + this.intzaTicker.Height + 1));
						if (this.intzaTicker.GetRecordPerPage() != this.intzaTicker.Rows)
						{
							this.intzaTicker.SetRowByHeight();
							this.intzaTickerTFEX.SetRowByHeight();
						}
						this.intzaTicker.Visible = true;
						this.intzaTickerTFEX.Visible = true;
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SetResize", ex);
			}
			this._isTickerLoading = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnFilterSetting_Click(object sender, EventArgs e)
		{
			try
			{
				this.ShowTickerSettingForm();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowTickerSettingForm()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new ucTickerSlide.ShowTickerSettingFormCallBack(this.ShowTickerSettingForm));
			}
			else
			{
				try
				{
					if (this._frmTickerSetting != null)
					{
						if (!this._frmTickerSetting.IsDisposed)
						{
							this._frmTickerSetting.Dispose();
						}
						this._frmTickerSetting = null;
					}
					this._frmTickerSetting = new frmTickerSetting(this._filterType);
					this._frmTickerSetting.FormClosing -= new FormClosingEventHandler(this.frmTickerSetting_FormClosing);
					this._frmTickerSetting.FormClosing += new FormClosingEventHandler(this.frmTickerSetting_FormClosing);
					this._frmTickerSetting.TopLevel = false;
					this._frmTickerSetting.Parent = base.Parent.Parent;
					this._frmTickerSetting.Location = new Point((this._frmTickerSetting.Parent.Width - this._frmTickerSetting.Width) / 2, (this._frmTickerSetting.Parent.Height - this._frmTickerSetting.Height) / 2);
					this._frmTickerSetting.TopMost = true;
					this._frmTickerSetting.Show();
					this._frmTickerSetting.BringToFront();
				}
				catch (Exception ex)
				{
					this.ShowError("ShowMessageInFormConfirm", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmTickerSetting_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				this._filterType = ((frmTickerSetting)sender).FilterTickerType;
				switch (this._filterType)
				{
				case frmTickerSetting.filterType.ALL_MARKET:
					this.lbFilterType.Text = "Filter : All";
					break;
				case frmTickerSetting.filterType.SET_ONLY:
					this.lbFilterType.Text = "Filter : SET Only";
					break;
				case frmTickerSetting.filterType.TFEX_ONLY:
					this.lbFilterType.Text = "Filter : TFEX Only";
					break;
				case frmTickerSetting.filterType.SYMBOL:
					ApplicationInfo.TickerStockList = ((frmTickerSetting)sender).SymbolList;
					this.lbFilterType.Text = "Filter : Symbols";
					break;
				case frmTickerSetting.filterType.FAV1:
					this._favListPage = 1;
					this.lbFilterType.Text = "Filter : Favorite 1";
					break;
				case frmTickerSetting.filterType.FAV2:
					this._favListPage = 2;
					this.lbFilterType.Text = "Filter : Favorite 2";
					break;
				case frmTickerSetting.filterType.FAV3:
					this._favListPage = 3;
					this.lbFilterType.Text = "Filter : Favorite 3";
					break;
				case frmTickerSetting.filterType.FAV4:
					this._favListPage = 4;
					this.lbFilterType.Text = "Filter : Favorite 4";
					break;
				case frmTickerSetting.filterType.FAV5:
					this._favListPage = 5;
					this.lbFilterType.Text = "Filter : Favorite 5";
					break;
				}
				this.SetResize();
			}
			catch (Exception ex)
			{
				throw ex;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ucTickerSlide));
			STIControl.SortTableGrid.ColumnItem columnItem = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem2 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem3 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem4 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem5 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem6 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem7 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem8 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem9 = new STIControl.SortTableGrid.ColumnItem();
			STIControl.SortTableGrid.ColumnItem columnItem10 = new STIControl.SortTableGrid.ColumnItem();
			this.panelTop = new Panel();
			this.btnFilterSetting = new Button();
			this.toolTip1 = new ToolTip(this.components);
			this.intzaTickerTFEX = new SortGrid();
			this.intzaTicker = new SortGrid();
			this.lbFilterType = new Label();
			this.panelTop.SuspendLayout();
			base.SuspendLayout();
			this.panelTop.BackColor = Color.FromArgb(20, 20, 20);
			this.panelTop.Controls.Add(this.lbFilterType);
			this.panelTop.Controls.Add(this.btnFilterSetting);
			this.panelTop.Dock = DockStyle.Top;
			this.panelTop.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.panelTop.Location = new Point(0, 0);
			this.panelTop.Name = "panelTop";
			this.panelTop.Size = new Size(268, 28);
			this.panelTop.TabIndex = 2;
			this.btnFilterSetting.FlatAppearance.BorderColor = Color.DimGray;
			this.btnFilterSetting.FlatAppearance.BorderSize = 0;
			this.btnFilterSetting.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnFilterSetting.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 192, 192);
			this.btnFilterSetting.FlatStyle = FlatStyle.Flat;
			this.btnFilterSetting.Image = (Image)componentResourceManager.GetObject("btnFilterSetting.Image");
			this.btnFilterSetting.Location = new Point(3, 2);
			this.btnFilterSetting.Name = "btnFilterSetting";
			this.btnFilterSetting.Size = new Size(26, 23);
			this.btnFilterSetting.TabIndex = 7;
			this.toolTip1.SetToolTip(this.btnFilterSetting, "Options");
			this.btnFilterSetting.UseVisualStyleBackColor = true;
			this.btnFilterSetting.Click += new EventHandler(this.btnFilterSetting_Click);
			this.toolTip1.IsBalloon = true;
			this.toolTip1.ToolTipTitle = "Info guide";
			this.intzaTickerTFEX.AllowDrop = true;
			this.intzaTickerTFEX.BackColor = Color.FromArgb(10, 10, 10);
			this.intzaTickerTFEX.CanBlink = false;
			this.intzaTickerTFEX.CanDrag = true;
			this.intzaTickerTFEX.CanGetMouseMove = false;
			columnItem.Alignment = StringAlignment.Near;
			columnItem.BackColor = Color.FromArgb(64, 64, 64);
			columnItem.ColumnAlignment = StringAlignment.Center;
			columnItem.FontColor = Color.LightGray;
			columnItem.MyStyle = FontStyle.Regular;
			columnItem.Name = "stock";
			columnItem.Text = "Series";
			columnItem.ValueFormat = FormatType.Symbol;
			columnItem.Visible = true;
			columnItem.Width = 28;
			columnItem2.Alignment = StringAlignment.Center;
			columnItem2.BackColor = Color.FromArgb(64, 64, 64);
			columnItem2.ColumnAlignment = StringAlignment.Center;
			columnItem2.FontColor = Color.LightGray;
			columnItem2.MyStyle = FontStyle.Regular;
			columnItem2.Name = "side";
			columnItem2.Text = "B/S";
			columnItem2.ValueFormat = FormatType.Text;
			columnItem2.Visible = true;
			columnItem2.Width = 12;
			columnItem3.Alignment = StringAlignment.Far;
			columnItem3.BackColor = Color.FromArgb(64, 64, 64);
			columnItem3.ColumnAlignment = StringAlignment.Center;
			columnItem3.FontColor = Color.LightGray;
			columnItem3.MyStyle = FontStyle.Regular;
			columnItem3.Name = "volume";
			columnItem3.Text = "Vol";
			columnItem3.ValueFormat = FormatType.Volume;
			columnItem3.Visible = true;
			columnItem3.Width = 18;
			columnItem4.Alignment = StringAlignment.Far;
			columnItem4.BackColor = Color.FromArgb(64, 64, 64);
			columnItem4.ColumnAlignment = StringAlignment.Center;
			columnItem4.FontColor = Color.LightGray;
			columnItem4.MyStyle = FontStyle.Regular;
			columnItem4.Name = "price";
			columnItem4.Text = "Price";
			columnItem4.ValueFormat = FormatType.Text;
			columnItem4.Visible = true;
			columnItem4.Width = 23;
			columnItem5.Alignment = StringAlignment.Far;
			columnItem5.BackColor = Color.FromArgb(64, 64, 64);
			columnItem5.ColumnAlignment = StringAlignment.Center;
			columnItem5.FontColor = Color.LightGray;
			columnItem5.MyStyle = FontStyle.Regular;
			columnItem5.Name = "change";
			columnItem5.Text = "Chg";
			columnItem5.ValueFormat = FormatType.ChangePrice;
			columnItem5.Visible = true;
			columnItem5.Width = 19;
			this.intzaTickerTFEX.Columns.Add(columnItem);
			this.intzaTickerTFEX.Columns.Add(columnItem2);
			this.intzaTickerTFEX.Columns.Add(columnItem3);
			this.intzaTickerTFEX.Columns.Add(columnItem4);
			this.intzaTickerTFEX.Columns.Add(columnItem5);
			this.intzaTickerTFEX.CurrentScroll = 0;
			this.intzaTickerTFEX.FocusItemIndex = -1;
			this.intzaTickerTFEX.ForeColor = Color.Black;
			this.intzaTickerTFEX.GridColor = Color.FromArgb(45, 45, 45);
			this.intzaTickerTFEX.HeaderPctHeight = 80f;
			this.intzaTickerTFEX.IsAutoRepaint = true;
			this.intzaTickerTFEX.IsDrawFullRow = true;
			this.intzaTickerTFEX.IsDrawGrid = true;
			this.intzaTickerTFEX.IsDrawHeader = true;
			this.intzaTickerTFEX.IsScrollable = false;
			this.intzaTickerTFEX.Location = new Point(-3, 323);
			this.intzaTickerTFEX.MainColumn = "";
			this.intzaTickerTFEX.Name = "intzaTickerTFEX";
			this.intzaTickerTFEX.Rows = 0;
			this.intzaTickerTFEX.RowSelectColor = Color.FromArgb(80, 80, 80);
			this.intzaTickerTFEX.RowSelectType = 1;
			this.intzaTickerTFEX.RowsVisible = 0;
			this.intzaTickerTFEX.ScrollChennelColor = Color.LightGray;
			this.intzaTickerTFEX.Size = new Size(268, 107);
			this.intzaTickerTFEX.SortColumnName = "";
			this.intzaTickerTFEX.SortType = SortType.Desc;
			this.intzaTickerTFEX.TabIndex = 3;
			this.intzaTicker.AllowDrop = true;
			this.intzaTicker.BackColor = Color.FromArgb(10, 10, 10);
			this.intzaTicker.CanBlink = false;
			this.intzaTicker.CanDrag = true;
			this.intzaTicker.CanGetMouseMove = false;
			columnItem6.Alignment = StringAlignment.Near;
			columnItem6.BackColor = Color.FromArgb(64, 64, 64);
			columnItem6.ColumnAlignment = StringAlignment.Center;
			columnItem6.FontColor = Color.LightGray;
			columnItem6.MyStyle = FontStyle.Regular;
			columnItem6.Name = "stock";
			columnItem6.Text = "Stock";
			columnItem6.ValueFormat = FormatType.Symbol;
			columnItem6.Visible = true;
			columnItem6.Width = 28;
			columnItem7.Alignment = StringAlignment.Center;
			columnItem7.BackColor = Color.FromArgb(64, 64, 64);
			columnItem7.ColumnAlignment = StringAlignment.Center;
			columnItem7.FontColor = Color.LightGray;
			columnItem7.MyStyle = FontStyle.Regular;
			columnItem7.Name = "side";
			columnItem7.Text = "B/S";
			columnItem7.ValueFormat = FormatType.Text;
			columnItem7.Visible = true;
			columnItem7.Width = 12;
			columnItem8.Alignment = StringAlignment.Far;
			columnItem8.BackColor = Color.FromArgb(64, 64, 64);
			columnItem8.ColumnAlignment = StringAlignment.Center;
			columnItem8.FontColor = Color.LightGray;
			columnItem8.MyStyle = FontStyle.Regular;
			columnItem8.Name = "volume";
			columnItem8.Text = "Volume";
			columnItem8.ValueFormat = FormatType.Volume;
			columnItem8.Visible = true;
			columnItem8.Width = 26;
			columnItem9.Alignment = StringAlignment.Far;
			columnItem9.BackColor = Color.FromArgb(64, 64, 64);
			columnItem9.ColumnAlignment = StringAlignment.Center;
			columnItem9.FontColor = Color.LightGray;
			columnItem9.MyStyle = FontStyle.Regular;
			columnItem9.Name = "price";
			columnItem9.Text = "Price";
			columnItem9.ValueFormat = FormatType.Price;
			columnItem9.Visible = true;
			columnItem9.Width = 17;
			columnItem10.Alignment = StringAlignment.Far;
			columnItem10.BackColor = Color.FromArgb(64, 64, 64);
			columnItem10.ColumnAlignment = StringAlignment.Center;
			columnItem10.FontColor = Color.LightGray;
			columnItem10.MyStyle = FontStyle.Regular;
			columnItem10.Name = "change";
			columnItem10.Text = "Chg";
			columnItem10.ValueFormat = FormatType.ChangePrice;
			columnItem10.Visible = true;
			columnItem10.Width = 17;
			this.intzaTicker.Columns.Add(columnItem6);
			this.intzaTicker.Columns.Add(columnItem7);
			this.intzaTicker.Columns.Add(columnItem8);
			this.intzaTicker.Columns.Add(columnItem9);
			this.intzaTicker.Columns.Add(columnItem10);
			this.intzaTicker.CurrentScroll = 0;
			this.intzaTicker.FocusItemIndex = -1;
			this.intzaTicker.ForeColor = Color.Black;
			this.intzaTicker.GridColor = Color.FromArgb(45, 45, 45);
			this.intzaTicker.HeaderPctHeight = 80f;
			this.intzaTicker.IsAutoRepaint = true;
			this.intzaTicker.IsDrawFullRow = true;
			this.intzaTicker.IsDrawGrid = true;
			this.intzaTicker.IsDrawHeader = true;
			this.intzaTicker.IsScrollable = false;
			this.intzaTicker.Location = new Point(0, 26);
			this.intzaTicker.MainColumn = "";
			this.intzaTicker.Name = "intzaTicker";
			this.intzaTicker.Rows = 0;
			this.intzaTicker.RowSelectColor = Color.FromArgb(80, 80, 80);
			this.intzaTicker.RowSelectType = 1;
			this.intzaTicker.RowsVisible = 0;
			this.intzaTicker.ScrollChennelColor = Color.LightGray;
			this.intzaTicker.Size = new Size(268, 291);
			this.intzaTicker.SortColumnName = "";
			this.intzaTicker.SortType = SortType.Desc;
			this.intzaTicker.TabIndex = 1;
			this.intzaTicker.TableMouseClick += new SortGrid.TableMouseClickEventHandler(this.intzaTicker_TableMouseClick);
			this.lbFilterType.AutoSize = true;
			this.lbFilterType.ForeColor = Color.Silver;
			this.lbFilterType.Location = new Point(35, 5);
			this.lbFilterType.Name = "lbFilterType";
			this.lbFilterType.Size = new Size(73, 15);
			this.lbFilterType.TabIndex = 8;
			this.lbFilterType.Text = "Filter : None";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.intzaTickerTFEX);
			base.Controls.Add(this.panelTop);
			base.Controls.Add(this.intzaTicker);
			base.Name = "ucTickerSlide";
			base.Size = new Size(268, 430);
			this.panelTop.ResumeLayout(false);
			this.panelTop.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
