using System;
using System.Configuration;
using System.Runtime.CompilerServices;
namespace ITSNet.Common.BIZ.Configuration
{
	public class CTCIFormatConfiguration : ConfigurationSection, IFormatConfiguration
	{
		[ConfigurationProperty("B1")]
		public MessageFormatElementCollection B1
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (MessageFormatElementCollection)base["B1"];
			}
		}
		[ConfigurationProperty("I1")]
		public MessageFormatElementCollection I1
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (MessageFormatElementCollection)base["I1"];
			}
		}
		[ConfigurationProperty("C1")]
		public MessageFormatElementCollection C1
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (MessageFormatElementCollection)base["C1"];
			}
		}
		[ConfigurationProperty("D1")]
		public MessageFormatElementCollection D1
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (MessageFormatElementCollection)base["D1"];
			}
		}
		[ConfigurationProperty("E1")]
		public MessageFormatElementCollection E1
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (MessageFormatElementCollection)base["E1"];
			}
		}
		[ConfigurationProperty("F1")]
		public MessageFormatElementCollection F1
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (MessageFormatElementCollection)base["F1"];
			}
		}
		[ConfigurationProperty("G1")]
		public MessageFormatElementCollection G1
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (MessageFormatElementCollection)base["G1"];
			}
		}
		[ConfigurationProperty("B2")]
		public MessageFormatElementCollection B2
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (MessageFormatElementCollection)base["B2"];
			}
		}
		[ConfigurationProperty("C2")]
		public MessageFormatElementCollection C2
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (MessageFormatElementCollection)base["C2"];
			}
		}
		[ConfigurationProperty("D2")]
		public MessageFormatElementCollection D2
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (MessageFormatElementCollection)base["D2"];
			}
		}
		[ConfigurationProperty("E2")]
		public MessageFormatElementCollection E2
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (MessageFormatElementCollection)base["E2"];
			}
		}
		[ConfigurationProperty("G2")]
		public MessageFormatElementCollection G2
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (MessageFormatElementCollection)base["G2"];
			}
		}
		[ConfigurationProperty("I2")]
		public MessageFormatElementCollection I2
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (MessageFormatElementCollection)base["I2"];
			}
		}
		[ConfigurationProperty("F2")]
		public MessageFormatElementCollection F2
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (MessageFormatElementCollection)base["F2"];
			}
		}
		[ConfigurationProperty("L2")]
		public MessageFormatElementCollection L2
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (MessageFormatElementCollection)base["L2"];
			}
		}
		[ConfigurationProperty("A3")]
		public MessageFormatElementCollection A3
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (MessageFormatElementCollection)base["A3"];
			}
		}
		[ConfigurationProperty("B3")]
		public MessageFormatElementCollection B3
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (MessageFormatElementCollection)base["B3"];
			}
		}
		[ConfigurationProperty("C3")]
		public MessageFormatElementCollection C3
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (MessageFormatElementCollection)base["C3"];
			}
		}
		[ConfigurationProperty("D3")]
		public MessageFormatElementCollection D3
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (MessageFormatElementCollection)base["D3"];
			}
		}
		[ConfigurationProperty("I7")]
		public MessageFormatElementCollection I7
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (MessageFormatElementCollection)base["I7"];
			}
		}
		[ConfigurationProperty("C7")]
		public MessageFormatElementCollection C7
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (MessageFormatElementCollection)base["C7"];
			}
		}
		[ConfigurationProperty("G8")]
		public MessageFormatElementCollection G8
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (MessageFormatElementCollection)base["G8"];
			}
		}
		[ConfigurationProperty("E8")]
		public MessageFormatElementCollection E8
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (MessageFormatElementCollection)base["E8"];
			}
		}
		[ConfigurationProperty("I8")]
		public MessageFormatElementCollection I8
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (MessageFormatElementCollection)base["I8"];
			}
		}
		[ConfigurationProperty("TS")]
		public MessageFormatElementCollection TS
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return (MessageFormatElementCollection)base["TS"];
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public MessageFormatElementCollection GetFormaterFormType(string formatType)
		{
			MessageFormatElementCollection result = null;
			string key;
			switch (key = formatType.ToUpper())
			{
			case "1B":
				result = this.B1;
				break;
			case "1I":
				result = this.I1;
				break;
			case "1C":
				result = this.C1;
				break;
			case "1D":
				result = this.D1;
				break;
			case "1E":
				result = this.E1;
				break;
			case "1F":
				result = this.F1;
				break;
			case "1G":
				result = this.G1;
				break;
			case "2B":
				result = this.B2;
				break;
			case "2C":
				result = this.C2;
				break;
			case "2D":
				result = this.D2;
				break;
			case "2E":
				result = this.E2;
				break;
			case "2G":
				result = this.G2;
				break;
			case "2I":
				result = this.I2;
				break;
			case "2F":
				result = this.F2;
				break;
			case "2L":
				result = this.L2;
				break;
			case "3A":
				result = this.A3;
				break;
			case "3B":
				result = this.B3;
				break;
			case "3C":
				result = this.C3;
				break;
			case "3D":
				result = this.D3;
				break;
			case "7I":
				result = this.I7;
				break;
			case "7C":
				result = this.C7;
				break;
			case "8G":
				result = this.G8;
				break;
			case "8E":
				result = this.E8;
				break;
			case "8I":
				result = this.I8;
				break;
			case "TS":
				result = this.TS;
				break;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public CTCIFormatConfiguration()
		{
		}
	}
}
