using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace i2TradePlus.User_Control
{
	public class ucMarketStateBox : UserControl
	{
		public delegate void SwitchMode();
		public delegate void CallAlert();
		private delegate void ShowFeedsStateCallback(bool isFeeding);
		private IContainer components = null;
		private Label lbTimerServer;
		private Label lbPushMode;
		private AlertStockUC alertStockControl;
		private ToolTip toolTip1;
		private Timer tmerServer;
		private SolidBrush _feedColor;
        public  ucMarketStateBox.SwitchMode _OnSitchMode;
		public event ucMarketStateBox.SwitchMode OnSitchMode
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._OnSitchMode += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._OnSitchMode -= value;
            }
		}
		public ucMarketStateBox.CallAlert _OnCallAlert;
		public event ucMarketStateBox.CallAlert OnCallAlert
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._OnCallAlert += value;
            }
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._OnCallAlert -= value;
            }
		}
		public bool IsAllowBlinkAlert
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.alertStockControl.IsAllowBlink;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				if (!base.DesignMode)
				{
					this.alertStockControl.IsAllowBlink = value;
				}
			}
		}
		public bool IsAlertStart
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.alertStockControl.IsStarted;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				if (!base.DesignMode)
				{
					this.alertStockControl.IsStarted = value;
				}
			}
		}
		public int AlterMessageCount
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.alertStockControl.AlterMessageCount;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.alertStockControl.AlterMessageCount = value;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ucMarketStateBox()
		{
			this.InitializeComponent();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ucMarketStateBox_Load(object sender, EventArgs e)
		{
			if (ApplicationInfo.MarketTime == DateTime.MinValue)
			{
				ApplicationInfo.MarketTime = DateTime.Now;
			}
			if (!base.DesignMode)
			{
				this.tmerServer.Start();
			}
			base.Width = this.lbPushMode.Right + 5;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void lbPushMode_Click(object sender, EventArgs e)
		{
			try
			{
				if (ApplicationInfo.IsPushMode)
				{
					ApplicationInfo.TunnelCounter++;
					if (ApplicationInfo.TunnelCounter > ApplicationInfo.TunnelHosts.Count - 1)
					{
						ApplicationInfo.IsPushMode = false;
						for (int i = 0; i < ApplicationInfo.TunnelHosts.Count; i++)
						{
							ApplicationInfo.TunnelHosts[i].IsAlreadyStart = false;
						}
					}
				}
				else
				{
					ApplicationInfo.IsPushMode = true;
					ApplicationInfo.TunnelCounter = 0;
				}
				if (this._OnSitchMode != null)
				{
					this._OnSitchMode();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("ucSETSwep_OnPushPullChanged", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowError(string methodName, Exception ex)
		{
			ExceptionManager.Show(new ErrorItem(DateTime.Now, base.Name, methodName, ex.Message, ex.ToString()));
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void alertStockControl_AlertClick(object sender, EventArgs e)
		{
			if (this._OnCallAlert != null)
			{
				this.alertStockControl.IsAllowBlink = false;
				this.alertStockControl.AlterMessageCount = 0;
				this._OnCallAlert();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ucMarketStateBox_Paint(object sender, PaintEventArgs e)
		{
			try
			{
				this.DrawFeedStatus(e.Graphics);
				e.Graphics.DrawRectangle(Pens.DimGray, 0, 0, base.Width - 1, base.Height - 1);
			}
			catch
			{
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void DrawFeedStatus(Graphics e)
		{
			try
			{
				e.FillRectangle(this._feedColor, new Rectangle(this.lbPushMode.Left - 10, this.lbPushMode.Top + 1, 7, this.lbPushMode.Height - 2));
			}
			catch
			{
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ShowFeedsState(bool isFeeding)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new ucMarketStateBox.ShowFeedsStateCallback(this.ShowFeedsState), new object[]
				{
					isFeeding
				});
			}
			else
			{
				if (ApplicationInfo.IsFeedsStarted)
				{
					this._feedColor = new SolidBrush(Color.Lime);
				}
				else
				{
					this._feedColor = new SolidBrush(Color.Red);
				}
				base.Invalidate();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void DisplayPushPullImage()
		{
			if (this.lbPushMode.InvokeRequired)
			{
				this.lbPushMode.Invoke(new MethodInvoker(this.DisplayPushPullImage));
			}
			else
			{
				try
				{
					if (ApplicationInfo.IsPushMode)
					{
						this.lbPushMode.Text = "PUSH";
						string caption = ApplicationInfo.TunnelHosts[ApplicationInfo.TunnelCounter].HostIP + ":" + ApplicationInfo.TunnelHosts[ApplicationInfo.TunnelCounter].Port;
						this.toolTip1.SetToolTip(this.lbPushMode, caption);
					}
					else
					{
						this.lbPushMode.Text = "PULL";
						this.toolTip1.SetToolTip(this.lbPushMode, "PULL");
					}
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tmerServer_Tick(object sender, EventArgs e)
		{
			try
			{
				ApplicationInfo.MarketTime = ApplicationInfo.MarketTime.AddSeconds(1.0);
				this.lbTimerServer.Text = ApplicationInfo.MarketTime.ToString("HH:mm:ss");
			}
			catch
			{
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ucMarketStateBox_Resize(object sender, EventArgs e)
		{
			this.lbPushMode.Top = (base.Height - this.lbPushMode.Height) / 2;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ucMarketStateBox));
			this.lbTimerServer = new Label();
			this.lbPushMode = new Label();
			this.toolTip1 = new ToolTip(this.components);
			this.tmerServer = new Timer(this.components);
			this.alertStockControl = new AlertStockUC();
			base.SuspendLayout();
			this.lbTimerServer.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left);
			this.lbTimerServer.Font = new Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lbTimerServer.ForeColor = Color.WhiteSmoke;
			this.lbTimerServer.ImageAlign = ContentAlignment.MiddleLeft;
			this.lbTimerServer.Location = new Point(5, 2);
			this.lbTimerServer.Name = "lbTimerServer";
			this.lbTimerServer.Size = new Size(62, 41);
			this.lbTimerServer.TabIndex = 61;
			this.lbTimerServer.Text = "00:00:00";
			this.lbTimerServer.TextAlign = ContentAlignment.MiddleCenter;
			this.lbPushMode.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left);
			this.lbPushMode.AutoSize = true;
			this.lbPushMode.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lbPushMode.ForeColor = Color.LightGray;
			this.lbPushMode.Location = new Point(124, 15);
			this.lbPushMode.Margin = new Padding(1, 0, 1, 0);
			this.lbPushMode.Name = "lbPushMode";
			this.lbPushMode.Size = new Size(41, 15);
			this.lbPushMode.TabIndex = 59;
			this.lbPushMode.Text = "PUSH";
			this.lbPushMode.TextAlign = ContentAlignment.MiddleCenter;
			this.lbPushMode.Click += new EventHandler(this.lbPushMode_Click);
			this.tmerServer.Interval = 1000;
			this.tmerServer.Tick += new EventHandler(this.tmerServer_Tick);
			this.alertStockControl.AlterMessageCount = 0;
			this.alertStockControl.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left);
			this.alertStockControl.BackColor = Color.Transparent;
			this.alertStockControl.BlinkColor = Color.OrangeRed;
			this.alertStockControl.BlinkImage = (Image)componentResourceManager.GetObject("alertStockControl.BlinkImage");
			this.alertStockControl.Border = BorderStyle.None;
			this.alertStockControl.DisplayImage = (Image)componentResourceManager.GetObject("alertStockControl.DisplayImage");
			this.alertStockControl.ForeColor = SystemColors.Control;
			this.alertStockControl.IsAllowBlink = false;
			this.alertStockControl.IsStarted = false;
			this.alertStockControl.Location = new Point(70, 3);
			this.alertStockControl.Margin = new Padding(1, 0, 1, 0);
			this.alertStockControl.Name = "alertStockControl";
			this.alertStockControl.NormalColor = Color.Transparent;
			this.alertStockControl.NormalImage = (Image)componentResourceManager.GetObject("alertStockControl.NormalImage");
			this.alertStockControl.Size = new Size(40, 39);
			this.alertStockControl.TabIndex = 58;
			this.alertStockControl.AlertClick += new EventHandler(this.alertStockControl_AlertClick);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.Black;
			base.Controls.Add(this.lbTimerServer);
			base.Controls.Add(this.lbPushMode);
			base.Controls.Add(this.alertStockControl);
			base.Name = "ucMarketStateBox";
			base.Size = new Size(168, 46);
			base.Load += new EventHandler(this.ucMarketStateBox_Load);
			base.Paint += new PaintEventHandler(this.ucMarketStateBox_Paint);
			base.Resize += new EventHandler(this.ucMarketStateBox_Resize);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
