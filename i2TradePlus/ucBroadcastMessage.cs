using i2TradePlus.Information;
using i2TradePlus.Properties;
using ITSNet.Common.BIZ;
using ITSNet.Common.BIZ.RealtimeMessage;
using ITSNet.Common.BIZ.RealtimeMessage.TFEX;
using STIControl;
using STIControl.SortTableGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace i2TradePlus
{
	internal class ucBroadcastMessage : UserControl, IRealtimeMessage
	{
		private struct RecordData
		{
			public long rowID;
			public string side;
			public string messageText;
			public string messageTime;
			public string messageType;
			public string stockSymbol;
			public string account;
			public int priority;
			public string url;
		}
		private delegate void UpdateRowCallback(ucBroadcastMessage.RecordData record, Color color, bool showTime);
		private delegate void ShowSplashCallBack(bool isLoading);
		private IContainer components = null;
		private Panel panelDetail;
		private Button btnClose;
		private TextBox txtMessage;
		private Label lbLoading;
		private SortGrid intza1;
		private ContextMenuStrip contextMenuStrip2;
		private ToolStripMenuItem toolStripMenuItem1;
		private int currentTop = 20;
		private BackgroundWorker bgwReloadData = null;
		private DataSet tdsBroadcast = null;
		private bool _isLoading = false;
		private bool _isFirstOpen = true;
		public bool IsFirstOpen
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this._isFirstOpen;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this._isFirstOpen = value;
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
			ColumnItem columnItem = new ColumnItem();
			ColumnItem columnItem2 = new ColumnItem();
			this.panelDetail = new Panel();
			this.btnClose = new Button();
			this.txtMessage = new TextBox();
			this.lbLoading = new Label();
			this.intza1 = new SortGrid();
			this.contextMenuStrip2 = new ContextMenuStrip(this.components);
			this.toolStripMenuItem1 = new ToolStripMenuItem();
			this.panelDetail.SuspendLayout();
			this.contextMenuStrip2.SuspendLayout();
			base.SuspendLayout();
			this.panelDetail.BackColor = Color.Black;
			this.panelDetail.Controls.Add(this.btnClose);
			this.panelDetail.Controls.Add(this.txtMessage);
			this.panelDetail.Location = new Point(-1, 54);
			this.panelDetail.Name = "panelDetail";
			this.panelDetail.Size = new Size(438, 86);
			this.panelDetail.TabIndex = 22;
			this.panelDetail.TabStop = true;
			this.panelDetail.Visible = false;
			this.btnClose.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.btnClose.Font = new Font("Wingdings", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 2);
			this.btnClose.Location = new Point(414, 1);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new Size(24, 23);
			this.btnClose.TabIndex = 1;
			this.btnClose.TabStop = false;
			this.btnClose.Text = "x";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new EventHandler(this.btnClose_Click);
			this.txtMessage.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.txtMessage.BackColor = Color.FromArgb(30, 30, 30);
			this.txtMessage.BorderStyle = BorderStyle.None;
			this.txtMessage.ForeColor = Color.Yellow;
			this.txtMessage.Location = new Point(0, 0);
			this.txtMessage.MaxLength = 500;
			this.txtMessage.Multiline = true;
			this.txtMessage.Name = "txtMessage";
			this.txtMessage.ReadOnly = true;
			this.txtMessage.Size = new Size(412, 86);
			this.txtMessage.TabIndex = 0;
			this.txtMessage.TabStop = false;
			this.txtMessage.Enter += new EventHandler(this.txtMessage_Enter);
			this.lbLoading.AutoSize = true;
			this.lbLoading.BackColor = Color.FromArgb(64, 64, 64);
			this.lbLoading.BorderStyle = BorderStyle.FixedSingle;
			this.lbLoading.ForeColor = Color.Yellow;
			this.lbLoading.Location = new Point(262, 79);
			this.lbLoading.Name = "lbLoading";
			this.lbLoading.Padding = new Padding(5, 3, 5, 3);
			this.lbLoading.Size = new Size(69, 21);
			this.lbLoading.TabIndex = 62;
			this.lbLoading.Text = "Loading ...";
			this.lbLoading.Visible = false;
			this.intza1.AllowDrop = true;
			this.intza1.BackColor = Color.FromArgb(10, 10, 10);
			this.intza1.CanBlink = false;
			this.intza1.CanDrag = false;
			this.intza1.CanGetMouseMove = true;
			columnItem.Alignment = StringAlignment.Near;
			columnItem.BackColor = Color.FromArgb(64, 64, 64);
			columnItem.ColumnAlignment = StringAlignment.Center;
			columnItem.FontColor = Color.LightGray;
			columnItem.MyStyle = FontStyle.Regular;
			columnItem.Name = "message";
			columnItem.Text = "None";
			columnItem.ValueFormat = FormatType.Text;
			columnItem.Visible = true;
			columnItem.Width = 100;
			columnItem2.Alignment = StringAlignment.Near;
			columnItem2.BackColor = Color.FromArgb(64, 64, 64);
			columnItem2.ColumnAlignment = StringAlignment.Center;
			columnItem2.FontColor = Color.LightGray;
			columnItem2.MyStyle = FontStyle.Regular;
			columnItem2.Name = "url";
			columnItem2.Text = "None";
			columnItem2.ValueFormat = FormatType.Text;
			columnItem2.Visible = false;
			columnItem2.Width = 10;
			this.intza1.Columns.Add(columnItem);
			this.intza1.Columns.Add(columnItem2);
			this.intza1.CurrentScroll = 0;
			this.intza1.FocusItemIndex = -1;
			this.intza1.ForeColor = Color.Black;
			this.intza1.GridColor = Color.FromArgb(45, 45, 45);
			this.intza1.HeaderPctHeight = 100f;
			this.intza1.IsAutoRepaint = true;
			this.intza1.IsDrawFullRow = true;
			this.intza1.IsDrawGrid = false;
			this.intza1.IsDrawHeader = false;
			this.intza1.IsScrollable = true;
			this.intza1.Location = new Point(1, 1);
			this.intza1.MainColumn = "";
			this.intza1.Name = "intza1";
			this.intza1.Rows = 0;
			this.intza1.RowSelectColor = Color.FromArgb(64, 64, 64);
			this.intza1.RowSelectType = 0;
			this.intza1.RowsVisible = 0;
			this.intza1.ScrollChennelColor = Color.Gray;
			this.intza1.Size = new Size(436, 53);
			this.intza1.SortColumnName = "";
			this.intza1.SortType = SortType.Desc;
			this.intza1.TabIndex = 63;
			this.intza1.MouseClick += new MouseEventHandler(this.intza1_MouseClick);
			this.intza1.TableMouseClick += new SortGrid.TableMouseClickEventHandler(this.intzaTableGrid1_TableMouseClick);
			this.intza1.TableMouseDoubleClick += new SortGrid.TableMouseDoubleClickEventHandler(this.intzaTableGrid1_TableMouseDoubleClick);
			this.contextMenuStrip2.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripMenuItem1
			});
			this.contextMenuStrip2.Name = "contextMenuStrip1";
			this.contextMenuStrip2.Size = new Size(153, 48);
			this.toolStripMenuItem1.Image = Resources.refresh;
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new Size(152, 22);
			this.toolStripMenuItem1.Text = "Reload";
			this.toolStripMenuItem1.Click += new EventHandler(this.toolStripMenuItem1_Click);
			this.BackColor = Color.DimGray;
			base.Controls.Add(this.lbLoading);
			base.Controls.Add(this.intza1);
			base.Controls.Add(this.panelDetail);
			base.Margin = new Padding(1);
			base.Name = "ucBroadcastMessage";
			base.Padding = new Padding(1);
			base.Size = new Size(439, 141);
			this.panelDetail.ResumeLayout(false);
			this.panelDetail.PerformLayout();
			this.contextMenuStrip2.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ucBroadcastMessage()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
			base.UpdateStyles();
			if (!base.DesignMode)
			{
				this.bgwReloadData = new BackgroundWorker();
				this.bgwReloadData.WorkerReportsProgress = true;
				this.bgwReloadData.DoWork += new DoWorkEventHandler(this.bgwReloadData_DoWork);
				this.bgwReloadData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwReloadData_RunWorkerCompleted);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwReloadData_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				this.ShowSplash(true);
				string text = string.Empty;
				foreach (KeyValuePair<string, AccountInfo.ItemInfo> current in ApplicationInfo.AccInfo.Items)
				{
					text = text + ",'" + current.Key + "'";
				}
				if (text != string.Empty)
				{
					text = text.Substring(1);
				}
				string text2 = string.Empty;
				text2 = ApplicationInfo.WebService.BoardcastMessage(text, this.currentTop);
				if (this.tdsBroadcast == null)
				{
					this.tdsBroadcast = new DataSet();
				}
				else
				{
					this.tdsBroadcast.Clear();
				}
				if (!string.IsNullOrEmpty(text2))
				{
					MyDataHelper.StringToDataSet(text2, this.tdsBroadcast);
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
				this.UpdateToControl();
				this.tdsBroadcast.Clear();
			}
			catch (Exception ex)
			{
				this.ShowError("BroadcastMessage:RunWorkerCompleted", ex);
			}
			this.ShowSplash(false);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReceiveMessage(IBroadcastMessage message, StockList.StockInformation realtimeStockInfo)
		{
			if (!this._isLoading)
			{
				try
				{
					if (message.MessageType == "B+")
					{
						BCTextMessage bCTextMessage = (BCTextMessage)message;
						if (bCTextMessage.BcMessageType != "SS")
						{
							this.UpdateToGrid(new ucBroadcastMessage.RecordData
							{
								messageTime = Utilities.GetShortTime(bCTextMessage.MessageTime),
								messageText = bCTextMessage.MessageText,
								stockSymbol = bCTextMessage.SecuritySymbol,
								messageType = bCTextMessage.BcMessageType
							}, Color.White, true);
							this.intza1.Redraw();
						}
					}
					else
					{
						if (message.MessageType == "0B")
						{
							BroadCastOrderMessageClient broadCastOrderMessageClient = (BroadCastOrderMessageClient)message;
							if (ApplicationInfo.CanReceiveMessage(broadCastOrderMessageClient.EntryID))
							{
								this.UpdateToGrid(new ucBroadcastMessage.RecordData
								{
									messageType = broadCastOrderMessageClient.OriginalMessageType,
									stockSymbol = "",
									messageTime = broadCastOrderMessageClient.Time.ToString("HH:mm"),
									messageText = broadCastOrderMessageClient.Content,
									side = broadCastOrderMessageClient.Side
								}, Color.Red, true);
								this.intza1.Redraw();
							}
						}
						else
						{
                            //if (message.MessageType == "0I")
                            //{
                            //    Color color = Color.White;
                            //    OrderInfoClient orderInfoClient = (OrderInfoClient)message;
                            //    if (ApplicationInfo.CanReceiveMessage(orderInfoClient.Account))
                            //    {
                            //        string text = string.Empty;
                            //        string originalMessageType = orderInfoClient.OriginalMessageType;
                            //        if (originalMessageType != null)
                            //        {
                            //            if (<PrivateImplementationDetails>{0480AB1A-2E71-4BDC-B8BC-63D777CD5366}.$$method0x6000008-1 == null)
                            //            {
                            //                <PrivateImplementationDetails>{0480AB1A-2E71-4BDC-B8BC-63D777CD5366}.$$method0x6000008-1 = new Dictionary<string, int>(7)
                            //                {

                            //                    {
                            //                        "2G",
                            //                        0
                            //                    },

                            //                    {
                            //                        "2E",
                            //                        1
                            //                    },

                            //                    {
                            //                        "2I",
                            //                        2
                            //                    },

                            //                    {
                            //                        "1C",
                            //                        3
                            //                    },

                            //                    {
                            //                        "2C",
                            //                        4
                            //                    },

                            //                    {
                            //                        "1I",
                            //                        5
                            //                    },

                            //                    {
                            //                        "2D",
                            //                        6
                            //                    }
                            //                };
                            //            }
                            //            int num;
                            //            if (<PrivateImplementationDetails>{0480AB1A-2E71-4BDC-B8BC-63D777CD5366}.$$method0x6000008-1.TryGetValue(originalMessageType, out num))
                            //            {
                            //                switch (num)
                            //                {
                            //                case 0:
                            //                    text = string.Concat(new object[]
                            //                    {
                            //                        "Request fail #",
                            //                        orderInfoClient.OrderNumber,
                            //                        ", ",
                            //                        Utilities.GetOrderSideName(orderInfoClient.Side),
                            //                        "  ",
                            //                        orderInfoClient.Stock,
                            //                        ", Vol ",
                            //                        FormatUtil.VolumeFormat(orderInfoClient.Volume, false),
                            //                        ", Price ",
                            //                        Utilities.PriceFormat(orderInfoClient.PriceToSET),
                            //                        ", Acc ",
                            //                        orderInfoClient.Account
                            //                    });
                            //                    color = Color.Yellow;
                            //                    break;
                            //                case 1:
                            //                case 2:
                            //                    if (orderInfoClient.OriginalMessageType == "2I")
                            //                    {
                            //                        text = "Crossing Deal ";
                            //                    }
                            //                    text = string.Concat(new object[]
                            //                    {
                            //                        text,
                            //                        "Matched ",
                            //                        FormatUtil.VolumeFormat(orderInfoClient.LastVolumeInCase, true),
                            //                        "  @",
                            //                        Utilities.PriceFormat(orderInfoClient.LastPriceInCase),
                            //                        "  Order# ",
                            //                        orderInfoClient.OrderNumber,
                            //                        "  ",
                            //                        orderInfoClient.Side,
                            //                        "  ",
                            //                        orderInfoClient.Stock,
                            //                        "  ",
                            //                        FormatUtil.VolumeFormat(orderInfoClient.Volume, true),
                            //                        "  @",
                            //                        Utilities.PriceFormat(orderInfoClient.PriceToSET),
                            //                        "  ",
                            //                        "  Account ",
                            //                        orderInfoClient.Account,
                            //                        "  Leave ",
                            //                        (orderInfoClient.Volume - orderInfoClient.MatchedVolume).ToString("#,##0")
                            //                    });
                            //                    if (orderInfoClient.Side == "B")
                            //                    {
                            //                        color = Color.Lime;
                            //                    }
                            //                    else
                            //                    {
                            //                        if (orderInfoClient.Side == "S")
                            //                        {
                            //                            color = Color.Red;
                            //                        }
                            //                        else
                            //                        {
                            //                            if (orderInfoClient.Side == "C")
                            //                            {
                            //                                color = Color.Cyan;
                            //                            }
                            //                            else
                            //                            {
                            //                                if (orderInfoClient.Side == "H")
                            //                                {
                            //                                    color = Color.Magenta;
                            //                                }
                            //                                else
                            //                                {
                            //                                    color = Color.Yellow;
                            //                                }
                            //                            }
                            //                        }
                            //                    }
                            //                    break;
                            //                case 3:
                            //                case 4:
                            //                    if (orderInfoClient.Status == "X")
                            //                    {
                            //                        text = string.Concat(new object[]
                            //                        {
                            //                            "Cancel Order# ",
                            //                            orderInfoClient.OrderNumber,
                            //                            "  ",
                            //                            orderInfoClient.Side,
                            //                            "  ",
                            //                            orderInfoClient.Stock,
                            //                            "  ",
                            //                            FormatUtil.VolumeFormat(orderInfoClient.LastVolumeInCase, false),
                            //                            "  @",
                            //                            Utilities.PriceFormat(orderInfoClient.LastPriceInCase),
                            //                            "  Account ",
                            //                            orderInfoClient.Account,
                            //                            "  Leave ",
                            //                            orderInfoClient.LastVolumeInCase
                            //                        });
                            //                    }
                            //                    else
                            //                    {
                            //                        if (!(orderInfoClient.Status == "C"))
                            //                        {
                            //                            return;
                            //                        }
                            //                        text = string.Concat(new object[]
                            //                        {
                            //                            "Cancel from SET Order# ",
                            //                            orderInfoClient.OrderNumber,
                            //                            "  ",
                            //                            orderInfoClient.Side,
                            //                            "  ",
                            //                            orderInfoClient.Stock,
                            //                            "  ",
                            //                            FormatUtil.VolumeFormat(orderInfoClient.LastVolumeInCase, false),
                            //                            "  @",
                            //                            Utilities.PriceFormat(orderInfoClient.LastPriceInCase),
                            //                            "  Account ",
                            //                            orderInfoClient.Account,
                            //                            "  Leave ",
                            //                            orderInfoClient.LastVolumeInCase
                            //                        });
                            //                    }
                            //                    color = Color.Yellow;
                            //                    break;
                            //                case 5:
                            //                    break;
                            //                case 6:
                            //                    text = string.Concat(new object[]
                            //                    {
                            //                        "Change Order Confirm ",
                            //                        ",  Order# ",
                            //                        orderInfoClient.OrderNumber,
                            //                        ",  Account ",
                            //                        orderInfoClient.Account
                            //                    });
                            //                    color = Color.Orange;
                            //                    break;
                            //                default:
                            //                    goto IL_746;
                            //                }
                            //                if (text != string.Empty)
                            //                {
                            //                    this.UpdateToGrid(new ucBroadcastMessage.RecordData
                            //                    {
                            //                        messageType = orderInfoClient.OriginalMessageType,
                            //                        stockSymbol = string.Empty,
                            //                        messageTime = DateTime.Now.ToString("HH:mm"),
                            //                        messageText = text,
                            //                        side = orderInfoClient.Side
                            //                    }, color, true);
                            //                    this.intza1.Redraw();
                            //                }
                            //            }
                            //        }
                            //        IL_746:;
                            //    }
                            //}
                            //else
                            //{
                            //    if (message.MessageType == "BA")
                            //    {
                            //        BAMessage bAMessage = (BAMessage)message;
                            //        if (bAMessage.AccountID == string.Empty || ApplicationInfo.AccInfo.Items.ContainsKey(bAMessage.AccountID))
                            //        {
                            //            this.UpdateToGrid(new ucBroadcastMessage.RecordData
                            //            {
                            //                stockSymbol = string.Empty,
                            //                messageText = bAMessage.MessageText,
                            //                account = bAMessage.AccountID,
                            //                messageType = "BA",
                            //                url = bAMessage.HyperLink,
                            //                priority = bAMessage.Priorities,
                            //                messageTime = bAMessage.MessageTime.ToString("HH:mm")
                            //            }, Color.White, true);
                            //            this.intza1.Redraw();
                            //        }
                            //    }
                            //}
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
			if (!this._isLoading)
			{
				try
				{
					if (message.MessageType == "BC")
					{
						BCMessageTFEX bCMessageTFEX = (BCMessageTFEX)message;
						this.UpdateToGrid(new ucBroadcastMessage.RecordData
						{
							messageTime = Utilities.GetShortTime(bCMessageTFEX.MessageTime),
							messageText = bCMessageTFEX.MessageText,
							messageType = bCMessageTFEX.MessageType
						}, Color.White, true);
						this.intza1.EndUpdate();
						if (this.intza1.Rows > 0)
						{
							this.intza1.SetFocusItem(0);
						}
					}
					else
					{
						if (message.MessageType == "#T9I")
						{
							OrderTFEXInfoClient orderTFEXInfoClient = (OrderTFEXInfoClient)message;
							if (ApplicationInfo.CanReceiveMessage(orderTFEXInfoClient.Account))
							{
								string text = string.Empty;
								Color color = Color.White;
								string text2 = string.Empty;
								if (orderTFEXInfoClient.Side == "1")
								{
									text2 = "Long";
								}
								else
								{
									if (orderTFEXInfoClient.Side == "2")
									{
										text2 = "Short";
									}
								}
								string originalMessageType = orderTFEXInfoClient.OriginalMessageType;
								if (originalMessageType != null)
								{
									if (!(originalMessageType == "2C"))
									{
										if (!(originalMessageType == "2E"))
										{
											if (originalMessageType == "2G")
											{
												text = string.Concat(new object[]
												{
													"Reject ",
													text2,
													" #",
													orderTFEXInfoClient.OrderNumber,
													"  ",
													orderTFEXInfoClient.Series,
													"  Volume ",
													FormatUtil.VolumeFormat(orderTFEXInfoClient.Volume, false),
													"  @",
													Utilities.PriceFormat(orderTFEXInfoClient.Price)
												});
												if (!string.IsNullOrEmpty(orderTFEXInfoClient.RejectDescription))
												{
													text = text + " Reject Reason : " + orderTFEXInfoClient.RejectDescription;
												}
												color = Color.Red;
											}
										}
										else
										{
											text = string.Concat(new object[]
											{
												text,
												"Matched #",
												orderTFEXInfoClient.OrderNumber,
												"  ",
												text2,
												"  ",
												orderTFEXInfoClient.Series,
												"  @",
												Utilities.PriceFormat(orderTFEXInfoClient.LastPriceInCase),
												"  Volume ",
												FormatUtil.VolumeFormat(orderTFEXInfoClient.LastVolumeInCase, true),
												"  Leave ",
												orderTFEXInfoClient.Volume - orderTFEXInfoClient.MatchedVolume
											});
											if (orderTFEXInfoClient.Side == "1")
											{
												color = Color.Cyan;
											}
											else
											{
												if (orderTFEXInfoClient.Side == "2")
												{
													color = Color.Magenta;
												}
											}
										}
									}
									else
									{
										if (orderTFEXInfoClient.Status == "X")
										{
											text = string.Concat(new object[]
											{
												"Canceled #",
												orderTFEXInfoClient.OrderNumber,
												"   ",
												text2,
												"  ",
												orderTFEXInfoClient.Series,
												"  Volume ",
												orderTFEXInfoClient.Volume - orderTFEXInfoClient.MatchedVolume,
												"  @",
												Utilities.PriceFormat(orderTFEXInfoClient.Price)
											});
											color = Color.Yellow;
										}
									}
								}
								if (text != string.Empty)
								{
									this.UpdateToGrid(new ucBroadcastMessage.RecordData
									{
										messageType = orderTFEXInfoClient.OriginalMessageType,
										stockSymbol = string.Empty,
										messageTime = Utilities.GetShortTime(DateTime.Now.ToString()),
										messageText = text
									}, color, true);
									this.intza1.EndUpdate();
									if (this.intza1.Rows > 0)
									{
										this.intza1.SetFocusItem(0);
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
		private void UpdateToGrid(ucBroadcastMessage.RecordData record, Color color, bool showTime)
		{
			if (this.intza1.InvokeRequired)
			{
				this.intza1.Invoke(new ucBroadcastMessage.UpdateRowCallback(this.UpdateToGrid), new object[]
				{
					record,
					color,
					showTime
				});
			}
			else
			{
				try
				{
					string text = string.Empty;
					if (showTime)
					{
						text = record.messageTime;
						if (record.messageType == "BA")
						{
							if (record.account != string.Empty)
							{
								text += "  * ";
							}
							else
							{
								text += "  ";
							}
							text += record.messageText;
						}
						else
						{
							if (record.messageType == "PA")
							{
								text = string.Concat(new string[]
								{
									text,
									"  ",
									record.account,
									" , ",
									record.messageText
								});
							}
							else
							{
								if (record.messageType == "SA")
								{
									text = text + "  " + record.messageText;
								}
								else
								{
									if (record.messageType == "RF")
									{
										text = text + "  " + record.messageText;
									}
									else
									{
										if (!string.IsNullOrEmpty(record.stockSymbol))
										{
											text = string.Concat(new string[]
											{
												text,
												"  ",
												record.stockSymbol,
												" : ",
												record.messageText
											});
										}
										else
										{
											text = text + "  " + record.messageText;
										}
									}
								}
							}
						}
					}
					else
					{
						text = record.messageText;
					}
					text = text.Trim();
					RecordItem recordItem = this.intza1.AddRecord(1, false);
					switch (record.priority)
					{
					case 1:
						recordItem.Fields("message").FontColor = Color.Magenta;
						break;
					case 2:
						recordItem.Fields("message").FontColor = Color.Cyan;
						break;
					case 3:
						recordItem.Fields("message").FontColor = Color.Yellow;
						break;
					default:
						recordItem.Fields("message").FontColor = color;
						break;
					}
					if (!string.IsNullOrEmpty(record.url))
					{
						recordItem.Fields("url").Text = record.url;
						recordItem.Fields("message").FontStyle = FontStyle.Underline;
						text += " <Click>";
					}
					recordItem.Fields("message").Text = text;
					if (record.messageType == "PA" || record.messageType == "SA")
					{
						recordItem.BackColor = Color.DarkRed;
					}
					else
					{
						if (record.messageType == "RF")
						{
							recordItem.BackColor = Color.DarkRed;
							recordItem.Fields("message").FontColor = Color.White;
						}
					}
					recordItem.Changed = true;
				}
				catch (Exception ex)
				{
					this.ShowError("UpdateToGrid", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReloadData()
		{
			if (this._isFirstOpen)
			{
				this._isFirstOpen = false;
				if (!this.bgwReloadData.IsBusy)
				{
					this.bgwReloadData.RunWorkerAsync();
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowSplash(bool isLoading)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new ucBroadcastMessage.ShowSplashCallBack(this.ShowSplash), new object[]
				{
					isLoading
				});
			}
			else
			{
				if (ApplicationInfo.SuuportSplash == "Y")
				{
					this._isLoading = isLoading;
					if (this._isLoading)
					{
						this.lbLoading.Left = (base.Width - this.lbLoading.Width) / 2;
						this.lbLoading.Top = (base.Height - this.lbLoading.Height) / 2;
						this.lbLoading.Visible = true;
						this.lbLoading.BringToFront();
					}
					else
					{
						this.lbLoading.Visible = false;
					}
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateToControl()
		{
			if (this.intza1.InvokeRequired)
			{
				this.intza1.Invoke(new MethodInvoker(this.UpdateToControl));
			}
			else
			{
				try
				{
					this.intza1.BeginUpdate();
					this.intza1.ClearAllText();
					if (this.tdsBroadcast != null)
					{
						List<ucBroadcastMessage.RecordData> list = new List<ucBroadcastMessage.RecordData>();
						if (this.tdsBroadcast.Tables.Contains("broadcast_message"))
						{
							for (int i = this.tdsBroadcast.Tables["broadcast_message"].Rows.Count - 1; i >= 0; i--)
							{
								ucBroadcastMessage.RecordData item = default(ucBroadcastMessage.RecordData);
								DataRow dataRow = this.tdsBroadcast.Tables["broadcast_message"].Rows[i];
								item.rowID = (long)Convert.ToInt32(dataRow["row_id"]);
								item.stockSymbol = dataRow["security_symbol"].ToString();
								item.messageText = dataRow["message"].ToString();
								item.messageType = dataRow["message_type"].ToString();
								DateTime dateTime;
								DateTime.TryParse(dataRow["last_update"].ToString(), out dateTime);
								item.messageTime = dateTime.ToString("HH:mm");
								item.url = string.Empty;
								item.priority = 0;
								item.account = string.Empty;
								list.Add(item);
							}
						}
						if (this.tdsBroadcast.Tables.Contains("ba"))
						{
							for (int i = this.tdsBroadcast.Tables["ba"].Rows.Count - 1; i >= 0; i--)
							{
								ucBroadcastMessage.RecordData item = default(ucBroadcastMessage.RecordData);
								DataRow dataRow = this.tdsBroadcast.Tables["ba"].Rows[i];
								item.rowID = (long)Convert.ToInt32(dataRow["row_id"]);
								item.stockSymbol = string.Empty;
								item.messageText = dataRow["message"].ToString();
								item.account = dataRow["customer_id"].ToString();
								item.messageType = "BA";
								item.url = dataRow["url"].ToString();
								int.TryParse(dataRow["priorities_id"].ToString(), out item.priority);
								DateTime dateTime;
								DateTime.TryParse(dataRow["last_update"].ToString(), out dateTime);
								item.messageTime = dateTime.ToString("HH:mm");
								list.Add(item);
							}
						}
						list.Sort(new Comparison<ucBroadcastMessage.RecordData>(this.SortByDesc));
						this.intza1.Rows = 0;
						foreach (ucBroadcastMessage.RecordData current in list)
						{
							this.UpdateToGrid(current, Color.White, true);
						}
						list.Clear();
						list = null;
					}
					this.intza1.Redraw();
				}
				catch (Exception ex)
				{
					this.intza1.Redraw();
					this.ShowError("UpdateToControl", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private int SortByDesc(ucBroadcastMessage.RecordData r1, ucBroadcastMessage.RecordData r2)
		{
			int result;
			try
			{
				if (r1.messageTime == r2.messageTime)
				{
					if (!string.IsNullOrEmpty(r1.messageTime))
					{
						if (r1.rowID == r2.rowID)
						{
							result = 0;
						}
						else
						{
							result = -1;
						}
					}
					else
					{
						if (string.IsNullOrEmpty(r1.messageTime))
						{
							if (r1.rowID == r2.rowID)
							{
								result = 0;
							}
							else
							{
								result = -1;
							}
						}
						else
						{
							result = -1;
						}
					}
				}
				else
				{
					if (string.IsNullOrEmpty(r2.messageTime) && !string.IsNullOrEmpty(r1.messageTime))
					{
						result = -1;
					}
					else
					{
						if (string.Compare(r1.messageTime, r2.messageTime) > 0)
						{
							result = 1;
						}
						else
						{
							result = -1;
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetResize()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(this.SetResize));
			}
			else
			{
				try
				{
					this.intza1.Font = Settings.Default.Default_Font;
					this.intza1.RowSelectType = 2;
					this.intza1.SetBounds(0, 0, base.ClientSize.Width, base.ClientSize.Height);
					this.panelDetail.SetBounds(0, 0, base.Width, base.Height);
				}
				catch (Exception ex)
				{
					this.ShowError("SetResize", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected virtual void ShowError(string functionName, Exception ex)
		{
			ExceptionManager.Show(new ErrorItem(DateTime.Now, base.Name, functionName, ex.Message, ex.ToString()));
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaTableGrid1_TableMouseClick(object sender, TableMouseEventArgs e)
		{
			if (e.Mouse.Button == MouseButtons.Left)
			{
				this.CallLinkBox(e.RowIndex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void CallLinkBox(int rowId)
		{
			try
			{
				if (rowId >= 0)
				{
					if (!string.IsNullOrEmpty(this.intza1.Records(rowId).Fields("url").Text.ToString()))
					{
						this.ExcuteFile("C:\\Program Files\\Internet Explorer\\iexplore.exe", this.intza1.Records(rowId).Fields("url").Text.ToString());
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("Open News Website", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ExcuteFile(string fileName, string param)
		{
			try
			{
				new Process
				{
					StartInfo = 
					{
						FileName = fileName,
						Arguments = param,
						UseShellExecute = true,
						RedirectStandardOutput = false
					}
				}.Start();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaTableGrid1_TableMouseDoubleClick(object sender, TableMouseEventArgs e)
		{
			if (e.Mouse.Button == MouseButtons.Left)
			{
				this.CallDetailBox(e.RowIndex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void CallDetailBox(int rowId)
		{
			try
			{
				if (rowId >= 0)
				{
					this.panelDetail.SetBounds(0, 0, base.Width, base.Height);
					this.txtMessage.Text = this.intza1.Records(rowId).Fields("message").Text.ToString();
					this.txtMessage.ForeColor = this.intza1.Records(rowId).Fields("message").FontColor;
					this.panelDetail.Show();
					this.panelDetail.BringToFront();
					this.btnClose.Focus();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("CallDetailBox", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnClose_Click(object sender, EventArgs e)
		{
			this.panelDetail.Hide();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void txtMessage_Enter(object sender, EventArgs e)
		{
			this.btnClose.Focus();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaTableGrid1_TableKeyDown(KeyEventArgs e)
		{
			try
			{
				if (e.KeyCode == Keys.Return)
				{
					if (e.Shift)
					{
						this.CallLinkBox(this.intza1.FocusItemIndex);
					}
					else
					{
						this.CallDetailBox(this.intza1.FocusItemIndex);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("intzaTableGrid1_TableKeyDown", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaTableGrid1_TableMouseMove(TableMouseEventArgs e)
		{
			try
			{
				if (e.RowIndex >= 0)
				{
					if (!string.IsNullOrEmpty(this.intza1.Records(e.RowIndex).Fields("url").Text.ToString()))
					{
						this.Cursor = Cursors.Hand;
					}
					else
					{
						this.Cursor = Cursors.Default;
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("Open News Website", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsmRefresh_Click(object sender, EventArgs e)
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intza1_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				this.contextMenuStrip2.Show(this.intza1, e.X, e.Y);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			this._isFirstOpen = true;
			this.ReloadData();
		}
	}
}
