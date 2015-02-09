using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ
{
	public class InternalOrderMMS
	{
		private const int SEQUENCE_SIZE = 10;
		private const int MESSAGE_PER_BLOCK_SIZE = 3;
		private const int MESSAGE_TYPE_SIZE = 2;
		public const int OTHER_SEQ_ID = -1;
		public const string ORDER_MESSAGE_TYPE = "OD";
		public const string REPLY_MESSAGE_TYPE = "RP";
		public const string NEWS_MESSAGE_FOR_BRANCH = "NH";
		public const string ASSET_MESSAGE_FOR_BRANCH = "AS";
		private string messageType;
		private int seq;
		private int messagePerBlock;
		private string body;
		public string MessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.messageType;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.messageType = value;
			}
		}
		public int Seq
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.seq;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.seq = value;
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
		public string Body
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.body;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.body = value;
			}
		}
		public string MessagePacket
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.Pack(this.messageType, this.seq, this.messagePerBlock, this.body);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool UnPack(string message)
		{
			bool result;
			try
			{
				this.messageType = string.Empty;
				this.seq = -1;
				this.messagePerBlock = 0;
				this.body = string.Empty;
				this.messageType = message.Substring(0, 2);
				this.seq = Convert.ToInt32(message.Substring(2, 10));
				this.messagePerBlock = Convert.ToInt32(message.Substring(12, 3));
				this.body = message.Substring(15);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(string messageType, int seq, int messagePerBlock, string body)
		{
			return messageType.PadRight(2, ' ') + seq.ToString().PadRight(10, ' ') + messagePerBlock.ToString().PadRight(3, ' ') + body;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public InternalOrderMMS()
		{
		}
	}
}
