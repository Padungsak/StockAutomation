using System;
using System.Runtime.CompilerServices;
namespace STIControl
{
	public class DragItemData
	{
		private object controlView;
		private string dragText = string.Empty;
		public object ControlView
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.controlView;
			}
		}
		public string DragText
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.dragText;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.dragText = value;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public DragItemData(object controlView)
		{
			this.controlView = controlView;
		}
	}
}
