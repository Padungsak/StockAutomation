using System;
using System.Configuration;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ
{
	public class MessageFormatElementCollection : ConfigurationElementCollection
	{
		public new MessageFormatElement this[string key]
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (MessageFormatElement)base.BaseGet(key);
			}
		}
		public MessageFormatElement this[int index]
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (MessageFormatElement)base.BaseGet(index);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override ConfigurationElement CreateNewElement()
		{
			return new MessageFormatElement();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((MessageFormatElement)element).Name;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public MessageFormatElementCollection()
		{
		}
	}
}
