using System;
using System.Runtime.InteropServices;
namespace ITSNet.Controls.XtWebBrowser
{
	public class XtWebBrowserEvents : StandardOleMarshalObject, DWebBrowserEvents2
	{
		private XtWebBrowser m_Browser;
		public XtWebBrowserEvents(XtWebBrowser browser)
		{
			this.m_Browser = browser;
		}
		public void BeforeNavigate2(object pDisp, ref string URL, ref object flags, ref string targetFrameName, ref object postData, ref string headers, ref bool cancel)
		{
			this.m_Browser.OnNavigatingExtended(URL, targetFrameName, (byte[])postData, headers, ref cancel);
		}
		public void NewWindow3(object pDisp, ref bool Cancel, ref object Flags, ref string UrlContext, ref string Url)
		{
			this.m_Browser.OnNewWindowExtended(Url, ref Cancel, (NewWindowMF)Flags, UrlContext);
		}
	}
}
