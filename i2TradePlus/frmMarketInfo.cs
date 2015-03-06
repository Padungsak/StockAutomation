using i2TradePlus.Information;
using ITSNet.Common.BIZ;
using ITSNet.Common.BIZ.RealtimeMessage;
using STIControl;
using STIControl.CustomGrid;
using STIControl.SortTableGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace i2TradePlus
{
	public class frmMarketInfo : ClientBaseForm, IRealtimeMessage
	{
		private struct RecordData
		{
			public int Number;
			public string Symbol;
			public decimal IndexPrior;
			public decimal Index;
			public long AccVolume;
			public decimal AccValue;
			public decimal Mkt;
		}
		private delegate void ShowSplashChartCallBack(bool visible);
		private delegate void ShowSplashCallBack(bool visible);
		private const int currentTop = 40;
		private const int SELECT_SECTOR = 0;
		private const int SELECT_INDUSTRY = 1;
		private int IndustryNumber = -1;
		private frmMarketInfo.RecordData _recvDataRealTime = default(frmMarketInfo.RecordData);
		private BackgroundWorker bgwReloadData = null;
		private DataSet tdsSectorInfo = null;
		private SortType sortType = SortType.Asc;
		private BackgroundWorker bgwMarketInfoLoadData = null;
		private BackgroundWorker bgwReloadChart = null;
		private DataSet dsMarketInfo = null;
		private int selectionMenu = 0;
		private bool isLoading = true;
		private string _currentChartName = string.Empty;
		private IContainer components = null;
		private ToolStrip tStripMenu;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripSeparator toolStripSeparator2;
		private ToolStripButton tsSortBySector;
		private ToolStripButton tsSortByVolume;
		private ToolStripButton tsSortByValues;
		private Panel panelSector;
		private IntzaCustomGrid intzaMarketInfo;
		private ToolStripLabel tsSortBy;
		private ToolStrip toolStrip1;
		private ToolStripButton tsbtnInfo;
		private ToolStripButton tsbtnSETChart;
		private ToolStripButton tsbtnSET50Chart;
		private ToolStripButton tsbtnSET100Chart;
		private ToolStripButton tsbtnMaiChart;
		private ToolStripSeparator toolStripSeparator3;
		private PictureBox pictureBox1;
		private Label lbChartLoading;
		private ToolStripButton tsbtnSETHdChart;
		private Label lbLoading2;
		private ToolStripButton tsbtnSector;
		private ToolStripButton tsbtnIndustry;
		private ToolStripLabel toolStripLabel2;
		private ToolStripButton tsbtnSortAsc;
		private ToolStripButton tsbtnSortDesc;
		private SortGrid intzaSET;
		private SortGrid intzaSector;
		private ToolStripLabel toolStripLabel1;
		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmMarketInfo()
		{
			this.InitializeComponent();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmMarketInfo(Dictionary<string, object> properties) : base(properties)
		{
			this.InitializeComponent();
			try
			{
				this.bgwReloadData = new BackgroundWorker();
				this.bgwReloadData.WorkerSupportsCancellation = true;
				this.bgwReloadData.WorkerReportsProgress = true;
				this.bgwReloadData.DoWork += new DoWorkEventHandler(this.bgwReloadData_DoWork);
				this.bgwReloadData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwReloadData_RunWorkerCompleted);
				this.bgwMarketInfoLoadData = new BackgroundWorker();
				this.bgwMarketInfoLoadData.DoWork += new DoWorkEventHandler(this.bgwMarketInfoLoadData_DoWork);
				this.bgwMarketInfoLoadData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwMarketInfoLoadData_RunWorkerCompleted);
				this.bgwReloadChart = new BackgroundWorker();
				this.bgwReloadChart.WorkerReportsProgress = true;
				this.bgwReloadChart.DoWork += new DoWorkEventHandler(this.bgwReloadChart_DoWork);
				this.bgwReloadChart.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwReloadChart_RunWorkerCompleted);
				this.intzaSET.Records(0).Fields("name").Text = "SET";
				this.intzaSET.Records(3).Fields("name").Text = "SET50";
				this.intzaSET.Records(6).Fields("name").Text = "SET100";
				this.intzaSET.Records(9).Fields("name").Text = "MAI";
				this.intzaSET.Records(12).Fields("name").Text = "SETHD";
				this.intzaSET.Records(0).Fields("name").FontColor = Color.Black;
				this.intzaSET.Records(3).Fields("name").FontColor = Color.Black;
				this.intzaSET.Records(6).Fields("name").FontColor = Color.Black;
				this.intzaSET.Records(9).Fields("name").FontColor = Color.Black;
				this.intzaSET.Records(12).Fields("name").FontColor = Color.Black;
				this.intzaSET.Records(0).Fields("name").BackColor = Color.Gold;
				this.intzaSET.Records(3).Fields("name").BackColor = Color.Gold;
				this.intzaSET.Records(6).Fields("name").BackColor = Color.Gold;
				this.intzaSET.Records(9).Fields("name").BackColor = Color.Gold;
				this.intzaSET.Records(12).Fields("name").BackColor = Color.Gold;
				this.intzaSET.Records(0).Fields("name").FontStyle = FontStyle.Bold;
				this.intzaSET.Records(3).Fields("name").FontStyle = FontStyle.Bold;
				this.intzaSET.Records(6).Fields("name").FontStyle = FontStyle.Bold;
				this.intzaSET.Records(9).Fields("name").FontStyle = FontStyle.Bold;
				this.intzaSET.Records(12).Fields("name").FontStyle = FontStyle.Bold;
				this.intzaSET.Records(1).Fields("name").Text = "High :";
				this.intzaSET.Records(4).Fields("name").Text = "High :";
				this.intzaSET.Records(7).Fields("name").Text = "High :";
				this.intzaSET.Records(10).Fields("name").Text = "High :";
				this.intzaSET.Records(13).Fields("name").Text = "High :";
				this.intzaSET.Records(1).Fields("name").FontColor = Color.Cyan;
				this.intzaSET.Records(4).Fields("name").FontColor = Color.Cyan;
				this.intzaSET.Records(7).Fields("name").FontColor = Color.Cyan;
				this.intzaSET.Records(10).Fields("name").FontColor = Color.Cyan;
				this.intzaSET.Records(13).Fields("name").FontColor = Color.Cyan;
				this.intzaSET.Records(2).Fields("name").Text = "Low :";
				this.intzaSET.Records(5).Fields("name").Text = "Low :";
				this.intzaSET.Records(8).Fields("name").Text = "Low :";
				this.intzaSET.Records(11).Fields("name").Text = "Low :";
				this.intzaSET.Records(14).Fields("name").Text = "Low :";
				this.intzaSET.Records(2).Fields("name").FontColor = Color.Magenta;
				this.intzaSET.Records(5).Fields("name").FontColor = Color.Magenta;
				this.intzaSET.Records(8).Fields("name").FontColor = Color.Magenta;
				this.intzaSET.Records(11).Fields("name").FontColor = Color.Magenta;
				this.intzaSET.Records(14).Fields("name").FontColor = Color.Magenta;
				this.intzaSET.Records(0).BackColor = Color.FromArgb(30, 30, 30);
				this.intzaSET.Records(3).BackColor = Color.FromArgb(30, 30, 30);
				this.intzaSET.Records(6).BackColor = Color.FromArgb(30, 30, 30);
				this.intzaSET.Records(9).BackColor = Color.FromArgb(30, 30, 30);
				this.intzaSET.Records(12).BackColor = Color.FromArgb(30, 30, 30);
			}
			catch (Exception ex)
			{
				this.ShowError("frmMarketInfo", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwReloadData_DoWork(object sender, DoWorkEventArgs e)
		{
			if (this.FormState != ClientBaseForm.ClientBaseFormState.Closing)
			{
				if (this.tdsSectorInfo == null)
				{
					this.tdsSectorInfo = new DataSet();
				}
				this.ShowSplash2(true);
				string text = string.Empty;
				try
				{
					if (this.selectionMenu == 0)
					{
						text = ApplicationInfo.WebService.SectorStat();
					}
					else
					{
						text = ApplicationInfo.WebService.IndustryStat();
					}
					this.tdsSectorInfo.Clear();
					if (!string.IsNullOrEmpty(text))
					{
						MyDataHelper.StringToDataSet(text, this.tdsSectorInfo);
					}
				}
				catch (Exception ex)
				{
					this.ShowError("RequestWebData", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwReloadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				if (this.FormState != ClientBaseForm.ClientBaseFormState.Closing)
				{
					this.UpdateToControl(this.tdsSectorInfo);
					this.tdsSectorInfo.Clear();
					this.ShowSplash2(false);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("bgwReloadData_RunWorkerCompleted", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmSectorInformation_IDOShownDelay()
		{
			try
			{
				this.SetResize(true);
				this.SetTextSortColumn("symbol");
				this.SetSort(SortType.Asc);
				this.SetPage(0);
				this.MarketInfoReloadDataSETindex();
				base.Show();
			}
			catch (Exception ex)
			{
				this.ShowError("frmSectorInformation_IDOShownDelay", ex);
			}
			base.OpenedForm();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmMarketInfo_IDoLoadData()
		{
			try
			{
				this.MarketInfoReloadData();
				this.SetChart("SET", this.tsbtnSETChart);
			}
			catch (Exception ex)
			{
				this.ShowError("frmMarketInfo_IDoLoadData", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmSectorStatistic_IDoReActivated()
		{
			if (!this.isLoading)
			{
				this.SetResize(this.IsWidthChanged || this.IsHeightChanged);
				base.Show();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmSectorStatistic_IDoFontChanged()
		{
			if (!this.isLoading)
			{
				this.SetResize(true);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReceiveMessage(IBroadcastMessage message, StockList.StockInformation realtimeStockInfo)
		{
			if (!this.isLoading)
			{
				if (message.MessageType == "IE")
				{
					IEMessage iEMessage = (IEMessage)message;
					IndexStat.IndexItem indexItem = ApplicationInfo.IndexStatInfo[iEMessage.Symbol];
					if (indexItem != null)
					{
						int num = this.intzaSector.FindIndex("symbol", indexItem.Symbol);
						if (num > -1)
						{
							this._recvDataRealTime.Symbol = indexItem.Symbol;
							this._recvDataRealTime.Number = indexItem.Number;
							this._recvDataRealTime.AccVolume = iEMessage.AccVolume;
							this._recvDataRealTime.AccValue = iEMessage.AccValue;
							this._recvDataRealTime.Index = iEMessage.IndexValue;
							IndexStat.IndexItem indexItem2 = ApplicationInfo.IndexStatInfo[".SET"];
							if (indexItem2 != null)
							{
								if (indexItem2.AccValue > 0m)
								{
									this._recvDataRealTime.Mkt = iEMessage.AccValue / indexItem2.AccValue * 100m;
								}
							}
							this._recvDataRealTime.IndexPrior = indexItem.Prior;
							this.UpdateToGrid(num + 1, this._recvDataRealTime);
							if (base.IsAllowRender)
							{
								this.intzaSector.EndUpdate(num);
							}
						}
					}
				}
				else
				{
					if (message.MessageType.ToUpper() == "IS")
					{
						ISMessage iSMessage = (ISMessage)message;
						this.ShowSetIndex(iSMessage.Symbol);
						if (base.IsAllowRender)
						{
							this.intzaSET.EndUpdate();
						}
						if (iSMessage.Symbol == ".SET")
						{
							this.MarketInfoUpdateByMT(iSMessage.MainAccVolume, iSMessage.MainAccValue, iSMessage.OddlotAccVolume, iSMessage.OddlotAccValue, iSMessage.BiglotAccVolume, iSMessage.BiglotAccValue, iSMessage.ForeignAccVolume, iSMessage.ForeignAccValue);
							this.MarketInfoUpdateByIS(iSMessage.Tick, iSMessage.Trin, iSMessage.SecurityUp, iSMessage.SecurityDown, iSMessage.SecurityNoChange, iSMessage.UpVolume * 1000L, iSMessage.DownVolume * 1000L, iSMessage.NoChangeVolume * 1000L);
						}
						if (base.IsAllowRender)
						{
							this.intzaMarketInfo.EndUpdate();
						}
					}
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReceiveTfexMessage(IBroadcastMessage message, SeriesList.SeriesInformation realtimeSeriesInfo)
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateToGrid(int rowIndex, frmMarketInfo.RecordData recordData)
		{
			try
			{
				if (!string.IsNullOrEmpty(recordData.Symbol))
				{
					RecordItem recordItem = this.intzaSector.Records(rowIndex - 1);
					if (recordItem != null)
					{
						recordItem.Fields("symbol").Text = recordData.Symbol;
						recordItem.Fields("prior").Text = recordData.IndexPrior;
						recordItem.Fields("last").Text = recordData.Index;
						recordItem.Fields("volume").Text = recordData.AccVolume;
						recordItem.Fields("value").Text = recordData.AccValue;
						recordItem.Fields("pmkt").Text = recordData.Mkt;
						Color fontColor = Color.Yellow;
						decimal num = 0m;
						decimal num2 = 0m;
						if (recordData.Index > 0m && recordData.IndexPrior > 0m)
						{
							num = recordData.Index - recordData.IndexPrior;
							num2 = Math.Round(num / recordData.IndexPrior * 100m, 4);
							fontColor = Utilities.ComparePriceColor(num, 0m);
						}
						recordItem.Fields("chg").Text = num;
						recordItem.Fields("pchg").Text = num2;
						recordItem.Fields("symbol").FontColor = fontColor;
						recordItem.Fields("prior").FontColor = Color.Yellow;
						recordItem.Fields("volume").FontColor = fontColor;
						recordItem.Fields("value").FontColor = fontColor;
						recordItem.Fields("last").FontColor = fontColor;
						recordItem.Fields("chg").FontColor = fontColor;
						recordItem.Fields("pchg").FontColor = fontColor;
						recordItem.Fields("pmkt").FontColor = fontColor;
						recordItem.Changed = true;
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateToGrid", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ReloadData()
		{
			if (!this.bgwReloadData.IsBusy)
			{
				this.bgwReloadData.RunWorkerAsync(this.IndustryNumber);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateToControl(DataSet ds)
		{
			try
			{
				string empty = string.Empty;
				if (this.selectionMenu == 0)
				{
					this.intzaSector.BeginUpdate();
					if (ds.Tables["sector_stat_info"].Rows.Count > 0)
					{
						this.intzaSector.Rows = ds.Tables["sector_stat_info"].Rows.Count;
					}
					for (int i = 0; i < ds.Tables["sector_stat_info"].Rows.Count; i++)
					{
						DataRow dataRow = ds.Tables["sector_stat_info"].Rows[i];
						frmMarketInfo.RecordData recordData = default(frmMarketInfo.RecordData);
						recordData.Number = Convert.ToInt32(dataRow["sector_number"]);
						recordData.Symbol = dataRow["sector_symbol"].ToString().Trim();
						recordData.IndexPrior = Convert.ToDecimal(dataRow["index_prior"]);
						recordData.Index = Convert.ToDecimal(dataRow["index_value"]);
						recordData.AccVolume = Convert.ToInt64(dataRow["accvolume"]);
						recordData.AccValue = Convert.ToDecimal(dataRow["accvalue"]);
						recordData.Mkt = Convert.ToDecimal(dataRow["mkt"]);
						this.UpdateToGrid(i + 1, recordData);
					}
					this.intzaSector.Sort(this.intzaSector.SortColumnName, this.sortType);
					this.intzaSector.IsAutoRepaint = true;
					this.intzaSector.Invalidate();
				}
				else
				{
					this.intzaSector.BeginUpdate();
					if (ds.Tables["industry_stat"].Rows.Count > 0)
					{
						this.intzaSector.Rows = ds.Tables["industry_stat"].Rows.Count;
					}
					for (int i = 0; i < ds.Tables["industry_stat"].Rows.Count; i++)
					{
						DataRow dataRow2 = ds.Tables["industry_stat"].Rows[i];
						frmMarketInfo.RecordData recordData = default(frmMarketInfo.RecordData);
						recordData.Number = Convert.ToInt32(dataRow2["industry_number"]);
						recordData.Symbol = dataRow2["industry_symbol"].ToString();
						recordData.Index = Convert.ToDecimal(dataRow2["index_value"]);
						recordData.IndexPrior = Convert.ToDecimal(dataRow2["index_prior"]);
						recordData.AccVolume = Convert.ToInt64(dataRow2["accvolume"]);
						recordData.AccValue = Convert.ToDecimal(dataRow2["accvalue"]);
						recordData.Mkt = Convert.ToDecimal(dataRow2["mkt"]);
						this.UpdateToGrid(i + 1, recordData);
					}
					this.intzaSector.Sort(this.intzaSector.SortColumnName, this.sortType);
					this.intzaSector.IsAutoRepaint = true;
					this.intzaSector.Invalidate();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateToControl", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SortColumn(string sortName, SortType sortTypeColumns)
		{
			this.intzaSector.Sort(sortName, sortTypeColumns);
			this.intzaSector.Redraw();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetSort(SortType newSortType)
		{
			try
			{
				this.sortType = newSortType;
				this.tsbtnSortAsc.ForeColor = Color.Gray;
				this.tsbtnSortDesc.ForeColor = Color.Gray;
				if (newSortType == SortType.Asc)
				{
					this.tsbtnSortAsc.ForeColor = Color.Orange;
				}
				else
				{
					this.tsbtnSortDesc.ForeColor = Color.Orange;
				}
				this.SortColumn(this.intzaSector.SortColumnName, this.sortType);
			}
			catch (Exception ex)
			{
				this.ShowError("SetSort", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetTextSortColumn(string sortName)
		{
			this.tsSortBySector.ForeColor = Color.Gray;
			this.tsSortByVolume.ForeColor = Color.Gray;
			this.tsSortByValues.ForeColor = Color.Gray;
			if (sortName != null)
			{
				if (sortName == "symbol")
				{
					this.tsSortBySector.ForeColor = Color.Orange;
					goto IL_D0;
				}
				if (sortName == "volume")
				{
					this.tsSortByVolume.ForeColor = Color.Orange;
					goto IL_D0;
				}
				if (sortName == "value")
				{
					this.tsSortByValues.ForeColor = Color.Orange;
					goto IL_D0;
				}
			}
			this.tsSortBySector.ForeColor = Color.Orange;
			IL_D0:
			this.SortColumn(sortName, this.sortType);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsSortBySector_Click(object sender, EventArgs e)
		{
			this.SetTextSortColumn("symbol");
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsSortByIndustry_Click(object sender, EventArgs e)
		{
			this.SetTextSortColumn("industry");
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsSortByVolume_Click(object sender, EventArgs e)
		{
			this.SetTextSortColumn("volume");
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsSortByValues_Click(object sender, EventArgs e)
		{
			this.SetTextSortColumn("value");
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmSectorInformation_IDOCustomSizeChanged()
		{
			if (!this.isLoading)
			{
				this.SetResize(this.IsHeightChanged | this.IsWidthChanged);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetResize(bool isChanged)
		{
			try
			{
				if (isChanged)
				{
					this.intzaSET.SetBounds(0, 0, (int)((double)base.Width * 0.5), this.intzaSET.GetHeightByRows(12));
					this.toolStrip1.SetBounds(this.intzaSET.Left + this.intzaSET.Width + 1, 0, base.Width - (this.intzaSET.Left + this.intzaSET.Width + 1), this.toolStrip1.Height);
					this.intzaMarketInfo.SetBounds(this.intzaSET.Left + this.intzaSET.Width + 1, this.toolStrip1.Top + this.toolStrip1.Height, base.Width - (this.intzaSET.Left + this.intzaSET.Width + 1), this.intzaSET.Height - (this.toolStrip1.Top + this.toolStrip1.Height));
					this.pictureBox1.Bounds = this.intzaMarketInfo.Bounds;
					this.panelSector.SetBounds(0, this.intzaSET.Top + this.intzaSET.Height + 1, base.Width, base.Height - (this.intzaSET.Top + this.intzaSET.Height + 1));
					if (this.pictureBox1.Visible)
					{
						this.ReloadChart();
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SetResize", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetPage(int mode)
		{
			try
			{
				if (mode == 0)
				{
					this.tsbtnSector.ForeColor = Color.Orange;
					this.tsbtnIndustry.ForeColor = Color.Gray;
					this.selectionMenu = 0;
					this.Text = "Sector Statistic";
				}
				else
				{
					this.tsbtnSector.ForeColor = Color.Gray;
					this.tsbtnIndustry.ForeColor = Color.Orange;
					this.selectionMenu = 1;
					this.Text = "Industry Statistic";
				}
				this.ReloadData();
			}
			catch (Exception ex)
			{
				this.ShowError("SetPage", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwMarketInfoLoadData_DoWork(object sender, DoWorkEventArgs e)
		{
			if (this.FormState != ClientBaseForm.ClientBaseFormState.Closing)
			{
				try
				{
					if (this.dsMarketInfo == null)
					{
						this.dsMarketInfo = new DataSet();
					}
					else
					{
						this.dsMarketInfo.Clear();
					}
					string text = ApplicationInfo.WebService.MarketIndicator();
					if (!string.IsNullOrEmpty(text))
					{
						MyDataHelper.StringToDataSet(text, this.dsMarketInfo);
					}
				}
				catch (Exception ex)
				{
					this.ShowError("MarketInfoRequestWebData", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwMarketInfoLoadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (this.FormState != ClientBaseForm.ClientBaseFormState.Closing)
			{
				this.MarketInfoUpdateToControl();
				this.dsMarketInfo.Clear();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void MarketInfoReloadData()
		{
			if (!this.bgwMarketInfoLoadData.IsBusy)
			{
				this.bgwMarketInfoLoadData.RunWorkerAsync();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void MarketInfoUpdateToControl()
		{
			if (this.intzaMarketInfo.InvokeRequired)
			{
				this.intzaMarketInfo.Invoke(new MethodInvoker(this.MarketInfoUpdateToControl));
			}
			else
			{
				try
				{
					this.intzaMarketInfo.BeginUpdate();
					if (this.dsMarketInfo.Tables.Count > 0)
					{
						if (this.dsMarketInfo.Tables.Contains("market_info") && this.dsMarketInfo.Tables["market_info"].Rows.Count > 0)
						{
							DataRow dataRow = this.dsMarketInfo.Tables["market_info"].Rows[0];
							long mainVolume;
							long.TryParse(dataRow["main_accvolume"].ToString(), out mainVolume);
							long biglotVolume;
							long.TryParse(dataRow["biglot_accvolume"].ToString(), out biglotVolume);
							long oddlotVolume;
							long.TryParse(dataRow["oddlot_accvolume"].ToString(), out oddlotVolume);
							long foreignVolume;
							long.TryParse(dataRow["foreign_accvolume"].ToString(), out foreignVolume);
							decimal mainValue;
							decimal.TryParse(dataRow["main_accvalue"].ToString(), out mainValue);
							decimal biglotValue;
							decimal.TryParse(dataRow["biglot_accvalue"].ToString(), out biglotValue);
							decimal oddlogValue;
							decimal.TryParse(dataRow["oddlot_accvalue"].ToString(), out oddlogValue);
							decimal foreignValue;
							decimal.TryParse(dataRow["foreign_accvalue"].ToString(), out foreignValue);
							this.MarketInfoUpdateByMT(mainVolume, mainValue, oddlotVolume, oddlogValue, biglotVolume, biglotValue, foreignVolume, foreignValue);
							if (this.dsMarketInfo.Tables.Contains("set_index") && this.dsMarketInfo.Tables["set_index"].Rows.Count > 0)
							{
								decimal trin = 0m;
								DataRow dataRow2 = this.dsMarketInfo.Tables["set_index"].Rows[0];
								int num;
								int.TryParse(dataRow2["advances"].ToString(), out num);
								int num2;
								int.TryParse(dataRow2["declines"].ToString(), out num2);
								int securityNoChg;
								int.TryParse(dataRow2["nochg"].ToString(), out securityNoChg);
								long num3;
								long.TryParse(dataRow2["up_volume"].ToString(), out num3);
								long num4;
								long.TryParse(dataRow2["down_volume"].ToString(), out num4);
								long num5;
								long.TryParse(dataRow2["unchg_volume"].ToString(), out num5);
								int num6;
								int.TryParse(dataRow["compare_price_up"].ToString(), out num6);
								int num7;
								int.TryParse(dataRow["compare_price_down"].ToString(), out num7);
								if (num2 > 0 && num > 0)
								{
                                    trin = Math.Round((decimal)(num4 / (long)num2 / (num3 / (long)num)), 2);
								}
								int tick = num6 - num7;
								this.MarketInfoUpdateByIS(tick, trin, num, num2, securityNoChg, num3 * 1000L, num4 * 1000L, num5 * 1000L);
							}
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("RequestWebData", ex);
				}
				finally
				{
					this.intzaMarketInfo.Redraw();
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void MarketInfoUpdateByMT(long mainVolume, decimal mainValue, long oddlotVolume, decimal oddlogValue, long biglotVolume, decimal biglotValue, long foreignVolume, decimal foreignValue)
		{
			long num = mainVolume + oddlotVolume + biglotVolume + foreignVolume;
			decimal num2 = mainValue + oddlogValue + biglotValue + foreignValue;
			decimal d = 0m;
			decimal num3 = 0m;
			decimal d2 = 0m;
			decimal d3 = 0m;
			if (num2 > 0m)
			{
				d = mainValue / num2 * 100m;
				d3 = foreignValue / num2 * 100m;
				d2 = biglotValue / num2 * 100m;
				num3 = 100m - (d + d3 + d2);
			}
			this.intzaMarketInfo.Item("main_volume_text").Text = mainVolume.ToString();
			this.intzaMarketInfo.Item("main_value_text").Text = mainValue.ToString();
			this.intzaMarketInfo.Item("main_pvalue_text").Text = d.ToString();
			this.intzaMarketInfo.Item("odd_volume_text").Text = oddlotVolume.ToString();
			this.intzaMarketInfo.Item("odd_value_text").Text = oddlogValue.ToString();
			this.intzaMarketInfo.Item("odd_pvalue_text").Text = num3.ToString();
			this.intzaMarketInfo.Item("big_volume_text").Text = biglotVolume.ToString();
			this.intzaMarketInfo.Item("big_value_text").Text = biglotValue.ToString();
			this.intzaMarketInfo.Item("big_pvalue_text").Text = d2.ToString();
			this.intzaMarketInfo.Item("foreign_volume_text").Text = foreignVolume.ToString();
			this.intzaMarketInfo.Item("foreign_value_text").Text = foreignValue.ToString();
			this.intzaMarketInfo.Item("foreign_pvalue_text").Text = d3.ToString();
			this.intzaMarketInfo.Item("sum_volume").Text = num.ToString();
			this.intzaMarketInfo.Item("sum_value").Text = num2.ToString();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void MarketInfoUpdateByIS(int tick, decimal trin, int securityUp, int securityDown, int securityNoChg, long upVolume, long downVolume, long nochangeVolume)
		{
			this.intzaMarketInfo.Item("tick_text").Text = tick.ToString();
			this.intzaMarketInfo.Item("tick_text").FontColor = Utilities.ComparePriceColor(tick, 0m);
			this.intzaMarketInfo.Item("trin_text").Text = trin.ToString();
			this.intzaMarketInfo.Item("trin_text").FontColor = Utilities.ComparePriceColor(trin, 0m);
			this.intzaMarketInfo.Item("up_text").Text = securityUp.ToString();
			this.intzaMarketInfo.Item("down_text").Text = securityDown.ToString();
			this.intzaMarketInfo.Item("nochange_text").Text = securityNoChg.ToString();
			this.intzaMarketInfo.Item("upvolume_text").Text = upVolume.ToString();
			this.intzaMarketInfo.Item("downvolume_text").Text = downVolume.ToString();
			this.intzaMarketInfo.Item("nochg_volume_text").Text = nochangeVolume.ToString();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void MarketInfoReloadDataSETindex()
		{
			try
			{
				this.intzaSET.BeginUpdate();
				this.ShowSetIndex(".SET");
				this.ShowSetIndex(".SET50");
				this.ShowSetIndex(".SET100");
				this.ShowSetIndex(".SETHD");
				this.ShowSetIndex(".MAI");
				this.intzaSET.Redraw();
			}
			catch (Exception ex)
			{
				this.ShowError("MarketInfoReloadDataSETindex", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowSetIndex(string symbol)
		{
			try
			{
				IndexStat.IndexItem indexItem = ApplicationInfo.IndexStatInfo[symbol];
				if (indexItem != null)
				{
					Color fontColor = Color.Yellow;
					decimal d = 0m;
					decimal num = 0m;
					fontColor = Utilities.ComparePriceColor(indexItem.LastIndex, indexItem.Prior);
					if (symbol == ".SET")
					{
						this.intzaSET.Records(0).Fields("prior").Text = FormatUtil.PriceFormat(indexItem.Prior);
						this.intzaSET.Records(0).Fields("index").Text = indexItem.LastIndex.ToString();
						this.intzaSET.Records(0).Fields("chg").Text = indexItem.IndexChg.ToString();
						this.intzaSET.Records(0).Fields("pchg").Text = indexItem.IndexChgPct.ToString();
						this.intzaSET.Records(0).Fields("prior").FontColor = Color.Yellow;
						this.intzaSET.Records(0).Fields("index").FontColor = fontColor;
						this.intzaSET.Records(0).Fields("chg").FontColor = fontColor;
						this.intzaSET.Records(0).Fields("pchg").FontColor = fontColor;
						fontColor = Utilities.ComparePriceColor(indexItem.IndexHigh, indexItem.Prior);
						if (indexItem.IndexHigh > 0m && indexItem.Prior > 0m)
						{
							if (indexItem.Prior > 0m && indexItem.IndexHigh > 0m)
							{
								d = indexItem.IndexHigh - indexItem.Prior;
								num = Math.Round(d / indexItem.Prior * 100m, 4);
							}
							this.intzaSET.Records(1).Fields("index").Text = indexItem.IndexHigh.ToString();
							this.intzaSET.Records(1).Fields("chg").Text = d.ToString();
							this.intzaSET.Records(1).Fields("pchg").Text = num.ToString();
							this.intzaSET.Records(1).Fields("index").FontColor = fontColor;
							this.intzaSET.Records(1).Fields("chg").FontColor = fontColor;
							this.intzaSET.Records(1).Fields("pchg").FontColor = fontColor;
						}
						fontColor = Utilities.ComparePriceColor(indexItem.IndexLow, indexItem.Prior);
						if (indexItem.IndexLow > 0m && indexItem.Prior > 0m)
						{
							if (indexItem.Prior > 0m && indexItem.IndexLow > 0m)
							{
								d = indexItem.IndexLow - indexItem.Prior;
								num = Math.Round(d / indexItem.Prior * 100m, 4);
							}
							this.intzaSET.Records(2).Fields("index").Text = indexItem.IndexLow.ToString();
							this.intzaSET.Records(2).Fields("chg").Text = d.ToString();
							this.intzaSET.Records(2).Fields("pchg").Text = num.ToString();
							this.intzaSET.Records(2).Fields("index").FontColor = fontColor;
							this.intzaSET.Records(2).Fields("chg").FontColor = fontColor;
							this.intzaSET.Records(2).Fields("pchg").FontColor = fontColor;
						}
					}
					else
					{
						if (symbol == ".SET50")
						{
							this.intzaSET.Records(3).Fields("prior").Text = FormatUtil.PriceFormat(indexItem.Prior);
							this.intzaSET.Records(3).Fields("index").Text = indexItem.LastIndex.ToString();
							this.intzaSET.Records(3).Fields("chg").Text = indexItem.IndexChg.ToString();
							this.intzaSET.Records(3).Fields("pchg").Text = indexItem.IndexChgPct.ToString();
							this.intzaSET.Records(3).Fields("prior").FontColor = Color.Yellow;
							this.intzaSET.Records(3).Fields("index").FontColor = fontColor;
							this.intzaSET.Records(3).Fields("chg").FontColor = fontColor;
							this.intzaSET.Records(3).Fields("pchg").FontColor = fontColor;
							fontColor = Utilities.ComparePriceColor(indexItem.IndexHigh, indexItem.Prior);
							if (indexItem.IndexHigh > 0m && indexItem.Prior > 0m)
							{
								if (indexItem.Prior > 0m && indexItem.IndexHigh > 0m)
								{
									d = indexItem.IndexHigh - indexItem.Prior;
									num = Math.Round(d / indexItem.Prior * 100m, 4);
								}
								this.intzaSET.Records(4).Fields("index").Text = indexItem.IndexHigh.ToString();
								this.intzaSET.Records(4).Fields("chg").Text = d.ToString();
								this.intzaSET.Records(4).Fields("pchg").Text = num.ToString();
								this.intzaSET.Records(4).Fields("index").FontColor = fontColor;
								this.intzaSET.Records(4).Fields("chg").FontColor = fontColor;
								this.intzaSET.Records(4).Fields("pchg").FontColor = fontColor;
							}
							fontColor = Utilities.ComparePriceColor(indexItem.IndexLow, indexItem.Prior);
							if (indexItem.IndexLow > 0m && indexItem.Prior > 0m)
							{
								if (indexItem.Prior > 0m && indexItem.IndexLow > 0m)
								{
									d = indexItem.IndexLow - indexItem.Prior;
									num = Math.Round(d / indexItem.Prior * 100m, 4);
								}
								this.intzaSET.Records(5).Fields("index").Text = indexItem.IndexLow.ToString();
								this.intzaSET.Records(5).Fields("chg").Text = d.ToString();
								this.intzaSET.Records(5).Fields("pchg").Text = num.ToString();
								this.intzaSET.Records(5).Fields("index").FontColor = fontColor;
								this.intzaSET.Records(5).Fields("chg").FontColor = fontColor;
								this.intzaSET.Records(5).Fields("pchg").FontColor = fontColor;
							}
						}
						else
						{
							if (symbol == ".SET100")
							{
								this.intzaSET.Records(6).Fields("prior").Text = FormatUtil.PriceFormat(indexItem.Prior);
								this.intzaSET.Records(6).Fields("index").Text = indexItem.LastIndex.ToString();
								this.intzaSET.Records(6).Fields("chg").Text = indexItem.IndexChg.ToString();
								this.intzaSET.Records(6).Fields("pchg").Text = indexItem.IndexChgPct.ToString();
								this.intzaSET.Records(6).Fields("prior").FontColor = Color.Yellow;
								this.intzaSET.Records(6).Fields("index").FontColor = fontColor;
								this.intzaSET.Records(6).Fields("chg").FontColor = fontColor;
								this.intzaSET.Records(6).Fields("pchg").FontColor = fontColor;
								fontColor = Utilities.ComparePriceColor(indexItem.IndexHigh, indexItem.Prior);
								if (indexItem.IndexHigh > 0m && indexItem.Prior > 0m)
								{
									if (indexItem.Prior > 0m && indexItem.IndexHigh > 0m)
									{
										d = indexItem.IndexHigh - indexItem.Prior;
										num = Math.Round(d / indexItem.Prior * 100m, 4);
									}
									this.intzaSET.Records(7).Fields("index").Text = indexItem.IndexHigh.ToString();
									this.intzaSET.Records(7).Fields("chg").Text = d.ToString();
									this.intzaSET.Records(7).Fields("pchg").Text = num.ToString();
									this.intzaSET.Records(7).Fields("index").FontColor = fontColor;
									this.intzaSET.Records(7).Fields("chg").FontColor = fontColor;
									this.intzaSET.Records(7).Fields("pchg").FontColor = fontColor;
								}
								fontColor = Utilities.ComparePriceColor(indexItem.IndexLow, indexItem.Prior);
								if (indexItem.IndexLow > 0m && indexItem.Prior > 0m)
								{
									if (indexItem.Prior > 0m && indexItem.IndexLow > 0m)
									{
										d = indexItem.IndexLow - indexItem.Prior;
										num = Math.Round(d / indexItem.Prior * 100m, 4);
									}
									this.intzaSET.Records(8).Fields("index").Text = indexItem.IndexLow.ToString();
									this.intzaSET.Records(8).Fields("chg").Text = d.ToString();
									this.intzaSET.Records(8).Fields("pchg").Text = num.ToString();
									this.intzaSET.Records(8).Fields("index").FontColor = fontColor;
									this.intzaSET.Records(8).Fields("chg").FontColor = fontColor;
									this.intzaSET.Records(8).Fields("pchg").FontColor = fontColor;
								}
							}
							else
							{
								if (symbol == ".MAI")
								{
									this.intzaMarketInfo.Item("maival_text").Text = FormatUtil.VolumeFormat(indexItem.AccValue.ToString(), true);
									this.intzaSET.Records(9).Fields("prior").Text = FormatUtil.PriceFormat(indexItem.Prior);
									this.intzaSET.Records(9).Fields("index").Text = indexItem.LastIndex.ToString();
									this.intzaSET.Records(9).Fields("chg").Text = indexItem.IndexChg.ToString();
									this.intzaSET.Records(9).Fields("pchg").Text = indexItem.IndexChgPct.ToString();
									this.intzaSET.Records(9).Fields("prior").FontColor = Color.Yellow;
									this.intzaSET.Records(9).Fields("index").FontColor = fontColor;
									this.intzaSET.Records(9).Fields("chg").FontColor = fontColor;
									this.intzaSET.Records(9).Fields("pchg").FontColor = fontColor;
									fontColor = Utilities.ComparePriceColor(indexItem.IndexHigh, indexItem.Prior);
									if (indexItem.IndexHigh > 0m && indexItem.Prior > 0m)
									{
										if (indexItem.Prior > 0m && indexItem.IndexHigh > 0m)
										{
											d = indexItem.IndexHigh - indexItem.Prior;
											num = Math.Round(d / indexItem.Prior * 100m, 4);
										}
										this.intzaSET.Records(10).Fields("index").Text = indexItem.IndexHigh.ToString();
										this.intzaSET.Records(10).Fields("chg").Text = d.ToString();
										this.intzaSET.Records(10).Fields("pchg").Text = num.ToString();
										this.intzaSET.Records(10).Fields("index").FontColor = fontColor;
										this.intzaSET.Records(10).Fields("chg").FontColor = fontColor;
										this.intzaSET.Records(10).Fields("pchg").FontColor = fontColor;
									}
									fontColor = Utilities.ComparePriceColor(indexItem.IndexLow, indexItem.Prior);
									if (indexItem.IndexLow > 0m && indexItem.Prior > 0m)
									{
										if (indexItem.Prior > 0m && indexItem.IndexLow > 0m)
										{
											d = indexItem.IndexLow - indexItem.Prior;
											num = Math.Round(d / indexItem.Prior * 100m, 4);
										}
										this.intzaSET.Records(11).Fields("index").Text = indexItem.IndexLow.ToString();
										this.intzaSET.Records(11).Fields("chg").Text = d.ToString();
										this.intzaSET.Records(11).Fields("pchg").Text = num.ToString();
										this.intzaSET.Records(11).Fields("index").FontColor = fontColor;
										this.intzaSET.Records(11).Fields("chg").FontColor = fontColor;
										this.intzaSET.Records(11).Fields("pchg").FontColor = fontColor;
									}
								}
								else
								{
									if (symbol == ".SETHD")
									{
										this.intzaSET.Records(12).Fields("prior").Text = FormatUtil.PriceFormat(indexItem.Prior);
										this.intzaSET.Records(12).Fields("index").Text = indexItem.LastIndex.ToString();
										this.intzaSET.Records(12).Fields("chg").Text = indexItem.IndexChg.ToString();
										this.intzaSET.Records(12).Fields("pchg").Text = indexItem.IndexChgPct.ToString();
										this.intzaSET.Records(12).Fields("prior").FontColor = Color.Yellow;
										this.intzaSET.Records(12).Fields("index").FontColor = fontColor;
										this.intzaSET.Records(12).Fields("chg").FontColor = fontColor;
										this.intzaSET.Records(12).Fields("pchg").FontColor = fontColor;
										fontColor = Utilities.ComparePriceColor(indexItem.IndexHigh, indexItem.Prior);
										if (indexItem.IndexHigh > 0m && indexItem.Prior > 0m)
										{
											if (indexItem.Prior > 0m && indexItem.IndexHigh > 0m)
											{
												d = indexItem.IndexHigh - indexItem.Prior;
												num = Math.Round(d / indexItem.Prior * 100m, 4);
											}
											this.intzaSET.Records(13).Fields("index").Text = indexItem.IndexHigh.ToString();
											this.intzaSET.Records(13).Fields("chg").Text = d.ToString();
											this.intzaSET.Records(13).Fields("pchg").Text = num.ToString();
											this.intzaSET.Records(13).Fields("index").FontColor = fontColor;
											this.intzaSET.Records(13).Fields("chg").FontColor = fontColor;
											this.intzaSET.Records(13).Fields("pchg").FontColor = fontColor;
										}
										fontColor = Utilities.ComparePriceColor(indexItem.IndexLow, indexItem.Prior);
										if (indexItem.IndexLow > 0m && indexItem.Prior > 0m)
										{
											if (indexItem.Prior > 0m && indexItem.IndexLow > 0m)
											{
												d = indexItem.IndexLow - indexItem.Prior;
												num = Math.Round(d / indexItem.Prior * 100m, 4);
											}
											this.intzaSET.Records(14).Fields("index").Text = indexItem.IndexLow.ToString();
											this.intzaSET.Records(14).Fields("chg").Text = d.ToString();
											this.intzaSET.Records(14).Fields("pchg").Text = num.ToString();
											this.intzaSET.Records(14).Fields("index").FontColor = fontColor;
											this.intzaSET.Records(14).Fields("chg").FontColor = fontColor;
											this.intzaSET.Records(14).Fields("pchg").FontColor = fontColor;
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
				this.ShowError("MarketInfoShowSetIndex", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ReloadChart()
		{
			if (!this.bgwReloadChart.IsBusy)
			{
				this.bgwReloadChart.RunWorkerAsync();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwReloadChart_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				this.ShowSplashChart(true);
				string text = string.Empty;
				IndexStat.IndexItem indexItem = ApplicationInfo.IndexStatInfo["." + this._currentChartName];
				if (indexItem != null)
				{
					decimal prior = indexItem.Prior;
					text = ApplicationInfo.WebService.GetSetIndexChartImage("." + this._currentChartName, (double)prior, this.pictureBox1.Width, this.pictureBox1.Height);
					if (text != string.Empty)
					{
						byte[] buffer = Convert.FromBase64String(text);
						using (MemoryStream memoryStream = new MemoryStream(buffer))
						{
							this.pictureBox1.Image = Image.FromStream(memoryStream);
						}
						text = string.Empty;
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
		private void ShowSplashChart(bool visible)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new frmMarketInfo.ShowSplashChartCallBack(this.ShowSplashChart), new object[]
				{
					visible
				});
			}
			else
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
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnSETChart_Click(object sender, EventArgs e)
		{
			this.SetChart(((ToolStripButton)sender).Text, sender);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnInfo_Click(object sender, EventArgs e)
		{
			this.SetChart("INFO", sender);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetChart(string symbol, object sender)
		{
			try
			{
				this.tsbtnSETChart.ForeColor = Color.Gray;
				this.tsbtnSET50Chart.ForeColor = Color.Gray;
				this.tsbtnSET100Chart.ForeColor = Color.Gray;
				this.tsbtnMaiChart.ForeColor = Color.Gray;
				this.tsbtnInfo.ForeColor = Color.Gray;
				this.tsbtnSETHdChart.ForeColor = Color.Gray;
				if (symbol != null)
				{
					if (symbol == "SET" || symbol == "SET50" || symbol == "SET100" || symbol == "MAI" || symbol == "SETHD")
					{
						this._currentChartName = symbol;
						this.pictureBox1.Show();
						this.intzaMarketInfo.Hide();
						((ToolStripButton)sender).ForeColor = Color.Orange;
						this.ReloadChart();
						goto IL_136;
					}
				}
				this.intzaMarketInfo.Show();
				this.pictureBox1.Hide();
				this.tsbtnInfo.ForeColor = Color.Orange;
				IL_136:;
			}
			catch (Exception ex)
			{
				this.ShowError("SetChart", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowSplash2(bool visible)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new frmMarketInfo.ShowSplashCallBack(this.ShowSplash2), new object[]
				{
					visible
				});
			}
			else
			{
				this.isLoading = visible;
				if (ApplicationInfo.SuuportSplash == "Y")
				{
					if (visible)
					{
						this.lbLoading2.Left = (this.intzaSector.Width - this.lbLoading2.Width) / 2;
						this.lbLoading2.Top = (this.intzaSector.Height - this.lbLoading2.Height) / 2;
						this.lbLoading2.Visible = true;
						this.lbLoading2.BringToFront();
					}
					else
					{
						this.lbLoading2.Visible = false;
					}
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnSector_Click(object sender, EventArgs e)
		{
			if (!this.isLoading)
			{
				this.SetPage(0);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnIndustry_Click(object sender, EventArgs e)
		{
			if (!this.isLoading)
			{
				this.SetPage(1);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnSortAsc_Click(object sender, EventArgs e)
		{
			this.SetSort(SortType.Asc);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnSortDesc_Click(object sender, EventArgs e)
		{
			this.SetSort(SortType.Desc);
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
			ColumnItem columnItem = new ColumnItem();
			ColumnItem columnItem2 = new ColumnItem();
			ColumnItem columnItem3 = new ColumnItem();
			ColumnItem columnItem4 = new ColumnItem();
			ColumnItem columnItem5 = new ColumnItem();
			ColumnItem columnItem6 = new ColumnItem();
			ColumnItem columnItem7 = new ColumnItem();
			ColumnItem columnItem8 = new ColumnItem();
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmMarketInfo));
			ColumnItem columnItem9 = new ColumnItem();
			ColumnItem columnItem10 = new ColumnItem();
			ColumnItem columnItem11 = new ColumnItem();
			ColumnItem columnItem12 = new ColumnItem();
			ColumnItem columnItem13 = new ColumnItem();
			this.tStripMenu = new ToolStrip();
			this.toolStripLabel1 = new ToolStripLabel();
			this.tsbtnSector = new ToolStripButton();
			this.tsbtnIndustry = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.tsSortBy = new ToolStripLabel();
			this.tsbtnSortAsc = new ToolStripButton();
			this.tsbtnSortDesc = new ToolStripButton();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.toolStripLabel2 = new ToolStripLabel();
			this.tsSortBySector = new ToolStripButton();
			this.tsSortByVolume = new ToolStripButton();
			this.tsSortByValues = new ToolStripButton();
			this.panelSector = new Panel();
			this.intzaSector = new SortGrid();
			this.lbLoading2 = new Label();
			this.intzaMarketInfo = new IntzaCustomGrid();
			this.toolStrip1 = new ToolStrip();
			this.tsbtnSETChart = new ToolStripButton();
			this.tsbtnSET50Chart = new ToolStripButton();
			this.tsbtnSET100Chart = new ToolStripButton();
			this.tsbtnSETHdChart = new ToolStripButton();
			this.tsbtnMaiChart = new ToolStripButton();
			this.toolStripSeparator3 = new ToolStripSeparator();
			this.tsbtnInfo = new ToolStripButton();
			this.pictureBox1 = new PictureBox();
			this.lbChartLoading = new Label();
			this.intzaSET = new SortGrid();
			this.tStripMenu.SuspendLayout();
			this.panelSector.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			base.SuspendLayout();
			this.tStripMenu.AllowMerge = false;
			this.tStripMenu.BackColor = Color.FromArgb(20, 20, 20);
			this.tStripMenu.GripMargin = new Padding(0);
			this.tStripMenu.GripStyle = ToolStripGripStyle.Hidden;
			this.tStripMenu.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripLabel1,
				this.tsbtnSector,
				this.tsbtnIndustry,
				this.toolStripSeparator1,
				this.tsSortBy,
				this.tsbtnSortAsc,
				this.tsbtnSortDesc,
				this.toolStripSeparator2,
				this.toolStripLabel2,
				this.tsSortBySector,
				this.tsSortByVolume,
				this.tsSortByValues
			});
			this.tStripMenu.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.tStripMenu.Location = new Point(0, 0);
			this.tStripMenu.Name = "tStripMenu";
			this.tStripMenu.Padding = new Padding(1, 1, 1, 2);
			this.tStripMenu.RenderMode = ToolStripRenderMode.Professional;
			this.tStripMenu.Size = new Size(675, 26);
			this.tStripMenu.TabIndex = 10;
			this.tStripMenu.Tag = "-1";
			this.tStripMenu.Text = "ToolStrip1";
			this.toolStripLabel1.ForeColor = Color.LightGray;
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new Size(64, 20);
			this.toolStripLabel1.Text = "Selection : ";
			this.tsbtnSector.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnSector.ForeColor = Color.LightGray;
			this.tsbtnSector.ImageTransparentColor = Color.Magenta;
			this.tsbtnSector.Name = "tsbtnSector";
			this.tsbtnSector.Size = new Size(44, 20);
			this.tsbtnSector.Text = "Sector";
			this.tsbtnSector.Click += new EventHandler(this.tsbtnSector_Click);
			this.tsbtnIndustry.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnIndustry.ForeColor = Color.LightGray;
			this.tsbtnIndustry.ImageTransparentColor = Color.Magenta;
			this.tsbtnIndustry.Name = "tsbtnIndustry";
			this.tsbtnIndustry.Size = new Size(54, 20);
			this.tsbtnIndustry.Text = "Industry";
			this.tsbtnIndustry.Click += new EventHandler(this.tsbtnIndustry_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 23);
			this.tsSortBy.BackColor = Color.Transparent;
			this.tsSortBy.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsSortBy.Font = new Font("Microsoft Sans Serif", 9f);
			this.tsSortBy.ForeColor = Color.LightGray;
			this.tsSortBy.ImageTransparentColor = Color.Magenta;
			this.tsSortBy.Name = "tsSortBy";
			this.tsSortBy.Size = new Size(35, 20);
			this.tsSortBy.Text = "Sort :";
			this.tsbtnSortAsc.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnSortAsc.ForeColor = Color.LightGray;
			this.tsbtnSortAsc.ImageTransparentColor = Color.Magenta;
			this.tsbtnSortAsc.Name = "tsbtnSortAsc";
			this.tsbtnSortAsc.Size = new Size(67, 20);
			this.tsbtnSortAsc.Text = "Ascending";
			this.tsbtnSortAsc.Click += new EventHandler(this.tsbtnSortAsc_Click);
			this.tsbtnSortDesc.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnSortDesc.ForeColor = Color.LightGray;
			this.tsbtnSortDesc.ImageTransparentColor = Color.Magenta;
			this.tsbtnSortDesc.Name = "tsbtnSortDesc";
			this.tsbtnSortDesc.Size = new Size(73, 20);
			this.tsbtnSortDesc.Text = "Descending";
			this.tsbtnSortDesc.Click += new EventHandler(this.tsbtnSortDesc_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new Size(6, 23);
			this.toolStripLabel2.ForeColor = Color.LightGray;
			this.toolStripLabel2.Name = "toolStripLabel2";
			this.toolStripLabel2.Size = new Size(50, 20);
			this.toolStripLabel2.Text = "Sort by :";
			this.tsSortBySector.BackColor = Color.Transparent;
			this.tsSortBySector.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsSortBySector.ForeColor = Color.LightGray;
			this.tsSortBySector.ImageTransparentColor = Color.Magenta;
			this.tsSortBySector.Name = "tsSortBySector";
			this.tsSortBySector.Size = new Size(51, 20);
			this.tsSortBySector.Text = "Symbol";
			this.tsSortBySector.Click += new EventHandler(this.tsSortBySector_Click);
			this.tsSortByVolume.BackColor = Color.Transparent;
			this.tsSortByVolume.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsSortByVolume.ForeColor = Color.LightGray;
			this.tsSortByVolume.ImageTransparentColor = Color.Magenta;
			this.tsSortByVolume.Name = "tsSortByVolume";
			this.tsSortByVolume.Size = new Size(55, 20);
			this.tsSortByVolume.Text = "Volume.";
			this.tsSortByVolume.Click += new EventHandler(this.tsSortByVolume_Click);
			this.tsSortByValues.BackColor = Color.Transparent;
			this.tsSortByValues.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsSortByValues.ForeColor = Color.LightGray;
			this.tsSortByValues.ImageTransparentColor = Color.Magenta;
			this.tsSortByValues.Name = "tsSortByValues";
			this.tsSortByValues.Size = new Size(43, 20);
			this.tsSortByValues.Text = "Value.";
			this.tsSortByValues.Click += new EventHandler(this.tsSortByValues_Click);
			this.panelSector.Controls.Add(this.intzaSector);
			this.panelSector.Controls.Add(this.lbLoading2);
			this.panelSector.Controls.Add(this.tStripMenu);
			this.panelSector.Location = new Point(0, 214);
			this.panelSector.Name = "panelSector";
			this.panelSector.Size = new Size(675, 188);
			this.panelSector.TabIndex = 19;
			this.intzaSector.AllowDrop = true;
			this.intzaSector.BackColor = Color.FromArgb(10, 10, 10);
			this.intzaSector.CanBlink = true;
			this.intzaSector.CanDrag = false;
			this.intzaSector.CanGetMouseMove = false;
			columnItem.Alignment = StringAlignment.Near;
			columnItem.BackColor = Color.FromArgb(64, 64, 64);
			columnItem.ColumnAlignment = StringAlignment.Center;
			columnItem.FontColor = Color.LightGray;
			columnItem.MyStyle = FontStyle.Regular;
			columnItem.Name = "symbol";
			columnItem.Text = "Symbol";
			columnItem.ValueFormat = FormatType.Text;
			columnItem.Visible = true;
			columnItem.Width = 14;
			columnItem2.Alignment = StringAlignment.Far;
			columnItem2.BackColor = Color.FromArgb(64, 64, 64);
			columnItem2.ColumnAlignment = StringAlignment.Center;
			columnItem2.FontColor = Color.LightGray;
			columnItem2.MyStyle = FontStyle.Regular;
			columnItem2.Name = "prior";
			columnItem2.Text = "Prior";
			columnItem2.ValueFormat = FormatType.Price;
			columnItem2.Visible = true;
			columnItem2.Width = 10;
			columnItem3.Alignment = StringAlignment.Far;
			columnItem3.BackColor = Color.FromArgb(64, 64, 64);
			columnItem3.ColumnAlignment = StringAlignment.Center;
			columnItem3.FontColor = Color.LightGray;
			columnItem3.MyStyle = FontStyle.Regular;
			columnItem3.Name = "last";
			columnItem3.Text = "Last";
			columnItem3.ValueFormat = FormatType.Price;
			columnItem3.Visible = true;
			columnItem3.Width = 10;
			columnItem4.Alignment = StringAlignment.Far;
			columnItem4.BackColor = Color.FromArgb(64, 64, 64);
			columnItem4.ColumnAlignment = StringAlignment.Center;
			columnItem4.FontColor = Color.LightGray;
			columnItem4.MyStyle = FontStyle.Regular;
			columnItem4.Name = "chg";
			columnItem4.Text = "Change";
			columnItem4.ValueFormat = FormatType.ChangePrice;
			columnItem4.Visible = true;
			columnItem4.Width = 10;
			columnItem5.Alignment = StringAlignment.Far;
			columnItem5.BackColor = Color.FromArgb(64, 64, 64);
			columnItem5.ColumnAlignment = StringAlignment.Center;
			columnItem5.FontColor = Color.LightGray;
			columnItem5.MyStyle = FontStyle.Regular;
			columnItem5.Name = "pchg";
			columnItem5.Text = "%Change";
			columnItem5.ValueFormat = FormatType.ChangePrice;
			columnItem5.Visible = true;
			columnItem5.Width = 10;
			columnItem6.Alignment = StringAlignment.Far;
			columnItem6.BackColor = Color.FromArgb(64, 64, 64);
			columnItem6.ColumnAlignment = StringAlignment.Center;
			columnItem6.FontColor = Color.LightGray;
			columnItem6.MyStyle = FontStyle.Regular;
			columnItem6.Name = "volume";
			columnItem6.Text = "Volume";
			columnItem6.ValueFormat = FormatType.Volume;
			columnItem6.Visible = true;
			columnItem6.Width = 17;
			columnItem7.Alignment = StringAlignment.Far;
			columnItem7.BackColor = Color.FromArgb(64, 64, 64);
			columnItem7.ColumnAlignment = StringAlignment.Center;
			columnItem7.FontColor = Color.LightGray;
			columnItem7.MyStyle = FontStyle.Regular;
			columnItem7.Name = "value";
			columnItem7.Text = "Value";
			columnItem7.ValueFormat = FormatType.Volume;
			columnItem7.Visible = true;
			columnItem7.Width = 19;
			columnItem8.Alignment = StringAlignment.Far;
			columnItem8.BackColor = Color.FromArgb(64, 64, 64);
			columnItem8.ColumnAlignment = StringAlignment.Center;
			columnItem8.FontColor = Color.LightGray;
			columnItem8.MyStyle = FontStyle.Regular;
			columnItem8.Name = "pmkt";
			columnItem8.Text = "%Mkt";
			columnItem8.ValueFormat = FormatType.Price;
			columnItem8.Visible = true;
			columnItem8.Width = 10;
			this.intzaSector.Columns.Add(columnItem);
			this.intzaSector.Columns.Add(columnItem2);
			this.intzaSector.Columns.Add(columnItem3);
			this.intzaSector.Columns.Add(columnItem4);
			this.intzaSector.Columns.Add(columnItem5);
			this.intzaSector.Columns.Add(columnItem6);
			this.intzaSector.Columns.Add(columnItem7);
			this.intzaSector.Columns.Add(columnItem8);
			this.intzaSector.CurrentScroll = 0;
			this.intzaSector.Dock = DockStyle.Fill;
			this.intzaSector.FocusItemIndex = -1;
			this.intzaSector.ForeColor = Color.Black;
			this.intzaSector.GridColor = Color.FromArgb(45, 45, 45);
			this.intzaSector.HeaderPctHeight = 80f;
			this.intzaSector.IsAutoRepaint = true;
			this.intzaSector.IsDrawFullRow = false;
			this.intzaSector.IsDrawGrid = true;
			this.intzaSector.IsDrawHeader = true;
			this.intzaSector.IsScrollable = true;
			this.intzaSector.Location = new Point(0, 26);
			this.intzaSector.MainColumn = "";
			this.intzaSector.Name = "intzaSector";
			this.intzaSector.Rows = 0;
			this.intzaSector.RowSelectColor = Color.FromArgb(95, 158, 160);
			this.intzaSector.RowSelectType = 0;
			this.intzaSector.RowsVisible = 0;
			this.intzaSector.ScrollChennelColor = Color.Gray;
			this.intzaSector.Size = new Size(675, 162);
			this.intzaSector.SortColumnName = "";
			this.intzaSector.SortType = SortType.Desc;
			this.intzaSector.TabIndex = 86;
			this.lbLoading2.AutoSize = true;
			this.lbLoading2.BorderStyle = BorderStyle.FixedSingle;
			this.lbLoading2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.lbLoading2.ForeColor = Color.Yellow;
			this.lbLoading2.Location = new Point(308, 99);
			this.lbLoading2.Name = "lbLoading2";
			this.lbLoading2.Padding = new Padding(5, 3, 5, 3);
			this.lbLoading2.Size = new Size(69, 21);
			this.lbLoading2.TabIndex = 85;
			this.lbLoading2.Text = "Loading ...";
			this.lbLoading2.TextAlign = ContentAlignment.MiddleCenter;
			this.lbLoading2.Visible = false;
			this.intzaMarketInfo.AllowDrop = true;
			this.intzaMarketInfo.BackColor = Color.Black;
			this.intzaMarketInfo.CanDrag = false;
			this.intzaMarketInfo.IsAutoRepaint = true;
			this.intzaMarketInfo.IsDroped = false;
			itemGrid.AdjustFontSize = 0f;
			itemGrid.Alignment = StringAlignment.Center;
			itemGrid.BackColor = Color.FromArgb(45, 45, 45);
			itemGrid.Changed = false;
			itemGrid.FieldType = ItemType.Label;
			itemGrid.FontColor = Color.LightGray;
			itemGrid.FontStyle = FontStyle.Regular;
			itemGrid.Height = 1f;
			itemGrid.IsBlink = 0;
			itemGrid.Name = "lbBoard";
			itemGrid.Text = "Board";
			itemGrid.ValueFormat = FormatType.Text;
			itemGrid.Visible = true;
			itemGrid.Width = 20;
			itemGrid.X = 0;
			itemGrid.Y = 0f;
			itemGrid2.AdjustFontSize = 0f;
			itemGrid2.Alignment = StringAlignment.Center;
			itemGrid2.BackColor = Color.FromArgb(45, 45, 45);
			itemGrid2.Changed = false;
			itemGrid2.FieldType = ItemType.Label;
			itemGrid2.FontColor = Color.LightGray;
			itemGrid2.FontStyle = FontStyle.Regular;
			itemGrid2.Height = 1f;
			itemGrid2.IsBlink = 0;
			itemGrid2.Name = "lbVolume";
			itemGrid2.Text = "Volume";
			itemGrid2.ValueFormat = FormatType.Text;
			itemGrid2.Visible = true;
			itemGrid2.Width = 27;
			itemGrid2.X = 20;
			itemGrid2.Y = 0f;
			itemGrid3.AdjustFontSize = 0f;
			itemGrid3.Alignment = StringAlignment.Center;
			itemGrid3.BackColor = Color.FromArgb(45, 45, 45);
			itemGrid3.Changed = false;
			itemGrid3.FieldType = ItemType.Label;
			itemGrid3.FontColor = Color.LightGray;
			itemGrid3.FontStyle = FontStyle.Regular;
			itemGrid3.Height = 1f;
			itemGrid3.IsBlink = 0;
			itemGrid3.Name = "lbValue";
			itemGrid3.Text = "Value";
			itemGrid3.ValueFormat = FormatType.Text;
			itemGrid3.Visible = true;
			itemGrid3.Width = 33;
			itemGrid3.X = 47;
			itemGrid3.Y = 0f;
			itemGrid4.AdjustFontSize = 0f;
			itemGrid4.Alignment = StringAlignment.Center;
			itemGrid4.BackColor = Color.FromArgb(45, 45, 45);
			itemGrid4.Changed = false;
			itemGrid4.FieldType = ItemType.Text;
			itemGrid4.FontColor = Color.LightGray;
			itemGrid4.FontStyle = FontStyle.Regular;
			itemGrid4.Height = 1f;
			itemGrid4.IsBlink = 0;
			itemGrid4.Name = "lbPvalue";
			itemGrid4.Text = "%Value";
			itemGrid4.ValueFormat = FormatType.Text;
			itemGrid4.Visible = true;
			itemGrid4.Width = 20;
			itemGrid4.X = 80;
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
			itemGrid5.Name = "main_label";
			itemGrid5.Text = "Main";
			itemGrid5.ValueFormat = FormatType.Text;
			itemGrid5.Visible = true;
			itemGrid5.Width = 20;
			itemGrid5.X = 0;
			itemGrid5.Y = 1f;
			itemGrid6.AdjustFontSize = 0f;
			itemGrid6.Alignment = StringAlignment.Far;
			itemGrid6.BackColor = Color.Black;
			itemGrid6.Changed = false;
			itemGrid6.FieldType = ItemType.Text;
			itemGrid6.FontColor = Color.Yellow;
			itemGrid6.FontStyle = FontStyle.Regular;
			itemGrid6.Height = 1f;
			itemGrid6.IsBlink = 0;
			itemGrid6.Name = "main_volume_text";
			itemGrid6.Text = "";
			itemGrid6.ValueFormat = FormatType.Volume;
			itemGrid6.Visible = true;
			itemGrid6.Width = 27;
			itemGrid6.X = 20;
			itemGrid6.Y = 1f;
			itemGrid7.AdjustFontSize = 0f;
			itemGrid7.Alignment = StringAlignment.Far;
			itemGrid7.BackColor = Color.Black;
			itemGrid7.Changed = false;
			itemGrid7.FieldType = ItemType.Text;
			itemGrid7.FontColor = Color.Yellow;
			itemGrid7.FontStyle = FontStyle.Regular;
			itemGrid7.Height = 1f;
			itemGrid7.IsBlink = 0;
			itemGrid7.Name = "main_value_text";
			itemGrid7.Text = "";
			itemGrid7.ValueFormat = FormatType.Volume;
			itemGrid7.Visible = true;
			itemGrid7.Width = 33;
			itemGrid7.X = 47;
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
			itemGrid8.Name = "main_pvalue_text";
			itemGrid8.Text = "";
			itemGrid8.ValueFormat = FormatType.Price;
			itemGrid8.Visible = true;
			itemGrid8.Width = 20;
			itemGrid8.X = 80;
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
			itemGrid9.Name = "odd_label";
			itemGrid9.Text = "OddLot";
			itemGrid9.ValueFormat = FormatType.Text;
			itemGrid9.Visible = true;
			itemGrid9.Width = 20;
			itemGrid9.X = 0;
			itemGrid9.Y = 2f;
			itemGrid10.AdjustFontSize = 0f;
			itemGrid10.Alignment = StringAlignment.Far;
			itemGrid10.BackColor = Color.Black;
			itemGrid10.Changed = false;
			itemGrid10.FieldType = ItemType.Text;
			itemGrid10.FontColor = Color.Yellow;
			itemGrid10.FontStyle = FontStyle.Regular;
			itemGrid10.Height = 1f;
			itemGrid10.IsBlink = 0;
			itemGrid10.Name = "odd_volume_text";
			itemGrid10.Text = "";
			itemGrid10.ValueFormat = FormatType.Volume;
			itemGrid10.Visible = true;
			itemGrid10.Width = 27;
			itemGrid10.X = 20;
			itemGrid10.Y = 2f;
			itemGrid11.AdjustFontSize = 0f;
			itemGrid11.Alignment = StringAlignment.Far;
			itemGrid11.BackColor = Color.Black;
			itemGrid11.Changed = false;
			itemGrid11.FieldType = ItemType.Text;
			itemGrid11.FontColor = Color.Yellow;
			itemGrid11.FontStyle = FontStyle.Regular;
			itemGrid11.Height = 1f;
			itemGrid11.IsBlink = 0;
			itemGrid11.Name = "odd_value_text";
			itemGrid11.Text = "";
			itemGrid11.ValueFormat = FormatType.Volume;
			itemGrid11.Visible = true;
			itemGrid11.Width = 33;
			itemGrid11.X = 47;
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
			itemGrid12.Name = "odd_pvalue_text";
			itemGrid12.Text = "";
			itemGrid12.ValueFormat = FormatType.Price;
			itemGrid12.Visible = true;
			itemGrid12.Width = 20;
			itemGrid12.X = 80;
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
			itemGrid13.Name = "big_label";
			itemGrid13.Text = "BigLot";
			itemGrid13.ValueFormat = FormatType.Text;
			itemGrid13.Visible = true;
			itemGrid13.Width = 20;
			itemGrid13.X = 0;
			itemGrid13.Y = 3f;
			itemGrid14.AdjustFontSize = 0f;
			itemGrid14.Alignment = StringAlignment.Far;
			itemGrid14.BackColor = Color.Black;
			itemGrid14.Changed = false;
			itemGrid14.FieldType = ItemType.Text;
			itemGrid14.FontColor = Color.Yellow;
			itemGrid14.FontStyle = FontStyle.Regular;
			itemGrid14.Height = 1f;
			itemGrid14.IsBlink = 0;
			itemGrid14.Name = "big_volume_text";
			itemGrid14.Text = "";
			itemGrid14.ValueFormat = FormatType.Volume;
			itemGrid14.Visible = true;
			itemGrid14.Width = 27;
			itemGrid14.X = 20;
			itemGrid14.Y = 3f;
			itemGrid15.AdjustFontSize = 0f;
			itemGrid15.Alignment = StringAlignment.Far;
			itemGrid15.BackColor = Color.Black;
			itemGrid15.Changed = false;
			itemGrid15.FieldType = ItemType.Text;
			itemGrid15.FontColor = Color.Yellow;
			itemGrid15.FontStyle = FontStyle.Regular;
			itemGrid15.Height = 1f;
			itemGrid15.IsBlink = 0;
			itemGrid15.Name = "big_value_text";
			itemGrid15.Text = "";
			itemGrid15.ValueFormat = FormatType.Volume;
			itemGrid15.Visible = true;
			itemGrid15.Width = 33;
			itemGrid15.X = 47;
			itemGrid15.Y = 3f;
			itemGrid16.AdjustFontSize = 0f;
			itemGrid16.Alignment = StringAlignment.Far;
			itemGrid16.BackColor = Color.Black;
			itemGrid16.Changed = false;
			itemGrid16.FieldType = ItemType.Text;
			itemGrid16.FontColor = Color.Yellow;
			itemGrid16.FontStyle = FontStyle.Regular;
			itemGrid16.Height = 1f;
			itemGrid16.IsBlink = 0;
			itemGrid16.Name = "big_pvalue_text";
			itemGrid16.Text = "";
			itemGrid16.ValueFormat = FormatType.Price;
			itemGrid16.Visible = true;
			itemGrid16.Width = 20;
			itemGrid16.X = 80;
			itemGrid16.Y = 3f;
			itemGrid17.AdjustFontSize = 0f;
			itemGrid17.Alignment = StringAlignment.Near;
			itemGrid17.BackColor = Color.Black;
			itemGrid17.Changed = false;
			itemGrid17.FieldType = ItemType.Label;
			itemGrid17.FontColor = Color.Gainsboro;
			itemGrid17.FontStyle = FontStyle.Regular;
			itemGrid17.Height = 1f;
			itemGrid17.IsBlink = 0;
			itemGrid17.Name = "foreign_label";
			itemGrid17.Text = "Foreign";
			itemGrid17.ValueFormat = FormatType.Text;
			itemGrid17.Visible = true;
			itemGrid17.Width = 20;
			itemGrid17.X = 0;
			itemGrid17.Y = 4f;
			itemGrid18.AdjustFontSize = 0f;
			itemGrid18.Alignment = StringAlignment.Far;
			itemGrid18.BackColor = Color.Black;
			itemGrid18.Changed = false;
			itemGrid18.FieldType = ItemType.Text;
			itemGrid18.FontColor = Color.Yellow;
			itemGrid18.FontStyle = FontStyle.Regular;
			itemGrid18.Height = 1f;
			itemGrid18.IsBlink = 0;
			itemGrid18.Name = "foreign_volume_text";
			itemGrid18.Text = "";
			itemGrid18.ValueFormat = FormatType.Volume;
			itemGrid18.Visible = true;
			itemGrid18.Width = 27;
			itemGrid18.X = 20;
			itemGrid18.Y = 4f;
			itemGrid19.AdjustFontSize = 0f;
			itemGrid19.Alignment = StringAlignment.Far;
			itemGrid19.BackColor = Color.Black;
			itemGrid19.Changed = false;
			itemGrid19.FieldType = ItemType.Text;
			itemGrid19.FontColor = Color.Yellow;
			itemGrid19.FontStyle = FontStyle.Regular;
			itemGrid19.Height = 1f;
			itemGrid19.IsBlink = 0;
			itemGrid19.Name = "foreign_value_text";
			itemGrid19.Text = "";
			itemGrid19.ValueFormat = FormatType.Volume;
			itemGrid19.Visible = true;
			itemGrid19.Width = 33;
			itemGrid19.X = 47;
			itemGrid19.Y = 4f;
			itemGrid20.AdjustFontSize = 0f;
			itemGrid20.Alignment = StringAlignment.Far;
			itemGrid20.BackColor = Color.Black;
			itemGrid20.Changed = false;
			itemGrid20.FieldType = ItemType.Text;
			itemGrid20.FontColor = Color.Yellow;
			itemGrid20.FontStyle = FontStyle.Regular;
			itemGrid20.Height = 1f;
			itemGrid20.IsBlink = 0;
			itemGrid20.Name = "foreign_pvalue_text";
			itemGrid20.Text = "";
			itemGrid20.ValueFormat = FormatType.Price;
			itemGrid20.Visible = true;
			itemGrid20.Width = 20;
			itemGrid20.X = 80;
			itemGrid20.Y = 4f;
			itemGrid21.AdjustFontSize = 0f;
			itemGrid21.Alignment = StringAlignment.Near;
			itemGrid21.BackColor = Color.Black;
			itemGrid21.Changed = false;
			itemGrid21.FieldType = ItemType.Label2;
			itemGrid21.FontColor = Color.LightGray;
			itemGrid21.FontStyle = FontStyle.Regular;
			itemGrid21.Height = 1f;
			itemGrid21.IsBlink = 0;
			itemGrid21.Name = "tick_label";
			itemGrid21.Text = "Tick";
			itemGrid21.ValueFormat = FormatType.Text;
			itemGrid21.Visible = true;
			itemGrid21.Width = 11;
			itemGrid21.X = 0;
			itemGrid21.Y = 10f;
			itemGrid22.AdjustFontSize = 0f;
			itemGrid22.Alignment = StringAlignment.Near;
			itemGrid22.BackColor = Color.Black;
			itemGrid22.Changed = false;
			itemGrid22.FieldType = ItemType.Text;
			itemGrid22.FontColor = Color.White;
			itemGrid22.FontStyle = FontStyle.Regular;
			itemGrid22.Height = 1f;
			itemGrid22.IsBlink = 0;
			itemGrid22.Name = "tick_text";
			itemGrid22.Text = "";
			itemGrid22.ValueFormat = FormatType.Price;
			itemGrid22.Visible = true;
			itemGrid22.Width = 15;
			itemGrid22.X = 12;
			itemGrid22.Y = 10f;
			itemGrid23.AdjustFontSize = 0f;
			itemGrid23.Alignment = StringAlignment.Near;
			itemGrid23.BackColor = Color.Black;
			itemGrid23.Changed = false;
			itemGrid23.FieldType = ItemType.Label2;
			itemGrid23.FontColor = Color.LightGray;
			itemGrid23.FontStyle = FontStyle.Regular;
			itemGrid23.Height = 1f;
			itemGrid23.IsBlink = 0;
			itemGrid23.Name = "trin_label";
			itemGrid23.Text = "Trin";
			itemGrid23.ValueFormat = FormatType.Volume;
			itemGrid23.Visible = true;
			itemGrid23.Width = 12;
			itemGrid23.X = 30;
			itemGrid23.Y = 10f;
			itemGrid24.AdjustFontSize = 0f;
			itemGrid24.Alignment = StringAlignment.Near;
			itemGrid24.BackColor = Color.Black;
			itemGrid24.Changed = false;
			itemGrid24.FieldType = ItemType.Text;
			itemGrid24.FontColor = Color.White;
			itemGrid24.FontStyle = FontStyle.Regular;
			itemGrid24.Height = 1f;
			itemGrid24.IsBlink = 0;
			itemGrid24.Name = "trin_text";
			itemGrid24.Text = "";
			itemGrid24.ValueFormat = FormatType.Price;
			itemGrid24.Visible = true;
			itemGrid24.Width = 55;
			itemGrid24.X = 43;
			itemGrid24.Y = 10f;
			itemGrid25.AdjustFontSize = 0f;
			itemGrid25.Alignment = StringAlignment.Near;
			itemGrid25.BackColor = Color.Black;
			itemGrid25.Changed = false;
			itemGrid25.FieldType = ItemType.Label2;
			itemGrid25.FontColor = Color.LightGray;
			itemGrid25.FontStyle = FontStyle.Regular;
			itemGrid25.Height = 1f;
			itemGrid25.IsBlink = 0;
			itemGrid25.Name = "up_label";
			itemGrid25.Text = "Up";
			itemGrid25.ValueFormat = FormatType.Text;
			itemGrid25.Visible = true;
			itemGrid25.Width = 11;
			itemGrid25.X = 0;
			itemGrid25.Y = 9f;
			itemGrid26.AdjustFontSize = 0f;
			itemGrid26.Alignment = StringAlignment.Near;
			itemGrid26.BackColor = Color.Black;
			itemGrid26.Changed = false;
			itemGrid26.FieldType = ItemType.Text;
			itemGrid26.FontColor = Color.Lime;
			itemGrid26.FontStyle = FontStyle.Regular;
			itemGrid26.Height = 1f;
			itemGrid26.IsBlink = 0;
			itemGrid26.Name = "up_text";
			itemGrid26.Text = "";
			itemGrid26.ValueFormat = FormatType.Volume;
			itemGrid26.Visible = true;
			itemGrid26.Width = 15;
			itemGrid26.X = 12;
			itemGrid26.Y = 9f;
			itemGrid27.AdjustFontSize = 0f;
			itemGrid27.Alignment = StringAlignment.Near;
			itemGrid27.BackColor = Color.Black;
			itemGrid27.Changed = false;
			itemGrid27.FieldType = ItemType.Label2;
			itemGrid27.FontColor = Color.LightGray;
			itemGrid27.FontStyle = FontStyle.Regular;
			itemGrid27.Height = 1f;
			itemGrid27.IsBlink = 0;
			itemGrid27.Name = "down_label";
			itemGrid27.Text = "Down";
			itemGrid27.ValueFormat = FormatType.Volume;
			itemGrid27.Visible = true;
			itemGrid27.Width = 12;
			itemGrid27.X = 30;
			itemGrid27.Y = 9f;
			itemGrid28.AdjustFontSize = 0f;
			itemGrid28.Alignment = StringAlignment.Near;
			itemGrid28.BackColor = Color.Black;
			itemGrid28.Changed = false;
			itemGrid28.FieldType = ItemType.Text;
			itemGrid28.FontColor = Color.Red;
			itemGrid28.FontStyle = FontStyle.Regular;
			itemGrid28.Height = 1f;
			itemGrid28.IsBlink = 0;
			itemGrid28.Name = "down_text";
			itemGrid28.Text = "";
			itemGrid28.ValueFormat = FormatType.Volume;
			itemGrid28.Visible = true;
			itemGrid28.Width = 15;
			itemGrid28.X = 43;
			itemGrid28.Y = 9f;
			itemGrid29.AdjustFontSize = 0f;
			itemGrid29.Alignment = StringAlignment.Near;
			itemGrid29.BackColor = Color.Black;
			itemGrid29.Changed = false;
			itemGrid29.FieldType = ItemType.Label2;
			itemGrid29.FontColor = Color.LightGray;
			itemGrid29.FontStyle = FontStyle.Regular;
			itemGrid29.Height = 1f;
			itemGrid29.IsBlink = 0;
			itemGrid29.Name = "nochange_label";
			itemGrid29.Text = "UnChg.";
			itemGrid29.ValueFormat = FormatType.Text;
			itemGrid29.Visible = true;
			itemGrid29.Width = 15;
			itemGrid29.X = 60;
			itemGrid29.Y = 9f;
			itemGrid30.AdjustFontSize = 0f;
			itemGrid30.Alignment = StringAlignment.Near;
			itemGrid30.BackColor = Color.Black;
			itemGrid30.Changed = false;
			itemGrid30.FieldType = ItemType.Text;
			itemGrid30.FontColor = Color.Yellow;
			itemGrid30.FontStyle = FontStyle.Regular;
			itemGrid30.Height = 1f;
			itemGrid30.IsBlink = 0;
			itemGrid30.Name = "nochange_text";
			itemGrid30.Text = "";
			itemGrid30.ValueFormat = FormatType.Volume;
			itemGrid30.Visible = true;
			itemGrid30.Width = 13;
			itemGrid30.X = 75;
			itemGrid30.Y = 9f;
			itemGrid31.AdjustFontSize = 0f;
			itemGrid31.Alignment = StringAlignment.Near;
			itemGrid31.BackColor = Color.Black;
			itemGrid31.Changed = false;
			itemGrid31.FieldType = ItemType.Label2;
			itemGrid31.FontColor = Color.LightGray;
			itemGrid31.FontStyle = FontStyle.Regular;
			itemGrid31.Height = 1f;
			itemGrid31.IsBlink = 0;
			itemGrid31.Name = "upvolume_label";
			itemGrid31.Text = "Up Vol";
			itemGrid31.ValueFormat = FormatType.Text;
			itemGrid31.Visible = true;
			itemGrid31.Width = 25;
			itemGrid31.X = 0;
			itemGrid31.Y = 6f;
			itemGrid32.AdjustFontSize = 0f;
			itemGrid32.Alignment = StringAlignment.Far;
			itemGrid32.BackColor = Color.Black;
			itemGrid32.Changed = false;
			itemGrid32.FieldType = ItemType.Text;
			itemGrid32.FontColor = Color.Lime;
			itemGrid32.FontStyle = FontStyle.Regular;
			itemGrid32.Height = 1f;
			itemGrid32.IsBlink = 0;
			itemGrid32.Name = "upvolume_text";
			itemGrid32.Text = "";
			itemGrid32.ValueFormat = FormatType.Volume;
			itemGrid32.Visible = true;
			itemGrid32.Width = 30;
			itemGrid32.X = 25;
			itemGrid32.Y = 6f;
			itemGrid33.AdjustFontSize = 0f;
			itemGrid33.Alignment = StringAlignment.Near;
			itemGrid33.BackColor = Color.Black;
			itemGrid33.Changed = false;
			itemGrid33.FieldType = ItemType.Label2;
			itemGrid33.FontColor = Color.LightGray;
			itemGrid33.FontStyle = FontStyle.Regular;
			itemGrid33.Height = 1f;
			itemGrid33.IsBlink = 0;
			itemGrid33.Name = "downvolume_label";
			itemGrid33.Text = "Down Vol";
			itemGrid33.ValueFormat = FormatType.Text;
			itemGrid33.Visible = true;
			itemGrid33.Width = 25;
			itemGrid33.X = 0;
			itemGrid33.Y = 7f;
			itemGrid34.AdjustFontSize = 0f;
			itemGrid34.Alignment = StringAlignment.Far;
			itemGrid34.BackColor = Color.Black;
			itemGrid34.Changed = false;
			itemGrid34.FieldType = ItemType.Text;
			itemGrid34.FontColor = Color.Red;
			itemGrid34.FontStyle = FontStyle.Regular;
			itemGrid34.Height = 1f;
			itemGrid34.IsBlink = 0;
			itemGrid34.Name = "downvolume_text";
			itemGrid34.Text = "";
			itemGrid34.ValueFormat = FormatType.Volume;
			itemGrid34.Visible = true;
			itemGrid34.Width = 30;
			itemGrid34.X = 25;
			itemGrid34.Y = 7f;
			itemGrid35.AdjustFontSize = 0f;
			itemGrid35.Alignment = StringAlignment.Near;
			itemGrid35.BackColor = Color.Black;
			itemGrid35.Changed = false;
			itemGrid35.FieldType = ItemType.Label2;
			itemGrid35.FontColor = Color.LightGray;
			itemGrid35.FontStyle = FontStyle.Regular;
			itemGrid35.Height = 1f;
			itemGrid35.IsBlink = 0;
			itemGrid35.Name = "nochg_volume_label";
			itemGrid35.Text = "UnChg Vol";
			itemGrid35.ValueFormat = FormatType.Text;
			itemGrid35.Visible = true;
			itemGrid35.Width = 25;
			itemGrid35.X = 0;
			itemGrid35.Y = 8f;
			itemGrid36.AdjustFontSize = 0f;
			itemGrid36.Alignment = StringAlignment.Far;
			itemGrid36.BackColor = Color.Black;
			itemGrid36.Changed = false;
			itemGrid36.FieldType = ItemType.Text;
			itemGrid36.FontColor = Color.Yellow;
			itemGrid36.FontStyle = FontStyle.Regular;
			itemGrid36.Height = 1f;
			itemGrid36.IsBlink = 0;
			itemGrid36.Name = "nochg_volume_text";
			itemGrid36.Text = "";
			itemGrid36.ValueFormat = FormatType.Volume;
			itemGrid36.Visible = true;
			itemGrid36.Width = 30;
			itemGrid36.X = 25;
			itemGrid36.Y = 8f;
			itemGrid37.AdjustFontSize = 0f;
			itemGrid37.Alignment = StringAlignment.Near;
			itemGrid37.BackColor = Color.FromArgb(64, 64, 64);
			itemGrid37.Changed = false;
			itemGrid37.FieldType = ItemType.Text;
			itemGrid37.FontColor = Color.LightGray;
			itemGrid37.FontStyle = FontStyle.Bold;
			itemGrid37.Height = 1f;
			itemGrid37.IsBlink = 0;
			itemGrid37.Name = "sum_label";
			itemGrid37.Text = "Total";
			itemGrid37.ValueFormat = FormatType.Text;
			itemGrid37.Visible = true;
			itemGrid37.Width = 20;
			itemGrid37.X = 0;
			itemGrid37.Y = 5f;
			itemGrid38.AdjustFontSize = 0f;
			itemGrid38.Alignment = StringAlignment.Far;
			itemGrid38.BackColor = Color.FromArgb(64, 64, 64);
			itemGrid38.Changed = false;
			itemGrid38.FieldType = ItemType.Text;
			itemGrid38.FontColor = Color.Yellow;
			itemGrid38.FontStyle = FontStyle.Regular;
			itemGrid38.Height = 1f;
			itemGrid38.IsBlink = 0;
			itemGrid38.Name = "sum_volume";
			itemGrid38.Text = "";
			itemGrid38.ValueFormat = FormatType.Volume;
			itemGrid38.Visible = true;
			itemGrid38.Width = 27;
			itemGrid38.X = 20;
			itemGrid38.Y = 5f;
			itemGrid39.AdjustFontSize = 0f;
			itemGrid39.Alignment = StringAlignment.Far;
			itemGrid39.BackColor = Color.FromArgb(64, 64, 64);
			itemGrid39.Changed = false;
			itemGrid39.FieldType = ItemType.Text;
			itemGrid39.FontColor = Color.Yellow;
			itemGrid39.FontStyle = FontStyle.Regular;
			itemGrid39.Height = 1f;
			itemGrid39.IsBlink = 0;
			itemGrid39.Name = "sum_value";
			itemGrid39.Text = "";
			itemGrid39.ValueFormat = FormatType.Volume;
			itemGrid39.Visible = true;
			itemGrid39.Width = 33;
			itemGrid39.X = 47;
			itemGrid39.Y = 5f;
			itemGrid40.AdjustFontSize = 0f;
			itemGrid40.Alignment = StringAlignment.Near;
			itemGrid40.BackColor = Color.FromArgb(64, 64, 64);
			itemGrid40.Changed = false;
			itemGrid40.FieldType = ItemType.Text;
			itemGrid40.FontColor = Color.White;
			itemGrid40.FontStyle = FontStyle.Regular;
			itemGrid40.Height = 1f;
			itemGrid40.IsBlink = 0;
			itemGrid40.Name = "sum_pvalue";
			itemGrid40.Text = "";
			itemGrid40.ValueFormat = FormatType.Text;
			itemGrid40.Visible = true;
			itemGrid40.Width = 20;
			itemGrid40.X = 80;
			itemGrid40.Y = 5f;
			itemGrid41.AdjustFontSize = 0f;
			itemGrid41.Alignment = StringAlignment.Near;
			itemGrid41.BackColor = Color.Black;
			itemGrid41.Changed = false;
			itemGrid41.FieldType = ItemType.Label2;
			itemGrid41.FontColor = Color.LightGray;
			itemGrid41.FontStyle = FontStyle.Regular;
			itemGrid41.Height = 1f;
			itemGrid41.IsBlink = 0;
			itemGrid41.Name = "maival_label";
			itemGrid41.Text = "MAI Val";
			itemGrid41.ValueFormat = FormatType.Text;
			itemGrid41.Visible = true;
			itemGrid41.Width = 18;
			itemGrid41.X = 57;
			itemGrid41.Y = 6f;
			itemGrid42.AdjustFontSize = 0f;
			itemGrid42.Alignment = StringAlignment.Far;
			itemGrid42.BackColor = Color.Black;
			itemGrid42.Changed = false;
			itemGrid42.FieldType = ItemType.Text;
			itemGrid42.FontColor = Color.Yellow;
			itemGrid42.FontStyle = FontStyle.Regular;
			itemGrid42.Height = 1f;
			itemGrid42.IsBlink = 0;
			itemGrid42.Name = "maival_text";
			itemGrid42.Text = "";
			itemGrid42.ValueFormat = FormatType.Price;
			itemGrid42.Visible = true;
			itemGrid42.Width = 25;
			itemGrid42.X = 75;
			itemGrid42.Y = 6f;
			this.intzaMarketInfo.Items.Add(itemGrid);
			this.intzaMarketInfo.Items.Add(itemGrid2);
			this.intzaMarketInfo.Items.Add(itemGrid3);
			this.intzaMarketInfo.Items.Add(itemGrid4);
			this.intzaMarketInfo.Items.Add(itemGrid5);
			this.intzaMarketInfo.Items.Add(itemGrid6);
			this.intzaMarketInfo.Items.Add(itemGrid7);
			this.intzaMarketInfo.Items.Add(itemGrid8);
			this.intzaMarketInfo.Items.Add(itemGrid9);
			this.intzaMarketInfo.Items.Add(itemGrid10);
			this.intzaMarketInfo.Items.Add(itemGrid11);
			this.intzaMarketInfo.Items.Add(itemGrid12);
			this.intzaMarketInfo.Items.Add(itemGrid13);
			this.intzaMarketInfo.Items.Add(itemGrid14);
			this.intzaMarketInfo.Items.Add(itemGrid15);
			this.intzaMarketInfo.Items.Add(itemGrid16);
			this.intzaMarketInfo.Items.Add(itemGrid17);
			this.intzaMarketInfo.Items.Add(itemGrid18);
			this.intzaMarketInfo.Items.Add(itemGrid19);
			this.intzaMarketInfo.Items.Add(itemGrid20);
			this.intzaMarketInfo.Items.Add(itemGrid21);
			this.intzaMarketInfo.Items.Add(itemGrid22);
			this.intzaMarketInfo.Items.Add(itemGrid23);
			this.intzaMarketInfo.Items.Add(itemGrid24);
			this.intzaMarketInfo.Items.Add(itemGrid25);
			this.intzaMarketInfo.Items.Add(itemGrid26);
			this.intzaMarketInfo.Items.Add(itemGrid27);
			this.intzaMarketInfo.Items.Add(itemGrid28);
			this.intzaMarketInfo.Items.Add(itemGrid29);
			this.intzaMarketInfo.Items.Add(itemGrid30);
			this.intzaMarketInfo.Items.Add(itemGrid31);
			this.intzaMarketInfo.Items.Add(itemGrid32);
			this.intzaMarketInfo.Items.Add(itemGrid33);
			this.intzaMarketInfo.Items.Add(itemGrid34);
			this.intzaMarketInfo.Items.Add(itemGrid35);
			this.intzaMarketInfo.Items.Add(itemGrid36);
			this.intzaMarketInfo.Items.Add(itemGrid37);
			this.intzaMarketInfo.Items.Add(itemGrid38);
			this.intzaMarketInfo.Items.Add(itemGrid39);
			this.intzaMarketInfo.Items.Add(itemGrid40);
			this.intzaMarketInfo.Items.Add(itemGrid41);
			this.intzaMarketInfo.Items.Add(itemGrid42);
			this.intzaMarketInfo.LineColor = Color.Red;
			this.intzaMarketInfo.Location = new Point(350, 26);
			this.intzaMarketInfo.Margin = new Padding(0);
			this.intzaMarketInfo.Name = "intzaMarketInfo";
			this.intzaMarketInfo.Size = new Size(316, 187);
			this.intzaMarketInfo.TabIndex = 23;
			this.intzaMarketInfo.Visible = false;
			this.toolStrip1.AutoSize = false;
			this.toolStrip1.BackColor = Color.FromArgb(20, 20, 20);
			this.toolStrip1.Dock = DockStyle.None;
			this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.tsbtnSETChart,
				this.tsbtnSET50Chart,
				this.tsbtnSET100Chart,
				this.tsbtnSETHdChart,
				this.tsbtnMaiChart,
				this.toolStripSeparator3,
				this.tsbtnInfo
			});
			this.toolStrip1.Location = new Point(350, 1);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Padding = new Padding(1, 1, 1, 2);
			this.toolStrip1.RenderMode = ToolStripRenderMode.Professional;
			this.toolStrip1.Size = new Size(325, 25);
			this.toolStrip1.TabIndex = 24;
			this.toolStrip1.Tag = "-1";
			this.toolStrip1.Text = "toolStrip1";
			this.tsbtnSETChart.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnSETChart.ForeColor = Color.LightGray;
			this.tsbtnSETChart.ImageTransparentColor = Color.Magenta;
			this.tsbtnSETChart.Name = "tsbtnSETChart";
			this.tsbtnSETChart.Size = new Size(30, 19);
			this.tsbtnSETChart.Text = "SET";
			this.tsbtnSETChart.Click += new EventHandler(this.tsbtnSETChart_Click);
			this.tsbtnSET50Chart.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnSET50Chart.ForeColor = Color.LightGray;
			this.tsbtnSET50Chart.ImageTransparentColor = Color.Magenta;
			this.tsbtnSET50Chart.Name = "tsbtnSET50Chart";
			this.tsbtnSET50Chart.Size = new Size(42, 19);
			this.tsbtnSET50Chart.Text = "SET50";
			this.tsbtnSET50Chart.Click += new EventHandler(this.tsbtnSETChart_Click);
			this.tsbtnSET100Chart.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnSET100Chart.ForeColor = Color.LightGray;
			this.tsbtnSET100Chart.ImageTransparentColor = Color.Magenta;
			this.tsbtnSET100Chart.Name = "tsbtnSET100Chart";
			this.tsbtnSET100Chart.Size = new Size(48, 19);
			this.tsbtnSET100Chart.Text = "SET100";
			this.tsbtnSET100Chart.Click += new EventHandler(this.tsbtnSETChart_Click);
			this.tsbtnSETHdChart.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnSETHdChart.ForeColor = Color.LightGray;
			this.tsbtnSETHdChart.ImageTransparentColor = Color.Magenta;
			this.tsbtnSETHdChart.Name = "tsbtnSETHdChart";
			this.tsbtnSETHdChart.Size = new Size(47, 19);
			this.tsbtnSETHdChart.Text = "SETHD";
			this.tsbtnSETHdChart.Click += new EventHandler(this.tsbtnSETChart_Click);
			this.tsbtnMaiChart.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnMaiChart.ForeColor = Color.LightGray;
			this.tsbtnMaiChart.ImageTransparentColor = Color.Magenta;
			this.tsbtnMaiChart.Name = "tsbtnMaiChart";
			this.tsbtnMaiChart.Size = new Size(33, 19);
			this.tsbtnMaiChart.Text = "MAI";
			this.tsbtnMaiChart.Click += new EventHandler(this.tsbtnSETChart_Click);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new Size(6, 22);
			this.tsbtnInfo.ForeColor = Color.LightGray;
			this.tsbtnInfo.Image = (Image)componentResourceManager.GetObject("tsbtnInfo.Image");
			this.tsbtnInfo.ImageTransparentColor = Color.Magenta;
			this.tsbtnInfo.Margin = new Padding(5, 1, 0, 2);
			this.tsbtnInfo.Name = "tsbtnInfo";
			this.tsbtnInfo.Size = new Size(51, 19);
			this.tsbtnInfo.Text = " Info";
			this.tsbtnInfo.Click += new EventHandler(this.tsbtnInfo_Click);
			this.pictureBox1.BackColor = Color.FromArgb(10, 10, 10);
			this.pictureBox1.Location = new Point(360, 67);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new Size(58, 64);
			this.pictureBox1.TabIndex = 82;
			this.pictureBox1.TabStop = false;
			this.lbChartLoading.AutoSize = true;
			this.lbChartLoading.BackColor = Color.FromArgb(64, 64, 64);
			this.lbChartLoading.BorderStyle = BorderStyle.FixedSingle;
			this.lbChartLoading.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.lbChartLoading.ForeColor = Color.Yellow;
			this.lbChartLoading.Location = new Point(360, 150);
			this.lbChartLoading.Name = "lbChartLoading";
			this.lbChartLoading.Padding = new Padding(5, 3, 5, 3);
			this.lbChartLoading.Size = new Size(69, 21);
			this.lbChartLoading.TabIndex = 83;
			this.lbChartLoading.Text = "Loading ...";
			this.lbChartLoading.TextAlign = ContentAlignment.MiddleCenter;
			this.lbChartLoading.Visible = false;
			this.intzaSET.AllowDrop = true;
			this.intzaSET.BackColor = Color.FromArgb(10, 10, 10);
			this.intzaSET.CanBlink = true;
			this.intzaSET.CanDrag = false;
			this.intzaSET.CanGetMouseMove = false;
			columnItem9.Alignment = StringAlignment.Center;
			columnItem9.BackColor = Color.FromArgb(64, 64, 64);
			columnItem9.ColumnAlignment = StringAlignment.Center;
			columnItem9.FontColor = Color.LightGray;
			columnItem9.MyStyle = FontStyle.Regular;
			columnItem9.Name = "name";
			columnItem9.Text = "";
			columnItem9.ValueFormat = FormatType.Text;
			columnItem9.Visible = true;
			columnItem9.Width = 20;
			columnItem10.Alignment = StringAlignment.Far;
			columnItem10.BackColor = Color.FromArgb(64, 64, 64);
			columnItem10.ColumnAlignment = StringAlignment.Center;
			columnItem10.FontColor = Color.LightGray;
			columnItem10.MyStyle = FontStyle.Regular;
			columnItem10.Name = "prior";
			columnItem10.Text = "Prior";
			columnItem10.ValueFormat = FormatType.Price;
			columnItem10.Visible = true;
			columnItem10.Width = 20;
			columnItem11.Alignment = StringAlignment.Far;
			columnItem11.BackColor = Color.FromArgb(64, 64, 64);
			columnItem11.ColumnAlignment = StringAlignment.Center;
			columnItem11.FontColor = Color.LightGray;
			columnItem11.MyStyle = FontStyle.Regular;
			columnItem11.Name = "index";
			columnItem11.Text = "Index";
			columnItem11.ValueFormat = FormatType.Price;
			columnItem11.Visible = true;
			columnItem11.Width = 20;
			columnItem12.Alignment = StringAlignment.Far;
			columnItem12.BackColor = Color.FromArgb(64, 64, 64);
			columnItem12.ColumnAlignment = StringAlignment.Center;
			columnItem12.FontColor = Color.LightGray;
			columnItem12.MyStyle = FontStyle.Regular;
			columnItem12.Name = "chg";
			columnItem12.Text = "Change";
			columnItem12.ValueFormat = FormatType.ChangePrice;
			columnItem12.Visible = true;
			columnItem12.Width = 20;
			columnItem13.Alignment = StringAlignment.Far;
			columnItem13.BackColor = Color.FromArgb(64, 64, 64);
			columnItem13.ColumnAlignment = StringAlignment.Center;
			columnItem13.FontColor = Color.LightGray;
			columnItem13.MyStyle = FontStyle.Regular;
			columnItem13.Name = "pchg";
			columnItem13.Text = "%Change";
			columnItem13.ValueFormat = FormatType.ChangePrice;
			columnItem13.Visible = true;
			columnItem13.Width = 20;
			this.intzaSET.Columns.Add(columnItem9);
			this.intzaSET.Columns.Add(columnItem10);
			this.intzaSET.Columns.Add(columnItem11);
			this.intzaSET.Columns.Add(columnItem12);
			this.intzaSET.Columns.Add(columnItem13);
			this.intzaSET.CurrentScroll = 0;
			this.intzaSET.FocusItemIndex = -1;
			this.intzaSET.ForeColor = Color.Black;
			this.intzaSET.GridColor = Color.FromArgb(45, 45, 45);
			this.intzaSET.HeaderPctHeight = 80f;
			this.intzaSET.IsAutoRepaint = true;
			this.intzaSET.IsDrawFullRow = false;
			this.intzaSET.IsDrawGrid = true;
			this.intzaSET.IsDrawHeader = true;
			this.intzaSET.IsScrollable = true;
			this.intzaSET.Location = new Point(0, 1);
			this.intzaSET.MainColumn = "";
			this.intzaSET.Name = "intzaSET";
			this.intzaSET.Rows = 15;
			this.intzaSET.RowSelectColor = Color.FromArgb(95, 158, 160);
			this.intzaSET.RowSelectType = 0;
			this.intzaSET.RowsVisible = 15;
			this.intzaSET.ScrollChennelColor = Color.Gray;
			this.intzaSET.Size = new Size(347, 207);
			this.intzaSET.SortColumnName = "";
			this.intzaSET.SortType = SortType.Desc;
			this.intzaSET.TabIndex = 85;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			this.BackColor = Color.DimGray;
			base.ClientSize = new Size(675, 403);
			base.Controls.Add(this.intzaMarketInfo);
			base.Controls.Add(this.intzaSET);
			base.Controls.Add(this.lbChartLoading);
			base.Controls.Add(this.pictureBox1);
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.panelSector);
			base.KeyPreview = true;
			base.Name = "frmMarketInfo";
			this.Text = "Sector Statistic";
			base.IDoShownDelay += new ClientBaseForm.OnShownDelayEventHandler(this.frmSectorInformation_IDOShownDelay);
			base.IDoLoadData += new ClientBaseForm.OnIDoLoadDataEventHandler(this.frmMarketInfo_IDoLoadData);
			base.IDoFontChanged += new ClientBaseForm.OnFontChangedEventHandler(this.frmSectorStatistic_IDoFontChanged);
			base.IDoCustomSizeChanged += new ClientBaseForm.CustomSizeChangedEventHandler(this.frmSectorInformation_IDOCustomSizeChanged);
			base.IDoReActivated += new ClientBaseForm.OnReActiveEventHandler(this.frmSectorStatistic_IDoReActivated);
			base.Controls.SetChildIndex(this.panelSector, 0);
			base.Controls.SetChildIndex(this.toolStrip1, 0);
			base.Controls.SetChildIndex(this.pictureBox1, 0);
			base.Controls.SetChildIndex(this.lbChartLoading, 0);
			base.Controls.SetChildIndex(this.intzaSET, 0);
			base.Controls.SetChildIndex(this.intzaMarketInfo, 0);
			this.tStripMenu.ResumeLayout(false);
			this.tStripMenu.PerformLayout();
			this.panelSector.ResumeLayout(false);
			this.panelSector.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			((ISupportInitialize)this.pictureBox1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
