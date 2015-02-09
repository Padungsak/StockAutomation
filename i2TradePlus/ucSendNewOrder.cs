using i2TradePlus.Controls;
using i2TradePlus.Information;
using i2TradePlus.Properties;
using i2TradePlus.Templates;
using i2TradePlus.WindowsForms;
using ITSNet.Common.BIZ;
using ITSNet.Common.BIZ.RealtimeMessage;
using STIControl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
namespace i2TradePlus
{
	internal class ucSendNewOrder : UserControl, IRealtimeMessage
	{
		public delegate void OnAccountChangedHandler(string account);
		public delegate void OnBoxStyleChangedHandler();
		public delegate void OnResizedHandler();
		public delegate void OnNewStopOrderHandler(DataRow dr);
		public delegate void OnResizeUpDownHandler(bool isUp);
		private delegate void ShowMessageInFormConfirmCallBack(string message, frmOrderFormConfirm.OpenStyle openStyle);
		private delegate void ShowOrderFormConfirmCallBack(string message, string orderParam, string ossWarning, string stockTH);
		private delegate void ShowStopDisclaimerCallBack();
		private delegate bool SetColorBySideCallBack(string side);
		private delegate void SetSmartOneClickCallBack(string side, string stock, string price, long volume);
		private delegate void SetCurrentSymbolCallBack(string symbol);
		private delegate void SendBatchOrderCallBack(string side, string stock, string ttf, string volume, string price, string pubvol, string condition, string deposit);
		private delegate void ShowSplashCallBack(bool visible, string message, bool isAutoClose);
		private const string CM_VERIFY_ORDER = "V";
		private const string CM_SEND_ORDER = "S";
		private const string CM_SEND_AUTO_TRADE = "T";
		private IContainer components = null;
		private Label lbPrice;
		private Label lbVolume;
		private TextBox tbVolume;
		private Label lbPublic;
		private Label lbCondition;
		private TextBox tbPublic;
		private CheckBox chbNVDR;
		private Panel panelTop;
		private Label lbBuyLimit;
		private Label lbLoading;
		private Label tbBuyLimit;
		private Button btnSendOrder;
		private ComboBox cbAccount;
		private Label lbAccount;
		private ComboBox cbCondition;
		private Label tbOnHand;
		private Label lbOnHand;
		private Panel panelEquity;
		private TextBox tbTimes;
		private Label lbTimes;
		private Button btnStyle2;
		private Button btnStyle1;
		private Button btnSetting;
		private Button btnCleanPort;
		private ComboBox cbPrice;
		private RadioButton rbSell;
		private RadioButton rbBuy;
		private RadioButton rbCover;
		private RadioButton rbShort;
		private Button btnStyle3;
		private TextBox tbPin;
		private Label lbPin;
		private Button btnClear;
		private Button btnVolDec;
		private Button btnVolInc;
		private Button btnPBDec;
		private Button btnPBInc;
		private Button btnPriceDec;
		private Button btnPriceInc;
		private ToolTip toolTip1;
		private ComboBox cbSide;
		private Label lbSide;
		private Label lbStock;
		private ComboBox cbStock;
		private Button btnStyle4;
		private Button btnRisk;
		private Button btnShowStockAlert;
		private ComboBox cbDepCollateral;
		private Label lbDep;
		private Button btnNotification;
		private CheckBox chbEqStopOrder;
		private Panel panelStopOrder;
		private CheckBox chbLimit;
		private Label label2;
		private ComboBox cbStopOrderField;
		private Panel panelDerivative;
		private TextBox tbTfexPriceCondition;
		private ComboBox cbTfexConStopOrder;
		private TextBox tbPriceT;
		private TextBox tbSeriesCondition;
		private TextBox tbSeries;
		private CheckBox chbTfexStopOrder;
		private TextBox tbPublishT;
		private Label lbValidity;
		private TextBox tbVolumeT;
		private Label lbType;
		private Button btnClearTextT;
		private Label lbPosition;
		private Button btnSendOrderT;
		private Label lbPublish;
		private ComboBox cbValidity;
		private Label lbPriceT;
		private ComboBox cbType;
		private Label lbVolumeT;
		private Label lbSeries;
		private Label tbEquity;
		private Label lbEquity;
		private RadioButton rdbTfexSell;
		private RadioButton rdbTfexBuy;
		private ComboBox cbPosition;
		private Button btnBResize_Up;
		private Button btnBResize_Down;
		private ComboBox cbStopOrderPrice;
		private Label lbStopPriceLable;
		private string _showSide = string.Empty;
		private string _showSideTFEX = string.Empty;
		private BackgroundWorker bgwReloadCredit = null;
		private BackgroundWorker bgwSendOrder = null;
		private DataSet tdsCredit = null;
		private StockList.StockInformation _stockInfo = null;
		private SeriesList.SeriesInformation _seriesInfoTfex = null;
		private long _returnOrderNumberFromServer = 0L;
		private long _returnOrderNumberFromServer_TFEX = 0L;
		private System.Windows.Forms.Timer timerReloadCredit = null;
		private decimal _buyCreditLimit = 0m;
		private decimal _totalCreditLimit = 0m;
		private System.Windows.Forms.Timer timerSwitchAccount = null;
		private int _creditType = 0;
		private bool _isActive = false;
		private frmOrderFormConfirm _frmConfirm = null;
		private frmStopDisclaimer _frmStopDisclaimer = null;
		private object _objLastActive = null;
		private int _OrdTimes = 0;
		private int _currTimes = 0;
		private string _retOrderMessage = string.Empty;
		private string _commandType = string.Empty;
		private string _OrdSymbol = string.Empty;
		private string _OrdSide = string.Empty;
		private long _OrdVolume = 0L;
		private string _OrdPrice = string.Empty;
		private long _OrdPubVol = 0L;
		private string _OrdCondition = string.Empty;
		private int _OrdTtf;
		private string _OrdIsDeposit = string.Empty;
		private string _OrdPriceType = string.Empty;
		private string _OrdPosition = string.Empty;
		private string _OrdValidityDate = string.Empty;
		private string _OrdTfexStopPrice = string.Empty;
		private string _OrdTfexStopSeries = string.Empty;
		private string _OrdTfexStopCond = string.Empty;
		private int _stopField = 0;
		private int _stopOperator = 0;
		private decimal _stopPrice = 0m;
		private int _stopLimit = 0;
		private bool _isLockPubVol = false;
		private bool _verifyResult_Pin = false;
		private string _verifyResultStr_Pin = string.Empty;
		private bool _verifyResult = false;
		private bool _verifyParam = false;
		private DataSet _dsSendOrder = null;
		private DataSet _dsSendOrderTfex = null;
		private ApplicationInfo.SendNewOrderResult _newOrderResult = null;
		private System.Windows.Forms.Timer tmCloseSplash = null;
		private ucBSSetting frm = null;
		private frmPcPriceAlert _pcPriceAlertForm = null;
		private frmMobileAlert alertSettingForm = null;
		public  ucSendNewOrder.OnAccountChangedHandler _OnAccountChanged;
		public event ucSendNewOrder.OnAccountChangedHandler OnAccountChanged
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._OnAccountChanged += value;
            }
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._OnAccountChanged -= value;
            }
		}
		public  ucSendNewOrder.OnBoxStyleChangedHandler _OnBoxStyleChanged;
		public event ucSendNewOrder.OnBoxStyleChangedHandler OnBoxStyleChanged
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._OnBoxStyleChanged += value;
            }
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._OnBoxStyleChanged -= value;
            }
		}
		public  ucSendNewOrder.OnResizedHandler _OnResized;
		public event ucSendNewOrder.OnResizedHandler OnResized
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._OnResized += value;
            }
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._OnResized -= value;
            }
		}
		public  ucSendNewOrder.OnNewStopOrderHandler _OnNewStopOrder;
		public event ucSendNewOrder.OnNewStopOrderHandler OnNewStopOrder
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._OnNewStopOrder += value;
            }
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._OnNewStopOrder -= value;
            }
		}
        public ucSendNewOrder.OnResizeUpDownHandler _OnResizeUpDown;
		public event ucSendNewOrder.OnResizeUpDownHandler OnResizeUpDown
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._OnResizeUpDown += value;
            }
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._OnResizeUpDown -= value;
			}
		}
		public bool IsActive
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this._isActive;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this._isActive = value;
				if (!base.DesignMode)
				{
					try
					{
						if (value)
						{
							this._buyCreditLimit = 0m;
							this._totalCreditLimit = 0m;
							this.lbBuyLimit.Text = "Buy Limit :";
							this.tbBuyLimit.Text = "0";
							this.cbSide.Items.Clear();
							this.cbSide.Items.Add("Buy");
							this.cbSide.Items.Add("Sell");
							if (ApplicationInfo.SuuportSBL == "Y")
							{
								this.cbSide.Items.Add("Cover");
								this.cbSide.Items.Add("Short");
							}
							this.timerSwitchAccount.Stop();
							this.timerSwitchAccount.Start();
						}
					}
					catch (Exception ex)
					{
						this.ShowError("UcSendOrder:IsActive", ex);
					}
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
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ucSendNewOrder));
			this.lbPrice = new Label();
			this.lbVolume = new Label();
			this.tbVolume = new TextBox();
			this.lbPublic = new Label();
			this.lbCondition = new Label();
			this.tbPublic = new TextBox();
			this.chbNVDR = new CheckBox();
			this.panelTop = new Panel();
			this.btnBResize_Up = new Button();
			this.btnBResize_Down = new Button();
			this.tbEquity = new Label();
			this.lbEquity = new Label();
			this.btnNotification = new Button();
			this.btnShowStockAlert = new Button();
			this.btnRisk = new Button();
			this.btnStyle4 = new Button();
			this.btnStyle3 = new Button();
			this.btnCleanPort = new Button();
			this.btnSetting = new Button();
			this.btnStyle2 = new Button();
			this.btnStyle1 = new Button();
			this.tbOnHand = new Label();
			this.lbOnHand = new Label();
			this.lbAccount = new Label();
			this.cbAccount = new ComboBox();
			this.tbBuyLimit = new Label();
			this.lbBuyLimit = new Label();
			this.chbEqStopOrder = new CheckBox();
			this.lbLoading = new Label();
			this.btnSendOrder = new Button();
			this.cbCondition = new ComboBox();
			this.panelEquity = new Panel();
			this.panelStopOrder = new Panel();
			this.lbStopPriceLable = new Label();
			this.cbStopOrderPrice = new ComboBox();
			this.chbLimit = new CheckBox();
			this.label2 = new Label();
			this.cbStopOrderField = new ComboBox();
			this.cbDepCollateral = new ComboBox();
			this.lbDep = new Label();
			this.cbStock = new ComboBox();
			this.lbStock = new Label();
			this.lbSide = new Label();
			this.cbSide = new ComboBox();
			this.btnPriceDec = new Button();
			this.btnPriceInc = new Button();
			this.btnPBDec = new Button();
			this.btnPBInc = new Button();
			this.btnVolDec = new Button();
			this.btnVolInc = new Button();
			this.btnClear = new Button();
			this.lbPin = new Label();
			this.tbPin = new TextBox();
			this.rbCover = new RadioButton();
			this.rbShort = new RadioButton();
			this.rbSell = new RadioButton();
			this.rbBuy = new RadioButton();
			this.cbPrice = new ComboBox();
			this.tbTimes = new TextBox();
			this.lbTimes = new Label();
			this.toolTip1 = new ToolTip(this.components);
			this.panelDerivative = new Panel();
			this.cbPosition = new ComboBox();
			this.rdbTfexSell = new RadioButton();
			this.rdbTfexBuy = new RadioButton();
			this.tbTfexPriceCondition = new TextBox();
			this.cbTfexConStopOrder = new ComboBox();
			this.tbPriceT = new TextBox();
			this.tbSeriesCondition = new TextBox();
			this.tbSeries = new TextBox();
			this.chbTfexStopOrder = new CheckBox();
			this.tbPublishT = new TextBox();
			this.lbValidity = new Label();
			this.tbVolumeT = new TextBox();
			this.lbType = new Label();
			this.btnClearTextT = new Button();
			this.lbPosition = new Label();
			this.btnSendOrderT = new Button();
			this.lbPublish = new Label();
			this.cbValidity = new ComboBox();
			this.lbPriceT = new Label();
			this.cbType = new ComboBox();
			this.lbVolumeT = new Label();
			this.lbSeries = new Label();
			this.panelTop.SuspendLayout();
			this.panelEquity.SuspendLayout();
			this.panelStopOrder.SuspendLayout();
			this.panelDerivative.SuspendLayout();
			base.SuspendLayout();
			this.lbPrice.AutoSize = true;
			this.lbPrice.ForeColor = Color.LightGray;
			this.lbPrice.Location = new Point(418, 8);
			this.lbPrice.Margin = new Padding(2, 0, 2, 0);
			this.lbPrice.Name = "lbPrice";
			this.lbPrice.Size = new Size(31, 13);
			this.lbPrice.TabIndex = 13;
			this.lbPrice.Text = "Price";
			this.lbPrice.TextAlign = ContentAlignment.MiddleLeft;
			this.lbVolume.AutoSize = true;
			this.lbVolume.ForeColor = Color.LightGray;
			this.lbVolume.Location = new Point(253, 8);
			this.lbVolume.Margin = new Padding(2, 0, 2, 0);
			this.lbVolume.Name = "lbVolume";
			this.lbVolume.Size = new Size(22, 13);
			this.lbVolume.TabIndex = 11;
			this.lbVolume.Text = "Vol";
			this.lbVolume.TextAlign = ContentAlignment.MiddleLeft;
			this.tbVolume.AllowDrop = true;
			this.tbVolume.BackColor = Color.FromArgb(224, 224, 224);
			this.tbVolume.BorderStyle = BorderStyle.FixedSingle;
			this.tbVolume.Location = new Point(276, 6);
			this.tbVolume.Margin = new Padding(2, 3, 2, 3);
			this.tbVolume.MaxLength = 10;
			this.tbVolume.Name = "tbVolume";
			this.tbVolume.Size = new Size(59, 20);
			this.tbVolume.TabIndex = 2;
			this.tbVolume.TextChanged += new EventHandler(this.tbVolume_TextChanged);
			this.tbVolume.DragDrop += new DragEventHandler(this.tbVolume_DragDrop);
			this.tbVolume.KeyDown += new KeyEventHandler(this.tbVolume_KeyDown);
			this.tbVolume.Leave += new EventHandler(this.controlOrder_Leave);
			this.tbVolume.Enter += new EventHandler(this.controlOrder_Enter);
			this.tbVolume.DragEnter += new DragEventHandler(this.tbVolume_DragEnter);
			this.lbPublic.AutoSize = true;
			this.lbPublic.ForeColor = Color.LightGray;
			this.lbPublic.Location = new Point(525, 7);
			this.lbPublic.Margin = new Padding(2, 0, 2, 0);
			this.lbPublic.Name = "lbPublic";
			this.lbPublic.Size = new Size(44, 13);
			this.lbPublic.TabIndex = 67;
			this.lbPublic.Text = "P/B Vol";
			this.lbPublic.TextAlign = ContentAlignment.MiddleLeft;
			this.lbCondition.AutoSize = true;
			this.lbCondition.ForeColor = Color.LightGray;
			this.lbCondition.Location = new Point(630, 6);
			this.lbCondition.Margin = new Padding(2, 0, 2, 0);
			this.lbCondition.Name = "lbCondition";
			this.lbCondition.Size = new Size(32, 13);
			this.lbCondition.TabIndex = 68;
			this.lbCondition.Text = "Cond";
			this.lbCondition.TextAlign = ContentAlignment.MiddleLeft;
			this.tbPublic.AllowDrop = true;
			this.tbPublic.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			this.tbPublic.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.tbPublic.BackColor = Color.FromArgb(224, 224, 224);
			this.tbPublic.BorderStyle = BorderStyle.FixedSingle;
			this.tbPublic.CharacterCasing = CharacterCasing.Upper;
			this.tbPublic.Location = new Point(567, 4);
			this.tbPublic.Margin = new Padding(2, 3, 2, 3);
			this.tbPublic.MaxLength = 10;
			this.tbPublic.Name = "tbPublic";
			this.tbPublic.Size = new Size(59, 20);
			this.tbPublic.TabIndex = 4;
			this.tbPublic.TextChanged += new EventHandler(this.tbPublic_TextChanged);
			this.tbPublic.KeyDown += new KeyEventHandler(this.tbPublic_KeyDown);
			this.tbPublic.Leave += new EventHandler(this.controlOrder_Leave);
			this.tbPublic.Enter += new EventHandler(this.controlOrder_Enter);
			this.chbNVDR.AutoSize = true;
			this.chbNVDR.ForeColor = Color.LightGray;
			this.chbNVDR.Location = new Point(201, 8);
			this.chbNVDR.Margin = new Padding(2, 3, 0, 3);
			this.chbNVDR.Name = "chbNVDR";
			this.chbNVDR.Size = new Size(57, 17);
			this.chbNVDR.TabIndex = 1;
			this.chbNVDR.Text = "NVDR";
			this.chbNVDR.UseVisualStyleBackColor = false;
			this.chbNVDR.Leave += new EventHandler(this.controlOrder_Leave);
			this.chbNVDR.Enter += new EventHandler(this.controlOrder_Enter);
			this.chbNVDR.CheckedChanged += new EventHandler(this.cbNVDR_CheckedChanged);
			this.chbNVDR.KeyDown += new KeyEventHandler(this.cbNDVR_KeyDown);
			this.panelTop.BackColor = Color.FromArgb(30, 30, 30);
			this.panelTop.Controls.Add(this.btnBResize_Up);
			this.panelTop.Controls.Add(this.btnBResize_Down);
			this.panelTop.Controls.Add(this.tbEquity);
			this.panelTop.Controls.Add(this.lbEquity);
			this.panelTop.Controls.Add(this.btnNotification);
			this.panelTop.Controls.Add(this.btnShowStockAlert);
			this.panelTop.Controls.Add(this.btnRisk);
			this.panelTop.Controls.Add(this.btnStyle4);
			this.panelTop.Controls.Add(this.btnStyle3);
			this.panelTop.Controls.Add(this.btnCleanPort);
			this.panelTop.Controls.Add(this.btnSetting);
			this.panelTop.Controls.Add(this.btnStyle2);
			this.panelTop.Controls.Add(this.btnStyle1);
			this.panelTop.Controls.Add(this.tbOnHand);
			this.panelTop.Controls.Add(this.lbOnHand);
			this.panelTop.Controls.Add(this.lbAccount);
			this.panelTop.Controls.Add(this.cbAccount);
			this.panelTop.Controls.Add(this.tbBuyLimit);
			this.panelTop.Controls.Add(this.lbBuyLimit);
			this.panelTop.ForeColor = Color.Black;
			this.panelTop.Location = new Point(1, 1);
			this.panelTop.Margin = new Padding(0);
			this.panelTop.Name = "panelTop";
			this.panelTop.Size = new Size(868, 26);
			this.panelTop.TabIndex = 72;
			this.btnBResize_Up.FlatAppearance.BorderColor = Color.Gray;
			this.btnBResize_Up.FlatAppearance.BorderSize = 0;
			this.btnBResize_Up.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnBResize_Up.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
			this.btnBResize_Up.FlatStyle = FlatStyle.Flat;
			this.btnBResize_Up.ForeColor = Color.LightGray;
			this.btnBResize_Up.Image = Resources.Up1;
			this.btnBResize_Up.Location = new Point(830, 2);
			this.btnBResize_Up.Name = "btnBResize_Up";
			this.btnBResize_Up.Size = new Size(16, 20);
			this.btnBResize_Up.TabIndex = 110;
			this.btnBResize_Up.TabStop = false;
			this.toolTip1.SetToolTip(this.btnBResize_Up, "Enlarge the box.");
			this.btnBResize_Up.UseVisualStyleBackColor = true;
			this.btnBResize_Up.Visible = false;
			this.btnBResize_Up.Click += new EventHandler(this.btnBResize_Up_Click);
			this.btnBResize_Down.FlatAppearance.BorderColor = Color.Gray;
			this.btnBResize_Down.FlatAppearance.BorderSize = 0;
			this.btnBResize_Down.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnBResize_Down.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
			this.btnBResize_Down.FlatStyle = FlatStyle.Flat;
			this.btnBResize_Down.ForeColor = Color.LightGray;
			this.btnBResize_Down.Image = Resources.Down;
			this.btnBResize_Down.Location = new Point(848, 2);
			this.btnBResize_Down.Name = "btnBResize_Down";
			this.btnBResize_Down.Size = new Size(16, 20);
			this.btnBResize_Down.TabIndex = 109;
			this.btnBResize_Down.TabStop = false;
			this.toolTip1.SetToolTip(this.btnBResize_Down, "Shrink the box.");
			this.btnBResize_Down.UseVisualStyleBackColor = true;
			this.btnBResize_Down.Visible = false;
			this.btnBResize_Down.Click += new EventHandler(this.btnBResize_Down_Click);
			this.tbEquity.AutoSize = true;
			this.tbEquity.ForeColor = Color.Yellow;
			this.tbEquity.Location = new Point(460, 5);
			this.tbEquity.Margin = new Padding(2, 0, 2, 0);
			this.tbEquity.Name = "tbEquity";
			this.tbEquity.Size = new Size(13, 13);
			this.tbEquity.TabIndex = 108;
			this.tbEquity.Text = "0";
			this.tbEquity.TextAlign = ContentAlignment.MiddleLeft;
			this.lbEquity.AutoSize = true;
			this.lbEquity.ForeColor = Color.LightGray;
			this.lbEquity.Location = new Point(400, 5);
			this.lbEquity.Margin = new Padding(2, 0, 2, 0);
			this.lbEquity.Name = "lbEquity";
			this.lbEquity.Size = new Size(42, 13);
			this.lbEquity.TabIndex = 107;
			this.lbEquity.Text = "Equity :";
			this.lbEquity.TextAlign = ContentAlignment.MiddleLeft;
			this.btnNotification.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.btnNotification.BackColor = Color.Transparent;
			this.btnNotification.FlatAppearance.BorderColor = Color.LightGray;
			this.btnNotification.FlatAppearance.BorderSize = 0;
			this.btnNotification.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnNotification.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
			this.btnNotification.FlatStyle = FlatStyle.Flat;
			this.btnNotification.ForeColor = Color.LightGray;
			this.btnNotification.Image = (Image)componentResourceManager.GetObject("btnNotification.Image");
			this.btnNotification.Location = new Point(607, 1);
			this.btnNotification.Name = "btnNotification";
			this.btnNotification.Size = new Size(24, 23);
			this.btnNotification.TabIndex = 95;
			this.btnNotification.TabStop = false;
			this.btnNotification.Tag = "5";
			this.toolTip1.SetToolTip(this.btnNotification, "Mobile Notification");
			this.btnNotification.UseVisualStyleBackColor = false;
			this.btnNotification.Click += new EventHandler(this.btnNotification_Click);
			this.btnShowStockAlert.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.btnShowStockAlert.BackColor = Color.Transparent;
			this.btnShowStockAlert.FlatAppearance.BorderColor = Color.LightGray;
			this.btnShowStockAlert.FlatAppearance.BorderSize = 0;
			this.btnShowStockAlert.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnShowStockAlert.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
			this.btnShowStockAlert.FlatStyle = FlatStyle.Flat;
			this.btnShowStockAlert.ForeColor = Color.LightGray;
			this.btnShowStockAlert.Image = (Image)componentResourceManager.GetObject("btnShowStockAlert.Image");
			this.btnShowStockAlert.Location = new Point(633, 1);
			this.btnShowStockAlert.Name = "btnShowStockAlert";
			this.btnShowStockAlert.Size = new Size(24, 23);
			this.btnShowStockAlert.TabIndex = 94;
			this.btnShowStockAlert.TabStop = false;
			this.btnShowStockAlert.Tag = "5";
			this.toolTip1.SetToolTip(this.btnShowStockAlert, "Price Alert on PC");
			this.btnShowStockAlert.UseVisualStyleBackColor = false;
			this.btnShowStockAlert.Click += new EventHandler(this.btnShowStockAlert_Click);
			this.btnRisk.FlatAppearance.BorderColor = Color.LightGray;
			this.btnRisk.FlatAppearance.BorderSize = 0;
			this.btnRisk.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnRisk.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
			this.btnRisk.FlatStyle = FlatStyle.Flat;
			this.btnRisk.ForeColor = Color.LightGray;
			this.btnRisk.Image = (Image)componentResourceManager.GetObject("btnRisk.Image");
			this.btnRisk.Location = new Point(562, 2);
			this.btnRisk.Name = "btnRisk";
			this.btnRisk.Size = new Size(37, 20);
			this.btnRisk.TabIndex = 93;
			this.btnRisk.TabStop = false;
			this.toolTip1.SetToolTip(this.btnRisk, "Risk Control / เครื่องมือควบคุมความเสี่ยง");
			this.btnRisk.UseVisualStyleBackColor = true;
			this.btnRisk.Visible = false;
			this.btnRisk.Click += new EventHandler(this.btnPolicy_Click);
			this.btnStyle4.FlatAppearance.BorderColor = Color.Gray;
			this.btnStyle4.FlatAppearance.BorderSize = 0;
			this.btnStyle4.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnStyle4.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
			this.btnStyle4.FlatStyle = FlatStyle.Flat;
			this.btnStyle4.ForeColor = Color.LightGray;
			this.btnStyle4.Location = new Point(761, 2);
			this.btnStyle4.Name = "btnStyle4";
			this.btnStyle4.Size = new Size(18, 20);
			this.btnStyle4.TabIndex = 92;
			this.btnStyle4.TabStop = false;
			this.btnStyle4.Text = "4";
			this.toolTip1.SetToolTip(this.btnStyle4, "Trade Style 4");
			this.btnStyle4.UseVisualStyleBackColor = true;
			this.btnStyle4.Click += new EventHandler(this.btnStyle_Click);
			this.btnStyle3.FlatAppearance.BorderColor = Color.Gray;
			this.btnStyle3.FlatAppearance.BorderSize = 0;
			this.btnStyle3.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnStyle3.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
			this.btnStyle3.FlatStyle = FlatStyle.Flat;
			this.btnStyle3.ForeColor = Color.LightGray;
			this.btnStyle3.Location = new Point(742, 2);
			this.btnStyle3.Name = "btnStyle3";
			this.btnStyle3.Size = new Size(18, 20);
			this.btnStyle3.TabIndex = 90;
			this.btnStyle3.TabStop = false;
			this.btnStyle3.Text = "3";
			this.toolTip1.SetToolTip(this.btnStyle3, "Trade Style 3");
			this.btnStyle3.UseVisualStyleBackColor = true;
			this.btnStyle3.Click += new EventHandler(this.btnStyle_Click);
			this.btnCleanPort.FlatAppearance.BorderColor = Color.Gray;
			this.btnCleanPort.FlatAppearance.BorderSize = 0;
			this.btnCleanPort.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnCleanPort.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
			this.btnCleanPort.FlatStyle = FlatStyle.Flat;
			this.btnCleanPort.ForeColor = Color.LightGray;
			this.btnCleanPort.Image = (Image)componentResourceManager.GetObject("btnCleanPort.Image");
			this.btnCleanPort.Location = new Point(663, 2);
			this.btnCleanPort.Name = "btnCleanPort";
			this.btnCleanPort.Size = new Size(20, 20);
			this.btnCleanPort.TabIndex = 89;
			this.btnCleanPort.TabStop = false;
			this.toolTip1.SetToolTip(this.btnCleanPort, "Portfolio clearing tool");
			this.btnCleanPort.UseVisualStyleBackColor = true;
			this.btnCleanPort.Click += new EventHandler(this.btnCleanPort_Click);
			this.btnSetting.FlatAppearance.BorderColor = Color.Gray;
			this.btnSetting.FlatAppearance.BorderSize = 0;
			this.btnSetting.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnSetting.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
			this.btnSetting.FlatStyle = FlatStyle.Flat;
			this.btnSetting.ForeColor = Color.LightGray;
			this.btnSetting.Image = (Image)componentResourceManager.GetObject("btnSetting.Image");
			this.btnSetting.Location = new Point(787, 2);
			this.btnSetting.Name = "btnSetting";
			this.btnSetting.Size = new Size(20, 20);
			this.btnSetting.TabIndex = 88;
			this.btnSetting.TabStop = false;
			this.toolTip1.SetToolTip(this.btnSetting, "Buy/Sell Options");
			this.btnSetting.UseVisualStyleBackColor = true;
			this.btnSetting.Click += new EventHandler(this.btnSetting_Click);
			this.btnStyle2.FlatAppearance.BorderColor = Color.Gray;
			this.btnStyle2.FlatAppearance.BorderSize = 0;
			this.btnStyle2.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnStyle2.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
			this.btnStyle2.FlatStyle = FlatStyle.Flat;
			this.btnStyle2.ForeColor = Color.LightGray;
			this.btnStyle2.Location = new Point(718, 2);
			this.btnStyle2.Name = "btnStyle2";
			this.btnStyle2.Size = new Size(18, 20);
			this.btnStyle2.TabIndex = 87;
			this.btnStyle2.TabStop = false;
			this.btnStyle2.Text = "2";
			this.toolTip1.SetToolTip(this.btnStyle2, "Quick Trade Style");
			this.btnStyle2.UseVisualStyleBackColor = true;
			this.btnStyle2.Click += new EventHandler(this.btnStyle_Click);
			this.btnStyle1.FlatAppearance.BorderColor = Color.Gray;
			this.btnStyle1.FlatAppearance.BorderSize = 0;
			this.btnStyle1.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnStyle1.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
			this.btnStyle1.FlatStyle = FlatStyle.Flat;
			this.btnStyle1.ForeColor = Color.LightGray;
			this.btnStyle1.Location = new Point(692, 2);
			this.btnStyle1.Name = "btnStyle1";
			this.btnStyle1.Size = new Size(18, 20);
			this.btnStyle1.TabIndex = 86;
			this.btnStyle1.TabStop = false;
			this.btnStyle1.Text = "1";
			this.toolTip1.SetToolTip(this.btnStyle1, "i2 Trade Style");
			this.btnStyle1.UseVisualStyleBackColor = true;
			this.btnStyle1.Click += new EventHandler(this.btnStyle_Click);
			this.tbOnHand.AutoSize = true;
			this.tbOnHand.BackColor = Color.Transparent;
			this.tbOnHand.ForeColor = Color.Yellow;
			this.tbOnHand.Location = new Point(345, 5);
			this.tbOnHand.Margin = new Padding(2, 0, 2, 0);
			this.tbOnHand.MinimumSize = new Size(60, 0);
			this.tbOnHand.Name = "tbOnHand";
			this.tbOnHand.Size = new Size(60, 13);
			this.tbOnHand.TabIndex = 81;
			this.tbOnHand.Text = "0";
			this.tbOnHand.TextAlign = ContentAlignment.MiddleLeft;
			this.lbOnHand.AutoSize = true;
			this.lbOnHand.BackColor = Color.Transparent;
			this.lbOnHand.ForeColor = Color.LightGray;
			this.lbOnHand.Location = new Point(285, 6);
			this.lbOnHand.Margin = new Padding(2, 0, 2, 0);
			this.lbOnHand.Name = "lbOnHand";
			this.lbOnHand.Size = new Size(56, 13);
			this.lbOnHand.TabIndex = 80;
			this.lbOnHand.Text = "OnHand : ";
			this.lbOnHand.TextAlign = ContentAlignment.MiddleLeft;
			this.lbAccount.AutoSize = true;
			this.lbAccount.BackColor = Color.Transparent;
			this.lbAccount.ForeColor = Color.LightGray;
			this.lbAccount.Location = new Point(3, 5);
			this.lbAccount.Margin = new Padding(2, 0, 1, 0);
			this.lbAccount.Name = "lbAccount";
			this.lbAccount.Size = new Size(53, 13);
			this.lbAccount.TabIndex = 79;
			this.lbAccount.Text = "Account :";
			this.lbAccount.TextAlign = ContentAlignment.MiddleLeft;
			this.cbAccount.BackColor = Color.FromArgb(30, 30, 30);
			this.cbAccount.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbAccount.FlatStyle = FlatStyle.Popup;
			this.cbAccount.ForeColor = Color.Yellow;
			this.cbAccount.FormattingEnabled = true;
			this.cbAccount.Location = new Point(59, 2);
			this.cbAccount.Margin = new Padding(0, 3, 0, 3);
			this.cbAccount.Name = "cbAccount";
			this.cbAccount.Size = new Size(130, 21);
			this.cbAccount.TabIndex = 78;
			this.cbAccount.TabStop = false;
			this.cbAccount.SelectedIndexChanged += new EventHandler(this.cbAccount_SelectedIndexChanged);
			this.tbBuyLimit.AutoSize = true;
			this.tbBuyLimit.BackColor = Color.Transparent;
			this.tbBuyLimit.ForeColor = Color.Yellow;
			this.tbBuyLimit.Location = new Point(254, 5);
			this.tbBuyLimit.Margin = new Padding(2, 0, 2, 0);
			this.tbBuyLimit.MinimumSize = new Size(60, 0);
			this.tbBuyLimit.Name = "tbBuyLimit";
			this.tbBuyLimit.Size = new Size(60, 13);
			this.tbBuyLimit.TabIndex = 76;
			this.tbBuyLimit.Text = "0";
			this.tbBuyLimit.TextAlign = ContentAlignment.MiddleLeft;
			this.lbBuyLimit.AutoSize = true;
			this.lbBuyLimit.BackColor = Color.Transparent;
			this.lbBuyLimit.ForeColor = Color.LightGray;
			this.lbBuyLimit.Location = new Point(195, 5);
			this.lbBuyLimit.Margin = new Padding(2, 0, 2, 0);
			this.lbBuyLimit.Name = "lbBuyLimit";
			this.lbBuyLimit.Size = new Size(55, 13);
			this.lbBuyLimit.TabIndex = 72;
			this.lbBuyLimit.Text = "Buy Limit :";
			this.lbBuyLimit.TextAlign = ContentAlignment.MiddleLeft;
			this.chbEqStopOrder.AutoSize = true;
			this.chbEqStopOrder.ForeColor = Color.FromArgb(255, 192, 128);
			this.chbEqStopOrder.Location = new Point(528, 61);
			this.chbEqStopOrder.Margin = new Padding(2, 3, 0, 3);
			this.chbEqStopOrder.Name = "chbEqStopOrder";
			this.chbEqStopOrder.Size = new Size(79, 17);
			this.chbEqStopOrder.TabIndex = 106;
			this.chbEqStopOrder.Text = "Auto Trade";
			this.chbEqStopOrder.UseVisualStyleBackColor = false;
			this.chbEqStopOrder.CheckedChanged += new EventHandler(this.chbStopOrder_CheckedChanged);
			this.lbLoading.AutoSize = true;
			this.lbLoading.BackColor = Color.FromArgb(64, 64, 64);
			this.lbLoading.BorderStyle = BorderStyle.FixedSingle;
			this.lbLoading.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.lbLoading.ForeColor = Color.Yellow;
			this.lbLoading.Location = new Point(362, 95);
			this.lbLoading.Name = "lbLoading";
			this.lbLoading.Padding = new Padding(4, 3, 4, 3);
			this.lbLoading.Size = new Size(137, 23);
			this.lbLoading.TabIndex = 73;
			this.lbLoading.Text = "Sending New Order ...";
			this.lbLoading.TextAlign = ContentAlignment.MiddleCenter;
			this.lbLoading.Visible = false;
			this.btnSendOrder.AutoEllipsis = true;
			this.btnSendOrder.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.btnSendOrder.BackColor = Color.Transparent;
			this.btnSendOrder.Cursor = Cursors.Hand;
			this.btnSendOrder.FlatAppearance.BorderColor = Color.LightGray;
			this.btnSendOrder.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnSendOrder.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 192, 192);
			this.btnSendOrder.FlatStyle = FlatStyle.Flat;
			this.btnSendOrder.ForeColor = Color.WhiteSmoke;
			this.btnSendOrder.Location = new Point(803, 2);
			this.btnSendOrder.MaximumSize = new Size(58, 23);
			this.btnSendOrder.Name = "btnSendOrder";
			this.btnSendOrder.Size = new Size(54, 22);
			this.btnSendOrder.TabIndex = 8;
			this.btnSendOrder.TabStop = false;
			this.btnSendOrder.Text = "Send";
			this.btnSendOrder.UseVisualStyleBackColor = false;
			this.btnSendOrder.Click += new EventHandler(this.btnSendOrder_Click);
			this.cbCondition.AutoCompleteCustomSource.AddRange(new string[]
			{
				"",
				"IOC",
				"FOK"
			});
			this.cbCondition.AutoCompleteMode = AutoCompleteMode.Append;
			this.cbCondition.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.cbCondition.BackColor = Color.FromArgb(224, 224, 224);
			this.cbCondition.FlatStyle = FlatStyle.Popup;
			this.cbCondition.ForeColor = Color.Black;
			this.cbCondition.FormattingEnabled = true;
			this.cbCondition.Items.AddRange(new object[]
			{
				"",
				"IOC",
				"FOK"
			});
			this.cbCondition.Location = new Point(663, 3);
			this.cbCondition.Name = "cbCondition";
			this.cbCondition.Size = new Size(40, 21);
			this.cbCondition.TabIndex = 5;
			this.cbCondition.Leave += new EventHandler(this.controlOrder_Leave);
			this.cbCondition.Enter += new EventHandler(this.controlOrder_Enter);
			this.cbCondition.KeyPress += new KeyPressEventHandler(this.cbCondition_KeyPress);
			this.cbCondition.KeyDown += new KeyEventHandler(this.cbCondition_KeyDown);
			this.panelEquity.BackColor = Color.FromArgb(45, 45, 45);
			this.panelEquity.Controls.Add(this.panelStopOrder);
			this.panelEquity.Controls.Add(this.cbDepCollateral);
			this.panelEquity.Controls.Add(this.chbEqStopOrder);
			this.panelEquity.Controls.Add(this.lbDep);
			this.panelEquity.Controls.Add(this.cbStock);
			this.panelEquity.Controls.Add(this.lbStock);
			this.panelEquity.Controls.Add(this.lbSide);
			this.panelEquity.Controls.Add(this.cbSide);
			this.panelEquity.Controls.Add(this.btnPriceDec);
			this.panelEquity.Controls.Add(this.btnPriceInc);
			this.panelEquity.Controls.Add(this.btnPBDec);
			this.panelEquity.Controls.Add(this.btnPBInc);
			this.panelEquity.Controls.Add(this.btnVolDec);
			this.panelEquity.Controls.Add(this.btnVolInc);
			this.panelEquity.Controls.Add(this.btnClear);
			this.panelEquity.Controls.Add(this.lbPin);
			this.panelEquity.Controls.Add(this.tbPin);
			this.panelEquity.Controls.Add(this.rbCover);
			this.panelEquity.Controls.Add(this.rbShort);
			this.panelEquity.Controls.Add(this.rbSell);
			this.panelEquity.Controls.Add(this.rbBuy);
			this.panelEquity.Controls.Add(this.cbPrice);
			this.panelEquity.Controls.Add(this.tbTimes);
			this.panelEquity.Controls.Add(this.lbTimes);
			this.panelEquity.Controls.Add(this.cbCondition);
			this.panelEquity.Controls.Add(this.lbPrice);
			this.panelEquity.Controls.Add(this.lbVolume);
			this.panelEquity.Controls.Add(this.btnSendOrder);
			this.panelEquity.Controls.Add(this.tbPublic);
			this.panelEquity.Controls.Add(this.chbNVDR);
			this.panelEquity.Controls.Add(this.lbCondition);
			this.panelEquity.Controls.Add(this.tbVolume);
			this.panelEquity.Controls.Add(this.lbPublic);
			this.panelEquity.Location = new Point(3, 30);
			this.panelEquity.Name = "panelEquity";
			this.panelEquity.Size = new Size(866, 88);
			this.panelEquity.TabIndex = 79;
			this.panelStopOrder.BackColor = Color.FromArgb(60, 60, 60);
			this.panelStopOrder.Controls.Add(this.lbStopPriceLable);
			this.panelStopOrder.Controls.Add(this.cbStopOrderPrice);
			this.panelStopOrder.Controls.Add(this.chbLimit);
			this.panelStopOrder.Controls.Add(this.label2);
			this.panelStopOrder.Controls.Add(this.cbStopOrderField);
			this.panelStopOrder.Location = new Point(8, 55);
			this.panelStopOrder.Name = "panelStopOrder";
			this.panelStopOrder.Size = new Size(469, 27);
			this.panelStopOrder.TabIndex = 110;
			this.lbStopPriceLable.AutoSize = true;
			this.lbStopPriceLable.ForeColor = Color.WhiteSmoke;
			this.lbStopPriceLable.Location = new Point(247, 7);
			this.lbStopPriceLable.Margin = new Padding(2, 0, 2, 0);
			this.lbStopPriceLable.Name = "lbStopPriceLable";
			this.lbStopPriceLable.Size = new Size(31, 13);
			this.lbStopPriceLable.TabIndex = 115;
			this.lbStopPriceLable.Text = "Price";
			this.lbStopPriceLable.TextAlign = ContentAlignment.MiddleLeft;
			this.cbStopOrderPrice.AllowDrop = true;
			this.cbStopOrderPrice.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.cbStopOrderPrice.BackColor = Color.FromArgb(224, 224, 224);
			this.cbStopOrderPrice.FlatStyle = FlatStyle.Popup;
			this.cbStopOrderPrice.ForeColor = Color.Black;
			this.cbStopOrderPrice.FormattingEnabled = true;
			this.cbStopOrderPrice.Location = new Point(289, 3);
			this.cbStopOrderPrice.Name = "cbStopOrderPrice";
			this.cbStopOrderPrice.Size = new Size(64, 21);
			this.cbStopOrderPrice.TabIndex = 114;
			this.chbLimit.AutoSize = true;
			this.chbLimit.ForeColor = Color.WhiteSmoke;
			this.chbLimit.Location = new Point(361, 7);
			this.chbLimit.Margin = new Padding(2, 3, 0, 3);
			this.chbLimit.Name = "chbLimit";
			this.chbLimit.Size = new Size(103, 17);
			this.chbLimit.TabIndex = 111;
			this.chbLimit.Text = "Cancel End Day";
			this.chbLimit.UseVisualStyleBackColor = false;
			this.label2.AutoSize = true;
			this.label2.ForeColor = Color.WhiteSmoke;
			this.label2.Location = new Point(4, 7);
			this.label2.Margin = new Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new Size(85, 13);
			this.label2.TabIndex = 109;
			this.label2.Text = "Order Conditions";
			this.label2.TextAlign = ContentAlignment.MiddleLeft;
			this.cbStopOrderField.AllowDrop = true;
			this.cbStopOrderField.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.cbStopOrderField.BackColor = Color.FromArgb(224, 224, 224);
			this.cbStopOrderField.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbStopOrderField.FlatStyle = FlatStyle.Popup;
			this.cbStopOrderField.ForeColor = Color.Black;
			this.cbStopOrderField.FormattingEnabled = true;
			this.cbStopOrderField.Items.AddRange(new object[]
			{
				"Last >=",
				"Last <=",
				"Last >= SMA(Day)",
				"Last <= SMA(Day)",
				"Last > Break High (Day)",
				"Last < Break High (Day)",
				"Last > Break Low (Day)",
				"Last < Break Low (Day)"
			});
			this.cbStopOrderField.Location = new Point(96, 3);
			this.cbStopOrderField.Name = "cbStopOrderField";
			this.cbStopOrderField.Size = new Size(143, 21);
			this.cbStopOrderField.TabIndex = 106;
			this.cbStopOrderField.SelectedIndexChanged += new EventHandler(this.cbStopOrderField_SelectedIndexChanged);
			this.cbStopOrderField.Leave += new EventHandler(this.controlOrder_Leave);
			this.cbStopOrderField.Enter += new EventHandler(this.controlOrder_Enter);
			this.cbDepCollateral.AutoCompleteCustomSource.AddRange(new string[]
			{
				"",
				"IOC",
				"FOK"
			});
			this.cbDepCollateral.AutoCompleteMode = AutoCompleteMode.Append;
			this.cbDepCollateral.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.cbDepCollateral.BackColor = Color.FromArgb(224, 224, 224);
			this.cbDepCollateral.FlatStyle = FlatStyle.Popup;
			this.cbDepCollateral.ForeColor = Color.Black;
			this.cbDepCollateral.FormattingEnabled = true;
			this.cbDepCollateral.Location = new Point(740, 3);
			this.cbDepCollateral.Name = "cbDepCollateral";
			this.cbDepCollateral.Size = new Size(57, 21);
			this.cbDepCollateral.TabIndex = 102;
			this.cbDepCollateral.SelectedIndexChanged += new EventHandler(this.cbDepCollateral_SelectedIndexChanged);
			this.cbDepCollateral.Leave += new EventHandler(this.controlOrder_Leave);
			this.cbDepCollateral.Enter += new EventHandler(this.controlOrder_Enter);
			this.cbDepCollateral.KeyPress += new KeyPressEventHandler(this.cbCondition_KeyPress);
			this.cbDepCollateral.KeyDown += new KeyEventHandler(this.cbDepCollateral_KeyDown);
			this.lbDep.AutoSize = true;
			this.lbDep.ForeColor = Color.LightGray;
			this.lbDep.Location = new Point(707, 6);
			this.lbDep.Margin = new Padding(2, 0, 2, 0);
			this.lbDep.Name = "lbDep";
			this.lbDep.Size = new Size(27, 13);
			this.lbDep.TabIndex = 101;
			this.lbDep.Text = "Dep";
			this.lbDep.TextAlign = ContentAlignment.MiddleLeft;
			this.cbStock.AllowDrop = true;
			this.cbStock.AutoCompleteMode = AutoCompleteMode.Suggest;
			this.cbStock.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.cbStock.BackColor = Color.FromArgb(224, 224, 224);
			this.cbStock.FlatStyle = FlatStyle.Popup;
			this.cbStock.ForeColor = Color.Black;
			this.cbStock.FormattingEnabled = true;
			this.cbStock.Location = new Point(128, 4);
			this.cbStock.MaxLength = 20;
			this.cbStock.Name = "cbStock";
			this.cbStock.Size = new Size(77, 21);
			this.cbStock.TabIndex = 0;
			this.cbStock.Leave += new EventHandler(this.controlOrder_Leave);
			this.cbStock.Enter += new EventHandler(this.controlOrder_Enter);
			this.cbStock.DragDrop += new DragEventHandler(this.cbStock_DragDrop);
			this.cbStock.DragEnter += new DragEventHandler(this.cbStock_DragEnter);
			this.cbStock.KeyPress += new KeyPressEventHandler(this.cbStock_KeyPress);
			this.cbStock.KeyDown += new KeyEventHandler(this.cbStock_KeyDown);
			this.lbStock.AutoSize = true;
			this.lbStock.ForeColor = Color.LightGray;
			this.lbStock.Location = new Point(94, 9);
			this.lbStock.Margin = new Padding(2, 0, 2, 0);
			this.lbStock.Name = "lbStock";
			this.lbStock.Size = new Size(35, 13);
			this.lbStock.TabIndex = 100;
			this.lbStock.Text = "Stock";
			this.lbStock.TextAlign = ContentAlignment.MiddleLeft;
			this.lbStock.Visible = false;
			this.lbSide.AutoSize = true;
			this.lbSide.ForeColor = Color.LightGray;
			this.lbSide.Location = new Point(133, 37);
			this.lbSide.Margin = new Padding(2, 0, 2, 0);
			this.lbSide.Name = "lbSide";
			this.lbSide.Size = new Size(28, 13);
			this.lbSide.TabIndex = 99;
			this.lbSide.Text = "Side";
			this.lbSide.TextAlign = ContentAlignment.MiddleLeft;
			this.lbSide.Visible = false;
			this.cbSide.AutoCompleteCustomSource.AddRange(new string[]
			{
				"Buy",
				"Sell",
				"Cover",
				"Short"
			});
			this.cbSide.AutoCompleteMode = AutoCompleteMode.Append;
			this.cbSide.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.cbSide.BackColor = Color.FromArgb(224, 224, 224);
			this.cbSide.FlatStyle = FlatStyle.Popup;
			this.cbSide.ForeColor = Color.Black;
			this.cbSide.FormattingEnabled = true;
			this.cbSide.Items.AddRange(new object[]
			{
				"Buy",
				"Sell",
				"Cover",
				"Short"
			});
			this.cbSide.Location = new Point(164, 33);
			this.cbSide.Name = "cbSide";
			this.cbSide.Size = new Size(51, 21);
			this.cbSide.TabIndex = 98;
			this.cbSide.SelectedIndexChanged += new EventHandler(this.cbSide_SelectedIndexChanged);
			this.cbSide.Leave += new EventHandler(this.controlOrder_Leave);
			this.cbSide.Enter += new EventHandler(this.controlOrder_Enter);
			this.cbSide.KeyPress += new KeyPressEventHandler(this.cbSide_KeyPress);
			this.cbSide.KeyDown += new KeyEventHandler(this.cbSide_KeyDown);
			this.btnPriceDec.FlatAppearance.BorderSize = 0;
			this.btnPriceDec.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnPriceDec.FlatAppearance.MouseOverBackColor = Color.Teal;
			this.btnPriceDec.FlatStyle = FlatStyle.Flat;
			this.btnPriceDec.Image = (Image)componentResourceManager.GetObject("btnPriceDec.Image");
			this.btnPriceDec.Location = new Point(510, 34);
			this.btnPriceDec.Name = "btnPriceDec";
			this.btnPriceDec.Size = new Size(15, 15);
			this.btnPriceDec.TabIndex = 97;
			this.btnPriceDec.UseVisualStyleBackColor = true;
			this.btnPriceDec.Click += new EventHandler(this.btnPriceDec_Click);
			this.btnPriceInc.FlatAppearance.BorderSize = 0;
			this.btnPriceInc.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnPriceInc.FlatAppearance.MouseOverBackColor = Color.Teal;
			this.btnPriceInc.FlatStyle = FlatStyle.Flat;
			this.btnPriceInc.Image = (Image)componentResourceManager.GetObject("btnPriceInc.Image");
			this.btnPriceInc.Location = new Point(537, 33);
			this.btnPriceInc.Name = "btnPriceInc";
			this.btnPriceInc.Size = new Size(15, 15);
			this.btnPriceInc.TabIndex = 96;
			this.btnPriceInc.UseVisualStyleBackColor = true;
			this.btnPriceInc.Click += new EventHandler(this.btnPriceInc_Click);
			this.btnPBDec.FlatAppearance.BorderSize = 0;
			this.btnPBDec.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnPBDec.FlatAppearance.MouseOverBackColor = Color.Teal;
			this.btnPBDec.FlatStyle = FlatStyle.Flat;
			this.btnPBDec.Image = (Image)componentResourceManager.GetObject("btnPBDec.Image");
			this.btnPBDec.Location = new Point(443, 33);
			this.btnPBDec.Name = "btnPBDec";
			this.btnPBDec.Size = new Size(15, 15);
			this.btnPBDec.TabIndex = 95;
			this.btnPBDec.UseVisualStyleBackColor = true;
			this.btnPBDec.Click += new EventHandler(this.btnPBDec_Click);
			this.btnPBInc.FlatAppearance.BorderSize = 0;
			this.btnPBInc.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnPBInc.FlatAppearance.MouseOverBackColor = Color.Teal;
			this.btnPBInc.FlatStyle = FlatStyle.Flat;
			this.btnPBInc.Image = (Image)componentResourceManager.GetObject("btnPBInc.Image");
			this.btnPBInc.Location = new Point(470, 32);
			this.btnPBInc.Name = "btnPBInc";
			this.btnPBInc.Size = new Size(15, 15);
			this.btnPBInc.TabIndex = 94;
			this.btnPBInc.UseVisualStyleBackColor = true;
			this.btnPBInc.Click += new EventHandler(this.btnPBInc_Click);
			this.btnVolDec.FlatAppearance.BorderSize = 0;
			this.btnVolDec.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnVolDec.FlatAppearance.MouseOverBackColor = Color.Teal;
			this.btnVolDec.FlatStyle = FlatStyle.Flat;
			this.btnVolDec.Image = (Image)componentResourceManager.GetObject("btnVolDec.Image");
			this.btnVolDec.Location = new Point(394, 28);
			this.btnVolDec.Name = "btnVolDec";
			this.btnVolDec.Size = new Size(15, 15);
			this.btnVolDec.TabIndex = 93;
			this.btnVolDec.UseVisualStyleBackColor = true;
			this.btnVolDec.Click += new EventHandler(this.btnVolDec_Click);
			this.btnVolInc.FlatAppearance.BorderSize = 0;
			this.btnVolInc.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnVolInc.FlatAppearance.MouseOverBackColor = Color.Teal;
			this.btnVolInc.FlatStyle = FlatStyle.Flat;
			this.btnVolInc.Image = (Image)componentResourceManager.GetObject("btnVolInc.Image");
			this.btnVolInc.Location = new Point(421, 27);
			this.btnVolInc.Name = "btnVolInc";
			this.btnVolInc.Size = new Size(15, 15);
			this.btnVolInc.TabIndex = 92;
			this.btnVolInc.UseVisualStyleBackColor = true;
			this.btnVolInc.Click += new EventHandler(this.btnVolInc_Click);
			this.btnClear.AutoEllipsis = true;
			this.btnClear.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.btnClear.BackColor = Color.Transparent;
			this.btnClear.Cursor = Cursors.Hand;
			this.btnClear.FlatAppearance.BorderColor = Color.LightGray;
			this.btnClear.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnClear.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 192, 192);
			this.btnClear.FlatStyle = FlatStyle.Flat;
			this.btnClear.ForeColor = Color.WhiteSmoke;
			this.btnClear.Location = new Point(802, 28);
			this.btnClear.MaximumSize = new Size(58, 23);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new Size(54, 22);
			this.btnClear.TabIndex = 9;
			this.btnClear.TabStop = false;
			this.btnClear.Text = "Clear";
			this.btnClear.UseVisualStyleBackColor = false;
			this.btnClear.Visible = false;
			this.btnClear.Click += new EventHandler(this.btnClear_Click);
			this.lbPin.AutoSize = true;
			this.lbPin.ForeColor = Color.LightGray;
			this.lbPin.Location = new Point(635, 37);
			this.lbPin.Margin = new Padding(2, 0, 2, 0);
			this.lbPin.Name = "lbPin";
			this.lbPin.Size = new Size(25, 13);
			this.lbPin.TabIndex = 90;
			this.lbPin.Text = "PIN";
			this.lbPin.TextAlign = ContentAlignment.MiddleLeft;
			this.lbPin.Visible = false;
			this.tbPin.AllowDrop = true;
			this.tbPin.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			this.tbPin.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.tbPin.BackColor = Color.FromArgb(224, 224, 224);
			this.tbPin.BorderStyle = BorderStyle.FixedSingle;
			this.tbPin.CharacterCasing = CharacterCasing.Upper;
			this.tbPin.Location = new Point(671, 30);
			this.tbPin.Margin = new Padding(2, 3, 2, 3);
			this.tbPin.MaxLength = 10;
			this.tbPin.Name = "tbPin";
			this.tbPin.PasswordChar = '*';
			this.tbPin.Size = new Size(40, 20);
			this.tbPin.TabIndex = 7;
			this.tbPin.Visible = false;
			this.tbPin.KeyDown += new KeyEventHandler(this.tbPin_KeyDown);
			this.tbPin.Leave += new EventHandler(this.controlOrder_Leave);
			this.tbPin.Enter += new EventHandler(this.controlOrder_Enter);
			this.rbCover.AutoSize = true;
			this.rbCover.ForeColor = Color.LightGray;
			this.rbCover.Location = new Point(57, 29);
			this.rbCover.Name = "rbCover";
			this.rbCover.Size = new Size(53, 17);
			this.rbCover.TabIndex = 88;
			this.rbCover.TabStop = true;
			this.rbCover.Text = "Cover";
			this.rbCover.UseVisualStyleBackColor = true;
			this.rbCover.CheckedChanged += new EventHandler(this.rbBuy_CheckedChanged);
			this.rbShort.AutoSize = true;
			this.rbShort.ForeColor = Color.LightGray;
			this.rbShort.Location = new Point(3, 30);
			this.rbShort.Name = "rbShort";
			this.rbShort.Size = new Size(50, 17);
			this.rbShort.TabIndex = 87;
			this.rbShort.TabStop = true;
			this.rbShort.Text = "Short";
			this.rbShort.UseVisualStyleBackColor = true;
			this.rbShort.CheckedChanged += new EventHandler(this.rbBuy_CheckedChanged);
			this.rbSell.AutoSize = true;
			this.rbSell.ForeColor = Color.LightGray;
			this.rbSell.Location = new Point(47, 7);
			this.rbSell.Name = "rbSell";
			this.rbSell.Size = new Size(42, 17);
			this.rbSell.TabIndex = 86;
			this.rbSell.TabStop = true;
			this.rbSell.Text = "Sell";
			this.rbSell.UseVisualStyleBackColor = true;
			this.rbSell.CheckedChanged += new EventHandler(this.rbBuy_CheckedChanged);
			this.rbBuy.AutoSize = true;
			this.rbBuy.ForeColor = Color.LightGray;
			this.rbBuy.Location = new Point(4, 6);
			this.rbBuy.Name = "rbBuy";
			this.rbBuy.Size = new Size(43, 17);
			this.rbBuy.TabIndex = 85;
			this.rbBuy.TabStop = true;
			this.rbBuy.Text = "Buy";
			this.rbBuy.UseVisualStyleBackColor = true;
			this.rbBuy.CheckedChanged += new EventHandler(this.rbBuy_CheckedChanged);
			this.cbPrice.AllowDrop = true;
			this.cbPrice.AutoCompleteCustomSource.AddRange(new string[]
			{
				"",
				"ATO",
				"ATC",
				"MP",
				"MO",
				"ML"
			});
			this.cbPrice.AutoCompleteMode = AutoCompleteMode.Append;
			this.cbPrice.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.cbPrice.BackColor = Color.FromArgb(224, 224, 224);
			this.cbPrice.FlatStyle = FlatStyle.Popup;
			this.cbPrice.ForeColor = Color.Black;
			this.cbPrice.FormattingEnabled = true;
			this.cbPrice.Items.AddRange(new object[]
			{
				"",
				"ATO",
				"ATC",
				"MP",
				"MO",
				"ML"
			});
			this.cbPrice.Location = new Point(454, 5);
			this.cbPrice.Name = "cbPrice";
			this.cbPrice.Size = new Size(57, 21);
			this.cbPrice.TabIndex = 3;
			this.cbPrice.Leave += new EventHandler(this.controlOrder_Leave);
			this.cbPrice.Enter += new EventHandler(this.controlOrder_Enter);
			this.cbPrice.DragDrop += new DragEventHandler(this.cbPrice_DragDrop);
			this.cbPrice.DragEnter += new DragEventHandler(this.cbPrice_DragEnter);
			this.cbPrice.KeyPress += new KeyPressEventHandler(this.cbPrice_KeyPress);
			this.cbPrice.KeyDown += new KeyEventHandler(this.cbPrice_KeyDown);
			this.tbTimes.AllowDrop = true;
			this.tbTimes.BackColor = Color.FromArgb(224, 224, 224);
			this.tbTimes.BorderStyle = BorderStyle.FixedSingle;
			this.tbTimes.CharacterCasing = CharacterCasing.Upper;
			this.tbTimes.Location = new Point(376, 7);
			this.tbTimes.Margin = new Padding(2, 3, 2, 3);
			this.tbTimes.MaxLength = 2;
			this.tbTimes.Name = "tbTimes";
			this.tbTimes.Size = new Size(40, 20);
			this.tbTimes.TabIndex = 80;
			this.tbTimes.TextAlign = HorizontalAlignment.Center;
			this.tbTimes.Visible = false;
			this.tbTimes.KeyDown += new KeyEventHandler(this.tbTimes_KeyDown);
			this.tbTimes.Leave += new EventHandler(this.controlOrder_Leave);
			this.tbTimes.Enter += new EventHandler(this.controlOrder_Enter);
			this.lbTimes.AutoSize = true;
			this.lbTimes.ForeColor = Color.LightGray;
			this.lbTimes.Location = new Point(337, 8);
			this.lbTimes.Margin = new Padding(2, 0, 2, 0);
			this.lbTimes.Name = "lbTimes";
			this.lbTimes.Size = new Size(35, 13);
			this.lbTimes.TabIndex = 79;
			this.lbTimes.Text = "Times";
			this.lbTimes.TextAlign = ContentAlignment.MiddleLeft;
			this.lbTimes.Visible = false;
			this.toolTip1.IsBalloon = true;
			this.toolTip1.ToolTipIcon = ToolTipIcon.Info;
			this.toolTip1.ToolTipTitle = "Info guide";
			this.panelDerivative.BackColor = Color.FromArgb(45, 45, 45);
			this.panelDerivative.Controls.Add(this.cbPosition);
			this.panelDerivative.Controls.Add(this.rdbTfexSell);
			this.panelDerivative.Controls.Add(this.rdbTfexBuy);
			this.panelDerivative.Controls.Add(this.tbTfexPriceCondition);
			this.panelDerivative.Controls.Add(this.cbTfexConStopOrder);
			this.panelDerivative.Controls.Add(this.tbPriceT);
			this.panelDerivative.Controls.Add(this.tbSeriesCondition);
			this.panelDerivative.Controls.Add(this.tbSeries);
			this.panelDerivative.Controls.Add(this.chbTfexStopOrder);
			this.panelDerivative.Controls.Add(this.tbPublishT);
			this.panelDerivative.Controls.Add(this.lbValidity);
			this.panelDerivative.Controls.Add(this.tbVolumeT);
			this.panelDerivative.Controls.Add(this.lbType);
			this.panelDerivative.Controls.Add(this.btnClearTextT);
			this.panelDerivative.Controls.Add(this.lbPosition);
			this.panelDerivative.Controls.Add(this.btnSendOrderT);
			this.panelDerivative.Controls.Add(this.lbPublish);
			this.panelDerivative.Controls.Add(this.cbValidity);
			this.panelDerivative.Controls.Add(this.lbPriceT);
			this.panelDerivative.Controls.Add(this.cbType);
			this.panelDerivative.Controls.Add(this.lbVolumeT);
			this.panelDerivative.Controls.Add(this.lbSeries);
			this.panelDerivative.Location = new Point(7, 124);
			this.panelDerivative.Name = "panelDerivative";
			this.panelDerivative.Size = new Size(827, 58);
			this.panelDerivative.TabIndex = 83;
			this.cbPosition.AutoCompleteCustomSource.AddRange(new string[]
			{
				"OPEN",
				"CLOSE"
			});
			this.cbPosition.AutoCompleteMode = AutoCompleteMode.Append;
			this.cbPosition.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.cbPosition.BackColor = Color.FromArgb(224, 224, 224);
			this.cbPosition.FlatStyle = FlatStyle.Popup;
			this.cbPosition.FormattingEnabled = true;
			this.cbPosition.Items.AddRange(new object[]
			{
				"OPEN",
				"CLOSE"
			});
			this.cbPosition.Location = new Point(160, 28);
			this.cbPosition.Name = "cbPosition";
			this.cbPosition.Size = new Size(90, 21);
			this.cbPosition.TabIndex = 117;
			this.cbPosition.Text = "OPEN";
			this.cbPosition.Leave += new EventHandler(this.controlOrderTFEX_Leave);
			this.cbPosition.Enter += new EventHandler(this.controlOrder_Enter);
			this.cbPosition.KeyDown += new KeyEventHandler(this.cbPosition_KeyDown);
			this.rdbTfexSell.AutoSize = true;
			this.rdbTfexSell.ForeColor = Color.LightGray;
			this.rdbTfexSell.Location = new Point(59, 4);
			this.rdbTfexSell.Name = "rdbTfexSell";
			this.rdbTfexSell.Size = new Size(42, 17);
			this.rdbTfexSell.TabIndex = 116;
			this.rdbTfexSell.TabStop = true;
			this.rdbTfexSell.Text = "Sell";
			this.rdbTfexSell.UseVisualStyleBackColor = true;
			this.rdbTfexSell.CheckedChanged += new EventHandler(this.rdbTfexSell_CheckedChanged);
			this.rdbTfexBuy.AutoSize = true;
			this.rdbTfexBuy.ForeColor = Color.LightGray;
			this.rdbTfexBuy.Location = new Point(13, 4);
			this.rdbTfexBuy.Name = "rdbTfexBuy";
			this.rdbTfexBuy.Size = new Size(43, 17);
			this.rdbTfexBuy.TabIndex = 115;
			this.rdbTfexBuy.TabStop = true;
			this.rdbTfexBuy.Text = "Buy";
			this.rdbTfexBuy.UseVisualStyleBackColor = true;
			this.rdbTfexBuy.CheckedChanged += new EventHandler(this.rdbTfexBuy_CheckedChanged);
			this.tbTfexPriceCondition.AllowDrop = true;
			this.tbTfexPriceCondition.AutoCompleteCustomSource.AddRange(new string[]
			{
				"ATO",
				"ATC",
				"MP"
			});
			this.tbTfexPriceCondition.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			this.tbTfexPriceCondition.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.tbTfexPriceCondition.BackColor = Color.FromArgb(224, 224, 224);
			this.tbTfexPriceCondition.BorderStyle = BorderStyle.FixedSingle;
			this.tbTfexPriceCondition.CharacterCasing = CharacterCasing.Upper;
			this.tbTfexPriceCondition.Location = new Point(761, 29);
			this.tbTfexPriceCondition.Margin = new Padding(2, 3, 2, 3);
			this.tbTfexPriceCondition.MaxLength = 10;
			this.tbTfexPriceCondition.Name = "tbTfexPriceCondition";
			this.tbTfexPriceCondition.Size = new Size(61, 20);
			this.tbTfexPriceCondition.TabIndex = 114;
			this.tbTfexPriceCondition.Visible = false;
			this.tbTfexPriceCondition.KeyDown += new KeyEventHandler(this.tbTfexPriceCondition_KeyDown);
			this.tbTfexPriceCondition.Leave += new EventHandler(this.controlOrderTFEX_Leave);
			this.tbTfexPriceCondition.KeyPress += new KeyPressEventHandler(this.tbTfexPriceCondition_KeyPress);
			this.tbTfexPriceCondition.Enter += new EventHandler(this.controlOrder_Enter);
			this.cbTfexConStopOrder.BackColor = Color.FromArgb(224, 224, 224);
			this.cbTfexConStopOrder.FlatStyle = FlatStyle.Popup;
			this.cbTfexConStopOrder.FormattingEnabled = true;
			this.cbTfexConStopOrder.Items.AddRange(new object[]
			{
				"Bid >=",
				"Bid <=",
				"Ask >=",
				"Ask <=",
				"Last >=",
				"Last <="
			});
			this.cbTfexConStopOrder.Location = new Point(688, 28);
			this.cbTfexConStopOrder.Name = "cbTfexConStopOrder";
			this.cbTfexConStopOrder.Size = new Size(68, 21);
			this.cbTfexConStopOrder.TabIndex = 113;
			this.cbTfexConStopOrder.TabStop = false;
			this.cbTfexConStopOrder.Visible = false;
			this.cbTfexConStopOrder.Leave += new EventHandler(this.controlOrderTFEX_Leave);
			this.cbTfexConStopOrder.Enter += new EventHandler(this.controlOrder_Enter);
			this.cbTfexConStopOrder.KeyDown += new KeyEventHandler(this.cbTfexConStopOrder_KeyDown);
			this.tbPriceT.AllowDrop = true;
			this.tbPriceT.BackColor = Color.FromArgb(224, 224, 224);
			this.tbPriceT.BorderStyle = BorderStyle.FixedSingle;
			this.tbPriceT.CharacterCasing = CharacterCasing.Upper;
			this.tbPriceT.Location = new Point(444, 4);
			this.tbPriceT.Margin = new Padding(2, 3, 2, 3);
			this.tbPriceT.MaxLength = 10;
			this.tbPriceT.Name = "tbPriceT";
			this.tbPriceT.Size = new Size(70, 20);
			this.tbPriceT.TabIndex = 96;
			this.tbPriceT.KeyDown += new KeyEventHandler(this.tbPriceT_KeyDown);
			this.tbPriceT.Leave += new EventHandler(this.controlOrderTFEX_Leave);
			this.tbPriceT.KeyPress += new KeyPressEventHandler(this.tbTfexPriceCondition_KeyPress);
			this.tbPriceT.Enter += new EventHandler(this.controlOrder_Enter);
			this.tbSeriesCondition.AllowDrop = true;
			this.tbSeriesCondition.BackColor = Color.FromArgb(224, 224, 224);
			this.tbSeriesCondition.BorderStyle = BorderStyle.FixedSingle;
			this.tbSeriesCondition.CharacterCasing = CharacterCasing.Upper;
			this.tbSeriesCondition.ForeColor = Color.Silver;
			this.tbSeriesCondition.Location = new Point(606, 29);
			this.tbSeriesCondition.Margin = new Padding(2, 3, 2, 3);
			this.tbSeriesCondition.MaxLength = 10;
			this.tbSeriesCondition.Name = "tbSeriesCondition";
			this.tbSeriesCondition.Size = new Size(77, 20);
			this.tbSeriesCondition.TabIndex = 112;
			this.tbSeriesCondition.Visible = false;
			this.tbSeriesCondition.KeyDown += new KeyEventHandler(this.tbSeriesCondition_KeyDown);
			this.tbSeriesCondition.Leave += new EventHandler(this.controlOrderTFEX_Leave);
			this.tbSeriesCondition.Enter += new EventHandler(this.controlOrder_Enter);
			this.tbSeries.AllowDrop = true;
			this.tbSeries.BackColor = Color.FromArgb(224, 224, 224);
			this.tbSeries.BorderStyle = BorderStyle.FixedSingle;
			this.tbSeries.CharacterCasing = CharacterCasing.Upper;
			this.tbSeries.Location = new Point(160, 4);
			this.tbSeries.Margin = new Padding(2, 3, 2, 3);
			this.tbSeries.MaxLength = 32;
			this.tbSeries.Name = "tbSeries";
			this.tbSeries.Size = new Size(90, 20);
			this.tbSeries.TabIndex = 94;
			this.tbSeries.KeyDown += new KeyEventHandler(this.tbSeries_KeyDown);
			this.tbSeries.Leave += new EventHandler(this.controlOrderTFEX_Leave);
			this.tbSeries.Enter += new EventHandler(this.controlOrder_Enter);
			this.chbTfexStopOrder.AutoSize = true;
			this.chbTfexStopOrder.ForeColor = Color.LightGray;
			this.chbTfexStopOrder.Location = new Point(528, 30);
			this.chbTfexStopOrder.Name = "chbTfexStopOrder";
			this.chbTfexStopOrder.Size = new Size(77, 17);
			this.chbTfexStopOrder.TabIndex = 100;
			this.chbTfexStopOrder.Text = "Stop Order";
			this.chbTfexStopOrder.UseVisualStyleBackColor = true;
			this.chbTfexStopOrder.CheckedChanged += new EventHandler(this.chbTfexStopOrder_CheckedChanged);
			this.tbPublishT.AllowDrop = true;
			this.tbPublishT.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			this.tbPublishT.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.tbPublishT.BackColor = Color.FromArgb(224, 224, 224);
			this.tbPublishT.BorderStyle = BorderStyle.FixedSingle;
			this.tbPublishT.CharacterCasing = CharacterCasing.Upper;
			this.tbPublishT.Location = new Point(580, 4);
			this.tbPublishT.Margin = new Padding(2, 3, 2, 3);
			this.tbPublishT.MaxLength = 12;
			this.tbPublishT.Name = "tbPublishT";
			this.tbPublishT.Size = new Size(70, 20);
			this.tbPublishT.TabIndex = 97;
			this.tbPublishT.KeyDown += new KeyEventHandler(this.tbPublishT_KeyDown);
			this.tbPublishT.Leave += new EventHandler(this.controlOrderTFEX_Leave);
			this.tbPublishT.Enter += new EventHandler(this.controlOrder_Enter);
			this.lbValidity.AutoSize = true;
			this.lbValidity.ForeColor = Color.LightGray;
			this.lbValidity.Location = new Point(395, 32);
			this.lbValidity.Margin = new Padding(2, 0, 2, 0);
			this.lbValidity.Name = "lbValidity";
			this.lbValidity.Size = new Size(40, 13);
			this.lbValidity.TabIndex = 106;
			this.lbValidity.Text = "Validity";
			this.lbValidity.TextAlign = ContentAlignment.MiddleLeft;
			this.tbVolumeT.AllowDrop = true;
			this.tbVolumeT.BackColor = Color.FromArgb(224, 224, 224);
			this.tbVolumeT.BorderStyle = BorderStyle.FixedSingle;
			this.tbVolumeT.Location = new Point(309, 4);
			this.tbVolumeT.Margin = new Padding(2, 3, 2, 3);
			this.tbVolumeT.MaxLength = 10;
			this.tbVolumeT.Name = "tbVolumeT";
			this.tbVolumeT.Size = new Size(70, 20);
			this.tbVolumeT.TabIndex = 95;
			this.tbVolumeT.TextChanged += new EventHandler(this.tbVolumeT_TextChanged);
			this.tbVolumeT.KeyDown += new KeyEventHandler(this.tbVolumeT_KeyDown);
			this.tbVolumeT.Leave += new EventHandler(this.controlOrderTFEX_Leave);
			this.tbVolumeT.Enter += new EventHandler(this.controlOrder_Enter);
			this.lbType.AutoSize = true;
			this.lbType.ForeColor = Color.LightGray;
			this.lbType.Location = new Point(261, 32);
			this.lbType.Margin = new Padding(2, 0, 2, 0);
			this.lbType.Name = "lbType";
			this.lbType.Size = new Size(31, 13);
			this.lbType.TabIndex = 110;
			this.lbType.Text = "Type";
			this.lbType.TextAlign = ContentAlignment.MiddleLeft;
			this.btnClearTextT.AutoSize = true;
			this.btnClearTextT.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.btnClearTextT.BackColor = Color.WhiteSmoke;
			this.btnClearTextT.FlatStyle = FlatStyle.Popup;
			this.btnClearTextT.ForeColor = Color.Black;
			this.btnClearTextT.Location = new Point(715, 3);
			this.btnClearTextT.Margin = new Padding(2);
			this.btnClearTextT.MaximumSize = new Size(68, 27);
			this.btnClearTextT.Name = "btnClearTextT";
			this.btnClearTextT.Size = new Size(41, 23);
			this.btnClearTextT.TabIndex = 108;
			this.btnClearTextT.TabStop = false;
			this.btnClearTextT.Text = "Clear";
			this.btnClearTextT.UseVisualStyleBackColor = false;
			this.btnClearTextT.Click += new EventHandler(this.btnClearTextT_Click);
			this.lbPosition.AutoSize = true;
			this.lbPosition.ForeColor = Color.LightGray;
			this.lbPosition.Location = new Point(102, 32);
			this.lbPosition.Margin = new Padding(2, 0, 2, 0);
			this.lbPosition.Name = "lbPosition";
			this.lbPosition.Size = new Size(44, 13);
			this.lbPosition.TabIndex = 109;
			this.lbPosition.Text = "Position";
			this.lbPosition.TextAlign = ContentAlignment.MiddleLeft;
			this.btnSendOrderT.AutoSize = true;
			this.btnSendOrderT.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.btnSendOrderT.BackColor = Color.WhiteSmoke;
			this.btnSendOrderT.FlatStyle = FlatStyle.Popup;
			this.btnSendOrderT.ForeColor = Color.Black;
			this.btnSendOrderT.Location = new Point(661, 3);
			this.btnSendOrderT.Margin = new Padding(2);
			this.btnSendOrderT.MaximumSize = new Size(68, 27);
			this.btnSendOrderT.Name = "btnSendOrderT";
			this.btnSendOrderT.Size = new Size(42, 23);
			this.btnSendOrderT.TabIndex = 107;
			this.btnSendOrderT.TabStop = false;
			this.btnSendOrderT.Text = "Send";
			this.btnSendOrderT.UseVisualStyleBackColor = false;
			this.btnSendOrderT.Click += new EventHandler(this.btnSendOrderT_Click);
			this.lbPublish.AutoSize = true;
			this.lbPublish.ForeColor = Color.LightGray;
			this.lbPublish.Location = new Point(528, 7);
			this.lbPublish.Margin = new Padding(2, 0, 2, 0);
			this.lbPublish.Name = "lbPublish";
			this.lbPublish.Size = new Size(44, 13);
			this.lbPublish.TabIndex = 105;
			this.lbPublish.Text = "P/B Vol";
			this.lbPublish.TextAlign = ContentAlignment.MiddleLeft;
			this.cbValidity.AutoCompleteCustomSource.AddRange(new string[]
			{
				"",
				"IOC",
				"FOK"
			});
			this.cbValidity.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.cbValidity.BackColor = Color.FromArgb(224, 224, 224);
			this.cbValidity.FlatStyle = FlatStyle.Popup;
			this.cbValidity.FormattingEnabled = true;
			this.cbValidity.Items.AddRange(new object[]
			{
				"DAY",
				"FAK",
				"FOK"
			});
			this.cbValidity.Location = new Point(444, 28);
			this.cbValidity.Name = "cbValidity";
			this.cbValidity.Size = new Size(70, 21);
			this.cbValidity.TabIndex = 99;
			this.cbValidity.Leave += new EventHandler(this.controlOrderTFEX_Leave);
			this.cbValidity.Enter += new EventHandler(this.controlOrder_Enter);
			this.cbValidity.KeyDown += new KeyEventHandler(this.cbValidity_KeyDown);
			this.lbPriceT.AutoSize = true;
			this.lbPriceT.ForeColor = Color.LightGray;
			this.lbPriceT.Location = new Point(395, 7);
			this.lbPriceT.Margin = new Padding(2, 0, 2, 0);
			this.lbPriceT.Name = "lbPriceT";
			this.lbPriceT.Size = new Size(31, 13);
			this.lbPriceT.TabIndex = 101;
			this.lbPriceT.Text = "Price";
			this.lbPriceT.TextAlign = ContentAlignment.MiddleLeft;
			this.cbType.AutoCompleteCustomSource.AddRange(new string[]
			{
				"",
				"IOC",
				"FOK"
			});
			this.cbType.AutoCompleteMode = AutoCompleteMode.Append;
			this.cbType.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.cbType.BackColor = Color.FromArgb(224, 224, 224);
			this.cbType.FlatStyle = FlatStyle.Popup;
			this.cbType.FormattingEnabled = true;
			this.cbType.Items.AddRange(new object[]
			{
				"Limit",
				"MP",
				"MO",
				"ML"
			});
			this.cbType.Location = new Point(309, 28);
			this.cbType.Name = "cbType";
			this.cbType.Size = new Size(70, 21);
			this.cbType.TabIndex = 98;
			this.cbType.Text = "Limit";
			this.cbType.SelectedIndexChanged += new EventHandler(this.cbType_SelectedIndexChanged);
			this.cbType.Leave += new EventHandler(this.controlOrder_Enter);
			this.cbType.Enter += new EventHandler(this.controlOrder_Enter);
			this.cbType.KeyDown += new KeyEventHandler(this.cbType_KeyDown);
			this.lbVolumeT.AutoSize = true;
			this.lbVolumeT.ForeColor = Color.LightGray;
			this.lbVolumeT.Location = new Point(261, 7);
			this.lbVolumeT.Margin = new Padding(2, 0, 2, 0);
			this.lbVolumeT.Name = "lbVolumeT";
			this.lbVolumeT.Size = new Size(42, 13);
			this.lbVolumeT.TabIndex = 103;
			this.lbVolumeT.Text = "Volume";
			this.lbVolumeT.TextAlign = ContentAlignment.MiddleLeft;
			this.lbSeries.AutoSize = true;
			this.lbSeries.ForeColor = Color.LightGray;
			this.lbSeries.Location = new Point(110, 7);
			this.lbSeries.Margin = new Padding(2, 0, 2, 0);
			this.lbSeries.Name = "lbSeries";
			this.lbSeries.Size = new Size(36, 13);
			this.lbSeries.TabIndex = 102;
			this.lbSeries.Text = "Series";
			this.lbSeries.TextAlign = ContentAlignment.MiddleLeft;
			this.AllowDrop = true;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.DimGray;
			base.Controls.Add(this.panelDerivative);
			base.Controls.Add(this.panelEquity);
			base.Controls.Add(this.panelTop);
			base.Controls.Add(this.lbLoading);
			this.ForeColor = Color.Black;
			base.Margin = new Padding(0);
			base.Name = "ucSendNewOrder";
			base.Size = new Size(870, 217);
			base.Leave += new EventHandler(this.ucSendNewOrder_Leave);
			base.Enter += new EventHandler(this.ucSendNewOrder_Enter);
			this.panelTop.ResumeLayout(false);
			this.panelTop.PerformLayout();
			this.panelEquity.ResumeLayout(false);
			this.panelEquity.PerformLayout();
			this.panelStopOrder.ResumeLayout(false);
			this.panelStopOrder.PerformLayout();
			this.panelDerivative.ResumeLayout(false);
			this.panelDerivative.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ucSendNewOrder()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
			base.UpdateStyles();
			try
			{
				if (!base.DesignMode)
				{
					this._showSide = "";
					this.bgwReloadCredit = new BackgroundWorker();
					this.bgwReloadCredit.WorkerReportsProgress = true;
					this.bgwReloadCredit.DoWork += new DoWorkEventHandler(this.bgwReloadCredit_DoWork);
					this.bgwReloadCredit.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwReloadCredit_RunWorkerCompleted);
					this.bgwSendOrder = new BackgroundWorker();
					this.bgwSendOrder.WorkerReportsProgress = true;
					this.bgwSendOrder.DoWork += new DoWorkEventHandler(this.bgwSendOrder_DoWork);
					this.bgwSendOrder.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwSendOrder_RunWorkerCompleted);
				}
				if (this.timerSwitchAccount == null)
				{
					this.timerSwitchAccount = new System.Windows.Forms.Timer();
					this.timerSwitchAccount.Interval = 300;
					this.timerSwitchAccount.Tick -= new EventHandler(this.timerSwitchAccount_Tick);
					this.timerSwitchAccount.Tick += new EventHandler(this.timerSwitchAccount_Tick);
				}
				this.timerSwitchAccount.Enabled = false;
				this.cbDepCollateral.Items.Add("");
				this.cbDepCollateral.Items.Add("Deposit");
				if (ApplicationInfo.SupportCollateral == "Y")
				{
					this.cbDepCollateral.Items.Add("Collateral");
				}
				this.panelEquity.Visible = false;
				foreach (Control control in this.panelEquity.Controls)
				{
					control.Visible = false;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SendOrderBox.Constructor", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void InitAccount()
		{
			if (this.cbAccount.InvokeRequired)
			{
				this.cbAccount.Invoke(new MethodInvoker(this.InitAccount));
			}
			else
			{
				try
				{
					if (this.cbStock.AutoCompleteCustomSource != null)
					{
						if (this.cbStock.AutoCompleteCustomSource.Count == 0 && ApplicationInfo.StockAutoComp != null)
						{
							this.cbStock.AutoCompleteMode = AutoCompleteMode.Suggest;
							this.cbStock.AutoCompleteSource = AutoCompleteSource.CustomSource;
							this.cbStock.AutoCompleteCustomSource = ApplicationInfo.StockAutoComp;
						}
					}
					if (ApplicationInfo.IsSupportTfex)
					{
						if (this.tbSeries.AutoCompleteCustomSource != null)
						{
							if (this.tbSeries.AutoCompleteCustomSource.Count == 0 && ApplicationInfo.SeriesAutoComp != null)
							{
								this.tbSeries.AutoCompleteMode = AutoCompleteMode.Suggest;
								this.tbSeries.AutoCompleteSource = AutoCompleteSource.CustomSource;
								this.tbSeries.AutoCompleteCustomSource = ApplicationInfo.SeriesAutoComp;
							}
						}
						if (this.tbSeriesCondition.AutoCompleteCustomSource != null)
						{
							if (this.tbSeriesCondition.AutoCompleteCustomSource.Count == 0 && ApplicationInfo.SeriesAutoComp != null)
							{
								this.tbSeriesCondition.AutoCompleteMode = AutoCompleteMode.Suggest;
								this.tbSeriesCondition.AutoCompleteSource = AutoCompleteSource.CustomSource;
								this.tbSeriesCondition.AutoCompleteCustomSource = ApplicationInfo.SeriesAutoComp;
							}
						}
					}
					this.cbAccount.Items.Clear();
					if (ApplicationInfo.AccInfo.Items.Count > 0)
					{
						foreach (KeyValuePair<string, AccountInfo.ItemInfo> current in ApplicationInfo.AccInfo.Items)
						{
							string str = string.Empty;
							if (current.Value.Market == "E")
							{
								str = "Equity";
							}
							else
							{
								if (current.Value.Market == "T")
								{
									str = "Derivative";
								}
							}
							this.cbAccount.Items.Add(current.Key + " (" + str + ")");
						}
						this.cbAccount.SelectedIndex = 0;
						ApplicationInfo.AccInfo.CurrentAccount = this.GetAccount(this.cbAccount.Text.Trim());
						if (ApplicationInfo.AccInfo.Items.ContainsKey(ApplicationInfo.AccInfo.CurrentAccount))
						{
							string market = ApplicationInfo.AccInfo.Items[ApplicationInfo.AccInfo.CurrentAccount].Market;
							if (market == "E")
							{
								ApplicationInfo.IsEquityAccount = true;
							}
							else
							{
								if (market == "T")
								{
									ApplicationInfo.IsEquityAccount = false;
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetResize()
		{
			try
			{
				this.panelTop.Hide();
				this.panelEquity.Hide();
				this.panelDerivative.Hide();
				Font default_Font = Settings.Default.Default_Font;
				if (!this.Font.Equals(default_Font))
				{
					this.Font = default_Font;
					this.panelTop.Font = default_Font;
				}
				int num = 5;
				int num2 = num + 3;
				this.chbNVDR.TabIndex = 2;
				if (ApplicationInfo.IsEquityAccount)
				{
					if (Settings.Default.MainBottomStyle == 1)
					{
						this.lbSide.Left = 5;
						this.lbSide.Top = num2;
						this.cbSide.Top = num - 1;
						this.cbSide.Left = this.lbSide.Right + 3;
						this.lbStock.Left = this.cbSide.Right + 5;
						this.lbStock.Top = num2;
						this.cbStock.Left = this.lbStock.Right + 1;
						this.cbStock.Top = num - 1;
						this.chbNVDR.Location = new Point(this.cbStock.Right + 5, num + 2);
						this.lbVolume.Left = this.chbNVDR.Right + 5;
						this.tbVolume.Left = this.lbVolume.Right + 1;
						this.lbVolume.Top = num2;
						this.tbVolume.Top = num;
						if (ApplicationInfo.SupportOrderTimes)
						{
							this.lbTimes.Top = num2;
							this.tbTimes.Top = num;
							this.lbTimes.Left = this.tbVolume.Right + 4;
							this.tbTimes.Left = this.lbTimes.Right + 1;
							this.lbPrice.Left = this.tbTimes.Right + 4;
							this.cbPrice.Left = this.lbPrice.Right + 1;
						}
						else
						{
							this.lbPrice.Left = this.tbVolume.Right + 4;
							this.cbPrice.Left = this.lbPrice.Right + 1;
						}
						this.lbPrice.Top = num2;
						this.cbPrice.Top = num - 1;
						int num3 = this.cbPrice.Right + 4 + this.lbPublic.Width + 4 + this.tbPublic.Width + 4 + this.lbCondition.Width + 4 + this.cbCondition.Width + 1 + this.btnSendOrder.Width + 10;
						if (ApplicationInfo.SuuportSBL == "Y" && ApplicationInfo.AccInfo.CurrentAccountType == "B")
						{
							num3 += this.lbDep.Width + 4 + this.cbDepCollateral.Width + 4;
						}
						if (ApplicationInfo.StopOrderSupported)
						{
							num3 += this.chbEqStopOrder.Width + 10;
						}
						if (num3 < base.Width)
						{
							this.lbPublic.Location = new Point(this.cbPrice.Right + 4, num2);
							this.tbPublic.Location = new Point(this.lbPublic.Right + 4, num);
							this.lbCondition.Location = new Point(this.tbPublic.Right + 4, num2);
							this.cbCondition.Location = new Point(this.lbCondition.Right + 1, num - 1);
							if (ApplicationInfo.SuuportSBL == "Y" && ApplicationInfo.AccInfo.CurrentAccountType == "B")
							{
								this.lbDep.Location = new Point(this.cbCondition.Right + 7, num2);
								this.cbDepCollateral.Location = new Point(this.lbDep.Right + 4, num - 1);
								if (ApplicationInfo.StopOrderSupported)
								{
									this.chbEqStopOrder.Location = new Point(this.cbDepCollateral.Right + 4, num + 2);
									this.btnSendOrder.Location = new Point(this.chbEqStopOrder.Right + 10, num - 2);
								}
								else
								{
									this.btnSendOrder.Location = new Point(this.cbDepCollateral.Right + 10, num - 2);
								}
							}
							else
							{
								if (ApplicationInfo.StopOrderSupported)
								{
									this.chbEqStopOrder.Location = new Point(this.cbCondition.Right + 10, num + 2);
									this.btnSendOrder.Location = new Point(this.chbEqStopOrder.Right + 10, num - 2);
								}
								else
								{
									this.btnSendOrder.Location = new Point(this.cbCondition.Right + 10, num - 2);
								}
							}
						}
						else
						{
							this.lbPublic.Location = new Point(this.lbVolume.Left, this.tbVolume.Bottom + 8);
							this.tbPublic.Location = new Point(this.tbVolume.Left, this.tbVolume.Bottom + 6);
							this.lbCondition.Location = new Point(this.lbTimes.Left, this.lbPublic.Top);
							this.cbCondition.Location = new Point(this.tbTimes.Left, this.tbPublic.Top);
							if (ApplicationInfo.SuuportSBL == "Y" && ApplicationInfo.AccInfo.CurrentAccountType == "B")
							{
								this.lbDep.Location = new Point(this.lbPrice.Left, this.lbPublic.Top);
								this.cbDepCollateral.Location = new Point(this.cbPrice.Left, this.tbPublic.Top);
								if (ApplicationInfo.StopOrderSupported)
								{
									this.chbEqStopOrder.Location = new Point(this.cbDepCollateral.Right + 10, this.tbPublic.Top + 2);
								}
							}
							else
							{
								if (ApplicationInfo.StopOrderSupported)
								{
									this.chbEqStopOrder.Location = new Point(this.cbCondition.Right + 10, this.tbPublic.Top + 2);
								}
							}
							this.btnSendOrder.Location = new Point(this.cbPrice.Right + 20, num - 2);
						}
					}
					else
					{
						if (Settings.Default.MainBottomStyle == 2)
						{
							this.rbBuy.Location = new Point(5, num + 1);
							this.rbSell.Location = new Point(this.rbBuy.Right + 2, this.rbSell.Top = num + 1);
							if (ApplicationInfo.SuuportSBL == "Y" && ApplicationInfo.AccInfo.CurrentAccountType == "B")
							{
								this.rbShort.Location = new Point(this.rbSell.Right + 2, num + 1);
								this.rbCover.Location = new Point(this.rbShort.Right + 2, num + 1);
								this.lbStock.Location = new Point(this.rbCover.Right + 5, num2);
							}
							else
							{
								this.lbStock.Location = new Point(this.rbSell.Right + 5, num2);
							}
							this.cbStock.Location = new Point(this.lbStock.Right + 5, num - 1);
							this.lbVolume.Location = new Point(this.cbStock.Right + 10, num2);
							this.btnVolDec.Location = new Point(this.lbVolume.Right + 3, num + 1);
							this.tbVolume.Location = new Point(this.btnVolDec.Right + 2, num);
							this.btnVolInc.Location = new Point(this.tbVolume.Right + 2, num + 1);
							this.lbPrice.Location = new Point(this.btnVolInc.Right + 10, num2);
							this.btnPriceDec.Location = new Point(this.lbPrice.Right + 2, num + 1);
							this.cbPrice.Location = new Point(this.btnPriceDec.Right + 1, num - 1);
							this.btnPriceInc.Location = new Point(this.cbPrice.Right + 2, num + 1);
							if (ApplicationInfo.StopOrderSupported)
							{
								this.chbEqStopOrder.Location = new Point(this.btnPriceInc.Right + 12, num + 2);
								this.lbPin.Location = new Point(this.chbEqStopOrder.Right + 10, num2);
							}
							else
							{
								this.lbPin.Location = new Point(this.btnPriceInc.Right + 15, num2);
							}
							this.tbPin.Location = new Point(this.lbPin.Right + 1, num);
							this.btnSendOrder.Location = new Point(this.tbPin.Right + 10, num - 2);
							this.btnClear.Location = new Point(this.btnSendOrder.Right + 5, num - 2);
						}
						else
						{
							if (Settings.Default.MainBottomStyle == 3)
							{
								this.rbBuy.Location = new Point(5, num + 1);
								this.rbSell.Location = new Point(this.rbBuy.Right + 15, this.rbBuy.Top);
								this.rbShort.Location = new Point(this.rbSell.Left, num + this.rbBuy.Height + 5);
								this.rbCover.Location = new Point(this.rbBuy.Left, this.rbShort.Top);
								this.cbStock.Location = new Point(this.rbSell.Right + 10, num);
								this.chbNVDR.Location = new Point(this.cbStock.Right + 6, num + 1);
								this.lbVolume.Location = new Point(this.chbNVDR.Right + 10, num2);
								this.btnVolDec.Location = new Point(this.lbVolume.Right + 2, num + 1);
								this.tbVolume.Location = new Point(this.btnVolDec.Right + 1, num);
								this.btnVolInc.Location = new Point(this.tbVolume.Right + 1, num + 1);
								this.lbPublic.Location = new Point(this.lbVolume.Right - this.lbPublic.Width, num2 + this.lbVolume.Height + 12);
								this.btnPBDec.Location = new Point(this.btnVolDec.Left, num + this.lbVolume.Height + 13);
								this.tbPublic.Location = new Point(this.tbVolume.Left, num + this.lbVolume.Height + 12);
								this.btnPBInc.Location = new Point(this.btnVolInc.Left, num + this.lbVolume.Height + 13);
								this.lbPrice.Location = new Point(this.btnVolInc.Right + 15, num2);
								this.btnPriceDec.Location = new Point(this.lbPrice.Right + 2, num + 1);
								this.cbPrice.Location = new Point(this.btnPriceDec.Right + 1, num - 1);
								this.btnPriceInc.Location = new Point(this.cbPrice.Right + 1, num + 1);
								this.lbCondition.Left = this.lbPrice.Left;
								this.lbCondition.Top = this.lbPublic.Top;
								this.cbCondition.Left = this.cbPrice.Left;
								this.cbCondition.Top = this.tbPublic.Top;
								this.lbDep.Top = num2;
								this.cbDepCollateral.Top = num - 1;
								if (ApplicationInfo.SuuportSBL == "Y" && ApplicationInfo.AccInfo.CurrentAccountType == "B")
								{
									this.lbDep.Left = this.btnPriceInc.Right + 7;
									this.cbDepCollateral.Left = this.lbDep.Right + 2;
									this.lbPin.Left = this.cbDepCollateral.Right + 10;
									this.btnSendOrder.Left = this.cbDepCollateral.Right + 10;
								}
								else
								{
									this.lbPin.Left = this.btnPriceInc.Right + 10;
								}
								this.lbPin.Top = num2;
								this.tbPin.Top = num;
								this.tbPin.Left = this.lbPin.Right + 2;
								this.btnSendOrder.Left = this.tbPin.Right + 10;
								this.btnSendOrder.Top = num - 2;
								this.btnClear.Left = this.btnSendOrder.Right + 5;
								this.btnClear.Top = num - 2;
								if (ApplicationInfo.StopOrderSupported)
								{
									this.chbEqStopOrder.Location = new Point(this.cbCondition.Right + 15, this.tbPublic.Top + 3);
								}
							}
							else
							{
								if (Settings.Default.MainBottomStyle == 4)
								{
									this.rbBuy.Left = 5;
									this.rbBuy.Top = num + 1;
									this.rbSell.Left = this.rbBuy.Right + 2;
									this.rbSell.Top = num + 1;
									this.rbShort.Top = num + 1;
									this.rbCover.Top = num + 1;
									if (ApplicationInfo.SuuportSBL == "Y" && ApplicationInfo.AccInfo.CurrentAccountType == "B")
									{
										this.rbShort.Left = this.rbSell.Right + 2;
										this.rbCover.Left = this.rbShort.Right + 2;
										this.lbStock.Left = this.rbCover.Right + 5;
									}
									else
									{
										this.lbStock.Left = this.rbSell.Right + 5;
									}
									this.lbStock.Top = num2;
									this.cbStock.Left = this.lbStock.Right + 5;
									this.cbStock.Top = num - 1;
									this.lbVolume.Left = this.cbStock.Right + 10;
									this.lbVolume.Top = num2;
									this.tbVolume.Left = this.lbVolume.Right + 2;
									this.tbVolume.Top = num;
									this.lbPrice.Left = this.tbVolume.Right + 10;
									this.lbPrice.Top = num2;
									this.cbPrice.Left = this.lbPrice.Right + 1;
									this.cbPrice.Top = num - 1;
									this.lbCondition.Left = this.cbPrice.Right + 5;
									this.lbCondition.Top = num2;
									this.cbCondition.Left = this.lbCondition.Right + 5;
									this.cbCondition.Top = num - 1;
									this.chbNVDR.Left = this.cbCondition.Right + 10;
									this.chbNVDR.Top = num + 2;
									if (ApplicationInfo.StopOrderSupported)
									{
										this.chbEqStopOrder.Location = new Point(this.chbNVDR.Right + 10, num + 2);
										this.lbPin.Left = this.chbEqStopOrder.Right + 10;
									}
									else
									{
										this.lbPin.Left = this.chbNVDR.Right + 10;
									}
									this.lbPin.Top = num2;
									this.tbPin.Left = this.lbPin.Right + 1;
									this.tbPin.Top = num;
									this.btnSendOrder.Left = this.tbPin.Right + 10;
									this.btnSendOrder.Top = num - 2;
									this.btnClear.Left = this.btnSendOrder.Right + 5;
									this.btnClear.Top = num - 2;
									this.chbNVDR.TabIndex = 6;
								}
							}
						}
					}
					this.panelStopOrder.Visible = this.chbEqStopOrder.Checked;
					if (this.chbEqStopOrder.Checked)
					{
						this.tbTimes.Text = "";
						this.tbTimes.Enabled = false;
						this.cbDepCollateral.Enabled = false;
					}
					else
					{
						this.tbTimes.Enabled = true;
						this.cbDepCollateral.Enabled = true;
					}
					this.panelStopOrder.Location = new Point(5, this.chbEqStopOrder.Bottom + 7);
					if (!this.panelEquity.Visible)
					{
						this.panelEquity.Show();
					}
				}
				else
				{
					this.rdbTfexBuy.Location = new Point(5, num);
					this.rdbTfexSell.Location = new Point(this.rdbTfexBuy.Right + 5, num);
					this.lbSeries.Location = new Point(this.rdbTfexSell.Right + 5, num2);
					this.tbSeries.Location = new Point(this.lbSeries.Right + 5, num);
					this.lbVolumeT.Location = new Point(this.tbSeries.Right + 5, num2);
					this.tbVolumeT.Location = new Point(this.lbVolumeT.Right + 5, num);
					this.lbPriceT.Location = new Point(this.tbVolumeT.Right + 17, num2);
					this.tbPriceT.Location = new Point(this.lbPriceT.Right + 5, num);
					this.lbPublish.Location = new Point(this.tbPriceT.Right + 5, num2);
					this.tbPublishT.Location = new Point(this.lbPublish.Right + 5, num);
					int y = this.tbSeries.Bottom + 7;
					int y2 = this.tbSeries.Bottom + 5;
					this.lbPosition.Location = new Point(this.lbSeries.Right - this.lbPosition.Width, y);
					this.cbPosition.Location = new Point(this.tbSeries.Left, y2);
					this.lbType.Location = new Point(this.lbVolumeT.Right - this.lbType.Width, y);
					this.cbType.Location = new Point(this.tbVolumeT.Left, y2);
					this.lbValidity.Location = new Point(this.lbPriceT.Right - this.lbValidity.Width, y);
					this.cbValidity.Location = new Point(this.tbPriceT.Left, y2);
					this.chbTfexStopOrder.Location = new Point(this.cbValidity.Right + 10, y);
					this.tbSeriesCondition.Location = new Point(this.chbTfexStopOrder.Right + 5, y);
					this.cbTfexConStopOrder.Location = new Point(this.tbSeriesCondition.Right + 5, y2);
					this.tbTfexPriceCondition.Location = new Point(this.cbTfexConStopOrder.Right + 5, y);
					this.btnSendOrderT.Location = new Point(this.tbPublishT.Right + 20, num - 1);
					this.btnClearTextT.Location = new Point(this.btnSendOrderT.Right + 5, num - 1);
				}
				int num4 = this.cbAccount.Height + 7;
				this.panelTop.SetBounds(0, 0, base.Parent.Width, num4);
				this.cbAccount.Left = this.lbAccount.Right + 2;
				this.lbBuyLimit.Left = this.cbAccount.Right + 10;
				this.tbBuyLimit.Left = this.lbBuyLimit.Right + 4;
				this.lbOnHand.Left = this.tbBuyLimit.Right + 10;
				this.tbOnHand.Left = this.lbOnHand.Right + 4;
				this.lbEquity.Left = this.tbOnHand.Right + 10;
				this.tbEquity.Left = this.lbEquity.Right + 2;
				int num5 = this.btnClear.Height + 4;
				if (ApplicationInfo.IsEquityAccount)
				{
					if (this.chbEqStopOrder.Checked)
					{
						num5 = num4 + this.panelStopOrder.Bottom + 5;
					}
					else
					{
						if (Settings.Default.MainBottomStyle == 2 || Settings.Default.MainBottomStyle == 4)
						{
							num5 += this.panelTop.Height + 2;
						}
						else
						{
							num5 = num4 + this.tbPublic.Bottom + 6;
						}
					}
					this.panelEquity.SetBounds(0, this.panelTop.Bottom, base.Parent.Width, num5 - num4);
				}
				else
				{
					this.panelDerivative.SetBounds(0, this.panelTop.Bottom, base.Parent.Width, this.cbValidity.Bottom + 5);
					num5 = this.panelDerivative.Bottom;
				}
				this.btnSetting.Left = base.Parent.Width - this.btnSetting.Width - 5;
				this.btnStyle4.Left = this.btnSetting.Left - this.btnStyle4.Width - 5;
				this.btnStyle3.Left = this.btnStyle4.Left - this.btnStyle3.Width - 5;
				this.btnStyle2.Left = this.btnStyle3.Left - this.btnStyle2.Width - 5;
				this.btnStyle1.Left = this.btnStyle2.Left - this.btnStyle1.Width - 5;
				this.btnCleanPort.Left = this.btnStyle1.Left - this.btnCleanPort.Width - 5;
				this.btnShowStockAlert.Left = this.btnCleanPort.Left - this.btnShowStockAlert.Width - 5;
				this.btnNotification.Left = this.btnShowStockAlert.Left - this.btnNotification.Width - 5;
				this.btnRisk.Left = this.btnNotification.Left - this.btnRisk.Width - 5;
				if (base.Height != num5)
				{
					base.Height = num5;
					if (this._OnResized != null)
					{
						this._OnResized();
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SetResize", ex);
			}
			finally
			{
				this.panelTop.Show();
				if (ApplicationInfo.IsEquityAccount)
				{
					this.panelEquity.Show();
				}
				else
				{
					this.panelDerivative.Show();
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetFormActivate(string side)
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
				if (ApplicationInfo.IsEquityAccount)
				{
					if (this.SetColorBySide(side))
					{
						if (Settings.Default.BSBoxDefaultStock)
						{
							if (this.cbStock.Text == string.Empty)
							{
								this.SetCurrentSymbol(ApplicationInfo.CurrentSymbol);
							}
						}
						this.ShowCreditValue();
						this.cbStock.Focus();
					}
				}
				else
				{
					this._showSideTFEX = ((side == "B") ? "L" : side);
					this.DoClear_TFEX();
					this.SetColorBySide_TFEX();
					if (Settings.Default.BSBoxDefaultStock)
					{
						if (this.tbSeries.Text == string.Empty)
						{
							this.SetCurrentSymbol(ApplicationInfo.CurrentSymbol);
						}
					}
					this.tbSeries.Focus();
					this.tbSeries.SelectAll();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void DisposeMe()
		{
			try
			{
				this._isActive = false;
				if (this.timerReloadCredit != null)
				{
					this.timerReloadCredit.Stop();
					this.timerReloadCredit = null;
				}
				if (this.tmCloseSplash != null)
				{
					this.tmCloseSplash.Stop();
					this.tmCloseSplash = null;
				}
				if (this.timerSwitchAccount != null)
				{
					this.timerSwitchAccount.Stop();
					this.timerSwitchAccount = null;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("DisposeMe", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnSendOrder_Click(object sender, EventArgs e)
		{
			try
			{
				if (ApplicationInfo.AccInfo.UserInternetInBroker == "Y")
				{
					this.ShowMessageInFormConfirm("Internal user not authenrize!", frmOrderFormConfirm.OpenStyle.ShowBox);
				}
				else
				{
					this.VerifyParam();
					if (this._verifyParam)
					{
						if (this.chbEqStopOrder.Checked)
						{
							this._commandType = "T";
							StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[this._OrdSymbol];
							if (stockInformation.Number > 0)
							{
								string text = string.Empty;
								if (this._stopField == 1 || this._stopField == 4 || this._stopField == 5 || this._stopField == 6)
								{
									text = "Last";
								}
								else
								{
									text = "Unknow";
								}
								if (this._stopOperator == 1)
								{
									text += " >= ";
								}
								else
								{
									if (this._stopOperator == 2)
									{
										text += " <= ";
									}
									else
									{
										if (this._stopOperator == 3)
										{
											text += " > ";
										}
										else
										{
											if (this._stopOperator == 4)
											{
												text += " < ";
											}
										}
									}
								}
								if (this._stopField == 4)
								{
									object obj = text;
									text = string.Concat(new object[]
									{
										obj,
										"SMA (",
										this._stopPrice,
										")"
									});
								}
								else
								{
									if (this._stopField == 5)
									{
										object obj = text;
										text = string.Concat(new object[]
										{
											obj,
											"Break High (",
											this._stopPrice,
											")"
										});
									}
									else
									{
										if (this._stopField == 6)
										{
											object obj = text;
											text = string.Concat(new object[]
											{
												obj,
												"Break Low (",
												this._stopPrice,
												")"
											});
										}
										else
										{
											text += this._stopPrice;
										}
									}
								}
								this._retOrderMessage = string.Concat(new string[]
								{
									"Auto Trade :",
									" Account : ",
									ApplicationInfo.AccInfo.CurrentAccount,
									"\n",
									Utilities.GetOrderSideName(this._OrdSide),
									" : ‘",
									this._OrdSymbol,
									"’",
									"\nVolume : ",
									FormatUtil.VolumeFormat(this._OrdVolume, true),
									"\nPrice : ",
									this._OrdPrice,
									(this._OrdTtf != 0) ? (" ,Trustee Id " + this._OrdTtf) : "",
									"\nCondition : ",
									text
								});
								ApplicationInfo.UserPincodeLastEntry = this.tbPin.Text.Trim();
								this.ShowOrderFormConfirm("Confirm to send?" + ((this._currTimes + 1 <= this._OrdTimes) ? string.Concat(new object[]
								{
									" *** Times : ",
									this._currTimes,
									"/",
									this._OrdTimes
								}) : ""), this._retOrderMessage, "", "");
							}
							else
							{
								this.ShowMessageInFormConfirm("Invalid Stock symbol '" + this._OrdSymbol + "'", frmOrderFormConfirm.OpenStyle.Error);
							}
						}
						else
						{
							if (!this.bgwSendOrder.IsBusy)
							{
								this._commandType = "V";
								this.btnSendOrder.Enabled = false;
								this.bgwSendOrder.RunWorkerAsync();
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("btnSendOrder_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnClear_Click(object sender, EventArgs e)
		{
			try
			{
				if (this._frmConfirm != null)
				{
					this._frmConfirm.CloseMe();
				}
				this.DoClear();
				this.cbStock.Focus();
			}
			catch (Exception ex)
			{
				this.ShowError("btnClear_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbPincodeForNewOrder_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Right)
			{
				this.btnSendOrder.Focus();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbVolume_TextChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.tbVolume.Text.Trim() != string.Empty)
				{
					if (FormatUtil.Isnumeric(this.tbVolume.Text))
					{
						try
						{
							decimal num = Convert.ToInt64(this.tbVolume.Text.Replace(",", ""));
							this.tbVolume.Text = num.ToString("#,###");
							this.tbVolume.Select(this.tbVolume.Text.Length, 0);
							this._isLockPubVol = true;
							this.tbPublic.Text = this.tbVolume.Text;
						}
						catch
						{
							this.tbVolume.Text = this.tbVolume.Text.Substring(0, this.tbVolume.Text.Length - 1);
						}
					}
					else
					{
						this.tbVolume.Text = this.tbVolume.Text.Substring(0, this.tbVolume.Text.Length - 1);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tbVolume_TextChanged", ex);
			}
			this._isLockPubVol = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbVolume_DragDrop(object sender, DragEventArgs e)
		{
			DragItemData dragItemData = (DragItemData)e.Data.GetData(typeof(DragItemData).ToString());
			this.tbVolume.Text = dragItemData.DragText;
			this.tbVolume.Focus();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbVolume_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Move;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbPrice_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Move;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbPrice_DragDrop(object sender, DragEventArgs e)
		{
			DragItemData dragItemData = (DragItemData)e.Data.GetData(typeof(DragItemData).ToString());
			this.cbPrice.Text = dragItemData.DragText;
			this.cbPrice.Focus();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReceiveMessage(IBroadcastMessage message, StockList.StockInformation realtimeStockInfo)
		{
			try
			{
				string messageType = message.MessageType;
				if (messageType != null)
				{
					if (!(messageType == "0G"))
					{
						if (!(messageType == "0B"))
						{
							if (!(messageType == "0I"))
							{
								if (messageType == "SC")
								{
									if (ApplicationInfo.MarketState == "R")
									{
										if (this._frmConfirm != null)
										{
											this._frmConfirm.CloseMe();
										}
									}
								}
							}
							else
							{
								OrderInfoClient orderInfoClient = (OrderInfoClient)message;
								if (orderInfoClient.Account == ApplicationInfo.AccInfo.CurrentAccount)
								{
									this.StartTimerLoadCredit();
									ApplicationInfo.RemoveOrderNoFromAutoRefreshList(orderInfoClient.OrderNumber.ToString(), orderInfoClient.Reserve2);
									if (ApplicationInfo.SupportFreewill && (orderInfoClient.Reserve2 == "R" + this._returnOrderNumberFromServer.ToString() || orderInfoClient.Reserve2 == "OFFLINE"))
									{
										this._returnOrderNumberFromServer = -1L;
										if (ApplicationInfo.SupportOrderTimes)
										{
											if (this._currTimes + 1 <= this._OrdTimes)
											{
												this._currTimes++;
												Thread thread = new Thread(new ThreadStart(this.threadSendTimes));
												thread.Start();
											}
											else
											{
												if (this._frmConfirm != null)
												{
													this._frmConfirm.CloseMe();
												}
											}
										}
										else
										{
											if (this._frmConfirm != null)
											{
												this._frmConfirm.CloseMe();
											}
										}
									}
								}
							}
						}
						else
						{
							if (ApplicationInfo.SupportFreewill)
							{
								BroadCastOrderMessageClient broadCastOrderMessageClient = (BroadCastOrderMessageClient)message;
								if (ApplicationInfo.CanReceiveMessage(broadCastOrderMessageClient.EntryID))
								{
									ApplicationInfo.RemoveOrderNoFromAutoRefreshList("", broadCastOrderMessageClient.Reserve2);
									this.ShowMessageInFormConfirm(broadCastOrderMessageClient.Content, frmOrderFormConfirm.OpenStyle.Error);
								}
							}
						}
					}
					else
					{
						this.ReceiveDGWReplyMessage(message);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SendOrder:ReceiveMessage", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReceiveTfexMessage(IBroadcastMessage message, SeriesList.SeriesInformation realtimeSeriesInfo)
		{
			try
			{
				string messageType = message.MessageType;
				if (messageType != null)
				{
					if (messageType == "#T9I")
					{
						OrderTFEXInfoClient orderTFEXInfoClient = (OrderTFEXInfoClient)message;
						if (orderTFEXInfoClient.Account == ApplicationInfo.AccInfo.CurrentAccount)
						{
							this.StartTimerLoadCredit();
							ApplicationInfo.RemoveOrderNoFromAutoRefreshList_TFEX(orderTFEXInfoClient.OrderNumber.ToString());
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SendOrder:ReceiveTFEXMessage", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void StartTimerLoadCredit()
		{
			try
			{
				if (base.InvokeRequired)
				{
					base.Invoke(new MethodInvoker(this.StartTimerLoadCredit));
				}
				else
				{
					if (this.timerReloadCredit == null)
					{
						this.timerReloadCredit = new System.Windows.Forms.Timer();
						this.timerReloadCredit.Interval = 1000;
						this.timerReloadCredit.Tick -= new EventHandler(this.timerReloadCredit_Tick);
						this.timerReloadCredit.Tick += new EventHandler(this.timerReloadCredit_Tick);
					}
					this.timerReloadCredit.Enabled = false;
					this.timerReloadCredit.Enabled = true;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("StartTimerLoadCredit", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void timerReloadCredit_Tick(object sender, EventArgs e)
		{
			try
			{
				this.timerReloadCredit.Enabled = false;
				if (!this.bgwReloadCredit.IsBusy)
				{
					this.bgwReloadCredit.RunWorkerAsync();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("timerReloadCredit_Tick", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwReloadCredit_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				if (!string.IsNullOrEmpty(ApplicationInfo.AccInfo.CurrentAccount))
				{
					string text = string.Empty;
					if (ApplicationInfo.IsEquityAccount)
					{
						if (ApplicationInfo.SupportFreewill)
						{
							text = ApplicationInfo.WebOrderService.ViewCustomerCreditOnSendBox_Freewill(ApplicationInfo.AccInfo.CurrentAccount, ApplicationInfo.AccInfo.CurrentAccountType, this._stockInfo.Symbol);
						}
						else
						{
							text = ApplicationInfo.WebOrderService.ViewCustomerCreditOnSendBox(ApplicationInfo.AccInfo.CurrentAccount, this._stockInfo.Symbol);
						}
					}
					else
					{
						text = ApplicationInfo.WebServiceTFEX.ViewCustomersCredit(ApplicationInfo.AccInfo.CurrentAccount);
					}
					if (this.tdsCredit != null)
					{
						this.tdsCredit.Clear();
						this.tdsCredit = null;
					}
					this.tdsCredit = new DataSet();
					if (!string.IsNullOrEmpty(text))
					{
						MyDataHelper.StringToDataSet(text, this.tdsCredit);
					}
				}
				else
				{
					this.ShowError("RequestWebData", new Exception("Current Account is null"));
				}
			}
			catch (Exception ex)
			{
				this.ShowError("bgwReloadData_DoWork", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwReloadCredit_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				if (ApplicationInfo.IsEquityAccount)
				{
					this._creditType = 0;
					this._buyCreditLimit = 0m;
					this._totalCreditLimit = 0m;
					if (this.tdsCredit != null && this.tdsCredit.Tables.Contains("CUSTOMER_CREDIT") && this.tdsCredit.Tables["CUSTOMER_CREDIT"].Rows.Count > 0)
					{
						DataRow dataRow = this.tdsCredit.Tables["CUSTOMER_CREDIT"].Rows[0];
						int.TryParse(dataRow["credit_type"].ToString(), out this._creditType);
						if (dataRow.Table.Columns.Contains("buy_credit_limit"))
						{
							decimal.TryParse(dataRow["buy_credit_limit"].ToString(), out this._buyCreditLimit);
						}
						if (dataRow.Table.Columns.Contains("buy_credit_limit"))
						{
							decimal.TryParse(dataRow["buy_credit_limit"].ToString(), out this._totalCreditLimit);
						}
					}
					else
					{
						this._creditType = 0;
						this._buyCreditLimit = 0m;
						this._totalCreditLimit = 0m;
					}
					this.ShowCreditValue();
				}
				else
				{
					if (this.tdsCredit != null && this.tdsCredit.Tables.Contains("ITDS_Get_Cust_Info") && this.tdsCredit.Tables["ITDS_Get_Cust_Info"].Rows.Count > 0)
					{
						DataRow dataRow2 = this.tdsCredit.Tables["ITDS_Get_Cust_Info"].Rows[0];
						this.tbBuyLimit.Text = Utilities.PriceFormat(dataRow2["BuyLimit"].ToString(), 0, "0");
						this.tbOnHand.Text = Utilities.PriceFormat(dataRow2["EE"].ToString(), 0, "0");
						this.tbEquity.Text = Utilities.PriceFormat(dataRow2["LiquidationValue"].ToString(), 0, "0");
					}
					else
					{
						this.tbBuyLimit.Text = string.Empty;
						this.tbOnHand.Text = string.Empty;
						this.tbEquity.Text = string.Empty;
					}
					this.tbBuyLimit.Left = this.lbBuyLimit.Right + 2;
					this.lbOnHand.Left = this.tbBuyLimit.Right + 10;
					this.tbOnHand.Left = this.lbOnHand.Right + 2;
					this.lbEquity.Left = this.tbOnHand.Right + 10;
					this.tbEquity.Left = this.lbEquity.Right + 2;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("bgwReloadData_RunWorkerCompleted", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ReceiveDGWReplyMessage(IBroadcastMessage message)
		{
			try
			{
				DGWOrderReplyMessage dGWOrderReplyMessage = (DGWOrderReplyMessage)message;
				if (dGWOrderReplyMessage.OrderNumber == this._returnOrderNumberFromServer)
				{
					this.ShowMessageInFormConfirm(string.Concat(new object[]
					{
						"Reject Order Number ",
						dGWOrderReplyMessage.OrderNumber,
						"\nReject Code : ",
						dGWOrderReplyMessage.ReplyCode,
						"\nReject Description : ",
						dGWOrderReplyMessage.ReplyMessage
					}), frmOrderFormConfirm.OpenStyle.ShowBox);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("ReceiveDGWReplyMessage", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowMessageInFormConfirm(string message, frmOrderFormConfirm.OpenStyle openStyle, object lastObject)
		{
			this._objLastActive = lastObject;
			this.ShowMessageInFormConfirm(message, openStyle);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowMessageInFormConfirm(string message, frmOrderFormConfirm.OpenStyle openStyle)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new ucSendNewOrder.ShowMessageInFormConfirmCallBack(this.ShowMessageInFormConfirm), new object[]
				{
					message,
					openStyle
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
							this._frmConfirm.FormClosing -= new FormClosingEventHandler(this.frmConfirm_FormClosing);
							this._frmConfirm.Dispose();
						}
						this._frmConfirm = null;
					}
					this._frmConfirm = new frmOrderFormConfirm(message, openStyle);
					this._frmConfirm.FormClosing -= new FormClosingEventHandler(this.frmConfirm_FormClosing);
					this._frmConfirm.FormClosing += new FormClosingEventHandler(this.frmConfirm_FormClosing);
					this._frmConfirm.TopLevel = false;
					this._frmConfirm.Parent = base.Parent.Parent;
					this._frmConfirm.Location = new Point((this._frmConfirm.Parent.Width - this._frmConfirm.Width) / 2, (this._frmConfirm.Parent.Height - this._frmConfirm.Height) / 2);
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
		private void ShowOrderFormConfirm(string message, string orderParam, string ossWarning, string stockTH)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new ucSendNewOrder.ShowOrderFormConfirmCallBack(this.ShowOrderFormConfirm), new object[]
				{
					message,
					orderParam,
					ossWarning,
					stockTH
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
							this._frmConfirm.FormClosing -= new FormClosingEventHandler(this.frmConfirm_FormClosing);
							this._frmConfirm.Dispose();
						}
						this._frmConfirm = null;
					}
					this._frmConfirm = new frmOrderFormConfirm(message, frmOrderFormConfirm.OpenStyle.ConfirmSendNew);
					this._frmConfirm.FormClosing -= new FormClosingEventHandler(this.frmConfirm_FormClosing);
					this._frmConfirm.FormClosing += new FormClosingEventHandler(this.frmConfirm_FormClosing);
					this._frmConfirm.TopLevel = false;
					this._frmConfirm.OssMessage = ossWarning;
					this._frmConfirm.StockThreshold = stockTH;
					this._frmConfirm.OrderParam = orderParam;
					this._frmConfirm.Parent = base.Parent.Parent;
					this._frmConfirm.Location = new Point((this._frmConfirm.Parent.Width - this._frmConfirm.Width) / 2, (this._frmConfirm.Parent.Height - this._frmConfirm.Height) / 2);
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
		private void ShowStopDisclaimer()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new ucSendNewOrder.ShowStopDisclaimerCallBack(this.ShowStopDisclaimer));
			}
			else
			{
				try
				{
					if (this._frmStopDisclaimer != null)
					{
						if (!this._frmStopDisclaimer.IsDisposed)
						{
							this._frmStopDisclaimer.Dispose();
						}
						this._frmStopDisclaimer = null;
					}
					this._frmStopDisclaimer = new frmStopDisclaimer();
					this._frmStopDisclaimer.FormClosing -= new FormClosingEventHandler(this.frmStopDisclaimer_FormClosing);
					this._frmStopDisclaimer.FormClosing += new FormClosingEventHandler(this.frmStopDisclaimer_FormClosing);
					this._frmStopDisclaimer.TopLevel = false;
					this._frmStopDisclaimer.Parent = base.Parent.Parent;
					this._frmStopDisclaimer.Location = new Point((this._frmStopDisclaimer.Parent.Width - this._frmStopDisclaimer.Width) / 2, (this._frmStopDisclaimer.Parent.Height - this._frmStopDisclaimer.Height) / 2);
					this._frmStopDisclaimer.TopMost = true;
					this._frmStopDisclaimer.Show();
					this._frmStopDisclaimer.BringToFront();
				}
				catch (Exception ex)
				{
					this.ShowError("ShowStopDisclaimer", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmStopDisclaimer_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				base.Focus();
				if (ApplicationInfo.StopOrderAccepted)
				{
					if (this._isActive)
					{
						this.SetResize();
						if (this._OnBoxStyleChanged != null)
						{
							this._OnBoxStyleChanged();
						}
					}
				}
				this.chbEqStopOrder.Checked = ApplicationInfo.StopOrderAccepted;
			}
			catch (Exception ex)
			{
				this.ShowError("ConfirmForm", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void threadSendTimes()
		{
			if (this._currTimes <= this._OrdTimes)
			{
				string text = string.Concat(new object[]
				{
					this._retOrderMessage,
					"\n Times : ",
					this._currTimes,
					"/",
					this._OrdTimes
				});
				this.ShowOrderFormConfirm(string.Concat(new object[]
				{
					"Confirm to send?  *** Times : ",
					this._currTimes,
					"/",
					this._OrdTimes
				}), this._retOrderMessage, "", "");
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmConfirm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				base.Focus();
				frmOrderFormConfirm.OpenStyle openFormStyle = ((frmOrderFormConfirm)sender).OpenFormStyle;
				DialogResult result = ((frmOrderFormConfirm)sender).Result;
				if (ApplicationInfo.IsEquityAccount)
				{
					if (openFormStyle == frmOrderFormConfirm.OpenStyle.ConfirmSendNew)
					{
						if (result == DialogResult.OK)
						{
							if (!this.bgwSendOrder.IsBusy)
							{
								if (this._commandType == "V")
								{
									this._commandType = "S";
								}
								this.bgwSendOrder.RunWorkerAsync();
							}
							else
							{
								this.ShowMessageInFormConfirm("The system is not ready yet.", frmOrderFormConfirm.OpenStyle.Error);
							}
						}
						else
						{
							this._OrdTimes = 0;
							this._returnOrderNumberFromServer = -1L;
							this.DoClear();
						}
					}
					else
					{
						this.btnSendOrder.Enabled = true;
						this.SetLastActiveObject();
					}
				}
				else
				{
					if (openFormStyle == frmOrderFormConfirm.OpenStyle.ConfirmSendNew)
					{
						if (result == DialogResult.OK)
						{
							if (!this.bgwSendOrder.IsBusy)
							{
								this._commandType = "S";
								this.bgwSendOrder.RunWorkerAsync();
							}
						}
						else
						{
							this.tbPriceT.Focus();
						}
					}
					else
					{
						this.SetLastActiveObject();
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("ConfirmForm", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetLastActiveObject()
		{
			if (this._objLastActive != null)
			{
				if (this._objLastActive == this.cbStock)
				{
					this.cbStock.Focus();
				}
				else
				{
					if (this._objLastActive == this.cbPrice)
					{
						this.cbPrice.Focus();
					}
					else
					{
						if (this._objLastActive == this.tbVolume)
						{
							this.tbVolume.Focus();
						}
						else
						{
							if (this._objLastActive == this.tbPublic)
							{
								this.tbPublic.Focus();
							}
							else
							{
								if (this._objLastActive == this.cbCondition)
								{
									this.cbCondition.Focus();
								}
								else
								{
									if (this._objLastActive == this.tbPin)
									{
										this.tbPin.Focus();
									}
								}
							}
						}
					}
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowCreditValue()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(this.ShowCreditValue));
			}
			else
			{
				try
				{
					string str = string.Empty;
					string text = string.Empty;
					switch (this._creditType)
					{
					case 0:
					case 1:
					case 2:
					case 4:
					case 5:
					case 6:
					case 7:
					case 8:
						if (ApplicationInfo.AccInfo.CurrentAccountType == "C" || ApplicationInfo.AccInfo.CurrentAccountType == "H" || ApplicationInfo.AccInfo.CurrentAccountType == "")
						{
							str = "Buy Limit";
							text = this._buyCreditLimit.ToString("#,##0");
						}
						else
						{
							str = "PP";
							if (ApplicationInfo.SupportFreewill)
							{
								text = this._buyCreditLimit.ToString("#,##0");
							}
							else
							{
								text = (this._buyCreditLimit / ApplicationInfo.UserMarginRate * 100m).ToString("#,##0");
							}
						}
						break;
					case 3:
						str = "Total Limit";
						text = this._totalCreditLimit.ToString("#,##0");
						break;
					}
					this.lbBuyLimit.Text = str + " : ";
					this.tbBuyLimit.Left = this.lbBuyLimit.Left + this.lbBuyLimit.Width;
					this.tbBuyLimit.Text = text;
					this.lbOnHand.Left = this.tbBuyLimit.Left + this.tbBuyLimit.Width + 10;
					this.tbOnHand.Left = this.lbOnHand.Left + this.lbOnHand.Width;
					this.tbOnHand.Text = this.GetOnHand().ToString("#,##0");
				}
				catch (Exception ex)
				{
					this.ShowError("ShowCreditValue", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private long GetOnHand()
		{
			long result = 0L;
			if (this.tdsCredit != null && this.tdsCredit.Tables.Contains("STOCK_POSITION") && this.tdsCredit.Tables["STOCK_POSITION"].Rows.Count > 0)
			{
				string b = "0";
				if (this.chbNVDR.Checked)
				{
					b = "2";
				}
				string b2 = string.Empty;
				if (this.cbDepCollateral.Text == "Deposit")
				{
					b2 = "D";
				}
				else
				{
					if (this.cbDepCollateral.Text == "Collateral")
					{
						b2 = "C";
					}
					else
					{
						if (this.rbShort.Checked)
						{
							b2 = "B";
						}
						else
						{
							if (this.rbCover.Checked)
							{
								b2 = "S";
							}
						}
					}
				}
				foreach (DataRow dataRow in this.tdsCredit.Tables["STOCK_POSITION"].Rows)
				{
					if (dataRow["trustee_id"].ToString().Trim() == b && dataRow["position_type"].ToString().Trim() == b2)
					{
						long.TryParse(dataRow["onhand"].ToString(), out result);
						break;
					}
				}
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void DoClear()
		{
			try
			{
				if (!Settings.Default.BSBoxDefaultStock)
				{
					this.cbStock.Text = string.Empty;
				}
				this.cbPrice.Text = string.Empty;
				this.chbNVDR.Checked = false;
				this.tbVolume.Text = string.Empty;
				this.tbTimes.Text = string.Empty;
				this.tbPublic.Text = string.Empty;
				this.cbCondition.Text = string.Empty;
				this.cbDepCollateral.Text = "";
				this.cbStopOrderField.Text = string.Empty;
				this.cbStopOrderPrice.Text = string.Empty;
				this.chbLimit.Checked = false;
				this.chbEqStopOrder.Checked = false;
				if (Settings.Default.MainBottomStyle == 3 || Settings.Default.MainBottomStyle == 2)
				{
					if (Settings.Default.BSBoxSavePincode)
					{
						this.tbPin.Text = ApplicationInfo.UserPincodeLastEntry;
					}
					else
					{
						this.tbPin.Text = string.Empty;
					}
				}
				this.btnSendOrder.Enabled = true;
			}
			catch (Exception ex)
			{
				this.ShowError("DoClear", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void DoClear_TFEX()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(this.DoClear_TFEX));
			}
			else
			{
				try
				{
					if (!Settings.Default.BSBoxDefaultStock)
					{
						this.tbSeries.Text = string.Empty;
					}
					this.tbVolumeT.Clear();
					this.tbPriceT.Clear();
					this.tbPublishT.Clear();
					this.cbPosition.Text = "OPEN";
					this.cbType.Text = "Limit";
					this.cbValidity.Text = "DAY";
					this.chbTfexStopOrder.Checked = false;
					this.tbSeriesCondition.Clear();
					this.cbTfexConStopOrder.SelectedIndex = 0;
					this.tbTfexPriceCondition.Clear();
					this.btnSendOrderT.Enabled = true;
				}
				catch (Exception ex)
				{
					this.ShowError("DoClear_TFEX", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool DoSend_TFEX(string commandType)
		{
			bool flag = false;
			bool result;
			try
			{
				if (!ApplicationInfo.AccInfo.IsAccCanTrade(ApplicationInfo.AccInfo.CurrentAccount))
				{
					this.ShowMessageInFormConfirm(ApplicationInfo.AccInfo.CurrentAccount + " is not allowed to long / short.", frmOrderFormConfirm.OpenStyle.ShowBox);
					result = false;
					return result;
				}
				try
				{
					SeriesList.SeriesInformation seriesInformation = null;
					this._OrdSide = string.Empty;
					this._OrdSymbol = string.Empty;
					this._OrdVolume = 0L;
					this._OrdPriceType = string.Empty;
					this._OrdPrice = string.Empty;
					this._OrdPubVol = 0L;
					this._OrdCondition = string.Empty;
					this._OrdPosition = string.Empty;
					this._OrdValidityDate = string.Empty;
					this._OrdTfexStopSeries = string.Empty;
					this._OrdTfexStopCond = string.Empty;
					this._OrdTfexStopPrice = string.Empty;
					if (this.rdbTfexBuy.Checked)
					{
						this._OrdSide = "L";
					}
					else
					{
						if (!this.rdbTfexSell.Checked)
						{
							this.ShowMessageInFormConfirm("Invalid Side!.", frmOrderFormConfirm.OpenStyle.ShowBox, this.tbSeries);
							result = false;
							return result;
						}
						this._OrdSide = "S";
					}
					if (ApplicationInfo.SeriesInfo.ItemsKeySymbol.ContainsKey(this.tbSeries.Text.ToUpper().Trim()))
					{
						seriesInformation = ApplicationInfo.SeriesInfo[this.tbSeries.Text.ToUpper().Trim()];
					}
					if (string.IsNullOrEmpty(this.tbSeries.Text.ToUpper().Trim()) || seriesInformation == null || seriesInformation.Group == 5)
					{
						this.ShowMessageInFormConfirm("Invalid Series Symbol!.", frmOrderFormConfirm.OpenStyle.ShowBox, this.tbSeries);
						this.tbSeries.Focus();
						result = false;
						return result;
					}
					this._OrdSymbol = this.tbSeries.Text.ToUpper().Trim();
					seriesInformation = null;
					this._OrdVolume = 0L;
					if (!FormatUtil.Isnumeric(this.tbVolumeT.Text.Trim()))
					{
						this.ShowMessageInFormConfirm("Invalid volume.", frmOrderFormConfirm.OpenStyle.ShowBox, this.tbVolumeT);
						result = false;
						return result;
					}
					try
					{
						this._OrdVolume = Convert.ToInt64(this.tbVolumeT.Text.Replace(",", ""));
						if (this._OrdVolume <= 0L)
						{
							this.ShowMessageInFormConfirm("Invalid Volume [More than Zero]!", frmOrderFormConfirm.OpenStyle.ShowBox, this.tbVolumeT);
							result = false;
							return result;
						}
					}
					catch
					{
						this.ShowMessageInFormConfirm("Invalid volume.", frmOrderFormConfirm.OpenStyle.ShowBox, this.tbVolumeT);
						result = false;
						return result;
					}
					if (this.cbType.Text.ToLower() == "limit")
					{
						this._OrdPriceType = "L";
						this._OrdPrice = this.tbPriceT.Text.ToUpper().Trim();
						if (!this.IsValidPrice_TFEX(true, this._OrdPrice))
						{
							result = false;
							return result;
						}
					}
					else
					{
						if (this.cbType.Text.ToLower() == "mp")
						{
							this._OrdPrice = "-1";
							this._OrdPriceType = "P";
						}
						else
						{
							if (this.cbType.Text.ToLower() == "mo")
							{
								this._OrdPrice = "-1";
								this._OrdPriceType = "M";
							}
							else
							{
								if (!(this.cbType.Text.ToLower() == "ml"))
								{
									this.ShowMessageInFormConfirm("Invalid Type", frmOrderFormConfirm.OpenStyle.ShowBox, this.cbType);
									result = false;
									return result;
								}
								this._OrdPrice = "-1";
								this._OrdPriceType = "T";
							}
						}
					}
					this._OrdPubVol = 0L;
					if (!FormatUtil.Isnumeric(this.tbPublishT.Text.Trim()))
					{
						this.ShowMessageInFormConfirm("Invalid public volume", frmOrderFormConfirm.OpenStyle.ShowBox, this.tbPublishT);
						result = false;
						return result;
					}
					try
					{
						this._OrdPubVol = Convert.ToInt64(this.tbPublishT.Text.Replace(",", ""));
					}
					catch
					{
						this.ShowMessageInFormConfirm("Invalid public volume", frmOrderFormConfirm.OpenStyle.ShowBox, this.tbPublishT);
						result = false;
						return result;
					}
					if (this._OrdPubVol == this._OrdVolume)
					{
						this._OrdPubVol = 0L;
					}
					string text = this.cbValidity.Text.Trim();
					if (text != null)
					{
						if (text == "DAY" || text == "FAK" || text == "FOK" || text == "")
						{
							this._OrdCondition = this.cbValidity.Text.Trim();
							goto IL_555;
						}
					}
					this.ShowMessageInFormConfirm("Invalid Condition!", frmOrderFormConfirm.OpenStyle.ShowBox, this.cbValidity);
					IL_555:
					if (this.cbPosition.Text.ToUpper() == "CLOSE")
					{
						this._OrdPosition = "C";
					}
					else
					{
						this._OrdPosition = "O";
					}
					this._OrdValidityDate = DateTime.Now.ToString("yyyyMMdd");
					if (this.chbTfexStopOrder.Checked)
					{
						this._OrdTfexStopSeries = this.tbSeriesCondition.Text.Trim();
						if (ApplicationInfo.SeriesInfo.ItemsKeySymbol.ContainsKey(this._OrdTfexStopSeries))
						{
							seriesInformation = ApplicationInfo.SeriesInfo[this._OrdTfexStopSeries];
						}
						if (string.IsNullOrEmpty(this._OrdTfexStopSeries) || seriesInformation == null || seriesInformation.Group == 5)
						{
							this.ShowMessageInFormConfirm("Invalid Stop Series!.", frmOrderFormConfirm.OpenStyle.ShowBox, this.tbSeries);
							result = false;
							return result;
						}
						this._OrdTfexStopCond = this.cbTfexConStopOrder.Text.Trim();
						if (!(this.tbTfexPriceCondition.Text.ToUpper().Trim() != "PRICE"))
						{
							this.ShowMessageInFormConfirm("Invalid Stop Price!.", frmOrderFormConfirm.OpenStyle.ShowBox, this.tbTfexPriceCondition);
							result = false;
							return result;
						}
						this._OrdTfexStopPrice = this.tbTfexPriceCondition.Text.Trim();
						if (!this.IsValidPrice_TFEX(false, this._OrdTfexStopPrice))
						{
							this.ShowMessageInFormConfirm("Invalid Stop Price!.", frmOrderFormConfirm.OpenStyle.ShowBox, this.tbSeries);
							result = false;
							return result;
						}
						if (this._OrdTfexStopCond == "Bid >=")
						{
							this._OrdTfexStopCond = "1";
						}
						else
						{
							if (this._OrdTfexStopCond == "Bid <=")
							{
								this._OrdTfexStopCond = "2";
							}
							else
							{
								if (this._OrdTfexStopCond == "Ask >=")
								{
									this._OrdTfexStopCond = "3";
								}
								else
								{
									if (this._OrdTfexStopCond == "Ask <=")
									{
										this._OrdTfexStopCond = "4";
									}
									else
									{
										if (this._OrdTfexStopCond == "Last >=")
										{
											this._OrdTfexStopCond = "5";
										}
										else
										{
											if (this._OrdTfexStopCond == "Last <=")
											{
												this._OrdTfexStopCond = "6";
											}
										}
									}
								}
							}
						}
						seriesInformation = null;
					}
					if (string.IsNullOrEmpty(ApplicationInfo.AccInfo.CurrentAccount))
					{
						this.ShowMessageInFormConfirm("Invalid Account!", frmOrderFormConfirm.OpenStyle.ShowBox);
						result = false;
						return result;
					}
					this._commandType = commandType;
					string text2 = string.Empty;
					if (this._OrdSide == "L")
					{
						text2 = "Long";
					}
					else
					{
						if (this._OrdSide == "S")
						{
							text2 = "Short";
						}
					}
					string orderParam = string.Concat(new object[]
					{
						text2,
						" ",
						this._OrdSymbol,
						" , Volume ",
						this._OrdVolume,
						"  ,Price ",
						(this._OrdPrice == "-1") ? "Market" : this._OrdPrice,
						" , Account ",
						ApplicationInfo.AccInfo.CurrentAccount
					});
					this.ShowOrderFormConfirm("Confirm to send?", orderParam, "", "");
				}
				catch (Exception ex)
				{
					this.ShowError("DoSend_TFEX", ex);
				}
			}
			catch (Exception ex2)
			{
				this.ShowError("DoSend_TFEX", ex2);
			}
			result = flag;
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void VerifyParam()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(this.VerifyParam));
			}
			else
			{
				this._verifyParam = false;
				if (ApplicationInfo.AccInfo.IsAccCanTrade(ApplicationInfo.AccInfo.CurrentAccount))
				{
					this._OrdSymbol = this.cbStock.Text.ToUpper().Trim();
					if (this.chbNVDR.Checked)
					{
						this._OrdTtf = 2;
					}
					else
					{
						this._OrdTtf = 0;
					}
					this._OrdSide = this._showSide;
					this._OrdCondition = this.cbCondition.Text.Trim();
					if (ApplicationInfo.UserLoginMode == "I")
					{
						this.ShowMessageInFormConfirm("Invalid Login Type", frmOrderFormConfirm.OpenStyle.ShowBox);
					}
					else
					{
						this._OrdIsDeposit = "";
						if (ApplicationInfo.AccInfo.CurrentAccountType == "B")
						{
							if (this.cbDepCollateral.Text.ToString() == "Deposit")
							{
								this._OrdIsDeposit = "D";
							}
							else
							{
								if (this.cbDepCollateral.Text.ToString() == "Collateral")
								{
									this._OrdIsDeposit = "C";
								}
							}
						}
						if (string.IsNullOrEmpty(this._OrdSide))
						{
							this.ShowMessageInFormConfirm("Invalid Side!.", frmOrderFormConfirm.OpenStyle.ShowBox, this.cbStock);
						}
						else
						{
							if (string.IsNullOrEmpty(this._OrdSymbol))
							{
								this.ShowMessageInFormConfirm("Invalid Stock Symbol!.", frmOrderFormConfirm.OpenStyle.ShowBox, this.cbStock);
								this.cbStock.Focus();
							}
							else
							{
								this._OrdVolume = 0L;
								try
								{
									this._OrdVolume = Convert.ToInt64(this.tbVolume.Text.Replace(",", ""));
									if (this._OrdVolume <= 0L)
									{
										this.ShowMessageInFormConfirm("Invalid Volume [More than Zero]!", frmOrderFormConfirm.OpenStyle.ShowBox, this.tbVolume);
										return;
									}
								}
								catch
								{
									this.ShowMessageInFormConfirm("Invalid volume.", frmOrderFormConfirm.OpenStyle.ShowBox, this.tbVolume);
									return;
								}
								if (this.IsValidPrice(this.cbPrice.Text, true))
								{
									this._OrdPrice = this.cbPrice.Text.ToUpper().Trim();
									switch (this._OrdTtf)
									{
									case 0:
									case 1:
									case 2:
									{
										this._OrdPubVol = 0L;
										string text;
										try
										{
											this._OrdPubVol = Convert.ToInt64(this.tbPublic.Text.Replace(",", ""));
											if (this._OrdPubVol > this._OrdVolume)
											{
												this.ShowMessageInFormConfirm("Published is Greater than Volume", frmOrderFormConfirm.OpenStyle.ShowBox, this.tbVolume);
												break;
											}
											if (this._OrdPubVol < this._OrdVolume)
											{
												text = this._OrdPrice;
												if (text != null)
												{
													if (text == "MP" || text == "ATO" || text == "ATC" || text == "MO" || text == "ML")
													{
														this.ShowMessageInFormConfirm("Price condition cannot use Published.", frmOrderFormConfirm.OpenStyle.ShowBox, this.tbVolume);
														break;
													}
												}
												text = this._OrdCondition;
												if (text != null)
												{
													if (text == "IOC" || text == "FOK")
													{
														this.ShowMessageInFormConfirm("Cannot use Published Volume with IOC, FOK", frmOrderFormConfirm.OpenStyle.ShowBox, this.tbVolume);
														break;
													}
												}
											}
										}
										catch
										{
											this.ShowMessageInFormConfirm("Invalid public volume", frmOrderFormConfirm.OpenStyle.ShowBox, this.tbPublic);
											break;
										}
										text = this._OrdCondition;
										if (text != null)
										{
											if (text == "IOC")
											{
												this._OrdCondition = "I";
												goto IL_4FE;
											}
											if (text == "FOK")
											{
												this._OrdCondition = "F";
												goto IL_4FE;
											}
											if (text == "")
											{
												if (ApplicationInfo.SupportFreewill)
												{
													if (this._OrdVolume < (long)this._stockInfo.BoardLot)
													{
														this._OrdCondition = "O";
													}
													else
													{
														this._OrdCondition = " ";
													}
												}
												else
												{
													this._OrdCondition = " ";
												}
												goto IL_4FE;
											}
										}
										this.ShowMessageInFormConfirm("Invalid Condition!", frmOrderFormConfirm.OpenStyle.ShowBox, this.cbCondition);
										IL_4FE:
										if (string.IsNullOrEmpty(ApplicationInfo.AccInfo.CurrentAccount))
										{
											this.ShowMessageInFormConfirm("Invalid Account!", frmOrderFormConfirm.OpenStyle.ShowBox);
										}
										else
										{
											this._OrdTimes = 0;
											if (ApplicationInfo.SupportOrderTimes)
											{
												int.TryParse(this.tbTimes.Text, out this._OrdTimes);
												if (this._OrdTimes <= -1 || this._OrdTimes >= 100)
												{
													this.ShowMessageInFormConfirm("Invalid times", frmOrderFormConfirm.OpenStyle.ShowBox);
													break;
												}
											}
											if (this.chbEqStopOrder.Checked)
											{
												if (this.cbStopOrderField.SelectedIndex == -1)
												{
													this.ShowMessageInFormConfirm("Auto Trade::\r\nInvalid field type!", frmOrderFormConfirm.OpenStyle.ShowBox);
													break;
												}
												if (this._OrdIsDeposit != string.Empty)
												{
													this.ShowMessageInFormConfirm("Auto Trade::\r\nDeposit not supported!", frmOrderFormConfirm.OpenStyle.ShowBox);
													break;
												}
												if (this._OrdTimes > 0)
												{
													this.ShowMessageInFormConfirm("Auto Trade::\r\nOrder Time not supported!", frmOrderFormConfirm.OpenStyle.ShowBox);
													break;
												}
												if (this.cbStopOrderField.Text.ToLower().IndexOf("sma") > 0)
												{
													this._stopField = 4;
												}
												else
												{
													if (this.cbStopOrderField.Text.ToLower().IndexOf("break high") > 0)
													{
														this._stopField = 5;
													}
													else
													{
														if (this.cbStopOrderField.Text.ToLower().IndexOf("break low") > 0)
														{
															this._stopField = 6;
														}
														else
														{
															this._stopField = 1;
														}
													}
												}
												if (this.cbStopOrderField.Text.IndexOf("SMA") > 0)
												{
													int num;
													int.TryParse(this.cbStopOrderPrice.Text, out num);
													if (num.ToString() != this.cbStopOrderPrice.Text.Trim())
													{
														this.ShowMessageInFormConfirm("Auto Trade::\r\nInvalid field type!", frmOrderFormConfirm.OpenStyle.ShowBox);
														break;
													}
													if ((num < 2 || num > 50) && num != 75 && num != 200)
													{
														this.ShowMessageInFormConfirm("Auto Trade::\r\nInvalid SMA Period![2-50, 75 and 200]", frmOrderFormConfirm.OpenStyle.ShowBox);
														break;
													}
												}
												if (this.cbStopOrderField.Text.IndexOf("Break") > 0)
												{
													int num;
													int.TryParse(this.cbStopOrderPrice.Text, out num);
													if (num.ToString() != this.cbStopOrderPrice.Text.Trim())
													{
														this.ShowMessageInFormConfirm("Auto Trade::\r\nInvalid field type!", frmOrderFormConfirm.OpenStyle.ShowBox);
														break;
													}
													if (num < 2 || num > 20)
													{
														this.ShowMessageInFormConfirm("Auto Trade\r\nInvalid Break High/Low Period![2-20]", frmOrderFormConfirm.OpenStyle.ShowBox);
														break;
													}
												}
												if (!FormatUtil.Isnumeric(this.cbStopOrderPrice.Text))
												{
													this.ShowMessageInFormConfirm("Auto Trade::\r\nInvalid field price!", frmOrderFormConfirm.OpenStyle.ShowBox);
													break;
												}
												if (this.cbStopOrderField.Text.IndexOf(">=") > 0)
												{
													this._stopOperator = 1;
												}
												else
												{
													if (this.cbStopOrderField.Text.IndexOf("<=") > 0)
													{
														this._stopOperator = 2;
													}
													else
													{
														if (this.cbStopOrderField.Text.IndexOf(">") > 0)
														{
															this._stopOperator = 3;
														}
														else
														{
															if (this.cbStopOrderField.Text.IndexOf("<") > 0)
															{
																this._stopOperator = 4;
															}
														}
													}
												}
												if (!this.IsValidPrice(this.cbStopOrderPrice.Text.ToUpper().Trim(), true))
												{
													break;
												}
												decimal.TryParse(this.cbStopOrderPrice.Text.ToUpper(), out this._stopPrice);
												this._stopLimit = (this.chbLimit.Checked ? 1 : 0);
											}
											this._verifyParam = true;
										}
										break;
									}
									default:
										this.ShowMessageInFormConfirm("Invalid Trustee Id.", frmOrderFormConfirm.OpenStyle.ShowBox);
										break;
									}
								}
							}
						}
					}
				}
				else
				{
					this.ShowMessageInFormConfirm(ApplicationInfo.AccInfo.CurrentAccount + " is not allowed to Buy / Sell.", frmOrderFormConfirm.OpenStyle.ShowBox);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwSendOrder_DoWork(object sender, DoWorkEventArgs e)
		{
			if (ApplicationInfo.IsEquityAccount)
			{
				try
				{
					this._verifyResultStr_Pin = string.Empty;
					this._verifyResult_Pin = false;
					if (Settings.Default.MainBottomStyle == 1)
					{
						this._verifyResult_Pin = true;
					}
					else
					{
						if (this.tbPin.Text.Trim() == string.Empty)
						{
							this._verifyResultStr_Pin = "Pincode is empty!!!";
							return;
						}
						if (ApplicationInfo.UserPincodeWrongCount < ApplicationInfo.UserMaxRetryPincode)
						{
							this.ShowSplash(true, "Check Pincode.", false);
							this._verifyResult_Pin = ApplicationInfo.VerifyPincode(this.tbPin.Text.Trim(), ref this._verifyResultStr_Pin);
							this.ShowSplash(false, "", false);
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowSplash(false, "", false);
					this.ShowError("CheckPin", ex);
					this._verifyResultStr_Pin = ex.Message;
				}
				this._verifyResult = false;
				if (this._verifyResult_Pin)
				{
					if (this._commandType == "V")
					{
						try
						{
							this.ShowSplash(true, "Verify New Order...", false);
							string data = string.Empty;
							if (ApplicationInfo.SupportFreewill)
							{
								data = ApplicationInfo.WebOrderService.VerifyOrderFw(this._OrdSymbol, this._OrdSide, this._OrdVolume, this._OrdPrice, this._OrdPubVol, this._OrdCondition);
							}
							else
							{
								data = ApplicationInfo.WebOrderService.VerifyOrder(this._OrdSymbol, this._OrdSide, this._OrdVolume, this._OrdPrice, ApplicationInfo.AccInfo.CurrentAccount, ApplicationInfo.IsRiskActive);
							}
							if (this._dsSendOrder == null)
							{
								this._dsSendOrder = new DataSet();
							}
							else
							{
								this._dsSendOrder.Clear();
							}
							MyDataHelper.StringToDataSet(data, this._dsSendOrder);
							this._verifyResult = true;
						}
						catch (Exception ex)
						{
							this.ShowMessageInFormConfirm(ex.Message, frmOrderFormConfirm.OpenStyle.ShowBox);
						}
						this.ShowSplash(false, "", false);
					}
					else
					{
						if (this._commandType == "S")
						{
							try
							{
								this.ShowSplash(true, "Sending New Order...", false);
								if (ApplicationInfo.SupportFreewill)
								{
									this.ShowMessageInFormConfirm("Please wait for confirmation.", frmOrderFormConfirm.OpenStyle.WaitingForm);
								}
								this._newOrderResult = ApplicationInfo.SendNewOrder(this._OrdSymbol, this._OrdSide, this._OrdVolume, this._OrdPrice, this._OrdPubVol, this._OrdCondition, this._OrdTtf, this._OrdIsDeposit);
								this._verifyResult = true;
							}
							catch (Exception ex)
							{
								this.ShowMessageInFormConfirm("SendNewOrder:" + ex.Message, frmOrderFormConfirm.OpenStyle.ShowBox);
							}
							this.ShowSplash(false, "", false);
						}
						else
						{
							if (this._commandType == "T")
							{
								try
								{
									this.ShowSplash(true, "Sending Auto Trade...", false);
									StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[this._OrdSymbol];
									string text = ApplicationInfo.WebAlertService.SendStopOrder(ApplicationInfo.UserLoginID, stockInformation.Number, this._stopField, this._stopOperator, this._stopPrice, ApplicationInfo.AccInfo.CurrentAccount, this._OrdSide, this._OrdTtf, this._OrdVolume, this._OrdPrice, this._OrdPubVol, this._OrdCondition, ApplicationInfo.AccInfo.CurrInternetUser, ApplicationInfo.KE_Session, ApplicationInfo.KE_LOCAL, this._stopLimit, ApplicationInfo.AuthenKey);
									if (this._dsSendOrder == null)
									{
										this._dsSendOrder = new DataSet();
									}
									else
									{
										this._dsSendOrder.Clear();
									}
									MyDataHelper.StringToDataSet(text, this._dsSendOrder);
									this._verifyResult = true;
								}
								catch (Exception ex)
								{
									this.ShowMessageInFormConfirm("SendStopOrder:" + ex.Message, frmOrderFormConfirm.OpenStyle.ShowBox);
								}
								this.ShowSplash(false, "", false);
							}
						}
					}
				}
			}
			else
			{
				if (this._commandType == "S")
				{
					try
					{
						this.ShowSplash(true, "Sending New Order...", false);
						string currentAccount = ApplicationInfo.AccInfo.CurrentAccount;
						string text = ApplicationInfo.WebServiceTFEX.SendTFEXNewOrder(this._OrdSymbol, this._OrdSide, this._OrdVolume, this._OrdPrice, currentAccount, this._OrdPubVol, this._OrdPosition, this._OrdTfexStopPrice, this._OrdTfexStopCond, this._OrdTfexStopSeries, this._OrdCondition, this._OrdValidityDate, ApplicationInfo.UserSessionID, "", "", ApplicationInfo.AuthenKey, ApplicationInfo.AccInfo.InternetUserTFEX, ApplicationInfo.IP, ApplicationInfo.KE_Session, ApplicationInfo.KE_LOCAL, "S", this._OrdPriceType);
						if (text.Trim() == string.Empty)
						{
							this.ShowMessageInFormConfirm("Request fail , return empty!!!", frmOrderFormConfirm.OpenStyle.ShowBox);
						}
						else
						{
							if (text.Trim().ToUpper() == "INVALID_SESSION_KEY")
							{
								this.ShowMessageInFormConfirm("Invalid session key!!!", frmOrderFormConfirm.OpenStyle.ShowBox);
							}
							else
							{
								if (this._dsSendOrderTfex == null)
								{
									this._dsSendOrderTfex = new DataSet();
								}
								else
								{
									this._dsSendOrderTfex.Clear();
								}
								MyDataHelper.StringToDataSet(text, this._dsSendOrderTfex);
							}
						}
					}
					catch (Exception ex)
					{
						this.ShowMessageInFormConfirm(ex.Message, frmOrderFormConfirm.OpenStyle.ShowBox);
					}
					this.ShowSplash(false, "", false);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwSendOrder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Error == null)
			{
				if (ApplicationInfo.IsEquityAccount)
				{
					if (this._verifyResult_Pin)
					{
						if (this._verifyResult)
						{
							if (this._commandType == "V")
							{
								try
								{
									if (this._dsSendOrder != null && this._dsSendOrder.Tables.Contains("verify_result") && this._dsSendOrder.Tables["verify_result"].Rows.Count > 0)
									{
										int num = Convert.ToInt32(this._dsSendOrder.Tables["verify_result"].Rows[0]["result_code"]);
										string message = this._dsSendOrder.Tables["verify_result"].Rows[0]["result_message"].ToString();
										int num2 = 0;
										int.TryParse(this._dsSendOrder.Tables["verify_result"].Rows[0]["verify_code"].ToString(), out num2);
										if (num == 1)
										{
											StockList.StockInformation stockInfo = ApplicationInfo.StockInfo[this._OrdSymbol];
											this._retOrderMessage = string.Concat(new string[]
											{
												"Account : ",
												ApplicationInfo.AccInfo.CurrentAccount,
												"\n",
												Utilities.GetOrderSideName(this._OrdSide),
												" : ‘",
												this._OrdSymbol,
												"’",
												"\nVolume : ",
												FormatUtil.VolumeFormat(this._OrdVolume, true),
												"\nPrice : ",
												this._OrdPrice,
												(this._OrdTtf != 0) ? (" ,Trustee Id " + this._OrdTtf) : "",
												"\nTotal Amount :  ",
												this.CalculateValue(this._OrdVolume, this._OrdPrice, this._OrdSide, stockInfo),
												"   (Commission and VAT not included)"
											});
											if (ApplicationInfo.SupportOrderTimes && this._OrdTimes > 0)
											{
												this._currTimes = 1;
												message = string.Concat(new object[]
												{
													this._retOrderMessage,
													", Times : ",
													this._currTimes,
													"/",
													this._OrdTimes
												});
											}
											this.ShowOrderFormConfirm("Confirm to send?" + ((this._currTimes + 1 <= this._OrdTimes) ? string.Concat(new object[]
											{
												" *** Times : ",
												this._currTimes,
												"/",
												this._OrdTimes
											}) : ""), this._retOrderMessage, this._dsSendOrder.Tables["verify_result"].Rows[0]["oss"].ToString(), this._dsSendOrder.Tables["verify_result"].Rows[0]["stock_threshold"].ToString());
										}
										else
										{
											this._objLastActive = this.cbPrice;
											this.ShowMessageInFormConfirm(message, frmOrderFormConfirm.OpenStyle.ShowBox);
										}
										this._dsSendOrder.Clear();
									}
									else
									{
										this.ShowMessageInFormConfirm("An error is detected.", frmOrderFormConfirm.OpenStyle.ShowBox);
										this.DoClear();
									}
								}
								catch (Exception ex)
								{
									this.ShowMessageInFormConfirm(ex.Message, frmOrderFormConfirm.OpenStyle.ShowBox);
								}
							}
							else
							{
								if (this._commandType == "S")
								{
									try
									{
										if (this._newOrderResult.OrderNo > 0L)
										{
											this._returnOrderNumberFromServer = this._newOrderResult.OrderNo;
											if (ApplicationInfo.SupportFreewill)
											{
												this.ShowSplash(true, "New Order Reference no. " + this._newOrderResult.OrderNo, true);
											}
											else
											{
												this.ShowSplash(true, "New Order Number " + this._newOrderResult.OrderNo, true);
											}
											ApplicationInfo.AddOrderNoToAutoRefreshList(this._newOrderResult.OrderNo.ToString(), this._newOrderResult.IsFwOfflineOrder ? 3 : 1);
										}
										else
										{
											this.ShowMessageInFormConfirm("Fail >> " + this._newOrderResult.ResultMessage, frmOrderFormConfirm.OpenStyle.ShowBox);
										}
										if (ApplicationInfo.SupportOrderTimes)
										{
											if (this._currTimes + 1 <= this._OrdTimes)
											{
												if (ApplicationInfo.SupportFreewill)
												{
													return;
												}
												this._currTimes++;
												Thread thread = new Thread(new ThreadStart(this.threadSendTimes));
												thread.Start();
												return;
											}
										}
										this.DoClear();
										this.cbStock.Focus();
										this.cbStock.SelectAll();
									}
									catch (Exception ex)
									{
										this.ShowMessageInFormConfirm("SendNewOrder:" + ex.Message, frmOrderFormConfirm.OpenStyle.ShowBox);
									}
								}
								else
								{
									if (this._commandType == "T")
									{
										try
										{
											if (this._dsSendOrder.Tables.Contains("ORDERS") && this._dsSendOrder.Tables["ORDERS"].Rows.Count > 0)
											{
												long num3;
												long.TryParse(this._dsSendOrder.Tables["ORDERS"].Rows[0]["ref_no"].ToString(), out num3);
												if (num3 > 0L)
												{
													this.ShowSplash(true, "Send Auto Trade #" + num3, true);
													if (this._OnNewStopOrder != null)
													{
														this._OnNewStopOrder(this._dsSendOrder.Tables["ORDERS"].Rows[0]);
													}
												}
												else
												{
													this.ShowMessageInFormConfirm("Fail >> Unexpected error!!!", frmOrderFormConfirm.OpenStyle.ShowBox);
												}
											}
											else
											{
												if (this._dsSendOrder.Tables.Contains("Results") && this._dsSendOrder.Tables["Results"].Rows.Count > 0)
												{
													long num3;
													long.TryParse(this._dsSendOrder.Tables["Results"].Rows[0]["code"].ToString(), out num3);
													if (num3 <= 0L)
													{
														this.ShowMessageInFormConfirm("Fail >> " + this._dsSendOrder.Tables["Results"].Rows[0]["message"].ToString(), frmOrderFormConfirm.OpenStyle.ShowBox);
													}
												}
											}
											this.DoClear();
											this.cbStock.Focus();
											this.cbStock.SelectAll();
										}
										catch (Exception ex)
										{
											this.ShowMessageInFormConfirm("SendStopOrder:" + ex.Message, frmOrderFormConfirm.OpenStyle.ShowBox);
										}
									}
								}
							}
						}
					}
					else
					{
						if (ApplicationInfo.UserPincodeWrongCount < ApplicationInfo.UserMaxRetryPincode)
						{
							this.ShowMessageInFormConfirm(this._verifyResultStr_Pin, frmOrderFormConfirm.OpenStyle.ShowBox, this.tbPin);
							this.tbPin.Focus();
						}
						else
						{
							if (this._verifyResultStr_Pin == ApplicationInfo.PINCODE_TIMEOUT)
							{
								this.ShowMessageInFormConfirm("*** Pincode timeout. ***\nPlease entry again!", frmOrderFormConfirm.OpenStyle.ShowBox, this.tbPin);
							}
							else
							{
								this.ShowMessageInFormConfirm("*** Pincode Locked. ***\nPlease logout and login again!", frmOrderFormConfirm.OpenStyle.ShowBox);
							}
						}
					}
				}
				else
				{
					try
					{
						if (this._commandType == "S")
						{
							if (this._dsSendOrderTfex != null && this._dsSendOrderTfex.Tables.Contains("Results") && this._dsSendOrderTfex.Tables["Results"].Rows.Count > 0)
							{
								DataRow dataRow = this._dsSendOrderTfex.Tables["Results"].Rows[0];
								long num4 = 0L;
								long.TryParse(dataRow["Code"].ToString(), out num4);
								if (num4 > 0L)
								{
									this._returnOrderNumberFromServer_TFEX = num4;
									ApplicationInfo.AddOrderNoToAutoRefreshList_TFEX(num4.ToString());
								}
								else
								{
									this.ShowMessageInFormConfirm(dataRow["message"].ToString(), frmOrderFormConfirm.OpenStyle.ShowBox);
								}
								this._dsSendOrderTfex.Clear();
							}
							else
							{
								this.ShowMessageInFormConfirm("Send new order Unsuccessful!!!", frmOrderFormConfirm.OpenStyle.ShowBox);
							}
							this.DoClear_TFEX();
							this.tbSeries.Focus();
							this.tbSeries.SelectAll();
						}
					}
					catch (Exception ex)
					{
						this.ShowError("bgwSendOrder_RunWorkerCompleted", ex);
					}
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string CalculateValue(long volume, string price, string side, StockList.StockInformation stockInfo)
		{
			string result = string.Empty;
			decimal d = 0m;
			try
			{
				if (stockInfo != null && stockInfo.Number > 0)
				{
					if (price == "ATO" || price == "ATC" || price == "MP" || price == "MO" || price == "ML")
					{
						if (side == "B" || side == "C")
						{
							d = stockInfo.Ceiling;
						}
						else
						{
							if (side == "S" || side == "H")
							{
								d = stockInfo.Floor;
							}
						}
					}
					else
					{
						decimal.TryParse(price, out d);
					}
					decimal num = volume * d;
					result = FormatUtil.PriceFormat(num, 2, "");
				}
			}
			catch (Exception ex)
			{
				this.ShowError("CalculateValue", ex);
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool SetColorBySide(string side)
		{
			bool result;
			if (base.InvokeRequired)
			{
				result = (bool)base.Invoke(new ucSendNewOrder.SetColorBySideCallBack(this.SetColorBySide), new object[]
				{
					side
				});
			}
			else
			{
				try
				{
					this._showSide = side;
					if (Settings.Default.MainBottomStyle == 1)
					{
						this.cbSide.Text = Utilities.GetOrderSideName(this._showSide);
					}
					else
					{
						string showSide = this._showSide;
						if (showSide != null)
						{
							if (!(showSide == "B"))
							{
								if (!(showSide == "S"))
								{
									if (!(showSide == "H"))
									{
										if (showSide == "C")
										{
											this.rbCover.Checked = true;
										}
									}
									else
									{
										this.rbShort.Checked = true;
									}
								}
								else
								{
									this.rbSell.Checked = true;
								}
							}
							else
							{
								this.rbBuy.Checked = true;
							}
						}
					}
					this.cbDepCollateral.Enabled = false;
					this.cbDepCollateral.Text = "";
					Color foreColor = Color.Black;
					if (this._showSide == "B")
					{
						this.panelEquity.BackColor = Color.DarkGreen;
						foreColor = Color.White;
					}
					else
					{
						if (this._showSide == "S")
						{
							if (ApplicationInfo.SuuportSBL == "Y")
							{
								if (ApplicationInfo.AccInfo.CurrentAccountType == "B")
								{
									if (!this.chbEqStopOrder.Checked)
									{
										this.cbDepCollateral.Enabled = true;
									}
								}
								else
								{
									this.cbDepCollateral.Text = "";
								}
							}
							this.panelEquity.BackColor = Color.Maroon;
							foreColor = Color.White;
						}
						else
						{
							if (this._showSide == "H")
							{
								if (ApplicationInfo.MarketState != "O")
								{
									this.ShowMessageInFormConfirm("This market state short sell not allowed!", frmOrderFormConfirm.OpenStyle.ShowBox);
									result = false;
									return result;
								}
								this.panelEquity.BackColor = Color.Pink;
								foreColor = Color.Black;
							}
							else
							{
								if (!(this._showSide == "C"))
								{
									result = false;
									return result;
								}
								this.panelEquity.BackColor = Color.Turquoise;
								foreColor = Color.Black;
							}
						}
					}
					foreach (Control control in base.Controls)
					{
						if (control.GetType() == typeof(Panel) && control as Panel == this.panelEquity)
						{
							foreach (Control control2 in control.Controls)
							{
								if (control2 != this.lbLoading)
								{
									if (control2.GetType() == typeof(Label) || control2.GetType() == typeof(CheckBox) || control2.GetType() == typeof(Button) || control2.GetType() == typeof(RadioButton))
									{
										control2.ForeColor = foreColor;
									}
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("SetColorBySide", ex);
				}
				result = true;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetColorBySide_TFEX()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(this.SetColorBySide_TFEX));
			}
			else
			{
				try
				{
					if (this._showSideTFEX == "L" && !this.rdbTfexBuy.Checked)
					{
						this.rdbTfexBuy.Checked = true;
					}
					else
					{
						if (this._showSideTFEX == "S" && !this.rdbTfexSell.Checked)
						{
							this.rdbTfexSell.Checked = true;
						}
					}
					Color foreColor = Color.Black;
					if (this._showSideTFEX == "L")
					{
						this.panelDerivative.BackColor = Color.DarkGreen;
						foreColor = Color.White;
					}
					else
					{
						if (!(this._showSideTFEX == "S"))
						{
							return;
						}
						this.panelDerivative.BackColor = Color.Maroon;
						foreColor = Color.White;
					}
					foreach (Control control in this.panelDerivative.Controls)
					{
						if (control.GetType() == typeof(Label) && control != this.lbLoading)
						{
							control.ForeColor = foreColor;
						}
						else
						{
							if (control.GetType() == typeof(CheckBox))
							{
								control.ForeColor = foreColor;
							}
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("SetColorBySide_TFEX", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void controlOrder_Enter(object sender, EventArgs e)
		{
			try
			{
				((Control)sender).BackColor = Color.Yellow;
				((Control)sender).ForeColor = Color.Black;
				if (sender.GetType() == typeof(TextBox))
				{
					((TextBox)sender).SelectAll();
				}
				if (sender.Equals(this.cbPrice))
				{
					if (this.tbPin.Text == string.Empty && ApplicationInfo.UserPincodeLastEntry != string.Empty)
					{
						this.tbPin.Text = ApplicationInfo.UserPincodeLastEntry;
					}
				}
				if (sender.Equals(this.tbTfexPriceCondition))
				{
					if (this.tbTfexPriceCondition.Text == "PRICE")
					{
						this.tbTfexPriceCondition.BackColor = Color.Yellow;
						this.tbTfexPriceCondition.ForeColor = Color.Black;
						this.tbTfexPriceCondition.Text = "";
					}
				}
				if (sender.Equals(this.tbSeriesCondition))
				{
					if (this.tbSeriesCondition.Text == "SERIES")
					{
						this.tbSeriesCondition.BackColor = Color.Yellow;
						this.tbSeriesCondition.ForeColor = Color.Black;
						this.tbSeriesCondition.Text = "";
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
				if (sender.GetType() == typeof(CheckBox))
				{
					((Control)sender).BackColor = Color.Transparent;
					if (this.panelEquity.BackColor == Color.Maroon || this.panelEquity.BackColor == Color.DarkGreen)
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
					if (sender.GetType() == typeof(ComboBox))
					{
						((Control)sender).BackColor = Color.FromArgb(224, 224, 224);
						((Control)sender).ForeColor = Color.Black;
					}
					else
					{
						((Control)sender).BackColor = Color.FromArgb(224, 224, 224);
						((Control)sender).ForeColor = Color.Black;
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("controlOrder_Leave", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void controlOrderTFEX_Leave(object sender, EventArgs e)
		{
			try
			{
				if (sender.GetType() == typeof(CheckBox))
				{
					((Control)sender).BackColor = Color.Transparent;
					if (this.panelDerivative.BackColor == Color.Maroon || this.panelDerivative.BackColor == Color.DarkGreen)
					{
						((Control)sender).ForeColor = Color.LightGray;
					}
					else
					{
						((Control)sender).ForeColor = Color.Black;
					}
				}
				else
				{
					((Control)sender).BackColor = Color.LightGray;
					((Control)sender).ForeColor = Color.Black;
					if (sender == this.tbSeriesCondition)
					{
						if (string.IsNullOrEmpty(((Control)sender).Text))
						{
							((Control)sender).Text = "SERIES";
							((Control)sender).ForeColor = Color.Silver;
						}
					}
					else
					{
						if (sender == this.tbTfexPriceCondition)
						{
							if (string.IsNullOrEmpty(((Control)sender).Text))
							{
								((Control)sender).Text = "PRICE";
								((Control)sender).ForeColor = Color.Silver;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("controlOrderTFEX_Leave", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetSmartOneClick(string side, string stock, string price, long volume)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new ucSendNewOrder.SetSmartOneClickCallBack(this.SetSmartOneClick), new object[]
				{
					side,
					stock,
					price,
					volume
				});
			}
			else
			{
				this._stockInfo = ApplicationInfo.StockInfo[stock];
				if (this._stockInfo.Number > -1)
				{
					this.SetColorBySide(side);
					this.StartTimerLoadCredit();
					this.cbStock.Text = stock;
					this.chbNVDR.Checked = false;
					this.cbPrice.Enabled = true;
					this.cbPrice.Text = price;
					this.cbDepCollateral.Text = "";
					this.cbCondition.Text = string.Empty;
					if (volume > -1L)
					{
						this.tbVolume.Text = volume.ToString();
						this.btnSendOrder.PerformClick();
					}
					else
					{
						this.tbVolume.Focus();
					}
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetCurrentSymbol(string symbol)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new ucSendNewOrder.SetCurrentSymbolCallBack(this.SetCurrentSymbol), new object[]
				{
					symbol
				});
			}
			else
			{
				this._stockInfo = ApplicationInfo.StockInfo[symbol];
				if (this._stockInfo.Number > -1)
				{
					this.cbStock.Text = this._stockInfo.Symbol;
					if (!this.cbStock.Items.Contains(symbol))
					{
						this.cbStock.Items.Add(symbol);
					}
					this.chbNVDR.Checked = false;
					this.cbDepCollateral.Text = "";
					this.cbCondition.Text = string.Empty;
					this.cbPrice.Text = string.Empty;
				}
				else
				{
					this._seriesInfoTfex = ApplicationInfo.SeriesInfo[symbol];
					if (this._seriesInfoTfex.Symbol != string.Empty)
					{
						this.tbSeries.Text = this._seriesInfoTfex.Symbol;
					}
				}
				if (this._isActive)
				{
					this.StartTimerLoadCredit();
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SendBatchOrder(string side, string stock, string ttf, string volume, string price, string pubvol, string condition, string deposit)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new ucSendNewOrder.SendBatchOrderCallBack(this.SendBatchOrder), new object[]
				{
					side,
					stock,
					ttf,
					volume,
					price,
					pubvol,
					condition,
					deposit
				});
			}
			else
			{
				this.SetColorBySide(side);
				this.cbStock.Text = stock;
				if (ttf.Trim() == "2")
				{
					this.chbNVDR.Checked = true;
				}
				else
				{
					this.chbNVDR.Checked = false;
				}
				this.tbVolume.Text = volume;
				this.cbPrice.Text = price;
				this.tbPublic.Text = pubvol;
				this.cbCondition.Text = condition;
				this.btnSendOrder.PerformClick();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbVolume_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			long num;
			if (keyCode != Keys.Return)
			{
				switch (keyCode)
				{
				case Keys.Left:
					if (Settings.Default.MainBottomStyle == 4)
					{
						this.cbStock.Focus();
					}
					else
					{
						if (this.chbNVDR.Visible)
						{
							this.chbNVDR.Focus();
						}
						else
						{
							this.cbStock.Focus();
						}
					}
					e.SuppressKeyPress = true;
					return;
				case Keys.Up:
					long.TryParse(this.tbVolume.Text.Replace(",", ""), out num);
					if (Settings.Default.BSBoxDefaultVolumeNext > 0L)
					{
						num += Settings.Default.BSBoxDefaultVolumeNext;
					}
					else
					{
						num += (long)((this._stockInfo != null) ? this._stockInfo.BoardLot : 0);
					}
					this.tbVolume.Text = Utilities.VolumeFormat(num, true);
					this.tbVolume.SelectAll();
					e.SuppressKeyPress = true;
					return;
				case Keys.Right:
					break;
				case Keys.Down:
					long.TryParse(this.tbVolume.Text.Replace(",", ""), out num);
					if (Settings.Default.BSBoxDefaultVolumeNext > 0L)
					{
						num -= Settings.Default.BSBoxDefaultVolumeNext;
					}
					else
					{
						num -= (long)((this._stockInfo != null) ? this._stockInfo.BoardLot : 0);
					}
					if (num > 0L)
					{
						this.tbVolume.Text = Utilities.VolumeFormat(num, true);
						this.tbVolume.SelectAll();
					}
					e.SuppressKeyPress = true;
					return;
				default:
					return;
				}
			}
			long.TryParse(this.tbVolume.Text.Replace(",", ""), out num);
			if (num > 0L)
			{
				if (this.tbTimes.Visible && this.tbTimes.Enabled)
				{
					this.tbTimes.Focus();
				}
				else
				{
					if (this.cbPrice.Visible)
					{
						this.cbPrice.Focus();
					}
				}
			}
			else
			{
				this.tbVolume.SelectAll();
			}
			e.SuppressKeyPress = true;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool IsValidPrice(string price, bool IsShowMessage)
		{
			bool result;
			try
			{
				if (price != null)
				{
					if (price == "ATO" || price == "ATC" || price == "MP" || price == "MO" || price == "ML")
					{
						result = true;
						return result;
					}
				}
				if (!FormatUtil.Isnumeric(price))
				{
					if (IsShowMessage)
					{
						this.ShowMessageInFormConfirm("Invalid price.", frmOrderFormConfirm.OpenStyle.ShowBox, this.cbPrice);
					}
					result = false;
					return result;
				}
				int num = price.IndexOf('.');
				string text = string.Empty;
				if (num > -1)
				{
					text = price.Substring(num + 1);
					if (text.Length < 2)
					{
						if (ApplicationInfo.BrokerId != 11)
						{
							if (IsShowMessage)
							{
								this.ShowMessageInFormConfirm("Invalid price format [2 digits]!.", frmOrderFormConfirm.OpenStyle.ShowBox, this.cbPrice);
							}
							result = false;
							return result;
						}
					}
					else
					{
						if (text.Length > 2)
						{
							if (IsShowMessage)
							{
								this.ShowMessageInFormConfirm("Invalid price format [2 digits]!.", frmOrderFormConfirm.OpenStyle.ShowBox, this.cbPrice);
							}
							result = false;
							return result;
						}
					}
				}
				if (Convert.ToDecimal(price) <= 0m)
				{
					if (IsShowMessage)
					{
						this.ShowMessageInFormConfirm("Invalid price [More than 0]!.", frmOrderFormConfirm.OpenStyle.ShowBox, this.cbPrice);
					}
					result = false;
					return result;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("IsValidPrice", ex);
			}
			result = true;
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool IsValidPrice_TFEX(bool IsShowMessage, string price)
		{
			bool result;
			try
			{
				if (price != null)
				{
					if (price == "MP")
					{
						result = true;
						return result;
					}
				}
				if (!FormatUtil.Isnumeric(price))
				{
					if (IsShowMessage)
					{
						this.ShowMessageInFormConfirm("Invalid price.", frmOrderFormConfirm.OpenStyle.ShowBox, this.tbPriceT);
					}
					result = false;
					return result;
				}
				int num = price.IndexOf('.');
				string text = string.Empty;
				if (num > -1)
				{
					text = price.Substring(num + 1);
					if (text.Length > 4)
					{
						if (IsShowMessage)
						{
							this.ShowMessageInFormConfirm("Invalid price decimal!.", frmOrderFormConfirm.OpenStyle.ShowBox, this.tbPriceT);
						}
						result = false;
						return result;
					}
				}
				decimal d = 0m;
				decimal d2 = 0m;
				decimal d3 = 0m;
				decimal.TryParse(this._seriesInfoTfex.Floor.ToString(), out d2);
				decimal.TryParse(this._seriesInfoTfex.Ceiling.ToString(), out d);
				decimal.TryParse(price.ToString(), out d3);
				if (d3 < d2 || d3 > d)
				{
					this.ShowMessageInFormConfirm("Check Floor price or Ceiling price!!", frmOrderFormConfirm.OpenStyle.ShowBox, this.tbPriceT);
					result = false;
					return result;
				}
				SeriesList.SeriesInformation seriesInformation = ApplicationInfo.SeriesInfo[this.tbSeries.Text.ToUpper().Trim()];
				if (seriesInformation != null && d3 % seriesInformation.TickSize != 0m)
				{
					this.ShowMessageInFormConfirm("Invalid Price [Check series tick size]!", frmOrderFormConfirm.OpenStyle.ShowBox, this.tbVolumeT);
					result = false;
					return result;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("IsValidPrice_TFEX", ex);
			}
			result = true;
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbTimes_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				Keys keyCode = e.KeyCode;
				if (keyCode != Keys.Return)
				{
					switch (keyCode)
					{
					case Keys.Left:
						this.tbVolume.Focus();
						goto IL_5C;
					case Keys.Up:
						goto IL_5C;
					case Keys.Right:
						break;
					default:
						goto IL_5C;
					}
				}
				this.cbPrice.Focus();
				e.SuppressKeyPress = true;
				IL_5C:;
			}
			catch (Exception ex)
			{
				this.ShowError("tbTimes_KeyDown", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbPrice_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				Keys keyCode = e.KeyCode;
				if (keyCode != Keys.Return)
				{
					switch (keyCode)
					{
					case Keys.Left:
						if (ApplicationInfo.SupportOrderTimes)
						{
							int num = 0;
							int.TryParse(this.tbTimes.Text, out num);
							if (num > 0)
							{
								this.tbTimes.Focus();
							}
							else
							{
								this.tbVolume.Focus();
							}
						}
						else
						{
							this.tbVolume.Focus();
						}
						e.SuppressKeyPress = true;
						break;
					case Keys.Up:
						this.cbPrice.Text = Utilities.PriceFormat(this.GetPrice(true));
						this.cbPrice.SelectAll();
						e.SuppressKeyPress = true;
						break;
					case Keys.Right:
						if (this.IsValidPrice(this.cbPrice.Text, false))
						{
							if (Settings.Default.MainBottomStyle == 2)
							{
								this.tbPin.Focus();
							}
							else
							{
								if (Settings.Default.MainBottomStyle == 4)
								{
									this.cbCondition.Focus();
								}
								else
								{
									this.tbPublic.Focus();
								}
							}
						}
						e.SuppressKeyPress = true;
						break;
					case Keys.Down:
						this.cbPrice.Text = Utilities.PriceFormat(this.GetPrice(false));
						this.cbPrice.SelectAll();
						e.SuppressKeyPress = true;
						break;
					}
				}
				else
				{
					if (this.IsValidPrice(this.cbPrice.Text, true))
					{
						if (Settings.Default.MainBottomStyle == 1)
						{
							this.btnSendOrder.PerformClick();
						}
						else
						{
							if (this.tbPin.Text.Trim() == string.Empty)
							{
								this.tbPin.Focus();
							}
							else
							{
								this.btnSendOrder.PerformClick();
							}
						}
					}
					e.SuppressKeyPress = true;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("cbPrice_KeyDown", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private decimal GetPrice(bool isIncrease)
		{
			decimal num = 0m;
			try
			{
				string stockSymbol = this.cbStock.Text.Trim().ToUpper();
				string text = this.cbPrice.Text;
				if (decimal.TryParse(text, out num))
				{
					StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[stockSymbol];
					if (stockInformation != null && stockInformation.Number > 0)
					{
						if (isIncrease)
						{
							num += this.GetNextSpreadUp(stockInformation, num);
							if (num > this._stockInfo.Ceiling)
							{
								num = this._stockInfo.Ceiling;
							}
						}
						else
						{
							num -= this.GetNextSpreadDown(stockInformation, num);
							if (num < this._stockInfo.Floor)
							{
								num = this._stockInfo.Floor;
							}
						}
					}
				}
				else
				{
					if (num == 0m && this._stockInfo != null)
					{
						num = this._stockInfo.PriorPrice;
					}
				}
			}
			catch
			{
			}
			return num;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private decimal GetSMAPeriod(bool isIncrease, string period)
		{
			decimal num = 0m;
			try
			{
				if (decimal.TryParse(period, out num))
				{
					if (isIncrease)
					{
						if (num >= 50m)
						{
							num = 75m;
						}
						else
						{
							if (num >= 75m)
							{
								num = 200m;
							}
							else
							{
								if (++num <= 200m)
								{
									num = ++num;
								}
							}
						}
					}
					else
					{
						if (num == 75m)
						{
							num = 50m;
						}
						else
						{
							if (num >= 200m)
							{
								num = 75m;
							}
							else
							{
								if (--num >= 2m)
								{
									num = --num;
								}
							}
						}
					}
				}
				else
				{
					if (num == 0m)
					{
						num = 7m;
					}
				}
			}
			catch
			{
			}
			return num;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private decimal GetHLVPeriod(bool isIncrease, string period)
		{
			decimal num = 0m;
			try
			{
				if (decimal.TryParse(period, out num))
				{
					if (isIncrease)
					{
						if (++num <= 20m)
						{
							num = ++num;
						}
					}
					else
					{
						if (--num >= 2m)
						{
							num = --num;
						}
					}
				}
				else
				{
					if (num == 0m)
					{
						num = 2m;
					}
				}
			}
			catch
			{
			}
			return num;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbPrice_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private decimal GetNextSpreadDown(StockList.StockInformation stockInfo, decimal Price)
		{
			decimal result;
			if (stockInfo.IsCheckSpread)
			{
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
			}
			else
			{
				result = 0.01m;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private decimal GetNextSpreadUp(StockList.StockInformation stockInfo, decimal Price)
		{
			decimal result;
			if (stockInfo.IsCheckSpread)
			{
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
			}
			else
			{
				result = 0.01m;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowError(string functionName, Exception ex)
		{
			ExceptionManager.Show(new ErrorItem(DateTime.Now, base.Name, functionName, ex.Message, ex.ToString()));
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbNDVR_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != Keys.Return)
			{
				if (keyCode == Keys.Add)
				{
					this.chbNVDR.Checked = !this.chbNVDR.Checked;
				}
			}
			else
			{
				this.tbVolume.Focus();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbNVDR_CheckedChanged(object sender, EventArgs e)
		{
			this.ShowCreditValue();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbPublic_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != Keys.Return)
			{
				switch (keyCode)
				{
				case Keys.Left:
				case Keys.Up:
					this.cbPrice.Focus();
					return;
				case Keys.Right:
					break;
				default:
					return;
				}
			}
			this.cbCondition.Focus();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbPublic_TextChanged(object sender, EventArgs e)
		{
			try
			{
				if (!this._isLockPubVol && this.tbPublic.Text.Trim() != string.Empty)
				{
					if (FormatUtil.Isnumeric(this.tbPublic.Text))
					{
						try
						{
							decimal num = Convert.ToInt64(this.tbPublic.Text.Replace(",", ""));
							this.tbPublic.Text = num.ToString("#,###");
							this.tbPublic.Select(this.tbPublic.Text.Length, 0);
						}
						catch
						{
							this.tbPublic.Text = this.tbPublic.Text.Substring(0, this.tbPublic.Text.Length - 1);
						}
					}
					else
					{
						this.tbPublic.Text = this.tbPublic.Text.Substring(0, this.tbPublic.Text.Length - 1);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tbPublic_TextChanged", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbCondition_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != Keys.Return)
			{
				switch (keyCode)
				{
				case Keys.Left:
					if (Settings.Default.MainBottomStyle == 4)
					{
						this.cbPrice.Focus();
					}
					else
					{
						this.tbPublic.Focus();
					}
					break;
				case Keys.Right:
					if (Settings.Default.MainBottomStyle == 4)
					{
						if (Settings.Default.BSBoxEntryTTF)
						{
							this.chbNVDR.Focus();
						}
						else
						{
							this.tbPin.Focus();
						}
					}
					else
					{
						if (Settings.Default.MainBottomStyle == 3)
						{
							this.tbPin.Focus();
						}
						else
						{
							if (this.cbDepCollateral.Enabled)
							{
								this.cbDepCollateral.Focus();
							}
							else
							{
								this.cbCondition.Focus();
							}
						}
					}
					e.SuppressKeyPress = true;
					break;
				}
			}
			else
			{
				this.btnSendOrder.PerformClick();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbDepCollateral_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != Keys.Return)
			{
				if (keyCode == Keys.Left)
				{
					if (Settings.Default.MainBottomStyle == 1)
					{
						this.cbCondition.Focus();
					}
					else
					{
						this.tbPublic.Focus();
					}
				}
			}
			else
			{
				this.btnSendOrder.PerformClick();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowSplash(bool visible, string message, bool isAutoClose)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new ucSendNewOrder.ShowSplashCallBack(this.ShowSplash), new object[]
				{
					visible,
					message,
					isAutoClose
				});
			}
			else
			{
				if (ApplicationInfo.SuuportSplash == "Y")
				{
					try
					{
						if (visible)
						{
							this.lbLoading.Text = message;
							this.lbLoading.Left = (base.Width - this.lbLoading.Width) / 2;
							this.lbLoading.Top = (base.Height - this.lbLoading.Height) / 2;
							this.lbLoading.Visible = true;
							this.lbLoading.BringToFront();
							if (isAutoClose)
							{
								if (this.tmCloseSplash == null)
								{
									this.tmCloseSplash = new System.Windows.Forms.Timer();
									this.tmCloseSplash.Interval = 500;
									this.tmCloseSplash.Tick += new EventHandler(this.tmCloseSplash_Tick);
								}
								this.tmCloseSplash.Enabled = false;
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
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tmCloseSplash_Tick(object sender, EventArgs e)
		{
			this.tmCloseSplash.Enabled = false;
			this.ShowSplash(false, "", false);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbDepCollateral_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ShowCreditValue();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbCondition_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ucSendNewOrder_Enter(object sender, EventArgs e)
		{
			ApplicationInfo.IsOrderBoxFocus = true;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ucSendNewOrder_Leave(object sender, EventArgs e)
		{
			ApplicationInfo.IsOrderBoxFocus = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SwitchAccountControl()
		{
			if (this.cbAccount.InvokeRequired)
			{
				this.cbAccount.Invoke(new MethodInvoker(this.SwitchAccountControl));
			}
			else
			{
				if (this.cbAccount.Items.Count > 1)
				{
					if (this.cbAccount.SelectedIndex + 1 < this.cbAccount.Items.Count)
					{
						this.cbAccount.SelectedIndex++;
					}
					else
					{
						this.cbAccount.SelectedIndex = 0;
					}
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this._isActive)
			{
				this.timerSwitchAccount.Stop();
				this.timerSwitchAccount.Start();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void timerSwitchAccount_Tick(object sender, EventArgs e)
		{
			try
			{
				this.timerSwitchAccount.Stop();
				ApplicationInfo.AccInfo.CurrentAccount = this.GetAccount(this.cbAccount.Text.Trim());
				if (ApplicationInfo.AccInfo.Items.ContainsKey(ApplicationInfo.AccInfo.CurrentAccount))
				{
					string market = ApplicationInfo.AccInfo.Items[ApplicationInfo.AccInfo.CurrentAccount].Market;
					if (market == "E")
					{
						ApplicationInfo.IsEquityAccount = true;
					}
					else
					{
						if (!(market == "T"))
						{
							return;
						}
						ApplicationInfo.IsEquityAccount = false;
					}
					this.tbBuyLimit.Text = "Loading...";
					this.tbOnHand.Text = "Loading...";
					this.tbEquity.Text = "Loading...";
					if (this._OnAccountChanged != null)
					{
						this._OnAccountChanged(ApplicationInfo.AccInfo.CurrentAccount);
					}
					if (ApplicationInfo.IsEquityAccount)
					{
						AccountInfo.ItemInfo itemInfo;
						if (ApplicationInfo.AccInfo.Items.TryGetValue(ApplicationInfo.AccInfo.CurrentAccount, out itemInfo))
						{
							if (itemInfo.AccountType == string.Empty)
							{
								string text = string.Empty;
								if (ApplicationInfo.IsSupportEservice)
								{
									text = ApplicationInfo.WebOrderService.GetSwitchAccountInfoEservice("042", ApplicationInfo.UserLoginID, ApplicationInfo.GetSession(), ApplicationInfo.AccInfo.CurrentAccount, "M", "I2Trade");
								}
								else
								{
									text = ApplicationInfo.WebOrderService.GetSwitchAccountInfo(ApplicationInfo.AccInfo.CurrentAccount);
								}
								if (!string.IsNullOrEmpty(text))
								{
									using (DataSet dataSet = new DataSet())
									{
										MyDataHelper.StringToDataSet(text, dataSet);
										if (dataSet != null && dataSet.Tables.Contains("INFO") && dataSet.Tables["INFO"].Rows.Count > 0)
										{
											DataRow dataRow = dataSet.Tables["INFO"].Rows[0];
											itemInfo.AccountType = dataRow["sAccType"].ToString().Trim();
											itemInfo.PcFlag = dataRow["sPC"].ToString();
											if (ApplicationInfo.SupportFreewill)
											{
												itemInfo.TraderId = dataRow["traderid"].ToString();
											}
											ApplicationInfo.AccInfo.CurrentCommRate = 0m;
											if (dataSet.Tables.Contains("COMM_RATE") && dataSet.Tables["COMM_RATE"].Rows.Count > 0)
											{
												decimal.TryParse(dataSet.Tables["COMM_RATE"].Rows[0]["commrate"].ToString(), out ApplicationInfo.AccInfo.CurrentCommRate);
												decimal.TryParse(dataSet.Tables["COMM_RATE"].Rows[0]["trading_fee"].ToString(), out ApplicationInfo.AccInfo.CurrentTradingFee);
												decimal.TryParse(dataSet.Tables["COMM_RATE"].Rows[0]["clearing_fee"].ToString(), out ApplicationInfo.AccInfo.CurrentClearingFee);
											}
											dataSet.Clear();
										}
										if (dataSet != null && dataSet.Tables.Contains("eservice") && dataSet.Tables["eservice"].Rows.Count > 0)
										{
											DataRow dataRow = dataSet.Tables["eservice"].Rows[0];
											ApplicationInfo.EserviceServer = ApplicationInfo.EserviceServer + "?txtParam=" + dataRow["control_value"].ToString().Trim();
										}
									}
								}
							}
							ApplicationInfo.AccInfo.CurrentAccountType = itemInfo.AccountType;
							if (itemInfo.PcFlag == "M" || itemInfo.PcFlag == "U")
							{
								ApplicationInfo.AccInfo.CurrInternetUser = ApplicationInfo.AccInfo.InternetMutualUser;
							}
							else
							{
								ApplicationInfo.AccInfo.CurrInternetUser = ApplicationInfo.AccInfo.InternetUser;
							}
							object syncRoot;
							Monitor.Enter(syncRoot = ((ICollection)ApplicationInfo.AutoGetOrderNoList).SyncRoot);
							try
							{
								ApplicationInfo.AutoGetOrderNoList.Clear();
							}
							finally
							{
								Monitor.Exit(syncRoot);
							}
							this.SwitchMarket();
							this.SetResize();
							this.StartTimerLoadCredit();
						}
						else
						{
							ApplicationInfo.AutoGetOrderNoList.Clear();
							this._creditType = 0;
							this._buyCreditLimit = 0m;
							this._totalCreditLimit = 0m;
							if (this.tdsCredit != null)
							{
								this.tdsCredit.Clear();
							}
							this.ShowCreditValue();
						}
					}
					else
					{
						this.SwitchMarket();
						this.SetResize();
						this.StartTimerLoadCredit();
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("timerSwitchAccount_Tick", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string GetAccount(string text)
		{
			string result;
			try
			{
				int num = text.IndexOf("(");
				if (num > -1)
				{
					result = text.Substring(0, num - 1).Trim();
					return result;
				}
				result = text;
				return result;
			}
			catch (Exception ex)
			{
				this.ShowError("GetAccount", ex);
			}
			result = text;
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SwitchMarket()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(this.SwitchMarket));
			}
			else
			{
				try
				{
					base.SuspendLayout();
					if (ApplicationInfo.IsEquityAccount)
					{
						this.lbBuyLimit.Visible = true;
						this.tbBuyLimit.Visible = true;
						this.lbOnHand.Visible = true;
						this.tbOnHand.Visible = true;
						if (ApplicationInfo.SupportFreewill)
						{
							this.btnRisk.Visible = false;
						}
						else
						{
							this.btnRisk.Visible = true;
						}
						this.btnNotification.Visible = true;
						this.btnShowStockAlert.Visible = true;
						this.btnCleanPort.Visible = true;
						this.btnStyle1.Visible = true;
						this.btnStyle2.Visible = true;
						this.btnStyle3.Visible = true;
						this.btnStyle4.Visible = true;
						this.btnSetting.Visible = true;
						this.DoClear();
						this.lbBuyLimit.Text = "Buy Limit :";
						this.lbOnHand.Text = "OnHand :";
						this.lbEquity.Visible = false;
						this.tbEquity.Visible = false;
						this.panelEquity.Visible = true;
						this.panelDerivative.Visible = false;
						if (this._showSide != string.Empty)
						{
							this.SetColorBySide(this._showSide);
							this.cbStock.Focus();
							this.cbStock.SelectAll();
						}
						this.rbShort.Hide();
						this.rbCover.Hide();
						this.SetVisibleControlEquity();
					}
					else
					{
						this.lbBuyLimit.Text = "Line Available :";
						this.lbOnHand.Text = "Excess Equity :";
						this.lbEquity.Visible = true;
						this.tbEquity.Visible = true;
						this.DoClear_TFEX();
						this.btnRisk.Visible = false;
						this.btnNotification.Visible = false;
						this.btnShowStockAlert.Visible = false;
						this.btnCleanPort.Visible = false;
						this.btnStyle1.Visible = false;
						this.btnStyle2.Visible = false;
						this.btnStyle3.Visible = false;
						this.btnStyle4.Visible = false;
						this.btnSetting.Visible = false;
						this.panelDerivative.Visible = true;
						this.panelEquity.Visible = false;
						if (this._showSideTFEX != string.Empty)
						{
							this.SetColorBySide_TFEX();
							this.tbSeries.Focus();
							this.tbSeries.SelectAll();
						}
					}
					base.ResumeLayout();
					if (this._OnBoxStyleChanged != null)
					{
						this._OnBoxStyleChanged();
					}
				}
				catch (Exception ex)
				{
					this.ShowError("SwitchMarket", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnStyle_Click(object sender, EventArgs e)
		{
			try
			{
				if (sender == this.btnStyle1)
				{
					Settings.Default.MainBottomStyle = 1;
				}
				else
				{
					if (sender == this.btnStyle2)
					{
						Settings.Default.MainBottomStyle = 2;
					}
					else
					{
						if (sender == this.btnStyle3)
						{
							Settings.Default.MainBottomStyle = 3;
						}
						else
						{
							if (sender == this.btnStyle4)
							{
								Settings.Default.MainBottomStyle = 4;
							}
						}
					}
				}
				this.SetVisibleControlEquity();
				this.SetColorBySide(this._showSide);
				if (this._OnBoxStyleChanged != null)
				{
					this._OnBoxStyleChanged();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("btnStyle_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnSetting_Click(object sender, EventArgs e)
		{
			this.OpenSystemOptionForm();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OpenSystemOptionForm()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(this.OpenSystemOptionForm));
			}
			else
			{
				try
				{
					if (this.frm == null || this.frm.IsDisposed)
					{
						this.frm = new ucBSSetting();
						this.frm.FormClosing -= new FormClosingEventHandler(this.frm_FormClosing);
						this.frm.FormClosing += new FormClosingEventHandler(this.frm_FormClosing);
						this.frm.TopLevel = false;
						this.frm.Parent = TemplateManager.Instance.MainForm;
					}
					this.frm.Left = (this.frm.Parent.Width - this.frm.Width) / 2;
					this.frm.Top = (this.frm.Parent.Height - this.frm.Height) / 2;
					this.frm.Show();
					this.frm.BringToFront();
				}
				catch (Exception ex)
				{
					this.ShowError("OpenSystemOptionForm", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				if (!ApplicationInfo.StopOrderAccepted)
				{
					this.chbEqStopOrder.Checked = false;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("frm_FormClosing", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void rbBuy_CheckedChanged(object sender, EventArgs e)
		{
			if (sender == this.rbBuy && this.rbBuy.Checked)
			{
				this.SetColorBySide("B");
			}
			else
			{
				if (sender == this.rbSell && this.rbSell.Checked)
				{
					this.SetColorBySide("S");
				}
				else
				{
					if (sender == this.rbShort && this.rbShort.Checked)
					{
						this.SetColorBySide("H");
					}
					else
					{
						if (sender != this.rbCover || !this.rbCover.Checked)
						{
							return;
						}
						this.SetColorBySide("C");
					}
				}
			}
			if (this._isActive)
			{
				this.cbStock.Focus();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbSide_SelectedIndexChanged(object sender, EventArgs e)
		{
			string text = this.cbSide.Text.ToLower();
			if (text != null)
			{
				if (!(text == "buy"))
				{
					if (!(text == "sell"))
					{
						if (!(text == "short"))
						{
							if (text == "cover")
							{
								this.SetColorBySide("C");
							}
						}
						else
						{
							this.SetColorBySide("H");
						}
					}
					else
					{
						this.SetColorBySide("S");
					}
				}
				else
				{
					this.SetColorBySide("B");
				}
			}
			if (this._isActive)
			{
				this.cbStock.Focus();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetVisibleControlEquity()
		{
			try
			{
				foreach (Control control in this.panelEquity.Controls)
				{
					control.Visible = false;
				}
				this.chbNVDR.Checked = false;
				if (ApplicationInfo.SuuportSBL == "Y" && ApplicationInfo.AccInfo.CurrentAccountType == "B" && Settings.Default.MainBottomStyle != 2 && Settings.Default.MainBottomStyle != 4)
				{
					this.lbDep.Show();
					this.cbDepCollateral.Show();
				}
				if (ApplicationInfo.StopOrderSupported)
				{
					this.chbEqStopOrder.Visible = true;
				}
				else
				{
					this.chbEqStopOrder.Visible = false;
				}
				if (Settings.Default.MainBottomStyle == 1)
				{
					this.lbSide.Visible = true;
					this.cbSide.Visible = true;
					this.lbStock.Visible = true;
					this.cbStock.Visible = true;
					this.chbNVDR.Visible = true;
					this.lbVolume.Visible = true;
					this.tbVolume.Visible = true;
					if (ApplicationInfo.SupportOrderTimes)
					{
						this.lbTimes.Visible = true;
						this.tbTimes.Visible = true;
					}
					this.lbPrice.Visible = true;
					this.cbPrice.Visible = true;
					this.lbPublic.Visible = true;
					this.tbPublic.Visible = true;
					this.lbCondition.Visible = true;
					this.lbCondition.Text = "Cond";
					this.cbCondition.Visible = true;
					this.btnSendOrder.Text = "Send";
					this.btnSendOrder.Visible = true;
				}
				else
				{
					if (Settings.Default.MainBottomStyle == 2)
					{
						this.rbBuy.Visible = true;
						this.rbSell.Visible = true;
						if (ApplicationInfo.SuuportSBL == "Y" && ApplicationInfo.AccInfo.CurrentAccountType == "B")
						{
							this.rbShort.Visible = true;
							this.rbCover.Visible = true;
						}
						else
						{
							this.rbShort.Visible = false;
							this.rbCover.Visible = false;
						}
						this.lbStock.Visible = true;
						this.cbStock.Visible = true;
						this.lbVolume.Visible = true;
						this.tbVolume.Visible = true;
						this.btnVolInc.Visible = true;
						this.btnVolDec.Visible = true;
						this.lbPrice.Visible = true;
						this.cbPrice.Visible = true;
						this.btnPriceInc.Visible = true;
						this.btnPriceDec.Visible = true;
						this.lbPin.Visible = true;
						this.tbPin.Visible = true;
						this.btnSendOrder.Text = "Submit";
						this.btnSendOrder.Visible = true;
						this.btnClear.Visible = true;
					}
					else
					{
						if (Settings.Default.MainBottomStyle == 3)
						{
							this.rbBuy.Visible = true;
							this.rbSell.Visible = true;
							if (ApplicationInfo.SuuportSBL == "Y" && ApplicationInfo.AccInfo.CurrentAccountType == "B")
							{
								this.rbShort.Visible = true;
								this.rbCover.Visible = true;
							}
							this.cbStock.Visible = true;
							this.chbNVDR.Visible = true;
							this.lbVolume.Visible = true;
							this.tbVolume.Visible = true;
							this.btnVolInc.Visible = true;
							this.btnVolDec.Visible = true;
							this.lbPublic.Visible = true;
							this.tbPublic.Visible = true;
							this.btnPBDec.Visible = true;
							this.btnPBInc.Visible = true;
							this.lbPrice.Visible = true;
							this.cbPrice.Visible = true;
							this.btnPriceInc.Visible = true;
							this.btnPriceDec.Visible = true;
							this.lbCondition.Text = "Validity";
							this.lbCondition.Visible = true;
							this.cbCondition.Visible = true;
							this.lbPin.Visible = true;
							this.tbPin.Visible = true;
							this.btnSendOrder.Text = "Submit";
							this.btnSendOrder.Visible = true;
							this.btnClear.Visible = true;
						}
						else
						{
							if (Settings.Default.MainBottomStyle == 4)
							{
								this.rbBuy.Visible = true;
								this.rbSell.Visible = true;
								if (ApplicationInfo.SuuportSBL == "Y" && ApplicationInfo.AccInfo.CurrentAccountType == "B")
								{
									this.rbShort.Visible = true;
									this.rbCover.Visible = true;
								}
								this.lbStock.Visible = true;
								this.cbStock.Visible = true;
								this.lbVolume.Visible = true;
								this.tbVolume.Visible = true;
								this.lbPrice.Visible = true;
								this.cbPrice.Visible = true;
								this.lbCondition.Text = "Validity";
								this.lbCondition.Visible = true;
								this.cbCondition.Visible = true;
								this.chbNVDR.Visible = true;
								this.lbPin.Visible = true;
								this.tbPin.Visible = true;
								this.btnSendOrder.Text = "Send";
								this.btnSendOrder.Visible = true;
								this.btnClear.Visible = true;
							}
						}
					}
				}
				this.btnStyle1.ForeColor = Color.LightGray;
				this.btnStyle2.ForeColor = Color.LightGray;
				this.btnStyle3.ForeColor = Color.LightGray;
				this.btnStyle4.ForeColor = Color.LightGray;
				if (Settings.Default.MainBottomStyle == 1)
				{
					this.btnStyle1.ForeColor = Color.Cyan;
				}
				else
				{
					if (Settings.Default.MainBottomStyle == 2)
					{
						this.btnStyle2.ForeColor = Color.Cyan;
					}
					else
					{
						if (Settings.Default.MainBottomStyle == 3)
						{
							this.btnStyle3.ForeColor = Color.Cyan;
						}
						else
						{
							if (Settings.Default.MainBottomStyle == 4)
							{
								this.btnStyle4.ForeColor = Color.Cyan;
							}
						}
					}
				}
				this.tbPin.Text = ApplicationInfo.UserPincodeLastEntry;
			}
			catch (Exception ex)
			{
				this.ShowError("SetVisibleControlEquity", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbPin_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				Keys keyCode = e.KeyCode;
				if (keyCode != Keys.Return)
				{
					switch (keyCode)
					{
					case Keys.Left:
						if (Settings.Default.MainBottomStyle == 2)
						{
							this.cbPrice.Focus();
						}
						else
						{
							if (Settings.Default.MainBottomStyle == 3)
							{
								this.cbCondition.Focus();
							}
							else
							{
								if (Settings.Default.MainBottomStyle == 4)
								{
									this.chbNVDR.Focus();
								}
							}
						}
						e.SuppressKeyPress = true;
						break;
					case Keys.Right:
						e.SuppressKeyPress = true;
						break;
					}
				}
				else
				{
					this.btnSendOrder.PerformClick();
					e.SuppressKeyPress = true;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tbPin_KeyDown", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnVolInc_Click(object sender, EventArgs e)
		{
			try
			{
				long num;
				long.TryParse(this.tbVolume.Text.Replace(",", ""), out num);
				if (Settings.Default.BSBoxDefaultVolumeNext > 0L)
				{
					num += Settings.Default.BSBoxDefaultVolumeNext;
				}
				else
				{
					if (this._stockInfo != null)
					{
						num += (long)this._stockInfo.BoardLot;
					}
				}
				this.tbVolume.Text = Utilities.VolumeFormat(num, true);
			}
			catch (Exception ex)
			{
				this.ShowError("btnVolInc_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnVolDec_Click(object sender, EventArgs e)
		{
			try
			{
				long num;
				long.TryParse(this.tbVolume.Text.Replace(",", ""), out num);
				long num2 = 0L;
				if (Settings.Default.BSBoxDefaultVolumeNext > 0L)
				{
					num2 = Settings.Default.BSBoxDefaultVolumeNext;
				}
				else
				{
					if (this._stockInfo != null)
					{
						num2 = (long)this._stockInfo.BoardLot;
					}
				}
				if (num - num2 > 0L)
				{
					num -= num2;
					this.tbVolume.Text = Utilities.VolumeFormat(num, true);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("btnVolDec_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnPriceInc_Click(object sender, EventArgs e)
		{
			this.cbPrice.Text = Utilities.PriceFormat(this.GetPrice(true));
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnPriceDec_Click(object sender, EventArgs e)
		{
			this.cbPrice.Text = Utilities.PriceFormat(this.GetPrice(false));
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnPBInc_Click(object sender, EventArgs e)
		{
			try
			{
				long num;
				long.TryParse(this.tbPublic.Text.Replace(",", ""), out num);
				if (Settings.Default.BSBoxDefaultVolumeNext > 0L)
				{
					num += Settings.Default.BSBoxDefaultVolumeNext;
				}
				else
				{
					if (this._stockInfo != null)
					{
						num += (long)this._stockInfo.BoardLot;
					}
				}
				this.tbPublic.Text = Utilities.VolumeFormat(num, true);
			}
			catch (Exception ex)
			{
				this.ShowError("btnPBInc_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnPBDec_Click(object sender, EventArgs e)
		{
			try
			{
				long num;
				long.TryParse(this.tbPublic.Text.Replace(",", ""), out num);
				long num2 = 0L;
				if (Settings.Default.BSBoxDefaultVolumeNext > 0L)
				{
					num2 = Settings.Default.BSBoxDefaultVolumeNext;
				}
				else
				{
					if (this._stockInfo != null)
					{
						num2 = (long)this._stockInfo.BoardLot;
					}
				}
				if (num - num2 > 0L)
				{
					num -= num2;
					this.tbPublic.Text = Utilities.VolumeFormat(num, true);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("btnPBDec_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbStock_DragDrop(object sender, DragEventArgs e)
		{
			try
			{
				DragItemData dragItemData = (DragItemData)e.Data.GetData(typeof(DragItemData).ToString());
				this.cbStock.Text = dragItemData.DragText;
				this.cbStock.Focus();
			}
			catch (Exception ex)
			{
				this.ShowError("cbStock_DragDrop", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbStock_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Move;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbStock_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				Keys keyCode = e.KeyCode;
				if (keyCode != Keys.Return)
				{
					switch (keyCode)
					{
					case Keys.End:
					case Keys.Home:
					case Keys.Down:
						e.SuppressKeyPress = true;
						goto IL_377;
					case Keys.Left:
						if (Settings.Default.MainBottomStyle == 1)
						{
							this.cbSide.Focus();
						}
						e.SuppressKeyPress = true;
						goto IL_377;
					case Keys.Up:
						goto IL_377;
					case Keys.Right:
						break;
					default:
						goto IL_377;
					}
				}
				if (this.cbStock.Text.Trim() != string.Empty)
				{
					StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[this.cbStock.Text.Trim()];
					if (stockInformation.Number > 0)
					{
						this._stockInfo = stockInformation;
						TemplateManager.Instance.SendSymbolLink(this, SymbolLinkSource.StockSymbol, this._stockInfo.Symbol);
						ApplicationInfo.CurrentSymbol = this._stockInfo.Symbol;
						if (this.cbPrice.Text == string.Empty)
						{
							if (Settings.Default.BSBoxDefaultPrice == 1)
							{
								this.cbPrice.Text = ((this._stockInfo.LastSalePrice > 0m) ? Utilities.PriceFormat(this._stockInfo.LastSalePrice) : Utilities.PriceFormat(this._stockInfo.PriorPrice));
							}
							else
							{
								if (Settings.Default.BSBoxDefaultPrice == 2)
								{
									if (this._showSide == "B" || this._showSide == "C")
									{
										this.cbPrice.Text = Utilities.PriceFormat(this._stockInfo.OfferPrice1);
									}
									else
									{
										if (this._showSide == "S" || this._showSide == "H")
										{
											this.cbPrice.Text = Utilities.PriceFormat(this._stockInfo.BidPrice1);
										}
									}
								}
							}
						}
						if (this.tbVolume.Text == string.Empty)
						{
							if (Settings.Default.BSBoxDefaultVolumeActive && Settings.Default.BSBoxDefaultVolume > 0L)
							{
								this.tbVolume.Text = Utilities.VolumeFormat(Settings.Default.BSBoxDefaultVolume, true);
							}
						}
						if (Settings.Default.MainBottomStyle == 2 || Settings.Default.MainBottomStyle == 4)
						{
							this.tbVolume.Focus();
						}
						else
						{
							if (Settings.Default.BSBoxEntryTTF)
							{
								this.chbNVDR.Focus();
							}
							else
							{
								this.tbVolume.Focus();
							}
						}
					}
					else
					{
						this.cbStock.Text = this._stockInfo.Symbol;
						this.cbStock.Focus();
						this.cbStock.SelectAll();
					}
				}
				e.SuppressKeyPress = true;
				IL_377:;
			}
			catch (Exception ex)
			{
				this.ShowError("cbStock_KeyDown", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbStock_KeyPress(object sender, KeyPressEventArgs e)
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
		private void cbSide_KeyPress(object sender, KeyPressEventArgs e)
		{
			try
			{
				e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
			}
			catch (Exception ex)
			{
				this.ShowError("cbSide_KeyPress", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbSide_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode <= Keys.Right)
			{
				if (keyCode != Keys.Return)
				{
					switch (keyCode)
					{
					case Keys.Left:
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
				this.SetColorBySide(this.cbSide.Text);
				this.cbStock.Focus();
			}
			else
			{
				switch (keyCode)
				{
				case Keys.B:
				case Keys.C:
					break;
				default:
					if (keyCode != Keys.H && keyCode != Keys.S)
					{
						return;
					}
					break;
				}
				this.SetColorBySide(e.KeyCode.ToString());
				this.cbStock.Focus();
				e.SuppressKeyPress = true;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnPolicy_Click(object sender, EventArgs e)
		{
			try
			{
				foreach (Control control in base.Controls)
				{
					if (control.GetType() == typeof(frmRiskControl))
					{
						return;
					}
				}
				frmRiskControl frmRiskControl = new frmRiskControl();
				frmRiskControl.TopLevel = false;
				frmRiskControl.Parent = TemplateManager.Instance.MainForm;
				frmRiskControl.Left = (frmRiskControl.Parent.Width - frmRiskControl.Width) / 2;
				frmRiskControl.Top = (frmRiskControl.Parent.Height - frmRiskControl.Height) / 2;
				frmRiskControl.Show();
				frmRiskControl.BringToFront();
			}
			catch (Exception ex)
			{
				this.ShowError("OpenRiskControlForm", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnCleanPort_Click(object sender, EventArgs e)
		{
			try
			{
				foreach (Control control in base.Controls)
				{
					if (control.GetType() == typeof(frmCleanPort))
					{
						return;
					}
				}
				frmCleanPort frmCleanPort = new frmCleanPort();
				frmCleanPort.TopLevel = false;
				frmCleanPort.Parent = TemplateManager.Instance.MainForm;
				frmCleanPort.Left = (frmCleanPort.Parent.Width - frmCleanPort.Width) / 2;
				frmCleanPort.Top = (frmCleanPort.Parent.Height - frmCleanPort.Height) / 2;
				frmCleanPort.Show();
				frmCleanPort.BringToFront();
			}
			catch (Exception ex)
			{
				this.ShowError("OpenCleanPortForm", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnShowStockAlert_Click(object sender, EventArgs e)
		{
			try
			{
				if (this._pcPriceAlertForm == null)
				{
					this._pcPriceAlertForm = new frmPcPriceAlert();
					this._pcPriceAlertForm.TopLevel = false;
					this._pcPriceAlertForm.Parent = TemplateManager.Instance.MainForm;
					this._pcPriceAlertForm.Left = (this._pcPriceAlertForm.Parent.Width - this._pcPriceAlertForm.Width) / 2;
					this._pcPriceAlertForm.Top = (this._pcPriceAlertForm.Parent.Height - this._pcPriceAlertForm.Height) / 2;
				}
				this._pcPriceAlertForm.Show();
				this._pcPriceAlertForm.BringToFront();
				this._pcPriceAlertForm.Reload();
			}
			catch (Exception ex)
			{
				this.ShowError("btnShowStockAlert_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnNotification_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.alertSettingForm == null)
				{
					this.alertSettingForm = new frmMobileAlert();
					this.alertSettingForm.TopLevel = false;
					this.alertSettingForm.Parent = TemplateManager.Instance.MainForm;
					this.alertSettingForm.Left = (this.alertSettingForm.Parent.Width - this.alertSettingForm.Width) / 2;
					this.alertSettingForm.Top = (this.alertSettingForm.Parent.Height - this.alertSettingForm.Height) / 2;
				}
				this.alertSettingForm.Show();
				this.alertSettingForm.BringToFront();
				this.alertSettingForm.Reload();
			}
			catch (Exception ex)
			{
				this.ShowError("btnNotification_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbStopOrderField_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.cbStopOrderField.SelectedIndex > -1 && this._stockInfo != null)
				{
					if (this.cbStopOrderField.Text.ToLower().IndexOf("sma") > 0)
					{
						this.lbStopPriceLable.Text = "Periods";
						this.cbStopOrderPrice.Items.Clear();
						this.cbStopOrderPrice.Text = string.Empty;
						for (int i = 2; i <= 200; i++)
						{
							if ((i >= 2 && i <= 50) || i == 75 || i == 200)
							{
								this.cbStopOrderPrice.Items.Add(i.ToString());
							}
						}
					}
					else
					{
						if (this.cbStopOrderField.Text.ToLower().IndexOf("break") > 0)
						{
							this.lbStopPriceLable.Text = "Periods";
							this.cbStopOrderPrice.Items.Clear();
							this.cbStopOrderPrice.Text = string.Empty;
							for (int j = 2; j <= 20; j++)
							{
								this.cbStopOrderPrice.Items.Add(j.ToString());
							}
						}
						else
						{
							this.lbStopPriceLable.Text = "Price";
							this.cbStopOrderPrice.Items.Clear();
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("cbStopOrderField_SelectedIndexChanged", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void chbStopOrder_CheckedChanged(object sender, EventArgs e)
		{
			if (this.chbEqStopOrder.Checked)
			{
				if (ApplicationInfo.StopOrderAccepted)
				{
					if (this._isActive)
					{
						this.SetResize();
						if (this._OnBoxStyleChanged != null)
						{
							this._OnBoxStyleChanged();
						}
					}
				}
				else
				{
					this.ShowStopDisclaimer();
				}
			}
			else
			{
				if (this._isActive)
				{
					this.SetResize();
					if (this._OnBoxStyleChanged != null)
					{
						this._OnBoxStyleChanged();
					}
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbSeries_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				Keys keyCode = e.KeyCode;
				if (keyCode != Keys.Return)
				{
					switch (keyCode)
					{
					case Keys.End:
					case Keys.Home:
					case Keys.Left:
					case Keys.Down:
						e.SuppressKeyPress = true;
						goto IL_118;
					case Keys.Up:
						goto IL_118;
					case Keys.Right:
						break;
					default:
						goto IL_118;
					}
				}
				if (this.tbSeries.Text.Trim() != string.Empty)
				{
					this._seriesInfoTfex = ApplicationInfo.SeriesInfo[this.tbSeries.Text.Trim()];
					if (!string.IsNullOrEmpty(this._seriesInfoTfex.Symbol) && this._seriesInfoTfex.Group != 5)
					{
						TemplateManager.Instance.SendSymbolLink(this, SymbolLinkSource.StockSymbol, this._seriesInfoTfex.Symbol);
						ApplicationInfo.CurrentSymbol = this._seriesInfoTfex.Symbol;
						this.tbVolumeT.Focus();
					}
					else
					{
						this.tbSeries.Focus();
					}
				}
				e.SuppressKeyPress = true;
				IL_118:;
			}
			catch (Exception ex)
			{
				this.ShowError("tbSeries_KeyUp", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbPriceT_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				Keys keyCode = e.KeyCode;
				if (keyCode != Keys.Return)
				{
					switch (keyCode)
					{
					case Keys.Left:
						this.tbVolumeT.Focus();
						this.tbVolumeT.SelectAll();
						break;
					case Keys.Right:
						if (this.IsValidPrice_TFEX(false, this.tbPriceT.Text.ToUpper().Trim()))
						{
							this.tbPublishT.Focus();
						}
						e.SuppressKeyPress = true;
						break;
					}
				}
				else
				{
					if (this.IsValidPrice_TFEX(false, this.tbPriceT.Text.ToUpper().Trim()))
					{
						this.btnSendOrderT.PerformClick();
					}
					e.SuppressKeyPress = true;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tbPriceT_KeyDown", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbPublishT_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				Keys keyCode = e.KeyCode;
				if (keyCode != Keys.Return)
				{
					switch (keyCode)
					{
					case Keys.Left:
						e.SuppressKeyPress = true;
						this.tbPriceT.Focus();
						break;
					case Keys.Right:
						this.cbPosition.Focus();
						e.SuppressKeyPress = true;
						break;
					}
				}
				else
				{
					this.btnSendOrderT.PerformClick();
					e.SuppressKeyPress = true;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tbPublish_KeyDown", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbType_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.cbType.Text == "MP" || this.cbType.Text == "MO" || this.cbType.Text == "ML")
				{
					this.tbPriceT.Text = "";
					this.tbPriceT.Enabled = false;
				}
				else
				{
					this.tbPriceT.Enabled = true;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("cbType_SelectedIndexChanged", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbValidity_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				switch (e.KeyCode)
				{
				case Keys.Left:
					this.cbType.Focus();
					e.SuppressKeyPress = true;
					break;
				case Keys.Right:
					this.cbCondition.Focus();
					e.SuppressKeyPress = true;
					break;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("cbValidity_KeyDown", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnSendOrderT_Click(object sender, EventArgs e)
		{
			try
			{
				this.DoSend_TFEX("V");
			}
			catch (Exception ex)
			{
				this.ShowError("btnSendOrderT_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnClearTextT_Click(object sender, EventArgs e)
		{
			try
			{
				this.DoClear_TFEX();
				this.tbSeries.Focus();
			}
			catch (Exception ex)
			{
				this.ShowError("btnClearTextT_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbVolumeT_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				Keys keyCode = e.KeyCode;
				if (keyCode != Keys.Return)
				{
					switch (keyCode)
					{
					case Keys.End:
					case Keys.Home:
					case Keys.Down:
						e.SuppressKeyPress = true;
						goto IL_B7;
					case Keys.Left:
						this.tbSeries.Focus();
						goto IL_B7;
					case Keys.Up:
						goto IL_B7;
					case Keys.Right:
						break;
					default:
						goto IL_B7;
					}
				}
				long num;
				long.TryParse(this.tbVolumeT.Text.Replace(",", ""), out num);
				if (num > 0L)
				{
					this.tbPriceT.Focus();
				}
				else
				{
					this.tbVolumeT.SelectAll();
				}
				e.SuppressKeyPress = true;
				IL_B7:;
			}
			catch (Exception ex)
			{
				this.ShowError("tbVolumeT_KeyDown", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbVolumeT_TextChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.tbVolumeT.Text.Trim() != string.Empty)
				{
					if (FormatUtil.Isnumeric(this.tbVolumeT.Text))
					{
						try
						{
							decimal num = Convert.ToInt64(this.tbVolumeT.Text.Replace(",", ""));
							this.tbVolumeT.Text = num.ToString("#,###");
							this.tbVolumeT.Select(this.tbVolumeT.Text.Length, 0);
							this.tbPublishT.Text = this.tbVolumeT.Text;
						}
						catch
						{
							this.tbVolumeT.Text = this.tbVolumeT.Text.Substring(0, this.tbVolumeT.Text.Length - 1);
						}
					}
					else
					{
						this.tbVolumeT.Text = this.tbVolumeT.Text.Substring(0, this.tbVolumeT.Text.Length - 1);
					}
				}
				else
				{
					this.tbPublishT.Text = this.tbVolumeT.Text;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tbVolumeT_TextChanged", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void rdbTfexBuy_CheckedChanged(object sender, EventArgs e)
		{
			if (this.rdbTfexBuy.Checked)
			{
				this._showSideTFEX = "L";
				this.SetColorBySide_TFEX();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void rdbTfexSell_CheckedChanged(object sender, EventArgs e)
		{
			if (this.rdbTfexSell.Checked)
			{
				this._showSideTFEX = "S";
				this.SetColorBySide_TFEX();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void chbTfexStopOrder_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.chbTfexStopOrder.Checked)
				{
					this.tbSeriesCondition.Visible = (this.cbTfexConStopOrder.Visible = (this.tbTfexPriceCondition.Visible = true));
					if (string.IsNullOrEmpty(this.tbSeries.Text))
					{
						this.tbSeriesCondition.Text = "SERIES";
						this.tbSeriesCondition.ForeColor = Color.Silver;
					}
					else
					{
						this.tbSeriesCondition.Text = this.tbSeries.Text;
						this.tbSeriesCondition.ForeColor = Color.Black;
					}
					this.tbTfexPriceCondition.Text = "PRICE";
					this.tbTfexPriceCondition.ForeColor = Color.Silver;
					this.cbTfexConStopOrder.SelectedIndex = 4;
				}
				else
				{
					this.tbSeriesCondition.Visible = (this.cbTfexConStopOrder.Visible = (this.tbTfexPriceCondition.Visible = false));
					this.tbSeriesCondition.Text = (this.tbTfexPriceCondition.Text = string.Empty);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("chbStopOrder_CheckedChanged", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbSeriesCondition_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
			case Keys.Left:
				this.cbValidity.Focus();
				e.SuppressKeyPress = true;
				break;
			case Keys.Right:
				this.cbTfexConStopOrder.Focus();
				e.SuppressKeyPress = true;
				break;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbTfexConStopOrder_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
			case Keys.Left:
				this.tbSeriesCondition.Focus();
				e.SuppressKeyPress = true;
				break;
			case Keys.Right:
				this.tbTfexPriceCondition.Focus();
				e.SuppressKeyPress = true;
				break;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbTfexPriceCondition_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
			case Keys.Left:
				this.cbTfexConStopOrder.Focus();
				e.SuppressKeyPress = true;
				break;
			case Keys.Right:
				e.SuppressKeyPress = true;
				break;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbType_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				Keys keyCode = e.KeyCode;
				if (keyCode != Keys.Return)
				{
					switch (keyCode)
					{
					case Keys.Left:
						this.cbPosition.Focus();
						e.SuppressKeyPress = true;
						goto IL_64;
					case Keys.Up:
						goto IL_64;
					case Keys.Right:
						break;
					default:
						goto IL_64;
					}
				}
				this.cbValidity.Focus();
				e.SuppressKeyPress = true;
				IL_64:;
			}
			catch (Exception ex)
			{
				this.ShowError("cbValidity_KeyDown", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbPosition_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				Keys keyCode = e.KeyCode;
				if (keyCode != Keys.Return)
				{
					switch (keyCode)
					{
					case Keys.Left:
						this.tbPublishT.Focus();
						e.SuppressKeyPress = true;
						goto IL_64;
					case Keys.Up:
						goto IL_64;
					case Keys.Right:
						break;
					default:
						goto IL_64;
					}
				}
				this.cbType.Focus();
				e.SuppressKeyPress = true;
				IL_64:;
			}
			catch (Exception ex)
			{
				this.ShowError("cbValidity_KeyDown", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnBResize_Up_Click(object sender, EventArgs e)
		{
			if (this._OnResizeUpDown != null)
			{
				this._OnResizeUpDown(true);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnBResize_Down_Click(object sender, EventArgs e)
		{
			if (this._OnResizeUpDown != null)
			{
				this._OnResizeUpDown(false);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbTfexPriceCondition_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '-')
			{
				e.Handled = true;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbTfexPriceCondition_MouseClick(object sender, MouseEventArgs e)
		{
			if (sender == this.tbTfexPriceCondition)
			{
				if (string.IsNullOrEmpty(this.tbTfexPriceCondition.Text))
				{
					this.tbTfexPriceCondition.Text = "PRICE";
					this.tbTfexPriceCondition.ForeColor = Color.Silver;
				}
				else
				{
					if (this.tbTfexPriceCondition.Text == "PRICE")
					{
						this.tbTfexPriceCondition.ForeColor = Color.Yellow;
						this.tbTfexPriceCondition.Text = "";
					}
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbSeriesCondition_MouseClick(object sender, MouseEventArgs e)
		{
			if (sender == this.tbSeriesCondition)
			{
				if (string.IsNullOrEmpty(this.tbSeriesCondition.Text))
				{
					this.tbSeriesCondition.Text = "SERIES";
					this.tbSeriesCondition.ForeColor = Color.Silver;
				}
				else
				{
					if (this.tbSeriesCondition.Text == "SERIES")
					{
						this.tbSeriesCondition.ForeColor = Color.Yellow;
						this.tbSeriesCondition.Text = "";
					}
				}
			}
		}
	}
}
