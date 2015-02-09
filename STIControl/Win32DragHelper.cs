using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
namespace STIControl
{
	public class Win32DragHelper
	{
		[DllImport("comctl32.dll")]
		public static extern bool InitCommonControls();
		[DllImport("comctl32.dll", CharSet = CharSet.Auto)]
		public static extern bool ImageList_BeginDrag(IntPtr himlTrack, int iTrack, int dxHotspot, int dyHotspot);
		[DllImport("comctl32.dll", CharSet = CharSet.Auto)]
		public static extern bool ImageList_DragMove(int x, int y);
		[DllImport("comctl32.dll", CharSet = CharSet.Auto)]
		public static extern void ImageList_EndDrag();
		[DllImport("comctl32.dll", CharSet = CharSet.Auto)]
		public static extern bool ImageList_DragEnter(IntPtr hwndLock, int x, int y);
		[DllImport("comctl32.dll", CharSet = CharSet.Auto)]
		public static extern bool ImageList_DragLeave(IntPtr hwndLock);
		[DllImport("comctl32.dll", CharSet = CharSet.Auto)]
		public static extern bool ImageList_DragShowNolock(bool fShow);
		[MethodImpl(MethodImplOptions.NoInlining)]
		static Win32DragHelper()
		{
			Win32DragHelper.InitCommonControls();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Win32DragHelper()
		{
		}
	}
}
