using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ
{
	public class DealConfirmationMessageFormat
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
		public string MessagePacket
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
					this.confirmNumber
				});
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(int firm, string side, long orderNumber, string entryDate, long volume, decimal price, int confirmNumber)
		{
			this.firm = firm;
			this.side = side;
			this.orderNumber = orderNumber;
			this.entryDate = entryDate;
			this.volume = volume;
			this.price = price;
			this.confirmNumber = confirmNumber;
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
				this.side = msgArray[2].ToString();
				this.orderNumber = Convert.ToInt64(msgArray[3]);
				this.entryDate = msgArray[4].ToString();
				this.volume = Convert.ToInt64(msgArray[5]);
				this.price = Convert.ToDecimal(msgArray[6]);
				int.TryParse(msgArray[7], out this.confirmNumber);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public DealConfirmationMessageFormat()
		{
		}
	}
}
