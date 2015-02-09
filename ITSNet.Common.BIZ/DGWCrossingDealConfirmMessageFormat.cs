using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ
{
	public class DGWCrossingDealConfirmMessageFormat
	{
		private const char spliter = '|';
		private string spliterString = "|";
		private int firm;
		private long orderNumberBuy;
		private string orderEntryDateBuy;
		private long orderNumberSell;
		private string orderEntryDateSell;
		private long volume;
		private decimal price;
		private int confirmNumber;
		private long publishVolumeBuy;
		private long publishVolumeSell;
		public int Firm
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.firm;
			}
		}
		public long OrderNumberBuy
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.orderNumberBuy;
			}
		}
		public string OrderEntryDateBuy
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.orderEntryDateBuy;
			}
		}
		public long OrderNumberSell
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.orderNumberSell;
			}
		}
		public string OrderEntryDateSell
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.orderEntryDateSell;
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
		public int ConfirmNumber
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.confirmNumber;
			}
		}
		public long PublishVolumeBuy
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.publishVolumeBuy;
			}
		}
		public long PublishVolumeSell
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.publishVolumeSell;
			}
		}
		public string MessagePacket
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return string.Concat(new object[]
				{
					OrderMessageType.CrossingDealConfirm.ToString(),
					this.spliterString,
					this.firm,
					this.spliterString,
					this.orderNumberBuy,
					this.spliterString,
					this.orderEntryDateBuy,
					this.spliterString,
					this.orderNumberSell,
					this.spliterString,
					this.orderEntryDateSell,
					this.spliterString,
					this.volume,
					this.spliterString,
					this.price,
					this.spliterString,
					this.confirmNumber,
					this.spliterString,
					this.publishVolumeBuy,
					this.spliterString,
					this.publishVolumeSell
				});
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(int firm, long orderNumberBuy, string orderEntryDataBuy, long orderNumberSell, string orderEntryDataSell, long volume, decimal price, int confirmNumber, long publishVolumeBuy, long publishVolumeSell)
		{
			this.firm = firm;
			this.orderNumberBuy = orderNumberBuy;
			this.orderEntryDateBuy = orderEntryDataBuy;
			this.orderNumberSell = orderNumberSell;
			this.orderEntryDateSell = orderEntryDataSell;
			this.volume = volume;
			this.price = price;
			this.confirmNumber = confirmNumber;
			this.publishVolumeBuy = publishVolumeBuy;
			this.publishVolumeSell = publishVolumeSell;
			return this.MessagePacket;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Unpack(string message)
		{
			string[] msgArray = message.Split(new char[]
			{
				'|'
			});
			this.Unpack(msgArray);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Unpack(string[] msgArray)
		{
			try
			{
				this.firm = Convert.ToInt32(msgArray[1]);
				this.orderNumberBuy = Convert.ToInt64(msgArray[2]);
				this.orderEntryDateBuy = msgArray[3].ToString();
				this.orderNumberSell = Convert.ToInt64(msgArray[4]);
				this.orderEntryDateSell = msgArray[5].ToString();
				this.volume = Convert.ToInt64(msgArray[6]);
				this.price = Convert.ToDecimal(msgArray[7]);
				this.confirmNumber = Convert.ToInt32(msgArray[8]);
				this.publishVolumeBuy = Convert.ToInt64(msgArray[9]);
				this.publishVolumeSell = Convert.ToInt64(msgArray[10]);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public DGWCrossingDealConfirmMessageFormat()
		{
		}
	}
}
