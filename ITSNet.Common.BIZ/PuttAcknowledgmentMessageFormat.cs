using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ
{
	public class PuttAcknowledgmentMessageFormat
	{
		private const char spliter = '|';
		private string spliterString = "|";
		private int firm;
		private string traderIDReceive;
		private int contraFirm;
		private string traderIDContra;
		private long confirmNumber;
		private string stock;
		private long volume;
		private decimal price;
		private string board;
		private string side;
		public int Firm
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.firm;
			}
		}
		public string TraderIDReceive
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.traderIDReceive;
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
		public string TraderIDContra
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.traderIDContra;
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
		public string Board
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.board;
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
					OrderMessageType.PuttDealAcknowledgment.ToString(),
					this.spliterString,
					this.firm,
					this.spliterString,
					this.traderIDReceive,
					this.spliterString,
					this.side,
					this.spliterString,
					this.contraFirm,
					this.spliterString,
					this.traderIDContra,
					this.spliterString,
					this.stock,
					this.spliterString,
					this.volume,
					this.spliterString,
					this.price,
					this.spliterString,
					this.board,
					this.spliterString,
					this.confirmNumber
				});
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(int firm, string traderIDReceive, string side, int contraFirm, string traderIDContra, string stock, long volume, decimal price, string board, long confirmNumber)
		{
			this.firm = firm;
			this.traderIDReceive = traderIDReceive;
			this.side = side;
			this.contraFirm = contraFirm;
			this.traderIDContra = traderIDContra;
			this.stock = stock;
			this.volume = volume;
			this.price = price;
			this.board = board;
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
				this.traderIDReceive = msgArray[2];
				this.side = msgArray[3];
				this.contraFirm = Convert.ToInt32(msgArray[4]);
				this.traderIDContra = msgArray[5];
				this.stock = msgArray[6];
				this.volume = Convert.ToInt64(msgArray[7]);
				this.price = Convert.ToDecimal(msgArray[8]);
				this.board = msgArray[9];
				this.confirmNumber = Convert.ToInt64(msgArray[10]);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public PuttAcknowledgmentMessageFormat()
		{
		}
	}
}
