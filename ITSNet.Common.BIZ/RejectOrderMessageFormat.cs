using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ
{
	public class RejectOrderMessageFormat
	{
		private const char spliter = '|';
		private string spliterString = "|";
		private int firm;
		private int rejectCode;
		private string originalMessageText;
		private string originalMessageType;
		private long originalOrderNumber;
		private long refOrderNo;
		public int Firm
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.firm;
			}
		}
		public int RejectCode
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.rejectCode;
			}
		}
		public string OriginalMessageText
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.originalMessageText;
			}
		}
		public string OriginalMessageType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.originalMessageType;
			}
		}
		public long OriginalOrderNumber
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.originalOrderNumber;
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
				return string.Concat(new object[]
				{
					OrderMessageType.Reject.ToString(),
					this.spliterString,
					this.firm,
					this.spliterString,
					this.rejectCode,
					this.spliterString,
					this.originalMessageText,
					this.spliterString,
					this.originalMessageType,
					this.spliterString,
					this.originalOrderNumber
				});
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(int firm, int rejectCode, string originalMessageText, string orginalMessageType, long originalOrderNumber)
		{
			this.firm = firm;
			this.rejectCode = rejectCode;
			this.originalMessageText = originalMessageText;
			this.originalMessageType = orginalMessageType;
			this.originalOrderNumber = originalOrderNumber;
			return this.MessagePacket;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(int firm, int rejectCode, string originalMessageText, string orginalMessageType, long originalOrderNumber, long refOrderNo)
		{
			this.firm = firm;
			this.rejectCode = rejectCode;
			this.originalMessageText = originalMessageText;
			this.originalMessageType = orginalMessageType;
			this.originalOrderNumber = originalOrderNumber;
			this.refOrderNo = refOrderNo;
			return this.MessagePacket + this.spliterString + refOrderNo;
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
				this.rejectCode = Convert.ToInt32(msgArray[2]);
				this.originalMessageText = msgArray[3].ToString();
				this.originalMessageType = msgArray[4].ToString();
				this.originalOrderNumber = Convert.ToInt64(msgArray[5].ToString());
				if (msgArray.Length > 6)
				{
					long.TryParse(msgArray[6], out this.refOrderNo);
				}
				msgArray = null;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public RejectOrderMessageFormat()
		{
		}
	}
}
