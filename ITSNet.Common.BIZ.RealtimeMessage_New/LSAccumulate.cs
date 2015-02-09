using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.RealtimeMessage_New
{
	public class LSAccumulate : IBroadcastMessage
	{
		private const char spliter = ';';
		private int securityNumber;
		private string side = string.Empty;
		private decimal lastPrice;
		private long volume;
		private int deals;
		private decimal highPrice;
		private decimal lowPrice;
		private int dealInMain;
		private decimal accValue;
		private long openVolume;
		private long buyVolume;
		private long sellVolume;
		private string comparePrice = string.Empty;
		private decimal premiumPrice;
		private string lastTime;
		public int SecurityNumber
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.securityNumber;
			}
		}
		public string Side
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.side;
			}
		}
		public decimal LastPrice
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.lastPrice;
			}
		}
		public long Volume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.volume;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.volume = value;
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
		public decimal HighPrice
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.highPrice;
			}
		}
		public decimal LowPrice
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.lowPrice;
			}
		}
		public int DealInMain
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.dealInMain;
			}
		}
		public long AccVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.openVolume + this.buyVolume + this.sellVolume;
			}
		}
		public decimal AccValue
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.accValue;
			}
		}
		public long OpenVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.openVolume;
			}
		}
		public long BuyVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.buyVolume;
			}
		}
		public long SellVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.sellVolume;
			}
		}
		public string ComparePrice
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.comparePrice;
			}
		}
		public decimal PremiumPrice
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.premiumPrice;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.premiumPrice = value;
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
				return "L+";
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public LSAccumulate()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public LSAccumulate(string message)
		{
			try
			{
				this.Unpack(message);
			}
			catch (Exception ex)
			{
				throw ex;
			}
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
				int num;
				if (int.TryParse(array[0], out num))
				{
					this.securityNumber = num;
					this.side = array[1];
					decimal.TryParse(array[2], out this.lastPrice);
					long.TryParse(array[3], out this.volume);
					int.TryParse(array[4], out this.deals);
					decimal.TryParse(array[5], out this.highPrice);
					decimal.TryParse(array[6], out this.lowPrice);
					this.dealInMain = Convert.ToInt32(array[7]);
					this.accValue = Convert.ToDecimal(array[8]);
					this.openVolume = Convert.ToInt64(array[9]);
					this.buyVolume = Convert.ToInt64(array[10]);
					this.sellVolume = Convert.ToInt64(array[11]);
					this.comparePrice = array[12];
					decimal.TryParse(array[13], out this.premiumPrice);
					this.lastTime = array[14];
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string Pack(int securityNumber, decimal highPrice, decimal lowPrice, long openVolume, long buyVolume, long sellVolume, string comparePrice, int dealInMain, decimal accValue, decimal premiumPrice, string side, decimal price, long sumLotVolume, int sumDeals)
		{
			return string.Concat(new object[]
			{
				"L+",
				securityNumber,
				';',
				side,
				';',
				Math.Round(price, 2),
				';',
				sumLotVolume,
				';',
				sumDeals,
				';',
				Math.Round(highPrice, 2),
				';',
				Math.Round(lowPrice, 2),
				';',
				dealInMain,
				';',
				FormatUtil.ValueFormat(accValue, false),
				';',
				openVolume,
				';',
				buyVolume,
				';',
				sellVolume,
				';',
				comparePrice,
				';',
				Math.Round(premiumPrice, 2),
				';',
				DateTime.Now.ToString("HH:mm:ss")
			});
		}
	}
}
