using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ
{
	public class DGWDealConfirmationMessageFormat
	{
		private const char spliter = '|';
		private string spliterString = "|";
		private int firm;
		private string side;
		private long orderNumber;
		private decimal price;
		private string entryDate;
		private long volume;
		private int confirmNumber;
		private long publishVolume;
		private long refOrderNo;
		public int Firm
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.firm;
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
		public long OrderNumber
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.orderNumber;
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
		public string EntryDate
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.entryDate;
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
		public int ConfirmNumber
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.confirmNumber;
			}
		}
		public long PublishVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.publishVolume;
			}
		}
		public long RefOrderNo
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.refOrderNo;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.refOrderNo = value;
			}
		}
		private string MessagePacket
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return string.Concat(new object[]
				{
					OrderMessageType.DealConfirmation.ToString(),
					this.spliterString,
					this.firm,
					this.spliterString,
					this.side,
					this.spliterString,
					this.orderNumber,
					this.spliterString,
					this.entryDate,
					this.spliterString,
					this.volume,
					this.spliterString,
					this.price,
					this.spliterString,
					this.confirmNumber,
					this.spliterString,
					this.publishVolume
				});
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(int firm, string side, long orderNumber, string entryDate, long volume, decimal price, int confirmNumber, long publishVolume)
		{
			this.firm = firm;
			this.side = side;
			this.orderNumber = orderNumber;
			this.entryDate = entryDate;
			this.volume = volume;
			this.price = price;
			this.confirmNumber = confirmNumber;
			this.publishVolume = publishVolume;
			return this.MessagePacket;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(int firm, string side, long orderNumber, long refOrderNo, string entryDate, long volume, decimal price, int confirmNumber, long publishVolume)
		{
			this.firm = firm;
			this.side = side;
			this.orderNumber = orderNumber;
			this.entryDate = entryDate;
			this.volume = volume;
			this.price = price;
			this.confirmNumber = confirmNumber;
			this.publishVolume = publishVolume;
			this.refOrderNo = refOrderNo;
			return this.MessagePacket + this.spliterString + refOrderNo;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Unpack(string message)
		{
			try
			{
				string[] msgArray = message.Split(new char[]
				{
					'|'
				});
				this.Unpack(msgArray);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Unpack(string[] msgArray)
		{
			try
			{
				this.firm = Convert.ToInt32(msgArray[1]);
				this.side = msgArray[2].ToString();
				this.orderNumber = Convert.ToInt64(msgArray[3]);
				this.entryDate = msgArray[4].ToString();
				this.volume = Convert.ToInt64(msgArray[5]);
				this.price = Convert.ToDecimal(msgArray[6]);
				int.TryParse(msgArray[7], out this.confirmNumber);
				long.TryParse(msgArray[8], out this.publishVolume);
				if (msgArray.Length > 9)
				{
					long.TryParse(msgArray[9], out this.refOrderNo);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public DGWDealConfirmationMessageFormat()
		{
		}
	}
}
