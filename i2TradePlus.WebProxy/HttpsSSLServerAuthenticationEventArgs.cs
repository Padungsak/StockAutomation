using System;
using System.Runtime.CompilerServices;
namespace i2TradePlus.WebProxy
{
	public class HttpsSSLServerAuthenticationEventArgs : EventArgs
	{
		private bool accept = false;
		public bool Accept
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.accept;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.accept = value;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public HttpsSSLServerAuthenticationEventArgs()
		{
		}
	}
}
