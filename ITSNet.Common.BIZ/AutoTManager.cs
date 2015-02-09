using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;
namespace ITSNet.Common.BIZ
{
	public class AutoTManager
	{
		public enum FieldType
		{
			NormalString,
			AlphaNumeric,
			AlphaString,
			MOD96int,
			MOD96dot,
			MOD96dot5Dec
		}
		public const char US_CHAR = '\u001f';
		public const char BEL_CHAR = '\a';
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static long Demod96int(string S)
		{
			long result;
			try
			{
				result = Convert.ToInt64(AutoTManager.Demod96(S));
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static decimal Demod96dot(string S)
		{
			decimal result;
			try
			{
				result = AutoTManager.Demod96(S) / 100m;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static decimal Demod96dot5Dec(string S)
		{
			decimal result;
			try
			{
				result = AutoTManager.Demod96(S) / 100000m;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static decimal Demod96(string S)
		{
			decimal num = 0m;
			try
			{
				char[] array = S.ToCharArray();
				for (int i = 1; i <= S.Length; i++)
				{
					num += Convert.ToDecimal((double)(array[i - 1] - ' ') * Math.Pow(96.0, (double)(S.Length - i)));
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return num;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static string Mod96int(long value, int length)
		{
			return AutoTManager.Mod96(value, length);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string Mode96dot(decimal value, int length)
		{
			long value2 = (long)(value * 100m);
			return AutoTManager.Mod96(value2, length);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string Mod96(long value, int length)
		{
			string result;
			try
			{
				long num = value;
				string text = string.Empty;
				byte[] array = new byte[length];
				int num2 = 0;
				while (num >= 96L)
				{
					array[num2] = (byte)(num % 96L);
					num /= 96L;
					if (num2 == length)
					{
						result = string.Empty;
						return result;
					}
					num2++;
				}
				if (num != 0L)
				{
					array[num2] = (byte)num;
					num2++;
				}
				for (int i = 0; i < length; i++)
				{
					array[i] += 32;
				}
				text = Encoding.ASCII.GetString(array);
				char[] array2 = text.ToCharArray();
				Array.Reverse(array2);
				text = new string(array2);
				result = text;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static string FullSizeString(string value, int length)
		{
			string text = value;
			for (int i = value.Length; i < length; i++)
			{
				text += " ";
			}
			return text;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static MessageAttribute Decode(string message, IFormatConfiguration formater)
		{
			MessageAttribute result;
			try
			{
				result.MessageType = message.Substring(0, 2);
				result.OriginalMessage = message;
				result.MessageNameValue = new Hashtable();
				MessageFormatElementCollection formaterFormType = formater.GetFormaterFormType(result.MessageType);
				if (formaterFormType != null && formaterFormType.Count > 0)
				{
					int num = 0;
					foreach (MessageFormatElement messageFormatElement in formaterFormType)
					{
						int num2 = (messageFormatElement.Size > 0) ? messageFormatElement.Size : (message.Length - num);
						if (!messageFormatElement.Name.ToLower().StartsWith("filler"))
						{
							string text = message.Substring(num, num2);
							switch (messageFormatElement.Type)
							{
							case 3:
								text = AutoTManager.Demod96int(text).ToString();
								break;
							case 4:
								text = AutoTManager.Demod96dot(text).ToString();
								break;
							case 5:
								text = AutoTManager.Demod96dot5Dec(text).ToString();
								break;
							default:
								text = text.Trim();
								if (messageFormatElement.Dec > 0 && FormatUtil.Isnumeric(text))
								{
									long num3 = Convert.ToInt64(text);
									text = ((decimal)((double)num3 / Math.Pow(10.0, (double)messageFormatElement.Dec))).ToString();
								}
								break;
							}
							result.MessageNameValue.Add(messageFormatElement.Name, text);
						}
						num += num2;
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static MessageAttribute DecodeTFEX(string message, IFormatConfiguration formater)
		{
			MessageAttribute result;
			try
			{
				result.MessageType = message.Substring(0, 4).Trim();
				result.OriginalMessage = message;
				result.MessageNameValue = new Hashtable();
				MessageFormatElementCollection formaterFormType = formater.GetFormaterFormType(result.MessageType);
				if (formaterFormType != null && formaterFormType.Count > 0)
				{
					int num = 0;
					foreach (MessageFormatElement messageFormatElement in formaterFormType)
					{
						int num2 = (messageFormatElement.Size > 0) ? messageFormatElement.Size : (message.Length - num);
						if (!messageFormatElement.Name.ToLower().StartsWith("filler"))
						{
							string text = message.Substring(num, num2);
							text = text.Trim();
							if (messageFormatElement.Dec > 0 && FormatUtil.Isnumeric(text))
							{
								long num3 = Convert.ToInt64(text);
								text = ((decimal)((double)num3 / Math.Pow(10.0, (double)messageFormatElement.Dec))).ToString();
							}
							result.MessageNameValue.Add(messageFormatElement.Name, text);
						}
						num += num2;
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string Encode(string messageType, Hashtable elements, IFormatConfiguration formater)
		{
			string text = string.Empty;
			MessageFormatElementCollection formaterFormType = formater.GetFormaterFormType(messageType);
			if (formaterFormType != null && formaterFormType.Count > 0)
			{
				foreach (MessageFormatElement messageFormatElement in formaterFormType)
				{
					if (elements.ContainsKey(messageFormatElement.Name))
					{
						string text2 = elements[messageFormatElement.Name].ToString();
						switch (messageFormatElement.Type)
						{
						case 3:
						{
							long value = Convert.ToInt64(text2);
							text2 = AutoTManager.Mod96int(value, messageFormatElement.Size);
							break;
						}
						case 4:
						{
							decimal value2 = Convert.ToDecimal(text2);
							text2 = AutoTManager.Mode96dot(value2, messageFormatElement.Size);
							break;
						}
						default:
							if (messageFormatElement.Dec > 0 && FormatUtil.Isnumeric(text2))
							{
								decimal value2 = Convert.ToDecimal(text2);
								text2 = ((long)((double)value2 * Math.Pow(10.0, (double)messageFormatElement.Dec))).ToString();
							}
							text2 = AutoTManager.FullSizeString(text2, messageFormatElement.Size);
							break;
						}
						text += text2;
					}
					else
					{
						if (messageFormatElement.Name.ToLower().StartsWith("filler"))
						{
							text += AutoTManager.FullSizeString(string.Empty, messageFormatElement.Size);
						}
						else
						{
							text = text + messageFormatElement.Name + ":ERR";
						}
					}
				}
			}
			return text;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public AutoTManager()
		{
		}
	}
}
