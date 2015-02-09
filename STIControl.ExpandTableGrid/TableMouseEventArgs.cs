using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace STIControl.ExpandTableGrid
{
	public class TableMouseEventArgs : EventArgs
	{
		public ColumnItem Column;
		public MouseEventArgs Mouse;
		public int RowIndex;
		[MethodImpl(MethodImplOptions.NoInlining)]
		public TableMouseEventArgs(MouseEventArgs mouse, int rowIndex, ColumnItem column)
		{
			this.Mouse = mouse;
			this.RowIndex = rowIndex;
			this.Column = column;
		}
	}
}
