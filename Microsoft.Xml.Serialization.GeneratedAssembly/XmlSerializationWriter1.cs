using System;
using System.Xml;
using System.Xml.Serialization;
namespace Microsoft.Xml.Serialization.GeneratedAssembly
{
	public class XmlSerializationWriter1 : XmlSerializationWriter
	{
		public void Write1_GetTunnelConfig(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("GetTunnelConfig", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write2_GetTunnelConfigInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write3_GetTunnel(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetTunnel", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("isRequestTfex", "http://tempuri.org/", XmlConvert.ToString((bool)p[0]));
			}
			base.WriteEndElement();
		}
		public void Write4_GetTunnelInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write5_VerifyOrder(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("VerifyOrder", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("side", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("volume", "http://tempuri.org/", XmlConvert.ToString((long)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementString("price", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[4]);
			}
			if (num > 5)
			{
				base.WriteElementStringRaw("isValidatePolicy", "http://tempuri.org/", XmlConvert.ToString((bool)p[5]));
			}
			base.WriteEndElement();
		}
		public void Write6_VerifyOrderInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write7_VerifyOrderFw(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("VerifyOrderFw", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("side", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("volume", "http://tempuri.org/", XmlConvert.ToString((long)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementString("price", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("pubVol", "http://tempuri.org/", XmlConvert.ToString((long)p[4]));
			}
			if (num > 5)
			{
				base.WriteElementString("condition", "http://tempuri.org/", (string)p[5]);
			}
			base.WriteEndElement();
		}
		public void Write8_VerifyOrderFwInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write9_VerifyOrderMkt(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("VerifyOrderMkt", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write10_VerifyOrderMktInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write11_GetMainInfo(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetMainInfo", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("userKey", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("localIp", "http://tempuri.org/", (string)p[2]);
			}
			base.WriteEndElement();
		}
		public void Write12_GetMainInfoInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write13_UserAuthen(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("UserAuthen", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("password", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("localIp", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementString("device", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementString("versionCode", "http://tempuri.org/", (string)p[4]);
			}
			if (num > 5)
			{
				base.WriteElementStringRaw("isGetMainInfo", "http://tempuri.org/", XmlConvert.ToString((bool)p[5]));
			}
			base.WriteEndElement();
		}
		public void Write14_UserAuthenInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write15_ClearEfinSession(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ClearEfinSession", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userefin", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write16_ClearEfinSessionInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write17_GetUrlClient(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("GetUrlClient", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write18_GetUrlClientInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write19_Logout(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("Logout", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("sessionID", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementString("loginMode", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("userLoginID", "http://tempuri.org/", (string)p[2]);
			}
			base.WriteEndElement();
		}
		public void Write20_LogoutInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write21_LogoutAD(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("LogoutAD", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("sessionID", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementString("loginMode", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("userLoginID", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementString("brokerParams", "http://tempuri.org/", (string)p[3]);
			}
			base.WriteEndElement();
		}
		public void Write22_LogoutADInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write23_ChangeCustomerPassword(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ChangeCustomerPassword", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custAccID", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("oldPassword", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("newPassword", "http://tempuri.org/", (string)p[2]);
			}
			base.WriteEndElement();
		}
		public void Write24_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write25_ChangeTraderPassword(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ChangeTraderPassword", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("traderID", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("oldPassword", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("newPassword", "http://tempuri.org/", (string)p[2]);
			}
			base.WriteEndElement();
		}
		public void Write26_ChangeTraderPasswordInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write27_StockThresholdInformation(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("StockThresholdInformation", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("loginID", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write28_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write29_SaveStockThreshold(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SaveStockThreshold", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("customerId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("items", "http://tempuri.org/", (string)p[1]);
			}
			base.WriteEndElement();
		}
		public void Write30_SaveStockThresholdInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write31_SendNewOrder(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SendNewOrder", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("side", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("volume", "http://tempuri.org/", XmlConvert.ToString((long)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementString("price", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[4]);
			}
			if (num > 5)
			{
				base.WriteElementStringRaw("publishVolume", "http://tempuri.org/", XmlConvert.ToString((long)p[5]));
			}
			if (num > 6)
			{
				base.WriteElementString("condition", "http://tempuri.org/", (string)p[6]);
			}
			if (num > 7)
			{
				base.WriteElementString("trusteeID", "http://tempuri.org/", (string)p[7]);
			}
			if (num > 8)
			{
				base.WriteElementString("entryID", "http://tempuri.org/", (string)p[8]);
			}
			if (num > 9)
			{
				base.WriteElementString("kimengSessionId", "http://tempuri.org/", (string)p[9]);
			}
			if (num > 10)
			{
				base.WriteElementString("kimengLocal", "http://tempuri.org/", (string)p[10]);
			}
			if (num > 11)
			{
				base.WriteElementString("deposit", "http://tempuri.org/", (string)p[11]);
			}
			if (num > 12)
			{
				base.WriteElementString("authenKey", "http://tempuri.org/", (string)p[12]);
			}
			if (num > 13)
			{
				base.WriteElementString("localIp", "http://tempuri.org/", (string)p[13]);
			}
			if (num > 14)
			{
				base.WriteElementString("pstWebIP", "http://tempuri.org/", (string)p[14]);
			}
			if (num > 15)
			{
				base.WriteElementString("pstIspIP", "http://tempuri.org/", (string)p[15]);
			}
			if (num > 16)
			{
				base.WriteElementString("terminalID", "http://tempuri.org/", (string)p[16]);
			}
			base.WriteEndElement();
		}
		public void Write32_SendNewOrderInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write33_SendNewOrderParams(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SendNewOrderParams", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("orderParams", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write34_SendNewOrderParamsInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write35_SendCancelOrder_AfterCloseFw(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SendCancelOrder_AfterCloseFw", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("orderDate", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("orderTime", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("orderNumber", "http://tempuri.org/", XmlConvert.ToString((long)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementString("endtryId", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementString("authenKey", "http://tempuri.org/", (string)p[4]);
			}
			base.WriteEndElement();
		}
		public void Write36_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write37_SendCancelOrder(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SendCancelOrder", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("sendDate", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("orderNumber", "http://tempuri.org/", XmlConvert.ToString((long)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementString("entryID", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementString("sessionId", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementString("authenKey", "http://tempuri.org/", (string)p[4]);
			}
			if (num > 5)
			{
				base.WriteElementString("internetUser", "http://tempuri.org/", (string)p[5]);
			}
			if (num > 6)
			{
				base.WriteElementString("terminalID", "http://tempuri.org/", (string)p[6]);
			}
			base.WriteEndElement();
		}
		public void Write38_SendCancelOrderInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write39_SendEditOrder(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SendEditOrder", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("authenKey", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("sessionId", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("orderNumber", "http://tempuri.org/", XmlConvert.ToString((long)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementString("entryDate", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("volume", "http://tempuri.org/", XmlConvert.ToString((long)p[4]));
			}
			if (num > 5)
			{
				base.WriteElementString("price", "http://tempuri.org/", (string)p[5]);
			}
			if (num > 6)
			{
				base.WriteElementString("logIn", "http://tempuri.org/", (string)p[6]);
			}
			if (num > 7)
			{
				base.WriteElementStringRaw("pubVol", "http://tempuri.org/", XmlConvert.ToString((long)p[7]));
			}
			if (num > 8)
			{
				base.WriteElementStringRaw("trusteeId", "http://tempuri.org/", XmlConvert.ToString((int)p[8]));
			}
			if (num > 9)
			{
				base.WriteElementString("userNo", "http://tempuri.org/", (string)p[9]);
			}
			base.WriteEndElement();
		}
		public void Write40_SendEditOrderInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write41_ViewNewsHeader(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewNewsHeader", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("isCurrentDate", "http://tempuri.org/", XmlConvert.ToString((bool)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementString("selectDate", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("selectStock", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("recordPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("page", "http://tempuri.org/", XmlConvert.ToString((int)p[4]));
			}
			base.WriteEndElement();
		}
		public void Write42_ViewNewsHeaderInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write43_ViewNewsStory(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewNewsStory", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("isCurrentDate", "http://tempuri.org/", XmlConvert.ToString((bool)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementString("selectDate", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("newNo", "http://tempuri.org/", XmlConvert.ToString((long)p[2]));
			}
			base.WriteEndElement();
		}
		public void Write44_ViewNewsStoryInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write45_ViewCustomersInfo(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomersInfo", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custAccID", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("userLoginID", "http://tempuri.org/", (string)p[1]);
			}
			base.WriteEndElement();
		}
		public void Write46_ViewCustomersInfoInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write47_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomerCreditOnSendBox_Freewill", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("accType", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[2]);
			}
			base.WriteEndElement();
		}
		public void Write48_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write49_ViewCustomerCreditOnSendBox(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomerCreditOnSendBox", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[1]);
			}
			base.WriteEndElement();
		}
		public void Write50_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write51_GetSwitchAccountInfo(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetSwitchAccountInfo", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write52_GetSwitchAccountInfoInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write53_GetSwitchAccountInfoEservice(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetSwitchAccountInfoEservice", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("brokerID", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("session", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementString("accountNo", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementString("system", "http://tempuri.org/", (string)p[4]);
			}
			if (num > 5)
			{
				base.WriteElementString("service", "http://tempuri.org/", (string)p[5]);
			}
			base.WriteEndElement();
		}
		public void Write54_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write55_ViewCustomer_MobileReportAll(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomer_MobileReportAll", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custAccID", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("stockSymbol", "http://tempuri.org/", (string)p[1]);
			}
			base.WriteEndElement();
		}
		public void Write56_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write57_ViewCustomer_OrdersConfirms(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomer_OrdersConfirms", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custAccID", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("stockSymbol", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("startRow", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			base.WriteEndElement();
		}
		public void Write58_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write59_ViewCustomer_CreditPosition(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomer_CreditPosition", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custAccID", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("stockSymbol", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("startRow", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			base.WriteEndElement();
		}
		public void Write60_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write61_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomer_ProjectedProfitLoss", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custAccID", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("stockSymbol", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("startRow", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			base.WriteEndElement();
		}
		public void Write62_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write63_ViewCustomer_RealizeProfitLoss(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomer_RealizeProfitLoss", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custAccID", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("stockSymbol", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("startRow", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			base.WriteEndElement();
		}
		public void Write64_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write65_ViewCustomer_Summary(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomer_Summary", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custAccID", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("stockSymbol", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("startRow", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			base.WriteEndElement();
		}
		public void Write66_ViewCustomer_SummaryInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write67_ViewCustomer_ConfirmSummary(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomer_ConfirmSummary", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custAccID", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("commGroup", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementString("stockSymbol", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("startRow", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[4]));
			}
			base.WriteEndElement();
		}
		public void Write68_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write69_ViewCustomer_Confirm(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomer_Confirm", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custAccID", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("commGroup", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementString("stockSymbol", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("startRow", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[4]));
			}
			base.WriteEndElement();
		}
		public void Write70_ViewCustomer_ConfirmInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write71_ViewCustomer_ConfirmByDealID(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomer_ConfirmByDealID", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custAccID", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("commGroup", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementString("stockSymbol", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("startRow", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[4]));
			}
			base.WriteEndElement();
		}
		public void Write72_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write73_ViewCustomer_ConfirmByStock(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomer_ConfirmByStock", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custAccID", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("commGroup", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementString("stockSymbol", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("startRow", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[4]));
			}
			base.WriteEndElement();
		}
		public void Write74_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write75_ViewOrderInfo_AfterCloseFw(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewOrderInfo_AfterCloseFw", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("orderNumber", "http://tempuri.org/", XmlConvert.ToString((long)p[0]));
			}
			base.WriteEndElement();
		}
		public void Write76_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write77_ViewOrderDealData(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewOrderDealData", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("sSendDate", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("orderNumber", "http://tempuri.org/", XmlConvert.ToString((long)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("startRow", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("dbType", "http://tempuri.org/", XmlConvert.ToString((int)p[4]));
			}
			base.WriteEndElement();
		}
		public void Write78_ViewOrderDealDataInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write79_ViewOrderDealDataHistory(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewOrderDealDataHistory", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("sSendDate", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("orderNumber", "http://tempuri.org/", XmlConvert.ToString((long)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("startRow", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			base.WriteEndElement();
		}
		public void Write80_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write81_GetCometInfo(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("GetCometInfo", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write82_GetCometInfoInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write83_TopActiveBBO_Sector(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("TopActiveBBO_Sector", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("sectorNumber", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			base.WriteEndElement();
		}
		public void Write84_TopActiveBBO_SectorInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write85_InvestorType(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("InvestorType", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("summaryType", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write86_InvestorTypeInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write87_MarketIndicator(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("MarketIndicator", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write88_MarketIndicatorInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write89_SaleByPrice(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SaleByPrice", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("securityNumber", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("startRow", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("maxTicker", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			base.WriteEndElement();
		}
		public void Write90_SaleByPriceInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write91_SectorStat(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("SectorStat", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write92_SectorStatInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write93_SectorStatForDump(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SectorStatForDump", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("industryNumber", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			base.WriteEndElement();
		}
		public void Write94_SectorStatForDumpInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write95_StockStatForDump(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("StockStatForDump", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("sectorNumber", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			base.WriteEndElement();
		}
		public void Write96_StockStatForDumpInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write97_GetUserConfigForDump(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetUserConfigForDump", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write98_GetUserConfigForDumpInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write99_SaveUserConfigForDump(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SaveUserConfigForDump", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("configName", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("configValue", "http://tempuri.org/", (string)p[2]);
			}
			base.WriteEndElement();
		}
		public void Write100_SaveUserConfigForDumpInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write101_SaleByTime2(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SaleByTime2", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("securityNumber", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("startRow", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("maxTicker", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementString("startTime", "http://tempuri.org/", (string)p[4]);
			}
			base.WriteEndElement();
		}
		public void Write102_SaleByTime2InHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write103_StockInPlay(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("StockInPlay", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("SecurityNumber", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("StartPrice", "http://tempuri.org/", XmlConvert.ToString((decimal)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementString("Side", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("maxTicker", "http://tempuri.org/", XmlConvert.ToString((int)p[4]));
			}
			base.WriteEndElement();
		}
		public void Write104_StockInPlayInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write105_StockByPricePage(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("StockByPricePage", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("stockNumber", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("maxTicker", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			base.WriteEndElement();
		}
		public void Write106_StockByPricePageInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write107_MarketStatus(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("MarketStatus", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write108_MarketStatusInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write109_TopBBO(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("TopBBO", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				int[] array = (int[])p[0];
				if (array != null)
				{
					base.WriteStartElement("stockNumber", "http://tempuri.org/", null, false);
					for (int i = 0; i < array.Length; i++)
					{
						base.WriteElementStringRaw("int", "http://tempuri.org/", XmlConvert.ToString(array[i]));
					}
					base.WriteEndElement();
				}
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("maxTicker", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			base.WriteEndElement();
		}
		public void Write110_TopBBOInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write111_TopBBOad(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("TopBBOad", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("stocks", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("maxTicker", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			base.WriteEndElement();
		}
		public void Write112_TopBBOadInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write113_ViewOddlot(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewOddlot", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("stockNo", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("maxTicker", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			base.WriteEndElement();
		}
		public void Write114_ViewOddlotInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write115_Get5BidOffer(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("Get5BidOffer", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("stockNumber", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			base.WriteEndElement();
		}
		public void Write116_Get5BidOfferInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write117_ViewOrderTransaction(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewOrderTransaction", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("id", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("senderType", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("readType", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[4]);
			}
			if (num > 5)
			{
				base.WriteElementString("side", "http://tempuri.org/", (string)p[5]);
			}
			if (num > 6)
			{
				base.WriteElementString("price", "http://tempuri.org/", (string)p[6]);
			}
			if (num > 7)
			{
				base.WriteElementString("status", "http://tempuri.org/", (string)p[7]);
			}
			if (num > 8)
			{
				base.WriteElementStringRaw("startOrderNo", "http://tempuri.org/", XmlConvert.ToString((long)p[8]));
			}
			if (num > 9)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[9]));
			}
			if (num > 10)
			{
				base.WriteElementStringRaw("expression", "http://tempuri.org/", XmlConvert.ToString((int)p[10]));
			}
			base.WriteEndElement();
		}
		public void Write118_ViewOrderTransactionInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write119_ViewOrderHistory(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewOrderHistory", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("side", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementString("selDate1", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementString("selDate2", "http://tempuri.org/", (string)p[4]);
			}
			if (num > 5)
			{
				base.WriteElementStringRaw("compareDays", "http://tempuri.org/", XmlConvert.ToString((int)p[5]));
			}
			base.WriteEndElement();
		}
		public void Write120_ViewOrderHistoryInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write121_ViewOrdersForDump(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewOrdersForDump", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("side", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementString("price", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementString("status", "http://tempuri.org/", (string)p[4]);
			}
			if (num > 5)
			{
				base.WriteElementStringRaw("startOrderNumber", "http://tempuri.org/", XmlConvert.ToString((long)p[5]));
			}
			if (num > 6)
			{
				base.WriteElementStringRaw("recordPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[6]));
			}
			if (num > 7)
			{
				base.WriteElementStringRaw("page", "http://tempuri.org/", XmlConvert.ToString((int)p[7]));
			}
			base.WriteEndElement();
		}
		public void Write122_ViewOrdersForDumpInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write123_ViewOrderByOrderNo(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewOrderByOrderNo", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("orderNoList", "http://tempuri.org/", (string)p[1]);
			}
			base.WriteEndElement();
		}
		public void Write124_ViewOrderByOrderNoInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write125_SaveUserConfigAll(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SaveUserConfigAll", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("value", "http://tempuri.org/", (string)p[1]);
			}
			base.WriteEndElement();
		}
		public void Write126_SaveUserConfigAllInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write127_SendHeartBeat(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SendHeartBeat", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userID", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("session", "http://tempuri.org/", (string)p[1]);
			}
			base.WriteEndElement();
		}
		public void Write128_SendHeartBeatInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write129_SendHeartBeat2(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SendHeartBeat2", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("param", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write130_SendHeartBeat2InHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write131_SaveUserEfinForward(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SaveUserEfinForward", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("fullname", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("nickname", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementString("regisDate", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementString("emailAccount", "http://tempuri.org/", (string)p[4]);
			}
			base.WriteEndElement();
		}
		public void Write132_SaveUserEfinForwardInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write133_LoadAllInformation(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("LoadAllInformation", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write134_LoadAllInformationInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write135_LoadAllInformationIsodd(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("LoadAllInformationIsodd", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("isLoadOddlot", "http://tempuri.org/", XmlConvert.ToString((bool)p[0]));
			}
			base.WriteEndElement();
		}
		public void Write136_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write137_LoadStockInformation(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("LoadStockInformation", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("startSecNo", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("topSelect", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			base.WriteEndElement();
		}
		public void Write138_LoadStockInformationInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write139_LoadMarketInformation(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("LoadMarketInformation", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write140_LoadMarketInformationInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write141_LoadOddLotInformation(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("LoadOddLotInformation", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write142_LoadOddLotInformationInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write143_BoardcastMessage(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("BoardcastMessage", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("recordPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			base.WriteEndElement();
		}
		public void Write144_BoardcastMessageInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write145_SendPushMessage(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SendPushMessage", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("key", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("groupId", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("message", "http://tempuri.org/", (string)p[2]);
			}
			base.WriteEndElement();
		}
		public void Write146_SendPushMessageInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write147_BestBidOffer(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("BestBidOffer", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("stockList", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write148_BestBidOfferInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write149_BestOpenPrice(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("BestOpenPrice", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("viewType", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("compareMode", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("marketID", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("sesssionID", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("startRow", "http://tempuri.org/", XmlConvert.ToString((int)p[4]));
			}
			if (num > 5)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[5]));
			}
			base.WriteEndElement();
		}
		public void Write150_BestOpenPriceInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write151_BestProjected(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("BestProjected", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("boardType", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("compareMode", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("poType", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementString("marketID", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("startRow", "http://tempuri.org/", XmlConvert.ToString((int)p[4]));
			}
			if (num > 5)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[5]));
			}
			base.WriteEndElement();
		}
		public void Write152_BestProjectedInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write153_IndustryStat(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("IndustryStat", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write154_IndustryStatInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write155_TopActive(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("TopActive", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("viewType", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("board", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("marketID", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			base.WriteEndElement();
		}
		public void Write156_TopActiveInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write157_TopActiveBBO(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("TopActiveBBO", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("viewType", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("board", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("marketID", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			base.WriteEndElement();
		}
		public void Write158_TopActiveBBOInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write159_TopActiveBBO_Benefit(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("TopActiveBBO_Benefit", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write160_TopActiveBBO_BenefitInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write161_TopActiveBBO_Warrant(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("TopActiveBBO_Warrant", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write162_TopActiveBBO_WarrantInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write163_TopActiveBBO_DW(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("TopActiveBBO_DW", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("parentStock", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write164_TopActiveBBO_DWInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write165_TopActiveBBO_News(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("TopActiveBBO_News", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write166_TopActiveBBO_NewsInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write167_TopActiveBBO_TurnOver(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("TopActiveBBO_TurnOver", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write168_TopActiveBBO_TurnOverInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write169_SavePushAccount(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SavePushAccount", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("accountlogin", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("registrationID", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("device", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementString("accList", "http://tempuri.org/", (string)p[3]);
			}
			base.WriteEndElement();
		}
		public void Write170_SavePushAccountInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write171_SaveStockAlert(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SaveStockAlert", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("field", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("operatorType", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementString("mobileAlert", "http://tempuri.org/", (string)p[4]);
			}
			if (num > 5)
			{
				base.WriteElementStringRaw("value", "http://tempuri.org/", XmlConvert.ToString((decimal)p[5]));
			}
			if (num > 6)
			{
				base.WriteElementStringRaw("updateMode", "http://tempuri.org/", XmlConvert.ToString((int)p[6]));
			}
			if (num > 7)
			{
				base.WriteElementString("memo", "http://tempuri.org/", (string)p[7]);
			}
			base.WriteEndElement();
		}
		public void Write172_SaveStockAlertInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write173_SavePortAlert(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SavePortAlert", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementString("sType", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("trusteeId", "http://tempuri.org/", XmlConvert.ToString((int)p[4]));
			}
			if (num > 5)
			{
				base.WriteElementStringRaw("costPct", "http://tempuri.org/", XmlConvert.ToString((decimal)p[5]));
			}
			if (num > 6)
			{
				base.WriteElementStringRaw("pricePct", "http://tempuri.org/", XmlConvert.ToString((decimal)p[6]));
			}
			if (num > 7)
			{
				base.WriteElementStringRaw("sellPct", "http://tempuri.org/", XmlConvert.ToString((decimal)p[7]));
			}
			base.WriteEndElement();
		}
		public void Write174_SavePortAlertInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write175_SendStopOrder(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SendStopOrder", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("stockNumber", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("fieldType", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("operatorType", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("price", "http://tempuri.org/", XmlConvert.ToString((decimal)p[4]));
			}
			if (num > 5)
			{
				base.WriteElementString("ordAccount", "http://tempuri.org/", (string)p[5]);
			}
			if (num > 6)
			{
				base.WriteElementString("ordSide", "http://tempuri.org/", (string)p[6]);
			}
			if (num > 7)
			{
				base.WriteElementStringRaw("ordTtf", "http://tempuri.org/", XmlConvert.ToString((int)p[7]));
			}
			if (num > 8)
			{
				base.WriteElementStringRaw("ordVolume", "http://tempuri.org/", XmlConvert.ToString((long)p[8]));
			}
			if (num > 9)
			{
				base.WriteElementString("ordPrice", "http://tempuri.org/", (string)p[9]);
			}
			if (num > 10)
			{
				base.WriteElementStringRaw("ordPublish", "http://tempuri.org/", XmlConvert.ToString((long)p[10]));
			}
			if (num > 11)
			{
				base.WriteElementString("ordCondition", "http://tempuri.org/", (string)p[11]);
			}
			if (num > 12)
			{
				base.WriteElementString("ordEntryId", "http://tempuri.org/", (string)p[12]);
			}
			if (num > 13)
			{
				base.WriteElementString("kimengSession", "http://tempuri.org/", (string)p[13]);
			}
			if (num > 14)
			{
				base.WriteElementString("kimengLocal", "http://tempuri.org/", (string)p[14]);
			}
			if (num > 15)
			{
				base.WriteElementStringRaw("limit", "http://tempuri.org/", XmlConvert.ToString((int)p[15]));
			}
			if (num > 16)
			{
				base.WriteElementString("authenKey", "http://tempuri.org/", (string)p[16]);
			}
			base.WriteEndElement();
		}
		public void Write176_SendStopOrderInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write177_ViewStopOrder(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewStopOrder", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("refNo", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			base.WriteEndElement();
		}
		public void Write178_ViewStopOrderInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write179_ViewStopOrder_FirstLS(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewStopOrder_FirstLS", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("stockNo", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementString("condition", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("time", "http://tempuri.org/", (string)p[2]);
			}
			base.WriteEndElement();
		}
		public void Write180_ViewStopOrder_FirstLSInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write181_SendCancelStopOrder(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SendCancelStopOrder", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("refNo", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("stockNo", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementString("authenKey", "http://tempuri.org/", (string)p[3]);
			}
			base.WriteEndElement();
		}
		public void Write182_SendCancelStopOrderInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write183_StopOrderRegister(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("StopOrderRegister", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("isRegister", "http://tempuri.org/", XmlConvert.ToString((bool)p[1]));
			}
			base.WriteEndElement();
		}
		public void Write184_StopOrderRegisterInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write185_StopOrderCheckDisclaimer(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("StopOrderCheckDisclaimer", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write186_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write187_SaveUserAlert(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SaveUserAlert", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("isRecvAdvertise", "http://tempuri.org/", XmlConvert.ToString((bool)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("isMktSummary", "http://tempuri.org/", XmlConvert.ToString((bool)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("isRecvPort", "http://tempuri.org/", XmlConvert.ToString((bool)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementString("device", "http://tempuri.org/", (string)p[4]);
			}
			if (num > 5)
			{
				base.WriteElementStringRaw("mode", "http://tempuri.org/", XmlConvert.ToString((int)p[5]));
			}
			base.WriteEndElement();
		}
		public void Write188_SaveUserAlertInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write189_SaveUserAlert2(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SaveUserAlert2", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("isRecvAdvertise", "http://tempuri.org/", XmlConvert.ToString((bool)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("isMktSummary", "http://tempuri.org/", XmlConvert.ToString((bool)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("isRecvPort", "http://tempuri.org/", XmlConvert.ToString((bool)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementString("lstActiveGroup", "http://tempuri.org/", (string)p[4]);
			}
			base.WriteEndElement();
		}
		public void Write190_SaveUserAlert2InHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write191_GetAlertLog(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetAlertLog", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write192_GetAlertLogInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write193_GetUserAlert(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetUserAlert", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write194_GetUserAlertInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write195_GetStockAlertItems(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetStockAlertItems", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write196_GetStockAlertItemsInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write197_GetPortAlertItems(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetPortAlertItems", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write198_GetPortAlertItemsInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write199_NAVChart(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("NAVChart", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("startDate", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("endDate", "http://tempuri.org/", (string)p[2]);
			}
			base.WriteEndElement();
		}
		public void Write200_NAVChartInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write201_GetDataForSmartClick(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetDataForSmartClick", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[1]);
			}
			base.WriteEndElement();
		}
		public void Write202_GetDataForSmartClickInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write203_GetOrderFor1Click(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetOrderFor1Click", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("price", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementString("side", "http://tempuri.org/", (string)p[3]);
			}
			base.WriteEndElement();
		}
		public void Write204_GetOrderFor1ClickInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write205_SaveSummaryMarketData(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SaveSummaryMarketData", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("date", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("sumBy", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("investor", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("buyValue", "http://tempuri.org/", XmlConvert.ToString((decimal)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("pctBuyValue", "http://tempuri.org/", XmlConvert.ToString((decimal)p[4]));
			}
			if (num > 5)
			{
				base.WriteElementStringRaw("sellValue", "http://tempuri.org/", XmlConvert.ToString((decimal)p[5]));
			}
			if (num > 6)
			{
				base.WriteElementStringRaw("pctSellValue", "http://tempuri.org/", XmlConvert.ToString((decimal)p[6]));
			}
			if (num > 7)
			{
				base.WriteElementStringRaw("sumValue", "http://tempuri.org/", XmlConvert.ToString((decimal)p[7]));
			}
			if (num > 8)
			{
				base.WriteElementStringRaw("pctSumValue", "http://tempuri.org/", XmlConvert.ToString((decimal)p[8]));
			}
			base.WriteEndElement();
		}
		public void Write206_SaveSummaryMarketDataInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write207_GetStockSMA(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetStockSMA", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("period", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			base.WriteEndElement();
		}
		public void Write208_GetStockSMAInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write209_SaveUserConfig(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SaveUserConfig", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("keyValue", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("value", "http://tempuri.org/", (string)p[2]);
			}
			base.WriteEndElement();
		}
		public void Write210_SaveUserConfigInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write211_CountOrderCancelForDump(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("CountOrderCancelForDump", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("trusteeId", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementString("side", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementString("price", "http://tempuri.org/", (string)p[4]);
			}
			if (num > 5)
			{
				base.WriteElementStringRaw("startOrderNo", "http://tempuri.org/", XmlConvert.ToString((long)p[5]));
			}
			if (num > 6)
			{
				base.WriteElementStringRaw("endOrderNo", "http://tempuri.org/", XmlConvert.ToString((long)p[6]));
			}
			base.WriteEndElement();
		}
		public void Write212_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write213_ViewCustomerByStockForDump(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomerByStockForDump", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("trusteeId", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			base.WriteEndElement();
		}
		public void Write214_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write215_GetBrokerMarginVolume(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetBrokerMarginVolume", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write216_GetBrokerMarginVolumeInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write217_VerifyPincode(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("VerifyPincode", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("pincode", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("sessionid", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementString("ktzCustMapKey", "http://tempuri.org/", (string)p[3]);
			}
			base.WriteEndElement();
		}
		public void Write218_VerifyPincodeInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write219_VerifyPincode2(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("VerifyPincode2", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("pincode", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("sessionid", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementString("ktzCustMapKey", "http://tempuri.org/", (string)p[3]);
			}
			base.WriteEndElement();
		}
		public void Write220_VerifyPincode2InHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write221_SendBAMessage(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SendBAMessage", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("message", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write222_SendBAMessageInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write223_StockHistory(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("StockHistory", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("sDate", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("isPageNext", "http://tempuri.org/", XmlConvert.ToString((bool)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("recordPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			base.WriteEndElement();
		}
		public void Write224_StockHistoryInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write225_StockChart(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("StockChart", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("stockNo", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("recordPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			base.WriteEndElement();
		}
		public void Write226_StockChartInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write227_StockHistData(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("StockHistData", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("symbol", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("startDate", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("key", "http://tempuri.org/", (string)p[2]);
			}
			base.WriteEndElement();
		}
		public void Write228_StockHistDataInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write229_LoadStockNickname(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("LoadStockNickname", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custId", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write230_LoadStockNicknameInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write231_SaveStockNickname(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SaveStockNickname", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("oldStock", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementString("nickName", "http://tempuri.org/", (string)p[3]);
			}
			base.WriteEndElement();
		}
		public void Write232_SaveStockNicknameInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write233_DeleteStockNickname(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("DeleteStockNickname", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[1]);
			}
			base.WriteEndElement();
		}
		public void Write234_DeleteStockNicknameInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write235_LoadStockNicknameExtra(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("LoadStockNicknameExtra", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custId", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write236_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write237_SaveStockNicknameExtra(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SaveStockNicknameExtra", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("oldStock", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementString("nickName", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementString("type", "http://tempuri.org/", (string)p[4]);
			}
			base.WriteEndElement();
		}
		public void Write238_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write239_DeleteStockNicknameExtra(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("DeleteStockNicknameExtra", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[1]);
			}
			base.WriteEndElement();
		}
		public void Write240_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write241_IntradayChart(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("IntradayChart", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("stockNumber", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementString("authenKey", "http://tempuri.org/", (string)p[1]);
			}
			base.WriteEndElement();
		}
		public void Write242_IntradayChartInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write243_IntradayIndexChart(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("IntradayIndexChart", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("symbol", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("authenKey", "http://tempuri.org/", (string)p[1]);
			}
			base.WriteEndElement();
		}
		public void Write244_IntradayIndexChartInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write245_GetChartImage(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetChartImage", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("stockNumber", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("showVolumn", "http://tempuri.org/", XmlConvert.ToString((bool)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("width", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("height", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			base.WriteEndElement();
		}
		public void Write246_GetChartImageInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write247_GetSetIndexChartImage(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetSetIndexChartImage", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("symbol", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("prior", "http://tempuri.org/", XmlConvert.ToString((double)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("width", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("height", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			base.WriteEndElement();
		}
		public void Write248_GetSetIndexChartImageInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write249_GetPortfolioStatAllStock(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetPortfolioStatAllStock", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("startDate", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("endDate", "http://tempuri.org/", (string)p[2]);
			}
			base.WriteEndElement();
		}
		public void Write250_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write251_GetPortfolioStatByStock(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetPortfolioStatByStock", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("startDate", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementString("endDate", "http://tempuri.org/", (string)p[3]);
			}
			base.WriteEndElement();
		}
		public void Write252_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write253_GetTunnelConfig(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("GetTunnelConfig", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write254_GetTunnelConfigInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write255_GetTunnel(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetTunnel", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("isRequestTfex", "http://tempuri.org/", XmlConvert.ToString((bool)p[0]));
			}
			base.WriteEndElement();
		}
		public void Write256_GetTunnelInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write257_VerifyOrder(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("VerifyOrder", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("side", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("volume", "http://tempuri.org/", XmlConvert.ToString((long)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementString("price", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[4]);
			}
			if (num > 5)
			{
				base.WriteElementStringRaw("isValidatePolicy", "http://tempuri.org/", XmlConvert.ToString((bool)p[5]));
			}
			base.WriteEndElement();
		}
		public void Write258_VerifyOrderInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write259_VerifyOrderFw(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("VerifyOrderFw", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("side", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("volume", "http://tempuri.org/", XmlConvert.ToString((long)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementString("price", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("pubVol", "http://tempuri.org/", XmlConvert.ToString((long)p[4]));
			}
			if (num > 5)
			{
				base.WriteElementString("condition", "http://tempuri.org/", (string)p[5]);
			}
			base.WriteEndElement();
		}
		public void Write260_VerifyOrderFwInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write261_VerifyOrderMkt(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("VerifyOrderMkt", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write262_VerifyOrderMktInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write263_GetMainInfo(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetMainInfo", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("userKey", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("localIp", "http://tempuri.org/", (string)p[2]);
			}
			base.WriteEndElement();
		}
		public void Write264_GetMainInfoInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write265_UserAuthen(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("UserAuthen", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("password", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("localIp", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementString("device", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementString("versionCode", "http://tempuri.org/", (string)p[4]);
			}
			if (num > 5)
			{
				base.WriteElementStringRaw("isGetMainInfo", "http://tempuri.org/", XmlConvert.ToString((bool)p[5]));
			}
			base.WriteEndElement();
		}
		public void Write266_UserAuthenInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write267_ClearEfinSession(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ClearEfinSession", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userefin", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write268_ClearEfinSessionInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write269_GetUrlClient(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("GetUrlClient", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write270_GetUrlClientInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write271_Logout(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("Logout", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("sessionID", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementString("loginMode", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("userLoginID", "http://tempuri.org/", (string)p[2]);
			}
			base.WriteEndElement();
		}
		public void Write272_LogoutInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write273_LogoutAD(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("LogoutAD", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("sessionID", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementString("loginMode", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("userLoginID", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementString("brokerParams", "http://tempuri.org/", (string)p[3]);
			}
			base.WriteEndElement();
		}
		public void Write274_LogoutADInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write275_ChangeCustomerPassword(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ChangeCustomerPassword", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custAccID", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("oldPassword", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("newPassword", "http://tempuri.org/", (string)p[2]);
			}
			base.WriteEndElement();
		}
		public void Write276_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write277_ChangeTraderPassword(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ChangeTraderPassword", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("traderID", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("oldPassword", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("newPassword", "http://tempuri.org/", (string)p[2]);
			}
			base.WriteEndElement();
		}
		public void Write278_ChangeTraderPasswordInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write279_StockThresholdInformation(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("StockThresholdInformation", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("loginID", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write280_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write281_SaveStockThreshold(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SaveStockThreshold", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("customerId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("items", "http://tempuri.org/", (string)p[1]);
			}
			base.WriteEndElement();
		}
		public void Write282_SaveStockThresholdInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write283_SendNewOrder(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SendNewOrder", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("side", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("volume", "http://tempuri.org/", XmlConvert.ToString((long)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementString("price", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[4]);
			}
			if (num > 5)
			{
				base.WriteElementStringRaw("publishVolume", "http://tempuri.org/", XmlConvert.ToString((long)p[5]));
			}
			if (num > 6)
			{
				base.WriteElementString("condition", "http://tempuri.org/", (string)p[6]);
			}
			if (num > 7)
			{
				base.WriteElementString("trusteeID", "http://tempuri.org/", (string)p[7]);
			}
			if (num > 8)
			{
				base.WriteElementString("entryID", "http://tempuri.org/", (string)p[8]);
			}
			if (num > 9)
			{
				base.WriteElementString("kimengSessionId", "http://tempuri.org/", (string)p[9]);
			}
			if (num > 10)
			{
				base.WriteElementString("kimengLocal", "http://tempuri.org/", (string)p[10]);
			}
			if (num > 11)
			{
				base.WriteElementString("deposit", "http://tempuri.org/", (string)p[11]);
			}
			if (num > 12)
			{
				base.WriteElementString("authenKey", "http://tempuri.org/", (string)p[12]);
			}
			if (num > 13)
			{
				base.WriteElementString("localIp", "http://tempuri.org/", (string)p[13]);
			}
			if (num > 14)
			{
				base.WriteElementString("pstWebIP", "http://tempuri.org/", (string)p[14]);
			}
			if (num > 15)
			{
				base.WriteElementString("pstIspIP", "http://tempuri.org/", (string)p[15]);
			}
			if (num > 16)
			{
				base.WriteElementString("terminalID", "http://tempuri.org/", (string)p[16]);
			}
			base.WriteEndElement();
		}
		public void Write284_SendNewOrderInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write285_SendNewOrderParams(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SendNewOrderParams", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("orderParams", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write286_SendNewOrderParamsInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write287_SendCancelOrder_AfterCloseFw(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SendCancelOrder_AfterCloseFw", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("orderDate", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("orderTime", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("orderNumber", "http://tempuri.org/", XmlConvert.ToString((long)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementString("endtryId", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementString("authenKey", "http://tempuri.org/", (string)p[4]);
			}
			base.WriteEndElement();
		}
		public void Write288_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write289_SendCancelOrder(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SendCancelOrder", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("sendDate", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("orderNumber", "http://tempuri.org/", XmlConvert.ToString((long)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementString("entryID", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementString("sessionId", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementString("authenKey", "http://tempuri.org/", (string)p[4]);
			}
			if (num > 5)
			{
				base.WriteElementString("internetUser", "http://tempuri.org/", (string)p[5]);
			}
			if (num > 6)
			{
				base.WriteElementString("terminalID", "http://tempuri.org/", (string)p[6]);
			}
			base.WriteEndElement();
		}
		public void Write290_SendCancelOrderInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write291_SendEditOrder(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SendEditOrder", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("authenKey", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("sessionId", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("orderNumber", "http://tempuri.org/", XmlConvert.ToString((long)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementString("entryDate", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("volume", "http://tempuri.org/", XmlConvert.ToString((long)p[4]));
			}
			if (num > 5)
			{
				base.WriteElementString("price", "http://tempuri.org/", (string)p[5]);
			}
			if (num > 6)
			{
				base.WriteElementString("logIn", "http://tempuri.org/", (string)p[6]);
			}
			if (num > 7)
			{
				base.WriteElementStringRaw("pubVol", "http://tempuri.org/", XmlConvert.ToString((long)p[7]));
			}
			if (num > 8)
			{
				base.WriteElementStringRaw("trusteeId", "http://tempuri.org/", XmlConvert.ToString((int)p[8]));
			}
			if (num > 9)
			{
				base.WriteElementString("userNo", "http://tempuri.org/", (string)p[9]);
			}
			base.WriteEndElement();
		}
		public void Write292_SendEditOrderInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write293_ViewNewsHeader(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewNewsHeader", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("isCurrentDate", "http://tempuri.org/", XmlConvert.ToString((bool)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementString("selectDate", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("selectStock", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("recordPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("page", "http://tempuri.org/", XmlConvert.ToString((int)p[4]));
			}
			base.WriteEndElement();
		}
		public void Write294_ViewNewsHeaderInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write295_ViewNewsStory(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewNewsStory", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("isCurrentDate", "http://tempuri.org/", XmlConvert.ToString((bool)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementString("selectDate", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("newNo", "http://tempuri.org/", XmlConvert.ToString((long)p[2]));
			}
			base.WriteEndElement();
		}
		public void Write296_ViewNewsStoryInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write297_ViewCustomersInfo(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomersInfo", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custAccID", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("userLoginID", "http://tempuri.org/", (string)p[1]);
			}
			base.WriteEndElement();
		}
		public void Write298_ViewCustomersInfoInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write299_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomerCreditOnSendBox_Freewill", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("accType", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[2]);
			}
			base.WriteEndElement();
		}
		public void Write300_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write301_ViewCustomerCreditOnSendBox(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomerCreditOnSendBox", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[1]);
			}
			base.WriteEndElement();
		}
		public void Write302_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write303_GetSwitchAccountInfo(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetSwitchAccountInfo", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write304_GetSwitchAccountInfoInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write305_GetSwitchAccountInfoEservice(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetSwitchAccountInfoEservice", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("brokerID", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("session", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementString("accountNo", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementString("system", "http://tempuri.org/", (string)p[4]);
			}
			if (num > 5)
			{
				base.WriteElementString("service", "http://tempuri.org/", (string)p[5]);
			}
			base.WriteEndElement();
		}
		public void Write306_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write307_ViewCustomer_MobileReportAll(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomer_MobileReportAll", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custAccID", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("stockSymbol", "http://tempuri.org/", (string)p[1]);
			}
			base.WriteEndElement();
		}
		public void Write308_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write309_ViewCustomer_OrdersConfirms(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomer_OrdersConfirms", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custAccID", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("stockSymbol", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("startRow", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			base.WriteEndElement();
		}
		public void Write310_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write311_ViewCustomer_CreditPosition(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomer_CreditPosition", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custAccID", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("stockSymbol", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("startRow", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			base.WriteEndElement();
		}
		public void Write312_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write313_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomer_ProjectedProfitLoss", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custAccID", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("stockSymbol", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("startRow", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			base.WriteEndElement();
		}
		public void Write314_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write315_ViewCustomer_RealizeProfitLoss(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomer_RealizeProfitLoss", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custAccID", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("stockSymbol", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("startRow", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			base.WriteEndElement();
		}
		public void Write316_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write317_ViewCustomer_Summary(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomer_Summary", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custAccID", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("stockSymbol", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("startRow", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			base.WriteEndElement();
		}
		public void Write318_ViewCustomer_SummaryInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write319_ViewCustomer_ConfirmSummary(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomer_ConfirmSummary", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custAccID", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("commGroup", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementString("stockSymbol", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("startRow", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[4]));
			}
			base.WriteEndElement();
		}
		public void Write320_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write321_ViewCustomer_Confirm(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomer_Confirm", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custAccID", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("commGroup", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementString("stockSymbol", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("startRow", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[4]));
			}
			base.WriteEndElement();
		}
		public void Write322_ViewCustomer_ConfirmInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write323_ViewCustomer_ConfirmByDealID(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomer_ConfirmByDealID", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custAccID", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("commGroup", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementString("stockSymbol", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("startRow", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[4]));
			}
			base.WriteEndElement();
		}
		public void Write324_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write325_ViewCustomer_ConfirmByStock(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomer_ConfirmByStock", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custAccID", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("commGroup", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementString("stockSymbol", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("startRow", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[4]));
			}
			base.WriteEndElement();
		}
		public void Write326_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write327_ViewOrderInfo_AfterCloseFw(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewOrderInfo_AfterCloseFw", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("orderNumber", "http://tempuri.org/", XmlConvert.ToString((long)p[0]));
			}
			base.WriteEndElement();
		}
		public void Write328_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write329_ViewOrderDealData(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewOrderDealData", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("sSendDate", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("orderNumber", "http://tempuri.org/", XmlConvert.ToString((long)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("startRow", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("dbType", "http://tempuri.org/", XmlConvert.ToString((int)p[4]));
			}
			base.WriteEndElement();
		}
		public void Write330_ViewOrderDealDataInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write331_ViewOrderDealDataHistory(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewOrderDealDataHistory", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("sSendDate", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("orderNumber", "http://tempuri.org/", XmlConvert.ToString((long)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("startRow", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			base.WriteEndElement();
		}
		public void Write332_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write333_GetCometInfo(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("GetCometInfo", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write334_GetCometInfoInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write335_TopActiveBBO_Sector(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("TopActiveBBO_Sector", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("sectorNumber", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			base.WriteEndElement();
		}
		public void Write336_TopActiveBBO_SectorInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write337_InvestorType(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("InvestorType", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("summaryType", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write338_InvestorTypeInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write339_MarketIndicator(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("MarketIndicator", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write340_MarketIndicatorInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write341_SaleByPrice(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SaleByPrice", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("securityNumber", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("startRow", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("maxTicker", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			base.WriteEndElement();
		}
		public void Write342_SaleByPriceInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write343_SectorStat(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("SectorStat", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write344_SectorStatInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write345_SectorStatForDump(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SectorStatForDump", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("industryNumber", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			base.WriteEndElement();
		}
		public void Write346_SectorStatForDumpInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write347_StockStatForDump(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("StockStatForDump", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("sectorNumber", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			base.WriteEndElement();
		}
		public void Write348_StockStatForDumpInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write349_GetUserConfigForDump(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetUserConfigForDump", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write350_GetUserConfigForDumpInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write351_SaveUserConfigForDump(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SaveUserConfigForDump", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("configName", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("configValue", "http://tempuri.org/", (string)p[2]);
			}
			base.WriteEndElement();
		}
		public void Write352_SaveUserConfigForDumpInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write353_SaleByTime2(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SaleByTime2", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("securityNumber", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("startRow", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("maxTicker", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementString("startTime", "http://tempuri.org/", (string)p[4]);
			}
			base.WriteEndElement();
		}
		public void Write354_SaleByTime2InHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write355_StockInPlay(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("StockInPlay", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("SecurityNumber", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("StartPrice", "http://tempuri.org/", XmlConvert.ToString((decimal)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementString("Side", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("maxTicker", "http://tempuri.org/", XmlConvert.ToString((int)p[4]));
			}
			base.WriteEndElement();
		}
		public void Write356_StockInPlayInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write357_StockByPricePage(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("StockByPricePage", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("stockNumber", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("maxTicker", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			base.WriteEndElement();
		}
		public void Write358_StockByPricePageInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write359_MarketStatus(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("MarketStatus", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write360_MarketStatusInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write361_TopBBO(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("TopBBO", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				int[] array = (int[])p[0];
				if (array != null)
				{
					base.WriteStartElement("stockNumber", "http://tempuri.org/", null, false);
					for (int i = 0; i < array.Length; i++)
					{
						base.WriteElementStringRaw("int", "http://tempuri.org/", XmlConvert.ToString(array[i]));
					}
					base.WriteEndElement();
				}
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("maxTicker", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			base.WriteEndElement();
		}
		public void Write362_TopBBOInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write363_TopBBOad(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("TopBBOad", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("stocks", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("maxTicker", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			base.WriteEndElement();
		}
		public void Write364_TopBBOadInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write365_ViewOddlot(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewOddlot", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("stockNo", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("maxTicker", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			base.WriteEndElement();
		}
		public void Write366_ViewOddlotInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write367_Get5BidOffer(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("Get5BidOffer", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("stockNumber", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			base.WriteEndElement();
		}
		public void Write368_Get5BidOfferInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write369_ViewOrderTransaction(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewOrderTransaction", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("id", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("senderType", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("readType", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[4]);
			}
			if (num > 5)
			{
				base.WriteElementString("side", "http://tempuri.org/", (string)p[5]);
			}
			if (num > 6)
			{
				base.WriteElementString("price", "http://tempuri.org/", (string)p[6]);
			}
			if (num > 7)
			{
				base.WriteElementString("status", "http://tempuri.org/", (string)p[7]);
			}
			if (num > 8)
			{
				base.WriteElementStringRaw("startOrderNo", "http://tempuri.org/", XmlConvert.ToString((long)p[8]));
			}
			if (num > 9)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[9]));
			}
			if (num > 10)
			{
				base.WriteElementStringRaw("expression", "http://tempuri.org/", XmlConvert.ToString((int)p[10]));
			}
			base.WriteEndElement();
		}
		public void Write370_ViewOrderTransactionInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write371_ViewOrderHistory(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewOrderHistory", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("side", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementString("selDate1", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementString("selDate2", "http://tempuri.org/", (string)p[4]);
			}
			if (num > 5)
			{
				base.WriteElementStringRaw("compareDays", "http://tempuri.org/", XmlConvert.ToString((int)p[5]));
			}
			base.WriteEndElement();
		}
		public void Write372_ViewOrderHistoryInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write373_ViewOrdersForDump(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewOrdersForDump", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("side", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementString("price", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementString("status", "http://tempuri.org/", (string)p[4]);
			}
			if (num > 5)
			{
				base.WriteElementStringRaw("startOrderNumber", "http://tempuri.org/", XmlConvert.ToString((long)p[5]));
			}
			if (num > 6)
			{
				base.WriteElementStringRaw("recordPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[6]));
			}
			if (num > 7)
			{
				base.WriteElementStringRaw("page", "http://tempuri.org/", XmlConvert.ToString((int)p[7]));
			}
			base.WriteEndElement();
		}
		public void Write374_ViewOrdersForDumpInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write375_ViewOrderByOrderNo(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewOrderByOrderNo", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("orderNoList", "http://tempuri.org/", (string)p[1]);
			}
			base.WriteEndElement();
		}
		public void Write376_ViewOrderByOrderNoInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write377_SaveUserConfigAll(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SaveUserConfigAll", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("value", "http://tempuri.org/", (string)p[1]);
			}
			base.WriteEndElement();
		}
		public void Write378_SaveUserConfigAllInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write379_SendHeartBeat(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SendHeartBeat", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userID", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("session", "http://tempuri.org/", (string)p[1]);
			}
			base.WriteEndElement();
		}
		public void Write380_SendHeartBeatInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write381_SendHeartBeat2(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SendHeartBeat2", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("param", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write382_SendHeartBeat2InHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write383_SaveUserEfinForward(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SaveUserEfinForward", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("fullname", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("nickname", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementString("regisDate", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementString("emailAccount", "http://tempuri.org/", (string)p[4]);
			}
			base.WriteEndElement();
		}
		public void Write384_SaveUserEfinForwardInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write385_LoadAllInformation(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("LoadAllInformation", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write386_LoadAllInformationInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write387_LoadAllInformationIsodd(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("LoadAllInformationIsodd", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("isLoadOddlot", "http://tempuri.org/", XmlConvert.ToString((bool)p[0]));
			}
			base.WriteEndElement();
		}
		public void Write388_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write389_LoadStockInformation(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("LoadStockInformation", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("startSecNo", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("topSelect", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			base.WriteEndElement();
		}
		public void Write390_LoadStockInformationInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write391_LoadMarketInformation(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("LoadMarketInformation", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write392_LoadMarketInformationInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write393_LoadOddLotInformation(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("LoadOddLotInformation", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write394_LoadOddLotInformationInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write395_BoardcastMessage(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("BoardcastMessage", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("recordPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			base.WriteEndElement();
		}
		public void Write396_BoardcastMessageInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write397_SendPushMessage(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SendPushMessage", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("key", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("groupId", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("message", "http://tempuri.org/", (string)p[2]);
			}
			base.WriteEndElement();
		}
		public void Write398_SendPushMessageInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write399_BestBidOffer(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("BestBidOffer", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("stockList", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write400_BestBidOfferInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write401_BestOpenPrice(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("BestOpenPrice", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("viewType", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("compareMode", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("marketID", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("sesssionID", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("startRow", "http://tempuri.org/", XmlConvert.ToString((int)p[4]));
			}
			if (num > 5)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[5]));
			}
			base.WriteEndElement();
		}
		public void Write402_BestOpenPriceInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write403_BestProjected(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("BestProjected", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("boardType", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("compareMode", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("poType", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementString("marketID", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("startRow", "http://tempuri.org/", XmlConvert.ToString((int)p[4]));
			}
			if (num > 5)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[5]));
			}
			base.WriteEndElement();
		}
		public void Write404_BestProjectedInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write405_IndustryStat(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("IndustryStat", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write406_IndustryStatInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write407_TopActive(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("TopActive", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("viewType", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("board", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("marketID", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			base.WriteEndElement();
		}
		public void Write408_TopActiveInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write409_TopActiveBBO(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("TopActiveBBO", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("viewType", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("board", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("marketID", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			base.WriteEndElement();
		}
		public void Write410_TopActiveBBOInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write411_TopActiveBBO_Benefit(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("TopActiveBBO_Benefit", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write412_TopActiveBBO_BenefitInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write413_TopActiveBBO_Warrant(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("TopActiveBBO_Warrant", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write414_TopActiveBBO_WarrantInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write415_TopActiveBBO_DW(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("TopActiveBBO_DW", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("parentStock", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write416_TopActiveBBO_DWInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write417_TopActiveBBO_News(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("TopActiveBBO_News", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write418_TopActiveBBO_NewsInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write419_TopActiveBBO_TurnOver(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("TopActiveBBO_TurnOver", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write420_TopActiveBBO_TurnOverInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write421_SavePushAccount(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SavePushAccount", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("accountlogin", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("registrationID", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("device", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementString("accList", "http://tempuri.org/", (string)p[3]);
			}
			base.WriteEndElement();
		}
		public void Write422_SavePushAccountInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write423_SaveStockAlert(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SaveStockAlert", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("field", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("operatorType", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementString("mobileAlert", "http://tempuri.org/", (string)p[4]);
			}
			if (num > 5)
			{
				base.WriteElementStringRaw("value", "http://tempuri.org/", XmlConvert.ToString((decimal)p[5]));
			}
			if (num > 6)
			{
				base.WriteElementStringRaw("updateMode", "http://tempuri.org/", XmlConvert.ToString((int)p[6]));
			}
			if (num > 7)
			{
				base.WriteElementString("memo", "http://tempuri.org/", (string)p[7]);
			}
			base.WriteEndElement();
		}
		public void Write424_SaveStockAlertInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write425_SavePortAlert(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SavePortAlert", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementString("sType", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("trusteeId", "http://tempuri.org/", XmlConvert.ToString((int)p[4]));
			}
			if (num > 5)
			{
				base.WriteElementStringRaw("costPct", "http://tempuri.org/", XmlConvert.ToString((decimal)p[5]));
			}
			if (num > 6)
			{
				base.WriteElementStringRaw("pricePct", "http://tempuri.org/", XmlConvert.ToString((decimal)p[6]));
			}
			if (num > 7)
			{
				base.WriteElementStringRaw("sellPct", "http://tempuri.org/", XmlConvert.ToString((decimal)p[7]));
			}
			base.WriteEndElement();
		}
		public void Write426_SavePortAlertInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write427_SendStopOrder(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SendStopOrder", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("stockNumber", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("fieldType", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("operatorType", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("price", "http://tempuri.org/", XmlConvert.ToString((decimal)p[4]));
			}
			if (num > 5)
			{
				base.WriteElementString("ordAccount", "http://tempuri.org/", (string)p[5]);
			}
			if (num > 6)
			{
				base.WriteElementString("ordSide", "http://tempuri.org/", (string)p[6]);
			}
			if (num > 7)
			{
				base.WriteElementStringRaw("ordTtf", "http://tempuri.org/", XmlConvert.ToString((int)p[7]));
			}
			if (num > 8)
			{
				base.WriteElementStringRaw("ordVolume", "http://tempuri.org/", XmlConvert.ToString((long)p[8]));
			}
			if (num > 9)
			{
				base.WriteElementString("ordPrice", "http://tempuri.org/", (string)p[9]);
			}
			if (num > 10)
			{
				base.WriteElementStringRaw("ordPublish", "http://tempuri.org/", XmlConvert.ToString((long)p[10]));
			}
			if (num > 11)
			{
				base.WriteElementString("ordCondition", "http://tempuri.org/", (string)p[11]);
			}
			if (num > 12)
			{
				base.WriteElementString("ordEntryId", "http://tempuri.org/", (string)p[12]);
			}
			if (num > 13)
			{
				base.WriteElementString("kimengSession", "http://tempuri.org/", (string)p[13]);
			}
			if (num > 14)
			{
				base.WriteElementString("kimengLocal", "http://tempuri.org/", (string)p[14]);
			}
			if (num > 15)
			{
				base.WriteElementStringRaw("limit", "http://tempuri.org/", XmlConvert.ToString((int)p[15]));
			}
			if (num > 16)
			{
				base.WriteElementString("authenKey", "http://tempuri.org/", (string)p[16]);
			}
			base.WriteEndElement();
		}
		public void Write428_SendStopOrderInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write429_ViewStopOrder(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewStopOrder", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("refNo", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			base.WriteEndElement();
		}
		public void Write430_ViewStopOrderInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write431_ViewStopOrder_FirstLS(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewStopOrder_FirstLS", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("stockNo", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementString("condition", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("time", "http://tempuri.org/", (string)p[2]);
			}
			base.WriteEndElement();
		}
		public void Write432_ViewStopOrder_FirstLSInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write433_SendCancelStopOrder(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SendCancelStopOrder", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("refNo", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("stockNo", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementString("authenKey", "http://tempuri.org/", (string)p[3]);
			}
			base.WriteEndElement();
		}
		public void Write434_SendCancelStopOrderInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write435_StopOrderRegister(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("StopOrderRegister", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("isRegister", "http://tempuri.org/", XmlConvert.ToString((bool)p[1]));
			}
			base.WriteEndElement();
		}
		public void Write436_StopOrderRegisterInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write437_StopOrderCheckDisclaimer(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("StopOrderCheckDisclaimer", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write438_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write439_SaveUserAlert(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SaveUserAlert", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("isRecvAdvertise", "http://tempuri.org/", XmlConvert.ToString((bool)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("isMktSummary", "http://tempuri.org/", XmlConvert.ToString((bool)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("isRecvPort", "http://tempuri.org/", XmlConvert.ToString((bool)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementString("device", "http://tempuri.org/", (string)p[4]);
			}
			if (num > 5)
			{
				base.WriteElementStringRaw("mode", "http://tempuri.org/", XmlConvert.ToString((int)p[5]));
			}
			base.WriteEndElement();
		}
		public void Write440_SaveUserAlertInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write441_SaveUserAlert2(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SaveUserAlert2", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("isRecvAdvertise", "http://tempuri.org/", XmlConvert.ToString((bool)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("isMktSummary", "http://tempuri.org/", XmlConvert.ToString((bool)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("isRecvPort", "http://tempuri.org/", XmlConvert.ToString((bool)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementString("lstActiveGroup", "http://tempuri.org/", (string)p[4]);
			}
			base.WriteEndElement();
		}
		public void Write442_SaveUserAlert2InHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write443_GetAlertLog(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetAlertLog", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write444_GetAlertLogInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write445_GetUserAlert(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetUserAlert", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write446_GetUserAlertInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write447_GetStockAlertItems(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetStockAlertItems", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write448_GetStockAlertItemsInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write449_GetPortAlertItems(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetPortAlertItems", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write450_GetPortAlertItemsInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write451_NAVChart(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("NAVChart", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("startDate", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("endDate", "http://tempuri.org/", (string)p[2]);
			}
			base.WriteEndElement();
		}
		public void Write452_NAVChartInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write453_GetDataForSmartClick(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetDataForSmartClick", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[1]);
			}
			base.WriteEndElement();
		}
		public void Write454_GetDataForSmartClickInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write455_GetOrderFor1Click(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetOrderFor1Click", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("price", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementString("side", "http://tempuri.org/", (string)p[3]);
			}
			base.WriteEndElement();
		}
		public void Write456_GetOrderFor1ClickInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write457_SaveSummaryMarketData(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SaveSummaryMarketData", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("date", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("sumBy", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("investor", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("buyValue", "http://tempuri.org/", XmlConvert.ToString((decimal)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("pctBuyValue", "http://tempuri.org/", XmlConvert.ToString((decimal)p[4]));
			}
			if (num > 5)
			{
				base.WriteElementStringRaw("sellValue", "http://tempuri.org/", XmlConvert.ToString((decimal)p[5]));
			}
			if (num > 6)
			{
				base.WriteElementStringRaw("pctSellValue", "http://tempuri.org/", XmlConvert.ToString((decimal)p[6]));
			}
			if (num > 7)
			{
				base.WriteElementStringRaw("sumValue", "http://tempuri.org/", XmlConvert.ToString((decimal)p[7]));
			}
			if (num > 8)
			{
				base.WriteElementStringRaw("pctSumValue", "http://tempuri.org/", XmlConvert.ToString((decimal)p[8]));
			}
			base.WriteEndElement();
		}
		public void Write458_SaveSummaryMarketDataInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write459_GetStockSMA(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetStockSMA", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("period", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			base.WriteEndElement();
		}
		public void Write460_GetStockSMAInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write461_SaveUserConfig(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SaveUserConfig", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("keyValue", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("value", "http://tempuri.org/", (string)p[2]);
			}
			base.WriteEndElement();
		}
		public void Write462_SaveUserConfigInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write463_CountOrderCancelForDump(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("CountOrderCancelForDump", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("trusteeId", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementString("side", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementString("price", "http://tempuri.org/", (string)p[4]);
			}
			if (num > 5)
			{
				base.WriteElementStringRaw("startOrderNo", "http://tempuri.org/", XmlConvert.ToString((long)p[5]));
			}
			if (num > 6)
			{
				base.WriteElementStringRaw("endOrderNo", "http://tempuri.org/", XmlConvert.ToString((long)p[6]));
			}
			base.WriteEndElement();
		}
		public void Write464_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write465_ViewCustomerByStockForDump(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomerByStockForDump", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("trusteeId", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			base.WriteEndElement();
		}
		public void Write466_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write467_GetBrokerMarginVolume(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetBrokerMarginVolume", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write468_GetBrokerMarginVolumeInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write469_VerifyPincode(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("VerifyPincode", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("pincode", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("sessionid", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementString("ktzCustMapKey", "http://tempuri.org/", (string)p[3]);
			}
			base.WriteEndElement();
		}
		public void Write470_VerifyPincodeInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write471_VerifyPincode2(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("VerifyPincode2", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("userId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("pincode", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("sessionid", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementString("ktzCustMapKey", "http://tempuri.org/", (string)p[3]);
			}
			base.WriteEndElement();
		}
		public void Write472_VerifyPincode2InHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write473_SendBAMessage(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SendBAMessage", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("message", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write474_SendBAMessageInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write475_StockHistory(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("StockHistory", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("sDate", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("isPageNext", "http://tempuri.org/", XmlConvert.ToString((bool)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("recordPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			base.WriteEndElement();
		}
		public void Write476_StockHistoryInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write477_StockChart(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("StockChart", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("stockNo", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("recordPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			base.WriteEndElement();
		}
		public void Write478_StockChartInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write479_StockHistData(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("StockHistData", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("symbol", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("startDate", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("key", "http://tempuri.org/", (string)p[2]);
			}
			base.WriteEndElement();
		}
		public void Write480_StockHistDataInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write481_LoadStockNickname(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("LoadStockNickname", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custId", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write482_LoadStockNicknameInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write483_SaveStockNickname(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SaveStockNickname", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("oldStock", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementString("nickName", "http://tempuri.org/", (string)p[3]);
			}
			base.WriteEndElement();
		}
		public void Write484_SaveStockNicknameInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write485_DeleteStockNickname(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("DeleteStockNickname", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[1]);
			}
			base.WriteEndElement();
		}
		public void Write486_DeleteStockNicknameInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write487_LoadStockNicknameExtra(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("LoadStockNicknameExtra", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custId", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write488_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write489_SaveStockNicknameExtra(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SaveStockNicknameExtra", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("oldStock", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementString("nickName", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementString("type", "http://tempuri.org/", (string)p[4]);
			}
			base.WriteEndElement();
		}
		public void Write490_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write491_DeleteStockNicknameExtra(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("DeleteStockNicknameExtra", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[1]);
			}
			base.WriteEndElement();
		}
		public void Write492_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write493_IntradayChart(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("IntradayChart", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("stockNumber", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementString("authenKey", "http://tempuri.org/", (string)p[1]);
			}
			base.WriteEndElement();
		}
		public void Write494_IntradayChartInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write495_IntradayIndexChart(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("IntradayIndexChart", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("symbol", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("authenKey", "http://tempuri.org/", (string)p[1]);
			}
			base.WriteEndElement();
		}
		public void Write496_IntradayIndexChartInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write497_GetChartImage(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetChartImage", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("stockNumber", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("showVolumn", "http://tempuri.org/", XmlConvert.ToString((bool)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("width", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("height", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			base.WriteEndElement();
		}
		public void Write498_GetChartImageInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write499_GetSetIndexChartImage(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetSetIndexChartImage", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("symbol", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("prior", "http://tempuri.org/", XmlConvert.ToString((double)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("width", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("height", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			base.WriteEndElement();
		}
		public void Write500_GetSetIndexChartImageInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write501_GetPortfolioStatAllStock(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetPortfolioStatAllStock", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("startDate", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("endDate", "http://tempuri.org/", (string)p[2]);
			}
			base.WriteEndElement();
		}
		public void Write502_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write503_GetPortfolioStatByStock(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetPortfolioStatByStock", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custId", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("startDate", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementString("endDate", "http://tempuri.org/", (string)p[3]);
			}
			base.WriteEndElement();
		}
		public void Write504_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write505_ViewCustomersCredit(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomersCredit", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custAccID", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write506_ViewCustomersCreditInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write507_ViewCustomersInfo(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomersInfo", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custAccID", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write508_ViewCustomersInfoInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write509_ViewCustomersAll(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomersAll", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custAccID", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write510_ViewCustomersAllInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write511_ViewOrderDealData(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewOrderDealData", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("orderNumber", "http://tempuri.org/", XmlConvert.ToString((long)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("startRow", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("dbType", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementString("ordType", "http://tempuri.org/", (string)p[4]);
			}
			if (num > 5)
			{
				base.WriteElementString("sendDate", "http://tempuri.org/", (string)p[5]);
			}
			base.WriteEndElement();
		}
		public void Write512_ViewOrderDealDataInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write513_ViewCustomerCreditOnSendBox(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomerCreditOnSendBox", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[1]);
			}
			base.WriteEndElement();
		}
		public void Write514_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write515_SendTFEXNewOrder(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SendTFEXNewOrder", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("series", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("side", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("volume", "http://tempuri.org/", XmlConvert.ToString((long)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementString("price", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[4]);
			}
			if (num > 5)
			{
				base.WriteElementStringRaw("publishVolume", "http://tempuri.org/", XmlConvert.ToString((long)p[5]));
			}
			if (num > 6)
			{
				base.WriteElementString("position", "http://tempuri.org/", (string)p[6]);
			}
			if (num > 7)
			{
				base.WriteElementString("stopPrice", "http://tempuri.org/", (string)p[7]);
			}
			if (num > 8)
			{
				base.WriteElementString("stopCond", "http://tempuri.org/", (string)p[8]);
			}
			if (num > 9)
			{
				base.WriteElementString("stopSeries", "http://tempuri.org/", (string)p[9]);
			}
			if (num > 10)
			{
				base.WriteElementString("Validity", "http://tempuri.org/", (string)p[10]);
			}
			if (num > 11)
			{
				base.WriteElementString("ValidityDate", "http://tempuri.org/", (string)p[11]);
			}
			if (num > 12)
			{
				base.WriteElementString("sessionID", "http://tempuri.org/", (string)p[12]);
			}
			if (num > 13)
			{
				base.WriteElementString("requstID", "http://tempuri.org/", (string)p[13]);
			}
			if (num > 14)
			{
				base.WriteElementString("pinCode", "http://tempuri.org/", (string)p[14]);
			}
			if (num > 15)
			{
				base.WriteElementString("authenKey", "http://tempuri.org/", (string)p[15]);
			}
			if (num > 16)
			{
				base.WriteElementString("TraderID", "http://tempuri.org/", (string)p[16]);
			}
			if (num > 17)
			{
				base.WriteElementString("localIp", "http://tempuri.org/", (string)p[17]);
			}
			if (num > 18)
			{
				base.WriteElementString("kimengSession", "http://tempuri.org/", (string)p[18]);
			}
			if (num > 19)
			{
				base.WriteElementString("kimengLocal", "http://tempuri.org/", (string)p[19]);
			}
			if (num > 20)
			{
				base.WriteElementString("AppSymbol", "http://tempuri.org/", (string)p[20]);
			}
			if (num > 21)
			{
				base.WriteElementString("priceType", "http://tempuri.org/", (string)p[21]);
			}
			base.WriteEndElement();
		}
		public void Write516_SendTFEXNewOrderInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write517_SendTFEXCancelOrder(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SendTFEXCancelOrder", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("orderNumber", "http://tempuri.org/", XmlConvert.ToString((long)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementString("entryID", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("sendDate", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementString("internetUser", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementString("authenKey", "http://tempuri.org/", (string)p[4]);
			}
			if (num > 5)
			{
				base.WriteElementString("localIp", "http://tempuri.org/", (string)p[5]);
			}
			if (num > 6)
			{
				base.WriteElementString("kimengSession", "http://tempuri.org/", (string)p[6]);
			}
			if (num > 7)
			{
				base.WriteElementString("AppSymbol", "http://tempuri.org/", (string)p[7]);
			}
			base.WriteEndElement();
		}
		public void Write518_SendTFEXCancelOrderInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write519_WriteLog(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("WriteLog", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("log", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write520_WriteLogInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write521_GetGoldSpot(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("GetGoldSpot", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write522_GetGoldSpotInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write523_LoadTFEXInformation(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("LoadTFEXInformation", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("orderBookId", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("topSelect", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			base.WriteEndElement();
		}
		public void Write524_LoadTFEXInformationInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write525_SeriesState(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("SeriesState", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write526_SeriesStateInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write527_GetTotalMarketValueInfo(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("GetTotalMarketValueInfo", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write528_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write529_SeriesByPricePage(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SeriesByPricePage", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("seriesInfo", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("seriesType", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("maxTicker", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			base.WriteEndElement();
		}
		public void Write530_SeriesByPricePageInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write531_TopBBOTFEX(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("TopBBOTFEX", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				string[] array = (string[])p[0];
				if (array != null)
				{
					base.WriteStartElement("seriesList", "http://tempuri.org/", null, false);
					for (int i = 0; i < array.Length; i++)
					{
						base.WriteNullableStringLiteral("string", "http://tempuri.org/", array[i]);
					}
					base.WriteEndElement();
				}
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("maxTicker", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			base.WriteEndElement();
		}
		public void Write532_TopBBOTFEXInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write533_TopBBOTFEXad(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("TopBBOTFEXad", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("series", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("maxTicker", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			base.WriteEndElement();
		}
		public void Write534_TopBBOTFEXadInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write535_TFEXTopActiveBBO(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("TFEXTopActiveBBO", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("viewType", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("isFuture", "http://tempuri.org/", XmlConvert.ToString((bool)p[1]));
			}
			base.WriteEndElement();
		}
		public void Write536_TFEXTopActiveBBOInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write537_BestProjected_TFEX(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("BestProjected_TFEX", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("isFutures", "http://tempuri.org/", XmlConvert.ToString((bool)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementString("viewType", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			base.WriteEndElement();
		}
		public void Write538_BestProjected_TFEXInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write539_BestBidOfferByList(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("BestBidOfferByList", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("seriesListName", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write540_BestBidOfferByListInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write541_BestBidOfferByInstrument(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("BestBidOfferByInstrument", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("orderBookID", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			base.WriteEndElement();
		}
		public void Write542_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write543_BestBidOfferByOptionsList(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("BestBidOfferByOptionsList", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("expDate", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write544_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write545_Get5BidOfferTFEX(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("Get5BidOfferTFEX", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("series", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write546_Get5BidOfferTFEXInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write547_GetChartImage(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetChartImage", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("seriesName", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("showVolumn", "http://tempuri.org/", XmlConvert.ToString((bool)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("width", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("height", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("marketCode", "http://tempuri.org/", XmlConvert.ToString((int)p[4]));
			}
			base.WriteEndElement();
		}
		public void Write548_GetChartImageInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write549_GetSwitchAccountInfoTFEX(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetSwitchAccountInfoTFEX", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write550_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write551_SeriesSaleByTime(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SeriesSaleByTime", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("seriesName", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("seriesType", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("pageNo", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("recordPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("maxTicker", "http://tempuri.org/", XmlConvert.ToString((int)p[4]));
			}
			if (num > 5)
			{
				base.WriteElementString("timeSearch", "http://tempuri.org/", (string)p[5]);
			}
			base.WriteEndElement();
		}
		public void Write552_SeriesSaleByTimeInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write553_SeriesSaleByPrice(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SeriesSaleByPrice", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("seriesName", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("seriesType", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("maxTicker", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			base.WriteEndElement();
		}
		public void Write554_SeriesSaleByPriceInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write555_StockInPlay(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("StockInPlay", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("seriesName", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("seriesType", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("tickSize", "http://tempuri.org/", XmlConvert.ToString((decimal)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("TFEXSession", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("StartPrice", "http://tempuri.org/", XmlConvert.ToString((decimal)p[4]));
			}
			if (num > 5)
			{
				base.WriteElementString("Side", "http://tempuri.org/", (string)p[5]);
			}
			if (num > 6)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[6]));
			}
			if (num > 7)
			{
				base.WriteElementStringRaw("maxTicker", "http://tempuri.org/", XmlConvert.ToString((int)p[7]));
			}
			base.WriteEndElement();
		}
		public void Write556_StockInPlayInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write557_StockInPlayAD(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("StockInPlayAD", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("seriesName", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("seriesType", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("tickSize", "http://tempuri.org/", XmlConvert.ToString((decimal)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("TFEXSession", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("StartPrice", "http://tempuri.org/", XmlConvert.ToString((decimal)p[4]));
			}
			if (num > 5)
			{
				base.WriteElementString("Side", "http://tempuri.org/", (string)p[5]);
			}
			if (num > 6)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[6]));
			}
			if (num > 7)
			{
				base.WriteElementStringRaw("maxTicker", "http://tempuri.org/", XmlConvert.ToString((int)p[7]));
			}
			base.WriteEndElement();
		}
		public void Write558_StockInPlayADInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write559_SeriesSumary(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SeriesSumary", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("seriesName", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("seriesType", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("pageNo", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("maxTicker", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementString("timeSearch", "http://tempuri.org/", (string)p[4]);
			}
			base.WriteEndElement();
		}
		public void Write560_SeriesSumaryInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write561_TFEXBoardcastMessage(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("TFEXBoardcastMessage", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("recordPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			base.WriteEndElement();
		}
		public void Write562_TFEXBoardcastMessageInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write563_ViewOrderTransaction(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewOrderTransaction", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("id", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("senderType", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("readType", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[4]);
			}
			if (num > 5)
			{
				base.WriteElementString("side", "http://tempuri.org/", (string)p[5]);
			}
			if (num > 6)
			{
				base.WriteElementString("price", "http://tempuri.org/", (string)p[6]);
			}
			if (num > 7)
			{
				base.WriteElementString("status", "http://tempuri.org/", (string)p[7]);
			}
			if (num > 8)
			{
				base.WriteElementStringRaw("startOrderNo", "http://tempuri.org/", XmlConvert.ToString((long)p[8]));
			}
			if (num > 9)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[9]));
			}
			if (num > 10)
			{
				base.WriteElementStringRaw("expression", "http://tempuri.org/", XmlConvert.ToString((int)p[10]));
			}
			base.WriteEndElement();
		}
		public void Write564_ViewOrderTransactionInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write565_ViewOrderByOrderNo(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewOrderByOrderNo", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("orderNo", "http://tempuri.org/", (string)p[1]);
			}
			base.WriteEndElement();
		}
		public void Write566_ViewOrderByOrderNoInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write567_LoadSETindex(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("LoadSETindex", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write568_LoadSETindexInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write569_LoadMktStatus(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("LoadMktStatus", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write570_LoadMktStatusInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write571_TFEXInformation(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("TFEXInformation", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write572_TFEXInformationInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write573_ViewCustomersCredit(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomersCredit", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custAccID", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write574_ViewCustomersCreditInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write575_ViewCustomersInfo(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomersInfo", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custAccID", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write576_ViewCustomersInfoInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write577_ViewCustomersAll(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomersAll", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("custAccID", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write578_ViewCustomersAllInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write579_ViewOrderDealData(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewOrderDealData", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("orderNumber", "http://tempuri.org/", XmlConvert.ToString((long)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("startRow", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("dbType", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementString("ordType", "http://tempuri.org/", (string)p[4]);
			}
			if (num > 5)
			{
				base.WriteElementString("sendDate", "http://tempuri.org/", (string)p[5]);
			}
			base.WriteEndElement();
		}
		public void Write580_ViewOrderDealDataInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write581_ViewCustomerCreditOnSendBox(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewCustomerCreditOnSendBox", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[1]);
			}
			base.WriteEndElement();
		}
		public void Write582_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write583_SendTFEXNewOrder(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SendTFEXNewOrder", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("series", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("side", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("volume", "http://tempuri.org/", XmlConvert.ToString((long)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementString("price", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[4]);
			}
			if (num > 5)
			{
				base.WriteElementStringRaw("publishVolume", "http://tempuri.org/", XmlConvert.ToString((long)p[5]));
			}
			if (num > 6)
			{
				base.WriteElementString("position", "http://tempuri.org/", (string)p[6]);
			}
			if (num > 7)
			{
				base.WriteElementString("stopPrice", "http://tempuri.org/", (string)p[7]);
			}
			if (num > 8)
			{
				base.WriteElementString("stopCond", "http://tempuri.org/", (string)p[8]);
			}
			if (num > 9)
			{
				base.WriteElementString("stopSeries", "http://tempuri.org/", (string)p[9]);
			}
			if (num > 10)
			{
				base.WriteElementString("Validity", "http://tempuri.org/", (string)p[10]);
			}
			if (num > 11)
			{
				base.WriteElementString("ValidityDate", "http://tempuri.org/", (string)p[11]);
			}
			if (num > 12)
			{
				base.WriteElementString("sessionID", "http://tempuri.org/", (string)p[12]);
			}
			if (num > 13)
			{
				base.WriteElementString("requstID", "http://tempuri.org/", (string)p[13]);
			}
			if (num > 14)
			{
				base.WriteElementString("pinCode", "http://tempuri.org/", (string)p[14]);
			}
			if (num > 15)
			{
				base.WriteElementString("authenKey", "http://tempuri.org/", (string)p[15]);
			}
			if (num > 16)
			{
				base.WriteElementString("TraderID", "http://tempuri.org/", (string)p[16]);
			}
			if (num > 17)
			{
				base.WriteElementString("localIp", "http://tempuri.org/", (string)p[17]);
			}
			if (num > 18)
			{
				base.WriteElementString("kimengSession", "http://tempuri.org/", (string)p[18]);
			}
			if (num > 19)
			{
				base.WriteElementString("kimengLocal", "http://tempuri.org/", (string)p[19]);
			}
			if (num > 20)
			{
				base.WriteElementString("AppSymbol", "http://tempuri.org/", (string)p[20]);
			}
			if (num > 21)
			{
				base.WriteElementString("priceType", "http://tempuri.org/", (string)p[21]);
			}
			base.WriteEndElement();
		}
		public void Write584_SendTFEXNewOrderInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write585_SendTFEXCancelOrder(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SendTFEXCancelOrder", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("orderNumber", "http://tempuri.org/", XmlConvert.ToString((long)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementString("entryID", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementString("sendDate", "http://tempuri.org/", (string)p[2]);
			}
			if (num > 3)
			{
				base.WriteElementString("internetUser", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementString("authenKey", "http://tempuri.org/", (string)p[4]);
			}
			if (num > 5)
			{
				base.WriteElementString("localIp", "http://tempuri.org/", (string)p[5]);
			}
			if (num > 6)
			{
				base.WriteElementString("kimengSession", "http://tempuri.org/", (string)p[6]);
			}
			if (num > 7)
			{
				base.WriteElementString("AppSymbol", "http://tempuri.org/", (string)p[7]);
			}
			base.WriteEndElement();
		}
		public void Write586_SendTFEXCancelOrderInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write587_WriteLog(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("WriteLog", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("log", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write588_WriteLogInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write589_GetGoldSpot(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("GetGoldSpot", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write590_GetGoldSpotInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write591_LoadTFEXInformation(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("LoadTFEXInformation", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("orderBookId", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("topSelect", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			base.WriteEndElement();
		}
		public void Write592_LoadTFEXInformationInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write593_SeriesState(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("SeriesState", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write594_SeriesStateInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write595_GetTotalMarketValueInfo(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("GetTotalMarketValueInfo", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write596_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write597_SeriesByPricePage(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SeriesByPricePage", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("seriesInfo", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("seriesType", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("maxTicker", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			base.WriteEndElement();
		}
		public void Write598_SeriesByPricePageInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write599_TopBBOTFEX(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("TopBBOTFEX", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				string[] array = (string[])p[0];
				if (array != null)
				{
					base.WriteStartElement("seriesList", "http://tempuri.org/", null, false);
					for (int i = 0; i < array.Length; i++)
					{
						base.WriteNullableStringLiteral("string", "http://tempuri.org/", array[i]);
					}
					base.WriteEndElement();
				}
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("maxTicker", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			base.WriteEndElement();
		}
		public void Write600_TopBBOTFEXInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write601_TopBBOTFEXad(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("TopBBOTFEXad", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("series", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("maxTicker", "http://tempuri.org/", XmlConvert.ToString((int)p[1]));
			}
			base.WriteEndElement();
		}
		public void Write602_TopBBOTFEXadInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write603_TFEXTopActiveBBO(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("TFEXTopActiveBBO", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("viewType", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("isFuture", "http://tempuri.org/", XmlConvert.ToString((bool)p[1]));
			}
			base.WriteEndElement();
		}
		public void Write604_TFEXTopActiveBBOInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write605_BestProjected_TFEX(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("BestProjected_TFEX", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("isFutures", "http://tempuri.org/", XmlConvert.ToString((bool)p[0]));
			}
			if (num > 1)
			{
				base.WriteElementString("viewType", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			base.WriteEndElement();
		}
		public void Write606_BestProjected_TFEXInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write607_BestBidOfferByList(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("BestBidOfferByList", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("seriesListName", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write608_BestBidOfferByListInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write609_BestBidOfferByInstrument(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("BestBidOfferByInstrument", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("orderBookID", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			base.WriteEndElement();
		}
		public void Write610_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write611_BestBidOfferByOptionsList(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("BestBidOfferByOptionsList", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("expDate", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write612_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write613_Get5BidOfferTFEX(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("Get5BidOfferTFEX", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("series", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write614_Get5BidOfferTFEXInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write615_GetChartImage(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetChartImage", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("seriesName", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementStringRaw("showVolumn", "http://tempuri.org/", XmlConvert.ToString((bool)p[1]));
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("width", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("height", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("marketCode", "http://tempuri.org/", XmlConvert.ToString((int)p[4]));
			}
			base.WriteEndElement();
		}
		public void Write616_GetChartImageInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write617_GetSwitchAccountInfoTFEX(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("GetSwitchAccountInfoTFEX", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[0]);
			}
			base.WriteEndElement();
		}
		public void Write618_Item(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write619_SeriesSaleByTime(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SeriesSaleByTime", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("seriesName", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("seriesType", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("pageNo", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("recordPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("maxTicker", "http://tempuri.org/", XmlConvert.ToString((int)p[4]));
			}
			if (num > 5)
			{
				base.WriteElementString("timeSearch", "http://tempuri.org/", (string)p[5]);
			}
			base.WriteEndElement();
		}
		public void Write620_SeriesSaleByTimeInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write621_SeriesSaleByPrice(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SeriesSaleByPrice", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("seriesName", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("seriesType", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("maxTicker", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			base.WriteEndElement();
		}
		public void Write622_SeriesSaleByPriceInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write623_StockInPlay(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("StockInPlay", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("seriesName", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("seriesType", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("tickSize", "http://tempuri.org/", XmlConvert.ToString((decimal)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("TFEXSession", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("StartPrice", "http://tempuri.org/", XmlConvert.ToString((decimal)p[4]));
			}
			if (num > 5)
			{
				base.WriteElementString("Side", "http://tempuri.org/", (string)p[5]);
			}
			if (num > 6)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[6]));
			}
			if (num > 7)
			{
				base.WriteElementStringRaw("maxTicker", "http://tempuri.org/", XmlConvert.ToString((int)p[7]));
			}
			base.WriteEndElement();
		}
		public void Write624_StockInPlayInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write625_StockInPlayAD(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("StockInPlayAD", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("seriesName", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("seriesType", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("tickSize", "http://tempuri.org/", XmlConvert.ToString((decimal)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("TFEXSession", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementStringRaw("StartPrice", "http://tempuri.org/", XmlConvert.ToString((decimal)p[4]));
			}
			if (num > 5)
			{
				base.WriteElementString("Side", "http://tempuri.org/", (string)p[5]);
			}
			if (num > 6)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[6]));
			}
			if (num > 7)
			{
				base.WriteElementStringRaw("maxTicker", "http://tempuri.org/", XmlConvert.ToString((int)p[7]));
			}
			base.WriteEndElement();
		}
		public void Write626_StockInPlayADInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write627_SeriesSumary(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("SeriesSumary", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("seriesName", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("seriesType", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("pageNo", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementStringRaw("maxTicker", "http://tempuri.org/", XmlConvert.ToString((int)p[3]));
			}
			if (num > 4)
			{
				base.WriteElementString("timeSearch", "http://tempuri.org/", (string)p[4]);
			}
			base.WriteEndElement();
		}
		public void Write628_SeriesSumaryInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write629_TFEXBoardcastMessage(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("TFEXBoardcastMessage", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementStringRaw("recordPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[0]));
			}
			base.WriteEndElement();
		}
		public void Write630_TFEXBoardcastMessageInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write631_ViewOrderTransaction(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewOrderTransaction", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("id", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("senderType", "http://tempuri.org/", (string)p[1]);
			}
			if (num > 2)
			{
				base.WriteElementStringRaw("readType", "http://tempuri.org/", XmlConvert.ToString((int)p[2]));
			}
			if (num > 3)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[3]);
			}
			if (num > 4)
			{
				base.WriteElementString("stock", "http://tempuri.org/", (string)p[4]);
			}
			if (num > 5)
			{
				base.WriteElementString("side", "http://tempuri.org/", (string)p[5]);
			}
			if (num > 6)
			{
				base.WriteElementString("price", "http://tempuri.org/", (string)p[6]);
			}
			if (num > 7)
			{
				base.WriteElementString("status", "http://tempuri.org/", (string)p[7]);
			}
			if (num > 8)
			{
				base.WriteElementStringRaw("startOrderNo", "http://tempuri.org/", XmlConvert.ToString((long)p[8]));
			}
			if (num > 9)
			{
				base.WriteElementStringRaw("rowPerPage", "http://tempuri.org/", XmlConvert.ToString((int)p[9]));
			}
			if (num > 10)
			{
				base.WriteElementStringRaw("expression", "http://tempuri.org/", XmlConvert.ToString((int)p[10]));
			}
			base.WriteEndElement();
		}
		public void Write632_ViewOrderTransactionInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write633_ViewOrderByOrderNo(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int num = p.Length;
			base.WriteStartElement("ViewOrderByOrderNo", "http://tempuri.org/", null, false);
			if (num > 0)
			{
				base.WriteElementString("account", "http://tempuri.org/", (string)p[0]);
			}
			if (num > 1)
			{
				base.WriteElementString("orderNo", "http://tempuri.org/", (string)p[1]);
			}
			base.WriteEndElement();
		}
		public void Write634_ViewOrderByOrderNoInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write635_LoadSETindex(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("LoadSETindex", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write636_LoadSETindexInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write637_LoadMktStatus(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("LoadMktStatus", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write638_LoadMktStatusInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		public void Write639_TFEXInformation(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
			base.WriteStartElement("TFEXInformation", "http://tempuri.org/", null, false);
			base.WriteEndElement();
		}
		public void Write640_TFEXInformationInHeaders(object[] p)
		{
			base.WriteStartDocument();
			base.TopLevelElement();
			int arg_0F_0 = p.Length;
		}
		protected override void InitCallbacks()
		{
		}
	}
}
