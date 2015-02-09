using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
namespace STIControl.ExpandTableGrid
{
	public class RecordItem
	{
		private bool changed = true;
		private Brush backBrush = Brushes.Black;
		private Color backColor = Color.Black;
		private int rows = 1;
		private int index = -1;
		private List<SubRecordItem> subRecord;
		private Hashtable fields;
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
		public Brush BackBrush
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.backBrush;
			}
		}
		public Color BackColor
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.backColor;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				if (this.backColor != value)
				{
					this.changed = true;
				}
				this.backColor = value;
				this.backBrush = new SolidBrush(value);
			}
		}
		public int Rows
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.rows;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.rows = value;
			}
		}
		public int Index
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.index;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.index = value;
			}
		}
		public List<SubRecordItem> SubRecord
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.subRecord;
			}
		}
		public int SubRecordCount
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (this.subRecord != null)
				{
					return this.subRecord.Count;
				}
				return 0;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public RecordItem(ref List<ColumnItem> ColTable, int index, Color recordBackColor)
		{
			this.BackColor = recordBackColor;
			this.index = index;
			this.fields = new Hashtable();
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
				result = (FieldItem)this.fields[columnName];
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected void Dispose(bool disposing)
		{
			this.fields.Clear();
			this.fields = null;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal SubRecordItem AddSubRecord(ref List<ColumnItem> ColTable)
		{
			SubRecordItem result;
			try
			{
				if (this.subRecord == null)
				{
					this.subRecord = new List<SubRecordItem>();
				}
				SubRecordItem subRecordItem = new SubRecordItem(ref ColTable);
				this.subRecord.Add(subRecordItem);
				result = subRecordItem;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal void ClearSubRecord()
		{
			if (this.subRecord != null)
			{
				this.subRecord.Clear();
				this.subRecord = null;
			}
		}
	}
}
