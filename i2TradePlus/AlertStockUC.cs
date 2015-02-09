using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace i2TradePlus
{
	internal class AlertStockUC : UserControl
	{
		public enum BlinkStyles
		{
			BlinkBackColor,
			BlinkImage
		}
		private bool isUseNormalImage = true;
		private AlertStockUC.BlinkStyles blinkStyle = AlertStockUC.BlinkStyles.BlinkImage;
		private Image displayImage = null;
		private Color normalColor = Color.Transparent;
		private Color blinkColor = Color.Orange;
		private int _alterMessageCount = 0;
		private bool isAllowBlink = false;
		private IContainer components = null;
		private PictureBox pictureBoxAlert;
		private Timer timerChangeBackColor;
		private PictureBox pictureBoxAlertNormal;
		private PictureBox pictureBoxAlertBlink;
        public EventHandler _AlertClick;
        public event EventHandler AlertClick
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._AlertClick += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._AlertClick -= value;
			}
		}
		public Image DisplayImage
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (this.displayImage == null)
				{
					this.displayImage = this.pictureBoxAlert.Image;
				}
				return this.pictureBoxAlert.Image;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.displayImage = value;
				if (!base.DesignMode)
				{
					this.pictureBoxAlert.Image = value;
				}
			}
		}
		[DefaultValue(PictureBoxSizeMode.CenterImage)]
		public PictureBoxSizeMode ImageSizeMode
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				PictureBoxSizeMode result;
				if (this.pictureBoxAlert != null)
				{
					result = this.pictureBoxAlert.SizeMode;
				}
				else
				{
					result = PictureBoxSizeMode.Normal;
				}
				return result;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				if (this.pictureBoxAlert != null)
				{
					this.pictureBoxAlert.SizeMode = value;
				}
			}
		}
		public Image NormalImage
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				Image result;
				if (this.pictureBoxAlertNormal != null)
				{
					result = this.pictureBoxAlertNormal.Image;
				}
				else
				{
					result = null;
				}
				return result;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				if (this.pictureBoxAlertNormal != null)
				{
					this.pictureBoxAlertNormal.Image = value;
				}
			}
		}
		public Image BlinkImage
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				Image result;
				if (this.pictureBoxAlertBlink != null)
				{
					result = this.pictureBoxAlertBlink.Image;
				}
				else
				{
					result = null;
				}
				return result;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				if (this.pictureBoxAlertBlink != null)
				{
					this.pictureBoxAlertBlink.Image = value;
				}
			}
		}
		public Color NormalColor
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.normalColor;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.normalColor = value;
			}
		}
		public Color BlinkColor
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.blinkColor;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.blinkColor = value;
			}
		}
		[DefaultValue(500)]
		public int BlinkFrequancy
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				int result;
				if (this.timerChangeBackColor != null)
				{
					result = this.timerChangeBackColor.Interval;
				}
				else
				{
					result = -1;
				}
				return result;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				if (this.timerChangeBackColor != null)
				{
					this.timerChangeBackColor.Interval = value;
				}
			}
		}
		public int AlterMessageCount
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this._alterMessageCount;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this._alterMessageCount = value;
			}
		}
		public bool IsStarted
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return !base.DesignMode && this.timerChangeBackColor.Enabled;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				if (!base.DesignMode)
				{
					this.timerChangeBackColor.Enabled = value;
				}
			}
		}
		public bool IsAllowBlink
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isAllowBlink;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.isAllowBlink = value;
				if (!base.DesignMode)
				{
					if (!this.IsAllowBlink)
					{
						if (!this.isUseNormalImage)
						{
							this.pictureBoxAlert.Image = this.pictureBoxAlertNormal.Image;
						}
					}
				}
			}
		}
		[DefaultValue(BorderStyle.FixedSingle)]
		public BorderStyle Border
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				BorderStyle result;
				if (!base.DesignMode)
				{
					result = this.pictureBoxAlert.BorderStyle;
				}
				else
				{
					result = BorderStyle.None;
				}
				return result;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				if (!base.DesignMode)
				{
					this.pictureBoxAlert.BorderStyle = value;
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public AlertStockUC()
		{
			this.InitializeComponent();
			if (!base.DesignMode)
			{
				this.BackColor = this.normalColor;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void timerChangeBackColor_Tick(object sender, EventArgs e)
		{
			if (this._alterMessageCount > 0 && this.isAllowBlink)
			{
				if (this.blinkStyle == AlertStockUC.BlinkStyles.BlinkBackColor)
				{
					if (this.BackColor == this.normalColor)
					{
						this.BackColor = this.blinkColor;
					}
					else
					{
						this.BackColor = this.normalColor;
					}
				}
				else
				{
					if (this.isUseNormalImage)
					{
						this.pictureBoxAlert.Image = this.pictureBoxAlertBlink.Image;
					}
					else
					{
						this.pictureBoxAlert.Image = this.pictureBoxAlertNormal.Image;
					}
					this.isUseNormalImage = !this.isUseNormalImage;
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void pictureBoxAlert_MouseEnter(object sender, EventArgs e)
		{
			this.BackColor = Color.LightSteelBlue;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void pictureBoxAlert_MouseLeave(object sender, EventArgs e)
		{
			this.BackColor = this.normalColor;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void pictureBoxAlert_Click(object sender, EventArgs e)
		{
			if (this._AlertClick != null)
			{
				this._AlertClick(sender, e);
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
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(AlertStockUC));
			this.pictureBoxAlert = new PictureBox();
			this.timerChangeBackColor = new Timer(this.components);
			this.pictureBoxAlertNormal = new PictureBox();
			this.pictureBoxAlertBlink = new PictureBox();
			((ISupportInitialize)this.pictureBoxAlert).BeginInit();
			((ISupportInitialize)this.pictureBoxAlertNormal).BeginInit();
			((ISupportInitialize)this.pictureBoxAlertBlink).BeginInit();
			base.SuspendLayout();
			this.pictureBoxAlert.BackColor = Color.Transparent;
			this.pictureBoxAlert.Dock = DockStyle.Fill;
			this.pictureBoxAlert.Image = (Image)componentResourceManager.GetObject("pictureBoxAlert.Image");
			this.pictureBoxAlert.Location = new Point(0, 0);
			this.pictureBoxAlert.Margin = new Padding(0);
			this.pictureBoxAlert.Name = "pictureBoxAlert";
			this.pictureBoxAlert.Size = new Size(43, 38);
			this.pictureBoxAlert.SizeMode = PictureBoxSizeMode.CenterImage;
			this.pictureBoxAlert.TabIndex = 0;
			this.pictureBoxAlert.TabStop = false;
			this.pictureBoxAlert.MouseLeave += new EventHandler(this.pictureBoxAlert_MouseLeave);
			this.pictureBoxAlert.Click += new EventHandler(this.pictureBoxAlert_Click);
			this.pictureBoxAlert.MouseEnter += new EventHandler(this.pictureBoxAlert_MouseEnter);
			this.timerChangeBackColor.Interval = 500;
			this.timerChangeBackColor.Tick += new EventHandler(this.timerChangeBackColor_Tick);
			this.pictureBoxAlertNormal.BackColor = Color.Transparent;
			this.pictureBoxAlertNormal.Image = (Image)componentResourceManager.GetObject("pictureBoxAlertNormal.Image");
			this.pictureBoxAlertNormal.Location = new Point(3, 34);
			this.pictureBoxAlertNormal.Name = "pictureBoxAlertNormal";
			this.pictureBoxAlertNormal.Size = new Size(27, 38);
			this.pictureBoxAlertNormal.TabIndex = 1;
			this.pictureBoxAlertNormal.TabStop = false;
			this.pictureBoxAlertNormal.Visible = false;
			this.pictureBoxAlertBlink.BackColor = Color.Transparent;
			this.pictureBoxAlertBlink.Image = (Image)componentResourceManager.GetObject("pictureBoxAlertBlink.Image");
			this.pictureBoxAlertBlink.Location = new Point(36, 34);
			this.pictureBoxAlertBlink.Name = "pictureBoxAlertBlink";
			this.pictureBoxAlertBlink.Size = new Size(27, 38);
			this.pictureBoxAlertBlink.TabIndex = 2;
			this.pictureBoxAlertBlink.TabStop = false;
			this.pictureBoxAlertBlink.Visible = false;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.Transparent;
			base.Controls.Add(this.pictureBoxAlert);
			base.Controls.Add(this.pictureBoxAlertBlink);
			base.Controls.Add(this.pictureBoxAlertNormal);
			base.Margin = new Padding(0);
			base.Name = "AlertStockUC";
			base.Size = new Size(43, 38);
			((ISupportInitialize)this.pictureBoxAlert).EndInit();
			((ISupportInitialize)this.pictureBoxAlertNormal).EndInit();
			((ISupportInitialize)this.pictureBoxAlertBlink).EndInit();
			base.ResumeLayout(false);
		}
	}
}
