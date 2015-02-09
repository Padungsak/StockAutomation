using ITSNet.Common.BIZ;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
public class clsGraphPanel
{
	private List<VolumeGraphItem> _graphItems;
	private List<ItemByStock> _itemsByStock;
	public string strSymbolList;
	private double dblLast;
	private double dblPrior;
	private double dblCeiling = 9E-05;
	private double dblFloor = 9E-05;
	private long _lngMaxVol;
	private double dblIPO;
	private Font m_Font;
	private Rectangle m_Rect;
	private int sngBottom;
	private int sngTop;
	private int intLastIndex;
	public bool bGetWs = true;
	public bool bException;
	public enumType oSymbolType = enumType.eSet;
	public clsPermission oActiveTFEX = new clsPermission();
	public clsPermission oActiveSET = new clsPermission();
	public Rectangle oldRect = default(Rectangle);
	public Dictionary<string, float> dictIPO = new Dictionary<string, float>();
	public Color m_UpColor = Color.Lime;
	public Color m_DownColor = Color.Red;
	public Color m_NoChgColor = Color.Yellow;
	public Color m_VolumeColor = Color.Yellow;
	public Color m_BgColor = Color.Black;
	public Color m_CeilingColor = Color.Aqua;
	public Color m_ValueColor = Color.White;
	public Color m_FloorColor = Color.Fuchsia;
	public Color m_GridColor = Color.LightGray;
	public float m_FontSize = 9.25f;
	public string m_FontName = "Arial";
	private Color m_BuyColor = Color.Lime;
	private Color m_SellColor = Color.Red;
	private StringFormat sfDraw = new StringFormat();
	public Color BuyColor
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.m_BuyColor;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			this.m_BuyColor = value;
		}
	}
	public Color SellColor
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.m_SellColor;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			this.m_SellColor = value;
		}
	}
	public clsPermission ActiveType
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			clsPermission result = null;
			if (this.oSymbolType == enumType.eSet)
			{
				result = this.oActiveSET;
			}
			else
			{
				if (this.oSymbolType == enumType.eTfex)
				{
					result = this.oActiveTFEX;
				}
			}
			return result;
		}
	}
	public int Width
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.m_Rect.Width;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			this.m_Rect.Width = value;
		}
	}
	public int Height
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.m_Rect.Height;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			this.m_Rect.Height = value;
		}
	}
	public string SymbolList
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.strSymbolList;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			this.strSymbolList = value;
		}
	}
	public double Ceiling
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.dblCeiling;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			this.dblCeiling = value;
		}
	}
	public double Floor
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.dblFloor;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			this.dblFloor = value;
		}
	}
	public double Last
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.dblLast;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			this.dblLast = value;
		}
	}
	public double Prior
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.dblPrior;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			this.dblPrior = value;
			if (this.dblPrior == 0.0)
			{
				this.IPO = this.GetIPOPrice(this.SymbolList);
			}
		}
	}
	public double IPO
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			return this.dblIPO;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		set
		{
			this.dblIPO = value;
		}
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	public clsGraphPanel()
	{
		this._graphItems = new List<VolumeGraphItem>();
		this._itemsByStock = new List<ItemByStock>();
		this.m_Rect = default(Rectangle);
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	private double GetIPOPrice(string strSymbol)
	{
		double result = 0.0;
		if (this.dictIPO.ContainsKey(strSymbol))
		{
			result = (double)this.dictIPO[strSymbol];
		}
		return result;
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	public void Clear()
	{
		try
		{
			if (this._graphItems != null)
			{
				this._graphItems.Clear();
			}
			if (this._itemsByStock != null)
			{
				this._itemsByStock.Clear();
			}
			this.strSymbolList = "";
			this._lngMaxVol = 0L;
			this.dblCeiling = 99999.99;
			this.dblFloor = 99999.99;
			this.dblLast = 0.0;
			this.dblPrior = 0.0;
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	public void Add(double dblPrice, long lngAccVol, long lngBuyVol, long lngSellVol)
	{
		try
		{
			VolumeGraphItem volumeGraphItem = new VolumeGraphItem();
			volumeGraphItem.Price = dblPrice;
			volumeGraphItem.AccVol = lngAccVol;
			if (lngAccVol > this._lngMaxVol)
			{
				this._lngMaxVol = lngAccVol;
			}
			volumeGraphItem.BuyVol = lngBuyVol;
			volumeGraphItem.SellVol = lngSellVol;
			volumeGraphItem.AtoCVol = (double)(volumeGraphItem.AccVol - volumeGraphItem.SellVol - volumeGraphItem.BuyVol);
			this._graphItems.Add(volumeGraphItem);
		}
		catch (Exception ex)
		{
			throw new Exception("Sub Add clsGraphPanel " + ex.Message);
		}
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	public void Add(string stock, double value, double profit)
	{
		try
		{
			ItemByStock itemByStock = new ItemByStock();
			itemByStock.Stock = stock;
			itemByStock.Value = value;
			itemByStock.Profit = profit;
			if (value > (double)this._lngMaxVol)
			{
				this._lngMaxVol = (long)value;
			}
			this._itemsByStock.Add(itemByStock);
		}
		catch (Exception ex)
		{
			throw new Exception("Sub Add clsGraphPanel " + ex.Message);
		}
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	private long GetMaxVolume()
	{
		long num = 0L;
		try
		{
			foreach (VolumeGraphItem current in this._graphItems)
			{
				if (current.AccVol > num)
				{
					num = current.AccVol;
				}
			}
		}
		catch (Exception ex)
		{
			throw new Exception("Function GetMaxVolume clsGraphPanel " + ex.Message);
		}
		return num;
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	private double GetMaxValue()
	{
		double num = 0.0;
		try
		{
			foreach (ItemByStock current in this._itemsByStock)
			{
				if (current.Value > num)
				{
					num = current.Value;
				}
			}
		}
		catch (Exception ex)
		{
			throw new Exception("Function GetMaxVolume clsGraphPanel " + ex.Message);
		}
		return num;
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	public void Sort()
	{
		this._graphItems.Sort(new Comparison<VolumeGraphItem>(this.Sort));
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	private int Sort(VolumeGraphItem x, VolumeGraphItem y)
	{
		int result;
		try
		{
			if (x.Price == 0.0)
			{
				if (y.Price == 0.0)
				{
					result = 0;
				}
				else
				{
					if (y.Price > 0.0)
					{
						result = -1;
					}
					else
					{
						result = 1;
					}
				}
			}
			else
			{
				if (y.Price == 0.0 && x.Price > 0.0)
				{
					result = 1;
				}
				else
				{
					if (y.Price == 0.0 && x.Price < 0.0)
					{
						result = -1;
					}
					else
					{
						int num = x.Price.CompareTo(y.Price);
						if (num != 0)
						{
							result = num;
						}
						else
						{
							result = x.Price.CompareTo(y.Price);
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
			throw new Exception("Function Sort clsGraphPanel " + ex.Message);
		}
		return result;
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	public void Update(double dblPrice, long lngAccVol, long lngBuyVol, long lngSellVol)
	{
		try
		{
			bool flag = false;
			int num = 0;
			foreach (VolumeGraphItem current in this._graphItems)
			{
				if (current.Price == dblPrice)
				{
					current.AccVol += lngAccVol;
					if (current.AccVol > this._lngMaxVol)
					{
						this._lngMaxVol = current.AccVol;
					}
					current.BuyVol += lngBuyVol;
					current.SellVol += lngSellVol;
					current.AtoCVol += (double)(lngAccVol - lngBuyVol - lngSellVol);
					this._graphItems[num] = current;
					flag = true;
					break;
				}
				num++;
			}
			if (!flag)
			{
				this.Add(dblPrice, lngAccVol, lngBuyVol, lngSellVol);
				this._graphItems.Sort(new Comparison<VolumeGraphItem>(this.Sort));
			}
		}
		catch (Exception ex)
		{
			throw new Exception("Sub Update clsGraphPanel " + ex.Message);
		}
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	public void OnMouseClick(Graphics gDC, float x, float y, double dblWidth)
	{
		try
		{
			int num = 0;
			if (y > (float)this.sngTop || (double)x > dblWidth)
			{
				if (this._graphItems.Count > 0)
				{
					foreach (VolumeGraphItem current in this._graphItems)
					{
						if (x >= (float)current.Left && x <= (float)current.Right)
						{
							using (Pen pen = new Pen(this.m_ValueColor))
							{
								this.oldRect = new Rectangle(current.Left + (current.Right - current.Left) / 2, this.sngTop, 1, this.sngBottom);
								pen.DashStyle = DashStyle.Dash;
								if (this.intLastIndex == num)
								{
									this.oldRect = new Rectangle(0, 0, 0, 0);
								}
								this.intLastIndex = num;
							}
							break;
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
			throw new Exception("Sub OnMouseClick clsGraphPanel " + ex.Message);
		}
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	public string GetSumBuySell(string Type)
	{
		string text = string.Empty;
		double num = 0.0;
		try
		{
			if ((this.oSymbolType == enumType.eSet || this.oSymbolType == enumType.eTfex) && this.ActiveType.DisplayBuySell == enumDisplayBuySell.Yes)
			{
				foreach (VolumeGraphItem current in this._graphItems)
				{
					num += current.AtoCVol;
					num += (double)current.BuyVol;
					num += (double)current.SellVol;
				}
			}
			if (Type != null)
			{
				if (!(Type == "B"))
				{
					if (!(Type == "S"))
					{
						if (Type == "A")
						{
							for (int i = 0; i <= this._graphItems.Count - 1; i++)
							{
								text += this._graphItems[i].AtoCVol;
							}
						}
					}
					else
					{
						for (int i = 0; i <= this._graphItems.Count - 1; i++)
						{
							text += this._graphItems[i].SellVol;
						}
					}
				}
				else
				{
					for (int i = 0; i <= this._graphItems.Count - 1; i++)
					{
						text += this._graphItems[i].BuyVol;
					}
				}
			}
			if ((this.oSymbolType == enumType.eTfex || this.oSymbolType == enumType.eSet) && this.ActiveType.DisplayBuySell == enumDisplayBuySell.No)
			{
				text = "N/A(N/A%)";
			}
		}
		catch (Exception ex)
		{
			throw new Exception("Sub Function GetSumBS clsGraphPanel " + ex.Message);
		}
		if (FormatUtil.Isnumeric(text))
		{
			if (num != 0.0)
			{
				text = Convert.ToDouble(text).ToString("#,##0") + "(" + Convert.ToDouble(Convert.ToDouble(text) * 100.0 / num).ToString("#,##0.00") + "%)";
			}
			else
			{
				text += "(0.00%)";
			}
		}
		return text;
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	public void Draw(Graphics gDC)
	{
		Pen pen = null;
		Rectangle rect = default(Rectangle);
		Rectangle rect2 = default(Rectangle);
		SolidBrush solidBrush = null;
		Font font = null;
		SizeF sizeF = default(SizeF);
		bool flag = false;
		try
		{
			List<clsVolumeItem> list = new List<clsVolumeItem>();
			this.m_Font = new Font(this.m_FontName, this.m_FontSize, FontStyle.Bold);
			int num = Convert.ToInt32(gDC.MeasureString("I", this.m_Font).Height);
			solidBrush = new SolidBrush(this.m_BgColor);
			rect2 = new Rectangle(0, 0, this.m_Rect.Width, num);
			gDC.FillRectangle(solidBrush, rect2);
			this.m_Font = new Font(this.m_FontName, this.m_FontSize, FontStyle.Regular);
			rect2 = new Rectangle(5, num + 1, this.m_Rect.Width - 10, this.m_Rect.Height - num - 30);
			this.sngTop = num + 5;
			this.sngBottom = this.m_Rect.Height - num - 50;
			pen = new Pen(Color.DimGray);
			gDC.DrawRectangle(pen, rect2);
			int x = rect2.Left;
			int x2 = rect2.Right;
			pen.DashStyle = DashStyle.Dash;
			pen.Color = ColorTranslator.FromHtml("#333333");
			for (int i = 1; i <= 9; i++)
			{
				int num2 = rect2.Top + Convert.ToInt32(rect2.Height / 10) * i;
				int y = num2;
				gDC.DrawLine(pen, x, num2, x2, y);
			}
			int num3 = 0;
			this.sfDraw.Alignment = StringAlignment.Center;
			int num4 = rect2.Width / (this._graphItems.Count * 3);
			num3 = rect2.Width / (this._graphItems.Count + 1) - num4;
			if (num3 > 120)
			{
				num3 = 120;
			}
			long num5 = this.GetMaxVolume() * 100L / 90L;
			float num6 = (float)(num3 * this._graphItems.Count + num4 * (this._graphItems.Count - 1));
			int num7 = Convert.ToInt32(((float)rect2.Width - num6) / 2f);
			int num8 = rect2.Left + num7;
			if (this._graphItems.Count > 0)
			{
				int num9 = 0;
				string text;
				foreach (VolumeGraphItem current in this._graphItems)
				{
					solidBrush.Dispose();
					solidBrush = new SolidBrush(this.m_BgColor);
					rect = new Rectangle(num8, this.sngTop + 1, num3, this.sngBottom - 1);
					gDC.FillRectangle(solidBrush, rect);
					x = rect.Left - 1;
					x2 = rect.Right + 1;
					pen.DashStyle = DashStyle.Dash;
					pen.Color = ColorTranslator.FromHtml("#333333");
					for (int i = 1; i <= 9; i++)
					{
						int num2 = rect2.Top + Convert.ToInt32(rect2.Height / 10) * i;
						int y = num2;
						gDC.DrawLine(pen, x, num2, x2, y);
					}
					pen.Color = Color.DarkGray;
					solidBrush.Dispose();
					solidBrush = new SolidBrush(this.m_VolumeColor);
					double num10 = (double)(current.AccVol * 100L / num5);
					num10 = 100.0 - num10;
					int num11 = rect2.Top + Convert.ToInt32((double)rect2.Height * num10 / 100.0);
					rect = new Rectangle(num8, num11, num3, rect2.Top + rect2.Height - num11);
					gDC.FillRectangle(solidBrush, rect);
					int num12 = num8;
					int num13 = num11 - num + 2;
					list.Add(new clsVolumeItem(num12, num13, (double)current.AccVol));
					solidBrush.Dispose();
					if (this.ActiveType.DisplayBuySell == enumDisplayBuySell.Yes)
					{
						solidBrush = new SolidBrush(this.m_SellColor);
					}
					else
					{
						solidBrush = new SolidBrush(this.m_VolumeColor);
					}
					num10 = (double)((current.SellVol + current.BuyVol) * 100L / num5);
					num10 = 100.0 - num10;
					num11 = rect2.Top + Convert.ToInt32((double)rect2.Height * num10 / 100.0);
					rect = new Rectangle(num8, num11, num3, rect2.Top + rect2.Height - num11);
					gDC.FillRectangle(solidBrush, rect);
					solidBrush.Dispose();
					if (this.ActiveType.DisplayBuySell == enumDisplayBuySell.Yes)
					{
						solidBrush = new SolidBrush(this.m_BuyColor);
					}
					else
					{
						solidBrush = new SolidBrush(this.m_VolumeColor);
					}
					num10 = (double)(current.BuyVol * 100L / num5);
					num10 = 100.0 - num10;
					num11 = rect2.Top + Convert.ToInt32((double)rect2.Height * num10 / 100.0);
					rect = new Rectangle(num8, num11, num3, rect2.Top + rect2.Height - num11);
					gDC.FillRectangle(solidBrush, rect);
					if (this._graphItems.Count < 15)
					{
						font = new Font(this.m_FontName, this.m_FontSize);
					}
					else
					{
						font = new Font(this.m_FontName, this.m_FontSize - 3f);
					}
					solidBrush.Dispose();
					if (Convert.ToDouble(current.Price) == 0.0 || (this.dblPrior == 0.0 && this.IPO == 0.0))
					{
						solidBrush = new SolidBrush(this.m_NoChgColor);
					}
					else
					{
						if (Convert.ToDouble(current.Price) == Convert.ToDouble(this.dblCeiling))
						{
							solidBrush = new SolidBrush(this.m_CeilingColor);
						}
						else
						{
							if (Convert.ToDouble(current.Price) == Convert.ToDouble(this.dblFloor))
							{
								solidBrush = new SolidBrush(this.m_FloorColor);
							}
							else
							{
								if (this.IPO != 0.0)
								{
									solidBrush = new SolidBrush(this.GetCompareColor(this.dblIPO, current.Price));
								}
								else
								{
									solidBrush = new SolidBrush(this.GetCompareColor(this.dblPrior, current.Price));
								}
							}
						}
					}
					text = current.Price.ToString();
					sizeF = gDC.MeasureString(Convert.ToDouble(text).ToString("#,##0.00"), font);
					num13 = rect2.Bottom + 4;
					bool flag2 = false;
					if (Convert.ToDouble(num3 * 2) > Convert.ToDouble(sizeF.Width + 10f))
					{
						flag2 = true;
						gDC.DrawString(Convert.ToDouble(text).ToString("#,##0.00"), font, solidBrush, (float)(num12 + num3 / 2), (float)num13, this.sfDraw);
					}
					else
					{
						if (Convert.ToDouble(num3 * 5) > Convert.ToDouble(sizeF.Width + 15f))
						{
							if (num9 % 2 == 0)
							{
								flag2 = true;
								gDC.DrawString(Convert.ToDouble(text).ToString("#,##0.00"), font, solidBrush, (float)(num12 + num3 / 2), (float)num13, this.sfDraw);
								if (Convert.ToDouble(text) == this.dblLast)
								{
									gDC.DrawString("[Last]", font, solidBrush, (float)(num12 + num3 / 2), (float)num13 + sizeF.Height, this.sfDraw);
								}
							}
							else
							{
								flag2 = true;
								gDC.DrawString(Convert.ToDouble(text).ToString("#,##0.00"), font, solidBrush, (float)(num12 + num3 / 2), (float)(num13 + 12), this.sfDraw);
								if (Convert.ToDouble(text) == this.dblLast)
								{
									gDC.DrawString("[Last]", font, solidBrush, (float)(num12 + num3 / 2), (float)(num13 + 12) + sizeF.Height, this.sfDraw);
								}
								num13 += 12;
							}
						}
						else
						{
							if (Convert.ToDouble(num3 * 10) > Convert.ToDouble(sizeF.Width + 15f))
							{
								if (num9 % 4 == 0)
								{
									flag2 = true;
									gDC.DrawString(Convert.ToDouble(text).ToString("#,##0.00"), font, solidBrush, (float)(num12 + num3 / 2), (float)num13, this.sfDraw);
									if (Convert.ToDouble(text) == this.dblLast)
									{
										gDC.DrawString("[Last]", font, solidBrush, (float)(num12 + num3 / 2), (float)num13 + sizeF.Height, this.sfDraw);
									}
								}
								else
								{
									if (num9 % 2 == 0)
									{
										flag2 = true;
										gDC.DrawString(Convert.ToDouble(text).ToString("#,##0.00"), font, solidBrush, (float)(num12 + num3 / 2), (float)(num13 + 12), this.sfDraw);
										if (Convert.ToDouble(text) == this.dblLast)
										{
											gDC.DrawString("[Last]", font, solidBrush, (float)(num12 + num3 / 2), (float)(num13 + 12) + sizeF.Height, this.sfDraw);
										}
										num13 += 12;
									}
								}
							}
						}
					}
					if (flag2)
					{
						flag = true;
					}
					current.Right = rect.Right;
					num8 += num4 + num3;
					num9++;
				}
				SizeF sizeF2 = default(SizeF);
				text = this.strSymbolList + " , ";
				solidBrush.Dispose();
				pen.DashStyle = DashStyle.Solid;
				if (flag)
				{
					string text2 = "0";
					foreach (clsVolumeItem current2 in list)
					{
						if (this.oSymbolType == enumType.eSet)
						{
							text2 = current2.Volume.ToString();
						}
						else
						{
							if (this.oSymbolType == enumType.eTfex)
							{
								text2 = current2.Volume.ToString();
							}
						}
						text2 = Convert.ToDouble(text2).ToString("#,##0");
						sizeF2 = gDC.MeasureString(text2, font);
						int num14 = current2.x + num3 / 2;
						Rectangle rect3 = new Rectangle(Convert.ToInt32((float)num14 - sizeF2.Width / 2f), current2.y - 6, Convert.ToInt32(sizeF2.Width), Convert.ToInt32(sizeF2.Height));
						gDC.DrawRectangle(pen, rect3);
						gDC.DrawString(text2, font, Brushes.White, (float)num14, (float)(current2.y - 5), this.sfDraw);
						gDC.DrawLine(pen, (float)num14, (float)current2.y + sizeF2.Height - 4f, (float)num14, (float)current2.y + sizeF2.Height + 5f);
					}
				}
				list.Clear();
				list = null;
			}
			else
			{
				string text;
				if (!string.IsNullOrEmpty(this.strSymbolList))
				{
					if (this.bGetWs && !this.bException)
					{
						text = this.strSymbolList + " - No Information!";
					}
					else
					{
						if (this.bGetWs && this.bException)
						{
							text = this.strSymbolList + " - Load fail retry agian...";
						}
						else
						{
							text = this.strSymbolList + " - Loading...";
						}
					}
				}
				else
				{
					text = "";
				}
				int num12 = (this.m_Rect.Width - rect2.Width) / 2;
				solidBrush = new SolidBrush(this.m_NoChgColor);
				if (font != null)
				{
					font.Dispose();
				}
				font = new Font(this.m_FontName, this.m_FontSize, FontStyle.Bold);
				int num13 = rect2.Top;
				gDC.DrawString(text, font, solidBrush, 1f, 1f);
			}
		}
		catch (Exception ex)
		{
			throw new Exception("Sub Draw clsGraphPanel " + ex.Message);
		}
		finally
		{
			if (this.m_Font != null)
			{
				this.m_Font.Dispose();
				this.m_Font = null;
			}
			if (solidBrush != null)
			{
				solidBrush.Dispose();
				solidBrush = null;
			}
		}
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	public void DrawByStock(Graphics gDC)
	{
		Pen pen = null;
		Rectangle rect = default(Rectangle);
		Rectangle rect2 = default(Rectangle);
		SolidBrush solidBrush = null;
		Font font = null;
		SizeF sizeF = default(SizeF);
		bool flag = false;
		try
		{
			List<clsVolumeItem> list = new List<clsVolumeItem>();
			this.m_Font = new Font(this.m_FontName, this.m_FontSize, FontStyle.Bold);
			int num = Convert.ToInt32(gDC.MeasureString("I", this.m_Font).Height);
			solidBrush = new SolidBrush(this.m_BgColor);
			rect2 = new Rectangle(0, 0, this.m_Rect.Width, num);
			gDC.FillRectangle(solidBrush, rect2);
			this.m_Font = new Font(this.m_FontName, this.m_FontSize, FontStyle.Regular);
			rect2 = new Rectangle(5, num + 1, this.m_Rect.Width - 10, this.m_Rect.Height - num - 30);
			this.sngTop = num + 5;
			this.sngBottom = this.m_Rect.Height - num - 50;
			pen = new Pen(Color.DimGray);
			gDC.DrawRectangle(pen, rect2);
			int x = rect2.Left;
			int x2 = rect2.Right;
			pen.DashStyle = DashStyle.Dash;
			pen.Color = ColorTranslator.FromHtml("#333333");
			for (int i = 1; i <= 9; i++)
			{
				int num2 = rect2.Top + Convert.ToInt32(rect2.Height / 10) * i;
				int y = num2;
				gDC.DrawLine(pen, x, num2, x2, y);
			}
			int num3 = 0;
			this.sfDraw.Alignment = StringAlignment.Center;
			int num4 = rect2.Width / (this._itemsByStock.Count * 3);
			num3 = rect2.Width / (this._itemsByStock.Count + 1) - num4;
			if (num3 > 120)
			{
				num3 = 120;
			}
			double num5 = this.GetMaxValue() * 100.0 / 90.0;
			float num6 = (float)(num3 * this._itemsByStock.Count + num4 * (this._itemsByStock.Count - 1));
			int num7 = Convert.ToInt32(((float)rect2.Width - num6) / 2f);
			int num8 = rect2.Left + num7;
			if (this._itemsByStock.Count > 0)
			{
				int num9 = 0;
				string text;
				foreach (ItemByStock current in this._itemsByStock)
				{
					solidBrush.Dispose();
					solidBrush = new SolidBrush(this.m_BgColor);
					rect = new Rectangle(num8, this.sngTop + 1, num3, this.sngBottom - 1);
					gDC.FillRectangle(solidBrush, rect);
					x = rect.Left - 1;
					x2 = rect.Right + 1;
					pen.DashStyle = DashStyle.Dash;
					pen.Color = ColorTranslator.FromHtml("#333333");
					for (int i = 1; i <= 9; i++)
					{
						int num2 = rect2.Top + Convert.ToInt32(rect2.Height / 10) * i;
						int y = num2;
						gDC.DrawLine(pen, x, num2, x2, y);
					}
					pen.Color = Color.DarkGray;
					solidBrush.Dispose();
					if (current.Profit > 0.0)
					{
						solidBrush = new SolidBrush(this.m_BuyColor);
					}
					else
					{
						if (current.Profit < 0.0)
						{
							solidBrush = new SolidBrush(this.m_SellColor);
						}
						else
						{
							solidBrush = new SolidBrush(this.m_VolumeColor);
						}
					}
					double num10 = current.Value * 100.0 / num5;
					num10 = 100.0 - num10;
					int num11 = rect2.Top + Convert.ToInt32((double)rect2.Height * num10 / 100.0);
					rect = new Rectangle(num8, num11, num3, rect2.Top + rect2.Height - num11);
					gDC.FillRectangle(solidBrush, rect);
					int num12 = num8;
					int num13 = num11 - num + 2;
					list.Add(new clsVolumeItem(num12, num13, current.Value));
					if (this._itemsByStock.Count < 15)
					{
						font = new Font(this.m_FontName, this.m_FontSize);
					}
					else
					{
						font = new Font(this.m_FontName, this.m_FontSize - 2f);
					}
					solidBrush.Dispose();
					solidBrush = new SolidBrush(this.m_VolumeColor);
					text = current.Stock;
					sizeF = gDC.MeasureString(text, font);
					num13 = rect2.Bottom + 4;
					bool flag2 = true;
					gDC.DrawString(text, font, solidBrush, (float)(num12 + num3 / 2), (float)num13, this.sfDraw);
					if (flag2)
					{
						flag = true;
					}
					num8 += num4 + num3;
					num9++;
				}
				SizeF sizeF2 = default(SizeF);
				text = this.strSymbolList + " , ";
				solidBrush.Dispose();
				pen.DashStyle = DashStyle.Solid;
				if (flag)
				{
					foreach (clsVolumeItem current2 in list)
					{
						string text2 = Convert.ToDouble(current2.Volume).ToString("#,##0.00") + "%";
						sizeF2 = gDC.MeasureString(text2, font);
						int num14 = current2.x + num3 / 2;
						Rectangle rect3 = new Rectangle(Convert.ToInt32((float)num14 - sizeF2.Width / 2f), current2.y - 6, Convert.ToInt32(sizeF2.Width), Convert.ToInt32(sizeF2.Height));
						gDC.DrawRectangle(pen, rect3);
						gDC.DrawString(text2, font, Brushes.White, (float)num14, (float)(current2.y - 5), this.sfDraw);
						gDC.DrawLine(pen, (float)num14, (float)current2.y + sizeF2.Height - 4f, (float)num14, (float)current2.y + sizeF2.Height + 5f);
					}
				}
				list.Clear();
				list = null;
			}
			else
			{
				string text;
				if (!string.IsNullOrEmpty(this.strSymbolList))
				{
					if (this.bGetWs && !this.bException)
					{
						text = this.strSymbolList + " - No Information!";
					}
					else
					{
						if (this.bGetWs && this.bException)
						{
							text = this.strSymbolList + " - Load fail retry agian...";
						}
						else
						{
							text = this.strSymbolList + " - Loading...";
						}
					}
				}
				else
				{
					text = "";
				}
				int num12 = (this.m_Rect.Width - rect2.Width) / 2;
				solidBrush = new SolidBrush(this.m_NoChgColor);
				if (font != null)
				{
					font.Dispose();
				}
				font = new Font(this.m_FontName, this.m_FontSize, FontStyle.Bold);
				int num13 = rect2.Top;
				gDC.DrawString(text, font, solidBrush, 1f, 1f);
			}
		}
		catch (Exception ex)
		{
			throw new Exception("Sub Draw clsGraphPanel " + ex.Message);
		}
		finally
		{
			if (this.m_Font != null)
			{
				this.m_Font.Dispose();
				this.m_Font = null;
			}
			if (solidBrush != null)
			{
				solidBrush.Dispose();
				solidBrush = null;
			}
		}
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	public Color GetCompareColor(double CompareValue, double InputValue)
	{
		Color result;
		if (InputValue > CompareValue)
		{
			result = this.m_UpColor;
		}
		else
		{
			if (InputValue < CompareValue)
			{
				result = this.m_DownColor;
			}
			else
			{
				result = this.m_NoChgColor;
			}
		}
		return result;
	}
}
