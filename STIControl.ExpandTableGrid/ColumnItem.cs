using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
namespace STIControl.ExpandTableGrid
{
	public class ColumnItem
	{
		private FormatType valueFormat;
		private string name = string.Empty;
		private StringAlignment alignment;
		private Color backColor = Color.FromArgb(64, 64, 64);
		private Color fontColor = Color.LightGray;
		private string text = "None";
		private int width = 10;
		private bool visible = true;
		private FontStyle myStyle;
		private bool isExpand;
		public float dX;
		public float dW;
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
		[Browsable(true), Category("Design")]
		public string Name
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.name;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.name = value;
			}
		}
		public StringAlignment Alignment
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.alignment;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.alignment = value;
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
				this.backColor = value;
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
				this.fontColor = value;
			}
		}
		public virtual string Text
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.text;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.text = value;
			}
		}
		public int Width
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.width;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.width = value;
			}
		}
		public bool Visible
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.visible;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.visible = value;
			}
		}
		public FontStyle MyStyle
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.myStyle;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.myStyle = value;
			}
		}
		public bool IsExpand
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isExpand;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.isExpand = value;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ColumnItem()
		{
			this.Name = "SpeedTableGrid" + ++ExpandGrid.ColumnID;
		}
	}
}
