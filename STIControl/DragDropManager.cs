using System;
using System.Runtime.CompilerServices;
namespace STIControl
{
	public class DragDropManager
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		public DragDropManager()
		{
			this.InitCommonControls();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool InitCommonControls()
		{
			return Win32DragHelper.InitCommonControls();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool BeginDrag(IntPtr himlTrack, int iTrack, int dxHotspot, int dyHotspot)
		{
			return Win32DragHelper.ImageList_BeginDrag(himlTrack, iTrack, dxHotspot, dyHotspot);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool DragMove(int x, int y)
		{
			return Win32DragHelper.ImageList_DragMove(x, y);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void EndDrag()
		{
			Win32DragHelper.ImageList_EndDrag();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool DragEnter(IntPtr hwndLock, int x, int y)
		{
			return Win32DragHelper.ImageList_DragEnter(hwndLock, x, y);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool DragLeave(IntPtr hwndLock)
		{
			return Win32DragHelper.ImageList_DragLeave(hwndLock);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool DragShowNolock(bool fShow)
		{
			return Win32DragHelper.ImageList_DragShowNolock(fShow);
		}
	}
}
