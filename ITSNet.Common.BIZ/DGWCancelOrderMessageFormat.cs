using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ
{
	public class DGWCancelOrderMessageFormat
	{
		private const char spliter = '|';
		private string spliterString = "|";
		private long orderNumber;
		private string cancelEntryId;
		private long refOrderNo;
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
		private string MessagePacket
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return string.Concat(new string[]
				{
					OrderMessageType.CancelOrder.ToString(),
					this.spliterString,
					this.orderNumber.ToString(),
					this.spliterString,
					this.cancelEntryId
				});
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(long orderNumber, string cancelEntryId)
		{
			this.orderNumber = orderNumber;
			this.cancelEntryId = cancelEntryId;
			return this.MessagePacket;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(long orderNumber, long refOrderNo, string cancelEntryId)
		{
			this.orderNumber = orderNumber;
			this.cancelEntryId = cancelEntryId;
			this.refOrderNo = refOrderNo;
			return this.MessagePacket + this.spliterString + refOrderNo;
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
				long.TryParse(msgArray[1], out this.orderNumber);
				this.cancelEntryId = msgArray[2];
				if (msgArray.Length > 3)
				{
					long.TryParse(msgArray[3], out this.refOrderNo);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public DGWCancelOrderMessageFormat()
		{
		}
	}
}
