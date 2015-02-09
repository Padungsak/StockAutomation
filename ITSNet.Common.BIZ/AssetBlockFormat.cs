using System;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ
{
	public class AssetBlockFormat
	{
		private string messageData = string.Empty;
		private int sequntNumber;
		public string MessageData
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.messageData;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.messageData = value;
			}
		}
		public int SequentNumber
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.sequntNumber;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.sequntNumber = value;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Clear()
		{
			this.sequntNumber = 0;
			this.messageData = string.Empty;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public int GetSequenceNumber(string message)
		{
			int result;
			try
			{
				result = Convert.ToInt32(AutoTManager.Demod96int(message.Substring(0, 3)));
			}
			catch
			{
				result = -1;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public AssetBlockFormat()
		{
		}
	}
}
