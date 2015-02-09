using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.RealtimeMessage
{
	public class LOMessage : IBroadcastMessage
	{
		private const char spliter = ';';
		private int securityNumber;
		private decimal price;
		private long volume;
		private int oddlotDeals;
		private long oddlotAccVolume;
		private decimal oddlotAccValue;
		private string side = string.Empty;
		private decimal lowPrice;
		private decimal highPrice;
		private string lastTime;
		public int SecurityNumber
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.securityNumber;
			}
		}
		public decimal Price
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.price;
			}
		}
		public long Volume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.volume;
			}
		}
		public int OddlotDeals
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.oddlotDeals;
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
		public string Side
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.side;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.side = value;
			}
		}
		public decimal LowPrice
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.lowPrice;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.lowPrice = value;
			}
		}
		public decimal HighPrice
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.highPrice;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.highPrice = value;
			}
		}
		public string LastTime
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.lastTime;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.lastTime = value;
			}
		}
		public string MessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return "LO";
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public LOMessage()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public LOMessage(string message)
		{
			this.Unpack(message);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string Pack(int securityNumber, decimal price, long volume, int oddlotDeals, long oddlotAccVolume, decimal oddlotAccValue, string side, decimal highPrice, decimal lowPrice, string lastTime)
		{
			return string.Concat(new object[]
			{
				"LO",
				securityNumber,
				';',
				price,
				';',
				volume,
				';',
				oddlotDeals,
				';',
				oddlotAccVolume,
				';',
				oddlotAccValue,
				';',
				side,
				';',
				highPrice,
				';',
				lowPrice,
				';',
				lastTime
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
				this.securityNumber = Convert.ToInt32(array[0]);
				this.price = Convert.ToDecimal(array[1]);
				this.volume = Convert.ToInt64(array[2]);
				this.oddlotDeals = Convert.ToInt32(array[3]);
				this.oddlotAccVolume = Convert.ToInt64(array[4]);
				this.oddlotAccValue = Convert.ToDecimal(array[5]);
				this.side = array[6];
				this.highPrice = Convert.ToDecimal(array[7]);
				this.lowPrice = Convert.ToDecimal(array[8]);
				this.lastTime = array[9];
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
