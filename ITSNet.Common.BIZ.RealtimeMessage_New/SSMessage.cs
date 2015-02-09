using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.RealtimeMessage_New
{
	public class SSMessage : IBroadcastMessage
	{
		private const char spliter = ';';
		private int securityNumber;
		private int boardLot;
		private decimal ceiling;
		private decimal floor;
		private string displayFlag;
		private string marketId;
		private decimal priorPrice;
		private int sectorNumber;
		private string securityType;
		public int SecurityNumber
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.securityNumber;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.securityNumber = value;
			}
		}
		public int BoardLot
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.boardLot;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.boardLot = value;
			}
		}
		public decimal Ceiling
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.ceiling;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.ceiling = value;
			}
		}
		public decimal Floor
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.floor;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.floor = value;
			}
		}
		public string DisplayFlag
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.displayFlag;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.displayFlag = value;
			}
		}
		public string MarketId
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.marketId;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.marketId = value;
			}
		}
		public decimal PriorPrice
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.priorPrice;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.priorPrice = value;
			}
		}
		public int SectorNumber
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.sectorNumber;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.sectorNumber = value;
			}
		}
		public string SecurityType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.securityType;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.securityType = value;
			}
		}
		public string MessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return "SS";
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public SSMessage()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public SSMessage(string message)
		{
			this.Unpack(message);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string Pack(int securityNumber, string marketId, int boardLot, decimal prior, decimal ceiling, decimal floor, string displayFlag, int sectorNumber, string securityType)
		{
			return string.Concat(new object[]
			{
				"SS",
				securityNumber,
				';',
				marketId,
				';',
				boardLot,
				';',
				prior,
				';',
				ceiling,
				';',
				floor,
				';',
				displayFlag,
				';',
				sectorNumber,
				';',
				securityType
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
				this.marketId = array[1];
				this.boardLot = Convert.ToInt32(array[2]);
				this.priorPrice = Convert.ToDecimal(array[3]);
				this.ceiling = Convert.ToDecimal(array[4]);
				this.floor = Convert.ToDecimal(array[5]);
				this.displayFlag = array[6];
				this.sectorNumber = Convert.ToInt32(array[7]);
				this.securityType = array[8];
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
