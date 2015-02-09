using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.RealtimeMessage
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
			return string.Concat(new object[]
			{
				"IE",
				symbol,
				';',
				indexValue,
				';',
				indexHigh,
				';',
				indexLow,
				';',
				accVol,
				';',
				accValue,
				';',
				originalNumber,
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
				this.accVolume = Convert.ToInt64(array[4]);
				this.accValue = Convert.ToDecimal(array[5]);
				this.originalNumber = Convert.ToInt32(array[6]);
				this.orderBookId = Convert.ToInt32(array[7]);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
