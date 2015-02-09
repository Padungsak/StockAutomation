using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ
{
	public class RNMessageFormat
	{
		private int firm;
		private string errorCode;
		private string originalMessage;
		public int Firm
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.firm;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.firm = value;
			}
		}
		public string ErrorCode
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.errorCode;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.errorCode = value;
			}
		}
		public string OriginalMessage
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.originalMessage;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.originalMessage = value;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public RNMessageFormat()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public RNMessageFormat(string message)
		{
			this.Unpack(message);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public string Pack(int firmId, string errorCode, string originalMessage)
		{
			return "RN" + AutoTManager.Mod96((long)firmId, 2) + errorCode.PadRight(2, ' ') + originalMessage.PadRight(478, ' ');
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Unpack(string message)
		{
			this.firm = (int)AutoTManager.Demod96int(message.Substring(2, 2));
			this.errorCode = message.Substring(4, 2);
			this.originalMessage = message.Substring(6);
		}
	}
}
