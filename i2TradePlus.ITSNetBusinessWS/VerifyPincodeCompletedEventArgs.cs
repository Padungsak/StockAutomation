using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
namespace i2TradePlus.ITSNetBusinessWS
{
	[GeneratedCode("System.Web.Services", "2.0.50727.5483"), DesignerCategory("code"), DebuggerStepThrough]
	public class VerifyPincodeCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;
		public bool Result
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[0];
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal VerifyPincodeCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
}
