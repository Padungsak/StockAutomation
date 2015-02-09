using i2TradePlus.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
namespace i2TradePlus
{
	internal class ExceptionManager
	{
		private static ErrorItem Error;
		public static List<ErrorItem> Items = new List<ErrorItem>();
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void Show(ErrorItem error)
		{
			try
			{
				ExceptionManager.AddItem(error.Time, error.ModuleName, error.Function, error.Information, error.Description);
			}
			catch (Exception ex)
			{
				ExceptionManager.WriteEventLog("ExceptionManager-Show", ex.ToString());
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void Close()
		{
			if (ExceptionManager.Items != null)
			{
				ExceptionManager.Items.Clear();
				ExceptionManager.Items = null;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void AddItem(DateTime time, string moduleName, string functionName, string information, string description)
		{
			try
			{
				ExceptionManager.Error = new ErrorItem(DateTime.Now, moduleName, functionName, information, description);
				ExceptionManager.Items.Add(ExceptionManager.Error);
				if (Settings.Default.IsWriteErrorLog)
				{
					ExceptionManager.WriteEventLog(moduleName + "-" + functionName, information + "-" + description);
				}
			}
			catch
			{
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void WriteEventLog(string source, string description)
		{
			EventLog.WriteEntry("i2Trade", source + " : " + description, EventLogEntryType.Error);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void Clear()
		{
			if (ExceptionManager.Items != null)
			{
				ExceptionManager.Items.Clear();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ExceptionManager()
		{
		}
	}
}
