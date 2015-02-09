using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ
{
	public class RQMessageFormat
	{
		private int firmId;
		private string marketId;
		private int startSeq;
		private int endSeq;
		public int FirmId
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.firmId;
			}
		}
		public string MarketId
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.marketId;
			}
		}
		public int StartSeq
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.startSeq;
			}
		}
		public int EndSeq
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.endSeq;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public RQMessageFormat()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(int firmID, string marketID, int startRetranSeqNo, int endRetranSeqNo)
		{
			return string.Concat(new string[]
			{
				"RQ",
				AutoTManager.Mod96((long)firmID, 2),
				marketID,
				AutoTManager.Mod96((long)startRetranSeqNo, 3),
				AutoTManager.Mod96((long)endRetranSeqNo, 3)
			});
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Unpack(string message)
		{
			message.Substring(0, 2);
			this.firmId = (int)AutoTManager.Demod96int(message.Substring(2, 2));
			this.marketId = message.Substring(4, 1);
			this.startSeq = (int)AutoTManager.Demod96int(message.Substring(5, 3));
			this.endSeq = (int)AutoTManager.Demod96int(message.Substring(8, 3));
		}
	}
}
