using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace ITSNet.Wins.Controls
{
	internal class LoadingCircleControl : Control
	{
		private const int NUMBER_OF_DEGREES_CIRCLE = 360;
		private const int NUMBER_OF_DEGREES_HALF_CIRCLE = 180;
		private const int PERCENTAGE_OF_DARKEN = 10;
		private const int DEFAULT_INNER_CIRCLE_RADIUS = 8;
		private const int DEFAULT_OUTER_CIRCLE_RADIUS = 10;
		private const int DEFAULT_NUMBER_OF_SPOKE = 10;
		private const int DEFAULT_SPOKE_THICKNESS = 4;
		private Color DEFAULT_COLOR = Color.DarkGray;
		private Timer aTimer;
		private bool aTimerActive;
		private int aNumberOfSpoke;
		private int aSpokeThickness;
		private int aProgressValue;
		private int aOuterCircleRadius;
		private int aInnerCircleRadius;
		private PointF aCenterPoint;
		private Color aColor;
		private Color[] aColors;
		private double[] aAngles;
		private IContainer components = null;
		[Category("LoadingCircle"), Description("Sets the color of spoke."), TypeConverter("System.Drawing.ColorConverter")]
		public Color Color
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.aColor;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.aColor = value;
				this.GenerateColorsPallet();
				base.Invalidate();
			}
		}
		[Category("LoadingCircle"), Description("Gets or sets the radius of outer circle.")]
		public int OuterCircleRadius
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (this.aOuterCircleRadius == 0)
				{
					this.aOuterCircleRadius = 10;
				}
				return this.aOuterCircleRadius;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.aOuterCircleRadius = value;
				base.Invalidate();
			}
		}
		[Category("LoadingCircle"), Description("Gets or sets the radius of inner circle.")]
		public int InnerCircleRadius
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (this.aInnerCircleRadius == 0)
				{
					this.aInnerCircleRadius = 8;
				}
				return this.aInnerCircleRadius;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.aInnerCircleRadius = value;
				base.Invalidate();
			}
		}
		[Category("LoadingCircle"), Description("Gets or sets the number of spoke.")]
		public int NumberSpoke
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (this.aNumberOfSpoke == 0)
				{
					this.aNumberOfSpoke = 10;
				}
				return this.aNumberOfSpoke;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				if (this.aNumberOfSpoke != value && this.aNumberOfSpoke > 0)
				{
					this.aNumberOfSpoke = value;
					this.GenerateColorsPallet();
					this.GetSpokesAngles();
					base.Invalidate();
				}
			}
		}
		[Category("LoadingCircle"), Description("Gets or sets the number of spoke.")]
		public bool Active
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.aTimerActive;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.aTimerActive = value;
				this.ActiveTimer();
			}
		}
		[Category("LoadingCircle"), Description("Gets or sets the thickness of a spoke.")]
		public int SpokeThickness
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (this.aSpokeThickness <= 0)
				{
					this.aSpokeThickness = 4;
				}
				return this.aSpokeThickness;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.aSpokeThickness = value;
				base.Invalidate();
			}
		}
		[Category("LoadingCircle"), Description("Gets or sets the rotation speed. Higher the slower.")]
		public int RotationSpeed
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.aTimer.Interval;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				if (value > 0)
				{
					this.aTimer.Interval = value;
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public LoadingCircleControl()
		{
			base.SetStyle(ControlStyles.UserPaint, true);
			base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			base.SetStyle(ControlStyles.ResizeRedraw, true);
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			this.aColor = this.DEFAULT_COLOR;
			this.GenerateColorsPallet();
			this.GetSpokesAngles();
			this.GetControlCenterPoint();
			this.aTimer = new Timer();
			this.aTimer.Tick += new EventHandler(this.aTimer_Tick);
			this.ActiveTimer();
			base.Resize += new EventHandler(this.LoadingCircle_Resize);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void LoadingCircle_Resize(object sender, EventArgs e)
		{
			this.GetControlCenterPoint();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void aTimer_Tick(object sender, EventArgs e)
		{
			this.aProgressValue = ++this.aProgressValue % this.aNumberOfSpoke;
			base.Invalidate();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnPaint(PaintEventArgs e)
		{
			if (this.aNumberOfSpoke > 0)
			{
				e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
				int num = this.aProgressValue;
				for (int i = 0; i < this.aNumberOfSpoke; i++)
				{
					num %= this.aNumberOfSpoke;
					this.DrawLine(e.Graphics, this.GetCoordinate(this.aCenterPoint, this.aInnerCircleRadius, this.aAngles[num]), this.GetCoordinate(this.aCenterPoint, this.aOuterCircleRadius, this.aAngles[num]), this.aColors[i], this.aSpokeThickness);
					num++;
				}
			}
			base.OnPaint(e);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private Color Darken(Color _objColor, int _intPercent)
		{
			int val = (int)_objColor.R - _intPercent * (int)(_objColor.R / 100);
			int val2 = (int)_objColor.G - _intPercent * (int)(_objColor.G / 100);
			int val3 = (int)_objColor.B - _intPercent * (int)(_objColor.B / 100);
			return Color.FromArgb(Math.Min(val, 255), Math.Min(val2, 255), Math.Min(val3, 255));
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void GenerateColorsPallet()
		{
			this.aColors = this.GenerateColorsPallet(this.aColor, this.Active, (int)Math.Floor((double)this.aNumberOfSpoke / 3.0));
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private Color[] GenerateColorsPallet(Color _objColor, bool _blnShadeColor, int _intNbSpoke)
		{
			Color[] array = new Color[this.NumberSpoke];
			for (int i = 0; i < this.NumberSpoke; i++)
			{
				if (_blnShadeColor)
				{
					if (i == 0 || i < this.NumberSpoke - _intNbSpoke)
					{
						array[i] = _objColor;
					}
					else
					{
						array[i] = this.Darken(array[i - 1], 10);
					}
				}
				else
				{
					array[i] = _objColor;
				}
			}
			return array;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void GetControlCenterPoint()
		{
			this.aCenterPoint = this.GetControlCenterPoint(this);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private PointF GetControlCenterPoint(Control _objControl)
		{
			return new PointF((float)(_objControl.Width / 2), (float)(_objControl.Height / 2));
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void DrawLine(Graphics _objGraphics, PointF _objPointOne, PointF _objPointTwo, Color _objColor, int _intLineThickness)
		{
			_objGraphics.DrawLine(new Pen(new SolidBrush(_objColor), (float)_intLineThickness)
			{
				StartCap = LineCap.Round,
				EndCap = LineCap.Round
			}, _objPointOne, _objPointTwo);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private PointF GetCoordinate(PointF _objCircleCenter, int _intRadius, double _dblAngle)
		{
			PointF result = default(PointF);
			double num = 3.1415926535897931 * _dblAngle / 180.0;
			result.X = _objCircleCenter.X + (float)_intRadius * (float)Math.Cos(num);
			result.Y = _objCircleCenter.Y + (float)_intRadius * (float)Math.Sin(num);
			return result;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void GetSpokesAngles()
		{
			this.aAngles = this.GetSpokesAngles(this.NumberSpoke);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private double[] GetSpokesAngles(int _shtNumberSpoke)
		{
			double[] array = new double[_shtNumberSpoke];
			double num = (double)(360 / _shtNumberSpoke);
			for (int i = 0; i < _shtNumberSpoke; i++)
			{
				array[i] = ((i == 0) ? num : (array[i - 1] + num));
			}
			return array;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ActiveTimer()
		{
			if (this.aTimerActive)
			{
				this.aTimer.Start();
			}
			else
			{
				this.aTimer.Stop();
				this.aProgressValue = 0;
			}
			this.GenerateColorsPallet();
			base.Invalidate();
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
	}
}
