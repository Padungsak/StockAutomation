using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;
namespace i2TradePlus.Start
{
	public class i2InfoWS
	{
		private const string EXECUTE_NAME = "i2TradePlus.Start";
		public const string DEVICE = "S";
		public const string WS_TYPE = "ashx";
		public const int BROKER_DEMO = 88;
		public const int BROKER_CNS = 1;
		public const int BROKER_KTZ = 2;
		public const int BROKER_BFIT = 3;
		public const int BROKER_KIMENG = 4;
		public const int BROKER_AIRA = 7;
		public const int BROKER_ASIA_PLUS = 8;
		public static string Hostproxy = string.Empty;
		public static string Portproxy = string.Empty;
		public static string Usernameproxy = string.Empty;
		public static string Passwordproxy = string.Empty;
		private static Https i2API;
		public static string MBKET_I2INFO_URL = string.Empty;
		public static string CNS_I2INFO_URL = string.Empty;
		public static string ASP_I2INFO_URL = string.Empty;
		public static string STI_I2INFO_URL = "http://203.145.118.18/i2Trade";
		public static string I2INFO_URL = "http://203.145.118.18/i2Trade";
		public static int BrokerId = 0;
        //private static i2TradePlus.WebProxy _proxy = null;
        //public static i2TradePlus.WebProxy Proxy
        //{
        //    get
        //    {
        //        return i2InfoWS._proxy;
        //    }
        //    set
        //    {
        //        i2InfoWS._proxy = value;
        //    }
        //}
		public static i2WSResult GetConnectionInfo(string host, string device)
		{
			i2WSResult i2WSResult = default(i2WSResult);
			i2WSResult result;
			try
			{
				i2InfoWS.i2API = new Https();
				string uRL = host + "?device=" + device;
				string xml = i2InfoWS.i2API.Get(uRL);
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(xml);
				i2WSResult.Code = xmlDocument.GetElementsByTagName("code").Item(0).Attributes["value"].Value;
				if (i2WSResult.Code == "0")
				{
					i2WSResult.Version = xmlDocument.GetElementsByTagName("version").Item(0).Attributes["value"].Value;
					i2WSResult.Installerurl = xmlDocument.GetElementsByTagName("installerurl").Item(0).Attributes["value"].Value;
					i2WSResult.UpdateURL = xmlDocument.GetElementsByTagName("updateurl").Item(0).Attributes["value"].Value;
					i2WSResult.WsURL = xmlDocument.GetElementsByTagName("servername").Item(0).Attributes["value"].Value;
					i2WSResult.WsDURL = xmlDocument.GetElementsByTagName("servernamed").Item(0).Attributes["value"].Value;
					i2WSResult.SessionID = xmlDocument.GetElementsByTagName("session").Item(0).Attributes["value"].Value;
					i2WSResult.Description = "Successed";
				}
				result = i2WSResult;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		public static string GetServerVersion(string url)
		{
			string result;
			try
			{
				i2InfoWS.i2API = new Https();
				string uRL = url + "/i2TradePlus.Start.application";
				string xml = i2InfoWS.i2API.Get(uRL);
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(xml);
				string value = xmlDocument.GetElementsByTagName("assemblyIdentity").Item(0).Attributes["version"].Value;
				result = value;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		public static string GetLocalVersion(string savePath)
		{
			string result = "";
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(savePath + "i2TradePlus.Start.exe.manifest");
			for (int i = 0; i < xmlDocument.GetElementsByTagName("asmv1:assemblyIdentity").Count; i++)
			{
				XmlNode xmlNode = xmlDocument.GetElementsByTagName("asmv1:assemblyIdentity").Item(i);
				if (xmlNode.Attributes["name"].Value == "i2TradePlus.Start.exe")
				{
					result = xmlNode.Attributes["version"].Value;
					break;
				}
			}
			return result;
		}
		public static string GetAppManifestURL(string url)
		{
			string result;
			try
			{
				i2InfoWS.i2API = new Https();
				string uRL = url + "/i2TradePlus.Start.application";
				string xml = i2InfoWS.i2API.Get(uRL);
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(xml);
				string value = xmlDocument.GetElementsByTagName("dependentAssembly").Item(0).Attributes["codebase"].Value;
				result = value;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		public static void GetAppFileList(string url, ref List<string> fileList)
		{
			List<string> list = fileList;
			try
			{
				i2InfoWS.i2API = new Https();
				string xml = i2InfoWS.i2API.Get(url);
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(xml);
				for (int i = 0; i < xmlDocument.GetElementsByTagName("dependentAssembly").Count; i++)
				{
					if (xmlDocument.GetElementsByTagName("dependentAssembly").Item(i).Attributes["codebase"] != null)
					{
						list.Add(xmlDocument.GetElementsByTagName("dependentAssembly").Item(i).Attributes["codebase"].Value);
					}
				}
				for (int j = 0; j < xmlDocument.GetElementsByTagName("file").Count; j++)
				{
					if (xmlDocument.GetElementsByTagName("file").Item(j).Attributes["name"] != null)
					{
						list.Add(xmlDocument.GetElementsByTagName("file").Item(j).Attributes["name"].Value);
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
