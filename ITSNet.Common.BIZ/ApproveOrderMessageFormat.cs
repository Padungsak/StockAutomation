using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ
{
	public class ApproveOrderMessageFormat
	{
		private const char spliter = '|';
		private string spliterString = "|";
		private long orderNumber;
		private string entryID;
		private string terminalIP;
		private string sessionID;
		private string requestID;
		private bool isForceUpdate;
		private string status = string.Empty;
		private decimal priceForCalulate;
		private decimal approveCredit;
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
		public string Status
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.status;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.status = value;
			}
		}
		public decimal PriceForCalulate
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.priceForCalulate;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.priceForCalulate = value;
			}
		}
		public decimal ApproveCredit
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.approveCredit;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.approveCredit = value;
			}
		}
		public string MessagePacket
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return string.Concat(new object[]
				{
					OrderMessageType.Approve.ToString(),
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
					this.status,
					this.spliterString,
					this.priceForCalulate,
					this.spliterString,
					this.approveCredit,
					this.spliterString,
					this.isForceUpdate
				});
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(long orderNumber, string endtryId, string terminalIp, string sessionId, string requestId, string status, decimal priceForCalulate, decimal approveCredit, bool isForceUpdate)
		{
			this.orderNumber = orderNumber;
			this.entryID = endtryId;
			this.terminalIP = terminalIp;
			this.sessionID = sessionId;
			this.requestID = requestId;
			this.status = status;
			this.priceForCalulate = priceForCalulate;
			this.approveCredit = approveCredit;
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
				this.status = msgArray[6];
				decimal.TryParse(msgArray[7], out this.priceForCalulate);
				decimal.TryParse(msgArray[8], out this.approveCredit);
				bool.TryParse(msgArray[9], out this.isForceUpdate);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ApproveOrderMessageFormat()
		{
		}
	}
}
