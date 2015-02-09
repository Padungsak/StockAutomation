using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.RealtimeMessage
{
	public class BroadCastOrderMessageClient : IBroadcastMessage
	{
		private const char spliter = ';';
		private string messageType = "0B";
		private string originalMessageType = string.Empty;
		private string side = string.Empty;
		private string content = string.Empty;
		private DateTime time;
		private string entryID = string.Empty;
		private string reserve1 = string.Empty;
		private string reserve2 = string.Empty;
		private string[] msg;
		public string OriginalMessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.originalMessageType;
			}
		}
		public string Side
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.side;
			}
		}
		public string Content
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.content;
			}
		}
		public DateTime Time
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.time;
			}
		}
		public string EntryID
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.entryID;
			}
		}
		public string Reserve1
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.reserve1;
			}
		}
		public string Reserve2
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.reserve2;
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
		public string Pack(string entryId, string reserve1, string reserve2, string originalMessageType, string side, string content)
		{
			return string.Concat(new object[]
			{
				this.messageType,
				entryId,
				';',
				reserve1,
				';',
				reserve2,
				';',
				originalMessageType,
				';',
				side,
				';',
				content,
				';',
				DateTime.Now.ToString("HH:mm:ss")
			});
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Unpack(string message)
		{
			this.messageType = message.Substring(0, 2);
			this.msg = message.Substring(2).Split(new char[]
			{
				';'
			});
			this.entryID = this.msg[0].Trim();
			this.reserve1 = this.msg[1].Trim();
			this.reserve2 = this.msg[2].Trim();
			this.originalMessageType = this.msg[3];
			this.side = this.msg[4];
			this.content = this.msg[5];
			if (this.msg.GetUpperBound(0) >= 6)
			{
				this.time = Convert.ToDateTime(this.msg[6]);
			}
			this.msg = null;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public BroadCastOrderMessageClient()
		{
		}
	}
}
