using i2TradePlus.Information;
using i2TradePlus.Properties;
using i2TradePlus.Templates;
using ITSNet.Common.BIZ;
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
	public class frmTopBBOs : ClientBaseForm, IRealtimeMessage
	{
		private delegate void UpdateTopPriceCallBack(StockList.StockInformation sInfo, string side, decimal price1, decimal price2, decimal price3, decimal price4, decimal price5, long volume1, long volume2, long volume3, long volume4, long volume5, SortGrid intzaTP);
		private delegate void UpdateLastPriceCallback(StockList.StockInformation stockInfo, decimal lastPrice, decimal high, decimal low, IntzaCustomGrid intzaHeader);
		private delegate void UpdateAllVolumeCallBack(long openVolume, long buyVolume, long sellVolume, IntzaCustomGrid intzaVolumeByBoard);
		private delegate void UpdateLastSaleTickerCallBack(StockList.StockInformation stockInfo, decimal price, string side, long volume, string lastUpdate, int index, SortGrid intzaLS);
		private const int maxControl = 8;
		private IContainer components = null;
		private Panel panelMain;
		private Scrollbar scrollbar1;
		private SortGrid speedTableGrid1;
		private SortGrid speedTableGrid2;
		private SortGrid speedTableGrid3;
		private SortGrid speedTableGrid4;
		private ToolStrip tStripMenu;
		private ToolStripLabel toolStripLabel10;
		private ToolStripComboBox tscbSelection;
		private ToolStripButton tsbtnAdd;
		private ToolStripButton tsbtnDel;
		private ucBids ucBids1;
		private ucBids ucBids4;
		private ucBids ucBids3;
		private ucBids ucBids2;
		private ucBids ucBids8;
		private ucBids ucBids7;
		private ucBids ucBids6;
		private ucBids ucBids5;
		private ContextMenuStrip contextLink;
		private ToolStripMenuItem tsmCallHistoricalChart;
		private ToolStripMenuItem tsmCallNews;
		private ToolStripMenuItem tsmCallStockInPlay;
		private ToolStripMenuItem tsmCallSaleByPrice;
		private ToolStripMenuItem tsmCallSaleByTime;
		private ToolStripMenuItem tsmCallOddlot;
		private ToolStripSeparator toolStripMenuItem1;
		private ToolStripMenuItem tsmCallMarketWatch;
		private BackgroundWorker bgwReloadData = null;
		private BackgroundWorker bgwReloadData_TFEX = null;
		private DataSet tdsTOPBBo = null;
		private DataSet tdsTOPBBoTfex = null;
		private string _selectionText = string.Empty;
		private int _currentActivePanel = 0;
		private int _currFavPage = 1;
		private ucBids[] _bidControl = null;
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
			this.panelMain = new Panel();
			this.ucBids8 = new ucBids();
			this.ucBids7 = new ucBids();
			this.ucBids6 = new ucBids();
			this.ucBids5 = new ucBids();
			this.ucBids4 = new ucBids();
			this.ucBids3 = new ucBids();
			this.ucBids2 = new ucBids();
			this.ucBids1 = new ucBids();
			this.tStripMenu = new ToolStrip();
			this.toolStripLabel10 = new ToolStripLabel();
			this.tscbSelection = new ToolStripComboBox();
			this.tsbtnAdd = new ToolStripButton();
			this.tsbtnDel = new ToolStripButton();
			this.scrollbar1 = new Scrollbar();
			this.speedTableGrid1 = new SortGrid();
			this.speedTableGrid2 = new SortGrid();
			this.speedTableGrid3 = new SortGrid();
			this.speedTableGrid4 = new SortGrid();
			this.contextLink = new ContextMenuStrip(this.components);
			this.tsmCallHistoricalChart = new ToolStripMenuItem();
			this.tsmCallNews = new ToolStripMenuItem();
			this.toolStripMenuItem1 = new ToolStripSeparator();
			this.tsmCallStockInPlay = new ToolStripMenuItem();
			this.tsmCallSaleByPrice = new ToolStripMenuItem();
			this.tsmCallSaleByTime = new ToolStripMenuItem();
			this.tsmCallOddlot = new ToolStripMenuItem();
			this.tsmCallMarketWatch = new ToolStripMenuItem();
			this.panelMain.SuspendLayout();
			this.tStripMenu.SuspendLayout();
			this.contextLink.SuspendLayout();
			base.SuspendLayout();
			this.panelMain.BackColor = Color.DimGray;
			this.panelMain.Controls.Add(this.ucBids8);
			this.panelMain.Controls.Add(this.ucBids7);
			this.panelMain.Controls.Add(this.ucBids6);
			this.panelMain.Controls.Add(this.ucBids5);
			this.panelMain.Controls.Add(this.ucBids4);
			this.panelMain.Controls.Add(this.ucBids3);
			this.panelMain.Controls.Add(this.ucBids2);
			this.panelMain.Controls.Add(this.ucBids1);
			this.panelMain.Location = new Point(0, 67);
			this.panelMain.Name = "panelMain";
			this.panelMain.Size = new Size(818, 418);
			this.panelMain.TabIndex = 70;
			this.ucBids8.BackColor = Color.Gray;
			this.ucBids8.CurrStockNo = 0;
			this.ucBids8.IsLoadingData = false;
			this.ucBids8.Location = new Point(374, 260);
			this.ucBids8.Name = "ucBids8";
			this.ucBids8.Size = new Size(444, 154);
			this.ucBids8.TabIndex = 85;
			this.ucBids8.OnNewStock += new ucBids.OnNewStockEventHandler(this.ucBids1_OnNewStock);
			this.ucBids8.OnLink += new ucBids.OnLinkEventHandler(this.ucBids_OnLink);
			this.ucBids8.Leave += new EventHandler(this.ucBids_Leave);
			this.ucBids8.Enter += new EventHandler(this.ucBids_Enter);
			this.ucBids7.BackColor = Color.Gray;
			this.ucBids7.CurrStockNo = 0;
			this.ucBids7.IsLoadingData = false;
			this.ucBids7.Location = new Point(371, 182);
			this.ucBids7.Name = "ucBids7";
			this.ucBids7.Size = new Size(444, 154);
			this.ucBids7.TabIndex = 84;
			this.ucBids7.OnNewStock += new ucBids.OnNewStockEventHandler(this.ucBids1_OnNewStock);
			this.ucBids7.OnLink += new ucBids.OnLinkEventHandler(this.ucBids_OnLink);
			this.ucBids7.Leave += new EventHandler(this.ucBids_Leave);
			this.ucBids7.Enter += new EventHandler(this.ucBids_Enter);
			this.ucBids6.BackColor = Color.Gray;
			this.ucBids6.CurrStockNo = 0;
			this.ucBids6.IsLoadingData = false;
			this.ucBids6.Location = new Point(374, 84);
			this.ucBids6.Name = "ucBids6";
			this.ucBids6.Size = new Size(444, 154);
			this.ucBids6.TabIndex = 83;
			this.ucBids6.OnNewStock += new ucBids.OnNewStockEventHandler(this.ucBids1_OnNewStock);
			this.ucBids6.OnLink += new ucBids.OnLinkEventHandler(this.ucBids_OnLink);
			this.ucBids6.Leave += new EventHandler(this.ucBids_Leave);
			this.ucBids6.Enter += new EventHandler(this.ucBids_Enter);
			this.ucBids5.BackColor = Color.Gray;
			this.ucBids5.CurrStockNo = 0;
			this.ucBids5.IsLoadingData = false;
			this.ucBids5.Location = new Point(374, 3);
			this.ucBids5.Name = "ucBids5";
			this.ucBids5.Size = new Size(444, 154);
			this.ucBids5.TabIndex = 82;
			this.ucBids5.OnNewStock += new ucBids.OnNewStockEventHandler(this.ucBids1_OnNewStock);
			this.ucBids5.OnLink += new ucBids.OnLinkEventHandler(this.ucBids_OnLink);
			this.ucBids5.Leave += new EventHandler(this.ucBids_Leave);
			this.ucBids5.Enter += new EventHandler(this.ucBids_Enter);
			this.ucBids4.BackColor = Color.Gray;
			this.ucBids4.CurrStockNo = 0;
			this.ucBids4.IsLoadingData = false;
			this.ucBids4.Location = new Point(3, 260);
			this.ucBids4.Name = "ucBids4";
			this.ucBids4.Size = new Size(444, 154);
			this.ucBids4.TabIndex = 81;
			this.ucBids4.OnNewStock += new ucBids.OnNewStockEventHandler(this.ucBids1_OnNewStock);
			this.ucBids4.OnLink += new ucBids.OnLinkEventHandler(this.ucBids_OnLink);
			this.ucBids4.Leave += new EventHandler(this.ucBids_Leave);
			this.ucBids4.Enter += new EventHandler(this.ucBids_Enter);
			this.ucBids3.BackColor = Color.Gray;
			this.ucBids3.CurrStockNo = 0;
			this.ucBids3.IsLoadingData = false;
			this.ucBids3.Location = new Point(-4, 200);
			this.ucBids3.Name = "ucBids3";
			this.ucBids3.Size = new Size(444, 154);
			this.ucBids3.TabIndex = 80;
			this.ucBids3.OnNewStock += new ucBids.OnNewStockEventHandler(this.ucBids1_OnNewStock);
			this.ucBids3.OnLink += new ucBids.OnLinkEventHandler(this.ucBids_OnLink);
			this.ucBids3.Leave += new EventHandler(this.ucBids_Leave);
			this.ucBids3.Enter += new EventHandler(this.ucBids_Enter);
			this.ucBids2.BackColor = Color.Gray;
			this.ucBids2.CurrStockNo = 0;
			this.ucBids2.IsLoadingData = false;
			this.ucBids2.Location = new Point(0, 112);
			this.ucBids2.Name = "ucBids2";
			this.ucBids2.Size = new Size(444, 154);
			this.ucBids2.TabIndex = 79;
			this.ucBids2.OnNewStock += new ucBids.OnNewStockEventHandler(this.ucBids1_OnNewStock);
			this.ucBids2.OnLink += new ucBids.OnLinkEventHandler(this.ucBids_OnLink);
			this.ucBids2.Leave += new EventHandler(this.ucBids_Leave);
			this.ucBids2.Enter += new EventHandler(this.ucBids_Enter);
			this.ucBids1.BackColor = Color.Gray;
			this.ucBids1.CurrStockNo = 0;
			this.ucBids1.IsLoadingData = false;
			this.ucBids1.Location = new Point(0, 3);
			this.ucBids1.Name = "ucBids1";
			this.ucBids1.Size = new Size(444, 154);
			this.ucBids1.TabIndex = 78;
			this.ucBids1.OnNewStock += new ucBids.OnNewStockEventHandler(this.ucBids1_OnNewStock);
			this.ucBids1.OnLink += new ucBids.OnLinkEventHandler(this.ucBids_OnLink);
			this.ucBids1.Leave += new EventHandler(this.ucBids_Leave);
			this.ucBids1.Enter += new EventHandler(this.ucBids_Enter);
			this.tStripMenu.AllowMerge = false;
			this.tStripMenu.BackColor = Color.FromArgb(20, 20, 20);
			this.tStripMenu.GripMargin = new Padding(0);
			this.tStripMenu.GripStyle = ToolStripGripStyle.Hidden;
			this.tStripMenu.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripLabel10,
				this.tscbSelection,
				this.tsbtnAdd,
				this.tsbtnDel
			});
			this.tStripMenu.Location = new Point(0, 0);
			this.tStripMenu.Name = "tStripMenu";
			this.tStripMenu.Padding = new Padding(1, 1, 1, 2);
			this.tStripMenu.RenderMode = ToolStripRenderMode.Professional;
			this.tStripMenu.Size = new Size(1121, 26);
			this.tStripMenu.TabIndex = 77;
			this.tStripMenu.Tag = "-1";
			this.toolStripLabel10.ForeColor = Color.LightGray;
			this.toolStripLabel10.Margin = new Padding(2, 1, 2, 2);
			this.toolStripLabel10.Name = "toolStripLabel10";
			this.toolStripLabel10.Size = new Size(61, 20);
			this.toolStripLabel10.Text = "Selection :";
			this.tscbSelection.BackColor = Color.FromArgb(30, 30, 30);
			this.tscbSelection.DropDownStyle = ComboBoxStyle.DropDownList;
			this.tscbSelection.ForeColor = Color.LightGray;
			this.tscbSelection.Items.AddRange(new object[]
			{
				"Favorites-1",
				"Favorites-2",
				"Favorites-3",
				"Favorites-4",
				"Favorites-5"
			});
			this.tscbSelection.Name = "tscbSelection";
			this.tscbSelection.Size = new Size(120, 23);
			this.tscbSelection.SelectedIndexChanged += new EventHandler(this.tscbSelection_SelectedIndexChanged);
			this.tsbtnAdd.ForeColor = Color.LightGray;
			this.tsbtnAdd.Image = Resources.Plus;
			this.tsbtnAdd.ImageTransparentColor = Color.Magenta;
			this.tsbtnAdd.Margin = new Padding(10, 1, 0, 2);
			this.tsbtnAdd.Name = "tsbtnAdd";
			this.tsbtnAdd.Size = new Size(49, 20);
			this.tsbtnAdd.Text = "Add";
			this.tsbtnAdd.Click += new EventHandler(this.tsbtnAdd_Click);
			this.tsbtnDel.ForeColor = Color.LightGray;
			this.tsbtnDel.Image = Resources.Minus;
			this.tsbtnDel.ImageTransparentColor = Color.Magenta;
			this.tsbtnDel.Name = "tsbtnDel";
			this.tsbtnDel.Size = new Size(60, 20);
			this.tsbtnDel.Text = "Delete";
			this.tsbtnDel.Click += new EventHandler(this.tsbtnDel_Click);
			this.scrollbar1.BackColor = Color.Black;
			this.scrollbar1.ChannelColor = Color.Gray;
			this.scrollbar1.Cursor = Cursors.Hand;
			this.scrollbar1.HeaderHeight = 0f;
			this.scrollbar1.LargeChange = 10;
			this.scrollbar1.Location = new Point(896, 29);
			this.scrollbar1.Maximum = 100;
			this.scrollbar1.MinimumSize = new Size(10, 47);
			this.scrollbar1.Name = "scrollbar1";
			this.scrollbar1.Size = new Size(10, 375);
			this.scrollbar1.TabIndex = 71;
			this.scrollbar1.Value = 0;
			this.scrollbar1.ValueChanged += new EventHandler(this.scrollbar1_ValueChanged);
			this.speedTableGrid1.AllowDrop = true;
			this.speedTableGrid1.BackColor = Color.Black;
			this.speedTableGrid1.CanBlink = true;
			this.speedTableGrid1.CanDrag = false;
			this.speedTableGrid1.CanGetMouseMove = false;
			columnItem.Alignment = StringAlignment.Far;
			columnItem.BackColor = Color.FromArgb(64, 64, 64);
			columnItem.ColumnAlignment = StringAlignment.Center;
			columnItem.FontColor = Color.LightGray;
			columnItem.MyStyle = FontStyle.Regular;
			columnItem.Name = "bidvolume";
			columnItem.Text = "Volume";
			columnItem.ValueFormat = FormatType.Volume;
			columnItem.Visible = true;
			columnItem.Width = 30;
			columnItem2.Alignment = StringAlignment.Near;
			columnItem2.BackColor = Color.FromArgb(64, 64, 64);
			columnItem2.ColumnAlignment = StringAlignment.Center;
			columnItem2.FontColor = Color.LightGray;
			columnItem2.MyStyle = FontStyle.Regular;
			columnItem2.Name = "bid";
			columnItem2.Text = "Bid";
			columnItem2.ValueFormat = FormatType.Text;
			columnItem2.Visible = true;
			columnItem2.Width = 20;
			columnItem3.Alignment = StringAlignment.Near;
			columnItem3.BackColor = Color.FromArgb(64, 64, 64);
			columnItem3.ColumnAlignment = StringAlignment.Center;
			columnItem3.FontColor = Color.LightGray;
			columnItem3.MyStyle = FontStyle.Regular;
			columnItem3.Name = "offer";
			columnItem3.Text = "Offer";
			columnItem3.ValueFormat = FormatType.Text;
			columnItem3.Visible = true;
			columnItem3.Width = 20;
			columnItem4.Alignment = StringAlignment.Far;
			columnItem4.BackColor = Color.FromArgb(64, 64, 64);
			columnItem4.ColumnAlignment = StringAlignment.Center;
			columnItem4.FontColor = Color.LightGray;
			columnItem4.MyStyle = FontStyle.Regular;
			columnItem4.Name = "offervolume";
			columnItem4.Text = "Volume";
			columnItem4.ValueFormat = FormatType.Volume;
			columnItem4.Visible = true;
			columnItem4.Width = 30;
			this.speedTableGrid1.Columns.Add(columnItem);
			this.speedTableGrid1.Columns.Add(columnItem2);
			this.speedTableGrid1.Columns.Add(columnItem3);
			this.speedTableGrid1.Columns.Add(columnItem4);
			this.speedTableGrid1.CurrentScroll = 0;
			this.speedTableGrid1.FocusItemIndex = -1;
			this.speedTableGrid1.ForeColor = Color.Black;
			this.speedTableGrid1.GridColor = Color.FromArgb(45, 45, 45);
			this.speedTableGrid1.HeaderPctHeight = 80f;
			this.speedTableGrid1.IsAutoRepaint = false;
			this.speedTableGrid1.IsDrawFullRow = false;
			this.speedTableGrid1.IsDrawGrid = true;
			this.speedTableGrid1.IsDrawHeader = true;
			this.speedTableGrid1.IsScrollable = false;
			this.speedTableGrid1.Location = new Point(0, 23);
			this.speedTableGrid1.MainColumn = "";
			this.speedTableGrid1.Margin = new Padding(1);
			this.speedTableGrid1.Name = "speedTableGrid1";
			this.speedTableGrid1.Rows = 5;
			this.speedTableGrid1.RowSelectColor = Color.FromArgb(95, 158, 160);
			this.speedTableGrid1.RowSelectType = 0;
			this.speedTableGrid1.RowsVisible = 5;
			this.speedTableGrid1.ScrollChennelColor = Color.FromArgb(100, 100, 100);
			this.speedTableGrid1.Size = new Size(220, 45);
			this.speedTableGrid1.SortColumnName = "";
			this.speedTableGrid1.SortType = SortType.Desc;
			this.speedTableGrid1.TabIndex = 66;
			this.speedTableGrid2.AllowDrop = true;
			this.speedTableGrid2.BackColor = Color.Black;
			this.speedTableGrid2.CanBlink = true;
			this.speedTableGrid2.CanDrag = false;
			this.speedTableGrid2.CanGetMouseMove = false;
			columnItem5.Alignment = StringAlignment.Far;
			columnItem5.BackColor = Color.FromArgb(64, 64, 64);
			columnItem5.ColumnAlignment = StringAlignment.Center;
			columnItem5.FontColor = Color.LightGray;
			columnItem5.MyStyle = FontStyle.Regular;
			columnItem5.Name = "volume";
			columnItem5.Text = "Volume";
			columnItem5.ValueFormat = FormatType.Volume;
			columnItem5.Visible = true;
			columnItem5.Width = 40;
			columnItem6.Alignment = StringAlignment.Far;
			columnItem6.BackColor = Color.FromArgb(64, 64, 64);
			columnItem6.ColumnAlignment = StringAlignment.Center;
			columnItem6.FontColor = Color.LightGray;
			columnItem6.MyStyle = FontStyle.Regular;
			columnItem6.Name = "price";
			columnItem6.Text = "Price";
			columnItem6.ValueFormat = FormatType.Text;
			columnItem6.Visible = true;
			columnItem6.Width = 27;
			columnItem7.Alignment = StringAlignment.Far;
			columnItem7.BackColor = Color.FromArgb(64, 64, 64);
			columnItem7.ColumnAlignment = StringAlignment.Center;
			columnItem7.FontColor = Color.LightGray;
			columnItem7.MyStyle = FontStyle.Regular;
			columnItem7.Name = "time";
			columnItem7.Text = "Time";
			columnItem7.ValueFormat = FormatType.Text;
			columnItem7.Visible = true;
			columnItem7.Width = 33;
			this.speedTableGrid2.Columns.Add(columnItem5);
			this.speedTableGrid2.Columns.Add(columnItem6);
			this.speedTableGrid2.Columns.Add(columnItem7);
			this.speedTableGrid2.CurrentScroll = 0;
			this.speedTableGrid2.FocusItemIndex = -1;
			this.speedTableGrid2.ForeColor = Color.Black;
			this.speedTableGrid2.GridColor = Color.FromArgb(45, 45, 45);
			this.speedTableGrid2.HeaderPctHeight = 80f;
			this.speedTableGrid2.IsAutoRepaint = false;
			this.speedTableGrid2.IsDrawFullRow = true;
			this.speedTableGrid2.IsDrawGrid = false;
			this.speedTableGrid2.IsDrawHeader = true;
			this.speedTableGrid2.IsScrollable = false;
			this.speedTableGrid2.Location = new Point(232, 21);
			this.speedTableGrid2.MainColumn = "";
			this.speedTableGrid2.Margin = new Padding(1);
			this.speedTableGrid2.Name = "speedTableGrid2";
			this.speedTableGrid2.Rows = 5;
			this.speedTableGrid2.RowSelectColor = Color.FromArgb(95, 158, 160);
			this.speedTableGrid2.RowSelectType = 0;
			this.speedTableGrid2.RowsVisible = 5;
			this.speedTableGrid2.ScrollChennelColor = Color.FromArgb(100, 100, 100);
			this.speedTableGrid2.Size = new Size(156, 38);
			this.speedTableGrid2.SortColumnName = "";
			this.speedTableGrid2.SortType = SortType.Desc;
			this.speedTableGrid2.TabIndex = 65;
			this.speedTableGrid3.AllowDrop = true;
			this.speedTableGrid3.BackColor = Color.Black;
			this.speedTableGrid3.CanBlink = true;
			this.speedTableGrid3.CanDrag = false;
			this.speedTableGrid3.CanGetMouseMove = false;
			columnItem8.Alignment = StringAlignment.Far;
			columnItem8.BackColor = Color.FromArgb(64, 64, 64);
			columnItem8.ColumnAlignment = StringAlignment.Center;
			columnItem8.FontColor = Color.LightGray;
			columnItem8.MyStyle = FontStyle.Regular;
			columnItem8.Name = "bidvolume";
			columnItem8.Text = "Volume";
			columnItem8.ValueFormat = FormatType.Volume;
			columnItem8.Visible = true;
			columnItem8.Width = 30;
			columnItem9.Alignment = StringAlignment.Near;
			columnItem9.BackColor = Color.FromArgb(64, 64, 64);
			columnItem9.ColumnAlignment = StringAlignment.Center;
			columnItem9.FontColor = Color.LightGray;
			columnItem9.MyStyle = FontStyle.Regular;
			columnItem9.Name = "bid";
			columnItem9.Text = "Bid";
			columnItem9.ValueFormat = FormatType.Text;
			columnItem9.Visible = true;
			columnItem9.Width = 20;
			columnItem10.Alignment = StringAlignment.Near;
			columnItem10.BackColor = Color.FromArgb(64, 64, 64);
			columnItem10.ColumnAlignment = StringAlignment.Center;
			columnItem10.FontColor = Color.LightGray;
			columnItem10.MyStyle = FontStyle.Regular;
			columnItem10.Name = "offer";
			columnItem10.Text = "Offer";
			columnItem10.ValueFormat = FormatType.Text;
			columnItem10.Visible = true;
			columnItem10.Width = 20;
			columnItem11.Alignment = StringAlignment.Far;
			columnItem11.BackColor = Color.FromArgb(64, 64, 64);
			columnItem11.ColumnAlignment = StringAlignment.Center;
			columnItem11.FontColor = Color.LightGray;
			columnItem11.MyStyle = FontStyle.Regular;
			columnItem11.Name = "offervolume";
			columnItem11.Text = "Volume";
			columnItem11.ValueFormat = FormatType.Volume;
			columnItem11.Visible = true;
			columnItem11.Width = 30;
			this.speedTableGrid3.Columns.Add(columnItem8);
			this.speedTableGrid3.Columns.Add(columnItem9);
			this.speedTableGrid3.Columns.Add(columnItem10);
			this.speedTableGrid3.Columns.Add(columnItem11);
			this.speedTableGrid3.CurrentScroll = 0;
			this.speedTableGrid3.FocusItemIndex = -1;
			this.speedTableGrid3.ForeColor = Color.Black;
			this.speedTableGrid3.GridColor = Color.FromArgb(45, 45, 45);
			this.speedTableGrid3.HeaderPctHeight = 80f;
			this.speedTableGrid3.IsAutoRepaint = false;
			this.speedTableGrid3.IsDrawFullRow = false;
			this.speedTableGrid3.IsDrawGrid = true;
			this.speedTableGrid3.IsDrawHeader = true;
			this.speedTableGrid3.IsScrollable = false;
			this.speedTableGrid3.Location = new Point(0, 23);
			this.speedTableGrid3.MainColumn = "";
			this.speedTableGrid3.Margin = new Padding(1);
			this.speedTableGrid3.Name = "speedTableGrid3";
			this.speedTableGrid3.Rows = 5;
			this.speedTableGrid3.RowSelectColor = Color.FromArgb(95, 158, 160);
			this.speedTableGrid3.RowSelectType = 0;
			this.speedTableGrid3.RowsVisible = 5;
			this.speedTableGrid3.ScrollChennelColor = Color.FromArgb(100, 100, 100);
			this.speedTableGrid3.Size = new Size(220, 45);
			this.speedTableGrid3.SortColumnName = "";
			this.speedTableGrid3.SortType = SortType.Desc;
			this.speedTableGrid3.TabIndex = 66;
			this.speedTableGrid4.AllowDrop = true;
			this.speedTableGrid4.BackColor = Color.Black;
			this.speedTableGrid4.CanBlink = true;
			this.speedTableGrid4.CanDrag = false;
			this.speedTableGrid4.CanGetMouseMove = false;
			columnItem12.Alignment = StringAlignment.Far;
			columnItem12.BackColor = Color.FromArgb(64, 64, 64);
			columnItem12.ColumnAlignment = StringAlignment.Center;
			columnItem12.FontColor = Color.LightGray;
			columnItem12.MyStyle = FontStyle.Regular;
			columnItem12.Name = "volume";
			columnItem12.Text = "Volume";
			columnItem12.ValueFormat = FormatType.Volume;
			columnItem12.Visible = true;
			columnItem12.Width = 40;
			columnItem13.Alignment = StringAlignment.Far;
			columnItem13.BackColor = Color.FromArgb(64, 64, 64);
			columnItem13.ColumnAlignment = StringAlignment.Center;
			columnItem13.FontColor = Color.LightGray;
			columnItem13.MyStyle = FontStyle.Regular;
			columnItem13.Name = "price";
			columnItem13.Text = "Price";
			columnItem13.ValueFormat = FormatType.Text;
			columnItem13.Visible = true;
			columnItem13.Width = 27;
			columnItem14.Alignment = StringAlignment.Far;
			columnItem14.BackColor = Color.FromArgb(64, 64, 64);
			columnItem14.ColumnAlignment = StringAlignment.Center;
			columnItem14.FontColor = Color.LightGray;
			columnItem14.MyStyle = FontStyle.Regular;
			columnItem14.Name = "time";
			columnItem14.Text = "Time";
			columnItem14.ValueFormat = FormatType.Text;
			columnItem14.Visible = true;
			columnItem14.Width = 33;
			this.speedTableGrid4.Columns.Add(columnItem12);
			this.speedTableGrid4.Columns.Add(columnItem13);
			this.speedTableGrid4.Columns.Add(columnItem14);
			this.speedTableGrid4.CurrentScroll = 0;
			this.speedTableGrid4.FocusItemIndex = -1;
			this.speedTableGrid4.ForeColor = Color.Black;
			this.speedTableGrid4.GridColor = Color.FromArgb(45, 45, 45);
			this.speedTableGrid4.HeaderPctHeight = 80f;
			this.speedTableGrid4.IsAutoRepaint = false;
			this.speedTableGrid4.IsDrawFullRow = true;
			this.speedTableGrid4.IsDrawGrid = false;
			this.speedTableGrid4.IsDrawHeader = true;
			this.speedTableGrid4.IsScrollable = false;
			this.speedTableGrid4.Location = new Point(232, 21);
			this.speedTableGrid4.MainColumn = "";
			this.speedTableGrid4.Margin = new Padding(1);
			this.speedTableGrid4.Name = "speedTableGrid4";
			this.speedTableGrid4.Rows = 5;
			this.speedTableGrid4.RowSelectColor = Color.FromArgb(95, 158, 160);
			this.speedTableGrid4.RowSelectType = 0;
			this.speedTableGrid4.RowsVisible = 5;
			this.speedTableGrid4.ScrollChennelColor = Color.FromArgb(100, 100, 100);
			this.speedTableGrid4.Size = new Size(156, 38);
			this.speedTableGrid4.SortColumnName = "";
			this.speedTableGrid4.SortType = SortType.Desc;
			this.speedTableGrid4.TabIndex = 65;
			this.contextLink.Items.AddRange(new ToolStripItem[]
			{
				this.tsmCallHistoricalChart,
				this.tsmCallNews,
				this.toolStripMenuItem1,
				this.tsmCallMarketWatch,
				this.tsmCallStockInPlay,
				this.tsmCallSaleByPrice,
				this.tsmCallSaleByTime,
				this.tsmCallOddlot
			});
			this.contextLink.Name = "contextMenuStrip1";
			this.contextLink.Size = new Size(212, 186);
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
			this.tsmCallMarketWatch.Name = "tsmCallMarketWatch";
			this.tsmCallMarketWatch.Size = new Size(211, 22);
			this.tsmCallMarketWatch.Text = "Market Watch";
			this.tsmCallMarketWatch.Click += new EventHandler(this.tsmCallMarketWatch_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = SystemColors.ControlDarkDark;
			base.ClientSize = new Size(1121, 497);
			base.Controls.Add(this.tStripMenu);
			base.Controls.Add(this.scrollbar1);
			base.Controls.Add(this.panelMain);
			base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			base.KeyPreview = true;
			base.Name = "frmTopBBOs";
			this.Text = "Top BBO";
			base.IDoShownDelay += new ClientBaseForm.OnShownDelayEventHandler(this.frmTopBBOs_IDoShownDelay);
			base.IDoLoadData += new ClientBaseForm.OnIDoLoadDataEventHandler(this.frmTopBBOs_IDoLoadData);
			base.IDoFontChanged += new ClientBaseForm.OnFontChangedEventHandler(this.frmTopBBOs_IDoFontChanged);
			base.IDoCustomSizeChanged += new ClientBaseForm.CustomSizeChangedEventHandler(this.frmTopBBOs_IDoCustomSizeChanged);
			base.IDoMainFormKeyUp += new ClientBaseForm.OnFormKeyUpEventHandler(this.frmTopBBOs_IDoMainFormKeyUp);
			base.IDoReActivated += new ClientBaseForm.OnReActiveEventHandler(this.frmTopBBOs_IDoReActivated);
			base.KeyDown += new KeyEventHandler(this.frmTopBBOs_KeyDown);
			base.Controls.SetChildIndex(this.panelMain, 0);
			base.Controls.SetChildIndex(this.scrollbar1, 0);
			base.Controls.SetChildIndex(this.tStripMenu, 0);
			this.panelMain.ResumeLayout(false);
			this.tStripMenu.ResumeLayout(false);
			this.tStripMenu.PerformLayout();
			this.contextLink.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmTopBBOs()
		{
			this.InitializeComponent();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmTopBBOs(Dictionary<string, object> propertiesValue) : base(propertiesValue)
		{
			this.InitializeComponent();
			try
			{
				this.bgwReloadData = new BackgroundWorker();
				this.bgwReloadData.WorkerReportsProgress = true;
				this.bgwReloadData.DoWork += new DoWorkEventHandler(this.bgwReloadData_DoWork);
				this.bgwReloadData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwReloadData_RunWorkerCompleted);
				this.bgwReloadData_TFEX = new BackgroundWorker();
				this.bgwReloadData_TFEX.WorkerReportsProgress = true;
				this.bgwReloadData_TFEX.DoWork += new DoWorkEventHandler(this.bgwReloadData_TFEX_DoWork);
				this.bgwReloadData_TFEX.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwReloadData_TFEX_RunWorkerCompleted);
			}
			catch (Exception ex)
			{
				this.ShowError("frmTopBBOs.Create", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetBlinkModeTopPrice()
		{
			try
			{
				base.IsLoadingData = true;
				ucBids[] bidControl = this._bidControl;
				for (int i = 0; i < bidControl.Length; i++)
				{
					ucBids ucBids = bidControl[i];
					ucBids.SetBlinkModeTopPrice();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SetBlinkModeTopPrice", ex);
			}
			base.IsLoadingData = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override Dictionary<string, object> DoPackProperties()
		{
			base.PropertiesValue.Clear();
			base.PropertiesValue.Add("SelectionText", this._selectionText);
			return base.PropertiesValue;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void DoUnpackProperties()
		{
			if (base.PropertiesValue != null)
			{
				if (base.PropertiesValue.ContainsKey("SelectionText"))
				{
					this._selectionText = base.PropertiesValue["SelectionText"].ToString();
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwReloadData_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				if (this.FormState != ClientBaseForm.ClientBaseFormState.Closing)
				{
					if (this.tdsTOPBBo == null)
					{
						this.tdsTOPBBo = new DataSet();
					}
					else
					{
						this.tdsTOPBBo.Clear();
					}
					base.IsLoadingData = true;
					int[] stockNumber = (int[])e.Argument;
					string text = ApplicationInfo.WebService.TopBBO(stockNumber, 5);
					if (!string.IsNullOrEmpty(text))
					{
						MyDataHelper.StringToDataSet(text, this.tdsTOPBBo);
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
			if (this.FormState != ClientBaseForm.ClientBaseFormState.Closing)
			{
				try
				{
					if (e.Error == null)
					{
						this.UpdateToControl();
						this.tdsTOPBBo.Clear();
					}
				}
				catch (Exception ex)
				{
					this.ShowError("bgwReloadData_RunWorkerCompleted", ex);
				}
				base.IsLoadingData = false;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwReloadData_TFEX_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				if (this.FormState != ClientBaseForm.ClientBaseFormState.Closing)
				{
					if (this.tdsTOPBBoTfex != null)
					{
						this.tdsTOPBBoTfex.Clear();
					}
					else
					{
						this.tdsTOPBBoTfex = new DataSet();
					}
					base.IsLoadingData = true;
					string[] array = (string[])e.Argument;
					if (array.Length > 0)
					{
						string text = ApplicationInfo.WebServiceTFEX.TopBBOTFEX(array, 5);
						if (!string.IsNullOrEmpty(text))
						{
							MyDataHelper.StringToDataSet(text, this.tdsTOPBBoTfex);
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("bgwReloadData_TFEX_DoWork", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwReloadData_TFEX_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				if (this.FormState != ClientBaseForm.ClientBaseFormState.Closing)
				{
					this.UpdateToControl_TFEX();
					this.tdsTOPBBoTfex.Clear();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("bgwReloadData_TFEX_RunWorkerCompleted", ex);
			}
			base.IsLoadingData = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetNewStock(string symbol, bool isPropertyChange)
		{
			try
			{
				StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[symbol];
				if (stockInformation.Number > 0)
				{
					ApplicationInfo.CurrentSymbol = stockInformation.Symbol;
					ApplicationInfo.FavStockChanged = true;
					ApplicationInfo.FavStockList[this._currFavPage][this._currentActivePanel - 1] = stockInformation.Symbol;
					this.ReloadData(new int[]
					{
						stockInformation.Number
					});
				}
				else
				{
					ApplicationInfo.CurrentSymbol = symbol;
					ApplicationInfo.FavStockChanged = true;
					ApplicationInfo.FavStockList[this._currFavPage][this._currentActivePanel - 1] = symbol;
					this.ReloadDataTFEX(new string[]
					{
						symbol
					});
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SetNewStock", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ReloadData(int[] stockNumber)
		{
			if (!this.bgwReloadData.IsBusy)
			{
				this.bgwReloadData.RunWorkerAsync(stockNumber);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ReloadDataTFEX(string[] symbolLst)
		{
			if (!this.bgwReloadData_TFEX.IsBusy)
			{
				this.bgwReloadData_TFEX.RunWorkerAsync(symbolLst);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateToControl()
		{
			try
			{
				if (this.tdsTOPBBo != null && this.tdsTOPBBo.Tables.Count > 0)
				{
					int stockNumber = 0;
					DataRow dataRow = null;
					StockList.StockInformation stockInformation = null;
					for (int i = 0; i < this.tdsTOPBBo.Tables["security_stat"].Rows.Count; i++)
					{
						if (this.tdsTOPBBo.Tables.Contains("security_stat") && this.tdsTOPBBo.Tables["security_stat"].Rows.Count > 0)
						{
							dataRow = this.tdsTOPBBo.Tables["security_stat"].Rows[i];
							stockNumber = Convert.ToInt32(dataRow["security_number"]);
							stockInformation = ApplicationInfo.StockInfo[stockNumber];
						}
						int num = ApplicationInfo.FavStockList[this._currFavPage].IndexOf(stockInformation.Symbol);
						if (num > -1 && num < 8)
						{
							DataRow[] drTicker = null;
							if (this.tdsTOPBBo.Tables.Contains("last_sale"))
							{
								if (this.tdsTOPBBo.Tables["last_sale"].Columns.Contains("security_number"))
								{
									drTicker = this.tdsTOPBBo.Tables["last_sale"].Select("security_number = '" + stockNumber.ToString() + "'");
								}
								else
								{
									drTicker = this.tdsTOPBBo.Tables["last_sale"].Select("0=0");
								}
							}
							this._bidControl[num].UpdateToControl(dataRow, drTicker);
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateToControl", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateToControl_TFEX()
		{
			try
			{
				if (this.tdsTOPBBoTfex != null && this.tdsTOPBBoTfex.Tables.Count > 0 && this.tdsTOPBBoTfex.Tables.Contains("info"))
				{
					DataRow dataRow = null;
					string text = string.Empty;
					int num = 0;
					for (int i = 0; i < this.tdsTOPBBoTfex.Tables["info"].Rows.Count; i++)
					{
						if (this.tdsTOPBBoTfex.Tables["info"].Rows.Count > 0)
						{
							dataRow = this.tdsTOPBBoTfex.Tables["info"].Rows[i];
							text = dataRow["sSeries"].ToString().Trim();
							int.TryParse(dataRow["iDecPrice"].ToString(), out num);
						}
						int num2 = ApplicationInfo.FavStockList[this._currFavPage].IndexOf(text);
						if (num2 > -1 && num2 < 8)
						{
							DataRow[] drTicker = null;
							if (this.tdsTOPBBoTfex.Tables.Contains("last_sale"))
							{
								if (this.tdsTOPBBoTfex.Tables["last_sale"].Columns.Contains("sSeries"))
								{
									drTicker = this.tdsTOPBBoTfex.Tables["last_sale"].Select("sSeries = '" + text.Trim() + "'");
								}
								else
								{
									drTicker = this.tdsTOPBBoTfex.Tables["last_sale"].Select("0=0");
								}
							}
							this._bidControl[num2].UpdateToControl_TFEX(dataRow, drTicker);
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateToControl_TFEX", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateTopPrice(StockList.StockInformation sInfo, string side, decimal price1, decimal price2, decimal price3, decimal price4, decimal price5, long volume1, long volume2, long volume3, long volume4, long volume5, SortGrid intzaTP)
		{
			if (intzaTP.InvokeRequired)
			{
				intzaTP.Invoke(new frmTopBBOs.UpdateTopPriceCallBack(this.UpdateTopPrice), new object[]
				{
					sInfo,
					side,
					price1,
					price2,
					price3,
					price4,
					price5,
					volume1,
					volume2,
					volume3,
					volume4,
					volume5,
					intzaTP
				});
			}
			else
			{
				try
				{
					Color fontColor = (Brushes.Yellow as SolidBrush).Color;
					if (side == "B")
					{
						if (price1 > -1m)
						{
							fontColor = Utilities.ComparePriceCFColor(price1, sInfo);
							intzaTP.Records(0).Fields("bidvolume").Text = volume1;
							intzaTP.Records(0).Fields("bidvolume").FontColor = fontColor;
							intzaTP.Records(0).Fields("bid").Text = Utilities.BidOfferPriceFormat(price1, volume1);
							intzaTP.Records(0).Fields("bid").FontColor = fontColor;
						}
						if (price2 > -1m)
						{
							fontColor = Utilities.ComparePriceCFColor(price2, sInfo);
							intzaTP.Records(1).Fields("bidvolume").Text = volume2;
							intzaTP.Records(1).Fields("bidvolume").FontColor = fontColor;
							intzaTP.Records(1).Fields("bid").Text = Utilities.PriceFormat(price2);
							intzaTP.Records(1).Fields("bid").FontColor = fontColor;
						}
						if (price3 > -1m)
						{
							fontColor = Utilities.ComparePriceCFColor(price3, sInfo);
							intzaTP.Records(2).Fields("bidvolume").Text = volume3;
							intzaTP.Records(2).Fields("bidvolume").FontColor = fontColor;
							intzaTP.Records(2).Fields("bid").Text = Utilities.PriceFormat(price3);
							intzaTP.Records(2).Fields("bid").FontColor = fontColor;
						}
						if (price4 > -1m)
						{
							fontColor = Utilities.ComparePriceCFColor(price4, sInfo);
							intzaTP.Records(3).Fields("bidvolume").Text = volume4;
							intzaTP.Records(3).Fields("bidvolume").FontColor = fontColor;
							intzaTP.Records(3).Fields("bid").Text = Utilities.PriceFormat(price4);
							intzaTP.Records(3).Fields("bid").FontColor = fontColor;
						}
						if (price5 > -1m)
						{
							fontColor = Utilities.ComparePriceCFColor(price5, sInfo);
							intzaTP.Records(4).Fields("bidvolume").Text = volume5;
							intzaTP.Records(4).Fields("bidvolume").FontColor = fontColor;
							intzaTP.Records(4).Fields("bid").Text = Utilities.PriceFormat(price5);
							intzaTP.Records(4).Fields("bid").FontColor = fontColor;
						}
					}
					else
					{
						if (side == "S")
						{
							if (price1 > -1m)
							{
								fontColor = Utilities.ComparePriceCFColor(price1, sInfo);
								intzaTP.Records(0).Fields("offervolume").Text = volume1;
								intzaTP.Records(0).Fields("offervolume").FontColor = fontColor;
								intzaTP.Records(0).Fields("offer").Text = Utilities.BidOfferPriceFormat(price1, volume1);
								intzaTP.Records(0).Fields("offer").FontColor = fontColor;
							}
							if (price2 > -1m)
							{
								fontColor = Utilities.ComparePriceCFColor(price2, sInfo);
								intzaTP.Records(1).Fields("offervolume").Text = volume2;
								intzaTP.Records(1).Fields("offervolume").FontColor = fontColor;
								intzaTP.Records(1).Fields("offer").Text = Utilities.PriceFormat(price2);
								intzaTP.Records(1).Fields("offer").FontColor = fontColor;
							}
							if (price3 > -1m)
							{
								fontColor = Utilities.ComparePriceCFColor(price3, sInfo);
								intzaTP.Records(2).Fields("offervolume").Text = volume3;
								intzaTP.Records(2).Fields("offervolume").FontColor = fontColor;
								intzaTP.Records(2).Fields("offer").Text = Utilities.PriceFormat(price3);
								intzaTP.Records(2).Fields("offer").FontColor = fontColor;
							}
							if (price4 > -1m)
							{
								fontColor = Utilities.ComparePriceCFColor(price4, sInfo);
								intzaTP.Records(3).Fields("offervolume").Text = volume4;
								intzaTP.Records(3).Fields("offervolume").FontColor = fontColor;
								intzaTP.Records(3).Fields("offer").Text = Utilities.PriceFormat(price4);
								intzaTP.Records(3).Fields("offer").FontColor = fontColor;
							}
							if (price5 > -1m)
							{
								fontColor = Utilities.ComparePriceCFColor(price5, sInfo);
								intzaTP.Records(4).Fields("offervolume").Text = volume5;
								intzaTP.Records(4).Fields("offervolume").FontColor = fontColor;
								intzaTP.Records(4).Fields("offer").Text = Utilities.PriceFormat(price5);
								intzaTP.Records(4).Fields("offer").FontColor = fontColor;
							}
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("UpdateTopPrice", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateLastPrice(StockList.StockInformation stockInfo, decimal lastPrice, decimal high, decimal low, IntzaCustomGrid intzaHeader)
		{
			if (intzaHeader.InvokeRequired)
			{
				intzaHeader.Invoke(new frmTopBBOs.UpdateLastPriceCallback(this.UpdateLastPrice), new object[]
				{
					stockInfo,
					lastPrice,
					high,
					low,
					intzaHeader
				});
			}
			else
			{
				try
				{
					if (stockInfo.PriorPrice > 0m && lastPrice > 0m)
					{
						Color fontColor = Utilities.ComparePriceCFColor(lastPrice, stockInfo);
						intzaHeader.Item("price").Text = lastPrice.ToString();
						intzaHeader.Item("price").FontColor = fontColor;
						intzaHeader.Item("chg").Text = Utilities.PriceFormat(stockInfo.ChangePrice, true, "");
						if (intzaHeader.Item("chg").Text == string.Empty)
						{
							intzaHeader.Item("chg").Text = "    ";
						}
						intzaHeader.Item("chg").FontColor = fontColor;
						intzaHeader.Item("pchg").Text = Utilities.PriceFormat(stockInfo.ChangePricePct, true, "");
						if (intzaHeader.Item("pchg").Text == string.Empty)
						{
							intzaHeader.Item("pchg").Text = "        ";
						}
						intzaHeader.Item("pchg").FontColor = fontColor;
					}
					else
					{
						lastPrice = stockInfo.PriorPrice;
						intzaHeader.Item("price").Text = stockInfo.PriorPrice.ToString();
						intzaHeader.Item("price").FontColor = Color.Yellow;
						intzaHeader.Item("chg").Text = "";
						intzaHeader.Item("pchg").Text = "";
					}
					intzaHeader.Item("high").Text = high.ToString();
					intzaHeader.Item("high").FontColor = Utilities.ComparePriceCFColor(high, stockInfo);
					intzaHeader.Item("low").Text = low.ToString();
					intzaHeader.Item("low").FontColor = Utilities.ComparePriceCFColor(low, stockInfo);
					if (base.IsAllowRender)
					{
						intzaHeader.EndUpdate();
					}
				}
				catch (Exception ex)
				{
					this.ShowError("UpdateLastSalePrice", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateAllVolume(long openVolume, long buyVolume, long sellVolume, IntzaCustomGrid intzaVolumeByBoard)
		{
			if (intzaVolumeByBoard.InvokeRequired)
			{
				intzaVolumeByBoard.Invoke(new frmTopBBOs.UpdateAllVolumeCallBack(this.UpdateAllVolume), new object[]
				{
					openVolume,
					buyVolume,
					sellVolume,
					intzaVolumeByBoard
				});
			}
			else
			{
				try
				{
					intzaVolumeByBoard.Item("open_vol").Text = openVolume.ToString();
					intzaVolumeByBoard.Item("buy_vol").Text = buyVolume.ToString();
					intzaVolumeByBoard.Item("sell_vol").Text = sellVolume.ToString();
				}
				catch (Exception ex)
				{
					this.ShowError("UpdateAllVolume", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateLastSaleTicker(StockList.StockInformation stockInfo, decimal price, string side, long volume, string lastUpdate, int index, SortGrid intzaLS)
		{
			if (intzaLS.InvokeRequired)
			{
				intzaLS.Invoke(new frmTopBBOs.UpdateLastSaleTickerCallBack(this.UpdateLastSaleTicker), new object[]
				{
					stockInfo,
					price,
					side,
					volume,
					lastUpdate,
					index,
					intzaLS
				});
			}
			else
			{
				try
				{
					RecordItem recordItem;
					if (index == -1)
					{
						recordItem = intzaLS.AddRecord(1, true);
					}
					else
					{
						recordItem = intzaLS.Records(index);
					}
					recordItem.Fields("volume").Text = volume.ToString();
					recordItem.Fields("side").Text = side;
					recordItem.Fields("price").Text = Utilities.PriceFormat(price);
					Color fontColor = Utilities.ComparePriceCFColor(price, stockInfo);
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
					recordItem.Changed = true;
				}
				catch (Exception ex)
				{
					this.ShowError("UpdateLastSaleTicker", ex);
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
					string messageType = message.MessageType;
					if (messageType != null)
					{
						if (!(messageType == "TP"))
						{
							if (!(messageType == "L+"))
							{
								if (messageType == "SS")
								{
									int num = ApplicationInfo.FavStockList[this._currFavPage].IndexOf(realtimeStockInfo.Symbol);
									if (num > -1 && num < 8)
									{
										this._bidControl[num].ReceiveMessage(message, realtimeStockInfo);
									}
								}
							}
							else
							{
								int num = ApplicationInfo.FavStockList[this._currFavPage].IndexOf(realtimeStockInfo.Symbol);
								if (num > -1 && num < 8)
								{
									this._bidControl[num].ReceiveMessage(message, realtimeStockInfo);
								}
							}
						}
						else
						{
							int num = ApplicationInfo.FavStockList[this._currFavPage].IndexOf(realtimeStockInfo.Symbol);
							if (num > -1 && num < 8)
							{
								this._bidControl[num].ReceiveMessage(message, realtimeStockInfo);
							}
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("SecurityInfo::RecvMessage", ex);
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
						if (message.MessageType == "TP")
						{
							if (realtimeSeriesInfo.Symbol != "")
							{
								int num = ApplicationInfo.FavStockList[this._currFavPage].IndexOf(realtimeSeriesInfo.Symbol);
								if (num > -1 && num < 8)
								{
									this._bidControl[num].ReceiveTfexMessage(message, realtimeSeriesInfo);
								}
							}
						}
						else
						{
							if (message.MessageType == "LS")
							{
								if (realtimeSeriesInfo.Symbol != "")
								{
									int num = ApplicationInfo.FavStockList[this._currFavPage].IndexOf(realtimeSeriesInfo.Symbol);
									if (num > -1 && num < 8)
									{
										this._bidControl[num].ReceiveTfexMessage(message, realtimeSeriesInfo);
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
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmTopBBOs_IDoShownDelay()
		{
			this._bidControl = new ucBids[8];
			this._bidControl[0] = this.ucBids1;
			this._bidControl[1] = this.ucBids2;
			this._bidControl[2] = this.ucBids3;
			this._bidControl[3] = this.ucBids4;
			this._bidControl[4] = this.ucBids5;
			this._bidControl[5] = this.ucBids6;
			this._bidControl[6] = this.ucBids7;
			this._bidControl[7] = this.ucBids8;
			this.SetBlinkModeTopPrice();
			this.SetResize(true, false);
			base.Show();
			base.OpenedForm();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmTopBBOs_IDoLoadData()
		{
			try
			{
				if (this._selectionText == string.Empty)
				{
					this._selectionText = this.tscbSelection.Items[0].ToString();
				}
				this.setFavPage(this._selectionText);
			}
			catch (Exception ex)
			{
				this.ShowError("frmTopBBOs_IDoLoadData", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetResize(bool isWidthChanged, bool isHeightChanged)
		{
			try
			{
				if (isWidthChanged || isHeightChanged)
				{
					int num = base.ClientSize.Width - this.scrollbar1.Width;
					int width = num / 2;
					this.ucBids1.SetResize();
					this.ucBids2.SetResize();
					this.ucBids3.SetResize();
					this.ucBids4.SetResize();
					this.ucBids5.SetResize();
					this.ucBids6.SetResize();
					this.ucBids7.SetResize();
					this.ucBids8.SetResize();
					this.ucBids1.SetBounds(0, 0, width, this.ucBids1.Height);
					this.ucBids2.SetBounds(this.ucBids1.Left, this.ucBids1.Bottom + 1, this.ucBids1.Width, this.ucBids1.Height);
					this.ucBids3.SetBounds(this.ucBids1.Left, this.ucBids2.Bottom + 1, this.ucBids1.Width, this.ucBids1.Height);
					this.ucBids4.SetBounds(this.ucBids1.Left, this.ucBids3.Bottom + 1, this.ucBids1.Width, this.ucBids1.Height);
					this.ucBids5.SetBounds(this.ucBids1.Right + 1, this.ucBids1.Top, this.ucBids1.Width, this.ucBids1.Height);
					this.ucBids6.SetBounds(this.ucBids5.Left, this.ucBids2.Top, this.ucBids1.Width, this.ucBids1.Height);
					this.ucBids7.SetBounds(this.ucBids5.Left, this.ucBids3.Top, this.ucBids1.Width, this.ucBids1.Height);
					this.ucBids8.SetBounds(this.ucBids5.Left, this.ucBids4.Top, this.ucBids1.Width, this.ucBids1.Height);
					int num2 = this.ucBids1.Height * 4 + 3;
					int height = (base.Height > num2) ? base.Height : num2;
					this.panelMain.SetBounds(0, this.tStripMenu.Top + this.tStripMenu.Height, num, height);
					this.scrollbar1.SetBounds(this.panelMain.Left + this.panelMain.Width, this.panelMain.Top, this.scrollbar1.Width, base.ClientSize.Height - this.panelMain.Top);
					this.scrollbar1.Maximum = num2;
					this.scrollbar1.LargeChange = base.Height - this.panelMain.Top;
					this.scrollbar1.Value = 0;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SetResize", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmTopBBOs_IDoCustomSizeChanged()
		{
			if (!base.IsLoadingData)
			{
				this.SetResize(this.IsWidthChanged, this.IsHeightChanged);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmTopBBOs_IDoMainFormKeyUp(KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode == Keys.Space)
			{
				this.SetTextPosition(this._currentActivePanel);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmTopBBOs_IDoFontChanged()
		{
			if (!base.IsLoadingData)
			{
				base.SuspendLayout();
				this.SetResize(true, true);
				base.ResumeLayout();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmTopBBOs_IDoReActivated()
		{
			if (!base.IsLoadingData)
			{
				this.SetResize(this.IsWidthChanged, this.IsHeightChanged);
				base.Show();
				if (ApplicationInfo.FavStockChanged || ApplicationInfo.IsResumeState)
				{
					this.setFavPage(this._selectionText);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmTopBBOs_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
			case Keys.Left:
				if (this._currentActivePanel - 4 > 0)
				{
					this.SetTextPosition(this._currentActivePanel - 4);
				}
				else
				{
					this.SetTextPosition(this._currentActivePanel + 4);
				}
				e.SuppressKeyPress = true;
				break;
			case Keys.Up:
				this.SetTextPosition(this._currentActivePanel - 1);
				e.SuppressKeyPress = true;
				break;
			case Keys.Right:
				if (this._currentActivePanel + 4 > 8)
				{
					this.SetTextPosition(this._currentActivePanel - 4);
				}
				else
				{
					this.SetTextPosition(this._currentActivePanel + 4);
				}
				e.SuppressKeyPress = true;
				break;
			case Keys.Down:
				this.SetTextPosition(this._currentActivePanel + 1);
				e.SuppressKeyPress = true;
				break;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetTextPosition(int toCurrentBox)
		{
			try
			{
				if (toCurrentBox > 8 || toCurrentBox == 0)
				{
					toCurrentBox = 1;
				}
				else
				{
					if (toCurrentBox < 1)
					{
						toCurrentBox = 8;
					}
				}
				if (this._bidControl[toCurrentBox - 1].Top + this.ucBids1.Height > base.Height + this.scrollbar1.Value)
				{
					this.scrollbar1.Value = this._bidControl[toCurrentBox - 1].Top + this.ucBids1.Height - (base.Height - this.tStripMenu.Height);
				}
				else
				{
					if (this._bidControl[toCurrentBox - 1].Top < this.scrollbar1.Value)
					{
						this.scrollbar1.Value = this._bidControl[toCurrentBox - 1].Top;
					}
				}
				this._bidControl[toCurrentBox - 1].SetTextPosition();
			}
			catch (Exception ex)
			{
				this.ShowError("SetTextPosition", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void scrollbar1_ValueChanged(object sender, EventArgs e)
		{
			this.panelMain.Top = -this.scrollbar1.Value + this.tStripMenu.Top + this.tStripMenu.Height;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tscbSelection_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!base.IsLoadingData)
			{
				this.setFavPage(this.tscbSelection.Text);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void setFavPage(string selectionText)
		{
			try
			{
				if (this.tscbSelection.Items.IndexOf(selectionText) > -1)
				{
					base.IsLoadingData = true;
					this._selectionText = selectionText;
					this.tscbSelection.Text = selectionText;
					this._currFavPage = this.tscbSelection.Items.IndexOf(selectionText) + 1;
					for (int i = 1; i <= 8; i++)
					{
						this._bidControl[i - 1].ClearControl(true);
					}
					ApplicationInfo.FavStockChanged = false;
					string empty = string.Empty;
					string empty2 = string.Empty;
					ApplicationInfo.GetFavListByPage(this._currFavPage, 8, false, ref empty, ref empty2);
					if (empty.Length > 0)
					{
						string[] array = empty.Split(new char[]
						{
							','
						});
						if (array.Length > 0)
						{
							int[] array2 = new int[array.Length];
							for (int j = 0; j < array.Length; j++)
							{
								array2[j] = Convert.ToInt32(array[j]);
							}
							this.ReloadData(array2);
						}
					}
					else
					{
						base.IsLoadingData = false;
					}
					if (empty2.Length > 0)
					{
						string[] array3 = empty2.Split(new char[]
						{
							','
						});
						if (array3.Length > 0)
						{
							this.ReloadDataTFEX(array3);
						}
					}
					else
					{
						base.IsLoadingData = false;
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tscbSelection_SelectedIndexChanged", ex);
				base.IsLoadingData = false;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnAdd_Click(object sender, EventArgs e)
		{
			this.SetTextPosition(this._currentActivePanel + 1);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnDel_Click(object sender, EventArgs e)
		{
			this.ClearStock();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ClearStock()
		{
			try
			{
				if (this._currentActivePanel > 0 && this._currentActivePanel <= 8)
				{
					int num = (this._currFavPage - 1) * ApplicationInfo.FavStockPerPage + this._currentActivePanel - 1;
					ApplicationInfo.FavStockChanged = true;
					ApplicationInfo.FavStockList[this._currFavPage][this._currentActivePanel - 1] = string.Empty;
					this._bidControl[this._currentActivePanel - 1].ClearControl(true);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tsbtnDel_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tStripMenu_Paint(object sender, PaintEventArgs e)
		{
			if (e.ClipRectangle.Width == this.tStripMenu.Width)
			{
				e.Graphics.DrawLine(Pens.Gray, 0, e.ClipRectangle.Height - 1, e.ClipRectangle.Width, e.ClipRectangle.Height - 1);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ucBids1_OnNewStock(object sender, string stock)
		{
			try
			{
				if (this._currentActivePanel > 0)
				{
					if (!string.IsNullOrEmpty(stock))
					{
						StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[stock];
						if (stockInformation.Number > 0)
						{
							if (ApplicationInfo.FavStockList[this._currFavPage].IndexOf(stockInformation.Symbol) == -1)
							{
								this.SetNewStock(stockInformation.Symbol, true);
							}
							else
							{
								((ucBids)sender).ResetToCurrentStock();
							}
						}
						else
						{
							SeriesList.SeriesInformation seriesInformation = ApplicationInfo.SeriesInfo[stock];
							if (seriesInformation.Symbol != string.Empty)
							{
								if (ApplicationInfo.FavStockList[this._currFavPage].IndexOf(seriesInformation.Symbol) == -1)
								{
									this.SetNewStock(seriesInformation.Symbol, true);
								}
								else
								{
									((ucBids)sender).ResetToCurrentStock();
								}
							}
						}
					}
					else
					{
						this.ClearStock();
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("txtStock_KeyDown", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ucBids_Enter(object sender, EventArgs e)
		{
			if (sender == this.ucBids1)
			{
				this._currentActivePanel = 1;
			}
			else
			{
				if (sender == this.ucBids2)
				{
					this._currentActivePanel = 2;
				}
				else
				{
					if (sender == this.ucBids3)
					{
						this._currentActivePanel = 3;
					}
					else
					{
						if (sender == this.ucBids4)
						{
							this._currentActivePanel = 4;
						}
						else
						{
							if (sender == this.ucBids5)
							{
								this._currentActivePanel = 5;
							}
							else
							{
								if (sender == this.ucBids6)
								{
									this._currentActivePanel = 6;
								}
								else
								{
									if (sender == this.ucBids7)
									{
										this._currentActivePanel = 7;
									}
									else
									{
										if (sender == this.ucBids8)
										{
											this._currentActivePanel = 8;
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
		private void ucBids_Leave(object sender, EventArgs e)
		{
			try
			{
				if (this._currentActivePanel > 0)
				{
					this._bidControl[this._currentActivePanel - 1].HideTextBox();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("ucBids_Leave", ex);
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
		private void tsmCallMarketWatch_Click(object sender, EventArgs e)
		{
			if (this.contextLink.Tag != null && this.contextLink.Tag.ToString() != string.Empty)
			{
				TemplateManager.Instance.MainForm.SetTemplateLink("Market Watch", string.Empty, this.contextLink.Tag.ToString());
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ucBids_OnLink(object sender, string stock, Point point)
		{
			try
			{
				this.contextLink.Tag = stock;
				this.contextLink.Show((ucBids)sender, point);
			}
			catch (Exception ex)
			{
				this.ShowError("ucBids_OnLink", ex);
			}
		}
	}
}
