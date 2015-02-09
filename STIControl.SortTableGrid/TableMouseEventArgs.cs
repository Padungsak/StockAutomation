using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace STIControl.SortTableGrid
{
	public class TableMouseEventArgs : EventArgs
	{
		public ColumnItem Column;
		public Rectangle FieldPosition;
		public MouseEventArgs Mouse;
		public int RowIndex;
		[MethodImpl(MethodImplOptions.NoInlining)]
		public TableMouseEventArgs(Rectangle fieldPosition, MouseEventArgs mouse, int rowIndex, ColumnItem col)
		{
			this.FieldPosition = fieldPosition;
			this.Mouse = mouse;
			this.RowIndex = rowIndex;
			this.Column = col;
		}
	}
}
