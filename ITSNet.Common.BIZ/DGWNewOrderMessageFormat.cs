using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ
{
	public class DGWNewOrderMessageFormat
	{
		private const char spliter = '|';
		private string spliterString = "|";
		private string securitySymbol;
		private string side;
		private long volume;
		private string priceToSET;
		private string account;
		private long publishVolume;
		private string conditions;
		private string trusteeID;
		private string entryID;
		private long orderNumber;
		private string orderStatus = string.Empty;
		private decimal priceForCalculate;
		private string approverId = string.Empty;
		private string entryTime = string.Empty;
		public string SecuritySymbol
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.securitySymbol;
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
		public long Volume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.volume;
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
		public string Account
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.account;
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
		public string Conditions
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.conditions;
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
		public string EntryID
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.entryID;
			}
		}
		public long OrderNumber
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.orderNumber;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.orderNumber = value;
			}
		}
		public string OrderStatus
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.orderStatus;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.orderStatus = value;
			}
		}
		public decimal PriceForCalculate
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.priceForCalculate;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.priceForCalculate = value;
			}
		}
		public string ApproverId
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.approverId;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.approverId = value;
			}
		}
		public string EntryTime
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.entryTime;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.entryTime = value;
			}
		}
		private string MessagePacket
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return string.Concat(new object[]
				{
					OrderMessageType.NewOrder.ToString(),
					this.spliterString,
					this.orderNumber,
					this.spliterString,
					this.side,
					this.spliterString,
					this.securitySymbol,
					this.spliterString,
					this.volume,
					this.spliterString,
					this.priceToSET,
					this.spliterString,
					this.account,
					this.spliterString,
					this.publishVolume,
					this.spliterString,
					this.conditions,
					this.spliterString,
					this.trusteeID,
					this.spliterString,
					this.entryID,
					this.spliterString,
					this.orderStatus,
					this.spliterString,
					this.priceForCalculate,
					this.spliterString,
					this.approverId,
					this.spliterString,
					this.entryTime
				});
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(long orderNumber, string side, string securitySymbol, long volume, string priceToSET, string account, long publishVolume, string conditions, string trusteeId, string entryId, string orderStatus, decimal priceForCalculate, string approverId, string entryTime)
		{
			this.orderNumber = orderNumber;
			this.side = side;
			this.securitySymbol = securitySymbol;
			this.volume = volume;
			this.priceToSET = priceToSET;
			this.account = account;
			this.publishVolume = publishVolume;
			this.conditions = conditions;
			this.trusteeID = trusteeId;
			this.entryID = entryId;
			this.orderStatus = orderStatus;
			this.priceForCalculate = priceForCalculate;
			this.approverId = approverId;
			this.entryTime = entryTime;
			return this.MessagePacket;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void UnPack(string content)
		{
			try
			{
				string[] msgArray = content.Split(new char[]
				{
					'|'
				});
				this.UnPack(msgArray);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void UnPack(string[] msgArray)
		{
			try
			{
				long.TryParse(msgArray[1], out this.orderNumber);
				this.side = msgArray[2];
				this.securitySymbol = msgArray[3];
				long.TryParse(msgArray[4], out this.volume);
				this.priceToSET = msgArray[5].Trim();
				this.account = msgArray[6];
				long.TryParse(msgArray[7], out this.publishVolume);
				this.conditions = msgArray[8];
				this.trusteeID = msgArray[9];
				this.entryID = msgArray[10];
				this.orderStatus = msgArray[11];
				decimal.TryParse(msgArray[12], out this.priceForCalculate);
				this.approverId = msgArray[13];
				this.entryTime = msgArray[14];
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public DGWNewOrderMessageFormat()
		{
		}
	}
}
