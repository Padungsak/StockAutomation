using System;
using System.ComponentModel;
namespace ITSNet.Controls.XtWebBrowser
{
	public class XtBrowserNewWindowEventArgs : CancelEventArgs
	{
		private string m_Url;
		private string m_UrlContext;
		private NewWindowMF m_Flags;
		public string Url
		{
			get
			{
				return this.m_Url;
			}
		}
		public string UrlContext
		{
			get
			{
				return this.m_UrlContext;
			}
		}
		public NewWindowMF Flags
		{
			get
			{
				return this.m_Flags;
			}
		}
		public XtBrowserNewWindowEventArgs(string url, string urlcontext, NewWindowMF flags)
		{
			this.m_Url = url;
			this.m_UrlContext = urlcontext;
			this.m_Flags = flags;
		}
	}
}
