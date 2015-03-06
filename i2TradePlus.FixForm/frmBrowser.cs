using i2TradePlus.Information;
using ITSNet.Common.BIZ;
using ITSNet.Controls.XtWebBrowser;
using ITSNet.Controls.XtWebBrowser.UserActivityMonitor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace i2TradePlus.FixForm
{
	public class frmBrowser : ClientBaseForm, IRealtimeMessage
	{
		private const int USE_NONE = 0;
		private const int USE_ALT = 1;
		private const int USE_CTRL = 2;
		private const int USE_SHIFT = 4;
		private const int USE_WIN = 8;
		private short mHotKeyId = 0;
		private Timer tmLoad = null;
		private string _url = string.Empty;
		private bool _isNews = false;
		private XtBrowser xtbNews;
		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmBrowser()
		{
			this.InitializeComponent();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmBrowser(Dictionary<string, object> propertiesValue) : base(propertiesValue)
		{
			this.InitializeComponent();
		}
		[DllImport("user32.dll")]
		private static extern int FindWindow(string cls, string wndwText);
		[DllImport("user32.dll")]
		private static extern int ShowWindow(int hwnd, int cmd);
		[DllImport("user32.dll")]
		private static extern long SHAppBarMessage(long dword, int cmd);
		[DllImport("user32.dll")]
		private static extern int RegisterHotKey(IntPtr hwnd, int id, int fsModifiers, int vk);
		[DllImport("user32.dll")]
		private static extern int UnregisterHotKey(IntPtr hwnd, int id);
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void RegisterGlobalHotKey(Keys hotkey, int modifiers)
		{
			try
			{
				this.mHotKeyId += 1;
				if (this.mHotKeyId > 0)
				{
					if (frmBrowser.RegisterHotKey(base.Handle, (int)this.mHotKeyId, modifiers, (int)Convert.ToInt16(hotkey)) == 0)
					{
						MessageBox.Show("Error: " + this.mHotKeyId.ToString() + " - " + Marshal.GetLastWin32Error().ToString(), "Hot Key Registration");
					}
				}
			}
			catch
			{
				this.UnregisterGlobalHotKey();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UnregisterGlobalHotKey()
		{
			for (int i = 1; i <= (int)this.mHotKeyId; i++)
			{
				frmBrowser.UnregisterHotKey(base.Handle, i);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
			if (m.Msg == 786)
			{
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void RegisterGlobalHotKey()
		{
			this.RegisterGlobalHotKey(Keys.F5, 0);
			this.RegisterGlobalHotKey(Keys.N, 2);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmi2Browser_Activated(object sender, EventArgs e)
		{
			this.RegisterGlobalHotKey();
			HookManager.KeyDown -= new KeyEventHandler(this.HookManager_KeyDown);
			HookManager.KeyDown += new KeyEventHandler(this.HookManager_KeyDown);
			HookManager.KeyUp -= new KeyEventHandler(this.HookManager_KeyUp);
			HookManager.KeyUp += new KeyEventHandler(this.HookManager_KeyUp);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmi2Browser_Deactivate(object sender, EventArgs e)
		{
			this.UnregisterGlobalHotKey();
			HookManager.KeyDown -= new KeyEventHandler(this.HookManager_KeyDown);
			HookManager.KeyUp -= new KeyEventHandler(this.HookManager_KeyUp);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReceiveMessage(IBroadcastMessage message, StockList.StockInformation realtimeStockInfo)
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OpenBrowser(string url)
		{
			if (this.tmLoad == null)
			{
				this.tmLoad = new Timer();
				this.tmLoad.Interval = 100;
				this.tmLoad.Tick += new EventHandler(this.tmLoad_Tick);
			}
			this._url = url;
			this.tmLoad.Stop();
			this.tmLoad.Start();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tmLoad_Tick(object sender, EventArgs e)
		{
			this.tmLoad.Stop();
			this.xtbNews.Navigate(this._url);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmi2Browser_IDoShownDelay()
		{
			try
			{
				if (this.FormState == ClientBaseForm.ClientBaseFormState.Closing)
				{
					return;
				}
				this.SetResize(true);
				base.Show();
				base.OpenedForm();
			}
			catch (Exception ex)
			{
				this.ShowError("frmi2Browser_IDoShownDelay", ex);
			}
			base.IsLoadingData = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmi2Browser_IDoLoadData()
		{
			try
			{
				this.CallSymbolLink(true, ApplicationInfo.NewsSymbol);
			}
			catch (Exception ex)
			{
				this.ShowError("frmi2Browser_IDoLoadData", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			if (base.Visible)
			{
				this.RegisterGlobalHotKey();
				HookManager.KeyDown -= new KeyEventHandler(this.HookManager_KeyDown);
				HookManager.KeyUp -= new KeyEventHandler(this.HookManager_KeyUp);
				HookManager.KeyDown += new KeyEventHandler(this.HookManager_KeyDown);
				HookManager.KeyUp += new KeyEventHandler(this.HookManager_KeyUp);
				(base.Parent as Form).Activated -= new EventHandler(this.frmi2Browser_Activated);
				(base.Parent as Form).Activated += new EventHandler(this.frmi2Browser_Activated);
				(base.Parent as Form).Deactivate -= new EventHandler(this.frmi2Browser_Deactivate);
				(base.Parent as Form).Deactivate += new EventHandler(this.frmi2Browser_Deactivate);
			}
			else
			{
				this.UnregisterGlobalHotKey();
				HookManager.KeyDown -= new KeyEventHandler(this.HookManager_KeyDown);
				HookManager.KeyUp -= new KeyEventHandler(this.HookManager_KeyUp);
				(base.Parent as Form).Activated -= new EventHandler(this.frmi2Browser_Activated);
				(base.Parent as Form).Deactivate -= new EventHandler(this.frmi2Browser_Deactivate);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void HookManager_KeyDown(object sender, KeyEventArgs e)
		{
			(base.Parent as frmMain).TrigKeyDown(this, e);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void HookManager_KeyUp(object sender, KeyEventArgs e)
		{
			(base.Parent as frmMain).TrigKeyUp(this, e);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmi2Browser_IDoReActivated()
		{
			if (!base.IsLoadingData)
			{
				this.SetResize(this.IsWidthChanged);
				base.Show();
				if (ApplicationInfo.NewsSymbol != string.Empty)
				{
					this.CallSymbolLink(this._isNews, ApplicationInfo.NewsSymbol);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void CallSymbolLink(bool isNews, string symbol)
		{
			try
			{
				this._isNews = isNews;
				if (this._isNews)
				{
					if (symbol == string.Empty || symbol.ToLower() == "symbol")
					{
						this.OpenBrowser(ApplicationInfo.NewsHomeLink);
					}
					else
					{
						symbol = symbol.Replace("&", "%26");
						this.OpenBrowser(ApplicationInfo.NewsSymbolLink.Replace("XXXXX", symbol));
						ApplicationInfo.NewsSymbol = string.Empty;
					}
				}
				else
				{
					if (symbol == string.Empty || symbol.ToLower() == "symbol")
					{
						this.OpenBrowser(ApplicationInfo.StockFocusHomeLink);
					}
					else
					{
						symbol = symbol.Replace("&", "%26");
						this.OpenBrowser(ApplicationInfo.StockFocusSymbolLink.Replace("XXXXX", symbol));
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("CallSymbolLink", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmi2Browser_IDoCustomSizeChanged()
		{
			if (!base.IsLoadingData)
			{
				this.SetResize(this.IsWidthChanged);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmi2Browser_IDoFontChanged()
		{
			if (!base.IsLoadingData)
			{
				base.SuspendLayout();
				this.SetResize(true);
				base.ResumeLayout();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetResize(bool isChanged)
		{
			try
			{
			}
			catch (Exception ex)
			{
				this.ShowError("SetResize", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReceiveTfexMessage(IBroadcastMessage message, SeriesList.SeriesInformation realtimeSeriesInfo)
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void xtbNews_SearchStockClicked(bool isNews, string symbol)
		{
			this.CallSymbolLink(isNews, symbol);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void InitializeComponent()
		{
			this.xtbNews = new XtBrowser();
			base.SuspendLayout();
			this.xtbNews.Dock = DockStyle.Fill;
			this.xtbNews.Location = new Point(0, 0);
			this.xtbNews.Name = "xtbNews";
			this.xtbNews.Size = new Size(740, 544);
			this.xtbNews.TabIndex = 0;
			this.xtbNews.Url = null;
			this.xtbNews.SearchStockClicked += new XtBrowser.SearchStockClickEventCallBack(this.xtbNews_SearchStockClicked);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(740, 544);
			base.Controls.Add(this.xtbNews);
			base.Name = "frmBrowser";
			this.Text = "frmi2Browser";
			base.IDoShownDelay += new ClientBaseForm.OnShownDelayEventHandler(this.frmi2Browser_IDoShownDelay);
			base.IDoLoadData += new ClientBaseForm.OnIDoLoadDataEventHandler(this.frmi2Browser_IDoLoadData);
			base.IDoFontChanged += new ClientBaseForm.OnFontChangedEventHandler(this.frmi2Browser_IDoFontChanged);
			base.IDoCustomSizeChanged += new ClientBaseForm.CustomSizeChangedEventHandler(this.frmi2Browser_IDoCustomSizeChanged);
			base.IDoReActivated += new ClientBaseForm.OnReActiveEventHandler(this.frmi2Browser_IDoReActivated);
			base.Controls.SetChildIndex(this.xtbNews, 0);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
