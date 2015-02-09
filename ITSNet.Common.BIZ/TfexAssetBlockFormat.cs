using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ
{
	public class TfexAssetBlockFormat
	{
		private string[] messageList;
		private int sequntNumber;
		private string marketID;
		private int messagePerBlock;
		private string originalMessage = string.Empty;
		private string content;
		public string[] MessageList
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.messageList;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.messageList = value;
			}
		}
		public int MessageListCount
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (this.messageList != null)
				{
					return this.messageList.Length;
				}
				return -1;
			}
		}
		public int SequentNumber
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.sequntNumber;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.sequntNumber = value;
			}
		}
		public string MarketID
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.marketID;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.marketID = value;
			}
		}
		public int MessagePerBlock
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.messagePerBlock;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.messagePerBlock = value;
			}
		}
		public string OriginalMessage
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.originalMessage;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.originalMessage = value;
			}
		}
		public string Content
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.content;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.content = value;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Clear()
		{
			this.sequntNumber = 0;
			this.marketID = string.Empty;
			this.messagePerBlock = 0;
			this.messageList = new string[0];
			this.content = string.Empty;
			this.originalMessage = string.Empty;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public int GetSequenceNumber(string message)
		{
			int result;
			try
			{
				result = Convert.ToInt32(AutoTManager.Demod96int(message.Substring(0, 3)));
			}
			catch
			{
				result = -1;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool UnPack(string message)
		{
			long value = AutoTManager.Demod96int(message.Substring(0, 3));
			this.sequntNumber = Convert.ToInt32(value);
			if (this.sequntNumber > 0)
			{
				this.originalMessage = message;
				this.marketID = message.Substring(3, 1);
				value = AutoTManager.Demod96int(message.Substring(4, 1));
				this.messagePerBlock = Convert.ToInt32(value);
				this.content = message.Substring(5);
				this.messageList = this.content.Split(new char[]
				{
					'\u001f'
				});
				return true;
			}
			return false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool UnPack(int seqNumber, string marketID, int msgPerBlock, string content)
		{
			bool result = false;
			if (seqNumber > 0)
			{
				result = true;
				this.sequntNumber = seqNumber;
				this.marketID = marketID;
				this.messagePerBlock = msgPerBlock;
				this.content = content;
				this.messageList = content.Split(new char[]
				{
					'\a'
				});
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string GetMessage(int index)
		{
			if (index > 0 && index < this.messageList.Length)
			{
				return this.messageList[index];
			}
			return string.Empty;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public TfexAssetBlockFormat()
		{
		}
	}
}
