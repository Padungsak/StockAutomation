using System;
using System.IO;
using System.Runtime.CompilerServices;
namespace CSVReaderTest
{
	public class CsvFileReader : StreamReader
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		public CsvFileReader(Stream stream) : base(stream)
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public CsvFileReader(string filename) : base(filename)
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool ReadRow(CsvRow row)
		{
			row.LineText = this.ReadLine();
			bool result;
			if (string.IsNullOrEmpty(row.LineText))
			{
				result = false;
			}
			else
			{
				int i = 0;
				int num = 0;
				while (i < row.LineText.Length)
				{
					string text;
					if (row.LineText[i] == '"')
					{
						i++;
						int num2 = i;
						while (i < row.LineText.Length)
						{
							if (row.LineText[i] == '"')
							{
								i++;
								if (i >= row.LineText.Length || row.LineText[i] != '"')
								{
									i--;
									break;
								}
							}
							i++;
						}
						text = row.LineText.Substring(num2, i - num2);
						text = text.Replace("\"\"", "\"");
					}
					else
					{
						int num2 = i;
						while (i < row.LineText.Length && row.LineText[i] != ',')
						{
							i++;
						}
						text = row.LineText.Substring(num2, i - num2);
					}
					if (num < row.Count)
					{
						row[num] = text;
					}
					else
					{
						row.Add(text);
					}
					num++;
					while (i < row.LineText.Length && row.LineText[i] != ',')
					{
						i++;
					}
					if (i < row.LineText.Length)
					{
						i++;
					}
				}
				while (row.Count > num)
				{
					row.RemoveAt(num);
				}
				result = (row.Count > 0);
			}
			return result;
		}
	}
}
