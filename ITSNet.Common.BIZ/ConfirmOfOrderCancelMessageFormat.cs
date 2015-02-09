using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ
{
	public class ConfirmOfOrderCancelMessageFormat
	{
		private const char spliter = '|';
		private string spliterString = "|";
		private int firm;
		private string cancelStatus = string.Empty;
		private long orderNumber;
		private string entryDate;
		private long cancelShare;
		private long refOrderNo;
		private string sourceId = string.Empty;
		public int Firm
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.firm;
			}
		}
		public string CancelStatus
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.cancelStatus;
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
		public string EntryDate
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.entryDate;
			}
		}
		public long CancelShare
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.cancelShare;
			}
		}
		public long RefOrderNo
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.refOrderNo;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.refOrderNo = value;
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
		private string MessagePacket
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return string.Concat(new object[]
				{
					OrderMessageType.ConfirmationCancel.ToString(),
					this.spliterString,
					this.firm,
					this.spliterString,
					this.cancelShare,
					this.spliterString,
					this.orderNumber,
					this.spliterString,
					this.entryDate,
					this.spliterString,
					this.cancelStatus
				});
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(int firm, long cancelShare, long orderNumber, string entryDate, string cancelStatus)
		{
			this.firm = firm;
			this.cancelShare = cancelShare;
			this.orderNumber = orderNumber;
			this.entryDate = entryDate;
			this.cancelStatus = cancelStatus;
			return this.MessagePacket;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(int firm, long cancelShare, long orderNumber, long refOrderNo, string entryDate, string cancelStatus, string sourceId)
		{
			this.firm = firm;
			this.cancelShare = cancelShare;
			this.orderNumber = orderNumber;
			this.entryDate = entryDate;
			this.cancelStatus = cancelStatus;
			this.refOrderNo = refOrderNo;
			return string.Concat(new object[]
			{
				this.MessagePacket,
				this.spliterString,
				refOrderNo,
				this.spliterString,
				sourceId
			});
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Unpack(string message)
		{
			try
			{
				string[] msgArray = message.Split(new char[]
				{
					'|'
				});
				this.Unpack(msgArray);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Unpack(string[] msgArray)
		{
			try
			{
				this.firm = Convert.ToInt32(msgArray[1]);
				this.cancelShare = Convert.ToInt64(msgArray[2]);
				this.orderNumber = Convert.ToInt64(msgArray[3]);
				this.entryDate = msgArray[4];
				this.cancelStatus = msgArray[5];
				if (msgArray.Length > 6)
				{
					long.TryParse(msgArray[6], out this.refOrderNo);
				}
				if (msgArray.Length > 7)
				{
					this.sourceId = msgArray[7];
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ConfirmOfOrderCancelMessageFormat()
		{
		}
	}
}
