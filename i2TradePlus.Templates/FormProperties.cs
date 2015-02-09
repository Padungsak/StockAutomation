using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace i2TradePlus.Templates
{
	internal class FormProperties
	{
		public static readonly string[] serializablePropertyNames = new string[]
		{
			"Name",
			"Type",
			"Text"
		};
		private object form = null;
		[MethodImpl(MethodImplOptions.NoInlining)]
		public FormProperties(object form)
		{
			this.form = form;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Dictionary<string, object> GetFormProperties()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.form);
			string[] array = FormProperties.serializablePropertyNames;
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				PropertyDescriptor propertyDescriptor = properties.Find(text, false);
				if (propertyDescriptor != null)
				{
					object value = propertyDescriptor.GetValue(this.form);
					dictionary.Add(text, value);
				}
				else
				{
					if (text == "Type")
					{
						dictionary.Add("Type", this.form.GetType());
					}
				}
			}
			return dictionary;
		}
	}
}
