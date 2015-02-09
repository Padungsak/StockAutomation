using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.RealtimeMessage.TFEX
{
	public class LSMessageTFEX : IBroadcastMessage
	{
		private const char spliter = ';';
		private string sec = string.Empty;
		private decimal price;
		private int hiddenPrice;
		private int vol;
		private string side = string.Empty;
		private int dealSource;
		private int longQty;
		private int shortQty;
		private int openQty;
		private int openInterest;
		private decimal basis;
		private int deals;
		private decimal openValue;
		private decimal longValue;
		private decimal shortValue;
		private int accVolume;
		private decimal accValue;
		private decimal high;
		private decimal low;
		private decimal lastUdlyIndx;
		private string lastTime;
		private string[] Arr;
		public string Sec
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.sec;
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
		public long HiddenPrice
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (long)this.hiddenPrice;
			}
		}
		public int Vol
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.vol;
			}
		}
		public string Side
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (this.side == "1")
				{
					return "B";
				}
				return "S";
			}
		}
		public int DealSource
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.dealSource;
			}
		}
		public int LongQty
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.longQty;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.longQty = value;
			}
		}
		public int ShortQty
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.shortQty;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.shortQty = value;
			}
		}
		public int OpenQty
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.openQty;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.openQty = value;
			}
		}
		public int OpenInterest
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.openInterest;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.openInterest = value;
			}
		}
		public decimal Basis
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.basis;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.basis = value;
			}
		}
		public int Deals
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.deals;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.deals = value;
			}
		}
		public decimal OpenValue
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.openValue;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.openValue = value;
			}
		}
		public decimal LongValue
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.longValue;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.longValue = value;
			}
		}
		public decimal ShortValue
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.shortValue;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.shortValue = value;
			}
		}
		public int AccVolume
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
		public decimal High
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.high;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.high = value;
			}
		}
		public decimal Low
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.low;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.low = value;
			}
		}
		public decimal LastUdlyIndx
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.lastUdlyIndx;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.lastUdlyIndx = value;
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
				return "LS";
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public LSMessageTFEX()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public LSMessageTFEX(string message)
		{
			this.Unpack(message);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string Pack(string sec, decimal price, int volume, string side, int dealsource, int longQty, int shortQty, int openQty, decimal basis, int deals, decimal OpenValue, decimal longValue, decimal shortValue, int accVolume, decimal accValue, decimal lastUdlyIndx, decimal high, decimal low)
		{
			string result;
			try
			{
				result = string.Concat(new object[]
				{
					"LS".PadRight(4, ' '),
					sec,
					';',
					price,
					';',
					volume,
					';',
					side,
					';',
					dealsource,
					';',
					longQty,
					';',
					shortQty,
					';',
					openQty,
					';',
					FormatUtil.PriceFormat(basis),
					';',
					deals,
					';',
					OpenValue,
					';',
					longValue,
					';',
					shortValue,
					';',
					accVolume,
					';',
					FormatUtil.ValueFormat(accValue, false),
					';',
					lastUdlyIndx,
					';',
					high,
					';',
					low,
					';',
					DateTime.Now.ToString("HH:mm:ss")
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
				message = message.Substring(4);
				this.Arr = message.Split(new char[]
				{
					';'
				});
				this.sec = this.Arr[0];
				decimal.TryParse(this.Arr[1], out this.price);
				int.TryParse(this.Arr[2], out this.vol);
				this.side = this.Arr[3];
				int.TryParse(this.Arr[4], out this.dealSource);
				int.TryParse(this.Arr[5], out this.longQty);
				int.TryParse(this.Arr[6], out this.shortQty);
				int.TryParse(this.Arr[7], out this.openQty);
				decimal.TryParse(this.Arr[8], out this.basis);
				int.TryParse(this.Arr[9], out this.deals);
				decimal.TryParse(this.Arr[10], out this.openValue);
				decimal.TryParse(this.Arr[11], out this.longValue);
				decimal.TryParse(this.Arr[12], out this.shortValue);
				int.TryParse(this.Arr[13], out this.accVolume);
				decimal.TryParse(this.Arr[14], out this.accValue);
				decimal.TryParse(this.Arr[15], out this.lastUdlyIndx);
				decimal.TryParse(this.Arr[16], out this.high);
				decimal.TryParse(this.Arr[17], out this.low);
				this.lastTime = this.Arr[18];
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
