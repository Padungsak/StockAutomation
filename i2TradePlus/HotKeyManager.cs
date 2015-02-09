using i2TradePlus.MyDataSet;
using i2TradePlus.Templates;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace i2TradePlus
{
	internal class HotKeyManager
	{
		public class HotkeyProperty
		{
			private string templateName = string.Empty;
			private string templateGroup = string.Empty;
			public string TemplateName
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.templateName;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.templateName = value;
				}
			}
			public string TemplateGroup
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.templateGroup;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.templateGroup = value;
				}
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			public HotkeyProperty(string templateName, string templateGroup)
			{
				this.templateName = templateName;
				this.templateGroup = templateGroup;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			public string Pack()
			{
				return this.templateName + "</>" + this.templateGroup;
			}
		}
		private static readonly Keys[] functionKeys;
		private static readonly Keys[] modifierKeys;
		private static Dictionary<Keys, HotKeyFor> systemHotKeys;
		private static Dictionary<Keys, HotKeyManager.HotkeyProperty> templatesHotKey;
		public static Keys[] FunctionKeys
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return HotKeyManager.functionKeys;
			}
		}
		public static Keys[] ModifierKeys
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return HotKeyManager.modifierKeys;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		static HotKeyManager()
		{
			HotKeyManager.functionKeys = new Keys[]
			{
				Keys.None,
				Keys.A,
				Keys.B,
				Keys.C,
				Keys.E,
				Keys.F,
				Keys.G,
				Keys.H,
				Keys.I,
				Keys.J,
				Keys.K,
				Keys.L,
				Keys.M,
				Keys.N,
				Keys.O,
				Keys.P,
				Keys.Q,
				Keys.R,
				Keys.S,
				Keys.T,
				Keys.U,
				Keys.V,
				Keys.W,
				Keys.X,
				Keys.Y,
				Keys.Z,
				Keys.F1,
				Keys.F2,
				Keys.F3,
				Keys.F4,
				Keys.F5,
				Keys.F6,
				Keys.F7,
				Keys.F8,
				Keys.F9,
				Keys.F10,
				Keys.F11,
				Keys.F12,
				Keys.NumLock,
				Keys.Subtract,
				Keys.Multiply,
				Keys.Divide
			};
			HotKeyManager.modifierKeys = new Keys[]
			{
				Keys.None,
				Keys.Control,
				Keys.Alt,
				Keys.Shift
			};
			HotKeyManager.systemHotKeys = null;
			HotKeyManager.templatesHotKey = null;
			try
			{
				HotKeyManager.systemHotKeys = new Dictionary<Keys, HotKeyFor>();
				HotKeyManager.templatesHotKey = new Dictionary<Keys, HotKeyManager.HotkeyProperty>();
				HotKeyManager.LoadSystemHotkey();
			}
			catch
			{
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void ClearTemplateHotkey()
		{
			if (HotKeyManager.templatesHotKey == null)
			{
				HotKeyManager.templatesHotKey = new Dictionary<Keys, HotKeyManager.HotkeyProperty>();
			}
			else
			{
				HotKeyManager.templatesHotKey.Clear();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Dictionary<Keys, HotKeyFor> ConvertToHashtable(StringCollection config)
		{
			Dictionary<Keys, HotKeyFor> dictionary = new Dictionary<Keys, HotKeyFor>();
			foreach (string current in config)
			{
				string[] array = current.Split(new char[]
				{
					'|'
				});
				dictionary.Add((Keys)int.Parse(array[0]), (HotKeyFor)int.Parse(array[1]));
			}
			return dictionary;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static bool IsValidHotKey(Keys hotKey)
		{
			Keys keys = hotKey & Keys.KeyCode;
			Keys keys2 = hotKey & Keys.Modifiers;
			bool result;
			if (hotKey == Keys.None)
			{
				result = false;
			}
			else
			{
				Keys keys3 = keys;
				if (keys3 <= Keys.Insert)
				{
					if (keys3 != Keys.Pause && keys3 != Keys.Insert)
					{
						goto IL_88;
					}
				}
				else
				{
					if (keys3 != Keys.Multiply)
					{
						switch (keys3)
						{
						case Keys.Subtract:
						case Keys.Divide:
							break;
						case Keys.Decimal:
							goto IL_88;
						default:
							if (keys3 != Keys.NumLock)
							{
								goto IL_88;
							}
							break;
						}
					}
				}
				result = true;
				return result;
				IL_88:
				if (keys < Keys.F1 || keys > Keys.F24)
				{
					if (keys == Keys.None || keys2 == Keys.None)
					{
						result = false;
						return result;
					}
					switch (keys)
					{
					case Keys.ShiftKey:
					case Keys.ControlKey:
					case Keys.Menu:
						result = false;
						return result;
					}
				}
				result = true;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static bool IsHotKeyDefined(Keys hotKey)
		{
			return HotKeyManager.templatesHotKey.ContainsKey(hotKey) || HotKeyManager.systemHotKeys.ContainsKey(hotKey);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static bool IsReservKey(Keys hotKey)
		{
			if (hotKey <= (Keys)131154)
			{
				if (hotKey != Keys.Pause && hotKey != Keys.Insert && hotKey != (Keys)131154)
				{
					goto IL_64;
				}
			}
			else
			{
				if (hotKey != (Keys)131162 && hotKey != (Keys)262227 && hotKey != (Keys)262259)
				{
					goto IL_64;
				}
			}
			bool result = true;
			return result;
			IL_64:
			result = false;
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static bool CheckHotkey(Keys functionKey)
		{
			if (!HotKeyManager.IsValidHotKey(functionKey))
			{
				throw new Exception("This key is invalid key for system, cannot defind for your HotKey!");
			}
			if (HotKeyManager.IsReservKey(functionKey))
			{
				throw new Exception("This key is Reserv key for system, cannot defind for your HotKey!");
			}
			if (HotKeyManager.IsHotKeyDefined(functionKey))
			{
				throw new Exception("This key is definded, cannot defind for your HotKey!");
			}
			return true;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void AddSystemHotkey(int keyID, Keys functionKey, bool isRestoreDefault)
		{
			try
			{
				if (!isRestoreDefault)
				{
					HotKeyManager.CheckHotkey(functionKey);
				}
				HotKeyManager.systemHotKeys.Add(functionKey, (HotKeyFor)keyID);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static bool AddTemplateHotkey(string templateName, string templateGroup, Keys functionKey)
		{
			bool result;
			try
			{
				if (functionKey != Keys.None)
				{
					if (!HotKeyManager.templatesHotKey.ContainsKey(functionKey))
					{
						HotKeyManager.templatesHotKey.Add(functionKey, new HotKeyManager.HotkeyProperty(templateName, templateGroup));
					}
					else
					{
						HotKeyManager.templatesHotKey[functionKey] = new HotKeyManager.HotkeyProperty(templateName, templateGroup);
					}
					result = true;
					return result;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			result = false;
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void RemoveHotkey_ByTemplateName(string templateName)
		{
			Keys keys = Keys.None;
			try
			{
				foreach (KeyValuePair<Keys, HotKeyManager.HotkeyProperty> current in HotKeyManager.templatesHotKey)
				{
					if (current.Value.TemplateName == templateName)
					{
						keys = current.Key;
						break;
					}
				}
				if (keys != Keys.None)
				{
					HotKeyManager.templatesHotKey.Remove(keys);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void RemoveHotkey_ByKey(Keys pressedKey)
		{
			try
			{
				if (HotKeyManager.systemHotKeys.ContainsKey(pressedKey))
				{
					HotKeyManager.systemHotKeys.Remove(pressedKey);
				}
				if (HotKeyManager.templatesHotKey.ContainsKey(pressedKey))
				{
					HotKeyManager.templatesHotKey.Remove(pressedKey);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static HotKeyManager.HotkeyProperty GetTemplateHotKey(Keys pressedKey)
		{
			HotKeyManager.HotkeyProperty result;
			if (HotKeyManager.templatesHotKey.ContainsKey(pressedKey))
			{
				result = HotKeyManager.templatesHotKey[pressedKey];
			}
			else
			{
				result = null;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Keys GetKeyFromTemplateHotKey(string templateName)
		{
			Keys result;
			foreach (KeyValuePair<Keys, HotKeyManager.HotkeyProperty> current in HotKeyManager.templatesHotKey)
			{
				if (templateName == current.Value.TemplateName)
				{
					result = current.Key;
					return result;
				}
			}
			result = Keys.None;
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static HotKeyFor GetSystemHotKey(Keys pressedKey)
		{
			HotKeyFor result;
			if (HotKeyManager.systemHotKeys.ContainsKey(pressedKey))
			{
				result = HotKeyManager.systemHotKeys[pressedKey];
			}
			else
			{
				result = HotKeyFor.NoAction;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Keys GetSystemHotKey(HotKeyFor keyFor)
		{
			Keys result;
			if (HotKeyManager.systemHotKeys.ContainsValue(keyFor))
			{
				foreach (KeyValuePair<Keys, HotKeyFor> current in HotKeyManager.systemHotKeys)
				{
					if (keyFor == current.Value)
					{
						result = current.Key;
						return result;
					}
				}
			}
			result = Keys.None;
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void LoadSystemHotkey()
		{
			try
			{
				HotKeyManager.systemHotKeys.Clear();
				HotKeyManager.AddSystemHotkey(0, (Keys)262259, true);
				HotKeyManager.AddSystemHotkey(1, Keys.Pause, true);
				HotKeyManager.AddSystemHotkey(2, (Keys)131162, true);
				HotKeyManager.AddSystemHotkey(4, (Keys)262227, true);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Initial()
		{
			try
			{
				TemplateManager.Instance.DefaultTemplateName = string.Empty;
				TemplateManager.Instance.DefaultTemplateGroup = string.Empty;
				HotKeyManager.ClearTemplateHotkey();
				UserWorkingProfileDS userWorkingProfileDS = new UserWorkingProfileDS();
				string templatePathByUser = ApplicationInfo.GetTemplatePathByUser();
				string text = templatePathByUser + "\\UserProfile.xml";
				if (File.Exists(text))
				{
					userWorkingProfileDS.ReadXml(text);
				}
				HotKeyManager.InputHotkey("Fixed", userWorkingProfileDS);
				HotKeyManager.InputHotkey("Order", userWorkingProfileDS);
				userWorkingProfileDS.Clear();
				userWorkingProfileDS.Dispose();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void InputHotkey(string groupName, UserWorkingProfileDS tdsUserProfile)
		{
			try
			{
				List<MainFixedTemplate.TemplateProperty> list = null;
				if (groupName == "Fixed")
				{
					list = MainFixedTemplate.GetFixedTemplateName();
				}
				else
				{
					if (groupName == "Order")
					{
						list = MainFixedTemplate.GetOrderTemplateName();
					}
				}
				UserWorkingProfileDS.TemplateItemRow[] array = (UserWorkingProfileDS.TemplateItemRow[])tdsUserProfile.TemplateItem.Select("RootTemplateName='" + groupName + "'", tdsUserProfile.TemplateItem.ListIndexColumn.ColumnName);
				string text = string.Empty;
				if (array.Length > 0)
				{
					UserWorkingProfileDS.TemplateItemRow[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						UserWorkingProfileDS.TemplateItemRow templateItemRow = array2[i];
						if (templateItemRow != null)
						{
							foreach (MainFixedTemplate.TemplateProperty current in list)
							{
								if (current.TempateName == templateItemRow.ItemName)
								{
									current.Hotkey = templateItemRow.HotKey;
									break;
								}
							}
							if (templateItemRow.IsDefalutTemplate)
							{
								text = templateItemRow.ItemName;
							}
						}
					}
				}
				foreach (MainFixedTemplate.TemplateProperty current2 in list)
				{
					HotKeyManager.AddTemplateHotkey(current2.TempateName, groupName, (Keys)current2.Hotkey);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public HotKeyManager()
		{
		}
	}
}
