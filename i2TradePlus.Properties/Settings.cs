using System;
using System.CodeDom.Compiler;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
namespace i2TradePlus.Properties
{
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "8.0.0.0"), CompilerGenerated]
	internal sealed class Settings : ApplicationSettingsBase
	{
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
		public static Settings Default
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return Settings.defaultInstance;
			}
		}
		[DefaultSettingValue("Segoe UI, 8.25pt"), UserScopedSetting, DebuggerNonUserCode]
		public Font Default_Font
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (Font)this["Default_Font"];
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this["Default_Font"] = value;
			}
		}
		[DefaultSettingValue("False"), UserScopedSetting, DebuggerNonUserCode]
		public bool IsWriteErrorLog
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (bool)this["IsWriteErrorLog"];
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this["IsWriteErrorLog"] = value;
			}
		}
		[DefaultSettingValue("True"), UserScopedSetting, DebuggerNonUserCode]
		public bool BSBoxEntryTTF
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (bool)this["BSBoxEntryTTF"];
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this["BSBoxEntryTTF"] = value;
			}
		}
		[DefaultSettingValue("True"), UserScopedSetting, DebuggerNonUserCode]
		public bool BSBoxDefaultStock
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (bool)this["BSBoxDefaultStock"];
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this["BSBoxDefaultStock"] = value;
			}
		}
		[DefaultSettingValue("0"), UserScopedSetting, DebuggerNonUserCode]
		public int BSBoxDefaultPrice
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (int)this["BSBoxDefaultPrice"];
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this["BSBoxDefaultPrice"] = value;
			}
		}
		[DefaultSettingValue("True"), UserScopedSetting, DebuggerNonUserCode]
		public bool BSBoxSavePincode
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (bool)this["BSBoxSavePincode"];
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this["BSBoxSavePincode"] = value;
			}
		}
		[DefaultSettingValue("False"), UserScopedSetting, DebuggerNonUserCode]
		public bool SmartOneClick
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (bool)this["SmartOneClick"];
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this["SmartOneClick"] = value;
			}
		}
		[DefaultSettingValue("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" />"), UserScopedSetting, DebuggerNonUserCode]
		public StringCollection Hotkeys
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (StringCollection)this["Hotkeys"];
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this["Hotkeys"] = value;
			}
		}
		[DefaultSettingValue(""), UserScopedSetting, DebuggerNonUserCode]
		public string ProxyPassword
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (string)this["ProxyPassword"];
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this["ProxyPassword"] = value;
			}
		}
		[DefaultSettingValue(""), UserScopedSetting, DebuggerNonUserCode]
		public string ProxyHost
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (string)this["ProxyHost"];
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this["ProxyHost"] = value;
			}
		}
		[DefaultSettingValue("80"), UserScopedSetting, DebuggerNonUserCode]
		public int ProxyPort
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (int)this["ProxyPort"];
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this["ProxyPort"] = value;
			}
		}
		[DefaultSettingValue("False"), UserScopedSetting, DebuggerNonUserCode]
		public bool RememberProxyPassword
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (bool)this["RememberProxyPassword"];
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this["RememberProxyPassword"] = value;
			}
		}
		[DefaultSettingValue("True"), UserScopedSetting, DebuggerNonUserCode]
		public bool Default_WindowMaximizeState
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (bool)this["Default_WindowMaximizeState"];
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this["Default_WindowMaximizeState"] = value;
			}
		}
		[DefaultSettingValue("0, 0, 800, 600"), UserScopedSetting, DebuggerNonUserCode]
		public Rectangle Default_Bound
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (Rectangle)this["Default_Bound"];
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this["Default_Bound"] = value;
			}
		}
		[DefaultSettingValue("True"), UserScopedSetting, DebuggerNonUserCode]
		public bool Default_FirstOpen
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (bool)this["Default_FirstOpen"];
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this["Default_FirstOpen"] = value;
			}
		}
		[DefaultSettingValue("200"), UserScopedSetting, DebuggerNonUserCode]
		public int ViewOrderRows
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (int)this["ViewOrderRows"];
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this["ViewOrderRows"] = value;
			}
		}
		[DefaultSettingValue("0.60"), UserScopedSetting, DebuggerNonUserCode]
		public decimal BottomSizePct
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (decimal)this["BottomSizePct"];
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this["BottomSizePct"] = value;
			}
		}
		[DefaultSettingValue("0"), UserScopedSetting, DebuggerNonUserCode]
		public int Default_MainScreenHeight
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (int)this["Default_MainScreenHeight"];
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this["Default_MainScreenHeight"] = value;
			}
		}
		[DefaultSettingValue("100, 100, 100"), UserScopedSetting, DebuggerNonUserCode]
		public Color HeaderBackGColor
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (Color)this["HeaderBackGColor"];
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this["HeaderBackGColor"] = value;
			}
		}
		[DefaultSettingValue("45, 45, 45"), UserScopedSetting, DebuggerNonUserCode]
		public Color GridColor
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (Color)this["GridColor"];
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this["GridColor"] = value;
			}
		}
		[DefaultSettingValue("LightGray"), UserScopedSetting, DebuggerNonUserCode]
		public Color HeaderFontColor
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (Color)this["HeaderFontColor"];
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this["HeaderFontColor"] = value;
			}
		}
		[ApplicationScopedSetting, DefaultSettingValue("http://localhost:1853/i2Trade.WebServer/Service.asmx"), SpecialSetting(SpecialSetting.WebServiceUrl), DebuggerNonUserCode]
		public string i2_TradePlus_ITSNetBusinessWS_Service
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (string)this["i2_TradePlus_ITSNetBusinessWS_Service"];
			}
		}
		[ApplicationScopedSetting, DefaultSettingValue("http://localhost:1853/i2Trade.WebServer/Service.asmx"), DebuggerNonUserCode]
		public string i2_Trade_ITSNetBusinessWS_Service
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (string)this["i2_Trade_ITSNetBusinessWS_Service"];
			}
		}
		[DefaultSettingValue("2"), UserScopedSetting, DebuggerNonUserCode]
		public int MainBottomStyle
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (int)this["MainBottomStyle"];
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this["MainBottomStyle"] = value;
			}
		}
		[DefaultSettingValue("False"), UserScopedSetting, DebuggerNonUserCode]
		public bool SmartClickLink
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (bool)this["SmartClickLink"];
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this["SmartClickLink"] = value;
			}
		}
		[DefaultSettingValue("100"), UserScopedSetting, DebuggerNonUserCode]
		public long SmartClickVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (long)this["SmartClickVolume"];
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this["SmartClickVolume"] = value;
			}
		}
		[DefaultSettingValue("0"), UserScopedSetting, DebuggerNonUserCode]
		public long BSBoxDefaultVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (long)this["BSBoxDefaultVolume"];
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this["BSBoxDefaultVolume"] = value;
			}
		}
		[DefaultSettingValue("0"), UserScopedSetting, DebuggerNonUserCode]
		public long BSBoxDefaultVolumeNext
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (long)this["BSBoxDefaultVolumeNext"];
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this["BSBoxDefaultVolumeNext"] = value;
			}
		}
		[DefaultSettingValue("False"), UserScopedSetting, DebuggerNonUserCode]
		public bool BSBoxDefaultVolumeActive
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (bool)this["BSBoxDefaultVolumeActive"];
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this["BSBoxDefaultVolumeActive"] = value;
			}
		}
		[DefaultSettingValue(""), UserScopedSetting, DebuggerNonUserCode]
		public string LastLoginId
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (string)this["LastLoginId"];
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this["LastLoginId"] = value;
			}
		}
		[DefaultSettingValue(""), UserScopedSetting, DebuggerNonUserCode]
		public string ProxyUsername
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (string)this["ProxyUsername"];
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this["ProxyUsername"] = value;
			}
		}
		[DefaultSettingValue("True"), UserScopedSetting, DebuggerNonUserCode]
		public bool RequestTfex
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (bool)this["RequestTfex"];
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this["RequestTfex"] = value;
			}
		}
		[ApplicationScopedSetting, DefaultSettingValue("http://localhost:1854/i2Trade.WebServerD/Service.asmx"), SpecialSetting(SpecialSetting.WebServiceUrl), DebuggerNonUserCode]
		public string i2_TradePlus_ITSNetBusinessWSTFEX_Service
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (string)this["i2_TradePlus_ITSNetBusinessWSTFEX_Service"];
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Settings()
		{
		}
	}
}
