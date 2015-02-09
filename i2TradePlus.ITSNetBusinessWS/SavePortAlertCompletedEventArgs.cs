using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
namespace i2TradePlus.ITSNetBusinessWS
{
	[GeneratedCode("System.Web.Services", "2.0.50727.5483"), DesignerCategory("code"), DebuggerStepThrough]
	public class SavePortAlertCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;
		public long Result
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				base.RaiseExceptionIfNecessary();
				return (long)this.results[0];
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal SavePortAlertCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
}
