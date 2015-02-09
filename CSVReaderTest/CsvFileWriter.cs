using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
namespace CSVReaderTest
{
	public class CsvFileWriter : StreamWriter
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		public CsvFileWriter(Stream stream) : base(stream)
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public CsvFileWriter(string filename) : base(filename)
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void WriteRow(CsvRow row)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (string current in row)
			{
				if (!flag)
				{
					stringBuilder.Append(',');
				}
				if (current.IndexOfAny(new char[]
				{
					'"',
					','
				}) != -1)
				{
					stringBuilder.AppendFormat("\"{0}\"", current.Replace("\"", "\"\""));
				}
				else
				{
					stringBuilder.Append(current);
				}
				flag = false;
			}
			row.LineText = stringBuilder.ToString();
			this.WriteLine(row.LineText);
		}
	}
}
