using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
namespace STIControl.SortTableGrid
{
	public class RecordItem
	{
		private bool changed = true;
		private Brush backBrush = Brushes.Black;
		private Color backColor = Color.Black;
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
		[MethodImpl(MethodImplOptions.NoInlining)]
		public RecordItem(ref List<ColumnItem> ColTable, Color recordBackColor)
		{
			this.BackColor = recordBackColor;
			this.fields = new Hashtable();
			foreach (ColumnItem current in ColTable)
			{
				this.fields.Add(current.Name, new FieldItem(current.ValueFormat));
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public FieldItem Fields(string columnName)
		{
			return (FieldItem)this.fields[columnName];
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected void Dispose(bool disposing)
		{
			this.fields.Clear();
			this.fields = null;
		}
	}
}
