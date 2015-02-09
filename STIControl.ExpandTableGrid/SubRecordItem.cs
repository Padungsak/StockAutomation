using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
namespace STIControl.ExpandTableGrid
{
	public class SubRecordItem
	{
		private bool changed = true;
		private Dictionary<string, FieldItem> fields;
		public bool Changed
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.changed;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.changed = value;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public SubRecordItem(ref List<ColumnItem> ColTable)
		{
			this.fields = new Dictionary<string, FieldItem>();
			foreach (ColumnItem current in ColTable)
			{
				this.fields.Add(current.Name, new FieldItem(current.ValueFormat));
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public FieldItem Fields(string columnName)
		{
			FieldItem result;
			try
			{
				result = this.fields[columnName];
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ClearAllText()
		{
			try
			{
				foreach (KeyValuePair<string, FieldItem> current in this.fields)
				{
					current.Value.Text = string.Empty;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected void Dispose(bool disposing)
		{
			this.fields.Clear();
			this.fields = null;
		}
	}
}
