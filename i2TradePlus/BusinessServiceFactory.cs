using i2TradePlus.ITSNetBusinessWS;
using i2TradePlus.ITSNetBusinessWSTFEX;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
namespace i2TradePlus
{
	internal class BusinessServiceFactory
	{
		public static bool IsSSLEnabled
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return !string.IsNullOrEmpty(ConfigurationManager.AppSettings["WebEnableSSL"]) && ConfigurationManager.AppSettings["WebEnableSSL"] == "1";
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public i2TradePlus.ITSNetBusinessWS.Service CreateSET(ITSNetWS ITSNetWS)
		{
			i2TradePlus.ITSNetBusinessWS.Service result;
			try
			{
				i2TradePlus.ITSNetBusinessWS.Service service = new BusinessService();
				ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this.RemoteCertificateValidationCallBack);
				if (ITSNetWS == ITSNetWS.StockService)
				{
					service.Url = ApplicationInfo.WebUrl;
				}
				else
				{
					if (ITSNetWS == ITSNetWS.UserService)
					{
						service.Url = ApplicationInfo.WebUserUrl;
					}
					else
					{
						if (ITSNetWS == ITSNetWS.OrderService)
						{
							service.Url = ApplicationInfo.WebOrderUrl;
						}
						else
						{
							if (ITSNetWS == ITSNetWS.AlertService)
							{
								service.Url = ApplicationInfo.WebAlertUrl;
							}
						}
					}
				}
				result = service;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public i2TradePlus.ITSNetBusinessWSTFEX.Service CreateTFEX()
		{
			i2TradePlus.ITSNetBusinessWSTFEX.Service result;
			try
			{
				i2TradePlus.ITSNetBusinessWSTFEX.Service service = new BusinessTfexService();
				ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this.RemoteCertificateValidationCallBack);
				service.Url = ApplicationInfo.WebTfexUrl;
				result = service;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string GetRegistryStringValue(RegistryKey baseKey, string strSubKey, string strValue)
		{
			object obj = null;
			string text = string.Empty;
			string result;
			try
			{
				RegistryKey registryKey = baseKey.OpenSubKey(strSubKey);
				if (registryKey == null)
				{
					result = null;
					return result;
				}
				obj = registryKey.GetValue(strValue);
				if (obj == null)
				{
					result = null;
					return result;
				}
				registryKey.Close();
				baseKey.Close();
			}
			catch (Exception ex)
			{
				text = ex.Message;
				result = null;
				return result;
			}
			result = obj.ToString();
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool RemoteCertificateValidationCallBack(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			bool result;
			if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["SSLAcceptAnyCert"]))
			{
				result = true;
			}
			else
			{
				if (ConfigurationManager.AppSettings["SSLAcceptAnyCert"] == "0")
				{
					result = true;
				}
				else
				{
					List<string> list = new List<string>();
					list.AddRange(certificate.Issuer.Split(new char[]
					{
						','
					}));
					string text = ConfigurationManager.AppSettings["SSLCertSerialNumber"];
					string text2 = ConfigurationManager.AppSettings["SSLCertSubject"];
					if (!string.IsNullOrEmpty(text) && list.Contains(text))
					{
						if (!string.IsNullOrEmpty(text2) && text2 == certificate.Subject)
						{
							result = true;
							return result;
						}
					}
					result = false;
				}
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public BusinessServiceFactory()
		{
		}
	}
}
