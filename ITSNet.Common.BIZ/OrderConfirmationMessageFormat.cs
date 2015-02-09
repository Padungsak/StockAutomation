using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ
{
	public class OrderConfirmationMessageFormat
	{
		private const char spliter = '|';
		private string spliterString = "|";
		private int firm;
		private long orderNumber;
		private string entryDate;
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
					OrderMessageType.OrderConfirmation.ToString(),
					this.spliterString,
					this.firm,
					this.spliterString,
					this.orderNumber,
					this.spliterString,
					this.entryDate
				});
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(int firm, long orderNumber, string entryDate)
		{
			this.firm = firm;
			this.orderNumber = orderNumber;
			this.entryDate = entryDate;
			return this.MessagePacket;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(int firm, long orderNumber, long refOrderNo, string sourceId, string entryDate)
		{
			this.firm = firm;
			this.orderNumber = orderNumber;
			this.entryDate = entryDate;
			this.refOrderNo = refOrderNo;
			this.sourceId = sourceId;
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
				int.TryParse(msgArray[1], out this.firm);
				long.TryParse(msgArray[2], out this.orderNumber);
				this.entryDate = msgArray[3];
				if (msgArray.Length > 4)
				{
					long.TryParse(msgArray[4], out this.refOrderNo);
				}
				if (msgArray.Length > 5)
				{
					this.sourceId = msgArray[5];
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public OrderConfirmationMessageFormat()
		{
		}
	}
}
