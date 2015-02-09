using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.RealtimeMessage
{
	public class OrderReplyMessage : IBroadcastMessage
	{
		private string messageType = "0R";
		private int replyCode;
		private string replyMessage;
		private string replyMessageEng;
		private string requestID;
		private string sessionID;
		private string[] arr;
		public int ReplyCode
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.replyCode;
			}
		}
		public string ReplyMessage
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.replyMessage;
			}
		}
		public string ReplyMessageEng
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.replyMessageEng;
			}
		}
		public string RequestID
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.requestID;
			}
		}
		public string SessionID
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.sessionID;
			}
		}
		public string MessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.messageType;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(string sessionID, string requestID, int replyCode, string replyMessage, string replyMessageEng)
		{
			return string.Concat(new object[]
			{
				this.messageType,
				sessionID,
				"|",
				requestID,
				"|",
				replyCode,
				"|",
				replyMessage,
				"|",
				replyMessageEng
			});
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Unpack(string message)
		{
			try
			{
				if (message != string.Empty)
				{
					message = message.Substring(2);
					this.arr = message.Split(new char[]
					{
						'|'
					});
					this.sessionID = this.arr[0];
					this.requestID = this.arr[1];
					this.replyCode = Convert.ToInt32(this.arr[2]);
					this.replyMessage = this.arr[3];
					this.replyMessageEng = this.arr[4];
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public OrderReplyMessage()
		{
		}
	}
}
