using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ
{
	public class NewOrderMessageFormat
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
		private string senderType;
		private string entryID;
		private string commandType = string.Empty;
		private long orderNumber;
		private string sourceId = string.Empty;
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
		public string CommandType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.commandType;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.commandType = value;
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
		public string SourceId
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.sourceId;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.sourceId = value;
			}
		}
		public string MessagePacket
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return string.Concat(new object[]
				{
					OrderMessageType.NewOrder.ToString(),
					this.spliterString,
					this.securitySymbol,
					this.spliterString,
					this.side,
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
					this.senderType,
					this.spliterString,
					this.entryID,
					this.spliterString,
					this.commandType,
					this.spliterString,
					this.orderNumber
				});
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(string securitySymbol, string side, long volume, string priceToSET, string account, long publishVolume, string conditions, string trusteeId, string senderType, string entryId, string commandType, long forceOrderNumber)
		{
			this.securitySymbol = securitySymbol;
			this.side = side;
			this.volume = volume;
			this.priceToSET = priceToSET;
			this.account = account;
			this.publishVolume = publishVolume;
			this.conditions = conditions;
			this.trusteeID = trusteeId;
			this.senderType = senderType;
			this.entryID = entryId;
			this.orderNumber = forceOrderNumber;
			return this.MessagePacket;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(string securitySymbol, string side, long volume, string priceToSET, string account, long publishVolume, string conditions, string trusteeId, string senderType, string entryId, string commandType, long forceOrderNumber, string sourceId)
		{
			this.securitySymbol = securitySymbol;
			this.side = side;
			this.volume = volume;
			this.priceToSET = priceToSET;
			this.account = account;
			this.publishVolume = publishVolume;
			this.conditions = conditions;
			this.trusteeID = trusteeId;
			this.senderType = senderType;
			this.entryID = entryId;
			this.orderNumber = forceOrderNumber;
			return this.MessagePacket + this.spliterString + sourceId;
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
				this.securitySymbol = msgArray[1];
				this.side = msgArray[2];
				long.TryParse(msgArray[3], out this.volume);
				this.priceToSET = msgArray[4].Trim();
				this.account = msgArray[5];
				long.TryParse(msgArray[6], out this.publishVolume);
				this.conditions = msgArray[7];
				this.trusteeID = msgArray[8];
				this.senderType = msgArray[9];
				this.entryID = msgArray[10];
				this.commandType = msgArray[11];
				long.TryParse(msgArray[12], out this.orderNumber);
				if (msgArray.Length > 13)
				{
					this.sourceId = msgArray[13];
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public NewOrderMessageFormat()
		{
		}
	}
}
