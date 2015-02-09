using System;
using System.Runtime.CompilerServices;
namespace i2TradePlus
{
	internal struct ErrorItem
	{
		internal DateTime Time;
		internal string ModuleName;
		internal string Function;
		internal string Information;
		internal string Description;
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal ErrorItem(DateTime time, string moduleName, string functionName, string information, string errorDescription)
		{
			this.Time = time;
			this.ModuleName = moduleName;
			this.Function = functionName;
			this.Information = information;
			this.Description = errorDescription;
		}
	}
}
