using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ
{
	public class PuttSendApproveMessageFormat
	{
		private const char spliter = '|';
		private string spliterString = "|";
		private long dealNumber;
		private string replyCode;
		private string accountBuyer;
		private string trusteeIDBuyer;
		private string entryID;
		private string sessionID;
		private string requestID;
		private bool isForceUpdate;
		public long DealNumber
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.dealNumber;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.dealNumber = value;
			}
		}
		public string ReplyCode
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.replyCode;
			}
		}
		public string AccountBuyer
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.accountBuyer;
			}
		}
		public string TrusteeIDBuyer
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.trusteeIDBuyer;
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
		public string MessagePacket
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return string.Concat(new object[]
				{
					OrderMessageType.PuttSendApprove.ToString(),
					this.spliterString,
					this.dealNumber,
					this.spliterString,
					this.replyCode,
					this.spliterString,
					this.accountBuyer,
					this.spliterString,
					this.trusteeIDBuyer,
					this.spliterString,
					this.entryID,
					this.spliterString,
					this.sessionID,
					this.spliterString,
					this.requestID,
					this.spliterString,
					this.isForceUpdate
				});
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(long dealNumber, string replyCode, string accountBuyer, string trusteeIDBuyer, string entryID, string sessionID, string requestID, bool isForceUpdate)
		{
			this.dealNumber = dealNumber;
			this.replyCode = replyCode;
			this.accountBuyer = accountBuyer;
			this.trusteeIDBuyer = trusteeIDBuyer;
			this.entryID = entryID;
			this.sessionID = sessionID;
			this.requestID = requestID;
			this.isForceUpdate = isForceUpdate;
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
				this.dealNumber = (long)Convert.ToInt32(msgArray[1]);
				this.replyCode = msgArray[2];
				this.accountBuyer = msgArray[3];
				this.trusteeIDBuyer = msgArray[4];
				this.entryID = msgArray[5];
				this.sessionID = msgArray[6];
				this.requestID = msgArray[7];
				this.isForceUpdate = Convert.ToBoolean(msgArray[8]);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public PuttSendApproveMessageFormat()
		{
		}
	}
}
