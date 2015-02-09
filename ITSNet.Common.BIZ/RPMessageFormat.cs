using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ
{
	public class RPMessageFormat
	{
		private int firmID;
		private string marketID;
		private int prevSeqNumber;
		private int seqNumber;
		private int messageCount;
		private string originalMessage;
		public int FirmID
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.firmID;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.firmID = value;
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
		public int PrevSeqNumber
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.prevSeqNumber;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.prevSeqNumber = value;
			}
		}
		public int SeqNumber
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.seqNumber;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.seqNumber = value;
			}
		}
		public int MessageCount
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.messageCount;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.messageCount = value;
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
		[MethodImpl(MethodImplOptions.NoInlining)]
		public RPMessageFormat()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public RPMessageFormat(string message)
		{
			this.Unpack(message);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(int firmId, string marketId, int prevSeq, int seq, int messageCount, string message)
		{
			return string.Concat(new string[]
			{
				"RP",
				AutoTManager.Mod96((long)firmId, 2),
				marketId,
				AutoTManager.Mod96((long)prevSeq, 3),
				AutoTManager.Mod96((long)seq, 3),
				AutoTManager.Mod96((long)messageCount, 1),
				message
			});
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Unpack(string message)
		{
			message.Substring(0, 2);
			this.firmID = (int)AutoTManager.Demod96int(message.Substring(2, 2));
			this.marketID = message.Substring(4, 1);
			this.prevSeqNumber = (int)AutoTManager.Demod96int(message.Substring(5, 3));
			this.seqNumber = (int)AutoTManager.Demod96int(message.Substring(8, 3));
			this.messageCount = (int)AutoTManager.Demod96int(message.Substring(11, 1));
			this.originalMessage = message.Substring(12);
		}
	}
}
