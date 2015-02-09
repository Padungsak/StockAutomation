using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace STIControl.CustomGrid
{
	public class ItemGridMouseEventArgs : EventArgs
	{
		private Rectangle position;
		private MouseEventArgs mouse;
		private ItemGrid item;
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
		public ItemGrid Item
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.item;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ItemGridMouseEventArgs(MouseEventArgs mouse, ItemGrid item)
		{
			this.position = item.Bounds;
			this.mouse = mouse;
			this.item = item;
		}
	}
}
