using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace CSVReaderTest
{
	internal class CSVReadWrite
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void exportToCSV(DataTable dt)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.InitialDirectory = Application.ExecutablePath.ToString();
			saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
			saveFileDialog.FilterIndex = 1;
			saveFileDialog.RestoreDirectory = true;
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				CSVReadWrite.CreateCSVfile(dt, saveFileDialog.FileName.ToString());
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void exportToCSV(DataSet ds)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.InitialDirectory = Application.ExecutablePath.ToString();
			saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
			saveFileDialog.FilterIndex = 1;
			saveFileDialog.RestoreDirectory = true;
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				CSVReadWrite.CreateCSVfile(ds, saveFileDialog.FileName.ToString());
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void CreateCSVfile(DataTable dtable, string strFilePath)
		{
			if (dtable.Rows.Count > 0)
			{
				StreamWriter streamWriter = new StreamWriter(strFilePath, false);
				int count = dtable.Columns.Count;
				foreach (DataRow dataRow in dtable.Rows)
				{
					for (int i = 0; i < count; i++)
					{
						if (!Convert.IsDBNull(dataRow[i]))
						{
							streamWriter.Write(dataRow[i].ToString());
						}
						if (i < count - 1)
						{
							streamWriter.Write(",");
						}
					}
					streamWriter.Write(streamWriter.NewLine);
				}
				streamWriter.Close();
				Process.Start("rundll32.exe", "shell32.dll, OpenAs_RunDLL " + strFilePath);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void CreateCSVfile(DataSet ds, string strFilePath)
		{
			if (ds.Tables.Count > 0)
			{
				StreamWriter streamWriter = new StreamWriter(strFilePath, false);
				foreach (DataTable dataTable in ds.Tables)
				{
					if (dataTable.Rows.Count > 0)
					{
						int count = dataTable.Columns.Count;
						foreach (DataRow dataRow in dataTable.Rows)
						{
							for (int i = 0; i < count; i++)
							{
								if (!Convert.IsDBNull(dataRow[i]))
								{
									streamWriter.Write(dataRow[i].ToString());
								}
								if (i < count - 1)
								{
									streamWriter.Write(",");
								}
							}
							streamWriter.Write(streamWriter.NewLine);
						}
					}
					streamWriter.Write(streamWriter.NewLine);
					streamWriter.Write(streamWriter.NewLine);
				}
				streamWriter.Close();
				Process.Start("rundll32.exe", "shell32.dll, OpenAs_RunDLL " + strFilePath);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public CSVReadWrite()
		{
		}
	}
}
