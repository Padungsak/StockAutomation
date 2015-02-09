using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.RealtimeMessage
{
	public class DGWOrderReplyMessage : IBroadcastMessage
	{
		private string messageType = "0G";
		private long orderNumber;
		private string replyCode = string.Empty;
		private string replyMessage = string.Empty;
		private string[] arr;
		public long OrderNumber
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.orderNumber;
			}
		}
		public string ReplyCode
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
		public string MessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.messageType;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(long orderNumber, string replyCode, string replyMessage)
		{
			return string.Concat(new object[]
			{
				this.messageType,
				orderNumber,
				"|",
				replyCode,
				"|",
				replyMessage
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
					long.TryParse(this.arr[0], out this.orderNumber);
					this.replyCode = this.arr[1];
					this.replyMessage = this.arr[2];
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public DGWOrderReplyMessage()
		{
		}
	}
}
