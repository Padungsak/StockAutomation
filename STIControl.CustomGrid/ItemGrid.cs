using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
namespace STIControl.CustomGrid
{
	public class ItemGrid : IntzaBaseItem
	{
		private ItemType fieldType;
		private FontStyle fontStyle;
		private int isBlink;
		private DateTime lastBlink = DateTime.MinValue;
		private FormatType valueFormat;
		private float adjustFontSize;
		[Browsable(true), Category("IntzaItem")]
		public ItemType FieldType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.fieldType;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.fieldType = value;
			}
		}
		[Browsable(true), Category("IntzaItem")]
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
				this.fontStyle = value;
			}
		}
		[Browsable(true), Category("IntzaItem")]
		public override string Text
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return base.Text;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				string text = value;
				switch (this.valueFormat)
				{
				case FormatType.Price:
					text = this.PriceFormat(value, false, "");
					break;
				case FormatType.Volume:
				case FormatType.BidOfferVolume:
				case FormatType.BidOfferVolWhite:
					text = this.VolumeFormat(value, true);
					break;
				case FormatType.ChangePrice:
					text = this.PriceFormat(value, true, "");
					break;
				case FormatType.PercentChange:
					text = this.PriceFormat(value, true, "%");
					break;
				}
				if (base.Text.ToString() != text)
				{
					base.Changed = true;
					if (this.fieldType == ItemType.Text && base.Text.ToString() != string.Empty && text != string.Empty && this.isBlink == 0)
					{
						if (this.valueFormat == FormatType.BidOfferVolume)
						{
							if (string.Compare(text, this.Text.ToString()) == 1)
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
				base.Text = text;
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
					base.Changed = true;
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
		[Category("IntzaItem")]
		public FormatType ValueFormat
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.valueFormat;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.valueFormat = value;
			}
		}
		[Category("IntzaItem")]
		public float AdjustFontSize
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.adjustFontSize;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.adjustFontSize = value;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ItemGrid()
		{
			base.Height = 1;
			this.Text = base.Name;
			base.FontColor = Color.White;
			base.BackColor = Color.DimGray;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string PriceFormat(object Price, bool Sign, string Unit = "")
		{
			return FormatUtil.PriceFormat(Price, Sign, Unit);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string VolumeFormat(object Volume, bool Comma)
		{
			return FormatUtil.VolumeFormat(Volume, Comma);
		}
	}
}
