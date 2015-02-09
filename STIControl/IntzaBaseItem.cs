using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
namespace STIControl
{
	public class IntzaBaseItem
	{
		private StringAlignment alignment;
		private Brush backBrush = Brushes.Transparent;
		private Color backColor = Color.Transparent;
		private Brush fontBrush = Brushes.White;
		private Color fontColor = Color.White;
		private string name = string.Empty;
		private string text = "IntzaItem";
		private bool changed;
		private int posX;
		private int posY;
		private int width = 10;
		private int height = 10;
		private bool visible = true;
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Rectangle Bounds
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return new Rectangle(this.posX, this.posY, this.width, this.height);
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.SetBounds(value.X, value.Y, value.Width, value.Height);
			}
		}
		[Browsable(true), Category("IntzaItem")]
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
		[Browsable(false)]
		public Brush BackBrush
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.backBrush;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.backBrush = value;
			}
		}
		[Browsable(true), Category("IntzaItem")]
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
		[Browsable(false)]
		public Brush FontBrush
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.fontBrush;
			}
		}
		[Browsable(true), Category("IntzaItem")]
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
		[Browsable(true), Category("IntzaGrid Design")]
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
		[Browsable(true), Category("IntzaItem")]
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
		[Category("IntzaItem Location")]
		public int X
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.posX;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.posX = value;
			}
		}
		[Category("IntzaItem Location")]
		public int Y
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.posY;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.posY = value;
			}
		}
		[Browsable(true), Category("IntzaItem Size")]
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
		[Browsable(true), Category("IntzaItem Size")]
		public int Height
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.height;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.height = value;
			}
		}
		[Browsable(true), Category("IntzaItem")]
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
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetBounds(Rectangle position)
		{
			this.SetBounds(position.X, position.Y, position.Width, position.Height);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetBounds(int x, int y, int width, int height)
		{
			this.posX = x;
			this.posY = y;
			this.width = width;
			this.height = height;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public IntzaBaseItem()
		{
		}
	}
}
