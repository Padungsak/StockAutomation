using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
namespace STIControl
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
	internal class Resource
	{
		private static ResourceManager resourceMan;
		private static CultureInfo resourceCulture;
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (object.ReferenceEquals(Resource.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("STIControl.Resource", typeof(Resource).Assembly);
					Resource.resourceMan = resourceManager;
				}
				return Resource.resourceMan;
			}
		}
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return Resource.resourceCulture;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				Resource.resourceCulture = value;
			}
		}
		internal static Bitmap action_check
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				object @object = Resource.ResourceManager.GetObject("action_check", Resource.resourceCulture);
				return (Bitmap)@object;
			}
		}
		internal static Bitmap action_delete
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				object @object = Resource.ResourceManager.GetObject("action_delete", Resource.resourceCulture);
				return (Bitmap)@object;
			}
		}
		internal static Bitmap collapse
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				object @object = Resource.ResourceManager.GetObject("collapse", Resource.resourceCulture);
				return (Bitmap)@object;
			}
		}
		internal static Bitmap delete
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				object @object = Resource.ResourceManager.GetObject("delete", Resource.resourceCulture);
				return (Bitmap)@object;
			}
		}
		internal static Bitmap delete_90CW
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				object @object = Resource.ResourceManager.GetObject("delete_90CW", Resource.resourceCulture);
				return (Bitmap)@object;
			}
		}
		internal static Bitmap expand
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				object @object = Resource.ResourceManager.GetObject("expand", Resource.resourceCulture);
				return (Bitmap)@object;
			}
		}
		internal static Bitmap info
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				object @object = Resource.ResourceManager.GetObject("info", Resource.resourceCulture);
				return (Bitmap)@object;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal Resource()
		{
		}
	}
}
