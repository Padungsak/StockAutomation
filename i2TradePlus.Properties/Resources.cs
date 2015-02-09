using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
namespace i2TradePlus.Properties
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
	internal class Resources
	{
		private static ResourceManager resourceMan;
		private static CultureInfo resourceCulture;
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (object.ReferenceEquals(Resources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("i2TradePlus.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return Resources.resourceCulture;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				Resources.resourceCulture = value;
			}
		}
		internal static Bitmap Down
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				object @object = Resources.ResourceManager.GetObject("Down", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}
		internal static Bitmap fileclose
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				object @object = Resources.ResourceManager.GetObject("fileclose", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}
		internal static Bitmap Minus
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				object @object = Resources.ResourceManager.GetObject("Minus", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}
		internal static Bitmap MoveFirstHS
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				object @object = Resources.ResourceManager.GetObject("MoveFirstHS", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}
		internal static Bitmap MoveLastHS
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				object @object = Resources.ResourceManager.GetObject("MoveLastHS", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}
		internal static Bitmap MoveNextHS
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				object @object = Resources.ResourceManager.GetObject("MoveNextHS", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}
		internal static Bitmap MovePreviousHS
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				object @object = Resources.ResourceManager.GetObject("MovePreviousHS", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}
		internal static Bitmap Plus
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				object @object = Resources.ResourceManager.GetObject("Plus", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}
		internal static Bitmap refresh
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				object @object = Resources.ResourceManager.GetObject("refresh", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}
		internal static Bitmap SortAsc
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				object @object = Resources.ResourceManager.GetObject("SortAsc", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}
		internal static Bitmap SortDes
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				object @object = Resources.ResourceManager.GetObject("SortDes", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}
		internal static Bitmap Up1
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				object @object = Resources.ResourceManager.GetObject("Up1", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}
		internal static Bitmap zoom_in
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				object @object = Resources.ResourceManager.GetObject("zoom_in", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}
		internal static Bitmap zoom_out
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				object @object = Resources.ResourceManager.GetObject("zoom_out", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal Resources()
		{
		}
	}
}
