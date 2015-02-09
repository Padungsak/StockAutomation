using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.RealtimeMessage_New
{
	public class PDMessage : IBroadcastMessage
	{
		private const char spliter = ';';
		private int securityNumber;
		private string board = string.Empty;
		private long volume;
		private decimal price;
		private int biglotDeals;
		private long biglotAccVolume;
		private decimal biglotAccValue;
		private decimal lastPrice;
		private decimal highPrice;
		private decimal lowPrice;
		public int SecurityNumber
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.securityNumber;
			}
		}
		public string Board
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.board;
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
		public decimal Price
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.price;
			}
		}
		public int BiglotDeals
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.biglotDeals;
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
		public decimal LastPrice
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.lastPrice;
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
		public string MessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return "PD";
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public PDMessage()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public PDMessage(string message)
		{
			this.Unpack(message);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string Pack(int securityNumber, string board, long volume, decimal price, int deals, long biglotAccVolume, decimal biglotAccValue, decimal lastPrice, decimal highPrice, decimal lowPrice)
		{
			return string.Concat(new object[]
			{
				"PD",
				';',
				securityNumber,
				';',
				board,
				';',
				volume,
				';',
				price,
				';',
				deals,
				';',
				biglotAccVolume,
				';',
				biglotAccValue,
				';',
				lastPrice,
				';',
				highPrice,
				';',
				lowPrice
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
				int num;
				if (int.TryParse(array[0], out num))
				{
					this.securityNumber = num;
					this.board = array[1];
					this.volume = Convert.ToInt64(array[2]);
					this.price = Convert.ToDecimal(array[3]);
					this.biglotDeals = Convert.ToInt32(array[4]);
					this.biglotAccVolume = Convert.ToInt64(array[5]);
					this.biglotAccValue = Convert.ToDecimal(array[6]);
					this.lastPrice = Convert.ToDecimal(array[7]);
					this.highPrice = Convert.ToDecimal(array[8]);
					this.lowPrice = Convert.ToDecimal(array[9]);
				}
				else
				{
					this.securityNumber = -1;
					this.board = "";
					this.volume = 0L;
					this.price = 0m;
					this.biglotDeals = 0;
					this.biglotAccVolume = 0L;
					this.biglotAccValue = 0m;
					this.lastPrice = 0m;
					this.highPrice = 0m;
					this.lowPrice = 0m;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
