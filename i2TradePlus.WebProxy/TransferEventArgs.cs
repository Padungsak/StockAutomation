using System;
using System.Runtime.CompilerServices;
namespace i2TradePlus.WebProxy
{
	internal class TransferEventArgs : EventArgs
	{
		private int code = 0;
		private string text = string.Empty;
		private Exception exception = null;
		internal int Code
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.code;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.code = value;
			}
		}
		internal string Text
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.text;
			}
		}
		internal Exception Exception
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.exception;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal TransferEventArgs()
		{
			throw new Exception("ต้องใช้ แบบ มี Parameter เท่านั้น");
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal TransferEventArgs(string text)
		{
			this.code = 0;
			this.text = text;
			this.exception = null;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal TransferEventArgs(int code, string text)
		{
			this.code = code;
			this.text = text;
			if (code == -1)
			{
				this.exception = new Exception(text);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal TransferEventArgs(Exception exception)
		{
			this.code = -1;
			this.exception = exception;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal TransferEventArgs(string text, Exception exception)
		{
			this.code = -1;
			this.text = text;
			this.exception = exception;
		}
	}
}
