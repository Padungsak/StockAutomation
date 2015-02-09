using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.RealtimeMessage_New
{
	public class BCTextMessage : IBroadcastMessage
	{
		private const char spliter = ';';
		private string securitySymbol;
		private string bcMessageType = string.Empty;
		private string messageText = string.Empty;
		private string messageTime = string.Empty;
		public string SecuritySymbol
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.securitySymbol;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.securitySymbol = value;
			}
		}
		public string BcMessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.bcMessageType;
			}
		}
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
				return "B+";
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public BCTextMessage()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public BCTextMessage(string message)
		{
			this.Unpack(message);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string Pack(string securitySymbol, string messageType, string message, string mesageTime)
		{
			return string.Concat(new object[]
			{
				"B+",
				securitySymbol,
				';',
				messageType,
				';',
				message,
				';',
				mesageTime
			});
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Unpack(string message)
		{
			message = message.Substring(2);
			string[] array = message.Split(new char[]
			{
				';'
			});
			this.securitySymbol = array[0];
			this.bcMessageType = array[1];
			this.messageText = array[2];
			this.messageTime = array[3];
		}
	}
}
