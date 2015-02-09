using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
namespace STIControl
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public class IntzaGridHitTestInfo
	{
		public int pt_x;
		public int pt_y;
		public int flags;
		public int iItem;
		public int iSubItem;
		[MethodImpl(MethodImplOptions.NoInlining)]
		public IntzaGridHitTestInfo()
		{
		}
	}
}
