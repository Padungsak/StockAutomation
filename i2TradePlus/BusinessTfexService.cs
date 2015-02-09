using i2TradePlus.ITSNetBusinessWSTFEX;
using i2TradePlus.Properties;
using System;
using System.Net;
using System.Runtime.CompilerServices;
namespace i2TradePlus
{
	internal class BusinessTfexService : Service
	{
		private bool isFirstOpen = true;
		//private WebProxy proxy = null;
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override WebRequest GetWebRequest(Uri uri)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
			if (this.isFirstOpen)
			{
				httpWebRequest.Timeout = 30000;
				httpWebRequest.ReadWriteTimeout = 30000;
			}
			else
			{
				httpWebRequest.Timeout = 30000;
				httpWebRequest.ReadWriteTimeout = 10000;
			}
			httpWebRequest.KeepAlive = true;
			httpWebRequest.ProtocolVersion = HttpVersion.Version11;
			if (ApplicationInfo.IsUseProxy)
			{
                //if (this.proxy == null)
                //{
                //    this.proxy = new WebProxy(Settings.Default.ProxyHost, Settings.Default.ProxyPort);
                //    this.proxy.Credentials = new NetworkCredential(Settings.Default.ProxyUsername, ApplicationInfo.ProxyPassword);
                //}
                //httpWebRequest.Proxy = this.proxy;
			}
			this.isFirstOpen = false;
			return httpWebRequest;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public BusinessTfexService()
		{
		}
	}
}
