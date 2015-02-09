using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ
{
	public class ExerciseOrderMessageFormat
	{
		private const char spliter = '|';
		private string spliterString = "|";
		private string securitySymbol;
		private long volume;
		private string account;
		private string senderType;
		private string entryID;
		private string sessionID;
		private string requestID;
		private long orderNumber;
		private bool isForceUpdate;
		public string SecuritySymbol
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.securitySymbol;
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
		public string Account
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.account;
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
					OrderMessageType.ExerciseOrder.ToString(),
					this.spliterString,
					this.securitySymbol,
					this.spliterString,
					this.volume,
					this.spliterString,
					this.account,
					this.spliterString,
					this.entryID,
					this.spliterString,
					this.senderType,
					this.spliterString,
					this.sessionID,
					this.spliterString,
					this.requestID,
					this.spliterString,
					this.orderNumber,
					this.spliterString,
					this.isForceUpdate
				});
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(string securitySymbol, long volume, string account, string endtryId, string senderType, string sessionId, string requestId, long orderNumber, bool isForceUpdate)
		{
			this.securitySymbol = securitySymbol;
			this.volume = volume;
			this.account = account;
			this.entryID = endtryId;
			this.senderType = senderType;
			this.sessionID = sessionId;
			this.requestID = requestId;
			this.orderNumber = orderNumber;
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
				this.securitySymbol = msgArray[1];
				long.TryParse(msgArray[2], out this.volume);
				this.account = msgArray[3];
				this.entryID = msgArray[4];
				this.senderType = msgArray[5];
				this.sessionID = msgArray[6];
				this.requestID = msgArray[7];
				long.TryParse(msgArray[8], out this.orderNumber);
				bool.TryParse(msgArray[9], out this.isForceUpdate);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ExerciseOrderMessageFormat()
		{
		}
	}
}
