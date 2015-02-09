using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.RealtimeMessage
{
	public class POMessage : IBroadcastMessage
	{
		private const char spliter = ';';
		private int securityNumber;
		private decimal projectedPrice;
		private long projectedVolume;
		private decimal breakClosePrice;
		private decimal avg;
		private decimal highPrice;
		private decimal lowPrice;
		private string comparePrice = string.Empty;
		private decimal lastSalePrice;
		public int SecurityNumber
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.securityNumber;
			}
		}
		public decimal ProjectedPrice
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.projectedPrice;
			}
		}
		public long ProjectedVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.projectedVolume;
			}
		}
		public decimal BreakClosePrice
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.breakClosePrice;
			}
		}
		public decimal Avg
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.avg;
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
		public string ComparePrice
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.comparePrice;
			}
		}
		public decimal LastSalePrice
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.lastSalePrice;
			}
		}
		public string MessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return "PO";
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public POMessage()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public POMessage(string message)
		{
			this.Unpack(message);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string Pack(int securityNumber, decimal projectedPrice, long projectedVolume, decimal breakClosePrice, decimal lastSalePrice, decimal avg, decimal highPrice, decimal lowPrice, string comparePrice)
		{
			return string.Concat(new object[]
			{
				"PO",
				securityNumber,
				';',
				projectedPrice,
				';',
				breakClosePrice,
				';',
				lastSalePrice,
				';',
				avg,
				';',
				highPrice,
				';',
				lowPrice,
				';',
				comparePrice,
				';',
				projectedVolume
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
				this.projectedPrice = Convert.ToDecimal(array[1]);
				this.breakClosePrice = Convert.ToDecimal(array[2]);
				this.lastSalePrice = Convert.ToDecimal(array[3]);
				this.avg = Convert.ToDecimal(array[4]);
				this.highPrice = Convert.ToDecimal(array[5]);
				this.lowPrice = Convert.ToDecimal(array[6]);
				this.comparePrice = array[7];
				long.TryParse(array[8], out this.projectedVolume);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
