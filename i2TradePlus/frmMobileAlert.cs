using i2TradePlus.Information;
using i2TradePlus.Properties;
using STIControl;
using STIControl.SortTableGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace i2TradePlus
{
	public class frmMobileAlert : Form
	{
		public delegate void OnAlertClickHandler();
		private delegate void ShowSplashCallBack(bool visible, string message, bool isAutoClose);
		private delegate void ShowMessageCallBack(string message);
		private delegate void ShowPushLogMessageCallBack(string message);
		private bool _isInit = false;
		private BackgroundWorker bgwLoadPriceM = null;
		private BackgroundWorker bgwSavePriceM = null;
		private BackgroundWorker bgwLoadPort = null;
		private BackgroundWorker bgwSavePort = null;
		private BackgroundWorker bgwLoadConfig = null;
		private BackgroundWorker bgwSaveConfig = null;
		private BackgroundWorker bgwAlertLog = null;
		private DataSet _tds = null;
		private DataSet _tdsPort = null;
		private DataSet _tdsConfig = null;
		private DataSet _tdsLog = null;
		private string _currentAccount = string.Empty;
		private decimal _costPct = 0m;
		private decimal _pChg = 0m;
		private decimal _sellPct = 0m;
		private int _savePortRecIndex = -1;
		private int _trusteeId = 0;
		private string _stock = string.Empty;
		private string _sType = string.Empty;
		private bool _suppertNoti = false;
		private string _paStock = string.Empty;
		private string _paField = string.Empty;
		private string _paOperator = string.Empty;
		private decimal _paValue = 0m;
		private int _paMode = -1;
		private string _paMemo = string.Empty;
		private string _savePriceAlertResult = string.Empty;
		private bool _isLoading = true;
		private Timer tmCloseSplash = null;
		private long _ordRefNo = -1L;
		private string _tStock = string.Empty;
		private string _tIndicator = string.Empty;
		private string _tCycle = string.Empty;
		private frmOrderFormConfirm frmConfirm = null;
		private string _saveConfigResult = string.Empty;
		private bool _isAlreadyLoadLog = false;
		private bool _isAlreadyLoadConfig = false;
		private bool _isAlreadyLoadPort = false;
		private frmOrderFormConfirm _frmConfirm = null;
		private IContainer components = null;
		private Button btnClose;
		private TabControl tabNoti;
		private TabPage tabPriceAlert;
		private SortGrid intzaM;
		private Label lbPAstock;
		private Label lbPAvalue;
		private Label lbPAoper;
		private Label lbPAfield;
		private ComboBox cbFieldM;
		private TextBox tbValueM;
		private ComboBox cbOperatorM;
		private Button btnSaveM;
		private Button btnDeleteM;
		private Button btnReloadM;
		private Button btnRemoveAllM;
		private TabPage tabPortAlert;
		private SortGrid intzaPortAlert;
		private ComboBox cbAccount;
		private TextBox tbPortPchg;
		private TextBox tbPortPCost;
		private Label lbFAttf;
		private TextBox tbPortTTF;
		private Button btbPortUpdate;
		private Label lbFAstock;
		private Label lbFAtype;
		private TextBox tbPortStock;
		private TextBox tbPortType;
		private Label lbFAChgPricePct;
		private Label lbFACostPct;
		private Label lbLoading;
		private Button btnPortDisable;
		private Button btnReloadPort;
		private TextBox tbMemo;
		private Label lbPAmemo;
		private TextBox tbStockM;
		private ToolTip toolTip1;
		private Button btnPriceDecPct;
		private Button btnPriceIncPct;
		private Button btnCostDecPct;
		private Button btnCostIncPct;
		private Label lbCountText;
		private Button btnClearMemo;
		private Label lbFAaccount;
		private TabPage tabNotSetting;
		private Label label1;
		private Button btnSaveSetting;
		private Label tbLastDevice;
		private Label lbLastDevice;
		private TextBox textBox1;
		private Label label2;
		private TabPage tabLog;
		private SortGrid intzaLog;
		private CheckedListBox clbNotiActive;
		private Button btnRefreshLog;
		public frmMobileAlert.OnAlertClickHandler _OnAlertClick;
		public event frmMobileAlert.OnAlertClickHandler OnAlertClick
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._OnAlertClick += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._OnAlertClick -= value;
			}
		}
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
		public frmMobileAlert()
		{
			this.InitializeComponent();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmAlertOption_Load(object sender, EventArgs e)
		{
			try
			{
				if (!this._isInit)
				{
					if (ApplicationInfo.SupportNod)
					{
						this._suppertNoti = true;
					}
					if (!this._suppertNoti)
					{
						this.tabNoti.Visible = false;
					}
					foreach (KeyValuePair<string, AccountInfo.ItemInfo> current in ApplicationInfo.AccInfo.Items)
					{
						if (current.Value.Market == "E")
						{
							this.cbAccount.Items.Add(current.Key);
						}
					}
					this.cbAccount.Text = ApplicationInfo.AccInfo.CurrentAccount;
					this._currentAccount = ApplicationInfo.AccInfo.CurrentAccount;
					if (this.tbStockM.AutoCompleteCustomSource != null)
					{
						this.tbStockM.AutoCompleteMode = AutoCompleteMode.Suggest;
						this.tbStockM.AutoCompleteSource = AutoCompleteSource.CustomSource;
						if (ApplicationInfo.IsSupportTfex)
						{
							this.tbStockM.AutoCompleteCustomSource = ApplicationInfo.MultiAutoComp;
						}
						else
						{
							this.tbStockM.AutoCompleteCustomSource = ApplicationInfo.StockAutoComp;
						}
					}
					this.bgwLoadPriceM = new BackgroundWorker();
					this.bgwLoadPriceM.DoWork += new DoWorkEventHandler(this.bgwLoadPriceM_DoWork);
					this.bgwLoadPriceM.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwLoadPriceM_RunWorkerCompleted);
					this.bgwSavePriceM = new BackgroundWorker();
					this.bgwSavePriceM.DoWork += new DoWorkEventHandler(this.bgwSavePriceM_DoWork);
					this.bgwSavePriceM.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwSavePrice_RunWorkerCompleted);
					this.bgwLoadPort = new BackgroundWorker();
					this.bgwLoadPort.DoWork += new DoWorkEventHandler(this.bgwLoadPort_DoWork);
					this.bgwLoadPort.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwLoadPort_RunWorkerCompleted);
					this.bgwSavePort = new BackgroundWorker();
					this.bgwSavePort.DoWork += new DoWorkEventHandler(this.bgwSavePort_DoWork);
					this.bgwSavePort.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwSavePort_RunWorkerCompleted);
					this.bgwLoadConfig = new BackgroundWorker();
					this.bgwLoadConfig.DoWork += new DoWorkEventHandler(this.bgwLoadConfig_DoWork);
					this.bgwLoadConfig.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwLoadConfig_RunWorkerCompleted);
					this.bgwSaveConfig = new BackgroundWorker();
					this.bgwSaveConfig.DoWork += new DoWorkEventHandler(this.bgwSaveConfig_DoWork);
					this.bgwSaveConfig.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwSaveConfig_RunWorkerCompleted);
					this.bgwAlertLog = new BackgroundWorker();
					this.bgwAlertLog.DoWork += new DoWorkEventHandler(this.bgwAlertLog_DoWork);
					this.bgwAlertLog.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwAlertLog_RunWorkerCompleted);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("frmAlertOption_Load", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwAlertLog_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				if (this._tdsLog == null)
				{
					this._tdsLog = new DataSet();
				}
				else
				{
					this._tdsLog.Clear();
				}
				this.IsLoadingData = true;
				string alertLog = ApplicationInfo.WebAlertService.GetAlertLog(ApplicationInfo.UserLoginID);
				if (alertLog != string.Empty)
				{
					MyDataHelper.StringToDataSet(alertLog, this._tdsLog);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("bgwAlertLog_DoWork", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwAlertLog_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				if (this._tdsLog != null && this._tdsLog.Tables.Contains("TAB"))
				{
					this.intzaLog.BeginUpdate();
					this.intzaLog.Rows = this._tdsLog.Tables["TAB"].Rows.Count;
					int num = 0;
					foreach (DataRow dataRow in this._tdsLog.Tables["TAB"].Rows)
					{
						RecordItem recordItem = this.intzaLog.Records(num);
						recordItem.Fields("time").Text = dataRow["sTime"].ToString();
						recordItem.Fields("message").Text = dataRow["sMessage"].ToString();
						num++;
					}
					this.intzaLog.Redraw();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("bgwAlertLog_RunWorkerCompleted", ex);
			}
			this.IsLoadingData = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmAlertSetting_Shown(object sender, EventArgs e)
		{
			try
			{
				base.FormBorderStyle = FormBorderStyle.FixedDialog;
			}
			catch (Exception ex)
			{
				this.ShowError("frmAlertSetting_Shown", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Reload()
		{
			try
			{
				if (this._suppertNoti)
				{
					if (!this.bgwLoadPriceM.IsBusy)
					{
						this.bgwLoadPriceM.RunWorkerAsync();
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("Reload", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnAlertDetail_Click(object sender, EventArgs e)
		{
			if (this._OnAlertClick != null)
			{
				this._OnAlertClick();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void controlOrder_Enter(object sender, EventArgs e)
		{
			if (sender == this.tbMemo)
			{
				ApplicationInfo.CanRecvSpace = true;
			}
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
			if (sender == this.tbMemo)
			{
				ApplicationInfo.CanRecvSpace = false;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected virtual void ShowError(string functionName, Exception ex)
		{
			ExceptionManager.Show(new ErrorItem(DateTime.Now, base.Name, functionName, ex.Message, ex.ToString()));
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbAlertValues_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode == Keys.Return)
			{
				this.btnSaveM.PerformClick();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnClose_Click(object sender, EventArgs e)
		{
			try
			{
				base.Hide();
			}
			catch (Exception ex)
			{
				this.ShowError("btnClose_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwLoadPriceM_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				if (this._tds == null)
				{
					this._tds = new DataSet();
				}
				else
				{
					this._tds.Clear();
				}
				this.IsLoadingData = true;
				string stockAlertItems = ApplicationInfo.WebAlertService.GetStockAlertItems(ApplicationInfo.UserLoginID);
				if (stockAlertItems != string.Empty)
				{
					MyDataHelper.StringToDataSet(stockAlertItems, this._tds);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("bgwLoadData_DoWork", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwLoadPriceM_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				if (this._tds != null && this._tds.Tables.Contains("TABLE1"))
				{
					int num = 0;
					this.intzaM.BeginUpdate();
					this.intzaM.Rows = this._tds.Tables["TABLE1"].Rows.Count;
					foreach (DataRow dataRow in this._tds.Tables["TABLE1"].Rows)
					{
						RecordItem recordItem = this.intzaM.Records(num);
						string text = dataRow["field"].ToString().Trim();
						if (text != null)
						{
							if (text == "LAST" || text == "PCHG")
							{
								recordItem.Fields("stock").Text = dataRow["stock"].ToString();
								text = dataRow["field"].ToString().Trim();
								if (text == null)
								{
									goto IL_198;
								}
								if (!(text == "LAST"))
								{
									if (!(text == "PCHG"))
									{
										goto IL_198;
									}
									recordItem.Fields("field").Text = "%Change";
								}
								else
								{
									recordItem.Fields("field").Text = "LastPrice";
								}
								IL_1C3:
								switch (Convert.ToInt32(dataRow["operator"].ToString()))
								{
								case 1:
									recordItem.Fields("operator").Text = "=";
									break;
								case 2:
									recordItem.Fields("operator").Text = ">=";
									break;
								case 3:
									recordItem.Fields("operator").Text = "<=";
									break;
								}
								recordItem.Fields("value").Text = Utilities.PriceFormat(dataRow["value"].ToString(), 2, 0);
								recordItem.Fields("value_at_alert").Text = Utilities.PriceFormat(dataRow["value_at_alert"].ToString());
								recordItem.Fields("alert_time").Text = dataRow["atime"].ToString();
								recordItem.Fields("memo").Text = dataRow["memo"].ToString();
								goto IL_2DF;
								IL_198:
								recordItem.Fields("field").Text = dataRow["field"].ToString().Trim();
								goto IL_1C3;
							}
						}
						IL_2DF:
						num++;
					}
				}
				this.intzaM.Redraw();
				this._tds.Clear();
			}
			catch (Exception ex)
			{
				this.ShowError("bgwLoadData_RunWorkerCompleted", ex);
			}
			this.IsLoadingData = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbStockM_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Right)
			{
				this.cbFieldM.Focus();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbFieldM_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != Keys.Return)
			{
				switch (keyCode)
				{
				case Keys.Left:
					this.tbStockM.Focus();
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
			this.cbOperatorM.Focus();
			e.SuppressKeyPress = true;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbOperatorM_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != Keys.Return)
			{
				switch (keyCode)
				{
				case Keys.Left:
					this.cbFieldM.Focus();
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
			this.tbValueM.Focus();
			e.SuppressKeyPress = true;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbValueM_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != Keys.Return)
			{
				switch (keyCode)
				{
				case Keys.Left:
					this.cbOperatorM.Focus();
					return;
				case Keys.Up:
					return;
				case Keys.Right:
				case Keys.Down:
					break;
				default:
					return;
				}
			}
			this.tbMemo.Focus();
			e.SuppressKeyPress = true;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnSaveM_Click(object sender, EventArgs e)
		{
			try
			{
				if (!decimal.TryParse(this.tbValueM.Text, out this._paValue))
				{
					this.ShowMessage("Invalid value!");
					this.tbValueM.Focus();
				}
				else
				{
					this._paStock = this.tbStockM.Text.Trim().ToUpper();
					this._paField = this.cbFieldM.Text;
					this._paOperator = this.cbOperatorM.Text;
					this._paMemo = this.tbMemo.Text.Trim();
					if (this._paField.ToUpper() == "%CHANGE")
					{
						if (this._paOperator != ">=")
						{
							return;
						}
					}
					bool flag;
					if (this._paStock != string.Empty)
					{
						StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[this._paStock];
						if (stockInformation.Number > 0)
						{
							flag = true;
						}
						else
						{
							if (ApplicationInfo.IsSupportTfex)
							{
								SeriesList.SeriesInformation seriesInformation = ApplicationInfo.SeriesInfo[this._paStock.ToUpper()];
								flag = !string.IsNullOrEmpty(seriesInformation.Symbol);
							}
							else
							{
								flag = false;
							}
						}
					}
					else
					{
						flag = false;
					}
					if (flag)
					{
						int num = -1;
						for (int i = 0; i < this.intzaM.Rows; i++)
						{
							RecordItem recordItem = this.intzaM.Records(i);
							if (recordItem.Fields("stock").Text.ToString() == this._paStock && recordItem.Fields("field").Text.ToString() == this._paField && recordItem.Fields("operator").Text.ToString() == this._paOperator)
							{
								num = i;
								break;
							}
						}
						if (num == -1)
						{
							if (this.intzaM.Rows + 1 > 10)
							{
								this.ShowMessage("เกินจำนวนรายการที่กำหนด (สูงสุด 10 รายการ)");
								return;
							}
						}
						this._paMode = 1;
						if (!this.bgwSavePriceM.IsBusy)
						{
							this.bgwSavePriceM.RunWorkerAsync();
						}
					}
					else
					{
						this.ShowMessage("Invalid Stock!");
						this.tbStockM.Focus();
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("btnAlertAdd_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnRemoveM_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.intzaM.Rows > 0 && this.intzaM.FocusItemIndex > -1)
				{
					this._paStock = this.intzaM.Records(this.intzaM.FocusItemIndex).Fields("stock").Text.ToString();
					this._paField = this.intzaM.Records(this.intzaM.FocusItemIndex).Fields("field").Text.ToString();
					this._paOperator = this.intzaM.Records(this.intzaM.FocusItemIndex).Fields("operator").Text.ToString();
					this._paMode = -1;
					if (!this.bgwSavePriceM.IsBusy)
					{
						this.bgwSavePriceM.RunWorkerAsync();
					}
					this.intzaM.DeleteRecord(this.intzaM.FocusItemIndex);
					this.intzaM.Redraw();
				}
				if (this.intzaM.Rows == 0)
				{
					this.tbStockM.Text = string.Empty;
					this.tbValueM.Text = string.Empty;
					this.tbMemo.Text = string.Empty;
					this.tbStockM.Focus();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("btnAlertRemove_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnRemoveAllM_Click(object sender, EventArgs e)
		{
			try
			{
				this._paStock = string.Empty;
				this._paField = string.Empty;
				this._paMode = -1;
				if (!this.bgwSavePriceM.IsBusy)
				{
					this.bgwSavePriceM.RunWorkerAsync();
				}
				this.intzaM.Rows = 0;
				this.intzaM.Redraw();
			}
			catch (Exception ex)
			{
				this.ShowError("btnRemoveAllM_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnReloadM_Click(object sender, EventArgs e)
		{
			if (!this.bgwLoadPriceM.IsBusy)
			{
				this.bgwLoadPriceM.RunWorkerAsync();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwSavePriceM_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				this.IsLoadingData = true;
				string field = string.Empty;
				string text = this._paField;
				if (text != null)
				{
					if (!(text == "LastPrice"))
					{
						if (text == "%Change")
						{
							field = "PCHG";
						}
					}
					else
					{
						field = "LAST";
					}
				}
				int operatorType = 0;
				text = this._paOperator;
				if (text != null)
				{
					if (!(text == "="))
					{
						if (!(text == ">="))
						{
							if (text == "<=")
							{
								operatorType = 3;
							}
						}
						else
						{
							operatorType = 2;
						}
					}
					else
					{
						operatorType = 1;
					}
				}
				this._savePriceAlertResult = ApplicationInfo.WebAlertService.SaveStockAlert(ApplicationInfo.UserLoginID, this._paStock, field, operatorType, "Y", this._paValue, this._paMode, this._paMemo);
			}
			catch (Exception ex)
			{
				this.ShowError("bgwSavePrice_DoWork", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwSavePrice_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.IsLoadingData = false;
			try
			{
				if (this._savePriceAlertResult == "ok")
				{
					if (this._paMode != -1)
					{
						int num = -1;
						RecordItem recordItem;
						for (int i = 0; i < this.intzaM.Rows; i++)
						{
							recordItem = this.intzaM.Records(i);
							if (recordItem.Fields("stock").Text.ToString() == this._paStock && recordItem.Fields("field").Text.ToString() == this._paField && recordItem.Fields("operator").Text.ToString() == this._paOperator)
							{
								num = i;
								break;
							}
						}
						if (num > -1)
						{
							recordItem = this.intzaM.Records(num);
						}
						else
						{
							recordItem = this.intzaM.AddRecord(-1, false);
						}
						recordItem.Fields("stock").Text = this._paStock;
						recordItem.Fields("field").Text = this._paField;
						recordItem.Fields("operator").Text = this._paOperator;
						recordItem.Fields("value").Text = this._paValue.ToString();
						recordItem.Fields("memo").Text = this._paMemo;
						recordItem.Fields("alert_time").Text = "";
						recordItem.Fields("value_at_alert").Text = "";
						this.intzaM.Redraw();
					}
				}
				else
				{
					this.ShowMessage(this._savePriceAlertResult);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("bgwSavePrice_RunWorkerCompleted", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwLoadPort_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				this.IsLoadingData = true;
				string portAlertItems = ApplicationInfo.WebAlertService.GetPortAlertItems(this._currentAccount);
				if (!string.IsNullOrEmpty(portAlertItems))
				{
					if (this._tdsPort == null)
					{
						this._tdsPort = new DataSet();
					}
					else
					{
						this._tdsPort.Clear();
					}
					MyDataHelper.StringToDataSet(portAlertItems, this._tdsPort);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("bgwLoadReport_DoWork", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwLoadPort_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				this.intzaPortAlert.BeginUpdate();
				this.intzaPortAlert.Rows = 0;
				long num = 0L;
				if (this._tdsPort != null)
				{
					if (this._tdsPort.Tables.Contains("stock_position"))
					{
						foreach (DataRow dataRow in this._tdsPort.Tables["stock_position"].Rows)
						{
							long.TryParse(dataRow["onhand"].ToString(), out num);
							RecordItem recordItem = this.intzaPortAlert.AddRecord(-1, false);
							recordItem.Fields("stock").Text = dataRow["stock"].ToString().Trim();
							recordItem.Fields("sType").Text = dataRow["position_type"].ToString().Trim();
							recordItem.Fields("trusteeId").Text = ((dataRow["trustee_id"].ToString() == "0") ? string.Empty : dataRow["trustee_id"].ToString());
							if (this._tdsPort.Tables.Contains("TABLE1"))
							{
								DataRow[] array = this._tdsPort.Tables["TABLE1"].Select(string.Concat(new string[]
								{
									"stock='",
									dataRow["stock"].ToString().Trim(),
									"'",
									" and stock_type='",
									dataRow["position_type"].ToString().Trim(),
									"'",
									ApplicationInfo.SupportFreewill ? (" and trustee_id='" + dataRow["trustee_id"].ToString().Trim() + "'") : (" and trustee_id=" + dataRow["trustee_id"].ToString().Trim())
								}));
								if (array.Length > 0)
								{
									recordItem.Fields("alarm_time").Text = array[array.Length - 1]["atime"].ToString();
									recordItem.Fields("cost").Text = Utilities.PriceFormat(array[array.Length - 1]["cost_pct"].ToString());
									recordItem.Fields("pchg").Text = Utilities.PriceFormat(array[array.Length - 1]["change_pct"].ToString());
								}
								else
								{
									recordItem.Fields("cost").Text = "";
									recordItem.Fields("pchg").Text = "";
								}
								recordItem.Fields("onhand").Text = Utilities.VolumeFormat(num, true);
							}
							recordItem.Fields("stock").FontColor = Color.Yellow;
							recordItem.Fields("sType").FontColor = Color.Yellow;
							recordItem.Fields("trusteeId").FontColor = Color.Yellow;
							recordItem.Fields("cost").FontColor = Color.Yellow;
							recordItem.Fields("pchg").FontColor = Color.Yellow;
						}
					}
					this._tdsPort.Clear();
				}
				this.intzaPortAlert.Redraw();
			}
			catch (Exception ex)
			{
				this.ShowError("bgwLoadReport_RunWorkerCompleted", ex);
			}
			this._isInit = true;
			this.IsLoadingData = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaPortAlert_TableMouseClick(object sender, TableMouseEventArgs e)
		{
			try
			{
				if (e.RowIndex > -1)
				{
					RecordItem recordItem = this.intzaPortAlert.Records(e.RowIndex);
					this.tbPortStock.Text = recordItem.Fields("stock").Text.ToString();
					this.tbPortType.Text = recordItem.Fields("sType").Text.ToString();
					this.tbPortTTF.Text = recordItem.Fields("trusteeId").Text.ToString();
					this._costPct = 0m;
					this._sellPct = 0m;
					this._pChg = 0m;
					this.tbPortPCost.Text = Utilities.PriceFormat(recordItem.Fields("cost").Text, 2, 0);
					this.tbPortPchg.Text = Utilities.PriceFormat(recordItem.Fields("pchg").Text, 2, 0);
					this.tbPortPCost.Focus();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("intzaPortAlert_TableMouseClick", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (this._isInit)
				{
					this._currentAccount = this.cbAccount.Text.Trim();
					if (!this.bgwLoadPort.IsBusy)
					{
						this.bgwLoadPort.RunWorkerAsync();
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("cbAccount_SelectedIndexChanged", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowSplash(bool visible, string message, bool isAutoClose)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new frmMobileAlert.ShowSplashCallBack(this.ShowSplash), new object[]
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
					if (this.tmCloseSplash == null)
					{
						this.tmCloseSplash = new Timer();
						this.tmCloseSplash.Interval = 1500;
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
		private void intzaM_TableFocusIndexChanged(object sender, int Index)
		{
			try
			{
				if (Index > -1 && Index < this.intzaM.Rows)
				{
					RecordItem recordItem = this.intzaM.Records(Index);
					this.tbStockM.Text = recordItem.Fields("stock").Text.ToString();
					this.cbFieldM.Text = recordItem.Fields("field").Text.ToString();
					this.cbOperatorM.Text = recordItem.Fields("operator").Text.ToString();
					this.tbValueM.Text = recordItem.Fields("value").Text.ToString();
					this.tbMemo.Text = recordItem.Fields("memo").Text.ToString();
					this.tbStockM.Focus();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("intzaM_TableFocusIndexChanged", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btbPortUpdate_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.intzaPortAlert.FocusItemIndex == -1)
				{
					this.ShowMessage("Please select the items you wish to update.");
				}
				else
				{
					if (!this.bgwSavePort.IsBusy)
					{
						if (this.tbPortPCost.Text.Trim() != string.Empty)
						{
							if (!decimal.TryParse(this.tbPortPCost.Text, out this._costPct))
							{
								this.ShowMessage("Invalid value!");
								this.tbPortPCost.Focus();
								return;
							}
							if (this._costPct < 0m)
							{
								this.ShowMessage("Invalid value!");
								this.tbPortPCost.Focus();
								return;
							}
						}
						if (this.tbPortPchg.Text.Trim() != string.Empty)
						{
							if (!decimal.TryParse(this.tbPortPchg.Text, out this._pChg))
							{
								this.ShowMessage("Invalid value!");
								this.tbPortPchg.Focus();
								return;
							}
							if (this._pChg < 0m)
							{
								this.ShowMessage("Invalid value!");
								this.tbPortPchg.Focus();
								return;
							}
						}
						if (this._costPct == 0m && this._pChg == 0m)
						{
							this._sellPct = 0m;
						}
						this.btbPortUpdate.Enabled = false;
						this._stock = this.tbPortStock.Text.Trim().ToUpper();
						this._sType = this.tbPortType.Text.Trim();
						int.TryParse(this.tbPortTTF.Text, out this._trusteeId);
						this._savePortRecIndex = this.intzaPortAlert.FocusItemIndex;
						this.bgwSavePort.RunWorkerAsync();
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("btbPortUpdate_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwSavePort_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				this.IsLoadingData = true;
				if (this._savePortRecIndex > -1)
				{
					this._ordRefNo = ApplicationInfo.WebAlertService.SavePortAlert(ApplicationInfo.UserLoginID, this._currentAccount, this._stock, this._sType, this._trusteeId, this._costPct, this._pChg, this._sellPct);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("bgwSavePort_RunWorkerCompleted", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwSavePort_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				RecordItem recordItem = this.intzaPortAlert.Records(this._savePortRecIndex);
				recordItem.Fields("cost").Text = Utilities.PriceFormat(this._costPct.ToString());
				recordItem.Fields("pchg").Text = Utilities.PriceFormat(this._pChg.ToString());
				recordItem.Fields("alarm_time").Text = string.Empty;
				this.intzaPortAlert.EndUpdate(this._savePortRecIndex);
			}
			catch (Exception ex)
			{
				this.ShowError("bgwSavePort_DoWork", ex);
			}
			this.btbPortUpdate.Enabled = true;
			this.IsLoadingData = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnPortDisable_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this.bgwSavePort.IsBusy)
				{
					if (this.intzaPortAlert.FocusItemIndex > -1)
					{
						this._savePortRecIndex = this.intzaPortAlert.FocusItemIndex;
						RecordItem recordItem = this.intzaPortAlert.Records(this._savePortRecIndex);
						this._stock = recordItem.Fields("stock").Text.ToString();
						this._sType = recordItem.Fields("sType").Text.ToString();
						int.TryParse(recordItem.Fields("trusteeId").Text.ToString(), out this._trusteeId);
						this._costPct = 0m;
						this._pChg = 0m;
						this._sellPct = 0m;
						this.bgwSavePort.RunWorkerAsync();
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("btnPortDisable_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbPortPCost_TextChanged(object sender, EventArgs e)
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbPortPchg_TextChanged(object sender, EventArgs e)
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnReloadPort_Click(object sender, EventArgs e)
		{
			if (!this.bgwLoadPort.IsBusy)
			{
				this.bgwLoadPort.RunWorkerAsync();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbTAstock_KeyPress(object sender, KeyPressEventArgs e)
		{
			try
			{
				e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
			}
			catch (Exception ex)
			{
				this.ShowError("cbStock_KeyPress", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnCostDecPct_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.tbPortStock.Text != string.Empty)
				{
					int num;
					int.TryParse(this.tbPortPCost.Text, out num);
					if (num - 1 > -1)
					{
						num--;
					}
					this.tbPortPCost.Text = num.ToString();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("btnCostDecPct_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnCostIncPct_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.tbPortStock.Text != string.Empty)
				{
					int num;
					int.TryParse(this.tbPortPCost.Text, out num);
					if (num + 1 < 1000)
					{
						num++;
					}
					this.tbPortPCost.Text = num.ToString();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("btnCostIncPct_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnPriceDecPct_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.tbPortStock.Text != string.Empty)
				{
					int num;
					int.TryParse(this.tbPortPchg.Text, out num);
					if (num - 1 > -1)
					{
						num--;
					}
					this.tbPortPchg.Text = num.ToString();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("btnPriceDecPct_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnPriceIncPct_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.tbPortStock.Text != string.Empty)
				{
					int num;
					int.TryParse(this.tbPortPchg.Text, out num);
					if (num + 1 < 1000)
					{
						num++;
					}
					this.tbPortPchg.Text = num.ToString();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("btnPriceDecPct_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbMemo_TextChanged(object sender, EventArgs e)
		{
			this.lbCountText.Text = Convert.ToString(80 - this.tbMemo.Text.Length);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnClearMemo_Click(object sender, EventArgs e)
		{
			this.tbMemo.Clear();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbFieldM_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.cbFieldM.Text == "%Change")
			{
				this.cbOperatorM.Text = ">=";
				this.cbOperatorM.Enabled = false;
			}
			else
			{
				this.cbOperatorM.Enabled = true;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbMemo_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != Keys.Return)
			{
				if (keyCode == Keys.Up)
				{
					this.tbValueM.Focus();
					e.SuppressKeyPress = true;
				}
			}
			else
			{
				this.btnSaveM.PerformClick();
				e.SuppressKeyPress = true;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbPortPCost_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode == Keys.Return || keyCode == Keys.Right)
			{
				this.tbPortPchg.Focus();
				e.SuppressKeyPress = true;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbPortPchg_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != Keys.Return)
			{
				if (keyCode == Keys.Left)
				{
					this.tbPortPCost.Focus();
				}
			}
			else
			{
				this.btbPortUpdate.PerformClick();
				e.SuppressKeyPress = true;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowMessage(string message)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new frmMobileAlert.ShowMessageCallBack(this.ShowMessage), new object[]
				{
					message
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
					this.frmConfirm = new frmOrderFormConfirm(message, "", frmOrderFormConfirm.OpenStyle.ShowBox);
					this.frmConfirm.TopLevel = false;
					this.frmConfirm.Parent = this;
					this.frmConfirm.Location = new Point((base.Width - this.frmConfirm.Width) / 2, (base.Height - this.frmConfirm.Height) / 2);
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
		private void bgwSaveConfig_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				if (this._tdsConfig == null)
				{
					this._tdsConfig = new DataSet();
				}
				else
				{
					this._tdsConfig.Clear();
				}
				this.IsLoadingData = true;
				string text = string.Empty;
				for (int i = 3; i < this.clbNotiActive.Items.Count; i++)
				{
					if (this.clbNotiActive.GetItemChecked(i))
					{
						string[] array = this.clbNotiActive.Items[i].ToString().Split(new char[]
						{
							':'
						});
						if (array.Length > 1)
						{
							text = text + "," + array[0];
						}
					}
				}
				if (text.Length > 0)
				{
					text = text.Substring(1);
				}
				this._saveConfigResult = ApplicationInfo.WebAlertService.SaveUserAlert2(ApplicationInfo.UserLoginID, this.clbNotiActive.GetItemChecked(0), this.clbNotiActive.GetItemChecked(1), this.clbNotiActive.GetItemChecked(2), text);
			}
			catch (Exception ex)
			{
				this.ShowError("bgwSaveConfig_DoWork", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwSaveConfig_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				this.IsLoadingData = false;
				if (!(this._saveConfigResult == "ok"))
				{
					this.ShowMessage(this._saveConfigResult);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("bgwSaveConfig_RunWorkerCompleted", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwLoadConfig_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				if (this._tdsConfig == null)
				{
					this._tdsConfig = new DataSet();
				}
				else
				{
					this._tdsConfig.Clear();
				}
				this.IsLoadingData = true;
				string userAlert = ApplicationInfo.WebAlertService.GetUserAlert(ApplicationInfo.UserLoginID);
				if (userAlert != string.Empty)
				{
					MyDataHelper.StringToDataSet(userAlert, this._tdsConfig);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("bgwLoadConfig_DoWork", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwLoadConfig_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				if (this._tdsConfig != null)
				{
					if (this._tdsConfig != null && this._tdsConfig.Tables.Contains("TABLE1") && this._tdsConfig.Tables["TABLE1"].Rows.Count > 0)
					{
						DataRow dataRow = this._tdsConfig.Tables["TABLE1"].Rows[0];
						string text = "Unknow";
						string text2 = dataRow["sDevice"].ToString().Trim();
						if (text2 != null)
						{
							if (!(text2 == "X"))
							{
								if (!(text2 == "P"))
								{
									if (!(text2 == "M"))
									{
										if (text2 == "T")
										{
											text = "Android Tablet";
										}
									}
									else
									{
										text = "Android Mobile";
									}
								}
								else
								{
									text = "iPad";
								}
							}
							else
							{
								text = "iPhone";
							}
						}
						this.tbLastDevice.Text = text;
						this.clbNotiActive.SetItemChecked(0, dataRow["sRecvAdver"].ToString() == "Y");
						this.clbNotiActive.SetItemChecked(1, dataRow["sRecvSumm"].ToString() == "Y");
						this.clbNotiActive.SetItemChecked(2, dataRow["sPortSumm"].ToString() == "Y");
						if (this._tdsConfig.Tables.Contains("TABLE2"))
						{
							foreach (DataRow dataRow2 in this._tdsConfig.Tables["TABLE2"].Rows)
							{
								this.clbNotiActive.Items.Add(dataRow2["sGroupId"].ToString() + ": " + dataRow2["sGroupDesc"].ToString(), dataRow2["IsActive"].ToString() == "1");
							}
						}
						this._tdsConfig.Clear();
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("bgwLoadConfig_RunWorkerCompleted", ex);
			}
			this.IsLoadingData = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnSaveSetting_Click(object sender, EventArgs e)
		{
			if (!this.bgwSaveConfig.IsBusy)
			{
				this.bgwSaveConfig.RunWorkerAsync();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tabNoti_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (this._suppertNoti)
				{
					if (this.tabNoti.SelectedTab == this.tabLog)
					{
						if (!this.bgwAlertLog.IsBusy && !this._isAlreadyLoadLog)
						{
							this._isAlreadyLoadLog = true;
							this.bgwAlertLog.RunWorkerAsync();
						}
					}
					else
					{
						if (this.tabNoti.SelectedTab == this.tabNotSetting)
						{
							if (!this.bgwLoadConfig.IsBusy && !this._isAlreadyLoadConfig)
							{
								this._isAlreadyLoadConfig = true;
								this.bgwLoadConfig.RunWorkerAsync();
							}
						}
						else
						{
							if (this.tabNoti.SelectedTab == this.tabPortAlert)
							{
								if (!this.bgwLoadPort.IsBusy && !this._isAlreadyLoadPort)
								{
									this._isAlreadyLoadPort = true;
									this.bgwLoadPort.RunWorkerAsync();
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tabNoti_SelectedIndexChanged", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnRefreshLog_Click(object sender, EventArgs e)
		{
			if (!this.bgwAlertLog.IsBusy)
			{
				this.bgwAlertLog.RunWorkerAsync();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowPushLogMessage(string message)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new frmMobileAlert.ShowPushLogMessageCallBack(this.ShowPushLogMessage), new object[]
				{
					message
				});
			}
			else
			{
				try
				{
					if (this._frmConfirm != null)
					{
						if (!this._frmConfirm.IsDisposed)
						{
							this._frmConfirm.Dispose();
						}
						this._frmConfirm = null;
					}
					this._frmConfirm = new frmOrderFormConfirm(message, "", frmOrderFormConfirm.OpenStyle.ShowBox);
					this._frmConfirm.TopLevel = false;
					this._frmConfirm.Parent = this;
					this._frmConfirm.Location = new Point((base.Width - this._frmConfirm.Width) / 2, (base.Height - this._frmConfirm.Height) / 2);
					this._frmConfirm.TopMost = true;
					this._frmConfirm.Show();
					this._frmConfirm.BringToFront();
				}
				catch (Exception ex)
				{
					this.ShowError("ShowMessageInFormConfirm", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaLog_TableMouseDoubleClick(object sender, TableMouseEventArgs e)
		{
			try
			{
				this.ShowPushLogMessage(this.intzaLog.Records(e.RowIndex).Fields("message").Text.ToString());
			}
			catch (Exception ex)
			{
				this.ShowError("intzaLog_TableMouseDoubleClick", ex);
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmMobileAlert));
			this.btnClose = new Button();
			this.tabNoti = new TabControl();
			this.tabPriceAlert = new TabPage();
			this.btnRemoveAllM = new Button();
			this.btnClearMemo = new Button();
			this.btnDeleteM = new Button();
			this.lbCountText = new Label();
			this.btnReloadM = new Button();
			this.tbStockM = new TextBox();
			this.tbMemo = new TextBox();
			this.lbPAmemo = new Label();
			this.btnSaveM = new Button();
			this.intzaM = new SortGrid();
			this.lbPAstock = new Label();
			this.lbPAvalue = new Label();
			this.lbPAoper = new Label();
			this.lbPAfield = new Label();
			this.cbFieldM = new ComboBox();
			this.tbValueM = new TextBox();
			this.cbOperatorM = new ComboBox();
			this.tabPortAlert = new TabPage();
			this.label2 = new Label();
			this.label1 = new Label();
			this.intzaPortAlert = new SortGrid();
			this.lbFAaccount = new Label();
			this.btnReloadPort = new Button();
			this.btnPortDisable = new Button();
			this.lbFAChgPricePct = new Label();
			this.lbFACostPct = new Label();
			this.tbPortPchg = new TextBox();
			this.tbPortStock = new TextBox();
			this.cbAccount = new ComboBox();
			this.tbPortType = new TextBox();
			this.tbPortTTF = new TextBox();
			this.lbFAtype = new Label();
			this.lbFAttf = new Label();
			this.btbPortUpdate = new Button();
			this.lbFAstock = new Label();
			this.tbPortPCost = new TextBox();
			this.btnPriceDecPct = new Button();
			this.btnPriceIncPct = new Button();
			this.btnCostDecPct = new Button();
			this.btnCostIncPct = new Button();
			this.tabLog = new TabPage();
			this.btnRefreshLog = new Button();
			this.intzaLog = new SortGrid();
			this.tabNotSetting = new TabPage();
			this.clbNotiActive = new CheckedListBox();
			this.textBox1 = new TextBox();
			this.tbLastDevice = new Label();
			this.lbLastDevice = new Label();
			this.btnSaveSetting = new Button();
			this.lbLoading = new Label();
			this.toolTip1 = new ToolTip(this.components);
			this.tabNoti.SuspendLayout();
			this.tabPriceAlert.SuspendLayout();
			this.tabPortAlert.SuspendLayout();
			this.tabLog.SuspendLayout();
			this.tabNotSetting.SuspendLayout();
			base.SuspendLayout();
			this.btnClose.AutoSize = true;
			this.btnClose.FlatAppearance.BorderColor = Color.Gray;
			this.btnClose.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnClose.FlatAppearance.MouseOverBackColor = Color.MediumAquamarine;
			this.btnClose.FlatStyle = FlatStyle.Flat;
			this.btnClose.Location = new Point(566, 383);
			this.btnClose.Margin = new Padding(3, 2, 3, 2);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new Size(80, 33);
			this.btnClose.TabIndex = 92;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new EventHandler(this.btnClose_Click);
			this.tabNoti.Appearance = TabAppearance.Buttons;
			this.tabNoti.Controls.Add(this.tabPriceAlert);
			this.tabNoti.Controls.Add(this.tabPortAlert);
			this.tabNoti.Controls.Add(this.tabLog);
			this.tabNoti.Controls.Add(this.tabNotSetting);
			this.tabNoti.Location = new Point(3, 3);
			this.tabNoti.Name = "tabNoti";
			this.tabNoti.SelectedIndex = 0;
			this.tabNoti.Size = new Size(650, 379);
			this.tabNoti.SizeMode = TabSizeMode.Fixed;
			this.tabNoti.TabIndex = 97;
			this.tabNoti.SelectedIndexChanged += new EventHandler(this.tabNoti_SelectedIndexChanged);
			this.tabPriceAlert.BackColor = Color.Silver;
			this.tabPriceAlert.Controls.Add(this.btnRemoveAllM);
			this.tabPriceAlert.Controls.Add(this.btnClearMemo);
			this.tabPriceAlert.Controls.Add(this.btnDeleteM);
			this.tabPriceAlert.Controls.Add(this.lbCountText);
			this.tabPriceAlert.Controls.Add(this.btnReloadM);
			this.tabPriceAlert.Controls.Add(this.tbStockM);
			this.tabPriceAlert.Controls.Add(this.tbMemo);
			this.tabPriceAlert.Controls.Add(this.lbPAmemo);
			this.tabPriceAlert.Controls.Add(this.btnSaveM);
			this.tabPriceAlert.Controls.Add(this.intzaM);
			this.tabPriceAlert.Controls.Add(this.lbPAstock);
			this.tabPriceAlert.Controls.Add(this.lbPAvalue);
			this.tabPriceAlert.Controls.Add(this.lbPAoper);
			this.tabPriceAlert.Controls.Add(this.lbPAfield);
			this.tabPriceAlert.Controls.Add(this.cbFieldM);
			this.tabPriceAlert.Controls.Add(this.tbValueM);
			this.tabPriceAlert.Controls.Add(this.cbOperatorM);
			this.tabPriceAlert.Location = new Point(4, 28);
			this.tabPriceAlert.Name = "tabPriceAlert";
			this.tabPriceAlert.Padding = new Padding(3);
			this.tabPriceAlert.Size = new Size(642, 347);
			this.tabPriceAlert.TabIndex = 1;
			this.tabPriceAlert.Text = "Price Alert";
			this.tabPriceAlert.UseVisualStyleBackColor = true;
			this.btnRemoveAllM.AutoSize = true;
			this.btnRemoveAllM.FlatAppearance.BorderColor = Color.Gray;
			this.btnRemoveAllM.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnRemoveAllM.FlatAppearance.MouseOverBackColor = Color.MediumAquamarine;
			this.btnRemoveAllM.FlatStyle = FlatStyle.Flat;
			this.btnRemoveAllM.ImageAlign = ContentAlignment.MiddleLeft;
			this.btnRemoveAllM.Location = new Point(531, 8);
			this.btnRemoveAllM.Margin = new Padding(3, 2, 3, 2);
			this.btnRemoveAllM.Name = "btnRemoveAllM";
			this.btnRemoveAllM.Size = new Size(106, 33);
			this.btnRemoveAllM.TabIndex = 97;
			this.btnRemoveAllM.Text = "Remove All";
			this.btnRemoveAllM.UseVisualStyleBackColor = true;
			this.btnRemoveAllM.Click += new EventHandler(this.btnRemoveAllM_Click);
			this.btnClearMemo.AutoSize = true;
			this.btnClearMemo.FlatAppearance.BorderColor = Color.Gray;
			this.btnClearMemo.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnClearMemo.FlatAppearance.MouseOverBackColor = Color.MediumAquamarine;
			this.btnClearMemo.FlatStyle = FlatStyle.Flat;
			this.btnClearMemo.Font = new Font("Tahoma", 9f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.btnClearMemo.ImageAlign = ContentAlignment.MiddleLeft;
			this.btnClearMemo.Location = new Point(497, 281);
			this.btnClearMemo.Margin = new Padding(3, 2, 3, 2);
			this.btnClearMemo.Name = "btnClearMemo";
			this.btnClearMemo.Size = new Size(99, 30);
			this.btnClearMemo.TabIndex = 101;
			this.btnClearMemo.Text = "Clear Memo";
			this.btnClearMemo.UseVisualStyleBackColor = true;
			this.btnClearMemo.Click += new EventHandler(this.btnClearMemo_Click);
			this.btnDeleteM.AutoSize = true;
			this.btnDeleteM.FlatAppearance.BorderColor = Color.Gray;
			this.btnDeleteM.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnDeleteM.FlatAppearance.MouseOverBackColor = Color.MediumAquamarine;
			this.btnDeleteM.FlatStyle = FlatStyle.Flat;
			this.btnDeleteM.ImageAlign = ContentAlignment.MiddleLeft;
			this.btnDeleteM.Location = new Point(446, 8);
			this.btnDeleteM.Margin = new Padding(3, 2, 3, 2);
			this.btnDeleteM.Name = "btnDeleteM";
			this.btnDeleteM.Size = new Size(82, 33);
			this.btnDeleteM.TabIndex = 93;
			this.btnDeleteM.Text = "Remove";
			this.btnDeleteM.UseVisualStyleBackColor = true;
			this.btnDeleteM.Click += new EventHandler(this.btnRemoveM_Click);
			this.lbCountText.AutoSize = true;
			this.lbCountText.Location = new Point(494, 313);
			this.lbCountText.Name = "lbCountText";
			this.lbCountText.Size = new Size(22, 16);
			this.lbCountText.TabIndex = 100;
			this.lbCountText.Text = "80";
			this.btnReloadM.AutoSize = true;
			this.btnReloadM.FlatAppearance.BorderColor = Color.Gray;
			this.btnReloadM.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnReloadM.FlatAppearance.MouseOverBackColor = Color.MediumAquamarine;
			this.btnReloadM.FlatStyle = FlatStyle.Flat;
			this.btnReloadM.ImageAlign = ContentAlignment.MiddleLeft;
			this.btnReloadM.Location = new Point(11, 8);
			this.btnReloadM.Margin = new Padding(3, 2, 3, 2);
			this.btnReloadM.Name = "btnReloadM";
			this.btnReloadM.Size = new Size(73, 33);
			this.btnReloadM.TabIndex = 95;
			this.btnReloadM.Text = "Reload";
			this.btnReloadM.UseVisualStyleBackColor = true;
			this.btnReloadM.Click += new EventHandler(this.btnReloadM_Click);
			this.tbStockM.AllowDrop = true;
			this.tbStockM.BorderStyle = BorderStyle.FixedSingle;
			this.tbStockM.CharacterCasing = CharacterCasing.Upper;
			this.tbStockM.Location = new Point(23, 244);
			this.tbStockM.Margin = new Padding(3, 2, 3, 2);
			this.tbStockM.MaxLength = 9;
			this.tbStockM.Name = "tbStockM";
			this.tbStockM.Size = new Size(106, 23);
			this.tbStockM.TabIndex = 99;
			this.tbStockM.KeyDown += new KeyEventHandler(this.tbStockM_KeyDown);
			this.tbStockM.Leave += new EventHandler(this.controlOrder_Leave);
			this.tbStockM.Enter += new EventHandler(this.controlOrder_Enter);
			this.tbMemo.AllowDrop = true;
			this.tbMemo.BorderStyle = BorderStyle.FixedSingle;
			this.tbMemo.Location = new Point(135, 272);
			this.tbMemo.Margin = new Padding(3, 2, 3, 2);
			this.tbMemo.MaxLength = 80;
			this.tbMemo.Multiline = true;
			this.tbMemo.Name = "tbMemo";
			this.tbMemo.Size = new Size(344, 57);
			this.tbMemo.TabIndex = 98;
			this.tbMemo.TextChanged += new EventHandler(this.tbMemo_TextChanged);
			this.tbMemo.KeyDown += new KeyEventHandler(this.tbMemo_KeyDown);
			this.tbMemo.Leave += new EventHandler(this.controlOrder_Leave);
			this.tbMemo.Enter += new EventHandler(this.controlOrder_Enter);
			this.lbPAmemo.AutoSize = true;
			this.lbPAmemo.Location = new Point(73, 274);
			this.lbPAmemo.Name = "lbPAmemo";
			this.lbPAmemo.Size = new Size(56, 16);
			this.lbPAmemo.TabIndex = 97;
			this.lbPAmemo.Text = "Memo : ";
			this.btnSaveM.AutoSize = true;
			this.btnSaveM.FlatAppearance.BorderColor = Color.Gray;
			this.btnSaveM.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnSaveM.FlatAppearance.MouseOverBackColor = Color.MediumAquamarine;
			this.btnSaveM.FlatStyle = FlatStyle.Flat;
			this.btnSaveM.ImageAlign = ContentAlignment.MiddleLeft;
			this.btnSaveM.Location = new Point(497, 244);
			this.btnSaveM.Margin = new Padding(3, 2, 3, 2);
			this.btnSaveM.Name = "btnSaveM";
			this.btnSaveM.Size = new Size(99, 33);
			this.btnSaveM.TabIndex = 92;
			this.btnSaveM.Text = "Update";
			this.btnSaveM.UseVisualStyleBackColor = true;
			this.btnSaveM.Click += new EventHandler(this.btnSaveM_Click);
			this.intzaM.AllowDrop = true;
			this.intzaM.BackColor = Color.FromArgb(20, 20, 20);
			this.intzaM.CanBlink = false;
			this.intzaM.CanDrag = false;
			this.intzaM.CanGetMouseMove = false;
			columnItem.Alignment = StringAlignment.Near;
			columnItem.BackColor = Color.FromArgb(64, 64, 64);
			columnItem.ColumnAlignment = StringAlignment.Center;
			columnItem.FontColor = Color.LightGray;
			columnItem.MyStyle = FontStyle.Regular;
			columnItem.Name = "stock";
			columnItem.Text = "Stock";
			columnItem.ValueFormat = FormatType.Text;
			columnItem.Visible = true;
			columnItem.Width = 18;
			columnItem2.Alignment = StringAlignment.Center;
			columnItem2.BackColor = Color.FromArgb(64, 64, 64);
			columnItem2.ColumnAlignment = StringAlignment.Center;
			columnItem2.FontColor = Color.LightGray;
			columnItem2.MyStyle = FontStyle.Regular;
			columnItem2.Name = "field";
			columnItem2.Text = "Field";
			columnItem2.ValueFormat = FormatType.Text;
			columnItem2.Visible = true;
			columnItem2.Width = 13;
			columnItem3.Alignment = StringAlignment.Center;
			columnItem3.BackColor = Color.FromArgb(64, 64, 64);
			columnItem3.ColumnAlignment = StringAlignment.Center;
			columnItem3.FontColor = Color.LightGray;
			columnItem3.MyStyle = FontStyle.Regular;
			columnItem3.Name = "operator";
			columnItem3.Text = "Operator";
			columnItem3.ValueFormat = FormatType.Text;
			columnItem3.Visible = true;
			columnItem3.Width = 13;
			columnItem4.Alignment = StringAlignment.Center;
			columnItem4.BackColor = Color.FromArgb(64, 64, 64);
			columnItem4.ColumnAlignment = StringAlignment.Center;
			columnItem4.FontColor = Color.LightGray;
			columnItem4.MyStyle = FontStyle.Regular;
			columnItem4.Name = "value";
			columnItem4.Text = "Value";
			columnItem4.ValueFormat = FormatType.Text;
			columnItem4.Visible = true;
			columnItem4.Width = 13;
			columnItem5.Alignment = StringAlignment.Near;
			columnItem5.BackColor = Color.FromArgb(64, 64, 64);
			columnItem5.ColumnAlignment = StringAlignment.Center;
			columnItem5.FontColor = Color.LightGray;
			columnItem5.MyStyle = FontStyle.Regular;
			columnItem5.Name = "memo";
			columnItem5.Text = "Memo";
			columnItem5.ValueFormat = FormatType.Text;
			columnItem5.Visible = true;
			columnItem5.Width = 28;
			columnItem6.Alignment = StringAlignment.Center;
			columnItem6.BackColor = Color.FromArgb(64, 64, 64);
			columnItem6.ColumnAlignment = StringAlignment.Center;
			columnItem6.FontColor = Color.FromArgb(255, 128, 0);
			columnItem6.MyStyle = FontStyle.Regular;
			columnItem6.Name = "alert_time";
			columnItem6.Text = "Alert Time";
			columnItem6.ValueFormat = FormatType.Text;
			columnItem6.Visible = true;
			columnItem6.Width = 15;
			columnItem7.Alignment = StringAlignment.Center;
			columnItem7.BackColor = Color.FromArgb(64, 64, 64);
			columnItem7.ColumnAlignment = StringAlignment.Center;
			columnItem7.FontColor = Color.FromArgb(255, 128, 0);
			columnItem7.MyStyle = FontStyle.Regular;
			columnItem7.Name = "value_at_alert";
			columnItem7.Text = "Alert Price";
			columnItem7.ValueFormat = FormatType.Text;
			columnItem7.Visible = false;
			columnItem7.Width = 0;
			this.intzaM.Columns.Add(columnItem);
			this.intzaM.Columns.Add(columnItem2);
			this.intzaM.Columns.Add(columnItem3);
			this.intzaM.Columns.Add(columnItem4);
			this.intzaM.Columns.Add(columnItem5);
			this.intzaM.Columns.Add(columnItem6);
			this.intzaM.Columns.Add(columnItem7);
			this.intzaM.CurrentScroll = 0;
			this.intzaM.FocusItemIndex = -1;
			this.intzaM.ForeColor = Color.Black;
			this.intzaM.GridColor = Color.FromArgb(45, 45, 45);
			this.intzaM.HeaderPctHeight = 100f;
			this.intzaM.IsAutoRepaint = true;
			this.intzaM.IsDrawFullRow = false;
			this.intzaM.IsDrawGrid = true;
			this.intzaM.IsDrawHeader = true;
			this.intzaM.IsScrollable = true;
			this.intzaM.Location = new Point(3, 45);
			this.intzaM.MainColumn = "";
			this.intzaM.Margin = new Padding(3, 2, 3, 2);
			this.intzaM.Name = "intzaM";
			this.intzaM.Rows = 0;
			this.intzaM.RowSelectColor = Color.CadetBlue;
			this.intzaM.RowSelectType = 3;
			this.intzaM.RowsVisible = 0;
			this.intzaM.ScrollChennelColor = Color.Gray;
			this.intzaM.Size = new Size(635, 170);
			this.intzaM.SortColumnName = "";
			this.intzaM.SortType = SortType.Desc;
			this.intzaM.TabIndex = 91;
			this.intzaM.TableFocusIndexChanged += new SortGrid.TableFocusIndexChangedEventHandler(this.intzaM_TableFocusIndexChanged);
			this.lbPAstock.AutoSize = true;
			this.lbPAstock.Location = new Point(59, 220);
			this.lbPAstock.Name = "lbPAstock";
			this.lbPAstock.Size = new Size(39, 16);
			this.lbPAstock.TabIndex = 42;
			this.lbPAstock.Text = "Stock";
			this.lbPAvalue.AutoSize = true;
			this.lbPAvalue.Location = new Point(404, 220);
			this.lbPAvalue.Name = "lbPAvalue";
			this.lbPAvalue.Size = new Size(40, 16);
			this.lbPAvalue.TabIndex = 45;
			this.lbPAvalue.Text = "Value";
			this.lbPAoper.AutoSize = true;
			this.lbPAoper.Location = new Point(265, 220);
			this.lbPAoper.Name = "lbPAoper";
			this.lbPAoper.Size = new Size(59, 16);
			this.lbPAoper.TabIndex = 44;
			this.lbPAoper.Text = "Operator";
			this.lbPAfield.AutoSize = true;
			this.lbPAfield.Location = new Point(162, 220);
			this.lbPAfield.Name = "lbPAfield";
			this.lbPAfield.Size = new Size(35, 16);
			this.lbPAfield.TabIndex = 43;
			this.lbPAfield.Text = "Field";
			this.cbFieldM.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbFieldM.FlatStyle = FlatStyle.Flat;
			this.cbFieldM.FormattingEnabled = true;
			this.cbFieldM.Items.AddRange(new object[]
			{
				"LastPrice",
				"%Change"
			});
			this.cbFieldM.Location = new Point(135, 244);
			this.cbFieldM.Margin = new Padding(3, 2, 3, 2);
			this.cbFieldM.Name = "cbFieldM";
			this.cbFieldM.Size = new Size(106, 24);
			this.cbFieldM.TabIndex = 39;
			this.cbFieldM.SelectedIndexChanged += new EventHandler(this.cbFieldM_SelectedIndexChanged);
			this.cbFieldM.Leave += new EventHandler(this.controlOrder_Leave);
			this.cbFieldM.Enter += new EventHandler(this.controlOrder_Enter);
			this.cbFieldM.KeyDown += new KeyEventHandler(this.cbFieldM_KeyDown);
			this.tbValueM.AllowDrop = true;
			this.tbValueM.BorderStyle = BorderStyle.FixedSingle;
			this.tbValueM.Location = new Point(373, 244);
			this.tbValueM.Margin = new Padding(3, 2, 3, 2);
			this.tbValueM.MaxLength = 9;
			this.tbValueM.Name = "tbValueM";
			this.tbValueM.Size = new Size(106, 23);
			this.tbValueM.TabIndex = 41;
			this.tbValueM.KeyDown += new KeyEventHandler(this.tbValueM_KeyDown);
			this.tbValueM.Leave += new EventHandler(this.controlOrder_Leave);
			this.tbValueM.Enter += new EventHandler(this.controlOrder_Enter);
			this.cbOperatorM.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbOperatorM.FlatStyle = FlatStyle.Flat;
			this.cbOperatorM.FormattingEnabled = true;
			this.cbOperatorM.Items.AddRange(new object[]
			{
				">=",
				"<="
			});
			this.cbOperatorM.Location = new Point(247, 244);
			this.cbOperatorM.Margin = new Padding(3, 2, 3, 2);
			this.cbOperatorM.Name = "cbOperatorM";
			this.cbOperatorM.Size = new Size(106, 24);
			this.cbOperatorM.TabIndex = 40;
			this.cbOperatorM.Leave += new EventHandler(this.controlOrder_Leave);
			this.cbOperatorM.Enter += new EventHandler(this.controlOrder_Enter);
			this.cbOperatorM.KeyDown += new KeyEventHandler(this.cbOperatorM_KeyDown);
			this.tabPortAlert.BackColor = Color.Silver;
			this.tabPortAlert.Controls.Add(this.label2);
			this.tabPortAlert.Controls.Add(this.label1);
			this.tabPortAlert.Controls.Add(this.intzaPortAlert);
			this.tabPortAlert.Controls.Add(this.lbFAaccount);
			this.tabPortAlert.Controls.Add(this.btnReloadPort);
			this.tabPortAlert.Controls.Add(this.btnPortDisable);
			this.tabPortAlert.Controls.Add(this.lbFAChgPricePct);
			this.tabPortAlert.Controls.Add(this.lbFACostPct);
			this.tabPortAlert.Controls.Add(this.tbPortPchg);
			this.tabPortAlert.Controls.Add(this.tbPortStock);
			this.tabPortAlert.Controls.Add(this.cbAccount);
			this.tabPortAlert.Controls.Add(this.tbPortType);
			this.tabPortAlert.Controls.Add(this.tbPortTTF);
			this.tabPortAlert.Controls.Add(this.lbFAtype);
			this.tabPortAlert.Controls.Add(this.lbFAttf);
			this.tabPortAlert.Controls.Add(this.btbPortUpdate);
			this.tabPortAlert.Controls.Add(this.lbFAstock);
			this.tabPortAlert.Controls.Add(this.tbPortPCost);
			this.tabPortAlert.Controls.Add(this.btnPriceDecPct);
			this.tabPortAlert.Controls.Add(this.btnPriceIncPct);
			this.tabPortAlert.Controls.Add(this.btnCostDecPct);
			this.tabPortAlert.Controls.Add(this.btnCostIncPct);
			this.tabPortAlert.Location = new Point(4, 28);
			this.tabPortAlert.Name = "tabPortAlert";
			this.tabPortAlert.Padding = new Padding(3);
			this.tabPortAlert.Size = new Size(642, 347);
			this.tabPortAlert.TabIndex = 2;
			this.tabPortAlert.Text = "Portfolio Alert";
			this.tabPortAlert.UseVisualStyleBackColor = true;
			this.label2.AutoSize = true;
			this.label2.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.label2.ForeColor = Color.Gray;
			this.label2.Location = new Point(295, 20);
			this.label2.Name = "label2";
			this.label2.Size = new Size(151, 13);
			this.label2.TabIndex = 135;
			this.label2.Text = "* Support equity account only";
			this.label1.AutoSize = true;
			this.label1.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.label1.ForeColor = Color.Gray;
			this.label1.Location = new Point(203, 328);
			this.label1.Name = "label1";
			this.label1.Size = new Size(134, 13);
			this.label1.TabIndex = 134;
			this.label1.Text = "* Not include commissions.";
			this.intzaPortAlert.AllowDrop = true;
			this.intzaPortAlert.BackColor = Color.FromArgb(20, 20, 20);
			this.intzaPortAlert.CanBlink = false;
			this.intzaPortAlert.CanDrag = false;
			this.intzaPortAlert.CanGetMouseMove = false;
			columnItem8.Alignment = StringAlignment.Near;
			columnItem8.BackColor = Color.FromArgb(64, 64, 64);
			columnItem8.ColumnAlignment = StringAlignment.Center;
			columnItem8.FontColor = Color.LightGray;
			columnItem8.MyStyle = FontStyle.Regular;
			columnItem8.Name = "stock";
			columnItem8.Text = "Stock";
			columnItem8.ValueFormat = FormatType.Text;
			columnItem8.Visible = true;
			columnItem8.Width = 19;
			columnItem9.Alignment = StringAlignment.Center;
			columnItem9.BackColor = Color.FromArgb(64, 64, 64);
			columnItem9.ColumnAlignment = StringAlignment.Center;
			columnItem9.FontColor = Color.LightGray;
			columnItem9.MyStyle = FontStyle.Regular;
			columnItem9.Name = "sType";
			columnItem9.Text = "Type";
			columnItem9.ValueFormat = FormatType.Text;
			columnItem9.Visible = true;
			columnItem9.Width = 8;
			columnItem10.Alignment = StringAlignment.Center;
			columnItem10.BackColor = Color.FromArgb(64, 64, 64);
			columnItem10.ColumnAlignment = StringAlignment.Center;
			columnItem10.FontColor = Color.LightGray;
			columnItem10.MyStyle = FontStyle.Regular;
			columnItem10.Name = "trusteeId";
			columnItem10.Text = "TTF";
			columnItem10.ValueFormat = FormatType.Text;
			columnItem10.Visible = true;
			columnItem10.Width = 8;
			columnItem11.Alignment = StringAlignment.Far;
			columnItem11.BackColor = Color.FromArgb(64, 64, 64);
			columnItem11.ColumnAlignment = StringAlignment.Center;
			columnItem11.FontColor = Color.LightGray;
			columnItem11.MyStyle = FontStyle.Regular;
			columnItem11.Name = "onhand";
			columnItem11.Text = "OnHand";
			columnItem11.ValueFormat = FormatType.Text;
			columnItem11.Visible = true;
			columnItem11.Width = 20;
			columnItem12.Alignment = StringAlignment.Center;
			columnItem12.BackColor = Color.FromArgb(64, 64, 64);
			columnItem12.ColumnAlignment = StringAlignment.Center;
			columnItem12.FontColor = Color.LightGray;
			columnItem12.MyStyle = FontStyle.Regular;
			columnItem12.Name = "cost";
			columnItem12.Text = "Cost (%)";
			columnItem12.ValueFormat = FormatType.Text;
			columnItem12.Visible = true;
			columnItem12.Width = 15;
			columnItem13.Alignment = StringAlignment.Center;
			columnItem13.BackColor = Color.FromArgb(64, 64, 64);
			columnItem13.ColumnAlignment = StringAlignment.Center;
			columnItem13.FontColor = Color.LightGray;
			columnItem13.MyStyle = FontStyle.Regular;
			columnItem13.Name = "pchg";
			columnItem13.Text = "Chg (%)";
			columnItem13.ValueFormat = FormatType.Text;
			columnItem13.Visible = true;
			columnItem13.Width = 15;
			columnItem14.Alignment = StringAlignment.Center;
			columnItem14.BackColor = Color.FromArgb(64, 64, 64);
			columnItem14.ColumnAlignment = StringAlignment.Center;
			columnItem14.FontColor = Color.FromArgb(255, 128, 0);
			columnItem14.MyStyle = FontStyle.Regular;
			columnItem14.Name = "alarm_time";
			columnItem14.Text = "Alert Time";
			columnItem14.ValueFormat = FormatType.Text;
			columnItem14.Visible = true;
			columnItem14.Width = 15;
			this.intzaPortAlert.Columns.Add(columnItem8);
			this.intzaPortAlert.Columns.Add(columnItem9);
			this.intzaPortAlert.Columns.Add(columnItem10);
			this.intzaPortAlert.Columns.Add(columnItem11);
			this.intzaPortAlert.Columns.Add(columnItem12);
			this.intzaPortAlert.Columns.Add(columnItem13);
			this.intzaPortAlert.Columns.Add(columnItem14);
			this.intzaPortAlert.CurrentScroll = 0;
			this.intzaPortAlert.FocusItemIndex = -1;
			this.intzaPortAlert.ForeColor = Color.Black;
			this.intzaPortAlert.GridColor = Color.FromArgb(30, 30, 30);
			this.intzaPortAlert.HeaderPctHeight = 100f;
			this.intzaPortAlert.IsAutoRepaint = true;
			this.intzaPortAlert.IsDrawFullRow = false;
			this.intzaPortAlert.IsDrawGrid = true;
			this.intzaPortAlert.IsDrawHeader = true;
			this.intzaPortAlert.IsScrollable = true;
			this.intzaPortAlert.Location = new Point(3, 44);
			this.intzaPortAlert.MainColumn = "";
			this.intzaPortAlert.Name = "intzaPortAlert";
			this.intzaPortAlert.Rows = 0;
			this.intzaPortAlert.RowSelectColor = Color.Gray;
			this.intzaPortAlert.RowSelectType = 3;
			this.intzaPortAlert.RowsVisible = 0;
			this.intzaPortAlert.ScrollChennelColor = Color.Gray;
			this.intzaPortAlert.Size = new Size(635, 224);
			this.intzaPortAlert.SortColumnName = "";
			this.intzaPortAlert.SortType = SortType.Desc;
			this.intzaPortAlert.TabIndex = 16;
			this.intzaPortAlert.TableMouseClick += new SortGrid.TableMouseClickEventHandler(this.intzaPortAlert_TableMouseClick);
			this.lbFAaccount.AutoSize = true;
			this.lbFAaccount.Location = new Point(15, 17);
			this.lbFAaccount.Name = "lbFAaccount";
			this.lbFAaccount.Size = new Size(62, 16);
			this.lbFAaccount.TabIndex = 130;
			this.lbFAaccount.Text = "Account :";
			this.btnReloadPort.AutoSize = true;
			this.btnReloadPort.FlatAppearance.BorderColor = Color.Gray;
			this.btnReloadPort.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnReloadPort.FlatAppearance.MouseOverBackColor = Color.MediumAquamarine;
			this.btnReloadPort.FlatStyle = FlatStyle.Flat;
			this.btnReloadPort.ImageAlign = ContentAlignment.MiddleLeft;
			this.btnReloadPort.Location = new Point(223, 11);
			this.btnReloadPort.Margin = new Padding(3, 2, 3, 2);
			this.btnReloadPort.Name = "btnReloadPort";
			this.btnReloadPort.Size = new Size(64, 28);
			this.btnReloadPort.TabIndex = 124;
			this.btnReloadPort.Text = "Reload";
			this.btnReloadPort.UseVisualStyleBackColor = true;
			this.btnReloadPort.Click += new EventHandler(this.btnReloadPort_Click);
			this.btnPortDisable.FlatAppearance.BorderColor = Color.Gray;
			this.btnPortDisable.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnPortDisable.FlatAppearance.MouseOverBackColor = Color.MediumAquamarine;
			this.btnPortDisable.FlatStyle = FlatStyle.Flat;
			this.btnPortDisable.ImageAlign = ContentAlignment.MiddleLeft;
			this.btnPortDisable.Location = new Point(527, 303);
			this.btnPortDisable.Margin = new Padding(3, 2, 3, 2);
			this.btnPortDisable.Name = "btnPortDisable";
			this.btnPortDisable.Size = new Size(63, 26);
			this.btnPortDisable.TabIndex = 121;
			this.btnPortDisable.Text = "Clear";
			this.btnPortDisable.UseVisualStyleBackColor = true;
			this.btnPortDisable.Click += new EventHandler(this.btnPortDisable_Click);
			this.lbFAChgPricePct.AutoSize = true;
			this.lbFAChgPricePct.BackColor = SystemColors.ControlDarkDark;
			this.lbFAChgPricePct.ForeColor = Color.White;
			this.lbFAChgPricePct.Location = new Point(328, 280);
			this.lbFAChgPricePct.Name = "lbFAChgPricePct";
			this.lbFAChgPricePct.Padding = new Padding(1);
			this.lbFAChgPricePct.Size = new Size(79, 18);
			this.lbFAChgPricePct.TabIndex = 119;
			this.lbFAChgPricePct.Text = "Change (%)";
			this.lbFACostPct.AutoSize = true;
			this.lbFACostPct.BackColor = SystemColors.ControlDarkDark;
			this.lbFACostPct.ForeColor = Color.White;
			this.lbFACostPct.Location = new Point(217, 280);
			this.lbFACostPct.Name = "lbFACostPct";
			this.lbFACostPct.Padding = new Padding(1);
			this.lbFACostPct.Size = new Size(61, 18);
			this.lbFACostPct.TabIndex = 118;
			this.lbFACostPct.Text = "Cost (%)";
			this.tbPortPchg.AllowDrop = true;
			this.tbPortPchg.BorderStyle = BorderStyle.FixedSingle;
			this.tbPortPchg.Location = new Point(343, 303);
			this.tbPortPchg.Margin = new Padding(3, 2, 3, 2);
			this.tbPortPchg.MaxLength = 9;
			this.tbPortPchg.Name = "tbPortPchg";
			this.tbPortPchg.Size = new Size(48, 23);
			this.tbPortPchg.TabIndex = 112;
			this.tbPortPchg.TextAlign = HorizontalAlignment.Center;
			this.toolTip1.SetToolTip(this.tbPortPchg, "ราคาเปลี่ยนแปลงเทียบกับราคาปิดวันก่อนหน้า");
			this.tbPortPchg.TextChanged += new EventHandler(this.tbPortPchg_TextChanged);
			this.tbPortPchg.KeyDown += new KeyEventHandler(this.tbPortPchg_KeyDown);
			this.tbPortPchg.Leave += new EventHandler(this.controlOrder_Leave);
			this.tbPortPchg.Enter += new EventHandler(this.controlOrder_Enter);
			this.tbPortStock.AllowDrop = true;
			this.tbPortStock.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			this.tbPortStock.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.tbPortStock.BackColor = Color.Silver;
			this.tbPortStock.BorderStyle = BorderStyle.FixedSingle;
			this.tbPortStock.CharacterCasing = CharacterCasing.Upper;
			this.tbPortStock.Location = new Point(9, 303);
			this.tbPortStock.Margin = new Padding(3, 2, 3, 2);
			this.tbPortStock.MaxLength = 10;
			this.tbPortStock.Name = "tbPortStock";
			this.tbPortStock.ReadOnly = true;
			this.tbPortStock.Size = new Size(93, 23);
			this.tbPortStock.TabIndex = 97;
			this.tbPortStock.TextAlign = HorizontalAlignment.Center;
			this.cbAccount.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbAccount.FlatStyle = FlatStyle.Popup;
			this.cbAccount.FormattingEnabled = true;
			this.cbAccount.Location = new Point(85, 12);
			this.cbAccount.Name = "cbAccount";
			this.cbAccount.Size = new Size(126, 24);
			this.cbAccount.TabIndex = 18;
			this.cbAccount.SelectedIndexChanged += new EventHandler(this.cbAccount_SelectedIndexChanged);
			this.tbPortType.AllowDrop = true;
			this.tbPortType.BackColor = Color.Silver;
			this.tbPortType.BorderStyle = BorderStyle.FixedSingle;
			this.tbPortType.Location = new Point(104, 303);
			this.tbPortType.Margin = new Padding(3, 2, 3, 2);
			this.tbPortType.MaxLength = 9;
			this.tbPortType.Name = "tbPortType";
			this.tbPortType.ReadOnly = true;
			this.tbPortType.Size = new Size(40, 23);
			this.tbPortType.TabIndex = 100;
			this.tbPortType.TextAlign = HorizontalAlignment.Center;
			this.tbPortTTF.AllowDrop = true;
			this.tbPortTTF.BackColor = Color.Silver;
			this.tbPortTTF.BorderStyle = BorderStyle.FixedSingle;
			this.tbPortTTF.Location = new Point(146, 303);
			this.tbPortTTF.Margin = new Padding(3, 2, 3, 2);
			this.tbPortTTF.MaxLength = 9;
			this.tbPortTTF.Name = "tbPortTTF";
			this.tbPortTTF.ReadOnly = true;
			this.tbPortTTF.Size = new Size(35, 23);
			this.tbPortTTF.TabIndex = 106;
			this.tbPortTTF.TextAlign = HorizontalAlignment.Center;
			this.lbFAtype.AutoSize = true;
			this.lbFAtype.Location = new Point(107, 280);
			this.lbFAtype.Name = "lbFAtype";
			this.lbFAtype.Size = new Size(36, 16);
			this.lbFAtype.TabIndex = 104;
			this.lbFAtype.Text = "Type";
			this.lbFAttf.AutoSize = true;
			this.lbFAttf.Location = new Point(148, 280);
			this.lbFAttf.Name = "lbFAttf";
			this.lbFAttf.Size = new Size(31, 16);
			this.lbFAttf.TabIndex = 107;
			this.lbFAttf.Text = "TTF";
			this.btbPortUpdate.FlatAppearance.BorderColor = Color.Gray;
			this.btbPortUpdate.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btbPortUpdate.FlatAppearance.MouseOverBackColor = Color.MediumAquamarine;
			this.btbPortUpdate.FlatStyle = FlatStyle.Flat;
			this.btbPortUpdate.ImageAlign = ContentAlignment.MiddleLeft;
			this.btbPortUpdate.Location = new Point(456, 303);
			this.btbPortUpdate.Margin = new Padding(3, 2, 3, 2);
			this.btbPortUpdate.Name = "btbPortUpdate";
			this.btbPortUpdate.Size = new Size(65, 26);
			this.btbPortUpdate.TabIndex = 105;
			this.btbPortUpdate.Text = "Update";
			this.btbPortUpdate.UseVisualStyleBackColor = true;
			this.btbPortUpdate.Click += new EventHandler(this.btbPortUpdate_Click);
			this.lbFAstock.AutoSize = true;
			this.lbFAstock.Location = new Point(39, 280);
			this.lbFAstock.Name = "lbFAstock";
			this.lbFAstock.Size = new Size(39, 16);
			this.lbFAstock.TabIndex = 101;
			this.lbFAstock.Text = "Stock";
			this.tbPortPCost.AllowDrop = true;
			this.tbPortPCost.BorderStyle = BorderStyle.FixedSingle;
			this.tbPortPCost.Location = new Point(224, 303);
			this.tbPortPCost.Margin = new Padding(3, 2, 3, 2);
			this.tbPortPCost.MaxLength = 9;
			this.tbPortPCost.Name = "tbPortPCost";
			this.tbPortPCost.Size = new Size(48, 23);
			this.tbPortPCost.TabIndex = 108;
			this.tbPortPCost.TextAlign = HorizontalAlignment.Center;
			this.toolTip1.SetToolTip(this.tbPortPCost, "ต้นทุนเฉลี่ยสะสมเปลี่ยนแปลง (+,-)");
			this.tbPortPCost.TextChanged += new EventHandler(this.tbPortPCost_TextChanged);
			this.tbPortPCost.KeyDown += new KeyEventHandler(this.tbPortPCost_KeyDown);
			this.tbPortPCost.Leave += new EventHandler(this.controlOrder_Leave);
			this.tbPortPCost.Enter += new EventHandler(this.controlOrder_Enter);
			this.btnPriceDecPct.FlatAppearance.BorderSize = 0;
			this.btnPriceDecPct.FlatAppearance.MouseDownBackColor = Color.Cyan;
			this.btnPriceDecPct.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 128, 0);
			this.btnPriceDecPct.FlatStyle = FlatStyle.Flat;
			this.btnPriceDecPct.Image = Resources.Minus;
			this.btnPriceDecPct.Location = new Point(315, 302);
			this.btnPriceDecPct.Name = "btnPriceDecPct";
			this.btnPriceDecPct.Size = new Size(25, 23);
			this.btnPriceDecPct.TabIndex = 128;
			this.btnPriceDecPct.UseVisualStyleBackColor = true;
			this.btnPriceDecPct.Click += new EventHandler(this.btnPriceDecPct_Click);
			this.btnPriceIncPct.FlatAppearance.BorderSize = 0;
			this.btnPriceIncPct.FlatAppearance.MouseDownBackColor = Color.Cyan;
			this.btnPriceIncPct.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 128, 0);
			this.btnPriceIncPct.FlatStyle = FlatStyle.Flat;
			this.btnPriceIncPct.Image = Resources.Plus;
			this.btnPriceIncPct.Location = new Point(392, 302);
			this.btnPriceIncPct.Name = "btnPriceIncPct";
			this.btnPriceIncPct.Size = new Size(25, 23);
			this.btnPriceIncPct.TabIndex = 127;
			this.btnPriceIncPct.UseVisualStyleBackColor = true;
			this.btnPriceIncPct.Click += new EventHandler(this.btnPriceIncPct_Click);
			this.btnCostDecPct.FlatAppearance.BorderSize = 0;
			this.btnCostDecPct.FlatAppearance.MouseDownBackColor = Color.Cyan;
			this.btnCostDecPct.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 128, 0);
			this.btnCostDecPct.FlatStyle = FlatStyle.Flat;
			this.btnCostDecPct.Image = Resources.Minus;
			this.btnCostDecPct.Location = new Point(197, 303);
			this.btnCostDecPct.Name = "btnCostDecPct";
			this.btnCostDecPct.Size = new Size(25, 23);
			this.btnCostDecPct.TabIndex = 126;
			this.btnCostDecPct.UseVisualStyleBackColor = true;
			this.btnCostDecPct.Click += new EventHandler(this.btnCostDecPct_Click);
			this.btnCostIncPct.FlatAppearance.BorderSize = 0;
			this.btnCostIncPct.FlatAppearance.MouseDownBackColor = Color.Cyan;
			this.btnCostIncPct.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 128, 0);
			this.btnCostIncPct.FlatStyle = FlatStyle.Flat;
			this.btnCostIncPct.Image = Resources.Plus;
			this.btnCostIncPct.Location = new Point(275, 303);
			this.btnCostIncPct.Name = "btnCostIncPct";
			this.btnCostIncPct.Size = new Size(25, 23);
			this.btnCostIncPct.TabIndex = 125;
			this.btnCostIncPct.UseVisualStyleBackColor = true;
			this.btnCostIncPct.Click += new EventHandler(this.btnCostIncPct_Click);
			this.tabLog.BackColor = Color.Silver;
			this.tabLog.Controls.Add(this.btnRefreshLog);
			this.tabLog.Controls.Add(this.intzaLog);
			this.tabLog.Location = new Point(4, 28);
			this.tabLog.Name = "tabLog";
			this.tabLog.Padding = new Padding(3);
			this.tabLog.Size = new Size(642, 347);
			this.tabLog.TabIndex = 5;
			this.tabLog.Text = "Sent Log";
			this.btnRefreshLog.AutoSize = true;
			this.btnRefreshLog.FlatAppearance.BorderColor = Color.Gray;
			this.btnRefreshLog.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnRefreshLog.FlatAppearance.MouseOverBackColor = Color.MediumAquamarine;
			this.btnRefreshLog.FlatStyle = FlatStyle.Flat;
			this.btnRefreshLog.ImageAlign = ContentAlignment.MiddleLeft;
			this.btnRefreshLog.Location = new Point(573, 3);
			this.btnRefreshLog.Margin = new Padding(3, 2, 3, 2);
			this.btnRefreshLog.Name = "btnRefreshLog";
			this.btnRefreshLog.Size = new Size(64, 28);
			this.btnRefreshLog.TabIndex = 125;
			this.btnRefreshLog.Text = "Reload";
			this.btnRefreshLog.UseVisualStyleBackColor = true;
			this.btnRefreshLog.Click += new EventHandler(this.btnRefreshLog_Click);
			this.intzaLog.AllowDrop = true;
			this.intzaLog.BackColor = Color.FromArgb(20, 20, 20);
			this.intzaLog.CanBlink = false;
			this.intzaLog.CanDrag = false;
			this.intzaLog.CanGetMouseMove = false;
			columnItem15.Alignment = StringAlignment.Center;
			columnItem15.BackColor = Color.FromArgb(64, 64, 64);
			columnItem15.ColumnAlignment = StringAlignment.Center;
			columnItem15.FontColor = Color.LightGray;
			columnItem15.MyStyle = FontStyle.Regular;
			columnItem15.Name = "time";
			columnItem15.Text = "Time";
			columnItem15.ValueFormat = FormatType.Text;
			columnItem15.Visible = true;
			columnItem15.Width = 12;
			columnItem16.Alignment = StringAlignment.Near;
			columnItem16.BackColor = Color.FromArgb(64, 64, 64);
			columnItem16.ColumnAlignment = StringAlignment.Center;
			columnItem16.FontColor = Color.LightGray;
			columnItem16.MyStyle = FontStyle.Regular;
			columnItem16.Name = "message";
			columnItem16.Text = "Message";
			columnItem16.ValueFormat = FormatType.Text;
			columnItem16.Visible = true;
			columnItem16.Width = 88;
			this.intzaLog.Columns.Add(columnItem15);
			this.intzaLog.Columns.Add(columnItem16);
			this.intzaLog.CurrentScroll = 0;
			this.intzaLog.FocusItemIndex = -1;
			this.intzaLog.ForeColor = Color.Black;
			this.intzaLog.GridColor = Color.FromArgb(45, 45, 45);
			this.intzaLog.HeaderPctHeight = 100f;
			this.intzaLog.IsAutoRepaint = true;
			this.intzaLog.IsDrawFullRow = false;
			this.intzaLog.IsDrawGrid = true;
			this.intzaLog.IsDrawHeader = true;
			this.intzaLog.IsScrollable = true;
			this.intzaLog.Location = new Point(3, 34);
			this.intzaLog.MainColumn = "";
			this.intzaLog.Margin = new Padding(3, 2, 3, 2);
			this.intzaLog.Name = "intzaLog";
			this.intzaLog.Rows = 0;
			this.intzaLog.RowSelectColor = Color.CadetBlue;
			this.intzaLog.RowSelectType = 3;
			this.intzaLog.RowsVisible = 0;
			this.intzaLog.ScrollChennelColor = Color.Gray;
			this.intzaLog.Size = new Size(635, 308);
			this.intzaLog.SortColumnName = "";
			this.intzaLog.SortType = SortType.Desc;
			this.intzaLog.TabIndex = 92;
			this.intzaLog.TableMouseDoubleClick += new SortGrid.TableMouseDoubleClickEventHandler(this.intzaLog_TableMouseDoubleClick);
			this.tabNotSetting.BackColor = Color.Silver;
			this.tabNotSetting.Controls.Add(this.clbNotiActive);
			this.tabNotSetting.Controls.Add(this.textBox1);
			this.tabNotSetting.Controls.Add(this.tbLastDevice);
			this.tabNotSetting.Controls.Add(this.lbLastDevice);
			this.tabNotSetting.Controls.Add(this.btnSaveSetting);
			this.tabNotSetting.Location = new Point(4, 28);
			this.tabNotSetting.Name = "tabNotSetting";
			this.tabNotSetting.Padding = new Padding(3);
			this.tabNotSetting.Size = new Size(642, 347);
			this.tabNotSetting.TabIndex = 4;
			this.tabNotSetting.Text = "Setting";
			this.tabNotSetting.UseVisualStyleBackColor = true;
			this.clbNotiActive.FormattingEnabled = true;
			this.clbNotiActive.Items.AddRange(new object[]
			{
				"รับ โฆษณา",
				"รับ สรุปสภาวะตลาดฯ (SET Index, Most-Active และอื่นๆ )",
				"รับ ข้อความเมื่อรายการซื้อขายจับคู่หมดแล้ว และสรุปพอร์ต ณ สิ้นวัน"
			});
			this.clbNotiActive.Location = new Point(24, 33);
			this.clbNotiActive.Name = "clbNotiActive";
			this.clbNotiActive.Size = new Size(479, 130);
			this.clbNotiActive.TabIndex = 136;
			this.textBox1.Location = new Point(24, 175);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new Size(549, 111);
			this.textBox1.TabIndex = 135;
			this.textBox1.Text = componentResourceManager.GetString("textBox1.Text");
			this.tbLastDevice.AutoSize = true;
			this.tbLastDevice.Font = new Font("Tahoma", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 222);
			this.tbLastDevice.Location = new Point(118, 11);
			this.tbLastDevice.Name = "tbLastDevice";
			this.tbLastDevice.Size = new Size(14, 16);
			this.tbLastDevice.TabIndex = 134;
			this.tbLastDevice.Text = "-";
			this.lbLastDevice.AutoSize = true;
			this.lbLastDevice.Location = new Point(27, 11);
			this.lbLastDevice.Name = "lbLastDevice";
			this.lbLastDevice.Size = new Size(81, 16);
			this.lbLastDevice.TabIndex = 133;
			this.lbLastDevice.Text = "Last Device :";
			this.btnSaveSetting.AutoSize = true;
			this.btnSaveSetting.FlatAppearance.BorderColor = Color.Gray;
			this.btnSaveSetting.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnSaveSetting.FlatAppearance.MouseOverBackColor = Color.MediumAquamarine;
			this.btnSaveSetting.FlatStyle = FlatStyle.Flat;
			this.btnSaveSetting.ImageAlign = ContentAlignment.MiddleLeft;
			this.btnSaveSetting.Location = new Point(509, 128);
			this.btnSaveSetting.Margin = new Padding(3, 2, 3, 2);
			this.btnSaveSetting.Name = "btnSaveSetting";
			this.btnSaveSetting.Size = new Size(64, 33);
			this.btnSaveSetting.TabIndex = 132;
			this.btnSaveSetting.Text = "Save";
			this.btnSaveSetting.UseVisualStyleBackColor = true;
			this.btnSaveSetting.Click += new EventHandler(this.btnSaveSetting_Click);
			this.lbLoading.AutoSize = true;
			this.lbLoading.BackColor = Color.FromArgb(64, 64, 64);
			this.lbLoading.BorderStyle = BorderStyle.FixedSingle;
			this.lbLoading.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.lbLoading.ForeColor = Color.Yellow;
			this.lbLoading.Location = new Point(272, 388);
			this.lbLoading.Name = "lbLoading";
			this.lbLoading.Padding = new Padding(5, 4, 5, 4);
			this.lbLoading.Size = new Size(73, 25);
			this.lbLoading.TabIndex = 98;
			this.lbLoading.Text = "Loading...";
			this.lbLoading.TextAlign = ContentAlignment.MiddleCenter;
			this.lbLoading.Visible = false;
			this.AllowDrop = true;
			base.AutoScaleDimensions = new SizeF(7f, 16f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(654, 420);
			base.ControlBox = false;
			base.Controls.Add(this.tabNoti);
			base.Controls.Add(this.lbLoading);
			base.Controls.Add(this.btnClose);
			this.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 222);
			base.Margin = new Padding(3, 2, 3, 2);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmMobileAlert";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Mobile Notification";
			base.Load += new EventHandler(this.frmAlertOption_Load);
			base.Shown += new EventHandler(this.frmAlertSetting_Shown);
			this.tabNoti.ResumeLayout(false);
			this.tabPriceAlert.ResumeLayout(false);
			this.tabPriceAlert.PerformLayout();
			this.tabPortAlert.ResumeLayout(false);
			this.tabPortAlert.PerformLayout();
			this.tabLog.ResumeLayout(false);
			this.tabLog.PerformLayout();
			this.tabNotSetting.ResumeLayout(false);
			this.tabNotSetting.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
