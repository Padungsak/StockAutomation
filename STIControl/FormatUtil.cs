using System;
using System.Runtime.CompilerServices;
namespace STIControl
{
	internal class FormatUtil
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static int NowTimeToIntTime()
		{
			int result = 0;
			try
			{
				result = FormatUtil.DateTimeToIntTime(DateTime.Now);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static int DateTimeToIntTime(DateTime dateTime)
		{
			int result = 0;
			try
			{
				string value = dateTime.ToString("HHmmss");
				result = Convert.ToInt32(value);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string PriceFormat(decimal price)
		{
			string result = string.Empty;
			if (Math.Round(price, 2) == 0m)
			{
				result = string.Empty;
			}
			else
			{
				if (price == Convert.ToInt64(price))
				{
					result = price.ToString("#,##0");
				}
				else
				{
					result = price.ToString("#,##0.00");
				}
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string PriceFormat(object price)
		{
			string result = string.Empty;
			double num;
			if (double.TryParse(price.ToString(), out num))
			{
				if (Math.Round(num, 2) == 0.0)
				{
					result = string.Empty;
				}
				else
				{
					if (num == (double)Convert.ToInt64(num))
					{
						result = num.ToString("#,##0");
					}
					else
					{
						result = num.ToString("#,##0.00");
					}
				}
			}
			else
			{
				result = price.ToString();
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string PriceFormat(object price, string unit)
		{
			string text = FormatUtil.PriceFormat(price);
			if (text != string.Empty)
			{
				text += unit;
			}
			return text;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string PriceFormat(object price, bool sign, string unit)
		{
			string text = FormatUtil.PriceFormat(price, unit);
			decimal d;
			if (text != string.Empty && decimal.TryParse(text, out d) && sign && d > 0m)
			{
				return "+" + text;
			}
			return text;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string PriceBySideFormat(object price, string buySellSide)
		{
			string text = FormatUtil.PriceFormat(price);
			if (buySellSide.ToUpper() == "B")
			{
				text = "+" + text;
			}
			else
			{
				if (buySellSide.ToUpper() == "S")
				{
					text = "-" + text;
				}
			}
			return text;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string VolumeFormat(object volume, bool comma)
		{
			string result = string.Empty;
			double num;
			if (double.TryParse(volume.ToString(), out num))
			{
				if (num == 0.0)
				{
					result = string.Empty;
				}
				else
				{
					if (comma)
					{
						result = num.ToString("#,###");
					}
					else
					{
						result = volume.ToString();
					}
				}
			}
			else
			{
				result = volume.ToString();
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public FormatUtil()
		{
		}
	}
}
