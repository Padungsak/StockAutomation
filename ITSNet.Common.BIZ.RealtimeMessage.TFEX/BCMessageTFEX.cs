using System;
using System.Runtime.CompilerServices;
using System.Text;
namespace ITSNet.Common.BIZ.RealtimeMessage.TFEX
{
	public class BCMessageTFEX : IBroadcastMessage
	{
		private const char spliter = ';';
		private string messageText = string.Empty;
		private string messageTime = string.Empty;
		private static StringBuilder lsString = new StringBuilder();
		private string[] Arr;
		public string MessageText
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.messageText;
			}
		}
		public string MessageTime
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.messageTime;
			}
		}
		public string MessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return "BC";
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public BCMessageTFEX()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public BCMessageTFEX(string message)
		{
			this.Unpack(message);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string Pack(string messageText, string messageTime)
		{
			try
			{
				if (BCMessageTFEX.lsString.Length > 0)
				{
					BCMessageTFEX.lsString.Remove(0, BCMessageTFEX.lsString.Length);
				}
				BCMessageTFEX.lsString.Append("BC  ");
				BCMessageTFEX.lsString.Append(messageText);
				BCMessageTFEX.lsString.Append(';');
				BCMessageTFEX.lsString.Append(messageTime);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return BCMessageTFEX.lsString.ToString();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Unpack(string message)
		{
			try
			{
				message = message.Substring(4);
				this.Arr = message.Split(new char[]
				{
					';'
				});
				this.messageText = this.Arr[0];
				this.messageTime = this.Arr[1];
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
