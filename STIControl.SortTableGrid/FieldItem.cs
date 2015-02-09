using System;
using System.Drawing;
using System.Runtime.CompilerServices;
namespace STIControl.SortTableGrid
{
	public class FieldItem
	{
		private FontStyle fontStyle;
		private FormatType columnFormat;
		private object text = string.Empty;
		private int isBlink;
		private DateTime lastBlink = DateTime.MinValue;
		private bool changed;
		private Brush fontBrush = Brushes.LightGray;
		private Color fontColor = Color.White;
		private Color backColor = Color.Transparent;
		private Brush backBrush = Brushes.Transparent;
		private string tag = string.Empty;
		private string tempText = string.Empty;
		public FontStyle FontStyle
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.fontStyle;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				if (this.fontStyle != value)
				{
					this.changed = true;
				}
				this.fontStyle = value;
			}
		}
		public FormatType ColumnFormat
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.columnFormat;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.columnFormat = value;
			}
		}
		public object Text
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.text;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				string text = value.ToString();
				switch (this.columnFormat)
				{
				case FormatType.Price:
					text = this.PriceFormat(value, false, "");
					break;
				case FormatType.Volume:
					text = this.VolumeFormat(value, true);
					break;
				case FormatType.ChangePrice:
					text = this.PriceFormat(value, true, "");
					break;
				case FormatType.PercentChange:
					text = this.PriceFormat(value, true, "%");
					break;
				case FormatType.BidOfferVolume:
				case FormatType.BidOfferVolWhite:
					if (value.ToString().Length > 8)
					{
						text = this.VolumeFormat(value.ToString().Substring(0, value.ToString().Length - 3), true) + "K";
					}
					else
					{
						text = this.VolumeFormat(value, true);
					}
					break;
				}
				if (this.text.ToString() != text)
				{
					this.Changed = true;
					if (this.text.ToString() != string.Empty && text != string.Empty && this.isBlink == 0)
					{
						if (this.columnFormat == FormatType.BidOfferVolume)
						{
							if (string.Compare(text, this.text.ToString()) == 1)
							{
								this.IsBlink = 2;
							}
							else
							{
								this.IsBlink = 1;
							}
						}
						else
						{
							this.IsBlink = 1;
						}
					}
				}
				this.text = text;
			}
		}
		public int IsBlink
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isBlink;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				if (value != 0)
				{
					this.lastBlink = DateTime.Now;
					this.Changed = true;
				}
				this.isBlink = value;
			}
		}
		public DateTime LastBlink
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.lastBlink;
			}
		}
		public bool Changed
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.changed;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.changed = value;
			}
		}
		public Brush FontBrush
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.fontBrush;
			}
		}
		public Color FontColor
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.fontColor;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				if (this.fontColor != value)
				{
					this.changed = true;
					this.fontColor = value;
					this.fontBrush = new SolidBrush(value);
				}
			}
		}
		public Color BackColor
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.backColor;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				if (this.backColor != value)
				{
					this.changed = true;
					this.backColor = value;
					this.backBrush = new SolidBrush(value);
				}
			}
		}
		public Brush BackBrush
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.backBrush;
			}
		}
		public string Tag
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.tag;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.tag = value;
			}
		}
		public string TempText
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.tempText;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.tempText = value;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public FieldItem(FormatType columnFormatType)
		{
			this.FontColor = Color.White;
			this.columnFormat = columnFormatType;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetBidOfferVolumeText(string volume, string currPrice)
		{
			try
			{
				decimal currPrice2;
				decimal.TryParse(currPrice, out currPrice2);
				this.SetBidOfferVolumeText(volume, currPrice2);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetBidOfferVolumeText(string volume, decimal currPrice)
		{
			try
			{
				decimal d;
				decimal.TryParse(this.tempText, out d);
				string strA = this.VolumeFormat(volume, true);
				if (volume.Length > 8)
				{
					strA = this.VolumeFormat(volume.Substring(0, volume.Length - 3), true) + "K";
				}
				if (!this.text.ToString().Equals(string.Empty) && d == currPrice && this.isBlink == 0)
				{
					if (this.columnFormat == FormatType.BidOfferVolume)
					{
						if (string.Compare(strA, this.text.ToString()) == 1)
						{
							this.IsBlink = 2;
						}
						else
						{
							if (string.Compare(strA, this.text.ToString()) == -1)
							{
								this.IsBlink = 1;
							}
						}
					}
					else
					{
						this.IsBlink = 1;
					}
				}
				this.Changed = true;
				this.text = strA;
				this.tempText = this.PriceFormat(currPrice, false, "");
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string VolumeFormat(object volume, bool comma)
		{
			return FormatUtil.VolumeFormat(volume, comma);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string PriceFormat(object price, bool sign, string unit = "")
		{
			return FormatUtil.PriceFormat(price, sign, unit);
		}
	}
}
