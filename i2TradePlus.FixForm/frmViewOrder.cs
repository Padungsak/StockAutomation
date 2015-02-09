using i2TradePlus.Information;
using ITSNet.Common.BIZ;
using STIControl;
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
	public class frmViewOrder : ClientBaseForm, IRealtimeMessage
	{
		private IContainer components = null;
		private ucViewOrder ViewOrderBox;
		private SortGrid intzaSum;
		private ToolStrip tStripTop;
		private ToolStripButton tsbtnViewByStock;
		private ToolStripButton tsbtnViewTransaction;
		private ToolStripSeparator tsripViewOrderByStock;
		private ucOrderStat OrderStatBox;
		private ToolStripSeparator tStripSep2;
		private ToolStripButton tsbtnStopOrder;
		private ToolStripSeparator tStripSep3;
		private ToolStripButton tsbtnReOrder;
		private int _currentPage = 0;
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
			ColumnItem columnItem = new ColumnItem();
			ColumnItem columnItem2 = new ColumnItem();
			ColumnItem columnItem3 = new ColumnItem();
			ColumnItem columnItem4 = new ColumnItem();
			ColumnItem columnItem5 = new ColumnItem();
			this.ViewOrderBox = new ucViewOrder();
			this.intzaSum = new SortGrid();
			this.tStripTop = new ToolStrip();
			this.tsbtnViewTransaction = new ToolStripButton();
			this.tsripViewOrderByStock = new ToolStripSeparator();
			this.tsbtnViewByStock = new ToolStripButton();
			this.tStripSep2 = new ToolStripSeparator();
			this.tsbtnStopOrder = new ToolStripButton();
			this.tStripSep3 = new ToolStripSeparator();
			this.tsbtnReOrder = new ToolStripButton();
			this.OrderStatBox = new ucOrderStat();
			this.tStripTop.SuspendLayout();
			base.SuspendLayout();
			this.ViewOrderBox.BackColor = Color.FromArgb(64, 64, 64);
			this.ViewOrderBox.Font = new Font("Microsoft Sans Serif", 8.25f);
			this.ViewOrderBox.IsActive = false;
			this.ViewOrderBox.IsLoadingData = false;
			this.ViewOrderBox.IsShowLoadingControl = false;
			this.ViewOrderBox.IsShowNextPage = false;
			this.ViewOrderBox.IsShowToolsBar = true;
			this.ViewOrderBox.Location = new Point(0, 36);
			this.ViewOrderBox.Margin = new Padding(0);
			this.ViewOrderBox.Name = "ViewOrderBox";
			this.ViewOrderBox.ShowOnMainForm = false;
			this.ViewOrderBox.Size = new Size(783, 146);
			this.ViewOrderBox.TabIndex = 8;
			this.ViewOrderBox.OnDisplaySummaryOrders += new ucViewOrder.OnDisplaySummaryOrdersHandler(this.ucViewOrder_OnDisplaySummaryOrders);
			this.intzaSum.AllowDrop = true;
			this.intzaSum.BackColor = Color.FromArgb(10, 10, 10);
			this.intzaSum.CanBlink = true;
			this.intzaSum.CanDrag = false;
			this.intzaSum.CanGetMouseMove = false;
			columnItem.Alignment = StringAlignment.Near;
			columnItem.BackColor = Color.FromArgb(64, 64, 64);
			columnItem.ColumnAlignment = StringAlignment.Center;
			columnItem.FontColor = Color.LightGray;
			columnItem.MyStyle = FontStyle.Regular;
			columnItem.Name = "sum";
			columnItem.Text = "Summary";
			columnItem.ValueFormat = FormatType.Text;
			columnItem.Visible = true;
			columnItem.Width = 28;
			columnItem2.Alignment = StringAlignment.Far;
			columnItem2.BackColor = Color.FromArgb(64, 64, 64);
			columnItem2.ColumnAlignment = StringAlignment.Center;
			columnItem2.FontColor = Color.LightGray;
			columnItem2.MyStyle = FontStyle.Regular;
			columnItem2.Name = "volume";
			columnItem2.Text = "Volume";
			columnItem2.ValueFormat = FormatType.Volume;
			columnItem2.Visible = true;
			columnItem2.Width = 18;
			columnItem3.Alignment = StringAlignment.Far;
			columnItem3.BackColor = Color.FromArgb(64, 64, 64);
			columnItem3.ColumnAlignment = StringAlignment.Center;
			columnItem3.FontColor = Color.LightGray;
			columnItem3.MyStyle = FontStyle.Regular;
			columnItem3.Name = "unmatch";
			columnItem3.Text = "UnMatch Volume";
			columnItem3.ValueFormat = FormatType.Volume;
			columnItem3.Visible = true;
			columnItem3.Width = 18;
			columnItem4.Alignment = StringAlignment.Far;
			columnItem4.BackColor = Color.FromArgb(64, 64, 64);
			columnItem4.ColumnAlignment = StringAlignment.Center;
			columnItem4.FontColor = Color.LightGray;
			columnItem4.MyStyle = FontStyle.Regular;
			columnItem4.Name = "mvolume";
			columnItem4.Text = "Matched Volume";
			columnItem4.ValueFormat = FormatType.Volume;
			columnItem4.Visible = true;
			columnItem4.Width = 18;
			columnItem5.Alignment = StringAlignment.Far;
			columnItem5.BackColor = Color.FromArgb(64, 64, 64);
			columnItem5.ColumnAlignment = StringAlignment.Center;
			columnItem5.FontColor = Color.LightGray;
			columnItem5.MyStyle = FontStyle.Regular;
			columnItem5.Name = "mvalue";
			columnItem5.Text = "Matched Value";
			columnItem5.ValueFormat = FormatType.Price;
			columnItem5.Visible = true;
			columnItem5.Width = 18;
			this.intzaSum.Columns.Add(columnItem);
			this.intzaSum.Columns.Add(columnItem2);
			this.intzaSum.Columns.Add(columnItem3);
			this.intzaSum.Columns.Add(columnItem4);
			this.intzaSum.Columns.Add(columnItem5);
			this.intzaSum.CurrentScroll = 0;
			this.intzaSum.FocusItemIndex = -1;
			this.intzaSum.ForeColor = Color.Black;
			this.intzaSum.GridColor = Color.FromArgb(45, 45, 45);
			this.intzaSum.HeaderPctHeight = 80f;
			this.intzaSum.IsAutoRepaint = true;
			this.intzaSum.IsDrawFullRow = false;
			this.intzaSum.IsDrawGrid = true;
			this.intzaSum.IsDrawHeader = true;
			this.intzaSum.IsScrollable = false;
			this.intzaSum.Location = new Point(0, 345);
			this.intzaSum.MainColumn = "";
			this.intzaSum.Name = "intzaSum";
			this.intzaSum.Rows = 2;
			this.intzaSum.RowSelectColor = Color.FromArgb(95, 158, 160);
			this.intzaSum.RowSelectType = 0;
			this.intzaSum.RowsVisible = 2;
			this.intzaSum.ScrollChennelColor = Color.LightGray;
			this.intzaSum.Size = new Size(783, 52);
			this.intzaSum.SortColumnName = "";
			this.intzaSum.SortType = SortType.Desc;
			this.intzaSum.TabIndex = 25;
			this.tStripTop.AllowMerge = false;
			this.tStripTop.BackColor = Color.FromArgb(10, 10, 10);
			this.tStripTop.CanOverflow = false;
			this.tStripTop.GripMargin = new Padding(0);
			this.tStripTop.GripStyle = ToolStripGripStyle.Hidden;
			this.tStripTop.Items.AddRange(new ToolStripItem[]
			{
				this.tsbtnViewTransaction,
				this.tsripViewOrderByStock,
				this.tsbtnViewByStock,
				this.tStripSep2,
				this.tsbtnStopOrder,
				this.tStripSep3,
				this.tsbtnReOrder
			});
			this.tStripTop.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.tStripTop.Location = new Point(0, 0);
			this.tStripTop.Name = "tStripTop";
			this.tStripTop.Padding = new Padding(1, 1, 1, 2);
			this.tStripTop.RenderMode = ToolStripRenderMode.Professional;
			this.tStripTop.Size = new Size(784, 26);
			this.tStripTop.TabIndex = 58;
			this.tStripTop.Tag = "-1";
			this.tsbtnViewTransaction.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnViewTransaction.ForeColor = Color.Orange;
			this.tsbtnViewTransaction.ImageTransparentColor = Color.Magenta;
			this.tsbtnViewTransaction.Margin = new Padding(5, 1, 5, 2);
			this.tsbtnViewTransaction.Name = "tsbtnViewTransaction";
			this.tsbtnViewTransaction.Size = new Size(155, 20);
			this.tsbtnViewTransaction.Text = "View Order by Transactions";
			this.tsbtnViewTransaction.Click += new EventHandler(this.tsbtnViewTransaction_Click);
			this.tsripViewOrderByStock.Name = "tsripViewOrderByStock";
			this.tsripViewOrderByStock.Size = new Size(6, 23);
			this.tsbtnViewByStock.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnViewByStock.ForeColor = Color.LightGray;
			this.tsbtnViewByStock.ImageTransparentColor = Color.Magenta;
			this.tsbtnViewByStock.Margin = new Padding(5, 1, 0, 2);
			this.tsbtnViewByStock.Name = "tsbtnViewByStock";
			this.tsbtnViewByStock.Size = new Size(117, 20);
			this.tsbtnViewByStock.Text = "View Order by Stock";
			this.tsbtnViewByStock.Click += new EventHandler(this.tsbtnViewByStock_Click);
			this.tStripSep2.Name = "tStripSep2";
			this.tStripSep2.Size = new Size(6, 23);
			this.tsbtnStopOrder.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnStopOrder.ForeColor = Color.LightGray;
			this.tsbtnStopOrder.ImageTransparentColor = Color.Magenta;
			this.tsbtnStopOrder.Margin = new Padding(5, 1, 0, 2);
			this.tsbtnStopOrder.Name = "tsbtnStopOrder";
			this.tsbtnStopOrder.Size = new Size(98, 20);
			this.tsbtnStopOrder.Text = "View Auto Trade";
			this.tsbtnStopOrder.Click += new EventHandler(this.tsbtnStopOrder_Click);
			this.tStripSep3.Name = "tStripSep3";
			this.tStripSep3.Size = new Size(6, 23);
			this.tsbtnReOrder.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnReOrder.ForeColor = Color.LightGray;
			this.tsbtnReOrder.ImageTransparentColor = Color.Magenta;
			this.tsbtnReOrder.Name = "tsbtnReOrder";
			this.tsbtnReOrder.Size = new Size(59, 20);
			this.tsbtnReOrder.Text = "Re-Order";
			this.tsbtnReOrder.Click += new EventHandler(this.tsbtnReOrder_Click);
			this.OrderStatBox.BackColor = SystemColors.ControlDark;
			this.OrderStatBox.IsLoadingData = false;
			this.OrderStatBox.Location = new Point(3, 196);
			this.OrderStatBox.Margin = new Padding(3, 4, 3, 4);
			this.OrderStatBox.Name = "OrderStatBox";
			this.OrderStatBox.Size = new Size(780, 128);
			this.OrderStatBox.TabIndex = 66;
			this.OrderStatBox.Visible = false;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.DimGray;
			base.ClientSize = new Size(784, 431);
			base.Controls.Add(this.OrderStatBox);
			base.Controls.Add(this.tStripTop);
			base.Controls.Add(this.intzaSum);
			base.Controls.Add(this.ViewOrderBox);
			base.Margin = new Padding(3, 4, 3, 4);
			base.Name = "frmViewOrder";
			this.Text = "ViewOrder";
			base.IDoShownDelay += new ClientBaseForm.OnShownDelayEventHandler(this.frmViewOrder_IDoShownDelay);
			base.IDoLoadData += new ClientBaseForm.OnIDoLoadDataEventHandler(this.frmViewOrder_IDoLoadData);
			base.IDoFontChanged += new ClientBaseForm.OnFontChangedEventHandler(this.frmViewOrder_IDoFontChanged);
			base.IDoCustomSizeChanged += new ClientBaseForm.CustomSizeChangedEventHandler(this.frmViewOrder_IDoCustomSizeChanged);
			base.IDoSymbolLinked += new ClientBaseForm.OnSymbolLinkEventHandler(this.frmViewOrder_IDoSymbolLinked);
			base.VisibleChanged += new EventHandler(this.frmViewOrder_VisibleChanged);
			base.IDoMainFormKeyUp += new ClientBaseForm.OnFormKeyUpEventHandler(this.frmViewOrder_IDoMainFormKeyUp);
			base.IDoReActivated += new ClientBaseForm.OnReActiveEventHandler(this.frmViewOrder_IDoReActivated);
			base.Controls.SetChildIndex(this.ViewOrderBox, 0);
			base.Controls.SetChildIndex(this.intzaSum, 0);
			base.Controls.SetChildIndex(this.tStripTop, 0);
			base.Controls.SetChildIndex(this.OrderStatBox, 0);
			this.tStripTop.ResumeLayout(false);
			this.tStripTop.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmViewOrder()
		{
			this.InitializeComponent();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmViewOrder(Dictionary<string, object> propertiesValue)
		{
			this.InitializeComponent();
			if (ApplicationInfo.SupportFreewill)
			{
				this.intzaSum.GetColumn("mvalue").Visible = false;
				this.intzaSum.GetColumn("unmatch").Width = 27;
				this.intzaSum.GetColumn("mvolume").Width = 27;
				this.intzaSum.Redraw();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReceiveMessage(IBroadcastMessage message, StockList.StockInformation realtimeStockInfo)
		{
			this.ViewOrderBox.ReceiveMessage(message, realtimeStockInfo);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReceiveTfexMessage(IBroadcastMessage message, SeriesList.SeriesInformation realtimeSeriesInfo)
		{
			this.ViewOrderBox.ReceiveTfexMessage(message, realtimeSeriesInfo);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmViewOrder_IDoShownDelay()
		{
			this.ViewOrderBox.IsActive = true;
			this.SetPage(0);
			base.Show();
			base.OpenedForm();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmViewOrder_IDoLoadData()
		{
			this.ViewOrderBox.ReloadData();
			base.IsLoadingData = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmViewOrder_IDoReActivated()
		{
			if (!base.IsLoadingData)
			{
				this.SetResize(this.IsWidthChanged | this.IsHeightChanged);
				base.Show();
				this.ViewOrderBox.SelectAllOrderForCancel(false);
				this.ViewOrderBox.SetGridFocus(true);
				if (!ApplicationInfo.IsEquityAccount)
				{
					this.SetPage(0);
				}
				else
				{
					this.SetPage(this._currentPage);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmViewOrder_IDoFontChanged()
		{
			if (!base.IsLoadingData)
			{
				this.SetResize(this.IsWidthChanged);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmViewOrder_IDoMainFormKeyUp(KeyEventArgs e)
		{
			this.ViewOrderBox.FormKeyUp(this, e);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmViewOrder_IDoCustomSizeChanged()
		{
			if (!base.IsLoadingData)
			{
				this.SetResize(this.IsWidthChanged | this.IsHeightChanged);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetResize(bool isChanged)
		{
			try
			{
				if (isChanged)
				{
					if (this._currentPage == 0 && ApplicationInfo.IsEquityAccount)
					{
						int heightByRows = this.intzaSum.GetHeightByRows();
						this.intzaSum.SetBounds(0, base.Height - heightByRows + 1, base.Width, heightByRows);
						this.ViewOrderBox.SetBounds(0, this.tStripTop.Top + this.tStripTop.Height, base.Width, base.Height - heightByRows - this.tStripTop.Bottom - 1);
						this.ViewOrderBox.SetResize(isChanged);
					}
					else
					{
						if (this._currentPage == 1)
						{
							this.OrderStatBox.SetBounds(0, this.tStripTop.Top + this.tStripTop.Height, base.Width, base.Height - this.tStripTop.Bottom);
							this.OrderStatBox.SetResize();
						}
						else
						{
							this.ViewOrderBox.SetBounds(0, this.tStripTop.Top + this.tStripTop.Height, base.Width, base.Height - this.tStripTop.Bottom - 1);
							this.ViewOrderBox.SetResize(isChanged);
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
		private void ucViewOrder_OnDisplaySummaryOrders()
		{
			try
			{
				this.intzaSum.BeginUpdate();
				this.intzaSum.ClearAllText();
				this.intzaSum.Records(0).Fields("sum").Text = "*** BUY ***";
				this.intzaSum.Records(0).Fields("volume").Text = this.ViewOrderBox.TotalBuyVolume;
				this.intzaSum.Records(0).Fields("mvolume").Text = this.ViewOrderBox.TotalBuyMatchedVolume;
				this.intzaSum.Records(0).Fields("mvalue").Text = this.ViewOrderBox.TotalBuyMatchedValue;
				this.intzaSum.Records(0).Fields("unmatch").Text = this.ViewOrderBox.UnMatchedBuyVol;
				this.intzaSum.Records(0).Fields("sum").FontColor = Color.Lime;
				this.intzaSum.Records(0).Fields("volume").FontColor = Color.Lime;
				this.intzaSum.Records(0).Fields("mvolume").FontColor = Color.Lime;
				this.intzaSum.Records(0).Fields("mvalue").FontColor = Color.Lime;
				this.intzaSum.Records(0).Fields("unmatch").FontColor = Color.Lime;
				this.intzaSum.Records(1).Fields("sum").Text = "*** SELL ***";
				this.intzaSum.Records(1).Fields("volume").Text = this.ViewOrderBox.TotalSellVolume;
				this.intzaSum.Records(1).Fields("mvolume").Text = this.ViewOrderBox.TotalSellMatchedVolume;
				this.intzaSum.Records(1).Fields("mvalue").Text = this.ViewOrderBox.TotalSellMatchedValue;
				this.intzaSum.Records(1).Fields("unmatch").Text = this.ViewOrderBox.UnMatchedSellVol;
				this.intzaSum.Records(1).Fields("sum").FontColor = Color.Red;
				this.intzaSum.Records(1).Fields("volume").FontColor = Color.Red;
				this.intzaSum.Records(1).Fields("mvolume").FontColor = Color.Red;
				this.intzaSum.Records(1).Fields("mvalue").FontColor = Color.Red;
				this.intzaSum.Records(1).Fields("unmatch").FontColor = Color.Red;
				this.intzaSum.EndUpdate();
			}
			catch (Exception ex)
			{
				this.ShowError("ucViewOrder_OnDisplaySummaryOrders", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmViewOrder_VisibleChanged(object sender, EventArgs e)
		{
			if (!base.DesignMode)
			{
				if (!base.Visible)
				{
					this.ViewOrderBox.CloseViewOrderInfoBox();
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmViewOrder_IDoSymbolLinked(object sender, SymbolLinkSource source, string newStock)
		{
			if (source == SymbolLinkSource.SwitchAccount)
			{
				if (!ApplicationInfo.IsEquityAccount)
				{
					this.SetPage(0);
				}
				else
				{
					this.SetPage(this._currentPage);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnViewByStock_Click(object sender, EventArgs e)
		{
			this.SetPage(1);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnViewTransaction_Click(object sender, EventArgs e)
		{
			this.SetPage(0);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnStopOrder_Click(object sender, EventArgs e)
		{
			this.SetPage(2);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnReOrder_Click(object sender, EventArgs e)
		{
			this.SetPage(4);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetPage(int currPage)
		{
			try
			{
				this.tsbtnStopOrder.Visible = (ApplicationInfo.IsEquityAccount && ApplicationInfo.StopOrderSupported);
				this.tsbtnReOrder.Visible = (ApplicationInfo.IsEquityAccount && ApplicationInfo.SupportReOrderBox);
				if (ApplicationInfo.SupportFreewill)
				{
					this.tsripViewOrderByStock.Visible = false;
					this.tsbtnViewByStock.Visible = false;
				}
				else
				{
					this.tsripViewOrderByStock.Visible = ApplicationInfo.IsEquityAccount;
					this.tsbtnViewByStock.Visible = ApplicationInfo.IsEquityAccount;
				}
				this.tStripSep2.Visible = (ApplicationInfo.IsEquityAccount && ApplicationInfo.StopOrderSupported);
				this.tStripSep3.Visible = (ApplicationInfo.IsEquityAccount && ApplicationInfo.SupportReOrderBox);
				this.tsbtnViewTransaction.ForeColor = Color.LightGray;
				this.tsbtnViewByStock.ForeColor = Color.LightGray;
				this.tsbtnStopOrder.ForeColor = Color.LightGray;
				this.tsbtnReOrder.ForeColor = Color.LightGray;
				if (currPage == 0)
				{
					this._currentPage = 0;
					this.tsbtnViewTransaction.ForeColor = Color.Orange;
					this.ViewOrderBox.Show();
					this.OrderStatBox.Hide();
					this.SetResize(true);
					if (ApplicationInfo.IsEquityAccount)
					{
						this.intzaSum.Show();
						this.ViewOrderBox.SetView(1);
					}
					else
					{
						this.intzaSum.Hide();
						this.ViewOrderBox.SetView(3);
					}
				}
				else
				{
					if (currPage == 1)
					{
						this._currentPage = 1;
						this.tsbtnViewByStock.ForeColor = Color.Orange;
						this.intzaSum.Visible = false;
						this.OrderStatBox.Show();
						this.OrderStatBox.Init();
						this.ViewOrderBox.Hide();
						this.intzaSum.Hide();
						this.SetResize(true);
						this.OrderStatBox.Reload_AccountChanged();
					}
					else
					{
						if (currPage == 2)
						{
							this._currentPage = 2;
							this.tsbtnStopOrder.ForeColor = Color.Orange;
							this.ViewOrderBox.Show();
							this.OrderStatBox.Hide();
							this.intzaSum.Hide();
							this.SetResize(true);
							this.ViewOrderBox.SetView(2);
						}
						else
						{
							if (currPage == 4)
							{
								this._currentPage = 4;
								this.tsbtnReOrder.ForeColor = Color.Orange;
								this.ViewOrderBox.Show();
								this.OrderStatBox.Hide();
								this.intzaSum.Hide();
								this.SetResize(true);
								this.ViewOrderBox.SetView(4);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SetPage", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void OnNewStopOrder(DataRow dr)
		{
			this.ViewOrderBox.UpdateStopOrderToGrid(dr, -1, true);
		}
	}
}
