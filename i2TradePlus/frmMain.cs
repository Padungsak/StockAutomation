using i2TradePlus.Controls;
using i2TradePlus.FixForm;
using i2TradePlus.Information;
using i2TradePlus.ITSNetBusinessWS;
using i2TradePlus.MyDataSet;
using i2TradePlus.Properties;
using i2TradePlus.Templates;
using i2TradePlus.User_Control;
using i2TradePlus.WebProxy;
using ITSNet.Common.BIZ;
using ITSNet.Common.BIZ.RealtimeMessage;
using ITSNet.Common.BIZ.RealtimeMessage.TFEX;
using ITSNet.Common.BIZ.Tunnel;
using Microsoft.Win32;
using nsoftware.IPWorksSSL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
namespace i2TradePlus
{
	internal class frmMain : Form
	{
		internal delegate void OnMessageRecievedEventHendler(IBroadcastMessage message, StockList.StockInformation stockInfo);
		internal delegate void OnMessageTfexRecievedEventHendler(IBroadcastMessage message, SeriesList.SeriesInformation seriesInfo);
		private delegate void OnAlertCallback(AlertItem e);
		private delegate void showSpashFormCallBack(string message);
		private delegate void LogoutCallBack(bool isForce);
		private delegate void DisplayMessageBoxCallBack(string message);
		private delegate void SetTopPanelActiveCallBack(bool isStart);
		private delegate void RegistrationToTunnelCallBack();
		private delegate void UpdateAutoCompleteCallBack(string symbol);
		private const int NUMLOCK = 144;
		private IContainer components = null;
		private System.Windows.Forms.Timer timerCallLoginForm;
		private ucBroadcastMessage BroadcastMessageBox;
		private Panel panBottom;
		private System.Windows.Forms.Timer timerGetOrderInfo;
		private Button btnResizeMD1;
		private Panel panelSep;
		private ucViewOrder ViewOrderBox;
		public ucSendNewOrder SendOrderBox;
		private Panel panelTop;
		private Button btnLogout;
		private PictureBox pici2Logo;
		private Panel panelFontAdjust;
		private CheckBox chbBold;
		private ToolTip toolTip1;
		private ComboBox cbFontSize;
		private ComboBox cbDefaultFontName;
		private Button btnFontDone;
		private Panel panelControlDockR;
		private Panel panelDockR;
		private ucTickerSlide TickerSlideBox;
		private Button btnShowTickerSlide;
		private Panel panelMenu;
		private Button btnPortfolio;
		private Button btnViewOrder;
		private Button btnSummary;
		private Button btnTopBBOs;
		private Button btnMarketWatch;
		private Button btnMarketInfo;
		private Button btnNews;
		private System.Windows.Forms.Timer timerMonitorFeed;
		private Button btnAdjFont2;
		private Button btnOptions2;
		private ucSmart1Click Smart1ClickBox;
		private Button btnShowSpeedOrderSlide;
		private Button btnStockHistory;
		private Button btnBatchOrder;
		private ucIndexBox SETBox;
		private ucIndexBox SET2Box;
		private ucIndexBox SectorBox;
		private ucMarketStateBox MarketStateBox;
		private Button btnFacebook;
		private Button btnRanking;
		private System.Windows.Forms.Timer timer1;
		private Button btnEservice;
				private FeedsWebProxy feedsWebProxy = null;
		private System.Timers.Timer timerHeartBeat = null;
		private bool _isResizeMDNext = false;
		private Ipports ipwIPPortMain = null;
		private frmAlertDetail _alertDetailForm = null;
		private frmSystemOption _systemOptionForm = null;
		private BackgroundWorker bgwLoadProfile = null;
		private BackgroundWorker bgwLogout = null;
		private bool _isCheckHBworking = false;
		private BroadcastMessageFactory _bcMessageFactory = null;
		private bool _isSessionTimout = false;
		private int _connectionPullCounter = 0;
		private DateTime _lastEcho;
		private bool _isPushConnected = false;
		private string param = string.Empty;
		private frmSplash splashForm = null;
		private PowerModes _powerMode = PowerModes.StatusChange;
		private System.Windows.Forms.Timer timmerStartBC = null;
		private bool isTerminate = false;
		private bool IsOpeningTemplate = false;
		private DateTime OpenTemplateLastTime = DateTime.MinValue;
		private string _lastTemplate = string.Empty;
		private System.Windows.Forms.Timer tmBottomClientResize = null;
		private int _bTop;
		private bool _isPanelSepResize = false;
		private Form _formRS = null;
		private Rectangle _screenRectangle;
		private bool _isFontLock = false;
		private int _slideType = 1;
		private Queue<string> _qMessage = null;
		private bool _isRunPeekQ = false;
		private bool _isPeekQWorking = false;
		
