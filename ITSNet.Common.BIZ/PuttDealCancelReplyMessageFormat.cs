using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ
{
	public class PuttDealCancelReplyMessageFormat
	{
		private const char spliter = '|';
		private string spliterString = "|";
		private int firm;
		private long confirmNumber;
		private string replyCode;
		public int Firm
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.firm;
			}
		}
		public long ConfirmNumber
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.confirmNumber;
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
		public string MessagePacket
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return string.Concat(new object[]
				{
					OrderMessageType.PuttDealCancelReply.ToString(),
					this.spliterString,
					this.firm,
					this.spliterString,
					this.confirmNumber,
					this.spliterString,
					this.replyCode
				});
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(int firm, long confirmNumber, string replyCode)
		{
			this.firm = firm;
			this.confirmNumber = confirmNumber;
			this.replyCode = replyCode;
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
				this.firm = Convert.ToInt32(msgArray[1]);
				this.confirmNumber = Convert.ToInt64(msgArray[2]);
				this.replyCode = msgArray[3];
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public PuttDealCancelReplyMessageFormat()
		{
		}
	}
}
