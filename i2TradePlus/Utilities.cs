using i2TradePlus.Information;
using ITSNet.Common.BIZ;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
namespace i2TradePlus
{
	public class Utilities
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		static Utilities()
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Color ComparePriceColor(decimal price, decimal priceCompare)
		{
			Color result;
			if (price > priceCompare)
			{
				result = Color.Lime;
			}
			else
			{
				if (price < priceCompare)
				{
					result = Color.Red;
				}
				else
				{
					result = Color.Yellow;
				}
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Color ComparePriceCFColor(decimal price, StockList.StockInformation stockInfo)
		{
			Color result = Color.White;
			if (stockInfo.Number > -1)
			{
				if (price == stockInfo.Ceiling)
				{
					result = Color.Cyan;
				}
				else
				{
					if (price == stockInfo.Floor)
					{
						result = Color.Magenta;
					}
					else
					{
						if (stockInfo.PriorPrice > 0m && price > stockInfo.PriorPrice)
						{
							result = Color.Lime;
						}
						else
						{
							if (price > 0m && stockInfo.PriorPrice > 0m && price < stockInfo.PriorPrice)
							{
								result = Color.Red;
							}
							else
							{
								result = Color.Yellow;
							}
						}
					}
				}
			}
			else
			{
				result = Color.White;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Color ComparePriceCFColor(string price, StockList.StockInformation stockinfo)
		{
			Color result = Color.Yellow;
			decimal num;
			if (decimal.TryParse(price, out num))
			{
				if (num > 0m)
				{
					result = Utilities.ComparePriceCFColor(num, stockinfo);
				}
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Color ComparePriceCFColor(object price, SeriesList.SeriesInformation seriesInfo)
		{
			decimal price2;
			decimal.TryParse(price.ToString(), out price2);
			return Utilities.ComparePriceCFColor(price2, seriesInfo);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Color ComparePriceCFColor(decimal price, SeriesList.SeriesInformation seriesInfo)
		{
			Color result = Color.White;
			if (!string.IsNullOrEmpty(seriesInfo.Symbol))
			{
				if (price == seriesInfo.Ceiling)
				{
					result = Color.Cyan;
				}
				else
				{
					if (price == seriesInfo.Floor)
					{
						result = Color.Magenta;
					}
					else
					{
						if (price > seriesInfo.PrevFixPrice)
						{
							result = Color.Lime;
						}
						else
						{
							if (price < seriesInfo.PrevFixPrice)
							{
								result = Color.Red;
							}
							else
							{
								result = Color.Yellow;
							}
						}
					}
				}
			}
			else
			{
				result = Color.White;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Color ComparePriceCFColor(decimal price, decimal priceCompare, StockList.StockInformation stockInfo)
		{
			Color result = Color.White;
			if (stockInfo.Number > -1)
			{
				if (price == stockInfo.Ceiling)
				{
					result = Color.Cyan;
				}
				else
				{
					if (price == stockInfo.Floor)
					{
						result = Color.Magenta;
					}
					else
					{
						if (price > priceCompare)
						{
							result = Color.Lime;
						}
						else
						{
							if (price < priceCompare)
							{
								result = Color.Red;
							}
							else
							{
								result = Color.Yellow;
							}
						}
					}
				}
			}
			else
			{
				result = Color.White;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string PriceFormat(object price)
		{
			return FormatUtil.PriceFormat(price);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string PriceFormat(object price, int numOfDec)
		{
			return FormatUtil.PriceFormat(price, numOfDec, "");
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string PriceFormat(object price, int numOfDec, object defaultValue)
		{
			return FormatUtil.PriceFormat(price, numOfDec, defaultValue);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string PriceFormat(object price, string unit)
		{
			return FormatUtil.PriceFormat(price, false, unit, 2);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string PriceFormat(object price, bool sign, string unit)
		{
			return FormatUtil.PriceFormat(price, sign, unit, 2);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string PriceFormat(object price, bool sign, int numOfDec)
		{
			return FormatUtil.PriceFormat(price, sign, "", numOfDec);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string PriceBySideFormat(object price, string buySellSide)
		{
			return FormatUtil.PriceBySideFormat(price, buySellSide);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string VolumeFormat(object volume, bool comma)
		{
			return FormatUtil.VolumeFormat(volume, comma);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string BidOfferPriceFormat(string price, long volume)
		{
			string text = string.Empty;
			string result;
			try
			{
				text = Utilities.PriceFormat(price);
				if (string.IsNullOrEmpty(text) && volume > 0L)
				{
					if (ApplicationInfo.MarketSession == 1)
					{
						text = "ATO";
					}
					else
					{
						if (ApplicationInfo.MarketSession == 2)
						{
							if (ApplicationInfo.MarketState == "P" || ApplicationInfo.MarketState == "O")
							{
								text = "ATO";
							}
							else
							{
								text = "ATC";
							}
						}
					}
				}
			}
			catch
			{
				result = string.Empty;
				return result;
			}
			result = text;
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string BidOfferPriceFormat(decimal price, long volume)
		{
			string text = string.Empty;
			string result;
			try
			{
				if (price == 0m && volume > 0L)
				{
					if (ApplicationInfo.MarketSession == 1)
					{
						text = "ATO";
					}
					else
					{
						if (ApplicationInfo.MarketSession == 2)
						{
							if (ApplicationInfo.MarketState == "P" || ApplicationInfo.MarketState == "O")
							{
								text = "ATO";
							}
							else
							{
								text = "ATC";
							}
						}
					}
				}
				else
				{
					text = Utilities.PriceFormat(price);
				}
			}
			catch
			{
				result = string.Empty;
				return result;
			}
			result = text;
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string SetPrice(object price, decimal priceCompare)
		{
			return Utilities.SetPrice(price, priceCompare, string.Empty);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string SetPrice(object price, decimal priceCompare, ref Color color)
		{
			return Utilities.SetPrice(price, priceCompare, string.Empty, ref color);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string SetPrice(object price, decimal priceCompare, string unit)
		{
			Color white = Color.White;
			return Utilities.SetPrice(price, priceCompare, ref white);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string SetPrice(object price, decimal priceCompare, string unit, ref Color color)
		{
			string result = string.Empty;
			if (FormatUtil.Isnumeric(price.ToString()))
			{
				result = FormatUtil.PriceFormat(price, false, unit, 2);
				color = Utilities.ComparePriceColor(Convert.ToDecimal(price), priceCompare);
			}
			else
			{
				color = (Brushes.Red as SolidBrush).Color;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static decimal GetSpreadPrice(decimal lastSalePrice, decimal prior, string checkSpread)
		{
			decimal d = lastSalePrice;
			if (d == 0m)
			{
				d = prior;
			}
			decimal result;
			if (checkSpread == "N")
			{
				result = 0.01m;
			}
			else
			{
				if (d >= 400m)
				{
					result = 2m;
				}
				else
				{
					if (d >= 200m && d < 400m)
					{
						result = 1m;
					}
					else
					{
						if (d >= 100m && d < 200m)
						{
							result = 0.50m;
						}
						else
						{
							if (d >= 25m && d < 100m)
							{
								result = 0.25m;
							}
							else
							{
								if (d >= 10m && d < 25m)
								{
									result = 0.1m;
								}
								else
								{
									if (d >= 5m && d < 10m)
									{
										result = 0.05m;
									}
									else
									{
										if (d >= 2m && d < 5m)
										{
											result = 0.02m;
										}
										else
										{
											if (d > 0m && d < 2m)
											{
												result = 0.01m;
											}
											else
											{
												result = -1m;
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static long GetPublishedVolumeDisplay(long volume, long publicVolume, long matchedVolume, string status)
		{
			string text = status.Trim();
			long result;
			switch (text)
			{
			case "X":
			case "XA":
			case "R":
			case "C":
			case "M":
			case "MA":
				result = 0L;
				return result;
			}
			if (publicVolume > 0L)
			{
				if (matchedVolume == volume)
				{
					result = 0L;
				}
				else
				{
					long num2 = volume - matchedVolume;
					if (num2 <= publicVolume)
					{
						result = num2;
					}
					else
					{
						num2 = (volume - matchedVolume) % 1000L;
						if (num2 == 0L)
						{
							result = publicVolume;
						}
						else
						{
							result = num2;
						}
					}
				}
			}
			else
			{
				result = volume - matchedVolume;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string GetTime(string value)
		{
			string result;
			try
			{
				if (value.Length == 6)
				{
					result = string.Concat(new string[]
					{
						value.Substring(0, 2),
						":",
						value.Substring(2, 2),
						":",
						value.Substring(4, 2)
					});
				}
				else
				{
					if (value.Length == 5)
					{
						result = string.Concat(new string[]
						{
							value.Substring(0, 1),
							":",
							value.Substring(1, 2),
							":",
							value.Substring(3, 2)
						});
					}
					else
					{
						if (value.Length == 8 && value.IndexOf(':') == -1)
						{
							result = string.Concat(new string[]
							{
								value.Substring(0, 4),
								"/",
								value.Substring(4, 2),
								"/",
								value.Substring(6, 2)
							});
						}
						else
						{
							result = value;
						}
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
		public static string GetShortTime(string value)
		{
			string result;
			try
			{
				if (value.Length == 6)
				{
					result = value.Substring(0, 2) + ":" + value.Substring(2, 2);
				}
				else
				{
					if (value.Length == 5)
					{
						result = value.Substring(0, 1) + ":" + value.Substring(1, 2);
					}
					else
					{
						result = value;
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
		public static string GetDateFormat(string date)
		{
			string result = string.Empty;
			try
			{
				string text = date.Substring(2, 2);
				string text2 = date.Substring(4, 2);
				string text3 = date.Substring(6, 2);
				string text4 = string.Empty;
				string text5 = text2;
				switch (text5)
				{
				case "01":
					text4 = "Jan";
					break;
				case "02":
					text4 = "Fab";
					break;
				case "03":
					text4 = "Mar";
					break;
				case "04":
					text4 = "Apr";
					break;
				case "05":
					text4 = "May";
					break;
				case "06":
					text4 = "Jun";
					break;
				case "07":
					text4 = "Jul";
					break;
				case "08":
					text4 = "Aug";
					break;
				case "09":
					text4 = "Sep";
					break;
				case "10":
					text4 = "Oct";
					break;
				case "11":
					text4 = "Nov";
					break;
				case "12":
					text4 = "Dec";
					break;
				}
				result = string.Concat(new string[]
				{
					text3,
					" ",
					text4,
					" ",
					text
				});
			}
			catch (Exception innerException)
			{
				throw new Exception("Error from method 'GetDateFormat' ==> ", innerException);
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string GetTimeLastSale(string time)
		{
			string str = string.Empty;
			string text = string.Empty;
			try
			{
				if (time.Length == 6)
				{
					for (int i = 0; i < time.Length; i += 2)
					{
						str = time.Substring(i, 2);
						text = text + str + ":";
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			string result;
			if (text.Length > 7)
			{
				result = text.Substring(0, 8);
			}
			else
			{
				result = time;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static bool GetTopPriceZero(long volume, decimal price)
		{
			return volume > 0L && price == 0m;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static bool GetTopPriceZero(long volume, string price)
		{
			decimal d;
			return volume > 0L && decimal.TryParse(price.ToString(), out d) && d == 0m;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Dictionary<string, string> GetFullOptionsName()
		{
			SortedDictionary<string, string> sortedDictionary = new SortedDictionary<string, string>();
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			try
			{
				foreach (KeyValuePair<int, SeriesList.SeriesInformation> current in ApplicationInfo.SeriesInfo.Items)
				{
					if ((current.Value.Group == 1 || current.Value.Group == 2) && current.Value.ExpireDate != "0" && current.Value.ExpireDate != "")
					{
						string str = current.Value.ExpireDate.Substring(2, 2);
						string text = current.Value.ExpireDate.Substring(4, 2);
						string str2 = string.Empty;
						string text2 = text;
						switch (text2)
						{
						case "01":
							str2 = "Jan";
							break;
						case "02":
							str2 = "Feb";
							break;
						case "03":
							str2 = "Mar";
							break;
						case "04":
							str2 = "Apr";
							break;
						case "05":
							str2 = "May";
							break;
						case "06":
							str2 = "Jun";
							break;
						case "07":
							str2 = "Jul";
							break;
						case "08":
							str2 = "Aug";
							break;
						case "09":
							str2 = "Sep";
							break;
						case "10":
							str2 = "Oct";
							break;
						case "11":
							str2 = "Nov";
							break;
						case "12":
							str2 = "Dec";
							break;
						}
						string value = "SET50 Index Options " + str2 + " " + str;
						if (!sortedDictionary.ContainsKey(current.Value.ExpireDate.ToString()))
						{
							sortedDictionary.Add(current.Value.ExpireDate.ToString(), value);
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return new Dictionary<string, string>(sortedDictionary);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static DateTime GetDateValue(string sVal)
		{
			DateTime dateTime;
			DateTime result;
			try
			{
				int num = 1900;
				if (Convert.ToInt32(sVal.Substring(0, 2)) < 80)
				{
					num = 2000;
				}
				if (sVal.Length == 6)
				{
					dateTime = new DateTime(num + Convert.ToInt32(sVal.Substring(0, 2)), Convert.ToInt32(sVal.Substring(2, 2)), Convert.ToInt32(sVal.Substring(4, 2)), 0, 0, 0);
					result = dateTime;
					return result;
				}
				if (sVal.Length == 8)
				{
					if (sVal.IndexOf("-") > 0)
					{
						dateTime = new DateTime(num + Convert.ToInt32(sVal.Substring(0, 2)), Convert.ToInt32(sVal.Substring(3, 2)), Convert.ToInt32(sVal.Substring(6, 2)), 0, 0, 0);
						result = dateTime;
						return result;
					}
					dateTime = new DateTime(Convert.ToInt32(sVal.Substring(0, 4)), Convert.ToInt32(sVal.Substring(4, 2)), Convert.ToInt32(sVal.Substring(6, 2)), 0, 0, 0);
					result = dateTime;
					return result;
				}
				else
				{
					if (sVal.Length == 10)
					{
						dateTime = new DateTime(Convert.ToInt32(sVal.Substring(0, 4)), Convert.ToInt32(sVal.Substring(5, 2)), Convert.ToInt32(sVal.Substring(8, 2)), 0, 0, 0);
						result = dateTime;
						return result;
					}
					result = Convert.ToDateTime(sVal);
					return result;
				}
			}
			catch (Exception)
			{
			}
			dateTime = new DateTime(1900, 1, 1, 0, 0, 0);
			result = dateTime;
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string GetOrderSideName(string side)
		{
			string result;
			if (side != null)
			{
				if (side == "B")
				{
					result = "Buy";
					return result;
				}
				if (side == "S")
				{
					result = "Sell";
					return result;
				}
				if (side == "C")
				{
					result = "Cover";
					return result;
				}
				if (side == "H")
				{
					result = "Short";
					return result;
				}
			}
			result = side;
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string GetCreditTypeName(string cType)
		{
			string result;
			if (ApplicationInfo.SupportFreewill)
			{
				string text = cType.Trim();
				switch (text)
				{
				case "1":
					result = "Premium";
					return result;
				case "2":
					result = "Regular";
					return result;
				case "3":
					result = "Sell Only";
					return result;
				case "4":
					result = "Total Explosure";
					return result;
				case "5":
					result = "Extend Premier";
					return result;
				case "6":
					result = "Credit Line";
					return result;
				case "7":
					result = "Cash Balance";
					return result;
				case "8":
					result = "Credit Balance";
					return result;
				case "9":
					result = "Credit Balance";
					return result;
				}
				result = cType;
			}
			else
			{
				string text = cType.Trim();
				if (text != null)
				{
					if (text == "1")
					{
						result = "Regular";
						return result;
					}
				}
				result = cType;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string GetAccountTypeName(string aType)
		{
			string result;
			if (ApplicationInfo.SupportFreewill)
			{
				string text = aType.Trim();
				switch (text)
				{
				case "C":
					result = "Cash";
					return result;
				case "A":
					result = "Cash Margin";
					return result;
				case "M":
					result = "Maintenance Margin";
					return result;
				case "B":
					result = "Credit Balance";
					return result;
				case "H":
					result = "Cash Balance";
					return result;
				case "I":
					result = "Internet";
					return result;
				}
				result = aType;
			}
			else
			{
				string text = aType.Trim();
				if (text != null)
				{
					if (text == "B")
					{
						result = "Credit Balance";
						return result;
					}
					if (text == "C")
					{
						result = "C";
						return result;
					}
					if (text == "H")
					{
						result = "Cash Margin";
						return result;
					}
					if (text == "M")
					{
						result = "Margin";
						return result;
					}
				}
				result = aType;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string GetDisplayOrderStatus(string orderStatus)
		{
			string result;
			try
			{
				string text = orderStatus.Trim();
				string text2 = orderStatus.Trim();
				switch (text2)
				{
				case "A":
					text = "Approve";
					break;
				case "OA":
					text = "Opened Approve";
					break;
				case "XA":
					text = "Canceled";
					break;
				case "MA":
					text = "Match Approve";
					break;
				case "C":
					text = "Canceled";
					break;
				case "D":
					text = "Disapprove";
					break;
				case "M":
					text = "Matched";
					break;
				case "O":
				case "OC":
					text = "Opened";
					break;
				case "PO":
					text = "Pre open";
					break;
				case "POA":
					text = "Pre open Approve";
					break;
				case "PX":
				case "PXC":
					text = "Pending Cancel";
					break;
				case "R":
					text = "Rejected";
					break;
				case "X":
				case "XC":
					text = "Cancelled";
					break;
				case "PC":
					text = "Pending Chg";
					break;
				}
				result = text;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Utilities()
		{
		}
	}
}
