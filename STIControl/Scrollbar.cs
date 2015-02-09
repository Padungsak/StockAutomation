using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace STIControl
{
	[Designer(typeof(ScrollbarControlDesigner))]
	public class Scrollbar : UserControl
	{
		protected Color moChannelColor = Color.LightGray;
		protected int moLargeChange = 10;
		protected int moMinimum;
		protected int moMaximum = 100;
		protected int moValue;
		private int nClickPoint;
		protected int minScrollSize = 15;
		protected int _moThumbTop;
		private bool moThumbDown;
		private bool moThumbDragging;
		private float _headerHeight;
        public EventHandler _ValueChanged;
		public event EventHandler ValueChanged
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._ValueChanged += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._ValueChanged -= value;
			}
		}
		[Browsable(true), Category("Behavior"), DefaultValue(false), Description("LargeChange"), EditorBrowsable(EditorBrowsableState.Always)]
		public int LargeChange
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.moLargeChange;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				if (this.moLargeChange != value)
				{
					this.moLargeChange = value;
					base.Invalidate();
				}
			}
		}
		[Browsable(true), Category("Behavior"), DefaultValue(false), Description("Maximum"), EditorBrowsable(EditorBrowsableState.Always)]
		public int Maximum
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.moMaximum;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				if (this.moMaximum != value)
				{
					this.moMaximum = value;
					base.Invalidate();
				}
			}
		}
		[Browsable(true), Category("Behavior"), DefaultValue(false), Description("Value"), EditorBrowsable(EditorBrowsableState.Always)]
		public int Value
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.moValue;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				if (this.moValue != value)
				{
					this.moValue = value;
					int num = base.Height - (int)this._headerHeight;
					int thumbHeight = this.GetThumbHeight();
					int num2 = num - thumbHeight;
					int num3 = this.Maximum - this.LargeChange;
					float num4 = 0f;
					if (num3 != 0)
					{
						num4 = (float)this.moValue / (float)num3;
					}
					this._moThumbTop = (int)(num4 * (float)num2);
					if (this._ValueChanged != null)
					{
						this._ValueChanged(this, new EventArgs());
					}
					base.Invalidate();
				}
			}
		}
		[Browsable(true), Category("Skin"), DefaultValue(false), Description("Channel Color"), EditorBrowsable(EditorBrowsableState.Always)]
		public Color ChannelColor
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.moChannelColor;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.moChannelColor = value;
				base.Invalidate();
			}
		}
		public float HeaderHeight
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this._headerHeight;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this._headerHeight = 0f;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private int GetThumbHeight()
		{
			int num = base.Height - (int)this._headerHeight;
			float num2 = (float)this.LargeChange / (float)this.Maximum * (float)num;
			int num3 = (int)num2;
			if (num3 > num)
			{
				num3 = num;
			}
			if (this.Maximum > 0)
			{
				if (num3 < this.minScrollSize)
				{
					num3 = this.minScrollSize;
				}
			}
			else
			{
				num3 = 0;
			}
			return num3;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Scrollbar()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.ResizeRedraw, true);
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			base.SetStyle(ControlStyles.DoubleBuffer, true);
			this.moChannelColor = Color.FromArgb(100, 100, 100);
			base.MinimumSize = new Size(base.Width, (int)this._headerHeight + this.GetThumbHeight());
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
			e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
			if (this._headerHeight > 0f)
			{
				e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(180, this.moChannelColor)), 2f, 1f, (float)(base.Width - 3), this._headerHeight - 1f);
			}
			e.Graphics.DrawLine(new Pen(Color.FromArgb(45, 45, 45)), 0, 0, 0, base.Height);
			int thumbHeight = this.GetThumbHeight();
			int num = this._moThumbTop;
			num += (int)this._headerHeight;
			if (num > base.Height - thumbHeight)
			{
				num = base.Height - thumbHeight;
			}
			if (this.Maximum > this.LargeChange)
			{
				GraphicsPath graphicsPath = new GraphicsPath();
				graphicsPath.AddArc(1, num + 1, base.Width - 2, 8, 180f, 180f);
				graphicsPath.AddRectangle(new Rectangle(1, num + 1 + 4, base.Width - 2, thumbHeight - 2 - 8));
				graphicsPath.AddArc(1, num + thumbHeight - 1 - 8, base.Width - 2, 8, -180f, -180f);
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new RectangleF(1f, (float)num, (float)(base.Width - 1), (float)thumbHeight), this.moChannelColor, Color.FromArgb(80, this.moChannelColor), LinearGradientMode.Horizontal))
				{
					e.Graphics.FillPath(linearGradientBrush, graphicsPath);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.Name = "CustomScrollbar";
			base.MouseDown += new MouseEventHandler(this.CustomScrollbar_MouseDown);
			base.MouseMove += new MouseEventHandler(this.CustomScrollbar_MouseMove);
			base.MouseUp += new MouseEventHandler(this.CustomScrollbar_MouseUp);
			base.ResumeLayout(false);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void CustomScrollbar_MouseDown(object sender, MouseEventArgs e)
		{
			int num = this.Maximum - this.LargeChange;
			if (num < 0)
			{
				return;
			}
			Point pt = base.PointToClient(Cursor.Position);
			int num2 = base.Height - (int)this._headerHeight;
			int thumbHeight = this.GetThumbHeight();
			int num3 = this._moThumbTop;
			num3 += (int)this._headerHeight;
			Rectangle rectangle = new Rectangle(1, num3, base.Width, thumbHeight);
			if (rectangle.Contains(pt))
			{
				this.nClickPoint = pt.Y - num3;
				this.moThumbDown = true;
			}
			Rectangle rectangle2 = new Rectangle(1, (int)this._headerHeight, base.Width - 2, num3 - (int)this._headerHeight);
			if (rectangle2.Contains(pt))
			{
				int num4 = num2 - thumbHeight;
				if (num4 > 0 && this.Value > 0)
				{
					if (this.Value - this.LargeChange < 0)
					{
						this.Value = 0;
					}
					else
					{
						this.Value -= this.LargeChange;
					}
				}
			}
			Rectangle rectangle3 = new Rectangle(1, num3 + thumbHeight, base.Width, num2 - (int)this._headerHeight - (num3 + thumbHeight));
			if (rectangle3.Contains(pt))
			{
				int num5 = num2 - thumbHeight;
				if (num5 > 0)
				{
					if (this.Value + this.LargeChange + this.LargeChange <= this.Maximum)
					{
						this.Value += this.LargeChange;
						return;
					}
					this.Value = this.Maximum - this.LargeChange;
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void CustomScrollbar_MouseUp(object sender, MouseEventArgs e)
		{
			this.moThumbDown = false;
			this.moThumbDragging = false;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void MoveThumb(int y)
		{
			int maximum = this.Maximum;
			int num = base.Height - (int)this._headerHeight;
			int thumbHeight = this.GetThumbHeight();
			int num2 = this.nClickPoint;
			int num3 = num - thumbHeight;
			if (this.moThumbDown && maximum > 0 && num3 > 0)
			{
				int num4 = y - ((int)this._headerHeight + num2);
				if (num4 < 0)
				{
					this._moThumbTop = 0;
				}
				else
				{
					if (num4 > num3)
					{
						this._moThumbTop = num3;
					}
					else
					{
						this._moThumbTop = y - ((int)this._headerHeight + num2);
					}
				}
				float num5 = (float)this._moThumbTop / (float)num3;
				float num6 = num5 * (float)(this.Maximum - this.LargeChange);
				this.moValue = (int)num6;
				Application.DoEvents();
				base.Invalidate();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void CustomScrollbar_MouseMove(object sender, MouseEventArgs e)
		{
			if (this.moThumbDown)
			{
				this.moThumbDragging = true;
			}
			if (this.moThumbDragging)
			{
				this.MoveThumb(e.Y);
				if (this._ValueChanged != null)
				{
					this._ValueChanged(this, new EventArgs());
				}
			}
		}
	}
}
