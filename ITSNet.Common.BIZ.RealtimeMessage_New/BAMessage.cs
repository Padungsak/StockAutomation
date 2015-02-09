using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.RealtimeMessage_New
{
	public class BAMessage : IBroadcastMessage
	{
		public const int Critical = 1;
		public const int Normal = 2;
		public const int News = 3;
		private const char spliter = ';';
		private string accountID = string.Empty;
		private string senderID = string.Empty;
		private int priorities;
		private int messageID;
		private string messageText = string.Empty;
		private string hyperLink = string.Empty;
		private DateTime messageTime = DateTime.MinValue;
		public string AccountID
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.accountID;
			}
		}
		public string SenderID
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.senderID;
			}
		}
		public int Priorities
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.priorities;
			}
		}
		public int MessageID
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.messageID;
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
		public string HyperLink
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.hyperLink;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.hyperLink = value;
			}
		}
		public DateTime MessageTime
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
				return "BA";
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public BAMessage()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public BAMessage(string message)
		{
			this.Unpack(message);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string Pack(string accountID, string senderID, int priorities, int messageID, string message, string hyperLink, DateTime mesageTime)
		{
			return string.Concat(new object[]
			{
				"BA",
				accountID,
				';',
				senderID,
				';',
				priorities,
				';',
				messageID,
				';',
				message,
				';',
				hyperLink,
				';',
				mesageTime.ToString("yyyy/MM/dd HH:mm:ss")
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
			this.accountID = array[0];
			this.senderID = array[1];
			int.TryParse(array[2], out this.priorities);
			int.TryParse(array[3], out this.messageID);
			this.messageText = array[4];
			this.hyperLink = array[5];
			if (FormatUtil.Isdatetime(array[6]))
			{
				this.messageTime = Convert.ToDateTime(array[6]);
			}
		}
	}
}