        internal static frmMain.OnMessageRecievedEventHendler _OnMessageReceived;
		internal static event frmMain.OnMessageRecievedEventHendler OnMessageReceived
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                frmMain._OnMessageReceived += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                frmMain._OnMessageReceived -= value;
			}
		}
        internal static frmMain.OnMessageTfexRecievedEventHendler _OnMessageTfexReceived;
		internal static event frmMain.OnMessageTfexRecievedEventHendler OnMessageTfexReceived
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				frmMain._OnMessageTfexReceived += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				frmMain._OnMessageTfexReceived -= value;
			}
		}
		private int _BTop
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				int num = base.ClientSize.Height - this.SendOrderBox.Height - 2;
				if (this._bTop > num)
				{
					this._bTop = num;
				}
				else
				{
					if (this._bTop <= 0)
					{
						this._bTop = (int)((double)base.Height * 0.75);
					}
					else
					{
						if ((double)this._bTop < (double)base.Height * 0.6)
						{
							this._bTop = (int)((double)base.Height * 0.6);
						}
					}
				}
				return this._bTop;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this._bTop = value;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmMain));
			this.timerCallLoginForm = new System.Windows.Forms.Timer(this.components);
			this.panBottom = new Panel();
			this.btnResizeMD1 = new Button();
			this.ViewOrderBox = new ucViewOrder();
			this.SendOrderBox = new ucSendNewOrder();
			this.BroadcastMessageBox = new ucBroadcastMessage();
			this.timerGetOrderInfo = new System.Windows.Forms.Timer(this.components);
			this.panelSep = new Panel();
			this.panelTop = new Panel();
			this.MarketStateBox = new ucMarketStateBox();
			this.SET2Box = new ucIndexBox();
			this.SETBox = new ucIndexBox();
			this.pici2Logo = new PictureBox();
			this.SectorBox = new ucIndexBox();
			this.btnLogout = new Button();
			this.panelFontAdjust = new Panel();
			this.btnFontDone = new Button();
			this.cbFontSize = new ComboBox();
			this.cbDefaultFontName = new ComboBox();
			this.chbBold = new CheckBox();
			this.toolTip1 = new ToolTip(this.components);
			this.btnShowTickerSlide = new Button();
			this.btnShowSpeedOrderSlide = new Button();
			this.btnOptions2 = new Button();
			this.btnAdjFont2 = new Button();
			this.btnNews = new Button();
			this.btnFacebook = new Button();
			this.panelControlDockR = new Panel();
			this.panelDockR = new Panel();
			this.Smart1ClickBox = new ucSmart1Click();
			this.TickerSlideBox = new ucTickerSlide();
			this.panelMenu = new Panel();
			this.btnEservice = new Button();
			this.btnRanking = new Button();
			this.btnBatchOrder = new Button();
			this.btnStockHistory = new Button();
			this.btnMarketInfo = new Button();
			this.btnPortfolio = new Button();
			this.btnViewOrder = new Button();
			this.btnSummary = new Button();
			this.btnTopBBOs = new Button();
			this.btnMarketWatch = new Button();
			this.timerMonitorFeed = new System.Windows.Forms.Timer(this.components);
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.panBottom.SuspendLayout();
			this.panelTop.SuspendLayout();
			((ISupportInitialize)this.pici2Logo).BeginInit();
			this.panelFontAdjust.SuspendLayout();
			this.panelControlDockR.SuspendLayout();
			this.panelDockR.SuspendLayout();
			this.panelMenu.SuspendLayout();
			base.SuspendLayout();
			this.timerCallLoginForm.Interval = 200;
			this.timerCallLoginForm.Tick += new EventHandler(this.timerCallLoginForm_Tick);
			this.panBottom.BackColor = SystemColors.ControlDarkDark;
			this.panBottom.Controls.Add(this.btnResizeMD1);
			this.panBottom.Controls.Add(this.ViewOrderBox);
			this.panBottom.Controls.Add(this.SendOrderBox);
			this.panBottom.Controls.Add(this.BroadcastMessageBox);
			this.panBottom.Dock = DockStyle.Bottom;
			this.panBottom.Location = new Point(0, 390);
			this.panBottom.Margin = new Padding(0);
			this.panBottom.Name = "panBottom";
			this.panBottom.Padding = new Padding(1);
			this.panBottom.Size = new Size(1158, 174);
			this.panBottom.TabIndex = 67;
			this.panBottom.Visible = false;
			this.panBottom.Paint += new PaintEventHandler(this.panBottom_Paint);
			this.btnResizeMD1.BackColor = Color.FromArgb(10, 10, 10);
			this.btnResizeMD1.FlatAppearance.BorderColor = Color.Gray;
			this.btnResizeMD1.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnResizeMD1.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 192, 192);
			this.btnResizeMD1.FlatStyle = FlatStyle.Flat;
			this.btnResizeMD1.ForeColor = Color.DarkGray;
			this.btnResizeMD1.Image = Resources.MovePreviousHS;
			this.btnResizeMD1.Location = new Point(597, 52);
			this.btnResizeMD1.Margin = new Padding(0);
			this.btnResizeMD1.Name = "btnResizeMD1";
			this.btnResizeMD1.Size = new Size(17, 121);
			this.btnResizeMD1.TabIndex = 97;
			this.btnResizeMD1.TabStop = false;
			this.btnResizeMD1.UseVisualStyleBackColor = false;
			this.btnResizeMD1.Visible = false;
			this.btnResizeMD1.Click += new EventHandler(this.btnResizeMD_Click);
			this.ViewOrderBox.BackColor = Color.FromArgb(64, 64, 64);
			this.ViewOrderBox.IsActive = false;
			this.ViewOrderBox.IsLoadingData = false;
			this.ViewOrderBox.IsShowLoadingControl = true;
			this.ViewOrderBox.IsShowNextPage = true;
			this.ViewOrderBox.IsShowToolsBar = true;
			this.ViewOrderBox.Location = new Point(5, 64);
			this.ViewOrderBox.Margin = new Padding(0);
			this.ViewOrderBox.Name = "ViewOrderBox";
			this.ViewOrderBox.ShowOnMainForm = true;
			this.ViewOrderBox.Size = new Size(592, 95);
			this.ViewOrderBox.TabIndex = 93;
			this.ViewOrderBox.Visible = false;
			this.SendOrderBox.AllowDrop = true;
			this.SendOrderBox.BackColor = Color.DimGray;
			this.SendOrderBox.Font = new Font("Microsoft Sans Serif", 8.25f);
			this.SendOrderBox.ForeColor = Color.White;
			this.SendOrderBox.IsActive = false;
			this.SendOrderBox.Location = new Point(1, 1);
			this.SendOrderBox.Margin = new Padding(0);
			this.SendOrderBox.Name = "SendOrderBox";
			this.SendOrderBox.Size = new Size(845, 50);
			this.SendOrderBox.TabIndex = 94;
			this.SendOrderBox.TabStop = false;
			this.SendOrderBox.Visible = false;
			this.SendOrderBox.OnResized += new ucSendNewOrder.OnResizedHandler(this.SendOrderBox_OnResized);
			this.SendOrderBox.OnBoxStyleChanged += new ucSendNewOrder.OnBoxStyleChangedHandler(this.SendOrderBox_OnBoxStyleChanged);
			this.SendOrderBox.OnNewStopOrder += new ucSendNewOrder.OnNewStopOrderHandler(this.SendOrderBox_OnNewStopOrder);
			this.SendOrderBox.OnResizeUpDown += new ucSendNewOrder.OnResizeUpDownHandler(this.SendOrderBox_OnResizeUpDown);
			this.SendOrderBox.OnAccountChanged += new ucSendNewOrder.OnAccountChangedHandler(this.SendOrderBox_OnAccountChanged);
			this.BroadcastMessageBox.BackColor = SystemColors.Control;
			this.BroadcastMessageBox.IsFirstOpen = true;
			this.BroadcastMessageBox.Location = new Point(615, 52);
			this.BroadcastMessageBox.Margin = new Padding(1);
			this.BroadcastMessageBox.Name = "BroadcastMessageBox";
			this.BroadcastMessageBox.Padding = new Padding(1);
			this.BroadcastMessageBox.Size = new Size(231, 123);
			this.BroadcastMessageBox.TabIndex = 0;
			this.BroadcastMessageBox.Visible = false;
			this.timerGetOrderInfo.Interval = 1000;
			this.timerGetOrderInfo.Tick += new EventHandler(this.timerGetOrderInfo_Tick);
			this.panelSep.BackColor = Color.LightGray;
			this.panelSep.Cursor = Cursors.HSplit;
			this.panelSep.Location = new Point(22, 378);
			this.panelSep.Name = "panelSep";
			this.panelSep.Size = new Size(799, 3);
			this.panelSep.TabIndex = 69;
			this.panelSep.Visible = false;
			this.panelSep.MouseMove += new MouseEventHandler(this.panelSep_MouseMove);
			this.panelSep.MouseDown += new MouseEventHandler(this.panelSep_MouseDown);
			this.panelSep.MouseUp += new MouseEventHandler(this.panelSep_MouseUp);
			this.panelTop.BackColor = Color.FromArgb(10, 10, 10);
			this.panelTop.Controls.Add(this.MarketStateBox);
			this.panelTop.Controls.Add(this.SET2Box);
			this.panelTop.Controls.Add(this.SETBox);
			this.panelTop.Controls.Add(this.pici2Logo);
			this.panelTop.Controls.Add(this.SectorBox);
			this.panelTop.Dock = DockStyle.Top;
			this.panelTop.Location = new Point(0, 0);
			this.panelTop.Name = "panelTop";
			this.panelTop.Size = new Size(1158, 53);
			this.panelTop.TabIndex = 70;
			this.MarketStateBox.AlterMessageCount = 0;
			this.MarketStateBox.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right);
			this.MarketStateBox.BackColor = Color.Black;
			this.MarketStateBox.IsAlertStart = false;
			this.MarketStateBox.IsAllowBlinkAlert = false;
			this.MarketStateBox.Location = new Point(985, 2);
			this.MarketStateBox.Margin = new Padding(4);
			this.MarketStateBox.Name = "MarketStateBox";
			this.MarketStateBox.Size = new Size(170, 49);
			this.MarketStateBox.TabIndex = 62;
			this.MarketStateBox.OnSitchMode += new ucMarketStateBox.SwitchMode(this.MarketStateBox_OnSitchMode);
			this.MarketStateBox.OnCallAlert += new ucMarketStateBox.CallAlert(this.MarketStateBox_OnCallAlert);
			this.SET2Box.Active = false;
			this.SET2Box.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left);
			this.SET2Box.BackColor = Color.Black;
			this.SET2Box.DisplaySector = 0;
			this.SET2Box.DisplaySET = "SET50";
			this.SET2Box.DisplayType = 1;
			this.SET2Box.Location = new Point(289, 2);
			this.SET2Box.Margin = new Padding(4);
			this.SET2Box.Name = "SET2Box";
			this.SET2Box.Size = new Size(270, 49);
			this.SET2Box.TabIndex = 60;
			this.SETBox.Active = false;
			this.SETBox.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left);
			this.SETBox.BackColor = Color.Black;
			this.SETBox.DisplaySector = 0;
			this.SETBox.DisplaySET = "SET";
			this.SETBox.DisplayType = 1;
			this.SETBox.Location = new Point(57, 2);
			this.SETBox.Margin = new Padding(4);
			this.SETBox.Name = "SETBox";
			this.SETBox.Size = new Size(227, 49);
			this.SETBox.TabIndex = 59;
			this.pici2Logo.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left);
			this.pici2Logo.BackColor = Color.Transparent;
			this.pici2Logo.Image = (Image)componentResourceManager.GetObject("pici2Logo.Image");
			this.pici2Logo.Location = new Point(2, 2);
			this.pici2Logo.Name = "pici2Logo";
			this.pici2Logo.Padding = new Padding(5);
			this.pici2Logo.Size = new Size(50, 48);
			this.pici2Logo.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pici2Logo.TabIndex = 12;
			this.pici2Logo.TabStop = false;
			this.SectorBox.Active = false;
			this.SectorBox.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left);
			this.SectorBox.BackColor = Color.Black;
			this.SectorBox.DisplaySector = 1;
			this.SectorBox.DisplaySET = "";
			this.SectorBox.DisplayType = 3;
			this.SectorBox.Location = new Point(564, 2);
			this.SectorBox.Margin = new Padding(4);
			this.SectorBox.Name = "SectorBox";
			this.SectorBox.Size = new Size(222, 49);
			this.SectorBox.TabIndex = 61;
			this.btnLogout.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right);
			this.btnLogout.BackColor = Color.Transparent;
			this.btnLogout.FlatAppearance.BorderColor = Color.DimGray;
			this.btnLogout.FlatAppearance.BorderSize = 0;
			this.btnLogout.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnLogout.FlatAppearance.MouseOverBackColor = Color.Gray;
			this.btnLogout.FlatStyle = FlatStyle.Flat;
			this.btnLogout.Font = new Font("Segoe UI", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnLogout.ForeColor = Color.LightGray;
			this.btnLogout.Location = new Point(160, 2);
			this.btnLogout.Name = "btnLogout";
			this.btnLogout.Padding = new Padding(1, 0, 1, 0);
			this.btnLogout.Size = new Size(68, 24);
			this.btnLogout.TabIndex = 11;
			this.btnLogout.Tag = "0";
			this.btnLogout.Text = "Logout";
			this.btnLogout.TextAlign = ContentAlignment.TopCenter;
			this.btnLogout.UseVisualStyleBackColor = false;
			this.btnLogout.Click += new EventHandler(this.btnLogout_Click);
			this.panelFontAdjust.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.panelFontAdjust.BackColor = Color.SkyBlue;
			this.panelFontAdjust.Controls.Add(this.btnFontDone);
			this.panelFontAdjust.Controls.Add(this.cbFontSize);
			this.panelFontAdjust.Controls.Add(this.cbDefaultFontName);
			this.panelFontAdjust.Controls.Add(this.chbBold);
			this.panelFontAdjust.Location = new Point(445, 103);
			this.panelFontAdjust.Name = "panelFontAdjust";
			this.panelFontAdjust.Size = new Size(281, 35);
			this.panelFontAdjust.TabIndex = 71;
			this.panelFontAdjust.Visible = false;
			this.panelFontAdjust.Paint += new PaintEventHandler(this.panelFontAdjust_Paint);
			this.btnFontDone.FlatAppearance.BorderColor = Color.LightGray;
			this.btnFontDone.FlatAppearance.BorderSize = 0;
			this.btnFontDone.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnFontDone.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 192, 192);
			this.btnFontDone.FlatStyle = FlatStyle.Flat;
			this.btnFontDone.ForeColor = Color.WhiteSmoke;
			this.btnFontDone.Image = Resources.fileclose;
			this.btnFontDone.Location = new Point(248, 3);
			this.btnFontDone.Name = "btnFontDone";
			this.btnFontDone.Size = new Size(29, 29);
			this.btnFontDone.TabIndex = 15;
			this.btnFontDone.UseVisualStyleBackColor = true;
			this.btnFontDone.Click += new EventHandler(this.btnFontDone_Click);
			this.cbFontSize.BackColor = Color.White;
			this.cbFontSize.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbFontSize.FlatStyle = FlatStyle.Flat;
			this.cbFontSize.ForeColor = Color.Black;
			this.cbFontSize.FormattingEnabled = true;
			this.cbFontSize.Items.AddRange(new object[]
			{
				"8",
				"9",
				"10",
				"11",
				"12",
				"14",
				"16",
				"18",
				"20"
			});
			this.cbFontSize.Location = new Point(144, 7);
			this.cbFontSize.Name = "cbFontSize";
			this.cbFontSize.Size = new Size(44, 21);
			this.cbFontSize.TabIndex = 14;
			this.cbFontSize.SelectedIndexChanged += new EventHandler(this.cbFontSize_SelectedIndexChanged);
			this.cbDefaultFontName.BackColor = Color.White;
			this.cbDefaultFontName.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbDefaultFontName.FlatStyle = FlatStyle.Flat;
			this.cbDefaultFontName.ForeColor = Color.Black;
			this.cbDefaultFontName.FormattingEnabled = true;
			this.cbDefaultFontName.Items.AddRange(new object[]
			{
				"Microsoft Sans Serif",
				"Tahoma",
				"Arial Narrow",
				"Segoe UI"
			});
			this.cbDefaultFontName.Location = new Point(5, 7);
			this.cbDefaultFontName.Name = "cbDefaultFontName";
			this.cbDefaultFontName.Size = new Size(135, 21);
			this.cbDefaultFontName.TabIndex = 12;
			this.cbDefaultFontName.SelectedIndexChanged += new EventHandler(this.cbDefaultFontName_SelectedIndexChanged);
			this.chbBold.AutoSize = true;
			this.chbBold.BackColor = Color.Transparent;
			this.chbBold.ForeColor = Color.Black;
			this.chbBold.Location = new Point(192, 9);
			this.chbBold.Name = "chbBold";
			this.chbBold.Size = new Size(47, 17);
			this.chbBold.TabIndex = 7;
			this.chbBold.Text = "Bold";
			this.chbBold.UseVisualStyleBackColor = false;
			this.chbBold.Click += new EventHandler(this.tsbtnFontBold_Click);
			this.toolTip1.IsBalloon = true;
			this.toolTip1.ToolTipTitle = "Info guide";
			this.btnShowTickerSlide.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left);
			this.btnShowTickerSlide.BackColor = Color.Transparent;
			this.btnShowTickerSlide.FlatAppearance.BorderColor = Color.DimGray;
			this.btnShowTickerSlide.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnShowTickerSlide.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 192, 192);
			this.btnShowTickerSlide.FlatStyle = FlatStyle.Flat;
			this.btnShowTickerSlide.Font = new Font("Segoe UI", 9f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.btnShowTickerSlide.ForeColor = Color.Cyan;
			this.btnShowTickerSlide.Location = new Point(3, 2);
			this.btnShowTickerSlide.Name = "btnShowTickerSlide";
			this.btnShowTickerSlide.Size = new Size(25, 24);
			this.btnShowTickerSlide.TabIndex = 0;
			this.btnShowTickerSlide.TabStop = false;
			this.btnShowTickerSlide.Tag = "1";
			this.btnShowTickerSlide.Text = "T";
			this.toolTip1.SetToolTip(this.btnShowTickerSlide, "Ticker Panel");
			this.btnShowTickerSlide.UseVisualStyleBackColor = false;
			this.btnShowTickerSlide.Click += new EventHandler(this.btnShowSlide_Click);
			this.btnShowSpeedOrderSlide.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left);
			this.btnShowSpeedOrderSlide.BackColor = Color.Transparent;
			this.btnShowSpeedOrderSlide.FlatAppearance.BorderColor = Color.DimGray;
			this.btnShowSpeedOrderSlide.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnShowSpeedOrderSlide.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 192, 192);
			this.btnShowSpeedOrderSlide.FlatStyle = FlatStyle.Flat;
			this.btnShowSpeedOrderSlide.Font = new Font("Segoe UI", 9f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.btnShowSpeedOrderSlide.ForeColor = Color.LightGray;
			this.btnShowSpeedOrderSlide.Location = new Point(27, 2);
			this.btnShowSpeedOrderSlide.Name = "btnShowSpeedOrderSlide";
			this.btnShowSpeedOrderSlide.Size = new Size(25, 24);
			this.btnShowSpeedOrderSlide.TabIndex = 12;
			this.btnShowSpeedOrderSlide.TabStop = false;
			this.btnShowSpeedOrderSlide.Tag = "2";
			this.btnShowSpeedOrderSlide.Text = "S";
			this.toolTip1.SetToolTip(this.btnShowSpeedOrderSlide, "Smart One Click Panel");
			this.btnShowSpeedOrderSlide.UseVisualStyleBackColor = false;
			this.btnShowSpeedOrderSlide.Click += new EventHandler(this.btnShowSlide_Click);
			this.btnOptions2.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right);
			this.btnOptions2.BackColor = Color.Transparent;
			this.btnOptions2.FlatAppearance.BorderColor = Color.DimGray;
			this.btnOptions2.FlatAppearance.BorderSize = 0;
			this.btnOptions2.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnOptions2.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 192, 192);
			this.btnOptions2.FlatStyle = FlatStyle.Flat;
			this.btnOptions2.ForeColor = Color.LightGray;
			this.btnOptions2.Image = (Image)componentResourceManager.GetObject("btnOptions2.Image");
			this.btnOptions2.Location = new Point(137, 2);
			this.btnOptions2.Name = "btnOptions2";
			this.btnOptions2.Size = new Size(25, 24);
			this.btnOptions2.TabIndex = 2;
			this.btnOptions2.TabStop = false;
			this.toolTip1.SetToolTip(this.btnOptions2, "Options");
			this.btnOptions2.UseVisualStyleBackColor = false;
			this.btnOptions2.Click += new EventHandler(this.btnOptions_Click);
			this.btnAdjFont2.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right);
			this.btnAdjFont2.BackColor = Color.Transparent;
			this.btnAdjFont2.FlatAppearance.BorderColor = Color.DimGray;
			this.btnAdjFont2.FlatAppearance.BorderSize = 0;
			this.btnAdjFont2.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnAdjFont2.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 192, 192);
			this.btnAdjFont2.FlatStyle = FlatStyle.Flat;
			this.btnAdjFont2.ForeColor = Color.LightGray;
			this.btnAdjFont2.Image = (Image)componentResourceManager.GetObject("btnAdjFont2.Image");
			this.btnAdjFont2.Location = new Point(113, 2);
			this.btnAdjFont2.Name = "btnAdjFont2";
			this.btnAdjFont2.Size = new Size(25, 24);
			this.btnAdjFont2.TabIndex = 4;
			this.btnAdjFont2.TabStop = false;
			this.toolTip1.SetToolTip(this.btnAdjFont2, "Adjust Font");
			this.btnAdjFont2.UseVisualStyleBackColor = false;
			this.btnAdjFont2.Click += new EventHandler(this.btnAdjFont_Click);
			this.btnNews.AutoSize = true;
			this.btnNews.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.btnNews.BackColor = Color.Transparent;
			this.btnNews.FlatAppearance.BorderColor = Color.Gray;
			this.btnNews.FlatAppearance.BorderSize = 0;
			this.btnNews.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnNews.FlatAppearance.MouseOverBackColor = Color.Gray;
			this.btnNews.FlatStyle = FlatStyle.Flat;
			this.btnNews.Font = new Font("Segoe UI", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnNews.ForeColor = Color.LightGray;
			this.btnNews.Location = new Point(385, 1);
			this.btnNews.Name = "btnNews";
			this.btnNews.Padding = new Padding(1, 0, 1, 0);
			this.btnNews.Size = new Size(52, 27);
			this.btnNews.TabIndex = 13;
			this.btnNews.Tag = "0";
			this.btnNews.Text = "News";
			this.btnNews.TextAlign = ContentAlignment.TopCenter;
			this.toolTip1.SetToolTip(this.btnNews, "ข่าว / ข้อมูลหลักทรัพย์");
			this.btnNews.UseVisualStyleBackColor = false;
			this.btnNews.Click += new EventHandler(this.Menus_Click);
			this.btnFacebook.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right);
			this.btnFacebook.BackColor = Color.Transparent;
			this.btnFacebook.FlatAppearance.BorderColor = Color.DimGray;
			this.btnFacebook.FlatAppearance.BorderSize = 0;
			this.btnFacebook.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnFacebook.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 192, 192);
			this.btnFacebook.FlatStyle = FlatStyle.Flat;
			this.btnFacebook.Font = new Font("Segoe UI", 9f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.btnFacebook.ForeColor = Color.LightGray;
			this.btnFacebook.Image = (Image)componentResourceManager.GetObject("btnFacebook.Image");
			this.btnFacebook.Location = new Point(89, 2);
			this.btnFacebook.Name = "btnFacebook";
			this.btnFacebook.Size = new Size(25, 24);
			this.btnFacebook.TabIndex = 75;
			this.btnFacebook.TabStop = false;
			this.btnFacebook.Tag = "2";
			this.toolTip1.SetToolTip(this.btnFacebook, "Goto i2Trade facebook");
			this.btnFacebook.UseVisualStyleBackColor = false;
			this.btnFacebook.Click += new EventHandler(this.btnFacebook_Click);
			this.panelControlDockR.BackColor = Color.FromArgb(20, 20, 20);
			this.panelControlDockR.Controls.Add(this.btnFacebook);
			this.panelControlDockR.Controls.Add(this.btnLogout);
			this.panelControlDockR.Controls.Add(this.btnOptions2);
			this.panelControlDockR.Controls.Add(this.btnAdjFont2);
			this.panelControlDockR.Controls.Add(this.btnShowSpeedOrderSlide);
			this.panelControlDockR.Controls.Add(this.btnShowTickerSlide);
			this.panelControlDockR.Location = new Point(825, 51);
			this.panelControlDockR.Name = "panelControlDockR";
			this.panelControlDockR.Padding = new Padding(0, 0, 0, 1);
			this.panelControlDockR.Size = new Size(230, 28);
			this.panelControlDockR.TabIndex = 72;
			this.panelControlDockR.Visible = false;
			this.panelControlDockR.Paint += new PaintEventHandler(this.panelControlDockR_Paint);
			this.panelDockR.BackColor = Color.FromArgb(64, 64, 64);
			this.panelDockR.Controls.Add(this.Smart1ClickBox);
			this.panelDockR.Controls.Add(this.TickerSlideBox);
			this.panelDockR.Location = new Point(825, 87);
			this.panelDockR.Name = "panelDockR";
			this.panelDockR.Size = new Size(255, 272);
			this.panelDockR.TabIndex = 73;
			this.panelDockR.Visible = false;
			this.Smart1ClickBox.IsLoadingData = true;
			this.Smart1ClickBox.Location = new Point(10, 98);
			this.Smart1ClickBox.Margin = new Padding(4);
			this.Smart1ClickBox.Name = "Smart1ClickBox";
			this.Smart1ClickBox.Size = new Size(242, 26);
			this.Smart1ClickBox.TabIndex = 2;
			this.Smart1ClickBox.Visible = false;
			this.TickerSlideBox.Location = new Point(14, 3);
			this.TickerSlideBox.Margin = new Padding(4);
			this.TickerSlideBox.Name = "TickerSlideBox";
			this.TickerSlideBox.Size = new Size(268, 54);
			this.TickerSlideBox.TabIndex = 0;
			this.TickerSlideBox.Visible = false;
			this.panelMenu.BackColor = Color.FromArgb(20, 20, 20);
			this.panelMenu.Controls.Add(this.btnEservice);
			this.panelMenu.Controls.Add(this.btnRanking);
			this.panelMenu.Controls.Add(this.btnBatchOrder);
			this.panelMenu.Controls.Add(this.btnStockHistory);
			this.panelMenu.Controls.Add(this.btnNews);
			this.panelMenu.Controls.Add(this.btnMarketInfo);
			this.panelMenu.Controls.Add(this.btnPortfolio);
			this.panelMenu.Controls.Add(this.btnViewOrder);
			this.panelMenu.Controls.Add(this.btnSummary);
			this.panelMenu.Controls.Add(this.btnTopBBOs);
			this.panelMenu.Controls.Add(this.btnMarketWatch);
			this.panelMenu.Location = new Point(2, 63);
			this.panelMenu.Name = "panelMenu";
			this.panelMenu.Size = new Size(820, 31);
			this.panelMenu.TabIndex = 74;
			this.panelMenu.Visible = false;
			this.btnEservice.AutoSize = true;
			this.btnEservice.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.btnEservice.BackColor = Color.Transparent;
			this.btnEservice.FlatAppearance.BorderColor = Color.Gray;
			this.btnEservice.FlatAppearance.BorderSize = 0;
			this.btnEservice.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnEservice.FlatAppearance.MouseOverBackColor = Color.Gray;
			this.btnEservice.FlatStyle = FlatStyle.Flat;
			this.btnEservice.Font = new Font("Segoe UI", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnEservice.ForeColor = Color.LightGray;
			this.btnEservice.Location = new Point(744, 1);
			this.btnEservice.Name = "btnEservice";
			this.btnEservice.Padding = new Padding(1, 0, 1, 0);
			this.btnEservice.Size = new Size(73, 27);
			this.btnEservice.TabIndex = 17;
			this.btnEservice.Tag = "0";
			this.btnEservice.Text = "e-Service";
			this.btnEservice.TextAlign = ContentAlignment.TopCenter;
			this.btnEservice.UseVisualStyleBackColor = false;
			this.btnEservice.Visible = false;
			this.btnEservice.Click += new EventHandler(this.Menus_Click);
			this.btnRanking.AutoSize = true;
			this.btnRanking.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.btnRanking.BackColor = Color.Transparent;
			this.btnRanking.FlatAppearance.BorderColor = Color.Gray;
			this.btnRanking.FlatAppearance.BorderSize = 0;
			this.btnRanking.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnRanking.FlatAppearance.MouseOverBackColor = Color.Gray;
			this.btnRanking.FlatStyle = FlatStyle.Flat;
			this.btnRanking.Font = new Font("Segoe UI", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnRanking.ForeColor = Color.LightGray;
			this.btnRanking.Location = new Point(255, 1);
			this.btnRanking.Name = "btnRanking";
			this.btnRanking.Padding = new Padding(1, 0, 1, 0);
			this.btnRanking.Size = new Size(66, 27);
			this.btnRanking.TabIndex = 16;
			this.btnRanking.Tag = "0";
			this.btnRanking.Text = "Ranking";
			this.btnRanking.TextAlign = ContentAlignment.TopCenter;
			this.btnRanking.UseVisualStyleBackColor = false;
			this.btnRanking.Click += new EventHandler(this.Menus_Click);
			this.btnBatchOrder.AutoSize = true;
			this.btnBatchOrder.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.btnBatchOrder.BackColor = Color.Transparent;
			this.btnBatchOrder.FlatAppearance.BorderColor = Color.Gray;
			this.btnBatchOrder.FlatAppearance.BorderSize = 0;
			this.btnBatchOrder.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnBatchOrder.FlatAppearance.MouseOverBackColor = Color.Gray;
			this.btnBatchOrder.FlatStyle = FlatStyle.Flat;
			this.btnBatchOrder.Font = new Font("Segoe UI", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnBatchOrder.ForeColor = Color.LightGray;
			this.btnBatchOrder.Location = new Point(644, 1);
			this.btnBatchOrder.Name = "btnBatchOrder";
			this.btnBatchOrder.Padding = new Padding(1, 0, 1, 0);
			this.btnBatchOrder.Size = new Size(90, 27);
			this.btnBatchOrder.TabIndex = 15;
			this.btnBatchOrder.Tag = "0";
			this.btnBatchOrder.Text = "Batch Order";
			this.btnBatchOrder.TextAlign = ContentAlignment.TopCenter;
			this.btnBatchOrder.UseVisualStyleBackColor = false;
			this.btnBatchOrder.Visible = false;
			this.btnBatchOrder.Click += new EventHandler(this.Menus_Click);
			this.btnStockHistory.AutoSize = true;
			this.btnStockHistory.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.btnStockHistory.BackColor = Color.Transparent;
			this.btnStockHistory.FlatAppearance.BorderColor = Color.Gray;
			this.btnStockHistory.FlatAppearance.BorderSize = 0;
			this.btnStockHistory.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnStockHistory.FlatAppearance.MouseOverBackColor = Color.Gray;
			this.btnStockHistory.FlatStyle = FlatStyle.Flat;
			this.btnStockHistory.Font = new Font("Segoe UI", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnStockHistory.ForeColor = Color.LightGray;
			this.btnStockHistory.Location = new Point(593, 1);
			this.btnStockHistory.Name = "btnStockHistory";
			this.btnStockHistory.Padding = new Padding(1, 0, 1, 0);
			this.btnStockHistory.Size = new Size(51, 27);
			this.btnStockHistory.TabIndex = 14;
			this.btnStockHistory.Tag = "0";
			this.btnStockHistory.Text = "Chart";
			this.btnStockHistory.TextAlign = ContentAlignment.TopCenter;
			this.btnStockHistory.UseVisualStyleBackColor = false;
			this.btnStockHistory.Click += new EventHandler(this.Menus_Click);
			this.btnMarketInfo.AutoSize = true;
			this.btnMarketInfo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.btnMarketInfo.BackColor = Color.Transparent;
			this.btnMarketInfo.FlatAppearance.BorderColor = Color.Gray;
			this.btnMarketInfo.FlatAppearance.BorderSize = 0;
			this.btnMarketInfo.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnMarketInfo.FlatAppearance.MouseOverBackColor = Color.Gray;
			this.btnMarketInfo.FlatStyle = FlatStyle.Flat;
			this.btnMarketInfo.Font = new Font("Segoe UI", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnMarketInfo.ForeColor = Color.LightGray;
			this.btnMarketInfo.Location = new Point(324, 1);
			this.btnMarketInfo.Name = "btnMarketInfo";
			this.btnMarketInfo.Padding = new Padding(1, 0, 1, 0);
			this.btnMarketInfo.Size = new Size(61, 27);
			this.btnMarketInfo.TabIndex = 12;
			this.btnMarketInfo.Tag = "0";
			this.btnMarketInfo.Text = "Market";
			this.btnMarketInfo.TextAlign = ContentAlignment.TopCenter;
			this.btnMarketInfo.UseVisualStyleBackColor = false;
			this.btnMarketInfo.Click += new EventHandler(this.Menus_Click);
			this.btnPortfolio.AutoSize = true;
			this.btnPortfolio.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.btnPortfolio.BackColor = Color.Transparent;
			this.btnPortfolio.FlatAppearance.BorderColor = Color.Gray;
			this.btnPortfolio.FlatAppearance.BorderSize = 0;
			this.btnPortfolio.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnPortfolio.FlatAppearance.MouseOverBackColor = Color.Gray;
			this.btnPortfolio.FlatStyle = FlatStyle.Flat;
			this.btnPortfolio.Font = new Font("Segoe UI", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnPortfolio.ForeColor = Color.LightGray;
			this.btnPortfolio.Location = new Point(523, 1);
			this.btnPortfolio.Name = "btnPortfolio";
			this.btnPortfolio.Padding = new Padding(1, 0, 1, 0);
			this.btnPortfolio.Size = new Size(70, 27);
			this.btnPortfolio.TabIndex = 5;
			this.btnPortfolio.Tag = "0";
			this.btnPortfolio.Text = "Portfolio";
			this.btnPortfolio.TextAlign = ContentAlignment.TopCenter;
			this.btnPortfolio.UseVisualStyleBackColor = false;
			this.btnPortfolio.Click += new EventHandler(this.Menus_Click);
			this.btnViewOrder.AutoSize = true;
			this.btnViewOrder.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.btnViewOrder.BackColor = Color.Transparent;
			this.btnViewOrder.FlatAppearance.BorderColor = Color.Gray;
			this.btnViewOrder.FlatAppearance.BorderSize = 0;
			this.btnViewOrder.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnViewOrder.FlatAppearance.MouseOverBackColor = Color.Gray;
			this.btnViewOrder.FlatStyle = FlatStyle.Flat;
			this.btnViewOrder.Font = new Font("Segoe UI", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnViewOrder.ForeColor = Color.LightGray;
			this.btnViewOrder.Location = new Point(437, 1);
			this.btnViewOrder.Name = "btnViewOrder";
			this.btnViewOrder.Padding = new Padding(1, 0, 1, 0);
			this.btnViewOrder.Size = new Size(86, 27);
			this.btnViewOrder.TabIndex = 4;
			this.btnViewOrder.Tag = "0";
			this.btnViewOrder.Text = "View Order";
			this.btnViewOrder.TextAlign = ContentAlignment.TopCenter;
			this.btnViewOrder.UseVisualStyleBackColor = false;
			this.btnViewOrder.Click += new EventHandler(this.Menus_Click);
			this.btnSummary.AutoSize = true;
			this.btnSummary.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.btnSummary.BackColor = Color.Transparent;
			this.btnSummary.FlatAppearance.BorderColor = Color.Gray;
			this.btnSummary.FlatAppearance.BorderSize = 0;
			this.btnSummary.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnSummary.FlatAppearance.MouseOverBackColor = Color.Gray;
			this.btnSummary.FlatStyle = FlatStyle.Flat;
			this.btnSummary.Font = new Font("Segoe UI", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnSummary.ForeColor = Color.LightGray;
			this.btnSummary.Location = new Point(182, 1);
			this.btnSummary.Name = "btnSummary";
			this.btnSummary.Size = new Size(72, 27);
			this.btnSummary.TabIndex = 2;
			this.btnSummary.Tag = "0";
			this.btnSummary.Text = "Summary";
			this.btnSummary.TextAlign = ContentAlignment.TopCenter;
			this.btnSummary.UseVisualStyleBackColor = false;
			this.btnSummary.Click += new EventHandler(this.Menus_Click);
			this.btnTopBBOs.AutoSize = true;
			this.btnTopBBOs.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.btnTopBBOs.BackColor = Color.Transparent;
			this.btnTopBBOs.FlatAppearance.BorderColor = Color.Gray;
			this.btnTopBBOs.FlatAppearance.BorderSize = 0;
			this.btnTopBBOs.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnTopBBOs.FlatAppearance.MouseOverBackColor = Color.Gray;
			this.btnTopBBOs.FlatStyle = FlatStyle.Flat;
			this.btnTopBBOs.Font = new Font("Segoe UI", 10.2f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnTopBBOs.ForeColor = Color.LightGray;
			this.btnTopBBOs.Location = new Point(105, 1);
			this.btnTopBBOs.Name = "btnTopBBOs";
			this.btnTopBBOs.Padding = new Padding(1, 0, 1, 0);
			this.btnTopBBOs.Size = new Size(81, 29);
			this.btnTopBBOs.TabIndex = 1;
			this.btnTopBBOs.Tag = "0";
			this.btnTopBBOs.Text = "Top BBOs";
			this.btnTopBBOs.TextAlign = ContentAlignment.TopCenter;
			this.btnTopBBOs.UseVisualStyleBackColor = false;
			this.btnTopBBOs.Click += new EventHandler(this.Menus_Click);
			this.btnMarketWatch.AutoSize = true;
			this.btnMarketWatch.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.btnMarketWatch.BackColor = Color.Transparent;
			this.btnMarketWatch.FlatAppearance.BorderColor = Color.Gray;
			this.btnMarketWatch.FlatAppearance.BorderSize = 0;
			this.btnMarketWatch.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnMarketWatch.FlatAppearance.MouseOverBackColor = Color.Gray;
			this.btnMarketWatch.FlatStyle = FlatStyle.Flat;
			this.btnMarketWatch.Font = new Font("Segoe UI", 10.2f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btnMarketWatch.ForeColor = Color.GreenYellow;
			this.btnMarketWatch.Location = new Point(3, 1);
			this.btnMarketWatch.Name = "btnMarketWatch";
			this.btnMarketWatch.Padding = new Padding(1, 0, 1, 0);
			this.btnMarketWatch.Size = new Size(108, 29);
			this.btnMarketWatch.TabIndex = 0;
			this.btnMarketWatch.Tag = "1";
			this.btnMarketWatch.Text = "Market Watch";
			this.btnMarketWatch.TextAlign = ContentAlignment.TopCenter;
			this.btnMarketWatch.UseVisualStyleBackColor = false;
			this.btnMarketWatch.Click += new EventHandler(this.Menus_Click);
			this.timerMonitorFeed.Interval = 6000;
			this.timerMonitorFeed.Tick += new EventHandler(this.timerMonitorFeed_Tick);
			this.timer1.Interval = 1000;
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = SystemColors.ControlDarkDark;
			base.ClientSize = new Size(1158, 564);
			base.Controls.Add(this.panelMenu);
			base.Controls.Add(this.panelFontAdjust);
			base.Controls.Add(this.panelDockR);
			base.Controls.Add(this.panelControlDockR);
			base.Controls.Add(this.panelSep);
			base.Controls.Add(this.panBottom);
			base.Controls.Add(this.panelTop);
			this.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "frmMain";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Trading Workstation";
			base.Load += new EventHandler(this.frmMain_Load);
			base.SizeChanged += new EventHandler(this.frmMain_SizeChanged);
			base.KeyUp += new KeyEventHandler(this.frmMain_KeyUp);
			base.FormClosing += new FormClosingEventHandler(this.frmMain_FormClosing);
			base.KeyDown += new KeyEventHandler(this.frmMain_KeyDown);
			this.panBottom.ResumeLayout(false);
			this.panelTop.ResumeLayout(false);
			((ISupportInitialize)this.pici2Logo).EndInit();
			this.panelFontAdjust.ResumeLayout(false);
			this.panelFontAdjust.PerformLayout();
			this.panelControlDockR.ResumeLayout(false);
			this.panelDockR.ResumeLayout(false);
			this.panelMenu.ResumeLayout(false);
			this.panelMenu.PerformLayout();
			base.ResumeLayout(false);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmMain()
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			this.InitializeComponent();
			this.MinimumSize = new Size(800, 600);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void InitializeFeedsWebProxy()
		{
			if (this.feedsWebProxy == null)
			{
				this.feedsWebProxy = new FeedsWebProxy();
				this.feedsWebProxy.UrlForSyncHandler = ApplicationInfo.UrlSyncHandler;
				this.feedsWebProxy.OnDataIN += new FeedsWebProxy.OnDataInHandler(this.feedsWebProxy_OnDataIN);
				this.feedsWebProxy.OnError += new FeedsWebProxy.OnErrorHandler(this.feedsWebProxy_OnError);
				this.feedsWebProxy.OnGettingHttp += new EventHandler(this.feedsWebProxy_OnGettingHttp);
				this.feedsWebProxy.OnGettedHttp += new EventHandler(this.feedsWebProxy_OnGettedHttp);
				this.feedsWebProxy.OnStoped += new EventHandler(this.feedsWebProxy_OnStoped);
				this.feedsWebProxy.OnStarted += new EventHandler(this.feedsWebProxy_OnStarted);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void feedsWebProxy_OnStarted(object sender, EventArgs e)
		{
			this._connectionPullCounter = 0;
			this.ShowConnectionStatus();
			this.CloseSpashForm();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void feedsWebProxy_OnStoped(object sender, EventArgs e)
		{
			this.ShowConnectionStatus();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void feedsWebProxy_OnGettedHttp(object sender, EventArgs e)
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void feedsWebProxy_OnGettingHttp(object sender, EventArgs e)
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void feedsWebProxy_OnError(TransferEventArgs e)
		{
			string text = e.Exception.Message.ToUpper();
			if (text != null)
			{
				if (text == "THE OPERATION HAS TIMED OUT" || text == "THE UNDERLYING CONNECTION WAS CLOSED: AN UNEXPECTED ERROR OCCURRED ON A RECEIVE." || text == "UNABLE TO CONNECT TO THE REMOTE SERVER")
				{
					this.ShowConnectionStatus();
					this.ShowSpashForm("PULL Connecting...");
					return;
				}
			}
			if (e.Exception != null)
			{
				this.ShowError("feedsWebProxy_OnError", e.Exception);
			}
			else
			{
				if (e.Code == -1)
				{
					this.ShowError("feedsWebProxy_OnError", e.Exception);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void feedsWebProxy_OnDataIN(string message)
		{
			if (!string.IsNullOrEmpty(message))
			{
				this.OnDataIn(message);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmMain_Load(object sender, EventArgs e)
		{
			try
			{
				if (base.CreateGraphics().DpiX == 120f)
				{
					ApplicationInfo.IsScreen125 = true;
				}
				else
				{
					ApplicationInfo.IsScreen125 = false;
				}
				if (Settings.Default.Default_WindowMaximizeState)
				{
					base.WindowState = FormWindowState.Maximized;
				}
				else
				{
					base.Bounds = Settings.Default.Default_Bound;
				}
				this._BTop = Settings.Default.Default_MainScreenHeight;
				if (Settings.Default.Default_FirstOpen)
				{
					int num;
					if (Screen.PrimaryScreen.Bounds.Size == new Size(1024, 768))
					{
						num = 10;
					}
					else
					{
						if (Screen.PrimaryScreen.Bounds.Size == new Size(800, 600))
						{
							num = 7;
						}
						else
						{
							if (Screen.PrimaryScreen.Bounds.Size == new Size(1280, 720))
							{
								num = 9;
							}
							else
							{
								if (Screen.PrimaryScreen.Bounds.Size == new Size(1280, 768))
								{
									num = 10;
								}
								else
								{
									if (Screen.PrimaryScreen.Bounds.Size == new Size(1280, 800))
									{
										num = 11;
									}
									else
									{
										if (Screen.PrimaryScreen.Bounds.Size.Height <= 600)
										{
											num = 7;
										}
										else
										{
											if (Screen.PrimaryScreen.Bounds.Size.Height <= 720)
											{
												num = 9;
											}
											else
											{
												if (Screen.PrimaryScreen.Bounds.Size.Height <= 768)
												{
													num = 10;
												}
												else
												{
													if (Screen.PrimaryScreen.Bounds.Size.Height <= 800)
													{
														num = 11;
													}
													else
													{
														num = 12;
													}
												}
											}
										}
									}
								}
							}
						}
					}
					Settings.Default.Default_Font = new Font(this.Font.Name, (float)num, FontStyle.Regular);
					Settings.Default.Default_FirstOpen = false;
				}
				this.panelTop.Font = new Font(Settings.Default.Default_Font.Name, 8.25f, Settings.Default.Default_Font.Style);
				this.MarketStateBox.Left = base.ClientRectangle.Width - this.MarketStateBox.Width - 2;
				this.timerCallLoginForm.Enabled = true;
			}
			catch (Exception ex)
			{
				this.ShowError("frmMain_Load", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void timerCallLoginForm_Tick(object sender, EventArgs e)
		{
			try
			{
				this.timerCallLoginForm.Enabled = false;
				if (this.LoadAllInformation())
				{
					ApplicationInfo.IsAreadyLogin = true;
					if (this.bgwLoadProfile == null)
					{
						this.bgwLoadProfile = new BackgroundWorker();
					}
					this.bgwLoadProfile.DoWork -= new DoWorkEventHandler(this.bgwLoadProfile_DoWork);
					this.bgwLoadProfile.DoWork += new DoWorkEventHandler(this.bgwLoadProfile_DoWork);
					this.bgwLoadProfile.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(this.bgwLoadProfile_RunWorkerCompleted);
					this.bgwLoadProfile.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwLoadProfile_RunWorkerCompleted);
					this.bgwLoadProfile.RunWorkerAsync();
				}
				else
				{
					MessageBox.Show("Can't load information, please reopen program again");
					GC.Collect();
					Application.Exit();
				}
			}
			catch (Exception ex)
			{
				EventLog.WriteEntry("i2Trade", ex.Message, EventLogEntryType.Error);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmMain_SizeChanged(object sender, EventArgs e)
		{
			if (base.WindowState != FormWindowState.Minimized)
			{
				if (TemplateManager.Instance.CurrentActiveTemplateView != null)
				{
					this.Instance_SetBottomSize();
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!this.isTerminate)
			{
				e.Cancel = !this.CloseApplication();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal void TrigKeyDown(object sender, KeyEventArgs e)
		{
			if (sender is frmBrowser)
			{
				this.frmMain_KeyDown(sender, e);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal void TrigKeyUp(object sender, KeyEventArgs e)
		{
			if (sender is frmBrowser)
			{
				this.frmMain_KeyUp(sender, e);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmMain_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode <= Keys.Delete)
			{
				if (keyCode != Keys.Tab)
				{
					if (keyCode != Keys.Space)
					{
						if (keyCode != Keys.Delete)
						{
							goto IL_BC;
						}
						this.ViewOrderBox.SetFocus();
						e.SuppressKeyPress = true;
						goto IL_BC;
					}
					else
					{
						if (ApplicationInfo.CanRecvSpace)
						{
							return;
						}
						e.SuppressKeyPress = true;
						goto IL_BC;
					}
				}
			}
			else
			{
				if (keyCode <= Keys.Divide)
				{
					if (keyCode != Keys.Multiply)
					{
						switch (keyCode)
						{
						case Keys.Subtract:
						case Keys.Divide:
							break;
						case Keys.Decimal:
							goto IL_BC;
						default:
							goto IL_BC;
						}
					}
				}
				else
				{
					if (keyCode != Keys.F4 && keyCode != Keys.F10)
					{
						goto IL_BC;
					}
				}
			}
			e.SuppressKeyPress = true;
            IL_BC:
            try
			{
				
				if (ApplicationInfo.IsAreadyLogin)
				{
					if (HotKeyManager.IsValidHotKey(e.KeyData))
					{
						switch (HotKeyManager.GetSystemHotKey(e.KeyData))
						{
						case HotKeyFor.ToggleMenuTemplate:
							e.SuppressKeyPress = true;
							break;
						case HotKeyFor.LogOut:
							this.Logout(false);
							e.SuppressKeyPress = true;
							break;
						case HotKeyFor.SwitchAccount:
							this.SendOrderBox.SwitchAccountControl();
							e.SuppressKeyPress = true;
							break;
						case HotKeyFor.SwitchOrderBox:
							break;
						default:
						{
							HotKeyManager.HotkeyProperty templateHotKey = HotKeyManager.GetTemplateHotKey(e.KeyData);
							if (templateHotKey != null && !string.IsNullOrEmpty(templateHotKey.TemplateName))
							{
								string templateName = templateHotKey.TemplateName;
								if (templateName != null)
								{
									if (templateName == "Buy Order" || templateName == "Sell Order" || templateName == "Short Sell Order" || templateName == "Cover Buy Order")
									{
										this.OpenSendNewOrderForm(templateHotKey.TemplateName);
										goto IL_200;
									}
								}
								this.OpenTemplate(templateHotKey.TemplateName);
								IL_200:
								e.SuppressKeyPress = true;
							}
							break;
						}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("frmMain_KeyUp", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmMain_KeyUp(object sender, KeyEventArgs e)
		{
			try
			{
				if (ApplicationInfo.IsAreadyLogin)
				{
					if (e.KeyCode != Keys.Space || !ApplicationInfo.CanRecvSpace)
					{
						if (TemplateManager.Instance.CurrentActiveTemplateView != null)
						{
							TemplateManager.Instance.CurrentActiveTemplateView.FormKeyUp(e);
						}
						if (e.KeyCode == Keys.NumLock)
						{
							this.SetNumLock();
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("frmMain_KeyUp", ex);
			}
		}
		[DllImport("user32.dll")]
		internal static extern short GetKeyState(int keyCode);
		[DllImport("user32.dll")]
		private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);
		[DllImport("user32.dll")]
		private static extern int SetKeyboardState(byte keyState);
		[DllImport("user32.dll")]
		private static extern int GetKeyboardState(ref byte keyState);
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetNumLock()
		{
			byte[] array = new byte[256];
			frmMain.GetKeyboardState(ref array[0]);
			if (array[144] != 1)
			{
				frmMain.keybd_event(144, 0, 0u, 0);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OpenSendNewOrderForm(string templateName)
		{
			try
			{
				if (ApplicationInfo.CheckFormCanShowByLoginMode(typeof(ucSendNewOrder)))
				{
					string formActivate = string.Empty;
					if (templateName != null)
					{
						if (!(templateName == "Buy Order"))
						{
							if (!(templateName == "Sell Order"))
							{
								if (!(templateName == "Short Sell Order"))
								{
									if (templateName == "Cover Buy Order")
									{
										formActivate = "C";
									}
								}
								else
								{
									formActivate = "H";
								}
							}
							else
							{
								formActivate = "S";
							}
						}
						else
						{
							formActivate = "B";
						}
					}
					if (this.SendOrderBox != null)
					{
						this.SendOrderBox.SetFormActivate(formActivate);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("OpenSendNewOrderForm", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void stopBC()
		{
			try
			{
				this._isRunPeekQ = false;
				if (this.ipwIPPortMain != null)
				{
					if (this.ipwIPPortMain.Connected)
					{
						this.UnregistrationFromTunnel();
						Thread.Sleep(100);
						this.ipwIPPortMain.Disconnect();
					}
					this.ipwIPPortMain.OnConnected -= new Ipports.OnConnectedHandler(this.ipwIPPortMain_OnConnected);
					this.ipwIPPortMain.OnDisconnected -= new Ipports.OnDisconnectedHandler(this.ipwIPPortMain_OnDisconnected);
					this.ipwIPPortMain.OnDataIn -= new Ipports.OnDataInHandler(this.ipwIPPortMain_OnDataIn);
					this.ipwIPPortMain.OnError -= new Ipports.OnErrorHandler(this.ipwIPPortMain_OnError);
					this.ipwIPPortMain.OnSSLServerAuthentication -= new Ipports.OnSSLServerAuthenticationHandler(this.ipwIPPortMain_OnSSLServerAuthentication);
					this.ipwIPPortMain.OnReadyToSend -= new Ipports.OnReadyToSendHandler(this.ipwIPPortMain_OnReadyToSend);
					this.ipwIPPortMain.Dispose();
					this.ipwIPPortMain = null;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("stopBC", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void startBC()
		{
			try
			{
				this.timerMonitorFeed.Enabled = false;
				this.MarketStateBox.DisplayPushPullImage();
				if (!ApplicationInfo.IsPushMode)
				{
					this.ShowSpashForm("PULL Connecting...");
					this.InitializeFeedsWebProxy();
					this.stopBC();
					this.feedsWebProxy.Start();
				}
				else
				{
					this.ShowSpashForm("PUSH(" + Convert.ToString(ApplicationInfo.TunnelCounter + 1) + ") Connecting...");
					if (this.feedsWebProxy != null && this.feedsWebProxy.IsServiceStarted)
					{
						this.feedsWebProxy.Stop();
					}
					if (this.ipwIPPortMain == null)
					{
						this.ipwIPPortMain = new Ipports();
						this.ipwIPPortMain.OnConnected += new Ipports.OnConnectedHandler(this.ipwIPPortMain_OnConnected);
						this.ipwIPPortMain.OnDisconnected += new Ipports.OnDisconnectedHandler(this.ipwIPPortMain_OnDisconnected);
						this.ipwIPPortMain.OnDataIn += new Ipports.OnDataInHandler(this.ipwIPPortMain_OnDataIn);
						this.ipwIPPortMain.OnError += new Ipports.OnErrorHandler(this.ipwIPPortMain_OnError);
						this.ipwIPPortMain.OnSSLServerAuthentication += new Ipports.OnSSLServerAuthenticationHandler(this.ipwIPPortMain_OnSSLServerAuthentication);
						this.ipwIPPortMain.OnReadyToSend += new Ipports.OnReadyToSendHandler(this.ipwIPPortMain_OnReadyToSend);
						this.ipwIPPortMain.Config("InBufferSize=102400");
						if (ApplicationInfo.TunnelHosts[ApplicationInfo.TunnelCounter].EnableSSL)
						{
							this.ipwIPPortMain.SSLStartMode = IpportsSSLStartModes.sslImplicit;
						}
						else
						{
							this.ipwIPPortMain.SSLStartMode = IpportsSSLStartModes.sslNone;
						}
					}
					this.ipwIPPortMain.Connected = false;
					this._isRunPeekQ = false;
					int num = 0;
					while (this._isPeekQWorking)
					{
						num++;
						Thread.Sleep(100);
						if (num > 50)
						{
							break;
						}
					}
					this._isRunPeekQ = true;
					if (this.ipwIPPortMain.RemoteHost != ApplicationInfo.TunnelHosts[ApplicationInfo.TunnelCounter].HostIP)
					{
						this.ipwIPPortMain.RemoteHost = ApplicationInfo.TunnelHosts[ApplicationInfo.TunnelCounter].HostIP;
					}
					if (this.ipwIPPortMain.RemotePort != ApplicationInfo.TunnelHosts[ApplicationInfo.TunnelCounter].Port)
					{
						this.ipwIPPortMain.RemotePort = ApplicationInfo.TunnelHosts[ApplicationInfo.TunnelCounter].Port;
					}
					this._lastEcho = DateTime.Now;
					this.ipwIPPortMain.Connected = true;
				}
				this.ShowConnectionStatus();
			}
			catch (Exception ex)
			{
				this.ShowError("StartBC", ex);
			}
			this.timerMonitorFeed.Enabled = true;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowConnectionStatus()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(this.ShowConnectionStatus));
			}
			else
			{
				try
				{
					if (ApplicationInfo.IsPushMode)
					{
						if (this.ipwIPPortMain != null)
						{
							ApplicationInfo.IsFeedsStarted = this.ipwIPPortMain.Connected;
						}
						else
						{
							ApplicationInfo.IsFeedsStarted = false;
						}
					}
					else
					{
						if (this.feedsWebProxy != null)
						{
							ApplicationInfo.IsFeedsStarted = this.feedsWebProxy.CanConnectServer;
						}
						else
						{
							ApplicationInfo.IsFeedsStarted = false;
						}
					}
					this.MarketStateBox.ShowFeedsState(ApplicationInfo.IsFeedsStarted);
				}
				catch (Exception ex)
				{
					this.ShowError("ShowConnectionStatus", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private Form ShowChildForm(Type formType)
		{
			return this.ShowChildForm(formType, null);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private Form ShowChildForm(Type formType, Dictionary<string, object> prop)
		{
			Form form = null;
			try
			{
				form = this.ShowChildForm(formType, TemplateManager.Instance.CurrentActiveTemplateView.Name, prop);
				if (form != null)
				{
					form.Location = new Point(1, 1);
					form.Visible = ApplicationInfo.CheckFormCanShowByLoginMode(formType);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("ShowChildForm", ex);
			}
			return form;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private Form ShowChildForm(Type formType, string templateName, Dictionary<string, object> prop)
		{
			Form form = null;
			Form result;
			try
			{
				TemplateView template = TemplateManager.Instance.GetTemplate(templateName);
				if (template == null)
				{
					this.DisplayMessageBox("Template " + templateName + " dosn't exits.");
					result = null;
					return result;
				}
				if (template.CurrentState != TemplateView.ContentState.OpeningOrClosing)
				{
					this.DisplayMessageBox("Template is locked.");
					result = null;
					return result;
				}
				if (template.CurrentState != TemplateView.ContentState.OpeningOrClosing)
				{
					this.DisplayMessageBox("Template is locked.");
					result = null;
					return result;
				}
				form = (Form)Activator.CreateInstance(formType, new object[]
				{
					prop
				});
				if (prop != null && prop.ContainsKey("Name"))
				{
					form.Name = prop["Name"].ToString();
				}
				else
				{
					Form expr_F0 = form;
					expr_F0.Name += form.GetHashCode().ToString();
				}
				form.Visible = false;
				form.TopLevel = false;
				form.Parent = this;
				this.SetEventToNewForm(form);
				TemplateManager.Instance.AddFormToTemplate(templateName, form, 0);
			}
			catch (Exception ex)
			{
				this.ShowError("ShowChildForm", ex);
			}
			result = form;
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetEventToNewForm(Form form)
		{
			try
			{
				frmMain._OnMessageReceived = (frmMain.OnMessageRecievedEventHendler)Delegate.Remove(frmMain._OnMessageReceived, new frmMain.OnMessageRecievedEventHendler(((IRealtimeMessage)form).ReceiveMessage));
				frmMain._OnMessageReceived = (frmMain.OnMessageRecievedEventHendler)Delegate.Combine(frmMain._OnMessageReceived, new frmMain.OnMessageRecievedEventHendler(((IRealtimeMessage)form).ReceiveMessage));
				frmMain._OnMessageTfexReceived = (frmMain.OnMessageTfexRecievedEventHendler)Delegate.Remove(frmMain._OnMessageTfexReceived, new frmMain.OnMessageTfexRecievedEventHendler(((IRealtimeMessage)form).ReceiveTfexMessage));
				frmMain._OnMessageTfexReceived = (frmMain.OnMessageTfexRecievedEventHendler)Delegate.Combine(frmMain._OnMessageTfexReceived, new frmMain.OnMessageTfexRecievedEventHendler(((IRealtimeMessage)form).ReceiveTfexMessage));
			}
			catch (Exception ex)
			{
				this.ShowError("SetEventToNewForm", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnDataIn(string message)
		{
			if (!string.IsNullOrEmpty(message))
			{
				try
				{
					this._lastEcho = DateTime.Now;
					if (!(message == "ER"))
					{
						if (message.Substring(0, 1) == "E")
						{
							IBroadcastMessage broadcastMessage = this._bcMessageFactory.CreateSETMessage(message.Substring(1));
							if (broadcastMessage != null)
							{
								StockList.StockInformation stockInformation = null;
								string messageType = broadcastMessage.MessageType;
								switch (messageType)
								{
								case "L+":
								{
									LSAccumulate lSAccumulate = (LSAccumulate)broadcastMessage;
									stockInformation = ApplicationInfo.StockInfo[lSAccumulate.SecurityNumber];
									if (stockInformation.Number <= 0)
									{
										return;
									}
									stockInformation.LastSalePrice = lSAccumulate.LastPrice;
									if (lSAccumulate.LastPrice > stockInformation.HighPrice)
									{
										stockInformation.HighPrice = lSAccumulate.LastPrice;
									}
									if (stockInformation.LowPrice == 0m || lSAccumulate.LastPrice < stockInformation.LowPrice)
									{
										stockInformation.LowPrice = lSAccumulate.LastPrice;
									}
									break;
								}
								case "TP":
								{
									TPMessage tPMessage = (TPMessage)broadcastMessage;
									stockInformation = ApplicationInfo.StockInfo[tPMessage.SecurityNumber];
									if (stockInformation.Number <= 0)
									{
										return;
									}
									if (tPMessage.Side == "B")
									{
										stockInformation.BidPrice1 = tPMessage.Price1.ToString();
									}
									else
									{
										if (tPMessage.Side == "S")
										{
											stockInformation.OfferPrice1 = tPMessage.Price1.ToString();
										}
									}
									break;
								}
								case "PD":
									stockInformation = ApplicationInfo.StockInfo[((PDMessage)broadcastMessage).SecurityNumber];
									break;
								case "SC":
								{
									SCMessage sCMessage = (SCMessage)broadcastMessage;
									ApplicationInfo.MarketState = sCMessage.MarketState;
									ApplicationInfo.MarketSession = sCMessage.MarketSession;
									this.SETBox.Invalidate();
									break;
								}
								case "MT":
								{
									MarketInfo marketInfo = (MarketInfo)broadcastMessage;
									ApplicationInfo.MarketState = marketInfo.MarketState;
									ApplicationInfo.MarketSession = marketInfo.MarketSession;
									ApplicationInfo.MarketTime = marketInfo.TimeStamp;
									break;
								}
								case "PO":
									stockInformation = ApplicationInfo.StockInfo[((POMessage)broadcastMessage).SecurityNumber];
									break;
								case "SS":
								{
									SSMessage sSMessage = (SSMessage)broadcastMessage;
									stockInformation = ApplicationInfo.StockInfo[sSMessage.SecurityNumber];
									if (stockInformation.Number > -1)
									{
										stockInformation.PriorPrice = sSMessage.PriorPrice;
										stockInformation.BoardLot = sSMessage.BoardLot;
										stockInformation.Ceiling = sSMessage.Ceiling;
										stockInformation.Floor = sSMessage.Floor;
										stockInformation.MarketId = sSMessage.MarketId;
										stockInformation.SecurityType = sSMessage.SecurityType;
										stockInformation.SectorNumber = sSMessage.SectorNumber;
										stockInformation.DisplayFlag = sSMessage.DisplayFlag;
									}
									break;
								}
								case "IS":
								{
									ISMessage iSMessage = (ISMessage)broadcastMessage;
									IndexStat.IndexItem indexItem = ApplicationInfo.IndexStatInfo[iSMessage.Symbol];
									indexItem.IndexHigh = iSMessage.IndexHigh;
									indexItem.IndexLow = iSMessage.IndexLow;
									indexItem.LastIndex = iSMessage.IndexValue;
									indexItem.AccValue = iSMessage.TotalValuesTraded;
									if (iSMessage.Symbol == ".SET")
									{
										this.SETBox.Invalidate();
									}
									else
									{
										if (iSMessage.Symbol == "." + this.SET2Box.DisplaySET)
										{
											this.SET2Box.Invalidate();
										}
									}
									break;
								}
								case "IE":
								{
									IEMessage iEMessage = (IEMessage)broadcastMessage;
									IndexStat.IndexItem indexItem = ApplicationInfo.IndexStatInfo[iEMessage.Symbol];
									if (indexItem != null)
									{
										indexItem.IndexHigh = iEMessage.IndexHigh;
										indexItem.IndexLow = iEMessage.IndexLow;
										indexItem.LastIndex = iEMessage.IndexValue;
										indexItem.AccValue = iEMessage.AccValue;
									}
									break;
								}
								case "OL":
									stockInformation = ApplicationInfo.StockInfo[((OLMessage)broadcastMessage).SecurityNumber];
									break;
								case "LO":
									stockInformation = ApplicationInfo.StockInfo[((LOMessage)broadcastMessage).SecurityNumber];
									break;
								case "CO":
									stockInformation = ApplicationInfo.StockInfo[((COMessage)broadcastMessage).SecurityNumber];
									break;
								}
								if (frmMain._OnMessageReceived != null)
								{
									frmMain._OnMessageReceived(broadcastMessage, stockInformation);
								}
							}
						}
						else
						{
							if (message.Substring(0, 1) == "T")
							{
								IBroadcastMessage broadcastMessage = this._bcMessageFactory.CreateTfexMessage(message.Substring(1));
								if (broadcastMessage != null)
								{
									SeriesList.SeriesInformation seriesInformation = null;
									string messageType = broadcastMessage.MessageType;
									switch (messageType)
									{
									case "TP":
									{
										TPMessageTFEX tPMessageTFEX = (TPMessageTFEX)broadcastMessage;
										seriesInformation = ApplicationInfo.SeriesInfo[tPMessageTFEX.OrderBookId];
										if (tPMessageTFEX.Side == "B" && tPMessageTFEX.Vol1 != -1)
										{
											seriesInformation.BidPrice1 = tPMessageTFEX.Price1;
										}
										else
										{
											if (tPMessageTFEX.Side == "A" && tPMessageTFEX.Vol1 != -1)
											{
												seriesInformation.OfferPrice1 = tPMessageTFEX.Price1;
											}
										}
										break;
									}
									case "LS":
									{
										LSMessageTFEX lSMessageTFEX = (LSMessageTFEX)broadcastMessage;
										seriesInformation = ApplicationInfo.SeriesInfo[lSMessageTFEX.Sec];
										seriesInformation.LastSalePrice = lSMessageTFEX.Price;
										break;
									}
									case "PO":
									{
										POMessageTFEX pOMessageTFEX = (POMessageTFEX)broadcastMessage;
										seriesInformation = ApplicationInfo.SeriesInfo[pOMessageTFEX.Sec];
										break;
									}
									case "ST":
									{
										STMessageTFEX sTMessageTFEX = (STMessageTFEX)broadcastMessage;
										if (sTMessageTFEX.MarketCode == 1)
										{
											ApplicationInfo.IndexInfoTfex.TXIState = sTMessageTFEX.MarketState;
											ApplicationInfo.IndexInfoTfex.TXISession = sTMessageTFEX.MarketSession;
										}
										else
										{
											if (sTMessageTFEX.MarketCode == 4)
											{
												ApplicationInfo.IndexInfoTfex.TXMState = sTMessageTFEX.MarketState;
												ApplicationInfo.IndexInfoTfex.TXMSession = sTMessageTFEX.MarketSession;
											}
											else
											{
												if (sTMessageTFEX.MarketCode == 3)
												{
													ApplicationInfo.IndexInfoTfex.TXRState = sTMessageTFEX.MarketState;
													ApplicationInfo.IndexInfoTfex.TXRSession = sTMessageTFEX.MarketSession;
												}
												else
												{
													if (sTMessageTFEX.MarketCode == 2)
													{
														ApplicationInfo.IndexInfoTfex.TXSState = sTMessageTFEX.MarketState;
														ApplicationInfo.IndexInfoTfex.TXSSession = sTMessageTFEX.MarketSession;
													}
													else
													{
														if (sTMessageTFEX.MarketCode == 5)
														{
															ApplicationInfo.IndexInfoTfex.TXEState = sTMessageTFEX.MarketState;
															ApplicationInfo.IndexInfoTfex.TXESession = sTMessageTFEX.MarketSession;
														}
														else
														{
															if (sTMessageTFEX.MarketCode == 6)
															{
																ApplicationInfo.IndexInfoTfex.TXCState = sTMessageTFEX.MarketState;
																ApplicationInfo.IndexInfoTfex.TXCSession = sTMessageTFEX.MarketSession;
															}
														}
													}
												}
											}
										}
										break;
									}
									case "SU":
									{
										SUMessageTFEX sUMessageTFEX = (SUMessageTFEX)broadcastMessage;
										seriesInformation = ApplicationInfo.SeriesInfo[sUMessageTFEX.Sec];
										seriesInformation.ExpireDate = sUMessageTFEX.Expdate.ToString();
										seriesInformation.OpenInt = sUMessageTFEX.OpenBalance;
										break;
									}
									case "SD":
									{
										SDMessageTFEX sDMessageTFEX = (SDMessageTFEX)broadcastMessage;
										if (!ApplicationInfo.SeriesInfo.ItemsKeySymbol.ContainsKey(sDMessageTFEX.Sec.ToString()))
										{
											if (sDMessageTFEX.Group != 5)
											{
												seriesInformation = new SeriesList.SeriesInformation();
												seriesInformation.Symbol = sDMessageTFEX.Sec;
												seriesInformation.MarketCode = sDMessageTFEX.Market;
												seriesInformation.Group = sDMessageTFEX.Group;
												seriesInformation.UnderOrderBookId = sDMessageTFEX.UnderOrderBookId;
												seriesInformation.ContractSize = sDMessageTFEX.Price_quot_factor;
												ApplicationInfo.SeriesInfo.AddItem(seriesInformation);
												this.UpdateAutoComplete(seriesInformation.Symbol);
											}
										}
										else
										{
											seriesInformation = ApplicationInfo.SeriesInfo[sDMessageTFEX.Sec];
											seriesInformation.MarketCode = sDMessageTFEX.Market;
											seriesInformation.Group = sDMessageTFEX.Group;
											seriesInformation.UnderOrderBookId = sDMessageTFEX.UnderOrderBookId;
											seriesInformation.ContractSize = sDMessageTFEX.Price_quot_factor;
										}
										break;
									}
									case "SS":
									{
										SSMessageTFEX sSMessageTFEX = (SSMessageTFEX)broadcastMessage;
										seriesInformation = ApplicationInfo.SeriesInfo[sSMessageTFEX.Sec];
										break;
									}
									case "BU10":
									{
										BU10MessageTFEX bU10MessageTFEX = (BU10MessageTFEX)broadcastMessage;
										foreach (KeyValuePair<int, SeriesList.SeriesInformation> current in ApplicationInfo.SeriesInfo.Items)
										{
											if (current.Value.Group == bU10MessageTFEX.Group && current.Value.UnderOrderBookId == bU10MessageTFEX.Commodity)
											{
												current.Value.TickSize = bU10MessageTFEX.StepSize;
												break;
											}
										}
										break;
									}
									case "IU":
									{
										IUMessageTFEX iUMessageTFEX = (IUMessageTFEX)broadcastMessage;
										if (iUMessageTFEX.IndxName == "SET50")
										{
											IndexStat.IndexItem indexItem2 = ApplicationInfo.IndexStatInfo[".SET50"];
											if (indexItem2 != null)
											{
												if (iUMessageTFEX.LastIndx > 0m)
												{
													indexItem2.LastIndex = iUMessageTFEX.LastIndx;
												}
											}
										}
										break;
									}
									case "CA8":
									{
										CA8MessageTFEX cA8MessageTFEX = (CA8MessageTFEX)broadcastMessage;
										seriesInformation = ApplicationInfo.SeriesInfo[cA8MessageTFEX.Sec];
										if (ApplicationInfo.IndexInfoTfex.TXISession == 2)
										{
											seriesInformation.FixPrice = cA8MessageTFEX.FixingPrice;
											if (seriesInformation.MarketCode == 4)
											{
												seriesInformation.PrevFixPrice = cA8MessageTFEX.FixingPrice;
											}
										}
										else
										{
											seriesInformation.PrevFixPrice = cA8MessageTFEX.FixingPrice;
										}
										seriesInformation.NumOfDec = cA8MessageTFEX.DecPrice;
										break;
									}
									case "TCF":
									{
										TCFMessageTFEX tCFMessageTFEX = (TCFMessageTFEX)broadcastMessage;
										seriesInformation = ApplicationInfo.SeriesInfo[tCFMessageTFEX.SeriesName.Trim()];
										if (!string.IsNullOrEmpty(seriesInformation.Symbol))
										{
											seriesInformation.Ceiling = tCFMessageTFEX.Ceiling;
											seriesInformation.Floor = tCFMessageTFEX.Floor;
											seriesInformation.PrevFixPrice = tCFMessageTFEX.PrevFixPrice;
										}
										break;
									}
									case "MI":
									{
										MIMessageTFEX mIMessageTFEX = (MIMessageTFEX)broadcastMessage;
										ApplicationInfo.IndexInfoTfex.FutureVol = mIMessageTFEX.FuturesVol;
										ApplicationInfo.IndexInfoTfex.FutureOI = mIMessageTFEX.FuturesOI;
										ApplicationInfo.IndexInfoTfex.OptionsVol = mIMessageTFEX.OptionsVol;
										ApplicationInfo.IndexInfoTfex.OptionsOI = mIMessageTFEX.OptionsOI;
										ApplicationInfo.IndexInfoTfex.TfexTotalVol = mIMessageTFEX.TotalVol;
										ApplicationInfo.IndexInfoTfex.TfexTotalDeal = mIMessageTFEX.TotalDeal;
										ApplicationInfo.IndexInfoTfex.TfexTotalOI = mIMessageTFEX.TotalOI;
										break;
									}
									}
									if (frmMain._OnMessageTfexReceived != null)
									{
										frmMain._OnMessageTfexReceived(broadcastMessage, seriesInformation);
									}
								}
							}
							else
							{
								if (message.Substring(0, 2) == "KC")
								{
									this._isSessionTimout = true;
									this.Logout(true);
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("OnDataIn", new Exception(ex.Message + ("[" + message) + "]"));
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OnAlert(AlertItem e)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new frmMain.OnAlertCallback(this.OnAlert), new object[]
				{
					e
				});
			}
			else
			{
				try
				{
					this.CreateAlertDetailForm();
					this._alertDetailForm.AddAlertItem(e);
					if (ApplicationInfo.AlertAutoPopup)
					{
						this._alertDetailForm.Show();
						this._alertDetailForm.BringToFront();
					}
					else
					{
						this.MarketStateBox.IsAllowBlinkAlert = true;
						this.MarketStateBox.AlterMessageCount++;
					}
					if (ApplicationInfo.AlertSound)
					{
						ApplicationInfo.Beep(500, 200);
					}
				}
				catch (Exception ex)
				{
					this.ShowError("OnAlert", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void CreateAlertDetailForm()
		{
			try
			{
				if (this._alertDetailForm == null)
				{
					this._alertDetailForm = new frmAlertDetail();
					this._alertDetailForm.TopMost = true;
					this._alertDetailForm.StartPosition = FormStartPosition.CenterScreen;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OpenAlertForm()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(this.OpenAlertForm));
			}
			else
			{
				try
				{
					this.CreateAlertDetailForm();
					this._alertDetailForm.Show();
					this._alertDetailForm.BringToFront();
				}
				catch (Exception ex)
				{
					this.ShowError("OpenAlertForm", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ClearAllTemplate()
		{
			try
			{
				List<string> list = new List<string>();
				foreach (KeyValuePair<string, TemplateView> current in TemplateManager.Instance.TemplateViews)
				{
					list.Add(current.Value.Name);
				}
				for (int i = 0; i < list.Count; i++)
				{
					TemplateView template = TemplateManager.Instance.GetTemplate(list[i]);
					if (template != null)
					{
						TemplateManager.Instance.CurrentActiveTemplateView = template;
						this.SaveTemplate();
						TemplateManager.Instance.DeleteTemplate(TemplateManager.Instance.CurrentActiveTemplateView.Name);
						Thread.Sleep(100);
					}
				}
				list.Clear();
				list = null;
			}
			catch (Exception ex)
			{
				this.ShowError("ClearAllTemplate", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void LoadAppInfo()
		{
			try
			{
				using (ApplicationInfoDS applicationInfoDS = new ApplicationInfoDS())
				{
					string templatePathByUser = ApplicationInfo.GetTemplatePathByUser();
					string text = templatePathByUser + "\\ApplicationInfo.xml";
					if (File.Exists(text))
					{
						applicationInfoDS.ReadXml(text);
					}
					if (applicationInfoDS.Info.Rows.Count > 0)
					{
						if (applicationInfoDS.Info.Columns.Contains("TPBlinkColor"))
						{
							if (applicationInfoDS.Info[0].TPBlinkColor == "Y")
							{
								ApplicationInfo.IsSupportTPBlinkColor = true;
							}
							else
							{
								ApplicationInfo.IsSupportTPBlinkColor = false;
							}
						}
						if (applicationInfoDS.Info.Columns.Contains("ValidatePolicy"))
						{
							ApplicationInfo.IsRiskActive = applicationInfoDS.Info[0].ValidatePolicy;
						}
						if (applicationInfoDS.Info.Columns.Contains("AlertEnable"))
						{
							ApplicationInfo.AlertOpen = applicationInfoDS.Info[0].AlertEnable;
						}
						if (applicationInfoDS.Info.Columns.Contains("AlertAutoPopup"))
						{
							ApplicationInfo.AlertAutoPopup = applicationInfoDS.Info[0].AlertAutoPopup;
						}
						if (applicationInfoDS.Info.Columns.Contains("AlertSound"))
						{
							ApplicationInfo.AlertSound = applicationInfoDS.Info[0].AlertSound;
						}
					}
					else
					{
						ApplicationInfo.IsSupportTPBlinkColor = true;
						ApplicationInfo.IsRiskActive = false;
						ApplicationInfo.AlertOpen = false;
						ApplicationInfo.AlertAutoPopup = true;
						ApplicationInfo.AlertSound = false;
					}
					applicationInfoDS.Clear();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("LoadAppInfo", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SaveAppInfo()
		{
			try
			{
				using (ApplicationInfoDS applicationInfoDS = new ApplicationInfoDS())
				{
					string text = ApplicationInfo.GetTemplatePathByUser() + "\\ApplicationInfo.xml";
					if (File.Exists(text))
					{
						applicationInfoDS.ReadXml(text);
						applicationInfoDS.Info.Clear();
					}
					ApplicationInfoDS.InfoRow infoRow = applicationInfoDS.Info.NewInfoRow();
					if (ApplicationInfo.IsSupportTPBlinkColor)
					{
						infoRow.TPBlinkColor = "Y";
					}
					else
					{
						infoRow.TPBlinkColor = "N";
					}
					infoRow.ValidatePolicy = ApplicationInfo.IsRiskActive;
					infoRow.AlertEnable = ApplicationInfo.AlertOpen;
					infoRow.AlertSound = ApplicationInfo.AlertSound;
					infoRow.AlertAutoPopup = ApplicationInfo.AlertAutoPopup;
					applicationInfoDS.Info.AddInfoRow(infoRow);
					if (File.Exists(text))
					{
						applicationInfoDS.WriteXml(text);
					}
					else
					{
						if (!Directory.Exists(ApplicationInfo.GetTemplatePathByUser()))
						{
							Directory.CreateDirectory(ApplicationInfo.GetTemplatePathByUser());
						}
						applicationInfoDS.WriteXml(text);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SaveUserProfile", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool CloseApplication()
		{
			bool result;
			try
			{
				if (this._alertDetailForm != null)
				{
					this._alertDetailForm.Hide();
				}
				if (!ApplicationInfo.IsAreadyLogin)
				{
					result = true;
					return result;
				}
				if (MessageBox.Show("Would you like to Exit?", "Exit", MessageBoxButtons.YesNo) != DialogResult.Yes)
				{
					result = false;
					return result;
				}
				this.Logout(true);
			}
			catch (Exception ex)
			{
				this.ShowError("CloseApplication", ex);
			}
			result = false;
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowSpashForm(string message)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new frmMain.showSpashFormCallBack(this.ShowSpashForm), new object[]
				{
					message
				});
			}
			else
			{
				if (this.splashForm == null)
				{
					this.splashForm = new frmSplash();
					this.splashForm.TopLevel = false;
					this.splashForm.Parent = this;
					this.splashForm.TopMost = true;
				}
				if (base.WindowState != FormWindowState.Minimized)
				{
					this.splashForm.Left = (base.Width - this.splashForm.Width) / 2;
					this.splashForm.Top = (base.Height - this.splashForm.Height) / 2;
				}
				this.splashForm.SetCurrentTask(message);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void CloseSpashForm()
		{
			if (this.splashForm != null)
			{
				if (this.splashForm.InvokeRequired)
				{
					this.splashForm.Invoke(new MethodInvoker(this.CloseSpashForm));
				}
				else
				{
					this.splashForm.Hide();
					this.splashForm.DisposeMe();
					this.splashForm = null;
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwLoadProfile_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				if (AlertManager.Instance.ReadData())
				{
					AlertManager.Instance.OnAlert -= new AlertManager.OnAlertHandler(this.OnAlert);
					AlertManager.Instance.OnAlert += new AlertManager.OnAlertHandler(this.OnAlert);
				}
				AlertManager.Instance.IsMonitoring = true;
				frmMain._OnMessageReceived = (frmMain.OnMessageRecievedEventHendler)Delegate.Remove(frmMain._OnMessageReceived, new frmMain.OnMessageRecievedEventHendler(AlertManager.Instance.ReceiveMessage));
				frmMain._OnMessageReceived = (frmMain.OnMessageRecievedEventHendler)Delegate.Combine(frmMain._OnMessageReceived, new frmMain.OnMessageRecievedEventHendler(AlertManager.Instance.ReceiveMessage));
				frmMain._OnMessageTfexReceived = (frmMain.OnMessageTfexRecievedEventHendler)Delegate.Remove(frmMain._OnMessageTfexReceived, new frmMain.OnMessageTfexRecievedEventHendler(AlertManager.Instance.ReceiveTfexMessage));
				frmMain._OnMessageTfexReceived = (frmMain.OnMessageTfexRecievedEventHendler)Delegate.Combine(frmMain._OnMessageTfexReceived, new frmMain.OnMessageTfexRecievedEventHendler(AlertManager.Instance.ReceiveTfexMessage));

                if (ApplicationInfo.StopOrderSupported)
                {
                    frmMain._OnMessageReceived = (frmMain.OnMessageRecievedEventHendler)Delegate.Remove(frmMain._OnMessageReceived, new frmMain.OnMessageRecievedEventHendler(AutoTradeManager.Instance.ReceiveMessage));
                    frmMain._OnMessageReceived = (frmMain.OnMessageRecievedEventHendler)Delegate.Combine(frmMain._OnMessageReceived, new frmMain.OnMessageRecievedEventHendler(AutoTradeManager.Instance.ReceiveMessage));
                    frmMain._OnMessageTfexReceived = (frmMain.OnMessageTfexRecievedEventHendler)Delegate.Remove(frmMain._OnMessageTfexReceived, new frmMain.OnMessageTfexRecievedEventHendler(AutoTradeManager.Instance.ReceiveTfexMessage));
                    frmMain._OnMessageTfexReceived = (frmMain.OnMessageTfexRecievedEventHendler)Delegate.Combine(frmMain._OnMessageTfexReceived, new frmMain.OnMessageTfexRecievedEventHendler(AutoTradeManager.Instance.ReceiveTfexMessage));
                }
            }
			catch (Exception ex)
			{
				this.ShowError("bgwLoadProfile_DoWork", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwLoadProfile_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				this.LoadAppInfo();
				try
				{
					HotKeyManager.Initial();
				}
				catch (Exception ex)
				{
					this.ShowError("HotkeyInit()", ex);
				}
				string text = "<" + ApplicationInfo.UserLoginID + "> ";
				switch (ApplicationInfo.BrokerId)
				{
				case 1:
					text += "CNS";
					break;
				case 2:
					text += "KTZ";
					break;
				case 3:
					text += "RHBOSK";
					break;
				case 4:
					text += "MAYBANK KIM ENG";
					break;
				case 7:
					text += "AIRA";
					break;
				case 8:
					text += "ASIA PLUS";
					break;
				case 9:
					text += "KTBST";
					break;
				case 10:
					text += "AWS";
					break;
				case 11:
					text += "CIMBS";
					break;
				case 12:
					text += "KKTRADE";
					break;
				case 13:
					text += "LHSEC";
					break;
				case 14:
					text += "AECS";
					break;
				}
				text += " i2Trade Plus";
				text = text + " " + Application.ProductVersion;
				if (ApplicationInfo.BrokerId == 88)
				{
					text += " -- DEMO";
				}
				if (ApplicationInfo.BrokerId == 89)
				{
					text += " -- CNS DEMO";
				}
				this.Text = text;
				this.SetTopPanelActive(true);
				this.CloseSpashForm();
				TemplateManager.Instance.MainForm = this;
				frmMain._OnMessageReceived = (frmMain.OnMessageRecievedEventHendler)Delegate.Remove(frmMain._OnMessageReceived, new frmMain.OnMessageRecievedEventHendler(this.SendOrderBox.ReceiveMessage));
				frmMain._OnMessageReceived = (frmMain.OnMessageRecievedEventHendler)Delegate.Combine(frmMain._OnMessageReceived, new frmMain.OnMessageRecievedEventHendler(this.SendOrderBox.ReceiveMessage));
				frmMain._OnMessageTfexReceived = (frmMain.OnMessageTfexRecievedEventHendler)Delegate.Remove(frmMain._OnMessageTfexReceived, new frmMain.OnMessageTfexRecievedEventHendler(this.SendOrderBox.ReceiveTfexMessage));
				frmMain._OnMessageTfexReceived = (frmMain.OnMessageTfexRecievedEventHendler)Delegate.Combine(frmMain._OnMessageTfexReceived, new frmMain.OnMessageTfexRecievedEventHendler(this.SendOrderBox.ReceiveTfexMessage));
				frmMain._OnMessageReceived = (frmMain.OnMessageRecievedEventHendler)Delegate.Remove(frmMain._OnMessageReceived, new frmMain.OnMessageRecievedEventHendler(this.ViewOrderBox.ReceiveMessage));
				frmMain._OnMessageReceived = (frmMain.OnMessageRecievedEventHendler)Delegate.Combine(frmMain._OnMessageReceived, new frmMain.OnMessageRecievedEventHendler(this.ViewOrderBox.ReceiveMessage));
				frmMain._OnMessageTfexReceived = (frmMain.OnMessageTfexRecievedEventHendler)Delegate.Remove(frmMain._OnMessageTfexReceived, new frmMain.OnMessageTfexRecievedEventHendler(this.ViewOrderBox.ReceiveTfexMessage));
				frmMain._OnMessageTfexReceived = (frmMain.OnMessageTfexRecievedEventHendler)Delegate.Combine(frmMain._OnMessageTfexReceived, new frmMain.OnMessageTfexRecievedEventHendler(this.ViewOrderBox.ReceiveTfexMessage));
				frmMain._OnMessageReceived = (frmMain.OnMessageRecievedEventHendler)Delegate.Remove(frmMain._OnMessageReceived, new frmMain.OnMessageRecievedEventHendler(this.BroadcastMessageBox.ReceiveMessage));
				frmMain._OnMessageReceived = (frmMain.OnMessageRecievedEventHendler)Delegate.Combine(frmMain._OnMessageReceived, new frmMain.OnMessageRecievedEventHendler(this.BroadcastMessageBox.ReceiveMessage));
				frmMain._OnMessageTfexReceived = (frmMain.OnMessageTfexRecievedEventHendler)Delegate.Remove(frmMain._OnMessageTfexReceived, new frmMain.OnMessageTfexRecievedEventHendler(this.BroadcastMessageBox.ReceiveTfexMessage));
				frmMain._OnMessageTfexReceived = (frmMain.OnMessageTfexRecievedEventHendler)Delegate.Combine(frmMain._OnMessageTfexReceived, new frmMain.OnMessageTfexRecievedEventHendler(this.BroadcastMessageBox.ReceiveTfexMessage));
				frmMain._OnMessageReceived = (frmMain.OnMessageRecievedEventHendler)Delegate.Remove(frmMain._OnMessageReceived, new frmMain.OnMessageRecievedEventHendler(this.TickerSlideBox.ReceiveMessage));
				frmMain._OnMessageReceived = (frmMain.OnMessageRecievedEventHendler)Delegate.Combine(frmMain._OnMessageReceived, new frmMain.OnMessageRecievedEventHendler(this.TickerSlideBox.ReceiveMessage));
				frmMain._OnMessageTfexReceived = (frmMain.OnMessageTfexRecievedEventHendler)Delegate.Remove(frmMain._OnMessageTfexReceived, new frmMain.OnMessageTfexRecievedEventHendler(this.TickerSlideBox.ReceiveTfexMessage));
				frmMain._OnMessageTfexReceived = (frmMain.OnMessageTfexRecievedEventHendler)Delegate.Combine(frmMain._OnMessageTfexReceived, new frmMain.OnMessageTfexRecievedEventHendler(this.TickerSlideBox.ReceiveTfexMessage));
				frmMain._OnMessageReceived = (frmMain.OnMessageRecievedEventHendler)Delegate.Remove(frmMain._OnMessageReceived, new frmMain.OnMessageRecievedEventHendler(this.Smart1ClickBox.ReceiveMessage));
				frmMain._OnMessageReceived = (frmMain.OnMessageRecievedEventHendler)Delegate.Combine(frmMain._OnMessageReceived, new frmMain.OnMessageRecievedEventHendler(this.Smart1ClickBox.ReceiveMessage));
				this.panBottom.Font = Settings.Default.Default_Font;
				ApplicationInfo.UserPincodeWrongCount = 0;
				this.Resize_Menu();
				Font font;
				if (ApplicationInfo.IsScreen125)
				{
					font = new Font("Segoe UI", 9.2f);
				}
				else
				{
					font = new Font("Segoe UI", 10.2f);
				}
				if (this.btnMarketWatch.Font != font)
				{
					this.btnMarketWatch.Font = font;
					this.btnTopBBOs.Font = font;
					this.btnSummary.Font = font;
					this.btnMarketInfo.Font = font;
					this.btnRanking.Font = font;
					this.btnNews.Font = font;
					this.btnViewOrder.Font = font;
					this.btnPortfolio.Font = font;
					this.btnStockHistory.Font = font;
					this.btnBatchOrder.Font = font;
					this.btnEservice.Font = font;
					this.btnLogout.Font = new Font(font.Name, font.Size - 1.25f);
					this.btnMarketWatch.Left = 3;
					this.btnTopBBOs.Left = this.btnMarketWatch.Right;
					this.btnSummary.Left = this.btnTopBBOs.Right;
					this.btnMarketInfo.Left = this.btnSummary.Right;
					this.btnRanking.Left = this.btnMarketInfo.Right;
					this.btnNews.Left = this.btnRanking.Right;
					this.btnViewOrder.Left = this.btnNews.Right;
					this.btnPortfolio.Left = this.btnViewOrder.Right;
					this.btnStockHistory.Left = this.btnPortfolio.Right;
					this.btnBatchOrder.Left = this.btnStockHistory.Right;
					this.btnEservice.Left = this.btnBatchOrder.Right;
					this.SETBox.Left = this.pici2Logo.Right + 5;
					this.SET2Box.Left = this.SETBox.Right + 5;
					this.SectorBox.Left = this.SET2Box.Right + 5;
				}
				if (ApplicationInfo.BrokerId == 4)
				{
					this.btnBatchOrder.Visible = ApplicationInfo.StopOrderSupported;
				}
				else
				{
					this.btnBatchOrder.Visible = (ApplicationInfo.SupportBatchOrder == "Y");
				}
				if (ApplicationInfo.IsSupportEservice)
				{
					this.btnEservice.Visible = true;
				}
				this.panelMenu.Visible = true;
				this.SendOrderBox.Hide();
				this.ViewOrderBox.Hide();
				this.BroadcastMessageBox.Hide();
				this.btnResizeMD1.Hide();
				this.Resize_BottomBox();
				this.panelSep.Show();
				this.panBottom.Show();
				this.SendOrderBox.InitAccount();
				this.Resize_SendOrderBox();
				this.SendOrderBox.Visible = true;
				this.Resize_ViewOrderBox_BcBox();
				this.ViewOrderBox.Visible = true;
				this.btnResizeMD1.Visible = true;
				this.BroadcastMessageBox.Visible = true;
				this.Resize_DockR();
				this.ViewOrderBox.IsShowToolsBar = true;
				this.ViewOrderBox.SetHeaderHeightPct(80);
				if (Settings.Default.BottomSizePct == 0m)
				{
					Settings.Default.BottomSizePct = 0.6m;
				}
				this.SendOrderBox.IsActive = true;
				this.CloseSpashForm();
				this.ShowSpashForm("Open Default Template.");
				if (string.IsNullOrEmpty(TemplateManager.Instance.DefaultTemplateName))
				{
					this.OpenTemplate("Market Watch");
				}
				else
				{
					this.OpenTemplate(TemplateManager.Instance.DefaultTemplateName);
				}
				this._isSessionTimout = false;
				if (this.timerHeartBeat == null)
				{
					this.timerHeartBeat = new System.Timers.Timer();
					this.timerHeartBeat.Interval = (double)ApplicationInfo.SendHeartBeatInterval;
					this.timerHeartBeat.Elapsed += new ElapsedEventHandler(this.timerHeartBeat_Elapsed);
				}
				this.timerHeartBeat.Start();
				if (!ApplicationInfo.IsUseProxy)
				{
					ApplicationInfo.IsPushMode = true;
				}
				if (this.timmerStartBC == null)
				{
					this.timmerStartBC = new System.Windows.Forms.Timer();
					this.timmerStartBC.Interval = 1000;
					this.timmerStartBC.Tick += new EventHandler(this.timmerStartBC_Tick);
				}
				this.timmerStartBC.Enabled = false;
				this.timmerStartBC.Enabled = true;
				if (ApplicationInfo.AutoGetOrderInfoInterval < 1)
				{
					ApplicationInfo.AutoGetOrderInfoInterval = 1;
				}
				this.timerGetOrderInfo.Enabled = false;
				this.timerGetOrderInfo.Enabled = true;
				SystemEvents.PowerModeChanged -= new PowerModeChangedEventHandler(this.SystemEvents_PowerModeChanged);
				SystemEvents.PowerModeChanged += new PowerModeChangedEventHandler(this.SystemEvents_PowerModeChanged);
			}
			catch (Exception ex2)
			{
				this.ShowError("bgwLoadProfile_RunWorkerCompleted", ex2);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
		{
			try
			{
				this._powerMode = e.Mode;
				if (this._powerMode == PowerModes.Suspend)
				{
					if (this.timerMonitorFeed != null)
					{
						this.timerMonitorFeed.Enabled = false;
					}
					if (!ApplicationInfo.IsPushMode)
					{
						this.stopBC();
					}
					else
					{
						if (this.feedsWebProxy != null && this.feedsWebProxy.IsServiceStarted)
						{
							this.feedsWebProxy.Stop();
						}
					}
				}
				else
				{
					this.timer1.Start();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SystemEvents_PowerModeChanged", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void timmerStartBC_Tick(object sender, EventArgs e)
		{
			try
			{
				this.timmerStartBC.Enabled = false;
				if (this._bcMessageFactory == null)
				{
					this._bcMessageFactory = new BroadcastMessageFactory();
				}
				this.startBC();
			}
			catch (Exception ex)
			{
				this.ShowError("timmerStartBC_Tick", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void Logout(bool isForce)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new frmMain.LogoutCallBack(this.Logout), new object[]
				{
					isForce
				});
			}
			else
			{
				try
				{
					this.CloseSpashForm();
					DialogResult dialogResult;
					if (this._isSessionTimout)
					{
						dialogResult = DialogResult.Yes;
					}
					else
					{
						if (!isForce)
						{
							dialogResult = MessageBox.Show("Would you like to logout?", "Logout", MessageBoxButtons.YesNo);
						}
						else
						{
							dialogResult = DialogResult.Yes;
						}
					}
					if (dialogResult == DialogResult.Yes)
					{
						if (this.bgwLogout == null)
						{
							this.bgwLogout = new BackgroundWorker();
							this.bgwLogout.WorkerReportsProgress = true;
							this.bgwLogout.DoWork += new DoWorkEventHandler(this.bgwLogout_DoWork);
							this.bgwLogout.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwLogout_RunWorkerCompleted);
						}
						this.ShowSpashForm("Logout...");
						this.isTerminate = isForce;
						if (!this.bgwLogout.IsBusy)
						{
							this.bgwLogout.RunWorkerAsync();
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("Logout", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwLogout_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				this.timmerStartBC.Enabled = false;
				this.timerMonitorFeed.Enabled = false;
				this.timerHeartBeat.Enabled = false;
				this.timerCallLoginForm.Enabled = false;
				this.timerGetOrderInfo.Enabled = false;
				this._isRunPeekQ = false;
				if (!string.IsNullOrEmpty(ApplicationInfo.UserSessionID))
				{
					for (int i = 1; i <= 5; i++)
					{
						string text = string.Empty;
						string text2 = string.Empty;
						foreach (string current in ApplicationInfo.FavStockList[i])
						{
							text = text + ";" + current;
						}
						if (text.Length > 0)
						{
							text = text.Substring(1);
							if (!ApplicationInfo.IsSupportTfex)
							{
								text2 = ApplicationInfo.WebUserService.SaveUserConfig(ApplicationInfo.UserLoginID, "FAV" + i, text);
							}
							else
							{
								text2 = ApplicationInfo.WebUserService.SaveUserConfig(ApplicationInfo.UserLoginID, "FAV" + i + "T", text);
							}
							if (text2 != "ok")
							{
								this.ShowError("SaveFavorites", new Exception(text2));
							}
						}
					}
					string text3 = string.Empty;
					for (int j = 0; j < ApplicationInfo.TickerStockList.Count; j++)
					{
						text3 = text3 + ";" + ApplicationInfo.TickerStockList[j];
					}
					if (text3.Length > 0)
					{
						text3 = text3.Substring(1);
						string text2 = string.Empty;
						if (!ApplicationInfo.IsSupportTfex)
						{
							text2 = ApplicationInfo.WebUserService.SaveUserConfig(ApplicationInfo.UserLoginID, "TICKER_FIL", text3);
						}
						else
						{
							text2 = ApplicationInfo.WebUserService.SaveUserConfig(ApplicationInfo.UserLoginID, "TICKER_FILT", text3);
						}
						if (text2 != "ok")
						{
							this.ShowError("SaveTickerFilter", new Exception(text2));
						}
					}
					ApplicationInfo.WebUserService.Logout(Convert.ToInt32(ApplicationInfo.UserSessionID), ApplicationInfo.UserLoginMode, ApplicationInfo.UserLoginID);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("LogOut", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwLogout_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				ApplicationInfo.IsAreadyLogin = false;
				AlertManager.Instance.IsMonitoring = false;
				if (this.feedsWebProxy != null)
				{
					this.feedsWebProxy.Stop();
					this.feedsWebProxy.OnDataIN -= new FeedsWebProxy.OnDataInHandler(this.feedsWebProxy_OnDataIN);
					this.feedsWebProxy.OnError -= new FeedsWebProxy.OnErrorHandler(this.feedsWebProxy_OnError);
					this.feedsWebProxy.OnGettingHttp -= new EventHandler(this.feedsWebProxy_OnGettingHttp);
					this.feedsWebProxy.OnGettedHttp -= new EventHandler(this.feedsWebProxy_OnGettedHttp);
					this.feedsWebProxy.OnStoped -= new EventHandler(this.feedsWebProxy_OnStoped);
					this.feedsWebProxy.OnStarted -= new EventHandler(this.feedsWebProxy_OnStarted);
					this.feedsWebProxy = null;
				}
				this.stopBC();
				if (this._qMessage != null)
				{
					this._qMessage.Clear();
					this._qMessage = null;
				}
				SystemEvents.PowerModeChanged -= new PowerModeChangedEventHandler(this.SystemEvents_PowerModeChanged);
				frmMain._OnMessageReceived = (frmMain.OnMessageRecievedEventHendler)Delegate.Remove(frmMain._OnMessageReceived, new frmMain.OnMessageRecievedEventHendler(this.SendOrderBox.ReceiveMessage));
				frmMain._OnMessageTfexReceived = (frmMain.OnMessageTfexRecievedEventHendler)Delegate.Remove(frmMain._OnMessageTfexReceived, new frmMain.OnMessageTfexRecievedEventHendler(this.SendOrderBox.ReceiveTfexMessage));
				frmMain._OnMessageReceived = (frmMain.OnMessageRecievedEventHendler)Delegate.Remove(frmMain._OnMessageReceived, new frmMain.OnMessageRecievedEventHendler(this.ViewOrderBox.ReceiveMessage));
				frmMain._OnMessageTfexReceived = (frmMain.OnMessageTfexRecievedEventHendler)Delegate.Remove(frmMain._OnMessageTfexReceived, new frmMain.OnMessageTfexRecievedEventHendler(this.ViewOrderBox.ReceiveTfexMessage));
				frmMain._OnMessageReceived = (frmMain.OnMessageRecievedEventHendler)Delegate.Remove(frmMain._OnMessageReceived, new frmMain.OnMessageRecievedEventHendler(this.BroadcastMessageBox.ReceiveMessage));
				frmMain._OnMessageTfexReceived = (frmMain.OnMessageTfexRecievedEventHendler)Delegate.Remove(frmMain._OnMessageTfexReceived, new frmMain.OnMessageTfexRecievedEventHendler(this.BroadcastMessageBox.ReceiveTfexMessage));
				frmMain._OnMessageReceived = (frmMain.OnMessageRecievedEventHendler)Delegate.Remove(frmMain._OnMessageReceived, new frmMain.OnMessageRecievedEventHendler(this.TickerSlideBox.ReceiveMessage));
				frmMain._OnMessageTfexReceived = (frmMain.OnMessageTfexRecievedEventHendler)Delegate.Remove(frmMain._OnMessageTfexReceived, new frmMain.OnMessageTfexRecievedEventHendler(this.TickerSlideBox.ReceiveTfexMessage));
				frmMain._OnMessageReceived = (frmMain.OnMessageRecievedEventHendler)Delegate.Remove(frmMain._OnMessageReceived, new frmMain.OnMessageRecievedEventHendler(this.Smart1ClickBox.ReceiveMessage));
				frmMain._OnMessageReceived = (frmMain.OnMessageRecievedEventHendler)Delegate.Remove(frmMain._OnMessageReceived, new frmMain.OnMessageRecievedEventHendler(AlertManager.Instance.ReceiveMessage));
				frmMain._OnMessageTfexReceived = (frmMain.OnMessageTfexRecievedEventHendler)Delegate.Remove(frmMain._OnMessageTfexReceived, new frmMain.OnMessageTfexRecievedEventHendler(AlertManager.Instance.ReceiveTfexMessage));
				try
				{
					AlertManager.Instance.OnAlert -= new AlertManager.OnAlertHandler(this.OnAlert);
					AlertManager.Instance.SaveXML();
				}
				catch (Exception ex)
				{
					EventLog.WriteEntry("i2TradePlus", "SaveAlert::" + ex.Message, EventLogEntryType.Error);
				}
				this.SetTopPanelActive(false);
				if (this._alertDetailForm != null)
				{
					if (!this._alertDetailForm.IsDisposed)
					{
						this._alertDetailForm.Dispose();
					}
					this._alertDetailForm = null;
				}
				if (this._systemOptionForm != null)
				{
					if (!this._systemOptionForm.IsDisposed)
					{
						this._systemOptionForm.Dispose();
					}
					this._systemOptionForm = null;
				}
				this.ClearAllTemplate();
				this.SaveAppInfo();
				if (base.WindowState == FormWindowState.Maximized)
				{
					Settings.Default.Default_WindowMaximizeState = true;
					Settings.Default.Default_Bound = base.Bounds;
				}
				else
				{
					if (base.WindowState != FormWindowState.Minimized)
					{
						Settings.Default.Default_WindowMaximizeState = false;
						Settings.Default.Default_Bound = base.Bounds;
					}
				}
				Settings.Default.Default_MainScreenHeight = this._BTop;
				Settings.Default.Save();
				if (this.ViewOrderBox != null)
				{
					this.ViewOrderBox.DisposeMe();
				}
				this.SendOrderBox.DisposeMe();
				this.panelMenu.Hide();
				this.panelControlDockR.Hide();
				this.panelDockR.Hide();
				this.panBottom.Hide();
				this.CloseSpashForm();
				ExceptionManager.Close();
				if (this._isSessionTimout)
				{
					this._isSessionTimout = false;
					MessageBox.Show("Your session has expired. \nOr Another sign with the same account!.", "Logout", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				if (ApplicationInfo.StockAutoComp != null)
				{
					ApplicationInfo.StockAutoComp.Clear();
					ApplicationInfo.StockAutoComp = null;
				}
				if (ApplicationInfo.StockInfo != null)
				{
					ApplicationInfo.StockInfo.Items.Clear();
					ApplicationInfo.StockInfo = null;
				}
				if (ApplicationInfo.IndexStatInfo != null)
				{
					ApplicationInfo.IndexStatInfo.Items.Clear();
					ApplicationInfo.IndexStatInfo = null;
				}
				if (ApplicationInfo.TunnelHosts != null)
				{
					ApplicationInfo.TunnelHosts.Clear();
					ApplicationInfo.TunnelHosts = null;
				}
				if (ApplicationInfo.AccInfo != null)
				{
					ApplicationInfo.AccInfo.Items.Clear();
				}
				if (ApplicationInfo.FavStockList != null)
				{
					ApplicationInfo.FavStockList.Clear();
					ApplicationInfo.FavStockList = null;
				}
				base.Close();
			}
			catch (Exception ex2)
			{
				EventLog.WriteEntry("i2Trade", ex2.Message, EventLogEntryType.Error);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void DisplayMessageBox(string message)
		{
			if (base.InvokeRequired)
			{
				base.BeginInvoke(new frmMain.DisplayMessageBoxCallBack(this.DisplayMessageBox), new object[]
				{
					message
				});
			}
			else
			{
				MessageBox.Show(message, "");
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OpenTemplate(string templateName)
		{
			this.OpenTemplate(templateName, "");
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OpenTemplate(string templateName, string subPage)
		{
			try
			{
				if (!this.IsOpeningTemplate)
				{
					if (!(this.OpenTemplateLastTime.AddMilliseconds(100.0) >= DateTime.Now))
					{
						if (TemplateManager.Instance.CurrentActiveTemplateView != null)
						{
							this._lastTemplate = TemplateManager.Instance.CurrentActiveTemplateView.Name;
						}
						if (this.ViewOrderBox != null)
						{
							this.ViewOrderBox.CloseViewOrderInfoBox();
						}
						this.OpenTemplateLastTime = DateTime.Now;
						this.IsOpeningTemplate = true;
						if (TemplateManager.Instance.IsTemplateContains(templateName))
						{
							if (subPage != string.Empty && templateName == "Stock Summary")
							{
								((frmStockSummary)TemplateManager.Instance.TemplateViews["Stock Summary"].FormObj).SubPage = subPage;
							}
							TemplateManager.Instance.Show(templateName);
							this.SetMenu(templateName);
							this.IsOpeningTemplate = false;
						}
						else
						{
							string text = string.Empty;
							bool flag = false;
							text = ApplicationInfo.GetTemplatePathByUser();
							if (text != string.Empty)
							{
								text = text + "\\" + templateName + ".xml";
								if (File.Exists(text))
								{
									flag = true;
								}
							}
							TemplateView templateView = templateView = TemplateManager.Instance.CreateTemplateView(templateName);
							templateView.CurrentState = TemplateView.ContentState.OpeningOrClosing;
							Dictionary<string, object> dictionary = new Dictionary<string, object>();
							if (flag)
							{
								TemplateDS templateDS = new TemplateDS();
								templateDS.ReadXml(text);
								TemplateDS.FormRememberFieldRow[] array = (TemplateDS.FormRememberFieldRow[])templateDS.FormRememberField.Select();
								TemplateDS.FormRememberFieldRow[] array2 = array;
								for (int i = 0; i < array2.Length; i++)
								{
									TemplateDS.FormRememberFieldRow formRememberFieldRow = array2[i];
									dictionary.Add(formRememberFieldRow.FieldName, formRememberFieldRow.FieldValue);
								}
							}
							List<MainFixedTemplate.ControlClient> list = MainFixedTemplate.GetFixedTemplate(templateName);
							foreach (MainFixedTemplate.ControlClient current in list)
							{
								if (dictionary.Count == 0)
								{
									dictionary = current.Property;
								}
								Form form;
								if (subPage == string.Empty)
								{
									form = (Form)Activator.CreateInstance(current.ControlType, new object[]
									{
										dictionary
									});
								}
								else
								{
									form = (Form)Activator.CreateInstance(current.ControlType, new object[]
									{
										dictionary,
										subPage
									});
								}
								Form expr_2BE = form;
								expr_2BE.Name += form.GetHashCode().ToString();
								form.FormBorderStyle = FormBorderStyle.None;
								form.TopLevel = false;
								form.Bounds = this.GetWorkingArea((ClientBaseForm)form);
								form.Font = Settings.Default.Default_Font;
								form.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
								form.Parent = this;
								this.SetEventToNewForm(form);
								TemplateManager.Instance.AddFormToTemplate(templateName, form, current.FormIndex);
								Thread.Sleep(10);
							}
							list.Clear();
							list = null;
							dictionary.Clear();
							dictionary = null;
							TemplateManager.Instance.SetTabControl(templateName);
							TemplateManager.Instance.Show(templateName);
							this.SetMenu(templateName);
							templateView.CurrentState = TemplateView.ContentState.Unchanged;
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("OpenTemplate", ex);
			}
			finally
			{
				this.IsOpeningTemplate = false;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SaveTemplate()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(this.SaveTemplate));
			}
			else
			{
				try
				{
					string text = string.Empty;
					string text2 = string.Empty;
					string name = TemplateManager.Instance.CurrentActiveTemplateView.Name;
					TemplateView.ContentState templateState = TemplateManager.Instance.GetTemplateState(TemplateManager.Instance.CurrentActiveTemplateView.Name);
					text = TemplateManager.Instance.CurrentActiveTemplateView.Name;
					text2 = ApplicationInfo.GetTemplatePathByUser();
					if (!string.IsNullOrEmpty(text))
					{
						Form activeMdiChild = base.ActiveMdiChild;
						string str = text + ".xml";
						TemplateDS templateDS = new TemplateDS();
						Dictionary<string, object> templateProperties = TemplateManager.Instance.GetTemplateProperties(text);
						if (templateProperties != null)
						{
							TemplateDS.TemplatePropertyRow templatePropertyRow = templateDS.TemplateProperty.NewTemplatePropertyRow();
							templatePropertyRow.PropertyName = "Name";
							templatePropertyRow.PropertyValue = text;
							templateDS.TemplateProperty.AddTemplatePropertyRow(templatePropertyRow);
							foreach (KeyValuePair<string, object> current in templateProperties)
							{
								if (current.Key != "Name")
								{
									templatePropertyRow = templateDS.TemplateProperty.NewTemplatePropertyRow();
									templatePropertyRow.PropertyName = current.Key;
									templatePropertyRow.PropertyValue = current.Value.ToString();
									templateDS.TemplateProperty.AddTemplatePropertyRow(templatePropertyRow);
								}
							}
							List<Dictionary<string, object>> allFormProperties = TemplateManager.Instance.GetAllFormProperties(text);
							if (allFormProperties != null && allFormProperties.Count > 0)
							{
								int num = 0;
								foreach (Dictionary<string, object> current2 in allFormProperties)
								{
									num++;
									if (current2 != null)
									{
										TemplateDS.FormInfoRow formInfoRow = templateDS.FormInfo.NewFormInfoRow();
										formInfoRow.FormID = num;
										foreach (KeyValuePair<string, object> current3 in current2)
										{
											if (current3.Key == "Type")
											{
												formInfoRow.Type = current3.Value.ToString();
											}
											else
											{
												if (current3.Key == "Name")
												{
													formInfoRow.Name = current3.Value.ToString();
												}
												else
												{
													TemplateDS.FormPropertyRow formPropertyRow = templateDS.FormProperty.NewFormPropertyRow();
													formPropertyRow.FormID = num;
													formPropertyRow.PropertyName = current3.Key;
													if (current3.Key == "Font")
													{
														Font font = (Font)current3.Value;
														formPropertyRow.PropertyValue = string.Format("{0}|{1}|{2}", font.Name, font.Size, (int)font.Style);
													}
													else
													{
														formPropertyRow.PropertyValue = current3.Value.ToString();
													}
													templateDS.FormProperty.AddFormPropertyRow(formPropertyRow);
												}
											}
										}
										templateDS.FormInfo.AddFormInfoRow(formInfoRow);
										if (current2.ContainsKey("Name"))
										{
											Dictionary<string, object> formPropertyFields = TemplateManager.Instance.GetFormPropertyFields(text, current2["Name"].ToString());
											if (formPropertyFields != null)
											{
												foreach (KeyValuePair<string, object> current4 in formPropertyFields)
												{
													TemplateDS.FormRememberFieldRow formRememberFieldRow = templateDS.FormRememberField.NewFormRememberFieldRow();
													formRememberFieldRow.FormID = num;
													formRememberFieldRow.FieldName = current4.Key;
													formRememberFieldRow.FieldValue = current4.Value.ToString();
													templateDS.FormRememberField.AddFormRememberFieldRow(formRememberFieldRow);
												}
											}
										}
									}
								}
								try
								{
									if (!Directory.Exists(text2))
									{
										Directory.CreateDirectory(text2);
									}
									templateDS.WriteXml(text2 + "\\" + str);
								}
								catch (Exception ex)
								{
									this.ShowError("SaveTemplate", ex);
									return;
								}
							}
						}
					}
					text = null;
					text2 = null;
				}
				catch (Exception ex)
				{
					this.ShowError("SaveTemplate", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void templateMenu_OnOpenTemplate(string templateName, string templateGroup)
		{
			if (templateName != null)
			{
				if (templateName == "Buy Order" || templateName == "Sell Order" || templateName == "Short Sell Order" || templateName == "Cover Buy Order")
				{
					this.OpenSendNewOrderForm(templateName);
					return;
				}
			}
			this.OpenTemplate(templateName);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowError(string methodName, Exception ex)
		{
			ExceptionManager.Show(new ErrorItem(DateTime.Now, base.Name, methodName, ex.Message, ex.ToString()));
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void timerHeartBeat_Elapsed(object sender, ElapsedEventArgs e)
		{
			this.timerHeartBeat.Stop();
			try
			{
				if (ApplicationInfo.IsAreadyLogin && this._powerMode != PowerModes.Suspend)
				{
					if (!this._isCheckHBworking)
					{
						this._isCheckHBworking = true;
						string text = ApplicationInfo.TunnelHosts[ApplicationInfo.TunnelCounter].HostIP + ":" + ApplicationInfo.TunnelHosts[ApplicationInfo.TunnelCounter].Port;
						this.param = (ApplicationInfo.IsPushMode ? "1" : "0");
						ApplicationInfo.WebUserService.SendHeartBeat2Completed += new SendHeartBeat2CompletedEventHandler(this.WebUserService_SendHeartBeat2Completed);
						ApplicationInfo.WebUserService.SendHeartBeat2Async(string.Concat(new string[]
						{
							"user_id=",
							ApplicationInfo.UserLoginID,
							"|session=",
							ApplicationInfo.UserSessionID,
							"|web_ip=",
							new Uri(ApplicationInfo.WebService.Url).Host,
							"|conn_mode=",
							this.param,
							"|tunnel_info=",
							text
						}));
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("HeartBeat", ex);
			}
			if (ApplicationInfo.IsAreadyLogin)
			{
				if (!this._isSessionTimout)
				{
					this.timerHeartBeat.Start();
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void WebUserService_SendHeartBeat2Completed(object sender, SendHeartBeat2CompletedEventArgs e)
		{
			try
			{
				ApplicationInfo.WebUserService.SendHeartBeat2Completed -= new SendHeartBeat2CompletedEventHandler(this.WebUserService_SendHeartBeat2Completed);
				if (e.Error == null)
				{
					bool flag = false;
					bool.TryParse(e.Result.ToString(), out flag);
					this._isSessionTimout = !flag;
					if (this._isSessionTimout)
					{
						this.Logout(true);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("HeartBeatCallBack", ex);
			}
			this._isCheckHBworking = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void WebUserService_SendHeartBeatCompleted(object sender, SendHeartBeatCompletedEventArgs e)
		{
			try
			{
				ApplicationInfo.WebUserService.SendHeartBeatCompleted -= new SendHeartBeatCompletedEventHandler(this.WebUserService_SendHeartBeatCompleted);
				if (e.Error == null)
				{
					bool flag = false;
					bool.TryParse(e.Result.ToString(), out flag);
					this._isSessionTimout = !flag;
					if (this._isSessionTimout)
					{
						this.Logout(true);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("HeartBeatCallBack", ex);
			}
			this._isCheckHBworking = false;
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
		public void Instance_SetBottomSize()
		{
			if (base.WindowState != FormWindowState.Minimized)
			{
				if (this.tmBottomClientResize == null)
				{
					this.tmBottomClientResize = new System.Windows.Forms.Timer();
					this.tmBottomClientResize.Interval = 50;
					this.tmBottomClientResize.Tick += new EventHandler(this.tmBottomClientResize_Tick);
				}
				this.tmBottomClientResize.Enabled = false;
				this.tmBottomClientResize.Enabled = true;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void Resize_Menu()
		{
			int num = base.ClientSize.Width * 26 / 100;
			this.panelMenu.SetBounds(1, this.panelTop.Bottom + 1, base.ClientSize.Width - num - 4, this.panelMenu.Height);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void Resize_DockR()
		{
			this.panelControlDockR.SetBounds(this.panelMenu.Right + 1, this.panelMenu.Top, base.ClientSize.Width - (this.panelMenu.Right + 1) - 1, this.panelMenu.Height);
			if (!this.panelControlDockR.Visible)
			{
				this.panelControlDockR.Visible = true;
			}
			this.panelDockR.SuspendLayout();
			this.panelDockR.SetBounds(this.panelControlDockR.Left, this.panelControlDockR.Bottom + 1, this.panelControlDockR.Width, this._BTop - this.panelControlDockR.Bottom - 2);
			if (!this.panelDockR.Visible)
			{
				this.panelDockR.Visible = true;
			}
			if (this._slideType == 1)
			{
				this.TickerSlideBox.SetBounds(0, 0, this.panelDockR.Width, this.panelDockR.Height);
				if (!this.TickerSlideBox.IsInit)
				{
					this.TickerSlideBox.Init();
					this.TickerSlideBox.SetResize();
				}
				else
				{
					this.TickerSlideBox.SetResize();
				}
				this.TickerSlideBox.Visible = true;
			}
			else
			{
				if (this._slideType == 2)
				{
					this.Smart1ClickBox.SetBounds(0, 0, this.panelDockR.Width, this.panelDockR.Height);
					this.Smart1ClickBox.SetResize();
					this.Smart1ClickBox.Visible = true;
				}
			}
			if (this._slideType != 1)
			{
				this.TickerSlideBox.Visible = false;
			}
			if (this._slideType != 2)
			{
				this.Smart1ClickBox.Visible = false;
			}
			this.panelDockR.ResumeLayout();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void Resize_SendOrderBox()
		{
			this.SendOrderBox.SuspendLayout();
			this.SendOrderBox.SetBounds(1, 1, base.ClientSize.Width - 2, this.SendOrderBox.Height);
			this.SendOrderBox.SetVisibleControlEquity();
			this.SendOrderBox.SetResize();
			this.SendOrderBox.ResumeLayout();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void Resize_BottomBox()
		{
			this.panelSep.SetBounds(base.ClientRectangle.Left, this._BTop, base.ClientRectangle.Width, 2);
			this.panBottom.SetBounds(0, this._BTop + this.panelSep.Height, base.ClientSize.Width, base.ClientSize.Height - this._BTop - 2);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tmBottomClientResize_Tick(object sender, EventArgs e)
		{
			try
			{
				if (this.tmBottomClientResize != null)
				{
					this.tmBottomClientResize.Enabled = false;
				}
				this.Resize_Menu();
				this.panBottom.SuspendLayout();
				this.Resize_SendOrderBox();
				this.Resize_BottomBox();
				this.Resize_ViewOrderBox_BcBox();
				this.panBottom.ResumeLayout(false);
				this.Resize_DockR();
				if (TemplateManager.Instance.CurrentActiveTemplateView != null)
				{
					TemplateManager.Instance.CurrentActiveTemplateView.SetFormSize();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tmBottomClientResize_Tick", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnResizeMD_Click(object sender, EventArgs e)
		{
			if (this._isResizeMDNext)
			{
				if (Settings.Default.BottomSizePct == 0.6m)
				{
					Settings.Default.BottomSizePct = 1m;
				}
				else
				{
					Settings.Default.BottomSizePct += 0.1m;
				}
			}
			else
			{
				if (Settings.Default.BottomSizePct >= 1m)
				{
					Settings.Default.BottomSizePct = 0.6m;
				}
				else
				{
					Settings.Default.BottomSizePct -= 0.1m;
				}
			}
			this.Resize_ViewOrderBox_BcBox();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void Resize_ViewOrderBox_BcBox()
		{
			try
			{
				this.ViewOrderBox.SetBounds(this.SendOrderBox.Left, this.SendOrderBox.Bottom + 1, Convert.ToInt32((this.SendOrderBox.Width - this.btnResizeMD1.Width - 2) * Settings.Default.BottomSizePct), this.panBottom.Height - this.SendOrderBox.Height - 4);
				this.ViewOrderBox.SetResize(true);
				if (Settings.Default.BottomSizePct <= 0.5m)
				{
					this.btnResizeMD1.Image = Resources.MoveNextHS;
					this._isResizeMDNext = true;
				}
				else
				{
					if (Settings.Default.BottomSizePct >= 1m)
					{
						this.btnResizeMD1.Image = Resources.MovePreviousHS;
						this._isResizeMDNext = false;
					}
				}
				this.btnResizeMD1.SetBounds(this.ViewOrderBox.Right, this.ViewOrderBox.Top, 14, this.ViewOrderBox.Height);
				this.BroadcastMessageBox.SetBounds(this.btnResizeMD1.Right, this.ViewOrderBox.Top, this.panBottom.Width - (this.btnResizeMD1.Right + 1), this.ViewOrderBox.Height);
				this.BroadcastMessageBox.SetResize();
			}
			catch (Exception ex)
			{
				this.panBottom.ResumeLayout();
				this.ShowError("ShowControlInBottomBox", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void timerGetOrderInfo_Tick(object sender, EventArgs e)
		{
			try
			{
				if (ApplicationInfo.AutoGetOrderNoList.Count > 0)
				{
					this.timerGetOrderInfo.Enabled = false;
					string text = string.Empty;
					List<string> list = new List<string>();
					foreach (KeyValuePair<string, DateTime> current in ApplicationInfo.AutoGetOrderNoList)
					{
						if (current.Value.AddSeconds((double)ApplicationInfo.AutoGetOrderInfoInterval) < DateTime.Now)
						{
							text = text + "," + current.Key;
							list.Add(current.Key);
						}
					}
					foreach (string current2 in list)
					{
						ApplicationInfo.RemoveOrderNoFromAutoRefreshList(current2, "");
					}
					list.Clear();
					list = null;
					if (text.Length > 0)
					{
						text = text.Substring(1);
					}
					if (!string.IsNullOrEmpty(text))
					{
						string text2 = ApplicationInfo.WebOrderService.ViewOrderByOrderNo(ApplicationInfo.AccInfo.CurrentAccount, text);
						if (!string.IsNullOrEmpty(text2))
						{
							using (DataSet dataSet = new DataSet())
							{
								MyDataHelper.StringToDataSet(text2, dataSet);
								if (dataSet.Tables.Contains("ORDERS"))
								{
									long volume = 0L;
									long matchedVolume = 0L;
									long num = 0L;
									long orderNumber = 0L;
									string text3 = string.Empty;
									decimal priceForCal = 0m;
									decimal matcedValue = 0m;
									string approverId = string.Empty;
									string sendDate = string.Empty;
									string orderTime = string.Empty;
									foreach (DataRow dataRow in dataSet.Tables["ORDERS"].Rows)
									{
										long.TryParse(dataRow["order_number"].ToString(), out orderNumber);
										long.TryParse(dataRow["volume"].ToString(), out volume);
										long.TryParse(dataRow["matched_volume"].ToString(), out matchedVolume);
										long.TryParse(dataRow["pub_vol"].ToString(), out num);
										text3 = Utilities.PriceFormat(dataRow["price_to_set"].ToString());
										decimal.TryParse(dataRow["price"].ToString(), out priceForCal);
										decimal.TryParse(dataRow["matched_value"].ToString(), out matcedValue);
										orderTime = Utilities.GetTime(dataRow["order_time"].ToString());
										if (dataRow.Table.Columns.Contains("bitApproval"))
										{
											if (dataRow["bitApproval"].ToString().Trim() == "1" || dataRow["bitApproval"].ToString().Trim().ToUpper() == "TRUE")
											{
												approverId = "APP";
											}
											else
											{
												approverId = string.Empty;
											}
										}
										if (dataRow.Table.Columns.Contains("sSendDate"))
										{
											sendDate = dataRow["sSendDate"].ToString();
										}
										string str = string.Empty;
										if (ApplicationInfo.SupportFreewill && dataSet.Tables.Contains("TABREF"))
										{
											DataRow[] array = dataSet.Tables["TABREF"].Select("iFisOrdNo=" + dataRow["order_number"].ToString());
											if (array.Length > 0)
											{
												str = array[0]["iRefOrdNo"].ToString();
											}
										}
										string originalMessageType = "1I";
										if (ApplicationInfo.SupportFreewill)
										{
											string text4 = dataRow["status"].ToString().Trim();
											if (text4 != null)
											{
												if (text4 == "X" || text4 == "XA" || text4 == "XC" || text4 == "XAC" || text4 == "C")
												{
													originalMessageType = "2C";
												}
											}
										}
										string message = new OrderInfoClient().Pack("", "", "R" + str, orderNumber, dataRow["side"].ToString(), dataRow["stock"].ToString(), volume, text3, priceForCal, matchedVolume, matcedValue, num, ApplicationInfo.AccInfo.CurrentAccount, dataRow["trustee_id"].ToString(), "", dataRow["status"].ToString(), dataRow["quote"].ToString(), orderTime, "", 0m, originalMessageType, 0L, "", approverId, sendDate);
										IBroadcastMessage message2 = this._bcMessageFactory.CreateSETMessage(message);
										if (frmMain._OnMessageReceived != null)
										{
											frmMain._OnMessageReceived(message2, null);
										}
									}
								}
								if (ApplicationInfo.SupportFreewill)
								{
									if (dataSet.Tables.Contains("FAIL_ORD") && dataSet.Tables["FAIL_ORD"].Rows.Count > 0)
									{
										foreach (DataRow dataRow in dataSet.Tables["FAIL_ORD"].Rows)
										{
											string[] array2 = dataRow["item"].ToString().Split(new char[]
											{
												'#'
											});
											string message = new BroadCastOrderMessageClient().Pack(array2[5], "", "", "RF", "", string.Concat(new string[]
											{
												"Sending command fails, ",
												Utilities.GetOrderSideName(array2[1]),
												" ",
												array2[2],
												" at ",
												array2[4],
												"@ vol ",
												array2[3],
												", Account ",
												array2[5]
											}));
											IBroadcastMessage message2 = this._bcMessageFactory.CreateSETMessage(message);
											if (frmMain._OnMessageReceived != null)
											{
												frmMain._OnMessageReceived(message2, null);
											}
										}
									}
									if (dataSet.Tables.Contains("OFFLINE") && dataSet.Tables["OFFLINE"].Rows.Count > 0)
									{
										string text3 = string.Empty;
										string orderTime = string.Empty;
										string sendDate = string.Empty;
										decimal priceForCal = 0m;
										foreach (DataRow dataRow in dataSet.Tables["OFFLINE"].Rows)
										{
											long orderNumber;
											long.TryParse(dataRow["orderno"].ToString(), out orderNumber);
											long volume;
											long.TryParse(dataRow["volume"].ToString(), out volume);
											long num;
											long.TryParse(dataRow["pubvolume"].ToString(), out num);
											string text4 = dataRow["conditionprice"].ToString().Trim();
											if (text4 == null)
											{
												goto IL_924;
											}
											if (!(text4 == "A"))
											{
												if (!(text4 == "C"))
												{
													if (!(text4 == "M"))
													{
														if (!(text4 == "K"))
														{
															if (!(text4 == "L"))
															{
																goto IL_924;
															}
															text3 = "ML";
														}
														else
														{
															text3 = "MO";
														}
													}
													else
													{
														text3 = "MP";
													}
												}
												else
												{
													text3 = "ATC";
												}
											}
											else
											{
												text3 = "ATO";
											}
											IL_95F:
											orderTime = Utilities.GetTime(dataRow["ordertime"].ToString());
											sendDate = dataRow["orderdate"].ToString();
											string message = new OrderInfoClient().Pack("", "", "OFFLINE", orderNumber, dataRow["side"].ToString(), dataRow["secsymbol"].ToString(), volume, text3, priceForCal, 0L, 0m, num, ApplicationInfo.AccInfo.CurrentAccount, dataRow["trusteeid"].ToString(), "", dataRow["orderstatus"].ToString(), "", orderTime, "", 0m, "1I", 0L, "", "", sendDate);
											IBroadcastMessage message2 = this._bcMessageFactory.CreateSETMessage(message);
											if (frmMain._OnMessageReceived != null)
											{
												frmMain._OnMessageReceived(message2, null);
											}
											continue;
											IL_924:
											text3 = Utilities.PriceFormat(dataRow["price"].ToString().Trim());
											decimal.TryParse(dataRow["price"].ToString(), out priceForCal);
											goto IL_95F;
										}
									}
								}
								dataSet.Clear();
							}
						}
					}
					text = null;
				}
				if (ApplicationInfo.AutoGetOrderNoList_TFEX.Count > 0)
				{
					string text = string.Empty;
					foreach (KeyValuePair<string, DateTime> current in ApplicationInfo.AutoGetOrderNoList_TFEX)
					{
						if (current.Value.AddSeconds((double)ApplicationInfo.AutoGetOrderInfoInterval) < DateTime.Now)
						{
							text = text + "," + current.Key;
						}
					}
					if (text.Length > 0)
					{
						text = text.Substring(1);
					}
					if (!string.IsNullOrEmpty(text))
					{
						string text2 = ApplicationInfo.WebServiceTFEX.ViewOrderByOrderNo(ApplicationInfo.AccInfo.CurrentAccount, text);
						if (!string.IsNullOrEmpty(text2))
						{
							using (DataSet dataSet = new DataSet())
							{
								MyDataHelper.StringToDataSet(text2, dataSet);
								if (dataSet.Tables.Contains("ORDERS"))
								{
									long volume = 0L;
									long matchedVolume = 0L;
									long num = 0L;
									long orderNumber = 0L;
									string text3 = string.Empty;
									decimal priceForCal = 0m;
									string orderTime = string.Empty;
									string sendDate2 = string.Empty;
									for (int i = 0; i < dataSet.Tables["ORDERS"].Rows.Count; i++)
									{
										DataRow dataRow = dataSet.Tables["ORDERS"].Rows[i];
										long.TryParse(dataRow["order_number"].ToString(), out orderNumber);
										long.TryParse(dataRow["volume"].ToString(), out volume);
										long.TryParse(dataRow["matched_volume"].ToString(), out matchedVolume);
										long.TryParse(dataRow["pub_vol"].ToString(), out num);
										text3 = Utilities.PriceFormat(dataRow["price_to_set"].ToString());
										decimal.TryParse(dataRow["price"].ToString(), out priceForCal);
										orderTime = Utilities.GetTime(dataRow["order_time"].ToString());
										sendDate2 = dataRow["sSendDate"].ToString();
										string message = new OrderTFEXInfoClient().Pack("", ApplicationInfo.AccInfo.CurrentAccount, orderNumber.ToString(), dataRow["position"].ToString(), dataRow["side"].ToString(), dataRow["series"].ToString().Trim(), volume, text3, matchedVolume, num, dataRow["status"].ToString().Trim(), orderTime, dataRow["quote"].ToString().Trim(), sendDate2, dataRow["sOrdType"].ToString(), 0L, "", "");
										IBroadcastMessage message2 = this._bcMessageFactory.CreateTfexMessage(message);
										if (frmMain._OnMessageTfexReceived != null)
										{
											frmMain._OnMessageTfexReceived(message2, null);
										}
										ApplicationInfo.RemoveOrderNoFromAutoRefreshList_TFEX(orderNumber.ToString());
									}
								}
								dataSet.Clear();
							}
						}
					}
					text = null;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("timerGetOrderInfo_Tick", ex);
			}
			this.timerGetOrderInfo.Enabled = true;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnLogout_Click(object sender, EventArgs e)
		{
			this.Logout(false);
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
					if (this._systemOptionForm == null || this._systemOptionForm.IsDisposed)
					{
						this._systemOptionForm = new frmSystemOption();
					}
					this._systemOptionForm.TopLevel = false;
					this._systemOptionForm.Parent = this;
					if (base.Left < 0)
					{
						this._systemOptionForm.Left = base.Left + (base.Width - this._systemOptionForm.Width) / 2;
					}
					else
					{
						this._systemOptionForm.Left = (base.Width - this._systemOptionForm.Width) / 2;
					}
					if (base.Top < 0)
					{
						this._systemOptionForm.Top = base.Top + (base.Height - this._systemOptionForm.Height) / 2;
					}
					else
					{
						this._systemOptionForm.Top = (base.Height - this._systemOptionForm.Height) / 2;
					}
					this._systemOptionForm.Show();
					this._systemOptionForm.BringToFront();
				}
				catch (Exception ex)
				{
					this.ShowError("OpenSystemOptionForm", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnFontBold_Click(object sender, EventArgs e)
		{
			this.SetFont();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tslbLogo_Click(object sender, EventArgs e)
		{
			try
			{
				this.ExcuteFile("C:\\Program Files\\Internet Explorer\\iexplore.exe", "www.i2TradePlus.com");
			}
			catch (Exception ex)
			{
				this.ShowError("Open www.i2TradePlus.com Website", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool LoadAllInformation()
		{
			bool result;
			try
			{
				if (!ApplicationInfo.IsLoadInformation)
				{
					this.ShowSpashForm("Loading Information ...");
					ApplicationInfo.IndexStatInfo.ResetData();
					string data = ApplicationInfo.WebService.LoadAllInformation();
					using (DataSet dataSet = new DataSet())
					{
						MyDataHelper.StringToDataSet(data, dataSet);
						if (dataSet != null)
						{
							this.LoadStockInfomation(dataSet);
							this.LoadIndustrialInfomation(dataSet);
							this.LoadSectorInfomation(dataSet);
							this.LoadMarketInformation(dataSet);
							this.LoadDWInformation(dataSet);
							dataSet.Clear();
						}
					}
					if (ApplicationInfo.StopOrderSupported)
					{
						ApplicationInfo.StopOrderAccepted = (ApplicationInfo.WebAlertService.StopOrderCheckDisclaimer(ApplicationInfo.UserLoginID) == ApplicationInfo.UserLoginID);
					}
                    //<PS> Enable stop order feature
                    ApplicationInfo.StopOrderAccepted = true;

					data = string.Empty;
					if (ApplicationInfo.IsSupportTfex)
					{
						this.SectorBox.DisplayType = 3;
						this.SectorBox.Width = 430;
						this.ShowSpashForm("Loading TFEX Information ...");
						try
						{
							this.LoadTFEXInfomation();
							this.LoadTFEXMarketstate();
						}
						catch (Exception ex)
						{
							this.DisplayMessageBox("Load TFEX Information Error!!!\r\n" + ex.Message);
						}
						if (ApplicationInfo.MultiAutoComp == null)
						{
							ApplicationInfo.MultiAutoComp = new AutoCompleteStringCollection();
						}
						else
						{
							ApplicationInfo.MultiAutoComp.Clear();
						}
						if (ApplicationInfo.StockAutoComp == null)
						{
							ApplicationInfo.StockAutoComp = new AutoCompleteStringCollection();
						}
						else
						{
							ApplicationInfo.StockAutoComp.Clear();
						}
						if (ApplicationInfo.SeriesAutoComp == null)
						{
							ApplicationInfo.SeriesAutoComp = new AutoCompleteStringCollection();
						}
						else
						{
							ApplicationInfo.SeriesAutoComp.Clear();
						}
						foreach (KeyValuePair<int, StockList.StockInformation> current in ApplicationInfo.StockInfo.Items)
						{
							if (!current.Value.IsOddLot)
							{
								ApplicationInfo.MultiAutoComp.Add(current.Value.Symbol);
								ApplicationInfo.StockAutoComp.Add(current.Value.Symbol);
							}
						}
						ApplicationInfo.StockAutoCompStringArr.Initialize();
						foreach (KeyValuePair<int, SeriesList.SeriesInformation> current2 in ApplicationInfo.SeriesInfo.Items)
						{
							if (current2.Value.Group != 5)
							{
								ApplicationInfo.MultiAutoComp.Add(current2.Value.Symbol);
								ApplicationInfo.SeriesAutoComp.Add(current2.Value.Symbol);
							}
						}
					}
					else
					{
						this.SectorBox.DisplayType = 2;
						this.SectorBox.Width = 270;
						if (ApplicationInfo.StockAutoComp == null)
						{
							ApplicationInfo.StockAutoComp = new AutoCompleteStringCollection();
						}
						else
						{
							ApplicationInfo.StockAutoComp.Clear();
						}
						foreach (KeyValuePair<int, StockList.StockInformation> current in ApplicationInfo.StockInfo.Items)
						{
							if (!current.Value.IsOddLot)
							{
								ApplicationInfo.StockAutoComp.Add(current.Value.Symbol);
							}
						}
						ApplicationInfo.StockAutoCompStringArr.Initialize();
					}
					string text = string.Empty;
					text = ApplicationInfo.WebService.GetTunnel(ApplicationInfo.IsSupportTfex);
					if (text != string.Empty)
					{
						string[] array = text.Trim().Split(new char[]
						{
							'|'
						});
						int num;
						int.TryParse(array[1], out num);
						string[] array2 = array[0].Trim().Split(new char[]
						{
							';'
						});
						if (ApplicationInfo.TunnelHosts == null)
						{
							ApplicationInfo.TunnelHosts = new List<TunnelInfo>();
						}
						else
						{
							ApplicationInfo.TunnelHosts.Clear();
						}
						string[] array3 = array2;
						for (int i = 0; i < array3.Length; i++)
						{
							string text2 = array3[i];
							if (!string.IsNullOrEmpty(text2))
							{
								string[] array4 = text2.Trim().Split(new char[]
								{
									':'
								});
								TunnelInfo tunnelInfo = new TunnelInfo();
								tunnelInfo.HostIP = array4[0].Trim();
								tunnelInfo.Port = (string.IsNullOrEmpty(array4[1]) ? 27000 : Convert.ToInt32(array4[1].Trim()));
								tunnelInfo.EnableSSL = Convert.ToBoolean(array4[3]);
								tunnelInfo.IsAlreadyStart = false;
								ApplicationInfo.TunnelHosts.Add(tunnelInfo);
							}
						}
						int tunnelCounter = num % ApplicationInfo.TunnelHosts.Count;
						ApplicationInfo.TunnelCounter = tunnelCounter;
					}
					ApplicationInfo.UrlSyncHandler = ApplicationInfo.WebService.GetCometInfo();
					ApplicationInfo.IsLoadInformation = true;
				}
			}
			catch (Exception ex2)
			{
				this.ShowError("LoadAllInformation", ex2);
				this.DisplayMessageBox(ex2.ToString());
				result = false;
				return result;
			}
			this.ShowSpashForm("");
			result = true;
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void LoadStockInfomation(DataSet dsInvestor)
		{
			try
			{
				if (dsInvestor.Tables.Contains("StockInformation") && dsInvestor.Tables["StockInformation"].Rows.Count > 0)
				{
					ApplicationInfo.StockInfo.ResetData();
					foreach (DataRow dataRow in dsInvestor.Tables["StockInformation"].Rows)
					{
						StockList.StockInformation stockInformation = new StockList.StockInformation();
						int num;
						int.TryParse(dataRow["security_number"].ToString(), out num);
						stockInformation.Number = num;
						stockInformation.Symbol = dataRow["security_symbol"].ToString().Trim();
						decimal num2;
						decimal.TryParse(dataRow["ceiling"].ToString(), out num2);
						stockInformation.Ceiling = num2;
						decimal.TryParse(dataRow["floor"].ToString(), out num2);
						stockInformation.Floor = num2;
						decimal.TryParse(dataRow["prior_close_price"].ToString(), out num2);
						stockInformation.PriorPrice = num2;
						decimal.TryParse(dataRow["high_price"].ToString(), out num2);
						stockInformation.HighPrice = num2;
						decimal.TryParse(dataRow["low_price"].ToString(), out num2);
						stockInformation.LowPrice = num2;
						decimal.TryParse(dataRow["last_sale_price"].ToString(), out num2);
						stockInformation.LastSalePrice = num2;
						stockInformation.BidPrice1 = dataRow["bid_price1"].ToString();
						stockInformation.OfferPrice1 = dataRow["offer_price1"].ToString();
						int.TryParse(dataRow["sector_number"].ToString(), out num);
						stockInformation.SectorNumber = num;
						int.TryParse(dataRow["board_lot"].ToString(), out num);
						stockInformation.BoardLot = num;
						stockInformation.SecurityType = dataRow["security_type"].ToString();
						stockInformation.DisplayFlag = dataRow["display_flag"].ToString();
						stockInformation.MarketId = dataRow["market_id"].ToString();
						stockInformation.IsOddLot = (dataRow["sIsOdd"].ToString() == "Y");
						stockInformation.IsCheckSpread = (dataRow["check_spread"].ToString() == "Y");
						if (stockInformation.IsOddLot)
						{
							StockList.StockInformation expr_2AD = stockInformation;
							expr_2AD.Symbol += "_ODD";
						}
						ApplicationInfo.StockInfo.AddItem(stockInformation);
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void LoadIndustrialInfomation(DataSet dsInvestor)
		{
			try
			{
				if (dsInvestor.Tables.Contains("IndustryInformation") && dsInvestor.Tables["IndustryInformation"].Rows.Count > 0)
				{
					foreach (DataRow dataRow in dsInvestor.Tables["IndustryInformation"].Rows)
					{
						IndexStat.IndexItem indexItem = new IndexStat.IndexItem();
						int number;
						int.TryParse(dataRow["industry_number"].ToString(), out number);
						indexItem.Number = number;
						indexItem.Symbol = dataRow["industry_symbol"].ToString();
						indexItem.Fullname = dataRow["industry_name"].ToString();
						decimal prior;
						decimal.TryParse(dataRow["index_prior"].ToString(), out prior);
						indexItem.Prior = prior;
						indexItem.Type = "I";
						ApplicationInfo.IndexStatInfo.Items.Add(indexItem);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("LoadIndustrialInfomation", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void LoadSectorInfomation(DataSet dsInvestor)
		{
			try
			{
				if (dsInvestor.Tables.Contains("SectorInformation") && dsInvestor.Tables["SectorInformation"].Rows.Count > 0)
				{
					foreach (DataRow dataRow in dsInvestor.Tables["SectorInformation"].Rows)
					{
						IndexStat.IndexItem indexItem = new IndexStat.IndexItem();
						indexItem.Symbol = dataRow["sector_symbol"].ToString();
						indexItem.Fullname = dataRow["sector_name"].ToString();
						int number;
						int.TryParse(dataRow["sector_number"].ToString(), out number);
						indexItem.Number = number;
						decimal num;
						decimal.TryParse(dataRow["index_prior"].ToString(), out num);
						indexItem.Prior = num;
						decimal.TryParse(dataRow["index_value"].ToString(), out num);
						indexItem.LastIndex = num;
						decimal.TryParse(dataRow["accvalue"].ToString(), out num);
						indexItem.AccValue = num;
						decimal.TryParse(dataRow["index_high"].ToString(), out num);
						indexItem.IndexHigh = num;
						decimal.TryParse(dataRow["index_low"].ToString(), out num);
						indexItem.IndexLow = num;
						string symbol = indexItem.Symbol;
						if (symbol == null)
						{
							goto IL_1EE;
						}
						if (!(symbol == ".SET") && !(symbol == ".SET50") && !(symbol == ".SET100") && !(symbol == ".SETHD") && !(symbol == ".MAI"))
						{
							goto IL_1EE;
						}
						indexItem.Type = "E";
						IL_1FF:
						ApplicationInfo.IndexStatInfo.Items.Add(indexItem);
						continue;
						IL_1EE:
						indexItem.Type = "S";
						goto IL_1FF;
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void LoadMarketInformation(DataSet dsInvestor)
		{
			try
			{
				if (dsInvestor.Tables.Contains("market_status") && dsInvestor.Tables["market_status"].Rows.Count > 0)
				{
					ApplicationInfo.MarketState = dsInvestor.Tables["market_status"].Rows[0]["market_state"].ToString();
					int marketSession;
					int.TryParse(dsInvestor.Tables["market_status"].Rows[0]["market_session"].ToString(), out marketSession);
					ApplicationInfo.MarketSession = marketSession;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("LoadMarketInformation", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void LoadDWInformation(DataSet dsInvestor)
		{
			try
			{
				if (dsInvestor.Tables.Contains("DWInformation") && dsInvestor.Tables["DWInformation"].Rows.Count > 0)
				{
					foreach (DataRow dataRow in dsInvestor.Tables["DWInformation"].Rows)
					{
						if (!string.IsNullOrEmpty(dataRow["sParentStock"].ToString().Trim()))
						{
							ApplicationInfo.DWParentStockList.Add(dataRow["sParentStock"].ToString().Trim());
						}
					}
				}
				dsInvestor.Clear();
			}
			catch (Exception ex)
			{
				this.ShowError("LoadDWInformation", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void panelSep_MouseDown(object sender, MouseEventArgs e)
		{
			try
			{
				this._isPanelSepResize = true;
				if (this._formRS == null)
				{
					this._formRS = new Form();
					this._formRS.FormBorderStyle = FormBorderStyle.None;
					this._formRS.BackColor = Color.LightSteelBlue;
					this._formRS.Opacity = 0.7;
				}
				this._screenRectangle = base.RectangleToScreen(base.ClientRectangle);
				this._formRS.SetBounds(this._screenRectangle.Left, this._screenRectangle.Top + this.panBottom.Top, this.panBottom.Width, this.panBottom.Height);
				this._formRS.Show();
				this._formRS.BringToFront();
			}
			catch (Exception ex)
			{
				this.ShowError("panelSep_MouseDown", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void panelSep_MouseMove(object sender, MouseEventArgs e)
		{
			try
			{
				if (this._formRS != null)
				{
					this._formRS.SetBounds(this._screenRectangle.Left, this._screenRectangle.Top + this.panelSep.Top + e.Y, this.panBottom.Width, this.panBottom.Height - e.Y);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("panelSep_MouseMove", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void panelSep_MouseUp(object sender, MouseEventArgs e)
		{
			try
			{
				if (this._isPanelSepResize)
				{
					if (this._formRS != null)
					{
						this._formRS.Hide();
					}
					this._isPanelSepResize = false;
					if (e.Y != 0)
					{
						this._BTop = this.panelSep.Top + e.Y;
						this.Instance_SetBottomSize();
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("panelSep_MouseUp", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnOptions_Click(object sender, EventArgs e)
		{
			this.OpenSystemOptionForm();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnAdjFont_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.panelFontAdjust.Visible)
				{
					this.panelFontAdjust.Hide();
				}
				else
				{
					this._isFontLock = true;
					this.cbFontSize.Text = Settings.Default.Default_Font.Size.ToString();
					this.cbDefaultFontName.Text = Settings.Default.Default_Font.Name;
					this.chbBold.Checked = Settings.Default.Default_Font.Bold;
					this.panelFontAdjust.Top = this.panelControlDockR.Top + this.panelControlDockR.Height + 1;
					this.panelFontAdjust.Left = this.panelControlDockR.Left;
					this.panelFontAdjust.Show();
					this.panelFontAdjust.BringToFront();
					this._isFontLock = false;
				}
				this.Instance_SetBottomSize();
			}
			catch (Exception ex)
			{
				this.ShowError("btnAdjFont_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnLogout_Click(object sender, EventArgs e)
		{
			this.Logout(false);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void panelFontAdjust_Paint(object sender, PaintEventArgs e)
		{
			if (e.ClipRectangle.Width == this.panelFontAdjust.Width)
			{
				e.Graphics.DrawRectangle(new Pen(Brushes.Red, 2f), e.ClipRectangle);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetTopPanelActive(bool isStart)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new frmMain.SetTopPanelActiveCallBack(this.SetTopPanelActive), new object[]
				{
					isStart
				});
			}
			else
			{
				try
				{
					this.MarketStateBox.IsAllowBlinkAlert = isStart;
					this.MarketStateBox.IsAlertStart = true;
					if (isStart)
					{
						ApplicationInfo.MarketTime = DateTime.Now;
					}
					this.SETBox.Active = isStart;
					this.SET2Box.Active = isStart;
					this.SectorBox.Active = isStart;
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void panBottom_Paint(object sender, PaintEventArgs e)
		{
			if (e.ClipRectangle.Left == 0 && e.ClipRectangle.Width == this.panBottom.Width)
			{
				e.Graphics.DrawRectangle(Pens.LightGray, e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbDefaultFontName_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.SetFont();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnFontDone_Click(object sender, EventArgs e)
		{
			this.panelFontAdjust.Hide();
			this.Instance_SetBottomSize();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbFontSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.SetFont();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnShowSlide_Click(object sender, EventArgs e)
		{
			try
			{
				this._slideType = Convert.ToInt32((sender as Button).Tag);
				this.btnShowTickerSlide.ForeColor = Color.LightGray;
				this.btnShowSpeedOrderSlide.ForeColor = Color.LightGray;
				(sender as Button).ForeColor = Color.Cyan;
				this.Instance_SetBottomSize();
				if (this._slideType == 1)
				{
					this.TickerSlideBox.Init();
				}
				else
				{
					if (this._slideType == 2)
					{
						this.Smart1ClickBox.Init();
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("btnShowSlide_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void panelControlDockR_Paint(object sender, PaintEventArgs e)
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetMenu(string templateName)
		{
			try
			{
				foreach (Control control in this.panelMenu.Controls)
				{
					if (control.GetType() == typeof(Button))
					{
						control.ForeColor = Color.LightGray;
					}
				}
				Button button = null;
				switch (templateName)
				{
				case "Market Watch":
					button = this.btnMarketWatch;
					break;
				case "Top BBO":
					button = this.btnTopBBOs;
					break;
				case "Stock Summary":
					button = this.btnSummary;
					break;
				case "Market Information":
					button = this.btnMarketInfo;
					break;
				case "Stock Ranking":
					button = this.btnRanking;
					break;
				case "View Order":
					button = this.btnViewOrder;
					break;
				case "Portfolio":
					button = this.btnPortfolio;
					break;
				case "Historical Chart":
					button = this.btnStockHistory;
					break;
				case "Batch Order":
					button = this.btnBatchOrder;
					break;
				case "e-Service":
					button = this.btnEservice;
					break;
				}
				if (button != null)
				{
					button.ForeColor = Color.GreenYellow;
				}
				if (ApplicationInfo.IsNewPortStyle)
				{
					if (templateName.ToLower() == "portfolio")
					{
						this.panelDockR.Visible = false;
						this.btnShowTickerSlide.Visible = false;
						this.btnShowSpeedOrderSlide.Visible = false;
					}
					else
					{
						this.panelDockR.Visible = true;
						this.btnShowTickerSlide.Visible = true;
						this.btnShowSpeedOrderSlide.Visible = true;
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SetMenu", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void Menus_Click(object sender, EventArgs e)
		{
			try
			{
				foreach (Control control in this.panelMenu.Controls)
				{
					if (control.GetType() == typeof(Button))
					{
						control.ForeColor = Color.LightGray;
					}
				}
				Button button = sender as Button;
				if (button == this.btnMarketWatch)
				{
					this.OpenTemplate("Market Watch");
				}
				else
				{
					if (button == this.btnTopBBOs)
					{
						this.OpenTemplate("Top BBO");
					}
					else
					{
						if (button == this.btnSummary)
						{
							this.OpenTemplate("Stock Summary");
						}
						else
						{
							if (button == this.btnMarketInfo)
							{
								this.OpenTemplate("Market Information");
							}
							else
							{
								if (button == this.btnRanking)
								{
									this.OpenTemplate("Stock Ranking");
								}
								else
								{
									if (button == this.btnNews)
									{
										this.OpenTemplate("News Center");
									}
									else
									{
										if (button == this.btnViewOrder)
										{
											this.OpenTemplate("View Order");
										}
										else
										{
											if (button == this.btnPortfolio)
											{
												this.OpenTemplate("Portfolio");
											}
											else
											{
												if (button == this.btnStockHistory)
												{
													this.OpenTemplate("Historical Chart");
												}
												else
												{
													if (button == this.btnBatchOrder)
													{
														this.OpenTemplate("Batch Order");
													}
													else
													{
														if (button == this.btnEservice)
														{
															this.OpenTemplate("e-Service");
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
				}
				button.ForeColor = Color.GreenYellow;
			}
			catch (Exception ex)
			{
				this.ShowError("Menus_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ipwIPPortMain_OnConnected(object sender, IpportsConnectedEventArgs e)
		{
			try
			{
				if (e.StatusCode == 0)
				{
					this.ipwIPPortMain.EOL = "\r\n";
				}
				else
				{
					this.ShowError("Socket.Connected", new Exception(e.StatusCode + ":" + e.Description));
				}
			}
			catch (Exception ex)
			{
				this.ShowError("OnConnected", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ipwIPPortMain_OnReadyToSend(object sender, IpportsReadyToSendEventArgs e)
		{
			try
			{
				Thread thread = new Thread(new ThreadStart(this.RegistrationToTunnel));
				thread.Start();
			}
			catch (Exception ex)
			{
				this.ShowError("OnReadyToSend", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ipwIPPortMain_OnDataIn(object sender, IpportsDataInEventArgs e)
		{
			object syncRoot;
			Monitor.Enter(syncRoot = ((ICollection)this._qMessage).SyncRoot);
			try
			{
				this._qMessage.Enqueue(e.Text);
			}
			finally
			{
				Monitor.Exit(syncRoot);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void PeekQ()
		{
			int i = 0;
			string text = string.Empty;
			this._isPeekQWorking = true;
			while (this._isRunPeekQ)
			{
				try
				{
					object syncRoot;
					Monitor.Enter(syncRoot = ((ICollection)this._qMessage).SyncRoot);
					try
					{
						i = this._qMessage.Count;
					}
					finally
					{
						Monitor.Exit(syncRoot);
					}
					if (i > 20)
					{
						i = 20;
					}
					while (i > 0)
					{
						Monitor.Enter(syncRoot = ((ICollection)this._qMessage).SyncRoot);
						try
						{
							text = this._qMessage.Dequeue();
						}
						finally
						{
							Monitor.Exit(syncRoot);
						}
						i--;
						string[] array = text.Split(new char[]
						{
							Convert.ToChar(3)
						});
						string[] array2 = array;
						for (int j = 0; j < array2.Length; j++)
						{
							string message = array2[j];
							this.OnDataIn(message);
						}
						if (!this._isRunPeekQ)
						{
							break;
						}
					}
				}
				catch
				{
				}
				Thread.Sleep(20);
			}
			if (this._qMessage != null)
			{
				this._qMessage.Clear();
			}
			this._isPeekQWorking = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ipwIPPortMain_OnDisconnected(object sender, IpportsDisconnectedEventArgs e)
		{
			this.ShowConnectionStatus();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ipwIPPortMain_OnError(object sender, IpportsErrorEventArgs e)
		{
			this.ShowError("Socket.Error", new Exception(e.Description));
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ipwIPPortMain_OnSSLServerAuthentication(object sender, IpportsSSLServerAuthenticationEventArgs e)
		{
			e.Accept = true;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SendEchoToServer()
		{
			try
			{
				this.ipwIPPortMain.DataToSend = new Echo().Pack(ApplicationInfo.UserLoginID, ApplicationInfo.UserSessionID) + this.ipwIPPortMain.EOL;
			}
			catch (IPWorksSSLIpportsException ex)
			{
				this.ShowError("SendEcho", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void timerMonitorFeed_Tick(object sender, EventArgs e)
		{
			try
			{
				if (ApplicationInfo.IsAreadyLogin)
				{
					if (ApplicationInfo.IsPushMode)
					{
						if (this.ipwIPPortMain.Connected)
						{
							this._isPushConnected = true;
							if (this._lastEcho.AddSeconds(12.0) >= DateTime.Now)
							{
								this.SendEchoToServer();
							}
							else
							{
								this.ShowError("", new Exception("Echo timeout!"));
								this.startBC();
							}
						}
						else
						{
							this.ShowError("", new Exception("Socket not connected!"));
							this.GetTunnelNode();
							this.startBC();
						}
					}
					else
					{
						if (!this.feedsWebProxy.CanConnectServer)
						{
							this._connectionPullCounter++;
							if (this._connectionPullCounter >= 2)
							{
								this._connectionPullCounter = 0;
								this.startBC();
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("timerCheckECForBcTunnel_Tick", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void GetTunnelNode()
		{
			if (this._isPushConnected)
			{
				ApplicationInfo.TunnelCounter++;
				if (ApplicationInfo.TunnelCounter > ApplicationInfo.TunnelHosts.Count - 1)
				{
					ApplicationInfo.TunnelCounter = 0;
				}
			}
			else
			{
				bool isPushMode = false;
				for (int i = 0; i < ApplicationInfo.TunnelHosts.Count; i++)
				{
					ApplicationInfo.TunnelCounter++;
					if (ApplicationInfo.TunnelCounter > ApplicationInfo.TunnelHosts.Count - 1)
					{
						ApplicationInfo.TunnelCounter = 0;
					}
					if (!ApplicationInfo.TunnelHosts[ApplicationInfo.TunnelCounter].IsAlreadyStart)
					{
						isPushMode = true;
						break;
					}
				}
				ApplicationInfo.IsPushMode = isPushMode;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void RegistrationToTunnel()
		{
			try
			{
				if (ApplicationInfo.UserLoginMode != "I")
				{
					if (this.ipwIPPortMain.Connected)
					{
						if (this._qMessage == null)
						{
							this._qMessage = new Queue<string>();
						}
						else
						{
							this._qMessage.Clear();
						}
						this._isRunPeekQ = true;
						Thread thread = new Thread(new ThreadStart(this.PeekQ));
						thread.Start();
						string text = string.Empty;
						if (ApplicationInfo.BrokerId == 4)
						{
							using (Dictionary<string, AccountInfo.ItemInfo>.Enumerator enumerator = ApplicationInfo.AccInfo.Items.GetEnumerator())
							{
								if (enumerator.MoveNext())
								{
									KeyValuePair<string, AccountInfo.ItemInfo> current = enumerator.Current;
									text = current.Key;
								}
							}
						}
						else
						{
							foreach (KeyValuePair<string, AccountInfo.ItemInfo> current in ApplicationInfo.AccInfo.Items)
							{
								text = text + "|" + current.Key;
							}
							if (text != string.Empty)
							{
								text = text.Substring(1);
							}
						}
						string str = new Register().Pack(ApplicationInfo.UserLoginID, ApplicationInfo.UserSessionID, 1, text);
						this.ipwIPPortMain.DataToSend = str + this.ipwIPPortMain.EOL;
						text = null;
						this.ShowConnectionStatus();
						this.CloseSpashForm();
					}
					else
					{
						this.ShowError("RegistrationToTunnel", new Exception("Can't register to server , Socket not connect"));
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("RegistrationToTunnel", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UnregistrationFromTunnel()
		{
			try
			{
				if (ApplicationInfo.UserLoginMode != "I" && this.ipwIPPortMain.Connected)
				{
					string str = new Unregister().Pack(ApplicationInfo.UserLoginID, ApplicationInfo.UserSessionID, 1, "");
					this.ipwIPPortMain.DataToSend = str + this.ipwIPPortMain.EOL;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("UnregistrationFromTunnel", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Rectangle GetWorkingArea(ClientBaseForm form)
		{
			Rectangle result;
			if (form.GetType() == typeof(frmPortfolio) && ApplicationInfo.IsNewPortStyle)
			{
				result = new Rectangle(1, this.panelTop.Bottom + this.panelMenu.Height + 2, base.ClientSize.Width - 3, this._BTop - (this.panelTop.Top + this.panelTop.Height) - 1 - this.panelMenu.Height - 1);
			}
			else
			{
				result = new Rectangle(1, this.panelTop.Bottom + this.panelMenu.Height + 2, base.ClientSize.Width - this.panelDockR.Width - 3, this._BTop - this.panelTop.Bottom - 1 - this.panelMenu.Height - 1);
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetHeaderToChild()
		{
			this.ViewOrderBox.SetHeader();
			this.TickerSlideBox.SetHeaderColor(true);
			this.Smart1ClickBox.SetHeaderColor(true);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetSmartStock(object sender, SymbolLinkSource source, string newStock)
		{
			if (this.Smart1ClickBox.Visible)
			{
				this.Smart1ClickBox.SetSmartStock(sender, source, newStock);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetTemplateLink(string page, string subPage, string stock)
		{
			try
			{
				ApplicationInfo.CurrentSymbol = stock;
				TemplateManager.Instance.MainForm.OpenTemplate(page, subPage);
				foreach (Control control in this.panelMenu.Controls)
				{
					if (control.GetType() == typeof(Button))
					{
						control.ForeColor = Color.LightGray;
					}
				}
				Button button = null;
				if (page == "Stock Summary")
				{
					button = this.btnSummary;
				}
				else
				{
					if (page == "News Center")
					{
						button = this.btnNews;
					}
					else
					{
						if (page == "Historical Chart")
						{
							button = this.btnStockHistory;
						}
						else
						{
							if (page == "Market Watch")
							{
								button = this.btnMarketWatch;
							}
						}
					}
				}
				if (button != null)
				{
					button.ForeColor = Color.GreenYellow;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SetTemplateLink", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void MarketStateBox_OnCallAlert()
		{
			this.OpenAlertForm();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void MarketStateBox_OnSitchMode()
		{
			this.startBC();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SendOrderBox_OnAccountChanged(string account)
		{
			try
			{
				this.ViewOrderBox.ReloadData();
				this.BroadcastMessageBox.ReloadData();
				TemplateManager.Instance.SendSymbolLink(this, SymbolLinkSource.SwitchAccount, "");
			}
			catch (Exception ex)
			{
				this.ShowError("SendOrderBox_OnAccountChanged", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SendOrderBox_OnBoxStyleChanged()
		{
			this.Instance_SetBottomSize();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SendOrderBox_OnResized()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SendOrderBox_OnResizeUpDown(bool isUp)
		{
			int bTop = this._BTop;
			if (isUp)
			{
				this._BTop -= (int)((float)base.Height * 0.08f);
			}
			else
			{
				this._BTop += (int)((float)base.Height * 0.08f);
			}
			if (bTop != this._BTop)
			{
				this.Instance_SetBottomSize();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SendOrderBox_OnNewStopOrder(DataRow dr)
		{
			if (TemplateManager.Instance.CurrentActiveTemplateView.FormObj is frmViewOrder)
			{
				((frmViewOrder)TemplateManager.Instance.CurrentActiveTemplateView.FormObj).OnNewStopOrder(dr);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateAutoComplete(string symbol)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new frmMain.UpdateAutoCompleteCallBack(this.UpdateAutoComplete), new object[]
				{
					symbol
				});
			}
			else
			{
				try
				{
					ApplicationInfo.MultiAutoComp.Add(symbol);
					ApplicationInfo.MultiAutoCompStringArr = null;
					ApplicationInfo.MultiAutoCompStringArr.Initialize();
					ApplicationInfo.SeriesAutoComp.Add(symbol);
					ApplicationInfo.SeriesAutoCompStringArr.Initialize();
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void LoadTFEXInfomation()
		{
			try
			{
				string text = ApplicationInfo.WebServiceTFEX.TFEXInformation();
				if (!string.IsNullOrEmpty(text))
				{
					using (DataSet dataSet = new DataSet())
					{
						MyDataHelper.StringToDataSet(text, dataSet);
						if (dataSet.Tables.Contains("Info") && dataSet.Tables["Info"].Rows.Count > 0)
						{
							ApplicationInfo.SeriesInfo.ResetData();
							foreach (DataRow dataRow in dataSet.Tables["Info"].Rows)
							{
								SeriesList.SeriesInformation seriesInformation = new SeriesList.SeriesInformation();
								seriesInformation.Symbol = dataRow["sSeries"].ToString();
								int num;
								int.TryParse(dataRow["lOrderBookId"].ToString(), out num);
								seriesInformation.OrderBookId = num;
								int.TryParse(dataRow["lUndOrderBookId"].ToString(), out num);
								seriesInformation.UnderOrderBookId = num;
								decimal num2;
								decimal.TryParse(dataRow["nmrCeiling"].ToString(), out num2);
								seriesInformation.Ceiling = num2;
								decimal.TryParse(dataRow["nmrFloor"].ToString(), out num2);
								seriesInformation.Floor = num2;
								decimal.TryParse(dataRow["nmrFixPrice"].ToString(), out num2);
								seriesInformation.FixPrice = num2;
								decimal.TryParse(dataRow["nmrPrevFixPrice"].ToString(), out num2);
								seriesInformation.PrevFixPrice = num2;
								int.TryParse(dataRow["iMarketNo"].ToString(), out num);
								seriesInformation.MarketCode = num;
								int.TryParse(dataRow["iSymbolTypeNo"].ToString(), out num);
								seriesInformation.Group = num;
								decimal.TryParse(dataRow["nmrContractSize"].ToString(), out num2);
								seriesInformation.ContractSize = num2;
								seriesInformation.ExpireDate = dataRow["sExpDate"].ToString();
								int.TryParse(dataRow["iOpenBalance"].ToString(), out num);
								seriesInformation.OpenInt = num;
								decimal.TryParse(dataRow["nmrStrike"].ToString(), out num2);
								seriesInformation.StrikPrice = num2;
								decimal.TryParse(dataRow["nmrPrice"].ToString(), out num2);
								seriesInformation.LastSalePrice = num2;
								decimal.TryParse(dataRow["tickSize"].ToString(), out num2);
								seriesInformation.TickSize = num2;
								seriesInformation.SeriesType = dataRow["seriesType"].ToString();
								int numOfDec = 0;
								int.TryParse(dataRow["iDecPrice"].ToString(), out numOfDec);
								seriesInformation.NumOfDec = numOfDec;
								seriesInformation.MarketId = dataRow["sMarketId"].ToString().Trim();
								ApplicationInfo.SeriesInfo.AddItem(seriesInformation);
							}
						}
						if (dataSet.Tables.Contains("MarketInfo") && dataSet.Tables["MarketInfo"].Rows.Count > 0)
						{
							foreach (DataRow dataRow in dataSet.Tables["MarketInfo"].Rows)
							{
								long num3;
								long.TryParse(dataRow["TotVolume"].ToString(), out num3);
								long num4;
								long.TryParse(dataRow["TotDeal"].ToString(), out num4);
								long num5;
								long.TryParse(dataRow["oi"].ToString(), out num5);
								if (dataRow["iSymbolTypeNo"].ToString() == "1")
								{
									ApplicationInfo.IndexInfoTfex.OptionsVol += num3;
									ApplicationInfo.IndexInfoTfex.OptionsOI += num5;
									ApplicationInfo.IndexInfoTfex.TfexTotalDeal += num4;
								}
								else
								{
									if (dataRow["iSymbolTypeNo"].ToString() == "2")
									{
										ApplicationInfo.IndexInfoTfex.OptionsVol += num3;
										ApplicationInfo.IndexInfoTfex.OptionsOI += num5;
										ApplicationInfo.IndexInfoTfex.TfexTotalDeal += num4;
									}
									else
									{
										ApplicationInfo.IndexInfoTfex.FutureVol = num3;
										ApplicationInfo.IndexInfoTfex.FutureOI = num5;
										ApplicationInfo.IndexInfoTfex.TfexTotalDeal += num4;
									}
								}
							}
							ApplicationInfo.IndexInfoTfex.TfexTotalVol = ApplicationInfo.IndexInfoTfex.FutureVol + ApplicationInfo.IndexInfoTfex.OptionsVol;
							ApplicationInfo.IndexInfoTfex.TfexTotalOI = ApplicationInfo.IndexInfoTfex.FutureOI + ApplicationInfo.IndexInfoTfex.OptionsOI;
						}
						if (dataSet.Tables.Contains("UnderlyingInfo") && dataSet.Tables["UnderlyingInfo"].Rows.Count > 0)
						{
							ApplicationInfo.UnderlyingInfo.ResetData();
							foreach (DataRow dataRow in dataSet.Tables["UnderlyingInfo"].Rows)
							{
								UnderlyingInfo.UnderlyingList underlyingList = new UnderlyingInfo.UnderlyingList();
								int num;
								int.TryParse(dataRow["lOrderBookId"].ToString(), out num);
								underlyingList.OrderBookId = num;
								underlyingList.Symbol = dataRow["sSymbol"].ToString().Trim();
								ApplicationInfo.UnderlyingInfo.AddItem(underlyingList);
							}
						}
						if (ApplicationInfo.BrokerId == 4 && ApplicationInfo.IsSupportTfex)
						{
							BBOTFEXCurrency bBOTFEXCurrency = new BBOTFEXCurrency();
							bBOTFEXCurrency.CurrencyName = "USD";
							bBOTFEXCurrency.BidPrice = 0m;
							bBOTFEXCurrency.AskPrice = 0m;
							bBOTFEXCurrency.LastTime = "00:00:00";
							bBOTFEXCurrency.LastDate = "01 January 2000";
							ApplicationInfo.IndexInfoTfex.BBOCurrency.Add(bBOTFEXCurrency.CurrencyName, bBOTFEXCurrency);
							bBOTFEXCurrency = new BBOTFEXCurrency();
							bBOTFEXCurrency.CurrencyName = "THB";
							bBOTFEXCurrency.BidPrice = 0m;
							bBOTFEXCurrency.AskPrice = 0m;
							bBOTFEXCurrency.LastTime = "00:00:00";
							bBOTFEXCurrency.LastDate = "01 January 2000";
							ApplicationInfo.IndexInfoTfex.BBOCurrency.Add(bBOTFEXCurrency.CurrencyName, bBOTFEXCurrency);
						}
						dataSet.Clear();
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void LoadTFEXMarketstate()
		{
			try
			{
				string text = ApplicationInfo.WebServiceTFEX.LoadMktStatus();
				if (!string.IsNullOrEmpty(text))
				{
					using (DataSet dataSet = new DataSet())
					{
						MyDataHelper.StringToDataSet(text, dataSet);
						if (dataSet != null && dataSet.Tables.Contains("Table") && dataSet.Tables["Table"].Rows.Count > 0)
						{
							foreach (DataRow dataRow in dataSet.Tables["Table"].Rows)
							{
								if (dataRow["Market"].ToString().Trim() == "TXI")
								{
									ApplicationInfo.IndexInfoTfex.TXIState = dataRow["sStateNumber"].ToString().Trim();
									int num;
									int.TryParse(dataRow["Session"].ToString(), out num);
									ApplicationInfo.IndexInfoTfex.TXISession = num;
								}
								else
								{
									if (dataRow["Market"].ToString().Trim() == "TXM")
									{
										ApplicationInfo.IndexInfoTfex.TXMState = dataRow["sStateNumber"].ToString();
										int num;
										int.TryParse(dataRow["Session"].ToString(), out num);
										ApplicationInfo.IndexInfoTfex.TXMSession = num;
									}
									else
									{
										if (dataRow["Market"].ToString().Trim() == "TXR")
										{
											ApplicationInfo.IndexInfoTfex.TXRState = dataRow["sStateNumber"].ToString();
											int num;
											int.TryParse(dataRow["Session"].ToString(), out num);
											ApplicationInfo.IndexInfoTfex.TXRSession = num;
										}
										else
										{
											if (dataRow["Market"].ToString().Trim() == "TXS")
											{
												ApplicationInfo.IndexInfoTfex.TXSState = dataRow["sStateNumber"].ToString();
												int num;
												int.TryParse(dataRow["Session"].ToString(), out num);
												ApplicationInfo.IndexInfoTfex.TXSSession = num;
											}
											else
											{
												if (dataRow["Market"].ToString().Trim() == "TXE")
												{
													ApplicationInfo.IndexInfoTfex.TXEState = dataRow["sStateNumber"].ToString();
													int num;
													int.TryParse(dataRow["Session"].ToString(), out num);
													ApplicationInfo.IndexInfoTfex.TXESession = num;
												}
												else
												{
													if (dataRow["Market"].ToString().Trim() == "TXC")
													{
														ApplicationInfo.IndexInfoTfex.TXCState = dataRow["sStateNumber"].ToString();
														int num;
														int.TryParse(dataRow["Session"].ToString(), out num);
														ApplicationInfo.IndexInfoTfex.TXCSession = num;
													}
												}
											}
										}
									}
								}
							}
						}
						dataSet.Clear();
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetFont()
		{
			try
			{
				if (!this._isFontLock)
				{
					Settings.Default.Default_Font = new Font(this.cbDefaultFontName.Text, Convert.ToSingle(this.cbFontSize.Text), this.chbBold.Checked ? FontStyle.Bold : FontStyle.Regular);
					this.Instance_SetBottomSize();
					this.TickerSlideBox.SetResize();
					this.Smart1ClickBox.SetResize();
					if (TemplateManager.Instance.CurrentActiveTemplateView != null)
					{
						TemplateManager.Instance.CurrentActiveTemplateView.SetFont();
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SetFont", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnFacebook_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start("http://www.facebook.com/i2trade");
			}
			catch (Exception ex)
			{
				this.ShowError("btnFacebook_Click", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void timer1_Tick(object sender, EventArgs e)
		{
			try
			{
				this.timer1.Stop();
				ApplicationInfo.IsResumeState = true;
				this.OpenTemplate(TemplateManager.Instance.CurrentActiveTemplateView.Name);
				ApplicationInfo.IsResumeState = false;
				this.startBC();
			}
			catch (Exception ex)
			{
				this.ShowError("timer1_Tick", ex);
			}
		}
	}
}
