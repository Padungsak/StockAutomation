using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.RealtimeMessage
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
			return string.Concat(new object[]
			{
				"IS",
				symbol,
				';',
				indexValue,
				';',
				indexHigh,
				';',
				indexLow,
				';',
				tick,
				';',
				trin,
				';',
				upVolume,
				';',
				downVolume,
				';',
				noChangeVolume,
				';',
				securityUp,
				';',
				securityDown,
				';',
				securityNoChange,
				';',
				totalValuesTraded,
				';',
				mainAccVolume,
				';',
				FormatUtil.VolumeFormat(mainAccValue, false),
				';',
				oddlotAccVolume,
				';',
				FormatUtil.VolumeFormat(oddlotAccValue, false),
				';',
				biglotAccVolume,
				';',
				FormatUtil.VolumeFormat(biglotAccValue, false),
				';',
				foreignAccVolume,
				';',
				FormatUtil.VolumeFormat(foreignAccValue, false),
				';',
				orderBookId
			});
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Unpack(string message)
		{
			try
			{
				message = message.Substring(2);
				string[] array = message.Split(new char[]
				{
					';'
				});
				this.symbol = array[0];
				this.indexValue = Convert.ToDecimal(array[1]);
				this.indexHigh = Convert.ToDecimal(array[2]);
				this.indexLow = Convert.ToDecimal(array[3]);
				this.tick = Convert.ToInt32(array[4]);
				this.trin = Convert.ToDecimal(array[5]);
				this.upVolume = Convert.ToInt64(array[6]);
				this.downVolume = Convert.ToInt64(array[7]);
				this.noChangeVolume = Convert.ToInt64(array[8]);
				this.securityUp = Convert.ToInt32(array[9]);
				this.securityDown = Convert.ToInt32(array[10]);
				this.securityNoChange = Convert.ToInt32(array[11]);
				this.totalValuesTraded = Convert.ToDecimal(array[12]);
				this.mainAccVolume = Convert.ToInt64(array[13]);
				decimal.TryParse(array[14], out this.mainAccValue);
				this.oddlotAccVolume = Convert.ToInt64(array[15]);
				decimal.TryParse(array[16], out this.oddlotAccValue);
				this.biglotAccVolume = Convert.ToInt64(array[17]);
				decimal.TryParse(array[18], out this.biglotAccValue);
				this.foreignAccVolume = Convert.ToInt64(array[19]);
				decimal.TryParse(array[20], out this.foreignAccValue);
				this.orderBookId = Convert.ToInt32(array[21]);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
