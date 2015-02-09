using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ
{
	public class ChangeOrderMessageFormat
	{
		private string spliterString = "|";
		private long orderNumber;
		private string newAccount;
		private long newPublishVolume;
		private int newTrusteeId;
		private string terminalIp;
		private string entryID;
		private long newVolume;
		private string newPrice = string.Empty;
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
		public string NewAccount
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.newAccount;
			}
		}
		public long NewPublishVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.newPublishVolume;
			}
		}
		public int NewTrusteeId
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.newTrusteeId;
			}
		}
		public string TerminalIp
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.terminalIp;
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
		public long NewVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.newVolume;
			}
		}
		public string NewPrice
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.newPrice;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.newPrice = value;
			}
		}
		public string MessagePacket
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return string.Concat(new object[]
				{
					OrderMessageType.ChangeOrder.ToString(),
					this.spliterString,
					this.orderNumber,
					this.spliterString,
					this.newAccount,
					this.spliterString,
					this.newPublishVolume,
					this.spliterString,
					this.newTrusteeId,
					this.spliterString,
					this.newPrice,
					this.spliterString,
					this.newVolume,
					this.spliterString,
					this.terminalIp,
					this.spliterString,
					this.entryID
				});
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(string orderNumber, string newAccount, long newPubVol, string newTrusteeId, string newPrice, long newVolume, string terminalIp, string entryId)
		{
			this.orderNumber = Convert.ToInt64(orderNumber);
			this.newAccount = newAccount;
			this.entryID = entryId;
			this.newVolume = newVolume;
			this.newPublishVolume = newPubVol;
			this.newPrice = newPrice;
			this.newTrusteeId = Convert.ToInt32(newTrusteeId);
			this.terminalIp = terminalIp;
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
				this.orderNumber = Convert.ToInt64(msgArray[1]);
				this.newAccount = msgArray[2];
				this.newPublishVolume = Convert.ToInt64(msgArray[3]);
				this.newTrusteeId = Convert.ToInt32(msgArray[4].Trim());
				this.newPrice = msgArray[5].Trim();
				this.newVolume = Convert.ToInt64(msgArray[6].Trim());
				this.terminalIp = msgArray[7];
				this.entryID = msgArray[8];
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ChangeOrderMessageFormat()
		{
		}
	}
}
