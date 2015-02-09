using System;
using System.Runtime.InteropServices;
namespace ITSNet.Controls.XtWebBrowser
{
	[Guid("34A715A0-6587-11D0-924A-0020AFC7AC4D"), InterfaceType(ComInterfaceType.InterfaceIsIDispatch), TypeLibType(TypeLibTypeFlags.FHidden)]
	[ComImport]
	public interface DWebBrowserEvents2
	{
		[DispId(250)]
		void BeforeNavigate2([MarshalAs(UnmanagedType.IDispatch)] [In] object pDisp, [MarshalAs(UnmanagedType.BStr)] [In] ref string URL, [In] ref object flags, [MarshalAs(UnmanagedType.BStr)] [In] ref string targetFrameName, [In] ref object postdata, [MarshalAs(UnmanagedType.BStr)] [In] ref string headers, [In] [Out] ref bool cancel);
		[DispId(273)]
		void NewWindow3([MarshalAs(UnmanagedType.IDispatch)] [In] object pDisp, [In] [Out] ref bool cancel, [In] ref object Flags, [MarshalAs(UnmanagedType.BStr)] [In] ref string UrlContext, [MarshalAs(UnmanagedType.BStr)] [In] ref string Url);
	}
}
