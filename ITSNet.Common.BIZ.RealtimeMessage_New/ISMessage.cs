using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.RealtimeMessage_New
{
	public class ISMessage : IBroadcastMessage
	{
		private const char spliter = ';';
		private string symbol;
		private int orderBookId;
		private decimal indexValue;
		private decimal indexHigh;
		private decimal indexLow;
		private int tick;
		private decimal trin;
		private long upVolume;
		private long downVolume;
		private long noChangeVolume;
		private int securityUp;
		private int securityDown;
		private int securityNoChange;
		private decimal totalValuesTraded;
		private long mainAccVolume;
		private decimal mainAccValue;
		private long oddlotAccVolume;
		private decimal oddlotAccValue;
		private long biglotAccVolume;
		private decimal biglotAccValue;
		private long foreignAccVolume;
		private decimal foreignAccValue;
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
		public int Tick
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.tick;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.tick = value;
			}
		}
		public decimal Trin
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.trin;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.trin = value;
			}
		}
		public long UpVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.upVolume;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.upVolume = value;
			}
		}
		public long DownVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.downVolume;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.downVolume = value;
			}
		}
		public long NoChangeVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.noChangeVolume;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.noChangeVolume = value;
			}
		}
		public int SecurityUp
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.securityUp;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.securityUp = value;
			}
		}
		public int SecurityDown
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.securityDown;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.securityDown = value;
			}
		}
		public int SecurityNoChange
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.securityNoChange;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.securityNoChange = value;
			}
		}
		public decimal TotalValuesTraded
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.totalValuesTraded;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.totalValuesTraded = value;
			}
		}
		public long MainAccVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.mainAccVolume;
			}
		}
		public decimal MainAccValue
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.mainAccValue;
			}
		}
		public long OddlotAccVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.oddlotAccVolume;
			}
		}
		public decimal OddlotAccValue
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.oddlotAccValue;
			}
		}
		public long BiglotAccVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.biglotAccVolume;
			}
		}
		public decimal BiglotAccValue
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.biglotAccValue;
			}
		}
		public long ForeignAccVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.foreignAccVolume;
			}
		}
		public decimal ForeignAccValue
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.foreignAccValue;
			}
		}
		public string MessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return "IS";
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ISMessage()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ISMessage(string message)
		{
			this.Unpack(message);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string Pack(string symbol, decimal indexValue, decimal indexHigh, decimal indexLow, int tick, decimal trin, long upVolume, long downVolume, long noChangeVolume, int securityUp, int securityDown, int securityNoChange, decimal totalValuesTraded, long mainAccVolume, decimal mainAccValue, long oddlotAccVolume, decimal oddlotAccValue, long biglotAccVolume, decimal biglotAccValue, long foreignAccVolume, decimal foreignAccValue, int orderBookId)
		{
			string result;
			try
			{
				result = string.Concat(new string[]
				{
					"IS",
					symbol.PadRight(10, ' '),
					AutoTManager.Mode96dot(indexValue, 4),
					AutoTManager.Mode96dot(indexHigh, 4),
					AutoTManager.Mode96dot(indexLow, 4),
					AutoTManager.Mod96((long)(tick + 10000), 3),
					AutoTManager.Mode96dot(trin, 3),
					AutoTManager.Mod96(upVolume, 6),
					AutoTManager.Mod96(downVolume, 6),
					AutoTManager.Mod96(noChangeVolume, 6),
					AutoTManager.Mod96((long)securityUp, 3),
					AutoTManager.Mod96((long)securityDown, 3),
					AutoTManager.Mod96((long)securityNoChange, 3),
					AutoTManager.Mod96((long)totalValuesTraded, 6),
					AutoTManager.Mod96(mainAccVolume, 6),
					AutoTManager.Mod96((long)mainAccValue, 6),
					AutoTManager.Mod96(oddlotAccVolume, 6),
					AutoTManager.Mod96((long)oddlotAccValue, 6),
					AutoTManager.Mod96(biglotAccVolume, 6),
					AutoTManager.Mod96((long)biglotAccValue, 6),
					AutoTManager.Mod96(foreignAccVolume, 6),
					AutoTManager.Mod96((long)foreignAccValue, 6),
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
				this.tick = (int)AutoTManager.Demod96int(message.Substring(22, 3)) - 10000;
				this.trin = AutoTManager.Demod96dot(message.Substring(25, 3));
				this.upVolume = AutoTManager.Demod96int(message.Substring(28, 6));
				this.downVolume = AutoTManager.Demod96int(message.Substring(34, 6));
				this.noChangeVolume = AutoTManager.Demod96int(message.Substring(40, 6));
				this.securityUp = (int)AutoTManager.Demod96int(message.Substring(46, 3));
				this.securityDown = (int)AutoTManager.Demod96int(message.Substring(49, 3));
				this.securityNoChange = (int)AutoTManager.Demod96int(message.Substring(52, 3));
				this.totalValuesTraded = AutoTManager.Demod96int(message.Substring(55, 6));
				this.mainAccVolume = AutoTManager.Demod96int(message.Substring(61, 6));
				this.mainAccValue = AutoTManager.Demod96int(message.Substring(67, 6));
				this.oddlotAccVolume = AutoTManager.Demod96int(message.Substring(73, 6));
				this.oddlotAccValue = AutoTManager.Demod96int(message.Substring(79, 6));
				this.biglotAccVolume = AutoTManager.Demod96int(message.Substring(85, 6));
				this.biglotAccValue = AutoTManager.Demod96int(message.Substring(91, 6));
				this.foreignAccVolume = AutoTManager.Demod96int(message.Substring(97, 6));
				this.foreignAccValue = AutoTManager.Demod96int(message.Substring(103, 6));
				this.orderBookId = (int)AutoTManager.Demod96int(message.Substring(109, 2));
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
