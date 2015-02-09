using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
namespace i2TradePlus.ITSNetBusinessWS
{
	[GeneratedCode("System.Web.Services", "2.0.50727.5483"), DesignerCategory("code"), DebuggerStepThrough]
	public class ViewCustomer_ConfirmCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;
		public string Result
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal ViewCustomer_ConfirmCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
}
