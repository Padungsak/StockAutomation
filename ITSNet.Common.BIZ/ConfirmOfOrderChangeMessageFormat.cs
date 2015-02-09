using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ
{
	public class ConfirmOfOrderChangeMessageFormat
	{
		private const char spliter = '|';
		private string spliterString = "|";
		private int firm;
		private long orderNumber;
		private string orderEntryDate;
		private string newAccount;
		private string newAccType;
		private long newPubVol;
		private string newPrice;
		private int newtrusteeID;
		private long newVolume;
		private string sourceId = string.Empty;
		public int Firm
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.firm;
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
		public string OrderEntryDate
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.orderEntryDate;
			}
		}
		public string NewAccount
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.newAccount;
			}
		}
		public string NewAccType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.newAccType;
			}
		}
		public long NewPubVol
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.newPubVol;
			}
		}
		public string NewPrice
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.newPrice;
			}
		}
		public int NewTrusteeID
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.newtrusteeID;
			}
		}
		public long NewVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.newVolume;
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
					OrderMessageType.ConfirmOrderChange.ToString(),
					this.spliterString,
					this.firm,
					this.spliterString,
					this.orderNumber,
					this.spliterString,
					this.orderEntryDate,
					this.spliterString,
					this.newAccount,
					this.spliterString,
					this.newAccType,
					this.spliterString,
					this.newPubVol,
					this.spliterString,
					this.newPrice,
					this.spliterString,
					this.newtrusteeID,
					this.spliterString,
					this.newVolume
				});
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(int firm, long orderNumber, string orderEntryDate, string newAccount, string newAccType, long newPubVol, string newPrice, int newTrusteeID, long newVolume)
		{
			this.firm = firm;
			this.orderNumber = orderNumber;
			this.orderEntryDate = orderEntryDate;
			this.newAccount = newAccount;
			this.newAccType = newAccType;
			this.newPubVol = newPubVol;
			this.newPrice = newPrice;
			this.newtrusteeID = newTrusteeID;
			this.newVolume = newVolume;
			return this.MessagePacket;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(int firm, long orderNumber, string orderEntryDate, string newAccount, string newAccType, long newPubVol, string newPrice, int newTrusteeID, long newVolume, string sourceId)
		{
			this.firm = firm;
			this.orderNumber = orderNumber;
			this.orderEntryDate = orderEntryDate;
			this.newAccount = newAccount;
			this.newAccType = newAccType;
			this.newPubVol = newPubVol;
			this.newPrice = newPrice;
			this.newtrusteeID = newTrusteeID;
			this.newVolume = newVolume;
			this.sourceId = sourceId;
			return this.MessagePacket + this.spliterString + sourceId;
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
				this.orderNumber = Convert.ToInt64(msgArray[2]);
				this.orderEntryDate = msgArray[3];
				this.newAccount = msgArray[4];
				this.newAccType = msgArray[5];
				long.TryParse(msgArray[6], out this.newPubVol);
				this.newPrice = msgArray[7];
				int.TryParse(msgArray[8], out this.newtrusteeID);
				long.TryParse(msgArray[9], out this.newVolume);
				if (msgArray.Length > 10)
				{
					this.sourceId = msgArray[10];
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ConfirmOfOrderChangeMessageFormat()
		{
		}
	}
}
