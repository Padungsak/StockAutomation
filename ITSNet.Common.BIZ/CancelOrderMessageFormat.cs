using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ
{
	public class CancelOrderMessageFormat
	{
		private const char spliter = '|';
		private string spliterString = "|";
		private long orderNumber;
		private string cancelEntryId;
		private string senderType;
		private string terminalIP;
		private string sessionId;
		private string requestId;
		private bool isForceUpdate;
		private string pinCode = string.Empty;
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
		public string CancelEntryId
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.cancelEntryId;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.cancelEntryId = value;
			}
		}
		public string SenderType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.senderType;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.senderType = value;
			}
		}
		public string TerminalIP
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.terminalIP;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.terminalIP = value;
			}
		}
		public string SessionId
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.sessionId;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.sessionId = value;
			}
		}
		public string RequestId
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.requestId;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.requestId = value;
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
		public string MessagePacket
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return string.Concat(new object[]
				{
					OrderMessageType.CancelOrder.ToString(),
					this.spliterString,
					this.orderNumber.ToString(),
					this.spliterString,
					this.cancelEntryId,
					this.spliterString,
					this.senderType,
					this.spliterString,
					this.terminalIP,
					this.spliterString,
					this.sessionId,
					this.spliterString,
					this.requestId,
					this.spliterString,
					this.isForceUpdate,
					this.spliterString,
					this.pinCode
				});
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(long orderNumber, string cancelEntryId, string senderType, string terminalIp, string sessionId, string requestId, bool isForceUpdate, string pinCode)
		{
			this.orderNumber = orderNumber;
			this.cancelEntryId = cancelEntryId;
			this.senderType = senderType;
			this.terminalIP = terminalIp;
			this.sessionId = sessionId;
			this.requestId = requestId;
			this.isForceUpdate = isForceUpdate;
			this.pinCode = pinCode;
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
				long.TryParse(msgArray[1], out this.orderNumber);
				this.cancelEntryId = msgArray[2];
				this.senderType = msgArray[3];
				this.terminalIP = msgArray[4];
				this.sessionId = msgArray[5];
				this.requestId = msgArray[6];
				bool.TryParse(msgArray[7], out this.isForceUpdate);
				this.pinCode = msgArray[8];
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public CancelOrderMessageFormat()
		{
		}
	}
}
