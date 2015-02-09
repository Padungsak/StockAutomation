using System;
using System.Configuration;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ
{
	public class MessageFormatElement : ConfigurationElement
	{
		[ConfigurationProperty("Name", IsKey = true, IsRequired = true)]
		public string Name
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (string)base["Name"];
			}
		}
		[ConfigurationProperty("Type")]
		public int Type
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (int)base["Type"];
			}
		}
		[ConfigurationProperty("Size")]
		public int Size
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (int)base["Size"];
			}
		}
		[ConfigurationProperty("Dec")]
		public int Dec
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (int)base["Dec"];
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public MessageFormatElement()
		{
		}
	}
}
