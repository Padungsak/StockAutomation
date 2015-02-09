using Microsoft.Win32;
using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
namespace i2TradePlus.Start
{
	public class Https
	{
		private int readBufferSize = 1024;
		[CompilerGenerated]
		//private static RemoteCertificateValidationCallback <>9__CachedAnonymousMethodDelegate1;
		public static bool GetProxyEnable()
		{
			string registryStringValue = Https.GetRegistryStringValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", "ProxyEnable");
			return registryStringValue == "1";
		}
		public static string GetProxyServer()
		{
			return Https.GetRegistryStringValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", "ProxyServer");
		}
		public static string GetRegistryStringValue(RegistryKey baseKey, string strSubKey, string strValue)
		{
			object obj = null;
			string arg_09_0 = string.Empty;
			try
			{
				RegistryKey registryKey = baseKey.OpenSubKey(strSubKey);
				if (registryKey == null)
				{
					string result = null;
					return result;
				}
				obj = registryKey.GetValue(strValue);
				if (obj == null)
				{
					string result = null;
					return result;
				}
				registryKey.Close();
				baseKey.Close();
			}
			catch (Exception ex)
			{
				string arg_3D_0 = ex.Message;
				string result = null;
				return result;
			}
			return obj.ToString();
		}
		public string Get(string URL)
		{
			CookieContainer cookies = new CookieContainer();
			HttpWebRequest webRequest = this.GetWebRequest(new Uri(URL), cookies);
			ServicePointManager.ServerCertificateValidationCallback = ((object obj, X509Certificate x509Certificate, X509Chain x509Chain, SslPolicyErrors sslPolicyErrors) => true);
			string result;
			try
			{
				HttpWebResponse httpWebResponse = (HttpWebResponse)webRequest.GetResponse();
				Stream responseStream = httpWebResponse.GetResponseStream();
				StreamReader streamReader = new StreamReader(responseStream);
				char[] array = new char[this.readBufferSize];
				int i = streamReader.Read(array, 0, this.readBufferSize);
				string text = string.Empty;
				while (i > 0)
				{
					string str = new string(array, 0, i);
					text += str;
					i = streamReader.Read(array, 0, this.readBufferSize);
				}
				streamReader.Close();
				responseStream.Close();
				httpWebResponse.Close();
				result = text;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		private HttpWebRequest GetWebRequest(Uri uri, CookieContainer cookies)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
			httpWebRequest.CookieContainer = cookies;
			httpWebRequest.Timeout = 15000;
			httpWebRequest.ReadWriteTimeout = 10000;
			httpWebRequest.KeepAlive = true;
			httpWebRequest.ProtocolVersion = HttpVersion.Version11;
            //if (i2InfoWS.Proxy != null)
            //{
            //    httpWebRequest.Proxy = i2InfoWS.Proxy;
            //}
            //else
            //{
				httpWebRequest.Credentials = CredentialCache.DefaultCredentials;
			//}
			return httpWebRequest;
		}
	}
}
