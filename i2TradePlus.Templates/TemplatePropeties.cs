using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace i2TradePlus.Templates
{
	internal class TemplatePropeties
	{
		public static readonly string[] serializablePropertyNames = new string[]
		{
			"IsLock"
		};
		private TemplateView template = null;
		[MethodImpl(MethodImplOptions.NoInlining)]
		public TemplatePropeties(TemplateView template)
		{
			this.template = template;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Dictionary<string, object> GetTemplateProperties()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.template);
			string[] array = TemplatePropeties.serializablePropertyNames;
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				PropertyDescriptor propertyDescriptor = properties.Find(text, false);
				if (propertyDescriptor != null)
				{
					object value = propertyDescriptor.GetValue(this.template);
					dictionary.Add(text, value);
				}
			}
			return dictionary;
		}
	}
}
