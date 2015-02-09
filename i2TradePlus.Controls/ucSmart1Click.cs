using i2TradePlus.Information;
using i2TradePlus.Properties;
using i2TradePlus.Templates;
using ITSNet.Common.BIZ;
using ITSNet.Common.BIZ.RealtimeMessage;
using STIControl;
using STIControl.SortTableGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
namespace i2TradePlus.Controls
{
	public class ucSmart1Click : UserControl, IRealtimeMessage
	{
		private struct RecordData
		{
			public int buyDeal;
			public long buyVol;
			public decimal price;
			public long sellVol;
			public int sellDeal;
		}
		private delegate void ReceiveOrderInfoCallBack(IBroadcastMessage message);
		private delegate void ShowMessageInFormConfirmCallBack(string message, frmOrderFormConfirm.OpenStyle openStyle);
		private delegate void ShowSplashCallBack(bool visible, string message, bool isAutoClose);
		private StockList.StockInformation _stockInfo = null;
		private BackgroundWorker bgwReloadData = null;
		private BackgroundWorker bgwGetOrderForCancel = null;
		private DataSet tdsMainInfo = null;
		private bool _isInit = false;
		private System.Windows.Forms.Timer tmLoad = null;
		private Dictionary<string, ucSmart1Click.RecordData> _lstOrder = null;
		private decimal _price;
		private string _cancelSide = string.Empty;
		private decimal _cancelPrice;
		private Point _cancelLocation;
		private DataSet tdsOrderForCancel = null;
		private Dictionary<long, string> _listOfOrderCancel = null;
		private frmOrderFormConfirm frmConfirm = null;
		private System.Windows.Forms.Timer tmCloseSplash = null;
		private bool _isLoading = true;
		private IContainer components = null;
		private ToolStrip toolStrip1;
		private ToolStripComboBox tstbStock;
		private ToolStrip toolStrip2;
		private ToolStripButton tsbtnDefVol100;
		private ToolStripButton tsbtnDefVol1000;
		private ToolStripButton tsbtnDefVol10K;
		private SortGrid sortGrid;
		private ToolStripButton tsbtnDownPrice;
		private ToolStripButton tsbtnUpPrice;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripTextBox tstbDefVol;
		private ToolStripButton tsbtnDefVol20K;
		private ToolStripButton tsbtnDefVol200;
		private ToolStripButton tsbtnDefVol500;
		private ContextMenuStrip contextMenuStrip1;
		private Label lbLoading;
		private Panel panelTitle;
		private ToolTip toolTip1;
		private ToolStripButton tsbtnDefVol100K;
		private ToolStripLabel toolStripLabel1;
		private ToolStripDropDownButton toolStripDropDownButton1;
		private ToolStripMenuItem tsmLink;
		private ToolStripMenuItem tsmSmartClick;
		private ToolStripButton toolStripButton1;
		private ToolStripButton tsbtnRefresh;
		[CompilerGenerated]
		//private static Comparison<decimal> <>9__CachedAnonymousMethodDelegate1;
		public bool IsLoadingData
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this._isLoading;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				if (!base.DesignMode)
				{
					this._isLoading = value;
					this.ShowSplash(this._isLoading, "Loading...", false);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ucSmart1Click()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
			base.UpdateStyles();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Init()
		{
			try
			{
				if (!this._isInit)
				{
					this._isInit = true;
					this.bgwReloadData = new BackgroundWorker();
					this.bgwReloadData.WorkerReportsProgress = true;
					this.bgwReloadData.DoWork += new DoWorkEventHandler(this.bgwReloadData_DoWork);
					this.bgwReloadData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwReloadData_RunWorkerCompleted);
					this.bgwGetOrderForCancel = new BackgroundWorker();
					this.bgwGetOrderForCancel.WorkerReportsProgress = true;
					this.bgwGetOrderForCancel.DoWork += new DoWorkEventHandler(this.bgwGetOrderForCancel_DoWork);
					this.bgwGetOrderForCancel.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwGetOrderForCancel_RunWorkerCompleted);
					this.tstbStock.Items.AddRange(ApplicationInfo.StockAutoCompStringArr);
					this.tstbStock.Sorted = true;
					this.tstbStock.AutoCompleteMode = AutoCompleteMode.Suggest;
					this.tstbStock.AutoCompleteSource = AutoCompleteSource.ListItems;
					this.setNewStock(ApplicationInfo.CurrentSymbol, true);
					this.tsmSmartClick.Checked = Settings.Default.SmartOneClick;
					this.tsmLink.Checked = Settings.Default.SmartClickLink;
					this.tstbDefVol.Text = Utilities.VolumeFormat(Settings.Default.SmartClickVolume, true);
				}
				else
				{
					if (Settings.Default.SmartClickLink)
					{
						this.setNewStock(ApplicationInfo.CurrentSymbol, false);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("Init", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetResize()
		{
			try
			{
				this.setToolStripFont(this.toolStrip1);
				this.setToolStripFont(this.toolStrip2);
				this.sortGrid.Font = new Font(Settings.Default.Default_Font.Name, Settings.Default.Default_Font.Size - 1f, Settings.Default.Default_Font.Style);
				this.panelTitle.Invalidate();
			}
			catch (Exception ex)
			{
				this.ShowError("SetResize", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void setNewStock(string stock, bool isForce)
		{
			try
			{
				StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[stock];
				if (stockInformation.Number > 0)
				{
					if (stockInformation != this._stockInfo || isForce)
					{
						this._stockInfo = stockInformation;
						if (this.tmLoad == null)
						{
							this.tmLoad = new System.Windows.Forms.Timer();
							this.tmLoad.Interval = 300;
							this.tmLoad.Tick += new EventHandler(this.tmLoad_Tick);
						}
						this.tmLoad.Stop();
						this.tmLoad.Start();
					}
				}
				else
				{
					this._stockInfo = null;
					this.IsLoadingData = false;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("setNewStock", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tmLoad_Tick(object sender, EventArgs e)
		{
			try
			{
				this.tmLoad.Stop();
				if (this._stockInfo != null)
				{
					if (!this.bgwReloadData.IsBusy)
					{
						this.bgwReloadData.RunWorkerAsync();
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tmLoad_Tick", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwReloadData_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				if (this._stockInfo != null)
				{
					this.IsLoadingData = true;
					if (this.tdsMainInfo == null)
					{
						this.tdsMainInfo = new DataSet();
					}
					else
					{
						this.tdsMainInfo.Clear();
					}
					string dataForSmartClick = ApplicationInfo.WebOrderService.GetDataForSmartClick(this._stockInfo.Symbol, ApplicationInfo.AccInfo.CurrentAccount);
					if (!string.IsNullOrEmpty(dataForSmartClick))
					{
						MyDataHelper.StringToDataSet(dataForSmartClick, this.tdsMainInfo);
					}
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
				this.tstbStock.Text = this._stockInfo.Symbol;
				this._price = ((this._stockInfo.LastSalePrice > 0m) ? this._stockInfo.LastSalePrice : this._stockInfo.PriorPrice);
				if (this._price == 0m)
				{
					if (!decimal.TryParse(this._stockInfo.BidPrice1, out this._price))
					{
						decimal.TryParse(this._stockInfo.OfferPrice1, out this._price);
					}
					if (this._price == 0m)
					{
						return;
					}
				}
				List<decimal> list = new List<decimal>();
				list.Add(this._price);
				decimal num = this._price;
				for (int i = 0; i < 15; i++)
				{
					num -= this.GetNextSpreadDown(num);
					if (!(num > 0m) || !(num >= this._stockInfo.Floor))
					{
						break;
					}
					list.Add(num);
				}
				num = this._price;
				for (int i = 0; i < 15; i++)
				{
					num += this.GetNextSpreadUp(num);
					if (!(num <= this._stockInfo.Ceiling))
					{
						break;
					}
					list.Add(num);
				}
				list.Sort((decimal r1, decimal r2) => decimal.Compare(r2, r1));
				if (this._lstOrder == null)
				{
					this._lstOrder = new Dictionary<string, ucSmart1Click.RecordData>();
				}
				else
				{
					this._lstOrder.Clear();
				}
				foreach (decimal current in list)
				{
					ucSmart1Click.RecordData value = default(ucSmart1Click.RecordData);
					value.price = current;
					this._lstOrder.Add(Utilities.PriceFormat(current), value);
				}
				list.Clear();
				list = null;
				if (this.tdsMainInfo.Tables.Contains("ORDERS") && this.tdsMainInfo.Tables["ORDERS"].Rows.Count > 0)
				{
					string key = string.Empty;
					string a = string.Empty;
					foreach (DataRow dataRow in this.tdsMainInfo.Tables["ORDERS"].Rows)
					{
						key = Utilities.PriceFormat(dataRow["price"].ToString());
						if (this._lstOrder.ContainsKey(key))
						{
							a = dataRow["side"].ToString().Trim();
							ucSmart1Click.RecordData value = this._lstOrder[key];
							if (a == "B")
							{
								value.buyVol += Convert.ToInt64(dataRow["volume"].ToString()) - Convert.ToInt64(dataRow["matched_volume"].ToString());
								value.buyDeal++;
							}
							else
							{
								value.sellVol += Convert.ToInt64(dataRow["volume"].ToString()) - Convert.ToInt64(dataRow["matched_volume"].ToString());
								value.sellDeal++;
							}
							this._lstOrder[key] = value;
						}
					}
				}
				this.sortGrid.BeginUpdate();
				this.sortGrid.Rows = this._lstOrder.Count;
				this.sortGrid.ClearAllText();
				int num2 = 0;
				int num3 = -1;
				foreach (KeyValuePair<string, ucSmart1Click.RecordData> current2 in this._lstOrder)
				{
					this.updateToGrid(num2, current2.Value);
					if (current2.Value.price == this._price)
					{
						num3 = num2;
					}
					num2++;
				}
				this.sortGrid.SetFocusItem(num3);
				this.sortGrid.SetCenterScroll(num3);
				this.sortGrid.Redraw();
				this.tdsMainInfo.Clear();
				long num4 = Settings.Default.SmartClickVolume;
				long num5 = num4 % (long)this._stockInfo.BoardLot;
				if (num5 > 0L)
				{
					num4 -= num5;
					this.tstbDefVol.Text = Utilities.VolumeFormat(num4, true);
					Settings.Default.SmartClickVolume = num4;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SecurityInfo:RunWorkerCompleted", ex);
			}
			this.IsLoadingData = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void updateToGrid(int index, ucSmart1Click.RecordData recData)
		{
			try
			{
				RecordItem recordItem;
				if (index == -1)
				{
					recordItem = this.sortGrid.AddRecord(-1, false);
				}
				else
				{
					recordItem = this.sortGrid.Records(index);
				}
				if (recData.buyDeal > 0)
				{
					recordItem.Fields("buycancel").Text = "0";
				}
				else
				{
					recordItem.Fields("buycancel").Text = "";
				}
				if (recData.sellDeal > 0)
				{
					recordItem.Fields("sellcancel").Text = "0";
				}
				else
				{
					recordItem.Fields("sellcancel").Text = "";
				}
				recordItem.Fields("buydeal").Text = recData.buyDeal;
				recordItem.Fields("buyvol").Text = recData.buyVol;
				recordItem.Fields("price").Text = recData.price;
				recordItem.Fields("sellvol").Text = recData.sellVol;
				recordItem.Fields("selldeal").Text = recData.sellDeal;
				Color fontColor = Utilities.ComparePriceCFColor(recData.price, this._stockInfo);
				recordItem.Fields("buydeal").FontColor = fontColor;
				recordItem.Fields("buyvol").FontColor = fontColor;
				recordItem.Fields("price").FontColor = fontColor;
				recordItem.Fields("price").BackColor = Color.FromArgb(25, 25, 25);
				recordItem.Fields("sellvol").FontColor = fontColor;
				recordItem.Fields("selldeal").FontColor = fontColor;
			}
			catch (Exception ex)
			{
				this.ShowError("updateToGrid", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowError(string functionName, Exception ex)
		{
			ExceptionManager.Show(new ErrorItem(DateTime.Now, base.Name, functionName, ex.Message, ex.ToString()));
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private decimal GetNextSpreadDown(decimal Price)
		{
			decimal result;
			if (Price <= 2m)
			{
				result = 0.01m;
			}
			else
			{
				if (Price <= 5m)
				{
					result = 0.02m;
				}
				else
				{
					if (Price <= 10m)
					{
						result = 0.05m;
					}
					else
					{
						if (Price <= 25m)
						{
							result = 0.1m;
						}
						else
						{
							if (Price <= 100m)
							{
								result = 0.25m;
							}
							else
							{
								if (Price <= 200m)
								{
									result = 0.5m;
								}
								else
								{
									if (Price <= 400m)
									{
										result = 1m;
									}
									else
									{
										result = 2m;
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
		private decimal GetNextSpreadUp(decimal Price)
		{
			decimal result;
			if (Price < 2m)
			{
				result = 0.01m;
			}
			else
			{
				if (Price < 5m)
				{
					result = 0.02m;
				}
				else
				{
					if (Price < 10m)
					{
						result = 0.05m;
					}
					else
					{
						if (Price < 25m)
						{
							result = 0.1m;
						}
						else
						{
							if (Price < 100m)
							{
								result = 0.25m;
							}
							else
							{
								if (Price < 200m)
								{
									result = 0.5m;
								}
								else
								{
									if (Price < 400m)
									{
										result = 1m;
									}
									else
									{
										result = 2m;
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
		public void SetHeaderColor(bool isRedraw)
		{
			try
			{
				foreach (Control control in base.Controls)
				{
					if (control.GetType() == typeof(SortGrid))
					{
						foreach (ColumnItem current in ((SortGrid)control).Columns)
						{
							current.BackColor = Settings.Default.HeaderBackGColor;
							current.FontColor = Settings.Default.HeaderFontColor;
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
				this.ShowError("SetHeaderBackColor", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tstbStock_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!this._isLoading)
			{
				this.setNewStock(this.tstbStock.Text, false);
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
		private void setToolStripFont(ToolStrip control)
		{
			try
			{
				Font font = new Font(Settings.Default.Default_Font.Name, Settings.Default.Default_Font.Size - 1f, FontStyle.Regular);
				control.Font = font;
				foreach (ToolStripItem toolStripItem in control.Items)
				{
					toolStripItem.Font = font;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("setToolStripFont", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReceiveMessage(IBroadcastMessage message, StockList.StockInformation realtimeStockInfo)
		{
			if (!this._isLoading)
			{
				try
				{
					string messageType = message.MessageType;
					if (messageType != null)
					{
						if (!(messageType == "0I"))
						{
							if (messageType == "L+")
							{
								if (this._stockInfo != null && realtimeStockInfo.Number == this._stockInfo.Number)
								{
									LSAccumulate lSAccumulate = (LSAccumulate)message;
									int num = this.sortGrid.FindIndex("price", Utilities.PriceFormat(lSAccumulate.LastPrice));
									if (num > -1)
									{
										this.sortGrid.SetFocusItem(num);
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
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ReceiveOrderInfo(IBroadcastMessage message)
		{
			if (this.sortGrid.InvokeRequired)
			{
				this.sortGrid.Invoke(new ucSmart1Click.ReceiveOrderInfoCallBack(this.ReceiveOrderInfo), new object[]
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
						if (this._lstOrder.ContainsKey(Utilities.PriceFormat(orderInfoClient.PriceToSET)))
						{
							ucSmart1Click.RecordData recordData = this._lstOrder[Utilities.PriceFormat(orderInfoClient.PriceToSET)];
							string status = orderInfoClient.Status;
							switch (status)
							{
							case "X":
							case "XA":
							case "C":
							{
								if (orderInfoClient.Side == "B")
								{
									recordData.buyDeal--;
									recordData.buyVol -= orderInfoClient.Volume - orderInfoClient.MatchedVolume;
								}
								else
								{
									if (orderInfoClient.Side == "S")
									{
										recordData.sellDeal--;
										recordData.sellVol -= orderInfoClient.Volume - orderInfoClient.MatchedVolume;
									}
								}
								this._lstOrder[Utilities.PriceFormat(orderInfoClient.PriceToSET)] = recordData;
								int num2 = this.sortGrid.FindIndex("price", Utilities.PriceFormat(orderInfoClient.PriceToSET));
								if (num2 > -1)
								{
									this.updateToGrid(num2, recordData);
									this.sortGrid.EndUpdate(num2);
								}
								break;
							}
							case "M":
							case "MA":
							{
								if (orderInfoClient.Side == "B")
								{
									recordData.buyDeal--;
									recordData.buyVol -= orderInfoClient.LastVolumeInCase;
								}
								else
								{
									if (orderInfoClient.Side == "S")
									{
										recordData.sellDeal--;
										recordData.sellVol -= orderInfoClient.LastVolumeInCase;
									}
								}
								this._lstOrder[Utilities.PriceFormat(orderInfoClient.PriceToSET)] = recordData;
								int num2 = this.sortGrid.FindIndex("price", Utilities.PriceFormat(orderInfoClient.PriceToSET));
								if (num2 > -1)
								{
									this.updateToGrid(num2, recordData);
									this.sortGrid.EndUpdate(num2);
								}
								break;
							}
							case "O":
							case "OA":
							case "PO":
							case "POA":
							{
								if (orderInfoClient.Side == "B")
								{
									if (orderInfoClient.OriginalMessageType == "1I")
									{
										recordData.buyVol += orderInfoClient.Volume;
										recordData.buyDeal++;
									}
									else
									{
										recordData.buyVol -= orderInfoClient.LastVolumeInCase;
									}
								}
								else
								{
									if (orderInfoClient.Side == "S")
									{
										if (orderInfoClient.OriginalMessageType == "1I")
										{
											recordData.sellVol += orderInfoClient.Volume;
											recordData.sellDeal++;
										}
										else
										{
											recordData.sellVol -= orderInfoClient.LastVolumeInCase;
										}
									}
								}
								this._lstOrder[Utilities.PriceFormat(orderInfoClient.PriceToSET)] = recordData;
								int num2 = this.sortGrid.FindIndex("price", Utilities.PriceFormat(orderInfoClient.PriceToSET));
								if (num2 > -1)
								{
									this.updateToGrid(num2, recordData);
									this.sortGrid.EndUpdate(num2);
								}
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
		private void sortGrid_TableMouseClick(object sender, TableMouseEventArgs e)
		{
			try
			{
				if (e.RowIndex > -1)
				{
					if (e.Mouse.Button == MouseButtons.Left)
					{
						if (e.Column.Name == "buyvol")
						{
							string price = this.sortGrid.Records(e.RowIndex).Fields("price").Text.ToString();
							TemplateManager.Instance.MainForm.SendOrderBox.SetSmartOneClick("B", this._stockInfo.Symbol, price, Settings.Default.SmartClickVolume);
						}
						else
						{
							if (e.Column.Name == "sellvol")
							{
								string price = this.sortGrid.Records(e.RowIndex).Fields("price").Text.ToString();
								TemplateManager.Instance.MainForm.SendOrderBox.SetSmartOneClick("S", this._stockInfo.Symbol, price, Settings.Default.SmartClickVolume);
							}
							else
							{
								if (e.Column.Name == "buycancel")
								{
									if (this.sortGrid.Records(e.RowIndex).Fields(e.Column.Name).Text.ToString() == "1")
									{
										this._cancelSide = "B";
										decimal.TryParse(this.sortGrid.Records(e.RowIndex).Fields("price").Text.ToString(), out this._cancelPrice);
										if (!this.bgwGetOrderForCancel.IsBusy)
										{
											this._cancelLocation = new Point(e.Mouse.X, e.FieldPosition.Top);
											if (!this.bgwGetOrderForCancel.IsBusy)
											{
												this.bgwGetOrderForCancel.RunWorkerAsync();
											}
										}
									}
								}
								else
								{
									if (e.Column.Name == "sellcancel")
									{
										if (this.sortGrid.Records(e.RowIndex).Fields(e.Column.Name).Text.ToString() == "1")
										{
											this._cancelSide = "S";
											decimal.TryParse(this.sortGrid.Records(e.RowIndex).Fields("price").Text.ToString(), out this._cancelPrice);
											if (!this.bgwGetOrderForCancel.IsBusy)
											{
												this._cancelLocation = new Point(e.Mouse.X, e.FieldPosition.Top);
												if (!this.bgwGetOrderForCancel.IsBusy)
												{
													this.bgwGetOrderForCancel.RunWorkerAsync();
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
				this.ShowError("sortGrid_TableMouseClick", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwGetOrderForCancel_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				if (this.tdsOrderForCancel == null)
				{
					this.tdsOrderForCancel = new DataSet();
				}
				else
				{
					this.tdsOrderForCancel.Clear();
				}
				string orderFor1Click = ApplicationInfo.WebOrderService.GetOrderFor1Click(this._stockInfo.Symbol, ApplicationInfo.AccInfo.CurrentAccount, this._cancelPrice.ToString(), this._cancelSide);
				if (!string.IsNullOrEmpty(orderFor1Click))
				{
					MyDataHelper.StringToDataSet(orderFor1Click, this.tdsOrderForCancel);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("bgwGetOrderForCancel_DoWork", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwGetOrderForCancel_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				if (this.tdsOrderForCancel != null && this.tdsOrderForCancel.Tables.Contains("ORDERS"))
				{
					foreach (object current in this.contextMenuStrip1.Items)
					{
						if (current.GetType() == typeof(ToolStripMenuItem))
						{
							(current as ToolStripMenuItem).Click -= new EventHandler(this.item_Click);
						}
					}
					this.contextMenuStrip1.Items.Clear();
					foreach (DataRow dataRow in this.tdsOrderForCancel.Tables["ORDERS"].Rows)
					{
						ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
						toolStripMenuItem.Text = "OrderNo " + dataRow["order_number"].ToString().Trim() + " ,Vol " + dataRow["volume"].ToString();
						toolStripMenuItem.Tag = dataRow["order_number"].ToString().Trim() + "|" + dataRow["sSendDate"].ToString().Trim();
						toolStripMenuItem.Click += new EventHandler(this.item_Click);
						this.contextMenuStrip1.Items.Add(toolStripMenuItem);
					}
					if (this.contextMenuStrip1.Items.Count > 0)
					{
						this.contextMenuStrip1.Items.Add(new ToolStripSeparator());
						ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
						toolStripMenuItem.Text = "CANCEL ALL";
						toolStripMenuItem.Tag = "ALL";
						toolStripMenuItem.Click += new EventHandler(this.item_Click);
						this.contextMenuStrip1.Items.Add(toolStripMenuItem);
						ToolStripSeparator value = new ToolStripSeparator();
						this.contextMenuStrip1.Items.Add(value);
						ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
						toolStripMenuItem2.Text = "Close";
						toolStripMenuItem2.Tag = "CLOSE";
						toolStripMenuItem2.Click += new EventHandler(this.item_Click);
						this.contextMenuStrip1.Items.Add(toolStripMenuItem2);
					}
					this.contextMenuStrip1.Show(this.sortGrid, this._cancelLocation.X, this._cancelLocation.Y);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("bgwGetOrderForCancel_RunWorkerCompleted", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void item_Click(object sender, EventArgs e)
		{
			try
			{
				string text = (sender as ToolStripMenuItem).Tag.ToString();
				if (this._listOfOrderCancel == null)
				{
					this._listOfOrderCancel = new Dictionary<long, string>();
				}
				else
				{
					this._listOfOrderCancel.Clear();
				}
				if (text == "ALL")
				{
					foreach (DataRow dataRow in this.tdsOrderForCancel.Tables["ORDERS"].Rows)
					{
						this._listOfOrderCancel.Add(Convert.ToInt64(dataRow["order_number"].ToString()), dataRow["sSendDate"].ToString().Trim());
					}
					this.ShowMessageInFormConfirm("Do you want to cancel all at " + this._cancelPrice + " ?", frmOrderFormConfirm.OpenStyle.ConfirmCancel);
				}
				else
				{
					if (text == "CLOSE")
					{
						int num = this.sortGrid.FindIndex("price", Utilities.PriceFormat(this._cancelPrice));
						if (num > -1)
						{
							RecordItem recordItem = this.sortGrid.Records(num);
							if (this._cancelSide == "B" && recordItem.Fields("buycancel").Text.ToString() == "1")
							{
								recordItem.Fields("buycancel").Text = "0";
							}
							if (this._cancelSide == "S" && recordItem.Fields("sellcancel").Text.ToString() == "1")
							{
								recordItem.Fields("sellcancel").Text = "0";
							}
							this.sortGrid.EndUpdate(num);
						}
					}
					else
					{
						string[] array = text.Split(new char[]
						{
							'|'
						});
						this._listOfOrderCancel.Add(Convert.ToInt64(array[0]), array[1]);
						this.ShowMessageInFormConfirm("Do you want to cancel order number " + Convert.ToInt64(array[0]) + " ?", frmOrderFormConfirm.OpenStyle.ConfirmCancel);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("item_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void LoopCancelOrder()
		{
			try
			{
				foreach (KeyValuePair<long, string> current in this._listOfOrderCancel)
				{
					if (current.Key > 0L)
					{
						this.SendCancelOrder(current.Value, current.Key);
						Thread.Sleep(20);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("LoopCancelOrder", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool SendCancelOrder(string sendDate, long orderNumber)
		{
			bool result = false;
			string data = string.Empty;
			try
			{
				this.ShowSplash(true, "Send Cancel Order " + orderNumber + "...", true);
				data = ApplicationInfo.WebOrderService.SendCancelOrder(sendDate, orderNumber, ApplicationInfo.AccInfo.CurrentAccount, ApplicationInfo.IP, ApplicationInfo.AuthenKey, ApplicationInfo.AccInfo.CurrInternetUser, ApplicationInfo.GetTermicalId());
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
							if (Convert.ToInt32(dataSet.Tables["Results"].Rows[0]["code"]) == 1)
							{
								this.ShowSplash(true, "Send Request Cancel Order Number " + orderNumber, true);
							}
							else
							{
								this.ShowMessageInFormConfirm(dataSet.Tables["Results"].Rows[0]["message"].ToString(), frmOrderFormConfirm.OpenStyle.ShowBox);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowMessageInFormConfirm(ex.Message, frmOrderFormConfirm.OpenStyle.ShowBox);
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowMessageInFormConfirm(string message, frmOrderFormConfirm.OpenStyle openStyle)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new ucSmart1Click.ShowMessageInFormConfirmCallBack(this.ShowMessageInFormConfirm), new object[]
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
						RecordItem recordItem = this.sortGrid.Find("price", Utilities.PriceFormat(this._cancelPrice));
						if (recordItem != null)
						{
							if (this._cancelSide == "B")
							{
								recordItem.Fields("buycancel").Text = "0";
							}
							else
							{
								if (this._cancelSide == "S")
								{
									recordItem.Fields("sellcancel").Text = "0";
								}
							}
							this.sortGrid.EndUpdate();
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("ConfirmForm", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void changeDefaultVolume_Click(object sender, EventArgs e)
		{
			try
			{
				Settings.Default.SmartClickVolume = Convert.ToInt64((sender as ToolStripButton).Text.Replace(",", ""));
				this.tstbDefVol.Text = Utilities.VolumeFormat(Settings.Default.SmartClickVolume, true);
			}
			catch (Exception ex)
			{
				this.ShowError("changeDefaultVolume_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnUpPrice_Click(object sender, EventArgs e)
		{
			try
			{
				if (sender == this.tsbtnUpPrice)
				{
					Settings.Default.SmartClickVolume += (long)this._stockInfo.BoardLot;
				}
				else
				{
					Settings.Default.SmartClickVolume -= (long)this._stockInfo.BoardLot;
					if (Settings.Default.SmartClickVolume <= 0L)
					{
						Settings.Default.SmartClickVolume = (long)this._stockInfo.BoardLot;
					}
				}
				this.tstbDefVol.Text = Utilities.VolumeFormat(Settings.Default.SmartClickVolume.ToString(), true);
			}
			catch (Exception ex)
			{
				this.ShowError("tsbtnUpPrice_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tstbDefVol_TextChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.tstbDefVol.Text.Trim() != string.Empty)
				{
					if (FormatUtil.Isnumeric(this.tstbDefVol.Text))
					{
						try
						{
							decimal num = Convert.ToInt64(this.tstbDefVol.Text.Replace(",", ""));
							this.tstbDefVol.Text = num.ToString("#,###");
							this.tstbDefVol.Select(this.tstbDefVol.Text.Length, 0);
						}
						catch
						{
							this.tstbDefVol.Text = this.tstbDefVol.Text.Substring(0, this.tstbDefVol.Text.Length - 1);
						}
					}
					else
					{
						this.tstbDefVol.Text = this.tstbDefVol.Text.Substring(0, this.tstbDefVol.Text.Length - 1);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tstbDefVol_TextChanged", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tstbDefVol_Leave(object sender, EventArgs e)
		{
			try
			{
				if (this.tstbDefVol.Text.Trim() != string.Empty)
				{
					if (FormatUtil.Isnumeric(this.tstbDefVol.Text))
					{
						try
						{
							decimal d = Convert.ToInt64(this.tstbDefVol.Text.Replace(",", ""));
							if (!(d > 0m) || !(d < this._stockInfo.BoardLot))
							{
								d %= this._stockInfo.BoardLot;
								if (d > 0m)
								{
									this.tstbDefVol.Text = Utilities.VolumeFormat(Settings.Default.SmartClickVolume, true);
								}
								else
								{
									Settings.Default.SmartClickVolume = Convert.ToInt64(this.tstbDefVol.Text.Replace(",", ""));
								}
							}
						}
						catch
						{
							this.tstbDefVol.Text = Utilities.VolumeFormat(Settings.Default.SmartClickVolume, true);
						}
					}
					else
					{
						this.tstbDefVol.Text = Utilities.VolumeFormat(Settings.Default.SmartClickVolume, true);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tstbDefVol_Leave", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowSplash(bool visible, string message, bool isAutoClose)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new ucSmart1Click.ShowSplashCallBack(this.ShowSplash), new object[]
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
		public void SetSmartStock(object sender, SymbolLinkSource source, string newStock)
		{
			if (this._isInit)
			{
				if (source == SymbolLinkSource.SmartStock && Settings.Default.SmartClickLink)
				{
					this.setNewStock(newStock, false);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void panelTitle_Paint(object sender, PaintEventArgs e)
		{
			if (e.ClipRectangle.Width == this.panelTitle.Width)
			{
				e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
				Color teal = Color.Teal;
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(e.ClipRectangle, teal, Color.FromArgb(80, teal), LinearGradientMode.Vertical))
				{
					e.Graphics.FillRectangle(linearGradientBrush, e.ClipRectangle);
				}
				using (StringFormat stringFormat = new StringFormat())
				{
					stringFormat.Alignment = StringAlignment.Center;
					stringFormat.LineAlignment = StringAlignment.Center;
					e.Graphics.DrawString("Smart One Click", this.Font, Brushes.LightGray, e.ClipRectangle, stringFormat);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsmLink_Click(object sender, EventArgs e)
		{
			this.tsmLink.Checked = !this.tsmLink.Checked;
			Settings.Default.SmartClickLink = this.tsmLink.Checked;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsmSmartClick_Click(object sender, EventArgs e)
		{
			this.tsmSmartClick.Checked = !this.tsmSmartClick.Checked;
			Settings.Default.SmartOneClick = this.tsmSmartClick.Checked;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnRefresh_Click(object sender, EventArgs e)
		{
			try
			{
				this.setNewStock((this._stockInfo != null) ? this._stockInfo.Symbol : ApplicationInfo.CurrentSymbol, true);
			}
			catch (Exception ex)
			{
				this.ShowError("tsbtnRefresh_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void sortGrid_ItemDragDrop(object sender, TableMouseEventArgs e, string dragValue)
		{
			this.setNewStock(dragValue, false);
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ucSmart1Click));
			ColumnItem columnItem = new ColumnItem();
			ColumnItem columnItem2 = new ColumnItem();
			ColumnItem columnItem3 = new ColumnItem();
			ColumnItem columnItem4 = new ColumnItem();
			ColumnItem columnItem5 = new ColumnItem();
			ColumnItem columnItem6 = new ColumnItem();
			ColumnItem columnItem7 = new ColumnItem();
			this.toolStrip1 = new ToolStrip();
			this.toolStripLabel1 = new ToolStripLabel();
			this.tstbStock = new ToolStripComboBox();
			this.toolStripDropDownButton1 = new ToolStripDropDownButton();
			this.tsmLink = new ToolStripMenuItem();
			this.tsmSmartClick = new ToolStripMenuItem();
			this.toolStripButton1 = new ToolStripButton();
			this.toolStrip2 = new ToolStrip();
			this.tsbtnDownPrice = new ToolStripButton();
			this.tstbDefVol = new ToolStripTextBox();
			this.tsbtnUpPrice = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.tsbtnDefVol100 = new ToolStripButton();
			this.tsbtnDefVol1000 = new ToolStripButton();
			this.tsbtnDefVol200 = new ToolStripButton();
			this.tsbtnDefVol500 = new ToolStripButton();
			this.tsbtnDefVol10K = new ToolStripButton();
			this.tsbtnDefVol20K = new ToolStripButton();
			this.tsbtnDefVol100K = new ToolStripButton();
			this.contextMenuStrip1 = new ContextMenuStrip(this.components);
			this.lbLoading = new Label();
			this.panelTitle = new Panel();
			this.toolTip1 = new ToolTip(this.components);
			this.sortGrid = new SortGrid();
			this.tsbtnRefresh = new ToolStripButton();
			this.toolStrip1.SuspendLayout();
			this.toolStrip2.SuspendLayout();
			base.SuspendLayout();
			this.toolStrip1.BackColor = Color.FromArgb(20, 20, 20);
			this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripLabel1,
				this.tstbStock,
				this.toolStripDropDownButton1,
				this.toolStripButton1,
				this.tsbtnRefresh
			});
			this.toolStrip1.Location = new Point(0, 21);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Padding = new Padding(1, 1, 1, 2);
			this.toolStrip1.RenderMode = ToolStripRenderMode.Professional;
			this.toolStrip1.Size = new Size(331, 26);
			this.toolStrip1.TabIndex = 2;
			this.toolStrip1.Tag = "-1";
			this.toolStrip1.Text = "toolStrip1";
			this.toolStripLabel1.ForeColor = Color.LightGray;
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new Size(36, 20);
			this.toolStripLabel1.Text = "Stock";
			this.tstbStock.BackColor = Color.FromArgb(20, 20, 20);
			this.tstbStock.ForeColor = Color.Gainsboro;
			this.tstbStock.Margin = new Padding(2, 0, 1, 0);
			this.tstbStock.Name = "tstbStock";
			this.tstbStock.Size = new Size(110, 23);
			this.tstbStock.SelectedIndexChanged += new EventHandler(this.tstbStock_SelectedIndexChanged);
			this.tstbStock.KeyPress += new KeyPressEventHandler(this.tstbStock_KeyPress);
			this.toolStripDropDownButton1.Alignment = ToolStripItemAlignment.Right;
			this.toolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.tsmLink,
				this.tsmSmartClick
			});
			this.toolStripDropDownButton1.ForeColor = Color.Gainsboro;
			this.toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
			this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			this.toolStripDropDownButton1.Size = new Size(62, 20);
			this.toolStripDropDownButton1.Text = "Options";
			this.tsmLink.Name = "tsmLink";
			this.tsmLink.Size = new Size(134, 22);
			this.tsmLink.Text = "Link";
			this.tsmLink.Click += new EventHandler(this.tsmLink_Click);
			this.tsmSmartClick.Name = "tsmSmartClick";
			this.tsmSmartClick.Size = new Size(134, 22);
			this.tsmSmartClick.Text = "Smart Click";
			this.tsmSmartClick.Click += new EventHandler(this.tsmSmartClick_Click);
			this.toolStripButton1.Alignment = ToolStripItemAlignment.Right;
			this.toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.toolStripButton1.Image = (Image)componentResourceManager.GetObject("toolStripButton1.Image");
			this.toolStripButton1.ImageTransparentColor = Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new Size(23, 20);
			this.toolStripButton1.ToolTipText = "วิธีการใช้งาน";
			this.toolStripButton1.Visible = false;
			this.toolStrip2.BackColor = Color.FromArgb(20, 20, 20);
			this.toolStrip2.GripStyle = ToolStripGripStyle.Hidden;
			this.toolStrip2.Items.AddRange(new ToolStripItem[]
			{
				this.tsbtnDownPrice,
				this.tstbDefVol,
				this.tsbtnUpPrice,
				this.toolStripSeparator1,
				this.tsbtnDefVol100,
				this.tsbtnDefVol1000,
				this.tsbtnDefVol200,
				this.tsbtnDefVol500,
				this.tsbtnDefVol10K,
				this.tsbtnDefVol20K,
				this.tsbtnDefVol100K
			});
			this.toolStrip2.LayoutStyle = ToolStripLayoutStyle.Flow;
			this.toolStrip2.Location = new Point(0, 47);
			this.toolStrip2.Name = "toolStrip2";
			this.toolStrip2.Padding = new Padding(1, 1, 1, 2);
			this.toolStrip2.RenderMode = ToolStripRenderMode.Professional;
			this.toolStrip2.Size = new Size(331, 48);
			this.toolStrip2.TabIndex = 4;
			this.toolStrip2.Tag = "-1";
			this.tsbtnDownPrice.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbtnDownPrice.Image = Resources.MovePreviousHS;
			this.tsbtnDownPrice.ImageTransparentColor = Color.Magenta;
			this.tsbtnDownPrice.Name = "tsbtnDownPrice";
			this.tsbtnDownPrice.Padding = new Padding(2, 0, 2, 0);
			this.tsbtnDownPrice.Size = new Size(24, 20);
			this.tsbtnDownPrice.Click += new EventHandler(this.tsbtnUpPrice_Click);
			this.tstbDefVol.BackColor = Color.Gainsboro;
			this.tstbDefVol.BorderStyle = BorderStyle.FixedSingle;
			this.tstbDefVol.ForeColor = Color.Black;
			this.tstbDefVol.MaxLength = 9;
			this.tstbDefVol.Name = "tstbDefVol";
			this.tstbDefVol.Size = new Size(80, 23);
			this.tstbDefVol.Text = "100";
			this.tstbDefVol.TextBoxTextAlign = HorizontalAlignment.Center;
			this.tstbDefVol.Leave += new EventHandler(this.tstbDefVol_Leave);
			this.tstbDefVol.TextChanged += new EventHandler(this.tstbDefVol_TextChanged);
			this.tsbtnUpPrice.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbtnUpPrice.Image = Resources.MoveNextHS;
			this.tsbtnUpPrice.ImageTransparentColor = Color.Magenta;
			this.tsbtnUpPrice.Name = "tsbtnUpPrice";
			this.tsbtnUpPrice.Padding = new Padding(2, 0, 2, 0);
			this.tsbtnUpPrice.Size = new Size(24, 20);
			this.tsbtnUpPrice.Click += new EventHandler(this.tsbtnUpPrice_Click);
			this.toolStripSeparator1.Margin = new Padding(0, 0, 5, 0);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 23);
			this.tsbtnDefVol100.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnDefVol100.ForeColor = Color.LightGray;
			this.tsbtnDefVol100.ImageTransparentColor = Color.Magenta;
			this.tsbtnDefVol100.Name = "tsbtnDefVol100";
			this.tsbtnDefVol100.Size = new Size(29, 19);
			this.tsbtnDefVol100.Text = "100";
			this.tsbtnDefVol100.Click += new EventHandler(this.changeDefaultVolume_Click);
			this.tsbtnDefVol1000.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnDefVol1000.ForeColor = Color.LightGray;
			this.tsbtnDefVol1000.ImageTransparentColor = Color.Magenta;
			this.tsbtnDefVol1000.Name = "tsbtnDefVol1000";
			this.tsbtnDefVol1000.Size = new Size(38, 19);
			this.tsbtnDefVol1000.Text = "1,000";
			this.tsbtnDefVol1000.Click += new EventHandler(this.changeDefaultVolume_Click);
			this.tsbtnDefVol200.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnDefVol200.ForeColor = Color.LightGray;
			this.tsbtnDefVol200.ImageTransparentColor = Color.Magenta;
			this.tsbtnDefVol200.Name = "tsbtnDefVol200";
			this.tsbtnDefVol200.Size = new Size(35, 19);
			this.tsbtnDefVol200.Text = "2000";
			this.tsbtnDefVol200.Click += new EventHandler(this.changeDefaultVolume_Click);
			this.tsbtnDefVol500.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnDefVol500.ForeColor = Color.LightGray;
			this.tsbtnDefVol500.ImageTransparentColor = Color.Magenta;
			this.tsbtnDefVol500.Name = "tsbtnDefVol500";
			this.tsbtnDefVol500.Size = new Size(35, 19);
			this.tsbtnDefVol500.Text = "5000";
			this.tsbtnDefVol500.Click += new EventHandler(this.changeDefaultVolume_Click);
			this.tsbtnDefVol10K.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnDefVol10K.ForeColor = Color.LightGray;
			this.tsbtnDefVol10K.ImageTransparentColor = Color.Magenta;
			this.tsbtnDefVol10K.Name = "tsbtnDefVol10K";
			this.tsbtnDefVol10K.Size = new Size(44, 19);
			this.tsbtnDefVol10K.Text = "10,000";
			this.tsbtnDefVol10K.Click += new EventHandler(this.changeDefaultVolume_Click);
			this.tsbtnDefVol20K.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnDefVol20K.ForeColor = Color.LightGray;
			this.tsbtnDefVol20K.ImageTransparentColor = Color.Magenta;
			this.tsbtnDefVol20K.Name = "tsbtnDefVol20K";
			this.tsbtnDefVol20K.Size = new Size(44, 19);
			this.tsbtnDefVol20K.Text = "20,000";
			this.tsbtnDefVol20K.Click += new EventHandler(this.changeDefaultVolume_Click);
			this.tsbtnDefVol100K.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnDefVol100K.ForeColor = Color.LightGray;
			this.tsbtnDefVol100K.ImageTransparentColor = Color.Magenta;
			this.tsbtnDefVol100K.Name = "tsbtnDefVol100K";
			this.tsbtnDefVol100K.Size = new Size(50, 19);
			this.tsbtnDefVol100K.Text = "100,000";
			this.tsbtnDefVol100K.Click += new EventHandler(this.changeDefaultVolume_Click);
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new Size(61, 4);
			this.lbLoading.AutoSize = true;
			this.lbLoading.BackColor = Color.FromArgb(64, 64, 64);
			this.lbLoading.BorderStyle = BorderStyle.FixedSingle;
			this.lbLoading.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.lbLoading.ForeColor = Color.Yellow;
			this.lbLoading.Location = new Point(109, 254);
			this.lbLoading.Name = "lbLoading";
			this.lbLoading.Padding = new Padding(5, 3, 5, 3);
			this.lbLoading.Size = new Size(76, 23);
			this.lbLoading.TabIndex = 9;
			this.lbLoading.Text = "Loading ...";
			this.lbLoading.TextAlign = ContentAlignment.MiddleCenter;
			this.lbLoading.Visible = false;
			this.panelTitle.BackColor = Color.Black;
			this.panelTitle.Dock = DockStyle.Top;
			this.panelTitle.Location = new Point(0, 0);
			this.panelTitle.Name = "panelTitle";
			this.panelTitle.Size = new Size(331, 21);
			this.panelTitle.TabIndex = 88;
			this.panelTitle.Paint += new PaintEventHandler(this.panelTitle_Paint);
			this.sortGrid.AllowDrop = true;
			this.sortGrid.BackColor = Color.Black;
			this.sortGrid.CanBlink = true;
			this.sortGrid.CanDrag = false;
			this.sortGrid.CanGetMouseMove = false;
			columnItem.Alignment = StringAlignment.Near;
			columnItem.BackColor = Color.FromArgb(64, 64, 64);
			columnItem.ColumnAlignment = StringAlignment.Center;
			columnItem.FontColor = Color.LightGray;
			columnItem.MyStyle = FontStyle.Regular;
			columnItem.Name = "buycancel";
			columnItem.Text = "";
			columnItem.ValueFormat = FormatType.Bitmap;
			columnItem.Visible = true;
			columnItem.Width = 7;
			columnItem2.Alignment = StringAlignment.Far;
			columnItem2.BackColor = Color.FromArgb(64, 64, 64);
			columnItem2.ColumnAlignment = StringAlignment.Center;
			columnItem2.FontColor = Color.LightGray;
			columnItem2.MyStyle = FontStyle.Regular;
			columnItem2.Name = "buydeal";
			columnItem2.Text = "#";
			columnItem2.ValueFormat = FormatType.Volume;
			columnItem2.Visible = true;
			columnItem2.Width = 10;
			columnItem3.Alignment = StringAlignment.Far;
			columnItem3.BackColor = Color.FromArgb(64, 64, 64);
			columnItem3.ColumnAlignment = StringAlignment.Center;
			columnItem3.FontColor = Color.Cyan;
			columnItem3.MyStyle = FontStyle.Regular;
			columnItem3.Name = "buyvol";
			columnItem3.Text = "Buy";
			columnItem3.ValueFormat = FormatType.BidOfferVolume;
			columnItem3.Visible = true;
			columnItem3.Width = 23;
			columnItem4.Alignment = StringAlignment.Center;
			columnItem4.BackColor = Color.FromArgb(64, 64, 64);
			columnItem4.ColumnAlignment = StringAlignment.Center;
			columnItem4.FontColor = Color.LightGray;
			columnItem4.MyStyle = FontStyle.Regular;
			columnItem4.Name = "price";
			columnItem4.Text = "Price";
			columnItem4.ValueFormat = FormatType.Price;
			columnItem4.Visible = true;
			columnItem4.Width = 20;
			columnItem5.Alignment = StringAlignment.Far;
			columnItem5.BackColor = Color.FromArgb(64, 64, 64);
			columnItem5.ColumnAlignment = StringAlignment.Center;
			columnItem5.FontColor = Color.Magenta;
			columnItem5.MyStyle = FontStyle.Regular;
			columnItem5.Name = "sellvol";
			columnItem5.Text = "Sell";
			columnItem5.ValueFormat = FormatType.BidOfferVolume;
			columnItem5.Visible = true;
			columnItem5.Width = 23;
			columnItem6.Alignment = StringAlignment.Far;
			columnItem6.BackColor = Color.FromArgb(64, 64, 64);
			columnItem6.ColumnAlignment = StringAlignment.Center;
			columnItem6.FontColor = Color.LightGray;
			columnItem6.MyStyle = FontStyle.Regular;
			columnItem6.Name = "selldeal";
			columnItem6.Text = "#";
			columnItem6.ValueFormat = FormatType.Volume;
			columnItem6.Visible = true;
			columnItem6.Width = 10;
			columnItem7.Alignment = StringAlignment.Near;
			columnItem7.BackColor = Color.FromArgb(64, 64, 64);
			columnItem7.ColumnAlignment = StringAlignment.Center;
			columnItem7.FontColor = Color.LightGray;
			columnItem7.MyStyle = FontStyle.Regular;
			columnItem7.Name = "sellcancel";
			columnItem7.Text = "";
			columnItem7.ValueFormat = FormatType.Bitmap;
			columnItem7.Visible = true;
			columnItem7.Width = 7;
			this.sortGrid.Columns.Add(columnItem);
			this.sortGrid.Columns.Add(columnItem2);
			this.sortGrid.Columns.Add(columnItem3);
			this.sortGrid.Columns.Add(columnItem4);
			this.sortGrid.Columns.Add(columnItem5);
			this.sortGrid.Columns.Add(columnItem6);
			this.sortGrid.Columns.Add(columnItem7);
			this.sortGrid.CurrentScroll = 0;
			this.sortGrid.Cursor = Cursors.Hand;
			this.sortGrid.Dock = DockStyle.Fill;
			this.sortGrid.FocusItemIndex = -1;
			this.sortGrid.ForeColor = Color.Black;
			this.sortGrid.GridColor = Color.FromArgb(45, 45, 45);
			this.sortGrid.HeaderPctHeight = 80f;
			this.sortGrid.IsAutoRepaint = true;
			this.sortGrid.IsDrawFullRow = false;
			this.sortGrid.IsDrawGrid = true;
			this.sortGrid.IsDrawHeader = true;
			this.sortGrid.IsScrollable = true;
			this.sortGrid.Location = new Point(0, 95);
			this.sortGrid.MainColumn = "";
			this.sortGrid.Name = "sortGrid";
			this.sortGrid.Rows = 0;
			this.sortGrid.RowSelectColor = Color.FromArgb(64, 64, 64);
			this.sortGrid.RowSelectType = 1;
			this.sortGrid.RowsVisible = 0;
			this.sortGrid.ScrollChennelColor = Color.Gray;
			this.sortGrid.Size = new Size(331, 436);
			this.sortGrid.SortColumnName = "";
			this.sortGrid.SortType = SortType.Desc;
			this.sortGrid.TabIndex = 5;
			this.sortGrid.TableMouseClick += new SortGrid.TableMouseClickEventHandler(this.sortGrid_TableMouseClick);
			this.sortGrid.ItemDragDrop += new SortGrid.ItemDragDropEventHandler(this.sortGrid_ItemDragDrop);
			this.tsbtnRefresh.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbtnRefresh.Image = Resources.refresh;
			this.tsbtnRefresh.ImageTransparentColor = Color.Magenta;
			this.tsbtnRefresh.Name = "tsbtnRefresh";
			this.tsbtnRefresh.Size = new Size(23, 20);
			this.tsbtnRefresh.Text = "toolStripButton2";
			this.tsbtnRefresh.Click += new EventHandler(this.tsbtnRefresh_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.lbLoading);
			base.Controls.Add(this.sortGrid);
			base.Controls.Add(this.toolStrip2);
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.panelTitle);
			base.Name = "ucSmart1Click";
			base.Size = new Size(331, 531);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
