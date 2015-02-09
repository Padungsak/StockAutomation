using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ
{
	public class PuttDealConfrimMessageFormat
	{
		private const char spliter = '|';
		private string spliterString = "|";
		private int firm;
		private string side;
		private long dealNumber;
		private int contraFirm;
		private long volume;
		private decimal price;
		private long confirmNumber;
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
		public long DealNumber
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.dealNumber;
			}
		}
		public int ContraFirm
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.contraFirm;
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
		public long ConfirmNumber
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
					OrderMessageType.PuttDealConfirm.ToString(),
					this.spliterString,
					this.firm,
					this.spliterString,
					this.side,
					this.spliterString,
					this.dealNumber,
					this.spliterString,
					this.contraFirm,
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
		public string Pack(int firm, string side, long dealNumber, int contraFirm, long volume, decimal price, long confirmNumber)
		{
			this.firm = firm;
			this.side = side;
			this.dealNumber = dealNumber;
			this.contraFirm = contraFirm;
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
				this.side = msgArray[2];
				this.dealNumber = Convert.ToInt64(msgArray[3]);
				this.contraFirm = Convert.ToInt32(msgArray[4]);
				this.volume = Convert.ToInt64(msgArray[5]);
				this.price = Convert.ToDecimal(msgArray[6]);
				this.confirmNumber = Convert.ToInt64(msgArray[7]);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public PuttDealConfrimMessageFormat()
		{
		}
	}
}
