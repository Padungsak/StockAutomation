using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace STIControl
{
	public class HScrollbar : UserControl
	{
		private int moLargeChange;
		private int moMaximum;
		protected Color moChannelColor = Color.LightGray;
		private int moValue;
		private int nClickPoint;
		private int minScrollSize = 15;
		private int _moThumbTop;
		private bool moThumbDown;
		private bool moThumbDragging;
		private float _lastX;
		private IContainer components;
        public event EventHandler _ValueChanged;
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
					int width = base.Width;
					int thumbWidth = this.GetThumbWidth();
					int num = width - thumbWidth;
					int num2 = this.Maximum - this.LargeChange;
					float num3 = 0f;
					if (num2 != 0)
					{
						num3 = (float)this.moValue / (float)num2;
					}
					float num4 = num3 * (float)num;
					this._moThumbTop = (int)num4;
					if (this._ValueChanged != null)
					{
						this._ValueChanged(this, new EventArgs());
					}
					base.Invalidate();
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public HScrollbar()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.ResizeRedraw, true);
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			base.SetStyle(ControlStyles.DoubleBuffer, true);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private int GetThumbWidth()
		{
			int width = base.Width;
			float num = (float)this.LargeChange / (float)this.Maximum * (float)width;
			int num2 = (int)num;
			if (num2 > width)
			{
				num2 = width;
			}
			if (this.Maximum > 0)
			{
				if (num2 < this.minScrollSize)
				{
					num2 = this.minScrollSize;
				}
			}
			else
			{
				num2 = 0;
			}
			return num2;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
			e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
			e.Graphics.DrawLine(new Pen(Color.FromArgb(45, 45, 45)), 0, 0, base.Width, 0);
			int thumbWidth = this.GetThumbWidth();
			int num = this._moThumbTop;
			if (num > base.Width - thumbWidth)
			{
				num = base.Width - thumbWidth;
			}
			if (this.Maximum > this.LargeChange)
			{
				GraphicsPath graphicsPath = new GraphicsPath();
				graphicsPath.AddArc(num, 1, 8, base.Height - 2, 90f, 180f);
				graphicsPath.AddRectangle(new Rectangle(num + 4, 1, thumbWidth - 8, base.Height - 2));
				graphicsPath.AddArc(num + thumbWidth - 8, 1, 8, base.Height - 2, 270f, 180f);
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new RectangleF((float)(num + 1), 1f, (float)(thumbWidth - 2), (float)(base.Height - 1)), this.moChannelColor, Color.FromArgb(80, this.moChannelColor), LinearGradientMode.Vertical))
				{
					e.Graphics.FillPath(linearGradientBrush, graphicsPath);
				}
			}
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
			int width = base.Width;
			int thumbWidth = this.GetThumbWidth();
			int moThumbTop = this._moThumbTop;
			Rectangle rectangle = new Rectangle(moThumbTop, 1, thumbWidth, base.Height - 2);
			if (rectangle.Contains(pt))
			{
				this.nClickPoint = pt.X - moThumbTop;
				this.moThumbDown = true;
				this._lastX = (float)pt.X;
			}
			Rectangle rectangle2 = new Rectangle(1, 1, moThumbTop, base.Height - 2);
			if (rectangle2.Contains(pt))
			{
				int num2 = width - thumbWidth;
				if (num2 > 0 && this.Value > 0)
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
			Rectangle rectangle3 = new Rectangle(moThumbTop + thumbWidth, 1, width - (moThumbTop + thumbWidth), base.Height - 2);
			if (rectangle3.Contains(pt))
			{
				int num3 = width - thumbWidth;
				if (num3 > 0)
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
		private void MoveThumb(int x)
		{
			int maximum = this.Maximum;
			int width = base.Width;
			int thumbWidth = this.GetThumbWidth();
			int num = this.nClickPoint;
			int num2 = width - thumbWidth;
			if (this.moThumbDown && maximum > 0 && num2 > 0)
			{
				int num3 = x - num;
				if (num3 < 0)
				{
					this._moThumbTop = 0;
				}
				else
				{
					if (num3 > num2)
					{
						this._moThumbTop = num2;
					}
					else
					{
						this._moThumbTop = x - num;
					}
				}
				float num4 = (float)this._moThumbTop / (float)num2;
				float num5 = num4 * (float)(this.Maximum - this.LargeChange);
				this.moValue = (int)num5;
				Application.DoEvents();
				base.Invalidate();
				if (this._ValueChanged != null)
				{
					this._ValueChanged(this, new EventArgs());
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void CustomScrollbar_MouseMove(object sender, MouseEventArgs e)
		{
			if (this.moThumbDown)
			{
				this.moThumbDragging = true;
			}
			if (this.moThumbDragging && Math.Abs(this._lastX - (float)e.X) > 5f)
			{
				this.MoveThumb(e.X);
				this._lastX = (float)e.X;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Name = "HScrollbar";
			base.MouseMove += new MouseEventHandler(this.CustomScrollbar_MouseMove);
			base.MouseDown += new MouseEventHandler(this.CustomScrollbar_MouseDown);
			base.MouseUp += new MouseEventHandler(this.CustomScrollbar_MouseUp);
			base.ResumeLayout(false);
		}
	}
}
