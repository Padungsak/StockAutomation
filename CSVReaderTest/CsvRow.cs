using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
namespace CSVReaderTest
{
	public class CsvRow : List<string>
	{
		private string _lineText;
		public string LineText
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this._lineText;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this._lineText = value;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public CsvRow()
		{
		}
	}
}
