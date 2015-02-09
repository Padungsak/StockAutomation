using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.RealtimeMessage
{
	public class OrderInfoClient : IBroadcastMessage
	{
		private const char spliter = ';';
		private string messageType = "0I";
		private long orderNumber;
		private string side;
		private string stock;
		private string priceToSET;
		private decimal priceForCal;
		private long volume;
		private long matchedVolume;
		private decimal matchedValue;
		private long publicVolume;
		private string account;
		private string trusteeID;
		private string status;
		private string quote;
		private string board;
		private string pcFlag;
		private decimal approveCredit;
		private string entryID = string.Empty;
		private string reserve1 = string.Empty;
		private string reserve2 = string.Empty;
		private string orgMessageType = string.Empty;
		private long lastVolumeInCase;
		private string lastPriceInCase = string.Empty;
		private string approverId = string.Empty;
		private string orderDate = string.Empty;
		private string orderTime;
		public long OrderNumber
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.orderNumber;
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
		public string Stock
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.stock;
			}
		}
		public string PriceToSET
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.priceToSET;
			}
		}
		public decimal PriceForCal
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.priceForCal;
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
		public long MatchedVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.matchedVolume;
			}
		}
		public decimal MatchedValue
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.matchedValue;
			}
		}
		public long PublicVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.publicVolume;
			}
		}
		public string Account
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.account;
			}
		}
		public string TrusteeID
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.trusteeID;
			}
		}
		public string Status
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.status;
			}
		}
		public string Quote
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.quote;
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
		public string PcFlag
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.pcFlag;
			}
		}
		public string FullStock
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (this.trusteeID.Trim() != "")
				{
					return this.stock + " (" + this.trusteeID + ")";
				}
				return this.stock;
			}
		}
		public decimal ApproveCredit
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.approveCredit;
			}
		}
		public string EntryID
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.entryID;
			}
		}
		public string Reserve1
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.reserve1;
			}
		}
		public string Reserve2
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.reserve2;
			}
		}
		public string OriginalMessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.orgMessageType;
			}
		}
		public string StatusDisplay
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (this.approveCredit > 0m && this.status != "R")
				{
					return this.status + "A";
				}
				return this.status;
			}
		}
		public long LastVolumeInCase
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.lastVolumeInCase;
			}
		}
		public string LastPriceInCase
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.lastPriceInCase;
			}
		}
		public string ApproverId
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.approverId;
			}
		}
		public string OrderDate
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.orderDate;
			}
		}
		public string OrderTime
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.orderTime;
			}
		}
		public string MessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.messageType;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public OrderInfoClient()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public OrderInfoClient(string message)
		{
			this.Unpack(message);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Unpack(string message)
		{
			try
			{
				this.messageType = message.Substring(0, 2);
				string[] array = message.Substring(2).Split(new char[]
				{
					';'
				});
				this.entryID = array[0];
				this.reserve1 = array[1];
				this.reserve2 = array[2];
				long.TryParse(array[3], out this.orderNumber);
				this.side = array[4].Trim();
				this.stock = array[5].Trim();
				long.TryParse(array[6], out this.volume);
				this.priceToSET = array[7];
				decimal.TryParse(array[8], out this.priceForCal);
				long.TryParse(array[9], out this.matchedVolume);
				decimal.TryParse(array[10], out this.matchedValue);
				long.TryParse(array[11], out this.publicVolume);
				this.account = array[12].Trim();
				this.trusteeID = array[13];
				this.board = array[14];
				this.status = array[15].Trim();
				this.quote = array[16];
				this.orderTime = array[17];
				this.pcFlag = array[18];
				decimal.TryParse(array[19], out this.approveCredit);
				this.orgMessageType = array[20];
				long.TryParse(array[21], out this.lastVolumeInCase);
				this.lastPriceInCase = array[22];
				this.approverId = array[23];
				if (array.Length > 24)
				{
					this.orderDate = array[24].Trim();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(string entryId, string reserve1, string reserve2, long orderNumber, string side, string stock, long volume, string priceToSET, decimal priceForCal, long matchedVolume, decimal matcedValue, long publishedVolume, string account, string trusteeId, string board, string status, string quote, string orderTime, string pcFlag, decimal approveCredit, string originalMessageType, long lastVolumeInCase, string lastPriceInCase, string approverId, string sendDate)
		{
			return string.Concat(new object[]
			{
				this.messageType,
				entryId,
				';',
				reserve1,
				';',
				reserve2,
				';',
				orderNumber,
				';',
				side,
				';',
				stock,
				';',
				volume,
				';',
				priceToSET,
				';',
				priceForCal,
				';',
				matchedVolume,
				';',
				matcedValue,
				';',
				publishedVolume,
				';',
				account,
				';',
				trusteeId,
				';',
				board,
				';',
				status,
				';',
				quote,
				';',
				orderTime,
				';',
				pcFlag,
				';',
				approveCredit,
				';',
				originalMessageType,
				';',
				lastVolumeInCase,
				';',
				lastPriceInCase,
				';',
				approverId,
				';',
				sendDate
			});
		}
	}
}
