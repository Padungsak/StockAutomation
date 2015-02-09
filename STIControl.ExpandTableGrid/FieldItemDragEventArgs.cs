using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace STIControl.ExpandTableGrid
{
	public class FieldItemDragEventArgs : EventArgs
	{
		private string columnName;
		private Rectangle position;
		private MouseEventArgs mouse;
		private int rowIndex;
		private FieldItem item;
		public string ColumnName
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.columnName;
			}
		}
		public Rectangle Position
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.position;
			}
		}
		public MouseEventArgs Mouse
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.mouse;
			}
		}
		public int RowIndex
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.rowIndex;
			}
		}
		public FieldItem Item
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.item;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public FieldItemDragEventArgs(Rectangle fieldPosition, MouseEventArgs mouse, int rowIndex, string columnName)
		{
			this.position = fieldPosition;
			this.mouse = mouse;
			this.rowIndex = rowIndex;
			this.columnName = columnName;
			this.item = null;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public FieldItemDragEventArgs(Rectangle fieldPosition, MouseEventArgs mouse, int rowIndex, string columnName, FieldItem item)
		{
			this.position = fieldPosition;
			this.mouse = mouse;
			this.rowIndex = rowIndex;
			this.columnName = columnName;
			this.item = item;
		}
	}
}
