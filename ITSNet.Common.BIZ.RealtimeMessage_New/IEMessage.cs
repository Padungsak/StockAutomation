using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.RealtimeMessage_New
{
	public class IEMessage : IBroadcastMessage
	{
		private const char spliter = ';';
		private string symbol = string.Empty;
		private decimal indexValue;
		private decimal indexHigh;
		private decimal indexLow;
		private long accVolume;
		private decimal accValue;
		private int originalNumber;
		private int orderBookId;
		public string Symbol
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.symbol;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.symbol = value;
			}
		}
		public decimal IndexValue
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.indexValue;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.indexValue = value;
			}
		}
		public decimal IndexHigh
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.indexHigh;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.indexHigh = value;
			}
		}
		public decimal IndexLow
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.indexLow;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.indexLow = value;
			}
		}
		public long AccVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.accVolume;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.accVolume = value;
			}
		}
		public decimal AccValue
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.accValue;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.accValue = value;
			}
		}
		public int OriginalNumber
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.originalNumber;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.originalNumber = value;
			}
		}
		public int OrderBookId
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.orderBookId;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.orderBookId = value;
			}
		}
		public string MessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return "IE";
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public IEMessage()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public IEMessage(string message)
		{
			this.Unpack(message);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string Pack(string symbol, decimal indexValue, decimal indexHigh, decimal indexLow, long accVol, decimal accValue, int originalNumber, int orderBookId)
		{
			string result;
			try
			{
				result = string.Concat(new string[]
				{
					"IE",
					symbol.PadRight(10, ' '),
					AutoTManager.Mode96dot(indexValue, 4),
					AutoTManager.Mode96dot(indexHigh, 4),
					AutoTManager.Mode96dot(indexLow, 4),
					AutoTManager.Mod96(accVol, 6),
					AutoTManager.Mod96((long)accValue, 6),
					AutoTManager.Mod96((long)originalNumber, 2),
					AutoTManager.Mod96((long)orderBookId, 2)
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Unpack(string message)
		{
			try
			{
				message = message.Substring(2);
				this.symbol = message.Substring(0, 10).Trim();
				this.indexValue = AutoTManager.Demod96dot(message.Substring(10, 4));
				this.indexHigh = AutoTManager.Demod96dot(message.Substring(14, 4));
				this.indexLow = AutoTManager.Demod96dot(message.Substring(18, 4));
				this.accVolume = AutoTManager.Demod96int(message.Substring(22, 6));
				this.accValue = AutoTManager.Demod96int(message.Substring(28, 6));
				this.originalNumber = (int)AutoTManager.Demod96int(message.Substring(34, 2));
				this.orderBookId = (int)AutoTManager.Demod96int(message.Substring(36, 2));
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
