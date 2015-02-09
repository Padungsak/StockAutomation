using System;
using System.ComponentModel;
namespace ITSNet.Controls.XtWebBrowser
{
	public class XtBrowserNavigatorEventArgs : CancelEventArgs
	{
		private string m_Url;
		private string m_Frame;
		private byte[] m_Postdata;
		private string m_Headers;
		public string Url
		{
			get
			{
				return this.m_Url;
			}
		}
		public string Frame
		{
			get
			{
				return this.m_Frame;
			}
		}
		public string Headers
		{
			get
			{
				return this.m_Headers;
			}
		}
		public string Postdata
		{
			get
			{
				return this.PostdataToString(this.m_Postdata);
			}
		}
		public byte[] PostdataByte
		{
			get
			{
				return this.m_Postdata;
			}
		}
		public XtBrowserNavigatorEventArgs(string url, string frame, byte[] postdata, string headers)
		{
			this.m_Url = url;
			this.m_Frame = frame;
			this.m_Postdata = postdata;
			this.m_Headers = headers;
		}
		private string PostdataToString(byte[] p)
		{
			string text = "";
			if (p == null || p.Length == 0)
			{
				return "";
			}
			for (int i = 0; i <= p.Length - 1; i++)
			{
				text += Convert.ToChar(p[i]).ToString();
			}
			text = text.Replace(Convert.ToChar(13).ToString(), "");
			text = text.Replace(Convert.ToChar(10).ToString(), "");
			text = text.Replace(Convert.ToChar(0).ToString(), "");
			if (text == null)
			{
				return "";
			}
			return text;
		}
	}
}
