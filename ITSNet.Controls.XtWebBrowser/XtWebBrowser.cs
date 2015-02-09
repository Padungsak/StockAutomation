using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace ITSNet.Controls.XtWebBrowser
{
	public class XtWebBrowser : WebBrowser
	{
		public delegate void WebBrowserNavigatingExtendedEventHandler(object sender, XtBrowserNavigatorEventArgs e);
		public delegate void WebBrowserNewWindowExtendedEventHandler(object sender, XtBrowserNewWindowEventArgs e);
		public delegate void WBWantsToCloseEventHandler();
		private const int WM_PARENTNOTIFY = 528;
		private AxHost.ConnectionPointCookie cookie;
		private XtWebBrowserEvents wevents;
		private IntPtr WM_DESTROY = new IntPtr(2);
		public event XtWebBrowser.WebBrowserNavigatingExtendedEventHandler NavigatingExtended
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.NavigatingExtended = (XtWebBrowser.WebBrowserNavigatingExtendedEventHandler)Delegate.Combine(this.NavigatingExtended, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.NavigatingExtended = (XtWebBrowser.WebBrowserNavigatingExtendedEventHandler)Delegate.Remove(this.NavigatingExtended, value);
			}
		}
		public event XtWebBrowser.WebBrowserNewWindowExtendedEventHandler NewWindowExtended
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.NewWindowExtended = (XtWebBrowser.WebBrowserNewWindowExtendedEventHandler)Delegate.Combine(this.NewWindowExtended, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.NewWindowExtended = (XtWebBrowser.WebBrowserNewWindowExtendedEventHandler)Delegate.Remove(this.NewWindowExtended, value);
			}
		}
		public event XtWebBrowser.WBWantsToCloseEventHandler WBWantsToClose
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.WBWantsToClose = (XtWebBrowser.WBWantsToCloseEventHandler)Delegate.Combine(this.WBWantsToClose, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.WBWantsToClose = (XtWebBrowser.WBWantsToCloseEventHandler)Delegate.Remove(this.WBWantsToClose, value);
			}
		}
		protected override void CreateSink()
		{
			base.CreateSink();
			this.wevents = new XtWebBrowserEvents(this);
			this.cookie = new AxHost.ConnectionPointCookie(base.ActiveXInstance, this.wevents, typeof(DWebBrowserEvents2));
		}
		protected override void DetachSink()
		{
			if (this.cookie != null)
			{
				this.cookie.Disconnect();
				this.cookie = null;
			}
			base.DetachSink();
		}
		protected internal void OnNavigatingExtended(string Url, string Frame, byte[] Postdata, string Headers, ref bool Cancel)
		{
			XtBrowserNavigatorEventArgs xtBrowserNavigatorEventArgs = new XtBrowserNavigatorEventArgs(Url, Frame, Postdata, Headers);
			if (this.NavigatingExtended != null)
			{
				this.NavigatingExtended(this, xtBrowserNavigatorEventArgs);
			}
			Cancel = xtBrowserNavigatorEventArgs.Cancel;
		}
		protected internal void OnNewWindowExtended(string Url, ref bool Cancel, NewWindowMF Flags, string UrlContext)
		{
			XtBrowserNewWindowEventArgs xtBrowserNewWindowEventArgs = new XtBrowserNewWindowEventArgs(Url, UrlContext, Flags);
			if (Flags == (NewWindowMF)262148)
			{
				Cancel = true;
				return;
			}
			if (this.NewWindowExtended != null)
			{
				this.NewWindowExtended(this, xtBrowserNewWindowEventArgs);
			}
			Cancel = xtBrowserNewWindowEventArgs.Cancel;
		}
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg == 528)
			{
				if (!base.DesignMode && m.WParam == this.WM_DESTROY && this.WBWantsToClose != null)
				{
					this.WBWantsToClose();
				}
				this.DefWndProc(ref m);
				return;
			}
			base.WndProc(ref m);
		}
	}
}
