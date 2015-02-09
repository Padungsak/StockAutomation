using ITSNet.Controls.XtWebBrowser.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Windows.Forms;
namespace ITSNet.Controls.XtWebBrowser
{
	[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
	public class XtBrowser : UserControl
	{
		public delegate void ActivatedEventHandler();
		public delegate void ItemChangedEventHandler(string Url, string Title);
		public delegate void TitleDocChangedEventHandler(string Str);
		public delegate void NewWindowEventHandler(string Url, string UrlContext);
		public delegate void ObjClickEventHandler();
		public delegate void WantToCloseEventHandler();
		public delegate void SendErrorMsgEventHandler(string SubName, string ErrorMsg);
		public delegate void UnRegisSpacebarEventHandler();
		public delegate void RegisSpacebarEventHandler();
		public delegate void SearchStockClickEventCallBack(bool isNews, string symbol);
		private string m_Url;
		private string Watermark = "SYMBOL";
		private IContainer components;
		private ToolStripButton stopButton;
		internal ToolStripSeparator ToolStripSeparator5;
		private ToolStripButton tsbtnNews;
		private StatusStrip statusStrip1;
		private ToolStripStatusLabel toolStripStatusLabel1;
		private ToolStripButton printButton;
		internal ToolStripSeparator ToolStripSeparator8;
		private ToolStripButton forwardButton;
		private ToolStripButton backButton;
		internal ToolStripSeparator ToolStripSeparator3;
		private ToolStripButton goButton;
		public ToolStripComboBox ToolStripComboBox1;
		private ToolStrip tsTopMenu;
		public XtWebBrowser webBrowser1;
		private ToolStripTextBox tbSearchStock;
		private ToolStripButton searchButton;
		internal ToolStripSeparator toolStripSeparator1;
		private ToolStripButton stockFocusButton;
		private ToolStripSeparator toolStripSeparator2;
		private ToolStripLabel toolStripLabel1;
		private ToolStripButton tsbtnClear;
		public event XtBrowser.ActivatedEventHandler Activated
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.Activated = (XtBrowser.ActivatedEventHandler)Delegate.Combine(this.Activated, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.Activated = (XtBrowser.ActivatedEventHandler)Delegate.Remove(this.Activated, value);
			}
		}
		public event XtBrowser.ItemChangedEventHandler ItemChanged
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.ItemChanged = (XtBrowser.ItemChangedEventHandler)Delegate.Combine(this.ItemChanged, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.ItemChanged = (XtBrowser.ItemChangedEventHandler)Delegate.Remove(this.ItemChanged, value);
			}
		}
		public event XtBrowser.TitleDocChangedEventHandler TitleDocChanged
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.TitleDocChanged = (XtBrowser.TitleDocChangedEventHandler)Delegate.Combine(this.TitleDocChanged, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.TitleDocChanged = (XtBrowser.TitleDocChangedEventHandler)Delegate.Remove(this.TitleDocChanged, value);
			}
		}
		public event XtBrowser.ObjClickEventHandler ObjClick
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.ObjClick = (XtBrowser.ObjClickEventHandler)Delegate.Combine(this.ObjClick, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.ObjClick = (XtBrowser.ObjClickEventHandler)Delegate.Remove(this.ObjClick, value);
			}
		}
		public event XtBrowser.WantToCloseEventHandler WantToClose
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.WantToClose = (XtBrowser.WantToCloseEventHandler)Delegate.Combine(this.WantToClose, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.WantToClose = (XtBrowser.WantToCloseEventHandler)Delegate.Remove(this.WantToClose, value);
			}
		}
		public event XtBrowser.SendErrorMsgEventHandler SendErrorMsg
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.SendErrorMsg = (XtBrowser.SendErrorMsgEventHandler)Delegate.Combine(this.SendErrorMsg, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.SendErrorMsg = (XtBrowser.SendErrorMsgEventHandler)Delegate.Remove(this.SendErrorMsg, value);
			}
		}
		public event XtBrowser.UnRegisSpacebarEventHandler UnRegisSpacebar
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.UnRegisSpacebar = (XtBrowser.UnRegisSpacebarEventHandler)Delegate.Combine(this.UnRegisSpacebar, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.UnRegisSpacebar = (XtBrowser.UnRegisSpacebarEventHandler)Delegate.Remove(this.UnRegisSpacebar, value);
			}
		}
		public event XtBrowser.RegisSpacebarEventHandler RegisSpacebar
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.RegisSpacebar = (XtBrowser.RegisSpacebarEventHandler)Delegate.Combine(this.RegisSpacebar, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.RegisSpacebar = (XtBrowser.RegisSpacebarEventHandler)Delegate.Remove(this.RegisSpacebar, value);
			}
		}
		public event XtBrowser.SearchStockClickEventCallBack SearchStockClicked
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.SearchStockClicked = (XtBrowser.SearchStockClickEventCallBack)Delegate.Combine(this.SearchStockClicked, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.SearchStockClicked = (XtBrowser.SearchStockClickEventCallBack)Delegate.Remove(this.SearchStockClicked, value);
			}
		}
		public ToolStrip ToolStripBrowser
		{
			get
			{
				return this.tsTopMenu;
			}
		}
		public StatusStrip StatusStripBrowser
		{
			get
			{
				return this.statusStrip1;
			}
		}
		public string Url
		{
			get
			{
				return this.m_Url;
			}
			set
			{
				if (this.m_Url != value)
				{
					this.m_Url = value;
				}
			}
		}
		public XtBrowser()
		{
			this.InitializeComponent();
		}
		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.webBrowser1.ShowSaveAsDialog();
		}
		private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.webBrowser1.ShowPageSetupDialog();
		}
		private void printToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.webBrowser1.ShowPrintDialog();
		}
		private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.webBrowser1.ShowPrintPreviewDialog();
		}
		private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.webBrowser1.ShowPropertiesDialog();
		}
		private void ToolStripComboBox1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.Navigate(this.ToolStripComboBox1.Text);
			}
		}
		private void goButton_Click(object sender, EventArgs e)
		{
			this.Navigate(this.ToolStripComboBox1.Text);
		}
		public void Navigate(string address)
		{
			try
			{
				if (string.IsNullOrEmpty(address))
				{
					return;
				}
				if (address.Equals("about:blank"))
				{
					this.webBrowser1.Navigate(address);
					this.ToolStripComboBox1.Text = address;
					return;
				}
				if (address.Equals("http://www.i2trade.com/NewPortalPage.aspx"))
				{
					this.webBrowser1.Navigate(address);
					this.ToolStripComboBox1.Text = "i2Trade News Center";
					return;
				}
				if (address.Substring(1, 2) == ":\\")
				{
					this.ToolStripBrowser.Visible = false;
				}
				if (!(address.Substring(1, 2) == ":\\") && !address.StartsWith("http://") && !address.StartsWith("https://"))
				{
					address = "http://" + address;
				}
			}
			catch (Exception ex)
			{
				if (this.SendErrorMsg != null)
				{
					this.SendErrorMsg("Navigate 1 ", ex.Message);
				}
			}
			try
			{
				this.webBrowser1.ScriptErrorsSuppressed = false;
				this.webBrowser1.Navigate(address);
				this.ToolStripComboBox1.Text = address;
				this.Url = address;
				if (!this.ToolStripComboBox1.Items.Contains(address))
				{
					this.ToolStripComboBox1.Items.Add(address);
				}
				if (!this.ToolStripComboBox1.AutoCompleteCustomSource.Contains(address))
				{
					this.ToolStripComboBox1.AutoCompleteCustomSource.Add(address);
				}
			}
			catch (UriFormatException ex2)
			{
				if (this.SendErrorMsg != null)
				{
					this.SendErrorMsg("Navigate 2 Browser ", ex2.Message);
				}
			}
		}
		private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			try
			{
				if (this.ItemChanged != null)
				{
					this.ItemChanged(this.Url, this.webBrowser1.Document.Title);
				}
				this.webBrowser1.ScriptErrorsSuppressed = true;
			}
			catch (UriFormatException ex)
			{
				if (this.SendErrorMsg != null)
				{
					this.SendErrorMsg("webBrowser1_DocumentCompleted 2 Browser ", ex.Message);
				}
			}
		}
		public void AddEventClick()
		{
			try
			{
				this.webBrowser1.Document.Click += new HtmlElementEventHandler(this.DocumentClicked);
				this.webBrowser1.Document.Body.Click += new HtmlElementEventHandler(this.DocumentClicked);
				this.webBrowser1.Document.Body.MouseUp += new HtmlElementEventHandler(this.DocumentClicked);
				this.webBrowser1.Document.Body.MouseDown += new HtmlElementEventHandler(this.DocumentClicked);
				this.webBrowser1.Document.Body.GotFocus += new HtmlElementEventHandler(this.DocumentClicked);
				this.webBrowser1.Document.Body.Focusing += new HtmlElementEventHandler(this.DocumentClicked);
				this.webBrowser1.Document.ContextMenuShowing += new HtmlElementEventHandler(this.DocumentClicked);
			}
			catch (Exception ex)
			{
				if (this.SendErrorMsg != null)
				{
					this.SendErrorMsg("AddEventClick ", ex.Message);
				}
			}
		}
		private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
		{
			try
			{
				if (this.webBrowser1.Url.AbsoluteUri.Equals("http://www.i2trade.com/NewPortalPage.aspx"))
				{
					this.ToolStripComboBox1.Text = "i2Trade News Center";
				}
				else
				{
					this.ToolStripComboBox1.Text = this.webBrowser1.Url.AbsoluteUri;
				}
				((WebBrowser)sender).Document.Window.Error += new HtmlElementErrorEventHandler(this.Script_Error);
			}
			catch (UriFormatException ex)
			{
				if (this.SendErrorMsg != null)
				{
					this.SendErrorMsg("webBrowser1_Navigated 1 Smart Browser ", ex.Message);
				}
			}
		}
		private void backButton_Click(object sender, EventArgs e)
		{
			this.webBrowser1.GoBack();
		}
		private void webBrowser1_CanGoBackChanged(object sender, EventArgs e)
		{
			this.backButton.Enabled = this.webBrowser1.CanGoBack;
		}
		private void forwardButton_Click(object sender, EventArgs e)
		{
			try
			{
				this.webBrowser1.GoForward();
			}
			catch (UriFormatException ex)
			{
				if (this.SendErrorMsg != null)
				{
					this.SendErrorMsg("forwardButton_Click  Smart Browser ", ex.Message);
				}
			}
		}
		private void webBrowser1_CanGoForwardChanged(object sender, EventArgs e)
		{
			try
			{
				this.forwardButton.Enabled = this.webBrowser1.CanGoForward;
			}
			catch (UriFormatException ex)
			{
				if (this.SendErrorMsg != null)
				{
					this.SendErrorMsg("webBrowser1_CanGoForwardChanged  Smart Browser ", ex.Message);
				}
			}
		}
		private void stopButton_Click(object sender, EventArgs e)
		{
			try
			{
				this.webBrowser1.Stop();
			}
			catch (UriFormatException ex)
			{
				if (this.SendErrorMsg != null)
				{
					this.SendErrorMsg("stopButton_Click  Smart Browser ", ex.Message);
				}
			}
		}
		private void refreshButton_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this.webBrowser1.Url.Equals("about:blank"))
				{
					this.webBrowser1.Refresh();
				}
			}
			catch (Exception ex)
			{
				if (this.SendErrorMsg != null)
				{
					this.SendErrorMsg("refreshButton_Click", ex.Message);
				}
			}
		}
		private void tsbtnNews_Click(object sender, EventArgs e)
		{
			this.SearchStockInfo(true);
			this.tsbtnNews.Checked = true;
			this.stockFocusButton.Checked = false;
		}
		private void printButton_Click(object sender, EventArgs e)
		{
			PrintDialog printDialog = new PrintDialog();
			if (printDialog.ShowDialog() == DialogResult.OK)
			{
				this.webBrowser1.Print();
			}
		}
		private void webBrowser1_StatusTextChanged(object sender, EventArgs e)
		{
			this.toolStripStatusLabel1.Text = this.webBrowser1.StatusText;
		}
		private void webBrowser1_DocumentTitleChanged(object sender, EventArgs e)
		{
			if (this.TitleDocChanged != null)
			{
				this.TitleDocChanged(this.webBrowser1.DocumentTitle);
			}
		}
		private void webBrowser1_NewWindowExtended(object sender, XtBrowserNewWindowEventArgs e)
		{
		}
		private void statusStrip1_ItemClicked(object sender, EventArgs e)
		{
			if (this.ObjClick != null)
			{
				this.ObjClick();
			}
		}
		private void DocumentClicked(object sender, HtmlElementEventArgs e)
		{
			if (this.Activated != null)
			{
				this.Activated();
			}
		}
		public void OnspaceBar()
		{
			try
			{
				this.ToolStripComboBox1.Focus();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		private void ResizeComboBoxUrl()
		{
			try
			{
				int num = 300;
				int num2 = this.tsTopMenu.Width - this.goButton.Width - this.backButton.Width - this.forwardButton.Width - this.stopButton.Width - this.tsbtnNews.Width - this.printButton.Width - 80;
				if (num2 < num)
				{
					num2 = num;
				}
				this.ToolStripComboBox1.Width = num2;
			}
			catch (Exception ex)
			{
				if (this.SendErrorMsg != null)
				{
					this.SendErrorMsg("Sub ResizeComboBoxUrl", ex.Message);
				}
			}
		}
		private void webBrowser1_WBWantsToClose()
		{
			if (this.WantToClose != null)
			{
				this.WantToClose();
			}
		}
		private void Script_Error(object sender, HtmlElementErrorEventArgs e)
		{
			e.Handled = true;
		}
		private void webBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
		{
			((WebBrowser)sender).Document.Window.Error += new HtmlElementErrorEventHandler(this.Script_Error);
			this.webBrowser1.StatusTextChanged += new EventHandler(this.webBrowser1_StatusTextChanged);
			this.webBrowser1.CanGoBackChanged += new EventHandler(this.webBrowser1_CanGoBackChanged);
			this.webBrowser1.CanGoForwardChanged += new EventHandler(this.webBrowser1_CanGoForwardChanged);
		}
		private void ToolStripComboBox1_GotFocus(object sender, EventArgs e)
		{
			if (this.UnRegisSpacebar != null)
			{
				this.UnRegisSpacebar();
			}
		}
		private void ToolStripComboBox1_LostFocus(object sender, EventArgs e)
		{
			if (this.RegisSpacebar != null)
			{
				this.RegisSpacebar();
			}
		}
		private void webBrowser1_LostFocus(object sender, EventArgs e)
		{
			if (this.UnRegisSpacebar != null)
			{
				this.UnRegisSpacebar();
			}
		}
		private void webBrowser1_GotFocus(object sender, EventArgs e)
		{
			if (this.RegisSpacebar != null)
			{
				this.RegisSpacebar();
			}
		}
		private void XtBrowser_Resize(object sender, EventArgs e)
		{
			this.ResizeComboBoxUrl();
		}
		private void stockInfoButton_Click(object sender, EventArgs e)
		{
			this.SearchStockInfo(false);
			this.tsbtnNews.Checked = false;
			this.stockFocusButton.Checked = true;
		}
		private void searchButton_Click(object sender, EventArgs e)
		{
			this.SearchStockInfo(this.tsbtnNews.Checked);
		}
		private void SearchStockInfo(bool isNews)
		{
			if (this.SearchStockClicked != null)
			{
				this.SearchStockClicked(isNews, this.tbSearchStock.Text.Trim());
			}
		}
		private void tbSearchStock_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.SearchStockInfo(this.tsbtnNews.Checked);
			}
		}
		private void tbSearchStock_Click(object sender, EventArgs e)
		{
			if (this.tbSearchStock.Text.Equals(this.Watermark))
			{
				this.tbSearchStock.Clear();
				this.tbSearchStock.ForeColor = Color.Black;
			}
		}
		private void tbSearchStock_Leave(object sender, EventArgs e)
		{
			if (this.tbSearchStock.Text.Length.Equals(0))
			{
				this.tbSearchStock.Text = this.Watermark;
				this.tbSearchStock.ForeColor = Color.LightGray;
			}
		}
		private void tsbtnClear_Click(object sender, EventArgs e)
		{
			this.tbSearchStock.Text = string.Empty;
			this.SearchStockInfo(this.tsbtnNews.Checked);
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}
		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(XtBrowser));
			this.ToolStripSeparator5 = new ToolStripSeparator();
			this.statusStrip1 = new StatusStrip();
			this.toolStripStatusLabel1 = new ToolStripStatusLabel();
			this.ToolStripSeparator8 = new ToolStripSeparator();
			this.ToolStripSeparator3 = new ToolStripSeparator();
			this.ToolStripComboBox1 = new ToolStripComboBox();
			this.tsTopMenu = new ToolStrip();
			this.tsbtnNews = new ToolStripButton();
			this.stockFocusButton = new ToolStripButton();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.toolStripLabel1 = new ToolStripLabel();
			this.tbSearchStock = new ToolStripTextBox();
			this.searchButton = new ToolStripButton();
			this.tsbtnClear = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.printButton = new ToolStripButton();
			this.backButton = new ToolStripButton();
			this.forwardButton = new ToolStripButton();
			this.goButton = new ToolStripButton();
			this.stopButton = new ToolStripButton();
			this.webBrowser1 = new XtWebBrowser();
			this.statusStrip1.SuspendLayout();
			this.tsTopMenu.SuspendLayout();
			base.SuspendLayout();
			this.ToolStripSeparator5.Name = "ToolStripSeparator5";
			this.ToolStripSeparator5.Size = new Size(6, 25);
			this.ToolStripSeparator5.Visible = false;
			this.statusStrip1.AutoSize = false;
			this.statusStrip1.BackColor = SystemColors.Control;
			this.statusStrip1.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.statusStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripStatusLabel1
			});
			this.statusStrip1.Location = new Point(0, 519);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new Size(1325, 20);
			this.statusStrip1.TabIndex = 5;
			this.statusStrip1.Click += new EventHandler(this.statusStrip1_ItemClicked);
			this.toolStripStatusLabel1.BackColor = Color.Transparent;
			this.toolStripStatusLabel1.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.toolStripStatusLabel1.ForeColor = SystemColors.ControlText;
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new Size(0, 15);
			this.toolStripStatusLabel1.TextAlign = ContentAlignment.TopLeft;
			this.ToolStripSeparator8.Name = "ToolStripSeparator8";
			this.ToolStripSeparator8.Size = new Size(6, 25);
			this.ToolStripSeparator3.Name = "ToolStripSeparator3";
			this.ToolStripSeparator3.Size = new Size(6, 25);
			this.ToolStripSeparator3.Visible = false;
			this.ToolStripComboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			this.ToolStripComboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.ToolStripComboBox1.AutoSize = false;
			this.ToolStripComboBox1.BackColor = Color.WhiteSmoke;
			this.ToolStripComboBox1.FlatStyle = FlatStyle.Flat;
			this.ToolStripComboBox1.Name = "ToolStripComboBox1";
			this.ToolStripComboBox1.Size = new Size(420, 23);
			this.ToolStripComboBox1.Text = "about:blank";
			this.ToolStripComboBox1.Visible = false;
			this.ToolStripComboBox1.KeyDown += new KeyEventHandler(this.ToolStripComboBox1_KeyDown);
			this.ToolStripComboBox1.Click += new EventHandler(this.statusStrip1_ItemClicked);
			this.tsTopMenu.Items.AddRange(new ToolStripItem[]
			{
				this.tsbtnNews,
				this.ToolStripSeparator8,
				this.stockFocusButton,
				this.toolStripSeparator2,
				this.toolStripLabel1,
				this.tbSearchStock,
				this.searchButton,
				this.tsbtnClear,
				this.toolStripSeparator1,
				this.printButton,
				this.backButton,
				this.forwardButton,
				this.ToolStripSeparator3,
				this.ToolStripComboBox1,
				this.ToolStripSeparator5,
				this.goButton,
				this.stopButton
			});
			this.tsTopMenu.Location = new Point(0, 0);
			this.tsTopMenu.Name = "tsTopMenu";
			this.tsTopMenu.RenderMode = ToolStripRenderMode.System;
			this.tsTopMenu.Size = new Size(1325, 25);
			this.tsTopMenu.TabIndex = 4;
			this.tsTopMenu.Click += new EventHandler(this.statusStrip1_ItemClicked);
			this.tsbtnNews.Checked = true;
			this.tsbtnNews.CheckState = CheckState.Checked;
			this.tsbtnNews.Image = Resources.news;
			this.tsbtnNews.Name = "tsbtnNews";
			this.tsbtnNews.Size = new Size(45, 22);
			this.tsbtnNews.Text = "ข่าว";
			this.tsbtnNews.Click += new EventHandler(this.tsbtnNews_Click);
			this.stockFocusButton.Image = Resources.Information2;
			this.stockFocusButton.Name = "stockFocusButton";
			this.stockFocusButton.Size = new Size(150, 22);
			this.stockFocusButton.Text = "ข้อมูลรายบริษัท/หลักทรัพย์";
			this.stockFocusButton.Click += new EventHandler(this.stockInfoButton_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new Size(6, 25);
			this.toolStripLabel1.Margin = new Padding(0, 1, 3, 2);
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new Size(34, 22);
			this.toolStripLabel1.Text = "ค้นหา";
			this.tbSearchStock.BackColor = SystemColors.Info;
			this.tbSearchStock.CharacterCasing = CharacterCasing.Upper;
			this.tbSearchStock.ForeColor = Color.LightGray;
			this.tbSearchStock.Name = "tbSearchStock";
			this.tbSearchStock.Size = new Size(100, 25);
			this.tbSearchStock.Text = "SYMBOL";
			this.tbSearchStock.Leave += new EventHandler(this.tbSearchStock_Leave);
			this.tbSearchStock.KeyDown += new KeyEventHandler(this.tbSearchStock_KeyDown);
			this.tbSearchStock.Click += new EventHandler(this.tbSearchStock_Click);
			this.searchButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.searchButton.Image = Resources.forward;
			this.searchButton.ImageTransparentColor = Color.Magenta;
			this.searchButton.Margin = new Padding(3, 1, 3, 2);
			this.searchButton.Name = "searchButton";
			this.searchButton.Size = new Size(23, 22);
			this.searchButton.Text = "toolStripButton1";
			this.searchButton.ToolTipText = "ค้นหา";
			this.searchButton.Click += new EventHandler(this.searchButton_Click);
			this.tsbtnClear.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnClear.Image = (Image)componentResourceManager.GetObject("tsbtnClear.Image");
			this.tsbtnClear.ImageTransparentColor = Color.Magenta;
			this.tsbtnClear.Name = "tsbtnClear";
			this.tsbtnClear.Size = new Size(38, 22);
			this.tsbtnClear.Text = "Clear";
			this.tsbtnClear.Click += new EventHandler(this.tsbtnClear_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 25);
			this.printButton.Image = Resources.Print;
			this.printButton.Name = "printButton";
			this.printButton.Size = new Size(52, 22);
			this.printButton.Text = "Print";
			this.printButton.Click += new EventHandler(this.printButton_Click);
			this.backButton.Enabled = false;
			this.backButton.Image = Resources.GoRtlHS;
			this.backButton.Name = "backButton";
			this.backButton.Size = new Size(23, 22);
			this.backButton.Visible = false;
			this.backButton.Click += new EventHandler(this.backButton_Click);
			this.forwardButton.Enabled = false;
			this.forwardButton.Image = Resources.GoLtrHS;
			this.forwardButton.Name = "forwardButton";
			this.forwardButton.Size = new Size(23, 22);
			this.forwardButton.TextImageRelation = TextImageRelation.TextBeforeImage;
			this.forwardButton.Visible = false;
			this.forwardButton.Click += new EventHandler(this.forwardButton_Click);
			this.goButton.Image = Resources.forward;
			this.goButton.Name = "goButton";
			this.goButton.Size = new Size(23, 22);
			this.goButton.Visible = false;
			this.goButton.Click += new EventHandler(this.goButton_Click);
			this.stopButton.Image = Resources.StopControl;
			this.stopButton.Name = "stopButton";
			this.stopButton.Size = new Size(23, 22);
			this.stopButton.Visible = false;
			this.stopButton.Click += new EventHandler(this.stopButton_Click);
			this.webBrowser1.CausesValidation = false;
			this.webBrowser1.Dock = DockStyle.Fill;
			this.webBrowser1.Location = new Point(0, 25);
			this.webBrowser1.MinimumSize = new Size(20, 20);
			this.webBrowser1.Name = "webBrowser1";
			this.webBrowser1.Size = new Size(1325, 494);
			this.webBrowser1.TabIndex = 6;
			this.webBrowser1.ProgressChanged += new WebBrowserProgressChangedEventHandler(this.webBrowser1_ProgressChanged);
			this.webBrowser1.WBWantsToClose += new XtWebBrowser.WBWantsToCloseEventHandler(this.webBrowser1_WBWantsToClose);
			this.webBrowser1.NewWindowExtended += new XtWebBrowser.WebBrowserNewWindowExtendedEventHandler(this.webBrowser1_NewWindowExtended);
			this.webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
			this.webBrowser1.Navigated += new WebBrowserNavigatedEventHandler(this.webBrowser1_Navigated);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.webBrowser1);
			base.Controls.Add(this.statusStrip1);
			base.Controls.Add(this.tsTopMenu);
			base.Name = "XtBrowser";
			base.Size = new Size(1325, 539);
			base.Resize += new EventHandler(this.XtBrowser_Resize);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.tsTopMenu.ResumeLayout(false);
			this.tsTopMenu.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
