using i2TradePlus.Properties;
using STIControl.CustomGrid;
using STIControl.ExpandTableGrid;
using STIControl.SortTableGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
namespace i2TradePlus
{
	public class ClientBaseForm : Form
	{
		public enum ClientBaseFormState
		{
			Opening,
			Showed,
			Closing,
			Hide
		}
		private delegate void ShowSplashCallBack(bool visible);
		public delegate void OnReActiveEventHandler();
		public delegate void CustomSizeChangedEventHandler();
		public delegate void OnShownDelayEventHandler();
		public delegate void OnFormKeyUpEventHandler(KeyEventArgs e);
		public delegate void OnSymbolLinkEventHandler(object sender, SymbolLinkSource source, string newStock);
		public delegate void OnFontChangedEventHandler();
		public delegate void OnIDoLoadDataEventHandler();
		public delegate void OnHeaderChangedEventHandler();
		private delegate void SetFontToControlCallBack(Font newFont);
		private IContainer components = null;
		private Label lbLoading;
		public ClientBaseForm.ClientBaseFormState FormState = ClientBaseForm.ClientBaseFormState.Opening;
		private Font MyFont = Settings.Default.Default_Font;
		private bool isLoadingData = true;
		private bool isAllowRender = true;
		private Dictionary<string, object> propertiesValue = null;
		private bool isRaiseEventIDoFontChanged = false;
		private System.Windows.Forms.Timer timerLoading = null;
		public bool IsWidthChanged = false;
		public bool IsHeightChanged = false;
        public ClientBaseForm.OnReActiveEventHandler _IDoReActivated;
		public event ClientBaseForm.OnReActiveEventHandler IDoReActivated
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._IDoReActivated += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._IDoReActivated -= value;
			}
		}
        public ClientBaseForm.CustomSizeChangedEventHandler _IDoCustomSizeChanged;
		public event ClientBaseForm.CustomSizeChangedEventHandler IDoCustomSizeChanged
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._IDoCustomSizeChanged += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._IDoCustomSizeChanged -= value;
			}
		}
        public  ClientBaseForm.OnShownDelayEventHandler _IDoShownDelay;
		public event ClientBaseForm.OnShownDelayEventHandler IDoShownDelay
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._IDoShownDelay += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._IDoShownDelay -=  value;
			}
		}
        public ClientBaseForm.OnFormKeyUpEventHandler _IDoMainFormKeyUp;
		public event ClientBaseForm.OnFormKeyUpEventHandler IDoMainFormKeyUp
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._IDoMainFormKeyUp += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._IDoMainFormKeyUp -= value;
			}
		}
        public ClientBaseForm.OnSymbolLinkEventHandler _IDoSymbolLinked;
		public event ClientBaseForm.OnSymbolLinkEventHandler IDoSymbolLinked
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this._IDoSymbolLinked += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this._IDoSymbolLinked -= value;
			}
		}
        public ClientBaseForm.OnFontChangedEventHandler _IDoFontChanged;
		public event ClientBaseForm.OnFontChangedEventHandler IDoFontChanged
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._IDoFontChanged +=  value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._IDoFontChanged -= value;
			}
		}
        public ClientBaseForm.OnIDoLoadDataEventHandler _IDoLoadData;
		public event ClientBaseForm.OnIDoLoadDataEventHandler IDoLoadData
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._IDoLoadData += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._IDoLoadData -= value;
			}
		}
        public ClientBaseForm.OnHeaderChangedEventHandler _IDoHeaderChanged;
		public event ClientBaseForm.OnHeaderChangedEventHandler IDoHeaderChanged
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
                this._IDoHeaderChanged += value;
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
                this._IDoHeaderChanged -=  value;
			}
		}
		public bool IsLoadingData
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isLoadingData;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				if (!base.DesignMode)
				{
					this.isLoadingData = value;
					this.ShowSplash(this.isLoadingData);
				}
			}
		}
		public bool IsAllowRender
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isAllowRender;
			}
		}
		public Dictionary<string, object> PropertiesValue
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (this.propertiesValue == null)
				{
					this.propertiesValue = new Dictionary<string, object>();
				}
				return this.propertiesValue;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.propertiesValue = value;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void Dispose(bool disposing)
		{
			if (this.timerLoading != null)
			{
				this.timerLoading.Stop();
				this.timerLoading.Dispose();
				this.timerLoading = null;
			}
			if (this.propertiesValue != null)
			{
				this.propertiesValue.Clear();
				this.propertiesValue = null;
			}
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void InitializeComponent()
		{
			this.lbLoading = new Label();
			base.SuspendLayout();
			this.lbLoading.AutoSize = true;
			this.lbLoading.BackColor = Color.FromArgb(64, 64, 64);
			this.lbLoading.BorderStyle = BorderStyle.FixedSingle;
			this.lbLoading.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.lbLoading.ForeColor = Color.Yellow;
			this.lbLoading.Location = new Point(117, 118);
			this.lbLoading.Name = "lbLoading";
			this.lbLoading.Padding = new Padding(5, 3, 5, 3);
			this.lbLoading.Size = new Size(76, 23);
			this.lbLoading.TabIndex = 6;
			this.lbLoading.Text = "Loading ...";
			this.lbLoading.TextAlign = ContentAlignment.MiddleCenter;
			this.lbLoading.Visible = false;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = SystemColors.Control;
			base.ClientSize = new Size(290, 250);
			base.Controls.Add(this.lbLoading);
			base.FormBorderStyle = FormBorderStyle.SizableToolWindow;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ClientBaseForm";
			base.StartPosition = FormStartPosition.Manual;
			this.Text = "ClientBaseForm";
			base.ResumeLayout(false);
			base.PerformLayout();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal void ShowSplash(bool visible)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new ClientBaseForm.ShowSplashCallBack(this.ShowSplash), new object[]
				{
					visible
				});
			}
			else
			{
				if (!base.DesignMode)
				{
					if (ApplicationInfo.SuuportSplash == "Y")
					{
						if (visible)
						{
							this.lbLoading.Left = (base.Width - this.lbLoading.Width) / 2;
							this.lbLoading.Top = (base.Height - this.lbLoading.Height) / 2;
							this.lbLoading.Visible = true;
							this.lbLoading.BringToFront();
						}
						else
						{
							this.lbLoading.Visible = false;
						}
					}
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ClientBaseForm()
		{
			this.SetControl();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ClientBaseForm(Dictionary<string, object> propertiesValue)
		{
			this.SetControl();
			if (!base.DesignMode)
			{
				this.propertiesValue = propertiesValue;
				this.UnpackProperties();
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetControl()
		{
			try
			{
				this.SetGlobalization();
				this.InitializeComponent();
				base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
				this.DoubleBuffered = false;
				base.UpdateStyles();
				if (!base.DesignMode)
				{
					this.SetAutoRepaint(false);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SetControl", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetHeaderColor(bool isRedraw)
		{
			try
			{
				foreach (Control control in base.Controls)
				{
					if (control.GetType() == typeof(SortGrid) || control.GetType() == typeof(ExpandGrid))
					{
						this.SetHeader(control, isRedraw);
					}
					else
					{
						if (control.GetType() == typeof(Panel))
						{
							foreach (Control control2 in control.Controls)
							{
								if (control2.GetType() == typeof(SortGrid) || control2.GetType() == typeof(ExpandGrid))
								{
									this.SetHeader(control2, isRedraw);
								}
								else
								{
									if (control2.GetType() == typeof(Panel))
									{
										foreach (Control control3 in control2.Controls)
										{
											if (control3.GetType() == typeof(SortGrid) || control3.GetType() == typeof(ExpandGrid))
											{
												this.SetHeader(control3, isRedraw);
											}
										}
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SetHeaderBackColor", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetHeader(Control control, bool isRedraw)
		{
			try
			{
				if (control.GetType() == typeof(ExpandGrid))
				{
					foreach (STIControl.ExpandTableGrid.ColumnItem current in ((ExpandGrid)control).Columns)
					{
						current.BackColor = Settings.Default.HeaderBackGColor;
						current.FontColor = Settings.Default.HeaderFontColor;
					}
					((ExpandGrid)control).GridColor = Settings.Default.GridColor;
					if (isRedraw)
					{
						((ExpandGrid)control).Redraw();
					}
				}
				else
				{
					if (control.GetType() == typeof(SortGrid))
					{
						foreach (STIControl.SortTableGrid.ColumnItem current2 in ((SortGrid)control).Columns)
						{
							current2.BackColor = Settings.Default.HeaderBackGColor;
							current2.FontColor = Settings.Default.HeaderFontColor;
						}
						((SortGrid)control).GridColor = Settings.Default.GridColor;
						if (isRedraw)
						{
							((SortGrid)control).Redraw();
						}
					}
				}
				if (this._IDoHeaderChanged != null)
				{
					this._IDoHeaderChanged();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetAutoRepaint(bool isAutoRepaint)
		{
			try
			{
				foreach (Control control in base.Controls)
				{
					if (control.GetType() == typeof(SortGrid))
					{
						((SortGrid)control).IsAutoRepaint = isAutoRepaint;
					}
					else
					{
						if (control.GetType() == typeof(ExpandGrid))
						{
							((ExpandGrid)control).IsAutoRepaint = isAutoRepaint;
						}
						else
						{
							if (control.GetType() == typeof(IntzaCustomGrid))
							{
								((IntzaCustomGrid)control).IsAutoRepaint = isAutoRepaint;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SetAutoRepaint", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void timerLoading_Tick(object sender, EventArgs e)
		{
			this.timerLoading.Enabled = false;
			if (this.FormState != ClientBaseForm.ClientBaseFormState.Closing)
			{
				if (this._IDoLoadData != null)
				{
					this._IDoLoadData();
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void OpenedForm()
		{
			try
			{
				if (this.timerLoading == null)
				{
					this.timerLoading = new System.Windows.Forms.Timer();
					this.timerLoading.Interval = 100;
					this.timerLoading.Tick += new EventHandler(this.timerLoading_Tick);
				}
				this.timerLoading.Enabled = false;
				this.timerLoading.Enabled = true;
			}
			catch (Exception ex)
			{
				this.ShowError("OpenedForm", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void OpenForm(bool isFirstOpen)
		{
			if (this.FormState != ClientBaseForm.ClientBaseFormState.Closing)
			{
				this.SetHeaderColor(false);
				if (isFirstOpen)
				{
					this.SetFontToControl(Settings.Default.Default_Font);
					this.isRaiseEventIDoFontChanged = true;
					this.FormState = ClientBaseForm.ClientBaseFormState.Showed;
					if (this._IDoShownDelay != null)
					{
						this._IDoShownDelay();
					}
				}
				else
				{
					if (this.FormState != ClientBaseForm.ClientBaseFormState.Opening)
					{
						this.SetAutoRepaint(true);
						if (!ApplicationInfo.IsAreadyLogin)
						{
							return;
						}
						this.FormState = ClientBaseForm.ClientBaseFormState.Showed;
						this.IsWidthChanged = false;
						if (this.MyFont != Settings.Default.Default_Font)
						{
							this.isRaiseEventIDoFontChanged = false;
							this.SetFontToControl(Settings.Default.Default_Font);
							this.isRaiseEventIDoFontChanged = true;
							this.IsWidthChanged = true;
						}
						if (this._IDoReActivated != null)
						{
							Rectangle workingArea = ((frmMain)base.Parent).GetWorkingArea(this);
							this.IsWidthChanged = (workingArea.Width > 0 && workingArea.Width != base.Width);
							if (this.IsWidthChanged)
							{
								base.Width = workingArea.Width;
							}
							this.IsHeightChanged = (workingArea.Height > 0 && workingArea.Height != base.Height);
							if (this.IsHeightChanged)
							{
								base.Height = workingArea.Height;
							}
							this._IDoReActivated();
						}
					}
				}
				this.isAllowRender = true;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void HideForm()
		{
			this.isAllowRender = false;
			this.FormState = ClientBaseForm.ClientBaseFormState.Hide;
			this.SetAutoRepaint(false);
			this.IsLoadingData = false;
			base.Hide();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetFormSize()
		{
			try
			{
				if (this.FormState != ClientBaseForm.ClientBaseFormState.Closing)
				{
					Rectangle workingArea = ((frmMain)base.Parent).GetWorkingArea(this);
					this.IsWidthChanged = (workingArea.Width > 0 && workingArea.Width != base.Width);
					this.IsHeightChanged = (workingArea.Height > 0 && workingArea.Height != base.Height);
					if (this.IsWidthChanged)
					{
						base.Width = workingArea.Width;
					}
					if (this.IsHeightChanged)
					{
						base.Height = workingArea.Height;
					}
					if (this._IDoCustomSizeChanged != null)
					{
						if (this.IsWidthChanged || this.IsHeightChanged)
						{
							this._IDoCustomSizeChanged();
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnClosing(CancelEventArgs e)
		{
			this.FormState = ClientBaseForm.ClientBaseFormState.Closing;
			this.isLoadingData = true;
			if (this.timerLoading != null)
			{
				this.timerLoading.Enabled = false;
			}
			base.OnClosing(e);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnPaint(PaintEventArgs e)
		{
			if (this.FormState != ClientBaseForm.ClientBaseFormState.Closing)
			{
				if (this.FormState == ClientBaseForm.ClientBaseFormState.Showed)
				{
					base.OnPaint(e);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnFontChanged(EventArgs e)
		{
			this.MyFont = Settings.Default.Default_Font;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetGlobalization()
		{
			CultureInfo cultureInfo = new CultureInfo("en-US");
			cultureInfo.DateTimeFormat.LongTimePattern = "HH:mm:ss";
			Thread.CurrentThread.CurrentCulture = cultureInfo;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetFontToControl(Font newFont)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new ClientBaseForm.SetFontToControlCallBack(this.SetFontToControl), new object[]
				{
					newFont
				});
			}
			else
			{
				try
				{
					this.MyFont = newFont;
					foreach (Control control in base.Controls)
					{
						if (control.GetType() == typeof(ToolStrip))
						{
							this.setToolStripFont(control as ToolStrip);
						}
						else
						{
							if (control.GetType() == typeof(Panel))
							{
								if (control.Tag == null)
								{
									foreach (Control control2 in ((Panel)control).Controls)
									{
										if (control2.GetType() == typeof(Panel))
										{
											if (control2.Tag == null)
											{
												foreach (Control control3 in ((Panel)control2).Controls)
												{
													if (control3.GetType() == typeof(ToolStrip))
													{
														this.setToolStripFont(control3 as ToolStrip);
													}
													else
													{
														control3.Font = newFont;
													}
												}
											}
										}
										else
										{
											if (control2.GetType() == typeof(ToolStrip))
											{
												this.setToolStripFont(control2 as ToolStrip);
											}
											else
											{
												control2.Font = newFont;
											}
										}
									}
								}
							}
							else
							{
								if (control.GetType() == typeof(ExpandGrid) || control.GetType() == typeof(SortGrid) || control.GetType() == typeof(IntzaCustomGrid))
								{
									control.Font = newFont;
								}
							}
						}
					}
					if (this.isRaiseEventIDoFontChanged && this._IDoFontChanged != null)
					{
						this.IsWidthChanged = true;
						this.IsHeightChanged = true;
						this._IDoFontChanged();
					}
				}
				catch (Exception ex)
				{
					this.ShowError("SetControlFont", ex);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void setToolStripFont(ToolStrip control)
		{
			try
			{
				if (control.Tag != null && control.Tag.ToString() == "-1")
				{
					Font font = new Font(Settings.Default.Default_Font.Name, Settings.Default.Default_Font.Size - 1f, FontStyle.Regular);
					control.Font = font;
					foreach (ToolStripItem toolStripItem in control.Items)
					{
						toolStripItem.Font = font;
					}
				}
				else
				{
					control.Font = Settings.Default.Default_Font;
					foreach (ToolStripItem toolStripItem2 in control.Items)
					{
						if (toolStripItem2.Tag != null && toolStripItem2.Tag.ToString() == "0")
						{
							toolStripItem2.Font = new Font("Wingdings", Settings.Default.Default_Font.Size);
						}
						else
						{
							if (toolStripItem2.Tag != null && toolStripItem2.Tag.ToString() == "S")
							{
								toolStripItem2.Font = new Font("Tempus Sans ITC", Settings.Default.Default_Font.Size);
							}
							else
							{
								toolStripItem2.Font = Settings.Default.Default_Font;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("setToolStripFont", ex);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Dictionary<string, object> PackProperties()
		{
			if (this.propertiesValue == null)
			{
				this.propertiesValue = new Dictionary<string, object>();
			}
			return this.DoPackProperties();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void UnpackProperties()
		{
			this.DoUnpackProperties();
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReceiveKeyupMainForm(KeyEventArgs e)
		{
			if (this._IDoMainFormKeyUp != null)
			{
				this._IDoMainFormKeyUp(e);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetSymbolLink(object sender, SymbolLinkSource source, string newStock)
		{
			if (this._IDoSymbolLinked != null)
			{
				this._IDoSymbolLinked(sender, source, newStock);
			}
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected virtual Dictionary<string, object> DoPackProperties()
		{
			return this.propertiesValue;
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected virtual void DoUnpackProperties()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected virtual void ShowError(string functionName, Exception ex)
		{
			ExceptionManager.Show(new ErrorItem(DateTime.Now, base.Name, functionName, ex.Message, ex.ToString()));
		}
	}
}
