using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Deployment.Application;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Windows.Forms;
namespace i2TradePlus.Start
{
	internal static class Program
	{
		private static byte[] bytes = null;
		private static int _brokerID = 0;
		private static string url = string.Empty;
		private static string url_tfex = string.Empty;
		private static string authenkey = "{52E38375-7953-493b-9149-D19A86B5B25E}";
		private static string I2INFO_URL = string.Empty;
		private static string DES_KEY = string.Empty;
		[STAThread]
		private static void Main(params string[] args)
		{
			try
			{
				string text = string.Empty;
				int.TryParse(ConfigurationManager.AppSettings["BROKER_ID"], out Program._brokerID);
				Program.DES_KEY = ConfigurationManager.AppSettings["KEY"];
				Program.bytes = Encoding.ASCII.GetBytes(Program.DES_KEY);
				text = Program.GetparameterfromWeb();
				i2TradePlus.Program.Start(new string[]
				{
					text
				});
			}
			catch (Exception ex)
			{
				EventLog.WriteEntry("i2TradePlus", "StartProgram:" + ex.Message, EventLogEntryType.Error);
			}
		}
		private static string GetparameterfromWeb()
		{
			string result = string.Empty;
			string text = string.Empty;
			string text2 = string.Empty;
			string text3 = string.Empty;
			string text4 = string.Empty;
			string text5 = string.Empty;
			string text6 = string.Empty;
			string arg_32_0 = string.Empty;
			string arg_38_0 = string.Empty;
			string text7 = string.Empty;
			string text8 = "Y";
			string text9 = "N";
			string text10 = string.Empty;
			string text11 = "C";
			try
			{
				string cryptedString = "";
				string text12 = "";
				string s = "";
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				bool flag;
				if (ApplicationDeployment.IsNetworkDeployed && ApplicationDeployment.CurrentDeployment.ActivationUri != null)
				{
					NameValueCollection nameValueCollection = new NameValueCollection();
					string text13 = HttpUtility.UrlDecode(ApplicationDeployment.CurrentDeployment.ActivationUri.Query);
					if (!string.IsNullOrEmpty(text13))
					{
						if (text13.Substring(0, 1) == "?")
						{
							text13 = text13.Substring(1);
						}
						string[] array = text13.Split(new char[]
						{
							'&'
						});
						for (int i = 0; i < array.Length; i++)
						{
							dictionary.Add(array[i].Substring(0, 1), array[i].Substring(2));
						}
						foreach (KeyValuePair<string, string> current in dictionary)
						{
							if (current.Key == "D")
							{
								cryptedString = current.Value;
							}
							else
							{
								if (current.Key == "M")
								{
									text12 = current.Value;
								}
								else
								{
									if (current.Key == "P")
									{
										s = current.Value;
									}
								}
							}
						}
						if (text12 == "" || text12 == "1")
						{
							text13 = Program.DecryptDES(cryptedString);
						}
						else
						{
							text13 = Program.DecryptDES(cryptedString, int.Parse(text12), int.Parse(s));
						}
						nameValueCollection = HttpUtility.ParseQueryString(text13.Replace("\0", ""));
						if (!string.IsNullOrEmpty(text13))
						{
							text = nameValueCollection["account"];
							text2 = nameValueCollection["inbroker"];
							text3 = nameValueCollection["ke_session"];
							text4 = nameValueCollection["ke_athurl"];
							text5 = nameValueCollection["ke_local"];
							text6 = nameValueCollection["aspticket"];
							text10 = nameValueCollection["pin"];
							string arg_27E_0 = nameValueCollection["ktzmico_session"];
							string arg_28B_0 = nameValueCollection["ktz_cust_map_key"];
							text7 = nameValueCollection["user_efin"];
							text8 = nameValueCollection["second_i2info"];
							if (!string.IsNullOrEmpty(nameValueCollection["req_tfex"]))
							{
								text9 = nameValueCollection["req_tfex"];
							}
							flag = true;
						}
						else
						{
							flag = false;
							text2 = "N";
						}
					}
					else
					{
						flag = false;
						text2 = "N";
					}
				}
				else
				{
					flag = false;
					text2 = "N";
				}
				result = string.Concat(new object[]
				{
					"openfromweb=",
					flag,
					";account=",
					text,
					";inbroker=",
					text2,
					";ke_session=",
					text3,
					";ke_athurl=",
					text4,
					";ke_local=",
					text5,
					";appid=",
					text11,
					";aspticket=",
					text6,
					";pin=",
					text10,
					";usernameproxy=",
					i2InfoWS.Usernameproxy,
					";passwordproxy=",
					i2InfoWS.Passwordproxy,
					";hostproxy=",
					i2InfoWS.Hostproxy,
					";portproxy=",
					i2InfoWS.Portproxy,
					";key=",
					Program.authenkey,
					";user_efin=",
					text7,
					";brokerid=",
					Program._brokerID,
					";req_tfex=",
					text9,
					";second_i2info=",
					text8
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "GetparameterfromWeb(), Please contact administrator.");
			}
			return result;
		}
		private static string EncryptDES(string originalString)
		{
			string result;
			try
			{
				if (string.IsNullOrEmpty(originalString))
				{
					throw new ArgumentNullException("The string which needs to be encrypted can not be null.");
				}
				DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
				MemoryStream memoryStream = new MemoryStream();
				CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(Program.bytes, Program.bytes), CryptoStreamMode.Write);
				StreamWriter streamWriter = new StreamWriter(cryptoStream);
				streamWriter.Write(originalString);
				streamWriter.Flush();
				cryptoStream.FlushFinalBlock();
				streamWriter.Flush();
				result = Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		private static string DecryptDES(string cryptedString)
		{
			string result;
			try
			{
				if (string.IsNullOrEmpty(cryptedString))
				{
					throw new ArgumentNullException("The string which needs to be decrypted can not be null.");
				}
				DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
				MemoryStream stream = new MemoryStream(Convert.FromBase64String(cryptedString));
				CryptoStream stream2 = new CryptoStream(stream, dESCryptoServiceProvider.CreateDecryptor(Program.bytes, Program.bytes), CryptoStreamMode.Read);
				StreamReader streamReader = new StreamReader(stream2);
				result = streamReader.ReadToEnd();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		private static string DecryptDES(string cryptedString, int mode, int pading)
		{
			string result;
			try
			{
				if (string.IsNullOrEmpty("+" + cryptedString))
				{
					throw new ArgumentNullException("The string which needs to be decrypted can not be null.");
				}
				DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
				dESCryptoServiceProvider.Mode = (CipherMode)mode;
				dESCryptoServiceProvider.Padding = (PaddingMode)pading;
				MemoryStream stream = new MemoryStream(Convert.FromBase64String(cryptedString));
				CryptoStream stream2 = new CryptoStream(stream, dESCryptoServiceProvider.CreateDecryptor(Program.bytes, Program.bytes), CryptoStreamMode.Read);
				StreamReader streamReader = new StreamReader(stream2);
				result = streamReader.ReadToEnd();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
	}
}
