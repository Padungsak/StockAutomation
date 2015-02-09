using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ
{
	public class AfterCloseOrderMessageFormat
	{
		private const char spliter = '|';
		private string spliterString = "|";
		private long orderNumber;
		private string securitySymbol;
		private string side;
		private long volume;
		private string priceToSET;
		private string account;
		private string trusteeID;
		private string senderType;
		private string entryID;
		private string sessionID;
		private string requestID;
		private bool isForceUpdate;
		private string pinCode = string.Empty;
		private string orderFlag = string.Empty;
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
		public string TrusteeID
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.trusteeID;
			}
		}
		public string SenderType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.senderType;
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
		public string SessionID
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.sessionID;
			}
		}
		public string RequestID
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.requestID;
			}
		}
		public bool IsForceUpdate
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isForceUpdate;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.isForceUpdate = value;
			}
		}
		public string PinCode
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.pinCode;
			}
		}
		public string OrderFlag
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.orderFlag;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.orderFlag = value;
			}
		}
		public string MessagePacket
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return string.Concat(new object[]
				{
					OrderMessageType.OrderAfterClose.ToString(),
					this.spliterString,
					this.side,
					this.spliterString,
					this.securitySymbol,
					this.spliterString,
					this.trusteeID,
					this.spliterString,
					this.volume,
					this.spliterString,
					this.priceToSET,
					this.spliterString,
					this.account,
					this.spliterString,
					this.senderType,
					this.spliterString,
					this.entryID,
					this.spliterString,
					this.sessionID,
					this.spliterString,
					this.requestID,
					this.spliterString,
					this.pinCode,
					this.spliterString,
					this.orderNumber,
					this.spliterString,
					this.orderFlag,
					this.spliterString,
					this.isForceUpdate
				});
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(string securitySymbol, string side, long volume, string priceToSET, string account, string trusteeId, string senderType, string entryId, string sessionId, string requestId, string pinCode, long forceOrderNumber, string orderFlag, bool isForceUpdate)
		{
			this.securitySymbol = securitySymbol;
			this.side = side;
			this.volume = volume;
			this.priceToSET = priceToSET;
			this.account = account;
			this.trusteeID = trusteeId;
			this.senderType = senderType;
			this.entryID = entryId;
			this.sessionID = sessionId;
			this.requestID = requestId;
			this.pinCode = pinCode;
			this.orderNumber = forceOrderNumber;
			this.orderFlag = orderFlag;
			this.isForceUpdate = isForceUpdate;
			return this.MessagePacket;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void UnPack(string content)
		{
			string[] msgArray = content.Split(new char[]
			{
				'|'
			});
			this.UnPack(msgArray);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void UnPack(string[] msgArray)
		{
			try
			{
				this.side = msgArray[1];
				this.securitySymbol = msgArray[2];
				this.trusteeID = msgArray[3];
				long.TryParse(msgArray[4], out this.volume);
				this.priceToSET = msgArray[5].Trim();
				this.account = msgArray[6];
				this.senderType = msgArray[7];
				this.entryID = msgArray[8];
				this.sessionID = msgArray[9];
				this.requestID = msgArray[10];
				this.pinCode = msgArray[11];
				this.orderNumber = 0L;
				this.isForceUpdate = false;
				long.TryParse(msgArray[12], out this.orderNumber);
				this.orderFlag = msgArray[13];
				bool.TryParse(msgArray[14], out this.isForceUpdate);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public AfterCloseOrderMessageFormat()
		{
		}
	}
}
