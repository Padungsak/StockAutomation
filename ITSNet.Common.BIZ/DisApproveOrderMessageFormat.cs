using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ
{
	public class DisApproveOrderMessageFormat
	{
		private const char spliter = '|';
		private string spliterString = "|";
		private long orderNumber;
		private string entryID;
		private string terminalIP;
		private string sessionID;
		private string requestID;
		private bool isForceUpdate;
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
		public string EntryID
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.entryID;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.entryID = value;
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
		public string SessionID
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.sessionID;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.sessionID = value;
			}
		}
		public string RequestID
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.requestID;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.requestID = value;
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
					OrderMessageType.DisApprove.ToString(),
					this.spliterString,
					this.orderNumber.ToString(),
					this.spliterString,
					this.entryID,
					this.spliterString,
					this.terminalIP,
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
		public string Pack(long orderNumber, string endtryId, string terminalIp, string sessionId, string requestId, bool isForceUpdate)
		{
			this.orderNumber = orderNumber;
			this.entryID = endtryId;
			this.terminalIP = terminalIp;
			this.sessionID = sessionId;
			this.requestID = requestId;
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
				long.TryParse(msgArray[1], out this.orderNumber);
				this.entryID = msgArray[2];
				this.terminalIP = msgArray[3];
				this.sessionID = msgArray[4];
				this.requestID = msgArray[5];
				bool.TryParse(msgArray[6], out this.isForceUpdate);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public DisApproveOrderMessageFormat()
		{
		}
	}
}
