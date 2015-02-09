using System;
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
namespace ITSNet.Common.BIZ
{
	public class FormatUtil
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		public FormatUtil()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static bool Isnumeric(object value)
		{
			double num;
			return value != null && double.TryParse(value.ToString(), out num);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static bool Isdatetime(object value)
		{
			DateTime dateTime;
			return DateTime.TryParse(value.ToString(), out dateTime);
		}
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
		public static DateTime IntTimeToTodayTime(int timeIntForm)
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			double num = (double)(timeIntForm / 10000);
			double value = (double)(timeIntForm % 100);
			double value2 = (double)((timeIntForm - 10000 * (int)num) / 100);
			return DateTime.Today.AddHours(num).AddMinutes(value2).AddSeconds(value);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Color ComparePriceColor(decimal price, decimal priceCompare)
		{
			if (price > priceCompare)
			{
				return Color.Lime;
			}
			if (price < priceCompare)
			{
				return Color.Red;
			}
			return Color.Yellow;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string PriceFormat(object price)
		{
			return FormatUtil.PriceFormat(price, 2, "");
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string PriceFormat(object price, int numOfDec, object defaultValue)
		{
			string result = price.ToString();
			try
			{
				double num;
				if (double.TryParse(price.ToString(), out num))
				{
					if (Math.Round(num, numOfDec) == 0.0)
					{
						result = defaultValue.ToString();
					}
					else
					{
						if (num == (double)Convert.ToInt64(num))
						{
							result = num.ToString("#,##0");
						}
						else
						{
							string text = "0";
							result = num.ToString("#,##0." + text.PadRight(numOfDec, '0'));
						}
					}
				}
			}
			catch
			{
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string PriceFormat(object price, bool sign, string unit, int numOfDec)
		{
			string text = price.ToString();
			try
			{
				text = FormatUtil.PriceFormat(price, numOfDec, "");
				if (text != string.Empty)
				{
					text += unit;
					if (sign && !text.StartsWith("-"))
					{
						text = "+" + text;
					}
				}
			}
			catch
			{
			}
			return text;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string PriceBySideFormat(object price, string buySellSide)
		{
			string text = price.ToString();
			try
			{
				text = FormatUtil.PriceFormat(price, 2, 0);
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
			}
			catch
			{
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
						result = num.ToString("F0");
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
		public static string ValueFormat(decimal value, bool comma)
		{
			string result = string.Empty;
			if (value == 0m)
			{
				result = "0";
			}
			else
			{
				if (value == Convert.ToInt64(value))
				{
					if (comma)
					{
						result = value.ToString("#,###");
					}
					else
					{
						result = value.ToString("####");
					}
				}
				else
				{
					if (comma)
					{
						result = value.ToString("#,###.##");
					}
					else
					{
						result = value.ToString("####.##");
					}
				}
			}
			return result;
		}
	}
}
