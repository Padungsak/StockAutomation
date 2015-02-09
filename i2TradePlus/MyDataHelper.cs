using System;
using System.Data;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
namespace i2TradePlus
{
	public class MyDataHelper
	{
		private const string TableEndSection = "</t>";
		private const string TableNameEndSection = "</n>";
		private const string ColumnInfoEndSection = "</i>";
		private const string RowEndSection = "</r>";
		private const char ColumnInfoNameTypeSplitter = ';';
		private static readonly char emptyDataValue;
		private static readonly char ColumnValueSplitter;
		[MethodImpl(MethodImplOptions.NoInlining)]
		static MyDataHelper()
		{
			MyDataHelper.emptyDataValue = '`';
			MyDataHelper.ColumnValueSplitter = '|';
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void StringToDataSet(string data, DataSet ds)
		{
			string text = string.Empty;
			string text2 = data;
			string text3 = string.Empty;
			string text4 = string.Empty;
			string text5 = string.Empty;
			string text6 = string.Empty;
			try
			{
				while (text2 != string.Empty)
				{
					int num = text2.IndexOf("</t>");
					if (num > 0)
					{
						text3 = text2.Substring(0, text2.IndexOf("</t>"));
						text2 = text2.Substring(num + "</t>".Length);
					}
					else
					{
						text3 = text2;
						text2 = string.Empty;
					}
					text = text3.Substring(0, text3.IndexOf("</n>"));
					if (ds.Tables.Contains(text))
					{
						ds.Tables.Remove(text);
					}
					ds.Tables.Add(text);
					text5 = text3.Substring((text + "</n>").Length);
					int num2 = text5.IndexOf("</i>");
					text4 = text5.Substring(0, num2);
					string[] array = text4.Split(new char[]
					{
						MyDataHelper.ColumnValueSplitter
					});
					if (array.Length > 0)
					{
						int num3 = array.Length;
						for (int i = 0; i < num3; i++)
						{
							ds.Tables[text].Columns.Add(new DataColumn(array[i]));
							array[i] = string.Empty;
						}
					}
					text5 = text5.Substring(num2 + "</n>".Length);
					while (text5 != string.Empty)
					{
						int num4 = text5.IndexOf("</r>");
						text6 = text5.Substring(0, num4);
						string[] array2 = text6.Split(new char[]
						{
							MyDataHelper.ColumnValueSplitter
						});
						DataRow dataRow = ds.Tables[text].NewRow();
						int num3 = array2.Length;
						for (int i = 0; i < num3; i++)
						{
							if (array2[i] == MyDataHelper.emptyDataValue.ToString())
							{
								dataRow[i] = string.Empty;
							}
							else
							{
								dataRow[i] = array2[i];
							}
						}
						ds.Tables[text].Rows.Add(dataRow);
						text5 = text5.Substring(num4 + "</r>".Length);
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			data = string.Empty;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public MyDataHelper()
		{
		}
	}
}
