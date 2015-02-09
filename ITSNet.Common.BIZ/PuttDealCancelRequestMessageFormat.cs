using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ
{
	public class PuttDealCancelRequestMessageFormat
	{
		private const char spliter = '|';
		private string spliterString = "|";
		private int firm;
		private int contraFirm;
		private string traderId;
		private long confirmNumber;
		private string stock;
		private string side;
		public int Firm
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.firm;
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
		public string TraderId
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.traderId;
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
		public string Stock
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.stock;
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
		public string MessagePacket
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return string.Concat(new object[]
				{
					OrderMessageType.PuttDealCancelRequest.ToString(),
					this.spliterString,
					this.firm,
					this.spliterString,
					this.contraFirm,
					this.spliterString,
					this.traderId,
					this.spliterString,
					this.confirmNumber,
					this.spliterString,
					this.stock,
					this.spliterString,
					this.side
				});
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(int firm, int contraFirm, string traderId, long confirmNumber, string stock, string side)
		{
			this.firm = firm;
			this.contraFirm = contraFirm;
			this.traderId = traderId;
			this.confirmNumber = confirmNumber;
			this.stock = stock;
			this.side = side;
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
				this.contraFirm = Convert.ToInt32(msgArray[2]);
				this.traderId = msgArray[3];
				this.confirmNumber = Convert.ToInt64(msgArray[4]);
				this.stock = msgArray[5];
				this.side = msgArray[6];
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public PuttDealCancelRequestMessageFormat()
		{
		}
	}
}
